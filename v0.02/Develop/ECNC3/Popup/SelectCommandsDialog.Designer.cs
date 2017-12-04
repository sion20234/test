namespace ECNC3.Views.Popup
{
    partial class SelectCommandsDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewEx1 = new ECNC3.Views.DataGridViewEx();
            this.Column1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewButtonColumn();
            this._titleLabel = new ECNC3.Views.LabelEx();
            this.CancelBt = new ECNC3.Views.ButtonEx();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEx1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewEx1
            // 
            this.dataGridViewEx1.AllowUserToAddRows = false;
            this.dataGridViewEx1.AllowUserToDeleteRows = false;
            this.dataGridViewEx1.AllowUserToResizeColumns = false;
            this.dataGridViewEx1.AllowUserToResizeRows = false;
            this.dataGridViewEx1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
            this.dataGridViewEx1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewEx1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEx1.ColumnHeadersVisible = false;
            this.dataGridViewEx1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2}); 
            this.dataGridViewEx1.GridColor = System.Drawing.Color.Gainsboro;
            this.dataGridViewEx1.Location = new System.Drawing.Point(23, 63);
            this.dataGridViewEx1.Name = "dataGridViewEx1";
            this.dataGridViewEx1.RowHeadersVisible = false;
            this.dataGridViewEx1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewEx1.RowTemplate.Height = 50;
            this.dataGridViewEx1.Size = new System.Drawing.Size(239, 226);
            this.dataGridViewEx1.TabIndex = 1;
            this.dataGridViewEx1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewEx1_CellClick);
            // 
            // Column1
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
            this.Column1.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.Width = 118;
            // 
            // Column2
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
            this.Column2.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            this.Column2.Width = 118;
            // 
            // _titleLabel
            // 
            this._titleLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
            this._titleLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._titleLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._titleLabel.ForeColor = System.Drawing.Color.Gainsboro;
            this._titleLabel.Location = new System.Drawing.Point(23, 26);
            this._titleLabel.Name = "_titleLabel";
            this._titleLabel.Size = new System.Drawing.Size(239, 34);
            this._titleLabel.TabIndex = 2;
            this._titleLabel.Text = "No Title";
            this._titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CancelBt
            // 
            this.CancelBt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
            this.CancelBt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
            this.CancelBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CancelBt.ForeColor = System.Drawing.Color.Gainsboro;
            this.CancelBt.Location = new System.Drawing.Point(82, 306);
            this.CancelBt.Name = "CancelBt";
            this.CancelBt.Selectable = true;
            this.CancelBt.Size = new System.Drawing.Size(120, 40);
            this.CancelBt.TabIndex = 0;
            this.CancelBt.Text = "キャンセル";
            this.CancelBt.UseVisualStyleBackColor = false;
            this.CancelBt.Click += new System.EventHandler(this.CancelBt_Click);
            // 
            // SelectCommandsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(284, 364);
            this.Controls.Add(this.dataGridViewEx1);
            this.Controls.Add(this._titleLabel);
            this.Controls.Add(this.CancelBt);
            this.ForeColor = System.Drawing.Color.Gainsboro;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SelectCommandsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SelectCommandsDialog";
            this.Load += new System.EventHandler(this.SelectCommandsDialog_Load);
            this.Shown += new System.EventHandler(this.SelectCommandsDialog_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEx1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ButtonEx CancelBt;
        private DataGridViewEx dataGridViewEx1;
        private System.Windows.Forms.DataGridViewButtonColumn Column1;
        private System.Windows.Forms.DataGridViewButtonColumn Column2;
        private LabelEx _titleLabel;
    }
}