using System;
using ECNC3.Enumeration;
using ECNC3.Models.Common;
using static Rt64ecdata;

namespace ECNC3.Models.McIf
{
	/// <summary>N4-2-2.動作モード変更コマンド</summary>
	public class McReqModeChange : McCommBasic, IEcnc3McCommand, IDisposable
    {
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;

		/// <summary>コンストラクタ</summary>
		public McReqModeChange()
		{
			ClassName = GetType().Name;
			DataType = Syncdef.REQ_MODECHG;
			DataTypeName = "REQ_MODECHG";
		}
		/// <summary>動作モード</summary>
		public McTaskModes TaskMode { private get; set; }

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
			AidLog logs = new AidLog( "McReqModeChange.Execute" );
			if( true == StandBy() ) {
				MODECHG data = MODECHG.Init();
				data.mode = (short)TaskMode;
				int ret = 0;
				logs.Sure( $"SendCommand({DataTypeName},mode={data.mode}({TaskMode}))" );
				while( true ) {
					if( true == BootMode.HasFlag( BootModes.Machine ) ) {
						ret = Rt64eccomapi.SendCommand( CommHandle, DataType, Task, ref data );
					}
					if( true == BootMode.HasFlag( BootModes.Desktop ) ) {
						ret = Rt64eccomapiWrap.SendCommand( CommHandle, DataType, Task, ref data );
					}
					if( Syncdef.E_OK != ret ) {
						logs.Error( $"SendCommand(Comm={CommHandle},Type={DataTypeName}(0x{DataType:x}))={ret}" );
						if( true == CanCommContinue() ) {
							continue;
						}
						return ConvertReturnCode( ret );
					}
					break;
				}
				return ResultCodes.Success;
			}
			return ResultCodes.McNotInitialize;
		}
        /// <summary>コマンド発行(タスク指定)</summary>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// プロパティに設定された内容によりMCボードへコマンドを発行します。
		/// </remarks>
		public ResultCodes Execute(short task = 0)
        {
            AidLog logs = new AidLog("McReqModeChange.Execute");
            if (true == StandBy())
            {
                MODECHG data = MODECHG.Init();
                data.mode = (short)TaskMode;
                int ret = 0;
                logs.Sure($"SendCommand({DataTypeName},mode={data.mode}({TaskMode}))"
                            + ((task < 0) ? "[AllTasksApply]" : ""));
                while (true)
                {
                    if (true == BootMode.HasFlag(BootModes.Machine))
                    {
                        if(task < 0)
                        {
                            for(short taskCount = 0; taskCount < 8; taskCount++)
                            {
                                ret = Rt64eccomapi.SendCommand(CommHandle, DataType, taskCount, ref data);
                            }
                        }
                        else
                        {
                            ret = Rt64eccomapi.SendCommand(CommHandle, DataType, task, ref data);
                        }
                    }
                    if (true == BootMode.HasFlag(BootModes.Desktop))
                    {
                        ret = Rt64eccomapiWrap.SendCommand(CommHandle, DataType, Task, ref data);
                    }
                    if (Syncdef.E_OK != ret)
                    {
                        logs.Error($"SendCommand(Comm={CommHandle},Type={DataTypeName}(0x{DataType:x}))={ret}");
                        if (true == CanCommContinue())
                        {
                            continue;
                        }
                        return ConvertReturnCode(ret);
                    }
                    break;
                }
                return ResultCodes.Success;
            }
            return ResultCodes.McNotInitialize;
        }
    }
}
