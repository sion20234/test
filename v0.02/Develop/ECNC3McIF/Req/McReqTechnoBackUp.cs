using System;
using ECNC3.Enumeration;
using ECNC3.Models.Common;

namespace ECNC3.Models.McIf
{
    /// <summary>ECNC3 4-5-54.パラメータバックアップコマンド</summary>
    public class McReqTechnoBackUp : McCommBasic, IEcnc3McCommand, IDisposable
    {
        /// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
        private bool _disposed = false;
        /// <summary>コンストラクタ</summary>
        public McReqTechnoBackUp()
        {
            ClassName = GetType().Name;
            DataType = Syncdef.REQ_REFPRM;
            DataTypeName = "REQ_REFPRM";
        }
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
        /// <summary>コマンド発行</summary>
        /// <returns>実行結果</returns>
        /// <remarks>
        /// プロパティに設定された内容によりMCボードへコマンドを発行します。
        /// </remarks>
        public ResultCodes Execute()
        {
            return ExecuteByNonParam();
        }
    }
}
