using System;
using System.Xml.Linq;
using ECNC3.Enumeration;
using ECNC3.Models.Common;

namespace ECNC3.Models
{
	/// <summary>汎用データ項目</summary>
	public class StructurePcondSFIpBitItem : IDisposable, ICloneable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>コンストラクタ</summary>
		public StructurePcondSFIpBitItem()
		{
		}

        /// <summary>オフセット</summary>
        public int OFFSET { get; set; }

        /// <summary>bit0</summary>
        public int SFIP10 { get; set; }
        /// <summary>bit1</summary>
        public int SFIP20 { get; set; }
        /// <summary>bit2</summary>
		public int SFIP30 { get; set; }
        /// <summary>bit3</summary>
		public int SFIP40 { get; set; }
        /// <summary>bit4</summary>
		public int SFIP50 { get; set; }
        /// <summary>bit5</summary>
		public int SFIP60 { get; set; }
        /// <summary>bit6</summary>
		public int SFIP70 { get; set; }
        /// <summary>bit7</summary>
		public int SFIP80 { get; set; }
        /// <summary>bit8</summary>
		public int SFIP90 { get; set; }

        /// <summary>値の初期化</summary>
        public void Clear()
        {
            OFFSET = 10;
            SFIP10 = 0;
            SFIP20 = 0;
            SFIP30 = 0;
            SFIP40 = 0;
            SFIP50 = 0;
            SFIP60 = 0;
            SFIP70 = 0;
            SFIP80 = 0;
            SFIP90 = 0;
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
            StructurePcondSFIpBitItem temp = new StructurePcondSFIpBitItem();
            temp.OFFSET = OFFSET;
			temp.SFIP10 = SFIP10;
            temp.SFIP20 = SFIP20;
            temp.SFIP30 = SFIP30;
            temp.SFIP40 = SFIP40;
            temp.SFIP50 = SFIP50;
            temp.SFIP60 = SFIP60;
            temp.SFIP70 = SFIP70;
            temp.SFIP80 = SFIP80;
            temp.SFIP90 = SFIP90;
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
            OFFSET = xml.GetAttrValue(root, "offset");
            SFIP10 = xml.GetAttrValue(root, "sfip10");
            SFIP20 = xml.GetAttrValue(root, "sfip20");
            SFIP30 = xml.GetAttrValue(root, "sfip30");
            SFIP40 = xml.GetAttrValue(root, "sfip40");
            SFIP50 = xml.GetAttrValue(root, "sfip50");
            SFIP60 = xml.GetAttrValue(root, "sfip60");
            SFIP70 = xml.GetAttrValue(root, "sfip70");
            SFIP80 = xml.GetAttrValue(root, "sfip80");
            SFIP90 = xml.GetAttrValue(root, "sfip90");
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
