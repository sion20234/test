///////////////////////////////////////////////////////////////////////////////////////////
//
// (1) システム名称 : ELENIX ECNCⅢ
// (2) ファイル名   : HelpForm.cs
// (3) 概要         : Gコードヘルプ画面
// (4) 作成日       : 2015.06.01
// (5) 作成者       : 八野
// (6) 特記事項     : VisualStudio2015 C#
// (7) 更新履歴     : アドビPDF-LIB関数機能追加 ：2017-06-02：柏原
//                  　Pdfiumに変更              ：2017-10-13：柏原
//
///////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;	//DLL Import
using ECNC3.Models;                     //FileSettings用
using PdfiumViewer;                     //PDFビューワ：オープンソース(googleが開発したPDFのレンダリングエンジン『Pdfium』の.netラッパー)
using System.Threading;

namespace ECNC3.Views
{
    /// <summary>
    /// ヘルプ画面
    /// </summary>
    public partial class HelpForm : ECNC3Form
    {
        #region<初期化>
        /// <summary>ポップアップテンキー</summary>
        private TenKeyDialog _popupTenkey = new TenKeyDialog("", 0, 0, 0);//初回インスタンスを作っておく

        /// <summary>
        /// フォーム　コンストラクタ
        /// </summary>
        public HelpForm()
        {
            InitializeComponent();
        }
        private System.Windows.Forms.Timer watchPageNumber;
        /// <summary>
        /// フォーム　ロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpForm_Load(object sender, EventArgs e)
        {
        }
        private void pdfViewer1_Load(object sender, EventArgs e)
        {
            //PdfiumViewer(フリー)
            PDF_Find__buttonEx.Visible = true; //検索ボタン：表示
            pdfViewer1.ShowToolbar = false;     //ツールバー：非表示
            PDF_TextBoxEx.Visible = true;       //ページ表示
            string Path = Models.FilePathInfo.GCodeHelpFile;
            // PdfDocument オブジェクトの作成。表示したいPDFのパスを指定
            PdfiumViewer.PdfDocument doc = PdfiumViewer.PdfDocument.Load(Path);
            //ビューワーコントロールのDocumentプロパティにセット 
            pdfViewer1.Document = doc;
            //PdfiumViewerのズームイン / アウトとパーセント表示
            SetZoomInOut(100);
            //CncSys.xmlのヘルプファイル：表示ページ位置：読込み
            HelpFileDispPosRead();
            PDF_TrackBar.Visible = false;//非表示
            PDF_Find__buttonEx.Visible = false;//非表示
            textBoxEx_Find.Visible = false;//非表示
            //監視
            watchPageNumber = new System.Windows.Forms.Timer();
            watchPageNumber.Interval = 50;// 100msだと遅い
            watchPageNumber.Tick += watchPageNumberWorker;
            if (isWatchStoped == true) isWatchStoped = false;
            watchPageNumber.Start();
        }
        private int _iFirstPage_GCode = 0;
        private int _iFirstPage_MCode = 0;
        private int _iFirstPage_Contents = 0;
        /// <summary>
        /// CncSys.xmlのヘルプファイル：表示ページ位置：読込み
        /// ※PDFは1ファイル
        /// </summary>
        private void HelpFileDispPosRead()
        {
            try {
                using (FileSettings fs = new FileSettings()) {
                    //ファイル読み込み
                    fs.Read();
                    _iFirstPage_GCode = fs.AttrValue("Root/HelpFile/FirstPage", "gcode"); //Gコードページ
                    _iFirstPage_MCode = fs.AttrValue("Root/HelpFile/FirstPage", "mcode"); //Mコードページ
                    _iFirstPage_Contents = fs.AttrValue("Root/HelpFile/FirstPage", "contents");//目次ページ
                }
            } catch (Exception exc) {
                //例外処理
                MessageBox.Show(exc.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        #region<終了>
        /// <summary>
        /// 戻る：クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Back_ButtonEx_Click(object sender, EventArgs e)
        {
            this.FormClosing -= HelpForm_FormClosing;
            HelpForm_Dispose();
            //自分を閉じる
            this.Close();
        }
        /// <summary>
        /// フォーム：閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpForm_Dispose()
        {
            //ポップアップ・キーボード：閉じる
            if (null != _PopupKeybord)
            {
                this.Controls.Remove(_PopupKeybord);
                _PopupKeybord.Dispose();
                _PopupKeybord = null;
            }
            if (null != _popupTenkey)
            {//ポップアップテンキー
                _popupTenkey = null;
            }
            if (pdfViewer1 != null)
            {
                pdfViewer1.Dispose();
                pdfViewer1 = null;
            }
        }
        private void HelpForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //クローズを一時停止
            e.Cancel = true;
            //ストップ処理
            if(watchStop() == false)return;
            //クローズ処理再開
            e.Cancel = false;
        }
        private void HelpForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //破棄処理
            HelpForm_Dispose();
        }
        #endregion
        #region<ページ番号：監視タスク>
        bool isWatchStoped = true;
        private bool watchStop()
        {
            int ct = 0;
            while(watchPageNumber.Enabled == true)
            {
                if (ct > 20) return false;
                Thread.Sleep(100);
                watchPageNumber.Stop();
                ct++;
            }
            return true;
        }
        /// <summary>
        /// ページ番号監視タスク：一定周期でコール
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void watchPageNumberWorker(object sender, EventArgs e)
        {
            if (isWatchStoped == true || pdfViewer1 == null ) return;
            if ((this.IsDisposed == true || this.Disposing == true)) return;
            int intPage = pdfViewer1.Renderer.Page + 1;//0オリジンなので＋1
            if (isWatchStoped == false) PDF_TextBoxEx.Text = intPage.ToString();//ページ表示
            if(watchPageNumber.Enabled == false)
                if (isWatchStoped == false) isWatchStoped = true;

        }
        /// <summary>
        /// EnumWindowProc
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public delegate bool EnumWindowProc(IntPtr hWnd, IntPtr parameter);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumChildWindows(IntPtr hwndParent, EnumWindowProc lpEnumFunc, IntPtr lParam);

        public delegate bool Win32Callback(IntPtr hwnd, IntPtr lParam);

        [DllImport("user32.Dll")]
        [return: MarshalAs(UnmanagedType.Bool)]

        private static extern bool EnumChildWindows(IntPtr parentHandle, Win32Callback callback, IntPtr lParam);
        /// <summary>
        /// 子ウインド取得
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        private static List<IntPtr> GetChildWindows(IntPtr parent)
        {
            List<IntPtr> result = new List<IntPtr>();
            GCHandle listHandle = GCHandle.Alloc(result);
            try {
                EnumWindowProc childProc = new EnumWindowProc(EnumWindow);
                EnumChildWindows(parent, childProc, GCHandle.ToIntPtr(listHandle));
            } finally {
                if (listHandle.IsAllocated) {
                    listHandle.Free();
                }
            }
            return result;
        }
        /// <summary>
        /// ウインド列挙
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="pointer"></param>
        /// <returns></returns>
        private static bool EnumWindow(IntPtr handle, IntPtr pointer)
        {
            GCHandle gch = GCHandle.FromIntPtr(pointer);
            List<IntPtr> list = gch.Target as List<IntPtr>;
            if (list == null) {
                throw new InvalidCastException("GCHandle Target could not be cast as List<IntPtr>");
            }
            list.Add(handle);
            return true;
        }

        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hwndControl, uint Msg, int wParam, int lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        private static extern int SendMessage_s(IntPtr hwndControl, uint Msg, int wParam, StringBuilder strBuffer);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        private const uint WM_GETTEXT = 0x000D;
        private const uint WM_GETTEXTLENGTH = 0x000E;
        /// <summary>
        /// テキストボックスからテキストを取得
        /// </summary>
        /// <param name="hTextBox"></param>
        /// <returns></returns>
        private static string GetTextBoxText(IntPtr hTextBox)
        {
            var len = SendMessage(hTextBox, WM_GETTEXTLENGTH, 0, 0) + 1;
            var buf = new StringBuilder(len);
            SendMessage_s(hTextBox, WM_GETTEXT, len, buf);
            return buf.ToString();
        }
        /// <summary>
        /// ウインドのクラス名取得
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        private string GetClassNameOfWindow(IntPtr hwnd)
        {
            var buf = new StringBuilder(1024);
            GetClassName(hwnd, buf, buf.MaxCapacity);
            return buf.ToString();
        }
        #endregion
        #region<トラックバー>
        /// <summary>
        /// トラックバー移動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBar1_Move(object sender, EventArgs e)
        {
            float floatVal = PDF_TrackBar.Value;
            SetZoomInOut(PDF_TrackBar.Value);
        }
        /// <summary>
        /// トラックバー：スクロール
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PDF_TrackBar_Scroll(object sender, EventArgs e)
        {
            int TrackValNow = ((TrackBar)sender).Value;
            PDF_ZoomTextBoxEx.Text = ((TrackBar)sender).Value.ToString();
        }
        /// <summary>
        /// トラックバー位置をスクロール位置に変換
        /// </summary>
        /// <param name="trackVal"></param>
        private int changeTrackPosToScPos(int trackVal)
        {
            return trackVal;//
            //return PDF_TrackBar.Maximum - trackVal;//SCと逆
        }
        /// <summary>
        /// 表示倍率：トラックバーの設定：
        /// </summary>
        /// <param name="trackVal"></param>
        /// <returns>スクロールバー位置</returns>	
        private int setTrackbar(int trackVal)
        {
            return PDF_TrackBar.Value = changeTrackPosToScPos(trackVal);//トラックバー位置をスクロール位置に変換
        }
        #endregion
        #region<ページ操作>
        /// <summary>
        /// インデックス：1オリジン値
        /// </summary>
        /// <returns></returns>
        private int pdfViewer1RendererPage()
        {
            return pdfViewer1.Renderer.Page+1;
        }
        /// <summary>
        /// ▲：クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PDF_Button_Up_Click(object sender, EventArgs e)
        {
            var activePageIndex = pdfViewer1.Document.PageCount - 1;//マイナス
            if (activePageIndex > 0)
            {
               PDF_TextBoxEx.Text = pdfViewer1.Renderer.Page.ToString();//ページ表示
               pdfViewer1.Renderer.Page = pdfViewer1.Renderer.Page - 1;
            }
        }
        /// <summary>
        /// ▼：クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PDF_Button_Down_Click( object sender, EventArgs e )
		{
            // 最終頁のインデックス
            var maxPageIndex = pdfViewer1.Document.PageCount;
            // 現在ページのインデックス
            var activePageIndex = pdfViewer1.Renderer.Page + 1;//プラス
            if (activePageIndex <= maxPageIndex)
            {
                PDF_TextBoxEx.Text = (activePageIndex+1).ToString();//ページ表示
                pdfViewer1.Renderer.Page = activePageIndex;
            }
        }
        /// <summary>
        /// 最初のページ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PDF_Button_UpTop_Click( object sender, EventArgs e )
		{
            pdfViewer1.Renderer.Page = 0;
            int activePageIndex = pdfViewer1RendererPage();
            PDF_TextBoxEx.Text = activePageIndex.ToString();//ページ表示
        }
		/// <summary>
		/// 最後のページ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PDF_Button_DownBotom_Click( object sender, EventArgs e )
		{
            pdfViewer1.Renderer.Page = pdfViewer1.Document.PageCount - 1;
            int activePageIndex = pdfViewer1RendererPage();
            PDF_TextBoxEx.Text = activePageIndex.ToString();//ページ表示
        }
        /// <summary>
        /// ページが変更された
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PDF_TextBoxEx_TextChanged( object sender, EventArgs e )
		{
			if( PDF_TextBoxEx.Text == null ) return;
			if( PDF_TextBoxEx.Text == "" ) return;
			int index = int.Parse( PDF_TextBoxEx.Text);

            var minPageIndex = 0;
            if (index < minPageIndex)
            {//最小値超えチェック
                return;
            }
            if (pdfViewer1 == null) return;
            var maxPageIndex = pdfViewer1.Document.PageCount - 1;
            if (index < maxPageIndex)
            {//最大超えチェック
                return;
            }
            pdfViewer1.Renderer.Page= index;
            PDF_TextBoxEx.Text = index.ToString();//ページ表示
        }
        #endregion
        #region<表示倍率>
        /// <summary>
        /// PdfiumViewerのズームイン/アウトとパーセント表示
        /// </summary>
        /// <param name="intVal"></param>
        private void SetZoomInOut(int intVal)
        {
            PDF_ZoomTextBoxEx.Text = intVal.ToString();//表示倍率100パーセントは100
            if (intVal < 10) intVal = 10;
            if (intVal > 500) intVal = 500;
            double devVal = double.Parse(intVal.ToString()) / 100.0;
            //値調整
            devVal = devVal * 2;
            pdfViewer1.Renderer.Zoom = devVal;//１＝100  範囲0.1から5　なので　10から500が有効値
        }

        private void PDF_P_buttonExPush(int intVal)
        {
            pdfViewer1.Renderer.Zoom = intVal;//ap
            PDF_ZoomTextBoxEx.Text = intVal.ToString();
        }
        /// <summary>
        /// 表示倍率：50％
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PDF_50P_buttonEx_Click( object sender, EventArgs e )
		{
            PDF_P_buttonExPush(50);

            //PdfiumViewerのズームイン / アウトとパーセント表示
            SetZoomInOut(50);
        }
		/// <summary>
		/// 表示倍率：100％
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PDF_100P_buttonEx_Click( object sender, EventArgs e )
		{
            PDF_P_buttonExPush(100);

            //PdfiumViewerのズームイン / アウトとパーセント表示
            SetZoomInOut(100);
        }
        /// <summary>
        /// 表示倍率：150％
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PDF_150P_buttonEx_Click( object sender, EventArgs e )
		{
            PDF_P_buttonExPush(150);

            //PdfiumViewerのズームイン / アウトとパーセント表示
            SetZoomInOut(150);
        }
        /// <summary>
        /// 表示倍率：内容変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PDF_ZoomTextBoxEx_TextChanged( object sender, EventArgs e )
		{
			if(((TextBox)sender ).Text == "" ) return;
			string strSender = ( (TextBox)sender ).Text;

			//数字か判断
			int TrackValNow = 0;
			if( int.TryParse( strSender, out TrackValNow ) ) {
			} else {
				return;
			}
			if( TrackValNow < 5 || TrackValNow > 300 ) return;
			setTrackbar( TrackValNow );
            SetZoomInOut(TrackValNow);//
        }
		#endregion
		#region<バージョン番号>
		/// <summary>
		/// バージョン番号：ボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PDF_Button_Ver_Click( object sender, EventArgs e )
		{
            PdfInformation pdfInformation = pdfViewer1.Document.GetInformation();
            string stringDisp =
                "主題:" + pdfInformation.Subject + "\n" +
                "副題:" + pdfInformation.Title + "\n" +
                "\n" +
                "作成日:" + pdfInformation.CreationDate + "\n" +
                "更新日:" + pdfInformation.ModificationDate + "\n" +
                "\n" +
                "著者:" + pdfInformation.Author;// + "\n"; +
                //"作成ツール:" + pdfInformation.Creator + "\n";// +
                //"キーワード:" + pdfInformation.Keywords + "\n" +
                //"製作者:" + pdfInformation.Producer + "\n" +
                MessageBox.Show(stringDisp, "バージョン情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
		#endregion
		#region<ボタン：クリック>
		private const int VK_CTRL  = 0x11;//スクロールではなくズームIN/OUTになる
		private const int VK_SHIFT = 0x10;//スクロールがロックされる
		private const int VK_SPACE = 0x20;//一番問題が無い
		/// <summary>
		/// 「Gコード」ボタン：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GCode_ButtonEx_Click( object sender, EventArgs e )
		{
			//ページに移動
            pdfViewer1.Renderer.Page = _iFirstPage_GCode-1;
            PDF_TextBoxEx.Text = _iFirstPage_GCode.ToString();//ページ表示
            return;
        }
        /// <summary>
        /// 「Mコード」ボタン：クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MCode_ButtonEx_Click( object sender, EventArgs e )
		{
            pdfViewer1.Renderer.Page = _iFirstPage_MCode-1;
            PDF_TextBoxEx.Text = _iFirstPage_MCode.ToString();//ページ表示
            return;
		}
		/// <summary>
		/// 「目次に移動」ボタン：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PDF_Content_ButtonEx_Click( object sender, EventArgs e )
		{
            pdfViewer1.Renderer.Page = _iFirstPage_Contents-1;
            PDF_TextBoxEx.Text = _iFirstPage_Contents.ToString();//ページ表示
            return;
		}
         private int _intPopupType = 0;
        /// <summary>
        /// ページ：マウスクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PDF_TextBoxEx_MouseClick(object sender, MouseEventArgs e)
        {
            _intPopupType = 0;
            popupTenkeyOn(PDF_TextBoxEx.Text.ToString(), 1, 999, labelEx_Page.Text.ToString());
        }
        /// <summary>
        /// 倍率：マウスクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PDF_ZoomTextBoxEx_MouseClick(object sender, MouseEventArgs e)
        {
            _intPopupType = 1;
            popupTenkeyOn(PDF_ZoomTextBoxEx.Text.ToString(),5, 300, labelEx_Mag.Text.ToString());
        }
        #endregion
        #region<ポップアップ・キーボード>
        [DllImport( "user32.dll" )]
		public static extern uint keybd_event( byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo );
		/// ポップアップ・キーボード
		private StandardKeyBord _PopupKeybord = null;
        /// <summary>
        /// 「検索」ボタン：クリック：CTRL+Fと同等処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PDF_Find_buttonEx_Click(object sender, EventArgs e)
        {
            string findWord =  textBoxEx_Find.Text;
            PdfMatches retPdfMatches= pdfViewer1.Document.Search(findWord,true,true);
            int ItemsCount = retPdfMatches.Items.Count;
            MessageBox.Show(ItemsCount.ToString()+"個検出","サーチ結果");

            //{//アドビ
            //    CtrlPlusFKeyUpDn();
            //    CtrlPlusFKeyUpDn();                     //2回コールしないと検索ウインドが出ない
                //ポップアップキーボードがあるか？
            //    if (null != _PopupKeybord)
            //    {
            //        this.Controls.Remove(_PopupKeybord);
            //        _PopupKeybord.Dispose();
            //        _PopupKeybord = null;
            //    }
            //    if (_PopupKeybord == null)
            //    {
            //        //ポップアップ・キーボード表示
            //        _PopupKeybord = new StandardKeyBord();
            //        _PopupKeybord.NotifyReturn = NotifyReturn;//ESC(戻る)を押したイベント
            //        _PopupKeybord.Location = new System.Drawing.Point(0, 410);
            //        this.Controls.Add(_PopupKeybord);
            //    }
            //}
            return;
        }
        /// <summary>
        /// ポップアップ：ESC(閉じる)を押した
        /// </summary>
        private void NotifyReturn()
        {
            SendKeys.Send("{ESC}");//検索ウインドを閉じる。※PDFにESCすると検索ウインドが閉じる
        }
        /// <summary>
        /// PDFの「検索」ウインド表示(CTRL+Fキー)
        /// </summary>
        private void CtrlPlusFKeyUpDn()
		{
			//フォーカス設定
			Focus();
			//axAcroPDF1.Focus();
			//検索：CTRL+Fキー
			keybd_event( VK_SPACE, 0, 0, (UIntPtr)0 );//PDF_TextBoxExが更新
			//keybd_event( VK_SPACE, 0, 2, (UIntPtr)0 );//PDF_TextBoxExが更新
			//SendKeys.Send( "^F" );
			Focus();			//ここで再度フォーカスを当てると、コール2回で出る
			//axAcroPDF1.Focus();	//ここで再度フォーカスを当てると、コール2回で出る
			keybd_event( 0x11, 0, 0, (UIntPtr)0 );				//CTRL：ダウン
			keybd_event( (byte)'F', 0, 0, (System.UIntPtr)0 );	//Fキー：ダウン
			keybd_event( (byte)'F', 0, 2, (System.UIntPtr)0 );	//Fキー：アップ
			keybd_event( 0x11, 0, 2, (UIntPtr)0 );				//CTRL：アップ
		}
        #endregion
        #region <ポップアップテンキー>
        /// <summary>
        /// ポップアップテンキー：表示
        /// </summary>
        /// <param name="val"></param>
        /// <returns>false=上下ポップアップを表示</returns>
        private bool popupTenkeyOn(string val,int min,int max,string popupTitle)
        {
            if (_popupTenkey != null)
            {
                _popupTenkey.Close();   //画面を閉じる
                _popupTenkey = null;    //null初期化
            }
            //クリックしたコントロールの値を取得
            string changeVal = val;                 //編集値
            Decimal lowerLimitDec = (decimal)min;   //最小値
            Decimal upperLimitDec = (decimal)max;   //最大値
            //フォーマットタイプ
            NumericTextBox.FormatTypes formatType = NumericTextBox.FormatTypes.Integer3;
            //ポップアップTenKey表示
            _popupTenkey = new TenKeyDialog(changeVal, formatType, lowerLimitDec, upperLimitDec, false, false, false, "");
            _popupTenkey.NotifyReturnOk = popupTenkey_OnNotifyReturnOk; //イベント通知：OK
            _popupTenkey.Text = popupTitle;                             //テンキータイトル表示
            _popupTenkey.ShowDialog(this);                              //画面を開く
            return true;
        }
        /// <summary>
        /// ポップアップテンキー：「OK」ボタンを押した時、入力値をこのクラスに反映
        /// </summary>
        private void popupTenkey_OnNotifyReturnOk()
        {
            string retVal = _popupTenkey._tenkeyValReturn;//ポップアップテンキーで編集された値
            switch (_intPopupType)
            {
                case 0:
                    if (retVal=="") retVal = "1";
                    if (int.Parse(retVal) < 1) retVal = "1";
                    PDF_TextBoxEx.Text = retVal;
                    pdfViewer1.Renderer.Page = int.Parse(retVal)-1;
                    break;//ページ
                case 1:
                    PDF_ZoomTextBoxEx.Text = retVal;
                    SetZoomInOut(int.Parse(retVal));//ap
                    break; ;//倍率
            }
        }
        #endregion


    }
}
#region<axAcroPDF1のコントロールの持つ値をログで表示>
/*　
Static::C:\J\ECNC3Server\ECNC3\DOCUMENT\GjbNXl_? dl_20160318.pdf - Adobe Acrobat Reader DC
AVL_AVView::AVTabLinksContainerViewForDocs
AVL_AVView :: AVFlipContainerView
AVL_AVView :: AVDocumentMainView
AVL_AVView :: AVTaskPaneHostView
AVL_AVView :: AVTaskPaneView
AVL_AVView :: AVScrollView
ScrollBar :: 
　ScrollBar::
AVL_AVView :: AVTaskPanelTableContainerView
AVL_AVView :: AVTableContainerView
AVL_AVView :: AVScrollView
AVL_AVView :: AVTaskPanelTableContainerView
ScrollBar :: 
　ScrollBar::
AVL_AVView :: AVTableContainerView
AVL_AVView :: AVFlipContainerView
AVL_AVView :: AVTableContainerView
AVL_AVView :: AVDockableTabStripView
AVL_AVView :: AVSplitterView
AVL_AVView :: SecondaryHostView
AVL_AVView :: PrimaryHostView
AVL_AVView :: AVSplitationPageView
AVL_AVView :: AVSplitterView
AVL_AVView :: AVScrolledPageView
AVL_AVView :: AVScrollView
AVL_AVView :: AVCornerView
AVL_AVView :: AVRulerView
AVL_AVView :: AVRulerView
AVL_AVView :: AVTableContainerView
Static :: 210 x 297 mm	//A4サイズ
AVL_AVView :: AVPageView
RICHEDIT50W :: 7			//★表示ページ番号：1から可変
　ScrollBar::
　ScrollBar :: 
　AVL_AVView :: AVTopBarView
　Edit :: 97.5%				//拡大率
　Edit:: 1

※「検索」を表示した場合、
・・・	
Static :: 210 x 297 mm
AVL_AVView :: AVPageView
Edit :: abcef・・・「abcef」は検索コントロールへの入力値
RICHEDIT50W :: 1
*/
#endregion

