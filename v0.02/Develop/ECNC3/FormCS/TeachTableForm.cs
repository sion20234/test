using ECNC3.Enumeration;
using ECNC3.Models;
using ECNC3.Views.Popup;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECNC3.Views
{
    public partial class TeachTableForm : ECNC3Form
    {
        #region Constractor
        public TeachTableForm()
        {
            InitializeComponent();
            //枠線表示
            SelectFormInit();
        }
        public TeachTableForm(ButtonEx parentButton)
        {
            InitializeComponent();
            //枠線表示
            SelectFormInit();

            base.targetBtn = parentButton;
        }
        #endregion

        #region ValiableMembers
        /// <summary>
        /// ティーチング設定画面使用コマンド
        /// </summary>
        private enum _TeachSetCommand
        {
            /// <summary>
            /// 削除
            /// </summary>
            Delete = 0,
            /// <summary>
            /// コピー
            /// </summary>
            Copy = 1,
            /// <summary>
            /// 選択
            /// </summary>
            Select = 2
        }
        public ReturnMessage retMessage = ReturnMessage.Cancel;
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
        private void TeachTableForm_Load(object sender, EventArgs e)
        {
            if (_Initialize() != ResultCodes.Success)
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
            this.OutLineEnable = true;
            this.OutLineColor = FileUIStyleTable.SelectedLineColor;
            this.OutLineSize = 3;
            if (retResult == ResultCodes.Success) retResult = _GridViewInit();
            if (retResult == ResultCodes.Success) retResult = _SelectedValueListInit();
            if (retResult == ResultCodes.Success) retResult = _TableDataLoad();

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

            _parameterList.ReadOnly = true;//編集不可：柏原
            _parameterList.BackgroundColor = FileUIStyleTable.DefaultBackColor;
            //行列の数の指定
            _parameterList.Columns.Add("parameterNumberCol", "");
            _parameterList.InitCol("parameterNumberCol", 20, typeof(string));
            //行列の数の指定
            _parameterList.Columns.Add("parameterValueCol", "");
            _parameterList.InitCol("parameterValueCol", 20, typeof(string));
            _parameterList.RowCount = 100;
                
            _parameterList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _parameterList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            _parameterList.ColumnHeadersHeight = 30;
            _parameterList.Initialize(20, 30, false);
            //列の設定
            foreach (DataGridViewColumn col in _parameterList.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                col.ReadOnly = false;
                col.CellTemplate = new DataGridViewTextBoxCell();
            }            
            //行の設定
            foreach (DataGridViewRow row in _parameterList.Rows)
            {
                foreach(DataGridViewCell cell in row.Cells)
                {
                    _parameterList[cell.ColumnIndex, row.Index].ReadOnly = false;
                }
            }
            
            return retResult;
        }
        /// <summary>
        /// グリッドビュー初期化処理
        /// </summary>
        /// <returns>
        /// Success：成功
        /// その他：失敗
        /// </returns>
        private ResultCodes _SelectedValueListInit()
        {
            ResultCodes retResult = ResultCodes.Success;

            _SelectedValueList.BackgroundColor = FileUIStyleTable.DefaultBackColor;
            //行列の数の指定
            _SelectedValueList.Columns.Add("selectedNumberCol", "");
            _SelectedValueList.InitCol("selectedNumberCol", 16, typeof(string));
            //行列の数の指定
            _SelectedValueList.Columns.Add("selectedValueCol", "");
            _SelectedValueList.InitCol("selectedValueCol", 16, typeof(string));

            _SelectedValueList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _SelectedValueList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            foreach (DataGridViewColumn col in _SelectedValueList.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            _SelectedValueList.ColumnHeadersHeight = 30;
            _SelectedValueList.Initialize(20, 30);

            return retResult;
        }
        private ResultCodes _TableDataLoad()
        {
            ResultCodes retResult = ResultCodes.Success;
            using (FileTeachTable fileTeach = new FileTeachTable())
            {
                fileTeach.Read();
                int teachCtMax = 100;
                for (int teachCt = 0; teachCt < teachCtMax; teachCt++)
                {
                    _parameterList.Rows[teachCt].Cells[0].Value = fileTeach.Items[teachCt].Name;
                    _parameterList.Rows[teachCt].Cells[1].Value = fileTeach.Items[teachCt].Value;
                    if (fileTeach.Items[teachCt].Selected == false){ }
                    else
                    {
                        _SelectedValueList.Rows.Add();
                        _SelectedValueList.Rows[_SelectedValueList.Rows.Count - 1].Cells[0].Value = fileTeach.Items[teachCt].Name;
                        _SelectedValueList.Rows[_SelectedValueList.Rows.Count - 1].Cells[1].Value = fileTeach.Items[teachCt].Value;
                    }
                }
            }
            return retResult;
        }
        private void _EditingStart()
        {
            //ポップアップキーボードがあるか？
            if (null != _PopupKeybord)
            {
                this.Controls.Remove(_PopupKeybord);
                _PopupKeybord.Dispose();
                _PopupKeybord = null;
                _parameterList.EndEdit();

            }
            if (_PopupKeybord == null)
            {
                //ポップアップ・キーボード表示
                _PopupKeybord = new StandardKeyBord();
                _PopupKeybord.Location = new System.Drawing.Point(0, 400);
                _PopupKeybord.NotifyReturn = PopupKeybord_OnNotifyReturn; //イベント通知：OK
                _PopupKeybord.VisibleChanged += _PopupKeybord_VisibleChanged;
                this.Controls.Add(_PopupKeybord);
                _parameterList.Focus();
                _parameterList[colIndex, rowIndex].Selected = true;
                _parameterList.BeginEdit(false);
            }
            
        }
        private void _KeybordClose()
        {
            if (null != _PopupKeybord)
            {
                this.Controls.Remove(_PopupKeybord);
                _PopupKeybord.Dispose();
                _PopupKeybord = null;
            }
        }
        int rowIndex = 0;
        int colIndex = 0;
        private void dataGridViewEx1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            rowIndex = e.RowIndex;
            colIndex = e.ColumnIndex;
            _SelectBtn.Text = "⇨";
            _SelectedValueList.ClearSelection();
            _CopyBtn.Enabled = true;
            _PasteBtn.Enabled = true;
            _DeleteBtn.Enabled = true;
            _EditBtn.Enabled = true;
        }
        private void _SelectedValueList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _SelectBtn.Text = "⇦";
            _parameterList.ClearSelection();
            _CopyBtn.Enabled = false;
            _PasteBtn.Enabled = false;
            _DeleteBtn.Enabled = false;
            _EditBtn.Enabled = false;
        }
        private void _PopupKeybord_VisibleChanged(object sender, EventArgs e)
        {
            if (_PopupKeybord.Visible == false)
            {
                if (_EditBtn.GetBack() == true)
                {
                    _EditBtn.SetBack(false);
                }
            }
            else
            {
                if (_EditBtn.GetBack() == false)
                {
                    _EditBtn.SetBack(true);
                }
            }
        }
        private ResultCodes _SaveParameter()
        {
            ResultCodes retResult = ResultCodes.Success;                 
            using (FileTeachTable fileTeach = new FileTeachTable())
            {
                if (fileTeach.Items == null) fileTeach.Items = new StructureTeachTableList();
                for (int teachCt = 0; teachCt < 100; teachCt++)
                {
                    StructureTeachTableItem tempItem = new StructureTeachTableItem();
                    tempItem.Number = teachCt;
                    //変数名
                    if (_parameterList.Rows[teachCt].Cells[0].Value == null)
                    {
                        tempItem.Name = "";
                    }
                    else
                    {
                        tempItem.Name = _parameterList.Rows[teachCt].Cells[0].Value.ToString();
                    }

                    //値
                    if(_parameterList.Rows[teachCt].Cells[0].Value == null)
                    {
                        tempItem.Value = "";
                    }
                    else
                    {
                        tempItem.Value = _parameterList.Rows[teachCt].Cells[1].Value.ToString();
                    }
                    //変数の使用状態
                    bool selected = false;
                    foreach(DataGridViewRow row in _SelectedValueList.Rows)
                    {
                        string temp = "";
                        if(_parameterList.Rows[teachCt].Cells[0].Value != null)
                        {
                            temp = _parameterList.Rows[teachCt].Cells[0].Value.ToString();
                        }
                        if(row.Cells[0].Value.ToString() == temp)
                        {
                            selected = true;
                            break;
                        }
                    }
                    tempItem.Selected = selected;
                    if (retResult == ResultCodes.Success) retResult = fileTeach.Write(tempItem);
                }
             
            }
            return retResult;
        }
        public void Close(bool resist)
        {
            if (resist == true)
            {
                _SaveParameter();
            }
            Close();
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

        private void TeachTableForm_Shown(object sender, EventArgs e)
        {
            if (_parameterList.SelectedCells.Count != 0) _parameterList.ClearSelection();
        }

        static public ReturnMessage ShowSubForm(IWin32Window owner, ButtonEx parentButton = null)
        {
            if (parentButton == null)
            {
                TeachTableForm f = new TeachTableForm();
                f.ShowDialog(owner);
                ReturnMessage receiveMessage = f.retMessage;
                f.Dispose();
                return receiveMessage;
            }
            else
            {
                TeachTableForm f = new TeachTableForm(parentButton);
                f.ShowDialog(owner);
                ReturnMessage receiveMessage = f.retMessage;
                f.Dispose();
                return receiveMessage;
            }
        }

        private void TeachTableForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void _parameterList_SelectionChanged(object sender, EventArgs e)
        {
        }
        #region<ポップアップ・キーボード>
        /// ポップアップ・キーボード
        private StandardKeyBord _PopupKeybord = null;
        /// <summary>
        /// キーボード「閉じる」ボタン：押された
        /// </summary>
        private void PopupKeybord_OnNotifyReturn()
        {
            
        }
        #endregion
        private string _ClipNumber = "";
        private string _ClipValue = "";
        private void _SelectBtn_Click(object sender, EventArgs e)
        {
            if (_parameterList.SelectedCells.Count != 0 && _SelectedValueList.SelectedCells.Count == 0)
            {
                bool returnFlag = false;
                foreach(DataGridViewRow row in _SelectedValueList.Rows)
                {
                    if (row.Cells[0].Value.ToString() == _parameterList.Rows[_parameterList.SelectedCells[0].RowIndex].Cells[0].Value.ToString()) returnFlag = true;
                }
                if (returnFlag == true) return;
                _SelectedValueList.Rows.Add();
                _SelectedValueList.Rows[_SelectedValueList.Rows.Count - 1].Cells[0].Value = _parameterList.Rows[_parameterList.SelectedCells[0].RowIndex].Cells[0].Value.ToString();
                _SelectedValueList.Rows[_SelectedValueList.Rows.Count - 1].Cells[1].Value = _parameterList.Rows[_parameterList.SelectedCells[0].RowIndex].Cells[1].Value.ToString();
            }
            else if(_parameterList.SelectedCells.Count == 0 && _SelectedValueList.SelectedCells.Count != 0)
            {
                _SelectedValueList.Rows.RemoveAt(_SelectedValueList.SelectedCells[0].RowIndex);
            }
        }

        private void _DeleteBtn_Click(object sender, EventArgs e)
        {
            if (_parameterList.SelectedCells.Count == 0 && _SelectedValueList.SelectedCells.Count != 0)
            {
                _SelectedValueList.Rows.RemoveAt(_SelectedValueList.SelectedCells[0].RowIndex);
            }
            else if (_parameterList.SelectedCells.Count != 0 && _SelectedValueList.SelectedCells.Count == 0)
            {
                int paramSelectIndex = _parameterList.SelectedCells[0].RowIndex;
                _parameterList.Rows[paramSelectIndex].Cells[0].Value = "";
                _parameterList.Rows[paramSelectIndex].Cells[1].Value = "";
            }
        }

        private void _CopyBtn_Click(object sender, EventArgs e)
        {
            if (_parameterList.SelectedCells.Count != 0 && _SelectedValueList.SelectedCells.Count == 0)
            {
                _ClipNumber = _parameterList.Rows[_parameterList.SelectedCells[0].RowIndex].Cells[0].Value.ToString();
                _ClipValue = _parameterList.Rows[_parameterList.SelectedCells[0].RowIndex].Cells[1].Value.ToString();
            }
        }

        private void _PasteBtn_Click(object sender, EventArgs e)
        {
            if (_parameterList.SelectedCells.Count != 0 && _SelectedValueList.SelectedCells.Count == 0)
            {
                _parameterList.Rows[_parameterList.SelectedCells[0].RowIndex].Cells[0].Value = _ClipNumber;
                _parameterList.Rows[_parameterList.SelectedCells[0].RowIndex].Cells[1].Value = _ClipValue;
            }
        }

        private void _CloseBtn_Click(object sender, EventArgs e)
        {
            if(_SaveParameter() == ResultCodes.Success)
            {

            }
            _notifyReturn?.Invoke();
        }

        private void _EditBtn_Click(object sender, EventArgs e)
        {
            _EditingStart();
        }

       
    }
}
