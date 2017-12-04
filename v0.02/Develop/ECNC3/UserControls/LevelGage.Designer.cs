namespace ECNC3.Views.UserControls
{
    partial class LevelGage
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.CategoryNameV = new ECNC3.Views.LabelEx();
            this.CategoryNameA = new ECNC3.Views.LabelEx();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.Empty;
            this.chart1.BorderlineColor = System.Drawing.Color.Empty;
            this.chart1.BorderSkin.BorderColor = System.Drawing.Color.Empty;
            this.chart1.BorderSkin.PageColor = System.Drawing.Color.Empty;
            chartArea1.Area3DStyle.Enable3D = true;
            chartArea1.Area3DStyle.Inclination = 5;
            chartArea1.Area3DStyle.LightStyle = System.Windows.Forms.DataVisualization.Charting.LightStyle.Realistic;
            chartArea1.Area3DStyle.PointDepth = 47;
            chartArea1.Area3DStyle.PointGapDepth = 80;
            chartArea1.Area3DStyle.Rotation = 90;
            chartArea1.Area3DStyle.WallWidth = 0;
            chartArea1.AxisX.LineColor = System.Drawing.Color.Empty;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.Empty;
            chartArea1.AxisX.MajorTickMark.LineColor = System.Drawing.Color.Empty;
            chartArea1.AxisX.MinorGrid.LineColor = System.Drawing.Color.Empty;
            chartArea1.AxisX.TitleForeColor = System.Drawing.Color.Empty;
            chartArea1.AxisX2.LineColor = System.Drawing.Color.Empty;
            chartArea1.AxisX2.MajorGrid.LineColor = System.Drawing.Color.Empty;
            chartArea1.AxisX2.MajorTickMark.LineColor = System.Drawing.Color.Empty;
            chartArea1.AxisX2.MinorGrid.LineColor = System.Drawing.Color.Empty;
            chartArea1.AxisX2.MinorTickMark.LineColor = System.Drawing.Color.Empty;
            chartArea1.AxisY.LineColor = System.Drawing.Color.Empty;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.Empty;
            chartArea1.AxisY.MajorTickMark.LineColor = System.Drawing.Color.Empty;
            chartArea1.AxisY.MinorGrid.LineColor = System.Drawing.Color.Empty;
            chartArea1.AxisY2.LineColor = System.Drawing.Color.Empty;
            chartArea1.AxisY2.MajorGrid.LineColor = System.Drawing.Color.Empty;
            chartArea1.AxisY2.MajorTickMark.LineColor = System.Drawing.Color.Empty;
            chartArea1.AxisY2.MinorGrid.LineColor = System.Drawing.Color.Empty;
            chartArea1.BorderColor = System.Drawing.Color.Empty;
            chartArea1.Name = "ChartArea1";
            chartArea1.ShadowColor = System.Drawing.Color.Empty;
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(6, 6);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(138, 165);
            this.chart1.TabIndex = 6;
            this.chart1.Text = "chart1";
            // 
            // CategoryNameV
            // 
            this.CategoryNameV.AutoSize = true;
            this.CategoryNameV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CategoryNameV.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CategoryNameV.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.CategoryNameV.Location = new System.Drawing.Point(52, 149);
            this.CategoryNameV.Name = "CategoryNameV";
            this.CategoryNameV.Size = new System.Drawing.Size(33, 17);
            this.CategoryNameV.TabIndex = 7;
            this.CategoryNameV.Text = "電流";
            // 
            // CategoryNameA
            // 
            this.CategoryNameA.AutoSize = true;
            this.CategoryNameA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CategoryNameA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CategoryNameA.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.CategoryNameA.Location = new System.Drawing.Point(89, 149);
            this.CategoryNameA.Name = "CategoryNameA";
            this.CategoryNameA.Size = new System.Drawing.Size(33, 17);
            this.CategoryNameA.TabIndex = 3;
            this.CategoryNameA.Text = "電流";
            // 
            // LevelGage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CategoryNameV);
            this.Controls.Add(this.CategoryNameA);
            this.Controls.Add(this.chart1);
            this.Name = "LevelGage";
            this.Size = new System.Drawing.Size(152, 179);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private LabelEx CategoryNameA;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private LabelEx CategoryNameV;
    }
}
