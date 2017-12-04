namespace ECNC3.Views
{
    partial class MDIAUTOForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MDIAUTOForm));
            this.ProgramTextList = new ECNC3.Views.ListBoxEx();
            this.panel6 = new ECNC3.Views.PanelEx();
            this.M02Label = new ECNC3.Views.LabelEx();
            this.SignalLabel = new ECNC3.Views.LabelEx();
            this.MachineLockLabel = new ECNC3.Views.LabelEx();
            this.CorrectAngleLabel = new ECNC3.Views.LabelEx();
            this.PartitionRoundStopLabel = new ECNC3.Views.LabelEx();
            this.StartNoLabel = new ECNC3.Views.LabelEx();
            this.IncRefMoveLabel = new ECNC3.Views.LabelEx();
            this.BlockSkipLabel = new ECNC3.Views.LabelEx();
            this.OptionalStopLabel = new ECNC3.Views.LabelEx();
            this.BuzzerLabel = new ECNC3.Views.LabelEx();
            this.AecByLifeLabel = new ECNC3.Views.LabelEx();
            this._timerPanelEx = new ECNC3.Views.PanelEx();
            this.ProcTimerTextBox = new ECNC3.Views.LabelEx();
            this._oneProcessingTimeBt = new ECNC3.Views.ButtonEx();
            this.DischargeTimerTextBox = new ECNC3.Views.LabelEx();
            this.MachineTimerTextBox = new ECNC3.Views.LabelEx();
            this._processingTimeBt = new ECNC3.Views.ButtonEx();
            this._programRunTimeBt = new ECNC3.Views.ButtonEx();
            this.pictureBoxEx2 = new ECNC3.Views.PictureBoxEx();
            this.LineCodeLabel = new ECNC3.Views.LabelEx();
            this._waxisSettingPanelEx = new ECNC3.Views.PanelEx();
            this.WAxisLowerVal = new ECNC3.Views.LabelEx();
            this.WAxisUpperVal = new ECNC3.Views.LabelEx();
            this.WAxisLowerSetBt = new ECNC3.Views.ButtonEx();
            this.WAxisUpperSetBt = new ECNC3.Views.ButtonEx();
            this.WAxisUpperUnitLabel = new ECNC3.Views.LabelEx();
            this.WAxisLowerUnitLabel = new ECNC3.Views.LabelEx();
            this.pictureBoxEx1 = new ECNC3.Views.PictureBoxEx();
            this.plot1 = new ECNC3.Views.LogForm();
            this.levelGage1 = new ECNC3.Views.UserControls.LevelGage();
            this.panel2 = new ECNC3.Views.PanelEx();
            this._btnReset = new ECNC3.Views.ButtonEx();
            this.panelEx1 = new ECNC3.Views.PanelEx();
            this._HelpBtn = new ECNC3.Views.ButtonEx();
            this.DryRunBt = new ECNC3.Views.ButtonEx();
            this.SingleBlockBt = new ECNC3.Views.ButtonEx();
            this.FileKeybordFormBt = new ECNC3.Views.ButtonEx();
            this.OptionBt = new ECNC3.Views.ButtonEx();
            this.axisMonitor1 = new ECNC3.Views.AxisMonitor();
            this.ConditionsFormOpenBt = new ECNC3.Views.ButtonEx();
            this.PnumTextBox = new ECNC3.Views.LabelEx();
            this.PTextBox = new ECNC3.Views.LabelEx();
            this.ProgramStatusLabel = new ECNC3.Views.LabelEx();
            this.ProgramEditBox = new ECNC3.Views.RichTextBoxEx();
            this._GraphPanel = new ECNC3.Views.PanelEx();
            this.editProcessCondition1 = new ECNC3.Views.EditProcessCondition();
            this.panel6.SuspendLayout();
            this._timerPanelEx.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEx2)).BeginInit();
            this._waxisSettingPanelEx.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEx1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this._GraphPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ProgramTextList
            // 
            this.ProgramTextList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ProgramTextList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ProgramTextList.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.ProgramTextList.FormattingEnabled = true;
            this.ProgramTextList.ItemHeight = 15;
            this.ProgramTextList.Location = new System.Drawing.Point(424, 319);
            this.ProgramTextList.Name = "ProgramTextList";
            this.ProgramTextList.Size = new System.Drawing.Size(595, 212);
            this.ProgramTextList.TabIndex = 135;
            this.ProgramTextList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ProgramTextList_DrawItem);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.M02Label);
            this.panel6.Controls.Add(this.SignalLabel);
            this.panel6.Controls.Add(this.MachineLockLabel);
            this.panel6.Controls.Add(this.CorrectAngleLabel);
            this.panel6.Controls.Add(this.PartitionRoundStopLabel);
            this.panel6.Controls.Add(this.StartNoLabel);
            this.panel6.Controls.Add(this.IncRefMoveLabel);
            this.panel6.Controls.Add(this.BlockSkipLabel);
            this.panel6.Controls.Add(this.OptionalStopLabel);
            this.panel6.Controls.Add(this.BuzzerLabel);
            this.panel6.Controls.Add(this.AecByLifeLabel);
            this.panel6.Location = new System.Drawing.Point(672, 0);
            this.panel6.Name = "panel6";
            this.panel6.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panel6.OutLineSize = 0F;
            this.panel6.Size = new System.Drawing.Size(348, 105);
            this.panel6.TabIndex = 347;
            // 
            // M02Label
            // 
            this.M02Label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.M02Label.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.M02Label.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.M02Label.Location = new System.Drawing.Point(230, 52);
            this.M02Label.Name = "M02Label";
            this.M02Label.Size = new System.Drawing.Size(110, 23);
            this.M02Label.TabIndex = 358;
            this.M02Label.Text = "自動電源断";
            this.M02Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SignalLabel
            // 
            this.SignalLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SignalLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SignalLabel.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.SignalLabel.Location = new System.Drawing.Point(230, 28);
            this.SignalLabel.Name = "SignalLabel";
            this.SignalLabel.Size = new System.Drawing.Size(110, 23);
            this.SignalLabel.TabIndex = 357;
            this.SignalLabel.Text = "出力信号";
            this.SignalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MachineLockLabel
            // 
            this.MachineLockLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MachineLockLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MachineLockLabel.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.MachineLockLabel.Location = new System.Drawing.Point(230, 4);
            this.MachineLockLabel.Name = "MachineLockLabel";
            this.MachineLockLabel.Size = new System.Drawing.Size(110, 23);
            this.MachineLockLabel.TabIndex = 356;
            this.MachineLockLabel.Text = "マシンロック";
            this.MachineLockLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CorrectAngleLabel
            // 
            this.CorrectAngleLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CorrectAngleLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CorrectAngleLabel.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.CorrectAngleLabel.Location = new System.Drawing.Point(118, 76);
            this.CorrectAngleLabel.Name = "CorrectAngleLabel";
            this.CorrectAngleLabel.Size = new System.Drawing.Size(110, 23);
            this.CorrectAngleLabel.TabIndex = 355;
            this.CorrectAngleLabel.Text = "角度補正";
            this.CorrectAngleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PartitionRoundStopLabel
            // 
            this.PartitionRoundStopLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PartitionRoundStopLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PartitionRoundStopLabel.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.PartitionRoundStopLabel.Location = new System.Drawing.Point(6, 28);
            this.PartitionRoundStopLabel.Name = "PartitionRoundStopLabel";
            this.PartitionRoundStopLabel.Size = new System.Drawing.Size(110, 23);
            this.PartitionRoundStopLabel.TabIndex = 354;
            this.PartitionRoundStopLabel.Text = "AEC1週停止";
            this.PartitionRoundStopLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // StartNoLabel
            // 
            this.StartNoLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StartNoLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StartNoLabel.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.StartNoLabel.Location = new System.Drawing.Point(118, 52);
            this.StartNoLabel.Name = "StartNoLabel";
            this.StartNoLabel.Size = new System.Drawing.Size(110, 23);
            this.StartNoLabel.TabIndex = 353;
            this.StartNoLabel.Text = "スタート番号";
            this.StartNoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // IncRefMoveLabel
            // 
            this.IncRefMoveLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.IncRefMoveLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IncRefMoveLabel.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.IncRefMoveLabel.Location = new System.Drawing.Point(118, 28);
            this.IncRefMoveLabel.Name = "IncRefMoveLabel";
            this.IncRefMoveLabel.Size = new System.Drawing.Size(110, 23);
            this.IncRefMoveLabel.TabIndex = 352;
            this.IncRefMoveLabel.Text = "G07移動";
            this.IncRefMoveLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BlockSkipLabel
            // 
            this.BlockSkipLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BlockSkipLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BlockSkipLabel.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.BlockSkipLabel.Location = new System.Drawing.Point(118, 4);
            this.BlockSkipLabel.Name = "BlockSkipLabel";
            this.BlockSkipLabel.Size = new System.Drawing.Size(110, 23);
            this.BlockSkipLabel.TabIndex = 351;
            this.BlockSkipLabel.Text = "B.Skip";
            this.BlockSkipLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OptionalStopLabel
            // 
            this.OptionalStopLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OptionalStopLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OptionalStopLabel.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.OptionalStopLabel.Location = new System.Drawing.Point(6, 76);
            this.OptionalStopLabel.Name = "OptionalStopLabel";
            this.OptionalStopLabel.Size = new System.Drawing.Size(110, 23);
            this.OptionalStopLabel.TabIndex = 350;
            this.OptionalStopLabel.Text = "O.Stop";
            this.OptionalStopLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BuzzerLabel
            // 
            this.BuzzerLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BuzzerLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BuzzerLabel.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.BuzzerLabel.Location = new System.Drawing.Point(6, 52);
            this.BuzzerLabel.Name = "BuzzerLabel";
            this.BuzzerLabel.Size = new System.Drawing.Size(110, 23);
            this.BuzzerLabel.TabIndex = 348;
            this.BuzzerLabel.Text = "ブザー";
            this.BuzzerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AecByLifeLabel
            // 
            this.AecByLifeLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AecByLifeLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AecByLifeLabel.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.AecByLifeLabel.Location = new System.Drawing.Point(6, 4);
            this.AecByLifeLabel.Name = "AecByLifeLabel";
            this.AecByLifeLabel.Size = new System.Drawing.Size(110, 23);
            this.AecByLifeLabel.TabIndex = 347;
            this.AecByLifeLabel.Text = "電極交換";
            this.AecByLifeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _timerPanelEx
            // 
            this._timerPanelEx.Controls.Add(this.ProcTimerTextBox);
            this._timerPanelEx.Controls.Add(this._oneProcessingTimeBt);
            this._timerPanelEx.Controls.Add(this.DischargeTimerTextBox);
            this._timerPanelEx.Controls.Add(this.MachineTimerTextBox);
            this._timerPanelEx.Controls.Add(this._processingTimeBt);
            this._timerPanelEx.Controls.Add(this._programRunTimeBt);
            this._timerPanelEx.Controls.Add(this.pictureBoxEx2);
            this._timerPanelEx.Font = new System.Drawing.Font("Meiryo UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._timerPanelEx.Location = new System.Drawing.Point(877, 111);
            this._timerPanelEx.Name = "_timerPanelEx";
            this._timerPanelEx.OutLineColor = System.Drawing.Color.DarkOliveGreen;
            this._timerPanelEx.OutLineSize = 2F;
            this._timerPanelEx.Size = new System.Drawing.Size(173, 187);
            this._timerPanelEx.TabIndex = 370;
            // 
            // ProcTimerTextBox
            // 
            this.ProcTimerTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ProcTimerTextBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ProcTimerTextBox.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Bold);
            this.ProcTimerTextBox.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ProcTimerTextBox.Location = new System.Drawing.Point(9, 153);
            this.ProcTimerTextBox.Name = "ProcTimerTextBox";
            this.ProcTimerTextBox.Size = new System.Drawing.Size(130, 25);
            this.ProcTimerTextBox.TabIndex = 160;
            this.ProcTimerTextBox.Text = "0.0S";
            this.ProcTimerTextBox.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // _oneProcessingTimeBt
            // 
            this._oneProcessingTimeBt.EditBox = null;
            this._oneProcessingTimeBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._oneProcessingTimeBt.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._oneProcessingTimeBt.IsActive = false;
            this._oneProcessingTimeBt.LedSyncedBackColorEnable = true;
            this._oneProcessingTimeBt.Location = new System.Drawing.Point(9, 128);
            this._oneProcessingTimeBt.MultiSelectEn = false;
            this._oneProcessingTimeBt.Name = "_oneProcessingTimeBt";
            this._oneProcessingTimeBt.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._oneProcessingTimeBt.OutLineEn = true;
            this._oneProcessingTimeBt.OutLineSize = 3F;
            this._oneProcessingTimeBt.ProgressBarColor = System.Drawing.Color.Empty;
            this._oneProcessingTimeBt.ProgressBarEnable = false;
            this._oneProcessingTimeBt.ProgressBarMaxValue = 100;
            this._oneProcessingTimeBt.ProgressBarMinValue = 0;
            this._oneProcessingTimeBt.ProgressBarSize = 5;
            this._oneProcessingTimeBt.ProgressBarValue = 0;
            this._oneProcessingTimeBt.Selectable = true;
            this._oneProcessingTimeBt.Selected = false;
            this._oneProcessingTimeBt.Size = new System.Drawing.Size(130, 25);
            this._oneProcessingTimeBt.StatusLedEnable = false;
            this._oneProcessingTimeBt.StatusLedSize = ((byte)(10));
            this._oneProcessingTimeBt.TabIndex = 161;
            this._oneProcessingTimeBt.Text = "1穴加工時間";
            this._oneProcessingTimeBt.UseVisualStyleBackColor = false;
            this._oneProcessingTimeBt.Click += new System.EventHandler(this._oneProcessingTimeBt_Click);
            // 
            // DischargeTimerTextBox
            // 
            this.DischargeTimerTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DischargeTimerTextBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DischargeTimerTextBox.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Bold);
            this.DischargeTimerTextBox.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.DischargeTimerTextBox.Location = new System.Drawing.Point(9, 94);
            this.DischargeTimerTextBox.Name = "DischargeTimerTextBox";
            this.DischargeTimerTextBox.Size = new System.Drawing.Size(130, 25);
            this.DischargeTimerTextBox.TabIndex = 156;
            this.DischargeTimerTextBox.Text = "0H 0M 0S";
            this.DischargeTimerTextBox.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // MachineTimerTextBox
            // 
            this.MachineTimerTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MachineTimerTextBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MachineTimerTextBox.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Bold);
            this.MachineTimerTextBox.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.MachineTimerTextBox.Location = new System.Drawing.Point(9, 35);
            this.MachineTimerTextBox.Name = "MachineTimerTextBox";
            this.MachineTimerTextBox.Size = new System.Drawing.Size(130, 25);
            this.MachineTimerTextBox.TabIndex = 155;
            this.MachineTimerTextBox.Text = "0H 0M 0S";
            this.MachineTimerTextBox.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // _processingTimeBt
            // 
            this._processingTimeBt.EditBox = null;
            this._processingTimeBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._processingTimeBt.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._processingTimeBt.IsActive = false;
            this._processingTimeBt.LedSyncedBackColorEnable = true;
            this._processingTimeBt.Location = new System.Drawing.Point(9, 69);
            this._processingTimeBt.MultiSelectEn = false;
            this._processingTimeBt.Name = "_processingTimeBt";
            this._processingTimeBt.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._processingTimeBt.OutLineEn = true;
            this._processingTimeBt.OutLineSize = 3F;
            this._processingTimeBt.ProgressBarColor = System.Drawing.Color.Empty;
            this._processingTimeBt.ProgressBarEnable = false;
            this._processingTimeBt.ProgressBarMaxValue = 100;
            this._processingTimeBt.ProgressBarMinValue = 0;
            this._processingTimeBt.ProgressBarSize = 5;
            this._processingTimeBt.ProgressBarValue = 0;
            this._processingTimeBt.Selectable = true;
            this._processingTimeBt.Selected = false;
            this._processingTimeBt.Size = new System.Drawing.Size(130, 25);
            this._processingTimeBt.StatusLedEnable = false;
            this._processingTimeBt.StatusLedSize = ((byte)(10));
            this._processingTimeBt.TabIndex = 157;
            this._processingTimeBt.Text = "加工時間";
            this._processingTimeBt.UseVisualStyleBackColor = false;
            this._processingTimeBt.Click += new System.EventHandler(this._processingTimeBt_Click);
            // 
            // _programRunTimeBt
            // 
            this._programRunTimeBt.EditBox = null;
            this._programRunTimeBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._programRunTimeBt.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._programRunTimeBt.IsActive = false;
            this._programRunTimeBt.LedSyncedBackColorEnable = true;
            this._programRunTimeBt.Location = new System.Drawing.Point(9, 10);
            this._programRunTimeBt.MultiSelectEn = false;
            this._programRunTimeBt.Name = "_programRunTimeBt";
            this._programRunTimeBt.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._programRunTimeBt.OutLineEn = true;
            this._programRunTimeBt.OutLineSize = 3F;
            this._programRunTimeBt.ProgressBarColor = System.Drawing.Color.Empty;
            this._programRunTimeBt.ProgressBarEnable = false;
            this._programRunTimeBt.ProgressBarMaxValue = 100;
            this._programRunTimeBt.ProgressBarMinValue = 0;
            this._programRunTimeBt.ProgressBarSize = 5;
            this._programRunTimeBt.ProgressBarValue = 0;
            this._programRunTimeBt.Selectable = true;
            this._programRunTimeBt.Selected = false;
            this._programRunTimeBt.Size = new System.Drawing.Size(130, 25);
            this._programRunTimeBt.StatusLedEnable = false;
            this._programRunTimeBt.StatusLedSize = ((byte)(10));
            this._programRunTimeBt.TabIndex = 155;
            this._programRunTimeBt.Text = "プログラム運転時間";
            this._programRunTimeBt.UseVisualStyleBackColor = false;
            this._programRunTimeBt.Click += new System.EventHandler(this._programRunTimeBt_Click);
            // 
            // pictureBoxEx2
            // 
            this.pictureBoxEx2.IconType = ECNC3.Views.PictureBoxEx.IconTypes.Clear;
            this.pictureBoxEx2.Location = new System.Drawing.Point(137, 16);
            this.pictureBoxEx2.Name = "pictureBoxEx2";
            this.pictureBoxEx2.Size = new System.Drawing.Size(50, 50);
            this.pictureBoxEx2.TabIndex = 150;
            this.pictureBoxEx2.TabStop = false;
            // 
            // LineCodeLabel
            // 
            this.LineCodeLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LineCodeLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LineCodeLabel.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.LineCodeLabel.Location = new System.Drawing.Point(434, 7);
            this.LineCodeLabel.Name = "LineCodeLabel";
            this.LineCodeLabel.Size = new System.Drawing.Size(232, 34);
            this.LineCodeLabel.TabIndex = 162;
            this.LineCodeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _waxisSettingPanelEx
            // 
            this._waxisSettingPanelEx.Controls.Add(this.WAxisLowerVal);
            this._waxisSettingPanelEx.Controls.Add(this.WAxisUpperVal);
            this._waxisSettingPanelEx.Controls.Add(this.WAxisLowerSetBt);
            this._waxisSettingPanelEx.Controls.Add(this.WAxisUpperSetBt);
            this._waxisSettingPanelEx.Controls.Add(this.WAxisUpperUnitLabel);
            this._waxisSettingPanelEx.Controls.Add(this.WAxisLowerUnitLabel);
            this._waxisSettingPanelEx.Controls.Add(this.pictureBoxEx1);
            this._waxisSettingPanelEx.Font = new System.Drawing.Font("Meiryo UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._waxisSettingPanelEx.Location = new System.Drawing.Point(-22, 414);
            this._waxisSettingPanelEx.Name = "_waxisSettingPanelEx";
            this._waxisSettingPanelEx.OutLineColor = System.Drawing.Color.DarkOliveGreen;
            this._waxisSettingPanelEx.OutLineSize = 2F;
            this._waxisSettingPanelEx.Size = new System.Drawing.Size(443, 88);
            this._waxisSettingPanelEx.TabIndex = 371;
            // 
            // WAxisLowerVal
            // 
            this.WAxisLowerVal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.WAxisLowerVal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.WAxisLowerVal.Font = new System.Drawing.Font("Meiryo UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.WAxisLowerVal.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.WAxisLowerVal.Location = new System.Drawing.Point(218, 46);
            this.WAxisLowerVal.Name = "WAxisLowerVal";
            this.WAxisLowerVal.Size = new System.Drawing.Size(177, 35);
            this.WAxisLowerVal.TabIndex = 375;
            this.WAxisLowerVal.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // WAxisUpperVal
            // 
            this.WAxisUpperVal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.WAxisUpperVal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.WAxisUpperVal.Font = new System.Drawing.Font("Meiryo UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.WAxisUpperVal.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.WAxisUpperVal.Location = new System.Drawing.Point(218, 7);
            this.WAxisUpperVal.Name = "WAxisUpperVal";
            this.WAxisUpperVal.Size = new System.Drawing.Size(177, 35);
            this.WAxisUpperVal.TabIndex = 374;
            this.WAxisUpperVal.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // WAxisLowerSetBt
            // 
            this.WAxisLowerSetBt.EditBox = null;
            this.WAxisLowerSetBt.Enabled = false;
            this.WAxisLowerSetBt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.WAxisLowerSetBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.WAxisLowerSetBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.WAxisLowerSetBt.IsActive = false;
            this.WAxisLowerSetBt.LedSyncedBackColorEnable = true;
            this.WAxisLowerSetBt.Location = new System.Drawing.Point(30, 46);
            this.WAxisLowerSetBt.MultiSelectEn = false;
            this.WAxisLowerSetBt.Name = "WAxisLowerSetBt";
            this.WAxisLowerSetBt.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.WAxisLowerSetBt.OutLineEn = true;
            this.WAxisLowerSetBt.OutLineSize = 3F;
            this.WAxisLowerSetBt.ProgressBarColor = System.Drawing.Color.Empty;
            this.WAxisLowerSetBt.ProgressBarEnable = false;
            this.WAxisLowerSetBt.ProgressBarMaxValue = 100;
            this.WAxisLowerSetBt.ProgressBarMinValue = 0;
            this.WAxisLowerSetBt.ProgressBarSize = 5;
            this.WAxisLowerSetBt.ProgressBarValue = 0;
            this.WAxisLowerSetBt.Selectable = true;
            this.WAxisLowerSetBt.Selected = false;
            this.WAxisLowerSetBt.Size = new System.Drawing.Size(188, 36);
            this.WAxisLowerSetBt.StatusLedEnable = false;
            this.WAxisLowerSetBt.StatusLedSize = ((byte)(10));
            this.WAxisLowerSetBt.TabIndex = 373;
            this.WAxisLowerSetBt.TabStop = false;
            this.WAxisLowerSetBt.Text = "W軸下限値";
            this.WAxisLowerSetBt.UseVisualStyleBackColor = false;
            // 
            // WAxisUpperSetBt
            // 
            this.WAxisUpperSetBt.EditBox = null;
            this.WAxisUpperSetBt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.WAxisUpperSetBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.WAxisUpperSetBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.WAxisUpperSetBt.IsActive = false;
            this.WAxisUpperSetBt.LedSyncedBackColorEnable = true;
            this.WAxisUpperSetBt.Location = new System.Drawing.Point(30, 7);
            this.WAxisUpperSetBt.MultiSelectEn = false;
            this.WAxisUpperSetBt.Name = "WAxisUpperSetBt";
            this.WAxisUpperSetBt.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.WAxisUpperSetBt.OutLineEn = true;
            this.WAxisUpperSetBt.OutLineSize = 3F;
            this.WAxisUpperSetBt.ProgressBarColor = System.Drawing.Color.Empty;
            this.WAxisUpperSetBt.ProgressBarEnable = false;
            this.WAxisUpperSetBt.ProgressBarMaxValue = 100;
            this.WAxisUpperSetBt.ProgressBarMinValue = 0;
            this.WAxisUpperSetBt.ProgressBarSize = 5;
            this.WAxisUpperSetBt.ProgressBarValue = 0;
            this.WAxisUpperSetBt.Selectable = true;
            this.WAxisUpperSetBt.Selected = false;
            this.WAxisUpperSetBt.Size = new System.Drawing.Size(188, 36);
            this.WAxisUpperSetBt.StatusLedEnable = false;
            this.WAxisUpperSetBt.StatusLedSize = ((byte)(10));
            this.WAxisUpperSetBt.TabIndex = 17;
            this.WAxisUpperSetBt.TabStop = false;
            this.WAxisUpperSetBt.Text = "W軸上限値";
            this.WAxisUpperSetBt.UseVisualStyleBackColor = false;
            this.WAxisUpperSetBt.Click += new System.EventHandler(this.WAxisUpperSetBt_Click);
            // 
            // WAxisUpperUnitLabel
            // 
            this.WAxisUpperUnitLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.WAxisUpperUnitLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.WAxisUpperUnitLabel.Font = new System.Drawing.Font("Meiryo UI", 12F);
            this.WAxisUpperUnitLabel.Location = new System.Drawing.Point(383, 7);
            this.WAxisUpperUnitLabel.Name = "WAxisUpperUnitLabel";
            this.WAxisUpperUnitLabel.Size = new System.Drawing.Size(53, 35);
            this.WAxisUpperUnitLabel.TabIndex = 376;
            this.WAxisUpperUnitLabel.Text = "mm";
            this.WAxisUpperUnitLabel.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // WAxisLowerUnitLabel
            // 
            this.WAxisLowerUnitLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.WAxisLowerUnitLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.WAxisLowerUnitLabel.Font = new System.Drawing.Font("Meiryo UI", 12F);
            this.WAxisLowerUnitLabel.Location = new System.Drawing.Point(383, 46);
            this.WAxisLowerUnitLabel.Name = "WAxisLowerUnitLabel";
            this.WAxisLowerUnitLabel.Size = new System.Drawing.Size(53, 35);
            this.WAxisLowerUnitLabel.TabIndex = 377;
            this.WAxisLowerUnitLabel.Text = "mm";
            this.WAxisLowerUnitLabel.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // pictureBoxEx1
            // 
            this.pictureBoxEx1.IconType = ECNC3.Views.PictureBoxEx.IconTypes.Clear;
            this.pictureBoxEx1.Location = new System.Drawing.Point(137, 16);
            this.pictureBoxEx1.Name = "pictureBoxEx1";
            this.pictureBoxEx1.Size = new System.Drawing.Size(50, 50);
            this.pictureBoxEx1.TabIndex = 150;
            this.pictureBoxEx1.TabStop = false;
            // 
            // plot1
            // 
            this.plot1.Location = new System.Drawing.Point(560, 114);
            this.plot1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.plot1.Name = "plot1";
            this.plot1.OutLineSize = 3;
            this.plot1.plotNotify = null;
            this.plot1.Size = new System.Drawing.Size(293, 174);
            this.plot1.TabIndex = 367;
            // 
            // levelGage1
            // 
            this.levelGage1.ACatName = "noname";
            this.levelGage1.AValue = 0D;
            this.levelGage1.Location = new System.Drawing.Point(6, 7);
            this.levelGage1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.levelGage1.Name = "levelGage1";
            this.levelGage1.Size = new System.Drawing.Size(140, 175);
            this.levelGage1.TabIndex = 368;
            this.levelGage1.VCatName = "noname";
            this.levelGage1.VValue = 0D;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this._btnReset);
            this.panel2.Location = new System.Drawing.Point(867, 618);
            this.panel2.Name = "panel2";
            this.panel2.OutLineColor = System.Drawing.Color.Maroon;
            this.panel2.OutLineSize = 2F;
            this.panel2.Size = new System.Drawing.Size(153, 91);
            this.panel2.TabIndex = 365;
            // 
            // _btnReset
            // 
            this._btnReset.EditBox = null;
            this._btnReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this._btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnReset.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._btnReset.IsActive = false;
            this._btnReset.LedSyncedBackColorEnable = true;
            this._btnReset.Location = new System.Drawing.Point(9, 8);
            this._btnReset.MultiSelectEn = false;
            this._btnReset.Name = "_btnReset";
            this._btnReset.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._btnReset.OutLineEn = true;
            this._btnReset.OutLineSize = 3F;
            this._btnReset.ProgressBarColor = System.Drawing.Color.Empty;
            this._btnReset.ProgressBarEnable = false;
            this._btnReset.ProgressBarMaxValue = 100;
            this._btnReset.ProgressBarMinValue = 0;
            this._btnReset.ProgressBarSize = 5;
            this._btnReset.ProgressBarValue = 0;
            this._btnReset.Selectable = true;
            this._btnReset.Selected = false;
            this._btnReset.Size = new System.Drawing.Size(133, 50);
            this._btnReset.StatusLedEnable = false;
            this._btnReset.StatusLedSize = ((byte)(10));
            this._btnReset.TabIndex = 61;
            this._btnReset.TabStop = false;
            this._btnReset.Text = "リセット";
            this._btnReset.UseVisualStyleBackColor = false;
            this._btnReset.Click += new System.EventHandler(this._btnReset_Click);
            // 
            // panelEx1
            // 
            this.panelEx1.Controls.Add(this._HelpBtn);
            this.panelEx1.Controls.Add(this.DryRunBt);
            this.panelEx1.Controls.Add(this.SingleBlockBt);
            this.panelEx1.Controls.Add(this.FileKeybordFormBt);
            this.panelEx1.Controls.Add(this.OptionBt);
            this.panelEx1.Location = new System.Drawing.Point(5, 620);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.OutLineColor = System.Drawing.Color.DarkOliveGreen;
            this.panelEx1.OutLineSize = 2F;
            this.panelEx1.Size = new System.Drawing.Size(725, 82);
            this.panelEx1.TabIndex = 364;
            // 
            // _HelpBtn
            // 
            this._HelpBtn.EditBox = null;
            this._HelpBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this._HelpBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._HelpBtn.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._HelpBtn.IsActive = false;
            this._HelpBtn.LedSyncedBackColorEnable = true;
            this._HelpBtn.Location = new System.Drawing.Point(291, 5);
            this._HelpBtn.MultiSelectEn = false;
            this._HelpBtn.Name = "_HelpBtn";
            this._HelpBtn.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._HelpBtn.OutLineEn = true;
            this._HelpBtn.OutLineSize = 3F;
            this._HelpBtn.ProgressBarColor = System.Drawing.Color.Empty;
            this._HelpBtn.ProgressBarEnable = false;
            this._HelpBtn.ProgressBarMaxValue = 100;
            this._HelpBtn.ProgressBarMinValue = 0;
            this._HelpBtn.ProgressBarSize = 5;
            this._HelpBtn.ProgressBarValue = 0;
            this._HelpBtn.Selectable = true;
            this._HelpBtn.Selected = false;
            this._HelpBtn.Size = new System.Drawing.Size(142, 50);
            this._HelpBtn.StatusLedEnable = false;
            this._HelpBtn.StatusLedSize = ((byte)(10));
            this._HelpBtn.TabIndex = 61;
            this._HelpBtn.TabStop = false;
            this._HelpBtn.Text = "ヘルプ";
            this._HelpBtn.UseVisualStyleBackColor = false;
            this._HelpBtn.Click += new System.EventHandler(this.ButtunEx_Help_Click);
            // 
            // DryRunBt
            // 
            this.DryRunBt.EditBox = null;
            this.DryRunBt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.DryRunBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DryRunBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DryRunBt.IsActive = false;
            this.DryRunBt.LedSyncedBackColorEnable = true;
            this.DryRunBt.Location = new System.Drawing.Point(5, 5);
            this.DryRunBt.MultiSelectEn = false;
            this.DryRunBt.Name = "DryRunBt";
            this.DryRunBt.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.DryRunBt.OutLineEn = true;
            this.DryRunBt.OutLineSize = 3F;
            this.DryRunBt.ProgressBarColor = System.Drawing.Color.Empty;
            this.DryRunBt.ProgressBarEnable = false;
            this.DryRunBt.ProgressBarMaxValue = 100;
            this.DryRunBt.ProgressBarMinValue = 0;
            this.DryRunBt.ProgressBarSize = 5;
            this.DryRunBt.ProgressBarValue = 0;
            this.DryRunBt.Selectable = true;
            this.DryRunBt.Selected = false;
            this.DryRunBt.Size = new System.Drawing.Size(142, 50);
            this.DryRunBt.StatusLedEnable = true;
            this.DryRunBt.StatusLedSize = ((byte)(10));
            this.DryRunBt.TabIndex = 57;
            this.DryRunBt.TabStop = false;
            this.DryRunBt.Text = "ドライラン";
            this.DryRunBt.UseVisualStyleBackColor = false;
            this.DryRunBt.Click += new System.EventHandler(this.DryRunBt_Click);
            // 
            // SingleBlockBt
            // 
            this.SingleBlockBt.EditBox = null;
            this.SingleBlockBt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.SingleBlockBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SingleBlockBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SingleBlockBt.IsActive = false;
            this.SingleBlockBt.LedSyncedBackColorEnable = true;
            this.SingleBlockBt.Location = new System.Drawing.Point(148, 5);
            this.SingleBlockBt.MultiSelectEn = false;
            this.SingleBlockBt.Name = "SingleBlockBt";
            this.SingleBlockBt.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.SingleBlockBt.OutLineEn = true;
            this.SingleBlockBt.OutLineSize = 3F;
            this.SingleBlockBt.ProgressBarColor = System.Drawing.Color.Empty;
            this.SingleBlockBt.ProgressBarEnable = false;
            this.SingleBlockBt.ProgressBarMaxValue = 100;
            this.SingleBlockBt.ProgressBarMinValue = 0;
            this.SingleBlockBt.ProgressBarSize = 5;
            this.SingleBlockBt.ProgressBarValue = 0;
            this.SingleBlockBt.Selectable = true;
            this.SingleBlockBt.Selected = false;
            this.SingleBlockBt.Size = new System.Drawing.Size(142, 50);
            this.SingleBlockBt.StatusLedEnable = true;
            this.SingleBlockBt.StatusLedSize = ((byte)(10));
            this.SingleBlockBt.TabIndex = 58;
            this.SingleBlockBt.TabStop = false;
            this.SingleBlockBt.Text = "シングルブロック";
            this.SingleBlockBt.UseVisualStyleBackColor = false;
            this.SingleBlockBt.Click += new System.EventHandler(this.SingleBlockBt_Click);
            // 
            // FileKeybordFormBt
            // 
            this.FileKeybordFormBt.EditBox = null;
            this.FileKeybordFormBt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.FileKeybordFormBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FileKeybordFormBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FileKeybordFormBt.IsActive = false;
            this.FileKeybordFormBt.LedSyncedBackColorEnable = true;
            this.FileKeybordFormBt.Location = new System.Drawing.Point(577, 5);
            this.FileKeybordFormBt.MultiSelectEn = false;
            this.FileKeybordFormBt.Name = "FileKeybordFormBt";
            this.FileKeybordFormBt.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.FileKeybordFormBt.OutLineEn = true;
            this.FileKeybordFormBt.OutLineSize = 3F;
            this.FileKeybordFormBt.ProgressBarColor = System.Drawing.Color.Empty;
            this.FileKeybordFormBt.ProgressBarEnable = false;
            this.FileKeybordFormBt.ProgressBarMaxValue = 100;
            this.FileKeybordFormBt.ProgressBarMinValue = 0;
            this.FileKeybordFormBt.ProgressBarSize = 5;
            this.FileKeybordFormBt.ProgressBarValue = 0;
            this.FileKeybordFormBt.Selectable = true;
            this.FileKeybordFormBt.Selected = false;
            this.FileKeybordFormBt.Size = new System.Drawing.Size(142, 50);
            this.FileKeybordFormBt.StatusLedEnable = false;
            this.FileKeybordFormBt.StatusLedSize = ((byte)(10));
            this.FileKeybordFormBt.TabIndex = 59;
            this.FileKeybordFormBt.TabStop = false;
            this.FileKeybordFormBt.UseVisualStyleBackColor = false;
            this.FileKeybordFormBt.Click += new System.EventHandler(this.FileKeybordFormBt_Click);
            // 
            // OptionBt
            // 
            this.OptionBt.EditBox = null;
            this.OptionBt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.OptionBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OptionBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.OptionBt.IsActive = false;
            this.OptionBt.LedSyncedBackColorEnable = true;
            this.OptionBt.Location = new System.Drawing.Point(434, 5);
            this.OptionBt.MultiSelectEn = false;
            this.OptionBt.Name = "OptionBt";
            this.OptionBt.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.OptionBt.OutLineEn = true;
            this.OptionBt.OutLineSize = 3F;
            this.OptionBt.ProgressBarColor = System.Drawing.Color.Empty;
            this.OptionBt.ProgressBarEnable = false;
            this.OptionBt.ProgressBarMaxValue = 100;
            this.OptionBt.ProgressBarMinValue = 0;
            this.OptionBt.ProgressBarSize = 5;
            this.OptionBt.ProgressBarValue = 0;
            this.OptionBt.Selectable = true;
            this.OptionBt.Selected = false;
            this.OptionBt.Size = new System.Drawing.Size(142, 50);
            this.OptionBt.StatusLedEnable = false;
            this.OptionBt.StatusLedSize = ((byte)(10));
            this.OptionBt.TabIndex = 60;
            this.OptionBt.TabStop = false;
            this.OptionBt.Text = "オプション";
            this.OptionBt.UseVisualStyleBackColor = false;
            this.OptionBt.Click += new System.EventHandler(this.OptionFormBt_Click);
            // 
            // axisMonitor1
            // 
            this.axisMonitor1.HeaderPainted = false;
            this.axisMonitor1.Location = new System.Drawing.Point(5, 4);
            this.axisMonitor1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.axisMonitor1.Name = "axisMonitor1";
            this.axisMonitor1.Size = new System.Drawing.Size(423, 372);
            this.axisMonitor1.TabIndex = 153;
            // 
            // ConditionsFormOpenBt
            // 
            this.ConditionsFormOpenBt.EditBox = null;
            this.ConditionsFormOpenBt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.ConditionsFormOpenBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ConditionsFormOpenBt.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ConditionsFormOpenBt.IsActive = false;
            this.ConditionsFormOpenBt.LedSyncedBackColorEnable = true;
            this.ConditionsFormOpenBt.Location = new System.Drawing.Point(6, 534);
            this.ConditionsFormOpenBt.MultiSelectEn = false;
            this.ConditionsFormOpenBt.Name = "ConditionsFormOpenBt";
            this.ConditionsFormOpenBt.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ConditionsFormOpenBt.OutLineEn = true;
            this.ConditionsFormOpenBt.OutLineSize = 3F;
            this.ConditionsFormOpenBt.ProgressBarColor = System.Drawing.Color.Empty;
            this.ConditionsFormOpenBt.ProgressBarEnable = false;
            this.ConditionsFormOpenBt.ProgressBarMaxValue = 100;
            this.ConditionsFormOpenBt.ProgressBarMinValue = 0;
            this.ConditionsFormOpenBt.ProgressBarSize = 5;
            this.ConditionsFormOpenBt.ProgressBarValue = 0;
            this.ConditionsFormOpenBt.Selectable = true;
            this.ConditionsFormOpenBt.Selected = false;
            this.ConditionsFormOpenBt.Size = new System.Drawing.Size(91, 40);
            this.ConditionsFormOpenBt.StatusLedEnable = false;
            this.ConditionsFormOpenBt.StatusLedSize = ((byte)(10));
            this.ConditionsFormOpenBt.TabIndex = 18;
            this.ConditionsFormOpenBt.TabStop = false;
            this.ConditionsFormOpenBt.Text = "加工条件";
            this.ConditionsFormOpenBt.UseVisualStyleBackColor = false;
            this.ConditionsFormOpenBt.Click += new System.EventHandler(this.ConditionsFormOpenBt_Click);
            // 
            // PnumTextBox
            // 
            this.PnumTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnumTextBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PnumTextBox.Font = new System.Drawing.Font("Meiryo UI", 18F);
            this.PnumTextBox.Location = new System.Drawing.Point(35, 574);
            this.PnumTextBox.Name = "PnumTextBox";
            this.PnumTextBox.Size = new System.Drawing.Size(62, 38);
            this.PnumTextBox.TabIndex = 347;
            this.PnumTextBox.Text = "000";
            this.PnumTextBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PTextBox
            // 
            this.PTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PTextBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PTextBox.Font = new System.Drawing.Font("Meiryo UI", 18F);
            this.PTextBox.Location = new System.Drawing.Point(6, 574);
            this.PTextBox.Name = "PTextBox";
            this.PTextBox.Size = new System.Drawing.Size(29, 38);
            this.PTextBox.TabIndex = 31;
            this.PTextBox.Text = "P";
            // 
            // ProgramStatusLabel
            // 
            this.ProgramStatusLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ProgramStatusLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ProgramStatusLabel.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ProgramStatusLabel.Location = new System.Drawing.Point(424, 300);
            this.ProgramStatusLabel.Name = "ProgramStatusLabel";
            this.ProgramStatusLabel.Size = new System.Drawing.Size(595, 20);
            this.ProgramStatusLabel.TabIndex = 372;
            // 
            // ProgramEditBox
            // 
            this.ProgramEditBox.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ProgramEditBox.Location = new System.Drawing.Point(424, 319);
            this.ProgramEditBox.Name = "ProgramEditBox";
            this.ProgramEditBox.RowBackColorEn = true;
            this.ProgramEditBox.RowBackColorKey = ((System.Collections.Generic.List<string>)(resources.GetObject("ProgramEditBox.RowBackColorKey")));
            this.ProgramEditBox.Size = new System.Drawing.Size(595, 212);
            this.ProgramEditBox.TabIndex = 373;
            this.ProgramEditBox.Text = "";
            // 
            // _GraphPanel
            // 
            this._GraphPanel.Controls.Add(this.levelGage1);
            this._GraphPanel.Location = new System.Drawing.Point(428, 111);
            this._GraphPanel.Name = "_GraphPanel";
            this._GraphPanel.OutLineColor = System.Drawing.Color.DarkOliveGreen;
            this._GraphPanel.OutLineSize = 2F;
            this._GraphPanel.Size = new System.Drawing.Size(446, 187);
            this._GraphPanel.TabIndex = 375;
            // 
            // editProcessCondition1
            // 
            this.editProcessCondition1.CallingFunction = ECNC3.Views.EditProcessCondition.CallingFunctions.Manual;
            this.editProcessCondition1.CurrentProcessConditionNumber = 0;
            this.editProcessCondition1.Location = new System.Drawing.Point(3, 501);
            this.editProcessCondition1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.editProcessCondition1.Name = "editProcessCondition1";
            this.editProcessCondition1.Protect = 0;
            this.editProcessCondition1.Size = new System.Drawing.Size(1024, 113);
            this.editProcessCondition1.TabIndex = 376;
            // 
            // MDIAUTOForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 678);
            this.Controls.Add(this.plot1);
            this.Controls.Add(this._GraphPanel);
            this.Controls.Add(this.ProgramEditBox);
            this.Controls.Add(this.ProgramStatusLabel);
            this.Controls.Add(this.ConditionsFormOpenBt);
            this.Controls.Add(this.PnumTextBox);
            this.Controls.Add(this.ProgramTextList);
            this.Controls.Add(this.PTextBox);
            this.Controls.Add(this._waxisSettingPanelEx);
            this.Controls.Add(this.LineCodeLabel);
            this.Controls.Add(this._timerPanelEx);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.axisMonitor1);
            this.Controls.Add(this.editProcessCondition1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(0, 90);
            this.Name = "MDIAUTOForm";
            this.OutLineSize = 3;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Activated += new System.EventHandler(this.MDIAUTOForm_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MDIAUTOForm_FormClosed);
            this.Load += new System.EventHandler(this.MDIAUTOForm_Load);
            this.VisibleChanged += new System.EventHandler(this.MDIAUTOForm_VisibleChanged);
            this.panel6.ResumeLayout(false);
            this._timerPanelEx.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEx2)).EndInit();
            this._waxisSettingPanelEx.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEx1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this._GraphPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private ListBoxEx ProgramTextList;
        private AxisMonitor axisMonitor1;
        private ECNC3.Views.PanelEx panel6;
        private LabelEx M02Label;
        private LabelEx SignalLabel;
        private LabelEx MachineLockLabel;
        private LabelEx CorrectAngleLabel;
        private LabelEx PartitionRoundStopLabel;
        private LabelEx StartNoLabel;
        private LabelEx IncRefMoveLabel;
        private LabelEx BlockSkipLabel;
        private LabelEx OptionalStopLabel;
        private LabelEx BuzzerLabel;
        private LabelEx AecByLifeLabel;
        private PanelEx panel2;
        private ButtonEx _btnReset;
        private PanelEx panelEx1;
        private ButtonEx DryRunBt;
        private ButtonEx SingleBlockBt;
        private ButtonEx FileKeybordFormBt;
        private ButtonEx OptionBt;
        private ButtonEx ConditionsFormOpenBt;
        private LabelEx PnumTextBox;
        private LogForm plot1;
        private UserControls.LevelGage levelGage1;
        private ECNC3.Views.PanelEx _timerPanelEx;
        private LabelEx ProcTimerTextBox;
        private LabelEx DischargeTimerTextBox;
        private LabelEx MachineTimerTextBox;
        private ButtonEx _processingTimeBt;
        private ButtonEx _programRunTimeBt;
        private PictureBoxEx pictureBoxEx2;
        private ButtonEx _oneProcessingTimeBt;
        private LabelEx LineCodeLabel;
        private ECNC3.Views.PanelEx _waxisSettingPanelEx;
        private LabelEx WAxisLowerVal;
        private LabelEx WAxisUpperVal;
        private ButtonEx WAxisLowerSetBt;
        private ButtonEx WAxisUpperSetBt;
        private LabelEx WAxisUpperUnitLabel;
        private LabelEx WAxisLowerUnitLabel;
        private PictureBoxEx pictureBoxEx1;
        private LabelEx ProgramStatusLabel;
        private LabelEx PTextBox;
        private RichTextBoxEx ProgramEditBox;
        private PanelEx _GraphPanel;
        private EditProcessCondition editProcessCondition1;
        private ButtonEx _HelpBtn;
    }
}