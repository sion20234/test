using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECNC3.Enumeration;
using ECNC3.Models.Common;
using static Rt64ecgcdapi;

namespace ECNC3.Models.McIf
{
	/// <summary>テクノコード変換基底クラス</summary>
	public class McGcdBasic : McIfBasic, IDisposable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;

		/// <summary>コンストラクタ</summary>
		public McGcdBasic()
		{
		}

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

		/// <summary>テクノコード変換ライブラリ呼び出し戻り値変換</summary>
		/// <param name="code">テクノ提供関数の戻り値</param>
		/// <returns>ECNCアプリケーション定義戻り値</returns>
		protected virtual ResultCodes ConvertReturnCode( int code )
		{
			ResultCodes ret = ResultCodes.McGcdErrorUnknown;
			switch( code ) {
				case 0:
					ret = ResultCodes.Success;
					break;
				case PRGERR_FILE: //	テキスト運転プログラムファイルエラー
					ret = ResultCodes.McGcdErrorFile;
					break;
				case PRGERR_BUFF_OVERFlOW: //	運転プログラムバッファオーバーフロー
					ret = ResultCodes.McGcdErrorBufferOverflow;
					break;
				case PRGERR_FORMAT: //	テキスト運転プログラムフォーマットエラー
					ret = ResultCodes.McGcdErrorFileFormat;
					break;
				case PRGERR_CALC: //	プログラム変換演算エラー
					ret = ResultCodes.McGcdErrorConvertion;
					break;
				case PRGERR_WMEM_OVERFLOW: //	作業メモリオーバーフロー
					ret = ResultCodes.McGcdErrorWorkMemoryOverflow;
					break;
				case PRGERR_INITIAL: //	変換ライブラリ未初期化エラー
					ret = ResultCodes.McGcdErrorNotInitialize;
					break;
				case PRGERR_CYCLIC_CALL: //	ユーザー定義Ｇ／Ｍコード循環呼出エラー
					ret = ResultCodes.McGcdErrorUserCodeCirculateCall;
					break;
				case PRGERR_UNDEFINED_CODE: //	未定義コード指定エラー
					ret = ResultCodes.McGcdErrorUndefinedCodeSpecified;
					break;
				case PRGERR_DISABLE_CODE: //	無効Ｇ／Ｍコード指定エラー
					ret = ResultCodes.McGcdErrorUserCodeInvalidSpecified;
					break;
				default:
					break;
			}
			// StackFrameクラスをインスタンス化する
			System.Diagnostics.StackFrame stack = new System.Diagnostics.StackFrame( 1 );
			AidLog logs = new AidLog( $"{stack.GetMethod().ReflectedType.Name}.{stack.GetMethod().Name}" );
			logs.Error( $"{ret}({code})" );
			return ret;
		}
	}
}
