namespace ECNC3.Views.Popup
{
    partial class SelectCommandsDialogSimple
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
            this._command1Bt = new ECNC3.Views.ButtonEx();
            this._command2Bt = new ECNC3.Views.ButtonEx();
            this._command3Bt = new ECNC3.Views.ButtonEx();
            this.SuspendLayout();
            // 
            // _command1Bt
            // 
            this._command1Bt.EditBox = null;
            this._command1Bt.FlatAppearance.BorderSize = 0;
            this._command1Bt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._command1Bt.IsActive = false;
            this._command1Bt.Location = new System.Drawing.Point(6, 6);
            this._command1Bt.MultiSelectEn = false;
            this._command1Bt.Name = "_command1Bt";
            this._command1Bt.OutLineColor = System.Drawing.Color.Empty;
            this._command1Bt.OutLineEn = true;
            this._command1Bt.OutLineSize = 3;
            this._command1Bt.Selectable = false;
            this._command1Bt.Size = new System.Drawing.Size(133, 50);
            this._command1Bt.StatusLedEnable = false;
            this._command1Bt.StatusLedSize = ((byte)(15));
            this._command1Bt.TabIndex = 0;
            this._command1Bt.UseVisualStyleBackColor = false;
            this._command1Bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this._commandBt_MouseUp);
            // 
            // _command2Bt
            // 
            this._command2Bt.EditBox = null;
            this._command2Bt.FlatAppearance.BorderSize = 0;
            this._command2Bt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._command2Bt.IsActive = false;
            this._command2Bt.Location = new System.Drawing.Point(6, 62);
            this._command2Bt.MultiSelectEn = false;
            this._command2Bt.Name = "_command2Bt";
            this._command2Bt.OutLineColor = System.Drawing.Color.Empty;
            this._command2Bt.OutLineEn = true;
            this._command2Bt.OutLineSize = 3;
            this._command2Bt.Selectable = false;
            this._command2Bt.Size = new System.Drawing.Size(133, 50);
            this._command2Bt.StatusLedEnable = false;
            this._command2Bt.StatusLedSize = ((byte)(15));
            this._command2Bt.TabIndex = 1;
            this._command2Bt.UseVisualStyleBackColor = false;
            this._command2Bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this._commandBt_MouseUp);
            // 
            // _command3Bt
            // 
            this._command3Bt.EditBox = null;
            this._command3Bt.FlatAppearance.BorderSize = 0;
            this._command3Bt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._command3Bt.IsActive = false;
            this._command3Bt.Location = new System.Drawing.Point(6, 118);
            this._command3Bt.MultiSelectEn = false;
            this._command3Bt.Name = "_command3Bt";
            this._command3Bt.OutLineColor = System.Drawing.Color.Empty;
            this._command3Bt.OutLineEn = true;
            this._command3Bt.OutLineSize = 3;
            this._command3Bt.Selectable = false;
            this._command3Bt.Size = new System.Drawing.Size(133, 50);
            this._command3Bt.StatusLedEnable = false;
            this._command3Bt.StatusLedSize = ((byte)(15));
            this._command3Bt.TabIndex = 2;
            this._command3Bt.UseVisualStyleBackColor = false;
            this._command3Bt.MouseUp += new System.Windows.Forms.MouseEventHandler(this._commandBt_MouseUp);
            // 
            // SelectCommandsDialogSimple
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(145, 175);
            this.Controls.Add(this._command3Bt);
            this.Controls.Add(this._command2Bt);
            this.Controls.Add(this._command1Bt);
            this.ForeColor = System.Drawing.Color.Gainsboro;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SelectCommandsDialogSimple";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SelectCommandsDialog";
            this.Load += new System.EventHandler(this.SelectCommandsDialogSimple_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ButtonEx _command1Bt;
        private ButtonEx _command2Bt;
        private ButtonEx _command3Bt;
    }
}