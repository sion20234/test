namespace ECNC3.Views.Popup
{
    partial class ListViewDialog
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
            this._parameterList = new ECNC3.Views.DataGridViewEx();
            this._titleLabel = new ECNC3.Views.LabelEx();
            ((System.ComponentModel.ISupportInitialize)(this._parameterList)).BeginInit();
            this.SuspendLayout();
            // 
            // _parameterList
            // 
            this._parameterList.AllowUserToAddRows = false;
            this._parameterList.AllowUserToDeleteRows = false;
            this._parameterList.AllowUserToResizeColumns = false;
            this._parameterList.AllowUserToResizeRows = false;
            this._parameterList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
            this._parameterList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._parameterList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._parameterList.ColumnHeadersVisible = false;
            this._parameterList.EnableLastIndex = true;
            this._parameterList.GridColor = System.Drawing.Color.Gainsboro;
            this._parameterList.Location = new System.Drawing.Point(10, 50);
            this._parameterList.Name = "_parameterList";
            this._parameterList.RowHeadersVisible = false;
            this._parameterList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this._parameterList.RowTemplate.Height = 50;
            this._parameterList.Size = new System.Drawing.Size(262, 492);
            this._parameterList.TabIndex = 1;
            this._parameterList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewEx1_CellClick);
            this._parameterList.SelectionChanged += new System.EventHandler(this._parameterList_SelectionChanged);
            // 
            // _titleLabel
            // 
            this._titleLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
            this._titleLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._titleLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._titleLabel.ForeColor = System.Drawing.Color.Gainsboro;
            this._titleLabel.Location = new System.Drawing.Point(10, 13);
            this._titleLabel.Name = "_titleLabel";
            this._titleLabel.Size = new System.Drawing.Size(262, 34);
            this._titleLabel.TabIndex = 2;
            this._titleLabel.Text = "No Title";
            this._titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ListViewDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(284, 555);
            this.Controls.Add(this._parameterList);
            this.Controls.Add(this._titleLabel);
            this.ForeColor = System.Drawing.Color.Gainsboro;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ListViewDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ListViewDialog";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ListViewDialog_FormClosing);
            this.Load += new System.EventHandler(this.ListViewDialog_Load);
            this.Shown += new System.EventHandler(this.ListViewDialog_Shown);
            ((System.ComponentModel.ISupportInitialize)(this._parameterList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DataGridViewEx _parameterList;
        private LabelEx _titleLabel;
    }
}