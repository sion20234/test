namespace ECNC3Updater
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this._updateProcState = new System.Windows.Forms.ProgressBar();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this._updateState = new System.Windows.Forms.ToolStripStatusLabel();
            this._beforeVerLabel = new ECNC3.Views.LabelEx();
            this._afterVerLabel = new ECNC3.Views.LabelEx();
            this.labelEx1 = new ECNC3.Views.LabelEx();
            this._closeBt = new ECNC3.Views.ButtonEx();
            this.LogButton = new ECNC3.Views.ButtonEx();
            this._excuteBt = new ECNC3.Views.ButtonEx();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // _updateProcState
            // 
            this._updateProcState.Location = new System.Drawing.Point(3, 214);
            this._updateProcState.Name = "_updateProcState";
            this._updateProcState.Size = new System.Drawing.Size(279, 23);
            this._updateProcState.TabIndex = 3;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._updateState});
            this.statusStrip1.Location = new System.Drawing.Point(0, 289);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(284, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // _updateState
            // 
            this._updateState.AutoSize = false;
            this._updateState.Name = "_updateState";
            this._updateState.Size = new System.Drawing.Size(258, 17);
            this._updateState.Text = "None";
            // 
            // _beforeVerLabel
            // 
            this._beforeVerLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._beforeVerLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._beforeVerLabel.Font = new System.Drawing.Font("Meiryo UI", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._beforeVerLabel.Location = new System.Drawing.Point(3, 10);
            this._beforeVerLabel.Name = "_beforeVerLabel";
            this._beforeVerLabel.Size = new System.Drawing.Size(279, 45);
            this._beforeVerLabel.TabIndex = 7;
            this._beforeVerLabel.Text = "labelEx3";
            this._beforeVerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _afterVerLabel
            // 
            this._afterVerLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._afterVerLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._afterVerLabel.Font = new System.Drawing.Font("Meiryo UI", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._afterVerLabel.Location = new System.Drawing.Point(3, 138);
            this._afterVerLabel.Name = "_afterVerLabel";
            this._afterVerLabel.Size = new System.Drawing.Size(279, 45);
            this._afterVerLabel.TabIndex = 6;
            this._afterVerLabel.Text = "labelEx2";
            this._afterVerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelEx1
            // 
            this.labelEx1.AutoSize = true;
            this.labelEx1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelEx1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelEx1.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEx1.Location = new System.Drawing.Point(3, 194);
            this.labelEx1.Name = "labelEx1";
            this.labelEx1.Size = new System.Drawing.Size(86, 19);
            this.labelEx1.TabIndex = 4;
            this.labelEx1.Text = "全体の進行：";
            // 
            // _closeBt
            // 
            this._closeBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._closeBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._closeBt.Location = new System.Drawing.Point(193, 243);
            this._closeBt.Name = "_closeBt";
            this._closeBt.Selectable = true;
            this._closeBt.Size = new System.Drawing.Size(89, 45);
            this._closeBt.TabIndex = 2;
            this._closeBt.Text = "閉じる";
            this._closeBt.UseVisualStyleBackColor = false;
            this._closeBt.Click += new System.EventHandler(this._closeBt_Click);
            // 
            // LogButton
            // 
            this.LogButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LogButton.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LogButton.Location = new System.Drawing.Point(98, 243);
            this.LogButton.Name = "LogButton";
            this.LogButton.Selectable = true;
            this.LogButton.Size = new System.Drawing.Size(89, 45);
            this.LogButton.TabIndex = 1;
            this.LogButton.Text = "ログ";
            this.LogButton.UseVisualStyleBackColor = false;
            this.LogButton.Click += new System.EventHandler(this.LogButton_Click);
            // 
            // _excuteBt
            // 
            this._excuteBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._excuteBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._excuteBt.Location = new System.Drawing.Point(3, 243);
            this._excuteBt.Name = "_excuteBt";
            this._excuteBt.Selectable = true;
            this._excuteBt.Size = new System.Drawing.Size(89, 45);
            this._excuteBt.TabIndex = 0;
            this._excuteBt.Text = "実行";
            this._excuteBt.UseVisualStyleBackColor = false;
            this._excuteBt.Click += new System.EventHandler(this._excuteBt_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ECNC3Updater.Properties.Resources.tnm;
            this.pictureBox1.Location = new System.Drawing.Point(112, 58);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(61, 77);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 311);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this._beforeVerLabel);
            this.Controls.Add(this._afterVerLabel);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelEx1);
            this.Controls.Add(this._updateProcState);
            this.Controls.Add(this._closeBt);
            this.Controls.Add(this.LogButton);
            this.Controls.Add(this._excuteBt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(300, 350);
            this.MinimumSize = new System.Drawing.Size(300, 350);
            this.Name = "MainForm";
            this.Text = "ECNC3Updater";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ECNC3.Views.ButtonEx _excuteBt;
        private ECNC3.Views.ButtonEx LogButton;
        private ECNC3.Views.ButtonEx _closeBt;
        private System.Windows.Forms.ProgressBar _updateProcState;
        private ECNC3.Views.LabelEx labelEx1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private ECNC3.Views.LabelEx _afterVerLabel;
        private ECNC3.Views.LabelEx _beforeVerLabel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel _updateState;
    }
}

