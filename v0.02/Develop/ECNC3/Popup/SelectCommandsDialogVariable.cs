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

    public partial class SelectCommandsDialogVariable : ECNC3Form
    {
        #region Constractor
        public SelectCommandsDialogVariable(List<string>buttonNameList, int pointX = -1, int pointY = -1)
        {
            InitializeComponent();
            if(pointX >= 0 && pointY >= 0)
            {
                this.Location = new Point(pointX, pointY);
            }
            else
            {
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            _buttonNames = new List<string>(buttonNameList);
            this.LostFocus += SelectCommandsDialogVariable_LostFocus;
            buttonInit();
            //枠線表示
            SelectFormInit();
        }
        public SelectCommandsDialogVariable(List<string> buttonNameList, ButtonEx target, int pointX = -1, int pointY = -1)
        {
            InitializeComponent();
            if (pointX >= 0 && pointY >= 0)
            {
                this.Location = new Point(pointX, pointY);
            }
            else
            {
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            _buttonNames = new List<string>(buttonNameList);
            base.targetBtn = target;
            this.LostFocus += SelectCommandsDialogVariable_LostFocus;
            buttonInit();
            //枠線表示
            SelectFormInit();
        }
        #endregion

        #region VariableMembers
        public string retMessage = "";
        private List<string> _buttonNames = null;
        #endregion

        #region EventHandler
        private void SelectCommandsDialogVariable_Load(object sender, EventArgs e)
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

        static public string ShowSubForm(IWin32Window owner, List<string>buttonNameList, int pointX = -1, int pointY = -1)
        {
            SelectCommandsDialogVariable f = new SelectCommandsDialogVariable(buttonNameList, pointX, pointY);
            f.ShowDialog(owner);
            string receiveMessage = f.retMessage;
            f.Dispose();
            return receiveMessage;
        }
        static public string ShowSubForm(IWin32Window owner, List<string> buttonNameList, ButtonEx target, int pointX = -1, int pointY = -1)
        {
            SelectCommandsDialogVariable f = new SelectCommandsDialogVariable(buttonNameList, target, pointX, pointY);
            f.ShowDialog(owner);
            string receiveMessage = f.retMessage;
            f.Dispose();
            return receiveMessage;
        }
        private void SelectCommandsDialogVariable_LostFocus(object sender, EventArgs e)
        {
            retMessage = "";
            this.Close();
        }

        private void _commandBt_MouseUp(object sender, MouseEventArgs e)
        {
             retMessage = (sender as Control).Text;
            this.Close();
        }
        /// <summary>
        /// ボタンを１つ追加する。
        /// </summary>
        /// <param name="text">表示文字列</param>
        private void _buttonAdd(string text)
        {
            ButtonEx addButton = new ButtonEx();
            addButton.Size = new Size(133, 50);
            addButton.Text = text;
            addButton.MouseUp += _commandBt_MouseUp;
            flowLayoutPanel1.Controls.Add(addButton);
        }
        /// <summary>
        /// ボタンの初期化処理
        /// コンストラクタで設定したボタン名リストの項目数分ボタンを追加する。
        /// </summary>
        private void buttonInit()
        {
            foreach(string buttonText in _buttonNames)
            {
                _buttonAdd(buttonText);
            }
            foreach(Control ctrl in flowLayoutPanel1.Controls)
            {
                ctrl.Location = new Point(ctrl.Location.X + 3, ctrl.Location.Y);
            }
        }
    }
}
