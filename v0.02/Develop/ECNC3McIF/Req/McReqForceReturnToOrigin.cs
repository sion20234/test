using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECNC3.Enumeration;
using ECNC3.Models.Common;
using static Rt64ecdata;

namespace ECNC3.Models.McIf
{
	/// <summary>4-5-45.強制原点復帰完了コマンド</summary>
	public class McReqForceReturnToOrigin : McCommBasic, IEcnc3McCommand, IDisposable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;

		/// <summary>コンストラクタ</summary>
		public McReqForceReturnToOrigin()
		{
			ClassName = GetType().Name;
			DataType = Syncdef.REQ_FORCEZRNFIN;
			DataTypeName = "REQ_FORCEZRNFIN";
		}
		/// <summary>有効軸</summary>
		private short EnableAxisValue { get; set; }
		/// <summary>有効軸</summary>
		public AxisBits EnableAxis
		{
			get { return (AxisBits)EnableAxisValue; }
			set { EnableAxisValue = (short)value; }
		}

		/// <summary>X軸</summary>
		public bool AxisX
		{
			get { return EnableAxis.HasFlag( AxisBits.X ); }
			set
			{
				AidBit bit = new AidBit();
				EnableAxisValue = bit.SetBit( EnableAxisValue, (int)AxisBits.X, value );
			}
		}
		/// <summary>Y軸</summary>
		public bool AxisY
		{
			get { return EnableAxis.HasFlag( AxisBits.Y ); }
			set
			{
				AidBit bit = new AidBit();
				EnableAxisValue = bit.SetBit( EnableAxisValue, (int)AxisBits.Y, value );
			}
		}
		/// <summary>W軸</summary>
		public bool AxisW
		{
			get { return EnableAxis.HasFlag( AxisBits.W ); }
			set
			{
				AidBit bit = new AidBit();
				EnableAxisValue = bit.SetBit( EnableAxisValue, (int)AxisBits.W, value );
			}
		}
		/// <summary>Z軸</summary>
		public bool AxisZ
		{
			get { return EnableAxis.HasFlag( AxisBits.Z ); }
			set
			{
				AidBit bit = new AidBit();
				EnableAxisValue = bit.SetBit( EnableAxisValue, (int)AxisBits.Z, value );
			}
		}
		/// <summary>A軸</summary>
		public bool AxisA
		{
			get { return EnableAxis.HasFlag( AxisBits.A ); }
			set
			{
				AidBit bit = new AidBit();
				EnableAxisValue = bit.SetBit( EnableAxisValue, (int)AxisBits.A, value );
			}
		}
		/// <summary>B軸</summary>
		public bool AxisB
		{
			get { return EnableAxis.HasFlag( AxisBits.B ); }
			set
			{
				AidBit bit = new AidBit();
				EnableAxisValue = bit.SetBit( EnableAxisValue, (int)AxisBits.B, value );
			}
		}
		/// <summary>C軸</summary>
		public bool AxisC
		{
			get { return EnableAxis.HasFlag( AxisBits.C ); }
			set
			{
				AidBit bit = new AidBit();
				EnableAxisValue = bit.SetBit( EnableAxisValue, (int)AxisBits.C, value );
			}
		}
		/// <summary>I軸</summary>
		public bool AxisI
		{
			get { return EnableAxis.HasFlag( AxisBits.I ); }
			set
			{
				AidBit bit = new AidBit();
				EnableAxisValue = bit.SetBit( EnableAxisValue, (int)AxisBits.I, value );
			}
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
		public ResultCodes Execute()
		{
			AidLog logs = new AidLog( "McReqForceReturnToOrigin.Execute" );
			ResultCodes ret = ResultCodes.McNotInitialize;
			while( true == StandBy() ) {
				try {
					FORCEZRNFIN data = FORCEZRNFIN.Init();
					{
						data.AxisFlag = EnableAxisValue;
					}
					int retRt64 = 0;
					TechnoMethods method = TechnoMethods.SendCommand;
					logs.Sure( $"SendCommand({DataTypeName},AxisFlag=0x{data.AxisFlag:x},[0x{ data.AxisFlag:x}]" );
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
