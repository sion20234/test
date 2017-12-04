namespace ECNC3.Views
{
    partial class NumericFeedForm
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
            this.EditText = new System.Windows.Forms.RichTextBox();
            this._IncrimentModeBt = new ECNC3.Views.ButtonEx();
            this._MachinePositionBt = new ECNC3.Views.ButtonEx();
            this._AxisBtPanel = new ECNC3.Views.PanelEx();
            this.SignLabel = new ECNC3.Views.LabelEx();
            this._TitlePanel = new ECNC3.Views.LabelEx();
            this.label1 = new ECNC3.Views.LabelEx();
            this.EditUnit = new ECNC3.Views.LabelEx();
            this._AllCrearBt = new ECNC3.Views.ButtonEx();
            this._CrearBt = new ECNC3.Views.ButtonEx();
            this._SubTitlePanel = new ECNC3.Views.LabelEx();
            this._AxisSelectButtons = new ECNC3.Views.DataGridViewEx();
            this._WorkPositionBt = new ECNC3.Views.ButtonEx();
            this._AbsoluteModeBt = new ECNC3.Views.ButtonEx();
            this._CloseBtn = new ECNC3.Views.ButtonEx();
            this._ClosePanel = new ECNC3.Views.PanelEx();
            this._enterBtnPanel = new ECNC3.Views.PanelEx();
            this._enterBtn = new ECNC3.Views.ButtonEx();
            ((System.ComponentModel.ISupportInitialize)(this._AxisSelectButtons)).BeginInit();
            this._ClosePanel.SuspendLayout();
            this._enterBtnPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // EditText
            // 
            this.EditText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EditText.Font = new System.Drawing.Font("Meiryo UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.EditText.Location = new System.Drawing.Point(6, 107);
            this.EditText.MaxLength = 7;
            this.EditText.Multiline = false;
            this.EditText.Name = "EditText";
            this.EditText.ReadOnly = true;
            this.EditText.RightMargin = 10;
            this.EditText.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.EditText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.EditText.Size = new System.Drawing.Size(205, 48);
            this.EditText.TabIndex = 364;
            this.EditText.Text = "0.0000";
            this.EditText.Click += new System.EventHandler(this.EditText_Click);
            // 
            // _IncrimentModeBt
            // 
            this._IncrimentModeBt.EditBox = null;
            this._IncrimentModeBt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this._IncrimentModeBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._IncrimentModeBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._IncrimentModeBt.IsActive = false;
            this._IncrimentModeBt.LedSyncedBackColorEnable = true;
            this._IncrimentModeBt.Location = new System.Drawing.Point(6, 161);
            this._IncrimentModeBt.MultiSelectEn = false;
            this._IncrimentModeBt.Name = "_IncrimentModeBt";
            this._IncrimentModeBt.OutLineColor = System.Drawing.Color.Empty;
            this._IncrimentModeBt.OutLineEn = true;
            this._IncrimentModeBt.OutLineSize = 3;
            this._IncrimentModeBt.ProgressBarColor = System.Drawing.Color.Empty;
            this._IncrimentModeBt.ProgressBarEnable = false;
            this._IncrimentModeBt.ProgressBarMaxValue = 100;
            this._IncrimentModeBt.ProgressBarMinValue = 0;
            this._IncrimentModeBt.ProgressBarSize = 5;
            this._IncrimentModeBt.ProgressBarValue = 0;
            this._IncrimentModeBt.Selectable = false;
            this._IncrimentModeBt.Selected = false;
            this._IncrimentModeBt.Size = new System.Drawing.Size(122, 50);
            this._IncrimentModeBt.StatusLedEnable = false;
            this._IncrimentModeBt.StatusLedSize = ((byte)(10));
            this._IncrimentModeBt.TabIndex = 66;
            this._IncrimentModeBt.TabStop = false;
            this._IncrimentModeBt.Text = "INC";
            this._IncrimentModeBt.UseVisualStyleBackColor = false;
            this._IncrimentModeBt.MouseUp += new System.Windows.Forms.MouseEventHandler(this._IncrimentModeBt_MouseUp);
            // 
            // _MachinePositionBt
            // 
            this._MachinePositionBt.EditBox = null;
            this._MachinePositionBt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this._MachinePositionBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._MachinePositionBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._MachinePositionBt.IsActive = false;
            this._MachinePositionBt.LedSyncedBackColorEnable = true;
            this._MachinePositionBt.Location = new System.Drawing.Point(6, 226);
            this._MachinePositionBt.MultiSelectEn = false;
            this._MachinePositionBt.Name = "_MachinePositionBt";
            this._MachinePositionBt.OutLineColor = System.Drawing.Color.Empty;
            this._MachinePositionBt.OutLineEn = true;
            this._MachinePositionBt.OutLineSize = 3;
            this._MachinePositionBt.ProgressBarColor = System.Drawing.Color.Empty;
            this._MachinePositionBt.ProgressBarEnable = false;
            this._MachinePositionBt.ProgressBarMaxValue = 100;
            this._MachinePositionBt.ProgressBarMinValue = 0;
            this._MachinePositionBt.ProgressBarSize = 5;
            this._MachinePositionBt.ProgressBarValue = 0;
            this._MachinePositionBt.Selectable = false;
            this._MachinePositionBt.Selected = false;
            this._MachinePositionBt.Size = new System.Drawing.Size(122, 50);
            this._MachinePositionBt.StatusLedEnable = false;
            this._MachinePositionBt.StatusLedSize = ((byte)(10));
            this._MachinePositionBt.TabIndex = 67;
            this._MachinePositionBt.TabStop = false;
            this._MachinePositionBt.Text = "MACHINE";
            this._MachinePositionBt.UseVisualStyleBackColor = false;
            this._MachinePositionBt.MouseUp += new System.Windows.Forms.MouseEventHandler(this._MachinePositionBt_MouseUp);
            // 
            // _AxisBtPanel
            // 
            this._AxisBtPanel.Location = new System.Drawing.Point(6, 33);
            this._AxisBtPanel.Name = "_AxisBtPanel";
            this._AxisBtPanel.OutLineColor = System.Drawing.Color.DarkOliveGreen;
            this._AxisBtPanel.OutLineSize = 2;
            this._AxisBtPanel.Size = new System.Drawing.Size(582, 68);
            this._AxisBtPanel.TabIndex = 370;
            // 
            // SignLabel
            // 
            this.SignLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SignLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SignLabel.Font = new System.Drawing.Font("Meiryo UI", 24F);
            this.SignLabel.Location = new System.Drawing.Point(137, 655);
            this.SignLabel.Name = "SignLabel";
            this.SignLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.SignLabel.Size = new System.Drawing.Size(23, 40);
            this.SignLabel.TabIndex = 281;
            this.SignLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _TitlePanel
            // 
            this._TitlePanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._TitlePanel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._TitlePanel.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._TitlePanel.Location = new System.Drawing.Point(8, 9);
            this._TitlePanel.Name = "_TitlePanel";
            this._TitlePanel.Size = new System.Drawing.Size(580, 21);
            this._TitlePanel.TabIndex = 113;
            this._TitlePanel.Text = "NUMERIC FEED";
            this._TitlePanel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(0, 570);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(593, 30);
            this.label1.TabIndex = 112;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // EditUnit
            // 
            this.EditUnit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EditUnit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EditUnit.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.EditUnit.Location = new System.Drawing.Point(213, 107);
            this.EditUnit.Name = "EditUnit";
            this.EditUnit.Size = new System.Drawing.Size(60, 48);
            this.EditUnit.TabIndex = 271;
            this.EditUnit.Text = "mm";
            this.EditUnit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _AllCrearBt
            // 
            this._AllCrearBt.EditBox = null;
            this._AllCrearBt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this._AllCrearBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._AllCrearBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._AllCrearBt.IsActive = false;
            this._AllCrearBt.LedSyncedBackColorEnable = true;
            this._AllCrearBt.Location = new System.Drawing.Point(6, 161);
            this._AllCrearBt.MultiSelectEn = false;
            this._AllCrearBt.Name = "_AllCrearBt";
            this._AllCrearBt.OutLineColor = System.Drawing.Color.Empty;
            this._AllCrearBt.OutLineEn = true;
            this._AllCrearBt.OutLineSize = 3;
            this._AllCrearBt.ProgressBarColor = System.Drawing.Color.Empty;
            this._AllCrearBt.ProgressBarEnable = false;
            this._AllCrearBt.ProgressBarMaxValue = 100;
            this._AllCrearBt.ProgressBarMinValue = 0;
            this._AllCrearBt.ProgressBarSize = 5;
            this._AllCrearBt.ProgressBarValue = 0;
            this._AllCrearBt.Selectable = false;
            this._AllCrearBt.Selected = false;
            this._AllCrearBt.Size = new System.Drawing.Size(122, 50);
            this._AllCrearBt.StatusLedEnable = false;
            this._AllCrearBt.StatusLedSize = ((byte)(10));
            this._AllCrearBt.TabIndex = 371;
            this._AllCrearBt.TabStop = false;
            this._AllCrearBt.Text = "全クリア";
            this._AllCrearBt.UseVisualStyleBackColor = false;
            this._AllCrearBt.MouseUp += new System.Windows.Forms.MouseEventHandler(this._AllCrearBt_MouseUp);
            // 
            // _CrearBt
            // 
            this._CrearBt.EditBox = null;
            this._CrearBt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this._CrearBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._CrearBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._CrearBt.IsActive = false;
            this._CrearBt.LedSyncedBackColorEnable = true;
            this._CrearBt.Location = new System.Drawing.Point(151, 161);
            this._CrearBt.MultiSelectEn = false;
            this._CrearBt.Name = "_CrearBt";
            this._CrearBt.OutLineColor = System.Drawing.Color.Empty;
            this._CrearBt.OutLineEn = true;
            this._CrearBt.OutLineSize = 3;
            this._CrearBt.ProgressBarColor = System.Drawing.Color.Empty;
            this._CrearBt.ProgressBarEnable = false;
            this._CrearBt.ProgressBarMaxValue = 100;
            this._CrearBt.ProgressBarMinValue = 0;
            this._CrearBt.ProgressBarSize = 5;
            this._CrearBt.ProgressBarValue = 0;
            this._CrearBt.Selectable = false;
            this._CrearBt.Selected = false;
            this._CrearBt.Size = new System.Drawing.Size(122, 50);
            this._CrearBt.StatusLedEnable = false;
            this._CrearBt.StatusLedSize = ((byte)(10));
            this._CrearBt.TabIndex = 372;
            this._CrearBt.TabStop = false;
            this._CrearBt.Text = "クリア";
            this._CrearBt.UseVisualStyleBackColor = false;
            this._CrearBt.MouseUp += new System.Windows.Forms.MouseEventHandler(this._CrearBt_MouseUp);
            // 
            // _SubTitlePanel
            // 
            this._SubTitlePanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._SubTitlePanel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._SubTitlePanel.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._SubTitlePanel.Location = new System.Drawing.Point(8, 9);
            this._SubTitlePanel.Name = "_SubTitlePanel";
            this._SubTitlePanel.Size = new System.Drawing.Size(580, 21);
            this._SubTitlePanel.TabIndex = 373;
            this._SubTitlePanel.Text = "WORK SETTING";
            this._SubTitlePanel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _AxisSelectButtons
            // 
            this._AxisSelectButtons.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._AxisSelectButtons.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._AxisSelectButtons.ColumnHeadersVisible = false;
            this._AxisSelectButtons.EnableLastIndex = false;
            this._AxisSelectButtons.Location = new System.Drawing.Point(9, 36);
            this._AxisSelectButtons.Name = "_AxisSelectButtons";
            this._AxisSelectButtons.RowHeadersVisible = false;
            this._AxisSelectButtons.RowTemplate.Height = 21;
            this._AxisSelectButtons.Size = new System.Drawing.Size(576, 62);
            this._AxisSelectButtons.TabIndex = 0;
            this._AxisSelectButtons.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this._AxisSelectButtons_CellMouseUp);
            this._AxisSelectButtons.SelectionChanged += new System.EventHandler(this._AxisSelectButtons_SelectionChanged);
            // 
            // _WorkPositionBt
            // 
            this._WorkPositionBt.EditBox = null;
            this._WorkPositionBt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this._WorkPositionBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._WorkPositionBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._WorkPositionBt.IsActive = false;
            this._WorkPositionBt.LedSyncedBackColorEnable = true;
            this._WorkPositionBt.Location = new System.Drawing.Point(151, 226);
            this._WorkPositionBt.MultiSelectEn = false;
            this._WorkPositionBt.Name = "_WorkPositionBt";
            this._WorkPositionBt.OutLineColor = System.Drawing.Color.Empty;
            this._WorkPositionBt.OutLineEn = true;
            this._WorkPositionBt.OutLineSize = 3;
            this._WorkPositionBt.ProgressBarColor = System.Drawing.Color.Empty;
            this._WorkPositionBt.ProgressBarEnable = false;
            this._WorkPositionBt.ProgressBarMaxValue = 100;
            this._WorkPositionBt.ProgressBarMinValue = 0;
            this._WorkPositionBt.ProgressBarSize = 5;
            this._WorkPositionBt.ProgressBarValue = 0;
            this._WorkPositionBt.Selectable = false;
            this._WorkPositionBt.Selected = false;
            this._WorkPositionBt.Size = new System.Drawing.Size(122, 50);
            this._WorkPositionBt.StatusLedEnable = false;
            this._WorkPositionBt.StatusLedSize = ((byte)(10));
            this._WorkPositionBt.TabIndex = 374;
            this._WorkPositionBt.TabStop = false;
            this._WorkPositionBt.UseVisualStyleBackColor = false;
            this._WorkPositionBt.MouseUp += new System.Windows.Forms.MouseEventHandler(this._WorkPositionBt_MouseUp);
            // 
            // _AbsoluteModeBt
            // 
            this._AbsoluteModeBt.EditBox = null;
            this._AbsoluteModeBt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this._AbsoluteModeBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._AbsoluteModeBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._AbsoluteModeBt.IsActive = false;
            this._AbsoluteModeBt.LedSyncedBackColorEnable = true;
            this._AbsoluteModeBt.Location = new System.Drawing.Point(151, 161);
            this._AbsoluteModeBt.MultiSelectEn = false;
            this._AbsoluteModeBt.Name = "_AbsoluteModeBt";
            this._AbsoluteModeBt.OutLineColor = System.Drawing.Color.Empty;
            this._AbsoluteModeBt.OutLineEn = true;
            this._AbsoluteModeBt.OutLineSize = 3;
            this._AbsoluteModeBt.ProgressBarColor = System.Drawing.Color.Empty;
            this._AbsoluteModeBt.ProgressBarEnable = false;
            this._AbsoluteModeBt.ProgressBarMaxValue = 100;
            this._AbsoluteModeBt.ProgressBarMinValue = 0;
            this._AbsoluteModeBt.ProgressBarSize = 5;
            this._AbsoluteModeBt.ProgressBarValue = 0;
            this._AbsoluteModeBt.Selectable = false;
            this._AbsoluteModeBt.Selected = false;
            this._AbsoluteModeBt.Size = new System.Drawing.Size(122, 50);
            this._AbsoluteModeBt.StatusLedEnable = false;
            this._AbsoluteModeBt.StatusLedSize = ((byte)(10));
            this._AbsoluteModeBt.TabIndex = 375;
            this._AbsoluteModeBt.TabStop = false;
            this._AbsoluteModeBt.Text = "ABS";
            this._AbsoluteModeBt.UseVisualStyleBackColor = false;
            this._AbsoluteModeBt.MouseUp += new System.Windows.Forms.MouseEventHandler(this._AbsoluteModeBt_MouseUp);
            // 
            // _CloseBtn
            // 
            this._CloseBtn.EditBox = null;
            this._CloseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._CloseBtn.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._CloseBtn.IsActive = false;
            this._CloseBtn.LedSyncedBackColorEnable = true;
            this._CloseBtn.Location = new System.Drawing.Point(6, 6);
            this._CloseBtn.MultiSelectEn = false;
            this._CloseBtn.Name = "_CloseBtn";
            this._CloseBtn.OutLineColor = System.Drawing.Color.Empty;
            this._CloseBtn.OutLineEn = true;
            this._CloseBtn.OutLineSize = 3;
            this._CloseBtn.ProgressBarColor = System.Drawing.Color.Empty;
            this._CloseBtn.ProgressBarEnable = false;
            this._CloseBtn.ProgressBarMaxValue = 100;
            this._CloseBtn.ProgressBarMinValue = 0;
            this._CloseBtn.ProgressBarSize = 5;
            this._CloseBtn.ProgressBarValue = 0;
            this._CloseBtn.Selectable = true;
            this._CloseBtn.Selected = false;
            this._CloseBtn.Size = new System.Drawing.Size(133, 50);
            this._CloseBtn.StatusLedEnable = false;
            this._CloseBtn.StatusLedSize = ((byte)(15));
            this._CloseBtn.TabIndex = 15;
            this._CloseBtn.Text = "閉じる";
            this._CloseBtn.UseVisualStyleBackColor = false;
            this._CloseBtn.Click += new System.EventHandler(this._CloseBtn_Click);
            // 
            // _ClosePanel
            // 
            this._ClosePanel.Controls.Add(this._CloseBtn);
            this._ClosePanel.Location = new System.Drawing.Point(437, 457);
            this._ClosePanel.Name = "_ClosePanel";
            this._ClosePanel.OutLineColor = System.Drawing.Color.DarkOliveGreen;
            this._ClosePanel.OutLineSize = 2;
            this._ClosePanel.Size = new System.Drawing.Size(145, 62);
            this._ClosePanel.TabIndex = 376;
            // 
            // _enterBtnPanel
            // 
            this._enterBtnPanel.Controls.Add(this._enterBtn);
            this._enterBtnPanel.Location = new System.Drawing.Point(6, 463);
            this._enterBtnPanel.Name = "_enterBtnPanel";
            this._enterBtnPanel.OutLineColor = System.Drawing.Color.DarkOliveGreen;
            this._enterBtnPanel.OutLineSize = 2;
            this._enterBtnPanel.Size = new System.Drawing.Size(145, 62);
            this._enterBtnPanel.TabIndex = 377;
            this._enterBtnPanel.Visible = false;
            // 
            // _enterBtn
            // 
            this._enterBtn.EditBox = null;
            this._enterBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._enterBtn.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._enterBtn.IsActive = false;
            this._enterBtn.LedSyncedBackColorEnable = true;
            this._enterBtn.Location = new System.Drawing.Point(6, 6);
            this._enterBtn.MultiSelectEn = false;
            this._enterBtn.Name = "_enterBtn";
            this._enterBtn.OutLineColor = System.Drawing.Color.Empty;
            this._enterBtn.OutLineEn = true;
            this._enterBtn.OutLineSize = 3;
            this._enterBtn.ProgressBarColor = System.Drawing.Color.Empty;
            this._enterBtn.ProgressBarEnable = false;
            this._enterBtn.ProgressBarMaxValue = 100;
            this._enterBtn.ProgressBarMinValue = 0;
            this._enterBtn.ProgressBarSize = 5;
            this._enterBtn.ProgressBarValue = 0;
            this._enterBtn.Selectable = true;
            this._enterBtn.Selected = false;
            this._enterBtn.Size = new System.Drawing.Size(133, 50);
            this._enterBtn.StatusLedEnable = false;
            this._enterBtn.StatusLedSize = ((byte)(15));
            this._enterBtn.TabIndex = 15;
            this._enterBtn.Text = "実行";
            this._enterBtn.UseVisualStyleBackColor = false;
            this._enterBtn.Click += new System.EventHandler(this._enterBtn_Click);
            // 
            // NumericFeedForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 531);
            this.Controls.Add(this._enterBtnPanel);
            this.Controls.Add(this._ClosePanel);
            this.Controls.Add(this._WorkPositionBt);
            this.Controls.Add(this._AxisSelectButtons);
            this.Controls.Add(this._SubTitlePanel);
            this.Controls.Add(this._AllCrearBt);
            this.Controls.Add(this._CrearBt);
            this.Controls.Add(this._IncrimentModeBt);
            this.Controls.Add(this._MachinePositionBt);
            this.Controls.Add(this._AxisBtPanel);
            this.Controls.Add(this.SignLabel);
            this.Controls.Add(this._TitlePanel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.EditUnit);
            this.Controls.Add(this.EditText);
            this.Controls.Add(this._AbsoluteModeBt);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(430, 90);
            this.Name = "NumericFeedForm";
            this.OutLineSize = 3;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "NumericFeedForm";
            this.Load += new System.EventHandler(this.NumericFeedForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this._AxisSelectButtons)).EndInit();
            this._ClosePanel.ResumeLayout(false);
            this._enterBtnPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private LabelEx label1;
        private LabelEx _TitlePanel;
        private LabelEx EditUnit;
        private LabelEx SignLabel;
        public System.Windows.Forms.RichTextBox EditText;
        private PanelEx _AxisBtPanel;
        private ButtonEx _IncrimentModeBt;
        private ButtonEx _MachinePositionBt;
        private ButtonEx _AllCrearBt;
        private ButtonEx _CrearBt;
        private LabelEx _SubTitlePanel;
        private DataGridViewEx _AxisSelectButtons;
        private ButtonEx _WorkPositionBt;
        private ButtonEx _AbsoluteModeBt;
        private ButtonEx _CloseBtn;
        private PanelEx _ClosePanel;
        private PanelEx _enterBtnPanel;
        private ButtonEx _enterBtn;
    }
}