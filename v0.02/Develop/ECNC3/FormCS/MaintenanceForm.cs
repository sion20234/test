///////////////////////////////////////////////////////////////////////////////////////////
//
// (1) システム名称 : ELENIX ECNCⅢ
// (2) ファイル名   : MaintenanceForm.cs
// (3) 概要         : メンテナンス期間表示画面
// (4) 作成日       : 2015.06.01
// (5) 作成者       : 八野
// (6) 特記事項     : VisualStudio2015 C#
// (7) 更新履歴     : 2017-07-21:追加：柏原
//
///////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.IO;            //File
using System.Drawing;
using System.Windows.Forms;
using ECNC3.Models;			//FilePathInfo
using System.Xml;			//XML
using ECNC3.Enumeration;    //DG背景色
using System.Linq;

namespace ECNC3.Views
{
	public partial class MaintenanceForm : ECNC3Form
	{
		#region<初期化>
		/// <summary>
		/// メンテナンス１行データ構造体
		/// </summary>
		public struct StructMainte1LineData
		{
			public int index;       //インデックス
			public string itemName; //アイテム名
			public string life;     //寿命
			public string unit;     //単位
			public string now;      //現在値
		}
		/// <summary>
		/// メンテナンス１行データ列挙体
		/// </summary>
		public enum EnumMainte1LineData
		{
			index = 0, //インデックス
			itemName = 1, //アイテム名
			life = 2, //寿命
			unit = 3, //単位
			now = 4,  //現在値
			nowSec = 5  //現在値(秒)
		}
		private StructMainte1LineData[] _stMainte1LineDat;//1行データ
		public bool RequestTimerStop = false;

		/// <summary>
		///　フォーム　コンストラクタ
		/// </summary>
		public MaintenanceForm()
		{
			InitializeComponent();
		}
		/// <summary>
		/// Formロード時
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MaintenanceForm_Load( object sender, EventArgs e )
		{
            //System.Threading.Thread.Sleep(5000);
			// 元のカーソルを保持
			Cursor preCursor = Cursor.Current;
			// カーソルを待機カーソルに変更
			Cursor.Current = Cursors.WaitCursor;
			//読込み
			int rowCount = 0;
			MaintenanseXML_Read( ref rowCount );
			//DGVEx初期設定
			SetDGVEx( dataGridViewEx1, rowCount );
			dataGridViewEx1.Columns[(int)EnumMainte1LineData.nowSec].Visible = false;//最終列非表示
			//DGVExデータ設定
			for( int intLoop = 0 ; intLoop < rowCount ; intLoop++ ) {
				dataGridViewEx1.Rows[intLoop].Cells[(int)EnumMainte1LineData.index	].Value = _stMainte1LineDat[intLoop].index;
				dataGridViewEx1.Rows[intLoop].Cells[(int)EnumMainte1LineData.itemName].Value = _stMainte1LineDat[intLoop].itemName;
				dataGridViewEx1.Rows[intLoop].Cells[(int)EnumMainte1LineData.life	].Value = _stMainte1LineDat[intLoop].life;
				dataGridViewEx1.Rows[intLoop].Cells[(int)EnumMainte1LineData.unit	].Value = _stMainte1LineDat[intLoop].unit;
                if (_stMainte1LineDat[intLoop].now.Contains("/"))
                {//日付
                    dataGridViewEx1.Rows[intLoop].Cells[(int)EnumMainte1LineData.now].Value = _stMainte1LineDat[intLoop].now;//yyyy/mm/dd
                }
                else
                {//数字
                    dataGridViewEx1.Rows[intLoop].Cells[(int)EnumMainte1LineData.now].Value = int.Parse(_stMainte1LineDat[intLoop].now) / 3600;//時
                }
                dataGridViewEx1.Rows[intLoop].Cells[(int)EnumMainte1LineData.nowSec	].Value = _stMainte1LineDat[intLoop].now;//秒
				//寿命を超えた場合、背景色設定
				redThenDayLimitOver( intLoop, _stMainte1LineDat[intLoop].life );
			}
			dataGridViewEx1.DefaultCellStyle.BackColor = ECNC3Color.StandardBack;
            this.TopMost = true;
            // カーソルを元に戻す
            Cursor.Current = preCursor;            
        }
		#endregion
		#region<終了時>
		/// <summary>
		/// 閉じるボタン：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BackUserFuncFormBt_Click( object sender, EventArgs e )
		{
			if( _stMainte1LineDat != null ) {
				_stMainte1LineDat = null;
			}
			//ポップアップ・キーボード：閉じる
			if( null != _PopupKeybord ) {
				this.Controls.Remove( _PopupKeybord );
				_PopupKeybord.Dispose();
				_PopupKeybord = null;
			}
			//ポップアップ・テンキー：閉じる
			if( _popupTenkey != null ) {
				_popupTenkey.Dispose();
				_popupTenkey = null;
			}
			//自分を閉じる
			this.Close();
		}
		#endregion
		#region<DGVEX>
		/// <summary>
		/// DGVExの設定
		/// </summary>
		/// <param name="dgvEx"></param>
		/// <param name="headerText"></param>
		/// <param name="rowCount"></param>
		private void SetDGVEx( DataGridViewEx dgvEx, int rowCount )
		{
			//DataGridViewEx設定
			//dgvEx.Initialize();
			//dataGridViewEx1：ヘッダ色や大きさ設定
			dgvEx.Initialize( 12.0F, 30, false );//true=編集不可

			//列ヘッダー設定:1
			DataGridViewColumn col1 = new DataGridViewColumn();
			col1.CellTemplate = new DataGridViewTextBoxCell();
			col1.Name = "Col1";
			col1.HeaderText = "番号";
			col1.Width = 50;
			//dgvEx：列ヘッダーをdataGridViewEx1に追加
			dgvEx.Columns.Add( col1 );
			//dgvEx：列ヘッダー：フォントや色設定
			dgvEx.InitCol( "Col1", 12.0F, DataGridViewContentAlignment.MiddleRight, typeof( string ) );

			//列ヘッダー設定:2
			DataGridViewColumn col2 = new DataGridViewColumn();
			col2.CellTemplate = new DataGridViewTextBoxCell();
			col2.Name = "Col2";
			col2.HeaderText = "アイテム名";
			col2.Width = 400;
			//dgvEx：列ヘッダーをdataGridViewEx1に追加
			dgvEx.Columns.Add( col2 );
			//dgvEx：列ヘッダー：フォントや色設定
			dgvEx.InitCol( "Col2", 12.0F, DataGridViewContentAlignment.MiddleLeft, typeof( string ) );

			//列ヘッダー設定:3
			DataGridViewColumn col3 = new DataGridViewColumn();
			col3.CellTemplate = new DataGridViewTextBoxCell();
			col3.Name = "Col3";
			col3.HeaderText = "寿命";
			col3.Width = 180;
			//dgvEx：列ヘッダーをdataGridViewEx1に追加
			dgvEx.Columns.Add( col3 );
			//dgvEx：列ヘッダー：フォントや色設定
			dgvEx.InitCol( "Col3", 12.0F, DataGridViewContentAlignment.MiddleRight, typeof( string ) );

			//列ヘッダー設定:4
			DataGridViewColumn col4 = new DataGridViewColumn();
			col4.CellTemplate = new DataGridViewTextBoxCell();
			col4.Name = "Col4";
			col4.HeaderText = "単位";
			col4.Width = 180;
			//dgvEx：列ヘッダーをdataGridViewEx1に追加
			dgvEx.Columns.Add( col4 );
			//dgvEx：列ヘッダー：フォントや色設定
			dgvEx.InitCol( "Col4", 12.0F, DataGridViewContentAlignment.MiddleLeft, typeof( string ) );

			//列ヘッダー設定:5
			DataGridViewColumn col5 = new DataGridViewColumn();
			col5.CellTemplate = new DataGridViewTextBoxCell();
			col5.Name = "Col5";
			col5.HeaderText = "現在";
			col5.Width = 180;
			//dgvEx：列ヘッダーをdataGridViewEx1に追加
			dgvEx.Columns.Add( col5 );
			//dgvEx：列ヘッダー：フォントや色設定
			dgvEx.InitCol( "Col5", 12.0F, DataGridViewContentAlignment.MiddleRight, typeof( string ) );

			//列ヘッダー設定:6
			DataGridViewColumn col6 = new DataGridViewColumn();
			col6.CellTemplate = new DataGridViewTextBoxCell();
			col6.Name = "Col6";
			col6.HeaderText = "現在(秒)";
			col6.Width = 180;
			//dgvEx：列ヘッダーをdataGridViewEx1に追加
			dgvEx.Columns.Add( col6 );
			//dgvEx：列ヘッダー：フォントや色設定
			dgvEx.InitCol( "Col6", 12.0F, DataGridViewContentAlignment.MiddleRight, typeof( string ) );

			//dgvEx：行数設定
			dgvEx.RowCount = rowCount;
			//dgvEx：各種設定
			dgvEx.AutoSize = false;
			dgvEx.ReadOnly = true;
			dgvEx.MultiSelect = false;                  //複数選択＝不可
			dgvEx.AllowUserToAddRows = false;           //追加不可
			dgvEx.AllowUserToDeleteRows = false;        //削除不可
			dgvEx.RowHeadersVisible = false;            //固定列表示 = true
            dgvEx.ReadOnly = false;                     //全セル編集許可
            //列毎のReadOnly
            dgvEx.Columns[(int)EnumMainte1LineData.index].ReadOnly = true;      //インデックス
			dgvEx.Columns[(int)EnumMainte1LineData.itemName].ReadOnly = false;  //アイテム名
			dgvEx.Columns[(int)EnumMainte1LineData.life].ReadOnly = false;      //寿命
			dgvEx.Columns[(int)EnumMainte1LineData.unit].ReadOnly = false;      //単位
			dgvEx.Columns[(int)EnumMainte1LineData.now].ReadOnly = false;       //現在値
			dgvEx.Columns[(int)EnumMainte1LineData.nowSec].ReadOnly = false;       //現在値
		}
		/// <summary>
		/// DGVEX：寿命を超えた場合、赤色行にする
		/// </summary>
		/// <param name="intRow"></param>
		/// <param name="strLimitDay"></param>
		void redThenDayLimitOver( int intRow, string strLimitDay )
		{
			if( strLimitDay == null ) return;
			//現在値を取得
			string stringNow = dataGridViewEx1.Rows[intRow].Cells[(int)EnumMainte1LineData.now].Value.ToString();
			if( stringNow == null ) return;
			if( stringNow.Contains( "/" ) ) {
				//日時
				switch( strLimitDay.CompareTo( stringNow ) ) {
					case -1://古い/小さい
					case 0://同じ
						dataGridViewEx1.Rows[intRow].DefaultCellStyle.BackColor = Color.Red;
						break;
					case 1://新しい/大きい
						dataGridViewEx1.Rows[intRow].DefaultCellStyle.BackColor = ECNC3Color.StandardBack;
						break;
				}
			} else {
				//値
				if( strLimitDay == "" ) return;
				if( stringNow == "" ) return;
				int intLimitData = int.Parse( strLimitDay );
				int intNow = int.Parse( stringNow );

				if( intLimitData <= intNow ) dataGridViewEx1.Rows[intRow].DefaultCellStyle.BackColor = Color.Red;
				else dataGridViewEx1.Rows[intRow].DefaultCellStyle.BackColor = ECNC3Color.StandardBack;
			}
		}
		#endregion
		#region<Maintenanse.xml：読込み/書込み>
		/// <summary>
		/// Maintenanse.xml：読込み
		/// </summary>
		/// <param name="loopCont"></param>
		void MaintenanseXML_Read( ref int loopCont )
		{
			try {
                using (FileMaintenanceTimer maintTimer = new FileMaintenanceTimer())
                {
                    maintTimer.Read();
                    loopCont = maintTimer.Items.Count;
                    _stMainte1LineDat = null;//読み込むデータ
                    for (int intLoop = 0; intLoop < loopCont; intLoop++)
                    {
                        //要素数を増やす
                        Array.Resize(ref _stMainte1LineDat, intLoop + 1);

                        _stMainte1LineDat[intLoop].index = maintTimer.Items[intLoop].Number;
                        _stMainte1LineDat[intLoop].itemName = maintTimer.Items[intLoop].Name;
                        _stMainte1LineDat[intLoop].life = maintTimer.Items[intLoop].Limit.ToString();
                        string unitTemp = MaintenanceTimerCategory.NULL.ToString();
                        switch(maintTimer.Items[intLoop].Category)
                        {
                            case MaintenanceTimerCategory.PowerON: unitTemp = "運転時間"; break;
                            case MaintenanceTimerCategory.DischargeON: unitTemp = "放電時間"; break;
                            case MaintenanceTimerCategory.DateTime: unitTemp = "期間"; break;
                        }
                        _stMainte1LineDat[intLoop].unit = unitTemp;
                        _stMainte1LineDat[intLoop].now = maintTimer.Items[intLoop].Value.ToString();
                        if (_stMainte1LineDat[intLoop].life.Contains("/"))
                        {
                            //寿命に"/"が有る場合、現在の日時を入れます。
                            _stMainte1LineDat[intLoop].now = getNowTime();//現在時間：yyyy/MM/ddで取得
                        }
                    }
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
			}
		}
		/// <summary>
		///  Maintenanse.xml：書込み
		/// </summary>
		private void MaintenanseXML_Write()
		{
			try {
                using (FileMaintenanceTimer maintTimer = new FileMaintenanceTimer())
                {
                    maintTimer.Read();
                    ulong mainPowerTime = maintTimer.Items.MainPowerCount;
                    ulong processPowerTime = maintTimer.Items.ProcessPowerCount;
                    maintTimer.Items.Clear();
                    maintTimer.Items.Dispose();
                    maintTimer.Items = null;
                    maintTimer.Items = new StructureMaintenanceTimerList();
                    maintTimer.Items.MainPowerCount = mainPowerTime;
                    maintTimer.Items.ProcessPowerCount = processPowerTime;
                    for (int intCount = 0; intCount < dataGridViewEx1.RowCount; intCount++)
                    {
                        StructureMaintenanceTimerItem addItem = new StructureMaintenanceTimerItem();
                        addItem.Number = int.Parse(dataGridViewEx1[(int)EnumMainte1LineData.index, intCount].Value.ToString());
                        addItem.Name = dataGridViewEx1[(int)EnumMainte1LineData.itemName, intCount].Value.ToString();
                        addItem.Limit = ulong.Parse((dataGridViewEx1[(int)EnumMainte1LineData.life, intCount].Value.ToString().Contains("/") == true)
                            ? dataGridViewEx1[(int)EnumMainte1LineData.life, intCount].Value.ToString().Replace("/", "") 
                            : dataGridViewEx1[(int)EnumMainte1LineData.life, intCount].Value.ToString());

                        addItem.Value = ulong.Parse((dataGridViewEx1[(int)EnumMainte1LineData.nowSec, intCount].Value.ToString().Contains("/") == true)
                            ? dataGridViewEx1[(int)EnumMainte1LineData.nowSec, intCount].Value.ToString().Replace("/", "")
                            : dataGridViewEx1[(int)EnumMainte1LineData.nowSec, intCount].Value.ToString());

                        MaintenanceTimerCategory maintCatTemp = MaintenanceTimerCategory.NULL;
                        string maintCatStringTemp = "";
                        switch (dataGridViewEx1[(int)EnumMainte1LineData.unit, intCount].Value.ToString())
                        {
                            case "運転時間": maintCatStringTemp = MaintenanceTimerCategory.PowerON.ToString(); break;
                            case "放電時間": maintCatStringTemp = MaintenanceTimerCategory.DischargeON.ToString(); break;
                            case "期間": maintCatStringTemp = MaintenanceTimerCategory.DateTime.ToString(); break;
                        }
                        foreach (MaintenanceTimerCategory maintCat in Enum.GetValues(typeof(MaintenanceTimerCategory)).Cast<MaintenanceTimerCategory>().ToList())
                        {
                            if(maintCat.ToString() == maintCatStringTemp)
                            {
                                maintCatTemp = maintCat;
                            }
                        }
                        addItem.Category = maintCatTemp;
                        maintTimer.Items.Add((StructureMaintenanceTimerItem)(addItem.Clone()));
                        addItem.Dispose();
                        addItem = null;
                    }
                    maintTimer.Write();
                }
			} catch( System.Xml.XmlException ex ) {
				//XMLによる例外をキャッチ
				Console.WriteLine( ex.Message );
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
        #region<時間：yyyy/MM/ddで取得>
        /// <summary>
        /// 今日の日(時間)：yyyy/MM/ddで取得
        /// </summary>
        /// <returns></returns>
        private string getNowTime()
		{
			DateTime dtNow = DateTime.Now;//現在時刻
			return dtNow.ToShortDateString();//日本の例、2002/05/12
		}
        /// <summary>
        /// 次の日を取得
        /// </summary>
        /// <returns></returns>
        private string getNextDay()
        {
            DateTime dtNow = DateTime.Now;//現在日（時刻）
            DateTime dtNext = dtNow.AddDays(1);//次の日
            return dtNext.ToShortDateString();//日本の例、2002/05/12
        }
        #endregion
        #region<ボタン：クリック>
        /// <summary>
        /// 行追加ボタン：クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEx_LineAdd_Click( object sender, EventArgs e )
		{
            if (dataGridViewEx1.CurrentCell == null) return;
            int rowIndex = dataGridViewEx1.CurrentCell.RowIndex;
			int colIndex = dataGridViewEx1.CurrentCell.ColumnIndex;
            if (rowIndex < 0) return;
            if (colIndex < 0) return;
            dataGridViewEx1.Rows.Insert( rowIndex + 1, 1 );
			dataGridViewEx1.Rows[rowIndex + 1].Cells[0].Value = rowIndex + 1;//インデックス
			dataGridViewEx1.Rows[rowIndex + 1].Cells[1].Value = "";         //アイテム名
			dataGridViewEx1.Rows[rowIndex + 1].Cells[2].Value = "1";        //寿命
			dataGridViewEx1.Rows[rowIndex + 1].Cells[3].Value = "運転時間"; //単位
			dataGridViewEx1.Rows[rowIndex + 1].Cells[4].Value = "0";        //現在
			dataGridViewEx1.Rows[rowIndex + 1].Cells[5].Value = "0";        //現在(秒)
			dataGridViewEx1.CurrentCell = dataGridViewEx1[0, rowIndex + 1];//下行を現在のセルにする
			indexRealloc( dataGridViewEx1.Rows.Count ); //インデックス再度割り当て
			dataGridViewEx1.EndEdit();                  //これが無いと表示が更新されません。
			dataGridViewEx1.Refresh();                  //再描画
			MaintenanseXML_Write();                     //ファイルに書込み
		}
		/// <summary>
		/// 行削除：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonEx_LineDel_Click( object sender, EventArgs e )
		{
            if (dataGridViewEx1.CurrentCell == null) return;
            int rowIndex = dataGridViewEx1.CurrentCell.RowIndex;
            int colIndex = dataGridViewEx1.CurrentCell.ColumnIndex;
            if (rowIndex < 0) return;
            if (colIndex < 0) return;
            //選択されている行を削除します。&#xD;&#xA;よろしいですか？
            using ( MessageDialog msg = new MessageDialog() ) {
				if( false == msg.Question( 5517, this ) ) return;
			}
			dataGridViewEx1.Rows.RemoveAt( rowIndex );  //行削除
			int rowMax = dataGridViewEx1.Rows.Count;    //行数取得
			indexRealloc( rowMax );                     //インデックス再度割り当て
			dataGridViewEx1.EndEdit();                  //これが無いと表示が更新されません。
			dataGridViewEx1.Refresh();                  //再描画
			MaintenanseXML_Write();                     //ファイルに書込み
		}
		/// <summary>
		/// インデックス再度割り当て
		/// </summary>
		/// <param name="rowMax"></param>
		private void indexRealloc( int rowMax )
		{
			for( int intCount = 0 ; intCount < rowMax ; intCount++ ) {
				dataGridViewEx1.Rows[intCount].Cells[0].Value = intCount + 1;//インデックス再度割り当て
			}
		}
		/// <summary>
		/// リセットボタン：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonEx_Reset_Click( object sender, EventArgs e )
		{
            if (dataGridViewEx1.CurrentCell == null) return;
            int rowIndex = dataGridViewEx1.CurrentCell.RowIndex;
            int colIndex = dataGridViewEx1.CurrentCell.ColumnIndex;
            if (rowIndex < 0) return;
            if (colIndex < 0) return;
            //選択されている行の現在値をリセットします。
            using ( MessageDialog msg = new MessageDialog() ) {
				if( false == msg.Question( 5516, this ) ) return;
			}
			//dataGridViewEx1[(int)EnumMainte1LineData.itemName, rowIndex].Value = "";    //アイテム名
			//dataGridViewEx1[(int)EnumMainte1LineData.life, rowIndex].Value = 1;         //寿命
			//dataGridViewEx1[(int)EnumMainte1LineData.unit, rowIndex].Value = "運転時間";//単位
			dataGridViewEx1[(int)EnumMainte1LineData.now, rowIndex].Value = 0;          //現在
			dataGridViewEx1[(int)EnumMainte1LineData.nowSec, rowIndex].Value = 0;       //現在(秒)
            //背景色：リセット
            redThenDayLimitOver(rowIndex, "1");
            dataGridViewEx1.EndEdit();//これが無いと表示が更新されません。
			dataGridViewEx1.Refresh();//再描画
			MaintenanseXML_Write();//ファイルに書込み
            dataGridViewEx1.Refresh();//再描画
        }
		#endregion
		#region<セル：クリック>
		/// <summary>
		/// セルマウス：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dataGridViewEx1_CellMouseClick( object sender, DataGridViewCellMouseEventArgs e )
		{
            if (e.RowIndex < 0)     return;
            if (e.ColumnIndex < 0)  return;
            int rowIndex = e.RowIndex;
			int colIndex = e.ColumnIndex;
			_rowIndex = rowIndex;
			_colIndex = colIndex;

			switch( (EnumMainte1LineData)colIndex ) {
				case EnumMainte1LineData.index: break;  //インデックス
				case EnumMainte1LineData.itemName:      //アイテム名
					//ポップアップキーボードがあるか？
					if( null != _PopupKeybord ) {
						this.Controls.Remove( _PopupKeybord );
						_PopupKeybord.Dispose();
						_PopupKeybord = null;
					}
					if( _PopupKeybord == null ) {
						//ポップアップ・キーボード表示
						_PopupKeybord = new StandardKeyBord();
						_PopupKeybord.Location = new System.Drawing.Point( 0, 340 );//500から340に変更：柏原
						_PopupKeybord.NotifyReturn = PopupKeybord_OnNotifyReturn; //イベント通知：OK
						this.Controls.Add( _PopupKeybord );
						//dataGridViewEx1.Columns[(int)EnumMainte1LineData.itemName].ReadOnly = false;//アイテム名：使用
						dataGridViewEx1.Columns[(int)EnumMainte1LineData.life].ReadOnly = true;     //寿命
						dataGridViewEx1.Columns[(int)EnumMainte1LineData.unit].ReadOnly = true;     //単位
					}
					break;
				case EnumMainte1LineData.life:  //寿命
				case EnumMainte1LineData.now:   //現在
					//アイテム名が入力されている場合
					if( dataGridViewEx1.Columns[(int)EnumMainte1LineData.life].ReadOnly == true ) {
						return;//ポップアップ・キーボードが編集できる不具合
					}
					popupTenkeyOn( sender, rowIndex, colIndex );//ポップアップテンキー：表示
					break;
				case EnumMainte1LineData.unit://単位
                    if (dataGridViewEx1.Columns[(int)EnumMainte1LineData.life].ReadOnly == true) return;//ポップアップ・キーボードが編集できる不具合
                    string beforeString = dataGridViewEx1[colIndex, rowIndex].Value.ToString();//変更前
					if( beforeString == "" ) {
						dataGridViewEx1.EndEdit();//編集できてしまうので、再度考察
						return;
					}
					string afterString = "";
					string[] stringUnits = { "運転時間", "放電時間", "期間" };
					Popup.ReturnMessage retMessage = Popup.SelectCommandsDialogSimple.ShowSubForm( this, stringUnits[0], stringUnits[1], stringUnits[2] );
					switch( retMessage ) {
						case Popup.ReturnMessage.ExecuteA1: afterString = stringUnits[0]; break;//運転時間
						case Popup.ReturnMessage.ExecuteA2: afterString = stringUnits[1]; break;//放電時間
						case Popup.ReturnMessage.ExecuteA3: afterString = stringUnits[2]; break;//期間
                        case Popup.ReturnMessage.Cancel: return;
					}
					int intMes = 0;
					if( beforeString == stringUnits[0] && afterString == stringUnits[2] ) {
						intMes = 1;
					} else
					if( beforeString == stringUnits[1] && afterString == stringUnits[2] ) {
						intMes = 1;
					} else
					if( beforeString == stringUnits[2] && afterString == stringUnits[0] ) {
						intMes = 2;
					} else
					if( beforeString == stringUnits[2] && afterString == stringUnits[1] ) {
						intMes = 2;
					}
					if( intMes > 0 ) {
						using( MessageDialog msg = new MessageDialog() ) {
							if( intMes == 1 ) {
								if( false == msg.Question( 5513, this ) ) {//"寿命の時間を失い、初期設定の期間に変わります。&#xD;&#xA;よろしいですか？"
									return;
								}
                                //実行
                                string nextTime = getNextDay();//時間：yyyy/MM/ddで取得
                                dataGridViewEx1[(int)EnumMainte1LineData.life, rowIndex].Value = nextTime; //寿命：リセット
                                string nowTime = getNowTime();//時間：yyyy/MM/ddで取得
								dataGridViewEx1[(int)EnumMainte1LineData.now, rowIndex].Value = nowTime;   //現在：リセット
							}else if( intMes == 2 ) {
								if( false == msg.Question( 5514, this ) ) {//"寿命の日付を失い、初期設定の運転/放電時間に変わります。&#xD;&#xA;よろしいですか？"
									return;
								}
								//実行
								dataGridViewEx1[(int)EnumMainte1LineData.life, rowIndex].Value = 1; //寿命：リセット
								dataGridViewEx1[(int)EnumMainte1LineData.now,  rowIndex].Value = 0; //現在：リセット
							}
						}
					}
					switch( retMessage ) {
						case Popup.ReturnMessage.ExecuteA1: dataGridViewEx1[colIndex, rowIndex].Value = "運転時間"; break;//運転時間
						case Popup.ReturnMessage.ExecuteA2: dataGridViewEx1[colIndex, rowIndex].Value = "放電時間"; break;//放電時間
						case Popup.ReturnMessage.ExecuteA3: dataGridViewEx1[colIndex, rowIndex].Value = "期間"; break;//期間
					}
					////コンボボックスの場合、リストが3回クリックしないと開かないのでSendKeys.Send( "{F4}" )で対応
					//SendKeys.Send( "{F4}" );
					//ファイルに書込み
					MaintenanseXML_Write();
					break;
				default:
					break;
			}
			dataGridViewEx1.EndEdit();//これが無いと表示が更新されません。
			dataGridViewEx1.Refresh();//再描画
		}
		#endregion
		#region<ポップアップ・キーボード>
		/// ポップアップ・キーボード
		private StandardKeyBord _PopupKeybord = null;
		/// <summary>
		/// キーボード「閉じる」ボタン：押された
		/// </summary>
		private void PopupKeybord_OnNotifyReturn()
		{
			if( _colIndex == (int)EnumMainte1LineData.itemName ) {

				if( dataGridViewEx1[(int)EnumMainte1LineData.life, _rowIndex].Value.ToString() == "" ) {
					dataGridViewEx1[(int)EnumMainte1LineData.life, _rowIndex].Value = 1;
					dataGridViewEx1[(int)EnumMainte1LineData.unit, _rowIndex].Value = "運転時間";
					dataGridViewEx1[(int)EnumMainte1LineData.now, _rowIndex].Value = 0;
				}
			}
			//下記列を使用可能に戻す
			dataGridViewEx1.Columns[(int)EnumMainte1LineData.life].ReadOnly = false;     //寿命
			dataGridViewEx1.Columns[(int)EnumMainte1LineData.unit].ReadOnly = false;     //単位
			dataGridViewEx1.EndEdit();  //これが無いと表示が更新されません。
			dataGridViewEx1.Refresh();  //再描画
			MaintenanseXML_Write();     //ファイルに書込み
		}
		#endregion
		#region<ポップアップ・テンキー>
		/// <summary>ポップアップテンキー</summary>
		private TenKeyDialog _popupTenkey = null;
		/// <summary>
		/// 記録する加工条件値
		/// </summary>
		/// <summary>
		object _controlName = "";//記録するコントロール名
		/// ポップアップテンキー：表示/非表示
		/// </summary>
		/// <param name="objControlName">コントロール名</param>
		/// <param name="rowIndex">コントロール名</param>
		/// <param name="colIndex">コントロール名</param>
		/// </summary>
		private void popupTenkeyOn( object objControlName, int rowIndex, int colIndex )
		{
			if( _popupTenkey != null ) {
				_popupTenkey.Close();           //画面を閉じる
				_popupTenkey.Dispose();         //破棄
				_popupTenkey = null;            //null初期化
			}
			//初期値設定
			_controlName = objControlName;      //記録するコントロール名
			string titleString = "";            //ウインドタイトルデフォルト
			string changeVal = "";              //編集値デフォルト
			Decimal lowerLimitDec = 0;          //最小デフォルト
			Decimal upperLimitDec = 0;          //最大デフォルト
			string strTenkeyMode = "";          //UITenkeyModeAndPosRec.xmlより表示位置、表示モードを取得
			NumericTextBox.FormatTypes formatType = NumericTextBox.FormatTypes.Integer1;//フォーマットタイプ
			bool firstValueSelect = true;       //初回文字列選択= true
			bool realValueEdit = true;          //実数編集		= true
			bool oneKetaEditImpossible = true;  //下1桁編集不可	= true
												//初期値設定を変更します
			setCodeForControl(
				objControlName,                 //コントロール名
				rowIndex,
				colIndex,
				ref titleString,                //ウインドタイトル
				ref changeVal,                  //編集文字列
				ref lowerLimitDec,              //最小値
				ref upperLimitDec,              //最大値
				ref strTenkeyMode,              //UITenkeyModeAndPosRec.xmlより表示位置、表示モードを取得
				ref formatType,                 //フォーマットタイプ
				ref firstValueSelect,           //初回文字列選択	= true
				ref realValueEdit,              //実数編集			= true
				ref oneKetaEditImpossible       //下1桁編集不可		= true
			);
			if( changeVal == "" ) return;
			//ポップアップTenKey：
			_popupTenkey = new TenKeyDialog(
				changeVal,                      //このコントロールで表示する文字
				formatType,                     //NumericTextBoxの編集フォーマットタイプ
				lowerLimitDec,                  //最小値
				upperLimitDec,                  //最大値
				firstValueSelect,               //true=初回文字列選択
				realValueEdit,                  //true=実数編集
				oneKetaEditImpossible,          //true=下1桁編集不可
				strTenkeyMode                   //テンキー表示モード：UITenkeyModeAndPosRec.xmlに記述された表示位置やモードで表示
			);
			_popupTenkey.NotifyReturnOk = popupTenkey_OnNotifyReturnOk; //イベント通知：OK
			_popupTenkey.Text = titleString;                            //テンキータイトル表示
			_popupTenkey.ShowDialog( this );                            //画面を開く:
			_popupTenkey.Dispose();                                     //破棄
			_popupTenkey = null;                                        //null初期化
		}
		//記録するDGVEXの行列位置
		private int _rowIndex = 0;
		private int _colIndex = 0;
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
			int rowIndex,
			int colIndex,
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
			_rowIndex = rowIndex;
			_colIndex = colIndex;
			switch( colIndex ) {
				//各クリックされたセル値を取得
				case (int)EnumMainte1LineData.life: //寿命
					titleString = dataGridViewEx1.Columns[(int)EnumMainte1LineData.life].HeaderCell.Value.ToString();//ウインドタイトル
					if( dataGridViewEx1.CurrentCell.Value == null ) return;
					changeVal = dataGridViewEx1.CurrentCell.Value.ToString();   //編集文字列
					lowerLimitDec = (decimal)0;                                 //最小値
					if( changeVal.Contains( "/" ) ) {
						//日付
						changeVal = changeVal.Replace( "/", "" );               //区切り文字削除
						upperLimitDec = (decimal)99999999;                      //最大値
						formatType = NumericTextBox.FormatTypes.Integer8;       //フォーマットタイプ
					} else {
						//値
						upperLimitDec = (decimal)99999;                         //最大値
						formatType = NumericTextBox.FormatTypes.Integer5;       //フォーマットタイプ
					}
					//表示場所＆表示モード
					//strTenkeyMode = "ThinLineSet_ZUpIncrease";				//UITenkeyModeAndPosRec.xmlより表示位置、表示モードを取得
					break;

				case (int)EnumMainte1LineData.now:  //現在値
					titleString = dataGridViewEx1.Columns[(int)EnumMainte1LineData.now].HeaderCell.Value.ToString();//ウインドタイトル
					if( dataGridViewEx1.CurrentCell.Value == null ) return;
					changeVal = dataGridViewEx1.CurrentCell.Value.ToString();   //編集文字列
					lowerLimitDec = (decimal)0;                                 //最小値
					if( changeVal.Contains( "/" ) ) {
						//日付
						changeVal = changeVal.Replace( "/", "" );               //区切り文字削除
						upperLimitDec = (decimal)99999999;                      //最大値
						formatType = NumericTextBox.FormatTypes.Integer8;       //フォーマットタイプ
					} else {
						//値
						upperLimitDec = (decimal)99999;                         //最大値
						formatType = NumericTextBox.FormatTypes.Integer5;       //フォーマットタイプ
					}
					//表示場所＆表示モード
					//strTenkeyMode = "ThinLineSet_ZUpIncrease";				//UITenkeyModeAndPosRec.xmlより表示位置、表示モードを取得
					break;

			}
			firstValueSelect = true;        //初回文字列選択	= true
			realValueEdit = false;          //実数編集			= true
			oneKetaEditImpossible = false;  //下1桁編集不可		= true
		}

		private Color _BeforeColor = Color.Black;
		/// <summary>
		/// ポップアップテンキーで「OK」ボタンを押した時、入力値をこのクラスに反映
		/// </summary>
		private void popupTenkey_OnNotifyReturnOk()
		{
			string retVal = _popupTenkey._tenkeyValReturn;  //ポップアップテンキーで編集された値
			switch( _colIndex ) {                           //記録していたコントロール値
				case (int)EnumMainte1LineData.life: //寿命
				case (int)EnumMainte1LineData.now:  //現在
					string selCombo = dataGridViewEx1.Rows[_rowIndex].Cells[(int)EnumMainte1LineData.unit].Value.ToString();
					switch( selCombo ) {
						case "期間":
							if( retVal.Length != 8 ) {
								//title="入力フォーマットエラー" txt="年月日(yyyyMMdd)形式で入力して下さい。"
								using( MessageDialog msg = new MessageDialog() ) {
									msg.Error( 5515, this ); return;
								}
							}
							//下記は日本仕様
							string strYear = retVal.Substring( 0, 4 ); //yyyy
							string strMonth = retVal.Substring( 4, 2 ); //MM
							string strDay = retVal.Substring( 6, 2 ); //dd
							//存在する日付かチェック
							string stringDate = strYear + "/" + strMonth + "/" + strDay;   // チェックする日付を設定
							DateTime dt1;
							if( DateTime.TryParse( stringDate, out dt1 ) ) {
							} else {
								//存在しない日付
								using( MessageDialog msg = new MessageDialog() ) {
									msg.Error( 5512, this );
									{
										return;//"入力範囲外" txt="この日付では設定できません、&#xD;&#xA;他の日付に変更して下さい。
									}
								}
							}
							string stringYearMonthDay = dt1.ToShortDateString();
							//現在値を取得
							string stringNow = dataGridViewEx1.Rows[_rowIndex].Cells[(int)EnumMainte1LineData.now].Value.ToString();
							redThenDayLimitOver( _rowIndex, stringYearMonthDay );
							dataGridViewEx1.CurrentCell.Value = stringYearMonthDay;//カレントセルに入れる
							dataGridViewEx1.Refresh();  //再描画
							MaintenanseXML_Write();     //ファイルに書込み
							break;
						default://運転時間、放電時間
							int intNow = int.Parse( dataGridViewEx1[(int)EnumMainte1LineData.now, _rowIndex].Value.ToString() );
							int intRetVal = int.Parse( retVal );
							decimal decVal = decimal.Parse(retVal) * 60 * 60;//時-＞秒
							dataGridViewEx1[_colIndex,   _rowIndex].Value = retVal;//カレントセルに入れる
                            switch(_colIndex)
                            {
                                case (int)EnumMainte1LineData.life: break;

                                case (int)EnumMainte1LineData.now:
                                    dataGridViewEx1[(int)EnumMainte1LineData.nowSec, _rowIndex].Value = decVal;//セルに入れる
                                    break;
                            }
							string strLife = dataGridViewEx1[(int)EnumMainte1LineData.life, _rowIndex].Value.ToString();
							redThenDayLimitOver( _rowIndex, strLife );
							dataGridViewEx1.Refresh();  //再描画
							MaintenanseXML_Write();     //ファイルに書込み
							break;
					}
					break;
			}
		}
		#endregion
	}
}
