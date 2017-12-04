using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECNC3.Models
{
	/// <summary>ユーザ情報リスト</summary>
	public class StructureUserSettingList : List<StructureUserSettingItem>, IDisposable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;

		/// <summary>コンストラクタ</summary>
		public StructureUserSettingList()
		{
		}
		/// <summary>ユーザー名</summary>
		public string UserName { get; internal set; }
		/// <summary>パスワード</summary>
		public string Password { get; internal set; }

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
						foreach( StructureUserSettingItem item in this ) {
							item.Dispose();
						}
						Clear();
					}
					//  アンマネージリソースの解放
				}
				_disposed = true;
			} finally {
				;   //  基底クラスのDispose()を確実に呼び出す。
					//base.Dispose( disposing );
			}
		}
		/// <summary>ユーザ名による検索</summary>
		/// <param name="name">検索キー(ユーザ名)</param>
		/// <returns>Items プロパティの参照</returns>
		public StructureUserSettingItem FindName( string name )
		{
			int index = FindIndex( ( x ) => x.UserName == name );
			if( 0 > index ) {
				return null;
			}
			return this[index];
		}
	}
}
