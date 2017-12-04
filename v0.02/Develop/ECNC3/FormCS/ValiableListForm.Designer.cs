namespace ECNC3.Views
{
    partial class ValiableListForm
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
            this.listView_List = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView_ListName = new System.Windows.Forms.ListView();
            this.SelectListNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ListNameList = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel2 = new ECNC3.Views.PanelEx();
            this.ButtonEx_Return = new ECNC3.Views.ButtonEx();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.labelEx1 = new ECNC3.Views.LabelEx();
            this.labelEx2 = new ECNC3.Views.LabelEx();
            this.labelEx3 = new ECNC3.Views.LabelEx();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label8.Font = new System.Drawing.Font("HG丸ｺﾞｼｯｸM-PRO", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.Location = new System.Drawing.Point(7, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1012, 30);
            this.label8.TabIndex = 217;
            this.label8.Text = "VALIABLE LIST";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listView_List
            // 
            this.listView_List.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3}); 
            this.listView_List.Font = new System.Drawing.Font("MS UI Gothic", 16F);
            this.listView_List.FullRowSelect = true;
            this.listView_List.Location = new System.Drawing.Point(12, 41);
            this.listView_List.Name = "listView_List";
            this.listView_List.Size = new System.Drawing.Size(630, 577);
            this.listView_List.TabIndex = 218;
            this.listView_List.UseCompatibleStateImageBehavior = false;
            this.listView_List.View = System.Windows.Forms.View.Details;
            this.listView_List.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.listView1_RetrieveVirtualItem);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "#No.";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "DATA";
            this.columnHeader2.Width = 116;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "COMMENT";
            this.columnHeader3.Width = 140;
            // 
            // listView_ListName
            // 
            this.listView_ListName.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.SelectListNo,
            this.ListNameList}); 
            this.listView_ListName.Font = new System.Drawing.Font("MS UI Gothic", 16F);
            this.listView_ListName.FullRowSelect = true;
            this.listView_ListName.Location = new System.Drawing.Point(647, 41);
            this.listView_ListName.Name = "listView_ListName";
            this.listView_ListName.Size = new System.Drawing.Size(365, 475);
            this.listView_ListName.TabIndex = 219;
            this.listView_ListName.UseCompatibleStateImageBehavior = false;
            this.listView_ListName.View = System.Windows.Forms.View.Details;
            this.listView_ListName.Click += new System.EventHandler(this.listView_ListName_Click);
            // 
            // SelectListNo
            // 
            this.SelectListNo.Text = "No.";
            this.SelectListNo.Width = 68;
            // 
            // ListNameList
            // 
            this.ListNameList.Text = "LIST NAME";
            this.ListNameList.Width = 290;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.ButtonEx_Return);
            this.panel2.Location = new System.Drawing.Point(849, 549);
            this.panel2.Name = "panel2";
            this.panel2.OutLineColor = System.Drawing.Color.DarkOliveGreen;
            this.panel2.OutLineSize = 2;
            this.panel2.Size = new System.Drawing.Size(169, 69);
            this.panel2.TabIndex = 221;
            // 
            // ButtonEx_Return
            // 
            this.ButtonEx_Return.EditBox = null;
            this.ButtonEx_Return.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonEx_Return.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ButtonEx_Return.IsActive = false;
            this.ButtonEx_Return.Location = new System.Drawing.Point(8, 8);
            this.ButtonEx_Return.MultiSelectEn = false;
            this.ButtonEx_Return.Name = "ButtonEx_Return";
            this.ButtonEx_Return.OutLineColor = System.Drawing.Color.Empty;
            this.ButtonEx_Return.OutLineEn = true;
            this.ButtonEx_Return.OutLineSize = 3;
            this.ButtonEx_Return.Selectable = true;
            this.ButtonEx_Return.Selected = false;
            this.ButtonEx_Return.Size = new System.Drawing.Size(150, 50);
            this.ButtonEx_Return.StatusLedEnable = false;
            this.ButtonEx_Return.StatusLedSize = ((byte)(15));
            this.ButtonEx_Return.TabIndex = 15;
            this.ButtonEx_Return.Text = "閉じる";
            this.ButtonEx_Return.UseVisualStyleBackColor = true;
            this.ButtonEx_Return.Click += new System.EventHandler(this.ButtonEx_Return_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(648, 517);
            this.trackBar1.Maximum = 10000;
            this.trackBar1.Minimum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(199, 45);
            this.trackBar1.TabIndex = 223;
            this.trackBar1.Value = 100;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // labelEx1
            // 
            this.labelEx1.AutoSize = true;
            this.labelEx1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelEx1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelEx1.Location = new System.Drawing.Point(648, 548);
            this.labelEx1.Name = "labelEx1";
            this.labelEx1.Size = new System.Drawing.Size(40, 14);
            this.labelEx1.TabIndex = 224;
            this.labelEx1.Text = "100ms";
            // 
            // labelEx2
            // 
            this.labelEx2.AutoSize = true;
            this.labelEx2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelEx2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelEx2.Location = new System.Drawing.Point(796, 548);
            this.labelEx2.Name = "labelEx2";
            this.labelEx2.Size = new System.Drawing.Size(52, 14);
            this.labelEx2.TabIndex = 225;
            this.labelEx2.Text = "10000ms";
            // 
            // labelEx3
            // 
            this.labelEx3.AutoSize = true;
            this.labelEx3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelEx3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelEx3.Location = new System.Drawing.Point(726, 548);
            this.labelEx3.Name = "labelEx3";
            this.labelEx3.Size = new System.Drawing.Size(46, 14);
            this.labelEx3.TabIndex = 226;
            this.labelEx3.Text = "5000ms";
            // 
            // ValiableListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 626);
            this.Controls.Add(this.labelEx3);
            this.Controls.Add(this.labelEx2);
            this.Controls.Add(this.labelEx1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.listView_ListName);
            this.Controls.Add(this.listView_List);
            this.Controls.Add(this.label8);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(0, 90);
            this.Name = "ValiableListForm";
            this.OutLineSize = 3;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ValiableListForm";
            this.Load += new System.EventHandler(this.ValiableListForm_Load);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LabelEx label8;
        private System.Windows.Forms.ListView listView_List;
        private System.Windows.Forms.ListView listView_ListName;
        private System.Windows.Forms.ColumnHeader SelectListNo;
        private System.Windows.Forms.ColumnHeader ListNameList;
		private PanelEx panel2;
		private ButtonEx ButtonEx_Return;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.TrackBar trackBar1;
		private LabelEx labelEx1;
		private LabelEx labelEx2;
		private LabelEx labelEx3;
	}
}