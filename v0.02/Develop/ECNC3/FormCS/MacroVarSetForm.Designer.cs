namespace ECNC3.Views
{
    partial class MacroVarSetForm
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
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label_Read1 = new ECNC3.Views.LabelEx();
            this.label_Read2 = new ECNC3.Views.LabelEx();
            this.label_MaxCount = new ECNC3.Views.LabelEx();
            this.label_Sharp = new ECNC3.Views.LabelEx();
            this.numericTextBox1 = new ECNC3.Views.NumericTextBox();
            this.panel_NcRW = new ECNC3.Views.PanelEx();
            this.button_NcRead = new ECNC3.Views.ButtonEx();
            this.button_NcWrite = new ECNC3.Views.ButtonEx();
            this.panel_FileRW = new ECNC3.Views.PanelEx();
            this.button_FileWrite = new ECNC3.Views.ButtonEx();
            this.button_FileRead = new ECNC3.Views.ButtonEx();
            this.dataGridViewEx1 = new ECNC3.Views.DataGridViewEx();
            this.panel_Back = new ECNC3.Views.PanelEx();
            this.BackUserFuncFormBt = new ECNC3.Views.ButtonEx();
            this.label_Variable = new ECNC3.Views.LabelEx();
            this.button_10000Down = new ECNC3.Views.ButtonEx();
            this.button_1000Down = new ECNC3.Views.ButtonEx();
            this.button_100Down = new ECNC3.Views.ButtonEx();
            this.button_10Down = new ECNC3.Views.ButtonEx();
            this.button_10Up = new ECNC3.Views.ButtonEx();
            this.button_100Up = new ECNC3.Views.ButtonEx();
            this.button_1000Up = new ECNC3.Views.ButtonEx();
            this.button_10000Up = new ECNC3.Views.ButtonEx();
            this.panel_CountButton = new ECNC3.Views.PanelEx();
            this.labelEx1 = new ECNC3.Views.LabelEx();
            this.panel_NcRW.SuspendLayout();
            this.panel_FileRW.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEx1)).BeginInit();
            this.panel_Back.SuspendLayout();
            this.panel_CountButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // Column1
            // 
            this.Column1.HeaderText = "番号";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "変数値";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "備考";
            this.Column3.Name = "Column3";
            // 
            // label_Read1
            // 
            this.label_Read1.AutoSize = true;
            this.label_Read1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_Read1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label_Read1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Read1.Location = new System.Drawing.Point(360, 38);
            this.label_Read1.Name = "label_Read1";
            this.label_Read1.Size = new System.Drawing.Size(48, 18);
            this.label_Read1.TabIndex = 241;
            this.label_Read1.Text = "label1";
            this.label_Read1.Visible = false;
            // 
            // label_Read2
            // 
            this.label_Read2.AutoSize = true;
            this.label_Read2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_Read2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label_Read2.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Read2.Location = new System.Drawing.Point(572, 38);
            this.label_Read2.Name = "label_Read2";
            this.label_Read2.Size = new System.Drawing.Size(48, 18);
            this.label_Read2.TabIndex = 242;
            this.label_Read2.Text = "label2";
            this.label_Read2.Visible = false;
            // 
            // label_MaxCount
            // 
            this.label_MaxCount.AutoSize = true;
            this.label_MaxCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_MaxCount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label_MaxCount.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.label_MaxCount.Location = new System.Drawing.Point(146, 44);
            this.label_MaxCount.Name = "label_MaxCount";
            this.label_MaxCount.Size = new System.Drawing.Size(88, 26);
            this.label_MaxCount.TabIndex = 246;
            this.label_MaxCount.Text = "/99999";
            this.label_MaxCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_Sharp
            // 
            this.label_Sharp.AutoSize = true;
            this.label_Sharp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_Sharp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label_Sharp.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.label_Sharp.Location = new System.Drawing.Point(37, 44);
            this.label_Sharp.Name = "label_Sharp";
            this.label_Sharp.Size = new System.Drawing.Size(28, 26);
            this.label_Sharp.TabIndex = 247;
            this.label_Sharp.Text = "#";
            this.label_Sharp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericTextBox1
            // 
            this.numericTextBox1.Enabled = false;
            this.numericTextBox1.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.numericTextBox1.FormatType = ECNC3.Views.NumericTextBox.FormatTypes.Free;
            this.numericTextBox1.InputDataRatio = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericTextBox1.Location = new System.Drawing.Point(66, 41);
            this.numericTextBox1.LowerLimit = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numericTextBox1.Name = "numericTextBox1";
            this.numericTextBox1.RawText = "0";
            this.numericTextBox1.Size = new System.Drawing.Size(79, 32);
            this.numericTextBox1.TabIndex = 244;
            this.numericTextBox1.Text = "0";
            this.numericTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericTextBox1.UpperLimit = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numericTextBox1.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numericTextBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.numericTextBox1_MouseDown);
            // 
            // panel_NcRW
            // 
            this.panel_NcRW.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel_NcRW.Controls.Add(this.button_NcRead);
            this.panel_NcRW.Controls.Add(this.button_NcWrite);
            this.panel_NcRW.Location = new System.Drawing.Point(33, 607);
            this.panel_NcRW.Name = "panel_NcRW";
            this.panel_NcRW.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panel_NcRW.OutLineSize = 0F;
            this.panel_NcRW.Size = new System.Drawing.Size(305, 59);
            this.panel_NcRW.TabIndex = 213;
            // 
            // button_NcRead
            // 
            this.button_NcRead.EditBox = null;
            this.button_NcRead.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_NcRead.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_NcRead.IsActive = false;
            this.button_NcRead.LedSyncedBackColorEnable = true;
            this.button_NcRead.Location = new System.Drawing.Point(8, 8);
            this.button_NcRead.MultiSelectEn = false;
            this.button_NcRead.Name = "button_NcRead";
            this.button_NcRead.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button_NcRead.OutLineEn = true;
            this.button_NcRead.OutLineSize = 3F;
            this.button_NcRead.ProgressBarColor = System.Drawing.Color.Empty;
            this.button_NcRead.ProgressBarEnable = true;
            this.button_NcRead.ProgressBarMaxValue = 99999;
            this.button_NcRead.ProgressBarMinValue = 0;
            this.button_NcRead.ProgressBarSize = 5;
            this.button_NcRead.ProgressBarValue = 0;
            this.button_NcRead.Selectable = true;
            this.button_NcRead.Selected = false;
            this.button_NcRead.Size = new System.Drawing.Size(140, 40);
            this.button_NcRead.StatusLedEnable = false;
            this.button_NcRead.StatusLedSize = ((byte)(15));
            this.button_NcRead.TabIndex = 3;
            this.button_NcRead.Text = "NC読込み";
            this.button_NcRead.UseVisualStyleBackColor = true;
            this.button_NcRead.Click += new System.EventHandler(this.button_NcRead_Click);
            // 
            // button_NcWrite
            // 
            this.button_NcWrite.EditBox = null;
            this.button_NcWrite.Enabled = false;
            this.button_NcWrite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_NcWrite.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_NcWrite.IsActive = false;
            this.button_NcWrite.LedSyncedBackColorEnable = true;
            this.button_NcWrite.Location = new System.Drawing.Point(154, 8);
            this.button_NcWrite.MultiSelectEn = false;
            this.button_NcWrite.Name = "button_NcWrite";
            this.button_NcWrite.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button_NcWrite.OutLineEn = true;
            this.button_NcWrite.OutLineSize = 3F;
            this.button_NcWrite.ProgressBarColor = System.Drawing.Color.Empty;
            this.button_NcWrite.ProgressBarEnable = false;
            this.button_NcWrite.ProgressBarMaxValue = 100;
            this.button_NcWrite.ProgressBarMinValue = 0;
            this.button_NcWrite.ProgressBarSize = 5;
            this.button_NcWrite.ProgressBarValue = 0;
            this.button_NcWrite.Selectable = true;
            this.button_NcWrite.Selected = false;
            this.button_NcWrite.Size = new System.Drawing.Size(140, 40);
            this.button_NcWrite.StatusLedEnable = false;
            this.button_NcWrite.StatusLedSize = ((byte)(15));
            this.button_NcWrite.TabIndex = 2;
            this.button_NcWrite.Text = "NC書込み";
            this.button_NcWrite.UseVisualStyleBackColor = true;
            this.button_NcWrite.Click += new System.EventHandler(this.button_NcWrite_Click);
            // 
            // panel_FileRW
            // 
            this.panel_FileRW.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel_FileRW.Controls.Add(this.button_FileWrite);
            this.panel_FileRW.Controls.Add(this.button_FileRead);
            this.panel_FileRW.Location = new System.Drawing.Point(344, 607);
            this.panel_FileRW.Name = "panel_FileRW";
            this.panel_FileRW.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panel_FileRW.OutLineSize = 0F;
            this.panel_FileRW.Size = new System.Drawing.Size(306, 59);
            this.panel_FileRW.TabIndex = 212;
            // 
            // button_FileWrite
            // 
            this.button_FileWrite.EditBox = null;
            this.button_FileWrite.Enabled = false;
            this.button_FileWrite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_FileWrite.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_FileWrite.IsActive = false;
            this.button_FileWrite.LedSyncedBackColorEnable = true;
            this.button_FileWrite.Location = new System.Drawing.Point(156, 8);
            this.button_FileWrite.MultiSelectEn = false;
            this.button_FileWrite.Name = "button_FileWrite";
            this.button_FileWrite.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button_FileWrite.OutLineEn = true;
            this.button_FileWrite.OutLineSize = 3F;
            this.button_FileWrite.ProgressBarColor = System.Drawing.Color.Empty;
            this.button_FileWrite.ProgressBarEnable = false;
            this.button_FileWrite.ProgressBarMaxValue = 100;
            this.button_FileWrite.ProgressBarMinValue = 0;
            this.button_FileWrite.ProgressBarSize = 5;
            this.button_FileWrite.ProgressBarValue = 0;
            this.button_FileWrite.Selectable = true;
            this.button_FileWrite.Selected = false;
            this.button_FileWrite.Size = new System.Drawing.Size(140, 40);
            this.button_FileWrite.StatusLedEnable = false;
            this.button_FileWrite.StatusLedSize = ((byte)(15));
            this.button_FileWrite.TabIndex = 2;
            this.button_FileWrite.Text = "ファイル書込み";
            this.button_FileWrite.UseVisualStyleBackColor = true;
            this.button_FileWrite.Click += new System.EventHandler(this.button_FileWrite_Click);
            // 
            // button_FileRead
            // 
            this.button_FileRead.EditBox = null;
            this.button_FileRead.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_FileRead.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_FileRead.IsActive = false;
            this.button_FileRead.LedSyncedBackColorEnable = true;
            this.button_FileRead.Location = new System.Drawing.Point(10, 8);
            this.button_FileRead.MultiSelectEn = false;
            this.button_FileRead.Name = "button_FileRead";
            this.button_FileRead.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button_FileRead.OutLineEn = true;
            this.button_FileRead.OutLineSize = 3F;
            this.button_FileRead.ProgressBarColor = System.Drawing.Color.Empty;
            this.button_FileRead.ProgressBarEnable = false;
            this.button_FileRead.ProgressBarMaxValue = 100;
            this.button_FileRead.ProgressBarMinValue = 0;
            this.button_FileRead.ProgressBarSize = 5;
            this.button_FileRead.ProgressBarValue = 0;
            this.button_FileRead.Selectable = true;
            this.button_FileRead.Selected = false;
            this.button_FileRead.Size = new System.Drawing.Size(140, 40);
            this.button_FileRead.StatusLedEnable = false;
            this.button_FileRead.StatusLedSize = ((byte)(15));
            this.button_FileRead.TabIndex = 3;
            this.button_FileRead.Text = "ファイル読込み";
            this.button_FileRead.UseVisualStyleBackColor = true;
            this.button_FileRead.Click += new System.EventHandler(this.button_FileRead_Click);
            // 
            // dataGridViewEx1
            // 
            this.dataGridViewEx1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEx1.EnableLastIndex = true;
            this.dataGridViewEx1.Location = new System.Drawing.Point(37, 76);
            this.dataGridViewEx1.Name = "dataGridViewEx1";
            this.dataGridViewEx1.RowTemplate.Height = 21;
            this.dataGridViewEx1.Size = new System.Drawing.Size(800, 513);
            this.dataGridViewEx1.TabIndex = 236;
            this.dataGridViewEx1.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewEx1_CellMouseDown);
            this.dataGridViewEx1.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewEx1_CellMouseUp);
            this.dataGridViewEx1.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridViewEx1_CellPainting);
            this.dataGridViewEx1.CurrentCellChanged += new System.EventHandler(this.dataGridViewEx1_CurrentCellChanged);
            // 
            // panel_Back
            // 
            this.panel_Back.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel_Back.Controls.Add(this.BackUserFuncFormBt);
            this.panel_Back.Location = new System.Drawing.Point(852, 609);
            this.panel_Back.Name = "panel_Back";
            this.panel_Back.OutLineColor = System.Drawing.Color.DarkOliveGreen;
            this.panel_Back.OutLineSize = 2F;
            this.panel_Back.Size = new System.Drawing.Size(160, 57);
            this.panel_Back.TabIndex = 211;
            // 
            // BackUserFuncFormBt
            // 
            this.BackUserFuncFormBt.EditBox = null;
            this.BackUserFuncFormBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BackUserFuncFormBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BackUserFuncFormBt.IsActive = false;
            this.BackUserFuncFormBt.LedSyncedBackColorEnable = true;
            this.BackUserFuncFormBt.Location = new System.Drawing.Point(8, 7);
            this.BackUserFuncFormBt.MultiSelectEn = false;
            this.BackUserFuncFormBt.Name = "BackUserFuncFormBt";
            this.BackUserFuncFormBt.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BackUserFuncFormBt.OutLineEn = true;
            this.BackUserFuncFormBt.OutLineSize = 3F;
            this.BackUserFuncFormBt.ProgressBarColor = System.Drawing.Color.Empty;
            this.BackUserFuncFormBt.ProgressBarEnable = false;
            this.BackUserFuncFormBt.ProgressBarMaxValue = 100;
            this.BackUserFuncFormBt.ProgressBarMinValue = 0;
            this.BackUserFuncFormBt.ProgressBarSize = 5;
            this.BackUserFuncFormBt.ProgressBarValue = 0;
            this.BackUserFuncFormBt.Selectable = true;
            this.BackUserFuncFormBt.Selected = false;
            this.BackUserFuncFormBt.Size = new System.Drawing.Size(140, 40);
            this.BackUserFuncFormBt.StatusLedEnable = false;
            this.BackUserFuncFormBt.StatusLedSize = ((byte)(15));
            this.BackUserFuncFormBt.TabIndex = 15;
            this.BackUserFuncFormBt.Text = "閉じる";
            this.BackUserFuncFormBt.UseVisualStyleBackColor = true;
            this.BackUserFuncFormBt.Click += new System.EventHandler(this.BackUserFuncFormBt_Click);
            // 
            // label_Variable
            // 
            this.label_Variable.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_Variable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label_Variable.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Variable.Location = new System.Drawing.Point(6, 7);
            this.label_Variable.Name = "label_Variable";
            this.label_Variable.Size = new System.Drawing.Size(1012, 30);
            this.label_Variable.TabIndex = 85;
            this.label_Variable.Text = "VALIABLE";
            this.label_Variable.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // button_10000Down
            // 
            this.button_10000Down.EditBox = null;
            this.button_10000Down.Enabled = false;
            this.button_10000Down.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_10000Down.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.button_10000Down.IsActive = false;
            this.button_10000Down.LedSyncedBackColorEnable = true;
            this.button_10000Down.Location = new System.Drawing.Point(67, 440);
            this.button_10000Down.MultiSelectEn = false;
            this.button_10000Down.Name = "button_10000Down";
            this.button_10000Down.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button_10000Down.OutLineEn = true;
            this.button_10000Down.OutLineSize = 3F;
            this.button_10000Down.ProgressBarColor = System.Drawing.Color.Empty;
            this.button_10000Down.ProgressBarEnable = false;
            this.button_10000Down.ProgressBarMaxValue = 100;
            this.button_10000Down.ProgressBarMinValue = 0;
            this.button_10000Down.ProgressBarSize = 5;
            this.button_10000Down.ProgressBarValue = 0;
            this.button_10000Down.Selectable = true;
            this.button_10000Down.Selected = false;
            this.button_10000Down.Size = new System.Drawing.Size(140, 40);
            this.button_10000Down.StatusLedEnable = false;
            this.button_10000Down.StatusLedSize = ((byte)(15));
            this.button_10000Down.TabIndex = 221;
            this.button_10000Down.Text = "10000▼";
            this.button_10000Down.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_10000Down.UseVisualStyleBackColor = true;
            this.button_10000Down.Click += new System.EventHandler(this.button_10000Down_Click);
            // 
            // button_1000Down
            // 
            this.button_1000Down.EditBox = null;
            this.button_1000Down.Enabled = false;
            this.button_1000Down.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_1000Down.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.button_1000Down.IsActive = false;
            this.button_1000Down.LedSyncedBackColorEnable = true;
            this.button_1000Down.Location = new System.Drawing.Point(67, 384);
            this.button_1000Down.MultiSelectEn = false;
            this.button_1000Down.Name = "button_1000Down";
            this.button_1000Down.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button_1000Down.OutLineEn = true;
            this.button_1000Down.OutLineSize = 3F;
            this.button_1000Down.ProgressBarColor = System.Drawing.Color.Empty;
            this.button_1000Down.ProgressBarEnable = false;
            this.button_1000Down.ProgressBarMaxValue = 100;
            this.button_1000Down.ProgressBarMinValue = 0;
            this.button_1000Down.ProgressBarSize = 5;
            this.button_1000Down.ProgressBarValue = 0;
            this.button_1000Down.Selectable = true;
            this.button_1000Down.Selected = false;
            this.button_1000Down.Size = new System.Drawing.Size(140, 40);
            this.button_1000Down.StatusLedEnable = false;
            this.button_1000Down.StatusLedSize = ((byte)(15));
            this.button_1000Down.TabIndex = 220;
            this.button_1000Down.Text = "1000▼";
            this.button_1000Down.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_1000Down.UseVisualStyleBackColor = true;
            this.button_1000Down.Click += new System.EventHandler(this.button_1000Down_Click);
            // 
            // button_100Down
            // 
            this.button_100Down.EditBox = null;
            this.button_100Down.Enabled = false;
            this.button_100Down.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_100Down.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.button_100Down.IsActive = false;
            this.button_100Down.LedSyncedBackColorEnable = true;
            this.button_100Down.Location = new System.Drawing.Point(67, 329);
            this.button_100Down.MultiSelectEn = false;
            this.button_100Down.Name = "button_100Down";
            this.button_100Down.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button_100Down.OutLineEn = true;
            this.button_100Down.OutLineSize = 3F;
            this.button_100Down.ProgressBarColor = System.Drawing.Color.Empty;
            this.button_100Down.ProgressBarEnable = false;
            this.button_100Down.ProgressBarMaxValue = 100;
            this.button_100Down.ProgressBarMinValue = 0;
            this.button_100Down.ProgressBarSize = 5;
            this.button_100Down.ProgressBarValue = 0;
            this.button_100Down.Selectable = true;
            this.button_100Down.Selected = false;
            this.button_100Down.Size = new System.Drawing.Size(140, 40);
            this.button_100Down.StatusLedEnable = false;
            this.button_100Down.StatusLedSize = ((byte)(15));
            this.button_100Down.TabIndex = 219;
            this.button_100Down.Text = "100▼";
            this.button_100Down.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_100Down.UseVisualStyleBackColor = true;
            this.button_100Down.Click += new System.EventHandler(this.button_100Down_Click);
            // 
            // button_10Down
            // 
            this.button_10Down.EditBox = null;
            this.button_10Down.Enabled = false;
            this.button_10Down.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_10Down.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.button_10Down.IsActive = false;
            this.button_10Down.LedSyncedBackColorEnable = true;
            this.button_10Down.Location = new System.Drawing.Point(67, 274);
            this.button_10Down.MultiSelectEn = false;
            this.button_10Down.Name = "button_10Down";
            this.button_10Down.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button_10Down.OutLineEn = true;
            this.button_10Down.OutLineSize = 3F;
            this.button_10Down.ProgressBarColor = System.Drawing.Color.Empty;
            this.button_10Down.ProgressBarEnable = false;
            this.button_10Down.ProgressBarMaxValue = 100;
            this.button_10Down.ProgressBarMinValue = 0;
            this.button_10Down.ProgressBarSize = 5;
            this.button_10Down.ProgressBarValue = 0;
            this.button_10Down.Selectable = true;
            this.button_10Down.Selected = false;
            this.button_10Down.Size = new System.Drawing.Size(140, 40);
            this.button_10Down.StatusLedEnable = false;
            this.button_10Down.StatusLedSize = ((byte)(15));
            this.button_10Down.TabIndex = 218;
            this.button_10Down.Text = "10▼";
            this.button_10Down.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_10Down.UseVisualStyleBackColor = true;
            this.button_10Down.Click += new System.EventHandler(this.button_10Down_Click);
            // 
            // button_10Up
            // 
            this.button_10Up.EditBox = null;
            this.button_10Up.Enabled = false;
            this.button_10Up.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_10Up.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.button_10Up.IsActive = false;
            this.button_10Up.LedSyncedBackColorEnable = true;
            this.button_10Up.Location = new System.Drawing.Point(67, 189);
            this.button_10Up.MultiSelectEn = false;
            this.button_10Up.Name = "button_10Up";
            this.button_10Up.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button_10Up.OutLineEn = true;
            this.button_10Up.OutLineSize = 3F;
            this.button_10Up.ProgressBarColor = System.Drawing.Color.Empty;
            this.button_10Up.ProgressBarEnable = false;
            this.button_10Up.ProgressBarMaxValue = 100;
            this.button_10Up.ProgressBarMinValue = 0;
            this.button_10Up.ProgressBarSize = 5;
            this.button_10Up.ProgressBarValue = 0;
            this.button_10Up.Selectable = true;
            this.button_10Up.Selected = false;
            this.button_10Up.Size = new System.Drawing.Size(140, 40);
            this.button_10Up.StatusLedEnable = false;
            this.button_10Up.StatusLedSize = ((byte)(15));
            this.button_10Up.TabIndex = 217;
            this.button_10Up.Text = "10▲";
            this.button_10Up.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_10Up.UseVisualStyleBackColor = true;
            this.button_10Up.Click += new System.EventHandler(this.button_10Up_Click);
            // 
            // button_100Up
            // 
            this.button_100Up.EditBox = null;
            this.button_100Up.Enabled = false;
            this.button_100Up.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_100Up.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.button_100Up.IsActive = false;
            this.button_100Up.LedSyncedBackColorEnable = true;
            this.button_100Up.Location = new System.Drawing.Point(67, 134);
            this.button_100Up.MultiSelectEn = false;
            this.button_100Up.Name = "button_100Up";
            this.button_100Up.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button_100Up.OutLineEn = true;
            this.button_100Up.OutLineSize = 3F;
            this.button_100Up.ProgressBarColor = System.Drawing.Color.Empty;
            this.button_100Up.ProgressBarEnable = false;
            this.button_100Up.ProgressBarMaxValue = 100;
            this.button_100Up.ProgressBarMinValue = 0;
            this.button_100Up.ProgressBarSize = 5;
            this.button_100Up.ProgressBarValue = 0;
            this.button_100Up.Selectable = true;
            this.button_100Up.Selected = false;
            this.button_100Up.Size = new System.Drawing.Size(140, 40);
            this.button_100Up.StatusLedEnable = false;
            this.button_100Up.StatusLedSize = ((byte)(15));
            this.button_100Up.TabIndex = 216;
            this.button_100Up.Text = "100▲";
            this.button_100Up.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_100Up.UseVisualStyleBackColor = true;
            this.button_100Up.Click += new System.EventHandler(this.button_100Up_Click);
            // 
            // button_1000Up
            // 
            this.button_1000Up.EditBox = null;
            this.button_1000Up.Enabled = false;
            this.button_1000Up.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_1000Up.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.button_1000Up.IsActive = false;
            this.button_1000Up.LedSyncedBackColorEnable = true;
            this.button_1000Up.Location = new System.Drawing.Point(67, 79);
            this.button_1000Up.MultiSelectEn = false;
            this.button_1000Up.Name = "button_1000Up";
            this.button_1000Up.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button_1000Up.OutLineEn = true;
            this.button_1000Up.OutLineSize = 3F;
            this.button_1000Up.ProgressBarColor = System.Drawing.Color.Empty;
            this.button_1000Up.ProgressBarEnable = false;
            this.button_1000Up.ProgressBarMaxValue = 100;
            this.button_1000Up.ProgressBarMinValue = 0;
            this.button_1000Up.ProgressBarSize = 5;
            this.button_1000Up.ProgressBarValue = 0;
            this.button_1000Up.Selectable = true;
            this.button_1000Up.Selected = false;
            this.button_1000Up.Size = new System.Drawing.Size(140, 40);
            this.button_1000Up.StatusLedEnable = false;
            this.button_1000Up.StatusLedSize = ((byte)(15));
            this.button_1000Up.TabIndex = 215;
            this.button_1000Up.Text = "1000▲";
            this.button_1000Up.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_1000Up.UseVisualStyleBackColor = true;
            this.button_1000Up.Click += new System.EventHandler(this.button_1000Up_Click);
            // 
            // button_10000Up
            // 
            this.button_10000Up.EditBox = null;
            this.button_10000Up.Enabled = false;
            this.button_10000Up.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_10000Up.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.button_10000Up.IsActive = false;
            this.button_10000Up.LedSyncedBackColorEnable = true;
            this.button_10000Up.Location = new System.Drawing.Point(67, 23);
            this.button_10000Up.MultiSelectEn = false;
            this.button_10000Up.Name = "button_10000Up";
            this.button_10000Up.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button_10000Up.OutLineEn = true;
            this.button_10000Up.OutLineSize = 3F;
            this.button_10000Up.ProgressBarColor = System.Drawing.Color.Empty;
            this.button_10000Up.ProgressBarEnable = false;
            this.button_10000Up.ProgressBarMaxValue = 100;
            this.button_10000Up.ProgressBarMinValue = 0;
            this.button_10000Up.ProgressBarSize = 5;
            this.button_10000Up.ProgressBarValue = 0;
            this.button_10000Up.Selectable = true;
            this.button_10000Up.Selected = false;
            this.button_10000Up.Size = new System.Drawing.Size(140, 40);
            this.button_10000Up.StatusLedEnable = false;
            this.button_10000Up.StatusLedSize = ((byte)(15));
            this.button_10000Up.TabIndex = 214;
            this.button_10000Up.Text = "10000▲";
            this.button_10000Up.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_10000Up.UseVisualStyleBackColor = true;
            this.button_10000Up.Click += new System.EventHandler(this.button_10000Up_Click);
            // 
            // panel_CountButton
            // 
            this.panel_CountButton.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel_CountButton.Controls.Add(this.labelEx1);
            this.panel_CountButton.Controls.Add(this.button_10000Up);
            this.panel_CountButton.Controls.Add(this.button_1000Up);
            this.panel_CountButton.Controls.Add(this.button_100Up);
            this.panel_CountButton.Controls.Add(this.button_10Up);
            this.panel_CountButton.Controls.Add(this.button_10Down);
            this.panel_CountButton.Controls.Add(this.button_100Down);
            this.panel_CountButton.Controls.Add(this.button_1000Down);
            this.panel_CountButton.Controls.Add(this.button_10000Down);
            this.panel_CountButton.Location = new System.Drawing.Point(793, 76);
            this.panel_CountButton.Name = "panel_CountButton";
            this.panel_CountButton.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panel_CountButton.OutLineSize = 0F;
            this.panel_CountButton.Size = new System.Drawing.Size(219, 513);
            this.panel_CountButton.TabIndex = 212;
            // 
            // labelEx1
            // 
            this.labelEx1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelEx1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelEx1.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelEx1.Location = new System.Drawing.Point(36, 250);
            this.labelEx1.Name = "labelEx1";
            this.labelEx1.Size = new System.Drawing.Size(181, 3);
            this.labelEx1.TabIndex = 248;
            this.labelEx1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // MacroVarSetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 678);
            this.Controls.Add(this.label_Sharp);
            this.Controls.Add(this.label_MaxCount);
            this.Controls.Add(this.numericTextBox1);
            this.Controls.Add(this.label_Read2);
            this.Controls.Add(this.label_Read1);
            this.Controls.Add(this.panel_NcRW);
            this.Controls.Add(this.panel_FileRW);
            this.Controls.Add(this.dataGridViewEx1);
            this.Controls.Add(this.panel_Back);
            this.Controls.Add(this.label_Variable);
            this.Controls.Add(this.panel_CountButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(0, 90);
            this.Name = "MacroVarSetForm";
            this.OutLineSize = 3;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "MacroVarSetForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MacroVarSetForm_FormClosed);
            this.Load += new System.EventHandler(this.MacroVarSetForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MacroVarSetForm_Paint);
            this.panel_NcRW.ResumeLayout(false);
            this.panel_FileRW.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEx1)).EndInit();
            this.panel_Back.ResumeLayout(false);
            this.panel_CountButton.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LabelEx label_Variable;
        private PanelEx panel_FileRW;
        private ButtonEx button_NcWrite;
        private PanelEx panel_Back;
        private ButtonEx BackUserFuncFormBt;
        private ButtonEx button_NcRead;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private PanelEx panel_NcRW;
        private ButtonEx button_FileWrite;
        private ButtonEx button_FileRead;
		private LabelEx label_Read1;
		private LabelEx label_Read2;
        private NumericTextBox numericTextBox1;
        private DataGridViewEx dataGridViewEx1;
        private LabelEx label_MaxCount;
        private LabelEx label_Sharp;
        private ButtonEx button_10000Down;
        private ButtonEx button_1000Down;
        private ButtonEx button_100Down;
        private ButtonEx button_10Down;
        private ButtonEx button_10Up;
        private ButtonEx button_100Up;
        private ButtonEx button_1000Up;
        private ButtonEx button_10000Up;
        private PanelEx panel_CountButton;
        private LabelEx labelEx1;
    }
}