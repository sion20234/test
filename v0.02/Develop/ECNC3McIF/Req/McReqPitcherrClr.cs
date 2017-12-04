using System;
using ECNC3.Enumeration;
using ECNC3.Models.Common;
using System.Diagnostics;

namespace ECNC3.Models.McIf
{
	/// <summary>N4-2-13.リセットコマンド</summary>
	public class McReqPitcherrClr : McCommBasic, IEcnc3McCommand, IDisposable
    {
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>コンストラクタ</summary>
		public McReqPitcherrClr()
		{
			ClassName = GetType().Name;
			DataType = Syncdef.REQ_PITCHERR_CLR;
			DataTypeName = "REQ_PITCHERR_CLR";
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
            StackFrame stack = new StackFrame(1);
            AidLog logs = new AidLog($"{stack.GetMethod().ReflectedType.Name}.{stack.GetMethod().Name}");
            ResultCodes ret = ResultCodes.McNotInitialize;
            while (true == StandBy())
            {
                try
                {
                    int retRt64 = 0;
                    TechnoMethods method = TechnoMethods.SendCommand;
                    logs.Sure($"SendCommand({DataTypeName})");
                    if (true == BootMode.HasFlag(BootModes.Machine))
                    {
                        retRt64 = Rt64eccomapi.SendCommand(CommHandle, DataType, Task, IntPtr.Zero);
                        ret = CheckResultTechno(method, retRt64);
                        if (ResultCodes.Success != ret)
                        {
                            break;
                        }
                    }
                    if (true == BootMode.HasFlag(BootModes.Desktop))
                    {
                        //retRt64 = Rt64eccomapiWrap.SendCommand(CommHandle, DataType, Task, IntPtr.Zero);
                        //ret = CheckResultDebug(method, retRt64);
                        ret = ResultCodes.Success;
                        if (ResultCodes.Success != ret)
                        {
                            break;
                        }
                    }
                }
                catch (Exception e)
                {
                    bool unexpected = true;
                    if ((e is DllNotFoundException) ||
                        (e is EntryPointNotFoundException))
                    {
                        unexpected = false;   //	想定内の例外。
                    }
                    ret = logs.Exception(e, unexpected);
                }
                break;
            }
            return ret;

            //if ((int)BootMode < (int)BootModes.Desktop) return ResultCodes.Success;
            //return CheckResultTechno(TechnoMethods.SendCommand, (int)ExecuteByNonParam());
		}
	}
}
