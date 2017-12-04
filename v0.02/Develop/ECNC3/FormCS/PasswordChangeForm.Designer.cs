namespace ECNC3.Views
{
    partial class PasswordChangeForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new ECNC3.Views.LabelEx();
            this.panel8 = new ECNC3.Views.PanelEx();
            this._OkBtn = new ECNC3.Views.ButtonEx();
            this.panel2 = new ECNC3.Views.PanelEx();
            this.BackUserServiceFormBt = new ECNC3.Views.ButtonEx();
            this._AccountList = new ECNC3.Views.DataGridViewEx();
            this.IndexColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._AccountLevelColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this._AccountIDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._AccountPasswordColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._RowRemoveBtn = new ECNC3.Views.ButtonEx();
            this._KeybordBtn = new ECNC3.Views.ButtonEx();
            this.panel8.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._AccountList)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(6, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1012, 30);
            this.label1.TabIndex = 86;
            this.label1.Text = "PASSWORD CHANGE";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this._OkBtn);
            this.panel8.Location = new System.Drawing.Point(709, 604);
            this.panel8.Name = "panel8";
            this.panel8.OutLineColor = System.Drawing.Color.DarkOliveGreen;
            this.panel8.OutLineSize = 2;
            this.panel8.Size = new System.Drawing.Size(145, 62);
            this.panel8.TabIndex = 212;
            // 
            // _OkBtn
            // 
            this._OkBtn.EditBox = null;
            this._OkBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._OkBtn.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._OkBtn.IsActive = false;
            this._OkBtn.Location = new System.Drawing.Point(6, 6);
            this._OkBtn.MultiSelectEn = false;
            this._OkBtn.Name = "_OkBtn";
            this._OkBtn.OutLineColor = System.Drawing.Color.Empty;
            this._OkBtn.OutLineEn = true;
            this._OkBtn.OutLineSize = 3;
            this._OkBtn.Selectable = true;
            this._OkBtn.Selected = false;
            this._OkBtn.Size = new System.Drawing.Size(133, 50);
            this._OkBtn.StatusLedEnable = false;
            this._OkBtn.StatusLedSize = ((byte)(15));
            this._OkBtn.TabIndex = 2;
            this._OkBtn.Text = "OK";
            this._OkBtn.UseVisualStyleBackColor = false;
            this._OkBtn.Click += new System.EventHandler(this._OkBtn_Click);
            this._OkBtn.MouseUp += new System.Windows.Forms.MouseEventHandler(this._OkBtn_MouseUp);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.BackUserServiceFormBt);
            this.panel2.Location = new System.Drawing.Point(867, 604);
            this.panel2.Name = "panel2";
            this.panel2.OutLineColor = System.Drawing.Color.DarkOliveGreen;
            this.panel2.OutLineSize = 2;
            this.panel2.Size = new System.Drawing.Size(145, 62);
            this.panel2.TabIndex = 211;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // BackUserServiceFormBt
            // 
            this.BackUserServiceFormBt.EditBox = null;
            this.BackUserServiceFormBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BackUserServiceFormBt.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BackUserServiceFormBt.IsActive = false;
            this.BackUserServiceFormBt.Location = new System.Drawing.Point(6, 6);
            this.BackUserServiceFormBt.MultiSelectEn = false;
            this.BackUserServiceFormBt.Name = "BackUserServiceFormBt";
            this.BackUserServiceFormBt.OutLineColor = System.Drawing.Color.Empty;
            this.BackUserServiceFormBt.OutLineEn = true;
            this.BackUserServiceFormBt.OutLineSize = 3;
            this.BackUserServiceFormBt.Selectable = true;
            this.BackUserServiceFormBt.Selected = false;
            this.BackUserServiceFormBt.Size = new System.Drawing.Size(133, 50);
            this.BackUserServiceFormBt.StatusLedEnable = false;
            this.BackUserServiceFormBt.StatusLedSize = ((byte)(15));
            this.BackUserServiceFormBt.TabIndex = 15;
            this.BackUserServiceFormBt.Text = "閉じる";
            this.BackUserServiceFormBt.UseVisualStyleBackColor = false;
            this.BackUserServiceFormBt.Click += new System.EventHandler(this.BackUserServiceFormBt_Click);
            // 
            // _AccountList
            // 
            this._AccountList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._AccountList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IndexColumn,
            this._AccountLevelColumn,
            this._AccountIDColumn,
            this._AccountPasswordColumn}); 
            this._AccountList.EnableLastIndex = false;
            this._AccountList.Location = new System.Drawing.Point(12, 42);
            this._AccountList.MultiSelect = false;
            this._AccountList.Name = "_AccountList";
            this._AccountList.RowHeadersVisible = false;
            this._AccountList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this._AccountList.RowTemplate.Height = 40;
            this._AccountList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this._AccountList.Size = new System.Drawing.Size(1000, 556);
            this._AccountList.TabIndex = 213;
            this._AccountList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this._AccountList_CellClick);
            // 
            // IndexColumn
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.IndexColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.IndexColumn.HeaderText = "#";
            this.IndexColumn.Name = "IndexColumn";
            this.IndexColumn.Width = 65;
            // 
            // _AccountLevelColumn
            // 
            this._AccountLevelColumn.HeaderText = "権限";
            this._AccountLevelColumn.Name = "_AccountLevelColumn";
            this._AccountLevelColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this._AccountLevelColumn.Width = 200;
            // 
            // _AccountIDColumn
            // 
            this._AccountIDColumn.HeaderText = "アカウントID";
            this._AccountIDColumn.Name = "_AccountIDColumn";
            this._AccountIDColumn.Width = 350;
            // 
            // _AccountPasswordColumn
            // 
            this._AccountPasswordColumn.HeaderText = "パスワード";
            this._AccountPasswordColumn.Name = "_AccountPasswordColumn";
            this._AccountPasswordColumn.Width = 350;
            // 
            // _RowRemoveBtn
            // 
            this._RowRemoveBtn.EditBox = null;
            this._RowRemoveBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._RowRemoveBtn.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._RowRemoveBtn.IsActive = false;
            this._RowRemoveBtn.Location = new System.Drawing.Point(152, 612);
            this._RowRemoveBtn.MultiSelectEn = false;
            this._RowRemoveBtn.Name = "_RowRemoveBtn";
            this._RowRemoveBtn.OutLineColor = System.Drawing.Color.Empty;
            this._RowRemoveBtn.OutLineEn = true;
            this._RowRemoveBtn.OutLineSize = 3;
            this._RowRemoveBtn.Selectable = true;
            this._RowRemoveBtn.Selected = false;
            this._RowRemoveBtn.Size = new System.Drawing.Size(133, 50);
            this._RowRemoveBtn.StatusLedEnable = false;
            this._RowRemoveBtn.StatusLedSize = ((byte)(15));
            this._RowRemoveBtn.TabIndex = 215;
            this._RowRemoveBtn.Text = "削除";
            this._RowRemoveBtn.UseVisualStyleBackColor = false;
            this._RowRemoveBtn.Click += new System.EventHandler(this._RowRemoveBtn_Click);
            this._RowRemoveBtn.MouseUp += new System.Windows.Forms.MouseEventHandler(this._RowRemoveBtn_MouseUp);
            // 
            // _KeybordBtn
            // 
            this._KeybordBtn.EditBox = null;
            this._KeybordBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._KeybordBtn.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._KeybordBtn.IsActive = false;
            this._KeybordBtn.Location = new System.Drawing.Point(14, 612);
            this._KeybordBtn.MultiSelectEn = false;
            this._KeybordBtn.Name = "_KeybordBtn";
            this._KeybordBtn.OutLineColor = System.Drawing.Color.Empty;
            this._KeybordBtn.OutLineEn = true;
            this._KeybordBtn.OutLineSize = 3;
            this._KeybordBtn.Selectable = true;
            this._KeybordBtn.Selected = false;
            this._KeybordBtn.Size = new System.Drawing.Size(133, 50);
            this._KeybordBtn.StatusLedEnable = false;
            this._KeybordBtn.StatusLedSize = ((byte)(15));
            this._KeybordBtn.TabIndex = 216;
            this._KeybordBtn.Text = "キーボード";
            this._KeybordBtn.UseVisualStyleBackColor = false;
            this._KeybordBtn.Click += new System.EventHandler(this._KeybordBtn_Click);
            // 
            // PasswordChangeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 678);
            this.Controls.Add(this._KeybordBtn);
            this.Controls.Add(this._RowRemoveBtn);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this._AccountList);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(0, 90);
            this.Name = "PasswordChangeForm";
            this.OutLineSize = 3;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "PasswordChangeForm";
            this.Load += new System.EventHandler(this.PasswordChangeForm_Load);
            this.Shown += new System.EventHandler(this.PasswordChangeForm_Shown);
            this.panel8.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._AccountList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private LabelEx label1;
        private PanelEx panel8;
        private ButtonEx _OkBtn;
        private PanelEx panel2;
        private ButtonEx BackUserServiceFormBt;
        private DataGridViewEx _AccountList;
        private ButtonEx _RowRemoveBtn;
        private ButtonEx _KeybordBtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn IndexColumn;
        private System.Windows.Forms.DataGridViewButtonColumn _AccountLevelColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn _AccountIDColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn _AccountPasswordColumn;
    }
}