namespace ECNC3.Views
{
    partial class AlarmLogForm
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
            this.buttonEx_LogOpen = new ECNC3.Views.ButtonEx();
            this.buttonEx_FileExpor = new ECNC3.Views.ButtonEx();
            this.buttonEx_Last = new ECNC3.Views.ButtonEx();
            this.buttonEx_1Dn = new ECNC3.Views.ButtonEx();
            this.buttonEx_1Up = new ECNC3.Views.ButtonEx();
            this.buttonEx_First = new ECNC3.Views.ButtonEx();
            this.panel2 = new ECNC3.Views.PanelEx();
            this.BackUserFuncFormBt = new ECNC3.Views.ButtonEx();
            this.buttonEx_100Up = new ECNC3.Views.ButtonEx();
            this.buttonEx_10Up = new ECNC3.Views.ButtonEx();
            this.buttonEx_100Dn = new ECNC3.Views.ButtonEx();
            this.buttonEx_10Dn = new ECNC3.Views.ButtonEx();
            this.panelEx2 = new ECNC3.Views.PanelEx();
            this.buttonEx_Find = new ECNC3.Views.ButtonEx();
            this.dataGridViewEx1 = new ECNC3.Views.DataGridViewEx();
            this.labelEx_Path = new ECNC3.Views.LabelEx();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEx1)).BeginInit();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label8.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.Location = new System.Drawing.Point(-5, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1024, 30);
            this.label8.TabIndex = 84;
            this.label8.Text = "ALARM LOG";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // buttonEx_LogOpen
            // 
            this.buttonEx_LogOpen.EditBox = null;
            this.buttonEx_LogOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEx_LogOpen.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonEx_LogOpen.IsActive = false;
            this.buttonEx_LogOpen.Location = new System.Drawing.Point(12, 665);
            this.buttonEx_LogOpen.MultiSelectEn = false;
            this.buttonEx_LogOpen.Name = "buttonEx_LogOpen";
            this.buttonEx_LogOpen.OutLineColor = System.Drawing.Color.Empty;
            this.buttonEx_LogOpen.OutLineEn = true;
            this.buttonEx_LogOpen.OutLineSize = 3;
            this.buttonEx_LogOpen.Selectable = true;
            this.buttonEx_LogOpen.Selected = false;
            this.buttonEx_LogOpen.Size = new System.Drawing.Size(140, 70);
            this.buttonEx_LogOpen.StatusLedEnable = false;
            this.buttonEx_LogOpen.StatusLedSize = ((byte)(15));
            this.buttonEx_LogOpen.TabIndex = 85;
            this.buttonEx_LogOpen.Text = "ログを開く";
            this.buttonEx_LogOpen.UseVisualStyleBackColor = true;
            this.buttonEx_LogOpen.Click += new System.EventHandler(this.buttonEx_LogOpen_Click);
            // 
            // buttonEx_FileExpor
            // 
            this.buttonEx_FileExpor.EditBox = null;
            this.buttonEx_FileExpor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEx_FileExpor.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonEx_FileExpor.IsActive = false;
            this.buttonEx_FileExpor.Location = new System.Drawing.Point(175, 665);
            this.buttonEx_FileExpor.MultiSelectEn = false;
            this.buttonEx_FileExpor.Name = "buttonEx_FileExpor";
            this.buttonEx_FileExpor.OutLineColor = System.Drawing.Color.Empty;
            this.buttonEx_FileExpor.OutLineEn = true;
            this.buttonEx_FileExpor.OutLineSize = 3;
            this.buttonEx_FileExpor.Selectable = true;
            this.buttonEx_FileExpor.Selected = false;
            this.buttonEx_FileExpor.Size = new System.Drawing.Size(140, 70);
            this.buttonEx_FileExpor.StatusLedEnable = false;
            this.buttonEx_FileExpor.StatusLedSize = ((byte)(15));
            this.buttonEx_FileExpor.TabIndex = 88;
            this.buttonEx_FileExpor.Text = "エクスポート";
            this.buttonEx_FileExpor.UseVisualStyleBackColor = true;
            this.buttonEx_FileExpor.Click += new System.EventHandler(this.FileExportBt_Click);
            // 
            // buttonEx_Last
            // 
            this.buttonEx_Last.EditBox = null;
            this.buttonEx_Last.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEx_Last.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonEx_Last.IsActive = false;
            this.buttonEx_Last.Location = new System.Drawing.Point(923, 587);
            this.buttonEx_Last.MultiSelectEn = false;
            this.buttonEx_Last.Name = "buttonEx_Last";
            this.buttonEx_Last.OutLineColor = System.Drawing.Color.Empty;
            this.buttonEx_Last.OutLineEn = true;
            this.buttonEx_Last.OutLineSize = 3;
            this.buttonEx_Last.Selectable = true;
            this.buttonEx_Last.Selected = false;
            this.buttonEx_Last.Size = new System.Drawing.Size(90, 60);
            this.buttonEx_Last.StatusLedEnable = false;
            this.buttonEx_Last.StatusLedSize = ((byte)(15));
            this.buttonEx_Last.TabIndex = 89;
            this.buttonEx_Last.Text = "最後";
            this.buttonEx_Last.UseVisualStyleBackColor = true;
            this.buttonEx_Last.Click += new System.EventHandler(this.buttonEx_Last_Click);
            // 
            // buttonEx_1Dn
            // 
            this.buttonEx_1Dn.EditBox = null;
            this.buttonEx_1Dn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEx_1Dn.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonEx_1Dn.IsActive = false;
            this.buttonEx_1Dn.Location = new System.Drawing.Point(923, 362);
            this.buttonEx_1Dn.MultiSelectEn = false;
            this.buttonEx_1Dn.Name = "buttonEx_1Dn";
            this.buttonEx_1Dn.OutLineColor = System.Drawing.Color.Empty;
            this.buttonEx_1Dn.OutLineEn = true;
            this.buttonEx_1Dn.OutLineSize = 3;
            this.buttonEx_1Dn.Selectable = true;
            this.buttonEx_1Dn.Selected = false;
            this.buttonEx_1Dn.Size = new System.Drawing.Size(90, 60);
            this.buttonEx_1Dn.StatusLedEnable = false;
            this.buttonEx_1Dn.StatusLedSize = ((byte)(15));
            this.buttonEx_1Dn.TabIndex = 90;
            this.buttonEx_1Dn.Text = "1▼";
            this.buttonEx_1Dn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonEx_1Dn.UseVisualStyleBackColor = true;
            this.buttonEx_1Dn.Click += new System.EventHandler(this.buttonEx_1Dn_Click);
            // 
            // buttonEx_1Up
            // 
            this.buttonEx_1Up.EditBox = null;
            this.buttonEx_1Up.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEx_1Up.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonEx_1Up.IsActive = false;
            this.buttonEx_1Up.Location = new System.Drawing.Point(923, 286);
            this.buttonEx_1Up.MultiSelectEn = false;
            this.buttonEx_1Up.Name = "buttonEx_1Up";
            this.buttonEx_1Up.OutLineColor = System.Drawing.Color.Empty;
            this.buttonEx_1Up.OutLineEn = true;
            this.buttonEx_1Up.OutLineSize = 3;
            this.buttonEx_1Up.Selectable = true;
            this.buttonEx_1Up.Selected = false;
            this.buttonEx_1Up.Size = new System.Drawing.Size(90, 60);
            this.buttonEx_1Up.StatusLedEnable = false;
            this.buttonEx_1Up.StatusLedSize = ((byte)(15));
            this.buttonEx_1Up.TabIndex = 91;
            this.buttonEx_1Up.Text = "1▲";
            this.buttonEx_1Up.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonEx_1Up.UseVisualStyleBackColor = true;
            this.buttonEx_1Up.Click += new System.EventHandler(this.buttonEx_1Up_Click);
            // 
            // buttonEx_First
            // 
            this.buttonEx_First.EditBox = null;
            this.buttonEx_First.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEx_First.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonEx_First.IsActive = false;
            this.buttonEx_First.Location = new System.Drawing.Point(923, 60);
            this.buttonEx_First.MultiSelectEn = false;
            this.buttonEx_First.Name = "buttonEx_First";
            this.buttonEx_First.OutLineColor = System.Drawing.Color.Empty;
            this.buttonEx_First.OutLineEn = true;
            this.buttonEx_First.OutLineSize = 3;
            this.buttonEx_First.Selectable = true;
            this.buttonEx_First.Selected = false;
            this.buttonEx_First.Size = new System.Drawing.Size(90, 60);
            this.buttonEx_First.StatusLedEnable = false;
            this.buttonEx_First.StatusLedSize = ((byte)(15));
            this.buttonEx_First.TabIndex = 92;
            this.buttonEx_First.Text = "最初";
            this.buttonEx_First.UseVisualStyleBackColor = true;
            this.buttonEx_First.Click += new System.EventHandler(this.buttonEx_First_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.BackUserFuncFormBt);
            this.panel2.Location = new System.Drawing.Point(852, 662);
            this.panel2.Name = "panel2";
            this.panel2.OutLineColor = System.Drawing.Color.DarkOliveGreen;
            this.panel2.OutLineSize = 2;
            this.panel2.Size = new System.Drawing.Size(160, 90);
            this.panel2.TabIndex = 95;
            // 
            // BackUserFuncFormBt
            // 
            this.BackUserFuncFormBt.EditBox = null;
            this.BackUserFuncFormBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BackUserFuncFormBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BackUserFuncFormBt.IsActive = false;
            this.BackUserFuncFormBt.Location = new System.Drawing.Point(8, 8);
            this.BackUserFuncFormBt.MultiSelectEn = false;
            this.BackUserFuncFormBt.Name = "BackUserFuncFormBt";
            this.BackUserFuncFormBt.OutLineColor = System.Drawing.Color.Empty;
            this.BackUserFuncFormBt.OutLineEn = true;
            this.BackUserFuncFormBt.OutLineSize = 3;
            this.BackUserFuncFormBt.Selectable = true;
            this.BackUserFuncFormBt.Selected = false;
            this.BackUserFuncFormBt.Size = new System.Drawing.Size(140, 70);
            this.BackUserFuncFormBt.StatusLedEnable = false;
            this.BackUserFuncFormBt.StatusLedSize = ((byte)(15));
            this.BackUserFuncFormBt.TabIndex = 15;
            this.BackUserFuncFormBt.Text = "閉じる";
            this.BackUserFuncFormBt.UseVisualStyleBackColor = true;
            this.BackUserFuncFormBt.Click += new System.EventHandler(this.UserFuncFormBt_Click);
            // 
            // buttonEx_100Up
            // 
            this.buttonEx_100Up.EditBox = null;
            this.buttonEx_100Up.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEx_100Up.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonEx_100Up.IsActive = false;
            this.buttonEx_100Up.Location = new System.Drawing.Point(923, 154);
            this.buttonEx_100Up.MultiSelectEn = false;
            this.buttonEx_100Up.Name = "buttonEx_100Up";
            this.buttonEx_100Up.OutLineColor = System.Drawing.Color.Empty;
            this.buttonEx_100Up.OutLineEn = true;
            this.buttonEx_100Up.OutLineSize = 3;
            this.buttonEx_100Up.Selectable = true;
            this.buttonEx_100Up.Selected = false;
            this.buttonEx_100Up.Size = new System.Drawing.Size(90, 60);
            this.buttonEx_100Up.StatusLedEnable = false;
            this.buttonEx_100Up.StatusLedSize = ((byte)(15));
            this.buttonEx_100Up.TabIndex = 96;
            this.buttonEx_100Up.Text = "100▲";
            this.buttonEx_100Up.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonEx_100Up.UseVisualStyleBackColor = true;
            this.buttonEx_100Up.Click += new System.EventHandler(this.buttonEx_100Up_Click);
            // 
            // buttonEx_10Up
            // 
            this.buttonEx_10Up.EditBox = null;
            this.buttonEx_10Up.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEx_10Up.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonEx_10Up.IsActive = false;
            this.buttonEx_10Up.Location = new System.Drawing.Point(923, 220);
            this.buttonEx_10Up.MultiSelectEn = false;
            this.buttonEx_10Up.Name = "buttonEx_10Up";
            this.buttonEx_10Up.OutLineColor = System.Drawing.Color.Empty;
            this.buttonEx_10Up.OutLineEn = true;
            this.buttonEx_10Up.OutLineSize = 3;
            this.buttonEx_10Up.Selectable = true;
            this.buttonEx_10Up.Selected = false;
            this.buttonEx_10Up.Size = new System.Drawing.Size(90, 60);
            this.buttonEx_10Up.StatusLedEnable = false;
            this.buttonEx_10Up.StatusLedSize = ((byte)(15));
            this.buttonEx_10Up.TabIndex = 97;
            this.buttonEx_10Up.Text = "10▲";
            this.buttonEx_10Up.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonEx_10Up.UseVisualStyleBackColor = true;
            this.buttonEx_10Up.Click += new System.EventHandler(this.buttonEx_10Up_Click);
            // 
            // buttonEx_100Dn
            // 
            this.buttonEx_100Dn.EditBox = null;
            this.buttonEx_100Dn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEx_100Dn.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonEx_100Dn.IsActive = false;
            this.buttonEx_100Dn.Location = new System.Drawing.Point(923, 494);
            this.buttonEx_100Dn.MultiSelectEn = false;
            this.buttonEx_100Dn.Name = "buttonEx_100Dn";
            this.buttonEx_100Dn.OutLineColor = System.Drawing.Color.Empty;
            this.buttonEx_100Dn.OutLineEn = true;
            this.buttonEx_100Dn.OutLineSize = 3;
            this.buttonEx_100Dn.Selectable = true;
            this.buttonEx_100Dn.Selected = false;
            this.buttonEx_100Dn.Size = new System.Drawing.Size(90, 60);
            this.buttonEx_100Dn.StatusLedEnable = false;
            this.buttonEx_100Dn.StatusLedSize = ((byte)(15));
            this.buttonEx_100Dn.TabIndex = 98;
            this.buttonEx_100Dn.Text = "100▼";
            this.buttonEx_100Dn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonEx_100Dn.UseVisualStyleBackColor = true;
            this.buttonEx_100Dn.Click += new System.EventHandler(this.buttonEx_100Dn_Click);
            // 
            // buttonEx_10Dn
            // 
            this.buttonEx_10Dn.EditBox = null;
            this.buttonEx_10Dn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEx_10Dn.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonEx_10Dn.IsActive = false;
            this.buttonEx_10Dn.Location = new System.Drawing.Point(923, 428);
            this.buttonEx_10Dn.MultiSelectEn = false;
            this.buttonEx_10Dn.Name = "buttonEx_10Dn";
            this.buttonEx_10Dn.OutLineColor = System.Drawing.Color.Empty;
            this.buttonEx_10Dn.OutLineEn = true;
            this.buttonEx_10Dn.OutLineSize = 3;
            this.buttonEx_10Dn.Selectable = true;
            this.buttonEx_10Dn.Selected = false;
            this.buttonEx_10Dn.Size = new System.Drawing.Size(90, 60);
            this.buttonEx_10Dn.StatusLedEnable = false;
            this.buttonEx_10Dn.StatusLedSize = ((byte)(15));
            this.buttonEx_10Dn.TabIndex = 99;
            this.buttonEx_10Dn.Text = "10▼";
            this.buttonEx_10Dn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonEx_10Dn.UseVisualStyleBackColor = true;
            this.buttonEx_10Dn.Click += new System.EventHandler(this.buttonEx_10Dn_Click);
            // 
            // panelEx2
            // 
            this.panelEx2.Location = new System.Drawing.Point(920, 351);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.OutLineColor = System.Drawing.Color.Empty;
            this.panelEx2.OutLineSize = 0;
            this.panelEx2.Size = new System.Drawing.Size(93, 5);
            this.panelEx2.TabIndex = 226;
            // 
            // buttonEx_Find
            // 
            this.buttonEx_Find.EditBox = null;
            this.buttonEx_Find.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEx_Find.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonEx_Find.IsActive = false;
            this.buttonEx_Find.Location = new System.Drawing.Point(338, 665);
            this.buttonEx_Find.MultiSelectEn = false;
            this.buttonEx_Find.Name = "buttonEx_Find";
            this.buttonEx_Find.OutLineColor = System.Drawing.Color.Empty;
            this.buttonEx_Find.OutLineEn = true;
            this.buttonEx_Find.OutLineSize = 3;
            this.buttonEx_Find.Selectable = true;
            this.buttonEx_Find.Selected = false;
            this.buttonEx_Find.Size = new System.Drawing.Size(140, 70);
            this.buttonEx_Find.StatusLedEnable = false;
            this.buttonEx_Find.StatusLedSize = ((byte)(15));
            this.buttonEx_Find.TabIndex = 227;
            this.buttonEx_Find.Text = "検索";
            this.buttonEx_Find.UseVisualStyleBackColor = true;
            this.buttonEx_Find.Visible = false;
            this.buttonEx_Find.Click += new System.EventHandler(this.buttonEx_Find_Click);
            // 
            // dataGridViewEx1
            // 
            this.dataGridViewEx1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEx1.EnableLastIndex = true;
            this.dataGridViewEx1.Location = new System.Drawing.Point(12, 60);
            this.dataGridViewEx1.Name = "dataGridViewEx1";
            this.dataGridViewEx1.RowTemplate.Height = 21;
            this.dataGridViewEx1.Size = new System.Drawing.Size(899, 586);
            this.dataGridViewEx1.TabIndex = 228;
            // 
            // labelEx_Path
            // 
            this.labelEx_Path.AutoSize = true;
            this.labelEx_Path.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelEx_Path.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelEx_Path.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelEx_Path.Location = new System.Drawing.Point(12, 36);
            this.labelEx_Path.Name = "labelEx_Path";
            this.labelEx_Path.Size = new System.Drawing.Size(2, 26);
            this.labelEx_Path.TabIndex = 229;
            // 
            // AlarmLogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 757);
            this.Controls.Add(this.labelEx_Path);
            this.Controls.Add(this.dataGridViewEx1);
            this.Controls.Add(this.buttonEx_Find);
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.buttonEx_10Dn);
            this.Controls.Add(this.buttonEx_100Dn);
            this.Controls.Add(this.buttonEx_10Up);
            this.Controls.Add(this.buttonEx_100Up);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.buttonEx_First);
            this.Controls.Add(this.buttonEx_1Up);
            this.Controls.Add(this.buttonEx_1Dn);
            this.Controls.Add(this.buttonEx_Last);
            this.Controls.Add(this.buttonEx_FileExpor);
            this.Controls.Add(this.buttonEx_LogOpen);
            this.Controls.Add(this.label8);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AlarmLogForm";
            this.OutLineSize = 3;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "AlarmLogForm";
            this.Load += new System.EventHandler(this.AlarmLogForm_Load);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEx1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LabelEx label8;
        private ButtonEx buttonEx_LogOpen;
        private ButtonEx buttonEx_FileExpor;
        private ButtonEx buttonEx_Last;
        private ButtonEx buttonEx_1Dn;
        private ButtonEx buttonEx_1Up;
        private ButtonEx buttonEx_First;
        private PanelEx panel2;
        private ButtonEx BackUserFuncFormBt;
		private ButtonEx buttonEx_100Up;
		private ButtonEx buttonEx_10Up;
		private ButtonEx buttonEx_100Dn;
		private ButtonEx buttonEx_10Dn;
		private PanelEx panelEx2;
		private ButtonEx buttonEx_Find;
		private DataGridViewEx dataGridViewEx1;
		private LabelEx labelEx_Path;
	}
}