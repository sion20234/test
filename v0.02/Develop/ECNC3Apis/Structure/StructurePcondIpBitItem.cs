using System;
using System.Xml.Linq;
using ECNC3.Enumeration;
using ECNC3.Models.Common;

namespace ECNC3.Models
{
	/// <summary>汎用データ項目</summary>
	public class StructurePcondIpBitItem : IDisposable, ICloneable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>コンストラクタ</summary>
		public StructurePcondIpBitItem()
		{
		}
		/// <summary>bit0</summary>
		public int IP10 { get; set; }
        /// <summary>bit1</summary>
        public int IP20 { get; set; }
        /// <summary>bit2</summary>
		public int IP30 { get; set; }
        /// <summary>bit3</summary>
		public int IP40 { get; set; }
        /// <summary>bit4</summary>
		public int IP50 { get; set; }
        /// <summary>bit5</summary>
		public int IP60 { get; set; }
        /// <summary>bit6</summary>
		public int IP70 { get; set; }
        /// <summary>bit7</summary>
		public int IP80 { get; set; }

        /// <summary>値の初期化</summary>
        public void Clear()
        {
            IP10 = 0;
            IP20 = 0;
            IP30 = 0;
            IP40 = 0;
            IP50 = 0;
            IP60 = 0;
            IP70 = 0;
            IP80 = 0;
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
            StructurePcondIpBitItem temp = new StructurePcondIpBitItem();
			temp.IP10 = IP10;
            temp.IP20 = IP20;
            temp.IP30 = IP30;
            temp.IP40 = IP40;
            temp.IP50 = IP50;
            temp.IP60 = IP60;
            temp.IP70 = IP70;
            temp.IP80 = IP80;
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
            IP10 = xml.GetAttrValue(root, "ip10");
            IP20 = xml.GetAttrValue(root, "ip20");
            IP30 = xml.GetAttrValue(root, "ip30");
            IP40 = xml.GetAttrValue(root, "ip40");
            IP50 = xml.GetAttrValue(root, "ip50");
            IP60 = xml.GetAttrValue(root, "ip60");
            IP70 = xml.GetAttrValue(root, "ip70");
            IP80 = xml.GetAttrValue(root, "ip80");
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
