using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ECNC3.Enumeration;
using ECNC3.Models;
using ECNC3.Models.Common;
using ECNC3.Models.McIf;

namespace ECNC3.Views
{
	/// <summary>アラーム表示</summary>
	public partial class AlarmDialog : ECNC3Form
	{
		/// <summary>マウスのクリック位置を記憶</summary>
		private Point _mousePoint;
		/// <summary>直前の保持値</summary>
		private StructureMcDatStatus _before;
		/// <summary>メッセージファイルアクセス</summary>
		private FileOperatorMessage _fileMessage;
		/// <summary>終了通知</summary>
		private NotifyReturnDelegate _notifyReturn = null;

		/// <summary>設定結果取得関数定義</summary>
		public delegate void NotifyReturnDelegate();

		/// <summary>コンストラクタ</summary>
		public AlarmDialog()
		{
			InitializeComponent();
		}

		/// <summary>>設定結果取得</summary>
		public NotifyReturnDelegate NotifyReturn
		{
			set { _notifyReturn = value; }
		}

		/// <summary>ロードイベント</summary>
		/// <param name="sender">イベント送信元オブジェクト</param>
		/// <param name="e">イベントパラメータ</param>
		private void AlarmDialog_Load( object sender, EventArgs e )
		{
			//	データグリッド初期化
			_dgList.Initialize();
			_dgList.DefaultCellStyle.SelectionBackColor = Color.Transparent;
			_dgList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			//	データ各項目
			_dgList.InitCol( "Code" );
			_dgList.InitCol( "IndexNumber" );
			_dgList.InitCol( "Title", typeof( string ) );
			_dgList.InitCol( "Details", typeof( string ) );
            _dgList.Columns["Title"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            Disposed += AlarmDialog_Disposed;
		}
		/// <summary>破棄イベント</summary>
		/// <param name="sender">イベント送信元オブジェクト</param>
		/// <param name="e">イベントパラメータ</param>
		private void AlarmDialog_Disposed( object sender, EventArgs e )
		{
			if( null != _before ) {
				_before.Dispose();
				_before = null;
			}
			if( null != _notifyReturn ) {
				_notifyReturn = null;
			}
			if( null != _fileMessage ) {
				_fileMessage.Dispose();
				_fileMessage = null;
			}
		}

		/// <summary>【閉じる】ボタンクリックイベントsummary>
		/// <param name="sender">イベント送信元オブジェクト</param>
		/// <param name="e">イベントパラメータ</param>
		private void _btnRetrun_Click( object sender, EventArgs e )
		{
			_notifyReturn?.Invoke();
		}
        /// <summary>表示更新</summary>
		/// <param name="target">更新情報</param>
		public ResultCodes RefreshAlarmOnly(StructureMcDatStatus target)
        {
            while (null != target)
            {
                if(target == null)
                {
                    return ResultCodes.NotFound;
                }
                _dgList.Rows.Clear();
                _dgList.Refresh();
                //	メイン
                //	変数の型は32bitであるが、仕様書で定義される出力範囲は16bitである。
                AddItems(6100, 16, target.Main.Alarm);
                int index;
                //	軸
                for (index = 0; index < (int)AxisNumbers.I; ++index)
                {
                    //	変数の型は32bitであるが、仕様書で定義される出力範囲は16bitである。
                    AddItems(6150, 16, target.Axes[index].AxAlarm, (AxisNumbers)index);
                }
                //	タスク
                for (index = 0; index < 8; ++index)
                {
                    AddItems(6200, 32, target.Tasks[index].TaskAlarm, index);
                }
                //	ECNC.Alarm2
                AddItems(6250, 16, target.Ecnc.Alarm2);
                //	ECNC.Alarm3
                AddItems(6300, 16, target.Ecnc.Alarm3);
                //	ECNC.Alarm4
                AddItems(6350, 16, target.Ecnc.Alarm4);
                //	ECNC.Alarm5
                AddItems(6400, 16, target.Ecnc.Alarm5);
                //	ECNC.EtherCAT1
                AddItems(6450, 32, target.Ecnc.EIFErr1);
                //	ECNC.EtherCAT2
                AddItems(6500, 32, target.Ecnc.EIFErr2);

                _dgList.CurrentCell = null;
                _btnRetrun.Focus();
                break;
            }
            return ResultCodes.Success;
        }
        /// <summary>表示更新</summary>
        /// <param name="target">更新情報</param>
        public void Refresh( StructureMcDatStatus target )
		{
			while( null != target ) {
				if( false == target.HasAlarm ) {
					_notifyReturn?.Invoke();
					break;  //	閉じてよい
				}
				if( null != _before ) {
					//	アラーム情報に差がなければ表示を更新しない。
					if( true == _before.EqualsAlarmOnly( target ) ) {
						break;
					}
				}

				_dgList.Rows.Clear();
				_dgList.Refresh();
				//	メイン
				//	変数の型は32bitであるが、仕様書で定義される出力範囲は16bitである。
				AddItems( 6100, 16, target.Main.Alarm );
				int index;
				//	軸
				for( index = 0 ; index < (int)AxisNumbers.I ; ++index ) {
					//	変数の型は32bitであるが、仕様書で定義される出力範囲は16bitである。
					AddItems( 6150, 16, target.Axes[index].AxAlarm, (AxisNumbers)index );
				}
				//	タスク
				for( index = 0 ; index < 8 ; ++index ) {
					AddItems( 6200, 32, target.Tasks[index].TaskAlarm, index );
				}
				//	ECNC.Alarm2
				AddItems( 6250, 16, target.Ecnc.Alarm2 );
				//	ECNC.Alarm3
				AddItems( 6300, 16, target.Ecnc.Alarm3 );
				//	ECNC.Alarm4
				AddItems( 6350, 16, target.Ecnc.Alarm4 );
				//	ECNC.Alarm5
				AddItems( 6400, 16, target.Ecnc.Alarm5 );
				//	ECNC.EtherCAT1
				AddItems( 6450, 32, target.Ecnc.EIFErr1 );
				//	ECNC.EtherCAT2
				AddItems( 6500, 32, target.Ecnc.EIFErr2 );

				_dgList.CurrentCell = null;
				_btnRetrun.Focus();
				break;
			}
			if( true == target.HasAlarm ) {
				using( ECNC3Settings us = new ECNC3Settings() ) {
					if( BootModes.Desktop != us.BootMode ) {
						TopMost = true;
					}
				}
					
				Visible = true;
				if( null == _before ) {
					_before = new StructureMcDatStatus();
				}
				_before.Copy( target );
			}
		}
    

		/// <summary>複数行追加</summary>
		/// <param name="baseId">メッセージ番号検索基準番号</param>
		/// <param name="size">検索回数</param>
		/// <param name="target">設定値</param>
		/// <param name="taskNumber">タスク番号</param>
		private void AddItems( int baseId, int size, int target, int taskNumber = -1 )
		{
			int mask = 0;
			int index = 0;
			for( index = 0 ; index < size ; ++index ) {
				mask = ( 0x0001 << index );
				if( mask == ( target & mask ) ) {
					AddItem( baseId + index, taskNumber );
				}
			}
		}
		/// <summary>複数行追加</summary>
		/// <param name="baseId">メッセージ番号検索基準番号</param>
		/// <param name="size">検索回数</param>
		/// <param name="target">設定値</param>
		/// <param name="axisNumber">軸番号</param>
		private void AddItems( int baseId, int size, int target, AxisNumbers axisNumber )
		{
			int mask = 0;
			int index = 0;
			for( index = 0 ; index < size ; ++index ) {
				mask = ( 0x0001 << index );
				if( mask == ( target & mask ) ) {
					AddItem( baseId + index, axisNumber );
				}
			}
		}

		/// <summary>行追加</summary>
		/// <param name="code">メッセージID</param>
		/// <param name="taskNumber">タスク番号
		///		<list type="bullet" >
		///			<item>-1=未使用</item>
		///			<item>他=タスク番号</item>
		///		</list>
		/// </param>
		private void AddItem( int code, int taskNumber = -1 )
		{
			if( 0 > taskNumber ) {
				AddItem( code, string.Empty );
			} else {
				AddItem( code, $"TASK{taskNumber}" );
			}
		}
		/// <summary>行追加(軸アラーム)</summary>
		/// <param name="code">メッセージID</param>
		/// <param name="axis">軸番号</param>
		private void AddItem( int code, AxisNumbers axis )
		{
			AddItem( code, $"{axis}" );
		}
		/// <summary>行追加</summary>
		/// <param name="code"></param>
		/// <param name="note"></param>
		private void AddItem( int code, string note )
		{
			if( null != _dgList ) {
				if( null != _dgList.Rows ) {
					if( null == _fileMessage ) {
						_fileMessage = new FileOperatorMessage();
						_fileMessage.Read();
					}
					StructureOperatorMessageItem item = _fileMessage.Find( code );
					_dgList.Rows.Add( $"{code}", note, item?.Title, item?.Text );
				}
			}
		}

		#region マウスイベント
		/// <summary>マウスダウンイベント</summary>
		/// <param name="sender">イベント送信元オブジェクト</param>
		/// <param name="e">イベントパラメータ</param>
		private void AlarmDialog_MouseDown( object sender, MouseEventArgs e )
		{
			if( ( e.Button & MouseButtons.Left ) == MouseButtons.Left ) {
				//位置を記憶する
				_mousePoint = new Point( e.X, e.Y );
			}
		}
		/// <summary>マウス移動イベント</summary>
		/// <param name="sender">イベント送信元オブジェクト</param>
		/// <param name="e">イベントパラメータ</param>
		private void AlarmDialog_MouseMove( object sender, MouseEventArgs e )
		{
			if( ( e.Button & MouseButtons.Left ) == MouseButtons.Left ) {
				this.Left += e.X - _mousePoint.X;
				this.Top += e.Y - _mousePoint.Y;
				//または、つぎのようにする
				//this.Location = new Point(
				//    this.Location.X + e.X - mousePoint.X,
				//    this.Location.Y + e.Y - mousePoint.Y);
			}
		}
        #endregion

        private void ResetBt_Click(object sender, EventArgs e)
        {
            using (Models.McIf.McReqReset Reset = new McReqReset())
            {
                Reset.Execute();
            }
        }
        //マウスクリック位置記憶
        /// <summary>
        /// タイトル・マウス：ダウン
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEx_Title_MouseDown(object sender, MouseEventArgs e)
        {
            TenKeyMouseDown(sender, e);
        }
        /// <summary>
        /// タイトル・マウス：移動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEx_Title_MouseMove(object sender, MouseEventArgs e)
        {
            TenKeyMouseMove(sender, e);
        }
        private Point mousePoint;
        /// <summary>
		/// マウス：ダウン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TenKeyMouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                mousePoint = new Point(e.X, e.Y);//位置を記憶
            }
        }
        /// <summary>
        /// マウス：移動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TenKeyMouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                this.Left += e.X - mousePoint.X;
                this.Top += e.Y - mousePoint.Y;
            }
        }

        private void _dgList_MouseUp(object sender, MouseEventArgs e)
        {
            ////列幅変更時表示バグ対応
            //Visible = false;
            //Visible = true;
        }

        private void _dgList_SelectionChanged(object sender, EventArgs e)
        {
            if (_dgList.SelectedCells == null) return;
            if (_dgList.SelectedCells.Count == 0) return;
            _dgList.ClearSelection();
        }
    }
}
