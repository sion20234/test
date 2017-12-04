﻿namespace ECNC3.Views
{
	partial class TenKeyDialog
	{
		/// <summary>
		///ポップアップテンキー
		/// </summary>


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
            this.buttonEx_Title = new ECNC3.Views.ButtonEx();
            this.buttonEx3 = new ECNC3.Views.ButtonEx();
            this.buttonEx2 = new ECNC3.Views.ButtonEx();
            this._keyDisplay2 = new ECNC3.Views.DecimalTextBox();
            this._keyDisplay = new ECNC3.Views.NumericTextBox();
            this.InputModeChange = new ECNC3.Views.ButtonEx();
            this.buttonEx1 = new ECNC3.Views.ButtonEx();
            this.buttonEx_Up = new ECNC3.Views.ButtonEx();
            this.buttonEx_Dn = new ECNC3.Views.ButtonEx();
            this.BackSpaceKey = new ECNC3.Views.ButtonEx();
            this.button_Plus = new ECNC3.Views.ButtonEx();
            this.button_asterisk = new ECNC3.Views.ButtonEx();
            this.button_division = new ECNC3.Views.ButtonEx();
            this.button_dot = new ECNC3.Views.ButtonEx();
            this.EnterKey = new ECNC3.Views.ButtonEx();
            this.button_9 = new ECNC3.Views.ButtonEx();
            this.button_8 = new ECNC3.Views.ButtonEx();
            this.button_7 = new ECNC3.Views.ButtonEx();
            this.button_6 = new ECNC3.Views.ButtonEx();
            this.AllClearKey = new ECNC3.Views.ButtonEx();
            this.button_3 = new ECNC3.Views.ButtonEx();
            this.button_2 = new ECNC3.Views.ButtonEx();
            this.button_0 = new ECNC3.Views.ButtonEx();
            this.button_1 = new ECNC3.Views.ButtonEx();
            this.button_5 = new ECNC3.Views.ButtonEx();
            this.button_4 = new ECNC3.Views.ButtonEx();
            this.button_Minus = new ECNC3.Views.ButtonEx();
            this._btnCANCEL = new ECNC3.Views.ButtonEx();
            this._btnOK = new ECNC3.Views.ButtonEx();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonEx_Title
            // 
            this.buttonEx_Title.EditBox = null;
            this.buttonEx_Title.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonEx_Title.IsActive = false;
            this.buttonEx_Title.LedSyncedBackColorEnable = true;
            this.buttonEx_Title.Location = new System.Drawing.Point(9, 5);
            this.buttonEx_Title.MultiSelectEn = false;
            this.buttonEx_Title.Name = "buttonEx_Title";
            this.buttonEx_Title.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.buttonEx_Title.OutLineEn = true;
            this.buttonEx_Title.OutLineSize = 3F;
            this.buttonEx_Title.ProgressBarColor = System.Drawing.Color.Empty;
            this.buttonEx_Title.ProgressBarEnable = false;
            this.buttonEx_Title.ProgressBarMaxValue = 100;
            this.buttonEx_Title.ProgressBarMinValue = 0;
            this.buttonEx_Title.ProgressBarSize = 5;
            this.buttonEx_Title.ProgressBarValue = 0;
            this.buttonEx_Title.Selectable = false;
            this.buttonEx_Title.Selected = false;
            this.buttonEx_Title.Size = new System.Drawing.Size(296, 40);
            this.buttonEx_Title.StatusLedEnable = false;
            this.buttonEx_Title.StatusLedSize = ((byte)(10));
            this.buttonEx_Title.TabIndex = 31;
            this.buttonEx_Title.Text = "TITLE";
            this.buttonEx_Title.UseVisualStyleBackColor = false;
            this.buttonEx_Title.Click += new System.EventHandler(this.buttonEx_Title_Click);
            this.buttonEx_Title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonEx_Title_MouseDown);
            this.buttonEx_Title.MouseMove += new System.Windows.Forms.MouseEventHandler(this.buttonEx_Title_MouseMove);
            // 
            // buttonEx3
            // 
            this.buttonEx3.EditBox = null;
            this.buttonEx3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEx3.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonEx3.IsActive = false;
            this.buttonEx3.LedSyncedBackColorEnable = true;
            this.buttonEx3.Location = new System.Drawing.Point(172, 51);
            this.buttonEx3.MultiSelectEn = false;
            this.buttonEx3.Name = "buttonEx3";
            this.buttonEx3.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.buttonEx3.OutLineEn = true;
            this.buttonEx3.OutLineSize = 3F;
            this.buttonEx3.ProgressBarColor = System.Drawing.Color.Empty;
            this.buttonEx3.ProgressBarEnable = false;
            this.buttonEx3.ProgressBarMaxValue = 100;
            this.buttonEx3.ProgressBarMinValue = 0;
            this.buttonEx3.ProgressBarSize = 5;
            this.buttonEx3.ProgressBarValue = 0;
            this.buttonEx3.Selectable = true;
            this.buttonEx3.Selected = false;
            this.buttonEx3.Size = new System.Drawing.Size(133, 43);
            this.buttonEx3.StatusLedEnable = false;
            this.buttonEx3.StatusLedSize = ((byte)(10));
            this.buttonEx3.TabIndex = 30;
            this.buttonEx3.Text = "テンキー";
            this.buttonEx3.UseVisualStyleBackColor = true;
            this.buttonEx3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttton_InputMode_Tenkey);
            // 
            // buttonEx2
            // 
            this.buttonEx2.EditBox = null;
            this.buttonEx2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEx2.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonEx2.IsActive = false;
            this.buttonEx2.LedSyncedBackColorEnable = true;
            this.buttonEx2.Location = new System.Drawing.Point(215, 51);
            this.buttonEx2.MultiSelectEn = false;
            this.buttonEx2.Name = "buttonEx2";
            this.buttonEx2.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.buttonEx2.OutLineEn = true;
            this.buttonEx2.OutLineSize = 3F;
            this.buttonEx2.ProgressBarColor = System.Drawing.Color.Empty;
            this.buttonEx2.ProgressBarEnable = false;
            this.buttonEx2.ProgressBarMaxValue = 100;
            this.buttonEx2.ProgressBarMinValue = 0;
            this.buttonEx2.ProgressBarSize = 5;
            this.buttonEx2.ProgressBarValue = 0;
            this.buttonEx2.Selectable = true;
            this.buttonEx2.Selected = false;
            this.buttonEx2.Size = new System.Drawing.Size(90, 43);
            this.buttonEx2.StatusLedEnable = false;
            this.buttonEx2.StatusLedSize = ((byte)(10));
            this.buttonEx2.TabIndex = 29;
            this.buttonEx2.Text = "両方";
            this.buttonEx2.UseVisualStyleBackColor = true;
            this.buttonEx2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttton_InputMode_Both);
            // 
            // _keyDisplay2
            // 
            this._keyDisplay2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
            this._keyDisplay2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._keyDisplay2.Font = new System.Drawing.Font("Meiryo UI", 18F);
            this._keyDisplay2.ForeColor = System.Drawing.Color.Gainsboro;
            this._keyDisplay2.Location = new System.Drawing.Point(9, 101);
            this._keyDisplay2.LowerLimit = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this._keyDisplay2.Name = "_keyDisplay2";
            this._keyDisplay2.RawText = "000";
            this._keyDisplay2.Size = new System.Drawing.Size(296, 38);
            this._keyDisplay2.TabIndex = 28;
            this._keyDisplay2.Text = "000";
            this._keyDisplay2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this._keyDisplay2.UpperLimit = new decimal(new int[] {
            99999999,
            0,
            0,
            -2147483648});
            this._keyDisplay2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._keyDisplay2_KeyPress);
            // 
            // _keyDisplay
            // 
            this._keyDisplay.AcceptsReturn = true;
            this._keyDisplay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
            this._keyDisplay.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._keyDisplay.Font = new System.Drawing.Font("Meiryo UI", 18F);
            this._keyDisplay.ForeColor = System.Drawing.Color.Gainsboro;
            this._keyDisplay.FormatType = ECNC3.Views.NumericTextBox.FormatTypes.Free;
            this._keyDisplay.InputDataRatio = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._keyDisplay.Location = new System.Drawing.Point(281, 68);
            this._keyDisplay.LowerLimit = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this._keyDisplay.Margin = new System.Windows.Forms.Padding(0);
            this._keyDisplay.Name = "_keyDisplay";
            this._keyDisplay.RawText = "0";
            this._keyDisplay.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._keyDisplay.Size = new System.Drawing.Size(20, 31);
            this._keyDisplay.TabIndex = 27;
            this._keyDisplay.Text = "0";
            this._keyDisplay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this._keyDisplay.UpperLimit = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._keyDisplay.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._keyDisplay.Visible = false;
            // 
            // InputModeChange
            // 
            this.InputModeChange.EditBox = null;
            this.InputModeChange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.InputModeChange.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.InputModeChange.IsActive = false;
            this.InputModeChange.LedSyncedBackColorEnable = true;
            this.InputModeChange.Location = new System.Drawing.Point(10, 51);
            this.InputModeChange.MultiSelectEn = false;
            this.InputModeChange.Name = "InputModeChange";
            this.InputModeChange.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.InputModeChange.OutLineEn = true;
            this.InputModeChange.OutLineSize = 3F;
            this.InputModeChange.ProgressBarColor = System.Drawing.Color.Empty;
            this.InputModeChange.ProgressBarEnable = false;
            this.InputModeChange.ProgressBarMaxValue = 100;
            this.InputModeChange.ProgressBarMinValue = 0;
            this.InputModeChange.ProgressBarSize = 5;
            this.InputModeChange.ProgressBarValue = 0;
            this.InputModeChange.Selectable = true;
            this.InputModeChange.Selected = false;
            this.InputModeChange.Size = new System.Drawing.Size(133, 43);
            this.InputModeChange.StatusLedEnable = false;
            this.InputModeChange.StatusLedSize = ((byte)(10));
            this.InputModeChange.TabIndex = 8;
            this.InputModeChange.Text = "▲▼キー";
            this.InputModeChange.UseVisualStyleBackColor = true;
            this.InputModeChange.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttton_InputMode_UpDn);
            // 
            // buttonEx1
            // 
            this.buttonEx1.EditBox = null;
            this.buttonEx1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEx1.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonEx1.IsActive = false;
            this.buttonEx1.LedSyncedBackColorEnable = true;
            this.buttonEx1.Location = new System.Drawing.Point(216, 495);
            this.buttonEx1.MultiSelectEn = false;
            this.buttonEx1.Name = "buttonEx1";
            this.buttonEx1.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.buttonEx1.OutLineEn = true;
            this.buttonEx1.OutLineSize = 3F;
            this.buttonEx1.ProgressBarColor = System.Drawing.Color.Empty;
            this.buttonEx1.ProgressBarEnable = false;
            this.buttonEx1.ProgressBarMaxValue = 100;
            this.buttonEx1.ProgressBarMinValue = 0;
            this.buttonEx1.ProgressBarSize = 5;
            this.buttonEx1.ProgressBarValue = 0;
            this.buttonEx1.Selectable = true;
            this.buttonEx1.Selected = false;
            this.buttonEx1.Size = new System.Drawing.Size(90, 39);
            this.buttonEx1.StatusLedEnable = false;
            this.buttonEx1.StatusLedSize = ((byte)(10));
            this.buttonEx1.TabIndex = 7;
            this.buttonEx1.Text = "閉じる";
            this.buttonEx1.UseVisualStyleBackColor = true;
            this.buttonEx1.MouseUp += new System.Windows.Forms.MouseEventHandler(this._btnCANCEL_Click);
            // 
            // buttonEx_Up
            // 
            this.buttonEx_Up.EditBox = null;
            this.buttonEx_Up.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.buttonEx_Up.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEx_Up.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonEx_Up.IsActive = false;
            this.buttonEx_Up.LedSyncedBackColorEnable = true;
            this.buttonEx_Up.Location = new System.Drawing.Point(10, 372);
            this.buttonEx_Up.MultiSelectEn = false;
            this.buttonEx_Up.Name = "buttonEx_Up";
            this.buttonEx_Up.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.buttonEx_Up.OutLineEn = true;
            this.buttonEx_Up.OutLineSize = 3F;
            this.buttonEx_Up.ProgressBarColor = System.Drawing.Color.Empty;
            this.buttonEx_Up.ProgressBarEnable = false;
            this.buttonEx_Up.ProgressBarMaxValue = 100;
            this.buttonEx_Up.ProgressBarMinValue = 0;
            this.buttonEx_Up.ProgressBarSize = 5;
            this.buttonEx_Up.ProgressBarValue = 0;
            this.buttonEx_Up.Selectable = false;
            this.buttonEx_Up.Selected = false;
            this.buttonEx_Up.Size = new System.Drawing.Size(296, 52);
            this.buttonEx_Up.StatusLedEnable = false;
            this.buttonEx_Up.StatusLedSize = ((byte)(10));
            this.buttonEx_Up.TabIndex = 2;
            this.buttonEx_Up.Text = "▲";
            this.buttonEx_Up.UseVisualStyleBackColor = false;
            this.buttonEx_Up.Click += new System.EventHandler(this.buttonArrowkeyUp_Click);
            this.buttonEx_Up.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonArrowkeyUp_MouseDn);
            this.buttonEx_Up.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonArrowkeyUp_MouseUp);
            // 
            // buttonEx_Dn
            // 
            this.buttonEx_Dn.EditBox = null;
            this.buttonEx_Dn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.buttonEx_Dn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEx_Dn.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonEx_Dn.IsActive = false;
            this.buttonEx_Dn.LedSyncedBackColorEnable = true;
            this.buttonEx_Dn.Location = new System.Drawing.Point(10, 432);
            this.buttonEx_Dn.MultiSelectEn = false;
            this.buttonEx_Dn.Name = "buttonEx_Dn";
            this.buttonEx_Dn.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.buttonEx_Dn.OutLineEn = true;
            this.buttonEx_Dn.OutLineSize = 3F;
            this.buttonEx_Dn.ProgressBarColor = System.Drawing.Color.Empty;
            this.buttonEx_Dn.ProgressBarEnable = false;
            this.buttonEx_Dn.ProgressBarMaxValue = 100;
            this.buttonEx_Dn.ProgressBarMinValue = 0;
            this.buttonEx_Dn.ProgressBarSize = 5;
            this.buttonEx_Dn.ProgressBarValue = 0;
            this.buttonEx_Dn.Selectable = false;
            this.buttonEx_Dn.Selected = false;
            this.buttonEx_Dn.Size = new System.Drawing.Size(295, 52);
            this.buttonEx_Dn.StatusLedEnable = false;
            this.buttonEx_Dn.StatusLedSize = ((byte)(10));
            this.buttonEx_Dn.TabIndex = 4;
            this.buttonEx_Dn.Text = "▼";
            this.buttonEx_Dn.UseVisualStyleBackColor = false;
            this.buttonEx_Dn.Click += new System.EventHandler(this.buttonArrowkeyDn_Click);
            this.buttonEx_Dn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonArrowkeyDn_MouseDn);
            this.buttonEx_Dn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonArrowkeyDn_MouseUp);
            // 
            // BackSpaceKey
            // 
            this.BackSpaceKey.EditBox = null;
            this.BackSpaceKey.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BackSpaceKey.IsActive = false;
            this.BackSpaceKey.LedSyncedBackColorEnable = true;
            this.BackSpaceKey.Location = new System.Drawing.Point(187, 203);
            this.BackSpaceKey.MultiSelectEn = false;
            this.BackSpaceKey.Name = "BackSpaceKey";
            this.BackSpaceKey.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BackSpaceKey.OutLineEn = true;
            this.BackSpaceKey.OutLineSize = 3F;
            this.BackSpaceKey.ProgressBarColor = System.Drawing.Color.Empty;
            this.BackSpaceKey.ProgressBarEnable = false;
            this.BackSpaceKey.ProgressBarMaxValue = 100;
            this.BackSpaceKey.ProgressBarMinValue = 0;
            this.BackSpaceKey.ProgressBarSize = 5;
            this.BackSpaceKey.ProgressBarValue = 0;
            this.BackSpaceKey.Selectable = false;
            this.BackSpaceKey.Selected = false;
            this.BackSpaceKey.Size = new System.Drawing.Size(50, 50);
            this.BackSpaceKey.StatusLedEnable = false;
            this.BackSpaceKey.StatusLedSize = ((byte)(10));
            this.BackSpaceKey.TabIndex = 17;
            this.BackSpaceKey.Text = "BS";
            this.BackSpaceKey.UseVisualStyleBackColor = false;
            this.BackSpaceKey.Click += new System.EventHandler(this.BackSpacerKey_Click);
            // 
            // button_Plus
            // 
            this.button_Plus.EditBox = null;
            this.button_Plus.Enabled = false;
            this.button_Plus.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_Plus.IsActive = false;
            this.button_Plus.LedSyncedBackColorEnable = true;
            this.button_Plus.Location = new System.Drawing.Point(243, 203);
            this.button_Plus.MultiSelectEn = false;
            this.button_Plus.Name = "button_Plus";
            this.button_Plus.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button_Plus.OutLineEn = true;
            this.button_Plus.OutLineSize = 3F;
            this.button_Plus.ProgressBarColor = System.Drawing.Color.Empty;
            this.button_Plus.ProgressBarEnable = false;
            this.button_Plus.ProgressBarMaxValue = 100;
            this.button_Plus.ProgressBarMinValue = 0;
            this.button_Plus.ProgressBarSize = 5;
            this.button_Plus.ProgressBarValue = 0;
            this.button_Plus.Selectable = false;
            this.button_Plus.Selected = false;
            this.button_Plus.Size = new System.Drawing.Size(50, 50);
            this.button_Plus.StatusLedEnable = false;
            this.button_Plus.StatusLedSize = ((byte)(10));
            this.button_Plus.TabIndex = 18;
            this.button_Plus.Text = "+";
            this.button_Plus.UseVisualStyleBackColor = false;
            this.button_Plus.Click += new System.EventHandler(this.button_PlusClick);
            // 
            // button_asterisk
            // 
            this.button_asterisk.EditBox = null;
            this.button_asterisk.Enabled = false;
            this.button_asterisk.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_asterisk.IsActive = false;
            this.button_asterisk.LedSyncedBackColorEnable = true;
            this.button_asterisk.Location = new System.Drawing.Point(188, 315);
            this.button_asterisk.MultiSelectEn = false;
            this.button_asterisk.Name = "button_asterisk";
            this.button_asterisk.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button_asterisk.OutLineEn = true;
            this.button_asterisk.OutLineSize = 3F;
            this.button_asterisk.ProgressBarColor = System.Drawing.Color.Empty;
            this.button_asterisk.ProgressBarEnable = false;
            this.button_asterisk.ProgressBarMaxValue = 100;
            this.button_asterisk.ProgressBarMinValue = 0;
            this.button_asterisk.ProgressBarSize = 5;
            this.button_asterisk.ProgressBarValue = 0;
            this.button_asterisk.Selectable = false;
            this.button_asterisk.Selected = false;
            this.button_asterisk.Size = new System.Drawing.Size(50, 50);
            this.button_asterisk.StatusLedEnable = false;
            this.button_asterisk.StatusLedSize = ((byte)(10));
            this.button_asterisk.TabIndex = 26;
            this.button_asterisk.Text = "*";
            this.button_asterisk.UseVisualStyleBackColor = false;
            this.button_asterisk.Click += new System.EventHandler(this.button46_Click);
            // 
            // button_division
            // 
            this.button_division.EditBox = null;
            this.button_division.Enabled = false;
            this.button_division.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_division.IsActive = false;
            this.button_division.LedSyncedBackColorEnable = true;
            this.button_division.Location = new System.Drawing.Point(187, 259);
            this.button_division.MultiSelectEn = false;
            this.button_division.Name = "button_division";
            this.button_division.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button_division.OutLineEn = true;
            this.button_division.OutLineSize = 3F;
            this.button_division.ProgressBarColor = System.Drawing.Color.Empty;
            this.button_division.ProgressBarEnable = false;
            this.button_division.ProgressBarMaxValue = 100;
            this.button_division.ProgressBarMinValue = 0;
            this.button_division.ProgressBarSize = 5;
            this.button_division.ProgressBarValue = 0;
            this.button_division.Selectable = false;
            this.button_division.Selected = false;
            this.button_division.Size = new System.Drawing.Size(50, 50);
            this.button_division.StatusLedEnable = false;
            this.button_division.StatusLedSize = ((byte)(10));
            this.button_division.TabIndex = 22;
            this.button_division.Text = "/";
            this.button_division.UseVisualStyleBackColor = false;
            this.button_division.Click += new System.EventHandler(this.button48_Click);
            // 
            // button_dot
            // 
            this.button_dot.EditBox = null;
            this.button_dot.Enabled = false;
            this.button_dot.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_dot.IsActive = false;
            this.button_dot.LedSyncedBackColorEnable = true;
            this.button_dot.Location = new System.Drawing.Point(132, 315);
            this.button_dot.MultiSelectEn = false;
            this.button_dot.Name = "button_dot";
            this.button_dot.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button_dot.OutLineEn = true;
            this.button_dot.OutLineSize = 3F;
            this.button_dot.ProgressBarColor = System.Drawing.Color.Empty;
            this.button_dot.ProgressBarEnable = false;
            this.button_dot.ProgressBarMaxValue = 100;
            this.button_dot.ProgressBarMinValue = 0;
            this.button_dot.ProgressBarSize = 5;
            this.button_dot.ProgressBarValue = 0;
            this.button_dot.Selectable = false;
            this.button_dot.Selected = false;
            this.button_dot.Size = new System.Drawing.Size(50, 50);
            this.button_dot.StatusLedEnable = false;
            this.button_dot.StatusLedSize = ((byte)(10));
            this.button_dot.TabIndex = 25;
            this.button_dot.Text = ".";
            this.button_dot.UseVisualStyleBackColor = false;
            this.button_dot.Click += new System.EventHandler(this.button_dot_Click);
            // 
            // EnterKey
            // 
            this.EnterKey.EditBox = null;
            this.EnterKey.Enabled = false;
            this.EnterKey.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.EnterKey.IsActive = false;
            this.EnterKey.LedSyncedBackColorEnable = true;
            this.EnterKey.Location = new System.Drawing.Point(243, 259);
            this.EnterKey.MultiSelectEn = false;
            this.EnterKey.Name = "EnterKey";
            this.EnterKey.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.EnterKey.OutLineEn = true;
            this.EnterKey.OutLineSize = 3F;
            this.EnterKey.ProgressBarColor = System.Drawing.Color.Empty;
            this.EnterKey.ProgressBarEnable = false;
            this.EnterKey.ProgressBarMaxValue = 100;
            this.EnterKey.ProgressBarMinValue = 0;
            this.EnterKey.ProgressBarSize = 5;
            this.EnterKey.ProgressBarValue = 0;
            this.EnterKey.Selectable = false;
            this.EnterKey.Selected = false;
            this.EnterKey.Size = new System.Drawing.Size(50, 106);
            this.EnterKey.StatusLedEnable = false;
            this.EnterKey.StatusLedSize = ((byte)(10));
            this.EnterKey.TabIndex = 23;
            this.EnterKey.Text = "Enter";
            this.EnterKey.UseVisualStyleBackColor = false;
            this.EnterKey.Click += new System.EventHandler(this.EnterKey_Click);
            // 
            // button_9
            // 
            this.button_9.EditBox = null;
            this.button_9.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_9.IsActive = false;
            this.button_9.LedSyncedBackColorEnable = true;
            this.button_9.Location = new System.Drawing.Point(132, 147);
            this.button_9.MultiSelectEn = false;
            this.button_9.Name = "button_9";
            this.button_9.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button_9.OutLineEn = true;
            this.button_9.OutLineSize = 3F;
            this.button_9.ProgressBarColor = System.Drawing.Color.Empty;
            this.button_9.ProgressBarEnable = false;
            this.button_9.ProgressBarMaxValue = 100;
            this.button_9.ProgressBarMinValue = 0;
            this.button_9.ProgressBarSize = 5;
            this.button_9.ProgressBarValue = 0;
            this.button_9.Selectable = false;
            this.button_9.Selected = false;
            this.button_9.Size = new System.Drawing.Size(50, 50);
            this.button_9.StatusLedEnable = false;
            this.button_9.StatusLedSize = ((byte)(10));
            this.button_9.TabIndex = 11;
            this.button_9.Text = "9";
            this.button_9.UseVisualStyleBackColor = false;
            this.button_9.Click += new System.EventHandler(this.buttonTenkey_Click);
            // 
            // button_8
            // 
            this.button_8.EditBox = null;
            this.button_8.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_8.IsActive = false;
            this.button_8.LedSyncedBackColorEnable = true;
            this.button_8.Location = new System.Drawing.Point(76, 147);
            this.button_8.MultiSelectEn = false;
            this.button_8.Name = "button_8";
            this.button_8.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button_8.OutLineEn = true;
            this.button_8.OutLineSize = 3F;
            this.button_8.ProgressBarColor = System.Drawing.Color.Empty;
            this.button_8.ProgressBarEnable = false;
            this.button_8.ProgressBarMaxValue = 100;
            this.button_8.ProgressBarMinValue = 0;
            this.button_8.ProgressBarSize = 5;
            this.button_8.ProgressBarValue = 0;
            this.button_8.Selectable = false;
            this.button_8.Selected = false;
            this.button_8.Size = new System.Drawing.Size(50, 50);
            this.button_8.StatusLedEnable = false;
            this.button_8.StatusLedSize = ((byte)(10));
            this.button_8.TabIndex = 10;
            this.button_8.Text = "8";
            this.button_8.UseVisualStyleBackColor = false;
            this.button_8.Click += new System.EventHandler(this.buttonTenkey_Click);
            // 
            // button_7
            // 
            this.button_7.EditBox = null;
            this.button_7.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_7.IsActive = false;
            this.button_7.LedSyncedBackColorEnable = true;
            this.button_7.Location = new System.Drawing.Point(20, 147);
            this.button_7.MultiSelectEn = false;
            this.button_7.Name = "button_7";
            this.button_7.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button_7.OutLineEn = true;
            this.button_7.OutLineSize = 3F;
            this.button_7.ProgressBarColor = System.Drawing.Color.Empty;
            this.button_7.ProgressBarEnable = false;
            this.button_7.ProgressBarMaxValue = 100;
            this.button_7.ProgressBarMinValue = 0;
            this.button_7.ProgressBarSize = 5;
            this.button_7.ProgressBarValue = 0;
            this.button_7.Selectable = false;
            this.button_7.Selected = false;
            this.button_7.Size = new System.Drawing.Size(50, 50);
            this.button_7.StatusLedEnable = false;
            this.button_7.StatusLedSize = ((byte)(10));
            this.button_7.TabIndex = 9;
            this.button_7.Text = "7";
            this.button_7.UseVisualStyleBackColor = false;
            this.button_7.Click += new System.EventHandler(this.buttonTenkey_Click);
            // 
            // button_6
            // 
            this.button_6.EditBox = null;
            this.button_6.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_6.IsActive = false;
            this.button_6.LedSyncedBackColorEnable = true;
            this.button_6.Location = new System.Drawing.Point(132, 203);
            this.button_6.MultiSelectEn = false;
            this.button_6.Name = "button_6";
            this.button_6.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button_6.OutLineEn = true;
            this.button_6.OutLineSize = 3F;
            this.button_6.ProgressBarColor = System.Drawing.Color.Empty;
            this.button_6.ProgressBarEnable = false;
            this.button_6.ProgressBarMaxValue = 100;
            this.button_6.ProgressBarMinValue = 0;
            this.button_6.ProgressBarSize = 5;
            this.button_6.ProgressBarValue = 0;
            this.button_6.Selectable = false;
            this.button_6.Selected = false;
            this.button_6.Size = new System.Drawing.Size(50, 50);
            this.button_6.StatusLedEnable = false;
            this.button_6.StatusLedSize = ((byte)(10));
            this.button_6.TabIndex = 16;
            this.button_6.Text = "6";
            this.button_6.UseVisualStyleBackColor = false;
            this.button_6.Click += new System.EventHandler(this.buttonTenkey_Click);
            // 
            // AllClearKey
            // 
            this.AllClearKey.EditBox = null;
            this.AllClearKey.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.AllClearKey.IsActive = false;
            this.AllClearKey.LedSyncedBackColorEnable = true;
            this.AllClearKey.Location = new System.Drawing.Point(188, 147);
            this.AllClearKey.MultiSelectEn = false;
            this.AllClearKey.Name = "AllClearKey";
            this.AllClearKey.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.AllClearKey.OutLineEn = true;
            this.AllClearKey.OutLineSize = 3F;
            this.AllClearKey.ProgressBarColor = System.Drawing.Color.Empty;
            this.AllClearKey.ProgressBarEnable = false;
            this.AllClearKey.ProgressBarMaxValue = 100;
            this.AllClearKey.ProgressBarMinValue = 0;
            this.AllClearKey.ProgressBarSize = 5;
            this.AllClearKey.ProgressBarValue = 0;
            this.AllClearKey.Selectable = false;
            this.AllClearKey.Selected = false;
            this.AllClearKey.Size = new System.Drawing.Size(50, 50);
            this.AllClearKey.StatusLedEnable = false;
            this.AllClearKey.StatusLedSize = ((byte)(10));
            this.AllClearKey.TabIndex = 12;
            this.AllClearKey.Text = "AC";
            this.AllClearKey.UseVisualStyleBackColor = false;
            this.AllClearKey.Click += new System.EventHandler(this.AllClearKey_Click);
            // 
            // button_3
            // 
            this.button_3.EditBox = null;
            this.button_3.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_3.IsActive = false;
            this.button_3.LedSyncedBackColorEnable = true;
            this.button_3.Location = new System.Drawing.Point(132, 259);
            this.button_3.MultiSelectEn = false;
            this.button_3.Name = "button_3";
            this.button_3.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button_3.OutLineEn = true;
            this.button_3.OutLineSize = 3F;
            this.button_3.ProgressBarColor = System.Drawing.Color.Empty;
            this.button_3.ProgressBarEnable = false;
            this.button_3.ProgressBarMaxValue = 100;
            this.button_3.ProgressBarMinValue = 0;
            this.button_3.ProgressBarSize = 5;
            this.button_3.ProgressBarValue = 0;
            this.button_3.Selectable = false;
            this.button_3.Selected = false;
            this.button_3.Size = new System.Drawing.Size(50, 50);
            this.button_3.StatusLedEnable = false;
            this.button_3.StatusLedSize = ((byte)(10));
            this.button_3.TabIndex = 21;
            this.button_3.Text = "3";
            this.button_3.UseVisualStyleBackColor = false;
            this.button_3.Click += new System.EventHandler(this.buttonTenkey_Click);
            // 
            // button_2
            // 
            this.button_2.EditBox = null;
            this.button_2.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_2.IsActive = false;
            this.button_2.LedSyncedBackColorEnable = true;
            this.button_2.Location = new System.Drawing.Point(76, 260);
            this.button_2.MultiSelectEn = false;
            this.button_2.Name = "button_2";
            this.button_2.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button_2.OutLineEn = true;
            this.button_2.OutLineSize = 3F;
            this.button_2.ProgressBarColor = System.Drawing.Color.Empty;
            this.button_2.ProgressBarEnable = false;
            this.button_2.ProgressBarMaxValue = 100;
            this.button_2.ProgressBarMinValue = 0;
            this.button_2.ProgressBarSize = 5;
            this.button_2.ProgressBarValue = 0;
            this.button_2.Selectable = false;
            this.button_2.Selected = false;
            this.button_2.Size = new System.Drawing.Size(50, 50);
            this.button_2.StatusLedEnable = false;
            this.button_2.StatusLedSize = ((byte)(10));
            this.button_2.TabIndex = 20;
            this.button_2.Text = "2";
            this.button_2.UseVisualStyleBackColor = false;
            this.button_2.Click += new System.EventHandler(this.buttonTenkey_Click);
            // 
            // button_0
            // 
            this.button_0.EditBox = null;
            this.button_0.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_0.IsActive = false;
            this.button_0.LedSyncedBackColorEnable = true;
            this.button_0.Location = new System.Drawing.Point(20, 315);
            this.button_0.MultiSelectEn = false;
            this.button_0.Name = "button_0";
            this.button_0.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button_0.OutLineEn = true;
            this.button_0.OutLineSize = 3F;
            this.button_0.ProgressBarColor = System.Drawing.Color.Empty;
            this.button_0.ProgressBarEnable = false;
            this.button_0.ProgressBarMaxValue = 100;
            this.button_0.ProgressBarMinValue = 0;
            this.button_0.ProgressBarSize = 5;
            this.button_0.ProgressBarValue = 0;
            this.button_0.Selectable = false;
            this.button_0.Selected = false;
            this.button_0.Size = new System.Drawing.Size(106, 50);
            this.button_0.StatusLedEnable = false;
            this.button_0.StatusLedSize = ((byte)(10));
            this.button_0.TabIndex = 24;
            this.button_0.Text = "0";
            this.button_0.UseVisualStyleBackColor = false;
            this.button_0.Click += new System.EventHandler(this.buttonTenkey_Click);
            // 
            // button_1
            // 
            this.button_1.EditBox = null;
            this.button_1.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_1.IsActive = false;
            this.button_1.LedSyncedBackColorEnable = true;
            this.button_1.Location = new System.Drawing.Point(20, 259);
            this.button_1.MultiSelectEn = false;
            this.button_1.Name = "button_1";
            this.button_1.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button_1.OutLineEn = true;
            this.button_1.OutLineSize = 3F;
            this.button_1.ProgressBarColor = System.Drawing.Color.Empty;
            this.button_1.ProgressBarEnable = false;
            this.button_1.ProgressBarMaxValue = 100;
            this.button_1.ProgressBarMinValue = 0;
            this.button_1.ProgressBarSize = 5;
            this.button_1.ProgressBarValue = 0;
            this.button_1.Selectable = false;
            this.button_1.Selected = false;
            this.button_1.Size = new System.Drawing.Size(50, 50);
            this.button_1.StatusLedEnable = false;
            this.button_1.StatusLedSize = ((byte)(10));
            this.button_1.TabIndex = 19;
            this.button_1.Text = "1";
            this.button_1.UseVisualStyleBackColor = false;
            this.button_1.Click += new System.EventHandler(this.buttonTenkey_Click);
            // 
            // button_5
            // 
            this.button_5.EditBox = null;
            this.button_5.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_5.IsActive = false;
            this.button_5.LedSyncedBackColorEnable = true;
            this.button_5.Location = new System.Drawing.Point(76, 203);
            this.button_5.MultiSelectEn = false;
            this.button_5.Name = "button_5";
            this.button_5.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button_5.OutLineEn = true;
            this.button_5.OutLineSize = 3F;
            this.button_5.ProgressBarColor = System.Drawing.Color.Empty;
            this.button_5.ProgressBarEnable = false;
            this.button_5.ProgressBarMaxValue = 100;
            this.button_5.ProgressBarMinValue = 0;
            this.button_5.ProgressBarSize = 5;
            this.button_5.ProgressBarValue = 0;
            this.button_5.Selectable = false;
            this.button_5.Selected = false;
            this.button_5.Size = new System.Drawing.Size(50, 50);
            this.button_5.StatusLedEnable = false;
            this.button_5.StatusLedSize = ((byte)(10));
            this.button_5.TabIndex = 15;
            this.button_5.Text = "5";
            this.button_5.UseVisualStyleBackColor = false;
            this.button_5.Click += new System.EventHandler(this.buttonTenkey_Click);
            // 
            // button_4
            // 
            this.button_4.EditBox = null;
            this.button_4.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_4.IsActive = false;
            this.button_4.LedSyncedBackColorEnable = true;
            this.button_4.Location = new System.Drawing.Point(20, 203);
            this.button_4.MultiSelectEn = false;
            this.button_4.Name = "button_4";
            this.button_4.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button_4.OutLineEn = true;
            this.button_4.OutLineSize = 3F;
            this.button_4.ProgressBarColor = System.Drawing.Color.Empty;
            this.button_4.ProgressBarEnable = false;
            this.button_4.ProgressBarMaxValue = 100;
            this.button_4.ProgressBarMinValue = 0;
            this.button_4.ProgressBarSize = 5;
            this.button_4.ProgressBarValue = 0;
            this.button_4.Selectable = false;
            this.button_4.Selected = false;
            this.button_4.Size = new System.Drawing.Size(50, 50);
            this.button_4.StatusLedEnable = false;
            this.button_4.StatusLedSize = ((byte)(10));
            this.button_4.TabIndex = 14;
            this.button_4.Text = "4";
            this.button_4.UseVisualStyleBackColor = false;
            this.button_4.Click += new System.EventHandler(this.buttonTenkey_Click);
            // 
            // button_Minus
            // 
            this.button_Minus.EditBox = null;
            this.button_Minus.Enabled = false;
            this.button_Minus.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_Minus.IsActive = false;
            this.button_Minus.LedSyncedBackColorEnable = true;
            this.button_Minus.Location = new System.Drawing.Point(243, 147);
            this.button_Minus.MultiSelectEn = false;
            this.button_Minus.Name = "button_Minus";
            this.button_Minus.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button_Minus.OutLineEn = true;
            this.button_Minus.OutLineSize = 3F;
            this.button_Minus.ProgressBarColor = System.Drawing.Color.Empty;
            this.button_Minus.ProgressBarEnable = false;
            this.button_Minus.ProgressBarMaxValue = 100;
            this.button_Minus.ProgressBarMinValue = 0;
            this.button_Minus.ProgressBarSize = 5;
            this.button_Minus.ProgressBarValue = 0;
            this.button_Minus.Selectable = false;
            this.button_Minus.Selected = false;
            this.button_Minus.Size = new System.Drawing.Size(50, 50);
            this.button_Minus.StatusLedEnable = false;
            this.button_Minus.StatusLedSize = ((byte)(10));
            this.button_Minus.TabIndex = 13;
            this.button_Minus.Text = "-";
            this.button_Minus.UseVisualStyleBackColor = false;
            this.button_Minus.Click += new System.EventHandler(this.button_MinusClick);
            // 
            // _btnCANCEL
            // 
            this._btnCANCEL.EditBox = null;
            this._btnCANCEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnCANCEL.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._btnCANCEL.IsActive = false;
            this._btnCANCEL.LedSyncedBackColorEnable = true;
            this._btnCANCEL.Location = new System.Drawing.Point(112, 495);
            this._btnCANCEL.MultiSelectEn = false;
            this._btnCANCEL.Name = "_btnCANCEL";
            this._btnCANCEL.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._btnCANCEL.OutLineEn = true;
            this._btnCANCEL.OutLineSize = 3F;
            this._btnCANCEL.ProgressBarColor = System.Drawing.Color.Empty;
            this._btnCANCEL.ProgressBarEnable = false;
            this._btnCANCEL.ProgressBarMaxValue = 100;
            this._btnCANCEL.ProgressBarMinValue = 0;
            this._btnCANCEL.ProgressBarSize = 5;
            this._btnCANCEL.ProgressBarValue = 0;
            this._btnCANCEL.Selectable = true;
            this._btnCANCEL.Selected = false;
            this._btnCANCEL.Size = new System.Drawing.Size(90, 39);
            this._btnCANCEL.StatusLedEnable = false;
            this._btnCANCEL.StatusLedSize = ((byte)(10));
            this._btnCANCEL.TabIndex = 6;
            this._btnCANCEL.Text = "戻す";
            this._btnCANCEL.UseVisualStyleBackColor = true;
            this._btnCANCEL.MouseUp += new System.Windows.Forms.MouseEventHandler(this._btnValueBack_Click);
            // 
            // _btnOK
            // 
            this._btnOK.EditBox = null;
            this._btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnOK.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._btnOK.IsActive = false;
            this._btnOK.LedSyncedBackColorEnable = true;
            this._btnOK.Location = new System.Drawing.Point(10, 495);
            this._btnOK.MultiSelectEn = false;
            this._btnOK.Name = "_btnOK";
            this._btnOK.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._btnOK.OutLineEn = true;
            this._btnOK.OutLineSize = 3F;
            this._btnOK.ProgressBarColor = System.Drawing.Color.Empty;
            this._btnOK.ProgressBarEnable = false;
            this._btnOK.ProgressBarMaxValue = 100;
            this._btnOK.ProgressBarMinValue = 0;
            this._btnOK.ProgressBarSize = 5;
            this._btnOK.ProgressBarValue = 0;
            this._btnOK.Selectable = true;
            this._btnOK.Selected = false;
            this._btnOK.Size = new System.Drawing.Size(90, 39);
            this._btnOK.StatusLedEnable = false;
            this._btnOK.StatusLedSize = ((byte)(10));
            this._btnOK.TabIndex = 5;
            this._btnOK.Text = "OK";
            this._btnOK.UseVisualStyleBackColor = true;
            this._btnOK.MouseUp += new System.Windows.Forms.MouseEventHandler(this._btnOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 32;
            this.label1.Text = "label1";
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(104, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 33;
            this.label2.Text = "label2";
            this.label2.Visible = false;
            // 
            // TenKeyDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(314, 540);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonEx_Title);
            this.Controls.Add(this.buttonEx3);
            this.Controls.Add(this.buttonEx2);
            this.Controls.Add(this._keyDisplay2);
            this.Controls.Add(this._keyDisplay);
            this.Controls.Add(this.InputModeChange);
            this.Controls.Add(this.buttonEx1);
            this.Controls.Add(this.buttonEx_Up);
            this.Controls.Add(this.buttonEx_Dn);
            this.Controls.Add(this.BackSpaceKey);
            this.Controls.Add(this.button_Plus);
            this.Controls.Add(this.button_asterisk);
            this.Controls.Add(this.button_division);
            this.Controls.Add(this.button_dot);
            this.Controls.Add(this.EnterKey);
            this.Controls.Add(this.button_9);
            this.Controls.Add(this.button_8);
            this.Controls.Add(this.button_7);
            this.Controls.Add(this.button_6);
            this.Controls.Add(this.AllClearKey);
            this.Controls.Add(this.button_3);
            this.Controls.Add(this.button_2);
            this.Controls.Add(this.button_0);
            this.Controls.Add(this.button_1);
            this.Controls.Add(this.button_5);
            this.Controls.Add(this.button_4);
            this.Controls.Add(this.button_Minus);
            this.Controls.Add(this._btnCANCEL);
            this.Controls.Add(this._btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TenKeyDialog";
            this.OutLineSize = 3;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "テンキー入力";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.TenKeyDialog_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.TenKeyDialog_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TenKeyDialog_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TenKeyDialog_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
		private ButtonEx _btnOK;
		private ButtonEx _btnCANCEL;
		private ButtonEx BackSpaceKey;
		private ButtonEx button_Plus;
		private ButtonEx button_asterisk;
		private ButtonEx button_division;
		private ButtonEx button_dot;
		private ButtonEx button_9;
		private ButtonEx button_8;
		private ButtonEx button_7;
		private ButtonEx button_6;
		private ButtonEx AllClearKey;
		private ButtonEx button_3;
		private ButtonEx button_2;
		private ButtonEx button_0;
		private ButtonEx button_1;
		private ButtonEx button_5;
		private ButtonEx button_4;
		private ButtonEx button_Minus;
		private ButtonEx buttonEx_Dn;
		private ButtonEx buttonEx_Up;
		private ButtonEx EnterKey;
		private ButtonEx buttonEx1;
		private ButtonEx InputModeChange;
        private NumericTextBox _keyDisplay;
		private DecimalTextBox _keyDisplay2;
		private ButtonEx buttonEx2;
		private ButtonEx buttonEx3;
		private ButtonEx buttonEx_Title;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
	}
}