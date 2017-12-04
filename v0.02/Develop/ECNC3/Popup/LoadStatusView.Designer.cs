namespace ECNC3.Views.Popup
{
    partial class LoadStatusView
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
            this._StatusTitleLabel = new ECNC3.Views.LabelEx();
            this._StatusTextLabel = new ECNC3.Views.LabelEx();
            this._ProgressBar = new ECNC3.Views.ButtonEx();
            this._BackGroundImage = new ECNC3.Views.PictureBoxEx();
            this._SubProgressBar = new ECNC3.Views.ButtonEx();
            this._SecondSubProgressBar = new ECNC3.Views.ButtonEx();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this._BackGroundImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // _StatusTitleLabel
            // 
            this._StatusTitleLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._StatusTitleLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._StatusTitleLabel.Font = new System.Drawing.Font("Meiryo UI", 60F);
            this._StatusTitleLabel.Location = new System.Drawing.Point(252, 193);
            this._StatusTitleLabel.Name = "_StatusTitleLabel";
            this._StatusTitleLabel.Size = new System.Drawing.Size(526, 139);
            this._StatusTitleLabel.TabIndex = 1;
            this._StatusTitleLabel.Text = "Empty";
            this._StatusTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _StatusTextLabel
            // 
            this._StatusTextLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._StatusTextLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._StatusTextLabel.Font = new System.Drawing.Font("Meiryo UI", 20F);
            this._StatusTextLabel.Location = new System.Drawing.Point(25, 447);
            this._StatusTextLabel.Name = "_StatusTextLabel";
            this._StatusTextLabel.Size = new System.Drawing.Size(972, 47);
            this._StatusTextLabel.TabIndex = 2;
            this._StatusTextLabel.Text = "Empty";
            // 
            // _ProgressBar
            // 
            this._ProgressBar.EditBox = null;
            this._ProgressBar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._ProgressBar.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._ProgressBar.IsActive = false;
            this._ProgressBar.LedSyncedBackColorEnable = true;
            this._ProgressBar.Location = new System.Drawing.Point(25, 665);
            this._ProgressBar.MultiSelectEn = false;
            this._ProgressBar.Name = "_ProgressBar";
            this._ProgressBar.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._ProgressBar.OutLineEn = true;
            this._ProgressBar.OutLineSize = 2F;
            this._ProgressBar.ProgressBarColor = System.Drawing.Color.Empty;
            this._ProgressBar.ProgressBarEnable = true;
            this._ProgressBar.ProgressBarMaxValue = 100;
            this._ProgressBar.ProgressBarMinValue = 0;
            this._ProgressBar.ProgressBarSize = 5;
            this._ProgressBar.ProgressBarValue = 0;
            this._ProgressBar.Selectable = true;
            this._ProgressBar.Selected = false;
            this._ProgressBar.Size = new System.Drawing.Size(972, 50);
            this._ProgressBar.StatusLedEnable = false;
            this._ProgressBar.StatusLedSize = ((byte)(15));
            this._ProgressBar.TabIndex = 3;
            this._ProgressBar.UseVisualStyleBackColor = false;
            // 
            // _BackGroundImage
            // 
            this._BackGroundImage.IconType = ECNC3.Views.PictureBoxEx.IconTypes.Clear;
            this._BackGroundImage.Location = new System.Drawing.Point(25, 26);
            this._BackGroundImage.Name = "_BackGroundImage";
            this._BackGroundImage.OutLineSize = 2F;
            this._BackGroundImage.ProgressBarColor = System.Drawing.Color.Empty;
            this._BackGroundImage.ProgressBarEnable = true;
            this._BackGroundImage.ProgressBarMaxValue = 100;
            this._BackGroundImage.ProgressBarMinValue = 0;
            this._BackGroundImage.ProgressBarSize = 5;
            this._BackGroundImage.ProgressBarValue = 0;
            this._BackGroundImage.Size = new System.Drawing.Size(972, 489);
            this._BackGroundImage.TabIndex = 4;
            this._BackGroundImage.TabStop = false;
            // 
            // _SubProgressBar
            // 
            this._SubProgressBar.EditBox = null;
            this._SubProgressBar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._SubProgressBar.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._SubProgressBar.IsActive = false;
            this._SubProgressBar.LedSyncedBackColorEnable = true;
            this._SubProgressBar.Location = new System.Drawing.Point(25, 715);
            this._SubProgressBar.MultiSelectEn = false;
            this._SubProgressBar.Name = "_SubProgressBar";
            this._SubProgressBar.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._SubProgressBar.OutLineEn = true;
            this._SubProgressBar.OutLineSize = 2F;
            this._SubProgressBar.ProgressBarColor = System.Drawing.Color.Empty;
            this._SubProgressBar.ProgressBarEnable = true;
            this._SubProgressBar.ProgressBarMaxValue = 100;
            this._SubProgressBar.ProgressBarMinValue = 0;
            this._SubProgressBar.ProgressBarSize = 5;
            this._SubProgressBar.ProgressBarValue = 0;
            this._SubProgressBar.Selectable = true;
            this._SubProgressBar.Selected = false;
            this._SubProgressBar.Size = new System.Drawing.Size(972, 15);
            this._SubProgressBar.StatusLedEnable = false;
            this._SubProgressBar.StatusLedSize = ((byte)(15));
            this._SubProgressBar.TabIndex = 5;
            this._SubProgressBar.UseVisualStyleBackColor = false;
            // 
            // _SecondSubProgressBar
            // 
            this._SecondSubProgressBar.EditBox = null;
            this._SecondSubProgressBar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._SecondSubProgressBar.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._SecondSubProgressBar.IsActive = false;
            this._SecondSubProgressBar.LedSyncedBackColorEnable = true;
            this._SecondSubProgressBar.Location = new System.Drawing.Point(25, 730);
            this._SecondSubProgressBar.MultiSelectEn = false;
            this._SecondSubProgressBar.Name = "_SecondSubProgressBar";
            this._SecondSubProgressBar.OutLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._SecondSubProgressBar.OutLineEn = true;
            this._SecondSubProgressBar.OutLineSize = 2F;
            this._SecondSubProgressBar.ProgressBarColor = System.Drawing.Color.Empty;
            this._SecondSubProgressBar.ProgressBarEnable = true;
            this._SecondSubProgressBar.ProgressBarMaxValue = 100;
            this._SecondSubProgressBar.ProgressBarMinValue = 0;
            this._SecondSubProgressBar.ProgressBarSize = 5;
            this._SecondSubProgressBar.ProgressBarValue = 0;
            this._SecondSubProgressBar.Selectable = true;
            this._SecondSubProgressBar.Selected = false;
            this._SecondSubProgressBar.Size = new System.Drawing.Size(972, 15);
            this._SecondSubProgressBar.StatusLedEnable = false;
            this._SecondSubProgressBar.StatusLedSize = ((byte)(15));
            this._SecondSubProgressBar.TabIndex = 6;
            this._SecondSubProgressBar.UseVisualStyleBackColor = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(233, 309);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(558, 146);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // LoadStatusView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this._SecondSubProgressBar);
            this.Controls.Add(this._SubProgressBar);
            this.Controls.Add(this._ProgressBar);
            this.Controls.Add(this._StatusTextLabel);
            this.Controls.Add(this._StatusTitleLabel);
            this.Controls.Add(this._BackGroundImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoadStatusView";
            this.OutLineSize = 3;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SelectCommandsDialog";
            ((System.ComponentModel.ISupportInitialize)(this._BackGroundImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private LabelEx _StatusTitleLabel;
        private LabelEx _StatusTextLabel;
        private ButtonEx _ProgressBar;
        private PictureBoxEx _BackGroundImage;
        private ButtonEx _SubProgressBar;
        private ButtonEx _SecondSubProgressBar;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}