using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECNC3.Models
{
    public class StructureParamTableItem : ICloneable, IDisposable
    {
        /// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;

        /// <summary>P番号</summary>
        public int Number { get; set; }
        /// <summary>項目名</summary>
        public string ViewName { get; set; }
        /// <summary>上限値</summary>
        public string Upper { get; set; }
        /// <summary>下限値</summary>
        public string Lower { get; set; }

        /// <summary>インスタンスの破棄</summary>
        public void Dispose()
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
        protected void Dispose(bool disposing)
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
                ;   // base.Dispose( disposing );
            }
        }
        /// <summary>クローン</summary>
        /// <returns>クローンされたインスタンス</returns>
        public object Clone()
        {
            StructureParamTableItem temp = new StructureParamTableItem();
            temp.Number = Number;
            temp.ViewName = ViewName;
            temp.Lower = Lower;
            temp.Upper = Upper;
            return temp;
        }
        /// <summary>コピー</summary>
        /// <param name="source">コピー元</param>
        public void Copy(StructureParamTableItem source)
        {
            Number = source.Number;
            ViewName = source.ViewName;
            Lower = source.Lower;
            Upper = source.Upper;
        }
        /// <summary>初期化</summary>
        /// <param name="includedNumber">加工条件番号に対する初期化の要否</param>
        public void Clear(bool includedNumber = false)
        {
            if (true == includedNumber)
            {
                Number = 0;
            }
            ViewName = "";
            Lower = "";
            Upper = "";
        }
        public int Compare(StructureParamTableItem source)
        {
            int ret = 0;
            while (true)
            {
                ret = Number.CompareTo(source.Number);
                if (0 != ret)
                {
                    break;
                }
                ret = ViewName.CompareTo(source.ViewName);
                if (0 != ret)
                {
                    break;
                }
                ret = Lower.CompareTo(source.Lower);
                if (0 != ret)
                {
                    break;
                }
                ret = Upper.CompareTo(source.Upper);
                if (0 != ret)
                {
                    break;
                }
                break;
            }
            return ret;
        }
    }
}
