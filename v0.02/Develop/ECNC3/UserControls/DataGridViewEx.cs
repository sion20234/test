using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECNC3.Views
{
	/// <summary>DataGridViewコントロール拡張</summary>
	public partial class DataGridViewEx : DataGridView
	{
		private const int WM_SETREDRAW = 0x000B;

        /// <summary>コンストラクタ</summary>
        public DataGridViewEx()
		{
			InitializeComponent();
        }
		/// <summary>初期化</summary>
		public void Initialize()
		{
            RowHeadersVisible = false;
            AllowUserToAddRows = false;
			AllowUserToOrderColumns = false;
			AllowUserToResizeRows = false;
			ReadOnly = true;
			MultiSelect = false;
			//	書式設定
			EnableHeadersVisualStyles = false;  //	trueを設定するとヘッダの背景色が設定できない！
			ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
			ColumnHeadersHeight = 48;
			ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			ColumnHeadersDefaultCellStyle.BackColor = Models.FileUIStyleTable.DefaultBackColor;
			ColumnHeadersDefaultCellStyle.ForeColor = Models.FileUIStyleTable.DefaultForeColor;
			ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			ColumnHeadersDefaultCellStyle.Font = new Font( "Meiryo UI", 11 );
			//	行
			RowTemplate.Height = 32;
			RowTemplate.ReadOnly = true;
		}
        /// <summary>初期化</summary>
		public void Initialize( float _fontSize, int _rowHeight)
        {
            RowHeadersVisible = false;
            AllowUserToAddRows = false;
            AllowUserToOrderColumns = false;
            AllowUserToResizeRows = false;
            ReadOnly = true;
            MultiSelect = false;
            //	書式設定
            EnableHeadersVisualStyles = false;  //	trueを設定するとヘッダの背景色が設定できない！
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            ColumnHeadersHeight = 48;
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            ColumnHeadersDefaultCellStyle.BackColor = Models.FileUIStyleTable.DefaultBackColor;
            ColumnHeadersDefaultCellStyle.ForeColor = Models.FileUIStyleTable.DefaultForeColor;
            ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ColumnHeadersDefaultCellStyle.Font = new Font("Meiryo UI", _fontSize);
            //	行
            RowTemplate.Height = _rowHeight;
            RowTemplate.ReadOnly = true;
        }
        /// <summary>列の初期化</summary>
        /// <param name="name">初期対象の列名</param>
        /// <param name="type">設定値の型</param>
        /// /// <summary>初期化</summary>
		public void Initialize(float _fontSize, int _rowHeight, bool _readOnly)
        {
            RowHeadersVisible = false;
            AllowUserToAddRows = false;
            AllowUserToOrderColumns = false;
            AllowUserToResizeRows = false;
            ReadOnly = _readOnly;
            MultiSelect = false;
            //	書式設定
            EnableHeadersVisualStyles = false;  //	trueを設定するとヘッダの背景色が設定できない！
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            ColumnHeadersHeight = 48;
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            ColumnHeadersDefaultCellStyle.BackColor = Models.FileUIStyleTable.DefaultBackColor;
            ColumnHeadersDefaultCellStyle.ForeColor = Models.FileUIStyleTable.DefaultForeColor;
            ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ColumnHeadersDefaultCellStyle.Font = new Font("Meiryo UI", _fontSize);
            //	行
            RowTemplate.Height = _rowHeight;
            RowTemplate.ReadOnly = _readOnly;
        }
        /// <summary>列の初期化</summary>
        /// <param name="name">初期対象の列名</param>
        /// <param name="type">設定値の型</param>
        public void InitCol( string name, Type type = null )
		{
			if( true == string.IsNullOrEmpty( name ) ) {
				return;
			}
			if( null == Columns ) {
				return;
			}
			if( 1 > ColumnCount ) {
				return;
			}

			DataGridViewColumn source = Columns[name];
			if( null != type ) {
				source.ValueType = type;
			} else {
				source.ValueType = typeof( int );
			}
			if( ( typeof( int ) == source.ValueType ) || ( typeof( double ) == source.ValueType ) ) {
				source.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
			} else if( typeof( string ) == source.ValueType ) {
				source.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			}
			source.DefaultCellStyle.BackColor = Models.FileUIStyleTable.DefaultBackColor;
			source.DefaultCellStyle.ForeColor = Models.FileUIStyleTable.DefaultForeColor;
            source.DefaultCellStyle.Font = new Font( "Meiryo UI", 11 );
		}

        /// <summary>列の初期化</summary>
        /// <param name="name">初期対象の列名</param>
        /// <param name="type">設定値の型</param>
        public void InitCol(string name, float fontSize, Type type = null)
        {
            if (true == string.IsNullOrEmpty(name))
            {
                return;
            }
            if (null == Columns)
            {
                return;
            }
            if (1 > ColumnCount)
            {
                return;
            }

            DataGridViewColumn source = Columns[name];
            if (null != type)
            {
                source.ValueType = type;
            }
            else
            {
                source.ValueType = typeof(int);
            }
            if ((typeof(int) == source.ValueType) || (typeof(double) == source.ValueType))
            {
                source.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            else if (typeof(string) == source.ValueType)
            {
                source.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            source.DefaultCellStyle.BackColor = Models.FileUIStyleTable.DefaultBackColor;
            source.DefaultCellStyle.ForeColor = Models.FileUIStyleTable.DefaultForeColor;
            source.DefaultCellStyle.Font = new Font("Meiryo UI", fontSize);
        }

        /// <summary>列の初期化</summary>
        /// <param name="name">初期対象の列名</param>
        /// <param name="type">設定値の型</param>
        public void InitCol(string name, float fontSize, DataGridViewContentAlignment editLocate, Type type = null)
        {
            if (true == string.IsNullOrEmpty(name))
            {
                return;
            }
            if (null == Columns)
            {
                return;
            }
            if (1 > ColumnCount)
            {
                return;
            }

            DataGridViewColumn source = Columns[name];
            if (null != type)
            {
                source.ValueType = type;
            }
            else
            {
                source.ValueType = typeof(int);
            }
            if ((typeof(int) == source.ValueType) || (typeof(double) == source.ValueType))
            {
                source.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            else if (typeof(string) == source.ValueType)
            {
                source.DefaultCellStyle.Alignment = editLocate;
            }
            source.DefaultCellStyle.BackColor = Models.FileUIStyleTable.DefaultBackColor;
            source.DefaultCellStyle.ForeColor = Models.FileUIStyleTable.DefaultForeColor;
            source.DefaultCellStyle.Font = new Font("Meiryo UI", fontSize);
        }
        /// <summary>選択行のGUI上への表示(スクロール)</summary>
        /// <remarks>
        /// 選択状態にある行をコントロール上に表示させます。
        /// </remarks>
        public void ShowSelectedRow()
		{
			while( true ) {
				if( null == SelectedCells ) {
					break;
				}
				if( 1 > SelectedCells.Count ) {
					break;  //	選択セルなし
				}
				if( false == SelectedCells[0].Visible ) {
					break;  //	対象行が非表示の場合は何もしない
				}
				if( true == SelectedCells[0].Displayed ) {
					break;  // 対象行が既に画面内に表示されている時は何もしない
				}
				//	表示行を移動する。
				FirstDisplayedScrollingRowIndex = SelectedCells[0].RowIndex;
				break;
			}
		}
		/// <summary>セルの有効性チェック</summary>
		/// <param name="row">チェック行番号</param>
		/// <returns>判定結果</returns>
		public bool VerifyRowsCells( int row )
		{
			if( null != Rows ) {
				if( 0 < RowCount ) {
					if( null != Rows[row].Cells ) {
						if( 0 < Rows[row].Cells.Count ) {
							return true;
						}
					}
				}
			}
			return false;
		}
		/// <summary>行内のセル検索</summary>
		/// <param name="row">検索対象となる行コントロール</param>
		/// <param name="name">セル名</param>
		/// <returns>セルコントロール</returns>
		public DataGridViewCell FindCell( int row, string name )
		{
			if( true == VerifyRowsCells( row ) ) {
				return Rows[row].Cells[name];
			}
			return null;
		}
		/// <summary>再描画設定</summary>
		/// <param name="inhibit">設定内容
		///		<list type="bullet" >
		///			<item>true=禁止</item>
		///			<item>false=許可</item>
		///		</list>
		/// </param>
		public void SetRedraw( bool inhibit )
		{
			if( IsHandleCreated ) {
				if( true == inhibit ) {
					Message msg = Message.Create(
						Handle, WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero );
					NativeWindow window = NativeWindow.FromHandle( Handle );
					window.DefWndProc( ref msg );
				} else {
					if( IsHandleCreated ) {
						Message msg = Message.Create(
							Handle, WM_SETREDRAW, new IntPtr( 1 ), IntPtr.Zero );
						NativeWindow window = NativeWindow.FromHandle( Handle );
						window.DefWndProc( ref msg );
						Invalidate();
					}
				}
			}
		}
        /// <summary>
        //  OnMouseDownオーバーライドメソッド:DataGridViewの最終行は選択不可対応
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
			if( EnableLastIndex ) {
				//最終行が有効
			} else {
				//最終行が無効
				// MouseDownした座標からセルを取得
				HitTestInfo hitTest = this.HitTest( e.X, e.Y );
				//イベントを許可しない最終行は処理中断
				if( hitTest.RowIndex + 1 >= RowCount ) {
					return;
				}
			}
            //基本クラスのメソッドに渡す
            base.OnMouseDown(e); 
        }
		/// <summary>
		/// 最終行が有効：デフォルト＝有効
		/// </summary>
		private bool enableLastIndex = true;
		/// <summary>
		/// 最終行が有効
		/// </summary>
		/// <value>最終行が有効 = true</value>
		[Description( "最終行が有効=true、無効=false" )]//「プロパティ」画面下部メッセージ
		//[DefaultValue( true )]
		public bool EnableLastIndex
		{
			set { enableLastIndex = value; }
			get { return enableLastIndex; }
		}
        
	}
}
