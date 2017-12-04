///////////////////////////////////////////////////////////////////////////////////////////
//
// (1) システム名称 : ELENIX ECNCⅢ
// (2) ファイル名   : UserFuncForm.cs
// (3) 概要         : ユーザー機能画面
// (4) 作成日       : 2016.07.19
// (5) 作成者       : 八野
// (6) 特記事項     : VisualStudio2015 C#
// (7) 更新履歴     :
//
///////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Windows.Forms;
using ECNC3.Enumeration;
using ECNC3.Models;
using ECNC3.Models.McIf;

namespace ECNC3.Views
{
    /// <summary>
    /// ユーザー機能画面
    /// </summary>
    public partial class UserFuncForm : ECNC3Form
    {
        #region Constructor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UserFuncForm()
        {
            InitializeComponent();
			Disposed += UserFuncForm_Disposed;
        }
        #endregion
        #region VariableMember
        internal event StatusMonitoringEventHandler StatusMonitoringEvent; 
        internal event IOMonitoringEventHandler IOStatusMonitoringEvent;
        MessageDialog _msgDia = null;
        /// <summary>
        /// アラームログ画面
        /// </summary>
        AlarmLogForm AlarmLog = null;
        /// <summary>
        /// メンテナンス時刻設定画面
        /// </summary>
        MaintenanceForm MaintForm = null;
        /// <summary>
        /// 材質名登録画面
        /// </summary>
        MaterialNameForm Material = null;
        /// <summary>
        /// マクロ変数設定画面
        /// </summary>
        MacroVarSetForm MacroSet = null;
        /// <summary>
        /// アラーム表示画面
        /// </summary>
        AlarmDialog _formAlarm = null;
        /// <summary>メーカーサービス画面</summary>
        public MakerServiceForm _formMaker = null;
		/// <summary>ユーザーサービス画面</summary>
		public UserServiceForm _formUser = null;
        /// <summary>MCステータス情報</summary>
		private McDatStatus _beforeMcStatus = null;
        /// <summary>
        /// 主電源ON時間タイマ
        /// </summary>
        private ulong _oldMainPowerSupplyOnTimer = 0;
        /// <summary>
        /// 加工電源ON時間タイマ
        /// </summary>
        private ulong _oldProcessingPowerOnTimer = 0;
        bool ProgramResume = false;
        bool ProgramResumeReady = false;
        bool StartBtnOffEdge = false;
        /// <summary>設定結果取得関数定義</summary>
        public delegate void NotifyReturnDelegate();
		/// <summary>終了通知</summary>
		public NotifyReturnDelegate _notifyReturn = null;
		/// <summary>>設定結果取得</summary>
		public NotifyReturnDelegate NotifyReturn
        {
            set { _notifyReturn = value; }
        }
        #endregion
        #region PrivateMethods
        /// <summary>リセットボタン押下</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _btnReset_Click(object sender, EventArgs e)
        {
			//リセットを実行します。&#xD;&#xA;よろしいですか？
			using( MessageDialog msg = new MessageDialog() ) {
				if( false == msg.Question( 5500, this ) ) {
					return;
				}
			}
			ResultCodes ret = ResultCodes.Success;
            //	汚水槽エラーかつSPXリセット無効設定のときはリセットを実行しない。
            while (true)
            {
                using (McDatStatus mc = new McDatStatus())
                {
                    ret = mc.Read();
                    if (ResultCodes.Success != ret)
                    {
                        break;
                    }
                    if (true == mc.Status.EtherCatErrorHoldingTankLiquidEmpty)
                    {
                        using (FileSettings fs = new FileSettings())
                        {
                            ret = fs.Read();
                            if (ResultCodes.Success != ret)
                            {
                                break;
                            }
                            if (true == fs.AttrBool("Root/Motions/Spx/Reset", "enbl"))
                            {
                                break;  //	リセットしない
                            }
                        }
                    }
                }
                using (McReqReset mc = new McReqReset())
                {
                    ret = mc.Execute();
                    if (ResultCodes.Success != ret)
                    {
                        break;
                    }
                }
                return;
            }
            if (ResultCodes.Success != ret)
            {
                using (MessageDialog dlg = new MessageDialog())
                {
                    dlg.Error(110, this);
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

            if (null == _formAlarm)
            {
                _formAlarm = new AlarmDialog();
                _formAlarm.NotifyReturn = OnNotifyReturnFromAlarm;
                _formAlarm.Show(this);
            }
            else
            {
                _formAlarm.Close();
                _formAlarm = null;
            }
            if (null != _formAlarm)
            {
                if (true == _formAlarm.Visible)
                {
                    _beforeMcStatus = new McDatStatus();
                    _beforeMcStatus.Read();
                    _formAlarm.RefreshAlarmOnly(_beforeMcStatus.Status);
                }
            }
        }
        /// <summary>
        /// アラーム画面クローズ処理
        /// </summary>
        private void OnNotifyReturnFromAlarm()
        {
            if (null != _formAlarm)
            {
                _formAlarm.Close();
                _formAlarm = null;
            }
        }
        /// <summary>
        /// 解放処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserFuncForm_Disposed(object sender, EventArgs e)
        {
            if (null != AlarmLog)
            {
                AlarmLog.Close();
                AlarmLog = null;
            }
            if (null != MaintForm)
            {
                MaintForm.Close();
                MaintForm = null;
            }
            if (null != Material)
            {
                Material.Close();
                Material = null;
            }
            if (null != MacroSet)
            {
                MacroSet.Close();
                MacroSet = null;
            }
            if (null != _formMaker)
            {
                _formMaker.Close();
                _formMaker = null;
            }
            if (null != _formUser)
            {
                _formUser.Close();
                _formUser = null;
            }
            if (null != _beforeMcStatus)
            {
                _beforeMcStatus.Dispose();
                _beforeMcStatus = null;
            }
            StatusMonitoringEvent = null;
        }
                
        /// <summary>
        /// アラームログ画面をモードレスで開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">AlarmLogBtのボタンクリックイベント</param>
        private void AlarmLogBt_Click(object sender, EventArgs e)
        {
            AlarmLog = new AlarmLogForm();
            AlarmLog.Show(this);
        }

        /// <summary>
        /// メンテナンス時刻設定画面をモードレスで開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">MaintenanceFormBtのボタンクリックイベント</param>
        private void MaintenanceFormBt_Click(object sender, EventArgs e)
        {
            MaintForm = new MaintenanceForm();
            MaintForm.Show(this);
        }

        /// <summary>材質名登録画面をモードレスで開く </summary>
        /// <param name="sender"></param>
        /// <param name="e">MateerialNameBtのボタンクリックイベント</param>
        private void MaterialNameBt_Click(object sender, EventArgs e)
        {
			if( null == Material ) {
				Material = new MaterialNameForm( 0 );
				Material.NotifyReturn = _btnMaterial_Click;
				Material.Show( this );
			}
        }
		void _btnMaterial_Click()
		{
			if( null != Material ) {
				Material.Close();
				Material = null;
			}
		}
		/// <summary>
		/// マクロ変数設定画面を開く
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">MacroVarSetBtのボタンクリックイベント</param>
		private void MacroVarSetBt_Click(object sender, EventArgs e)
        {
            MacroSet = new MacroVarSetForm();
            MacroSet.Show(this);
        }
		/// <summary>
		/// サービス画面パスワード入力画面を開く
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">ServiceFormBtのボタンクリックイベント</param>
		/// <remarks>パスワードによって、UserServiceForm又はMakerServiceFormを開く</remarks>
		private void ServiceFormBt_Click(object sender, EventArgs e)
        {
            AccountLevel ret = FuncPassForm.ShowDialogMode(this);
            switch (ret)
            {
                case AccountLevel.Admin:
                    //	メーカー
                    if (null == _formUser)
                    {
                        _formUser = new UserServiceForm(true);
                        StatusMonitoringEvent = _formUser.StatusMonitoring;
                        IOStatusMonitoringEvent = _formUser.IOStatusMonitoring;
                        _formUser.NotifyReturn = _btn_ClickCallBack;
                        _formUser.Show(this);
                    }
                    break;

                case AccountLevel.MakerAdmin:
                    //	メーカー
                    if (null == _formUser)
                    {
                        _formUser = new UserServiceForm(true);
                        StatusMonitoringEvent = _formUser.StatusMonitoring;
                        IOStatusMonitoringEvent = _formUser.IOStatusMonitoring;
                        _formUser.NotifyReturn = _btn_ClickCallBack;
                        _formUser.Show(this);
                    }
                    break;

                case AccountLevel.Maker:
                    //	メーカー
                    if (null == _formUser)
                    {
                        _formUser = new UserServiceForm(true);
                        StatusMonitoringEvent = _formUser.StatusMonitoring;
                        IOStatusMonitoringEvent = _formUser.IOStatusMonitoring;
                        _formUser.NotifyReturn = _btn_ClickCallBack;
                        _formUser.Show(this);
                    }
                    break;

                case AccountLevel.UserAdmin:
                    //	ユーザー
                    if (null == _formUser)
                    {
                        _formUser = new UserServiceForm(false);
                        StatusMonitoringEvent = _formUser.StatusMonitoring;
                        IOStatusMonitoringEvent = _formUser.IOStatusMonitoring;
                        _formUser.NotifyReturn = _btn_ClickCallBack;
                        _formUser.Show(this);
                    }
                    break;

                case AccountLevel.User:
                    //	ユーザー
                    if (null == _formUser)
                    {
                        _formUser = new UserServiceForm(false);
                        StatusMonitoringEvent = _formUser.StatusMonitoring;
                        IOStatusMonitoringEvent = _formUser.IOStatusMonitoring;
                        _formUser.NotifyReturn = _btn_ClickCallBack;
                        _formUser.Show(this);
                    }
                    break;

                case AccountLevel.Guest:
                    //	ユーザー
                    if (null == _formUser)
                    {
                        _formUser = new UserServiceForm(false);
                        StatusMonitoringEvent = _formUser.StatusMonitoring;
                        IOStatusMonitoringEvent = _formUser.IOStatusMonitoring;
                        _formUser.NotifyReturn = _btn_ClickCallBack;
                        _formUser.Show(this);
                    }
                    break;

                case AccountLevel.None:
                    return;
                    
            }
		}
        /// <summary>
        /// 自動運転開始位置への復帰ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button10_Click(object sender, EventArgs e)
        {
            //フラグのONOFF
            ProgramResumeReady = (ProgramResumeReady == true) ? false : true;
            //フラグの状態に合わせてボタンの背景変更
            if (ProgramResumeReady == true)
            {
                (sender as ButtonEx).SetBack(true);
                if (_msgDia == null || _msgDia.IsDisposed == true)
                {
                    _msgDia = new MessageDialog();
                    _msgDia.StatusMessage(893, this);
                } 
            }
            else
            {
                (sender as ButtonEx).SetBack(false);
            }            
        }
        private void _btn_ClickCallBack()
		{
			if( null != _formMaker ) {
				_formMaker.Close();
				_formMaker = null;
			}
			if( null != _formUser ) {
				_formUser.Close();
				_formUser = null;
			}
            StatusMonitoringEvent = null;
			IOStatusMonitoringEvent = null;
		}

		private void UserFuncForm_Load( object sender, EventArgs e )
		{

#if __V001_INHIBIT__
#else
			foreach( Control item in this.Controls ) {
				if( ( true == item.Equals( panel8 ) ) || ( true == item.Equals( panel2 ) || ( true == item.Equals( panel6 ) ) ) ) {
					continue;
				} else if( true == item.Equals( panel4 ) ) {
					foreach( Control item1 in panel4.Controls ) {
						if( true == item1.Equals( MaterialNameBt ) ) {
							continue;
						}
						item1.Enabled = false;
					}
					continue;
				}
				item.Enabled = false;
			}
#endif
            using (FileSettings fs = new FileSettings())
            {
                fs.Read();
                _thinEnBt.Enabled = (fs.AttrBool("Root/Service/ThinLineSetting", "enbl"));
            }
        }
        private void SetTimer(TimerCategory timer, string value)
        {
            switch (timer)
            {
                case TimerCategory.MainPowerOnTime:
                    APLTimeTextBox.Text = value;
                    break;

                case TimerCategory.ProcessingPowerOnTime:
                    DischargeTimeTextBox.Text = value;
                    break;

            }
        }
        private void _thinEnBt_Click(object sender, EventArgs e)
        {
            if (_thinEnBt.Enabled == false) return;
            using (McReqPartitionChange mcPart = new McReqPartitionChange())
            {
                mcPart.Read();
                mcPart.Partitions.Items[0].Thinline = !mcPart.Partitions.Items[0].Thinline;
                //NC書き込みとファイル書き込み
                if (mcPart.Execute() == ResultCodes.Success) mcPart.Write();
            }
        }
        #endregion
        #region PublicMethods
        public void ReturnForm()
        {
            //this.Close();
            _notifyReturn?.Invoke();
        }
        /// <summary>サービス画面表示／非表示判定</summary>
        public bool ShownIOForm
        {
            get
            {
                if (null != _formUser)
                {
                    return _formUser.ShownIOForm;
                }
                return false;
            }
        }
        /// <summary>サービス画面表示／非表示判定</summary>
        public bool ShownMaintForm
        {
            get
            {
                if (null != MaintForm)
                {
                    return MaintForm.Visible;
                }
                return false;
            }
        }

        internal void StatusMonitoring(StatusMonitorEventArgs e)
        {
            if (StatusMonitoringEvent != null)
            {
                StatusMonitoringEvent(e);
            }
            //スタートボタンOFFエッジ検出
            if(e.Items.StartSwBtnOffEdge != StartBtnOffEdge)
            {
                StartBtnOffEdge = e.Items.StartSwBtnOffEdge;
            }

            //プログラム運転タイマー
            if (_oldMainPowerSupplyOnTimer != TimerView.GetMainPowerTime())
            {
                ulong Hour, Min, Sec;
                TimerView.GetMainPowerTime(out Hour, out Min, out Sec);
                if( (this.IsDisposed == true || this.Disposing == true)   ) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                {
                    SetTimer(TimerCategory.MainPowerOnTime, Hour.ToString() + "H " + Min.ToString() + "M " + Sec.ToString() + "S");
                }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                _oldMainPowerSupplyOnTimer = TimerView.GetMainPowerTime();
            }
            //プログラム運転タイマー
            if (_oldProcessingPowerOnTimer != TimerView.GetProcessingPowerTime())
            {
                ulong Hour, Min, Sec;
                TimerView.GetProcessingPowerTime(out Hour, out Min, out Sec);
                if( (this.IsDisposed == true || this.Disposing == true)   ) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                {
                    SetTimer(TimerCategory.ProcessingPowerOnTime, Hour.ToString() + "H " + Min.ToString() + "M " + Sec.ToString() + "S");
                }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                _oldProcessingPowerOnTimer = TimerView.GetProcessingPowerTime();
            }
            //細線モード有効無効
            if (_thinEnBt.Enabled == true
                || e.Items.ThinEn != _thinEnBt.GetLed())
            {
                _thinEnBt.SetLed(e.Items.ThinEn);

            }
            //自動運転開始座標への復帰
            if (_msgDia != null)
            {
                if (
                    _msgDia.Visible == false
                    && _ProgramResumeBtn.GetBack() == true
                    )
                {
                    _ProgramResumeBtn.SetBack(false);
                    _MessageBoxClose();
                }                
            }
                
            if(
                ProgramResume == false
                && _ProgramResumeBtn.GetBack() == true
                )
            {
                if(StartBtnOffEdge == true)
                {
                    using (McReqProgramResume resumePos = new McReqProgramResume())
                    {
                        resumePos.Execute();
                    }
                    ProgramResume = true;
                    ProgramResumeReady = false;
                    if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                    {
                        _MessageBoxClose();
                    }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                }
                if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                {
                    if (e.Items.SequenceEnd == false)
                    {
                        //メッセージ表示
                        _MessageBoxInit(894);
                    }
                    else
                    {
                        //メッセージを閉じる
                        if(ProgramResume == true) _MessageBoxClose(true);
                    }
                }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
            }
            else
            {
                if (ProgramResumeReady == false && ProgramResume == true)
                {
                    ProgramResume = false;
                    _ProgramResumeBtn.SetBack(false);
                }
            }
        }
        internal void IOStatusMonitoring(IOMonitorEventArgs e)
        {
            if (IOStatusMonitoringEvent != null)
            {
                IOStatusMonitoringEvent(e);
            }
        }
        /// <summary>
        /// メッセージ表示時の初期処理
        /// </summary>
        /// <param name="msgID"></param>
        private void _MessageBoxInit(int msgID)
        {
            if (_msgDia != null)
            {
                _msgDia = new MessageDialog();
                _msgDia.StatusMessage(msgID, this);
            }
        }
        /// <summary>
        /// メッセージ表示を閉じる。
        /// </summary>
        private void _MessageBoxClose(bool programResumeSequence = false)
        {
            //null参照排他
            if (_msgDia == null) return;
            //メッセージを閉じる
            _msgDia.Close();
            _msgDia = null;
            _ProgramResumeBtn.SetBack(false);
            //自動運転開始座標にて使用したメッセージ表示の破棄処理時の処理
            if ( programResumeSequence == true )
            {
                ProgramResume = false;
            }
        }
        #endregion

        private void UserFuncForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void _CloseBtn_Click(object sender, EventArgs e)
        {
            ReturnForm();
        }
    }
}
