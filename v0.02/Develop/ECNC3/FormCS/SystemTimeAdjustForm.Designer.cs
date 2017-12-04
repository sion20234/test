namespace ECNC3.Views
{
    partial class SystemTimeAdjustForm
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
            this.components = new System.ComponentModel.Container();
            this.timeTimer = new System.Windows.Forms.Timer(this.components);
            this.panel8 = new ECNC3.Views.PanelEx();
            this.button_Reg = new ECNC3.Views.ButtonEx();
            this.panel2 = new ECNC3.Views.PanelEx();
            this.button_Back = new ECNC3.Views.ButtonEx();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button_Second_Dn = new ECNC3.Views.ButtonEx();
            this.button_Second_Up = new ECNC3.Views.ButtonEx();
            this.textBox_Second = new ECNC3.Views.TextBoxEx();
            this.label_Second = new ECNC3.Views.LabelEx();
            this.button_Minute_Dn = new ECNC3.Views.ButtonEx();
            this.button_Minute_Up = new ECNC3.Views.ButtonEx();
            this.textBox_Minute = new ECNC3.Views.TextBoxEx();
            this.label_Minute = new ECNC3.Views.LabelEx();
            this.button_Hour_Dn = new ECNC3.Views.ButtonEx();
            this.button_Hour_Up = new ECNC3.Views.ButtonEx();
            this.textBox_Hour = new ECNC3.Views.TextBoxEx();
            this.label_Hour = new ECNC3.Views.LabelEx();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_Year_Dn = new ECNC3.Views.ButtonEx();
            this.textBox_Year = new ECNC3.Views.TextBoxEx();
            this.label_Year = new ECNC3.Views.LabelEx();
            this.button_Year_Up = new ECNC3.Views.ButtonEx();
            this.button_Day_Dn = new ECNC3.Views.ButtonEx();
            this.button_Day_Up = new ECNC3.Views.ButtonEx();
            this.textBox_Day = new ECNC3.Views.TextBoxEx();
            this.label_Day = new ECNC3.Views.LabelEx();
            this.button_Month_Dn = new ECNC3.Views.ButtonEx();
            this.button_Month_Up = new ECNC3.Views.ButtonEx();
            this.textBox_Month = new ECNC3.Views.TextBoxEx();
            this.label_Month = new ECNC3.Views.LabelEx();
            this.label8 = new ECNC3.Views.LabelEx();
            this.panel8.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timeTimer
            // 
            this.timeTimer.Interval = 1000;
            this.timeTimer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.button_Reg);
            this.panel8.Font = new System.Drawing.Font("HG丸ｺﾞｼｯｸM-PRO", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.panel8.Location = new System.Drawing.Point(370, 390);
            this.panel8.Name = "panel8";
            this.panel8.OutLineColor = System.Drawing.Color.DarkOliveGreen;
            this.panel8.OutLineSize = 2;
            this.panel8.Size = new System.Drawing.Size(169, 65);
            this.panel8.TabIndex = 2;
            // 
            // button_Reg
            // 
            this.button_Reg.EditBox = null;
            this.button_Reg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Reg.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_Reg.IsActive = false;
            this.button_Reg.Location = new System.Drawing.Point(10, 8);
            this.button_Reg.MultiSelectEn = false;
            this.button_Reg.Name = "button_Reg";
            this.button_Reg.OutLineColor = System.Drawing.Color.Empty;
            this.button_Reg.OutLineEn = true;
            this.button_Reg.OutLineSize = 3;
            this.button_Reg.Selectable = true;
            this.button_Reg.Selected = false;
            this.button_Reg.Size = new System.Drawing.Size(150, 50);
            this.button_Reg.StatusLedEnable = false;
            this.button_Reg.StatusLedSize = ((byte)(15));
            this.button_Reg.TabIndex = 0;
            this.button_Reg.Text = "登録";
            this.button_Reg.UseVisualStyleBackColor = true;
            this.button_Reg.Click += new System.EventHandler(this.button_Reg_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button_Back);
            this.panel2.Font = new System.Drawing.Font("HG丸ｺﾞｼｯｸM-PRO", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.panel2.Location = new System.Drawing.Point(546, 390);
            this.panel2.Name = "panel2";
            this.panel2.OutLineColor = System.Drawing.Color.DarkOliveGreen;
            this.panel2.OutLineSize = 2;
            this.panel2.Size = new System.Drawing.Size(169, 65);
            this.panel2.TabIndex = 3;
            // 
            // button_Back
            // 
            this.button_Back.EditBox = null;
            this.button_Back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Back.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_Back.IsActive = false;
            this.button_Back.Location = new System.Drawing.Point(10, 8);
            this.button_Back.MultiSelectEn = false;
            this.button_Back.Name = "button_Back";
            this.button_Back.OutLineColor = System.Drawing.Color.Empty;
            this.button_Back.OutLineEn = true;
            this.button_Back.OutLineSize = 3;
            this.button_Back.Selectable = true;
            this.button_Back.Selected = false;
            this.button_Back.Size = new System.Drawing.Size(150, 50);
            this.button_Back.StatusLedEnable = false;
            this.button_Back.StatusLedSize = ((byte)(15));
            this.button_Back.TabIndex = 0;
            this.button_Back.Text = "閉じる";
            this.button_Back.UseVisualStyleBackColor = true;
            this.button_Back.Click += new System.EventHandler(this.button_Back_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button_Second_Dn);
            this.groupBox2.Controls.Add(this.button_Second_Up);
            this.groupBox2.Controls.Add(this.textBox_Second);
            this.groupBox2.Controls.Add(this.label_Second);
            this.groupBox2.Controls.Add(this.button_Minute_Dn);
            this.groupBox2.Controls.Add(this.button_Minute_Up);
            this.groupBox2.Controls.Add(this.textBox_Minute);
            this.groupBox2.Controls.Add(this.label_Minute);
            this.groupBox2.Controls.Add(this.button_Hour_Dn);
            this.groupBox2.Controls.Add(this.button_Hour_Up);
            this.groupBox2.Controls.Add(this.textBox_Hour);
            this.groupBox2.Controls.Add(this.label_Hour);
            this.groupBox2.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBox2.Location = new System.Drawing.Point(10, 214);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(705, 160);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "時刻";
            // 
            // button_Second_Dn
            // 
            this.button_Second_Dn.EditBox = null;
            this.button_Second_Dn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Second_Dn.Font = new System.Drawing.Font("Meiryo UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_Second_Dn.IsActive = false;
            this.button_Second_Dn.Location = new System.Drawing.Point(615, 80);
            this.button_Second_Dn.MultiSelectEn = false;
            this.button_Second_Dn.Name = "button_Second_Dn";
            this.button_Second_Dn.OutLineColor = System.Drawing.Color.Empty;
            this.button_Second_Dn.OutLineEn = true;
            this.button_Second_Dn.OutLineSize = 3;
            this.button_Second_Dn.Selectable = true;
            this.button_Second_Dn.Selected = false;
            this.button_Second_Dn.Size = new System.Drawing.Size(60, 50);
            this.button_Second_Dn.StatusLedEnable = false;
            this.button_Second_Dn.StatusLedSize = ((byte)(15));
            this.button_Second_Dn.TabIndex = 11;
            this.button_Second_Dn.Text = "▼";
            this.button_Second_Dn.UseVisualStyleBackColor = true;
            this.button_Second_Dn.Click += new System.EventHandler(this.button_Second_Dn_Click);
            // 
            // button_Second_Up
            // 
            this.button_Second_Up.EditBox = null;
            this.button_Second_Up.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Second_Up.Font = new System.Drawing.Font("Meiryo UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_Second_Up.IsActive = false;
            this.button_Second_Up.Location = new System.Drawing.Point(615, 25);
            this.button_Second_Up.MultiSelectEn = false;
            this.button_Second_Up.Name = "button_Second_Up";
            this.button_Second_Up.OutLineColor = System.Drawing.Color.Empty;
            this.button_Second_Up.OutLineEn = true;
            this.button_Second_Up.OutLineSize = 3;
            this.button_Second_Up.Selectable = true;
            this.button_Second_Up.Selected = false;
            this.button_Second_Up.Size = new System.Drawing.Size(60, 50);
            this.button_Second_Up.StatusLedEnable = false;
            this.button_Second_Up.StatusLedSize = ((byte)(15));
            this.button_Second_Up.TabIndex = 10;
            this.button_Second_Up.Text = "▲";
            this.button_Second_Up.UseVisualStyleBackColor = true;
            this.button_Second_Up.Click += new System.EventHandler(this.button_Second_Up_Click);
            // 
            // textBox_Second
            // 
            this.textBox_Second.Enabled = false;
            this.textBox_Second.Font = new System.Drawing.Font("Meiryo UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_Second.Location = new System.Drawing.Point(474, 61);
            this.textBox_Second.Name = "textBox_Second";
            this.textBox_Second.Size = new System.Drawing.Size(100, 38);
            this.textBox_Second.TabIndex = 8;
            this.textBox_Second.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label_Second
            // 
            this.label_Second.AutoSize = true;
            this.label_Second.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_Second.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label_Second.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Second.Location = new System.Drawing.Point(580, 63);
            this.label_Second.Name = "label_Second";
            this.label_Second.Size = new System.Drawing.Size(31, 26);
            this.label_Second.TabIndex = 9;
            this.label_Second.Text = "秒";
            // 
            // button_Minute_Dn
            // 
            this.button_Minute_Dn.EditBox = null;
            this.button_Minute_Dn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Minute_Dn.Font = new System.Drawing.Font("Meiryo UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_Minute_Dn.IsActive = false;
            this.button_Minute_Dn.Location = new System.Drawing.Point(393, 80);
            this.button_Minute_Dn.MultiSelectEn = false;
            this.button_Minute_Dn.Name = "button_Minute_Dn";
            this.button_Minute_Dn.OutLineColor = System.Drawing.Color.Empty;
            this.button_Minute_Dn.OutLineEn = true;
            this.button_Minute_Dn.OutLineSize = 3;
            this.button_Minute_Dn.Selectable = true;
            this.button_Minute_Dn.Selected = false;
            this.button_Minute_Dn.Size = new System.Drawing.Size(60, 50);
            this.button_Minute_Dn.StatusLedEnable = false;
            this.button_Minute_Dn.StatusLedSize = ((byte)(15));
            this.button_Minute_Dn.TabIndex = 7;
            this.button_Minute_Dn.Text = "▼";
            this.button_Minute_Dn.UseVisualStyleBackColor = true;
            this.button_Minute_Dn.Click += new System.EventHandler(this.button_Minute_Dn_Click);
            // 
            // button_Minute_Up
            // 
            this.button_Minute_Up.EditBox = null;
            this.button_Minute_Up.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Minute_Up.Font = new System.Drawing.Font("Meiryo UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_Minute_Up.IsActive = false;
            this.button_Minute_Up.Location = new System.Drawing.Point(393, 25);
            this.button_Minute_Up.MultiSelectEn = false;
            this.button_Minute_Up.Name = "button_Minute_Up";
            this.button_Minute_Up.OutLineColor = System.Drawing.Color.Empty;
            this.button_Minute_Up.OutLineEn = true;
            this.button_Minute_Up.OutLineSize = 3;
            this.button_Minute_Up.Selectable = true;
            this.button_Minute_Up.Selected = false;
            this.button_Minute_Up.Size = new System.Drawing.Size(60, 50);
            this.button_Minute_Up.StatusLedEnable = false;
            this.button_Minute_Up.StatusLedSize = ((byte)(15));
            this.button_Minute_Up.TabIndex = 6;
            this.button_Minute_Up.Text = "▲";
            this.button_Minute_Up.UseVisualStyleBackColor = true;
            this.button_Minute_Up.Click += new System.EventHandler(this.button_Minute_Up_Click);
            // 
            // textBox_Minute
            // 
            this.textBox_Minute.Enabled = false;
            this.textBox_Minute.Font = new System.Drawing.Font("Meiryo UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_Minute.Location = new System.Drawing.Point(252, 60);
            this.textBox_Minute.Name = "textBox_Minute";
            this.textBox_Minute.Size = new System.Drawing.Size(100, 38);
            this.textBox_Minute.TabIndex = 4;
            this.textBox_Minute.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label_Minute
            // 
            this.label_Minute.AutoSize = true;
            this.label_Minute.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_Minute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label_Minute.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Minute.Location = new System.Drawing.Point(358, 63);
            this.label_Minute.Name = "label_Minute";
            this.label_Minute.Size = new System.Drawing.Size(31, 26);
            this.label_Minute.TabIndex = 5;
            this.label_Minute.Text = "分";
            // 
            // button_Hour_Dn
            // 
            this.button_Hour_Dn.EditBox = null;
            this.button_Hour_Dn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Hour_Dn.Font = new System.Drawing.Font("Meiryo UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_Hour_Dn.IsActive = false;
            this.button_Hour_Dn.Location = new System.Drawing.Point(176, 80);
            this.button_Hour_Dn.MultiSelectEn = false;
            this.button_Hour_Dn.Name = "button_Hour_Dn";
            this.button_Hour_Dn.OutLineColor = System.Drawing.Color.Empty;
            this.button_Hour_Dn.OutLineEn = true;
            this.button_Hour_Dn.OutLineSize = 3;
            this.button_Hour_Dn.Selectable = true;
            this.button_Hour_Dn.Selected = false;
            this.button_Hour_Dn.Size = new System.Drawing.Size(60, 50);
            this.button_Hour_Dn.StatusLedEnable = false;
            this.button_Hour_Dn.StatusLedSize = ((byte)(15));
            this.button_Hour_Dn.TabIndex = 3;
            this.button_Hour_Dn.Text = "▼";
            this.button_Hour_Dn.UseVisualStyleBackColor = true;
            this.button_Hour_Dn.Click += new System.EventHandler(this.button_Hour_Dn_Click);
            // 
            // button_Hour_Up
            // 
            this.button_Hour_Up.EditBox = null;
            this.button_Hour_Up.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Hour_Up.Font = new System.Drawing.Font("Meiryo UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_Hour_Up.IsActive = false;
            this.button_Hour_Up.Location = new System.Drawing.Point(176, 25);
            this.button_Hour_Up.MultiSelectEn = false;
            this.button_Hour_Up.Name = "button_Hour_Up";
            this.button_Hour_Up.OutLineColor = System.Drawing.Color.Empty;
            this.button_Hour_Up.OutLineEn = true;
            this.button_Hour_Up.OutLineSize = 3;
            this.button_Hour_Up.Selectable = true;
            this.button_Hour_Up.Selected = false;
            this.button_Hour_Up.Size = new System.Drawing.Size(60, 50);
            this.button_Hour_Up.StatusLedEnable = false;
            this.button_Hour_Up.StatusLedSize = ((byte)(15));
            this.button_Hour_Up.TabIndex = 2;
            this.button_Hour_Up.Text = "▲";
            this.button_Hour_Up.UseVisualStyleBackColor = true;
            this.button_Hour_Up.Click += new System.EventHandler(this.button_Hour_Up_Click);
            // 
            // textBox_Hour
            // 
            this.textBox_Hour.Enabled = false;
            this.textBox_Hour.Font = new System.Drawing.Font("Meiryo UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_Hour.Location = new System.Drawing.Point(35, 60);
            this.textBox_Hour.Name = "textBox_Hour";
            this.textBox_Hour.Size = new System.Drawing.Size(100, 38);
            this.textBox_Hour.TabIndex = 0;
            this.textBox_Hour.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label_Hour
            // 
            this.label_Hour.AutoSize = true;
            this.label_Hour.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_Hour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label_Hour.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Hour.Location = new System.Drawing.Point(141, 62);
            this.label_Hour.Name = "label_Hour";
            this.label_Hour.Size = new System.Drawing.Size(31, 26);
            this.label_Hour.TabIndex = 1;
            this.label_Hour.Text = "時";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button_Year_Dn);
            this.groupBox1.Controls.Add(this.textBox_Year);
            this.groupBox1.Controls.Add(this.label_Year);
            this.groupBox1.Controls.Add(this.button_Year_Up);
            this.groupBox1.Controls.Add(this.button_Day_Dn);
            this.groupBox1.Controls.Add(this.button_Day_Up);
            this.groupBox1.Controls.Add(this.textBox_Day);
            this.groupBox1.Controls.Add(this.label_Day);
            this.groupBox1.Controls.Add(this.button_Month_Dn);
            this.groupBox1.Controls.Add(this.button_Month_Up);
            this.groupBox1.Controls.Add(this.textBox_Month);
            this.groupBox1.Controls.Add(this.label_Month);
            this.groupBox1.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBox1.Location = new System.Drawing.Point(10, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(705, 160);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "日付";
            // 
            // button_Year_Dn
            // 
            this.button_Year_Dn.EditBox = null;
            this.button_Year_Dn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Year_Dn.Font = new System.Drawing.Font("Meiryo UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_Year_Dn.IsActive = false;
            this.button_Year_Dn.Location = new System.Drawing.Point(615, 80);
            this.button_Year_Dn.MultiSelectEn = false;
            this.button_Year_Dn.Name = "button_Year_Dn";
            this.button_Year_Dn.OutLineColor = System.Drawing.Color.Empty;
            this.button_Year_Dn.OutLineEn = true;
            this.button_Year_Dn.OutLineSize = 3;
            this.button_Year_Dn.Selectable = true;
            this.button_Year_Dn.Selected = false;
            this.button_Year_Dn.Size = new System.Drawing.Size(60, 50);
            this.button_Year_Dn.StatusLedEnable = false;
            this.button_Year_Dn.StatusLedSize = ((byte)(15));
            this.button_Year_Dn.TabIndex = 11;
            this.button_Year_Dn.Text = "▼";
            this.button_Year_Dn.UseVisualStyleBackColor = true;
            this.button_Year_Dn.Click += new System.EventHandler(this.button_Year_Dn_Click);
            // 
            // textBox_Year
            // 
            this.textBox_Year.Enabled = false;
            this.textBox_Year.Font = new System.Drawing.Font("Meiryo UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_Year.Location = new System.Drawing.Point(474, 56);
            this.textBox_Year.Name = "textBox_Year";
            this.textBox_Year.Size = new System.Drawing.Size(100, 38);
            this.textBox_Year.TabIndex = 8;
            this.textBox_Year.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label_Year
            // 
            this.label_Year.AutoSize = true;
            this.label_Year.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_Year.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label_Year.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Year.Location = new System.Drawing.Point(580, 61);
            this.label_Year.Name = "label_Year";
            this.label_Year.Size = new System.Drawing.Size(31, 26);
            this.label_Year.TabIndex = 9;
            this.label_Year.Text = "年";
            // 
            // button_Year_Up
            // 
            this.button_Year_Up.EditBox = null;
            this.button_Year_Up.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Year_Up.Font = new System.Drawing.Font("Meiryo UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_Year_Up.IsActive = false;
            this.button_Year_Up.Location = new System.Drawing.Point(615, 24);
            this.button_Year_Up.MultiSelectEn = false;
            this.button_Year_Up.Name = "button_Year_Up";
            this.button_Year_Up.OutLineColor = System.Drawing.Color.Empty;
            this.button_Year_Up.OutLineEn = true;
            this.button_Year_Up.OutLineSize = 3;
            this.button_Year_Up.Selectable = true;
            this.button_Year_Up.Selected = false;
            this.button_Year_Up.Size = new System.Drawing.Size(60, 50);
            this.button_Year_Up.StatusLedEnable = false;
            this.button_Year_Up.StatusLedSize = ((byte)(15));
            this.button_Year_Up.TabIndex = 10;
            this.button_Year_Up.Text = "▲";
            this.button_Year_Up.UseVisualStyleBackColor = true;
            this.button_Year_Up.Click += new System.EventHandler(this.button_Year_Up_Click);
            // 
            // button_Day_Dn
            // 
            this.button_Day_Dn.EditBox = null;
            this.button_Day_Dn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Day_Dn.Font = new System.Drawing.Font("Meiryo UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_Day_Dn.IsActive = false;
            this.button_Day_Dn.Location = new System.Drawing.Point(393, 80);
            this.button_Day_Dn.MultiSelectEn = false;
            this.button_Day_Dn.Name = "button_Day_Dn";
            this.button_Day_Dn.OutLineColor = System.Drawing.Color.Empty;
            this.button_Day_Dn.OutLineEn = true;
            this.button_Day_Dn.OutLineSize = 3;
            this.button_Day_Dn.Selectable = true;
            this.button_Day_Dn.Selected = false;
            this.button_Day_Dn.Size = new System.Drawing.Size(60, 50);
            this.button_Day_Dn.StatusLedEnable = false;
            this.button_Day_Dn.StatusLedSize = ((byte)(15));
            this.button_Day_Dn.TabIndex = 7;
            this.button_Day_Dn.Text = "▼";
            this.button_Day_Dn.UseVisualStyleBackColor = true;
            this.button_Day_Dn.Click += new System.EventHandler(this.button_Day_Dn_Click);
            // 
            // button_Day_Up
            // 
            this.button_Day_Up.EditBox = null;
            this.button_Day_Up.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Day_Up.Font = new System.Drawing.Font("Meiryo UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_Day_Up.IsActive = false;
            this.button_Day_Up.Location = new System.Drawing.Point(393, 25);
            this.button_Day_Up.MultiSelectEn = false;
            this.button_Day_Up.Name = "button_Day_Up";
            this.button_Day_Up.OutLineColor = System.Drawing.Color.Empty;
            this.button_Day_Up.OutLineEn = true;
            this.button_Day_Up.OutLineSize = 3;
            this.button_Day_Up.Selectable = true;
            this.button_Day_Up.Selected = false;
            this.button_Day_Up.Size = new System.Drawing.Size(60, 50);
            this.button_Day_Up.StatusLedEnable = false;
            this.button_Day_Up.StatusLedSize = ((byte)(15));
            this.button_Day_Up.TabIndex = 6;
            this.button_Day_Up.Text = "▲";
            this.button_Day_Up.UseVisualStyleBackColor = true;
            this.button_Day_Up.Click += new System.EventHandler(this.button_Day_Up_Click);
            // 
            // textBox_Day
            // 
            this.textBox_Day.Enabled = false;
            this.textBox_Day.Font = new System.Drawing.Font("Meiryo UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_Day.Location = new System.Drawing.Point(252, 56);
            this.textBox_Day.Name = "textBox_Day";
            this.textBox_Day.Size = new System.Drawing.Size(100, 38);
            this.textBox_Day.TabIndex = 4;
            this.textBox_Day.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label_Day
            // 
            this.label_Day.AutoSize = true;
            this.label_Day.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_Day.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label_Day.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Day.Location = new System.Drawing.Point(358, 61);
            this.label_Day.Name = "label_Day";
            this.label_Day.Size = new System.Drawing.Size(31, 26);
            this.label_Day.TabIndex = 5;
            this.label_Day.Text = "日";
            // 
            // button_Month_Dn
            // 
            this.button_Month_Dn.EditBox = null;
            this.button_Month_Dn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Month_Dn.Font = new System.Drawing.Font("Meiryo UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_Month_Dn.IsActive = false;
            this.button_Month_Dn.Location = new System.Drawing.Point(176, 80);
            this.button_Month_Dn.MultiSelectEn = false;
            this.button_Month_Dn.Name = "button_Month_Dn";
            this.button_Month_Dn.OutLineColor = System.Drawing.Color.Empty;
            this.button_Month_Dn.OutLineEn = true;
            this.button_Month_Dn.OutLineSize = 3;
            this.button_Month_Dn.Selectable = true;
            this.button_Month_Dn.Selected = false;
            this.button_Month_Dn.Size = new System.Drawing.Size(60, 50);
            this.button_Month_Dn.StatusLedEnable = false;
            this.button_Month_Dn.StatusLedSize = ((byte)(15));
            this.button_Month_Dn.TabIndex = 3;
            this.button_Month_Dn.Text = "▼";
            this.button_Month_Dn.UseVisualStyleBackColor = true;
            this.button_Month_Dn.Click += new System.EventHandler(this.button_Month_Dn_Click);
            // 
            // button_Month_Up
            // 
            this.button_Month_Up.EditBox = null;
            this.button_Month_Up.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Month_Up.Font = new System.Drawing.Font("Meiryo UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_Month_Up.IsActive = false;
            this.button_Month_Up.Location = new System.Drawing.Point(176, 25);
            this.button_Month_Up.MultiSelectEn = false;
            this.button_Month_Up.Name = "button_Month_Up";
            this.button_Month_Up.OutLineColor = System.Drawing.Color.Empty;
            this.button_Month_Up.OutLineEn = true;
            this.button_Month_Up.OutLineSize = 3;
            this.button_Month_Up.Selectable = true;
            this.button_Month_Up.Selected = false;
            this.button_Month_Up.Size = new System.Drawing.Size(60, 50);
            this.button_Month_Up.StatusLedEnable = false;
            this.button_Month_Up.StatusLedSize = ((byte)(15));
            this.button_Month_Up.TabIndex = 2;
            this.button_Month_Up.Text = "▲";
            this.button_Month_Up.UseVisualStyleBackColor = true;
            this.button_Month_Up.Click += new System.EventHandler(this.button_Month_Up_Click);
            // 
            // textBox_Month
            // 
            this.textBox_Month.Enabled = false;
            this.textBox_Month.Font = new System.Drawing.Font("Meiryo UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_Month.Location = new System.Drawing.Point(35, 56);
            this.textBox_Month.Name = "textBox_Month";
            this.textBox_Month.Size = new System.Drawing.Size(100, 38);
            this.textBox_Month.TabIndex = 0;
            this.textBox_Month.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label_Month
            // 
            this.label_Month.AutoSize = true;
            this.label_Month.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_Month.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label_Month.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Month.Location = new System.Drawing.Point(141, 62);
            this.label_Month.Name = "label_Month";
            this.label_Month.Size = new System.Drawing.Size(31, 26);
            this.label_Month.TabIndex = 1;
            this.label_Month.Text = "月";
            // 
            // label8
            // 
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label8.Font = new System.Drawing.Font("Meiryo UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.Location = new System.Drawing.Point(10, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(705, 30);
            this.label8.TabIndex = 86;
            this.label8.Text = "TIME ADJUST";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // SystemTimeAdjustForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 464);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label8);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(0, 90);
            this.Name = "SystemTimeAdjustForm";
            this.OutLineSize = 3;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SystemTimeAdjustForm";
            this.Load += new System.EventHandler(this.SystemTimeAdjustForm_Load);
            this.panel8.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private LabelEx label8;
        private LabelEx label_Month;
        private ButtonEx button_Month_Up;
        private ButtonEx button_Month_Dn;
        private ButtonEx button_Day_Dn;
        private ButtonEx button_Day_Up;
        private LabelEx label_Day;
        private ButtonEx button_Year_Dn;
        private ButtonEx button_Year_Up;
        private LabelEx label_Year;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private ButtonEx button_Second_Dn;
        private ButtonEx button_Second_Up;
        private LabelEx label_Second;
        private ButtonEx button_Minute_Dn;
        private ButtonEx button_Minute_Up;
        private LabelEx label_Minute;
        private ButtonEx button_Hour_Dn;
        private ButtonEx button_Hour_Up;
        private LabelEx label_Hour;
        private PanelEx panel8;
        private ButtonEx button_Reg;
        private PanelEx panel2;
        private ButtonEx button_Back;
		private TextBoxEx textBox_Month;
		private TextBoxEx textBox_Day;
		private TextBoxEx textBox_Year;
		private TextBoxEx textBox_Second;
		private TextBoxEx textBox_Minute;
		private TextBoxEx textBox_Hour;
		private System.Windows.Forms.Timer timeTimer;
	}
}