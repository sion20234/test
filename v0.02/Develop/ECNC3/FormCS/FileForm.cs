///////////////////////////////////////////////////////////////////////////////////////////
//
// (1) システム名称 : ELENIX ECNCⅢ
// (2) ファイル名   : FileForm.cs
// (3) 概要         : プログラムファイル入出力画面
// (4) 作成日       : 2015.06.01
// (5) 作成者       : 八野
// (6) 特記事項     : VisualStudio2015 C#
// (7) 更新履歴     : 2017.10.20：柏原ひろむ
//
///////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using ECNC3.Views.Popup;
using System.Management;    //エクスプローラ風
using ECNC3.Models;         //FileSettings用
using System.Xml;			//XML

namespace ECNC3.Views
{
	public enum FileFormMode
	{
		All,                //全て：ファイル名:取得
		AllPath,            //全て：パス名:取得
		OpenProgram,        //NCプログラム：ファイル名：開く
		SaveProgram,        //NCプログラム：ファイル名：保存
        RenameProgram,      //NCプログラム：ファイル名：名前変更
        OpenMacro,          //マクロ変数：ファイル名
		SaveMacro,          //マクロ変数：ファイル名
		ProcLogExport,      //加工ログ：パス名
		ProcLogDelete,      //加工ログ：ファイル名
		AlamLogHistOpen,    //アラーム履歴：ファイル名:取得
		AlamLogHistExport,  //アラーム履歴：複数ファイル名:取得
		OpenRenishaw,       //エラーピッチ補正：レニショーファイル名:取得
		UserLogExport,      //ユーザーログ：パス名
		UserLogDelete       //ユーザーログ：パス名
	}
	public enum FileCommand
	{
		Success,//成功
		Failed  //失敗
	}
	public enum SystemIoType
	{
		File,   //ファイル
		Folder  //フォルダ
	}

    /// <summary>
    /// FileForm
    /// </summary>
    public partial class FileForm : ECNC3Form
    {
        //ListViewItemSorterに指定するフィールド
        ListSortView listViewItemSorter;
        //呼び出し先に戻すパス
        public string _returnPath = "";         //1個
        public string _returnFullPath = "";     //1個
        public string[] _returnPaths = { };     //複数
        public string[] _returnFullPaths = { }; //複数
                                                /// <summary>Open指定ディレクトリパス</summary>
        private string OpenDirectoryPath = "";
        /// <summary>Save指定ディレクトリパス</summary>
        private string SaveDirectoryPath = "";
        /// <summary>Log指定ディレクトリパス</summary>
        //private string LogDirectoryPath = "";

        private string _filePath = "";
        private string _directoryPath = "";
        /// <summary>RW処理モード選択</summary>
        private FileFormMode _mode;

        /// <summary>設定結果取得関数定義</summary>
        public delegate void NotifyReturnDelegate();
        /// <summary>終了通知</summary>
        private NotifyReturnDelegate _notifyReturn = null;
        /// <summary>>設定結果取得</summary>
        public NotifyReturnDelegate NotifyReturn
        {
            set { _notifyReturn = value; }
        }

        public delegate void NotifyReturnDelegate2();
        /// <summary>終了通知</summary>
        static private NotifyReturnDelegate2 _notifyReturn2 = null;
        /// <summary>>設定結果取得</summary>
        static public NotifyReturnDelegate2 NotifyReturn2
        {
            set { _notifyReturn2 = value; }
        }
        //「CncSys.xml」ファイル値
        private string _CncSysDrvHide1 = "C:";          //隠すドライブ[C:]
        private int _CncSysDelayShowDevIn = 100;        //遅延表示：デバイス挿入待ち時間
        private int _CncSysDelayShowDevOut = 10;        //遅延表示：デバイス脱着待ち時間
        private int _CncSysDelayShowDevRetryTime = 1000;//遅延表示：リトライ時間待ち時間
        private int _CncSysDelayShowDevRetryCount = 3;  //遅延表示：リトライ回数
        private int _firstShowFolder = -1;              //初回表示フォルダ番号：
        /// <summary>
        /// 書込保護１行データ構造体
        /// </summary>
        public struct Struct1LineDataWriteProtect
        {
            public int index;           //インデックス
            public string itemName;     //アイテム名
            public int protectLevel;    //書込保護レベル
        }
        private Struct1LineDataWriteProtect[] _st1LineDat;  //1行データ
        private static bool _showProgButton = false;        //true=プログラム、Gコード、Mコードボタンを表示
        private static bool _goldKeyIcon = false;           //true=金鍵アイコン表示
        public enum WriteProtectLevel//書込み保護：プロテクトレベル
        {
            None=0, //無し
            Guest=1,//ゲスト
            User=2  //ユーザー
        }
        public enum DispTypeForWProtect//書込み保護：画面タイプ
        {
            Ather = 0,      //その他
            UserService = 1,//ユーザー:サービス
        }
        // -1=無し、
        //	0=自動：ファイル：PRG、
        //	2=手動：機能：マクロ変数：VAR
        //	3=
        //	4=アラーム履歴
        #region<起動時>
        /// <summary>									
        /// フォーム：コンストラクタ
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="directory"></param>
        /// <param name="firstShowFolder"></param>
        /// <param name="topmost"></param>
        public FileForm(
            FileFormMode mode, string directory, int firstShowFolder = -1, bool topmostVal = true)
        {
            bool showProgButton = false;

            InitializeComponent();
            _mode = mode;
            switch (_mode)
            {
                case FileFormMode.OpenProgram:
                    showProgButton = true;
                    OpenDirectoryPath = directory;
                    break;
                case FileFormMode.OpenMacro:
                case FileFormMode.OpenRenishaw:
                    OpenDirectoryPath = directory;
                    break;
                case FileFormMode.SaveProgram:
                    showProgButton = true;
                    SaveDirectoryPath = directory;
                    _saveBt.Visible = true;//保存ボタン：表示
                    break;
                case FileFormMode.RenameProgram://名前変更
                    showProgButton = true;
                    SaveDirectoryPath = directory;
                    _saveBt.Visible = true;//保存ボタン：表示
                    break;

                case FileFormMode.SaveMacro:
                    SaveDirectoryPath = directory;
                    _saveBt.Visible = true;//保存ボタン：表示
                    break;
                case FileFormMode.ProcLogExport:            //ログ出力
                case FileFormMode.AlamLogHistExport:        //アラーム履歴：EXPORT
                    _saveBt.Visible = true;                 //保存ボタン：表示
                    directoryListView.FullRowSelect = true; //ListView行選択モード
                    break;
                case FileFormMode.UserLogExport:            //ユーザーログ
                    _saveBt.Visible = true;                 //保存ボタン：表示
                    directoryListView.FullRowSelect = true; //ListView行選択モード
                    break;
                case FileFormMode.ProcLogDelete:            //加工ログ
                    _deleteBt.Visible = true;               //削除ボタン：表示
                    directoryListView.FullRowSelect = true; //ListView行選択モード
                    break;
                case FileFormMode.UserLogDelete:            //ユーザーログ
                    _deleteBt.Visible = true;               //削除ボタン：表示
                    directoryListView.FullRowSelect = true; //ListView行選択モード
                    break;
            }
            _showProgButton = showProgButton;//プログラム、Gコード、Mコードボタンを表示/非表示
            if (_showProgButton)
            {//プログラム、Gコード、Mコードボタンを表示
                radioButtonEx_Program.Select();//初回選択
                radioButtonEx_Program.Visible = true;
                radioButtonEx_GCode.Visible = true;
                radioButtonEx_MCode.Visible = true;

            }
            _firstShowFolder = firstShowFolder;//初回表示フォルダ番号

            Disposed += FileForm_Disposed;
            this.TopMost = topmostVal;
        }
        /// <summary>
        /// フォーム：ロード時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileForm_Load(object sender, EventArgs e)
        {
            radioButtonEx_Program.Checked = true;//ラジオボタン：プログラム
            radioButtonEx_GCode.Checked = false;//ラジオボタン：Mコード
            radioButtonEx_MCode.Checked = false;//ラジオボタン：Gコード

            directoryListView.BackColor = FileUIStyleTable.DefaultBackColor;
            directoryListView.ForeColor = FileUIStyleTable.DefaultForeColor;
            treeView1.BackColor = FileUIStyleTable.DefaultBackColor;
            treeView1.ForeColor = FileUIStyleTable.DefaultForeColor;
            //「CncSys.xml」取得
            using (FileSettings fs = new FileSettings())
            {
                //読み込み
                fs.Read();
                _CncSysDrvHide1 = fs.AttrText("Root/FileFormTreeView/Drive", "Hide1");
                _CncSysDelayShowDevIn = fs.AttrValue("Root/FileFormTreeView/DelayShowDevice", "In");
                _CncSysDelayShowDevOut = fs.AttrValue("Root/FileFormTreeView/DelayShowDevice", "Out");
                _CncSysDelayShowDevRetryTime = fs.AttrValue("Root/FileFormTreeView/DelayShowDevice", "RetryTime");
                _CncSysDelayShowDevRetryCount = fs.AttrValue("Root/FileFormTreeView/DelayShowDevice", "RetryCount");
            }
            //並べ替えソートの初期パラメータ設定
            //ColumnClickイベントハンドラの追加
            directoryListView.ColumnClick +=
                new ColumnClickEventHandler(directoryListView_ColumnClick);

            //ListSortViewの作成と設定
            listViewItemSorter = new ListSortView();
            listViewItemSorter.ColumnModes =
                new ListSortView.ComparerMode[]
                {
                    ListSortView.ComparerMode.String,
//                  ListSortView.ComparerMode.Integer,
                    ListSortView.ComparerMode.DateTime
                };

            //ListViewItemSorterを指定する
            directoryListView.ListViewItemSorter = listViewItemSorter;
            //ツリービュー：ＰＣ全フォルダと全ファイルを表示：※Cドライブは除く：CncSys.xmlの<FileFormTreeView> <Drive Hide1="C:"/>で設定
            SetTreeView(treeView1, _CncSysDrvHide1);
            //ツリービュー：指定ノードを開く
            TreeNodesExpands();

            //ツリービュー：コール遅延タイマー設定
            _DelayTimerTreeView = new System.Threading.Timer(
                new System.Threading.TimerCallback(fireTreeView), null, System.Threading.Timeout.Infinite, 0);
            //ツリービュー：表示確認タイマー設定
            _DisplayConfirmTimerTreeView = new System.Threading.Timer(
            new System.Threading.TimerCallback(fireDisplayConfirmTreeView), null, System.Threading.Timeout.Infinite, 0);
            _DelayTimerTreeView.Change(_CncSysDelayShowDevRetryTime, 0); //ツリービュー：コール遅延タイマー起動
            return;
        }
        #endregion
        #region<起動時：サブフォーム>
        static public FileForm _subForm;
        public FileForm _subForm2;
        /// <summary>
        /// ファイル画面表示
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="directory"></param>
        /// <param name="showProgButton"></param>
        /// <param name="dispType"></param>
        /// <returns></returns>
        static public FileForm ShowSubFormEd(
        IWin32Window owner,
        FileFormMode mode,
        string directory,
        DispTypeForWProtect dispType = DispTypeForWProtect.Ather)
        {
            if (dispType == DispTypeForWProtect.UserService)
            {//画面タイプ＝USER:SERVICEはリストに表示する鍵アイコンを金色にします。
                _goldKeyIcon = true;
            }
            else
            {
                _goldKeyIcon = false;
            }
            int selectFolderMode = -1;//ツリーフォルダ選択モード
            switch (mode)
            {
                //NCプログラム
                case FileFormMode.OpenProgram:
                case FileFormMode.SaveProgram:
                    selectFolderMode = 0;
                    break;
                //マクロ
                case FileFormMode.OpenMacro:
                case FileFormMode.SaveMacro:
                    selectFolderMode = 1;
                    break;
                //加工ログ
                case FileFormMode.ProcLogExport:
                case FileFormMode.ProcLogDelete:
                    selectFolderMode = 2;
                    break;
            }
            //ファイルフォーム：ツリービュー＋リストビュー
            _subForm = new FileForm(mode, directory, selectFolderMode);
            _subForm.Show(owner);//モーダレス
            return _subForm;
            //return null;

            //f.ShowDialog();//モーダル
            //string receiveText = f._returnPath;
            //f.Dispose();
            //return receiveText;
        }
        private static string _fileNameNonEx;
        /// <summary>
        /// モーダレス
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="mode"></param>
        /// <param name="directory"></param>
        /// <param name="dispType"></param>
        /// <returns></returns>
        // public FileForm ShowSubFormModeless(
        static public FileForm ShowSubFormModeless(
            IWin32Window owner,
             FileFormMode mode,
             string directory,
             DispTypeForWProtect dispType = DispTypeForWProtect.Ather,
             string fileNameNonEx = ""
            )
        {
            _fileNameNonEx = fileNameNonEx;
            if (dispType == DispTypeForWProtect.UserService)
            {//画面タイプ＝USER:SERVICEはリストに表示する鍵アイコンを金色にします。
                _goldKeyIcon = true;
            }
            else
            {
                _goldKeyIcon = false;
            }
            int selectFolderMode = -1;//ツリーフォルダ選択モード
            switch (mode)
            {
                //NCプログラム
                case FileFormMode.OpenProgram:
                case FileFormMode.SaveProgram:
                case FileFormMode.RenameProgram://名前変更
                    selectFolderMode = 0;
                    break;
                //マクロ
                case FileFormMode.OpenMacro:
                case FileFormMode.SaveMacro:
                    selectFolderMode = 1;
                    break;
                //加工ログ
                case FileFormMode.ProcLogExport:
                case FileFormMode.ProcLogDelete:
                    selectFolderMode = 2;
                    break;
            }
            //フォーム：
            _subForm = new FileForm(mode, directory, selectFolderMode,false);
            _subForm.Show(owner);//モーダレス
            return _subForm;
 
            //_subForm.ShowDialog();//モーダル
            //string receiveText = _subForm._returnPath;
            //_subForm.Dispose();
            //return receiveText;
        }
        /// <summary>
        /// モーダル
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="mode"></param>
        /// <param name="directory"></param>
        /// <param name="dispType"></param>
        /// <returns></returns>
        static public string ShowSubForm(
          IWin32Window owner,
          FileFormMode mode,
          string directory,
          DispTypeForWProtect dispType = DispTypeForWProtect.Ather)
        {
            if (dispType == DispTypeForWProtect.UserService)
            {//画面タイプ＝USER:SERVICEはリストに表示する鍵アイコンを金色にします。
                _goldKeyIcon = true;
            }
            else
            {
                _goldKeyIcon = false;
            }
            int selectFolderMode = -1;//ツリーフォルダ選択モード
            switch (mode)
            {
                //NCプログラム
                case FileFormMode.OpenProgram:
                case FileFormMode.SaveProgram:
                    selectFolderMode = 0;
                    break;
                //マクロ
                case FileFormMode.OpenMacro:
                case FileFormMode.SaveMacro:
                    selectFolderMode = 1;
                    break;
                //加工ログ
                case FileFormMode.ProcLogExport:
                case FileFormMode.ProcLogDelete:
                    selectFolderMode = 2;
                    break;
            }
            //ファイルフォーム：ツリービュー＋リストビュー
            _subForm = new FileForm(mode, directory, selectFolderMode);
            _subForm.ShowDialog();//モーダル
            string receiveText = _subForm._returnPath;
            _subForm.Dispose();
            return receiveText;
        }

        /// <summary>
        /// ファイル画面表示
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="directory"></param>
        /// <param name="showProgButton"></param>
        /// <param name="dispType"></param>
        /// <returns></returns>
        static public string ShowSubForm(
        FileFormMode mode,
        string directory,
        DispTypeForWProtect dispType = DispTypeForWProtect.Ather)
        {
            if (dispType == DispTypeForWProtect.UserService)
            {//画面タイプ＝USER:SERVICEはリストに表示する鍵アイコンを金色にします。
                _goldKeyIcon = true;
            }else{
                _goldKeyIcon = false;
            }
            int selectFolderMode = -1;//ツリーフォルダ選択モード
            switch (mode)
            {
                //NCプログラム
                case FileFormMode.OpenProgram:
                case FileFormMode.SaveProgram:
                    selectFolderMode = 0;
                    break;
                //マクロ
                case FileFormMode.OpenMacro:
                case FileFormMode.SaveMacro:
                    selectFolderMode = 1;
                    break;
                //加工ログ
                case FileFormMode.ProcLogExport:
                case FileFormMode.ProcLogDelete:
                    selectFolderMode = 2;
                    break;
            }
            //ファイルフォーム：ツリービュー＋リストビュー
            _subForm = new FileForm(mode, directory, selectFolderMode);
            _subForm.Show();//モーダレス
            return null;

            //f.ShowDialog();//モーダル
            //string receiveText = f._returnPath;
            //f.Dispose();
            //return receiveText;
        }
        static public FileForm ShowSubFormMod(
            FileFormMode mode,
            string directory,
            DispTypeForWProtect dispType = DispTypeForWProtect.Ather)
        {
            if (dispType == DispTypeForWProtect.UserService)
            {//画面タイプ＝USER:SERVICEはリストに表示する鍵アイコンを金色にします。
                _goldKeyIcon = true;
            }
            else
            {
                _goldKeyIcon = false;
            }
            int selectFolderMode = -1;//ツリーフォルダ選択モード
            switch (mode)
            {
                //NCプログラム
                case FileFormMode.OpenProgram:
                case FileFormMode.SaveProgram:
                    selectFolderMode = 0;
                    break;
                //マクロ
                case FileFormMode.OpenMacro:
                case FileFormMode.SaveMacro:
                    selectFolderMode = 1;
                    break;
                //加工ログ
                case FileFormMode.ProcLogExport:
                case FileFormMode.ProcLogDelete:
                    selectFolderMode = 2;
                    break;
            }
            //ファイルフォーム：ツリービュー＋リストビュー
            _subForm = new FileForm(mode, directory, selectFolderMode);
            _subForm.Show();//モーダレス
            return _subForm;

            //FileForm f = new FileForm(mode, directory, selectFolderMode);
            //IWin32Window owner = this;
            //f.ShowDialog();//モーダル
            //string receiveText = f._returnPath;
            //f.Dispose();
            //return receiveText;
        }
        #endregion
        #region<終了時>
        /// <summary>
        /// 閉じる：クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnReturn_Click(object sender, EventArgs e)
        {   //A「閉じる」ボタン時：1番
            
            Close();//閉じる
        }
        /// <summary>
        /// フォーム：閉じた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileForm_FormClosed(object sender, FormClosedEventArgs e)
        {   //A「閉じる」ボタン時：2番
            //B　画面遷移時：1番
            Form_Disposed();
        }
        /// <summary>
        /// フォームを閉じる
        /// </summary>
        private void Form_Disposed()
        {   //A「閉じる」ボタン時：3、5番
            //B　画面遷移時：2、4番
            //ツリービュー：コール遅延タイマー
            if (_DelayTimerTreeView != null)
            {
                _DelayTimerTreeView.Dispose();
                _DelayTimerTreeView = null;
            }
            //ツリービュー：表示確認タイマー
            if (_DisplayConfirmTimerTreeView != null)
            {
                _DisplayConfirmTimerTreeView.Dispose();
                _DisplayConfirmTimerTreeView = null;
            }
        }
        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileForm_Disposed(object sender, EventArgs e)
        {   //A「閉じる」ボタン時：4番
            //B 画面遷移時：3番
            Form_Disposed();
            if (_subForm != null)
            {
                //SubFormが開いていれば、閉じる
                _subForm.Close();
                _subForm = null;
            }
        }
        #endregion
        #region<ツリービュー>
        private object _treeView1SelectedNodeMemo;
        /// <summary>
        /// ツリービュー：指定ノードを開く
        /// </summary>
        private void TreeNodesExpands()
        {
            //ツリービュー：開くフォルダを「Path.xml」からファイル読み込み
            //★決まった拡張子がある場合、ここに追加とコンストラクタ、Load、treeView1_AfterSelectに追加
            string readfullPath = "";
            switch (_firstShowFolder)
            {
                case 0: readfullPath = FilePathInfo.ProgramData; break; //自動：ファイル：PRG
                case 1: readfullPath = FilePathInfo.MacroData; break;   //手動：機能：マクロ変数：VAR
                case 2: readfullPath = FilePathInfo.ECNC3PATH; break;   //手動：機能：サービス8188：エクスプローラ
                //case 3: readfullPath = FilePathInfo.ProcLog; break;    //加工ログ：リスト側クリック不可
                case 4: readfullPath = FilePathInfo.Log; break;         //ログ：アラームログ履歴、他
                default: break;
            }
            switch (_mode)
            {//今後_firstShowFolderでなく_modeで判断するようにします。
                case FileFormMode.ProcLogExport: readfullPath = FilePathInfo.ProcLog; break;
                case FileFormMode.ProcLogDelete: readfullPath = FilePathInfo.ProcLog; break;
                case FileFormMode.UserLogExport: readfullPath = FilePathInfo.User; break;//ユーザーログ
                case FileFormMode.UserLogDelete: readfullPath = FilePathInfo.User; break;//ユーザーログ
            }

            //フルパスをフォルダ単位に分割
            string[] partPath = null;
            int pathCount = pathFolderUnit(readfullPath, ref partPath);

            if (_firstShowFolder > -1)
            {//_firstShowFolderが-1だとツリービューが開かない
             //パス有り
             //ツリービューに設定
                tempNode = treeView1.Nodes;
                foreach (string str in partPath)
                {
                    if (partPath[partPath.Length - 1] == str)
                    {
                        //ノードを開く
                        TreeNodesExpands(tempNode, str, true);//最後
                    }
                    //ノードを開く
                    TreeNodesExpands(tempNode, str);
                }
            }
            //※ツリービューのノード選択はTreeViewSetFocus()でフォーカスで設定してます。
        }

        TreeNodeCollection tempNode = null;
        /// <summary>
        /// ツリーノードを開く
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="target"></param>
        /// <param name="last"></param>
        private void TreeNodesExpands(TreeNodeCollection nodes, string target, bool last = false)
        {
            for (int ct = 0; ct < nodes.Count; ct++)
            {
                string nodeString = nodes[ct].Text.ToString();
                if (nodeString.Contains("\\"))
                {
                    //\\(バックスラッシュ)削除
                    nodeString = nodeString.Replace("\\", "");
                }
                if (nodeString == target)
                //if (nodes[ct].Text.Contains(target))
                {
                    nodes[ct].Expand();
                    tempNode = nodes[ct].Nodes;
                    if (last == true)
                    {
                        treeView1.SelectedNode = nodes[ct];
                        treeView1.Focus();
                    }
                }
            }
        }
        /// <summary>
        /// パスをフォルダー単位に分割
        /// </summary>
        /// <param name="inPath"></param>
        /// <param name="outPaths"></param>
        /// <returns></returns>
        private int pathFolderUnit(string inPath, ref string[] outPaths)
        {
            if (inPath == null)
            {
                return 0;
            }
            string[] stringArray = { };
            int iCount1 = 0;
            int iCount2 = 0;
            for (; ; iCount1++)
            {
                //サイズを1増加
                Array.Resize(ref stringArray, iCount1 + 1);
                string tmpString = "";
                for (; iCount2 < inPath.Length; iCount2++)
                {
                    if (iCount2 > 0)
                    {
                        if (inPath[iCount2 - 1] == '\r' && inPath[iCount2] == '\n')
                        {
                            iCount2 += 2;
                            break;
                        }
                    }
                    if (inPath[iCount2] == '/')
                    {
                        iCount2++;
                        break;
                    }
                    tmpString += inPath[iCount2].ToString();
                }
                //サイズを1増加
                Array.Resize(ref outPaths, iCount1 + 1);
                outPaths[iCount1] = tmpString;
                if (iCount2 >= inPath.Length)
                {
                    break;
                }
            }
            return iCount1;
        }
        /// <summary>
        /// フォーム：クリック：ツリービューが無い場合、表示：(緊急策：クリックするとツリービュー表示)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileForm_Click(object sender, EventArgs e)
        {
            ShowSetTreeView();// ツリービュー：無ければ表示
        }
        #endregion
        #region<WndProc>
        /// <summary>
        /// デバイス変更：WM定義
        /// </summary>
        private enum WM_MES : uint
        {
            WM_DEVICECHANGE = 0x0219,   //デバイス：変更時
        }
        /// デバイス変更：ドライブ：WM定義
        private enum DBT
        {
            DBT_DEVICEARRIVAL = 0x8000,        //ドライブ：装着時
            DBT_DEVICEREMOVECOMPLETE = 0x8004, //ドライブ：脱着時
        }
        //隠しドライブ：１個のみ指定可
        //private string _hideDrive = "C:";
        /// <summary>
        /// デバイス変更：イベントハンドラ
        /// </summary>
        /// <param name="wMmes"></param>
        protected override void WndProc(ref Message wMmes)
        {
            switch ((WM_MES)wMmes.Msg)
            {
                case WM_MES.WM_DEVICECHANGE:
                    switch ((DBT)wMmes.WParam.ToInt32())
                    {
                        case DBT.DBT_DEVICEARRIVAL:         //ドライブが装着された時の処理
                            //USBを挿入してOSが反応するまで時間がかかる
                            //タイマー：設定
                            _DelayTimerTreeView.Change(_CncSysDelayShowDevIn, 0);//※70以下だとtreeView1が表示しない確率が上がります。
                            break;
                        case DBT.DBT_DEVICEREMOVECOMPLETE:  //ドライブが取り外されたされた時の処理
                            //タイマー：設定
                            _DelayTimerTreeView.Change(_CncSysDelayShowDevOut, 0);//※1以下だとtreeView1が表示しない確率が上がります。
                            break;
                    }
                    break;
            }
            base.WndProc(ref wMmes);
        }
        #endregion
        #region<①ツリービュー：初回タイマー、メッセージと表示>
        /// <summary>
        /// ツリービュー：コール遅延タイマー
        /// </summary>
        private System.Threading.Timer _DelayTimerTreeView = null;
        /// <summary>
        /// ツリービュー：コール遅延タイマー：ファイアー時
        /// </summary>
        /// <param name="obj"></param>
        private void fireTreeView(Object obj)
        {
            //directoryListView.Enabled = false;//リスト使用不可・・・falseはリストが消えるので不可
            retInvoke = BeginInvoke((MethodInvoker)delegate ()
            {
                if (_firstShowFolder == 3)
                {
                    switch (_mode)
                    {
                        case FileFormMode.ProcLogExport: //加工ログ：EXPORT
                                                         //"保存ファイルの選択" txt="ファイルを選択し、保存を押して下さい。
                             using (MessageDialog msg = new MessageDialog())
                            {
                                msg.Information(5510, this);
                            }
                            break;
                        case FileFormMode.ProcLogDelete: //加工ログ：削除
                                                         //"削除ファイルの選択" txt="ファイルを選択し、削除を押して下さい。
                             using (MessageDialog msg = new MessageDialog())
                            {
                                _deleteBt.Visible = true;//表示
                                 msg.Information(5511, this);
                            }
                            break;
                    }
                }
                if (treeView1.Visible)
                {
                    TreeViewSetFocus();//TreeViewの選択したノードを表示
                    return;//既にツリー表示が表示している場合
                }
                 _DelayTimerTreeView.Change(System.Threading.Timeout.Infinite, 0);//タイマ待機
                if ((this.IsDisposed == true || this.Disposing == true))
                {
                    TreeViewSetFocus();//TreeViewの選択したノードを表示
                    return;
                }
                 //ツリービュー：ＰＣ全フォルダ/ファイル表示：Cドライブは除く
                 ShowSetTreeView(true);
                 //ツリービュー：指定ノードを開く
                 TreeNodesExpands();
                _DisplayConfirmTimerTreeView.Change(_CncSysDelayShowDevRetryTime, 0); //表示確認タイマー：起動
                TreeViewSetFocus();//TreeViewの選択したノードを表示
            });
            if ((this.IsDisposed == true || this.Disposing == true))
            {
                TreeViewSetFocus();//TreeViewの選択したノードを表示
                return; EndInvoke(retInvoke);
            }
            return;
        }
        /// <summary>
        /// TreeViewの選択したノードを表示　※フォーカス制御なので最後に行う
        /// </summary>
        private void TreeViewSetFocus()
        {
            //treeView1.SelectedNode・・・選択されたノード
            this.Focus();
            treeView1.Focus();
         }
        //リトライカウンタ
        private int _RetryCount = 0;
        /// <summary>
        /// ツリービュー：表示確認タイマー　※遅延タイマー処理でツリービューが失敗した場合、リトライ
        /// </summary>
        private System.Threading.Timer _DisplayConfirmTimerTreeView = null;
        /// <summary>
        /// ツリービュー：表示確認タイマー：ファイアー時
        /// </summary>
        /// <param name="obj"></param>
        private void fireDisplayConfirmTreeView(Object obj)
        {
            _DisplayConfirmTimerTreeView.Change(System.Threading.Timeout.Infinite, 0);//タイマ待機
            if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate ()
         {
             if (ShowSetTreeView() == false)
             {// ツリービュー：表示
              //treeView1.Visible=trueでない場合、リトライ
                    if (_RetryCount < _CncSysDelayShowDevRetryCount)
                 {
                        //指定回数分リトライします。
                        _DisplayConfirmTimerTreeView.Change(_CncSysDelayShowDevRetryTime, 0);
                     return;
                 }
             }
                //ツリービュー：指定ノードを開く
                TreeNodesExpands();
         }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
        }
        #endregion
        #region<①ツリービュー>
        /// <summary>
        /// ツリービュー：ツリービューが無ければ表示、boolForce=trueも表示
        /// </summary>
        /// <param name="boolForce">true=強制表示</param>
        private bool ShowSetTreeView(bool boolForce = false)
        {
            if (treeView1.Visible == false)
            {
                //ツリーが無ければ表示
                ShowSetTreeViewDsp();
                return true;
            }
            else if (boolForce)
            {
                //強制表示
                ShowSetTreeViewDsp();
                return true;
            }
            if (treeView1.Visible) return true;
            return false;//表示していない
        }

        /// <summary>
        /// ツリービュー：ツリービュー表示
        /// </summary>
        private void ShowSetTreeViewDsp()
        {
            CenterWaitCursor();                 //ウェイトカーソル中央表示
            treeView1.Nodes.Clear();            //全ノード：削除
            treeView1.Visible = false;          //ツリービュー非表示
                                                //ツリービュー：ＰＣ全フォルダと全ファイルを表示：Cドライブは除く
            SetTreeView(treeView1, _CncSysDrvHide1);
            treeView1.Visible = true;           //ツリービュー表示
            Cursor.Current = Cursors.Default;   //カーソル戻す
        }
        /// <summary>
        ///  ツリービューにＰＣ全フォルダと全ファイルを表示
        /// </summary>
        /// <param name="tView">コントロールインスタンス</param>
        /// <param name="hideDrive">非表示ドライブ</param>
        /// <param name="forDevChange">デバイス変更時に使用する場合=true</param>
        private void SetTreeView(TreeView tView, string hideDrive = "", bool forDevChange = false)
        {
            try
            {
                tView.Sorted = true;//ソート；ＯＮ    
                // 論理ドライブ一覧を列挙
                string[] drives = Environment.GetLogicalDrives();//論理ドライブ文字列取得
                //ドライブ数カウンタ(エラー監視用)
                int drvCount = 0;
                foreach (string drive in drives)
                {
                    ManagementObject mObject = new ManagementObject();
                    //ドライブのタイプを取得
                    mObject.Path = new ManagementPath("Win32_LogicalDisk='" + drive.TrimEnd('\\') + "'");
                    if (mObject.Path.ToString().IndexOf(hideDrive) > -1)
                    {
                        if (hideDrive == "")
                        {
                            //隠しドライブなし
                        }
                        else
                        {
                            continue;//指定ドライブを隠す
                        }
                    }
                    int drivetype = Convert.ToInt32(mObject.Properties["DriveType"].Value);
                    // ドライブのタイプごとにアイコンを設定
                    TreeNode node = new TreeNode();
                    switch (drivetype)
                    {
                        case 0: continue;//ドライブを判別できません
                        case 1: continue;//ドライブ上にルートディレクトリが存在しません
                        case 2: node = new TreeNode(drive, 1, 1); break;//リムーバブル/Floppy
                        case 3: node = new TreeNode(drive, 1, 1); break;//HDD
                        case 4: node = new TreeNode(drive, 1, 1); break;//Network
                        case 5: node = new TreeNode(drive, 1, 1); break;//CD-ROM
                        case 6: node = new TreeNode(drive, 1, 1); break;//RAMディスク
                    }
                    //ノードに追加
                    int ret = tView.Nodes.Add(node);
                    Console.WriteLine("ドライブの名前:{0}", node);
                    try
                    {//ノード追加されていなければ、tView.Nodes.Add　がおかしい
                        if (tView.Nodes[drvCount] == null)
                        {
                        }
                    }
                    catch (System.IO.IOException exc)
                    {
                        MessageBox.Show(exc.Message, "入出力エラー:IOException", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (System.ArgumentOutOfRangeException)
                    {
                        //ノード追加再度リトライ①
                        tView.Nodes.Clear();//全ノード：削除
                        //自身再帰コール
                        SetTreeView(tView, hideDrive);
                        return;//
                    };
                    // +ボックスを表示するためのダミー
                    node.Nodes.Add("dummy");
                    mObject.Dispose();
                    mObject = null;
                    drvCount++;
                }
            }
            catch (Exception exc)
            {//例外処理
                MessageBox.Show(exc.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        #region<①ツリービュー：展開/閉じる>
        /// <summary>
        /// ツリー展開前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode node = e.Node;
            // 展開するノードのフルパスを取得
            string fullpath = node.FullPath;
            node.Nodes.Clear();
            // フォルダ一覧を取得
            DirectoryInfo dirs = new DirectoryInfo(fullpath);
            try
            {
                foreach (DirectoryInfo dir in dirs.GetDirectories())
                {
                    // フォルダを追加
                    TreeNode nodeFolder = new TreeNode(dir.Name, 2, 3);
                    node.Nodes.Add(nodeFolder);
                    // +ボックスを表示するためのダミー
                    nodeFolder.Nodes.Add("dummy");
                }
            }
            catch { }
        }
        /// <summary>
        /// ツリー展開後：ノード選択変更、リストで表示する拡張子をここで選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //ListViewにファイル一覧を表示
            //20160324修正
            //Deteil表示の場合、Columも消えてしまうので、「.Clear」から「.Items.Clear」に変更。
            directoryListView.Items.Clear();//これがないとファイル表示が累積
            DirectoryInfo di = new DirectoryInfo(e.Node.FullPath);

            if (e.Node.Text != "A:\\")//＋マークによるノード展開でデフォルトのA:\アクセス回避
            {
                if (di.Exists)
                {
                    try
                    {
                        switch (_mode)
                        {
                            case FileFormMode.AllPath://パス名
                                _returnPath = di.ToString();
                                this.Close();//このフォームを閉じる
                                return;
                            case FileFormMode.All://ファイル名
                                displyExtension(di, "*");//指定した拡張子をリストに表示
                                break;
                            //NCプログラム
                            case FileFormMode.OpenProgram:
                            case FileFormMode.SaveProgram://追加
                            case FileFormMode.RenameProgram://
                                displyExtension(di, "PGM");//指定した拡張子をリストに表示
                                break;
                            //マクロ
                            case FileFormMode.OpenMacro:
                            case FileFormMode.SaveMacro:
                                displyExtension(di, "VAR");//指定した拡張子をリストに表示
                                break;
                            //加工ログ：
                            case FileFormMode.ProcLogExport:
                                displyExtension(di, "txt");//指定した拡張子をリストに表示
                                break;
                            //加工ログ：削除
                            case FileFormMode.ProcLogDelete:
                                //_returnPath = di.ToString();
                                displyExtension(di, "txt");//指定した拡張子をリストに表示
                                break;
                            //アラームログ履歴
                            case FileFormMode.AlamLogHistOpen:
                            case FileFormMode.AlamLogHistExport:
                                _returnPath = di.ToString();
                                displyExtension(di, "xml");//指定した拡張子をリストに表示
                                                           //パスリスト：初期化
                                Array.Resize(ref _returnPaths, 0);
                                Array.Resize(ref _returnFullPaths, 0);
                                break;
                            //レニショー
                            case FileFormMode.OpenRenishaw:
                                displyExtension(di, "rtl");//指定した拡張子をリストに表示
                                displyExtension(di, "rta");
                                break;
                            //ユーザーログ
                            case FileFormMode.UserLogExport:
                            case FileFormMode.UserLogDelete:
                                displyExtension(di, "txt");//指定した拡張子をリストに表示(仮)
                                break;
                        }
                    }
                    catch (Exception exc)
                    {//例外処理
                        MessageBox.Show(exc.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            statusBarTextShow(e.Node.FullPath);//★statusBarにFullPathを表示する下記メソッド
        }
        #endregion
        #region<②リストビュー>
        /// <summary>
        /// リストビュー：指定した拡張子を表示
        /// </summary>
        /// <param name="di"></param>
        /// <param name="extension">拡張子</param>
        void displyExtension(DirectoryInfo di, string extension)
        {
            ShowListView(extension, di);//指定の拡張子でリストビューを表示
        }
        #region<UIWriteProtect.xml：読込み/書込み>
        /// <summary>
        /// リストビューにファイルが有るか比較
        /// </summary>
        /// <param name="compFileName"></param>
        /// <returns>true=有る</returns>
        private bool directoryListViewComp(string compFileName) {
            int intLoopMax = directoryListView.Items.Count;
            int intCount = 0;
            for (; intCount < intLoopMax; intCount++)
            {
                if (compFileName == directoryListView.Items[intCount].Text)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// ファイル保護：UIWriteProtect.xml：読込み
        /// </summary>
        /// <param name="loopCont"></param>
        void WriteProtectXML_Read(ref int loopCont)
        {
            try
            {
                //ECNC3Apisを使用しないでリード
                string masterFolder = @FilePathInfo.MasterData;//\Masterフォルダ
                XmlDocument xmlDocument = new XmlDocument();
                if (File.Exists(masterFolder + "UIWriteProtect.xml")==false)
                {//ファイルが無い場合、空ファイル作成
                    _st1LineDat = null;
                    WriteProtectXML_Write();
                }
                xmlDocument.Load(masterFolder + "UIWriteProtect.xml");//読込
                XmlNode root = xmlDocument.DocumentElement;
                loopCont = root.ChildNodes.Count;
                _st1LineDat = null;//読み込むデータ
                for (int intLoop = 0; intLoop < loopCont; intLoop++)
                {
                    //要素数を増やす
                    Array.Resize(ref _st1LineDat, intLoop + 1);
                    XmlNode node = root.ChildNodes[intLoop];
                    string itemName = ((XmlElement)node).GetAttribute("ItemName");
                    _st1LineDat[intLoop].itemName = ((XmlElement)node).GetAttribute("ItemName");
                    _st1LineDat[intLoop].protectLevel =int.Parse( ((XmlElement)node).GetAttribute("protectLevel"));
                }
                xmlDocument = null;
                //例外処理
            }
            catch (ArgumentException ex)
            {   //引数例外
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (DirectoryNotFoundException ex)
            {  //ファイル/フォルダが見つからない例外
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (IOException ex)
            {                 //I/Oエラーが発生例外
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (NotSupportedException ex)
            {       //メソッドがサポートされていない例外
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (UnauthorizedAccessException ex)
            { //OSがI/Oエラーやセキュリティエラーアクセス拒否例外
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {                   //アプリケーション実行中エラー例外
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
            }
        }
        /// <summary>
        ///  ファイル保護：UIWriteProtect.xml：書込み
        /// </summary>
        private void WriteProtectXML_Write()
        {
            try
            {
                System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                // XML宣言を設定する
                System.Xml.XmlDeclaration xmlDecl =
                    xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                //作成したXML宣言をDOMドキュメントに追加します
                xmlDoc.AppendChild(xmlDecl);
                //ルート要素の作成
                XmlElement elem = xmlDoc.CreateElement("Root");
                xmlDoc.AppendChild(elem);
                string stringExtension = selectedRadioButtonEx();//各ボタンの拡張子
                if (_st1LineDat != null)
                {
                    for (int intCount = 0; intCount < _st1LineDat.Length; intCount++)
                    {
                        string stringFile = _st1LineDat[intCount].itemName.ToString();
                        if (stringFile.IndexOf(stringExtension) < 0)
                        {//関係無い拡張子
                        }else{
                            if (directoryListViewComp(_st1LineDat[intCount].itemName) == false)
                            {//既に無いファイル は"UIWriteProtect.xml"に記録しない。
                                continue;
                            }
                        }
                        XmlElement item_elem = xmlDoc.CreateElement("Item");//各行のItemは同じ
                        item_elem.SetAttribute("ItemName", _st1LineDat[intCount].itemName);
                        item_elem.SetAttribute("protectLevel", _st1LineDat[intCount].protectLevel.ToString());
                        elem.AppendChild(item_elem);
                    }
                }
                try
                {
                    //作成したDOMドキュメントをファイルに保存
                    string masterFolder = @FilePathInfo.MasterData;//\Masterフォルダ
                    xmlDoc.Save(masterFolder + "UIWriteProtect.xml");
                }
                catch (System.Xml.XmlException ex)
                {
                    //XMLによる例外をキャッチ
                    Console.WriteLine(ex.Message);
                }
                //例外処理
            }
            catch (ArgumentException ex)
            {  //引数例外
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (DirectoryNotFoundException ex)
            {  //ファイル/フォルダが見つからない例外
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (IOException ex)
            {                 //I/Oエラーが発生例外
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (NotSupportedException ex)
            {       //メソッドがサポートされていない例外
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (UnauthorizedAccessException ex)
            { //OSがI/Oエラーやセキュリティエラーアクセス拒否例外
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {                   //アプリケーション実行中エラー例外
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
            }
        }
        /// <summary>
        /// 選択されたラジオボタンの拡張子を取得
        /// </summary>
        /// <returns></returns>
        private string selectedRadioButtonEx()
        {
            string extension = "";
            if (radioButtonEx_Program.Checked)
            {
                extension = "PRG";
            }
            else if (radioButtonEx_GCode.Checked)
            {
                extension = "UGC";
            }
            else if (radioButtonEx_MCode.Checked)
            {
                extension = "UMC";
            }
            return extension;
        }
        #endregion

        /// <summary>
        /// リストビュー：列がクリックされた
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void directoryListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            //クリックされた列を設定
            listViewItemSorter.Column = e.Column;
            //並び替える
            directoryListView.Sort();
        }
        //#region<ボタン：クリック>
        System.Drawing.Color _directoryListViewForeColor = System.Drawing.Color.Black;
        System.Drawing.Color _directoryListViewBackColor = System.Drawing.Color.White;
        /// <summary>
        /// リストビュー：クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void directoryListView_Click(object sender, System.EventArgs e)
        {
            //statusBarにファイル名までのFullPathを表示
            string s1, s2, s3;
            if (treeView1.SelectedNode == null) return;
            if (treeView1.SelectedNode.FullPath.Length <= 2)
            {
                s1 = treeView1.SelectedNode.FullPath.Substring(0, 2);
                s2 = "\\";
                //s2は、s2 = ""; のことです。
            }
            else
            {
                s1 = treeView1.SelectedNode.FullPath.Substring(0, 3);
                s2 = treeView1.SelectedNode.FullPath.Substring(3, (treeView1.SelectedNode.FullPath.Length - 3));
            }
            s3 = "\\";
            if (directoryListView.SelectedItems.Count > 0)
            {
                s3 = "\\" + directoryListView.SelectedItems[0].Text;
            }
            switch (_mode)
            {
                case FileFormMode.OpenProgram:
                case FileFormMode.OpenMacro:
                case FileFormMode.OpenRenishaw:
                    statusBar.Text = s1 + s2 + s3;
                    _filePath = s1 + s2 + s3;
                    _directoryPath = s1 + s2;
                    OpenDirectoryPath = s1 + s2 + s3;
                    break;
                case FileFormMode.SaveProgram:
                case FileFormMode.SaveMacro:
                case FileFormMode.RenameProgram://追加
                    statusBar.Text = s1 + s2 + s3;
                    _filePath = s1 + s2 + s3;
                    _directoryPath = s1 + s2;
                    SaveDirectoryPath = s1 + s2;
                    break;
                case FileFormMode.ProcLogDelete://加工ログ      ：削除
                case FileFormMode.UserLogDelete://ユーザーログ  ：削除
                    _returnPath = treeView1.SelectedNode.FullPath + "\\" + directoryListView.SelectedItems[0].Text;
                    return;
                case FileFormMode.ProcLogExport:    //ログ出力          ：EXPORT
                case FileFormMode.UserLogExport:    //ユーザーログ出力  ：EXPORT
                case FileFormMode.AlamLogHistExport://アラームログ履歴  ：EXPORT
                                                    //選択行に色を着ける
                    int selIndex = _selectedIndices;//選択された行位置
                    if (directoryListView.Items[selIndex].BackColor == FileUIStyleTable.DefaultBackColor)
                    {
                        directoryListView.Items[selIndex].ForeColor = _directoryListViewForeColor;
                        directoryListView.Items[selIndex].BackColor = _directoryListViewBackColor;
                    }
                    else
                    {
                        _directoryListViewForeColor = directoryListView.Items[selIndex].ForeColor;//記録
                        _directoryListViewBackColor = directoryListView.Items[selIndex].BackColor;//記録
                        directoryListView.Items[selIndex].ForeColor = FileUIStyleTable.DefaultForeColor;                                                                                              //directoryListView.Items[selIndex].BackColor = System.Drawing.Color.Red;
                        directoryListView.Items[selIndex].BackColor = FileUIStyleTable.DefaultBackColor;
                    }
                    break;
                default:
                    statusBar.Text = s1 + s2 + s3;
                    _filePath = s1 + s2 + s3;
                    _directoryPath = s1 + s2;

                    //_directoryPath = _directoryPath.Replace( ":\\\\", ":/" );
                    //_directoryPath = _directoryPath.Replace( "\\", "/" );
                    break;
            }
            int retValue = ShowSelectDialog();
            if (retValue == 0)
            {
                if (_subForm != null)
                {//インスタンスがあれば閉じる：モーダレス化による追加
                    _subForm.Close();
                    _subForm = null;
                }
            }
        }

        int _selectedIndices = -1;//選択された行位置
        /// <summary>
        /// リストビュー：項目選択不可
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void directoryListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (_mode)
            {
                case FileFormMode.ProcLogExport:    //加工ログ出力：Export
                case FileFormMode.UserLogExport:    //ユーザーログ出力：Export
                case FileFormMode.AlamLogHistExport://アラームログ履歴：Export
                                                    //リフレッシュすると複数選択が消えるので、ここで閉じる
                    if (directoryListView.SelectedIndices.Count == 0) return;  //未選択
                    _selectedIndices = directoryListView.SelectedIndices[0];    //選択された行位置：記録
                    directoryListView.SelectedIndices.Clear();
                    return;
            }
        }
        /// <summary>
        /// リストビュー：リフレッシュ
        /// </summary>
        /// <param name="fullPath"></param>
        private void RefreshListView(string fullPath)
        {
            switch (_mode)
            {
                case FileFormMode.ProcLogExport:    //加工ログ出力：EXPORT
                case FileFormMode.UserLogExport:    //ユーザーログ：EXPORT
                case FileFormMode.AlamLogHistExport://アラームログ履歴：EXPORT
                                                    //リフレッシュすると複数選択が消えるので、ここで閉じる
                    return;
            }
            //リストビューを再表示させる。
            directoryListView.Items.Clear();//これがないとファイル表示が累積
            DirectoryInfo di = new DirectoryInfo(fullPath);

            if (di.Exists)
            {
                try
                {
                    switch (_mode)
                    {   //NCプログラム
                        case FileFormMode.OpenProgram:
                        case FileFormMode.SaveProgram:
                        case FileFormMode.RenameProgram://追加
                            foreach (FileInfo file in di.GetFiles("*.PGM"))
                            {
                                string[] CollumListAdd = { file.Name, Convert.ToString(file.CreationTime) };
                                //string[] CollumListAdd = { file.Name, Convert.ToString(file.CreationTimeUtc) };//UTC＝イギリスのグリニッジ標準時
                                directoryListView.Items.Add(new ListViewItem(CollumListAdd));
                            }
                            break;
                        //マクロ
                        case FileFormMode.OpenMacro:
                        case FileFormMode.SaveMacro:
                            foreach (FileInfo file in di.GetFiles("*.VAR"))
                            {
                                string[] CollumListAdd = { file.Name, Convert.ToString(file.CreationTime) };
                                //string[] CollumListAdd = { file.Name, Convert.ToString(file.CreationTimeUtc) };UTC＝イギリスのグリニッジ標準時
                                directoryListView.Items.Add(new ListViewItem(CollumListAdd));
                            }
                            break;
                        //アラームログ履歴
                        case FileFormMode.AlamLogHistOpen:
                        case FileFormMode.AlamLogHistExport:
                            foreach (FileInfo file in di.GetFiles("*.XML"))
                            {
                                string[] CollumListAdd = { file.Name, Convert.ToString(file.CreationTime) };
                                //string[] CollumListAdd = { file.Name, Convert.ToString(file.CreationTimeUtc) };UTC＝イギリスのグリニッジ標準時
                                directoryListView.Items.Add(new ListViewItem(CollumListAdd));
                            }
                            break;
                        case FileFormMode.ProcLogDelete: //加工ログ削除：
                            foreach (FileInfo file in di.GetFiles("*.TXT"))
                            {
                                string[] CollumListAdd = { file.Name, Convert.ToString(file.CreationTime) };
                                directoryListView.Items.Add(new ListViewItem(CollumListAdd));
                            }
                            break;
                        case FileFormMode.UserLogDelete: //ユーザーログ削除：2017-07-26：追加：柏原
                            foreach (FileInfo file in di.GetFiles("*.TXT"))
                            {//TXTは仮
                                string[] CollumListAdd = { file.Name, Convert.ToString(file.CreationTime) };
                                directoryListView.Items.Add(new ListViewItem(CollumListAdd));
                            }
                            break;
                    }
                }
                catch (Exception exc)
                {//例外処理
                    MessageBox.Show(exc.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            statusBarTextShow(fullPath);                  //★statusBarにFullPathを表示する下記メソッド                                                                 
        }
        #endregion
        #region<ステータス・バー>
        /// <summary>
        /// ステータスバーにファイルパス表示
        /// </summary>
        /// <param name="fullPath"></param>
        private void statusBarTextShow(string fullPath)
        {
            //★statusBarにフォルダ名までのFullPathを表示
            //
            //FolderStatus.Text = e.Node.FullPath;とすると、
            //Environment.GetLogicalDrives()を使うと取得ドライブ名に\マークが付き（例：C:\）、
            //FullPathを取得すると\マークが付く（例：\WINDOWS）ので、
            //FolderStatus.TextにFullPathを入れると「C:\\WINDOWS」となってしまう。。。
            //ちなみに、
            //string s1 = e.Node.FullPath.Substring(0,2);
            //string s2 = e.Node.FullPath.Substring(3,(e.Node.FullPath.Length - 3));
            //FolderStatus.Text = s1 + s2;
            //これではドライブ初期表示に\マークが表示されない（例：C:）

            string s1, s2, s3;                    //20160324//ファイル名取得の為追記                                              
            int i1 = fullPath.LastIndexOf('\\');                      //20160324//ファイル名取得の為追記

            if (fullPath.Length <= 2)
            {
                s1 = fullPath.Substring(0, 2);
                s2 = "\\";
                //s2は、s2 = ""; のことです。

                //PassTextBox.Text = s1;                                  //20160324//ファイル名取得の為追記
            }
            else
            {
                s1 = fullPath.Substring(0, 3);
                s2 = fullPath.Substring(3, (fullPath.Length - 3));
                s3 = fullPath.Substring(i1, (fullPath.Length - i1));//20160324//ファイル名取得の為追記

                //PassTextBox.Text = s3.Replace("\\", "");                //20160324//ファイル名取得の為追記
            }
            switch (_mode)
            {
                case FileFormMode.OpenProgram:
                    statusBar.Text = s1 + s2;
                    _directoryPath = s1 + s2;
                    _filePath = s1 + s2;
                    OpenDirectoryPath = s1 + s2;
                    break;
                case FileFormMode.SaveProgram:
                case FileFormMode.RenameProgram://追加
                    statusBar.Text = s1 + s2;
                    _filePath = s1 + s2;
                    _directoryPath = s1 + s2;
                    SaveDirectoryPath = s1 + s2;
                    break;
                case FileFormMode.ProcLogExport://加工ログ
                case FileFormMode.UserLogExport://ユーザーログ：出力
                case FileFormMode.UserLogDelete://ユーザーログ：削除
                case FileFormMode.ProcLogDelete://プログラムログ：削除
                    statusBar.Text = s1 + s2;
                    _filePath = s1 + s2;
                    _directoryPath = s1 + s2;//これが上位に戻ります。
                    break;
                default:
                    break;
            }
            labelEx_Status.Text = statusBar.Text;//フォルダ表示追加：柏原
        }
        #endregion
        #region<選択メニューダイアログ>
        /// <summary>
        /// 選択メニューダイアログ：表示
        /// </summary>
        private int ShowSelectDialog()
        {
            int retValue = 0;//3=名前変更
            try
            {
                switch (_mode)
                {
                    case FileFormMode.All:              //全て
                    case FileFormMode.ProcLogDelete:    //加工ログ：削除
                    case FileFormMode.UserLogDelete:    //ユーザーログ：削除
                    case FileFormMode.AlamLogHistOpen:  //アラームログ履歴：ログを開く
                        _returnPath = directoryListView.SelectedItems[0].Text;//ファイル名のみ

                        if (_directoryPath.EndsWith("/") == false) _directoryPath += "\\";
                        _returnFullPath = _directoryPath + directoryListView.SelectedItems[0].Text;  //フルパス
                                                                                                     //ツリービュウで「c;\\\\ED_LOG\\」になったパスを「c:/ED_LOG/」に変換します。
                        _returnFullPath = _returnFullPath.Replace(":\\\\", ":/");
                        _returnFullPath = _returnFullPath.Replace("\\", "/");

                        this.Close();                                                               //この画面を閉じる
                        break;
                    case FileFormMode.OpenRenishaw:
                        _returnPath = directoryListView.SelectedItems[0].Text;          //ファイル名のみ
                        _returnFullPath = Path.Combine(_directoryPath, _returnPath);    //フルパス
                        this.Close();                                                   //この画面を閉じる
                        break;
                    case FileFormMode.AlamLogHistExport://アラームログ履歴：EXPORT(USB出力) 
                    case FileFormMode.ProcLogExport:    //加工ログ：EXPORT(USB出力) 
                        break;
                    case FileFormMode.OpenProgram://NCプログラム：
                        ReturnMessage receiveOpenProgram = SelectCommandsDialog.ShowSubForm(this, statusBar.Text.Remove(0, statusBar.Text.LastIndexOf("\\")), new string[] { "開く", "削除", "名前変更", "書き込み保護" }); if ((this.IsDisposed == true || this.Disposing == true)) return 0; EndInvoke(retInvoke);
                        //A1 = 開く　A2 = 削除　A3 = 名前変更　A4 = 書き込み保護        
                        switch (receiveOpenProgram)
                        {
                            case ReturnMessage.Cancel: return 0;

                            case ReturnMessage.ExecuteA1://開く
                                _returnPath = OpenDirectoryPath;
                                _returnFullPath = OpenDirectoryPath;
                                //呼び出し先に通知
                                _notifyReturn.Invoke();
                                break;

                            case ReturnMessage.ExecuteA2://削除
                                if (Directory.Exists(_filePath))
                                {
                                    DeleteFileOrFolder(_directoryPath, SystemIoType.Folder);
                                }
                                else
                                {
                                    DeleteFileOrFolder(_filePath, SystemIoType.File);
                                }
                                break;

                            case ReturnMessage.ExecuteA3://名前変更
                                retValue = 3;
                                if (Directory.Exists(_filePath))
                                {
                                    //string NewName = EditTextDialog.ShowSubForm("新しいフォルダ名を入力してください。");
                                    //if (!NewName.Contains(".txt"))
                                    //{
                                    //    NewName += ".txt";
                                    //}
                                    //if (_directoryPath.LastIndexOf("\\") == _directoryPath.Length - 1)
                                    //{
                                    //    NewName = _directoryPath.Remove(_directoryPath.LastIndexOf("\\"), _directoryPath.Length - 1).Remove(_directoryPath.LastIndexOf("\\"), _directoryPath.Length - 1) + NewName;
                                    //}
                                    //else
                                    //{
                                    //    NewName = _directoryPath.Remove(_directoryPath.LastIndexOf("\\"), _directoryPath.Length - 1) + NewName;
                                    //}

                                    //CopyDirectory(_directoryPath, NewName);                                
                                }
                                else
                                {
                                    _BeforefilePath = _filePath;
                                    //ポップアップキーボード表示
                                    OpenKeyboardDialog(directoryListView.SelectedItems[0].Text);
                                }
                                break;

                            case ReturnMessage.ExecuteA4://書込み保護
                                int rowCount = directoryListView.Items.Count;           //行数
                                int selRowCount = directoryListView.FocusedItem.Index;  //選択行
                                string stringFileName = directoryListView.Items[selRowCount].SubItems[0].Text;
                                int intLoop = 0;

                                if (_st1LineDat == null)
                                {//ファイルが無い場合、データを作成
                                 //要素数を増やす
                                    Array.Resize(ref _st1LineDat, intLoop + 1);
                                    _st1LineDat[0].itemName = stringFileName;
                                }
                                bool boolNewData = true;//true = UIWriteProtect.xmlに無いファイル
                                for (; intLoop < _st1LineDat.Length; intLoop++)
                                {
                                    if (stringFileName == _st1LineDat[intLoop].itemName)
                                    {
                                        switch (_st1LineDat[intLoop].protectLevel)
                                        {//トグルなので有れば消し、無ければ表示
                                            case 2://金鍵アイコン
                                            case 1://鍵アイコン
                                                _st1LineDat[intLoop].protectLevel = 0;
                                                directoryListView.SmallImageList = imageList1;
                                                directoryListView.Items[selRowCount].ImageIndex = 0;//無しアイコン
                                                boolNewData = false;//UIWriteProtect.xmlに有るファイル
                                                break;
                                            case 0://無しアイコン
                                            default://その他アイコン
                                                if (_goldKeyIcon)
                                                {
                                                    _st1LineDat[intLoop].protectLevel = 2;
                                                    directoryListView.SmallImageList = imageList1;//表示
                                                    directoryListView.Items[selRowCount].ImageIndex = 2;//金鍵アイコン
                                                }
                                                else
                                                {
                                                    _st1LineDat[intLoop].protectLevel = 1;
                                                    directoryListView.SmallImageList = imageList1;//表示
                                                    directoryListView.Items[selRowCount].ImageIndex = 1;//鍵アイコン
                                                }
                                                boolNewData = false;//UIWriteProtect.xmlに有るファイル
                                                break;
                                        }
                                    }
                                }
                                if (boolNewData)
                                {//新規(UIWriteProtect.xmlに無いのでここで追加)
                                    //要素数を増やす
                                    Array.Resize(ref _st1LineDat, intLoop + 1);
                                    _st1LineDat[intLoop].index = 0;
                                    _st1LineDat[intLoop].itemName = stringFileName;
                                    if (_goldKeyIcon)
                                    {
                                        _st1LineDat[intLoop].protectLevel = 2;
                                        directoryListView.SmallImageList = imageList1;//表示
                                        directoryListView.Items[selRowCount].ImageIndex = 2;//金鍵アイコン
                                    }
                                    else
                                    {
                                        _st1LineDat[intLoop].protectLevel = 1;
                                        directoryListView.SmallImageList = imageList1;//表示
                                        directoryListView.Items[selRowCount].ImageIndex = 1;//鍵アイコン
                                    }
                                }
                                WriteProtectXML_Write();//UIWriteProtect.xmlに書込み
                                return 0;//鍵アイコン更新だけならここでリターンし下位処理をさせない。柏原
                        }
                        break;

                    case FileFormMode.SaveProgram://NCプログラム：保存
                    case FileFormMode.RenameProgram://NCプログラム：名前変更
                        ReturnMessage receiveSaveProgram = SelectCommandsDialog.ShowSubForm(this, statusBar.Text.Remove(0, statusBar.Text.LastIndexOf("\\")),
                                                                                                new string[] { "", "削除", "名前変更", "書き込み保護" }); if ((this.IsDisposed == true || this.Disposing == true)) return 0; EndInvoke(retInvoke);
                        //A1 = ""　A2 = 削除　A3 = 名前変更　A4 = 書き込み保護        
                        switch (receiveSaveProgram)
                        {
                            case ReturnMessage.Cancel: return 0;

                            case ReturnMessage.ExecuteA1: return 0;

                            case ReturnMessage.ExecuteA2://削除
                                if (Directory.Exists(_filePath))
                                {
                                    DeleteFileOrFolder(_directoryPath, SystemIoType.Folder);
                                }
                                else
                                {
                                    DeleteFileOrFolder(_filePath, SystemIoType.File);
                                }
                                break;

                            case ReturnMessage.ExecuteA3://名前変更
                                retValue = 3;
                                if (Directory.Exists(_filePath))
                                {
                                    //string NewName = EditTextDialog.ShowSubForm("新しいフォルダ名を入力してください。");
                                    //if (!NewName.Contains(".txt"))
                                    //{
                                    //    NewName += ".txt";
                                    //}
                                    //if (_directoryPath.LastIndexOf("\\") == _directoryPath.Length - 1)
                                    //{
                                    //    NewName = _directoryPath.Remove(_directoryPath.LastIndexOf("\\"), _directoryPath.Length - 1).Remove(_directoryPath.LastIndexOf("\\"), _directoryPath.Length - 1) + NewName;
                                    //}
                                    //else
                                    //{
                                    //    NewName = _directoryPath.Remove(_directoryPath.LastIndexOf("\\"), _directoryPath.Length - 1) + NewName;
                                    //}

                                    //CopyDirectory(_directoryPath, NewName);                                
                                }
                                else
                                {
                                    string fileNameNonEx = System.IO.Path.GetFileNameWithoutExtension(_filePath);//ファイル名の取得
                                    string NewName = EditTextDialog.ShowSubForm(this,"新しいファイル名を入力してください。", fileNameNonEx);
                                    if (NewName == "")
                                    {
                                        _notifyReturn.Invoke();//呼び出し先に通知
                                        return 0;
                                    }
                                    if (NewName.Contains(".PGM") || NewName.Contains(".pgm"))
                                    {//OK：何もしない
                                     //if (!NewName.Contains(".PGM")//「.pgm.PGM」の不具合対応
                                    }
                                    else
                                    {//拡張子が無い
                                        NewName += ".PGM";
                                    }
                                    string fullNewName = _directoryPath + "\\" + NewName;
                                    if (File.Exists(NewName))
                                    {
                                        //同名ファイル：有り
                                        using (MessageDialog msg = new MessageDialog())
                                        {
                                            //同名ファイルが存在します、上書きしますか？
                                            string addMes = fullNewName +           //リストで選択されたファイル名
                                                             Environment.NewLine +  //２行隙間をあける
                                                             Environment.NewLine;
                                            bool dialogResult = msg.Question(5519, this, addMes);
                                            if (dialogResult == false)
                                            {
                                                break;//上書きしません。
                                            }
                                        }
                                        File.Copy(_filePath, fullNewName, true);//true=上書きコピー
                                    }
                                    else
                                    {//同名ファイル：無し
                                        File.Copy(_filePath, fullNewName);  //コピー
                                        File.Delete(_filePath);         //変更前ファイルを削除 
                                    }

                                    //フォルダ：更新
                                    //リストビュー：リフレッシュ
                                    RefreshListView(treeView1.SelectedNode.FullPath);//1本削除にFileFormModeが追加されたらRefreshListView()に追加します。

                                    int listMax =directoryListView.Items.Count;
                                    int nodeNum = -1;
                                    for (int loop=0; loop<listMax; loop++) {
                                        if(directoryListView.Items[loop].Text== NewName)
                                        {
                                            nodeNum = loop;
                                        }
                                    }
                                    if (nodeNum < 0)
                                    {
                                    }
                                    else
                                    {
                                        directoryListView.EnsureVisible(nodeNum);//見えるところまでスクロール
                                        //directoryListView.Items[nodeNum].Focused = true;//フォーカス
                                        //directoryListView.Items[nodeNum].Selected = true;//選択
                                        //directoryListView.Refresh();
                                        //Refresh();
                                    }
                                    // _returnPath = NewName;//モーダレス用：ここでは必要無し
                                }
                                break;

                            case ReturnMessage.ExecuteA4://書き込み保護
                                break;
                        }
                        break;
                    case FileFormMode.OpenMacro://マクロ
                        ReturnMessage receiveOpenMacro = SelectCommandsDialog.ShowSubForm(this, statusBar.Text.Remove(0, statusBar.Text.LastIndexOf("\\")), new string[] { "開く", "削除", "名前変更", "書き込み保護" }); if ((this.IsDisposed == true || this.Disposing == true)) return 0; EndInvoke(retInvoke);
                        //A1 = 開く　A2 = 削除　A3 = 名前変更　A4 = 書き込み保護        
                        switch (receiveOpenMacro)
                        {
                            case ReturnMessage.Cancel: return 0;

                            case ReturnMessage.ExecuteA1://開く
                                _returnPath = OpenDirectoryPath;
                                this.Close();

                                break;

                            case ReturnMessage.ExecuteA2://削除
                                if (Directory.Exists(_filePath))
                                {
                                    DeleteFileOrFolder(_directoryPath, SystemIoType.Folder);
                                }
                                else
                                {
                                    DeleteFileOrFolder(_filePath, SystemIoType.File);
                                }

                                break;

                            case ReturnMessage.ExecuteA3://名前変更
                                retValue = 3;
                                if (Directory.Exists(_filePath))
                                {
                                    //string NewName = EditTextDialog.ShowSubForm("新しいフォルダ名を入力してください。");
                                    //if (!NewName.Contains(".txt"))
                                    //{
                                    //    NewName += ".txt";
                                    //}
                                    //if (_directoryPath.LastIndexOf("\\") == _directoryPath.Length - 1)
                                    //{
                                    //    NewName = _directoryPath.Remove(_directoryPath.LastIndexOf("\\"), _directoryPath.Length - 1).Remove(_directoryPath.LastIndexOf("\\"), _directoryPath.Length - 1) + NewName;
                                    //}
                                    //else
                                    //{
                                    //    NewName = _directoryPath.Remove(_directoryPath.LastIndexOf("\\"), _directoryPath.Length - 1) + NewName;
                                    //}

                                    //CopyDirectory(_directoryPath, NewName);                                
                                }
                                else
                                {
                                    string NewName = EditTextDialog.ShowSubForm(this,"新しいファイル名を入力してください。");
                                    if (NewName == "")
                                    {
                                        _notifyReturn.Invoke();//呼び出し先に通知
                                        return 0;
                                    }
                                    if (!NewName.Contains(".VAR"))
                                    {
                                        NewName += ".VAR";
                                    }
                                    NewName = _directoryPath + "\\" + NewName;
                                    File.Copy(_filePath, NewName);
                                    File.Delete(_filePath);
                                }
                                break;

                            case ReturnMessage.ExecuteA4://書き込み保護
                                break;
                        }
                        break;

                    case FileFormMode.SaveMacro://マクロ
                        ReturnMessage receiveSaveMacro = SelectCommandsDialog.ShowSubForm(this, statusBar.Text.Remove(0, statusBar.Text.LastIndexOf("\\")),
                                                                                                new string[] { "", "削除", "名前変更", "書き込み保護" }); if ((this.IsDisposed == true || this.Disposing == true)) return 0; EndInvoke(retInvoke);
                        //A1 = ""　A2 = 削除　A3 = 名前変更　A4 = 書き込み保護        
                        switch (receiveSaveMacro)
                        {
                            case ReturnMessage.Cancel: return 0;

                            case ReturnMessage.ExecuteA1: return 0;

                            case ReturnMessage.ExecuteA2://削除
                                if (Directory.Exists(_filePath))
                                {
                                    DeleteFileOrFolder(_directoryPath, SystemIoType.Folder);
                                }
                                else
                                {
                                    DeleteFileOrFolder(_filePath, SystemIoType.File);
                                }
                                _notifyReturn.Invoke();//呼び出し先に通知
                                break;

                            case ReturnMessage.ExecuteA3://名前変更
                                retValue = 3;
                                if (Directory.Exists(_filePath))
                                {
                                    //string NewName = EditTextDialog.ShowSubForm("新しいフォルダ名を入力してください。");
                                    //if (!NewName.Contains(".txt"))
                                    //{
                                    //    NewName += ".txt";
                                    //}
                                    //if (_directoryPath.LastIndexOf("\\") == _directoryPath.Length - 1)
                                    //{
                                    //    NewName = _directoryPath.Remove(_directoryPath.LastIndexOf("\\"), _directoryPath.Length - 1).Remove(_directoryPath.LastIndexOf("\\"), _directoryPath.Length - 1) + NewName;
                                    //}
                                    //else
                                    //{
                                    //    NewName = _directoryPath.Remove(_directoryPath.LastIndexOf("\\"), _directoryPath.Length - 1) + NewName;
                                    //}

                                    //CopyDirectory(_directoryPath, NewName);                                
                                }
                                else
                                {
                                    string NewName = EditTextDialog.ShowSubForm(this,"新しいファイル名を入力してください。");
                                    if (NewName == "")
                                    {
                                        _notifyReturn.Invoke();//呼び出し先に通知

                                        return 0;
                                    }
                                    if (!NewName.Contains(".VAR"))
                                    {
                                        NewName += ".VAR";
                                    }
                                    NewName = _directoryPath + "\\" + NewName;
                                    File.Copy(_filePath, NewName);
                                    File.Delete(_filePath);
                                }
                                break;

                            case ReturnMessage.ExecuteA4://書き込み保護
                                break;
                        }
                        break;
                }
                _notifyReturn.Invoke();//呼び出し先に通知

                RefreshListView(_directoryPath);//リストビュー：リフレッシュ
            }
            catch (Exception exp)
            {//例外処理
                ECNC3Exception.FileIOFilter(exp, this);
            }
            return retValue;
        }
        #endregion
        #region <ポップアップ・キーボード>
        private KeyboardDialog _keybord = null;
        /// <summary>
        /// ファイル名クリック時、ポップアップ・キーボード表示
        /// </summary>
        /// <param name="inputString"></param>
        private void OpenKeyboardDialog(string inputString)
        {
            if (_keybord == null)
            {
                _keybord = new KeyboardDialog();
                _keybord.Location = new System.Drawing.Point(0, 768 - _keybord.Height);
                _keybord.FormClosed += KeyboardClosed;
                _keybord.InputValue = inputString;
                _keybord.NotifyReturn = _edtComment_ClickCallBack;
                _keybord.NotifyTextChanged = _edtComment_ClickTextChanged;
                _keybord.Show(this);
                _keybord.FocusedInputForm();
                _NewFileName = inputString;
            }
            else
            {
                _keybord.Close();
                _keybord = null;
            }
        }
        /// <summary>
        /// ポップアップキーボード：閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyboardClosed(object sender, FormClosedEventArgs e)
        {
            if (_keybord != null)
            {
                _keybord.FormClosed -= KeyboardClosed;
                _keybord = null;
            }
        }
        //ポップアップキーボード：ファイル：フルパス
        private string _BeforefilePath;
        /// <summary>ポップアップキーボード：入力からの終了要求</summary>
        private void _edtComment_ClickCallBack()
        {
            ResetInput();
            string NewName = _NewFileName;//戻ってきた入力値

            //先頭と最後のスペース削除
            _BeforefilePath = _BeforefilePath.Trim();
            NewName = NewName.Trim();
            if (NewName == "")
            {
                return;
            }
            if (NewName.Contains(".PGM") || NewName.Contains(".pgm"))
            {//OK：何もしない
             //if (!NewName.Contains(".PGM")//「.pgm.PGM」の不具合対応
            }
            else
            {//拡張子が無い
                NewName += ".PGM";
            }
            NewName = _directoryPath + "\\" + NewName;
            if (_BeforefilePath == NewName)
            {//同名の場合、何もしません。※File.Copy()で同名の場合、例外が出る
            }
            else
            {
                //異なる場合、コピー後、元ファイル削除
                File.Copy(_BeforefilePath, NewName, true);//上書き
                File.Delete(_BeforefilePath);
                RefreshListView(_directoryPath);//リストビュー：リフレッシュ
            }
        }
        //ポップアップキーボード：新しい入力文字列
        string _NewFileName;
        /// <summary>ポップアップキーボード：入力からの表示文字列の更新</summary>
        private void _edtComment_ClickTextChanged(string text)
        {
            _NewFileName = text;
        }
        /// <summary>ポップアップキーボード：入力状態の初期化</summary>
        public void ResetInput()
        {
            if (null != _keybord)
            {
                _keybord.Close();
                _keybord = null;
            }
        }
        #endregion
        #region<ファイル操作>
        /// <summary>
        /// ファイルやフォルダ：削除
        /// </summary>
        /// <param name="path"></param>
        /// <param name="FileOrFolder"></param>
        /// <returns></returns>
        private FileCommand DeleteFileOrFolder(string path, SystemIoType FileOrFolder)
        {
            if (path == "") return FileCommand.Failed;
            switch (FileOrFolder)
            {
                case SystemIoType.File:
                    try
                    {
                        //ファイルを削除します。&#xD;&#xA;よろしいですか？" 
                        using (MessageDialog msg = new MessageDialog())
                        {
                            if (false == msg.Question(5501, this, path + "\n"))
                            {
                                return FileCommand.Failed;
                            }
                        }
                        //削除
                        File.Delete(path);
                    }
                    catch (ArgumentException ex) { MessageBox.Show(ex.Message); }
                    catch (DirectoryNotFoundException ex) { MessageBox.Show(ex.Message); }
                    catch (IOException ex) { MessageBox.Show(ex.Message); }
                    catch (NotSupportedException ex) { MessageBox.Show(ex.Message); }
                    catch (UnauthorizedAccessException ex) { MessageBox.Show(ex.Message); }
                    return FileCommand.Success;

                case SystemIoType.Folder:
                    try
                    {
                        //フォルダを削除します。&#xD;&#xA;よろしいですか？" 
                        using (MessageDialog msg = new MessageDialog())
                        {
                            if (false == msg.Question(5502, this))
                            {
                                return FileCommand.Failed;
                            }
                        }
                        Directory.Delete(path, true);
                    }
                    catch (ArgumentException ex) { MessageBox.Show(ex.Message); }
                    catch (DirectoryNotFoundException ex) { MessageBox.Show(ex.Message); }
                    catch (IOException ex) { MessageBox.Show(ex.Message); }
                    catch (NotSupportedException ex) { MessageBox.Show(ex.Message); }
                    catch (UnauthorizedAccessException ex) { MessageBox.Show(ex.Message); }
                    return FileCommand.Success;
            }
            return FileCommand.Failed;
        }
        /// <summary>
        /// ファイルやフォルダ：変更
        /// </summary>
        /// <param name="path"></param>
        /// <param name="NewName"></param>
        /// <param name="FileOrFolder"></param>
        /// <returns></returns>
        private FileCommand NameChgFileOrFolder(string path, string NewName, SystemIoType FileOrFolder)
        {
            if (path == "") return FileCommand.Failed;
            switch (FileOrFolder)
            {
                case SystemIoType.File:
                    // ファイルをコピーする
                    try
                    {
                        System.IO.File.Copy(path, NewName, false);
                    }
                    catch (UnauthorizedAccessException ex) { MessageBox.Show(ex.Message); }
                    catch (ArgumentException ex) { MessageBox.Show(ex.Message); }
                    catch (PathTooLongException ex) { MessageBox.Show(ex.Message); }
                    catch (DirectoryNotFoundException ex) { MessageBox.Show(ex.Message); }
                    catch (FileNotFoundException ex) { MessageBox.Show(ex.Message); }
                    catch (IOException ex) { MessageBox.Show(ex.Message); }
                    catch (NotSupportedException ex) { MessageBox.Show(ex.Message); }
                    return FileCommand.Success;

                case SystemIoType.Folder:
                    // フォルダを削除する                    
                    try
                    {
                        CopyDirectory(path, NewName, true);
                    }
                    catch (IOException ex) { MessageBox.Show(ex.Message); }
                    catch (UnauthorizedAccessException ex) { MessageBox.Show(ex.Message); }
                    catch (ArgumentException ex) { MessageBox.Show(ex.Message); }
                    catch (NotSupportedException ex) { MessageBox.Show(ex.Message); }
                    return FileCommand.Success;
            }
            return FileCommand.Failed;
        }

        /// ------------------------------------------------------------------------------------
        /// <summary>
        ///     ファイルまたはディレクトリ、およびその内容を新しい場所にコピーします。
        ///     新しい場所に同名のファイルがある場合は上書きしません。<summary>
        /// <param name="stSourcePath">
        ///     コピー元のディレクトリのパス。</param>
        /// <param name="stDestPath">
        ///     コピー先のディレクトリのパス。</param>
        /// ------------------------------------------------------------------------------------
        public static void CopyDirectory(string stSourcePath, string stDestPath)
        {
            CopyDirectory(stSourcePath, stDestPath, false);
        }


        /// ------------------------------------------------------------------------------------
        /// <summary>
        ///     ファイルまたはディレクトリ、およびその内容を新しい場所にコピーします。<summary>
        /// <param name="stSourcePath">
        ///     コピー元のディレクトリのパス。</param>
        /// <param name="stDestPath">
        ///     コピー先のディレクトリのパス。</param>
        /// <param name="bOverwrite">
        ///     コピー先が上書きできる場合は true。それ以外の場合は false。</param>
        /// ------------------------------------------------------------------------------------
        public static void CopyDirectory(string stSourcePath, string stDestPath, bool bOverwrite)
        {
            // コピー先のディレクトリがなければ作成する
            if (!System.IO.Directory.Exists(stDestPath))
            {
                System.IO.Directory.CreateDirectory(stDestPath);
                System.IO.File.SetAttributes(stDestPath, System.IO.File.GetAttributes(stSourcePath));
                bOverwrite = true;
            }

            // コピー元のディレクトリにあるすべてのファイルをコピーする
            if (bOverwrite)
            {
                foreach (string stCopyFrom in System.IO.Directory.GetFiles(stSourcePath))
                {
                    string stCopyTo = System.IO.Path.Combine(stDestPath, System.IO.Path.GetFileName(stCopyFrom));
                    System.IO.File.Copy(stCopyFrom, stCopyTo, true);
                }

                // 上書き不可能な場合は存在しない時のみコピーする
            }
            else
            {
                foreach (string stCopyFrom in System.IO.Directory.GetFiles(stSourcePath))
                {
                    string stCopyTo = System.IO.Path.Combine(stDestPath, System.IO.Path.GetFileName(stCopyFrom));

                    if (!System.IO.File.Exists(stCopyTo))
                    {
                        System.IO.File.Copy(stCopyFrom, stCopyTo, false);
                    }
                }
            }

            // コピー元のディレクトリをすべてコピーする (再帰)
            foreach (string stCopyFrom in System.IO.Directory.GetDirectories(stSourcePath))
            {
                string stCopyTo = System.IO.Path.Combine(stDestPath, System.IO.Path.GetFileName(stCopyFrom));
                CopyDirectory(stCopyFrom, stCopyTo, bOverwrite);
            }
        }
        #endregion
        #region<ウェイトカーソル中央表示>
        /// <summary>
        /// ウェイトカーソル中央表示
        /// </summary>
        private void CenterWaitCursor()
        {
            //真ん中表示
            //MainFormの幅と高さ取得
            Form mainForm = new MAINForm();
            int pWidth = mainForm.Size.Width;
            int pHeight = mainForm.Size.Height;
            mainForm.Dispose();//mainForm破棄
                               //MainFormの中心を取得
            int pHarfWidth = pWidth / 2;
            int pHarfHeight = pHeight / 2;
            ////WAITカーソル(マウスポインタ)位置をセンターに設定
            System.Windows.Forms.Cursor.Position = new System.Drawing.Point(pHarfWidth, pHarfHeight);
            Cursor.Current = Cursors.WaitCursor;
        }
        #endregion
        #region<各ボタン：クリック>
        /// <summary>
        /// 指定拡張子でリストビューの内容を表示
        /// </summary>
        /// <param name="ext">拡張子</param>
        /// <param name="di">ファイル情報</param>
        private void ShowListView(string extension=null, DirectoryInfo di=null)
        {
            if(extension == null)
            {//拡張子情報が無い場合、選択されているラジオボタンから判断
                extension = selectedRadioButtonEx();
            }
            if (di == null)
            {//ファイル情報が無い場合、作成
                di = new DirectoryInfo(treeView1.SelectedNode.FullPath);
            }
            int loopCont = 0;//全部のリスト数
            WriteProtectXML_Read(ref loopCont);//UIWriteProtect.xml：読込み

            int lineCont = 0;
            foreach (FileInfo file in di.GetFiles("*." + extension))
            {
                string[] CollumListAdd = { file.Name, Convert.ToString(file.LastWriteTime) };//更新日時：変更：柏原
                directoryListView.Items.Add(new ListViewItem(CollumListAdd));
                if (_showProgButton)
                {//鍵アイコンを表示
                    directoryListView.SmallImageList = imageList1;//表示
                    for (int cnt = 0; cnt < loopCont; cnt++)
                    {
                        if (CollumListAdd[0] == _st1LineDat[cnt].itemName)//ファイル名比較
                        {
                            switch ((WriteProtectLevel)_st1LineDat[cnt].protectLevel)
                            {
                                case WriteProtectLevel.None:
                                    directoryListView.Items[lineCont].ImageIndex = 0; break;//鍵無しアイコン
                                case WriteProtectLevel.Guest:
                                    directoryListView.Items[lineCont].ImageIndex = 1; break;//鍵アイコン
                                case WriteProtectLevel.User:
                                    directoryListView.Items[lineCont].ImageIndex = 2; break;//金鍵アイコン
                                default:
                                    directoryListView.Items[lineCont].ImageIndex = 0; break;//鍵無しアイコン
                            }
                        }
                    }
                }
                lineCont++;
            }
        }
        /// <summary>
        /// プログラム：チェックされた
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void radioButtonEx_Program_CheckedChanged(object sender, EventArgs e)
        {
            directoryListView.Items.Clear();//リスト削除
            ShowListView("PGM");
        }
        /// <summary>
        /// Gコード：チェックされた
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonEx_GCode_CheckedChanged(object sender, EventArgs e)
        {
            directoryListView.Items.Clear();//リスト削除
            ShowListView("UGC");
        }
        /// <summary>
        /// Mコード：チェックされた
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonEx_MCode_CheckedChanged(object sender, EventArgs e)
        {
            directoryListView.Items.Clear();//リスト削除
            ShowListView("UMC");
        }
        /// <summary>
        /// 保存ボタン：クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _saveBt_Click(object sender, EventArgs e)
        {
            string retPath = "";
            if (//ファイル名ダイアログ非表示
                _mode == FileFormMode.ProcLogExport ||      //加工ログ：EXPORT
                _mode == FileFormMode.UserLogExport ||      //ユーザーログ：EXPORT
                _mode == FileFormMode.AlamLogHistExport     //アラームログ履歴：EXPORT
                )
            {   //アラーム履歴：EXPORT
            }
            else
            {
                retPath = EditTextDialog.ShowSubForm(this,"ファイル名を入力してください。",_fileNameNonEx);
                if (retPath == "") return;
            }

            switch (_mode)
            {
                case FileFormMode.SaveProgram://NCプログラム：保存
                    if (!retPath.Contains(".PGM")) { SaveDirectoryPath += "\\" + retPath + ".PGM"; } else { SaveDirectoryPath += "\\" + retPath; }
                    _returnPath = SaveDirectoryPath;
                    //this.Close();//この画面を閉じる
                    break;
                case FileFormMode.SaveMacro://マクロ変数：保存
                    if (!retPath.Contains(".VAR")) { SaveDirectoryPath += "\\" + retPath + ".VAR"; } else { SaveDirectoryPath += "\\" + retPath; }
                    _returnPath = SaveDirectoryPath;
                    //this.Close();//この画面を閉じる
                    break;
                case FileFormMode.ProcLogExport:    //ログ出力
                case FileFormMode.UserLogExport:    //ユーザーログ出力
                case FileFormMode.AlamLogHistExport://アラームログ履歴
                    //選択されたファイル/フルパス名を上位に返します。
                    int loopMax = directoryListView.Items.Count;
                    int dataCount = 0; //選択数
                    for (int loopCount = 0; loopCount < loopMax; loopCount++)
                    {
                        //リスト中で選択されているか(前景色が着いているか)？
                        if (directoryListView.Items[loopCount].ForeColor != FileUIStyleTable.DefaultForeColor)
                        {
                            //ファイル名配列取得
                            Array.Resize(ref _returnPaths, _returnPaths.Length + 1);                                        //配列追加
                            _returnPaths[dataCount] = directoryListView.Items[loopCount].Text;                              //ファイル名
                            //フルパス名配列取得
                            Array.Resize(ref _returnFullPaths, _returnFullPaths.Length + 1);                                //配列追加
                            _directoryPath = _directoryPath.Replace(":\\\\", ":/");
                            _directoryPath = _directoryPath.Replace("\\", "/");
                            _returnFullPaths[dataCount] = _directoryPath + "/" + directoryListView.Items[loopCount].Text;   //フルパス名
                            dataCount++;
                        }
                    }
                    if (dataCount == 0)
                    {
                        //リストが未選択の場合、処理続行(「閉じる」が押されるまで)
                        return;
                    }
                    break;
            }
            //呼び出し先に通知
            _notifyReturn.Invoke();
        }
        /// <summary>
        /// 削除ボタン：クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _deleteBt_Click(object sender, EventArgs e)
        {
            if (_returnPath == "") return;//不具合表：番号15：修正：柏原

            string addMes = directoryListView.FocusedItem.Text +        //リストで選択されたファイル名
                                                 Environment.NewLine +  //２行隙間をあける
                                                 Environment.NewLine;
            if (directoryListView.Items.Count == 0) return;
            this.directoryListView.Items[0].Focused = true;
            this.directoryListView.Items[0].Selected = true;
            using (MessageDialog msg = new MessageDialog())
            {
                //title="FILE" txt="選択したファイルを削除します。&#xD;&#xA;よろしいですか？"
                if (msg.Question(5501, this, addMes ) == false) return;
            }
            //削除
            System.IO.File.Delete(_returnPath);
            //リストビュー：リフレッシュ
            RefreshListView(treeView1.SelectedNode.FullPath);//1本削除にFileFormModeが追加されたらRefreshListView()に追加します。
        }
    }
    #endregion

    #region<ListSortViewクラス>
    /// <summary>
    /// ListViewの項目の並び替えに使用するクラス
    /// </summary>
    public class ListSortView : IComparer
    {
        /// <summary>
        /// 比較する方法
        /// </summary>
        public enum ComparerMode
        {
            /// <summary>
            /// 文字列として比較
            /// </summary>
            String,
            /// <summary>
            /// 数値（Int32型）として比較
            /// </summary>
            Integer,
            /// <summary>
            /// 日時（DataTime型）として比較
            /// </summary>
            DateTime
        };

        private int _column;
        private SortOrder _order;
        private ComparerMode _mode;
        private ComparerMode[] _columnModes;

        /// <summary>
        /// 並び替えるListView列の番号
        /// </summary>
        public int Column
        {
            set
            {
                //現在と同じ列の時は、昇順降順を切り替える
                if (_column == value)
                {
                    if (_order == SortOrder.Ascending)
                    {
                        _order = SortOrder.Descending;
                    }
                    else if (_order == SortOrder.Descending)
                    {
                        _order = SortOrder.Ascending;
                    }
                }
                _column = value;
            }
            get
            {
                return _column;
            }
        }
        /// <summary>
        /// 昇順か降順か
        /// </summary>
        public SortOrder Order
        {
            set
            {
                _order = value;
            }
            get
            {
                return _order;
            }
        }
        /// <summary>
        /// 並び替えの方法
        /// </summary>
        public ComparerMode Mode
        {
            set
            {
                _mode = value;
            }
            get
            {
                return _mode;
            }
        }
        /// <summary>
        /// 列ごとの並び替えの方法
        /// </summary>
        public ComparerMode[] ColumnModes
        {
            set
            {
                _columnModes = value;
            }
        }
        /// <summary>
        /// ListSortView：コンストラクタ1
        /// </summary>
        /// <param name="col">並び替える列の番号</param>
        /// <param name="ord">昇順か降順か</param>
        /// <param name="cmod">並び替えの方法</param>
        public ListSortView(
            int col, SortOrder ord, ComparerMode cmod)
        {
            _column = col;
            _order = ord;
            _mode = cmod;
        }
		/// <summary>
		/// ListSortView：：コンストラクタ2
		/// </summary>
		public ListSortView()
        {
            _column = 0;
            _order = SortOrder.Ascending;       //列ごとの並び替え
            _mode = ComparerMode.String;
        }
		/// <summary>
		/// xがyより小さいときはマイナスの数、大きいときはプラスの数、
		/// 同じときは0を返す
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
        public int Compare(object x, object y)
        {
            if (_order == SortOrder.None)
            {
                //並び替えない時
                return 0;
            }
            int result = 0;
            //ListViewItemの取得
            ListViewItem itemx = (ListViewItem)x;
            ListViewItem itemy = (ListViewItem)y;

            //並べ替えの方法を決定
            if (_columnModes != null && _columnModes.Length > _column)
            {
                _mode = _columnModes[_column];
            }
            //並び替えの方法別に、xとyを比較する
            switch (_mode)
            {
                case ComparerMode.String:
                    //文字列をとして比較
                    result = string.Compare(itemx.SubItems[_column].Text,
                        itemy.SubItems[_column].Text);
                    break;
                case ComparerMode.Integer:
                    //Int32に変換して比較
                    //.NET Framework 2.0からは、TryParseメソッドを使うこともできる
                    result = int.Parse(itemx.SubItems[_column].Text).CompareTo(
                        int.Parse(itemy.SubItems[_column].Text));
                    break;
                case ComparerMode.DateTime:
                    //DateTimeに変換して比較
                    //.NET Framework 2.0からは、TryParseメソッドを使うこともできる
                    result = DateTime.Compare(
                        DateTime.Parse(itemx.SubItems[_column].Text),
                        DateTime.Parse(itemy.SubItems[_column].Text));
                    break;
            }
            //降順の時は結果を+-逆にする
            if (_order == SortOrder.Descending)
            {
                result = -result;
            }
            //結果を返す
            return result;
        }
	}
	#endregion
}
