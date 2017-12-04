using System;
using ECNC3.Enumeration;
using ECNC3.Models.Common;
using static Rt64ecdata;

namespace ECNC3.Models.McIf
{
	/// <summary>4-2-7.インクレPTP位置決めコマンド</summary>
	public class McReqPositioningPointToPointInc : McReqPositioningPointToPoint, IEcnc3McCommand, IDisposable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>コンストラクタ</summary>
		public McReqPositioningPointToPointInc()
		{
			ClassName = GetType().Name;
			DataType = Syncdef.REQ_PTPSTART;
			DataTypeName = "REQ_PTPSTART";
			Mode = Modes.PointToPointInc;
		}
		/// <summary>座標系設定</summary>
		public new CoordTypes CoordType
		{
			get { return SelectedCoordType; }
			set {; }
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
		/// <summary>コマンド発行</summary>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// プロパティに設定された内容によりMCボードへコマンドを発行します。
		/// </remarks>
		public new ResultCodes Execute()
		{
			AidLog logs = new AidLog( "McReqPositioningPointToPointInc.Execute" );
			ResultCodes ret = ResultCodes.McNotInitialize;
			while( true == StandBy() ) {
				try {
					PTPSTART data = PTPSTART.Init();
					{
						SetParam( ref data, AxisNumbers.X, AxisX );
						SetParam( ref data, AxisNumbers.Y, AxisY );
						SetParam( ref data, AxisNumbers.W, AxisW );
						SetParam( ref data, AxisNumbers.Z, AxisZ );
						SetParam( ref data, AxisNumbers.A, AxisA );
						SetParam( ref data, AxisNumbers.B, AxisB );
						SetParam( ref data, AxisNumbers.C, AxisC );
						SetParam( ref data, AxisNumbers.I, AxisI );
					}
					int retRt64 = 0;
					TechnoMethods method = TechnoMethods.SendCommand;
					logs.Sure( $"SendCommand({DataTypeName},AxisFlag=0x{data.AxisFlag:x},[" + logs.ToString( data.IncAxis ) );
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
		/// <summary>軸ごとのパラメータ設定</summary>
		/// <param name="target">設定対象の構造体の参照</param>
		/// <param name="number">軸番号</param>
		/// <param name="source">設定元のインスタンスの参照</param>
		private void SetParam( ref PTPSTART target, AxisNumbers number, StructurePositioniingItem source )
		{
			if( null != source ) {
				if( true == source.Movable ) {
					int bit = AxisNumberToBit( number );
					if( 0 != bit ) {
						target.AxisFlag |= bit;
					}
				}
				target.IncAxis[(int)number] = source.TargetPosition;
			}
		}
	}
}
