using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using ECNC3.Enumeration;
using System.Windows;
using ECNC3.Models.McIf;
using ECNC3.Models;
using System.IO;

namespace ECNC3.Views
{
    /// <summary>
    /// テクノボードへの処理と、ボード状態監視用
    /// </summary>
    internal class ApiMonitorTask : IDisposable
    {
        /// <summary>
        /// スレッドの排他制御
        /// </summary>
        public static Mutex monitorMx = new Mutex(true, "ApiMonitorTask");
        /// <summary>
        /// 各インスタンスの初期化
        /// </summary>
        internal ApiMonitorTask()
        {
            McIO = new Models.McIf.McDatIOData();
            McSta = new Models.McIf.McDatStatus();
            McPcond = new Models.McIf.McDatProcessCondition();
            McAec = new McDatAecData();
            McRom = new McDatRomSwitch();
            McCode = new McDatGcd();
            McIni = new McDatInitialPrm();
            Mode = MonitoringMode.Main;
            LoopBreak = false;
            procLog = new McDatProcessLog();
            MaintTimer = new MaintenanceTimerView();
            MaintTimer.MainPowerTimerStart();
       }

        /// <summary>
        /// 状態パラメータクラス
        /// </summary>
        public MonitoringItems Items = new MonitoringItems();

        internal Models.McIf.McDatIOData McIO;
        internal Models.McIf.McDatStatus McSta;
        internal Models.McIf.McDatProcessCondition McPcond;
        internal McDatAecData McAec;
        internal McDatRomSwitch McRom;
        internal McDatGcd McCode;
        internal McDatInitialPrm McIni;
        #region 加工ログで使用
        internal McDatProcessLog procLog;
        private bool procLoggingNow = false;
        #endregion
        /// <summary>
        /// 状態監視処理のイベント
        /// </summary>
        public event StatusMonitoringEventHandler StatusMonitoringEvent;

        /// <summary>
        /// 状態監視(IOのみ)処理のイベント
        /// </summary>
        public event IOMonitoringEventHandler IOMonitoringEvent;

        /// <summary>
        /// 状態監視名列挙体
        /// </summary>
        internal MonitorTargets Monitor { get; set; }

        /// <summary>
        /// 状態監視モード
        /// </summary>
        internal MonitoringMode Mode { get; set; }

        //ループ判定
        internal bool LoopBreak { get; set; }

        internal int EventHandlarCount { get; set; }

		private McDatStatusAlarm _alarmCheck = null;
        public MaintenanceTimerView MaintTimer = null;
        /// <summary>
        /// スレッド処理
        /// </summary>
        /// <remarks>
        /// 値の取得は、メンバーの「StrMcAxis」から参照する。
        /// </remarks>
        internal async void ApiMonitoring()
        {
            Task MainTask = Task.Run(new Action(() =>
            {
                //監視用スレッドのループ開始
                while (LoopBreak == false)
                {
                    
#if false
										switch (Mode)
                    {
                        case MonitoringMode.Main:
                            //ループ間隔（ﾐﾘ秒）
                            SleepLoop(20);
                            MonitorEwh.WaitOne();
                            //状態監視処理実行
                            McIO.Read();
                            McSta.Read();
							if( null == Items.McStatus ) {
								Items.McStatus = new Models.McIf.StructureMcDatStatus();
							}
							if( ( null != McSta.Status ) && ( null != Items.McStatus ) ) {
								Items.McStatus.Copy( McSta.Status );
							}
							if( null == _alarmCheck ) {
								_alarmCheck = new McDatStatusAlarm();
							}
							_alarmCheck.Execute( McSta.Status );

							McPcond.Read();
                            ProcModeMonitor();
                            Items.MachineAxisPos = McSta.Status.CoordinateAsPosReg;
                            WAxisUpperLimitMonitor();
                            StartBtOffEdgeMonitor();
                            SpindleOnMonitor();
                            DischargeOnMonitor();
                            PompOnMonitor();
                            ContactSensingOnMonitor();
                            InitialSetOnMonitor();
                            BuzzerOnMonitor();
                            //状態監視処理後イベント発行
                            if (StatusMonitoringEvent != null)
                            {
                                StatusMonitoringEvent(new StatusMonitorEventArgs(Items));
                            }
                            break;

                        case MonitoringMode.IOChk:
                            //ループ間隔（ﾐﾘ秒）
                            SleepLoop(500);
                            MonitorEwh.WaitOne();
                            McIO.ReadEx();
                            //状態監視処理実行
                            if (!McIO.EqualToPrevious)
                            {
                                //状態監視処理後イベント発行
                                if (IOMonitoringEvent != null)
                                {
                                    IOMonitoringEvent(new IOMonitorEventArgs(McIO.Items.Clone() as Models.StructureIODataList));
                                }
                            }



                            break;
                    }
#else
					//ループ間隔（ﾐﾘ秒）
					SleepLoop(1);

                    //MonitorEwh.WaitOne();
                    //状態監視処理実行
                    if (MonitoringMode.IOChk == Mode)
                    {
                        McIO.ReadEx();
                    }
                    else
                    {
                        McIO.Read();
                    }
                    McSta.Read();
                    if (null == Items.McStatus)
                    {
                        Items.McStatus = new Models.McIf.StructureMcDatStatus();
                    }
                    if ((null != McSta.Status) && (null != Items.McStatus))
                    {
                        Items.McStatus.Copy(McSta.Status);
                    }
                    if (null == _alarmCheck)
                    {
                        _alarmCheck = new McDatStatusAlarm();
                    }
                    _alarmCheck.Execute(McSta.Status);
                    McCode.Read();
                    McRom.Read();
                    McPcond.Read();
                    McAec.Read();
                    McIni.Read();
                    ReturnCmdMonitor();
                    ProcModeMonitor();
                    Items.MacAxisPos = (StructureAxisCoordinate)McSta.Status.CoordinateAsAbsReg.Clone();
                    Items.WorkAxisPos = (StructureAxisCoordinate)McSta.Status.CoordinateAsPosReg.Clone();
                    WAxisUpperLimitMonitor();
                    StartBtOffEdgeMonitor();
                    SpindleOnMonitor();
                    DischargeOnMonitor();
                    PompOnMonitor();
                    ContactSensingOnMonitor();
                    InitialSetOnMonitor();
                    BuzzerOnMonitor();
                    SeqEndMonitor();
                    FGStoppedMonitor();
                    FGEndMonitor();
                    ColletClampMonitor();
                    GuideClampMonitor();
                    FingerArmClampMonitor();
                    FingerArmPosMonitor();
                    ElectNumberMonitor();
                    GuideNumberMonitor();
                    ProcCondNumMonitor();
                    AECByLifeMonitor();
                    PartitionRoundStopMonitor();
                    DryRunMonitor();
                    IncrimentalReferenceAxisMoveMonitor();
                    OptionalStopMonitor();
                    BlockSkipEnMonitor();
                    CorrectAngleEnMonitor();
                    MachineLockEnMonitor();
                    M02EnMonitor();
                    OverrideMonitor();
                    ProgramRowNumMonitor();
                    SingleStepMonitor();
                    CorrectAngleValueMonitor();
                    EnableAxisMonitor();
                    AecEnableMonitor();
                    Sf02EnableMonitor();
                    ThinSettingEnableMonitor();
                    CompletedReturnOriginMonitor();
                    ProcessLogginig();
                    TimerCount();
                    MaintenanceTimerCheck();
                    GuideThroughEnableMonitor();
                    PrcsDistMonitor();
                    ShutdownSygnalMonitor();

                    //状態監視処理後イベント発行
                    if ( StatusMonitoringEvent != null ) {
						StatusMonitoringEvent( new StatusMonitorEventArgs( Items ) );
					}
					if( MonitoringMode.IOChk == Mode ) {
						//	状態監視処理実行
						if( true == McIO.EqualToPrevious ) {
							//状態監視処理後イベント発行
							if( IOMonitoringEvent != null ) {
								IOMonitoringEvent( new IOMonitorEventArgs( McIO.Items.Clone() as Models.StructureIODataList ) );
							}
						}
					}
#endif
				}
            }));
            await MainTask;
        }
        /// <summary>
        /// シャットダウン動作要求ONフラグ
        /// </summary>
        private void ShutdownSygnalMonitor()
        {
            if (
                McSta.Status.RequestShutDown 
                != Items.ShutdownSygnal
                )
                Items.ShutdownSygnal
                = McSta.Status.RequestShutDown;
        }
        /// <summary>
        /// メンテナンスタイマーチェック
        /// </summary>
        private void MaintenanceTimerCheck()
        {
            if(MaintTimer.HasWarning != Items.HasMaint) Items.HasMaint = MaintTimer.HasWarning;
        }
        bool _OneProcTimerResetFlag = false;
        private void TimerCount()
        {
            //主電源ON時間タイマのカウント
            TimerView.MainPowerTimerStart();
            //加工電源ON時間タイマのカウント
            if(Items.ProcessMode == McTaskModes.Manual)
            {
                if (Items.DischargeOn == true)
                {
                    TimerView.ProcessingPowerTimerStart();
                    MaintTimer.DischargeTimerStart();
                    //加工時間タイマスタート
                    TimerView.DischargeTimerStart(McTaskModes.Manual);
                    //一穴加工時間タイマスタート
                    TimerView.OneProcessingTimerStart(McTaskModes.Manual);
                }
                else
                {
                    TimerView.ProcessingPowerTimerStop();
                    MaintTimer.DischargeTimerStop();
                    //加工時間タイマストップ
                    TimerView.DischargeTimerStop(McTaskModes.Manual);
                    //一穴加工時間タイマストップ
                    TimerView.OneProcessingTimerStop(McTaskModes.Manual);
                }
                //プログラム運転時間タイマのカウント
                if ((McSta.Status.Tasks[0].TaskStatus & 0x00000010) == 0x00000010)
                {
                    TimerView.ProgramProcessingTimerStart(McTaskModes.Manual);
                }
                else
                {
                    TimerView.ProgramProcessingTimerStop(McTaskModes.Manual);
                }
            }
            else if(Items.ProcessMode == McTaskModes.Auto)
            {
                //加工時間タイマと一穴加工時間タイマのカウント
                if ((McPcond.Status & 0x0800) == 0x0800)
                {
                    if(_OneProcTimerResetFlag == true)
                    {
                        //一穴加工時間リセット
                        TimerView.OneProcessingTimeReset(McTaskModes.Auto);
                        _OneProcTimerResetFlag = false;
                    }
                    //加工時間タイマスタート
                    TimerView.DischargeTimerStart(McTaskModes.Auto);
                    //一穴加工時間スタート
                    TimerView.OneProcessingTimerStart(McTaskModes.Auto);
                }
                else
                {
                    //加工時間タイマストップ
                    TimerView.DischargeTimerStop(McTaskModes.Auto);
                    //一穴加工時間タイマストップ
                    TimerView.OneProcessingTimerStop(McTaskModes.Auto);
                    _OneProcTimerResetFlag = true;
                }
                //プログラム運転時間タイマのカウント
                if ((McSta.Status.Tasks[0].TaskStatus & 0x00000010) == 0x00000010)
                {
                    TimerView.ProgramProcessingTimerStart(McTaskModes.Auto);
                }
                else
                {
                    TimerView.ProgramProcessingTimerStop(McTaskModes.Auto);
                }
            }            
        }

        internal int StatusMonitoringEventCtChk()
        {
            if (EventHandlarCount != StatusMonitoringEvent.GetInvocationList().Count())
            {
                EventHandlarCount = StatusMonitoringEvent.GetInvocationList().Count();
            }
            return EventHandlarCount;
        }

        private void SleepLoop(int Count)
        {
            for(int i = 0; i < Count && LoopBreak == false; i+=10)
            {
                Thread.Sleep(10);
            }
        }

        internal void StatusEventChk()
        {
            string strList = "";
            Delegate[] DlList = StatusMonitoringEvent.GetInvocationList();

            foreach (Delegate Dl in DlList)
            {
                strList += Dl.Method.Name + "\r\n";
            }            
            MessageBox.Show(strList);
        }
               
        /// <summary>
        /// 返送コマンド監視
        /// </summary>
        internal void ReturnCmdMonitor()
        {
            if (Items.ReturnCmd == false && McPcond.RequestSendingBack == true)
            {
                Items.ReturnCmd = true;
            }
            else if (Items.ReturnCmd == true && McPcond.RequestSendingBack == false)
            {
                Items.ReturnCmd = false;
            }
        }

        /// <summary>
        /// スタートボタンのオフエッジ検出
        /// </summary>
        internal void StartBtOffEdgeMonitor()
        {
            Items.StartSwBtnOffEdge = false;
            switch (McIO.StartButton == true)
            {
                case true:
                    if (Items.StartSwBtnOnEdge == false)
                    {
                        Items.StartSwBtnOnEdge = true;
                    }
                    break;

                case false:
                    if (Items.StartSwBtnOnEdge == true)
                    {
                        Items.StartSwBtnOffEdge = true;
                        Items.StartSwBtnOnEdge = false;
                    }
                    break;

            }
            if(Items.StartSwBtnOffEdge == true)
            {
                UILog StartBtOffLog = new UILog("ApiMonitorTask.StartBtOffEdgeMonitor");
                StartBtOffLog.Sure("START_OFFEDGE=" + Items.StartSwBtnOffEdge.ToString());
            }            
        }

        /// <summary>
        /// 主軸回転ON検出
        /// </summary>
        internal void SpindleOnMonitor()
        {
            if (Items.SpindleOn == false 
                && (McPcond.SpinOut == SpinStates.Clockwise || McPcond.SpinOut == SpinStates.Counterclockwise))
            {
                Items.SpindleOn = true;
            }
            else if(Items.SpindleOn == true 
                && (McPcond.SpinOut == SpinStates.Stop || McPcond.SpinOut == SpinStates.Unknown))
            {
                Items.SpindleOn = false;
            }
        }
        /// <summary>
        /// 放電ON検出
        /// </summary>
        internal void DischargeOnMonitor()
        {
            if (Items.DischargeOn == false && McPcond.Discharge == true)
            {
                TimerView.OneProcessingTimeReset(Items.ProcessMode);
                Items.DischargeOn = true;
            }
            else if (Items.DischargeOn == true && McPcond.Discharge == false)
            {
                Items.DischargeOn = false;
            }
        }
        /// <summary>
        /// ポンプON検出
        /// </summary>
        internal void PompOnMonitor()
        {
            if (Items.PumpOn == false && McPcond.PumpOut == true)
            {
                Items.PumpOn = true;
            }
            else if (Items.PumpOn == true && McPcond.PumpOut == false)
            {
                Items.PumpOn = false;
            }
        }
        /// <summary>
        /// 接触感知ON検出
        /// </summary>
        internal void ContactSensingOnMonitor()
        {
            if(Items.ContactSensing == false && McSta.Status.TouchSensor == true)
            {
                Items.ContactSensing = true;
            }
            else if (Items.ContactSensing == true && McSta.Status.TouchSensor == false)
            {
                Items.ContactSensing = false;
            }
        }
        /// <summary>
        /// イニシャルセットON検出
        /// </summary>
        internal void InitialSetOnMonitor()
        {
            if (Items.InitialSet == false && McPcond.InitialSet == true)
            {
                Items.InitialSet = true;
            }
            else if (Items.InitialSet == true && McPcond.InitialSet == false)
            {
                Items.InitialSet = false;
            }
        }
        /// <summary>
        /// ブザーON検出
        /// </summary>
        internal void BuzzerOnMonitor()
        {
            if (Items.Buzzer == false && McIO.Buzzer == true)
            {
                
                Items.Buzzer = true;
            }
            else if (Items.Buzzer == true && McIO.Buzzer == false)
            {
                Items.Buzzer = false;
            }
        }
        /// <summary>
        /// シーケンス完了検出
        /// </summary>
        internal void SeqEndMonitor()
        {
            if (Items.SequenceEnd == false && McSta.Status.CompletedSequence == true)
            {
                Items.SequenceEnd = true;
            }
            else if (Items.SequenceEnd == true && McSta.Status.CompletedSequence == false)
            {
                Items.SequenceEnd = false;
            }
        }
        /// <summary>
        /// FG完了検出
        /// </summary>
        internal void FGEndMonitor()
        {
            if (Items.FGEnd == false && McSta.Status.CompletedFg == true)
            {
                Items.FGEnd = true;
            }
            else if (Items.FGEnd == true && McSta.Status.CompletedFg == false)
            {
                Items.FGEnd = false;
            }
        }
        /// <summary>
        /// FG停止中検出
        /// </summary>
        internal void FGStoppedMonitor()
        {
            if(Items.FGStopped != McSta.Status.StoppedFg) Items.FGStopped = McSta.Status.StoppedFg;
        }
        /// <summary>
        /// FG中検出
        /// </summary>
        internal void FGRunningMonitor()
        {
            if (McSta.Status.RunningFg != Items.FGRunning)
            {
                Items.FGRunning = McSta.Status.RunningFg;
            }
        }
        /// <summary>
        /// 動作モード検出
        /// </summary>
        internal void ProcModeMonitor()
        {
            if(Items.ProcessMode != McSta.Status.MotionMode)
            {
                Items.ProcessMode = McSta.Status.MotionMode;
            }
        }
        /// <summary>
        /// オプショナルストップ設定検出
        /// </summary>
        internal void OptionalStopMonitor()
        {
            if (Items.OptionalStop != McSta.Status.OptionalStop)
            {
                Items.OptionalStop = McSta.Status.OptionalStop;
            }
        }
        /// <summary>
        /// ガイド貫通動作許可検出
        /// </summary>
        internal void GuideThroughEnableMonitor()
        {
            if (Items.GuideThroughEnable != McPcond.RequestPermitGuideThrough)
            {
                Items.GuideThroughEnable = McPcond.RequestPermitGuideThrough;
            }
        }
        /// <summary>
        /// 相対測定点設定時軸移動設定検出
        /// </summary>
        internal void IncrimentalReferenceAxisMoveMonitor()
        {
            if (Items.IncrimentalReferenceAxisMove != McSta.Status.IncrimentalReferenceAxisMove)
            {
                Items.IncrimentalReferenceAxisMove = McSta.Status.IncrimentalReferenceAxisMove;
            }
        }
        /// <summary>
        /// W軸上限値設定検出
        /// </summary>
        internal void WAxisUpperLimitMonitor()
        {
            if (Items.WAxisUpperLimit != McSta.Status.WAxisUpperLimit)
            {
                Items.WAxisUpperLimit = McSta.Status.WAxisUpperLimit;
            }
        }
        /// <summary>
        /// ドライラン設定検出
        /// </summary>
        internal void DryRunMonitor()
        {
            if (Items.DryRun != McPcond.DryRun)
            {
                Items.DryRun = McPcond.DryRun;
            }
        }
        /// <summary>
        /// AEC1週停止設定検出
        /// </summary>
        internal void PartitionRoundStopMonitor()
        {
            if (Items.PartitionRoundStop != McPcond.PartitionRoundStop)
            {
                Items.PartitionRoundStop = McPcond.PartitionRoundStop;
            }
        }
        /// <summary>
        /// 電極交換設定検出
        /// </summary>
        internal void AECByLifeMonitor()
        {
            if (Items.AecByLife != McPcond.AecByLife)
            {
                Items.AecByLife = McPcond.AecByLife;
            }
        }
        /// <summary>
        /// 角度補正設定検出
        /// </summary>
        internal void CorrectAngleEnMonitor()
        {
            if (Items.CorrectAngleEn != McSta.Status.CorrectAngle)
            {
                Items.CorrectAngleEn = McSta.Status.CorrectAngle;
            }
        }
        /// <summary>
        /// ブロックスキップ設定検出
        /// </summary>
        internal void BlockSkipEnMonitor()
        {
            if (Items.BlockSkipEn != McSta.Status.BlockSkip)
            {
                Items.BlockSkipEn = McSta.Status.BlockSkip;
            }
        }
        /// <summary>
        /// マシンロック設定検出
        /// </summary>
        internal void MachineLockEnMonitor()
        {
            if (Items.MachineLockEn != McSta.Status.MachineLock)
            {
                Items.MachineLockEn = McSta.Status.MachineLock;
            }
        }
        /// <summary>
        /// M02によるプログラム終了設定検出
        /// </summary>
        internal void M02EnMonitor()
        {
            if (Items.M02Dis != McCode.M02Dis)
            {
                Items.M02Dis = McSta.Status.M02En;
            }
        }
        /// <summary>
        /// コレットクランプ状態検出
        /// </summary>
        internal void ColletClampMonitor()
        {
            if(Items.ColletClampOn != McIO.ColletClamp)
            {
                Items.ColletClampOn = McIO.ColletClamp;
            }
        }
        /// <summary>
        /// ガイドクランプ状態検出
        /// </summary>
        internal void GuideClampMonitor()
        {
            if(Items.GuideHolderClampOn != McIO.GuideClamp)
            {
                Items.GuideHolderClampOn = McIO.GuideClamp;
            }
        }
        /// <summary>
        /// フィンガーアーム状態検出
        /// </summary>
        internal void FingerArmClampMonitor()
        {
            if(Items.FingerArmClampOn != McIO.ColletFingerClamp)
            {
                Items.FingerArmClampOn = McIO.ColletFingerClamp;
            }
        }
        /// <summary>
        /// フィンガーアーム状態検出
        /// </summary>
        internal void FingerArmPosMonitor()
        {
            if(Items.FingerArmPos != McIO.EsfArmPosition)
            {
                Items.FingerArmPos = McIO.EsfArmPosition;
            }        
        }
        /// <summary>
        /// 電極番号検出
        /// </summary>
        internal void ElectNumberMonitor()
        {
            if(Items.ElectrodeNumber != McAec.ElectrodeNumber)
            {
                Items.ElectrodeNumber = McAec.ElectrodeNumber;
            }
        }
        /// <summary>
        /// ガイド番号検出
        /// </summary>
        internal void GuideNumberMonitor()
        {
            if(Items.GuideNumber != McAec.GuideNumber)
            {
                Items.GuideNumber = McAec.GuideNumber;
            }
        }
        /// <summary>
        /// 加工条件番号
        /// </summary>
        internal void ProcCondNumMonitor()
        {
            if (Items.ProcCondNum != McPcond.PNo)
            {
                Items.ProcCondNum = McPcond.PNo;
            }
        }

        /// <summary>
        /// オーバーライド値
        /// </summary>
        internal void OverrideMonitor()
        {
            if (Items.OverRide != McSta.Status.OverrideAsOverall)
            {
                Items.OverRide = McSta.Status.OverrideAsOverall;
            }
        }

        /// <summary>
        /// プログラム実行行取得
        /// </summary>
        internal void ProgramRowNumMonitor()
        {
            //動作確認で使用。
            //Items.ProgRowNum = 5;
            if (Items.ProgRowNum != McSta.Status.LineNo)
            {
                //リリースする際は下の記述のコメントアウトを解除する。
                Items.ProgRowNum = McSta.Status.LineNo;
            }
        }
        /// <summary>
        /// シングルステップモード値
        /// </summary>
        internal void SingleStepMonitor()
        {
            if(Items.SingleStep != McSta.Status.SingleStepMode)
            {
                Items.SingleStep = McSta.Status.SingleStepMode;            
            }
        }
        internal void CorrectAngleValueMonitor()
        {
            if(Items.CorrectAngleValue != McSta.Status.CorrectAngleValue)
            {
                Items.CorrectAngleValue = McSta.Status.CorrectAngleValue;
            }
        }

        internal void EnableAxisMonitor()
        {
            //	有効軸設定
            Items.AxisAEn = (true == McRom.EnableAxisA) ? true : false;
            Items.AxisBEn = (true == McRom.EnableAxisB) ? true : false;
            Items.AxisCEn = (true == McRom.EnableAxisC) ? true : false;
        }
        internal void AecEnableMonitor()
        {
            if(Items.EsfEn != McIni.EnableEsf)
            {
                Items.EsfEn = McIni.EnableEsf;
            }
            if(Items.GsfEn != McIni.EnableGsf)
            {
                Items.GsfEn = McIni.EnableGsf;
            }            
        }
        internal void Sf02EnableMonitor()
        {
            if(Items.Sf02En != McIni.EnableSF02)
            {
                Items.Sf02En = McIni.EnableSF02;
            }
        }
        internal void ThinSettingEnableMonitor()
        {
            if(Items.ThinEn != McAec.Partitions.Items[0].Thinline)
            {
                Items.ThinEn = McAec.Partitions.Items[0].Thinline;
            }
        }
        internal void CompletedReturnOriginMonitor()
        {
            if(McSta.Status.CompletedReturnToOrigin(AxisNumbers.X)
                && McSta.Status.CompletedReturnToOrigin(AxisNumbers.Y)
                && McSta.Status.CompletedReturnToOrigin(AxisNumbers.W)
                && McSta.Status.CompletedReturnToOrigin(AxisNumbers.Z)
                && McSta.Status.CompletedReturnToOrigin(AxisNumbers.A)
                && McSta.Status.CompletedReturnToOrigin(AxisNumbers.B))
            {
                if(Items.CompletedReturnOriginEn != true)
                Items.CompletedReturnOriginEn = true;
            }
            else
            {
                if (Items.CompletedReturnOriginEn != false)
                {
                    Items.CompletedReturnOriginEn = false;
                }
            }
        }
        internal void GSFHolderArmPosMonitor()
        {
            if(Items.GsfArmBack != McIO.GsfArmBack )
            {
                Items.GsfArmBack = McIO.GsfArmBack;
            }
            if(Items.GsfArmFoward != McIO.GsfArmFoward)
            {
                Items.GsfArmFoward = McIO.GsfArmFoward;
            }            
        }
        /// <summary>
        /// 加工深度モニター
        /// </summary>
        internal void PrcsDistMonitor()
        {
            if(Items.PrcsTargetDist != McSta.Status.Ecnc.PrcsTargetDist)
            {
                Items.PrcsTargetDist = McSta.Status.Ecnc.PrcsTargetDist;
            }
            if (Items.PrcsNowDist != McSta.Status.Ecnc.PrcsNowDist)
            {
                Items.PrcsNowDist = McSta.Status.Ecnc.PrcsNowDist;
            }
        }
        public string ReadFileName = "";
        private void ProcessLogginig()
        {
            if (Items.ProcessMode == McTaskModes.Auto)
            {
                //加工時間タイマと一穴加工時間タイマのカウント
                if (((McPcond.Status & 0x0800) == 0x0800) != procLoggingNow)
                {
                    if (procLoggingNow == false)
                    {
                        procLoggingNow = true;
                        procLog.StartLog(Path.GetFileName(ReadFileName));
                    }
                    else
                    {
                        procLoggingNow = false;
                        procLog.EndLog();
                    }
                }
            }
        }

        /// <summary>
        /// メモリ解放処理
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _disposed = false;
        protected void Dispose(bool disposing)
        {
            try
            {
                if(false == _disposed)
                {
                    if(true == disposing)
                    {
						if( null != _alarmCheck ) {
							_alarmCheck.Dispose();
							_alarmCheck = null;
						}
					}
                }
                _disposed = true;
            }
            finally
            {
               
            }
        } 
    }
}
