using ECNC3.Enumeration;
using ECNC3.Models;
using ECNC3.Models.McIf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

namespace ECNC3.Views
{
    public partial class PitchSettingForm : ECNC3Form
    {
        /// <summary>軸名称リスト</summary>
        private readonly string[] _axisNames = new string[] { "X", "Y", "W", "Z", "A", "B", "C", "I", "" };
        /// <summary>選択軸番号</summary>
        private AxisNumbers _selectedAxis;
        /// <summary>
        /// 軸毎の最大補正値データ数
        /// </summary>
        private const int RevDtCount = 30000;

        private class _StructureSelectAdjust
        {
            public int index { get; set; } = 0;
            public string value { get; set; } = "0";
            public _StructureSelectAdjust(int initIndex = 0)
            {
                index = initIndex;
            }
        }
        private List<_StructureSelectAdjust> _selectAdjustListAxisX = new List<_StructureSelectAdjust>();
        private List<_StructureSelectAdjust> _selectAdjustListAxisY = new List<_StructureSelectAdjust>();
        private List<_StructureSelectAdjust> _selectAdjustListAxisW = new List<_StructureSelectAdjust>();
        private List<_StructureSelectAdjust> _selectAdjustListAxisZ = new List<_StructureSelectAdjust>();
        private List<_StructureSelectAdjust> _selectAdjustListAxisA = new List<_StructureSelectAdjust>();
        private List<_StructureSelectAdjust> _selectAdjustListAxisB = new List<_StructureSelectAdjust>();
        private List<_StructureSelectAdjust> _selectAdjustListAxisC = new List<_StructureSelectAdjust>();
        private List<_StructureSelectAdjust> _selectAdjustListAxisI = new List<_StructureSelectAdjust>();
        private List<_StructureSelectAdjust> _selectAdjustListAxisDot = new List<_StructureSelectAdjust>();
        /// <summary>コンストラクタ</summary>
        public PitchSettingForm()
        {
            InitializeComponent();
            _selectedAxis = AxisNumbers.Unknown;
        }
        /// <summary>ロード</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PitchSettingForm_Load(object sender, EventArgs e)
        {
            for(int adjustCt = 0; adjustCt < 2000; adjustCt++)
            {
                _selectAdjustListAxisX.Add(new _StructureSelectAdjust(adjustCt));
                _selectAdjustListAxisY.Add(new _StructureSelectAdjust(adjustCt));
                _selectAdjustListAxisW.Add(new _StructureSelectAdjust(adjustCt));
                _selectAdjustListAxisZ.Add(new _StructureSelectAdjust(adjustCt));
                _selectAdjustListAxisA.Add(new _StructureSelectAdjust(adjustCt));
                _selectAdjustListAxisB.Add(new _StructureSelectAdjust(adjustCt));
                _selectAdjustListAxisC.Add(new _StructureSelectAdjust(adjustCt));
                _selectAdjustListAxisI.Add(new _StructureSelectAdjust(adjustCt));
                _selectAdjustListAxisDot.Add(new _StructureSelectAdjust(adjustCt));
            }

            this.OutLineEnable = true;
            this.OutLineColor = FileUIStyleTable.SelectedLineColor;
            this.OutLineSize = 3;
            //  ピッチエラー補正情報グリッドコントロールの初期設定
            {
                _gridAxis.Initialize(12, 35);
                _gridAxis.BackgroundColor = FileUIStyleTable.DefaultBackColor;
                //行列の数の指定
                _gridAxis.Columns.Add("AxisNo", "");
                _gridAxis.InitCol("AxisNo", typeof(int));
                _gridAxis.Columns.Add("RevMagnify", "補正倍率");
                _gridAxis.InitCol("RevMagnify", typeof(int));
                _gridAxis.Columns.Add("RevSpace", "補正間隔");
                _gridAxis.InitCol("RevSpace", typeof(int));
                _gridAxis.Columns.Add("RevTopNo", "先頭番号");
                _gridAxis.InitCol("RevTopNo", typeof(int));
                _gridAxis.Columns.Add("RevMCnt", "-区間数");
                _gridAxis.InitCol("RevMCnt", typeof(int));
                _gridAxis.Columns.Add("RevPCnt", "+区間数");
                _gridAxis.InitCol("RevPCnt", typeof(int));

                _gridAxis.RowCount = 8;
                _gridAxis.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                _gridAxis.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

                foreach (DataGridViewColumn col in _gridAxis.Columns)
                {
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                    if ("RevTopNo" == col.Name)
                    {
                        col.Visible = false;
                    }
                }
                foreach (DataGridViewRow row in _gridAxis.Rows)
                {
                    row.Cells["AxisNo"].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                int index = 0;
                //  軸名称を設定
                for (; index < _axisNames.Length && false == string.IsNullOrEmpty(_axisNames[index]); ++index)
                {
                    _gridAxis["AxisNo", index].Value = _axisNames[index];
                    _gridAxis["AxisNo", index].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            //  軸ごとの補正値グリッドコントロールの初期設定
            {
               
                int topValue = 0;
                foreach (DataGridViewRow row in _gridAxis.Rows)
                {
                    row.Cells["RevTopNo"].Value = topValue;
                    topValue += 520;
                }
            }
            //補正値データ編集用の全コントロールにクリックイベント処理を設定
            foreach(Control ctrl in _RevDtPanel.Controls)
            {
                if (ctrl.GetType() != typeof(LabelExAndButtonEx)) continue;
                (ctrl as LabelExAndButtonEx).ClickEventSet(_RevDtValues_Click);
            }
            _buttonMcReading_Click(null, null);
            _gridAxis[0, (int)AxisNumbers.X].Style.BackColor = FileUIStyleTable.SelectedLineColor;
            _gridAxis[0, (int)AxisNumbers.X].Style.ForeColor = FileUIStyleTable.EnabledForeColor;
            _selectedAxis = AxisNumbers.X;
        }
        private void BackMakerServiceFormBt_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void PitchSettingForm_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
        /// <summary>選択軸番号取得</summary>
        /// <returns>選択軸番号</returns>
        private AxisNumbers GetSelectedAxis()
        {
            //string axisSelected = _gridAxis["AxisNo", _gridAxis.CurrentCell.RowIndex].Value as string;
            string axisSelected = "";
            foreach(DataGridViewRow row in _gridAxis.Rows)
            {
                if(row.Cells[0].Style.BackColor == FileUIStyleTable.SelectedLineColor)
                {
                    axisSelected = row.Cells[0].Value as string;
                }
            }
            int found = Array.FindIndex(_axisNames, (x) => 0 == string.Compare(x, axisSelected));
            if (0 > found)
            {
                return AxisNumbers.Unknown;
            }
            return (AxisNumbers)found;
        }


        /// <summary>【ファイル読込み】ボタンクリックイベント</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// レニショーファイルの読み込みを行います。
        /// </remarks>
        private void _buttonFileImport_Click(object sender, EventArgs e)
        {
            if (AxisNumbers.Unknown == _selectedAxis)
            {
                MessageBox.Show("更新対象の軸を選択してください。");
                return;
            }
            AxisNumbers axis = _selectedAxis;
            while (AxisNumbers.Unknown != axis)
            {
                string path = string.Empty;
                using (FileForm fd = new FileForm(FileFormMode.OpenRenishaw, string.Empty))
                {
                    fd.ShowDialog();

                    path = fd._returnFullPath;
                    if (true == string.IsNullOrEmpty(path))
                    {
                        break;
                    }
                    if (false == File.Exists(path))
                    {
                        break;
                    }

                    using (FileRenishaw fr = new FileRenishaw())
                    {
                        fr.FilePath = path;
                        fr.AxisNumber = axis;
                        ResultCodes ret = fr.Read();
                        if (ResultCodes.Success != ret)
                        {
                            break;
                        }
                        switch(axis)
                        {
                            case AxisNumbers.X:
                                if (_selectAdjustListAxisX != null) _selectAdjustListAxisX.Clear(); _selectAdjustListAxisX = null;
                                _selectAdjustListAxisX = new List<_StructureSelectAdjust>();
                                for (int pitCount = 0; pitCount < fr.Data.RevDt.Count(); pitCount++)
                                {
                                    _StructureSelectAdjust pitAdjustTemp = new _StructureSelectAdjust();
                                    pitAdjustTemp.index = pitCount;
                                    pitAdjustTemp.value = fr.Data.RevDt[pitCount].ToString();
                                    _selectAdjustListAxisX.Add(pitAdjustTemp);
                                }
                                break;

                            case AxisNumbers.Y:
                                if (_selectAdjustListAxisY != null) _selectAdjustListAxisY.Clear(); _selectAdjustListAxisY = null;
                                _selectAdjustListAxisY = new List<_StructureSelectAdjust>();
                                for (int pitCount = 0; pitCount < fr.Data.RevDt.Count(); pitCount++)
                                {
                                    _StructureSelectAdjust pitAdjustTemp = new _StructureSelectAdjust();
                                    pitAdjustTemp.index = pitCount;
                                    pitAdjustTemp.value = fr.Data.RevDt[pitCount].ToString();
                                    _selectAdjustListAxisY.Add(pitAdjustTemp);
                                }
                                break;

                            case AxisNumbers.W:
                                if (_selectAdjustListAxisW != null) _selectAdjustListAxisW.Clear(); _selectAdjustListAxisW = null;
                                _selectAdjustListAxisW = new List<_StructureSelectAdjust>();
                                for (int pitCount = 0; pitCount < fr.Data.RevDt.Count(); pitCount++)
                                {
                                    _StructureSelectAdjust pitAdjustTemp = new _StructureSelectAdjust();
                                    pitAdjustTemp.index = pitCount;
                                    pitAdjustTemp.value = fr.Data.RevDt[pitCount].ToString();
                                    _selectAdjustListAxisW.Add(pitAdjustTemp);
                                }
                                break;

                            case AxisNumbers.Z:
                                if (_selectAdjustListAxisZ != null) _selectAdjustListAxisZ.Clear(); _selectAdjustListAxisZ = null;
                                _selectAdjustListAxisZ = new List<_StructureSelectAdjust>();
                                for (int pitCount = 0; pitCount < fr.Data.RevDt.Count(); pitCount++)
                                {
                                    _StructureSelectAdjust pitAdjustTemp = new _StructureSelectAdjust();
                                    pitAdjustTemp.index = pitCount;
                                    pitAdjustTemp.value = fr.Data.RevDt[pitCount].ToString();
                                    _selectAdjustListAxisZ.Add(pitAdjustTemp);
                                }
                                break;

                            case AxisNumbers.A:
                                if (_selectAdjustListAxisA != null) _selectAdjustListAxisA.Clear(); _selectAdjustListAxisA = null;
                                _selectAdjustListAxisA = new List<_StructureSelectAdjust>();
                                for (int pitCount = 0; pitCount < fr.Data.RevDt.Count(); pitCount++)
                                {
                                    _StructureSelectAdjust pitAdjustTemp = new _StructureSelectAdjust();
                                    pitAdjustTemp.index = pitCount;
                                    pitAdjustTemp.value = fr.Data.RevDt[pitCount].ToString();
                                    _selectAdjustListAxisA.Add(pitAdjustTemp);
                                }
                                break;

                            case AxisNumbers.B:
                                if (_selectAdjustListAxisB != null) _selectAdjustListAxisB.Clear(); _selectAdjustListAxisB = null;
                                _selectAdjustListAxisB = new List<_StructureSelectAdjust>();
                                for (int pitCount = 0; pitCount < fr.Data.RevDt.Count(); pitCount++)
                                {
                                    _StructureSelectAdjust pitAdjustTemp = new _StructureSelectAdjust();
                                    pitAdjustTemp.index = pitCount;
                                    pitAdjustTemp.value = fr.Data.RevDt[pitCount].ToString();
                                    _selectAdjustListAxisB.Add(pitAdjustTemp);
                                }
                                break;

                            case AxisNumbers.C:
                                if (_selectAdjustListAxisC != null) _selectAdjustListAxisC.Clear(); _selectAdjustListAxisC = null;
                                _selectAdjustListAxisC = new List<_StructureSelectAdjust>();
                                for (int pitCount = 0; pitCount < fr.Data.RevDt.Count(); pitCount++)
                                {
                                    _StructureSelectAdjust pitAdjustTemp = new _StructureSelectAdjust();
                                    pitAdjustTemp.index = pitCount;
                                    pitAdjustTemp.value = fr.Data.RevDt[pitCount].ToString();
                                    _selectAdjustListAxisC.Add(pitAdjustTemp);
                                }
                                break;

                            case AxisNumbers.I:
                                if (_selectAdjustListAxisI != null) _selectAdjustListAxisI.Clear(); _selectAdjustListAxisI = null;
                                _selectAdjustListAxisI = new List<_StructureSelectAdjust>();
                                for (int pitCount = 0; pitCount < fr.Data.RevDt.Count(); pitCount++)
                                {
                                    _StructureSelectAdjust pitAdjustTemp = new _StructureSelectAdjust();
                                    pitAdjustTemp.index = pitCount;
                                    pitAdjustTemp.value = fr.Data.RevDt[pitCount].ToString();
                                    _selectAdjustListAxisI.Add(pitAdjustTemp);
                                }
                                break;

                        }
                        SetData(fr.AxisNumber, 0, fr.Data);
                    }
                }
                break;
            }
        }

        /// <summary>【NC読込み】ボタンクリックイベント</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// NC情報の再読み込みを行います。
        /// </remarks>
        private void _buttonMcReading_Click(object sender, EventArgs e)
        {
            using (McDatPitchAdjust mc = new McDatPitchAdjust())
            {
                using (StructureMcDatPitchAdjustItem data = new StructureMcDatPitchAdjustItem( AxisNumbers.X))
                {
                    ResultCodes ret = mc.ReadData(data);
                    if (_selectAdjustListAxisX != null) _selectAdjustListAxisX.Clear(); _selectAdjustListAxisX = null;
                    _selectAdjustListAxisX = new List<_StructureSelectAdjust>();
                    for (int pitCount = 0; pitCount < data.RevDt.Count(); pitCount++)
                    {
                        _StructureSelectAdjust pitAdjustTemp = new _StructureSelectAdjust();
                        pitAdjustTemp.index = pitCount;
                        pitAdjustTemp.value = data.RevDt[pitCount].ToString();
                        _selectAdjustListAxisX.Add(pitAdjustTemp);
                    }
                    SetData(AxisNumbers.X, 0, data, false);
                }
                using (StructureMcDatPitchAdjustItem data = new StructureMcDatPitchAdjustItem(AxisNumbers.Y))
                {
                    ResultCodes ret = mc.ReadData(data);
                    if (_selectAdjustListAxisY != null) _selectAdjustListAxisY.Clear(); _selectAdjustListAxisY = null;
                    _selectAdjustListAxisY = new List<_StructureSelectAdjust>();
                    for (int pitCount = 0; pitCount < data.RevDt.Count(); pitCount++)
                    {
                        _StructureSelectAdjust pitAdjustTemp = new _StructureSelectAdjust();
                        pitAdjustTemp.index = pitCount;
                        pitAdjustTemp.value = data.RevDt[pitCount].ToString();
                        _selectAdjustListAxisY.Add(pitAdjustTemp);
                    }
                    SetData(AxisNumbers.Y, 0, data, true);
                }
                using (StructureMcDatPitchAdjustItem data = new StructureMcDatPitchAdjustItem(AxisNumbers.W))
                {
                    ResultCodes ret = mc.ReadData(data);
                    if (_selectAdjustListAxisW != null) _selectAdjustListAxisW.Clear(); _selectAdjustListAxisW = null;
                    _selectAdjustListAxisW = new List<_StructureSelectAdjust>();
                    for (int pitCount = 0; pitCount < data.RevDt.Count(); pitCount++)
                    {
                        _StructureSelectAdjust pitAdjustTemp = new _StructureSelectAdjust();
                        pitAdjustTemp.index = pitCount;
                        pitAdjustTemp.value = data.RevDt[pitCount].ToString();
                        _selectAdjustListAxisW.Add(pitAdjustTemp);
                    }
                    SetData(AxisNumbers.W, 0, data, true);
                }
                using (StructureMcDatPitchAdjustItem data = new StructureMcDatPitchAdjustItem(AxisNumbers.Z))
                {
                    ResultCodes ret = mc.ReadData(data);
                    if (_selectAdjustListAxisZ != null) _selectAdjustListAxisZ.Clear(); _selectAdjustListAxisZ = null;
                    _selectAdjustListAxisZ = new List<_StructureSelectAdjust>();
                    for (int pitCount = 0; pitCount < data.RevDt.Count(); pitCount++)
                    {
                        _StructureSelectAdjust pitAdjustTemp = new _StructureSelectAdjust();
                        pitAdjustTemp.index = pitCount;
                        pitAdjustTemp.value = data.RevDt[pitCount].ToString();
                        _selectAdjustListAxisZ.Add(pitAdjustTemp);
                    }
                    SetData(AxisNumbers.Z, 0, data, true);
                }
                using (StructureMcDatPitchAdjustItem data = new StructureMcDatPitchAdjustItem(AxisNumbers.A))
                {
                    ResultCodes ret = mc.ReadData(data);
                    if (_selectAdjustListAxisA != null) _selectAdjustListAxisA.Clear(); _selectAdjustListAxisA = null;
                    _selectAdjustListAxisA = new List<_StructureSelectAdjust>();
                    for (int pitCount = 0; pitCount < data.RevDt.Count(); pitCount++)
                    {
                        _StructureSelectAdjust pitAdjustTemp = new _StructureSelectAdjust();
                        pitAdjustTemp.index = pitCount;
                        pitAdjustTemp.value = data.RevDt[pitCount].ToString();
                        _selectAdjustListAxisA.Add(pitAdjustTemp);
                    }
                    SetData(AxisNumbers.A, 0, data, true);
                }
                using (StructureMcDatPitchAdjustItem data = new StructureMcDatPitchAdjustItem(AxisNumbers.B))
                {
                    ResultCodes ret = mc.ReadData(data);
                    if (_selectAdjustListAxisB != null) _selectAdjustListAxisB.Clear(); _selectAdjustListAxisB = null;
                    _selectAdjustListAxisB = new List<_StructureSelectAdjust>();
                    for (int pitCount = 0; pitCount < data.RevDt.Count(); pitCount++)
                    {
                        _StructureSelectAdjust pitAdjustTemp = new _StructureSelectAdjust();
                        pitAdjustTemp.index = pitCount;
                        pitAdjustTemp.value = data.RevDt[pitCount].ToString();
                        _selectAdjustListAxisB.Add(pitAdjustTemp);
                    }
                    SetData(AxisNumbers.B, 0, data, true);
                }
                using (StructureMcDatPitchAdjustItem data = new StructureMcDatPitchAdjustItem(AxisNumbers.C))
                {
                    ResultCodes ret = mc.ReadData(data);
                    if (_selectAdjustListAxisC != null) _selectAdjustListAxisC.Clear(); _selectAdjustListAxisC = null;
                    _selectAdjustListAxisC = new List<_StructureSelectAdjust>();
                    for (int pitCount = 0; pitCount < data.RevDt.Count(); pitCount++)
                    {
                        _StructureSelectAdjust pitAdjustTemp = new _StructureSelectAdjust();
                        pitAdjustTemp.index = pitCount;
                        pitAdjustTemp.value = data.RevDt[pitCount].ToString();
                        _selectAdjustListAxisC.Add(pitAdjustTemp);
                    }
                    SetData(AxisNumbers.C, 0, data, true);
                }
                using (StructureMcDatPitchAdjustItem data = new StructureMcDatPitchAdjustItem(AxisNumbers.I))
                {
                    ResultCodes ret = mc.ReadData(data);
                    if (_selectAdjustListAxisI != null) _selectAdjustListAxisI.Clear(); _selectAdjustListAxisI = null;
                    _selectAdjustListAxisI = new List<_StructureSelectAdjust>();
                    for (int pitCount = 0; pitCount < data.RevDt.Count(); pitCount++)
                    {
                        _StructureSelectAdjust pitAdjustTemp = new _StructureSelectAdjust();
                        pitAdjustTemp.index = pitCount;
                        pitAdjustTemp.value = data.RevDt[pitCount].ToString();
                        _selectAdjustListAxisI.Add(pitAdjustTemp);
                    }
                    SetData(AxisNumbers.I, 0, data, true);
                }
            }
        }

        /// <summary>【NC書込み】ボタンクリックイベント</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// NC情報の再読み込みを行います。
        /// </remarks>
        private void _buttonMcWriting_Click(object sender, EventArgs e)
        {
            if (AxisNumbers.Unknown == _selectedAxis)
            {
                MessageBox.Show("更新対象の軸を選択してください。");
                return;
            }
            //未入力項目チェック
            if (_CheckValues() != ResultCodes.Success) return;
            MessageBox.Show(_selectedAxis.ToString() + "軸を書き込みます。");

            using (StructureMcDatPitchAdjustItem target = new StructureMcDatPitchAdjustItem(_selectedAxis))
            {
                GetData(target);
                if (null != target)
                {
                    using (McDatInitialPrm datini = new McDatInitialPrm())
                    {
                        ECNC3.Enumeration.ResultCodes writeResult = Enumeration.ResultCodes.Success;
                        using (McDatPitchAdjust mc = new McDatPitchAdjust())
                        {
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
                            using (McReqPitcherrClr pitClear = new McReqPitcherrClr())
                            {
                                writeResult = pitClear.Execute();
                            }
                            //選択軸の書き込み
                            writeResult = mc.WriteData(target);
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
                        }
                        return;
                    }
                }
            }
        }
        /// <summary>
        /// モード切替のリトライ処理
        /// </summary>
        /// <param name="_retryCt"></param>
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
        /// <summary>補正データ表示</summary>
        /// <param name="axis">軸番号</param>
        /// <param name="target">補正情報</param>
        /// <param name="excludedAdjustValue">補正値表示を除外する</param>
        void SetData(AxisNumbers axis, int startRevDtPage, StructureMcDatPitchAdjustItem target, bool excludedAdjustValue = false)
        {
            int row = (int)axis;
            _gridAxis["RevMagnify", row].Value = target.RevMagnify;
            _gridAxis["RevSpace", row].Value = target.RevSpace;
            _gridAxis["RevTopNo", row].Value = target.RevTopNo;
            _gridAxis["RevMCnt", row].Value = target.RevMCnt;
            _gridAxis["RevPCnt", row].Value = target.RevPCnt;
            if (false == excludedAdjustValue)
            {
                switch(axis)
                {
                    case AxisNumbers.X: _SetAllRevDt(target, startRevDtPage); break;
                    case AxisNumbers.Y: _SetAllRevDt(target, startRevDtPage); break;
                    case AxisNumbers.W: _SetAllRevDt(target, startRevDtPage); break;
                    case AxisNumbers.Z: _SetAllRevDt(target, startRevDtPage); break;
                    case AxisNumbers.A: _SetAllRevDt(target, startRevDtPage); break;
                    case AxisNumbers.B: _SetAllRevDt(target, startRevDtPage); break;
                    case AxisNumbers.C: _SetAllRevDt(target, startRevDtPage); break;
                    case AxisNumbers.I: _SetAllRevDt(target, startRevDtPage); break;
                }
            }
        }

        private void _SetAllRevDt(StructureMcDatPitchAdjustItem target, int startRevDtPage)
        {
            //対象データが無ければ編集欄を無効にする。あれば有効にする。
            _RevDtEditAllEnable(!(target == null));
            _RevDt1.Title = ("No." + (startRevDtPage).ToString());
            _RevDt1.Value = (target.RevDt[startRevDtPage].ToString());
            startRevDtPage++;
            _RevDt2.Title = ("No." + (startRevDtPage).ToString());
            _RevDt2.Value = (target.RevDt[startRevDtPage].ToString());
            startRevDtPage++;
            _RevDt3.Title = ("No." + (startRevDtPage).ToString());
            _RevDt3.Value = (target.RevDt[startRevDtPage].ToString());
            startRevDtPage++;
            _RevDt4.Title = ("No." + (startRevDtPage).ToString());
            _RevDt4.Value = (target.RevDt[startRevDtPage].ToString());
            startRevDtPage++;
            _RevDt5.Title = ("No." + (startRevDtPage).ToString());
            _RevDt5.Value = (target.RevDt[startRevDtPage].ToString());
            startRevDtPage++;
            _RevDt6.Title = ("No." + (startRevDtPage).ToString());
            _RevDt6.Value = (target.RevDt[startRevDtPage].ToString());
            startRevDtPage++;
            _RevDt7.Title = ("No." + (startRevDtPage).ToString());
            _RevDt7.Value = (target.RevDt[startRevDtPage].ToString());
            startRevDtPage++;
            _RevDt8.Title = ("No." + (startRevDtPage).ToString());
            _RevDt8.Value = (target.RevDt[startRevDtPage].ToString());
            startRevDtPage++;
            _RevDt9.Title = ("No." + (startRevDtPage).ToString());
            _RevDt9.Value = (target.RevDt[startRevDtPage].ToString());
            startRevDtPage++;
            _RevDt10.Title = ("No." + (startRevDtPage).ToString());
            _RevDt10.Value = (target.RevDt[startRevDtPage].ToString());
            startRevDtPage++;
            _RevDt11.Title = ("No." + (startRevDtPage).ToString());
            _RevDt11.Value = (target.RevDt[startRevDtPage].ToString());
            startRevDtPage++;
            _RevDt12.Title = ("No." + (startRevDtPage).ToString());
            _RevDt12.Value = (target.RevDt[startRevDtPage].ToString());
            startRevDtPage++;
            _RevDt13.Title = ("No." + (startRevDtPage).ToString());
            _RevDt13.Value = (target.RevDt[startRevDtPage].ToString());
            startRevDtPage++;
            _RevDt14.Title = ("No." + (startRevDtPage).ToString());
            _RevDt14.Value = (target.RevDt[startRevDtPage].ToString());
            startRevDtPage++;
            _RevDt15.Title = ("No." + (startRevDtPage).ToString());
            _RevDt15.Value = (target.RevDt[startRevDtPage].ToString());
            startRevDtPage++;
            _RevDt16.Title = ("No." + (startRevDtPage).ToString());
            _RevDt16.Value = (target.RevDt[startRevDtPage].ToString());
            startRevDtPage++;
            _RevDt17.Title = ("No." + (startRevDtPage).ToString());
            _RevDt17.Value = (target.RevDt[startRevDtPage].ToString());
            startRevDtPage++;
            _RevDt18.Title = ("No." + (startRevDtPage).ToString());
            _RevDt18.Value = (target.RevDt[startRevDtPage].ToString());
            startRevDtPage++;
            _RevDt19.Title = ("No." + (startRevDtPage).ToString());
            _RevDt19.Value = (target.RevDt[startRevDtPage].ToString());
            startRevDtPage++;
            _RevDt20.Title = ("No." + (startRevDtPage).ToString());
            _RevDt20.Value = (target.RevDt[startRevDtPage].ToString());
            startRevDtPage++;
            _RevDt21.Title = ("No." + (startRevDtPage).ToString());
            _RevDt21.Value = (target.RevDt[startRevDtPage].ToString());
            startRevDtPage++;
            _RevDt22.Title = ("No." + (startRevDtPage).ToString());
            _RevDt22.Value = (target.RevDt[startRevDtPage].ToString());
            startRevDtPage++;
            _RevDt23.Title = ("No." + (startRevDtPage).ToString());
            _RevDt23.Value = (target.RevDt[startRevDtPage].ToString());
            startRevDtPage++;
            _RevDt24.Title = ("No." + (startRevDtPage).ToString());
            _RevDt24.Value = (target.RevDt[startRevDtPage].ToString());
            startRevDtPage++;
            _RevDt25.Title = ("No." + (startRevDtPage).ToString());
            _RevDt25.Value = (target.RevDt[startRevDtPage].ToString());
            startRevDtPage++;
            _RevDt26.Title = ("No." + (startRevDtPage).ToString());
            _RevDt26.Value = (target.RevDt[startRevDtPage].ToString());
            startRevDtPage++;
            _RevDt27.Title = ("No." + (startRevDtPage).ToString());
            _RevDt27.Value = (target.RevDt[startRevDtPage].ToString());
            startRevDtPage++;
            _RevDt28.Title = ("No." + (startRevDtPage).ToString());
            _RevDt28.Value = (target.RevDt[startRevDtPage].ToString());
            startRevDtPage++;
            _RevDt29.Title = ("No." + (startRevDtPage).ToString());
            _RevDt29.Value = (target.RevDt[startRevDtPage].ToString());
            startRevDtPage++;
            _RevDt30.Title = ("No." + (startRevDtPage).ToString());
            _RevDt30.Value = (target.RevDt[startRevDtPage].ToString());
            startRevDtPage++;
            _RevDt31.Title = ("No." + (startRevDtPage).ToString());
            _RevDt31.Value = (target.RevDt[startRevDtPage].ToString());
            startRevDtPage++;
            _RevDt32.Title = ("No." + (startRevDtPage).ToString());
            _RevDt32.Value = (target.RevDt[startRevDtPage].ToString());
        }
        private void _SetAllRevDt(List<_StructureSelectAdjust> target, int startRevDtPage)
        {
            //対象データが無ければ編集欄を無効にする。あれば有効にする。
            _RevDtEditAllEnable(!(target == null));
            _RevDt1.Title = ("No." + (startRevDtPage).ToString());
            _RevDt1.Value = (target[startRevDtPage].value);
            startRevDtPage++;
            _RevDt2.Title = ("No." + (startRevDtPage).ToString());
            _RevDt2.Value = (target[startRevDtPage].value);
            startRevDtPage++;
            _RevDt3.Title = ("No." + (startRevDtPage).ToString());
            _RevDt3.Value = (target[startRevDtPage].value);
            startRevDtPage++;
            _RevDt4.Title = ("No." + (startRevDtPage).ToString());
            _RevDt4.Value = (target[startRevDtPage].value);
            startRevDtPage++;
            _RevDt5.Title = ("No." + (startRevDtPage).ToString());
            _RevDt5.Value = (target[startRevDtPage].value);
            startRevDtPage++;
            _RevDt6.Title = ("No." + (startRevDtPage).ToString());
            _RevDt6.Value = (target[startRevDtPage].value);
            startRevDtPage++;
            _RevDt7.Title = ("No." + (startRevDtPage).ToString());
            _RevDt7.Value = (target[startRevDtPage].value);
            startRevDtPage++;
            _RevDt8.Title = ("No." + (startRevDtPage).ToString());
            _RevDt8.Value = (target[startRevDtPage].value);
            startRevDtPage++;
            _RevDt9.Title = ("No." + (startRevDtPage).ToString());
            _RevDt9.Value = (target[startRevDtPage].value);
            startRevDtPage++;
            _RevDt10.Title = ("No." + (startRevDtPage).ToString());
            _RevDt10.Value = (target[startRevDtPage].value);
            startRevDtPage++;
            _RevDt11.Title = ("No." + (startRevDtPage).ToString());
            _RevDt11.Value = (target[startRevDtPage].value);
            startRevDtPage++;
            _RevDt12.Title = ("No." + (startRevDtPage).ToString());
            _RevDt12.Value = (target[startRevDtPage].value);
            startRevDtPage++;
            _RevDt13.Title = ("No." + (startRevDtPage).ToString());
            _RevDt13.Value = (target[startRevDtPage].value);
            startRevDtPage++;
            _RevDt14.Title = ("No." + (startRevDtPage).ToString());
            _RevDt14.Value = (target[startRevDtPage].value);
            startRevDtPage++;
            _RevDt15.Title = ("No." + (startRevDtPage).ToString());
            _RevDt15.Value = (target[startRevDtPage].value);
            startRevDtPage++;
            _RevDt16.Title = ("No." + (startRevDtPage).ToString());
            _RevDt16.Value = (target[startRevDtPage].value);
            startRevDtPage++;
            _RevDt17.Title = ("No." + (startRevDtPage).ToString());
            _RevDt17.Value = (target[startRevDtPage].value);
            startRevDtPage++;
            _RevDt18.Title = ("No." + (startRevDtPage).ToString());
            _RevDt18.Value = (target[startRevDtPage].value);
            startRevDtPage++;
            _RevDt19.Title = ("No." + (startRevDtPage).ToString());
            _RevDt19.Value = (target[startRevDtPage].value);
            startRevDtPage++;
            _RevDt20.Title = ("No." + (startRevDtPage).ToString());
            _RevDt20.Value = (target[startRevDtPage].value);
            startRevDtPage++;
            _RevDt21.Title = ("No." + (startRevDtPage).ToString());
            _RevDt21.Value = (target[startRevDtPage].value);
            startRevDtPage++;
            _RevDt22.Title = ("No." + (startRevDtPage).ToString());
            _RevDt22.Value = (target[startRevDtPage].value);
            startRevDtPage++;
            _RevDt23.Title = ("No." + (startRevDtPage).ToString());
            _RevDt23.Value = (target[startRevDtPage].value);
            startRevDtPage++;
            _RevDt24.Title = ("No." + (startRevDtPage).ToString());
            _RevDt24.Value = (target[startRevDtPage].value);
            startRevDtPage++;
            _RevDt25.Title = ("No." + (startRevDtPage).ToString());
            _RevDt25.Value = (target[startRevDtPage].value);
            startRevDtPage++;
            _RevDt26.Title = ("No." + (startRevDtPage).ToString());
            _RevDt26.Value = (target[startRevDtPage].value);
            startRevDtPage++;
            _RevDt27.Title = ("No." + (startRevDtPage).ToString());
            _RevDt27.Value = (target[startRevDtPage].value);
            startRevDtPage++;
            _RevDt28.Title = ("No." + (startRevDtPage).ToString());
            _RevDt28.Value = (target[startRevDtPage].value);
            startRevDtPage++;
            _RevDt29.Title = ("No." + (startRevDtPage).ToString());
            _RevDt29.Value = (target[startRevDtPage].value);
            startRevDtPage++;
            _RevDt30.Title = ("No." + (startRevDtPage).ToString());
            _RevDt30.Value = (target[startRevDtPage].value);
            startRevDtPage++;
            _RevDt31.Title = ("No." + (startRevDtPage).ToString());
            _RevDt31.Value = (target[startRevDtPage].value);
            startRevDtPage++;
            _RevDt32.Title = ("No." + (startRevDtPage).ToString());
            _RevDt32.Value = (target[startRevDtPage].value);
        }
        private void _SaveAllRevDt(List<_StructureSelectAdjust> target)
        {
            int startRevDtPage = int.Parse(_RevDt1.Title.Replace("No.", ""));
            target[startRevDtPage].value = _RevDt1.Value  ;
            startRevDtPage++;
            target[startRevDtPage].value = _RevDt2.Value  ;
            startRevDtPage++;
            target[startRevDtPage].value = _RevDt3.Value  ;
            startRevDtPage++;
            target[startRevDtPage].value = _RevDt4.Value  ;
            startRevDtPage++;
            target[startRevDtPage].value = _RevDt5.Value  ;
            startRevDtPage++;
            target[startRevDtPage].value = _RevDt6.Value  ;
            startRevDtPage++;
            target[startRevDtPage].value = _RevDt7.Value  ;
            startRevDtPage++;
            target[startRevDtPage].value = _RevDt8.Value  ;
            startRevDtPage++;
            target[startRevDtPage].value = _RevDt9.Value  ;
            startRevDtPage++;
            target[startRevDtPage].value = _RevDt10.Value  ;
            startRevDtPage++;
            target[startRevDtPage].value = _RevDt11.Value  ;
            startRevDtPage++;
            target[startRevDtPage].value = _RevDt12.Value  ;
            startRevDtPage++;
            target[startRevDtPage].value = _RevDt13.Value  ;
            startRevDtPage++;
            target[startRevDtPage].value = _RevDt14.Value  ;
            startRevDtPage++;
            target[startRevDtPage].value = _RevDt15.Value  ;
            startRevDtPage++;
            target[startRevDtPage].value = _RevDt16.Value  ;
            startRevDtPage++;
            target[startRevDtPage].value = _RevDt17.Value  ;
            startRevDtPage++;
            target[startRevDtPage].value = _RevDt18.Value  ;
            startRevDtPage++;
            target[startRevDtPage].value = _RevDt19.Value  ;
            startRevDtPage++;
            target[startRevDtPage].value = _RevDt20.Value  ;
            startRevDtPage++;
            target[startRevDtPage].value = _RevDt21.Value  ;
            startRevDtPage++;
            target[startRevDtPage].value = _RevDt22.Value  ;
            startRevDtPage++;
            target[startRevDtPage].value = _RevDt23.Value  ;
            startRevDtPage++;
            target[startRevDtPage].value = _RevDt24.Value  ;
            startRevDtPage++;
            target[startRevDtPage].value = _RevDt25.Value  ;
            startRevDtPage++;
            target[startRevDtPage].value = _RevDt26.Value  ;
            startRevDtPage++;
            target[startRevDtPage].value = _RevDt27.Value  ;
            startRevDtPage++;
            target[startRevDtPage].value = _RevDt28.Value  ;
            startRevDtPage++;
            target[startRevDtPage].value = _RevDt29.Value  ;
            startRevDtPage++;
            target[startRevDtPage].value = _RevDt30.Value  ;
            startRevDtPage++;
            target[startRevDtPage].value = _RevDt31.Value  ;
            startRevDtPage++;
            target[startRevDtPage].value = _RevDt32.Value  ;
        }

        private void _RevDtEditAllEnable(bool enable)
        {
            foreach(Control ctrl in _RevDtPanel.Controls)
            {
                ctrl.Enabled = enable;
            }
        }
        /// <summary>ピッチエラー補正情報取得</summary>
        /// <param name="target"></param>
        /// <returns></returns>
        void GetData(StructureMcDatPitchAdjustItem target)
        {
            int row = (int)target.AxisNumber;
            target.RevMagnify = (int)_gridAxis["RevMagnify", row].Value;
            target.RevSpace = (int)_gridAxis["RevSpace", row].Value;
            target.RevTopNo = (int)_gridAxis["RevTopNo", row].Value;
            target.RevMCnt = (int)_gridAxis["RevMCnt", row].Value;
            target.RevPCnt = (int)_gridAxis["RevPCnt", row].Value;
            switch (target.AxisNumber)
            {
                case AxisNumbers.X:
                    for(int ctX = 0; ctX < _selectAdjustListAxisX.Count; ctX++)
                    {
                        target.RevDt[ctX] = short.Parse(_selectAdjustListAxisX[ctX].value);
                    }
                    break;

                case AxisNumbers.Y:
                    for (int ctY = 0; ctY < _selectAdjustListAxisY.Count; ctY++)
                    {
                        target.RevDt[ctY] = short.Parse(_selectAdjustListAxisY[ctY].value);
                    }
                    break;

                case AxisNumbers.W:
                    for (int ctW = 0; ctW < _selectAdjustListAxisW.Count; ctW++)
                    {
                        target.RevDt[ctW] = short.Parse(_selectAdjustListAxisW[ctW].value);
                    }
                    break;

                case AxisNumbers.Z:
                    for (int ctZ = 0; ctZ < _selectAdjustListAxisZ.Count; ctZ++)
                    {
                        target.RevDt[ctZ] = short.Parse(_selectAdjustListAxisZ[ctZ].value);
                    }
                    break;

                case AxisNumbers.A:
                    for (int ctA = 0; ctA < _selectAdjustListAxisA.Count; ctA++)
                    {
                        target.RevDt[ctA] = short.Parse(_selectAdjustListAxisA[ctA].value);
                    }
                    break;

                case AxisNumbers.B:
                    for (int ctB = 0; ctB < _selectAdjustListAxisB.Count; ctB++)
                    {
                        target.RevDt[ctB] = short.Parse(_selectAdjustListAxisB[ctB].value);
                    }
                    break;

                case AxisNumbers.C:
                    for (int ctC = 0; ctC < _selectAdjustListAxisC.Count; ctC++)
                    {
                        target.RevDt[ctC] = short.Parse(_selectAdjustListAxisC[ctC].value);
                    }
                    break;

                case AxisNumbers.I:
                    for (int ctI = 0; ctI < _selectAdjustListAxisI.Count; ctI++)
                    {
                        target.RevDt[ctI] = short.Parse(_selectAdjustListAxisI[ctI].value);
                    }
                    break;

            }
        }
        /// <summary>
        /// 未入力項目チェック
        /// </summary>
        /// <returns>
        /// Success : 成功
        /// その他：失敗
        /// </returns>
        ResultCodes _CheckValues()
        {
            foreach (DataGridViewCell cell in _gridAxis.Rows[(int)_selectedAxis].Cells)
            {
                if (_gridAxis[cell.ColumnIndex, cell.RowIndex].Value == null)
                {
                    MessageBox.Show("未入力項目が存在するため、書き込みできません。");
                    return ResultCodes.FailToWriteFile;
                }
            }
            return ResultCodes.Success;
        }
        #region<ポップアップテンキー>
        /// <summary>
        /// ピッチエラー補正パラメータのカテゴリ
        /// </summary>
        private enum _gridAxisColumnName
        {
            /// <summary>
            /// 補正倍率
            /// </summary>
            RevMagnify = 1,
            /// <summary>
            /// 補正間隔
            /// </summary>
            RevSpace = 2,
            /// <summary>
            /// 先頭番号
            /// </summary>
            RevTopNo = 3,
            /// <summary>
            /// -区間数
            /// </summary>
            RevMCnt = 4,
            /// <summary>
            /// +区間数
            /// </summary>
            RevPCnt = 5
        }
        /// <summary>
        /// ピッチエラー補正値のカテゴリ
        /// </summary>
        private enum _gridAdjustColumnName
        {
            /// <summary>
            /// 補正値
            /// </summary>
            AdjVal = 0,
        }
        /// <summary>ポップアップテンキー</summary>
        private TenKeyDialog _popupTenkey = null;
        /// <summary>
        /// 記録するコントロール名
        /// </summary>
        object _controlName = "";
        /// <summary>
        /// ポップアップテンキー：表示/非表示
        /// </summary>
        /// <param name="objControlName">コントロール名</param>
        /// <param name="rowIndex">コントロール名</param>
        /// <param name="colIndex">コントロール名</param>
        private void popupTenkeyOn(object objControlName, int rowIndex, int colIndex)
        {
            if (_popupTenkey != null)
            {
                _popupTenkey.Close();           //画面を閉じる
                _popupTenkey.Dispose();         //破棄
                _popupTenkey = null;            //null初期化
            }
            //初期値設定
            _controlName = objControlName;      //記録するコントロール名
            string titleString = "";            //ウインドタイトルデフォルト
            string changeVal = "";              //編集値デフォルト
            Decimal lowerLimitDec = 0;          //最小デフォルト
            Decimal upperLimitDec = 0;          //最大デフォルト
            string strTenkeyMode = "";          //UITenkeyModeAndPosRec.xmlより表示位置、表示モードを取得
            NumericTextBox.FormatTypes formatType = NumericTextBox.FormatTypes.Integer1;//フォーマットタイプ
            bool firstValueSelect = true;       //初回文字列選択= true
            bool realValueEdit = true;          //実数編集		= true
            bool oneKetaEditImpossible = true;  //下1桁編集不可	= true
            string controlName = ((Control)(objControlName)).Name;                        //初期値設定を変更します

            switch (controlName)
            {
                case "_gridAxis":
                    _gridAxis_setCodeForControl(
                              objControlName,                 //コントロール名
                              rowIndex,
                              colIndex,
                              ref titleString,                //ウインドタイトル
                              ref changeVal,                  //編集文字列
                              ref lowerLimitDec,              //最小値
                              ref upperLimitDec,              //最大値
                              ref strTenkeyMode,              //UITenkeyModeAndPosRec.xmlより表示位置、表示モードを取得
                              ref formatType,                 //フォーマットタイプ
                              ref firstValueSelect,           //初回文字列選択	= true
                              ref realValueEdit,              //実数編集			= true
                              ref oneKetaEditImpossible       //下1桁編集不可		= true
                              );
                    //if (changeVal == "") return;
                    //ポップアップTenKey：
                    _popupTenkey = new TenKeyDialog(
                        changeVal,                      //このコントロールで表示する文字
                        formatType,                     //NumericTextBoxの編集フォーマットタイプ
                        lowerLimitDec,                  //最小値
                        upperLimitDec,                  //最大値
                        firstValueSelect,               //true=初回文字列選択
                        realValueEdit,                  //true=実数編集
                        oneKetaEditImpossible,          //true=下1桁編集不可
                        strTenkeyMode                   //テンキー表示モード：UITenkeyModeAndPosRec.xmlに記述された表示位置やモードで表示
                    );
                    _popupTenkey.NotifyReturnOk = popupTenkey_To_gridAxis_OnNotifyReturnOk; //イベント通知：OK
                    break;

                case "_gridAdjust":
                    _gridAdjust_setCodeForControl(
                            objControlName,                 //コントロール名
                            ref titleString,                //ウインドタイトル
                            ref changeVal,                  //編集文字列
                            ref lowerLimitDec,              //最小値
                            ref upperLimitDec,              //最大値
                            ref strTenkeyMode,              //UITenkeyModeAndPosRec.xmlより表示位置、表示モードを取得
                            ref formatType,                 //フォーマットタイプ
                            ref firstValueSelect,           //初回文字列選択	= true
                            ref realValueEdit,              //実数編集			= true
                            ref oneKetaEditImpossible       //下1桁編集不可		= true
                            );
                    if (changeVal == "") changeVal = "0";
                    //ポップアップTenKey：
                    _popupTenkey = new TenKeyDialog(
                        changeVal,                      //このコントロールで表示する文字
                        formatType,                     //NumericTextBoxの編集フォーマットタイプ
                        lowerLimitDec,                  //最小値
                        upperLimitDec,                  //最大値
                        firstValueSelect,               //true=初回文字列選択
                        realValueEdit,                  //true=実数編集
                        oneKetaEditImpossible,          //true=下1桁編集不可
                        strTenkeyMode                   //テンキー表示モード：UITenkeyModeAndPosRec.xmlに記述された表示位置やモードで表示
                    );
                    _popupTenkey.NotifyReturnOk = popupTenkey_To_Adjust_OnNotifyReturnOk; //イベント通知：OK
                    break;

            }
            _popupTenkey.Text = titleString;                            //テンキータイトル表示
            _popupTenkey.ShowDialog(this);                            //画面を開く:
            _popupTenkey.Dispose();                                     //破棄
            _popupTenkey = null;                                        //null初期化
        }
        /// <summary>
        /// ポップアップテンキー：表示/非表示
        /// </summary>
        /// <param name="objControlName">コントロール名</param>
        /// <param name="rowIndex">コントロール名</param>
        /// <param name="colIndex">コントロール名</param>
        private void popupTenkeyOn(object objControlName)
        {
            if (_popupTenkey != null)
            {
                _popupTenkey.Close();           //画面を閉じる
                _popupTenkey.Dispose();         //破棄
                _popupTenkey = null;            //null初期化
            }
            //初期値設定
            _controlName = objControlName;      //記録するコントロール名
            string titleString = "";            //ウインドタイトルデフォルト
            string changeVal = "";              //編集値デフォルト
            Decimal lowerLimitDec = 0;          //最小デフォルト
            Decimal upperLimitDec = 0;          //最大デフォルト
            string strTenkeyMode = "";          //UITenkeyModeAndPosRec.xmlより表示位置、表示モードを取得
            NumericTextBox.FormatTypes formatType = NumericTextBox.FormatTypes.Integer1;//フォーマットタイプ
            bool firstValueSelect = true;       //初回文字列選択= true
            bool realValueEdit = true;          //実数編集		= true
            bool oneKetaEditImpossible = true;  //下1桁編集不可	= true
            string controlName = ((Control)(objControlName)).Name;                        //初期値設定を変更します

           
            _gridAdjust_setCodeForControl(
                    objControlName,                 //コントロール名
                    ref titleString,                //ウインドタイトル
                    ref changeVal,                  //編集文字列
                    ref lowerLimitDec,              //最小値
                    ref upperLimitDec,              //最大値
                    ref strTenkeyMode,              //UITenkeyModeAndPosRec.xmlより表示位置、表示モードを取得
                    ref formatType,                 //フォーマットタイプ
                    ref firstValueSelect,           //初回文字列選択	= true
                    ref realValueEdit,              //実数編集			= true
                    ref oneKetaEditImpossible       //下1桁編集不可		= true
                    );
            if (changeVal == "") changeVal = "0";
            //ポップアップTenKey：
            _popupTenkey = new TenKeyDialog(
                changeVal,                      //このコントロールで表示する文字
                formatType,                     //NumericTextBoxの編集フォーマットタイプ
                lowerLimitDec,                  //最小値
                upperLimitDec,                  //最大値
                firstValueSelect,               //true=初回文字列選択
                realValueEdit,                  //true=実数編集
                oneKetaEditImpossible,          //true=下1桁編集不可
                strTenkeyMode                   //テンキー表示モード：UITenkeyModeAndPosRec.xmlに記述された表示位置やモードで表示
            );
            _popupTenkey.NotifyReturnOk = popupTenkey_To_Adjust_OnNotifyReturnOk; //イベント通知：OK
            _popupTenkey.NotifyClose = popupTenkey_To_Adjust_NotifyClose;
            _popupTenkey.Text = titleString;                            //テンキータイトル表示
            _popupTenkey.ShowDialog(this);                              //画面を開く:
            _popupTenkey.Dispose();                                     //破棄
            _popupTenkey = null;                                        //null初期化
         
        }
        //記録する行列位置
        //_gridAxis
        private int _gridAxis_rowIndex = 0;
        private int _gridAxis_colIndex = 0;
        /// <summary>
        /// 各コントロールごとにコード変更：※基本ここを編集します
        /// </summary>
        /// <param name="objControlName"></param>
        /// <param name="titleString"></param>
        /// <param name="changeVal"></param>
        /// <param name="lowerLimitDec"></param>
        /// <param name="upperLimitDec"></param>
        /// <param name="strTenkeyMode"></param>
        /// <param name="formatType"></param>
        /// <param name="firstValueSelect"></param>
        /// <param name="realValueEdit"></param>
        /// <param name="oneKetaEditImpossible"></param>
        private void _gridAxis_setCodeForControl(
            object objControlName,          //コントロール名
            int rowIndex,
            int colIndex,
            ref string titleString,         //ウインドタイトル
            ref string changeVal,           //編集文字列
            ref Decimal lowerLimitDec,      //最小値
            ref Decimal upperLimitDec,      //最大値
            ref string strTenkeyMode,       //UITenkeyModeAndPosRec.xmlより表示位置、表示モードを取得
            ref NumericTextBox.FormatTypes formatType,//フォーマットタイプ
            ref bool firstValueSelect,      //初回文字列選択	= true
            ref bool realValueEdit,         //実数編集			= true
            ref bool oneKetaEditImpossible  //下1桁編集不可		= true
            )
        {
            _gridAxis_rowIndex = rowIndex;
            _gridAxis_colIndex = colIndex;
            switch (colIndex)
            {
                //各クリックされたセル値を取得
                case (int)_gridAxisColumnName.RevMagnify: //寿命
                    titleString = _gridAxis.Columns[(int)_gridAxisColumnName.RevMagnify].HeaderCell.Value.ToString();//ウインドタイトル
                    changeVal = (_gridAxis[_gridAxis_colIndex, _gridAxis_rowIndex].Value == null)? "0": (_gridAxis[_gridAxis_colIndex, _gridAxis_rowIndex].Value.ToString());   //編集文字列
                    lowerLimitDec = (decimal)0;                                 //最小値
                    upperLimitDec = (decimal)10;                         //最大値
                    formatType = NumericTextBox.FormatTypes.Integer3;       //フォーマットタイプ
                    break;

                case (int)_gridAxisColumnName.RevSpace:  //現在値
                    titleString = _gridAxis.Columns[(int)_gridAxisColumnName.RevSpace].HeaderCell.Value.ToString();//ウインドタイトル
                    changeVal = (_gridAxis[_gridAxis_colIndex, _gridAxis_rowIndex].Value == null) ? "1000" : (_gridAxis[_gridAxis_colIndex, _gridAxis_rowIndex].Value.ToString());   //編集文字列
                    lowerLimitDec = (decimal)1000;                             //最小値
                    upperLimitDec = (decimal)1000000;                      //最大値
                    formatType = NumericTextBox.FormatTypes.Integer7;       //フォーマットタイプ
                    break;

                case (int)_gridAxisColumnName.RevTopNo:  //現在値
                    titleString = _gridAxis.Columns[(int)_gridAxisColumnName.RevTopNo].HeaderCell.Value.ToString();//ウインドタイトル
                    changeVal = (_gridAxis[_gridAxis_colIndex, _gridAxis_rowIndex].Value == null) ? "0" : (_gridAxis[_gridAxis_colIndex, _gridAxis_rowIndex].Value.ToString());   //編集文字列
                    lowerLimitDec = (decimal)0;                             //最小値
                    upperLimitDec = (decimal)33280;                      //最大値
                    formatType = NumericTextBox.FormatTypes.Integer5;       //フォーマットタイプ
                    break;

                case (int)_gridAxisColumnName.RevMCnt:  //現在値
                    titleString = _gridAxis.Columns[(int)_gridAxisColumnName.RevMCnt].HeaderCell.Value.ToString();//ウインドタイトル
                    changeVal = (_gridAxis[_gridAxis_colIndex, _gridAxis_rowIndex].Value == null) ? "0" : (_gridAxis[_gridAxis_colIndex, _gridAxis_rowIndex].Value.ToString());   //編集文字列
                    lowerLimitDec = (decimal)0;                             //最小値
                    upperLimitDec = (decimal)2000;                      //最大値
                    formatType = NumericTextBox.FormatTypes.Integer4;       //フォーマットタイプ
                    break;

                case (int)_gridAxisColumnName.RevPCnt:  //現在値
                    titleString = _gridAxis.Columns[(int)_gridAxisColumnName.RevPCnt].HeaderCell.Value.ToString();//ウインドタイトル
                    changeVal = (_gridAxis[_gridAxis_colIndex, _gridAxis_rowIndex].Value == null) ? "0" : (_gridAxis[_gridAxis_colIndex, _gridAxis_rowIndex].Value.ToString());   //編集文字列
                    lowerLimitDec = (decimal)0;                             //最小値
                    upperLimitDec = (decimal)2000;                      //最大値
                    formatType = NumericTextBox.FormatTypes.Integer4;       //フォーマットタイプ
                    break;

            }
            firstValueSelect = true;        //初回文字列選択	= true
            realValueEdit = false;          //実数編集			= true
            oneKetaEditImpossible = false;  //下1桁編集不可		= true
        }

        private Color _BeforeColor = Color.Black;
        /// <summary>
        /// ポップアップテンキーで「OK」ボタンを押した時、入力値をこのクラスに反映
        /// </summary>
        private void popupTenkey_To_gridAxis_OnNotifyReturnOk()
        { 
            string retVal = _popupTenkey._tenkeyValReturn;  //ポップアップテンキーで編集された値
            switch (_gridAxis_colIndex)
            {                           //記録していたコントロール値
                case (int)_gridAxisColumnName.RevMagnify: //補正倍率
                case (int)_gridAxisColumnName.RevSpace:  //補正間隔
                case (int)_gridAxisColumnName.RevTopNo:  //先頭番号
                case (int)_gridAxisColumnName.RevMCnt:  //-区間数
                case (int)_gridAxisColumnName.RevPCnt:  //+区間数
                    _gridAxis[_gridAxis_colIndex, _gridAxis_rowIndex].Value = int.Parse(retVal);//カレントセルに入れる
                    _gridAxis.EndEdit();  //これが無いと表示が更新されません。
                    _gridAxis.Refresh();  //再描画
                    break;

            }
        }
        /// <summary>
        /// 各コントロールごとにコード変更：※基本ここを編集します
        /// </summary>
        /// <param name="objControlName"></param>
        /// <param name="titleString"></param>
        /// <param name="changeVal"></param>
        /// <param name="lowerLimitDec"></param>
        /// <param name="upperLimitDec"></param>
        /// <param name="strTenkeyMode"></param>
        /// <param name="formatType"></param>
        /// <param name="firstValueSelect"></param>
        /// <param name="realValueEdit"></param>
        /// <param name="oneKetaEditImpossible"></param>
        private void _gridAdjust_setCodeForControl(
            object objControlName,          //コントロール名
            ref string titleString,         //ウインドタイトル
            ref string changeVal,           //編集文字列
            ref Decimal lowerLimitDec,      //最小値
            ref Decimal upperLimitDec,      //最大値
            ref string strTenkeyMode,       //UITenkeyModeAndPosRec.xmlより表示位置、表示モードを取得
            ref NumericTextBox.FormatTypes formatType,//フォーマットタイプ
            ref bool firstValueSelect,      //初回文字列選択	= true
            ref bool realValueEdit,         //実数編集			= true
            ref bool oneKetaEditImpossible  //下1桁編集不可		= true
            )
        {
            lowerLimitDec = (decimal)-32767;                        //最小値
            upperLimitDec = (decimal)32767;                         //最大値
            formatType = NumericTextBox.FormatTypes.SignLong5;      //フォーマットタイプ
            if (changeVal == "") changeVal = "0";
            firstValueSelect = true;        //初回文字列選択	= true
            realValueEdit = false;          //実数編集			= true
            oneKetaEditImpossible = false;  //下1桁編集不可		= true
        }
        private void popupTenkey_To_Adjust_NotifyClose()
        {
            foreach (Control ctrl in _RevDtPanel.Controls)
            {
                //型が違ったら処理をスキップする。
                if (ctrl.GetType() != typeof(LabelExAndButtonEx)) continue;
                //選択状態のコントロールに、入力値を格納する。
                if ((ctrl as LabelExAndButtonEx).Selected == true) (ctrl as LabelExAndButtonEx).Selected = false;
            }
        }
        /// <summary>
        /// ポップアップテンキーで「OK」ボタンを押した時、入力値をこのクラスに反映
        /// </summary>
        private void popupTenkey_To_Adjust_OnNotifyReturnOk()
        {
            string retVal = _popupTenkey._tenkeyValReturn;  //ポップアップテンキーで編集された値
            foreach(Control ctrl in _RevDtPanel.Controls)
            {
                //型が違ったら処理をスキップする。
                if (ctrl.GetType() != typeof(LabelExAndButtonEx)) continue;
                //選択状態のコントロールに、入力値を格納する。
                if ((ctrl as LabelExAndButtonEx).Selected == true)
                {
                    (ctrl as LabelExAndButtonEx).Value = retVal;
                } 
            }
            //表示している補正値データの格納
            switch (_selectedAxis)
            {
                case AxisNumbers.X: _SaveAllRevDt(_selectAdjustListAxisX); break;
                case AxisNumbers.Y: _SaveAllRevDt(_selectAdjustListAxisY); break;
                case AxisNumbers.W: _SaveAllRevDt(_selectAdjustListAxisW); break;
                case AxisNumbers.Z: _SaveAllRevDt(_selectAdjustListAxisZ); break;
                case AxisNumbers.A: _SaveAllRevDt(_selectAdjustListAxisA); break;
                case AxisNumbers.B: _SaveAllRevDt(_selectAdjustListAxisB); break;
                case AxisNumbers.C: _SaveAllRevDt(_selectAdjustListAxisC); break;
                case AxisNumbers.I: _SaveAllRevDt(_selectAdjustListAxisI); break;
            }

        }
        #endregion
        #region DataGridView
        private void _gridAxis_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if(e.ColumnIndex == 0)
            {
                //行ソート
                foreach (DataGridViewRow row in _gridAxis.Rows)
                {
                    //列ソート
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        _gridAxis[cell.ColumnIndex, cell.RowIndex].Style.BackColor = FileUIStyleTable.DefaultBackColor;
                        _gridAxis[cell.ColumnIndex, cell.RowIndex].Style.ForeColor = FileUIStyleTable.DefaultForeColor;
                    }
                }
                //列ヘッダ
                foreach(DataGridViewColumn col in _gridAxis.Columns)
                {
                    _gridAxis.Columns[col.Index].HeaderCell.Style.BackColor = FileUIStyleTable.DefaultBackColor;
                    _gridAxis.Columns[col.Index].HeaderCell.Style.ForeColor = FileUIStyleTable.DefaultForeColor;
                }
                //その他のセルが選択色なら未選択色に切り替える。
                if (_gridAxis[e.ColumnIndex, e.RowIndex].Style.BackColor == FileUIStyleTable.DefaultBackColor)
                {
                    _gridAxis[e.ColumnIndex, e.RowIndex].Style.BackColor = FileUIStyleTable.SelectedLineColor;
                    _gridAxis[e.ColumnIndex, e.RowIndex].Style.ForeColor = FileUIStyleTable.EnabledForeColor;
                }
                else
                {
                    _gridAxis[e.ColumnIndex, e.RowIndex].Style.BackColor = FileUIStyleTable.DefaultBackColor;
                    _gridAxis[e.ColumnIndex, e.RowIndex].Style.ForeColor = FileUIStyleTable.DefaultForeColor;
                }
                //表示している補正値データの格納
                switch (_selectedAxis)
                {
                    case AxisNumbers.X: _SaveAllRevDt(_selectAdjustListAxisX); break;
                    case AxisNumbers.Y: _SaveAllRevDt(_selectAdjustListAxisY); break;
                    case AxisNumbers.W: _SaveAllRevDt(_selectAdjustListAxisW); break;
                    case AxisNumbers.Z: _SaveAllRevDt(_selectAdjustListAxisZ); break;
                    case AxisNumbers.A: _SaveAllRevDt(_selectAdjustListAxisA); break;
                    case AxisNumbers.B: _SaveAllRevDt(_selectAdjustListAxisB); break;
                    case AxisNumbers.C: _SaveAllRevDt(_selectAdjustListAxisC); break;
                    case AxisNumbers.I: _SaveAllRevDt(_selectAdjustListAxisI); break;
                }
                _selectedAxis = GetSelectedAxis();
                switch(_selectedAxis)
                {
                    case AxisNumbers.X: _SetAllRevDt(_selectAdjustListAxisX, 0); break;
                    case AxisNumbers.Y: _SetAllRevDt(_selectAdjustListAxisY, 0); break;
                    case AxisNumbers.W: _SetAllRevDt(_selectAdjustListAxisW, 0); break;
                    case AxisNumbers.Z: _SetAllRevDt(_selectAdjustListAxisZ, 0); break;
                    case AxisNumbers.A: _SetAllRevDt(_selectAdjustListAxisA, 0); break;
                    case AxisNumbers.B: _SetAllRevDt(_selectAdjustListAxisB, 0); break;
                    case AxisNumbers.C: _SetAllRevDt(_selectAdjustListAxisC, 0); break;
                    case AxisNumbers.I: _SetAllRevDt(_selectAdjustListAxisI, 0); break;
                }
            }
            else
            {
                //各軸パラメータリストの選択状態変更
                //行ソート
                foreach (DataGridViewRow row in _gridAxis.Rows)
                {
                    //列ソート
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.RowIndex == e.RowIndex
                        && cell.ColumnIndex == e.ColumnIndex)
                        {
                            if (e.ColumnIndex != 0)
                            {
                                //クリックしたセルの色を切り替える
                                _gridAxis[e.ColumnIndex, e.RowIndex].Style.BackColor = FileUIStyleTable.EnabledBackColor;
                                _gridAxis[e.ColumnIndex, e.RowIndex].Style.ForeColor = FileUIStyleTable.EnabledForeColor;
                                //選択セルの列ヘッダ色を切り替える
                                _gridAxis.Columns[e.ColumnIndex].HeaderCell.Style.BackColor = FileUIStyleTable.EnabledBackColor;
                                _gridAxis.Columns[e.ColumnIndex].HeaderCell.Style.ForeColor = FileUIStyleTable.EnabledForeColor;
                                //選択セルの行ヘッダ色を切り替える。
                                _gridAxis.Rows[e.RowIndex].Cells[0].Style.BackColor = FileUIStyleTable.SelectedLineColor;
                                _gridAxis.Rows[e.RowIndex].Cells[0].Style.ForeColor = FileUIStyleTable.EnabledForeColor;
                            }
                        }
                        else
                        {
                            //その他のセルが選択色なら未選択色に切り替える。
                            _gridAxis[cell.ColumnIndex, cell.RowIndex].Style.BackColor = FileUIStyleTable.DefaultBackColor;
                            _gridAxis[cell.ColumnIndex, cell.RowIndex].Style.ForeColor = FileUIStyleTable.DefaultForeColor;
                        }
                        if (cell.ColumnIndex != e.ColumnIndex)
                        {
                            //未選択セルの列ヘッダ色を未選択色に切り替える。
                            if (_gridAxis.Columns[cell.ColumnIndex].HeaderCell.Style.BackColor == FileUIStyleTable.EnabledBackColor)
                            {
                                _gridAxis.Columns[cell.ColumnIndex].HeaderCell.Style.BackColor = FileUIStyleTable.DefaultBackColor;
                                _gridAxis.Columns[cell.ColumnIndex].HeaderCell.Style.ForeColor = FileUIStyleTable.DefaultForeColor;
                            }
                        }
                        if (row.Index != e.RowIndex)
                        {
                            //未選択セルの行ヘッダ色を未選択色に切り替える。
                            if (_gridAxis.Rows[cell.RowIndex].Cells[0].Style.BackColor == FileUIStyleTable.EnabledBackColor)
                            {
                                _gridAxis.Rows[cell.RowIndex].Cells[0].Style.BackColor = FileUIStyleTable.DefaultBackColor;
                                _gridAxis.Rows[cell.RowIndex].Cells[0].Style.ForeColor = FileUIStyleTable.DefaultForeColor;
                            }
                        }
                    }
                }
                //表示している補正値データの格納
                switch (_selectedAxis)
                {
                    case AxisNumbers.X: _SaveAllRevDt(_selectAdjustListAxisX); break;
                    case AxisNumbers.Y: _SaveAllRevDt(_selectAdjustListAxisY); break;
                    case AxisNumbers.W: _SaveAllRevDt(_selectAdjustListAxisW); break;
                    case AxisNumbers.Z: _SaveAllRevDt(_selectAdjustListAxisZ); break;
                    case AxisNumbers.A: _SaveAllRevDt(_selectAdjustListAxisA); break;
                    case AxisNumbers.B: _SaveAllRevDt(_selectAdjustListAxisB); break;
                    case AxisNumbers.C: _SaveAllRevDt(_selectAdjustListAxisC); break;
                    case AxisNumbers.I: _SaveAllRevDt(_selectAdjustListAxisI); break;
                }
                _selectedAxis = GetSelectedAxis();
                //格納されている補正値データの表示
                switch (_selectedAxis)
                {
                    case AxisNumbers.X: _SetAllRevDt(_selectAdjustListAxisX, 0); break;
                    case AxisNumbers.Y: _SetAllRevDt(_selectAdjustListAxisY, 0); break;
                    case AxisNumbers.W: _SetAllRevDt(_selectAdjustListAxisW, 0); break;
                    case AxisNumbers.Z: _SetAllRevDt(_selectAdjustListAxisZ, 0); break;
                    case AxisNumbers.A: _SetAllRevDt(_selectAdjustListAxisA, 0); break;
                    case AxisNumbers.B: _SetAllRevDt(_selectAdjustListAxisB, 0); break;
                    case AxisNumbers.C: _SetAllRevDt(_selectAdjustListAxisC, 0); break;
                    case AxisNumbers.I: _SetAllRevDt(_selectAdjustListAxisI, 0); break;
                }
                if (e.ColumnIndex > 0) popupTenkeyOn(_gridAxis, e.RowIndex, e.ColumnIndex);
            }
        }
       

        private void _gridAxis_SelectionChanged(object sender, EventArgs e)
        {
            if(_gridAxis.SelectedCells.Count > 0)_gridAxis.ClearSelection();
        }

        private void PitchSettingForm_Shown(object sender, EventArgs e)
        {
            if (_gridAxis.SelectedCells.Count > 0) _gridAxis.ClearSelection();
        }
        #endregion

        private void _CsvReadingBtn_Click(object sender, EventArgs e)
        {
            using (McDatPitchAdjust datPit = new McDatPitchAdjust())
            {
                try
                {
                    ResultCodes writeResult = datPit.XmlToBinary();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void _CsvWritingBtn_Click(object sender, EventArgs e)
        {
            using (McDatPitchAdjust datPit = new McDatPitchAdjust())
            {
                try
                {
                    ResultCodes readResult = datPit.BinaryToXml();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void _RevDtValues_Click(object sender, EventArgs e)
        {
            foreach(Control ctrl in _RevDtPanel.Controls)
            {
                if(ctrl.GetType() == typeof(LabelExAndButtonEx))
                {
                    if(ctrl.Name != ((sender as Control).Parent as LabelExAndButtonEx).Name)
                    {
                        (ctrl as LabelExAndButtonEx).Selected = false;
                    }
                }
            }
            ((sender as Control).Parent as LabelExAndButtonEx).Selected = true;
            popupTenkeyOn((sender as Control).Parent as LabelExAndButtonEx);
        }
        /// <summary>
        /// 補正値データのページ送り機能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _AdjustIndexUpDown_Click(object sender, EventArgs e)
        {
            //ボタンテキストの格納
            string tempSenderName = (sender as Control).Text;
            //ページの送り方向を解析
            Direction direction = (tempSenderName.Contains("▲") == true) ? Direction.Up : Direction.Down;
            //ページの送り量を解析
            int upDownCount = int.Parse(tempSenderName.Replace((direction == Direction.Up) ? "▲" : "▼", ""))
                              * ((direction == Direction.Up) ? -1 : 1);
            //現在のページから、目標ページ数を算出
            upDownCount += int.Parse(_RevDt1.Title.Replace("No.", ""));
            //ページ数の上下限チェック
            if (upDownCount < 0) upDownCount = 0;
            if (upDownCount > (RevDtCount - 32)) upDownCount = (RevDtCount - 32);
            //ページ移動
            switch (_selectedAxis)
            {
                case AxisNumbers.X: _SetAllRevDt(_selectAdjustListAxisX, upDownCount); break;
                case AxisNumbers.Y: _SetAllRevDt(_selectAdjustListAxisY, upDownCount); break;
                case AxisNumbers.W: _SetAllRevDt(_selectAdjustListAxisW, upDownCount); break;
                case AxisNumbers.Z: _SetAllRevDt(_selectAdjustListAxisZ, upDownCount); break;
                case AxisNumbers.A: _SetAllRevDt(_selectAdjustListAxisA, upDownCount); break;
                case AxisNumbers.B: _SetAllRevDt(_selectAdjustListAxisB, upDownCount); break;
                case AxisNumbers.C: _SetAllRevDt(_selectAdjustListAxisC, upDownCount); break;
                case AxisNumbers.I: _SetAllRevDt(_selectAdjustListAxisI, upDownCount); break;
            }
        }

        private void _TopBtn_Click(object sender, EventArgs e)
        {
            //ページ移動
            switch (_selectedAxis)
            {
                case AxisNumbers.X: _SetAllRevDt(_selectAdjustListAxisX, 0); break;
                case AxisNumbers.Y: _SetAllRevDt(_selectAdjustListAxisY, 0); break;
                case AxisNumbers.W: _SetAllRevDt(_selectAdjustListAxisW, 0); break;
                case AxisNumbers.Z: _SetAllRevDt(_selectAdjustListAxisZ, 0); break;
                case AxisNumbers.A: _SetAllRevDt(_selectAdjustListAxisA, 0); break;
                case AxisNumbers.B: _SetAllRevDt(_selectAdjustListAxisB, 0); break;
                case AxisNumbers.C: _SetAllRevDt(_selectAdjustListAxisC, 0); break;
                case AxisNumbers.I: _SetAllRevDt(_selectAdjustListAxisI, 0); break;
            }
        }

        private void _EndBtn_Click(object sender, EventArgs e)
        {
            //ページ移動
            switch (_selectedAxis)
            {
                case AxisNumbers.X: _SetAllRevDt(_selectAdjustListAxisX, RevDtCount - 32); break;
                case AxisNumbers.Y: _SetAllRevDt(_selectAdjustListAxisY, RevDtCount - 32); break;
                case AxisNumbers.W: _SetAllRevDt(_selectAdjustListAxisW, RevDtCount - 32); break;
                case AxisNumbers.Z: _SetAllRevDt(_selectAdjustListAxisZ, RevDtCount - 32); break;
                case AxisNumbers.A: _SetAllRevDt(_selectAdjustListAxisA, RevDtCount - 32); break;
                case AxisNumbers.B: _SetAllRevDt(_selectAdjustListAxisB, RevDtCount - 32); break;
                case AxisNumbers.C: _SetAllRevDt(_selectAdjustListAxisC, RevDtCount - 32); break;
                case AxisNumbers.I: _SetAllRevDt(_selectAdjustListAxisI, RevDtCount - 32); break;
            }
        }
    }
}
