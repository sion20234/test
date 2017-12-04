namespace ECNC3.Views
{
    partial class ProtectCommandsDialog
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
            this._enableValueLabel = new ECNC3.Views.LabelEx();
            this._selectCommandsPanel = new ECNC3.Views.PanelEx();
            this._multiSelectSubLabel = new ECNC3.Views.LabelEx();
            this._multiSelectStartLabel = new ECNC3.Views.LabelEx();
            this._multiSelectEndLabel = new ECNC3.Views.LabelEx();
            this._multiSelectEndEdit = new ECNC3.Views.TextBoxEx();
            this._multiSelectStartEdit = new ECNC3.Views.TextBoxEx();
            this._singleSelectEdit = new ECNC3.Views.TextBoxEx();
            this._multiSelectBtn = new ECNC3.Views.ButtonEx();
            this._lockBtn = new ECNC3.Views.ButtonEx();
            this._titleLabel = new ECNC3.Views.LabelEx();
            this.CancelBt = new ECNC3.Views.ButtonEx();
            this._unlockBtn = new ECNC3.Views.ButtonEx();
            this._selectCommandsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _enableValueLabel
            // 
            this._enableValueLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._enableValueLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._enableValueLabel.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold);
            this._enableValueLabel.Location = new System.Drawing.Point(8, 54);
            this._enableValueLabel.Name = "_enableValueLabel";
            this._enableValueLabel.Size = new System.Drawing.Size(403, 34);
            this._enableValueLabel.TabIndex = 7;
            this._enableValueLabel.Text = "Empty";
            this._enableValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _selectCommandsPanel
            // 
            this._selectCommandsPanel.Controls.Add(this._multiSelectSubLabel);
            this._selectCommandsPanel.Controls.Add(this._multiSelectStartLabel);
            this._selectCommandsPanel.Controls.Add(this._multiSelectEndLabel);
            this._selectCommandsPanel.Controls.Add(this._multiSelectEndEdit);
            this._selectCommandsPanel.Controls.Add(this._multiSelectStartEdit);
            this._selectCommandsPanel.Controls.Add(this._singleSelectEdit);
            this._selectCommandsPanel.Controls.Add(this._multiSelectBtn);
            this._selectCommandsPanel.Location = new System.Drawing.Point(8, 91);
            this._selectCommandsPanel.Name = "_selectCommandsPanel";
            this._selectCommandsPanel.OutLineSize = 0F;
            this._selectCommandsPanel.Size = new System.Drawing.Size(403, 110);
            this._selectCommandsPanel.TabIndex = 6;
            // 
            // _multiSelectSubLabel
            // 
            this._multiSelectSubLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._multiSelectSubLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._multiSelectSubLabel.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold);
            this._multiSelectSubLabel.Location = new System.Drawing.Point(141, 68);
            this._multiSelectSubLabel.Name = "_multiSelectSubLabel";
            this._multiSelectSubLabel.Size = new System.Drawing.Size(120, 32);
            this._multiSelectSubLabel.TabIndex = 12;
            this._multiSelectSubLabel.Text = "------";
            this._multiSelectSubLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _multiSelectStartLabel
            // 
            this._multiSelectStartLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._multiSelectStartLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._multiSelectStartLabel.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold);
            this._multiSelectStartLabel.Location = new System.Drawing.Point(12, 31);
            this._multiSelectStartLabel.Name = "_multiSelectStartLabel";
            this._multiSelectStartLabel.Size = new System.Drawing.Size(105, 34);
            this._multiSelectStartLabel.TabIndex = 9;
            this._multiSelectStartLabel.Text = "開始";
            this._multiSelectStartLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _multiSelectEndLabel
            // 
            this._multiSelectEndLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._multiSelectEndLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._multiSelectEndLabel.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold);
            this._multiSelectEndLabel.Location = new System.Drawing.Point(286, 31);
            this._multiSelectEndLabel.Name = "_multiSelectEndLabel";
            this._multiSelectEndLabel.Size = new System.Drawing.Size(105, 34);
            this._multiSelectEndLabel.TabIndex = 10;
            this._multiSelectEndLabel.Text = "終了";
            this._multiSelectEndLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _multiSelectEndEdit
            // 
            this._multiSelectEndEdit.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold);
            this._multiSelectEndEdit.Location = new System.Drawing.Point(286, 68);
            this._multiSelectEndEdit.Name = "_multiSelectEndEdit";
            this._multiSelectEndEdit.Size = new System.Drawing.Size(105, 32);
            this._multiSelectEndEdit.TabIndex = 11;
            this._multiSelectEndEdit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this._multiSelectEndEdit.MouseUp += new System.Windows.Forms.MouseEventHandler(this._multiSelectEndEdit_MouseUp);
            // 
            // _multiSelectStartEdit
            // 
            this._multiSelectStartEdit.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold);
            this._multiSelectStartEdit.Location = new System.Drawing.Point(12, 68);
            this._multiSelectStartEdit.Name = "_multiSelectStartEdit";
            this._multiSelectStartEdit.Size = new System.Drawing.Size(105, 32);
            this._multiSelectStartEdit.TabIndex = 10;
            this._multiSelectStartEdit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this._multiSelectStartEdit.MouseUp += new System.Windows.Forms.MouseEventHandler(this._multiSelectEndEdit_MouseUp);
            // 
            // _singleSelectEdit
            // 
            this._singleSelectEdit.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold);
            this._singleSelectEdit.Location = new System.Drawing.Point(12, 68);
            this._singleSelectEdit.Name = "_singleSelectEdit";
            this._singleSelectEdit.Size = new System.Drawing.Size(379, 32);
            this._singleSelectEdit.TabIndex = 9;
            this._singleSelectEdit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this._singleSelectEdit.MouseUp += new System.Windows.Forms.MouseEventHandler(this._multiSelectEndEdit_MouseUp);
            // 
            // _multiSelectBtn
            // 
            this._multiSelectBtn.EditBox = null;
            this._multiSelectBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._multiSelectBtn.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._multiSelectBtn.IsActive = false;
            this._multiSelectBtn.LedSyncedBackColorEnable = true;
            this._multiSelectBtn.Location = new System.Drawing.Point(141, 10);
            this._multiSelectBtn.MultiSelectEn = false;
            this._multiSelectBtn.Name = "_multiSelectBtn";
            this._multiSelectBtn.OutLineEn = true;
            this._multiSelectBtn.OutLineSize = 3F;
            this._multiSelectBtn.ProgressBarEnable = false;
            this._multiSelectBtn.ProgressBarMaxValue = 100;
            this._multiSelectBtn.ProgressBarMinValue = 0;
            this._multiSelectBtn.ProgressBarSize = 5;
            this._multiSelectBtn.ProgressBarValue = 0;
            this._multiSelectBtn.Selectable = true;
            this._multiSelectBtn.Selected = false;
            this._multiSelectBtn.Size = new System.Drawing.Size(120, 40);
            this._multiSelectBtn.StatusLedEnable = false;
            this._multiSelectBtn.StatusLedSize = ((byte)(15));
            this._multiSelectBtn.TabIndex = 7;
            this._multiSelectBtn.Text = "範囲選択";
            this._multiSelectBtn.UseVisualStyleBackColor = false;
            this._multiSelectBtn.MouseUp += new System.Windows.Forms.MouseEventHandler(this._multiSelectBtn_MouseUp);
            // 
            // _lockBtn
            // 
            this._lockBtn.EditBox = null;
            this._lockBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._lockBtn.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._lockBtn.IsActive = false;
            this._lockBtn.LedSyncedBackColorEnable = true;
            this._lockBtn.Location = new System.Drawing.Point(8, 207);
            this._lockBtn.MultiSelectEn = false;
            this._lockBtn.Name = "_lockBtn";
            this._lockBtn.OutLineEn = true;
            this._lockBtn.OutLineSize = 3F;
            this._lockBtn.ProgressBarEnable = false;
            this._lockBtn.ProgressBarMaxValue = 100;
            this._lockBtn.ProgressBarMinValue = 0;
            this._lockBtn.ProgressBarSize = 5;
            this._lockBtn.ProgressBarValue = 0;
            this._lockBtn.Selectable = true;
            this._lockBtn.Selected = false;
            this._lockBtn.Size = new System.Drawing.Size(120, 40);
            this._lockBtn.StatusLedEnable = false;
            this._lockBtn.StatusLedSize = ((byte)(15));
            this._lockBtn.TabIndex = 3;
            this._lockBtn.Text = "ロック";
            this._lockBtn.UseVisualStyleBackColor = false;
            this._lockBtn.MouseUp += new System.Windows.Forms.MouseEventHandler(this._lockBtn_MouseUp);
            // 
            // _titleLabel
            // 
            this._titleLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._titleLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._titleLabel.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold);
            this._titleLabel.Location = new System.Drawing.Point(8, 9);
            this._titleLabel.Name = "_titleLabel";
            this._titleLabel.Size = new System.Drawing.Size(403, 34);
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
            this.CancelBt.Location = new System.Drawing.Point(291, 207);
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
            this.CancelBt.Text = "閉じる";
            this.CancelBt.UseVisualStyleBackColor = false;
            this.CancelBt.Click += new System.EventHandler(this.CancelBt_Click);
            // 
            // _unlockBtn
            // 
            this._unlockBtn.EditBox = null;
            this._unlockBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._unlockBtn.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._unlockBtn.IsActive = false;
            this._unlockBtn.LedSyncedBackColorEnable = true;
            this._unlockBtn.Location = new System.Drawing.Point(149, 207);
            this._unlockBtn.MultiSelectEn = false;
            this._unlockBtn.Name = "_unlockBtn";
            this._unlockBtn.OutLineEn = true;
            this._unlockBtn.OutLineSize = 3F;
            this._unlockBtn.ProgressBarEnable = false;
            this._unlockBtn.ProgressBarMaxValue = 100;
            this._unlockBtn.ProgressBarMinValue = 0;
            this._unlockBtn.ProgressBarSize = 5;
            this._unlockBtn.ProgressBarValue = 0;
            this._unlockBtn.Selectable = true;
            this._unlockBtn.Selected = false;
            this._unlockBtn.Size = new System.Drawing.Size(120, 40);
            this._unlockBtn.StatusLedEnable = false;
            this._unlockBtn.StatusLedSize = ((byte)(15));
            this._unlockBtn.TabIndex = 8;
            this._unlockBtn.Text = "解除";
            this._unlockBtn.UseVisualStyleBackColor = false;
            this._unlockBtn.MouseUp += new System.Windows.Forms.MouseEventHandler(this._unlockBtn_MouseUp);
            // 
            // ProtectCommandsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 255);
            this.Controls.Add(this._unlockBtn);
            this.Controls.Add(this._enableValueLabel);
            this.Controls.Add(this._selectCommandsPanel);
            this.Controls.Add(this._lockBtn);
            this.Controls.Add(this._titleLabel);
            this.Controls.Add(this.CancelBt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ProtectCommandsDialog";
            this.OutLineSize = 3;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ProtectCommandsDialog";
            this.Load += new System.EventHandler(this.ProtectCommandsDialog_Load);
            this._selectCommandsPanel.ResumeLayout(false);
            this._selectCommandsPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ButtonEx CancelBt;
        private LabelEx _titleLabel;
        private ButtonEx _lockBtn;
        private PanelEx _selectCommandsPanel;
        private ButtonEx _multiSelectBtn;
        private LabelEx _enableValueLabel;
        private TextBoxEx _multiSelectEndEdit;
        private TextBoxEx _multiSelectStartEdit;
        private ButtonEx _unlockBtn;
        private LabelEx _multiSelectEndLabel;
        private LabelEx _multiSelectStartLabel;
        private LabelEx _multiSelectSubLabel;
        private TextBoxEx _singleSelectEdit;
    }
}