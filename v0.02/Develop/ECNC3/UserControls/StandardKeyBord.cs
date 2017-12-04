using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ECNC3.Models;
using ECNC3.Views.UserControls;

namespace ECNC3.Views
{
    /// <summary>キーボードユーザーコントロール</summary>
    public partial class StandardKeyBord : UserControlEx
    {
        int r;
        int b;
        public ButtonEx targetBtn = null;
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
        Point mousePoint;
        /// <summary>コンストラクタ</summary>
        public StandardKeyBord()
        {
            ToolColorEnable = true;
            InitializeComponent();
            ButtonsToolInit(Controls);
        }
        /// <summary>コンストラクタ</summary>
        public StandardKeyBord(ButtonEx parentButton = null)
        {
            ToolColorEnable = true;
            InitializeComponent();
            ButtonsToolInit(Controls);
            targetBtn = parentButton;
        }
        public void TextBoxVisible(bool visible)
        {
            _EditTextBox.Visible = visible;
            _movebt.Visible = !visible;
            ESC_Button.Enabled = !visible;
            if (visible == true)
            {
                _EditTextBox.BackColor = FileUIStyleTable.ToolBackColor;
                _EditTextBox.ForeColor = FileUIStyleTable.ToolForeColor;
                this.Focus();
                _EditTextBox.Focus();
            }
        }
        public string GetText()
        {
            return _EditTextBox.Text;
        }
        public void SetText(string value)
        {
            _EditTextBox.Text = value;
        }
        /// <summary>
        /// 
        /// </summary>
		public enum OperationModes
        {
            BuilIn,
            Popup,
        }
        /// <summary>
        /// テキストボックスのタイプ
        /// </summary>
        private enum TextboxType
        {
            TextBox = 1,
            RichTextBox = 2
        }
        /// <summary>
        /// クリップボード操作用
        /// </summary>
        private enum ClipBoardCommand
        {
            Cut = 1,
            Copy = 2,
            Paste = 3
        }
        public OperationModes OperationMode
        {
            set
            {
                if (OperationModes.BuilIn == value)
                {
                    BackColor = FileUIStyleTable.DefaultBackColor;
                }
                else
                {
                    BackColor = Color.FromArgb(60, 60, 0);
                }
            }
        }
        private void KeyBord_Load(object sender, EventArgs e)
        {
            Selectable = false;
            BringToFront();
            r = this.ClientRectangle.Right - OutLineSize;
            b = this.ClientRectangle.Bottom - OutLineSize;
            SelectFormInit();
            _dummyBackPanelBt.OutLineEn = false;
            _dummyBackPanelBt.OutLineSize = 0;
            _dummyBackPanelBt.OutLineColor = Color.Transparent;
            _dummyBackPanelBt.ForeColor = Color.Transparent;
            _dummyBackPanelBt.FlatAppearance.MouseDownBackColor = Color.Transparent;
            _btnKeyShift.SetBack(false);//初回GetBack不具合回避
        }

        private void key_MouseUp(object sender, MouseEventArgs e)
        {
            if (_EditTextBox.Visible == true) _EditTextBox.Focus();
            string strKey = (sender as ButtonEx).Text;
            switch (strKey)
            {
                case ""://空
                    break;
                case "&&"://アンド　※[デザイン]で"&"を表示するには"&&"なので
                    SendKeys.Send("&");
                    break;
                case "^"://山　※
                    Clipboard.SetText("^", TextDataFormat.UnicodeText);
                    SendKeys.SendWait("+{INS}");
                    Clipboard.Clear();
                    //SendKeys.Send("{^}");
                    break;

                default://その他
                    SendKeys.Send("{" + strKey + "}");//+、%、~、(、)、 特別な意味を持つコードもあるので{}でくくる。柏原
                    break;
            }
        }
        private void _btnKeyEnter_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{Enter}");
        }

        private void ESC_Button_Click(object sender, EventArgs e)
        {
            _notifyReturn?.Invoke();//画面閉じるイベント送信
            this.Dispose();
        }

        private void _movebt_MouseDown(object sender, MouseEventArgs e)
        {
            TenKeyMouseDown(sender, e);
        }

        private void _movebt_MouseMove(object sender, MouseEventArgs e)
        {
            TenKeyMouseMove(sender, e);
        }
        /// <summary>
        /// マウス：ダウン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TenKeyMouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                mousePoint = new Point(e.X, e.Y);//位置を記憶
            }
        }
        /// <summary>
        /// マウス：移動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TenKeyMouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                if ((this.Top + e.Y - mousePoint.Y) <= 0)
                {
                    this.Top = 0;
                }
                else
                {
                    this.Top += e.Y - mousePoint.Y;
                }
                this.Left += e.X - mousePoint.X;
            }
        }
        /// <summary>
        /// マウス：アップ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TenKeyMouseUp(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                if (this.Left < 0 || (this.Left + this.Size.Width) > 1024) this.Left = 0;
                if (this.Top < 0 || (this.Top + this.Size.Height) > 768) this.Top = 400;
            }
        }

        private void PLUSkey_MouseUp(object sender, MouseEventArgs e)
        {
            if (_btnKeyShift.GetBack() == true)
            {
                SendKeys.Send(Keys.Shift + (sender as ButtonEx).Text);
            }
            else
            {
                SendKeys.Send("{+}");
            }
        }

        private void _btnKeySpace_MouseUp(object sender, MouseEventArgs e)
        {
            SendKeys.Send(" ");
        }

        private void _btnKeyBackSpace_MouseUp(object sender, MouseEventArgs e)
        {
            SendKeys.Send("{BACKSPACE}");
        }

        private void _btnKeyDelete_MouseUp(object sender, MouseEventArgs e)
        {
            SendKeys.Send("{DELETE}");
        }

        private void _btnKeyUp_MouseUp(object sender, MouseEventArgs e)
        {
            SendKeys.Send("{UP}");
        }

        private void _btnKeyLeft_MouseUp(object sender, MouseEventArgs e)
        {
            SendKeys.Send("{LEFT}");
        }

        private void _btnKeyRight_MouseUp(object sender, MouseEventArgs e)
        {
            SendKeys.Send("{RIGHT}");
        }

        private void _btnKeyDown_MouseUp(object sender, MouseEventArgs e)
        {
            SendKeys.Send("{DOWN}");
        }

        private void _btnKeyHome_MouseUp(object sender, MouseEventArgs e)
        {
            SendKeys.Send("{HOME}");
        }

        private void _btnKeyEnd_MouseUp(object sender, MouseEventArgs e)
        {
            SendKeys.Send("{END}");
        }
        private void CARETkey_MouseUp(object sender, MouseEventArgs e)
        {
            SendKeys.Send("+(6)");
        }
        /// <summary>
        /// クリップボード操作処理
        /// </summary>
        private void ClipBoardProccess(ClipBoardCommand cmd)
        {
            TextboxType type = TextboxType.TextBox;
            foreach (Control ctrl in Parent.Controls)
            {
                //テキストボックスが編集中？
                if (ctrl.Focused == false) { continue; }
                //テキストボックス？
                if (ctrl.GetType() == typeof(TextBoxEx)) { type = TextboxType.TextBox; }
                //リッチテキスト？
                else if (ctrl.GetType() == typeof(RichTextBoxEx)) { type = TextboxType.RichTextBox; }
                //対象外
                else { continue; }
                switch (type)
                {
                    //テキストボックスの場合
                    case TextboxType.TextBox:
                        switch (cmd)
                        {
                            case ClipBoardCommand.Cut:
                                (ctrl as TextBoxEx).Cut();
                                break;

                            case ClipBoardCommand.Copy:
                                (ctrl as TextBoxEx).Copy();
                                break;

                            case ClipBoardCommand.Paste:
                                (ctrl as TextBoxEx).Paste();
                                break;

                        }
                        break;
                    //リッチテキストボックスの場合
                    case TextboxType.RichTextBox:
                        switch (cmd)
                        {
                            case ClipBoardCommand.Cut:
                                (ctrl as RichTextBoxEx).Cut();
                                break;

                            case ClipBoardCommand.Copy:
                                (ctrl as RichTextBoxEx).Copy();
                                break;

                            case ClipBoardCommand.Paste:
                                (ctrl as RichTextBoxEx).Paste();
                                break;

                        }
                        break;

                }
            }
        }
        private void _btnKeyCut_MouseUp(object sender, MouseEventArgs e)
        {
            ClipBoardProccess(ClipBoardCommand.Cut);
        }

        private void _btnKeyCopy_MouseUp(object sender, MouseEventArgs e)
        {
            ClipBoardProccess(ClipBoardCommand.Copy);
        }

        private void _btnKeyPaste_MouseUp(object sender, MouseEventArgs e)
        {
            ClipBoardProccess(ClipBoardCommand.Paste);
        }

        private void _btnKeyLineDelete_MouseUp(object sender, MouseEventArgs e)
        {
            TextboxType type = TextboxType.TextBox;
            foreach (Control ctrl in Parent.Controls)
            {
                //テキストボックスが編集中？
                if (ctrl.Focused == false) { continue; }
                //テキストボックス？
                if (ctrl.GetType() == typeof(TextBoxEx)) { type = TextboxType.TextBox; }
                //リッチテキスト？
                else if (ctrl.GetType() == typeof(RichTextBoxEx)) { type = TextboxType.RichTextBox; }
                //対象外
                else { continue; }
                string str = "";
                List<string> lines = null;
                int row = 1, startPos = 0, selectPos = 0;
                switch (type)
                {
                    case TextboxType.TextBox:
                        //文字列
                        str = (ctrl as TextBoxEx).Text;
                        //カレットの位置を取得
                        selectPos = (ctrl as TextBoxEx).SelectionStart;
                        //カレットの位置までの行を数える
                        for (int endPos = 0;
                            (endPos = str.IndexOf('\n', startPos)) < selectPos && endPos > -1;
                            row++)
                        {
                            startPos = endPos + 1;
                        }
                        lines = new List<string>((ctrl as TextBoxEx).Lines);
                        lines.RemoveAt(row - 1); // 行削除
                        (ctrl as TextBoxEx).Text = String.Join("\n", lines);
                        break;

                    case TextboxType.RichTextBox:
                        //文字列
                        str = (ctrl as RichTextBoxEx).Text;
                        //カレットの位置を取得
                        selectPos = (ctrl as RichTextBoxEx).SelectionStart;
                        //カレットの位置までの行を数える
                        for (int endPos = 0;
                            (endPos = str.IndexOf('\n', startPos)) < selectPos && endPos > -1;
                            row++)
                        {
                            startPos = endPos + 1;
                        }
                        lines = new List<string>((ctrl as RichTextBoxEx).Lines);
                        if (row - 1 > 0)
                        {//row=0対応//柏原
                            lines.RemoveAt(row - 1); // 行削除
                        }
                        (ctrl as RichTextBoxEx).Text = String.Join("\n", lines);
                        break;
                }
            }
        }
        /// <summary>設定結果取得関数定義</summary>
        public delegate void NotifyReturnDelegate();
        /// <summary>終了通知</summary>
        private NotifyReturnDelegate _notifyReturn = null;
        /// <summary>>設定結果取得</summary>
        public NotifyReturnDelegate NotifyReturn
        {
            set { _notifyReturn = value; }
        }

        private void _btnKeyShift_CheckedChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// SHIFTキーON/OFFでキー表示を変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnKeyShift_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl.GetType() != typeof(ButtonEx)) continue;
                if ((ctrl as ButtonEx).Text.Length == 1)
                {
                }
                else
                {
                    //1桁以外
                    switch ((ctrl as ButtonEx).Text)
                    {
                        case "&&":
                        case "":
                            break;//有効
                        default:
                            continue;//無効
                    }
                }
                string strKeyText;
                char keyText;
                if ((ctrl as ButtonEx).Text == "")
                {
                    keyText = '0';
                    strKeyText = "";
                }
                else
                {
                    keyText = (ctrl as ButtonEx).Text.ToCharArray()[0];
                    strKeyText = keyText.ToString();
                }

                bool boolRet = specialShiftCode(_btnKeyShift.GetBack(), (ctrl as ButtonEx).Name, ref strKeyText);//特別なコードはここで変換
                if (boolRet)
                {//特別なキー
                    (ctrl as ButtonEx).Text = strKeyText;
                }
                else
                {//その他 
                    if (char.IsLetter(keyText) == true)
                    {
                        if (_btnKeyShift.GetBack())
                        {
                            if (char.IsUpper(keyText) == false)
                            {
                                (ctrl as ButtonEx).Text = char.ToUpper(keyText).ToString();
                            }
                        }
                        else
                        {
                            if (char.IsLower(keyText) == false)
                            {
                                (ctrl as ButtonEx).Text = char.ToLower(keyText).ToString();
                            }
                        }
                    }
                }
            }
            if (_btnKeyShift.GetBack()) _btnKeyShift.SetBack(false);//背景色：デフォルト
            else                        _btnKeyShift.SetBack(true); //背景色：ハイライト
            //再描画リフレッシュ
            this.Refresh();
        }
        /// <summary>
        /// 特別なコードのSHIFT ON/OFFのコード変換
        /// </summary>
        /// <param name="ShiftON"></param>
        /// <param name="keyTex"></param>
        /// <param name="ctrlName"></param>
        /// <returns></returns>
        private bool specialShiftCode(bool ShiftON, string ctrlName, ref string strKeyText)
        {
            if (CtrlNameFilter(ctrlName) == false)
            {//ここで変換しないコードは戻る
                return false;
            }
            if (ShiftON)
            {//OFF時に戻す
                switch (strKeyText)
                {
                    case "1": strKeyText = "!"; break;
                    case "2": strKeyText = "\""; break;
                    case "3": strKeyText = "#"; break;
                    case "4": strKeyText = "$"; break;
                    case "5": strKeyText = "%"; break;
                    case "6": strKeyText = "&&"; break;
                    case "7": strKeyText = "'"; break;
                    case "8": strKeyText = "("; break;
                    case "9": strKeyText = ")"; break;
                    case "0": strKeyText = ""; break;
                    case "-": strKeyText = "="; break;
                    case "^": strKeyText = "~";
                        break;
                    case "\\": strKeyText = "|"; break;
                    default: strKeyText = "@"; break;
                }
                if (strKeyText == "@") return false;
                return true;
            }
            else
            {//ON時に戻す
                switch (strKeyText)
                {
                    case "!": strKeyText = "1"; break;
                    case "\"": strKeyText = "2"; break;
                    case "#": strKeyText = "3"; break;
                    case "$": strKeyText = "4"; break;
                    case "%": strKeyText = "5"; break;
                    case "&": strKeyText = "6"; break;
                    case "'": strKeyText = "7"; break;
                    case "(": strKeyText = "8"; break;
                    case ")": strKeyText = "9"; break;
                    case "": strKeyText = "0"; break;
                    case "=": strKeyText = "-"; break;
                    case "~": strKeyText = "^";
                        break;
                    case "|": strKeyText = "\\"; break;
                    default: strKeyText = "@"; break;
                }
                if (strKeyText == "@") return false;
                return true;
            }
        }
        /// <summary>
        /// コントロール名でフィルター
        /// </summary>
        /// <param name="ctrlName"></param>
        /// <returns></returns>
        private bool CtrlNameFilter(string ctrlName)
        {
            switch (ctrlName)
            {
                case "ExclamationKey": return true;
                case "DoubleQKey": return true;
                case "SharpKey": return true;
                case "DollKey": return true;
                case "PercentKey": return true;
                case "AndKey": return true;
                case "SingleQKey": return true;
                case "ParentLKey": return true;
                case "ParentRKey": return true;
                case "ZeroAndWaKey": return true;
                case "EqualKey": return true;
                case "TildeKey":
                    return true;
                case "PipeKey": return true;
            }
            return false;
        }
    }
}
