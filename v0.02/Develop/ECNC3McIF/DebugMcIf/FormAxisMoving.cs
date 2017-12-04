using System;
using System.Windows.Forms;
using ECNC3.Enumeration;
using ECNC3.Models;
using ECNC3.Models.McIf;

namespace DebugMcIf
{
	/// <summary>軸移動フォーム</summary>
	public partial class FormAxisMoving : Form
	{
		/// <summary>読み込み中フラグ</summary>
		private bool isLoading = true;
		/// <summary>コンストラクタ</summary>
		public FormAxisMoving()
		{
			InitializeComponent();
		}
		/// <summary>ロード</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void FormAxisMoving_Load( object sender, EventArgs e )
		{
			_dgPos.RowHeadersVisible = false;
			_dgPos.AllowUserToOrderColumns = false;
			_dgPos.AllowUserToResizeRows = false;
			DataGridViewTextBoxColumn num = _dgPos.Columns["Number"] as DataGridViewTextBoxColumn;
			{
				num.MaxInputLength = 3;
				num.Width = 24;
				num.ReadOnly = true;
			}
			InitCol( _dgPos.Columns["AxisX"] as DataGridViewTextBoxColumn );
			InitCol( _dgPos.Columns["AxisY"] as DataGridViewTextBoxColumn );
			InitCol( _dgPos.Columns["AxisW"] as DataGridViewTextBoxColumn );
			InitCol( _dgPos.Columns["AxisZ"] as DataGridViewTextBoxColumn );
			InitCol( _dgPos.Columns["AxisA"] as DataGridViewTextBoxColumn );
			InitCol( _dgPos.Columns["AxisB"] as DataGridViewTextBoxColumn );
			InitCol( _dgPos.Columns["AxisC"] as DataGridViewTextBoxColumn );
			InitCol( _dgPos.Columns["AxisI"] as DataGridViewTextBoxColumn );
			InitCol( _dgPos.Columns["NoUse"] as DataGridViewTextBoxColumn );
			using( McDatVirtualPosition mc = new McDatVirtualPosition() ) {
				mc.Read();
				_dgPos.Rows.Add( mc.Items.Count );
			}
			Reload();
			isLoading = false;
		}
		/// <summary>グリッドコントロール列初期化</summary>
		/// <param name="target">初期化対象の列オブジェクト</param>
		private void InitCol( DataGridViewTextBoxColumn target )
		{
			target.MaxInputLength = 9;
			target.Width = 65;
			target.ReadOnly = true;
		}
		/// <summary>再表示</summary>
		private void Reload()
		{
			using( McDatVirtualPosition mc = new McDatVirtualPosition() ) {
				mc.Read();
				int index;
				foreach( DataGridViewRow item in _dgPos.Rows ) {
					index = item.Index;
					StructureAxisCoordinate data = mc.Items.Find( ( x ) => x.Number == index );
					item.Cells["Number"].Value = data.Number;
					item.Cells["AxisX"].Value = data.AxisX;
					item.Cells["AxisY"].Value = data.AxisY;
					item.Cells["AxisW"].Value = data.AxisW;
					item.Cells["AxisZ"].Value = data.AxisZ;
					item.Cells["AxisA"].Value = data.AxisA;
					item.Cells["AxisB"].Value = data.AxisB;
					item.Cells["AxisC"].Value = data.AxisC;
					item.Cells["AxisI"].Value = data.Axis8;
					item.Cells["NoUse"].Value = data.Axis9;
				}
			}
		}
		/// <summary>設定ボタンクリックイベント</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void _btnSet_Click( object sender, EventArgs e )
		{
			if( true == _rdoWorkOrigin.Checked ) {
				using( McReqWorkPositionChange mc = new McReqWorkPositionChange() ) {
					if( null == mc.WorkPosition ) {
						mc.WorkPosition = new StructureAxisCoordinate();
					}
					_userCoord.GetParam( mc.WorkPosition );
					mc.Execute();
				}
			} else if( true == _rdoVirPos.Checked ) {
				using( McReqVirtualPositionChange mc = new McReqVirtualPositionChange() ) {
					if( null == mc.VirtualPosition ) {
						mc.VirtualPosition = new StructureAxisCoordinate();
					}
					mc.VirtualPosition.Number = (int)_dgPos.CurrentRow.Cells["Number"].Value;
					_userCoord.GetParam( mc.VirtualPosition );
					mc.Execute();
				}
			} else if( true == _rdoMoving.Checked ) {
				CoordTypes coordType = ( true == _rdoTypeMachine.Checked ) ? CoordTypes.Machine :
										( ( true == _rdoTypeWork.Checked ) ? CoordTypes.Work : CoordTypes.NotSet );
				if( true == _rdoModePtp.Checked ) {
					using( McReqPositioningPointToPoint mc = new McReqPositioningPointToPoint() ) {
						mc.CoordType = coordType;
						_userCoord.GetParam( mc );
						mc.Execute();
					}
				} else if( true == _rdoModeRetractToPtp.Checked ) {
					using( McReqPositioningPointToPointWAxisUpperLimitValid mc = new McReqPositioningPointToPointWAxisUpperLimitValid() ) {
						mc.CoordType = coordType;
						_userCoord.GetParam( mc );
						mc.Execute();
					}
				} else if( true == _rdoModeLiner.Checked ) {
					using( McReqPositioningLinearInterpolation mc = new McReqPositioningLinearInterpolation() ) {
						mc.CoordType = coordType;
						_userCoord.GetParam( mc );
						mc.Feed = (int)_edtFeed.Value;
						mc.Execute();
					}
				}
				return;
			}
			Reload();
		}
		/// <summary>絶対座標設定</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void _rdoMoving_CheckedChanged( object sender, EventArgs e )
		{
			using( McDatStatus mc = new McDatStatus() )
			using( StructureAxisCoordinate coord = mc.Status.CoordinateAsAbsReg ) {
				_userCoord.SetParam( coord );
			}
		}
		/// <summary>ワーク原点設定読出</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void _rdoWorkOrigin_CheckedChanged( object sender, EventArgs e )
		{
			using( McDatWorkOrigin dat = new McDatWorkOrigin() ) {
				dat.Read();
				_userCoord.SetParam( dat.Coordinate );
			}
		}
		/// <summary>仮想点グリッドコントロール 選択位置変更イベント</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void _dgPos_CurrentCellChanged( object sender, EventArgs e )
		{
			if( true == isLoading ) {
				return;
			}
			DataGridView dgv = sender as DataGridView;
			DataGridViewRow row = dgv.CurrentRow;
			int number = (int)row.Cells["Number"].Value;
			using( McDatVirtualPosition mc = new McDatVirtualPosition() ) {
				mc.Read();
				StructureAxisCoordinate data = mc.Items.Find( ( x ) => x.Number == number );
				if( null != data ) {
					_userCoord.SetParam( data );
				}
			}
		}
	}
}
