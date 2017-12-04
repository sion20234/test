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
	/// <summary>ラジオボタン拡張</summary>
	public partial class RadioButtonEx : RadioButton
	{
		private Color _defaultBackColor;
		private Color _defaultForeColor;
        private Color _enabledBackColor;
        private Color _enabledForeColor;
        /// <summary>コンストラクタ</summary>
        public RadioButtonEx()
		{
			InitializeComponent();
			_defaultBackColor = Models.FileUIStyleTable.DefaultBackColor;
			_defaultForeColor = Models.FileUIStyleTable.DefaultForeColor;
            _enabledBackColor = Models.FileUIStyleTable.EnabledBackColor;
            _enabledForeColor = Models.FileUIStyleTable.EnabledForeColor;
            BackColor = _defaultBackColor;
			ForeColor = _defaultForeColor;
			TextAlign = ContentAlignment.MiddleCenter;
			FlatAppearance.MouseDownBackColor = _defaultBackColor;
			FlatAppearance.CheckedBackColor = Models.FileUIStyleTable.EnabledBackColor;
			Font = new System.Drawing.Font( "Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte)( 128 ) ) );
			Click += RadioButtonEx_Click;
		}
		/// <summary>クリックイベント</summary>
		/// <param name="sender">イベント送信元オブジェクト</param>
		/// <param name="e">イベントパラメータ</param>
		private void RadioButtonEx_Click( object sender, EventArgs e )
		{
			
			RadioButtonEx target = sender as RadioButtonEx;
			ECNC3.Models.Common.ECNC3Log logs = new Models.Common.ECNC3Log( "RadioButtonEx_Click" );
			string text = target.Text.Replace( Environment.NewLine, " " );
			logs.Operate( $"CLK,{text}" );
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
				BackColor = _enabledBackColor;
				ForeColor = _enabledForeColor;
				FlatAppearance.MouseDownBackColor = _enabledBackColor;
			}
		}
		/// <summary>メンバ名末尾の数字解析</summary>
		/// <param name="effectiveDigits">有効桁数</param>
		/// <returns>取得結果
		///		<list type="bullet" >
		///			<item>0以上=取得されたインデックス番号</item>
		///			<item>0未満=失敗</item>
		///		</list>
		/// </returns>
		/// <remarks>
		/// メンバ名の末尾の値を解析します。
		/// メンバ名が任意の規則に沿って命名されている必要があります。
		/// </remarks>
		public int GetIndexNumber( int effectiveDigits )
		{
			while( true ) {
				if( effectiveDigits > Name.Length ) {
					break;
				}
				//	有効とされる文字列を抽出
				string name = Name.Substring( Name.Length - effectiveDigits, effectiveDigits );
				int result = -1;
				if( false == int.TryParse( name, out result ) ) {
					break;
				}
				return result;
			}
			return -1;
		}

		private void RadioButtonEx_CheckedChanged( object sender, EventArgs e )
		{
			if( Checked == true ) {
				ForeColor = _enabledForeColor;
			} else {
				ForeColor = _defaultForeColor;
			}
		}
	}
}
