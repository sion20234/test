using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECNC3.Enumeration;
using ECNC3.Models.Common;
using ECNC3.Models.McIf;
using static Rt64ecdata;

namespace ECNC3.Models.McIf
{
	/// <summary>N4-2-27.送りオーバライド変更コマンド</summary>
	public class McReqOverrideChange : McCommBasic, IEcnc3McCommand, IDisposable
    {
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;

		/// <summary>コンストラクタ</summary>
		public McReqOverrideChange()
		{
			ClassName = GetType().Name;
		}
		/// <summary>オーバライド種別</summary>
		private OverrideModes SelectedMode { get; set; }
		/// <summary>オーバライド設定モード</summary>
		public OverrideModes OverrideMode
		{
			get { return SelectedMode; }
			set
			{
				if( OverrideModes.Overall == value ) {
					DataType = Syncdef.REQ_OVRDCHGP;
					DataTypeName = "REQ_OVRDCHGP";
				} else if( OverrideModes.Interpolation == value ) {
					DataType = Syncdef.REQ_COVRDCHGP;
					DataTypeName = "REQ_COVRDCHGP";
				} else if( OverrideModes.Spindle == value ) {
					DataType = Syncdef.REQ_SOVRDCHGP;
					DataTypeName = "REQ_SOVRDCHGP";
				} else {
					DataType = 0;
					DataTypeName = "NOT SET";
				}
			}
		}
		/// <summary>設定値</summary>
		public short SettingValue { get; set; }

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
			AidLog logs = new AidLog( "McReqOverrideChange.Execute" );
			ResultCodes ret = ResultCodes.McNotInitialize;
			while( true == StandBy() ) {
				try {
					if( ( Syncdef.REQ_OVRDCHGP != DataType ) &&
						( Syncdef.REQ_COVRDCHGP != DataType ) &&
						( Syncdef.REQ_SOVRDCHGP != DataType ) ) {
						break;
					}
					OVERCHG data = OVERCHG.Init();
					data.Override = SettingValue;
					int retRt64 = 0;
					TechnoMethods method = TechnoMethods.SendCommand;
					logs.Sure( $"SendCommand({DataTypeName},Override={data.Override})" );
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
