using System;
using System.Runtime.InteropServices;
using ECNC3.Enumeration;
using ECNC3.Models.Common;
using static Rt64ecdata;

namespace ECNC3.Models.McIf
{
    /// <summary>4-4-27.マクロ変数データ任意数書込/読出</summary>
    public class McReqMacroNVariableVal : McCommBasic, IDisposable
    {
        /// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
        private bool _disposed = false;

        public double[] Item;
        /// <summary>コンストラクタ</summary>
        public McReqMacroNVariableVal()
        {
            ClassName = GetType().Name;
            DataType = Syncdef.DAT_NVARIABLE_VAL;
            DataTypeName = "DAT_NVARIABLE_VAL";
            Item = new double[90000];
            //Item = EXTVARIABLE.Init();
            Reset();
        }
        /// <remarks>
        /// 書き込み時のみ必要な設定です。
        /// </remarks>
        /// どのプログラムでエラーが発生したのかを示します。
        /// </remarks>
        public ProgramCodeTypes ErrorType { get; private set; }
        /// <summary>エラー発生コード</summary>

        /// <summary>インスタンスの破棄</summary>
        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);    //  ファイナライザによるDispose()呼び出しの抑制。
        }
        /// <summary>インスタンスの破棄</summary>
        /// <param name="disposing">呼び出し元の判別
        ///     <list type="bullet" >
        ///         <item>true=Dispose()関数からの呼び出し。</item>
        ///         <item>false=ファイナライザによる呼び出し。</item>
        ///     </list>
        /// </param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (false == _disposed)
                {
                    if (true == disposing)
                    {
                        //  マネージリソースの解放
                    }
                    //  アンマネージリソースの解放
                }
                _disposed = true;
            }
            finally
            {
                //  基底クラスのDispose()を確実に呼び出す。
                base.Dispose(disposing);
            }
        }
        //#if __KEY_USAGE_UNKNOWN__
        /// <summary>読み込み</summary>
        /// <returns>実行結果</returns>
        public ResultCodes Read()
        {
            AidLog logs = new AidLog("McReqMacroNVariableVal.Read");
            double[] data;
            ResultCodes ret = ReadData(out data);
            if (ret == ResultCodes.Success)
            {
                Copy(data, ref Item);
            }
            return ret;
        }
        /// <summary>MCデータ読み込み</summary>
        /// <param name="data">MCより読み込まれたデータ</param>
        /// <returns>実行結果</returns>
        /// <remarks>
        /// MCより情報を取得します。
        /// </remarks>
        private ResultCodes ReadData(out double[] data)
        {
            AidLog logs = new AidLog("McReqMacroNVariableVal.ReadData");
            //	データ数はＤＡＴ＿ＶＡＲＩＡＢＬＥのデータ数最大まで。
            data = new double[90000];
            if (false == Verify(false))
            {
                return ResultCodes.InvalidArgument;
            }
            ResultCodes ret = ResultCodes.McNotInitialize;
            while (true == StandBy())
            {
                try
                {
                    int retRt64 = 0;
                    int sz = 0;
                    int sts = 0;
                    Rt64ecdata.NVARIABLE_PRE nvarPre = Rt64ecdata.NVARIABLE_PRE.Init();
                    Rt64ecdata.EXTVARIABLE extVar_r = Rt64ecdata.EXTVARIABLE.Init();//受信用構造体
                                                                                    //アンマネージドメモリの確保(送受信共用)
                    IntPtr extVarPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(extVar_r));

                    //マクロ変数送受信データ数の設定
                    nvarPre.Start = 10000;
                    nvarPre.Num = 90000;
                    TechnoMethods method = TechnoMethods.ReceiveData;
                    if (BootModes.Desktop == BootMode)
                    {
                        //マクロ変数　受信データ数の送信
                        sts = Rt64eccomapiWrap.SendData(CommHandle, Syncdef.DAT_NVARIABLE_PRE, 0, 0, Marshal.SizeOf(nvarPre), ref nvarPre);
                        //マクロ変数受信※アンマネージドメモリに対して実行する
                        sts = Rt64eccomapiWrap.ReceiveData(CommHandle, Syncdef.DAT_NVARIABLE_VAL, 0, 0, ref sz, extVarPtr);
                        //アンマネージドメモリからマネージド構造体へのコピー
                        extVar_r = (Rt64ecdata.EXTVARIABLE)Marshal.PtrToStructure(extVarPtr, extVar_r.GetType());
                        //アンマネージドメモリの解放
                        Marshal.FreeCoTaskMem(extVarPtr);
                        for (int ct = 0; ct < extVar_r.Var.Length; ct++)
                        {
                            data[ct] = extVar_r.Var[ct];
                        }
                    }
                    else
                    {
                        //マクロ変数　受信データ数の送信
                        sts = Rt64eccomapi.SendData(CommHandle, Syncdef.DAT_NVARIABLE_PRE, 0, 0, Marshal.SizeOf(nvarPre), ref nvarPre);
                        //マクロ変数受信※アンマネージドメモリに対して実行する
                        sts = Rt64eccomapi.ReceiveData(CommHandle, Syncdef.DAT_NVARIABLE_VAL, 0, 0, ref sz, extVarPtr);
                        //アンマネージドメモリからマネージド構造体へのコピー
                        extVar_r = (Rt64ecdata.EXTVARIABLE)Marshal.PtrToStructure(extVarPtr, extVar_r.GetType());
                        //アンマネージドメモリの解放
                        Marshal.FreeCoTaskMem(extVarPtr);
                        for (int ct = 0; ct < extVar_r.Var.Length; ct++)
                        {
                            data[ct] = extVar_r.Var[ct];
                        }
                    }
                    ret = CheckResultTechno(method, retRt64);
                    if (ResultCodes.Success != ret) { break; }
                }
                catch (Exception e)
                {
                    bool unexpected = true;
                    if ((e is DllNotFoundException) ||
                        (e is EntryPointNotFoundException) ||
                        (e is AccessViolationException))
                    {
                        unexpected = false;   //	想定内の例外。
                    }
                    ret = logs.Exception(e, unexpected);
                }
                break;
            }
            return ret;
        }
        //#endif
        /// <summary>書き込み</summary>
        /// <returns>実行結果</returns>
        public unsafe ResultCodes Write()
        {
            AidLog logs = new AidLog("McReqMacroNVariableVal.Write");
            EXTVARIABLE data = EXTVARIABLE.Init();
            for (int ct = 0; ct < 10000; ct++)
            {
                data.Var[ct] = Item[ct];
            }
            //	データを直接設定することによるダウンロード
            ResultCodes ret = WriteData(data);

            return ResultCodes.Success;
        }
        /// <summary>書き込み</summary>
        /// <param name="data">書き込み情報</param>
        /// <returns>実行結果</returns>
        private unsafe ResultCodes WriteData(EXTVARIABLE data)
        {
            AidLog logs = new AidLog("McReqMacroNVariableVal.WriteData");
            ResultCodes ret = ResultCodes.McNotInitialize;
            while (true == StandBy())
            {
                try
                {
                    int retRt64 = 0;
                    TechnoMethods method = TechnoMethods.SendData;
                    //int sz = 0;
                    int sts = 0;
                    Rt64ecdata.NVARIABLE_PRE nvarPre = Rt64ecdata.NVARIABLE_PRE.Init();
                    //アンマネージドメモリの確保(送受信共用)
                    IntPtr extVarPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(data));
                    //マクロ変数送受信データ数の設定
                    nvarPre.Start = 10000;
                    nvarPre.Num = 90000;

                    logs.Sure($"SendData({DataTypeName}");
                    if (BootModes.Desktop == BootMode)
                    {
                        //マクロ変数　送信データ数の送信
                        sts = Rt64eccomapi.SendData(CommHandle, Syncdef.DAT_NVARIABLE_PRE, 0, 0, Marshal.SizeOf(nvarPre), ref nvarPre);
                        //マネージ構造体からアンマネージドメモリにコピー
                        Marshal.StructureToPtr(data, extVarPtr, false);
                        //マクロ変数送信※アンマネージドメモリに対して実行する
                        retRt64 = Rt64eccomapiWrap.SendData(CommHandle, Syncdef.DAT_NVARIABLE_VAL, 0, 0, Marshal.SizeOf(data), extVarPtr);
                        //アンマネージドメモリの解放
                        Marshal.FreeCoTaskMem(extVarPtr);
                    }
                    else
                    {
                        //マクロ変数　送信データ数の送信
                        sts = Rt64eccomapi.SendData(CommHandle, Syncdef.DAT_NVARIABLE_PRE, 0, 0, Marshal.SizeOf(nvarPre), ref nvarPre);
                        //マネージ構造体からアンマネージドメモリにコピー
                        Marshal.StructureToPtr(data, extVarPtr, false);
                        //マクロ変数送信※アンマネージドメモリに対して実行する
                        retRt64 = Rt64eccomapi.SendData(CommHandle, Syncdef.DAT_NVARIABLE_VAL, 0, 0, Marshal.SizeOf(data), extVarPtr);
                        //アンマネージドメモリの解放
                        Marshal.FreeCoTaskMem(extVarPtr);
                    }



                    //int size = System.Runtime.InteropServices.Marshal.SizeOf(data);
                    //int param = 0;
                    //int retRt64 = 0;
                    //EXTVARIABLE* ptr = &data;
                    //TechnoMethods method = TechnoMethods.SendData;
                    //logs.Sure($"SendData({DataTypeName}");
                    //if (BootModes.Desktop == BootMode)
                    //{
                    //    //retRt64 = Rt64eccomapiWrap.SendData(CommHandle, DataType, Task, param, size, ptr);
                    //    using (FileMacroManage macroManage = new FileMacroManage())
                    //    {
                    //        macroManage.Read();
                    //        for (int ict = 0; ict < macroManage.Items.Count; ict++)
                    //        {
                    //            macroManage.Items[ict].Value = data.Var[ict].ToString();
                    //        }
                    //        macroManage.Write();
                    //    }
                    //}
                    //else
                    //{
                    //    retRt64 = Rt64eccomapi.SendData(CommHandle, DataType, Task, param, size, ptr);
                    //}
                    ret = CheckResultTechno(method, retRt64);
                    if (ResultCodes.Success != ret)
                    {
                        break;
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
        /// <summary>パラメータチェック</summary>
        /// <param name="isWrite">実行内容
        ///		<list type="bullet" >
        ///			<item>true=書き込み処理</item>
        ///			<item>false=読み込み処理</item>
        ///		</list>
        /// </param>
        /// <returns>判定結果
        ///		<list type="bullet" >
        ///			<item>true=正当</item>
        ///			<item>false=不当</item>
        ///		</list>
        /// </returns>
        /// <remarks>
        /// 送信パラメータの正当性チェックを行います。
        /// </remarks>
        private bool Verify(bool isWrite)
        {
            return true;
        }
        /// <summary>応答メンバ変数初期化</summary>
        /// <remarks>
        /// Executeメソッドの呼び出しにより設定される各プロパティ値の初期化を実施します。
        /// </remarks>
        private void Reset()
        {
            ErrorType = ProgramCodeTypes.Unknown;
        }
    }
}
