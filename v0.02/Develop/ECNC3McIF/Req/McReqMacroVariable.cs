﻿using System;
using System.Runtime.InteropServices;
using ECNC3.Enumeration;
using ECNC3.Models.Common;
using static Rt64ecdata;

namespace ECNC3.Models.McIf
{
    /// <summary>4-4-20.マクロ変数（一般レジスタ）書込／読出（専用化）</summary>
    public class McReqMacroVariable : McCommBasic, IDisposable
    {
        /// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
        private bool _disposed = false;

        /// <summary>取得結果</summary>
        private const ushort addressmin = 1000;
        private const ushort addressmax = 1099;
        private const ushort macrodatamax = 100;

        /// <summary>コンストラクタ</summary>
        public McReqMacroVariable(int startno, ushort datanum)
        {
            ClassName = GetType().Name;
            DataType = Syncdef.DAT_VARIABLE;
            DataTypeName = "DAT_VARIABLE";
            StartMacroNumber = startno;
            MacroDataNum = datanum;
            Reset();
        }
        /// <remarks>
        /// 書き込み時のみ必要な設定です。
        /// </remarks>
        /// <summary>先頭マクロ変数番号</summary>
        public int StartMacroNumber { get; set; }
        /// <summary>データ数</summary>
        public int MacroDataNum { get; set; }
        /// <summary>マクロ変数データ</summary>
        public VARIABLE MacroData;

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
            //	プログラムを読み取った際の使途が不明確なので保留。
            AidLog logs = new AidLog("McDatMacroVariable.Read");
            VARIABLE data;
            ResultCodes ret = ReadData(out data);
            if (ResultCodes.Success != ret)
            {
                return ret;
            }
            //if (null != MacroData.Var)
            //{
            //    MacroData.Var = new double[] { };
            //    MacroData.Var = null;
            //}
            MacroData.Var = new double[data.Var.Length];
            //Buffer.BlockCopy(data, 0, MacroData, 0, data.Length);
            return ret;
        }
        /// <summary>MCデータ読み込み</summary>
        /// <param name="data">MCより読み込まれたデータ</param>
        /// <returns>実行結果</returns>
        /// <remarks>
        /// MCより情報を取得します。
        /// </remarks>
        private ResultCodes ReadData(out VARIABLE data)
        {
            AidLog logs = new AidLog("McDatMacroVariable.ReadData");
            //	データ数はＤＡＴ＿ＶＡＲＩＡＢＬＥのデータ数最大まで。
            data.Var = new double[MacroDataNum];
            if (false == Verify(false))
            {
                return ResultCodes.InvalidArgument;
            }
            ResultCodes ret = ResultCodes.McNotInitialize;
            //data = INITIALPRM.Init();
            while (true == StandBy())
            {
                try
                {
                    int size = data.Var.Length;

                    //int size = Marshal.SizeOf( data );
                    int param = MacroDataNum + StartMacroNumber << 16;
                    int retRt64 = 0;
                    TechnoMethods method = TechnoMethods.ReceiveData;
                    if (BootModes.Desktop == BootMode)
                    {
                        retRt64 = Rt64eccomapiWrap.ReceiveData(CommHandle, DataType, Task, param, ref size, ref data);
                        ret = CheckResultDebug(method, retRt64);
                        if (ResultCodes.Success != ret)
                        {
                            break;
                        }
                    }
                    else
                    {
                        retRt64 = Rt64eccomapi.ReceiveData(CommHandle, DataType, Task, param, ref size, ref data);
                        ret = CheckResultTechno(method, retRt64);
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
        //#endif
        /// <summary>書き込み</summary>
        /// <returns>実行結果</returns>
        public ResultCodes Write()
        {
            AidLog logs = new AidLog("McDatMacroVariable.Write");
            ResultCodes ret = ResultCodes.InvalidArgument;
            Reset();
            //	データを直接設定することによるダウンロード
            ret = WriteData(MacroData);

            return ret;
        }
        /// <summary>書き込み</summary>
        /// <param name="data">書き込み情報</param>
        /// <returns>実行結果</returns>
        private ResultCodes WriteData(VARIABLE data)
        {
            AidLog logs = new AidLog("McDatMacroVariable.WriteData");
            if (false == Verify(true))
            {
                logs.Error($"B#{StartMacroNumber},P#{MacroDataNum}");
                return ResultCodes.InvalidArgument;
            }
            ResultCodes ret = ResultCodes.McNotInitialize;
            while (true == StandBy())
            {
                try
                {
                    int size = data.Var.Length * 8;
                    int param = MacroDataNum + StartMacroNumber << 16;
                    int retRt64 = 0;
                    TechnoMethods method = TechnoMethods.SendData;
                    logs.Sure($"SendData({DataTypeName},Param=0x{param:x8}(B#{StartMacroNumber},P#{MacroDataNum}),Size={size})");
                    if (BootModes.Desktop == BootMode)
                    {
                        retRt64 = Rt64eccomapiWrap.SendData(CommHandle, DataType, Task, param, size,  ref data);
                        ret = CheckResultDebug(method, retRt64);
                        if (ResultCodes.Success != ret)
                        {
                            break;
                        }
                    }
                    else
                    {
                        retRt64 = Rt64eccomapi.SendData(CommHandle, DataType, Task, param, size,  ref data);
                        ret = CheckResultTechno(method, retRt64);
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
            while (true)
            {
                if ((addressmin > StartMacroNumber) || (addressmax < StartMacroNumber))
                {
                    // マクロ変数範囲外
                    break;
                }

                if (0 == MacroDataNum)
                {
                    MacroDataNum = macrodatamax;
                }
                if ((0 > MacroDataNum) || (macrodatamax < MacroDataNum))
                {
                    // 最大データ数範囲外
                    break;
                }

                if ((addressmax > StartMacroNumber + MacroDataNum))
                {
                    // マクロ変数範囲外
                    break;
                }

                return true;
            }

            return false;
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