using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECNC3.Models.McIf
{
	/// <summary>ECNCステータス情報構造体</summary>
	/// <remarks>
	/// 可読性の都合、MCボードインターフェース定義と型、名称を同じにしています。
	/// </remarks>
	public class StructureMcDatStatusEcnc : IDisposable, ICloneable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>アナログ入力値 定義数</summary>
		private readonly int ADCount = 5;
		/// <summary>コンストラクタ</summary>
		public StructureMcDatStatusEcnc()
		{
			ADVal = new short[ADCount];
		}
		/// <summary>各種状態情報２</summary>
		public short Status2 { get; set; }
		/// <summary>各種状態情報３</summary>
		public short Status3 { get; set; }
		/// <summary>アラーム情報２</summary>
		public short Alarm2 { get; set; }
		/// <summary>アラーム情報３</summary>
		public short Alarm3 { get; set; }
		/// <summary>アラーム情報４</summary>
		public short Alarm4 { get; set; }
		/// <summary>アラーム情報５</summary>
		public short Alarm5 { get; set; }
		/// <summary>Ｗ軸上限値</summary>
		public int WTopPos { get; set; }
		/// <summary>補正角度(8-24ﾋﾞｯﾄ固定小数点)</summary>
		public int CorrAng { get; set; }
        /// <summary>放電加工目標加工量</summary>
        public int PrcsTargetDist { get; set; }
        /// <summary>放電加工現在加工量</summary>
        public int PrcsNowDist { get; set; }
		/// <summary>EtherCAT IFボード エラー1</summary>
		public int EIFErr1 { get; set; }
		/// <summary>EtherCAT IFボード エラー2</summary>
		public int EIFErr2 { get; set; }
		/// <summary>アナログ入力値</summary>
		public short[] ADVal { get; set; }

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
						if( null != ADVal ) {
							ADVal = null;
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
		/// <summary>コピー</summary>
		/// <param name="source">コピー元</param>
		public void Copy( StructureMcDatStatusEcnc source )
		{
			Status2 = source.Status2;
			Status3 = source.Status3;
			Alarm2 = source.Alarm2;
			Alarm3 = source.Alarm3;
			Alarm4 = source.Alarm4;
			Alarm5 = source.Alarm5;
			WTopPos = source.WTopPos;
            PrcsTargetDist = source.PrcsTargetDist;
            PrcsNowDist = source.PrcsNowDist;
            CorrAng = source.CorrAng;
			EIFErr1 = source.EIFErr1;
			EIFErr2 = source.EIFErr2;
			int index = 0;
			if( ( null != source.ADVal ) && ( null != ADVal ) ) {
				for( index = 0 ; ( index < source.ADVal.Length ) && ( index < ADVal.Length ) ; ++index ) {
					ADVal[index] = source.ADVal[index];
				}
			}
		}
		/// <summary>クローン</summary>
		/// <returns>クローンされたインスタンス</returns>
		public object Clone()
		{
			StructureMcDatStatusEcnc temp = new StructureMcDatStatusEcnc();
			temp.Copy( this );
			return temp;
		}
		public int Compare( StructureMcDatStatusEcnc source )
		{
			int ret = 0;
			while( true ) {
				ret = Status2.CompareTo( source.Status2 );
				if( 0 != ret ) {
					break;
				}
				ret = Status3.CompareTo( source.Status3 );
				if( 0 != ret ) {
					break;
				}
				ret = Alarm2.CompareTo( source.Alarm2 );
				if( 0 != ret ) {
					break;
				}
				ret = Alarm3.CompareTo( source.Alarm3 );
				if( 0 != ret ) {
					break;
				}
				ret = Alarm4.CompareTo( source.Alarm4 );
				if( 0 != ret ) {
					break;
				}
				ret = Alarm5.CompareTo( source.Alarm5 );
				if( 0 != ret ) {
					break;
				}
				ret = WTopPos.CompareTo( source.WTopPos );
				if( 0 != ret ) {
					break;
                }
                ret = PrcsTargetDist.CompareTo(source.PrcsTargetDist);
                if (0 != ret)
                {
                    break;
                }
                ret = PrcsNowDist.CompareTo(source.PrcsNowDist);
                if (0 != ret)
                {
                    break;
                }
                ret = CorrAng.CompareTo( source.CorrAng );
				if( 0 != ret ) {
					break;
				}
				ret = EIFErr1.CompareTo( source.EIFErr1 );
				if( 0 != ret ) {
					break;
				}
				ret = EIFErr2.CompareTo( source.EIFErr2 );
				if( 0 != ret ) {
					break;
				}
				int index = 0;
				if( ( null != source.ADVal ) && ( null != ADVal ) ) {
					for( index = 0 ; ( index < source.ADVal.Length ) && ( index < ADVal.Length ) ; ++index ) {
						ret = ADVal[index].CompareTo( source.ADVal[index] );
						if( 0 != ret ) {
							break;
						}
					}
				}
				if( 0 != ret ) {
					break;
				}
				break;
			}
			return ret;
		}
	}
}
