using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ECNC3.Models;

namespace ECNC3.Views.UserControls
{
    public partial class UserControlEx : UserControl
    {
        public UserControlEx()
        {
            InitializeComponent();
            SelectFormInit();
        }
        Pen pen = null;
        int r;
        int b;
        /// <summary>
        /// ツール色表示フラグ
        /// </summary>
        [Browsable(true)]
        [Description("ツール色有効")]
        [Category("表示")]
        public bool ToolColorEnable = false;
        /// <summary>
        /// 枠線の色
        /// </summary>
        [Browsable(true)]
        [Description("枠線有効")]
        [Category("表示")]
        public bool OutLineEnable = false;
        /// <summary>
        /// 枠線の太さ
        /// </summary>
        [Browsable(true)]
        [Description("枠線の太さ")]
        [Category("表示")]
        public int OutLineSize { get; set; }

        private Color OutLineColor { get; set; }
        /// <summary>
        /// 枠線表示（Paintイベントハンドラ）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ECNC3Form_Paint(object sender, PaintEventArgs e)
        {
            if (OutLineEnable == true)
            {
                pen.Color = OutLineColor;
                pen.Width = OutLineSize;
                r = this.ClientRectangle.Right - OutLineSize;
                b = this.ClientRectangle.Bottom - OutLineSize;
                e.Graphics.DrawLine(pen, 0 + OutLineSize, 0 + OutLineSize, r, 0 + OutLineSize); // 上
                e.Graphics.DrawLine(pen, 0 + OutLineSize, 0 + OutLineSize, 0 + OutLineSize, b); // 左
                e.Graphics.DrawLine(pen, r, 0 + OutLineSize, r, b); // 右
                e.Graphics.DrawLine(pen, 0 + OutLineSize, b, r, b); // 下 
            }
        }
        /// <summary>
        /// ボタンをツール色表示する場合の初期化処理
        /// </summary>
        /// <param name="source"></param>
        public void ButtonsToolInit(Control.ControlCollection source)
        {
            if (source == null) return;
            foreach(Control ctrl in source)
            {
                if(ctrl.GetType() == typeof(ButtonEx)) (ctrl as ButtonEx).ToolModeInit(true);
                if (ctrl.HasChildren == true) ButtonsToolInit(ctrl.Controls);
            }
        }
        /// <summary>
        /// 初期化処理
        /// </summary>
        public void SelectFormInit()
        {
            OutLineEnable = true;
            OutLineSize = 3;
            this.BackColor = ToolColorEnable ? FileUIStyleTable.ToolBackColor : FileUIStyleTable.DefaultBackColor;
            this.ForeColor = ToolColorEnable ? FileUIStyleTable.ToolForeColor : FileUIStyleTable.DefaultForeColor;
            this.OutLineColor = ToolColorEnable ? FileUIStyleTable.ToolLineColor : FileUIStyleTable.DefaultLineColor;
            r = this.ClientRectangle.Right - OutLineSize;
            b = this.ClientRectangle.Bottom - OutLineSize;
            if(pen == null)pen = new Pen(this.OutLineColor, OutLineSize);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ECNC3Form_Paint);
            this.ResumeLayout(false);
        }
    }
}
