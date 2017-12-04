using System;
using ECNC3.Enumeration;
using ECNC3.Models.Common;
using static Rt64ecdata;

namespace ECNC3.Models.McIf
{
	/// <summary>4-5-15 アブソ直線補間位置決めコマンド</summary>
	public class McReqPositioningLinearInterpolation : McReqPositioningPointToPoint, IEcnc3McCommand, IDisposable
    {
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>コンストラクタ</summary>
		public McReqPositioningLinearInterpolation()
		{
			ClassName = GetType().Name;
			Mode = Modes.LinearInterpolation;
		}
		/// <summary>座標系設定</summary>
		public new CoordTypes CoordType
		{
			get { return SelectedCoordType; }
			set
			{
				SelectedCoordType = value;
				if( CoordTypes.Machine == value ) {
					DataType = Syncdef.REQ_LINBSTART;
					DataTypeName = "REQ_LINBSTART";
				} else if( CoordTypes.Work == value ) {
					DataType = Syncdef.REQ_LINASTART;
					DataTypeName = "REQ_LINASTART";
				} else {
					DataType = 0;
				}
			}
		}
		/// <summary>送り速度</summary>
		public int Feed { get; set; }

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
			AidLog logs = new AidLog( "McReqPositioningLinearInterpolation.Execute" );
			ResultCodes ret = ResultCodes.McNotInitialize;
			while( true == StandBy() ) {
				try {
					if( ( Syncdef.REQ_LINBSTART != DataType ) &&
						( Syncdef.REQ_LINASTART != DataType ) ) {
						break;
					}
					LINASTART data = LINASTART.Init();
					{
						SetParam( ref data, AxisNumbers.X, AxisX );
						SetParam( ref data, AxisNumbers.Y, AxisY );
						SetParam( ref data, AxisNumbers.W, AxisW );
						SetParam( ref data, AxisNumbers.Z, AxisZ );
						SetParam( ref data, AxisNumbers.A, AxisA );
						SetParam( ref data, AxisNumbers.B, AxisB );
						SetParam( ref data, AxisNumbers.C, AxisC );
						SetParam( ref data, AxisNumbers.I, AxisI );
						data.Feed = Feed;
					}
					int retRt64 = 0;
					TechnoMethods method = TechnoMethods.SendCommand;
					logs.Sure( $"SendCommand({DataTypeName},AxisFlag=0x{data.AxisFlag:x},[" + logs.ToString( data.PosAxis ) + $"],Feed={data.Feed}" );
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
		private void SetParam( ref LINASTART target, AxisNumbers number, StructurePositioniingItem source )
		{
			if( null != source ) {
				if( true == source.Movable ) {
					int bit = AxisNumberToBit( number );
					if( 0 != bit ) {
						target.AxisFlag |= bit;
					}
				}
				target.PosAxis[(int)number] = source.TargetPosition;
			}
		}
	}
}
