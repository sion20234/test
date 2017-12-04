using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECNC3.Models
{
	/// <summary>加工条件ファイル構造</summary>
	public class StructureMacroManageItem : ICloneable, IDisposable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;

		/// <summary>マクロ番号</summary>
		public int Number { get; set; }
		/// <summary>コメント</summary>
		public string Comment { get; set; }
		/// <summary>変数値</summary>
		public string Value { get; set; }
        /// <summary>書き込み保護</summary>
        public int Protect { get; set; }

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
            StructureMacroManageItem temp = new StructureMacroManageItem();
			temp.Number = Number;
			temp.Comment = Comment;
			temp.Value = Value;
			temp.Protect = Protect;
			return temp;
		}
		/// <summary>コピー</summary>
		/// <param name="source">コピー元</param>
		public void Copy(StructureMacroManageItem source )
		{
			Number = source.Number;
            Comment = source.Comment;
            Value = source.Value;
            Protect = source.Protect;
		}
		/// <summary>初期化</summary>
		/// <param name="includedNumber">加工条件番号に対する初期化の要否</param>
		public void Clear( bool includedNumber = false )
		{
			if( true == includedNumber ) {
				Number = 0;
			}
			Comment = string.Empty;
            Value = string.Empty;
            Protect = 0;
		}
		public int Compare(StructureMacroManageItem source )
		{
			int ret = 0;
			while( true ) {
				ret = Number.CompareTo( source.Number );
				if( 0 != ret ) {
					break;
				}
				ret = Comment.CompareTo( source.Comment );
				if( 0 != ret ) {
					break;
				}
				ret = Value.CompareTo( source.Value );
                if (0 != ret){
                    break;
                }
                ret = Protect.CompareTo( source.Protect );
                if( 0 != ret){
                    break;
                }
                break;
			}
			return ret;
		}
	}
}
