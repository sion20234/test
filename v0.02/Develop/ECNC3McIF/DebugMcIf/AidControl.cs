using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DebugMcIf
{
	/// <summary>コントロール操作支援</summary>
	internal class AidControl
	{
		/// <summary>コンストラクタ</summary>
		public AidControl()
		{
		}
		/// <summary>チェックボックス設定</summary>
		/// <param name="target">対象となるコントロール</param>
		/// <param name="state">OFF／ON状態</param>
		public void SetState( CheckBox target, bool state )
		{
			target.Checked = state;
			BackColor( target, state );
		}
		/// <summary>ボタン設定</summary>
		/// <param name="target">対象となるコントロール</param>
		/// <param name="state">OFF／ON状態</param>
		public void SetState( Button target, bool state )
		{
			BackColor( target, state );
		}
		/// <summary>背景色設定</summary>
		/// <param name="target">対象となるコントロール</param>
		/// <param name="state">OFF／ON状態</param>
		private void BackColor( Control target, bool state )
		{
			target.BackColor = ( true == state ) ? Color.LawnGreen : SystemColors.Control;
		}
		/// <summary>コントロール有効無効状態設定</summary>
		/// <param name="list">コントロールのリスト</param>
		/// <param name="enabled">有効／無効設定</param>
		/// <param name="exclusion">設定からの除外対象</param>
		public void SetEnables( Control.ControlCollection list, bool enabled, Control exclusion )
		{
			foreach( Control item in list ) {
				if( null != exclusion ) {
					if( true == exclusion.Equals( item ) ) {
						continue;
					}
				}
				item.Enabled = enabled;
			}
		}
		/// <summary>3ボタン設定</summary>
		/// <param name="state1">制御対象コントロール1</param>
		/// <param name="state2">制御対象コントロール2</param>
		/// <param name="state3">制御対象コントロール3</param>
		/// <param name="state">設定状態</param>
		public void SetColorRadio3( Control state1, Control state2, Control state3, int state )
		{
			if( 0 == state ) {
				state1.BackColor = Color.LawnGreen;
				state2.BackColor = Color.LightGray;
				state3.BackColor = Color.LightGray;
			} else if( 1 == state ) {
				state1.BackColor = Color.LightGray;
				state2.BackColor = Color.LawnGreen;
				state3.BackColor = Color.LightGray;
			} else if( 2 == state ) {
				state1.BackColor = Color.LightGray;
				state2.BackColor = Color.LightGray;
				state3.BackColor = Color.LawnGreen;
			}
		}
	}
}
