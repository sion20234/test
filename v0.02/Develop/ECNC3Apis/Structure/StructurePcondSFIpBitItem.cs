using System;
using System.Xml.Linq;
using ECNC3.Enumeration;
using ECNC3.Models.Common;

namespace ECNC3.Models
{
	/// <summary>汎用データ項目</summary>
	public class StructurePcondCapBitItem : IDisposable, ICloneable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>コンストラクタ</summary>
		public StructurePcondCapBitItem()
		{
		}
		/// <summary>bit0</summary>
		public int CAP10 { get; set; }
        /// <summary>bit1</summary>
        public int CAP20 { get; set; }
        /// <summary>bit2</summary>
		public int CAP30 { get; set; }
        /// <summary>bit3</summary>
		public int CAP40 { get; set; }
        /// <summary>bit4</summary>
		public int CAP50 { get; set; }
        /// <summary>bit5</summary>
		public int CAP60 { get; set; }
        /// <summary>bit6</summary>
		public int CAP70 { get; set; }
        /// <summary>bit7</summary>
		public int CAP80 { get; set; }
        /// <summary>bit8</summary>
		public int CAP90 { get; set; }

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
				;   //  基底クラスのDispose()を確実に呼び出す。
					//base.Dispose( disposing );
			}
		}
		/// <summary>クローン</summary>
		/// <returns>クローンされたインスタンス</returns>
		public object Clone()
		{
            StructurePcondCapBitItem temp = new StructurePcondCapBitItem();
			temp.CAP10 = CAP10;
            temp.CAP20 = CAP20;
            temp.CAP30 = CAP30;
            temp.CAP40 = CAP40;
            temp.CAP50 = CAP50;
            temp.CAP60 = CAP60;
            temp.CAP70 = CAP70;
            temp.CAP80 = CAP80;
            temp.CAP90 = CAP90;
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
            CAP10 = xml.GetAttrValue(root, "cap10");
            CAP20 = xml.GetAttrValue(root, "cap20");
            CAP30 = xml.GetAttrValue(root, "cap30");
            CAP40 = xml.GetAttrValue(root, "cap40");
            CAP50 = xml.GetAttrValue(root, "cap50");
            CAP60 = xml.GetAttrValue(root, "cap60");
            CAP70 = xml.GetAttrValue(root, "cap70");
            CAP80 = xml.GetAttrValue(root, "cap80");
            CAP90 = xml.GetAttrValue(root, "cap90");
            return ResultCodes.Success;
		}
#if false
		/// <summary>要素の作成</summary>
		/// <remarks>
		/// クラス構造に沿ったXML要素を作成します。
		/// </remarks>
		/// <returns>生成された要素</returns>
		public XElement MakeElement()
		{
			return new XElement( "Item",
				new XAttribute( "num", Number ), new XAttribute( "val", Value ), new XAttribute( "io", Io ) );
		}
#endif
	}
}
