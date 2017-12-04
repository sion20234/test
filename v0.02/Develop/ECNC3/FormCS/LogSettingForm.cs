///////////////////////////////////////////////////////////////////////////////////////////
//
// (1) システム名称 : ELENIX ECNCⅢ
// (2) ファイル名   : LogSettingForm.cs
// (3) 概要         : 加工ログ表示設定画面
// (4) 作成日       : 2015.06.01
// (5) 作成者       : 八野
// (6) 特記事項     : VisualStudio2015 C#
// (7) 更新履歴     : ログ設定に追加：2017-06-15：柏原
//
///////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Windows.Forms;
using ECNC3.Models;			//FileSettings用
using ECNC3.Models.McIf;	//McDatMacroManage
using ECNC3.Enumeration;
using System.Xml;           //XML
using System.IO;            //テキストR/W
using System.Text;          //Encoding
using System.Runtime.InteropServices;   //DLL Import

namespace ECNC3.Views
{
    public partial class LogSettingForm : ECNC3Form
    {
		#region<初期化>
		/// <summary>ポップアップテンキー</summary>
		private TenKeyDialog _popupTenkey = new TenKeyDialog( "", 0, 0, 0 );    //初回インスタンスを作っておく

		/// <summary>
		/// フォーム　コンストラクタ
		/// </summary>
		public LogSettingForm()
        {
            InitializeComponent();
        }
		/// <summary>
		/// フォームロード時
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LogSettingForm_Load( object sender, EventArgs e )
		{
            ProgressValue = 0;
            // 元のカーソルを保持
            Cursor preCursor = Cursor.Current;
			// カーソルを待機カーソルに変更
			Cursor.Current = Cursors.WaitCursor;
            ProgressValue = 2;

            LogSettingReadWrite( false );			//LogSetting：ファイル保存：読込み
			CncSysReadWriteLogSettingFile( false);	//ログ設定ファイル：全読込み
            ProgressValue = 5;
            //グラフ
            textBoxEx_XTimeAxisMax.Text = _xTimeAxisMax.ToString();			//X軸時間軸最大
			textBoxEx_YProsessDepthMax.Text = _yProsessDepthMax.ToString();	//Y軸加工深さ最大
			textBoxEx_SampLingCycle.Text = _sampLingCycle.ToString();         //サンプリング周期
            ProgressValue = 8;
            //ログタイトル番号設定
            textBoxEx_LogTitleNumSet_1.Text = _title1.ToString();
			textBoxEx_LogTitleNumSet_2.Text = _title2.ToString();
			textBoxEx_LogTitleNumSet_3.Text = _title3.ToString();
			textBoxEx_LogTitleNumSet_4.Text = _title4.ToString();
            ProgressValue = 10;
            //ラジオボタン値に変換
            if ( _intUpDownInverted == 0 ) {
				//上下反転：無し
				radioButton_UpsideDn_Up.Checked = true;
			} else {
				//上下反転：有り
				radioButton_UpsideDn_Dn.Checked = true;
			}
			if( _saveValid == 1 ) {
				//保存：有効
				radioButton_Save_Valid.Checked = true;
			} else {
				//保存：無効
				radioButton_Save_Invalid.Checked = true;
			}
            ProgressValue = 13;
            //NCデータ読み込み
            McDatMacroManage macroManage = new McDatMacroManage( MacroManage.ExtensionMacro );  //クラス作成
			ResultCodes ret = macroManage.Read();               //データ読込み
			if( ret == ResultCodes.FailToReadFile ) {
				macroManage.Dispose();
				macroManage = null;
				using( MessageDialog msg = new MessageDialog() ) {
					msg.Error( 5509, this );
					{
						return;//"読み込みエラー発生" "ファイルが存在しているか確認して下さい。"
					}
				}
			}
            ProgressValue = 60;

            SetDGVEx( dataGridViewEx1, "#マクロ変数", 90000 );
			for( int index = 0 ; index < 90000 ; index++ ) {
				//DGVEX のセル[10000]＝ファイルの先頭
				int indexPlus = 10000 + index;
				dataGridViewEx1.Rows[index].Cells[0].Value = "#" + indexPlus.ToString("00000");
                if (index % 3000 == 0) ProgressValue++;

            }
            ProgressValue = 90;

            SetDGVEx( dataGridViewEx2, "#ログ記録変数", 0 );
            ProgressValue = 95;

            LogVarListReadWrite( false ); //ログ記録変数：読込み
            ProgressValue = 100;

            Cursor.Current = preCursor;   //カーソルを元に戻す 
            ProgressValue = 0;
		}
		/// <summary>
		/// DGVExの設定
		/// </summary>
		/// <param name="dgvEx"></param>
		/// <param name="headerText"></param>
		/// <param name="rowCount"></param>
		private void SetDGVEx(DataGridViewEx dgvEx, string headerText,int rowCount )
		{
			//dataGridViewEx1：ヘッダ色や大きさ設定
			dgvEx.Initialize( 12.0F, 30, true );//true=編集不可
			//列ヘッダー設定
			DataGridViewColumn _numberCol = new DataGridViewColumn();
			_numberCol.CellTemplate = new DataGridViewTextBoxCell();
			_numberCol.Name = "NumberCol";
			_numberCol.HeaderText = headerText;//ヘッダーテキスト
//			_numberCol.HeaderText = "";
			_numberCol.Width = 180;
			//dgvEx：列ヘッダーをdataGridViewEx1に追加
			dgvEx.Columns.Add( _numberCol );
			//dgvEx：列ヘッダー：フォントや色設定
			dgvEx.InitCol( "NumberCol", 12.0F, DataGridViewContentAlignment.MiddleLeft, typeof( string ) );
			//dgvEx：行数設定
			dgvEx.RowCount = rowCount;
			//dgvEx：各種設定
			dgvEx.AutoSize = false;
			dgvEx.ReadOnly = true;
			//dgvEx.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
			dgvEx.MultiSelect = false;                //複数選択＝不可
			dgvEx.AllowUserToAddRows = false;         //追加不可
			dgvEx.AllowUserToDeleteRows = false;      //削除不可
                                                      //dataGridViewEx1_Enable( false )//初回セル使用不可
                                                      //dgvEx.RowHeadersVisible = true;//固定列：表示
            dgvEx.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

		}
		/// <summary>
		/// データグリッド：使用可能/不可
		/// </summary>
		/// <param name="boolVal"></param>
		private void dataGridViewEx1_Enable( bool boolVal )
		{
			if( boolVal ) {
				dataGridViewEx1.Enabled = true;
				dataGridViewEx1.CurrentCell = dataGridViewEx1[1, 0];
			} else {
				dataGridViewEx1.CurrentCell = null;
				dataGridViewEx1.ClearSelection();
				dataGridViewEx1.Enabled = false;
			}
		}
		/// <summary>
		///  LogSetting：ファイル保存：読込み/書き込み
		/// </summary>
		/// <param name="boolWrite"></param>
		private void LogSettingReadWrite( bool boolWrite )
		{
			if( boolWrite ) {
				//書き込み
				using( FilePathInfo filePathInf = new FilePathInfo() ) {
					filePathInf.Read();
					FilePathInfo.LogSetting = label_SavePath.Text;
					filePathInf.Write();
				}
			} else {
				//読込み
				label_SavePath.Text = FilePathInfo.LogSetting;
			}
		}
        #endregion
        #region <進捗状態通知処理>
        public int _progressValue = 0;
        public int ProgressValue
        {
            get
            {
                return _progressValue;
            }
            set
            {
                _progressValue = value;
                UpdateProcessing();
            }
        }
        public delegate void NotifyUpdateProcessing();
        /// <summary>進捗値変更通知</summary>
        private NotifyUpdateProcessing _notifyUpdate = null;
        /// <summary>>進捗値取得</summary>
        public NotifyUpdateProcessing NotifyUpdate
        {
            get
            {
                return _notifyUpdate;
            }
            set
            {
                _notifyUpdate = value;
            }
        }
        public void UpdateProcessing()
        {
            _notifyUpdate?.Invoke();
        }
        #endregion
        #region<終了>
        /// <summary>
        /// 閉じる　ボタン　クリック時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEx_Back_Click( object sender, EventArgs e )
		{
			LogSettingReadWrite( true );            //LogSetting：ファイル保存：書込み
			CncSysReadWriteLogSettingFile( true );  //ログ設定ファイル：全書込み
			this.Close();
		}
		#endregion
		#region<グラフ設定、ログタイトル番号設定、保存：読込み/書込み：CncSys.xmlファイル：>
		private int _xTimeAxisMax = 0;
		private int _sampLingCycle = 0;
		private int _yProsessDepthMax = 0;
		private int _intUpDownInverted = 0;
		private int _title1 = 0;
		private int _title2 = 0;
		private int _title3 = 0;
		private int _title4 = 0;
		private int _comment1 = 0;
		private int _comment2 = 0;
		private int _comment3 = 0;
		private int _comment4 = 0;
		private int _saveValid = 0;
		private int _areaRegStart = 0;
		private int _areaRegEnd = 0;
		/// <summary>
		/// グラフ設定、ログタイトル番号設定、保存、上下反転、ファイル保存：読込み/書き込み
		/// </summary>
		/// <param name="boolVal">false=読込み、true=書き込み</param>
		private void CncSysReadWriteLogSettingFile( bool boolVal )
		{
				try {
					using( FileSettings fs = new FileSettings() ) {
						if( boolVal ) {//ファイル書き込み
							fs.Read();
							//グラフ
							fs.WriteAttr( "Root/LogSettingFile/Graph", "xTimeAxisMax", _xTimeAxisMax.ToString() );
							fs.WriteAttr( "Root/LogSettingFile/Graph", "yProsessDepthMax", _yProsessDepthMax.ToString() );
							fs.WriteAttr( "Root/LogSettingFile/Graph", "sampingCycle", _sampLingCycle.ToString() );
							fs.WriteAttr( "Root/LogSettingFile/Graph", "upDownInverted", _intUpDownInverted.ToString() );
							//ログタイトル番号設定
							fs.WriteAttr( "Root/LogSettingFile/LogTitleNumSet", "title1", _title1.ToString() );
							fs.WriteAttr( "Root/LogSettingFile/LogTitleNumSet", "title2", _title2.ToString() );
							fs.WriteAttr( "Root/LogSettingFile/LogTitleNumSet", "title3", _title3.ToString() );
							fs.WriteAttr( "Root/LogSettingFile/LogTitleNumSet", "title4", _title4.ToString() );
							//ファイル保存
							fs.WriteAttr( "Root/LogSettingFile/File", "saveValid", _saveValid.ToString() );
							fs.Write();
						} else {   //ファイル読み込み
							fs.Read();
							//グラフ
							_xTimeAxisMax = fs.AttrValue( "Root/LogSettingFile/Graph", "xTimeAxisMax" );//
							_yProsessDepthMax = fs.AttrValue( "Root/LogSettingFile/Graph", "yProsessDepthMax" );//
							_sampLingCycle = fs.AttrValue( "Root/LogSettingFile/Graph", "sampingCycle" );//
							_intUpDownInverted = fs.AttrValue( "Root/LogSettingFile/Graph", "upDownInverted" );//
							//ログタイトル番号設定
							_title1 = fs.AttrValue( "Root/LogSettingFile/LogTitleNumSet", "title1" );//
							_title2 = fs.AttrValue( "Root/LogSettingFile/LogTitleNumSet", "title2" );//
							_title3 = fs.AttrValue( "Root/LogSettingFile/LogTitleNumSet", "title3" );//
							_title4 = fs.AttrValue( "Root/LogSettingFile/LogTitleNumSet", "title4" );//
							//ログコメント設定
							_comment1 = fs.AttrValue( "Root/LogSettingFile/LogComment", "Comment1" );//
							_comment2 = fs.AttrValue( "Root/LogSettingFile/LogComment", "Comment2" );//
							_comment3 = fs.AttrValue( "Root/LogSettingFile/LogComment", "Comment3" );//
							_comment4 = fs.AttrValue( "Root/LogSettingFile/LogComment", "Comment4" );//
							//ファイル保存
							_saveValid = fs.AttrValue( "Root/LogSettingFile/File", "saveValid" );//
						}
					}
				} catch( Exception exc ) {
					//例外処理
					MessageBox.Show( exc.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error );
				}
		}
		/// <summary>
		/// CncSys.xmlのファイル：上下反転：読込み/書き込み
		/// </summary>
		/// <param name="boolVal">false=読込み、true=書き込み</param>
		private void CncSysReadWriteUpDownInverted( bool boolVal )
		{
			try {
				using( FileSettings fs = new FileSettings() ) {
					if( boolVal ) {   //ファイル書き込み
						fs.Read();
						fs.WriteAttr( "Root/LogSettingFile/Graph", "upDownInverted", _intUpDownInverted.ToString() );
						fs.Write();
					} else {   //ファイル読み込み
						fs.Read();
						_intUpDownInverted = fs.AttrValue( "Root/LogSettingFile/Graph", "upDownInverted" );//
					}
				}
			} catch( Exception exc ) {
				//例外処理
				MessageBox.Show( exc.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error );
			}
		}
		/// <summary>
		/// CncSys.xmlのファイル：ファイル保存：読込み/書き込み
		/// </summary>
		/// <param name="boolVal">false=読込み、true=書き込み</param>
		private void CncSysReadWriteFileSave( bool boolVal )
		{
			try {
				using( FileSettings fs = new FileSettings() ) {
					if( boolVal ) {   //ファイル書き込み
						fs.Read();
						fs.WriteAttr( "Root/LogSettingFile/File", "saveValid", _saveValid.ToString() );
						fs.Write();
					} else {   //ファイル読み込み
						fs.Read();
						_saveValid = fs.AttrValue( "Root/LogSettingFile/File", "saveValid" );//
					}
				}
			} catch( Exception exc ) {
				//例外処理
				MessageBox.Show( exc.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error );
			}
		}
		#endregion
		#region<フォルダ指定>
		/// <summary>
		/// フォルダ指定：ボタン：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonEx_FolderAssign_Click( object sender, EventArgs e )
		{
			//ファイルフォーム：ツリービュー＋リストビュー
			FileForm file = new FileForm( FileFormMode.AllPath, "" );
			file.ShowDialog();
			if( file._returnPath != "") {
				label_SavePath.Text = file._returnPath;
			}
			file.Dispose();
		}
		/// <summary>
		/// 保存先初期化：ボタン：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonEx_SetInitFolder_Click( object sender, EventArgs e )
		{
			//保存先を初期設定に戻します。&#xD;&#xA;よろしいですか？" 
			using( MessageDialog msg = new MessageDialog() ) {
				if( false == msg.Question( 5505, this ) ) {
					return;
				}
			}
			//初期値：設定
			label_SavePath.Text = @"D:\User\ED_LOG";
		}
		/// <summary>
		/// 保存：ラジオボタン：有効
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButton_Save_Valid_CheckedChanged( object sender, EventArgs e )
		{
			bool val = ( (RadioButton)( sender ) ).Checked;
			if( val == false ) return;

			_saveValid = 1;
			CncSysReadWriteFileSave( true );
		}
		/// <summary>
		/// 保存：ラジオボタン：無効
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButton_Save_Invalid_CheckedChanged( object sender, EventArgs e )
		{
			bool val = ( (RadioButton)( sender ) ).Checked;
			if( val == false ) return;

			_saveValid = 0;
			CncSysReadWriteFileSave( true );
		}

		/// <summary>
		/// 上下反転：ラジオボタン：上
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButton_UpsideDn_Up_CheckedChanged( object sender, EventArgs e )
		{
			bool val = ( (RadioButton)( sender ) ).Checked;
			if( val == false ) return;

			_intUpDownInverted = 0;
			CncSysReadWriteUpDownInverted( true );
		}
		/// <summary>
		/// 上下反転：ラジオボタン：下
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButton_UpsideDn_Dn_CheckedChanged( object sender, EventArgs e )
		{
			bool val = ( (RadioButton)( sender ) ).Checked;
			if( val == false ) return;

			_intUpDownInverted = 1;
			CncSysReadWriteUpDownInverted( true );
		}
		#endregion
		#region<ポップアップ・テンキーで各値を編集>
		LogSettingEditTypes _logSettingEditTypes;//ログ設定：編集タイプ
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
		private bool popupTenkeyOnFind( object val, string popupTitle,int intKeta, int intMin,int intMax,string modeAndPos="")
		{
			if( _popupTenkey != null ) {
				_popupTenkey.Close();   //画面を閉じる
				_popupTenkey = null;    //null初期化
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
			switch( intKeta )
			{
				case 4: formatType = NumericTextBox.FormatTypes.Integer4; break;
				case 5: formatType = NumericTextBox.FormatTypes.Integer5; break;
			}
			//ポップアップTenKey表示
			_popupTenkey = new TenKeyDialog( changeVal, formatType, lowerLimitDec, upperLimitDec, true ,false,false,modeAndPos);
			_popupTenkey.NotifyReturnOk = popupTenkey_OnNotifyReturnOk; //イベント通知：OK
			_popupTenkey.Text = popupTitle;                             //テンキータイトル表示
			_popupTenkey.ShowDialog( this );                            //画面を開く
			return true;
		}
		/// <summary>
		/// ポップアップテンキー：「OK」ボタンを押した時、入力値をこのクラスに反映
		/// </summary>
		private void popupTenkey_OnNotifyReturnOk()
		{
			string retVal = _popupTenkey._tenkeyValReturn;//ポップアップテンキーで編集された値
			//各コントロールごとに値をセット
			switch( _logSettingEditTypes )
			{
				case LogSettingEditTypes.xTimeAxisMax:		textBoxEx_XTimeAxisMax.Text		= retVal; _xTimeAxisMax = int.Parse( retVal ); break;
				case LogSettingEditTypes.yProsessDepthMax:	textBoxEx_YProsessDepthMax.Text = retVal; _yProsessDepthMax = int.Parse( retVal ); break; ;
				case LogSettingEditTypes.sampLingCycle:		textBoxEx_SampLingCycle.Text	= retVal; _sampLingCycle = int.Parse( retVal ); break; ;
				case LogSettingEditTypes.title1:			textBoxEx_LogTitleNumSet_1.Text = retVal; _title1 = int.Parse( retVal ); break;
				case LogSettingEditTypes.title2:			textBoxEx_LogTitleNumSet_2.Text = retVal; _title2 = int.Parse( retVal ); break;
				case LogSettingEditTypes.title3:			textBoxEx_LogTitleNumSet_3.Text = retVal; _title3 = int.Parse( retVal ); break;
				case LogSettingEditTypes.title4:			textBoxEx_LogTitleNumSet_4.Text = retVal; _title4 = int.Parse( retVal ); break;
				case LogSettingEditTypes.AreaRegStrat:		textBoxEx_AriaRegStart.Text		= retVal; _areaRegStart = int.Parse( retVal ); break;
				case LogSettingEditTypes.AreaRegEnd:		textBoxEx_AriaRegEnd.Text		= retVal; _areaRegEnd = int.Parse( retVal ); break;
				default: return;
			}
		}
		/// <summary>
		/// ログ設定：編集タイプ
		/// </summary>
		private enum LogSettingEditTypes
		{
			xTimeAxisMax,
			yProsessDepthMax,
			sampLingCycle,
			title1,
			title2,
			title3,
			title4,
			AreaRegStrat,
			AreaRegEnd
		}
		/// <summary>
		/// X軸:時間軸最大：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBoxEx_XTimeAxisMax_MouseClick( object sender, MouseEventArgs e )
		{
			_logSettingEditTypes = LogSettingEditTypes.xTimeAxisMax;
			string objectString = ( (TextBox)sender ).Text;
			popupTenkeyOnFind( objectString, "X軸:時間軸最大", 4, 10, 1800 , "LogSetting_Graph_XAxis" );
		}
		/// <summary>
		/// Y軸:加工深度最大：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBoxEx_YProsessDepthMax_MouseClick( object sender, MouseEventArgs e )
		{
			_logSettingEditTypes = LogSettingEditTypes.yProsessDepthMax;
			string objectString = ( (TextBox)sender ).Text;
			popupTenkeyOnFind( objectString, "Y軸:加工深度最大", 4, 10, 500, "LogSetting_Graph_YAxis" );
		}
		/// <summary>
		/// サンプリング周期 ：クリック     
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBoxEx_SampingCycle_MouseClick( object sender, MouseEventArgs e )
		{
			_logSettingEditTypes = LogSettingEditTypes.sampLingCycle;
			string objectString = ( (TextBox)sender ).Text;
			popupTenkeyOnFind( objectString, "サンプリング周期", 4, 1, 100, "LogSetting_Graph_Sampling" );
		}
        static int _intTitleNumMin = 10000;
        static int _intTitleNumMax = 99999;
        static int _intTitleLength = 5;
        /// <summary>
        /// ログタイトル番号設定：タイトル1 ：クリック 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxEx_LogTitleNumSet_1_MouseClick( object sender, MouseEventArgs e )
		{
			_logSettingEditTypes = LogSettingEditTypes.title1;
			string objectString = ( (TextBox)sender ).Text;
			popupTenkeyOnFind( objectString, "タイトル1", _intTitleLength, _intTitleNumMin, _intTitleNumMax, "LogSetting_LogTitle_1" );
		}
		/// <summary>
		/// ログタイトル番号設定：タイトル2 ：クリック 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBoxEx_LogTitleNumSet_2_MouseClick( object sender, MouseEventArgs e )
		{
			_logSettingEditTypes = LogSettingEditTypes.title2;
			string objectString = ( (TextBox)sender ).Text;
			popupTenkeyOnFind( objectString, "タイトル2", _intTitleLength, _intTitleNumMin, _intTitleNumMax, "LogSetting_LogTitle_2" );
		}
		/// <summary>
		/// ログタイトル番号設定：タイトル3 ：クリック 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBoxEx_LogTitleNumSet_3_MouseClick( object sender, MouseEventArgs e )
		{
			_logSettingEditTypes = LogSettingEditTypes.title3;
			string objectString = ( (TextBox)sender ).Text;
			popupTenkeyOnFind( objectString, "タイトル3", _intTitleLength, _intTitleNumMin, _intTitleNumMax, "LogSetting_LogTitle_3" );
		}
		/// <summary>
		/// ログタイトル番号設定：タイトル4 ：クリック 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBoxEx_LogTitleNumSet_4_MouseClick( object sender, MouseEventArgs e )
		{
			_logSettingEditTypes = LogSettingEditTypes.title4;
			string objectString = ( (TextBox)sender ).Text;
			popupTenkeyOnFind( objectString, "タイトル4", _intTitleLength, _intTitleNumMin, _intTitleNumMax, "LogSetting_LogTitle_4" );
		}
		#endregion
		#region<ログ変数設定：読込み/書込み、dataGridViewEx1とdataGridViewEx2の設定と読込み>
		/// <summary>
		/// ログ変数設定：読込み/書込み
		/// </summary>
		/// <param name="boolValue"></param>
		private void LogVarListReadWrite( bool boolValue )
		{
			if( boolValue ) {
				LogVarListWrite();
			} else {
				LogVarListRead();
			}
		}
		/// <summary>
		///ログ変数設定： Log_VAR_LISTファイル読込みとDGVEXの設定
		/// </summary>
		private void LogVarListRead()
		{
			string filePath = FilePathInfo.MasterData + "Log_VAR_LIST.inf";
			string stringContent = "";
			System.Collections.ArrayList al = new System.Collections.ArrayList();
			//読込み
			using( StreamReader sr = new StreamReader( filePath, Encoding.GetEncoding( "UTF-8" ) ) ) {
				while( ( stringContent = sr.ReadLine() ) != null ) {
					//ログ記録変数：行追加
					LogRecValRowAdd( stringContent );
				}
			}
		}
		/// <summary>
		/// ログ変数設定：Log_VAR_LISTファイル書込み
		/// </summary>
		private void LogVarListWrite()
		{
			string filePath = FilePathInfo.MasterData + "Log_VAR_LIST.inf";
			string text = "";
			//DGVExの各行からログ記録変数を取得しTEXT用フォーマットを作成
			foreach( DataGridViewRow rows in dataGridViewEx2.Rows ) {
				text += rows.Cells[0].Value;//ログ記録変数　#xxxxx値
				text += "\r\n";             //改行
			}
			//書込み
			StreamWriter sw = new StreamWriter( filePath, false, Encoding.GetEncoding( "UTF-8" ) );
			sw.Write( text );
			sw.Close();
		}
		/// <summary>
		/// ログ変数設定：「▶」ボタン：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonEx_Data_Move_Click( object sender, EventArgs e )
		{
			if( alreadyNumberCheck( dataGridViewEx1.CurrentCell.Value ) ) {
				//行追加
				LogRecValRowAdd( dataGridViewEx1.SelectedCells[0].Value );
			}
		}
		/// <summary>
		/// ログ変数設定：行削除：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonEx_LogRec_Delete_Click( object sender, EventArgs e )
		{
			int rowCount = dataGridViewEx2.RowCount;
			if( rowCount < 1 ) return;
			int index = dataGridViewEx2.CurrentRow.Index;
			//選択されたログ記録変数を削除します。&#xD;&#xA;&#xD;&#xA;よろしいですか？ 
			using( MessageDialog msg = new MessageDialog() ) {
				if( false == msg.Question( 5506, this ) ) {
					return;
				}
			}
			dataGridViewEx2.Rows.RemoveAt( index );
		}
		/// <summary>
		/// ログ変数設定：登録ボタン：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonEx_LogRec_Regist_Click( object sender, EventArgs e )
		{
			//「LOG_VAR_LIST.inf」ファイルに保存
			LogVarListReadWrite( true );            //ログ記録変数：書込み
		}
		/// <summary>
		/// ログ変数設定：追加ボタン：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonEx_MacroVal_Add_Click( object sender, EventArgs e )
		{
			object objectAriaReg;
			if( int.Parse( textBoxEx_AriaRegStart.Text ) > int.Parse( textBoxEx_AriaRegEnd.Text ) ) {
				//開始が大きい場合、逆にする ※ECNC3_不具合表_20170807.xls：16番：柏原
				objectAriaReg = "#" + textBoxEx_AriaRegEnd.Text + "-#" + textBoxEx_AriaRegStart.Text;
			} else {
				objectAriaReg = "#" + textBoxEx_AriaRegStart.Text + "-#" + textBoxEx_AriaRegEnd.Text;
			}
			if( alreadyNumberCheck( objectAriaReg ) ) {
				//行追加
				LogRecValRowAdd( objectAriaReg );
			}
		}

		/// <summary>
		/// ログ変数設定：ログ記録変数に既に番号が存在するかチェック
		/// </summary>
		private bool alreadyNumberCheck( object value )
		{
			if( dataGridViewEx1.CurrentCell.RowIndex < 0 ) return false;
			object AddLogVal = value;
			//既に番号が有るかチェック
			foreach( DataGridViewRow rows in dataGridViewEx2.Rows ) {
				if( AddLogVal.ToString() == rows.Cells[0].Value.ToString() ) {
					//選択されたログ記録変数は登録済みです。&#xD;&#xA;&#xD;&#xA;他を選択して下さい。 
					using( MessageDialog msg = new MessageDialog() ) {
						msg.Error( 5507, this );
						{
							return false;
						}
					}
				}
			}
			return true;
		}
		/// <summary>
		/// ログ変数設定：ログ記録変数：行追加
		/// </summary>
		/// <param name="stringContent"></param>
		private void LogRecValRowAdd(object stringContent)
		{
			//行追加
			dataGridViewEx2.Rows.Add();
			//行数取得
			int index = dataGridViewEx2.Rows.Count - 1;
			//選択されたマクロ番号設定
			dataGridViewEx2.Rows[index].Cells[0].Value = stringContent;
		}
		/// <summary>
		/// ログ変数設定：範囲登録：開始番号：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBoxEx_AriaRegStart_MouseClick( object sender, MouseEventArgs e )
		{
			_logSettingEditTypes = LogSettingEditTypes.AreaRegStrat;
			string objectString = ( (TextBox)sender ).Text;
			popupTenkeyOnFind( objectString, "範囲登録：開始番号", 5, 10000, 99998, "LogSetting_AreaReg_Start" );
		}
		/// <summary>
		/// ログ変数設定：範囲登録：終了番号：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBoxEx_AriaRegEnd_MouseClick( object sender, MouseEventArgs e )
		{
			_logSettingEditTypes = LogSettingEditTypes.AreaRegEnd;
			string objectString = ( (TextBox)sender ).Text;
			popupTenkeyOnFind( objectString, "範囲登録：終了番号", 5, 10001, 99999, "LogSetting_AreaReg_End" );
		}
		#endregion
		#region<スクロールバーの幅を40ptに設定>
		private const int SMTO_ABORTIFHUNG = 0x2;
		private const int HWND_BROADCAST = 0xffff;
		private const int WM_SETTINGCHANGE =0x001A;
		const int WM_WININICHANGE = 0x001A;

		public enum SendMessageTimeoutFlags : uint
		{
			SMTO_NORMAL = 0x0,
			SMTO_BLOCK = 0x1,
			SMTO_ABORTIFHUNG = 0x2,
			SMTO_NOTIMEOUTIFNOTHUNG = 0x8,
			SMTO_ERRORONEXIT = 0x20
		}
		[DllImport( "user32.dll", SetLastError = true, CharSet = CharSet.Auto )]
		public static extern IntPtr SendMessageTimeout(
			IntPtr hWnd,
			uint Msg,
			UIntPtr wParam,
			IntPtr lParam,
			uint fuFlags,
			uint uTimeout,
			out UIntPtr lpdwResult
		);
		/// <summary>
		/// Windプロシジャー
		/// </summary>
		/// <param name="m"></param>
		protected override void WndProc( ref Message m )
		{
			base.WndProc( ref m );
			if( m.Msg == WM_SETTINGCHANGE ) { 
			}
		}
		/// <summary>
		/// スクロールバーの幅を40ptに設定
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button1_Click( object sender, EventArgs e )
		{
			//レジストリに設定
			Microsoft.Win32.RegistryKey regkey =
				Microsoft.Win32.Registry.CurrentUser.CreateSubKey( @"Control Panel\Desktop\WindowMetrics" );
			//object obj1 = regkey.GetValue( "ScrollWidth" );
			//object obj2 = regkey.GetValue( "ScrollHeight" );
			//スクロールバーの幅設定
			const int pt = 40;//フォントの大きさ：デフォルト：17
			string twips = ( -( pt * 15 )).ToString();
			regkey.SetValue( "ScrollWidth", twips );//デフォルト：-255
			regkey.SetValue( "ScrollHeight", twips );//デフォルト：-255
			//ログオフ/オンか再起動でシステムに反映


			//レジストリを更新
			//UIntPtr result = UIntPtr.Zero;
			//IntPtr setting = Marshal.StringToHGlobalUni( "Environment" );
			//SendMessageTimeout(
			//	(IntPtr)0xFFFF,        //HWND_BROADCAST：システム内の全トップレベルウィンドウへメッセージを送信
			//	0x001A,                //WM_SETTINGCHANGE
			//	(UIntPtr)0,
			//	(IntPtr)setting,
			//	0x0002,                // SMTO_ABORTIFHUNG
			//	5000,
			//	out result
			//);
			//SendMessageTimeout(
			//	(IntPtr)0xFFFF,        //HWND_BROADCAST：システム内の全トップレベルウィンドウへメッセージを送信
			//	0x001A,                //WM_SETTINGCHANGE
			//	(UIntPtr)0x2A,	//wparam
			//	(IntPtr)0,		//lparam
			//	0x0002,                // SMTO_ABORTIFHUNG
			//	5000,
			//	out result
			//);
			//SendMessageTimeout(
			//	(IntPtr)0xFFFF,        //HWND_BROADCAST：システム内の全トップレベルウィンドウへメッセージを送信
			//	0x001A,                //WM_SETTINGCHANGE
			//	(UIntPtr)0,			//wparam
			//	(IntPtr)0xee758,      //lparam
			//	0x0002,                // SMTO_ABORTIFHUNG
			//	5000,
			//	out result
			//);
			//SendMessageTimeout(
			//	(IntPtr)HWND_BROADCAST,//HWND_BROADCAST
			//	WM_SETTINGCHANGE,      //WM_SETTINGCHANGE
			//	IntPtr.Zero,
			//	(IntPtr)Marshal.StringToHGlobalAnsi("Environment" ),
			//	0x0002,                // SMTO_ABORTIFHUNG
			//	5000,
			//	result
			//);
			//Marshal.FreeHGlobal( setting );
		}
		#endregion
	}
}
