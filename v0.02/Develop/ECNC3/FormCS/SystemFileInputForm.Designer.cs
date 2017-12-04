namespace ECNC3.Views
{
    partial class SystemFileInputForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SystemFileInputForm));
            this.label8 = new ECNC3.Views.LabelEx();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this._sysFileStatusList = new ECNC3.Views.ListBoxEx();
            this.dataGridViewEx1 = new ECNC3.Views.DataGridViewEx();
            this.panel4 = new ECNC3.Views.PanelEx();
            this.BackUserServiceFormBt = new ECNC3.Views.ButtonEx();
            this.panel8 = new ECNC3.Views.PanelEx();
            this._startBt = new ECNC3.Views.ButtonEx();
            this._backUpBt = new ECNC3.Views.ButtonEx();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEx1)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label8.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.Location = new System.Drawing.Point(7, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1012, 30);
            this.label8.TabIndex = 223;
            this.label8.Text = "SYSTEM FILE INPORT ";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "cancel.png");
            this.imageList1.Images.SetKeyName(1, "delete.png");
            this.imageList1.Images.SetKeyName(2, "error.png");
            this.imageList1.Images.SetKeyName(3, "accept.png");
            // 
            // _sysFileStatusList
            // 
            this._sysFileStatusList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._sysFileStatusList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this._sysFileStatusList.FormattingEnabled = true;
            this._sysFileStatusList.ItemHeight = 12;
            this._sysFileStatusList.Location = new System.Drawing.Point(690, 53);
            this._sysFileStatusList.Name = "_sysFileStatusList";
            this._sysFileStatusList.Size = new System.Drawing.Size(322, 410);
            this._sysFileStatusList.TabIndex = 227;
            this._sysFileStatusList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this._sysFileStatusList_DrawItem);
            this._sysFileStatusList.SelectedIndexChanged += new System.EventHandler(this._sysFileStatusList_SelectedIndexChanged);
            // 
            // dataGridViewEx1
            // 
            this.dataGridViewEx1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
            this.dataGridViewEx1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewEx1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEx1.EnableLastIndex = true;
            this.dataGridViewEx1.Location = new System.Drawing.Point(12, 42);
            this.dataGridViewEx1.MultiSelect = false;
            this.dataGridViewEx1.Name = "dataGridViewEx1";
            this.dataGridViewEx1.RowHeadersVisible = false;
            this.dataGridViewEx1.RowTemplate.Height = 21;
            this.dataGridViewEx1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewEx1.Size = new System.Drawing.Size(658, 622);
            this.dataGridViewEx1.TabIndex = 226;
            this.dataGridViewEx1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewEx1_CellClick);
            this.dataGridViewEx1.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewEx1_CellMouseDown);
            this.dataGridViewEx1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dataGridViewEx1_MouseMove);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.BackUserServiceFormBt);
            this.panel4.Location = new System.Drawing.Point(852, 607);
            this.panel4.Name = "panel4";
            this.panel4.OutLineColor = System.Drawing.Color.DarkOliveGreen;
            this.panel4.OutLineSize = 2;
            this.panel4.Size = new System.Drawing.Size(160, 57);
            this.panel4.TabIndex = 224;
            // 
            // BackUserServiceFormBt
            // 
            this.BackUserServiceFormBt.EditBox = null;
            this.BackUserServiceFormBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BackUserServiceFormBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BackUserServiceFormBt.IsActive = false;
            this.BackUserServiceFormBt.Location = new System.Drawing.Point(9, 9);
            this.BackUserServiceFormBt.MultiSelectEn = false;
            this.BackUserServiceFormBt.Name = "BackUserServiceFormBt";
            this.BackUserServiceFormBt.OutLineColor = System.Drawing.Color.Empty;
            this.BackUserServiceFormBt.OutLineEn = true;
            this.BackUserServiceFormBt.OutLineSize = 3;
            this.BackUserServiceFormBt.Selectable = true;
            this.BackUserServiceFormBt.Size = new System.Drawing.Size(140, 39);
            this.BackUserServiceFormBt.StatusLedEnable = false;
            this.BackUserServiceFormBt.StatusLedSize = ((byte)(15));
            this.BackUserServiceFormBt.TabIndex = 15;
            this.BackUserServiceFormBt.Text = "閉じる";
            this.BackUserServiceFormBt.UseVisualStyleBackColor = true;
            this.BackUserServiceFormBt.Click += new System.EventHandler(this.BackUserServiceFormBt_Click);
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this._startBt);
            this.panel8.Location = new System.Drawing.Point(852, 544);
            this.panel8.Name = "panel8";
            this.panel8.OutLineColor = System.Drawing.Color.LightCoral;
            this.panel8.OutLineSize = 2;
            this.panel8.Size = new System.Drawing.Size(160, 57);
            this.panel8.TabIndex = 225;
            // 
            // _startBt
            // 
            this._startBt.EditBox = null;
            this._startBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._startBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._startBt.IsActive = false;
            this._startBt.Location = new System.Drawing.Point(9, 9);
            this._startBt.MultiSelectEn = false;
            this._startBt.Name = "_startBt";
            this._startBt.OutLineColor = System.Drawing.Color.Empty;
            this._startBt.OutLineEn = true;
            this._startBt.OutLineSize = 3;
            this._startBt.Selectable = true;
            this._startBt.Size = new System.Drawing.Size(140, 39);
            this._startBt.StatusLedEnable = false;
            this._startBt.StatusLedSize = ((byte)(15));
            this._startBt.TabIndex = 2;
            this._startBt.Text = "開始";
            this._startBt.UseVisualStyleBackColor = true;
            this._startBt.Click += new System.EventHandler(this._startBt_Click);
            // 
            // _backUpBt
            // 
            this._backUpBt.EditBox = null;
            this._backUpBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._backUpBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._backUpBt.IsActive = false;
            this._backUpBt.Location = new System.Drawing.Point(861, 485);
            this._backUpBt.MultiSelectEn = false;
            this._backUpBt.Name = "_backUpBt";
            this._backUpBt.OutLineColor = System.Drawing.Color.Empty;
            this._backUpBt.OutLineEn = true;
            this._backUpBt.OutLineSize = 3;
            this._backUpBt.Selectable = true;
            this._backUpBt.Size = new System.Drawing.Size(140, 39);
            this._backUpBt.StatusLedEnable = false;
            this._backUpBt.StatusLedSize = ((byte)(15));
            this._backUpBt.TabIndex = 16;
            this._backUpBt.Text = "バックアップ";
            this._backUpBt.UseVisualStyleBackColor = true;
            this._backUpBt.Click += new System.EventHandler(this._backUpBt_Click);
            // 
            // SystemFileInputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 678);
            this.Controls.Add(this._backUpBt);
            this.Controls.Add(this._sysFileStatusList);
            this.Controls.Add(this.dataGridViewEx1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.label8);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(0, 90);
            this.Name = "SystemFileInputForm";
            this.OutLineSize = 3;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SystemFileInputForm";
            this.Load += new System.EventHandler(this.SystemFileInputForm_Load);
            this.Shown += new System.EventHandler(this.SystemFileInputForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEx1)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private PanelEx panel4;
        private ButtonEx BackUserServiceFormBt;
        private PanelEx panel8;
        private ButtonEx _startBt;
        private LabelEx label8;
        private DataGridViewEx dataGridViewEx1;
        private System.Windows.Forms.ImageList imageList1;
        private ListBoxEx _sysFileStatusList;
        private ButtonEx _backUpBt;
    }
}