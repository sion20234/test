using System.Windows.Forms;
using ECNC3.Enumeration;

namespace DebugMcIf
{
	/// <summary>軸選択コントロール</summary>
	public partial class UserAxisFlags : UserControl
	{
		/// <summary>コンストラクタ</summary>
		public UserAxisFlags()
		{
			InitializeComponent();
		}
		/// <summary>選択状態</summary>
		public AxisBits Selected
		{
			get
			{
				AxisBits temp = AxisBits.Free;
				if( true == _chkAxisX.Checked ) {
					temp |= AxisBits.X;
				}
				if( true == _chkAxisY.Checked ) {
					temp |= AxisBits.Y;
				}
				if( true == _chkAxisW.Checked ) {
					temp |= AxisBits.W;
				}
				if( true == _chkAxisZ.Checked ) {
					temp |= AxisBits.Z;
				}
				if( true == _chkAxisA.Checked ) {
					temp |= AxisBits.A;
				}
				if( true == _chkAxisB.Checked ) {
					temp |= AxisBits.B;
				}
				if( true == _chkAxisC.Checked ) {
					temp |= AxisBits.C;
				}
				if( true == _chkAxisI.Checked ) {
					temp |= AxisBits.I;
				}
				return temp;
			}
			set
			{
				_chkAxisX.Checked = value.HasFlag( AxisBits.X );
				_chkAxisY.Checked = value.HasFlag( AxisBits.Y );
				_chkAxisW.Checked = value.HasFlag( AxisBits.W );
				_chkAxisZ.Checked = value.HasFlag( AxisBits.Z );
				_chkAxisA.Checked = value.HasFlag( AxisBits.A );
				_chkAxisB.Checked = value.HasFlag( AxisBits.B );
				_chkAxisC.Checked = value.HasFlag( AxisBits.C );
				_chkAxisI.Checked = value.HasFlag( AxisBits.I );
			}
		}
	}
}
