using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECNC3.Models
{
	/// <summary>加工条件ファイル構造</summary>
	public class StructureUpperAndLowerLimitItem : ICloneable, IDisposable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;

        /// <summary>P番号</summary>
        public decimal UpperLimit { get; set; }
        /// <summary>P番号</summary>
        public decimal LowerLimit { get; set; }

		/// <summary>インスタンスの破棄</summary>
		public void Dispose()
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
		protected void Dispose( bool disposing )
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
				;	// base.Dispose( disposing );
			}
		}
		/// <summary>クローン</summary>
		/// <returns>クローンされたインスタンス</returns>
		public object Clone()
		{
            StructureUpperAndLowerLimitItem temp = new StructureUpperAndLowerLimitItem();
			temp.UpperLimit = UpperLimit;
            temp.LowerLimit = LowerLimit;
			return temp;
		}
		/// <summary>コピー</summary>
		/// <param name="source">コピー元</param>
		public void Copy(StructureUpperAndLowerLimitItem source )
		{
            UpperLimit = source.UpperLimit;
            LowerLimit = source.LowerLimit;
		}
		/// <summary>初期化</summary>
		/// <param name="includedNumber">加工条件番号に対する初期化の要否</param>
		public void Clear( bool includedNumber = false )
		{
            UpperLimit = 0;
            LowerLimit = 0;
		}
		public int Compare(StructureUpperAndLowerLimitItem source )
		{
			int ret = 0;
			while( true ) {
				ret = UpperLimit.CompareTo( source.UpperLimit );
				if( 0 != ret ) {
					break;
				}
				ret = LowerLimit.CompareTo( source.LowerLimit );
				if( 0 != ret ) {
					break;
				}
                break;
			}
			return ret;
		}
	}
}
