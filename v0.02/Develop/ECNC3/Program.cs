///////////////////////////////////////////////////////////////////////////////////////////
//
// (1) システム名称 : ELENIX ECNCⅢ
// (2) ファイル名   : Program.cs
// (3) 概要         : メインプログラム
// (4) 作成日       : 2016.07.19
// (5) 作成者       : 八野
// (6) 特記事項     : VisualStudio2015 C#
// (7) 更新履歴     :
//
///////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Windows.Forms;
using System.Threading;

namespace ECNC3.Views
{
    /// <summary>メインクラス</summary>
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
            Application.SetCompatibleTextRenderingDefault(false);
            //パス設定ファイル読込み
            using (Models.FilePathInfo pathInfo = new Models.FilePathInfo())
            //カラーテーブル読込み
            using (Models.FileUIStyleTable uiStyle = new Models.FileUIStyleTable())
            //文字列テーブル読込み
            using (Models.FileUIStringTable strTbl = new Models.FileUIStringTable())
            {
                pathInfo.Read();
                uiStyle.Read();
                strTbl.Read();
                //文字列テーブル作成のみに使用
                //Models.FileUIStringTable.StringTablesClear();
                //Models.FileUIStringTable.StringTablesTrimExcess();
                //Models.FileUIStringTable.StringTablesNullSet();
                //Models.FileUIStringTable.StringTablesInit();
            }
#if false
           Application.Run(new MAINForm());
#else
            using (Mutex hMutex = new Mutex( false, Application.ProductName ) ) {
				try {
					// Mutex のシグナルを受信できるかどうか判断する
					if( true == hMutex.WaitOne( 0, false ) ) {
						Application.Run( new MAINForm() );
					} else {
						MessageBox.Show( "Already been started.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning );
					}
					// GC.KeepAlive メソッドが呼び出されるまで、ガベージ コレクション対象から除外される
					GC.KeepAlive( hMutex );
				} catch( Exception e ) {
					if( ( e is ObjectDisposedException ) ||
						( e is ArgumentOutOfRangeException ) ||
						( e is AbandonedMutexException ) ||
						( e is InvalidOperationException ) ) {
					}
				} finally {
					// Mutex を閉じる (正しくは オブジェクトの破棄を保証する を参照)
					hMutex.Close();
				}
			}
#endif
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
		/// <param name="e">例外オブジェクト</param>
		/// <param name="title">タイトル</param>
		private static void ShowError( Exception exception, string title )
		{
			ECNC3.Models.Common.ECNC3Log logs = new Models.Common.ECNC3Log( title );
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
									+ $"{e.Message}",
									"Fail to Application start.", MessageBoxButtons.OK, MessageBoxIcon.Error );
				}
			}
			Application.Exit();
		}
    }
}
