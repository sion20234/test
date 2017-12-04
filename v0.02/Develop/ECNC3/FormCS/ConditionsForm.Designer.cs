namespace ECNC3.Views
{
    partial class ConditionsForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label8 = new ECNC3.Views.LabelEx();
            this.label1 = new ECNC3.Views.LabelEx();
            this.label2 = new ECNC3.Views.LabelEx();
            this._btnCall = new ECNC3.Views.ButtonEx();
            this._btnNumber = new ECNC3.Views.ButtonEx();
            this._edtNumber = new ECNC3.Views.NumericTextBox();
            this._details = new ECNC3.Views.EditProcessCondition();
            this.panel2 = new ECNC3.Views.PanelEx();
            this._btnReturn = new ECNC3.Views.ButtonEx();
            this.panel1 = new ECNC3.Views.PanelEx();
            this._btnFindFree = new ECNC3.Views.ButtonEx();
            this._btnFind = new ECNC3.Views.ButtonEx();
            this._btnDelete = new ECNC3.Views.ButtonEx();
            this._btnRegister = new ECNC3.Views.ButtonEx();
            this._dgList = new ECNC3.Views.DataGridViewEx();
            this.Number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Diameter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Material = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TurnOn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TurnOff = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServoControl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SfrFront = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SfrBack = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Crs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PompValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServoSelect = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PowerSupply = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._PcondImportBtn = new ECNC3.Views.ButtonEx();
            this._PcondExportBtn = new ECNC3.Views.ButtonEx();
            this.panelEx1 = new ECNC3.Views.PanelEx();
            this.panel_CountButton = new ECNC3.Views.PanelEx();
            this.panelEx2 = new ECNC3.Views.PanelEx();
            this.button_100Up = new ECNC3.Views.ButtonEx();
            this.button_10Up = new ECNC3.Views.ButtonEx();
            this.button_10Down = new ECNC3.Views.ButtonEx();
            this.button_100Down = new ECNC3.Views.ButtonEx();
            this._protectCommandPanel = new ECNC3.Views.PanelEx();
            this._protectCommandBtn = new ECNC3.Views.ButtonEx();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dgList)).BeginInit();
            this.panelEx1.SuspendLayout();
            this.panel_CountButton.SuspendLayout();
            this._protectCommandPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label8.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.Location = new System.Drawing.Point(4, 6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1016, 23);
            this.label8.TabIndex = 100;
            this.label8.Text = "PARAMETER";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(3, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 24);
            this.label1.TabIndex = 132;
            this.label1.Text = "LOCKED(0 - 999)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(266, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 24);
            this.label2.TabIndex = 135;
            this.label2.Text = "to";
            // 
            // _btnCall
            // 
            this._btnCall.EditBox = null;
            this._btnCall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnCall.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._btnCall.IsActive = false;
            this._btnCall.LedSyncedBackColorEnable = true;
            this._btnCall.Location = new System.Drawing.Point(12, 496);
            this._btnCall.MultiSelectEn = false;
            this._btnCall.Name = "_btnCall";
            this._btnCall.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._btnCall.OutLineEn = true;
            this._btnCall.OutLineSize = 3F;
            this._btnCall.ProgressBarColor = System.Drawing.Color.Empty;
            this._btnCall.ProgressBarEnable = false;
            this._btnCall.ProgressBarMaxValue = 100;
            this._btnCall.ProgressBarMinValue = 0;
            this._btnCall.ProgressBarSize = 5;
            this._btnCall.ProgressBarValue = 0;
            this._btnCall.Selectable = true;
            this._btnCall.Selected = false;
            this._btnCall.Size = new System.Drawing.Size(86, 106);
            this._btnCall.StatusLedEnable = false;
            this._btnCall.StatusLedSize = ((byte)(15));
            this._btnCall.TabIndex = 326;
            this._btnCall.Text = "呼出";
            this._btnCall.UseVisualStyleBackColor = false;
            this._btnCall.Click += new System.EventHandler(this._btnCall_Click);
            // 
            // _btnNumber
            // 
            this._btnNumber.EditBox = null;
            this._btnNumber.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnNumber.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this._btnNumber.IsActive = false;
            this._btnNumber.LedSyncedBackColorEnable = true;
            this._btnNumber.Location = new System.Drawing.Point(100, 524);
            this._btnNumber.MultiSelectEn = false;
            this._btnNumber.Name = "_btnNumber";
            this._btnNumber.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._btnNumber.OutLineEn = true;
            this._btnNumber.OutLineSize = 3F;
            this._btnNumber.ProgressBarColor = System.Drawing.Color.Empty;
            this._btnNumber.ProgressBarEnable = false;
            this._btnNumber.ProgressBarMaxValue = 100;
            this._btnNumber.ProgressBarMinValue = 0;
            this._btnNumber.ProgressBarSize = 5;
            this._btnNumber.ProgressBarValue = 0;
            this._btnNumber.Selectable = true;
            this._btnNumber.Selected = false;
            this._btnNumber.Size = new System.Drawing.Size(92, 40);
            this._btnNumber.StatusLedEnable = false;
            this._btnNumber.StatusLedSize = ((byte)(15));
            this._btnNumber.TabIndex = 327;
            this._btnNumber.TabStop = false;
            this._btnNumber.Text = "PNo.";
            this._btnNumber.UseVisualStyleBackColor = false;
            this._btnNumber.Click += new System.EventHandler(this._btnNumber_TargetSelect_Click);
            // 
            // _edtNumber
            // 
            this._edtNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._edtNumber.Font = new System.Drawing.Font("Meiryo UI", 18F);
            this._edtNumber.FormatType = ECNC3.Views.NumericTextBox.FormatTypes.Free;
            this._edtNumber.InputDataRatio = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._edtNumber.Location = new System.Drawing.Point(100, 564);
            this._edtNumber.LowerLimit = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this._edtNumber.Name = "_edtNumber";
            this._edtNumber.RawText = "0";
            this._edtNumber.Size = new System.Drawing.Size(92, 38);
            this._edtNumber.TabIndex = 300;
            this._edtNumber.Text = "0";
            this._edtNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this._edtNumber.UpperLimit = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._edtNumber.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._edtNumber.Leave += new System.EventHandler(this._btnNumber_Leave);
            this._edtNumber.Validating += new System.ComponentModel.CancelEventHandler(this._btnNumber_Validating);
            // 
            // _details
            // 
            this._details.CallingFunction = ECNC3.Views.EditProcessCondition.CallingFunctions.Manual;
            this._details.CurrentProcessConditionNumber = 0;
            this._details.Location = new System.Drawing.Point(98, 491);
            this._details.Name = "_details";
            this._details.Protect = 0;
            this._details.Size = new System.Drawing.Size(915, 115);
            this._details.TabIndex = 329;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this._btnReturn);
            this.panel2.Location = new System.Drawing.Point(852, 609);
            this.panel2.Name = "panel2";
            this.panel2.OutLineColor = System.Drawing.Color.DarkOliveGreen;
            this.panel2.OutLineSize = 2F;
            this.panel2.Size = new System.Drawing.Size(160, 57);
            this.panel2.TabIndex = 98;
            // 
            // _btnReturn
            // 
            this._btnReturn.EditBox = null;
            this._btnReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnReturn.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._btnReturn.IsActive = false;
            this._btnReturn.LedSyncedBackColorEnable = true;
            this._btnReturn.Location = new System.Drawing.Point(9, 9);
            this._btnReturn.MultiSelectEn = false;
            this._btnReturn.Name = "_btnReturn";
            this._btnReturn.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._btnReturn.OutLineEn = true;
            this._btnReturn.OutLineSize = 3F;
            this._btnReturn.ProgressBarColor = System.Drawing.Color.Empty;
            this._btnReturn.ProgressBarEnable = false;
            this._btnReturn.ProgressBarMaxValue = 100;
            this._btnReturn.ProgressBarMinValue = 0;
            this._btnReturn.ProgressBarSize = 5;
            this._btnReturn.ProgressBarValue = 0;
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
            // panel1
            // 
            this.panel1.Controls.Add(this._btnFindFree);
            this.panel1.Controls.Add(this._btnFind);
            this.panel1.Controls.Add(this._btnDelete);
            this.panel1.Controls.Add(this._btnRegister);
            this.panel1.Location = new System.Drawing.Point(5, 616);
            this.panel1.Name = "panel1";
            this.panel1.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panel1.OutLineSize = 0F;
            this.panel1.Size = new System.Drawing.Size(548, 82);
            this.panel1.TabIndex = 99;
            // 
            // _btnFindFree
            // 
            this._btnFindFree.EditBox = null;
            this._btnFindFree.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnFindFree.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._btnFindFree.IsActive = false;
            this._btnFindFree.LedSyncedBackColorEnable = true;
            this._btnFindFree.Location = new System.Drawing.Point(411, 4);
            this._btnFindFree.MultiSelectEn = false;
            this._btnFindFree.Name = "_btnFindFree";
            this._btnFindFree.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._btnFindFree.OutLineEn = true;
            this._btnFindFree.OutLineSize = 3F;
            this._btnFindFree.ProgressBarColor = System.Drawing.Color.Empty;
            this._btnFindFree.ProgressBarEnable = false;
            this._btnFindFree.ProgressBarMaxValue = 100;
            this._btnFindFree.ProgressBarMinValue = 0;
            this._btnFindFree.ProgressBarSize = 5;
            this._btnFindFree.ProgressBarValue = 0;
            this._btnFindFree.Selectable = true;
            this._btnFindFree.Selected = false;
            this._btnFindFree.Size = new System.Drawing.Size(133, 50);
            this._btnFindFree.StatusLedEnable = false;
            this._btnFindFree.StatusLedSize = ((byte)(15));
            this._btnFindFree.TabIndex = 18;
            this._btnFindFree.Text = "全表示";
            this._btnFindFree.UseVisualStyleBackColor = false;
            this._btnFindFree.Click += new System.EventHandler(this._btnFindFree_Click);
            // 
            // _btnFind
            // 
            this._btnFind.EditBox = null;
            this._btnFind.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnFind.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._btnFind.IsActive = false;
            this._btnFind.LedSyncedBackColorEnable = true;
            this._btnFind.Location = new System.Drawing.Point(275, 4);
            this._btnFind.MultiSelectEn = false;
            this._btnFind.Name = "_btnFind";
            this._btnFind.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._btnFind.OutLineEn = true;
            this._btnFind.OutLineSize = 3F;
            this._btnFind.ProgressBarColor = System.Drawing.Color.Empty;
            this._btnFind.ProgressBarEnable = false;
            this._btnFind.ProgressBarMaxValue = 100;
            this._btnFind.ProgressBarMinValue = 0;
            this._btnFind.ProgressBarSize = 5;
            this._btnFind.ProgressBarValue = 0;
            this._btnFind.Selectable = true;
            this._btnFind.Selected = false;
            this._btnFind.Size = new System.Drawing.Size(133, 50);
            this._btnFind.StatusLedEnable = false;
            this._btnFind.StatusLedSize = ((byte)(15));
            this._btnFind.TabIndex = 17;
            this._btnFind.Text = "検索";
            this._btnFind.UseVisualStyleBackColor = false;
            this._btnFind.Click += new System.EventHandler(this._btnFind_Click);
            // 
            // _btnDelete
            // 
            this._btnDelete.EditBox = null;
            this._btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnDelete.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._btnDelete.IsActive = false;
            this._btnDelete.LedSyncedBackColorEnable = true;
            this._btnDelete.Location = new System.Drawing.Point(139, 4);
            this._btnDelete.MultiSelectEn = false;
            this._btnDelete.Name = "_btnDelete";
            this._btnDelete.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._btnDelete.OutLineEn = true;
            this._btnDelete.OutLineSize = 3F;
            this._btnDelete.ProgressBarColor = System.Drawing.Color.Empty;
            this._btnDelete.ProgressBarEnable = false;
            this._btnDelete.ProgressBarMaxValue = 100;
            this._btnDelete.ProgressBarMinValue = 0;
            this._btnDelete.ProgressBarSize = 5;
            this._btnDelete.ProgressBarValue = 0;
            this._btnDelete.Selectable = true;
            this._btnDelete.Selected = false;
            this._btnDelete.Size = new System.Drawing.Size(133, 50);
            this._btnDelete.StatusLedEnable = false;
            this._btnDelete.StatusLedSize = ((byte)(15));
            this._btnDelete.TabIndex = 15;
            this._btnDelete.Text = "削除";
            this._btnDelete.UseVisualStyleBackColor = false;
            this._btnDelete.Click += new System.EventHandler(this._btnDelete_Click);
            // 
            // _btnRegister
            // 
            this._btnRegister.EditBox = null;
            this._btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnRegister.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._btnRegister.IsActive = false;
            this._btnRegister.LedSyncedBackColorEnable = true;
            this._btnRegister.Location = new System.Drawing.Point(3, 4);
            this._btnRegister.MultiSelectEn = false;
            this._btnRegister.Name = "_btnRegister";
            this._btnRegister.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._btnRegister.OutLineEn = true;
            this._btnRegister.OutLineSize = 3F;
            this._btnRegister.ProgressBarColor = System.Drawing.Color.Empty;
            this._btnRegister.ProgressBarEnable = false;
            this._btnRegister.ProgressBarMaxValue = 100;
            this._btnRegister.ProgressBarMinValue = 0;
            this._btnRegister.ProgressBarSize = 5;
            this._btnRegister.ProgressBarValue = 0;
            this._btnRegister.Selectable = true;
            this._btnRegister.Selected = false;
            this._btnRegister.Size = new System.Drawing.Size(133, 50);
            this._btnRegister.StatusLedEnable = false;
            this._btnRegister.StatusLedSize = ((byte)(15));
            this._btnRegister.TabIndex = 16;
            this._btnRegister.Text = "登録";
            this._btnRegister.UseVisualStyleBackColor = false;
            this._btnRegister.Click += new System.EventHandler(this._btnRegister_Click);
            // 
            // _dgList
            // 
            this._dgList.AllowUserToAddRows = false;
            this._dgList.AllowUserToDeleteRows = false;
            this._dgList.AllowUserToOrderColumns = true;
            this._dgList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Number,
            this.Diameter,
            this.Material,
            this.Comment,
            this.TurnOn,
            this.TurnOff,
            this.Ip,
            this.Cap,
            this.ServoControl,
            this.SfrFront,
            this.SfrBack,
            this.Crs,
            this.Pol,
            this.PompValue,
            this.ServoSelect,
            this.PowerSupply});
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._dgList.DefaultCellStyle = dataGridViewCellStyle18;
            this._dgList.EnableLastIndex = true;
            this._dgList.Location = new System.Drawing.Point(12, 32);
            this._dgList.MultiSelect = false;
            this._dgList.Name = "_dgList";
            this._dgList.RowTemplate.Height = 21;
            this._dgList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._dgList.Size = new System.Drawing.Size(843, 450);
            this._dgList.TabIndex = 328;
            this._dgList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this._dgList_CellClick);
            this._dgList.SelectionChanged += new System.EventHandler(this._dgList_SelectionChanged);
            // 
            // Number
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle13.Format = "N0";
            dataGridViewCellStyle13.NullValue = "???";
            this.Number.DefaultCellStyle = dataGridViewCellStyle13;
            this.Number.Frozen = true;
            this.Number.HeaderText = "P";
            this.Number.MaxInputLength = 3;
            this.Number.Name = "Number";
            this.Number.ReadOnly = true;
            this.Number.Width = 40;
            // 
            // Diameter
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle14.Format = "N2";
            dataGridViewCellStyle14.NullValue = "-.--";
            this.Diameter.DefaultCellStyle = dataGridViewCellStyle14;
            this.Diameter.HeaderText = "電極径";
            this.Diameter.MaxInputLength = 4;
            this.Diameter.Name = "Diameter";
            this.Diameter.Width = 75;
            // 
            // Material
            // 
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Material.DefaultCellStyle = dataGridViewCellStyle15;
            this.Material.HeaderText = "材質";
            this.Material.MaxInputLength = 32;
            this.Material.Name = "Material";
            this.Material.Width = 120;
            // 
            // Comment
            // 
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Comment.DefaultCellStyle = dataGridViewCellStyle16;
            this.Comment.HeaderText = "コメント";
            this.Comment.MaxInputLength = 128;
            this.Comment.Name = "Comment";
            this.Comment.Width = 200;
            // 
            // TurnOn
            // 
            this.TurnOn.HeaderText = "T-ON";
            this.TurnOn.MaxInputLength = 3;
            this.TurnOn.Name = "TurnOn";
            this.TurnOn.Width = 50;
            // 
            // TurnOff
            // 
            this.TurnOff.HeaderText = "T-OFF";
            this.TurnOff.MaxInputLength = 3;
            this.TurnOff.Name = "TurnOff";
            this.TurnOff.Width = 50;
            // 
            // Ip
            // 
            this.Ip.HeaderText = "IP";
            this.Ip.MaxInputLength = 3;
            this.Ip.Name = "Ip";
            this.Ip.Width = 45;
            // 
            // Cap
            // 
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle17.NullValue = null;
            this.Cap.DefaultCellStyle = dataGridViewCellStyle17;
            this.Cap.HeaderText = "CAP";
            this.Cap.MaxInputLength = 5;
            this.Cap.Name = "Cap";
            this.Cap.Width = 65;
            // 
            // ServoControl
            // 
            this.ServoControl.HeaderText = "SC";
            this.ServoControl.MaxInputLength = 2;
            this.ServoControl.Name = "ServoControl";
            this.ServoControl.Width = 40;
            // 
            // SfrFront
            // 
            this.SfrFront.HeaderText = "SFR-D";
            this.SfrFront.MaxInputLength = 3;
            this.SfrFront.Name = "SfrFront";
            this.SfrFront.Width = 50;
            // 
            // SfrBack
            // 
            this.SfrBack.HeaderText = "SFR-U";
            this.SfrBack.MaxInputLength = 3;
            this.SfrBack.Name = "SfrBack";
            this.SfrBack.Width = 50;
            // 
            // Crs
            // 
            this.Crs.HeaderText = "CRS";
            this.Crs.MaxInputLength = 2;
            this.Crs.Name = "Crs";
            this.Crs.Width = 40;
            // 
            // Pol
            // 
            this.Pol.HeaderText = "POL";
            this.Pol.MaxInputLength = 1;
            this.Pol.Name = "Pol";
            this.Pol.Width = 40;
            // 
            // PompValue
            // 
            this.PompValue.HeaderText = "Pomp";
            this.PompValue.MaxInputLength = 2;
            this.PompValue.Name = "PompValue";
            // 
            // ServoSelect
            // 
            this.ServoSelect.HeaderText = "SvoSel";
            this.ServoSelect.MaxInputLength = 1;
            this.ServoSelect.Name = "ServoSelect";
            // 
            // PowerSupply
            // 
            this.PowerSupply.HeaderText = "PS";
            this.PowerSupply.MaxInputLength = 1;
            this.PowerSupply.Name = "PowerSupply";
            // 
            // _PcondImportBtn
            // 
            this._PcondImportBtn.EditBox = null;
            this._PcondImportBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._PcondImportBtn.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._PcondImportBtn.IsActive = false;
            this._PcondImportBtn.LedSyncedBackColorEnable = true;
            this._PcondImportBtn.Location = new System.Drawing.Point(8, 5);
            this._PcondImportBtn.MultiSelectEn = false;
            this._PcondImportBtn.Name = "_PcondImportBtn";
            this._PcondImportBtn.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._PcondImportBtn.OutLineEn = true;
            this._PcondImportBtn.OutLineSize = 3F;
            this._PcondImportBtn.ProgressBarColor = System.Drawing.Color.Empty;
            this._PcondImportBtn.ProgressBarEnable = false;
            this._PcondImportBtn.ProgressBarMaxValue = 100;
            this._PcondImportBtn.ProgressBarMinValue = 0;
            this._PcondImportBtn.ProgressBarSize = 5;
            this._PcondImportBtn.ProgressBarValue = 0;
            this._PcondImportBtn.Selectable = true;
            this._PcondImportBtn.Selected = false;
            this._PcondImportBtn.Size = new System.Drawing.Size(120, 48);
            this._PcondImportBtn.StatusLedEnable = false;
            this._PcondImportBtn.StatusLedSize = ((byte)(15));
            this._PcondImportBtn.TabIndex = 331;
            this._PcondImportBtn.Text = "インポート";
            this._PcondImportBtn.UseVisualStyleBackColor = false;
            this._PcondImportBtn.Click += new System.EventHandler(this._PcondImportBtn_Click);
            // 
            // _PcondExportBtn
            // 
            this._PcondExportBtn.EditBox = null;
            this._PcondExportBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._PcondExportBtn.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._PcondExportBtn.IsActive = false;
            this._PcondExportBtn.LedSyncedBackColorEnable = true;
            this._PcondExportBtn.Location = new System.Drawing.Point(8, 59);
            this._PcondExportBtn.MultiSelectEn = false;
            this._PcondExportBtn.Name = "_PcondExportBtn";
            this._PcondExportBtn.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._PcondExportBtn.OutLineEn = true;
            this._PcondExportBtn.OutLineSize = 3F;
            this._PcondExportBtn.ProgressBarColor = System.Drawing.Color.Empty;
            this._PcondExportBtn.ProgressBarEnable = false;
            this._PcondExportBtn.ProgressBarMaxValue = 100;
            this._PcondExportBtn.ProgressBarMinValue = 0;
            this._PcondExportBtn.ProgressBarSize = 5;
            this._PcondExportBtn.ProgressBarValue = 0;
            this._PcondExportBtn.Selectable = true;
            this._PcondExportBtn.Selected = false;
            this._PcondExportBtn.Size = new System.Drawing.Size(120, 48);
            this._PcondExportBtn.StatusLedEnable = false;
            this._PcondExportBtn.StatusLedSize = ((byte)(15));
            this._PcondExportBtn.TabIndex = 330;
            this._PcondExportBtn.Text = "エクスポート";
            this._PcondExportBtn.UseVisualStyleBackColor = false;
            this._PcondExportBtn.Click += new System.EventHandler(this._PcondExportBtn_Click);
            // 
            // panelEx1
            // 
            this.panelEx1.Controls.Add(this._PcondImportBtn);
            this.panelEx1.Controls.Add(this._PcondExportBtn);
            this.panelEx1.Location = new System.Drawing.Point(877, 373);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panelEx1.OutLineSize = 0F;
            this.panelEx1.Size = new System.Drawing.Size(135, 112);
            this.panelEx1.TabIndex = 100;
            // 
            // panel_CountButton
            // 
            this.panel_CountButton.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel_CountButton.Controls.Add(this.panelEx2);
            this.panel_CountButton.Controls.Add(this.button_100Up);
            this.panel_CountButton.Controls.Add(this.button_10Up);
            this.panel_CountButton.Controls.Add(this.button_10Down);
            this.panel_CountButton.Controls.Add(this.button_100Down);
            this.panel_CountButton.Location = new System.Drawing.Point(877, 32);
            this.panel_CountButton.Name = "panel_CountButton";
            this.panel_CountButton.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panel_CountButton.OutLineSize = 0F;
            this.panel_CountButton.Size = new System.Drawing.Size(135, 237);
            this.panel_CountButton.TabIndex = 330;
            // 
            // panelEx2
            // 
            this.panelEx2.Location = new System.Drawing.Point(-3, 114);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panelEx2.OutLineSize = 0F;
            this.panelEx2.Size = new System.Drawing.Size(158, 5);
            this.panelEx2.TabIndex = 225;
            // 
            // button_100Up
            // 
            this.button_100Up.EditBox = null;
            this.button_100Up.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_100Up.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.button_100Up.IsActive = false;
            this.button_100Up.LedSyncedBackColorEnable = true;
            this.button_100Up.Location = new System.Drawing.Point(6, 6);
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
            this.button_100Up.Size = new System.Drawing.Size(120, 48);
            this.button_100Up.StatusLedEnable = false;
            this.button_100Up.StatusLedSize = ((byte)(15));
            this.button_100Up.TabIndex = 216;
            this.button_100Up.Text = "100▲";
            this.button_100Up.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_100Up.UseVisualStyleBackColor = true;
            this.button_100Up.Click += new System.EventHandler(this.button_100Up_Click);
            // 
            // button_10Up
            // 
            this.button_10Up.EditBox = null;
            this.button_10Up.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_10Up.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.button_10Up.IsActive = false;
            this.button_10Up.LedSyncedBackColorEnable = true;
            this.button_10Up.Location = new System.Drawing.Point(6, 59);
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
            this.button_10Up.Size = new System.Drawing.Size(120, 48);
            this.button_10Up.StatusLedEnable = false;
            this.button_10Up.StatusLedSize = ((byte)(15));
            this.button_10Up.TabIndex = 217;
            this.button_10Up.Text = "10▲";
            this.button_10Up.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_10Up.UseVisualStyleBackColor = true;
            this.button_10Up.Click += new System.EventHandler(this.button_10Up_Click);
            // 
            // button_10Down
            // 
            this.button_10Down.EditBox = null;
            this.button_10Down.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_10Down.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.button_10Down.IsActive = false;
            this.button_10Down.LedSyncedBackColorEnable = true;
            this.button_10Down.Location = new System.Drawing.Point(6, 126);
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
            this.button_10Down.Size = new System.Drawing.Size(120, 48);
            this.button_10Down.StatusLedEnable = false;
            this.button_10Down.StatusLedSize = ((byte)(15));
            this.button_10Down.TabIndex = 218;
            this.button_10Down.Text = "10▼";
            this.button_10Down.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_10Down.UseVisualStyleBackColor = true;
            this.button_10Down.Click += new System.EventHandler(this.button_10Down_Click);
            // 
            // button_100Down
            // 
            this.button_100Down.EditBox = null;
            this.button_100Down.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_100Down.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.button_100Down.IsActive = false;
            this.button_100Down.LedSyncedBackColorEnable = true;
            this.button_100Down.Location = new System.Drawing.Point(6, 179);
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
            this.button_100Down.Size = new System.Drawing.Size(120, 48);
            this.button_100Down.StatusLedEnable = false;
            this.button_100Down.StatusLedSize = ((byte)(15));
            this.button_100Down.TabIndex = 219;
            this.button_100Down.Text = "100▼";
            this.button_100Down.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_100Down.UseVisualStyleBackColor = true;
            this.button_100Down.Click += new System.EventHandler(this.button_100Down_Click);
            // 
            // _protectCommandPanel
            // 
            this._protectCommandPanel.Controls.Add(this._protectCommandBtn);
            this._protectCommandPanel.Location = new System.Drawing.Point(877, 292);
            this._protectCommandPanel.Name = "_protectCommandPanel";
            this._protectCommandPanel.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._protectCommandPanel.OutLineSize = 0F;
            this._protectCommandPanel.Size = new System.Drawing.Size(135, 62);
            this._protectCommandPanel.TabIndex = 332;
            // 
            // _protectCommandBtn
            // 
            this._protectCommandBtn.EditBox = null;
            this._protectCommandBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._protectCommandBtn.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._protectCommandBtn.IsActive = false;
            this._protectCommandBtn.LedSyncedBackColorEnable = true;
            this._protectCommandBtn.Location = new System.Drawing.Point(8, 7);
            this._protectCommandBtn.MultiSelectEn = false;
            this._protectCommandBtn.Name = "_protectCommandBtn";
            this._protectCommandBtn.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._protectCommandBtn.OutLineEn = true;
            this._protectCommandBtn.OutLineSize = 3F;
            this._protectCommandBtn.ProgressBarColor = System.Drawing.Color.Empty;
            this._protectCommandBtn.ProgressBarEnable = false;
            this._protectCommandBtn.ProgressBarMaxValue = 100;
            this._protectCommandBtn.ProgressBarMinValue = 0;
            this._protectCommandBtn.ProgressBarSize = 5;
            this._protectCommandBtn.ProgressBarValue = 0;
            this._protectCommandBtn.Selectable = true;
            this._protectCommandBtn.Selected = false;
            this._protectCommandBtn.Size = new System.Drawing.Size(120, 48);
            this._protectCommandBtn.StatusLedEnable = false;
            this._protectCommandBtn.StatusLedSize = ((byte)(15));
            this._protectCommandBtn.TabIndex = 331;
            this._protectCommandBtn.Text = "ロック";
            this._protectCommandBtn.UseVisualStyleBackColor = false;
            this._protectCommandBtn.MouseUp += new System.Windows.Forms.MouseEventHandler(this._protectCommandBtn_MouseUp);
            // 
            // ConditionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 678);
            this.Controls.Add(this._protectCommandPanel);
            this.Controls.Add(this.panel_CountButton);
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this._btnCall);
            this.Controls.Add(this._btnNumber);
            this.Controls.Add(this._edtNumber);
            this.Controls.Add(this._details);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this._dgList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(0, 90);
            this.Name = "ConditionsForm";
            this.OutLineSize = 3;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Activated += new System.EventHandler(this.ConditionsForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConditionsForm_FormClosing);
            this.Load += new System.EventHandler(this.ConditionsForm_Load);
            this.VisibleChanged += new System.EventHandler(this.ConditionsForm_VisibleChanged);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._dgList)).EndInit();
            this.panelEx1.ResumeLayout(false);
            this.panel_CountButton.ResumeLayout(false);
            this._protectCommandPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ECNC3.Views.PanelEx panel1;
        private ECNC3.Views.PanelEx panel2;
        private ButtonEx _btnReturn;
        private LabelEx label8;
        private LabelEx label1;
        private LabelEx label2;
        private NumericTextBox _edtNumber;
        private ButtonEx _btnCall;
        private DataGridViewEx _dgList;
        private ButtonEx _btnNumber;
        private EditProcessCondition _details;
        private ButtonEx _btnFindFree;
        private ButtonEx _btnFind;
        private ButtonEx _btnDelete;
        private ButtonEx _btnRegister;
        private ButtonEx _PcondImportBtn;
        private ButtonEx _PcondExportBtn;
        private PanelEx panelEx1;
		private PanelEx panel_CountButton;
		private ButtonEx button_100Up;
		private ButtonEx button_10Up;
		private ButtonEx button_10Down;
		private ButtonEx button_100Down;
		private PanelEx panelEx2;
        private PanelEx _protectCommandPanel;
        private ButtonEx _protectCommandBtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Number;
        private System.Windows.Forms.DataGridViewTextBoxColumn Diameter;
        private System.Windows.Forms.DataGridViewTextBoxColumn Material;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comment;
        private System.Windows.Forms.DataGridViewTextBoxColumn TurnOn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TurnOff;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ip;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cap;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServoControl;
        private System.Windows.Forms.DataGridViewTextBoxColumn SfrFront;
        private System.Windows.Forms.DataGridViewTextBoxColumn SfrBack;
        private System.Windows.Forms.DataGridViewTextBoxColumn Crs;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pol;
        private System.Windows.Forms.DataGridViewTextBoxColumn PompValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServoSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn PowerSupply;
    }
}