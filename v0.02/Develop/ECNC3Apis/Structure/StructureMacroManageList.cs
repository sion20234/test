﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECNC3.Models
{
	/// <summary>加工条件構造リスト</summary>
	public class StructureMacroManageList : List<StructureMacroManageItem>, IDisposable, ICloneable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
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
						if( 0 < Count ) {
							foreach(StructureMacroManageItem item in this ) {
								item.Dispose();
							}
							this.Clear();
						}
					}
					//  アンマネージリソースの解放
				}
				_disposed = true;
			} finally {
				;	//  基底クラスのDispose()を確実に呼び出す。
					//base.Dispose( disposing );
			}
		}
		/// <summary>クローン</summary>
		/// <returns>クローンされたインスタンス</returns>
		public object Clone()
		{
            StructureMacroManageList temp = new StructureMacroManageList();
			foreach(StructureMacroManageItem item in this ) {
				temp.Add( item.Clone() as StructureMacroManageItem);
			}
			return temp;
		}
#if false
		/// <summary>取得</summary>
		/// <param name="number">加工条件番号</param>
		/// <returns>指定の加工条件の参照</returns>
		public StructureProcessConditionItem Refer( int number )
		{
			int ret = FindIndex( ( x ) => x.Number == number );
			if( 0 > ret ) {
				return null;
			}
			return this[ret];
		}
		/// <summary>取得</summary>
		/// <param name="number">加工条件番号</param>
		/// <returns>指定の加工条件のクローン</returns>
		public StructureProcessConditionItem Find( int number )
		{
			StructureProcessConditionItem data = Refer( number );
			if( null != data ) {
				return data.Clone() as StructureProcessConditionItem;
			}
			return null;
		}
#endif
	}
}
