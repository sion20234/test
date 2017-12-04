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
	/// <summary>ボタンコントロール拡張</summary>
	public partial class ButtonEx : Button
	{
		private Color _defaultBackColor;
		private Color _defaultForeColor;
		/// <summary>コンストラクタ</summary>
		public ButtonEx()
		{
			InitializeComponent();

			//			SetStyle( ControlStyles.Selectable, false );

			_defaultBackColor = Models.FileUIStyleTable.DefaultBackColor;
			_defaultForeColor = Models.FileUIStyleTable.DefaultForeColor;

			FlatStyle = FlatStyle.Flat;
			FlatAppearance.MouseDownBackColor = _defaultForeColor;
            BackColor = _defaultBackColor;
			ForeColor = _defaultForeColor;
			FlatAppearance.MouseDownBackColor = _defaultBackColor;
			Font = new System.Drawing.Font( "Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte)( 128 ) ) );
			Click += ButtonEx_Click;
		}
		/// <summary>フォーカス移動の要否設定</summary>
		public bool Selectable
		{
			get { return GetStyle( ControlStyles.Selectable ); }
			set { SetStyle( ControlStyles.Selectable, value ); }
		}
		/// <summary>クリックイベント</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ButtonEx_Click( object sender, EventArgs e )
		{
			ButtonEx target = sender as ButtonEx;
			//ECNC3.Models.Common.ECNC3UpdateLog logs = new Models.Common.ECNC3UpdateLog( "ButtonEx_Click" );
			string text = target.Text.Replace( Environment.NewLine, " " );
			//logs.Operate( $"CLK,{text}" );
		}

        /// <summary>
        /// ボタンをランプとして使う場合の色切替
        /// </summary>
        /// <param name="LampOn"></param>
        public void SetLamp(bool LampOn)
        {
            if(LampOn == true)
            {
                ForeColor = _defaultBackColor;
                BackColor = _defaultForeColor;
            }            
            else
            {
                BackColor = _defaultBackColor;
                ForeColor = _defaultForeColor;
            }
        }

        public bool GetLamp()
        {
            if(BackColor == _defaultBackColor)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
	}
}
