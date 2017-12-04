using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ECNC3.Views.UserControls
{
    public partial class LevelGage : UserControl
    {

        readonly Series AmSeries = new Series("電流");
        readonly Series VmSeries = new Series("電圧");
        /// <summary>
        /// グラフデータ取得用変数
        /// </summary>
        public double AValue { get; set; }
        public double VValue { get; set; }
        public string ACatName { get { return CategoryNameA.Text; } set { CategoryNameA.Text = value; } }
        public string VCatName { get { return CategoryNameV.Text; } set { CategoryNameV.Text = value; } }
        internal ControlSize LevelGageSize = ControlSize.Maximum;

        public LevelGage()
        {
            InitializeComponent();

            ACatName = "noname";
            VCatName = "noname";
            initializeChart();
            AValue = 0;
            VValue = 0;
            setDummyData();
        }

        void initializeChart()
        {
            // X軸の属性設定
            Axis ax = chart1.ChartAreas[0].AxisX;
            ax.LabelStyle.Enabled = false;
            ax.MajorGrid.Interval = 2;
            ax.MajorTickMark.Enabled = false;
            ax.MajorGrid.LineColor = Models.FileUIStyleTable.DefaultLineColor;
            ax.MinorGrid.LineColor = Models.FileUIStyleTable.DefaultLineColor;

            // Y軸の属性設定
            Axis ay = chart1.ChartAreas[0].AxisY;
            ay.Minimum = 0;                                                     // 最低    
            ay.Maximum = 999.999;                                               // 最高    
            ay.LabelStyle.Interval = 200;                                       // 温度数値の間隔
            ay.LabelStyle.ForeColor = Models.FileUIStyleTable.DefaultForeColor;
            ay.MajorGrid.Interval = 100;
            ay.MinorGrid.Enabled = true;
            ay.MinorGrid.Interval = 100;                                        // 目盛線　の間隔
            ay.MinorGrid.LineColor = Models.FileUIStyleTable.DefaultLineColor;
            ay.MajorTickMark.Enabled = false;
            ay.MinorTickMark.Enabled = false;

            AmSeries.Points.Add(0);                                             // 初期
            AmSeries.Color = Color.DarkRed;                                     // 棒グラフの色
            AmSeries["MinPixelPointWidth"] = "5";                              // 棒グラフの幅の最小値
            AmSeries.IsValueShownAsLabel = true;
            AmSeries.LabelForeColor = Models.FileUIStyleTable.SelectedLineColor;
            

            VmSeries.Points.Add(0);                                             // 初期
            VmSeries.Color = Color.Gold;                                        // 棒グラフの色
            VmSeries["MinPixelPointWidth"] = "5";                              // 棒グラフの幅の最小値
            VmSeries.IsValueShownAsLabel = true;
            VmSeries.LabelForeColor = Models.FileUIStyleTable.SelectedLineColor;

            chart1.Series.Add(VmSeries);
            chart1.Series.Add(AmSeries);
            chart1.Series[1].SetCustomProperty("PointWidth", "1.5");
            chart1.Series[2].SetCustomProperty("PointWidth", "1.5");
            chart1.BackColor = Models.FileUIStyleTable.DefaultBackColor;                       // グラフ外側の色
            chart1.ChartAreas[0].BackColor = Models.FileUIStyleTable.DefaultBackColor;         // 背景色　グラフ内部 
            chart1.Legends.Clear();                                             // 凡例を非表示にする
           
        }

        public void setDummyData()
        {
            AmSeries.Points.Clear();
            VmSeries.Points.Clear();
            AmSeries.Points.Add(AValue);
            VmSeries.Points.Add(VValue);
            //AValueText.Text = AValue.ToString() + "mA";
            //VValueText.Text = VValue.ToString() + "V";
        }
    }
}
