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

namespace ECNC3.Views
{
    public partial class PanelEx : Panel
    {
        public PanelEx()
        {
            InitializeComponent();

            pen = new Pen(this.OutLineColor, OutLineSize);
        }

        /// <summary>
        /// 枠線の太さ
        /// </summary>
        [Browsable(true)]
        [Description("枠線の太さ")]
        [Category("表示")]
        public float OutLineSize
        {
            get
            {
                return pen.Width;
            }
            set
            {
                pen.Width = value;
            }
        }

        /// <summary>
        /// 枠線の色
        /// </summary>
        [Browsable(true)]
        [Description("枠線の色")]
        [Category("表示")]
        public Color OutLineColor
        {
            get
            {
                return pen.Color;
            }
            set
            {
                pen.Color = value;
            }
        }

        Pen pen = new Pen(FileUIStyleTable.SelectedLineColor);
        private void PanelEx_Paint(object sender, PaintEventArgs e)
        {
            float r = this.ClientRectangle.Right - OutLineSize;
            float b = this.ClientRectangle.Bottom - OutLineSize;
            
            e.Graphics.DrawLine(pen, 0 + OutLineSize, 0 + OutLineSize, r, 0 + OutLineSize); // 上
            e.Graphics.DrawLine(pen, 0 + OutLineSize, 0 + OutLineSize, 0 + OutLineSize, b); // 左
            e.Graphics.DrawLine(pen, r, 0 + OutLineSize, r, b); // 右
            e.Graphics.DrawLine(pen, 0 + OutLineSize, b, r, b); // 下 
        }
    }
}
