using ECNC3.Enumeration;
using ECNC3.Models;
using ECNC3.Models.McIf;
using ECNC3.Views.Popup;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace ECNC3.Views
{
    
    public partial class ParameterViewForm : ECNC3Form
    {
		/// <summary>ポップアップテンキー</summary>
		private TenKeyDialog _popupTenkey = new TenKeyDialog( "", 0, 0, 0 );//初回インスタンスを作っておく

		public ParameterViewForm()
        {
            InitializeComponent();
            
        }
        #region VariableMember
        private string[] _tableNames;
        UIDatInitialParam datini = null;
        UIDatServoPrm datsrv = null;
        PrmEditDlg editDlg = null;
        #endregion

        #region Initialize
        private void _InitializeParameterList()
        {
            _tableNames = new string[]{
                "Cap_No.",
                "SF02_Cap_No.",
                "Ip_No.",
                "SF02_Ip_No.",
                "OverRide_No.",
                "GCodeDisable_G",
                "MCodeDisable_M"
            };

            //座標表示初期化
            dataGridViewEx1.Initialize(12.0F, 30, true);

            DataGridViewColumn _numberCol = new DataGridViewColumn();
            _numberCol.CellTemplate = new DataGridViewTextBoxCell();
            _numberCol.Name = "NumberCol";
            _numberCol.HeaderText = "カテゴリ";
            _numberCol.Width = 90;
            DataGridViewColumn _dataCol = new DataGridViewColumn();
            _dataCol.CellTemplate = new DataGridViewTextBoxCell();
            _dataCol.Name = "VariableCol";
            _dataCol.HeaderText = "パラメーター名";
            _dataCol.Width = 300;
            DataGridViewColumn _commentCol = new DataGridViewColumn();
            _commentCol.CellTemplate = new DataGridViewTextBoxCell();
            _commentCol.Name = "CommentCol";
            _commentCol.HeaderText = "値";
            _commentCol.Width = 130;
            DataGridViewColumn _valUppCol = new DataGridViewColumn();
            _valUppCol.CellTemplate = new DataGridViewTextBoxCell();
            _valUppCol.Name = "ValUppCol";
            _valUppCol.HeaderText = "上限値";
            _valUppCol.Width = 130;
            DataGridViewColumn _valLowCol = new DataGridViewColumn();
            _valLowCol.CellTemplate = new DataGridViewTextBoxCell();
            _valLowCol.Name = "ValLowCol";
            _valLowCol.HeaderText = "下限値";
            _valLowCol.Width = 130;
            dataGridViewEx1.Columns.Add(_numberCol);
            dataGridViewEx1.Columns.Add(_dataCol);
            dataGridViewEx1.Columns.Add(_commentCol);
            dataGridViewEx1.Columns.Add(_valLowCol);            
            dataGridViewEx1.Columns.Add(_valUppCol);            
            dataGridViewEx1.AutoSize = false;
            dataGridViewEx1.ColumnHeadersVisible = true;
            dataGridViewEx1.DefaultCellStyle.SelectionBackColor = FileUIStyleTable.EnabledBackColor;
            dataGridViewEx1.DefaultCellStyle.SelectionForeColor = FileUIStyleTable.EnabledForeColor;
            dataGridViewEx1.InitCol("NumberCol", 12.0F, DataGridViewContentAlignment.MiddleCenter, typeof(string));
            dataGridViewEx1.InitCol("VariableCol", 12.0F, DataGridViewContentAlignment.MiddleLeft, typeof(string));
            dataGridViewEx1.InitCol("CommentCol", 12.0F, DataGridViewContentAlignment.MiddleRight, typeof(string));
            dataGridViewEx1.InitCol("ValLowCol", 12.0F, DataGridViewContentAlignment.MiddleRight, typeof(string));
			dataGridViewEx1.InitCol( "ValUppCol", 12.0F, DataGridViewContentAlignment.MiddleRight, typeof( string ) );
			dataGridViewEx1.RowCount = 500;
            dataGridViewEx1.ReadOnly = false;
            dataGridViewEx1.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
        }
        #endregion

        #region UpdateValueMembers
        private ResultCodes UpdateInitialPrm()
        {
            DataGridView_ClearCells();
            ResultCodes ret = ResultCodes.NotFound;
            using (Models.UIParamTable PrmTbl = new Models.UIParamTable())
            {
                int rowCt = 0;
                datini = new UIDatInitialParam();
                datini.Read();
                PrmTbl.Read(Models.ParamCategory.INITIAL);
                foreach (string _data in datini.InitialParamDataList)
                {
                    dataGridViewEx1[2, rowCt].Value = _data;
                    rowCt++;
                }
                rowCt = 0;
                foreach(Models.StructureParamTableItem _initialInfo in PrmTbl.Items)
                {
                    dataGridViewEx1[1, rowCt].Value = _initialInfo.ViewName;
                    dataGridViewEx1[3, rowCt].Value = _initialInfo.Lower;
                    dataGridViewEx1[4, rowCt].Value = _initialInfo.Upper;
                    rowCt++;
                }
            }
                return ret;
        }
        private ResultCodes UpdateServoPrm()
        {
            DataGridView_ClearCells();
            ResultCodes ret = ResultCodes.NotFound;
            datsrv = new UIDatServoPrm();
            using (Models.UIParamTable PrmTbl = new Models.UIParamTable())
            {
                //カウンタセット　パラメータ読み込み準備
                int rowCt = 0;
                datsrv.Read();
                PrmTbl.Read(Models.ParamCategory.SERVO);

                for(int listCt = 0; listCt < UIDatServoPrm.listCtUpper; listCt++)
                {
                    dataGridViewEx1[2, rowCt].Value = datsrv.ServoParamDataList.Items[listCt].InPos; rowCt++;
                    dataGridViewEx1[2, rowCt].Value = datsrv.ServoParamDataList.Items[listCt].ErMax; rowCt++;
                    dataGridViewEx1[2, rowCt].Value = datsrv.ServoParamDataList.Items[listCt].Ka; rowCt++;
                    dataGridViewEx1[2, rowCt].Value = datsrv.ServoParamDataList.Items[listCt].SKa; rowCt++;
                    dataGridViewEx1[2, rowCt].Value = datsrv.ServoParamDataList.Items[listCt].Dx; rowCt++;
                    dataGridViewEx1[2, rowCt].Value = datsrv.ServoParamDataList.Items[listCt].PtpFeed; rowCt++;
                    dataGridViewEx1[2, rowCt].Value = datsrv.ServoParamDataList.Items[listCt].JogFeed; rowCt++;
                    dataGridViewEx1[2, rowCt].Value = datsrv.ServoParamDataList.Items[listCt].SoftLimP; rowCt++;
                    dataGridViewEx1[2, rowCt].Value = datsrv.ServoParamDataList.Items[listCt].SoftLimM; rowCt++;
                    dataGridViewEx1[2, rowCt].Value = datsrv.ServoParamDataList.Items[listCt].OrgDir; rowCt++;
                    dataGridViewEx1[2, rowCt].Value = datsrv.ServoParamDataList.Items[listCt].OrgOfs; rowCt++;
                    dataGridViewEx1[2, rowCt].Value = datsrv.ServoParamDataList.Items[listCt].OrgPos; rowCt++;
                    dataGridViewEx1[2, rowCt].Value = datsrv.ServoParamDataList.Items[listCt].OrgFeed; rowCt++;
                    dataGridViewEx1[2, rowCt].Value = datsrv.ServoParamDataList.Items[listCt].AprFeed; rowCt++;
                    dataGridViewEx1[2, rowCt].Value = datsrv.ServoParamDataList.Items[listCt].SrchFeed; rowCt++;
                    dataGridViewEx1[2, rowCt].Value = datsrv.ServoParamDataList.Items[listCt].OrgPri; rowCt++;
                    dataGridViewEx1[2, rowCt].Value = datsrv.ServoParamDataList.Items[listCt].Homepos; rowCt++;
                    dataGridViewEx1[2, rowCt].Value = datsrv.ServoParamDataList.Items[listCt].Homepri; rowCt++;
                    dataGridViewEx1[2, rowCt].Value = datsrv.ServoParamDataList.Items[listCt].BackL; rowCt++;
                    dataGridViewEx1[2, rowCt].Value = datsrv.ServoParamDataList.Items[listCt].Revise; rowCt++;
                    dataGridViewEx1[2, rowCt].Value = datsrv.ServoParamDataList.Items[listCt].OrgCsetOfs; rowCt++;
                    dataGridViewEx1[2, rowCt].Value = datsrv.ServoParamDataList.Items[listCt].handle_max; rowCt++;
                    dataGridViewEx1[2, rowCt].Value = datsrv.ServoParamDataList.Items[listCt].handle_ka; rowCt++;
                    dataGridViewEx1[2, rowCt].Value = datsrv.ServoParamDataList.Items[listCt].PrcsKa; rowCt++;
                    dataGridViewEx1[2, rowCt].Value = datsrv.ServoParamDataList.Items[listCt].AecSoftLimP; rowCt++;
                    dataGridViewEx1[2, rowCt].Value = datsrv.ServoParamDataList.Items[listCt].AecSoftLimM; rowCt++;
                }
                dataGridViewEx1[2, rowCt].Value = datsrv.ServoParamDataList.AxAecSoftLim;
                //カウンタリセット　パラメーターINFO読み込み準備
                rowCt = 0;
                foreach(Models.StructureParamTableItem _servoInfo in PrmTbl.Items)
                {
                    dataGridViewEx1[1, rowCt].Value = _servoInfo.ViewName;
                    dataGridViewEx1[3, rowCt].Value = _servoInfo.Lower;
                    dataGridViewEx1[4, rowCt].Value = _servoInfo.Upper;
                    rowCt++;
                }
            }
            return ret;
        }
        private ResultCodes UpdateCRSTbl()
        {
            DataGridView_ClearCells();
            ResultCodes ret = ResultCodes.NotFound;
            using (Models.UIParamTable PrmTbl = new Models.UIParamTable())
            {
                int rowCt = 0;
                datini = new UIDatInitialParam();
                datini.Read();
                PrmTbl.Read(Models.ParamCategory.INITIAL);
                foreach(int _data in datini.CRS10DATable)
                {
                    dataGridViewEx1[2, rowCt].Value = _data.ToString();
                    rowCt++;
                }
                foreach (int _data in datini.CRSDATable)
                {
                    dataGridViewEx1[2, rowCt].Value = _data.ToString();
                    rowCt++;
                }
                rowCt = 0;
                foreach (Models.StructureParamTableItem _initialInfo in PrmTbl.Items)
                {
                    if(_initialInfo.ViewName.Contains("CRS値")
                        && _initialInfo.ViewName.Contains("テーブル"))
                    {
                        dataGridViewEx1[1, rowCt].Value = _initialInfo.ViewName;
                        dataGridViewEx1[3, rowCt].Value = _initialInfo.Lower;
                        dataGridViewEx1[4, rowCt].Value = _initialInfo.Upper;
                        rowCt++;
                    }                    
                }
            }
            return ret;
        }
        private ResultCodes UpdateServoPCondTbl()
        {
            DataGridView_ClearCells();
            ResultCodes ret = ResultCodes.NotFound;
            using (Models.UIParamTable PrmTbl = new Models.UIParamTable())
            {
                int rowCt = 0;
                datini = new UIDatInitialParam();
                datini.Read();
                PrmTbl.Read(Models.ParamCategory.INITIAL);
                //foreach (int _data in datini.SfrFrTable)
                //{
                //    dataGridViewEx1[2, rowCt].Value = _data.ToString();
                //    rowCt++;
                //}
                //foreach (int _data in datini.SfrBkTable)
                //{
                //    dataGridViewEx1[2, rowCt].Value = _data.ToString();
                //    rowCt++;
                //}
                rowCt = 0;
                foreach (Models.StructureParamTableItem _initialInfo in PrmTbl.Items)
                {
                    if (_initialInfo.ViewName.Contains("加工サーボ")
                        && _initialInfo.ViewName.Contains("テーブル"))
                    {
                        dataGridViewEx1[1, rowCt].Value = _initialInfo.ViewName;
                        dataGridViewEx1[3, rowCt].Value = _initialInfo.Lower;
                        dataGridViewEx1[4, rowCt].Value = _initialInfo.Upper;
                        rowCt++;
                    }
                }
            }
            return ret;
        }
        private void UpdateCapTable()
        {
            DataGridView_ClearCells();
            using (Models.FileProcessConditionParameter PcondTbl = new Models.FileProcessConditionParameter())
            {
                int rowCt = 0;
                PcondTbl.Read();
                foreach (Models.StructureDataItem item in PcondTbl.Caps)
                {
                    if(rowCt != 0
                    && item.Value == 0
                      )
                    {
                        continue;
                    }
                    dataGridViewEx1[1, rowCt].Value = _tableNames[0] + item.Number.ToString();
                    dataGridViewEx1[2, rowCt].Value = item.Value;
                    dataGridViewEx1[3, rowCt].Value = "0";
                    dataGridViewEx1[4, rowCt].Value = "1.1";
                    rowCt++;
                }
                //foreach (Models.StructureDataItem item in PcondTbl.Caps)
                //{
                //    if (rowCt != 0
                //    && item.SF02 == 0
                //      )
                //    {
                //        continue;
                //    }
                //    dataGridViewEx1[1, rowCt].Value = _tableNames[1] + item.Number.ToString();
                //    dataGridViewEx1[2, rowCt].Value = item.SF02;
                //    rowCt++;
                //}
            }
        }
        private void UpdateIpTable()
        {
            DataGridView_ClearCells();
            using (Models.FileProcessConditionParameter PcondTbl = new Models.FileProcessConditionParameter())
            {
                int rowCt = 0;
                PcondTbl.Read();
                foreach (Models.StructureDataItem item in PcondTbl.Ips)
                {
                    if (rowCt != 0
                    && item.Value == 0
                      )
                    {
                        continue;
                    }
                    dataGridViewEx1[1, rowCt].Value = _tableNames[2] + item.Number.ToString();
                    dataGridViewEx1[2, rowCt].Value = item.Value;
                    dataGridViewEx1[3, rowCt].Value = "3";
                    dataGridViewEx1[4, rowCt].Value = "57";
                    rowCt++;
                }
                foreach (Models.StructureDataItem item in PcondTbl.SFIps)
                {
                    if (rowCt != 0
                    && item.Value == 0
                      )
                    {
                        continue;
                    }
                    dataGridViewEx1[1, rowCt].Value = _tableNames[3] + item.Number.ToString();
                    dataGridViewEx1[2, rowCt].Value = item.Value;
                    dataGridViewEx1[3, rowCt].Value = "0.010";
                    dataGridViewEx1[4, rowCt].Value = "2.560";
                    rowCt++;
                }
            }
        }
        private void UpdateOverRideTable()
        {
            DataGridView_ClearCells();
            using (Models.FileProcessConditionParameter PcondTbl = new Models.FileProcessConditionParameter())
            {
                int rowCt = 0;
                PcondTbl.Read();
                foreach (Models.StructureDataItem item in PcondTbl.OverRides)
                {
                    if (rowCt != 0
                    && item.Value == 0
                      )
                    {
                        continue;
                    }
                    dataGridViewEx1[1, rowCt].Value = _tableNames[4] + item.Number.ToString();
                    dataGridViewEx1[2, rowCt].Value = item.Value;
                    dataGridViewEx1[3, rowCt].Value = "0";
                    dataGridViewEx1[4, rowCt].Value = "200";
                    rowCt++;
                }
            }
        }
        private void UpdateGMCodeDisable()
        {
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
                foreach(XmlNode mcditem in mcdList)
                {
                    mcdValue[int.Parse(mcditem.Attributes[0].Value)] = byte.Parse(mcditem.Attributes[1].Value);
                }

                //リスト表示
                int totalCt = 0;
                for(int index = 0; index < gcdValue.Length; index++, totalCt++)
                {
                    dataGridViewEx1[1, totalCt].Value = _tableNames[5] + index.ToString("00");
                    dataGridViewEx1[2, totalCt].Value = gcdValue[index].ToString();
                    dataGridViewEx1[3, totalCt].Value = "0";
                    dataGridViewEx1[4, totalCt].Value = "1";
                }
                for (int index = 0; index < gcdValue.Length; index++, totalCt++)
                {
                    dataGridViewEx1[1, totalCt].Value = _tableNames[6] + index.ToString("00");
                    dataGridViewEx1[2, totalCt].Value = mcdValue[index].ToString();
                    dataGridViewEx1[3, totalCt].Value = "0";
                    dataGridViewEx1[4, totalCt].Value = "1";
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
        #endregion

        #region SaveValueMembers
        private ResultCodes SaveInitialPrm()
        {
            ECNC3.Enumeration.ResultCodes writeResult = Enumeration.ResultCodes.Success;
            //RTMC64ECの動作モードを「Setting」にする。
            using (McReqModeChange ReqModeChg = new McReqModeChange())
            {
                ReqModeChg.TaskMode = Enumeration.McTaskModes.Setting;
                if (ReqModeChg.Execute() != Enumeration.ResultCodes.Success)
                {
                    //動作モード変更に失敗したら有効軸切替を行わない。
                    return writeResult;
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
            
            for (int ict = 0; ict < datini.InitialParamDataList.Count; ict++)
            {
                try
                {
                    datini.InitialParamDataList[ict] = dataGridViewEx1[2, ict].Value.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "/r/n" + "ict= " + ict.ToString());
                }
                
            }
            writeResult = datini.WriteAll();
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
            return writeResult;
        }
        private ResultCodes SaveServoPrm()
        {
            ECNC3.Enumeration.ResultCodes writeResult = Enumeration.ResultCodes.Success;
            //RTMC64ECの動作モードを「Setting」にする。
            using (McReqModeChange ReqModeChg = new McReqModeChange())
            {
                ReqModeChg.TaskMode = Enumeration.McTaskModes.Setting;
                if (ReqModeChg.Execute() != Enumeration.ResultCodes.Success)
                {
                    //動作モード変更に失敗したら有効軸切替を行わない。
                    return writeResult;
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
            int gridCt = 0;
            for (int listCt = 0; listCt < UIDatServoPrm.listCtUpper; listCt++)
            {
                datsrv.ServoParamDataList.Items[listCt].InPos = dataGridViewEx1[2, gridCt].Value.ToString(); gridCt++;
                datsrv.ServoParamDataList.Items[listCt].ErMax = dataGridViewEx1[2, gridCt].Value.ToString(); gridCt++;
                datsrv.ServoParamDataList.Items[listCt].Ka = dataGridViewEx1[2, gridCt].Value.ToString(); gridCt++;
                datsrv.ServoParamDataList.Items[listCt].SKa = dataGridViewEx1[2, gridCt].Value.ToString(); gridCt++;
                datsrv.ServoParamDataList.Items[listCt].Dx = dataGridViewEx1[2, gridCt].Value.ToString(); gridCt++;
                datsrv.ServoParamDataList.Items[listCt].PtpFeed = dataGridViewEx1[2, gridCt].Value.ToString(); gridCt++;
                datsrv.ServoParamDataList.Items[listCt].JogFeed = dataGridViewEx1[2, gridCt].Value.ToString(); gridCt++;
                datsrv.ServoParamDataList.Items[listCt].SoftLimP = dataGridViewEx1[2, gridCt].Value.ToString(); gridCt++;
                datsrv.ServoParamDataList.Items[listCt].SoftLimM = dataGridViewEx1[2, gridCt].Value.ToString(); gridCt++;
                datsrv.ServoParamDataList.Items[listCt].OrgDir = dataGridViewEx1[2, gridCt].Value.ToString(); gridCt++;
                datsrv.ServoParamDataList.Items[listCt].OrgOfs = dataGridViewEx1[2, gridCt].Value.ToString(); gridCt++;
                datsrv.ServoParamDataList.Items[listCt].OrgPos = dataGridViewEx1[2, gridCt].Value.ToString(); gridCt++;
                datsrv.ServoParamDataList.Items[listCt].OrgFeed = dataGridViewEx1[2, gridCt].Value.ToString(); gridCt++;
                datsrv.ServoParamDataList.Items[listCt].AprFeed = dataGridViewEx1[2, gridCt].Value.ToString(); gridCt++;
                datsrv.ServoParamDataList.Items[listCt].SrchFeed = dataGridViewEx1[2, gridCt].Value.ToString(); gridCt++;
                datsrv.ServoParamDataList.Items[listCt].OrgPri = dataGridViewEx1[2, gridCt].Value.ToString(); gridCt++;
                datsrv.ServoParamDataList.Items[listCt].Homepos = dataGridViewEx1[2, gridCt].Value.ToString(); gridCt++;
                datsrv.ServoParamDataList.Items[listCt].Homepri = dataGridViewEx1[2, gridCt].Value.ToString(); gridCt++;
                datsrv.ServoParamDataList.Items[listCt].BackL = dataGridViewEx1[2, gridCt].Value.ToString(); gridCt++;
                datsrv.ServoParamDataList.Items[listCt].Revise = dataGridViewEx1[2, gridCt].Value.ToString(); gridCt++;
                datsrv.ServoParamDataList.Items[listCt].OrgCsetOfs = dataGridViewEx1[2, gridCt].Value.ToString(); gridCt++;
                datsrv.ServoParamDataList.Items[listCt].handle_max = dataGridViewEx1[2, gridCt].Value.ToString(); gridCt++;
                datsrv.ServoParamDataList.Items[listCt].handle_ka = dataGridViewEx1[2, gridCt].Value.ToString(); gridCt++;
                datsrv.ServoParamDataList.Items[listCt].PrcsKa = dataGridViewEx1[2, gridCt].Value.ToString(); gridCt++;
                datsrv.ServoParamDataList.Items[listCt].AecSoftLimP = dataGridViewEx1[2, gridCt].Value.ToString(); gridCt++;
                datsrv.ServoParamDataList.Items[listCt].AecSoftLimM = dataGridViewEx1[2, gridCt].Value.ToString(); gridCt++;
            }
            datsrv.ServoParamDataList.AxAecSoftLim = dataGridViewEx1[2, gridCt].Value.ToString();
            writeResult = datsrv.WriteAll();
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
            return writeResult;
        }
        private ResultCodes SaveCRSTbl()
        {
            ECNC3.Enumeration.ResultCodes writeResult = Enumeration.ResultCodes.Success;
            //RTMC64ECの動作モードを「Setting」にする。
            using (McReqModeChange ReqModeChg = new McReqModeChange())
            {
                ReqModeChg.TaskMode = Enumeration.McTaskModes.Setting;
                if (ReqModeChg.Execute() != Enumeration.ResultCodes.Success)
                {
                    //動作モード変更に失敗したら有効軸切替を行わない。
                    return writeResult;
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
            int totalCt = 0;
            for (int ict = 0; ict < datini.CRS10DATable.Count; ict++)
            {
                datini.CRS10DATable[ict] = int.Parse(dataGridViewEx1[2, totalCt].Value.ToString());
                totalCt++;
            }
            for (int ict = 0; ict < datini.CRSDATable.Count; ict++)
            {
                datini.CRSDATable[ict] = int.Parse(dataGridViewEx1[2, totalCt].Value.ToString());
                totalCt++;
            }
            writeResult = datini.WriteAll();
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
            return writeResult;
        }
        private ResultCodes SaveServoPCondTbl()
        {
            ECNC3.Enumeration.ResultCodes writeResult = Enumeration.ResultCodes.Success;
            //RTMC64ECの動作モードを「Setting」にする。
            using (McReqModeChange ReqModeChg = new McReqModeChange())
            {
                ReqModeChg.TaskMode = Enumeration.McTaskModes.Setting;
                if (ReqModeChg.Execute() != Enumeration.ResultCodes.Success)
                {
                    //動作モード変更に失敗したら有効軸切替を行わない。
                    return writeResult;
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
            //int iTotalCt = 0;
            //for (int ict = 0; ict < datini.SfrFrTable.Count; ict++, iTotalCt++)
            //{
            //    datini.SfrFrTable[ict] = int.Parse(dataGridViewEx1[2, iTotalCt].Value.ToString());
            //}
            //for (int ict = 0; ict < datini.SfrBkTable.Count; ict++, iTotalCt++)
            //{
            //    datini.SfrBkTable[ict] = int.Parse(dataGridViewEx1[2, iTotalCt].Value.ToString());
            //}
            writeResult = datini.WriteAll();
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
            return writeResult;
        }
        private ResultCodes SaveIpTbl()
        {
            ResultCodes ret = ResultCodes.Success;
            using (FileProcessConditionParameter PcondTbl = new FileProcessConditionParameter())
            {
                foreach (DataGridViewRow row in dataGridViewEx1.Rows)
                {
                    if (row.Cells[1].Value == null
                        || !row.Cells[1].Value.ToString().Contains("Ip")) continue;
                    StructureDataItem data = new StructureDataItem();
                    string name = ((string)(row.Cells[1].Value));
                    if (!name.Contains(_tableNames[3]))
                    {
                        data.Number = int.Parse(name.Replace(_tableNames[2], ""));
                        data.Value = double.Parse(row.Cells[2].Value.ToString());
                        ret = PcondTbl.WriteIp(data);
                    }
                    else
                    {
                        data.Number = int.Parse(name.Replace(_tableNames[3], ""));
                        data.Value = double.Parse(row.Cells[2].Value.ToString());
                        ret = PcondTbl.WriteSFIp(data);
                    }
                }
            }
            return ret;
        }
        private ResultCodes SaveCapTbl()
        {
            ResultCodes ret = ResultCodes.Success;
            using (FileProcessConditionParameter PcondTbl = new FileProcessConditionParameter())
            {
                foreach(DataGridViewRow row in dataGridViewEx1.Rows)
                {
                    if (row.Cells[1].Value == null) continue;
                    StructureDataItem data = new StructureDataItem();
                    string name = ((string)(row.Cells[1].Value));
                    if (name.Contains("Cap"))
                    {
                        //if ((name.Contains("SF02")))
                        //{
                        //    data.Number = int.Parse(name.Replace(_tableNames[1], ""));
                        //}
                        //else
                        //{
                            data.Number = int.Parse(name.Replace(_tableNames[0], ""));
                        //}

                        //if (name.Contains("SF02"))
                        //{
                        //    data.SF02 = double.Parse(row.Cells[2].Value.ToString());
                        //}
                        if (!name.Contains("SF02"))
                        {
                            data.Value = double.Parse(row.Cells[2].Value.ToString());
                        }
                        ResultCodes result = PcondTbl.WriteCap(data);
                        if (result != ResultCodes.Success)
                        {
                            ret = result;
                        }
                    }
                }
            }
            return ret;
        }
        private ResultCodes SaveOverRideTbl()
        {
            ResultCodes ret = ResultCodes.Success;
            using (FileProcessConditionParameter PcondTbl = new FileProcessConditionParameter())
            {
                foreach (DataGridViewRow row in dataGridViewEx1.Rows)
                {
                    if (row.Cells[1].Value == null) continue;
                    StructureDataItem data = new StructureDataItem();
                    string name = ((string)(row.Cells[1].Value));
                    if (name.Contains("OverRide"))
                    {
                        data.Number = int.Parse(name.Replace(_tableNames[4], ""));
                        data.Value = double.Parse(row.Cells[2].Value.ToString());
                        ResultCodes result = PcondTbl.WriteOverRide(data);
                        if (result != ResultCodes.Success)
                        {
                            ret = result;
                        }
                    }
                }
            }
            return ret;
        }
        private ResultCodes SaveGMCodeDisable()
        {
            ResultCodes retResult = ResultCodes.Success;
            using (FileSettings fs = new FileSettings("GMCodeDisable.xml"))
            {
                //書き込み
                fs.Read();
                foreach(DataGridViewRow row in dataGridViewEx1.Rows)
                {
                    if(row.Cells[1].Value != null)
                    {
                        if(row.Cells[1].Value.ToString().Contains("GCodeDisable_G") == true )
                        {
                            if(row.Cells[1].Value.ToString().Contains("GCodeDisable_G0") == true)
                            {
                                fs.WriteAttr("Prog/GCodDsbl/Item", "dsbl", row.Cells[2].Value.ToString()
                                    , int.Parse(row.Cells[1].Value.ToString().Replace("GCodeDisable_G0", "")));
                            }
                            else
                            {
                                fs.WriteAttr("Prog/GCodDsbl/Item", "dsbl", row.Cells[2].Value.ToString()
                                    , int.Parse(row.Cells[1].Value.ToString().Replace("GCodeDisable_G", "")));
                            }
                            ;

                        }
                        else if(row.Cells[1].Value.ToString().Contains("MCodeDisable_M") == true)
                        {
                            if (row.Cells[1].Value.ToString().Contains("MCodeDisable_M0") == true)
                            {
                                fs.WriteAttr("Prog/MCodDsbl/Item", "dsbl", row.Cells[2].Value.ToString()
                                    , int.Parse(row.Cells[1].Value.ToString().Replace("MCodeDisable_M0", "")));
                            }
                            else
                            {
                                fs.WriteAttr("Prog/MCodDsbl/Item", "dsbl", row.Cells[2].Value.ToString()
                                    , int.Parse(row.Cells[1].Value.ToString().Replace("MCodeDisable_M", "")));
                            }
                        }
                    }
                }                
                fs.WriteSimple();
                return retResult;
            }
        }
        #endregion

        #region EventHandler  
        private void ParameterViewForm_Load(object sender, EventArgs e)
        {
            _InitializeParameterList();
        }
        //-----------------------------------------------------------------------------------------
        //
        //　FileList　列　カラムクリック時のイベントハンドラ
        //
        //-----------------------------------------------------------------------------------------
        //列がクリックされた時
        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
        }
        
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CellApplyBt_Click(object sender, EventArgs e)
        {
        }

        private void CellTextBox_Click(object sender, EventArgs e)
        {

        }

        private void LineInsertBt_Click(object sender, EventArgs e)
        {
        }

        private void LineDeleteBt_Click(object sender, EventArgs e)
        {
        }

        private void ParamSaveBt_Click(object sender, EventArgs e)
        {
            if (INITIALPARAMBt.GetLed() == true)
            {
                SaveInitialPrm();
            }
            else if (SERVOPARAMBt.GetLed() == true)
            {
                SaveServoPrm();
            }
            else if (CRSSPEEDBt.GetLed() == true)
            {
                SaveCRSTbl();
            }
            else if (SEVOPCONBt.GetLed() == true)
            {
                SaveServoPCondTbl();
            }
            else if (IPDATABt.GetLed() == true)
            {
                SaveIpTbl();
            }
            else if (CPSDATABt.GetLed() == true)
            {
                SaveCapTbl();                
            }
            else if (OVERRIDEBt.GetLed() == true)
            {
                SaveOverRideTbl();
            }
            else if(GMCORDSETBt.GetLed() == true)
            {
                SaveGMCodeDisable();
            }
        }

        private void ListReloadBt_Click(object sender, EventArgs e)
        {
            if (INITIALPARAMBt.GetLed() == true)
            {
                DisposeMember();
                UpdateInitialPrm();
            }
            else if (SERVOPARAMBt.GetLed() == true)
            {
                DisposeMember();
                UpdateServoPrm();
            }
            else if (IPDATABt.GetLed() == true)
            {
                DisposeMember();
                UpdateIpTable();
            }
            else if (CPSDATABt.GetLed() == true)
            {
                DisposeMember();
                UpdateCapTable();
            }
            else if (OVERRIDEBt.GetLed() == true)
            {
                DisposeMember();
                UpdateOverRideTable();
            }
            else if (CRSSPEEDBt.GetLed() == true)
            {
                DisposeMember();
                UpdateCRSTbl();
            }
            else if (SEVOPCONBt.GetLed() == true)
            {
                DisposeMember();
                UpdateServoPCondTbl();
            }
            else if (GMCORDSETBt.GetLed() == true)
            {
                DisposeMember();
                UpdateGMCodeDisable();
            }
        }

        private void ParamListView_Click(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void INITIALPARAMBt_Click(object sender, EventArgs e)
        {
            DisposeMember();
            UpdateInitialPrm();
        }

        private void SERVOPARAMBt_Click(object sender, EventArgs e)
        {
            DisposeMember();
            UpdateServoPrm();
        }

        private void CPSDATABt_Click(object sender, EventArgs e)
        {
            DisposeMember();
            UpdateCapTable();
        }

        private void IPDATABt_Click(object sender, EventArgs e)
        {
            DisposeMember();
            UpdateIpTable();
        }

        private void CloseBt_Click(object sender, EventArgs e)
        {
            DisposeMember();
            this.Close();
        }
        private void PARAMBt_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonToRadio((ButtonEx)(sender));
        }

        private void SERIALSETBt_Click(object sender, EventArgs e)
        {
            DisposeMember();
        }
        private void IOCONFIGBt_Click(object sender, EventArgs e)
        {
            DisposeMember();
        }
        private void OVERRIDEBt_Click(object sender, EventArgs e)
        {
            DisposeMember();
            UpdateOverRideTable();
        }

        private void CRSSPEEDBt_Click(object sender, EventArgs e)
        {
            DisposeMember();
            UpdateCRSTbl();
        }

        private void GMCORDSETBt_Click(object sender, EventArgs e)
        {
            DisposeMember();
            UpdateGMCodeDisable();
        }

        private void SEVOPCONBt_Click(object sender, EventArgs e)
        {
            DisposeMember();
            UpdateServoPCondTbl();
        }

        private void OTHERSETBt_Click(object sender, EventArgs e)
        {
            DisposeMember();
        }
        private void dataGridViewEx1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
			if( e.RowIndex < 0 ) return;
			//if( e.RowIndex <= 0 ) return;//最上段行でポップアップテンキーが出せないので変更：柏原
			if( dataGridViewEx1[1, e.RowIndex].Value == null) return;

            if ( INITIALPARAMBt.GetLed())
            {
                if(128 <= e.RowIndex
                && e.RowIndex <= 131)
                {
                    dataGridViewEx1[2, e.RowIndex].Value
                    = BitEditDialog.ShowSubForm(this,
                                                    dataGridViewEx1[1, e.RowIndex].Value.ToString(),
                                                    dataGridViewEx1[2, e.RowIndex].Value.ToString(),
                                                    32);
                }
                else if(136 <= e.RowIndex
                && e.RowIndex <= 139)
                {
                    dataGridViewEx1[2, e.RowIndex].Value
                    = BitEditDialog.ShowSubForm(this,
                                                    dataGridViewEx1[1, e.RowIndex].Value.ToString(),
                                                    dataGridViewEx1[2, e.RowIndex].Value.ToString(),
                                                    32);
                }
                else if (147 <= e.RowIndex
                && e.RowIndex <= 150)
                {
                    dataGridViewEx1[2, e.RowIndex].Value
                    = BitEditDialog.ShowSubForm(this,
                                                    dataGridViewEx1[1, e.RowIndex].Value.ToString(),
                                                    dataGridViewEx1[2, e.RowIndex].Value.ToString(),
                                                    32);
                }
                else if (266 <= e.RowIndex
                && e.RowIndex <= 269)
                {
                    dataGridViewEx1[2, e.RowIndex].Value
                    = BitEditDialog.ShowSubForm(this,
                                                    dataGridViewEx1[1, e.RowIndex].Value.ToString(),
                                                    dataGridViewEx1[2, e.RowIndex].Value.ToString(),
                                                    32);
                }
                else if (155 <= e.RowIndex
                && e.RowIndex <= 158)
                {
                    dataGridViewEx1[2, e.RowIndex].Value
                    = BitEditDialog.ShowSubForm(this,
                                                    dataGridViewEx1[1, e.RowIndex].Value.ToString(),
                                                    dataGridViewEx1[2, e.RowIndex].Value.ToString(),
                                                    32);
                }
                else if (141 == e.RowIndex
                    || 187 == e.RowIndex
                    || 227 == e.RowIndex)
                {
                    dataGridViewEx1[2, e.RowIndex].Value
                    = BitEditDialog.ShowSubForm(this,
                                                    dataGridViewEx1[1, e.RowIndex].Value.ToString(),
                                                    dataGridViewEx1[2, e.RowIndex].Value.ToString(),
                                                    6);
                }
                else if (142 == e.RowIndex
                    || 154 == e.RowIndex
                    || 225 == e.RowIndex)
                {
                    dataGridViewEx1[2, e.RowIndex].Value
                    = BitEditDialog.ShowSubForm(this,
                                                    dataGridViewEx1[1, e.RowIndex].Value.ToString(),
                                                    dataGridViewEx1[2, e.RowIndex].Value.ToString(),
                                                    8);
                }
                else
                {
                    //ポップアップテンキー表示
                    popupTenkeyOn(dataGridViewEx1, e);
                }
            }
            else if(SERVOPARAMBt.GetLed())
            {
                if (234 == e.RowIndex)
                {
                    dataGridViewEx1[2, e.RowIndex].Value
                    = BitEditDialog.ShowSubForm(this,
                                                    dataGridViewEx1[1, e.RowIndex].Value.ToString(),
                                                    dataGridViewEx1[2, e.RowIndex].Value.ToString(),
                                                    64);
                }
                else
                {
                    //ポップアップテンキー表示
                    popupTenkeyOn(dataGridViewEx1, e);
                }
            }
            else
            {
                //ポップアップテンキー表示
                popupTenkeyOn(dataGridViewEx1, e);
            }
            
        }
        private void ReturnValue(object sender, FormClosingEventArgs e)
        {
            dataGridViewEx1[2, dataGridViewEx1.SelectedRows[0].Cells[0].RowIndex].Value = editDlg.GetValueText();
        }
        private void ButtonToRadio(ButtonEx sender)
        {
            bool senderLamp = sender.GetLed();
            if (true == senderLamp){ }
            else
            {
                foreach (ButtonEx btn in sender.Parent.Controls)
                {
                    if (sender == btn)
                    {
                        btn.SetLed(!senderLamp);
                    }
                    else
                    {
                        btn.SetLed(senderLamp);
                    }
                }
            }
        }
        #endregion

        private void DisposeMember()
        {
            DataGridView_ClearCells();
            if (datini != null)
            {
                datini.Dispose();
                datini = null;
            }
            if (datsrv != null)
            {
                datsrv.Dispose();
                datsrv = null;
            }
        }

        private void DataGridView_ClearCells()
        {
            dataGridViewEx1.Rows.Clear();
            dataGridViewEx1.RowCount = 500;

        }

		#region PopupTenkey
		/// <summary>
		/// 記録する加工条件値
		/// </summary>
		//object _controlName = "";
		private DataGridViewCellEventArgs mem_e;
		/// <summary>
		/// ポップアップテンキー：表示/非表示
		/// </summary>
		/// <param name="val"></param>
		/// <returns>false=上下ポップアップを表示</returns>
		private bool popupTenkeyOn( object val, DataGridViewCellEventArgs e )
		{
			try {
				//引数記録
				mem_e = e;
				//フォーマットタイプ
				NumericTextBox.FormatTypes formatType = NumericTextBox.FormatTypes.Integer1;
				if( _popupTenkey != null ) {
					_popupTenkey.Close();   //画面を閉じる
					_popupTenkey = null;    //null初期化
				}
				//引数をDataGridViewに戻す
				DataGridView dataGridView = (DataGridView)val;
				//各セルNULLチェック
				for( int count = 1 ; count < 4 ; count++ ) {
					if( dataGridViewEx1[count, e.RowIndex].Value == null ) return false;
				}
				//各セル値取得
				string tmpName = dataGridViewEx1[1, e.RowIndex].Value.ToString();//名称
				string tmpVal = dataGridViewEx1[2, e.RowIndex].Value.ToString();//値
				string tmpMin = dataGridViewEx1[3, e.RowIndex].Value.ToString();//最小値
				string tmpMax = dataGridViewEx1[4, e.RowIndex].Value.ToString();//最大値

				//Int32　-2147483648から2147483648                                                                  //フォーマットタイプ設定
				//long -9223372036854775808 ～ 9223372036854775807。 符号付き 64 ビット整数
				// System.Numerics.BigInteger one = new System.Numerics.BigInteger(999999999999999);
				formatType = NumericTextBox.FormatTypes.SignLong10;//-9999999999から9999999999　※SignInteger10をやめSignLong10にする。
																	  //セル空白対応
				if( tmpVal == "" ) tmpVal = "0";
				if( tmpMin == "" ) tmpMin = "-9999999999";
				if( tmpMax == "" ) tmpMax = "9999999999";
				//「fff・・・」対応
				// HEX文字か数字か判断、それ以外は例外が発生します。
				//if (!char.IsNumber(tmpVal, 0)) tmpVal = Convert.ToInt64(tmpVal, 16).ToString();//例外が発生例：「3f」
				//if (!char.IsNumber(tmpMin, 0)) tmpMin = Convert.ToInt64(tmpMin, 16).ToString();
				//if (!char.IsNumber(tmpMax, 0)) tmpMax = Convert.ToInt64(tmpMax, 16).ToString();
				//値セット
				string changeVal = tmpVal;                      //値
				Decimal lowerLimitDec = decimal.Parse( tmpMin );//最小値
				Decimal upperLimitDec = decimal.Parse( tmpMax );//最大値
				//ポップアップTenKey：2017-1-12:柏原
				_popupTenkey = new TenKeyDialog( changeVal, formatType, lowerLimitDec, upperLimitDec,true);//全選択あり
				_popupTenkey.NotifyReturnOk = popupTenkey_OnNotifyReturnOk; //イベント通知：OK
				_popupTenkey.Text = tmpName;                                //テンキータイトル表示
				_popupTenkey.ShowDialog( this );                            //画面を開く
				return true;
			}
			//例外発生
			catch( Exception ex ) {
				MessageBox.Show( ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
			}
			return false;
		}
		/// <summary>
		/// ポップアップテンキーで「OK」ボタンを押した時、入力値をこのクラスに反映
		/// </summary>
		private void popupTenkey_OnNotifyReturnOk()
        {
            string retVal = _popupTenkey._tenkeyValReturn;      //ポップアップテンキーで編集された値
            dataGridViewEx1[2, mem_e.RowIndex].Value = retVal;  //値をセルにセット
        }
        /// <summary>
        /// Form閉じる時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParameterViewForm_FormClosed( object sender, FormClosedEventArgs e )
		{
			//ポップアップテンキー
			if( null != _popupTenkey ) {
				_popupTenkey.Close();
				_popupTenkey = null;
			}
		}
        #endregion
    }
}
