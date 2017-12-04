///////////////////////////////////////////////////////////////////////////////////////////
//
// (1) システム名称 : ELENIX ECNCⅢ
// (2) ファイル名   : NumericFeedForm.cs
// (3) 概要         : 数量位置決め設定画面
// (4) 作成日       : 2015.06.01
// (5) 作成者       : 八野
// (6) 特記事項     : VisualStudio2015 C#
// (7) 更新履歴     :
//
///////////////////////////////////////////////////////////////////////////////////////////


using ECNC3.Enumeration;
using ECNC3.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ECNC3.Models;//FileSettings用：2017-02-13：柏原
using ECNC3.Views.Popup;

namespace ECNC3.Views
{
    public partial class NumericFeedForm : ECNC3Form
    {
        //-----------------------------------------------------------------------------------------
        //
        //　フォーム　コンストラクタ
        //
        //-----------------------------------------------------------------------------------------
        public NumericFeedForm
            (
            FunctionMode mode, 
            AxisMode axis = AxisMode.NULL, 
            PositionMode Pos = PositionMode.Incrimental, 
            CoordinateMode coodinate = CoordinateMode.Work 
            )
        {
            InitializeComponent();
            Disposed += FormDisposed;
            if(mode == FunctionMode.NumericFeed)
            {
                _functionMode = mode;
                axisMode = axis;
                _positionMode = Pos;
                _coordinateMode = coodinate;
            }
            else
            {
                _functionMode = mode;
                axisMode = axis;
                _coordinateMode = CoordinateMode.Work;
            }
        }
        public void NumericFeedForm_Init
            (
            FunctionMode mode, 
            AxisMode axis = AxisMode.NULL, 
            PositionMode Pos = PositionMode.Incrimental, 
            CoordinateMode coodinate = CoordinateMode.Work
            )
        {
            if (mode == FunctionMode.NumericFeed)
            {
                _functionMode = mode;
                axisMode = axis;
                _positionMode = Pos;
                _coordinateMode = coodinate;
            }
            else
            {
                _functionMode = mode;
                axisMode = axis;
                _coordinateMode = CoordinateMode.Work;
            }
            //LoadSequence
            if (workPositions != null)
            {
                workPositions.Clear();
                workPositions = null;
            }
            if (workPositions == null)
            {
                workPositions = new List<string>();
                foreach (WorkPositions temp in Enum.GetValues(typeof(WorkPositions)).Cast<WorkPositions>().ToList())
                {
                    workPositions.Add(temp.ToString());
                }
            }
            SetOffset(new Point(0, 0));
            //スタイル初期化
            FormControl_Init();
            //モード初期化
            switch (_functionMode)
            {
                case FunctionMode.NumericFeed:
                    NumericFeedMode_Init();
                    break;

                case FunctionMode.PositionSet:
                    WorkSettingMode_Init();
                    break;

            }
            using (FileSettings fs = new FileSettings())
            {
                //ファイル読み込み
                fs.Read();
                LoadDigit(fs);
            }
            switch (_digit)
            {
                case DigitSelect.Three: EditText.Text = "0.000"; break;

                case DigitSelect.Four: EditText.Text = "0.0000"; break;

            }
            _SelectWorkPosition();
            popupTenkeyOn(EditText);
        }
        #region VariableMember
        //int i = 0;
        /// <summary>
        /// 機械座標
        /// </summary>
        private Models.StructureAxisCoordinate MacPos = new Models.StructureAxisCoordinate();
        /// <summary>
        /// ワーク座標
        /// </summary>
        private Models.StructureAxisCoordinate WorkPos = new Models.StructureAxisCoordinate();
        /// <summary>
        /// ポップアップテンキー
        /// </summary>
        private TenKeyDialog _popupTenkey = new TenKeyDialog("", 0, 0, 0);//初回インスタンスを作っておく
        /// <summary>
        /// ポップアップ上下キーのLocation調整
        /// </summary>
        public Point _offset = new Point();
        /// <summary>
        /// 軸移動用パラメータ
        /// </summary>
        private Models.StructurePositioniingItem[] PosItems = new Models.StructurePositioniingItem[8];
        /// <summary>
        /// フォームのモード
        /// </summary>
        public FunctionMode _functionMode { get; private set; }
        /// <summary>
        /// 選択軸
        /// </summary>
        public AxisMode axisMode;
        /// <summary>
        /// 座標モード
        /// </summary>
        private PositionMode _positionMode;
        /// <summary>
        /// 座標系モード
        /// </summary>
        public CoordinateMode _coordinateMode;
        /// <summary>
        /// 現在の有効桁数
        /// </summary>
        private DigitSelect _digit = DigitSelect.Four;
        static bool _threadStop = false;
        bool _IsActivatedAllControls = true;
        bool threadStop {
            get
            {
                return _threadStop;
            }
            set
            {
                _threadStop = value;
            }
        }

        #endregion
        #region Enums 
        /// <summary>
        /// フォームのモード
        /// </summary>
        public enum FunctionMode
        {
            /// <summary>
            /// 位置決め
            /// </summary>
            NumericFeed,
            /// <summary>
            /// ワーク座標設定
            /// </summary>
            PositionSet
        }
        /// <summary>
        /// 選択軸
        /// </summary>
        public enum AxisMode
        {
            NULL = 0,
            X = 1,
            Y = 2,
            W = 3,
            Z = 4,
            A = 5,
            B = 6,
            C = 7,
            I = 8
        }
        /// <summary>
        /// 座標モード
        /// </summary>
        public enum PositionMode
        {
            /// <summary>
            /// 相対座標
            /// </summary>
            Incrimental,
            /// <summary>
            /// 絶対座標
            /// </summary>
            Absolute
        }
        /// <summary>
        /// 座標系モード
        /// </summary>
        public enum CoordinateMode
        {
            /// <summary>
            /// ワーク座標系
            /// </summary>
            Work = 1,
            /// <summary>
            /// 機械座標系
            /// </summary>
            Machine = 2            
        }
        #endregion
        bool closed = false;
        public void RTOStatusMonitoring(StatusMonitorEventArgs e)
        {
            if (closed == true) return;
            if (threadStop == true)
            {
                if ((this.IsDisposed == true || this.Disposing == true)) return;
                retInvoke = BeginInvoke((MethodInvoker)delegate
                {
                    threadStop = false;
                    this.Close();
                    closed = true;
                    return;
                }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
            }
            if (_functionMode == FunctionMode.PositionSet) return;
            //Startボタンを押したときの動作
            if (e.Items.StartSwBtnOffEdge == true)
            {
                if(_popupTenkey.Visible == true)
                {
                    if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                    {
                        using (MessageDialog msgDir = new MessageDialog())
                        {
                            msgDir.Error(5036, this);
                        }
                    }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                    return;
                }

                int _targetPos = 0;
                
                
                if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                {
                    string tempTarget = EditText.Text.Replace(".", "");
                    int.TryParse(tempTarget, out _targetPos);
                    PositioningAxis(e.Items, _targetPos);
                }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
            }
            if (e.Items.FGEnd == true)
            {
                if (_IsActivatedAllControls == false)
                {
                    if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                    {
                        _AllButtonsActivate(this.Controls);
                    }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                }                    
            }
            else
            {
                if (_IsActivatedAllControls == true)
                {
                    if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                    {
                        _AllButtonsDeActivate(this.Controls);
                    }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                }
            }
        }
        private void _AllButtonsActivate(Control.ControlCollection target)
        {
            if (target == null) return;
            //重複処理排他
            if (_IsActivatedAllControls == true) return;
            foreach (Control ctrl in target)
            {
                if (ctrl.Controls.Count != 0) _AllButtonsActivate(ctrl.Controls);
                if (ctrl.GetType() == typeof(ButtonEx))
                {
                    if ((ctrl as ButtonEx).Enabled != true) (ctrl as ButtonEx).Enabled = true;
                }
                if (ctrl.GetType() == typeof(DataGridViewEx))
                {
                    if ((ctrl as DataGridViewEx).EditMode != DataGridViewEditMode.EditOnKeystrokeOrF2)
                        (ctrl as DataGridViewEx).EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
                }
            }
            _IsActivatedAllControls = true;
        }
        private void _AllButtonsDeActivate(Control.ControlCollection target)
        {
            if (target == null) return;
            //重複処理排他
            if (_IsActivatedAllControls == false) return;

            foreach (Control ctrl in target)
            {
                if (ctrl.Controls.Count != 0) _AllButtonsDeActivate(ctrl.Controls);
                if (ctrl.GetType() == typeof(ButtonEx))
                {
                    if ((ctrl as ButtonEx).Enabled != false) (ctrl as ButtonEx).Enabled = false;
                }
                if (ctrl.GetType() == typeof(DataGridViewEx))
                {
                    if ((ctrl as DataGridViewEx).EditMode != DataGridViewEditMode.EditProgrammatically)
                        (ctrl as DataGridViewEx).EditMode = DataGridViewEditMode.EditProgrammatically;
                }
            }
            _IsActivatedAllControls = false;
        }
        /// <summary>
        /// ワーク座標選択
        /// </summary>
        /// <param name="index"></param>
        private void _SelectWorkPosition(string select = "Work")
        {
            _WorkPositionBt.Text = workPositionMode = select;
        }
        /// <summary>
        /// 桁数チェック関数
        /// </summary>
        /// <param name="_pos">入力値</param>
        /// <param name="_target">ﾊﾟﾙｽ変換値</param>
        /// <returns>true/false = 正常/異常</returns>
        private bool ChkPosText(string _pos, out int _target)
        {
            //符号有無
            bool unsigned = true;
            //小数点有無
            bool point = true;
            //整数の桁数
            int pointIndex = 0;
            //小数点以下の桁数
            int digitCt = 0;
            //少数⇒整数の変換係数
            int coef = 10;
            //符号チェック
            if (_pos.Contains("-"))
            {
                unsigned = false;
            }
            else
            {
                unsigned = true;
            }
            //小数点チェック
            if (_pos.Contains("."))
            {
                point = true;
            }
            else
            {
                point = false;
            }
            //パルスへ変換
            if (point == true)
            {
                //小数点がある場合は桁数確認後変換
                if (unsigned == false)
                {
                    pointIndex = _pos.Replace("-", "").IndexOf(".");
                }
                else
                {
                    pointIndex = _pos.IndexOf(".");
                }
                if(!_pos.EndsWith(".")
                    && _pos.EndsWith("0"))
                {
                    _pos = _pos.Remove(_pos.LastIndexOf("0"), 1);
                    coef = 100;
                }
                digitCt = _pos.Count() - (pointIndex + 1);
                if (unsigned == false)
                {
                    digitCt = digitCt - 1;
                }

                //桁数エラーの場合は入力値不正で処理中断
                if (pointIndex > 3
                || pointIndex < 0
                || digitCt > 3)
                {
                    _target = 0;
                    return false;
                }
                //小数点以下の桁がある場合
                if (digitCt != 0)
                {
                    //ﾊﾟﾙｽに変換するための係数を演算
                    for (int iCt = 1; iCt < digitCt; iCt++)
                    {
                        coef *= 10;
                    }
                    //ﾊﾟﾙｽ（整数）に変換
                    _target = ((int)(double.Parse(_pos) * coef));
                }
                else
                {
                    _target = int.Parse(_pos.Replace(".", "")) * 1000;
                }
                return true;
            }
            //小数点がない場合はそのままﾊﾟﾙｽに変換
            else
            {
                _target = int.Parse(_pos) * 1000;
                return true;
            }
        }

        internal void PositioningAxis(MonitoringItems Items, int targetPos)
        {
            //座標クラス定義
            Models.StructurePositioniingItem XaxisPos = new Models.StructurePositioniingItem();
            Models.StructurePositioniingItem YaxisPos = new Models.StructurePositioniingItem();
            Models.StructurePositioniingItem WaxisPos = new Models.StructurePositioniingItem();
            Models.StructurePositioniingItem ZaxisPos = new Models.StructurePositioniingItem();
            Models.StructurePositioniingItem AaxisPos = new Models.StructurePositioniingItem();
            Models.StructurePositioniingItem BaxisPos = new Models.StructurePositioniingItem();
            Models.StructurePositioniingItem CaxisPos = new Models.StructurePositioniingItem();
            Models.StructurePositioniingItem IaxisPos = new Models.StructurePositioniingItem();

            //初期化Start
            XaxisPos.Movable = false;
            XaxisPos.TargetPosition = 0;
            YaxisPos.Movable = false;
            YaxisPos.TargetPosition = 0;
            WaxisPos.Movable = false;
            WaxisPos.TargetPosition = 0;
            ZaxisPos.Movable = false;
            ZaxisPos.TargetPosition = 0;
            AaxisPos.Movable = false;
            AaxisPos.TargetPosition = 0;
            BaxisPos.Movable = false;
            BaxisPos.TargetPosition = 0;
            CaxisPos.Movable = false;
            CaxisPos.TargetPosition = 0;
            IaxisPos.Movable = false;
            IaxisPos.TargetPosition = 0;
            //初期化End


            if( (this.IsDisposed == true || this.Disposing == true)   ) return; retInvoke = BeginInvoke((MethodInvoker)delegate
            {
                int pos = 0;
                pos = targetPos;
                switch (axisMode)
                {
                    case AxisMode.NULL:
                        using (MessageDialog msgDir = new MessageDialog())
                        {
                            msgDir.Information(5030, this);
                        }
                        return;

                    case AxisMode.X:
                        XaxisPos.Movable = true;
                        XaxisPos.TargetPosition = pos;
                        break;

                    case AxisMode.Y:
                        YaxisPos.Movable = true;
                        YaxisPos.TargetPosition = pos;
                        break;

                    case AxisMode.W:
                        WaxisPos.Movable = true;
                        WaxisPos.TargetPosition = pos;
                        break;

                    case AxisMode.Z:
                        ZaxisPos.Movable = true;
                        ZaxisPos.TargetPosition = pos;
                        break;

                    case AxisMode.A:
                        AaxisPos.Movable = true;
                        AaxisPos.TargetPosition = pos;
                        break;

                    case AxisMode.B:
                        BaxisPos.Movable = true;
                        BaxisPos.TargetPosition = pos;
                        break;

                    case AxisMode.C:
                        CaxisPos.Movable = true;
                        CaxisPos.TargetPosition = pos;
                        break;

                    case AxisMode.I:
                        IaxisPos.Movable = true;
                        IaxisPos.TargetPosition = pos;
                        break;

                }
            }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
            PosItems[0] = XaxisPos;
            PosItems[1] = YaxisPos;
            PosItems[2] = WaxisPos;
            PosItems[3] = ZaxisPos;
            PosItems[4] = AaxisPos;
            PosItems[5] = BaxisPos;
            PosItems[6] = CaxisPos;
            PosItems[7] = IaxisPos;

            //軸移動パラメータ有
            using (SequenceFunction SeqFunc = new SequenceFunction(PosItems.ToList()))
            {
                using (MonitoringFunction MonitorFunc = new MonitoringFunction())
                {
                    try
                    {
                        if (Items.FGEnd == true)
                        {
                            ResultCodes Error = ResultCodes.NotFound;
                            UILog logs = new UILog("MAINForm.MANUAL.NumForm.SeqFunc.SequenceMonitoring");
                            //	入力座標
                            switch (_positionMode)
                            {
                                case PositionMode.Absolute:
                                    switch (_coordinateMode)
                                    {
                                        case CoordinateMode.Work:
                                            Error = SeqFunc.SequenceMonitoring(Sequences.AbsoluteWorkMovePointToPoint);
                                            //ログ処理
                                            logs.Error("ABSPTPMOVE_WORK_START, " + "Result = " + Error);
                                            return;

                                        case CoordinateMode.Machine:
                                            Error = SeqFunc.SequenceMonitoring(Sequences.AbsoluteMacMovePointToPoint);
                                            //ログ処理
                                            logs.Error("ABSPTPMOVE_MACHINE_START, " + "Result = " + Error);
                                            break;

                                    }
                                    break;

                                case PositionMode.Incrimental:

                                    Error = SeqFunc.SequenceMonitoring(Sequences.IncMovePointToPoint);
                                    //ログ処理
                                    logs.Error("INCPTPMOVE_START, " + "Result = " + Error);
                                    break;
                            }
                        }
                        else
                        {
                            if (true == InvokeRequired)
                            {
                                Invoke((MethodInvoker)delegate
                                {
                                    UILog logs = new UILog("MAINForm.MANUAL.NumForm.SeqFunc.SequenceMonitoring");
                                    logs.Error("FGEnd == false");
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
                    finally
                    {
                        XaxisPos.Dispose();
                        YaxisPos.Dispose();
                        WaxisPos.Dispose();
                        ZaxisPos.Dispose();
                        AaxisPos.Dispose();
                        BaxisPos.Dispose();
                        CaxisPos.Dispose();
                        IaxisPos.Dispose();
                    }
                }
            }
            //動作後のテキストボックスクリア
            if (true == InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    EditText.Text = "";
                    SignLabel.Text = "";
                    EditText.SelectAll();
                }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
            }
        }

        public void SetOffset(Point offset)
        {
            _offset = offset;
        }
        /// <summary>
		/// ポップアップテンキーで「OK」ボタンを押した時、入力値をこのクラスに反映
		/// </summary>
		private void popupTenkey_OnNotifyReturnOk()
        {
            string retVal = _popupTenkey._tenkeyValReturn;//ポップアップテンキーで編集された値
            EditText.Text = retVal;
        }
        private void _WorkSettingExecute(int intWorkPosition)
        {
            using (Models.McIf.McDatWorkOrigin mcWorkOrg = new Models.McIf.McDatWorkOrigin())
            using (Models.McIf.McReqWorkPositionChange workOrg = new Models.McIf.McReqWorkPositionChange())
            {
                mcWorkOrg.Read();
                workOrg.WorkPosition = new StructureAxisCoordinate();
                //ワーク座標原点オフセット値計算
                switch (axisMode)
                {
                    //20増加する場合、入力は「20」オフセット値は「現オフセット値 - 20」
                    case AxisMode.X:
                        workOrg.WorkPosition.AxisX = mcWorkOrg.Coordinate.AxisX - intWorkPosition;
                        workOrg.WorkPosition.EnableAxis = 1;
                        break;

                    case AxisMode.Y:
                        workOrg.WorkPosition.AxisY = mcWorkOrg.Coordinate.AxisY - intWorkPosition;
                        workOrg.WorkPosition.EnableAxis = 2;
                        break;

                    case AxisMode.W:
                        workOrg.WorkPosition.AxisW = mcWorkOrg.Coordinate.AxisW - intWorkPosition;
                        workOrg.WorkPosition.EnableAxis = 4;
                        break;

                    case AxisMode.Z:
                        workOrg.WorkPosition.AxisZ = mcWorkOrg.Coordinate.AxisZ - intWorkPosition;
                        workOrg.WorkPosition.EnableAxis = 8;
                        break;

                    case AxisMode.A:
                        workOrg.WorkPosition.AxisA = mcWorkOrg.Coordinate.AxisA - intWorkPosition;
                        workOrg.WorkPosition.EnableAxis = 16;
                        break;

                    case AxisMode.B:
                        workOrg.WorkPosition.AxisB = mcWorkOrg.Coordinate.AxisB - intWorkPosition;
                        workOrg.WorkPosition.EnableAxis = 32;
                        break;

                    case AxisMode.C:
                        workOrg.WorkPosition.AxisC = mcWorkOrg.Coordinate.AxisC - intWorkPosition;
                        workOrg.WorkPosition.EnableAxis = 64;
                        break;

                    case AxisMode.I:
                        workOrg.WorkPosition.AxisC = mcWorkOrg.Coordinate.Axis8 - intWorkPosition;
                        workOrg.WorkPosition.EnableAxis = 128;
                        break;

                    default: break;
                }
                //オフセット値設定
                if (axisMode != AxisMode.NULL)
                {
                    ResultCodes result = workOrg.Execute();
                    if (result != ResultCodes.Success)
                    {
                        return;
                    }
                    ////桁設定
                    //switch (_digit)
                    //{
                    //    case DigitSelect.Three: EditText.Text = "0.000"; break;
                    //    case DigitSelect.Four: EditText.Text = "0.0000"; break;
                    //}
                }
            }
        }
        private void FormDisposed(object sender, EventArgs e)
        {
            //ポップアップテンキー
            if (null != _popupTenkey)
            {
                _popupTenkey.Close();
                _popupTenkey = null;
            }
        }

        /// <summary>
		/// 記録する加工条件値
		/// </summary>
		object _controlName = "";
        /// <summary>
        /// ポップアップテンキー：表示/非表示
        /// </summary>
        /// <param name="val"></param>
        /// <returns>false=上下ポップアップを表示</returns>
        private bool popupTenkeyOn(object val)
        {
            _controlName = val;     //コントロール名を記録
            string changeVal = "";  //編集値
            Decimal lowerLimitDec = 0;//最小値
            Decimal upperLimitDec = 0;//最大値

            //フォーマットタイプ
            NumericTextBox.FormatTypes formatType = NumericTextBox.FormatTypes.Integer1;
            if (_popupTenkey != null)
            {
                _popupTenkey.Close();   //画面を閉じる
                _popupTenkey = null;    //null初期化
            }
            //クリックしたコントロールの下段コントロール値を取得
            switch (((Control)_controlName).Name)
            {
                case "EditText":
                    changeVal = EditText.Text;
                    lowerLimitDec = (decimal)-999.999;
                    upperLimitDec = (decimal)999.999;
                    formatType = NumericTextBox.FormatTypes.SignDecimalUpper3Lower3;
                    break;

            }
            //ポップアップTenKey：2017-1-12:柏原
            _popupTenkey = new TenKeyDialog(changeVal, formatType, lowerLimitDec, upperLimitDec, true, true, false, "numeric_Feed");
            _popupTenkey.NotifyReturnOk = popupTenkey_OnNotifyReturnOk; //イベント通知：OK
            _popupTenkey.Text = _TitlePanel.Text;                        //テンキータイトル表示
            _popupTenkey.Location = new Point(this.Location.X + 250, this.Location.Y + 107);
            _popupTenkey._tenkeyMode = 1;
            _popupTenkey.Show(this); //画面を開く
            return true;
        }

        private void NumericFeedForm_Load(object sender, EventArgs e)
        {
            if(workPositions != null)
            {
                workPositions.Clear();
                workPositions = null;
            }
            if (workPositions == null)
            {
                workPositions = new List<string>();
                foreach (WorkPositions temp in Enum.GetValues(typeof(WorkPositions)).Cast<WorkPositions>().ToList())
                {
                    workPositions.Add(temp.ToString());
                }
            }
            SetOffset(new Point(0, 0));
            //スタイル初期化
            AxisSelectButtons_Init();
            FormControl_Init();
            //モード初期化
            switch (_functionMode)
            {
                case FunctionMode.NumericFeed:
                    NumericFeedMode_Init();
                    break;

                case FunctionMode.PositionSet:
                    WorkSettingMode_Init();
                    break;

            }
            string[] axisName = { "X", "Y", "W", "Z", "A", "B", "C", "I" };
            using (FileSettings fs = new FileSettings())
            {
                //ファイル読み込み
                fs.Read();
                LoadDigit(fs);
            }
            switch (_digit)
            {
                case DigitSelect.Three: EditText.Text = "0.000"; break;

                case DigitSelect.Four: EditText.Text = "0.0000"; break;

            }
            _SelectWorkPosition();
            popupTenkeyOn(EditText);
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
        private void AxisSelectButtons_Init()
        {
            int axisCount = 7;
            List<string> axisName = new List<string>();
            using (FileSettings fs = new FileSettings())
            {
                //ファイル読み込み
                fs.Read();
                axisCount = fs.AttrValue("Root/AxisInfomation/EnableAxis", "count");
                axisName.Add(fs.AttrText("Root/AxisInfomation/EnableAxis", "axis1"));
                axisName.Add(fs.AttrText("Root/AxisInfomation/EnableAxis", "axis2"));
                axisName.Add(fs.AttrText("Root/AxisInfomation/EnableAxis", "axis3"));
                axisName.Add(fs.AttrText("Root/AxisInfomation/EnableAxis", "axis4"));
                axisName.Add(fs.AttrText("Root/AxisInfomation/EnableAxis", "axis5"));
                axisName.Add(fs.AttrText("Root/AxisInfomation/EnableAxis", "axis6"));
                axisName.Add(fs.AttrText("Root/AxisInfomation/EnableAxis", "axis7"));
                axisName.Add(fs.AttrText("Root/AxisInfomation/EnableAxis", "axis8"));
            }
            //列の作成
            for (int ct = 0; ct < axisCount; ct++)
            {
                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                col.Name = "axis" + (axisCount + 1).ToString();
                _AxisSelectButtons.Columns.Add(col);
            }
            _AxisSelectButtons.RowCount = 1;
            int axisNameCt = 0;
            //行の作成
            foreach (string temp in axisName)
            {
                if (temp == "") continue;
                _AxisSelectButtons.Rows[0].Cells[axisNameCt].Value = temp;
                axisNameCt++;
            }
            _AxisSelectButtons.Rows[0].Height = 60;
            //セルのスタイル設定
            _AxisSelectButtons.DefaultCellStyle.BackColor = FileUIStyleTable.DefaultBackColor;
            _AxisSelectButtons.DefaultCellStyle.ForeColor = FileUIStyleTable.DefaultForeColor;
            _AxisSelectButtons.DefaultCellStyle.Font = new Font("Meiryo UI", 30, FontStyle.Regular);
            _AxisSelectButtons.DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
            //DataGridView1の列の幅をユーザーが変更できないようにする
            _AxisSelectButtons.AllowUserToResizeColumns = false;
            //DataGridView1の行の高さをユーザーが変更できないようにする
            _AxisSelectButtons.AllowUserToResizeRows = false;
        }
        private void FormControl_Init()
        {
            switch (_functionMode)
            {
                case FunctionMode.NumericFeed:
                    _TitlePanel.Visible = true;
                    _SubTitlePanel.Visible = false;
                    _IncrimentModeBt.Visible = true;
                    _AbsoluteModeBt.Visible = true;
                    _MachinePositionBt.Visible = true;
                    _WorkPositionBt.Visible = true;
                    _AllCrearBt.Visible = false;
                    _CrearBt.Visible = false;
                    _enterBtnPanel.Visible = false;
                    break;

                case FunctionMode.PositionSet:
                    _TitlePanel.Visible = false;
                    _SubTitlePanel.Visible = true;
                    _IncrimentModeBt.Visible = false;
                    _AbsoluteModeBt.Visible = false;
                    _MachinePositionBt.Visible = false;
                    _WorkPositionBt.Visible = false;
                    _AllCrearBt.Visible = true;
                    _CrearBt.Visible = true;
                    _enterBtnPanel.Visible = true;
                    break;

            }
        }
        private void NumericFeedMode_Init()
        {
            SetCoordinateMode(_coordinateMode);
            SetPositionMode(_positionMode);
            SetSelectAxis((int)axisMode - 1);
        }
        private void WorkSettingMode_Init()
        {
            SetSelectAxis((int)axisMode - 1);
        }
        private void EditText_Click(object sender, EventArgs e)
        {
            //ポップアップTenKey
            if (popupTenkeyOn(sender))
            {
                return;//popupTenkeyOn()=falseはUpDownキーポップアップ処理を実行
            }
        }

        private void _AllCrearBt_MouseUp(object sender, MouseEventArgs e)
        {
            using (Models.McIf.McDatStatus mcSta = new Models.McIf.McDatStatus())
            using (Models.McIf.McReqWorkPositionChange workOrg = new Models.McIf.McReqWorkPositionChange())
            {
                mcSta.Read();
                workOrg.WorkPosition = mcSta.Status.CoordinateAsAbsReg.Clone() as StructureAxisCoordinate;
                workOrg.WorkPosition.EnableAxis = 0x7f;
                workOrg.Execute();
            }
        }

        private void _CrearBt_MouseUp(object sender, MouseEventArgs e)
        {
            using (Models.McIf.McDatStatus mcSta = new Models.McIf.McDatStatus())
            using (Models.McIf.McReqWorkPositionChange workOrg = new Models.McIf.McReqWorkPositionChange())
            {
                mcSta.Read();
                workOrg.WorkPosition = mcSta.Status.CoordinateAsAbsReg.Clone() as StructureAxisCoordinate;
                workOrg.WorkPosition.EnableAxis = SelectAxisToBit();
                workOrg.Execute();
            }
        }

        private void _IncrimentModeBt_MouseUp(object sender, MouseEventArgs e)
        {
            SetPositionMode(PositionMode.Incrimental);
        }
        private void _AbsoluteModeBt_MouseUp(object sender, MouseEventArgs e)
        {
            SetPositionMode(PositionMode.Absolute);
        }

        private void _MachinePositionBt_MouseUp(object sender, MouseEventArgs e)
        {
            SetCoordinateMode(CoordinateMode.Machine);
        }
        /// <summary>
        /// ワーク座標選択ダイアログ
        /// </summary>
        private SelectCommandsDialogVariable selectWorkPosDialog = null;
        private void _WorkPositionBt_MouseUp(object sender, MouseEventArgs e)
        {
            if (selectWorkPosDialog != null)
            {
                if (selectWorkPosDialog.IsDisposed != true) selectWorkPosDialog.Dispose();
                selectWorkPosDialog = null;
            }
            if(selectWorkPosDialog == null) selectWorkPosDialog = new SelectCommandsDialogVariable(workPositions
                                                            , this.Location.X + _WorkPositionBt.Bounds.Left
                                                            , this.Location.Y + _WorkPositionBt.Bounds.Bottom);
            selectWorkPosDialog.FormClosed += _WorkPositionSelectDialog_Closed;
            selectWorkPosDialog.Show(this);
        }
        private void _WorkPositionSelectDialog_Closed(object sender, FormClosedEventArgs e)
        {
            if (selectWorkPosDialog.retMessage == "") return;
            _SelectWorkPosition(selectWorkPosDialog.retMessage);
            SetCoordinateMode(CoordinateMode.Work);
            selectWorkPosDialog = null;
        }

        private void _AxisSelectButtons_SelectionChanged(object sender, EventArgs e)
        {
            if (_AxisSelectButtons.SelectedCells.Count == 0)
            {
                return;
            }
            else
            {
                _AxisSelectButtons.SelectedCells[0].Selected = false;
            }
        }

        private void _AxisSelectButtons_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            SetSelectAxis(e.ColumnIndex);
        }
        /// <summary>
        /// フォーム内の軸選択設定
        /// </summary>
        /// <param name="col">
        /// 対象軸
        /// Axis1 = 1
        /// 
        /// </param>
        public void SetSelectAxis(int col)
        {
            foreach (DataGridViewCell cell in _AxisSelectButtons.Rows[0].Cells)
            {
                if (cell.ColumnIndex == col)
                {
                    _AxisSelectButtons[col, 0].Style.BackColor = FileUIStyleTable.EnabledBackColor;
                    _AxisSelectButtons[col, 0].Style.ForeColor = FileUIStyleTable.EnabledForeColor;
                    IndexToAxisMode(col);
                }
                else
                {
                    _AxisSelectButtons[cell.ColumnIndex, 0].Style.BackColor = FileUIStyleTable.DefaultBackColor;
                    _AxisSelectButtons[cell.ColumnIndex, 0].Style.ForeColor = FileUIStyleTable.DefaultForeColor;
                }
            }
        }
        List<string> workPositions = null;
        string workPositionMode = "";
        /// <summary>
        /// フォーム内の座標系選択設定
        /// </summary>
        /// <param name="mode"></param>
        public void SetCoordinateMode(CoordinateMode mode)
        {
            if(_coordinateMode != mode) _coordinateMode = mode;
            switch (_coordinateMode)
            {
                case CoordinateMode.Machine:
                    if (_MachinePositionBt.GetBack() != true) _MachinePositionBt.SetBack(true);
                    if (_WorkPositionBt.GetBack() != false) _WorkPositionBt.SetBack(false);
                    break;

                case CoordinateMode.Work:
                    if (_MachinePositionBt.GetBack() != false) _MachinePositionBt.SetBack(false);
                    if (_WorkPositionBt.GetBack() != true) _WorkPositionBt.SetBack(true);
                    break;

            }
        }
       public void SetPositionMode(PositionMode mode)
        {
            if (_positionMode != mode) _positionMode = mode;
            switch (_positionMode)
            {
                case PositionMode.Absolute:
                    if (_IncrimentModeBt.GetBack() != false) _IncrimentModeBt.SetBack(false);
                    if (_AbsoluteModeBt.GetBack() != true) _AbsoluteModeBt.SetBack(true);
                    break;

                case PositionMode.Incrimental:
                    if(_IncrimentModeBt.GetBack() != true) _IncrimentModeBt.SetBack(true);
                    if (_AbsoluteModeBt.GetBack() != false) _AbsoluteModeBt.SetBack(false);
                    break;

            }
        }
        private void IndexToAxisMode(int index)
        {
            axisMode = (AxisMode)(index + 1);
        }
        private int SelectAxisToBit()
        {
            int retBit = 0;
            switch (axisMode)
            {
                case AxisMode.X: retBit = 1; break;
                case AxisMode.Y: retBit = 2; break;
                case AxisMode.W: retBit = 4; break;
                case AxisMode.Z: retBit = 8; break;
                case AxisMode.A: retBit = 16; break;
                case AxisMode.B: retBit = 32; break;
                case AxisMode.C: retBit = 64; break;
                case AxisMode.I: retBit = 128; break;
            }
            return retBit;
        }
        private void _CloseBtn_Click(object sender, EventArgs e)
        {
            threadStop = true;
            if (this.Visible == true) closed = false;
        }
        public void CloseEx()
        {
            threadStop = true;
            if (this.Visible == true) closed = false;
        }
        private void _enterBtn_Click(object sender, EventArgs e)
        {
            int target = 0;
            string source = (_digit == DigitSelect.Four) ? EditText.Text.Remove(EditText.Text.Count() - 1, 1) : EditText.Text;
            if (int.TryParse(source.Replace(".", ""), out target) == true) _WorkSettingExecute(target);
        }
    }
}
