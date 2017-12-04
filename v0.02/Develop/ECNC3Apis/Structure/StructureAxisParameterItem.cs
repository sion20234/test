using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECNC3.Models
{
	/// <summary>加工条件ファイル構造</summary>
	public class StructureAxisParameterItem : IDisposable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;

        public string AecSoftLimM = "";
        public string AecSoftLimP = "";
        public string AprFeed = "";
        public string BackL = "";
        public string Dx = "";
        public string ErMax = "";
        public string handle_ka = "";
        public string handle_max = "";
        public string Homepos = "";
        public string Homepri = "";
        public string InPos = "";
        public string JogFeed = "";
        public string Ka = "";
        public string OrgCsetOfs = "";
        public string OrgDir = "";
        public string OrgFeed = "";
        public string OrgOfs = "";
        public string OrgPos = "";
        public string OrgPri = "";
        public string PrcsKa = "";
        public string PtpFeed = "";
        public string Revise = "";
        public string SKa = "";
        public string SoftLimM = "";
        public string SoftLimP = "";
        public string SrchFeed = "";
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
	}
}
