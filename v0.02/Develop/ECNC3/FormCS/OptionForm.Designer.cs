namespace ECNC3.Views
{
    partial class OptionForm
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
            this.label14 = new ECNC3.Views.LabelEx();
            this.panel1 = new ECNC3.Views.PanelEx();
            this._startNoLabel = new ECNC3.Views.LabelEx();
            this._startNoEnBt = new ECNC3.Views.ButtonEx();
            this.ValiableListBt = new ECNC3.Views.ButtonEx();
            this.MachineLockBt = new ECNC3.Views.ButtonEx();
            this.CorrectAngleBt = new ECNC3.Views.ButtonEx();
            this.IncRefMoveBt = new ECNC3.Views.ButtonEx();
            this.M02Bt = new ECNC3.Views.ButtonEx();
            this.BlockSkipBt = new ECNC3.Views.ButtonEx();
            this.OptionalStopBt = new ECNC3.Views.ButtonEx();
            this.BuzzerBt = new ECNC3.Views.ButtonEx();
            this.PartitionRoundStopBt = new ECNC3.Views.ButtonEx();
            this.AecByLifeBt = new ECNC3.Views.ButtonEx();
            this._ClosePanel = new ECNC3.Views.PanelEx();
            this._CloseBtn = new ECNC3.Views.ButtonEx();
            this.panel1.SuspendLayout();
            this._ClosePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label8.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.Location = new System.Drawing.Point(7, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(410, 30);
            this.label8.TabIndex = 215;
            this.label8.Text = "OPTION";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            this.label14.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label14.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label14.Location = new System.Drawing.Point(6, 50);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(46, 41);
            this.label14.TabIndex = 245;
            this.label14.Text = "N";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this._startNoLabel);
            this.panel1.Controls.Add(this._startNoEnBt);
            this.panel1.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.panel1.Location = new System.Drawing.Point(238, 45);
            this.panel1.Name = "panel1";
            this.panel1.OutLineColor = System.Drawing.Color.Empty;
            this.panel1.OutLineSize = 0;
            this.panel1.Size = new System.Drawing.Size(176, 99);
            this.panel1.TabIndex = 246;
            // 
            // _startNoLabel
            // 
            this._startNoLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._startNoLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._startNoLabel.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._startNoLabel.Location = new System.Drawing.Point(54, 50);
            this._startNoLabel.Name = "_startNoLabel";
            this._startNoLabel.Size = new System.Drawing.Size(113, 41);
            this._startNoLabel.TabIndex = 244;
            this._startNoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._startNoLabel.Click += new System.EventHandler(this._startNoLabel_Click);
            // 
            // _startNoEnBt
            // 
            this._startNoEnBt.EditBox = null;
            this._startNoEnBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._startNoEnBt.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._startNoEnBt.IsActive = false;
            this._startNoEnBt.LedSyncedBackColorEnable = true;
            this._startNoEnBt.Location = new System.Drawing.Point(6, 6);
            this._startNoEnBt.MultiSelectEn = false;
            this._startNoEnBt.Name = "_startNoEnBt";
            this._startNoEnBt.OutLineColor = System.Drawing.Color.Empty;
            this._startNoEnBt.OutLineEn = true;
            this._startNoEnBt.OutLineSize = 3;
            this._startNoEnBt.Selectable = true;
            this._startNoEnBt.Selected = false;
            this._startNoEnBt.Size = new System.Drawing.Size(161, 41);
            this._startNoEnBt.StatusLedEnable = true;
            this._startNoEnBt.StatusLedSize = ((byte)(15));
            this._startNoEnBt.TabIndex = 223;
            this._startNoEnBt.Text = "スタート番号";
            this._startNoEnBt.UseVisualStyleBackColor = true;
            this._startNoEnBt.Click += new System.EventHandler(this._startNoEnBt_Click);
            // 
            // ValiableListBt
            // 
            this.ValiableListBt.EditBox = null;
            this.ValiableListBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ValiableListBt.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ValiableListBt.IsActive = false;
            this.ValiableListBt.LedSyncedBackColorEnable = true;
            this.ValiableListBt.Location = new System.Drawing.Point(15, 492);
            this.ValiableListBt.MultiSelectEn = false;
            this.ValiableListBt.Name = "ValiableListBt";
            this.ValiableListBt.OutLineColor = System.Drawing.Color.Empty;
            this.ValiableListBt.OutLineEn = true;
            this.ValiableListBt.OutLineSize = 3;
            this.ValiableListBt.Selectable = true;
            this.ValiableListBt.Selected = false;
            this.ValiableListBt.Size = new System.Drawing.Size(161, 41);
            this.ValiableListBt.StatusLedEnable = false;
            this.ValiableListBt.StatusLedSize = ((byte)(15));
            this.ValiableListBt.TabIndex = 230;
            this.ValiableListBt.Text = "指定マクロ変数表示";
            this.ValiableListBt.UseVisualStyleBackColor = true;
            this.ValiableListBt.Click += new System.EventHandler(this.ValiableListBt_Click);
            // 
            // MachineLockBt
            // 
            this.MachineLockBt.EditBox = null;
            this.MachineLockBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MachineLockBt.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.MachineLockBt.IsActive = false;
            this.MachineLockBt.LedSyncedBackColorEnable = true;
            this.MachineLockBt.Location = new System.Drawing.Point(15, 395);
            this.MachineLockBt.MultiSelectEn = false;
            this.MachineLockBt.Name = "MachineLockBt";
            this.MachineLockBt.OutLineColor = System.Drawing.Color.Empty;
            this.MachineLockBt.OutLineEn = true;
            this.MachineLockBt.OutLineSize = 3;
            this.MachineLockBt.Selectable = true;
            this.MachineLockBt.Selected = false;
            this.MachineLockBt.Size = new System.Drawing.Size(161, 41);
            this.MachineLockBt.StatusLedEnable = true;
            this.MachineLockBt.StatusLedSize = ((byte)(15));
            this.MachineLockBt.TabIndex = 229;
            this.MachineLockBt.Text = "マシンロック";
            this.MachineLockBt.UseVisualStyleBackColor = true;
            this.MachineLockBt.Click += new System.EventHandler(this.MachineLockBt_Click);
            // 
            // CorrectAngleBt
            // 
            this.CorrectAngleBt.EditBox = null;
            this.CorrectAngleBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CorrectAngleBt.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CorrectAngleBt.IsActive = false;
            this.CorrectAngleBt.LedSyncedBackColorEnable = true;
            this.CorrectAngleBt.Location = new System.Drawing.Point(15, 345);
            this.CorrectAngleBt.MultiSelectEn = false;
            this.CorrectAngleBt.Name = "CorrectAngleBt";
            this.CorrectAngleBt.OutLineColor = System.Drawing.Color.Empty;
            this.CorrectAngleBt.OutLineEn = true;
            this.CorrectAngleBt.OutLineSize = 3;
            this.CorrectAngleBt.Selectable = true;
            this.CorrectAngleBt.Selected = false;
            this.CorrectAngleBt.Size = new System.Drawing.Size(161, 41);
            this.CorrectAngleBt.StatusLedEnable = true;
            this.CorrectAngleBt.StatusLedSize = ((byte)(15));
            this.CorrectAngleBt.TabIndex = 228;
            this.CorrectAngleBt.Text = "角度補正オフセット";
            this.CorrectAngleBt.UseVisualStyleBackColor = true;
            this.CorrectAngleBt.Click += new System.EventHandler(this.CorrectAngleBt_Click);
            // 
            // IncRefMoveBt
            // 
            this.IncRefMoveBt.EditBox = null;
            this.IncRefMoveBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IncRefMoveBt.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.IncRefMoveBt.IsActive = false;
            this.IncRefMoveBt.LedSyncedBackColorEnable = true;
            this.IncRefMoveBt.Location = new System.Drawing.Point(15, 295);
            this.IncRefMoveBt.MultiSelectEn = false;
            this.IncRefMoveBt.Name = "IncRefMoveBt";
            this.IncRefMoveBt.OutLineColor = System.Drawing.Color.Empty;
            this.IncRefMoveBt.OutLineEn = true;
            this.IncRefMoveBt.OutLineSize = 3;
            this.IncRefMoveBt.Selectable = true;
            this.IncRefMoveBt.Selected = false;
            this.IncRefMoveBt.Size = new System.Drawing.Size(161, 41);
            this.IncRefMoveBt.StatusLedEnable = true;
            this.IncRefMoveBt.StatusLedSize = ((byte)(15));
            this.IncRefMoveBt.TabIndex = 227;
            this.IncRefMoveBt.Text = "相対測定点移動";
            this.IncRefMoveBt.UseVisualStyleBackColor = true;
            this.IncRefMoveBt.Click += new System.EventHandler(this.IncRefMoveBt_Click);
            // 
            // M02Bt
            // 
            this.M02Bt.EditBox = null;
            this.M02Bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.M02Bt.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.M02Bt.IsActive = false;
            this.M02Bt.LedSyncedBackColorEnable = true;
            this.M02Bt.Location = new System.Drawing.Point(15, 445);
            this.M02Bt.MultiSelectEn = false;
            this.M02Bt.Name = "M02Bt";
            this.M02Bt.OutLineColor = System.Drawing.Color.Empty;
            this.M02Bt.OutLineEn = true;
            this.M02Bt.OutLineSize = 3;
            this.M02Bt.Selectable = true;
            this.M02Bt.Selected = false;
            this.M02Bt.Size = new System.Drawing.Size(161, 41);
            this.M02Bt.StatusLedEnable = true;
            this.M02Bt.StatusLedSize = ((byte)(15));
            this.M02Bt.TabIndex = 226;
            this.M02Bt.Text = "自動電源遮断";
            this.M02Bt.UseVisualStyleBackColor = true;
            this.M02Bt.Click += new System.EventHandler(this.M02Bt_Click);
            // 
            // BlockSkipBt
            // 
            this.BlockSkipBt.EditBox = null;
            this.BlockSkipBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BlockSkipBt.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BlockSkipBt.IsActive = false;
            this.BlockSkipBt.LedSyncedBackColorEnable = true;
            this.BlockSkipBt.Location = new System.Drawing.Point(15, 245);
            this.BlockSkipBt.MultiSelectEn = false;
            this.BlockSkipBt.Name = "BlockSkipBt";
            this.BlockSkipBt.OutLineColor = System.Drawing.Color.Empty;
            this.BlockSkipBt.OutLineEn = true;
            this.BlockSkipBt.OutLineSize = 3;
            this.BlockSkipBt.Selectable = true;
            this.BlockSkipBt.Selected = false;
            this.BlockSkipBt.Size = new System.Drawing.Size(161, 41);
            this.BlockSkipBt.StatusLedEnable = true;
            this.BlockSkipBt.StatusLedSize = ((byte)(15));
            this.BlockSkipBt.TabIndex = 225;
            this.BlockSkipBt.Text = "ブロックスキップ";
            this.BlockSkipBt.UseVisualStyleBackColor = true;
            this.BlockSkipBt.Click += new System.EventHandler(this.BlockSkipBt_Click);
            // 
            // OptionalStopBt
            // 
            this.OptionalStopBt.EditBox = null;
            this.OptionalStopBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OptionalStopBt.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.OptionalStopBt.IsActive = false;
            this.OptionalStopBt.LedSyncedBackColorEnable = true;
            this.OptionalStopBt.Location = new System.Drawing.Point(15, 195);
            this.OptionalStopBt.MultiSelectEn = false;
            this.OptionalStopBt.Name = "OptionalStopBt";
            this.OptionalStopBt.OutLineColor = System.Drawing.Color.Empty;
            this.OptionalStopBt.OutLineEn = true;
            this.OptionalStopBt.OutLineSize = 3;
            this.OptionalStopBt.Selectable = true;
            this.OptionalStopBt.Selected = false;
            this.OptionalStopBt.Size = new System.Drawing.Size(161, 41);
            this.OptionalStopBt.StatusLedEnable = true;
            this.OptionalStopBt.StatusLedSize = ((byte)(15));
            this.OptionalStopBt.TabIndex = 224;
            this.OptionalStopBt.Text = "オプショナルストップ";
            this.OptionalStopBt.UseVisualStyleBackColor = true;
            this.OptionalStopBt.Click += new System.EventHandler(this.OptionalStopBt_Click);
            // 
            // BuzzerBt
            // 
            this.BuzzerBt.EditBox = null;
            this.BuzzerBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BuzzerBt.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BuzzerBt.IsActive = false;
            this.BuzzerBt.LedSyncedBackColorEnable = true;
            this.BuzzerBt.Location = new System.Drawing.Point(15, 145);
            this.BuzzerBt.MultiSelectEn = false;
            this.BuzzerBt.Name = "BuzzerBt";
            this.BuzzerBt.OutLineColor = System.Drawing.Color.Empty;
            this.BuzzerBt.OutLineEn = true;
            this.BuzzerBt.OutLineSize = 3;
            this.BuzzerBt.Selectable = true;
            this.BuzzerBt.Selected = false;
            this.BuzzerBt.Size = new System.Drawing.Size(161, 41);
            this.BuzzerBt.StatusLedEnable = true;
            this.BuzzerBt.StatusLedSize = ((byte)(15));
            this.BuzzerBt.TabIndex = 221;
            this.BuzzerBt.Text = "ブザー";
            this.BuzzerBt.UseVisualStyleBackColor = true;
            this.BuzzerBt.Click += new System.EventHandler(this.BuzzerBt_Click);
            // 
            // PartitionRoundStopBt
            // 
            this.PartitionRoundStopBt.EditBox = null;
            this.PartitionRoundStopBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PartitionRoundStopBt.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PartitionRoundStopBt.IsActive = false;
            this.PartitionRoundStopBt.LedSyncedBackColorEnable = true;
            this.PartitionRoundStopBt.Location = new System.Drawing.Point(15, 95);
            this.PartitionRoundStopBt.MultiSelectEn = false;
            this.PartitionRoundStopBt.Name = "PartitionRoundStopBt";
            this.PartitionRoundStopBt.OutLineColor = System.Drawing.Color.Empty;
            this.PartitionRoundStopBt.OutLineEn = true;
            this.PartitionRoundStopBt.OutLineSize = 3;
            this.PartitionRoundStopBt.Selectable = true;
            this.PartitionRoundStopBt.Selected = false;
            this.PartitionRoundStopBt.Size = new System.Drawing.Size(161, 41);
            this.PartitionRoundStopBt.StatusLedEnable = true;
            this.PartitionRoundStopBt.StatusLedSize = ((byte)(15));
            this.PartitionRoundStopBt.TabIndex = 220;
            this.PartitionRoundStopBt.Text = "AEC1周待機";
            this.PartitionRoundStopBt.UseVisualStyleBackColor = true;
            this.PartitionRoundStopBt.Click += new System.EventHandler(this.PartitionRoundStopBt_Click);
            // 
            // AecByLifeBt
            // 
            this.AecByLifeBt.EditBox = null;
            this.AecByLifeBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AecByLifeBt.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.AecByLifeBt.IsActive = false;
            this.AecByLifeBt.LedSyncedBackColorEnable = true;
            this.AecByLifeBt.Location = new System.Drawing.Point(15, 45);
            this.AecByLifeBt.MultiSelectEn = false;
            this.AecByLifeBt.Name = "AecByLifeBt";
            this.AecByLifeBt.OutLineColor = System.Drawing.Color.Empty;
            this.AecByLifeBt.OutLineEn = true;
            this.AecByLifeBt.OutLineSize = 3;
            this.AecByLifeBt.Selectable = true;
            this.AecByLifeBt.Selected = false;
            this.AecByLifeBt.Size = new System.Drawing.Size(161, 41);
            this.AecByLifeBt.StatusLedEnable = true;
            this.AecByLifeBt.StatusLedSize = ((byte)(15));
            this.AecByLifeBt.TabIndex = 219;
            this.AecByLifeBt.Text = "電極交換";
            this.AecByLifeBt.UseVisualStyleBackColor = true;
            this.AecByLifeBt.Click += new System.EventHandler(this.AecByLifeBt_Click);
            // 
            // _ClosePanel
            // 
            this._ClosePanel.Controls.Add(this._CloseBtn);
            this._ClosePanel.Location = new System.Drawing.Point(269, 484);
            this._ClosePanel.Name = "_ClosePanel";
            this._ClosePanel.OutLineColor = System.Drawing.Color.DarkOliveGreen;
            this._ClosePanel.OutLineSize = 2;
            this._ClosePanel.Size = new System.Drawing.Size(145, 62);
            this._ClosePanel.TabIndex = 377;
            // 
            // _CloseBtn
            // 
            this._CloseBtn.EditBox = null;
            this._CloseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._CloseBtn.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._CloseBtn.IsActive = false;
            this._CloseBtn.LedSyncedBackColorEnable = true;
            this._CloseBtn.Location = new System.Drawing.Point(6, 6);
            this._CloseBtn.MultiSelectEn = false;
            this._CloseBtn.Name = "_CloseBtn";
            this._CloseBtn.OutLineColor = System.Drawing.Color.Empty;
            this._CloseBtn.OutLineEn = true;
            this._CloseBtn.OutLineSize = 3;
            this._CloseBtn.Selectable = true;
            this._CloseBtn.Selected = false;
            this._CloseBtn.Size = new System.Drawing.Size(133, 50);
            this._CloseBtn.StatusLedEnable = false;
            this._CloseBtn.StatusLedSize = ((byte)(15));
            this._CloseBtn.TabIndex = 15;
            this._CloseBtn.Text = "閉じる";
            this._CloseBtn.UseVisualStyleBackColor = false;
            this._CloseBtn.Click += new System.EventHandler(this._CloseBtn_Click);
            // 
            // OptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 554);
            this.Controls.Add(this._ClosePanel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ValiableListBt);
            this.Controls.Add(this.MachineLockBt);
            this.Controls.Add(this.CorrectAngleBt);
            this.Controls.Add(this.IncRefMoveBt);
            this.Controls.Add(this.M02Bt);
            this.Controls.Add(this.BlockSkipBt);
            this.Controls.Add(this.OptionalStopBt);
            this.Controls.Add(this.BuzzerBt);
            this.Controls.Add(this.PartitionRoundStopBt);
            this.Controls.Add(this.AecByLifeBt);
            this.Controls.Add(this.label8);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(0, 90);
            this.Name = "OptionForm";
            this.OutLineSize = 3;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "OptionForm";
            this.Load += new System.EventHandler(this.OptionForm_Load);
            this.panel1.ResumeLayout(false);
            this._ClosePanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private LabelEx label8;
        private ButtonEx AecByLifeBt;
        private ButtonEx PartitionRoundStopBt;
        private ButtonEx BuzzerBt;
        private ButtonEx _startNoEnBt;
        private ButtonEx M02Bt;
        private ButtonEx BlockSkipBt;
        private ButtonEx OptionalStopBt;
        private ButtonEx MachineLockBt;
        private ButtonEx CorrectAngleBt;
        private ButtonEx IncRefMoveBt;
        private ButtonEx ValiableListBt;
        private LabelEx label14;
        private PanelEx panel1;
        private LabelEx _startNoLabel;
        private PanelEx _ClosePanel;
        private ButtonEx _CloseBtn;
    }
}