using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECNC3.Views.Popup
{
    public partial class BitEditDialog : ECNC3Form
    {
        static public string ShowSubForm(Form parent, string title, string value, int bitCount)
        {
            BitEditDialog f = new BitEditDialog(title, value, bitCount);
            f.ShowDialog(parent);
            string receiveMessage = value;
            if (!f.cancelSygnal)
            {
                receiveMessage = f.GetAllSwich();
            }
            f.Dispose();
            return receiveMessage;
        }
        static public string ShowSubForm(string title, string value, int bitCount)
        {
            BitEditDialog f = new BitEditDialog(title, value, bitCount);
            f.ShowDialog();
            string receiveMessage = value;
            if (!f.cancelSygnal)
            {
                receiveMessage = f.GetAllSwich();
            }
            f.Dispose();
            return receiveMessage;
        }
        #region Constractor
        public BitEditDialog(string title, string value, int bitCount)
        {
            InitializeComponent();
            
            _title = title;
            _value = value;
            _bitCount = bitCount;
            if(bitCount != 64)
            {
                this.Size = new Size(814, 397);
                _CancelBtn.Location = new Point(199, 339);
                _OKBtn.Location = new Point(483, 339);
            }
            
        }
        #endregion

        #region VariableMembers
        private string _value = "";
        private string _title = "";
        private int _bitCount = 0;
        private bool cancelSygnal = false;
        public Color SwichOFFBackColor = Models.FileUIStyleTable.DefaultBackColor;
        public Color SwichOFFForeColor = Models.FileUIStyleTable.DefaultForeColor;
        public Color SwichONBackColor = Models.FileUIStyleTable.EnabledBackColor;
        public Color SwichONForeColor = Models.FileUIStyleTable.EnabledForeColor;


        #endregion

        #region EventHandler
        private void BitEditDialog_Load(object sender, EventArgs e)
        {
            _titleLabel.Text = _title;
            InitializeSwichies();
        }
        #endregion

        #region SwichMethods

        private void InitializeSwichies()
        {
            UpdateSwich();
        }
        private void Swiching(PanelEx lamp)
        {
            foreach(Control ctrl in this.Controls)
            {
                Type ctrlType = ctrl.GetType();
                if (ctrlType != typeof(PanelEx)) continue;
                
                if (((PanelEx)(ctrl)) == lamp)
                {
                    if(lamp.BackColor == SwichOFFBackColor)
                    {
                        lamp.BackColor = SwichONBackColor;
                        lamp.ForeColor = SwichONForeColor;
                    }
                    else
                    {
                        lamp.BackColor = SwichOFFBackColor;
                        lamp.ForeColor = SwichOFFForeColor;
                    }
                    return;
                }
            }
        }


        private string GetAllSwich()
        {
            string retSwichStatus = "";
            retSwichStatus += GetSwich(_D31Panel);
            retSwichStatus += GetSwich(_D30Panel);
            retSwichStatus += GetSwich(_D29Panel);
            retSwichStatus += GetSwich(_D28Panel);
            retSwichStatus += GetSwich(_D27Panel);
            retSwichStatus += GetSwich(_D26Panel);
            retSwichStatus += GetSwich(_D25Panel);
            retSwichStatus += GetSwich(_D24Panel);
            retSwichStatus += GetSwich(_D23Panel);
            retSwichStatus += GetSwich(_D22Panel);
            retSwichStatus += GetSwich(_D21Panel);
            retSwichStatus += GetSwich(_D20Panel);
            retSwichStatus += GetSwich(_D19Panel);
            retSwichStatus += GetSwich(_D18Panel);
            retSwichStatus += GetSwich(_D17Panel);
            retSwichStatus += GetSwich(_D16Panel);
            retSwichStatus += GetSwich(_D15Panel);
            retSwichStatus += GetSwich(_D14Panel);
            retSwichStatus += GetSwich(_D13Panel);
            retSwichStatus += GetSwich(_D12Panel);
            retSwichStatus += GetSwich(_D11Panel);
            retSwichStatus += GetSwich(_D10Panel);
            retSwichStatus += GetSwich(_D09Panel);
            retSwichStatus += GetSwich(_D08Panel);
            retSwichStatus += GetSwich(_D07Panel);
            retSwichStatus += GetSwich(_D06Panel);
            retSwichStatus += GetSwich(_D05Panel);
            retSwichStatus += GetSwich(_D04Panel);
            retSwichStatus += GetSwich(_D03Panel);
            retSwichStatus += GetSwich(_D02Panel);
            retSwichStatus += GetSwich(_D01Panel);
            retSwichStatus += GetSwich(_D00Panel);
            
            retSwichStatus = Convert.ToInt32(retSwichStatus, 2).ToString();

            return retSwichStatus;
        }
        private string GetSwich(PanelEx lamp)
        {
            if(lamp.BackColor == SwichOFFBackColor)
            {
                return "0";
            }
            else
            {
                return "1";
            }
        }

        private void UpdateSwich()
        {
            int value = Convert.ToInt32(_value);
            string retvalue = Convert.ToString( value , 2);
            int ctMax = 32 - retvalue.Length;
            for (int ct = 0; ct < ctMax; ct++)
            {
                retvalue = retvalue.Insert(0, "0");
            }
            char[] valueArray= retvalue.ToCharArray();

            if (valueArray[31] == '1') Swiching(_D00Panel);
            if (valueArray[30] == '1') Swiching(_D01Panel);
            if (valueArray[29] == '1') Swiching(_D02Panel);
            if (valueArray[28] == '1') Swiching(_D03Panel);
            if (valueArray[27] == '1') Swiching(_D04Panel);
            if (valueArray[26] == '1') Swiching(_D05Panel);
            if (valueArray[25] == '1') Swiching(_D06Panel);
            if (valueArray[24] == '1') Swiching(_D07Panel);
            if (valueArray[23] == '1') Swiching(_D08Panel);
            if (valueArray[22] == '1') Swiching(_D09Panel);
            if (valueArray[21] == '1') Swiching(_D10Panel);
            if (valueArray[20] == '1') Swiching(_D11Panel);
            if (valueArray[19] == '1') Swiching(_D12Panel);
            if (valueArray[18] == '1') Swiching(_D13Panel);
            if (valueArray[17] == '1') Swiching(_D14Panel);
            if (valueArray[16] == '1') Swiching(_D15Panel);
            if (valueArray[15] == '1') Swiching(_D16Panel);
            if (valueArray[14] == '1') Swiching(_D17Panel);
            if (valueArray[13] == '1') Swiching(_D18Panel);
            if (valueArray[12] == '1') Swiching(_D19Panel);
            if (valueArray[11] == '1') Swiching(_D20Panel);
            if (valueArray[10] == '1') Swiching(_D21Panel);
            if (valueArray[9] == '1') Swiching(_D22Panel);
            if (valueArray[8] == '1') Swiching(_D23Panel);
            if (valueArray[7] == '1') Swiching(_D24Panel);
            if (valueArray[6] == '1') Swiching(_D25Panel);
            if (valueArray[5] == '1') Swiching(_D26Panel);
            if (valueArray[4] == '1') Swiching(_D27Panel);
            if (valueArray[3] == '1') Swiching(_D28Panel);
            if (valueArray[2] == '1') Swiching(_D29Panel);
            if (valueArray[1] == '1') Swiching(_D30Panel);
            if (valueArray[0] == '1') Swiching(_D31Panel);
        }

        #endregion

        #region Tools

        #endregion

        private void _D00Btn_Click(object sender, EventArgs e)
        {
            Swiching(_D00Panel);
        }

        private void _D01Btn_Click(object sender, EventArgs e)
        {
            Swiching(_D01Panel);
        }

        private void _D02Btn_Click(object sender, EventArgs e)
        {
            Swiching(_D02Panel);
        }

        private void _D03Btn_Click(object sender, EventArgs e)
        {
            Swiching(_D03Panel);
        }

        private void _D04Btn_Click(object sender, EventArgs e)
        {
            Swiching(_D04Panel);
        }

        private void _D05Btn_Click(object sender, EventArgs e)
        {
            Swiching(_D05Panel);
        }

        private void _D06Btn_Click(object sender, EventArgs e)
        {
            Swiching(_D06Panel);
        }

        private void _D07Btn_Click(object sender, EventArgs e)
        {
            Swiching(_D07Panel);
        }

        private void _D08Btn_Click(object sender, EventArgs e)
        {
            Swiching(_D08Panel);
        }

        private void _D09Btn_Click(object sender, EventArgs e)
        {
            Swiching(_D09Panel);
        }

        private void _D10Btn_Click(object sender, EventArgs e)
        {
            Swiching(_D10Panel);
        }

        private void _D11Btn_Click(object sender, EventArgs e)
        {
            Swiching(_D11Panel);
        }

        private void _D12Btn_Click(object sender, EventArgs e)
        {
            Swiching(_D12Panel);
        }

        private void _D13Btn_Click(object sender, EventArgs e)
        {
            Swiching(_D13Panel);
        }

        private void _D14Btn_Click(object sender, EventArgs e)
        {
            Swiching(_D14Panel);
        }

        private void _D15Btn_Click(object sender, EventArgs e)
        {
            Swiching(_D15Panel);
        }

        private void _D16Btn_Click(object sender, EventArgs e)
        {
            Swiching(_D16Panel);
        }

        private void _D17Btn_Click(object sender, EventArgs e)
        {
            Swiching(_D17Panel);
        }

        private void _D18Btn_Click(object sender, EventArgs e)
        {
            Swiching(_D18Panel);
        }

        private void _D19Btn_Click(object sender, EventArgs e)
        {
            Swiching(_D19Panel);
        }

        private void _D20Btn_Click(object sender, EventArgs e)
        {
            Swiching(_D20Panel);
        }

        private void _D21Btn_Click(object sender, EventArgs e)
        {
            Swiching(_D21Panel);
        }

        private void _D22Btn_Click(object sender, EventArgs e)
        {
            Swiching(_D22Panel);
        }

        private void _D23Btn_Click(object sender, EventArgs e)
        {
            Swiching(_D23Panel);
        }

        private void _D24Btn_Click(object sender, EventArgs e)
        {
            Swiching(_D24Panel);
        }

        private void _D25Btn_Click(object sender, EventArgs e)
        {
            Swiching(_D25Panel);
        }

        private void _D26Btn_Click(object sender, EventArgs e)
        {
            Swiching(_D26Panel);
        }

        private void _D27Btn_Click(object sender, EventArgs e)
        {
            Swiching(_D27Panel);
        }

        private void _D28Btn_Click(object sender, EventArgs e)
        {
            Swiching(_D28Panel);
        }

        private void _D29Btn_Click(object sender, EventArgs e)
        {
            Swiching(_D29Panel);
        }

        private void _D30Btn_Click(object sender, EventArgs e)
        {
            Swiching(_D30Panel);
        }

        private void _D31Btn_Click(object sender, EventArgs e)
        {
            Swiching(_D31Panel);
        }

        private void _OKBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _CancelBtn_Click(object sender, EventArgs e)
        {
            cancelSygnal = true;
            this.Close();
        }
    }
}
