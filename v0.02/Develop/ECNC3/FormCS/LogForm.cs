﻿///////////////////////////////////////////////////////////////////////////////////////////
//
// (1) システム名称 : ELENIX ECNCⅢ
// (2) ファイル名   : LogForm.cs
// (3) 概要         : 加工ログ表示画面
// (4) 作成日       : 2015.06.01
// (5) 作成者       : 八野
// (6) 特記事項     : VisualStudio2015 C#
// (7) 更新履歴     : ログ設定に追加：2017-06-15：柏原
//
///////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;        //テキストR/W
using ECNC3.Models;     //FilePathInfo
using System.Drawing;
using ECNC3.Models.McIf;
using ECNC3.Enumeration;
using System.Threading;
using System.Collections.Generic;
using ECNC3.Views.UserControls;

namespace ECNC3.Views
{
    public partial class LogForm : UserControlEx
    {
		#region<初期化>
		Point mousePoint;
		LogSettingForm SettingForm;
        int _macroNum1 = 10000;
        int _macroNum2 = 10001;
        int _macroNum3 = 10002;
        int _macroNum4 = 10003;

        private bool _ExpandEn = false;
        /// <summary>
        /// 拡大モードフラグ
        /// true:拡大モード　false:縮小モード
        /// </summary>
        public bool ExpandEn
        {
            get
            {
                return _ExpandEn;
            }
            set
            {
                _ExpandEn = value;
                if(_ExpandEn == true)
                {
                    //プロット表示にかぶるので非表示
                    this.Visible = false;
                    this.Location = new Point(0, 7);      //表示位置設定
                    this.Size = new Size(870, 515);     //フォームサイズ：変更
                    this.chart1.Location = new Point(10, 7);
                    this.chart1.Size = new Size(676, 498);//チャートサイズ：変更
                    this.OutLineEnable = true;
                    this.Visible = true;
                }
                else
                {
                    this.Visible = false;
                    this.Location = new Point(560, 114);    //表示位置設定
                    this.Size = new Size(293, 174);     //フォームサイズ：変更
                    this.chart1.Location = new Point(0, 0);
                    chart1.Size = new Size(319, 188);//チャートサイズ：変更
                    this.OutLineEnable = false;
                    this.Visible = true;
                }
            }
        }

		/// <summary>
		/// フォーム　コンストラクタ
		/// </summary>
		public LogForm()
        {
            InitializeComponent();
            // チャートの表示を初期化
            initChart(chart1);
            ExpandEn = false;
            _Init_View_Macro();
            buttonEx_LogSetting.ProgressBarEnable = true;
        }
        private void ProgressCheck()
        {
            if ((this.IsDisposed == true || this.Disposing == true)) return;
            this.Invoke((MethodInvoker)delegate
            {
                buttonEx_LogSetting.ProgressBarValue = SettingForm.ProgressValue;
            }); 
        }
        /// <summary>
        /// フォーム　ロード時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogForm_Load( object sender, EventArgs e )
		{
            
        }
        private void _Init_View_Macro()
        {
            try
            {
                using (FileSettings fs = new FileSettings())
                {
                    //ファイル読み込み
                        fs.Read();
                    string macroNum1 = fs.AttrText("Root/LogSettingFile/LogTitleNumSet", "title1");
                    string macroNum2 = fs.AttrText("Root/LogSettingFile/LogTitleNumSet", "title2");
                    string macroNum3 = fs.AttrText("Root/LogSettingFile/LogTitleNumSet", "title3");
                    string macroNum4 = fs.AttrText("Root/LogSettingFile/LogTitleNumSet", "title4");
                    //グラフ
                    using (FileMacroManage macroMgr = new FileMacroManage(fs.AttrText("Root/LogSettingFile/OpenFile", "name")))
                    {
                        string macroComment1 = string.Empty;
                        string macroComment2 = string.Empty;
                        string macroComment3 = string.Empty;
                        string macroComment4 = string.Empty;
                        _macroNum1 = int.Parse(macroNum1);
                        _macroNum2 = int.Parse(macroNum2);
                        _macroNum3 = int.Parse(macroNum3);
                        _macroNum4 = int.Parse(macroNum4);
                        macroMgr.Read();
                        foreach(StructureMacroManageItem macroItem in macroMgr.Items)
                        {
                            //マクロコメントのセットが完了次第処理を抜ける
                            if(
                                macroComment1 != string.Empty
                                && macroComment2 != string.Empty
                                && macroComment3 != string.Empty
                                && macroComment4 != string.Empty
                                )
                            {
                                break;
                            }
                            if (macroNum1 == macroItem.Number.ToString()) macroComment1 = macroItem.Comment;
                            if (macroNum2 == macroItem.Number.ToString()) macroComment2 = macroItem.Comment;
                            if (macroNum3 == macroItem.Number.ToString()) macroComment3 = macroItem.Comment;
                            if (macroNum4 == macroItem.Number.ToString()) macroComment4 = macroItem.Comment;
                        }
                        _MacroLabel1.Text = macroComment1;
                        _MacroLabel2.Text = macroComment2;
                        _MacroLabel3.Text = macroComment3;
                        _MacroLabel4.Text = macroComment4;
                    }
                }
            }
            catch (Exception exc)
            {
                //例外処理
                MessageBox.Show(exc.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //bool _threadStop = false;
        McReqOneVariable datMacro = new McReqOneVariable(0);
        double[] macroTemps = new double []{ 0, 0, 0, 0 };

        public void LogStatusView(StatusMonitorEventArgs e, DigitSelect enableDigitCount)
        { 
            if (true == ExpandEn)
            {
                datMacro.MacroNumber = _macroNum1;
                datMacro.Read();
                _MacroValue1.Text = datMacro.MacroData.var.ToString();
                datMacro.MacroNumber = _macroNum2;
                datMacro.Read();
                _MacroValue2.Text = datMacro.MacroData.var.ToString();
                datMacro.MacroNumber = _macroNum3;
                datMacro.Read();
                _MacroValue3.Text = datMacro.MacroData.var.ToString();
                datMacro.MacroNumber = _macroNum4;
                datMacro.Read();
                _MacroValue4.Text = datMacro.MacroData.var.ToString();
                _TargetDistLabel.Text = (enableDigitCount == DigitSelect.Three) ? 
                    ((float)(e.Items.PrcsTargetDist) ).ToString("###0'.'000") : 
                    ((float)(e.Items.PrcsTargetDist) * 10).ToString("###0'.'0000");
                _NowDistLabel.Text = (enableDigitCount == DigitSelect.Three) ? 
                    ((float)(e.Items.WorkAxisPos.AxisZ)).ToString("###0'.'000") : 
                    ((float)(e.Items.WorkAxisPos.AxisZ) * 10).ToString("###0'.'0000");
                _LastDistLabel.Text = (enableDigitCount == DigitSelect.Three) ? 
                    ((float)((e.Items.PrcsTargetDist - e.Items.PrcsNowDist))).ToString("###0'.'000") : 
                    ((float)((e.Items.PrcsTargetDist - e.Items.PrcsNowDist)) * 10).ToString("###0'.'0000");

            }
        }
        #endregion
        #region<終了時>
        /// <summary>
        /// FileFormクラス閉じる
        /// </summary>
        private void FileFormClose()
        {
            if (_logFileForm != null)
            {
                _logFileForm.Close();//FileForm閉じる
                _logFileForm = null;
            }
            if (_logFilePath != null)
            {
                _logFilePath = null;
            }
        }
        /// <summary>
        /// 閉じる　ボタン　クリック時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEx_Back_Click( object sender, EventArgs e )
		{
            ExpandEn = false;
        }
        #endregion
        #region<各ボタン：クリック>
        /// <summary>
        /// データ出力ボタン：クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEx_DataExport_Click( object sender, EventArgs e )
		{
			LogExport( FilePathInfo.User, FileFormMode.UserLogExport, 0 );//-1だとツリーが展開しません。
		}
		/// <summary>
		/// データ削除ボタン：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonEx_DataDelete_Click( object sender, EventArgs e )
		{
			LogDelete( FilePathInfo.User, FileFormMode.UserLogDelete, 0 );
		}
		/// <summary>
		/// ログ出力(エクスポート(USB))：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonEx_LogExport_Click( object sender, EventArgs e )
		{
			LogExport( FilePathInfo.Usb, FileFormMode.ProcLogExport, 0 );//-1だとツリーが展開しません。
		}
		/// <summary>
		/// ログ削除：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonEx_LogDelete_Click( object sender, EventArgs e )
		{
			LogDelete( FilePathInfo.Usb, FileFormMode.ProcLogDelete, 0 );
		}
		/// <summary>
		/// 設定ボタン：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonEx_LogSetting_Click( object sender, EventArgs e )
		{
			SettingForm = new LogSettingForm();
            SettingForm.NotifyUpdate = ProgressCheck;
            SettingForm.Show( this );
		}
		#endregion
		#region<テンキー：マウス>
		/// <summary>
		/// 移動：マウス：ダウン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _movebt_MouseDown(object sender, MouseEventArgs e)
        {
            TenKeyMouseDown(sender, e);
        }
		/// <summary>
		/// 移動：マウス：移動
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _movebt_MouseMove(object sender, MouseEventArgs e)
        {
            TenKeyMouseMove(sender, e);
        }
        /// <summary>
        /// テンキー：マウス：ダウン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TenKeyMouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                mousePoint = new Point(e.X, e.Y);//位置を記憶
            }
        }
		/// <summary>
		/// テンキー：マウス：移動
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TenKeyMouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                if ((this.Top + e.Y - mousePoint.Y) <= 0)
                {
                    this.Top = 0;
                }
                else
                {
                    this.Top += e.Y - mousePoint.Y;
                }
                this.Left += e.X - mousePoint.X;
            }
        }
		/// <summary>
		/// テンキー：マウス：アップ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TenKeyMouseUp(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                if ((this.Top + e.Y - mousePoint.Y) <= 0)
                {
                    this.Top = 0;
                }
                else
                {
                    this.Top += e.Y - mousePoint.Y;
                }
                this.Left += e.X - mousePoint.X;
                //if (this.Left < 0 || (this.Left + this.Size.Width) > 1024) this.Left = 0;
                //if (this.Top < 0 || (this.Top + this.Size.Height) > 768) this.Top = 0;
            }
        }
        #endregion
        #region<出力/削除>
        private string _logFilePath;
        private FileForm _logFileForm;
        /// <summary>
        /// 出力：エクスプローラ風を表示
        /// </summary>
        private void LogExport( string filePath , FileFormMode fileFormMode,  int firstShowFolder)
		{
             //記録
            _logFilePath = filePath;
            _logFileForm = null;
            //try {
            //ツリービューで選択されたフォルダパスを取得
            _logFileForm = new FileForm( fileFormMode, "", firstShowFolder );//リスト側でファイル選択は無効
            _logFileForm.NotifyReturn = logForm_OnNotifyReturnOk;//OK時の通知セット
            _logFileForm.Show(this);//モーダレスで表示　※thisを付けると、画面後方に移動しない。
            return;
		}
        /// <summary>
        /// FileFormクラスからのDelegate通知：OK
        /// </summary>
        private void logForm_OnNotifyReturnOk()
        {
            FileForm file = _logFileForm;//記録値
            DirectoryInfo di = null;
            try
            {
                if (file._returnFullPaths.Length == 0) return;         //データ無し
                if (file._returnFullPaths[0] == "") return;            //未選択

                string path = _logFilePath;                             //Path.xml：パスにコピー保存
                if (path.EndsWith("/") == false) path += "/";           //最後にスラッシュが無い場合、追加
                int loopMax = file._returnFullPaths.Length;             //ループ回数＝リスト配列数
                //コピー先パス
                if (!File.Exists(path))
                {
                    //存在しない場合、コピー先パスを作成
                    di = Directory.CreateDirectory(path);
                }
                //選択されたファイルリスト数だけコピー処理をします。
                for (int loopCount = 0; loopCount < loopMax; loopCount++)
                {
                    if (File.Exists(path + file._returnPaths[loopCount]))
                    {
                        //同名確認
                        if (file._returnFullPaths[loopCount] == path + file._returnPaths[loopCount])
                        {
                            //コピー元とコピー先が同じ？
                            using (MessageDialog msg = new MessageDialog())
                            {
                                msg.Error(5518, this);
                                {
                                    return;//title="コピー失敗" txt="コピー元とコピー先が同じなので、コピーできません。
                                }
                            }
                        }
                        using (MessageDialog msg = new MessageDialog())
                        {
                            //同名ファイルが存在します、上書きしますか？
                            string addMes = file._returnPaths[loopCount] +  //リストで選択されたファイル名
                                             Environment.NewLine +          //２行隙間をあける
                                             Environment.NewLine;
                            bool dialogResult = msg.Question(5519, this, addMes);
                            if (dialogResult == false)
                            {
                                continue;//上書きしません。
                            }
                        }
                        //上書きする場合、まずコピー先ファイルを削除
                        //File.Delete( path + file._returnPaths[loopCount] );
                    }
                    //ファイルをコピー
                    File.Copy(file._returnFullPaths[loopCount], path + file._returnPaths[loopCount], true);//上書き
                }
            }
            catch (Exception ex)
            {
                ECNC3Exception.XmlFilter(ex);//ECNC3例外処理
            }
            finally
            {
                FileFormClose();
            }
        }
        /// <summary>
        /// 削除：１データ選択
        /// </summary>
        private void LogDelete( string filePath, FileFormMode fileFormMode, int firstShowFolder = -1)
		{
            //記録
            _logFilePath = filePath;
            _logFileForm = null;
            //ファイルフォーム：ツリービュー＋リストビュー
            _logFileForm = new FileForm(fileFormMode, "", firstShowFolder );//ProcLogDelの3はFileForm内で指定ファイルが削除され「閉じる」でリターンします。
           // _logFileForm.NotifyReturn = logForm_OnNotifyReturnDelOk;//OK時の通知セット
            _logFileForm.Show(this);//モーダレスで表示　※thisを付けると、画面後方に移動しない。
        }
        /// <summary>
        /// FileFormクラスからのDelegate通知：OK
        /// </summary>
        /// <para>現在未使用</para>
        private void logForm_OnNotifyReturnDelOk()
        {
			try {
				if(_logFileForm._returnFullPath.Length == 0 ) return;       //データ無し
				if(_logFileForm._returnFullPath == "" ) return;             //未選択
				File.Delete(_logFileForm._returnFullPath );				    //削除
			} catch(Exception ex ) {
				ECNC3Exception.XmlFilter(ex );//ECNC3例外処理
			} finally {
				if(_logFileForm != null ) {
                    _logFileForm.Close(); //閉じる
                    _logFileForm = null;
				}
			}
		}
        #endregion
        #region < Plot >
        // 取得データの履歴
        const int MAX_HISTORY = 40;
        Queue<double> countHistory = new Queue<double>();

        public void SetPlotValue(double value)
        {
            countHistory.Enqueue(value);

            //------------------------------------------------
            // 履歴の最大数を超えていたら、古いものを削除する
            //------------------------------------------------
            while (countHistory.Count > MAX_HISTORY)
            {
                countHistory.Dequeue();
            }

            //------------------------------------------------
            // グラフを再描画する
            //------------------------------------------------
            showChart(chart1);
        }


        private void initChart(Chart chart)
        {
            // チャート全体の背景色を設定
            chart.BackColor = Models.FileUIStyleTable.DefaultBackColor;
            chart.ChartAreas[0].BackColor = Color.Transparent;

            // チャート表示エリア周囲の余白をカットする
            chart.ChartAreas[0].InnerPlotPosition.Auto = false;
            chart.ChartAreas[0].InnerPlotPosition.Width = 100; // 100%
            chart.ChartAreas[0].InnerPlotPosition.Height = 90;  // 90%(横軸のメモリラベル印字分の余裕を設ける)
            chart.ChartAreas[0].InnerPlotPosition.X = 8;
            chart.ChartAreas[0].InnerPlotPosition.Y = 0;


            // X,Y軸情報のセット関数を定義
            Action<Axis> setAxis = (axisInfo) => {
                // 軸のメモリラベルのフォントサイズ上限値を制限
                axisInfo.LabelAutoFitMaxFontSize = 8;

                // 軸のメモリラベルの文字色をセット
                axisInfo.LabelStyle.ForeColor = Models.FileUIStyleTable.DefaultForeColor;

                // 軸タイトルの文字色をセット(今回はTitle未使用なので関係ないが...)
                axisInfo.TitleForeColor = Models.FileUIStyleTable.DefaultForeColor;

                // 軸の色をセット
                axisInfo.MajorGrid.Enabled = true;
                axisInfo.MajorGrid.LineColor = Models.FileUIStyleTable.DefaultLineColor;
                axisInfo.MinorGrid.Enabled = false;
                axisInfo.MinorGrid.LineColor = Models.FileUIStyleTable.DefaultLineColor;
            };




            // X,Y軸の表示方法を定義
            setAxis(chart.ChartAreas[0].AxisY);
            setAxis(chart.ChartAreas[0].AxisX);
            chart.ChartAreas[0].AxisX.MinorGrid.Enabled = true;
            chart.ChartAreas[0].AxisY.Maximum = 999.999;    // 縦軸の最大値を100にする

            chart.AntiAliasing = AntiAliasingStyles.None;

            // 折れ線グラフとして表示
            chart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            // 線の色を指定
            chart.Series[0].Color = Models.FileUIStyleTable.SelectedLineColor;
            chart.Series[0].BorderWidth = 2;//線幅＝１から２に変更
                                            // 凡例を非表示,各値に数値を表示しない
            chart.Series[0].IsVisibleInLegend = false;
            chart.Series[0].IsValueShownAsLabel = false;

            // チャートに表示させる値の履歴を全て0クリア
            while (countHistory.Count <= MAX_HISTORY)
            {
                countHistory.Enqueue(0);
            }
        }


        //***************************************************************************
        /// <summary> チャートを描画する
        /// </summary>
        /// <param name="chart"></param>
        //***************************************************************************
        private void showChart(Chart chart)
        {

            //-----------------------
            // チャートに値をセット
            //-----------------------
            chart.Series[0].Points.Clear();
            foreach (double value in countHistory)
            {

                // データをチャートに追加
                chart.Series[0].Points.Add(new DataPoint(0, value));
            }
        }
        /// <summary>
        /// ManualFormにイベント通知
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chart1_Click(object sender, EventArgs e)
        {
            ExpandEn = !ExpandEn;
            //呼び出し先に通知
            _plotNotify.Invoke();
            //「MDIAUTOForm」からも呼ばれるので、
            //「MDIAUTOForm」の「plot1」の「Enabled」は「false」で
            //クリック時ここが呼ばれない様にしてあります。
        }
        /// <summary>設定結果取得関数定義</summary>
        public delegate void PlotNotifyDelegate();
        /// <summary>OK終了通知</summary>
        private PlotNotifyDelegate _plotNotify = null;
        /// <summary>>設定結果取得</summary>
        public PlotNotifyDelegate plotNotify
        {
            set { _plotNotify = value; }
            get { return _plotNotify; }
        }
        #endregion


    }
}
