using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECNC3.Enumeration;

namespace ECNC3.Models
{
	/// <summary>軸座標構造</summary>
	public class StructureAxisCoordinate : IDisposable, ICloneable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>コンストラクタ</summary>
		public StructureAxisCoordinate()
		{
		}
		/// <summary>コンストラクタ</summary>
		/// <param name="number">軸番号</param>
		/// <param name="source">設定値</param>
		public StructureAxisCoordinate( int number, int[] source )
		{
			Number = number;
			if( 0 < source.Length ) {
				Axis1 = source[0];
			}
			if( 1 < source.Length ) {
				Axis2 = source[1];
			}
			if( 2 < source.Length ) {
				Axis3 = source[2];
			}
			if( 3 < source.Length ) {
				Axis4 = source[3];
			}
			if( 4 < source.Length ) {
				Axis5 = source[4];
			}
			if( 5 < source.Length ) {
				Axis6 = source[5];
			}
			if( 6 < source.Length ) {
				Axis7 = source[6];
			}
			if( 7 < source.Length ) {
				Axis8 = source[7];
			}
			if( 8 < source.Length ) {
				Axis9 = source[8];
			}
		}
		/// <summary>第1軸</summary>
		public int Axis1 { get; set; }
		/// <summary>第2軸</summary>
		public int Axis2 { get; set; }
		/// <summary>第3軸</summary>
		public int Axis3 { get; set; }
		/// <summary>第4軸</summary>
		public int Axis4 { get; set; }
		/// <summary>第5軸</summary>
		public int Axis5 { get; set; }
		/// <summary>第6軸</summary>
		public int Axis6 { get; set; }
		/// <summary>第7軸</summary>
		public int Axis7 { get; set; }
		/// <summary>第8軸</summary>
		public int Axis8 { get; set; }
		/// <summary>第9軸</summary>
		public int Axis9 { get; set; }

		/// <summary>番号</summary>
		/// <remarks>
		/// REQ_VIRPOSCHG_EXで有効となります。
		/// </remarks>
		public int Number { get; set; }
        /// <summary>仮想点リスト使用時の書き込み保護20170116HachinoADD</summary>
        /// <remarks>
        /// 0・・・無効
        /// 1・・・有効
        /// </remarks>
        public int Protect { get; set; }
		/// <summary>軸選択フラグ</summary>
		/// <remarks>
		/// REQ_WORGPOSCHGで有効となります。
		/// </remarks>
		public int EnableAxis { get; set; }
		/// <summary>X軸</summary>
		public int AxisX { get { return Axis1; } set { Axis1 = value; } }
		/// <summary>Y軸</summary>
		public int AxisY { get { return Axis2; } set { Axis2 = value; } }
		/// <summary>W軸</summary>
		public int AxisW { get { return Axis3; } set { Axis3 = value; } }
		/// <summary>Z軸</summary>
		public int AxisZ { get { return Axis4; } set { Axis4 = value; } }
		/// <summary>A軸</summary>
		public int AxisA { get { return Axis5; } set { Axis5 = value; } }
		/// <summary>B軸</summary>
		public int AxisB { get { return Axis6; } set { Axis6 = value; } }
		/// <summary>C軸</summary>
		public int AxisC { get { return Axis7; } set { Axis7 = value; } }

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
				;	//  基底クラスのDispose()を確実に呼び出す。
					//base.Dispose( disposing );
			}
		}
		/// <summary>クローン</summary>
		/// <returns>クローンされたインスタンス</returns>
		public object Clone()
		{
			StructureAxisCoordinate temp = new StructureAxisCoordinate();
			temp.Number = Number;
			temp.EnableAxis = EnableAxis;
			temp.Axis1 = Axis1;
			temp.Axis2 = Axis2;
			temp.Axis3 = Axis3;
			temp.Axis4 = Axis4;
			temp.Axis5 = Axis5;
			temp.Axis6 = Axis6;
			temp.Axis7 = Axis7;
			temp.Axis8 = Axis8;
			temp.Axis9 = Axis9;
            temp.Protect = Protect;
			return temp;
		}
		/// <summary>座標オフセット</summary>
		/// <param name="source">オフセットデータ</param>
		/// <returns>オフセット結果</returns>
		/// <remarks>
		/// 引数 sourceにあわせてオフセットします。
		/// </remarks>
		public StructureAxisCoordinate OffSset( StructureAxisCoordinate source )
		{
			if( null != source ) {
				AxisBits axis = (AxisBits)source.EnableAxis;
				if( true == axis.HasFlag( AxisBits.X ) ) {
					AxisX -= source.AxisX;
				}
				if( true == axis.HasFlag( AxisBits.Y ) ) {
					AxisY -= source.AxisY;
				}
				if( true == axis.HasFlag( AxisBits.W ) ) {
					AxisW -= source.AxisW;
				}
				if( true == axis.HasFlag( AxisBits.Z ) ) {
					AxisZ -= source.AxisZ;
				}
				if( true == axis.HasFlag( AxisBits.A ) ) {
					AxisA -= source.AxisA;
				}
				if( true == axis.HasFlag( AxisBits.B ) ) {
					AxisB -= source.AxisB;
				}
				if( true == axis.HasFlag( AxisBits.C ) ) {
					AxisC -= source.AxisC;
				}
			}
			return this;
		}
	}
}
