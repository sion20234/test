using System.Drawing;
using System.Windows.Forms;

namespace ECNC3.Views
{
    partial class ParameterViewForm
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
            this._prmCatPanel = new ECNC3.Views.PanelEx();
            this.INITIALPARAMBt = new ECNC3.Views.ButtonEx();
            this.SERVOPARAMBt = new ECNC3.Views.ButtonEx();
            this.CPSDATABt = new ECNC3.Views.ButtonEx();
            this.IPDATABt = new ECNC3.Views.ButtonEx();
            this.OTHERSETBt = new ECNC3.Views.ButtonEx();
            this.SERIALSETBt = new ECNC3.Views.ButtonEx();
            this.SEVOPCONBt = new ECNC3.Views.ButtonEx();
            this.IOCONFIGBt = new ECNC3.Views.ButtonEx();
            this.GMCORDSETBt = new ECNC3.Views.ButtonEx();
            this.OVERRIDEBt = new ECNC3.Views.ButtonEx();
            this.CRSSPEEDBt = new ECNC3.Views.ButtonEx();
            this.dataGridViewEx1 = new ECNC3.Views.DataGridViewEx();
            this.ListEditPanel = new ECNC3.Views.PanelEx();
            this.LineInsertBt = new ECNC3.Views.ButtonEx();
            this.ListReloadBt = new ECNC3.Views.ButtonEx();
            this.ParamSaveBt = new ECNC3.Views.ButtonEx();
            this.LineDeleteBt = new ECNC3.Views.ButtonEx();
            this.CloseBt = new ECNC3.Views.ButtonEx();
            this.panelEx1 = new ECNC3.Views.PanelEx();
            this._prmCatPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEx1)).BeginInit();
            this.ListEditPanel.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _prmCatPanel
            // 
            this._prmCatPanel.Controls.Add(this.INITIALPARAMBt);
            this._prmCatPanel.Controls.Add(this.SERVOPARAMBt);
            this._prmCatPanel.Controls.Add(this.CPSDATABt);
            this._prmCatPanel.Controls.Add(this.IPDATABt);
            this._prmCatPanel.Controls.Add(this.OTHERSETBt);
            this._prmCatPanel.Controls.Add(this.SERIALSETBt);
            this._prmCatPanel.Controls.Add(this.SEVOPCONBt);
            this._prmCatPanel.Controls.Add(this.IOCONFIGBt);
            this._prmCatPanel.Controls.Add(this.GMCORDSETBt);
            this._prmCatPanel.Controls.Add(this.OVERRIDEBt);
            this._prmCatPanel.Controls.Add(this.CRSSPEEDBt);
            this._prmCatPanel.Location = new System.Drawing.Point(819, 10);
            this._prmCatPanel.Name = "_prmCatPanel";
            this._prmCatPanel.OutLineColor = System.Drawing.Color.DarkOliveGreen;
            this._prmCatPanel.OutLineSize = 2;
            this._prmCatPanel.Size = new System.Drawing.Size(200, 417);
            this._prmCatPanel.TabIndex = 19;
            // 
            // INITIALPARAMBt
            // 
            this.INITIALPARAMBt.EditBox = null;
            this.INITIALPARAMBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.INITIALPARAMBt.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.INITIALPARAMBt.IsActive = false;
            this.INITIALPARAMBt.Location = new System.Drawing.Point(11, 11);
            this.INITIALPARAMBt.MultiSelectEn = false;
            this.INITIALPARAMBt.Name = "INITIALPARAMBt";
            this.INITIALPARAMBt.OutLineColor = System.Drawing.Color.Empty;
            this.INITIALPARAMBt.OutLineEn = true;
            this.INITIALPARAMBt.OutLineSize = 3;
            this.INITIALPARAMBt.Selectable = true;
            this.INITIALPARAMBt.Selected = false;
            this.INITIALPARAMBt.Size = new System.Drawing.Size(178, 35);
            this.INITIALPARAMBt.StatusLedEnable = false;
            this.INITIALPARAMBt.StatusLedSize = ((byte)(15));
            this.INITIALPARAMBt.TabIndex = 2;
            this.INITIALPARAMBt.Text = "INITIAL PARAM";
            this.INITIALPARAMBt.UseVisualStyleBackColor = false;
            this.INITIALPARAMBt.Click += new System.EventHandler(this.INITIALPARAMBt_Click);
            this.INITIALPARAMBt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PARAMBt_MouseDown);
            // 
            // SERVOPARAMBt
            // 
            this.SERVOPARAMBt.EditBox = null;
            this.SERVOPARAMBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SERVOPARAMBt.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SERVOPARAMBt.IsActive = false;
            this.SERVOPARAMBt.Location = new System.Drawing.Point(11, 47);
            this.SERVOPARAMBt.MultiSelectEn = false;
            this.SERVOPARAMBt.Name = "SERVOPARAMBt";
            this.SERVOPARAMBt.OutLineColor = System.Drawing.Color.Empty;
            this.SERVOPARAMBt.OutLineEn = true;
            this.SERVOPARAMBt.OutLineSize = 3;
            this.SERVOPARAMBt.Selectable = true;
            this.SERVOPARAMBt.Selected = false;
            this.SERVOPARAMBt.Size = new System.Drawing.Size(178, 35);
            this.SERVOPARAMBt.StatusLedEnable = false;
            this.SERVOPARAMBt.StatusLedSize = ((byte)(15));
            this.SERVOPARAMBt.TabIndex = 3;
            this.SERVOPARAMBt.Text = "SERVO PARAM";
            this.SERVOPARAMBt.UseVisualStyleBackColor = false;
            this.SERVOPARAMBt.Click += new System.EventHandler(this.SERVOPARAMBt_Click);
            this.SERVOPARAMBt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PARAMBt_MouseDown);
            // 
            // CPSDATABt
            // 
            this.CPSDATABt.EditBox = null;
            this.CPSDATABt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CPSDATABt.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CPSDATABt.IsActive = false;
            this.CPSDATABt.Location = new System.Drawing.Point(11, 83);
            this.CPSDATABt.MultiSelectEn = false;
            this.CPSDATABt.Name = "CPSDATABt";
            this.CPSDATABt.OutLineColor = System.Drawing.Color.Empty;
            this.CPSDATABt.OutLineEn = true;
            this.CPSDATABt.OutLineSize = 3;
            this.CPSDATABt.Selectable = true;
            this.CPSDATABt.Selected = false;
            this.CPSDATABt.Size = new System.Drawing.Size(178, 35);
            this.CPSDATABt.StatusLedEnable = false;
            this.CPSDATABt.StatusLedSize = ((byte)(15));
            this.CPSDATABt.TabIndex = 4;
            this.CPSDATABt.Text = "CPS DATA";
            this.CPSDATABt.UseVisualStyleBackColor = false;
            this.CPSDATABt.Click += new System.EventHandler(this.CPSDATABt_Click);
            this.CPSDATABt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PARAMBt_MouseDown);
            // 
            // IPDATABt
            // 
            this.IPDATABt.EditBox = null;
            this.IPDATABt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IPDATABt.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.IPDATABt.IsActive = false;
            this.IPDATABt.Location = new System.Drawing.Point(11, 119);
            this.IPDATABt.MultiSelectEn = false;
            this.IPDATABt.Name = "IPDATABt";
            this.IPDATABt.OutLineColor = System.Drawing.Color.Empty;
            this.IPDATABt.OutLineEn = true;
            this.IPDATABt.OutLineSize = 3;
            this.IPDATABt.Selectable = true;
            this.IPDATABt.Selected = false;
            this.IPDATABt.Size = new System.Drawing.Size(178, 35);
            this.IPDATABt.StatusLedEnable = false;
            this.IPDATABt.StatusLedSize = ((byte)(15));
            this.IPDATABt.TabIndex = 5;
            this.IPDATABt.Text = "IP DATA";
            this.IPDATABt.UseVisualStyleBackColor = false;
            this.IPDATABt.Click += new System.EventHandler(this.IPDATABt_Click);
            this.IPDATABt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PARAMBt_MouseDown);
            // 
            // OTHERSETBt
            // 
            this.OTHERSETBt.EditBox = null;
            this.OTHERSETBt.Enabled = false;
            this.OTHERSETBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OTHERSETBt.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.OTHERSETBt.IsActive = false;
            this.OTHERSETBt.Location = new System.Drawing.Point(11, 371);
            this.OTHERSETBt.MultiSelectEn = false;
            this.OTHERSETBt.Name = "OTHERSETBt";
            this.OTHERSETBt.OutLineColor = System.Drawing.Color.Empty;
            this.OTHERSETBt.OutLineEn = true;
            this.OTHERSETBt.OutLineSize = 3;
            this.OTHERSETBt.Selectable = true;
            this.OTHERSETBt.Selected = false;
            this.OTHERSETBt.Size = new System.Drawing.Size(178, 35);
            this.OTHERSETBt.StatusLedEnable = false;
            this.OTHERSETBt.StatusLedSize = ((byte)(15));
            this.OTHERSETBt.TabIndex = 12;
            this.OTHERSETBt.Text = "OTHER SET";
            this.OTHERSETBt.UseVisualStyleBackColor = false;
            this.OTHERSETBt.Click += new System.EventHandler(this.OTHERSETBt_Click);
            this.OTHERSETBt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PARAMBt_MouseDown);
            // 
            // SERIALSETBt
            // 
            this.SERIALSETBt.EditBox = null;
            this.SERIALSETBt.Enabled = false;
            this.SERIALSETBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SERIALSETBt.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SERIALSETBt.IsActive = false;
            this.SERIALSETBt.Location = new System.Drawing.Point(11, 155);
            this.SERIALSETBt.MultiSelectEn = false;
            this.SERIALSETBt.Name = "SERIALSETBt";
            this.SERIALSETBt.OutLineColor = System.Drawing.Color.Empty;
            this.SERIALSETBt.OutLineEn = true;
            this.SERIALSETBt.OutLineSize = 3;
            this.SERIALSETBt.Selectable = true;
            this.SERIALSETBt.Selected = false;
            this.SERIALSETBt.Size = new System.Drawing.Size(178, 35);
            this.SERIALSETBt.StatusLedEnable = false;
            this.SERIALSETBt.StatusLedSize = ((byte)(15));
            this.SERIALSETBt.TabIndex = 6;
            this.SERIALSETBt.Text = "SERIAL SET";
            this.SERIALSETBt.UseVisualStyleBackColor = false;
            this.SERIALSETBt.Click += new System.EventHandler(this.SERIALSETBt_Click);
            this.SERIALSETBt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PARAMBt_MouseDown);
            // 
            // SEVOPCONBt
            // 
            this.SEVOPCONBt.EditBox = null;
            this.SEVOPCONBt.Enabled = false;
            this.SEVOPCONBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SEVOPCONBt.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SEVOPCONBt.IsActive = false;
            this.SEVOPCONBt.Location = new System.Drawing.Point(11, 335);
            this.SEVOPCONBt.MultiSelectEn = false;
            this.SEVOPCONBt.Name = "SEVOPCONBt";
            this.SEVOPCONBt.OutLineColor = System.Drawing.Color.Empty;
            this.SEVOPCONBt.OutLineEn = true;
            this.SEVOPCONBt.OutLineSize = 3;
            this.SEVOPCONBt.Selectable = true;
            this.SEVOPCONBt.Selected = false;
            this.SEVOPCONBt.Size = new System.Drawing.Size(178, 35);
            this.SEVOPCONBt.StatusLedEnable = false;
            this.SEVOPCONBt.StatusLedSize = ((byte)(15));
            this.SEVOPCONBt.TabIndex = 11;
            this.SEVOPCONBt.Text = "SERVO P CONDITION";
            this.SEVOPCONBt.UseVisualStyleBackColor = false;
            this.SEVOPCONBt.Click += new System.EventHandler(this.SEVOPCONBt_Click);
            this.SEVOPCONBt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PARAMBt_MouseDown);
            // 
            // IOCONFIGBt
            // 
            this.IOCONFIGBt.EditBox = null;
            this.IOCONFIGBt.Enabled = false;
            this.IOCONFIGBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IOCONFIGBt.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.IOCONFIGBt.IsActive = false;
            this.IOCONFIGBt.Location = new System.Drawing.Point(11, 191);
            this.IOCONFIGBt.MultiSelectEn = false;
            this.IOCONFIGBt.Name = "IOCONFIGBt";
            this.IOCONFIGBt.OutLineColor = System.Drawing.Color.Empty;
            this.IOCONFIGBt.OutLineEn = true;
            this.IOCONFIGBt.OutLineSize = 3;
            this.IOCONFIGBt.Selectable = true;
            this.IOCONFIGBt.Selected = false;
            this.IOCONFIGBt.Size = new System.Drawing.Size(178, 35);
            this.IOCONFIGBt.StatusLedEnable = false;
            this.IOCONFIGBt.StatusLedSize = ((byte)(15));
            this.IOCONFIGBt.TabIndex = 7;
            this.IOCONFIGBt.Text = "I/O CONFIG";
            this.IOCONFIGBt.UseVisualStyleBackColor = false;
            this.IOCONFIGBt.Click += new System.EventHandler(this.IOCONFIGBt_Click);
            this.IOCONFIGBt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PARAMBt_MouseDown);
            // 
            // GMCORDSETBt
            // 
            this.GMCORDSETBt.EditBox = null;
            this.GMCORDSETBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GMCORDSETBt.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GMCORDSETBt.IsActive = false;
            this.GMCORDSETBt.Location = new System.Drawing.Point(11, 299);
            this.GMCORDSETBt.MultiSelectEn = false;
            this.GMCORDSETBt.Name = "GMCORDSETBt";
            this.GMCORDSETBt.OutLineColor = System.Drawing.Color.Empty;
            this.GMCORDSETBt.OutLineEn = true;
            this.GMCORDSETBt.OutLineSize = 3;
            this.GMCORDSETBt.Selectable = true;
            this.GMCORDSETBt.Selected = false;
            this.GMCORDSETBt.Size = new System.Drawing.Size(178, 35);
            this.GMCORDSETBt.StatusLedEnable = false;
            this.GMCORDSETBt.StatusLedSize = ((byte)(15));
            this.GMCORDSETBt.TabIndex = 10;
            this.GMCORDSETBt.Text = "GM CORD SETTING";
            this.GMCORDSETBt.UseVisualStyleBackColor = false;
            this.GMCORDSETBt.Click += new System.EventHandler(this.GMCORDSETBt_Click);
            this.GMCORDSETBt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PARAMBt_MouseDown);
            // 
            // OVERRIDEBt
            // 
            this.OVERRIDEBt.EditBox = null;
            this.OVERRIDEBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OVERRIDEBt.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.OVERRIDEBt.IsActive = false;
            this.OVERRIDEBt.Location = new System.Drawing.Point(11, 227);
            this.OVERRIDEBt.MultiSelectEn = false;
            this.OVERRIDEBt.Name = "OVERRIDEBt";
            this.OVERRIDEBt.OutLineColor = System.Drawing.Color.Empty;
            this.OVERRIDEBt.OutLineEn = true;
            this.OVERRIDEBt.OutLineSize = 3;
            this.OVERRIDEBt.Selectable = true;
            this.OVERRIDEBt.Selected = false;
            this.OVERRIDEBt.Size = new System.Drawing.Size(178, 35);
            this.OVERRIDEBt.StatusLedEnable = false;
            this.OVERRIDEBt.StatusLedSize = ((byte)(15));
            this.OVERRIDEBt.TabIndex = 8;
            this.OVERRIDEBt.Text = "OVERRIDE";
            this.OVERRIDEBt.UseVisualStyleBackColor = false;
            this.OVERRIDEBt.Click += new System.EventHandler(this.OVERRIDEBt_Click);
            this.OVERRIDEBt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PARAMBt_MouseDown);
            // 
            // CRSSPEEDBt
            // 
            this.CRSSPEEDBt.EditBox = null;
            this.CRSSPEEDBt.Enabled = false;
            this.CRSSPEEDBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CRSSPEEDBt.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CRSSPEEDBt.IsActive = false;
            this.CRSSPEEDBt.Location = new System.Drawing.Point(11, 263);
            this.CRSSPEEDBt.MultiSelectEn = false;
            this.CRSSPEEDBt.Name = "CRSSPEEDBt";
            this.CRSSPEEDBt.OutLineColor = System.Drawing.Color.Empty;
            this.CRSSPEEDBt.OutLineEn = true;
            this.CRSSPEEDBt.OutLineSize = 3;
            this.CRSSPEEDBt.Selectable = true;
            this.CRSSPEEDBt.Selected = false;
            this.CRSSPEEDBt.Size = new System.Drawing.Size(178, 35);
            this.CRSSPEEDBt.StatusLedEnable = false;
            this.CRSSPEEDBt.StatusLedSize = ((byte)(15));
            this.CRSSPEEDBt.TabIndex = 9;
            this.CRSSPEEDBt.Text = "CRS SPEED";
            this.CRSSPEEDBt.UseVisualStyleBackColor = false;
            this.CRSSPEEDBt.Click += new System.EventHandler(this.CRSSPEEDBt_Click);
            this.CRSSPEEDBt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PARAMBt_MouseDown);
            // 
            // dataGridViewEx1
            // 
            this.dataGridViewEx1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(0)))));
            this.dataGridViewEx1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEx1.EnableLastIndex = true;
            this.dataGridViewEx1.Location = new System.Drawing.Point(5, 6);
            this.dataGridViewEx1.Name = "dataGridViewEx1";
            this.dataGridViewEx1.RowTemplate.Height = 21;
            this.dataGridViewEx1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewEx1.Size = new System.Drawing.Size(808, 757);
            this.dataGridViewEx1.TabIndex = 20;
            this.dataGridViewEx1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewEx1_CellClick);
            // 
            // ListEditPanel
            // 
            this.ListEditPanel.Controls.Add(this.LineInsertBt);
            this.ListEditPanel.Controls.Add(this.ListReloadBt);
            this.ListEditPanel.Controls.Add(this.ParamSaveBt);
            this.ListEditPanel.Controls.Add(this.LineDeleteBt);
            this.ListEditPanel.Location = new System.Drawing.Point(819, 503);
            this.ListEditPanel.Name = "ListEditPanel";
            this.ListEditPanel.OutLineColor = System.Drawing.Color.DarkOliveGreen;
            this.ListEditPanel.OutLineSize = 2;
            this.ListEditPanel.Size = new System.Drawing.Size(200, 128);
            this.ListEditPanel.TabIndex = 19;
            // 
            // LineInsertBt
            // 
            this.LineInsertBt.EditBox = null;
            this.LineInsertBt.Enabled = false;
            this.LineInsertBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LineInsertBt.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LineInsertBt.IsActive = false;
            this.LineInsertBt.Location = new System.Drawing.Point(9, 8);
            this.LineInsertBt.MultiSelectEn = false;
            this.LineInsertBt.Name = "LineInsertBt";
            this.LineInsertBt.OutLineColor = System.Drawing.Color.Empty;
            this.LineInsertBt.OutLineEn = true;
            this.LineInsertBt.OutLineSize = 3;
            this.LineInsertBt.Selectable = true;
            this.LineInsertBt.Selected = false;
            this.LineInsertBt.Size = new System.Drawing.Size(89, 50);
            this.LineInsertBt.StatusLedEnable = false;
            this.LineInsertBt.StatusLedSize = ((byte)(15));
            this.LineInsertBt.TabIndex = 19;
            this.LineInsertBt.Text = "Line\r\nInsert";
            this.LineInsertBt.UseVisualStyleBackColor = false;
            this.LineInsertBt.Click += new System.EventHandler(this.LineInsertBt_Click);
            // 
            // ListReloadBt
            // 
            this.ListReloadBt.EditBox = null;
            this.ListReloadBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ListReloadBt.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ListReloadBt.IsActive = false;
            this.ListReloadBt.Location = new System.Drawing.Point(103, 63);
            this.ListReloadBt.MultiSelectEn = false;
            this.ListReloadBt.Name = "ListReloadBt";
            this.ListReloadBt.OutLineColor = System.Drawing.Color.Empty;
            this.ListReloadBt.OutLineEn = true;
            this.ListReloadBt.OutLineSize = 3;
            this.ListReloadBt.Selectable = true;
            this.ListReloadBt.Selected = false;
            this.ListReloadBt.Size = new System.Drawing.Size(89, 56);
            this.ListReloadBt.StatusLedEnable = false;
            this.ListReloadBt.StatusLedSize = ((byte)(15));
            this.ListReloadBt.TabIndex = 14;
            this.ListReloadBt.Text = "再読込";
            this.ListReloadBt.UseVisualStyleBackColor = false;
            this.ListReloadBt.Click += new System.EventHandler(this.ListReloadBt_Click);
            // 
            // ParamSaveBt
            // 
            this.ParamSaveBt.EditBox = null;
            this.ParamSaveBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ParamSaveBt.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ParamSaveBt.IsActive = false;
            this.ParamSaveBt.Location = new System.Drawing.Point(9, 63);
            this.ParamSaveBt.MultiSelectEn = false;
            this.ParamSaveBt.Name = "ParamSaveBt";
            this.ParamSaveBt.OutLineColor = System.Drawing.Color.Empty;
            this.ParamSaveBt.OutLineEn = true;
            this.ParamSaveBt.OutLineSize = 3;
            this.ParamSaveBt.Selectable = true;
            this.ParamSaveBt.Selected = false;
            this.ParamSaveBt.Size = new System.Drawing.Size(89, 56);
            this.ParamSaveBt.StatusLedEnable = false;
            this.ParamSaveBt.StatusLedSize = ((byte)(15));
            this.ParamSaveBt.TabIndex = 15;
            this.ParamSaveBt.Text = "保存";
            this.ParamSaveBt.UseVisualStyleBackColor = false;
            this.ParamSaveBt.Click += new System.EventHandler(this.ParamSaveBt_Click);
            // 
            // LineDeleteBt
            // 
            this.LineDeleteBt.EditBox = null;
            this.LineDeleteBt.Enabled = false;
            this.LineDeleteBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LineDeleteBt.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LineDeleteBt.IsActive = false;
            this.LineDeleteBt.Location = new System.Drawing.Point(102, 8);
            this.LineDeleteBt.MultiSelectEn = false;
            this.LineDeleteBt.Name = "LineDeleteBt";
            this.LineDeleteBt.OutLineColor = System.Drawing.Color.Empty;
            this.LineDeleteBt.OutLineEn = true;
            this.LineDeleteBt.OutLineSize = 3;
            this.LineDeleteBt.Selectable = true;
            this.LineDeleteBt.Selected = false;
            this.LineDeleteBt.Size = new System.Drawing.Size(90, 50);
            this.LineDeleteBt.StatusLedEnable = false;
            this.LineDeleteBt.StatusLedSize = ((byte)(15));
            this.LineDeleteBt.TabIndex = 16;
            this.LineDeleteBt.Text = "Line\r\nDelete";
            this.LineDeleteBt.UseVisualStyleBackColor = false;
            this.LineDeleteBt.Click += new System.EventHandler(this.LineDeleteBt_Click);
            // 
            // CloseBt
            // 
            this.CloseBt.EditBox = null;
            this.CloseBt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseBt.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CloseBt.IsActive = false;
            this.CloseBt.Location = new System.Drawing.Point(9, 10);
            this.CloseBt.MultiSelectEn = false;
            this.CloseBt.Name = "CloseBt";
            this.CloseBt.OutLineColor = System.Drawing.Color.Empty;
            this.CloseBt.OutLineEn = true;
            this.CloseBt.OutLineSize = 3;
            this.CloseBt.Selectable = true;
            this.CloseBt.Selected = false;
            this.CloseBt.Size = new System.Drawing.Size(183, 47);
            this.CloseBt.StatusLedEnable = false;
            this.CloseBt.StatusLedSize = ((byte)(15));
            this.CloseBt.TabIndex = 1;
            this.CloseBt.Text = "閉じる";
            this.CloseBt.UseVisualStyleBackColor = false;
            this.CloseBt.Click += new System.EventHandler(this.CloseBt_Click);
            // 
            // panelEx1
            // 
            this.panelEx1.Controls.Add(this.CloseBt);
            this.panelEx1.Location = new System.Drawing.Point(819, 687);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.OutLineColor = System.Drawing.Color.DarkOliveGreen;
            this.panelEx1.OutLineSize = 2;
            this.panelEx1.Size = new System.Drawing.Size(200, 69);
            this.panelEx1.TabIndex = 20;
            // 
            // ParameterViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this._prmCatPanel);
            this.Controls.Add(this.dataGridViewEx1);
            this.Controls.Add(this.ListEditPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ParameterViewForm";
            this.OutLineSize = 3;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ParameterViewForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ParameterViewForm_Load);
            this._prmCatPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEx1)).EndInit();
            this.ListEditPanel.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private ButtonEx CloseBt;
        private ButtonEx INITIALPARAMBt;
        private ButtonEx SERVOPARAMBt;
        private ButtonEx CPSDATABt;
        private ButtonEx IPDATABt;
        private ButtonEx SERIALSETBt;
        private ButtonEx IOCONFIGBt;
        private ButtonEx OVERRIDEBt;
        private ButtonEx CRSSPEEDBt;
        private ButtonEx GMCORDSETBt;
        private ButtonEx SEVOPCONBt;
        private ButtonEx OTHERSETBt;
        private ButtonEx ListReloadBt;
        private ButtonEx ParamSaveBt;
        private ButtonEx LineDeleteBt;
        private ButtonEx LineInsertBt;
        private PanelEx ListEditPanel;
        private DataGridViewEx dataGridViewEx1;
        private PanelEx _prmCatPanel;
        private PanelEx panelEx1;
    }
}