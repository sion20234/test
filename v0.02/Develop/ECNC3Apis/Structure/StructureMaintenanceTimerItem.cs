using System;
using System.Xml.Linq;
using ECNC3.Enumeration;
using ECNC3.Models.Common;

namespace ECNC3.Models
{
    public enum MaintenanceTimerCategory
    {
        NULL = 0,
        /// <summary>
        /// 運転時間
        /// </summary>
        PowerON = 1,
        /// <summary>
        /// 放電時間
        /// </summary>
        DischargeON = 2,
        /// <summary>
        /// 年月日
        /// </summary>
        DateTime = 3
    }
	/// <summary>汎用データ項目</summary>
	public class StructureMaintenanceTimerItem : IDisposable, ICloneable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>コンストラクタ</summary>
		public StructureMaintenanceTimerItem()
		{
            Number = 0;
            Name = "";
            Value = 0;
            Limit = 0;
            Category = MaintenanceTimerCategory.NULL;
            CountFlag = false;
        }
        /// <summary>コンストラクタ</summary>
		public StructureMaintenanceTimerItem(int index, string name = "", ulong value = 0, ulong limit = 0, MaintenanceTimerCategory category = MaintenanceTimerCategory.NULL)
        {
            Number = index;
            Name = name;
            Value = value;
            Limit = limit;
            Category = category;
            CountFlag = false;
        }
        public int Number { get; set; }
		/// <summary>値</summary>
		public ulong Value { get; set; }
        /// <summary>期限</summary>
		public ulong Limit { get; set; }
        /// <summary>
        /// タイマー名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// タイマーカウントフラグ
        /// </summary>
        public bool CountFlag { get; set; }
        /// <summary>
        /// タイマーのカテゴリ
        /// </summary>
        public MaintenanceTimerCategory Category { get; set; }

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
            StructureMaintenanceTimerItem temp = new StructureMaintenanceTimerItem();
            temp.Number = Number;
			temp.Value = Value;
            temp.Category = Category;
            temp.CountFlag = CountFlag;
            temp.Name = Name;
            temp.Limit = Limit;
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
			Name = xml.GetAttrText( root, "name" );
			Value = (ulong)xml.GetAttrValue( root, "val" );
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
