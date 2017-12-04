namespace ECNC3.Views
{
	partial class KeyboardDialog
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if( disposing && ( components != null ) ) {
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this._keyBoard = new ECNC3.Views.KeyBord();
			this.SuspendLayout();
			// 
			// _keyBoard
			// 
			this._keyBoard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
			this._keyBoard.ForeColor = System.Drawing.Color.White;
			this._keyBoard.InputValue = "";
			this._keyBoard.Location = new System.Drawing.Point(2, 2);
			this._keyBoard.Name = "_keyBoard";
			this._keyBoard.Size = new System.Drawing.Size(1016, 280);
			this._keyBoard.TabIndex = 0;
			this._keyBoard.Load += new System.EventHandler(this._keyBoard_Load);
			// 
			// KeyboardDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
			this.ClientSize = new System.Drawing.Size(1022, 284);
			this.Controls.Add(this._keyBoard);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "KeyboardDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "KeyboardDialog";
			this.ResumeLayout(false);

		}

		#endregion

		private KeyBord _keyBoard;
	}
}