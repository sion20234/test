using System;
using System.IO;
using System.Windows.Forms;//MessageBox
using ECNC3.Enumeration;

namespace ECNC3.Models.Common
{
	/// <summary>ECNC3アップデートログ出力</summary>
	/// <code>
	/// ECNC3UpdateLog logs = new ECNC3UpdateLog( "クラス名.メソッド名" );
	/// logs.Error( "ログ出力内容" );
	/// </code>
	public class ECNC3UpdateLog
	{
		/// <summary>ログファイルの拡張子</summary>
		private const string LogFileExtension = ".log";
		/// <summary>ログファイル保存先ディレクトリパス</summary>
		private string SaveDirectory
		{
			get
			{
				using( ECNC3Settings fs = new ECNC3Settings() ) {
					return fs.DirectoryLog;
				}
			}
		}
		/// <summary>出力ファイル名</summary>
		private string OutputFileName
		{
			get { return $"{DateTime.Now.ToString( "yyyyMMddhhmmss", System.Globalization.CultureInfo.InvariantCulture )+ _strVer + "Update"}{LogFileExtension}"; }
		}
		/// <summary>出力ファイルパス</summary>
		public string OutputFilePath
		{
			get { return @FilePathInfo.ECNC3PATH + _logName; }
//			get { return Path.Combine( SaveDirectory, OutputFileName ); }
		}
		/// <summary>関数名</summary>
		protected string FunctionName { get; set; }

		string _strVer = "";

		/// <summary>コンストラクタ</summary>
		/// <param name="functionName">関数名
		/// 「クラス名.メソッド名」の書式で設定する。
		/// </param>
		/// <remarks>
		/// StackTraceを使用しないのはコストを抑えるため。
		/// </remarks>
		public ECNC3UpdateLog()
		{
		}
		/// <summary>
		/// 新バージョン番号表示
		/// </summary>
		/// <param name="strNewVer"></param>
		public void SetLogNewVersion( string strNewVer )
		{
			if( strNewVer.IndexOf( "Ver." )< 0 ) strNewVer = "Ver." + strNewVer;
			_strVer = strNewVer;
		}
		/// <summary>正常ログ</summary>
		/// <param name="note">出力内容</param>
		/// <remarks>
		/// 正常系であっても必ず出力したいログ出力
		/// </remarks>
		public void Sure( int logType, string note )
		{
			Output( logType, note );
		}
		/// <summary>
		/// 初回ログ時間
		/// </summary>
		private string _firstTime = "";
		/// <summary>
		/// ログファイル名
		/// </summary>
		private string _logName = @"Log\Ecnc3Update.log";
		/// <summary>
		/// 初回ログパス
		/// </summary>
		//private string _firstOutputFilePath = "";
		/// <summary>ログ出力</summary>
		/// <param name="msg">取得される文字列</param>
		private string Output(int logType, string msg )
		{
			StreamWriter sw = null;
			//ログ出力文字列作成
			string output = "";
			try {
				//if( logType == 0 ) _firstOutputFilePath = OutputFilePath;//初回パス
				output = DateTime.Now.ToString( "yyyy/MM/dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture ) + "," + msg;
				try {
					//	ログ出力
					sw = new StreamWriter( @FilePathInfo.ECNC3PATH + _logName, true, System.Text.Encoding.GetEncoding( "UTF-8" ) );
//個別ログ用		sw = new StreamWriter( _firstOutputFilePath, true, System.Text.Encoding.GetEncoding( "UTF-8" ) );
					if( null != sw ) {
						sw.WriteLine( output );
					}
				} catch( Exception exp ) {//例外発生時
					MessageBox.Show(exp.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
				} finally {
					//System.Diagnostics.Debug.WriteLine( output );
					if( sw != null ) {
						sw.Close();
					}
				}
			} catch {
				;
			}
			return _firstTime;
		}
		/// <summary>初期化</summary>
		/// <remarks>
		/// 出力されたログが設定された保持数以上である場合、削除します。
		/// </remarks>
		public void Initialize()
		{
		//	FunctionName = "ECNC3UpdateLog.Initialize";
		//	int saveCount = 5;
		//	using( ECNC3Settings cmn = new ECNC3Settings() ) {
		//		saveCount = cmn.LogSaveCount;
		////		Sure( $"Lv={cmn.LogLevel}/Count={cmn.LogSaveCount}" );
		//		FileAccessCommon fa = new FileAccessCommon();
		//		fa.BackupStandBy( cmn.DirectoryLog, saveCount, $"????????{LogFileExtension}" );
		//	}
		}
	}
}
