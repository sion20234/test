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
	public partial class TextBoxEx : TextBox
	{
        private Color _defaultBackColor;
        private Color _defaultForeColor;

        public TextBoxEx()
        {
            _defaultBackColor = Models.FileUIStyleTable.DefaultBackColor;
            _defaultForeColor = Models.FileUIStyleTable.DefaultForeColor;
            BackColor = _defaultBackColor;
            ForeColor = _defaultForeColor;

        }
    }
}
