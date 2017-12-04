using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ECNC3.Enumeration;
using ECNC3.Models.Common;

namespace ECNC3.Models.McIf
{
	/// <summary>4-1-2.運転プログラム書込／読出</summary>
	public class McDatProgram : McCommBasic, IEcnc3McDatWriteOnly, IDisposable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>コンストラクタ</summary>
		public McDatProgram()
		{
			ClassName = GetType().Name;
			DataType = Syncdef.DAT_PROGRAM;
			DataTypeName = "DAT_PROGRAM";
			BlockNumber = -1;
			ProgramNumber = 0;
			Reset();
		}
		/// <summary>プログラムファイル名</summary>
		public string ProgramFilePath { get; set; }

		/// <summary>ブロック番号</summary>
		/// <remarks>
		/// 書き込み時のみ必要な設定です。
		/// </remarks>
		public int BlockNumber { get; set; }
		/// <summary>プログラム番号</summary>
		public int ProgramNumber { get; set; }
		/// <summary>変換結果プログラム</summary>
		public byte[] ProgramCode { get; private set; }

		#region プログラム変換エラー情報
		/// <summary>エラー発生行</summary>
		/// <remarks>
		/// プログラム変換エラーが発生した行番号。
		/// エラーがない場合は、最終行を格納する。
		/// </remarks>
		public int ErrorLineNumber { get; set; }
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
		public int ErrorCodeNumber { get; set; }
		#endregion

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
						if( null != ProgramCode ) {
							ProgramCode = new byte[] { };
							ProgramCode = null;
						}
					}
					//  アンマネージリソースの解放
				}
				_disposed = true;
			} finally {
				//  基底クラスのDispose()を確実に呼び出す。
				base.Dispose( disposing );
			}
		}
//#if __KEY_USAGE_UNKNOWN__
		/// <summary>読み込み</summary>
		/// <returns>実行結果</returns>
		public ResultCodes Read()
		{
			//	プログラムを読み取った際の使途が不明確なので保留。
			byte[] data;
			ResultCodes ret = ReadData( out data );
			if( ResultCodes.Success != ret ) {
				return ret;
			}
			if( null != ProgramCode ) {
				ProgramCode = new byte[] { };
				ProgramCode = null;
			}
			ProgramCode = new byte[data.Length];
			Buffer.BlockCopy( data, 0, ProgramCode, 0, data.Length );
			return ResultCodes.Success;
		}
		/// <summary>MCデータ読み込み</summary>
		/// <param name="data">MCより読み込まれたデータ</param>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// MCより情報を取得します。
		/// </remarks>
		private ResultCodes ReadData( out byte[] data )
		{
			AidLog logs = new AidLog( "McDatProgram.ReadData" );
			//	データサイズはPLCデータレジスタの最大。
			data = new byte[196608];
			if( false == Verify( false ) ) {
				return ResultCodes.InvalidArgument;
			}
			ResultCodes ret = ResultCodes.McNotInitialize;
			//data = INITIALPRM.Init();
			while( true == StandBy() ) {
				try {
					int size = data.Length;
					//int size = Marshal.SizeOf( data );
					int param = ProgramNumber;
					int retRt64 = 0;
					TechnoMethods method = TechnoMethods.ReceiveData;
					if( BootModes.Desktop == BootMode ) {
						retRt64 = Rt64eccomapiWrap.ReceiveData( CommHandle, DataType, Task, param, ref size, data );
						ret = CheckResultDebug( method, retRt64 );
						if( ResultCodes.Success != ret ) {
							break;
						}
					} else {
						retRt64 = Rt64eccomapi.ReceiveData( CommHandle, DataType, Task, param, ref size, data );
						ret = CheckResultTechno( method, retRt64 );
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
//#endif
		/// <summary>書き込み</summary>
		/// <returns>実行結果</returns>
		public ResultCodes Write()
		{
			AidLog logs = new AidLog( "McDatProgram.Write" );
			ResultCodes ret = ResultCodes.InvalidArgument;
			Reset();
			if( false == string.IsNullOrEmpty( ProgramFilePath ) ) {
				//	ファイル解析後、プログラムダウンロード
				using( McGcdProgramConvertFromFile gcd = new McGcdProgramConvertFromFile() ) {
					gcd.FilePath = ProgramFilePath;
					ret = gcd.Execute();
					if( ResultCodes.Success != ret ) {
						logs.Error( $"{ret}" );
						//	変換エラーにつき、エラー情報を取得
						using( McGcdErrorLine err = new McGcdErrorLine() ) {
							if( ResultCodes.Success == err.Execute() ) {
								ErrorLineNumber = err.ErrorLineNumber;
								ErrorCodeType = err.ErrorCodeType;
								ErrorCodeNumber = err.ErrorCodeNumber;
							}
						}
					} else {
						//	成功
						ProgramNumber = gcd.ProgramNumber;
						ret = WriteData( gcd.ConvertedProgram );
					}
				}
			} else if( null != ProgramCode ) {
				//	データを直接設定することによるダウンロード
				ret = WriteData( ProgramCode );
			}
			return ret;
		}
		/// <summary>書き込み</summary>
		/// <param name="data">書き込み情報</param>
		/// <returns>実行結果</returns>
		private ResultCodes WriteData( byte[] data )
		{
			AidLog logs = new AidLog( "McDatProgram.WriteData" );
			if( false == Verify( true ) ) {
				logs.Error( $"B#{BlockNumber},P#{ProgramNumber}" );
				return ResultCodes.InvalidArgument;
			}
			ResultCodes ret = ResultCodes.McNotInitialize;
			while( true == StandBy() ) {
				try {
					int size = data.Length;
					int param = ( BlockNumber << 16 ) | ProgramNumber;
					int retRt64 = 0;
					TechnoMethods method = TechnoMethods.SendData;
					logs.Sure( $"SendData({DataTypeName},Param=0x{param:x8}(B#{BlockNumber},P#{ProgramNumber}),Size={size})" );
					if( BootModes.Desktop == BootMode ) {
						retRt64 = Rt64eccomapiWrap.SendData( CommHandle, DataType, Task, param, size, data );
						ret = CheckResultDebug( method, retRt64 );
						if( ResultCodes.Success != ret ) {
							break;
						}
					} else {
						retRt64 = Rt64eccomapi.SendData( CommHandle, DataType, Task, param, size, data );
						ret = CheckResultTechno( method, retRt64 );
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
		/// <summary>パラメータチェック</summary>
		/// <param name="isWrite">実行内容
		///		<list type="bullet" >
		///			<item>true=書き込み処理</item>
		///			<item>false=読み込み処理</item>
		///		</list>
		/// </param>
		/// <returns>判定結果
		///		<list type="bullet" >
		///			<item>true=正当</item>
		///			<item>false=不当</item>
		///		</list>
		/// </returns>
		/// <remarks>
		/// 送信パラメータの正当性チェックを行います。
		/// </remarks>
		private bool Verify( bool isWrite )
		{
			while( true ) {
				if( ( 1 > ProgramNumber ) && ( 62767 < ProgramNumber ) ) {
					break;
				}
				if( true == isWrite ) {
					if( ( 0 > BlockNumber ) && ( 63 < BlockNumber ) ) {
						break;
					}
				}
				return true;
			}
			return false;
		}
		/// <summary>応答メンバ変数初期化</summary>
		/// <remarks>
		/// Executeメソッドの呼び出しにより設定される各プロパティ値の初期化を実施します。
		/// </remarks>
		private void Reset()
		{
			ErrorLineNumber = 0;
			ErrorCodeType = ProgramCodeTypes.Unknown;
			ErrorCodeNumber = 0;
		}
	}
}
