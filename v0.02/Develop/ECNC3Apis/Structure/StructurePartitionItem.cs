using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECNC3.Models.McIf
{
	/// <summary>パーティション設定(個別)</summary>
	public class StructurePartitionItem : IDisposable, ICloneable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>コンストラクタ</summary>
		public StructurePartitionItem()
		{
			Number = -1;
		}
		/// <summary>パーティション番号</summary>
		public int Number { get; set; }
		/// <summary>パーティション開始位置</summary>
		public short IndexStart { get; set; }
		/// <summary>パーティション終了位置</summary>
		public short IndexEnd { get; set; }
		/// <summary>細線設定</summary>
		/// <value>
		///		<list type="bullet" >
		///			<item>true=細線</item>
		///			<item>false=通常</item>
		///		</list>
		/// </value>
		public bool Thinline { get; set; }

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
					}
					//  アンマネージリソースの解放
				}
				_disposed = true;
			} finally {
				//  基底クラスのDispose()を確実に呼び出す。
				;	//base.Dispose( disposing );
			}
		}
		/// <summary>クローン</summary>
		/// <returns>クローンされたインスタンス</returns>
		public object Clone()
		{
			StructurePartitionItem temp = new StructurePartitionItem();
			temp.Number = Number;
			temp.IndexStart = IndexStart;
			temp.IndexEnd = IndexEnd;
			temp.Thinline = Thinline;
			return temp;
		}
	}
}
