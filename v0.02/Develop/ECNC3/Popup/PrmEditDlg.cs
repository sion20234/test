using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECNC3.Views.Popup
{
    public partial class PrmEditDlg : ECNC3Form
    {
        public PrmEditDlg()
        {
            InitializeComponent();
            SelectFormInit();
        }

        public void InitEditing(string title, string limit, string before)
        {
            _titleLabel.Text = title;
            _valueLimitLabel.Text = limit;
            _beforeValueLabel.Text = before;
            _editValueText.Text = "";
        }

        public string GetValueText()
        {
            return _editValueText.Text;
        }

        private void buttonEx1_Click(object sender, EventArgs e)
        {
            _editValueText.Text = _beforeValueLabel.Text;
            this.Close();
        }

        private void buttonEx2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
