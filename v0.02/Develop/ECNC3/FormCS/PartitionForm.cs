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
using System.Threading;
using System.Windows.Forms;

namespace ECNC3.Views
{
    public partial class PartitionForm : ECNC3Form
    {
        public PartitionForm()
        {
            InitializeComponent();
        }
        private int PartitionEnable = 0;
        private int _enableGuideCount = 6;
        /// <summary>ポップアップテンキー</summary>
        private TenKeyDialog _popupTenkey = new TenKeyDialog("", 0, 0, 0);//初回インスタンスを作っておく
        /// <summary>
        /// クリアボタンクリック時のアブソ座標
        /// </summary>
        private int _clearAbsPos = 0;
        private void BackAECFormBt_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //CellPaintingイベントハンドラ
        private void PdataListView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //列ヘッダーかどうか調べる
            if (e.ColumnIndex < 0 && e.RowIndex >= 0)
            {
                //セルを描画する
                e.Paint(e.ClipBounds, DataGridViewPaintParts.All);

                //行番号を描画する範囲を決定する
                //e.AdvancedBorderStyleやe.CellStyle.Paddingは無視しています
                Rectangle indexRect = e.CellBounds;
                indexRect.Inflate(-2, -2);
                //行番号を描画する
                TextRenderer.DrawText(e.Graphics,
                    (e.RowIndex + 1).ToString(),
                    e.CellStyle.Font,
                    indexRect,
                    e.CellStyle.ForeColor,
                    TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
                //描画が完了したことを知らせる
                e.Handled = true;
            }
        }

        private void PdataListView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void NumDown_Click(object sender, EventArgs e)
        {

        }

        private void NumUp_Click(object sender, EventArgs e)
        {

        }

        private void PartitionForm_Load(object sender, EventArgs e)
        {
            this.OutLineEnable = true;
            this.OutLineColor = FileUIStyleTable.SelectedLineColor;
            this.OutLineSize = 3;
            _SettingBt.Enabled = false;
            _CrearBt.Enabled = false;
            _CrearBt.SetLed(false);
            panel3.OutLineColor = FileUIStyleTable.OutLineColor;
            _PartitionList.BackgroundColor = FileUIStyleTable.DefaultBackColor;
            //行列の数の指定
            _PartitionList.Columns.Add("functionCol", "");
            _PartitionList.InitCol("functionCol", 16, typeof(string));
            _PartitionList.Columns.Add("oneCol", "1");
            _PartitionList.InitCol("oneCol", 16, typeof(string));
            _PartitionList.Columns.Add("twoCol", "2");
            _PartitionList.InitCol("twoCol", 16, typeof(string));
            _PartitionList.Columns.Add("threeCol", "3");
            _PartitionList.InitCol("threeCol", 16, typeof(string));
            _PartitionList.Columns.Add("fourCol", "4");
            _PartitionList.InitCol("fourCol", 16, typeof(string));
            _PartitionList.Columns.Add("fiveCol", "5");
            _PartitionList.InitCol("fiveCol", 16, typeof(string));
            _PartitionList.Columns.Add("sixCol", "6");
            _PartitionList.InitCol("sixCol", 16, typeof(string));
            _PartitionList.RowCount = 7;
            _PartitionList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _PartitionList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            //_PartitionList.AutoResizeRows();
            foreach(DataGridViewColumn col in _PartitionList.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            foreach(DataGridViewRow row in _PartitionList.Rows)
            {
                row.Height = 52;
                row.Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            for (int rowCellsCt = 0; rowCellsCt < _PartitionList.Rows[2].Cells.Count; rowCellsCt++)
            {
                _PartitionList.Rows[2].Cells[rowCellsCt].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            _PartitionList.ColumnHeadersHeight = 52;
            PartitionTableLoad();
            _PartitionList.Initialize(30, 60);
        }
        /// <summary>
        /// パーティション情報ロード時処理
        /// </summary>
        private void PartitionTableLoad()
        {
            _PartitionList[0, 0].Value = "始点マガジン";
            _PartitionList[0, 1].Value = "終点マガジン";
            _PartitionList[0, 2].Value = "細線モード";
            _PartitionList[0, 3].Value = "Z送り速度";
            _PartitionList[0, 4].Value = "CRS";
            _PartitionList[0, 5].Value = "Z送り速度";
            _PartitionList[0, 6].Value = "Z送り量";
            List<string> RetTable = new List<string>();
            using (Models.McIf.McDatAecData PartitionDat = new Models.McIf.McDatAecData())
            using (Models.McIf.McDatInitialPrm initParam = new Models.McIf.McDatInitialPrm())
            {
                //パーティション情報取得
                PartitionDat.Read();
                initParam.Read();
                _enableGuideCount = initParam.GuideCount;
                foreach (Models.McIf.StructurePartitionItem PartitionItem in PartitionDat.Partitions.Items)
                {
                    //ガイド毎のパーティション情報を表示
                    _PartitionList.Rows[0].Cells[PartitionItem.Number].Value = PartitionItem.IndexStart.ToString();
                    _PartitionList.Rows[1].Cells[PartitionItem.Number].Value = PartitionItem.IndexEnd.ToString();
                    _PartitionList.Rows[2].Cells[PartitionItem.Number].Value = (PartitionItem.Thinline == true)? "ON" : "OFF";
                    _PartitionList.Rows[3].Cells[PartitionItem.Number].Value = initParam.ElctdChkSpdZ1[PartitionItem.Number - 1].ToString();
                    _PartitionList.Rows[4].Cells[PartitionItem.Number].Value = initParam.ElctdChkSpdS2[PartitionItem.Number - 1].ToString("00");
                    _PartitionList.Rows[5].Cells[PartitionItem.Number].Value = initParam.ElctdChkSpdZ2[PartitionItem.Number - 1].ToString();
                    _PartitionList.Rows[6].Cells[PartitionItem.Number].Value = initParam.ElctdChkOfsZ[PartitionItem.Number - 1].ToString();
                }
                //未使用ガイドのパーティション設定は空白にする。
                for(int guideCt = 0; guideCt < _PartitionList.Columns.Count; guideCt++)
                {
                    if(_enableGuideCount <= guideCt - 1)
                    {
                        //パーティション番号
                        _PartitionList.Columns[guideCt].HeaderText = null;
                        foreach(DataGridViewRow row in _PartitionList.Rows)
                        {
                            //パーティション設定
                            row.Cells[guideCt].Value = null;
                        }
                    }
                }
                _PartitionEnableBt.SetLed(!PartitionDat.PartitionEnabled);
            }
            for(int guideCt = 1; guideCt <= _enableGuideCount; guideCt++)
            {
                if (CheckPartitionNumberRange(guideCt) == false)
                {
                    _PartitionList[guideCt, 0].Style.BackColor = Color.Red;
                    _PartitionList[guideCt, 1].Style.BackColor = Color.Red;
                }
            }
        }

        private void PartitionTableSave()
        {
            Thread.Sleep(100);
            using (McReqPartitionChange partitionChange = new McReqPartitionChange())
            {
                ResultCodes writeResult = ResultCodes.Success;
                writeResult = partitionChange.Read();
                //RTMC64ECの動作モードを「Setting」にする。
                using (McReqModeChange ReqModeChg = new McReqModeChange())
                {
                    ReqModeChg.TaskMode = McTaskModes.Setting;
                    if (ReqModeChg.Execute() != ResultCodes.Success)
                    {
                        //動作モード変更に失敗したら行わない。
                        return;
                    }
                }
                using (McDatStatus status = new McDatStatus())
                {
                    writeResult = status.Read();
                    //settingモード待機
                    while (status.Status.MotionMode != McTaskModes.Setting)
                    {
                        writeResult = status.Read();
                    }
                }
                using (McDatInitialPrm datini = new McDatInitialPrm())
                {
                    //ﾌﾗｸﾞセット
                    writeResult = datini.Read();

                    foreach (DataGridViewRow row in _PartitionList.Rows)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            int colIndex = cell.ColumnIndex;
                            if (cell.ColumnIndex == 0) continue;
                            if (_enableGuideCount >= colIndex)
                            {
                                int rowIndex = cell.RowIndex;
                                switch (rowIndex)
                                {
                                    case 0: partitionChange.Partitions.Items[colIndex - 1].IndexStart = short.Parse(cell.Value.ToString()); break;
                                    case 1: partitionChange.Partitions.Items[colIndex - 1].IndexEnd = short.Parse(cell.Value.ToString()); break;
                                    case 2: partitionChange.Partitions.Items[colIndex - 1].Thinline = (cell.Value.ToString() == "ON") ? true : false; break;
                                    case 3: datini.ElctdChkSpdZ1[colIndex - 1] = int.Parse(cell.Value.ToString()); break;
                                    case 4: datini.ElctdChkSpdS2[colIndex - 1] = int.Parse(cell.Value.ToString()); break;
                                    case 5: datini.ElctdChkSpdZ2[colIndex - 1] = int.Parse(cell.Value.ToString()); break;
                                    case 6: datini.ElctdChkOfsZ[colIndex - 1] = int.Parse(cell.Value.ToString()); break;
                                }
                            }
                        }
                    }
                    writeResult = datini.Write();
                }
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
                    partitionChange.Execute();
                    partitionChange.Write();
                }
                return;
            }
        }

        private void label51_Click(object sender, EventArgs e)
        {
            if(PartitionEnable == 0)
            {
                PartitionEnable = 1;
            }
            else
            {
                PartitionEnable = 0;
            }
        }

        private void PartitionForm_Shown(object sender, EventArgs e)
        {
            _PartitionList.ClearSelection();
        }

        private void _PartitionList_SelectionChanged(object sender, EventArgs e)
        {
            if(_PartitionList.SelectedCells.Count >= 1)
            {
                _PartitionList.ClearSelection();
            }
        }

        private int _beforeSelectColumnIndex = 1;
        private int _beforeSelectRowIndex = 0;
        /// <summary>
        /// セルマウスアップ時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _PartitionList_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex == -1) return;
            if (e.ColumnIndex > _enableGuideCount) return;

            if(e.RowIndex == 2)
            {
                if(e.ColumnIndex != 0)
                {
                    _PartitionList[e.ColumnIndex, 2].Value
                    = (_PartitionList[e.ColumnIndex, 2].Value.ToString() == "ON") ? "OFF" : "ON";
                    PartitionTableSave();
                }
            }

            if(e.RowIndex == 6)
            {
                _CrearBt.Enabled = true;
                if(_CrearBt.GetLed() == true)
                {
                    _SettingBt.Enabled = true;
                }
            }
            else
            {
                if (_CrearBt.GetLed() != false)
                {
                    _clearAbsPos = 0;
                    _CrearBt.SetLed(false);
                } 
                if (_CrearBt.Enabled != false)_CrearBt.Enabled = false;
                if (_SettingBt.Enabled != false) _SettingBt.Enabled = false;
            }
            if (e.RowIndex < 0)
            {
                foreach (DataGridViewColumn col in _PartitionList.Columns)
                {
                    if (col.Index == e.ColumnIndex)
                    {
                        col.HeaderCell.Style.BackColor = FileUIStyleTable.EnabledBackColor;
                        col.HeaderCell.Style.ForeColor = FileUIStyleTable.EnabledForeColor;
                    }
                    else
                    {
                        col.HeaderCell.Style.BackColor = FileUIStyleTable.DefaultBackColor;
                        col.HeaderCell.Style.ForeColor = FileUIStyleTable.DefaultForeColor;
                    }
                }
                foreach (DataGridViewRow row in _PartitionList.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.ColumnIndex == e.ColumnIndex
                        && cell.RowIndex == _beforeSelectRowIndex)
                        {
                            cell.Style.BackColor = FileUIStyleTable.EnabledBackColor;
                            cell.Style.ForeColor = FileUIStyleTable.EnabledForeColor;
                        }
                        else
                        {
                            if(cell.ColumnIndex != 0)
                            {
                                cell.Style.BackColor = FileUIStyleTable.DefaultBackColor;
                                cell.Style.ForeColor = FileUIStyleTable.DefaultForeColor;
                            }
                        }
                    }
                }
                _beforeSelectColumnIndex = e.ColumnIndex;
                return;
            }
            if(e.ColumnIndex == 0)
            {
                foreach (DataGridViewRow row in _PartitionList.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.ColumnIndex == e.ColumnIndex
                        && cell.RowIndex == e.RowIndex)
                        {
                            cell.Style.BackColor = FileUIStyleTable.EnabledBackColor;
                            cell.Style.ForeColor = FileUIStyleTable.EnabledForeColor;
                        }
                        else
                        {
                            cell.Style.BackColor = FileUIStyleTable.DefaultBackColor;
                            cell.Style.ForeColor = FileUIStyleTable.DefaultForeColor;
                        }
                    }
                }
                _PartitionList[_beforeSelectColumnIndex, e.RowIndex].Style.BackColor = FileUIStyleTable.EnabledBackColor;
                _PartitionList[_beforeSelectColumnIndex, e.RowIndex].Style.ForeColor = FileUIStyleTable.EnabledForeColor;
                _beforeSelectRowIndex = e.RowIndex;
                return;
            }
            foreach(DataGridViewRow row in _PartitionList.Rows)
            {
                foreach(DataGridViewCell cell in row.Cells)
                {
                    if (cell.ColumnIndex == e.ColumnIndex
                    && cell.RowIndex == e.RowIndex)
                    {
                        cell.Style.BackColor = FileUIStyleTable.EnabledBackColor;
                        cell.Style.ForeColor = FileUIStyleTable.EnabledForeColor;
                    }
                    else
                    {
                        cell.Style.BackColor = FileUIStyleTable.DefaultBackColor;
                        cell.Style.ForeColor = FileUIStyleTable.DefaultForeColor;
                    }
                }  
            }
            _PartitionList.Rows[e.RowIndex].Cells[0].Style.BackColor = FileUIStyleTable.EnabledBackColor;
            _PartitionList.Rows[e.RowIndex].Cells[0].Style.ForeColor = FileUIStyleTable.EnabledForeColor;
            foreach(DataGridViewColumn col in _PartitionList.Columns)
            {
                if(col.Index == e.ColumnIndex)
                {
                    col.HeaderCell.Style.BackColor = FileUIStyleTable.EnabledBackColor;
                    col.HeaderCell.Style.ForeColor = FileUIStyleTable.EnabledForeColor;
                }
                else
                {
                    col.HeaderCell.Style.BackColor = FileUIStyleTable.DefaultBackColor;
                    col.HeaderCell.Style.ForeColor = FileUIStyleTable.DefaultForeColor;
                }
            }
            if (e.RowIndex != 2) popupTenkeyOn(((_PartitionSetting)(e.RowIndex)), e.ColumnIndex);
            _beforeSelectColumnIndex = e.ColumnIndex;
            _beforeSelectRowIndex = e.RowIndex;
        }

        private void _CrearBt_MouseUp(object sender, MouseEventArgs e)
        {
            if(_CrearBt.GetLed() == false)
            {
                _CrearBt.SetLed(true);
                if(_SettingBt.Enabled == false)
                {
                    _SettingBt.Enabled = true;
                }
                foreach (DataGridViewCell cell in _PartitionList.Rows[6].Cells)
                {
                    if (cell.ColumnIndex <= 0) continue;
                    if(cell.Style.BackColor == FileUIStyleTable.EnabledBackColor)
                    {
                        cell.Value = 0;
                    }
                }
                PartitionTableSave();
            }
        }

        private void _SettingBt_MouseUp(object sender, MouseEventArgs e)
        {
            _CrearBt.SetLed(false);
            _SettingBt.Enabled = false;
            foreach (DataGridViewCell cell in _PartitionList.Rows[6].Cells)
            {
                if (cell.ColumnIndex <= 0) continue;
                if (cell.Style.BackColor == FileUIStyleTable.EnabledBackColor)
                {
                    if(int.Parse(cell.Value.ToString()) > 0) cell.Value = 0;
                }
            }
            PartitionTableSave();
            _clearAbsPos = 0;
        }

        private void _GuideThroughSequenceBtn_MouseUp(object sender, MouseEventArgs e)
        {
           if(true == _GuideThroughSequenceBtn.GetBack())
            {
                _GuideThroughSequenceBtn.SetBack(false);
            }
           else
            {
                _GuideThroughSequenceBtn.SetBack(true);
            }
        }
        public void StatusMonitoring(StatusMonitorEventArgs e)
        {
            if ((this.IsDisposed == true || this.Disposing == true)) return;
            if (_CrearBt.GetLed() == true)
            {
                if(_clearAbsPos == 0)_clearAbsPos = e.Items.MacAxisPos.AxisZ;
                foreach (DataGridViewCell cell in _PartitionList.Rows[6].Cells)
                {
                    if (cell.ColumnIndex <= 0) continue;
                    if (cell.Style.BackColor == FileUIStyleTable.EnabledBackColor)
                    {
                        using (McDatStatus datSta = new McDatStatus())
                        {
                            datSta.Read();
                            cell.Value = (_clearAbsPos - e.Items.MacAxisPos.AxisZ) * -1;
                        }
                    }
                }
            }

            //スタートボタンが押され、そのオフエッジが取れた時
            if (true == e.Items.StartSwBtnOffEdge
                && _popupTenkey.Visible == false)
            {
                //ガイド貫通動作コマンドボタンが選択されていれば、コマンド発行する。
                if (true == _GuideThroughSequenceBtn.GetBack())
                {
                    _GuideThroughSequence();
                    if (true == _GuideThroughSequenceBtn.GetBack())
                    {
                        _GuideThroughSequenceBtn.SetBack(false);
                    }
                }
            }
        }
        /// <summary>
        /// ガイド貫通動作コマンド実行関数
        /// </summary>
        /// <returns>
        /// Success：成功
        /// それ以外：失敗
        /// </returns>
        private ResultCodes _GuideThroughSequence()
        {
            ResultCodes _retResultCodes = ResultCodes.Success;
            using (Models.McIf.McReqGuideThroughStart ReqGuideThrough = new Models.McIf.McReqGuideThroughStart())
            {
                _retResultCodes = ReqGuideThrough.Execute();
                //ログ処理
                UILog logs = new UILog("PartitionForm._GuideThroughSequenceBtn_MouseUp");
                if (_retResultCodes == ResultCodes.Success)
                {
                    logs.Sure("GUIDE_THROUGH Result = " + _retResultCodes);
                }
                else
                {
                    logs.Error("GUIDE_THROUGH Result = " + _retResultCodes);
                }
            }
            return _retResultCodes;
        }
        /// <summary>
        /// パーティション設定有効無効切り替えボタンのマウスアップ時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _PartitionEnableBt_MouseUp(object sender, MouseEventArgs e)
        {
            _PartitionEnableChenge(_PartitionEnableBt.GetLed());
        }
        /// <summary>
        /// パーティション有効無効切り替え処理
        /// </summary>
        /// <param name="enable">
        /// true：有効
        /// false：無効
        /// </param>
        /// <returns>
        /// 結果
        /// Success：成功
        /// その他：失敗
        /// </returns>
        private ResultCodes _PartitionEnableChenge(bool enable)
        {
            //テクノコマンド処理
            ResultCodes retMessage = ResultCodes.Success;
            using (McReqPartitionChange partitionChange = new McReqPartitionChange())
            {
                retMessage = partitionChange.Read();
                partitionChange.Partitions.Enabled = enable;
                retMessage = partitionChange.Execute();
                retMessage = partitionChange.Write();
            }
            //成功時ボタン切り替え処理
            if(retMessage == ResultCodes.Success)
            {
                _PartitionEnableBt.SetLed(!enable);
            }
            if (enable)
            {//パーティション有効時、DGVEx有効
                _PartitionList.Enabled = true;
            }
            else
            {//パーティション無効時、DGVEx無効
                _PartitionList.Enabled = false;
            }
            return retMessage;
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
        private bool CheckPartitionNumberRange(int partitionNum)
        {
            bool chkValue = true;
            int targetMinValue = int.Parse(_PartitionList[partitionNum, 0].Value.ToString());
            int targetMaxValue = int.Parse(_PartitionList[partitionNum, 1].Value.ToString());
            foreach (DataGridViewColumn col in _PartitionList.Columns)
            {
                if (partitionNum == col.Index || col.Index == 0 || col.Index >= _enableGuideCount) continue;
                int minValue = int.Parse(_PartitionList[col.Index, 0].Value.ToString());
                int maxValue = int.Parse(_PartitionList[col.Index, 1].Value.ToString());
                if (maxValue < targetMinValue || minValue > targetMaxValue)
                { }
                else
                {
                    chkValue = false;
                }
            }
            return chkValue;
        }
        private bool CheckPartitionNumberRange(int partitionNum, int filterVal, int filterRow)
        {
            bool chkValue = true;
            int targetMinValue;
            int targetMaxValue;
            if (filterRow == 0)
            {
                targetMinValue = filterVal;
                targetMaxValue = int.Parse(_PartitionList[partitionNum, 1].Value.ToString());
            }
            else
            {
                targetMinValue = int.Parse(_PartitionList[partitionNum, 0].Value.ToString());
                targetMaxValue = filterVal;
            }
           
            foreach (DataGridViewColumn col in _PartitionList.Columns)
            {
                if (partitionNum == col.Index || col.Index == 0 || col.Index > _enableGuideCount) continue;
                int minValue = int.Parse(_PartitionList[col.Index, 0].Value.ToString());
                int maxValue = int.Parse(_PartitionList[col.Index, 1].Value.ToString());
                if (maxValue < targetMinValue || minValue > targetMaxValue)
                { }
                else
                {
                    chkValue = false;
                }
            }
            return chkValue;
        }
        #region<ポップアップテンキー>
        /// <summary>
        /// 記録する加工条件値
        /// </summary>
        enum _PartitionSetting
        {
            //始点マガジン
            StartPosition = 0,
            //終点マガジン
            EndPosition = 1,
            //センサーまでの加工速度
            ElctdChkSpdZ1 = 3,
            //センサーからの主軸回転速度
            ElctdChkSpdS2 = 4,
            //センサーからの加工速度
            ElctdChkspdZ2 = 5,         
            //センサーからの加工量
            ElctdChkOfsZ = 6
        }
        private _PartitionSetting _SettingTarget;
        private int _GuideNumber = 0;
        /// <summary>
		/// ポップアップテンキー：表示/非表示
		/// </summary>
		/// <param name="val"></param>
		/// <returns>false=上下ポップアップを表示</returns>
		private bool popupTenkeyOn(_PartitionSetting target, int guideNumber)
        {
            _SettingTarget = target;
            _GuideNumber = guideNumber;
            string changeVal = "";	//編集値
            Decimal lowerLimitDec = 0;//最小値
            Decimal upperLimitDec = 0;//最大値

            //フォーマットタイプ
            NumericTextBox.FormatTypes formatType = NumericTextBox.FormatTypes.Integer1;
            if (_popupTenkey != null)
            {
                _popupTenkey.Close();   //画面を閉じる
                _popupTenkey = null;    //null初期化
            }
            //ウインドタイトル
            string titleString = "";
            //最小/最大デフォルト
            lowerLimitDec = (decimal)-99999999;
            upperLimitDec = (decimal)4000000;
            string strTenkeyMode = "";
            //クリックしたコントロールの下段コントロール値を取得
            switch (_SettingTarget)
            {
                case _PartitionSetting.StartPosition:
                    McDatInitialPrm datIniStart = new McDatInitialPrm();
                    datIniStart.Read();
                    titleString = _PartitionList[0, (int)target].Value.ToString() + "  GSF" + guideNumber.ToString();
                    changeVal = _PartitionList[guideNumber, (int)target].Value.ToString();
                    lowerLimitDec = (decimal)0;
                    upperLimitDec = (decimal)datIniStart.ElectrodeCount;
                    strTenkeyMode = "part_Setting";
                    //フォーマット
                    formatType = NumericTextBox.FormatTypes.Integer3;
                    break;

                case _PartitionSetting.EndPosition:
                    McDatInitialPrm datiniEnd = new McDatInitialPrm();
                    datiniEnd.Read();
                    titleString = _PartitionList[0, (int)target].Value.ToString() + "  GSF" + guideNumber.ToString();
                    changeVal = _PartitionList[guideNumber, (int)target].Value.ToString();
                    lowerLimitDec = (decimal)0;
                    upperLimitDec = (decimal)datiniEnd.ElectrodeCount;
                    strTenkeyMode = "part_Setting";
                    //フォーマット
                    formatType = NumericTextBox.FormatTypes.Integer3;
                    break;

                case _PartitionSetting.ElctdChkSpdZ1:
                    titleString = _PartitionList[0, (int)target].Value.ToString() + "  GSF" + guideNumber.ToString();
                    changeVal = _PartitionList[guideNumber, (int)target].Value.ToString();
                    lowerLimitDec = (decimal)1;
                    upperLimitDec = (decimal)4000000;
                    strTenkeyMode = "part_Setting";
                    //フォーマット
                    formatType = NumericTextBox.FormatTypes.Integer7;
                    break;

                case _PartitionSetting.ElctdChkSpdS2:
                    titleString = _PartitionList[0, (int)target].Value.ToString() + "  GSF" + guideNumber.ToString();
                    changeVal = _PartitionList[guideNumber, (int)target].Value.ToString();
                    lowerLimitDec = (decimal)0;
                    upperLimitDec = (decimal)15;
                    strTenkeyMode = "part_Setting";
                    //フォーマット
                    formatType = NumericTextBox.FormatTypes.IntegerZeroPlace2;
                    break;

                case _PartitionSetting.ElctdChkspdZ2:
                    titleString = _PartitionList[0, (int)target].Value.ToString() + "  GSF" + guideNumber.ToString();
                    changeVal = _PartitionList[guideNumber, (int)target].Value.ToString();
                    lowerLimitDec = (decimal)1;
                    upperLimitDec = (decimal)4000000;
                    strTenkeyMode = "part_Setting";
                    //フォーマット
                    formatType = NumericTextBox.FormatTypes.Integer7;
                    break;

                case _PartitionSetting.ElctdChkOfsZ:
                    titleString = _PartitionList[0, (int)target].Value.ToString() + "  GSF" + guideNumber.ToString();
                    changeVal = _PartitionList[guideNumber, (int)target].Value.ToString();
                    lowerLimitDec = (decimal)-99999999;
                    upperLimitDec = (decimal)0;
                    strTenkeyMode = "part_Setting";
                    //フォーマット
                    formatType = NumericTextBox.FormatTypes.Free;
                    break;

            }
            //ポップアップTenKey：2017-1-12:柏原
            _popupTenkey = new TenKeyDialog(
                changeVal,
                formatType,
                lowerLimitDec,
                upperLimitDec,
                true,
                false,
                false,
                strTenkeyMode
                );
            _popupTenkey.NotifyReturnOk = popupTenkey_OnNotifyReturnOk; //イベント通知：OK
            _popupTenkey.Text = titleString;	                        //テンキータイトル表示：改行が有れば空白に変換　※ここは項目名が無い
            _popupTenkey.Show(this);                              //画面を開く
            return true;
        }
        /// <summary>
        /// ポップアップテンキーで「OK」ボタンを押した時、入力値をこのクラスに反映
        /// </summary>
        private void popupTenkey_OnNotifyReturnOk()
        {
            string retVal = _popupTenkey._tenkeyValReturn;//ポップアップテンキーで編集された値
            int retElctNum;
            bool result = int.TryParse(retVal, out retElctNum);
            if(result == true)
            {
                for(int guideCt = 1; guideCt <= _enableGuideCount; guideCt++)
                {
                    result = CheckPartitionNumberRange(guideCt, retElctNum, (int)_SettingTarget);
                }
            }
            //入力値が範囲外のばあい、メッセージを出して処理を抜ける。
            if(result == false)
            {
                using (MessageDialog msgDlg = new MessageDialog())
                {
                    msgDlg.Error(5035, this);
                }
                return;
            }
            _PartitionList[_GuideNumber, (int)_SettingTarget].Value = retVal;
            PartitionTableSave();
        }
        #endregion
    }
}