namespace ECNC3.Views
{
    partial class HelpForm
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
            this.panel1 = new ECNC3.Views.PanelEx();
            this.MCode_ButtonEx = new ECNC3.Views.ButtonEx();
            this.GCode_ButtonEx = new ECNC3.Views.ButtonEx();
            this.PDF_Content_ButtonEx = new ECNC3.Views.ButtonEx();
            this.panel3 = new ECNC3.Views.PanelEx();
            this.Back_ButtonEx = new ECNC3.Views.ButtonEx();
            this.label8 = new ECNC3.Views.LabelEx();
            this.PDF_TrackBar = new System.Windows.Forms.TrackBar();
            this.PDF_Button_Ver = new ECNC3.Views.ButtonEx();
            this.PDF_50P_buttonEx = new ECNC3.Views.ButtonEx();
            this.PDF_100P_buttonEx = new ECNC3.Views.ButtonEx();
            this.PDF_150P_buttonEx = new ECNC3.Views.ButtonEx();
            this.PDF_ZoomTextBoxEx = new ECNC3.Views.TextBoxEx();
            this.labelEx_Mag = new ECNC3.Views.LabelEx();
            this.PDF_PanelMode_ButtonEx_None = new ECNC3.Views.ButtonEx();
            this.PDF_PanelMode_ButtonEx_Thum = new ECNC3.Views.ButtonEx();
            this.PDF_PanelMode_ButtonEx_BM = new ECNC3.Views.ButtonEx();
            this.PDF_DontCare_buttonEx = new ECNC3.Views.ButtonEx();
            this.PDF_OneColumn_buttonEx = new ECNC3.Views.ButtonEx();
            this.PDF_TwoColumnLeft_buttonEx = new ECNC3.Views.ButtonEx();
            this.PDF_TwoColumnRight_buttonEx = new ECNC3.Views.ButtonEx();
            this.PDF_SinglePage_buttonEx = new ECNC3.Views.ButtonEx();
            this.PDF_SetNamedDist_buttonEx = new ECNC3.Views.ButtonEx();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.PDF_Find__buttonEx = new ECNC3.Views.ButtonEx();
            this.PDF_Button_DownBotom = new ECNC3.Views.ButtonEx();
            this.PDF_Button_UpTop = new ECNC3.Views.ButtonEx();
            this.PDF_Button_Down = new ECNC3.Views.ButtonEx();
            this.PDF_Button_Up = new ECNC3.Views.ButtonEx();
            this.PDF_TextBoxEx = new ECNC3.Views.TextBoxEx();
            this.labelEx_Page = new ECNC3.Views.LabelEx();
            this.pdfViewer1 = new PdfiumViewer.PdfViewer();
            this.textBoxEx_Find = new ECNC3.Views.TextBoxEx();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PDF_TrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.MCode_ButtonEx);
            this.panel1.Controls.Add(this.GCode_ButtonEx);
            this.panel1.Controls.Add(this.PDF_Content_ButtonEx);
            this.panel1.Location = new System.Drawing.Point(12, 601);
            this.panel1.Name = "panel1";
            this.panel1.OutLineColor = System.Drawing.Color.White;
            this.panel1.OutLineSize = 1;
            this.panel1.Size = new System.Drawing.Size(349, 70);
            this.panel1.TabIndex = 219;
            // 
            // MCode_ButtonEx
            // 
            this.MCode_ButtonEx.EditBox = null;
            this.MCode_ButtonEx.FlatAppearance.BorderSize = 0;
            this.MCode_ButtonEx.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.MCode_ButtonEx.IsActive = false;
            this.MCode_ButtonEx.LedSyncedBackColorEnable = true;
            this.MCode_ButtonEx.Location = new System.Drawing.Point(121, 8);
            this.MCode_ButtonEx.MultiSelectEn = false;
            this.MCode_ButtonEx.Name = "MCode_ButtonEx";
            this.MCode_ButtonEx.OutLineColor = System.Drawing.Color.Empty;
            this.MCode_ButtonEx.OutLineEn = true;
            this.MCode_ButtonEx.OutLineSize = 3;
            this.MCode_ButtonEx.Selectable = true;
            this.MCode_ButtonEx.Selected = false;
            this.MCode_ButtonEx.Size = new System.Drawing.Size(107, 55);
            this.MCode_ButtonEx.StatusLedEnable = false;
            this.MCode_ButtonEx.StatusLedSize = ((byte)(15));
            this.MCode_ButtonEx.TabIndex = 1;
            this.MCode_ButtonEx.Text = "Mコード";
            this.MCode_ButtonEx.UseVisualStyleBackColor = false;
            this.MCode_ButtonEx.Click += new System.EventHandler(this.MCode_ButtonEx_Click);
            // 
            // GCode_ButtonEx
            // 
            this.GCode_ButtonEx.EditBox = null;
            this.GCode_ButtonEx.FlatAppearance.BorderSize = 0;
            this.GCode_ButtonEx.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GCode_ButtonEx.IsActive = false;
            this.GCode_ButtonEx.LedSyncedBackColorEnable = true;
            this.GCode_ButtonEx.Location = new System.Drawing.Point(8, 8);
            this.GCode_ButtonEx.MultiSelectEn = false;
            this.GCode_ButtonEx.Name = "GCode_ButtonEx";
            this.GCode_ButtonEx.OutLineColor = System.Drawing.Color.Empty;
            this.GCode_ButtonEx.OutLineEn = true;
            this.GCode_ButtonEx.OutLineSize = 3;
            this.GCode_ButtonEx.Selectable = true;
            this.GCode_ButtonEx.Selected = false;
            this.GCode_ButtonEx.Size = new System.Drawing.Size(107, 55);
            this.GCode_ButtonEx.StatusLedEnable = false;
            this.GCode_ButtonEx.StatusLedSize = ((byte)(15));
            this.GCode_ButtonEx.TabIndex = 0;
            this.GCode_ButtonEx.Text = "Gコード";
            this.GCode_ButtonEx.UseVisualStyleBackColor = false;
            this.GCode_ButtonEx.Click += new System.EventHandler(this.GCode_ButtonEx_Click);
            // 
            // PDF_Content_ButtonEx
            // 
            this.PDF_Content_ButtonEx.EditBox = null;
            this.PDF_Content_ButtonEx.FlatAppearance.BorderSize = 0;
            this.PDF_Content_ButtonEx.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PDF_Content_ButtonEx.IsActive = false;
            this.PDF_Content_ButtonEx.LedSyncedBackColorEnable = true;
            this.PDF_Content_ButtonEx.Location = new System.Drawing.Point(234, 8);
            this.PDF_Content_ButtonEx.MultiSelectEn = false;
            this.PDF_Content_ButtonEx.Name = "PDF_Content_ButtonEx";
            this.PDF_Content_ButtonEx.OutLineColor = System.Drawing.Color.Empty;
            this.PDF_Content_ButtonEx.OutLineEn = true;
            this.PDF_Content_ButtonEx.OutLineSize = 3;
            this.PDF_Content_ButtonEx.Selectable = true;
            this.PDF_Content_ButtonEx.Selected = false;
            this.PDF_Content_ButtonEx.Size = new System.Drawing.Size(110, 55);
            this.PDF_Content_ButtonEx.StatusLedEnable = false;
            this.PDF_Content_ButtonEx.StatusLedSize = ((byte)(15));
            this.PDF_Content_ButtonEx.TabIndex = 236;
            this.PDF_Content_ButtonEx.Text = "目次に移動";
            this.PDF_Content_ButtonEx.UseVisualStyleBackColor = false;
            this.PDF_Content_ButtonEx.Click += new System.EventHandler(this.PDF_Content_ButtonEx_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.Back_ButtonEx);
            this.panel3.Location = new System.Drawing.Point(869, 601);
            this.panel3.Name = "panel3";
            this.panel3.OutLineColor = System.Drawing.Color.DarkOliveGreen;
            this.panel3.OutLineSize = 2;
            this.panel3.Size = new System.Drawing.Size(149, 70);
            this.panel3.TabIndex = 218;
            // 
            // Back_ButtonEx
            // 
            this.Back_ButtonEx.EditBox = null;
            this.Back_ButtonEx.FlatAppearance.BorderSize = 0;
            this.Back_ButtonEx.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Back_ButtonEx.IsActive = false;
            this.Back_ButtonEx.LedSyncedBackColorEnable = true;
            this.Back_ButtonEx.Location = new System.Drawing.Point(6, 9);
            this.Back_ButtonEx.MultiSelectEn = false;
            this.Back_ButtonEx.Name = "Back_ButtonEx";
            this.Back_ButtonEx.OutLineColor = System.Drawing.Color.Empty;
            this.Back_ButtonEx.OutLineEn = true;
            this.Back_ButtonEx.OutLineSize = 3;
            this.Back_ButtonEx.Selectable = true;
            this.Back_ButtonEx.Selected = false;
            this.Back_ButtonEx.Size = new System.Drawing.Size(140, 55);
            this.Back_ButtonEx.StatusLedEnable = false;
            this.Back_ButtonEx.StatusLedSize = ((byte)(15));
            this.Back_ButtonEx.TabIndex = 237;
            this.Back_ButtonEx.Text = "閉じる";
            this.Back_ButtonEx.UseVisualStyleBackColor = false;
            this.Back_ButtonEx.Click += new System.EventHandler(this.Back_ButtonEx_Click);
            // 
            // label8
            // 
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label8.Font = new System.Drawing.Font("HG丸ｺﾞｼｯｸM-PRO", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.Location = new System.Drawing.Point(6, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1014, 30);
            this.label8.TabIndex = 216;
            this.label8.Text = "HELP  ";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PDF_TrackBar
            // 
            this.PDF_TrackBar.Location = new System.Drawing.Point(673, 611);
            this.PDF_TrackBar.Maximum = 300;
            this.PDF_TrackBar.Minimum = 5;
            this.PDF_TrackBar.Name = "PDF_TrackBar";
            this.PDF_TrackBar.Size = new System.Drawing.Size(166, 45);
            this.PDF_TrackBar.TabIndex = 220;
            this.PDF_TrackBar.Value = 100;
            this.PDF_TrackBar.Visible = false;
            this.PDF_TrackBar.Scroll += new System.EventHandler(this.PDF_TrackBar_Scroll);
            this.PDF_TrackBar.Move += new System.EventHandler(this.trackBar1_Move);
            // 
            // PDF_Button_Ver
            // 
            this.PDF_Button_Ver.EditBox = null;
            this.PDF_Button_Ver.FlatAppearance.BorderSize = 0;
            this.PDF_Button_Ver.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PDF_Button_Ver.IsActive = false;
            this.PDF_Button_Ver.LedSyncedBackColorEnable = true;
            this.PDF_Button_Ver.Location = new System.Drawing.Point(939, 43);
            this.PDF_Button_Ver.MultiSelectEn = false;
            this.PDF_Button_Ver.Name = "PDF_Button_Ver";
            this.PDF_Button_Ver.OutLineColor = System.Drawing.Color.Empty;
            this.PDF_Button_Ver.OutLineEn = true;
            this.PDF_Button_Ver.OutLineSize = 3;
            this.PDF_Button_Ver.Selectable = true;
            this.PDF_Button_Ver.Selected = false;
            this.PDF_Button_Ver.Size = new System.Drawing.Size(79, 40);
            this.PDF_Button_Ver.StatusLedEnable = false;
            this.PDF_Button_Ver.StatusLedSize = ((byte)(15));
            this.PDF_Button_Ver.TabIndex = 226;
            this.PDF_Button_Ver.Text = "Ver.";
            this.PDF_Button_Ver.UseVisualStyleBackColor = false;
            this.PDF_Button_Ver.Click += new System.EventHandler(this.PDF_Button_Ver_Click);
            // 
            // PDF_50P_buttonEx
            // 
            this.PDF_50P_buttonEx.EditBox = null;
            this.PDF_50P_buttonEx.FlatAppearance.BorderSize = 0;
            this.PDF_50P_buttonEx.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PDF_50P_buttonEx.IsActive = false;
            this.PDF_50P_buttonEx.LedSyncedBackColorEnable = true;
            this.PDF_50P_buttonEx.Location = new System.Drawing.Point(939, 433);
            this.PDF_50P_buttonEx.MultiSelectEn = false;
            this.PDF_50P_buttonEx.Name = "PDF_50P_buttonEx";
            this.PDF_50P_buttonEx.OutLineColor = System.Drawing.Color.Empty;
            this.PDF_50P_buttonEx.OutLineEn = true;
            this.PDF_50P_buttonEx.OutLineSize = 3;
            this.PDF_50P_buttonEx.Selectable = true;
            this.PDF_50P_buttonEx.Selected = false;
            this.PDF_50P_buttonEx.Size = new System.Drawing.Size(79, 40);
            this.PDF_50P_buttonEx.StatusLedEnable = false;
            this.PDF_50P_buttonEx.StatusLedSize = ((byte)(15));
            this.PDF_50P_buttonEx.TabIndex = 227;
            this.PDF_50P_buttonEx.Text = "50％";
            this.PDF_50P_buttonEx.UseVisualStyleBackColor = false;
            this.PDF_50P_buttonEx.Click += new System.EventHandler(this.PDF_50P_buttonEx_Click);
            // 
            // PDF_100P_buttonEx
            // 
            this.PDF_100P_buttonEx.EditBox = null;
            this.PDF_100P_buttonEx.FlatAppearance.BorderSize = 0;
            this.PDF_100P_buttonEx.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PDF_100P_buttonEx.IsActive = false;
            this.PDF_100P_buttonEx.LedSyncedBackColorEnable = true;
            this.PDF_100P_buttonEx.Location = new System.Drawing.Point(939, 483);
            this.PDF_100P_buttonEx.MultiSelectEn = false;
            this.PDF_100P_buttonEx.Name = "PDF_100P_buttonEx";
            this.PDF_100P_buttonEx.OutLineColor = System.Drawing.Color.Empty;
            this.PDF_100P_buttonEx.OutLineEn = true;
            this.PDF_100P_buttonEx.OutLineSize = 3;
            this.PDF_100P_buttonEx.Selectable = true;
            this.PDF_100P_buttonEx.Selected = false;
            this.PDF_100P_buttonEx.Size = new System.Drawing.Size(79, 40);
            this.PDF_100P_buttonEx.StatusLedEnable = false;
            this.PDF_100P_buttonEx.StatusLedSize = ((byte)(15));
            this.PDF_100P_buttonEx.TabIndex = 228;
            this.PDF_100P_buttonEx.Text = "100％";
            this.PDF_100P_buttonEx.UseVisualStyleBackColor = false;
            this.PDF_100P_buttonEx.Click += new System.EventHandler(this.PDF_100P_buttonEx_Click);
            // 
            // PDF_150P_buttonEx
            // 
            this.PDF_150P_buttonEx.EditBox = null;
            this.PDF_150P_buttonEx.FlatAppearance.BorderSize = 0;
            this.PDF_150P_buttonEx.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PDF_150P_buttonEx.IsActive = false;
            this.PDF_150P_buttonEx.LedSyncedBackColorEnable = true;
            this.PDF_150P_buttonEx.Location = new System.Drawing.Point(939, 533);
            this.PDF_150P_buttonEx.MultiSelectEn = false;
            this.PDF_150P_buttonEx.Name = "PDF_150P_buttonEx";
            this.PDF_150P_buttonEx.OutLineColor = System.Drawing.Color.Empty;
            this.PDF_150P_buttonEx.OutLineEn = true;
            this.PDF_150P_buttonEx.OutLineSize = 3;
            this.PDF_150P_buttonEx.Selectable = true;
            this.PDF_150P_buttonEx.Selected = false;
            this.PDF_150P_buttonEx.Size = new System.Drawing.Size(79, 40);
            this.PDF_150P_buttonEx.StatusLedEnable = false;
            this.PDF_150P_buttonEx.StatusLedSize = ((byte)(15));
            this.PDF_150P_buttonEx.TabIndex = 229;
            this.PDF_150P_buttonEx.Text = "150％";
            this.PDF_150P_buttonEx.UseVisualStyleBackColor = false;
            this.PDF_150P_buttonEx.Click += new System.EventHandler(this.PDF_150P_buttonEx_Click);
            // 
            // PDF_ZoomTextBoxEx
            // 
            this.PDF_ZoomTextBoxEx.Font = new System.Drawing.Font("MS UI Gothic", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PDF_ZoomTextBoxEx.Location = new System.Drawing.Point(939, 385);
            this.PDF_ZoomTextBoxEx.Name = "PDF_ZoomTextBoxEx";
            this.PDF_ZoomTextBoxEx.Size = new System.Drawing.Size(79, 42);
            this.PDF_ZoomTextBoxEx.TabIndex = 230;
            this.PDF_ZoomTextBoxEx.Text = "100";
            this.PDF_ZoomTextBoxEx.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PDF_ZoomTextBoxEx.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PDF_ZoomTextBoxEx_MouseClick);
            this.PDF_ZoomTextBoxEx.TextChanged += new System.EventHandler(this.PDF_ZoomTextBoxEx_TextChanged);
            // 
            // labelEx_Mag
            // 
            this.labelEx_Mag.AutoSize = true;
            this.labelEx_Mag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelEx_Mag.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelEx_Mag.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelEx_Mag.Location = new System.Drawing.Point(937, 364);
            this.labelEx_Mag.Name = "labelEx_Mag";
            this.labelEx_Mag.Size = new System.Drawing.Size(42, 18);
            this.labelEx_Mag.TabIndex = 0;
            this.labelEx_Mag.Text = "倍率";
            // 
            // PDF_PanelMode_ButtonEx_None
            // 
            this.PDF_PanelMode_ButtonEx_None.EditBox = null;
            this.PDF_PanelMode_ButtonEx_None.FlatAppearance.BorderSize = 0;
            this.PDF_PanelMode_ButtonEx_None.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PDF_PanelMode_ButtonEx_None.IsActive = false;
            this.PDF_PanelMode_ButtonEx_None.LedSyncedBackColorEnable = true;
            this.PDF_PanelMode_ButtonEx_None.Location = new System.Drawing.Point(427, 520);
            this.PDF_PanelMode_ButtonEx_None.MultiSelectEn = false;
            this.PDF_PanelMode_ButtonEx_None.Name = "PDF_PanelMode_ButtonEx_None";
            this.PDF_PanelMode_ButtonEx_None.OutLineColor = System.Drawing.Color.Empty;
            this.PDF_PanelMode_ButtonEx_None.OutLineEn = true;
            this.PDF_PanelMode_ButtonEx_None.OutLineSize = 3;
            this.PDF_PanelMode_ButtonEx_None.Selectable = true;
            this.PDF_PanelMode_ButtonEx_None.Selected = false;
            this.PDF_PanelMode_ButtonEx_None.Size = new System.Drawing.Size(77, 59);
            this.PDF_PanelMode_ButtonEx_None.StatusLedEnable = false;
            this.PDF_PanelMode_ButtonEx_None.StatusLedSize = ((byte)(15));
            this.PDF_PanelMode_ButtonEx_None.TabIndex = 233;
            this.PDF_PanelMode_ButtonEx_None.Text = "モード none";
            this.PDF_PanelMode_ButtonEx_None.UseVisualStyleBackColor = false;
            this.PDF_PanelMode_ButtonEx_None.Visible = false;
            // 
            // PDF_PanelMode_ButtonEx_Thum
            // 
            this.PDF_PanelMode_ButtonEx_Thum.EditBox = null;
            this.PDF_PanelMode_ButtonEx_Thum.FlatAppearance.BorderSize = 0;
            this.PDF_PanelMode_ButtonEx_Thum.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PDF_PanelMode_ButtonEx_Thum.IsActive = false;
            this.PDF_PanelMode_ButtonEx_Thum.LedSyncedBackColorEnable = true;
            this.PDF_PanelMode_ButtonEx_Thum.Location = new System.Drawing.Point(593, 520);
            this.PDF_PanelMode_ButtonEx_Thum.MultiSelectEn = false;
            this.PDF_PanelMode_ButtonEx_Thum.Name = "PDF_PanelMode_ButtonEx_Thum";
            this.PDF_PanelMode_ButtonEx_Thum.OutLineColor = System.Drawing.Color.Empty;
            this.PDF_PanelMode_ButtonEx_Thum.OutLineEn = true;
            this.PDF_PanelMode_ButtonEx_Thum.OutLineSize = 3;
            this.PDF_PanelMode_ButtonEx_Thum.Selectable = true;
            this.PDF_PanelMode_ButtonEx_Thum.Selected = false;
            this.PDF_PanelMode_ButtonEx_Thum.Size = new System.Drawing.Size(77, 59);
            this.PDF_PanelMode_ButtonEx_Thum.StatusLedEnable = false;
            this.PDF_PanelMode_ButtonEx_Thum.StatusLedSize = ((byte)(15));
            this.PDF_PanelMode_ButtonEx_Thum.TabIndex = 234;
            this.PDF_PanelMode_ButtonEx_Thum.Text = "モード thumbs";
            this.PDF_PanelMode_ButtonEx_Thum.UseVisualStyleBackColor = false;
            this.PDF_PanelMode_ButtonEx_Thum.Visible = false;
            // 
            // PDF_PanelMode_ButtonEx_BM
            // 
            this.PDF_PanelMode_ButtonEx_BM.EditBox = null;
            this.PDF_PanelMode_ButtonEx_BM.FlatAppearance.BorderSize = 0;
            this.PDF_PanelMode_ButtonEx_BM.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PDF_PanelMode_ButtonEx_BM.IsActive = false;
            this.PDF_PanelMode_ButtonEx_BM.LedSyncedBackColorEnable = true;
            this.PDF_PanelMode_ButtonEx_BM.Location = new System.Drawing.Point(510, 520);
            this.PDF_PanelMode_ButtonEx_BM.MultiSelectEn = false;
            this.PDF_PanelMode_ButtonEx_BM.Name = "PDF_PanelMode_ButtonEx_BM";
            this.PDF_PanelMode_ButtonEx_BM.OutLineColor = System.Drawing.Color.Empty;
            this.PDF_PanelMode_ButtonEx_BM.OutLineEn = true;
            this.PDF_PanelMode_ButtonEx_BM.OutLineSize = 3;
            this.PDF_PanelMode_ButtonEx_BM.Selectable = true;
            this.PDF_PanelMode_ButtonEx_BM.Selected = false;
            this.PDF_PanelMode_ButtonEx_BM.Size = new System.Drawing.Size(77, 59);
            this.PDF_PanelMode_ButtonEx_BM.StatusLedEnable = false;
            this.PDF_PanelMode_ButtonEx_BM.StatusLedSize = ((byte)(15));
            this.PDF_PanelMode_ButtonEx_BM.TabIndex = 235;
            this.PDF_PanelMode_ButtonEx_BM.Text = "モード Book";
            this.PDF_PanelMode_ButtonEx_BM.UseVisualStyleBackColor = false;
            this.PDF_PanelMode_ButtonEx_BM.Visible = false;
            // 
            // PDF_DontCare_buttonEx
            // 
            this.PDF_DontCare_buttonEx.EditBox = null;
            this.PDF_DontCare_buttonEx.FlatAppearance.BorderSize = 0;
            this.PDF_DontCare_buttonEx.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PDF_DontCare_buttonEx.IsActive = false;
            this.PDF_DontCare_buttonEx.LedSyncedBackColorEnable = true;
            this.PDF_DontCare_buttonEx.Location = new System.Drawing.Point(723, 335);
            this.PDF_DontCare_buttonEx.MultiSelectEn = false;
            this.PDF_DontCare_buttonEx.Name = "PDF_DontCare_buttonEx";
            this.PDF_DontCare_buttonEx.OutLineColor = System.Drawing.Color.Empty;
            this.PDF_DontCare_buttonEx.OutLineEn = true;
            this.PDF_DontCare_buttonEx.OutLineSize = 3;
            this.PDF_DontCare_buttonEx.Selectable = true;
            this.PDF_DontCare_buttonEx.Selected = false;
            this.PDF_DontCare_buttonEx.Size = new System.Drawing.Size(139, 34);
            this.PDF_DontCare_buttonEx.StatusLedEnable = false;
            this.PDF_DontCare_buttonEx.StatusLedSize = ((byte)(15));
            this.PDF_DontCare_buttonEx.TabIndex = 237;
            this.PDF_DontCare_buttonEx.Text = "DontCare";
            this.PDF_DontCare_buttonEx.UseVisualStyleBackColor = false;
            this.PDF_DontCare_buttonEx.Visible = false;
            // 
            // PDF_OneColumn_buttonEx
            // 
            this.PDF_OneColumn_buttonEx.EditBox = null;
            this.PDF_OneColumn_buttonEx.FlatAppearance.BorderSize = 0;
            this.PDF_OneColumn_buttonEx.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PDF_OneColumn_buttonEx.IsActive = false;
            this.PDF_OneColumn_buttonEx.LedSyncedBackColorEnable = true;
            this.PDF_OneColumn_buttonEx.Location = new System.Drawing.Point(723, 415);
            this.PDF_OneColumn_buttonEx.MultiSelectEn = false;
            this.PDF_OneColumn_buttonEx.Name = "PDF_OneColumn_buttonEx";
            this.PDF_OneColumn_buttonEx.OutLineColor = System.Drawing.Color.Empty;
            this.PDF_OneColumn_buttonEx.OutLineEn = true;
            this.PDF_OneColumn_buttonEx.OutLineSize = 3;
            this.PDF_OneColumn_buttonEx.Selectable = true;
            this.PDF_OneColumn_buttonEx.Selected = false;
            this.PDF_OneColumn_buttonEx.Size = new System.Drawing.Size(139, 36);
            this.PDF_OneColumn_buttonEx.StatusLedEnable = false;
            this.PDF_OneColumn_buttonEx.StatusLedSize = ((byte)(15));
            this.PDF_OneColumn_buttonEx.TabIndex = 238;
            this.PDF_OneColumn_buttonEx.Text = "OneColumn";
            this.PDF_OneColumn_buttonEx.UseVisualStyleBackColor = false;
            this.PDF_OneColumn_buttonEx.Visible = false;
            // 
            // PDF_TwoColumnLeft_buttonEx
            // 
            this.PDF_TwoColumnLeft_buttonEx.EditBox = null;
            this.PDF_TwoColumnLeft_buttonEx.FlatAppearance.BorderSize = 0;
            this.PDF_TwoColumnLeft_buttonEx.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PDF_TwoColumnLeft_buttonEx.IsActive = false;
            this.PDF_TwoColumnLeft_buttonEx.LedSyncedBackColorEnable = true;
            this.PDF_TwoColumnLeft_buttonEx.Location = new System.Drawing.Point(723, 457);
            this.PDF_TwoColumnLeft_buttonEx.MultiSelectEn = false;
            this.PDF_TwoColumnLeft_buttonEx.Name = "PDF_TwoColumnLeft_buttonEx";
            this.PDF_TwoColumnLeft_buttonEx.OutLineColor = System.Drawing.Color.Empty;
            this.PDF_TwoColumnLeft_buttonEx.OutLineEn = true;
            this.PDF_TwoColumnLeft_buttonEx.OutLineSize = 3;
            this.PDF_TwoColumnLeft_buttonEx.Selectable = true;
            this.PDF_TwoColumnLeft_buttonEx.Selected = false;
            this.PDF_TwoColumnLeft_buttonEx.Size = new System.Drawing.Size(139, 59);
            this.PDF_TwoColumnLeft_buttonEx.StatusLedEnable = false;
            this.PDF_TwoColumnLeft_buttonEx.StatusLedSize = ((byte)(15));
            this.PDF_TwoColumnLeft_buttonEx.TabIndex = 239;
            this.PDF_TwoColumnLeft_buttonEx.Text = "TwoColumnLeft";
            this.PDF_TwoColumnLeft_buttonEx.UseVisualStyleBackColor = false;
            this.PDF_TwoColumnLeft_buttonEx.Visible = false;
            // 
            // PDF_TwoColumnRight_buttonEx
            // 
            this.PDF_TwoColumnRight_buttonEx.EditBox = null;
            this.PDF_TwoColumnRight_buttonEx.FlatAppearance.BorderSize = 0;
            this.PDF_TwoColumnRight_buttonEx.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PDF_TwoColumnRight_buttonEx.IsActive = false;
            this.PDF_TwoColumnRight_buttonEx.LedSyncedBackColorEnable = true;
            this.PDF_TwoColumnRight_buttonEx.Location = new System.Drawing.Point(723, 522);
            this.PDF_TwoColumnRight_buttonEx.MultiSelectEn = false;
            this.PDF_TwoColumnRight_buttonEx.Name = "PDF_TwoColumnRight_buttonEx";
            this.PDF_TwoColumnRight_buttonEx.OutLineColor = System.Drawing.Color.Empty;
            this.PDF_TwoColumnRight_buttonEx.OutLineEn = true;
            this.PDF_TwoColumnRight_buttonEx.OutLineSize = 3;
            this.PDF_TwoColumnRight_buttonEx.Selectable = true;
            this.PDF_TwoColumnRight_buttonEx.Selected = false;
            this.PDF_TwoColumnRight_buttonEx.Size = new System.Drawing.Size(139, 59);
            this.PDF_TwoColumnRight_buttonEx.StatusLedEnable = false;
            this.PDF_TwoColumnRight_buttonEx.StatusLedSize = ((byte)(15));
            this.PDF_TwoColumnRight_buttonEx.TabIndex = 240;
            this.PDF_TwoColumnRight_buttonEx.Text = "TwoColumnRight";
            this.PDF_TwoColumnRight_buttonEx.UseVisualStyleBackColor = false;
            this.PDF_TwoColumnRight_buttonEx.Visible = false;
            // 
            // PDF_SinglePage_buttonEx
            // 
            this.PDF_SinglePage_buttonEx.EditBox = null;
            this.PDF_SinglePage_buttonEx.FlatAppearance.BorderSize = 0;
            this.PDF_SinglePage_buttonEx.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PDF_SinglePage_buttonEx.IsActive = false;
            this.PDF_SinglePage_buttonEx.LedSyncedBackColorEnable = true;
            this.PDF_SinglePage_buttonEx.Location = new System.Drawing.Point(723, 375);
            this.PDF_SinglePage_buttonEx.MultiSelectEn = false;
            this.PDF_SinglePage_buttonEx.Name = "PDF_SinglePage_buttonEx";
            this.PDF_SinglePage_buttonEx.OutLineColor = System.Drawing.Color.Empty;
            this.PDF_SinglePage_buttonEx.OutLineEn = true;
            this.PDF_SinglePage_buttonEx.OutLineSize = 3;
            this.PDF_SinglePage_buttonEx.Selectable = true;
            this.PDF_SinglePage_buttonEx.Selected = false;
            this.PDF_SinglePage_buttonEx.Size = new System.Drawing.Size(139, 34);
            this.PDF_SinglePage_buttonEx.StatusLedEnable = false;
            this.PDF_SinglePage_buttonEx.StatusLedSize = ((byte)(15));
            this.PDF_SinglePage_buttonEx.TabIndex = 241;
            this.PDF_SinglePage_buttonEx.Text = "SinglePage";
            this.PDF_SinglePage_buttonEx.UseVisualStyleBackColor = false;
            this.PDF_SinglePage_buttonEx.Visible = false;
            // 
            // PDF_SetNamedDist_buttonEx
            // 
            this.PDF_SetNamedDist_buttonEx.EditBox = null;
            this.PDF_SetNamedDist_buttonEx.FlatAppearance.BorderSize = 0;
            this.PDF_SetNamedDist_buttonEx.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PDF_SetNamedDist_buttonEx.IsActive = false;
            this.PDF_SetNamedDist_buttonEx.LedSyncedBackColorEnable = true;
            this.PDF_SetNamedDist_buttonEx.Location = new System.Drawing.Point(344, 520);
            this.PDF_SetNamedDist_buttonEx.MultiSelectEn = false;
            this.PDF_SetNamedDist_buttonEx.Name = "PDF_SetNamedDist_buttonEx";
            this.PDF_SetNamedDist_buttonEx.OutLineColor = System.Drawing.Color.Empty;
            this.PDF_SetNamedDist_buttonEx.OutLineEn = true;
            this.PDF_SetNamedDist_buttonEx.OutLineSize = 3;
            this.PDF_SetNamedDist_buttonEx.Selectable = true;
            this.PDF_SetNamedDist_buttonEx.Selected = false;
            this.PDF_SetNamedDist_buttonEx.Size = new System.Drawing.Size(77, 59);
            this.PDF_SetNamedDist_buttonEx.StatusLedEnable = false;
            this.PDF_SetNamedDist_buttonEx.StatusLedSize = ((byte)(15));
            this.PDF_SetNamedDist_buttonEx.TabIndex = 242;
            this.PDF_SetNamedDist_buttonEx.Text = "setNamedDist";
            this.PDF_SetNamedDist_buttonEx.UseVisualStyleBackColor = false;
            this.PDF_SetNamedDist_buttonEx.Visible = false;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(62, 251);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 328);
            this.listBox1.TabIndex = 243;
            this.listBox1.Visible = false;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 12;
            this.listBox2.Location = new System.Drawing.Point(218, 251);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(120, 328);
            this.listBox2.TabIndex = 244;
            this.listBox2.Visible = false;
            // 
            // PDF_Find__buttonEx
            // 
            this.PDF_Find__buttonEx.EditBox = null;
            this.PDF_Find__buttonEx.FlatAppearance.BorderSize = 0;
            this.PDF_Find__buttonEx.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PDF_Find__buttonEx.IsActive = false;
            this.PDF_Find__buttonEx.LedSyncedBackColorEnable = true;
            this.PDF_Find__buttonEx.Location = new System.Drawing.Point(394, 609);
            this.PDF_Find__buttonEx.MultiSelectEn = false;
            this.PDF_Find__buttonEx.Name = "PDF_Find__buttonEx";
            this.PDF_Find__buttonEx.OutLineColor = System.Drawing.Color.Empty;
            this.PDF_Find__buttonEx.OutLineEn = true;
            this.PDF_Find__buttonEx.OutLineSize = 3;
            this.PDF_Find__buttonEx.Selectable = true;
            this.PDF_Find__buttonEx.Selected = false;
            this.PDF_Find__buttonEx.Size = new System.Drawing.Size(110, 55);
            this.PDF_Find__buttonEx.StatusLedEnable = false;
            this.PDF_Find__buttonEx.StatusLedSize = ((byte)(15));
            this.PDF_Find__buttonEx.TabIndex = 245;
            this.PDF_Find__buttonEx.Text = "検索";
            this.PDF_Find__buttonEx.UseVisualStyleBackColor = false;
            this.PDF_Find__buttonEx.Visible = false;
            this.PDF_Find__buttonEx.Click += new System.EventHandler(this.PDF_Find_buttonEx_Click);
            // 
            // PDF_Button_DownBotom
            // 
            this.PDF_Button_DownBotom.EditBox = null;
            this.PDF_Button_DownBotom.FlatAppearance.BorderSize = 0;
            this.PDF_Button_DownBotom.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PDF_Button_DownBotom.IsActive = false;
            this.PDF_Button_DownBotom.LedSyncedBackColorEnable = true;
            this.PDF_Button_DownBotom.Location = new System.Drawing.Point(939, 310);
            this.PDF_Button_DownBotom.MultiSelectEn = false;
            this.PDF_Button_DownBotom.Name = "PDF_Button_DownBotom";
            this.PDF_Button_DownBotom.OutLineColor = System.Drawing.Color.Empty;
            this.PDF_Button_DownBotom.OutLineEn = true;
            this.PDF_Button_DownBotom.OutLineSize = 3;
            this.PDF_Button_DownBotom.Selectable = true;
            this.PDF_Button_DownBotom.Selected = false;
            this.PDF_Button_DownBotom.Size = new System.Drawing.Size(79, 40);
            this.PDF_Button_DownBotom.StatusLedEnable = false;
            this.PDF_Button_DownBotom.StatusLedSize = ((byte)(15));
            this.PDF_Button_DownBotom.TabIndex = 225;
            this.PDF_Button_DownBotom.Text = "最後";
            this.PDF_Button_DownBotom.UseVisualStyleBackColor = false;
            this.PDF_Button_DownBotom.Click += new System.EventHandler(this.PDF_Button_DownBotom_Click);
            // 
            // PDF_Button_UpTop
            // 
            this.PDF_Button_UpTop.EditBox = null;
            this.PDF_Button_UpTop.FlatAppearance.BorderSize = 0;
            this.PDF_Button_UpTop.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PDF_Button_UpTop.IsActive = false;
            this.PDF_Button_UpTop.LedSyncedBackColorEnable = true;
            this.PDF_Button_UpTop.Location = new System.Drawing.Point(939, 112);
            this.PDF_Button_UpTop.MultiSelectEn = false;
            this.PDF_Button_UpTop.Name = "PDF_Button_UpTop";
            this.PDF_Button_UpTop.OutLineColor = System.Drawing.Color.Empty;
            this.PDF_Button_UpTop.OutLineEn = true;
            this.PDF_Button_UpTop.OutLineSize = 3;
            this.PDF_Button_UpTop.Selectable = true;
            this.PDF_Button_UpTop.Selected = false;
            this.PDF_Button_UpTop.Size = new System.Drawing.Size(79, 40);
            this.PDF_Button_UpTop.StatusLedEnable = false;
            this.PDF_Button_UpTop.StatusLedSize = ((byte)(15));
            this.PDF_Button_UpTop.TabIndex = 224;
            this.PDF_Button_UpTop.Text = "最初";
            this.PDF_Button_UpTop.UseVisualStyleBackColor = false;
            this.PDF_Button_UpTop.Click += new System.EventHandler(this.PDF_Button_UpTop_Click);
            // 
            // PDF_Button_Down
            // 
            this.PDF_Button_Down.EditBox = null;
            this.PDF_Button_Down.FlatAppearance.BorderSize = 0;
            this.PDF_Button_Down.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PDF_Button_Down.IsActive = false;
            this.PDF_Button_Down.LedSyncedBackColorEnable = true;
            this.PDF_Button_Down.Location = new System.Drawing.Point(939, 260);
            this.PDF_Button_Down.MultiSelectEn = false;
            this.PDF_Button_Down.Name = "PDF_Button_Down";
            this.PDF_Button_Down.OutLineColor = System.Drawing.Color.Empty;
            this.PDF_Button_Down.OutLineEn = true;
            this.PDF_Button_Down.OutLineSize = 3;
            this.PDF_Button_Down.Selectable = true;
            this.PDF_Button_Down.Selected = false;
            this.PDF_Button_Down.Size = new System.Drawing.Size(79, 40);
            this.PDF_Button_Down.StatusLedEnable = false;
            this.PDF_Button_Down.StatusLedSize = ((byte)(15));
            this.PDF_Button_Down.TabIndex = 222;
            this.PDF_Button_Down.Text = "▼";
            this.PDF_Button_Down.UseVisualStyleBackColor = false;
            this.PDF_Button_Down.Click += new System.EventHandler(this.PDF_Button_Down_Click);
            // 
            // PDF_Button_Up
            // 
            this.PDF_Button_Up.EditBox = null;
            this.PDF_Button_Up.FlatAppearance.BorderSize = 0;
            this.PDF_Button_Up.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PDF_Button_Up.IsActive = false;
            this.PDF_Button_Up.LedSyncedBackColorEnable = true;
            this.PDF_Button_Up.Location = new System.Drawing.Point(939, 162);
            this.PDF_Button_Up.MultiSelectEn = false;
            this.PDF_Button_Up.Name = "PDF_Button_Up";
            this.PDF_Button_Up.OutLineColor = System.Drawing.Color.Empty;
            this.PDF_Button_Up.OutLineEn = true;
            this.PDF_Button_Up.OutLineSize = 3;
            this.PDF_Button_Up.Selectable = true;
            this.PDF_Button_Up.Selected = false;
            this.PDF_Button_Up.Size = new System.Drawing.Size(79, 40);
            this.PDF_Button_Up.StatusLedEnable = false;
            this.PDF_Button_Up.StatusLedSize = ((byte)(15));
            this.PDF_Button_Up.TabIndex = 221;
            this.PDF_Button_Up.Text = "▲";
            this.PDF_Button_Up.UseVisualStyleBackColor = false;
            this.PDF_Button_Up.Click += new System.EventHandler(this.PDF_Button_Up_Click);
            // 
            // PDF_TextBoxEx
            // 
            this.PDF_TextBoxEx.Font = new System.Drawing.Font("MS UI Gothic", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PDF_TextBoxEx.Location = new System.Drawing.Point(939, 212);
            this.PDF_TextBoxEx.Name = "PDF_TextBoxEx";
            this.PDF_TextBoxEx.Size = new System.Drawing.Size(79, 42);
            this.PDF_TextBoxEx.TabIndex = 223;
            this.PDF_TextBoxEx.Text = "1";
            this.PDF_TextBoxEx.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PDF_TextBoxEx.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PDF_TextBoxEx_MouseClick);
            this.PDF_TextBoxEx.TextChanged += new System.EventHandler(this.PDF_TextBoxEx_TextChanged);
            // 
            // labelEx_Page
            // 
            this.labelEx_Page.AutoSize = true;
            this.labelEx_Page.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelEx_Page.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelEx_Page.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelEx_Page.Location = new System.Drawing.Point(937, 91);
            this.labelEx_Page.Name = "labelEx_Page";
            this.labelEx_Page.Size = new System.Drawing.Size(49, 18);
            this.labelEx_Page.TabIndex = 0;
            this.labelEx_Page.Text = "ページ";
            // 
            // pdfViewer1
            // 
            this.pdfViewer1.Location = new System.Drawing.Point(12, 40);
            this.pdfViewer1.Name = "pdfViewer1";
            this.pdfViewer1.Size = new System.Drawing.Size(919, 555);
            this.pdfViewer1.TabIndex = 246;
            this.pdfViewer1.Load += new System.EventHandler(this.pdfViewer1_Load);
            // 
            // textBoxEx_Find
            // 
            this.textBoxEx_Find.Font = new System.Drawing.Font("MS UI Gothic", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxEx_Find.Location = new System.Drawing.Point(724, 49);
            this.textBoxEx_Find.Name = "textBoxEx_Find";
            this.textBoxEx_Find.Size = new System.Drawing.Size(154, 34);
            this.textBoxEx_Find.TabIndex = 247;
            // 
            // HelpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 678);
            this.Controls.Add(this.textBoxEx_Find);
            this.Controls.Add(this.pdfViewer1);
            this.Controls.Add(this.labelEx_Mag);
            this.Controls.Add(this.PDF_ZoomTextBoxEx);
            this.Controls.Add(this.labelEx_Page);
            this.Controls.Add(this.PDF_50P_buttonEx);
            this.Controls.Add(this.PDF_Find__buttonEx);
            this.Controls.Add(this.PDF_100P_buttonEx);
            this.Controls.Add(this.PDF_TextBoxEx);
            this.Controls.Add(this.PDF_150P_buttonEx);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.PDF_Button_Up);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.PDF_Button_Down);
            this.Controls.Add(this.PDF_SetNamedDist_buttonEx);
            this.Controls.Add(this.PDF_Button_UpTop);
            this.Controls.Add(this.PDF_Button_DownBotom);
            this.Controls.Add(this.PDF_SinglePage_buttonEx);
            this.Controls.Add(this.PDF_TwoColumnRight_buttonEx);
            this.Controls.Add(this.PDF_TwoColumnLeft_buttonEx);
            this.Controls.Add(this.PDF_OneColumn_buttonEx);
            this.Controls.Add(this.PDF_DontCare_buttonEx);
            this.Controls.Add(this.PDF_PanelMode_ButtonEx_BM);
            this.Controls.Add(this.PDF_PanelMode_ButtonEx_Thum);
            this.Controls.Add(this.PDF_PanelMode_ButtonEx_None);
            this.Controls.Add(this.PDF_Button_Ver);
            this.Controls.Add(this.PDF_TrackBar);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label8);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(0, 90);
            this.Name = "HelpForm";
            this.OutLineSize = 3;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HelpForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.HelpForm_FormClosed);
            this.Load += new System.EventHandler(this.HelpForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PDF_TrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LabelEx label8;
        private PanelEx panel3;
        private PanelEx panel1;
		private System.Windows.Forms.TrackBar PDF_TrackBar;
		private ButtonEx PDF_Button_Ver;
		private ButtonEx PDF_50P_buttonEx;
		private ButtonEx PDF_100P_buttonEx;
		private ButtonEx PDF_150P_buttonEx;
		private TextBoxEx PDF_ZoomTextBoxEx;
		private LabelEx labelEx_Mag;
		private ButtonEx PDF_PanelMode_ButtonEx_None;
		private ButtonEx PDF_PanelMode_ButtonEx_Thum;
		private ButtonEx PDF_PanelMode_ButtonEx_BM;
		private ButtonEx MCode_ButtonEx;
		private ButtonEx GCode_ButtonEx;
		private ButtonEx Back_ButtonEx;
		private ButtonEx PDF_Content_ButtonEx;
		private ButtonEx PDF_DontCare_buttonEx;
		private ButtonEx PDF_OneColumn_buttonEx;
		private ButtonEx PDF_TwoColumnLeft_buttonEx;
		private ButtonEx PDF_TwoColumnRight_buttonEx;
		private ButtonEx PDF_SinglePage_buttonEx;
		private ButtonEx PDF_SetNamedDist_buttonEx;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.ListBox listBox2;
        private ButtonEx PDF_Find__buttonEx;
        private ButtonEx PDF_Button_DownBotom;
        private ButtonEx PDF_Button_UpTop;
        private ButtonEx PDF_Button_Down;
        private ButtonEx PDF_Button_Up;
        private TextBoxEx PDF_TextBoxEx;
        private LabelEx labelEx_Page;
        private PdfiumViewer.PdfViewer pdfViewer1;
        private TextBoxEx textBoxEx_Find;
    }
}