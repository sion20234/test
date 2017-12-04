///////////////////////////////////////////////////////////////////////////////////////////
//
// (1) システム名称 : ELENIX ECNCⅢ
// (2) ファイル名   : AlarnLogForm.cs
// (3) 概要         : アラームログ表示画面
// (4) 作成日       : 2017.07.03
// (5) 作成者       : 柏原ひろむ
// (6) 特記事項     : VisualStudio2015 C#
// (7) 更新履歴     : 
//
///////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Windows.Forms;
using ECNC3.Models;			//fileAlarmLog
using ECNC3.Enumeration;    //ResultCodes
using System.IO;            //File

namespace ECNC3.Views
{
    public partial class AlarmLogForm : ECNC3Form
    {
		#region<初期化時>
		//ValiableFileForm FileExp;
		/// <summary>
		/// フォーム：コンストラクタ
		/// </summary>
		public AlarmLogForm()
        {
            InitializeComponent();
        }
		/// <summary>
		/// フォーム：ロード時のイベントハンドラ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AlarmLogForm_Load( object sender, EventArgs e )
		{
		}
		#endregion
		#region<終了時>
		/// <summary>
		/// 閉じる　ボタン　クリック時のイベントハンドラ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UserFuncFormBt_Click(object sender, EventArgs e)
        {
            this.Close();
        }
		#endregion
		#region<DGVEX：設定>
		/// <summary>
		/// DGVEXの設定
		/// </summary>
		/// <param name="dgvEx"></param>
		/// <param name="headerText"></param>
		/// <param name="rowCount"></param>
		private void SetDGVEx( DataGridViewEx dgvEx, string[] stringHeders, int rowCount )
		{
			//dataGridViewEx1：ヘッダ色や大きさ設定
			dgvEx.Initialize( 12.0F, 30, true );//true=編集不可
			//列クリア
			dgvEx.Columns.Clear();
			//列数
			int colCount = stringHeders.Length;
			//列ヘッダー設定
			for( int loopCol = 0 ; loopCol < colCount ; loopCol++ ) {
				DataGridViewColumn colTemp = new DataGridViewColumn();
				colTemp.CellTemplate = new DataGridViewTextBoxCell();
				colTemp.Name = "Col" + loopCol.ToString();
				colTemp.HeaderText = stringHeders[loopCol];//ヘッダーテキスト
				colTemp.Width = 260;
				//dgvEx：列ヘッダーをdataGridViewEx1に追加
				dgvEx.Columns.Add( colTemp );
                if (loopCol == 2)
                {//右：数字
                    dgvEx.InitCol(colTemp.Name, 12.0F, DataGridViewContentAlignment.MiddleRight, typeof(string));
                }else
                {//左：文字
                    //dgvEx：列ヘッダー：フォントや色設定
                    dgvEx.InitCol(colTemp.Name, 12.0F, DataGridViewContentAlignment.MiddleLeft, typeof(string));
                }
            }
			//dgvEx：行数設定
			dgvEx.RowCount = rowCount;
			//dgvEx：各種設定
			dgvEx.AutoSize = false;//
			dgvEx.ReadOnly = true;
			//dgvEx.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
			dgvEx.MultiSelect = false;                  //複数選択＝不可
			dgvEx.AllowUserToAddRows = false;           //追加不可
			dgvEx.AllowUserToDeleteRows = false;        //削除不可
														//dataGridViewEx1_Enable( false )			//初回セル使用不可
														//dgvEx.RowHeadersVisible = true;			//固定列：表示
		}
		#endregion
		#region<DGVEX：行移動ボタン：-100▲から100▲>
		/// <summary>
		/// ページ：最初ボタン：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonEx_First_Click( object sender, EventArgs e )
		{
			cellRowMove( 0 );
		}
		/// <summary>
		/// ページ：100▲ボタン：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonEx_100Up_Click( object sender, EventArgs e )
		{
			valueUpDn( -100 );
		}
		/// <summary>
		/// ページ：10▲ボタン：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonEx_10Up_Click( object sender, EventArgs e )
		{
			valueUpDn( -10 );
		}
		/// <summary>
		/// ページ：1▲ボタン：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonEx_1Up_Click( object sender, EventArgs e )
		{
			valueUpDn( -1 );
		}
		/// <summary>
		/// ページ：1▼ボタン：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonEx_1Dn_Click( object sender, EventArgs e )
		{
			valueUpDn( 1 );
		}
		/// <summary>
		/// ページ：10▼ボタン：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonEx_10Dn_Click( object sender, EventArgs e )
		{
			valueUpDn( 10 );
		}
		/// <summary>
		/// ページ：100▼ボタン：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonEx_100Dn_Click( object sender, EventArgs e )
		{
			valueUpDn( 100 );
		}
		/// <summary>
		/// ページ：最後ボタン：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonEx_Last_Click( object sender, EventArgs e )
		{
			cellRowMove( dataGridViewEx1.RowCount - 1 );
		}
		/// <summary>
		/// 数字ボタン：▲1▼0、100：加算/減算
		/// </summary>
		/// <param name="value"></param>
		private void valueUpDn( int value )
		{
			if( dataGridViewEx1 == null ) return;
			if( dataGridViewEx1.CurrentCell == null ) return;
			//カレントセル + とび先インデックス
			int rowIndex = dataGridViewEx1.CurrentCell.RowIndex + value;
			//とび先インデックスが0以下
			if( rowIndex < 0 ) rowIndex = 0;
			//rowIndexが範囲を超えた場合
			if( rowIndex >= dataGridViewEx1.RowCount ) {
				rowIndex = dataGridViewEx1.RowCount - 1;
			}
			//とび先の番号セルがnull？
			if( dataGridViewEx1[0, rowIndex].Value == null || dataGridViewEx1[0, rowIndex].Value.ToString() == "" ) return;
			//指定行へセル移動
			cellRowMove( rowIndex );
			if( rowIndex < 0 ) rowIndex = 0;
		}
		/// <summary>
		/// 指定行セルに移動
		/// </summary>
		/// <param name="rowIndex"></param>
		private void cellRowMove( int rowIndex )
		{
			if( dataGridViewEx1.CurrentCell == null ) return;
			int colIndex = dataGridViewEx1.CurrentCell.ColumnIndex;//列
			if( rowIndex < 0 ) rowIndex = 0;
			//指定行へセル移動
			dataGridViewEx1.CurrentCell = dataGridViewEx1[colIndex, rowIndex];
		}
		#endregion
		#region<ログを開くボタン：クリック>
		/// <summary>
		/// ログを開くボタン：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonEx_LogOpen_Click( object sender, EventArgs e )
		{
			FileForm file = null;
			FileAlarmLog fileAlarmLog = null;
			try {
				//ツリービューで選択されたパスとファイル名を取得
				file = new FileForm( FileFormMode.AlamLogHistOpen, "", 4 );	//最後の引数は拡張子の種類番号
				file.ShowDialog();                                          //ツリーとリスト表示
				if( file._returnFullPath == "" )return;						//未選択
				labelEx_Path.Text = file._returnFullPath;					//フルパスをラベルに設定
				//アラームログ：読込み
				fileAlarmLog = new FileAlarmLog( file._returnPath );		//選択されたファイル
				ResultCodes resultCodes = fileAlarmLog.Read();				//読込み
				//DGVExの設定
				string[] stringHeders = { "発生日時", "種類", "番号" };
				SetDGVEx( dataGridViewEx1, stringHeders, fileAlarmLog.Items.Count );
				//DGVExのセルにデータ設定
				for( int index = 0 ; index < fileAlarmLog.Items.Count ; index++ ) {
					dataGridViewEx1.Rows[index].Cells[0].Value = fileAlarmLog.Items[index].Time;    //発生日時
					dataGridViewEx1.Rows[index].Cells[1].Value = fileAlarmLog.Items[index].Kind;    //番号
					dataGridViewEx1.Rows[index].Cells[2].Value = fileAlarmLog.Items[index].Number;  //番号
				}
			//例外処理
			} catch( ArgumentException ex ) {			//引数例外
				MessageBox.Show( ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
			} catch( DirectoryNotFoundException ex ) {	//ファイル/フォルダが見つからない例外
				MessageBox.Show( ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
			} catch( IOException ex ) {					//I/Oエラーが発生例外
				MessageBox.Show( ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
			} catch( NotSupportedException ex ) {		//メソッドがサポートされていない例外
				MessageBox.Show( ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
			} catch( UnauthorizedAccessException ex ) {	//OSがI/Oエラーやセキュリティエラーアクセス拒否例外
				MessageBox.Show( ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
			} catch( Exception ex ) {					//アプリケーション実行中エラー例外
				MessageBox.Show( ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
			} finally {
				if( file != null ) {
					file.Dispose(); //リソース解放
					file = null;
				}
				if( fileAlarmLog != null ) {
					fileAlarmLog.Dispose(); //リソース解放
					fileAlarmLog = null;
				}
			}
		}
		#endregion
		#region<エクスポート(USB)ボタン：クリック>
		/// </summary>>
		/// <summary>
		/// エクスポート(USB)ボタン：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FileExportBt_Click( object sender, EventArgs e )
		{
			FileForm file = null;
			try {
				//リストビューで選択された複数ファイル名を取得
				file = new FileForm( FileFormMode.AlamLogHistExport, "", 4 );	//最後の引数は拡張子の種類番号
				file.ShowDialog();												//ツリーとリスト表示
				if( file._returnFullPaths.Length == 0 ) return;					//データ無し
				if( file._returnFullPaths[0] == "" ) 	return;					//未選択
				string usbPath = FilePathInfo.Usb;								//Path.xml：USBパスにコピー保存
				int loopMax = file._returnFullPaths.Length;						//ループ回数＝リスト配列数
				//コピー先パス
				if( !File.Exists( usbPath ) ) {
					//存在しない場合、コピー先パスを作成
					DirectoryInfo di = Directory.CreateDirectory( usbPath );
				}
				//選択されたファイルリスト数だけコピー処理をします。
				for( int loopCount = 0 ; loopCount < loopMax ; loopCount++ ) {
					if( File.Exists( usbPath + file._returnPaths[loopCount] ))
					{
						DialogResult dRes = MessageBox.Show(
							usbPath + file._returnPaths[loopCount] + "\r\n" + "同名ファイルが存在します、上書きしますか？",
							this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question );
						if( DialogResult.No == dRes ) {
							continue;//上書きしません。
						}
						//上書きする場合、まずコピー先ファイルを削除
						File.Delete( usbPath + file._returnPaths[loopCount] );
					}
					//ファイルをコピー
					File.Copy( file._returnPath + file._returnFullPaths[loopCount], usbPath + file._returnPaths[loopCount] );
				}
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
				if( file != null ) {
					file.Dispose(); //リソース解放
					file = null;
				}
			}
		}
	#endregion
		#region<検索ボタン：クリック>
		/// <summary>
		/// 検索ボタン：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonEx_Find_Click( object sender, EventArgs e )
		{

		}
		#endregion
	}
}
