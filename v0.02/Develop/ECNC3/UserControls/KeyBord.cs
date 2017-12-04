using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ECNC3.Models;

namespace ECNC3.Views
{
    /// <summary>キーボードユーザーコントロール</summary>
    public partial class KeyBord : UserControl
    {
		/// <summary>コンストラクタ</summary>
        public KeyBord()
        {
            InitializeComponent();
        }
		public enum OperationModes
		{
			BuilIn,
			Popup,
		}
		public OperationModes OperationMode
		{
			set
			{
				if( OperationModes.BuilIn == value ) {
					BackColor = Color.FromArgb( 35, 35, 0 );
				} else {
					BackColor = Color.FromArgb(60, 60, 0);
				}
			}
		}

		private void KeyBord_Load( object sender, EventArgs e )
		{
            this.BackColor = FileUIStyleTable.ToolBackColor;
            this.ForeColor = FileUIStyleTable.ToolForeColor;
            _AllControlsStyleChange(this.Controls);
        }
        /// <summary>
        /// 全コントロールのスタイル変更処理（回帰処理）
        /// </summary>
        /// <param name="ctrls"></param>
        private void _AllControlsStyleChange(Control.ControlCollection ctrls)
        {
            if (ctrls != null)
            {
                foreach (Control ctrl in ctrls)
                {
                    ctrl.BackColor = FileUIStyleTable.ToolBackColor;
                    ctrl.ForeColor = FileUIStyleTable.ToolForeColor;
                    if (ctrl.Controls != null)
                    {
                        _AllControlsStyleChange(ctrl.Controls);
                    }
                }
            }
        }

        //ESC時文字列を戻す
        string _edtInputEscBack;
		/// <summary>入力文字列</summary>
		public string InputValue
		{
			get { return _edtInput.Text; }
			set
			{
				_edtInput.Text = value;
				_edtInputEscBack = value;
				MoveCursor( Keys.End );
			}
		}
		/// <summary>カーソル移動イベント</summary>
		/// <param name="sender">イベント送信元オブジェクト</param>
		/// <param name="e">イベントパラメータ</param>
		private void _cursorMove_Click( object sender, EventArgs e )
		{
			ApiMonitorTask.monitorMx.WaitOne();
			if( sender.Equals( _btnKeyUp ) ) {
				MoveCursor( Keys.Up );
			} else if( sender.Equals( _btnKeyDown ) ) {
				MoveCursor( Keys.Down );
			} else if( sender.Equals( _btnKeyLeft ) ) {
				MoveCursor( Keys.Left );
			} else if( sender.Equals( _btnKeyRight ) ) {
				MoveCursor( Keys.Right );
			} else if( sender.Equals( _btnKeyHome ) ) {
				MoveCursor( Keys.Home );
			} else if( sender.Equals( _btnKeyEnd ) ) {
				MoveCursor( Keys.End );
			}
			ApiMonitorTask.monitorMx.ReleaseMutex();
		}
		/// <summary>現在位置よりのカーソル移動</summary>
		/// <param name="input">入力キー</param>
		private void MoveCursor( Keys input )
		{
			int start = _edtInput.SelectionStart;
			switch( input ) {
				case Keys.Down:
					//if( true == _edtInput.Multiline ) {
                        //	複数行対応は保留。
					//
					break;
				case Keys.Up:
					//if( true == _edtInput.Multiline ) {
                        //	複数行対応は保留。
                    //}
					break;
				case Keys.Left:
					if( 0 < start ) {
						--_edtInput.SelectionStart;
					}
					break;
				case Keys.Right:
					if( _edtInput.Text.Length > start ) {
						++_edtInput.SelectionStart;
					}
					break;
				case Keys.Home:
					if( 0 < start ) {
						_edtInput.SelectionStart = 0;
					}
					break;
				case Keys.End:
					if( _edtInput.Text.Length > start ) {
						_edtInput.SelectionStart = _edtInput.Text.Length;
					}
					break;
				default:
					break;
			}
		}

        public void FocusedInputForm()
        {
            _edtInput.Focus();
        }

        public void SetKeyClickEvent(MouseEventHandler _keyEvent)
        {
            _btnKeyUp.MouseDown += _keyEvent;
            _btnKeyDown.MouseDown += _keyEvent;
        }

        /// <summary>文字列操作イベント</summary>
        /// <param name="sender">イベント送信元オブジェクト</param>
        /// <param name="e">イベントパラメータ</param>
        private void Key_Click(object sender, EventArgs e)
        {
			Button target = sender as Button;
			string text = ( sender as ButtonEx ).Text;
			string input;

			if( sender.Equals( _btnKeyLineDelete ) ) {
				if( false == _edtInput.Multiline ) {
					_edtInput.Text = string.Empty;
				} else {
					;	//	複数行対応は保留。
				}
			} else if( sender.Equals( _btnKeyBackSpace ) ) {
				InputRemove( Keys.Back );
			} else if( sender.Equals( _btnKeyDelete ) ) {
				InputRemove( Keys.Delete );
			} else if( sender.Equals( _btnKeyAllClear ) ) {
				_edtInput.Text = string.Empty;
			} else if( sender.Equals( _btnKeyEnter ) ) {
				if( false == _edtInput.Multiline ) {
					_notifyReturn?.Invoke();
				} else {
					InputText( Environment.NewLine );
				}
			} else if( sender.Equals( _btnKeySpace ) ) {
				InputText( " " );
			} else if( true == UIStaticMethods.IsAlphabet( text ) ) {
				//	アルファベット
				if( false == _btnKeyShift.Checked ) {
					input = text.ToUpper();//大文字　※初回こちら
				} else {
					input = text.ToLower();//小文字
				}
				InputText( input );
			} else {
				InputText( text );
			}
		}
		/// <summary>クリップボード操作イベント</summary>
		/// <param name="sender">イベント送信元オブジェクト</param>
		/// <param name="e">イベントパラメータ</param>
		private void _clipboard_Click( object sender, EventArgs e )
		{

			if( sender.Equals( _btnKeyCut ) ) {
				if( 0 < _edtInput.SelectionLength ) {
					Clipboard.SetText( _edtInput.SelectedText );
					InputRemove( Keys.Delete );
				}
			} else if( sender.Equals( _btnKeyCopy ) ) {
				if( 0 < _edtInput.SelectionLength ) {
					Clipboard.SetText( _edtInput.SelectedText );
				}
			} else if( sender.Equals( _btnKeyPaste ) ) {
				if( true == Clipboard.ContainsText() ) {
					string text = Clipboard.GetText();
					InputText( text );
					_edtInput.SelectionStart += text.Length;
				}
			}
		}

		/// <summary>文字入力</summary>
		/// <param name="input">入力文字列</param>
		private void InputText( string input )
		{
			int start = _edtInput.SelectionStart;
			string source = _edtInput.Text;
			while( false == string.IsNullOrEmpty( input ) ) {
				//	その他
				string remove = string.Empty;
				string insert = string.Empty;
				if( 0 < _edtInput.SelectionLength ) {
					//	選択状態にある部分を削除した上で追加。
					remove = source.Remove( start, _edtInput.SelectionLength );
				} else {
					remove = source;
				}
				if( _edtInput.MaxLength > remove.Length ) {
					_edtInput.Text = remove.Insert( start, input );
					_edtInput.Select( start + 1, 0 );
				}
				break;
			}
		}
		/// <summary>文字列の削除</summary>
		/// <param name="input"></param>
		/// <remarks>
		/// カーソル移動を必要とするため、関数を別個にしています。
		/// </remarks>
		private void InputRemove( Keys input )
		{
			int start = _edtInput.SelectionStart;
			string source = _edtInput.Text;
			while( true ) {
				if( true == string.IsNullOrEmpty( source ) ) {
					break;
				}
				string insert = string.Empty;
				if( 0 < _edtInput.SelectionLength ) {
					//	選択状態にある部分を削除して終了
					_edtInput.Text = source.Remove( start, _edtInput.SelectionLength );
					_edtInput.SelectionStart = start;
					break;
				}
				if( Keys.Back == input ) {
					if( 1 > start ) {
						break;  //	左端からの削除はできない。
					}
					_edtInput.Text = source.Remove( start - 1, 1 );
					_edtInput.SelectionStart = start - 1;
				} else if( Keys.Delete == input ) {
					if( ( _edtInput.TextLength - 1 ) < start ) {
						break;  //	右端からの削除はできない。
					}
					_edtInput.Text = source.Remove( start, 1 );
					_edtInput.SelectionStart = start;
				}
				break;
			}
		}

		/// <summary>設定結果取得関数定義</summary>
		public delegate void NotifyTextChangedDelegate( string text );
		/// <summary>終了通知</summary>
		private NotifyTextChangedDelegate _notifyTextChanged = null;
		/// <summary>>設定結果取得</summary>
		public NotifyTextChangedDelegate NotifyTextChanged
		{
			set { _notifyTextChanged = value; }
		}
		/// <summary>表示文字列変化イベント</summary>
		/// <param name="sender">イベント送信元オブジェクト</param>
		/// <param name="e">イベントパラメータ</param>
		private void _edtInput_TextChanged( object sender, EventArgs e )
		{
			if( ( sender as RichTextBox )==null) return;//追加
			_notifyTextChanged?.Invoke( ( sender as RichTextBox ).Text );
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

		/// <summary>設定結果取得関数定義</summary>
		public delegate void NotifyBSDelegate();
		/// <summary>終了通知</summary>
		private NotifyBSDelegate _notifyBS = null;
		/// <summary>>設定結果取得</summary>
		public NotifyBSDelegate NotifyBS
		{
			set { _notifyBS = value; }
		}
		/// <summary>
		/// BSキー：マウスアップ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _btnKeyBackSpace_MouseUp( object sender, MouseEventArgs e )
		{
			if(_notifyBS != null)_notifyBS.Invoke();
		}

		/// <summary>設定結果取得関数定義</summary>
		public delegate void NotifyDELDelegate();
		/// <summary>終了通知</summary>
		private NotifyDELDelegate _notifyDEL = null;
		/// <summary>>設定結果取得</summary>
		public NotifyDELDelegate NotifyDEL
		{
			set { _notifyDEL = value; }
		}
		/// <summary>
		/// DELキー：マウスアップ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _btnKeyDelete_MouseUp( object sender, MouseEventArgs e )
		{
            if (_notifyDEL != null) _notifyDEL.Invoke();
		}
		/// <summary>設定結果取得関数定義</summary>
		public delegate void NotifyUpKeyDelegate();
		/// <summary>終了通知</summary>
		private NotifyUpKeyDelegate _notifyUpKey = null;
		/// <summary>>設定結果取得</summary>
		public NotifyUpKeyDelegate NotifyUpKey
		{
			set { _notifyUpKey = value; }
		}
		/// <summary>
		/// UpKeyキー：マウスアップ
		/// </summary>
		private void _btnKeyUp_MouseUp( object sender, MouseEventArgs e )
		{
            if (_notifyUpKey != null) _notifyUpKey.Invoke();
		}

		/// <summary>設定結果取得関数定義</summary>
		public delegate void NotifyDnKeyDelegate();
		/// <summary>終了通知</summary>
		private NotifyDnKeyDelegate _notifyDnKey = null;
		/// <summary>>設定結果取得</summary>
		public NotifyDnKeyDelegate NotifyDnKey
		{
			set { _notifyDnKey = value; }
		}
		/// <summary>
		/// DnKeyキー：マウスアップ
		/// </summary>
		private void _btnKeyDn_MouseUp( object sender, MouseEventArgs e )
		{
            if (_notifyDnKey != null) _notifyDnKey.Invoke();
		}
		/// <summary>
		/// ESCキー：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ESC_Button_Click( object sender, EventArgs e )
		{
			//入力文字列を戻す
			_edtInput.Text = _edtInputEscBack;
			_notifyReturn?.Invoke();
		}
	}
}
