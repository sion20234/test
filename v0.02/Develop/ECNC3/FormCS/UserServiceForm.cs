///////////////////////////////////////////////////////////////////////////////////////////
//
// (1) システム名称 : ELENIX ECNCⅢ
// (2) ファイル名   : UserServiceForm.cs
// (3) 概要         : ユーザーサービス設定画面
// (4) 作成日       : 2016.07.19
// (5) 作成者       : 八野
// (6) 特記事項     : VisualStudio2015 C#
// (7) 更新履歴     :
//
///////////////////////////////////////////////////////////////////////////////////////////

using ECNC3.Enumeration;
using ECNC3.Models;
using ECNC3.Models.McIf;
using System;
using System.Drawing;
using System.Windows.Forms;
namespace ECNC3.Views
{
    /// <summary>
    /// ユーザーサービス設定画面
    /// </summary>
    public partial class UserServiceForm : ECNC3Form
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UserServiceForm(bool adminMode)
        {
            InitializeComponent();
			Disposed += UserServiceForm_Disposed;
            _MakerServiceFormPanel.Visible = (adminMode == true) ? true : false;
        }

		private void UserServiceForm_Disposed( object sender, EventArgs e )
		{
            if (null != MaintForm)
            {
                MaintForm.Close();
                MaintForm = null;
            }
            if ( null != TimeSetForm ) {
				TimeSetForm.Close();
				TimeSetForm = null;
			}
			if( null != IOChkForm ) {
				IOChkForm.Close();
				IOChkForm = null;
			}
			if( null != PassChgForm ) {
				PassChgForm.Close();
				PassChgForm = null;
			}
			if( null != ThinSetForm ) {
				ThinSetForm.Close();
				ThinSetForm = null;
			}
			if( null != FileInOutForm) {
                FileInOutForm.Close();
                FileInOutForm = null;
			}
			if( null != GSFSetForm ) {
				GSFSetForm.Close();
				GSFSetForm = null;
			}
			if( null != ESFSetForm ) {
				ESFSetForm.Close();
				ESFSetForm = null;
			}
            if (null != macAxisClear)
            {
                macAxisClear.Close();
                macAxisClear = null;
            }
            if (null != verChkForm)
            {
                verChkForm.Close();
                verChkForm = null;
            }
            if(null != _formMaker)
            {
                _formMaker.Close();
                _formMaker = null;
            }
            if ( null != _notifyReturn ) {
				_notifyReturn = null;
			}
		}
        /// <summary>
        /// 加工条件書き込み保護リスト表示用
        /// </summary>
        FileProcessCondition pcondTable = new FileProcessCondition();
        /// <summary>
        /// 仮想点書き込み保護リスト表示用
        /// </summary>
        FileOrgPos virPosTable = new FileOrgPos();
        /// <summary>
        /// メンテナンスタイマー設定フォーム
        /// </summary>
        private MaintenanceForm MaintForm = null;
        /// <summary>
        /// システム時刻設定フォーム
        /// </summary>
        SystemTimeAdjustForm TimeSetForm = null;
        /// <summary>
        /// システムIO表示フォーム
        /// </summary>
        internal IOCheckForm IOChkForm = null;
        /// <summary>
        /// パスワード変更設定フォーム
        /// </summary>
        PasswordChangeForm PassChgForm = null;
        /// <summary>
        /// 細線モード設定フォーム
        /// </summary>
        ThinLineSettingForm ThinSetForm = null;
        /// <summary>
        /// システムファイル入出力フォーム
        /// </summary>
        SystemFileInputForm FileInOutForm = null;
        /// <summary>
        /// GSF設定フォーム
        /// </summary>
        GSFMainForm GSFSetForm = null;
        /// <summary>
        /// ESF設定フォーム
        /// </summary>
        ESFMainForm ESFSetForm = null;
        /// <summary>
        /// 機械座標原点設定フォーム
        /// </summary>
        AxisClearForm macAxisClear = null;
        /// <summary>
        /// バージョン情報表示フォーム
        /// </summary>
        VersionCheckForm verChkForm = null;
        /// <summary>メーカーサービス画面</summary>
        MakerServiceForm _formMaker = null;


        /// <summary>設定結果取得関数定義</summary>
        public delegate void NotifyReturnDelegate();
		/// <summary>終了通知</summary>
		private NotifyReturnDelegate _notifyReturn = null;
		/// <summary>>設定結果取得</summary>
		public NotifyReturnDelegate NotifyReturn
		{
			set { _notifyReturn = value; }
		}

		public bool ShownIOForm
		{
			get
			{
				if( null != IOChkForm ) {
					return IOChkForm.Visible;
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

        /// <summary>
        /// フォームを閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">BackMANUALFormBtのボタンクリックイベント</param>
        private void BackMANUALFormBt_Click(object sender, EventArgs e)
        {
            //フォームを閉じる
			_notifyReturn?.Invoke();
		}

        /// <summary>
        /// システム時刻変更画面をモーダルで開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">SystemTimeAdjustBtのボタンクリックイベント</param>
        private void SystemTimeAdjustBt_Click(object sender, EventArgs e)
        {
            TimeSetForm = new SystemTimeAdjustForm();                                               
            TimeSetForm.Show(this);                                                                                                      
        }

        /// <summary>
        /// パスワード変更画面をモーダルで開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">PasswordChangeBtのボタンクリックイベント</param>
        private void PasswordChangeBt_Click(object sender, EventArgs e)
        {
            PassChgForm = new PasswordChangeForm();                                                 
            PassChgForm.Show(this);                                                                 
        }

		/// <summary>
		/// システムIO表示画面をモーダルで開く
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">IOCheckBtのボタンクリックイベント</param>
		private void IOCheckBt_Click( object sender, EventArgs e )
		{
			if( null != IOChkForm ) {
				return;
			}
			IOChkForm = new IOCheckForm();
			IOChkForm.NotifyReturn = _btnIO_ClickCallBack;
			IOStatusMonitoringEvent = IOChkForm.IOMonitoring;
			IOChkForm.Show( this );
		}
		private void _btnIO_ClickCallBack()
		{
			if( null != IOChkForm ) {
				IOChkForm.Close();
				IOChkForm = null;
			}
			IOStatusMonitoringEvent = null;
		}

		/// <summary>
		/// 細線モード設定画面をモーダルで開く
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">ThinLineSettingBtのボタンクリックイベント</param>
		private void ThinLineSettingBt_Click(object sender, EventArgs e)
        {
			this.Enabled = false;//親画面：使用不可
            ThinSetForm = new ThinLineSettingForm();
			//ThinSetForm中央表示
			//ここのFormの幅と高さ取得
			int pWidth = this.Size.Width;
			int pHeight = this.Size.Height;
			//MainFormの中心を取得
			int pHarfWidth = pWidth / 2;
			int pHarfHeight = pHeight / 2;
			//この画面の中心を取得
			int harfWidth = ThinSetForm.Width / 2;
			int harfHeight = ThinSetForm.Height / 2;
			//MainFormの中心からこの画面の中心を引いてこの画面を中央表示
			int WidthPos = pHarfWidth - harfWidth;
			int heightPos = pHarfHeight - harfHeight;
			ThinSetForm.Location = new Point( WidthPos, heightPos );//表示位置設定
			ThinSetForm.ShowDialog( this );	//画面表示
			ThinSetForm.Dispose();			//破棄
			this.Enabled = true;			//親画面：使用可
			//ThinSetForm.Show( this );//画面を狭くしたので背面が触れないようShowDialogに変更：柏原
		}

        /// <summary>
        /// システムファイル出力画面をモーダルで開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">SystemFileOutputBtのボタンクリックイベント</param>
        private void SystemFileOutputBt_Click(object sender, EventArgs e)
        {
            FileInOutForm = new SystemFileInputForm(FileVector.Output, 13);
            FileInOutForm.Show(this);                                                               
        }

        /// <summary>
        /// システムファイル入力画面をモーダルで開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">SystemFileInputBtのボタンクリックイベント</param>
        private void SystemFileInputBt_Click(object sender, EventArgs e)
        {
            FileInOutForm = new SystemFileInputForm(FileVector.Input, 13);
            FileInOutForm.Show(this);
        }

        /// <summary>
        /// GSF設定画面をモーダルで開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">GSFSetBtのボタンクリックイベント</param>
        private void GSFSetBt_Click(object sender, EventArgs e)
        {
            if (null != GSFSetForm)
            {
                return;
            }
            GSFSetForm = new GSFMainForm();
            GSFSetForm.NotifyReturn = _btnGsf_ClickCallBack;
            StatusMonitoringEvent = GSFSetForm.StatusMonitoring;
            GSFSetForm.Show(this);
        }
        private void _btnGsf_ClickCallBack()
        {
            if (null != GSFSetForm)
            {
                GSFSetForm.Close();
                GSFSetForm = null;
            }
            StatusMonitoringEvent = null;
        }

        /// <summary>
        /// ESF設定画面をモーダルで開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">ESFSetBtのボタンクリックイベント</param>
        private void ESFSetBt_Click(object sender, EventArgs e)
        {
            if (null != ESFSetForm)
            {
                return;
            }
            ESFSetForm = new ESFMainForm();
            ESFSetForm.NotifyReturn = _btnEsf_ClickCallBack;
            StatusMonitoringEvent = ESFSetForm.StatusMonitoring;
            ESFSetForm.Show(this);
        }
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
            _EnableAxisChg_TabPage.Text = "有効軸切替";
            _LanguageChg_TabPage.Text = "地域";
            _CysCheck_TabPage.Text = "CYS";
            _SystemFile_TabPage.Text = "システムファイル";
            _FileOption_TabPage.Text = "ファイル";
            _MaintenanceOption_TabPage.Text = "メンテナンス";
            _PcondList_TabPage.Text = "加工条件";
            _VirtualPosition_TabPage.Text = "仮想点";
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
        private void _btnEsf_ClickCallBack()
        {
            if (null != ESFSetForm)
            {
                ESFSetForm.Close();
                ESFSetForm = null;
            }
            StatusMonitoringEvent = null;
        }

        private void UserServiceForm_Activated(object sender, EventArgs e)
        {

        }

		private void UserServiceForm_Load( object sender, EventArgs e )
		{
            _AxisAEnableBtn.Enabled = false;
            _AxisBEnableBtn.Enabled = false;
            _AxisCEnableBtn.Enabled = false;
#if __V001_INHIBIT__
#else
			foreach( Control item in this.Controls ) {
				if( ( true == item.Equals( IOCheckBt ) ) || ( true == item.Equals( panel5 ) )
                    || (true == item.Equals(button12))) {
					continue;
				}
				item.Enabled = false;
			}
#endif
            //初期化処理
            Initialize();
            //状態取得
            _StatusLoad();
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
        /// <summary>
        /// ボタン状態の更新
        /// </summary>
        private void _StatusLoad()
        {
            using (FileSettings fs = new FileSettings())
            {
                fs.Read();
                _PcondImportDisableBtn.SetLed(fs.AttrBool("Root/Service/ImportProtect", "pcond"));
                _VirPosImportDisableBtn.SetLed(fs.AttrBool("Root/Service/ImportProtect", "virpos"));
                _FileImportDisableBtn.SetLed(fs.AttrBool("Root/Service/ImportProtect", "file"));
                _ResetDisableBtn.SetLed(fs.AttrBool("Root/Service/Reset", "disbl"));
                _SygnalLampBtn.SetLed(fs.AttrBool("Root/Service/SygnalLampSetting", "enbl"));
                _UnitViewLabel.Text = ((UnitCategory)(fs.AttrValue("Root/Service/LangSetting", "unit"))).ToString();
                _LangageViewLabel.Text = ((Language)(fs.AttrValue("Root/Service/LangSetting", "langage"))).ToString();
            }

            using (McDatRomSwitch rom = new McDatRomSwitch())
            {
                rom.Read();
                _AxisAEnableBtn.SetLed(rom.EnableAxisA == true);
                _AxisBEnableBtn.SetLed(rom.EnableAxisB == true);
                _AxisCEnableBtn.SetLed(rom.EnableAxisC == true);
            }
            foreach(Control ctrl in _CysCheckPanel.Controls)
            {
                //CYSボタンの仕様確認していない為保留
                ctrl.Enabled = false;
            }
            _PcondImportBtn.Enabled = !_PcondImportDisableBtn.GetLed();
            _VirtualPositionImportBtn.Enabled = !_VirPosImportDisableBtn.GetLed();
        }

		/// <summary>
		/// 座標クリア：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_AxisClear_Click( object sender, EventArgs e )
		{
            if (null == macAxisClear)
            {
                StructureAxisCoordinate pos = new Models.StructureAxisCoordinate();
                using (McDatStatus McSta = new Models.McIf.McDatStatus())
                {
                    McSta.Read();
                    pos = (StructureAxisCoordinate)McSta.Status.CoordinateAsAbsReg.Clone();
                }
                macAxisClear = new AxisClearForm(0, pos, true);
                macAxisClear.Show(this);
                macAxisClear.NotifyReturn = macAxisClear_Click;
            }
        }
        private void macAxisClear_Click()
        {
            if (null != macAxisClear)
            {
                macAxisClear.Close();
                macAxisClear = null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _updateBt_Click(object sender, EventArgs e)
        {
            //確認ウインドウ流用
            MessageDialog msgDir = new MessageDialog();
            msgDir.Subject = MessageBoxIcon.Question;
            msgDir.UpdateData(5026);

			string beforeVer = "";
			string afterVer  = "";
			//Ecnc3NeoVersion.xml：現バージョン/更新バージョンの番号：取得
			getVerAndUpdateVer( ref beforeVer ,ref afterVer );
			msgDir.Item.Title = msgDir.Item.Title + "[" + beforeVer + " - " + afterVer + "]";
			//            msgDir.Item.Title = msgDir.Item.Title + "[" + "Ver.2.00 - Ver.3.00" + "]";
			DialogResult result = msgDir.ShowDialog(this);
            //アップデータを起動する。但し確認画面で「いいえ」を選択の場合は起動しない。
            if(result != DialogResult.Yes)
            {
                return;
            }
            try
            {
                if(System.IO.File.Exists("ECNC3Updater.exe"))
                {

                }
                System.Diagnostics.Process p =
                System.Diagnostics.Process.Start("ECNC3Updater.exe");
            }
            catch(Exception ex)
            {
                //例外処理
                string msg = ECNC3Exception.FileIOFilter(ex, this);
                if (msg.Contains("Not") && msg.Contains("Found"))
                {
                    MessageBox.Show("アップデータが見つかりません。");
                }
                return;
            }
            //アップデータ起動後アプリケーションを閉じる。
            Application.Exit();
		}
		/// <summary>
		/// /// Ecnc3NeoVersion.xml：現バージョン/更新バージョンの番号：取得
		/// </summary>
		/// <param name="_beforeVerLabel"></param>
		/// <param name="AfterVerLabel"></param>
		private void getVerAndUpdateVer(ref string beforeVer, ref string afterVer )
		{
			//bool _debugFlg = false;//実機環境
			//自分自身の実行ファイルのパスを取得する
			string appPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
			//"bin"が有ればデバック環境：C:\開発\ECNC3Server\ECNC3\v0.02\Develop\ECNC3\bin\Debug\ECNC3.exe
			//if( appPath.IndexOf( "bin" ) > -1 ) _debugFlg = true;//デバック環境

			string masterFolder = @FilePathInfo.MasterData;
            //20170411DEL↓↓パス設定ファイルで切り替える為
            //if( _debugFlg ) masterFolder = @"C:\開発\ECNC3Server\ECNC3\v0.02\ECNC3\Master\";//デバック時

            //"Ecnc3NeoVersion.xml"からバージョン情報取得
            using ( FileVersionInfo verInfo = new FileVersionInfo( masterFolder ) ) {
				verInfo.Read();
				beforeVer = "Ver." + verInfo.UIReleaseVersion;
			}
			using( FileVersionInfo verInfo = new FileVersionInfo(@FilePathInfo.NeoUpdateData) ) {//1個下の階層
				verInfo.Read();
				afterVer = "Ver." + verInfo.UIReleaseVersion; ;
			}
		}

		private void _versionCheckFormBt_Click(object sender, EventArgs e)
        {
            if (null != verChkForm)
            {
                return;
            }
            verChkForm = new VersionCheckForm();
            verChkForm.NotifyReturn = _btnVer_ClickCallBack;
            verChkForm.ShowDialog(this);
        }
        private void _btnVer_ClickCallBack()
        {
            if (null != verChkForm)
            {
                verChkForm.Close();
                verChkForm = null;
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {

        }

        private void button22_Click(object sender, EventArgs e)
        {

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
        private void _paramChg(EsfSetting setting)
        {
        }
        private enum _Commands
        {
            PcondImportDisable,
            VirPosImportDisable,
            FileImportDisable,
            FileLock,
            ResetDisable,
            SygnalLamp,
            UnitChange,
            LangageChange,
            AxisAEnable,
            AxisBEnable,
            AxisCEnable,
            PcondImport,
            PcondExport,
            VirtualPositionImport,
            VirtualPositionExport
        }
        private ResultCodes _SetCommands(_Commands command)
        {
            ResultCodes retResult = ResultCodes.Success;
            switch (command)
            {
                case _Commands.PcondImportDisable: retResult = SetSysParam(command); break;
                case _Commands.VirPosImportDisable: retResult = SetSysParam(command); break;
                case _Commands.FileImportDisable: retResult = SetSysParam(command); break;
                case _Commands.FileLock:
                    //エクスプローラー風を出す
                    string _receivedPath = FileForm.ShowSubForm(this,FileFormMode.OpenProgram, System.IO.Path.GetFullPath(@"Program"), FileForm.DispTypeForWProtect.UserService);
                    //ResultCodes ret = ResultCodes.Success;

                    break;

                case _Commands.ResetDisable: retResult = SetSysParam(command); break;
                case _Commands.SygnalLamp: retResult = SetSysParam(command); break;
                case _Commands.UnitChange: retResult = SetSysParam(command); break;
                case _Commands.LangageChange: retResult = SetSysParam(command); break;
                case _Commands.AxisAEnable: retResult = SetRomParam(command); break;
                case _Commands.AxisBEnable: retResult = SetRomParam(command); break;
                case _Commands.AxisCEnable: retResult = SetRomParam(command); break;
                case _Commands.PcondImport: retResult = UIStaticMethods.ImportMasterFile("Pdata.xml"); break;
                case _Commands.PcondExport: retResult = UIStaticMethods.ExportMasterFile("Pdata.xml"); break;
                case _Commands.VirtualPositionImport: retResult = UIStaticMethods.ImportMasterFile("OrgPos.xml"); break;
                case _Commands.VirtualPositionExport: retResult = UIStaticMethods.ExportMasterFile("OrgPos.xml"); break;
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
                    case _Commands.PcondImportDisable: fs.WriteAttr("Root/Service/ImportProtect", "pcond", (_PcondImportDisableBtn.GetLed() == true)? "0" : "1" ); break;
                    case _Commands.VirPosImportDisable: fs.WriteAttr("Root/Service/ImportProtect", "virpos", (_VirPosImportDisableBtn.GetLed() == true)? "0" : "1" ); break;
                    case _Commands.FileImportDisable: fs.WriteAttr("Root/Service/ImportProtect", "file", (_FileImportDisableBtn.GetLed() == true)? "0" : "1" ); break;
                    case _Commands.ResetDisable: fs.WriteAttr("Root/Service/Reset", "disbl", (_ResetDisableBtn.GetLed() == true)? "0" : "1" ); break;
                    case _Commands.SygnalLamp: fs.WriteAttr("Root/Service/SygnalLampSetting", "enbl", (_SygnalLampBtn.GetLed() == true)? "0" : "1" ); break;
                    case _Commands.UnitChange:
                        UnitCategory tempUnit = (UnitCategory.mm.ToString() == _UnitViewLabel.Text)? UnitCategory.inch : UnitCategory.mm;
                        _UnitViewLabel.Text = tempUnit.ToString();
                        fs.WriteAttr("Root/Service/LangSetting", "unit", ((int)tempUnit).ToString());
                        break;

                    case _Commands.LangageChange:
                        Language tempLang = (Language.JP.ToString() == _LangageViewLabel.Text) ? Language.EN : Language.JP;
                        _LangageViewLabel.Text = tempLang.ToString();
                        fs.WriteAttr("Root/Service/LangSetting", "langage", ((int)tempLang).ToString());
                        break;
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
        /// 言語切り替え
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        private ResultCodes _LanguageChange(Language language)
        {
            ResultCodes retResult = ResultCodes.Success;
            switch (language)
            {
                case Language.JP: _LangageViewLabel.Text = language.ToString(); break;
                case Language.EN: _LangageViewLabel.Text = language.ToString(); break;
            }
            return retResult;
        }
        /// <summary>
        /// 単位切り替え
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        private ResultCodes _UnitChange(UnitCategory unit)
        {
            ResultCodes retResult = ResultCodes.Success;
            switch (unit)
            {
                case UnitCategory.mm: _LangageViewLabel.Text = unit.ToString(); break;
                case UnitCategory.inch: _LangageViewLabel.Text = unit.ToString(); break;
            }
            return retResult;
        }
        private void _VirPosLockBt_MouseUp(object sender, MouseEventArgs e)
        {
            _VirPosLockBt.SetBack(true);
            ProtectCommandsDialog.ShowSubForm(this, "仮想点保護", ProtectParamCategory.VirtualPositions, _VirPosLockBt, true );
            _VirPosLockBt.SetBack(false);
        }

        private void _PcondTableLockBtn_MouseUp(object sender, MouseEventArgs e)
        {
            _PcondTableLockBtn.SetBack(true);
            ProtectCommandsDialog.ShowSubForm(this, "加工条件保護", ProtectParamCategory.ProcessConditions, _PcondTableLockBtn, true);
            _PcondTableLockBtn.SetBack(false);
        }

        private void _PcondImportDisableBtn_MouseUp(object sender, MouseEventArgs e)
        {
            _SetCommands( _Commands.PcondImportDisable);
            _StatusLoad();
        }

        private void _PcondExportBtn_MouseUp(object sender, MouseEventArgs e)
        {
            (sender as ButtonEx).SetSelected(true);
            using (MessageDialog msgDia = new MessageDialog())
            {
                if (msgDia.Question(554, this))
                {
                    if (_SetCommands(_Commands.PcondExport) == ResultCodes.Success)
                    {
                        msgDia.Information(555, this);
                    }
                    else
                    {
                        msgDia.Error(5039, this);
                    }
                }
            }
            (sender as ButtonEx).SetSelected(false);
            _StatusLoad();
        }

        private void _PcondImportBtn_MouseUp(object sender, MouseEventArgs e)
        {
            (sender as ButtonEx).SetSelected(true);
            using (MessageDialog msgDia = new MessageDialog())
            {
                if (msgDia.Question(551, this))
                {
                    if (_SetCommands(_Commands.PcondImport) == ResultCodes.Success)
                    {
                        msgDia.Information(552, this);
                    }
                    else
                    {
                        msgDia.Error(5038, this);
                    }
                }
            }
            (sender as ButtonEx).SetSelected(false);
            _StatusLoad();
        }

        private void _ResetDisableBtn_MouseUp(object sender, MouseEventArgs e)
        {
            _SetCommands(_Commands.ResetDisable);
            _StatusLoad();
        }

        private void _SygnalLampBtn_MouseUp(object sender, MouseEventArgs e)
        {
            _SetCommands(_Commands.SygnalLamp);
            _StatusLoad();
        }

        private void _UnitChangeBtn_MouseUp(object sender, MouseEventArgs e)
        {
            _SetCommands(_Commands.UnitChange);
            _StatusLoad();
        }

        private void _LangageChangeBtn_MouseUp(object sender, MouseEventArgs e)
        {
            _SetCommands(_Commands.LangageChange);
            _StatusLoad();
        }

        private void _VirPosImportDisableBtn_MouseUp(object sender, MouseEventArgs e)
        {
            _SetCommands(_Commands.VirPosImportDisable);
            _StatusLoad();
        }

        private void _VirtualPositionExportBtn_MouseUp(object sender, MouseEventArgs e)
        {
            (sender as ButtonEx).SetSelected(true);
            using (MessageDialog msgDia = new MessageDialog())
            {
                if (msgDia.Question(559, this))
                {
                    if (_SetCommands(_Commands.VirtualPositionExport) == ResultCodes.Success)
                    {
                        msgDia.Information(560, this);
                    }
                    else
                    {
                        msgDia.Error(5039, this);
                    }
                }
            }
            (sender as ButtonEx).SetSelected(false);
            _StatusLoad();
        }

        private void _VirtualPositionImportBtn_MouseUp(object sender, MouseEventArgs e)
        {
            (sender as ButtonEx).SetSelected(true);
            using (MessageDialog msgDia = new MessageDialog())
            {
                if (msgDia.Question(556, this))
                {
                    if (_SetCommands(_Commands.VirtualPositionImport) == ResultCodes.Success)
                    {
                        msgDia.Information(557, this);
                    }
                    else
                    {
                        msgDia.Error(5038, this);
                    }
                }
            }
            (sender as ButtonEx).SetSelected(false);
            _StatusLoad();
        }

        private void _AxisAEnableBtn_MouseUp(object sender, MouseEventArgs e)
        {
            _SetCommands(_Commands.AxisAEnable);
            _StatusLoad();
        }

        private void _AxisBEnableBtn_MouseUp(object sender, MouseEventArgs e)
        {
            _SetCommands(_Commands.AxisBEnable);
            _StatusLoad();
        }

        private void _AxisCEnableBtn_MouseUp(object sender, MouseEventArgs e)
        {
            _SetCommands(_Commands.AxisCEnable);
            _StatusLoad();
        }

        private void _FileImportDisableBtn_MouseUp(object sender, MouseEventArgs e)
        {
            _SetCommands(_Commands.FileImportDisable);
            _StatusLoad();
        }
        /// <summary>
        /// ファイル：ロックボタン：クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Lockbtn_Click(object sender, EventArgs e)
        {
            _SetCommands(_Commands.FileLock);
            _StatusLoad();
        }

        private void _MakerServiceFormBtn_Click(object sender, EventArgs e)
        {
            if (null == _formMaker)
            {
                _formMaker = new MakerServiceForm();
                StatusMonitoringEvent = _formMaker.StatusMonitoring;
                IOStatusMonitoringEvent = _formMaker.IOStatusMonitoring;
                _formMaker.NotifyReturn = _btn_ClickCallBack;
                _formMaker.Show(this);
            }
        }
        private void _btn_ClickCallBack()
        {
            if (null != _formMaker)
            {
                _formMaker.Close();
                _formMaker = null;
            }
            StatusMonitoringEvent = null;
            IOStatusMonitoringEvent = null;
        }

        private void MaintenanceEditBt_Click(object sender, EventArgs e)
        {
            MaintForm = new MaintenanceForm();
            MaintForm.Show(this);
        }
    }
}
