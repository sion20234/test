namespace DebugMcIf
{
	partial class FormAlarm
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			this._dgList = new System.Windows.Forms.DataGridView();
			this.MemberName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.IndexNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.BitNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.State = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this._dgList)).BeginInit();
			this.SuspendLayout();
			// 
			// _dgList
			// 
			this._dgList.AllowUserToAddRows = false;
			this._dgList.AllowUserToDeleteRows = false;
			this._dgList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this._dgList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MemberName,
            this.IndexNumber,
            this.BitNumber,
            this.Title,
            this.State});
			this._dgList.Dock = System.Windows.Forms.DockStyle.Fill;
			this._dgList.Location = new System.Drawing.Point(0, 0);
			this._dgList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this._dgList.MultiSelect = false;
			this._dgList.Name = "_dgList";
			this._dgList.RowTemplate.Height = 21;
			this._dgList.Size = new System.Drawing.Size(728, 552);
			this._dgList.TabIndex = 0;
			this._dgList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this._dgList_CellClick);
			// 
			// MemberName
			// 
			this.MemberName.HeaderText = "STRUCT";
			this.MemberName.Name = "MemberName";
			this.MemberName.ReadOnly = true;
			this.MemberName.Width = 50;
			// 
			// IndexNumber
			// 
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			this.IndexNumber.DefaultCellStyle = dataGridViewCellStyle1;
			this.IndexNumber.HeaderText = "#";
			this.IndexNumber.Name = "IndexNumber";
			this.IndexNumber.ReadOnly = true;
			this.IndexNumber.Width = 40;
			// 
			// BitNumber
			// 
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			this.BitNumber.DefaultCellStyle = dataGridViewCellStyle2;
			this.BitNumber.HeaderText = "Bit";
			this.BitNumber.Name = "BitNumber";
			this.BitNumber.ReadOnly = true;
			this.BitNumber.Width = 40;
			// 
			// Title
			// 
			this.Title.HeaderText = "TITLE";
			this.Title.Name = "Title";
			this.Title.ReadOnly = true;
			this.Title.Width = 400;
			// 
			// State
			// 
			this.State.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.State.HeaderText = "STATE";
			this.State.Name = "State";
			this.State.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.State.Width = 25;
			// 
			// FormAlarm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(728, 552);
			this.Controls.Add(this._dgList);
			this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "FormAlarm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "FormAlarm";
			this.Load += new System.EventHandler(this.FormAlarm_Load);
			((System.ComponentModel.ISupportInitialize)(this._dgList)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView _dgList;
		private System.Windows.Forms.DataGridViewTextBoxColumn MemberName;
		private System.Windows.Forms.DataGridViewTextBoxColumn IndexNumber;
		private System.Windows.Forms.DataGridViewTextBoxColumn BitNumber;
		private System.Windows.Forms.DataGridViewTextBoxColumn Title;
		private System.Windows.Forms.DataGridViewCheckBoxColumn State;
	}
}