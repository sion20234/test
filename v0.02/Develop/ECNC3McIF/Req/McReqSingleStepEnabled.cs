using System;
using ECNC3.Enumeration;

namespace ECNC3.Models.McIf
{
    /// <summary>4-5-6.ドライラン有効/無効コマンド</summary>
    public class McReqSingleStepEnabled : McCommBasic, IEcnc3McCommand, IDisposable
    {
        /// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
        private bool _disposed = false;
        /// <summary>コンストラクタ</summary>
        public McReqSingleStepEnabled()
        {
            ClassName = GetType().Name;
            DataType = Syncdef.REQ_SINGLE;
            DataTypeName = "REQ_SINGLE";
        }
        ///// <summary>無効／有効</summary>
        ///// <value>
        /////		<list type="bullet" >
        /////			<item>true=クランプ</item>
        /////			<item>false=アンクランプ</item>
        /////		</list>
        ///// </value>
        //public bool Enabled { get; set; }

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
