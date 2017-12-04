///////////////////////////////////////////////////////////////////////////////////////////
//
// (1) システム名称 : ELENIX ECNCⅢ
// (2) ファイル名   : MakerServiceForm.cs
// (3) 概要         : メーカーサービス画面
// (4) 作成日       : 2015.06.01
// (5) 作成者       : 八野
// (6) 特記事項     : VisualStudio2015 C#
// (7) 更新履歴     :
//
///////////////////////////////////////////////////////////////////////////////////////////


using ECNC3.Enumeration;
using ECNC3.Models;
using ECNC3.Models.McIf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ECNC3.Views
{
    public partial class MakerServiceForm : ECNC3Form
    {
        #region Constractor
        //-----------------------------------------------------------------------------------------
        //
        //　フォーム　コンストラクタ
        //
        //-----------------------------------------------------------------------------------------
        public MakerServiceForm()
        {
            InitializeComponent();
            Disposed += MakerServiceForm_Disposed;
        }
        #endregion
        #region Initialize
        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <returns>
        /// Success：成功
        /// その他：失敗
        /// </returns>
        private ResultCodes Initialize()
        {
            ResultCodes retResult = ResultCodes.Success;
            //フォーム内コントロールの初期化処理
            if (retResult == ResultCodes.Success) retResult = ControlsInit();

            return retResult;
        }
        /// <summary>
        /// フォーム内コントロールの初期化処理
        /// </summary>
        /// <returns>
        /// Success：成功
        /// その他：失敗
        /// </returns>
        private ResultCodes ControlsInit()
        {
            ResultCodes ret = ResultCodes.Success;
            //タブコントロール初期化
            if (ret == ResultCodes.Success) ret = FunctionTabInit();
            this.OutLineEnable = true;
            this.OutLineColor = FileUIStyleTable.SelectedLineColor;
            this.OutLineSize = 3;
            return ret;
        }

        /// <summary>
        /// タブコントロールの初期化処理
        /// </summary>
        /// <returns>
        /// Success：成功
        /// その他：失敗
        /// </returns>
        private ResultCodes FunctionTabInit()
        {
            ResultCodes ret = ResultCodes.Success;

            //タブテキスト設定
            _FunctionChg_TabPage.Text = "機能切替";
            _EnableAxisChgTabPage.Text = "有効軸切替";
            _SpecificationsChg_TabPage.Text = "機械仕様";
            _TimerResetTabPage.Text = "積算時間リセット";
            _ToolTabPage.Text = "ツール";
            _SystemFileTabPage.Text = "システムファイル";
            //タブを左側に表示する
            _FunctionTab.Alignment = TabAlignment.Right;
            //タブのサイズを固定する
            _FunctionTab.SizeMode = TabSizeMode.Fixed;
            _FunctionTab.ItemSize = new Size(50, 150);
            //TabControlをオーナードローする
            _FunctionTab.DrawMode = TabDrawMode.OwnerDrawFixed;
            //DrawItemイベントハンドラを追加
            _FunctionTab.DrawItem += new DrawItemEventHandler(FunctionTab_DrawItem);
            foreach (TabPage page in _FunctionTab.TabPages)
            {
                //タブテキストのフォント設定
                page.Font = new Font("Meiryo UI", 12, FontStyle.Regular);
                //未選択色設定
                page.BackColor = FileUIStyleTable.DefaultBackColor;
                page.ForeColor = FileUIStyleTable.DefaultForeColor;
            }
            //選択色設定
            _FunctionTab.SelectedTab.BackColor = Color.FromArgb(25, 25, 0);
            _FunctionTab.SelectedTab.ForeColor = FileUIStyleTable.OutLineColor;
            return ret;
        }

        #endregion
        #region Dispose
        /// <summary>
        /// 破棄処理（Disposeイベント時処理）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MakerServiceForm_Disposed(object sender, EventArgs e)
        {
            if (null != MaintForm)
            {
                MaintForm.Close();
                MaintForm = null;
            }
            if (null != PassChange)
            {
                PassChange.Close();
                PassChange = null;
            }
            if (null != TimeAdjForm)
            {
                TimeAdjForm.Close();
                TimeAdjForm = null;
            }
            if (null != SysFileInForm)
            {
                SysFileInForm.Close();
                SysFileInForm = null;
            }
            if (null != PitSetForm)
            {
                PitSetForm.Close();
                PitSetForm = null;
            }
            if (null != GSFForm)
            {
                GSFForm.Close();
                GSFForm = null;
            }
            if (null != ESFForm)
            {
                ESFForm.Close();
                ESFForm = null;
            }
            if (null != AlarmLog)
            {
                AlarmLog.Close();
                AlarmLog = null;
            }
            if (null != MacroSetForm)
            {
                MacroSetForm.Close();
                MacroSetForm = null;
            }
            if (null != SysParamForm)
            {
                SysParamForm.Close();
                SysParamForm = null;
            }
            if (null != _notifyReturn)
            {
                _notifyReturn = null;
            }
        }
        #endregion
        #region VariableMember
        private MaintenanceForm MaintForm;
        private PasswordChangeForm PassChange;
        private SystemTimeAdjustForm TimeAdjForm;
        private SystemFileInputForm SysFileInForm;
        private PitchSettingForm PitSetForm;
        private GSFMainForm GSFForm;
        private ESFMainForm ESFForm;
        private AlarmLogForm AlarmLog;
        private MacroVarSetForm MacroSetForm;
        private ParameterViewForm SysParamForm;

        /// <summary>設定結果取得関数定義</summary>
        public delegate void NotifyReturnDelegate();
        /// <summary>終了通知</summary>
        private NotifyReturnDelegate _notifyReturn = null;
        /// <summary>>設定結果取得</summary>
        public NotifyReturnDelegate NotifyReturn
        {
            set { _notifyReturn = value; }
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
        /// <summary>
        /// 装置状態監視用イベント
        /// </summary>
        internal event StatusMonitoringEventHandler StatusMonitoringEvent;
        /// <summary>
        /// IO状態監視用イベント
        /// </summary>
        internal event IOMonitoringEventHandler IOStatusMonitoringEvent;
        #endregion
        #region Monitoring
        /// <summary>
        /// 装置状態監視用イベント処理
        /// </summary>
        /// <param name="e"></param>
        internal void StatusMonitoring(StatusMonitorEventArgs e)
        {
            if (StatusMonitoringEvent != null)
            {
                StatusMonitoringEvent(e);
            }
            if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate {
                //ESFボタンランプ表示
                if (e.Items.EsfEn != _esfEnBt.GetLed()) _esfEnBt.SetLed(e.Items.EsfEn);
                //ESFボタンランプ表示
                if (e.Items.GsfEn != _gsfEnBt.GetLed()) _gsfEnBt.SetLed(e.Items.GsfEn);
                //ESFボタンランプ表示
                if (e.Items.Sf02En != _sf02Bt.GetLed()) _sf02Bt.SetLed(e.Items.Sf02En);
            }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
        }
        /// <summary>
        /// IO状態監視用イベント処理
        /// </summary>
        /// <param name="e"></param>
        internal void IOStatusMonitoring(IOMonitorEventArgs e)
        {

            if (IOStatusMonitoringEvent != null)
            {
                IOStatusMonitoringEvent(e);
            }
        }
        #endregion
        #region EventHandler
        //-----------------------------------------------------------------------------------------
        //
        //　フォーム　ロード時のイベントハンドラ
        //
        //-----------------------------------------------------------------------------------------
        private void MakerServiceForm_Load(object sender, EventArgs e)
        {
            //初期化処理
            Initialize();
        }
        /// <summary>
        /// タブのオーナードロー処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FunctionTab_DrawItem(object sender, DrawItemEventArgs e)
        {
            foreach (TabPage drawPage in _FunctionTab.TabPages)
            {
                drawPage.BackColor = FileUIStyleTable.DefaultBackColor;
                drawPage.ForeColor = FileUIStyleTable.DefaultForeColor;
            }
            _FunctionTab.SelectedTab.BackColor = Color.FromArgb(25, 25, 0);
            _FunctionTab.SelectedTab.ForeColor = FileUIStyleTable.OutLineColor;
            //対象のTabControlを取得
            TabControl tab = (TabControl)sender;
            TabPage page = tab.TabPages[e.Index];
            //タブページのテキストを取得
            string txt = page.Text;
          
            //StringFormatを作成
            StringFormat sf = new StringFormat();
            //水平垂直方向の中央に、行が完全に表示されるようにする
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            sf.FormatFlags |= StringFormatFlags.LineLimit;

            //背景の描画
            Brush backBrush = new SolidBrush(page.BackColor);
            e.Graphics.FillRectangle(backBrush, e.Bounds);
            backBrush.Dispose();

            //Textの描画
            Brush foreBrush = new SolidBrush(page.ForeColor);
            e.Graphics.DrawString(txt, page.Font, foreBrush, e.Bounds, sf);
            foreBrush.Dispose();
        }

        //-----------------------------------------------------------------------------------------
        //
        //　閉じる　ボタン　クリック時のイベントハンドラ
        //
        //-----------------------------------------------------------------------------------------
        private void BackUserFunkFormBt_Click(object sender, EventArgs e)
        {
            _notifyReturn?.Invoke();
        }

        //-----------------------------------------------------------------------------------------
        //
        //　メンテナンス設定　ボタン　クリック時のイベントハンドラ
        //
        //-----------------------------------------------------------------------------------------
        private void MaintenanceBt_Click(object sender, EventArgs e)
        {
            MaintForm = new MaintenanceForm();
            MaintForm.Show(this);
        }

        //-----------------------------------------------------------------------------------------
        //
        //　メーカーパスワード変更　ボタン　クリック時のイベントハンドラ
        //
        //-----------------------------------------------------------------------------------------
        private void MakerPassChangeBt_Click(object sender, EventArgs e)
        {
            PassChange = new PasswordChangeForm();
            PassChange.Show(this);
        }

        //-----------------------------------------------------------------------------------------
        //
        //　システム時刻設定　ボタン　クリック時のイベントハンドラ
        //
        //-----------------------------------------------------------------------------------------
        private void SystemTimeSettingBt_Click(object sender, EventArgs e)
        {
            TimeAdjForm = new SystemTimeAdjustForm();
            TimeAdjForm.Show(this);
        }

        //-----------------------------------------------------------------------------------------
        //
        //　システムファイルエクスポート　ボタン　クリック時のイベントハンドラ
        //
        //-----------------------------------------------------------------------------------------
        private void SysExport_Click(object sender, EventArgs e)
        {
            SysFileInForm = new SystemFileInputForm(FileVector.Output, 13);
            SysFileInForm.Show(this);
        }

        //-----------------------------------------------------------------------------------------
        //
        //　システムファイルインポート　ボタン　クリック時のイベントハンドラ
        //
        //-----------------------------------------------------------------------------------------
        private void SysInport_Click(object sender, EventArgs e)
        {
            SysFileInForm = new SystemFileInputForm(FileVector.Input, 13);
            SysFileInForm.Show(this);
        }

        //-----------------------------------------------------------------------------------------
        //
        //　ピッチ補正　ボタン　クリック時のイベントハンドラ
        //
        //-----------------------------------------------------------------------------------------
        private void PitchCompBt_Click(object sender, EventArgs e)
        {
            PitSetForm = new PitchSettingForm();
            PitSetForm.ShowDialog(this);
        }

        //-----------------------------------------------------------------------------------------
        //
        //　GSF設定　ボタン　クリック時のイベントハンドラ
        //
        //-----------------------------------------------------------------------------------------
        private void GSFSetBt_Click(object sender, EventArgs e)
        {
            if (null != GSFForm)
            {
                return;
            }
            GSFForm = new GSFMainForm();
            GSFForm.NotifyReturn = _btnGsf_ClickCallBack;
            StatusMonitoringEvent = GSFForm.StatusMonitoring;
            GSFForm.Show(this);
        }
        private void _btnGsf_ClickCallBack()
        {
            if (null != GSFForm)
            {
                GSFForm.Close();
                GSFForm = null;
            }
            StatusMonitoringEvent = null;
        }

        //-----------------------------------------------------------------------------------------
        //
        //　ESF設定　ボタン　クリック時のイベントハンドラ
        //
        //-----------------------------------------------------------------------------------------
        private void ESFSetBt_Click(object sender, EventArgs e)
        {
            if (null != ESFForm)
            {
                return;
            }
            ESFForm = new ESFMainForm();
            ESFForm.NotifyReturn = _btnEsf_ClickCallBack;
            StatusMonitoringEvent = ESFForm.StatusMonitoring;
            ESFForm.Show(this);
        }
        private void _btnEsf_ClickCallBack()
        {
            if (null != ESFForm)
            {
                ESFForm.Close();
                ESFForm = null;
            }
            StatusMonitoringEvent = null;
        }

        //-----------------------------------------------------------------------------------------
        //
        //　アラームログ　ボタン　クリック時のイベントハンドラ
        //
        //-----------------------------------------------------------------------------------------
        private void AlarmLogBt_Click(object sender, EventArgs e)
        {
            AlarmLog = new AlarmLogForm();
            AlarmLog.Show(this);
        }

        //-----------------------------------------------------------------------------------------
        //
        //　マクロ変数設定　ボタン　クリック時のイベントハンドラ
        //
        //-----------------------------------------------------------------------------------------
        private void MacroVarSetBt_Click_(object sender, EventArgs e)
        {
            MacroSetForm = new MacroVarSetForm();
            MacroSetForm.Show(this);
        }

        private void SysEditBt_Click(object sender, EventArgs e)
        {
            SysParamForm = new ParameterViewForm();
            SysParamForm.FormClosed -= SysParamForm_Closed;
            SysParamForm.FormClosed += SysParamForm_Closed;
            SysParamForm.Show(this);
        }
        private void SysParamForm_Closed(object sender, FormClosedEventArgs e)
        {
            if (SysParamForm != null)
            {
                SysParamForm = null;
            }
        }

        static MakerServiceSettingSoftForm _SoftForm = null;
        /// <summary>
        /// 設定ソフト：表示/非表示
        /// </summary>
        /// <param name="boolShow"></param>
        private void SettingSoft_Show(bool boolShow)
        {
            if (boolShow)
            {//表示
                if (_SoftForm != null) _SoftForm = null;
                _SoftForm = new MakerServiceSettingSoftForm();
                AddOwnedForm(_SoftForm);                          //親フォームと同時に閉じるため連動
                _SoftForm.StartPosition = FormStartPosition.Manual; //表示方法を自動から手動
                _SoftForm.Location = new Point(100, 210);         //押したボタンの上付近　※真ん中：400,200
                                                                  //FormoderStyle=FixedToolWindowで最小/最大ボタン非表示
                _SoftForm.Show();                                   //フォーム：表示
            }
            else
            {
                //非表示
                _SoftForm.Close();                                  //フォーム：閉じる
                _SoftForm.Dispose();                                //フォーム：ディスポーズ  
                _SoftForm = null;                                   //フォーム：NULL
            }
        }
        /// <summary>
        /// 設定ソフト：ボタン：クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEx_SettingSoft_Click(object sender, EventArgs e)
        {
            if (_SoftForm == null)
            {
                //非表示の場合、表示
                SettingSoft_Show(true);
            }
            else if (_SoftForm.IsDisposed)
            {
                SettingSoft_Show(true);
            }
            else if (_SoftForm != null)
            {
                SettingSoft_Show(false);
            }
        }

        /// <summary>
        /// メイン電源ON時間積算タイマリセット
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PowerOnTimeResetBt_Click(object sender, EventArgs e)
        {
            using (MessageDialog msgDia = new MessageDialog())
            {
                if (msgDia.Question(541, this))
                {
                    TimerView.MainPowerTimeReset();
                    msgDia.Information(542, this);
                }
            }
        }
        /// <summary>
        /// 加工電源ON時間積算タイマリセット
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkTimeResetBt_Click(object sender, EventArgs e)
        {
            using (MessageDialog msgDia = new MessageDialog())
            {
                if (msgDia.Question(543, this))
                {
                    TimerView.ProcessingPowerTimeReset();
                    msgDia.Information(544, this);
                }
            }
        }

        private void _sf02Bt_Click(object sender, EventArgs e)
        {
            _SetCommands(_Commands.Sf02bEnable);
            _StatusLoad();
        }

        private void _esfEnBt_Click(object sender, EventArgs e)
        {
            _SetCommands(_Commands.EsfEnable);
            _StatusLoad();
        }

        private void _gsfEnBt_Click(object sender, EventArgs e)
        {
            _SetCommands(_Commands.GsfEnable);
            _StatusLoad();
        }

        private void _thinModeBt_Click(object sender, EventArgs e)
        {
            _SetCommands(_Commands.ThinLineSettingEnable);
            _StatusLoad();
        }
        /// <summary>
        /// MSエクスプローラーを開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Explorer_Click(object sender, EventArgs e)
        {
            //MSエクスプローラーを開く
            try
            {
                System.Diagnostics.Process.Start("EXPLORER.EXE");
            }
            catch(Exception ex)
            {
                ECNC3Exception.ProcessFilter(ex, this, ExceptionHandling.LogOnly);
            }
        }
        private void _OutPutCapEnableBtn_Click(object sender, EventArgs e)
        {
            _SetCommands(_Commands.OutputCapEnable);
            _StatusLoad();
        }

        private void _LanguageSelectEnableBtn_Click(object sender, EventArgs e)
        {
            _SetCommands(_Commands.LanguageSettingEnable);
            _StatusLoad();
        }

        private void _AecAutoSettingEnableBtn_Click(object sender, EventArgs e)
        {
            _SetCommands(_Commands.AecNumberAutoSettingEnable);
            _StatusLoad();
        }
        private void _EsfSensorBtn_Click(object sender, EventArgs e)
        {
            _SetCommands(_Commands.EsfSenserEnable);
            _StatusLoad();
        }
        private void _AxisAEnableBtn_Click(object sender, EventArgs e)
        {

            _SetCommands(_Commands.AxisAEnable);
            _StatusLoad();
        }

        private void _AxisBEnableBtn_Click(object sender, EventArgs e)
        {

            _SetCommands(_Commands.AxisBEnable);
            _StatusLoad();
        }

        private void _AxisCEnableBtn_Click(object sender, EventArgs e)
        {

            _SetCommands(_Commands.AxisCEnable);
            _StatusLoad();
        }
        #endregion
        #region PrivateMethod
        /// <summary>
        /// ボタン状態の更新
        /// </summary>
        private void _StatusLoad()
        {
            using (FileSettings fs = new FileSettings())
            {
                fs.Read();
                _OutPutCapEnableBtn.SetLed(fs.AttrBool("Root/Service/OutputCap", "enbl"));
                _thinModeBt.SetLed(fs.AttrBool("Root/Service/ThinLineSetting", "enbl"));
                _LanguageSelectEnableBtn.SetLed(fs.AttrBool("Root/Service/LangSetting", "enbl"));
                _AecAutoSettingEnableBtn.SetLed(fs.AttrBool("Root/Service/AecAutoSet", "enbl"));
                _EsfSensorBtn.SetLed(fs.AttrBool("Root/Service/EsfSensor", "enbl"));
            }

            using (McDatRomSwitch rom = new McDatRomSwitch())
            {
                rom.Read();
                _AxisAEnableBtn.SetLed(rom.EnableAxisA == true);
                _AxisBEnableBtn.SetLed(rom.EnableAxisB == true);
                _AxisCEnableBtn.SetLed(rom.EnableAxisC == true);
            }
        }

        private enum _Commands
        {
            OutputCapEnable,
            ThinLineSettingEnable,
            LanguageSettingEnable,
            AecNumberAutoSettingEnable,
            EsfEnable,
            EsfSenserEnable,
            GsfEnable,
            Sf02bEnable,
            AxisAEnable,
            AxisBEnable,
            AxisCEnable
        }
        private ResultCodes _SetCommands(_Commands command)
        {
            ResultCodes retResult = ResultCodes.Success;
            switch (command)
            {
                case _Commands.OutputCapEnable: retResult = SetSysParam(command); break;
                case _Commands.ThinLineSettingEnable: retResult = SetSysParam(command); break;
                case _Commands.LanguageSettingEnable: retResult = SetSysParam(command); break;
                case _Commands.AecNumberAutoSettingEnable: retResult = SetSysParam(command); break;
                case _Commands.AxisAEnable: retResult = SetRomParam(command); break;
                case _Commands.AxisBEnable: retResult = SetRomParam(command); break;
                case _Commands.AxisCEnable: retResult = SetRomParam(command); break;
                case _Commands.EsfEnable: ChgESFEnFlg(); break;
                case _Commands.EsfSenserEnable: SetSysParam(command); break;
                case _Commands.GsfEnable: ChgGSFEnFlg(); break;
                case _Commands.Sf02bEnable: ChgSf02EnFlg(); break;
            }
            return retResult;
        }
        private ResultCodes SetSysParam(_Commands command)
        {
            ResultCodes retResult = ResultCodes.Success;
            using (FileSettings fs = new FileSettings())
            {
                retResult = fs.Read();
                if (retResult != ResultCodes.Success) return retResult;
                switch (command)
                {
                    case _Commands.OutputCapEnable: fs.WriteAttr("Root/Service/OutputCap", "enbl", (_OutPutCapEnableBtn.GetLed() == true) ? "0" : "1"); break;
                    case _Commands.ThinLineSettingEnable: fs.WriteAttr("Root/Service/ThinLineSetting", "enbl", (_thinModeBt.GetLed() == true) ? "0" : "1"); break;
                    case _Commands.LanguageSettingEnable: fs.WriteAttr("Root/Service/LangSetting", "enbl", (_LanguageSelectEnableBtn.GetLed() == true) ? "0" : "1"); break;
                    case _Commands.AecNumberAutoSettingEnable: fs.WriteAttr("Root/Service/AecAutoSet", "enbl", (_AecAutoSettingEnableBtn.GetLed() == true) ? "0" : "1"); break;
                    case _Commands.EsfSenserEnable: fs.WriteAttr("Root/Service/EsfSensor", "enbl", (_EsfSensorBtn.GetLed() == true) ? "0" : "1"); break;
                }
                retResult = fs.Write();
            }
            return retResult;
        }
        private ResultCodes SetRomParam(_Commands command)
        {
            ECNC3.Enumeration.ResultCodes writeResult = Enumeration.ResultCodes.Success;
            using (McDatRomSwitch rom = new McDatRomSwitch())
            {
                //RTMC64ECの動作モードを「Setting」にする。
                using (McReqModeChange ReqModeChg = new McReqModeChange())
                {
                    ReqModeChg.TaskMode = Enumeration.McTaskModes.Setting;
                    if (ReqModeChg.Execute() != Enumeration.ResultCodes.Success)
                    {
                        //動作モード変更に失敗したら有効軸切替を行わない。
                        return ResultCodes.McCommErrorBusy;
                    }
                }
                using (Models.McIf.McDatStatus status = new McDatStatus())
                {
                    status.Read();

                    //settingモード待機
                    while (status.Status.MotionMode != Enumeration.McTaskModes.Setting)
                    {
                        status.Read();
                    }
                }
                //実行
                switch (command)
                {
                    case _Commands.AxisAEnable:
                        rom.EnableAxisA = !_AxisAEnableBtn.GetLed();
                        break;

                    case _Commands.AxisBEnable:
                        rom.EnableAxisB = !_AxisBEnableBtn.GetLed();
                        break;

                    case _Commands.AxisCEnable:
                        rom.EnableAxisC = !_AxisCEnableBtn.GetLed();
                        break;

                }
                writeResult = rom.Write();

                if (writeResult == Enumeration.ResultCodes.Success)
                {
                    //RTMC64ECの動作モードを「Manual」にする。
                    using (McReqModeChange ReqModeChg = new McReqModeChange())
                    {
                        ReqModeChg.TaskMode = Enumeration.McTaskModes.Manual;
                        Enumeration.ResultCodes modeManualResult = Enumeration.ResultCodes.NotExecute;
                        int retryCt = 0;
                        //動作モード変更に失敗したらリトライする。
                        while (modeManualResult != Enumeration.ResultCodes.Success)
                        {
                            RetrySequence(retryCt);
                            modeManualResult = ReqModeChg.Execute();
                            retryCt++;
                        }
                    }
                    using (Models.McIf.McDatStatus status = new McDatStatus())
                    {
                        status.Read();
                        int retryCt = 0;
                        //settingモード待機
                        while (status.Status.MotionMode != Enumeration.McTaskModes.Manual)
                        {
                            RetrySequence(retryCt);
                            status.Read();
                            retryCt++;
                        }
                    }
                }
            }
            return writeResult;
        }
        /// <summary>
        /// SF02有効/無効切り替え
        /// </summary>
        private void ChgSf02EnFlg()
        {
            using (McDatInitialPrm datini = new McDatInitialPrm())
            {
                ECNC3.Enumeration.ResultCodes writeResult = Enumeration.ResultCodes.Success;
                //RTMC64ECの動作モードを「Setting」にする。
                using (McReqModeChange ReqModeChg = new McReqModeChange())
                {
                    ReqModeChg.TaskMode = Enumeration.McTaskModes.Setting;
                    if (ReqModeChg.Execute() != Enumeration.ResultCodes.Success)
                    {
                        //動作モード変更に失敗したら行わない。
                        return;
                    }
                }
                using (Models.McIf.McDatStatus status = new McDatStatus())
                {
                    status.Read();

                    //settingモード待機
                    while (status.Status.MotionMode != Enumeration.McTaskModes.Setting)
                    {
                        status.Read();
                    }
                }
                //SF02ﾌﾗｸﾞセット
                datini.Read();
                datini.EnableSF02 = !datini.EnableSF02;
                writeResult = datini.Write();
                if (writeResult == Enumeration.ResultCodes.Success)
                {
                    //RTMC64ECの動作モードを「Manual」にする。
                    using (McReqModeChange ReqModeChg = new McReqModeChange())
                    {
                        ReqModeChg.TaskMode = Enumeration.McTaskModes.Manual;
                        Enumeration.ResultCodes modeManualResult = Enumeration.ResultCodes.NotExecute;
                        int retryCt = 0;
                        //動作モード変更に失敗したらリトライする。
                        while (modeManualResult != Enumeration.ResultCodes.Success)
                        {
                            RetrySequence(retryCt);
                            modeManualResult = ReqModeChg.Execute();
                            retryCt++;
                        }
                    }
                    using (Models.McIf.McDatStatus status = new McDatStatus())
                    {
                        status.Read();
                        int retryCt = 0;
                        //settingモード待機
                        while (status.Status.MotionMode != Enumeration.McTaskModes.Manual)
                        {
                            RetrySequence(retryCt);
                            status.Read();
                            retryCt++;
                        }
                    }
                    if(datini.EnableSF02 == false) _ProcessConditionInit();
                }
                return;
            }
        }
        private void _ProcessConditionInit()
        {
            using (McDatProcessConditionTable beforeData = new McDatProcessConditionTable())
            using (FileProcessConditionParameter pcondParam = new FileProcessConditionParameter())
            {
                ResultCodes ret = beforeData.Read();
                ret = pcondParam.Read();
                using (McDatInitialPrm datIni = new McDatInitialPrm())
                {
                    ret = datIni.Read();
                    if (ret != ResultCodes.Success) return;
                    bool repairFlag = false;
                    foreach (StructureProcessConditionItem tempItem in beforeData.Items)
                    {
                        if (tempItem.IsValid(datIni.EnableSF02, (int)(pcondParam.Caps.Find(x => x.Number == pcondParam.CapsBit.BoundIndex).Value)) == false)
                        {
                            if (repairFlag == false)
                            {
                                using (MessageDialog msgDia = new MessageDialog())
                                {
                                    if (msgDia.WarningYesNo(5046, this))
                                    {
                                        tempItem.Repair();
                                        beforeData.Write(tempItem, false);
                                        _MessageShow(MessageBoxIcon.Information, 5047);
                                    }
                                    else
                                    {
                                        this.Close();
                                        Application.Exit();
                                    }
                                }
                            }
                            else
                            {
                                if(true == tempItem.Repair()) beforeData.Write(tempItem, false);
                            }
                            repairFlag = true;
                        }
                    }
                }
            }
        }


        /// <summary>
        /// ESF有効/無効切り替え
        /// </summary>
        private void ChgESFEnFlg()
        {
            using (McDatInitialPrm datini = new McDatInitialPrm())
            {
                ECNC3.Enumeration.ResultCodes writeResult = Enumeration.ResultCodes.Success;
                //RTMC64ECの動作モードを「Setting」にする。
                using (McReqModeChange ReqModeChg = new McReqModeChange())
                {
                    ReqModeChg.TaskMode = Enumeration.McTaskModes.Setting;
                    if (ReqModeChg.Execute() != Enumeration.ResultCodes.Success)
                    {
                        //動作モード変更に失敗したら行わない。
                        return;
                    }
                }
                using (Models.McIf.McDatStatus status = new McDatStatus())
                {
                    status.Read();

                    //settingモード待機
                    while (status.Status.MotionMode != Enumeration.McTaskModes.Setting)
                    {
                        status.Read();
                    }
                }
                //ﾌﾗｸﾞセット
                datini.Read();
                datini.EnableEsf = !datini.EnableEsf;
                writeResult = datini.Write();
                if (writeResult == Enumeration.ResultCodes.Success)
                {
                    //RTMC64ECの動作モードを「Manual」にする。
                    using (McReqModeChange ReqModeChg = new McReqModeChange())
                    {
                        ReqModeChg.TaskMode = Enumeration.McTaskModes.Manual;
                        Enumeration.ResultCodes modeManualResult = Enumeration.ResultCodes.NotExecute;
                        int retryCt = 0;
                        //動作モード変更に失敗したらリトライする。
                        while (modeManualResult != Enumeration.ResultCodes.Success)
                        {
                            RetrySequence(retryCt);
                            modeManualResult = ReqModeChg.Execute();
                            retryCt++;
                        }
                    }
                    using (Models.McIf.McDatStatus status = new McDatStatus())
                    {
                        status.Read();
                        int retryCt = 0;
                        //settingモード待機
                        while (status.Status.MotionMode != Enumeration.McTaskModes.Manual)
                        {
                            RetrySequence(retryCt);
                            status.Read();
                            retryCt++;
                        }
                    }
                }
                return;
            }
        }
        /// <summary>
        /// GSF有効/無効切り替え
        /// </summary>
        private void ChgGSFEnFlg()
        {
            using (McDatInitialPrm datini = new McDatInitialPrm())
            {
                ECNC3.Enumeration.ResultCodes writeResult = Enumeration.ResultCodes.Success;
                //RTMC64ECの動作モードを「Setting」にする。
                using (McReqModeChange ReqModeChg = new McReqModeChange())
                {
                    ReqModeChg.TaskMode = Enumeration.McTaskModes.Setting;
                    if (ReqModeChg.Execute() != Enumeration.ResultCodes.Success)
                    {
                        //動作モード変更に失敗したら行わない。
                        return;
                    }
                }
                using (Models.McIf.McDatStatus status = new McDatStatus())
                {
                    status.Read();

                    //settingモード待機
                    while (status.Status.MotionMode != Enumeration.McTaskModes.Setting)
                    {
                        status.Read();
                    }
                }
                //ﾌﾗｸﾞセット
                datini.Read();
                datini.EnableGsf = !datini.EnableGsf;
                writeResult = datini.Write();
                if (writeResult == Enumeration.ResultCodes.Success)
                {
                    //RTMC64ECの動作モードを「Manual」にする。
                    using (McReqModeChange ReqModeChg = new McReqModeChange())
                    {
                        ReqModeChg.TaskMode = Enumeration.McTaskModes.Manual;
                        Enumeration.ResultCodes modeManualResult = Enumeration.ResultCodes.NotExecute;
                        int retryCt = 0;
                        //動作モード変更に失敗したらリトライする。
                        while (modeManualResult != Enumeration.ResultCodes.Success)
                        {
                            RetrySequence(retryCt);
                            modeManualResult = ReqModeChg.Execute();
                            retryCt++;
                        }
                    }
                    using (Models.McIf.McDatStatus status = new McDatStatus())
                    {
                        status.Read();
                        int retryCt = 0;
                        //settingモード待機
                        while (status.Status.MotionMode != Enumeration.McTaskModes.Manual)
                        {
                            RetrySequence(retryCt);
                            status.Read();
                            retryCt++;
                        }
                    }
                }
                return;
            }
        }

        private void RetrySequence(int _retryCt)
        {
            if (_retryCt != 0 && _retryCt <= 10)
            {
                System.Threading.Thread.Sleep(500);
            }
            else if (_retryCt == 0) { }
            else
            {
                //テクノのセッティングPCを立ち上げるか、PCアプリを再起動する
            }
        }
        #endregion

       
    }
}
