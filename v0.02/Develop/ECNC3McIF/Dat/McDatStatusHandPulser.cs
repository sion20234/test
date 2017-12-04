using System;
using System.Runtime.InteropServices;
using ECNC3.Enumeration;
using ECNC3.Models.Common;
using static Rt64ecdata;

namespace ECNC3.Models.McIf
{
	/// <summary>4-1-20.手動パルサ設定情報読み出し</summary>
	public class McDatStatusHandPulser : McCommBasic, IEcnc3McDatReadOnly, IDisposable
    {
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>MCインターフェース構造体</summary>
		private HANDLESTS _data;
		/// <summary>コンストラクタ</summary>
		public McDatStatusHandPulser()
		{
			ClassName = GetType().Name;
			DataType = Syncdef.DAT_HANDLESTS;
			DataTypeName = "DAT_HANDLESTS";
		}
		/// <summary>手動パルサー有効／無効</summary>
		public bool Enabled { get { return ( 0 != _data.handle_mode ) ? true : false; } }
		/// <summary>手動パルサー倍率</summary>
		public short Scale { get { return _data.kp; } }
		/// <summary>手動パルサー第1軸</summary>
		public short Axis1 { get { return _data.ax1; } }
		/// <summary>手動パルサー第2軸</summary>
		public short Axis2 { get { return _data.ax2; } }

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
		/// <summary>実行</summary>
		/// <returns>実行結果</returns>
		public ResultCodes Read()
		{
			return ReadData( out _data );
		}
		/// <summary>MCデータ読み込み</summary>
		/// <param name="data">MCより読み込まれたデータ</param>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// MCより情報を取得します。
		/// </remarks>
		private ResultCodes ReadData( out HANDLESTS data )
		{
			AidLog logs = new AidLog( "McDatStatusHandPulser.ReadData" );
			ResultCodes ret = ResultCodes.McNotInitialize;
			data = HANDLESTS.Init();
			while( true == StandBy() ) {
				try {
					int size = Marshal.SizeOf( data );
					int retRt64 = 0;
					TechnoMethods method = TechnoMethods.ReceiveData;
					if( true == BootMode.HasFlag( BootModes.Machine ) ) {
						retRt64 = Rt64eccomapi.ReceiveData( CommHandle, DataType, Task, NonParameter, ref size, ref data );
						ret = CheckResultTechno( method, retRt64 );
						if( ResultCodes.Success != ret ) {
							break;
						}
					}
					if( true == BootMode.HasFlag( BootModes.Desktop ) ) {
						retRt64 = Rt64eccomapiWrap.ReceiveData( CommHandle, DataType, Task, NonParameter, ref size, ref data );
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
