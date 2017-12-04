using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECNC3.Enumeration;
using ECNC3.Models.Common;

namespace ECNC3.Models
{
	/// <summary>Ｉ／０情報リスト</summary>
	public class StructureIODataList : List<StructureIODataItem>, IDisposable, ICloneable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>コンストラクタ</summary>
		public StructureIODataList()
		{
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
		private void Dispose( bool disposing )
		{
			try {
				if( false == _disposed ) {
					if( true == disposing ) {
						//  マネージリソースの解放
						if( 0 < this.Count ) {
							foreach( StructureIODataItem item in this ) {
								item.Dispose();
							}
							this.Clear();
						}
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
			StructureIODataList temp = new StructureIODataList();
			foreach( StructureIODataItem item in this ) {
				temp.Add( item.Clone() as StructureIODataItem );
			}
			return temp;
		}
		/// <summary>検索</summary>
		/// <param name="ioTarget">I／O種別</param>
		/// <param name="address">アドレス</param>
		/// <param name="mask">マスク</param>
		/// <returns>該当のインスタンスの参照</returns>
		public StructureIODataItem Find( IOAccessTargets ioTarget, ushort address, ushort mask )
		{
			try {
				int index = FindIndex( ( x ) => ( ( x.AccessTarget == ioTarget ) && ( x.Address == address ) && ( x.Mask == mask ) ) );
				if( 0 <= index ) {
					return this[index];
				}
			} catch( ArgumentNullException e ) {
				AidLog logs = new AidLog( "StructureIODataList.Find" );
				logs.Exception( e, $"{ioTarget}{address},{mask:x}" );
			}
			return null;
		}
		/// <summary>検索</summary>
		/// <param name="target">検索対象</param>
		/// <returns>該当のインスタンスの参照</returns>
		public StructureIODataItem Find( StructureIODataItem target )
		{
			return Find( target.AccessTarget, target.Address, target.Mask );
		}
		/// <summary>内容の更新(ワード単位)</summary>
		/// <param name="ioTarget">I／O種別</param>
		/// <param name="address">アドレス</param>
		/// <param name="val">設定値</param>
		/// <param name="forced">強制設定</param>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// 1WORD単位で値を更新します。
		/// </remarks>
		public ResultCodes UpdateByWord( IOAccessTargets ioTarget, ushort address, ushort val, ushort forced = 0 )
		{
			UpdateByBit( ioTarget, address, 0x0001, val, forced );
			UpdateByBit( ioTarget, address, 0x0002, val, forced );
			UpdateByBit( ioTarget, address, 0x0004, val, forced );
			UpdateByBit( ioTarget, address, 0x0008, val, forced );
			UpdateByBit( ioTarget, address, 0x0010, val, forced );
			UpdateByBit( ioTarget, address, 0x0020, val, forced );
			UpdateByBit( ioTarget, address, 0x0040, val, forced );
			UpdateByBit( ioTarget, address, 0x0080, val, forced );
			UpdateByBit( ioTarget, address, 0x0100, val, forced );
			UpdateByBit( ioTarget, address, 0x0200, val, forced );
			UpdateByBit( ioTarget, address, 0x0400, val, forced );
			UpdateByBit( ioTarget, address, 0x0800, val, forced );
			UpdateByBit( ioTarget, address, 0x1000, val, forced );
			UpdateByBit( ioTarget, address, 0x2000, val, forced );
			UpdateByBit( ioTarget, address, 0x4000, val, forced );
			UpdateByBit( ioTarget, address, 0x8000, val, forced );
			return ResultCodes.Success;
		}
		/// <summary>内容の更新(ビット単位)</summary>
		/// <param name="ioTarget">I／O種別</param>
		/// <param name="address">アドレス</param>
		/// <param name="mask">マスク値</param>
		/// <param name="val">設定値</param>
		/// <param name="forced">強制設定</param>
		/// <returns>実行結果</returns>
		private ResultCodes UpdateByBit( IOAccessTargets ioTarget, ushort address, ushort mask, ushort val, ushort forced = 0 )
		{
			StructureIODataItem item = Find( ioTarget, address, mask );
			if( null != item ) {
				item.Value = (ushort)( val & mask );
				item.Forced = ( mask == ( forced & mask ) ) ? true : false;
#if false
				if( true == item.Forced ) {
					AidLog logs = new AidLog( "UpdateByBit" );
					logs.Debug( $"Force {address},0x{mask:x4},0x{val:x4},0x{forced:x4}" );
				}
#endif
				return ResultCodes.Success;
			}
			return ResultCodes.NotFound;
		}
		/// <summary>ビット設定(WORD単位)</summary>
		/// <param name="ioTarget">入力／出力</param>
		/// <param name="address">アドレス</param>
		/// <param name="val">設定値</param>
		/// <remarks>
		/// 引数 val の値を 引数 address に対して16ビット分の値を設定します。
		/// </remarks>
		public void AddByWord( IOAccessTargets ioTarget, ushort address, ushort val )
		{
			Add( new StructureIODataItem( ioTarget, address, 0x0001, val ) );
			Add( new StructureIODataItem( ioTarget, address, 0x0002, val ) );
			Add( new StructureIODataItem( ioTarget, address, 0x0004, val ) );
			Add( new StructureIODataItem( ioTarget, address, 0x0008, val ) );
			Add( new StructureIODataItem( ioTarget, address, 0x0010, val ) );
			Add( new StructureIODataItem( ioTarget, address, 0x0020, val ) );
			Add( new StructureIODataItem( ioTarget, address, 0x0040, val ) );
			Add( new StructureIODataItem( ioTarget, address, 0x0080, val ) );
			Add( new StructureIODataItem( ioTarget, address, 0x0100, val ) );
			Add( new StructureIODataItem( ioTarget, address, 0x0200, val ) );
			Add( new StructureIODataItem( ioTarget, address, 0x0400, val ) );
			Add( new StructureIODataItem( ioTarget, address, 0x0800, val ) );
			Add( new StructureIODataItem( ioTarget, address, 0x1000, val ) );
			Add( new StructureIODataItem( ioTarget, address, 0x2000, val ) );
			Add( new StructureIODataItem( ioTarget, address, 0x4000, val ) );
			Add( new StructureIODataItem( ioTarget, address, 0x8000, val ) );
		}
	}
}
