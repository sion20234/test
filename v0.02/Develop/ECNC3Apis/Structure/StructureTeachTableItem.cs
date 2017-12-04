using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECNC3.Models
{
	/// <summary>加工条件ファイル構造</summary>
	public class StructureTeachTableItem : ICloneable, IDisposable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;

		/// <summary>INDEX番号</summary>
		public int Number { get; set; }
      
		/// <summary>変数名</summary>
		public string Name { get; set; }
		/// <summary>条件</summary>
		public string Value { get; set; }
		/// <summary>使用状態</summary>
		public bool Selected { get; set; }
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
			StructureTeachTableItem temp = new StructureTeachTableItem();
			temp.Number = Number;
			temp.Name = Name;
			temp.Value = Value;
			temp.Selected = Selected;
			return temp;
		}
		/// <summary>コピー</summary>
		/// <param name="source">コピー元</param>
		public void Copy( StructureTeachTableItem source )
		{
			Number = source.Number;
			Name = source.Name;
			Value = source.Value;
			Selected = source.Selected;
		}
		/// <summary>初期化</summary>
		/// <param name="includedNumber">加工条件番号に対する初期化の要否</param>
		public void Clear( bool includedNumber = false )
		{
			if( true == includedNumber ) {
				Number = 0;
			}
			Name = "";
			Value = "";
			Selected = false;
		}
		public int Compare( StructureTeachTableItem source )
		{
			int ret = 0;
			while( true ) {
				ret = Number.CompareTo( source.Number );
				if( 0 != ret ) {
					break;
				}
				ret = Name.CompareTo( source.Name );
				if( 0 != ret ) {
					break;
				}
				ret = Value.CompareTo( source.Value );
				if( 0 != ret ) {
					break;
				}
				ret = Selected.CompareTo( source.Selected );
				if( 0 != ret ) {
					break;
				}
                break;
			}
			return ret;
		}
	}
}
