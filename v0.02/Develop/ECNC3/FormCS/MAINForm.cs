///////////////////////////////////////////////////////////////////////////////////////////
//
// (1) システム名称 : ELENIX ECNCⅢ
// (2) ファイル名   : MAINForm.cs
// (3) 概要         : メイン画面
// (4) 作成日       : 2016.06.01
// (5) 作成者       : 八野
// (6) 特記事項     : VisualStudio2015 C#
// (7) 更新履歴     :
//
///////////////////////////////////////////////////////////////////////////////////////////


using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ECNC3.Models.McIf;
using ECNC3.Models.Common;
using ECNC3.Enumeration;
using System.IO;
using System.Diagnostics;
using ECNC3.Views.Popup;
using ECNC3.Models;

namespace ECNC3.Views
{
    public partial class MAINForm : ECNC3Form
    {
		//-----------------------------------------------------------------------------------------
		//
		//　フォーム　コンストラクタ
		//
		//-----------------------------------------------------------------------------------------
		public MAINForm()
        {
            InitializeComponent();
        }
        /// <summary>MCボードインターフェース</summary>
        /// <remarks>
        /// 2016.09.21 Takano MOD	初期化失敗時にアプリケーションがダウンする不具合への対応。
        /// </remarks>
        private Models.McIf.McCommProc _mcIf = null;

        internal ApiMonitorTask ApiMonitor = null;

        private MAINFormCategory Category = MAINFormCategory.Manual;
        private MAINFormCategory _BeforeCategory = MAINFormCategory.Auto;

        //スタートボタンオフエッジフラグ
        internal bool StartBtOffEdge = false;
        private ulong _oldProgramTimer = 0;
        private ulong _oldProcessingTimer = 0;
        private double _oldOneProcessingTimer = 0; 
        TimerView timerCounter = null;

        ///////////////////////////////////////////////////////////////////////////////////////////
        //↓子画面の宣言
        /// <summary>
        /// 手動画面
        /// </summary>
        internal MANUALForm MANUAL = null;
        /// <summary>
        /// 自動/MDI画面
        /// </summary>
        internal MDIAUTOForm MDIAUTO = null;
        /// <summary>
        /// 編集画面
        /// </summary>
        internal EDITForm EDIT = null;
		/// <summary>アラーム画面</summary>
		private AlarmDialog _formAlarm = null;
        private MaintenanceForm _formMaintenance = null;
		/// <summary>MCステータス情報</summary>
		private StructureMcDatStatus _beforeMcStatus = null;
        /// <summary>
        /// ロード画面
        /// </summary>
        private LoadStatusView loadStatusView = null;
		//↑子画面の宣言
		///////////////////////////////////////////////////////////////////////////////////////////

		//-----------------------------------------------------------------------------------------
		//
		//　フォーム　ロード時のイベントハンドラ
		//
		//-----------------------------------------------------------------------------------------
		private async void MAINForm_Load(object sender, EventArgs e)
        {
            if (loadStatusView == null) loadStatusView = new LoadStatusView();
            loadStatusView.Show(this);
            loadStatusView.ProgressBarValue = 0;

            //外枠表示
            this.OutLineEnable = false;
            loadStatusView.ProgressBarValue = 7;
            //UI終了イベントの処理設定
            Application.ApplicationExit += Application_ApplicationExit;
            loadStatusView.ProgressBarValue = 14;
            if ( null == _mcIf ) {
				_mcIf = new Models.McIf.McCommProc();
                _mcIf.NotifyUpdate = _UpdateProgress;
            }
            loadStatusView.ProgressBarValue = 21;
            //MC初期化処理
            ResultCodes ret = ResultCodes.Success;
            loadStatusView.ProgressBarValue = 28;
            Task MainTask = Task.Run(new Action(() =>
            {
                ret = _mcIf.Initialize();
            })); await MainTask;
            loadStatusView.ProgressBarValue = 42;
            Thread.Sleep(500);
            if ( Enumeration.ResultCodes.Success != ret ) {

                //	MCへの接続が失敗した場合は、画面の起動を中断する。
                using (MessageDialog msgDir = new MessageDialog())
                {
                    if(msgDir.WarningYesNo(5048, this) == true)
                    {
                        loadStatusView.Close();
                        loadStatusView = null;
                        this.Close();
                        return;

                    }
                }              
			}
            loadStatusView.ProgressBarValue = 49;
            //加工条件チェック
            switch(_ProcessConditionInit())
            {
                case ResultCodes.McCommErrorInvalidParameter: this.Close(); return;
            }
            loadStatusView.ProgressBarValue = 56;
            //タイマーカウント開始
            timerCounter = new TimerView();
            loadStatusView.ProgressBarValue = 63;
            //手動/自動/編集　各画面の初期化
            if (MANUAL == null)MANUAL = new MANUALForm();
            if(MDIAUTO == null)MDIAUTO = new MDIAUTOForm();
            if(EDIT == null)EDIT = new EDITForm();
            loadStatusView.ProgressBarValue = 70;
            //各画面非表示設定
            MANUAL.Visible = false;
            MDIAUTO.Visible = false;
            EDIT.Visible = false;
            loadStatusView.ProgressBarValue = 77;
            //親子関係設定
            MANUAL.Show(this);
            MDIAUTO.Show(this);
            EDIT.Show(this);
            loadStatusView.ProgressBarValue = 84;
            //ステータス取得ループ初期化処理
            if ( null == ApiMonitor )
            {
                ApiMonitor = new ApiMonitorTask();
            }
            //ステータス取得イベント処理設定
            ApiMonitor.IOMonitoringEvent += MANUAL.IOStatusMonitoring;
            ApiMonitor.IOMonitoringEvent += IOStatusMonitoring;
            ApiMonitor.StatusMonitoringEvent += MainStatusMonitoring;

            loadStatusView.ProgressBarValue = 91;
            //ステータス取得ループ処理開始
            try{ ApiMonitor.ApiMonitoring(); }
            catch(Exception ex) { string result = ECNC3Exception.ThreadExceptionFilter(ex); }
        }
        private void _UpdateProgress()
        {
            if ((this.IsDisposed == true || this.Disposing == true)) return;
            retInvoke = BeginInvoke((MethodInvoker)delegate
            {
                loadStatusView.SubProgressBarValue = _mcIf.ProgressValue;
            }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
            EndInvoke(retInvoke);
        }
        private ResultCodes _ProcessConditionInit()
        {
            ResultCodes ret = ResultCodes.Success;
            using (FileProcessCondition beforeData = new FileProcessCondition())
            using (FileProcessConditionParameter pcondParam = new FileProcessConditionParameter())
            {
                ret = beforeData.Read();
                ret = pcondParam.Read();
                using (McDatInitialPrm datIni = new McDatInitialPrm())
                {
                    ret = datIni.Read();
                    if (ret != ResultCodes.Success) return ret;
                    bool repairFlag = false;
                    foreach (StructureProcessConditionItem tempItem in beforeData.Items)
                    {
                        if (tempItem.IsValid(datIni.EnableSF02, (int)(pcondParam.Caps.Find(x => x.Number == pcondParam.CapsBit.BoundIndex + 1).Value * 1000)) == false)
                        {
                            if (repairFlag == false)
                            {
                                using (MessageDialog msgDia = new MessageDialog())
                                {
                                    if (msgDia.WarningYesNo(5046, this))
                                    {
                                        tempItem.Repair();
                                        beforeData.Write(tempItem);
                                        _MessageShow(MessageBoxIcon.Information, 5047);
                                    }
                                    else
                                    {
                                        return ResultCodes.McCommErrorInvalidParameter;
                                    }
                                }
                            }
                            else
                            {
                                if(true == tempItem.Repair()) beforeData.Write(tempItem);
                            }
                            repairFlag = true;
                        }

                    }
                }
            }
            using (McDatProcessConditionTable datPcond = new McDatProcessConditionTable())
            {
                datPcond.Initialize();
            }
            return ret;
        }


        #region Monitorling
        /// <summary>
        /// ステータス表示イベントハンドラ
        /// </summary>
        /// <param name="e">状態監視用クラス</param>
        private void MainStatusMonitoring(StatusMonitorEventArgs e)
        {
            if (loadStatusView != null)
            {                
                Thread.Sleep(500);
                if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                {
                    loadStatusView.ProgressBarValue = 98;
                }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
            }
            //シャットダウン処理
            if (_ShutDownSequence(e)) return;
            //スタートボタンのオフエッジ処理
            if (StartBtOffEdge != e.Items.StartSwBtnOffEdge) StartBtOffEdge = e.Items.StartSwBtnOffEdge;
            //メンテナンスタイマー処理
            _MaintTimerHandler();

            //モード別処理
            switch (e.Items.ProcessMode)
            {
                //手動モード時
                case Enumeration.McTaskModes.Manual:
                    if(_BeforeCategory != Category)
                    {
                        //MDI画面表示フラグリセット
                        if (MDIAUTO.MdiModeEn == true) MDIAUTO.ModeChg(MAINFormCategory.Auto);
                        //表示非表示切替
                        MainFormLayerMove(Category);
                        //ボタン表示切替
                        ModeButtonChenge(Category);
                        //イベントループ処理変更
                        MainFormEventChange(Category);
                        //モード変更完了
                        _BeforeCategory = Category;
                    }
                    else
                    {
                        //モード変更完了
                        if(Category != MAINFormCategory.Edit)_BeforeCategory = Category = MAINFormCategory.Manual;
                        //MDI画面表示フラグリセット
                        if (MDIAUTO.MdiModeEn == true) MDIAUTO.ModeChg(MAINFormCategory.Auto);
                        //表示非表示切替
                        MainFormLayerMove(Category);
                        //ボタン表示切替
                        ModeButtonChenge(Category);
                        //イベントループ処理変更
                        MainFormEventChange(Category);
                    }

                    if (Category != MAINFormCategory.Edit)
                    {
                        //加工ステータス取得、表示
                        _ReadProcessStatus(e);
                        //タイマー値取得、表示
                        _ReadTimer(e);
                        //IOモードチェック
                        _IOModeCheck();
                        //ガイド貫通動作許可
                        _ReqGuideThroughEnable(e);
                    }
                    break;

                //自動モード時
                case Enumeration.McTaskModes.Auto:
                    if (_BeforeCategory != Category)
                    {         
                        //ボタン変更
                        ModeButtonChenge(Category);
                        //自動/MDI切り替え
                        MDIAUTO.ModeChg(Category);
                        //表示非表示切り替え
                        MainFormLayerMove(Category);
                        //イベントループ処理変更
                        MainFormEventChange(Category);
                        //モード変更完了
                        _BeforeCategory = Category;
                    } 
                    else
                    {
                        if (ApiMonitor.ReadFileName != MDIAUTO.GetReadPath()) ApiMonitor.ReadFileName = MDIAUTO.GetReadPath();
                        //位置出し画面表示の場合
                        if (MANUAL.RefForm != null)
                        {
                            //AUTO画面の場合
                            if (MDIAUTO.MdiModeEn == false)
                            {
                                //表示非表示切り替え
                                MainFormLayerMove(MAINFormCategory.Manual);
                                //ボタンONOFF切り替え
                                ModeButtonChenge(MAINFormCategory.Manual);
                                //イベントループ処理変更
                                MainFormEventChange(MAINFormCategory.Manual);
                            }
                            //加工ステータス取得、表示
                            _ReadProcessStatus(e, McTaskModes.Manual);
                            //タイマー値取得、表示
                            _ReadTimer(e, McTaskModes.Manual);
                            //IOモードチェック
                            _IOModeCheck();
                            return;
                        }
                        else
                        {
                            //モード変更完了
                            if ((Category != MAINFormCategory.Edit)
                                && (Category != MAINFormCategory.MDI)) _BeforeCategory = Category = MAINFormCategory.Auto;
                            //ボタン変更
                            ModeButtonChenge(Category);
                            //自動/MDI切り替え
                            MDIAUTO.ModeChg(Category);
                            //表示非表示切り替え
                            MainFormLayerMove(Category);
                            //イベントループ処理変更
                            MainFormEventChange(Category);
                        }
                    }
                    //加工レベル表示
                    _ReadProcessStatus(e);
                    //ブログラム運転開始
                    _ReqProgramStart(e);
                    //返送要求コマンド                   
                    _ReqReturnCommand(e);
                    //タイマー値取得、表示
                    _ReadTimer(e);
                    break;

            }
            //時計表示
            _DayTimeView();
            //アラームとメンテナンスアイコン表示切替
            _StatusIconChange(e);
            //非アクティブでアラーム画面を閉じる
            _AlarmForm_Close();
            //非アクティブでメンテナンス画面を閉じる
            _MaintenanceForm_Close();

            if (loadStatusView != null)
            {
                if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                {
                    loadStatusView.ProgressBarValue = 100;
                    Thread.Sleep(2000);
                    loadStatusView.Close();
                    loadStatusView = null;
                    this.Refresh();
                    MANUAL.Refresh();
                }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
            }
        }
        #endregion
        /// <summary>
        /// アラーム画面の最前面表示処理
        /// </summary>
        private void _AlarmForm_Close()
        {
            if (null == _formAlarm) return;
            if (true == _formAlarm.IsActivated) return;
            //非同期処理時呼出元の破棄済み確認
            if ((this.IsDisposed == true || this.Disposing == true)) return;
            retInvoke = BeginInvoke((MethodInvoker)delegate
            {
                if (_btnAlarm.IsMouseOver == true) return;
                _formAlarm.Close();
                _formAlarm = null;
            }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
        }
        /// <summary>
        /// メンテナンス画面の最前面表示処理
        /// </summary>
        private void _MaintenanceForm_Close()
        {
            if (null == _formMaintenance) return;
            if (true == _formMaintenance.IsActivated) return;
            //非同期処理時呼出元の破棄済み確認
            if ((this.IsDisposed == true || this.Disposing == true)) return;
            retInvoke = BeginInvoke((MethodInvoker)delegate
            {
                ApiMonitor.MaintTimer.ReadTimer();
                _formMaintenance.Close();
                _formMaintenance = null;
            }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
        }

        /// <summary>アプリケーション終了イベント</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// アプリケーションをシャットダウンしようとすると発生します。
        /// </remarks>
        private void Application_ApplicationExit( object sender, EventArgs e )
		{
            //MC内プログラム削除
            using (McReqProgramDelete progDel = new McReqProgramDelete())
            {
                progDel.Execute();
            }
            //メンテナンスタイマーの保存
            ApiMonitor.MaintTimer.onStop();
            ApiMonitor.MaintTimer.SaveTimer();
            TimerView.SaveTimer();
            //外部ファイルバックアップ処理
            _mcIf.Backup();
            //イベントループ処理の停止
            if (null != ApiMonitor) ApiMonitor.LoopBreak = true;
            ApiMonitor.StatusMonitoringEvent -= MainStatusMonitoring;
            ApiMonitor.IOMonitoringEvent -= IOStatusMonitoring;

			if( null != ApiMonitor ) {
				ApiMonitor.Dispose();
				ApiMonitor = null;
			}
			if( null != _mcIf ) {
				_mcIf.Dispose();
				_mcIf = null;
			}
            if (null != _formAlarm)
            {
                _formAlarm.Close();
                _formAlarm = null;
            }
            if (null != _formMaintenance)
            {
                _formMaintenance.Close();
                _formMaintenance = null;
            }
            if ( null != _beforeMcStatus ) {
				_beforeMcStatus.Dispose();
				_beforeMcStatus = null;
			}
			//2017-01-12：柏原
			if( null != _formTenKeyDialog ) {
				_formTenKeyDialog.Dispose();
				_formTenKeyDialog = null;
			}
            if(null != loadStatusView)
            {
                loadStatusView.Close();
                loadStatusView = null;
            }
		}

		private bool ShownIOForm { get { return ( true == MANUAL?.FuncForm?.ShownIOForm ) ? true : false; } }
        private bool ShownMaintForm { get { return (true == MANUAL?.FuncForm?.ShownMaintForm 
                                                    || true == MANUAL?.FuncForm?._formMaker?.ShownMaintForm) ? true : false; } }
        bool maintTimerSaved = false;
        private void IOStatusMonitoring(IOMonitorEventArgs e)
        {
			if( ( false == ShownIOForm ) && ( null != ApiMonitor ) ) {
				ApiMonitor.Mode = MonitoringMode.Main;
			}
		}

        private void ManualBtCheck(ButtonCheck Chk)
        {
            switch (Chk)
            {
                case ButtonCheck.UnCheck:
                    if(MANUALFormBt.GetBack() == true)
                    {
                        MANUAL.ManualFunctionFormsClose();
                    }
                    MANUALFormBt.SetBack(false);
                    break;

                case ButtonCheck.Check:
                    MANUALFormBt.SetBack(true);
                    break;
            }
        }
        private void MDIBtCheck(ButtonCheck Chk)
        {
            switch (Chk)
            {
                case ButtonCheck.UnCheck:
                    if (MDIFormBt.GetBack() == true)
                    {
                        MDIAUTO.FunctionFormsClose();
                    }
                    MDIFormBt.SetBack(false);
                    break;

                case ButtonCheck.Check:
                    MDIFormBt.SetBack(true);
                    break;
            }
        }
        private void EditBtCheck(ButtonCheck Chk)
        {
            switch (Chk)
            {
                case ButtonCheck.UnCheck:
                    if (EDITFormBt.GetBack() == true)
                    {
                        EDIT.FunctionFormsClose();
                    }
                    EDITFormBt.SetBack(false);
                    break;

                case ButtonCheck.Check:
                    EDITFormBt.SetBack(true);
                    break;
            }
        }
        private void AutoBtCheck(ButtonCheck Chk)
        {
            switch(Chk)
            {
                case ButtonCheck.UnCheck:
                    if (AUTOFormBt.GetBack() == true)
                    {
                        MDIAUTO.FunctionFormsClose();
                    }
                    AUTOFormBt.SetBack(false);
                    break;

                case ButtonCheck.Check:
                    AUTOFormBt.SetBack(true);
                    break;
            }
        }

        private void ModeButtonChenge(MAINFormCategory category)
        {
            if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
            {
                switch (category)
                {
                    case MAINFormCategory.Manual:
                        ManualBtCheck(ButtonCheck.Check);
                        MDIBtCheck(ButtonCheck.UnCheck);
                        EditBtCheck(ButtonCheck.UnCheck);
                        AutoBtCheck(ButtonCheck.UnCheck);
                        break;

                    case MAINFormCategory.MDI:
                        ManualBtCheck(ButtonCheck.UnCheck);
                        MDIBtCheck(ButtonCheck.Check);
                        EditBtCheck(ButtonCheck.UnCheck);
                        AutoBtCheck(ButtonCheck.UnCheck);
                        break;

                    case MAINFormCategory.Edit:
                        ManualBtCheck(ButtonCheck.UnCheck);
                        MDIBtCheck(ButtonCheck.UnCheck);
                        EditBtCheck(ButtonCheck.Check);
                        AutoBtCheck(ButtonCheck.UnCheck);
                        break;

                    case MAINFormCategory.Auto:
                        ManualBtCheck(ButtonCheck.UnCheck);
                        MDIBtCheck(ButtonCheck.UnCheck);
                        EditBtCheck(ButtonCheck.UnCheck);
                        AutoBtCheck(ButtonCheck.Check);
                        break;

                }
            }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
        }
        private void MainFormLayerMove(MAINFormCategory cat)
        {
            if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
            {
                switch (cat)
                {
                    case MAINFormCategory.Manual:
                        MANUAL.Visible = true;
                        MDIAUTO.Visible = false;
                        EDIT.Visible = false;
                        break;

                    case MAINFormCategory.Auto:
                    case MAINFormCategory.MDI:
                        MANUAL.Visible = false;
                        MDIAUTO.Visible = true;
                        EDIT.Visible = false;
                        break;

                    case MAINFormCategory.Edit:
                        MANUAL.Visible = false;
                        MDIAUTO.Visible = false;
                        EDIT.Visible = true;
                        break;

                }
            }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
        }
        private void MainFormEventChange(MAINFormCategory cat)
        {
            if (null == ApiMonitor) return;
                switch (cat)
            {
                case MAINFormCategory.Manual:
                    ApiMonitor.StatusMonitoringEvent -= EDIT.StatusMonitoring;
                    ApiMonitor.StatusMonitoringEvent -= MDIAUTO.StatusMonitoring;
                    if (ApiMonitor.StatusMonitoringEventCtChk() < 2)
                    {
                        ApiMonitor.StatusMonitoringEvent += MANUAL.StatusMonitoring;
                    }
                    break;

                case MAINFormCategory.Auto:
                case MAINFormCategory.MDI:
                    ApiMonitor.StatusMonitoringEvent -= EDIT.StatusMonitoring;
                    ApiMonitor.StatusMonitoringEvent -= MANUAL.StatusMonitoring;
                    if(ApiMonitor.StatusMonitoringEventCtChk() < 2)
                    {
                        ApiMonitor.StatusMonitoringEvent += MDIAUTO.StatusMonitoring;
                    }
                    break;

                case MAINFormCategory.Edit:
                    ApiMonitor.StatusMonitoringEvent -= MANUAL.StatusMonitoring;
                    ApiMonitor.StatusMonitoringEvent -= MDIAUTO.StatusMonitoring;
                    if (ApiMonitor.StatusMonitoringEventCtChk() < 2)
                    {
                        ApiMonitor.StatusMonitoringEvent += EDIT.StatusMonitoring;
                    }
                    break;
                    
            }
        }
        private void _IOModeCheck()
        {
            if ((true == ShownIOForm) && (null != ApiMonitor))
            {
                ApiMonitor.Mode = MonitoringMode.IOChk;
            }
        }
        private void _ReadProcessStatus(StatusMonitorEventArgs e, McTaskModes mode = McTaskModes.NotSupported)
        {
            //引数にモード指定があれば、指定モードで実行する。
            McTaskModes taskModeTemp = (mode == McTaskModes.NotSupported)? e.Items.ProcessMode : mode;
            switch(taskModeTemp)
            {
                case McTaskModes.Manual:
                    //電流棒グラフ表示
                    //電圧棒グラフ表示
                    //電流値が「０」である場合
                    if (e.Items.MacAxisPos.AxisW == 0)
                    {
                        //電圧値が「０」である場合
                        if (e.Items.WorkAxisPos.AxisW == 0)
                        {
                            if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                            {
                                //棒グラフに、「0(電流),0(電圧)」を表示する
                                MANUAL.SetLevelGageValue(0, 0);
                            }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                        }
                        //電圧値が「０」以外である場合
                        else
                        {
                            if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                            {
                                //棒グラフに、「0(電流),実値(電圧)」を表示する
                                MANUAL.SetLevelGageValue(0, e.Items.WorkAxisPos.AxisW / 1000);
                            }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                        }
                    }
                    //電流値が「０」以外である場合
                    else
                    {
                        //電圧値が「０」である場合
                        if (e.Items.WorkAxisPos.AxisW == 0)
                        {
                            if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                            {
                                //棒グラフに、「実値(電流),0(電圧)」を表示する
                                MANUAL.SetLevelGageValue(e.Items.MacAxisPos.AxisW / 1000, 0);
                            }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                        }
                        //電圧値が「０」以外である場合
                        else
                        {
                            if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                            {
                                //棒グラフに、「実値(電流),実値(電圧)」を表示する
                                MANUAL.SetLevelGageValue(e.Items.MacAxisPos.AxisW / 1000, e.Items.WorkAxisPos.AxisW / 1000);
                            }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                        }
                    }
                    break;

                case McTaskModes.Auto:
                    //電流棒グラフ表示
                    //電圧棒グラフ表示
                    //電流値が「０」である場合
                    if (e.Items.MacAxisPos.AxisW == 0)
                    {
                        //電圧値が「０」である場合
                        if (e.Items.WorkAxisPos.AxisW == 0)
                        {
                            if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                            {
                                //棒グラフに、「0(電流),0(電圧)」を表示する
                                MDIAUTO.SetLevelGageValue(0, 0);
                            }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                        }
                        //電圧値が「０」以外である場合
                        else
                        {
                            if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                            {
                                //棒グラフに、「0(電流),実値(電圧)」を表示する
                                MDIAUTO.SetLevelGageValue(0, e.Items.WorkAxisPos.AxisW / 1000);
                            }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                        }
                    }
                    //電流値が「０」以外である場合
                    else
                    {
                        //電圧値が「０」である場合
                        if (e.Items.WorkAxisPos.AxisW == 0)
                        {
                            if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                            {
                                //棒グラフに、「実値(電流),0(電圧)」を表示する
                                MDIAUTO.SetLevelGageValue(e.Items.MacAxisPos.AxisW / 1000, 0);
                            }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                        }
                        //電圧値が「０」以外である場合
                        else
                        {
                            if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                            {
                                //棒グラフに、「実値(電流),実値(電圧)」を表示する
                                MDIAUTO.SetLevelGageValue(e.Items.MacAxisPos.AxisW / 1000, e.Items.WorkAxisPos.AxisW / 1000);
                            }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                        }
                    }
                    break;
            }
        }
        /// <summary>
        /// 自動モード時、手動画面以外に画面を切り替えた場合の処理
        /// </summary>
        private void _ChangeAutoModeFunction()
        {
            if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
            {
                //ボタン表示切替とMDI/AUTO機能切替のみ実行する。
                switch (Category)
                {
                    case MAINFormCategory.MDI: if (MDIFormBt.GetBack() == true) return; break;
                    case MAINFormCategory.Edit: if (EDITFormBt.GetBack() == true) return; break;
                    case MAINFormCategory.Auto: if (AUTOFormBt.GetBack() == true) return; break;
                }
                ModeButtonChenge(Category);
                MDIAUTO.ModeChg(Category);
            }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
        }
        private void _ReqGuideThroughEnable(StatusMonitorEventArgs e)
        {
            if (e.Items.GuideThroughEnable == true)
            {
                
                if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                {
                    using (MessageDialog msgDir = new MessageDialog())
                    {
                        using (McReqGuideThroughEnabled reqGuideThroughEn = new McReqGuideThroughEnabled())
                        {
                            reqGuideThroughEn.Enabled = (msgDir.Question(530, MANUAL) == true);
                            reqGuideThroughEn.Execute();
                        }
                    }
                }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);                
            }
        }
        /// <summary>
        /// プログラム運転開始
        /// </summary>
        /// <param name="e"></param>
        private void _ReqProgramStart(StatusMonitorEventArgs e)
        {
            if (StartBtOffEdge == true)
            {
                if (e.Items.FGEnd == true && e.Items.SequenceEnd == true)
                {
                    if (MDIFormBt.GetBack() == true)
                    {
                        if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                        {
                            //相対パス"..\YYMMDDEDITING.PGM"の絶対パスを取得する
                            string fileName = DateTime.Today.ToString("yyyyMMdd") + "MDI" + ".PGM";
                            MDIAUTO.SaveProgram(fileName);
                            Thread.Sleep(1000);
                            //MDIモードの際、スタートボタンでプログラムをMCに転送。
                            MDIAUTO.ResistProgram();
                            Thread.Sleep(1000);
                        }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                    }
                    string SelectNumber = "", Value = "";
                    using (SequenceFunction SeqFunc = new SequenceFunction())
                    {
                        SelectNumber = SeqFunc.SequenceMonitoring(Sequences.ProgramSelect).ToString();
                        SeqFunc.SeqMode = -1;//MDIAUTO.Options.StartNo;
                        Value = SeqFunc.SequenceMonitoring(Sequences.ProgramStart).ToString();
                    }
                    if (Value != ResultCodes.Success.ToString())
                    {
                        UILog ReturnCmdLog = new UILog("MAINForm.MainStatusMonitoring");
                        ReturnCmdLog.Error("SELECTNUMBER_SET (1) = " + SelectNumber + ", " + "RETURN_CMD Result = " + Value);
                    }
                }
                //FG停止中の場合、プログラム運転再開
                else if (e.Items.FGStopped == true)
                {
                    using (SequenceFunction SeqFunc = new SequenceFunction(-1))
                    {
                        ResultCodes Value = SeqFunc.SequenceMonitoring(Sequences.ProgramStart);
                        if (Value != ResultCodes.Success)
                        {
                            UILog ReturnCmdLog = new UILog("MAINForm.MainStatusMonitoring");
                            ReturnCmdLog.Error("PROGSTAT_RESTART (1) = " + ", " + "RETURN_CMD Result = " + Value);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 返送コマンド実行処理
        /// </summary>
        /// <param name="e"></param>
        private void _ReqReturnCommand(StatusMonitorEventArgs e)
        {
            if (e.Items.ReturnCmd == true)
            {
                if (MDIAUTO.RegistOnlyProc == true)
                {
                    using (Models.FileProcessCondition filePcond = new Models.FileProcessCondition())
                    using (McDatProcessConditionTable datPcondTbl = new McDatProcessConditionTable())
                    {
                        filePcond.Read();
                        datPcondTbl.Write(filePcond.Items.Find(x => x.Number == e.Items.ProcCondNum), false);
                    }
                    MDIAUTO.RegistOnlyProc = false;
                }
                ResultCodes Value = ResultCodes.Success;
                using (SettingFunction SetFunc = new SettingFunction())
                {
                    Value = SetFunc.SettingMonitoring(Settings.ReturnCmd);
                }
                if (Value != ResultCodes.Success)
                {
                    UILog ReturnCmdLog = new UILog("MAINForm.MainStatusMonitoring");
                    ReturnCmdLog.Error("RETURN_CMD Result = " + Value);
                }
            }
        }
        private void _DayTimeView()
        {
            if ((this.IsDisposed == true || this.Disposing == true)) return;
            if (DateTime.Now.ToString() != DateTimeView.Text)
            {
                if ((this.IsDisposed == true || this.Disposing == true)) return;
                retInvoke = BeginInvoke((MethodInvoker)delegate
                {
                    DateTimeView.Text = DateTime.Now.ToString();
                    switch (ApiMonitor.Items.ProcessMode)
                    {
                        case McTaskModes.Manual:
                            //プロット表示
                            if (ApiMonitor.Items.WorkAxisPos.AxisZ == 0)
                            {
                                MANUAL.SetPlotInfo("Zaxis", 0);
                            }
                            else
                            {
                                MANUAL.SetPlotInfo("Zaxis", ApiMonitor.Items.WorkAxisPos.AxisZ / 1000);
                            }
                            break;

                        case McTaskModes.Auto:
                            //プロット表示
                            if (ApiMonitor.Items.WorkAxisPos.AxisZ == 0)
                            {
                                MDIAUTO.SetPlotInfo("Zaxis", 0);
                            }
                            else
                            {
                                MDIAUTO.SetPlotInfo("Zaxis", ApiMonitor.Items.WorkAxisPos.AxisZ / 1000);
                            }
                            break;
                    }
                }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
            }
        }
        private void _StatusIconChange(StatusMonitorEventArgs e)
        {
            //	アラーム発生状態判定
            if (true == e.Items.McStatus.HasAlarm)
            {
                if (_btnAlarm.IconType != PictureBoxEx.IconTypes.CriticalError)
                {
                    if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                    {
                        _btnAlarm.IconType = PictureBoxEx.IconTypes.CriticalError;
                    }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                }

            }
            else
            {
                if (_btnAlarm.IconType != PictureBoxEx.IconTypes.Clear)
                {
                    if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                    {
                        _btnAlarm.IconType = PictureBoxEx.IconTypes.Clear;
                    }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                }
            }
            if (null == _beforeMcStatus)
            {
                _beforeMcStatus = new StructureMcDatStatus();   //	比較不要
            }
            if (null != _formAlarm)
            {
                if (true == _formAlarm.Visible)
                {
                    if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate 
                    {
                        _formAlarm.Refresh(e.Items.McStatus);
                    }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                }
            }
            _beforeMcStatus.Copy(e.Items.McStatus);
            //  メンテナンスフラグ状態
            if (true == e.Items.HasMaint)
            {
                if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                {
                    if (_btnMaintenance.Visible != true) _btnMaintenance.Visible = true;
                }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                if (_btnMaintenance.IconType != PictureBoxEx.IconTypes.Warning)
                {
                    if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                    {
                        _btnMaintenance.IconType = PictureBoxEx.IconTypes.Warning;
                    }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                }
            }
            else
            {
                if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                {
                    if (_btnMaintenance.Visible != false) _btnMaintenance.Visible = false;
                }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                if (_btnMaintenance.IconType != PictureBoxEx.IconTypes.Clear)
                {
                    if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                    {
                        _btnMaintenance.IconType = PictureBoxEx.IconTypes.Clear;
                    }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                }
            }
        }
       
        #region ShutDownSequence
        private bool _ShutDownSequence(StatusMonitorEventArgs e)
        {
            if (e.Items.ShutdownSygnal == false) return false;
            try
            {
                AidLog log = new AidLog();
                log.Sure("///////////////// SHUTDOWN START!! /////////////////");
                log.Sure("ParameterBackUp Ready....");
                //パラメータバックアップ処理
                using (McReqTechnoBackUp technoBk = new McReqTechnoBackUp())
                {
                    if (technoBk.Execute() == ResultCodes.Success)
                    {
                        log.Sure("Techno Success!");
                    }
                    else
                    {
                        log.Error("Techno Fail....Retry");
                        if (technoBk.Execute() == ResultCodes.Success)
                        {
                            log.Sure("Techno Success!");
                        }
                        else
                        {
                            log.Error("Techno Fail....End");
                        }
                    }
                }
                log.Sure("MonitoringStop Ready....");
                //イベント処理削除
                ApiMonitor.StatusMonitoringEvent -= MainStatusMonitoring;
                log.Sure("MonitoringStop Success!");
                log.Sure("Mc Shutdown Ready....");
                using (McReqShutDownStart mcShutDownSt = new McReqShutDownStart())
                {
                    //mc側シャットダウン開始
                    if (mcShutDownSt.Execute() == ResultCodes.Success)
                    {
                        log.Sure("Mc Shutdown Success!");
                    }
                    else
                    {
                        log.Error("Mc Shutdown Fail....Retry");
                        if (mcShutDownSt.Execute() == ResultCodes.Success)
                        {
                            log.Sure("Mc Shutdown Success!");
                        }
                        else
                        {
                            log.Error("Mc Shutdown Fail....End");
                        }
                    }
                }
                Application_ApplicationExit(null, null);
                log.Sure("Application Exit Success!");
                log.Sure("Windows Shutdown Ready....");
                //Windowsシャットダウン開始
                System.Diagnostics.Process rtProcess = System.Diagnostics.Process.Start("ECNCShutdown.exe");//EXEと同じフォルダ
                log.Sure("Windows Shutdown Executed....");
                log.Sure("///////////////// SHUTDOWN END!! /////////////////");
            }
            catch (Exception exp)
            {
                ECNC3Exception.FileIOFilter(exp);
                return false;
            }
            return true;
        }
        #endregion
        #region TimerSequence
        private void _MaintTimerHandler()
        {
            if (ShownMaintForm == true)
            {
                if (maintTimerSaved == false)
                {
                    ApiMonitor.MaintTimer.MainPowerTimerStop();
                    ApiMonitor.MaintTimer.onStop();
                    ApiMonitor.MaintTimer.SaveTimer();
                    maintTimerSaved = true;
                }
            }
            else
            {
                if (maintTimerSaved == true)
                {
                    ApiMonitor.MaintTimer.ReadTimer();
                    ApiMonitor.MaintTimer.onStart();
                    ApiMonitor.MaintTimer.MainPowerTimerStart();
                    maintTimerSaved = false;
                }
            }
        }
        private void _ReadTimer(StatusMonitorEventArgs e, McTaskModes mode = McTaskModes.NotSupported)
        {
            McTaskModes taskModeTemp = (mode == McTaskModes.NotSupported) ? e.Items.ProcessMode : mode;
            switch(taskModeTemp)
            {
                case McTaskModes.Manual:
                    //加工時間タイマー
                    if (_oldProcessingTimer != TimerView.GetDischargeTime(McTaskModes.Manual))
                    {
                        ulong Hour, Min, Sec;
                        TimerView.GetDischargeTime(out Hour, out Min, out Sec, McTaskModes.Manual);
                        if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                        {
                            MANUAL.SetTimer(TimerCategory.Discharge, Hour.ToString() + "H " + Min.ToString() + "M " + Sec.ToString() + "S");
                        }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);

                        _oldProcessingTimer = TimerView.GetDischargeTime(McTaskModes.Manual);
                    }
                    if (_oldOneProcessingTimer != TimerView.GetOneProcessingTime(McTaskModes.Manual))
                    {
                        if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                        {
                            MANUAL.SetTimer(TimerCategory.OneProcessingTime, TimerView.GetOneProcessingTime(McTaskModes.Manual).ToString("f1") + "S");
                        }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                        _oldOneProcessingTimer = TimerView.GetOneProcessingTime(McTaskModes.Manual);
                    }
                    //プログラム運転タイマー
                    if (_oldProgramTimer != TimerView.GetProgramProcessingTime(McTaskModes.Manual))
                    {
                        ulong Hour, Min, Sec;
                        TimerView.GetProgramProcessingTime(out Hour, out Min, out Sec, McTaskModes.Manual);
                        if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                        {
                            MANUAL.SetTimer(TimerCategory.ProgramTime, Hour.ToString() + "H " + Min.ToString() + "M " + Sec.ToString() + "S");
                        }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                        _oldProgramTimer = TimerView.GetProgramProcessingTime(McTaskModes.Manual);
                    }
                    break;
                    
                case McTaskModes.Auto:
                    //加工時間タイマー
                    if (_oldProcessingTimer != TimerView.GetDischargeTime(McTaskModes.Auto))
                    {
                        ulong Hour, Min, Sec;
                        TimerView.GetDischargeTime(out Hour, out Min, out Sec, McTaskModes.Auto);
                        if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                        {
                            MDIAUTO.SetTimer(TimerCategory.Discharge, Hour.ToString() + "H " + Min.ToString() + "M " + Sec.ToString() + "S");
                        }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                        _oldProcessingTimer = TimerView.GetDischargeTime(McTaskModes.Auto);
                    }
                    if (_oldOneProcessingTimer != TimerView.GetOneProcessingTime(McTaskModes.Auto))
                    {
                        if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                        {
                            MDIAUTO.SetTimer(TimerCategory.OneProcessingTime, TimerView.GetOneProcessingTime(McTaskModes.Auto).ToString("f1") + "S");
                        }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                        _oldOneProcessingTimer = TimerView.GetOneProcessingTime(McTaskModes.Auto);
                    }
                    //プログラム運転タイマー
                    if (_oldProgramTimer != TimerView.GetProgramProcessingTime(McTaskModes.Auto))
                    {
                        ulong Hour, Min, Sec;
                        TimerView.GetProgramProcessingTime(out Hour, out Min, out Sec, McTaskModes.Auto);
                        if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                        {
                            MDIAUTO.SetTimer(TimerCategory.ProgramTime, Hour.ToString() + "H " + Min.ToString() + "M " + Sec.ToString() + "S");
                        }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                        _oldProgramTimer = TimerView.GetProgramProcessingTime(McTaskModes.Auto);
                    }
                    break;
            }
        }

        #endregion
        
        private void OnNotifyReturnFromAlarm()
		{
			if( null != _formAlarm ) {
				_formAlarm.Close();
				_formAlarm = null;
			}
		}
		
        private void ManualFormVisible()
        {
            MANUAL.Visible = true;
        }

		/// <summary>アプリケーションログを開く</summary>
		/// <param name="sender">イベント送信元オブジェクト</param>
		/// <param name="e">イベントパラメータ</param>
		private void _menuOpenAppLog_Click( object sender, EventArgs e )
		{
			ECNC3.Models.Common.ECNC3Log logs = new Models.Common.ECNC3Log( "OPEN APP. LOG" );
			string path = logs.OutputFilePath;
			System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
			try {
				psi.FileName = @"notepad.exe";
				psi.Arguments = path;
				System.Diagnostics.Process.Start( psi );
			} catch( Exception ex ) {
				logs.Exception( ex );
				MessageBox.Show( $"{psi.Arguments}" + Environment.NewLine
								+ Environment.NewLine
								+ $"{ex.Message}",
								"Fail to Application start.", MessageBoxButtons.OK, MessageBoxIcon.Error );
			}
		}

		private void _btnAlarm_Click( object sender, EventArgs e )
		{
			//pictureBoxEx1
			if( null != _beforeMcStatus ) {
				if( true == _beforeMcStatus.HasAlarm ) {
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
                            _formAlarm.Refresh(_beforeMcStatus);
                        }
                    }
                }
			}
        }
        private void _btnMaintenance_Click(object sender, EventArgs e)
        {
            if (PictureBoxEx.IconTypes.Warning == _btnMaintenance.IconType)
            {
                if (null == _formMaintenance)
                {
                    ApiMonitor.MaintTimer.SaveTimer();
                    _formMaintenance = new MaintenanceForm();
                    _formMaintenance.Show(this);
                }
            }
        }
        /// <summary>アプリケーションの終了イベント</summary>
		/// <param name="sender">イベント送信元オブジェクト</param>
		/// <param name="e">イベントパラメータ</param>
		private void _menuClose_Click(object sender, EventArgs e)
        {
            Application_ApplicationExit(null, null);
            Thread.Sleep(1000);
            _notifyIcon.Visible = false;
            Environment.Exit(0);
        }

        private void _menuActivate_Click( object sender, EventArgs e )
		{
			Activate();
		}

        private void MANUALFormBt_Click(object sender, EventArgs e)
        {
            //破棄処理後or初期化前の場合処理を抜ける。
            if ((null == MANUAL) || (null == MDIAUTO) || (null == EDIT) || (null == ApiMonitor)) return;
            if ((EDIT.Visible == true)
                && (EDIT.CompareProgram() == false))
            {
                switch (_MessageShow(MessageBoxIcon.Question, 5044))
                {
                    case DialogResult.Yes: EDIT.ProgramSaveDialog(); break;
                    case DialogResult.No: break;
                }
            }
            ApiMonitorTask.monitorMx.WaitOne();
            using (Models.McIf.McReqModeChange ModeChg = new Models.McIf.McReqModeChange())
            {
                ModeChg.TaskMode = Enumeration.McTaskModes.Manual;
                ResultCodes Error = ModeChg.Execute();
                if(Error != ResultCodes.Success)
                {
                    //ログ処理
                    UILog logs = new UILog("MAINForm.MANUALFormBt_CheckedChanged");
                    logs.Operate("ModeChg_Manual" + " Value = " + Error.ToString());
                    logs.Debug("MODECHG-MANUAL");
                }
                //モード変更
                else Category = MAINFormCategory.Manual;
            }
            ApiMonitorTask.monitorMx.ReleaseMutex();
        }

        private void MDIFormBt_Click(object sender, EventArgs e)
        {
            //破棄処理後or初期化前の場合処理を抜ける。
            if ((null == MANUAL) || (null == MDIAUTO) || (null == EDIT) || (null == ApiMonitor)) return;
            if (MANUAL.RefForm != null) MANUAL.RefForm.EscapeFlag = true;
            MDIAUTO.ModeChg(MAINFormCategory.MDI);
            if ((EDIT.Visible == true)
            && (EDIT.CompareProgram() == false))
            {
                switch (_MessageShow(MessageBoxIcon.Question, 5044))
                {
                    case DialogResult.Yes: EDIT.ProgramSaveDialog(); break;
                    case DialogResult.No: break;
                }
            }

            ApiMonitorTask.monitorMx.WaitOne();
            using (Models.McIf.McReqModeChange ModeChg = new Models.McIf.McReqModeChange())
            {
                ModeChg.TaskMode = Enumeration.McTaskModes.Auto;
                ResultCodes Error = ModeChg.Execute();
                if (Error != ResultCodes.Success)
                {
                    //ログ処理
                    UILog logs = new UILog("MAINForm.MDIFormBt_CheckedChanged");
                    logs.Operate("ModeChg_Auto" + " Value = " + Error.ToString());
                    logs.Debug("MODECHG-AUTO");
                }
                //モード変更
                else Category = MAINFormCategory.MDI;
            }
            ApiMonitorTask.monitorMx.ReleaseMutex();
        }

        private void EDITFormBt_Click(object sender, EventArgs e)
        {
            //破棄処理後or初期化前の場合処理を抜ける。
            if ((null == MANUAL) || (null == MDIAUTO) || (null == EDIT) || (null == ApiMonitor)) return;
            //モード変更
            Category = MAINFormCategory.Edit;
        }

        private void AUTOFormBt_Click(object sender, EventArgs e)
        {
            //破棄処理後or初期化前の場合処理を抜ける。
            if ((null == MANUAL) || (null == MDIAUTO) || (null == EDIT) || (null == ApiMonitor)) return;
            if (MANUAL.RefForm != null) MANUAL.RefForm.EscapeFlag = true;
            if ((EDIT.Visible == true)
            && (EDIT.CompareProgram() == false))
            {
                switch (_MessageShow(MessageBoxIcon.Question, 5044))
                {
                    case DialogResult.Yes: EDIT.ProgramSaveDialog(); break;
                    case DialogResult.No: break;
                }
            }

            ApiMonitorTask.monitorMx.WaitOne();
            using (Models.McIf.McReqModeChange ModeChg = new Models.McIf.McReqModeChange())
            {
                ModeChg.TaskMode = Enumeration.McTaskModes.Auto;
                ResultCodes Error = ModeChg.Execute();
                if (Error != ResultCodes.Success)
                {
                    //ログ処理
                    UILog logs = new UILog("MAINForm.AUTOFormBt_CheckedChanged");
                    logs.Operate("ModeChg_Auto" + " Value = " + Error.ToString());
                    logs.Debug("MODECHG-AUTO");
                }
                else
                {
                    //モード変更
                    Category = MAINFormCategory.Auto;
                    //プログラム文字列の設定
                    string path = EDIT.GetProgramFromEdit();
                    if(path != "")
                    {
                        MDIAUTO.SetProgramTexts(path);
                    }                        
                }
            }
            ApiMonitorTask.monitorMx.ReleaseMutex();
        }
		///2017-1-12：柏原
		/// <summary>テンキーダイアログ画面</summary>
		private TenKeyDialog _formTenKeyDialog = null;
		//イベント
		private void OnNotifyReturnFromTenKeyDialog()
		{
			if( null != _formTenKeyDialog ) {
				_formTenKeyDialog.Close();//画面閉じる
				_formTenKeyDialog = null;
			}
		}

		private void MAINForm_FormClosing( object sender, FormClosingEventArgs e )
		{
			ApiMonitorTask.monitorMx.WaitOne();
		}
	}
}
