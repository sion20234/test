//-----------------------------------------------------------------------------------------
//
// (1) システム名称 : ELENIX ECNCⅢ
// (2) ファイル名   : FuncPassForm.cs
// (3) 概要         : FUNCTIONパスワード画面
// (4) 作成日       : 2015.05.31
// (5) 作成者       : 八野
// (6) 特記事項     : VisualStudio2015 C#
// (7) 更新履歴     :
//
//-----------------------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ECNC3.Views
{
    public partial class FuncPassForm : ECNC3Form
    {
        //-----------------------------------------------------------------------------------------
        //
        //　フォーム　コンストラクタ
        //
        //-----------------------------------------------------------------------------------------
        public FuncPassForm()
        {
            InitializeComponent();
            OutLineEnable = true;
        }
        /// <summary>
        /// SendKey式キーボード
        /// </summary>
        StandardKeyBord _keyBord = null;
        /// <summary>
        /// 
        /// </summary>
        public Enumeration.AccountLevel _ret = Enumeration.AccountLevel.None;
        //-----------------------------------------------------------------------------------------
        //
        //　フォーム　ロード時のイベントハンドラ
        //
        //-----------------------------------------------------------------------------------------
        private void FuncPassForm_Load(object sender, EventArgs e)
        {
            _PassTextBox.UseSystemPasswordChar = true;
            _PasswordHideBt.SetBack(_PassTextBox.UseSystemPasswordChar);

            _keyBord = new StandardKeyBord();
            _keyBord.Location = new Point((this.Size.Width - _keyBord.Size.Width) / 2, 270);
            this.Controls.Add(_keyBord);
            _IdTextBox.Focus();
        }

        //-----------------------------------------------------------------------------------------
        //
        //　閉じる　ボタン　クリック時のイベントハンドラ
        //
        //-----------------------------------------------------------------------------------------
        private void CloseBt_Click(object sender, EventArgs e)
        {
			DialogResult = DialogResult.Cancel;
			this.Close();
        }

        //-----------------------------------------------------------------------------------------
        //
        //　確定　ボタン　クリック時のイベントハンドラ
        //
        //-----------------------------------------------------------------------------------------
        
        private void EnterPassBt_Click(object sender, EventArgs e)
        {
            _ret = Enumeration.AccountLevel.None;
            Models.FileAccountPasswords account = new Models.FileAccountPasswords();
            account.Read();
            foreach (Models.StructureAccountItem item in account.Items)
            {
                if(item.ID == _IdTextBox.Text)
                {
                    if (item.Password == _PassTextBox.Text)
                    {
                        _ret = item.Level;
                    }
                }
            }
            if(_ret == Enumeration.AccountLevel.None
                || _IdTextBox.Text == "")
            {
                using (MessageDialog dlg = new MessageDialog())
                {
                    dlg.Error(842, this);
                }
                _PassTextBox.Focus();
                _PassTextBox.SelectAll();
                if(_IdTextBox.Text == "")
                {
                    _IdTextBox.Focus();
                }
                _ret = Enumeration.AccountLevel.None;
                return;

            }
 			this.Close();
		}
        //-----------------------------------------------------------------------------------------
        //
        //　クリア　ボタン　クリック時のイベントハンドラ
        //
        //-----------------------------------------------------------------------------------------
        private void DeleteTextBt_Click(object sender, EventArgs e)
        {
            if (_IdTextBox.Focused == true) _IdTextBox.Text = "";
            if (_PassTextBox.Focused == true) _PassTextBox.Text = "";
        }

      

        private void PassTextBox_MouseUp(object sender, MouseEventArgs e)
        {
            if(_keyBord != null)
            {
                if (this.Controls.Contains(_keyBord)) this.Controls.Remove(_keyBord);
                _keyBord.Dispose();
                {
                    _keyBord = null;
                }
            }
            _keyBord = new StandardKeyBord();
            _keyBord.Location = new Point(0, 270);
            this.Controls.Add(_keyBord);
            _PassTextBox.Focus();
        }

        private void FuncPassForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //キーボード破棄
            if (_keyBord != null)
            {
                if (this.Controls.Contains(_keyBord)) this.Controls.Remove(_keyBord);
                _keyBord.Dispose();
                {
                    _keyBord = null;
                }
            }
        }
        public static Enumeration.AccountLevel ShowDialogMode(IWin32Window parent)
        {
            FuncPassForm passForm = new FuncPassForm();
            passForm.ShowDialog(parent);
            return passForm._ret;
        }

        private void _PasswordHideBt_MouseUp(object sender, MouseEventArgs e)
        {
            if(_PasswordHideBt.GetBack() == true)
            {
                _PassTextBox.UseSystemPasswordChar = false;
                _PasswordHideBt.SetBack(false);
            }
            else
            {
                _PassTextBox.UseSystemPasswordChar = true;
                _PasswordHideBt.SetBack(true);
            }
        }

        private void _IdTextBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (_keyBord != null)
            {
                if (this.Controls.Contains(_keyBord)) this.Controls.Remove(_keyBord);
                _keyBord.Dispose();
                {
                    _keyBord = null;
                }
            }
            _keyBord = new StandardKeyBord();
            _keyBord.Location = new Point(0, 270);
            this.Controls.Add(_keyBord);
            _IdTextBox.Focus();
        }

        private void FuncPassForm_Shown(object sender, EventArgs e)
        {
            _IdTextBox.Focus();
        }
    }
}
