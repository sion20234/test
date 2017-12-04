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
    public partial class PanelEx : Panel
    {
        public PanelEx()
        {
            InitializeComponent();

              
        }

        /// <summary>
        /// 枠線の太さ
        /// </summary>
        [Browsable(true)]
        [Description("枠線の太さ")]
        [Category("表示")]
        public int OutLineSize { get; set; }

        /// <summary>
        /// 枠線の色
        /// </summary>
        [Browsable(true)]
        [Description("枠線の色")]
        [Category("表示")]
        public Color OutLineColor { get; set; }
        
        
        private void PanelEx_Paint(object sender, PaintEventArgs e)
        {
            Graphics g;
            g = this.CreateGraphics();
            Pen pen;

            int r = this.ClientRectangle.Right - OutLineSize;
            int b = this.ClientRectangle.Bottom - OutLineSize;
            pen = new Pen(this.OutLineColor, OutLineSize);
            g.DrawLine(pen, 0 + OutLineSize, 0 + OutLineSize, r, 0 + OutLineSize); // 上
            g.DrawLine(pen, 0 + OutLineSize, 0 + OutLineSize, 0 + OutLineSize, b); // 左
            g.DrawLine(pen, r, 0 + OutLineSize, r, b); // 右
            g.DrawLine(pen, 0 + OutLineSize, b, r, b); // 下 
        }
    }
}
