using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ECNC3.Enumeration;
using ECNC3.Models.Common;
using static Rt64ecdata;

namespace ECNC3.Models.McIf
{
	/// <summary>MC通信インターフェース基底クラス</summary>
	public class McCommBasic : McIfBasic, IDisposable
	{
		/// <summary>パラメータなし</summary>
		public readonly int NonParameter = 0;
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>コンストラクタ</summary>
		public McCommBasic()
		{
		}
		/// <summary>テクノボード関数</summary>
		public enum TechnoMethods
		{
			/// <summary>初期化(接続)</summary>
			InitCommProc,
			/// <summary>終了(切断)</summary>
			QuitCommProc,
			/// <summary>データ送信</summary>
			SendData,
			/// <summary>データ受信</summary>
			ReceiveData,
			/// <summary>コマンド発行</summary>
			SendCommand,
		}

		/// <summary>通信ハンドル</summary>
		protected int CommHandle
		{
			get; private set;
		}
		/// <summary>データタイプ</summary>
		public short DataType { get; protected set; }

		/// <summary>データタイプ名称</summary>
		/// <remarks>
		/// McDataType プロパティの定義名称です。
		/// </remarks>
		public string DataTypeName { get; protected set; }

		/// <summary>タスク</summary>
		public short Task { get; set; }

		/// <summary>通信リトライ回数</summary>
		public int CommRetryCount { get; set; } = 5;
		/// <summary>通信回数</summary>
		public int CommExecuteCount { get; set; } = 0;
		/// <summary>通信リトライ周期</summary>
		public int CommRetrySpan { get; set; } = 100;

		/// <summary>インスタンスの破棄</summary>
		public new void Dispose()
		{
			Dispose( true );
			GC.SuppressFinalize( this );    //  ファイナライザによるDispose()呼び出しの抑制。
		}

		/// <summary>インスタンスの破棄</summary>
		/// <param name="disposing">呼び出し元の判別
		///     <list type="bullet" >
		///         <item>true=Dispose()関数からの呼び出し。</item>
		///         <item>false=ファイナライザによる呼び出し。</item>
		///     </list>
		/// </param>
		protected override void Dispose( bool disposing )
		{
			try {
				if( false == _disposed ) {
					if( true == disposing ) {
						//  マネージリソースの解放
					}
					//  アンマネージリソースの解放
				}
				_disposed = true;
			} finally {
				//  基底クラスのDispose()を確実に呼び出す。
				base.Dispose( disposing );
			}
		}

		/// <summary>関数呼び出し準備</summary>
		/// <returns>
		///		<item>true=準備完了</item>
		///		<item>false=準備未完</item>
		/// </returns>
		/// <remarks>
		/// MCRT64EC 提供関数呼び出しの準備を行います。
		/// </remarks>
		protected new bool StandBy()
		{
			CommExecuteCount = 0;
			using( ECNC3Settings es = new ECNC3Settings() ) {
				while( null != es ) {
					if( false == es.WasMcInitialzed ) {
						break;
					}
					//	通信ハンドルを設定
					CommHandle = es.CommHandle;
					return base.StandBy();
				}
			}
			return false;
		}
		/// <summary>通信リトライ可否判定</summary>
		/// <returns>
		///		<list type="bullet" >
		///			<item>true=リトライ可</item>
		///			<item>false=リトライ不可</item>
		///		</list>
		/// </returns>
		/// <remarks>
		///	リトライ回数のカウンタは、StandByメソッドで初期化されます。
		/// </remarks>
		protected bool CanCommContinue()
		{
			if( 0 < CommRetryCount ) {
				if( CommExecuteCount < CommRetryCount ) {
					++CommExecuteCount;
					AidLog logs = new AidLog( "McCommBasic.CanContinue" );
					logs.Error( $"Retry={CommExecuteCount}/{CommRetryCount}.Span={CommRetrySpan}" );
					System.Threading.Thread.Sleep( CommRetrySpan );
					return true;
				}
			}
			return false;
		}
		/// <summary>実行</summary>
		/// <param name="target">設定値</param>
		/// <param name="element">XMLファイル保持 要素名</param>
		/// <param name="attr">XMLファイル保持 属性名</param>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// ENABLE 構造体を利用するMCボード転送の集約です。
		/// 引数 element, attr の両方に対して値が設定されている場合は、設定ファイルに対する永続化もあわせて行います。
		/// </remarks>
		protected ResultCodes ExecuteByEnable( bool target, string element, string attr )
		{
			// StackFrameクラスをインスタンス化する
			StackFrame stack = new StackFrame( 1 );
			AidLog logs = new AidLog( $"{stack.GetMethod().ReflectedType.Name}.{stack.GetMethod().Name}" );
			ResultCodes ret = ResultCodes.McNotInitialize;
			while( true == StandBy() ) {
				try {
					ENABLE data = ENABLE.Init();
					data.enable = (short)( ( true == target ) ? 1 : 0 );
					int retRt64 = 0;
					TechnoMethods method = TechnoMethods.SendCommand;
					logs.Sure( $"SendCommand({DataTypeName},enable={data.enable})" );
					if( true == BootMode.HasFlag( BootModes.Machine ) ) {
						retRt64 = Rt64eccomapi.SendCommand( CommHandle, DataType, Task, ref data );
						ret = CheckResultTechno( method, retRt64 );
						if( ResultCodes.Success != ret ) {
							break;
						}
					}
					if( true == BootMode.HasFlag( BootModes.Desktop ) ) {
						retRt64 = Rt64eccomapiWrap.SendCommand( CommHandle, DataType, Task, ref data );
						ret = CheckResultDebug( method, retRt64 );
						if( ResultCodes.Success != ret ) {
							break;
						}
					}
					if( ( false == string.IsNullOrEmpty( element ) ) && ( false == string.IsNullOrEmpty( attr ) ) ) {
						//	転送処理が成功した場合、保持値を更新する。
						using( FileSettings fs = new FileSettings() ) {
							fs.Read();
							fs.WriteAttr( element, attr, ( true == target ) ? "1" : "0" );
							fs.Write();
						}
					}
				} catch( Exception e ) {
					bool unexpected = true;
					if( ( e is DllNotFoundException ) ||
						( e is EntryPointNotFoundException ) ) {
						unexpected = false;   //	想定内の例外。
					}
					ret = logs.Exception( e, unexpected );
				}
				break;
			}
			return ret;
		}
		/// <summary>実行</summary>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// コマンドに引数を必要としない。MCボード転送の集約です。
		/// </remarks>
		protected ResultCodes ExecuteByNonParam()
		{
			StackFrame stack = new StackFrame( 1 );
			AidLog logs = new AidLog( $"{stack.GetMethod().ReflectedType.Name}.{stack.GetMethod().Name}" );
			ResultCodes ret = ResultCodes.McNotInitialize;
			while( true == StandBy() ) {
				try {
					int retRt64 = 0;
					TechnoMethods method = TechnoMethods.SendCommand;
					logs.Sure( $"SendCommand({DataTypeName})" );
					if( true == BootMode.HasFlag( BootModes.Machine ) ) {
						retRt64 = Rt64eccomapi.SendCommand( CommHandle, DataType, Task, IntPtr.Zero );
						ret = CheckResultTechno( method, retRt64 );
						if( ResultCodes.Success != ret ) {
							break;
						}
					}
					if( true == BootMode.HasFlag( BootModes.Desktop ) ) {
						retRt64 = Rt64eccomapiWrap.SendCommand( CommHandle, DataType, Task, IntPtr.Zero );
						ret = CheckResultDebug( method, retRt64 );
						if( ResultCodes.Success != ret ) {
							break;
						}
					}
				} catch( Exception e ) {
					bool unexpected = true;
					if( ( e is DllNotFoundException ) ||
						( e is EntryPointNotFoundException ) ) {
						unexpected = false;   //	想定内の例外。
					}
					ret = logs.Exception( e, unexpected );
				}
				break;
			}
			return ret;
		}

		/// <summary>テクノ通信ライブラリ関数戻り値変換</summary>
		/// <param name="code">テクノ提供関数の戻り値</param>
		/// <param name="funcName">呼び出しテクノ通信関数名</param>
		/// <param name="note">備考</param>
		/// <returns>ECNCアプリケーション定義戻り値</returns>
		internal ResultCodes ConvertReturnCode( int code, string funcName = null, string note = null )
		{
			ResultCodes ret = ResultCodes.McCommErrorUnknown;
			switch( code ) {
				case Syncdef.E_OK:  //	正常終了
					ret = ResultCodes.Success;
					break;
				case Syncdef.E_DEVNRDY:	//	デバイス未初期化
					ret = ResultCodes.McCommErrorNotInitialize;
					break;
				case Syncdef.E_PARAM:   //	通信パラメータ設定異常
					ret = ResultCodes.McCommErrorInvalidParameter;
					break;
				case Syncdef.E_TIME:    //	タイムアウト発生
					ret = ResultCodes.McCommErrorTimeout;
					break;
				case Syncdef.E_RTRY:    //	リトライオーバー発生
					ret = ResultCodes.McCommErrorRetryOver;
					break;
				case Syncdef.E_MLTRTRY: //	多重リトライ発生
					ret = ResultCodes.McCommErrorMultipleRetry;
					break;
				case Syncdef.E_HARDER:  //	通信ハードウェアエラー
					ret = ResultCodes.McCommErrorHardware;
					break;
				case Syncdef.E_NEXIST:  //	要求データが存在しない
					ret = ResultCodes.McCommErrorNotRequest;
					break;
				case Syncdef.E_PROTECT: //	送信データ書込不可
					ret = ResultCodes.McCommErrorUnwritable;
					break;
				case Syncdef.E_SEQ: //	通信データフォーマットエラー
					ret = ResultCodes.McCommErrorFormat;
					break;
				case Syncdef.E_PRGTERM: //	運転プログラム書込中断
					ret = ResultCodes.McCommErrorProgramWriteAbort;
					break;
				case Syncdef.E_PRGBUFF: //	運転プログラムバッファオーバーフロー
					ret = ResultCodes.McCommErrorBufferoverflow;
					break;
				case Syncdef.E_CMDNOT:  //	コマンド実行不可
					ret = ResultCodes.McCommErrorNotBeExecuted;
					break;
				case Syncdef.E_EMPTYHANDLE: //	空き通信ハンドル無し
					ret = ResultCodes.McCommErrorEmptyHandle;
					break;
				case Syncdef.E_NOHANDLE:    //	無効ハンドル
					ret = ResultCodes.McCommErrorInvalidHandle;
					break;
				case Syncdef.E_BUSY:    //	通信ビジー
					ret = ResultCodes.McCommErrorBusy;
					break;
				case Syncdef.E_PRMWRITE:    //	パラメータ書き込みエラー
					ret = ResultCodes.McCommErrorWriteParameter;
					break;
				case Syncdef.E_PRGSTOPPOS:	// ﾌﾟﾛｸﾞﾗﾑ停止位置でない
					ret = ResultCodes.McCommErrorProgramStopPosition;
					break;
				default:
					break;
			}
			if( ResultCodes.Success != ret ) {
				string header = $"{ClassName}";
				if( false == string.IsNullOrEmpty( funcName ) ) {
					header += $".{funcName}";
				}
				if( true == string.IsNullOrEmpty( note ) ) {
					note = string.Empty;
				}
				AidLog logs = new AidLog( header );
				logs.Error( $"{ret}({code}),Type={DataTypeName}(0x{DataType:X2}),Handle={CommHandle}" );
			}
			return ret;
		}
		/// <summary>テクノボード関数呼び出し時のエラーコード変換</summary>
		/// <param name="method">呼び出しメソッド種別</param>
		/// <param name="returnCode">取得されたエラーコード</param>
		/// <returns>ECNCアプリケーション用エラーコード</returns>
		public ResultCodes CheckResultTechno( TechnoMethods method, int returnCode )
		{
			return ConvertReturnCode( returnCode, $"{method}({BootModes.Machine})" );
		}
		/// <summary>テクノボード関数ダミー関数呼び出し時のエラーコード変換</summary>
		/// <param name="method">呼び出しメソッド種別</param>
		/// <param name="returnCode">取得されたエラーコード</param>
		/// <returns>ECNCアプリケーション用エラーコード</returns>
		public ResultCodes CheckResultDebug( TechnoMethods method, int returnCode )
		{
			return ConvertReturnCode( returnCode, $"{method}({BootModes.Desktop})" );
		}
		/// <summary>ヒットフラグ判定</summary>
		/// <param name="target">判定対象</param>
		/// <param name="mask">マスク値</param>
		/// <returns>判定結果</returns>
		protected bool HasFlag( short target, short mask )
		{
			return ( mask == ( target & mask ) ) ? true : false;
		}
		/// <summary>ヒットフラグ判定</summary>
		/// <param name="target">判定対象</param>
		/// <param name="mask">マスク値</param>
		/// <returns>判定結果</returns>
		protected bool HasFlag( int target, int mask )
		{
			return ( mask == ( target & mask ) ) ? true : false;
		}

		/// <summary>ファイル読み込み</summary>
		/// <typeparam name="T">読み取り構造体タイプ</typeparam>
		/// <param name="data">取得結果</param>
		/// <param name="path">読み取り対象ファイルパス</param>
		/// <remarks>
		/// ファイルに永続化されたMCデータの読み込みを行います。
		/// 引数 data は呼び出し元でメモリ領域を確保してから呼び出してください。
		/// </remarks>
		/// <returns>実行結果</returns>
		protected ResultCodes ReadFile<T>( ref T data, string path )
		{
			AidLog logs = new AidLog( $"McCommBasic.ReadFile({path})" );
			ResultCodes ret = ResultCodes.Success;
			try {
				FileStream fs = new FileStream( path, FileMode.Open );
				using( BinaryReader br = new BinaryReader( fs ) ) {
					var size = Marshal.SizeOf( typeof( T ) );
					var ptr = IntPtr.Zero;
					try {
						//	ポインタのためのコピー用領域の作成。
						ptr = Marshal.AllocHGlobal( size );
						//	バイト配列を構造体に変換してコピーする。
						Marshal.Copy( br.ReadBytes( size ), 0, ptr, size );
						data = (T)Marshal.PtrToStructure( ptr, typeof( T ) );
					} finally {
						if( ptr != IntPtr.Zero ) {
							Marshal.FreeHGlobal( ptr );
						}
						br.Close();
					}
				}
				fs.Close();
			} catch( Exception e ) {
				bool unexpected = true;
				if( ( e is ArgumentException ) ||
					( e is NotSupportedException ) ||
					( e is ArgumentNullException ) ||
					( e is System.Security.SecurityException ) ||
					( e is IOException ) ||
					( e is DirectoryNotFoundException ) ||
					( e is PathTooLongException ) ||
					( e is ArgumentOutOfRangeException ) ||
					( e is EntryPointNotFoundException ) ) {
					unexpected = false;   //	想定内の例外。
				}
				ret = logs.Exception( e, unexpected );
			}
			return ret;
		}
		/// <summary>ファイル書き込み</summary>
		/// <typeparam name="T">読み取り構造体タイプ</typeparam>
		/// <param name="data">設定値</param>
		/// <param name="path">書き込み対象ファイルパス</param>
		/// <returns>実行結果</returns>
		protected ResultCodes WriteFile<T>( T data, string path )
		{
			AidLog logs = new AidLog( $"McCommBasic.WriteFile({path})" );
			ResultCodes ret = ResultCodes.Success;
			try {
				FileStream fs = new FileStream( path, FileMode.OpenOrCreate );
				using( BinaryWriter bw = new BinaryWriter( fs ) ) {
					var size = Marshal.SizeOf( typeof( T ) );
					var buffer = new byte[size];
					var ptr = IntPtr.Zero;
					try {
						//	ポインタのためのコピー用領域の作成。
						ptr = Marshal.AllocHGlobal( size );
						//	構造体をバイト配列に変換してコピーする。
						Marshal.StructureToPtr( data, ptr, false );
						Marshal.Copy( ptr, buffer, 0, size );
					} finally {
						if( ptr != IntPtr.Zero ) {
							Marshal.FreeHGlobal( ptr );
						}
					}
					bw.Write( buffer );
					bw.Close();
				}
				fs.Close();
			} catch( Exception e ) {
				bool unexpected = true;
				if( ( e is ArgumentException ) ||
					( e is NotSupportedException ) ||
					( e is ArgumentNullException ) ||
					( e is System.Security.SecurityException ) ||
					( e is IOException ) ||
					( e is DirectoryNotFoundException ) ||
					( e is PathTooLongException ) ||
					( e is ArgumentOutOfRangeException ) ||
					( e is EntryPointNotFoundException ) ) {
					unexpected = false;   //	想定内の例外。
				}
				ret = logs.Exception( e, unexpected );
			}
			return ret;
		}
	}
}
