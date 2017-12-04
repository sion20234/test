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
    /// <summary>
    /// 返送メッセージ
    /// </summary>
    public enum ReturnMessage
    {
        ExecuteA1,
        ExecuteA2,
        ExecuteA3,
        ExecuteA4,
        ExecuteA5,
        ExecuteA6,
        ExecuteB1,
        ExecuteB2,
        ExecuteB3,
        ExecuteB4,
        Cancel
    }
    public partial class SelectCommandsDialog : ECNC3Form
    {
        #region Constractor
        public SelectCommandsDialog(string title, string[] buttonNameA)
        {
            InitializeComponent();

            _rowCount = buttonNameA.Count();
            Column1.Visible = true;
            Column2.Visible = false;
            _buttonNameA = new string[4];
            for (int ct = 0; ct < 4; ct++)
            {
                if (buttonNameA[ct] != null
                    && buttonNameA[ct] != "")
                {
                    _buttonNameA[ct] = buttonNameA[ct];
                }
                else
                {
                    _buttonNameA[ct] = "";
                }
            }
            Column1.Width = 236;

            _titleLabel.Text = title;
            //枠線表示
            SelectFormInit();
        }
        public SelectCommandsDialog(string title, string[] buttonNameA, ButtonEx parentButton)
        {
            InitializeComponent();

            _rowCount = buttonNameA.Count();
            Column1.Visible = true;
            Column2.Visible = false;
            _buttonNameA = new string[4];
            for (int ct = 0; ct < 4; ct++)
            {
                if (buttonNameA[ct] != null
                    && buttonNameA[ct] != "")
                {
                    _buttonNameA[ct] = buttonNameA[ct];
                }
                else
                {
                    _buttonNameA[ct] = "";
                }
            }
            Column1.Width = 236;

            _titleLabel.Text = title;
            //枠線表示
            SelectFormInit();
            base.targetBtn = parentButton;
            dataGridViewEx1.MultiSelect = false;//複数選択：不可：柏原
        }

        public SelectCommandsDialog(string title,
                                    string[] buttonNameA,
                                    string[] buttonNameB)
        {
            InitializeComponent();

            if (buttonNameA.Count() <= buttonNameB.Count())
            {
                _rowCount = buttonNameB.Count();
            }
            else
            {
                _rowCount = buttonNameA.Count();
            }
            Column1.Visible = true;
            Column2.Visible = true;
            _buttonNameA = new string[4];
            _buttonNameB = new string[4];
            for (int ct = 0; ct < 4; ct++)
            {
                if (buttonNameA[ct] != null
                    && buttonNameA[ct] != "")
                {
                    _buttonNameA[ct] = buttonNameA[ct];
                }
                else
                {
                    _buttonNameA[ct] = "";
                }

                if (buttonNameB[ct] != null
                    && buttonNameB[ct] != "")
                {
                    _buttonNameB[ct] = buttonNameB[ct];
                }
                else
                {
                    _buttonNameB[ct] = "";
                }
            }
            _titleLabel.Text = title;
            OutLineEnable = true;
        }

        #endregion

        #region ValiableMembers
        private int _rowCount = 0;
        private string[] _buttonNameA = null;
        private string[] _buttonNameB = null;
        public ReturnMessage retMessage = ReturnMessage.Cancel;
        #endregion

        #region EventHandler
        private void SelectCommandsDialog_Load(object sender, EventArgs e)
        {
            dataGridViewEx1.RowCount = 4;
            dataGridViewEx1.RowTemplate.MinimumHeight = 50;
            dataGridViewEx1.RowTemplate.Height = 50;

            for (int RowCt = 0; RowCt < 4; RowCt++)
            {
                dataGridViewEx1[0, RowCt].Value = _buttonNameA[RowCt];
                if (Column2.Visible != false)
                {
                    dataGridViewEx1[1, RowCt].Value = _buttonNameB[RowCt];
                }
            }
        }
        private void dataGridViewEx1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewEx1[e.ColumnIndex, e.RowIndex].Value == null)
            {
                return;
            }

            if (e.ColumnIndex == 0)
            {
                switch (e.RowIndex)
                {
                    case 0:
                        retMessage = ReturnMessage.ExecuteA1;
                        break;

                    case 1:
                        retMessage = ReturnMessage.ExecuteA2;
                        break;

                    case 2:
                        retMessage = ReturnMessage.ExecuteA3;
                        break;

                    case 3:
                        retMessage = ReturnMessage.ExecuteA4;
                        break;

                }
            }
            else
            {
                switch (e.RowIndex)
                {
                    case 0:
                        retMessage = ReturnMessage.ExecuteB1;
                        break;

                    case 1:
                        retMessage = ReturnMessage.ExecuteB2;
                        break;

                    case 2:
                        retMessage = ReturnMessage.ExecuteB3;
                        break;

                    case 3:
                        retMessage = ReturnMessage.ExecuteB4;
                        break;

                }
            }
            DisposeMember();
            this.Close();
        }

        private void CancelBt_Click(object sender, EventArgs e)
        {
            DisposeMember();
            this.Close();
        }

        #endregion

        private void DisposeMember()
        {
            //_buttonNameA
            if (_buttonNameA != null)
            {
                for (int ct = 0; ct < _rowCount; ct++)
                {
                    _buttonNameA[ct] = "";
                }
                _buttonNameA = null;
            }

            //_buttonNameB
            if (_buttonNameB != null)
            {
                for (int ct = 0; ct < _rowCount; ct++)
                {
                    _buttonNameB[ct] = "";
                }
                _buttonNameB = null;
            }
        }

        private void SelectCommandsDialog_Shown(object sender, EventArgs e)
        {
            dataGridViewEx1.ClearSelection();
        }

        static public ReturnMessage ShowSubForm(IWin32Window owner, string title, string[] buttonNameA, ButtonEx parentButton = null)
        {
            if(parentButton == null)
            {
                SelectCommandsDialog f = new SelectCommandsDialog(title, buttonNameA);
                f.ShowDialog(owner);
                ReturnMessage receiveMessage = f.retMessage;
                f.Dispose();
                return receiveMessage;
            }
            else
            {
                SelectCommandsDialog f = new SelectCommandsDialog(title, buttonNameA, parentButton);
                f.ShowDialog(owner);
                ReturnMessage receiveMessage = f.retMessage;
                f.Dispose();
                return receiveMessage;
            }
        }
    }
}
