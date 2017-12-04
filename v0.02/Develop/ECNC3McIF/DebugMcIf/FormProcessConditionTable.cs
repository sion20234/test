using System;
using System.Windows.Forms;
using ECNC3.Models;
using ECNC3.Models.McIf;

namespace DebugMcIf
{
	public partial class FormProcessConditionTable : Form
	{
		/// <summary>読み込み中フラグ</summary>
		private bool isLoading = true;
		/// <summary>コンストラクタ</summary>
		public FormProcessConditionTable()
		{
			InitializeComponent();
		}
		/// <summary>ロード</summary>
		/// <param name="sender">イベント送信元オブジェクト</param>
		/// <param name="e">イベントパラメータ</param>
		private void FormProcessConditionTable_Load( object sender, EventArgs e )
		{
			_dgCond.RowHeadersVisible = false;
			_dgCond.AllowUserToOrderColumns = false;
			_dgCond.AllowUserToResizeRows = false;
			//	DataGridViewTextBoxColumn num = _dgCond.Columns["Number"] as DataGridViewTextBoxColumn;
			//	{
			//		num.MaxInputLength = 3;
			//		num.Width = 24;
			//		num.ReadOnly = true;
			//	}
			//	InitCol( _dgCond.Columns["Number"] as DataGridViewTextBoxColumn );
			DataGridViewButtonColumn btn = _dgCond.Columns["Number"] as DataGridViewButtonColumn;
			{
				btn.Width = 30;
			}
			InitCol( _dgCond.Columns["TurnOn"] as DataGridViewTextBoxColumn );
			InitCol( _dgCond.Columns["TurnOff"] as DataGridViewTextBoxColumn );
			InitCol( _dgCond.Columns["Cap"] as DataGridViewTextBoxColumn );
			InitCol( _dgCond.Columns["ServoControl"] as DataGridViewTextBoxColumn );
			InitCol( _dgCond.Columns["Crs"] as DataGridViewTextBoxColumn );
			InitCol( _dgCond.Columns["SfrFront"] as DataGridViewTextBoxColumn );
			InitCol( _dgCond.Columns["SfrBack"] as DataGridViewTextBoxColumn );
			InitCol( _dgCond.Columns["Inverter"] as DataGridViewTextBoxColumn );
			InitCol( _dgCond.Columns["ServoSelect"] as DataGridViewTextBoxColumn );
			InitCol( _dgCond.Columns["PowerSupply"] as DataGridViewTextBoxColumn );
			InitCol( _dgCond.Columns["Pol"] as DataGridViewTextBoxColumn );
			InitCol( _dgCond.Columns["JumpDown"] as DataGridViewTextBoxColumn );
			InitCol( _dgCond.Columns["JumpUp"] as DataGridViewTextBoxColumn );
			InitCol( _dgCond.Columns["Material"] as DataGridViewTextBoxColumn );
			InitCol( _dgCond.Columns["Diameter"] as DataGridViewTextBoxColumn, typeof( double ) );
			InitCol( _dgCond.Columns["Ip"] as DataGridViewTextBoxColumn );
			InitCol( _dgCond.Columns["Comment"] as DataGridViewTextBoxColumn, typeof( string ) );
			using( McDatProcessConditionTable mc = new McDatProcessConditionTable() ) {
				mc.Read();
				_dgCond.Rows.Add( mc.Items.Count );
			}
			Reload();
			isLoading = false;
		}
		/// <summary>グリッドコントロール列初期化</summary>
		/// <param name="target">初期化対象の列オブジェクト</param>
		private void InitCol( DataGridViewTextBoxColumn target, Type type = null )
		{
			if( null != type ) {
				//	個別設定
				target.ValueType = type;
			} else {
				//	デフォルト
				target.ValueType = typeof( int );
				target.MaxInputLength = 9;
				target.Width = 30;
			}
			//target.ReadOnly = true;
		}
		/// <summary>再表示</summary>
		private void Reload()
		{
			using( McDatProcessConditionTable mc = new McDatProcessConditionTable() ) {
				mc.Read();
				int index;
				foreach( DataGridViewRow item in _dgCond.Rows ) {
					index = item.Index;
					StructureProcessConditionItem data = mc.Items.Find( ( x ) => x.Number == index );
					item.Cells["Number"].Value = data.Number;
					item.Cells["TurnOn"].Value = data.Ton;
					item.Cells["TurnOff"].Value = data.Toff;
					item.Cells["Cap"].Value = data.CAPVal;
					item.Cells["ServoControl"].Value = data.SCVal;
					item.Cells["Crs"].Value = data.CRSVal;
					item.Cells["SfrFront"].Value = data.SfrFrSel;
					item.Cells["SfrBack"].Value = data.SfrBkSel;
					item.Cells["PompValue"].Value = data.PompVal;
					item.Cells["ServoSelect"].Value = data.ServoSel;
					item.Cells["PowerSupply"].Value = data.PSSel;
					item.Cells["Pol"].Value = data.POLVal;
					item.Cells["Comment"].Value = data.Comment;
					item.Cells["Material"].Value = data.Material;
					item.Cells["Diameter"].Value = data.Diameter;
					item.Cells["Ip"].Value = data.IPVal;
				}
			}
		}

		private void _dgCond_CurrentCellChanged( object sender, EventArgs e )
		{
			if( true == isLoading ) {
				return;
			}
		}
		/// <summary>編集確定イベント</summary>
		/// <param name="sender">イベント送信元オブジェクト</param>
		/// <param name="e">イベントパラメータ</param>
		private void _dgCond_CellEndEdit( object sender, DataGridViewCellEventArgs e )
		{
			if( true == isLoading ) {
				return;
			}
			DataGridView dgv = sender as DataGridView;
			DataGridViewRow row = dgv.CurrentRow;

			using( StructureProcessConditionItem data = new StructureProcessConditionItem() )
			using( McDatProcessConditionTable mc = new McDatProcessConditionTable() ) {
				data.Number = GetValue( row, "Number" );
				data.Ton = (short)GetValue( row, "TurnOn" );
				data.Toff = (short)GetValue( row, "TurnOff" );
				data.CAPVal = GetValue( row, "Cap" );
				data.SCVal = (short)GetValue( row, "ServoControl" );
				data.CRSVal = (short)GetValue( row, "Crs" );
				data.SfrFrSel = (short)GetValue( row, "SfrFront" );
				data.SfrBkSel = (short)GetValue( row, "SfrBack" );
				data.PompVal = (short)GetValue( row, "PompValue" );
				data.ServoSel = (short)GetValue( row, "ServoSelect" );
				data.PSSel = (short)GetValue( row, "PowerSupply" );
				data.POLVal = (short)GetValue( row, "Pol" );
				//data.JumpDown = GetValue( row, "JumpDown" );
				//data.JumpUp = GetValue( row, "JumpUp" );
				data.Comment = row.Cells["Comment"].Value as string;
				data.Material = GetValue( row, "Material" );
				data.Diameter = GetDouble( row, "Diameter" );
				data.IPVal = GetValue( row, "Ip" );

				mc.Write( data, true );
			}
		}

		private int GetValue( DataGridViewRow row, string name )
		{
			try {
				while( null != row ) {
					if( true == string.IsNullOrEmpty( name ) ) {
						break;
					}
					if( null == row.Cells ) {
						break;
					}
					if( null == row.Cells[name] ) {
						break;
					}
					if( null == row.Cells[name].Value ) {
						break;
					}
					string temp = row.Cells[name].Value.ToString();
					int result;
					if( false == int.TryParse( temp, out result ) ) {
						break;
					}
					return result;
				}
			} catch( Exception e ) {
				if( ( e is ArgumentException ) ||
					( e is ArgumentNullException ) ||
					( e is InvalidOperationException ) ) {
					;
				}
			} finally {
				;
			}
			return 0;
		}
		private double GetDouble( DataGridViewRow row, string name )
		{
			try {
				while( null != row ) {
					if( true == string.IsNullOrEmpty( name ) ) {
						break;
					}
					if( null == row.Cells ) {
						break;
					}
					if( null == row.Cells[name] ) {
						break;
					}
					if( null == row.Cells[name].Value ) {
						break;
					}
					return (double)row.Cells[name].Value;
				}
			} catch( Exception e ) {
				if( ( e is ArgumentException ) ||
					( e is ArgumentNullException ) ||
					( e is InvalidOperationException ) ) {
					;
				}
			} finally {
				;
			}
			return 0.0;
		}
		/// <summary>編集検証イベント</summary>
		/// <param name="sender">イベント送信元オブジェクト</param>
		/// <param name="e">イベントパラメータ</param>
		private void _dgCond_CellValidating( object sender, DataGridViewCellValidatingEventArgs e )
		{
			if( true == isLoading ) {
				return;
			}
			DataGridView dgv = sender as DataGridView;
			Type type = dgv.CurrentCell.ValueType;
			string formattedValue = e.FormattedValue as string;
			while( true ) {
				if( typeof( int ) == type ) {
					if( false == string.IsNullOrEmpty( formattedValue ) ) {
						int result = 0;
						if( false == int.TryParse( formattedValue, out result ) ) {
							break;
						}
					}
				} else if( typeof( double ) == type ) {
					if( false == string.IsNullOrEmpty( formattedValue ) ) {
						double result = 0;
						if( false == double.TryParse( formattedValue, out result ) ) {
							int result1 = 0;
							if( false == int.TryParse( formattedValue, out result1 ) ) {
								break;
							}
						}
					}
				}
				return;
			}
			e.Cancel = true;
		}
		/// <summary>
		/// セルクリックイベント
		/// </summary>
		/// <param name="sender">イベント送信元オブジェクト</param>
		/// <param name="e">イベントパラメータ</param>
		private void _dgCond_CellClick( object sender, DataGridViewCellEventArgs e )
		{
			if( true == isLoading ) {
				return;
			}
			while( true ) {
				DataGridView dgv = sender as DataGridView;
				int number;
				if( false == int.TryParse( dgv.CurrentCell.Value.ToString(), out number ) ) {
					break; //	値でないので無視
				}
				int currentNumber;
				using( McDatProcessCondition mc = new McDatProcessCondition() ) {
					mc.Read();
					currentNumber = mc.PNo;
				}
				if( number == currentNumber ) {
					break;
				}
				if( DialogResult.Yes != MessageBox.Show( $"Change processing conditions.{Environment.NewLine}"
					+ $"#{currentNumber} -> #{number}.", "CHECK",
					MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2 ) ) {
					break;
				}
				using( McReqProcessConditionNumberSelect mc = new McReqProcessConditionNumberSelect() ) {
					mc.SelectingNumber = number;
					mc.Execute();
				}
				return;
			}
		}
	}
}
