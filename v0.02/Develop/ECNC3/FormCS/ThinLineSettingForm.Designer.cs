namespace ECNC3.Views
{
    partial class ThinLineSettingForm
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
            this.label8 = new ECNC3.Views.LabelEx();
            this.label_ZUpIncrease = new ECNC3.Views.LabelEx();
            this.label_ZUpVelocity = new ECNC3.Views.LabelEx();
            this.label_RetryCount = new ECNC3.Views.LabelEx();
            this.label5 = new ECNC3.Views.LabelEx();
            this.panel1 = new ECNC3.Views.PanelEx();
            this.textBoxEx_RetryCount = new ECNC3.Views.TextBoxEx();
            this.textBoxEx_ZUpVelocity = new ECNC3.Views.TextBoxEx();
            this.textBoxEx_ZUpIncrease = new ECNC3.Views.TextBoxEx();
            this.panel8 = new ECNC3.Views.PanelEx();
            this.buttonEx_Ok = new ECNC3.Views.ButtonEx();
            this.panel2 = new ECNC3.Views.PanelEx();
            this.buttonEx_Back = new ECNC3.Views.ButtonEx();
            this.panel1.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label8.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.Location = new System.Drawing.Point(12, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(322, 30);
            this.label8.TabIndex = 86;
            this.label8.Text = "THIN LINE SETTING";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_ZUpIncrease
            // 
            this.label_ZUpIncrease.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_ZUpIncrease.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label_ZUpIncrease.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_ZUpIncrease.Location = new System.Drawing.Point(33, 27);
            this.label_ZUpIncrease.Name = "label_ZUpIncrease";
            this.label_ZUpIncrease.Size = new System.Drawing.Size(252, 22);
            this.label_ZUpIncrease.TabIndex = 87;
            this.label_ZUpIncrease.Text = "座屈センサーONによるZ軸上昇量";
            // 
            // label_ZUpVelocity
            // 
            this.label_ZUpVelocity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_ZUpVelocity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label_ZUpVelocity.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_ZUpVelocity.Location = new System.Drawing.Point(33, 97);
            this.label_ZUpVelocity.Name = "label_ZUpVelocity";
            this.label_ZUpVelocity.Size = new System.Drawing.Size(252, 22);
            this.label_ZUpVelocity.TabIndex = 88;
            this.label_ZUpVelocity.Text = "座屈センサーONによるZ軸上昇速度";
            // 
            // label_RetryCount
            // 
            this.label_RetryCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_RetryCount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label_RetryCount.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_RetryCount.Location = new System.Drawing.Point(33, 167);
            this.label_RetryCount.Name = "label_RetryCount";
            this.label_RetryCount.Size = new System.Drawing.Size(252, 22);
            this.label_RetryCount.TabIndex = 89;
            this.label_RetryCount.Text = "リトライ回数";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("HG丸ｺﾞｼｯｸM-PRO", 12F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(243, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 18);
            this.label5.TabIndex = 91;
            this.label5.Text = "mm";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.textBoxEx_RetryCount);
            this.panel1.Controls.Add(this.textBoxEx_ZUpVelocity);
            this.panel1.Controls.Add(this.textBoxEx_ZUpIncrease);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label_RetryCount);
            this.panel1.Controls.Add(this.label_ZUpVelocity);
            this.panel1.Controls.Add(this.label_ZUpIncrease);
            this.panel1.Location = new System.Drawing.Point(12, 41);
            this.panel1.Name = "panel1";
            this.panel1.OutLineColor = System.Drawing.Color.Empty;
            this.panel1.OutLineSize = 0;
            this.panel1.Size = new System.Drawing.Size(322, 260);
            this.panel1.TabIndex = 100;
            // 
            // textBoxEx_RetryCount
            // 
            this.textBoxEx_RetryCount.Font = new System.Drawing.Font("MS UI Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxEx_RetryCount.Location = new System.Drawing.Point(33, 192);
            this.textBoxEx_RetryCount.Name = "textBoxEx_RetryCount";
            this.textBoxEx_RetryCount.Size = new System.Drawing.Size(252, 39);
            this.textBoxEx_RetryCount.TabIndex = 97;
            this.textBoxEx_RetryCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxEx_RetryCount.Click += new System.EventHandler(this.textBoxEx_RetryCount_Click);
            // 
            // textBoxEx_ZUpVelocity
            // 
            this.textBoxEx_ZUpVelocity.Font = new System.Drawing.Font("MS UI Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxEx_ZUpVelocity.Location = new System.Drawing.Point(33, 122);
            this.textBoxEx_ZUpVelocity.Name = "textBoxEx_ZUpVelocity";
            this.textBoxEx_ZUpVelocity.Size = new System.Drawing.Size(252, 39);
            this.textBoxEx_ZUpVelocity.TabIndex = 96;
            this.textBoxEx_ZUpVelocity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxEx_ZUpVelocity.Click += new System.EventHandler(this.textBoxEx_ZUpVelocity_Click);
            // 
            // textBoxEx_ZUpIncrease
            // 
            this.textBoxEx_ZUpIncrease.Font = new System.Drawing.Font("MS UI Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxEx_ZUpIncrease.Location = new System.Drawing.Point(33, 52);
            this.textBoxEx_ZUpIncrease.Name = "textBoxEx_ZUpIncrease";
            this.textBoxEx_ZUpIncrease.Size = new System.Drawing.Size(204, 39);
            this.textBoxEx_ZUpIncrease.TabIndex = 95;
            this.textBoxEx_ZUpIncrease.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxEx_ZUpIncrease.Click += new System.EventHandler(this.textBoxEx_ZUpIncrease_Click);
            // 
            // panel8
            // 
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel8.Controls.Add(this.buttonEx_Ok);
            this.panel8.Location = new System.Drawing.Point(12, 306);
            this.panel8.Name = "panel8";
            this.panel8.OutLineColor = System.Drawing.Color.DarkOliveGreen;
            this.panel8.OutLineSize = 2;
            this.panel8.Size = new System.Drawing.Size(152, 57);
            this.panel8.TabIndex = 216;
            // 
            // buttonEx_Ok
            // 
            this.buttonEx_Ok.EditBox = null;
            this.buttonEx_Ok.FlatAppearance.BorderSize = 0;
            this.buttonEx_Ok.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonEx_Ok.IsActive = false;
            this.buttonEx_Ok.Location = new System.Drawing.Point(8, 6);
            this.buttonEx_Ok.MultiSelectEn = false;
            this.buttonEx_Ok.Name = "buttonEx_Ok";
            this.buttonEx_Ok.OutLineColor = System.Drawing.Color.Empty;
            this.buttonEx_Ok.OutLineEn = true;
            this.buttonEx_Ok.OutLineSize = 3;
            this.buttonEx_Ok.Selectable = true;
            this.buttonEx_Ok.Selected = false;
            this.buttonEx_Ok.Size = new System.Drawing.Size(132, 40);
            this.buttonEx_Ok.StatusLedEnable = false;
            this.buttonEx_Ok.StatusLedSize = ((byte)(15));
            this.buttonEx_Ok.TabIndex = 0;
            this.buttonEx_Ok.Text = "OK";
            this.buttonEx_Ok.UseVisualStyleBackColor = false;
            this.buttonEx_Ok.Click += new System.EventHandler(this.buttonEx_Ok_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.buttonEx_Back);
            this.panel2.Location = new System.Drawing.Point(182, 306);
            this.panel2.Name = "panel2";
            this.panel2.OutLineColor = System.Drawing.Color.DarkOliveGreen;
            this.panel2.OutLineSize = 2;
            this.panel2.Size = new System.Drawing.Size(152, 57);
            this.panel2.TabIndex = 215;
            // 
            // buttonEx_Back
            // 
            this.buttonEx_Back.EditBox = null;
            this.buttonEx_Back.FlatAppearance.BorderSize = 0;
            this.buttonEx_Back.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonEx_Back.IsActive = false;
            this.buttonEx_Back.Location = new System.Drawing.Point(8, 6);
            this.buttonEx_Back.MultiSelectEn = false;
            this.buttonEx_Back.Name = "buttonEx_Back";
            this.buttonEx_Back.OutLineColor = System.Drawing.Color.Empty;
            this.buttonEx_Back.OutLineEn = true;
            this.buttonEx_Back.OutLineSize = 3;
            this.buttonEx_Back.Selectable = true;
            this.buttonEx_Back.Selected = false;
            this.buttonEx_Back.Size = new System.Drawing.Size(132, 40);
            this.buttonEx_Back.StatusLedEnable = false;
            this.buttonEx_Back.StatusLedSize = ((byte)(15));
            this.buttonEx_Back.TabIndex = 1;
            this.buttonEx_Back.Text = "閉じる";
            this.buttonEx_Back.UseVisualStyleBackColor = false;
            this.buttonEx_Back.Click += new System.EventHandler(this.buttonEx_Back_Click);
            // 
            // ThinLineSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 372);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label8);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ThinLineSettingForm";
            this.OutLineSize = 3;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ThinLineSettingForm";
            this.Load += new System.EventHandler(this.ThinLineSettingForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private LabelEx label8;
        private LabelEx label_ZUpIncrease;
        private LabelEx label_ZUpVelocity;
        private LabelEx label_RetryCount;
        private LabelEx label5;
        private PanelEx panel1;
        private PanelEx panel8;
        private PanelEx panel2;
		private ButtonEx buttonEx_Ok;
		private ButtonEx buttonEx_Back;
		private TextBoxEx textBoxEx_RetryCount;
		private TextBoxEx textBoxEx_ZUpVelocity;
		private TextBoxEx textBoxEx_ZUpIncrease;
	}
}