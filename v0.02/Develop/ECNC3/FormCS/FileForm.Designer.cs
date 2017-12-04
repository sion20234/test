namespace ECNC3.Views
{
    partial class FileForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileForm));
            this._deleteBt = new ECNC3.Views.ButtonEx();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this._saveBt = new ECNC3.Views.ButtonEx();
            this.panel2 = new ECNC3.Views.PanelEx();
            this._btnReturn = new ECNC3.Views.ButtonEx();
            this.directoryListView = new System.Windows.Forms.ListView();
            this._nameCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._dateCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.statusBar = new System.Windows.Forms.ToolStripStatusLabel();
            this.FileProtectLabel = new ECNC3.Views.LabelEx();
            this.FileSizeLabel = new ECNC3.Views.LabelEx();
            this.FileContentsLabel = new ECNC3.Views.LabelEx();
            this.label8 = new ECNC3.Views.LabelEx();
            this.radioButtonEx_Program = new System.Windows.Forms.RadioButton();
            this.radioButtonEx_GCode = new System.Windows.Forms.RadioButton();
            this.radioButtonEx_MCode = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.labelEx_Status = new ECNC3.Views.LabelEx();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel2.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _deleteBt
            // 
            this._deleteBt.EditBox = null;
            this._deleteBt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
            this._deleteBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._deleteBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._deleteBt.IsActive = false;
            this._deleteBt.Location = new System.Drawing.Point(560, 610);
            this._deleteBt.MultiSelectEn = false;
            this._deleteBt.Name = "_deleteBt";
            this._deleteBt.OutLineColor = System.Drawing.Color.Empty;
            this._deleteBt.OutLineEn = true;
            this._deleteBt.OutLineSize = 3;
            this._deleteBt.Selectable = true;
            this._deleteBt.Selected = false;
            this._deleteBt.Size = new System.Drawing.Size(131, 39);
            this._deleteBt.StatusLedEnable = false;
            this._deleteBt.StatusLedSize = ((byte)(15));
            this._deleteBt.TabIndex = 374;
            this._deleteBt.Text = "削除";
            this._deleteBt.UseVisualStyleBackColor = false;
            this._deleteBt.Visible = false;
            this._deleteBt.Click += new System.EventHandler(this._deleteBt_Click);
            // 
            // treeView1
            // 
            this.treeView1.Font = new System.Drawing.Font("Meiryo UI", 18F);
            this.treeView1.Location = new System.Drawing.Point(12, 68);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(330, 529);
            this.treeView1.TabIndex = 373;
            this.treeView1.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeExpand);
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // _saveBt
            // 
            this._saveBt.EditBox = null;
            this._saveBt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
            this._saveBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._saveBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._saveBt.IsActive = false;
            this._saveBt.Location = new System.Drawing.Point(711, 610);
            this._saveBt.MultiSelectEn = false;
            this._saveBt.Name = "_saveBt";
            this._saveBt.OutLineColor = System.Drawing.Color.Empty;
            this._saveBt.OutLineEn = true;
            this._saveBt.OutLineSize = 3;
            this._saveBt.Selectable = true;
            this._saveBt.Selected = false;
            this._saveBt.Size = new System.Drawing.Size(131, 39);
            this._saveBt.StatusLedEnable = false;
            this._saveBt.StatusLedSize = ((byte)(15));
            this._saveBt.TabIndex = 372;
            this._saveBt.Text = "保存";
            this._saveBt.UseVisualStyleBackColor = false;
            this._saveBt.Visible = false;
            this._saveBt.Click += new System.EventHandler(this._saveBt_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this._btnReturn);
            this.panel2.Location = new System.Drawing.Point(857, 601);
            this.panel2.Name = "panel2";
            this.panel2.OutLineColor = System.Drawing.Color.DarkOliveGreen;
            this.panel2.OutLineSize = 2;
            this.panel2.Size = new System.Drawing.Size(160, 57);
            this.panel2.TabIndex = 99;
            // 
            // _btnReturn
            // 
            this._btnReturn.EditBox = null;
            this._btnReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnReturn.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._btnReturn.IsActive = false;
            this._btnReturn.Location = new System.Drawing.Point(9, 9);
            this._btnReturn.MultiSelectEn = false;
            this._btnReturn.Name = "_btnReturn";
            this._btnReturn.OutLineColor = System.Drawing.Color.Empty;
            this._btnReturn.OutLineEn = true;
            this._btnReturn.OutLineSize = 3;
            this._btnReturn.Selectable = true;
            this._btnReturn.Selected = false;
            this._btnReturn.Size = new System.Drawing.Size(140, 39);
            this._btnReturn.StatusLedEnable = false;
            this._btnReturn.StatusLedSize = ((byte)(15));
            this._btnReturn.TabIndex = 100;
            this._btnReturn.Text = "閉じる";
            this._btnReturn.UseVisualStyleBackColor = false;
            this._btnReturn.Click += new System.EventHandler(this._btnReturn_Click);
            // 
            // directoryListView
            // 
            this.directoryListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._nameCol,
            this._dateCol}); 
            this.directoryListView.Font = new System.Drawing.Font("Meiryo UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.directoryListView.LargeImageList = this.imageList1;
            this.directoryListView.Location = new System.Drawing.Point(343, 68);
            this.directoryListView.MultiSelect = false;
            this.directoryListView.Name = "directoryListView";
            this.directoryListView.Size = new System.Drawing.Size(674, 529);
            this.directoryListView.TabIndex = 251;
            this.directoryListView.UseCompatibleStateImageBehavior = false;
            this.directoryListView.View = System.Windows.Forms.View.Details;
            this.directoryListView.SelectedIndexChanged += new System.EventHandler(this.directoryListView_SelectedIndexChanged);
            this.directoryListView.Click += new System.EventHandler(this.directoryListView_Click);
            // 
            // _nameCol
            // 
            this._nameCol.Text = "名前";
            this._nameCol.Width = 335;
            // 
            // _dateCol
            // 
            this._dateCol.Text = "日付";
            this._dateCol.Width = 285;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Free32_UnRock.png");
            this.imageList1.Images.SetKeyName(1, "Free32_Rock.png");
            this.imageList1.Images.SetKeyName(2, "Free32_RockGold.png");
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.statusBar,
            this.toolStripStatusLabel1}); 
            this.statusStrip.Location = new System.Drawing.Point(0, 656);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1024, 22);
            this.statusStrip.TabIndex = 250;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // statusBar
            // 
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(0, 17);
            // 
            // FileProtectLabel
            // 
            this.FileProtectLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.FileProtectLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FileProtectLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FileProtectLabel.Location = new System.Drawing.Point(638, 41);
            this.FileProtectLabel.Name = "FileProtectLabel";
            this.FileProtectLabel.Size = new System.Drawing.Size(103, 24);
            this.FileProtectLabel.TabIndex = 243;
            this.FileProtectLabel.Text = "PROTECT";
            this.FileProtectLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FileSizeLabel
            // 
            this.FileSizeLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.FileSizeLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FileSizeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FileSizeLabel.Location = new System.Drawing.Point(343, 41);
            this.FileSizeLabel.Name = "FileSizeLabel";
            this.FileSizeLabel.Size = new System.Drawing.Size(294, 24);
            this.FileSizeLabel.TabIndex = 242;
            this.FileSizeLabel.Text = "0 file(s) Disk Free Space: 0.00GB";
            this.FileSizeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.FileSizeLabel.Visible = false;
            // 
            // FileContentsLabel
            // 
            this.FileContentsLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.FileContentsLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FileContentsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FileContentsLabel.Location = new System.Drawing.Point(12, 41);
            this.FileContentsLabel.Name = "FileContentsLabel";
            this.FileContentsLabel.Size = new System.Drawing.Size(330, 24);
            this.FileContentsLabel.TabIndex = 241;
            this.FileContentsLabel.Text = "Contents(ECNC)";
            this.FileContentsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label8.Font = new System.Drawing.Font("HG丸ｺﾞｼｯｸM-PRO", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.Location = new System.Drawing.Point(6, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1012, 30);
            this.label8.TabIndex = 217;
            this.label8.Text = "FILE";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // radioButtonEx_Program
            // 
            this.radioButtonEx_Program.AutoSize = true;
            this.radioButtonEx_Program.Checked = true;
            this.radioButtonEx_Program.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.radioButtonEx_Program.Location = new System.Drawing.Point(30, 610);
            this.radioButtonEx_Program.Name = "radioButtonEx_Program";
            this.radioButtonEx_Program.Size = new System.Drawing.Size(106, 28);
            this.radioButtonEx_Program.TabIndex = 381;
            this.radioButtonEx_Program.TabStop = true;
            this.radioButtonEx_Program.Text = "プログラム";
            this.radioButtonEx_Program.UseVisualStyleBackColor = true;
            this.radioButtonEx_Program.Visible = false;
            this.radioButtonEx_Program.CheckedChanged += new System.EventHandler(this.radioButtonEx_Program_CheckedChanged);
            // 
            // radioButtonEx_GCode
            // 
            this.radioButtonEx_GCode.AutoSize = true;
            this.radioButtonEx_GCode.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.radioButtonEx_GCode.Location = new System.Drawing.Point(167, 610);
            this.radioButtonEx_GCode.Name = "radioButtonEx_GCode";
            this.radioButtonEx_GCode.Size = new System.Drawing.Size(88, 28);
            this.radioButtonEx_GCode.TabIndex = 382;
            this.radioButtonEx_GCode.Text = "Gコード";
            this.radioButtonEx_GCode.UseVisualStyleBackColor = true;
            this.radioButtonEx_GCode.Visible = false;
            this.radioButtonEx_GCode.CheckedChanged += new System.EventHandler(this.radioButtonEx_GCode_CheckedChanged);
            // 
            // radioButtonEx_MCode
            // 
            this.radioButtonEx_MCode.AutoSize = true;
            this.radioButtonEx_MCode.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.radioButtonEx_MCode.Location = new System.Drawing.Point(297, 610);
            this.radioButtonEx_MCode.Name = "radioButtonEx_MCode";
            this.radioButtonEx_MCode.Size = new System.Drawing.Size(90, 28);
            this.radioButtonEx_MCode.TabIndex = 383;
            this.radioButtonEx_MCode.Text = "Mコード";
            this.radioButtonEx_MCode.UseVisualStyleBackColor = true;
            this.radioButtonEx_MCode.Visible = false;
            this.radioButtonEx_MCode.CheckedChanged += new System.EventHandler(this.radioButtonEx_MCode_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.radioButton1.Location = new System.Drawing.Point(417, 610);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(86, 28);
            this.radioButton1.TabIndex = 384;
            this.radioButton1.Text = "Tコード";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.Visible = false;
            // 
            // labelEx_Status
            // 
            this.labelEx_Status.AutoSize = true;
            this.labelEx_Status.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelEx_Status.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelEx_Status.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelEx_Status.Location = new System.Drawing.Point(18, 742);
            this.labelEx_Status.Name = "labelEx_Status";
            this.labelEx_Status.Size = new System.Drawing.Size(2, 18);
            this.labelEx_Status.TabIndex = 385;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // FileForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1024, 678);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.labelEx_Status);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.radioButtonEx_MCode);
            this.Controls.Add(this.radioButtonEx_GCode);
            this.Controls.Add(this.radioButtonEx_Program);
            this.Controls.Add(this._deleteBt);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this._saveBt);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.directoryListView);
            this.Controls.Add(this.FileProtectLabel);
            this.Controls.Add(this.FileSizeLabel);
            this.Controls.Add(this.FileContentsLabel);
            this.Controls.Add(this.label8);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(0, 90);
            this.Name = "FileForm";
            this.OutLineSize = 3;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FileForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FileForm_FormClosed);
            this.Load += new System.EventHandler(this.FileForm_Load);
            this.Click += new System.EventHandler(this.FileForm_Click);
            this.panel2.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LabelEx label8;
        private LabelEx FileContentsLabel;
        private LabelEx FileSizeLabel;
        private LabelEx FileProtectLabel;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel statusBar;
        private System.Windows.Forms.ListView directoryListView;
        private System.Windows.Forms.ColumnHeader _nameCol;
        private System.Windows.Forms.ColumnHeader _dateCol;
        private PanelEx panel2;
        private ButtonEx _btnReturn;
        private ButtonEx _saveBt;
		private System.Windows.Forms.TreeView treeView1;
		private ButtonEx _deleteBt;
        private System.Windows.Forms.RadioButton radioButtonEx_Program;
        private System.Windows.Forms.RadioButton radioButtonEx_GCode;
        private System.Windows.Forms.RadioButton radioButtonEx_MCode;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.RadioButton radioButton1;
        private LabelEx labelEx_Status;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}