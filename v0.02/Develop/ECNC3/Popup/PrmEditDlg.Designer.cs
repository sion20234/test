namespace ECNC3.Views.Popup
{
    partial class PrmEditDlg
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
            this.buttonEx1 = new ECNC3.Views.ButtonEx();
            this.buttonEx2 = new ECNC3.Views.ButtonEx();
            this._editValueText = new ECNC3.Views.TextBoxEx();
            this._valueLimitLabel = new ECNC3.Views.LabelEx();
            this._titleLabel = new ECNC3.Views.LabelEx();
            this._beforeValueLabel = new ECNC3.Views.LabelEx();
            this.SuspendLayout();
            // 
            // buttonEx1
            // 
            this.buttonEx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
            this.buttonEx1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
            this.buttonEx1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEx1.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonEx1.ForeColor = System.Drawing.Color.Gainsboro;
            this.buttonEx1.Location = new System.Drawing.Point(24, 206);
            this.buttonEx1.Name = "buttonEx1";
            this.buttonEx1.Selectable = true;
            this.buttonEx1.Size = new System.Drawing.Size(123, 54);
            this.buttonEx1.TabIndex = 1;
            this.buttonEx1.Text = "キャンセル";
            this.buttonEx1.UseVisualStyleBackColor = false;
            this.buttonEx1.Click += new System.EventHandler(this.buttonEx1_Click);
            // 
            // buttonEx2
            // 
            this.buttonEx2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
            this.buttonEx2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
            this.buttonEx2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEx2.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonEx2.ForeColor = System.Drawing.Color.Gainsboro;
            this.buttonEx2.Location = new System.Drawing.Point(248, 206);
            this.buttonEx2.Name = "buttonEx2";
            this.buttonEx2.Selectable = true;
            this.buttonEx2.Size = new System.Drawing.Size(123, 54);
            this.buttonEx2.TabIndex = 2;
            this.buttonEx2.Text = "OK";
            this.buttonEx2.UseVisualStyleBackColor = false;
            this.buttonEx2.Click += new System.EventHandler(this.buttonEx2_Click);
            // 
            // _editValueText
            // 
            this._editValueText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
            this._editValueText.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._editValueText.ForeColor = System.Drawing.Color.Gainsboro;
            this._editValueText.Location = new System.Drawing.Point(24, 137);
            this._editValueText.Name = "_editValueText";
            this._editValueText.Size = new System.Drawing.Size(347, 32);
            this._editValueText.TabIndex = 0;
            // 
            // _valueLimitLabel
            // 
            this._valueLimitLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
            this._valueLimitLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._valueLimitLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._valueLimitLabel.Font = new System.Drawing.Font("Meiryo UI", 14.25F);
            this._valueLimitLabel.ForeColor = System.Drawing.Color.Gainsboro;
            this._valueLimitLabel.Location = new System.Drawing.Point(24, 67);
            this._valueLimitLabel.Name = "_valueLimitLabel";
            this._valueLimitLabel.Size = new System.Drawing.Size(347, 32);
            this._valueLimitLabel.TabIndex = 3;
            this._valueLimitLabel.Text = "no limit";
            this._valueLimitLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // _titleLabel
            // 
            this._titleLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
            this._titleLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._titleLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._titleLabel.Font = new System.Drawing.Font("Meiryo UI", 14.25F);
            this._titleLabel.ForeColor = System.Drawing.Color.Gainsboro;
            this._titleLabel.Location = new System.Drawing.Point(24, 32);
            this._titleLabel.Name = "_titleLabel";
            this._titleLabel.Size = new System.Drawing.Size(347, 32);
            this._titleLabel.TabIndex = 4;
            this._titleLabel.Text = "no title";
            this._titleLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // _beforeValueLabel
            // 
            this._beforeValueLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
            this._beforeValueLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._beforeValueLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._beforeValueLabel.Font = new System.Drawing.Font("Meiryo UI", 14.25F);
            this._beforeValueLabel.ForeColor = System.Drawing.Color.Gainsboro;
            this._beforeValueLabel.Location = new System.Drawing.Point(24, 102);
            this._beforeValueLabel.Name = "_beforeValueLabel";
            this._beforeValueLabel.Size = new System.Drawing.Size(347, 32);
            this._beforeValueLabel.TabIndex = 5;
            this._beforeValueLabel.Text = "no value";
            // 
            // PrmEditDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(398, 281);
            this.Controls.Add(this._beforeValueLabel);
            this.Controls.Add(this._titleLabel);
            this.Controls.Add(this._valueLimitLabel);
            this.Controls.Add(this._editValueText);
            this.Controls.Add(this.buttonEx2);
            this.Controls.Add(this.buttonEx1);
            this.ForeColor = System.Drawing.Color.Gainsboro;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PrmEditDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PrmEditDlg";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ButtonEx buttonEx1;
        private ButtonEx buttonEx2;
        private TextBoxEx _editValueText;
        private LabelEx _valueLimitLabel;
        private LabelEx _titleLabel;
        private LabelEx _beforeValueLabel;
    }
}