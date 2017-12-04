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
using System.Diagnostics;

namespace ECNC3.Views.UserControls
{
    public partial class Plot : UserControlEx
    {
        public Plot()
        {
            InitializeComponent();

            // チャートの表示を初期化
            initChart(chart1);
            //枠表示
            base.OutLineEnable = true;
        }

        // 取得データの履歴
        const int MAX_HISTORY = 40;
        Queue<double> countHistory = new Queue<double>();

        public void SetPlotValue(double value)
        {
            countHistory.Enqueue(value);

            //------------------------------------------------
            // 履歴の最大数を超えていたら、古いものを削除する
            //------------------------------------------------
            while (countHistory.Count > MAX_HISTORY)
            {
                countHistory.Dequeue();
            }

            //------------------------------------------------
            // グラフを再描画する
            //------------------------------------------------
            showChart(chart1);
        }


        private void initChart(Chart chart)
        {
            // チャート全体の背景色を設定
            chart.BackColor = Models.FileUIStyleTable.DefaultBackColor;
            chart.ChartAreas[0].BackColor = Color.Transparent;

            // チャート表示エリア周囲の余白をカットする
            chart.ChartAreas[0].InnerPlotPosition.Auto = false;
            chart.ChartAreas[0].InnerPlotPosition.Width = 100; // 100%
            chart.ChartAreas[0].InnerPlotPosition.Height = 90;  // 90%(横軸のメモリラベル印字分の余裕を設ける)
            chart.ChartAreas[0].InnerPlotPosition.X = 8;
            chart.ChartAreas[0].InnerPlotPosition.Y = 0;


            // X,Y軸情報のセット関数を定義
            Action<Axis> setAxis = (axisInfo) => {
                // 軸のメモリラベルのフォントサイズ上限値を制限
                axisInfo.LabelAutoFitMaxFontSize = 8;

                // 軸のメモリラベルの文字色をセット
                axisInfo.LabelStyle.ForeColor = Models.FileUIStyleTable.DefaultForeColor;

                // 軸タイトルの文字色をセット(今回はTitle未使用なので関係ないが...)
                axisInfo.TitleForeColor = Models.FileUIStyleTable.DefaultForeColor;

                // 軸の色をセット
                axisInfo.MajorGrid.Enabled = true;
                axisInfo.MajorGrid.LineColor = Models.FileUIStyleTable.DefaultLineColor;
                axisInfo.MinorGrid.Enabled = false;
                axisInfo.MinorGrid.LineColor = Models.FileUIStyleTable.DefaultLineColor;
            };

            
            

            // X,Y軸の表示方法を定義
            setAxis(chart.ChartAreas[0].AxisY);
            setAxis(chart.ChartAreas[0].AxisX);
            chart.ChartAreas[0].AxisX.MinorGrid.Enabled = true;
            chart.ChartAreas[0].AxisY.Maximum = 999.999;    // 縦軸の最大値を100にする

            chart.AntiAliasing = AntiAliasingStyles.None;

            // 折れ線グラフとして表示
            chart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            // 線の色を指定
            chart.Series[0].Color = Models.FileUIStyleTable.SelectedLineColor;
			chart.Series[0].BorderWidth = 2;//線幅＝１から２に変更
			// 凡例を非表示,各値に数値を表示しない
			chart.Series[0].IsVisibleInLegend = false;
            chart.Series[0].IsValueShownAsLabel = false;

            // チャートに表示させる値の履歴を全て0クリア
            while (countHistory.Count <= MAX_HISTORY)
            {
                countHistory.Enqueue(0);
            }
        }


        //***************************************************************************
        /// <summary> チャートを描画する
        /// </summary>
        /// <param name="chart"></param>
        //***************************************************************************
        private void showChart(Chart chart)
        {

            //-----------------------
            // チャートに値をセット
            //-----------------------
            chart.Series[0].Points.Clear();
            foreach (double value in countHistory)
            {

                // データをチャートに追加
                chart.Series[0].Points.Add(new DataPoint(0, value));
            }
        }
		/// <summary>
		/// ManualFormにイベント通知
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void chart1_Click( object sender, EventArgs e )
		{
			//呼び出し先に通知
			_plotNotify.Invoke();
			//「MDIAUTOForm」からも呼ばれるので、
			//「MDIAUTOForm」の「plot1」の「Enabled」は「false」で
			//クリック時ここが呼ばれない様にしてあります。
		}
		/// <summary>設定結果取得関数定義</summary>
		public delegate void PlotNotifyDelegate();
  	    /// <summary>OK終了通知</summary>
		private PlotNotifyDelegate _plotNotify = null;
		/// <summary>>設定結果取得</summary>
		public PlotNotifyDelegate plotNotify
		{
			set { _plotNotify = value; }
			get { return _plotNotify; }
		}
	}
}
