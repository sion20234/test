///////////////////////////////////////////////////////////////////////////////////////////
//
// (1) システム名称 : ELENIX ECNCⅢ
// (2) ファイル名   : ReferencingForm.cs
// (3) 概要         : 位置出し画面
// (4) 作成日       : 2016.07.19
// (5) 作成者       : 八野
// (6) 特記事項     : VisualStudio2015 C#
// (7) 更新履歴     : 2017.03.31：柏原
//
///////////////////////////////////////////////////////////////////////////////////////////

using ECNC3.Enumeration;
using ECNC3.Models.McIf;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using ECNC3.Models;         //FileSettings用
using ECNC3.Views.Popup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECNC3.Views
{
    /// <summary>
    /// 位置出し画面
    /// </summary>
    public partial class ReferencingForm : ECNC3Form
    {
        #region <region<初期化>region>
        /// <summary>ポップアップテンキー</summary>
        private TenKeyDialog _popupTenkey = new TenKeyDialog("", 0, 0, 0);//初回インスタンスを作っておく
        /// <summary>
        /// ポップアップ上下キーのLocation調整
        /// </summary>
        public Point _offset = new Point();
        /// <summary>
        /// 子フォームの切り替えモード
        /// </summary>
        string strSubFormMode = "";
        /// <summary>
        /// ReferencingFormのSubForm画像切り替えのフラグ用変数
        /// </summary>
        internal string ImagePath;
        private int BeforeCorrectAngle = 0;
		//画面の初期値
		string textResetValue = "0.0000";
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ReferencingForm()
        {
            InitializeComponent();
        }
		/// <summary>
		/// フォームロード
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ReferencingForm_Load(object sender, EventArgs e)
        {
            if (DesignMode == true)
            {
                return;
            }
			//下3桁か4桁か取得：サブミクロン
			CncSys_AxisInfomation_XmlRead( ref _submicronFlg );
			if( _submicronFlg ) textResetValue = "0.0000";
			else				textResetValue = "0.000";

			//メイン・データグリッドビュー：設定
			dataGridViewEx1.Initialize(12.0F, 35, true);
            //軸名称
            DataGridViewColumn _axisCol = new DataGridViewColumn();
            _axisCol.CellTemplate = new DataGridViewTextBoxCell();
            _axisCol.Name = "ReferenceAxisSelBtCol";
            _axisCol.Width = 50;
            //値
            DataGridViewColumn _valueCol = new DataGridViewColumn();
            _valueCol.CellTemplate = new DataGridViewTextBoxCell();
            _valueCol.Name = "ReferenceOffsetValueCol";
            _valueCol.Width = 130;
            _valueCol.DefaultCellStyle.Format = "####0.0##";
            //単位
            DataGridViewColumn _unitCol = new DataGridViewColumn();
            _unitCol.CellTemplate = new DataGridViewTextBoxCell();
            _unitCol.Name = "ReferenceAxisUnitCol";
            _unitCol.Width = 55;

            dataGridViewEx1.Columns.Add(_axisCol);    //軸名称
            dataGridViewEx1.Columns.Add(_valueCol);   //値
            dataGridViewEx1.Columns.Add(_unitCol);    //単位

            dataGridViewEx1.ColumnHeadersVisible = false;
            dataGridViewEx1.InitCol("ReferenceAxisSelBtCol", 15.0F, DataGridViewContentAlignment.MiddleCenter, typeof(string));
            dataGridViewEx1.InitCol("ReferenceOffsetValueCol", 15.0F, DataGridViewContentAlignment.MiddleRight, typeof(string));
            dataGridViewEx1.InitCol("ReferenceAxisUnitCol", 12.0F, DataGridViewContentAlignment.MiddleCenter, typeof(string));
            dataGridViewEx1.RowCount = 5;
            dataGridViewEx1.Rows[0].Cells[0].Value = "X";
            dataGridViewEx1.Rows[1].Cells[0].Value = "Y";
            dataGridViewEx1.Rows[2].Cells[0].Value = "Z";
            dataGridViewEx1.Rows[3].Cells[0].Value = "H";
            dataGridViewEx1.Rows[4].Cells[0].Value = "I";
            foreach (DataGridViewRow row in dataGridViewEx1.Rows)
            {
                row.Cells[1].Value = textResetValue;//値
                row.Cells[2].Value = "mm";          //単位
                row.Height = 35;                    //行高さ
            }

            //角度補正：データグリッドビュー：設定
            DataGridViewColumn _miniaxisCol = new DataGridViewColumn();
            _miniaxisCol.CellTemplate = new DataGridViewButtonCell();
            _miniaxisCol.Name = "MiniaxisCol";
            _miniaxisCol.Width = 49;
            dataGridViewEx2.Columns.Add(_miniaxisCol);
            //dataGridViewEx2.Initialize(12.0F, 35, true);
            dataGridViewEx2.InitCol("MiniaxisCol", 15.0F, DataGridViewContentAlignment.MiddleCenter, typeof(string));
            dataGridViewEx2.RowCount = 2;
            dataGridViewEx2.Rows[0].Cells[0].Value = "●";
            dataGridViewEx2.Rows[1].Cells[0].Value = "";
            dataGridViewEx2.Rows[0].Height = 33;
            dataGridViewEx2.Rows[1].Height = 33;
            //角度補正以外非表示
            dataGridViewEx2.Visible = false;

            tanmenBt.SetBack(true);
            _AllBtOff(tanmenBt as object);
            Imagechange("tanmen");




            //// 画像ファイルをロードする
            //_image = new Bitmap(System.IO.Path.Combine(Application.StartupPath, "Picture\\arrow_right.gif"));
            //// pictureBox1の背景画像としてセット
            //pictureBox1.BackgroundImage = _image;

            //// 描画(Paint)イベントハンドラを追加
            //pictureBox1.Paint += pictureBoxEx1_Paint;
            //// アニメーション開始
            //ImageAnimator.Animate(_image, new EventHandler(Image_FrameChanged));
            //pictureBox1.Refresh();
        }
		private bool _submicronFlg = false;
		/// <summary>
		///  CncSys.xml：軸関係情報設定<AxisInfomation>からサブミクロン(下3か4桁)表示を取得
		/// </summary>
		/// <param name="submicronFlg"></param>
		private void CncSys_AxisInfomation_XmlRead(ref bool submicronFlg)
		{
			int intDigit = 0;
			try {
				using( FileSettings fs = new FileSettings() ) {
				   //ファイル読み込み
					fs.Read();
					//サブミクロン表示(submicron)：取得
					intDigit = fs.AttrValue( "Root/AxisInfomation/Position ", "digit" );
					if( intDigit == 4 ) {
						submicronFlg = true;
					} else {
						submicronFlg = false;
					}
				}
			} catch( Exception exc ) {
				//例外処理
				MessageBox.Show( exc.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error );
			}
		}
		private void Image_FrameChanged(object o, EventArgs e)
        {
            // Paintイベントハンドラを呼び出す
            //pictureBox1.Invalidate();
        }

        void pictureBoxEx1_Paint(object sender, PaintEventArgs e)
        {
            // イベントハンドラ：ピクチャボックスの描画
            //ImageAnimator.UpdateFrames(_image);
        }


        private void InitInput(ButtonEx selSw, NumericTextBox textBox, NumericTextBox.FormatTypes format)
        {
            textBox.FormatType = format;
            textBox.ReadOnly = true;
            selSw.EditBox = textBox;
        }
        #endregion
        bool _threadStop = false;

        /// <summary>
        /// ステータスモニタ
        /// </summary>
        /// <param name="e"></param>
        internal void StatusMonitoring(StatusMonitorEventArgs e)
        {
            if (_threadStop == true)
            {
                if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                {
                    _threadStop = false;
                    CloseForm();
                }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                //Thread.Sleep(3000);
                //if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                //{
                //    if (e.Items.ProcessMode == McTaskModes.Manual) this.Close();
                //    if(_ChangeMode(e, McTaskModes.Manual) == ResultCodes.Success)
                //    {
                //        this.Close();
                //    }
                //    else
                //    {
                //        _threadStop = false;
                //    }
                //}); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                //return;
            }
            //角度補正表示処理
            if (e.Items.CorrectAngleEn == true)
            {
                if (_currectAngleBt.GetBack() == false)
                {
                    _currectAngleBt.SetBack(true);
                    UILog logs = new UILog("OptionForm.SetFunc.SettingMonitoring");
                    logs.Error("CHG_CORRCTANGLE_ON");
                }
            }
            else
            {
                if (_currectAngleBt.GetBack() == true)
                {
                    _currectAngleBt.SetBack(false);
                    UILog logs = new UILog("OptionForm.SetFunc.SettingMonitoring");
                    logs.Error("CHG_CORRCTANGLE_OFF");
                }
            }
            
            sbyte sign = 0;
            ushort value = 0;
            if (BeforeCorrectAngle != e.Items.CorrectAngleValue)
            {
                sign = (sbyte)BitConverter.GetBytes(e.Items.CorrectAngleValue)[0];
                value = (ushort)((e.Items.CorrectAngleValue) >> 8);
                if( (this.IsDisposed == true || this.Disposing == true)   ) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                {
                    ApiMonitorTask.monitorMx.WaitOne();
                    _currAngleText.Text = sign.ToString() + "." + value.ToString();
                    ApiMonitorTask.monitorMx.ReleaseMutex();
                }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
            BeforeCorrectAngle = e.Items.CorrectAngleValue;
            }
                
            //Startボタン処理
            if ((e.Items.StartSwBtnOffEdge == true || dbgSt == true)
                && _popupTenkey.Visible == false)
            {
                dbgSt = false;
                //主軸動作を選択している場合、主軸動作実行後、位置出し操作処理を抜ける。
                if(Owner.GetType() == typeof(MANUALForm))
                {
                    bool startSwitchProcEnd = false;
                    //主軸回転
                    if((Owner as MANUALForm).GetProcCommandButton( ButtonEx.ButtonExStatus.Back, ManualModeProcCommands.Spin) == true)
                    {
                        using (SequenceFunction SeqFunc = new SequenceFunction(1))
                        {
                            ResultCodes Error = (SeqFunc.SequenceMonitoring(Sequences.SpindleOn));
                            //ログ処理
                            UILog logs = new UILog("MANUALForm.ReferencingForm.SeqFunc.SequenceMonitoring");
                            if (Error == ResultCodes.Success)
                            {
                                logs.Sure("SPINDLE_CW-ON Result = " + Error);
                            }
                            else
                            {
                                logs.Error("SPINDLE_CW-ON Result = " + Error);
                            }
                        }
                        (Owner as MANUALForm).SetProcCommandButton( ButtonEx.ButtonExStatus.Back, ManualModeProcCommands.Spin, false);
                        startSwitchProcEnd = true;
                    }
                    //ポンプ
                    if((Owner as MANUALForm).GetProcCommandButton( ButtonEx.ButtonExStatus.Back, ManualModeProcCommands.Pomp) == true)
                    {
                        using (SequenceFunction SeqFunc = new SequenceFunction(1))
                        {
                            ResultCodes Error = SeqFunc.SequenceMonitoring(Sequences.PompOn);
                            UILog logs = new UILog("MANUALForm.ReferencingForm.SeqFunc.SequenceMonitoring");
                            if (Error == ResultCodes.Success)
                            {
                                logs.Sure("POMP-ON Result = " + Error);
                            }
                            else
                            {
                                logs.Error("POMP-ON Result = " + Error);
                            }
                        }
                        (Owner as MANUALForm).SetProcCommandButton(ButtonEx.ButtonExStatus.Back, ManualModeProcCommands.Pomp, false);
                        startSwitchProcEnd = true;
                    }
                    if (startSwitchProcEnd == true) return;
                }
                //X
                string strTemp = dataGridViewEx1.Rows[0].Cells[1].Value.ToString();
                decimal decimalX = decimal.Parse(strTemp);
                //Y
                strTemp = dataGridViewEx1.Rows[1].Cells[1].Value.ToString();
                decimal decimalY = decimal.Parse(strTemp);
                //Z
                strTemp = dataGridViewEx1.Rows[2].Cells[1].Value.ToString();
                decimal decimalZ = decimal.Parse(strTemp);
                //H
                strTemp = dataGridViewEx1.Rows[3].Cells[1].Value.ToString();
                decimal decimalH = decimal.Parse(strTemp);
                //I
                strTemp = dataGridViewEx1.Rows[4].Cells[1].Value.ToString();
                decimal decimalI = decimal.Parse(strTemp);


                //RTMC64ECの動作モードを「AUTO」にする。
                using (McReqModeChange ReqModeChg = new McReqModeChange())
                {
                    ReqModeChg.TaskMode = Enumeration.McTaskModes.Auto;
                    if (ReqModeChg.Execute() != Enumeration.ResultCodes.Success)
                    {
                        //動作モード変更に失敗したら有効軸切替を行わない。
                        return;
                    }
                }
                using (Models.McIf.McDatStatus status = new McDatStatus())
                {
                    status.Read();

                    //Autoモード待機
                    while (status.Status.MotionMode != Enumeration.McTaskModes.Auto)
                    {
                        status.Read();
                    }
                }
                //位置出し処理実行
                ECNC3.Models.McIf.McReqEggeProgram mce = new Models.McIf.McReqEggeProgram();
                int intRet = mce.EdgeSerchExec(
                    getModeNum(),//モード番号：取得
                    decimalX,
                    decimalY,
                    decimalZ,
                    decimalH,
                    decimalI,
                    ((string)dataGridViewEx2[0, 0].Value == "") ? true : false//角度補正：dgvex2
                                                                              //dataGridViewEx1.Rows[1].Visible//角度補正：false=X軸選択、true=Y軸選択
                    );
                //ブログラム運転開始↓↓
                if (e.Items.FGEnd == true && e.Items.SequenceEnd == true)
                {
                    if (e.Items.FGEnd == true && e.Items.SequenceEnd == true)
                    {
                        string SelectNumber = "", Value = "";

                        using (SequenceFunction SeqFunc = new SequenceFunction(-1))
                        {
                            SelectNumber = SeqFunc.SequenceMonitoring(Sequences.ProgramSelect).ToString();
                            Value = SeqFunc.SequenceMonitoring(Sequences.ProgramStart).ToString();
                        }
                        if (Value != ResultCodes.Success.ToString())
                        {
                            UILog ReturnCmdLog = new UILog("ReferencingForm.StatusMonitoring");
                            ReturnCmdLog.Error("SELECTNUMBER_SET (1) = " + SelectNumber + ", " + "RETURN_CMD Result = " + Value);
                        }
                    }
                }
                //ブログラム運転開始↑↑
            }
            //位置出し後動作モード変更
            if (e.Items.FGEnd == true
                && e.Items.ProcessMode != McTaskModes.Manual)
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
                    //Manualモード待機
                    while (status.Status.MotionMode != Enumeration.McTaskModes.Manual)
                    {
                        RetrySequence(retryCt);
                        status.Read();
                        retryCt++;
                    }
                }
            }
        }
        /// <summary>
        /// リトライ処理(0.5秒/回)
        /// </summary>
        /// <param name="_retryCt">リトライ回数</param>
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
        public bool EscapeFlag = false;
        private ResultCodes _ChangeMode(McTaskModes sourceMode, McTaskModes targetMode)
        {
            if (EscapeFlag == true)
            {
                EscapeFlag = false;
                return ResultCodes.Success;
            }
            //自動モード→手動モードに変更する場合、リセットを実行する。
            if (sourceMode == targetMode) return ResultCodes.InvalidArgument;
            ResultCodes ret = ResultCodes.Success;
            if(sourceMode == McTaskModes.Auto)
            {
                #region ResetSequence
                //	汚水槽エラーかつSPXリセット無効設定のときはリセットを実行しない。
                //キャンセルした場合、画面クローズ処理を抜ける
                if (DialogResult.No == _MessageShow(MessageBoxIcon.Question, 5500)) return ResultCodes.NotExecute;

                while (true)
                {
                    using (McDatStatus mc = new McDatStatus())
                    {
                        ret = mc.Read();
                        if (ResultCodes.Success != ret) break;
                        if (true == mc.Status.EtherCatErrorHoldingTankLiquidEmpty)
                        {
                            using (FileSettings fs = new FileSettings())
                            {
                                ret = fs.Read();
                                if (ResultCodes.Success != ret) break;
                                if (true == fs.AttrBool("Root/Motions/Spx/Reset", "enbl")) break;  //	リセットしない
                            }
                        }
                        if (true == mc.Status.ProgramRunning)
                            if (DialogResult.No == _MessageShow(MessageBoxIcon.Question, 527)) break;
                    }
                    using (McReqReset mc = new McReqReset())
                    {
                        ret = mc.Execute();
                        if (ResultCodes.Success != ret) return ResultCodes.NotExecute;
                        
                    }
                    break;
                }
                #endregion
            }
            if (ResultCodes.Success != ret) _MessageShow(MessageBoxIcon.Error, 110);
            //動作モード変更
            if (sourceMode != targetMode)
            {
                //RTMC64ECの動作モードを変更する。
                using (McReqModeChange ReqModeChg = new McReqModeChange())
                {
                    ReqModeChg.TaskMode = targetMode;
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
                    //Manualモード待機
                    while (status.Status.MotionMode != targetMode)
                    {
                        RetrySequence(retryCt);
                        status.Read();
                        retryCt++;
                    }
                    ret = (status.Status.MotionMode == targetMode) ? ResultCodes.Success : ResultCodes.InvalidArgument;
                }
            }
            return ret;
        }
        /// <summary>
        /// 位置出し図の画像切り替え処理
        /// </summary>
        /// <param name="strName">親フォームの位置だしモード</param>
        private void Imagechange(string strName)
        {
            _pictureboxDispose();
            _panelDispose();
            string PathA = null,
                   PathB = null,
                   PathC = null,
                   PathD = null,
                   ModeNameA = null,
                   ModeNameB = null,
                   ModeNameC = null,
                   ModeNameD = null;

            strSubFormMode = strName;
            _modeBtInit();

			SubFormDBt.Visible = true;
			SubDImage.Visible = true;
			switch( strName)
            {
                case "tanmen":
                    ModeNameA = "tanmenA";
                    ModeNameB = "tanmenB";
                    ModeNameC = "tanmenC";
                    ModeNameD = "tanmenD";
                    break;

                case "gaikei":
                    ModeNameA = "gaikeiA";
                    ModeNameB = "gaikeiB";
                    ModeNameC = "gaikeiC";
                    ModeNameD = "";
					SubFormDBt.Visible = false;
					SubDImage.Visible = false;
					break;

                case "naikei":
                    ModeNameA = "naikeiA";
                    ModeNameB = "naikeiB";
                    ModeNameC = "naikeiC";
                    ModeNameD = "";
					SubFormDBt.Visible = false;
					SubDImage.Visible = false;
					break;

                case "soto":
                    ModeNameA = "sotoA";
                    ModeNameB = "sotoB";
                    ModeNameC = "sotoC";
                    ModeNameD = "sotoD";
                    break;

                case "uchi":
                    ModeNameA = "uchiA";
                    ModeNameB = "uchiB";
                    ModeNameC = "uchiC";
                    ModeNameD = "uchiD";
                    break;

                case "Ret":
                    ModeNameA = "RetA";
                    ModeNameB = "RetB";
                    ModeNameC = "RetC";
                    ModeNameD = "RetD";
                    break;
            }
            if (ModeNameA != "")
            {
                PathA = FilePathInfo.ECNC3PATH + "Picture\\" + ModeNameA + ".png";                                        //相対パス"Picture\\"+ ModeName +".png"の絶対パスを取得する
                SubAImage.LoadImage(PathA);
            }
            else { }

            if (ModeNameB != "")
            {
                PathB = FilePathInfo.ECNC3PATH + "Picture\\" + ModeNameB + ".png";                                        //相対パス"Picture\\"+ ModeName +".png"の絶対パスを取得する
                SubBImage.LoadImage(PathB);
            }
            else { }

            if (ModeNameC != "")
            {
                PathC = FilePathInfo.ECNC3PATH + "Picture\\" + ModeNameC + ".png";                                        //相対パス"Picture\\"+ ModeName +".png"の絶対パスを取得する
                SubCImage.LoadImage(PathC);
            }
            else { }

            if (ModeNameD != "")
            {
                PathD = FilePathInfo.ECNC3PATH + "Picture\\" + ModeNameD + ".png";                                        //相対パス"Picture\\"+ ModeName +".png"の絶対パスを取得する
                SubDImage.LoadImage(PathD);
            }
            else { }
        }

        /// <summary>
        /// 端面ボタンクリック時、位置取りイメージ図を切り替える
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">tanmenBt選択時のイベント</param>
        private void tanmenBt_CheckedChanged(object sender, EventArgs e)
        {
            if (tanmenBt.GetBack() == true)
            {
                return;
            }
            tanmenBt.SetBack(true);
            _AllBtOff(sender);
            Imagechange("tanmen");

            //モード番号取得用、ボタンタイプ：設定
            setButtonType((int)enumButtonType.endFace);
            //モード番号取得用、図タイプ：設定
            setFigType((int)enumFigType.figA);
            //補正角度：X/Y選択コンボ：非表示
            comboBox_angleCorrectionXY.Visible = false;
            //角度補正グリッド：非表示
            dataGridViewEx2.Visible = false;
        }
        /// <summary>
        /// 外径ボタンクリック時、位置取りイメージ図を切り替える
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">gaikeiBt選択時のイベント</param>
        private void gaikeiBt_CheckedChanged(object sender, EventArgs e)
        {
            if (gaikeiBt.GetBack() == true)
            {
                return;
            }
            gaikeiBt.SetBack(true);
            _AllBtOff(sender);
            Imagechange("gaikei");
            //モード番号取得用、ボタンタイプ：設定
            setButtonType((int)enumButtonType.outerDiameter);
            //モード番号取得用、図タイプ：設定
            setFigType((int)enumFigType.figA);
            //補正角度：X/Y選択コンボ：非表示
            comboBox_angleCorrectionXY.Visible = false;
            //角度補正グリッド：非表示
            dataGridViewEx2.Visible = false;
        }
        /// <summary>
        /// 内径ボタンクリック時、位置取りイメージ図を切り替える
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">naikeiBt選択時のイベント</param>
        private void naikeiBt_CheckedChanged(object sender, EventArgs e)
        {
            if (naikeiBt.GetBack() == true)
            {
                return;
            }
            naikeiBt.SetBack(true);
            _AllBtOff(sender);
            Imagechange("naikei");
            //モード番号取得用、ボタンタイプ：設定
            setButtonType((int)enumButtonType.innerDiameter);
            //モード番号取得用、図タイプ：設定
            setFigType((int)enumFigType.figA);
            //補正角度：X/Y選択コンボ：非表示
            comboBox_angleCorrectionXY.Visible = false;
            //角度補正グリッド：非表示
            dataGridViewEx2.Visible = false;
        }
        /// <summary>
        /// 外コーナーボタンクリック時、位置取りイメージ図を切り替える
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">sotoBt選択時のイベント</param>
        private void sotoBt_CheckedChanged(object sender, EventArgs e)
        {
            if (sotoBt.GetBack() == true)
            {
                return;
            }
            sotoBt.SetBack(true);
            _AllBtOff(sender);
            Imagechange("soto");
            //モード番号取得用、ボタンタイプ：設定
            setButtonType((int)enumButtonType.outerCorner);
            //モード番号取得用、図タイプ：設定
            setFigType((int)enumFigType.figA);
            //補正角度：X/Y選択コンボ：非表示
            comboBox_angleCorrectionXY.Visible = false;
            //角度補正グリッド：非表示
            dataGridViewEx2.Visible = false;
        }
        /// <summary>
        /// 内コーナーボタンクリック時、位置取りイメージを切り替える
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">uchiBt選択時のイベント</param>
        private void uchiBt_CheckedChanged(object sender, EventArgs e)
        {
            if (utiBt.GetBack() == true)
            {
                return;
            }
            utiBt.SetBack(true);
            _AllBtOff(sender);
            Imagechange("uchi");
            //モード番号取得用、ボタンタイプ：設定
            setButtonType((int)enumButtonType.innerCorner);
            //モード番号取得用、図タイプ：設定
            setFigType((int)enumFigType.figA);
            //補正角度：X/Y選択コンボ：非表示
            comboBox_angleCorrectionXY.Visible = false;
            //角度補正グリッド：非表示
            dataGridViewEx2.Visible = false;
        }
        /// <summary>
        /// 角度補正ボタンクリック時、位置取りイメージ図を切り替える
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">retBt選択時のイベント</param>
        private void retBt_CheckedChanged(object sender, EventArgs e)
        {
            if (retBt.GetBack() == true)
            {
                return;
            }
            retBt.SetBack(true);
            _AllBtOff(sender);
            Imagechange("Ret");
            //モード番号取得用、ボタンタイプ：設定
            setButtonType((int)enumButtonType.angleCorrection);
            //モード番号取得用、図タイプ：設定
            setFigType((int)enumFigType.figA);
            //補正角度：X/Y選択コンボ：表示
            comboBox_angleCorrectionXY.Visible = false;//ミニ・グリッドがだめな場合、trueにする。
//            comboBox_angleCorrectionXY.Visible = true;
            //セルX/Y行どちらか非表示
            comboBox_angleCorrectionXY_Select(0);//初回はXを表示
            //角度補正グリッド：表示
            dataGridViewEx2.Visible = true;
            //角度補正グリッド：
            dataGridViewEx2_SetSelect(false);//X選択
        }

        private void _AllBtOff(object sender)
        {
            ButtonEx BtnEx = sender as ButtonEx;
            foreach (Control Ctrl in (sender as ButtonEx).Parent.Controls)
            {
                if (Ctrl.GetType() == typeof(ButtonEx))
                {
                    if (BtnEx.Name == (Ctrl as ButtonEx).Name)
                    {
                        continue;
                    }
                    else
                    {
                        (Ctrl as ButtonEx).SetBack(false);
                    }
                }
            }
        }
        private void _modeBtInit()
        {
            ImageView("A3D", true);
            SubFormABt.SetBack(true);
            _AllBtOff(SubFormABt as object);
        }
        /// <summary>
        /// 位置取り詳細イメージAで子フォームを開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">SubFormABtのボタンクリック時のイベント</param>
        private void SubFormABt_Click(object sender, EventArgs e)
        {
            if ((sender as ButtonEx).GetBack() == false)
            {
                ImageView("A3D");
                (sender as ButtonEx).SetBack(true);
                _AllBtOff(sender);
                //モード番号取得用、図タイプ：設定
                setFigType((int)enumFigType.figA);
                //現在の状態でセルX/Y行どちらか表示
                comboBox_angleCorrectionXY_SelectedIndexChanged(null, null);
            }
        }
        /// <summary>
        /// 位置取り詳細イメージBで子フォームを開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">SubFormBBtのボタンクリック時のイベント</param>
        private void SubFormBBt_Click(object sender, EventArgs e)
        {
            if ((sender as ButtonEx).GetBack() == false)
            {
                ImageView("B3D");
                (sender as ButtonEx).SetBack(true);
                _AllBtOff(sender);
                //モード番号取得用、図タイプ：設定
                setFigType((int)enumFigType.figB);
                //現在の状態でセルX/Y行どちらか表示
                comboBox_angleCorrectionXY_SelectedIndexChanged(null, null);
            }
        }

        /// <summary>
        /// 位置取り詳細イメージCで子フォームを開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">SubFormCBtのボタンクリック時のイベント</param>
        private void SubFormCBt_Click(object sender, EventArgs e)
        {
            if ((sender as ButtonEx).GetBack() == false)
            {
                ImageView("C3D");
                (sender as ButtonEx).SetBack(true);
                _AllBtOff(sender);
                //モード番号取得用、図タイプ：設定
                setFigType((int)enumFigType.figC);
                //現在の状態でセルX/Y行どちらか表示
                comboBox_angleCorrectionXY_SelectedIndexChanged(null, null);
            }
        }

        /// <summary>
        /// 位置取り詳細イメージDで子フォームを開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">SubFormDBtのボタンクリック時のイベント</param>
        private void SubFormDBt_Click(object sender, EventArgs e)
        {
            if (strSubFormMode == "gaikei"
            || strSubFormMode == "naikei")
            {
                return;
            }
            if ((sender as ButtonEx).GetBack() == false)
            {
                ImageView("D3D");
                (sender as ButtonEx).SetBack(true);
                _AllBtOff(sender);
                //モード番号取得用、図タイプ：設定
                setFigType((int)enumFigType.figD);
                //現在の状態でセルX/Y行どちらか表示
                comboBox_angleCorrectionXY_SelectedIndexChanged(null, null);
            }
        }
        private void ImageView(string _mode, bool cellValReset = false)
        {
            ImagePath = strSubFormMode + _mode;                                              //ReferencingFormSubで、SubFormImagePathを判定して位置取りイメージを表示する。
            _pictureboxDispose();
            string Path = "";
            if (ImagePath != "")
            {
                Path = FilePathInfo.ECNC3PATH + "Picture\\" + ImagePath + ".png";                                         //相対パス"Picture\\"+ ModeName +".png"の絶対パスを取得する
				if( System.IO.File.Exists( Path ) == false ) {
					//ファイルが存在しない
					return;
				}
				pictureBox5.LoadImage(Path);                                             //子画面の対象の位置取り図を表示する。
                labelEx3.Text = SelectModeView(ImagePath.Replace("3D", ""));

                EditListChgEn(labelEx3.Text, cellValReset);

            }
            else { }

        }

        private void EditListChgEn(string _mode, bool cellValReset)
        {
            switch (_mode)
            {
                case "端面A":
                    foreach (DataGridViewRow _dgrow in dataGridViewEx1.Rows)
                    {
                        _dgrow.Visible = true;
                        switch (_dgrow.Cells[0].Value as string)
                        {
                            case "X":
                                _dgrow.Visible = false;
                                break;

                            case "Y":
                                _dgrow.Visible = false;
                                break;

                            case "Z":

                                break;

                            case "H":

                                break;

                            case "I":
                                _dgrow.Visible = false;
                                break;
                        }
                        //画面変更時の初期値
                        if (cellValReset) _dgrow.Cells[1].Value = textResetValue;
                    }
                    break;

                case "端面B":
                    foreach (DataGridViewRow _dgrow in dataGridViewEx1.Rows)
                    {
                        _dgrow.Visible = true;
                        switch (_dgrow.Cells[0].Value as string)
                        {
                            case "X":
                                _dgrow.Visible = false;
                                break;

                            case "Y":
                                _dgrow.Visible = false;
                                break;

                            case "Z":

                                break;

                            case "H":

                                break;

                            case "I":
                                _dgrow.Visible = false;
                                break;

                        }
                    }
                    break;

                case "端面C":
                    foreach (DataGridViewRow _dgrow in dataGridViewEx1.Rows)
                    {
                        _dgrow.Visible = true;
                        switch (_dgrow.Cells[0].Value as string)
                        {
                            case "X":
                                _dgrow.Visible = false;
                                break;

                            case "Y":
                                _dgrow.Visible = false;
                                break;

                            case "Z":

                                break;

                            case "H":

                                break;

                            case "I":
                                _dgrow.Visible = false;
                                break;

                        }
                    }
                    break;

                case "端面D":
                    foreach (DataGridViewRow _dgrow in dataGridViewEx1.Rows)
                    {
                        _dgrow.Visible = true;
                        switch (_dgrow.Cells[0].Value as string)
                        {
                            case "X": _dgrow.Visible = false; break;
                            case "Y": _dgrow.Visible = false; break;
                            case "Z": break;
                            case "H": break;
                            case "I": _dgrow.Visible = false; break;
                        }
                    }
                    break;

                case "外径A":
                    foreach (DataGridViewRow _dgrow in dataGridViewEx1.Rows)
                    {
                        _dgrow.Visible = true;
                        switch (_dgrow.Cells[0].Value as string)
                        {
                            case "X": break;
                            case "Y": _dgrow.Visible = false; break;
                            case "Z": break;
                            case "H": _dgrow.Visible = false; break;
                            case "I": break;
                        }
                        //画面変更時の初期値
                        if (cellValReset) _dgrow.Cells[1].Value = textResetValue;
                    }
                    break;

                case "外径B":
                    foreach (DataGridViewRow _dgrow in dataGridViewEx1.Rows)
                    {
                        _dgrow.Visible = true;
                        switch (_dgrow.Cells[0].Value as string)
                        {
                            case "X": _dgrow.Visible = false; break;
                            case "Y": _dgrow.Visible = false; break;
                            case "Z": break;
                            case "H": break;
                            case "I": _dgrow.Visible = false; break;
                        }
                    }
                    break;

                case "外径C":
                    foreach (DataGridViewRow _dgrow in dataGridViewEx1.Rows)
                    {
                        _dgrow.Visible = true;
                        switch (_dgrow.Cells[0].Value as string)
                        {
                            case "X": _dgrow.Visible = false; break;
                            case "Y": _dgrow.Visible = false; break;
                            case "Z": break;
                            case "H": _dgrow.Visible = false; break;
                            case "I": break;
                        }
                    }
                    break;

                case "内径A":
                    //画面変更時の初期値
                    if (cellValReset) dataGridViewEx1.Rows[0].Cells[1].Value = textResetValue;
                    if (cellValReset) dataGridViewEx1.Rows[1].Cells[1].Value = textResetValue;
                    if (cellValReset) dataGridViewEx1.Rows[2].Cells[1].Value = textResetValue;
                    if (cellValReset) dataGridViewEx1.Rows[3].Cells[1].Value = textResetValue;
                    break;

                case "内径B":
                    foreach (DataGridViewRow _dgrow in dataGridViewEx1.Rows)
                    {
                        _dgrow.Visible = true;
                        switch (_dgrow.Cells[0].Value as string)
                        {
                            case "X": _dgrow.Visible = false; break;
                            case "Y": _dgrow.Visible = false; break;
                            case "Z": break;
                            case "H": break;
                            case "I": _dgrow.Visible = false; break;
                        }
                    }
                    break;

                case "内径C":
                    foreach (DataGridViewRow _dgrow in dataGridViewEx1.Rows)
                    {
                        _dgrow.Visible = true;
                        switch (_dgrow.Cells[0].Value as string)
                        {
                            case "X": _dgrow.Visible = false; break;
                            case "Y": _dgrow.Visible = false; break;
                            case "Z": break;
                            case "H": _dgrow.Visible = false; break;
                            case "I": break;
                        }
                    }
                    break;

                case "外コーナーA":
                    foreach (DataGridViewRow _dgrow in dataGridViewEx1.Rows)
                    {
                        _dgrow.Visible = true;
                        switch (_dgrow.Cells[0].Value as string)
                        {
                            case "X": break;
                            case "Y": break;
                            case "Z": break;
                            case "H": break;
                            case "I": _dgrow.Visible = false; break;
                        }
                        //画面変更時の初期値
                        if (cellValReset) _dgrow.Cells[1].Value = textResetValue;
                    }
                    break;

                case "外コーナーB":
                    foreach (DataGridViewRow _dgrow in dataGridViewEx1.Rows)
                    {
                        _dgrow.Visible = true;
                        switch (_dgrow.Cells[0].Value as string)
                        {
                            case "X": break;
                            case "Y": break;
                            case "Z": break;
                            case "H": break;
                            case "I": _dgrow.Visible = false; break;
                        }
                    }
                    break;

                case "外コーナーC":
                    foreach (DataGridViewRow _dgrow in dataGridViewEx1.Rows)
                    {
                        _dgrow.Visible = true;
                        switch (_dgrow.Cells[0].Value as string)
                        {
                            case "X": break;
                            case "Y": break;
                            case "Z": break;
                            case "H": break;
                            case "I": _dgrow.Visible = false; break;
                        }
                    }
                    break;

                case "外コーナーD":
                    foreach (DataGridViewRow _dgrow in dataGridViewEx1.Rows)
                    {
                        _dgrow.Visible = true;
                        switch (_dgrow.Cells[0].Value as string)
                        {
                            case "X": break;
                            case "Y": break;
                            case "Z": break;
                            case "H": break;
                            case "I": _dgrow.Visible = false; break;
                        }
                    }
                    break;

                case "内コーナーA":
                    foreach (DataGridViewRow _dgrow in dataGridViewEx1.Rows)
                    {
                        _dgrow.Visible = true;
                        switch (_dgrow.Cells[0].Value as string)
                        {
                            case "X": break;
                            case "Y": break;
                            case "Z": break;
                            case "H": break;
                            case "I": _dgrow.Visible = false; break;
                        }
                        //画面変更時の初期値
                        if (cellValReset) _dgrow.Cells[1].Value = textResetValue;
                    }
                    break;

                case "内コーナーB":
                    foreach (DataGridViewRow _dgrow in dataGridViewEx1.Rows)
                    {
                        _dgrow.Visible = true;
                        switch (_dgrow.Cells[0].Value as string)
                        {
                            case "X": break;
                            case "Y": break;
                            case "Z": break;
                            case "H": break;
                            case "I": _dgrow.Visible = false; break;
                        }
                    }
                    break;

                case "内コーナーC":
                    foreach (DataGridViewRow _dgrow in dataGridViewEx1.Rows)
                    {
                        _dgrow.Visible = true;
                        switch (_dgrow.Cells[0].Value as string)
                        {
                            case "X": break;
                            case "Y": break;
                            case "Z": break;
                            case "H": break;
                            case "I": _dgrow.Visible = false; break;
                        }
                    }
                    break;

                case "内コーナーD":
                    foreach (DataGridViewRow _dgrow in dataGridViewEx1.Rows)
                    {
                        _dgrow.Visible = true;
                        switch (_dgrow.Cells[0].Value as string)
                        {
                            case "X": break;
                            case "Y": break;
                            case "Z": break;
                            case "H": break;
                            case "I": _dgrow.Visible = false; break;
                        }
                    }
                    break;

                case "角度補正A":
                    foreach (DataGridViewRow _dgrow in dataGridViewEx1.Rows)
                    {
                        _dgrow.Visible = true;
                        switch (_dgrow.Cells[0].Value as string)
                        {
                            case "X": break;
                            case "Y": break;
                            case "Z": break;
                            case "H": break;
                            case "I": _dgrow.Visible = false; break;
                        }
                        //画面変更時の初期値
                        if (cellValReset) _dgrow.Cells[1].Value = textResetValue;
                    }
                    break;

                case "角度補正B":
                    foreach (DataGridViewRow _dgrow in dataGridViewEx1.Rows)
                    {
                        _dgrow.Visible = true;
                        switch (_dgrow.Cells[0].Value as string)
                        {
                            case "X": break;
                            case "Y": break;
                            case "Z": break;
                            case "H": break;
                            case "I": _dgrow.Visible = false; break;
                        }
                    }
                    break;

                case "角度補正C":
                    foreach (DataGridViewRow _dgrow in dataGridViewEx1.Rows)
                    {
                        _dgrow.Visible = true;
                    }
                    break;

                case "角度補正D":
                    foreach (DataGridViewRow _dgrow in dataGridViewEx1.Rows)
                    {
                        _dgrow.Visible = true;
                    }
                    break;
            }
        }


        private string SelectModeView(string _mode)
        {
            string _ret = "Empty";
            if (_mode == null)
            {
                return _ret;
            }
            switch (_mode.TrimEnd('A', 'B', 'C', 'D'))
            {
                case "tanmen": _ret = _mode.Replace("tanmen", "端面"); break;
                case "gaikei": _ret = _mode.Replace("gaikei", "外径"); break;
                case "naikei": _ret = _mode.Replace("naikei", "内径"); break;
                case "soto": _ret = _mode.Replace("soto", "外コーナー"); break;
                case "uchi": _ret = _mode.Replace("uchi", "内コーナー"); break;
                case "Ret": _ret = _mode.Replace("Ret", "角度補正"); break;
            }
            return _ret;
        }

        public void SetOffset(Point offset)
        {
            _offset = offset;
        }

        private void _currectAngleBt_Click(object sender, EventArgs e)
        {
            if (_currectAngleBt.GetBack() == false)
            {
                using (SettingFunction SetFunc = new SettingFunction(1))
                {
                    ResultCodes Error = SetFunc.SettingMonitoring(Settings.CorrectAngleEn);
                    UILog logs = new UILog("ReferencingForm.SetFunc.SettingMonitoring");
                    if (Error == ResultCodes.Success)
                    {
                        logs.Sure("CORRECTANGLE-ON Result = " + Error);
                    }
                    else
                    {
                        logs.Error("CORRECTANGLE-ON Result = " + Error);
                    }
                }
            }
            else
            {
                using (SettingFunction SetFunc = new SettingFunction(0))
                {
                    ResultCodes Error = SetFunc.SettingMonitoring(Settings.CorrectAngleEn);
                    UILog logs = new UILog("ReferencingForm.SetFunc.SettingMonitoring");
                    if (Error == ResultCodes.Success)
                    {
                        logs.Sure("CORRECTANGLE-OFF Result = " + Error);
                    }
                    else
                    {
                        logs.Error("CORRECTANGLE-OFF Result = " + Error);
                    }
                }
            }
        }

        public void _panelDispose()
        {
            if (SubAImage.BackgroundImage != null)
            {
                SubAImage.BackgroundImage.Dispose();
                SubAImage.BackgroundImage = null;
            }
            if (SubBImage.BackgroundImage != null)
            {
                SubBImage.BackgroundImage.Dispose();
                SubBImage.BackgroundImage = null;
            }
            if (SubCImage.BackgroundImage != null)
            {
                SubCImage.BackgroundImage.Dispose();
                SubCImage.BackgroundImage = null;
            }
            if (SubDImage.BackgroundImage != null)
            {
                SubDImage.BackgroundImage.Dispose();
                SubDImage.BackgroundImage = null;
            }
        }
        public void _pictureboxDispose()
        {
            if (pictureBox5.BackgroundImage != null)
            {
                pictureBox5.BackgroundImage.Dispose();
                pictureBox5.BackgroundImage = null;
            }
        }

        private void ReferencingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            using (McDatStatus status = new McDatStatus())
            {
                status.Read();
                if(status.Status.MotionMode == McTaskModes.Auto) _ChangeMode(McTaskModes.Auto, (McTaskModes.Manual));
            }
            e.Cancel = false;
            _panelDispose();
            _pictureboxDispose();
        }

        /// <summary>
        /// 軸名称
        /// </summary>
        private string _axisName = "";

        /// <summary>
        /// DataGridViewEx1：クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewEx1_Click(object sender, EventArgs e)
        {
            //セルが存在しない箇所のクリック⇒処理を抜ける。　20170228 Hachino ADD
            if (dataGridViewEx1.CurrentCell == null) return;
            //現在カレントのセル行インデックス
            int rowIndex = dataGridViewEx1.CurrentCell.RowIndex;
            //軸名称取得
            _axisName = dataGridViewEx1.Rows[rowIndex].Cells[0].Value.ToString();
            //軸番号取得
            int rowVal = getAxisNumber(_axisName);
            //編集対象セルをコピー
            string tmpString = dataGridViewEx1.Rows[rowVal].Cells[1].Value.ToString();
            //編集対象が無い場合処理を抜ける
            if (tmpString == "") return;
            //ポップアップTenKey
            if (popupTenkeyOn(tmpString)) return;
        }
        /// <summary>
        /// 軸番号取得
        /// </summary>
        /// <param name="axisName">軸名称</param>
        /// <returns>軸番号</returns>
        private int getAxisNumber(string axisName)
        {
            int rowVal = 0;
            switch (axisName)
            {
                case "X": rowVal = 0; break;
                case "Y": rowVal = 1; break;
                case "Z": rowVal = 2; break;
                case "H": rowVal = 3; break;
                case "I": rowVal = 4; break;
            }
            return rowVal;
        }

        private void SubAImage_Click(object sender, EventArgs e)
        {
            if (SubFormABt.GetBack() == false)
            {
                ImageView("A3D");
                SubFormABt.SetBack(true);
                _AllBtOff(SubFormABt);
                //モード番号取得用、図タイプ：設定
                setFigType((int)enumFigType.figA);
                //現在の状態でセルX/Y行どちらか表示
                comboBox_angleCorrectionXY_SelectedIndexChanged(null, null);
            }
        }

        private void SubBImage_Click(object sender, EventArgs e)
        {
            if (SubFormBBt.GetBack() == false)
            {
                ImageView("B3D");
                SubFormBBt.SetBack(true);
                _AllBtOff(SubFormBBt);
                //モード番号取得用、図タイプ：設定
                setFigType((int)enumFigType.figB);
                //現在の状態でセルX/Y行どちらか表示
                comboBox_angleCorrectionXY_SelectedIndexChanged(null, null);
            }
        }

        private void SubCImage_Click(object sender, EventArgs e)
        {
            if (SubFormCBt.GetBack() == false)
            {
                ImageView("C3D");
                SubFormCBt.SetBack(true);
                _AllBtOff(SubFormCBt);
                //モード番号取得用、図タイプ：設定
                setFigType((int)enumFigType.figC);
                //現在の状態でセルX/Y行どちらか表示
                comboBox_angleCorrectionXY_SelectedIndexChanged(null, null);
            }
        }

        private void SubDImage_Click(object sender, EventArgs e)
        {
            if (SubFormDBt.GetBack() == false)
            {
                ImageView("D3D");
                SubFormDBt.SetBack(true);
                _AllBtOff(SubFormDBt);
                //モード番号取得用、図タイプ：設定
                setFigType((int)enumFigType.figD);
                //現在の状態でセルX/Y行どちらか表示
                comboBox_angleCorrectionXY_SelectedIndexChanged(null, null);
            }
        }
        #region <region<ポップアップテンキー>region>
        /// <summary>
        /// ポップアップテンキー：表示/非表示
        /// </summary>
        /// <param name="val"></param>
        /// <returns>false=上下ポップアップを表示</returns>
        private bool popupTenkeyOn(string changeVal)
        {
            //編集値を取得
            Decimal lowerLimitDec = 0;  //最小値
            Decimal upperLimitDec = 0;  //最大値
            if (_popupTenkey != null)
            {
                _popupTenkey.Close();   //画面を閉じる
                _popupTenkey = null;    //null初期化
            }
            //フォーマットタイプ設定
            NumericTextBox.FormatTypes formatType = NumericTextBox.FormatTypes.SignDecimalUpper3Lower3;
            //入力最小/最大設定
            lowerLimitDec = (decimal)-999.999;
            upperLimitDec = (decimal)999.999;
            //ポップアップTenKey
            _popupTenkey = new TenKeyDialog(changeVal, formatType, lowerLimitDec, upperLimitDec, true, true, _submicronFlg, "referencing" );
            _popupTenkey.NotifyReturnOk = popupTenkey_OnNotifyReturnOk;     //イベント通知：OK
            _popupTenkey.Text = labelEx3.Text + " : " + _axisName + "軸";   //テンキータイトル表示
            _popupTenkey.ShowDialog(this);									//画面を開く
            return true;
        }
        /// <summary>
        /// ポップアップテンキーで「OK」ボタンを押した時、入力値をこのクラスに反映
        /// </summary>
        private void popupTenkey_OnNotifyReturnOk()
        {
            //編集内容を取得
            string retVal = _popupTenkey._tenkeyValReturn;////ポップアップテンキーで編集された値 + 単位
            //軸番号取得
            int rowVal = getAxisNumber(_axisName);
            //値をセルに設定
            dataGridViewEx1.Rows[rowVal].Cells[1].Value = retVal;
            // dataGridViewEx1[rowVal, 1].Value = retVal;
        }
        #endregion
        #region <region<(McEggeProgram)モード番号取得>region>
        /// <summary>
        /// (McEggeProgram)モード番号取得用、ボタン：タイプ
        /// </summary>
        private enum enumButtonType
        {
            //端面＝0、外形＝1、内径＝2、外コーナ＝3、内コーナ＝4、補正角度＝5
            endFace = 0, outerDiameter = 1, innerDiameter = 2, outerCorner = 3, innerCorner = 4, angleCorrection = 5
        };
        /// <summary>
        /// (McEggeProgram)モード番号取得用、図：タイプ
        /// </summary>
        private enum enumFigType
        {
            //A＝0、B＝1、C＝2、D＝3
            figA = 0, figB = 1, figC = 2, figD = 3
        };

        private int intNowButton = 0;
        private int intNowFig = 0;
        /// <summary>
        /// (McEggeProgram)モード番号取得用、ボタンタイプ：設定
        /// </summary>
        /// <param name="enumButtonType"></param>
        private void setButtonType(int enumButtonType)
        {
            intNowButton = enumButtonType;
        }
        /// <summary>
        /// (McEggeProgram)モード番号取得用、図タイプ：設定
        /// </summary>
        /// <param name="enumFigType"></param>
        private void setFigType(int enumFigType)
        {
            intNowFig = enumFigType;
        }
        /// <summary>
        /// (McEggeProgram)モード番号：取得
        /// </summary>
        private int getModeNum()
        {
            int modeNum = -1;
            //押されているボタン
            switch (intNowButton)
            {
                case (int)enumButtonType.endFace://端面
                    switch (intNowFig)
                    {
                        case (int)enumFigType.figA: modeNum = 1; break;
                        case (int)enumFigType.figB: modeNum = 2; break;
                        case (int)enumFigType.figC: modeNum = 3; break;
                        case (int)enumFigType.figD: modeNum = 4; break;
                        default:
                            //エラー
                            break;
                    }
                    break;
                case (int)enumButtonType.outerDiameter://外形
                    switch (intNowFig)
                    {
                        case (int)enumFigType.figA: modeNum = 14; break;
                        case (int)enumFigType.figB: modeNum = 15; break;
                        case (int)enumFigType.figC: modeNum = 16; break;
                        default:
                            //エラー
                            break;
                    }
                    break;
                case (int)enumButtonType.innerDiameter://内形
                    switch (intNowFig)
                    {
                        case (int)enumFigType.figA: modeNum = 13; break;
                        case (int)enumFigType.figB: modeNum = 17; break;
                        case (int)enumFigType.figC: modeNum = 18; break;
                        default:
                            //エラー
                            break;
                    }
                    break;
                case (int)enumButtonType.outerCorner://外コーナ
                    switch (intNowFig)
                    {
                        case (int)enumFigType.figA: modeNum = 5; break;
                        case (int)enumFigType.figB: modeNum = 6; break;
                        case (int)enumFigType.figC: modeNum = 7; break;
                        case (int)enumFigType.figD: modeNum = 8; break;
                        default:
                            //エラー
                            break;
                    }
                    break;
                case (int)enumButtonType.innerCorner://内コーナ
                    switch (intNowFig)
                    {
                        case (int)enumFigType.figA: modeNum = 9; break;
                        case (int)enumFigType.figB: modeNum = 10; break;
                        case (int)enumFigType.figC: modeNum = 11; break;
                        case (int)enumFigType.figD: modeNum = 12; break;
                        default:
                            //エラー
                            break;
                    }
                    break;
                case (int)enumButtonType.angleCorrection://補正角度
                    switch (intNowFig)
                    {
                        case (int)enumFigType.figA: modeNum = 20; break;
                        case (int)enumFigType.figB: modeNum = 21; break;
                        case (int)enumFigType.figC: modeNum = 22; break;
                        case (int)enumFigType.figD: modeNum = 23; break;
                        default:
                            //エラー
                            break;
                    }
                    break;
                default:
                    //エラー
                    break;
            }
            return modeNum;
        }
        #endregion
        #region <region<(McEggeProgram)デバック用：スタート>region>
        /// <summary>
        /// デバック用：スタートボタン：クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_NCStart_Click_1(object sender, EventArgs e)
        {
            dbgSt = true;
        }
        bool dbgSt = false;
        #endregion
        #region <region<補正角度：X/Y選択>region>
        /// <summary>
        /// 補正角度：XY変更：コンボボックス変更時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_angleCorrectionXY_SelectedIndexChanged(object sender, EventArgs e)
        {
            //リストの選択状態を取得
            int selectIndex = comboBox_angleCorrectionXY.SelectedIndex;
            //セルX/Y行どちらか非表示
            comboBox_angleCorrectionXY_Select(selectIndex);
        }
		/// <summary>
		/// 角度補正：セルX/Y行どちらか非表示※
		/// </summary>
		/// <param name="selectIndex"></param>
		private void comboBox_angleCorrectionXY_Select(int selectIndex)
        {
            return;//※角度補正：データグリッドビューを使用中・・・
            /*comboBox_angleCorrectionXY.SelectedIndex = selectIndex;
            // 読み取り専用（テキストボックスは編集不可）にする
            comboBox_angleCorrectionXY.DropDownStyle = ComboBoxStyle.DropDownList;
            if (selectIndex == 0)
            {//Xを選択
                dataGridViewEx1.Rows[0].Visible = true;//X
                dataGridViewEx1.Rows[1].Visible = false;//Y
            }
            else
            {//Yを選択
                dataGridViewEx1.Rows[0].Visible = false;//X
                dataGridViewEx1.Rows[1].Visible = true;//Y
            }
            //選択：しない
            dataGridViewEx1.CurrentCell = null;*/
        }
        /// <summary>
        /// 角度補正：データグリッドビュー２：マウス：ダウン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewEx2_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = ((DataGridViewCellMouseEventArgs)e).RowIndex;  //行
            int colIndex = ((DataGridViewCellMouseEventArgs)e).ColumnIndex; //列
            if (rowIndex == -1) return;

            dataGridViewEx2.CurrentCell = null;
            //各行
            switch ( rowIndex )
            {
                case 0://X選択ボタン※セル上のボタン
                    dataGridViewEx2_SetSelect(false);
                    break;
				case 1://Y選択ボタン※セル上のボタン
					dataGridViewEx2_SetSelect( true );
					break;
			}
		}
        /// <summary>
        /// 角度補正：データグリッドビュー２：X/Y選択
        /// </summary>
        /// <param name="xySelect">false=X軸選択、true=Y軸選択</param>
        private void dataGridViewEx2_SetSelect(bool xySelect)
        {
            if (dataGridViewEx2[0, 0].Value == null) dataGridViewEx2[0, 0].Value = "";
            if (dataGridViewEx2[0, 1].Value == null) dataGridViewEx2[0, 1].Value = "";
            if (xySelect)
            {//Y軸選択
                dataGridViewEx2[0, 0].Value = "";  //X
                dataGridViewEx2[0, 1].Value = "●";//Y 
            }
            else
            {//X軸選択
                dataGridViewEx2[0, 0].Value = "●";//X
                dataGridViewEx2[0, 1].Value = "";  //Y
            }
        }

        #endregion

        private void _CloseBtn_Click(object sender, EventArgs e)
        {
            CloseForm();
        }
        public void CloseForm()
        {
            _threadStop = true;
            this.Close();
        }
    }
}
