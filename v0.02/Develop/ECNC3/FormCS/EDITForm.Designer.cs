namespace ECNC3.Views
{
    partial class EDITForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EDITForm));
            this.ProgramEditBox = new ECNC3.Views.RichTextBoxEx();
            this.panelEx1 = new ECNC3.Views.PanelEx();
            this.TeachingSetBt = new ECNC3.Views.ButtonEx();
            this._SelectVariablesComboBox = new ECNC3.Views.ComboBoxEx();
            this._TeachBtn = new ECNC3.Views.ButtonEx();
            this._SelectPositionsComboBox = new ECNC3.Views.ComboBoxEx();
            this.HelpBt = new ECNC3.Views.ButtonEx();
            this.panel1 = new ECNC3.Views.PanelEx();
            this._returnSerchBt = new ECNC3.Views.ButtonEx();
            this._keybordBt = new ECNC3.Views.ButtonEx();
            this._nextSerchBt = new ECNC3.Views.ButtonEx();
            this._serchBt = new ECNC3.Views.ButtonEx();
            this._newBt = new ECNC3.Views.ButtonEx();
            this._fileExpBt = new ECNC3.Views.ButtonEx();
            this._saveBt = new ECNC3.Views.ButtonEx();
            this._FileNameTextBox = new ECNC3.Views.TextBoxEx();
            this.label2 = new ECNC3.Views.LabelEx();
            this._programStatusLabel = new ECNC3.Views.LabelEx();
            this.panelEx1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ProgramEditBox
            // 
            this.ProgramEditBox.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ProgramEditBox.Location = new System.Drawing.Point(12, 131);
            this.ProgramEditBox.Name = "ProgramEditBox";
            this.ProgramEditBox.RowBackColorEn = true;
            this.ProgramEditBox.RowBackColorKey = ((System.Collections.Generic.List<string>)(resources.GetObject("ProgramEditBox.RowBackColorKey")));
            this.ProgramEditBox.Size = new System.Drawing.Size(1000, 535);
            this.ProgramEditBox.TabIndex = 172;
            this.ProgramEditBox.Text = "";
            // 
            // panelEx1
            // 
            this.panelEx1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelEx1.Controls.Add(this.TeachingSetBt);
            this.panelEx1.Controls.Add(this._SelectVariablesComboBox);
            this.panelEx1.Controls.Add(this._TeachBtn);
            this.panelEx1.Controls.Add(this._SelectPositionsComboBox);
            this.panelEx1.Controls.Add(this.HelpBt);
            this.panelEx1.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.panelEx1.Location = new System.Drawing.Point(674, 7);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panelEx1.OutLineSize = 0F;
            this.panelEx1.Size = new System.Drawing.Size(337, 92);
            this.panelEx1.TabIndex = 171;
            // 
            // TeachingSetBt
            // 
            this.TeachingSetBt.EditBox = null;
            this.TeachingSetBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TeachingSetBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TeachingSetBt.IsActive = false;
            this.TeachingSetBt.LedSyncedBackColorEnable = true;
            this.TeachingSetBt.Location = new System.Drawing.Point(118, 47);
            this.TeachingSetBt.MultiSelectEn = false;
            this.TeachingSetBt.Name = "TeachingSetBt";
            this.TeachingSetBt.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.TeachingSetBt.OutLineEn = true;
            this.TeachingSetBt.OutLineSize = 3F;
            this.TeachingSetBt.ProgressBarColor = System.Drawing.Color.Empty;
            this.TeachingSetBt.ProgressBarEnable = false;
            this.TeachingSetBt.ProgressBarMaxValue = 100;
            this.TeachingSetBt.ProgressBarMinValue = 0;
            this.TeachingSetBt.ProgressBarSize = 5;
            this.TeachingSetBt.ProgressBarValue = 0;
            this.TeachingSetBt.Selectable = false;
            this.TeachingSetBt.Selected = false;
            this.TeachingSetBt.Size = new System.Drawing.Size(107, 38);
            this.TeachingSetBt.StatusLedEnable = false;
            this.TeachingSetBt.StatusLedSize = ((byte)(15));
            this.TeachingSetBt.TabIndex = 144;
            this.TeachingSetBt.TabStop = false;
            this.TeachingSetBt.Text = "設定";
            this.TeachingSetBt.UseVisualStyleBackColor = false;
            this.TeachingSetBt.Click += new System.EventHandler(this.TeachingSetBt_Click);
            // 
            // _SelectVariablesComboBox
            // 
            this._SelectVariablesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._SelectVariablesComboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._SelectVariablesComboBox.Font = new System.Drawing.Font("Meiryo UI", 14F);
            this._SelectVariablesComboBox.FormattingEnabled = true;
            this._SelectVariablesComboBox.Items.AddRange(new object[] {
            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G"});
            this._SelectVariablesComboBox.Location = new System.Drawing.Point(197, 9);
            this._SelectVariablesComboBox.MaxDropDownItems = 10;
            this._SelectVariablesComboBox.Name = "_SelectVariablesComboBox";
            this._SelectVariablesComboBox.Size = new System.Drawing.Size(133, 32);
            this._SelectVariablesComboBox.TabIndex = 221;
            // 
            // _TeachBtn
            // 
            this._TeachBtn.EditBox = null;
            this._TeachBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._TeachBtn.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._TeachBtn.IsActive = false;
            this._TeachBtn.LedSyncedBackColorEnable = true;
            this._TeachBtn.Location = new System.Drawing.Point(5, 47);
            this._TeachBtn.MultiSelectEn = false;
            this._TeachBtn.Name = "_TeachBtn";
            this._TeachBtn.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._TeachBtn.OutLineEn = true;
            this._TeachBtn.OutLineSize = 3F;
            this._TeachBtn.ProgressBarColor = System.Drawing.Color.Empty;
            this._TeachBtn.ProgressBarEnable = false;
            this._TeachBtn.ProgressBarMaxValue = 100;
            this._TeachBtn.ProgressBarMinValue = 0;
            this._TeachBtn.ProgressBarSize = 5;
            this._TeachBtn.ProgressBarValue = 0;
            this._TeachBtn.Selectable = false;
            this._TeachBtn.Selected = false;
            this._TeachBtn.Size = new System.Drawing.Size(107, 38);
            this._TeachBtn.StatusLedEnable = false;
            this._TeachBtn.StatusLedSize = ((byte)(15));
            this._TeachBtn.TabIndex = 141;
            this._TeachBtn.TabStop = false;
            this._TeachBtn.Text = "ティーチ";
            this._TeachBtn.UseVisualStyleBackColor = false;
            this._TeachBtn.Click += new System.EventHandler(this._TeachBtn_Click);
            // 
            // _SelectPositionsComboBox
            // 
            this._SelectPositionsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._SelectPositionsComboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._SelectPositionsComboBox.Font = new System.Drawing.Font("Meiryo UI", 14F);
            this._SelectPositionsComboBox.FormattingEnabled = true;
            this._SelectPositionsComboBox.Items.AddRange(new object[] {
            "機械座標",
            "ワーク座標",
            "測定点"});
            this._SelectPositionsComboBox.Location = new System.Drawing.Point(6, 9);
            this._SelectPositionsComboBox.MaxDropDownItems = 6;
            this._SelectPositionsComboBox.Name = "_SelectPositionsComboBox";
            this._SelectPositionsComboBox.Size = new System.Drawing.Size(185, 32);
            this._SelectPositionsComboBox.TabIndex = 220;
            // 
            // HelpBt
            // 
            this.HelpBt.EditBox = null;
            this.HelpBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HelpBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.HelpBt.IsActive = false;
            this.HelpBt.LedSyncedBackColorEnable = true;
            this.HelpBt.Location = new System.Drawing.Point(231, 47);
            this.HelpBt.MultiSelectEn = false;
            this.HelpBt.Name = "HelpBt";
            this.HelpBt.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.HelpBt.OutLineEn = true;
            this.HelpBt.OutLineSize = 3F;
            this.HelpBt.ProgressBarColor = System.Drawing.Color.Empty;
            this.HelpBt.ProgressBarEnable = false;
            this.HelpBt.ProgressBarMaxValue = 100;
            this.HelpBt.ProgressBarMinValue = 0;
            this.HelpBt.ProgressBarSize = 5;
            this.HelpBt.ProgressBarValue = 0;
            this.HelpBt.Selectable = false;
            this.HelpBt.Selected = false;
            this.HelpBt.Size = new System.Drawing.Size(99, 38);
            this.HelpBt.StatusLedEnable = false;
            this.HelpBt.StatusLedSize = ((byte)(15));
            this.HelpBt.TabIndex = 136;
            this.HelpBt.TabStop = false;
            this.HelpBt.Text = "ヘルプ";
            this.HelpBt.UseVisualStyleBackColor = false;
            this.HelpBt.Click += new System.EventHandler(this.HelpBt_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this._returnSerchBt);
            this.panel1.Controls.Add(this._keybordBt);
            this.panel1.Controls.Add(this._nextSerchBt);
            this.panel1.Controls.Add(this._serchBt);
            this.panel1.Controls.Add(this._newBt);
            this.panel1.Controls.Add(this._fileExpBt);
            this.panel1.Controls.Add(this._saveBt);
            this.panel1.Controls.Add(this._FileNameTextBox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.panel1.Location = new System.Drawing.Point(12, 7);
            this.panel1.Name = "panel1";
            this.panel1.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panel1.OutLineSize = 0F;
            this.panel1.Size = new System.Drawing.Size(656, 92);
            this.panel1.TabIndex = 170;
            // 
            // _returnSerchBt
            // 
            this._returnSerchBt.EditBox = null;
            this._returnSerchBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._returnSerchBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._returnSerchBt.IsActive = false;
            this._returnSerchBt.LedSyncedBackColorEnable = true;
            this._returnSerchBt.Location = new System.Drawing.Point(556, 47);
            this._returnSerchBt.MultiSelectEn = false;
            this._returnSerchBt.Name = "_returnSerchBt";
            this._returnSerchBt.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._returnSerchBt.OutLineEn = true;
            this._returnSerchBt.OutLineSize = 3F;
            this._returnSerchBt.ProgressBarColor = System.Drawing.Color.Empty;
            this._returnSerchBt.ProgressBarEnable = false;
            this._returnSerchBt.ProgressBarMaxValue = 100;
            this._returnSerchBt.ProgressBarMinValue = 0;
            this._returnSerchBt.ProgressBarSize = 5;
            this._returnSerchBt.ProgressBarValue = 0;
            this._returnSerchBt.Selectable = false;
            this._returnSerchBt.Selected = false;
            this._returnSerchBt.Size = new System.Drawing.Size(92, 38);
            this._returnSerchBt.StatusLedEnable = false;
            this._returnSerchBt.StatusLedSize = ((byte)(15));
            this._returnSerchBt.TabIndex = 176;
            this._returnSerchBt.TabStop = false;
            this._returnSerchBt.Text = "前へ";
            this._returnSerchBt.UseVisualStyleBackColor = false;
            this._returnSerchBt.MouseUp += new System.Windows.Forms.MouseEventHandler(this._returnSerchBt_MouseUp);
            // 
            // _keybordBt
            // 
            this._keybordBt.EditBox = null;
            this._keybordBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._keybordBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._keybordBt.IsActive = false;
            this._keybordBt.LedSyncedBackColorEnable = true;
            this._keybordBt.Location = new System.Drawing.Point(232, 47);
            this._keybordBt.MultiSelectEn = false;
            this._keybordBt.Name = "_keybordBt";
            this._keybordBt.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._keybordBt.OutLineEn = true;
            this._keybordBt.OutLineSize = 3F;
            this._keybordBt.ProgressBarColor = System.Drawing.Color.Empty;
            this._keybordBt.ProgressBarEnable = false;
            this._keybordBt.ProgressBarMaxValue = 100;
            this._keybordBt.ProgressBarMinValue = 0;
            this._keybordBt.ProgressBarSize = 5;
            this._keybordBt.ProgressBarValue = 0;
            this._keybordBt.Selectable = false;
            this._keybordBt.Selected = false;
            this._keybordBt.Size = new System.Drawing.Size(107, 38);
            this._keybordBt.StatusLedEnable = false;
            this._keybordBt.StatusLedSize = ((byte)(15));
            this._keybordBt.TabIndex = 168;
            this._keybordBt.TabStop = false;
            this._keybordBt.Text = "キーボード";
            this._keybordBt.UseVisualStyleBackColor = false;
            this._keybordBt.Click += new System.EventHandler(this._keybordBt_Click);
            // 
            // _nextSerchBt
            // 
            this._nextSerchBt.EditBox = null;
            this._nextSerchBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._nextSerchBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._nextSerchBt.IsActive = false;
            this._nextSerchBt.LedSyncedBackColorEnable = true;
            this._nextSerchBt.Location = new System.Drawing.Point(458, 47);
            this._nextSerchBt.MultiSelectEn = false;
            this._nextSerchBt.Name = "_nextSerchBt";
            this._nextSerchBt.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._nextSerchBt.OutLineEn = true;
            this._nextSerchBt.OutLineSize = 3F;
            this._nextSerchBt.ProgressBarColor = System.Drawing.Color.Empty;
            this._nextSerchBt.ProgressBarEnable = false;
            this._nextSerchBt.ProgressBarMaxValue = 100;
            this._nextSerchBt.ProgressBarMinValue = 0;
            this._nextSerchBt.ProgressBarSize = 5;
            this._nextSerchBt.ProgressBarValue = 0;
            this._nextSerchBt.Selectable = false;
            this._nextSerchBt.Selected = false;
            this._nextSerchBt.Size = new System.Drawing.Size(92, 38);
            this._nextSerchBt.StatusLedEnable = false;
            this._nextSerchBt.StatusLedSize = ((byte)(15));
            this._nextSerchBt.TabIndex = 175;
            this._nextSerchBt.TabStop = false;
            this._nextSerchBt.Text = "次へ";
            this._nextSerchBt.UseVisualStyleBackColor = false;
            this._nextSerchBt.MouseUp += new System.Windows.Forms.MouseEventHandler(this._nextSerchBt_MouseUp);
            // 
            // _serchBt
            // 
            this._serchBt.EditBox = null;
            this._serchBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._serchBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._serchBt.IsActive = false;
            this._serchBt.LedSyncedBackColorEnable = true;
            this._serchBt.Location = new System.Drawing.Point(345, 47);
            this._serchBt.MultiSelectEn = false;
            this._serchBt.Name = "_serchBt";
            this._serchBt.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._serchBt.OutLineEn = true;
            this._serchBt.OutLineSize = 3F;
            this._serchBt.ProgressBarColor = System.Drawing.Color.Empty;
            this._serchBt.ProgressBarEnable = false;
            this._serchBt.ProgressBarMaxValue = 100;
            this._serchBt.ProgressBarMinValue = 0;
            this._serchBt.ProgressBarSize = 5;
            this._serchBt.ProgressBarValue = 0;
            this._serchBt.Selectable = false;
            this._serchBt.Selected = false;
            this._serchBt.Size = new System.Drawing.Size(107, 38);
            this._serchBt.StatusLedEnable = false;
            this._serchBt.StatusLedSize = ((byte)(15));
            this._serchBt.TabIndex = 174;
            this._serchBt.TabStop = false;
            this._serchBt.Text = "検索";
            this._serchBt.UseVisualStyleBackColor = false;
            this._serchBt.MouseUp += new System.Windows.Forms.MouseEventHandler(this._serchBt_MouseUp);
            // 
            // _newBt
            // 
            this._newBt.EditBox = null;
            this._newBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._newBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._newBt.IsActive = false;
            this._newBt.LedSyncedBackColorEnable = true;
            this._newBt.Location = new System.Drawing.Point(119, 47);
            this._newBt.MultiSelectEn = false;
            this._newBt.Name = "_newBt";
            this._newBt.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._newBt.OutLineEn = true;
            this._newBt.OutLineSize = 3F;
            this._newBt.ProgressBarColor = System.Drawing.Color.Empty;
            this._newBt.ProgressBarEnable = false;
            this._newBt.ProgressBarMaxValue = 100;
            this._newBt.ProgressBarMinValue = 0;
            this._newBt.ProgressBarSize = 5;
            this._newBt.ProgressBarValue = 0;
            this._newBt.Selectable = false;
            this._newBt.Selected = false;
            this._newBt.Size = new System.Drawing.Size(107, 38);
            this._newBt.StatusLedEnable = false;
            this._newBt.StatusLedSize = ((byte)(15));
            this._newBt.TabIndex = 167;
            this._newBt.TabStop = false;
            this._newBt.Text = "新規作成";
            this._newBt.UseVisualStyleBackColor = false;
            this._newBt.Click += new System.EventHandler(this._newBt_Click);
            // 
            // _fileExpBt
            // 
            this._fileExpBt.EditBox = null;
            this._fileExpBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._fileExpBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._fileExpBt.IsActive = false;
            this._fileExpBt.LedSyncedBackColorEnable = true;
            this._fileExpBt.Location = new System.Drawing.Point(6, 6);
            this._fileExpBt.MultiSelectEn = false;
            this._fileExpBt.Name = "_fileExpBt";
            this._fileExpBt.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._fileExpBt.OutLineEn = true;
            this._fileExpBt.OutLineSize = 3F;
            this._fileExpBt.ProgressBarColor = System.Drawing.Color.Empty;
            this._fileExpBt.ProgressBarEnable = false;
            this._fileExpBt.ProgressBarMaxValue = 100;
            this._fileExpBt.ProgressBarMinValue = 0;
            this._fileExpBt.ProgressBarSize = 5;
            this._fileExpBt.ProgressBarValue = 0;
            this._fileExpBt.Selectable = false;
            this._fileExpBt.Selected = false;
            this._fileExpBt.Size = new System.Drawing.Size(107, 38);
            this._fileExpBt.StatusLedEnable = false;
            this._fileExpBt.StatusLedSize = ((byte)(15));
            this._fileExpBt.TabIndex = 166;
            this._fileExpBt.TabStop = false;
            this._fileExpBt.Text = "ファイル";
            this._fileExpBt.UseVisualStyleBackColor = false;
            this._fileExpBt.Click += new System.EventHandler(this._fileExpBt_Click);
            // 
            // _saveBt
            // 
            this._saveBt.EditBox = null;
            this._saveBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._saveBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._saveBt.IsActive = false;
            this._saveBt.LedSyncedBackColorEnable = true;
            this._saveBt.Location = new System.Drawing.Point(6, 47);
            this._saveBt.MultiSelectEn = false;
            this._saveBt.Name = "_saveBt";
            this._saveBt.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._saveBt.OutLineEn = true;
            this._saveBt.OutLineSize = 3F;
            this._saveBt.ProgressBarColor = System.Drawing.Color.Empty;
            this._saveBt.ProgressBarEnable = false;
            this._saveBt.ProgressBarMaxValue = 100;
            this._saveBt.ProgressBarMinValue = 0;
            this._saveBt.ProgressBarSize = 5;
            this._saveBt.ProgressBarValue = 0;
            this._saveBt.Selectable = false;
            this._saveBt.Selected = false;
            this._saveBt.Size = new System.Drawing.Size(107, 38);
            this._saveBt.StatusLedEnable = false;
            this._saveBt.StatusLedSize = ((byte)(15));
            this._saveBt.TabIndex = 146;
            this._saveBt.TabStop = false;
            this._saveBt.Text = "保存";
            this._saveBt.UseVisualStyleBackColor = false;
            this._saveBt.Click += new System.EventHandler(this._saveBt_Click);
            // 
            // _FileNameTextBox
            // 
            this._FileNameTextBox.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._FileNameTextBox.Location = new System.Drawing.Point(119, 9);
            this._FileNameTextBox.Name = "_FileNameTextBox";
            this._FileNameTextBox.ReadOnly = true;
            this._FileNameTextBox.Size = new System.Drawing.Size(529, 32);
            this._FileNameTextBox.TabIndex = 156;
            this._FileNameTextBox.TextChanged += new System.EventHandler(this._FileNameTextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(6, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 32);
            this.label2.TabIndex = 161;
            this.label2.Text = "ファイル名";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _programStatusLabel
            // 
            this._programStatusLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._programStatusLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._programStatusLabel.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._programStatusLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._programStatusLabel.Location = new System.Drawing.Point(12, 104);
            this._programStatusLabel.Name = "_programStatusLabel";
            this._programStatusLabel.Size = new System.Drawing.Size(999, 24);
            this._programStatusLabel.TabIndex = 173;
            // 
            // EDITForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 678);
            this.Controls.Add(this._programStatusLabel);
            this.Controls.Add(this.ProgramEditBox);
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(0, 90);
            this.Name = "EDITForm";
            this.OutLineSize = 3;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "EDITForm";
            this.Load += new System.EventHandler(this.EDITForm_Load);
            this.Shown += new System.EventHandler(this.EDITForm_Shown);
            this.VisibleChanged += new System.EventHandler(this.EDITForm_VisibleChanged);
            this.panelEx1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ButtonEx HelpBt;
        private ButtonEx _TeachBtn;
        private ButtonEx TeachingSetBt;
        private ButtonEx _saveBt;
        private LabelEx label2;
        private ButtonEx _fileExpBt;
        private PanelEx panel1;
        private ComboBoxEx _SelectPositionsComboBox;
        private ComboBoxEx _SelectVariablesComboBox;
        private PanelEx panelEx1;
        private RichTextBoxEx ProgramEditBox;
        private TextBoxEx _FileNameTextBox;
        private LabelEx _programStatusLabel;
        private ButtonEx _keybordBt;
        private ButtonEx _newBt;
        private ButtonEx _serchBt;
        private ButtonEx _nextSerchBt;
        private ButtonEx _returnSerchBt;
    }
}