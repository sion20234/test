using System.Data;
namespace ECNC3.Views
{
    partial class MAINForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MAINForm));
            this.panel6 = new ECNC3.Views.PanelEx();
            this.DateTimeView = new ECNC3.Views.LabelEx();
            this._btnAlarm = new ECNC3.Views.PictureBoxEx();
            this._btnMaintenance = new ECNC3.Views.PictureBoxEx();
            this._notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this._contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._menuOpenAppLog = new System.Windows.Forms.ToolStripMenuItem();
            this._menuClose = new System.Windows.Forms.ToolStripMenuItem();
            this._menuActivate = new System.Windows.Forms.ToolStripMenuItem();
            this.panel5 = new ECNC3.Views.PanelEx();
            this.AUTOFormBt = new ECNC3.Views.ButtonEx();
            this.EDITFormBt = new ECNC3.Views.ButtonEx();
            this.MDIFormBt = new ECNC3.Views.ButtonEx();
            this.MANUALFormBt = new ECNC3.Views.ButtonEx();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._btnAlarm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._btnMaintenance)).BeginInit();
            this._contextMenuStrip.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.DateTimeView);
            this.panel6.Controls.Add(this._btnAlarm);
            this.panel6.Controls.Add(this._btnMaintenance);
            this.panel6.Font = new System.Drawing.Font("Meiryo UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.panel6.Location = new System.Drawing.Point(822, -12);
            this.panel6.Name = "panel6";
            this.panel6.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panel6.OutLineSize = 0F;
            this.panel6.Size = new System.Drawing.Size(190, 98);
            this.panel6.TabIndex = 153;
            // 
            // DateTimeView
            // 
            this.DateTimeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DateTimeView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DateTimeView.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Bold);
            this.DateTimeView.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.DateTimeView.Location = new System.Drawing.Point(3, 70);
            this.DateTimeView.Name = "DateTimeView";
            this.DateTimeView.Size = new System.Drawing.Size(184, 25);
            this.DateTimeView.TabIndex = 162;
            this.DateTimeView.Text = "0H 0M 0S";
            this.DateTimeView.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // _btnAlarm
            // 
            this._btnAlarm.IconType = ECNC3.Views.PictureBoxEx.IconTypes.Clear;
            this._btnAlarm.Location = new System.Drawing.Point(137, 16);
            this._btnAlarm.Name = "_btnAlarm";
            this._btnAlarm.Size = new System.Drawing.Size(50, 50);
            this._btnAlarm.TabIndex = 150;
            this._btnAlarm.TabStop = false;
            this._btnAlarm.Click += new System.EventHandler(this._btnAlarm_Click);
            // 
            // _btnMaintenance
            // 
            this._btnMaintenance.IconType = ECNC3.Views.PictureBoxEx.IconTypes.Clear;
            this._btnMaintenance.Location = new System.Drawing.Point(81, 16);
            this._btnMaintenance.Name = "_btnMaintenance";
            this._btnMaintenance.Size = new System.Drawing.Size(50, 50);
            this._btnMaintenance.TabIndex = 150;
            this._btnMaintenance.TabStop = false;
            this._btnMaintenance.Click += new System.EventHandler(this._btnMaintenance_Click);
            // 
            // _notifyIcon
            // 
            this._notifyIcon.ContextMenuStrip = this._contextMenuStrip;
            this._notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("_notifyIcon.Icon")));
            this._notifyIcon.Text = "ECNC3 Application";
            this._notifyIcon.Visible = true;
            // 
            // _contextMenuStrip
            // 
            this._contextMenuStrip.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuOpenAppLog,
            this._menuClose,
            this._menuActivate});
            this._contextMenuStrip.Name = "_contextMenuStrip";
            this._contextMenuStrip.Size = new System.Drawing.Size(268, 124);
            // 
            // _menuOpenAppLog
            // 
            this._menuOpenAppLog.Name = "_menuOpenAppLog";
            this._menuOpenAppLog.Size = new System.Drawing.Size(267, 40);
            this._menuOpenAppLog.Text = "OPEN APP. LOG";
            this._menuOpenAppLog.Click += new System.EventHandler(this._menuOpenAppLog_Click);
            // 
            // _menuClose
            // 
            this._menuClose.Name = "_menuClose";
            this._menuClose.Size = new System.Drawing.Size(267, 40);
            this._menuClose.Text = "CLOSE";
            this._menuClose.Click += new System.EventHandler(this._menuClose_Click);
            // 
            // _menuActivate
            // 
            this._menuActivate.Name = "_menuActivate";
            this._menuActivate.Size = new System.Drawing.Size(267, 40);
            this._menuActivate.Text = "ACTIVATE";
            this._menuActivate.Click += new System.EventHandler(this._menuActivate_Click);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.AUTOFormBt);
            this.panel5.Controls.Add(this.EDITFormBt);
            this.panel5.Controls.Add(this.MDIFormBt);
            this.panel5.Controls.Add(this.MANUALFormBt);
            this.panel5.Font = new System.Drawing.Font("Meiryo UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.panel5.Location = new System.Drawing.Point(3, -16);
            this.panel5.Name = "panel5";
            this.panel5.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panel5.OutLineSize = 0F;
            this.panel5.Size = new System.Drawing.Size(423, 102);
            this.panel5.TabIndex = 154;
            // 
            // AUTOFormBt
            // 
            this.AUTOFormBt.EditBox = null;
            this.AUTOFormBt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
            this.AUTOFormBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AUTOFormBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.AUTOFormBt.IsActive = false;
            this.AUTOFormBt.LedSyncedBackColorEnable = true;
            this.AUTOFormBt.Location = new System.Drawing.Point(316, 31);
            this.AUTOFormBt.MultiSelectEn = false;
            this.AUTOFormBt.Name = "AUTOFormBt";
            this.AUTOFormBt.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.AUTOFormBt.OutLineEn = true;
            this.AUTOFormBt.OutLineSize = 3F;
            this.AUTOFormBt.ProgressBarColor = System.Drawing.Color.Empty;
            this.AUTOFormBt.ProgressBarEnable = false;
            this.AUTOFormBt.ProgressBarMaxValue = 100;
            this.AUTOFormBt.ProgressBarMinValue = 0;
            this.AUTOFormBt.ProgressBarSize = 5;
            this.AUTOFormBt.ProgressBarValue = 0;
            this.AUTOFormBt.Selectable = true;
            this.AUTOFormBt.Selected = false;
            this.AUTOFormBt.Size = new System.Drawing.Size(100, 65);
            this.AUTOFormBt.StatusLedEnable = false;
            this.AUTOFormBt.StatusLedSize = ((byte)(15));
            this.AUTOFormBt.TabIndex = 155;
            this.AUTOFormBt.Text = "自動";
            this.AUTOFormBt.UseVisualStyleBackColor = false;
            this.AUTOFormBt.Click += new System.EventHandler(this.AUTOFormBt_Click);
            // 
            // EDITFormBt
            // 
            this.EDITFormBt.EditBox = null;
            this.EDITFormBt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
            this.EDITFormBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EDITFormBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.EDITFormBt.IsActive = false;
            this.EDITFormBt.LedSyncedBackColorEnable = true;
            this.EDITFormBt.Location = new System.Drawing.Point(213, 31);
            this.EDITFormBt.MultiSelectEn = false;
            this.EDITFormBt.Name = "EDITFormBt";
            this.EDITFormBt.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.EDITFormBt.OutLineEn = true;
            this.EDITFormBt.OutLineSize = 3F;
            this.EDITFormBt.ProgressBarColor = System.Drawing.Color.Empty;
            this.EDITFormBt.ProgressBarEnable = false;
            this.EDITFormBt.ProgressBarMaxValue = 100;
            this.EDITFormBt.ProgressBarMinValue = 0;
            this.EDITFormBt.ProgressBarSize = 5;
            this.EDITFormBt.ProgressBarValue = 0;
            this.EDITFormBt.Selectable = true;
            this.EDITFormBt.Selected = false;
            this.EDITFormBt.Size = new System.Drawing.Size(100, 65);
            this.EDITFormBt.StatusLedEnable = false;
            this.EDITFormBt.StatusLedSize = ((byte)(15));
            this.EDITFormBt.TabIndex = 157;
            this.EDITFormBt.Text = "編集";
            this.EDITFormBt.UseVisualStyleBackColor = false;
            this.EDITFormBt.Click += new System.EventHandler(this.EDITFormBt_Click);
            // 
            // MDIFormBt
            // 
            this.MDIFormBt.EditBox = null;
            this.MDIFormBt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
            this.MDIFormBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MDIFormBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.MDIFormBt.IsActive = false;
            this.MDIFormBt.LedSyncedBackColorEnable = true;
            this.MDIFormBt.Location = new System.Drawing.Point(110, 31);
            this.MDIFormBt.MultiSelectEn = false;
            this.MDIFormBt.Name = "MDIFormBt";
            this.MDIFormBt.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.MDIFormBt.OutLineEn = true;
            this.MDIFormBt.OutLineSize = 3F;
            this.MDIFormBt.ProgressBarColor = System.Drawing.Color.Empty;
            this.MDIFormBt.ProgressBarEnable = false;
            this.MDIFormBt.ProgressBarMaxValue = 100;
            this.MDIFormBt.ProgressBarMinValue = 0;
            this.MDIFormBt.ProgressBarSize = 5;
            this.MDIFormBt.ProgressBarValue = 0;
            this.MDIFormBt.Selectable = true;
            this.MDIFormBt.Selected = false;
            this.MDIFormBt.Size = new System.Drawing.Size(100, 65);
            this.MDIFormBt.StatusLedEnable = false;
            this.MDIFormBt.StatusLedSize = ((byte)(15));
            this.MDIFormBt.TabIndex = 156;
            this.MDIFormBt.Text = "M.D.I.";
            this.MDIFormBt.UseVisualStyleBackColor = false;
            this.MDIFormBt.Click += new System.EventHandler(this.MDIFormBt_Click);
            // 
            // MANUALFormBt
            // 
            this.MANUALFormBt.EditBox = null;
            this.MANUALFormBt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
            this.MANUALFormBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MANUALFormBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.MANUALFormBt.IsActive = false;
            this.MANUALFormBt.LedSyncedBackColorEnable = true;
            this.MANUALFormBt.Location = new System.Drawing.Point(7, 31);
            this.MANUALFormBt.MultiSelectEn = false;
            this.MANUALFormBt.Name = "MANUALFormBt";
            this.MANUALFormBt.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.MANUALFormBt.OutLineEn = true;
            this.MANUALFormBt.OutLineSize = 3F;
            this.MANUALFormBt.ProgressBarColor = System.Drawing.Color.Empty;
            this.MANUALFormBt.ProgressBarEnable = false;
            this.MANUALFormBt.ProgressBarMaxValue = 100;
            this.MANUALFormBt.ProgressBarMinValue = 0;
            this.MANUALFormBt.ProgressBarSize = 5;
            this.MANUALFormBt.ProgressBarValue = 0;
            this.MANUALFormBt.Selectable = true;
            this.MANUALFormBt.Selected = false;
            this.MANUALFormBt.Size = new System.Drawing.Size(100, 65);
            this.MANUALFormBt.StatusLedEnable = false;
            this.MANUALFormBt.StatusLedSize = ((byte)(15));
            this.MANUALFormBt.TabIndex = 154;
            this.MANUALFormBt.Text = "手動";
            this.MANUALFormBt.UseVisualStyleBackColor = false;
            this.MANUALFormBt.Click += new System.EventHandler(this.MANUALFormBt_Click);
            // 
            // MAINForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MAINForm";
            this.OutLineSize = 3;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MAINForm_FormClosing);
            this.Load += new System.EventHandler(this.MAINForm_Load);
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._btnAlarm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._btnMaintenance)).EndInit();
            this._contextMenuStrip.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private PanelEx panel6;
        private System.Windows.Forms.NotifyIcon _notifyIcon;
        private System.Windows.Forms.ContextMenuStrip _contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem _menuClose;
        private System.Windows.Forms.ToolStripMenuItem _menuOpenAppLog;
        private PictureBoxEx _btnAlarm;
        private System.Windows.Forms.ToolStripMenuItem _menuActivate;
        private ButtonEx MANUALFormBt;
        private ButtonEx AUTOFormBt;
        private ButtonEx MDIFormBt;
        private ButtonEx EDITFormBt;
        private PanelEx panel5;
        private PictureBoxEx _btnMaintenance;
        private LabelEx DateTimeView;
    }
}