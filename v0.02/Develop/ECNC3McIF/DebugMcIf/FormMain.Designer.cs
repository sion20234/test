﻿// <auto-generated />
namespace DebugMcIf
{
	partial class FormMain
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
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
			this._cmbTaskMode = new System.Windows.Forms.ComboBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this._btnColletClamp = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this._edtAecElectrodeNumber = new System.Windows.Forms.NumericUpDown();
			this._edtAecGuideNumber = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this._userAxis = new DebugMcIf.UserAxisFlags();
			this._btnReqReturnToOrigin = new System.Windows.Forms.Button();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this._btnGuideClamp = new System.Windows.Forms.Button();
			this._chkMcCompletedSequence = new System.Windows.Forms.CheckBox();
			this._chkCompletedFg = new System.Windows.Forms.CheckBox();
			this._btnMcStart = new System.Windows.Forms.Button();
			this._chkMcConnect = new System.Windows.Forms.CheckBox();
			this._btnRefresh = new System.Windows.Forms.Button();
			this.groupBox9 = new System.Windows.Forms.GroupBox();
			this._btnReqSpinCw = new System.Windows.Forms.Button();
			this._btnReqSpinStop = new System.Windows.Forms.Button();
			this._btnReqSpinCcw = new System.Windows.Forms.Button();
			this._btnReqPumpOn = new System.Windows.Forms.Button();
			this._btnReqDischargeOn = new System.Windows.Forms.Button();
			this.groupBox12 = new System.Windows.Forms.GroupBox();
			this._btnTopMost = new System.Windows.Forms.Button();
			this._edtOverrideOverall = new System.Windows.Forms.NumericUpDown();
			this._edtOverrideInterpolation = new System.Windows.Forms.NumericUpDown();
			this._edtOverrideSpindle = new System.Windows.Forms.NumericUpDown();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this._cmbCallScreen = new System.Windows.Forms.ComboBox();
			this._btnReqSwithing = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this._cmbCoordMode = new System.Windows.Forms.ComboBox();
			this._edtReqWPosLimit = new System.Windows.Forms.NumericUpDown();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this._edtSelectedProcessConditionNumber = new System.Windows.Forms.NumericUpDown();
			this._btnReqReset = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this._edtIoAddress = new System.Windows.Forms.TextBox();
			this._edtIoMask = new System.Windows.Forms.TextBox();
			this._edtIoValue = new System.Windows.Forms.TextBox();
			this._btnIoWrite = new System.Windows.Forms.Button();
			this._userCoord = new DebugMcIf.UserCoordinate();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this._chkAlarm = new System.Windows.Forms.CheckBox();
			this._btnAlarm = new System.Windows.Forms.Button();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._edtAecElectrodeNumber)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._edtAecGuideNumber)).BeginInit();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox9.SuspendLayout();
			this.groupBox12.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._edtOverrideOverall)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._edtOverrideInterpolation)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._edtOverrideSpindle)).BeginInit();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._edtReqWPosLimit)).BeginInit();
			this.groupBox5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._edtSelectedProcessConditionNumber)).BeginInit();
			this.groupBox6.SuspendLayout();
			this.SuspendLayout();
			// 
			// _cmbTaskMode
			// 
			this._cmbTaskMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._cmbTaskMode.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this._cmbTaskMode.FormattingEnabled = true;
			this._cmbTaskMode.Location = new System.Drawing.Point(93, 4);
			this._cmbTaskMode.Name = "_cmbTaskMode";
			this._cmbTaskMode.Size = new System.Drawing.Size(106, 27);
			this._cmbTaskMode.TabIndex = 4;
			this._cmbTaskMode.SelectedIndexChanged += new System.EventHandler(this._cmbTaskMode_SelectedIndexChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this._btnColletClamp);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this._edtAecElectrodeNumber);
			this.groupBox2.Location = new System.Drawing.Point(9, 182);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(170, 57);
			this.groupBox2.TabIndex = 11;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "ELECTRODE";
			// 
			// _btnColletClamp
			// 
			this._btnColletClamp.Location = new System.Drawing.Point(104, 18);
			this._btnColletClamp.Name = "_btnColletClamp";
			this._btnColletClamp.Size = new System.Drawing.Size(60, 30);
			this._btnColletClamp.TabIndex = 8;
			this._btnColletClamp.Text = "CLAMP";
			this._btnColletClamp.UseVisualStyleBackColor = true;
			this._btnColletClamp.Click += new System.EventHandler(this._btnColletClamp_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label2.Location = new System.Drawing.Point(6, 20);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(22, 24);
			this.label2.TabIndex = 6;
			this.label2.Text = "#";
			// 
			// _edtAecElectrodeNumber
			// 
			this._edtAecElectrodeNumber.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this._edtAecElectrodeNumber.ImeMode = System.Windows.Forms.ImeMode.Disable;
			this._edtAecElectrodeNumber.Location = new System.Drawing.Point(34, 18);
			this._edtAecElectrodeNumber.Name = "_edtAecElectrodeNumber";
			this._edtAecElectrodeNumber.Size = new System.Drawing.Size(64, 31);
			this._edtAecElectrodeNumber.TabIndex = 4;
			this._edtAecElectrodeNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this._edtAecElectrodeNumber.ValueChanged += new System.EventHandler(this._edtAecElectrodeNumber_ValueChanged);
			// 
			// _edtAecGuideNumber
			// 
			this._edtAecGuideNumber.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this._edtAecGuideNumber.ImeMode = System.Windows.Forms.ImeMode.Disable;
			this._edtAecGuideNumber.Location = new System.Drawing.Point(34, 18);
			this._edtAecGuideNumber.Name = "_edtAecGuideNumber";
			this._edtAecGuideNumber.Size = new System.Drawing.Size(64, 31);
			this._edtAecGuideNumber.TabIndex = 5;
			this._edtAecGuideNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this._edtAecGuideNumber.ValueChanged += new System.EventHandler(this._edtAecGuideNumber_ValueChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label1.Location = new System.Drawing.Point(6, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(22, 24);
			this.label1.TabIndex = 6;
			this.label1.Text = "#";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this._userAxis);
			this.groupBox3.Controls.Add(this._btnReqReturnToOrigin);
			this.groupBox3.Location = new System.Drawing.Point(155, 37);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(284, 78);
			this.groupBox3.TabIndex = 12;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "RETURN TO ORIGIN";
			// 
			// _userAxis
			// 
			this._userAxis.Location = new System.Drawing.Point(6, 18);
			this._userAxis.Name = "_userAxis";
			this._userAxis.Selected = ECNC3.Enumeration.AxisBits.Free;
			this._userAxis.Size = new System.Drawing.Size(276, 24);
			this._userAxis.TabIndex = 10;
			// 
			// _btnReqReturnToOrigin
			// 
			this._btnReqReturnToOrigin.Location = new System.Drawing.Point(6, 42);
			this._btnReqReturnToOrigin.Name = "_btnReqReturnToOrigin";
			this._btnReqReturnToOrigin.Size = new System.Drawing.Size(45, 30);
			this._btnReqReturnToOrigin.TabIndex = 9;
			this._btnReqReturnToOrigin.Text = "SET";
			this._btnReqReturnToOrigin.UseVisualStyleBackColor = true;
			this._btnReqReturnToOrigin.Click += new System.EventHandler(this._btnReqReturnToOrigin_Click);
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this._btnGuideClamp);
			this.groupBox4.Controls.Add(this.label1);
			this.groupBox4.Controls.Add(this._edtAecGuideNumber);
			this.groupBox4.Location = new System.Drawing.Point(188, 182);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(175, 57);
			this.groupBox4.TabIndex = 13;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "GUIDE";
			// 
			// _btnGuideClamp
			// 
			this._btnGuideClamp.Location = new System.Drawing.Point(104, 18);
			this._btnGuideClamp.Name = "_btnGuideClamp";
			this._btnGuideClamp.Size = new System.Drawing.Size(60, 30);
			this._btnGuideClamp.TabIndex = 8;
			this._btnGuideClamp.Text = "CLAMP";
			this._btnGuideClamp.UseVisualStyleBackColor = true;
			this._btnGuideClamp.Click += new System.EventHandler(this._btnGuideClamp_Click);
			// 
			// _chkMcCompletedSequence
			// 
			this._chkMcCompletedSequence.AutoSize = true;
			this._chkMcCompletedSequence.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this._chkMcCompletedSequence.Location = new System.Drawing.Point(6, 18);
			this._chkMcCompletedSequence.Name = "_chkMcCompletedSequence";
			this._chkMcCompletedSequence.Size = new System.Drawing.Size(83, 16);
			this._chkMcCompletedSequence.TabIndex = 15;
			this._chkMcCompletedSequence.Text = "COMP. SEQ.";
			this._chkMcCompletedSequence.UseVisualStyleBackColor = true;
			this._chkMcCompletedSequence.CheckedChanged += new System.EventHandler(this._chkMcCompletedSequence_CheckedChanged);
			// 
			// _chkCompletedFg
			// 
			this._chkCompletedFg.AutoSize = true;
			this._chkCompletedFg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this._chkCompletedFg.Location = new System.Drawing.Point(6, 37);
			this._chkCompletedFg.Name = "_chkCompletedFg";
			this._chkCompletedFg.Size = new System.Drawing.Size(74, 16);
			this._chkCompletedFg.TabIndex = 16;
			this._chkCompletedFg.Text = "COMP. FG";
			this._chkCompletedFg.UseVisualStyleBackColor = true;
			this._chkCompletedFg.CheckedChanged += new System.EventHandler(this._chkMcCompletedSequence_CheckedChanged);
			// 
			// _btnMcStart
			// 
			this._btnMcStart.Location = new System.Drawing.Point(456, 378);
			this._btnMcStart.Name = "_btnMcStart";
			this._btnMcStart.Size = new System.Drawing.Size(75, 52);
			this._btnMcStart.TabIndex = 23;
			this._btnMcStart.Text = "START";
			this._btnMcStart.MouseDown += new System.Windows.Forms.MouseEventHandler(this._btnMcStart_MouseDown);
			this._btnMcStart.MouseUp += new System.Windows.Forms.MouseEventHandler(this._btnMcStart_MouseUp);
			// 
			// _chkMcConnect
			// 
			this._chkMcConnect.AutoSize = true;
			this._chkMcConnect.Location = new System.Drawing.Point(9, 12);
			this._chkMcConnect.Name = "_chkMcConnect";
			this._chkMcConnect.Size = new System.Drawing.Size(78, 16);
			this._chkMcConnect.TabIndex = 24;
			this._chkMcConnect.Text = "CONNECT";
			this._chkMcConnect.UseVisualStyleBackColor = true;
			this._chkMcConnect.CheckedChanged += new System.EventHandler(this._chkMcConnect_CheckedChanged);
			// 
			// _btnRefresh
			// 
			this._btnRefresh.Location = new System.Drawing.Point(537, 4);
			this._btnRefresh.Name = "_btnRefresh";
			this._btnRefresh.Size = new System.Drawing.Size(75, 35);
			this._btnRefresh.TabIndex = 25;
			this._btnRefresh.Text = "REFRESH";
			this._btnRefresh.UseVisualStyleBackColor = true;
			this._btnRefresh.Click += new System.EventHandler(this._btnRefresh_Click);
			// 
			// groupBox9
			// 
			this.groupBox9.Controls.Add(this._btnReqSpinCw);
			this.groupBox9.Controls.Add(this._btnReqSpinStop);
			this.groupBox9.Controls.Add(this._btnReqSpinCcw);
			this.groupBox9.Location = new System.Drawing.Point(128, 245);
			this.groupBox9.Name = "groupBox9";
			this.groupBox9.Size = new System.Drawing.Size(164, 57);
			this.groupBox9.TabIndex = 13;
			this.groupBox9.TabStop = false;
			this.groupBox9.Text = "REQ_SPCMND";
			// 
			// _btnReqSpinCw
			// 
			this._btnReqSpinCw.Location = new System.Drawing.Point(57, 18);
			this._btnReqSpinCw.Name = "_btnReqSpinCw";
			this._btnReqSpinCw.Size = new System.Drawing.Size(45, 30);
			this._btnReqSpinCw.TabIndex = 4;
			this._btnReqSpinCw.Text = "CW";
			this._btnReqSpinCw.UseVisualStyleBackColor = true;
			this._btnReqSpinCw.Click += new System.EventHandler(this._btnReqSpin_Click);
			// 
			// _btnReqSpinStop
			// 
			this._btnReqSpinStop.Location = new System.Drawing.Point(6, 18);
			this._btnReqSpinStop.Name = "_btnReqSpinStop";
			this._btnReqSpinStop.Size = new System.Drawing.Size(45, 30);
			this._btnReqSpinStop.TabIndex = 3;
			this._btnReqSpinStop.Text = "STOP";
			this._btnReqSpinStop.UseVisualStyleBackColor = true;
			this._btnReqSpinStop.Click += new System.EventHandler(this._btnReqSpin_Click);
			// 
			// _btnReqSpinCcw
			// 
			this._btnReqSpinCcw.Location = new System.Drawing.Point(108, 18);
			this._btnReqSpinCcw.Name = "_btnReqSpinCcw";
			this._btnReqSpinCcw.Size = new System.Drawing.Size(45, 30);
			this._btnReqSpinCcw.TabIndex = 4;
			this._btnReqSpinCcw.Text = "CCW";
			this._btnReqSpinCcw.UseVisualStyleBackColor = true;
			this._btnReqSpinCcw.Click += new System.EventHandler(this._btnReqSpin_Click);
			// 
			// _btnReqPumpOn
			// 
			this._btnReqPumpOn.Location = new System.Drawing.Point(298, 245);
			this._btnReqPumpOn.Name = "_btnReqPumpOn";
			this._btnReqPumpOn.Size = new System.Drawing.Size(86, 30);
			this._btnReqPumpOn.TabIndex = 3;
			this._btnReqPumpOn.Text = "PUMPCMND";
			this._btnReqPumpOn.UseVisualStyleBackColor = true;
			this._btnReqPumpOn.Click += new System.EventHandler(this._btnReqPump_Click);
			// 
			// _btnReqDischargeOn
			// 
			this._btnReqDischargeOn.Location = new System.Drawing.Point(298, 281);
			this._btnReqDischargeOn.Name = "_btnReqDischargeOn";
			this._btnReqDischargeOn.Size = new System.Drawing.Size(86, 30);
			this._btnReqDischargeOn.TabIndex = 3;
			this._btnReqDischargeOn.Text = "BONCMND";
			this._btnReqDischargeOn.UseVisualStyleBackColor = true;
			this._btnReqDischargeOn.Click += new System.EventHandler(this._btnReqDischarge_Click);
			// 
			// groupBox12
			// 
			this.groupBox12.Controls.Add(this._chkMcCompletedSequence);
			this.groupBox12.Controls.Add(this._chkCompletedFg);
			this.groupBox12.Location = new System.Drawing.Point(9, 37);
			this.groupBox12.Name = "groupBox12";
			this.groupBox12.Size = new System.Drawing.Size(140, 78);
			this.groupBox12.TabIndex = 29;
			this.groupBox12.TabStop = false;
			this.groupBox12.Text = "STATUS";
			// 
			// _btnTopMost
			// 
			this._btnTopMost.Location = new System.Drawing.Point(456, 4);
			this._btnTopMost.Name = "_btnTopMost";
			this._btnTopMost.Size = new System.Drawing.Size(75, 35);
			this._btnTopMost.TabIndex = 34;
			this._btnTopMost.Text = "TOP MOST";
			this._btnTopMost.UseVisualStyleBackColor = true;
			this._btnTopMost.Click += new System.EventHandler(this._btnTopMost_Click);
			// 
			// _edtOverrideOverall
			// 
			this._edtOverrideOverall.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this._edtOverrideOverall.ImeMode = System.Windows.Forms.ImeMode.Disable;
			this._edtOverrideOverall.Location = new System.Drawing.Point(6, 18);
			this._edtOverrideOverall.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
			this._edtOverrideOverall.Name = "_edtOverrideOverall";
			this._edtOverrideOverall.Size = new System.Drawing.Size(64, 31);
			this._edtOverrideOverall.TabIndex = 9;
			this._edtOverrideOverall.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this._edtOverrideOverall.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this._edtOverrideOverall.ValueChanged += new System.EventHandler(this._edtOverride_ValueChanged);
			// 
			// _edtOverrideInterpolation
			// 
			this._edtOverrideInterpolation.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this._edtOverrideInterpolation.ImeMode = System.Windows.Forms.ImeMode.Disable;
			this._edtOverrideInterpolation.Location = new System.Drawing.Point(76, 18);
			this._edtOverrideInterpolation.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
			this._edtOverrideInterpolation.Name = "_edtOverrideInterpolation";
			this._edtOverrideInterpolation.Size = new System.Drawing.Size(64, 31);
			this._edtOverrideInterpolation.TabIndex = 41;
			this._edtOverrideInterpolation.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this._edtOverrideInterpolation.ValueChanged += new System.EventHandler(this._edtOverride_ValueChanged);
			// 
			// _edtOverrideSpindle
			// 
			this._edtOverrideSpindle.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this._edtOverrideSpindle.ImeMode = System.Windows.Forms.ImeMode.Disable;
			this._edtOverrideSpindle.Location = new System.Drawing.Point(146, 18);
			this._edtOverrideSpindle.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
			this._edtOverrideSpindle.Name = "_edtOverrideSpindle";
			this._edtOverrideSpindle.Size = new System.Drawing.Size(64, 31);
			this._edtOverrideSpindle.TabIndex = 42;
			this._edtOverrideSpindle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this._edtOverrideSpindle.ValueChanged += new System.EventHandler(this._edtOverride_ValueChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this._edtOverrideOverall);
			this.groupBox1.Controls.Add(this._edtOverrideSpindle);
			this.groupBox1.Controls.Add(this._edtOverrideInterpolation);
			this.groupBox1.Location = new System.Drawing.Point(9, 121);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(217, 55);
			this.groupBox1.TabIndex = 43;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "OVERRIDE";
			// 
			// _cmbCallScreen
			// 
			this._cmbCallScreen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._cmbCallScreen.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this._cmbCallScreen.FormattingEnabled = true;
			this._cmbCallScreen.Location = new System.Drawing.Point(9, 398);
			this._cmbCallScreen.Name = "_cmbCallScreen";
			this._cmbCallScreen.Size = new System.Drawing.Size(360, 32);
			this._cmbCallScreen.TabIndex = 44;
			this._cmbCallScreen.SelectionChangeCommitted += new System.EventHandler(this._cmbCallScreen_SelectionChangeCommitted);
			// 
			// _btnReqSwithing
			// 
			this._btnReqSwithing.Location = new System.Drawing.Point(375, 378);
			this._btnReqSwithing.Name = "_btnReqSwithing";
			this._btnReqSwithing.Size = new System.Drawing.Size(75, 52);
			this._btnReqSwithing.TabIndex = 45;
			this._btnReqSwithing.Text = "SWITCH";
			this._btnReqSwithing.UseVisualStyleBackColor = true;
			this._btnReqSwithing.Click += new System.EventHandler(this._btnReqSwithing_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(205, 13);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(37, 12);
			this.label3.TabIndex = 46;
			this.label3.Text = "MODE";
			// 
			// _cmbCoordMode
			// 
			this._cmbCoordMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._cmbCoordMode.FormattingEnabled = true;
			this._cmbCoordMode.Items.AddRange(new object[] {
            "MACHINE",
            "WORK"});
			this._cmbCoordMode.Location = new System.Drawing.Point(471, 51);
			this._cmbCoordMode.Name = "_cmbCoordMode";
			this._cmbCoordMode.Size = new System.Drawing.Size(141, 20);
			this._cmbCoordMode.TabIndex = 47;
			this._cmbCoordMode.SelectedIndexChanged += new System.EventHandler(this._cmbCoordMode_SelectedIndexChanged);
			// 
			// _edtReqWPosLimit
			// 
			this._edtReqWPosLimit.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this._edtReqWPosLimit.ImeMode = System.Windows.Forms.ImeMode.Disable;
			this._edtReqWPosLimit.Location = new System.Drawing.Point(6, 18);
			this._edtReqWPosLimit.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
			this._edtReqWPosLimit.Name = "_edtReqWPosLimit";
			this._edtReqWPosLimit.Size = new System.Drawing.Size(120, 31);
			this._edtReqWPosLimit.TabIndex = 9;
			this._edtReqWPosLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this._edtReqWPosLimit.ValueChanged += new System.EventHandler(this._edtReqWPosLimit_ValueChanged);
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this._edtReqWPosLimit);
			this.groupBox5.Location = new System.Drawing.Point(232, 121);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(137, 55);
			this.groupBox5.TabIndex = 48;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "WTopPos";
			// 
			// _edtSelectedProcessConditionNumber
			// 
			this._edtSelectedProcessConditionNumber.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this._edtSelectedProcessConditionNumber.ImeMode = System.Windows.Forms.ImeMode.Disable;
			this._edtSelectedProcessConditionNumber.Location = new System.Drawing.Point(58, 250);
			this._edtSelectedProcessConditionNumber.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
			this._edtSelectedProcessConditionNumber.Name = "_edtSelectedProcessConditionNumber";
			this._edtSelectedProcessConditionNumber.Size = new System.Drawing.Size(64, 31);
			this._edtSelectedProcessConditionNumber.TabIndex = 9;
			this._edtSelectedProcessConditionNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this._edtSelectedProcessConditionNumber.ValueChanged += new System.EventHandler(this._edtSelectedProcessConditionNumber_ValueChanged);
			// 
			// _btnReqReset
			// 
			this._btnReqReset.Location = new System.Drawing.Point(537, 378);
			this._btnReqReset.Name = "_btnReqReset";
			this._btnReqReset.Size = new System.Drawing.Size(75, 52);
			this._btnReqReset.TabIndex = 49;
			this._btnReqReset.Text = "RESET";
			this._btnReqReset.Click += new System.EventHandler(this._btnReqReset_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label4.Location = new System.Drawing.Point(15, 252);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(37, 24);
			this.label4.TabIndex = 50;
			this.label4.Text = "P#";
			// 
			// _edtIoAddress
			// 
			this._edtIoAddress.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this._edtIoAddress.Location = new System.Drawing.Point(6, 30);
			this._edtIoAddress.MaxLength = 4;
			this._edtIoAddress.Name = "_edtIoAddress";
			this._edtIoAddress.Size = new System.Drawing.Size(80, 31);
			this._edtIoAddress.TabIndex = 51;
			// 
			// _edtIoMask
			// 
			this._edtIoMask.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this._edtIoMask.Location = new System.Drawing.Point(92, 30);
			this._edtIoMask.MaxLength = 6;
			this._edtIoMask.Name = "_edtIoMask";
			this._edtIoMask.Size = new System.Drawing.Size(80, 31);
			this._edtIoMask.TabIndex = 51;
			// 
			// _edtIoValue
			// 
			this._edtIoValue.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this._edtIoValue.Location = new System.Drawing.Point(178, 30);
			this._edtIoValue.MaxLength = 6;
			this._edtIoValue.Name = "_edtIoValue";
			this._edtIoValue.Size = new System.Drawing.Size(80, 31);
			this._edtIoValue.TabIndex = 51;
			// 
			// _btnIoWrite
			// 
			this._btnIoWrite.Location = new System.Drawing.Point(264, 30);
			this._btnIoWrite.Name = "_btnIoWrite";
			this._btnIoWrite.Size = new System.Drawing.Size(45, 31);
			this._btnIoWrite.TabIndex = 52;
			this._btnIoWrite.Text = "SET";
			this._btnIoWrite.UseVisualStyleBackColor = true;
			this._btnIoWrite.Click += new System.EventHandler(this._btnIoWrite_Click);
			// 
			// _userCoord
			// 
			this._userCoord.Location = new System.Drawing.Point(390, 77);
			this._userCoord.Name = "_userCoord";
			this._userCoord.Size = new System.Drawing.Size(222, 295);
			this._userCoord.TabIndex = 33;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 15);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(58, 12);
			this.label5.TabIndex = 53;
			this.label5.Text = "ADDRESS";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(117, 15);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(36, 12);
			this.label6.TabIndex = 53;
			this.label6.Text = "MASK";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(196, 15);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(42, 12);
			this.label7.TabIndex = 53;
			this.label7.Text = "VALUE";
			// 
			// groupBox6
			// 
			this.groupBox6.Controls.Add(this.label5);
			this.groupBox6.Controls.Add(this._btnIoWrite);
			this.groupBox6.Controls.Add(this.label7);
			this.groupBox6.Controls.Add(this._edtIoValue);
			this.groupBox6.Controls.Add(this._edtIoAddress);
			this.groupBox6.Controls.Add(this.label6);
			this.groupBox6.Controls.Add(this._edtIoMask);
			this.groupBox6.Location = new System.Drawing.Point(9, 317);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(318, 75);
			this.groupBox6.TabIndex = 54;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "I/O";
			// 
			// _chkAlarm
			// 
			this._chkAlarm.AutoSize = true;
			this._chkAlarm.Location = new System.Drawing.Point(304, 9);
			this._chkAlarm.Name = "_chkAlarm";
			this._chkAlarm.Size = new System.Drawing.Size(80, 16);
			this._chkAlarm.TabIndex = 55;
			this._chkAlarm.Text = "checkBox1";
			this._chkAlarm.UseVisualStyleBackColor = true;
			this._chkAlarm.Click += new System.EventHandler(this._chkAlarm_Click);
			// 
			// _btnAlarm
			// 
			this._btnAlarm.Location = new System.Drawing.Point(391, 4);
			this._btnAlarm.Name = "_btnAlarm";
			this._btnAlarm.Size = new System.Drawing.Size(59, 35);
			this._btnAlarm.TabIndex = 56;
			this._btnAlarm.Text = "ALARM";
			this._btnAlarm.UseVisualStyleBackColor = true;
			this._btnAlarm.Click += new System.EventHandler(this._btnAlarm_Click);
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(624, 442);
			this.Controls.Add(this._btnAlarm);
			this.Controls.Add(this._chkAlarm);
			this.Controls.Add(this.groupBox6);
			this.Controls.Add(this.label4);
			this.Controls.Add(this._btnReqReset);
			this.Controls.Add(this._edtSelectedProcessConditionNumber);
			this.Controls.Add(this.groupBox5);
			this.Controls.Add(this._cmbCoordMode);
			this.Controls.Add(this.label3);
			this.Controls.Add(this._cmbTaskMode);
			this.Controls.Add(this._btnReqSwithing);
			this.Controls.Add(this._cmbCallScreen);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this._btnReqPumpOn);
			this.Controls.Add(this._btnReqDischargeOn);
			this.Controls.Add(this._btnTopMost);
			this.Controls.Add(this._userCoord);
			this.Controls.Add(this.groupBox12);
			this.Controls.Add(this.groupBox9);
			this.Controls.Add(this._btnRefresh);
			this.Controls.Add(this._chkMcConnect);
			this.Controls.Add(this._btnMcStart);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "FormMain";
			this.Text = "McIFDebug";
			this.Load += new System.EventHandler(this.FormMain_Load);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this._edtAecElectrodeNumber)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._edtAecGuideNumber)).EndInit();
			this.groupBox3.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.groupBox9.ResumeLayout(false);
			this.groupBox12.ResumeLayout(false);
			this.groupBox12.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this._edtOverrideOverall)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._edtOverrideInterpolation)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._edtOverrideSpindle)).EndInit();
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._edtReqWPosLimit)).EndInit();
			this.groupBox5.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._edtSelectedProcessConditionNumber)).EndInit();
			this.groupBox6.ResumeLayout(false);
			this.groupBox6.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ComboBox _cmbTaskMode;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.NumericUpDown _edtAecElectrodeNumber;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown _edtAecGuideNumber;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.CheckBox _chkMcCompletedSequence;
		private System.Windows.Forms.CheckBox _chkCompletedFg;
		private System.Windows.Forms.Button _btnMcStart;
		private System.Windows.Forms.CheckBox _chkMcConnect;
		private System.Windows.Forms.Button _btnRefresh;
		private System.Windows.Forms.GroupBox groupBox9;
		private System.Windows.Forms.Button _btnReqSpinCw;
		private System.Windows.Forms.Button _btnReqSpinStop;
		private System.Windows.Forms.Button _btnReqSpinCcw;
		private System.Windows.Forms.Button _btnReqPumpOn;
		private System.Windows.Forms.Button _btnReqDischargeOn;
		private System.Windows.Forms.Button _btnReqReturnToOrigin;
		private System.Windows.Forms.GroupBox groupBox12;
		private UserCoordinate _userCoord;
		private System.Windows.Forms.Button _btnTopMost;
		private System.Windows.Forms.Button _btnGuideClamp;
		private System.Windows.Forms.Button _btnColletClamp;
		private System.Windows.Forms.NumericUpDown _edtOverrideOverall;
		private System.Windows.Forms.NumericUpDown _edtOverrideInterpolation;
		private System.Windows.Forms.NumericUpDown _edtOverrideSpindle;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox _cmbCallScreen;
		private System.Windows.Forms.Button _btnReqSwithing;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox _cmbCoordMode;
		private System.Windows.Forms.NumericUpDown _edtReqWPosLimit;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.NumericUpDown _edtSelectedProcessConditionNumber;
		private System.Windows.Forms.Button _btnReqReset;
		private System.Windows.Forms.Label label4;
		private UserAxisFlags _userAxis;
		private System.Windows.Forms.TextBox _edtIoAddress;
		private System.Windows.Forms.TextBox _edtIoMask;
		private System.Windows.Forms.TextBox _edtIoValue;
		private System.Windows.Forms.Button _btnIoWrite;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.GroupBox groupBox6;
		private System.Windows.Forms.CheckBox _chkAlarm;
		private System.Windows.Forms.Button _btnAlarm;
	}
}

