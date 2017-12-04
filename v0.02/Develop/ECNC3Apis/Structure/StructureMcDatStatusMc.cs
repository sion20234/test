using System;

namespace ECNC3.Models.McIf
{
	/// <summary>全体情報構造体</summary>
	public class StructureMcDatStatusMc : IDisposable, ICloneable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;

		/// <summary>コンストラクタ</summary>
		public StructureMcDatStatusMc()
		{
		}

		/// <summary>全体ステータス</summary>
		public int Status { get; set; }
		/// <summary>全体アラーム</summary>
		public int Alarm { get; set; }

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
		/// <summary>コピー</summary>
		/// <param name="source">コピー元</param>
		public void Copy( StructureMcDatStatusMc source )
		{
			Status = source.Status;
			Alarm = source.Alarm;
		}
		/// <summary>クローン</summary>
		/// <returns>クローンされたインスタンス</returns>
		public object Clone()
		{
			StructureMcDatStatusMc temp = new StructureMcDatStatusMc();
			temp.Copy( this );
			return temp;
		}
		public int Compare( StructureMcDatStatusMc source )
		{
			int ret = 0;
			while( true ) {
				ret = Status.CompareTo( source.Status );
				if( 0 != ret ) {
					break;
				}
				ret = Alarm.CompareTo( source.Alarm );
				if( 0 != ret ) {
					break;
				}
				break;
			}
			return ret;
		}
	}
}
