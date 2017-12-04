using ECNC3.Enumeration;
using ECNC3.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECNC3.Views.Popup
{
    public partial class ListViewDialog : ECNC3Form
    {
        #region Constractor
        public ListViewDialog(string title, ProtectParamCategory mode)
        {
            InitializeComponent();


            _Mode = mode;
            _titleLabel.Text = title;
            //枠線表示
            SelectFormInit();
        }
        public ListViewDialog(string title, ProtectParamCategory mode, ButtonEx parentButton)
        {
            InitializeComponent();


            _Mode = mode;
            _titleLabel.Text = title;
            //枠線表示
            SelectFormInit();
            
            base.targetBtn = parentButton;
        }
        #endregion

        #region ValiableMembers
        public ReturnMessage retMessage = ReturnMessage.Cancel;
        ProtectParamCategory _Mode = ProtectParamCategory.ProcessConditions;
        /// <summary>設定結果取得関数定義</summary>
        public delegate void NotifyReturnDelegate();
        /// <summary>終了通知</summary>
        public NotifyReturnDelegate _notifyReturn = null;
        /// <summary>>設定結果取得</summary>
        public NotifyReturnDelegate NotifyReturn
        {
            set { _notifyReturn = value; }
        }
        #endregion

        #region EventHandler
        private void ListViewDialog_Load(object sender, EventArgs e)
        {
           if(_Initialize() != ResultCodes.Success)
            {
                //初期化失敗
            }
        }
        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <returns>
        /// Success：成功
        /// その他：失敗
        /// </returns>
        private ResultCodes _Initialize()
        {
            ResultCodes retResult = ResultCodes.Success;
            if (retResult == ResultCodes.Success) retResult = _GridViewInit();

            return retResult;
        }
        /// <summary>
        /// グリッドビュー初期化処理
        /// </summary>
        /// <returns>
        /// Success：成功
        /// その他：失敗
        /// </returns>
        private ResultCodes _GridViewInit()
        {
            ResultCodes retResult = ResultCodes.Success;
            this.OutLineEnable = true;
            this.OutLineColor = FileUIStyleTable.SelectedLineColor;
            this.OutLineSize = 3;
            _parameterList.BackgroundColor = FileUIStyleTable.DefaultBackColor;
            //行列の数の指定
            _parameterList.Columns.Add("parameterNumberCol", "");
            _parameterList.InitCol("parameterNumberCol", 16, typeof(string));
            if(_Mode == ProtectParamCategory.TeachingTable)
            {
                //行列の数の指定
                _parameterList.Columns.Add("parameterValueCol", "");
                _parameterList.InitCol("parameterValueCol", 16, typeof(string));
            }
            
            switch (_Mode)
            {
                case ProtectParamCategory.ProcessConditions:
                    _parameterList.RowCount = 1000;
                    break;

                case ProtectParamCategory.VirtualPositions:
                    _parameterList.RowCount = 1000;
                    break;

                case ProtectParamCategory.TeachingTable:
                    _parameterList.RowCount = 100;
                    this.Size = new Size(this.Size.Width + 300, this.Size.Height);
                    _parameterList.Width += 300;
                    break;

            }
            _parameterList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _parameterList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            //_PartitionList.AutoResizeRows();
            foreach (DataGridViewColumn col in _parameterList.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            foreach (DataGridViewRow row in _parameterList.Rows)
            {
                row.Height = 30;
                row.Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            for (int rowCellsCt = 0; rowCellsCt < _parameterList.Rows[2].Cells.Count; rowCellsCt++)
            {
                _parameterList.Rows[2].Cells[rowCellsCt].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            _parameterList.ColumnHeadersHeight = 30;
           
            _TableDataLoad();
            _parameterList.Initialize(20, 30);

            return retResult;
        }
        private ResultCodes _TableDataLoad()
        {
            ResultCodes retResult = ResultCodes.Success;
            switch (_Mode)
            {
                case ProtectParamCategory.ProcessConditions:
                    using (FileProcessCondition filePcond = new FileProcessCondition())
                    {
                        filePcond.Read();
                        for(int pcondCt = 0; pcondCt < 1000; pcondCt++)
                        {
                            _parameterList.Rows[pcondCt].Cells[0].Value = filePcond.Items[pcondCt].Number;
                            if (filePcond.Items[pcondCt].Protect == 0)
                            {
                                _parameterList.Rows[pcondCt].Cells[0].Style.BackColor = FileUIStyleTable.DefaultBackColor;
                                _parameterList.Rows[pcondCt].Cells[0].Style.ForeColor = FileUIStyleTable.DefaultForeColor;
                            }
                            else
                            {
                                _parameterList.Rows[pcondCt].Cells[0].Style.BackColor = FileUIStyleTable.EnabledBackColor;
                                _parameterList.Rows[pcondCt].Cells[0].Style.ForeColor = FileUIStyleTable.EnabledForeColor;
                            }
                        }
                    }
                    break;

                case ProtectParamCategory.VirtualPositions:
                    using (FileOrgPos fileOrgPos = new FileOrgPos())
                    {
                        fileOrgPos.Read();
                        FileSettings fs = new FileSettings();
                        fs.Read();
                        int pcondCtMax = 1000;
                        if(fs.AttrText("Root/Apl", "boot") == "DESKTOP") pcondCtMax = 100;
                        fs.Dispose();
                        fs = null;
                        for (int pcondCt = 0; pcondCt < pcondCtMax; pcondCt++)
                        {
                            _parameterList.Rows[pcondCt].Cells[0].Value = fileOrgPos.Items[pcondCt + 1].Number;
                            if (fileOrgPos.Items[pcondCt + 1].Protect == 0)
                            {
                                _parameterList.Rows[pcondCt].Cells[0].Style.BackColor = FileUIStyleTable.DefaultBackColor;
                                _parameterList.Rows[pcondCt].Cells[0].Style.ForeColor = FileUIStyleTable.DefaultForeColor;
                            }
                            else
                            {
                                _parameterList.Rows[pcondCt].Cells[0].Style.BackColor = FileUIStyleTable.EnabledBackColor;
                                _parameterList.Rows[pcondCt].Cells[0].Style.ForeColor = FileUIStyleTable.EnabledForeColor;
                            }
                        }
                    }
                    break;

                case ProtectParamCategory.TeachingTable:
                    using (FileTeachTable fileTeach = new FileTeachTable())
                    {
                        fileTeach.Read();
                        int teachCtMax = 100;
                        for (int teachCt = 0; teachCt < teachCtMax; teachCt++)
                        {
                            _parameterList.Rows[teachCt].Cells[0].Value = fileTeach.Items[teachCt].Name;
                            _parameterList.Rows[teachCt].Cells[1].Value = fileTeach.Items[teachCt].Value;
                            if (fileTeach.Items[teachCt].Selected == false)
                            {
                                _parameterList.Rows[teachCt].DefaultCellStyle.BackColor = FileUIStyleTable.DefaultBackColor;
                                _parameterList.Rows[teachCt].DefaultCellStyle.ForeColor = FileUIStyleTable.DefaultForeColor;
                            }
                            else
                            {
                                _parameterList.Rows[teachCt].DefaultCellStyle.BackColor = FileUIStyleTable.EnabledBackColor;
                                _parameterList.Rows[teachCt].DefaultCellStyle.ForeColor = FileUIStyleTable.EnabledForeColor;
                            }
                        }
                    }
                    break;

            }


            return retResult;
        }

        private void dataGridViewEx1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            switch(_Mode)
            {
                case ProtectParamCategory.ProcessConditions:
                case ProtectParamCategory.VirtualPositions:
                    if (_parameterList[e.ColumnIndex, e.RowIndex].Value == null)
                    {
                        return;
                    }
                    if (_parameterList[e.ColumnIndex, e.RowIndex].Style.BackColor != FileUIStyleTable.DefaultBackColor)
                    {
                        _parameterList[e.ColumnIndex, e.RowIndex].Style.BackColor = FileUIStyleTable.DefaultBackColor;
                        _parameterList[e.ColumnIndex, e.RowIndex].Style.ForeColor = FileUIStyleTable.DefaultForeColor;
                        _SaveParameter(false, e.RowIndex);
                    }
                    else
                    {
                        _parameterList[e.ColumnIndex, e.RowIndex].Style.BackColor = FileUIStyleTable.EnabledBackColor;
                        _parameterList[e.ColumnIndex, e.RowIndex].Style.ForeColor = FileUIStyleTable.EnabledForeColor;
                        _SaveParameter(true, e.RowIndex);
                    }
                    break;

            }
        }
        private ResultCodes _SaveParameter(bool protectEn, int index = 0)
        {
            ResultCodes retResult = ResultCodes.FailToWriteFile;
            switch (_Mode)
            {
                case ProtectParamCategory.ProcessConditions:
                    using (FileProcessCondition filePcond = new FileProcessCondition())
                    {
                        filePcond.Read();
                        filePcond.Items[index].Protect = (protectEn == false) ? 0 : 1;
                        retResult = filePcond.Write(filePcond.Items[index]);
                    }
                    break;

                case ProtectParamCategory.VirtualPositions:
                    using (FileOrgPos fileOrgPos = new FileOrgPos())
                    {
                        fileOrgPos.Read();
                        fileOrgPos.Items[index + 1].Protect = (protectEn == false) ? 0 : 1;
                        retResult = fileOrgPos.Write();
                    }
                    break;

                case ProtectParamCategory.TeachingTable:
                    using (FileTeachTable fileTeach = new FileTeachTable())
                    {
                        fileTeach.Read();
                        for (int teachCt = 0; teachCt < 100; teachCt++)
                        {
                            if(teachCt == fileTeach.Items[teachCt].Number)
                            {
                                //変数名
                                fileTeach.Items[teachCt].Name = _parameterList.Rows[teachCt].Cells[0].Value.ToString();
                                //値
                                fileTeach.Items[teachCt].Value = _parameterList.Rows[teachCt].Cells[1].Value.ToString();
                                //変数の使用状態
                                fileTeach.Items[teachCt].Selected
                                = (_parameterList.Rows[teachCt].DefaultCellStyle.BackColor == FileUIStyleTable.DefaultBackColor) ? false : true;
                            }
                        }
                        if (retResult == ResultCodes.Success) retResult = fileTeach.Write();
                    }
                    break;
            }


            return retResult;
        }
        private void CancelBt_Click(object sender, EventArgs e)
        {
            DisposeMember();
            _notifyReturn?.Invoke();
        }

        #endregion

        private void DisposeMember()
        {

        }

        private void ListViewDialog_Shown(object sender, EventArgs e)
        {
            _parameterList.ClearSelection();
        }

        static public ReturnMessage ShowSubForm(IWin32Window owner, string title, ProtectParamCategory mode, ButtonEx parentButton = null)
        {
            if(parentButton == null)
            {
                ListViewDialog f = new ListViewDialog(title, mode);
                f.ShowDialog(owner);
                ReturnMessage receiveMessage = f.retMessage;
                f.Dispose();
                return receiveMessage;
            }
            else
            {
                ListViewDialog f = new ListViewDialog(title, mode, parentButton);
                f.ShowDialog(owner);
                ReturnMessage receiveMessage = f.retMessage;
                f.Dispose();
                return receiveMessage;
            }
        }

        private void ListViewDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void _parameterList_SelectionChanged(object sender, EventArgs e)
        {
            if (_parameterList.SelectedCells.Count != 0) _parameterList.ClearSelection();
        }
        #region<ポップアップ・キーボード>
        /// <summary>
        /// キーボード「閉じる」ボタン：押された
        /// </summary>
        private void PopupKeybord_OnNotifyReturn()
        {
     
        }
        #endregion

    }
}
