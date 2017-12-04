using System;
using ECNC3.Enumeration;
using ECNC3.Models.Common;
using static Rt64ecdata;

namespace ECNC3.Models.McIf
{
	/// <summary>4-2-18./4-5-12.プログラム実行開始コマンド</summary>
	/// <remarks>
	/// プログラム運転を開始/再開するためのコマンドです。
	/// StartNNumber の設定値により実行されるコマンドが変化します。
	/// StartNNumber が、
	/// <list type="bullet" >
	///		<item>0未満の場合、REQ_PROGSTRT が発行されます。</item>
	///		<item>0以上の場合、REQ_PROGSTRTN が発行されます。</item>
	/// </list>
	/// REQ_PROGSTRTN が発行されるケースにおいて、指定されたＮ番号が見つからないときは、実行を開始しません。
	/// </remarks>
	public class McReqProgramStart : McCommBasic, IEcnc3McCommand, IDisposable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>コンストラクタ</summary>
		public McReqProgramStart()
		{
			ClassName = GetType().Name;
			StartNNumber = -1;
			DataType = Syncdef.REQ_PROGSTRT;
			DataTypeName = "REQ_PROGSTRT";
		}
		/// <summary>実行開始N番号</summary>
		public short StartNNumber { get; set; }

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

		/// <summary>コマンド発行</summary>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// プロパティに設定された内容によりMCボードへコマンドを発行します。
		/// </remarks>
		public ResultCodes Execute()
		{
			AidLog logs = new AidLog( "McReqProgramStart.Execute" );
			ResultCodes ret = ResultCodes.McNotInitialize;
			while( true == StandBy() ) {
				try {
					if( 0 > StartNNumber ) {
						//	4-2-18.プログラム運転開始コマンド
						DataType = Syncdef.REQ_PROGSTRT;
						DataTypeName = "REQ_PROGSTRT";
						return ExecuteByNonParam();
					}
					//	4-5-12.プログラム実行開始コマンド
					DataType = Syncdef.REQ_PROGSTRTN;
					DataTypeName = "REQ_PROGSTRTN";
					PROGSTRT data = PROGSTRT.Init();
					data.NNo = (short)StartNNumber;
					int retRt64 = 0;
					TechnoMethods method = TechnoMethods.SendCommand;
					logs.Sure( $"SendCommand({DataTypeName},NNo={data.NNo}" );
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
