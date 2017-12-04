using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECNC3.Models
{
	/// <summary>アクセス対象</summary>
	public enum IOAccessTargets
	{
		/// <summary>未設定</summary>
		NotSet,
		/// <summary>SPX汎用入出力 INPUT</summary>
		Input,
		/// <summary>SPX汎用入出力 OUTPUT</summary>
		Output,
	}

	/// <summary>Ｉ／０項目ごとのリスト</summary>
	/// <remarks>
	/// Ｉ／０ 情報の1ビット毎の定義です。
	/// </remarks>
	public class StructureIODataItem : IDisposable, ICloneable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>コンストラクタ</summary>
		public StructureIODataItem()
		{
		}
		/// <summary>コンストラクタ</summary>
		/// <param name="ioTraget">Ｉ／O</param>
		/// <param name="address">アドレス</param>
		/// <param name="mask">マスク</param>
		/// <param name="val">値</param>
		internal StructureIODataItem( IOAccessTargets ioTraget, ushort address, ushort mask, ushort val )
		{
			AccessTarget = ioTraget;
			Address = address;
			Channel = (ushort)( address + 1600 );
			Mask = mask;
			Value = (ushort)( val & mask );
		}

		/// <summary>アクセス対象</summary>
		public IOAccessTargets AccessTarget { get; internal set; }
		/// <summary>アドレス</summary>
		public ushort Address { get; internal set; }
		/// <summary>ビットマスク</summary>
		public ushort Mask { get; internal set; }
		/// <summary>項目名称</summary>
		public string Name { get; set; }

		/// <summary>設定値</summary>
		public ushort Value { get; internal set; }
		/// <summary>信号状態</summary>
		public bool Signal { get { return ( Mask == ( Mask & Value ) ) ? true : false; } }
		
		/// <summary>チャネル</summary>
		public ushort Channel { get; internal set; }
		/// <summary>強制設定</summary>
		public bool Forced { get; internal set; } = false;
		/// <summary>補足</summary>
		public string Note { get; set; }
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
			StructureIODataItem temp = new StructureIODataItem();
			temp.AccessTarget = AccessTarget;
			temp.Address = Address;
			temp.Mask = Mask;
			temp.Name = Name;
			temp.Value = Value;
			temp.Channel = Channel;
			temp.Forced = Forced;
			temp.Note = Note;
			return temp;
		}
	}
}
