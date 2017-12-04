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
	/// <summary>チェックボタンコントロール拡張</summary>
	public partial class CheckBoxEx : CheckBox
	{
		private Color _defaultBackColor;
		private Color _defaultForeColor;
		/// <summary>コンストラクタ</summary>
		public CheckBoxEx()
		{
			InitializeComponent();
			_defaultBackColor = Models.FileUIStyleTable.DefaultBackColor;
			_defaultForeColor = Models.FileUIStyleTable.DefaultForeColor;
			FlatStyle = FlatStyle.Flat;
			BackColor = _defaultBackColor;
			ForeColor = _defaultForeColor;
			Appearance = Appearance.Button;
			TextAlign = ContentAlignment.MiddleCenter;
			FlatAppearance.MouseDownBackColor = _defaultBackColor;
			FlatAppearance.CheckedBackColor = Color.FromArgb( _defaultBackColor.ToArgb() ^ 0xffffff );
			Font = new System.Drawing.Font( "Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte)( 128 ) ) );
			//	イベント
			Click += CheckBoxEx_Click;
		}

		/// <summary>クリックイベント</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CheckBoxEx_Click( object sender, EventArgs e )
		{
			CheckBoxEx target = sender as CheckBoxEx;
			//ECNC3.Models.Common.ECNC3UpdateLog logs = new Models.Common.ECNC3UpdateLog( "CheckBoxEx_Click" );
			//string text = target.Text.Replace( Environment.NewLine, " " );
			//logs.Operate( $"CLK,{text}" );
			SetChecked( target.Checked );
		}
		/// <summary>ボタン状態の設定</summary>
		/// <param name="state">設定状態</param>
		public void SetChecked( bool state )
		{
			Checked = state;
			if( false == state ) {
				BackColor = _defaultBackColor;
				ForeColor = _defaultForeColor;
				FlatAppearance.MouseDownBackColor = _defaultBackColor;
			} else {
				BackColor = Color.FromArgb( _defaultBackColor.ToArgb() ^ 0xffffff );
				ForeColor = Color.FromArgb( _defaultForeColor.ToArgb() ^ 0xffffff );
				FlatAppearance.MouseDownBackColor = Color.FromArgb( _defaultBackColor.ToArgb() ^ 0xffffff );
			}
		}
	}
}
