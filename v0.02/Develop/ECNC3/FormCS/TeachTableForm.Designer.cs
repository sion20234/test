namespace ECNC3.Views
{
    partial class TeachTableForm
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
            this._CloseBtn = new ECNC3.Views.ButtonEx();
            this._PasteBtn = new ECNC3.Views.ButtonEx();
            this._CopyBtn = new ECNC3.Views.ButtonEx();
            this._DeleteBtn = new ECNC3.Views.ButtonEx();
            this._SelectBtn = new ECNC3.Views.ButtonEx();
            this.label17 = new ECNC3.Views.LabelEx();
            this._parameterList = new ECNC3.Views.DataGridViewEx();
            this._SelectedValueList = new ECNC3.Views.DataGridViewEx();
            this._EditBtn = new ECNC3.Views.ButtonEx();
            this.panel2 = new ECNC3.Views.PanelEx();
            ((System.ComponentModel.ISupportInitialize)(this._parameterList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._SelectedValueList)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _CloseBtn
            // 
            this._CloseBtn.EditBox = null;
            this._CloseBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this._CloseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._CloseBtn.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._CloseBtn.IsActive = false;
            this._CloseBtn.Location = new System.Drawing.Point(7, 7);
            this._CloseBtn.MultiSelectEn = false;
            this._CloseBtn.Name = "_CloseBtn";
            this._CloseBtn.OutLineColor = System.Drawing.Color.Empty;
            this._CloseBtn.OutLineEn = true;
            this._CloseBtn.OutLineSize = 3;
            this._CloseBtn.Selectable = true;
            this._CloseBtn.Selected = false;
            this._CloseBtn.Size = new System.Drawing.Size(142, 50);
            this._CloseBtn.StatusLedEnable = false;
            this._CloseBtn.StatusLedSize = ((byte)(10));
            this._CloseBtn.TabIndex = 189;
            this._CloseBtn.TabStop = false;
            this._CloseBtn.Text = "閉じる";
            this._CloseBtn.UseVisualStyleBackColor = false;
            this._CloseBtn.Click += new System.EventHandler(this._CloseBtn_Click);
            // 
            // _PasteBtn
            // 
            this._PasteBtn.EditBox = null;
            this._PasteBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this._PasteBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._PasteBtn.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._PasteBtn.IsActive = false;
            this._PasteBtn.Location = new System.Drawing.Point(457, 596);
            this._PasteBtn.MultiSelectEn = false;
            this._PasteBtn.Name = "_PasteBtn";
            this._PasteBtn.OutLineColor = System.Drawing.Color.Empty;
            this._PasteBtn.OutLineEn = true;
            this._PasteBtn.OutLineSize = 3;
            this._PasteBtn.Selectable = true;
            this._PasteBtn.Selected = false;
            this._PasteBtn.Size = new System.Drawing.Size(142, 50);
            this._PasteBtn.StatusLedEnable = false;
            this._PasteBtn.StatusLedSize = ((byte)(10));
            this._PasteBtn.TabIndex = 188;
            this._PasteBtn.TabStop = false;
            this._PasteBtn.Text = "貼り付け";
            this._PasteBtn.UseVisualStyleBackColor = false;
            this._PasteBtn.Click += new System.EventHandler(this._PasteBtn_Click);
            // 
            // _CopyBtn
            // 
            this._CopyBtn.EditBox = null;
            this._CopyBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this._CopyBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._CopyBtn.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._CopyBtn.IsActive = false;
            this._CopyBtn.Location = new System.Drawing.Point(309, 596);
            this._CopyBtn.MultiSelectEn = false;
            this._CopyBtn.Name = "_CopyBtn";
            this._CopyBtn.OutLineColor = System.Drawing.Color.Empty;
            this._CopyBtn.OutLineEn = true;
            this._CopyBtn.OutLineSize = 3;
            this._CopyBtn.Selectable = true;
            this._CopyBtn.Selected = false;
            this._CopyBtn.Size = new System.Drawing.Size(142, 50);
            this._CopyBtn.StatusLedEnable = false;
            this._CopyBtn.StatusLedSize = ((byte)(10));
            this._CopyBtn.TabIndex = 187;
            this._CopyBtn.TabStop = false;
            this._CopyBtn.Text = "コピー";
            this._CopyBtn.UseVisualStyleBackColor = false;
            this._CopyBtn.Click += new System.EventHandler(this._CopyBtn_Click);
            // 
            // _DeleteBtn
            // 
            this._DeleteBtn.EditBox = null;
            this._DeleteBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this._DeleteBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._DeleteBtn.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._DeleteBtn.IsActive = false;
            this._DeleteBtn.Location = new System.Drawing.Point(161, 596);
            this._DeleteBtn.MultiSelectEn = false;
            this._DeleteBtn.Name = "_DeleteBtn";
            this._DeleteBtn.OutLineColor = System.Drawing.Color.Empty;
            this._DeleteBtn.OutLineEn = true;
            this._DeleteBtn.OutLineSize = 3;
            this._DeleteBtn.Selectable = true;
            this._DeleteBtn.Selected = false;
            this._DeleteBtn.Size = new System.Drawing.Size(142, 50);
            this._DeleteBtn.StatusLedEnable = false;
            this._DeleteBtn.StatusLedSize = ((byte)(10));
            this._DeleteBtn.TabIndex = 186;
            this._DeleteBtn.TabStop = false;
            this._DeleteBtn.Text = "削除";
            this._DeleteBtn.UseVisualStyleBackColor = false;
            this._DeleteBtn.Click += new System.EventHandler(this._DeleteBtn_Click);
            // 
            // _SelectBtn
            // 
            this._SelectBtn.EditBox = null;
            this._SelectBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this._SelectBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._SelectBtn.Font = new System.Drawing.Font("Meiryo UI", 30.25F, System.Drawing.FontStyle.Bold);
            this._SelectBtn.IsActive = false;
            this._SelectBtn.Location = new System.Drawing.Point(602, 323);
            this._SelectBtn.MultiSelectEn = false;
            this._SelectBtn.Name = "_SelectBtn";
            this._SelectBtn.OutLineColor = System.Drawing.Color.Empty;
            this._SelectBtn.OutLineEn = true;
            this._SelectBtn.OutLineSize = 3;
            this._SelectBtn.Selectable = true;
            this._SelectBtn.Selected = false;
            this._SelectBtn.Size = new System.Drawing.Size(124, 82);
            this._SelectBtn.StatusLedEnable = false;
            this._SelectBtn.StatusLedSize = ((byte)(10));
            this._SelectBtn.TabIndex = 185;
            this._SelectBtn.TabStop = false;
            this._SelectBtn.Text = "➡";
            this._SelectBtn.UseVisualStyleBackColor = false;
            this._SelectBtn.Click += new System.EventHandler(this._SelectBtn_Click);
            // 
            // label17
            // 
            this.label17.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label17.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label17.Location = new System.Drawing.Point(6, 9);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(1012, 30);
            this.label17.TabIndex = 183;
            this.label17.Text = "TEACH";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _parameterList
            // 
            this._parameterList.AllowUserToAddRows = false;
            this._parameterList.AllowUserToDeleteRows = false;
            this._parameterList.AllowUserToResizeColumns = false;
            this._parameterList.AllowUserToResizeRows = false;
            this._parameterList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
            this._parameterList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._parameterList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._parameterList.ColumnHeadersVisible = false;
            this._parameterList.EnableLastIndex = true;
            this._parameterList.GridColor = System.Drawing.Color.Gainsboro;
            this._parameterList.Location = new System.Drawing.Point(10, 64);
            this._parameterList.Name = "_parameterList";
            this._parameterList.RowHeadersVisible = false;
            this._parameterList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this._parameterList.RowTemplate.Height = 50;
            this._parameterList.Size = new System.Drawing.Size(586, 518);
            this._parameterList.TabIndex = 1;
            this._parameterList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewEx1_CellClick);
            // 
            // _SelectedValueList
            // 
            this._SelectedValueList.AllowUserToAddRows = false;
            this._SelectedValueList.AllowUserToDeleteRows = false;
            this._SelectedValueList.AllowUserToResizeColumns = false;
            this._SelectedValueList.AllowUserToResizeRows = false;
            this._SelectedValueList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
            this._SelectedValueList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._SelectedValueList.ColumnHeadersVisible = false;
            this._SelectedValueList.EnableLastIndex = true;
            this._SelectedValueList.GridColor = System.Drawing.Color.Gainsboro;
            this._SelectedValueList.Location = new System.Drawing.Point(732, 64);
            this._SelectedValueList.Name = "_SelectedValueList";
            this._SelectedValueList.RowHeadersVisible = false;
            this._SelectedValueList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this._SelectedValueList.RowTemplate.Height = 50;
            this._SelectedValueList.Size = new System.Drawing.Size(280, 518);
            this._SelectedValueList.TabIndex = 190;
            this._SelectedValueList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this._SelectedValueList_CellClick);
            // 
            // _EditBtn
            // 
            this._EditBtn.EditBox = null;
            this._EditBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this._EditBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._EditBtn.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._EditBtn.IsActive = false;
            this._EditBtn.Location = new System.Drawing.Point(13, 596);
            this._EditBtn.MultiSelectEn = false;
            this._EditBtn.Name = "_EditBtn";
            this._EditBtn.OutLineColor = System.Drawing.Color.Empty;
            this._EditBtn.OutLineEn = true;
            this._EditBtn.OutLineSize = 3;
            this._EditBtn.Selectable = true;
            this._EditBtn.Selected = false;
            this._EditBtn.Size = new System.Drawing.Size(142, 50);
            this._EditBtn.StatusLedEnable = false;
            this._EditBtn.StatusLedSize = ((byte)(10));
            this._EditBtn.TabIndex = 191;
            this._EditBtn.TabStop = false;
            this._EditBtn.Text = "編集";
            this._EditBtn.UseVisualStyleBackColor = false;
            this._EditBtn.Click += new System.EventHandler(this._EditBtn_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this._CloseBtn);
            this.panel2.Font = new System.Drawing.Font("HG丸ｺﾞｼｯｸM-PRO", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.panel2.Location = new System.Drawing.Point(857, 588);
            this.panel2.Name = "panel2";
            this.panel2.OutLineColor = System.Drawing.Color.DarkOliveGreen;
            this.panel2.OutLineSize = 2;
            this.panel2.Size = new System.Drawing.Size(155, 65);
            this.panel2.TabIndex = 192;
            // 
            // TeachTableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(1024, 658);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this._EditBtn);
            this.Controls.Add(this._SelectedValueList);
            this.Controls.Add(this._PasteBtn);
            this.Controls.Add(this._CopyBtn);
            this.Controls.Add(this._DeleteBtn);
            this.Controls.Add(this._SelectBtn);
            this.Controls.Add(this.label17);
            this.Controls.Add(this._parameterList);
            this.ForeColor = System.Drawing.Color.Gainsboro;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(0, 90);
            this.Name = "TeachTableForm";
            this.OutLineSize = 3;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "TeachTableForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TeachTableForm_FormClosing);
            this.Load += new System.EventHandler(this.TeachTableForm_Load);
            this.Shown += new System.EventHandler(this.TeachTableForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this._parameterList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._SelectedValueList)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DataGridViewEx _parameterList;
        private LabelEx label17;
        private ButtonEx _DeleteBtn;
        private ButtonEx _CopyBtn;
        private ButtonEx _PasteBtn;
        private ButtonEx _CloseBtn;
        private DataGridViewEx _SelectedValueList;
        private ButtonEx _EditBtn;
        private ButtonEx _SelectBtn;
        private PanelEx panel2;
    }
}