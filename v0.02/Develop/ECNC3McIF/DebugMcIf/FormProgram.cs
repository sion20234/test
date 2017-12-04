using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ECNC3.Enumeration;
using ECNC3.Models.McIf;

namespace DebugMcIf
{
	/// <summary>プログラム呼び出しフォーム</summary>
	public partial class FormProgram : Form
	{
		/// <summary>コンストラクタ</summary>
		public FormProgram()
		{
			InitializeComponent();
		}
		/// <summary>プログラム呼び出し</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void _btnOpen_Click( object sender, EventArgs e )
		{
			ResultCodes ret = ResultCodes.Success;
			OpenFileDialog of = new OpenFileDialog();
			if( DialogResult.OK == of.ShowDialog( this ) ) {
				//	プログラムの表示を更新
				_lnkFilePath.Text = of.FileName;
				ShowProgram();

				using( McDatProgram mc = new McDatProgram() ) {
					mc.ProgramFilePath = of.FileName;
					mc.BlockNumber = (int)_edtBlockNumber.Value;
					ret = mc.Write();

					//	実行結果の表示
					_stcResult.Text = $"{ret}";
					if( ResultCodes.Success != ret ) {
						//	エラー詳細を反映
						if( ProgramCodeTypes.Main == mc.ErrorCodeType ) {
							_stcErrorCode.Text = $"User program";
						} else if( ProgramCodeTypes.MCode == mc.ErrorCodeType ) {
							_stcErrorCode.Text = $"M{mc.ErrorCodeNumber}";
						} else if( ProgramCodeTypes.GCode == mc.ErrorCodeType ) {
							_stcErrorCode.Text = $"G{mc.ErrorCodeNumber}";
						} else {
							_stcErrorCode.Text = $"????";
						}
						_stcErrorLine.Text = $"{mc.ErrorLineNumber}";

						MessageBox.Show( $"{ret}({(int)ret}){Environment.NewLine}"
							+ $"Type={mc.ErrorCodeType}#{mc.ErrorCodeNumber}{Environment.NewLine}"
							+ $"LINE={mc.ErrorLineNumber}" );
					} else {
						_stcErrorCode.Text = $"-";
						_stcErrorLine.Text = $"-";
					}
				}
			}
		}
		/// <summary>プログラム実行開始コマンド</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void _btnStart_Click( object sender, EventArgs e )
		{
			using( McReqProgramStart mc = new McReqProgramStart() ) {
				//				string source = _edtProgram.Text;
				if( true == _chkSelectNNumber.Checked ) {
					int charpos = _edtProgram.SelectionStart;
					int linepos = _edtProgram.GetLineFromCharIndex( charpos );
					mc.StartNNumber = (short)linepos;
				}
				mc.Execute();
			}
		}
		/// <summary>プログラム実行停止位置への位置決め開始コマンド</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void _btnResume_Click( object sender, EventArgs e )
		{
			using( McReqProgramResume mc = new McReqProgramResume() ) {
				mc.Execute();
			}
		}
		/// <summary>加工プログラムの表示</summary>
		private void ShowProgram()
		{
			string path = _lnkFilePath.Text;
			if( false == string.IsNullOrEmpty( path ) ) {
				if( true == File.Exists( path ) ) {
					FileStream fs = new FileStream( path, FileMode.Open, FileAccess.Read );
					using( TextReader sr = new StreamReader( fs ) ) {
						_edtProgram.Text = sr.ReadToEnd();
						return;
					}
				}
			}
			_edtProgram.Text = string.Empty;
		}
		/// <summary>NC転送済みプログラム取得</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void _btnRead_Click( object sender, EventArgs e )
		{
			using( McDatProgram mc = new McDatProgram() ) {
				mc.ProgramNumber = (int)_edtBlockNumber.Value;
				ResultCodes ret = mc.Read();
				if( ResultCodes.Success != ret ) {
					return;
				}
				if( null != mc.ProgramCode ) {
					_edtProgram.Text = Encoding.ASCII.GetString( mc.ProgramCode );
				}
			}
		}
		/// <summary>プログラム番号変更</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void _edtProgramNumber_ValueChanged( object sender, EventArgs e )
		{
			using( McReqProgramSelect mc = new McReqProgramSelect() ) {
				mc.ProgramNumber = (short)( sender as NumericUpDown ).Value;
				mc.Execute();
			}
		}
	}
}
