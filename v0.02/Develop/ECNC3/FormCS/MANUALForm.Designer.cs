﻿namespace ECNC3.Views
{
	partial class MANUALForm
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		protected override void Dispose( bool disposing )
		{
			if( disposing && ( components != null ) ) {
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MANUALForm));
            this._timerPanelEx = new ECNC3.Views.PanelEx();
            this.ProcTimerTextBox = new ECNC3.Views.LabelEx();
            this._oneProcessingTimeBt = new ECNC3.Views.ButtonEx();
            this.DischargeTimerTextBox = new ECNC3.Views.LabelEx();
            this.MachineTimerTextBox = new ECNC3.Views.LabelEx();
            this._processingTimeBt = new ECNC3.Views.ButtonEx();
            this._programRunTimeBt = new ECNC3.Views.ButtonEx();
            this.pictureBoxEx2 = new ECNC3.Views.PictureBoxEx();
            this._waxisSettingPanelEx = new ECNC3.Views.PanelEx();
            this.WAxisLowerVal = new ECNC3.Views.LabelEx();
            this.WAxisUpperVal = new ECNC3.Views.LabelEx();
            this.WAxisLowerSetBt = new ECNC3.Views.ButtonEx();
            this.WAxisUpperSetBt = new ECNC3.Views.ButtonEx();
            this.WAxisUpperUnitLabel = new ECNC3.Views.LabelEx();
            this.WAxisLowerUnitLabel = new ECNC3.Views.LabelEx();
            this.pictureBoxEx1 = new ECNC3.Views.PictureBoxEx();
            this.FunctionBt = new ECNC3.Views.ButtonEx();
            this.BuzzerBt = new ECNC3.Views.ButtonEx();
            this.InitialSetBt = new ECNC3.Views.ButtonEx();
            this.ContactSensingBt = new ECNC3.Views.ButtonEx();
            this.levelGage1 = new ECNC3.Views.UserControls.LevelGage();
            this.axisMonitor2 = new ECNC3.Views.AxisMonitor();
            this.ConditionsFormOpenBt = new ECNC3.Views.ButtonEx();
            this.PnumTextBox = new ECNC3.Views.LabelEx();
            this.panel2 = new ECNC3.Views.PanelEx();
            this.SpinBt = new ECNC3.Views.ButtonEx();
            this.DischargeBt = new ECNC3.Views.ButtonEx();
            this.PompBt = new ECNC3.Views.ButtonEx();
            this.PTextBox = new ECNC3.Views.LabelEx();
            this.panel1 = new ECNC3.Views.PanelEx();
            this.ReturnOriginBt = new ECNC3.Views.ButtonEx();
            this.AecBt = new ECNC3.Views.ButtonEx();
            this.ReferencingBt = new ECNC3.Views.ButtonEx();
            this.NumericFeedBt = new ECNC3.Views.ButtonEx();
            this.plot1 = new ECNC3.Views.LogForm();
            this.panelEx1 = new ECNC3.Views.PanelEx();
            this.panelEx2 = new ECNC3.Views.PanelEx();
            this._GraphPanel = new ECNC3.Views.PanelEx();
            this.editProcessCondition1 = new ECNC3.Views.EditProcessCondition();
            this._timerPanelEx.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEx2)).BeginInit();
            this._waxisSettingPanelEx.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEx1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this._GraphPanel.SuspendLayout();
            this.SuspendLayout();
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
            this._oneProcessingTimeBt.TextAlign = System.Drawing.ContentAlignment.TopCenter;
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
            this._processingTimeBt.TextAlign = System.Drawing.ContentAlignment.TopCenter;
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
            this._programRunTimeBt.TextAlign = System.Drawing.ContentAlignment.TopCenter;
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
            this.WAxisUpperUnitLabel.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
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
            this.WAxisLowerUnitLabel.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
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
            // FunctionBt
            // 
            this.FunctionBt.EditBox = null;
            this.FunctionBt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.FunctionBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FunctionBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FunctionBt.IsActive = false;
            this.FunctionBt.LedSyncedBackColorEnable = true;
            this.FunctionBt.Location = new System.Drawing.Point(7, 7);
            this.FunctionBt.MultiSelectEn = false;
            this.FunctionBt.Name = "FunctionBt";
            this.FunctionBt.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.FunctionBt.OutLineEn = true;
            this.FunctionBt.OutLineSize = 3F;
            this.FunctionBt.ProgressBarColor = System.Drawing.Color.Empty;
            this.FunctionBt.ProgressBarEnable = false;
            this.FunctionBt.ProgressBarMaxValue = 100;
            this.FunctionBt.ProgressBarMinValue = 0;
            this.FunctionBt.ProgressBarSize = 5;
            this.FunctionBt.ProgressBarValue = 0;
            this.FunctionBt.Selectable = true;
            this.FunctionBt.Selected = false;
            this.FunctionBt.Size = new System.Drawing.Size(128, 42);
            this.FunctionBt.StatusLedEnable = false;
            this.FunctionBt.StatusLedSize = ((byte)(10));
            this.FunctionBt.TabIndex = 0;
            this.FunctionBt.Text = "機能";
            this.FunctionBt.UseVisualStyleBackColor = false;
            this.FunctionBt.Click += new System.EventHandler(this.FunctionFormBt_Click);
            // 
            // BuzzerBt
            // 
            this.BuzzerBt.EditBox = null;
            this.BuzzerBt.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.BuzzerBt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.BuzzerBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BuzzerBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BuzzerBt.IsActive = false;
            this.BuzzerBt.LedSyncedBackColorEnable = true;
            this.BuzzerBt.Location = new System.Drawing.Point(7, 7);
            this.BuzzerBt.MultiSelectEn = false;
            this.BuzzerBt.Name = "BuzzerBt";
            this.BuzzerBt.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BuzzerBt.OutLineEn = true;
            this.BuzzerBt.OutLineSize = 3F;
            this.BuzzerBt.ProgressBarColor = System.Drawing.Color.Empty;
            this.BuzzerBt.ProgressBarEnable = false;
            this.BuzzerBt.ProgressBarMaxValue = 100;
            this.BuzzerBt.ProgressBarMinValue = 0;
            this.BuzzerBt.ProgressBarSize = 5;
            this.BuzzerBt.ProgressBarValue = 0;
            this.BuzzerBt.Selectable = true;
            this.BuzzerBt.Selected = false;
            this.BuzzerBt.Size = new System.Drawing.Size(136, 42);
            this.BuzzerBt.StatusLedEnable = true;
            this.BuzzerBt.StatusLedSize = ((byte)(10));
            this.BuzzerBt.TabIndex = 11;
            this.BuzzerBt.TabStop = false;
            this.BuzzerBt.Text = "ブザー";
            this.BuzzerBt.UseVisualStyleBackColor = false;
            this.BuzzerBt.Click += new System.EventHandler(this.BuzzerBt_Click);
            // 
            // InitialSetBt
            // 
            this.InitialSetBt.EditBox = null;
            this.InitialSetBt.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.InitialSetBt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.InitialSetBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.InitialSetBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.InitialSetBt.IsActive = false;
            this.InitialSetBt.LedSyncedBackColorEnable = true;
            this.InitialSetBt.Location = new System.Drawing.Point(149, 7);
            this.InitialSetBt.MultiSelectEn = false;
            this.InitialSetBt.Name = "InitialSetBt";
            this.InitialSetBt.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.InitialSetBt.OutLineEn = true;
            this.InitialSetBt.OutLineSize = 3F;
            this.InitialSetBt.ProgressBarColor = System.Drawing.Color.Empty;
            this.InitialSetBt.ProgressBarEnable = false;
            this.InitialSetBt.ProgressBarMaxValue = 100;
            this.InitialSetBt.ProgressBarMinValue = 0;
            this.InitialSetBt.ProgressBarSize = 5;
            this.InitialSetBt.ProgressBarValue = 0;
            this.InitialSetBt.Selectable = true;
            this.InitialSetBt.Selected = false;
            this.InitialSetBt.Size = new System.Drawing.Size(136, 42);
            this.InitialSetBt.StatusLedEnable = true;
            this.InitialSetBt.StatusLedSize = ((byte)(10));
            this.InitialSetBt.TabIndex = 13;
            this.InitialSetBt.TabStop = false;
            this.InitialSetBt.Text = "イニシャルセット";
            this.InitialSetBt.UseVisualStyleBackColor = false;
            this.InitialSetBt.Click += new System.EventHandler(this.InitialSetBt_Click);
            // 
            // ContactSensingBt
            // 
            this.ContactSensingBt.EditBox = null;
            this.ContactSensingBt.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.ContactSensingBt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.ContactSensingBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ContactSensingBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ContactSensingBt.IsActive = false;
            this.ContactSensingBt.LedSyncedBackColorEnable = true;
            this.ContactSensingBt.Location = new System.Drawing.Point(291, 7);
            this.ContactSensingBt.MultiSelectEn = false;
            this.ContactSensingBt.Name = "ContactSensingBt";
            this.ContactSensingBt.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ContactSensingBt.OutLineEn = true;
            this.ContactSensingBt.OutLineSize = 3F;
            this.ContactSensingBt.ProgressBarColor = System.Drawing.Color.Empty;
            this.ContactSensingBt.ProgressBarEnable = false;
            this.ContactSensingBt.ProgressBarMaxValue = 100;
            this.ContactSensingBt.ProgressBarMinValue = 0;
            this.ContactSensingBt.ProgressBarSize = 5;
            this.ContactSensingBt.ProgressBarValue = 0;
            this.ContactSensingBt.Selectable = true;
            this.ContactSensingBt.Selected = false;
            this.ContactSensingBt.Size = new System.Drawing.Size(136, 42);
            this.ContactSensingBt.StatusLedEnable = true;
            this.ContactSensingBt.StatusLedSize = ((byte)(10));
            this.ContactSensingBt.TabIndex = 15;
            this.ContactSensingBt.TabStop = false;
            this.ContactSensingBt.Text = "接触感知";
            this.ContactSensingBt.UseVisualStyleBackColor = false;
            this.ContactSensingBt.Click += new System.EventHandler(this.ContactSensingBt_Click);
            // 
            // levelGage1
            // 
            this.levelGage1.ACatName = "noname";
            this.levelGage1.AValue = 0D;
            this.levelGage1.Location = new System.Drawing.Point(6, 7);
            this.levelGage1.Name = "levelGage1";
            this.levelGage1.Size = new System.Drawing.Size(141, 175);
            this.levelGage1.TabIndex = 334;
            this.levelGage1.VCatName = "noname";
            this.levelGage1.VValue = 0D;
            // 
            // axisMonitor2
            // 
            this.axisMonitor2.HeaderPainted = false;
            this.axisMonitor2.Location = new System.Drawing.Point(5, 4);
            this.axisMonitor2.Name = "axisMonitor2";
            this.axisMonitor2.Size = new System.Drawing.Size(421, 368);
            this.axisMonitor2.TabIndex = 331;
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
            // panel2
            // 
            this.panel2.Controls.Add(this.SpinBt);
            this.panel2.Controls.Add(this.DischargeBt);
            this.panel2.Controls.Add(this.PompBt);
            this.panel2.Location = new System.Drawing.Point(609, 616);
            this.panel2.Name = "panel2";
            this.panel2.OutLineColor = System.Drawing.Color.Maroon;
            this.panel2.OutLineSize = 2F;
            this.panel2.Size = new System.Drawing.Size(412, 91);
            this.panel2.TabIndex = 151;
            // 
            // SpinBt
            // 
            this.SpinBt.EditBox = null;
            this.SpinBt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.SpinBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SpinBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SpinBt.IsActive = false;
            this.SpinBt.LedSyncedBackColorEnable = true;
            this.SpinBt.Location = new System.Drawing.Point(274, 5);
            this.SpinBt.MultiSelectEn = true;
            this.SpinBt.Name = "SpinBt";
            this.SpinBt.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.SpinBt.OutLineEn = true;
            this.SpinBt.OutLineSize = 3F;
            this.SpinBt.ProgressBarColor = System.Drawing.Color.Empty;
            this.SpinBt.ProgressBarEnable = false;
            this.SpinBt.ProgressBarMaxValue = 100;
            this.SpinBt.ProgressBarMinValue = 0;
            this.SpinBt.ProgressBarSize = 5;
            this.SpinBt.ProgressBarValue = 0;
            this.SpinBt.Selectable = true;
            this.SpinBt.Selected = false;
            this.SpinBt.Size = new System.Drawing.Size(133, 50);
            this.SpinBt.StatusLedEnable = true;
            this.SpinBt.StatusLedSize = ((byte)(10));
            this.SpinBt.TabIndex = 61;
            this.SpinBt.TabStop = false;
            this.SpinBt.Text = "回転";
            this.SpinBt.UseVisualStyleBackColor = false;
            // 
            // DischargeBt
            // 
            this.DischargeBt.EditBox = null;
            this.DischargeBt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.DischargeBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DischargeBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DischargeBt.Image = ((System.Drawing.Image)(resources.GetObject("DischargeBt.Image")));
            this.DischargeBt.IsActive = false;
            this.DischargeBt.LedSyncedBackColorEnable = true;
            this.DischargeBt.Location = new System.Drawing.Point(5, 5);
            this.DischargeBt.MultiSelectEn = true;
            this.DischargeBt.Name = "DischargeBt";
            this.DischargeBt.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.DischargeBt.OutLineEn = true;
            this.DischargeBt.OutLineSize = 3F;
            this.DischargeBt.ProgressBarColor = System.Drawing.Color.Empty;
            this.DischargeBt.ProgressBarEnable = false;
            this.DischargeBt.ProgressBarMaxValue = 200;
            this.DischargeBt.ProgressBarMinValue = 0;
            this.DischargeBt.ProgressBarSize = 23;
            this.DischargeBt.ProgressBarValue = 0;
            this.DischargeBt.Selectable = true;
            this.DischargeBt.Selected = false;
            this.DischargeBt.Size = new System.Drawing.Size(132, 50);
            this.DischargeBt.StatusLedEnable = true;
            this.DischargeBt.StatusLedSize = ((byte)(10));
            this.DischargeBt.TabIndex = 59;
            this.DischargeBt.TabStop = false;
            this.DischargeBt.Text = "放電";
            this.DischargeBt.UseVisualStyleBackColor = false;
            // 
            // PompBt
            // 
            this.PompBt.EditBox = null;
            this.PompBt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.PompBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PompBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PompBt.IsActive = false;
            this.PompBt.LedSyncedBackColorEnable = true;
            this.PompBt.Location = new System.Drawing.Point(139, 5);
            this.PompBt.MultiSelectEn = true;
            this.PompBt.Name = "PompBt";
            this.PompBt.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.PompBt.OutLineEn = true;
            this.PompBt.OutLineSize = 3F;
            this.PompBt.ProgressBarColor = System.Drawing.Color.Empty;
            this.PompBt.ProgressBarEnable = false;
            this.PompBt.ProgressBarMaxValue = 100;
            this.PompBt.ProgressBarMinValue = 0;
            this.PompBt.ProgressBarSize = 5;
            this.PompBt.ProgressBarValue = 0;
            this.PompBt.Selectable = true;
            this.PompBt.Selected = false;
            this.PompBt.Size = new System.Drawing.Size(133, 50);
            this.PompBt.StatusLedEnable = true;
            this.PompBt.StatusLedSize = ((byte)(10));
            this.PompBt.TabIndex = 60;
            this.PompBt.TabStop = false;
            this.PompBt.Text = "ポンプ";
            this.PompBt.UseVisualStyleBackColor = false;
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
            // panel1
            // 
            this.panel1.Controls.Add(this.ReturnOriginBt);
            this.panel1.Controls.Add(this.AecBt);
            this.panel1.Controls.Add(this.ReferencingBt);
            this.panel1.Controls.Add(this.NumericFeedBt);
            this.panel1.Location = new System.Drawing.Point(5, 616);
            this.panel1.Name = "panel1";
            this.panel1.OutLineColor = System.Drawing.Color.DarkOliveGreen;
            this.panel1.OutLineSize = 2F;
            this.panel1.Size = new System.Drawing.Size(548, 82);
            this.panel1.TabIndex = 150;
            // 
            // ReturnOriginBt
            // 
            this.ReturnOriginBt.EditBox = null;
            this.ReturnOriginBt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.ReturnOriginBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ReturnOriginBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ReturnOriginBt.IsActive = false;
            this.ReturnOriginBt.LedSyncedBackColorEnable = false;
            this.ReturnOriginBt.Location = new System.Drawing.Point(5, 5);
            this.ReturnOriginBt.MultiSelectEn = false;
            this.ReturnOriginBt.Name = "ReturnOriginBt";
            this.ReturnOriginBt.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ReturnOriginBt.OutLineEn = true;
            this.ReturnOriginBt.OutLineSize = 3F;
            this.ReturnOriginBt.ProgressBarColor = System.Drawing.Color.Empty;
            this.ReturnOriginBt.ProgressBarEnable = false;
            this.ReturnOriginBt.ProgressBarMaxValue = 100;
            this.ReturnOriginBt.ProgressBarMinValue = 0;
            this.ReturnOriginBt.ProgressBarSize = 5;
            this.ReturnOriginBt.ProgressBarValue = 0;
            this.ReturnOriginBt.Selectable = true;
            this.ReturnOriginBt.Selected = false;
            this.ReturnOriginBt.Size = new System.Drawing.Size(133, 50);
            this.ReturnOriginBt.StatusLedEnable = true;
            this.ReturnOriginBt.StatusLedSize = ((byte)(10));
            this.ReturnOriginBt.TabIndex = 57;
            this.ReturnOriginBt.TabStop = false;
            this.ReturnOriginBt.Text = "座標登録";
            this.ReturnOriginBt.UseVisualStyleBackColor = false;
            this.ReturnOriginBt.Click += new System.EventHandler(this.ReturnToOriginBt_Click);
            // 
            // AecBt
            // 
            this.AecBt.EditBox = null;
            this.AecBt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.AecBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AecBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.AecBt.IsActive = false;
            this.AecBt.LedSyncedBackColorEnable = false;
            this.AecBt.Location = new System.Drawing.Point(140, 5);
            this.AecBt.MultiSelectEn = false;
            this.AecBt.Name = "AecBt";
            this.AecBt.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.AecBt.OutLineEn = true;
            this.AecBt.OutLineSize = 3F;
            this.AecBt.ProgressBarColor = System.Drawing.Color.Empty;
            this.AecBt.ProgressBarEnable = false;
            this.AecBt.ProgressBarMaxValue = 100;
            this.AecBt.ProgressBarMinValue = 0;
            this.AecBt.ProgressBarSize = 5;
            this.AecBt.ProgressBarValue = 0;
            this.AecBt.Selectable = true;
            this.AecBt.Selected = false;
            this.AecBt.Size = new System.Drawing.Size(133, 50);
            this.AecBt.StatusLedEnable = true;
            this.AecBt.StatusLedSize = ((byte)(10));
            this.AecBt.TabIndex = 58;
            this.AecBt.TabStop = false;
            this.AecBt.Text = "電極交換";
            this.AecBt.UseVisualStyleBackColor = false;
            this.AecBt.Click += new System.EventHandler(this.AECFormBt_Click);
            // 
            // ReferencingBt
            // 
            this.ReferencingBt.EditBox = null;
            this.ReferencingBt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.ReferencingBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ReferencingBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ReferencingBt.IsActive = false;
            this.ReferencingBt.LedSyncedBackColorEnable = true;
            this.ReferencingBt.Location = new System.Drawing.Point(275, 5);
            this.ReferencingBt.MultiSelectEn = false;
            this.ReferencingBt.Name = "ReferencingBt";
            this.ReferencingBt.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ReferencingBt.OutLineEn = true;
            this.ReferencingBt.OutLineSize = 3F;
            this.ReferencingBt.ProgressBarColor = System.Drawing.Color.Empty;
            this.ReferencingBt.ProgressBarEnable = false;
            this.ReferencingBt.ProgressBarMaxValue = 100;
            this.ReferencingBt.ProgressBarMinValue = 0;
            this.ReferencingBt.ProgressBarSize = 5;
            this.ReferencingBt.ProgressBarValue = 0;
            this.ReferencingBt.Selectable = true;
            this.ReferencingBt.Selected = false;
            this.ReferencingBt.Size = new System.Drawing.Size(133, 50);
            this.ReferencingBt.StatusLedEnable = false;
            this.ReferencingBt.StatusLedSize = ((byte)(10));
            this.ReferencingBt.TabIndex = 59;
            this.ReferencingBt.TabStop = false;
            this.ReferencingBt.Text = "位置出し";
            this.ReferencingBt.UseVisualStyleBackColor = false;
            this.ReferencingBt.Click += new System.EventHandler(this.ReferensingFormBt_Click);
            // 
            // NumericFeedBt
            // 
            this.NumericFeedBt.EditBox = null;
            this.NumericFeedBt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.NumericFeedBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NumericFeedBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.NumericFeedBt.IsActive = false;
            this.NumericFeedBt.LedSyncedBackColorEnable = true;
            this.NumericFeedBt.Location = new System.Drawing.Point(410, 5);
            this.NumericFeedBt.MultiSelectEn = false;
            this.NumericFeedBt.Name = "NumericFeedBt";
            this.NumericFeedBt.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.NumericFeedBt.OutLineEn = true;
            this.NumericFeedBt.OutLineSize = 3F;
            this.NumericFeedBt.ProgressBarColor = System.Drawing.Color.Empty;
            this.NumericFeedBt.ProgressBarEnable = false;
            this.NumericFeedBt.ProgressBarMaxValue = 100;
            this.NumericFeedBt.ProgressBarMinValue = 0;
            this.NumericFeedBt.ProgressBarSize = 5;
            this.NumericFeedBt.ProgressBarValue = 0;
            this.NumericFeedBt.Selectable = true;
            this.NumericFeedBt.Selected = false;
            this.NumericFeedBt.Size = new System.Drawing.Size(133, 50);
            this.NumericFeedBt.StatusLedEnable = false;
            this.NumericFeedBt.StatusLedSize = ((byte)(10));
            this.NumericFeedBt.TabIndex = 60;
            this.NumericFeedBt.TabStop = false;
            this.NumericFeedBt.Text = "数値位置決め";
            this.NumericFeedBt.UseVisualStyleBackColor = false;
            this.NumericFeedBt.Click += new System.EventHandler(this.NumericFeedFormBt_Click);
            // 
            // plot1
            // 
            this.plot1.Location = new System.Drawing.Point(560, 114);
            this.plot1.Name = "plot1";
            this.plot1.OutLineSize = 3;
            this.plot1.plotNotify = null;
            this.plot1.Size = new System.Drawing.Size(293, 174);
            this.plot1.TabIndex = 372;
            // 
            // panelEx1
            // 
            this.panelEx1.Controls.Add(this.FunctionBt);
            this.panelEx1.Font = new System.Drawing.Font("Meiryo UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.panelEx1.Location = new System.Drawing.Point(877, 7);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.OutLineColor = System.Drawing.Color.DarkOliveGreen;
            this.panelEx1.OutLineSize = 2F;
            this.panelEx1.Size = new System.Drawing.Size(171, 56);
            this.panelEx1.TabIndex = 371;
            // 
            // panelEx2
            // 
            this.panelEx2.Controls.Add(this.BuzzerBt);
            this.panelEx2.Controls.Add(this.ContactSensingBt);
            this.panelEx2.Controls.Add(this.InitialSetBt);
            this.panelEx2.Font = new System.Drawing.Font("Meiryo UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.panelEx2.Location = new System.Drawing.Point(432, 7);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.OutLineColor = System.Drawing.Color.DarkOliveGreen;
            this.panelEx2.OutLineSize = 2F;
            this.panelEx2.Size = new System.Drawing.Size(434, 56);
            this.panelEx2.TabIndex = 372;
            // 
            // _GraphPanel
            // 
            this._GraphPanel.Controls.Add(this.levelGage1);
            this._GraphPanel.Location = new System.Drawing.Point(428, 111);
            this._GraphPanel.Name = "_GraphPanel";
            this._GraphPanel.OutLineColor = System.Drawing.Color.DarkOliveGreen;
            this._GraphPanel.OutLineSize = 2F;
            this._GraphPanel.Size = new System.Drawing.Size(446, 187);
            this._GraphPanel.TabIndex = 373;
            // 
            // editProcessCondition1
            // 
            this.editProcessCondition1.CallingFunction = ECNC3.Views.EditProcessCondition.CallingFunctions.Manual;
            this.editProcessCondition1.CurrentProcessConditionNumber = 0;
            this.editProcessCondition1.Location = new System.Drawing.Point(3, 501);
            this.editProcessCondition1.Name = "editProcessCondition1";
            this.editProcessCondition1.Protect = 0;
            this.editProcessCondition1.Size = new System.Drawing.Size(1022, 114);
            this.editProcessCondition1.TabIndex = 330;
            // 
            // MANUALForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 678);
            this.ControlBox = false;
            this.Controls.Add(this.plot1);
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ConditionsFormOpenBt);
            this.Controls.Add(this.PnumTextBox);
            this.Controls.Add(this._waxisSettingPanelEx);
            this.Controls.Add(this.PTextBox);
            this.Controls.Add(this._timerPanelEx);
            this.Controls.Add(this.axisMonitor2);
            this.Controls.Add(this._GraphPanel);
            this.Controls.Add(this.editProcessCondition1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(0, 90);
            this.Name = "MANUALForm";
            this.OutLineSize = 3;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Activated += new System.EventHandler(this.MANUALForm_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MANUALForm_FormClosed);
            this.Load += new System.EventHandler(this.MANUALForm_Load);
            this.VisibleChanged += new System.EventHandler(this.MANUALForm_VisibleChanged);
            this._timerPanelEx.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEx2)).EndInit();
            this._waxisSettingPanelEx.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEx1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            this._GraphPanel.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private ButtonEx BuzzerBt;
		private ButtonEx InitialSetBt;
		private ButtonEx ContactSensingBt;
		private ButtonEx WAxisUpperSetBt;
		private ButtonEx ReturnOriginBt;
		private ButtonEx AecBt;
		private ButtonEx ReferencingBt;
		private ButtonEx NumericFeedBt;
		private ButtonEx ConditionsFormOpenBt;
		private ButtonEx FunctionBt;
		private ECNC3.Views.PanelEx panel1;
		private EditProcessCondition editProcessCondition1;
		private AxisMonitor axisMonitor2;
		private ECNC3.Views.PanelEx panel2;
		private ButtonEx SpinBt;
		private ButtonEx DischargeBt;
		private ButtonEx PompBt;
		private LabelEx PnumTextBox;
		private UserControls.LevelGage levelGage1;
		private ECNC3.Views.PanelEx _timerPanelEx;
		private LabelEx ProcTimerTextBox;
		private ButtonEx _oneProcessingTimeBt;
		private LabelEx DischargeTimerTextBox;
		private LabelEx MachineTimerTextBox;
		private ButtonEx _processingTimeBt;
		private ButtonEx _programRunTimeBt;
		private PictureBoxEx pictureBoxEx2;
		private ECNC3.Views.PanelEx _waxisSettingPanelEx;
		private PictureBoxEx pictureBoxEx1;
		private LogForm plot1;
		private ButtonEx WAxisLowerSetBt;
		private LabelEx WAxisLowerVal;
		private LabelEx WAxisUpperVal;
		private LabelEx WAxisUpperUnitLabel;
		private LabelEx WAxisLowerUnitLabel;
		private PanelEx panelEx1;
		private PanelEx panelEx2;
		private LabelEx PTextBox;
        private PanelEx _GraphPanel;
    }
}
