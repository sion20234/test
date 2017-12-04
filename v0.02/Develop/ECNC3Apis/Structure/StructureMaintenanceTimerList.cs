﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECNC3.Models
{
	/// <summary>汎用データ項目リスト</summary>
	public class StructureMaintenanceTimerList : List<StructureMaintenanceTimerItem>, IDisposable, ICloneable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>コンストラクタ</summary>
		public StructureMaintenanceTimerList()
		{
		}
        /// <summary>
        /// メイン電源タイマー
        /// </summary>
        public UInt64 MainPowerCount { get; set; }
        /// <summary>
        /// 加工電源タイマー
        /// </summary>
        public UInt64 ProcessPowerCount { get; set; }
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
							foreach(StructureMaintenanceTimerItem item in this ) {
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
            StructureMaintenanceTimerList temp = new StructureMaintenanceTimerList();
			foreach(StructureMaintenanceTimerItem item in this ) {
				temp.Add( item.Clone() as StructureMaintenanceTimerItem);
			}
            temp.MainPowerCount = MainPowerCount;
            temp.ProcessPowerCount = ProcessPowerCount;
			return temp;
		}
	}
}
