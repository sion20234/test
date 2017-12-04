namespace ECNC3.Views
{
    partial class IOCheckForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label8 = new ECNC3.Views.LabelEx();
            this.label1 = new ECNC3.Views.LabelEx();
            this.panel2 = new ECNC3.Views.PanelEx();
            this.buttonEx_Return = new ECNC3.Views.ButtonEx();
            this.panel8 = new ECNC3.Views.PanelEx();
            this.buttonEx_Reset = new ECNC3.Views.ButtonEx();
            this.label51 = new ECNC3.Views.LabelEx();
            this.panel1 = new ECNC3.Views.PanelEx();
            this.radioButtonEx_Output = new ECNC3.Views.RadioButtonEx();
            this.radioButtonEx_Input = new ECNC3.Views.RadioButtonEx();
            this.panel3 = new ECNC3.Views.PanelEx();
            this.radioButtonEx_SelectDsp = new ECNC3.Views.RadioButtonEx();
            this.radioButtonEx_AllDsp = new ECNC3.Views.RadioButtonEx();
            this._dgList = new ECNC3.Views.DataGridViewEx();
            this.ColumnButton = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Names = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Note = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.radioButtonEx_Force = new ECNC3.Views.RadioButtonEx();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel2.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dgList)).BeginInit();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label8.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.Location = new System.Drawing.Point(7, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1012, 26);
            this.label8.TabIndex = 86;
            this.label8.Text = "I/O CHECK";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Meiryo UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(234, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 37);
            this.label1.TabIndex = 87;
            this.label1.Text = "！！警告！！";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.buttonEx_Return);
            this.panel2.Location = new System.Drawing.Point(867, 618);
            this.panel2.Name = "panel2";
            this.panel2.OutLineColor = System.Drawing.Color.DarkOliveGreen;
            this.panel2.OutLineSize = 2;
            this.panel2.Size = new System.Drawing.Size(150, 48);
            this.panel2.TabIndex = 213;
            // 
            // buttonEx_Return
            // 
            this.buttonEx_Return.EditBox = null;
            this.buttonEx_Return.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
            this.buttonEx_Return.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEx_Return.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonEx_Return.IsActive = false;
            this.buttonEx_Return.Location = new System.Drawing.Point(4, 2);
            this.buttonEx_Return.MultiSelectEn = false;
            this.buttonEx_Return.Name = "buttonEx_Return";
            this.buttonEx_Return.OutLineColor = System.Drawing.Color.Empty;
            this.buttonEx_Return.OutLineEn = true;
            this.buttonEx_Return.OutLineSize = 3;
            this.buttonEx_Return.Selectable = true;
            this.buttonEx_Return.Selected = false;
            this.buttonEx_Return.Size = new System.Drawing.Size(139, 39);
            this.buttonEx_Return.StatusLedEnable = false;
            this.buttonEx_Return.StatusLedSize = ((byte)(15));
            this.buttonEx_Return.TabIndex = 275;
            this.buttonEx_Return.Text = "閉じる";
            this.buttonEx_Return.UseVisualStyleBackColor = false;
            this.buttonEx_Return.Click += new System.EventHandler(this.buttonEx_Return_Click);
            // 
            // panel8
            // 
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel8.Controls.Add(this.buttonEx_Reset);
            this.panel8.Location = new System.Drawing.Point(867, 561);
            this.panel8.Name = "panel8";
            this.panel8.OutLineColor = System.Drawing.Color.Empty;
            this.panel8.OutLineSize = 0;
            this.panel8.Size = new System.Drawing.Size(150, 51);
            this.panel8.TabIndex = 214;
            // 
            // buttonEx_Reset
            // 
            this.buttonEx_Reset.EditBox = null;
            this.buttonEx_Reset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
            this.buttonEx_Reset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEx_Reset.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonEx_Reset.IsActive = false;
            this.buttonEx_Reset.Location = new System.Drawing.Point(4, 4);
            this.buttonEx_Reset.MultiSelectEn = false;
            this.buttonEx_Reset.Name = "buttonEx_Reset";
            this.buttonEx_Reset.OutLineColor = System.Drawing.Color.Empty;
            this.buttonEx_Reset.OutLineEn = true;
            this.buttonEx_Reset.OutLineSize = 3;
            this.buttonEx_Reset.Selectable = true;
            this.buttonEx_Reset.Selected = false;
            this.buttonEx_Reset.Size = new System.Drawing.Size(139, 39);
            this.buttonEx_Reset.StatusLedEnable = false;
            this.buttonEx_Reset.StatusLedSize = ((byte)(15));
            this.buttonEx_Reset.TabIndex = 274;
            this.buttonEx_Reset.Text = "リセット";
            this.buttonEx_Reset.UseVisualStyleBackColor = false;
            this.buttonEx_Reset.Click += new System.EventHandler(this.buttonEx_Reset_Click);
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label51.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label51.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label51.Location = new System.Drawing.Point(417, 44);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(400, 42);
            this.label51.TabIndex = 269;
            this.label51.Text = "この画面を表示中は、アラーム監視処理を一切行いません。\r\nこの画面での通常運用は、行わないで下さい。";
            this.label51.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.radioButtonEx_Output);
            this.panel1.Controls.Add(this.radioButtonEx_Input);
            this.panel1.Location = new System.Drawing.Point(866, 217);
            this.panel1.Name = "panel1";
            this.panel1.OutLineColor = System.Drawing.Color.Empty;
            this.panel1.OutLineSize = 0;
            this.panel1.Size = new System.Drawing.Size(151, 96);
            this.panel1.TabIndex = 219;
            // 
            // radioButtonEx_Output
            // 
            this.radioButtonEx_Output.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonEx_Output.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this.radioButtonEx_Output.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
            this.radioButtonEx_Output.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.radioButtonEx_Output.Location = new System.Drawing.Point(3, 51);
            this.radioButtonEx_Output.Name = "radioButtonEx_Output";
            this.radioButtonEx_Output.Size = new System.Drawing.Size(140, 39);
            this.radioButtonEx_Output.TabIndex = 270;
            this.radioButtonEx_Output.TabStop = true;
            this.radioButtonEx_Output.Text = "出力";
            this.radioButtonEx_Output.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButtonEx_Output.UseVisualStyleBackColor = false;
            this.radioButtonEx_Output.CheckedChanged += new System.EventHandler(this._rdoInput_CheckedChanged);
            // 
            // radioButtonEx_Input
            // 
            this.radioButtonEx_Input.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonEx_Input.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this.radioButtonEx_Input.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
            this.radioButtonEx_Input.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.radioButtonEx_Input.Location = new System.Drawing.Point(4, 6);
            this.radioButtonEx_Input.Name = "radioButtonEx_Input";
            this.radioButtonEx_Input.Size = new System.Drawing.Size(140, 39);
            this.radioButtonEx_Input.TabIndex = 270;
            this.radioButtonEx_Input.TabStop = true;
            this.radioButtonEx_Input.Text = "入力";
            this.radioButtonEx_Input.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButtonEx_Input.UseVisualStyleBackColor = false;
            this.radioButtonEx_Input.CheckedChanged += new System.EventHandler(this._rdoInput_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.radioButtonEx_SelectDsp);
            this.panel3.Controls.Add(this.radioButtonEx_AllDsp);
            this.panel3.Location = new System.Drawing.Point(866, 94);
            this.panel3.Name = "panel3";
            this.panel3.OutLineColor = System.Drawing.Color.Empty;
            this.panel3.OutLineSize = 0;
            this.panel3.Size = new System.Drawing.Size(151, 96);
            this.panel3.TabIndex = 271;
            // 
            // radioButtonEx_SelectDsp
            // 
            this.radioButtonEx_SelectDsp.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonEx_SelectDsp.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this.radioButtonEx_SelectDsp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
            this.radioButtonEx_SelectDsp.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.radioButtonEx_SelectDsp.Location = new System.Drawing.Point(3, 50);
            this.radioButtonEx_SelectDsp.Name = "radioButtonEx_SelectDsp";
            this.radioButtonEx_SelectDsp.Size = new System.Drawing.Size(140, 39);
            this.radioButtonEx_SelectDsp.TabIndex = 274;
            this.radioButtonEx_SelectDsp.TabStop = true;
            this.radioButtonEx_SelectDsp.Text = "選択表示";
            this.radioButtonEx_SelectDsp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButtonEx_SelectDsp.UseVisualStyleBackColor = false;
            this.radioButtonEx_SelectDsp.Click += new System.EventHandler(this.radioButtonEx_SelectDsp_Click);
            // 
            // radioButtonEx_AllDsp
            // 
            this.radioButtonEx_AllDsp.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonEx_AllDsp.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this.radioButtonEx_AllDsp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
            this.radioButtonEx_AllDsp.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.radioButtonEx_AllDsp.Location = new System.Drawing.Point(3, 5);
            this.radioButtonEx_AllDsp.Name = "radioButtonEx_AllDsp";
            this.radioButtonEx_AllDsp.Size = new System.Drawing.Size(140, 39);
            this.radioButtonEx_AllDsp.TabIndex = 273;
            this.radioButtonEx_AllDsp.TabStop = true;
            this.radioButtonEx_AllDsp.Text = "全表示";
            this.radioButtonEx_AllDsp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButtonEx_AllDsp.UseVisualStyleBackColor = false;
            this.radioButtonEx_AllDsp.Click += new System.EventHandler(this.radioButtonEx_AllDsp_Click);
            // 
            // _dgList
            // 
            this._dgList.AllowUserToAddRows = false;
            this._dgList.AllowUserToDeleteRows = false;
            this._dgList.AllowUserToOrderColumns = true;
            this._dgList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dgList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnButton,
            this.Names,
            this.Address,
            this.Note,
            this.IO}); 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Meiryo UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._dgList.DefaultCellStyle = dataGridViewCellStyle2;
            this._dgList.EnableLastIndex = true;
            this._dgList.Location = new System.Drawing.Point(12, 94);
            this._dgList.Name = "_dgList";
            this._dgList.RowHeadersVisible = false;
            this._dgList.RowTemplate.Height = 21;
            this._dgList.Size = new System.Drawing.Size(849, 572);
            this._dgList.TabIndex = 224;
            this._dgList.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this._dgList_CellMouseDown);
            this._dgList.SelectionChanged += new System.EventHandler(this._dgList_SelectionChanged);
            // 
            // ColumnButton
            // 
            this.ColumnButton.HeaderText = "選択";
            this.ColumnButton.Name = "ColumnButton";
            this.ColumnButton.Text = "";
            this.ColumnButton.Width = 70;
            // 
            // Names
            // 
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Meiryo UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Names.DefaultCellStyle = dataGridViewCellStyle1;
            this.Names.HeaderText = "信号名";
            this.Names.Name = "Names";
            this.Names.Width = 200;
            // 
            // Address
            // 
            this.Address.HeaderText = "アドレス";
            this.Address.Name = "Address";
            this.Address.Width = 200;
            // 
            // Note
            // 
            this.Note.HeaderText = "補足";
            this.Note.Name = "Note";
            this.Note.Width = 550;
            // 
            // IO
            // 
            this.IO.HeaderText = "I/O";
            this.IO.Name = "IO";
            this.IO.Visible = false;
            // 
            // radioButtonEx_Force
            // 
            this.radioButtonEx_Force.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonEx_Force.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this.radioButtonEx_Force.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
            this.radioButtonEx_Force.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.radioButtonEx_Force.Location = new System.Drawing.Point(871, 360);
            this.radioButtonEx_Force.Name = "radioButtonEx_Force";
            this.radioButtonEx_Force.Size = new System.Drawing.Size(140, 39);
            this.radioButtonEx_Force.TabIndex = 272;
            this.radioButtonEx_Force.TabStop = true;
            this.radioButtonEx_Force.Text = "強制";
            this.radioButtonEx_Force.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButtonEx_Force.UseVisualStyleBackColor = false;
            this.radioButtonEx_Force.CheckedChanged += new System.EventHandler(this.radioButtonEx_Force_CheckedChanged);
            this.radioButtonEx_Force.Click += new System.EventHandler(this.radioButtonEx_Force_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(262, 356);
            this.progressBar1.Maximum = 99999;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(500, 50);
            this.progressBar1.TabIndex = 273;
            this.progressBar1.Visible = false;
            // 
            // IOCheckForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 678);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.radioButtonEx_Force);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this._dgList);
            this.Controls.Add(this.label51);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label8);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(0, 90);
            this.Name = "IOCheckForm";
            this.OutLineSize = 3;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "IOCheckForm";
            this.Load += new System.EventHandler(this.IOCheckForm_Load);
            this.panel2.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._dgList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LabelEx label8;
        private LabelEx label1;
        private PanelEx panel2;
        private PanelEx panel8;
        private LabelEx label51;
        private PanelEx panel1;
        private DataGridViewEx _dgList;
        private PanelEx panel3;
		private ButtonEx buttonEx_Return;
		private ButtonEx buttonEx_Reset;
        private RadioButtonEx radioButtonEx_Output;
		private RadioButtonEx radioButtonEx_Input;
		private RadioButtonEx radioButtonEx_SelectDsp;
		private RadioButtonEx radioButtonEx_AllDsp;
		private System.Windows.Forms.DataGridViewButtonColumn ColumnButton;
		private System.Windows.Forms.DataGridViewTextBoxColumn Names;
		private System.Windows.Forms.DataGridViewTextBoxColumn Address;
		private System.Windows.Forms.DataGridViewTextBoxColumn Note;
		private System.Windows.Forms.DataGridViewTextBoxColumn IO;
		private RadioButtonEx radioButtonEx_Force;
		private System.Windows.Forms.ProgressBar progressBar1;
	}
}