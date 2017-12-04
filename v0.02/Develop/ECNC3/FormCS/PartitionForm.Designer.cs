namespace ECNC3.Views
{
    partial class PartitionForm
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
            this.label8 = new ECNC3.Views.LabelEx();
            this.panel3 = new ECNC3.Views.PanelEx();
            this.BackAECFormBt = new ECNC3.Views.ButtonEx();
            this._CrearBt = new ECNC3.Views.ButtonEx();
            this._SettingBt = new ECNC3.Views.ButtonEx();
            this._PartitionEnableBt = new ECNC3.Views.ButtonEx();
            this._PartitionList = new ECNC3.Views.DataGridViewEx();
            this._GuideThroughSequenceBtn = new ECNC3.Views.ButtonEx();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._PartitionList)).BeginInit();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label8.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.Location = new System.Drawing.Point(7, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1010, 25);
            this.label8.TabIndex = 110;
            this.label8.Text = "PARTITION";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.BackAECFormBt);
            this.panel3.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.panel3.Location = new System.Drawing.Point(865, 465);
            this.panel3.Name = "panel3";
            this.panel3.OutLineColor = System.Drawing.Color.DarkOliveGreen;
            this.panel3.OutLineSize = 2;
            this.panel3.Size = new System.Drawing.Size(152, 57);
            this.panel3.TabIndex = 214;
            // 
            // BackAECFormBt
            // 
            this.BackAECFormBt.EditBox = null;
            this.BackAECFormBt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.BackAECFormBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BackAECFormBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.BackAECFormBt.IsActive = false;
            this.BackAECFormBt.Location = new System.Drawing.Point(6, 6);
            this.BackAECFormBt.MultiSelectEn = false;
            this.BackAECFormBt.Name = "BackAECFormBt";
            this.BackAECFormBt.OutLineColor = System.Drawing.Color.Empty;
            this.BackAECFormBt.OutLineEn = true;
            this.BackAECFormBt.OutLineSize = 3;
            this.BackAECFormBt.Selectable = true;
            this.BackAECFormBt.Selected = false;
            this.BackAECFormBt.Size = new System.Drawing.Size(140, 45);
            this.BackAECFormBt.StatusLedEnable = false;
            this.BackAECFormBt.StatusLedSize = ((byte)(15));
            this.BackAECFormBt.TabIndex = 15;
            this.BackAECFormBt.Text = "閉じる";
            this.BackAECFormBt.UseVisualStyleBackColor = false;
            this.BackAECFormBt.Click += new System.EventHandler(this.BackAECFormBt_Click);
            // 
            // _CrearBt
            // 
            this._CrearBt.EditBox = null;
            this._CrearBt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this._CrearBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._CrearBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold);
            this._CrearBt.IsActive = false;
            this._CrearBt.Location = new System.Drawing.Point(7, 475);
            this._CrearBt.MultiSelectEn = false;
            this._CrearBt.Name = "_CrearBt";
            this._CrearBt.OutLineColor = System.Drawing.Color.Empty;
            this._CrearBt.OutLineEn = true;
            this._CrearBt.OutLineSize = 3;
            this._CrearBt.Selectable = true;
            this._CrearBt.Selected = false;
            this._CrearBt.Size = new System.Drawing.Size(140, 45);
            this._CrearBt.StatusLedEnable = true;
            this._CrearBt.StatusLedSize = ((byte)(15));
            this._CrearBt.TabIndex = 217;
            this._CrearBt.Text = "クリア";
            this._CrearBt.UseVisualStyleBackColor = false;
            this._CrearBt.MouseUp += new System.Windows.Forms.MouseEventHandler(this._CrearBt_MouseUp);
            // 
            // _SettingBt
            // 
            this._SettingBt.EditBox = null;
            this._SettingBt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this._SettingBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._SettingBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold);
            this._SettingBt.IsActive = false;
            this._SettingBt.Location = new System.Drawing.Point(153, 475);
            this._SettingBt.MultiSelectEn = false;
            this._SettingBt.Name = "_SettingBt";
            this._SettingBt.OutLineColor = System.Drawing.Color.Empty;
            this._SettingBt.OutLineEn = true;
            this._SettingBt.OutLineSize = 3;
            this._SettingBt.Selectable = true;
            this._SettingBt.Selected = false;
            this._SettingBt.Size = new System.Drawing.Size(140, 45);
            this._SettingBt.StatusLedEnable = false;
            this._SettingBt.StatusLedSize = ((byte)(15));
            this._SettingBt.TabIndex = 218;
            this._SettingBt.Text = "設定";
            this._SettingBt.UseVisualStyleBackColor = false;
            this._SettingBt.MouseUp += new System.Windows.Forms.MouseEventHandler(this._SettingBt_MouseUp);
            // 
            // _PartitionEnableBt
            // 
            this._PartitionEnableBt.EditBox = null;
            this._PartitionEnableBt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this._PartitionEnableBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._PartitionEnableBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold);
            this._PartitionEnableBt.IsActive = false;
            this._PartitionEnableBt.Location = new System.Drawing.Point(299, 475);
            this._PartitionEnableBt.MultiSelectEn = false;
            this._PartitionEnableBt.Name = "_PartitionEnableBt";
            this._PartitionEnableBt.OutLineColor = System.Drawing.Color.Empty;
            this._PartitionEnableBt.OutLineEn = true;
            this._PartitionEnableBt.OutLineSize = 3;
            this._PartitionEnableBt.Selectable = true;
            this._PartitionEnableBt.Selected = false;
            this._PartitionEnableBt.Size = new System.Drawing.Size(140, 45);
            this._PartitionEnableBt.StatusLedEnable = true;
            this._PartitionEnableBt.StatusLedSize = ((byte)(15));
            this._PartitionEnableBt.TabIndex = 219;
            this._PartitionEnableBt.Text = "無効";
            this._PartitionEnableBt.UseVisualStyleBackColor = false;
            this._PartitionEnableBt.MouseUp += new System.Windows.Forms.MouseEventHandler(this._PartitionEnableBt_MouseUp);
            // 
            // _PartitionList
            // 
            this._PartitionList.AllowUserToAddRows = false;
            this._PartitionList.AllowUserToDeleteRows = false;
            this._PartitionList.AllowUserToResizeColumns = false;
            this._PartitionList.AllowUserToResizeRows = false;
            this._PartitionList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this._PartitionList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this._PartitionList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this._PartitionList.EnableLastIndex = false;
            this._PartitionList.Location = new System.Drawing.Point(7, 37);
            this._PartitionList.MultiSelect = false;
            this._PartitionList.Name = "_PartitionList";
            this._PartitionList.ReadOnly = true;
            this._PartitionList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this._PartitionList.RowTemplate.Height = 21;
            this._PartitionList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this._PartitionList.Size = new System.Drawing.Size(1010, 415);
            this._PartitionList.TabIndex = 220;
            this._PartitionList.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this._PartitionList_CellMouseUp);
            this._PartitionList.SelectionChanged += new System.EventHandler(this._PartitionList_SelectionChanged);
            // 
            // _GuideThroughSequenceBtn
            // 
            this._GuideThroughSequenceBtn.EditBox = null;
            this._GuideThroughSequenceBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this._GuideThroughSequenceBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._GuideThroughSequenceBtn.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._GuideThroughSequenceBtn.IsActive = false;
            this._GuideThroughSequenceBtn.Location = new System.Drawing.Point(470, 475);
            this._GuideThroughSequenceBtn.MultiSelectEn = false;
            this._GuideThroughSequenceBtn.Name = "_GuideThroughSequenceBtn";
            this._GuideThroughSequenceBtn.OutLineColor = System.Drawing.Color.Empty;
            this._GuideThroughSequenceBtn.OutLineEn = true;
            this._GuideThroughSequenceBtn.OutLineSize = 3;
            this._GuideThroughSequenceBtn.Selectable = true;
            this._GuideThroughSequenceBtn.Selected = false;
            this._GuideThroughSequenceBtn.Size = new System.Drawing.Size(140, 45);
            this._GuideThroughSequenceBtn.StatusLedEnable = true;
            this._GuideThroughSequenceBtn.StatusLedSize = ((byte)(15));
            this._GuideThroughSequenceBtn.TabIndex = 221;
            this._GuideThroughSequenceBtn.Text = "ガイド貫通動作";
            this._GuideThroughSequenceBtn.UseVisualStyleBackColor = false;
            this._GuideThroughSequenceBtn.MouseUp += new System.Windows.Forms.MouseEventHandler(this._GuideThroughSequenceBtn_MouseUp);
            // 
            // PartitionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 531);
            this.Controls.Add(this._GuideThroughSequenceBtn);
            this.Controls.Add(this._PartitionList);
            this.Controls.Add(this._PartitionEnableBt);
            this.Controls.Add(this._SettingBt);
            this.Controls.Add(this._CrearBt);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label8);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(0, 90);
            this.Name = "PartitionForm";
            this.OutLineSize = 3;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "PartitionForm";
            this.Load += new System.EventHandler(this.PartitionForm_Load);
            this.Shown += new System.EventHandler(this.PartitionForm_Shown);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._PartitionList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private LabelEx label8;
        private PanelEx panel3;
        private ButtonEx BackAECFormBt;
        private ButtonEx _CrearBt;
        private ButtonEx _SettingBt;
        private ButtonEx _PartitionEnableBt;
        private DataGridViewEx _PartitionList;
        private ButtonEx _GuideThroughSequenceBtn;
    }
}