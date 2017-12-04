namespace ECNC3.Views
{
	partial class AlarmDialog
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
            this.label8 = new ECNC3.Views.LabelEx();
            this.panel1 = new ECNC3.Views.PanelEx();
            this.ResetBt = new ECNC3.Views.ButtonEx();
            this._btnRetrun = new ECNC3.Views.ButtonEx();
            this._dgList = new ECNC3.Views.DataGridViewEx();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IndexNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Details = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dgList)).BeginInit();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.DarkRed;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label8.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(634, 30);
            this.label8.TabIndex = 111;
            this.label8.Text = "ALARM";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label8.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonEx_Title_MouseDown);
            this.label8.MouseMove += new System.Windows.Forms.MouseEventHandler(this.buttonEx_Title_MouseMove);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ResetBt);
            this.panel1.Controls.Add(this._btnRetrun);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this._dgList);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.OutLineSize = 0F;
            this.panel1.Size = new System.Drawing.Size(634, 474);
            this.panel1.TabIndex = 114;
            // 
            // ResetBt
            // 
            this.ResetBt.EditBox = null;
            this.ResetBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ResetBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ResetBt.IsActive = false;
            this.ResetBt.LedSyncedBackColorEnable = true;
            this.ResetBt.Location = new System.Drawing.Point(3, 411);
            this.ResetBt.MultiSelectEn = false;
            this.ResetBt.Name = "ResetBt";
            this.ResetBt.OutLineEn = true;
            this.ResetBt.OutLineSize = 3F;
            this.ResetBt.ProgressBarColor = System.Drawing.Color.Empty;
            this.ResetBt.ProgressBarEnable = false;
            this.ResetBt.ProgressBarMaxValue = 100;
            this.ResetBt.ProgressBarMinValue = 0;
            this.ResetBt.ProgressBarSize = 5;
            this.ResetBt.ProgressBarValue = 0;
            this.ResetBt.Selectable = true;
            this.ResetBt.Selected = false;
            this.ResetBt.Size = new System.Drawing.Size(120, 60);
            this.ResetBt.StatusLedEnable = false;
            this.ResetBt.StatusLedSize = ((byte)(15));
            this.ResetBt.TabIndex = 114;
            this.ResetBt.Text = "リセット";
            this.ResetBt.UseVisualStyleBackColor = false;
            this.ResetBt.Click += new System.EventHandler(this.ResetBt_Click);
            // 
            // _btnRetrun
            // 
            this._btnRetrun.EditBox = null;
            this._btnRetrun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnRetrun.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._btnRetrun.IsActive = false;
            this._btnRetrun.LedSyncedBackColorEnable = true;
            this._btnRetrun.Location = new System.Drawing.Point(511, 411);
            this._btnRetrun.MultiSelectEn = false;
            this._btnRetrun.Name = "_btnRetrun";
            this._btnRetrun.OutLineEn = true;
            this._btnRetrun.OutLineSize = 3F;
            this._btnRetrun.ProgressBarColor = System.Drawing.Color.Empty;
            this._btnRetrun.ProgressBarEnable = false;
            this._btnRetrun.ProgressBarMaxValue = 100;
            this._btnRetrun.ProgressBarMinValue = 0;
            this._btnRetrun.ProgressBarSize = 5;
            this._btnRetrun.ProgressBarValue = 0;
            this._btnRetrun.Selectable = true;
            this._btnRetrun.Selected = false;
            this._btnRetrun.Size = new System.Drawing.Size(120, 60);
            this._btnRetrun.StatusLedEnable = false;
            this._btnRetrun.StatusLedSize = ((byte)(15));
            this._btnRetrun.TabIndex = 112;
            this._btnRetrun.Text = "閉じる";
            this._btnRetrun.UseVisualStyleBackColor = false;
            this._btnRetrun.Click += new System.EventHandler(this._btnRetrun_Click);
            // 
            // _dgList
            // 
            this._dgList.AllowUserToAddRows = false;
            this._dgList.AllowUserToDeleteRows = false;
            this._dgList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dgList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Code,
            this.IndexNumber,
            this.Title,
            this.Details});
            this._dgList.EnableLastIndex = true;
            this._dgList.Location = new System.Drawing.Point(3, 33);
            this._dgList.Name = "_dgList";
            this._dgList.ReadOnly = true;
            this._dgList.RowTemplate.Height = 21;
            this._dgList.Size = new System.Drawing.Size(628, 372);
            this._dgList.TabIndex = 113;
            this._dgList.SelectionChanged += new System.EventHandler(this._dgList_SelectionChanged);
            this._dgList.MouseUp += new System.Windows.Forms.MouseEventHandler(this._dgList_MouseUp);
            // 
            // Code
            // 
            this.Code.HeaderText = "Code";
            this.Code.Name = "Code";
            this.Code.ReadOnly = true;
            this.Code.Width = 75;
            // 
            // IndexNumber
            // 
            this.IndexNumber.HeaderText = "#";
            this.IndexNumber.Name = "IndexNumber";
            this.IndexNumber.ReadOnly = true;
            this.IndexNumber.Width = 50;
            // 
            // Title
            // 
            this.Title.HeaderText = "タイトル";
            this.Title.Name = "Title";
            this.Title.ReadOnly = true;
            this.Title.Visible = false;
            this.Title.Width = 300;
            // 
            // Details
            // 
            this.Details.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Details.HeaderText = "本文";
            this.Details.Name = "Details";
            this.Details.ReadOnly = true;
            // 
            // AlarmDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Red;
            this.ClientSize = new System.Drawing.Size(640, 480);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(20, 100);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "AlarmDialog";
            this.OutLineSize = 3;
            this.Padding = new System.Windows.Forms.Padding(3);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Load += new System.EventHandler(this.AlarmDialog_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AlarmDialog_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.AlarmDialog_MouseMove);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._dgList)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private LabelEx label8;
		private ButtonEx _btnRetrun;
		private DataGridViewEx _dgList;
		private PanelEx panel1;
        private ButtonEx ResetBt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn IndexNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn Details;
    }
}