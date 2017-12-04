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

namespace DebugMcIf
{
	public partial class FormAlarm : Form
	{
		public FormAlarm()
		{
			InitializeComponent();
		}

		private void FormAlarm_Load( object sender, EventArgs e )
		{
			_dgList.RowHeadersVisible = false;
			_dgList.AllowUserToOrderColumns = false;
			_dgList.AllowUserToResizeRows = false;
			_dgList.Columns["MemberName"].ValueType = typeof( string );
			_dgList.Columns["IndexNumber"].ValueType = typeof( int );
			_dgList.Columns["BitNumber"].ValueType = typeof( int );

			//_dgList.RowCount = 432;
			int index = 0;
//			int row = -1;
			using( McDatStatus ms = new McDatStatus() )
			using( FileOperatorMessage msg = new FileOperatorMessage() ) {
				ms.Read();
				msg.Read();
				//	全体
				for( index = 0 ; index < 32 ; ++index ) {
					InitInfo( $"MAIN", -1, index, msg.Find( 6100 + index )?.Text, ms.Status.Main.Alarm, 0x0001 << index );
				}
				//	軸
				int axis = 0;
				for( axis = 0 ; axis < 8 ; ++axis ) {
					for( index = 0 ; index < 32 ; ++index ) {
						InitInfo( $"AXIS", axis, index, msg.Find( 6150 + index )?.Text, ms.Status.Axes[axis].AxAlarm, 0x0001 << index );
					}
				}
				//	タスク
				int task = 0;
				for( task = 0 ; task < 8 ; ++task ) {
					for( index = 0 ; index < 32 ; ++index ) {
						InitInfo( $"TASK", task, index, msg.Find( 6200 + index )?.Text, ms.Status.Tasks[task].TaskAlarm, 0x0001 << index );
					}
				}
				//	Alarm2
				for( index = 0 ; index < 16 ; ++index ) {
					InitInfo( $"ECNC", 2, index, msg.Find( 6250 + index )?.Text, ms.Status.Ecnc.Alarm2, 0x0001 << index );
				}
				//	Alarm3
				for( index = 0 ; index < 16 ; ++index ) {
					InitInfo(  $"ECNC", 3, index, msg.Find( 6300 + index )?.Text, ms.Status.Ecnc.Alarm3, 0x0001 << index );
			}
				//	Alarm4
				for( index = 0 ; index < 16 ; ++index ) {
					InitInfo( $"ECNC", 4, index, msg.Find( 6350 + index )?.Text, ms.Status.Ecnc.Alarm4, 0x0001 << index );
			}
				//	Alarm5
				for( index = 0 ; index < 16 ; ++index ) {
					InitInfo(  $"ECNC", 5, index, msg.Find( 6400 + index )?.Text, ms.Status.Ecnc.Alarm5, 0x0001 << index );
			}
				//	EIFErr1
				for( index = 0 ; index < 32 ; ++index ) {
					InitInfo( $"ECAT", 1, index, msg.Find( 6450 + index )?.Text, ms.Status.Ecnc.EIFErr1, 0x0001 << index );
			}
				//	EIFErr2
				for( index = 0 ; index < 32 ; ++index ) {
					InitInfo( $"ECAT", 2, index, msg.Find( 6500 + index )?.Text, ms.Status.Ecnc.EIFErr2, 0x0001 << index );
				}
			}
//			Reload();
		}
		private void InitInfo( string category, int indexNumber, int bitNumber, string text, int value, int mask )
		{
			_dgList.Rows.Add( category, indexNumber, bitNumber, text, ( mask == ( value & mask ) ) ? true : false );
		}

		//enum AlarmTypes
		//{
		//	Main,
		//	Axis,
		//	Task,
		//	Ecnc,
		//}
		//enum AlarmCategories
		//{
		//	Alarm2,
		//	Alarm3,
		//	Alarm4,
		//	Alarm5,
		//	EtherCAT1,
		//	EtherCAT2,
		//	NotUse=0,
		//}
		private void _dgList_CellClick( object sender, DataGridViewCellEventArgs e )
		{
			AidConvert aid = new AidConvert();
			DataGridView dgv = sender as DataGridView;
			string header = dgv.CurrentRow.Cells["MemberName"].Value as string;
			ushort indexNumber = (ushort)(int)dgv.CurrentRow.Cells["IndexNumber"].Value;
			ushort bitNumber = (ushort)(int)dgv.CurrentRow.Cells["BitNumber"].Value;
			//			ushort indexNumber = Parse( dgv.CurrentRow.Cells["IndexNumber"].Value );
			//			ushort bitNumber = Parse( dgv.CurrentRow.Cells["BitNumber"].Value );
			int mask = ( 0x0001 << bitNumber );
			bool state = (bool)dgv.CurrentRow.Cells["State"].Value;

			bool resultState = true == state ? false : true;
			using( McIfDebugEmulation mc = new McIfDebugEmulation() ) {
				if( true == header.StartsWith( "MAIN" ) ) {
					//	全体
					mc.SignalAlarm( McIfDebugEmulation.AlarmKinds.Main, McIfDebugEmulation.AlarmCategories.NotUse, mask, resultState );
				} else if( true == header.StartsWith( "AXIS" ) ) {
					//	軸
					mc.SignalAlarm( McIfDebugEmulation.AlarmKinds.Axis, (AxisNumbers)indexNumber, mask, resultState );
				} else if( true == header.StartsWith( "TASK" ) ) {
					//	タスク
					mc.SignalAlarm( McIfDebugEmulation.AlarmKinds.Task, indexNumber, mask, resultState );
				} else if( true == header.StartsWith( "ECNC" ) ) {
					//	ECNC
					if( 2 == indexNumber ) {
						mc.SignalAlarm( McIfDebugEmulation.AlarmKinds.Ecnc, McIfDebugEmulation.AlarmCategories.Alarm2, mask, resultState );
					} else if( 3 == indexNumber ) {
						mc.SignalAlarm( McIfDebugEmulation.AlarmKinds.Ecnc, McIfDebugEmulation.AlarmCategories.Alarm3, mask, resultState );
					} else if( 4 == indexNumber ) {
						mc.SignalAlarm( McIfDebugEmulation.AlarmKinds.Ecnc, McIfDebugEmulation.AlarmCategories.Alarm4, mask, resultState );
					} else if( 5 == indexNumber ) {
						mc.SignalAlarm( McIfDebugEmulation.AlarmKinds.Ecnc, McIfDebugEmulation.AlarmCategories.Alarm5, mask, resultState );
					}
				} else if( true == header.StartsWith( "ECAT" ) ) {
					//	EtherCAT
					if( 1 == indexNumber ) {
						mc.SignalAlarm( McIfDebugEmulation.AlarmKinds.Ecnc, McIfDebugEmulation.AlarmCategories.EtherCAT1, mask, resultState );
					} else if( 2 == indexNumber ) {
						mc.SignalAlarm( McIfDebugEmulation.AlarmKinds.Ecnc, McIfDebugEmulation.AlarmCategories.EtherCAT2, mask, resultState );
					}
				} else {
				}
			//	Reload();
			}
			dgv.CurrentRow.Cells["State"].Value = resultState;
		}

		private ushort Parse( object target )
		{
			AidConvert aid = new AidConvert();
			int temp;
			ushort result = 0;
			if( true == aid.TryParse( target as string, out temp ) ) {
				result = (ushort)temp;
			}
			return result;
		}
	}
}
