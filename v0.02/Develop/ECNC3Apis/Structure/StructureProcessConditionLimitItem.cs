using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECNC3.Models
{
	/// <summary>加工条件上下限値構造</summary>
	public class StructureProcessConditionLimitItem : ICloneable, IDisposable
	{
        public StructureProcessConditionLimitItem()
        {
            Ton = new StructureUpperAndLowerLimitItem();
            Toff = new StructureUpperAndLowerLimitItem();
            IPVal = new StructureUpperAndLowerLimitItem();
            SFIPVal = new StructureUpperAndLowerLimitItem();
            CAPVal = new StructureUpperAndLowerLimitItem();
            SCVal = new StructureUpperAndLowerLimitItem();
            CRSVal = new StructureUpperAndLowerLimitItem();
            SfrBkSel = new StructureUpperAndLowerLimitItem();
            SfrFrSel = new StructureUpperAndLowerLimitItem();
            PompVal = new StructureUpperAndLowerLimitItem();
            ServoSel = new StructureUpperAndLowerLimitItem();
            PSSel = new StructureUpperAndLowerLimitItem();
            POLVal = new StructureUpperAndLowerLimitItem();
        }
        /// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
        private bool _disposed = false;
		/// <summary>Ton[us]</summary>
		public StructureUpperAndLowerLimitItem Ton { get; set; }
		/// <summary>Toff[us]</summary>
		public StructureUpperAndLowerLimitItem Toff { get; set; }
		/// <summary>IP[mA]</summary>
		/// <remarks>
		/// (IP[A]*1000の値を格納)
		/// </remarks>
		public StructureUpperAndLowerLimitItem IPVal { get; set; }
        /// <summary>SFIP[mA]</summary>
        /// <remarks>
        /// (IP[A]*1000の値を格納)
        /// </remarks>
        public StructureUpperAndLowerLimitItem SFIPVal { get; set; }
        /// <summary>CAP[nF]</summary>
        /// <remarks>
        /// (CAP[uF]*1000の値を格納)
        /// </remarks>
        public StructureUpperAndLowerLimitItem CAPVal { get; set; }
		/// <summary>サーボコントロールDA(0-63)</summary>
		public StructureUpperAndLowerLimitItem SCVal { get; set; }
		/// <summary>SP軸コントロールDA(0-15)</summary>
		public StructureUpperAndLowerLimitItem CRSVal { get; set; }
		/// <summary>加工サーボ送り速度選択(0-15)</summary>
		public StructureUpperAndLowerLimitItem SfrFrSel { get; set; }
		/// <summary>加工サーボ戻り速度選択(0-15)</summary>
		public StructureUpperAndLowerLimitItem SfrBkSel { get; set; }
		/// <summary>ポンプ出力(0-3)</summary>
		public StructureUpperAndLowerLimitItem PompVal { get; set; }
		/// <summary>サーボ選択(0-3)</summary>
		public StructureUpperAndLowerLimitItem ServoSel { get; set; }
		/// <summary>電源選択(0-5)</summary>
		public StructureUpperAndLowerLimitItem PSSel { get; set; }
		/// <summary>条件切替え</summary>
		public StructureUpperAndLowerLimitItem POLVal { get; set; }
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
		protected void Dispose( bool disposing )
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
				//  基底クラスのDispose()を確実に呼び出す。
				;	// base.Dispose( disposing );
			}
		}
		/// <summary>クローン</summary>
		/// <returns>クローンされたインスタンス</returns>
		public object Clone()
		{
            StructureProcessConditionLimitItem temp = new StructureProcessConditionLimitItem();
			temp.Ton = (StructureUpperAndLowerLimitItem)Ton.Clone();
			temp.Toff = (StructureUpperAndLowerLimitItem)Toff.Clone();
			temp.IPVal = (StructureUpperAndLowerLimitItem)IPVal.Clone();
			temp.CAPVal = (StructureUpperAndLowerLimitItem)CAPVal.Clone();
			temp.SCVal = (StructureUpperAndLowerLimitItem)SCVal.Clone();
			temp.CRSVal = (StructureUpperAndLowerLimitItem)CRSVal.Clone();
			temp.SfrFrSel = (StructureUpperAndLowerLimitItem)SfrFrSel.Clone();
			temp.SfrBkSel = (StructureUpperAndLowerLimitItem)SfrBkSel.Clone();
			temp.PompVal = (StructureUpperAndLowerLimitItem)PompVal.Clone();
			temp.ServoSel = (StructureUpperAndLowerLimitItem)ServoSel.Clone();
			temp.PSSel = (StructureUpperAndLowerLimitItem)PSSel.Clone();
			temp.POLVal = (StructureUpperAndLowerLimitItem)POLVal.Clone();
			return temp;
		}
		/// <summary>コピー</summary>
		/// <param name="source">コピー元</param>
		public void Copy( StructureProcessConditionLimitItem source )
		{
			Ton.Copy(source.Ton);
			Toff.Copy(source.Toff);
			IPVal.Copy(source.IPVal);
			CAPVal.Copy(source.CAPVal);
			SCVal.Copy(source.SCVal);
			CRSVal.Copy(source.CRSVal);
			SfrFrSel.Copy(source.SfrFrSel);
			SfrBkSel.Copy(source.SfrBkSel);
			PompVal.Copy(source.PompVal);
			ServoSel.Copy(source.ServoSel);
			PSSel.Copy(source.PSSel);
			POLVal.Copy(source.POLVal);
		}
		/// <summary>初期化</summary>
		/// <param name="includedNumber">加工条件番号に対する初期化の要否</param>
		public void Clear()
		{
			Ton.Clear();
			Toff.Clear();
			IPVal.Clear();
			CAPVal.Clear();
			SCVal.Clear();
			CRSVal.Clear();
			SfrFrSel.Clear();
			SfrBkSel.Clear();
			PompVal.Clear();
			ServoSel.Clear();
			PSSel.Clear();
			POLVal.Clear();
		}
		public int Compare( StructureProcessConditionLimitItem source )
		{
			int ret = 0;
			while( true ) {
				ret = Ton.Compare( source.Ton );
				if( 0 != ret ) {
					break;
				}
				ret = Toff.Compare( source.Toff );
				if( 0 != ret ) {
					break;
				}
				ret = IPVal.Compare( source.IPVal );
				if( 0 != ret ) {
					break;
				}
				ret = CAPVal.Compare( source.CAPVal );
				if( 0 != ret ) {
					break;
				}
				ret = SCVal.Compare( source.SCVal );
				if( 0 != ret ) {
					break;
				}
				ret = CRSVal.Compare( source.CRSVal );
				if( 0 != ret ) {
					break;
				}
				ret = SfrFrSel.Compare( source.SfrFrSel );
				if( 0 != ret ) {
					break;
				}
				ret = SfrBkSel.Compare( source.SfrBkSel );
				if( 0 != ret ) {
					break;
				}
				ret = PompVal.Compare( source.PompVal );
				if( 0 != ret ) {
					break;
				}
				ret = ServoSel.Compare( source.ServoSel );
				if( 0 != ret ) {
					break;
				}
				ret = PSSel.Compare( source.PSSel );
				if( 0 != ret ) {
					break;
				}
				ret = POLVal.Compare( source.POLVal );
				if( 0 != ret ) {
					break;
				}
                break;
			}
			return ret;
		}
	}
}
