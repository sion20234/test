using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ECNC3.Enumeration;
using ECNC3.Models.Common;
using static Rt64ecdata;

namespace ECNC3.Models.McIf
{
	/// <summary>4-4-14.放電加工スキップ時仮想点登録番号情報読出</summary>
	public class McDatProcessSkipVirtualPositionNumber : McCommBasic, IEcnc3McDatReadOnly, IDisposable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>MCインターフェース構造体</summary>
		private PRCSSKIPVPOSNO _data;

		/// <summary>コンストラクタ</summary>
		public McDatProcessSkipVirtualPositionNumber()
		{
			ClassName = GetType().Name;
			DataType = Syncdef.DAT_PRCSSKIPVPOSNO;
			DataTypeName = "DAT_PRCSSKIPVPOSNO";
		}

		/// <summary>最後に登録された仮想点登録番号</summary>
		public short LastNo { get { return _data.LastNo; } }
		/// <summary>次に放電加工スキップが発生したときに座標が登録される仮想点番号</summary>
		public short NextNo { get { return _data.NextNo; } }
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
		private ResultCodes ReadData( out PRCSSKIPVPOSNO data )
		{
			AidLog logs = new AidLog( "McDatProcessSkipVirtualPositionNumber.ReadData" );
			ResultCodes ret = ResultCodes.McNotInitialize;
			data = PRCSSKIPVPOSNO.Init();
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
