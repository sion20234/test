
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ECNC3.Models;
using ECNC3.Enumeration;
using ECNC3.Views.Popup;

namespace ECNC3.Views
{
    /// <summary>
    /// アカウント情報設定画面
    /// 旧：パスワード設定画面
    /// </summary>
    public partial class PasswordChangeForm : ECNC3Form
    {
        #region Constractor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PasswordChangeForm()
        {
            InitializeComponent();
            _accountLevel = new List<AccountLevel>();
            foreach(AccountLevel temp in Enum.GetValues(typeof(AccountLevel)))
            {
                _accountLevel.Add(temp);
            }
        }
        #endregion
        #region VariableMember
        /// <summary>
        /// SendKey式キーボード
        /// </summary>
        StandardKeyBord _keyBord = null;
        List<AccountLevel> _accountLevel = null;
        #endregion
        #region Initialize
        /// <summary>
        /// アカウントリスト初期化処理
        /// </summary>
        private void _AccountList_Initialize()
        {
            _AccountList.Initialize(24, 40, false);
            //背景、文字色などのスタイル設定
            _AccountList.DefaultCellStyle.BackColor = FileUIStyleTable.DefaultBackColor;
            _AccountList.DefaultCellStyle.ForeColor = FileUIStyleTable.DefaultForeColor;
            _AccountList.DefaultCellStyle.SelectionBackColor = FileUIStyleTable.EnabledBackColor;
            _AccountList.DefaultCellStyle.SelectionForeColor = FileUIStyleTable.EnabledForeColor;
            _AccountList.DefaultCellStyle.Font = new Font(_AccountList.DefaultCellStyle.Font.FontFamily, 24);
            _AccountList.Columns[1].ReadOnly = true;
            //最大行数100行
            _AccountList.RowCount = 100;
            
            foreach(DataGridViewRow row in _AccountList.Rows)
            {
                //インデックス番号設定
                _AccountList[0, row.Index].Value = row.Index + 1;
                _AccountList[1, row.Index].Value = AccountLevel.None.ToString();
                _AccountList[2, row.Index].Value = "";
                _AccountList[3, row.Index].Value = "";
            }
        }
        /// <summary>
        /// キーボード表示
        /// </summary>
        private void KeyBordShow()
        {
            _keyBord = new StandardKeyBord();
            _keyBord.VisibleChanged += _keyBord_VisibleChanged;
            _keyBord.Location = new Point(0, 300);
            this.Controls.Add(_keyBord);
        }

        #endregion
        #region FileImport/Export
        /// <summary>
        /// 外部ファイルアカウント情報読み込み
        /// </summary>
        private void _AccountDataImport()
        {
            FileAccountPasswords accountData = new FileAccountPasswords();
            //ファイル読込み
            accountData.Read();
            foreach (StructureAccountItem item in accountData.Items)
            {
                //権限レベルの初期値
                _AccountList[1, item.Number - 1].Value = AccountLevel.None.ToString();
                //IDが登録されていない場合は次の処理へ
                if (item.ID == "")  continue;
                //アカウント情報設定
                _AccountList[1, item.Number - 1].Value = item.Level.ToString();
                _AccountList[2, item.Number - 1].Value = item.ID;
                _AccountList[3, item.Number - 1].Value = item.Password;
            }
        }
        /// <summary>
        /// アカウント情報保存（外部ファイルに出力）
        /// </summary>
        private void _AccountDataSave()
        {
            FileAccountPasswords accountData = new FileAccountPasswords();
            //書込み準備
            foreach(DataGridViewRow row in _AccountList.Rows)
            {
                StructureAccountItem itemTemp = new StructureAccountItem();
                //インデックス番号取得
                itemTemp.Number = (int)row.Cells[0].Value;
                foreach(AccountLevel level in _accountLevel)
                {
                    if (level.ToString() == row.Cells[1].Value.ToString())
                    {
                        //権限レベル取得
                        itemTemp.Level = level;
                    }
                }
                //ID取得
                itemTemp.ID = row.Cells[2].Value.ToString();
                //パスワード取得
                itemTemp.Password = row.Cells[3].Value.ToString();
                //書き込みデータに追加
                accountData.Items.Add(itemTemp);
            }
            accountData.Write();
            accountData.Items.Clear();
            accountData.Items = null;
        }
        #endregion
        #region EventHandler
        /// <summary>
        /// フォームのロード時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PasswordChangeForm_Load(object sender, EventArgs e)
        {
            //アカウントリスト初期化
            _AccountList_Initialize();
            //アカウントリストへインポート
            _AccountDataImport();
            //キーボード表示
            KeyBordShow();
        }
        /// <summary>
        /// 閉じるボタンマウスアップ時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackUserServiceFormBt_Click(object sender, EventArgs e)
        {
            //パスワード変更フォームを閉じて、サービスフォームを開く
            this.Close();
        }
        /// <summary>
        /// 削除ボタンマウスアップ時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _RowRemoveBtn_MouseUp(object sender, MouseEventArgs e)
        {
            if (_AccountList.SelectedCells.Count <= 0) return;
            using (MessageDialog msgDir = new MessageDialog(_RowRemoveBtn))
            {
                if (_RowRemoveBtn.GetBack() != true) _RowRemoveBtn.SetBack(true);
                if (msgDir.Question(5033, this) == true)
                {
                    _AccountList.Rows[_AccountList.SelectedCells[0].RowIndex].Cells[1].Value = AccountLevel.None.ToString();
                    _AccountList.Rows[_AccountList.SelectedCells[0].RowIndex].Cells[2].Value = "";
                    _AccountList.Rows[_AccountList.SelectedCells[0].RowIndex].Cells[3].Value = "";
                }
                if (_RowRemoveBtn.GetBack() != false) _RowRemoveBtn.SetBack(false);
            }
        }
        /// <summary>
        /// フォーム表示後フォーカス設定前処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PasswordChangeForm_Shown(object sender, EventArgs e)
        {
            _AccountList.ClearSelection();
        }
        private void _OkBtn_MouseUp(object sender, MouseEventArgs e)
        {
            using (MessageDialog msgDir = new MessageDialog())
            {
                if (msgDir.Question(5034, this) == false) return;
            }
            _AccountDataSave();
            //パスワード変更フォームを閉じて、サービスフォームを開く
            this.Close();
        }
        private void _KeybordBtn_Click(object sender, EventArgs e)
        {
            if(_KeybordBtn.GetBack() == true)
            {
                _keyBord.Visible = false;
            }
            else
            {
                if (this.Controls.Contains(_keyBord) == false)
                {
                    KeyBordShow();
                    if (_KeybordBtn.GetBack() == false)
                    {
                        _KeybordBtn.SetBack(true);
                    }
                }
                else
                {
                    _keyBord.Visible = true;
                }
            }
        }
        private void _keyBord_VisibleChanged(object sender, EventArgs e)
        {
            if(_keyBord.Visible == false)
            {
                if(_KeybordBtn.GetBack() == true)
                {
                    _KeybordBtn.SetBack(false);
                }
            }
            else
            {
                if (_KeybordBtn.GetBack() == false)
                {
                    _KeybordBtn.SetBack(true);
                }
            }
        }
        #endregion

        private void _AccountList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 1)
            {
                List<string> selectAccount = new List<string>();
                foreach(AccountLevel tempSource in _accountLevel)
                {
                    selectAccount.Add(tempSource.ToString());
                }
                (_AccountList[1, e.RowIndex] as DataGridViewButtonCell).Value
                    = SelectCommandsDialogVariable.ShowSubForm(this, selectAccount);

            }
        }

        private void _OkBtn_Click(object sender, EventArgs e)
        {

        }

        private void _RowRemoveBtn_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
