using System;
using System.Xml.Linq;
using ECNC3.Enumeration;
using ECNC3.Models.Common;

namespace ECNC3.Models
{
	/// <summary>材料名称構造</summary>
	public class StructureMaterialNameItem : IDisposable, ICloneable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>コンストラクタ</summary>
		public StructureMaterialNameItem()
		{
		}
		/// <summary>番号</summary>
		public int Number { get; set; }
		/// <summary>材料名称</summary>
		public string Name { get; set; }

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
			StructureMaterialNameItem temp = new StructureMaterialNameItem();
			temp.Number = Number;
			temp.Name = Name;
			return temp;
		}

		///	<summary>XML要素読み込み</summary>
		/// <param name="root">読み込み対象となるXML要素</param>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// 引数のXML要素の解析を行います。
		/// </remarks>
		public ResultCodes Read( XElement root )
		{
			AidXmlLinq xml = new AidXmlLinq();
			Number = xml.GetAttrValue( root, "num" );
			Name = xml.GetAttrText( root, "nam" );
			return ResultCodes.Success;
		}
		/// <summary>要素の作成</summary>
		/// <remarks>
		/// クラス構造に沿ったXML要素を作成します。
		/// </remarks>
		/// <returns>生成された要素</returns>
		public XElement MakeElement()
		{
			return new XElement( "Item",
				new XAttribute( "num", Number ), new XAttribute( "nam", Name ) );
		}
	}
}
