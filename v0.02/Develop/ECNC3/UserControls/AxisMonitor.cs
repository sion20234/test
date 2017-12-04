using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ECNC3.Models;
using ECNC3.Models.McIf;
using System.Threading;

namespace ECNC3.Views
{
    /// <summary>
    /// 少数以下桁数
    /// </summary>
    public enum DigitSelect
    {
        Three = 1,
        Four = 10
    }
    public partial class AxisMonitor : UserControl
    {
        /// <summary>
        /// 単位種類
        /// </summary>
        private enum _unitCategory
        {
            mm,
            inch
        }
        /// <summary>
        /// Invoke処理管理用変数
        /// </summary>
        public IAsyncResult retInvoke = null;
        public AxisMonitor()
        {
            InitializeComponent();
			Disposed += AxisMonitor_Disposed;
        }

        public new void Dispose()
        {
            try
            {
                int retryCount = 0;
                if (retInvoke != null)
                    while (!retInvoke.IsCompleted)
                    {
                        if (retryCount > 10)
                        {
                            Thread.Sleep(100);
                            return;
                        }
                        retryCount++;
                    }
            }
            finally
            {
                base.Dispose();
            }
        }
		private void AxisMonitor_Disposed( object sender, EventArgs e )
		{
            if ( null != MacPosValue ) {
				MacPosValue.Dispose();
				MacPosValue = null;
			}
		}
        #region VariableMember
        Models.StructureAxisCoordinate MacPosValue;
        Models.StructureAxisCoordinate WorkPosValue;
        /// <summary>
        /// 現在の単位モード
        /// </summary>
        private _unitCategory _unit = _unitCategory.mm;
        /// <summary>
        /// 現在の有効桁数
        /// </summary>
        private DigitSelect _digit = DigitSelect.Four;
        private Color _defaultBackColor = FileUIStyleTable.DefaultBackColor;
        private Color _defaultForeColor = FileUIStyleTable.DefaultForeColor;
        private Color _selectedBackColor = FileUIStyleTable.EnabledBackColor;
        private Color _selectedForeColor = FileUIStyleTable.EnabledForeColor;
        private bool _cellClick = false;
        private bool _columnHeaderClick = false;
        public bool WorkSettingEn = false;
        public Point SelectCellRightPosition = new Point(0, 0);
        #endregion
        internal void SetAllMacAxisValue(StatusMonitorEventArgs e)
        {
            int macPositionTemp = 0;
            int workPositionTemp = 0;
            MacPosValue = e.Items.MacAxisPos;
            WorkPosValue = e.Items.WorkAxisPos;
            if( (this.IsDisposed == true || this.Disposing == true)   ) return; retInvoke = BeginInvoke((MethodInvoker)delegate
            {
                for(int ct = 0; ct < _axisMonitorView.RowCount; ct++)
                {
                    switch (ct)
                    {
                        case 0:
                            macPositionTemp = MacPosValue.AxisX;
                            workPositionTemp = WorkPosValue.AxisX;
                            break;

                        case 1:
                            macPositionTemp = MacPosValue.AxisY;
                            workPositionTemp = WorkPosValue.AxisY;
                            break;

                        case 2:
                            macPositionTemp = MacPosValue.AxisW;
                            workPositionTemp = WorkPosValue.AxisW;
                            break;

                        case 3:
                            macPositionTemp = MacPosValue.AxisZ;
                            workPositionTemp = WorkPosValue.AxisZ;
                            break;

                        case 4:
                            macPositionTemp = MacPosValue.AxisA;
                            workPositionTemp = WorkPosValue.AxisA;
                            break;

                        case 5:
                            macPositionTemp = MacPosValue.AxisB;
                            workPositionTemp = WorkPosValue.AxisB;
                            break;

                        case 6:
                            macPositionTemp = MacPosValue.AxisC;
                            workPositionTemp = WorkPosValue.AxisC;
                            break;

                        case 7:
                            macPositionTemp = MacPosValue.Axis8;
                            workPositionTemp = WorkPosValue.Axis8;
                            break;

                    }
                    _axisMonitorView[2, ct].Value = (macPositionTemp * (int)_digit);
                    _axisMonitorView[1, ct].Value = (workPositionTemp * (int)_digit);
                }               
            }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
        }

        internal int GetWMacVal()
        {
            return DigitAlign(_axisMonitorView[2, 2].Value.ToString());
        }
        #region EventHandler
        private void AxisMonitor_Load( object sender, EventArgs e )
	    {
            HeaderPainted = false;
            AxisMonitorView_Init();
    #if __V001_INHIBIT__
    #else
		    ChangeUnitBt.Enabled = false;
    #endif
        }
        private void _axisMonitorView_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            //手動画面以外の場合はクリックイベントを発生させない。
            if (ParentForm.GetType() != typeof(MANUALForm)) return;
            SelectCellRightPosition.X = _axisMonitorView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Right
                                        - ParentForm.Location.X;
            SelectCellRightPosition.Y = _axisMonitorView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Location.Y
                                        + ParentForm.Location.Y;
            AxisMonitorSelectChange(e.ColumnIndex, e.RowIndex);
            if (ParentForm.GetType() == typeof(MANUALForm))
            {
                if (((MANUALForm)ParentForm).NumForm == null)
                {
                    return;
                }
                NumericFeedForm.CoordinateMode coordMode;
                if (e.ColumnIndex == 1)
                {
                    coordMode = NumericFeedForm.CoordinateMode.Work;
                }
                else if (e.ColumnIndex == 2)
                {
                    coordMode = NumericFeedForm.CoordinateMode.Machine;
                }
                else
                {
                    return;
                }

                ((MANUALForm)ParentForm).NumForm.SetSelectAxis(e.RowIndex);
                ((MANUALForm)ParentForm).NumForm.SetCoordinateMode(coordMode);
            }

        }
        /// <summary>
        /// datagridの列ヘッダクリック時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _axisMonitorView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //手動画面以外の場合はクリックイベントを発生させない。
            if (ParentForm.GetType() != typeof(MANUALForm)) return;
            if (e.ColumnIndex == 1)
            {
                if (_columnHeaderClick == true)
                {
                    _columnHeaderClick = false;
                    _axisMonitorView.Columns[1].HeaderCell.Style.BackColor = _defaultBackColor;
                    _axisMonitorView.Columns[1].HeaderCell.Style.ForeColor = _defaultForeColor;
                    //((MANUALForm)(ParentForm)).OpenWorkSettingForm();
                }
                else
                {
                    _columnHeaderClick = true;
                    _axisMonitorView.Columns[1].HeaderCell.Style.BackColor = _selectedBackColor;
                    _axisMonitorView.Columns[1].HeaderCell.Style.ForeColor = _selectedForeColor;
                }

            }
        }
        public void SetSelectWorkPosition(bool select)
        {
            if (select == false)
            {
                _axisMonitorView.Columns[1].HeaderCell.Style.BackColor = _defaultBackColor;
                _axisMonitorView.Columns[1].HeaderCell.Style.ForeColor = _defaultForeColor;
            }
            else
            {
                _axisMonitorView.Columns[1].HeaderCell.Style.BackColor = _selectedBackColor;
                _axisMonitorView.Columns[1].HeaderCell.Style.ForeColor = _selectedForeColor;
            }
            _columnHeaderClick = select;
        }
        /// <summary>
        /// datagridの選択が変わったら選択を解除する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _axisMonitorView_SelectionChanged(object sender, EventArgs e)
        {
            if (_axisMonitorView.SelectedCells.Count == 0) return;
            _axisMonitorView.SelectedCells[0].Selected = false;
        }

        public NumericFeedForm.CoordinateMode GetSelectCoordinate()
        {
            NumericFeedForm.CoordinateMode ret = NumericFeedForm.CoordinateMode.Work;
            int tempIndex = 0;
            foreach (DataGridViewColumn col in _axisMonitorView.Columns)
            {
                if (col.HeaderCell.Style.BackColor == FileUIStyleTable.EnabledBackColor)
                {
                    tempIndex = col.HeaderCell.ColumnIndex;
                }
            }
            switch (tempIndex)
            {
                case 1: ret = NumericFeedForm.CoordinateMode.Work; break;
                case 2: ret = NumericFeedForm.CoordinateMode.Machine; break;
            }
            return ret;
        }

        #endregion
        #region DataGridView
        /// <summary>
        /// Gridの初期化処理
        /// </summary>
        private void AxisMonitorView_Init()
        {
            //列ヘッダスタイル設定
            Grid_StyleInit(_axisMonitorView, "Meiryo UI", 20, 25, 40);
            Grid_ColumnInit(_axisMonitorView.Columns[0], "Meiryo UI", 20, 45);
            Grid_ColumnInit(_axisMonitorView.Columns[1], "Meiryo UI", 20, 160);
            Grid_ColumnInit(_axisMonitorView.Columns[2], "Meiryo UI", 20, 160);
            Grid_ColumnInit(_axisMonitorView.Columns[3], "Meiryo UI", 12, 45);
            _axisMonitorView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //セルデータ設定
            string[] axisName = { "X", "Y", "W", "Z", "A", "B", "C", "I" };
            using (FileSettings fs = new FileSettings())
            {
                _axisMonitorView.ReadOnly = true;
                _axisMonitorView.BorderStyle = BorderStyle.None;

                //ファイル読み込み
                fs.Read();
                int intCount = LoadAxisCount(fs);
                if (intCount == 0)
                {//軸表示数が0を指定したのに1行出るのを修正：柏原
                    _axisMonitorView.AllowUserToAddRows = false;//行追加不可
                    return;
                }
                _axisMonitorView.RowCount = intCount;
                //_axisMonitorView.RowCount = LoadAxisCount(fs);//コメント化：柏原
                LoadDigit(fs);

                axisName[0] = fs.AttrText("Root/AxisInfomation/EnableAxis", "axis1");
                axisName[1] = fs.AttrText("Root/AxisInfomation/EnableAxis", "axis2");
                axisName[2] = fs.AttrText("Root/AxisInfomation/EnableAxis", "axis3");
                axisName[3] = fs.AttrText("Root/AxisInfomation/EnableAxis", "axis4");
                axisName[4] = fs.AttrText("Root/AxisInfomation/EnableAxis", "axis5");
                axisName[5] = fs.AttrText("Root/AxisInfomation/EnableAxis", "axis6");
                axisName[6] = fs.AttrText("Root/AxisInfomation/EnableAxis", "axis7");
                axisName[7] = fs.AttrText("Root/AxisInfomation/EnableAxis", "axis8");
            }
            //単位表示設定
            for (int rowCt = 0; rowCt < _axisMonitorView.RowCount; rowCt++)
            {
                _axisMonitorView[0, rowCt].Value = axisName[rowCt];
                switch(rowCt)
                {
                    case 4:
                    case 5:
                        _axisMonitorView[3, rowCt].Value = "deg";
                        break;

                    default:
                        _axisMonitorView[3, rowCt].Value = _unit.ToString();
                        break;

                }
            }
            //_axisMonitorView.ReadOnly = true;
            //_axisMonitorView.BorderStyle = BorderStyle.None;
        }
        /// <summary>
        /// DataGridViewスタイル設定初期化
        /// </summary>
        /// <param name="grid">DataGridViewのインスタンス</param>
        /// <param name="backColor">背景色</param>
        /// <param name="foreColor">文字色</param>
        /// <param name="font">フォント（文字列）</param>
        /// <param name="size">フォントサイズ</param>
        /// <param name="colHedderHeight">Gridの列ヘッダー高さ</param>
        /// <param name="rowHeight">Gridの行の高さ</param>
        private void Grid_StyleInit(DataGridView grid, string font, float size, int colHedderHeight, int rowHeight)
        {
            grid.ColumnHeadersDefaultCellStyle.BackColor = _defaultBackColor;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = _defaultForeColor;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("font", size, FontStyle.Regular);
            grid.ColumnHeadersHeight = colHedderHeight;
            grid.RowTemplate.Height = rowHeight;
            grid.BackgroundColor = _defaultBackColor;
            grid.DefaultCellStyle.SelectionBackColor = _defaultBackColor;
            grid.DefaultCellStyle.SelectionForeColor = _defaultForeColor;
            grid.DefaultCellStyle.BackColor = _defaultBackColor;
            grid.DefaultCellStyle.ForeColor = _defaultForeColor;
            grid.Rows[grid.RowCount - 1].Height = rowHeight;
            //DataGridView1の列の幅をユーザーが変更できないようにする
            grid.AllowUserToResizeColumns = false;
            //DataGridView1の行の高さをユーザーが変更できないようにする
            grid.AllowUserToResizeRows = false;
        }
        /// <summary>
        /// DataGridViewColmns初期化処理
        /// </summary>
        /// <param name="col">DataGridViewColumnのインスタンス</param>
        /// <param name="font">フォントファミリー</param>
        /// <param name="size">文字サイズ</param>
        /// <param name="width">セル幅</param>
        private void Grid_ColumnInit(DataGridViewColumn col, string font, float size, int width)
        {
            col.DefaultCellStyle.Font = new Font(font, size, FontStyle.Regular);
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            col.Width = width;
            col.SortMode = DataGridViewColumnSortMode.NotSortable;
        }
        public NumericFeedForm.AxisMode GetSelectAxis()
        {
            NumericFeedForm.AxisMode ret = NumericFeedForm.AxisMode.NULL;
            int tempIndex = 0;
            for (int ict = 0; ict < _axisMonitorView.Rows.Count; ict++)
            {
                if (_axisMonitorView.Rows[ict].Cells[0].Style.BackColor == FileUIStyleTable.EnabledBackColor)
                {
                    tempIndex = ict;
                }
            }

            switch (tempIndex)
            {
                case 0: ret = NumericFeedForm.AxisMode.X; break;
                case 1: ret = NumericFeedForm.AxisMode.Y; break;
                case 2: ret = NumericFeedForm.AxisMode.W; break;
                case 3: ret = NumericFeedForm.AxisMode.Z; break;
                case 4: ret = NumericFeedForm.AxisMode.A; break;
                case 5: ret = NumericFeedForm.AxisMode.B; break;
                case 6: ret = NumericFeedForm.AxisMode.C; break;
                case 7: ret = NumericFeedForm.AxisMode.I; break;
            }
            return ret;
        }
        /// <summary>
        /// 指定したセルを選択色に変更する。その他のセルは標準色戻す。
        /// </summary>
        /// <param name="columnIndex">列番号</param>
        /// <param name="rowIndex">行番号</param>
        public void AxisMonitorSelectChange(int columnIndex, int rowIndex)
        {
            if (columnIndex == 0 || rowIndex == -1 || columnIndex == 3)
            {
                return;
            }
            //AxisMonitorSelectClear();//ここでHeaderCell.Styleに_defaultxxxを
            //セットしさらに下記で別色をセットしていたのでちらついていた。：柏原
            if (_lastTimeColumnIndex == -2)
            {//初回 columnIndex & rowIndexが無い場合
                AxisMonitorSelectClear();
            }else{
                if( _lastTimeColumnIndex == columnIndex && _lastTimeRowIndex== rowIndex)
                {//前回と同じセル位置
                    //もうすでに色がセットされている
                }
                else
                {//前回と異なるセル
                    AxisMonitorSelectClear();
                }
            }
            _axisMonitorView[columnIndex, rowIndex].Style.BackColor = _selectedBackColor;
            _axisMonitorView[columnIndex, rowIndex].Style.ForeColor = _selectedForeColor;
            _axisMonitorView.Columns[columnIndex].HeaderCell.Style.BackColor = _selectedBackColor;
            _axisMonitorView.Columns[columnIndex].HeaderCell.Style.ForeColor = _selectedForeColor;
            _axisMonitorView[0, rowIndex].Style.BackColor = _selectedBackColor;
            _axisMonitorView[0, rowIndex].Style.ForeColor = _selectedForeColor;
            _cellClick = true;

            _lastTimeColumnIndex = columnIndex;
            _lastTimeRowIndex = rowIndex;
        }
        private int _lastTimeColumnIndex    = -2;
        private int _lastTimeRowIndex       = -2;
        /// <summary>
        /// datagridの選択色を全セルクリアする。
        /// </summary>
        public void AxisMonitorSelectClear(bool deleteHeader = true, bool resetCellClick = true)
        {
            if(deleteHeader == true)
            {
                foreach (DataGridViewColumn col in _axisMonitorView.Columns)
                {
                    col.HeaderCell.Style.BackColor = _defaultBackColor;
                    col.HeaderCell.Style.ForeColor = _defaultForeColor;
                }
            }
            foreach (DataGridViewRow row in _axisMonitorView.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.Style.BackColor = _defaultBackColor;
                    cell.Style.ForeColor = _defaultForeColor;
                }
            }
            if(resetCellClick == true)_cellClick = false;
        }
        /// <summary>
        /// datagridのセルをクリックしたかどうかを取得する
        /// </summary>
        /// <returns>
        /// true:クリックON
        /// false:クリックOFF
        /// </returns>
        public bool GetCellClick()
        {
            if (_cellClick == true)
            {
                _cellClick = false;
                return true;
            }
            return false;
        }
        /// <summary>
        /// datagridのセルをクリックしたかどうかを取得する
        /// </summary>
        /// <returns>
        /// true:クリックON
        /// false:クリックOFF
        /// </returns>
        public bool GetColumnHeaderOnClick()
        {
            return _columnHeaderClick;
        }
        /// <summary>
        /// datagridのセルをクリックしたかどうかを設定する
        /// </summary>
        /// <returns>
        /// true:クリックON
        /// false:クリックOFF
        /// </returns>
        public void SetColumnHeaderOnClick(bool click)
        {
            _columnHeaderClick = click;
        }

        #endregion
        #region PrivateMethods
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
            DigitChange(_digit);
        }
        /// <summary>
        /// 有効桁数変更
        /// </summary>
        /// <param name="dig">
        /// DigitSelect.Three：3桁
        /// DigitSelect.Four：4桁
        /// </param>
        private void DigitChange(DigitSelect dig)
        {
            string format = "###0'.'0000";
            switch (dig)
            {
                case DigitSelect.Three:
                    format = "###0'.'000";
                    break;

                case DigitSelect.Four:
                    format = "###0'.'0000";
                    break;

            }
            //書式指定
            _axisMonitorView.Columns[1].DefaultCellStyle.Format = format;
            _axisMonitorView.Columns[2].DefaultCellStyle.Format = format;
        }
        //-----------------------------------------------------------------------------------------
        //
        //		Title			: 表示用、単位変換メソッド
        //		Function Name	: ChangeUnit
        //	    Input		    : 
        //		Output		    : 
        //		Return		    : 
        //	    Description	    : 軸座標リストの単位のインチ/ミリ表示切替処理
        //
        //-----------------------------------------------------------------------------------------
        internal void ChangeUnit()
        {
            //単位変換ボタンの処理
            for (int ct = 0; ct < _axisMonitorView.RowCount; ct++)
            {
                switch (ct)
                {
                    case 4:
                    case 5:
                        _axisMonitorView[3, ct].Value = "deg";
                        break;

                    default:
                        _axisMonitorView[3, ct].Value = _unit.ToString();
                        break;

                }
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
            decimal decWLim = decimal.Parse(AxisVal) * 1000;//整数化;
            if (decWLim == 0)
            {
                return　ret;
            }
            //小数以下を取り除き、整数に変換
            string stringWLim = decWLim.ToString();
            int dotIndex = stringWLim.IndexOf(".");
            if(dotIndex > 0)
            {
                stringWLim = stringWLim.Substring(0, dotIndex);
            }

            //NCへデータセット
            int intResult = 0;
            bool boolParse = int.TryParse(stringWLim,out intResult);
            if (boolParse == false)
            {
                stringWLim = "0";//0リセット
                //<Item num="5518" title="W軸上限値オーバー" txt="範囲を超えましたので、0をセットします。" />
                using (MessageDialog msg = new MessageDialog())
                {
                    msg.Error(5518, this);
                }
            }
            ret = int.Parse(stringWLim);
            return ret;
        }
        //-----------------------------------------------------------------------------------------
        //
        //		Title			: インチミリ単位変換メソッド
        //		Function Name	: UnitChgVal
        //		Input		    : iMode===1--inchモード、2--ミリモード
        //                      : dVal===座標値
        //		Output		    : dRet===ミリインチ切替後の座標値
        //		Return		    :  0 　 　正常終了
        //		Description 	: 指定した座標値をミリインチ変換
        //
        //-----------------------------------------------------------------------------------------
        internal void UnitChgVal(int iMode, double dVal, out double dRet)                  //1--inchモード、2--ミリモード
        {
            dRet = dVal;
            switch (iMode)
            {
                case 0:
                    dRet = dVal / 25.4;
                    break;

            }
        }
        #endregion
        private Rectangle newRect = new Rectangle();
        private Pen _outLinePen = new Pen(FileUIStyleTable.SelectedLineColor, 3.0F);
        private bool _headerPainted = false;
        public bool HeaderPainted
        {
            get
            {
                return _headerPainted;
            }
            set
            {
                _headerPainted = value;
                this.Refresh();
            }
        }
        private void _axisMonitorView_Paint(object sender, PaintEventArgs e)
        {
            if (HeaderPainted == true)
            {
                //グラデーションブラシを作成
                newRect.X = _axisMonitorView.Columns[1].HeaderCell.ContentBounds.X 
                    + _axisMonitorView.Columns[0].HeaderCell.Size.Width + -1;
                newRect.Y = _axisMonitorView.Columns[1].HeaderCell.ContentBounds.Y + -1;
                newRect.Width = _axisMonitorView.Columns[1].Width;
                newRect.Height = _axisMonitorView.Columns[1].HeaderCell.ContentBounds.Height + 4;
                e.Graphics.DrawRectangle(_outLinePen, newRect);
            }
        }
    }
}
