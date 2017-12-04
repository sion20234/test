using System;
using System.Runtime.InteropServices;
using System.Text;
using ECNC3.Enumeration;
using ECNC3.Models.Common;
using static Rt64ecdata;

namespace ECNC3.Models.McIf
{
	/// <summary>4-1-13.ROMバージョン情報読出</summary>
	public class McDatRomVersion : McCommBasic, IEcnc3McDatReadOnly, IDisposable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>MCインターフェース構造体</summary>
		private ROMVERSION _data;
		/// <summary>コンストラクタ</summary>
		public McDatRomVersion()
		{
			ClassName = GetType().Name;
			DataType = Syncdef.DAT_VERSION;
			DataTypeName = "DAT_VERSION";
		}

		/// <summary>バージョン文字列</summary>
		public string Version { get { return Encoding.ASCII.GetString( _data.Version ); } }
		/// <summary>SUM:even(rom)</summary>
		public short EvenSum { get { return _data.EvenSum; } }
		/// <summary>SUM:odd(rom)</summary>
		public short OddSum { get { return _data.OddSum; } }
		/// <summary>SUM:SH内部FLASH</summary>
		public short FlashSum { get { return _data.FlashSum; } }
		/// <summary>SUM:SH内部FLASH使用フラグ</summary>
		public short FlashFlg { get { return _data.FlashFlg; } }
		/// <summary>機種ID</summary>
		public short KindID { get { return _data.KindID; } }
		/// <summary>シリアルID</summary>
		public int SerialID { get { return _data.SerialID; } }
		/// <summary>プロダクトID</summary>
		public int ProductID { get { return _data.ProductID; } }

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
		/// <summary>読み込み</summary>
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
		private ResultCodes ReadData( out ROMVERSION data )
		{
			AidLog logs = new AidLog( "McDatRomVersion.Read" );
			ResultCodes ret = ResultCodes.McNotInitialize;
			data = ROMVERSION.Init();
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
					logs.Sure( "Version=" + Encoding.ASCII.GetString( data.Version ) + ","
							+ $"EvenSum={data.EvenSum},OddSum={data.OddSum},FlashSum={data.FlashSum},FlashFlg={data.FlashFlg}"
							+ $"KindID={data.KindID},SerialID={data.SerialID},ProductID={data.ProductID}" );
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
