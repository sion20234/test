using System;
using System.Runtime.InteropServices;
using ECNC3.Enumeration;
using ECNC3.Models.Common;
using static Rt64ecdata;
using System.Linq;

namespace ECNC3.Models.McIf
{
    public enum MacroManage
    {
        /// <summary>R#1</summary>
        AccessErrorMacro = 1,
        /// <summary>RW#1000～#1099</summary>
        GlobalMacro = 1000,
        GlobalMacroLimit = 1099,
        /// <summary>R#1500～#1509</summary>
        InitialPrmMacro = 1500,
        InitialPrmMacroLimit = 1509,
        /// <summary>R#4000～#4899</summary>
        PositionMacro = 4000,
        PositionMacroLimit = 4899,
        /// <summary>R#5000～#5009</summary>
        EndFaceMacro = 5000,
        EndFaceMacroLimit = 5009,
        /// <summary>R#5020～#5021</summary>
        MessageResultMacro = 5020,
        MessageResultMacroLimit = 5021,
        /// <summary>R#5040～#5049</summary>
        CameraResultMacro = 5040,
        CameraResultMacroLimit = 5049,
        /// <summary>R#5050</summary>
        PierceAndMissedResultMacro = 5050,
        /// <summary>R#5060～#5068</summary>
        ProcEndPosMacro = 5060,
        ProcEndPosMacroLimit = 5068,
        /// <summary>RW#5070</summary>
        ProcSkipFlagMacro = 5070,
        /// <summary>R#5080～#5081</summary>
        CenteringResultMacro = 5080,
        CenteringResultMacroLimit = 5081,
        /// <summary>RW#5090～#5093</summary>
        GuidClampCheckMacro = 5090,
        GuidClampCheckMacroLimit = 5093,
        /// <summary>RW#10000～#99999</summary>
        ExtensionMacro = 10000,
        ExtensionMacroLimit = 99999,
        /// <summary>R#1～#99999</summary>
        AllMacro = -1,
        /// <summary>個別</summary>
        Particul = 0

    }
	/// <summary>4-4-6B.仮想点／測定点設定読出２</summary>
	public class McDatMacroManage : McCommBasic, IDisposable
    {
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>コンストラクタ</summary>
		public McDatMacroManage(MacroManage mode)
		{
            _manageMode = mode;
            Initialize();
        }

        private MacroManage _manageMode;

        /// <summary>マクロ情報リスト</summary>
        public StructureMacroManageList Items;

        /// <summary>対象のマクロ変数番号</summary>
        public int StartNo { get; set; }
        /// <summary>マクロ変数の要素数</summary>
        public int TargetCount { get; set; }

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

        /// <summary>
        /// マクロ情報とNC通信処理用の初期化処理
        /// </summary>
        /// <param name="mode">マクロ種別</param>
        /// <returns>初期化結果</returns>
        /// <remarks>
        /// クラス生成時に実行される。モードによりメンバーの要素数と
        /// テクノ通信処理用のパラメータを設定する。
        /// </remarks>
        private ResultCodes Initialize()
        {
            ResultCodes ret = ResultCodes.Success;
            //マクロ情報初期化
            if(Items != null)
            {
                Items.Clear();
                Items.TrimExcess();
                Items = null;
            }            

            switch(_manageMode)
            {
                case MacroManage.AccessErrorMacro:

                    break;

                case MacroManage.GlobalMacro:

                    break;

                case MacroManage.InitialPrmMacro:

                    break;

                case MacroManage.PositionMacro:

                    break;

                case MacroManage.EndFaceMacro:

                    break;

                case MacroManage.MessageResultMacro:

                    break;

                case MacroManage.CameraResultMacro:

                    break;

                case MacroManage.PierceAndMissedResultMacro:

                    break;

                case MacroManage.ProcEndPosMacro:

                    break;

                case MacroManage.ProcSkipFlagMacro:

                    break;

                case MacroManage.CenteringResultMacro:

                    break;

                case MacroManage.GuidClampCheckMacro:

                    break;

                case MacroManage.ExtensionMacro:

                    break;

                case MacroManage.AllMacro:

                    break;

                case MacroManage.Particul:
                    
                        break;
            }




            return ret;
        }

        /// <summary>
        /// マクロ情報とNC通信処理用の初期化処理
        /// </summary>
        /// <param name="mode">マクロ種別</param>
        /// <returns>初期化結果</returns>
        /// <remarks>
        /// クラス生成時に実行される。モードによりメンバーの要素数と
        /// テクノ通信処理用のパラメータを設定する。
        /// </remarks>
        public ResultCodes Read()
        {
            ResultCodes ret = ResultCodes.Success;

            switch (_manageMode)
            {
                case MacroManage.AccessErrorMacro:

                    break;

                case MacroManage.GlobalMacro:

                    break;

                case MacroManage.InitialPrmMacro:

                    break;

                case MacroManage.PositionMacro:

                    break;

                case MacroManage.EndFaceMacro:

                    break;

                case MacroManage.MessageResultMacro:

                    break;

                case MacroManage.CameraResultMacro:

                    break;

                case MacroManage.PierceAndMissedResultMacro:

                    break;

                case MacroManage.ProcEndPosMacro:

                    break;

                case MacroManage.ProcSkipFlagMacro:

                    break;

                case MacroManage.CenteringResultMacro:

                    break;

                case MacroManage.GuidClampCheckMacro:

                    break;

                case MacroManage.ExtensionMacro:
                    if(Items == null)
                    {
                        Items = new StructureMacroManageList();
                    }
                    else
                    {
                        Items.Clear();
                        Items.TrimExcess();
                        Items = null;
                        Items = new StructureMacroManageList();
                    }
                    using (Models.McIf.McReqMacroNVariableVal macroVal = new Models.McIf.McReqMacroNVariableVal())
                    {
                        ret = macroVal.Read();
                        for (int macroNum = 0; macroNum < macroVal.Item.Count(); macroNum++)
                        {
                            StructureMacroManageItem item = new StructureMacroManageItem();
                            item.Number = macroNum + 10000;
                            item.Value = macroVal.Item[macroNum].ToString();
                            Items.Add(item);
                        }
                    }
                    break;

                case MacroManage.AllMacro:

                    break;

                case MacroManage.Particul:
                    using (McReqOneVariable particul = new McReqOneVariable(StartNo))
                    {
                        particul.Read();
                    }
                    break;
            }
            return ret;
        }


        /// <summary>
        /// マクロ情報とNC通信処理用の初期化処理
        /// </summary>
        /// <param name="mode">マクロ種別</param>
        /// <returns>初期化結果</returns>
        /// <remarks>
        /// クラス生成時に実行される。モードによりメンバーの要素数と
        /// テクノ通信処理用のパラメータを設定する。
        /// </remarks>
        public ResultCodes Write()
        {
            ResultCodes ret = ResultCodes.Success;

            switch (_manageMode)
            {
                case MacroManage.AccessErrorMacro:

                    break;

                case MacroManage.GlobalMacro:

                    break;

                case MacroManage.InitialPrmMacro:

                    break;

                case MacroManage.PositionMacro:

                    break;

                case MacroManage.EndFaceMacro:

                    break;

                case MacroManage.MessageResultMacro:

                    break;

                case MacroManage.CameraResultMacro:

                    break;

                case MacroManage.PierceAndMissedResultMacro:

                    break;

                case MacroManage.ProcEndPosMacro:

                    break;

                case MacroManage.ProcSkipFlagMacro:

                    break;

                case MacroManage.CenteringResultMacro:

                    break;

                case MacroManage.GuidClampCheckMacro:

                    break;

                case MacroManage.ExtensionMacro:

                    if (Items == null)
                    {
                        return ResultCodes.McNotInitialize;
                    }
                    using (Models.McIf.McReqMacroNVariableVal macroVal = new Models.McIf.McReqMacroNVariableVal())
                    {
                        ret = macroVal.Read();

                        for (int macroNum = 0; macroNum < macroVal.Item.Count(); macroNum++)
                        {
                            if (Items[macroNum].Number == 0
                                || Items[macroNum].Value == null)
                            {
                                continue;
                            }
                            if (!double.TryParse(Items[macroNum].Value, out macroVal.Item[macroNum]))
                            {
                                continue;
                            }
                        }
                        if (ret != ResultCodes.Success)
                        {
                            return ret;
                        }
                        ret = macroVal.Write();
                    }
                    break;

                case MacroManage.AllMacro:

                    break;

                case MacroManage.Particul:
                    if (Items == null)
                    {
                        return ResultCodes.McNotInitialize;
                    }
                    using (McReqOneVariable particul = new McReqOneVariable(StartNo))
                    {
                        particul.Read();
                        if(!double.TryParse(Items[0].Value, out particul.MacroData.var))
                        {
                            return ret;
                        }
                        particul.Write();
                    }                    
                    break;
            }
            return ret;
        }
    }
}
