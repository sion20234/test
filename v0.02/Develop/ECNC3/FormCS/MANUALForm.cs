﻿///////////////////////////////////////////////////////////////////////////////////////////
//
// (1) システム名称 : ELENIX ECNCⅢ
// (2) ファイル名   : MANUALForm.cs
// (3) 概要         : 手動画面
// (4) 作成日       : 2015.06.01
// (5) 作成者       : 八野
// (6) 特記事項     : VisualStudio2015 C#
// (7) 更新履歴     :
//
///////////////////////////////////////////////////////////////////////////////////////////

using ECNC3.Enumeration;
using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ECNC3.Models.McIf;
using ECNC3.Views.Popup;
using ECNC3.Models;
using System.Runtime.InteropServices;	//DLL Import

namespace ECNC3.Views
{
    public partial class MANUALForm : ECNC3Form
    {
		#region Constructor
		/// <summary>ポップアップテンキー</summary>
		private TenKeyDialog _popupTenkey = null;//初回インスタンスを作っておく
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public MANUALForm()
        {
            ApiMonitorTask.monitorMx.WaitOne();
            //コントロールの初期化
            InitializeComponent();
            ApiMonitorTask.monitorMx.ReleaseMutex();
            //メモリ解放処理の追加
            Disposed += MANUALForm_Disposed;
        }
        #endregion
        #region VariableMember
        /// <summary>
        /// 機能画面
        /// </summary>
        internal UserFuncForm FuncForm;
        /// <summary>
        /// 加工条件画面
        /// </summary>
        private ConditionsForm CondForm;
        /// <summary>
        /// 原点復帰画面
        /// </summary>
        internal ReturnToOriginForm RTOForm;
        /// <summary>
        /// 電極交換画面
        /// </summary>
        internal AECForm AEC;
        ///// <summary>
        ///// 位置取り画面
        ///// </summary>
        internal ReferencingForm RefForm;
        /// <summary>
        /// 数量位置決め画面
        /// </summary>
        internal NumericFeedForm NumForm;
        ///// <summary>
        ///// 加工ログ表示画面
        ///// </summary>
        //LogForm Log;

        /// <summary>
        /// 装置状態監視用イベント
        /// </summary>
        internal event StatusMonitoringEventHandler StatusMonitoringEvent;
        /// <summary>
        /// IO状態監視用イベント
        /// </summary>
        internal event IOMonitoringEventHandler IOStatusMonitoringEvent;
        /// <summary>
        /// W軸上限値用変数
        /// </summary>
        int WAxisUpperSet { get; set; }

        //スタートボタンオフエッジフラグ
        internal bool StartBtOffEdge = false;
        /// <summary>
        /// 加工時間タイマーリセットフラグ
        /// </summary>
        internal bool ResetDischargeTimer { get; set; }
        /// <summary>
        /// 現在の有効桁数
        /// </summary>
        private DigitSelect _digit = DigitSelect.Four;
        /// <summary>
        /// イベント処理数
        /// </summary>
        public int EventHandlarCount { get { return StatusMonitoringEvent.GetInvocationList().Count(); } private set { } }
        /// <summary>
        /// ファイル画面
        /// </summary>
        FileForm FileForm = null;//追加：柏原
        #endregion
        #region Monitoring
        public void StatusMonitoring(StatusMonitorEventArgs e)
        {
            if (RTOForm != null)
            {
                if (RTOForm._FormCloseSygnal == true)
                {
                    if ((this.IsDisposed == true || this.Disposing == true)) return;
                    retInvoke = BeginInvoke((MethodInvoker)delegate
                    {
                         RTOForm.Close();
                    }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                }
            }
            if (NumForm != null)
            {
                if(NumForm.axisMode == NumericFeedForm.AxisMode.NULL)
                {
                    axisMonitor2.AxisMonitorSelectClear(false, false);
                } 
                else
                {
                    if (NumForm._functionMode == NumericFeedForm.FunctionMode.NumericFeed)
                    {
                        if (axisMonitor2.WorkSettingEn != false) axisMonitor2.WorkSettingEn = false;
                        axisMonitor2.AxisMonitorSelectChange((int)(NumForm._coordinateMode), ((int)(NumForm.axisMode)) - 1);
                    }
                    else
                    {
                        if (axisMonitor2.WorkSettingEn != true) axisMonitor2.WorkSettingEn = true;
                        axisMonitor2.AxisMonitorSelectChange(1, ((int)NumForm.axisMode) - 1);
                    }
                }                
            }
            if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
            {
                if (NumForm == null)
                {
                    if(axisMonitor2.HeaderPainted != false)axisMonitor2.HeaderPainted = false;
                    if (axisMonitor2.GetCellClick())
                    {
                        _axisCommandSelect(axisMonitor2.GetSelectCoordinate());                       
                    }
                    CoordinateHeaderSelect();
                }
                else
                {
                    if (NumForm._functionMode == NumericFeedForm.FunctionMode.NumericFeed)
                    {
                        CoordinateHeaderSelect();
                        if (axisMonitor2.HeaderPainted != false) axisMonitor2.HeaderPainted = false;
                    }
                    else
                    {
                        if (axisMonitor2.HeaderPainted != true) axisMonitor2.HeaderPainted = true;
                        if (axisMonitor2.GetColumnHeaderOnClick() == true)
                        {
                            NumForm.CloseEx();
                        }
                    }
                }                 
            }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
            if (StatusMonitoringEvent != null)
            {
                StatusMonitoringEvent(e);
            }
            LogForm_UpdateStatus(e);
            //OVERRIDE表示処理
            if (editProcessCondition1.GetOverride() != e.Items.OverRide)
            {
                if( (this.IsDisposed == true || this.Disposing == true)   ) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                {
                    editProcessCondition1.SetOverride(e.Items.OverRide);
                }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
            }

            //原点復帰済み確認
            if (ReturnOriginBt.GetLed() != e.Items.CompletedReturnOriginEn)
            {
                if( (this.IsDisposed == true || this.Disposing == true)   ) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                {
                    ReturnOriginBt.SetLed(e.Items.CompletedReturnOriginEn);
                }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
            }

            //加工条件番号変更時表示処理
            if (int.Parse(PnumTextBox.Text) != e.Items.ProcCondNum)
            {
                if( (this.IsDisposed == true || this.Disposing == true)   ) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                {
                    RefreshProcessCondition();
                }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
            }

            //W軸上限値変更時表示処理
            if( (this.IsDisposed == true || this.Disposing == true)   ) return; retInvoke = BeginInvoke((MethodInvoker)delegate
            {
                if (WAxisUpperVal.Text != PositionToString(e.Items.WAxisUpperLimit))
                {
                    WAxisUpperVal.Text = PositionToString(e.Items.WAxisUpperLimit);
                }
            }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
            //電極番号設定済み確認処理
            if (true == InvokeRequired)
            {
                if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                {
                    //番号設定が未設定の場合、設定ボタンのLED色を変更する
                    if (e.Items.ElectrodeNumber != 0)
                    {
                        //電極番号設定ボタンの背景色を白くする。
                        if(AecBt.GetLed() == false) AecBt.SetLed(true);
                    }
                    else
                    {
                        if (AecBt.GetLed() == true) AecBt.SetLed(false);
                    }
                }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
            }

            //ESF有無表示処理
            if ( (this.IsDisposed == true || this.Disposing == true)   ) return; retInvoke = BeginInvoke((MethodInvoker)delegate
            {
                if(e.Items.EsfEn == true)
                {
                    if(AecBt.Text != "電極交換")
                    {
                        AecBt.Text = "電極交換";
                    }
                }
                else
                {
                    if(AecBt.Text != "電極セット")
                    {
                        AecBt.Text = "電極セット";
                    }
                }
            }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
            //手動画面の子画面がいるときはスタートボタンを効かないようにする。
            if (RefForm == null
                && RTOForm == null
                && NumForm == null
                && AEC == null
                && FuncForm == null
                && _popupTenkey == null
                
                &&
                (
                (editProcessCondition1 != null) ? 
                ((editProcessCondition1._popupTenkey != null) ? 
                ((editProcessCondition1._popupTenkey.Visible == false)?
                true : false)
                : true) 
                : false
                )
                )
            {
                if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                {
                    if (SpinBt.Enabled != true) SpinBt.Enabled = true;
                    if (PompBt.Enabled != true) PompBt.Enabled = true;
                    if (DischargeBt.Enabled != true) DischargeBt.Enabled = true;
                }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                if (StartBtOffEdge == false && e.Items.StartSwBtnOffEdge == true)
                {
                    StartBtOffEdge = true;
                }
                else if (StartBtOffEdge == true && e.Items.StartSwBtnOffEdge == false)
                {
                    StartBtOffEdge = false;
                }
            }
            else
            {
                if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                {
                    if(RefForm != null)
                    {
                        if (SpinBt.Enabled != true) SpinBt.Enabled = true;
                        if (PompBt.Enabled != true) PompBt.Enabled = true;
                    }
                    else
                    {
                        if (SpinBt.Enabled != false) SpinBt.Enabled = false;
                        if (PompBt.Enabled != false) PompBt.Enabled = false;
                    }
                    if (DischargeBt.Enabled != false) DischargeBt.Enabled = false;
                }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                if (StartBtOffEdge == true) StartBtOffEdge = false;
            }
            if (StartBtOffEdge == true)
            {
                try
                {
                    if (PompBt.GetBack() == true)
                    {
                        ManualFormSequences(!PompBt.GetLed(), ManualModeProcCommands.Pomp);
                    }
                    if (SpinBt.GetBack() == true)
                    {
                        ManualFormSequences(!SpinBt.GetLed(), ManualModeProcCommands.Spin);
                    }
                }
                finally
                {
                    if (DischargeBt.GetBack() == true)
                    {
                        ManualFormSequences(!DischargeBt.GetLed(), ManualModeProcCommands.Discharge);
                    }
                }
            }
            //返送要求コマンド
            if (e.Items.ReturnCmd == true
                && RefForm == null)
            {
                string Value = "";
                using (SettingFunction SetFunc = new SettingFunction())
                {
                    Value = SetFunc.SettingMonitoring(Settings.ReturnCmd).ToString();
                }
                if (Value != ResultCodes.Success.ToString())
                {
                    UILog logs = new UILog("MANUALForm.SeqFunc.SequenceMonitoring");
                    logs.Error("RETURN_CMD Result = " + Value);
                }
            }
            //放電ON表示処理
            if (e.Items.DischargeOn != DischargeBt.GetLed())
            {
                DischargeBt.SetLed(e.Items.DischargeOn);
                //ログ処理
                UILog logs = new UILog("MANUALForm.SeqFunc.SequenceMonitoring");
                logs.Error("CHG_DISCHARGE_ON " + (e.Items.DischargeOn).ToString());
            }
            //ポンプ表示処理
            if (e.Items.PumpOn != PompBt.GetLed())
            {
                PompBt.SetLed(e.Items.PumpOn);
                //ログ処理
                UILog logs = new UILog("MANUALForm.SeqFunc.SequenceMonitoring");
                logs.Error("CHG_POMP_ON " + (e.Items.PumpOn).ToString());
            }
            //主軸回転表示処理
            if (e.Items.SpindleOn != SpinBt.GetLed())
            {
                SpinBt.SetLed(e.Items.SpindleOn);
                //ログ処理
                UILog logs = new UILog("MANUALForm.SeqFunc.SequenceMonitoring");
                logs.Error("CHG_SPINDLE_ON " + (e.Items.SpindleOn).ToString());
            }
            //接触感知表示処理
            if (e.Items.ContactSensing == true)
            {
                if (ContactSensingBt.GetLed() == false)
                {
                    ContactSensingBt.SetLed(true);
                    //ログ処理
                    UILog logs = new UILog("MANUALForm.SeqFunc.SequenceMonitoring");
                    logs.Error("CHG_CONTACTSENSER_ON");
                }
            }
            else
            {
                if (ContactSensingBt.GetLed() == true)
                {
                    ContactSensingBt.SetLed(false);
                    //ログ処理
                    UILog logs = new UILog("MANUALForm.SeqFunc.SequenceMonitoring");
                    logs.Error("CHG_CONTACTSENSER_OFF");
                }
            }

            //イニシャルセットON表示処理
            if (e.Items.InitialSet == true)
            {
                if (InitialSetBt.GetLed() == false)
                {
                    InitialSetBt.SetLed(true);
                    UILog logs = new UILog("MANUALForm.SeqFunc.SequenceMonitoring");
                    logs.Error("CHG_INITIALSET_ON");
                }
            }
            else
            {
                if (InitialSetBt.GetLed() == true)
                {
                    InitialSetBt.SetLed(false);
                    UILog logs = new UILog("MANUALForm.SeqFunc.SequenceMonitoring");
                    logs.Error("CHG_INITIALSET_OFF");
                }
            }
            ////////////////////状態取得構造体に定義されるまで不使用////////////////////////
            //現時点ではIOを直接参照していた。
            //ブザーON表示処理
            if (e.Items.Buzzer == true)
            {
                if (BuzzerBt.GetLed() == false)
                {
                    BuzzerBt.SetLed(true);
                    UILog logs = new UILog("MANUALForm.SeqFunc.SequenceMonitoring");
                    logs.Error("CHG_BUZZER_ON");
                }
            }
            else
            {
                if (BuzzerBt.GetLed() == true)
                {
                    BuzzerBt.SetLed(false);
                    UILog logs = new UILog("MANUALForm.SeqFunc.SequenceMonitoring");
                    logs.Error("CHG_BUZZER_OFF");
                }
            }
            ////////////////////////////////////////////////////////////////////////////////
        }
        #endregion
        #region EventHandler
        /// <summary>ロードイベント</summary>
        /// <param name="sender">イベント送信元オブジェクト</param>
        /// <param name="e">イベントパラメータ</param>
        private void MANUALForm_Load(object sender, EventArgs e)
        {
#if __V001_INHIBIT__
#else
			button10.Enabled = false;
#endif
            this.OutLineEnable = false;
            plot1.OutLineEnable = false;
            ResetDischargeTimer = false;

			levelGage1.VCatName = "電圧";
            levelGage1.ACatName = "電流";

            //加工条件編集/表示用コントロールのリフレッシュ処理
            //これがないと加工条件欄の数値の桁が合わなくなる。
            RefreshProcessCondition();

            UILog manualFormInitLog = new UILog(", MANUALForm()");
            //装置状態監視処理に座標取得処理の追加
            StatusMonitoringEvent = axisMonitor2.SetAllMacAxisValue;
            Delegate[] DlList = StatusMonitoringEvent.GetInvocationList();
            string strList = "";
            foreach (Delegate Dl in DlList)
            {
                strList += Dl.Method.Name + "\r\n";
            }
            if (strList == "")
            {
                //装置状態監視処理に関数が無ければログを出力する。
                manualFormInitLog.Error("StatusMonitoringEvent" + " IS NULL" + ", " + "Value= " + strList);
            }
			//Plot.csからプロット
			plot1.plotNotify += PlotNotifyClick; //イベント通知
            //有効桁数の取得
            string[] axisName = { "X", "Y", "W", "Z", "A", "B", "C", "I" };
            using (FileSettings fs = new FileSettings())
            {
                //ファイル読み込み
                fs.Read();
                LoadDigit(fs);
            }
        }
		/// <summary>
		/// フォーム：閉じる
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MANUALForm_FormClosed( object sender, FormClosedEventArgs e )
		{
			//ポップアップテンキー
			if( null != _popupTenkey ) {
				_popupTenkey.Close();
				_popupTenkey = null;
			}
		}
		/// <summary>
		/// Disposed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MANUALForm_Disposed( object sender, EventArgs e )
		{
			if( null != CondForm ) {
				CondForm.Dispose();
				CondForm = null;
			}
		}
		#endregion
		#region<加工ログ表示切替>
		/// <summary>
		/// Plot.csからプロット：クリック
		/// </summary>
		private void PlotNotifyClick()
		{
			//ManualFunctionFormsClose();
		}

        public void LogForm_UpdateStatus(StatusMonitorEventArgs e)
        {
            if ((this.IsDisposed == true || this.Disposing == true)) return;
            retInvoke = BeginInvoke((MethodInvoker)delegate
            {
                plot1.LogStatusView(e, _digit);
            }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
        }

        #endregion

        /// <summary>
        /// 有効軸数の取得
        /// </summary>
        /// <param name="fs"> ファイルデータ</param>
        /// <returns>有効軸数</returns>
        private int LoadAxisCount(FileSettings fs)
        {
            int ret = 0;
            {   //ファイル読み込み
                ret = fs.AttrValue("Root/AxisInfomation/EnableAxis", "count");//0=上下のみ、1=上下+テンキー、2=テンキーのみ
            }
            return ret;
        }
        /// <summary>
        /// 有効桁数の取得
        /// </summary>
        /// <param name="fs">ファイルデータ</param>
        private void LoadDigit(FileSettings fs)
        {
            int tempDigit = 4;
            tempDigit = fs.AttrValue("Root/AxisInfomation/Position", "digit");
            switch (tempDigit)
            {
                case 3: _digit = DigitSelect.Three; break;
                case 4: _digit = DigitSelect.Four; break;
            }
        }
        /// <summary>
        /// 整数の座標値を有効桁数によって変化する文字列に変換する。
        /// </summary>
        /// <param name="position">座標値</param>
        /// <returns>座標値の文字列</returns>
        private string PositionToString(int position)
        {
            switch (_digit)
            {
                case DigitSelect.Three: return position.ToString("###0'.'000");
                case DigitSelect.Four: return (position * 10).ToString("###0'.'0000");
                default: return "0.000";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        internal void ProcessCmdChange()
        {
            try
            {
                if (PompBt.GetLed() != false)
                {
                    PompBt.SetLed(false);
                    ManualFormSequences(false, ManualModeProcCommands.Pomp);
                }

                if (SpinBt.GetLed() != false)
                {
                    SpinBt.SetLed(false);
                    ManualFormSequences(false, ManualModeProcCommands.Spin);
                }
            }
            finally
            {
                if (DischargeBt.GetLed() != false)
                {
                    DischargeBt.SetLed(false);
                    ManualFormSequences(false, ManualModeProcCommands.Discharge);
                }
            }            
        }

        private void MANUALForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                StatusMonitoringEvent = axisMonitor2.SetAllMacAxisValue;
                RefreshProcessCondition();
            }
            else
            {
                StatusMonitoringEvent = null;
                ProcessCmdChange();
				editProcessCondition1.CancelEdit();
            }
        }

        public void IOStatusMonitoring(IOMonitorEventArgs e)
        {
            if (IOStatusMonitoringEvent != null)
            {
                IOStatusMonitoringEvent(e);
            }
        }

        /// <summary>
		/// 機能画面ボタン（イベントハンドラ）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FunctionFormBt_Click(object sender, EventArgs e)
        {
            ManualFunctionFormsHandling(ManualFunctions.Functions);
        }
        private void _btnFunction_ClickCallBack()
        {
            ManualFunctionFormsClosed(ManualFunctions.Functions);
        }

        /// <summary>加工条件　ボタン　クリック時のイベントハンドラ</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConditionsFormOpenBt_Click(object sender, EventArgs e)
        {
            if (null == CondForm)
            {
                CondForm = new ConditionsForm(PnumTextBox.Text);
                CondForm.NotifyReturn = ConditionsFormOpenBt_ClickCallBack;
            }
            //親子関係設定
            CondForm.Show(this);
            UILog logs = new UILog("MANUALForm.ConditionsFormOpenBt_Click");
            logs.Operate("Move_ConditionsForm" + "Pnum = " + PnumTextBox.Text);
            logs.Debug("MOVE_CONDITIONSFORM");
        }
        /// <summary>加工条件ボタン コールバック</summary>
        private void ConditionsFormOpenBt_ClickCallBack()
        {
            CondForm.Close();
            CondForm = null;
            RefreshProcessCondition();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        //↓手動画面メインボタンのイベント処理
        /// <summary>
        /// 手動画面の機能
        /// </summary>
        public enum ManualFunctions
        {
            /// <summary>
            /// ワーク座標設定
            /// </summary>
            SetWorkPosition,
            /// <summary>
            /// 座標登録
            /// </summary>
            RegistPosition,
            /// <summary>
            /// 電極交換
            /// </summary>
            AecControl,
            /// <summary>
            /// 位置出し
            /// </summary>
            Referencing,
            /// <summary>
            /// 位置決め
            /// </summary>
            NumericFeed,
            /// <summary>
            /// 機能
            /// </summary>
            Functions,
            /// <summary>
            /// 全て
            /// </summary>
            All
        }

        /// <summary>
        /// 原点復帰ボタン　クリック時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReturnToOriginBt_Click(object sender, EventArgs e)
        {
            ManualFunctionFormsHandling(ManualFunctions.RegistPosition);
        }
        private void ReturnToOriginForm_Closed(object sender, FormClosedEventArgs e)
        {
            ManualFunctionFormsClosed(ManualFunctions.RegistPosition);
        }
        
        /// <summary>
        /// 電極交換　ボタン　クリック時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AECFormBt_Click(object sender, EventArgs e)
        {
            ManualFunctionFormsHandling(ManualFunctions.AecControl);
        }
        private void AECForm_Closed(object sender, FormClosedEventArgs e)
        {
            ManualFunctionFormsClosed(ManualFunctions.AecControl);
        }

        /// <summary>
        /// 位置取り　ボタン　クリック時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReferensingFormBt_Click(object sender, EventArgs e)
        {
            ManualFunctionFormsHandling(ManualFunctions.Referencing);
        }
        private void ReferensingForm_Closed(object sender, FormClosedEventArgs e)
        {
            ManualFunctionFormsClosed(ManualFunctions.Referencing);
        }
        
        /// <summary>
        /// 数量位置決め　ボタン　クリック時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumericFeedFormBt_Click(object sender, EventArgs e)
        {
            ManualFunctionFormsHandling(ManualFunctions.NumericFeed);
        }
        private void NumericFeedForm_Closed(object sender, FormClosedEventArgs e)
        {
            ManualFunctionFormsClosed(ManualFunctions.NumericFeed);
            NumericFeedBt.SetBack(false);
        }
        private void WorkSettingForm_Closed(object sender, FormClosedEventArgs e)
        {
            ManualFunctionFormsClosed(ManualFunctions.SetWorkPosition);
            axisMonitor2.SetSelectWorkPosition(false);
        }
        /// <summary>
        /// 子フォーム非表示処理
        /// </summary>
        public void ManualFunctionFormsClose()
        {
            //位置出し画面非表示
            if (RefForm != null)
            {
                RefForm.CloseForm();
                ReferencingBt.SetBack(false);
                return;
            }
            //座標登録画面非表示
            if (RTOForm != null)
            {
                RTOForm._FormCloseSygnal = true;
                ReturnOriginBt.SetBack(false);
            }
            //電極交換画面非表示
            if (AEC != null)
            {
                AEC.BackMANUALForm_Click();
                AecBt.SetBack(false);
            }
            //数値位置決め画面非表示
            if (NumForm != null)
            {
                NumForm.CloseEx();
                NumericFeedBt.SetBack(false);
            }
            //機能画面非表示
            if (FuncForm != null)
            {
                FuncForm.ReturnForm();
                FunctionBt.SetBack(false);
            }
            //ログ表示画面非表示
            if(plot1.ExpandEn == true)
            {
                plot1.ExpandEn = false;
            }
            //テンキー非表示
            if(_popupTenkey != null)
            {
                _popupTenkey.Close();
                _popupTenkey = null;
            }
            // 加工条件編集欄のテンキー非表示
            editProcessCondition1.CancelEdit();
            //加工条件画面非表示
            if(CondForm != null)
            {
                CondForm._btnReturn_Click(null, null);
            }
            //ファイルフォーム
            if (FileForm != null)
            {
                FileForm.Close();
                FileForm = null;
            }
        }
        /// <summary>
        /// 子フォームのClosed処理
        /// </summary>
        /// <param name="func"></param>
        public void ManualFunctionFormsClosed(ManualFunctions func)
        {
            switch (func)
            {
                case ManualFunctions.RegistPosition:
                    if (RTOForm != null)
                    {
                        StatusMonitoringEvent -= RTOForm.RTOStatusMonitoring;
                        RTOForm = null;
                    } 
                    ReturnOriginBt.SetSelected(false);
                    break;

                case ManualFunctions.AecControl:
                    if (AEC != null)
                    {
                        StatusMonitoringEvent -= AEC.StatusMonitoring;
                        AEC = null;
                    }
                    AecBt.SetSelected(false);
                    break;

                case ManualFunctions.Referencing:
                    if (RefForm != null)
                    {
                        StatusMonitoringEvent -= RefForm.StatusMonitoring;
                        RefForm = null;
                    } 
                    ReferencingBt.SetSelected(false);
                    break;

                case ManualFunctions.NumericFeed:
                    if (NumForm != null)
                    {
                        StatusMonitoringEvent -= NumForm.RTOStatusMonitoring;
                        NumForm = null;
                    }
                    axisMonitor2.SetColumnHeaderOnClick(false);
                    NumericFeedBt.SetSelected(false);
                    axisMonitor2.AxisMonitorSelectClear();
                    break;

                case ManualFunctions.SetWorkPosition:
                    if (NumForm != null)
                    {
                        StatusMonitoringEvent -= NumForm.RTOStatusMonitoring;
                        NumForm = null;
                    }
                    axisMonitor2.SetColumnHeaderOnClick(false);
                    axisMonitor2.AxisMonitorSelectClear();
                    break;

                case ManualFunctions.Functions:
                    if (null != FuncForm)
                    {
                        IOStatusMonitoringEvent = null;
                        StatusMonitoringEvent -= FuncForm.StatusMonitoring;
                        FuncForm.Close();
                        FuncForm = null;
                        FunctionBt.SetSelected(false);
                    }
                    break;

                case ManualFunctions.All:
                    if (RTOForm != null)
                    {
                        StatusMonitoringEvent -= RTOForm.RTOStatusMonitoring;
                        RTOForm = null;
                    }
                    ReturnOriginBt.SetSelected(false);

                    if (AEC != null)
                    {
                        StatusMonitoringEvent -= AEC.StatusMonitoring;
                        AEC = null;
                    }
                    AecBt.SetSelected(false);

                    if (RefForm != null)
                    {
                        StatusMonitoringEvent -= RefForm.StatusMonitoring;
                        RefForm = null;
                    }
                    ReferencingBt.SetSelected(false);

                    if (NumForm != null)
                    {
                        StatusMonitoringEvent -= NumForm.RTOStatusMonitoring;
                        NumForm = null;
                    }
                    NumericFeedBt.SetSelected(false);

                    if (null != FuncForm)
                    {
                        IOStatusMonitoringEvent = null;
                        StatusMonitoringEvent -= FuncForm.StatusMonitoring;
                        FuncForm.Close();
                        FuncForm = null;
                        FunctionBt.SetSelected(false);
                    }
                    break;

            }
        }
        /// <summary>
        /// 手動画面の各機能のフォームに移動
        /// </summary>
        /// <param name="func"></param>
        private void ManualFunctionFormsHandling(ManualFunctions func)
        {
            UILog logs = new UILog("MANUALForm");
            switch (func)
            {
                case ManualFunctions.RegistPosition:
                    if (RTOForm != null)
                    {
                        RTOForm._FormCloseSygnal = true;
                        ReturnOriginBt.SetSelected(false);
                        return;
                    }
                    else
                    {
                        if (AEC != null)
                        {
                            AEC.BackMANUALForm_Click();
                            AecBt.SetBack(false);
                        }
                        if (RefForm != null)
                        {
                            RefForm.CloseForm();
                            ReferencingBt.SetBack(false);
                        }
                        if (NumForm != null)
                        {
                            NumForm.CloseEx();
                            NumericFeedBt.SetBack(false);
                        }
                        if (FuncForm != null)
                        {
                            FuncForm.ReturnForm();
                            FunctionBt.SetBack(false);
                        }
                        if (plot1.ExpandEn == true)
                        {
                            plot1.ExpandEn = false;
                        }
                        if (_popupTenkey != null)
                        {
                            _popupTenkey.Close();
                            _popupTenkey = null;
                        }
                        ReturnOriginBt.SetBack(true);
                        ReturnOriginBt.SetSelected(true);
                    }

                    RTOForm = new ReturnToOriginForm();
                    StatusMonitoringEvent += RTOForm.RTOStatusMonitoring;
                    RTOForm.FormClosed += ReturnToOriginForm_Closed;
                    RTOForm.SelectFormInit();
                    //親子関係設定
                    RTOForm.Show(this);
                    logs.Operate("Move_ReturnToOriginBt_Click, ");
                    logs.Debug("MOVE_RETURNTOORIGINFORM");
                    break;

                case ManualFunctions.AecControl:
                    //ESFなしの場合、電極番号設定のみ。
                    using (McDatInitialPrm initPrm = new McDatInitialPrm())
                    {
                        initPrm.Read();
                        if (initPrm.EnableEsf == false)
                        {
                            using (Models.McIf.McReqElectrodeNumber SetElectNum = new Models.McIf.McReqElectrodeNumber())
                            {
                                SetElectNum.ElectrodeNumber = 0;
                                SetElectNum.Execute();
                            }
                            using (Models.McIf.McReqGuideNumber SetGuideNum = new Models.McIf.McReqGuideNumber())
                            {
                                SetGuideNum.GuideNumber = 0;
                                SetGuideNum.Execute();
                            }
                            using (Models.McIf.McReqGuideThroughStart ReqGuideThrough = new Models.McIf.McReqGuideThroughStart())
                            {
                                ResultCodes _retResultCodes = ReqGuideThrough.Execute();
                                //ログ処理
                                if (_retResultCodes == ResultCodes.Success)
                                {
                                    logs.Sure("GUIDE_THROUGH Result = " + _retResultCodes);
                                }
                                else
                                {
                                    logs.Error("GUIDE_THROUGH Result = " + _retResultCodes);
                                }
                            }
                            AecBt.SetLed(false);
                    return;
                        }
                    }
                    if (AEC != null)
                    {
                        AEC.BackMANUALForm_Click();
                        AecBt.SetBack(false);
                        return;
                    }
                    else
                    {
                        if (RTOForm != null)
                        {
                            RTOForm._FormCloseSygnal = true;
                            ReturnOriginBt.SetBack(false);
                        }
                        if (RefForm != null)
                        {
                            RefForm.CloseForm();
                            ReferencingBt.SetBack(false);
                        }
                        if (NumForm != null)
                        {
                            NumForm.CloseEx();
                            NumericFeedBt.SetBack(false);
                        }
                        if (FuncForm != null)
                        {
                            FuncForm.ReturnForm();
                            FunctionBt.SetBack(false);
                        }
                        if (plot1.ExpandEn == true)
                        {
                            plot1.ExpandEn = false;
                        }
                        if (_popupTenkey != null)
                        {
                            _popupTenkey.Close();
                            _popupTenkey = null;
                        }
                        AecBt.SetBack(true);
                        AecBt.SetSelected(true);
                    }
                    AEC = new AECForm();
                    StatusMonitoringEvent += AEC.StatusMonitoring;
                    AEC.FormClosed += AECForm_Closed;
                    AEC.SelectFormInit();
                    //親子関係設定
                    AEC.Show(this);
                    logs.Operate("Move_AECFormBt_Click");
                    logs.Debug("MOVE_AECFORM");
                    break;

                case ManualFunctions.Referencing:
                    if (RefForm != null)
                    {
                        RefForm.CloseForm();
                        ReferencingBt.SetBack(false);
                        return;
                    }
                    else
                    {
                        if (RTOForm != null)
                        {
                            RTOForm._FormCloseSygnal = true;
                            ReturnOriginBt.SetBack(false);
                        }
                        if (AEC != null)
                        {
                            AEC.BackMANUALForm_Click();
                            AecBt.SetBack(false);
                        }
                        if (NumForm != null)
                        {
                            NumForm.CloseEx();
                            NumericFeedBt.SetBack(false);
                        }
                        if (FuncForm != null)
                        {
                            FuncForm.ReturnForm();
                            FunctionBt.SetBack(false);
                        }
                        if (plot1.ExpandEn == true)
                        {
                            plot1.ExpandEn = false;
                        }
                        if (_popupTenkey != null)
                        {
                            _popupTenkey.Close();
                            _popupTenkey = null;
                        }
                        ReferencingBt.SetBack(true);
                        ReferencingBt.SetSelected(true);
                    }

                    RefForm = new ReferencingForm();
                    StatusMonitoringEvent += RefForm.StatusMonitoring;
                    RefForm.FormClosed += ReferensingForm_Closed;
                    RefForm.SelectFormInit();
                    logs.Operate("Move_ReferensingFormBt_Click");
                    //親子関係設定
                    RefForm.Show(this);
                    break;

                case ManualFunctions.NumericFeed:
                    OpenNumericFeedForm();
                    logs.Operate("Move_NumericFeedFormBt_Click");
                    break;

                case ManualFunctions.SetWorkPosition:
                    OpenWorkSettingForm();
                    logs.Operate("Move_NumericFeedFormBt_Click");
                    break;

                case ManualFunctions.Functions:
                    if (FuncForm != null)
                    {
                        FuncForm.ReturnForm();
                        FunctionBt.SetBack(false);
                        return;
                    }
                    else
                    {
                        if (AEC != null)
                        {
                            AEC.BackMANUALForm_Click();
                            AecBt.SetBack(false);
                        }
                        if (RefForm != null)
                        {
                            RefForm.CloseForm();
                            ReferencingBt.SetBack(false);
                        }
                        if (NumForm != null)
                        {
                            NumForm.CloseEx();
                            NumericFeedBt.SetBack(false);
                        }
                        if (RTOForm != null)
                        {
                            RTOForm.Close();
                            ReturnOriginBt.SetBack(false);
                        }
                        if (plot1.ExpandEn == true)
                        {
                            plot1.ExpandEn = false;
                        }
                        if (_popupTenkey != null)
                        {
                            _popupTenkey.Close();
                            _popupTenkey = null;
                        }
                        FunctionBt.SetBack(true);
                        FunctionBt.SetSelected(true);
                    }
                    ProcessCmdChange();
                    FuncForm = new UserFuncForm();
                    FuncForm.NotifyReturn = _btnFunction_ClickCallBack;
                    StatusMonitoringEvent += FuncForm.StatusMonitoring;
                    FuncForm.SelectFormInit();
                    IOStatusMonitoringEvent = FuncForm.IOStatusMonitoring;
                    //親子関係設定
                    FuncForm.Show(this);
                    break;

            }        
        }
        /// <summary>
        /// NumericFeedForm専用Open関数
        /// </summary>
        /// <param name="funcMode"></param>
        /// <param name="axis"></param>
        /// <param name="Pos"></param>
        /// <param name="coordinate"></param>
        private void OpenNumericFeedForm( 
                NumericFeedForm.AxisMode axis               = NumericFeedForm.AxisMode.NULL,
                NumericFeedForm.PositionMode Pos            = NumericFeedForm.PositionMode.Incrimental,
                NumericFeedForm.CoordinateMode coordinate   = NumericFeedForm.CoordinateMode.Work)
        {
            if (NumForm != null )
            {
                if (NumForm._functionMode == NumericFeedForm.FunctionMode.NumericFeed)
                {
                    NumForm.CloseEx();
                    return;
                }
                else
                {
                    NumForm.FormClosed -= WorkSettingForm_Closed;
                    NumForm.FormClosed += NumericFeedForm_Closed;
                    axisMonitor2.SetSelectWorkPosition(false);
                }
            }
            else
            {
                if (RTOForm != null)
                {
                    RTOForm._FormCloseSygnal = true;
                    ReturnOriginBt.SetBack(false);
                }
                if (AEC != null)
                {
                    AEC.BackMANUALForm_Click();
                    AecBt.SetBack(false);
                }
                if (RefForm != null)
                {
                    RefForm.CloseForm();
                    ReferencingBt.SetBack(false);
                }
                if (FuncForm != null)
                {
                    FuncForm.ReturnForm();
                    FunctionBt.SetBack(false);
                }
                if (plot1.ExpandEn == true)
                {
                    plot1.ExpandEn = false;
                }
                if (_popupTenkey != null)
                {
                    _popupTenkey.Close();
                    _popupTenkey = null;
                }
            }
            NumericFeedBt.SetBack(true);
            NumericFeedBt.SetSelected(true);
            if (NumForm == null)
            {
                NumForm = new NumericFeedForm
                    (
                    NumericFeedForm.FunctionMode.NumericFeed, 
                    axis, 
                    Pos, 
                    coordinate
                    );
                StatusMonitoringEvent += NumForm.RTOStatusMonitoring;
                NumForm.FormClosed += NumericFeedForm_Closed;
                NumForm.SelectFormInit();
                NumForm.Show(this);
            }
            else
            {
                NumForm.NumericFeedForm_Init
                    (
                    NumericFeedForm.FunctionMode.NumericFeed,
                    axis,
                    Pos,
                    coordinate
                    );
            }
        }
        /// <summary>
        /// NumericFeedForm専用Open関数
        /// </summary>
        /// <param name="funcMode"></param>
        /// <param name="axis"></param>
        /// <param name="Pos"></param>
        /// <param name="coordinate"></param>
        public void OpenWorkSettingForm( NumericFeedForm.AxisMode axis = NumericFeedForm.AxisMode.NULL,
                NumericFeedForm.PositionMode Pos = NumericFeedForm.PositionMode.Incrimental,
                NumericFeedForm.CoordinateMode coordinate = NumericFeedForm.CoordinateMode.Work)
        {
            if (NumForm != null)
            {
                if ( NumForm._functionMode == NumericFeedForm.FunctionMode.PositionSet)
                {
                    NumForm.CloseEx();
                    return;
                }                    
                else
                {
                    NumForm.FormClosed -= NumericFeedForm_Closed;
                    NumForm.FormClosed += WorkSettingForm_Closed;
                    NumericFeedBt.SetSelected(false);
                }
            }
            else
            {
                if (RTOForm != null)
                {
                    RTOForm._FormCloseSygnal = true;
                    ReturnOriginBt.SetBack(false);
                }
                if (AEC != null)
                {
                    AEC.BackMANUALForm_Click();
                    AecBt.SetBack(false);
                }
                if (RefForm != null)
                {
                    RefForm.CloseForm();
                    ReferencingBt.SetBack(false);
                }
                if (FuncForm != null)
                {
                    FuncForm.ReturnForm();
                    FunctionBt.SetBack(false);
                }
                if (plot1.ExpandEn == true)
                {
                    plot1.ExpandEn = false;
                }
                if (_popupTenkey != null)
                {
                    _popupTenkey.Close();
                    _popupTenkey = null;
                }
            }
            if(NumForm == null)
            {
                NumForm = new NumericFeedForm
                    (
                    NumericFeedForm.FunctionMode.PositionSet,
                    axis, 
                    Pos, 
                    coordinate
                    );
                StatusMonitoringEvent += NumForm.RTOStatusMonitoring;
                NumForm.FormClosed += WorkSettingForm_Closed;
                NumForm.SelectFormInit();
                NumForm.Show(this);
            }
            else
            {
                NumForm.NumericFeedForm_Init
                    (
                    NumericFeedForm.
                    FunctionMode.
                    PositionSet,
                    NumericFeedForm.AxisMode.NULL,
                    NumericFeedForm.PositionMode.Incrimental,
                    NumericFeedForm.CoordinateMode.Work
                    );
           }
            
        }
        //↑手動画面メインボタンのイベント処理
        ///////////////////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////////////////
        //↓手動画面の加工条件各種ボタンのイベント処理

        /// <summary>入力コントロールの初期化</summary>
        /// <param name="selSw">入力遷移させるボタンコントロールの参照</param>
        /// <param name="textBox">入力コントロールの参照</param>
        /// <param name="format">入力コントロールの書式</param>
        private void InitInput(ButtonEx selSw, NumericTextBox textBox, NumericTextBox.FormatTypes format)
        {
            textBox.FormatType = format;
            textBox.ReadOnly = true;
            selSw.EditBox = textBox;
        }

        //↑手動画面の加工条件各種ボタンのイベント処理

        ///////////////////////////////////////////////////////////////////////////////////////////p
        //↓ランプボタン動作
        private void BuzzerBt_Click(object sender, EventArgs e)
        {
            if ((sender as ButtonEx).GetLed() == false)
            {
                using (SettingFunction SetFunc = new SettingFunction(1))
                {
                    ResultCodes Error = (SetFunc.SettingMonitoring(Settings.Buzzer));
                    //ログ処理
                    UILog logs = new UILog("MANUALForm.SetFunc.SettingMonitoring");
                    if (Error == ResultCodes.Success)
                    {
                        logs.Sure("BUZZER-ON, " + "Result = " + Error);
                        //BuzzerBt.SetLed(true);
                    }
                    else
                    {
                        logs.Error("BUZZER-ON, " + "Result = " + Error);
                    }
                }
            }
            else
            {
                using (SettingFunction SetFunc = new SettingFunction(0))
                {
                    ResultCodes Error = (SetFunc.SettingMonitoring(Settings.Buzzer));
                    //ログ処理
                    UILog logs = new UILog("MANUALForm.SetFunc.SettingMonitoring");
                    if (Error == ResultCodes.Success)
                    {
                        logs.Sure("BUZZER-OFF, " + "Result = " + Error);
                        //BuzzerBt.SetLed(false);
                    }
                    else
                    {
                        logs.Error("BUZZER-OFF, " + "Result = " + Error);
                    }
                }
            }
        }
        private void InitialSetBt_Click(object sender, EventArgs e)
        {
            if ((sender as ButtonEx).GetLed() == false)
            {
                using (SettingFunction SetFunc = new SettingFunction(1))
                {
                    ResultCodes Error = (SetFunc.SettingMonitoring(Settings.InitialSet));
                    //ログ処理
                    UILog logs = new UILog("MANUALForm.SetFunc.SettingMonitoring");
                    if (Error == ResultCodes.Success)
                    {
                        logs.Sure("INITIALSET-ON, " + "Result = " + Error);
                    }
                    else
                    {
                        logs.Error("INITIALSET-ON, " + "Result = " + Error);
                    }
                }
            }
            else
            {
                using (SettingFunction SetFunc = new SettingFunction(0))
                {
                    ResultCodes Error = (SetFunc.SettingMonitoring(Settings.InitialSet));
                    //ログ処理
                    UILog logs = new UILog("MANUALForm.SetFunc.SettingMonitoring");
                    if (Error == ResultCodes.Success)
                    {
                        logs.Sure("INITIALSET-OFF, " + "Result = " + Error);
                    }
                    else
                    {
                        logs.Error("INITIALSET-OFF, " + "Result = " + Error);
                    }
                }
            }
        }

        private void ContactSensingBt_Click(object sender, EventArgs e)
        {
            if ((sender as ButtonEx).GetLed() == false)
            {

                using (SettingFunction SetFunc = new SettingFunction(1))
                {
                    ResultCodes Error = SetFunc.SettingMonitoring(Settings.ContactSensing);
                    //ログ処理
                    UILog logs = new UILog("MANUALForm.SetFunc.SettingMonitoring");
                    if (Error == ResultCodes.Success)
                    {
                        logs.Sure("CONTACTSENSER-ON, " + "Result = " + Error);
                    }
                    else
                    {
                        logs.Error("CONTACTSENSER-ON, " + "Result = " + Error);
                    }
                }
            }
            else
            {
                using (SettingFunction SetFunc = new SettingFunction(0))
                {
                    ResultCodes Error = SetFunc.SettingMonitoring(Settings.ContactSensing);
                    //ログ処理
                    UILog logs = new UILog("MANUALForm.SetFunc.SettingMonitoring");
                    if (Error == ResultCodes.Success)
                    {
                        logs.Sure("CONTACTSENSER-OFF, " + "Result = " + Error);
                    }
                    else
                    {
                        logs.Error("CONTACTSENSER-OFF, " + "Result = " + Error);
                    }
                }
            }
        }
		/// <summary>
		/// プログラム運転時間タイマのリセット
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _programRunTimeBt_Click( object sender, EventArgs e )
		{
			using( MessageDialog msgDia = new MessageDialog(sender as ButtonEx) ) {
                if ( !msgDia.Question( 5503, this ) ) {
					//プログラム運転時間リセット確認" 
					return;
				}
			}
			TimerView.ProgramProcessingTimeReset(McTaskModes.Manual);
			//初期値セット
			MachineTimerTextBox.Text = "0H 0M 0S";
        }
		/// <summary>
		/// 加工時間タイマのリセット
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _processingTimeBt_Click( object sender, EventArgs e )
		{
			using( MessageDialog msgDia = new MessageDialog(sender as ButtonEx) ) {
                if ( !msgDia.Question( 5504, this ) ) {
                    //加工時間リセット確認"
                    return;
				}
			}
			TimerView.DischargeTimeReset(McTaskModes.Manual);
			//初期値セット
			DischargeTimerTextBox.Text = "0H 0M 0S";
        }
        private void _oneProcessingTimeBt_Click(object sender, EventArgs e)
        {
            TimerView.OneProcessingTimeReset(McTaskModes.Manual);
            //初期値セット
            DischargeTimerTextBox.Text = "0H 0M 0S";
        }
        /// <summary>
        /// ポップアップテンキー：表示/非表示
        /// </summary>
        /// <param name="val"></param>
        /// <returns>false=上下ポップアップを表示</returns>
        private bool popupTenkeyOn( object val, string title )
		{
			//フォーマットタイプ
			NumericTextBox.FormatTypes formatType = NumericTextBox.FormatTypes.Integer1;
			if( _popupTenkey != null ) {
				_popupTenkey.Close();   //画面を閉じる
				_popupTenkey = null;    //null初期化
                return false;
            }
            string changeVal = val.ToString();  //編集値
			Decimal lowerLimitDec = (decimal)-9999.999;//最小値
			Decimal upperLimitDec = (decimal)9999.999;//最大値
			formatType = NumericTextBox.FormatTypes.SignDecimalUpper3Lower3;

			//ポップアップTenKey：2017-1-12:柏原
			_popupTenkey = new TenKeyDialog( changeVal, formatType, lowerLimitDec, upperLimitDec, true, true, true );
			//_popupTenkey.Location = new Point( 400, 200 );			//真ん中：400,200※デフォルトはセンター
			_popupTenkey.NotifyReturnOk = popupTenkey_OnNotifyReturnOk; //イベント通知：OK
            _popupTenkey.FormClosed += popupTenkey_Closed;

            _popupTenkey.Text = title;                                  //テンキータイトル表示
			_popupTenkey.ShowDialog( this );                            //画面を開く
			return true;
		}
		private string _int32Val = "";
        private void popupTenkey_Closed(object sender, FormClosedEventArgs e)
        {
            if(_popupTenkey != null)
            {
                _popupTenkey = null;
            }
        }
		/// <summary>
		/// ポップアップテンキーで「OK」ボタンを押した時、入力値をこのクラスに反映
		/// </summary>
		private void popupTenkey_OnNotifyReturnOk()
		{
			_int32Val = _popupTenkey._tenkeyValReturn;//ポップアップテンキーで編集された値

		}
        SelectCommandsDialogSimple selDlg = null;
        int WLim = 0;

        private void WAxisUpperSetBt_Click(object sender, EventArgs e)
        {
            selDlg = new SelectCommandsDialogSimple("現在位置設定", "クリア", "上限値入力", sender as ButtonEx);
            WLim = axisMonitor2.GetWMacVal();
            selDlg.FormClosing += WaxisUpperSetSelDlg_Closing;
            selDlg.FormClosed += WaxisUpperSetSelDlg_Closed;
            selDlg.Show(this);
        }
        private void WaxisUpperSetSelDlg_Closed(object sender, FormClosedEventArgs e)
        {
            if (selDlg != null)
            {
                if (selDlg.IsDisposed == false) selDlg.Dispose();
                selDlg = null;
            }
        }
        private void WaxisUpperSetSelDlg_Closing(object sender, FormClosingEventArgs e)
        {
            if (selDlg != null)
                switch (selDlg.retMessage)
                {
                    case ReturnMessage.ExecuteA1:
                        switch (_digit)
                        {
                            case DigitSelect.Three: WLim /= 1000; break;//下3桁
                            case DigitSelect.Four: WLim /= 10000; break;//下4桁 
                        }
                        break;

                    case ReturnMessage.ExecuteA2:
                        WLim = 0;
                        break;

                    case ReturnMessage.ExecuteA3:
                        //ポップアップTenKey
                        popupTenkeyOn((object)WAxisUpperVal.Text, "W軸上限値");
                        string tempStr = _int32Val;//ポップアップテンキーで編集された値
                        if (tempStr == "")
                        {
                            return;
                        }
                        decimal decWLim = decimal.Parse(tempStr) * 1000;//整数化;
                        //小数以下を取り除き、整数に変換
                        string stringWLim = decWLim.ToString();
                        int dotIndex = stringWLim.IndexOf(".");
                        stringWLim = stringWLim.Substring(0, dotIndex);
                        //NCへデータセット
                        WLim = int.Parse(stringWLim);
                        break;

                    case ReturnMessage.Cancel:
                        return;
                }
            if (selDlg.retMessage == ReturnMessage.Cancel)
            {
                return;
            }

            using (SettingFunction SetFunc = new SettingFunction(WLim))
            {
                ResultCodes Error = SetFunc.SettingMonitoring(Settings.WaxisUpper);
                //ログ処理
                UILog logs = new UILog("MANUALForm.SetFunc.SettingMonitoring");
                if (Error == ResultCodes.Success)
                {
                    logs.Sure("WAXISUPPER-SET " + "Value = " + WLim + ", Result = " + Error);
                }
                else
                {
                    logs.Error("WAXISUPPER-SET " + "Value = " + WLim + ", Result = " + Error);
                }
            }
        }
        #region ManualFormProcSequences
        /// <summary>
        /// 主軸動作
        /// </summary>
        /// <param name="enable"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        internal ResultCodes ManualFormSequences
            (
            bool enable,
            ManualModeProcCommands function = ManualModeProcCommands.Null 
            )
        {
            ResultCodes ret = ResultCodes.Success;
            switch(function)
            {
                case ManualModeProcCommands.Pomp: ret = _PompSequence(enable); break;
                case ManualModeProcCommands.Spin: ret = _SpinSequence(enable); break;
                case ManualModeProcCommands.Discharge: ret = _DischargeSequence(enable); break;
            }


            return ret;
        }
        /// <summary>
        /// ポンプONOFF
        /// </summary>
        /// <param name="enable"></param>
        /// <returns></returns>
        private ResultCodes _PompSequence(bool enable)
        {
            ResultCodes ret = ResultCodes.Success;
            
            if (enable == true)
            {
                using (SequenceFunction SeqFunc = new SequenceFunction(1))
                {
                    UILog logs = new UILog("MANUALForm.SeqFunc.SequenceMonitoring");
                    ret = SeqFunc.SequenceMonitoring(Sequences.PompOn);
                    if (ret == ResultCodes.Success)
                    {
                        logs.Sure("PUMP_ON,  Result = " + ret);
                    }
                    else
                    {
                        logs.Error("PUMP_ON,  Result = " + ret);
                    }
                }
            }
            else
            {
                using (SequenceFunction SeqFunc = new SequenceFunction(0))
                {
                    ret = SeqFunc.SequenceMonitoring(Sequences.PompOn);
                    UILog logs = new UILog("MANUALForm.SeqFunc.SequenceMonitoring");
                    if (ret == ResultCodes.Success)
                    {
                        logs.Sure("POMP-OFF Result = " + ret);
                    }
                    else
                    {
                        logs.Error("POMP-OFF Result = " + ret);
                    }
                }
            }
            return ret;
        }
        /// <summary>
        /// 主軸回転ONOFF
        /// </summary>
        /// <param name="enable"></param>
        /// <returns></returns>
        private ResultCodes _SpinSequence(bool enable)
        {
            ResultCodes ret = ResultCodes.Success;

            if (enable == true)
            {
                using (SequenceFunction SeqFunc = new SequenceFunction(1))
                {
                    ret = SeqFunc.SequenceMonitoring(Sequences.SpindleOn);
                    UILog logs = new UILog("MANUALForm.SeqFunc.SequenceMonitoring");
                    if (ret == ResultCodes.Success)
                    {
                        logs.Sure("SPINDLE_CW-ON Result = " + ret);
                    }
                    else
                    {
                        logs.Error("SPINDLE_CW-ON Result = " + ret);
                    }
                }
            }
            else
            {
                using (SequenceFunction SeqFunc = new SequenceFunction(0))
                {
                    ret = (SeqFunc.SequenceMonitoring(Sequences.SpindleOn));
                    //ログ処理
                    UILog logs = new UILog("MANUALForm.SeqFunc.SequenceMonitoring");
                    if (ret == ResultCodes.Success)
                    {
                        logs.Sure("SPINDLE_CW-OFF Result = " + ret);
                    }
                    else
                    {
                        logs.Error("SPINDLE_CW-OFF Result = " + ret);
                    }
                }
            }
            return ret;
        }
        /// <summary>
        /// 放電ONOFF
        /// </summary>
        /// <param name="enable"></param>
        /// <returns></returns>
        private ResultCodes _DischargeSequence(bool enable)
        {
            ResultCodes ret = ResultCodes.Success;

            if (enable == true)
            {
                using (SequenceFunction SeqFunc = new SequenceFunction(1))
                {
                    ret = SeqFunc.SequenceMonitoring(Sequences.DischargeOn);
                    UILog logs = new UILog("MANUALForm.SeqFunc.SequenceMonitoring");
                    if (ret == ResultCodes.Success)
                    {
                        logs.Sure("DISCHARGE-ON Result = " + ret);
                    }
                    else
                    {
                        logs.Error("DISCHARGE-ON Result = " + ret);
                    }
                }
            }
            else
            {
                using (SequenceFunction SeqFunc = new SequenceFunction(0))
                {
                    ret = (SeqFunc.SequenceMonitoring(Sequences.DischargeOn));
                    //ログ処理
                    UILog logs = new UILog("MANUALForm.SeqFunc.SequenceMonitoring");
                    if (ret == ResultCodes.Success)
                    {
                        logs.Sure("DISCHARGE-OFF Result = " + ret);
                    }
                    else
                    {
                        logs.Error("DISCHARGE-OFF Result = " + ret);
                    }
                }
            }
            return ret;
        }
#endregion

        /// <summary>カレントの加工条件表示の更新</summary>
        private void RefreshProcessCondition()
        {
            UILog editPCondInitLog = new UILog(", MANUALForm()");
            using (McDatProcessCondition mc = new McDatProcessCondition())
            {
                ResultCodes ret = mc.Read();
                if (ResultCodes.Success != ret)
                {
                    //加工条件を取得した時のエラーメッセージ
                    editPCondInitLog.Error("editProcessCondition1.private void RefreshProcessCondition(), PCondition UpdateData Error" + ", " + "Result= " + ret);
                    return;
                }
                //加工条件番号の取得、表示
                if( (this.IsDisposed == true || this.Disposing == true)   ) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                {
                    PnumTextBox.Text = $"{mc.PNo:d3}";
                }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                //加工条件の表示更新    
                editProcessCondition1.UpdateData(mc.PNo);
            }
        }
        
        /// <summary>
        /// タイマー表示のSetアクセサ
        /// </summary>
        /// <param name="timer"></param>
        /// <param name="value"></param>
        internal void SetTimer(TimerCategory timer, string value)
        {
            switch (timer)
            {
                case TimerCategory.ProgramTime:
                    MachineTimerTextBox.Text = value;
                    break;

                case TimerCategory.Discharge:
                    DischargeTimerTextBox.Text = value;
                    break;

                case TimerCategory.OneProcessingTime:
                    ProcTimerTextBox.Text = value;
                    break;
            }
        }

        /// <summary>
        /// 棒グラフのフォーム外からのSetアクセサ
        /// </summary>
        /// <param name="Avalue"></param>
        public void SetLevelGageValue(double Avalue, double Vvalue)
        {
			if( (this.IsDisposed == true || this.Disposing == true)   ) return; retInvoke = BeginInvoke((MethodInvoker)delegate
            {
                if (Avalue != levelGage1.AValue)
                {
                    levelGage1.AValue = Avalue;

                }
                if (Vvalue != levelGage1.VValue)
                {
                    levelGage1.VValue = Vvalue;
                }
                levelGage1.setDummyData();
            }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
        }

        /// <summary>
        /// 折れ線グラフのフォーム外からのSetアクセサ
        /// </summary>
        /// <param name="InfoName"></param>
        /// <param name="iParam"></param>
        public void SetPlotInfo(string InfoName, double dParam)
        {
            switch (InfoName)
            {
                case "Zaxis":
                    if( (this.IsDisposed == true || this.Disposing == true)   ) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                    {
						plot1.SetPlotValue( dParam );
					} );
                    break;

            }
        }

        /// <summary>
        /// 座標の表示桁数が6桁以上の場合、6桁に合わせる。
        /// </summary>
        /// <param name="AxisVal">座標値</param>
        /// <returns></returns>
        private int DigitAlign(string AxisVal)
        {
            int ret = 0;

            //座標の表示桁数を調べる
            switch ((AxisVal.Count()
                 - (AxisVal.IndexOf(".") + 1)))
            {
                //下3桁の場合
                case 3:
                    ret = int.Parse(AxisVal.Replace(".", ""));
                    break;

                //下4桁の場合
                case 4:
                    string AlignVal = AxisVal.Remove(AxisVal.IndexOf(".") + 4).Replace(".", "");

                    if ((AlignVal.Count() <= 6)
                     || (AlignVal.StartsWith("-") && AlignVal.Count() == 7))
                    {
                        ret = int.Parse(AlignVal);
                    }
                    else
                    {
                        while (AlignVal.Count() > 6)
                        {
                            AlignVal = AlignVal.Remove(0, 1);
                            if (AlignVal.Count() == 6
                                || (AlignVal.StartsWith("-") && AlignVal.Count() == 7))
                            {
                                break;
                            }
                        }
                        ret = int.Parse(AlignVal);
                    }

                    break;
            }
            return ret;
        }

        private void _axisCommandSelect(NumericFeedForm.CoordinateMode mode)
        {
            switch (mode)
            {
                case NumericFeedForm.CoordinateMode.Machine:
                    selDlg = new SelectCommandsDialogSimple("位置決め", "", "閉じる");
                    selDlg.FormClosing += MachineAxisCommandSelDlgClosing;
                    break;

                case NumericFeedForm.CoordinateMode.Work:
                    selDlg = new SelectCommandsDialogSimple("位置決め", "軸動作", "閉じる");
                    selDlg.FormClosing += WorkAxisCommandSelDlgClosing;
                    break;
            }
            _boolFlg = false;
            selDlg.StartPosition = FormStartPosition.Manual;
            selDlg.Location = axisMonitor2.SelectCellRightPosition;
            selDlg.FormClosed += CommandSelDlgClosed;
            selDlg.Show(this);            
        }
        private void CommandSelDlgClosed(object sender, FormClosedEventArgs e)
        {
            if (selDlg != null)
            {
                if (selDlg.IsDisposed == false) selDlg.Dispose();
                selDlg = null;
            }
        }
        private void MachineAxisCommandSelDlgClosing(object sender, FormClosingEventArgs e)
        {
            switch (selDlg.retMessage)
            {
                case ReturnMessage.ExecuteA1:
                    OpenNumericFeedForm(axisMonitor2.GetSelectAxis(), NumericFeedForm.PositionMode.Incrimental,
                        axisMonitor2.GetSelectCoordinate()
                        );
                    break;

                case ReturnMessage.ExecuteA2:
                    axisMonitor2.AxisMonitorSelectClear();
                    break;

                case ReturnMessage.ExecuteA3:
                    axisMonitor2.AxisMonitorSelectClear();
                    break;

                case ReturnMessage.Cancel:
                    axisMonitor2.AxisMonitorSelectClear();
                    break;
            }
        }
        bool _boolFlg = false;
        private void WorkAxisCommandSelDlgClosing(object sender, FormClosingEventArgs e)
        {//不具合対応
            if (_boolFlg)
            {
                _boolFlg = false;
                return;
            }
            _boolFlg = true;
            switch (selDlg.retMessage)
            {
                case ReturnMessage.ExecuteA1:
                    OpenNumericFeedForm(axisMonitor2.GetSelectAxis(), NumericFeedForm.PositionMode.Incrimental,
                        axisMonitor2.GetSelectCoordinate()
                        );
                    break;

                case ReturnMessage.ExecuteA2:
                    OpenWorkSettingForm(axisMonitor2.GetSelectAxis(), NumericFeedForm.PositionMode.Incrimental,
                        axisMonitor2.GetSelectCoordinate()
                        );
                    break;

                case ReturnMessage.ExecuteA3:
                    axisMonitor2.AxisMonitorSelectClear();
                    break;

                case ReturnMessage.Cancel:
                    axisMonitor2.AxisMonitorSelectClear();
                    break;

            }
        }
        private void CoordinateHeaderSelect()
        {
            if (axisMonitor2.GetColumnHeaderOnClick() == true)
            {
                ApiMonitorTask.monitorMx.WaitOne();
                OpenWorkSettingForm
                    (
                    axisMonitor2.GetSelectAxis(), 
                    NumericFeedForm.PositionMode.Incrimental,
                    axisMonitor2.GetSelectCoordinate()
                    );
                ApiMonitorTask.monitorMx.ReleaseMutex();
                axisMonitor2.SetColumnHeaderOnClick(false);
            }
        }
        /// <summary>
        /// 子フォームから主軸動作ボタンの状態を取得する関数
        /// </summary>
        /// <param name="status"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        internal bool GetProcCommandButton(ButtonEx.ButtonExStatus status, ManualModeProcCommands command)
        {
            bool ret = false;
            switch (command)
            {
                case ManualModeProcCommands.Pomp:
                    switch(status)
                    {
                        case ButtonEx.ButtonExStatus.Back: ret = PompBt.GetBack(); break;
                        case ButtonEx.ButtonExStatus.Enabled: ret = PompBt.Enabled; break;
                        case ButtonEx.ButtonExStatus.Led: ret = PompBt.GetLed(); break;
                        case ButtonEx.ButtonExStatus.Selected: ret = PompBt.GetSelected(); break;
                    }
                    break;

                case ManualModeProcCommands.Spin:
                    switch (status)
                    {
                        case ButtonEx.ButtonExStatus.Back: ret = SpinBt.GetBack(); break;
                        case ButtonEx.ButtonExStatus.Enabled: ret = SpinBt.Enabled; break;
                        case ButtonEx.ButtonExStatus.Led: ret = SpinBt.GetLed(); break;
                        case ButtonEx.ButtonExStatus.Selected: ret = SpinBt.GetSelected(); break;
                    }
                    break;

            }
            return ret;
        }
        /// <summary>
        /// 子フォームから主軸動作ボタンの状態を取得する関数
        /// </summary>
        /// <param name="status"></param>
        /// <param name="command"></param>
        internal void SetProcCommandButton(ButtonEx.ButtonExStatus status, ManualModeProcCommands command, bool settingValue)
        {
            if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
            {
                switch (command)
                {
                    case ManualModeProcCommands.Pomp:
                        switch (status)
                        {
                            case ButtonEx.ButtonExStatus.Back: PompBt.SetBack(settingValue); break;
                            case ButtonEx.ButtonExStatus.Enabled: PompBt.Enabled = settingValue; break;
                            case ButtonEx.ButtonExStatus.Led: PompBt.SetLed(settingValue); break;
                            case ButtonEx.ButtonExStatus.Selected: PompBt.SetSelected(settingValue); break;
                        }
                        break;

                    case ManualModeProcCommands.Spin:
                        switch (status)
                        {
                            case ButtonEx.ButtonExStatus.Back: SpinBt.SetBack(settingValue); break;
                            case ButtonEx.ButtonExStatus.Enabled: SpinBt.Enabled = settingValue; break;
                            case ButtonEx.ButtonExStatus.Led: SpinBt.SetLed(settingValue); break;
                            case ButtonEx.ButtonExStatus.Selected: SpinBt.SetSelected(settingValue); break;
                        }
                        break;

                }
            }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
        }

        private void MANUALForm_Activated(object sender, EventArgs e)
        {
            editProcessCondition1.Sfip_Init();
            editProcessCondition1.UpdateData();
        }
    }
}