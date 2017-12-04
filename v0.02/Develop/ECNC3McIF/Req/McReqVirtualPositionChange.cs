using System;
using ECNC3.Enumeration;
using ECNC3.Models.Common;
using static Rt64ecdata;

namespace ECNC3.Models.McIf
{
	/// <summary>4-5-10B.仮想点／測定点変更コマンド２ </summary>
	public class McReqVirtualPositionChange : McCommBasic, IEcnc3McCommand, IDisposable
    {
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;

		/// <summary>コンストラクタ</summary>
		public McReqVirtualPositionChange()
		{
			ClassName = GetType().Name;
			DataType = Syncdef.REQ_VIRPOSCHG_EX;
			DataTypeName = "REQ_VIRPOSCHG_EX";
		}

		/// <summary>仮想点</summary>
		public StructureAxisCoordinate VirtualPosition { get; set; }

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
						if( null != VirtualPosition ) {
							VirtualPosition.Dispose();
							VirtualPosition = null;
						}
					}
					//  アンマネージリソースの解放
				}
				_disposed = true;
			} finally {
				//  基底クラスのDispose()を確実に呼び出す。
				base.Dispose( disposing );
			}
		}
		/// <summary>初期化</summary>
		/// <returns>実行結果</returns>
		public ResultCodes Initialize()
		{
			using( FileOrgPos fs = new FileOrgPos() ) {
				fs.Read();
				if( null != fs.Items ) {
					if( 0 < fs.Items.Count ) {
						//	1～100までの仮想点を転送する。
						foreach( StructureAxisCoordinate item in fs.Items ) {
							if( ( 1 > item.Number ) || ( 1001 < item.Number ) ) {
								continue;
							}
							if( null != VirtualPosition ) {
								VirtualPosition.Dispose();
								VirtualPosition = null;
							}
							VirtualPosition = item.Clone() as StructureAxisCoordinate;
							Execute( false );
						}
					}
				}
			}
			return ResultCodes.Success;
		}
		/// <summary>コマンド発行</summary>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// プロパティに設定された内容によりMCボードへコマンドを発行します。
		/// </remarks>
		public ResultCodes Execute()
		{
			return Execute( true );
		}
		/// <summary>コマンド発行</summary>
		/// <param name="fileWrite">ファイルへの反映の要否</param>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// プロパティに設定された内容によりMCボードへコマンドを発行します。
		/// </remarks>
		private ResultCodes Execute( bool fileWrite )
		{
			AidLog logs = new AidLog( "McReqVirtualPositionChange.Execute" );
			if( null == VirtualPosition ) {
				return ResultCodes.InvalidArgument;
			}
			ResultCodes ret = ResultCodes.McNotInitialize;
			while( true == StandBy() ) {
				try {
					VIRPOSCHG data = VIRPOSCHG.Init();
					data.VirNo = VirtualPosition.Number - 1;
					data.VirPos[0] = VirtualPosition.Axis1;
					data.VirPos[1] = VirtualPosition.Axis2;
					data.VirPos[2] = VirtualPosition.Axis3;
					data.VirPos[3] = VirtualPosition.Axis4;
					data.VirPos[4] = VirtualPosition.Axis5;
					data.VirPos[5] = VirtualPosition.Axis6;
					data.VirPos[6] = VirtualPosition.Axis7;
					data.VirPos[7] = VirtualPosition.Axis8;
					data.VirPos[8] = VirtualPosition.Axis9;

					int retRt64 = 0;
					TechnoMethods method = TechnoMethods.SendCommand;
                    
					logs.Sure( $"SendCommand({DataTypeName},VirNo={data.VirNo},[" + logs.ToString( data.VirPos ) + "]" );
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
