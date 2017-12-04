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
        public int BoundIndex { get; set; }
		/// <summary>bit9</summary>
		public int C10 { get; set; }
        /// <summary>bit10</summary>
        public int C20 { get; set; }
        /// <summary>bit11</summary>
		public int C30 { get; set; }
        /// <summary>bit12</summary>
		public int C40 { get; set; }
        /// <summary>bit13</summary>
		public int C50 { get; set; }
        /// <summary>bit14</summary>
		public int C60 { get; set; }
        /// <summary>bit15</summary>
		public int C70 { get; set; }
        /// <summary>bit16</summary>
		public int C80 { get; set; }
        /// <summary>bit17</summary>
		public int C90 { get; set; }
        /// <summary>bit18</summary>
		public int CA0 { get; set; }

        /// <summary>値の初期化</summary>
        public void Clear()
        {
            BoundIndex = 12;
            C10 = 0;
            C20 = 0;
            C30 = 0;
            C40 = 0;
            C50 = 0;
            C60 = 0;
            C70 = 0;
            C80 = 0;
            C90 = 0;
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
            StructurePcondCapBitItem temp = new StructurePcondCapBitItem();
            temp.BoundIndex = BoundIndex;
			temp.C10 = C10;
            temp.C20 = C20;
            temp.C30 = C30;
            temp.C40 = C40;
            temp.C50 = C50;
            temp.C60 = C60;
            temp.C70 = C70;
            temp.C80 = C80;
            temp.C90 = C90;
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
            BoundIndex = xml.GetAttrValue(root, "bound");
            C10 = xml.GetAttrValue(root, "c10");
            C20 = xml.GetAttrValue(root, "c20");
            C30 = xml.GetAttrValue(root, "c30");
            C40 = xml.GetAttrValue(root, "c40");
            C50 = xml.GetAttrValue(root, "c50");
            C60 = xml.GetAttrValue(root, "c60");
            C70 = xml.GetAttrValue(root, "c70");
            C80 = xml.GetAttrValue(root, "c80");
            C90 = xml.GetAttrValue(root, "ca0");
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
