namespace ECNC3.Views
{
    partial class MessageDialog
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
			this._edtText = new System.Windows.Forms.RichTextBox();
			this.label2 = new ECNC3.Views.LabelEx();
			this._stcTitle = new ECNC3.Views.LabelEx();
			this._btnYes = new ECNC3.Views.ButtonEx();
			this._btnNon = new ECNC3.Views.ButtonEx();
			this._stcNumber = new ECNC3.Views.LabelEx();
			this._btnOK = new ECNC3.Views.ButtonEx();
			this._picIcon = new ECNC3.Views.PictureBoxEx();
			this.panelEx1 = new ECNC3.Views.PanelEx();
			((System.ComponentModel.ISupportInitialize)(this._picIcon)).BeginInit();
			this.panelEx1.SuspendLayout();
			this.SuspendLayout();
			// 
			// _edtText
			// 
			this._edtText.Font = new System.Drawing.Font("Meiryo UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this._edtText.Location = new System.Drawing.Point(14, 91);
			this._edtText.Name = "_edtText";
			this._edtText.ReadOnly = true;
			this._edtText.Size = new System.Drawing.Size(614, 257);
			this._edtText.TabIndex = 11;
			this._edtText.Text = "";
			this._edtText.TextChanged += new System.EventHandler(this._edtText_TextChanged);
			// 
			// label2
			// 
			this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label2.Font = new System.Drawing.Font("Meiryo UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label2.Location = new System.Drawing.Point(360, 6);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(108, 35);
			this.label2.TabIndex = 10;
			this.label2.Text = "Code:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.label2.Click += new System.EventHandler(this.label2_Click);
			// 
			// _stcTitle
			// 
			this._stcTitle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this._stcTitle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this._stcTitle.Font = new System.Drawing.Font("Meiryo UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this._stcTitle.Location = new System.Drawing.Point(65, 45);
			this._stcTitle.Name = "_stcTitle";
			this._stcTitle.Size = new System.Drawing.Size(563, 41);
			this._stcTitle.TabIndex = 12;
			this._stcTitle.Click += new System.EventHandler(this._stcTitle_Click);
			// 
			// _btnYes
			// 
			this._btnYes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this._btnYes.Font = new System.Drawing.Font("Meiryo UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this._btnYes.Location = new System.Drawing.Point(342, 354);
			this._btnYes.Name = "_btnYes";
			this._btnYes.Selectable = true;
			this._btnYes.Size = new System.Drawing.Size(140, 70);
			this._btnYes.TabIndex = 13;
			this._btnYes.Text = "はい";
			this._btnYes.UseVisualStyleBackColor = true;
			this._btnYes.Click += new System.EventHandler(this._btnYes_Click);
			// 
			// _btnNon
			// 
			this._btnNon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this._btnNon.Font = new System.Drawing.Font("Meiryo UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this._btnNon.Location = new System.Drawing.Point(488, 354);
			this._btnNon.Name = "_btnNon";
			this._btnNon.Selectable = true;
			this._btnNon.Size = new System.Drawing.Size(140, 70);
			this._btnNon.TabIndex = 14;
			this._btnNon.Text = "いいえ";
			this._btnNon.UseVisualStyleBackColor = true;
			this._btnNon.Click += new System.EventHandler(this._btnNon_Click);
			// 
			// _stcNumber
			// 
			this._stcNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this._stcNumber.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this._stcNumber.Font = new System.Drawing.Font("Meiryo UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this._stcNumber.Location = new System.Drawing.Point(474, 6);
			this._stcNumber.Name = "_stcNumber";
			this._stcNumber.Size = new System.Drawing.Size(143, 37);
			this._stcNumber.TabIndex = 15;
			this._stcNumber.Click += new System.EventHandler(this._stcNumber_Click);
			// 
			// _btnOK
			// 
			this._btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this._btnOK.Font = new System.Drawing.Font("Meiryo UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this._btnOK.Location = new System.Drawing.Point(196, 354);
			this._btnOK.Name = "_btnOK";
			this._btnOK.Selectable = true;
			this._btnOK.Size = new System.Drawing.Size(140, 70);
			this._btnOK.TabIndex = 16;
			this._btnOK.Text = "OK";
			this._btnOK.UseVisualStyleBackColor = true;
			this._btnOK.Click += new System.EventHandler(this._btnOK_Click);
			// 
			// _picIcon
			// 
			this._picIcon.IconType = ECNC3.Views.PictureBoxEx.IconTypes.Clear;
			this._picIcon.Location = new System.Drawing.Point(6, 6);
			this._picIcon.Name = "_picIcon";
			this._picIcon.Size = new System.Drawing.Size(48, 48);
			this._picIcon.TabIndex = 17;
			this._picIcon.TabStop = false;
			this._picIcon.Click += new System.EventHandler(this._picIcon_Click);
			// 
			// panelEx1
			// 
			this.panelEx1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelEx1.Controls.Add(this._stcNumber);
			this.panelEx1.Controls.Add(this._edtText);
			this.panelEx1.Controls.Add(this._btnOK);
			this.panelEx1.Controls.Add(this._stcTitle);
			this.panelEx1.Controls.Add(this._btnNon);
			this.panelEx1.Controls.Add(this.label2);
			this.panelEx1.Controls.Add(this._btnYes);
			this.panelEx1.Controls.Add(this._picIcon);
			this.panelEx1.Location = new System.Drawing.Point(-1, -1);
			this.panelEx1.Name = "panelEx1";
			this.panelEx1.OutLineSize = 3;
			this.panelEx1.Size = new System.Drawing.Size(643, 437);
			this.panelEx1.TabIndex = 18;
			this.panelEx1.Paint += new System.Windows.Forms.PaintEventHandler(this.panelEx1_Paint);
			// 
			// MessageDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(641, 435);
			this.Controls.Add(this.panelEx1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "MessageDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "InfomationSelectForm";
			this.Load += new System.EventHandler(this.InfomationSelectForm_Load);
			((System.ComponentModel.ISupportInitialize)(this._picIcon)).EndInit();
			this.panelEx1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox _edtText;
        private LabelEx label2;
        private LabelEx _stcTitle;
        private ButtonEx _btnYes;
        private ButtonEx _btnNon;
		private LabelEx _stcNumber;
		private ButtonEx _btnOK;
		private PictureBoxEx _picIcon;
        private PanelEx panelEx1;
    }
}