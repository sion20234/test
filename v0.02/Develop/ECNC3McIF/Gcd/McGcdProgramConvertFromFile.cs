using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECNC3.Enumeration;
using ECNC3.Models.Common;

namespace ECNC3.Models.McIf
{
	/// <summary>プログラム変換</summary>
	/// <code>
	/// using( McGcdProgramConvertFromFile mc = new McGcdProgramConvertFromFile() ){
	///		mc.FilePath = 変換元プログラムファイルパス
	///		mc.Execute();
	///		
	///		mc.ConvertedProgram;
	///		...
	/// }
	/// </code>
	public class McGcdProgramConvertFromFile : McGcdBasic, IDisposable
	{
		/// <summary>プログラム変換コード格納サイズ最小値</summary>
		/// <remarks>
		/// バイナリデータ領域は呼出元で512KB以上の領域を確保しなければならない。
		/// </remarks>
		private readonly long lowerProgramBufferSize = 512 * 1024;
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;

		/// <summary>コンストラクタ</summary>
		public McGcdProgramConvertFromFile()
		{
			ClassName = GetType().Name;
			FilePath = string.Empty;
			Reset();
		}
		/// <summary>プログラムファイルパス</summary>
		public string FilePath { get; set; }

		/// <summary>変換結果プログラムサイズ</summary>
		public int ProgramSize { get; private set; }
		/// <summary>変換結果プログラム</summary>
		public byte[] ConvertedProgram { get; private set; }
		/// <summary>プログラム番号</summary>
		public int ProgramNumber { get; private set; }

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
			AidLog logs = new AidLog( "McGcdProgramConvertFromFile.Execute" );
			ResultCodes ret = ResultCodes.McNotInitialize;
			while( true == StandBy() ) {
				try {
					Reset();
					//	変換バッファのサイズを決定
					if( false == File.Exists( FilePath ) ) {
						logs.Error( $"File is not exists[{FilePath}]." );
						ret = ResultCodes.NotFound;
						break;
					}
					long fileSize = lowerProgramBufferSize;
					FileInfo fi = new FileInfo( FilePath );
					if( fileSize < fi.Length ) {
						fileSize = fi.Length;
					}
					//	テキストベースのファイルをバイナリベースのテクノ仕様に変換する。
					byte[] program = new byte[fileSize];    //	変換されたバイナリベースのプログラム
					int size = 0;                           //	変換されたバイナリベースのプログラムのサイズ
					short number = 0;                       //	プログラム番号
					int retRt64 = 0;

					while( true ) {
						logs.Sure( $"Path={FilePath},BufferSize={program.Length}." );

						//	プログラム変換に関して適正な関数が不明であるため、成功の可能性のあるすべての関数を実行する。

						//	ECNC2で利用されている関数
						logs.Sure( $"FileGcodeConv" );
						retRt64 = Rt64ecgcdapi.FileGcodeConv( FilePath, ref size, program, ref number );
						if( Syncdef.E_OK == retRt64 ) {
							break;
						}
						ret = ConvertReturnCode( retRt64 );
						//	テクノコード変換ライブラリにあるファイル変換
						logs.Sure( $"FileProgramConv" );
						retRt64 = Rt64ectcdapi.FileProgramConv( FilePath, ref size, program, ref number );
						if( Syncdef.E_OK == retRt64 ) {
							break;
						}
						ret = ConvertReturnCode( retRt64 );
						//	テクノコード変換ライブラリにあるメモリ変換
						logs.Sure( $"MemProgramConv" );
						string code;
						FileStream fs = new FileStream( FilePath, FileMode.Open, FileAccess.Read );
						using( TextReader sr = new StreamReader( fs, Encoding.GetEncoding( "shift_jis" ) ) ) {
							code = sr.ReadToEnd();
							retRt64 = Rt64ectcdapi.MemProgramConv( code, ref size, program, ref number );
							if( Syncdef.E_OK == retRt64 ) {
								break;
							}
							ret = ConvertReturnCode( retRt64 );
						}
						break;
					}
					if( Syncdef.E_OK != retRt64 ) {
						return ret;
					}
					//	変換結果をプロパティへ反映。
					ConvertedProgram = new byte[size];
					Buffer.BlockCopy( program, 0, ConvertedProgram, 0, size );
					ProgramNumber = number;
					ProgramSize = size;
					logs.Sure( $"Result(P#{ProgramNumber},Size={ProgramSize})" );
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
		/// <summary>メンバ変数初期化</summary>
		private void Reset()
		{
			ProgramSize = 0;
			ProgramNumber = 0;
			if( null != ConvertedProgram ) {
				ConvertedProgram = null;
			}
		}
		/// <summary>テクノコード変換ライブラリ呼び出し戻り値変換</summary>
		/// <param name="rt64ReturnCode">テクノ提供関数の戻り値</param>
		/// <returns>ECNCアプリケーション定義戻り値</returns>
		protected override ResultCodes ConvertReturnCode( int rt64ReturnCode )
		{
			AidLog logs = new AidLog( "McGcdProgramConvertFromFile.ConvertReturnCode" );
			int errorCode = ( rt64ReturnCode & 0xF000 ) >> 12;
			int stepNumber = rt64ReturnCode & 0x0FFF;
			logs.Sure( $"0x{rt64ReturnCode:x4}(Code ={errorCode},Step={stepNumber})" );
			return base.ConvertReturnCode( errorCode );
		}
	}
}
