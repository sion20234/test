///////////////////////////////////////////////////////////////////////////////////////////
//
// (1) システム名称 : ELENIX ECNCⅢ
// (2) ファイル名   : InfomationSelectForm.cs
// (3) 概要         : 確認メッセージ画面
// (4) 作成日       : 2015.06.01
// (5) 作成者       : 八野
// (6) 特記事項     : VisualStudio2015 C#
// (7) 更新履歴     :
//
///////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Drawing;
using System.Windows.Forms;
using ECNC3.Enumeration;
using ECNC3.Models;
using ECNC3.Models.Common;

namespace ECNC3.Views
{
	/// <summary>オペレータメッセージ通知画面</summary>
	public partial class MessageDialog : ECNC3Form
	{
		/// <summary>コンストラクタ</summary>
		public MessageDialog()
		{
			InitializeComponent();
			Disposed += MessageDialog_Disposed;
            this.TopMost = true;
        }
        /// <summary>コンストラクタ</summary>
		public MessageDialog(ButtonEx parentButton)
        {
            InitializeComponent();
            
            Disposed += MessageDialog_Disposed;
            base.targetBtn = parentButton;
            this.TopMost = true;
        }

        /// <summary>破棄イベント</summary>
        /// <param name="sender">イベント送信元オブジェクト</param>
        /// <param name="e">イベントパラメータ</param>
        private void MessageDialog_Disposed( object sender, EventArgs e )
		{
			if( null != Item ) {
				Item.Dispose();
				Item = null;
			}
		}

		/// <summary>呼び出しの目的</summary>
		public MessageBoxIcon Subject { get; set; } = MessageBoxIcon.Information;
		/// <summary>ボタン配置</summary>
		private MessageBoxButtons ButtonTypes { get; set; } = MessageBoxButtons.OK;
		/// <summary>メッセージID</summary>
		private int MessageId { get; set; } = -1;
        /// <summary>メッセージ情報</summary>
        public StructureOperatorMessageItem Item = null;

		/// <summary>補足情報</summary>
		public string Note { get; set; }
		/// <summary>ロードイベント</summary>
		/// <param name="sender">イベント送信元オブジェクト</param>
		/// <param name="e">イベントパラメータ</param>
		private void InfomationSelectForm_Load( object sender, EventArgs e )
		{
			UpdateData( MessageId );
            
		}
		/// <summary>表示更新</summary>
		/// <remarks>
		/// 設定されたプロパティにより再設定をおこないます。
		/// </remarks>
		public void UpdateData( int msgId = -1 )
		{
			if( MessageBoxIcon.Question == Subject ) {
				ButtonTypes = MessageBoxButtons.YesNo;
				SetBackColor( Color.SkyBlue );
				_picIcon.IconType = PictureBoxEx.IconTypes.Help;
			} else if( MessageBoxIcon.Exclamation == Subject ) {
				ButtonTypes = MessageBoxButtons.YesNo;
				SetBackColor( Color.Khaki );
				_picIcon.IconType = PictureBoxEx.IconTypes.Warning;
            }else if (MessageBoxIcon.Warning == Subject) {
                ButtonTypes = MessageBoxButtons.OK;
                SetBackColor(Color.Khaki);
                _picIcon.IconType = PictureBoxEx.IconTypes.Warning;
            } else if( ( MessageBoxIcon.Error == Subject ) || ( MessageBoxIcon.Exclamation == Subject ) ) {
				ButtonTypes = MessageBoxButtons.OK;
				SetBackColor( Color.IndianRed );
				_picIcon.IconType = PictureBoxEx.IconTypes.CriticalError;
			} else {
				ButtonTypes = MessageBoxButtons.OK;
				SetBackColor( Color.SkyBlue );
				_picIcon.IconType = PictureBoxEx.IconTypes.Information;
			}
			if( MessageBoxButtons.YesNo == ButtonTypes ) {
				_btnOK.Visible = false;
                _btnYes.Visible = true;
                _btnNon.Visible = true;
            }
            else {
				_btnYes.Visible = false;
				_btnNon.Visible = false;
                _btnOK.Visible = true;
				_btnOK.Location = new Point( 486, 357 );
			}
			if( 0 > msgId ) {
				;
			} else {
				ReadMassage( msgId );
			}

			if( null != Item ) {
				_stcNumber.Text = $"{Item.Number}";
				_stcTitle.Text = Item.Title;
				if( true == string.IsNullOrEmpty( Note ) ) {
					_edtText.Text = Item.Text;
				} else {
					_edtText.Text = Item.Text + Environment.NewLine + Note;
				}
                //表示メッセージに上と下に追加メッセージを表示
                _edtText.Text = _topAddMes + _edtText.Text + _botomAddMes;

                ECNC3Log logs = new ECNC3Log();
				string msg = _edtText.Text.Replace( "\n", " " );
				logs.Operate( $"MSG,{Subject},{_stcNumber.Text},{_stcTitle.Text},{msg}" );
			}
            ReShown();
		}
        
		/// <summary>背景色の変更</summary>
		/// <param name="target"></param>
		private void SetBackColor( Color target )
		{
			if( true == BackColor.Equals( target ) ) {
                OutLineColor = BackColor;
                return;
			}
			OutLineColor = BackColor = target;
		}
		/// <summary>メッセージ情報のプロパティへの設定</summary>
		/// <param name="msgId"></param>
		/// <returns></returns>
		private bool ReadMassage( int msgId )
		{
			while( true ) {
				if( null != Item ) {
					if( Item.Number == msgId ) {
						break;
					}
				}
				using( FileOperatorMessage fa = new FileOperatorMessage() ) {
					fa.Read();
					int index = fa.Items.FindIndex( ( x ) => x.Number == msgId );
					if( 0 > index ) {
						break;
					}
					if( null == Item ) {
						Item = fa.Items[index].Clone() as StructureOperatorMessageItem;
					} else {
						Item.Copy( fa.Items[index] );
					}
				}
				return true;
			}
			return false;
		}
		/// <summary>【はい】ボタンクリックイベント</summary>
		/// <param name="sender">イベント送信元オブジェクト</param>
		/// <param name="e">イベントパラメータ</param>
		private void _btnYes_Click( object sender, EventArgs e )
		{
			DialogResult = DialogResult.Yes;
			this.Close();
		}

		/// <summary>【いいえ】ボタンクリックイベント</summary>
		/// <param name="sender">イベント送信元オブジェクト</param>
		/// <param name="e">イベントパラメータ</param>
		private void _btnNon_Click( object sender, EventArgs e )
		{
			DialogResult = DialogResult.No;
			this.Close();
		}

		/// <summary>【OK】ボタンクリックイベント</summary>
		/// <param name="sender">イベント送信元オブジェクト</param>
		/// <param name="e">イベントパラメータ</param>
		private void _btnOK_Click( object sender, EventArgs e )
		{
			DialogResult = DialogResult.OK;
			this.Close();
		}

		/// <summary>通知メッセージ表示</summary>
		/// <param name="msgId">メッセージID</param>
		/// <param name="owner">関連付けられる親ハンドル</param>
		public void Information( int msgId, IWin32Window owner )
		{
			MessageId = msgId;
			Subject = MessageBoxIcon.Information;
			ShowDialog( owner );
		}
		/// <summary>警告メッセージ表示</summary>
		/// <param name="msgId">メッセージID</param>
		/// <param name="owner">関連付けられる親ハンドル</param>
		public void Warning( int msgId, IWin32Window owner )
		{
			MessageId = msgId;
			Subject = MessageBoxIcon.Warning;

            ShowDialog( owner );
		}
        /// <summary>警告メッセージ表示</summary>
		/// <param name="msgId">メッセージID</param>
		/// <param name="owner">関連付けられる親ハンドル</param>
		public bool WarningYesNo(int msgId, IWin32Window owner)
        {
            MessageId = msgId;
            Subject = MessageBoxIcon.Exclamation;
            return DialogResult.Yes == ShowDialog(owner) ? true : false;
        }
        /// <summary>異常メッセージ表示</summary>
        /// <param name="msgId">メッセージID</param>
        /// <param name="owner">関連付けられる親ハンドル</param>
        public void Error( int msgId, IWin32Window owner )
		{
			MessageId = msgId;
			Subject = MessageBoxIcon.Error;
			ShowDialog( owner );
		}
        private string _topAddMes;      //上追加メッセージ
        private string _botomAddMes;    //下追加メッセージ
        /// <summary>確認メッセージ表示</summary>
        /// <param name="msgId">メッセージID</param>
        /// <param name="owner">関連付けられる親ハンドル</param>
        /// <param name="topAddMes">上追加メッセージ</param>
        /// <param name="botomAddMes">下追加メッセージ</param>
        /// 
        public bool Question( int msgId, IWin32Window owner, string topAddMes="", string botomAddMes="")
		{
            _topAddMes = topAddMes;
            _botomAddMes = botomAddMes;
            MessageId = msgId;
			Subject = MessageBoxIcon.Question;
			return DialogResult.Yes == ShowDialog( owner ) ? true : false;
		}

		/// <summary>内部エラーコード変換による異常表示</summary>
		/// <param name="code">エラーコード</param>
		/// <param name="owner">関連付けられる親ハンドル</param>
		public void Error( ResultCodes code, IWin32Window owner )
		{
			SetInnerErrorCode( code );
			Error( -1, owner );
		}
		/// <summary>内部エラーコード変換による警告表示</summary>
		/// <param name="code">エラーコード</param>
		/// <param name="owner">関連付けられる親ハンドル</param>
		public void Warning( ResultCodes code, IWin32Window owner )
		{
			SetInnerErrorCode( code );
			Warning( -1, owner );
		}
        /// <summary>ステータスメッセージ表示</summary>
        /// <param name="msgId">メッセージID</param>
        /// <param name="owner">関連付けられる親ハンドル</param>
        public void StatusMessage(int msgId, IWin32Window owner)
        {
            MessageId = msgId;
            Subject = MessageBoxIcon.Information;
            Show(owner);
        }
        /// <summary>内部エラーコードによる変換を行います。</summary>
        /// <param name="code">エラーコード</param>
        private void SetInnerErrorCode( ResultCodes code )
		{
			int msgId = 0;
			switch( code ) {
				case ResultCodes.AlreadyRegistered:
					ReadMassage( 5003 );
					break;
				case ResultCodes.NotFound:
					ReadMassage( 5004 );
					break;
				case ResultCodes.InvalidArgument:
					ReadMassage( 5005 );
					break;
				case ResultCodes.NotSupported:
					ReadMassage( 5006 );
					break;
				case ResultCodes.NotExecute:
					ReadMassage( 5007 );
					break;
				case ResultCodes.OutOfRange:
					ReadMassage( 5008 );
					break;
				case ResultCodes.FailToReadFile:
					ReadMassage( 5009 );
					break;
				case ResultCodes.FailToWriteFile:
					ReadMassage( 5010 );
					break;
				case ResultCodes.FailToEncryption:
					ReadMassage( 5011 );
					break;
				case ResultCodes.FailToDecryption:
					ReadMassage( 5012 );
					break;
				case ResultCodes.LackOfPreparation:
					ReadMassage( 5013 );
					break;
				case ResultCodes.McNotInitialize:
					ReadMassage( 5014 );
					break;
				case ResultCodes.ExceptionFromWindows:
					ReadMassage( 5015 );
					break;
				#region モーションコントローラI/Fエラー
				case ResultCodes.McCommErrorNotInitialize:
					ReadMassage( 4001 );
					break;
				case ResultCodes.McCommErrorInvalidParameter:
					ReadMassage( 4002 );
					break;
				case ResultCodes.McCommErrorTimeout:
					ReadMassage( 4003 );
					break;
				case ResultCodes.McCommErrorRetryOver:
					ReadMassage( 4004 );
					break;
				case ResultCodes.McCommErrorMultipleRetry:
					ReadMassage( 4005 );
					break;
				case ResultCodes.McCommErrorHardware:
					ReadMassage( 4006 );
					break;
				case ResultCodes.McCommErrorNotRequest:
					ReadMassage( 4007 );
					break;
				case ResultCodes.McCommErrorUnwritable:
					ReadMassage( 4008 );
					break;
				case ResultCodes.McCommErrorFormat:
					ReadMassage( 4009 );
					break;
				case ResultCodes.McCommErrorProgramWriteAbort:
					ReadMassage( 4010 );
					break;
				case ResultCodes.McCommErrorBufferoverflow:
					ReadMassage( 4011 );
					break;
				case ResultCodes.McCommErrorNotBeExecuted:
					ReadMassage( 4012 );
					break;
				case ResultCodes.McCommErrorEmptyHandle:
					ReadMassage( 4013 );
					break;
				case ResultCodes.McCommErrorInvalidHandle:
					ReadMassage( 4014 );
					break;
				case ResultCodes.McCommErrorBusy:
					ReadMassage( 4015 );
					break;
				case ResultCodes.McCommErrorWriteParameter:
					ReadMassage( 4016 );
					break;
				case ResultCodes.McCommErrorProgramStopPosition:
					ReadMassage( 4017 );
					break;
				case ResultCodes.McCommErrorUnknown:
					ReadMassage( 4000 );
					break;
				#endregion
				#region プログラム変換I/Fエラー
				case ResultCodes.McGcdErrorFile:
					ReadMassage( 4101 );
					break;
				case ResultCodes.McGcdErrorBufferOverflow:
					ReadMassage( 4102 );
					break;
				case ResultCodes.McGcdErrorFileFormat:
					ReadMassage( 4103 );
					break;
				case ResultCodes.McGcdErrorConvertion:
					ReadMassage( 4104 );
					break;
				case ResultCodes.McGcdErrorWorkMemoryOverflow:
					ReadMassage( 4105 );
					break;
				case ResultCodes.McGcdErrorNotInitialize:
					ReadMassage( 4106 );
					break;
				case ResultCodes.McGcdErrorUserCodeCirculateCall:
					ReadMassage( 4107 );
					break;
				case ResultCodes.McGcdErrorUndefinedCodeSpecified:
					ReadMassage( 4108 );
					break;
				case ResultCodes.McGcdErrorUserCodeInvalidSpecified:
					ReadMassage( 4109 );
					break;
				case ResultCodes.McGcdErrorUnknown:
					ReadMassage( 4100 );
					break;
				#endregion
				default:
					if( null == Item ) {
						Item = new StructureOperatorMessageItem();
					}
					msgId = 80000 + (int)code;
					Item.Number = msgId;
					Item.Title = "INNER ERROR CODE";
					Item.Text = $"{code}";
					break;
			}
		}

		private void _edtText_TextChanged( object sender, EventArgs e )
		{

		}

		private void label2_Click( object sender, EventArgs e )
		{

		}

		private void _stcTitle_Click( object sender, EventArgs e )
		{

		}

		private void _stcNumber_Click( object sender, EventArgs e )
		{

		}

		private void _picIcon_Click( object sender, EventArgs e )
		{

		}

		private void panelEx1_Paint( object sender, PaintEventArgs e )
		{

		}
	}
}
