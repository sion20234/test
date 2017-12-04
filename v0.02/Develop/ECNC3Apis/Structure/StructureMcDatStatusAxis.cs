using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECNC3.Models.McIf
{
	/// <summary>各軸情報構造体</summary>
	public class StructureMcDatStatusAxis : IDisposable, ICloneable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;

		/// <summary>コンストラクタ</summary>
		public StructureMcDatStatusAxis()
		{
		}
		/// <summary>軸ステータス</summary>
		public int AxStatus { get; set; }
		/// <summary>軸アラーム</summary>
		public int AxAlarm { get; set; }
		/// <summary>指令位置</summary>
		public int ComReg { get; set; }
		/// <summary>機械位置</summary>
		public int PosReg { get; set; }
		/// <summary>偏差量</summary>
		public int ErrReg { get; set; }
		/// <summary>最新ブロック払い出し量</summary>
		public int BlockSeg { get; set; }
		/// <summary>絶対位置</summary>
		public int AbsReg { get; set; }
		/// <summary>トルク</summary>
		public int Trq { get; set; }
		/// <summary>絶対位置(指令:ﾏｼﾝﾛｯｸ込み)</summary>
		public int AMrReg { get; set; }

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
		public void Copy( StructureMcDatStatusAxis source )
		{
			AxStatus = source.AxStatus;
			AxAlarm = source.AxAlarm;
			ComReg = source.ComReg;
			PosReg = source.PosReg;
			ErrReg = source.ErrReg;
			BlockSeg = source.BlockSeg;
			AbsReg = source.AbsReg;
			Trq = source.Trq;
			AMrReg = source.AMrReg;
		}
		/// <summary>クローン</summary>
		/// <returns>クローンされたインスタンス</returns>
		public object Clone()
		{
			StructureMcDatStatusAxis temp = new StructureMcDatStatusAxis();
			temp.Copy( this );
			return temp;
		}
		public int Compare( StructureMcDatStatusAxis source )
		{
			int ret = 0;
			while( true ) {
				ret = AxStatus.CompareTo( source.AxStatus );
				if( 0 != ret ) {
					break;
				}
				ret = AxAlarm.CompareTo( source.AxAlarm );
				if( 0 != ret ) {
					break;
				}
				ret = ComReg.CompareTo( source.ComReg );
				if( 0 != ret ) {
					break;
				}
				ret = PosReg.CompareTo( source.PosReg );
				if( 0 != ret ) {
					break;
				}
				ret = ErrReg.CompareTo( source.ErrReg );
				if( 0 != ret ) {
					break;
				}
				ret = BlockSeg.CompareTo( source.BlockSeg );
				if( 0 != ret ) {
					break;
				}
				ret = AbsReg.CompareTo( source.AbsReg );
				if( 0 != ret ) {
					break;
				}
				ret = Trq.CompareTo( source.Trq );
				if( 0 != ret ) {
					break;
				}
				ret = AMrReg.CompareTo( source.AMrReg );
				if( 0 != ret ) {
					break;
				}
				break;
			}
			return ret;
		}
	}
}
