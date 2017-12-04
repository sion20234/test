namespace ECNC3.Views
{
    partial class LabelExAndButtonEx
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

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.labelEx1 = new ECNC3.Views.LabelEx();
            this.buttonEx1 = new ECNC3.Views.ButtonEx();
            this.SuspendLayout();
            // 
            // labelEx1
            // 
            this.labelEx1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelEx1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelEx1.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelEx1.Location = new System.Drawing.Point(0, 0);
            this.labelEx1.Name = "labelEx1";
            this.labelEx1.Size = new System.Drawing.Size(96, 27);
            this.labelEx1.TabIndex = 0;
            this.labelEx1.Text = "empty";
            this.labelEx1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonEx1
            // 
            this.buttonEx1.EditBox = null;
            this.buttonEx1.FlatAppearance.BorderSize = 0;
            this.buttonEx1.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonEx1.IsActive = false;
            this.buttonEx1.LedSyncedBackColorEnable = true;
            this.buttonEx1.Location = new System.Drawing.Point(0, 27);
            this.buttonEx1.MultiSelectEn = false;
            this.buttonEx1.Name = "buttonEx1";
            this.buttonEx1.OutLineColor = System.Drawing.Color.Empty;
            this.buttonEx1.OutLineEn = true;
            this.buttonEx1.OutLineSize = 3;
            this.buttonEx1.Selectable = true;
            this.buttonEx1.Selected = false;
            this.buttonEx1.Size = new System.Drawing.Size(96, 39);
            this.buttonEx1.StatusLedEnable = false;
            this.buttonEx1.StatusLedSize = ((byte)(15));
            this.buttonEx1.TabIndex = 1;
            this.buttonEx1.Text = "0";
            this.buttonEx1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonEx1.UseVisualStyleBackColor = false;
            // 
            // LabelExAndButtonEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
            this.Controls.Add(this.buttonEx1);
            this.Controls.Add(this.labelEx1);
            this.ForeColor = System.Drawing.Color.Gainsboro;
            this.Name = "LabelExAndButtonEx";
            this.Size = new System.Drawing.Size(96, 66);
            this.Load += new System.EventHandler(this.LabelExAndButtonEx_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private LabelEx labelEx1;
        private ButtonEx buttonEx1;
    }
}
