using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECNC3.Enumeration;
using ECNC3.Models.Common;

namespace ECNC3.Models.McIf
{
	/// <summary>4-6-3.エラー行取得</summary>
	public class McGcdErrorLine : McGcdBasic, IDisposable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;

		/// <summary>コンストラクタ</summary>
		public McGcdErrorLine()
		{
			ClassName = GetType().Name;
		}

		/// <summary>エラー発生行</summary>
		/// <remarks>
		/// プログラム変換エラーが発生した行番号。
		/// エラーがない場合は、最終行を格納する。
		/// </remarks>
		public int ErrorLineNumber { get; private set; }
		/// <summary>プログラム種別</summary>
		/// <remarks>
		/// どのプログラムでエラーが発生したのかを示します。
		/// </remarks>
		public ProgramCodeTypes ErrorCodeType { get; private set; }
		/// <summary>エラー発生コード</summary>
		/// <remarks>
		/// ユーザ定義GコードまたはMコードでプログラム内でエラーが発生した場合、
		/// そのエラーの発生したGコードまたはMコード番号を格納します。
		/// </remarks>
		public int ErrorCodeNumber { get; private set; }

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

		/// <summary>変換実行</summary>
		/// <returns>実行結果</returns>
		public ResultCodes Execute()
		{
			AidLog logs = new AidLog( "McGcdErrorLine.Execute" );
			ResultCodes ret = ResultCodes.McNotInitialize;
			while( true == StandBy() ) {
				try {
					int retRt64 = Rt64ecgcdapi.GcdGetErrLine();
					{
						logs.Sure( $"GcdGetErrLine()={retRt64}" );
						ErrorCodeType = (ProgramCodeTypes)( ( retRt64 & 0x03000000 ) >> 24 );  //	プログラム種別
						ErrorLineNumber = retRt64 & 0x0000FFFF;   //	エラー発生行番号
						ErrorCodeNumber = ( retRt64 & 0x00FF0000 ) >> 16;   //	エラー発生 Gコード／Mコード
					}
					ret = ResultCodes.Success;
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
	}
}
