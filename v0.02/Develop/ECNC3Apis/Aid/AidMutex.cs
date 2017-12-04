using System;
using System.Globalization;
using System.Threading;

namespace ECNC3.Models.Common
{
	/// <summary>ミューテックス管理</summary>
	/// <remarks>
	/// 排他制御オブジェクトの管理を行います。
	/// </remarks>
	public class AidMutex : IDisposable
	{
		/// <summary>Dispose()関数は呼び出し済みかどうか</summary>
		private bool _disposed = false;
		/// <summary>排他制御</summary>
		private Mutex _mutex = null;
		/// <summary>時間計測</summary>
		private System.Diagnostics.Stopwatch _stopWatch = null;

#if __KEY_FXCOP_CA1811__
		/// <summary>コンストラクタ</summary>
		/// <param name="name">ミューテックス名</param>
		/// <remarks>
		/// コンストラクタ内で排他制御を実行します。
		/// </remarks>
		public AidMutex( string name )
		{
			MutexTimeout = 5000;
			Open( name );
		}
#endif
		/// <summary>コンストラクタ</summary>
		/// <param name="name">ミューテックス名</param>
		/// <param name="timeout">タイムアウト時間(ms)</param>
		/// <remarks>
		/// コンストラクタ内で排他制御を実行します。
		/// </remarks>
		public AidMutex( string name, int timeout = 5000 )
		{
			MutexTimeout = timeout;
			Open( name );
		}
		/// <summary>ミューテックス名</summary>
		private string MutexName { get; set; }
		/// <summary>ミューテックスタイムアウト時間</summary>
		public int MutexTimeout { private get; set; }
#if __KEY_FXCOP_CA1811__
		/// <summary>ミューテックスオープン有無</summary>
		public bool IsOpened { get; private set; }
#endif

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
						Close();
					}
					//	アンマネージリソースの解放 
				}
				_disposed = true;
			} finally {
				;	//base.Dispose( disposing );	//	基底クラスのDispose() を確実にを呼び出す。
			}
		}
		/// <summary>ミーテックスの作成</summary>
		/// <param name="mutexName">ミューテックス名</param>
		/// <returns>
		///		<list type="bullet">
		///			<item>0=成功</item>
		///			<item>-1=引数異常</item>
		///			<item>-2=オブジェクト作成失敗</item>
		///			<item>-3=タイムアウト</item>
		///		</list>
		/// </returns>
		public int Open( string mutexName )
		{
#if __KEY_FXCOP_CA1811__
			IsOpened = false;
#endif
			_stopWatch = new System.Diagnostics.Stopwatch();
			_stopWatch.Start();
			if( true == string.IsNullOrEmpty( mutexName ) ) {
				return -1;
			}
			MutexName = mutexName.Replace( @"\", @"/" );
			string msg = null;
			int ret = -2;
			//	Mutexオブジェクトの作成
			try {
				_mutex = new Mutex( false, MutexName );
				if( false == _mutex.WaitOne( MutexTimeout ) ) {
					Close();
					ret = -3;
				} else {
					ret = 0;
				}
			} catch( System.ObjectDisposedException e ) {
				msg = e.Message;	//	現在のインスタンスは既に破棄されています。
			} catch( System.ArgumentOutOfRangeException e ) {
				msg = e.Message;	//	millisecondsTimeout が -1 以外の負数です。-1 は無制限のタイムアウトを表します。
			} catch( System.Threading.AbandonedMutexException e ) {
				msg = e.Message;	//     スレッドがミューテックスを解放せずに終了したため、待機が完了しました。
			} catch( System.InvalidOperationException e ) {
				msg = e.Message;	//	別のアプリケーション ドメインでは、現在のインスタンスは System.Threading.WaitHandle の透過プロキシです。
			} catch( UnauthorizedAccessException e ) {
				msg = e.Message;
			} catch( System.IO.IOException e ) {
				msg = e.Message;
			} catch( ApplicationException e ) {
				msg = e.Message;
			} catch( ArgumentException e ) {
				msg = e.Message;
			} catch( Exception e ) {
				msg = e.Message;
			}
			if( false == string.IsNullOrEmpty( msg ) ) {
				AidLog logs = new AidLog( "AidMutex.Open" );
				logs.Error( string.Format( CultureInfo.InvariantCulture, "({0})={1}", MutexName, msg ) );
				if( null != _mutex ) {
					_mutex.Dispose();
					_mutex = null;
				}
			}
#if __KEY_FXCOP_CA1811__
			if( 0 == ret ) {
				IsOpened = true;
			}
#endif
			return ret;
		}
		/// <summary>ミーテックスの解放</summary>
		/// <returns>
		///		<list type="bullet">
		///			<item>0=成功</item>
		///			<item>-2=失敗</item>
		///		</list>
		/// </returns>
		public int Close()
		{
			if( null != _stopWatch ) {
				_stopWatch.Stop();
			}
			if( null == _mutex ) {
				return 0;	//	解放不要。
			}
			string msg = null;
			int ret = -2;
			//	Mutexオブジェクトの破棄
			try {
				_mutex.ReleaseMutex();
				_mutex.Close();
				_mutex.Dispose();
				_mutex = null;
			} catch( System.ApplicationException e ) {
				msg = e.Message;	//	呼び出し元のスレッドはミューテックスを所有していません。
			} catch( Exception e ) {
				msg = e.Message;
			}
			if( false == string.IsNullOrEmpty( msg ) ) {
				AidLog logs = new AidLog( "AidMutex.Close" );
				logs.Error( string.Format( CultureInfo.InvariantCulture, "({0})={1}", MutexName, msg ) );
				_mutex.Dispose();
				_mutex = null;
			}
			return ret;
		}
	}
}
