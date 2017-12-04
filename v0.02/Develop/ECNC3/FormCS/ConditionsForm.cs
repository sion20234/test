using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ECNC3.Enumeration;
using ECNC3.Models;
using ECNC3.Models.McIf;
using ECNC3.Models.Common;

namespace ECNC3.Views
{
    /// <summary>加工条件画面</summary>
    /// <remarks>
    /// 以下、2016/09/27に確認。
    /// - 【呼出】ボタンは一覧上の選択行移動により実現されるため削除。
    /// - 加工条件は編集される都度、MCボードへ転送される。ファイルへの永続化を行ってはならない点に注意。
    /// - 入力用のコントロールを選択することで編集開始とする。フォーカスの移動は、確定であると判断し、キャンセルの方法はない。
    /// - 入力状態のまま、MCボードへの反映を行いたい場合は、【NC転送】ボタンを押下する。
    /// - 【登録】【削除】ボタンの押下によりファイルへの永続化を行う。削除は、加工条件番号以外の初期化によるデータの更新。
    /// </remarks>
    public partial class ConditionsForm : ECNC3Form
    {
        /// <summary>ポップアップテンキー</summary>
        private TenKeyDialog _popupTenkey = new TenKeyDialog("", 0, 0, 0);    //初回インスタンスを作っておく

        /// <summary>読み込み中フラグ</summary>
        private bool _isLoading = true;
        private bool _wasEdited = false;
        /// <summary>加工条件パラメータ</summary>
        private FileProcessConditionParameter _param = null;

        /// <summary>終了通知</summary>
        private NotifyReturnDelegate _notifyReturn = null;

        /// <summary>設定結果取得関数定義</summary>
        public delegate void NotifyReturnDelegate();

        //        /// <summary>親フォームへの加工条件番号通知イベント</summary>
        //        public event PnumUpLoadEventHandler UpLoadParentForm;

        /// <summary>>設定結果取得</summary>
        public NotifyReturnDelegate NotifyReturn
        {
            set { _notifyReturn = value; }
        }

        /// <summary>コンストラクタ</summary>
        /// <param name="Pdata">呼び出し元で表示中の加工条件情報</param>
        public ConditionsForm(string Pnum)
        {
            InitializeComponent();

            //	初期設定
            InitInput(_btnNumber, _edtNumber, NumericTextBox.FormatTypes.Integer3);//表記が合わないのでグリッド番号と同じにする。不具合17番：柏原
                                                                                   //InitInput( _btnNumber, _edtNumber, NumericTextBox.FormatTypes.IntegerZeroPlace3 );
                                                                                   //	呼び出し元からの入力値を設定
            _edtNumber.Text = Pnum;
            Disposed += ConditionsForm_Disposed;
        }

        private void ConditionsForm_Disposed(object sender, EventArgs e)
        {
            if (null != _param)
            {
                _param.Dispose();
                _param = null;
            }
            if (null != _notifyReturn)
            {
                _notifyReturn = null;
            }
            if (null != _fromFind)
            {
                _fromFind.Close();
                _fromFind = null;
            }
            if (null != _details)
            {
                _details.Dispose();
                _details = null;
            }
        }

        /// <summary>入力コントロールの初期化</summary>
        /// <param name="selSw">入力遷移させるボタンコントロールの参照</param>
        /// <param name="textBox">入力コントロールの参照</param>
        /// <param name="format">入力コントロールの書式</param>
        private void InitInput(ButtonEx selSw, NumericTextBox textBox, NumericTextBox.FormatTypes format)
        {
            textBox.FormatType = format;
            textBox.ReadOnly = true;
            selSw.EditBox = textBox;
        }

        /// <summary>加工条件 絞込み条件設定フォーム</summary>
        private ConditionsCallSetForm _fromFind = null;
        /// <summary>検索条件(材料名称)</summary>
        private List<string> FindMaterials { get; set; }
        /// <summary>検索条件(電極径)</summary>
        private decimal FindElectrodeDiameter { get; set; }
        /// <summary>現在、表示中の加工条件番号</summary>
        public int CurrentProcessConditionNumber { get { return _details.CurrentProcessConditionNumber; } }

        /// <summary>加工条件フォームのロードイベントハンドラ</summary>
        /// <param name="sender">イベント送信元オブジェクト</param>
        /// <param name="e">イベントパラメータ</param>
        private void ConditionsForm_Load(object sender, EventArgs e)
        {
            _details.RequestPermitEdit = _popupTenkey_Close;
            _details.CallingFunction = EditProcessCondition.CallingFunctions.List;
            _details.RequestCancelEdit = OnEventCancelEdit;

            if (null == _param)
            {
                _param = new FileProcessConditionParameter();
            }
            _param.Read();

            //	データグリッド初期化
            _dgList.Initialize();

            //	データ各項目
            _dgList.InitCol("Number");
            _dgList.InitCol("TurnOn");
            _dgList.InitCol("TurnOff");
            _dgList.InitCol("Cap", typeof(double));
            _dgList.InitCol("ServoControl");
            _dgList.InitCol("Crs");
            _dgList.InitCol("SfrFront");
            _dgList.InitCol("SfrBack");
            _dgList.InitCol("Pol");
            _dgList.InitCol("Comment", typeof(string));
            _dgList.InitCol("Material", typeof(string));
            _dgList.InitCol("Diameter", typeof(double));
            _dgList.InitCol("Ip");
            _dgList.InitCol("PompValue");
            _dgList.InitCol("ServoSelect");
            _dgList.InitCol("PowerSupply");

            //	データ欄作成
            using (McDatProcessConditionTable mc = new McDatProcessConditionTable())
            {
                mc.Read();
                _dgList.RowCount = mc.Items.Count;
                int row = 0;
                //	加工条件番号はメインキーであるため事前に設定する必要がある。
                for (row = 0; row < _dgList.RowCount; ++row)
                {
                    _dgList.Rows[row].Cells["Number"].Value = mc.Items[row].Number;
                }
            }
            using (FileSettings fs = new FileSettings())
            {
                fs.Read();
            }
            RefreshList();
            //SetDetail0();
            _dgList.DefaultCellStyle.SelectionBackColor = FileUIStyleTable.EnabledBackColor;
            _dgList.DefaultCellStyle.SelectionForeColor = FileUIStyleTable.EnabledForeColor;
            _details.SetOffset(new Point(0, -150));
            _details.UpdateData((int)_edtNumber.Value);
            _isLoading = false;

            _btnCall_Click(null, null);
        }

        private void _popupTenkey_Close()
        { 
            if(_popupTenkey != null)
            {
                _popupTenkey.Close();
                _popupTenkey = null;
                _btnNumber.SetSelected(false);
            }
        }

        /// <summary>一覧再表示</summary>
        private void RefreshList()
        {
            using (McDatProcessConditionTable mc = new McDatProcessConditionTable())
            {
                mc.Read();
                int index;
                foreach (DataGridViewRow item in _dgList.Rows)
                {
                    index = (int)item.Cells["Number"].Value;
                    StructureProcessConditionItem data = mc.Items.Find((x) => x.Number == index);
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
        private void SetRowData(DataGridViewRow item, StructureProcessConditionItem data)
        {
            item.Cells["Number"].Value = data.Number;
            item.Cells["TurnOn"].Value = data.Ton;
            item.Cells["TurnOff"].Value = data.Toff;
            item.Cells["Cap"].Value = $"{(double)data.CAPVal / 1000:f3}";
            item.Cells["ServoControl"].Value = data.SCVal;
            item.Cells["Crs"].Value = data.CRSVal;
            item.Cells["SfrFront"].Value = data.SfrFrSel / 1000;
            item.Cells["SfrBack"].Value = data.SfrBkSel / 1000;
            item.Cells["Pol"].Value = data.POLVal;
            item.Cells["Comment"].Value = data.Comment;
            item.Cells["Material"].Value = EnumToTextAsMaterial(data.Material);
            item.Cells["Diameter"].Value = $"{ data.Diameter:f2}";
            item.Cells["Ip"].Value = data.IPVal / 1000;
            item.Cells["PompValue"].Value = data.PompVal;
            item.Cells["ServoSelect"].Value = data.ServoSel;
            item.Cells["PowerSupply"].Value = data.PSSel;
        }

        /// <summary>加工条件番号→グリッドコントロール行番号変換</summary>
        /// <param name="source">加工条件番号</param>
        /// <returns></returns>
        private int ProcessConditionNumberToRowIndex(int source)
        {
            while (null != _dgList)
            {
                if (0 > source)
                {
                    break;
                }
                if (null == _dgList.Rows)
                {
                    break;
                }
                int row = 0;
                for (row = 0; row < _dgList.RowCount; ++row)
                {
                    if (null == _dgList.Rows[row].Cells)
                    {
                        break;
                    }
                    if (1 > _dgList.Rows[row].Cells.Count)
                    {
                        break;
                    }
                    if (source == (int)_dgList.Rows[row].Cells["Number"].Value)
                    {
                        return row;
                    }
                }
                break;
            }
            return -1;
        }

        /// <summary>【閉じる】ボタンクリックイベント</summary>
        /// <param name="sender">イベント送信元オブジェクト</param>
        /// <param name="e">イベントパラメータ</param>
        public void _btnReturn_Click(object sender, EventArgs e)
        {
            CancelInput();
            if (null != _notifyReturn)
            {
                _notifyReturn.Invoke();
            }
            else
            {
                this.Close();   //	本来ここを通ってはいけない。
            }
        }
        /// <summary>【登録】ボタンクリックイベント</summary>
        /// <param name="sender">イベント送信元オブジェクト</param>
        /// <param name="e">イベントパラメータ</param>
        /// <remarks>加工条件をファイルに永続化します</remarks>
        private void _btnRegister_Click(object sender, EventArgs e)
        {
            CancelInput();
            Register();
            _details.Download();                        //20170113 Hachino Add      //NC転送ボタン削除。登録ボタンに組み込み。
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
        }
        private ResultCodes Register()
        {
            _details.PerpetuatingRegister();
            RefreshList();
            return ResultCodes.Success;
        }
        /// <summary>【削除】ボタンクリックイベント</summary>
        /// <param name="sender">イベント送信元オブジェクト</param>
        /// <param name="e">イベントパラメータ</param>
        /// <remarks>加工条件をファイルに永続化します</remarks>
        private void _btnDelete_Click(object sender, EventArgs e)
        {
            CancelInput();
            //	削除します。
            using (MessageDialog msg = new MessageDialog())
            {
                if (false == msg.Question(5001, this))
                {
                    return;
                }
            }
            _details.PerpetuatingDelete();
            RefreshList();
        }

        /// <summary>一覧表示選択行変更イベント</summary>
        /// <param name="sender">イベント送信元オブジェクト</param>
        /// <param name="e">イベントパラメータ</param>
        private void _dgList_SelectionChanged(object sender, EventArgs e)
        {
            CancelInput();
            if (true == _isLoading)
            {
                return;
            }
            if (true == _wasEdited)
            {
                //	永続化されていません。永続化しますか？
                using (MessageDialog msg = new MessageDialog())
                {
                    if (true == msg.Question(5002, this))
                    {
                        if (ResultCodes.Success != Register())
                        {
                            return;
                        }
                    }
                }
            }
            int nextNumber = (int)_dgList.SelectedRows[0].Cells["Number"].Value;
            if (true == _wasEdited)
            {
                using (McDatProcessConditionTable mc = new McDatProcessConditionTable())
                {
                    mc.Initialize();
                }
            }
            _edtNumber.Value = nextNumber;
            _details.UpdateData(nextNumber);

            _dgListPainting();
        }


        private void _dgListPainting()
        {
            using (FileProcessCondition PcondRead = new FileProcessCondition())
            {
                PcondRead.Read();
                foreach (DataGridViewRow dgrow in _dgList.Rows)
                {
                    if (_dgList.SelectedCells.Count != 0
                        && _dgList.SelectedCells[0].RowIndex == dgrow.Index)
                    {
                        if (PcondRead.Items[dgrow.Index].Protect == 1)
                        {
                            foreach (DataGridViewCell dgCell in dgrow.Cells)
                            {
                                dgCell.Style.BackColor = FileUIStyleTable.EnabledBackColor;
                                dgCell.Style.ForeColor = Color.Gray;
                            }
                        }
                        else
                        {
                            foreach (DataGridViewCell dgCell in dgrow.Cells)
                            {
                                dgCell.Style.BackColor = FileUIStyleTable.EnabledBackColor;
                                dgCell.Style.ForeColor = FileUIStyleTable.EnabledForeColor;
                            }
                        }
                    }
                    else
                    {
                        if (PcondRead.Items[dgrow.Index].Protect == 1)
                        {
                            foreach (DataGridViewCell dgCell in dgrow.Cells)
                            {
                                dgCell.Style.BackColor = FileUIStyleTable.DefaultBackColor;
                                dgCell.Style.ForeColor = Color.Gray;
                            }

                        }
                        else
                        {
                            foreach (DataGridViewCell dgCell in dgrow.Cells)
                            {
                                dgCell.Style.BackColor = FileUIStyleTable.DefaultBackColor;
                                dgCell.Style.ForeColor = FileUIStyleTable.DefaultForeColor;
                            }
                        }
                    }
                }
            }
        }

        private void ConditionsForm_Activated(object sender, EventArgs e)
        {
            if (null == _param)
            {
                _param = new FileProcessConditionParameter();
            }
            _param.Read();
        }

        private void ConditionsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CancelInput();
            if (_popupTenkey != null)
            {
                _popupTenkey.Close();   //画面を閉じる
                _popupTenkey = null;    //null初期化
            }
            if (null != _param)
            {
                _param.Dispose();
                _param = null;
            }
            if (null != _fromFind)
            {
                _fromFind.NotifyReturn = null;
                _fromFind.Close();
                _fromFind = null;
            }
        }

        #region 絞込み表示
        /// <summary>【検索】ボタン押下</summary>
        /// <param name="sender">イベント送信元オブジェクト</param>
        /// <param name="e">イベントパラメータ</param>
        /// <remarks>
        /// 材料名称と電極径による絞込み込みを行います。
        /// </remarks>
        private void _btnFind_Click(object sender, EventArgs e)
        {
            CancelInput();
            CancelEdit();
            if (null == _fromFind)
            {
                _fromFind = new ConditionsCallSetForm();
                _fromFind.NotifyReturn = _btnFind_ClickCallBack;
                _fromFind.Reset();
                //	保持された検索条件を書き戻す
                _fromFind.FindElectrodeDiameter = FindElectrodeDiameter;
                if (null != FindMaterials)
                {
                    _fromFind.FindMaterials = new List<string>();
                    int index = 0;
                    for (index = 0; index < FindMaterials.Count; ++index)
                    {
                        _fromFind.FindMaterials.Add(FindMaterials[index]);
                    }
                }
            }
            _fromFind.Show(this);
        }
        /// <summary>検索ダイアログよりの戻りイベント</summary>
        private void _btnFind_ClickCallBack()
        {
            if ((null == _fromFind) || (null == _dgList))
            {
                return;
            }
            while (true == _fromFind.Visible)
            {
                if (ConditionsCallSetForm.Results.Return == _fromFind.FindResult)
                {
                    break;
                }
                if (null == _dgList.Rows)
                {
                    break;
                }
                if (1 > _dgList.RowCount)
                {
                    break;
                }
                int keyDiameter = (int)(_fromFind.FindElectrodeDiameter * 100);
                //	検索条件を含んでいる場合は、検索条件を保持する。
                if (ConditionsCallSetForm.Results.Execute == _fromFind.FindResult)
                {
                    FindElectrodeDiameter = _fromFind.FindElectrodeDiameter;
                    if (null != _fromFind.FindMaterials)
                    {
                        if (null != FindMaterials)
                        {
                            FindMaterials.Clear();
                        }
                        else
                        {
                            FindMaterials = new List<string>();
                        }
                        int index = 0;
                        for (index = 0; index < _fromFind.FindMaterials.Count; ++index)
                        {
                            FindMaterials.Add(_fromFind.FindMaterials[index]);
                        }
                    }
                    else
                    {
                        if (null != FindMaterials)
                        {
                            FindMaterials.Clear();
                            FindMaterials = null;
                        }
                    }
                }
                int row = 0;
                if ((null != _fromFind.FindMaterials) && (0 < keyDiameter))
                {
                    //	材料名称と電極径
                    for (row = 0; row < _dgList.RowCount; ++row)
                    {
                        if ((true == IsMatchMaterialNameInList(row, _fromFind.FindMaterials)) &&
                            (true == IsMatchElectrodeDiameterInList(row, keyDiameter)))
                        {
                            _dgList.Rows[row].Visible = true;
                        }
                        else
                        {
                            _dgList.Rows[row].Visible = false;
                        }
                    }
                }
                else if (null != _fromFind.FindMaterials)
                {
                    //	材料名称のみ
                    for (row = 0; row < _dgList.RowCount; ++row)
                    {
                        _dgList.Rows[row].Visible = IsMatchMaterialNameInList(row, _fromFind.FindMaterials);
                    }
                }
                else if (0 < keyDiameter)
                {
                    //	電極径のみ
                    for (row = 0; row < _dgList.RowCount; ++row)
                    {
                        _dgList.Rows[row].Visible = IsMatchElectrodeDiameterInList(row, keyDiameter);
                    }
                }
                else
                {
                    //	全表示
                    for (row = 0; row < _dgList.RowCount; ++row)
                    {
                        if (false == _dgList.Rows[row].Visible)
                        {
                            _dgList.Rows[row].Visible = true;
                        }
                    }
                }
                break;
            }
            _fromFind.Close();
            _fromFind = null;
        }
        /// <summary>全表示ボタンクリックイベント</summary>
        /// <param name="sender">イベント送信元オブジェクト</param>
        /// <param name="e">イベントパラメータ</param>
        private void _btnFindFree_Click(object sender, EventArgs e)
        {
            CancelInput();
            int row = 0;
            for (row = 0; row < _dgList.RowCount; ++row)
            {
                if (false == _dgList.Rows[row].Visible)
                {
                    _dgList.Rows[row].Visible = true;
                }
            }
            FindElectrodeDiameter = 0;
            if (null != FindMaterials)
            {
                FindMaterials.Clear();
                FindMaterials = null;
            }
        }
        /// <summary>一覧上の材料名称の一致判定</summary>
        /// <param name="row"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private bool IsMatchMaterialNameInList(int row, List<string> key)
        {
            if (null != _dgList)
            {
                if (true == _dgList.VerifyRowsCells(row))
                {
                    if (null != key)
                    {
                        if (0 < key.Count)
                        {
                            int index = key.FindIndex(
                                (x) => 0 == string.Compare(x, _dgList.FindCell(row, "Material")?.Value as string, false));
                            return (0 > index) ? false : true;
                        }
                    }
                }
            }
            return false;
        }
        /// <summary>一覧上の電極径の一致判定</summary>
        /// <param name="row"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private bool IsMatchElectrodeDiameterInList(int row, decimal key)
        {
            if (null != _dgList)
            {
                string text = _dgList.FindCell(row, "Diameter")?.Value as string;
                decimal val = 0;
                if (true == decimal.TryParse(text, out val))
                {
                    if (key == (val * 100))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion


        #region 加工条件編集
        /// <summary>加工条件編集 フォーカス喪失イベント</summary>
        /// <param name="sender">イベント送信元オブジェクト</param>
        /// <param name="e">イベントパラメータ</param>
        private void _btnNumber_Leave(object sender, EventArgs e)
        {
            //CancelInput();
            //CancelEdit();
            //OnEventCancelEdit();
        }
        /// <summary>加工条件編集 編集内容 検証イベント</summary>
        /// <param name="sender">イベント送信元オブジェクト</param>
        /// <param name="e">イベントパラメータ</param>
        /// <remarks>
        /// 詳細表示のうち、MCボードへの転送対象となる項目が編集された場合は、編集された時点で転送されます。
        /// 永続化を行ってはいけないことに注意してください。
        /// </remarks>
        private void _btnNumber_Validating(object sender, CancelEventArgs e)
        {
            CancelInput();
            if (true == sender.Equals(_edtNumber))
            {
                _details.UpdateData((int)_edtNumber.Value);
            }
        }
        /// <summary>加工条件各項目入力遷移切り替えボタン押下</summary>
        /// <param name="sender">イベント送信元オブジェクト</param>
        /// <param name="e">イベントパラメータ</param>
        private void _btnNumber_TargetSelect_Click(object sender, EventArgs e)
        {
            CancelInput();
            //	操作対象以外の選択キャンセル
            _details.CancelEdit(sender);
            //	操作対象
            ButtonEx source = sender as ButtonEx;
            if (true == source.IsActive)
            {
                source.Formating();
                source.IsActive = false;
                source.SetSelected(false);
                _popupTenkey_Close();
            }
            else
            {
                source.IsActive = true;
                source.SetSelected(true);
                string stringTitle = ((Button)sender).Text;             //タイトル
                string stringEditValue = ((ButtonEx)sender).EditBox.Text; //編集値
                popupTenkeyOnFind(stringEditValue, stringTitle, 3, 0, 999, "procCond_PNo");
            }
		}
		#region<ポップアップ・テンキーで値を編集>
		//LogSettingEditTypes _logSettingEditTypes;//ログ設定：編集タイプ
		/// <summary>
		/// ポップアップテンキー：表示/非表示
		/// </summary>
		/// <param name="val">編集値</param>
		/// <param name="popupTitle">タイトルバーの内容</param>
		/// <param name="intKeta">表示桁数</param>
		/// <param name="intMin">最小値</param>
		/// <param name="intMax">最大値</param>
		/// <param name="modeAndPos">「UITenkeyModeAndPosRec.xml」から取得する表示位置とモード</param>
		/// <returns></returns>
		private bool popupTenkeyOnFind( object val, string popupTitle, int intKeta, int intMin, int intMax, string modeAndPos = "" )
		{
			if( _popupTenkey != null ) {
				_popupTenkey.Close();   //画面を閉じる
				_popupTenkey = null;    //null初期化
                return false;
			}
			string changeVal = "";    //編集値
			Decimal lowerLimitDec = 0;//最小値
			Decimal upperLimitDec = 0;//最大値
			 //クリックしたコントロールの値を取得
			changeVal = val.ToString();

			lowerLimitDec = (decimal)intMin;
			upperLimitDec = (decimal)intMax;
			//フォーマットタイプ
			NumericTextBox.FormatTypes formatType = NumericTextBox.FormatTypes.Free;
			switch( intKeta ) {
				case 3: formatType = NumericTextBox.FormatTypes.Integer3; break;
			}
			//ポップアップTenKey表示
			_popupTenkey = new TenKeyDialog( changeVal, formatType, lowerLimitDec, upperLimitDec, true, false, false, modeAndPos );
            _popupTenkey.NotifyReturnOk = popupTenkey_OnNotifyReturnOk; //イベント通知：OKボタン
            _popupTenkey.NotifyClose = popupTenkey_OnNotifyClose;       //イベント通知：閉じるボタン
            _popupTenkey.Text = popupTitle;                             //テンキータイトル表示
			_popupTenkey.Show( this );                            //画面を開く
			return true;
		}
		/// <summary>
		/// ポップアップテンキー：「OK」ボタンを押した時、入力値をこのクラスに反映
		/// </summary>
		private void popupTenkey_OnNotifyReturnOk()
		{
			string retVal = _popupTenkey._tenkeyValReturn;//ポップアップテンキーで編集された値
			//PNo.TextBoxに設定
			_edtNumber.Text = retVal;
            SendKeys.Send("{TAB}");//編集状態を戻す。

            //先頭の行までスクロールする
            int intRow= int.Parse(retVal);
            _dgList.FirstDisplayedScrollingRowIndex = intRow;
            _dgList.Rows[intRow].Selected = true;
            _btnNumber.Formating();
            _btnNumber.IsActive = false;
            _btnNumber.SetSelected(false);
            _popupTenkey_Close();
        }
        /// <summary>
        /// ポップアップテンキー：「閉じる」ボタンを押した時
        /// </summary>
        private void popupTenkey_OnNotifyClose()
        {
             SendKeys.Send("{TAB}");//編集状態を戻す。
            _btnNumber.Formating();
            _btnNumber.IsActive = false;
            _btnNumber.SetSelected(false);
            _popupTenkey_Close();
        }
        #endregion
        /// <summary>編集状態のキャンセル</summary>
        /// <remarks>
        /// 加工条件編集コントロール群の背景色等の編集状態をクリアします。
        /// </remarks>
        private void CancelEdit()
        {
            //			OnEventCancelEdit();
            _details.CancelEdit();
            _popupTenkey_Close();
        }
        /// <summary>入力状態のキャンセル</summary>
        private void CancelInput()
        {
            _details.ResetInput();
        }

        #endregion

        /// <summary>材料名称変換</summary>
        /// <param name="source">変換元</param>
        /// <returns>変換結果</returns>
        private string EnumToTextAsMaterial(int source)
        {
            if (null == _param)
            {
                _param = new FileProcessConditionParameter();
                _param.Read();
            }
            int index = _param.Materials.FindIndex((x) => x.Number == source);
            if (0 > index)
            {
                return string.Empty;
            }
            return _param.Materials[index].Name;
        }

        /// <summary>呼び出しボタンクリック</summary>
        /// <param name="sender">イベント送信元オブジェクト</param>
        /// <param name="e">イベントパラメータ</param>
        private void _btnCall_Click(object sender, EventArgs e)
        {
            CancelInput();
            if (true == _isLoading)
            {
                return;
            }
            
            int nextNumber = (int)_edtNumber.Value;
            if (true == _wasEdited)
            {
                using (McDatProcessConditionTable mc = new McDatProcessConditionTable())
                {
                    mc.Initialize();
                }
            }
            using (McReqProcessConditionNumberSelect mc = new McReqProcessConditionNumberSelect())
            {
                mc.SelectingNumber = nextNumber;
                mc.Execute();
            }
            CancelEdit();
            _details.UpdateData(nextNumber);
            //	行番号を取得
            int row = 0;
            for (row = 0; row < _dgList.RowCount; ++row)
            {
                if (nextNumber == (int)_dgList.Rows[row].Cells["Number"].Value)
                {
                    _dgList.Rows[row].Selected = true;
                    break;
                }
            }
            // 選択された行を先頭行にする
            if (null != _dgList)
            {
                if( (this.IsDisposed == true || this.Disposing == true)   ) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                {
                    _dgList.ShowSelectedRow();
                }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
            }
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
            _dgListPainting();
        }

        /// <summary>下位モジュールからの編集キャンセル要求受信</summary>
        public void OnEventCancelEdit()
        {
            foreach (Control item in this.Controls)
            {
                if (typeof(ButtonEx) == item.GetType())
                {
                    ButtonEx target = item as ButtonEx;
                    target.IsActive = false;
                }
            }
        }

        /// <summary>
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
        }

        private void ConditionsForm_VisibleChanged(object sender, EventArgs e)
        {
            if(this != null)
            {
                _btnCall_Click(null, null);
            }
        }
		#region<ボタン▲10、100▼10、100によるDGVEXの操作>
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
        /// <summary>
        /// 数字ボタン：▲▼：1、10、100：加算/減算
        /// </summary>
        /// <param name="value"></param>
        private void valueUpDn(int value)
        {
            if (_dgList.CurrentCell == null) return;
            //カレントセル + とび先インデックス
            int rowIndex = _dgList.CurrentCell.RowIndex + value;
            //とび先インデックスが0以下
            if (rowIndex < 0) rowIndex = 0;
            //とび先のインデックスがオーバーは最大値
            if (rowIndex >= _dgList.RowCount) rowIndex = _dgList.RowCount - 1;//2017-10-11：追加：柏原
            //とび先の番号セルがnull
            if (_dgList[0, rowIndex].Value == null) return;
            //指定行へセル移動
            cellRowMove(rowIndex);
        }
        /// <summary>
        /// 指定行セルに移動
        /// </summary>
        /// <param name="rowIndex"></param>
        private void cellRowMove(int rowIndex)
        {
            if (_dgList.CurrentCell == null) return;
            int colIndex = _dgList.CurrentCell.ColumnIndex;//列
            //とび先インデックスが0以下
            if (rowIndex < 0) rowIndex = 0;
            //とび先のインデックスがオーバーは最大値
            if (rowIndex >= _dgList.RowCount) rowIndex = _dgList.RowCount - 1;//2017-10-11：追加：柏原
            //指定行へセル移動
            _dgList.CurrentCell = _dgList[colIndex, rowIndex];
        }
        #endregion
        #region <トラックバーによるDGVEXの操作>
        /// <summary>
        /// トラックバー移動時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBar1_Scroll( object sender, EventArgs e )
		{
			//_dgList.SetRedraw( true );//DGVEX：描画：停止
			//int TrackValNow = ( (TrackBar)sender ).Value;
			//トラックバー位置からスクロール位置に変換
			//int dspVal = changeTrackPosToScPos( TrackValNow );
			//cellRowMove( dspVal );
			//トラックバーの移動
			//setTrackbar( dspVal );
			//_dgList.SetRedraw( false );//DGVEX：描画：開始
			return;
		}
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
		/// トラックバー位置をスクロール位置に変換
		/// </summary>
		/// <param name="trackVal"></param>
		//private int changeTrackPosToScPos( int trackVal )
		//{
		//	return trackBar1.Maximum - trackVal;//SCと逆
		//}
		#endregion
		#region<右画面→ボタン、←左画面ボタンでのDGVEX操作>
		/// <summary>
		/// 右画面ボタン：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonEx_RightShow_Click( object sender, EventArgs e )
		{
			if( _dgList.CurrentCell == null ) return;
			int rowIndex = _dgList.CurrentCell.RowIndex;	//行
			int colIndex = _dgList.CurrentCell.ColumnIndex;	//列
			if( rowIndex < 0 ) rowIndex = 0;
			//指定行へセル移動
			int colPos = _dgList.ColumnCount-1;//右端列
			_dgList.CurrentCell = _dgList[colPos, rowIndex];
		}
		/// <summary>
		/// 左画面ボタン：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonEx_LeftShow_Click( object sender, EventArgs e )
		{
			if( _dgList.CurrentCell == null ) return;
			int rowIndex = _dgList.CurrentCell.RowIndex;    //行
			int colIndex = _dgList.CurrentCell.ColumnIndex; //列
			if( rowIndex < 0 ) rowIndex = 0;
			//指定行へセル移動
			int colPos = 1;//左端列
			_dgList.CurrentCell = _dgList[colPos, rowIndex];
		}
        #endregion

        private void _protectCommandBtn_MouseUp(object sender, MouseEventArgs e)
        {
                _protectCommandBtn.SetBack(true);
            ProtectCommandsDialog.ShowSubForm(this, "加工条件保護設定",
                                                ProtectParamCategory.ProcessConditions,
                                                sender as ButtonEx,
                                                (_dgList.SelectedCells.Count != 0) ? _dgList.SelectedCells[0].RowIndex : 0
                                                );
                //書き込み保護をリスト表示に適用する。
                SetEditableRowColor();
                _protectCommandBtn.SetBack(false);
        }

        private void _PcondImportBtn_Click(object sender, EventArgs e)
        {
            (sender as ButtonEx).SetSelected(true);
            using (MessageDialog msgDia = new MessageDialog())
            {
                if (msgDia.Question(551, this))
                {
                    if (_SetCommands(_Commands.PcondImport) == ResultCodes.Success)
                    {
                        msgDia.Information(552, this);
                    }
                    else
                    {
                        msgDia.Error(5038, this);
                    }
                }
            }
            (sender as ButtonEx).SetSelected(false);
        }

        private void _PcondExportBtn_Click(object sender, EventArgs e)
        {
            (sender as ButtonEx).SetSelected(true);
            using (MessageDialog msgDia = new MessageDialog())
            {
                if (msgDia.Question(554, this))
                {
                    if (_SetCommands(_Commands.PcondExport) == ResultCodes.Success)
                    {
                        msgDia.Information(555, this);
                    }
                    else
                    {
                        msgDia.Error(5039, this);
                    }
                }
            }
            (sender as ButtonEx).SetSelected(false);
        }
        private enum _Commands
        {
            PcondImport,
            PcondExport
        }
        private ResultCodes _SetCommands(_Commands command)
        {
            ResultCodes retResult = ResultCodes.Success;
            switch (command)
            {
                case _Commands.PcondImport: retResult = UIStaticMethods.ImportMasterFile("Pdata.xml"); break;
                case _Commands.PcondExport: retResult = UIStaticMethods.ExportMasterFile("Pdata.xml"); break;
            }
            return retResult;
        }
    }
}
