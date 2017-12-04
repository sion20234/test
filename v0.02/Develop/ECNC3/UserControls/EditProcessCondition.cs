using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ECNC3.Enumeration;
using ECNC3.Models;
using ECNC3.Models.McIf;
using System.Runtime.InteropServices;	//DLL Import

namespace ECNC3.Views
{
	/// <summary>加工条件入力ユーザーコントロール</summary>
	public partial class EditProcessCondition : UserControl
	{
        /// <summary>SF02の有無</summary>
        private bool SFIPEn = false;
		/// <summary>編集の有無</summary>
		private bool _wasEdited = false;
		/// <summary>加工条件パラメータ</summary>
		private FileProcessConditionParameter _param = null;
		/// <summary>材質名選択フォーム</summary>
		private MaterialNameForm _formMaterial = null;
		/// <summary>ポップアップキーボード</summary>
		private StandardKeyBord _popupKeyboard = null;
        /// <summary>ポップアップテンキー　2017-1-16:柏原</summary>
        public TenKeyDialog _popupTenkey = null; 
        /// <summary>編集状態解除要求</summary>
        private RequestCancelEditDelegate _requestCancelEdit = null;
		/// <summary>編集状態解除要求関数定義</summary>
		public delegate void RequestCancelEditDelegate();
		/// <summary>>編集状態解除要求</summary>
		public RequestCancelEditDelegate RequestCancelEdit
		{
			set { _requestCancelEdit = value; }
		}
        /// <summary>編集状態設定要求</summary>
        private RequestPermitEditDelegate _requestPermitEdit = null;
        /// <summary>編集状態設定要求関数定義</summary>
        public delegate void RequestPermitEditDelegate();
        /// <summary>>編集状態設定要求</summary>
        public RequestPermitEditDelegate RequestPermitEdit
        {
            set { _requestPermitEdit = value; }
        }

        /// <summary>コンストラクタ</summary>
        public EditProcessCondition()
		{
			InitializeComponent();
			Disposed += Form_Disposed;    
		}
		/// <summary>現在、表示中の加工条件</summary>
		private StructureProcessConditionItem CurrentItem { get; set; }

        /// <summary>
        /// IP、CAP値のテーブル
        /// </summary>
        private FileProcessConditionParameter ElectDataItems { get; set; }

        /// <summary>
        /// ポップアップ上下キーのLocation調整
        /// </summary>
        public Point _offset = new Point();

        public int Protect { get { return CurrentItem.Protect; } set { CurrentItem.Protect = value; } }
		/// <summary>現在、表示中の加工条件番号</summary>
		public int CurrentProcessConditionNumber
		{
			get { return ( null != CurrentItem ) ? CurrentItem.Number : 0; }
			set
			{
				if( null == CurrentItem ) {
					CurrentItem = new StructureProcessConditionItem();
				}
				CurrentItem.Number = value;
			}
		}
		/// <summary>呼び出し元機能</summary>
		public enum CallingFunctions
		{
			/// <summary>手動画面</summary>
			Manual,
			/// <summary>加工条件一覧画面</summary>
			List,
		}
		/// <summary>呼び出し元機能の設定</summary>
		public CallingFunctions CallingFunction { get; set; } = CallingFunctions.Manual;

		/// <summary>ロード</summary>
		/// <param name="sender">イベント送信元オブジェクト</param>
		/// <param name="e">イベントパラメータ</param>
		private void Form_Load( object sender, EventArgs e )
		{
            if(_popupTenkey == null)_popupTenkey = new TenKeyDialog("", 0, 0, 0);//初回インスタンスを作っておく
            //	初期設定
            InitInput( _btnTurnOn, _edtTurnOn, NumericTextBox.FormatTypes.IntegerZeroPlace3 );
			InitInput( _btnTurnOff, _edtTurnOff, NumericTextBox.FormatTypes.IntegerZeroPlace3 );

            //2017.1.20 Hachino　IP、CAP値のテーブルの初期化
            ElectDataItems = new FileProcessConditionParameter();
            ElectDataItems.Read();
            //SF02モード確認
            Sfip_Init();

            _edtIp.LowerLimit = ElectDataItems.Limit.IPVal.LowerLimit;
            _edtIp.UpperLimit = ElectDataItems.Limit.IPVal.UpperLimit;
			InitInput( _btnCap, _edtCap, NumericTextBox.FormatTypes.DecimalUpper1Lower3 );
			_edtCap.LowerLimit = ElectDataItems.Limit.CAPVal.LowerLimit;
            _edtCap.UpperLimit = ElectDataItems.Limit.CAPVal.UpperLimit;
            InitInput( _btnServoControl, _edtServoControl, NumericTextBox.FormatTypes.IntegerZeroPlace2 );
			_edtServoControl.LowerLimit = ElectDataItems.Limit.SCVal.LowerLimit;
			_edtServoControl.UpperLimit = ElectDataItems.Limit.SCVal.UpperLimit;
            InitInput( _btnSfrDown, _edtSfrDown, NumericTextBox.FormatTypes.IntegerZeroPlace3 );
			_edtSfrDown.LowerLimit = ElectDataItems.Limit.SfrFrSel.LowerLimit;
			_edtSfrDown.UpperLimit = ElectDataItems.Limit.SfrFrSel.UpperLimit;
            InitInput(_btnSfrUp, _edtSfrUp, NumericTextBox.FormatTypes.IntegerZeroPlace3);
            _edtSfrUp.LowerLimit = ElectDataItems.Limit.SfrBkSel.LowerLimit;
            _edtSfrUp.UpperLimit = ElectDataItems.Limit.SfrBkSel.UpperLimit;
            InitInput( _btnCrs, _edtCrs, NumericTextBox.FormatTypes.IntegerZeroPlace2 );
			_edtCrs.LowerLimit = ElectDataItems.Limit.CRSVal.LowerLimit;
            _edtCrs.UpperLimit = ElectDataItems.Limit.CRSVal.UpperLimit;
            InitInput( _btnPol, _edtPol , NumericTextBox.FormatTypes.Integer1 );
			_edtPol.LowerLimit = ElectDataItems.Limit.POLVal.LowerLimit;
            _edtPol.UpperLimit = ElectDataItems.Limit.POLVal.UpperLimit;
            InitInput( _btnDiameter, _edtDiameter, NumericTextBox.FormatTypes.DecimalUpper1Lower2 );
			InitInput( _btnServoSelect, _edtServoSelect, NumericTextBox.FormatTypes.Integer1 );
			_edtServoSelect.LowerLimit = ElectDataItems.Limit.ServoSel.LowerLimit;
            _edtServoSelect.UpperLimit = ElectDataItems.Limit.ServoSel.UpperLimit;
            InitInput(_btnPS, _edtPS, NumericTextBox.FormatTypes.Integer1);
            _edtPS.LowerLimit = ElectDataItems.Limit.PSSel.LowerLimit;
            _edtPS.UpperLimit = ElectDataItems.Limit.PSSel.UpperLimit;
            InitInput(_btnInverter, _edtInverter, NumericTextBox.FormatTypes.Integer2);//0埋め2桁から2桁に変更：柏原
            //InitInput(_btnInverter, _edtInverter, NumericTextBox.FormatTypes.IntegerZeroPlace2);
            _edtInverter.LowerLimit = ElectDataItems.Limit.PompVal.LowerLimit;
            _edtInverter.UpperLimit = ElectDataItems.Limit.PompVal.UpperLimit;
            //オバーライドのMANUALFormからの移動：2017-01-19：柏原
            InitInput(_btnOverRide, _edtOverRide, NumericTextBox.FormatTypes.Integer3);
			 _edtOverRide.LowerLimit = 0;
			 _edtOverRide.UpperLimit = 200;


			//加工条件：「CncSys.xml」から項目表示/非表示を取得、コントロールを表示/非表示：2017-05-26：柏原
			//SetShowHideItem();
		}
        public bool Sfip_Init(Keys vector = Keys.None, double inputValue = -1)
        {
            //SF02分岐
            using (McDatInitialPrm datIni = new McDatInitialPrm())
            {
                datIni.Read();
                SFIPEn = datIni.EnableSF02;
                if (inputValue == -1) inputValue = (double)_edtIp.Value;
                switch (SFIPEn)
                {
                    case false:
                        if (vector == Keys.None)
                        {
                            decimal tempvalue = _edtIp.Value;
                            InitInput(_btnIp, _edtIp, NumericTextBox.FormatTypes.IntegerZeroPlace3);
                            _edtIp.Value = tempvalue;
                        }
                        break;

                    case true:
                        if(inputValue >= 3)
                        {
                            if (vector == Keys.Down && inputValue == 3)
                                InitInput(_btnIp, _edtIp, NumericTextBox.FormatTypes.DecimalUpper1Lower3);
                        }
                        else
                        {
                            if (vector == Keys.Up && inputValue == 2.5)
                                InitInput(_btnIp, _edtIp, NumericTextBox.FormatTypes.IntegerZeroPlace3);
                        }
                        if(vector == Keys.None)
                        {
                            decimal tempvalue = _edtIp.Value;
                            if (inputValue >= 3)
                            {
                                InitInput(_btnIp, _edtIp, NumericTextBox.FormatTypes.IntegerZeroPlace3);
                            }
                            else
                            {
                                InitInput(_btnIp, _edtIp, NumericTextBox.FormatTypes.DecimalUpper1Lower3);
                            }
                            _edtIp.Value = tempvalue;
                        }
                        break;
                }
            }
            return SFIPEn;
        }
		/// <summary>
		/// 加工条件：項目表示/非表示を「CncSys.xml」から取得、コントロールを表示/非表示
		/// </summary>
		private bool SetShowHideItem()
		{
			try {
				using( FileSettings fs = new FileSettings() ) {
					//ファイル読み込み
					fs.Read();
					int intDiameter = fs.AttrValue( "Root/CondShowHide/Diameter", "show" );
					if( intDiameter == 0 ) {//電極径
						_btnDiameter.Visible = false;
						_edtDiameter.Visible = false;
					}
					int intMaterial = fs.AttrValue( "Root/CondShowHide/Material", "show" );
					if( intMaterial == 0 ) {//材質
						_btnMaterial.Visible = false;
						_btnMaterialSelect.Visible = false;
					}
				}
				return true;
			} catch(Exception exc ) {
				//例外処理
				MessageBox.Show( exc.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error );
			}
			return false;
		}
		/// <summary>コントロール破棄イベント</summary>
		/// <param name="sender">イベント送信元オブジェクト</param>
		/// <param name="e">イベントパラメータ</param>
		private void Form_Disposed( object sender, EventArgs e )
		{
			if( null != CurrentItem ) {
				CurrentItem.Dispose();
				CurrentItem = null;
			}
            if(null != ElectDataItems) {
                ElectDataItems.Dispose();
                ElectDataItems = null;
            }
			if( null != _param ) {
				_param.Dispose();
				_param = null;
			}
			if( null != _formMaterial ) {
				_formMaterial.Close();
				_formMaterial = null;
			}
			if( null != _popupKeyboard ) {
				_popupKeyboard.Dispose();
				_popupKeyboard = null;
			}
			///ポップアップテンキー：柏原：2017-01-16
			if( null != _popupTenkey ) {
				_popupTenkey.Close();
				_popupTenkey = null;
			}
        }

		/// <summary>入力コントロールの初期化</summary>
		/// <param name="selSw">入力遷移させるボタンコントロールの参照</param>
		/// <param name="textBox">入力コントロールの参照</param>
		/// <param name="format">入力コントロールの書式</param>
		private void InitInput( ButtonEx selSw, NumericTextBox textBox, NumericTextBox.FormatTypes format )
		{
			textBox.FormatType = format;
			textBox.ReadOnly = true;
			selSw.EditBox = textBox;
		}
		/// <summary>表示</summary>
		/// <param name="item">加工条件</param>
		public void UpdateData( StructureProcessConditionItem item )
		{
			if( null == CurrentItem ) {
				CurrentItem = new StructureProcessConditionItem();
			}
			//	現在値を更新
			CurrentItem.Copy( item );

			_edtTurnOn.Value = item.Ton;
			_edtTurnOff.Value = item.Toff;
            switch (SFIPEn)
            {
                case false: _edtIp.Value = item.IPVal / 1000; break;
                case true:
                    _edtIp.Value = (item.IPVal == 0)? (decimal)item.SFIPVal / 1000 : item.IPVal / 1000;
                    break;
            }			
			_edtCap.Value = (decimal)item.CAPVal / 1000;
			_edtServoControl.Value = item.SCVal;
			_edtSfrDown.Value = item.SfrFrSel / 1000;
			_edtSfrUp.Value = item.SfrBkSel / 1000;
			_edtCrs.Value = item.CRSVal;
			_edtPol.Value = item.POLVal;
			_edtServoSelect.Value = item.ServoSel;
			_edtPS.Value = item.PSSel;
			_edtInverter.Value = item.PompVal;
			using( FileProcessConditionParameter param = new FileProcessConditionParameter() ) {
				param.Read();
				int index = param.Materials.FindIndex( ( x ) => x.Number == item.Material );
				if( 0 > index ) {
					_btnMaterialSelect.Text = string.Empty;
				} else {
					_btnMaterialSelect.Text = param.Materials[index].Name;
				}
			}
			_edtDiameter.Value = (decimal)item.Diameter;
			_edtComment.Text = item.Comment;
			_wasEdited = false;
		}
		/// <summary>表示</summary>
		/// <param name="item">加工条件番号</param>
		public void UpdateData( int processConditionNumber = -1 )
		{
			using( McDatProcessConditionTable mc = new McDatProcessConditionTable() ) {
				if( ResultCodes.Success == mc.Read() ) {
					//	表示に反映
					if( 0 > processConditionNumber ) {
						processConditionNumber = CurrentProcessConditionNumber;
					}
					StructureProcessConditionItem item = mc.Items.Find( ( x ) => x.Number == processConditionNumber );
					if( null == CurrentItem ) {
						CurrentItem = new StructureProcessConditionItem();
					}
					if( null != item ) {
						CurrentItem.Copy( item );
					} else {
						CurrentItem.Clear();
					}
					UpdateData( CurrentItem );
				}
			}
		}
		/// <summary>入力状態の初期化</summary>
		public void ResetInput()
		{
			if( null != _popupKeyboard ) {
				_popupKeyboard.Dispose();
				_popupKeyboard = null;
			}
		}
		/// <summary>編集状態の初期化</summary>
		/// <remarks>
		/// 加工条件編集コントロール群の背景色等の編集状態をクリアします。
		/// </remarks>
		private void ResetEdit()
		{
			foreach( Control item in this.Controls ) {
				if( typeof( ButtonEx ) == item.GetType() ) {
					ButtonEx source = item as ButtonEx;
					source.IsActive = false;
                    source.SetSelected(false);
				}
			}
		}
        private void ResetEdit(object sender, EventArgs e)
        {
            foreach (Control item in this.Controls)
            {
                if (typeof(ButtonEx) == item.GetType())
                {
                    ButtonEx source = item as ButtonEx;
                    if(source.Name.Replace("_btn", "") != (sender as NumericTextBox).Name.Replace("_edt", ""))
                    {
                        source.IsActive = false;
                        source.SetSelected(false);
                    }
                    else
                    {
                        source.SetSelected(false);
                    }
                }
            }
        }
		#region<ポップアップ：テンキー>
		/// <summary>
		/// 記録する加工条件値
		/// </summary>
		object _controlName = "";
		/// <summary>
		/// ポップアップテンキー：閉じる
		/// </summary>
		private void PopupTenkeyClose()
		{
			if( _popupTenkey != null ) {
				_popupTenkey.Close();   //画面を閉じる
				_popupTenkey = null;    //null初期化
			}
		}
		private string _nowCondItemName;//現在の加工条件名
		/// <summary>
		/// ポップアップテンキー：表示/非表示
		/// </summary>
		/// <param name="val"></param>
		/// <returns>false=上下ポップアップを表示</returns>
		private bool popupTenkeyOn(object val)
		{
			_controlName = val;		//コントロール名を記録

			string changeVal = "";	//編集値
            Decimal lowerLimitDec=0;//最小値
            Decimal upperLimitDec=0;//最大値
 			//フォーマットタイプ
          　NumericTextBox.FormatTypes formatType = NumericTextBox.FormatTypes.Integer1;
			// ポップアップテンキー：閉じる
			PopupTenkeyClose();
            //クリックしたコントロールの項目名を取得
            string itemName = ((ButtonEx)val).Name;		//項目文字列
			_nowCondItemName = itemName;				//他の場所でも使用するので、クラス共通値にする
			//クリックしたコントロールの下段の値を取得
			changeVal = ((ButtonEx)val).EditBox.Text; //編集値
			string strTenkeyMode = "";
			switch ( itemName ) {
				case "_btnTurnOn"		:
                    lowerLimitDec = _edtTurnOn.LowerLimit;
                    upperLimitDec = _edtTurnOn.UpperLimit;
                    formatType = NumericTextBox.FormatTypes.IntegerZeroPlace3;
					strTenkeyMode = "procCond_T-ON";
					break;
				case "_btnTurnOff"	:
                    lowerLimitDec = _edtTurnOff.LowerLimit;
                    upperLimitDec = _edtTurnOff.UpperLimit;
                    formatType = NumericTextBox.FormatTypes.IntegerZeroPlace3;
					strTenkeyMode = "procCond_T-OFF";
					break;
				case "_btnIp":
					lowerLimitDec = _edtIp.LowerLimit;
					upperLimitDec = _edtIp.UpperLimit;
					formatType = (SFIPEn)? NumericTextBox.FormatTypes.DecimalUpper1Lower3 : NumericTextBox.FormatTypes.IntegerZeroPlace3;//1.3;
					strTenkeyMode = "procCond_IP";
					break;
				case "_btnCap":
					lowerLimitDec = _edtCap.LowerLimit;
					upperLimitDec = _edtCap.UpperLimit;
					formatType = NumericTextBox.FormatTypes.DecimalUpper1Lower3;//1.3
					strTenkeyMode = "procCond_CAP";
					break;
				case "_btnServoControl"		:
                    lowerLimitDec = _edtServoControl.LowerLimit;
                    upperLimitDec = _edtServoControl.UpperLimit;
                    formatType = NumericTextBox.FormatTypes.IntegerZeroPlace2;
					strTenkeyMode = "procCond_SC";
					break;
				case "_btnSfrDown"	:
                    lowerLimitDec = _edtSfrDown.LowerLimit;
                    upperLimitDec = _edtSfrDown.UpperLimit;
                    formatType = NumericTextBox.FormatTypes.IntegerZeroPlace3;
					strTenkeyMode = "procCond_SFRDOWN";
					break;
				case "_btnSfrUp":
                    lowerLimitDec = _edtSfrUp.LowerLimit;
                    upperLimitDec = _edtSfrUp.UpperLimit;
                    formatType = NumericTextBox.FormatTypes.IntegerZeroPlace3;
					strTenkeyMode = "procCond_SFRUP";
					break;
				case "_btnCrs":
                    lowerLimitDec = _edtCrs.LowerLimit;
                    upperLimitDec = _edtCrs.UpperLimit;
                    formatType = NumericTextBox.FormatTypes.IntegerZeroPlace2;
					strTenkeyMode = "procCond_ROT";
					break;
				case "_btnPol":
                    lowerLimitDec = _edtPol.LowerLimit;
                    upperLimitDec = _edtPol.UpperLimit;
                    formatType = NumericTextBox.FormatTypes.Integer1;
					strTenkeyMode = "procCond_POL";
					break;
				case "_btnInverter":
                    lowerLimitDec = _edtInverter.LowerLimit;
                    upperLimitDec = _edtInverter.UpperLimit;
                    formatType = NumericTextBox.FormatTypes.Integer2;//0埋め2桁から2桁に変更：柏原
                    strTenkeyMode = "procCond_HYD";
					break;
				case "_btnServoSelect":
                    lowerLimitDec = _edtServoSelect.LowerLimit;
                    upperLimitDec = _edtServoSelect.UpperLimit;
                    formatType = NumericTextBox.FormatTypes.Integer1;
					strTenkeyMode = "procCond_AC";
					break;
				case "_btnPS":
                    lowerLimitDec = _edtPS.LowerLimit;
                    upperLimitDec = _edtPS.UpperLimit;
                    formatType = NumericTextBox.FormatTypes.Integer1;
					strTenkeyMode = "procCond_DC";
					break;
				case "_btnMaterial"	:
                    lowerLimitDec = _edtDiameter.LowerLimit;
                    upperLimitDec = _edtDiameter.UpperLimit;
                    formatType = NumericTextBox.FormatTypes.DecimalUpper1Lower2;
					strTenkeyMode = "procCond_ELEDIAM";
					break;
				//下記の処理は「MANUALForm」から移動しました。2017-01-19：柏原
				case "_btnOverRide"	:
                    lowerLimitDec = _edtOverRide.LowerLimit;
                    upperLimitDec = _edtOverRide.UpperLimit;
                    formatType = NumericTextBox.FormatTypes.Integer3;
					strTenkeyMode = "procCond_OVERRIDE";
					break;
            }
			bool boolRealVal = false;//false=整数編集、true=実数編集
			switch( _nowCondItemName ) {
				case "_btnCap":	boolRealVal = true;	break;//実数編集
			}

            //ポップアップテンキー：柏原
            _popupTenkey = new TenKeyDialog(
				changeVal,      //ポップアップテンキーのタイトル表示			
				formatType,		//NumericTextBoxの編集フォーマットタイプ
				lowerLimitDec,	//最小値
				upperLimitDec,	//最大値
				true,           //true=初回文字選択
				boolRealVal,	//true=実数編集
				false,			//true=下1桁編集不可
				strTenkeyMode,	//表示モード：UITenkeyModeAndPosRec.xmlから表示位置もR/W
                this//どうする？
            );
            //_popupTenkey.Parent = this;

            _popupTenkey.NotifyArrowUp = popupTenkey_OnNotifyArrowUp;   //Tenkeyからのイベント通知：▲上
			_popupTenkey.NotifyArrowDn = popupTenkey_OnNotifyArrowDn;   //Tenkeyからのイベント通知：▼下
			_popupTenkey.NotifyReturnOk= popupTenkey_OnNotifyReturnOk;	//Tenkeyからのイベント通知：OK
			_popupTenkey.FormClosing += popupTenkey_FormClosing;        //Tenkeyからのイベント通知：閉じる
			_popupTenkey.Text = itemName.Replace( "\r\n", " ");			//テンキータイトル表示：改行が有れば空白に変換
			_popupTenkey.Show( this );                                  //画面を開く
			return true;
		}
		/// <summary>
		/// ポップアップテンキー：▲ボタンイベント
		/// </summary>
		private void popupTenkey_OnNotifyArrowUp()
		{   //NCにデータを送る
			OverRideUp_Click( null, null );
			//ポップアップテンキーの値を変更
			switch( _nowCondItemName ) {
                case "_btnTurnOn": _popupTenkey.stringNcImmediateUpDn = _edtTurnOn.Text; break;
                case "_btnTurnOff": _popupTenkey.stringNcImmediateUpDn = _edtTurnOff.Text; break;
                case "_btnIp": _popupTenkey.stringNcImmediateUpDn = _edtIp.Text; break;
                case "_btnCap": _popupTenkey.stringNcImmediateUpDn = _edtCap.Text; break;
                case "_btnServoControl": _popupTenkey.stringNcImmediateUpDn = _edtServoControl.Text; break;
                case "_btnSfrDown": _popupTenkey.stringNcImmediateUpDn = _edtSfrDown.Text; break;
                case "_btnSfrUp": _popupTenkey.stringNcImmediateUpDn = _edtSfrUp.Text; break;
                case "_btnCrs": _popupTenkey.stringNcImmediateUpDn = _edtCrs.Text; break;
                case "_btnPol": _popupTenkey.stringNcImmediateUpDn = _edtPol.Text; break;
                case "_btnInverter": _popupTenkey.stringNcImmediateUpDn = _edtInverter.Text; break;
                case "_btnServoSelect": _popupTenkey.stringNcImmediateUpDn = _edtServoSelect.Text; break;
                case "_btnPS": _popupTenkey.stringNcImmediateUpDn = _edtPS.Text; break;
                case "_btnMaterial": _popupTenkey.stringNcImmediateUpDn = _edtDiameter.Text; break;
                //下記の処理は「MANUALForm」から移動しました。2017-01-19：柏原
                case "_btnOverRide": _popupTenkey.stringNcImmediateUpDn = _edtOverRide.Text; break;

            }
		}
		/// <summary>
		/// ポップアップテンキー：▼ボタンイベント
		/// </summary>
		private void popupTenkey_OnNotifyArrowDn()
		{	//NCにデータを送る
			OverRideDown_Click( null, null );
			//ポップアップテンキーの値を変更
			switch( _nowCondItemName ) {
                case "_btnTurnOn": _popupTenkey.stringNcImmediateUpDn = _edtTurnOn.Text; break;
                case "_btnTurnOff": _popupTenkey.stringNcImmediateUpDn = _edtTurnOff.Text; break;
                case "_btnIp": _popupTenkey.stringNcImmediateUpDn = _edtIp.Text; break;
                case "_btnCap": _popupTenkey.stringNcImmediateUpDn = _edtCap.Text; break;
                case "_btnServoControl": _popupTenkey.stringNcImmediateUpDn = _edtServoControl.Text; break;
                case "_btnSfrDown": _popupTenkey.stringNcImmediateUpDn = _edtSfrDown.Text; break;
                case "_btnSfrUp": _popupTenkey.stringNcImmediateUpDn = _edtSfrUp.Text; break;
                case "_btnCrs": _popupTenkey.stringNcImmediateUpDn = _edtCrs.Text; break;
                case "_btnPol": _popupTenkey.stringNcImmediateUpDn = _edtPol.Text; break;
                case "_btnInverter": _popupTenkey.stringNcImmediateUpDn = _edtInverter.Text; break;
                case "_btnServoSelect": _popupTenkey.stringNcImmediateUpDn = _edtServoSelect.Text; break;
                case "_btnPS": _popupTenkey.stringNcImmediateUpDn = _edtPS.Text; break;
                case "_btnMaterial": _popupTenkey.stringNcImmediateUpDn = _edtDiameter.Text; break;
                //下記の処理は「MANUALForm」から移動しました。2017-01-19：柏原
                case "_btnOverRide": _popupTenkey.stringNcImmediateUpDn = _edtOverRide.Text; break;
            }
		}
        /// <summary>
        /// ポップアップテンキー：「OK」ボタンを押した時、入力値をこのクラスに反映
        /// </summary>
        private void popupTenkey_OnNotifyReturnOk()
        {
			string retVal = _popupTenkey._tenkeyValReturn;  //ポップアップテンキーで編集された値

            switch (((Button)_controlName).Name)
            {
                case "_btnTurnOn": _edtTurnOn.Text = retVal; break;
                case "_btnTurnOff": _edtTurnOff.Text = retVal; break;
                case "_btnIp": _edtIp.Value = (decimal.Parse(retVal) >= 3)? (int)decimal.Parse(retVal) : decimal.Parse(retVal); break;
                case "_btnCap": _edtCap.Text = retVal; break;
                case "_btnServoControl": _edtServoControl.Text = retVal; break;
                case "_btnSfrDown": _edtSfrDown.Text = retVal; break;
                case "_btnSfrUp": _edtSfrUp.Text = retVal; break;
                case "_btnCrs": _edtCrs.Text = retVal; break;
                case "_btnPol": _edtPol.Text = retVal; break;
                case "_btnInverter": _edtInverter.Text = retVal; break;
                case "_btnServoSelect": _edtServoSelect.Text = retVal; break;
                case "_btnPS": _edtPS.Text = retVal; break;
                case "_btnMaterial": _edtDiameter.Text = retVal; break;
                //下記の処理は「MANUALForm」から移動しました。2017-01-19：柏原
                case "_btnOverRide": _edtOverRide.Text = retVal; break;
            }
           //※イベントハンドラのEdit_Validating(sender, e)を直接コールしたいが、
           //  if (true == sender.Equals(_edtTurnOn)) の比較でTrueにならない。
           //   「sender」と「_edtTurnOn」は同じ「Text="010"」にもかかわらずなので、
           //   編集後すぐに終了させることでIsEqual()＝Trueにした。
           //編集に開始する
           (_controlName as ButtonEx)?.InvertState();
            //編集を終了する
            this.Edit_Leave(_controlName, null);
            //表示情報の転送
            Download();
        }
		/// <summary>
		/// ポップアップテンキー：フォーム閉じる
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void popupTenkey_FormClosing( object sender, FormClosingEventArgs e )
		{
			_popupTenkey = null;
			foreach( Control Ctrl in this.Controls ) {
				if( typeof( ButtonEx ) == Ctrl.GetType() ) {
					ButtonEx SelBt = Ctrl as ButtonEx;
					if( SelBt.GetBack() == true ) {
						( Ctrl as ButtonEx ).IsActive = false;
						( Ctrl as ButtonEx ).SetSelected( false );
						Download();
					}
				}
			}
		}
		#endregion
		/// <summary>入力値正当性チェック：Int</summary>
		/// <param name="target">現在値</param>
		/// <param name="source">入力値</param>
		/// <param name="minValue">最小値</param>
		/// <param name="maxValue">最大値</param>
		/// <returns>判定結果</returns>
		private bool IsEqual( int source, int target, int targetRate = 1 )
		{
			while( source != ( target * targetRate ) ) {
				return false;
			}
			return true;
		}
		/// <summary>
		/// 入力値正当性チェック：Decimal
		/// </summary>
		/// <param name="source"></param>
		/// <param name="target"></param>
		/// <param name="targetRate"></param>
		/// <returns></returns>
		private bool IsEqualDecimal( decimal source, decimal target, decimal targetRate = 1 )
		{
			while( source != ( target * targetRate ) ) {
				return false;
			}
			return true;
		}
		/// <summary>一時データの更新</summary>
		private bool UpdateCache()
		{
			if( null == CurrentItem ) {
				CurrentItem = new StructureProcessConditionItem();
			}
			using( StructureProcessConditionItem backup = CurrentItem.Clone() as StructureProcessConditionItem ) {
				//	表示されているデータを再取得
				CurrentItem.Clear();
				CurrentItem.Ton = (short)_edtTurnOn.Value;
				CurrentItem.Toff = (short)_edtTurnOff.Value;
                switch (SFIPEn)
                {
                    case false: CurrentItem.IPVal = (int)_edtIp.Value * 1000; break;
                    case true: CurrentItem.SFIPVal = (int)((double)_edtIp.Value * 1000); break;
                }
                CurrentItem.CAPVal = (int)((double)_edtCap.Value * 1000);
                CurrentItem.SCVal = (short)_edtServoControl.Value;
				CurrentItem.SfrFrSel = (short)_edtSfrDown.Value * 1000;
				CurrentItem.SfrBkSel = (short)_edtSfrUp.Value * 1000;
				CurrentItem.CRSVal = (short)_edtCrs.Value;
				CurrentItem.POLVal = (short)_edtPol.Value;
				CurrentItem.Diameter = (double)_edtDiameter.Value;
				CurrentItem.ServoSel = (short)_edtServoSelect.Value;
				CurrentItem.PSSel = (short)_edtPS.Value;
				CurrentItem.PompVal = (short)_edtInverter.Value;
				CurrentItem.Material = TextToEnumAsMaterial( _btnMaterialSelect.Text );
				CurrentItem.Comment = _edtComment.Text;
				if( 0 != CurrentItem.Compare( backup ) ) {
					return true;
				}
			}
			return false;
		}

		#region 材料名称選択
		/// <summary>材質選択ボタン押下イベント</summary>
		/// <param name="sender">イベント送信元オブジェクト</param>
		/// <param name="e">イベントパラメータ</param>
		private void _btnMaterialSelect_Click( object sender, EventArgs e )
		{
			ResetInput();
			if( CallingFunction == CallingFunctions.Manual ) {
				return;
			}
			if( true == IsProtected( CurrentProcessConditionNumber ) ) {
				return;
			}
			if( null == _formMaterial ) {
				_formMaterial = new MaterialNameForm( MaterialNameForm.OperationModes.Selectable );
				_formMaterial.NotifyReturn = _btnMaterialSelect_ClickCallBack;
			}
			_formMaterial.SelectedMaterialName = ( sender as ButtonEx ).Text;
			_formMaterial.Show( this );
		}
		/// <summary>材質選択画面コールバックデリゲート</summary>
		private void _btnMaterialSelect_ClickCallBack()
		{
			if( null == _formMaterial ) {
				return;
			}
			if( null == _param ) {
				_param = new FileProcessConditionParameter();
			}
			_param.Read();
			while( true == _formMaterial.Visible ) {
				if( MaterialNameForm.Results.Return == _formMaterial.Result ) {
					break;
				}
				_btnMaterialSelect.Text = _formMaterial.SelectedMaterialName;
				break;
			}
			_formMaterial.Close();
			_formMaterial = null;
		}
		private int TextToEnumAsMaterial( string source )
		{
			if( null == _param ) {
				_param = new FileProcessConditionParameter();
				_param.Read();
			}
			int index = _param.Materials.FindIndex( ( x ) => 0 == string.Compare( x.Name, source, false ) );
			if( 0 > index ) {
				return 0;
			}
			return _param.Materials[index].Number;
		}
		#endregion

		/// <summary>コメント欄クリックイベント</summary>
		/// <param name="sender">イベント送信元オブジェクト</param>
		/// <param name="e">イベントパラメータ</param>
		private void _edtComment_Click( object sender, EventArgs e )
		{
			if( CallingFunction == CallingFunctions.Manual ) {
				return;
			}
			if( true == IsProtected( CurrentProcessConditionNumber ) ) {
				return;
			}
			if( null == _popupKeyboard ) {
				_popupKeyboard = new StandardKeyBord();
				//_popupKeyboard.Location = new Point( 0, 0 );
				_popupKeyboard.Location = new Point( 0, 0 );
				_popupKeyboard.NotifyReturn = _edtComment_ClickCallBack;
                _popupKeyboard.Visible = true;
                if(Parent.Controls.Contains(_popupKeyboard) == false) Parent.Controls.Add(_popupKeyboard);
			} else {
				ResetInput();
			}
		}
		/// <summary>キーボード入力からの終了要求</summary>
		private void _edtComment_ClickCallBack()
		{
			ResetInput();
		}
		/// <summary>キーボード入力からの表示文字列の更新要求</summary>
		private void _edtComment_ClickTextChanged( string text )
		{
			_edtComment.Text = text;
		}

		/// <summary>加工条件編集 フォーカス喪失イベント</summary>
		/// <param name="sender">イベント送信元オブジェクト</param>
		/// <param name="e">イベントパラメータ</param>
		private void Edit_Leave( object sender, EventArgs e )
		{
			ResetInput();
            if(sender == null
                || e == null)
            {
                ResetEdit();
            }
            else
            {
                ResetEdit(sender, e);
            }
            if(sender.GetType() == typeof(ButtonEx))
            {
                (sender as ButtonEx).SetSelected(false);
            }
		}

		/// <summary>加工条件編集 編集内容 検証イベント</summary>
		/// <param name="sender">イベント送信元オブジェクト</param>
		/// <param name="e">イベントパラメータ</param>
		/// <remarks>
		/// 詳細表示のうち、MCボードへの転送対象となる項目が編集された場合は、編集された時点で転送されます。
		/// 永続化を行ってはいけないことに注意してください。
		/// </remarks>
		private void Edit_Validating( object sender, CancelEventArgs e )
		{
			ResetInput();
			ResultCodes ret = ResultCodes.NotExecute;
			while( null != CurrentItem ) {
				if( true == sender.Equals( _edtTurnOn ) ) {
					//	T-ON
					if( true == IsEqual( CurrentItem.Ton, (short)_edtTurnOn.Value ) ) {
						break;
					}
				} else if( true == sender.Equals( _edtTurnOff ) ) {
					//	T-OFF
					if( true == IsEqual( CurrentItem.Toff, (short)_edtTurnOff.Value ) ) {
						break;
					}
				} else if( true == sender.Equals( _edtIp ) ) {
                    //	IP
                    bool breakPoint = false;
                    switch (SFIPEn)
                    {
                        case false: breakPoint = (true == IsEqual(CurrentItem.IPVal, (int)_edtIp.Value, 1000)); break;
                        case true: breakPoint = (true == IsEqualDecimal(CurrentItem.SFIPVal, _edtIp.Value, 1000)); break;
                    }
                    if ( breakPoint ) {
						break;
					}
				} else if( true == sender.Equals( _edtCap ) ) {
					//	CAP
					//_edtCap.Valueは整数ではないので0.9が来るとintで0になりイコールにならないのでIsEqualDecimal()を追加：柏原
					if( true == IsEqualDecimal( CurrentItem.CAPVal, _edtCap.Value, 1000 ) ) {
						break;
					}
				} else if( true == sender.Equals( _edtServoControl ) ) {
					//	SC
					if( true == IsEqual( CurrentItem.SCVal, (short)_edtServoControl.Value ) ) {
						break;
					}
				} else if( true == sender.Equals( _edtSfrDown ) ) {
					//	SFR Down
					if( true == IsEqual( CurrentItem.SfrFrSel, (short)_edtSfrDown.Value, 1000) ) {
						break;
					}
				} else if( true == sender.Equals( _edtSfrUp ) ) {
					//	SFR Up
					if( true == IsEqual( CurrentItem.SfrBkSel, (short)_edtSfrUp.Value, 1000 ) ) {
						break;
					}
				} else if( true == sender.Equals( _edtCrs ) ) {
					//	CRS
					if( true == IsEqual( CurrentItem.CRSVal, (short)_edtCrs.Value ) ) {
						break;
					}
				} else if( true == sender.Equals( _edtPol ) ) {
					//	POL
					if( true == IsEqual( CurrentItem.POLVal, (short)_edtPol.Value ) ) {
						break;
					}
				} else if( true == sender.Equals( _edtServoSelect ) ) {
					//	Servo select
					if( true == IsEqual( CurrentItem.ServoSel, (short)_edtServoSelect.Value ) ) {
						break;
					}
				} else if( true == sender.Equals( _edtPS ) ) {
					//	PS
					if( true == IsEqual( CurrentItem.PSSel, (short)_edtPS.Value ) ) {
						break;
					}
				} else if( true == sender.Equals( _edtInverter ) ) {
					//	PompValue
					if( true == IsEqual( CurrentItem.PompVal, (short)_edtInverter.Value ) ) {
						break;
					}

				} else {
					break;  //	対象以外の項目対する編集。
				}
				using( StructureProcessConditionItem backup = CurrentItem.Clone() as StructureProcessConditionItem ) {
					UpdateCache();
					ret = WriteProcessCondition( CurrentItem, false );
					if( ResultCodes.Success != ret ) {
						e.Cancel = true;
						//	不正な値です。
						UpdateData( backup );
					}
				}
				break;
			}
		}

        /// <summary>加工条件情報書き込み</summary>
        /// <param name="target">加工条件</param>
        /// <param name="perpetuation">永続化の要否</param>
        private ResultCodes WriteProcessCondition( StructureProcessConditionItem target, bool perpetuation )
		{
			_wasEdited = ( false == perpetuation ) ? true : false;
			using( McDatProcessConditionTable mc = new McDatProcessConditionTable() ) {
				Cursor preCursor = Cursor.Current;
				Cursor.Current = Cursors.WaitCursor;
				ResultCodes ret = mc.Write( target, perpetuation );
				if( ResultCodes.Success != ret ) {
					using( MessageDialog msg = new MessageDialog() ) {
						msg.Note = mc.DataTypeName;
						msg.Error( ret, this );
					}
				}
				Cursor.Current = preCursor;
				return ret;
			}
		}

		/// <summary>加工条件各項目入力遷移切り替えボタン押下</summary>
		/// <param name="sender">イベント送信元オブジェクト</param>
		/// <param name="e">イベントパラメータ</param>
		private void Edit_TargetSelect_Click( object sender, EventArgs e )
		{
            if(_requestPermitEdit != null) _requestPermitEdit?.Invoke();

            foreach (Control item in this.Controls)
            {
                if (typeof(NumericTextBox) == item.GetType())
                {
                    NumericTextBox source = item as NumericTextBox;
                    if (source.Name.Replace("_edt", "") == (sender as ButtonEx).Name.Replace("_btn", ""))
                    {
                        if(source.GetLamp() == true)
                        {
                            CancelEdit();
                            return;
                        }
                    }
                }
            }
            if ((sender as ButtonEx).GetBack() == true)
            {
                CancelEdit();
                return;
            }
			ResetInput();
			//	操作対象以外の選択キャンセル
			_requestCancelEdit?.Invoke();
			CancelEdit( sender );

			//ポップアップTenKey：2017-1-19:柏原
			//下記でブロックされるのでテスト時はここをコメントにします：

			if( CallingFunction == CallingFunctions.Manual ) {
				//	MCへ影響を与えない項目は編集させない。
				if( ( true == sender.Equals( _btnDiameter ) ) ) {
					return;
				}
			}
			//	編集の可否を判定する。
			if( true == IsProtected( CurrentProcessConditionNumber ) ) {
				return;
			}
			//	操作対象
			( sender as ButtonEx )?.InvertState();
            (sender as ButtonEx).SetSelected(true);
            //ポップアップTenKey：2017-1-12:柏原
            if ( popupTenkeyOn( sender ) ) {
				return;
			}
        }


        public void SetOffset(Point offset)
        {
            _offset = offset;
            
        }

        /// <summary>編集状態解除</summary>
        /// <param name="sender">編集状態の解除の対象から除外されるオブジェクト</param>
        public void CancelEdit( object sender = null )
		{
			ResetInput();
			foreach( Control item in this.Controls ) {
				if( typeof( ButtonEx ) == item.GetType() ) {
					ButtonEx target = item as ButtonEx;
					if( null != sender ) {
						if( false == target.Equals( sender ) ) {
							if( true == target.IsActive ) {
								target.Formating();
							}
							target.IsActive = false;
                            target.SetSelected(false);
						}
					} else {
						target.IsActive = false;
                        target.SetSelected(false);
                    }
				}
			}
			//(手動、MDI、編集、自動)画面に移動時、ポップアップテンキーを閉じる：2017-02-03：柏原
			PopupTenkeyClose();
        }
		/// <summary>
		/// このコントロールが非アクティブになった時発生　※他のコントロールがアクティブ状態
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EditProcessCondition_Leave( object sender, EventArgs e )
		{
			//ポップアップテンキーを閉じる：2017-02-03：柏原
			PopupTenkeyClose();
        }
		/// <summary>加工条件編集可否判定</summary>
		/// <param name="processConditionnumber">加工条件番号</param>
		/// <returns>
		///		<item>true=許可されている。</item>
		///		<item>false=保護されている。</item>
		/// </returns>
		/// <remarks>
		/// 保護された加工条件であるかを判定します。
		/// </remarks>
		private bool IsProtected( int processConditionnumber )
		{
            using (FileProcessCondition PCondRead = new FileProcessCondition())
            {
                PCondRead.Read();
                foreach(StructureProcessConditionItem ProtectValue in PCondRead.Items )
                {
                    if(ProtectValue.Number == processConditionnumber)
                    {
                        if(ProtectValue.Protect == 0)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                        
                    }
                }
            }
                while (CallingFunction == CallingFunctions.List) {
                    using (FileSettings fs = new FileSettings()) {
                        fs.Read();
                        int start = fs.AttrValue("Root/Cond/Lock", "start");
                        int end = fs.AttrValue("Root/Cond/Lock", "end");
                        if (end < start) {
                            return false;    //	条件不整合につき、許可する。
                        }
                        if ((start > processConditionnumber) || (end < processConditionnumber)) {
                            break;
                        }
                        return true;
                    }
                }
			return false;
		}
		/// <summary>現在の表示情報の転送</summary>
		/// <returns></returns>
		public ResultCodes Download()
		{
			ResetInput();
			UpdateCache();
			return WriteProcessCondition( CurrentItem, false );
		}
		/// <summary>永続化(登録)</summary>
		/// <returns>実行結果</returns>
		public ResultCodes PerpetuatingRegister()
		{
			ResetInput();
			UpdateCache();
			ResultCodes ret = WriteProcessCondition( CurrentItem, true );
			UpdateData();
			return ret;
		}
		/// <summary>永続化(削除)</summary>
		/// <returns>実行結果</returns>
		public ResultCodes PerpetuatingDelete()
		{
			ResetInput();
			CurrentItem.Clear();
			ResultCodes ret = WriteProcessCondition( CurrentItem, true );
			UpdateData();
			return ret;
		}
		/// <summary>文字入力操作</summary>
		/// <param name="source">入力文字列</param>
		/// <returns>実行の有無
		///		<list type="bullet" >
		///			<item>true=当ウィンドウ内にフォーカスを持つコントロール対象を検出した。</item>
		///			<item>false=当ウィンドウ内にフォーカスを持つコントロール対象を検出しなかった。</item>
		///		</list>
		/// </returns>
		public bool OnEventInputText( string source )
		{
			ResetInput();
			foreach( Control item in this.Controls ) {
				if( true == item.Focused ) {
					if( typeof( NumericTextBox ) == item.GetType() ) {
						NumericTextBox target = item as NumericTextBox;
						if( false == target.ReadOnly ) {
							target.InputText( source );
						}
						return true;
					}
				}
			}
			return false;
		}
		/// <summary>方向キーによる入力操作</summary>
		/// <param name="inputKey">入力キー</param>
		/// <returns>実行の有無
		///		<list type="bullet" >
		///			<item>true=当ウィンドウ内にフォーカスを持つコントロール対象を検出した。</item>
		///			<item>false=当ウィンドウ内にフォーカスを持つコントロール対象を検出しなかった。</item>
		///		</list>
		/// </returns>
		public bool OnEventInputDirectionKey( Keys inputKey )
		{
			ResetInput();
			foreach( Control item in this.Controls ) {
				if( item.BackColor == Models.FileUIStyleTable.EnabledBackColor) {
					if( typeof( NumericTextBox ) == item.GetType() ) {
						NumericTextBox target = item as NumericTextBox;
                        int PcondParamIndex = 0;
                        double beforedata = 0;
                        double inputdata = 0;
                        double upperInputdata = 0;
                        double lowerInputdata = 0;
                        //現在値を読み取る
                        beforedata = (double)target.Value;

                        switch (target.Name)
                        {
                            case "_edtTurnOn":
                                if (inputKey == Keys.Up)
                                {
                                    upperInputdata = (double)ElectDataItems.Limit.Ton.UpperLimit;
                                    //現在のINDEXがINDEX最大値より低い場合、一つ上のINDEXの値に設定する。
                                    if (beforedata < upperInputdata)
                                    {
                                        inputdata = beforedata + 1;
                                        //値の設定
                                        target.UpdateData(inputKey, 1, false);
                                    }
                                    else if (beforedata == upperInputdata)
                                    {
                                        //最大値だった場合は最大値に設定する。
                                        inputdata = upperInputdata;
                                        //値の設定
                                        target.UpdateData(inputKey, 1, false);
                                    }
                                }
                                //下キーの場合
                                else if (inputKey == Keys.Down)
                                {
                                    lowerInputdata = (double)ElectDataItems.Limit.Ton.LowerLimit;
                                    //現在のINDEXがINDEX最小値より高い場合、一つ下のINDEXの値に設定する。
                                    if (beforedata > lowerInputdata)
                                    {
                                        inputdata = beforedata - 1;
                                        //値の設定
                                        target.UpdateData(inputKey, 1, false);
                                    }
                                    else if (beforedata == lowerInputdata)
                                    {
                                        //最小値だった場合は最小値に設定する。
                                        inputdata = lowerInputdata;
                                        //値の設定
                                        target.UpdateData(inputKey, 1, false);
                                    }
                                }
                                break;

                            case "_edtTurnOff":
                                if (inputKey == Keys.Up)
                                {
                                    upperInputdata = (double)ElectDataItems.Limit.Toff.UpperLimit;
                                    //現在のINDEXがINDEX最大値より低い場合、一つ上のINDEXの値に設定する。
                                    if (beforedata < upperInputdata)
                                    {
                                        inputdata = beforedata + 1;
                                        //値の設定
                                        target.UpdateData(inputKey, 1, false);
                                    }
                                    else if (beforedata == upperInputdata)
                                    {
                                        //最大値だった場合は最大値に設定する。
                                        inputdata = upperInputdata;
                                        //値の設定
                                        target.UpdateData(inputKey, 1, false);
                                    }
                                }
                                //下キーの場合
                                else if (inputKey == Keys.Down)
                                {
                                    lowerInputdata = (double)ElectDataItems.Limit.Toff.LowerLimit;
                                    //現在のINDEXがINDEX最小値より高い場合、一つ下のINDEXの値に設定する。
                                    if (beforedata > lowerInputdata)
                                    {
                                        inputdata = beforedata - 1;
                                        //値の設定
                                        target.UpdateData(inputKey, 1, false);
                                    }
                                    else if (beforedata == lowerInputdata)
                                    {
                                        //最小値だった場合は最小値に設定する。
                                        inputdata = lowerInputdata;
                                        //値の設定
                                        target.UpdateData(inputKey, 1, false);
                                    }
                                }
                                break;

                            case "_edtServoControl":
                                if (inputKey == Keys.Up)
                                {
                                    upperInputdata = (double)ElectDataItems.Limit.SCVal.UpperLimit;
                                    //現在のINDEXがINDEX最大値より低い場合、一つ上のINDEXの値に設定する。
                                    if (beforedata < upperInputdata)
                                    {
                                        inputdata = beforedata + 1;
                                        //値の設定
                                        target.UpdateData(inputKey, 1, false);
                                    }
                                    else if (beforedata == upperInputdata)
                                    {
                                        //最大値だった場合は最大値に設定する。
                                        inputdata = upperInputdata;
                                        //値の設定
                                        target.UpdateData(inputKey, 1, false);
                                    }
                                }
                                //下キーの場合
                                else if (inputKey == Keys.Down)
                                {
                                    lowerInputdata = (double)ElectDataItems.Limit.SCVal.LowerLimit;
                                    //現在のINDEXがINDEX最小値より高い場合、一つ下のINDEXの値に設定する。
                                    if (beforedata > lowerInputdata)
                                    {
                                        inputdata = beforedata - 1;
                                        //値の設定
                                        target.UpdateData(inputKey, 1, false);
                                    }
                                    else if (beforedata == lowerInputdata)
                                    {
                                        //最小値だった場合は最小値に設定する。
                                        inputdata = lowerInputdata;
                                        //値の設定
                                        target.UpdateData(inputKey, 1, false);
                                    }
                                }
                                break;
                            case "_edtSfrDown":
                                if (inputKey == Keys.Up)
                                {
                                    upperInputdata = (double)ElectDataItems.Limit.SfrFrSel.UpperLimit;
                                    //現在のINDEXがINDEX最大値より低い場合、一つ上のINDEXの値に設定する。
                                    if (beforedata < upperInputdata)
                                    {
                                        inputdata = beforedata + 1;
                                        //値の設定
                                        target.UpdateData(inputKey, 1, false);
                                    }
                                    else if (beforedata == upperInputdata)
                                    {
                                        //最大値だった場合は最大値に設定する。
                                        inputdata = upperInputdata;
                                        //値の設定
                                        target.UpdateData(inputKey, 1, false);
                                    }
                                }
                                //下キーの場合
                                else if (inputKey == Keys.Down)
                                {
                                    lowerInputdata = (double)ElectDataItems.Limit.SfrFrSel.LowerLimit;
                                    //現在のINDEXがINDEX最小値より高い場合、一つ下のINDEXの値に設定する。
                                    if (beforedata > lowerInputdata)
                                    {
                                        inputdata = beforedata - 1;
                                        //値の設定
                                        target.UpdateData(inputKey, 1, false);
                                    }
                                    else if (beforedata == lowerInputdata)
                                    {
                                        //最小値だった場合は最小値に設定する。
                                        inputdata = lowerInputdata;
                                        //値の設定
                                        target.UpdateData(inputKey, 1, false);
                                    }
                                }
                                break;
                            case "_edtSfrUp":
                                if (inputKey == Keys.Up)
                                {
                                    upperInputdata = (double)ElectDataItems.Limit.SfrBkSel.UpperLimit;
                                    //現在のINDEXがINDEX最大値より低い場合、一つ上のINDEXの値に設定する。
                                    if (beforedata < upperInputdata)
                                    {
                                        inputdata = beforedata + 1;
                                        //値の設定
                                        target.UpdateData(inputKey, 1, false);
                                    }
                                    else if (beforedata == upperInputdata)
                                    {
                                        //最大値だった場合は最大値に設定する。
                                        inputdata = upperInputdata;
                                        //値の設定
                                        target.UpdateData(inputKey, 1, false);
                                    }
                                }
                                //下キーの場合
                                else if (inputKey == Keys.Down)
                                {
                                    lowerInputdata = (double)ElectDataItems.Limit.SfrBkSel.LowerLimit;
                                    //現在のINDEXがINDEX最小値より高い場合、一つ下のINDEXの値に設定する。
                                    if (beforedata > lowerInputdata)
                                    {
                                        inputdata = beforedata - 1;
                                        //値の設定
                                        target.UpdateData(inputKey, 1, false);
                                    }
                                    else if (beforedata == lowerInputdata)
                                    {
                                        //最小値だった場合は最小値に設定する。
                                        inputdata = lowerInputdata;
                                        //値の設定
                                        target.UpdateData(inputKey, 1, false);
                                    }
                                }
                                break;
                            case "_edtCrs":
                                if (inputKey == Keys.Up)
                                {
                                    upperInputdata = (double)ElectDataItems.Limit.CRSVal.UpperLimit;
                                    //現在のINDEXがINDEX最大値より低い場合、一つ上のINDEXの値に設定する。
                                    if (beforedata < upperInputdata)
                                    {
                                        inputdata = beforedata + 1;
                                        //値の設定
                                        target.UpdateData(inputKey, 1, false);
                                    }
                                    else if (beforedata == upperInputdata)
                                    {
                                        //最大値だった場合は最大値に設定する。
                                        inputdata = upperInputdata;
                                        //値の設定
                                        target.UpdateData(inputKey, 1, false);
                                    }
                                }
                                //下キーの場合
                                else if (inputKey == Keys.Down)
                                {
                                    lowerInputdata = (double)ElectDataItems.Limit.CRSVal.LowerLimit;
                                    //現在のINDEXがINDEX最小値より高い場合、一つ下のINDEXの値に設定する。
                                    if (beforedata > lowerInputdata)
                                    {
                                        inputdata = beforedata - 1;
                                        //値の設定
                                        target.UpdateData(inputKey, 1, false);
                                    }
                                    else if (beforedata == lowerInputdata)
                                    {
                                        //最小値だった場合は最小値に設定する。
                                        inputdata = lowerInputdata;
                                        //値の設定
                                        target.UpdateData(inputKey, 1, false);
                                    }
                                }
                                break;
                            case "_edtPol":
                                if (inputKey == Keys.Up)
                                {
                                    upperInputdata = (double)ElectDataItems.Limit.POLVal.UpperLimit;
                                    //現在のINDEXがINDEX最大値より低い場合、一つ上のINDEXの値に設定する。
                                    if (beforedata < upperInputdata)
                                    {
                                        inputdata = beforedata + 1;
                                        //値の設定
                                        target.UpdateData(inputKey, 1, false);
                                    }
                                    else if (beforedata == upperInputdata)
                                    {
                                        //最大値だった場合は最大値に設定する。
                                        inputdata = upperInputdata;
                                        //値の設定
                                        target.UpdateData(inputKey, 1, false);
                                    }
                                }
                                //下キーの場合
                                else if (inputKey == Keys.Down)
                                {
                                    lowerInputdata = (double)ElectDataItems.Limit.POLVal.LowerLimit;
                                    //現在のINDEXがINDEX最小値より高い場合、一つ下のINDEXの値に設定する。
                                    if (beforedata > lowerInputdata)
                                    {
                                        inputdata = beforedata - 1;
                                        //値の設定
                                        target.UpdateData(inputKey, 1, false);
                                    }
                                    else if (beforedata == lowerInputdata)
                                    {
                                        //最小値だった場合は最小値に設定する。
                                        inputdata = lowerInputdata;
                                        //値の設定
                                        target.UpdateData(inputKey, 1, false);
                                    }
                                }
                                break;
                            case "_edtInverter":
                                if (inputKey == Keys.Up)
                                {
                                    upperInputdata = (double)ElectDataItems.Limit.PompVal.UpperLimit;
                                    //現在のINDEXがINDEX最大値より低い場合、一つ上のINDEXの値に設定する。
                                    if (beforedata < upperInputdata)
                                    {
                                        inputdata = beforedata + 1;
                                        //値の設定
                                        target.UpdateData(inputKey, 1, false);
                                    }
                                    else if (beforedata == upperInputdata)
                                    {
                                        //最大値だった場合は最大値に設定する。
                                        inputdata = upperInputdata;
                                        //値の設定
                                        target.UpdateData(inputKey, 1, false);
                                    }
                                }
                                //下キーの場合
                                else if (inputKey == Keys.Down)
                                {
                                    lowerInputdata = (double)ElectDataItems.Limit.PompVal.LowerLimit;
                                    //現在のINDEXがINDEX最小値より高い場合、一つ下のINDEXの値に設定する。
                                    if (beforedata > lowerInputdata)
                                    {
                                        inputdata = beforedata - 1;
                                        //値の設定
                                        target.UpdateData(inputKey, 1, false);
                                    }
                                    else if (beforedata == lowerInputdata)
                                    {
                                        //最小値だった場合は最小値に設定する。
                                        inputdata = lowerInputdata;
                                        //値の設定
                                        target.UpdateData(inputKey, 1, false);
                                    }
                                }
                                break;
                            case "_edtServoSelect":
                                if (inputKey == Keys.Up)
                                {
                                    upperInputdata = (double)ElectDataItems.Limit.ServoSel.UpperLimit;
                                    //現在のINDEXがINDEX最大値より低い場合、一つ上のINDEXの値に設定する。
                                    if (beforedata < upperInputdata)
                                    {
                                        inputdata = beforedata + 1;
                                        //値の設定
                                        target.UpdateData(inputKey, 1, false);
                                    }
                                    else if (beforedata == upperInputdata)
                                    {
                                        //最大値だった場合は最大値に設定する。
                                        inputdata = upperInputdata;
                                        //値の設定
                                        target.UpdateData(inputKey, 1, false);
                                    }
                                }
                                //下キーの場合
                                else if (inputKey == Keys.Down)
                                {
                                    lowerInputdata = (double)ElectDataItems.Limit.ServoSel.LowerLimit;
                                    //現在のINDEXがINDEX最小値より高い場合、一つ下のINDEXの値に設定する。
                                    if (beforedata > lowerInputdata)
                                    {
                                        inputdata = beforedata - 1;
                                        //値の設定
                                        target.UpdateData(inputKey, 1, false);
                                    }
                                    else if (beforedata == lowerInputdata)
                                    {
                                        //最小値だった場合は最小値に設定する。
                                        inputdata = lowerInputdata;
                                        //値の設定
                                        target.UpdateData(inputKey, 1, false);
                                    }
                                }
                                break;

                            case "_edtPS":
                                //上キーの場合
                                if (inputKey == Keys.Up)
                                {
                                    upperInputdata = (double)ElectDataItems.Limit.PSSel.UpperLimit;
                                    //現在のINDEXがINDEX最大値より低い場合、一つ上のINDEXの値に設定する。
                                    if (beforedata < upperInputdata)
                                    {
                                        inputdata = beforedata + 1;
                                        //値の設定
                                        target.UpdateData(inputKey, 1, false);
                                    }
                                    else if (beforedata == upperInputdata)
                                    {
                                        //最大値だった場合は最大値に設定する。
                                        inputdata = upperInputdata;
                                        //値の設定
                                        target.UpdateData(inputKey, 1, false);
                                    }
                                }
                                //下キーの場合
                                else if (inputKey == Keys.Down)
                                {
                                    lowerInputdata = (double)ElectDataItems.Limit.PSSel.LowerLimit;
                                    //現在のINDEXがINDEX最小値より高い場合、一つ下のINDEXの値に設定する。
                                    if (beforedata > lowerInputdata)
                                    {
                                        inputdata = beforedata - 1;
                                        //値の設定
                                        target.UpdateData(inputKey, 1, false);
                                    }
                                    else if (beforedata == lowerInputdata)
                                    {
                                        //最小値だった場合は最小値に設定する。
                                        inputdata = lowerInputdata;
                                        //値の設定
                                        target.UpdateData(inputKey, 1, false);
                                    }
                                }
                                break;

                            case "_edtIp":
                            Sfip_Init(inputKey, beforedata);
                            switch (SFIPEn)
                            {
                                case false:
                                    //リストと照らし合わせ、テーブルのINDEXを確認
                                    foreach (StructureDataItem IpItem in ElectDataItems.Ips)
                                    {

                                        if (IpItem.Value == beforedata)
                                        {
                                            PcondParamIndex = IpItem.Number;
                                            //上キーの場合
                                            if (inputKey == Keys.Up)
                                            {
                                                //現在のINDEXがINDEX最大値より低い場合、一つ上のINDEXの値に設定する。
                                                foreach (StructureDataItem IpIndex in ElectDataItems.Ips)
                                                {
                                                    if (upperInputdata < IpIndex.Value)
                                                    {
                                                        upperInputdata = IpIndex.Value;
                                                    }
                                                    if (beforedata < upperInputdata)
                                                    {
                                                        inputdata = ElectDataItems.Ips[PcondParamIndex + 1].Value;
                                                        //値の設定
                                                        target.UpdateData(inputKey, Math.Abs((decimal)inputdata - target.Value), false);
                                                    }
                                                    else if (beforedata == upperInputdata)
                                                    {
                                                        //最大値だった場合は最大値に設定する。
                                                        inputdata = upperInputdata;
                                                        //値の設定
                                                        target.UpdateData(inputKey, Math.Abs((decimal)inputdata - target.Value), false);
                                                    }
                                                }
                                            }
                                            //下キーの場合
                                            else if (inputKey == Keys.Down)
                                            {
                                                //現在のINDEXがINDEX最小値より高い場合、一つ下のINDEXの値に設定する。
                                                lowerInputdata = ElectDataItems.Ips[0].Value;
                                                if (beforedata > lowerInputdata)
                                                {
                                                    inputdata = ElectDataItems.Ips[PcondParamIndex - 1].Value;
                                                    //値の設定
                                                    target.UpdateData(inputKey, Math.Abs((decimal)inputdata - target.Value), false);
                                                }
                                                //最小値だった場合は最小値に設定する。
                                                else if (beforedata == lowerInputdata)
                                                {
                                                    inputdata = lowerInputdata;
                                                    //値の設定
                                                    target.UpdateData(inputKey, Math.Abs((decimal)inputdata - target.Value), false);
                                                }
                                            }
                                        }
                                    }
                                    break;

                                case true:
                                    //リストと照らし合わせ、テーブルのINDEXを確認
                                    foreach (StructureDataItem SFIpItem in ElectDataItems.SFIps)
                                    {

                                        if (SFIpItem.Value == beforedata)
                                        {
                                            PcondParamIndex = SFIpItem.Number;
                                            //上キーの場合
                                            if (inputKey == Keys.Up)
                                            {
                                                //現在のINDEXがINDEX最大値より低い場合、一つ上のINDEXの値に設定する。
                                                foreach (StructureDataItem SFIpIndex in ElectDataItems.SFIps)
                                                {
                                                    if (upperInputdata < SFIpIndex.Value)
                                                    {
                                                        upperInputdata = SFIpIndex.Value;
                                                    }
                                                    if (beforedata < upperInputdata)
                                                    {
                                                        inputdata = ElectDataItems.SFIps[PcondParamIndex + 1].Value;
                                                        //値の設定
                                                        target.UpdateData(inputKey, Math.Abs((decimal)inputdata - target.Value), false);
                                                    }
                                                    else if (beforedata == upperInputdata)
                                                    {
                                                        //最大値だった場合は最大値に設定する。
                                                        inputdata = upperInputdata;
                                                        //値の設定
                                                        target.UpdateData(inputKey, Math.Abs((decimal)inputdata - target.Value), false);
                                                    }
                                                }
                                            }
                                            else if (inputKey == Keys.Down)
                                            {
                                                //現在のINDEXがINDEX最小値より高い場合、一つ下のINDEXの値に設定する。
                                                lowerInputdata = ElectDataItems.SFIps[0].Value;
                                                if (beforedata > lowerInputdata)
                                                {
                                                    inputdata = ElectDataItems.SFIps[PcondParamIndex - 1].Value;
                                                    //値の設定
                                                    target.UpdateData(inputKey, Math.Abs((decimal)inputdata - target.Value), false);
                                                }
                                                //最小値だった場合は最小値に設定する。
                                                else if (beforedata == lowerInputdata)
                                                {
                                                    inputdata = lowerInputdata;
                                                    //値の設定
                                                    target.UpdateData(inputKey, Math.Abs((decimal)inputdata - target.Value), false);
                                                }
                                            }
                                        }
                                    }
                                    break;
                            }
                                break;

                            case "_edtCap":
                                    Sfip_Init(inputKey, beforedata);
                                switch (SFIPEn)
                                {
                                    case false:
                                        //リストと照らし合わせ、テーブルのINDEXを確認
                                        foreach (StructureDataItem CapItem in ElectDataItems.Caps)
                                        {

                                            if (CapItem.Value == beforedata)
                                            {
                                                PcondParamIndex = CapItem.Number;
                                                //上キーの場合
                                                if (inputKey == Keys.Up)
                                                {
                                                    //現在のINDEXがINDEX最大値より低い場合、一つ上のINDEXの値に設定する。
                                                    foreach (StructureDataItem CapIndex in ElectDataItems.Caps)
                                                    {
                                                        if (upperInputdata < CapIndex.Value)
                                                        {
                                                            upperInputdata = CapIndex.Value;
                                                        }
                                                    }
                                                    if (beforedata < upperInputdata)
                                                    {
                                                        if(beforedata == 0)
                                                        {
                                                            inputdata = ElectDataItems.Caps.Find(x => x.Number == ElectDataItems.CapsBit.BoundIndex + 1).Value;
                                                            //値の設定
                                                            target.UpdateData(inputKey, Math.Abs((decimal)(inputdata) - (target.Value)), false);
                                                        }
                                                        else
                                                        {
                                                            inputdata = ElectDataItems.Caps[PcondParamIndex + 1].Value;
                                                            //値の設定
                                                            target.UpdateData(inputKey, Math.Abs((decimal)(inputdata) - (target.Value)), false);
                                                        }
                                                    }
                                                    else if (beforedata == upperInputdata)
                                                    {
                                                        //最大値だった場合は最大値に設定する。
                                                        inputdata = 0;
                                                        //値の設定
                                                        target.UpdateData(inputKey, Math.Abs((decimal)(inputdata) - (target.Value)), false);
                                                    }
                                                }
                                                //下キーの場合
                                                else if (inputKey == Keys.Down)
                                                {
                                                    //現在のINDEXがINDEX最小値より高い場合、一つ下のINDEXの値に設定する。
                                                    lowerInputdata = ElectDataItems.Caps[0].Value;
                                                    if (beforedata > lowerInputdata)
                                                    {
                                                        if (ElectDataItems.Caps.Find(x => x.Number == ElectDataItems.CapsBit.BoundIndex).Value >= ElectDataItems.Caps[PcondParamIndex - 1].Value)
                                                        {
                                                            inputdata = 0;
                                                            //値の設定
                                                            target.UpdateData(inputKey, Math.Abs((decimal)(inputdata) - (target.Value)), false);
                                                        }
                                                        else
                                                        {
                                                            inputdata = ElectDataItems.Caps[PcondParamIndex - 1].Value;
                                                            //値の設定
                                                            target.UpdateData(inputKey, Math.Abs((decimal)(inputdata) - (target.Value)), false);
                                                        }
                                                    }
                                                    else if (beforedata == lowerInputdata)
                                                    //最小値だった場合は最小値に設定する。
                                                    {
                                                        inputdata = 0;
                                                        //値の設定
                                                        target.UpdateData(inputKey, Math.Abs((decimal)(inputdata) - (target.Value)), false);
                                                    }
                                                }
                                                break;
                                            }
                                        }

                                        break;

                                    case true:
                                        //リストと照らし合わせ、テーブルのINDEXを確認
                                        foreach (StructureDataItem CapItem in ElectDataItems.Caps)
                                        {

                                            if (CapItem.Value == beforedata)
                                            {
                                                PcondParamIndex = CapItem.Number;
                                                //上キーの場合
                                                if (inputKey == Keys.Up)
                                                {
                                                    //現在のINDEXがINDEX最大値より低い場合、一つ上のINDEXの値に設定する。
                                                    foreach (StructureDataItem CapIndex in ElectDataItems.Caps)
                                                    {
                                                        if (upperInputdata < CapIndex.Value)
                                                        {
                                                            upperInputdata = CapIndex.Value;
                                                        }
                                                    }
                                                    if (beforedata < upperInputdata)
                                                    {
                                                        inputdata = ElectDataItems.Caps[PcondParamIndex + 1].Value;
                                                        //値の設定
                                                        target.UpdateData(inputKey, Math.Abs((decimal)(inputdata) - (target.Value)), false);
                                                    }
                                                    else if (beforedata == upperInputdata)
                                                    {
                                                        //最大値だった場合は最大値に設定する。
                                                        inputdata = 0;
                                                        //値の設定
                                                        target.UpdateData(inputKey, Math.Abs((decimal)(inputdata) - (target.Value)), false);
                                                    }
                                                }
                                                //下キーの場合
                                                else if (inputKey == Keys.Down)
                                                {
                                                    //現在のINDEXがINDEX最小値より高い場合、一つ下のINDEXの値に設定する。
                                                    lowerInputdata = ElectDataItems.Caps[0].Value;
                                                    if (beforedata > lowerInputdata)
                                                    {
                                                        inputdata = ElectDataItems.Caps[PcondParamIndex - 1].Value;
                                                        //値の設定
                                                        target.UpdateData(inputKey, Math.Abs((decimal)(inputdata) - (target.Value)), false);
                                                    }
                                                    else if (beforedata == lowerInputdata)
                                                    //最小値だった場合は最小値に設定する。
                                                    {
                                                        inputdata = 0;
                                                        //値の設定
                                                        target.UpdateData(inputKey, Math.Abs((decimal)(inputdata) - (target.Value)), false);
                                                    }
                                                }
                                                break;
                                            }
                                        }

                                        break;

                                }
                                break;

                            default:
                                target.InputDirectionKey(inputKey);
                                break;
                        }   
                        Download();
						return true;
					}
				}
			}
			return false;
		}
        /// <summary>
        /// 下ボタンクリック時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OverRideDown_Click(object sender, EventArgs e)
        {
            if (true == _btnOverRide.IsActive )//オーバーライド
			{
                if (true == _edtOverRide.InputRelative(-10))
                {
                    using (SettingFunction SetFunc = new SettingFunction((int)_edtOverRide.Value))
                    {
                        SetFunc.SettingMonitoring(Settings.ManualOverRide);
                    }
                }
            }
            else//IP、CAP
            {
                OnEventInputDirectionKey(Keys.Down);
            }
        }
        /// <summary>
        /// 上ボタンクリック時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OverRideUp_Click(object sender, EventArgs e)
        {
            if (true == _btnOverRide.IsActive)//オーバーライド
            {
                if (true == _edtOverRide.InputRelative(10))
                {
                    using (SettingFunction SetFunc = new SettingFunction((int)_edtOverRide.Value))
                    {
                        SetFunc.SettingMonitoring(Settings.ManualOverRide);
                    }
                }
            }
            else//IP、CAP
            {
                OnEventInputDirectionKey(Keys.Up);
            }
        }

        private void _updownBtDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Control Ctrl in this.Controls)
            {
                if (typeof(ButtonEx) == Ctrl.GetType())
                {
                    ButtonEx SelBt = Ctrl as ButtonEx;
                    if(SelBt.GetBack() == true)
                    {
                        (Ctrl as ButtonEx).IsActive = false;
                        (Ctrl as ButtonEx).SetSelected(false);
                        Download();
                    }
                }
            }
        }
        public void SetOverride(int _override)
        {
            _edtOverRide.Text = _override.ToString();
        }
        public int GetOverride()
        {
            return int.Parse(_edtOverRide.Text);
        }
        #region<NumericTextBoxのキャレット非表示：ここでは入力不可のコントールをキャレット非表示>
        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);//指定コントロールのキャレット非表示
        /// <summary>
        /// TurnOn：マウスダウン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _edtTurnOn_MouseDown(object sender, MouseEventArgs e)
        {
            HideCaret(_edtTurnOn.Handle);
        }
        /// <summary>
        /// TurnOff：マウスダウン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _edtTurnOff_MouseDown(object sender, MouseEventArgs e)
        {
            HideCaret(_edtTurnOff.Handle);
        }
        /// <summary>
        /// IP：マウスダウン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _edtIp_MouseDown(object sender, MouseEventArgs e)
        {
            HideCaret(_edtIp.Handle);
        }
        /// <summary>
        /// CAP：マウスダウン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _edtCap_MouseDown(object sender, MouseEventArgs e)
        {
            HideCaret(_edtCap.Handle);
        }
        /// <summary>
        /// ServoControl：マウスダウン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _edtServoControl_MouseDown(object sender, MouseEventArgs e)
        {
            HideCaret(_edtServoControl.Handle);
        }
        /// <summary>
        /// SfrDown：マウスダウン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _edtSfrDown_MouseDown(object sender, MouseEventArgs e)
        {
            HideCaret(_edtSfrDown.Handle);
        }
        /// <summary>
        /// SfrUp：マウスダウン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _edtSfrUp_MouseDown(object sender, MouseEventArgs e)
        {
            HideCaret(_edtSfrUp.Handle);
        }
        /// <summary>
        /// CRS：マウスダウン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _edtCrs_MouseDown(object sender, MouseEventArgs e)
        {
            HideCaret(_edtCrs.Handle);
        }
        /// <summary>
        /// POL：マウスダウン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _edtPol_MouseDown(object sender, MouseEventArgs e)
        {
            HideCaret(_edtPol.Handle);
        }
        /// <summary>
        /// Inverter：マウスダウン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _edtInverter_MouseDown(object sender, MouseEventArgs e)
        {
            HideCaret(_edtInverter.Handle);
        }
        /// <summary>
        /// ServoSelect：マウスダウン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _edtServoSelect_MouseDown(object sender, MouseEventArgs e)
        {
            HideCaret(_edtServoSelect.Handle);
        }
        /// <summary>
        /// PS：マウスダウン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _edtPS_MouseDown(object sender, MouseEventArgs e)
        {
            HideCaret(_edtPS.Handle);
        }
        /// <summary>
        /// Diameter：マウスダウン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _edtDiameter_MouseDown(object sender, MouseEventArgs e)
        {
            HideCaret(_edtDiameter.Handle);
        }
        /// <summary>
        /// OverRide：マウスダウン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _edtOverRide_MouseDown(object sender, MouseEventArgs e)
        {
            HideCaret(_edtOverRide.Handle);
        }
        #endregion
    }
}
