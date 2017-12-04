using System;
using System.Threading;
using System.Windows.Forms;

namespace DebugMcIf
{
	/// <summary>エントリーポイント</summary>
	public static class Program
	{
		/// <summary>アプリケーションのメイン エントリ ポイントです。</summary>
		[STAThread]
		public static void Main()
		{
			//例外の取りこぼし対策としてイベントハンドラを登録
			Application.ThreadException += Application_ThreadException;
			Thread.GetDomain().UnhandledException += Program_UnhandledException;

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault( false );
			Application.Run( new FormMain() );
		}
		/// <summary>トラップされないスレッドの例外がスローされると、発生するイベント</summary>
		/// <param name="sender">イベント送信元オブジェクト</param>
		/// <param name="e">イベントパラメータ</param>
		private static void Application_ThreadException( object sender, ThreadExceptionEventArgs e )
		{
			ShowError( e.Exception as Exception, "Application_ThreadException" );
		}

		/// <summary>例外がキャッチされない場合に発生するイベント</summary>
		/// <param name="sender">イベント送信元オブジェクト</param>
		/// <param name="e">イベントパラメータ</param>
		private static void Program_UnhandledException( object sender, UnhandledExceptionEventArgs e )
		{
			ShowError( e.ExceptionObject as Exception, "Application_ThreadException" );
		}
		/// <summary>エラー通知</summary>
		/// <param name="exception">例外オブジェクト</param>
		/// <param name="title">タイトル</param>
		private static void ShowError( Exception exception, string title )
		{
			ECNC3.Models.Common.ECNC3Log logs = new ECNC3.Models.Common.ECNC3Log( title );
			if( exception != null ) {
				logs.Exception( exception );
			}
			string path = logs.OutputFilePath;
			string time = DateTime.Now.ToString( "yyyy/MM/dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture );
			DialogResult ret = MessageBox.Show( "Detected an exception that was not supplemented in the ECNC3 application." + Environment.NewLine
											+ "For more information check the error log." + Environment.NewLine
											+ $"({path})" + Environment.NewLine
											+ Environment.NewLine
											+ "Quit ECNC3 application." + Environment.NewLine
											+ Environment.NewLine
											+ "Do you want to see the application log file?",
											$"{title}({time})", MessageBoxButtons.YesNo, MessageBoxIcon.Error );
			if( DialogResult.Yes == ret ) {
				System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
				try {
					psi.FileName = @"notepad.exe";
					psi.Arguments = path;
					System.Diagnostics.Process.Start( psi );
				} catch( Exception e ) {
					logs.Exception( e );
					MessageBox.Show( $"{psi.Arguments}" + Environment.NewLine
									+ Environment.NewLine
									+ $"{e.Message}"
									+ Environment.NewLine
									+ $"{exception.Message}",
									"Fail to Application start.", MessageBoxButtons.OK, MessageBoxIcon.Error );
				}
			}
			Application.Exit();
		}
	}
}
