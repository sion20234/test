///////////////////////////////////////////////////////////////////////////////////////////
//
// (1) システム名称 : ELENIX ECNCⅢ
// (2) ファイル名   : MacroVarSetForm.cs
// (3) 概要         : マクロ変数設定画面
// (4) 作成日       : 2015.06.01
// (5) 作成者       : 八野
// (6) 特記事項     : VisualStudio2015 C#
// (7) 更新履歴     : 2017.03.14：柏原
//
///////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Drawing;
using System.Windows.Forms;
using ECNC3.Models;
using ECNC3.Models.McIf;//McDatMacroManage
using ECNC3.Enumeration;
using System.IO;
using ECNC3.Views.Popup;

namespace ECNC3.Views
{
    /// <summary>
    /// マクロ変数設定画面
    /// </summary>
    public partial class MacroVarSetForm : ECNC3Form
    {
        #region <region<このクラスの初期化>region>
        //private ValiableFileForm ValFile;
        /// <summary>ポップアップキーボード</summary>
        private KeyboardDialog _popupKeyboard = null;
        /// <summary>ポップアップテンキー</summary>
        private TenKeyDialog _popupTenkey = new TenKeyDialog("", 0, 0, 0);	//初回インスタンスを作っておく
		private string PopupTitleFind = "番号検索";							//タイトル：検索番号
		private string PopupTitleData = "";									//タイトル：変数値
		private string PopupTitleComment = "";								//タイトル：備考
		///<summary>残データー読み込み用時間差タイマー</summary>
		private System.Threading.Timer _TimerAfterNcData = null;			//NC読み込み
		private System.Threading.Timer _TimerAfterFileData = null;			//ファイル読み込み
		private const int constWaitTime = 500;
		private int timerSpeed = 500;
        ///<summary>データセットする行数(インデックス)NC/File両方</summary>
        private int _intCount = 0;  //NC/File両方
        private int _startCount = 0;//NC/File両方
        private int _endCount = 0;  //NC/File両方
        ///<summary>データセットする行数(インデックス)NC</summary>
        private int _startCountNc = 0;//NC
        private int _endCountNc = 0;//NC
        ///<summary>データセットする行数(インデックス)File</summary>
        private int _startCountFile = 0;//File
        private int _endCountFile = 0;//File
        private string _macroFileName = "";

        /// <summary>
        /// フォーム　コンストラクタ
        /// </summary>
        public MacroVarSetForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// フォーム　ロード時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MacroVarSetForm_Load(object sender, EventArgs e)
        {
            //不具合修正：区切りラインが表示しない対応
            //panel_sectionLine.BringToFront();
            //panel_sectionLine.SendToBack();
            // 元のカーソルを保持
            Cursor preCursor = Cursor.Current;
            // カーソルを待機カーソルに変更
            Cursor.Current = Cursors.WaitCursor;
            //dataGridViewEx1：ヘッダ色や大きさ設定
            dataGridViewEx1.Initialize(12.0F, 30, true);//true=編集不可
            //dataGridViewEx1：列ヘッダー設定
            DataGridViewColumn _numberCol = new DataGridViewColumn();
            _numberCol.CellTemplate = new DataGridViewTextBoxCell();
            _numberCol.Name = "NumberCol";
            _numberCol.HeaderText = "番号";
            _numberCol.Width = 90;
			DataGridViewColumn _dataCol = new DataGridViewColumn();
            _dataCol.CellTemplate = new DataGridViewTextBoxCell();
            _dataCol.Name = "VariableCol";
            _dataCol.HeaderText = "変数値";
            _dataCol.Width = 150;
			PopupTitleData = _dataCol.HeaderText;
			DataGridViewColumn _commentCol = new DataGridViewColumn();
            _commentCol.CellTemplate = new DataGridViewTextBoxCell();
            _commentCol.Name = "CommentCol";
            _commentCol.HeaderText = "備考";
            _commentCol.Width = 300;
			PopupTitleComment = _commentCol.HeaderText;
			//dataGridViewEx1：列ヘッダーをdataGridViewEx1に追加
			dataGridViewEx1.Columns.Add(_numberCol);
            dataGridViewEx1.Columns.Add(_dataCol);
            dataGridViewEx1.Columns.Add(_commentCol);
            //dataGridViewEx1：列ヘッダー：フォントや色設定
            dataGridViewEx1.InitCol("NumberCol",  12.0F, DataGridViewContentAlignment.MiddleLeft, typeof( string ) );
            dataGridViewEx1.InitCol("VariableCol",12.0F, DataGridViewContentAlignment.MiddleRight,typeof(string) );
            dataGridViewEx1.InitCol("CommentCol", 12.0F, DataGridViewContentAlignment.MiddleLeft, typeof( string ) );
            //dataGridViewEx：行数設定
            dataGridViewEx1.RowCount = 90000;//1-90000
            //dataGridViewEx1.RowCount = 100001;//1-100000
            //dataGridViewEx1：各種設定
            dataGridViewEx1.AutoSize = false;
			dataGridViewEx1.ReadOnly = true;
            dataGridViewEx1.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
			dataGridViewEx1.MultiSelect = false;				//複数選択＝不可
			dataGridViewEx1.AllowUserToAddRows = false;			//追加不可
			dataGridViewEx1.AllowUserToDeleteRows = false;	    //削除不可
            dataGridViewEx1_Enable(false);                      //初回セル使用不可
            dataGridViewEx1.Columns[2].AutoSizeMode
                = DataGridViewAutoSizeColumnMode.Fill;
            //dataGridViewEx1.RowHeadersVisible = true;//固定列：表示
            //トラックバー設定
            numericTextBox1.Text = "0";
			//textBoxEx1.Text = "0";
			//タイマ－設定
			_TimerAfterNcData = new System.Threading.Timer(
							  new System.Threading.TimerCallback( timerFireNcRead ), null, System.Threading.Timeout.Infinite, 500 );//NCデータ読み込み
			_TimerAfterFileData = new System.Threading.Timer(
                              new System.Threading.TimerCallback(timerFireRead), null, System.Threading.Timeout.Infinite, 500);//ファイルデータ読み込み
			Cursor.Current = preCursor;					//カーソルを元に戻す 
		}
        #endregion
        #region <region<このクラス終了時>region>
        /// <summary>
        /// 閉じる　ボタン　クリック時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackUserFuncFormBt_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// フォーム：閉じる時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MacroVarSetForm_FormClosed(object sender, FormClosedEventArgs e)
        {
			//タイマー破棄
			if( null != _TimerAfterNcData ) {
				_TimerAfterNcData.Dispose();
				_TimerAfterNcData = null;
			}
			if( null != _TimerAfterFileData ) {
				_TimerAfterFileData.Dispose();
				_TimerAfterFileData = null;
			}
			//ポップアップテンキー破棄
			if( null != _popupTenkey)
            {
                _popupTenkey.Close();
                _popupTenkey = null;
            }
            //ポップアップ・キーボード破棄
            if (null != _popupKeyboard)
            {
                _popupKeyboard.Close();
                _popupKeyboard = null;
            }
        }
        #endregion
        #region <region<トラックバー>region>
        /// <summary>
        /// トラックバーの設定：
        /// </summary>
        /// <param name="trackVal"></param>
        /// <returns>スクロールバー位置</returns>	
        //private int setTrackbar(int trackVal) {
        //   return trackBar1.Value = changeTrackPosToScPos(trackVal);//トラックバー位置をスクロール位置に変換
       // }
        /// <summary>
        /// トラックバー位置をスクロール位置に変換
        /// </summary>
        /// <param name="trackVal"></param>
        //private int changeTrackPosToScPos(int trackVal)
        //{
        //    return trackBar1.Maximum - trackVal;//SCと逆
        //}
        /// <summary>
        /// トラックバー移動時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /*private void trackbar_Scrol( object sender, EventArgs e )
		{
			int TrackValNow = ( (TrackBar)sender ).Value;
            //トラックバー位置からスクロール位置に変換
            int dspVal = changeTrackPosToScPos(TrackValNow);

            //とび先の番号セルがnullの場合、選択セル、検索番号、トラックバーを先頭にする
            if (dataGridViewEx1[0, dspVal].Value == null) {
                int intVal = _startCount;//先頭：#0/#10000：NC/File
                //選択セル移動
                cellRowMove(intVal);
                //検索番号の変更
                setSerch(intVal);
                //トラックバーの移動
                setTrackbar(intVal);
                return;
            }
             //指定行へセル移動
            cellRowMove(dspVal);
            //検索開始テキストに表示
            numericTextBox1.Text = dspVal.ToString();
		}*/
        #endregion
        #region <region<検索>region>
        /// <summary>
        /// 検索番号：テキストボックス：マウスダウン：「検索番号」ポップアップテンキーを表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numericTextBox1_MouseDown(object sender, MouseEventArgs e)
        {
			string objectString = ((NumericTextBox)sender).Text;
            popupTenkeyOnFind(objectString, PopupTitleFind);
        }
        /// <summary>
        /// //検索開始テキストに表示
        /// </summary>
        /// <param name="serchVal"></param>
        private void setSerch(int serchVal)
        {
            numericTextBox1.Text = serchVal.ToString();
        }
        /// <summary>
        /// 検索開始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startSerch(object sender, EventArgs e)
        {
            int rowIndexTpm = int.Parse(numericTextBox1.Text) ;//入力された数字
            int rowIndex = rowIndexTpm - 10000;//インデックス位置
            //とび先の番号セルがnull
            if (dataGridViewEx1[0, rowIndex].Value == null) return;

            //指定行へセル移動
            cellRowMove(rowIndex);
        }
        #endregion
        #region <region<ポップアップ・キーボード>region>
        /// <summary>
        /// ポップアップ・キーボード：表示文字列の更新要求
        /// </summary>
        /// <param name="text"></param>
        private void _edtComment_ClickFileTextChanged(string text)
        {
            dataGridViewEx1.CurrentCell.Value = text;
        }
        /// <summary>
        /// ポップアップ・キーボード：終了要求
        /// </summary>
        private void _edtComment_ClickCallBack()
        {
            ResetInput();
        }
        /// <summary>
        /// ポップアップ・キーボード：閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyboardClosed(object sender, FormClosedEventArgs e)
        {
            if (_popupKeyboard != null)
            {
                _popupKeyboard.FormClosed -= KeyboardClosed;
                _popupKeyboard = null;
            }
        }
        /// <summary>
        /// ポップアップ・キーボード：入力状態の初期化
        /// </summary>
        public void ResetInput()
        {
            if (null != _popupKeyboard)
            {
                _popupKeyboard.Close();
                _popupKeyboard = null;
            }
        }
		#endregion
		#region <region<ポップアップ：キーボード/テンキー>region>
		//0=検索,1=変数値,2=備考
		private int editKindNumber=0;
		/// <summary>
		/// ポップアップテンキー：「OK」ボタンを押した時、入力値をこのクラスに反映
		/// </summary>
		private void popupTenkey_OnNotifyReturnOk()
        {
            string retVal = _popupTenkey._tenkeyValReturn;//ポップアップテンキーで編集された値
			switch( editKindNumber ) {
				case 0://番号検索
                    //ポップアップテンキー値
                    int intRetVal = int.Parse(retVal);
                    //開始#が0/10000の10000の場合、_startCount以下のセル値はnullなので処理無し
                    if (_startCount > intRetVal) return;
                    //NumericTBに値をセット
                    numericTextBox1.Text = retVal;
                    //検索実行
                    startSerch( null, null );
					break;
				case 1://DGVExセル：変数値
 				    //カレントセルに値をセット
					dataGridViewEx1.CurrentCell.Value = retVal;//ポップアップキーボード値
                    break;
			}
		}
		#endregion
		#region <region<ポップアップテンキー>region>
		/// <summary>
		/// ポップアップテンキー：変数値：表示/非表示
		/// </summary>
		/// <param name="val"></param>
		/// <returns>false=上下ポップアップを表示</returns>
		private bool popupTenkeyOn(object val, string popupTitle, string popupTitleNumber)
        {//0=検索,1=変数値,2=備考
            editKindNumber = 1;
			string changeVal = "";	  //編集値
            Decimal lowerLimitDec = 0;//最小値
            Decimal upperLimitDec = 0;//最大値
            if (_popupTenkey != null)
            {
                _popupTenkey.Close();   //画面を閉じる
                _popupTenkey = null;    //null初期化
            }
            //クリックしたコントロールの値を取得
            changeVal = val.ToString();
            lowerLimitDec = (decimal)-999.999;
            upperLimitDec = (decimal)999.999;
            //フォーマットタイプ
            NumericTextBox.FormatTypes formatType = NumericTextBox.FormatTypes.SignDecimalUpper3Lower3;
            //ポップアップTenKey表示
            _popupTenkey = new TenKeyDialog(changeVal, formatType, lowerLimitDec, upperLimitDec, true, true);
			_popupTenkey.NotifyReturnOk = popupTenkey_OnNotifyReturnOk; //イベント通知：OK
			_popupTenkey.Text = popupTitle + " " + popupTitleNumber;  //テンキータイトル表示
			_popupTenkey.ShowDialog(this);                              //画面を開く
            return true;
        }
		/// <summary>
		/// ポップアップテンキー：検索：表示/非表示
		/// </summary>
		/// <param name="val"></param>
		/// <returns>false=上下ポップアップを表示</returns>
		private bool popupTenkeyOnFind( object val, string popupTitle )
        {//0=検索,1=変数値,2=備考
            editKindNumber = 0;
			string changeVal = "";    //編集値
			Decimal lowerLimitDec = 0;//最小値
			Decimal upperLimitDec = 0;//最大値
			if( _popupTenkey != null ) {
				_popupTenkey.Close();   //画面を閉じる
				_popupTenkey = null;    //null初期化
			}
			//クリックしたコントロールの値を取得
			changeVal = val.ToString();
            lowerLimitDec = (decimal)1;
            upperLimitDec = (decimal)(_startCount + _endCount - 1);
			//フォーマットタイプ
			NumericTextBox.FormatTypes formatType = NumericTextBox.FormatTypes.Integer5;
			//ポップアップTenKey表示
			_popupTenkey = new TenKeyDialog( changeVal, formatType, lowerLimitDec, upperLimitDec,true );
			_popupTenkey.NotifyReturnOk = popupTenkey_OnNotifyReturnOk; //イベント通知：OK
			_popupTenkey.Text = popupTitle;                             //テンキータイトル表示
			_popupTenkey.ShowDialog( this );                              //画面を開く
			return true;
		}
        #endregion
        #region <region<キャレット制御/描画時>region>
        private int EventCout = 0;//イベント回数
		/// <summary>
		/// 描画時
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MacroVarSetForm_Paint( object sender, PaintEventArgs e )
		{
			if( EventCout < 2 )//実際いらないかも、ポップアップを出す目印にはなる
			{
				this.Focus();
				numericTextBox1.Focus();
				numericTextBox1.SelectAll();
				EventCout++;
			}
		}
        #endregion
        #region <region<データグリッド：dataGridViewEx1>region>
        /// <summary>
        /// データグリッド：使用可能/不可
        /// </summary>
        /// <param name="boolVal"></param>
        private void dataGridViewEx1_Enable(bool boolVal)
        {
            if (boolVal)
            {
                dataGridViewEx1.Enabled = true;
                dataGridViewEx1.CurrentCell = dataGridViewEx1[1, 0];
            }
            else
            {
                dataGridViewEx1.CurrentCell = null;
                dataGridViewEx1.ClearSelection();
                dataGridViewEx1.Enabled = false;
            }
        }
        /// <summary>
        /// 指定行セルに移動
        /// </summary>
        /// <param name="rowIndex"></param>
        private void cellRowMove(int rowIndex)
        {
            if (dataGridViewEx1.CurrentCell == null) return;
            int colIndex = dataGridViewEx1.CurrentCell.ColumnIndex;//列
            if (rowIndex < 0) rowIndex = 0;
            //if (rowIndex > trackBar1.Maximum) rowIndex = trackBar1.Maximum;//99999
            //指定行へセル移動
            dataGridViewEx1.CurrentCell = dataGridViewEx1[colIndex, rowIndex];
        }
        /// <summary>
        /// 指定列セルに移動
        /// </summary>
        /// <param name="colIndex"></param>
        private void cellColMove(int colIndex)
        {
			if( dataGridViewEx1.CurrentCell == null ) return;

			int rowIndex = dataGridViewEx1.CurrentCell.RowIndex;//行
            if (colIndex < 1) colIndex = 1;
            if (colIndex > 2) colIndex = 2;
            //指定行へセル移動
            dataGridViewEx1.CurrentCell = dataGridViewEx1[colIndex, rowIndex];
        }
        /// <summary>
        /// dataGridViewExセル：マウス：ダウン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewEx1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = ((DataGridViewCellMouseEventArgs)e).RowIndex;      //行
            int colIndex = ((DataGridViewCellMouseEventArgs)e).ColumnIndex;   //列
            if (rowIndex == -1) return;//ヘッダークリック
            if (colIndex < 1)
            {//番号列の場合、変数値列に変更：
             // return;
                cellColMove(colIndex);//移動しない
            }
            //セル内容取得
            if (dataGridViewEx1[0, rowIndex].Value == null) return;//番号セル = null
            string stringVal = dataGridViewEx1[0, rowIndex].Value.ToString();//#番号
            //"#"を削除
            stringVal = stringVal.Replace("#", "");
            //数値変換
            int intVal = int.Parse(stringVal);
            //検索番号の変更
            setSerch(intVal);
            //トラックバーの移動
            //setTrackbar(intVal);
            //スクロールバーの移動
            //vScrollBar1.Value = rowIndexPlus;
        }
        /// <summary>
        /// dataGridViewExセル：マウス：アップ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewEx1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = ((DataGridViewCellMouseEventArgs)e).RowIndex;		//行
            int colIndex = ((DataGridViewCellMouseEventArgs)e).ColumnIndex;		//列
            if (rowIndex == -1) return;											//ヘッダークリック時、処理無し
            if (dataGridViewEx1[0, rowIndex].Value == null) return;				//番号無し
			string numString = dataGridViewEx1[0, rowIndex].Value.ToString();   //番号取得
			if( dataGridViewEx1.CurrentCell.Value == null ) {
				//セル=nullの場合、""に変える
				dataGridViewEx1.CurrentCell.Value = "";
			}
			object objVal = dataGridViewEx1.CurrentCell.Value.ToString();		//カレントセル内容取得
			if( colIndex == 1)//変数値
            {//ポップアップ：テンキー表示
                popupTenkeyOn((object)objVal, PopupTitleData, numString);		
            }
            if (colIndex == 2 )//備考
			{//ポップアップ：キーボード表示
                if (null != _popupKeyboard)ResetInput();							//インスタンスが無い場合、インスタンス作成
                int thisSizeWidth = this.Size.Height;								//フォーム高さ設定
                _popupKeyboard = new KeyboardDialog();								//ポップアップキーボードインスタンス作成
                _popupKeyboard.Location
					= new Point(0, thisSizeWidth - _popupKeyboard.Height);			//表示位置設定
                _popupKeyboard.FormClosed += KeyboardClosed;                        //FormClosedイベント
				_popupKeyboard.InputValue = objVal.ToString();						//編集文字列
                _popupKeyboard.NotifyReturn = _edtComment_ClickCallBack;			//画面閉じたイベント
                _popupKeyboard.NotifyTextChanged = _edtComment_ClickFileTextChanged;//テキスト変更イベント
				_popupKeyboard.Text= PopupTitleComment + " " + numString;			//タイトル
				_popupKeyboard.Show(this);											//画面表示
                _popupKeyboard.FocusedInputForm();									//キーボードのフォーカスON
            }
        }
        /// <summary>
        /// セル：チェンジ：0列目を選択の場合、1列目に変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewEx1_CurrentCellChanged( object sender, EventArgs e )
		{
			if( ( (DataGridViewEx)sender ).CurrentCell == null ) return;

			int rowIndex = ( (DataGridViewEx)sender ).CurrentCell.RowIndex;
			int colIndex = ( (DataGridViewEx)sender ).CurrentCell.ColumnIndex;

            //セル内容取得
            if (dataGridViewEx1[0, rowIndex].Value == null) return;//番号セル = null
            string stringVal = dataGridViewEx1[0, rowIndex].Value.ToString();//#番号
            //"#"を削除
            stringVal = stringVal.Replace("#", "");
            //数値変換
            int intVal = int.Parse(stringVal);
            //検索番号の変更
            setSerch(intVal);

            //別スレッド処理
            System.Threading.Tasks.Task.Run( () => {
				if( (this.IsDisposed == true || this.Disposing == true)   ) return; retInvoke = BeginInvoke((MethodInvoker)delegate ()//別スレッドで操作するので、delegateを使用
				{//0列が選択されたら選択を1列目に変更
					if( colIndex == 0 ) cellColMove( 1 );
				} );
			} );
		}
        /// <summary>
        /// セル：ペインティング：0列目セル選択色不可※もしくは0列目を表示：dataGridViewEx1.RowHeadersVisible = true;//固定列：表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewEx1_CellPainting( object sender, DataGridViewCellPaintingEventArgs e )
		{
			if( e.ColumnIndex == 0 && e.RowIndex >= 0 )//1列目
			{
				e.Graphics.FillRectangle( Brushes.DarkGray, e.CellBounds );//背景：ダークグレー
				StringFormat _format = new StringFormat();
				_format.Alignment = StringAlignment.Near;//左寄せ
				Rectangle _rect = e.CellBounds;
				_rect.Offset( 0, 5 );
				if( e.Value != null ) {
					e.Graphics.DrawString( e.Value.ToString(), new Font( "Meiryo UI", 12, FontStyle.Bold ), Brushes.Black, _rect, _format );//前景：黒
					e.Paint( e.ClipBounds, DataGridViewPaintParts.Border );
					e.Handled = true;
				}
			}
		}
		#endregion
		#region <region<数字ボタン：ステップ数(1から10000)▲▼：加算/減算>region>
		/// <summary>
		/// 数字ボタン：▲▼：加算/減算
		/// </summary>
		/// <param name="value"></param>
		private void valueUpDn(int value)
        {   //カレントセル + とび先インデックス
            int rowIndex = dataGridViewEx1.CurrentCell.RowIndex + value;
            //とび先インデックスが0以下
            //  if (rowIndex < _startCount ) rowIndex = _startCount;
            //rowIndexが範囲を超えた場合
            //  if (rowIndex >= _startCount + _endCount)
            // {
            //     rowIndex= _startCount + _endCount-1;
            // }

            int rowCount = dataGridViewEx1.RowCount;//90000
            if (rowIndex < 0) rowIndex = 0;
            if (rowIndex > rowCount-1) rowIndex = rowCount-1;

            //とび先の番号セルがnull
            if (dataGridViewEx1[0, rowIndex].Value == null) return;
            //指定行へセル移動
            cellRowMove(rowIndex);
            if (rowIndex < 0) rowIndex = 0;
            //if (rowIndex > trackBar1.Maximum) rowIndex = trackBar1.Maximum;
            //トラックバーの移動
            //setTrackbar(rowIndex);
            //TextBoxに表示
            numericTextBox1.Text = rowIndex.ToString();
            //スクロールバーの移動
            // vScrollBar1.Value = rowIndexPlus;
            //TextBoxに表示
            string num = dataGridViewEx1[0, rowIndex].Value.ToString(); //"#nnnnn"取得
            numericTextBox1.Text = num.Replace("#","");                 //"#"削除
        }
        /// <summary>
        /// 数字ボタン：▲10000をクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_10000Up_Click(object sender, EventArgs e)
        {
            valueUpDn(-10000);
        }
		/// <summary>
		/// 数字ボタン：▲1000をクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_1000Up_Click(object sender, EventArgs e)
        {
            valueUpDn(-1000);
        }
		/// <summary>
		/// 数字ボタン：▲100をクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_100Up_Click(object sender, EventArgs e)
        {
            valueUpDn(-100);
        }
		/// <summary>
		/// 数字ボタン：▲10をクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_10Up_Click(object sender, EventArgs e)
        {
            valueUpDn(-10);
        }
		/// <summary>
		/// 数字ボタン：▼10をクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_10Down_Click(object sender, EventArgs e)
        {
            valueUpDn(10);
        }
		/// <summary>
		/// 数字ボタン：▼100をクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_100Down_Click(object sender, EventArgs e)
        {
            valueUpDn(100);
        }
		/// <summary>
		/// 数字ボタン：▼1000をクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_1000Down_Click(object sender, EventArgs e)
        {
            valueUpDn(1000);
        }
		/// <summary>
		/// 数字ボタン：▼10000をクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_10000Down_Click(object sender, EventArgs e)
        {
            valueUpDn(10000);
        }
		/// <summary>
		/// 数字ボタン：使用可能/不可
		/// </summary>
		/// <param name="boolVal"></param>
		private void numStepbutton_Enabled(bool boolVal)
		{
			button_10000Up.Enabled = boolVal;
			button_1000Up.Enabled = boolVal;
			button_100Up.Enabled = boolVal;
			button_10Up.Enabled = boolVal;
			button_10Down.Enabled = boolVal;
			button_100Down.Enabled = boolVal;
			button_1000Down.Enabled = boolVal;
			button_10000Down.Enabled = boolVal;
		}
		#endregion
		#region <region<NCデータ読み込み100個>region>
		/// <summary>
		/// NC読み込みをクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_NcRead_Click(object sender, EventArgs e)
        {
			// 元のカーソルを保持
			Cursor preCursol = Cursor.Current;
			// カーソルを待機カーソルに変更
			Cursor.Current = Cursors.WaitCursor;

			McDatMacroManage macroManage = new McDatMacroManage( MacroManage.ExtensionMacro );  //クラス作成
			try {
				ResultCodes ret = macroManage.Read();
				if( ret == ResultCodes.Success ) {
					//読み込み成功時
					_startCount =  macroManage.Items[0].Number;
                    _startCountNc =_startCount;
                    _endCount = macroManage.Items.Count;
                    _endCountNc = _endCount;

                    _intCount = 0;//ファイル先頭
					for( ; _intCount < 100 ; _intCount++ ) {
                        int index = _intCount;//番号取得
                        //int index = macroManage.Items[_intCount].Number;//番号取得
                        //DGVEX のセル[10000]＝ファイルの先頭
                        dataGridViewEx1.Rows[index].Cells[0].Value = "#" + macroManage.Items[_intCount].Number.ToString();
						dataGridViewEx1.Rows[index].Cells[1].Value = macroManage.Items[_intCount].Value;
						dataGridViewEx1.Rows[index].Cells[2].Value = macroManage.Items[_intCount].Comment;
					}
					_TimerAfterNcData.Change( constWaitTime, timerSpeed );//1000ms後タイマを1回起動し、100から99999までセット
				} else {//エラー
					macroManage.Dispose();
					macroManage = null;
					MessageBox.Show( "読み込みエラー発生", "失敗", MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
					return;
				}
			} catch( Exception ex ) {//その他例外
				MessageBox.Show( ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
			}
			//破棄
			macroManage.Dispose();
			macroManage = null;
			//カーソルを元に戻す
			Cursor.Current = preCursol;         //カーソルを元に戻す 
		}
		#endregion
		#region <region<NCデータ読み込み100個以降全て>region>
		/// <summary>
		/// NCデータ読み込み：タイマ・ファイア：残データー読み込み
		/// </summary>
		/// <param name="obj"></param>
		public void timerFireNcRead( Object obj )
		{
			//別スレッド処理
			System.Threading.Tasks.Task.Run( () => {
				if( (this.IsDisposed == true || this.Disposing == true)   ) return; retInvoke = BeginInvoke((MethodInvoker)delegate ()//別スレッドで操作するので、delegateを使用
				{
					// 元のカーソルを保持
					Cursor preCursol = Cursor.Current;
					// カーソルを待機カーソルに変更
					Cursor.Current = Cursors.WaitCursor;					//処理時間：計測
					var sw = new System.Diagnostics.Stopwatch();
					sw.Start();//開始
					
					//データ読み込み
					McDatMacroManage macroManage = new McDatMacroManage( MacroManage.ExtensionMacro );  //クラス作成
					ResultCodes ret = macroManage.Read();               //データ読込み
					if( ret == ResultCodes.FailToReadFile ) {
						macroManage.Dispose();
						macroManage = null;
						MessageBox.Show( "読み込みエラー発生", "失敗", MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
						return;
					}
					//プログレス・バー表示
					//progressBar1.Visible = true;
					dataGridViewEx1.SetRedraw( true );//再描画：禁止
                                                      //progressBar1.Step = 1;//ステップ
                                                      //残りのデータをセット
                    for (; _intCount < _endCountNc; _intCount++)//100-99999
                    {
                        int index = _intCount;//番号取得
                                              //int index = macroManage.Items[_intCount].Number;//番号取得
                                              //DGVEX のセル[10000]＝ファイルの先頭
                        dataGridViewEx1.Rows[index].Cells[0].Value = "#" + macroManage.Items[_intCount].Number.ToString();
                        dataGridViewEx1.Rows[index].Cells[1].Value = macroManage.Items[_intCount].Value;
                        dataGridViewEx1.Rows[index].Cells[2].Value = macroManage.Items[_intCount].Comment;
                        //プログレス・バー更新
                        if ((_intCount % 100) == 0
                        ||(_intCount == _endCountNc)
                        )
                        {
                            button_NcRead.ProgressBarValue = _intCount; //プログレス・バーは処理時間が約1秒増しになる
                        }
                    }
                    dataGridViewEx1.SetRedraw( false ); //再描画：許可
                    dataGridViewEx1_Enable(true);       //dataGridViewEx1：使用可
                    button_NcRead.ProgressBarValue = 0;
					button_FileWrite.Enabled = true;    //「ファイルデータ書込」ボタン使用可
					button_NcWrite.Enabled = true;		//「NCデータ書込」ボタン使用可
					numericTextBox1.Enabled = true;     //「＃[]」を使用可能
					numStepbutton_Enabled( true );      //数字ステップボタン：使用可能
					Cursor.Current = preCursol;         //カーソルを元に戻す 
					//処理時間：計測
					sw.Stop();                          //停止
														//時間をラベルに表示
					label_Read2.Text = String.Format( "所要時間: {0}", sw.Elapsed );
				} );
			} );
			//タイマー
			_TimerAfterNcData.Change( System.Threading.Timeout.Infinite, timerSpeed );//タイマ待機
		}
		#endregion
		#region <region<NCデータ書き込み>region>
		/// <summary>
		/// NC書き込みをクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_NcWrite_Click(object sender, EventArgs e)
        {
			try {
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;
				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				McDatMacroManage macroManage = new McDatMacroManage( MacroManage.ExtensionMacro );  //クラス作成
				//データ読み込み
				ResultCodes ret = macroManage.Read();
                _intCount = 0;
                int index = 0;
                //int index = _startCountNc;
                for ( ; _intCount < _endCountNc ; _intCount++, index++ )//10000-99999
				{
					//DGVEX のセル[10000]＝ファイルの先頭
					//string tmpString = dataGridViewEx1.Rows[index].Cells[0].Value.ToString();//#番号
					//string valString = tmpString.Replace( "#", "" );//#番号

                    index = _intCount;
                    //index = int.Parse(valString);
                    //index = int.Parse( dataGridViewEx1.Rows[index].Cells[0].Value.ToString() );//番号
                    //データ==nullの時、""を代入
                    if ( dataGridViewEx1.Rows[index].Cells[1].Value == null ) dataGridViewEx1.Rows[index].Cells[1].Value = "";
					if( dataGridViewEx1.Rows[index].Cells[2].Value == null ) dataGridViewEx1.Rows[index].Cells[2].Value = "";
					//下記は書き込み対象データ
					macroManage.Items[_intCount].Value = dataGridViewEx1.Rows[index].Cells[1].Value.ToString();//変数値
					macroManage.Items[_intCount].Comment = dataGridViewEx1.Rows[index].Cells[2].Value.ToString();//備考
				}
				//データ書き込み
				ret = macroManage.Write();
				// カーソルを元に戻す
				Cursor.Current = preCursor;
				if( ret == ResultCodes.FailToReadFile ) {//エラー
					macroManage.Dispose();
					macroManage = null;
					MessageBox.Show( "書き込みエラー発生", "失敗", MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
					return;
				}
			} catch( Exception ex )//その他例外
			  {
				MessageBox.Show( ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
			}
		}
        #endregion
        #region <region<ファイル読み込み100個>region>
        //private int _startCountFile = 0;    //開始インデクス位置
        //private int _endCountFile = 0;      //終了開始インデクス位置
        /// <summary>
        /// ファイル読み込みをクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_FileRead_Click(object sender, EventArgs e)
        {
			//ファイルダイアログ表示
			_macroFileName = FileForm.ShowSubForm(this, FileFormMode.OpenMacro, System.IO.Path.GetFullPath( @"Macro" ) );
            if (!_macroFileName.Contains(".VAR"))
            {
                //ファイルが見つからなかったら読み込み処理をしない。
                return;
            }
            else
            {
                //ファイルのフルパスをファイル名に変換
                _macroFileName = _macroFileName.Remove(0, _macroFileName.LastIndexOf("\\") + 1).Replace(".VAR", "");

                //読み込んだファイルを既定ファイルに設定する。
                try
                {
                    using (FileSettings fs = new FileSettings())
                    {
                        //ファイル書き込み
                        fs.Read();
                        //グラフ
                        fs.WriteAttr("Root/LogSettingFile/OpenFile", "name", _macroFileName);
                        fs.Write();
                    }
                }
                catch(Exception ex)
                {
                    ECNC3Exception.FileIOFilter(ex, this);
                }
            }
			// 元のカーソルを保持
			Cursor preCursol = Cursor.Current;
			// カーソルを待機カーソルに変更
			Cursor.Current = Cursors.WaitCursor;

			FileMacroManage fileMacroManage = new FileMacroManage(_macroFileName);//クラス作成
			try
            {
				ResultCodes ret = fileMacroManage.Read();
				if( ret == ResultCodes.Success ) {
                    //読み込み成功時
                    _startCount = fileMacroManage.Items[0].Number;
                    _startCountFile = _startCount;
                    _endCount =fileMacroManage.Items.Count;
                    _endCountFile = _endCount;
                    _intCount = 0;//ファイル先頭
					for( ; _intCount < 100; _intCount++ ) {
                        int index = _intCount;//番号取得
                        //DGVEX のセル[10000]＝ファイルの先頭
                        dataGridViewEx1.Rows[index].Cells[0].Value = "#" + fileMacroManage.Items[_intCount].Number.ToString();
                        dataGridViewEx1.Rows[index].Cells[1].Value = fileMacroManage.Items[_intCount].Value;
                        dataGridViewEx1.Rows[index].Cells[2].Value = fileMacroManage.Items[_intCount].Comment;
                    }
                    _TimerAfterFileData.Change( constWaitTime, timerSpeed );//1000ms後タイマを1回起動し、100から99999までセット
                }else
                {//エラー
                    fileMacroManage.Dispose();
                    fileMacroManage = null;
					MessageBox.Show( "読み込みエラー発生", "失敗", MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
					return;
                }
			} catch(Exception ex) {//その他例外
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            //破棄
            fileMacroManage.Dispose();
            fileMacroManage = null;
			Cursor.Current = preCursol;         //カーソルを元に戻す 
		}
		#endregion
		#region <region<ファイル読み込み100個以降全て>region>

		/// <summary>
		/// ファイル読み込み：タイマ・ファイア：残データー読み込み
		/// </summary>
		/// <param name="obj"></param>
		public void timerFireRead(Object obj)
        {
            //別スレッド処理
            System.Threading.Tasks.Task.Run(() => {
                if( (this.IsDisposed == true || this.Disposing == true)   ) return; retInvoke = BeginInvoke((MethodInvoker)delegate ()//別スレッドで操作するので、delegateを使用
                {
					// 元のカーソルを保持
					Cursor preCursol = Cursor.Current;
                    // カーソルを待機カーソルに変更
                    Cursor.Current = Cursors.WaitCursor;
                    //処理時間：計測
                    var sw = new System.Diagnostics.Stopwatch();
                    sw.Start();//開始
                    //ファイル読み込み
                    FileMacroManage fileMacroManage = new FileMacroManage(_macroFileName);//クラス作成
                    ResultCodes ret = fileMacroManage.Read();				//データ読込み
                    if (ret == ResultCodes.FailToReadFile)
                    {
                        fileMacroManage.Dispose();
                        fileMacroManage = null;
						MessageBox.Show( "読み込みエラー発生", "失敗", MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
						return;
                    }
                    //プログレス・バー表示
                    dataGridViewEx1.SetRedraw(true);//再描画：禁止
                    //残りのデータをセット
                    for (; _intCount < _endCountFile; _intCount++)//100-99999
                    {
                        int index = _intCount;//番号取得
                        //int index = fileMacroManage.Items[_intCount].Number;//番号取得
                        //DGVEX のセル[10000]＝ファイルの先頭
                        dataGridViewEx1.Rows[index].Cells[0].Value = "#" + fileMacroManage.Items[_intCount].Number.ToString();
                        dataGridViewEx1.Rows[index].Cells[1].Value = fileMacroManage.Items[_intCount].Value;
                        dataGridViewEx1.Rows[index].Cells[2].Value = fileMacroManage.Items[_intCount].Comment;
                        //プログレス・バー更新
                        if ((_intCount % 100) == 0
                        || (_intCount == _endCountFile)
                        )
                        {
                            button_NcRead.ProgressBarValue = _intCount; //プログレス・バーは処理時間が約1秒増しになる
                        }
                    }
                    button_NcRead.ProgressBarValue = 0;
                    dataGridViewEx1.SetRedraw(false);   //再描画：許可
                    dataGridViewEx1_Enable(true);       //dataGridViewEx1：使用可
					button_FileWrite.Enabled = true;    //「ファイル書込」ボタン使用可
					button_NcWrite.Enabled = true;      //「NC書込」ボタン使用可
					numericTextBox1.Enabled = true;     //「＃[]」を使用可能
					numStepbutton_Enabled(true);		//数字ステップボタン：使用可能
					Cursor.Current = preCursol;         //カーソルを元に戻す 
					//処理時間：計測
					sw.Stop();                          //停止
					//時間をラベルに表示
					label_Read2.Text = String.Format("所要時間: {0}", sw.Elapsed);
                }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
            }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
            //タイマー
            _TimerAfterFileData.Change(System.Threading.Timeout.Infinite, timerSpeed);//タイマ待機
        }
        #endregion
        #region <region<ファイル書き込み>region>
        /// <summary>
        /// ファイル書き込みをクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_FileWrite_Click( object sender, EventArgs e )
		{
            try
            {
                ReturnMessage retMessage = SelectCommandsDialog.ShowSubForm( this,"プログラム保存", new string[] { "上書き保存", "名前を付けて保存", "", "" }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                switch (retMessage)
                {
                    case ReturnMessage.ExecuteA1:
                        // 元のカーソルを保持
                        Cursor preCursor = Cursor.Current;
                        // カーソルを待機カーソルに変更
                        Cursor.Current = Cursors.WaitCursor;
                        FileMacroManage fileMacroManage = new FileMacroManage(_macroFileName);
                        fileMacroManage.Items = new StructureMacroManageList();

                        //データ読み込み
                        ResultCodes ret = fileMacroManage.Read();

                        _intCount = 0;
                        int index = _startCountFile;
                        for (; _intCount < _endCountFile; _intCount++, index++)//10000-99999
                        {
                            //DGVEX のセル[10000]＝ファイルの先頭

                            //string tmpString = dataGridViewEx1.Rows[index].Cells[0].Value.ToString();//#番号
                            //string valString = tmpString.Replace("#", "");//#番号
                            //index = int.Parse(valString);

                            index = _intCount;
                            //下記は書き込み対象データ
                            fileMacroManage.Items[_intCount].Value = dataGridViewEx1.Rows[index].Cells[1].Value.ToString();//変数値
                            fileMacroManage.Items[_intCount].Comment = dataGridViewEx1.Rows[index].Cells[2].Value.ToString();//備考
                        }
                        //データ書き込み
                        ret = fileMacroManage.Write();
                        // カーソルを元に戻す
                        Cursor.Current = preCursor;
                        if (ret == ResultCodes.FailToReadFile)
                        {//エラー
                            fileMacroManage.Dispose();
                            fileMacroManage = null;
                            MessageBox.Show("書き込みエラー発生", "失敗", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        break;

                    case ReturnMessage.ExecuteA2:
                        _macroFileName = FileForm.ShowSubForm(this,FileFormMode.SaveMacro, Path.GetFullPath(@"Macro"));
                        if (!_macroFileName.Contains(".VAR"))
                        {
                            //ファイルが見つからなかったら読み込み処理をしない。
                            return;
                        }
                        else
                        {
                            //ファイルのフルパスをファイル名に変換
                            _macroFileName = _macroFileName.Remove(0, _macroFileName.LastIndexOf("\\") + 1).Replace(".VAR", "");
                        }
                        // 元のカーソルを保持
                        Cursor preCursorNewFile = Cursor.Current;
                        // カーソルを待機カーソルに変更
                        Cursor.Current = Cursors.WaitCursor;
                        FileMacroManage fileMacroManageNewFile = new FileMacroManage(_macroFileName);
                        fileMacroManageNewFile.Items = new StructureMacroManageList();
                        fileMacroManageNewFile.CreateFile();

                        //データ読み込み
                        ResultCodes retNewFile = fileMacroManageNewFile.Read();
                        //ResultCodes ret = ResultCodes.Success;

                        _intCount = 0;
                        int indexNewFile = _startCountFile;
                        for (; _intCount < _endCountFile; _intCount++, indexNewFile++)//10000-99999
                        {
                            //DGVEX のセル[10000]＝ファイルの先頭

                            string tmpString = dataGridViewEx1.Rows[indexNewFile].Cells[0].Value.ToString();//#番号
                            string valString = tmpString.Replace("#", "");//#番号
                            index = int.Parse(valString);

                            fileMacroManageNewFile.Items[_intCount].Value = dataGridViewEx1.Rows[indexNewFile].Cells[1].Value.ToString();//変数値
                            fileMacroManageNewFile.Items[_intCount].Comment = dataGridViewEx1.Rows[indexNewFile].Cells[2].Value.ToString();//備考
                        }
                        //データ書き込み
                        ret = fileMacroManageNewFile.Write();
                        // カーソルを元に戻す
                        Cursor.Current = preCursorNewFile;
                        if (ret == ResultCodes.FailToReadFile)
                        {//エラー
                            fileMacroManageNewFile.Dispose();
                            fileMacroManageNewFile = null;
                            MessageBox.Show("書き込みエラー発生", "失敗", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        break;

                    case ReturnMessage.Cancel:

                        break;

                }

                
            }
            catch (Exception ex)//その他例外
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
		#endregion
	}
}
