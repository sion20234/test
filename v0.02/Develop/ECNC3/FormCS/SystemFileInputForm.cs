///////////////////////////////////////////////////////////////////////////////////////////
//
// (1) システム名称 : ELENIX ECNCⅢ
// (2) ファイル名   : SystemFileInputForm.cs
// (3) 概要         : システムファイル読込み画面
// (4) 作成日       : 2016.07.19
// (5) 作成者       : 八野
// (6) 特記事項     : VisualStudio2015 C#
// (7) 更新履歴     :
//
///////////////////////////////////////////////////////////////////////////////////////////

using ECNC3.Enumeration;
using ECNC3.Models;
using ECNC3.Models.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ECNC3.Views
{
    public enum FileVector
    {
        Input,
        Output
    }
    public enum SystemFiles
    {
        INITIALROM,
        INITIALIPM,
        INITIALPRM,
        INITIALPIT,
        rt64ecdrv,
        CncIO,
        CncMsg,
        CncSys,
        OrgPos,
        ParamTable,
        Pdata,
        PdataParam,
        GMCodeHelp
    }

    /// <summary>
    /// システムファイル読込み画面
    /// </summary>
    /// <remarks>対象のファイル名をチェックし、読み込む。</remarks>
    public partial class SystemFileInputForm : ECNC3Form
    {
        #region Constructor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SystemFileInputForm(FileVector InOrOut, int targetCt)
        {
            InitializeComponent();
            _fileVector = InOrOut;
            _targetCount = targetCt;
            _cellSelectList = new bool[_targetCount];
        }
        #endregion

        #region VariableMember
        private int _targetCount = 0;
        private string[] _targetFileName =
        {
            "initial.rom",
            "initial.ipm",
            "initial.prm",
            "initial.pit",
            "rt64ecdrv.ini",
            "CncSys.xml",
            "Pdata.xml",
            "PdataParam.xml",
            "OrgPos.xml",
            "CncMsg.xml",
            "cncio.xml",
            "ParamTable.xml",
            "GMCodeHelp.pdf"
        };
        private string _inputPath = "";
        private string _exportPath = "";
        private FileVector _fileVector { get; set; }
        private bool[] _cellSelectList;
        #endregion

        #region EventHandler
        private void SystemFileInputForm_Load(object sender, EventArgs e)
        {
            _sysFileStatusList.SelectedIndex = _sysFileStatusList.Items.Add("リストを初期化しています・・・");
            //座標表示初期化
            dataGridViewEx1.Initialize(12.0F, 40, true);

            DataGridViewColumn _resultCol = new DataGridViewColumn();
            _resultCol.CellTemplate = new DataGridViewImageCell();
            _resultCol.Name = "ResultCol";
            _resultCol.HeaderText = "結果";
            _resultCol.Width = 100;
            DataGridViewColumn _fileNameCol = new DataGridViewColumn();
            _fileNameCol.CellTemplate = new DataGridViewTextBoxCell();
            _fileNameCol.Name = "FileNameCol";
            _fileNameCol.HeaderText = "ファイル名";
            _fileNameCol.Width = 200;
            DataGridViewColumn _summaryCol = new DataGridViewColumn();
            _summaryCol.CellTemplate = new DataGridViewTextBoxCell();
            _summaryCol.Name = "SummaryCol";
            _summaryCol.HeaderText = "説明";
            _summaryCol.Width = 350;

            dataGridViewEx1.Columns.Add(_resultCol);
            dataGridViewEx1.Columns.Add(_fileNameCol);
            dataGridViewEx1.Columns.Add(_summaryCol);
            dataGridViewEx1.RowCount = _targetCount;
            dataGridViewEx1.MultiSelect = true;
            dataGridViewEx1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewEx1.DefaultCellStyle.SelectionBackColor = FileUIStyleTable.EnabledBackColor;
            dataGridViewEx1.DefaultCellStyle.SelectionForeColor = FileUIStyleTable.EnabledForeColor;
            dataGridViewEx1.AutoSize = true;
            dataGridViewEx1.ColumnHeadersVisible = true;
            dataGridViewEx1.Columns["ResultCol"].Width = 60;
            (dataGridViewEx1.Columns["ResultCol"].CellTemplate as DataGridViewImageCell).ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridViewEx1.Columns["ResultCol"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewEx1.Columns["ResultCol"].DefaultCellStyle.BackColor = Color.FromArgb(35,35,0);
            dataGridViewEx1.Columns["ResultCol"].DefaultCellStyle.ForeColor = FileUIStyleTable.DefaultForeColor;
            dataGridViewEx1.InitCol("FileNameCol", 15.0F, DataGridViewContentAlignment.MiddleLeft, typeof(string));
            dataGridViewEx1.InitCol("SummaryCol", 15.0F, DataGridViewContentAlignment.MiddleLeft, typeof(string));

            dataGridViewEx1.Rows[0].Cells[0].Value = imageList1.Images[3];
            dataGridViewEx1.Rows[1].Cells[0].Value = imageList1.Images[3];
            dataGridViewEx1.Rows[2].Cells[0].Value = imageList1.Images[3];
            dataGridViewEx1.Rows[3].Cells[0].Value = imageList1.Images[3];
            dataGridViewEx1.Rows[4].Cells[0].Value = imageList1.Images[3];
            dataGridViewEx1.Rows[5].Cells[0].Value = imageList1.Images[3];
            dataGridViewEx1.Rows[6].Cells[0].Value = imageList1.Images[3];
            dataGridViewEx1.Rows[7].Cells[0].Value = imageList1.Images[3];
            dataGridViewEx1.Rows[8].Cells[0].Value = imageList1.Images[3];
            dataGridViewEx1.Rows[9].Cells[0].Value = imageList1.Images[3];
            dataGridViewEx1.Rows[10].Cells[0].Value = imageList1.Images[3];
            dataGridViewEx1.Rows[11].Cells[0].Value = imageList1.Images[3];
            dataGridViewEx1.Rows[12].Cells[0].Value = imageList1.Images[3];

            for(int tgtCt = 0; tgtCt < _targetCount; tgtCt++)
            {
                dataGridViewEx1.Rows[tgtCt].Cells[1].Value = _targetFileName[tgtCt];
            }

            dataGridViewEx1.Rows[0].Cells[2].Value = "ROMSWデータファイル";
            dataGridViewEx1.Rows[1].Cells[2].Value = "初期化パラメータファイル";
            dataGridViewEx1.Rows[2].Cells[2].Value = "サーボパラメータファイル";
            dataGridViewEx1.Rows[3].Cells[2].Value = "ピッチエラー補正データファイル";
            dataGridViewEx1.Rows[4].Cells[2].Value = "セッティングアプリ設定ファイル";
            dataGridViewEx1.Rows[5].Cells[2].Value = "システム設定ファイル";
            dataGridViewEx1.Rows[6].Cells[2].Value = "加工条件ファイル";
            dataGridViewEx1.Rows[7].Cells[2].Value = "加工条件関連登録ファイル";
            dataGridViewEx1.Rows[8].Cells[2].Value = "仮想点ファイル";
            dataGridViewEx1.Rows[9].Cells[2].Value = "メッセージファイル";
            dataGridViewEx1.Rows[10].Cells[2].Value = "チェック情報ファイル";
            dataGridViewEx1.Rows[11].Cells[2].Value = "パラメータ情報ファイル";
            dataGridViewEx1.Rows[12].Cells[2].Value = "ヘルプファイル";

            for(int index = 0; index < _targetCount; index++)
            {
                _cellSelectList[index] = false;
            }

            switch(_fileVector)
            {
                case FileVector.Input: label8.Text = "SYSTEM FILE INPORT"; break;
                case FileVector.Output: label8.Text = "SYSTEM FILE EXPORT"; break;
            }
            _sysFileStatusList.SelectedIndex = _sysFileStatusList.Items.Add("リストの初期化が完了しました。");


            //複数選択：不可
            dataGridViewEx1.MultiSelect = false;//柏原１
        }

        private void SystemFileInputForm_Shown(object sender, EventArgs e)
        {
            dataGridViewEx1.ClearSelection();
        }
		/// <summary>
		/// セル：マウスダウン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void dataGridViewEx1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {//複数選択：禁止
		 dataGridViewEx1.MultiSelect = false;//柏原２
		}
		//bool cellMove = false;
		/// <summary>
		/// セル：マウスムーブ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dataGridViewEx1_MouseMove( object sender, MouseEventArgs e )
		{
			dataGridViewEx1.MultiSelect = false;
		}
		/// <summary>
		/// セル：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dataGridViewEx1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewEx1.MultiSelect = true;//柏原３

            if (true == _cellSelectList[e.RowIndex])
            {
                _cellSelectList[e.RowIndex] = false;
                _resultImageChange(3, 2, e.RowIndex);
            }
            else
            {
                _cellSelectList[e.RowIndex] = true;
                _resultImageChange(2, 2, e.RowIndex);//imageList[2], 任意選択行画像変更。
                _sysFileStatusList.SelectedIndex = _sysFileStatusList.Items.Add("[" + dataGridViewEx1[1, e.RowIndex].Value + "]" + "...Ready...");
            }
            for (int index = 0; index < _targetCount; index++)
            {
                if (dataGridViewEx1.Rows[index].Selected != _cellSelectList[index])
                {
                    dataGridViewEx1.Rows[index].Selected = _cellSelectList[index];
                }
            }
        }
        
        private void dataGridViewEx1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            _sysFileStatusList.Refresh();
        }

        private void _startBt_Click(object sender, EventArgs e)
        {
            _sysFileStatusList.SelectedIndex = _sysFileStatusList.Items.Add(_fileVector.ToString() + "を開始します。");
            if (GetSetDirectoryPath(false, "") == false)
            {
                _resultImageChange(1, 1);
                _sysFileStatusList.SelectedIndex = _sysFileStatusList.Items.Add("対象ディレクトリのパスが設定されいません。");
                return;
            }

            switch(_fileVector)
            {
                case FileVector.Input:
                    for (int tgtCt = 0; tgtCt < _targetCount; tgtCt++)
                    {
                        if (_cellSelectList[tgtCt])
                        {
                            if (!InputFile(_targetFileName[tgtCt]))
                            {
                                _resultImageChange(0, 2, tgtCt);
                            }
                            else
                            {
                                _resultImageChange(3, 2, tgtCt);
                            }                                                        
                        }
                        _sysFileStatusList.Refresh();
                    }
                    break;

                case FileVector.Output:
                    for (int tgtCt = 0; tgtCt < _targetCount; tgtCt++)
                    {
                        if (_cellSelectList[tgtCt])
                        {
                            if (!OutputFile(_targetFileName[tgtCt]))
                            {
                                _resultImageChange(0, 2, tgtCt);
                            }
                            else
                            {
                                _resultImageChange(3, 2, tgtCt);
                            }
                        }
                        _sysFileStatusList.Refresh();
                    }
                    break;

            }
                
        }
        #endregion
        /// <summary>
        /// フォームを閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">BackUserServiceFormBtのボタンクリック時のイベント</param>
        private void BackUserServiceFormBt_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool InputFile(string target)
        {
            string path = "";
            if(target.Contains(".xml"))
            {
                path = FilePathInfo.MasterData + target;
            }
            else
            {
                path = FilePathInfo.ECNC3PATH + target;
            }
            _sysFileStatusList.SelectedIndex = _sysFileStatusList.Items.Add(path + "を探しています・・・");
            if (File.Exists(path))
            {
                _sysFileStatusList.SelectedIndex = _sysFileStatusList.Items.Add(path + "が見つかりました。");
                if(File.Exists(_inputPath + "\\" + target))
                {
                    _sysFileStatusList.SelectedIndex = _sysFileStatusList.Items.Add(_inputPath + "\\" + target + "が見つかりました。");
                    File.Copy(_inputPath + "\\" + target, path, true);
                }
                else
                {
                    _sysFileStatusList.SelectedIndex = _sysFileStatusList.Items.Add(_inputPath + "\\" + target + "が存在しません。");
                    return false;
                }
                
            }
            else
            {
                _sysFileStatusList.SelectedIndex = _sysFileStatusList.Items.Add(path + "が存在しません。");
                return false;
            }
            if(target == "OrgPos.xml")
            {
                _sysFileStatusList.SelectedIndex = _sysFileStatusList.Items.Add(target + " 仮想点データをInportしています・・・");
                using (Models.McIf.McReqVirtualPositionChange virpos = new Models.McIf.McReqVirtualPositionChange())
                {
                    virpos.Initialize();
                }
            }
            _sysFileStatusList.SelectedIndex = _sysFileStatusList.Items.Add(target + " Finished Inport.");
            return true;
        }

        private bool OutputFile(string target)
        {
            string path = "";
            if (target.Contains(".xml"))
            {
                path = FilePathInfo.MasterData + target;
            }
            else
            {
                path = FilePathInfo.ECNC3PATH + target;
            }
            _sysFileStatusList.SelectedIndex = _sysFileStatusList.Items.Add(path + "を探しています・・・");
            if (File.Exists(path))
            {
                _sysFileStatusList.SelectedIndex = _sysFileStatusList.Items.Add(path + "が見つかりました。");
                _sysFileStatusList.SelectedIndex = _sysFileStatusList.Items.Add(_exportPath + "を探しています・・・");
                if(Directory.Exists(_exportPath))
                {
                    _sysFileStatusList.SelectedIndex = _sysFileStatusList.Items.Add(_exportPath + "が見つかりました。");
                    File.Copy(path, _exportPath + "\\" + target, true);
                }
                else
                {
                    _sysFileStatusList.SelectedIndex = _sysFileStatusList.Items.Add(_exportPath + "が存在しません。");
                    return false;
                }
                
            }
            else
            {
                _sysFileStatusList.SelectedIndex = _sysFileStatusList.Items.Add(path + "が存在しません。");
                return false;
            }
            _sysFileStatusList.SelectedIndex = _sysFileStatusList.Items.Add(target + " Finished Export.");
            return true;
        }

        /// <summary>
        /// ファイル操作対象パス：「CncSys.xml」取得/設定
        /// </summary>
        /// <param name="path">string パス</param>
        /// <returns>成功=true/失敗=false</returns>
        private bool GetSetDirectoryPath(bool writeOrRead, string path)
        {
            bool retResult = true;
            using (FileSettings fs = new FileSettings())
            {
                if (writeOrRead)
                {   //書き込み
                    fs.Read();
                    retResult = fs.WriteAttr("Root/SystemFile/Directory", "path", path);//string パス
                    fs.Write();
                }
                else
                {   //読み込み
                    //保持パスの初期化
                    _inputPath = "";
                    _exportPath = "";
                    //ファイル読込
                    fs.Read();
                    if(_fileVector == FileVector.Input)
                    {
                        //パスの格納
                        _inputPath = fs.AttrText("Root/SystemFile/Directory", "path");//string パス
                        if (_inputPath == "")
                        {
                            retResult = false;
                        }
                    }
                    else if(_fileVector == FileVector.Output)
                    {
                        //パスの格納
                        _exportPath = fs.AttrText("Root/SystemFile/Directory", "path");//string パス
                        if (_exportPath == "")
                        {
                            retResult = false;
                        }
                    }
                }
            }
            return retResult;//読み書き結果
        }

        private void _resultImageChange(int imageIndex, int mode, int listIndex = 0)
        {
            switch(mode)
            {
                //全選択行変更
                case 1:
                    foreach (DataGridViewRow row in dataGridViewEx1.Rows)
                    {
                        if (row.Selected)
                        {
                            row.Cells[0].Value = imageList1.Images[imageIndex];
                        }
                    }
                    break;
                //任意選択行変更    
                case 2:
                    dataGridViewEx1[0, listIndex].Value = imageList1.Images[imageIndex];
                    break;

            }
        }
        private int sysFileStatusListIndexBefore = 0;
        /// <summary>
        /// プログラム表示処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _sysFileStatusList_DrawItem(object sender, DrawItemEventArgs e)
        {
            sysFileStatusListIndexBefore = e.Index;
            //項目が選択されている時は強調表示される
            e.DrawBackground();

            //ListBoxが空のときにListBoxが選択されるとe.Indexが-1になる
            if (e.Index > -1)
            {
                //文字を描画する色の選択
                Brush b = null;
                //選択されていない時
                if ((e.State & DrawItemState.Selected) != DrawItemState.Selected)
                {
                      //プログラム実行行
                    if (e.Index == sysFileStatusListIndexBefore)
                    {
                        //文字色を変更する。
                        b = new SolidBrush(FileUIStyleTable.EnabledForeColor);
                        //背景色を変更する。
                        e.Graphics.FillRectangle(new SolidBrush(FileUIStyleTable.EnabledBackColor), e.Bounds);
                    }
                    //選択されている時はそのままの前景色を使う
                    b = new SolidBrush(e.ForeColor);
                    e.Graphics.FillRectangle(new SolidBrush(FileUIStyleTable.DefaultBackColor), e.Bounds);
                    
                }
                else
                {
                    //プログラム実行行
                    if (e.Index == sysFileStatusListIndexBefore)
                    {
                          //文字色を変更する。
                        b = new SolidBrush(FileUIStyleTable.EnabledForeColor);
                        //背景色を変更する。
                        e.Graphics.FillRectangle(new SolidBrush(FileUIStyleTable.EnabledBackColor), e.Bounds);
                    }
                    else
                    {
                        b = new SolidBrush(FileUIStyleTable.DefaultForeColor);
                    }
                }
                //描画する文字列の取得
                string txt = ((ListBox)sender).Items[e.Index].ToString();
                //文字列の描画
                e.Graphics.DrawString(txt, e.Font, b, e.Bounds);
                //後始末
                b.Dispose();
            }

            //フォーカスを示す四角形を描画
            e.DrawFocusRectangle();
        }

        private void _sysFileStatusList_SelectedIndexChanged(object sender, EventArgs e)
        {
            _sysFileStatusList.Refresh();
        }
        /// <summary>
        /// Master内ファイルのバックアップ実行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _backUpBt_Click(object sender, EventArgs e)
        {
            _sysFileStatusList.Items.Add("バックアップを開始します。");
            _sysFileStatusList.Items.Add("バックアップ中・");
            string backupFilePath = null;
            AidLog logs = new AidLog("McCommProc.Backup");
            using (ECNC3Settings us = new ECNC3Settings())
            {
                //	Tempディレクトリを作成
                string tempAppDir = @"Temp";
                if (false == File.Exists(tempAppDir))
                {
                    Directory.CreateDirectory(tempAppDir);
                }
                _sysFileStatusList.Text += "・";
                int saveCount = 0;
                string backupDirectory = @"Backup";
                string tempName = DateTime.Now.ToString("yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
                string tempDir = Path.Combine(tempAppDir, tempName);

                bool isApplicationTerminate = false;
                if (true == string.IsNullOrEmpty(backupFilePath))
                {
                    _sysFileStatusList.Text += "・";

                    isApplicationTerminate = true;
                    using (FileSettings fs = new FileSettings())
                    {
                        fs.Read();
                        //	バックアップパス
                        backupDirectory = fs.AttrText("Root/Apl/Bkup", "path");
                        if (true == string.IsNullOrEmpty(backupDirectory))
                        {
                            backupDirectory = @"Backup";
                        }
                        //	バックアップ数
                        saveCount = fs.AttrValue("Root/Apl/Bkup", "cnt");
                        if (1 > saveCount)
                        {
                            saveCount = 1;
                        }
                    }
                    _sysFileStatusList.Text += "・";

                    //	アプリケーション終了によるバックアップであれば、自動生成したファイル名を使用する。
                    backupFilePath = Path.Combine(backupDirectory, $"{tempName}.zip");
                }
                ResultCodes ret = ResultCodes.Success;
                _sysFileStatusList.Text += "・";

                //	各ファイルのバックアップを実行
                AidAssembly<IEcnc3Backup> ifs = new AidAssembly<IEcnc3Backup>();
                foreach (Type type in ifs.Interfaces)
                {
                    IEcnc3Backup ifc = Activator.CreateInstance(type) as IEcnc3Backup;
                    if (null != ifc)
                    {
                        //	Tempディレクトリへ一時的にコピー
                        ret = ifc.Backup(tempDir);
                        if (ResultCodes.Success == ret)
                        {
                            logs.Sure($"BACKUP {ifc.Name}...{ret}");
                        }
                        else
                        {
                            logs.Sure($"BACKUP {ifc.Name}...{ret}");
                        }
                    }
                }
                _sysFileStatusList.Text += "・";

                //	ファイルを圧縮
                AidZip zip = new AidZip();
                if (0 != zip.Create(tempDir, backupFilePath))
                {
                    ret = ResultCodes.FailToWriteFile;
                }
                if (true == isApplicationTerminate)
                {
                    //	バックアップファイルの既定をそってファイルの削除を行う。
                    FileAccessCommon fa = new FileAccessCommon();
                    fa.BackupStandBy(backupDirectory, saveCount, @"??????????????.zip");
                }
                _sysFileStatusList.Text += "・";
                _sysFileStatusList.Items.Add("バックアップ完了");
            }
            //using (Models.McIf.McCommProc mcComPrc = new Models.McIf.McCommProc())
            //{
            //    mcComPrc.Backup();
            //}
        }
	}
}
