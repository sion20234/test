using System;
using ECNC3.Enumeration;
using ECNC3.Models.Common;
using static Rt64ecdata;

namespace ECNC3.Models.McIf
{
	/// <summary>4-5-49.放電制御コマンド</summary>
	public class McReqControllingDischarge : McCommBasic, IEcnc3McCommand, IDisposable
    {
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;

		/// <summary>コンストラクタ</summary>
		public McReqControllingDischarge()
		{
			ClassName = GetType().Name;
			DataType = Syncdef.REQ_BONCMND;
			DataTypeName = "REQ_BONCMND";
		}
		/// <summary>無効／有効設定</summary>
		public bool Enabled { get; set; }

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
			AidLog logs = new AidLog( "McReqControllingDischarge.Execute" );
			if( true == StandBy() ) {
				BONCMND data = BONCMND.Init();
				data.BonOut = (short)( ( true == Enabled ) ? 1 : 0 );
				int ret = 0;
				logs.Sure( $"SendCommand({DataTypeName},BonOut={data.BonOut})" );
				if( true == BootMode.HasFlag( BootModes.Machine ) ) {
					ret = Rt64eccomapi.SendCommand( CommHandle, DataType, Task, ref data );
				}
				if( true == BootMode.HasFlag( BootModes.Desktop ) ) {
					ret = Rt64eccomapiWrap.SendCommand( CommHandle, DataType, Task, ref data );
				}
				if( Syncdef.E_OK != ret ) {
					logs.Error( $"SendCommand(Comm={CommHandle},Type={DataTypeName}(0x{DataType:x}))={ret}" );
					return ConvertReturnCode( ret );
				}
				return ResultCodes.Success;
			}
			return ResultCodes.McNotInitialize;
		}
	}
}
