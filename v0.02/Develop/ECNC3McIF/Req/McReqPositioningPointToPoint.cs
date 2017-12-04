using System;
using ECNC3.Enumeration;
using ECNC3.Models.Common;
using static Rt64ecdata;

namespace ECNC3.Models.McIf
{
	/// <summary>4-5-14.アブソPTP位置決めコマンド</summary>
	public class McReqPositioningPointToPoint : McCommBasic, IEcnc3McCommand, IDisposable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>座標設定モード</summary>
		protected CoordTypes SelectedCoordType { get; set; }
		/// <summary>コンストラクタ</summary>
		public McReqPositioningPointToPoint()
		{
			ClassName = GetType().Name;
			Mode = Modes.PointToPoint;
			SelectedCoordType = CoordTypes.NotSet;
		}
		/// <summary>呼び出しモード</summary>
		protected enum Modes
		{
			/// <summary>アブソPTP位置決め</summary>
			PointToPoint,
			/// <summary>直線補間位置決め</summary>
			LinearInterpolation,
			/// <summary>W軸上限待避有効PTP位置決め</summary>
			WAxisUpperLimitValid,
			/// <summary>インクレPTP位置決め</summary>
			PointToPointInc,
		}
		/// <summary>呼び出しモード</summary>
		protected Modes Mode { get; set; }
		/// <summary>座標系設定</summary>
		public CoordTypes CoordType
		{
			get { return SelectedCoordType; }
			set
			{
				SelectedCoordType = value;
				if( CoordTypes.Machine == value ) {
					DataType = Syncdef.REQ_PTPBSTART;
					DataTypeName = "REQ_PTPBSTART";
				} else if( CoordTypes.Work == value ) {
					DataType = Syncdef.REQ_PTPASTART;
					DataTypeName = "REQ_PTPASTART";
				} else {
					DataType = 0;
				}
			}
		}
		/// <summary>第1軸目標位置</summary>
		public StructurePositioniingItem AxisX { get; set; }
		/// <summary>第2軸目標位置</summary>
		public StructurePositioniingItem AxisY { get; set; }
		/// <summary>第3軸目標位置</summary>
		public StructurePositioniingItem AxisW { get; set; }
		/// <summary>第4軸目標位置</summary>
		public StructurePositioniingItem AxisZ { get; set; }
		/// <summary>第5軸目標位置</summary>
		public StructurePositioniingItem AxisA { get; set; }
		/// <summary>第6軸目標位置</summary>
		public StructurePositioniingItem AxisB { get; set; }
		/// <summary>第7軸目標位置</summary>
		public StructurePositioniingItem AxisC { get; set; }
		/// <summary>第8軸目標位置</summary>
		public StructurePositioniingItem AxisI { get; set; }

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
			AidLog logs = new AidLog( "McReqPositioningPointToPoint.Execute" );
			ResultCodes ret = ResultCodes.McNotInitialize;
			while( true == StandBy() ) {
				try {
					if( ( Syncdef.REQ_PTPBSTART != DataType ) &&
						( Syncdef.REQ_PTPASTART != DataType ) ) {
						break;
					}
					PTPASTART data = PTPASTART.Init();
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
					logs.Sure( $"SendCommand({DataTypeName},AxisFlag=0x{data.AxisFlag:x},[" + logs.ToString( data.PosAxis ) + "]" );
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
		private void SetParam( ref PTPASTART target, AxisNumbers number, StructurePositioniingItem source )
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
		/// <summary>軸番号からビット配置への変換</summary>
		/// <param name="number">軸番号</param>
		/// <returns>変換結果</returns>
		protected int AxisNumberToBit( AxisNumbers number )
		{
			return ( AxisNumbers.X == number ) ? (int)AxisBits.X :
				( AxisNumbers.Y == number ) ? (int)AxisBits.Y :
				( AxisNumbers.W == number ) ? (int)AxisBits.W :
				( AxisNumbers.Z == number ) ? (int)AxisBits.Z :
				( AxisNumbers.A == number ) ? (int)AxisBits.A :
				( AxisNumbers.B == number ) ? (int)AxisBits.B :
				( AxisNumbers.C == number ) ? (int)AxisBits.C :
				( AxisNumbers.I == number ) ? (int)AxisBits.I : 0;
		}
	}
}
