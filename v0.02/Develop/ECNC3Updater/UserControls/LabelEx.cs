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
	/// <summary>ラベルコントロール拡張</summary>
	public partial class LabelEx : Label
	{
        private Color _defaultBackColor;
        private Color _defaultForeColor;
        private Color _enabledBackColor;
        private Color _enabledForeColor;

        public LabelEx()
		{
			InitializeComponent();

            _defaultBackColor = Models.FileUIStyleTable.DefaultBackColor;
            _defaultForeColor = Models.FileUIStyleTable.DefaultForeColor;
            _enabledBackColor = Models.FileUIStyleTable.EnabledBackColor;
            _enabledForeColor = Models.FileUIStyleTable.EnabledForeColor;
            BorderStyle = BorderStyle.FixedSingle;
            FlatStyle = FlatStyle.Flat;
            BackColor = _defaultBackColor;
            ForeColor = _defaultForeColor;

		}

        /// <summary>
        /// ボタンをランプとして使う場合の色切替
        /// </summary>
        /// <param name="LampOn"></param>
        public void SetLamp(bool LampOn)
        {
            if (LampOn == true)
            {
                BackColor = _enabledBackColor;
                ForeColor = _enabledForeColor;
            }
            else
            {
                BackColor = _defaultBackColor;
                ForeColor = _defaultForeColor;
            }
        }

        public bool GetLamp()
        {
            if (BackColor == _defaultBackColor)
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
