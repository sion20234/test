﻿///////////////////////////////////////////////////////////////////////////////////////////
//
// (1) システム名称 : ELENIX ECNCⅢ
// (2) ファイル名   : EDITForm.cs
// (3) 概要         : 編集モード画面
// (4) 作成日       : 2015.06.01
// (5) 作成者       : 八野
// (6) 特記事項     : VisualStudio2015 C#
// (7) 更新履歴     : 2017.04.13柏原ひろむ
//
///////////////////////////////////////////////////////////////////////////////////////////
using ECNC3.Enumeration;
using ECNC3.Models;
using ECNC3.Models.McIf;
using ECNC3.Views.Popup;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Documents;
using System.Xml;
using System.Threading;

namespace ECNC3.Views
{
	/// <summary>
	///	編集
	/// </summary>
	public partial class EDITForm : ECNC3Form
    {
		#region <---初期化時--->
		/// <summary>ヘルプ表示画面</summary>
		HelpForm Help = null;
        /// <summary>ティーチング設定画面</summary>
        TeachTableForm TeachSet = null;
        /// <summary>絶対座標</summary>
        StructureAxisCoordinate _AbsolutePosition = new StructureAxisCoordinate();
        /// <summary>論理座標</summary>
        StructureAxisCoordinate _WorkPosition = new StructureAxisCoordinate();
        /// <summary>測定点</summary>
        StructureAxisCoordinate _RefPosition = new StructureAxisCoordinate();
        /// <summary>ティーチングテーブルデータ</summary>
        FileTeachTable teachTable = new FileTeachTable();

        FileOperatorMessage fileMessage = new FileOperatorMessage();
        /// <summary>
        /// 状態監視処理イベント
        /// </summary>
        internal event EventHandler StartSwOffEdgeEvent;
        /// <summary>
        /// 読み込み済みファイルパス
        /// </summary>
        string _readPath;
        /// <summary>
        //　フォーム　コンストラクタ
        /// </summary>
        public EDITForm()
		{
			InitializeComponent();
		}
		/// <summary>
		/// フォーム　ロード時のイベントハンドラ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EDITForm_Load(object sender, EventArgs e)
        {
            //初期化処理
            _Initialize();
        }
        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <returns>
        /// 成否
        /// </returns>
        private ResultCodes _Initialize()
        {
            ResultCodes retResult = ResultCodes.Success;
            if (retResult == ResultCodes.Success) retResult = fileMessage.Read();
            if (retResult == ResultCodes.Success) retResult = _ControlsInit();
            if (retResult == ResultCodes.Success) retResult = _TeachingInit();
            if (retResult != ResultCodes.Success) _MessageShow(MessageBoxIcon.Error, 5042);
            return retResult;
        }
        /// <summary>
        /// フォーム内コントロールの初期化処理
        /// </summary>
        /// <returns></returns>
        private ResultCodes _ControlsInit()
        {
            ResultCodes ret = ResultCodes.Success;
            //表示非表示設定
            #region <Process>
            try
            {
                this.OutLineEnable = false;                 //フォーム枠線無効
                _nextSerchBt.Enabled = false;               //検索「次へ」無効
                _returnSerchBt.Enabled = false;             //検索「前へ」無効
                _serchBt.StatusLedEnable = true;            //検索ボタンLED表示
                _serchBt.SetLed(false);                     //検索ボタンLEDOFF
                _FileNameTextBox.TabStop = false;           //ファイル名Tabストップ無効
                _FileNameTextBox.ReadOnly = true;           //ファイル名編集禁止
            }
            #endregion
            #region <Result>
            catch (Exception ex)
            {
                ECNC3Exception.ThreadExceptionFilter(ex, this, ExceptionHandling.MessageDialog);
                ret = ResultCodes.InvalidArgument;
            }
            finally
            {
                ret = (true == _serchBt.GetLed()) ? ResultCodes.InvalidArgument : ResultCodes.Success;
                Invalidate();
            }
            #endregion
            return ret;
        }
        /// <summary>
        /// ティーチング_変数名初期化
        /// </summary>
        /// <returns></returns>
        private ResultCodes _TeachingInit()
        {
            ResultCodes ret = ResultCodes.Success;
            #region <Process>
            _SelectVariablesComboBox.Items.Clear();                                         //ティーチング_変数名初期化
            using (FileTeachTable teachData = new FileTeachTable())
            {
                ret = teachData.Read();                                                     //外部ファイル読み込み
                foreach (StructureTeachTableItem tempItem in teachData.Items)
                {
                    if (tempItem.Name == string.Empty) continue;                            //ティーチ設定名がないものは無視
                    if (tempItem.Selected == false) continue;                               //未選択のティーチ設定は無視
                    _SelectVariablesComboBox.Items.Add(tempItem.Name);                      //ティーチ設定をコンボボックスに追加
                }
            }
            //ティーチング変数が全て無効の場合は設定以外のティーチング操作を使用不可にする。
            if (_SelectVariablesComboBox.Items.Count == 0)
            {
                _SelectPositionsComboBox.Enabled = false;
                _SelectVariablesComboBox.Enabled = false;
                _TeachBtn.Enabled = false;
            }
            else
            {
                _SelectPositionsComboBox.Enabled = true;
                _SelectVariablesComboBox.Enabled = true;
                _TeachBtn.Enabled = true;
                _SelectVariablesComboBox.SelectedIndex = 0;                                 //コンボボックスの選択Item初期Index
            }
            _SelectPositionsComboBox.SelectedIndex = 0;
            #endregion
            #region <Result>
            if (ret != ResultCodes.Success) _MessageShow(MessageBoxIcon.Error, 5043);       //エラー処理
            #endregion
            return ret;
        }
        #endregion
        #region<---破棄処理--->
        public void FunctionFormsClose()
        {
            //キーボード非表示
            if (ProgramEditBox.GetKeyboadIsVisible() == true) ProgramEditBox.KeybordVisible(false);
            //ヘルプ画面非表示
            if (Help != null)
            {
                Help.Close();
                Help = null;
            }
            //ティーチング画面非表示
            if (TeachSet != null)
            {
                TeachSet.Close();
                TeachSet = null;
            }
            //ファイルフォーム
            if (_fileForm != null)
            {
                _fileForm.Close();
                _fileForm = null;
            }
        }
        /// <summary>
        /// メンバーの破棄処理
        /// </summary>
        public void _Dispose()
        {
            //キーボード非表示
            if (ProgramEditBox.GetKeyboadIsVisible() == true) ProgramEditBox.KeybordVisible(false);
            //ヘルプ画面非表示
            if (Help != null)
            {
                Help.Close();
                Help = null;
            }
            //ティーチング画面非表示
            if(TeachSet != null)
            {
                TeachSet.Close();
                TeachSet = null;
            }
            //座標データ破棄
            if(_AbsolutePosition != null)
            {
                _AbsolutePosition.Dispose();
                _AbsolutePosition = null;
            }
            //座標データ破棄
            if (_WorkPosition != null)
            {
                _WorkPosition.Dispose();
                _WorkPosition = null;
            }
        }
        #endregion
        #region<---ボタン操作--->
        /// <summary>
        /// 新規ボタン：クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _newBt_Click(object sender, EventArgs e)
        {
            ProgramEditBox.Clear();
            //相対パス"..\YYMMDDEDITING.PGM"の絶対パスを取得する
            _readPath = FilePathInfo.ProgramData + DateTime.Today.ToString("yyyyMMdd") + "EDITING" + ".PGM";
            SaveProgram(_readPath);
            GetProgramFromEdit();
        }
        /// <summary>
        /// キーボードボタン：クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _keybordBt_Click(object sender, EventArgs e)
        {
            _KeybordVisible(_keybordBt.GetBack());
        }
        /// <summary>
        /// キーボード表示処理
        /// キーボード表示後、テキストボックスにフォーカスします。
        /// </summary>
        /// <param name="visible">
        /// キーボードの表示/非表示。
        /// ボタンが連動して背景が変更されます。
        /// </param>
        private void _KeybordVisible(bool visible)
        {
            ProgramEditBox.KeybordVisible(!visible);
            _keybordBt.SetBack(!visible);
            if(visible == false) ProgramEditBox.Focus();
        }
        /// <summary>
        /// ヘルプボタン：クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpBt_Click( object sender, EventArgs e )
		{
			Help = new HelpForm();
			Help.Show( this );
		}
		/// <summary>
		/// ティーチング設定ボタン：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TeachingSetBt_Click( object sender, EventArgs e )
		{
            if(TeachSet == null)
            {
                ProgramEditBox.KeybordVisible(false);
                _keybordBt.Enabled = false;
                TeachSet = new TeachTableForm(TeachingSetBt)
                {
                    NotifyReturn = TeachingSetFormOpenBt_ClickCallBack
                };
                TeachSet.Show(this);
            }
		}

        /// <summary>ティーチング設定ボタン コールバック</summary>
        private void TeachingSetFormOpenBt_ClickCallBack()
        {
            TeachSet.Close();
            TeachSet = null;
            _keybordBt.Enabled = true;
            _TeachingInit();
            if (TeachingSetBt.GetBack() == true) TeachingSetBt.SetBack(false);
        }
        private void _AllControlDisable(Control.ControlCollection ctrlList, Control exceptionCtrl, bool enable = false)
        {
            foreach (Control ctrl in ctrlList)
            {
                if(ctrl.Controls.Count != 0)
                {
                    _AllControlDisable(ctrl.Controls, exceptionCtrl, enable);
                }
                if (ctrl.Name == exceptionCtrl.Name) continue;
                ctrl.Enabled = enable;
            }
        }
        private FileForm _fileForm;
        /// <summary>
        /// ファイルボタン：クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _fileExpBt_Click( object sender, EventArgs e )
		{
            _fileForm = FileForm.ShowSubFormEd(this, FileFormMode.OpenProgram, System.IO.Path.GetFullPath( @"Program" ));
            _fileForm.NotifyReturn = FileForm_OnNotifyReturnOk;//OK時の通知セット
		}
        /// <summary>
        /// FileFormから通知
        /// </summary>
        private void FileForm_OnNotifyReturnOk()
        {
            if (_fileForm != null)
            {
                ResultCodes ret = ResultCodes.Success;
                _readPath = _fileForm._returnFullPath;
                ShowProgram(_readPath);
                ChkProgram(_readPath, ret );//プログラムチェック
                _fileForm.Close();//ここで閉じる
                _fileForm = null;
            }
        }
        /// <summary>
        /// 保存ボタン：クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _saveBt_Click( object sender, EventArgs e )
		{
            ProgramSaveDialog();
        }
        public void ProgramSaveDialog()
        {
            ReturnMessage retMessage = SelectCommandsDialog.ShowSubForm(this, "プログラム保存", new string[] { "上書き保存", "名前を付けて保存", "", "" });
            switch (retMessage)
            {
                case ReturnMessage.ExecuteA1:
                    SaveProgram(System.IO.Path.GetFullPath(@"Program\\" + _FileNameTextBox.Text));
                    break;

                case ReturnMessage.ExecuteA2:
                    //_fileForm = FileForm.ShowSubFormModeless(this, FileFormMode.RenameProgram, Path.GetFullPath(@"Program"));
                    string fileNameNonEx = System.IO.Path.GetFileNameWithoutExtension(_readPath);
                    _fileForm = FileForm.ShowSubFormModeless(
                        this,
                        FileFormMode.SaveProgram,
                        Path.GetFullPath(@"Program"),
                        FileForm.DispTypeForWProtect.Ather,
                        fileNameNonEx
                        );
                    _fileForm.NotifyReturn = FileForm_OnNotifyReturnOk2;//OK時の通知セット

                    //if (_readPath == "") return;
                    //SaveProgram(_readPath);
                    //ShowProgram(_readPath);
                    break;

                case ReturnMessage.Cancel:
                    break;
            }
        }
        /// <summary>
        /// FileFormから通知
        /// </summary>
        private void FileForm_OnNotifyReturnOk2()
        {
            if (_fileForm != null)
            {

                _readPath = _fileForm._returnPath;

                if (_readPath==null || _readPath == "") return;
                SaveProgram(_readPath);
                ShowProgram(_readPath);
                _fileForm.Close();//ここで閉じる
                _fileForm = null;
            }
        }

        /// <summary>
        /// 表示されているプログラムが実際のテキストファイルと相違があるか。
        /// </summary>
        /// <returns>
        /// 相違の有り無し
        /// </returns>
        public bool CompareProgram()
        {
            bool ret = false;
            try
            {
                string targetFilePath = System.IO.Path.GetFullPath(@"Program\\" + _FileNameTextBox.Text);
                string tempFilePath = System.IO.Path.GetFullPath(@"Program\\" + "CompareCheck.PGM");
                //ファイルの存在を確認
                if (false == File.Exists(targetFilePath)) return false;
                //ファイルの読み込み。文字列抽出
                SaveProgram(tempFilePath);
                //文字列情報の比較
                ret = _FileCompare(targetFilePath, tempFilePath);
                //一時ファイルの削除
                File.Delete(tempFilePath);
            }
            catch(Exception ex)
            {
                ECNC3Exception.FileIOFilter(ex, this, ExceptionHandling.MessageDialog);
            }
            finally { }
            return ret;
        }
        /// <summary>
        /// ファイル一致判定
        /// </summary>
        /// <param name="file1"></param>
        /// <param name="file2"></param>
        /// <returns></returns>
        private bool _FileCompare(string file1, string file2)
        {
            if (file1 == file2)
                return true;

            FileStream fs1 = new FileStream(file1, FileMode.Open);
            FileStream fs2 = new FileStream(file2, FileMode.Open);
            int byte1;
            int byte2;
            bool ret = false;

            try
            {
                if (fs1.Length == fs2.Length)
                {
                    do
                    {
                        byte1 = fs1.ReadByte();
                        byte2 = fs2.ReadByte();
                    }
                    while ((byte1 == byte2) && (byte1 != -1));

                    if (byte1 == byte2)
                        ret = true;
                }
            }
            catch(Exception ex) { ECNC3Exception.FileIOFilter(ex, this, ExceptionHandling.MessageDialog); }
            finally
            {
                fs1.Close();
                fs2.Close();
            }

            return ret;
        }
        #endregion
        #region<---このフォーム--->
        private void EDITForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                ProgramEditBox.Clear();
                ShowProgram(GetProgramFromEdit());
            }
        }
        /// <summary>
        /// 編集フォームから表示プログラムパスを取得するGetアクセサ
        /// </summary>
        /// <returns>
        /// 編集フォームに表示されているプログラムファイルのフルパス。
        /// </returns>
        public string GetProgramFromEdit()
        {
            string Path = FilePathInfo.ProgramData;
            if (ProgramEditBox.TextToArray()[0] == "")
            {
                string fileName = DateTime.Today.ToString("yyyyMMdd") + "EDITING" + ".PGM";
                Path += fileName;
                SaveProgram(Path);
                if (this.Visible == true)
                {
                    _FileNameTextBox.Text = fileName;
                }
            }
            else
            {
                _serchModeOff();
                Path += _FileNameTextBox.Text;
            }
            return Path;
        }
        #endregion
        #region<---ファイル名TextBox--->
		/// <summary>
		/// ファイル名TextBox：内容変更時
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _FileNameTextBox_TextChanged( object sender, EventArgs e )
		{
			string filename = ( (TextBoxEx)( sender ) ).Text;
			if( filename == "" || filename.EndsWith( ".PGM" ) ) return;
			_FileNameTextBox.Text = filename + ".PGM";
		}
		#endregion
		#region<---加工プログラム--->
        public string GetReadPath()
        {
            return _readPath;
        }
		/// <summary>
		/// 加工プログラム：チェック
		/// </summary>
		/// <param name="path"></param>
		/// <param name="ret"></param>
		private void ChkProgram( string path, ResultCodes ret )
		{
            _programStatusLabel.Text = "Loading....Wait.....";

            string errCode = "";
            int errIndex = -1;
            //無効G/Mコードチェック
            List<string> GMCodeList = UpdateGMCodeDisable();
            List<string> progList = ProgramEditBox.TextToArray().ToList();
            int progListIndex = 0;
            foreach(string rowText in progList )
            {
                foreach(string chkCode in GMCodeList)
                {
                    if(rowText.Contains(chkCode))
                    {
                        //無効GMコード検知
                        errCode = chkCode;
                        //行番号取得
                        errIndex = progListIndex;
                        break;
                    }
                }
                if (errCode != "") break;
                progListIndex++;
            }
           
            _programStatusLabel.Text = "ProgramEdit  info: " + fileMessage.Find(4109).Text + "[" + errCode + "]"
                                    + errIndex.ToString() + "行目";

            if (errCode == "")
            {
                //MC内プログラム削除
                using (McReqProgramDelete progDel = new McReqProgramDelete())
                {
                    progDel.Execute();
                }
                //テクノコンパイル
                using (McDatProgram mc = new McDatProgram())
                {
                    mc.ProgramFilePath = path;
                    mc.BlockNumber = 0;
                    ret = mc.Write();
                    //	実行結果の表示
                    _programStatusLabel.Text = "ProgramEdit  info: " + $"{ret}";
                    if (ResultCodes.Success != ret)
                    {
                        _programStatusLabel.Text = fileMessage.Find(4104).Text + "[" + errCode + "]"
                                        + errIndex.ToString() + "行目";
                    }
                    else
                    {
                        _programStatusLabel.Text = "ProgramEdit  info: " + "SUCCESS";
                    }
                }
            }            
            _programStatusLabel.Text = _programStatusLabel.Text.Replace("\r\n", "");
        }
        private List<string> UpdateGMCodeDisable()
        {
            List<string> retList = new List<string>();
            using (FileSettings fs = new FileSettings("GMCodeDisable.xml"))
            {
                byte[] gcdValue = new byte[100];
                byte[] mcdValue = new byte[100];

                //ファイル読込
                fs.Read();
                XmlNodeList gcdList = fs.GetList("Prog/GCodDsbl/Item");
                XmlNodeList mcdList = fs.GetList("Prog/MCodDsbl/Item");
                foreach (XmlNode gcditem in gcdList)
                {
                    gcdValue[int.Parse(gcditem.Attributes[0].Value)] = byte.Parse(gcditem.Attributes[1].Value);
                }
                foreach (XmlNode mcditem in mcdList)
                {
                    mcdValue[int.Parse(mcditem.Attributes[0].Value)] = byte.Parse(mcditem.Attributes[1].Value);
                }
                //リスト表示
                int totalCt = 0;
                for (int index = 0; index < gcdValue.Length; index++, totalCt++)
                {
                    if(gcdValue[index] == 1) retList.Add("G" + index.ToString("00"));
                }
                for (int index = 0; index < gcdValue.Length; index++, totalCt++)
                {
                    if (mcdValue[index] == 1) retList.Add("M" + index.ToString("00"));;
                }

                //解放処理
                for (int ct = 0; ct < 100; ct++)
                {
                    gcdValue[ct] = 0;
                    mcdValue[ct] = 0;
                }
                gcdValue = null;
                mcdValue = null;
            }
            return retList;
        }
        /// <summary>
        /// 加工プログラム：表示
        /// </summary>
        /// <param name="_lnkFilePath"></param>
        private void ShowProgram(string _lnkFilePath)
        {
            string path = _lnkFilePath;
            if (false == string.IsNullOrEmpty(path))
            {
                if (true == System.IO.File.Exists(path))
                {
                    //using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                    //{
                    FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                    using (TextReader sr = new StreamReader(fs))
                    {
                        ProgramEditBox.Text = "";
                        string Line = Line = sr.ReadLine();
                        int _readLineCount = 0;
                        while (Line != null)
                        {
                            if (Line != string.Empty)
                            {
                                ProgramEditBox.Text += Line + "\n";
                                _readLineCount++;
                            }
                            Line = sr.ReadLine();
                        }
                        _FileNameTextBox.Text = path.Remove(0, (path.LastIndexOf("/") < path.LastIndexOf("\\"))
                            ? path.LastIndexOf("\\") : path.LastIndexOf("/") + 1);
                    }
                }
            }
        }
		/// <summary>
		/// 加工プログラム：保存
		/// </summary>
		/// <param name="_lnkFilePath"></param>
		private void SaveProgram(string _lnkFilePath)
        {
            string path = _lnkFilePath;
            if (false == string.IsNullOrEmpty(path))
            {
                try
                {
                    if (true == System.IO.File.Exists(path))
                    {
                        using (FileStream fs = new FileStream
                            (
                            path, 
                            FileMode.Create, 
                            FileAccess.Write
                            ))
                        using (TextWriter sw = new StreamWriter(fs))
                        {
                            foreach(string Row in ProgramEditBox.TextToArray()) { sw.WriteLine(Row); }                               
                        }
                    }
                    else
                    {
                        using (FileStream fs = new FileStream(path, FileMode.CreateNew, FileAccess.Write))
                        {
                            using (TextWriter sw = new StreamWriter(fs))
                            {
                                foreach (string Row in ProgramEditBox.TextToArray())
                                {
                                    sw.WriteLine(Row);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex) { ECNC3Exception.FileIOFilter(ex, this, ExceptionHandling.MessageDialog); }
            }
        }
		#endregion
		#region<---DataGridViewEX--->
		/// <summary>
		///  DGVEX：フォーム表示時
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EDITForm_Shown(object sender, EventArgs e)
        {
        }
        #endregion
        #region Monitoring
        /// <summary>
        /// ステータスモニタリング
        /// </summary>
        /// <param name="e"></param>
        public void StatusMonitoring( StatusMonitorEventArgs e )
		{
            if (!(_AbsolutePosition.Equals(e.Items.MacAxisPos))) _AbsolutePosition = (StructureAxisCoordinate)e.Items.MacAxisPos.Clone();
            if (!(_WorkPosition.Equals(e.Items.WorkAxisPos))) _WorkPosition = (StructureAxisCoordinate)e.Items.WorkAxisPos.Clone();
            if ((this.IsDisposed == true || this.Disposing == true)) return;
            //検索ボタン表示
            retInvoke = BeginInvoke((MethodInvoker)delegate
            {
                if (_programStatusLabel.Text.Contains("SUCCESS"))
                {
                    if (_programStatusLabel.BackColor != FileUIStyleTable.SelectedLineColor) _programStatusLabel.BackColor = FileUIStyleTable.SelectedLineColor;
                }
                else
                {
                    if (_programStatusLabel.BackColor != FileUIStyleTable.DefaultBackColor) _programStatusLabel.BackColor = FileUIStyleTable.DefaultBackColor;
                }
            }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);

            if ((this.IsDisposed == true || this.Disposing == true)) return;
            //検索ボタン表示
            retInvoke = BeginInvoke((MethodInvoker)delegate
            {
                if (_serchBt.GetLed() == true)
                {
                    if (_nextSerchBt.Enabled == false) _nextSerchBt.Enabled = true;
                    if (_returnSerchBt.Enabled == false) _returnSerchBt.Enabled = true;
                }
                else
                {
                    if (_nextSerchBt.Enabled == true) _nextSerchBt.Enabled = false;
                    if (_returnSerchBt.Enabled == true) _returnSerchBt.Enabled = false;
                }
            }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
            //キーボードボタン表示
            retInvoke = BeginInvoke((MethodInvoker)delegate
            {
                if(ProgramEditBox != null)
                {
                    if (ProgramEditBox.GetKeyboadIsVisible() == false)
                    {
                        if (_keybordBt.GetBack() == true)
                        {
                            _keybordBt.SetBack(false);
                            ChkProgram(_readPath, ResultCodes.Success);
                        } 
                    }
                    else
                    {
                        if (_keybordBt.GetBack() == false)
                        {
                            _keybordBt.SetBack(true);
                        }
                    }
                }
            }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
		}
		/// <summary>
		/// スタートボタン押下時のイベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		internal void StartBtnOffEdge( object sender, EventArgs e )
		{
			if( StartSwOffEdgeEvent != null ) {
				StartSwOffEdgeEvent( this, EventArgs.Empty );                                                     //"StartSwOffEdgeEvent"イベントの発生
			} else {

			}
		}
        #endregion
        #region Serching
        private List<int> serchPositionList = null;
        private int serchPosition = 0;
        /// <summary>
        /// 「検索」ボタンマウスアップ時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _serchBt_MouseUp(object sender, MouseEventArgs e)
        {
            if(_serchBt.GetLed() == true)
            {
                _serchBt.SetLed(false);
                if (serchPositionList != null)
                {
                    serchPositionList.Clear();
                    serchPositionList.TrimExcess();
                    serchPositionList = null;
                    ProgramEditBox.SelectAll();
                    ProgramEditBox.SelectionColor = FileUIStyleTable.DefaultForeColor;
                }
                return;
            }
            ProgramEditBox.tempText = ProgramEditBox.TextToArray();
            if (ProgramEditBox.Text == null) return;
            serchPosition = 0;
            if(serchPositionList != null)
            {
                serchPositionList.Clear();
                serchPositionList.TrimExcess();
                serchPositionList = null;
            }
            serchPositionList = new List<int>();
            ProgramEditBox.SelectAll();
            ProgramEditBox.SelectionColor = FileUIStyleTable.DefaultForeColor;
            int firstWord = -1;
            string serchWord = EditTextDialog.ShowSubForm(this,"検索する文字列を入力してください。", "", false, sender as ButtonEx );
            if (serchWord == "") return;
            //ProgramEditBox.RowBackColorKey.Clear();
            //ProgramEditBox.RowBackColorKey.Add(serchWord);
            // 対象の RichText// found 変数では検索を行う文字位置の一つ手前を示します。最初は - 1 となります。
            int found = -1;
            // キーワードが見つからなくなるまで繰り返します。
            do
            {
                // 対象の RichTextBox から、キーワードが見つかる位置を探します。検索開始位置は、前回見つかった場所の次からとします。
                int temp = ProgramEditBox.Find(serchWord, found + 1, RichTextBoxFinds.MatchCase);
                if(temp <= found) break;
                found = temp;
                // キーワードが見つかった場合は、その色を変更します。
                if (found >= 0)
                {
                    if (firstWord == -1) firstWord = found;
                    serchPositionList.Add(found);
                    ProgramEditBox.SelectionStart = found;
                    ProgramEditBox.SelectionLength = serchWord.Length;
                    ProgramEditBox.SelectionColor = Color.DarkRed;
                }
                else
                {
                    // キーワードが見つからなかった場合は、繰り返し処理を終了します。
                    break;
                }
            }
            while (true);
            //選択したところまで移動
            if(firstWord < 0)
            {
                _MessageShow( MessageBoxIcon.Information, 5028, sender as ButtonEx);
                using (MessageDialog msg = new MessageDialog())
                {
                    msg.Information(5028, this);
                }
                return;        
            }
            ProgramEditBox.Select(firstWord, 0);
            ProgramEditBox.ScrollToCaret();
            _serchBt.SetLed(true);
        }
        /// <summary>
        /// 「次へ」ボタンマウスアップ時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _nextSerchBt_MouseUp(object sender, MouseEventArgs e)
        {
            if (serchPositionList.Count <= serchPosition + 1)
            {
                serchPosition = 0;
            }
            else
            {
                serchPosition++;
            }
            //選択したところまで移動
            ProgramEditBox.Select(serchPositionList[serchPosition], 0);
            ProgramEditBox.ScrollToCaret();
        }
        /// <summary>
        /// 「前へ」ボタンマウスアップ時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _returnSerchBt_MouseUp(object sender, MouseEventArgs e)
        {
            if (0 > serchPosition - 1)
            {
                serchPosition = serchPositionList.Count - 1;
            }
            else
            {
                serchPosition--;
            }
            //選択したところまで移動
            ProgramEditBox.Select(serchPositionList[serchPosition], 0);
            ProgramEditBox.ScrollToCaret();
        }
        /// <summary>
        /// 「検索モード解除」ボタンマウスアップ時処理
        /// </summary>
		private void _serchModeOff()
		{
			_serchBt.SetLed( false );
			if( serchPositionList != null ) {
				serchPositionList.Clear();
				serchPositionList.TrimExcess();
				serchPositionList = null;
				ProgramEditBox.SelectAll();
				ProgramEditBox.SelectionColor = FileUIStyleTable.DefaultForeColor;
			}
		}
        #endregion
        #region Teaching
        /// <summary>
        /// 座標種別
        /// </summary>
        private enum PositionType
        {
            /// <summary>
            /// 機械座標
            /// </summary>
            Machine = 0,
            /// <summary>
            /// ワーク座標
            /// </summary>
            Work = 1,
            /// <summary>
            /// 測定点
            /// </summary>
            Reference = 2
        }
        private void _TeachBtn_Click(object sender, EventArgs e)
        {
            if (_TeachingToProgram() == false) _MessageShow(MessageBoxIcon.Error, 5032, _TeachBtn);

        }
        /// <summary>
        /// ティーチ実行
        /// </summary>
        /// <returns>
        /// true: 成功
        /// false: 失敗
        /// 
        /// </returns>
        private bool _TeachingToProgram()
        {
            //ティーチングデータ読込
            teachTable.Read();
            //座標種別確認
            PositionType posType = (PositionType)_SelectPositionsComboBox.SelectedIndex;
            //変数確認
            StructureTeachTableItem variableItem = null;
            foreach(StructureTeachTableItem tempItem in teachTable.Items)
            {
                if(_SelectVariablesComboBox.SelectedItem.ToString() == tempItem.Name)
                {
                    variableItem = (StructureTeachTableItem)tempItem.Clone();
                }
            }
            string tempValue = variableItem.Value.ToString();
            //値がNULLなら処理を抜ける。
            if (tempValue == "") return false;
            //値が無効なら処理を抜ける。
            if (!tempValue.Contains("G") && !tempValue.Contains("M")) return false;
            //軸名確認
            //座標ごとに別処理
            switch (posType)
            {
                case PositionType.Machine:
                    if (tempValue.Contains("X")) tempValue = tempValue.Insert(tempValue.IndexOf("X") + 1, ((decimal)(_AbsolutePosition.AxisX) / 1000).ToString().Contains(".")? ((decimal)(_AbsolutePosition.AxisX) / 1000).ToString() : ((decimal)(_AbsolutePosition.AxisX) / 1000).ToString() + ".");
                    if (tempValue.Contains("Y")) tempValue = tempValue.Insert(tempValue.IndexOf("Y") + 1, ((decimal)(_AbsolutePosition.AxisY) / 1000).ToString().Contains(".")? ((decimal)(_AbsolutePosition.AxisY) / 1000).ToString() : ((decimal)(_AbsolutePosition.AxisY) / 1000).ToString() + ".");
                    if (tempValue.Contains("W")) tempValue = tempValue.Insert(tempValue.IndexOf("W") + 1, ((decimal)(_AbsolutePosition.AxisW) / 1000).ToString().Contains(".")? ((decimal)(_AbsolutePosition.AxisW) / 1000).ToString() : ((decimal)(_AbsolutePosition.AxisW) / 1000).ToString() + ".");
                    if (tempValue.Contains("Z")) tempValue = tempValue.Insert(tempValue.IndexOf("Z") + 1, ((decimal)(_AbsolutePosition.AxisZ) / 1000).ToString().Contains(".")? ((decimal)(_AbsolutePosition.AxisZ) / 1000).ToString() : ((decimal)(_AbsolutePosition.AxisZ) / 1000).ToString() + ".");
                    if (tempValue.Contains("A")) tempValue = tempValue.Insert(tempValue.IndexOf("A") + 1, ((decimal)(_AbsolutePosition.AxisA) / 1000).ToString().Contains(".")? ((decimal)(_AbsolutePosition.AxisA) / 1000).ToString() : ((decimal)(_AbsolutePosition.AxisA) / 1000).ToString() + ".");
                    if (tempValue.Contains("B")) tempValue = tempValue.Insert(tempValue.IndexOf("B") + 1, ((decimal)(_AbsolutePosition.AxisB) / 1000).ToString().Contains(".")? ((decimal)(_AbsolutePosition.AxisB) / 1000).ToString() : ((decimal)(_AbsolutePosition.AxisB) / 1000).ToString() + ".");
                    if (tempValue.Contains("C")) tempValue = tempValue.Insert(tempValue.IndexOf("C") + 1, ((decimal)(_AbsolutePosition.AxisC) / 1000).ToString().Contains(".")? ((decimal)(_AbsolutePosition.AxisC) / 1000).ToString() : ((decimal)(_AbsolutePosition.AxisC) / 1000).ToString() + ".");
                    if (tempValue.Contains("I")) tempValue = tempValue.Insert(tempValue.IndexOf("I") + 1, ((decimal)(_AbsolutePosition.Axis8) / 1000).ToString().Contains(".")? ((decimal)(_AbsolutePosition.Axis8) / 1000).ToString() : ((decimal)(_AbsolutePosition.Axis8) / 1000).ToString() + ".");
                    break;

                case PositionType.Work:
                    if (tempValue.Contains("X")) tempValue = tempValue.Insert(tempValue.IndexOf("X") + 1, ((decimal)(_WorkPosition.AxisX) / 1000).ToString().Contains(".")? ((decimal)(_WorkPosition.AxisX) / 1000).ToString() : ((decimal)(_WorkPosition.AxisX) / 1000).ToString() + ".");
                    if (tempValue.Contains("Y")) tempValue = tempValue.Insert(tempValue.IndexOf("Y") + 1, ((decimal)(_WorkPosition.AxisY) / 1000).ToString().Contains(".")? ((decimal)(_WorkPosition.AxisY) / 1000).ToString() : ((decimal)(_WorkPosition.AxisY) / 1000).ToString() + ".");
                    if (tempValue.Contains("W")) tempValue = tempValue.Insert(tempValue.IndexOf("W") + 1, ((decimal)(_WorkPosition.AxisW) / 1000).ToString().Contains(".")? ((decimal)(_WorkPosition.AxisW) / 1000).ToString() : ((decimal)(_WorkPosition.AxisW) / 1000).ToString() + ".");
                    if (tempValue.Contains("Z")) tempValue = tempValue.Insert(tempValue.IndexOf("Z") + 1, ((decimal)(_WorkPosition.AxisZ) / 1000).ToString().Contains(".")? ((decimal)(_WorkPosition.AxisZ) / 1000).ToString() : ((decimal)(_WorkPosition.AxisZ) / 1000).ToString() + ".");
                    if (tempValue.Contains("A")) tempValue = tempValue.Insert(tempValue.IndexOf("A") + 1, ((decimal)(_WorkPosition.AxisA) / 1000).ToString().Contains(".")? ((decimal)(_WorkPosition.AxisA) / 1000).ToString() : ((decimal)(_WorkPosition.AxisA) / 1000).ToString() + ".");
                    if (tempValue.Contains("B")) tempValue = tempValue.Insert(tempValue.IndexOf("B") + 1, ((decimal)(_WorkPosition.AxisB) / 1000).ToString().Contains(".")? ((decimal)(_WorkPosition.AxisB) / 1000).ToString() : ((decimal)(_WorkPosition.AxisB) / 1000).ToString() + ".");
                    if (tempValue.Contains("C")) tempValue = tempValue.Insert(tempValue.IndexOf("C") + 1, ((decimal)(_WorkPosition.AxisC) / 1000).ToString().Contains(".")? ((decimal)(_WorkPosition.AxisC) / 1000).ToString() : ((decimal)(_WorkPosition.AxisC) / 1000).ToString() + ".");
                    if (tempValue.Contains("I")) tempValue = tempValue.Insert(tempValue.IndexOf("I") + 1, ((decimal)(_WorkPosition.Axis8) / 1000).ToString().Contains(".")? ((decimal)(_WorkPosition.Axis8) / 1000).ToString() : ((decimal)(_WorkPosition.Axis8) / 1000).ToString() + ".");
                    break;

                case PositionType.Reference:
                    using (McDatVirtualPosition datVirPos = new McDatVirtualPosition())
                    using (FileSettings fs = new FileSettings())
                    {
                        datVirPos.Read();
                        fs.Read();
                        int pcondCtMax = 1000;
                        if (fs.AttrText("Root/Apl", "boot") == "DESKTOP") pcondCtMax = 100;
                        if (tempValue.Contains("X")) tempValue = tempValue.Insert(tempValue.IndexOf("X") + 1, ((decimal)(datVirPos.Items[pcondCtMax].AxisX) / 1000).ToString().Contains(".")? ((decimal)(datVirPos.Items[pcondCtMax].AxisX) / 1000).ToString() : ((decimal)(datVirPos.Items[pcondCtMax].AxisX) / 1000).ToString() + ".");
                        if (tempValue.Contains("Y")) tempValue = tempValue.Insert(tempValue.IndexOf("Y") + 1, ((decimal)(datVirPos.Items[pcondCtMax].AxisY) / 1000).ToString().Contains(".")? ((decimal)(datVirPos.Items[pcondCtMax].AxisY) / 1000).ToString() : ((decimal)(datVirPos.Items[pcondCtMax].AxisY) / 1000).ToString() + ".");
                        if (tempValue.Contains("W")) tempValue = tempValue.Insert(tempValue.IndexOf("W") + 1, ((decimal)(datVirPos.Items[pcondCtMax].AxisW) / 1000).ToString().Contains(".")? ((decimal)(datVirPos.Items[pcondCtMax].AxisW) / 1000).ToString() : ((decimal)(datVirPos.Items[pcondCtMax].AxisW) / 1000).ToString() + ".");
                        if (tempValue.Contains("Z")) tempValue = tempValue.Insert(tempValue.IndexOf("Z") + 1, ((decimal)(datVirPos.Items[pcondCtMax].AxisZ) / 1000).ToString().Contains(".")? ((decimal)(datVirPos.Items[pcondCtMax].AxisZ) / 1000).ToString() : ((decimal)(datVirPos.Items[pcondCtMax].AxisZ) / 1000).ToString() + ".");
                        if (tempValue.Contains("A")) tempValue = tempValue.Insert(tempValue.IndexOf("A") + 1, ((decimal)(datVirPos.Items[pcondCtMax].AxisA) / 1000).ToString().Contains(".")? ((decimal)(datVirPos.Items[pcondCtMax].AxisA) / 1000).ToString() : ((decimal)(datVirPos.Items[pcondCtMax].AxisA) / 1000).ToString() + ".");
                        if (tempValue.Contains("B")) tempValue = tempValue.Insert(tempValue.IndexOf("B") + 1, ((decimal)(datVirPos.Items[pcondCtMax].AxisB) / 1000).ToString().Contains(".")? ((decimal)(datVirPos.Items[pcondCtMax].AxisB) / 1000).ToString() : ((decimal)(datVirPos.Items[pcondCtMax].AxisB) / 1000).ToString() + ".");
                        if (tempValue.Contains("C")) tempValue = tempValue.Insert(tempValue.IndexOf("C") + 1, ((decimal)(datVirPos.Items[pcondCtMax].AxisC) / 1000).ToString().Contains(".")? ((decimal)(datVirPos.Items[pcondCtMax].AxisC) / 1000).ToString() : ((decimal)(datVirPos.Items[pcondCtMax].AxisC) / 1000).ToString() + ".");
                        if (tempValue.Contains("I")) tempValue = tempValue.Insert(tempValue.IndexOf("I") + 1, ((decimal)(datVirPos.Items[pcondCtMax].Axis8) / 1000).ToString().Contains(".")? ((decimal)(datVirPos.Items[pcondCtMax].Axis8) / 1000).ToString() : ((decimal)(datVirPos.Items[pcondCtMax].Axis8) / 1000).ToString() + ".");
                    }
                    break;

            }
            //Gコードをプログラムカーソル位置に挿入
            ProgramEditBox.SelectedText = tempValue;
            return true;
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
                case 3: ret = int.Parse(AxisVal.Replace(".", "")); break;
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
        #endregion
       
    }
}
