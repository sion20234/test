///////////////////////////////////////////////////////////////////////////////////////////
//
// (1) システム名称 : ELENIX ECNCⅢ
// (2) ファイル名   : ReturnToOriginForm.cs
// (3) 概要         : 仮想点登録、保護設定画面
// (4) 作成日       : 2016.07.19
// (5) 作成者       : 八野
// (6) 特記事項     : VisualStudio2015 C#
// (7) 更新履歴     : 座標登録画面の選択、画面操作の変更：2017.06.12：柏原
//
///////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Linq;
using System.Windows.Forms;
using ECNC3.Models.McIf;
using ECNC3.Enumeration;
using ECNC3.Models;
using System.Drawing;
using System.Threading;
using ECNC3.Models.Common;
using System.Threading.Tasks;

namespace ECNC3.Views
{
    public enum ReturnToOriginEnum
    {
        NONE,
        MachineOriginMove,
        WorkMove,
        VirtualMove
    }

    /// <summary>
    /// 仮想点登録、保護設定画面
    /// </summary>
    /// <remarks>
    /// 機械座標、ワーク座標を指定し、仮想点を登録する。
    /// 登録した仮想点情報を、機械座標、ワーク座標、測定点のいずれかを指定し、設定する。
    /// </remarks>
    public partial class ReturnToOriginForm : ECNC3Form
    {
        ReturnToOriginEnum OriginMode { get; set; }

        internal bool[] SyncPosition = new bool[1002];

        internal int AxisX { get; set; }
        internal int AxisY { get; set; }
        internal int AxisW { get; set; }
        internal int AxisA { get; set; }
        internal int AxisB { get; set; }
        internal int AxisC { get; set; }

        Models.StructurePositioniingItem XaxisPos;
        Models.StructurePositioniingItem YaxisPos;
        Models.StructurePositioniingItem WaxisPos;
        Models.StructurePositioniingItem ZaxisPos;
        Models.StructurePositioniingItem AaxisPos;
        Models.StructurePositioniingItem BaxisPos;
        Models.StructurePositioniingItem CaxisPos;
        //フォームの終了通知
        public bool _FormCloseSygnal = false;
        public bool _FormFunctionRunning = false;

        /// <summary>
        /// 現在の有効桁数
        /// </summary>
        private DigitSelect _digit = DigitSelect.Four;

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
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <remarks>現座標をメインフォームの座標表示コントロールから取得するイベントハンドラ処理</remarks>
        public ReturnToOriginForm()
		{
			InitializeComponent();
		}
        bool _threadStop = false;

        public void RTOStatusMonitoring(StatusMonitorEventArgs e)
        {
            if (_threadStop == true)
            {
                if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                {
                    this.Close();
                }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                return;
            }
            if (_FormCloseSygnal == true)
            {
                return;
            }
            if (MachineWorkChgBt.Text == "WORK")
            {
                using (MonitoringFunction MonitorFunc = new MonitoringFunction())
                {
                    if( (this.IsDisposed == true || this.Disposing == true)   ) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                    {
                        XaxisText.Text = PositionToString(e.Items.WorkAxisPos.AxisX);
                        YaxisText.Text = PositionToString(e.Items.WorkAxisPos.AxisY);
                        WaxisText.Text = PositionToString(e.Items.WorkAxisPos.AxisW);
                        AaxisText.Text = PositionToString(e.Items.WorkAxisPos.AxisA);
                        BaxisText.Text = PositionToString(e.Items.WorkAxisPos.AxisB);
                        CaxisText.Text = PositionToString(e.Items.WorkAxisPos.AxisC);
                    }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                }
            }
            else
            {
                if( (this.IsDisposed == true || this.Disposing == true)   ) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                {
                    XaxisText.Text = PositionToString(e.Items.MacAxisPos.AxisX);
                    YaxisText.Text = PositionToString(e.Items.MacAxisPos.AxisY);
                    WaxisText.Text = PositionToString(e.Items.MacAxisPos.AxisW);
                    AaxisText.Text = PositionToString(e.Items.MacAxisPos.AxisA);
                    BaxisText.Text = PositionToString(e.Items.MacAxisPos.AxisB);
                    CaxisText.Text = PositionToString(e.Items.MacAxisPos.AxisC);
                }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
            }
            foreach (DataGridViewRow _dgRow in _dgList.Rows)
            {
                if ((string)(_dgRow.Cells[1].Value) == XaxisText.Text)
                {
                    SyncPosition[_dgRow.Index] = true;

                }
                else
                {
                    SyncPosition[_dgRow.Index] = false;
                }
            }


            if (e.Items.StartSwBtnOffEdge == true)
            {
                switch (OriginMode)
                {
                    case ReturnToOriginEnum.MachineOriginMove:

                        //状態監視処理による条件分岐
                        if ((e.Items.SequenceEnd == true)
                        && (e.Items.SpindleOn == false)
                        && (e.Items.PumpOn == false)
                        && (e.Items.DischargeOn == false))
                        {
                            using (SequenceFunction SeqFunc = new SequenceFunction())
                            {
                                if( (this.IsDisposed == true || this.Disposing == true)   ) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                                {
                                    using (MessageDialog msg = new MessageDialog())
                                    {
                                        if (msg.Question(5021, this) == true)
                                        {
                                            //動作処理
                                            SeqFunc.SequenceMonitoring(Sequences.OriginAll);
                                        }
                                    }
                                }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                            }
                        }
                        else
                        {
                            if( (this.IsDisposed == true || this.Disposing == true)   ) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                            {
                                if (e.Items.SequenceEnd != true)
                                {
                                    using (MessageDialog msg = new MessageDialog())
                                    {
                                        msg.Warning(5017, this);
                                    }
                                }
                                if (e.Items.SpindleOn != false)
                                {
                                    using (MessageDialog msg = new MessageDialog())
                                    {
                                        msg.Warning(5018, this);
                                    }
                                }
                                if (e.Items.PumpOn != false)
                                {
                                    using (MessageDialog msg = new MessageDialog())
                                    {
                                        msg.Warning(5019, this);
                                    }
                                }
                                if (e.Items.DischargeOn != false)
                                {
                                    using (MessageDialog msg = new MessageDialog())
                                    {
                                        msg.Warning(5020, this);
                                    }
                                }
                            }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);

                            return;
                        }
                        break;

                    case ReturnToOriginEnum.WorkMove:
                        //状態監視処理による条件分岐
                        if ((e.Items.SequenceEnd == true)
                        && (e.Items.SpindleOn == false)
                        && (e.Items.PumpOn == false)
                        && (e.Items.DischargeOn == false))
                        {

                        }
                        else
                        {
                            if( (this.IsDisposed == true || this.Disposing == true)   ) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                            {
                                if (e.Items.SequenceEnd != true)
                                {
                                    using (MessageDialog msg = new MessageDialog())
                                    {
                                        msg.Warning(5017, this);
                                    }
                                }
                                if (e.Items.SpindleOn != false)
                                {
                                    using (MessageDialog msg = new MessageDialog())
                                    {
                                        msg.Warning(5018, this);
                                    }
                                }
                                if (e.Items.PumpOn != false)
                                {
                                    using (MessageDialog msg = new MessageDialog())
                                    {
                                        msg.Warning(5019, this);
                                    }
                                }
                                if (e.Items.DischargeOn != false)
                                {
                                    using (MessageDialog msg = new MessageDialog())
                                    {
                                        msg.Warning(5020, this);
                                    }
                                }
                            }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                        }
                        break;

                    case ReturnToOriginEnum.VirtualMove:
                        /// <summary>
                        /// 軸移動用パラメータ
                        /// </summary>
                        Models.StructurePositioniingItem[] PosItems = new Models.StructurePositioniingItem[7];

                        //状態監視処理による条件分岐
                        if ((e.Items.SequenceEnd == true)
                        && (e.Items.SpindleOn == false)
                        && (e.Items.PumpOn == false)
                        && (e.Items.DischargeOn == false))
                        {
                            PosItems[0] = XaxisPos;
                            PosItems[1] = YaxisPos;
                            PosItems[2] = WaxisPos;
                            PosItems[3] = ZaxisPos;
                            PosItems[4] = AaxisPos;
                            PosItems[5] = BaxisPos;
                            PosItems[6] = CaxisPos;

                            //軸移動パラメータ有
                            using (SequenceFunction SeqFunc = new SequenceFunction(PosItems.ToList()))
                            {
                                using (MonitoringFunction MonitorFunc = new MonitoringFunction())
                                {
                                    try
                                    {
                                        if (e.Items.FGEnd == true)
                                        {
                                            if (true == InvokeRequired)
                                            {
                                                Invoke((MethodInvoker)delegate
                                                {
                                                    UILog logs = new UILog("MAINForm.MANUAL.NumForm.SeqFunc.SequenceMonitoring");
                                                    logs.Sure("FGEnd == true");
                                                    string Error = "";
                                                    if (MachineWorkChgBt.Text == "MACHINE")
                                                    {
                                                        Error = (SeqFunc.SequenceMonitoring(Sequences.WaxisUpperSavedAbsMacMovePntToPnt)).ToString();
                                                        //ログ処理
                                                        logs.Error("ABSPTPMOVE_WUPP_MAC_START" + "Result = " + Error);
                                                    }
                                                    else
                                                    {
                                                        Error = (SeqFunc.SequenceMonitoring(Sequences.WaxisUpperSavedAbsWorkMovePntToPnt)).ToString();
                                                        //ログ処理
                                                        logs.Error("ABSPTPMOVE_WUPP_WORK_START" + "Result = " + Error);
                                                    }
                                                }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                                            }
                                        }
                                        else
                                        {
                                            if (true == InvokeRequired)
                                            {
                                                Invoke((MethodInvoker)delegate
                                                {
                                                    UILog logs = new UILog("MAINForm.MANUAL.NumForm.SeqFunc.SequenceMonitoring");
                                                    logs.Debug("FGEnd == false");
                                                    using (MessageDialog msg = new MessageDialog())
                                                    {
                                                        msg.Warning(5022, this);
                                                    }
                                                }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                }
                            }
                        }
                        else
                        {
                            UILog RTOLog = new UILog("MAINForm.MANUAL.RTOForm.RTOStatusMonitoring");
                            if( (this.IsDisposed == true || this.Disposing == true)   ) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                            {
                                if (e.Items.SequenceEnd != true)
                                {
                                    RTOLog.Error("SequenceEnd" + " Value = " + "FALSE");
                                    using (MessageDialog msg = new MessageDialog())
                                    {
                                        msg.Warning(5017, this);
                                    }

                                }
                                if (e.Items.SpindleOn != false)
                                {
                                    RTOLog.Error("SpindleOn" + " Value = " + "true");
                                    using (MessageDialog msg = new MessageDialog())
                                    {
                                        msg.Warning(5018, this);
                                    }
                                }
                                if (e.Items.PumpOn != false)
                                {
                                    RTOLog.Error("PumpOn" + " Value = " + "true");
                                    using (MessageDialog msg = new MessageDialog())
                                    {
                                        msg.Warning(5019, this);
                                    }
                                }
                                if (e.Items.DischargeOn != false)
                                {
                                    RTOLog.Error("DischargeOn" + " Value = " + "true");
                                    using (MessageDialog msg = new MessageDialog())
                                    {
                                        msg.Warning(5020, this);
                                    }
                                }
                            }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                        }
                        if (XaxisPos != null &&
                            YaxisPos != null &&
                            WaxisPos != null &&
                            ZaxisPos != null &&
                            AaxisPos != null &&
                            BaxisPos != null &&
                            CaxisPos != null)
                        {
                            XaxisPos.Dispose();
                            YaxisPos.Dispose();
                            WaxisPos.Dispose();
                            ZaxisPos.Dispose();
                            AaxisPos.Dispose();
                            BaxisPos.Dispose();
                            CaxisPos.Dispose();

                            XaxisPos = null;
                            YaxisPos = null;
                            WaxisPos = null;
                            ZaxisPos = null;
                            AaxisPos = null;
                            BaxisPos = null;
                            CaxisPos = null;
                        }


                        break;
                }
            }
            if (e.Items.FGEnd == true)
            {
                if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                {
                    _AllButtonsActivate(this.Controls);
                }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
            }
            else
            {
                if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                {
                    _AllButtonsDeActivate(this.Controls);
                }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
            }
        }
        private void _AllButtonsActivate(Control.ControlCollection target)
        {
            if (target == null) return;
            foreach (Control ctrl in target)
            {
                if (ctrl.Controls.Count != 0) _AllButtonsActivate(ctrl.Controls);
                if (ctrl.GetType() == typeof(ButtonEx))
                {
                    if ((ctrl as ButtonEx).Enabled != true) (ctrl as ButtonEx).Enabled = true;
                }
            }
        }
        private void _AllButtonsDeActivate(Control.ControlCollection target)
        {
            if (target == null) return;
            foreach (Control ctrl in target)
            {
                if (ctrl.Controls.Count != 0) _AllButtonsDeActivate(ctrl.Controls);
                if (ctrl.GetType() == typeof(ButtonEx))
                {
                    if ((ctrl as ButtonEx).Enabled != false) (ctrl as ButtonEx).Enabled = false;
                }
            }
        }
        /// <summary>
        /// フォームロード時に座標表示を初期設定する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>MAC/Workモードの初期表示切替</remarks>
        private void ReturnToOriginForm_Load( object sender, EventArgs e )
		{
            //原点復帰対象座標初期化
			OriginMode = ReturnToOriginEnum.MachineOriginMove;
            //座標データ初期化
			XaxisPos = null;
			YaxisPos = null;
			WaxisPos = null;
			ZaxisPos = null;
			AaxisPos = null;
			BaxisPos = null;
			CaxisPos = null;
            //DataGridView初期化
            _dgList_Initialize();
            //桁数取得
            string[] axisName = { "X", "Y", "W", "Z", "A", "B", "C", "I" };
            using (FileSettings fs = new FileSettings())
            {
                //ファイル読み込み
                fs.Read();
                LoadDigit(fs);
            }
            //座標系初期化
            MachineWorkChgBt.Text = "MACHINE";
            //リストのフォーカス設定と仮想点データ取得
			VirtualPosListFocusLeave();
			RefreshList();
			//行数設定
			_startCount = 0;
			_endCount = _dgList.Rows.Count;
			//trackBar1.Minimum = _startCount;
			//trackBar1.Maximum = _endCount;
			_dgList.AllowUserToAddRows = false;//最終行：非表示
        }
        /// <summary>
        /// DataGridViewの初期化処理
        /// </summary>
        private void _dgList_Initialize()
        {
            _dgList.Initialize(16.0F, 35);

            _dgList.InitCol("Number", 15.0F, typeof(string));
            _dgList.InitCol("Axis1", 15.0F, typeof(string));
            _dgList.InitCol("Axis2", 15.0F, typeof(string));
            _dgList.InitCol("Axis3", 15.0F, typeof(string));
            _dgList.InitCol("Axis5", 15.0F, typeof(string));
            _dgList.InitCol("Axis6", 15.0F, typeof(string));
            _dgList.InitCol("Axis7", 15.0F, typeof(string));
            using (FileOrgPos mc = new FileOrgPos())
            {
                mc.Read();
                _dgList.RowCount = mc.Items.Count;
                for (int iCt = 1; _dgList.RowCount > iCt; iCt++)                   //OrgPosを1行ずつ繰り返し処理で読み込む
                {
                    _dgList.Rows[iCt - 1].Cells["Number"].Value = mc.Items[iCt].Number;
                }
            }
        }
		/// <summary>一覧再表示</summary>
		private void RefreshList()
        {
            using (FileOrgPos OrgPos = new FileOrgPos())
            {
                OrgPos.Read();
                int index;
                foreach (DataGridViewRow item in _dgList.Rows)
                {
                    if(item.Cells["Number"].Value == null)
                    {
                        break;
                    }
                    index = (int)item.Cells["Number"].Value;
                    StructureAxisCoordinate data = OrgPos.Items.Find((x) => x.Number == index);
                    if (null != data)
                    {
                        SetRowData(item, data);
                    }
                }
                SetEditableRowColor();
            }
        }

        /// <summary>一行分のデータ設定</summary>
        /// <param name="item">グリッドコントロール</param>
        /// <param name="data">表示データ</param>
        private void SetRowData(DataGridViewRow item, StructureAxisCoordinate data)
        {
            item.Cells["Axis1"].Value = PositionToString(data.Axis1);
            item.Cells["Axis2"].Value = PositionToString(data.Axis2);
            item.Cells["Axis3"].Value = PositionToString(data.Axis3);
            item.Cells["Axis5"].Value = PositionToString(data.Axis5);
            item.Cells["Axis6"].Value = PositionToString(data.Axis6);
            item.Cells["Axis7"].Value = PositionToString(data.Axis7);
        }                                                
        
        /// <summary>
        /// プロテクト設定されている行の色管理
        /// </summary>
        /// <remarks>
        /// 保護無・・・通常色
        /// 保護有・・・「P」列が「Gainsboro」。行の文字が「グレー」
        /// 20170113 Hachino Add
        /// </remarks>
        private void SetEditableRowColor()
        {
            using (FileOrgPos OrgPos = new FileOrgPos())
            {
                OrgPos.Read();
                int index;
                //	設定の沿って設定。
                foreach (DataGridViewRow item in _dgList.Rows)
                {
                    if (item.Cells["Number"].Value == null)
                    {
                        break;
                    }
                    index = (int)item.Cells["Number"].Value;
                    if (OrgPos.Items.Find(delegate (StructureAxisCoordinate OrgFind) { return OrgFind.Number == index; }).Protect == 0)
                    {
                        item.DefaultCellStyle.ForeColor = FileUIStyleTable.DefaultForeColor; ;
                        item.Cells[0].Style.BackColor = FileUIStyleTable.DefaultBackColor;
                        item.Cells[0].Style.ForeColor = FileUIStyleTable.DefaultForeColor;
                    }
                    else
                    {
                        item.DefaultCellStyle.ForeColor = Color.Gray;
                        item.Cells[0].Style.BackColor = FileUIStyleTable.DefaultBackColor;
                        item.Cells[0].Style.ForeColor = FileUIStyleTable.DefaultForeColor;
                    }
                }
            }
        }



        // <summary>
        /// 加工条件リストの単一のセルをクリックした場合。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// 主に、書き込み保護設定で使用。「P」列をクリックした場合、該当の加工条件の保護を切り替える。
        /// 20170113 Hachino Add
        /// </remarks>
        private void _dgList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex == -1)
            {
                return;
            }

            WorkOriginBt.SetLed(false);
            MachineOriginBt.SetLed(false);
            RefOriginBt.SetLed(false);

            OriginMode = ReturnToOriginEnum.VirtualMove;
            XaxisPos = new Models.StructurePositioniingItem();
            YaxisPos = new Models.StructurePositioniingItem();
            WaxisPos = new Models.StructurePositioniingItem();
            ZaxisPos = new Models.StructurePositioniingItem();
            AaxisPos = new Models.StructurePositioniingItem();
            BaxisPos = new Models.StructurePositioniingItem();
            CaxisPos = new Models.StructurePositioniingItem();

            XaxisPos.TargetPosition = DigitAlign((_dgList.Rows[e.RowIndex].Cells[1].Value as string));
            YaxisPos.TargetPosition = DigitAlign((_dgList.Rows[e.RowIndex].Cells[2].Value as string));
            WaxisPos.TargetPosition = DigitAlign((_dgList.Rows[e.RowIndex].Cells[3].Value as string));
            AaxisPos.TargetPosition = DigitAlign((_dgList.Rows[e.RowIndex].Cells[4].Value as string));
            BaxisPos.TargetPosition = DigitAlign((_dgList.Rows[e.RowIndex].Cells[5].Value as string));
            CaxisPos.TargetPosition = DigitAlign((_dgList.Rows[e.RowIndex].Cells[6].Value as string));

            XaxisPos.Movable = true;
            YaxisPos.Movable = true;
            WaxisPos.Movable = true;
            ZaxisPos.Movable = false;
            AaxisPos.Movable = true;
            BaxisPos.Movable = true;
            CaxisPos.Movable = true;

            string LogVal = "[ ";
            LogVal += XaxisPos.TargetPosition + " ,";
            LogVal += YaxisPos.TargetPosition + " ,";
            LogVal += WaxisPos.TargetPosition + " ,";
            LogVal += ZaxisPos.TargetPosition + " ,";
            LogVal += AaxisPos.TargetPosition + " ,";
            LogVal += BaxisPos.TargetPosition + " ,";
            LogVal += CaxisPos.TargetPosition;
            LogVal += " ]";

            UILog virtualMoveLog = new UILog("ReturnToOriginFormParamListView_ItemSelectionChanged");
            virtualMoveLog.Sure("PositionSet = " + LogVal);

            _dgList.Refresh();
        }


        /// <summary>
        /// 現座標読み込み時の機械座標、ワーク座標切り替えボタン処理
        /// </summary>        
        /// 
        /// <param name="sender"></param>
        /// <param name="e">MachineWorkChgBtのボタンクリック時のイベント</param>
        private void MachineWorkChgBt_Click(object sender, EventArgs e)
        {
            //MAC/Workモードの表示切替

            if (MachineWorkChgBt.Text == "WORK")
            {
                MachineWorkChgBt.Text = "MACHINE";
            }
            else if (MachineWorkChgBt.Text == "MACHINE")
            {
                MachineWorkChgBt.Text = "WORK";
            }
            else { }
        }

        private void SaveOrgData(int _index)
        {
            _dgList.Refresh();
            using (Models.FileOrgPos OrgPos = new Models.FileOrgPos())
            {
                OrgPos.Read();
                StructureAxisCoordinate _writeOrg = OrgPos.Items.Find(delegate (StructureAxisCoordinate OrgFind) { return OrgFind.Number == _index; }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                _writeOrg.AxisX = DigitAlign((string)(_dgList.Rows[_index - 1].Cells[1].Value));
                _writeOrg.AxisY = DigitAlign((string)(_dgList.Rows[_index - 1].Cells[2].Value));
                _writeOrg.AxisW = DigitAlign((string)(_dgList.Rows[_index - 1].Cells[3].Value));
                _writeOrg.AxisA = DigitAlign((string)(_dgList.Rows[_index - 1].Cells[4].Value));
                _writeOrg.AxisB = DigitAlign((string)(_dgList.Rows[_index - 1].Cells[5].Value));
                _writeOrg.AxisC = DigitAlign((string)(_dgList.Rows[_index - 1].Cells[6].Value));              
                OrgPos.Write(_writeOrg);
            }
            _dgList.Refresh();
        }

        /// <summary>
        /// 座標の表示桁数が6桁以上の場合、6桁に合わせる。
        /// </summary>
        /// <param name="AxisVal">座標値</param>
        /// <returns></returns>
        private int DigitAlign(string AxisVal)
        {
            int ret = 0;

            if (AxisVal == null) return 0;//追加：柏原
            //座標の表示桁数を調べる
            switch ((AxisVal.Count() 
                 - (AxisVal.IndexOf(".") + 1)))
            {
                //下3桁の場合
                case 3:
                    ret = int.Parse(AxisVal.Replace(".", ""));                    
                    break;

                //下4桁の場合
                case 4:
                    string AlignVal = AxisVal.Remove(AxisVal.IndexOf(".") + 4).Replace(".", "");
                    
                    if(AlignVal.Count() > 6)
                    {
                        while (AlignVal.Count() > 6)
                        {
                            AlignVal = AlignVal.Remove(0, 1);
                            if(AlignVal.Count() == 6
                                || AlignVal.StartsWith("-"))
                            {
                                break;
                            }
                        }
                    }
                    ret = int.Parse(AlignVal);                    
                    break;
                
            }
            return ret;
        }

        /// <summary>
        /// 表示されている仮想点リストをファイル出力するためのクラスに格納する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">OrgPosDataSetボタンクリック時のイベント</param>
        private void OrgPosDataSet_Click(object sender, EventArgs e)
        {
            using (MessageDialog msgDir = new MessageDialog())
            {
                if (!RefOriginBt.GetBack())
                {
                    //仮想点登録処理
                    switch (_OrgPosDataSet(false))
                    {
                        case ResultCodes.NotSelect: msgDir.Error(5037, this); break;
                        case ResultCodes.AlreadySetting: msgDir.Error(5038, this); break;
                        case ResultCodes.WriteProtected: msgDir.Error(5039, this); break;
                        case ResultCodes.Success: break;
                    }
                }
                else
                {
                    //測定点登録処理
                    switch (_OrgPosDataSet(true))
                    {
                        case ResultCodes.AlreadySetting: msgDir.Error(5040, this); break;
                        case ResultCodes.WriteProtected: msgDir.Error(5041, this); break;
                        case ResultCodes.Success: break;
                    }
                }
                //プログレスバー初期化
                OrgPosDataSet.ProgressBarValue = 0;
            }
        }
        private ResultCodes _OrgPosDataSet(bool selectRefOrigin)
        {
            OrgPosDataSet.ProgressBarValue = 5;

            /*測定点選択状態の排他*/
            if (selectRefOrigin != true)
            {
                /*仮想点未選択状態の排他*/
                if (_dgList.SelectedCells.Count == 0) return ResultCodes.NotSelect;
                /*書き込み保護設定済みの排他*/
                if (_dgList.SelectedCells[0].OwningRow.Cells[1].Style.ForeColor == Color.Gray) return ResultCodes.WriteProtected;
                _dgList.Rows[_dgList.SelectedCells[0].RowIndex].Cells[1].Value = XaxisText.Text;
                _dgList.Rows[_dgList.SelectedCells[0].RowIndex].Cells[2].Value = YaxisText.Text;
                _dgList.Rows[_dgList.SelectedCells[0].RowIndex].Cells[3].Value = WaxisText.Text;
                _dgList.Rows[_dgList.SelectedCells[0].RowIndex].Cells[4].Value = AaxisText.Text;
                _dgList.Rows[_dgList.SelectedCells[0].RowIndex].Cells[5].Value = BaxisText.Text;
                _dgList.Rows[_dgList.SelectedCells[0].RowIndex].Cells[6].Value = CaxisText.Text;


                _dgList.Refresh();
                SaveOrgData((int)_dgList.Rows[_dgList.SelectedCells[0].RowIndex].Cells[0].Value);

                OrgPosDataSet.ProgressBarValue = 10;
                //	仮想点／測定点設定(REQ_VIRPOSCHG_EX)
                using (McReqVirtualPositionChange mc = new McReqVirtualPositionChange())
                {
                    ResultCodes result = ResultCodes.Success;
                    result = mc.Initialize();
                    if (ResultCodes.Success != result)
                    {
                        UILog VirtualPosSendLog = new UILog("OrgPosDataSet_Click");
                        VirtualPosSendLog.Error("Result = " + result.ToString());
                    }
                }
                OrgPosDataSet.ProgressBarValue = 30;
            }
            else 
            {
                /*書き込み保護設定済みの排他*/
                int rowCt = _dgList.RowCount - 2;
                if (_dgList.Rows[rowCt].Cells[1].Style.ForeColor == Color.Gray) return ResultCodes.WriteProtected;
                XREFaxisText.Text = (_dgList.Rows[rowCt].Cells[1].Value = XaxisText.Text).ToString();
                YREFaxisText.Text = (_dgList.Rows[rowCt].Cells[2].Value = YaxisText.Text).ToString();
                WREFaxisText.Text = (_dgList.Rows[rowCt].Cells[3].Value = WaxisText.Text).ToString();
                AREFaxisText.Text = (_dgList.Rows[rowCt].Cells[4].Value = AaxisText.Text).ToString();
                BREFaxisText.Text = (_dgList.Rows[rowCt].Cells[5].Value = BaxisText.Text).ToString();
                CREFaxisText.Text = (_dgList.Rows[rowCt].Cells[6].Value = CaxisText.Text).ToString();

                SetEditableRowColor();
                _dgList.Refresh();
                OrgPosDataSet.ProgressBarValue = 50;
                SaveOrgData(rowCt + 1);
                OrgPosDataSet.ProgressBarValue = 70;

                //	仮想点／測定点設定(REQ_VIRPOSCHG_EX)
                using (McReqVirtualPositionChange mc = new McReqVirtualPositionChange())
                {
                    ResultCodes result = ResultCodes.Success;
                    result = mc.Initialize();
                    if (ResultCodes.Success != result)
                    {
                        UILog VirtualPosSendLog = new UILog("OrgPosDataSet_Click");
                        VirtualPosSendLog.Error("Result = " + result.ToString());
                    }
                }
            }
            RefOriginBt.SetBack(false);
            //他のボタンの選択を外します
            //仮想点リストのフォーカス解除
            //VirtualPosListFocusLeave();
            //機械原点ボタンが選択状態の場合
            if (MachineOriginBt.GetBack() == true)
            {
                //機械原点ボタンの選択解除
                MachineOriginBt.SetBack(false);
            }
            OrgPosDataSet.ProgressBarValue = 73
                ;
            //ワーク原点ボタンが選択状態の場合
            if (WorkOriginBt.GetBack() == true)
            {
                //ワーク原点ボタンの選択解除
                WorkOriginBt.SetBack(false);
            }
            OrgPosDataSet.ProgressBarValue = 75;
            //測定点ボタンが選択状態の場合
            if (RefOriginBt.GetBack() == true)
            {
                //測定点ボタンの選択解除
                RefOriginBt.SetBack(false);
            }
            OrgPosDataSet.ProgressBarValue = 80;
            AidLog log = new AidLog();
            //パラメータバックアップ処理
            using (McReqTechnoBackUp technoBk = new McReqTechnoBackUp())
            {
                if (technoBk.Execute() == ResultCodes.Success)
                {
                    log.Sure("Techno Success!");
                }
                else
                {
                    log.Error("Techno Fail....Retry");
                    if (technoBk.Execute() == ResultCodes.Success)
                    {
                        log.Sure("Techno Success!");
                    }
                    else
                    {
                        log.Error("Techno Fail....End");
                    }
                }
            }
            OrgPosDataSet.ProgressBarValue = 100;
            this.Visible = false;
            this.Visible = true;
            return ResultCodes.Success;

        }

        /// <summary>
        /// 測定点を表示する処理。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">RefOriginBtボタンクリック時のイベント</param>
        private void RefOriginBt_Click(object sender, EventArgs e)
        {
            //他のボタンの選択を外します
            //仮想点リストのフォーカス解除
            VirtualPosListFocusLeave();
            //機械原点ボタンが選択状態の場合
            if (MachineOriginBt.GetBack() == true)
            {
                //機械原点ボタンの選択解除
                MachineOriginBt.SetBack(false);
            }
            //ワーク原点ボタンが選択状態の場合
            if (WorkOriginBt.GetBack() == true)
            {
                //ワーク原点ボタンの選択解除
                WorkOriginBt.SetBack(false);
            }
            if (RefOriginBt.GetBack() == false)
            {
                RefOriginBt.SetBack(true);
            }
            else
            {
                RefOriginBt.SetBack(false);
            }
        }
        /// <summary>
        /// 機械原点移動ボタンのイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// 他ボタンとの排他処理、ボタンの選択状態によって
        /// スタートトリガー動作を機械原点移動動作に設定、設定解除します。
        /// </remarks>
        private void MachineOriginBt_Click(object sender, EventArgs e)
        {
            //他のボタンの選択を外します
            //仮想点リストのフォーカス解除
            VirtualPosListFocusLeave();
            //ワーク原点ボタンが選択状態の場合
            if (WorkOriginBt.GetBack() == true)
            {
                //ワーク原点ボタンの選択解除
                WorkOriginBt.SetBack(false);
            }
            //測定点ボタンが選択状態の場合
            if(RefOriginBt.GetBack() == true)
            {
                //測定点ボタンの選択解除
                RefOriginBt.SetBack(false);
            }

            //機械原点動作準備をします。
            //機械原点ボタンが未選択状態の場合
            if(((ButtonEx)sender).GetBack() != true)
            {
                //スタートトリガー動作を機械原点移動に設定します。
                OriginMode = ReturnToOriginEnum.MachineOriginMove;
                //機械原点移動に設定不可の場合
                if(OriginMode != ReturnToOriginEnum.MachineOriginMove)
                {
                    //処理をぬけます。
                    return;
                }
                //機械原点移動に設定された場合、ボタンを選択状態にします。
                ((ButtonEx)sender).SetBack(true);
            }
            //機械原点ボタンが選択状態の場合
            else
            {
                //スタートトリガー動作を設定なし状態にします。
                OriginMode = ReturnToOriginEnum.NONE;
                //機械原点移動が設定されている場合
                if(OriginMode == ReturnToOriginEnum.MachineOriginMove)
                {
                    //処理を抜けます。
                    return;
                }
                //スタートトリガー動作が設定されていなければボタンを未選択状態にします。
                ((ButtonEx)sender).SetBack(false);
            }
        }

        private void WorkOriginBt_Click(object sender, EventArgs e)
        {
            //他のボタンの選択を外します
            //仮想点リストのフォーカス解除
            VirtualPosListFocusLeave();
            //機械原点ボタンが選択状態の場合
            if (MachineOriginBt.GetBack() == true)
            {
                //機械原点ボタンの選択解除
                MachineOriginBt.SetBack(false);
            }
            //測定点ボタンが選択状態の場合
            if (RefOriginBt.GetBack() == true)
            {
                //測定点ボタンの選択解除
                RefOriginBt.SetBack(false);
            }
            if (((ButtonEx)sender).GetBack() != true)
            {
                OriginMode = ReturnToOriginEnum.WorkMove;
                if (OriginMode != ReturnToOriginEnum.WorkMove)
                {
                    return;
                }
                ((ButtonEx)sender).SetBack(true);
            }
            else
            {
                OriginMode = ReturnToOriginEnum.NONE;
                if (OriginMode == ReturnToOriginEnum.WorkMove)
                {
                    return;
                }
                ((ButtonEx)sender).SetBack(false);
            }
        }

        private void VirtualPosListFocusLeave()
        {
            _dgList.ClearSelection();
            SetEditableRowColor();
        }

        private void _selIdxComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(_selIdxComboBox.SelectedItem == null)
            {
                return;
            }
            VirtualPosListFocusLeave();

            _dgList.FirstDisplayedScrollingRowIndex = Convert.ToInt32(_selIdxComboBox.SelectedItem) - 1;
        }

        private Rectangle newRect = new Rectangle();
        private Pen _outLinePen = new Pen(Color.Lime, 3.0F);
        private void _dgList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (SyncPosition[e.RowIndex] == true)
            {
                //グラデーションブラシを作成
                newRect.X = e.RowBounds.X + 5;
                newRect.Y = e.RowBounds.Y + 4;
                newRect.Width = e.RowBounds.Width - 11;
                newRect.Height = e.RowBounds.Height - 10;
                e.Graphics.DrawRectangle(_outLinePen, newRect);
            }
        }

        private void _dgList_SelectionChanged(object sender, EventArgs e)
        {
            using (FileOrgPos OrgPos = new FileOrgPos())
            {
                OrgPos.Read();
                foreach (DataGridViewRow dgrow in _dgList.Rows)
            {
                    if(dgrow.Cells[0].Value == null)
                    {
                        break;
                    }
                    if (_dgList.SelectedCells.Count != 0
                        && _dgList.SelectedCells[0].RowIndex == dgrow.Index)
                    {
                        if (OrgPos.Items.Find(delegate (StructureAxisCoordinate OrgFind) { return OrgFind.Number == (int)(dgrow.Cells[0].Value); }).Protect == 1)
                        {
                            foreach (DataGridViewCell dgCell in dgrow.Cells)
                            {
                                if (dgCell.ColumnIndex != 0)
                                {
                                    dgCell.Style.BackColor = FileUIStyleTable.EnabledBackColor;
                                    dgCell.Style.ForeColor = Color.Gray;
                                }
                                else if (dgCell.ColumnIndex == 0)
                                {
                                    dgCell.Style.BackColor = FileUIStyleTable.EnabledBackColor;
                                    dgCell.Style.ForeColor = FileUIStyleTable.EnabledForeColor;
                                }
                            }
                                
                        }
                        else
                        {
                            foreach (DataGridViewCell dgCell in dgrow.Cells)
                            {
                                if (dgCell.ColumnIndex != 0)
                                {
                                    dgCell.Style.BackColor = FileUIStyleTable.EnabledBackColor;
                                    dgCell.Style.ForeColor = FileUIStyleTable.EnabledForeColor;
                                }
                                else if (dgCell.ColumnIndex == 0)
                                {
                                    dgCell.Style.BackColor = FileUIStyleTable.EnabledBackColor;
                                    dgCell.Style.ForeColor = FileUIStyleTable.EnabledForeColor;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (OrgPos.Items.Find(delegate (StructureAxisCoordinate OrgFind) { return OrgFind.Number == (int)(dgrow.Cells[0].Value); }).Protect == 1)
                        {
                            foreach (DataGridViewCell dgCell in dgrow.Cells)
                            {
                                if (dgCell.ColumnIndex != 0)
                                {
                                    dgCell.Style.BackColor = FileUIStyleTable.DefaultBackColor;
                                    dgCell.Style.ForeColor = Color.Gray;
                                }
                                else if (dgCell.ColumnIndex == 0)
                                {
                                    dgCell.Style.BackColor = FileUIStyleTable.DefaultBackColor;
                                    dgCell.Style.ForeColor = FileUIStyleTable.DefaultForeColor;
                                }
                            }
                        }
                        else
                        {
                            foreach (DataGridViewCell dgCell in dgrow.Cells)
                            {
                                if (dgCell.ColumnIndex != 0)
                                {
                                    dgCell.Style.BackColor = FileUIStyleTable.DefaultBackColor;
                                    dgCell.Style.ForeColor = FileUIStyleTable.DefaultForeColor;
                                }
                                else if (dgCell.ColumnIndex == 0)
                                {
                                    dgCell.Style.BackColor = FileUIStyleTable.DefaultBackColor;
                                    dgCell.Style.ForeColor = FileUIStyleTable.DefaultForeColor;
                                }
                            }
                        }
                    }
                }
            }
        }
		#region<数字ボタン：ステップ数(1から100)▲▼：加算/減算>
		private int _startCount = 0;  //
		private int _endCount = 0;  //
		/// <summary>
		/// 数字ボタン：▲▼：加算/減算
		/// </summary>
		/// <param name="value"></param>
		private void valueUpDn( int value )
		{   //カレントセル + とび先インデックス
			int rowIndex = _dgList.CurrentCell.RowIndex + value;
			//とび先インデックスが0以下
			if( rowIndex < _startCount ) rowIndex = 0;
			//rowIndexが範囲を超えた場合
			if( rowIndex >= _startCount + _endCount ) {
				rowIndex = _startCount + _endCount - 1;
			}
			//指定行へセル移動
			cellRowMove( rowIndex );
			if( rowIndex < 0 ) rowIndex = 0;
			//if( rowIndex > trackBar1.Maximum ) rowIndex = trackBar1.Maximum;
			//トラックバーの移動
			//setTrackbar( rowIndex );
		}
		/// <summary>
		/// 数字ボタン：▲100をクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_100Up_Click( object sender, EventArgs e )
		{
			valueUpDn( -100 );
		}
		/// <summary>
		/// 数字ボタン：▲10をクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_10Up_Click( object sender, EventArgs e )
		{
			valueUpDn( -10 );
		}
		/// <summary>
		/// 数字ボタン：▼10をクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_10Down_Click( object sender, EventArgs e )
		{
			valueUpDn( 10 );
		}
		/// <summary>
		/// 数字ボタン：▼100をクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_100Down_Click( object sender, EventArgs e )
		{
			valueUpDn( 100 );
		}
		#endregion
		#region<トラックバー>
		/// <summary>
		/// トラックバー移動時
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/*private void trackBar1_Scroll( object sender, EventArgs e )
		{
			int TrackValNow = ( (TrackBar)sender ).Value;
			//トラックバー位置からスクロール位置に変換
			//int dspVal = changeTrackPosToScPos( TrackValNow );

			if( dspVal < _startCount ) dspVal = 0;
			if( dspVal >= _endCount-1 ) dspVal = dspVal - 2;

			//とび先の番号セルがnullの場合、選択セル、検索番号、トラックバーを先頭にする
			if( _dgList[0, dspVal].Value == null ) {
				int intVal = 0;//先頭：
				//選択セル移動
				cellRowMove( intVal );
				//トラックバーの移動
				//setTrackbar( intVal );
				return;
			}
			//指定行へセル移動
			cellRowMove( dspVal );
		}*/
		/// <summary>
		/// トラックバー位置をスクロール位置に変換
		/// </summary>
		/// <param name="trackVal"></param>
		//private int changeTrackPosToScPos( int trackVal )
		//{
		//	return trackBar1.Maximum - trackVal;//SCと逆
		//}
		/// <summary>
		/// トラックバーの設定：
		/// </summary>
		/// <param name="trackVal"></param>
		/// <returns>スクロールバー位置</returns>	
		//private int setTrackbar( int trackVal )
		//{
		//	return trackBar1.Value = changeTrackPosToScPos( trackVal );//トラックバー位置をスクロール位置に変換
		//}
		/// <summary>
		/// 指定行セルに移動
		/// </summary>
		/// <param name="rowIndex"></param>
		private void cellRowMove( int moveRowIndex )
		{
			if( _dgList.CurrentCell == null ) return;
			int rowIndex = _dgList.CurrentCell.RowIndex;//行
			int colIndex = _dgList.CurrentCell.ColumnIndex;//列
			if( moveRowIndex < 0 ) moveRowIndex = 0;
			//if( moveRowIndex > trackBar1.Maximum-2 ) moveRowIndex = trackBar1.Maximum-2;//

			//一時的に選択を非表示　※ちらつき防止
			_dgList.Rows[rowIndex].Selected = false;
			//指定行へセル移動
			_dgList.CurrentCell = _dgList[colIndex, moveRowIndex];
			//選択を表示
			_dgList.Rows[moveRowIndex].Selected = true;
		}
        #endregion

        private void _LockBtn_MouseUp(object sender, MouseEventArgs e)
        {

            _dgList.SetRedraw(true);
            //書込み保護範囲設定、コマンド選択
            ProtectCommandsDialog.ShowSubForm(this, "仮想点保護設定",
                                                ProtectParamCategory.VirtualPositions,
                                                sender as ButtonEx,
                                                (_dgList.SelectedCells.Count != 0) ? _dgList.SelectedCells[0].RowIndex + 1 : 1);
            //書き込み保護をリスト表示に適用する。
            SetEditableRowColor();

            _dgList.SetRedraw(false);


            _dgList.Refresh();//表示更新：不具合表192
            Refresh();
        }
        /// <summary>
        /// 整数の座標値を有効桁数によって変化する文字列に変換する。
        /// </summary>
        /// <param name="position">座標値</param>
        /// <returns>座標値の文字列</returns>
        private string PositionToString(int position)
        {
            switch(_digit)
            {
                case DigitSelect.Three: return position.ToString("###0'.'000");
                case DigitSelect.Four:  return (position * 10).ToString("###0'.'0000");
                default: return "0.000";
            }
        }

        private void _CloseBtn_Click(object sender, EventArgs e)
        {
            _threadStop = true;
        }
    }
}
///ECNC2VB参考ソース
///

////**************************************************************************
////　１０項
////		Title			: 機械原点、WORK原点、仮想点／確認点復帰動作
////
////		Function Name	: CNC_ORG_MOVE
////
////			Input		: int hCom 通信ﾊﾝﾄﾞﾙ番号
////                        LPVOID OrgMovePrm 復帰動作ﾊﾟﾗﾒｰﾀ構造体ﾎﾟｲﾝﾀ
////			Output		: なし
////			Return		:  0 　 　正常終了
////						:  -1   　異常終了
////
////			Description	: 以下の動作を行う。
////								機械原点復帰
////								WORK原点復帰
////								仮想点１～９、確認点復帰
////
////		Rev1.38	06.10.25	M.Karasaki	C2軸追加対応
////		Rev1.28	03.02.15	H.Suzuki	SPX管理仮想点100件増対応
////		Rev　2000.11.22 New A.Okabe
////												
////**************************************************************************
//DLL_EXPORT int CCONV CNC_ORG_MOVE(int hCom, ORGMOVEFLAG* inOrgMovePrm)
//{
//    //	ORGMOVEFLAG *inOrgMovePrm;		/* 引数を格納する */
//    ZRNSTART SetZrnStart;
//    PTPASTART SetPtpABStart;
//    APL_VIRPOS RefPoint;
//    RETDEF RetInfo;
//    long l;
//    int ret, i, j;

//    //<--2009/03/30 ADD ishide Start
//    int iRet = 0;
//    BYTE bParam = 0x7;  //X,Y,W=bit On
//    STATUS GetStatus;
//    unsigned long size;
//    //-->2009/03/30 ADD ishide End


//    //	inOrgMovePrm = (ORGMOVEFLAG *)OrgMovePrm;	/* LPVOIDを型変換をする	*/

//    l = inOrgMovePrm->MovePrm;
//    ret = 0;
//    SeqNo = 0;
//    switch (l)
//    {
//        case 0:         /* 全軸原点復帰		*/
//            SeqNo = 1;
//            ret = SendCommand(hCom, REQ_ALLZRN, NULL);              /* CALL SPXDLL Function	*/
//            break;
//        case 1:         /* 任意軸原点復帰	*/
//            SeqNo = 2;
//            SetZrnStart.AxisFlag = (unsigned short)inOrgMovePrm->Action;
//            ret = SendCommand(hCom, REQ_ZRNSTART, &SetZrnStart);    /* CALL SPXDLL Function	*/
//            break;
//        case 2:         /* ﾜｰｸ原点復帰		*/
//                        // '01.04.05
//                        //SetPtpABStart.AxisFlag = 0x1ff;						/* １～９軸指定			*/
//                        //
//            SeqNo = 3;
//            //SetPtpABStart.AxisFlag = 0x37;						// １b00：Ｘ軸		//---DEL 061025 v1.38
//            //														// ２b01：Ｙ
//            //														// ３b02：Ｗ
//            //														// ４b03：Ｚ
//            //														// ５b04：Ａ
//            //														// ６b05：Ｂ
//            //														// ７b06：－
//            //														// ８b07：Ｉ（ＡＥＣ）
//            //														// ９b08：－

//            SetPtpABStart.AxisFlag = 0x77;                          // １b00：Ｘ軸		//---ADD 061025 v1.38
//                                                                    // ２b01：Ｙ
//                                                                    // ３b02：Ｗ
//                                                                    // ４b03：Ｚ
//                                                                    // ５b04：Ａ
//                                                                    // ６b05：Ｂ
//                                                                    // ７b06：Ｃ
//                                                                    // ８b07：Ｉ（ＡＥＣ）
//                                                                    // ９b08：－
//            for (i = 0; i < 9; i++)
//            {
//                SetPtpABStart.PosAxis[i] = 0;
//            }
//            ret = SendCommand(hCom, REQ_PTPASTART_W, &SetPtpABStart);/* CALL SPXDLL Function */
//            break;
//        case 3:         /* 仮想点/測定点復帰*/
//                        // ターゲット座標読み出し
//            SeqNo = 4;

//            // vv +Mod 030215(1.28)
//            RefPoint.Action = 0;            // 0:Read
//            ret = CNC_ORGPOS_RW(hCom, &RefPoint, &RetInfo);
//            if (ret != E$OK)		break;

//            j = inOrgMovePrm->VirAction;    //	移動対象ポイント番号設定
//                                            //	(1:仮想点0...,100:仮想点99,101:測定点100)
//            if ((j <= 0) || (j > 101))
//            {
//                ret = E$PARAM_DLL;
//                break;
//            }
//            // ^^ +Mod 030215(1.28)

//            // vv -Mod 030215(1.28)
//            //		i = 1;
//            //		j = inOrgMovePrm->VirAction;
//            //		j--;
//            //		RefPoint.Action = 0;
//            //		RefPoint.TargetAxis = (long)(i << j);
//            //		ret = CNC_ORGPOS_RW(hCom,&RefPoint,&RetInfo);
//            //		if(ret != E$OK){
//            //			break;	
//            //		}
//            // ^^ -Mod 030215(1.28)

//            //-->2009/03/30 ADD ishide Start
//            ret = ReceiveData(hCom, DAT_STATUS, 0, &size, &GetStatus);  /* CALL SPXDLL Function	*/
//            if (ret != E$OK){   /* もしエラーなら異常で閉じる */
//                bParam = 0x77;
//            }

//            if (iRet = 1)
//            {
//                //正常終了
//                //0 1 2 3 4 5 6
//                //X,Y,W,Z,A,B,C

//                if ((GetStatus.AxisSts[4] & 0x20) == 0)
//                {
//                    //A軸移動無効フラグ未セット ->bitON
//                    bParam += 0x10;
//                }
//                if ((GetStatus.AxisSts[5] & 0x20) == 0)
//                {
//                    //B軸移動無効フラグ未セット ->bitON
//                    bParam += 0x20;
//                }
//                if ((GetStatus.AxisSts[6] & 0x20) == 0)
//                {
//                    //C軸移動無効フラグ未セット ->bitON
//                    bParam += 0x40;
//                }
//            }
//            //<--2009/03/30 ADD ishide End

//            // 移動
//            // '01.04.05
//            //SetPtpABStart.AxisFlag = 0x1ff;
//            //SetPtpABStart.AxisFlag = 0x37;	//DEL 061025 v1.38
//            //-->2009/03/30 chg ishide
//            //SetPtpABStart.AxisFlag = 0x77;		//ADD 061025 v1.38
//            SetPtpABStart.AxisFlag = bParam;

//            for (i = 0; i < 9; i++)
//            {
//                SetPtpABStart.PosAxis[i] = RefPoint.AxPnt[j].Pnt[i];
//            }
//            ret = SendCommand(hCom, REQ_PTPBSTART_W, &SetPtpABStart);/* CALL SPXDLL Function */
//            break;
//        default:        /* パラメータ異常	*/
//            ret = E$PARAM_DLL;
//            break;
//    }
//    if (ret != 0)
//    {
//        ret |= (SeqNo << 16);
//    }
//    return ret;	/* 戻り値をそのまま返す	*/
//}



///************************************************************************/
///*	アンサーステータス情報構造体　										*/
///************************************************************************/
//typedef struct {	/* アンサーステータス情報構造体						*/

//    unsigned long Status;               /* 各種状態情報					*/
//unsigned long Status2;          /* 各種状態情報２				*/
//unsigned long AxisSts[9];           /* 各軸ステータス情報			*/
//unsigned long Alarm;                /* アラーム情報					*/
//unsigned long Alarm2;               /* アラーム情報２				*/

//// Add　01.04.07
//unsigned long Alarm3;               /* アラーム情報３				*/
//unsigned long Alarm4;               /* アラーム情報４				*/

//unsigned long AxisAlm[9];           /* 各軸アラーム情報				*/
//APL_AXISPOS AxisPos[9];         /* 各軸位置情報					*/
//long WTopPos;           /* Ｗ軸上限値					*/
//unsigned char Override;         /* 送りオーバーライド設定		*/
//unsigned char ProgramNo;            /* 選択・実行プログラム番号		*/
//unsigned long StepNo;               /* 待機・実行ブロック番号		*/
//long NNo;               /* 待機・実行N番号				*/
//long LineNo;                /* 待機・実行行番号				*/
//long LineFlg;           /* 待機・実行行番号種別			*/
//                        // ADD 010816(1.6) H.Suzuki
//long CorrAng;           /* 補正角度(8-24ﾋﾞｯﾄ固定小数点)	*/
//                        // del 2001.02.23
//                        //  APL_STPRGSTS	PrgSts;				/* プログラム実行情報データ		*/

//// 2001.03.11 add
//AECSTS AecSts;

//APL_PCONDITION PState;              /* 加工条件情報					*/
//long StartIO;           /* スタートキー状態(0:OFF/1:ON)	*/

//// 2001.03.11 add
//long BuzzerEn;          /* ブザー有効/無効IO出力状態	*/

//// 2001.03.29 add
//SPXIO SpxIOdat;         /* SPX 汎用入出力状態			*/

//// 2001.04.20 add
//SPXCPUINFO SpxCpuDat;			/* ＳＰＸ動作状態情報構造体		*/
//} APL_STATUS;




//    '*****************************************************************
//'SUMMARY    ：機械原点、WORK原点、仮想点／測定点復帰動作
//'PARAMETER  ：lngMovPrm,I,Long,復帰動作種類(0:全軸原点復帰/1:任意軸原点復帰/2:WORK原点復帰/3:仮想点、測定点復帰)
//'           ：Action,I,Long,原点復帰軸フラグ(任意軸同時動作)
//'           ：VirAction,I,Long,仮想点、測定点復帰パラメータ
//'           ：
//'           ：
//'COMMENT    ：各原点への復帰動作を開始する。
//'           ：任意軸原点復帰の際はフラグデータとして渡すため、フラグ
//'           ：データの組み立ても行う。
//'           ：
//'           ：履歴
//'           ： Rev002   03.02.20    H.Suzuki    軸フラグを省略可に変更
//'           ： Rev001   01.02.05    H.Suzuki    New
//'           ：
//'*****************************************************************
//'Public Function DLLOrgMove(lngMovPrm As Long, lngVirAct As Long, strAxisFlag As String) As Long    'Mod 030220
//Public Function DLLOrgMove(lngMovPrm As Long, _
//                            lngVirAct As Long, _
//                            Optional strAxisFlag As String = "") As Long

//Dim Ret As Long
//Dim OrgMovePrm As OrgMoveFlag
//Dim strMsg As String

//With OrgMovePrm
//    '復帰動作種別の分析
//    '任意軸原点復帰を指定した場合、指定軸文字列をフラグデータへ変換する。
//    If lngMovPrm = 1 Then
//        '軸選択フラグなし? Yes--> 終了          'Add 030220
//        If strAxisFlag = "" Then Exit Function

//        'フラグデータへ変換
//        '引数のフォーマット ...  "1,2,3,4,5"(最大11個までカンマ区切り)
//        .lngAction = AxisFlagCnv(strAxisFlag)
//    End If
//    '復帰動作種別セット
//    .lngMovePrm = lngMovPrm
//    '仮想点、測定点復帰パラメータ設定
//    .lngVirAction = lngVirAct
//End With

//'動作開始コマンド発行
//'Ret = CNC_ORG_MOVE(hCom, OrgMovePrm)
//'↓ 2015.04.17 MH8判定フラグを確認し、処理の分岐を実施
//If(gintHeadSpecParam = 0) Or(gintHeadSpecParam = 1) Then
//  Ret = CNC_ORG_MOVE(hCom, OrgMovePrm)
//Else
//    Ret = CNC_ORG_MOVE_MH8(hCom, OrgMovePrm)
//End If
//'↑

//'戻り値判断 エラーの場合メッセージを表示        '---ADD 010424
//If Ret Then
//    strMsg = ""
//    strMsg = strMsg + vbNewLine
//    strMsg = strMsg + "(CNC_ORG_MOVE;" + CStr(Ret) + ")"
//    'メッセージ表示(107:実行不可)
//    Call InfoMsgShow(107, vbOKOnly, strMsg)
//End If

//'戻り値渡し
//DLLOrgMove = Ret

//End Function



//'↓2015/05/08 K.Asanuma Add アンサーステータス取得分岐
//If(gintHeadSpecParam = 0) Or(gintHeadSpecParam = 1) Then
//    'シーケンス完了フラグ(True)、スピンドル(回転)(False)、ポンプ(False)、放電(False)判定
//    If DLLStsBitExt(AnsSts.lngStatus2, SPX_STS2_SEQEND) And(AnsSts.PState.lngPumpOut = False) And _
//       (AnsSts.PState.lngSpinOut = False) And(bolDisChgON = False) Then
//    '^^^ 2013.03.04 add watanabe 条件変更----------------------------------------

//        'HPモード？
//        If bolHP_MODEEn Then    'HPモード
//            'スタートボタン無効→監視しない
//            bolStartIO_f = False    'スタートボタンフラグリセット
//        Else    '通常モード
//            'スタートボタン押下?
//            If StartBTNChk() Then
//                'すでに押下済み?
//                If bolStartIO_f Then
//                    'Yes-->何もしない
//                Else
//                    'フラグセット -->軸移動処理へ
//                    bolStartIO_f = True
//                    GoTo timStartWait_Move
//                End If
//            Else
//                'スタートボタンフラグリセット
//                bolStartIO_f = False
//            End If
//        End If
//    End If
//Else
//    'シーケンス完了フラグ(True)、スピンドル(回転)(False)、ポンプ(False)、放電(False)判定
//    If DLLStsBitExt(AnsSts_MH8.lngStatus2, SPX_STS2_SEQEND) And(AnsSts_MH8.PState.lngPumpOut = False) And _
//       (AnsSts_MH8.PState.lngSpinOut = False) And(bolDisChgON = False) Then
//    '^^^ 2013.03.04 add watanabe 条件変更----------------------------------------

//        'HPモード？
//        If bolHP_MODEEn Then    'HPモード
//            'スタートボタン無効→監視しない
//            bolStartIO_f = False    'スタートボタンフラグリセット
//        Else    '通常モード
//            'スタートボタン押下?
//            If StartBTNChk() Then
//                'すでに押下済み?
//                If bolStartIO_f Then
//                    'Yes-->何もしない
//                Else
//                    'フラグセット -->軸移動処理へ
//                    bolStartIO_f = True
//                    GoTo timStartWait_Move
//                End If
//            Else
//                'スタートボタンフラグリセット
//                bolStartIO_f = False
//            End If
//        End If
//    End If
//End If
//'↑

//'定時処理へJump
//GoTo timStartWait_MoveSkip

//timStartWait_Move:
//'---

