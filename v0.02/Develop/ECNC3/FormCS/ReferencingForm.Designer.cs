namespace ECNC3.Views
{
    partial class ReferencingForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label8 = new ECNC3.Views.LabelEx();
            this.ReferenceAxisSelBtCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReferenceOffsetValueCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button_NCStart = new ECNC3.Views.ButtonEx();
            this._currectAngleBt = new ECNC3.Views.ButtonEx();
            this._currAngleText = new ECNC3.Views.LabelEx();
            this._modePanel = new ECNC3.Views.PanelEx();
            this.SubFormABt = new ECNC3.Views.ButtonEx();
            this.SubFormBBt = new ECNC3.Views.ButtonEx();
            this.SubFormCBt = new ECNC3.Views.ButtonEx();
            this.SubFormDBt = new ECNC3.Views.ButtonEx();
            this.SubAImage = new ECNC3.Views.PictureBoxEx();
            this.SubBImage = new ECNC3.Views.PictureBoxEx();
            this.SubCImage = new ECNC3.Views.PictureBoxEx();
            this.SubDImage = new ECNC3.Views.PictureBoxEx();
            this._categoryPanel = new ECNC3.Views.PanelEx();
            this.gaikeiBt = new ECNC3.Views.ButtonEx();
            this.naikeiBt = new ECNC3.Views.ButtonEx();
            this.retBt = new ECNC3.Views.ButtonEx();
            this.sotoBt = new ECNC3.Views.ButtonEx();
            this.tanmenBt = new ECNC3.Views.ButtonEx();
            this.utiBt = new ECNC3.Views.ButtonEx();
            this.panel1 = new ECNC3.Views.PanelEx();
            this.dataGridViewEx2 = new ECNC3.Views.DataGridViewEx();
            this.comboBox_angleCorrectionXY = new ECNC3.Views.ComboBoxEx();
            this.labelEx3 = new ECNC3.Views.LabelEx();
            this.dataGridViewEx1 = new ECNC3.Views.DataGridViewEx();
            this.pictureBox5 = new PictureBoxEx();
            this.labelEx2 = new ECNC3.Views.LabelEx();
            this.pictureBox1 = new PictureBoxEx();
            this._ClosePanel = new ECNC3.Views.PanelEx();
            this._CloseBtn = new ECNC3.Views.ButtonEx();
            this._currectAnglePanel = new ECNC3.Views.PanelEx();
            this._modePanel.SuspendLayout();
            this._categoryPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEx2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEx1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this._ClosePanel.SuspendLayout();
            this._currectAnglePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label8.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.Location = new System.Drawing.Point(9, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(577, 21);
            this.label8.TabIndex = 110;
            this.label8.Text = "REFERENCING";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ReferenceAxisSelBtCol
            // 
            this.ReferenceAxisSelBtCol.HeaderText = "Column1";
            this.ReferenceAxisSelBtCol.Name = "ReferenceAxisSelBtCol";
            this.ReferenceAxisSelBtCol.ReadOnly = true;
            this.ReferenceAxisSelBtCol.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ReferenceAxisSelBtCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ReferenceAxisSelBtCol.Width = 50;
            // 
            // ReferenceOffsetValueCol
            // 
            this.ReferenceOffsetValueCol.HeaderText = "Column2";
            this.ReferenceOffsetValueCol.Name = "ReferenceOffsetValueCol";
            this.ReferenceOffsetValueCol.ReadOnly = true;
            this.ReferenceOffsetValueCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ReferenceOffsetValueCol.Width = 217;
            // 
            // button_NCStart
            // 
            this.button_NCStart.EditBox = null;
            this.button_NCStart.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_NCStart.IsActive = false;
            this.button_NCStart.Location = new System.Drawing.Point(19, 483);
            this.button_NCStart.MultiSelectEn = false;
            this.button_NCStart.Name = "button_NCStart";
            this.button_NCStart.OutLineColor = System.Drawing.Color.Empty;
            this.button_NCStart.OutLineEn = true;
            this.button_NCStart.OutLineSize = 3;
            this.button_NCStart.Selectable = true;
            this.button_NCStart.Size = new System.Drawing.Size(108, 40);
            this.button_NCStart.StatusLedEnable = false;
            this.button_NCStart.StatusLedSize = ((byte)(15));
            this.button_NCStart.TabIndex = 374;
            this.button_NCStart.Text = "デバックスタート";
            this.button_NCStart.UseVisualStyleBackColor = true;
            this.button_NCStart.Visible = false;
            this.button_NCStart.Click += new System.EventHandler(this.button_NCStart_Click_1);
            // 
            // _currectAngleBt
            // 
            this._currectAngleBt.EditBox = null;
            this._currectAngleBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._currectAngleBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._currectAngleBt.IsActive = false;
            this._currectAngleBt.Location = new System.Drawing.Point(6, 6);
            this._currectAngleBt.MultiSelectEn = false;
            this._currectAngleBt.Name = "_currectAngleBt";
            this._currectAngleBt.OutLineColor = System.Drawing.Color.Empty;
            this._currectAngleBt.OutLineEn = true;
            this._currectAngleBt.OutLineSize = 3;
            this._currectAngleBt.Selectable = true;
            this._currectAngleBt.Size = new System.Drawing.Size(133, 37);
            this._currectAngleBt.StatusLedEnable = false;
            this._currectAngleBt.StatusLedSize = ((byte)(15));
            this._currectAngleBt.TabIndex = 361;
            this._currectAngleBt.TabStop = false;
            this._currectAngleBt.Text = "角度補正";
            this._currectAngleBt.UseVisualStyleBackColor = false;
            this._currectAngleBt.Click += new System.EventHandler(this._currectAngleBt_Click);
            // 
            // _currAngleText
            // 
            this._currAngleText.AccessibleRole = System.Windows.Forms.AccessibleRole.IpAddress;
            this._currAngleText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._currAngleText.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._currAngleText.Font = new System.Drawing.Font("Meiryo UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._currAngleText.Location = new System.Drawing.Point(144, 6);
            this._currAngleText.Name = "_currAngleText";
            this._currAngleText.Size = new System.Drawing.Size(133, 37);
            this._currAngleText.TabIndex = 360;
            this._currAngleText.Text = "0";
            this._currAngleText.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // _modePanel
            // 
            this._modePanel.Controls.Add(this.SubFormABt);
            this._modePanel.Controls.Add(this.SubFormBBt);
            this._modePanel.Controls.Add(this.SubFormCBt);
            this._modePanel.Controls.Add(this.SubFormDBt);
            this._modePanel.Controls.Add(this.SubAImage);
            this._modePanel.Controls.Add(this.SubBImage);
            this._modePanel.Controls.Add(this.SubCImage);
            this._modePanel.Controls.Add(this.SubDImage);
            this._modePanel.Font = new System.Drawing.Font("Meiryo UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._modePanel.Location = new System.Drawing.Point(9, 29);
            this._modePanel.Name = "_modePanel";
            this._modePanel.OutLineColor = System.Drawing.Color.DarkOliveGreen;
            this._modePanel.OutLineSize = 2;
            this._modePanel.Size = new System.Drawing.Size(454, 227);
            this._modePanel.TabIndex = 373;
            // 
            // SubFormABt
            // 
            this.SubFormABt.EditBox = null;
            this.SubFormABt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SubFormABt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SubFormABt.IsActive = false;
            this.SubFormABt.Location = new System.Drawing.Point(10, 9);
            this.SubFormABt.MultiSelectEn = false;
            this.SubFormABt.Name = "SubFormABt";
            this.SubFormABt.OutLineColor = System.Drawing.Color.Empty;
            this.SubFormABt.OutLineEn = true;
            this.SubFormABt.OutLineSize = 3;
            this.SubFormABt.Selectable = true;
            this.SubFormABt.Size = new System.Drawing.Size(108, 33);
            this.SubFormABt.StatusLedEnable = false;
            this.SubFormABt.StatusLedSize = ((byte)(15));
            this.SubFormABt.TabIndex = 254;
            this.SubFormABt.Text = "A";
            this.SubFormABt.UseVisualStyleBackColor = false;
            this.SubFormABt.Click += new System.EventHandler(this.SubFormABt_Click);
            // 
            // SubFormBBt
            // 
            this.SubFormBBt.EditBox = null;
            this.SubFormBBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SubFormBBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SubFormBBt.IsActive = false;
            this.SubFormBBt.Location = new System.Drawing.Point(119, 9);
            this.SubFormBBt.MultiSelectEn = false;
            this.SubFormBBt.Name = "SubFormBBt";
            this.SubFormBBt.OutLineColor = System.Drawing.Color.Empty;
            this.SubFormBBt.OutLineEn = true;
            this.SubFormBBt.OutLineSize = 3;
            this.SubFormBBt.Selectable = true;
            this.SubFormBBt.Size = new System.Drawing.Size(108, 33);
            this.SubFormBBt.StatusLedEnable = false;
            this.SubFormBBt.StatusLedSize = ((byte)(15));
            this.SubFormBBt.TabIndex = 255;
            this.SubFormBBt.Text = "B";
            this.SubFormBBt.UseVisualStyleBackColor = false;
            this.SubFormBBt.Click += new System.EventHandler(this.SubFormBBt_Click);
            // 
            // SubFormCBt
            // 
            this.SubFormCBt.EditBox = null;
            this.SubFormCBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SubFormCBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SubFormCBt.IsActive = false;
            this.SubFormCBt.Location = new System.Drawing.Point(228, 9);
            this.SubFormCBt.MultiSelectEn = false;
            this.SubFormCBt.Name = "SubFormCBt";
            this.SubFormCBt.OutLineColor = System.Drawing.Color.Empty;
            this.SubFormCBt.OutLineEn = true;
            this.SubFormCBt.OutLineSize = 3;
            this.SubFormCBt.Selectable = true;
            this.SubFormCBt.Size = new System.Drawing.Size(108, 33);
            this.SubFormCBt.StatusLedEnable = false;
            this.SubFormCBt.StatusLedSize = ((byte)(15));
            this.SubFormCBt.TabIndex = 256;
            this.SubFormCBt.Text = "C";
            this.SubFormCBt.UseVisualStyleBackColor = false;
            this.SubFormCBt.Click += new System.EventHandler(this.SubFormCBt_Click);
            // 
            // SubFormDBt
            // 
            this.SubFormDBt.EditBox = null;
            this.SubFormDBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SubFormDBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SubFormDBt.IsActive = false;
            this.SubFormDBt.Location = new System.Drawing.Point(337, 9);
            this.SubFormDBt.MultiSelectEn = false;
            this.SubFormDBt.Name = "SubFormDBt";
            this.SubFormDBt.OutLineColor = System.Drawing.Color.Empty;
            this.SubFormDBt.OutLineEn = true;
            this.SubFormDBt.OutLineSize = 3;
            this.SubFormDBt.Selectable = true;
            this.SubFormDBt.Size = new System.Drawing.Size(108, 33);
            this.SubFormDBt.StatusLedEnable = false;
            this.SubFormDBt.StatusLedSize = ((byte)(15));
            this.SubFormDBt.TabIndex = 257;
            this.SubFormDBt.Text = "D";
            this.SubFormDBt.UseVisualStyleBackColor = false;
            this.SubFormDBt.Click += new System.EventHandler(this.SubFormDBt_Click);
            // 
            // SubAImage
            // 
            this.SubAImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SubAImage.Location = new System.Drawing.Point(10, 44);
            this.SubAImage.Name = "SubAImage";
            this.SubAImage.Size = new System.Drawing.Size(108, 174);
            this.SubAImage.TabIndex = 258;
            this.SubAImage.Click += new System.EventHandler(this.SubAImage_Click);
            // 
            // SubBImage
            // 
            this.SubBImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SubBImage.Location = new System.Drawing.Point(119, 44);
            this.SubBImage.Name = "SubBImage";
            this.SubBImage.Size = new System.Drawing.Size(108, 174);
            this.SubBImage.TabIndex = 259;
            this.SubBImage.Click += new System.EventHandler(this.SubBImage_Click);
            // 
            // SubCImage
            // 
            this.SubCImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SubCImage.Location = new System.Drawing.Point(228, 44);
            this.SubCImage.Name = "SubCImage";
            this.SubCImage.Size = new System.Drawing.Size(108, 174);
            this.SubCImage.TabIndex = 259;
            this.SubCImage.Click += new System.EventHandler(this.SubCImage_Click);
            // 
            // SubDImage
            // 
            this.SubDImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SubDImage.Location = new System.Drawing.Point(337, 44);
            this.SubDImage.Name = "SubDImage";
            this.SubDImage.Size = new System.Drawing.Size(108, 174);
            this.SubDImage.TabIndex = 259;
            this.SubDImage.Click += new System.EventHandler(this.SubDImage_Click);
            // 
            // _categoryPanel
            // 
            this._categoryPanel.Controls.Add(this.gaikeiBt);
            this._categoryPanel.Controls.Add(this.naikeiBt);
            this._categoryPanel.Controls.Add(this.retBt);
            this._categoryPanel.Controls.Add(this.sotoBt);
            this._categoryPanel.Controls.Add(this.tanmenBt);
            this._categoryPanel.Controls.Add(this.utiBt);
            this._categoryPanel.Font = new System.Drawing.Font("Meiryo UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._categoryPanel.Location = new System.Drawing.Point(469, 29);
            this._categoryPanel.Name = "_categoryPanel";
            this._categoryPanel.OutLineColor = System.Drawing.Color.DarkOliveGreen;
            this._categoryPanel.OutLineSize = 2;
            this._categoryPanel.Size = new System.Drawing.Size(117, 227);
            this._categoryPanel.TabIndex = 373;
            // 
            // gaikeiBt
            // 
            this.gaikeiBt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.gaikeiBt.EditBox = null;
            this.gaikeiBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gaikeiBt.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.gaikeiBt.IsActive = false;
            this.gaikeiBt.Location = new System.Drawing.Point(7, 44);
            this.gaikeiBt.MultiSelectEn = false;
            this.gaikeiBt.Name = "gaikeiBt";
            this.gaikeiBt.OutLineColor = System.Drawing.Color.Empty;
            this.gaikeiBt.OutLineEn = true;
            this.gaikeiBt.OutLineSize = 3;
            this.gaikeiBt.Selectable = true;
            this.gaikeiBt.Size = new System.Drawing.Size(102, 34);
            this.gaikeiBt.StatusLedEnable = false;
            this.gaikeiBt.StatusLedSize = ((byte)(15));
            this.gaikeiBt.TabIndex = 225;
            this.gaikeiBt.Text = "外径";
            this.gaikeiBt.UseVisualStyleBackColor = false;
            this.gaikeiBt.Click += new System.EventHandler(this.gaikeiBt_CheckedChanged);
            // 
            // naikeiBt
            // 
            this.naikeiBt.EditBox = null;
            this.naikeiBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.naikeiBt.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.naikeiBt.IsActive = false;
            this.naikeiBt.Location = new System.Drawing.Point(7, 79);
            this.naikeiBt.MultiSelectEn = false;
            this.naikeiBt.Name = "naikeiBt";
            this.naikeiBt.OutLineColor = System.Drawing.Color.Empty;
            this.naikeiBt.OutLineEn = true;
            this.naikeiBt.OutLineSize = 3;
            this.naikeiBt.Selectable = true;
            this.naikeiBt.Size = new System.Drawing.Size(102, 34);
            this.naikeiBt.StatusLedEnable = false;
            this.naikeiBt.StatusLedSize = ((byte)(15));
            this.naikeiBt.TabIndex = 226;
            this.naikeiBt.Text = "内径";
            this.naikeiBt.UseVisualStyleBackColor = false;
            this.naikeiBt.Click += new System.EventHandler(this.naikeiBt_CheckedChanged);
            // 
            // retBt
            // 
            this.retBt.EditBox = null;
            this.retBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.retBt.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.retBt.IsActive = false;
            this.retBt.Location = new System.Drawing.Point(7, 184);
            this.retBt.MultiSelectEn = false;
            this.retBt.Name = "retBt";
            this.retBt.OutLineColor = System.Drawing.Color.Empty;
            this.retBt.OutLineEn = true;
            this.retBt.OutLineSize = 3;
            this.retBt.Selectable = true;
            this.retBt.Size = new System.Drawing.Size(102, 34);
            this.retBt.StatusLedEnable = false;
            this.retBt.StatusLedSize = ((byte)(15));
            this.retBt.TabIndex = 229;
            this.retBt.Text = "角度補正";
            this.retBt.UseVisualStyleBackColor = false;
            this.retBt.Click += new System.EventHandler(this.retBt_CheckedChanged);
            // 
            // sotoBt
            // 
            this.sotoBt.EditBox = null;
            this.sotoBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sotoBt.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.sotoBt.IsActive = false;
            this.sotoBt.Location = new System.Drawing.Point(7, 114);
            this.sotoBt.MultiSelectEn = false;
            this.sotoBt.Name = "sotoBt";
            this.sotoBt.OutLineColor = System.Drawing.Color.Empty;
            this.sotoBt.OutLineEn = true;
            this.sotoBt.OutLineSize = 3;
            this.sotoBt.Selectable = true;
            this.sotoBt.Size = new System.Drawing.Size(102, 34);
            this.sotoBt.StatusLedEnable = false;
            this.sotoBt.StatusLedSize = ((byte)(15));
            this.sotoBt.TabIndex = 227;
            this.sotoBt.Text = "外コーナー";
            this.sotoBt.UseVisualStyleBackColor = false;
            this.sotoBt.Click += new System.EventHandler(this.sotoBt_CheckedChanged);
            // 
            // tanmenBt
            // 
            this.tanmenBt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.tanmenBt.EditBox = null;
            this.tanmenBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tanmenBt.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tanmenBt.IsActive = false;
            this.tanmenBt.Location = new System.Drawing.Point(7, 9);
            this.tanmenBt.MultiSelectEn = false;
            this.tanmenBt.Name = "tanmenBt";
            this.tanmenBt.OutLineColor = System.Drawing.Color.Empty;
            this.tanmenBt.OutLineEn = true;
            this.tanmenBt.OutLineSize = 3;
            this.tanmenBt.Selectable = true;
            this.tanmenBt.Size = new System.Drawing.Size(102, 34);
            this.tanmenBt.StatusLedEnable = false;
            this.tanmenBt.StatusLedSize = ((byte)(15));
            this.tanmenBt.TabIndex = 224;
            this.tanmenBt.Text = "端面";
            this.tanmenBt.UseVisualStyleBackColor = false;
            this.tanmenBt.Click += new System.EventHandler(this.tanmenBt_CheckedChanged);
            // 
            // utiBt
            // 
            this.utiBt.EditBox = null;
            this.utiBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.utiBt.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.utiBt.IsActive = false;
            this.utiBt.Location = new System.Drawing.Point(7, 149);
            this.utiBt.MultiSelectEn = false;
            this.utiBt.Name = "utiBt";
            this.utiBt.OutLineColor = System.Drawing.Color.Empty;
            this.utiBt.OutLineEn = true;
            this.utiBt.OutLineSize = 3;
            this.utiBt.Selectable = true;
            this.utiBt.Size = new System.Drawing.Size(102, 34);
            this.utiBt.StatusLedEnable = false;
            this.utiBt.StatusLedSize = ((byte)(15));
            this.utiBt.TabIndex = 228;
            this.utiBt.Text = "内コーナー";
            this.utiBt.UseVisualStyleBackColor = false;
            this.utiBt.Click += new System.EventHandler(this.uchiBt_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridViewEx2);
            this.panel1.Controls.Add(this.comboBox_angleCorrectionXY);
            this.panel1.Controls.Add(this.labelEx3);
            this.panel1.Controls.Add(this.dataGridViewEx1);
            this.panel1.Controls.Add(this.pictureBox5);
            this.panel1.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.panel1.Location = new System.Drawing.Point(9, 259);
            this.panel1.Name = "panel1";
            this.panel1.OutLineColor = System.Drawing.Color.DarkOliveGreen;
            this.panel1.OutLineSize = 2;
            this.panel1.Size = new System.Drawing.Size(577, 214);
            this.panel1.TabIndex = 262;
            // 
            // dataGridViewEx2
            // 
            this.dataGridViewEx2.AllowUserToAddRows = false;
            this.dataGridViewEx2.AllowUserToDeleteRows = false;
            this.dataGridViewEx2.ColumnHeadersHeight = 15;
            this.dataGridViewEx2.ColumnHeadersVisible = false;
            this.dataGridViewEx2.EnableLastIndex = true;
            this.dataGridViewEx2.Location = new System.Drawing.Point(272, 27);
            this.dataGridViewEx2.Name = "dataGridViewEx2";
            this.dataGridViewEx2.ReadOnly = true;
            this.dataGridViewEx2.RowHeadersVisible = false;
            this.dataGridViewEx2.RowHeadersWidth = 20;
            this.dataGridViewEx2.RowTemplate.Height = 10;
            this.dataGridViewEx2.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridViewEx2.Size = new System.Drawing.Size(48, 70);
            this.dataGridViewEx2.TabIndex = 375;
            this.dataGridViewEx2.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewEx2_CellMouseUp);
            // 
            // comboBox_angleCorrectionXY
            // 
            this.comboBox_angleCorrectionXY.BackColor = System.Drawing.Color.White;
            this.comboBox_angleCorrectionXY.DropDownWidth = 48;
            this.comboBox_angleCorrectionXY.Font = new System.Drawing.Font("Meiryo UI", 15F);
            this.comboBox_angleCorrectionXY.FormattingEnabled = true;
            this.comboBox_angleCorrectionXY.Items.AddRange(new object[] {
            "X",
            "Y"}); 
            this.comboBox_angleCorrectionXY.Location = new System.Drawing.Point(320, 38);
            this.comboBox_angleCorrectionXY.Name = "comboBox_angleCorrectionXY";
            this.comboBox_angleCorrectionXY.Size = new System.Drawing.Size(48, 33);
            this.comboBox_angleCorrectionXY.TabIndex = 375;
            this.comboBox_angleCorrectionXY.Text = "X";
            this.comboBox_angleCorrectionXY.Visible = false;
            this.comboBox_angleCorrectionXY.SelectedIndexChanged += new System.EventHandler(this.comboBox_angleCorrectionXY_SelectedIndexChanged);
            // 
            // labelEx3
            // 
            this.labelEx3.AccessibleRole = System.Windows.Forms.AccessibleRole.IpAddress;
            this.labelEx3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelEx3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelEx3.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelEx3.Location = new System.Drawing.Point(319, 9);
            this.labelEx3.Name = "labelEx3";
            this.labelEx3.Size = new System.Drawing.Size(250, 17);
            this.labelEx3.TabIndex = 363;
            this.labelEx3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dataGridViewEx1
            // 
            this.dataGridViewEx1.AllowUserToAddRows = false;
            this.dataGridViewEx1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
            this.dataGridViewEx1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewEx1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewEx1.ColumnHeadersHeight = 50;
            this.dataGridViewEx1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridViewEx1.ColumnHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewEx1.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewEx1.EnableLastIndex = true;
            this.dataGridViewEx1.Location = new System.Drawing.Point(319, 28);
            this.dataGridViewEx1.MultiSelect = false;
            this.dataGridViewEx1.Name = "dataGridViewEx1";
            this.dataGridViewEx1.ReadOnly = true;
            this.dataGridViewEx1.RowHeadersVisible = false;
            this.dataGridViewEx1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewEx1.RowTemplate.Height = 50;
            this.dataGridViewEx1.RowTemplate.ReadOnly = true;
            this.dataGridViewEx1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewEx1.ShowRowErrors = false;
            this.dataGridViewEx1.Size = new System.Drawing.Size(250, 177);
            this.dataGridViewEx1.TabIndex = 259;
            this.dataGridViewEx1.TabStop = false;
            this.dataGridViewEx1.Click += new System.EventHandler(this.dataGridViewEx1_Click);
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.Color.Gainsboro;
            this.pictureBox5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox5.Location = new System.Drawing.Point(9, 9);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(292, 196);
            this.pictureBox5.TabIndex = 250;
            this.pictureBox5.TabStop = false;
            // 
            // labelEx2
            // 
            this.labelEx2.AccessibleRole = System.Windows.Forms.AccessibleRole.IpAddress;
            this.labelEx2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelEx2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelEx2.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelEx2.Location = new System.Drawing.Point(8, 170);
            this.labelEx2.Name = "labelEx2";
            this.labelEx2.Size = new System.Drawing.Size(140, 29);
            this.labelEx2.TabIndex = 362;
            this.labelEx2.Text = "deg";
            this.labelEx2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // _ClosePanel
            // 
            this._ClosePanel.Controls.Add(this._CloseBtn);
            this._ClosePanel.Location = new System.Drawing.Point(440, 476);
            this._ClosePanel.Name = "_ClosePanel";
            this._ClosePanel.OutLineColor = System.Drawing.Color.DarkOliveGreen;
            this._ClosePanel.OutLineSize = 2;
            this._ClosePanel.Size = new System.Drawing.Size(145, 49);
            this._ClosePanel.TabIndex = 377;
            // 
            // _CloseBtn
            // 
            this._CloseBtn.EditBox = null;
            this._CloseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._CloseBtn.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._CloseBtn.IsActive = false;
            this._CloseBtn.Location = new System.Drawing.Point(6, 6);
            this._CloseBtn.MultiSelectEn = false;
            this._CloseBtn.Name = "_CloseBtn";
            this._CloseBtn.OutLineColor = System.Drawing.Color.Empty;
            this._CloseBtn.OutLineEn = true;
            this._CloseBtn.OutLineSize = 3;
            this._CloseBtn.Selectable = true;
            this._CloseBtn.Size = new System.Drawing.Size(133, 37);
            this._CloseBtn.StatusLedEnable = false;
            this._CloseBtn.StatusLedSize = ((byte)(15));
            this._CloseBtn.TabIndex = 15;
            this._CloseBtn.Text = "閉じる";
            this._CloseBtn.UseVisualStyleBackColor = false;
            this._CloseBtn.Click += new System.EventHandler(this._CloseBtn_Click);
            // 
            // _currectAnglePanel
            // 
            this._currectAnglePanel.Controls.Add(this._currectAngleBt);
            this._currectAnglePanel.Controls.Add(this._currAngleText);
            this._currectAnglePanel.Location = new System.Drawing.Point(9, 476);
            this._currectAnglePanel.Name = "_currectAnglePanel";
            this._currectAnglePanel.OutLineColor = System.Drawing.Color.DarkOliveGreen;
            this._currectAnglePanel.OutLineSize = 2;
            this._currectAnglePanel.Size = new System.Drawing.Size(283, 49);
            this._currectAnglePanel.TabIndex = 378;
            // 
            // ReferencingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 531);
            this.Controls.Add(this._currectAnglePanel);
            this.Controls.Add(this._ClosePanel);
            this.Controls.Add(this.button_NCStart);
            this.Controls.Add(this._modePanel);
            this.Controls.Add(this._categoryPanel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label8);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(430, 90);
            this.Name = "ReferencingForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ReferencingForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReferencingForm_FormClosing);
            this.Load += new System.EventHandler(this.ReferencingForm_Load);
            this._modePanel.ResumeLayout(false);
            this._categoryPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEx2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEx1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this._ClosePanel.ResumeLayout(false);
            this._currectAnglePanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private LabelEx label8;
        private ButtonEx SubFormABt;
        private ButtonEx SubFormBBt;
        private ButtonEx SubFormCBt;
        private ButtonEx SubFormDBt;
        private ButtonEx tanmenBt;
        private ButtonEx gaikeiBt;
        private ButtonEx naikeiBt;
        private ButtonEx sotoBt;
        private ButtonEx utiBt;
        private ButtonEx retBt;
        private PanelEx panel1;
        private PictureBoxEx pictureBox5;
        private PictureBoxEx SubAImage;
        private PictureBoxEx SubCImage;
        private PictureBoxEx SubDImage;
        private PictureBoxEx SubBImage;
        private LabelEx _currAngleText;
        private ButtonEx _currectAngleBt;
        private LabelEx labelEx2;
        private PanelEx _categoryPanel;
        private PanelEx _modePanel;
        private DataGridViewEx dataGridViewEx1;
        private LabelEx labelEx3;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReferenceAxisSelBtCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReferenceOffsetValueCol;
		private ButtonEx button_NCStart;
        private ComboBoxEx comboBox_angleCorrectionXY;
        private DataGridViewEx dataGridViewEx2;
        private PictureBoxEx pictureBox1;
        private PanelEx _ClosePanel;
        private ButtonEx _CloseBtn;
        private PanelEx _currectAnglePanel;
    }
}