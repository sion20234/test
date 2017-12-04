namespace ECNC3.Views
{
	partial class MaintenanceForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if( disposing && ( components != null ) ) {
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label8 = new ECNC3.Views.LabelEx();
            this.panel6 = new ECNC3.Views.PanelEx();
            this.buttonEx_Reset = new ECNC3.Views.ButtonEx();
            this.panel2 = new ECNC3.Views.PanelEx();
            this.BackUserFuncFormBt = new ECNC3.Views.ButtonEx();
            this.dataGridViewEx1 = new ECNC3.Views.DataGridViewEx();
            this.panelEx1 = new ECNC3.Views.PanelEx();
            this.buttonEx_LineDel = new ECNC3.Views.ButtonEx();
            this.panelEx2 = new ECNC3.Views.PanelEx();
            this.buttonEx_LineAdd = new ECNC3.Views.ButtonEx();
            this.panel6.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEx1)).BeginInit();
            this.panelEx1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label8.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.Location = new System.Drawing.Point(6, 6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1014, 30);
            this.label8.TabIndex = 83;
            this.label8.Text = "MAINTENANCE";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel6.Controls.Add(this.buttonEx_Reset);
            this.panel6.Location = new System.Drawing.Point(498, 607);
            this.panel6.Name = "panel6";
            this.panel6.OutLineColor = System.Drawing.Color.Empty;
            this.panel6.OutLineSize = 0;
            this.panel6.Size = new System.Drawing.Size(169, 65);
            this.panel6.TabIndex = 100;
            // 
            // buttonEx_Reset
            // 
            this.buttonEx_Reset.EditBox = null;
            this.buttonEx_Reset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEx_Reset.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonEx_Reset.IsActive = false;
            this.buttonEx_Reset.Location = new System.Drawing.Point(7, 6);
            this.buttonEx_Reset.MultiSelectEn = false;
            this.buttonEx_Reset.Name = "buttonEx_Reset";
            this.buttonEx_Reset.OutLineColor = System.Drawing.Color.Empty;
            this.buttonEx_Reset.OutLineEn = true;
            this.buttonEx_Reset.OutLineSize = 3;
            this.buttonEx_Reset.Selectable = true;
            this.buttonEx_Reset.Selected = false;
            this.buttonEx_Reset.Size = new System.Drawing.Size(150, 50);
            this.buttonEx_Reset.StatusLedEnable = false;
            this.buttonEx_Reset.StatusLedSize = ((byte)(15));
            this.buttonEx_Reset.TabIndex = 1;
            this.buttonEx_Reset.Text = "リセット";
            this.buttonEx_Reset.UseVisualStyleBackColor = true;
            this.buttonEx_Reset.Click += new System.EventHandler(this.buttonEx_Reset_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.BackUserFuncFormBt);
            this.panel2.Location = new System.Drawing.Point(846, 607);
            this.panel2.Name = "panel2";
            this.panel2.OutLineColor = System.Drawing.Color.DarkOliveGreen;
            this.panel2.OutLineSize = 2;
            this.panel2.Size = new System.Drawing.Size(169, 65);
            this.panel2.TabIndex = 99;
            // 
            // BackUserFuncFormBt
            // 
            this.BackUserFuncFormBt.EditBox = null;
            this.BackUserFuncFormBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BackUserFuncFormBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BackUserFuncFormBt.IsActive = false;
            this.BackUserFuncFormBt.Location = new System.Drawing.Point(7, 6);
            this.BackUserFuncFormBt.MultiSelectEn = false;
            this.BackUserFuncFormBt.Name = "BackUserFuncFormBt";
            this.BackUserFuncFormBt.OutLineColor = System.Drawing.Color.Empty;
            this.BackUserFuncFormBt.OutLineEn = true;
            this.BackUserFuncFormBt.OutLineSize = 3;
            this.BackUserFuncFormBt.Selectable = true;
            this.BackUserFuncFormBt.Selected = false;
            this.BackUserFuncFormBt.Size = new System.Drawing.Size(150, 50);
            this.BackUserFuncFormBt.StatusLedEnable = false;
            this.BackUserFuncFormBt.StatusLedSize = ((byte)(15));
            this.BackUserFuncFormBt.TabIndex = 15;
            this.BackUserFuncFormBt.Text = "閉じる";
            this.BackUserFuncFormBt.UseVisualStyleBackColor = true;
            this.BackUserFuncFormBt.Click += new System.EventHandler(this.BackUserFuncFormBt_Click);
            // 
            // dataGridViewEx1
            // 
            this.dataGridViewEx1.AllowUserToAddRows = false;
            this.dataGridViewEx1.AllowUserToDeleteRows = false;
            this.dataGridViewEx1.AllowUserToOrderColumns = true;
            this.dataGridViewEx1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Meiryo UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewEx1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewEx1.EnableLastIndex = true;
            this.dataGridViewEx1.Location = new System.Drawing.Point(12, 39);
            this.dataGridViewEx1.Name = "dataGridViewEx1";
            this.dataGridViewEx1.RowHeadersVisible = false;
            this.dataGridViewEx1.RowTemplate.Height = 21;
            this.dataGridViewEx1.Size = new System.Drawing.Size(1000, 552);
            this.dataGridViewEx1.TabIndex = 225;
            this.dataGridViewEx1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewEx1_CellMouseClick);
            // 
            // panelEx1
            // 
            this.panelEx1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelEx1.Controls.Add(this.buttonEx_LineDel);
            this.panelEx1.Location = new System.Drawing.Point(177, 607);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.OutLineColor = System.Drawing.Color.Empty;
            this.panelEx1.OutLineSize = 0;
            this.panelEx1.Size = new System.Drawing.Size(169, 65);
            this.panelEx1.TabIndex = 226;
            // 
            // buttonEx_LineDel
            // 
            this.buttonEx_LineDel.EditBox = null;
            this.buttonEx_LineDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEx_LineDel.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonEx_LineDel.IsActive = false;
            this.buttonEx_LineDel.Location = new System.Drawing.Point(7, 6);
            this.buttonEx_LineDel.MultiSelectEn = false;
            this.buttonEx_LineDel.Name = "buttonEx_LineDel";
            this.buttonEx_LineDel.OutLineColor = System.Drawing.Color.Empty;
            this.buttonEx_LineDel.OutLineEn = true;
            this.buttonEx_LineDel.OutLineSize = 3;
            this.buttonEx_LineDel.Selectable = true;
            this.buttonEx_LineDel.Selected = false;
            this.buttonEx_LineDel.Size = new System.Drawing.Size(150, 50);
            this.buttonEx_LineDel.StatusLedEnable = false;
            this.buttonEx_LineDel.StatusLedSize = ((byte)(15));
            this.buttonEx_LineDel.TabIndex = 1;
            this.buttonEx_LineDel.Text = "行削除";
            this.buttonEx_LineDel.UseVisualStyleBackColor = true;
            this.buttonEx_LineDel.Click += new System.EventHandler(this.buttonEx_LineDel_Click);
            // 
            // panelEx2
            // 
            this.panelEx2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelEx2.Controls.Add(this.buttonEx_LineAdd);
            this.panelEx2.Location = new System.Drawing.Point(11, 607);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.OutLineColor = System.Drawing.Color.Empty;
            this.panelEx2.OutLineSize = 0;
            this.panelEx2.Size = new System.Drawing.Size(169, 65);
            this.panelEx2.TabIndex = 227;
            // 
            // buttonEx_LineAdd
            // 
            this.buttonEx_LineAdd.EditBox = null;
            this.buttonEx_LineAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEx_LineAdd.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonEx_LineAdd.IsActive = false;
            this.buttonEx_LineAdd.Location = new System.Drawing.Point(7, 6);
            this.buttonEx_LineAdd.MultiSelectEn = false;
            this.buttonEx_LineAdd.Name = "buttonEx_LineAdd";
            this.buttonEx_LineAdd.OutLineColor = System.Drawing.Color.Empty;
            this.buttonEx_LineAdd.OutLineEn = true;
            this.buttonEx_LineAdd.OutLineSize = 3;
            this.buttonEx_LineAdd.Selectable = true;
            this.buttonEx_LineAdd.Selected = false;
            this.buttonEx_LineAdd.Size = new System.Drawing.Size(150, 50);
            this.buttonEx_LineAdd.StatusLedEnable = false;
            this.buttonEx_LineAdd.StatusLedSize = ((byte)(15));
            this.buttonEx_LineAdd.TabIndex = 1;
            this.buttonEx_LineAdd.Text = "行追加";
            this.buttonEx_LineAdd.UseVisualStyleBackColor = true;
            this.buttonEx_LineAdd.Click += new System.EventHandler(this.buttonEx_LineAdd_Click);
            // 
            // MaintenanceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 678);
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.dataGridViewEx1);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label8);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(0, 90);
            this.Name = "MaintenanceForm";
            this.OutLineSize = 3;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "MaintenanceForm";
            this.Load += new System.EventHandler(this.MaintenanceForm_Load);
            this.panel6.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEx1)).EndInit();
            this.panelEx1.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private LabelEx label8;
		private PanelEx panel6;
		private ButtonEx buttonEx_Reset;
		private PanelEx panel2;
		private ButtonEx BackUserFuncFormBt;
		private DataGridViewEx dataGridViewEx1;
		private PanelEx panelEx1;
		private ButtonEx buttonEx_LineDel;
		private PanelEx panelEx2;
		private ButtonEx buttonEx_LineAdd;
	}
}