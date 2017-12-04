///////////////////////////////////////////////////////////////////////////////////////////
//
// (1) システム名称 : ELENIX ECNCⅢ
// (2) ファイル名   : ValiableListForm.cs
// (3) 概要         : マクロ変数リスト表示画面
// (4) 作成日       : 2016.07.19
// (5) 作成者       : 八野
// (6) 特記事項     : VisualStudio2015 C#
// (7) 更新履歴     : 2017.07.24変更：柏原
//
///////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ECNC3.Models.McIf;
using System.Xml;           //XML
using System.IO;            //File
using ECNC3.Enumeration;
using ECNC3.Models;         //FilePathInfo

namespace ECNC3.Views
{
	/// <summary>
	/// マクロ変数リスト表示画面
	/// </summary>
	public partial class ValiableListForm : ECNC3Form
	{
		#region<初期化>
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ValiableListForm()
		{
			InitializeComponent();
		}
		/// <summary>
		/// NCデータ読込みタイマー
		/// </summary>
		private Timer _ncReadTimer;
		/// <summary>
		/// フォームロード時
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ValiableListForm_Load( object sender, EventArgs e )
		{
            listView_List.BackColor = FileUIStyleTable.DefaultBackColor;
            listView_List.ForeColor = FileUIStyleTable.DefaultForeColor;
            listView_ListName.BackColor = FileUIStyleTable.DefaultBackColor;
            listView_ListName.ForeColor = FileUIStyleTable.DefaultForeColor;

            SetLeftListWidth();	// 左側リスト幅設定
			SetRightListWidth();// 右側リスト幅設定
			ValiableListXML_Read();//ValiableList.xml：読込み
								   //NCデータ読込みタイマー設定
			_ncReadTimer = new Timer();//精度が低い(上限 55msec)、•メイン関数と同一スレッド
			_ncReadTimer.Tick += new EventHandler( this.fireNcDataRead );
			_ncReadTimer.Interval = 5000;
			trackBar1.Value = _ncReadTimer.Interval;//トラックバー設定
			// タイマーを開始
			_ncReadTimer.Start();
		}
		/// <summary>
		/// NCデータ読込みタイマー：ファイアー時
		/// </summary>
		/// <param name="obj"></param>
		public void fireNcDataRead( object sender, EventArgs e )
		{
			NcReadAndListUpdate( _intMmacro );              //NC読み込みとListコントロールの更新
		}
		/// <summary>
		/// トラックバー：スクロール
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void trackBar1_Scroll( object sender, EventArgs e )
		{
			int TrackValNow = ( (TrackBar)sender ).Value;
			_ncReadTimer.Interval = TrackValNow;
		}
		/// <summary>
		/// ValiableList.xml：読込み
		/// </summary>
		/// <param name="loopCont"></param>
		private void ValiableListXML_Read()
		{
			try {
				//ECNC3Apisを使用しないでリード
				string masterFolder = @FilePathInfo.MasterData;//\Masterフォルダ
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load( masterFolder + "UIValiableList.xml" );//読込
				XmlNode root = xmlDocument.DocumentElement;
				int loopCont = root.ChildNodes.Count;
				//_stMainte1LineDat = null;//読み込むデータ
				for( int intLoop = 0 ; intLoop < loopCont ; intLoop++ ) {
					XmlNode node = root.ChildNodes[intLoop];
					if( node.Name == "Item" + ( intLoop + 1 ).ToString( "000" ) ) {
						listView_ListName.Items.Add( ( intLoop + 1 ).ToString( "000" ) );
						listView_ListName.Items[intLoop].SubItems.Add( ( (XmlElement)node ).GetAttribute( "ListName" ) );
					}
				}
				xmlDocument = null;
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
			}
		}
		#endregion
		#region<終了時>
		/// <summary>
		/// 閉じるボタン：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ButtonEx_Return_Click( object sender, EventArgs e )
		{
			//タイマー破棄
			if( null != _ncReadTimer ) {
				_ncReadTimer.Dispose();
				_ncReadTimer = null;
			}
			Close();
		}
		/// <summary>
		/// フォームを閉じるイベントハンドラ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">BackMDIAUTOボタンのClickイベント</param>
		private void BackMDIAUTOFormBt_Click( object sender, EventArgs e )
		{
			Close();
		}
		#endregion
		#region<リスト1(右側)>
		/// <summary>
		/// 左側リスト幅設定
		/// </summary>
		private void SetLeftListWidth() {
			listView_List.Columns[0].Width = 100;//リスト幅
			listView_List.Columns[1].Width = 200;//
			listView_List.Columns[2].Width = 300;//
		}
		/// <summary>
		/// NC読み込みとListコントロールへセット
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NcReadAndListSet( int intMacroVal )
		{
			if( intMacroVal == 0 ) return;

			// 元のカーソルを保持
			Cursor preCursol = Cursor.Current;
			// カーソルを待機カーソルに変更
			Cursor.Current = Cursors.WaitCursor;
			McDatMacroManage macroManage = new McDatMacroManage( MacroManage.ExtensionMacro );  //クラス作成
			try {
				ResultCodes ret = macroManage.Read();
				if( ret == ResultCodes.Success ) {
					//読み込み成功時
					listView_List.Items.Clear();//左リスト：内容クリア
					int intLoop = intMacroVal - 10000;//開始番号：設定
					ListViewItem listViewItem;//リストビューアイテム
					for( ; intLoop < intMacroVal ; intLoop++ ) {
						int index = macroManage.Items[intLoop].Number;                                       //#No.取得
						listViewItem = listView_List.Items.Add( "#" + macroManage.Items[intLoop].Number );   //#No.
						if( macroManage.Items[intLoop].Value != null ) {
							listViewItem.SubItems.Add( macroManage.Items[intLoop].Value.ToString() );        //DATA：整数/実数混合
						}
						if( macroManage.Items[intLoop].Comment != null ) {
							listViewItem.SubItems.Add( macroManage.Items[intLoop].Comment.ToString() );     //COMMENT
						}
					}
				} else {//エラー
					macroManage.Dispose();
					macroManage = null;
					MessageBox.Show( "読み込みエラー発生", "失敗", MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
					return;
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
				//破棄
				macroManage.Dispose();
				macroManage = null;
				//カーソルを元に戻す
				Cursor.Current = preCursol;         //カーソルを元に戻す
			}
		}
		/// <summary>
		/// NC読み込みとListコントロールの更新
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NcReadAndListUpdate( int intMacroVal )
		{
			if( intMacroVal == 0 ) return;
			// 元のカーソルを保持
			Cursor preCursol = Cursor.Current;
			// カーソルを待機カーソルに変更
			Cursor.Current = Cursors.WaitCursor;
			McDatMacroManage macroManage = new McDatMacroManage( MacroManage.ExtensionMacro );  //クラス作成
			try {
				ResultCodes ret = macroManage.Read();
				if( ret == ResultCodes.Success ) {
					//読み込み成功時
					int intLoop = intMacroVal - 10000;//開始番号：設定
					int intArray = 0;
					for( ; intLoop < intMacroVal ; intLoop++ ) {
						//int index = macroManage.Items[intLoop].Number;   //#No.取得
						//データ更新
						if( macroManage.Items[intLoop].Value != null ) {
							listView_List.Items[intArray].SubItems[1].Text = //SubItems[1] = DATA
							macroManage.Items[intLoop].Value.ToString();
						}
						if( macroManage.Items[intLoop].Comment != null ) {
							//データがnullで無ければ表示
							listView_List.Items[intArray].SubItems[2].Text = //SubItems[2] = COMMENT
								macroManage.Items[intLoop].Comment.ToString();
						}
						intArray++;
					}
				} else {//エラー
					macroManage.Dispose();
					macroManage = null;
					MessageBox.Show( "読み込みエラー発生", "失敗", MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
					return;
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
				//破棄
				macroManage.Dispose();
				macroManage = null;
				//カーソルを元に戻す
				Cursor.Current = preCursol;         //カーソルを元に戻す
			}
		}
		#endregion
		#region<リスト2(左側)>
		/// <summary>
		/// 右側リスト幅設定
		/// </summary>
		private void SetRightListWidth()
		{
			listView_ListName.Columns[0].Width = 100;//No,
			listView_ListName.Columns[1].Width = 265;//LIST NAME
		}
		private int _intMmacro = 0;
		/// <summary>
		/// リスト２(右側)：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listView_ListName_Click( object sender, EventArgs e )
		{
			if( listView_ListName.SelectedItems.Count == 0 )return;		//項目が１つも選択されていない場合
			int selectIndex = listView_ListName.SelectedItems[0].Index;
			string selectString = listView_ListName.SelectedItems[0].SubItems[1].Text;
			int intSharpPos = selectString.IndexOf( "#" );				//"マクロ変数#nnnnn"が前提
			int intHyphenPos = selectString.IndexOf( "-" );
			string stringMacro = selectString.Substring( intSharpPos + 1, intHyphenPos - intSharpPos - 1 );//マクロ番号取得
			int intMmacro = int.Parse( stringMacro );
			_intMmacro = intMmacro;
			NcReadAndListSet( intMmacro );				//NC読み込みとListコントロールへセット
		}
		#endregion


		private void button1_Click(object sender, EventArgs e)
        {
            if(listView_List.VirtualMode == true)
            {
                listView_List.VirtualMode = false;
                listView_List.VirtualListSize = 0;
                listView_List.Items.Clear();
            }
            //_macroData.Read();
            listView_List.VirtualMode = true;
            listView_List.VirtualListSize = 90000;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(listView_List.VirtualMode == false)
            {
                return;
            }
            //_macroData.Items.Clear();
            //_macroData.Items.TrimExcess();
            for(int ct = 0; ct < 90000; ct++) 
            {
                //Models.StructureMacroManageItem writeItem = new Models.StructureMacroManageItem();
                //writeItem.Number = int.Parse(listView1.Items[ct].Text);
                //writeItem.Value = "1" + listView1.Items[ct].SubItems[0].Text;
                //_macroData.Items[ct].Value = "123.456"; 
            }
           // _macroData.Write();
        }


        private void listView1_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (e.Item == null)
            {
                e.Item = new ListViewItem();
            }
           // e.Item.Text = "#" + _macroData.Items[e.ItemIndex].Number.ToString();
           // e.Item.SubItems.Add(_macroData.Items[e.ItemIndex].Value);

        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (listView_List.SelectedItems != null)
            {
                listView_List.VirtualMode = false;
                string stemp = listView_List.SelectedItems[0].SubItems[0].Text;
                double itemp = Convert.ToDouble(stemp);
                listView_List.SelectedItems[0].SubItems[0].Text = (itemp + 1).ToString();
                listView_List.VirtualMode = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (listView_List.SelectedItems != null)
            {
                double itemp = Convert.ToDouble(listView_List.SelectedItems[0].SubItems[0].Text);
                listView_List.SelectedItems[0].SubItems[0].Text = (itemp - 1).ToString();
            }
        }
	}
}
