///////////////////////////////////////////////////////////////////////////////////////////
//
// (1) システム名称 : ELENIX ECNCⅢ
// (2) ファイル名   : IOCheckForm.cs
// (3) 概要         : ﾎﾞｰﾄﾞIO設定確認画面
// (4) 作成日       : 2015.06.01
// (5) 作成者       : 八野
// (6) 特記事項     : VisualStudio2015 C#
// (7) 更新履歴     : 2017.03.21：柏原
//
///////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ECNC3.Enumeration;
using ECNC3.Models;
using ECNC3.Models.Common;
using ECNC3.Models.McIf;
using System.Xml;//XML
using System.IO;//XML

namespace ECNC3.Views
{
	/// <summary>I/O画面</summary>
	public partial class IOCheckForm : ECNC3Form
	{
		#region <region<初期化>region>>>
		/// <summary>設定結果取得関数定義</summary>
		public delegate void NotifyReturnDelegate();
		/// <summary>終了通知</summary>
		private NotifyReturnDelegate _notifyReturn = null;
		/// <summary>>設定結果取得</summary>
		public NotifyReturnDelegate NotifyReturn
		{
			set { _notifyReturn = value; }
		}
		/// <summary>
		/// フォーム　コンストラクタ
		/// </summary>
		public IOCheckForm()
		{
			InitializeComponent();
		}
		/// <summary>
		/// フォームロード時のイベントハンドラ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <remarks>IOCheckファイルの読み込みとGridView表示処理</remarks>
		private void IOCheckForm_Load( object sender, EventArgs e )
		{
            try{
                //ラジオボタン
                radioButtonEx_AllDsp.SetChecked(true);//全選択：ON
                //DataGridViewEx設定
                _dgList.Initialize();
                //_dgList.DefaultCellStyle.SelectionBackColor = Color.Transparent;//このコールはセル描画で前に表示していた文字と重なる場合が有る：2017-03-17：柏原
                _dgList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                _dgList.InitCol("Names", typeof(string));
                _dgList.InitCol("Address", typeof(string));
                _dgList.InitCol("Note", typeof(string));
                _dgList.InitCol("IO", typeof(int));
                using (McDatIOData ioData = new McDatIOData())
                {

                    ioData.ReadEx();
                    ioData.Initialize();
                    if(ioData.Items==null) throw new NullReferenceException();//null参照例外
                    _dgList.RowCount = ioData.Items.Count();
                    if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                 {
                     _dgList.SetRedraw(true); ;//再描画：禁止
                    //プログレス・バー表示
                    //progressBar1.Visible = true;
                    progressBar1.Minimum = 0;
                     progressBar1.Maximum = _dgList.RowCount;
                     progressBar1.Step = 1;//ステップ
                    //読み込まれたIOチェックデータをDataGridViewに表示
                    for (int RowNum = 0; RowNum < _dgList.RowCount; RowNum++)
                     {
                         _dgList.Rows[RowNum].Cells["Names"].Value = ioData.Items[RowNum].Name;
                         _dgList.Rows[RowNum].Cells["Address"].Value = $"{ioData.Items[RowNum].Address:d4}:"
                             + $"D{(ioData.Items[RowNum].Mask >> (RowNum % 16)) * (RowNum % 16):d2}"
                             + ((IOAccessTargets.Input == ioData.Items[RowNum].AccessTarget)
                                 ? " (i#" : (IOAccessTargets.Output == ioData.Items[RowNum].AccessTarget) ? " (o#" : "   ")
                             + $"{ioData.Items[RowNum].Channel})";
                         _dgList.Rows[RowNum].Cells["Note"].Value = ioData.Items[RowNum].Note;
                         _dgList.Rows[RowNum].Cells["IO"].Value = (IOAccessTargets.Input == ioData.Items[RowNum].AccessTarget) ? 0 : 1;
                         SetRowColor(_dgList.Rows[RowNum], ioData.Items[RowNum].Signal, ioData.Items[RowNum].Forced);
                        //プログレス・バー更新
                        progressBar1.Value = RowNum; //プログレス・バーは処理時間が約1秒増しになる
                    }
                     _dgList.SetRedraw(false);
                    //ShowCheange( true );2回コールするので、ここはコメント、代わりにradioButtonEx_Input.SetChecked()を使用：2017-03-22：柏原
                    radioButtonEx_Input.SetChecked(true);

                    //progressBar1.Visible = false;       //プログレス・バー：非表示
                }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                }
                //行選択をファイルから読み込み
                LineSelectXmlRead();

                //選択色無し
                _dgList.DefaultCellStyle.SelectionBackColor = Color.Transparent;
                _dgList.DefaultCellStyle.SelectionForeColor = Color.Transparent;
        //例外処理
        } catch(NullReferenceException) {
                MessageBox.Show("I/Oチェック表示データが取得できません。\nioData.Items = null", "null参照",MessageBoxButtons.OK,MessageBoxIcon.Error);
        } finally {
			}
        }
		/// <summary>
		/// IO表示イベントハンドラ
		/// </summary>
		/// <param name="e">状態監視用クラス</param>
		internal void IOMonitoring( IOMonitorEventArgs e )
		{
			if( e.Items.Count < 2880 ) {
                ECNC3Log logs = new ECNC3Log("IOCheckForm.IOMonitoring");
                logs.Debug("Detected. changing I/O signal");
                logs.Error("Not IO Mode!!");
				return;
			}
			try {
				//System.Threading.Thread.Sleep(100);
				if( (this.IsDisposed == true || this.Disposing == true)   ) return; retInvoke = BeginInvoke((MethodInvoker)delegate
				 {
					// _dgList.SetRedraw( true );//画面のちらつきをするのでコメント：柏原
					//読み込まれたIOチェックデータをDataGridViewに表示
					for( int RowNum = 0 ; RowNum < _dgList.RowCount ; RowNum++ ) {
						 SetRowColor( _dgList.Rows[RowNum], e.Items[RowNum].Signal, e.Items[RowNum].Forced );
					 }
                     //_dgList.SetRedraw( false );//画面のちらつきをするのでコメント：柏原
                 }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
			} catch( Exception ex ) {
				MessageBox.Show( ex.Message + "\n" + ex.InnerException.Message );
			}

		}
		/// <summary>行ごとの文字色、背景色設定</summary>
		/// <param name="row">DataGridView 行オブジェクト</param>
		/// <param name="signal">信号状態</param>
		/// <param name="forced">強制設定の有無</param>
		private void SetRowColor( DataGridViewRow row, bool signal, bool forced )
		{
            Color back = row.DefaultCellStyle.BackColor;
            if (true == signal)
            {
                if ((true == forced) && (Color.Yellow != back))
                {
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                }
                else if ((false == forced) && (Color.Lime != back))
                {
                    row.DefaultCellStyle.BackColor = Color.Lime;
                }
                if (Color.Black != row.DefaultCellStyle.ForeColor)
                {
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
            }
            else
            {
                if ((true == forced) && (Color.Navy != back))
                {
                    row.DefaultCellStyle.BackColor = Color.Navy;
                }
                else if ((false == forced) && (ECNC3Color.StandardBack != back))
                {
                    row.DefaultCellStyle.BackColor = ECNC3Color.StandardBack;
                }
                if (FileUIStyleTable.DefaultForeColor != row.DefaultCellStyle.ForeColor)
                {
                    row.DefaultCellStyle.ForeColor = FileUIStyleTable.DefaultForeColor;
                }
            }
        }
        #endregion
        #region <region<終了時>region>>>
        /// <summary>
        /// 閉じる：クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEx_Return_Click( object sender, EventArgs e )
		{
			//行選択を記録
			LineSelectXmlWrite();
			_notifyReturn?.Invoke();
		}
		#endregion
		#region <region<行選択をファイル保存/読込>region>>>
		/// <summary>
		/// 行選択をファイルに保存
		/// </summary>
		private void LineSelectXmlWrite()
		{
			try {
				XmlDocument xmlDocument = new XmlDocument();
				XmlElement elem = xmlDocument.CreateElement( "root" );//最初の要素文字
				xmlDocument.AppendChild( elem );
				//全行から
				for( int RowNum = 0 ; RowNum < _dgList.RowCount ; RowNum++ ) {
					XmlElement item_elem = xmlDocument.CreateElement( "item" + RowNum.ToString() );//item0から
					if(_dgList.Rows[RowNum].Cells["ColumnButton"].Value == null
					|| _dgList.Rows[RowNum].Cells["ColumnButton"].Value.ToString() == "" ) {
						//nullか"" = false
						item_elem.SetAttribute( "select", "false" );
					} else {
						//上記以外 = true
						item_elem.SetAttribute( "select", "true" );
					}
					elem.AppendChild( item_elem );//要素に追加
				}
				string masterFolder = @FilePathInfo.MasterData;//\Masterフォルダ
				xmlDocument.Save( masterFolder + "UIIoCheckSelectItem.xml" );//保存
			} catch( Exception exc ) {
				//例外処理
				MessageBox.Show( exc.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error );
			}
		}
		/// <summary>
		/// 行選択をファイルから読み込み
		/// </summary>
		private void LineSelectXmlRead()
		{
			try {
				string masterFolder = @FilePathInfo.MasterData;//\Masterフォルダ
				XmlDocument xmlDocument = new XmlDocument();
				bool booExists = File.Exists(masterFolder + "UIIoCheckSelectItem.xml");
				if( booExists == false ) {
					//ファイル無し ※終了時作成する
					return;
				}
				xmlDocument.Load( masterFolder + "UIIoCheckSelectItem.xml" );//読込
				XmlNode root = xmlDocument.DocumentElement;
				for( int RowNum = 0 ; RowNum < _dgList.RowCount ; RowNum++ ) {
					XmlNode node = root.ChildNodes[RowNum];
					string strTrueFalse = ( (XmlElement)node ).GetAttribute( "select" );
					if( strTrueFalse == "true" ) {
						//選択をセット
						_dgList.Rows[RowNum].Cells["ColumnButton"].Value = "●";
					}
				}
			} catch( Exception exc) {
				//例外処理
				MessageBox.Show( exc.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error );
			}
		}
		#endregion
		#region <region<入力/出力ラジオボタン>region>>>
		/// <summary>表示切替のためのラジオボタンが変更された</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _rdoInput_CheckedChanged( object sender, EventArgs e )
		{
			ShowCheange( radioButtonEx_Input.Checked );
        }
        /// <summary>
        /// <summary>表示切替</summary>
        /// </summary>
        /// <param name="input"></param>
        /// <param name="validProgress">プログレスバー：true=有効、false=無効</param>
        /// <param name="DGVShow">DataGridViewEx：true=表示、false=非表示</param>
        private void ShowCheange( bool input, bool validProgress = true, bool DGVShow = true)
		{
            //DataGridViewEx：データ表示
            if(DGVShow)dgvExDisply( input, validProgress);
			//入力/出力ラジオボタン：ON/OFF
			bool visibleA = input;
			bool visibleB = ( true == input ) ? false : true;
			radioButtonEx_Input.SetChecked( visibleA );
			radioButtonEx_Output.SetChecked( visibleB );
		}
        #endregion
        #region <region<データグリッド：_dgList>region>>>
        /// <summary>
        ///  DataGridViewEx：データ表示
        /// </summary>
        /// <param name="inOutput"></param>
        /// <param name="validProgress">プログレスバー：true=有効、false=無効</param>
        private void dgvExDisply( bool inOutput,bool validProgress = true)
		{
			Cursor preCursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;//ウエイトカーソルに変更
			//_dgList.SetRedraw( true );          //再描画：無効

			bool visibleA = inOutput;
			bool visibleB = ( true == inOutput ) ? false : true;
			int startRowIndex = -1;
			int curentIndex = -1;
			int iRows = _dgList.RowCount;
            if (validProgress) {
                //プログレス・バー表示
                progressBar1.Visible = true;
                progressBar1.Minimum = 0;
                progressBar1.Maximum = iRows;
                progressBar1.Value = 0;
            }
            else
            {
                //プログレス・バー非表示
                progressBar1.Visible = false;
            }
            foreach ( DataGridViewRow row in _dgList.Rows ) {
				//rowは前半＝Input、後半＝Outputデータ
				//表示する入力/出力データを切り替え
				curentIndex++;//カウンターインクリメント
				row.Visible = ( 0 == (int)row.Cells["IO"].Value ) ? visibleA : visibleB;
				if( row.Visible == false ) {
					continue;
				}
				//開始インデックスを１回だけ記録：前半Input=0、後半Output=全行/2
				if( startRowIndex == -1 ) {
					startRowIndex = curentIndex;
				}
				//Input/Outputで表示されるデータ
				if( SelDiplyMode ) {//選択表示＝ON
					if( (string)row.Cells[0].Value != "●" ) {//行の先頭に選択"●"がなければ、行非表示
						row.Visible = false;
					}
				}
                if (validProgress)
                {
                    //プログレス・バー更新
                    progressBar1.Value = curentIndex; //プログレス・バーは処理時間が約1秒増しになる
                }
			}
			if( _dgList.RowCount > 0 ) {//データ有り
				if( _dgList.Rows[startRowIndex].Visible ) {
					//指定セルに移動
					_dgList.CurrentCell = _dgList[1, startRowIndex];    //画面を先頭に移動
					_dgList.CurrentCell = null;                         //セル選択なし：Output画面で_dgList.Rows[0].Visible=falseで選択できない
				}
			}
            //_dgList.SetRedraw( false ); //再描画：有効
            if (validProgress) progressBar1.Visible = false;  //プログレス・バー：非表示
			Cursor.Current = preCursor;                      //ウエイトカーソル：戻す
		}
		/// <summary>
		/// セル：マウスダウン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _dgList_CellMouseDown( object sender, DataGridViewCellMouseEventArgs e )
		{
			int rowIndex = ( (DataGridViewCellMouseEventArgs)e ).RowIndex;      //行
			int colIndex = ( (DataGridViewCellMouseEventArgs)e ).ColumnIndex;   //列
			if( rowIndex == -1 ) return;//ヘッダークリック

            //全表示
            CellSelectChange( rowIndex );//セル選択変更
        }
        /// <summary>
        /// セル選択変更
        /// </summary>
        /// <param name="rowIndex"></param>
        private void CellSelectChange(int rowIndex)
		{
			//全表示
			//セル上のボタン
			//DGVEXはチェック無効なのでここでON/OFF※初回はnull
			if( _dgList[0, rowIndex].Value == null ) _dgList[0, rowIndex].Value = "";
            bool sygnalTemp = false;
            //IO状態読み込み
            using (McDatIOData iodata = new McDatIOData())
            {
                ResultCodes readResult = iodata.ReadEx();
                if (iodata.Items == null) return;
                sygnalTemp = iodata.Items[rowIndex].Signal;

                //強制モードON/OFF確認
                if (radioButtonEx_Force.Checked == true)
                {

                    //強制入出力処理
                    if (sygnalTemp == true)
                    {
                        //「強制」ボタン=ONの場合
                        string strData = _dgList.Rows[rowIndex].Cells["Address"].Value.ToString();
                        //1データ強制書き込み：COMPIOBITクラス
                        ForceWriteOneData(strData, false);//制御フラグ=OFF
                    }
                    else
                    {
                        //「強制」ボタン=OFFの場合
                        string strData = _dgList.Rows[rowIndex].Cells["Address"].Value.ToString();
                        //1データ強制書き込み：COMPIOBITクラス
                        ForceWriteOneData(strData, true);//制御フラグ=ON
                    }
                }
                else
                {
                    if ((string)_dgList[0, rowIndex].Value == "●")
                    {
                        //ONの時、OFF
                        _dgList[0, rowIndex].Value = "";
                        if (SelDiplyMode == true)
                        {
                            //クリックされたセルは非表示
                            //別スレッド処理
                            System.Threading.Tasks.Task.Run(() =>
                            {
                                if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate ()//別スレッドで操作するので、delegateを使用
                                {
                                    _dgList.Rows[rowIndex].Visible = false;
                                }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                            }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                        }
                    }
                    else
                    {
                        //OFFの時、ON
                        _dgList[0, rowIndex].Value = "●";
                    }
                }
            }
        }
		/// <summary>
		/// 1データ強制書き込み：COMPIOBITクラス
		/// </summary>
		/// <param name="strData"></param>
		/// <param name="boolFlg"></param>
		private void ForceWriteOneData(string strData, bool boolFlg, int iRows=0) {
			using( McReqDatIOData reqDatIoData = new McReqDatIOData() ) {
				//reqDatIoData.Initialize();
				//アドレスデータを「入出力ポート番号」と「制御ビット番号」に分解
				int intIndex = strData.IndexOf( ":" );
				string strIOPort = strData.Substring( 0, intIndex );
				string StrCtrlBit = strData.Substring( intIndex + 2, 2 );
				//数値変換
				Int16 iOPort = Int16.Parse( strIOPort );
				UInt16 ctrlBit = UInt16.Parse( StrCtrlBit );

				if(iOPort >= 116)
                {
                    //出力
                    int iTemp = 32768 + iOPort - 116;//32768+124-116：32768=1000 0000 0000 0000 
                    iOPort = (short)iTemp;
                }
					
				//COMPIOBITクラスにデータセット
				Rt64ecdata.COMPIOBIT compIoBit = Rt64ecdata.COMPIOBIT.Init();
				
				compIoBit.Pno = iOPort;                         //入出力ポート番号：0-65535
				compIoBit.Bno = ctrlBit;                        //制御ビット番号：0-15
				switch( boolFlg ) {
					//制御フラグ：無変更=0、強制解除=1、強制ON=2、強制OFF=3
					case false:
						if( radioButtonEx_Force.Checked == true ) {
							compIoBit.flg = 3;//OFF:強制OFF
						} else {
							compIoBit.flg = 1;//OFF:強制解除
						}
						break;
					case true:  compIoBit.flg = 2;break;//ON
				}
				//NC書き込み
				ResultCodes resultRet = reqDatIoData.WriteEx( compIoBit );
				if( ResultCodes.Success != resultRet ) {
					//失敗時
					//MessageBox.Show("エラー行＝"+iRows.ToString());
					using( MessageDialog msg = new MessageDialog() ) {
						msg.Note = reqDatIoData.DataTypeName;
						msg.Error( resultRet, this);
					}
				}
			}
		}
		/// <summary>
		/// 全表示/選択表示値
		/// </summary>
		bool SelDiplyMode = false;//false=全表示、true=選択表示
		/// <summary>
		/// データグリッド行：全表示
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonEx_AllDsp_Click( object sender, EventArgs e )
		{
			//全選択
			if( radioButtonEx_Input.Checked)
			{//Inputボタン
				SelDiplyMode = false;                           //全表示
				radioButtonEx_AllDsp.SetChecked( true );        //ON
				radioButtonEx_SelectDsp.SetChecked( false );    //OFF

			} else
			{//Outputボタン
				SelDiplyMode = false;							//全表示
				radioButtonEx_AllDsp.SetChecked( true );        //ON
				radioButtonEx_SelectDsp.SetChecked( false );    //OFF
			}
			SelDiplyMode = false;					      //全表示モード
			dgvExDisply( radioButtonEx_Input.Checked );   //表示
        }
        /// <summary>
        /// データグリッド行：選択表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonEx_SelectDsp_Click( object sender, EventArgs e )
		{
            //選択表示
            if (radioButtonEx_Input.Checked)
            {//Inputボタン
                SelDiplyMode = true;                           //選択表示
				radioButtonEx_AllDsp.SetChecked( false );      //OFF
				radioButtonEx_SelectDsp.SetChecked( true );    //ON
			} else 
			{//Outputボタン
				SelDiplyMode = true;                           //選択表示
				radioButtonEx_AllDsp.SetChecked( false );      //OFF
				radioButtonEx_SelectDsp.SetChecked( true );    //ON
			}
			SelDiplyMode = true;                          //選択表示モード
			dgvExDisply( radioButtonEx_Input.Checked );   //表示
        }
        /// <summary>
        /// 選択変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _dgList_SelectionChanged(object sender, EventArgs e)
        {
            if (_dgList.SelectedCells != null)
            {
                _dgList.ClearSelection();
                _dgList.Refresh();
            }
        }
        #endregion
        #region <region<リセットボタン>region>>>
        /// <summary>リセットボタン押下</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnReset_Click(object sender, EventArgs e)
        {
            ResultCodes ret = ResultCodes.Success;
            //	汚水槽エラーかつSPXリセット無効設定のときはリセットを実行しない。
            while (true)
            {
                using (McDatStatus mc = new McDatStatus())
                {
                    ret = mc.Read();
                    if (ResultCodes.Success != ret)
                    {
                        break;
                    }
                    if (true == mc.Status.EtherCatErrorHoldingTankLiquidEmpty)
                    {
                        using (FileSettings fs = new FileSettings())
                        {
                            ret = fs.Read();
                            if (ResultCodes.Success != ret)
                            {
                                break;
                            }
                            if (true == fs.AttrBool("Root/Motions/Spx/Reset", "enbl"))
                            {
                                break;  //	リセットしない
                            }
                        }
                    }
                }
                using (McReqReset mc = new McReqReset())
                {
                    ret = mc.Execute();
                    if (ResultCodes.Success != ret)
                    {
                        break;
                    }
                }
                return;
            }
            if (ResultCodes.Success != ret)
            {
                using (MessageDialog dlg = new MessageDialog())
                {
                    dlg.Error(110, this);
                }
            }
        }
        /// <summary>
        /// リセットボタン：クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEx_Reset_Click(object sender, EventArgs e)
        {
            ResultCodes ret = ResultCodes.Success;
            //	汚水槽エラーかつSPXリセット無効設定のときはリセットを実行しない。
            while (true)
            {
                using (McDatStatus mc = new McDatStatus())
                {
                    ret = mc.Read();
                    if (ResultCodes.Success != ret)
                    {
                        break;
                    }
                    if (true == mc.Status.EtherCatErrorHoldingTankLiquidEmpty)
                    {
                        using (FileSettings fs = new FileSettings())
                        {
                            ret = fs.Read();
                            if (ResultCodes.Success != ret)
                            {
                                break;
                            }
                            if (true == fs.AttrBool("Root/Motions/Spx/Reset", "enbl"))
                            {
                                break;  //	リセットしない
                            }
                        }
                    }
                }
                using (McReqReset mc = new McReqReset())
                {
                    ret = mc.Execute();
                    if (ResultCodes.Success != ret)
                    {
                        break;
                    }
                }
                return;
            }
            if (ResultCodes.Success != ret)
            {
                using (MessageDialog dlg = new MessageDialog())
                {
                    dlg.Error(110, this);
                }
            }
        }
        #endregion
        #region <region<強制ボタン>region>>>
        /// <summary>
        /// 強制ボタン：変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonEx_Force_CheckedChanged(object sender, EventArgs e)
        {//強制ボタン=ON
            radioButtonEx_Force.SetChecked(radioButtonEx_Force.Checked);
            _forceBoolFlg = true;

        }
		private bool _forceBoolFlg = false;
		/// <summary>
		/// 強制ボタン：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonEx_Force_Click( object sender, EventArgs e )
		{
            var sw = new System.Diagnostics.Stopwatch();//処理時間計測
            sw.Start();

            if ( _forceBoolFlg ) {
				//ボタン1個でトグルする場合、
				//強制ボタンONの後は「radioButtonEx_Force_CheckedChanged」イベントに
				//入らないので、「Force_Click」イベントでOFFにします。
				_forceBoolFlg = false;
                return;
			}
			if( radioButtonEx_Force.Checked ) {
				///強制ボタン=OFF
				radioButtonEx_Force.SetChecked( false );
                Cursor preCursor = Cursor.Current;
			    Cursor.Current = Cursors.WaitCursor;//ウエイトカーソルに変更
				//強制解除
				_dgList.SetRedraw( true );//再描画：停止
				//InputかOuttputか記録
				bool input = radioButtonEx_Input.Checked;
				int iRows =_dgList.RowCount;
				//プログレス・バー表示
				progressBar1.Visible = true;    //表示
				progressBar1.Minimum = 0;       //最小値
                progressBar1.Maximum = iRows;   //最大値:2880
                progressBar1.Value = 0;         //初期値
                int rowIndex = 0;
                for (; rowIndex < iRows; rowIndex++)
                {//IN=1856,OUT=992
                    string strData = _dgList.Rows[rowIndex].Cells["Address"].Value.ToString();
                    //1データ強制書き込み：COMPIOBITクラス
                     ForceWriteOneData(strData, false, rowIndex);//制御フラグ=OFF

                    //プログレス・バー更新
                    progressBar1.Value = rowIndex; //プログレス・バーは処理時間が約1秒増しになる
                }
                //ShowCheange( input, false);       //表示切替：戻す
                progressBar1.Value = progressBar1.Maximum;
				_dgList.SetRedraw( false );       //再描画：許可
                //通常カーソルに戻す
                Cursor.Current = preCursor;
                progressBar1.Visible = false;       //プログレス・バー：非表示
            }
            sw.Stop();
            TimeSpan ts = sw.Elapsed;
            Console.WriteLine($"　{ts.Hours}時間 {ts.Minutes}分 {ts.Seconds}秒 {ts.Milliseconds}ミリ秒");
            //Console.WriteLine($"　{sw.ElapsedMilliseconds}ミリ秒");
            sw = null;
        }
        #endregion
    }
}
