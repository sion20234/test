using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECNC3.Views
{
    public partial class VersionCheckForm : ECNC3Form
    {
        #region Constructor
        public VersionCheckForm()
        {
            InitializeComponent();
            SelectFormInit();
        }
        #endregion
        #region VariableMember
        /// <summary>設定結果取得関数定義</summary>
        public delegate void NotifyReturnDelegate();
        /// <summary>終了通知</summary>
        private NotifyReturnDelegate _notifyReturn = null;
        /// <summary>>設定結果取得</summary>
        public NotifyReturnDelegate NotifyReturn
        {
            set { _notifyReturn = value; }
        }
        #endregion
        #region PrivateMethod
        #region EventHandler
        /// <summary>
        /// フォームロードハンドラ　バージョン情報読み込み⇒表示処理
        /// </summary>
        /// <param name="sender">フォームオブジェクト</param>
        /// <param name="e">イベント</param>
        private void VersionCheckForm_Load(object sender, EventArgs e)
        {
            this.BackColor = Models.FileUIStyleTable.DefaultBackColor;
            this.ForeColor = Models.FileUIStyleTable.DefaultForeColor;
            using (Models.FileVersionInfo verInfo = new Models.FileVersionInfo())
            {
                verInfo.Read();
                _machineNameLabel.Text = "E-CNC";
                _serieseAndCopyrightLabel.Text = verInfo.Series + " Series CNC Software.";
                _uiVersionLabel.Text = "Software Version " + verInfo.UIReleaseVersion;
                _mcVersionLabel.Text = "ROM Version " + verInfo.MCReleaseVersion;
                _copyrightLabel.Text = "Copyright" + verInfo.Copyright;
                _makerNameLabel.Text = "ELENIX INC."; 
            }
            foreach(Control ctrl in this.Controls)
            {
                if(ctrl.GetType() == typeof(LabelEx))
                {
                    (ctrl as LabelEx).BorderStyle = BorderStyle.None;
                }
            }
        }
        private void _closeBt_Click(object sender, EventArgs e)
        {
            _notifyReturn?.Invoke();
        }
        #endregion
        #endregion
    }
}
