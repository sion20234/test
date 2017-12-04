using System;
using System.Windows.Forms;

namespace ECNC3.Views
{
	/// <summary>Button コントロール拡張</summary>
	public class NotSelectableButton : System.Windows.Forms.Button
    {
		/// <summary>コンストラクタ</summary>
        public NotSelectableButton()
        {
            this.SetStyle(ControlStyles.Selectable, false);
        }
    }
}
