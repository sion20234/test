using System;
using ECNC3.Enumeration;
using ECNC3.Models.Common;
using static Rt64ecdata;
using System.Windows.Forms;

namespace ECNC3.Models.McIf
{
	/// <summary>4-2-21(標準) 原点復帰コマンド</summary>
	/// <remarks>
	/// REQ_ALLZRN
	/// </remarks>
	public class McReqReturnToOrigin : McCommBasic, IEcnc3McCommand, IDisposable
    {
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>コンストラクタ</summary>
		public McReqReturnToOrigin()
		{
			ClassName = GetType().Name;
			DataType = Syncdef.REQ_ALLZRN;
			DataTypeName = "REQ_ALLZRN";
		}
        public McReqReturnToOrigin(AxisNumbers Num)
        {
            ClassName = GetType().Name;
            DataType = Syncdef.REQ_ZRNSTART;
            DataTypeName = "REQ_ZRNSTART";
            SelectAxis = Num;

        }
        private AxisNumbers SelectAxis = AxisNumbers.Unknown;

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
            if(DataType == Syncdef.REQ_ZRNSTART)
            {
                AidLog logs = new AidLog("McReqVirtualPositionChange.Execute");
                if (AxisNumbers.Unknown == SelectAxis)
                {
                    return ResultCodes.InvalidArgument;
                }
                ResultCodes ret = ResultCodes.McNotInitialize;
                while (true == StandBy())
                {
                    try
                    {
                        ZRNSTART data = ZRNSTART.Init();
                        data.AxisFlag = (short)(AxisNumberToBit(SelectAxis));

                        int retRt64 = 0;
                        TechnoMethods method = TechnoMethods.SendCommand;

                        logs.Sure($"SendCommand({DataTypeName},SelectAxis={data.AxisFlag}");
                        if (true == BootMode.HasFlag(BootModes.Machine))
                        {
                            retRt64 = Rt64eccomapi.SendCommand(CommHandle, DataType, Task, ref data);
                            ret = CheckResultTechno(method, retRt64);
                            if (ResultCodes.Success != ret)
                            {
                                break;
                            }
                        }
                        if (true == BootMode.HasFlag(BootModes.Desktop))
                        {
                            retRt64 = Rt64eccomapiWrap.SendCommand(CommHandle, DataType, Task, ref data);
                            ret = CheckResultDebug(method, retRt64);
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
            }
            else
            {
                return ExecuteByNonParam();
            }       
		}
        /// <summary>軸番号からビット配置への変換</summary>
		/// <param name="number">軸番号</param>
		/// <returns>変換結果</returns>
		protected int AxisNumberToBit(AxisNumbers number)
        {
            return (AxisNumbers.X == number) ? (int)AxisBits.X :
                (AxisNumbers.Y == number) ? (int)AxisBits.Y :
                (AxisNumbers.W == number) ? (int)AxisBits.W :
                (AxisNumbers.Z == number) ? (int)AxisBits.Z :
                (AxisNumbers.A == number) ? (int)AxisBits.A :
                (AxisNumbers.B == number) ? (int)AxisBits.B :
                (AxisNumbers.C == number) ? (int)AxisBits.C :
                (AxisNumbers.I == number) ? (int)AxisBits.I : 0;
        }
    }
}