namespace ECNC3.Views
{
    partial class FuncPassForm
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
            this.panel1 = new ECNC3.Views.PanelEx();
            this._PasswordHideBt = new ECNC3.Views.ButtonEx();
            this._idLabel = new ECNC3.Views.LabelEx();
            this._IdTextBox = new ECNC3.Views.TextBoxEx();
            this._passwordLabel = new ECNC3.Views.LabelEx();
            this.DeleteTextBt = new ECNC3.Views.ButtonEx();
            this._PassTextBox = new ECNC3.Views.TextBoxEx();
            this.EnterPassBt = new ECNC3.Views.ButtonEx();
            this.CloseBt = new ECNC3.Views.ButtonEx();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._PasswordHideBt);
            this.panel1.Controls.Add(this._idLabel);
            this.panel1.Controls.Add(this._IdTextBox);
            this.panel1.Controls.Add(this._passwordLabel);
            this.panel1.Controls.Add(this.DeleteTextBt);
            this.panel1.Controls.Add(this._PassTextBox);
            this.panel1.Controls.Add(this.EnterPassBt);
            this.panel1.Controls.Add(this.CloseBt);
            this.panel1.Location = new System.Drawing.Point(6, 6);
            this.panel1.Name = "panel1";
            this.panel1.OutLineColor = System.Drawing.Color.Empty;
            this.panel1.OutLineSize = 0;
            this.panel1.Size = new System.Drawing.Size(1012, 666);
            this.panel1.TabIndex = 15;
            // 
            // _PasswordHideBt
            // 
            this._PasswordHideBt.EditBox = null;
            this._PasswordHideBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._PasswordHideBt.Font = new System.Drawing.Font("Meiryo UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._PasswordHideBt.IsActive = false;
            this._PasswordHideBt.Location = new System.Drawing.Point(16, 593);
            this._PasswordHideBt.MultiSelectEn = false;
            this._PasswordHideBt.Name = "_PasswordHideBt";
            this._PasswordHideBt.OutLineColor = System.Drawing.Color.Empty;
            this._PasswordHideBt.OutLineEn = true;
            this._PasswordHideBt.OutLineSize = 3;
            this._PasswordHideBt.Selectable = false;
            this._PasswordHideBt.Size = new System.Drawing.Size(166, 61);
            this._PasswordHideBt.StatusLedEnable = false;
            this._PasswordHideBt.StatusLedSize = ((byte)(15));
            this._PasswordHideBt.TabIndex = 17;
            this._PasswordHideBt.Text = "パスワードを隠す";
            this._PasswordHideBt.UseVisualStyleBackColor = true;
            this._PasswordHideBt.MouseUp += new System.Windows.Forms.MouseEventHandler(this._PasswordHideBt_MouseUp);
            // 
            // _idLabel
            // 
            this._idLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._idLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._idLabel.Font = new System.Drawing.Font("Meiryo UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._idLabel.Location = new System.Drawing.Point(16, 11);
            this._idLabel.Name = "_idLabel";
            this._idLabel.Size = new System.Drawing.Size(984, 32);
            this._idLabel.TabIndex = 16;
            this._idLabel.Text = "IDを入力して下さい。";
            this._idLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _IdTextBox
            // 
            this._IdTextBox.Font = new System.Drawing.Font("MS UI Gothic", 47.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._IdTextBox.Location = new System.Drawing.Point(16, 48);
            this._IdTextBox.Name = "_IdTextBox";
            this._IdTextBox.Size = new System.Drawing.Size(984, 70);
            this._IdTextBox.TabIndex = 15;
            this._IdTextBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this._IdTextBox_MouseUp);
            // 
            // _passwordLabel
            // 
            this._passwordLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._passwordLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._passwordLabel.Font = new System.Drawing.Font("Meiryo UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._passwordLabel.Location = new System.Drawing.Point(16, 139);
            this._passwordLabel.Name = "_passwordLabel";
            this._passwordLabel.Size = new System.Drawing.Size(984, 32);
            this._passwordLabel.TabIndex = 3;
            this._passwordLabel.Text = "パスワードを入力して下さい。";
            this._passwordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DeleteTextBt
            // 
            this.DeleteTextBt.EditBox = null;
            this.DeleteTextBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteTextBt.Font = new System.Drawing.Font("Meiryo UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DeleteTextBt.IsActive = false;
            this.DeleteTextBt.Location = new System.Drawing.Point(747, 593);
            this.DeleteTextBt.MultiSelectEn = false;
            this.DeleteTextBt.Name = "DeleteTextBt";
            this.DeleteTextBt.OutLineColor = System.Drawing.Color.Empty;
            this.DeleteTextBt.OutLineEn = true;
            this.DeleteTextBt.OutLineSize = 3;
            this.DeleteTextBt.Selectable = false;
            this.DeleteTextBt.Size = new System.Drawing.Size(124, 61);
            this.DeleteTextBt.StatusLedEnable = false;
            this.DeleteTextBt.StatusLedSize = ((byte)(15));
            this.DeleteTextBt.TabIndex = 14;
            this.DeleteTextBt.Text = "クリア";
            this.DeleteTextBt.UseVisualStyleBackColor = true;
            this.DeleteTextBt.Click += new System.EventHandler(this.DeleteTextBt_Click);
            // 
            // _PassTextBox
            // 
            this._PassTextBox.Font = new System.Drawing.Font("MS UI Gothic", 47.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._PassTextBox.Location = new System.Drawing.Point(16, 176);
            this._PassTextBox.Name = "_PassTextBox";
            this._PassTextBox.Size = new System.Drawing.Size(984, 70);
            this._PassTextBox.TabIndex = 0;
            this._PassTextBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PassTextBox_MouseUp);
            // 
            // EnterPassBt
            // 
            this.EnterPassBt.EditBox = null;
            this.EnterPassBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EnterPassBt.Font = new System.Drawing.Font("Meiryo UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.EnterPassBt.IsActive = false;
            this.EnterPassBt.Location = new System.Drawing.Point(619, 593);
            this.EnterPassBt.MultiSelectEn = false;
            this.EnterPassBt.Name = "EnterPassBt";
            this.EnterPassBt.OutLineColor = System.Drawing.Color.Empty;
            this.EnterPassBt.OutLineEn = true;
            this.EnterPassBt.OutLineSize = 3;
            this.EnterPassBt.Selectable = true;
            this.EnterPassBt.Size = new System.Drawing.Size(124, 61);
            this.EnterPassBt.StatusLedEnable = false;
            this.EnterPassBt.StatusLedSize = ((byte)(15));
            this.EnterPassBt.TabIndex = 1;
            this.EnterPassBt.Text = "確定";
            this.EnterPassBt.UseVisualStyleBackColor = true;
            this.EnterPassBt.Click += new System.EventHandler(this.EnterPassBt_Click);
            // 
            // CloseBt
            // 
            this.CloseBt.EditBox = null;
            this.CloseBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseBt.Font = new System.Drawing.Font("Meiryo UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CloseBt.IsActive = false;
            this.CloseBt.Location = new System.Drawing.Point(876, 593);
            this.CloseBt.MultiSelectEn = false;
            this.CloseBt.Name = "CloseBt";
            this.CloseBt.OutLineColor = System.Drawing.Color.Empty;
            this.CloseBt.OutLineEn = true;
            this.CloseBt.OutLineSize = 3;
            this.CloseBt.Selectable = true;
            this.CloseBt.Size = new System.Drawing.Size(124, 61);
            this.CloseBt.StatusLedEnable = false;
            this.CloseBt.StatusLedSize = ((byte)(15));
            this.CloseBt.TabIndex = 2;
            this.CloseBt.Text = "閉じる";
            this.CloseBt.UseVisualStyleBackColor = true;
            this.CloseBt.Click += new System.EventHandler(this.CloseBt_Click);
            // 
            // FuncPassForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 678);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(0, 90);
            this.Name = "FuncPassForm";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FuncPassForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FuncPassForm_FormClosing);
            this.Load += new System.EventHandler(this.FuncPassForm_Load);
            this.Shown += new System.EventHandler(this.FuncPassForm_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private ButtonEx EnterPassBt;
        private ButtonEx CloseBt;
        private LabelEx _passwordLabel;
        private ButtonEx DeleteTextBt;
		private PanelEx panel1;
        private TextBoxEx _PassTextBox;
        private LabelEx _idLabel;
        private TextBoxEx _IdTextBox;
        private ButtonEx _PasswordHideBt;
    }
}