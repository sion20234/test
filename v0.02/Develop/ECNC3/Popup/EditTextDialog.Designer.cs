namespace ECNC3.Views.Popup
{
    partial class EditTextDialog
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
            this._titleLabel = new ECNC3.Views.LabelEx();
            this.CancelBt = new ECNC3.Views.ButtonEx();
            this._okBt = new ECNC3.Views.ButtonEx();
            this.textBoxEx1 = new ECNC3.Views.TextBoxEx();
            this.SuspendLayout();
            // 
            // _titleLabel
            // 
            this._titleLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._titleLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._titleLabel.Location = new System.Drawing.Point(14, 14);
            this._titleLabel.Name = "_titleLabel";
            this._titleLabel.Size = new System.Drawing.Size(995, 34);
            this._titleLabel.TabIndex = 2;
            this._titleLabel.Text = "No Title";
            this._titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CancelBt
            // 
            this.CancelBt.EditBox = null;
            this.CancelBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CancelBt.IsActive = false;
            this.CancelBt.LedSyncedBackColorEnable = true;
            this.CancelBt.Location = new System.Drawing.Point(892, 465);
            this.CancelBt.MultiSelectEn = false;
            this.CancelBt.Name = "CancelBt";
            this.CancelBt.OutLineEn = true;
            this.CancelBt.OutLineSize = 3F;
            this.CancelBt.ProgressBarEnable = false;
            this.CancelBt.ProgressBarMaxValue = 100;
            this.CancelBt.ProgressBarMinValue = 0;
            this.CancelBt.ProgressBarSize = 5;
            this.CancelBt.ProgressBarValue = 0;
            this.CancelBt.Selectable = true;
            this.CancelBt.Selected = false;
            this.CancelBt.Size = new System.Drawing.Size(120, 40);
            this.CancelBt.StatusLedEnable = false;
            this.CancelBt.StatusLedSize = ((byte)(15));
            this.CancelBt.TabIndex = 0;
            this.CancelBt.Text = "キャンセル";
            this.CancelBt.UseVisualStyleBackColor = false;
            this.CancelBt.Click += new System.EventHandler(this.CancelBt_Click);
            // 
            // _okBt
            // 
            this._okBt.EditBox = null;
            this._okBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._okBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._okBt.IsActive = false;
            this._okBt.LedSyncedBackColorEnable = true;
            this._okBt.Location = new System.Drawing.Point(738, 465);
            this._okBt.MultiSelectEn = false;
            this._okBt.Name = "_okBt";
            this._okBt.OutLineEn = true;
            this._okBt.OutLineSize = 3F;
            this._okBt.ProgressBarEnable = false;
            this._okBt.ProgressBarMaxValue = 100;
            this._okBt.ProgressBarMinValue = 0;
            this._okBt.ProgressBarSize = 5;
            this._okBt.ProgressBarValue = 0;
            this._okBt.Selectable = true;
            this._okBt.Selected = false;
            this._okBt.Size = new System.Drawing.Size(120, 40);
            this._okBt.StatusLedEnable = false;
            this._okBt.StatusLedSize = ((byte)(15));
            this._okBt.TabIndex = 3;
            this._okBt.Text = "OK";
            this._okBt.UseVisualStyleBackColor = false;
            this._okBt.Click += new System.EventHandler(this._okBt_Click);
            // 
            // textBoxEx1
            // 
            this.textBoxEx1.Font = new System.Drawing.Font("Meiryo UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxEx1.Location = new System.Drawing.Point(14, 59);
            this.textBoxEx1.Name = "textBoxEx1";
            this.textBoxEx1.Size = new System.Drawing.Size(995, 34);
            this.textBoxEx1.TabIndex = 4;
            this.textBoxEx1.Click += new System.EventHandler(this.textBoxEx1_Click);
            // 
            // EditTextDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 517);
            this.Controls.Add(this.textBoxEx1);
            this.Controls.Add(this._okBt);
            this.Controls.Add(this._titleLabel);
            this.Controls.Add(this.CancelBt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "EditTextDialog";
            this.OutLineSize = 3;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SelectCommandsDialog";
            this.Load += new System.EventHandler(this.EditTextDialog_Load);
            this.Shown += new System.EventHandler(this.EditTextDialog_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ButtonEx CancelBt;
        private LabelEx _titleLabel;
        private ButtonEx _okBt;
        private TextBoxEx textBoxEx1;
	}
}