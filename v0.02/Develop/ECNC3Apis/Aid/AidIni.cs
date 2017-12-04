using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ECNC3.Models.Common
{
	/// <summary>アンマネージ コード アクセス定義</summary>
	public abstract class NativeMethods
	{
		[DllImport( "KERNEL32.DLL", CharSet = CharSet.Unicode )]
		internal static extern uint GetPrivateProfileString( string appName, string keyName, string defaultValue, StringBuilder returnedString, uint size, string fileName );
		[DllImport( "KERNEL32.DLL", CharSet = CharSet.Unicode )]
		internal static extern uint WritePrivateProfileString( string appName, string keyName, string target, string fileName );
	}

	/// <summary>INIファイル解析</summary>
	public class AidIni : IDisposable
	{
		/// <summary>読み取り対象のファイルパス</summary>
		private string _path = string.Empty;
		/// <summary>Dispose()関数は呼び出し済みかどうか</summary>
		private bool _disposed = false;

		/// <summary>コンストラクタ</summary>
		/// <param name="path">読み取りファイルパス</param>
		public AidIni( string path )
		{
			_path = path;
		}
		/// <summary>インスタンス破棄</summary>
		public void Dispose()
		{
			Dispose( true );
			GC.SuppressFinalize( this );
		}
		/// <summary>インスタンスの破棄</summary>
		/// <param name="disposing">Dispose()実行の是非
		///		<list type="bullet">
		///			<item>true=Dispose()関数よりの呼び出し。</item>
		///			<item>false=ファイナライザによる呼び出し。</item>
		///		</list>
		/// </param>
		protected virtual void Dispose( bool disposing )
		{
			try {
				if( false == _disposed ) {
					if( true == disposing ) {
						//	マネージリソースの解放
					}
					//	アンマネージリソースの解放 
				}
				_disposed = true;
			} finally {
				;   //base.Dispose( disposing );	//	基底クラスのDispose() を確実にを呼び出す。
			}
		}
		/// <summary>文字列の取得</summary>
		/// <param name="app">セクション名</param>
		/// <param name="key">キー名</param>
		/// <param name="def">初期値</param>
		/// <returns>取得結果</returns>
		public string GetText( string app, string key, string def )
		{
			if( false == string.IsNullOrEmpty( _path ) ) {
				StringBuilder sb = new StringBuilder( 256 );
				NativeMethods.GetPrivateProfileString( app, key, def, sb, 256, _path );
				return sb.ToString();
			}
			return def;
		}
		/// <summary>整数値の取得</summary>
		/// <param name="app">セクション名</param>
		/// <param name="key">キー名</param>
		/// <param name="def">初期値</param>
		/// <returns>取得結果</returns>
		/// <remarks>
		/// 引数により指示されたINIファイルの情報を取得します。
		/// 設定された値に「＆Ｈ」の記述があった場合、HEX値であると判断します。
		/// </remarks>
		public int GetValue( string app, string key, int def )
		{
			while( false == string.IsNullOrEmpty( _path ) ) {
				StringBuilder sb = new StringBuilder( 256 );
				string text = GetText( app, key, $"{def}" );
				AidConvert fmt = new AidConvert();
				int temp = 0;
				if( false == fmt.TryParse( text, out temp ) ) {
					break;
				}
				return temp;
			}
			return def;
		}
		/// <summary>整数値の取得</summary>
		/// <param name="app">セクション名</param>
		/// <param name="key">キー名</param>
		/// <param name="def">初期値</param>
		/// <returns>取得結果</returns>
		/// <remarks>
		/// 引数により指示されたINIファイルの情報を取得します。
		/// 設定された値に「＆Ｈ」の記述があった場合、HEX値であると判断します。
		/// </remarks>
		public long GetLong( string app, string key, long def )
		{
			while( false == string.IsNullOrEmpty( _path ) ) {
				StringBuilder sb = new StringBuilder( 256 );
				string text = GetText( app, key, $"{def}" );
				AidConvert fmt = new AidConvert();
				long temp = 0;
				if( false == fmt.TryParse( text, out temp ) ) {
					break;
				}
				return temp;
			}
			return def;
		}
		/// <summary>16進数値によるINIファイル書き込み</summary>
		/// <param name="app">セクション名</param>
		/// <param name="key">キー名</param>
		/// <param name="target">設定値</param>
		/// <remarks>
		/// 引数 target を16進数の書式で保存します。
		/// </remarks>
		public void SetHex( string app, string key, long target )
		{
			string text = string.Format( "0x{0:x}", target );
			NativeMethods.WritePrivateProfileString( app, key, text, _path );
		}

		/// <summary>bool値によるINIファイル書き込み</summary>
		/// <param name="app">セクション名</param>
		/// <param name="key">キー名</param>
		/// <param name="def">設定値</param>
		/// <returns>取得結果</returns>
		public bool GetBool( string app, string key, bool def )
		{
			return ( 0 != GetValue( app, key, ( true == def ) ? 1 : 0 ) ) ? true : false;
		}
		/// <summary>INIファイル書き込み</summary>
		/// <param name="app">セクション名</param>
		/// <param name="key">キー名</param>
		/// <param name="def">設定値</param>
		/// <returns>取得結果</returns>
		public double GetDouble( string app, string key, double def )
		{
			double result = 0;
			string text = GetText( app, key, $"{def:F3}" );
			if( false == string.IsNullOrEmpty( text ) ) {
				if( false == double.TryParse( text, out result ) ) {
					int temp = 0;
					if( false == int.TryParse( text, out temp ) ) {
						result = 0.0;
					} else {
						result = temp;
					}
				}
			}
			return result;
		}
	}
}
