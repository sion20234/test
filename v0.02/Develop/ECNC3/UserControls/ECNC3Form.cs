using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ECNC3.Models;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using ECNC3.Views.UserControls;

namespace ECNC3.Views
{
    public class ECNC3Form : Form
    {
		Pen pen = null;
		int r;
		int b;
        public ButtonEx targetBtn = null;
        /// <summary>
        /// ツール色表示フラグ
        /// </summary>
        [Browsable(true)]
        [Description("ツール色有効")]
        [Category("表示")]
        public bool ToolColorEnable = false;
        /// <summary>
        /// フォーカス制御
        /// </summary>
        [Browsable(true)]
        [Description("フォーカス制御有効")]
        [Category("動作")]
        public bool Selectable
        {
            get { return GetStyle(ControlStyles.Selectable); }
            set { SetStyle(ControlStyles.Selectable, value); }
        }
        /// <summary>
        /// Invoke処理管理用変数
        /// </summary>
        public IAsyncResult retInvoke = null;
        public ECNC3Form()
        {
            InitializeComponent();
            this.Load += ECNC3Form_Load;
            this.Activated += ECNC3Form_Activated;
            this.Deactivate += ECNC3Form_Deactivated;
            this.FormClosing += ECNC3Form_FormClosing;
        }
        private void ECNC3Form_Load(object sender, EventArgs e)
        {
            SelectFormInit();
        }
        /// <summary>
        /// Activatedフラグ
        /// </summary>
        private bool _IsActivated = false;
        /// <summary>
        /// Activatedフラグのアクセサー
        /// </summary>
        public bool IsActivated
        {
            get
            {
                return _IsActivated;
            }
            set
            {
                _IsActivated = value;
            }
        }
        private void ECNC3Form_Activated(object sender, EventArgs e)
        {
            if (_IsActivated == false) _IsActivated = true;
        }
        private void ECNC3Form_Deactivated(object sender, EventArgs e)
        {
            if (_IsActivated == true) _IsActivated = false;
        }
        /// <summary>
        /// ECNC3Form表示時、コントロールテキスト取得
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ECNC3Form_Shown(object sender, EventArgs e)
        {
            if (targetBtn == null) return;
            if(Owner != null)
            {
                targetBtn.SetSelected(true);
            }
        }
        private void ECNC3Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            int retryCount = 0;
            if (retInvoke != null)
                while (!retInvoke.IsCompleted)
                {
                    if (retryCount > 10)
                    {
                        Thread.Sleep(100);
                        break;
                    }
                    retryCount++;
                }
            if(this.HasChildren == true)
            {
                foreach (Control ctrl in this.Controls)
                {
                    if(ctrl.GetType() == typeof(ButtonEx))
                    {
                        (ctrl as ButtonEx).Dispose();
                    }
                    else if(ctrl.GetType() == typeof(AxisMonitor))
                    {
                        (ctrl as AxisMonitor).Dispose();
                    }

                }
            }
            e.Cancel = false;
        }
        private void _ParentButton_OutLine(bool enable, Control.ControlCollection ctrls)
        {
            foreach (Control ctrl in ctrls)
            {
                if (ctrl.GetType() == typeof(ButtonEx))
                {
                    if ((ctrl as ButtonEx) == targetBtn)
                    {
                        targetBtn.SetSelected(true, OutLineColor);
                    }
                }
                if (ctrl.Controls.Count != 0)
                {
                    _ParentButton_OutLine(enable, ctrl.Controls);
                }
            }
        }
        /// <summary>
        /// ストリングテーブル作成
        /// </summary>
        /// <param name="sender"></param>
        private void CreateECNC3StringTable(object sender)
        {
            string temp = ((Form)sender).GetType().Name;
            switch (temp)
            {
                case "AECForm": ECNC3Form_GetControlText(ref FileUIStringTable.AECForm); break;
                case "AECFuncForm": ECNC3Form_GetControlText(ref FileUIStringTable.AECFuncForm); break;
                case "AlarmLogForm": ECNC3Form_GetControlText(ref FileUIStringTable.AlarmLogForm); break;
                case "AxisClearForm": ECNC3Form_GetControlText(ref FileUIStringTable.AxisClearForm); break;
                case "ConditionsCallSetForm": ECNC3Form_GetControlText(ref FileUIStringTable.ConditionsCallSetForm); break;
                case "ConditionsForm": ECNC3Form_GetControlText(ref FileUIStringTable.ConditionsForm); break;
                case "EDITForm": ECNC3Form_GetControlText(ref FileUIStringTable.EDITForm); break;
                case "ESFMainForm": ECNC3Form_GetControlText(ref FileUIStringTable.ESFMainForm); break;
                case "FileForm": ECNC3Form_GetControlText(ref FileUIStringTable.FileForm); break;
                case "FuncPassForm": ECNC3Form_GetControlText(ref FileUIStringTable.FuncPassForm); break;
                case "GSFMainForm": ECNC3Form_GetControlText(ref FileUIStringTable.GSFMainForm); break;
                case "HelpForm": ECNC3Form_GetControlText(ref FileUIStringTable.HelpForm); break;
                case "IOCheckForm": ECNC3Form_GetControlText(ref FileUIStringTable.IOCheckForm); break;
                case "LogForm": ECNC3Form_GetControlText(ref FileUIStringTable.LogForm); break;
                case "LogSettingForm": ECNC3Form_GetControlText(ref FileUIStringTable.LogSettingForm); break;
                case "LogSettingVariableForm": ECNC3Form_GetControlText(ref FileUIStringTable.LogSettingVariableForm); break;
                case "MacroVarSetForm": ECNC3Form_GetControlText(ref FileUIStringTable.MacroVarSetForm); break;
                case "MAINForm": ECNC3Form_GetControlText(ref FileUIStringTable.MAINForm); break;
                case "MaintenanceEditForm": ECNC3Form_GetControlText(ref FileUIStringTable.MaintenanceEditForm); break;
                case "MaintenanceForm": ECNC3Form_GetControlText(ref FileUIStringTable.MaintenanceForm); break;
                case "MakerServiceForm": ECNC3Form_GetControlText(ref FileUIStringTable.MakerServiceForm); break;
                case "MANUALForm": ECNC3Form_GetControlText(ref FileUIStringTable.MANUALForm); break;
                case "MaterialNameForm": ECNC3Form_GetControlText(ref FileUIStringTable.MaterialNameForm); break;
                case "MDIAUTOForm": ECNC3Form_GetControlText(ref FileUIStringTable.MDIAUTOForm); break;
                case "NumericFeedForm": ECNC3Form_GetControlText(ref FileUIStringTable.NumericFeedForm); break;
                case "OptionForm": ECNC3Form_GetControlText(ref FileUIStringTable.OptionForm); break;
                case "ParameterViewForm": ECNC3Form_GetControlText(ref FileUIStringTable.ParameterViewForm); break;
                case "PartitionForm": ECNC3Form_GetControlText(ref FileUIStringTable.PartitionForm); break;
                case "PasswordChangeForm": ECNC3Form_GetControlText(ref FileUIStringTable.PasswordChangeForm); break;
                case "PitchCompensationForm": ECNC3Form_GetControlText(ref FileUIStringTable.PitchCompensationForm); break;
                case "PitchSettingForm": ECNC3Form_GetControlText(ref FileUIStringTable.PitchSettingForm); break;
                case "PlotForm": ECNC3Form_GetControlText(ref FileUIStringTable.PlotForm); break;
                case "ReferencingForm": ECNC3Form_GetControlText(ref FileUIStringTable.ReferencingForm); break;
                case "ReturnToOriginForm": ECNC3Form_GetControlText(ref FileUIStringTable.ReturnToOriginForm); break;
                case "SettingSoft": ECNC3Form_GetControlText(ref FileUIStringTable.SettingSoft); break;
                case "SystemFileInputForm": ECNC3Form_GetControlText(ref FileUIStringTable.SystemFileInputForm); break;
                case "SystemTimeAdjustForm": ECNC3Form_GetControlText(ref FileUIStringTable.SystemTimeAdjustForm); break;
                case "TeachTableForm": ECNC3Form_GetControlText(ref FileUIStringTable.TeachTableForm); break;
                case "ThinLineSettingForm": ECNC3Form_GetControlText(ref FileUIStringTable.ThinLineSettingForm); break;
                case "UserFuncForm": ECNC3Form_GetControlText(ref FileUIStringTable.UserFuncForm); break;
                case "UserServiceForm": ECNC3Form_GetControlText(ref FileUIStringTable.UserServiceForm); break;
                case "ValiableFileForm": ECNC3Form_GetControlText(ref FileUIStringTable.ValiableFileForm); break;
                case "ValiableListForm": ECNC3Form_GetControlText(ref FileUIStringTable.ValiableListForm); break;
                case "VersionCHeckForm": ECNC3Form_GetControlText(ref FileUIStringTable.VersionCHeckForm); break;
                case "WorkSpinOpForm": ECNC3Form_GetControlText(ref FileUIStringTable.WorkSpinOpForm); break;
            }
            using (FileUIStringTable strTbl = new FileUIStringTable())
            {
                strTbl.Write();
            }
        }
        /// <summary>
        /// ストリングテーブル適用
        /// </summary>
        /// <param name="sender"></param>
        private void ActivateECNC3StringTable(object sender)
        {
            string temp = ((Form)sender).GetType().Name;
            switch (temp)
            {
                case "AECForm": UpdateFormControlText(FileUIStringTable.AECForm); break;
                case "AECFuncForm": UpdateFormControlText(FileUIStringTable.AECFuncForm); break;
                case "AlarmLogForm": UpdateFormControlText(FileUIStringTable.AlarmLogForm); break;
                case "AxisClearForm": UpdateFormControlText(FileUIStringTable.AxisClearForm); break;
                case "ConditionsCallSetForm": UpdateFormControlText(FileUIStringTable.ConditionsCallSetForm); break;
                case "ConditionsForm": UpdateFormControlText(FileUIStringTable.ConditionsForm); break;
                case "EDITForm": UpdateFormControlText(FileUIStringTable.EDITForm); break;
                case "ESFMainForm": UpdateFormControlText(FileUIStringTable.ESFMainForm); break;
                case "FileForm": UpdateFormControlText(FileUIStringTable.FileForm); break;
                case "FuncPassForm": UpdateFormControlText(FileUIStringTable.FuncPassForm); break;
                case "GSFMainForm": UpdateFormControlText(FileUIStringTable.GSFMainForm); break;
                case "HelpForm": UpdateFormControlText(FileUIStringTable.HelpForm); break;
                case "IOCheckForm": UpdateFormControlText(FileUIStringTable.IOCheckForm); break;
                case "LogForm": UpdateFormControlText(FileUIStringTable.LogForm); break;
                case "LogSettingForm": UpdateFormControlText(FileUIStringTable.LogSettingForm); break;
                case "LogSettingVariableForm": UpdateFormControlText(FileUIStringTable.LogSettingVariableForm); break;
                case "MacroVarSetForm": UpdateFormControlText(FileUIStringTable.MacroVarSetForm); break;
                case "MAINForm": UpdateFormControlText(FileUIStringTable.MAINForm); break;
                case "MaintenanceEditForm": UpdateFormControlText(FileUIStringTable.MaintenanceEditForm); break;
                case "MaintenanceForm": UpdateFormControlText(FileUIStringTable.MaintenanceForm); break;
                case "MakerServiceForm": UpdateFormControlText(FileUIStringTable.MakerServiceForm); break;
                case "MANUALForm": UpdateFormControlText(FileUIStringTable.MANUALForm); break;
                case "MaterialNameForm": UpdateFormControlText(FileUIStringTable.MaterialNameForm); break;
                case "MDIAUTOForm": UpdateFormControlText(FileUIStringTable.MDIAUTOForm); break;
                case "NumericFeedForm": UpdateFormControlText(FileUIStringTable.NumericFeedForm); break;
                case "OptionForm": UpdateFormControlText(FileUIStringTable.OptionForm); break;
                case "ParameterViewForm": UpdateFormControlText(FileUIStringTable.ParameterViewForm); break;
                case "PartitionForm": UpdateFormControlText(FileUIStringTable.PartitionForm); break;
                case "PasswordChangeForm": UpdateFormControlText(FileUIStringTable.PasswordChangeForm); break;
                case "PitchCompensationForm": UpdateFormControlText(FileUIStringTable.PitchCompensationForm); break;
                case "PitchSettingForm": UpdateFormControlText(FileUIStringTable.PitchSettingForm); break;
                case "PlotForm": UpdateFormControlText(FileUIStringTable.PlotForm); break;
                case "ReferencingForm": UpdateFormControlText(FileUIStringTable.ReferencingForm); break;
                case "ReturnToOriginForm": UpdateFormControlText(FileUIStringTable.ReturnToOriginForm); break;
                case "SettingSoft": UpdateFormControlText(FileUIStringTable.SettingSoft); break;
                case "SystemFileInputForm": UpdateFormControlText(FileUIStringTable.SystemFileInputForm); break;
                case "SystemTimeAdjustForm": UpdateFormControlText(FileUIStringTable.SystemTimeAdjustForm); break;
                case "TeachTableForm": UpdateFormControlText(FileUIStringTable.TeachTableForm); break;
                case "ThinLineSettingForm": UpdateFormControlText(FileUIStringTable.ThinLineSettingForm); break;
                case "UserFuncForm": UpdateFormControlText(FileUIStringTable.UserFuncForm); break;
                case "UserServiceForm": UpdateFormControlText(FileUIStringTable.UserServiceForm); break;
                case "ValiableFileForm": UpdateFormControlText(FileUIStringTable.ValiableFileForm); break;
                case "ValiableListForm": UpdateFormControlText(FileUIStringTable.ValiableListForm); break;
                case "VersionCHeckForm": UpdateFormControlText(FileUIStringTable.VersionCHeckForm); break;
                case "WorkSpinOpForm": UpdateFormControlText(FileUIStringTable.WorkSpinOpForm); break;
            }
        }
        private void UpdateFormControlText(StructureStringList list)
        {
            foreach (StructureStringItem item in list)
            {
                for (int ctrlCt = 0; ctrlCt < Controls.Count; ctrlCt++)
                {
                    if (Controls[ctrlCt].Controls.Count != 0)
                    {
                        for (int subCtrlCt = 0; subCtrlCt < Controls[ctrlCt].Controls.Count; subCtrlCt++)
                        {
                            if (Controls[ctrlCt].Controls[subCtrlCt].Name != item.Name) continue;
                            Controls[ctrlCt].Controls[subCtrlCt].Name = item.Name;
                            Controls[ctrlCt].Controls[subCtrlCt].Text = item.Value;
                        }
                    }
                    if (Controls[ctrlCt].Name != item.Name) continue;
                    Controls[ctrlCt].Name = item.Name;
                    Controls[ctrlCt].Text = item.Value;
                }
            }
        }

        /// <summary>
        /// ボタンをツール色表示する場合の初期化処理
        /// </summary>
        /// <param name="source"></param>
        public void ButtonsToolInit(Control.ControlCollection source)
        {
            if (source == null) return;
            foreach (Control ctrl in source)
            {
                if (ctrl.GetType() == typeof(ButtonEx)) (ctrl as ButtonEx).ToolModeInit(true);
                if (ctrl.HasChildren == true && (ctrl.GetType().BaseType != typeof(UserControlEx))) ButtonsToolInit(ctrl.Controls);
            }
        }
        private void ECNC3Form_GetControlText(ref StructureStringList list)
        {
            foreach (Control ctrl in Controls)
            {
                if (ctrl.Controls.Count != 0)
                {
                    foreach (Control subctrl in ctrl.Controls)
                    {
                        if (subctrl.Text == "") continue;
                        StructureStringItem subItem = new StructureStringItem
                        {
                            Name = subctrl.Name,
                            Value = subctrl.Text
                        };
                        list.Add(subItem);
                    }
                    continue;
                }
                if (ctrl.Text == "") continue;
                StructureStringItem item = new StructureStringItem
                {
                    Name = ctrl.Name,
                    Value = ctrl.Text
                };
                list.Add(item);
            }
        }
        private void ECNC3Form_Closing(object sender, FormClosingEventArgs e)
        {
            if (targetBtn == null) return;
            if (Owner != null) targetBtn.SetSelected(false);
            //this.Shown -= ECNC3Form_Shown;
            //this.FormClosing -= ECNC3Form_Closing;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ECNC3Form
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "ECNC3Form";
            this.Shown += new System.EventHandler(this.ECNC3Form_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ECNC3Form_Paint);
            this.ResumeLayout(false);

        }
        /// <summary>
        /// 枠線の色
        /// </summary>
        [Browsable(true)]
        [Description("枠線有効")]
        [Category("表示")]
        public bool OutLineEnable  = false;
        /// <summary>
        /// 枠線の太さ
        /// </summary>
        [Browsable(true)]
        [Description("枠線の太さ")]
        [Category("表示")]
        public int OutLineSize { get; set; }

        /// <summary>
        /// 枠線の色
        /// </summary>
        [Browsable(true)]
        [Description("枠線の色")]
        [Category("表示")]
        public Color OutLineColor { get; set; }
        /// <summary>
        /// 枠線表示（Paintイベントハンドラ）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ECNC3Form_Paint(object sender, PaintEventArgs e)
        {
            if(OutLineEnable == true)
            {
                pen.Color = OutLineColor;
                pen.Width = OutLineSize;
                r = this.ClientRectangle.Right - OutLineSize;
                b = this.ClientRectangle.Bottom - OutLineSize;
                e.Graphics.DrawLine(pen, 0 + OutLineSize, 0 + OutLineSize, r, 0 + OutLineSize); // 上
                e.Graphics.DrawLine(pen, 0 + OutLineSize, 0 + OutLineSize, 0 + OutLineSize, b); // 左
                e.Graphics.DrawLine(pen, r, 0 + OutLineSize, r, b); // 右
                e.Graphics.DrawLine(pen, 0 + OutLineSize, b, r, b); // 下 
            }
        }
        public void SelectFormInit()
        {
            this.OutLineEnable = true;
            this.OutLineSize = 3;
            this.OutLineColor = ToolColorEnable? FileUIStyleTable.ToolLineColor : FileUIStyleTable.SelectedLineColor;
            this.BackColor = ToolColorEnable ? FileUIStyleTable.ToolBackColor : FileUIStyleTable.DefaultBackColor;
            this.ForeColor = ToolColorEnable ? FileUIStyleTable.ToolForeColor : FileUIStyleTable.DefaultForeColor;
            this.FormClosing += ECNC3Form_Closing;
            r = this.ClientRectangle.Right - OutLineSize;
            b = this.ClientRectangle.Bottom - OutLineSize;
            pen = new Pen(this.OutLineColor, OutLineSize);
         
        }
        public void ReShown()
        {
            ECNC3Form_Shown(null, null);
        }
        /// <summary>
        /// メッセージボックス表示
        /// </summary>
        /// <param name="mode">
        /// Error：エラー
        /// Question：はい/いいえ選択
        /// Information：補足情報表示
        /// </param>
        /// <param name="messageID">
        /// OrgPos.xml内メッセージ番号
        /// </param>
        /// <param name="button">
        /// 表示中ボタンスタイル変更設定
        /// </param>
        /// <returns>
        /// Error：OK
        /// Question：YES/NO
        /// Information：OK
        /// </returns>
        protected DialogResult _MessageShow(MessageBoxIcon mode, int messageID, ButtonEx button = null)
        {
            using (MessageDialog dir = (button == null) ? new MessageDialog() : new MessageDialog(button))
            {
                switch (mode)
                {
                    case MessageBoxIcon.Error: dir.Error(messageID, this); return DialogResult.OK;
                    case MessageBoxIcon.Question: return (dir.Question(messageID, this)) ? DialogResult.Yes : DialogResult.No;
                    case MessageBoxIcon.Information: dir.Information(messageID, this); return DialogResult.OK;
                    default: return DialogResult.None;
                }
            }
        }
    }
}
