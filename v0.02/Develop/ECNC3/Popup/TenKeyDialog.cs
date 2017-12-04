///////////////////////////////////////////////////////////////////////////////////////////
//
// (1) システム名称 : ELENIX ECNCⅢ
// (2) ファイル名   : TenKeyDialog.cs
// (3) 概要         : 細ポップアップ・テンキーダイアログ画面
// (4) 作成日       : 2017-07-07
// (5) 作成者       : 柏原
// (6) 特記事項     : VisualStudio2015 C#
// (7) 更新履歴     : 
//
///////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Windows.Forms; //Form用
using System.Drawing;       //buttonEx_Dn.ForeColor、System.Drawing用
using ECNC3.Models;         //FileSettings用
using System.Xml;           //XML

namespace ECNC3.Views
{
	/// <summary>
	/// ポップアップ・テンキーダイアログ
	/// </summary>
	public partial class TenKeyDialog : ECNC3Form
	{
		#region <初期処理>
		/// 「OK」時に返す編集値
		public string _tenkeyValReturn = "";
		/// 0=上下キー、1=上下キー＋テンキー、2=テンキー
		public int _tenkeyMode = 1;
		/// 下1桁編集不可：TextBoxの幅を短くし、下にある"0"を表示
		private bool _oneKetaEditImpossible = false;
		//NumericTextBoxのタイプ
		private NumericTextBox.FormatTypes _editType;
		// 実数編集
		private bool _realValueEdit = false;
		//入力位置フリー
		private bool _inputPosFree = true;
		//初回選択
		private bool _firstValueSelect = false;
		//モードと位置
		string _strTenkeyMode = "";
		//空白桁0埋め値
		private bool _spaceZeroPlace = false;
		/// <summary>
		/// 「値を戻す用に」編集値を記録
		/// </summary>
		string keyDisplayBackVal;
        /// <summary>
        /// クラスメソッド初期化
        /// </summary>
        /// <param name="val">このコントロールで表示する文字</param>
        /// <param name="editType">NumericTextBoxの編集フォーマットタイプ</param>
        /// <param name="decLow">最小値</param>
        /// <param name="decUpp">最大値</param>
        /// <param name="selectValue">true=初回文字列選択</param>
        /// <param name="realValueEdit">true=実数編集</param>
        /// <param name="oneKetaEditImpossible">true=下1桁編集不可</param>
        /// <param name="strTenkeyMode">
        /// テンキー表示モード
        ///		UITenkeyModeAndPosRec.xmlに記述された表示位置やモードで表示します。
        ///		１、フォーマット：<ユニークな要素名 tenkeyMode="upDn/tenkey/both" posX="nnn" posY="nnn" />
        ///		２、画面ごとに"procCond_IP"、"aec_Up"、"axisClear"、"macroVarSet_Val"等の要素を作成
        ///			使用時は「strTenkeyMode」引数に"procCond_IP"等の文字列を入れます。
        ///		３、ITenkeyModeAndPosRec.xmlに記述が無い場合２つ方法が有り、
        ///			①引数に""を設定するとデフォルト表示(画面中央、"both")
        ///			②UITenkeyModeAndPosRec.xmlに新規に「１、フォーマット」で追加
        ///		４、tenkeyModeについて
        ///			"upDn"アップダウンのみ表示 "tenkey"テンキーのみ表示 "both"アップダウン＋テンキー表示　
        /// </param>
        /// 
        public TenKeyDialog(
			string val ="",
			NumericTextBox.FormatTypes editType= NumericTextBox.FormatTypes.Free,
			Decimal decLow=0,
			Decimal decUpp=0,
			bool firstValueSelect = false,
			bool realValueEdit = false,
			bool oneKetaEditImpossible = false,
			string strTenkeyMode = "",
            Control parent = null
            )
		{
            //入力NULLは初期値を入れる
            if ( val == "" ) val = "0";
            ToolColorEnable = true;
            //コンポーネント初期化
            InitializeComponent();
            ButtonsToolInit(Controls);
            SelectFormInit();
			//ポップアップテンキー：「CncSys.xml」非表示 / 表示の取得
			_tenkeyMode = GetSetTenkeyShow( false, _tenkeyMode );
			//画面変更
			//tenkeyModeChange( _tenkeyMode );
			//編集フォーマットタイプをセット　※必ず編集値の前で設定
			_editType = editType;                       //編集フォーマットタイプを記録
			this._keyDisplay.FormatType = editType;     //入力タイプ：
			//編集フォーマットタイプに変換
			_keyDisplay.Text = val;
			//編集値をセット
			this._keyDisplay2.Text = _keyDisplay.Text;
			//「値を戻す用に」編集値を記録
			keyDisplayBackVal = this._keyDisplay2.Text;
			//編集戻り値をセット
			_tenkeyValReturn = "";
			//入力範囲をセット
			_keyDisplay2.LowerLimit = decLow;
			_keyDisplay2.UpperLimit = decUpp;
			//初回全選択：true
			_firstValueSelect = firstValueSelect;
			//下1桁編集不可かどうかをセット
			_oneKetaEditImpossible = oneKetaEditImpossible;
			//実数：数量位置決め設定画面等＝true
			_realValueEdit = realValueEdit;
			//イベント発生時でキャレット右端設定回数：0初期化
			onceCounter = 0;

			bool minusFlg = false;
			bool dotFlg = false;
			//文字列に"-"が含まれる場合、
			if( decLow.ToString().IndexOf( "-" ) > -1 ) minusFlg = true;
			if( decUpp.ToString().IndexOf( "-" ) > -1 ) minusFlg = true;
			//文字列に"."が含まれる場合、
			if( decLow.ToString().IndexOf( "." ) > -1 ) dotFlg = true;
			if( decUpp.ToString().IndexOf( "." ) > -1 ) dotFlg = true;
			//TenkeyDiarogコントロールに"+""-"キー表示
			switch( _keyDisplay.FormatType ) {
				case NumericTextBox.FormatTypes.SignDecimalUpper3Lower3:
				case NumericTextBox.FormatTypes.SignDecimalUpper5Lower3:
					minusFlg = true;
					break;
			}
			//TenkeyDiarogコントロールに"."キー表示
			switch( _keyDisplay.FormatType ) {
				case NumericTextBox.FormatTypes.DecimalUpper1Lower2:
				case NumericTextBox.FormatTypes.DecimalUpper1Lower3:
				case NumericTextBox.FormatTypes.SignDecimalUpper3Lower3:
				case NumericTextBox.FormatTypes.SignDecimalUpper5Lower3:
					dotFlg = true;//"."キー：表示
					break;
			}
			//空白桁0埋め値
			switch( this._keyDisplay.FormatType ) {
				case NumericTextBox.FormatTypes.IntegerZeroPlace2:
				case NumericTextBox.FormatTypes.IntegerZeroPlace3:
					//空白桁0埋め値
					_spaceZeroPlace = true;
					break;
			}
			if( dotFlg ) {
				button_dot.Enabled = true;//"."キー：表示
			}
			if( minusFlg ) {
				button_Minus.Enabled = true;//"-"キー：表示
				button_Plus.Enabled = true;//"+"キー：表示
			}
			//表示モード
			_strTenkeyMode = strTenkeyMode;
			if( _strTenkeyMode == string.Empty || _strTenkeyMode == null ) {
				//表示モードが指定していない場合
				//真ん中表示//StartPositin=CentrParentは1024*768以外センターにならないので追加
				//MainFormの幅と高さ取得
				Form mainForm = new MAINForm();
				int pWidth = mainForm.Size.Width;
				int pHeight = mainForm.Size.Height;
				mainForm.Dispose();                 //mainForm破棄
				mainForm = null;
				//MainFormの中心を取得
				int pHarfWidth = pWidth / 2;
				int pHarfHeight = pHeight / 2;
				//この画面の中心を取得
				int harfWidth = this.Width / 2;
				int harfHeight = this.Height / 2;
				//MainFormの中心からこの画面の中心を引いてこの画面を中央表示
				int WidthPos = pHarfWidth - harfWidth;
				int heightPos = pHarfHeight - harfHeight;
				this.Location = new Point( WidthPos, heightPos );//表示位置設定
			}
            //この画面で入力禁止にしたキーはハードウェアキーボードでも入力禁止に設定
            string[] filterCodes = null;
            int count = 0;
            if (minusFlg == false)
            {
                Array.Resize(ref filterCodes, count + 1);
                filterCodes[count] = "-";
            }
            if (dotFlg == false)
            {
                count++;
                Array.Resize(ref filterCodes, count + 1);
                filterCodes[count] = ".";
            }
            _keyDisplay2.SetKeyFilterCode(filterCodes);
        }
		/// <summary>初回のみロード</summary>
		/// <param name="sender">イベント送信元オブジェクト</param>
		/// <param name="e">イベントパラメータ</param>
		private void TenKeyDialog_Load( object sender, EventArgs e )
		{
			//▲▼長押しタイマ作成
			_TimerUpDn = new System.Threading.Timer(
						 new System.Threading.TimerCallback( fireUpDn ), null, System.Threading.Timeout.Infinite, 200 );
			//タイトル設定
			buttonEx_Title.Text = this.Text;
			//
			TenkeyModeAndPosXmlRead();
            this.BackColor = FileUIStyleTable.ToolBackColor;
            this.ForeColor = FileUIStyleTable.ToolForeColor;
            this.OutLineColor = FileUIStyleTable.ToolLineColor;
            _AllControlsStyleChange(this.Controls);
        }
        /// <summary>
        /// 全コントロールのスタイル変更処理（回帰処理）
        /// </summary>
        /// <param name="ctrls"></param>
        private void _AllControlsStyleChange(Control.ControlCollection ctrls)
        {
            if (ctrls != null)
            {
                foreach (Control ctrl in ctrls)
                {
                    ctrl.BackColor = FileUIStyleTable.ToolBackColor;
                    ctrl.ForeColor = FileUIStyleTable.ToolForeColor;
                    if (ctrl.Controls != null)
                    {
                        _AllControlsStyleChange(ctrl.Controls);
                    }
                }
            }
            //不具合対応：シュミレータ/実機：▲を1回押しても、全選択が解除されないので、一度タイトルをクリックしてフォーカスを与える。：柏原
            buttonEx_Title.PerformClick();
        }

        /// <summary>設定結果取得関数定義</summary>
        public delegate void NotifyReturnDelegateOk();//OKボタン
		/// <summary>OK終了通知</summary>
		private NotifyReturnDelegateOk _notifyReturnOk = null;
		/// <summary>>設定結果取得</summary>
		public NotifyReturnDelegateOk NotifyReturnOk
		{
			set { _notifyReturnOk = value; }
		}
        /// <summary>設定結果取得関数定義</summary>
        public delegate void NotifyCloseDelegateOk();//閉じるボタン
                                                      /// <summary>OK終了通知</summary>
        private NotifyCloseDelegateOk _notifyClose = null;
        /// <summary>>設定結果取得</summary>
        public NotifyCloseDelegateOk NotifyClose
        {
            set { _notifyClose = value; }
        }

        /// <summary>設定結果取得関数定義</summary>
        public delegate void NotifyReturnDelegateArrowUp();//▲上ボタン
														   /// <summary>OK終了通知</summary>
		private NotifyReturnDelegateArrowUp _notifyReturnArrowUp = null;
		/// <summary>>設定結果取得</summary>
		public NotifyReturnDelegateArrowUp NotifyArrowUp
		{
			set { _notifyReturnArrowUp = value; }
		}
		/// <summary>設定結果取得関数定義</summary>
		public delegate void NotifyReturnDelegateArrowDn();//▼下ボタン
														   /// <summary>OK終了通知</summary>
		private NotifyReturnDelegateArrowDn _notifyReturnArrowDn = null;
		/// <summary>>設定結果取得</summary>
		public NotifyReturnDelegateArrowDn NotifyArrowDn
		{
			set { _notifyReturnArrowDn = value; }
		}
		#endregion
		#region <終了処理>
		/// <summary>
		/// フォーム：閉じる直前
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TenKeyDialog_FormClosed( object sender, FormClosedEventArgs e )
		{
			if( _TimerUpDn != null ) {
				//タイマー破棄
				_TimerUpDn.Dispose();
				_TimerUpDn = null;
			}
		}
		#endregion
		#region <テンキーモードと表示位置をファイル読込>
		private bool _ncImmediateFlg = false;	//NC即時値フラグ：true=値が変化毎に更新
        private bool _ncImmediateStyle = false; //NC即時値表示フラグ：true = 上下キーのみ、OK、戻すボタン非表示
		/// <summary>
		/// テンキーモードと表示位置をファイルから読み込み
		/// </summary>
		private void TenkeyModeAndPosXmlRead()
		{
			try {
				string masterFolder = @FilePathInfo.MasterData;//\Masterフォルダ
				XmlDocument xmlDocument = new XmlDocument();

				xmlDocument.Load( masterFolder + "UITenkeyModeAndPosRec.xml" );//読込
				XmlNode root = xmlDocument.DocumentElement;
				int loopCont = root.ChildNodes.Count;
				int intTenkeyMode = 2;
				_ncImmediateFlg = false;//NC更新フラグ：true=値が変化毎に更新
                _ncImmediateStyle = false;

                for ( int iCount = 0 ; iCount < loopCont ; iCount++ ) {
					XmlNode node = root.ChildNodes[iCount];
					if( node.Name == _strTenkeyMode ) {
						_ncImmediateFlg = false;
                        _ncImmediateStyle = true;
						switch( _strTenkeyMode ) {
							//IP、CAP、OVERRIDE用
							case "procCond_IP": _ncImmediateStyle = !(_ncImmediateFlg = true); break;
							case "procCond_CAP": _ncImmediateStyle = !(_ncImmediateFlg = true); break;
                            case "procCond_OVERRIDE": _ncImmediateStyle = !(_ncImmediateFlg = true); break;
                            case "procCond_T-ON": _ncImmediateFlg = true; break;
                            case "procCond_T-OFF": _ncImmediateFlg = true; break;
                            case "procCond_SC": _ncImmediateFlg = true; break;
                            case "procCond_SFRDOWN": _ncImmediateFlg = true; break;
                            case "procCond_SFRUP": _ncImmediateFlg = true; break;
                            case "procCond_ROT": _ncImmediateFlg = true; break;
                            case "procCond_POL": _ncImmediateFlg = true; break;
                            case "procCond_HYD": _ncImmediateFlg = true; break;
                            case "procCond_AC": _ncImmediateFlg = true; break;
                            case "procCond_DC": _ncImmediateFlg = true; break;
                        }
						if(false == _ncImmediateStyle) {
							//IP、CAP、OVERRIDE用
							_keyDisplay2.ReadOnly = true;       //入力表示：使用不可
							InputModeChange.Enabled = false;    //▲▼ボタン：使用不可
							buttonEx3.Visible = false;          //テンキーボタン：非表示
							buttonEx2.Visible = false;          //両方ボタン：非表示
							_btnOK.Visible = false;             //「OK」ボタン：非表示
							_btnCANCEL.Visible = false;         //「戻す」ボタン：非表示
						}
						int heightPosAjs = 0;
						//テンキーモード：取得
						string strTenkeyMode = ( (XmlElement)node ).GetAttribute( "tenkeyMode" );
						switch( strTenkeyMode ) {
							case "upDn": intTenkeyMode = 0; break;//heightPosAjs = -215;
							case "tenkey": intTenkeyMode = 1; break;//heightPosAjs = -108;
							case "both": intTenkeyMode = 1; break;
							default: intTenkeyMode = 1; break;
						}
						//表示位置：取得
						string strPosX = ( (XmlElement)node ).GetAttribute( "posX" );
						string strPosY = ( (XmlElement)node ).GetAttribute( "posY" );
						int widthPos = int.Parse( strPosX );
						int heightPos = int.Parse( strPosY );
						heightPos = heightPos - heightPosAjs;
						this.Location = new Point( widthPos, heightPos );//表示位置設定
						break;
					}
				}
				//テンキーモード3種類のうちどれかに設定
				tenkeyModeChange( intTenkeyMode );
				xmlDocument = null;
			} catch( Exception exc ) {
				//例外処理
				MessageBox.Show( exc.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error );
			}
		}
		#endregion
		#region <画面モード：0=上下、1=テンキー、2=上下+テンキー、>
		/// <summary>
		/// ポップアップテンキー：「CncSys.xml」非表示/表示の取得/設定
		/// </summary>
		/// <param name="boolVal">false=読み込み true=書き込み</param>
		/// <param name="tenkeyMode">=上下のみ、1=上下+テンキー、2=テンキーのみ</param>
		private int GetSetTenkeyShow( bool boolVal, int tenkeyMode )
		{
			using( FileSettings fs = new FileSettings() ) {
				if( boolVal ) {   //ファイル書き込み
					fs.Read();
					fs.WriteAttr( "Root/PopupTenkeyDialog/Tenkey", "mode", tenkeyMode.ToString() );//0=上下のみ、1=上下+テンキー
					fs.Write();
				} else {   //ファイル読み込み
					fs.Read();
					tenkeyMode = fs.AttrValue( "Root/PopupTenkeyDialog/Tenkey", "mode" );//0=上下のみ、1=上下+テンキー、2=テンキーのみ
				}
			}
			return tenkeyMode;//読み込み時に使用、書き込み時は不要
		}
		/// <summary>
		/// テンキーモード3種類のうちどれかに設定
		/// </summary>
		/// <param name="tenkeyMode">0=上下キーのみ、1=上下キー＋テンキー、2=テンキー</param>
		private void tenkeyModeChange( int tenkeyMode )
		{
			switch( tenkeyMode ) {
				case 0: buttton_InputMode_UpDn( null, null ); break;//上下キーのみ
				case 1: buttton_InputMode_Tenkey( null, null ); break;//テンキー
				case 2: buttton_InputMode_Both( null, null ); break;//上下キー＋テンキー
				default: buttton_InputMode_Both( null, null ); break;//上下キー＋テンキー
			}
		}
		/// <summary>
		/// テンキー：表示/非表示
		/// </summary>
		/// <param name="boolShowHide"></param>
		private void numButtonShowHide( bool boolShowHide )
		{
			button_0.Visible = boolShowHide;
			button_1.Visible = boolShowHide;
			button_2.Visible = boolShowHide;
			button_3.Visible = boolShowHide;
			button_4.Visible = boolShowHide;
			button_5.Visible = boolShowHide;
			button_6.Visible = boolShowHide;
			button_7.Visible = boolShowHide;
			button_8.Visible = boolShowHide;
			button_9.Visible = boolShowHide;
			button_dot.Visible = boolShowHide;
			AllClearKey.Visible = boolShowHide;
			BackSpaceKey.Visible = boolShowHide;
			button_division.Visible = boolShowHide;
			button_asterisk.Visible = boolShowHide;
			button_Minus.Visible = boolShowHide;
			button_Plus.Visible = boolShowHide;
			EnterKey.Visible = boolShowHide;
		}
		/// <summary>
		/// 上下ボタン：表示/非表示
		/// </summary>
		/// <param name="boolShowHide"></param>
		private void updnButtonShowHide( bool boolShowHide )
		{
			buttonEx_Up.Visible = boolShowHide;
			buttonEx_Dn.Visible = boolShowHide;
		}
		/// <summary>
		/// 入力モード：上下キー
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttton_InputMode_UpDn( object sender, MouseEventArgs e )
		{
			InputModeChange.SetBack( true );
			buttonEx2.SetBack( false );
			buttonEx3.SetBack( false );
			buttton_InputMode_All( sender, 0, false, true, -215 );
			this.Refresh();//枠線切れ対応
		}
		/// <summary>
		/// 入力モード：テンキー
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttton_InputMode_Tenkey( object sender, MouseEventArgs e )
		{
			InputModeChange.SetBack( false );
			buttonEx2.SetBack( false );
			buttonEx3.SetBack( true );
			buttton_InputMode_All( sender, 1, true, false, -108 );
			this.Refresh();//枠線切れ対応
		}
		/// <summary>
		/// 入力モード：テンキー/上下キー
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttton_InputMode_Both( object sender, MouseEventArgs e )
		{
			InputModeChange.SetBack( false );
			buttonEx2.SetBack( true );
			buttonEx3.SetBack( false );
			buttton_InputMode_All( sender, 2, true, true, 0 );
			this.Refresh();//枠線切れ対応
		}
		/// <summary>
		/// 入力モード設定
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="tenkeyMode"></param>
		/// <param name="numB"></param>
		/// <param name="updnB"></param>
		/// <param name="yMove"></param>
		private void buttton_InputMode_All( object sender, int tenkeyMode, bool numB, bool updnB, int yMove )
		{
			_tenkeyMode = tenkeyMode;
			if( sender != null ) GetSetTenkeyShow( true, _tenkeyMode );//ファイル：保存
			numButtonShowHide( numB );                //テンキー：表示
			updnButtonShowHide( updnB );              //上下ボタン：非表示
			butttonYMoveValue( yMove );               //ボタン：Y方向移動量
			//画面を変えるごとにキャレットが左端になるので元の位置に移動
			int pos = getKeyDisplayCursolPos();     //キャレット：元の位置に移動
			setKeyDisplayCursolPos( pos );            //カーソル位置とフォーカスをセット
		}
		/// <summary>
		/// ボタン：Y方向移動量
		/// </summary>
		/// <param name="intYShift">Y方向移動量</param>
		private void butttonYMoveValue( int intYShift )
		{
			//フォームサイズ：変更
			this.Size = new System.Drawing.Size( 314, 540 + intYShift );
			//OKボタン：元位置：
			_btnOK.Top = 495 + intYShift;
			//キャンセルボタン：
			_btnCANCEL.Top = 495 + intYShift;
			//閉じるボタン：
			buttonEx1.Top = 495 + intYShift;
			//▲▼ボタン
			buttonEx_Up.Top = 372 + intYShift;//
			buttonEx_Dn.Top = 432 + intYShift;//
		}
		#endregion
		#region <入力数字表示：keyDisplay>
		//keyDisplayカーソル位置を取得
		private int getKeyDisplayCursolPos()
		{
			return _keyDisplay2.SelectionStart;
		}
		//keyDisplayカーソル位置とフォーカスを設定
		private void setKeyDisplayCursolPos( int pos )
		{
			_keyDisplay2.setCursolPos( pos );
		}
		/// <summary>
		/// イベント発生時でキャレット右端設定回数
		/// </summary>
		static int onceCounter = 0;
		/// <summary>
		/// Paintイベント：キャレットに常にフォーカスをあてる
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TenKeyDialog_Paint( object sender, PaintEventArgs e )
		{
			if( _firstValueSelect ) {
				//初回選択
				if( onceCounter < 2 ) {//1回目では移動しないので2回は行う
                    //不具合対応：シュミレータ/実機：▲を1回押しても、全選択が解除されないので、一度タイトルをクリックしてフォーカスを与える。：柏原
                    buttonEx_Title_ClickSend();

                    _keyDisplay2.SelectAll();//全選択
					_keyDisplay2.Focus();
                } else {
					//初回以降
					if( _keyDisplay2.SelectionLength == _keyDisplay.Text.Length ) {
						//全選択時は、キャレットをIビームにしない
					} else {
						setForcusKeyDisplay();              //フォーカスをセット
					}
				}
			} else {
				//初回選択なし
				if( onceCounter < 2 ) {//1回目では移動しないので2回は行う
					int pos = _keyDisplay2.Text.Length;   //キャレット：右端に移動
					setKeyDisplayCursolPos( pos );        //カーソル位置とフォーカスをセット
				}
			}
			onceCounter++;
		}

        private void buttonEx_Title_ClickSend()
        {
            //buttonEx_Title.PerformClick();
            //this.OnClick(new EventArgs());

            buttonEx_Title.GetType().InvokeMember("OnClick",
                               System.Reflection.BindingFlags.InvokeMethod |
                               System.Reflection.BindingFlags.NonPublic |
                               System.Reflection.BindingFlags.Instance,
                               null,
                               buttonEx_Title,
                               new object[] { EventArgs.Empty });
        }


        private void buttonEx_Title_Click(object sender, EventArgs e)
        {
            // クリックイベントチェック用ハンドラ
        }



        /// <summary>
        /// 選択されている場合、その文字列を削除
        /// </summary>
        private bool keyDisplaySelDel()
		{
			return _keyDisplay2.setSelDel();
		}
		/// <summary>
		/// コントロールから外れた時：キャレットに常にフォーカスをあてる
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _keyDisplay_Leave( object sender, EventArgs e )
		{
			//setForcusKeyDisplay(); //フォーカスをセット
		}
		/// <summary>
		/// keyDisplayにフォーカスをセット
		/// </summary>
		private void setForcusKeyDisplay()
		{
			if( _firstValueSelect ) {
				//初回選択
				if( _inputPosFree ) {//入力位置：自由
                    _keyDisplay2.SelectionLength = 0;//選択無し
					_keyDisplay2.Focus();
				} else {//入力位置：右端
					int pos = _keyDisplay2.Text.Length; //キャレット：取得
					setKeyDisplayCursolPos( pos );        //キャレット：位置設定
				}
			} else {
				//初回選択なし
				_keyDisplay2.SelectionLength = 0;//選択無し
				_keyDisplay2.Focus();
			}
		}
		#endregion
		#region <ボタン：クリック；OK、値を戻す、キャンセル>
		/// <summary>
		/// OKボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _btnOK_Click( object sender, MouseEventArgs e )
		{
			if( _spaceZeroPlace ) {
				//固定長：処理無し
			} else {
				//可変長
				//数字以外の場合、"0"を代入
				string minString = "0";//_keyDisplay2.LowerLimit.ToString();
				//""の場合＝0
				if( _keyDisplay2.Text == "" ) _keyDisplay2.Text = minString;
				//符号のみの場合＝0
                switch (_keyDisplay2.Text)
                {
                    case "": _keyDisplay2.Text = minString; break;
                    case ".": _keyDisplay2.Text = minString; break;
                    case "-": _keyDisplay2.Text = minString; break;
                    case "-.": _keyDisplay2.Text = minString; break;
                }

				//入力範囲をセット
				decimal decimalVal = decimal.Parse( _keyDisplay2.Text );
				if( _keyDisplay2.LowerLimit <= decimalVal && _keyDisplay2.UpperLimit >= decimalVal ) {
					//範囲内
				} else {
					//範囲外：
					using( MessageDialog msg = new MessageDialog() ) {
						msg.Error( 5508, this );
						{
							return;//"入力範囲外" "値が範囲外なので、入力できません。"
						}
					}
				}
			}
			//フォーマット設定
			_keyDisplay.Text = _keyDisplay2.Text;
			//編集値をセット
			_tenkeyValReturn = _keyDisplay.Text;
			if( _oneKetaEditImpossible ) _tenkeyValReturn += "0";//1桁追加
			//呼び出し先に通知
			_notifyReturnOk.Invoke();
			//タイマー破棄
			_TimerUpDn.Dispose();
			//この画面を閉じる
			this.Close();
		}
		/// <summary>
		/// 値を戻すボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _btnValueBack_Click( object sender, MouseEventArgs e )
		{
			int pos = getKeyDisplayCursolPos();         //キャレット：元の位置
			this._keyDisplay2.Text = keyDisplayBackVal;  //初回に記録していた値
			setKeyDisplayCursolPos( _keyDisplay2.Text.Length );//キャレットを右端に移動する
															   //setKeyDisplayCursolPos(pos);                  //キャレットを元の位置に戻す ※桁が減る場合が有るので「元の位置」に戻せない
		}
        /// <summary>
        /// 閉じるボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnCANCEL_Click(object sender, MouseEventArgs e)
        {
            //タイマー破棄
            //_TimerUpDn.Dispose();
            if (_notifyClose != null) { 
                //呼び出し先に通知
                _notifyClose.Invoke();
            }
            //この画面を閉じる
            this.Close();
		}
        #endregion
        #region <ハード/ソフトキーボード判定>
        private bool _softKey = false;//ソフトキー入力
        /// <summary>
        /// 入力コントロール：キープレス(ハード/ソフトキーボード判定)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _keyDisplay2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (_softKey)
            {//ソフトキー入力は有効
                _softKey = false;
            }
            else
            {//ハードキー入力は無効
               _btnValueBack_Click(null,null);//値が消えるので値を戻す。
               e.Handled = true;//buttonTenkey_Clickを通過しないで直接ここにくる場合、ハードキーとします。
            }
        }
        #endregion
        #region <0-9キーボタン：クリック>
        /// <summary>
        /// 編集対象が"."の場合、入力不可
        /// </summary>
        //static bool desableDoTKeta = false;
        /// <summary>
        /// 0-9キーを押された
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonTenkey_Click( object sender, EventArgs e )
		{
            _softKey = true;//ソフトキー入力
			bool boolDel = keyDisplaySelDel();//選択文字列削除

			int caretPos = getKeyDisplayCursolPos();//左端=0からのキャレット位置
			string orgString = this._keyDisplay2.Text;//全体値
			int orgStringLen = this._keyDisplay2.Text.Length;//長さ

			string inString = ( (ButtonEx)sender ).Text;//入力値

			//入力フォーマット
			switch( this._keyDisplay.FormatType ) {
				//小数：整数1桁：単純に追加できないのでここで指定ファーマットの数字を作成
				//
				case NumericTextBox.FormatTypes.DecimalUpper1Lower2:
				case NumericTextBox.FormatTypes.DecimalUpper1Lower3:
				case NumericTextBox.FormatTypes.SignDecimalUpper3Lower3:
				case NumericTextBox.FormatTypes.SignDecimalUpper5Lower3:

					if( _keyDisplay2.SelectedText == orgString ) {
						//全選択の場合、元の数字を全消去し、入力文字に変更
						_keyDisplay2.Text = inString;
						setKeyDisplayCursolPos( 1 );
						return;
					}
					if( _inputPosFree ) {
						//入力位置：自由
						int dotIndex = orgString.IndexOf( "." );//小数点位置を記録
						orgString = orgString.Replace( ".", "" );   //小数一時的に削除
						if( dotIndex == -1 ) {//整数
							orgString = orgString.Insert( caretPos, inString );
						} else {//小数
							int seisuu = dotIndex - caretPos;//dotIndex=1から3、caretPos=0からorgStringLen
							if( seisuu > -1 ) {//小数点：キャレット[.]
								if( caretPos < 1 ) orgString = orgString.Insert( 0, inString );
								else orgString = orgString.Insert( caretPos, inString );
								dotIndex++;  //桁増加
							} else {//小数点：[.]キャレット
								orgString = orgString.Insert( caretPos - 1, inString );
							}
							orgString = orgString.Insert( dotIndex, "." );        //元の小数点位置に小数点を挿入
						}
					} else {
						//入力位置：右端
						orgString = orgString.Insert( orgString.Length, inString );//入力文字追加
					}

					break;

				//整数
				default:
                    //文字列を挿入
                    orgString = orgString.Insert(caretPos, inString);
					break;
			}
			//文字列を数値に変換
			Decimal decString = Decimal.Parse( orgString );
			if( _keyDisplay2.UpperLimit < decString || _keyDisplay2.LowerLimit > decString ) {
				//上限/下限値超える場合
				int len1 =_keyDisplay2.LowerLimit.ToString().Length;
				int len2 =_keyDisplay2.UpperLimit.ToString().Length;
				int intComp = len1;
				if( len2 > len1 ) intComp = len2;
				if( orgString.Length > intComp ) {
					//文字列長が最大を超える場合、入力ストップ
					return;
				}
			}
			if( _realValueEdit ) {
				//数量位置決め設定画面等＝true
				//実数
				//書式設定
				_keyDisplay.Text = orgString;
				string tmpString = _keyDisplay.Text;
				//文字列長のチェック
				if( !_keyDisplay2.upperLowerSepComp( orgString, _keyDisplay2.LowerLimit.ToString(), _keyDisplay2.UpperLimit.ToString() ) ) {//フォーマットの桁がオーバーしたので戻す
					return;
				}
			} else {
				//固定長：整数
				_keyDisplay.Text = orgString;//書式設定
				orgString = _keyDisplay.Text;
			}
			_keyDisplay2.Text = orgString; //値をセット
			//桁数が増加した場合、caretPos(キャレット移動)移動

			if (_spaceZeroPlace){//0埋め
                if (boolDel)
                {
                    int len = _keyDisplay2.Text.Length;
                    if (len < 0) len = 0;
                    caretPos = len;
                }
            }
            else{
                if (this._keyDisplay2.Text.Length > orgStringLen) caretPos++;
            }
            //キャレット位置設定
            setKeyDisplayCursolPos( caretPos );
		}
		/// <summary>
		/// 数字解析：処理に使用する数字の情報を取得
		/// </summary>
		/// <param name="orgString">編集文字列</param>
		/// <param name="inStringLen">編集文字列長</param>
		/// <param name="inStringLenDotless">編集文字列長："."はノーカウント</param>
		/// <param name="inStringLenMinusless">編集文字列長："-"はノーカウント</param>
		/// <param name="minusFlg">false=プラス値、true=マイナス値</param>
		/// <param name="realNumFlg">false=整数値、true=実数値</param>
		/// <param name="upperString">整数部文字列</param>
		/// <param name="lowerString">小数部文字列</param>
		/// <param name="dotPos">小数点位置：左端=0、右端=文字列長</param>
		/// <param name="minusPos">-1="-"無し、0から"-"の見つかった位置</param>
		private void analyzeStringVal(
			string orgString,
			ref int inStringLen,
			ref int inStringLenDotless,
			ref int inStringLenMinusless,
			ref bool minusFlg,
			ref bool realNumFlg,
			ref string upperString,
			ref string lowerString,
			ref int dotPos,
			ref int minusPos
			)
		{
			string inString = orgString;                    //orgStringコピー値			
			decimal inDecimal = decimal.Parse( inString );  //Decimal値

			inStringLen = inString.Length;                  //文字列長
			minusPos = inString.IndexOf( "-" );             //マイナス位置
			minusFlg = false;                               //minusFlg=false ：整数
			if( minusPos > -1 ) minusFlg = true;            //minusFlg=true  ：小数

			dotPos = inString.IndexOf( "." );                 //小数位置：-1＝小数点無し
			inStringLenDotless = orgString.Replace( ".", "" ).Length;
			inStringLenMinusless = orgString.Replace( "-", "" ).Length; ;
			realNumFlg = false;                                //realNumFlg=false ：整数
			if( dotPos > -1 ) realNumFlg = true;              //realNumFlg=true  ：小数
			if( dotPos == -1 ) {
				//整数
				upperString = "";
				lowerString = "";
			} else {//小数
				upperString = inString.Substring( 0, dotPos );                           //整数部 
				lowerString = inString.Substring( dotPos + 1, inStringLen - dotPos - 1 );//小数部
			}
		}
		#endregion
		#region <テンキーボタン：クリック>
		/// <summary>
		/// AC(全てクリア)キーを押された
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AllClearKey_Click( object sender, EventArgs e )
		{
			int pos = getKeyDisplayCursolPos();             //キャレット：元の位置
			this._keyDisplay2.Text = "";                     //_keyDisplay2テキスト：消去

			switch( this._keyDisplay.FormatType ) {
				case NumericTextBox.FormatTypes.IntegerZeroPlace2:
				case NumericTextBox.FormatTypes.IntegerZeroPlace3:
					//固定長：消しても書式は表示
					_keyDisplay.Text = _keyDisplay2.Text;
					_keyDisplay2.Text = _keyDisplay.Text;
					break;
				default:
					//数量位置決め設定画面等＝true
					break;
			}
			setKeyDisplayCursolPos( _keyDisplay2.Text.Length );//キャレットを右端に移動する
		}

		/// <summary>
		/// BS(BackSpace)キーを押された
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BackSpacerKey_Click( object sender, EventArgs e )
		{
			keyDisplaySelDel();//選択文字列削除

			int caretPos = getKeyDisplayCursolPos();    //左端=0からのキャレット位置
			if( caretPos == 0 ) return;                  //キャレット左端は処理無し
			string orgString = this._keyDisplay2.Text;   //全体値
			switch( _keyDisplay.FormatType ) {
				//整数：空白0埋め
				case NumericTextBox.FormatTypes.IntegerZeroPlace2:
				case NumericTextBox.FormatTypes.IntegerZeroPlace3:
					orgString = orgString.Remove( caretPos - 1, 1 );//キャレット左の文字削除
					caretPos++;//文字削除するとキャレットが左にずれるので1桁戻す。
					break;
                //整数
                case NumericTextBox.FormatTypes.SignLong5:
				case NumericTextBox.FormatTypes.SignLong10:
					orgString = orgString.Remove( caretPos - 1, 1 );//キャレット左の文字削除
					break;
				//実数
				default:
					orgString = BackSpacerKey_Decima( orgString, _keyDisplay.FormatType, ref caretPos );
					break;
			}
			if( orgString.Substring( 0 ) == "." || orgString.Substring( 0 ) == "-" ) {
			} else {
				//先頭が"."や"-"では無い
				if( orgString != "" ) {//何か入っている状態
					Decimal decString = Decimal.Parse( orgString );
					//桁が減った
					if( decString.ToString().Length > this._keyDisplay2.Text.Length ) {
						caretPos++;
					} else if( decString.ToString().Length < this._keyDisplay2.Text.Length ) {//桁が増えた
						caretPos--;
					}
				}
			}
			switch( _keyDisplay.FormatType ) {
				//整数
				case NumericTextBox.FormatTypes.IntegerZeroPlace2:
				case NumericTextBox.FormatTypes.IntegerZeroPlace3:
					this._keyDisplay.Text = orgString;          //書式セット
					this._keyDisplay2.Text = _keyDisplay.Text;  //値をセット
					break;
				default:
					//if( !_keyDisplay2.upperLowerSepComp( orgString, _keyDisplay2.LowerLimit.ToString(), _keyDisplay2.UpperLimit.ToString() ) ) {
					//文字列長のチェック：長い場合
					//return;
					//}
					this._keyDisplay2.Text = orgString;  //値をセット
					break;
			}
			setKeyDisplayCursolPos( caretPos );
		}
		/// <summary>
		/// 符号チェック
		/// </summary>
		/// <param name="compString"></param>
		/// <returns></returns>
		private bool sighJagge( string compString )
		{
			if( compString.IndexOf( "." ) > -1 ) return false;
			if( compString.IndexOf( "-" ) > -1 ) return false;
			return true;
		}
		/// <summary>
		///  BS(BackSpace)キーを押された：Decima型
		/// </summary>
		/// <param name="orgString"></param>
		/// <returns></returns>
		private string BackSpacerKey_Decima( string orgString, NumericTextBox.FormatTypes formatType, ref int caretPos )
		{
			string workString = orgString;
			if( _realValueEdit ) {
				//実数
				if( _inputPosFree ) {//入力位置：自由
					workString = workString.Remove( caretPos - 1, 1 );
					caretPos--;//文字削除するとキャレットが左にずれるので1桁戻す。
				} else {//入力位置：右端
					workString = workString.Remove( workString.Length - 1, 1 );//キャレット左の文字削除
				}
				caretPos++;//文字削除するとキャレットが左にずれるので1桁戻す。
				return workString;
			} else {
				//固定長：整数
				int dotIndex = workString.IndexOf( "." );            //小数点位置を記録
				if( dotIndex == -1 ) {//整数
									  //整数：空白0埋め
					workString = workString.Remove( caretPos - 1, 1 );//キャレット左の文字削除
					return workString;
				} else {
					//小数
					workString = workString.Replace( ".", "" );               //小数一時的に削除
					if( workString == "" ) return workString;                //文字無し

					switch( _keyDisplay.FormatType ) {
						case NumericTextBox.FormatTypes.DecimalUpper1Lower2:
						case NumericTextBox.FormatTypes.DecimalUpper1Lower3:
						case NumericTextBox.FormatTypes.SignZeroDecimalUpper3Lower3:
						case NumericTextBox.FormatTypes.SignDecimalUpper3Lower3:
						case NumericTextBox.FormatTypes.SignDecimalUpper5Lower3:
							if( caretPos < 2 ) workString = workString.Remove( 0, 1 );
							else {
								if( dotIndex >= caretPos ) workString = workString.Remove( caretPos - 1, 1 );//キャレット左の文字削除
								else workString = workString.Remove( caretPos - 2, 1 );//キャレット左の文字削除
							}
							break;
						default:
							workString = workString.Remove( caretPos - 1, 1 );//キャレット左の文字削除
							break;
					}
					dotIndex--;
					if( dotIndex < 0 ) dotIndex = 0;
					string stringTemp = workString.Insert( dotIndex, "." );
					if( stringTemp == "." || stringTemp == "-" ) {//数字無し
					} else {//数字が有る場合
						decimal decimalTemp = decimal.Parse( stringTemp );
						//大きい場合、戻す
						if( decimalTemp > _keyDisplay2.UpperLimit ) stringTemp = orgString;
						//小さい場合、戻す
						if( decimalTemp < _keyDisplay2.LowerLimit ) stringTemp = orgString;
					}
					return stringTemp;
				}
			}
		}
		/// <summary>
		/// "/"キーを押された
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button48_Click( object sender, EventArgs e )
		{
			string inString = ( (Button)sender ).Text;
			//文字列追加
			this._keyDisplay2.Text += inString;
			//桁数がオーバーした場合、１桁削除
			if( _keyDisplay2.MaxLength < _keyDisplay2.Text.Length ) {
				this._keyDisplay2.Text = this._keyDisplay2.Text.Remove( 0, 1 );
			}
		}
		/// <summary>
		///  "*"キーを押された
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button46_Click( object sender, EventArgs e )
		{
		}
		/// <summary>
		///  "Enter"キーを押された
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EnterKey_Click( object sender, EventArgs e )
		{
		}
		/// <summary>
		///  "."キーを押された
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_dot_Click( object sender, EventArgs e )
		{
			if( _inputPosFree ) {//入力位置：自由
				bool minusFlg = false;
				int minusIndex = _keyDisplay2.Text.IndexOf( "-" );

				keyDisplaySelDel();//選択文字列削除

				//キャレット位置に文字列追加
				int caretPos = getKeyDisplayCursolPos();

				if( minusIndex > -1 ) {//マイナス符号付き
					minusFlg = true;
				}
				if( minusFlg && caretPos == 0 ) {
					//[.-]：マイナス符号の前に"."は不可
					return;
				}

				//挿入文字
				string inString = ( (Button)sender ).Text;
				//すでに小数点が有る場合、その小数点を削除
				int dotIndex = _keyDisplay2.Text.IndexOf( "." );          //小数点位置を記録
				if( dotIndex > -1 ) {//"."が有る場合
					_keyDisplay2.Text = _keyDisplay2.Text.Remove( dotIndex, 1 ); //"."削除
				}
				if( dotIndex > 0 ) {
					if( dotIndex < caretPos ) caretPos--;
				}

				if( caretPos > _keyDisplay2.Text.Length ) {
					_keyDisplay2.Text = _keyDisplay2.Text.Insert( _keyDisplay2.Text.Length, inString );
					//キャレット位置：設定
					setKeyDisplayCursolPos( caretPos );
				} else {
					_keyDisplay2.Text = _keyDisplay2.Text.Insert( caretPos, inString );
					//キャレット位置：設定
					setKeyDisplayCursolPos( caretPos + 1 );
				}
			} else {//入力位置：右端
					//すでに小数点が有る場合
				if( _keyDisplay2.Text.IndexOf( "." ) > -1 ) return;
				keyDisplaySelDel();//選択文字列削除

				string workString = _keyDisplay2.Text;
				if( workString.IndexOf( "." ) > -1 ) {
					//すでに小数点が有る場合、"."削除
					workString = workString.Replace( ".", "" );
				}
				workString = workString + ".";

				if( workString == "." || workString == "-" || workString == "-." ) {//数字無し
				} else {//数字が有る場合
					decimal decimalTemp = decimal.Parse( workString );
					//大きい場合、戻す
					if( decimalTemp > _keyDisplay2.UpperLimit ) workString = _keyDisplay2.Text;
					//小さい場合、戻す
					if( decimalTemp < _keyDisplay2.LowerLimit ) workString = _keyDisplay2.Text;
				}
				_keyDisplay2.Text = workString;
				//キャレット位置：設定
				setKeyDisplayCursolPos( _keyDisplay2.Text.Length );
			}
		}
        /// <summary>
        /// TEXTが全選択状態か判断
        /// </summary>
        /// <param name="caretMove"></param>
        /// <returns></returns>
        private bool IsAllSelect(bool caretMove = false)
        {
            int selLen = _keyDisplay2.SelectionLength;
            //表示時、編集欄が全選択になるが、上下ボタンを押すと０からの加減算となってしまう対応
            int pos = _keyDisplay2.Text.Length; //キャレット：取得
            if (selLen == pos)
            {
                //全選択の場合、キャレット位置を右端に設定
                if (caretMove) setKeyDisplayCursolPos(pos);        //キャレット：位置設定
                return true;
            }
            return false;
        }
        /// <summary>
        /// "-"ボタン：クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_MinusClick( object sender, EventArgs e )
		{
            if (IsAllSelect())
            {//全選択状態は削除
            }
            else if (_keyDisplay2.Text.IndexOf('-') > -1)
            {
                //すでに存在している場合
                return;
            }
			int caretPos = getKeyDisplayCursolPos();

			keyDisplaySelDel();//選択文字列削除
			string orgString = this._keyDisplay2.Text;           //全体値
			orgString = "-" + orgString;
			_keyDisplay2.Text = orgString;
			if( _inputPosFree ) {
				setKeyDisplayCursolPos( caretPos + 1 );//キャレット：元の位置+1
			} else {
				setKeyDisplayCursolPos( _keyDisplay2.Text.Length );//キャレット：右端
			}
		}
		/// <summary>
		/// "+"ボタン：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>

		private void button_PlusClick( object sender, EventArgs e )
		{
			int minusIndex = _keyDisplay2.Text.IndexOf( '-' );

			int caretPos = getKeyDisplayCursolPos();

			keyDisplaySelDel();//選択文字列削除

			if( _keyDisplay2.Text == "" ) return;//無い場合、処理中止
			if( minusIndex < 0 ) {//すでに"-"が無い場合
				return;
			}
			//削除
			_keyDisplay2.Text = _keyDisplay2.Text.Remove( minusIndex, 1 );

			if( _inputPosFree ) {//入力位置：自由
				setKeyDisplayCursolPos( caretPos - 1 );//キャレット：元の位置-1
			} else {//入力位置：右端
				setKeyDisplayCursolPos( _keyDisplay2.Text.Length );//キャレット：右端
			}
		}
		#endregion
		#region <▲▼上下三角ボタン：クリック>
		public string stringNcImmediateUpDn = "1";
		/// <summary>
		/// ▲上三角をクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonArrowkeyUp_Click( object sender, EventArgs e )
		{
			//タイマースレッド動作時、有効でないスレッド間の操作エラー
			//が発生するので、delegate匿名メソッドで対応
			//(※通常別スレッドからFormのコントロールのプロパティ等を操作出来ない)
			if( ( this.IsDisposed == true || this.Disposing == true ) ) return;
            retInvoke = BeginInvoke( (MethodInvoker)delegate ()
			{
                int selLen = _keyDisplay2.SelectionLength;
                //表示時、編集欄が全選択になるが、上下ボタンを押すと０からの加減算となってしまう対応
                int pos = _keyDisplay2.Text.Length; //キャレット：取得
                if (selLen == pos)
                {
                    //不具合対応：シュミレータ/実機：▲を1回押しても、全選択が解除されないので、一度タイトルをクリックしてフォーカスを与える。：柏原
                    //buttonEx_Title.PerformClick();

                    //全選択の場合、キャレット位置を右端に設定
                    setKeyDisplayCursolPos(pos); //キャレット：位置設定

  /*                  if (pos < 0) pos = 0;
                    Focus();//ここで１回 Focus()を呼ぶと、このメソッドを２回コールしないで済む
                    _keyDisplay2.Focus();

                    _keyDisplay2.SelectionStart = pos;
                    _keyDisplay2.SelectionLength = 0;
                    _keyDisplay2.Select(pos, 0);

                    Focus();
                    _keyDisplay2.Focus();
                    _keyDisplay2.Refresh();*/
                }
                if ( _ncImmediateFlg ) {
					//IP、CAP、OVERRIDE用
					_notifyReturnArrowUp.Invoke();                  //呼び出し先に通知 ---> NCにデータセット
					this._keyDisplay.Text = stringNcImmediateUpDn;  //書式をセット
					this._keyDisplay2.Text = _keyDisplay.Text;      //値をセット
                    //キャレット位置を右端に設定
                    setKeyDisplayCursolPos(pos);                    //キャレット：位置設定
                    return;
				}
				//入力値をセット
				string orgString = this._keyDisplay2.Text;
				int orgStringLen = orgString.Length;
				int caretPos = getKeyDisplayCursolPos();//左端=0からのキャレット位置
				int minusIndex = 0;
				decimal decimalstring = 0;
				bool orgStringSpace = false;
				if( orgString == "" ) orgStringSpace = true;
				//桁数：新無制限：キャレット位置の桁数値の加算
				if( !getCaretPosUnitVal( true, orgString, ref caretPos, ref minusIndex, ref decimalstring ) ) return;
				//上限値チェック
				if( decimalstring > _keyDisplay2.UpperLimit ) {//少ない場合、処理中止
					if( _spaceZeroPlace ) return;//空白0埋めはエラーにします。
				}
				//数値から数字に戻す                    
				orgString = decimalstring.ToString();//orgString=""でも"-1"や"1"は返ってくる
				if( _realValueEdit ) {
					//数量位置決め設定画面等＝true
					//実数
					this._keyDisplay2.Text = orgString;  //値をセット
				} else {
					//固定長：整数
					this._keyDisplay.Text = orgString;  //書式をセット
					this._keyDisplay2.Text = _keyDisplay.Text;  //値をセット
				}
				//orgStringが""なのに、ここでは"001"等の数字が入っている場合の対応
				if( orgStringSpace ) {
					caretPos = _keyDisplay2.Text.Length;
				}
				setKeyDisplayCursolPos( caretPos ); //キャレット位置設定
			} );
		}
		/// <summary>
		/// 上限値チェックと文字列セット
		/// </summary>
		/// <param name="intString"></param>
		/// <param name="orgString"></param>
		/// <returns></returns>
		bool upperLimitAndSet( decimal intString, ref string orgString )
		{
			//上限値チェック
			if( intString > _keyDisplay2.UpperLimit ) {//多い場合、処理中止
				return false;
			}
			//数値から数字に戻す                    
			orgString = intString.ToString();
			return true;
		}
		/// <summary>
		/// ▼下三角をクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonArrowkeyDn_Click( object sender, EventArgs e )
		{
			//タイマースレッド動作時、有効でないスレッド間の操作エラー
			//が発生するので、delegate匿名メソッドで対応
			//(※通常別スレッドからFormのコントロールのプロパティ等を操作出来ない)
			if( ( this.IsDisposed == true || this.Disposing == true ) ) return; retInvoke = BeginInvoke( (MethodInvoker)delegate ()
			{
                int selLen = _keyDisplay2.SelectionLength;
                //表示時、編集欄が全選択になるが、上下ボタンを押すと０からの加減算となってしまう対応
                int pos = _keyDisplay2.Text.Length; //キャレット：取得
                if (selLen == pos)
                {
                    //全選択の場合、キャレット位置を右端に設定
                    setKeyDisplayCursolPos(pos);        //キャレット：位置設定
                }
				if( _ncImmediateFlg ) {
					//IP、CAP、OVERRIDE用
					_notifyReturnArrowDn.Invoke();                  //呼び出し先に通知 ---> NCにデータセット
					this._keyDisplay.Text = stringNcImmediateUpDn;  //書式をセット
					this._keyDisplay2.Text = _keyDisplay.Text;      //値をセット
                    //キャレット位置を右端に設定
                    setKeyDisplayCursolPos(pos);                    //キャレット：位置設定
                    return;
				}
				//入力値をセット
				int caretPos = getKeyDisplayCursolPos();//左端=0からのキャレット位置
				string orgString = this._keyDisplay2.Text;
				int orgStringLen = orgString.Length;
				int minusIndex = 0;
				decimal decimalstring = 0;
				bool orgStringSpace = false;
				if( orgString == "" ) orgStringSpace = true;

				//桁数：新無制限：キャレット位置の桁数値の減算
				if( !getCaretPosUnitVal( false, orgString, ref caretPos, ref minusIndex, ref decimalstring ) ) return;
				//下限値チェック
				if( decimalstring < _keyDisplay2.LowerLimit ) {//少ない場合、処理中止
					if( _spaceZeroPlace ) return;//空白0埋めはエラーにします。
				}
				//数値から数字に戻す                    
				orgString = decimalstring.ToString();
				//break;
				//}
				if( _realValueEdit ) {
					//数量位置決め設定画面等＝true
					//実数
					this._keyDisplay2.Text = orgString;  //値をセット
				} else {
					//固定長：整数
					this._keyDisplay.Text = orgString;  //書式をセット
					this._keyDisplay2.Text = _keyDisplay.Text;  //値をセット
				}
				//orgStringが""なのに、ここでは"001"等の数字が入っている場合の対応
				if( orgStringSpace ) {
					caretPos = _keyDisplay2.Text.Length;
				}
				setKeyDisplayCursolPos( caretPos );   //キャレット位置設定
				if( _ncImmediateFlg ) {
					stringNcImmediateUpDn = orgString;
					//IP、CAP、OVERRIDE用
					_notifyReturnArrowDn.Invoke();   //呼び出し先に通知 ---> NCにデータセット
				}

			} );
		}
		/// <summary>
		/// 下限値チェックと文字列セット
		/// </summary>
		/// <param name="intString"></param>
		/// <param name="orgString"></param>
		/// <returns></returns>
		bool lowerLimitAndSet( decimal intString, ref string orgString )
		{
			//下限値チェック
			if( intString < _keyDisplay2.LowerLimit ) {//多い場合、処理中止
				return false;
			}
			//数値から数字に戻す                    
			orgString = intString.ToString();
			return true;
		}
        private int _mabikiVal = 1;
        /// <summary>
        /// キャレット位置の加算/減算値を取得
        /// </summary>
        /// <param name="boolPlusFlg">false=減算、true=加算</param>
        /// <param name="orgString">編集文字列</param>
        /// <param name="caretPos">キャレット位置：左端=0、右端=orgString長</param>
        /// <param name="minusPos">-1="-"無し、0から"-"の見つかった位置</param>
        /// <param name="decimalstring">キャレット位置の桁で加算/減算した結果値</param>
        /// <returns></returns>
        private bool getCaretPosUnitVal( bool boolPlusFlg, string orgString, ref int caretPos, ref int minusIndex, ref decimal decimalstring )
		{
			//数字として有効な符号等を数字に変換
			//[][.][-][-.][.n][-.n]
			if( orgString == "" ) {//全てが点：ex.[]
				orgString = "0"; caretPos = orgString.Length;
			} else if( orgString == "." ) {//全てが点：ex.[.]
				orgString = "0.0"; caretPos = orgString.Length;
			} else if( orgString == "-" ) {//全てがマイナス：ex.[-]
				orgString = "0"; caretPos = orgString.Length;
			} else if( orgString == "-." ) {//全てがマイナスと点：ex.[-.]
				orgString = "0.0"; caretPos = orgString.Length;
			} else if( orgString == "-." ) {//全てがマイナスと点：ex.[-.]
				orgString = "0.0"; caretPos = orgString.Length;
			} else if( orgString.IndexOf( "." ) == 0 ) {//先頭が点：ex.[.895]
				orgString = "0" + orgString; caretPos = orgString.Length;
			} else if( orgString.IndexOf( "-" ) == 0 && orgString.IndexOf( "." ) == 1 ) {//先頭がマイナスと点：ex.[-.895]
				orgString = orgString.Insert( 1, "0" ); caretPos = orgString.Length;
			} else if( orgString.Length == orgString.IndexOf( "." ) + 1 ) {//最後が"."
																		   //ex.[356.]
				orgString = orgString + "0";
			}

			int inStringLen = 0;            //編集文字列長
			int inStringLenDotless = 0;     //編集文字列長："."はノーカウント
			int inStringLenMinusless = 0;   // 編集文字列長："-"はノーカウント
			bool minusFlg = false;          //false=プラス値、true=マイナス値
			bool realNumFlg = false;        //false = 整数値、true = 実数値
			string upperString = "";        //整数部文字列
			string lowerString = "";        //小数部文字列
			int dotPos = -1;
			// 数字解析：処理に使用する数字の情報を取得
			analyzeStringVal(
				orgString,
				ref inStringLen,
				ref inStringLenDotless,
				ref inStringLenMinusless,
				ref minusFlg,
				ref realNumFlg,
				ref upperString,
				ref lowerString,
				ref dotPos,
				ref minusIndex
		   );
			//加算/減算初期値
			decimal decimalVal = 1;
			//キャレット位置のチェック
			if( caretPos == 0 ) return false;//失敗
			if( caretPos <= 1 && minusFlg ) return false;//失敗
														 //整数/実数
			if( realNumFlg == false ) {
				//整数
				string tmpString = "";
				//編集文字列長から最大値を出しキャレット位置の加算/減算値をdecimalValとし取得
				tmpString = tmpString.PadRight( inStringLenMinusless - 1 );
				tmpString = "1" + tmpString.Replace( " ", "0" );
				decimalVal = decimal.Parse( tmpString );
				for( int count = 1 ; count < inStringLenMinusless ; count++ ) {
					if( caretPos == count ) break;
					decimalVal = decimalVal / 10;
				}
			} else {
				//実数
				if( dotPos + 1 == caretPos ) {//小数左：
											  //1倍
				} else if( dotPos + 1 >= caretPos ) {//整数部
					string tmpString = "";
					//編集文字列長から最大値を出しキャレット位置の加算/減算値をdecimalValとし取得
					tmpString = tmpString.PadRight( upperString.Length );
					tmpString = "1" + tmpString.Replace( " ", "0" );
					decimalVal = decimal.Parse( tmpString );
					//桁数数分
					for( int count = 1 ; count < inStringLenMinusless ; count++ ) {
						decimalVal = decimalVal / 10;
						if( caretPos == count ) break;
					}
				} else {//1から
					for( int count = 0 ; count < lowerString.Length ; count++ ) {
						decimalVal = decimalVal / 10;
						if( caretPos - ( upperString.Length + 2 ) == count ) break;
					}
				}
			}
			//文字列をdecimal値に変換
			decimalstring = decimal.Parse( orgString );
            if (boolPlusFlg)
            {//加算チェック
                if (decimalstring+1 > _keyDisplay2.UpperLimit) decimalstring = _keyDisplay2.UpperLimit;
            }else{//減算チェック
                if (decimalstring-1 < _keyDisplay2.LowerLimit) decimalstring = _keyDisplay2.LowerLimit;
            }
            /*{//加算チェック
                if (decimalstring + 1 > _keyDisplay2.UpperLimit) return false;
            }else{//減算チェック
                if (decimalstring - 1 < _keyDisplay2.LowerLimit) return false;
            }*/           //値の加算/減算
            if ( boolPlusFlg ) decimalstring += (decimalVal * _mabikiVal);
			else               decimalstring -= (decimalVal * _mabikiVal);

            //キャレット位置調整
            switch ( _keyDisplay.FormatType ) {
				//固定長：ゼロ埋め
				case NumericTextBox.FormatTypes.IntegerZeroPlace2:
				case NumericTextBox.FormatTypes.IntegerZeroPlace3:
				case NumericTextBox.FormatTypes.SignZeroDecimalUpper3Lower3:
					break;//処理無し
				default:
					//可変長：桁数が変わった場合、キャレット位置をずらす。
					//フォーマットを合わせる
					_keyDisplay.Text = _keyDisplay2.Text;
					string orgStringFormat = _keyDisplay.Text;
					_keyDisplay.Text = decimalstring.ToString();
					string decimalstringFormat = _keyDisplay.Text;
					//キャレット位置：加算/減算
					if( decimalstringFormat.Length > orgStringFormat.Length ) caretPos++;
					if( decimalstringFormat.Length < orgStringFormat.Length ) caretPos--;
					break;
			}
			return true;//成功
		}
		#endregion
		#region <▲▼上下三角ボタン：タイマーによる長押し>
		/// <summary>
		/// タイマーによる▲▼ボタン長押し時、数字を徐々に加速
		/// </summary>
		private System.Threading.Timer _TimerUpDn = null;
		private const int constWaitTime = 250;  //クリックと長押しの境界値	：0-499ms＝クリック、499ms以上＝長押し
		private const int constTimerSpeed = 200;//タイマ間隔時間ms			：これを小さくすると早くなる
		private const int constMinusCount = 5;  //タイマ減算値				：これを大きくすると早くなる
		private int timerSpeed = constTimerSpeed;//最初はゆっくり
		private int UpDnButtonFlg = 0;          //0=UPボタン 1=DOWNボタン
		/// <summary>
		/// ▲▼ボタン：タイマ・ファイア
		/// </summary>
		/// <param name="obj"></param>
		public void fireUpDn( Object obj )
		{  //実際にボタンが押されているかチェック ※ボタンが離されたのにイベントが出ない不具合対応
			if( ( Control.MouseButtons & MouseButtons.Left ) == MouseButtons.Left ) { } else if( ( Control.MouseButtons & MouseButtons.Right ) == MouseButtons.Right ) { } else if( ( Control.MouseButtons & MouseButtons.Middle ) == MouseButtons.Middle ) { } else {
				//押されていない場合、タイマーだけ動作しているので、処理中止
				timerSpeed = constTimerSpeed;
				_TimerUpDn.Change( System.Threading.Timeout.Infinite, timerSpeed );//タイマ待機
				return;
			}

			//▲▼ボタン：値の加算/減算
			if( UpDnButtonFlg == 0 ) { buttonArrowkeyUp_Click( null, null ); }//上
			else { buttonArrowkeyDn_Click( null, null ); }//下

			timerSpeed -= constMinusCount;                //タイマ減算
			if( timerSpeed < 100 ) timerSpeed = 100;      //タイマ減算値が100以下ではNC値のセットで、遅れがでるので100を限度とする。

            if (timerSpeed == 100)
            {//timerSpeedを100以下にできないので、1ずつ+-している所を50で+-し加速してるように見せる。

                _mabikiVal = 1;
                //_mabikiVal = (int) Math.Pow(2, _mabikiVal);
                //_mabikiVal += _mabikiVal* _mabikiVal;
            }
            else {
                _mabikiVal = 1;
            }

			_TimerUpDn.Change( timerSpeed, timerSpeed );  //タイマ設定変更
			//速度変化は直線的でなく指数的に変更するため dueTimeとperiodを同じ値にします。 
		}
		/// <summary>
		/// ▲ボタン：押された
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonArrowkeyUp_MouseDn( object sender, MouseEventArgs e )
		{
            buttonEx_Up.ForeColor = FileUIStyleTable.EnabledForeColor;//20170201HachinoADD//ボタン色反転ON
            UpDnButtonFlg = 0;
			timerSpeed = constTimerSpeed;
			_TimerUpDn.Change( constWaitTime, timerSpeed );//500ms後タイマをNms間隔で起動
		}
		/// <summary>
		/// ▲ボタン：離された
		/// </summary>　
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonArrowkeyUp_MouseUp( object sender, MouseEventArgs e )
		{
			buttonEx_Up.ForeColor = FileUIStyleTable.DefaultForeColor;//20170201HachinoADD//ボタン色反転OFF
			timerSpeed = constTimerSpeed;
			_TimerUpDn.Change( System.Threading.Timeout.Infinite, timerSpeed );//タイマ待機
            _mabikiVal = 1;
        }
		/// <summary>
		/// ▼ボタン：押された
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonArrowkeyDn_MouseDn( object sender, MouseEventArgs e )
		{
			buttonEx_Dn.ForeColor = FileUIStyleTable.EnabledForeColor;//20170201HachinoADD//ボタン色反転ON
			UpDnButtonFlg = 1;
			timerSpeed = constTimerSpeed;
			_TimerUpDn.Change( constWaitTime, timerSpeed ); //500ms後タイマをNms間隔で起動
		}
		/// <summary>
		/// ▼ボタン：離された
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonArrowkeyDn_MouseUp( object sender, MouseEventArgs e )
		{
			buttonEx_Dn.ForeColor = FileUIStyleTable.DefaultForeColor;//20170201HachinoADD//ボタン色反転OFF
			timerSpeed = constTimerSpeed;
			_TimerUpDn.Change( System.Threading.Timeout.Infinite, timerSpeed );//タイマ待機
            _mabikiVal = 1;
        }
		#endregion
		#region <タイトル無しウインド移動>
		//マウスクリック位置記憶
		/// <summary>
		/// タイトル・マウス：ダウン
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonEx_Title_MouseDown( object sender, MouseEventArgs e )
		{
			TenKeyMouseDown( sender, e );
		}
		/// <summary>
		/// タイトル・マウス：移動
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonEx_Title_MouseMove( object sender, MouseEventArgs e )
		{
			TenKeyMouseMove( sender, e );
		}
		private Point mousePoint;
        /// <summary>
		/// フォーム・マウス：ダウン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TenKeyDialog_MouseDown( object sender, MouseEventArgs e )
		{
			TenKeyMouseDown( sender, e );
		}
		/// <summary>
		/// フォーム・マウス：移動
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TenKeyDialog_MouseMove( object sender, MouseEventArgs e )
		{
			TenKeyMouseMove( sender, e );
		}
		/// <summary>
		/// マウス：ダウン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TenKeyMouseDown( object sender, MouseEventArgs e )
		{
			if( ( e.Button & MouseButtons.Left ) == MouseButtons.Left ) {
				mousePoint = new Point( e.X, e.Y );//位置を記憶
			}
		}
		/// <summary>
		/// マウス：移動
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TenKeyMouseMove( object sender, MouseEventArgs e )
		{
			if( ( e.Button & MouseButtons.Left ) == MouseButtons.Left ) {
				this.Left += e.X - mousePoint.X;
				this.Top += e.Y - mousePoint.Y;
			}
		}
        #endregion


    }
}
