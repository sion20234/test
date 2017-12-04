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

    public partial class SelectCommandsDialogSimple : ECNC3Form
    {
        #region Constractor
        public SelectCommandsDialogSimple(string buttonNameA, string buttonNameB, string buttonNameC)
        {
            InitializeComponent();
            _command1Bt.Text = buttonNameA;
            this.LostFocus += SelectCommandsDialogSimple_LostFocus;
            if (buttonNameB == "") _command2Bt.Enabled = false;
            _command2Bt.Text = buttonNameB;
            _command3Bt.Text = buttonNameC;
            //枠線表示
            SelectFormInit();
        }
        public SelectCommandsDialogSimple(string buttonNameA, string buttonNameB, string buttonNameC, ButtonEx target)
        {
            InitializeComponent();
            base.targetBtn = target;
            this.LostFocus += SelectCommandsDialogSimple_LostFocus;
            _command1Bt.Text = buttonNameA;
            if (buttonNameB == "") _command2Bt.Enabled = false;
            _command2Bt.Text = buttonNameB;
            _command3Bt.Text = buttonNameC;
            //枠線表示
            SelectFormInit();
        }
        #endregion

        #region VariableMembers
        public ReturnMessage retMessage = ReturnMessage.Cancel;
        #endregion

        #region EventHandler
        private void SelectCommandsDialogSimple_Load(object sender, EventArgs e)
        {
        }

        public void CancelBt_Click()
        {
            DisposeMember();
            this.Close();
        }

        #endregion

        private void DisposeMember()
        {

        }

        static public ReturnMessage ShowSubForm(IWin32Window owner, string buttonNameA, string buttonNameB, string buttonNameC)
        {
            SelectCommandsDialogSimple f = new SelectCommandsDialogSimple(buttonNameA, buttonNameB, buttonNameC);
            f.ShowDialog(owner);
            ReturnMessage receiveMessage = f.retMessage;
            f.Dispose();
            return receiveMessage;
        }
        static public ReturnMessage ShowSubForm(IWin32Window owner, string buttonNameA, string buttonNameB, string buttonNameC, ButtonEx target)
        {
            SelectCommandsDialogSimple f = new SelectCommandsDialogSimple(buttonNameA, buttonNameB, buttonNameC, target);
            f.ShowDialog(owner);
            ReturnMessage receiveMessage = f.retMessage;
            f.Dispose();
            return receiveMessage;
        }
        private void SelectCommandsDialogSimple_LostFocus(object sender, EventArgs e)
        {
            if (this.Focused == false)
            {
                this.Close();
            }
        }

        private void _commandBt_MouseUp(object sender, MouseEventArgs e)
        {
            LostFocus -= SelectCommandsDialogSimple_LostFocus;
            if ((sender as ButtonEx).Name == _command1Bt.Name)
            {
                retMessage = ReturnMessage.ExecuteA1;
            }
            else if ((sender as ButtonEx).Name == _command2Bt.Name)
            {
                retMessage = ReturnMessage.ExecuteA2;
            }
            else if ((sender as ButtonEx).Name == _command3Bt.Name)
            {
                retMessage = ReturnMessage.ExecuteA3;
            }
            this.Close();
        }
    }
}