using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ECNC3.Views
{
	public partial class RichTextBoxEx : RichTextBox
	{
        #region VariableMember
        /// <summary>ポップアップキーボード</summary>
		private StandardKeyBord _popupKeyboard = null;
        private Point _keyboardLocation = new Point(0, 0);
        /// <summary>
        /// 入力前文字列
        /// </summary>
        private string beforeLineString = "";
        /// <summary>
        /// デフォルト背景色
        /// </summary>
        private Color _defaultBackColor;
        /// <summary>
        /// デフォルト文字色
        /// </summary>
        private Color _defaultForeColor;
        /// <summary>
        /// 描画用ウインドウズメッセージ定数
        /// </summary>
        private const Int32 WM_PAINT = 0x000F;
        private const int EM_LINEFROMCHAR = 0xC9;
        [System.Runtime.InteropServices.DllImport("User32.Dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        int lineNo = 0;
        #endregion
        #region Propaties
        /// <summary>
        /// 行背景色有効
        /// </summary>
        [Browsable(true)]
        [Description("行背景色有効")]
        [Category("表示")]
        public bool RowBackColorEn { get; set; }
        /// <summary>
        /// 行背景色有効
        /// </summary>
        [Browsable(true)]
        [Description("行背景色")]
        [Category("表示")]
        public List<string> RowBackColorKey { get; set; }
        private SolidBrush paintStrBrush = null;
        public string[] tempText;
        #endregion
        #region Constractor
        public RichTextBoxEx()
        {
            _defaultBackColor = Models.FileUIStyleTable.DefaultBackColor;
            _defaultForeColor = Models.FileUIStyleTable.DefaultForeColor;
            BackColor = _defaultBackColor;
            ForeColor = _defaultForeColor;
            paintStrBrush = new SolidBrush(Models.FileUIStyleTable.EnabledForeColor);
            RowBackColorEn = false;
            RowBackColorKey = null;
            RowBackColorKey = new List<string>();
            Disposed += DisposeMember;
            MouseClick += ProgramEditBox_MouseClick;
            MouseDown += ProgramEditBox_MouseDown;
        }
        #endregion
        #region DisposeMethod
        private void DisposeMember(object sender, EventArgs e)
        {
            RowBackColorKey.Clear();
            RowBackColorKey.TrimExcess();
            RowBackColorKey = null;
        }
        #endregion
        #region PrivateMethods

        //protected override void WndProc(ref System.Windows.Forms.Message m)
        //{
        //    base.WndProc(ref m);
        //    if (m.Msg == WM_PAINT)
        //    {
        //        using (Graphics graphic = base.CreateGraphics())
        //            OnPaint(new PaintEventArgs(graphic, base.ClientRectangle));
        //    }
        //}
        //protected override void OnPaint(PaintEventArgs pe)
        //{
        //    if (RowBackColorEn == true
        //        && RowBackColorKey.Count != 0
        //        && RowBackColorKey[0] != "")
        //    {
        //        foreach (string key in RowBackColorKey)
        //        {
        //            if (key == "") continue;
        //            using (Brush brush = new SolidBrush(Color.Pink))
        //            {
        //                // 改行検索
        //                Regex regex = new Regex(key);
        //                foreach (Match match in regex.Matches(this.Text))
        //                {
        //                    Point point = this.GetPositionFromCharIndex(match.Index);
        //                    pe.Graphics.FillRectangle(brush, point.X, point.Y, pe.ClipRectangle.Width, 18);
        //                    pe.Graphics.DrawString(Lines[CharToRowCount(match.Index - 1)],
        //                                            Font, paintStrBrush, point);
        //                }
        //            }
        //        }
        //    }
        //}
        //private int CharToRowCount(int charCount)
        //{
        //    string[] temp = Lines;
        //    int ret = charCount;
        //    for (int textRowCt = 0; textRowCt < temp.Count(); textRowCt++)
        //    {
        //        ret -= temp[textRowCt].Length;
        //        if (ret <= 0) return textRowCt;
        //    }
        //    return 0;
        //}
        #region<---ポップアップキーボード--->
        /// <summary>設定結果取得関数定義</summary>
        public delegate void NotifyReturnDelegate();
        /// <summary>終了通知</summary>
        private NotifyReturnDelegate _notifyReturn = null;
        /// <summary>>設定結果取得</summary>
        public NotifyReturnDelegate NotifyReturn
        {
            set { _notifyReturn = value; }
        }

        /// <summary>
        /// ポップアップキーボード：入力からの終了要求
        /// </summary>
        private void _edtComment_ClickCallBack()
        {
            ResetInput();
        }
        /// <summary>
        /// ポップアップキーボード：入力からの表示文字列の更新要求
        /// </summary>
        /// <param name="text"></param>
        private void _edtComment_ClickTextChanged(string text)
        {
            //改行コードをLFに揃える
            string[] tempText = TextToArray();
            if (tempText[lineNo] != null)
            {
                beforeLineString = tempText[lineNo];
            }
            tempText[lineNo] = text;
            if (Text != null) Text = null;
            foreach (string rowText in tempText)
            {
                Text += rowText + "\n";
            }
            tempText = null;
        }
        /// <summary>
        /// キーボード表示/非表示用
        /// </summary>
        /// <param name="visible"></param>
        public void KeybordVisible(bool visible)
        {
            //int _selectIndex = SelectionStart;
            if (visible == true)
            {
                _keyboardLocation = _popupKeyboardLocationSetting(new Point(0, 0), _keyboardViewType.Clicked);
                if (null == _popupKeyboard)
                {
                    _popupKeyboard = new StandardKeyBord();
                    _popupKeyboard.Disposed += KeyboardClosed;
                }
                _popupKeyboard.Visible = false;
                this.Parent.Controls.Add(_popupKeyboard);
                _popupKeyboard.Location = new Point(0, 340);//400から変更：柏原
                _popupKeyboard.Visible = true;
            }
            else
            {
                if (null != _popupKeyboard)
                {
                    this.Parent.Controls.Remove(_popupKeyboard);
                    _popupKeyboard.Dispose();
                }
            }

            //SelectionStart = _selectIndex;
            //Select(_selectIndex, 0);
        }
       
        /// <summary>
        /// ポップアップキーボード：入力状態の初期化
        /// </summary>
        public void ResetInput()
        {
            if (null != _popupKeyboard)
            {
                this.Parent.Controls.Remove(_popupKeyboard);
                _popupKeyboard.Dispose();
            }
        }
        /// <summary>
		/// マウス：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ProgramEditBox_MouseClick(object sender, MouseEventArgs e)
        {
            lineNo = GetRowNumber();
            ProgramEditBox_SelectionChanged(e.Location);
        }
        /// <summary>
        /// DGVEX：
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgramEditBox_SelectionChanged(Point selectPoint)
        {
            if (_popupKeyboard == null)
            {
                if (this.Focused == false)
                {
                    //ポップアップキーボード：無し
                    return;
                }
                return;
            }
            if (lineNo < 0) return;//ProgramEditBox.CurrentCell = null;対応

            //選択されたXYを取得
           
            //キーボード位置を変更
            _keyboardLocation = _popupKeyboardLocationSetting(selectPoint, _keyboardViewType.KeyPressed);

            //キーボードのフォーカスON
            //_popupKeyboard.FocusedInputForm();
        }
        #endregion
        #region<---ポップアップキーボード--->
        /// <summary>
        /// ポップアップキーボード：表示タイプ
        /// </summary>
        private enum _keyboardViewType
        {
            Clicked,	//DGVEXクリック
            KeyPressed	//物理キーボードが押された
        }
     
        /// <summary>
        /// DGVEX：マウス：ダウン：表示座標取得
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgramEditBox_MouseDown(object sender, MouseEventArgs e)
        {
            //_keyboardLocation = _popupKeyboardLocationSetting(e.Location, _keyboardViewType.Clicked);
        }

        /// <summary>
        ///  ポップアップキーボード：有れば閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyboardClosed(object sender, EventArgs e)
        {
            if (_popupKeyboard != null)
            {
                _popupKeyboard = null;
            }
        }
        /// <summary>
        /// ポップアップキーボード：表示座標の取得
        /// </summary>
        /// <param name="_location"></param>
        /// <returns></returns>
        private Point _popupKeyboardLocationSetting(Point _location, _keyboardViewType _type)
        {
            Point ret = new Point(0, 0);
            switch (_type)
            {   //マウスクリック
                case _keyboardViewType.Clicked:
                    if (_location.Y <= Location.Y + 90)
                    {
                        ret.X = 0 - 7;
                        ret.Y = 768 - 323;
                    }
                    else
                    {
                        ret.X = 0 - 7;
                        ret.Y = 0;
                    }
                    break;
                //キー押下
                case _keyboardViewType.KeyPressed:
                    if (_location.Y <= 768 / 2)
                    {
                        ret.X = 0 - 7;
                        ret.Y = 768 - 323;
                    }
                    else
                    {
                        ret.X = 0 - 7;
                        ret.Y = 0;
                    }
                    break;
            }
            return ret;
        }
        /// <summary>
        /// DGVEX：キーダウン：選択ボタンの変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgramEditBox_KeyDown(object sender, MouseEventArgs e)
        {
            //Focus();
            switch (((ButtonEx)(sender)).Name)
            {
                case "_btnKeyUp":
                    SendKeys.Send("{UP}");
                    break;

                case "_btnKeyDown":
                    SendKeys.Send("{DOWN}");
                    break;

            }
        }
        #endregion
        #endregion
        public string[] TextToArray()
        {
            if (Text.Contains("\n"))
            {
                Text = Text.Replace("\r\n", "\n");
                Text = Text.Replace("\r", "\n");
            }
            return Text.Split('\n');
        }
        public int GetRowNumber()
        {
            return SendMessage(Handle, EM_LINEFROMCHAR, -1, 0);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // RichTextBoxEx
            // 
            this.ResumeLayout(false);

        }
        public bool GetKeyboadIsVisible()
        {
            return (_popupKeyboard != null) ? true : false;
        }
    }
}
