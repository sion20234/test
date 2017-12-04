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
	/// <summary>キーボードダイアログ</summary>
	public partial class KeyboardDialog : ECNC3Form
	{
		/// <summary>コンストラクタ</summary>
		public KeyboardDialog()
		{
			InitializeComponent();
		}

		/// <summary>入力文字列</summary>
		public string InputValue
		{
			get { return _keyBoard.InputValue; }
			set { _keyBoard.InputValue = value; }
		}
		/// <summary>ロード</summary>
		/// <param name="sender">イベント送信元オブジェクト</param>
		/// <param name="e">イベントパラメータ</param>
		private void _keyBoard_Load( object sender, EventArgs e )
		{
			_keyBoard.OperationMode = KeyBord.OperationModes.Popup;
			_keyBoard.NotifyReturn = _edtComment_ClickCallBack;
			_keyBoard.NotifyTextChanged = _edtComment_ClickTextChanged;

			_keyBoard.NotifyBS= BS_Click;
			_keyBoard.NotifyDEL= DEL_Click;

            _keyBoard.NotifyUpKey = UpKey_Click;
            _keyBoard.NotifyDnKey = DnKey_Click;



        }
        private void BS_Click()
		{
			_notifyBS?.Invoke();
		}
        private void DEL_Click()
        {
            _notifyDEL?.Invoke();
        }
        private void UpKey_Click()
        {
            _notifyUpKey?.Invoke();
        }
        private void DnKey_Click()
        {
            _notifyDnKey?.Invoke();
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
			_notifyBS?.Invoke();
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
		private void _btnKeyDELDelete_MouseUp( object sender, MouseEventArgs e )
		{
			_notifyDEL?.Invoke();
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
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _btnKeyUpKey_MouseUp( object sender, MouseEventArgs e )
		{
			_notifyUpKey?.Invoke();
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
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _btnKeyDnKey_MouseUp( object sender, MouseEventArgs e )
		{
			_notifyDnKey?.Invoke();
		}

		/// <summary>
		/// 入力欄フォーカス設定
		/// </summary>
		public void FocusedInputForm()
        {
            _keyBoard.FocusedInputForm();
        }

        public void SetKeyClickEvent(MouseEventHandler _keyEvent)
        {
            _keyBoard.SetKeyClickEvent(_keyEvent);
        }

        /// <summary>文字列変更イベント</summary>
        /// <param name="text"></param>
        private void _edtComment_ClickTextChanged( string text )
		{
			_notifyTextChanged?.Invoke( text );
		}
		/// <summary>終了要求イベント</summary>
		public void _edtComment_ClickCallBack()
		{
			_notifyReturn?.Invoke();
		}
	}
}
