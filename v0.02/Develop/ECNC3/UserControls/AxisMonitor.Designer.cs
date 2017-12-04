namespace ECNC3.Views
{
    partial class AxisMonitor
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this._axisMonitorView = new ECNC3.Views.DataGridViewEx();
            this.AxisColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WorkPosColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MachinePosColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this._axisMonitorView)).BeginInit();
            this.SuspendLayout();
            // 
            // _axisMonitorView
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._axisMonitorView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this._axisMonitorView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._axisMonitorView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AxisColumn,
            this.WorkPosColumn,
            this.MachinePosColumn,
            this.UnitColumn});
            this._axisMonitorView.EnableHeadersVisualStyles = false;
            this._axisMonitorView.EnableLastIndex = true;
            this._axisMonitorView.Location = new System.Drawing.Point(3, 3);
            this._axisMonitorView.Name = "_axisMonitorView";
            this._axisMonitorView.RowHeadersVisible = false;
            this._axisMonitorView.RowTemplate.Height = 21;
            this._axisMonitorView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this._axisMonitorView.Size = new System.Drawing.Size(415, 366);
            this._axisMonitorView.TabIndex = 0;
            this._axisMonitorView.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this._axisMonitorView_CellMouseUp);
            this._axisMonitorView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this._axisMonitorView_ColumnHeaderMouseClick);
            this._axisMonitorView.SelectionChanged += new System.EventHandler(this._axisMonitorView_SelectionChanged);
            this._axisMonitorView.Paint += new System.Windows.Forms.PaintEventHandler(this._axisMonitorView_Paint);
            // 
            // AxisColumn
            // 
            this.AxisColumn.HeaderText = "";
            this.AxisColumn.Name = "AxisColumn";
            this.AxisColumn.Width = 50;
            // 
            // WorkPosColumn
            // 
            this.WorkPosColumn.HeaderText = "ワーク座標";
            this.WorkPosColumn.Name = "WorkPosColumn";
            this.WorkPosColumn.Width = 150;
            // 
            // MachinePosColumn
            // 
            this.MachinePosColumn.HeaderText = "機械座標";
            this.MachinePosColumn.Name = "MachinePosColumn";
            this.MachinePosColumn.Width = 150;
            // 
            // UnitColumn
            // 
            this.UnitColumn.HeaderText = "";
            this.UnitColumn.Name = "UnitColumn";
            this.UnitColumn.Width = 50;
            // 
            // AxisMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._axisMonitorView);
            this.Name = "AxisMonitor";
            this.Size = new System.Drawing.Size(421, 372);
            this.Load += new System.EventHandler(this.AxisMonitor_Load);
            ((System.ComponentModel.ISupportInitialize)(this._axisMonitorView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DataGridViewEx _axisMonitorView;
        private System.Windows.Forms.DataGridViewTextBoxColumn AxisColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn WorkPosColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn MachinePosColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitColumn;
    }
}
