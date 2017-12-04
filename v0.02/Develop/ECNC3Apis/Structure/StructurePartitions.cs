using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECNC3.Models.McIf
{
	/// <summary>パーティション設定(全体)</summary>
	public class StructurePartitions : IDisposable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>コンストラクタ</summary>
		public StructurePartitions()
		{
			if( null == Items ) {
				Items = new StructurePartitionList();
			}
		}

		/// <summary>パーティション無効／有効</summary>
		/// <value>
		///		<list type="bullet" >
		///			<item>true=有効</item>
		///			<item>false=無効</item>
		///		</list>
		/// </value>
		public bool Enabled { get; set; }
		/// <summary>第1～6パーティション設定</summary>
		public StructurePartitionList Items { get; private set; }

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
		protected virtual void Dispose( bool disposing )
		{
			try {
				if( false == _disposed ) {
					if( true == disposing ) {
						//  マネージリソースの解放
						if( null != Items ) {
							if( 0 < Items.Count ) {
								foreach( StructurePartitionItem item in Items ) {
									item.Dispose();
								}
								Items.Clear();
								Items = null;
							}
						}
					}
					//  アンマネージリソースの解放
				}
				_disposed = true;
			} finally {
				//  基底クラスのDispose()を確実に呼び出す。
				;   //base.Dispose( disposing );
			}
		}
	}
}
