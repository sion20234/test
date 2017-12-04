using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECNC3.Models
{
	/// <summary>材料名称構造リスト</summary>
	public class StructureMaterialNameList : List<StructureMaterialNameItem>, IDisposable, ICloneable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>コンストラクタ</summary>
		public StructureMaterialNameList()
		{
		}
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
		private void Dispose( bool disposing )
		{
			try {
				if( false == _disposed ) {
					if( true == disposing ) {
						//  マネージリソースの解放
						if( 0 < this.Count ) {
							foreach( StructureMaterialNameItem item in this ) {
								item.Dispose();
							}
							this.Clear();
						}
					}
					//  アンマネージリソースの解放
				}
				_disposed = true;
			} finally {
				;   //  基底クラスのDispose()を確実に呼び出す。
					//base.Dispose( disposing );
			}
		}
		/// <summary>クローン</summary>
		/// <returns>クローンされたインスタンス</returns>
		public object Clone()
		{
			StructureMaterialNameList temp = new StructureMaterialNameList();
			foreach( StructureMaterialNameItem item in this ) {
				temp.Add( item.Clone() as StructureMaterialNameItem );
			}
			return temp;
		}
		/// <summary>材料番号による検索</summary>
		/// <param name="number">材料番号</param>
		/// <returns>要素番号</returns>
		public int FindByNumber( int number )
		{
			return FindIndex( ( x ) => x.Number == number );
		}
		/// <summary>材料名称による検索</summary>
		/// <param name="neme">材料名称</param>
		/// <returns>要素番号</returns>
		public int FindByName( string neme )
		{
			return FindIndex( ( x ) => 0 == string.Compare( x.Name, neme, false ) );
		}
	}
}
