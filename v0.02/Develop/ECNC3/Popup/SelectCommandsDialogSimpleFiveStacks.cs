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

    public partial class SelectCommandsDialogSimpleFiveStacks : ECNC3Form
    {
        #region Constractor
        public SelectCommandsDialogSimpleFiveStacks(string buttonNameA, string buttonNameB, string buttonNameC, string buttonNameD, string buttonNameE, string buttonNameF)
        {
            InitializeComponent();
            _command1Bt.Text = buttonNameA;
            _command2Bt.Text = buttonNameB;
            _command3Bt.Text = buttonNameC;
            _command4Bt.Text = buttonNameD;
            _command5Bt.Text = buttonNameE;
            _command6Bt.Text = buttonNameF;
            //枠線表示
            SelectFormInit();
        }
        #endregion

        #region VariableMembers
        public ReturnMessage retMessage = ReturnMessage.Cancel;
        #endregion

        #region EventHandler
        private void SelectCommandsDialogSimpleFiveStacks_Load(object sender, EventArgs e)
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

        static public ReturnMessage ShowSubForm(IWin32Window owner, string buttonNameA, string buttonNameB, string buttonNameC, string buttonNameD, string buttonNameE, string buttonNameF)
        {
            SelectCommandsDialogSimpleFiveStacks f = new SelectCommandsDialogSimpleFiveStacks(buttonNameA, buttonNameB, buttonNameC, buttonNameD, buttonNameE, buttonNameF);
            f.ShowDialog(owner);
            ReturnMessage receiveMessage = f.retMessage;
            f.Dispose();
            return receiveMessage;
        }

        private void _commandBt_MouseUp(object sender, MouseEventArgs e)
        {
            string senderName = (sender as ButtonEx).Name;
            switch(senderName)
            {
                case "_command1Bt": retMessage = ReturnMessage.ExecuteA1; break;
                case "_command2Bt": retMessage = ReturnMessage.ExecuteA2; break;
                case "_command3Bt": retMessage = ReturnMessage.ExecuteA3; break;
                case "_command4Bt": retMessage = ReturnMessage.ExecuteA4; break;
                case "_command5Bt": retMessage = ReturnMessage.ExecuteA5; break;
                case "_command6Bt": retMessage = ReturnMessage.ExecuteA6; break;
            }
            this.Close();
        }
    }
}
