///////////////////////////////////////////////////////////////////////////////////////////
//
// (1) システム名称 : ELENIX ECNCⅢ
// (2) ファイル名   : ThinLineSettingForm.cs
// (3) 概要         : 細線設定画面
// (4) 作成日       : 2016.07.19
// (5) 作成者       : 八野
// (6) 特記事項     : VisualStudio2015 C#
// (7) 更新履歴     : 追加：2017-07-07：柏原
//
///////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.IO;            //File
using System.Windows.Forms;	//Contorol用
using ECNC3.Models.McIf;	//BucklingUpOfsZ()、BucklingUpSpdZ()、BucklingRetry()用

namespace ECNC3.Views
{
    /// <summary>
    /// 細線モード設定画面
    /// </summary>
    public partial class ThinLineSettingForm : ECNC3Form
    {
		#region<初期化時>
		/// <summary>ポップアップテンキー</summary>
		private TenKeyDialog _popupTenkey = null;
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ThinLineSettingForm()
        {
            InitializeComponent();
        }
		/// <summary>
		/// FORMロード時
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ThinLineSettingForm_Load( object sender, EventArgs e )
		{
            this.OutLineEnable = true;
            this.OutLineSize = 3;
			McDatInitialPrm mcDatInitialPrm = new McDatInitialPrm();//クラス初期化
			mcDatInitialPrm.Read();//読込み
			//各コントロールに設定
			textBoxEx_ZUpIncrease.Text = mcDatInitialPrm.BucklingUpOfsZ.ToString();	//細線電極確認時の座屈ｾﾝｻｰONによるZ上昇量
			textBoxEx_ZUpVelocity.Text = mcDatInitialPrm.BucklingUpSpdZ.ToString();	//細線電極確認時の座屈ｾﾝｻｰONによるZ上昇速度
			textBoxEx_RetryCount.Text = mcDatInitialPrm.BucklingRetry.ToString();   //細線電極確認時の座屈ｾﾝｻｰONによるリトライ回数
			mcDatInitialPrm.Dispose();//クラス破棄
			mcDatInitialPrm = null;
		}
		#endregion
		#region<ボタン：クリック>
		/// <summary>
		/// OKボタン：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonEx_Ok_Click( object sender, EventArgs e )
		{
			using( McDatInitialPrm datini = new McDatInitialPrm() ) {
				try {
					//RTMC64ECの動作モードを「Setting」にする。
					using( McReqModeChange ReqModeChg = new McReqModeChange() ) {
						ReqModeChg.TaskMode = Enumeration.McTaskModes.Setting;
						if( ReqModeChg.Execute() != Enumeration.ResultCodes.Success ) {
							//動作モード変更に失敗したら行わない。
							return;
						}
					}
					McDatInitialPrm mcDatInitialPrm = new McDatInitialPrm();//クラス初期化
					mcDatInitialPrm.Read();//読込み
					//各コントロールに設定
					mcDatInitialPrm.BucklingUpOfsZ = int.Parse( textBoxEx_ZUpIncrease.Text );       //細線電極確認時の座屈ｾﾝｻｰONによるZ上昇量
					mcDatInitialPrm.BucklingUpSpdZ = int.Parse( textBoxEx_ZUpVelocity.Text );       //細線電極確認時の座屈ｾﾝｻｰONによるZ上昇速度
					mcDatInitialPrm.BucklingRetry = (short)int.Parse( textBoxEx_RetryCount.Text );  //細線電極確認時の座屈ｾﾝｻｰONによるリトライ回数
					mcDatInitialPrm.Write();	//書込み
					mcDatInitialPrm.Dispose();	//クラス破棄
					mcDatInitialPrm = null;
					this.Close();
					//例外処理
				} catch( ArgumentException ex ) {           //引数例外
					MessageBox.Show( ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
				} catch( DirectoryNotFoundException ex ) {  //ファイル/フォルダが見つからない例外
					MessageBox.Show( ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
				} catch( IOException ex ) {                 //I/Oエラーが発生例外
					MessageBox.Show( ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
				} catch( NotSupportedException ex ) {       //メソッドがサポートされていない例外
					MessageBox.Show( ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
				} catch( UnauthorizedAccessException ex ) { //OSがI/Oエラーやセキュリティエラーアクセス拒否例外
					MessageBox.Show( ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
				} catch( Exception ex ) {                   //アプリケーション実行中エラー例外
					MessageBox.Show( ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
				} finally {
					//RTMC64ECの動作モードを「Manual」にする。
					using( McReqModeChange ReqModeChg = new McReqModeChange() ) {
						ReqModeChg.TaskMode = Enumeration.McTaskModes.Manual;
						Enumeration.ResultCodes modeManualResult = Enumeration.ResultCodes.NotExecute;
						int retryCt = 0;
						//動作モード変更に失敗したらリトライする。
						while( modeManualResult != Enumeration.ResultCodes.Success ) {
							RetrySequence( retryCt );
							modeManualResult = ReqModeChg.Execute();
							retryCt++;
						}
					}
				}
			}
		}
		/// <summary>
		/// シーケンス再試行
		/// </summary>
		/// <param name="_retryCt"></param>
		private void RetrySequence( int _retryCt )
		{
			if( _retryCt != 0 && _retryCt <= 10 ) {
				System.Threading.Thread.Sleep( 500 );
			} else if( _retryCt == 0 ) { } else {
				//テクノのセッティングPCを立ち上げるか、PCアプリを再起動する
			}
		}
		/// <summary>
		/// 閉じるボタン：クリックフォームを閉じる
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonEx_Back_Click( object sender, EventArgs e )
		{
			this.Close();
		}
		#endregion
		#region<TextBoxEx：クリック>
		/// <summary>
		/// 座屈センサーONによるZ軸上昇量EditEx：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBoxEx_ZUpIncrease_Click( object sender, EventArgs e )
		{	//ポップアップテンキー：表示
			popupTenkeyOn( sender );
		}
		/// <summary>
		/// 座屈センサーONによるZ軸上昇速度EditEx：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBoxEx_ZUpVelocity_Click( object sender, EventArgs e )
		{	//ポップアップテンキー：表示
			popupTenkeyOn( sender );
		}
		/// <summary>
		/// リトライ回数EditEx：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBoxEx_RetryCount_Click( object sender, EventArgs e )
		{   //ポップアップテンキー：表示
			popupTenkeyOn( sender );
		}
		#endregion
		#region<ポップアップテンキー>
		/// <summary>
		/// 記録する加工条件値
		/// </summary>
		/// <summary>
		object _controlName = "";//記録するコントロール名
		/// ポップアップテンキー：表示/非表示
		/// </summary>
		/// <param name="objControlName">コントロール名</param>
		/// </summary>
		private void popupTenkeyOn( object objControlName )
		{
			if( _popupTenkey != null ) {
				_popupTenkey.Close();			//画面を閉じる
				_popupTenkey.Dispose();			//破棄
				_popupTenkey = null;			//null初期化
			}
			//初期値設定
			_controlName = objControlName;      //記録するコントロール名
			string titleString = "";            //ウインドタイトルデフォルト
			string changeVal = "";              //編集値デフォルト
			Decimal lowerLimitDec = 0;          //最小デフォルト
			Decimal upperLimitDec = 0;          //最大デフォルト
			string strTenkeyMode = "";          //UITenkeyModeAndPosRec.xmlより表示位置、表示モードを取得
			NumericTextBox.FormatTypes formatType = NumericTextBox.FormatTypes.Integer1;//フォーマットタイプ
			bool firstValueSelect = true;		//初回文字列選択= true
			bool realValueEdit = true;			//実数編集		= true
			bool oneKetaEditImpossible = true;	//下1桁編集不可	= true
			//初期値設定を変更します
			setCodeForControl(
				objControlName,					//コントロール名
				ref titleString,				//ウインドタイトル
				ref changeVal,					//編集文字列
				ref lowerLimitDec,				//最小値
				ref upperLimitDec,				//最大値
				ref strTenkeyMode,				//UITenkeyModeAndPosRec.xmlより表示位置、表示モードを取得
				ref formatType,					//フォーマットタイプ
				ref firstValueSelect,			//初回文字列選択	= true
				ref realValueEdit,				//実数編集			= true
				ref oneKetaEditImpossible		//下1桁編集不可		= true
			);
			//ポップアップTenKey：
			_popupTenkey = new TenKeyDialog(
				changeVal,						//このコントロールで表示する文字
				formatType,						//NumericTextBoxの編集フォーマットタイプ
				lowerLimitDec,					//最小値
				upperLimitDec,					//最大値
				firstValueSelect,				//true=初回文字列選択
				realValueEdit,					//true=実数編集
				oneKetaEditImpossible,			//true=下1桁編集不可
				strTenkeyMode					//テンキー表示モード：UITenkeyModeAndPosRec.xmlに記述された表示位置やモードで表示
			);
			_popupTenkey.NotifyReturnOk = popupTenkey_OnNotifyReturnOk; //イベント通知：OK
			_popupTenkey.Text = titleString;                            //テンキータイトル表示
			_popupTenkey.ShowDialog( this );                            //画面を開く:
			_popupTenkey.Dispose();                                     //破棄
			_popupTenkey = null;                                        //null初期化
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
		private void setCodeForControl(
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
			//各クリックされたコントロールの下段コントロール値を取得
			switch( ( (Control)objControlName ).Name ) {
				case "textBoxEx_ZUpIncrease"://細線電極確認時の座屈ｾﾝｻｰONによるZ上昇量
					titleString = label_ZUpIncrease.Text;               //ウインドタイトル
					changeVal = textBoxEx_ZUpIncrease.Text;             //編集文字列
					lowerLimitDec = (decimal)0;                         //最小値
					upperLimitDec = (decimal)99999999;                  //最大値
					strTenkeyMode = "ThinLineSet_ZUpIncrease";          //UITenkeyModeAndPosRec.xmlより表示位置、表示モードを取得
					formatType = NumericTextBox.FormatTypes.Integer8;   //フォーマットタイプ
					break;
				case "textBoxEx_ZUpVelocity"://細線電極確認時の座屈ｾﾝｻｰONによるZ上昇速度
					titleString = label_ZUpVelocity.Text;               //ウインドタイトル
					changeVal = textBoxEx_ZUpVelocity.Text;             //編集文字列
					lowerLimitDec = (decimal)1;                         //最小値
					upperLimitDec = (decimal)4000000;                   //最大値
					strTenkeyMode = "ThinLineSet_ZUpVelocity";          //UITenkeyModeAndPosRec.xmlより表示位置、表示モードを取得
					formatType = NumericTextBox.FormatTypes.Integer7;   //フォーマットタイプ
					break;
				case "textBoxEx_RetryCount"://細線電極確認時の座屈ｾﾝｻｰONによるリトライ回数
					titleString = label_RetryCount.Text;                //ウインドタイトル
					changeVal = textBoxEx_RetryCount.Text;              //編集文字列
					lowerLimitDec = (decimal)0;                         //最小値
					upperLimitDec = (decimal)32767;                     //最大値
					strTenkeyMode = "ThinLineSet_RetryCount";           //UITenkeyModeAndPosRec.xmlより表示位置、表示モードを取得
					formatType = NumericTextBox.FormatTypes.Integer5;   //フォーマットタイプ
					break;
			}
			firstValueSelect = true;        //初回文字列選択	= true
			realValueEdit = false;          //実数編集			= true
			oneKetaEditImpossible = false;  //下1桁編集不可		= true
		}
		/// <summary>
		/// ポップアップテンキーで「OK」ボタンを押した時、入力値をこのクラスに反映
		/// </summary>
		private void popupTenkey_OnNotifyReturnOk()
		{
			string retVal = _popupTenkey._tenkeyValReturn;	//ポップアップテンキーで編集された値
			switch( ( (Control)_controlName ).Name ) {		//記録していたコントロール値
				case "textBoxEx_ZUpIncrease": textBoxEx_ZUpIncrease.Text = retVal; break;//細線電極確認時の座屈ｾﾝｻｰONによるZ上昇量
				case "textBoxEx_ZUpVelocity": textBoxEx_ZUpVelocity.Text = retVal; break;//細線電極確認時の座屈ｾﾝｻｰONによるZ上昇速度
				case "textBoxEx_RetryCount" : textBoxEx_RetryCount.Text  = retVal; break;//細線電極確認時の座屈ｾﾝｻｰONによるリトライ回数
			}
		}
		#endregion
	}
}
