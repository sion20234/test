using System;
using ECNC3.Enumeration;
using ECNC3.Models.Common;
using static Rt64ecdata;

namespace ECNC3.Models.McIf
{
	/// <summary>4-5-11.ワーク原点座標設定コマンド </summary>
	public class McReqWorkPositionChange : McCommBasic, IEcnc3McCommand, IDisposable
    {
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;

		/// <summary>コンストラクタ</summary>
		public McReqWorkPositionChange()
		{
			ClassName = GetType().Name;
			DataType = Syncdef.REQ_WORGPOSCHG;
			DataTypeName = "REQ_WORGPOSCHG";
		}

		/// <summary>仮想点</summary>
		public StructureAxisCoordinate WorkPosition { get; set; }
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
						if( null != WorkPosition ) {
							WorkPosition.Dispose();
							WorkPosition = null;
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
				ResultCodes ret = fs.Read();
				while( ResultCodes.Success == ret ) {
					if( null == fs.Items ) {
						break;
					}
					if( 1 > fs.Items.Count ) {
						break;
					}
					//	0番目がワーク原点
					int found = fs.Items.FindIndex( ( x ) => ( 0 == x.Number ) );
					if( 0 > found ) {
						;
					} else {
						WorkPosition = fs.Items[found].Clone() as StructureAxisCoordinate;
						WorkPosition.EnableAxis = 0x11ff;
                        ret = Execute( false );
                    }
					break;
				}
				return ret;
			}
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
			AidLog logs = new AidLog( "McReqWorkPositionChange.Execute" );
			if( null == WorkPosition ) {
				return ResultCodes.InvalidArgument;
			}
			ResultCodes ret = ResultCodes.McNotInitialize;
			while( true == StandBy() ) {
				try {
					WORGPOSCHG data = WORGPOSCHG.Init();
					data.AxisFlag = WorkPosition.EnableAxis;
					data.PosAxis[0] = WorkPosition.Axis1;
					data.PosAxis[1] = WorkPosition.Axis2;
					data.PosAxis[2] = WorkPosition.Axis3;
					data.PosAxis[3] = WorkPosition.Axis4;
					data.PosAxis[4] = WorkPosition.Axis5;
					data.PosAxis[5] = WorkPosition.Axis6;
					data.PosAxis[6] = WorkPosition.Axis7;
					data.PosAxis[7] = WorkPosition.Axis8;
					data.PosAxis[8] = WorkPosition.Axis9;

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

                    using (FileOrgPos orgSave = new FileOrgPos())
                    {
                        WorkPosition.Number = 0;
                        orgSave.Write(WorkPosition);
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
