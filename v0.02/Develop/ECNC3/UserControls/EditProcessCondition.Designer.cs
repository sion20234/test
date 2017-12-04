namespace ECNC3.Views
{
	partial class EditProcessCondition
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

		#region コンポーネント デザイナーで生成されたコード

		/// <summary> 
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
            this._btnOverRide = new ECNC3.Views.ButtonEx();
            this._edtOverRide = new ECNC3.Views.NumericTextBox();
            this.label3 = new ECNC3.Views.LabelEx();
            this._edtComment = new ECNC3.Views.TextBoxEx();
            this._btnPS = new ECNC3.Views.ButtonEx();
            this._btnServoSelect = new ECNC3.Views.ButtonEx();
            this._edtPS = new ECNC3.Views.NumericTextBox();
            this._edtServoSelect = new ECNC3.Views.NumericTextBox();
            this._btnMaterialSelect = new ECNC3.Views.LabelEx();
            this._btnInverter = new ECNC3.Views.ButtonEx();
            this._edtInverter = new ECNC3.Views.NumericTextBox();
            this._btnMaterial = new ECNC3.Views.ButtonEx();
            this._edtTurnOn = new ECNC3.Views.NumericTextBox();
            this._btnTurnOff = new ECNC3.Views.ButtonEx();
            this._edtIp = new ECNC3.Views.NumericTextBox();
            this._edtCap = new ECNC3.Views.NumericTextBox();
            this._edtTurnOff = new ECNC3.Views.NumericTextBox();
            this._btnDiameter = new ECNC3.Views.ButtonEx();
            this._edtDiameter = new ECNC3.Views.NumericTextBox();
            this._edtServoControl = new ECNC3.Views.NumericTextBox();
            this._edtSfrDown = new ECNC3.Views.NumericTextBox();
            this._btnPol = new ECNC3.Views.ButtonEx();
            this._edtSfrUp = new ECNC3.Views.NumericTextBox();
            this._edtCrs = new ECNC3.Views.NumericTextBox();
            this._btnCrs = new ECNC3.Views.ButtonEx();
            this._btnSfrUp = new ECNC3.Views.ButtonEx();
            this._edtPol = new ECNC3.Views.NumericTextBox();
            this._btnSfrDown = new ECNC3.Views.ButtonEx();
            this._btnTurnOn = new ECNC3.Views.ButtonEx();
            this._btnServoControl = new ECNC3.Views.ButtonEx();
            this._btnIp = new ECNC3.Views.ButtonEx();
            this._btnCap = new ECNC3.Views.ButtonEx();
            this.SuspendLayout();
            // 
            // _btnOverRide
            // 
            this._btnOverRide.AccessibleRole = System.Windows.Forms.AccessibleRole.IpAddress;
            this._btnOverRide.EditBox = null;
            this._btnOverRide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnOverRide.Font = new System.Drawing.Font("Meiryo UI", 10F);
            this._btnOverRide.IsActive = false;
            this._btnOverRide.LedSyncedBackColorEnable = true;
            this._btnOverRide.Location = new System.Drawing.Point(914, 33);
            this._btnOverRide.MultiSelectEn = false;
            this._btnOverRide.Name = "_btnOverRide";
            this._btnOverRide.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._btnOverRide.OutLineEn = true;
            this._btnOverRide.OutLineSize = 3F;
            this._btnOverRide.ProgressBarColor = System.Drawing.Color.Empty;
            this._btnOverRide.ProgressBarEnable = false;
            this._btnOverRide.ProgressBarMaxValue = 100;
            this._btnOverRide.ProgressBarMinValue = 0;
            this._btnOverRide.ProgressBarSize = 5;
            this._btnOverRide.ProgressBarValue = 0;
            this._btnOverRide.Selectable = true;
            this._btnOverRide.Selected = false;
            this._btnOverRide.Size = new System.Drawing.Size(103, 40);
            this._btnOverRide.StatusLedEnable = false;
            this._btnOverRide.StatusLedSize = ((byte)(15));
            this._btnOverRide.TabIndex = 356;
            this._btnOverRide.TabStop = false;
            this._btnOverRide.Text = "OVERRIDE";
            this._btnOverRide.UseVisualStyleBackColor = false;
            this._btnOverRide.Click += new System.EventHandler(this.Edit_TargetSelect_Click);
            // 
            // _edtOverRide
            // 
            this._edtOverRide.AccessibleRole = System.Windows.Forms.AccessibleRole.IpAddress;
            this._edtOverRide.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._edtOverRide.Font = new System.Drawing.Font("Meiryo UI", 18F);
            this._edtOverRide.FormatType = ECNC3.Views.NumericTextBox.FormatTypes.Free;
            this._edtOverRide.InputDataRatio = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._edtOverRide.Location = new System.Drawing.Point(914, 73);
            this._edtOverRide.LowerLimit = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this._edtOverRide.Name = "_edtOverRide";
            this._edtOverRide.RawText = "0";
            this._edtOverRide.Size = new System.Drawing.Size(65, 38);
            this._edtOverRide.TabIndex = 354;
            this._edtOverRide.Text = "0";
            this._edtOverRide.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this._edtOverRide.UpperLimit = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._edtOverRide.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._edtOverRide.Leave += new System.EventHandler(this.Edit_Leave);
            this._edtOverRide.MouseDown += new System.Windows.Forms.MouseEventHandler(this._edtOverRide_MouseDown);
            this._edtOverRide.Validating += new System.ComponentModel.CancelEventHandler(this.Edit_Validating);
            // 
            // label3
            // 
            this.label3.AccessibleRole = System.Windows.Forms.AccessibleRole.IpAddress;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("Meiryo UI", 18F);
            this.label3.Location = new System.Drawing.Point(980, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 38);
            this.label3.TabIndex = 355;
            this.label3.Text = "％";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _edtComment
            // 
            this._edtComment.Cursor = System.Windows.Forms.Cursors.IBeam;
            this._edtComment.Font = new System.Drawing.Font("Meiryo UI", 10.25F);
            this._edtComment.Location = new System.Drawing.Point(2, 5);
            this._edtComment.Name = "_edtComment";
            this._edtComment.Size = new System.Drawing.Size(417, 25);
            this._edtComment.TabIndex = 353;
            this._edtComment.Click += new System.EventHandler(this._edtComment_Click);
            // 
            // _btnPS
            // 
            this._btnPS.EditBox = null;
            this._btnPS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnPS.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._btnPS.IsActive = false;
            this._btnPS.LedSyncedBackColorEnable = true;
            this._btnPS.Location = new System.Drawing.Point(708, 33);
            this._btnPS.Margin = new System.Windows.Forms.Padding(0);
            this._btnPS.MultiSelectEn = false;
            this._btnPS.Name = "_btnPS";
            this._btnPS.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._btnPS.OutLineEn = true;
            this._btnPS.OutLineSize = 3F;
            this._btnPS.ProgressBarColor = System.Drawing.Color.Empty;
            this._btnPS.ProgressBarEnable = false;
            this._btnPS.ProgressBarMaxValue = 100;
            this._btnPS.ProgressBarMinValue = 0;
            this._btnPS.ProgressBarSize = 5;
            this._btnPS.ProgressBarValue = 0;
            this._btnPS.Selectable = true;
            this._btnPS.Selected = false;
            this._btnPS.Size = new System.Drawing.Size(43, 40);
            this._btnPS.StatusLedEnable = false;
            this._btnPS.StatusLedSize = ((byte)(15));
            this._btnPS.TabIndex = 350;
            this._btnPS.TabStop = false;
            this._btnPS.Text = "DC電源";
            this._btnPS.UseVisualStyleBackColor = false;
            this._btnPS.Click += new System.EventHandler(this.Edit_TargetSelect_Click);
            // 
            // _btnServoSelect
            // 
            this._btnServoSelect.EditBox = null;
            this._btnServoSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnServoSelect.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._btnServoSelect.IsActive = false;
            this._btnServoSelect.LedSyncedBackColorEnable = true;
            this._btnServoSelect.Location = new System.Drawing.Point(665, 33);
            this._btnServoSelect.Margin = new System.Windows.Forms.Padding(0);
            this._btnServoSelect.MultiSelectEn = false;
            this._btnServoSelect.Name = "_btnServoSelect";
            this._btnServoSelect.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._btnServoSelect.OutLineEn = true;
            this._btnServoSelect.OutLineSize = 3F;
            this._btnServoSelect.ProgressBarColor = System.Drawing.Color.Empty;
            this._btnServoSelect.ProgressBarEnable = false;
            this._btnServoSelect.ProgressBarMaxValue = 100;
            this._btnServoSelect.ProgressBarMinValue = 0;
            this._btnServoSelect.ProgressBarSize = 5;
            this._btnServoSelect.ProgressBarValue = 0;
            this._btnServoSelect.Selectable = true;
            this._btnServoSelect.Selected = false;
            this._btnServoSelect.Size = new System.Drawing.Size(43, 40);
            this._btnServoSelect.StatusLedEnable = false;
            this._btnServoSelect.StatusLedSize = ((byte)(15));
            this._btnServoSelect.TabIndex = 350;
            this._btnServoSelect.TabStop = false;
            this._btnServoSelect.Text = "AC送りモード";
            this._btnServoSelect.UseVisualStyleBackColor = false;
            this._btnServoSelect.Click += new System.EventHandler(this.Edit_TargetSelect_Click);
            // 
            // _edtPS
            // 
            this._edtPS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._edtPS.Font = new System.Drawing.Font("Meiryo UI", 18F);
            this._edtPS.FormatType = ECNC3.Views.NumericTextBox.FormatTypes.Free;
            this._edtPS.InputDataRatio = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._edtPS.Location = new System.Drawing.Point(708, 73);
            this._edtPS.LowerLimit = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this._edtPS.Margin = new System.Windows.Forms.Padding(0);
            this._edtPS.Name = "_edtPS";
            this._edtPS.RawText = "0";
            this._edtPS.Size = new System.Drawing.Size(43, 38);
            this._edtPS.TabIndex = 351;
            this._edtPS.Text = "0";
            this._edtPS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._edtPS.UpperLimit = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._edtPS.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._edtPS.Leave += new System.EventHandler(this.Edit_Leave);
            this._edtPS.MouseDown += new System.Windows.Forms.MouseEventHandler(this._edtPS_MouseDown);
            this._edtPS.Validating += new System.ComponentModel.CancelEventHandler(this.Edit_Validating);
            // 
            // _edtServoSelect
            // 
            this._edtServoSelect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._edtServoSelect.Font = new System.Drawing.Font("Meiryo UI", 18F);
            this._edtServoSelect.FormatType = ECNC3.Views.NumericTextBox.FormatTypes.Free;
            this._edtServoSelect.InputDataRatio = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._edtServoSelect.Location = new System.Drawing.Point(665, 73);
            this._edtServoSelect.LowerLimit = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this._edtServoSelect.Margin = new System.Windows.Forms.Padding(0);
            this._edtServoSelect.Name = "_edtServoSelect";
            this._edtServoSelect.RawText = "0";
            this._edtServoSelect.Size = new System.Drawing.Size(43, 38);
            this._edtServoSelect.TabIndex = 351;
            this._edtServoSelect.Text = "0";
            this._edtServoSelect.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._edtServoSelect.UpperLimit = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._edtServoSelect.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._edtServoSelect.Leave += new System.EventHandler(this.Edit_Leave);
            this._edtServoSelect.MouseDown += new System.Windows.Forms.MouseEventHandler(this._edtServoSelect_MouseDown);
            this._edtServoSelect.Validating += new System.ComponentModel.CancelEventHandler(this.Edit_Validating);
            // 
            // _btnMaterialSelect
            // 
            this._btnMaterialSelect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._btnMaterialSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnMaterialSelect.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._btnMaterialSelect.Location = new System.Drawing.Point(831, 73);
            this._btnMaterialSelect.Margin = new System.Windows.Forms.Padding(0);
            this._btnMaterialSelect.Name = "_btnMaterialSelect";
            this._btnMaterialSelect.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this._btnMaterialSelect.Size = new System.Drawing.Size(83, 38);
            this._btnMaterialSelect.TabIndex = 349;
            this._btnMaterialSelect.Text = "SUS";
            this._btnMaterialSelect.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _btnInverter
            // 
            this._btnInverter.EditBox = null;
            this._btnInverter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnInverter.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._btnInverter.IsActive = false;
            this._btnInverter.LedSyncedBackColorEnable = true;
            this._btnInverter.Location = new System.Drawing.Point(622, 33);
            this._btnInverter.Margin = new System.Windows.Forms.Padding(0);
            this._btnInverter.MultiSelectEn = false;
            this._btnInverter.Name = "_btnInverter";
            this._btnInverter.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._btnInverter.OutLineEn = true;
            this._btnInverter.OutLineSize = 3F;
            this._btnInverter.ProgressBarColor = System.Drawing.Color.Empty;
            this._btnInverter.ProgressBarEnable = false;
            this._btnInverter.ProgressBarMaxValue = 100;
            this._btnInverter.ProgressBarMinValue = 0;
            this._btnInverter.ProgressBarSize = 5;
            this._btnInverter.ProgressBarValue = 0;
            this._btnInverter.Selectable = true;
            this._btnInverter.Selected = false;
            this._btnInverter.Size = new System.Drawing.Size(43, 40);
            this._btnInverter.StatusLedEnable = false;
            this._btnInverter.StatusLedSize = ((byte)(15));
            this._btnInverter.TabIndex = 337;
            this._btnInverter.TabStop = false;
            this._btnInverter.Text = "液圧";
            this._btnInverter.UseVisualStyleBackColor = false;
            this._btnInverter.Click += new System.EventHandler(this.Edit_TargetSelect_Click);
            // 
            // _edtInverter
            // 
            this._edtInverter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._edtInverter.Font = new System.Drawing.Font("Meiryo UI", 18F);
            this._edtInverter.FormatType = ECNC3.Views.NumericTextBox.FormatTypes.Free;
            this._edtInverter.InputDataRatio = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._edtInverter.Location = new System.Drawing.Point(622, 73);
            this._edtInverter.LowerLimit = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this._edtInverter.Margin = new System.Windows.Forms.Padding(0);
            this._edtInverter.Name = "_edtInverter";
            this._edtInverter.RawText = "0";
            this._edtInverter.Size = new System.Drawing.Size(43, 38);
            this._edtInverter.TabIndex = 347;
            this._edtInverter.Text = "0";
            this._edtInverter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._edtInverter.UpperLimit = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._edtInverter.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._edtInverter.Leave += new System.EventHandler(this.Edit_Leave);
            this._edtInverter.MouseDown += new System.Windows.Forms.MouseEventHandler(this._edtInverter_MouseDown);
            this._edtInverter.Validating += new System.ComponentModel.CancelEventHandler(this.Edit_Validating);
            // 
            // _btnMaterial
            // 
            this._btnMaterial.EditBox = null;
            this._btnMaterial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnMaterial.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._btnMaterial.IsActive = false;
            this._btnMaterial.LedSyncedBackColorEnable = true;
            this._btnMaterial.Location = new System.Drawing.Point(831, 33);
            this._btnMaterial.Margin = new System.Windows.Forms.Padding(0);
            this._btnMaterial.MultiSelectEn = false;
            this._btnMaterial.Name = "_btnMaterial";
            this._btnMaterial.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._btnMaterial.OutLineEn = true;
            this._btnMaterial.OutLineSize = 3F;
            this._btnMaterial.ProgressBarColor = System.Drawing.Color.Empty;
            this._btnMaterial.ProgressBarEnable = false;
            this._btnMaterial.ProgressBarMaxValue = 100;
            this._btnMaterial.ProgressBarMinValue = 0;
            this._btnMaterial.ProgressBarSize = 5;
            this._btnMaterial.ProgressBarValue = 0;
            this._btnMaterial.Selectable = true;
            this._btnMaterial.Selected = false;
            this._btnMaterial.Size = new System.Drawing.Size(83, 40);
            this._btnMaterial.StatusLedEnable = false;
            this._btnMaterial.StatusLedSize = ((byte)(15));
            this._btnMaterial.TabIndex = 348;
            this._btnMaterial.TabStop = false;
            this._btnMaterial.Text = "材質";
            this._btnMaterial.UseVisualStyleBackColor = false;
            this._btnMaterial.Click += new System.EventHandler(this._btnMaterialSelect_Click);
            // 
            // _edtTurnOn
            // 
            this._edtTurnOn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._edtTurnOn.Font = new System.Drawing.Font("Meiryo UI", 18F);
            this._edtTurnOn.FormatType = ECNC3.Views.NumericTextBox.FormatTypes.Free;
            this._edtTurnOn.InputDataRatio = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._edtTurnOn.Location = new System.Drawing.Point(94, 73);
            this._edtTurnOn.LowerLimit = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this._edtTurnOn.Margin = new System.Windows.Forms.Padding(0);
            this._edtTurnOn.Name = "_edtTurnOn";
            this._edtTurnOn.RawText = "0";
            this._edtTurnOn.Size = new System.Drawing.Size(60, 38);
            this._edtTurnOn.TabIndex = 338;
            this._edtTurnOn.Text = "0";
            this._edtTurnOn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._edtTurnOn.UpperLimit = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._edtTurnOn.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._edtTurnOn.Leave += new System.EventHandler(this.Edit_Leave);
            this._edtTurnOn.MouseDown += new System.Windows.Forms.MouseEventHandler(this._edtTurnOn_MouseDown);
            this._edtTurnOn.Validating += new System.ComponentModel.CancelEventHandler(this.Edit_Validating);
            // 
            // _btnTurnOff
            // 
            this._btnTurnOff.EditBox = null;
            this._btnTurnOff.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnTurnOff.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._btnTurnOff.IsActive = false;
            this._btnTurnOff.LedSyncedBackColorEnable = true;
            this._btnTurnOff.Location = new System.Drawing.Point(154, 33);
            this._btnTurnOff.Margin = new System.Windows.Forms.Padding(0);
            this._btnTurnOff.MultiSelectEn = false;
            this._btnTurnOff.Name = "_btnTurnOff";
            this._btnTurnOff.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._btnTurnOff.OutLineEn = true;
            this._btnTurnOff.OutLineSize = 3F;
            this._btnTurnOff.ProgressBarColor = System.Drawing.Color.Empty;
            this._btnTurnOff.ProgressBarEnable = false;
            this._btnTurnOff.ProgressBarMaxValue = 100;
            this._btnTurnOff.ProgressBarMinValue = 0;
            this._btnTurnOff.ProgressBarSize = 5;
            this._btnTurnOff.ProgressBarValue = 0;
            this._btnTurnOff.Selectable = true;
            this._btnTurnOff.Selected = false;
            this._btnTurnOff.Size = new System.Drawing.Size(60, 40);
            this._btnTurnOff.StatusLedEnable = false;
            this._btnTurnOff.StatusLedSize = ((byte)(15));
            this._btnTurnOff.TabIndex = 329;
            this._btnTurnOff.TabStop = false;
            this._btnTurnOff.Text = "T-OFF";
            this._btnTurnOff.UseVisualStyleBackColor = false;
            this._btnTurnOff.Click += new System.EventHandler(this.Edit_TargetSelect_Click);
            // 
            // _edtIp
            // 
            this._edtIp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._edtIp.Font = new System.Drawing.Font("Meiryo UI", 18F);
            this._edtIp.FormatType = ECNC3.Views.NumericTextBox.FormatTypes.Free;
            this._edtIp.InputDataRatio = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._edtIp.Location = new System.Drawing.Point(214, 73);
            this._edtIp.LowerLimit = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this._edtIp.Margin = new System.Windows.Forms.Padding(0);
            this._edtIp.Name = "_edtIp";
            this._edtIp.RawText = "0";
            this._edtIp.Size = new System.Drawing.Size(77, 38);
            this._edtIp.TabIndex = 340;
            this._edtIp.Text = "0";
            this._edtIp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._edtIp.UpperLimit = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._edtIp.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._edtIp.Leave += new System.EventHandler(this.Edit_Leave);
            this._edtIp.MouseDown += new System.Windows.Forms.MouseEventHandler(this._edtIp_MouseDown);
            this._edtIp.Validating += new System.ComponentModel.CancelEventHandler(this.Edit_Validating);
            // 
            // _edtCap
            // 
            this._edtCap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._edtCap.Font = new System.Drawing.Font("Meiryo UI", 18F);
            this._edtCap.FormatType = ECNC3.Views.NumericTextBox.FormatTypes.Free;
            this._edtCap.InputDataRatio = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._edtCap.Location = new System.Drawing.Point(291, 73);
            this._edtCap.LowerLimit = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this._edtCap.Margin = new System.Windows.Forms.Padding(0);
            this._edtCap.Name = "_edtCap";
            this._edtCap.RawText = "0";
            this._edtCap.Size = new System.Drawing.Size(82, 38);
            this._edtCap.TabIndex = 341;
            this._edtCap.Text = "0";
            this._edtCap.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._edtCap.UpperLimit = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._edtCap.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._edtCap.Leave += new System.EventHandler(this.Edit_Leave);
            this._edtCap.MouseDown += new System.Windows.Forms.MouseEventHandler(this._edtCap_MouseDown);
            this._edtCap.Validating += new System.ComponentModel.CancelEventHandler(this.Edit_Validating);
            // 
            // _edtTurnOff
            // 
            this._edtTurnOff.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._edtTurnOff.Font = new System.Drawing.Font("Meiryo UI", 18F);
            this._edtTurnOff.FormatType = ECNC3.Views.NumericTextBox.FormatTypes.Free;
            this._edtTurnOff.InputDataRatio = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._edtTurnOff.Location = new System.Drawing.Point(154, 73);
            this._edtTurnOff.LowerLimit = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this._edtTurnOff.Margin = new System.Windows.Forms.Padding(0);
            this._edtTurnOff.Name = "_edtTurnOff";
            this._edtTurnOff.RawText = "0";
            this._edtTurnOff.Size = new System.Drawing.Size(60, 38);
            this._edtTurnOff.TabIndex = 339;
            this._edtTurnOff.Text = "0";
            this._edtTurnOff.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._edtTurnOff.UpperLimit = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._edtTurnOff.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._edtTurnOff.Leave += new System.EventHandler(this.Edit_Leave);
            this._edtTurnOff.MouseDown += new System.Windows.Forms.MouseEventHandler(this._edtTurnOff_MouseDown);
            this._edtTurnOff.Validating += new System.ComponentModel.CancelEventHandler(this.Edit_Validating);
            // 
            // _btnDiameter
            // 
            this._btnDiameter.EditBox = null;
            this._btnDiameter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnDiameter.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._btnDiameter.IsActive = false;
            this._btnDiameter.LedSyncedBackColorEnable = true;
            this._btnDiameter.Location = new System.Drawing.Point(751, 33);
            this._btnDiameter.Margin = new System.Windows.Forms.Padding(0);
            this._btnDiameter.MultiSelectEn = false;
            this._btnDiameter.Name = "_btnDiameter";
            this._btnDiameter.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._btnDiameter.OutLineEn = true;
            this._btnDiameter.OutLineSize = 3F;
            this._btnDiameter.ProgressBarColor = System.Drawing.Color.Empty;
            this._btnDiameter.ProgressBarEnable = false;
            this._btnDiameter.ProgressBarMaxValue = 100;
            this._btnDiameter.ProgressBarMinValue = 0;
            this._btnDiameter.ProgressBarSize = 5;
            this._btnDiameter.ProgressBarValue = 0;
            this._btnDiameter.Selectable = true;
            this._btnDiameter.Selected = false;
            this._btnDiameter.Size = new System.Drawing.Size(80, 40);
            this._btnDiameter.StatusLedEnable = false;
            this._btnDiameter.StatusLedSize = ((byte)(15));
            this._btnDiameter.TabIndex = 326;
            this._btnDiameter.TabStop = false;
            this._btnDiameter.Text = "電極径";
            this._btnDiameter.UseVisualStyleBackColor = false;
            this._btnDiameter.Click += new System.EventHandler(this.Edit_TargetSelect_Click);
            // 
            // _edtDiameter
            // 
            this._edtDiameter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._edtDiameter.Font = new System.Drawing.Font("Meiryo UI", 18F);
            this._edtDiameter.FormatType = ECNC3.Views.NumericTextBox.FormatTypes.Free;
            this._edtDiameter.InputDataRatio = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._edtDiameter.Location = new System.Drawing.Point(751, 73);
            this._edtDiameter.LowerLimit = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this._edtDiameter.Margin = new System.Windows.Forms.Padding(0);
            this._edtDiameter.Name = "_edtDiameter";
            this._edtDiameter.RawText = "0";
            this._edtDiameter.Size = new System.Drawing.Size(80, 38);
            this._edtDiameter.TabIndex = 327;
            this._edtDiameter.Text = "0";
            this._edtDiameter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._edtDiameter.UpperLimit = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._edtDiameter.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._edtDiameter.Leave += new System.EventHandler(this.Edit_Leave);
            this._edtDiameter.MouseDown += new System.Windows.Forms.MouseEventHandler(this._edtDiameter_MouseDown);
            this._edtDiameter.Validating += new System.ComponentModel.CancelEventHandler(this.Edit_Validating);
            // 
            // _edtServoControl
            // 
            this._edtServoControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._edtServoControl.Font = new System.Drawing.Font("Meiryo UI", 18F);
            this._edtServoControl.FormatType = ECNC3.Views.NumericTextBox.FormatTypes.Free;
            this._edtServoControl.InputDataRatio = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._edtServoControl.Location = new System.Drawing.Point(373, 73);
            this._edtServoControl.LowerLimit = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this._edtServoControl.Margin = new System.Windows.Forms.Padding(0);
            this._edtServoControl.Name = "_edtServoControl";
            this._edtServoControl.RawText = "0";
            this._edtServoControl.Size = new System.Drawing.Size(43, 38);
            this._edtServoControl.TabIndex = 342;
            this._edtServoControl.Text = "0";
            this._edtServoControl.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._edtServoControl.UpperLimit = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._edtServoControl.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._edtServoControl.Leave += new System.EventHandler(this.Edit_Leave);
            this._edtServoControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this._edtServoControl_MouseDown);
            this._edtServoControl.Validating += new System.ComponentModel.CancelEventHandler(this.Edit_Validating);
            // 
            // _edtSfrDown
            // 
            this._edtSfrDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._edtSfrDown.Font = new System.Drawing.Font("Meiryo UI", 18F);
            this._edtSfrDown.FormatType = ECNC3.Views.NumericTextBox.FormatTypes.Free;
            this._edtSfrDown.InputDataRatio = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._edtSfrDown.Location = new System.Drawing.Point(416, 73);
            this._edtSfrDown.LowerLimit = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this._edtSfrDown.Margin = new System.Windows.Forms.Padding(0);
            this._edtSfrDown.Name = "_edtSfrDown";
            this._edtSfrDown.RawText = "0";
            this._edtSfrDown.Size = new System.Drawing.Size(60, 38);
            this._edtSfrDown.TabIndex = 343;
            this._edtSfrDown.Text = "0";
            this._edtSfrDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._edtSfrDown.UpperLimit = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._edtSfrDown.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._edtSfrDown.Leave += new System.EventHandler(this.Edit_Leave);
            this._edtSfrDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this._edtSfrDown_MouseDown);
            this._edtSfrDown.Validating += new System.ComponentModel.CancelEventHandler(this.Edit_Validating);
            // 
            // _btnPol
            // 
            this._btnPol.EditBox = null;
            this._btnPol.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnPol.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._btnPol.IsActive = false;
            this._btnPol.LedSyncedBackColorEnable = true;
            this._btnPol.Location = new System.Drawing.Point(579, 33);
            this._btnPol.Margin = new System.Windows.Forms.Padding(0);
            this._btnPol.MultiSelectEn = false;
            this._btnPol.Name = "_btnPol";
            this._btnPol.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._btnPol.OutLineEn = true;
            this._btnPol.OutLineSize = 3F;
            this._btnPol.ProgressBarColor = System.Drawing.Color.Empty;
            this._btnPol.ProgressBarEnable = false;
            this._btnPol.ProgressBarMaxValue = 100;
            this._btnPol.ProgressBarMinValue = 0;
            this._btnPol.ProgressBarSize = 5;
            this._btnPol.ProgressBarValue = 0;
            this._btnPol.Selectable = true;
            this._btnPol.Selected = false;
            this._btnPol.Size = new System.Drawing.Size(43, 40);
            this._btnPol.StatusLedEnable = false;
            this._btnPol.StatusLedSize = ((byte)(15));
            this._btnPol.TabIndex = 336;
            this._btnPol.TabStop = false;
            this._btnPol.Text = "極性";
            this._btnPol.UseVisualStyleBackColor = false;
            this._btnPol.Click += new System.EventHandler(this.Edit_TargetSelect_Click);
            // 
            // _edtSfrUp
            // 
            this._edtSfrUp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._edtSfrUp.Font = new System.Drawing.Font("Meiryo UI", 18F);
            this._edtSfrUp.FormatType = ECNC3.Views.NumericTextBox.FormatTypes.Free;
            this._edtSfrUp.InputDataRatio = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._edtSfrUp.Location = new System.Drawing.Point(476, 73);
            this._edtSfrUp.LowerLimit = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this._edtSfrUp.Margin = new System.Windows.Forms.Padding(0);
            this._edtSfrUp.Name = "_edtSfrUp";
            this._edtSfrUp.RawText = "0";
            this._edtSfrUp.Size = new System.Drawing.Size(60, 38);
            this._edtSfrUp.TabIndex = 344;
            this._edtSfrUp.Text = "0";
            this._edtSfrUp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._edtSfrUp.UpperLimit = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._edtSfrUp.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._edtSfrUp.Leave += new System.EventHandler(this.Edit_Leave);
            this._edtSfrUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this._edtSfrUp_MouseDown);
            this._edtSfrUp.Validating += new System.ComponentModel.CancelEventHandler(this.Edit_Validating);
            // 
            // _edtCrs
            // 
            this._edtCrs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._edtCrs.Font = new System.Drawing.Font("Meiryo UI", 18F);
            this._edtCrs.FormatType = ECNC3.Views.NumericTextBox.FormatTypes.Free;
            this._edtCrs.InputDataRatio = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._edtCrs.Location = new System.Drawing.Point(536, 73);
            this._edtCrs.LowerLimit = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this._edtCrs.Margin = new System.Windows.Forms.Padding(0);
            this._edtCrs.Name = "_edtCrs";
            this._edtCrs.RawText = "0";
            this._edtCrs.Size = new System.Drawing.Size(43, 38);
            this._edtCrs.TabIndex = 345;
            this._edtCrs.Text = "0";
            this._edtCrs.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._edtCrs.UpperLimit = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._edtCrs.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._edtCrs.Leave += new System.EventHandler(this.Edit_Leave);
            this._edtCrs.MouseDown += new System.Windows.Forms.MouseEventHandler(this._edtCrs_MouseDown);
            this._edtCrs.Validating += new System.ComponentModel.CancelEventHandler(this.Edit_Validating);
            // 
            // _btnCrs
            // 
            this._btnCrs.EditBox = null;
            this._btnCrs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnCrs.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._btnCrs.IsActive = false;
            this._btnCrs.LedSyncedBackColorEnable = true;
            this._btnCrs.Location = new System.Drawing.Point(536, 33);
            this._btnCrs.Margin = new System.Windows.Forms.Padding(0);
            this._btnCrs.MultiSelectEn = false;
            this._btnCrs.Name = "_btnCrs";
            this._btnCrs.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._btnCrs.OutLineEn = true;
            this._btnCrs.OutLineSize = 3F;
            this._btnCrs.ProgressBarColor = System.Drawing.Color.Empty;
            this._btnCrs.ProgressBarEnable = false;
            this._btnCrs.ProgressBarMaxValue = 100;
            this._btnCrs.ProgressBarMinValue = 0;
            this._btnCrs.ProgressBarSize = 5;
            this._btnCrs.ProgressBarValue = 0;
            this._btnCrs.Selectable = true;
            this._btnCrs.Selected = false;
            this._btnCrs.Size = new System.Drawing.Size(43, 40);
            this._btnCrs.StatusLedEnable = false;
            this._btnCrs.StatusLedSize = ((byte)(15));
            this._btnCrs.TabIndex = 335;
            this._btnCrs.TabStop = false;
            this._btnCrs.Text = "回転";
            this._btnCrs.UseVisualStyleBackColor = false;
            this._btnCrs.Click += new System.EventHandler(this.Edit_TargetSelect_Click);
            // 
            // _btnSfrUp
            // 
            this._btnSfrUp.EditBox = null;
            this._btnSfrUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnSfrUp.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._btnSfrUp.IsActive = false;
            this._btnSfrUp.LedSyncedBackColorEnable = true;
            this._btnSfrUp.Location = new System.Drawing.Point(476, 33);
            this._btnSfrUp.Margin = new System.Windows.Forms.Padding(0);
            this._btnSfrUp.MultiSelectEn = false;
            this._btnSfrUp.Name = "_btnSfrUp";
            this._btnSfrUp.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._btnSfrUp.OutLineEn = true;
            this._btnSfrUp.OutLineSize = 3F;
            this._btnSfrUp.ProgressBarColor = System.Drawing.Color.Empty;
            this._btnSfrUp.ProgressBarEnable = false;
            this._btnSfrUp.ProgressBarMaxValue = 100;
            this._btnSfrUp.ProgressBarMinValue = 0;
            this._btnSfrUp.ProgressBarSize = 5;
            this._btnSfrUp.ProgressBarValue = 0;
            this._btnSfrUp.Selectable = true;
            this._btnSfrUp.Selected = false;
            this._btnSfrUp.Size = new System.Drawing.Size(60, 40);
            this._btnSfrUp.StatusLedEnable = false;
            this._btnSfrUp.StatusLedSize = ((byte)(15));
            this._btnSfrUp.TabIndex = 334;
            this._btnSfrUp.TabStop = false;
            this._btnSfrUp.Text = "SFR\r\nUP";
            this._btnSfrUp.UseVisualStyleBackColor = false;
            this._btnSfrUp.Click += new System.EventHandler(this.Edit_TargetSelect_Click);
            // 
            // _edtPol
            // 
            this._edtPol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._edtPol.Font = new System.Drawing.Font("Meiryo UI", 18F);
            this._edtPol.FormatType = ECNC3.Views.NumericTextBox.FormatTypes.Free;
            this._edtPol.InputDataRatio = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._edtPol.Location = new System.Drawing.Point(579, 73);
            this._edtPol.LowerLimit = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this._edtPol.Margin = new System.Windows.Forms.Padding(0);
            this._edtPol.Name = "_edtPol";
            this._edtPol.RawText = "0";
            this._edtPol.Size = new System.Drawing.Size(43, 38);
            this._edtPol.TabIndex = 346;
            this._edtPol.Text = "0";
            this._edtPol.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._edtPol.UpperLimit = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._edtPol.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._edtPol.Leave += new System.EventHandler(this.Edit_Leave);
            this._edtPol.MouseDown += new System.Windows.Forms.MouseEventHandler(this._edtPol_MouseDown);
            this._edtPol.Validating += new System.ComponentModel.CancelEventHandler(this.Edit_Validating);
            // 
            // _btnSfrDown
            // 
            this._btnSfrDown.EditBox = null;
            this._btnSfrDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnSfrDown.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._btnSfrDown.IsActive = false;
            this._btnSfrDown.LedSyncedBackColorEnable = true;
            this._btnSfrDown.Location = new System.Drawing.Point(416, 33);
            this._btnSfrDown.Margin = new System.Windows.Forms.Padding(0);
            this._btnSfrDown.MultiSelectEn = false;
            this._btnSfrDown.Name = "_btnSfrDown";
            this._btnSfrDown.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._btnSfrDown.OutLineEn = true;
            this._btnSfrDown.OutLineSize = 3F;
            this._btnSfrDown.ProgressBarColor = System.Drawing.Color.Empty;
            this._btnSfrDown.ProgressBarEnable = false;
            this._btnSfrDown.ProgressBarMaxValue = 100;
            this._btnSfrDown.ProgressBarMinValue = 0;
            this._btnSfrDown.ProgressBarSize = 5;
            this._btnSfrDown.ProgressBarValue = 0;
            this._btnSfrDown.Selectable = true;
            this._btnSfrDown.Selected = false;
            this._btnSfrDown.Size = new System.Drawing.Size(60, 40);
            this._btnSfrDown.StatusLedEnable = false;
            this._btnSfrDown.StatusLedSize = ((byte)(15));
            this._btnSfrDown.TabIndex = 333;
            this._btnSfrDown.TabStop = false;
            this._btnSfrDown.Text = "SFR\r\nDOWN";
            this._btnSfrDown.UseVisualStyleBackColor = false;
            this._btnSfrDown.Click += new System.EventHandler(this.Edit_TargetSelect_Click);
            // 
            // _btnTurnOn
            // 
            this._btnTurnOn.EditBox = null;
            this._btnTurnOn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnTurnOn.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._btnTurnOn.IsActive = false;
            this._btnTurnOn.LedSyncedBackColorEnable = true;
            this._btnTurnOn.Location = new System.Drawing.Point(94, 33);
            this._btnTurnOn.Margin = new System.Windows.Forms.Padding(0);
            this._btnTurnOn.MultiSelectEn = false;
            this._btnTurnOn.Name = "_btnTurnOn";
            this._btnTurnOn.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._btnTurnOn.OutLineEn = true;
            this._btnTurnOn.OutLineSize = 3F;
            this._btnTurnOn.ProgressBarColor = System.Drawing.Color.Empty;
            this._btnTurnOn.ProgressBarEnable = false;
            this._btnTurnOn.ProgressBarMaxValue = 100;
            this._btnTurnOn.ProgressBarMinValue = 0;
            this._btnTurnOn.ProgressBarSize = 5;
            this._btnTurnOn.ProgressBarValue = 0;
            this._btnTurnOn.Selectable = true;
            this._btnTurnOn.Selected = false;
            this._btnTurnOn.Size = new System.Drawing.Size(60, 40);
            this._btnTurnOn.StatusLedEnable = false;
            this._btnTurnOn.StatusLedSize = ((byte)(15));
            this._btnTurnOn.TabIndex = 328;
            this._btnTurnOn.TabStop = false;
            this._btnTurnOn.Text = "T-ON";
            this._btnTurnOn.UseVisualStyleBackColor = false;
            this._btnTurnOn.Click += new System.EventHandler(this.Edit_TargetSelect_Click);
            // 
            // _btnServoControl
            // 
            this._btnServoControl.EditBox = null;
            this._btnServoControl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnServoControl.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._btnServoControl.IsActive = false;
            this._btnServoControl.LedSyncedBackColorEnable = true;
            this._btnServoControl.Location = new System.Drawing.Point(373, 33);
            this._btnServoControl.Margin = new System.Windows.Forms.Padding(0);
            this._btnServoControl.MultiSelectEn = false;
            this._btnServoControl.Name = "_btnServoControl";
            this._btnServoControl.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._btnServoControl.OutLineEn = true;
            this._btnServoControl.OutLineSize = 3F;
            this._btnServoControl.ProgressBarColor = System.Drawing.Color.Empty;
            this._btnServoControl.ProgressBarEnable = false;
            this._btnServoControl.ProgressBarMaxValue = 100;
            this._btnServoControl.ProgressBarMinValue = 0;
            this._btnServoControl.ProgressBarSize = 5;
            this._btnServoControl.ProgressBarValue = 0;
            this._btnServoControl.Selectable = true;
            this._btnServoControl.Selected = false;
            this._btnServoControl.Size = new System.Drawing.Size(43, 40);
            this._btnServoControl.StatusLedEnable = false;
            this._btnServoControl.StatusLedSize = ((byte)(15));
            this._btnServoControl.TabIndex = 332;
            this._btnServoControl.TabStop = false;
            this._btnServoControl.Text = "SC";
            this._btnServoControl.UseVisualStyleBackColor = false;
            this._btnServoControl.Click += new System.EventHandler(this.Edit_TargetSelect_Click);
            // 
            // _btnIp
            // 
            this._btnIp.EditBox = null;
            this._btnIp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnIp.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._btnIp.IsActive = false;
            this._btnIp.LedSyncedBackColorEnable = true;
            this._btnIp.Location = new System.Drawing.Point(214, 33);
            this._btnIp.Margin = new System.Windows.Forms.Padding(0);
            this._btnIp.MultiSelectEn = false;
            this._btnIp.Name = "_btnIp";
            this._btnIp.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._btnIp.OutLineEn = true;
            this._btnIp.OutLineSize = 3F;
            this._btnIp.ProgressBarColor = System.Drawing.Color.Empty;
            this._btnIp.ProgressBarEnable = false;
            this._btnIp.ProgressBarMaxValue = 100;
            this._btnIp.ProgressBarMinValue = 0;
            this._btnIp.ProgressBarSize = 5;
            this._btnIp.ProgressBarValue = 0;
            this._btnIp.Selectable = true;
            this._btnIp.Selected = false;
            this._btnIp.Size = new System.Drawing.Size(77, 40);
            this._btnIp.StatusLedEnable = false;
            this._btnIp.StatusLedSize = ((byte)(15));
            this._btnIp.TabIndex = 330;
            this._btnIp.TabStop = false;
            this._btnIp.Text = "IP";
            this._btnIp.UseVisualStyleBackColor = false;
            this._btnIp.Click += new System.EventHandler(this.Edit_TargetSelect_Click);
            // 
            // _btnCap
            // 
            this._btnCap.EditBox = null;
            this._btnCap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnCap.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._btnCap.IsActive = false;
            this._btnCap.LedSyncedBackColorEnable = true;
            this._btnCap.Location = new System.Drawing.Point(291, 33);
            this._btnCap.Margin = new System.Windows.Forms.Padding(0);
            this._btnCap.MultiSelectEn = false;
            this._btnCap.Name = "_btnCap";
            this._btnCap.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._btnCap.OutLineEn = true;
            this._btnCap.OutLineSize = 3F;
            this._btnCap.ProgressBarColor = System.Drawing.Color.Empty;
            this._btnCap.ProgressBarEnable = false;
            this._btnCap.ProgressBarMaxValue = 100;
            this._btnCap.ProgressBarMinValue = 0;
            this._btnCap.ProgressBarSize = 5;
            this._btnCap.ProgressBarValue = 0;
            this._btnCap.Selectable = true;
            this._btnCap.Selected = false;
            this._btnCap.Size = new System.Drawing.Size(82, 40);
            this._btnCap.StatusLedEnable = false;
            this._btnCap.StatusLedSize = ((byte)(15));
            this._btnCap.TabIndex = 331;
            this._btnCap.TabStop = false;
            this._btnCap.Text = "CAP";
            this._btnCap.UseVisualStyleBackColor = false;
            this._btnCap.Click += new System.EventHandler(this.Edit_TargetSelect_Click);
            // 
            // EditProcessCondition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._btnOverRide);
            this.Controls.Add(this._edtOverRide);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._edtComment);
            this.Controls.Add(this._btnPS);
            this.Controls.Add(this._btnServoSelect);
            this.Controls.Add(this._edtPS);
            this.Controls.Add(this._edtServoSelect);
            this.Controls.Add(this._btnMaterialSelect);
            this.Controls.Add(this._btnInverter);
            this.Controls.Add(this._edtInverter);
            this.Controls.Add(this._btnMaterial);
            this.Controls.Add(this._edtTurnOn);
            this.Controls.Add(this._btnTurnOff);
            this.Controls.Add(this._edtIp);
            this.Controls.Add(this._edtCap);
            this.Controls.Add(this._edtTurnOff);
            this.Controls.Add(this._btnDiameter);
            this.Controls.Add(this._edtDiameter);
            this.Controls.Add(this._edtServoControl);
            this.Controls.Add(this._edtSfrDown);
            this.Controls.Add(this._btnPol);
            this.Controls.Add(this._edtSfrUp);
            this.Controls.Add(this._edtCrs);
            this.Controls.Add(this._btnCrs);
            this.Controls.Add(this._btnSfrUp);
            this.Controls.Add(this._edtPol);
            this.Controls.Add(this._btnSfrDown);
            this.Controls.Add(this._btnTurnOn);
            this.Controls.Add(this._btnServoControl);
            this.Controls.Add(this._btnIp);
            this.Controls.Add(this._btnCap);
            this.Name = "EditProcessCondition";
            this.Size = new System.Drawing.Size(1024, 113);
            this.Load += new System.EventHandler(this.Form_Load);
            this.Leave += new System.EventHandler(this.EditProcessCondition_Leave);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private LabelEx _btnMaterialSelect;
		private ButtonEx _btnInverter;
		private NumericTextBox _edtInverter;
		private ButtonEx _btnMaterial;
		private NumericTextBox _edtTurnOn;
		private ButtonEx _btnTurnOff;
		private NumericTextBox _edtIp;
		private NumericTextBox _edtCap;
		private NumericTextBox _edtTurnOff;
		private ButtonEx _btnDiameter;
		private NumericTextBox _edtDiameter;
		private NumericTextBox _edtServoControl;
		private NumericTextBox _edtSfrDown;
		private ButtonEx _btnPol;
		private NumericTextBox _edtSfrUp;
		private NumericTextBox _edtCrs;
		private ButtonEx _btnCrs;
		private ButtonEx _btnSfrUp;
		private NumericTextBox _edtPol;
		private ButtonEx _btnSfrDown;
		private ButtonEx _btnTurnOn;
		private ButtonEx _btnServoControl;
		private ButtonEx _btnIp;
		private ButtonEx _btnCap;
		private ButtonEx _btnServoSelect;
		private NumericTextBox _edtServoSelect;
		private NumericTextBox _edtPS;
		private ButtonEx _btnPS;
		private TextBoxEx _edtComment;
		private ButtonEx _btnOverRide;
		private NumericTextBox _edtOverRide;
		private LabelEx label3;
	}
}
