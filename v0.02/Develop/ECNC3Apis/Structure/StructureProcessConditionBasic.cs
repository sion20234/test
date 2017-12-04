#if false
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECNC3.Models
{
	/// <summary>加工条件ファイル基本構造</summary>
    public class StructureProcessConditionBasic : ICloneable, IDisposable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>コンストラクタ</summary>
		public StructureProcessConditionBasic()
		{
		}
		/// <summary>P番号</summary>
		public int Number { get; set; }
		/// <summary>T-ON</summary>
		public int TurnOn { get; set; }
		/// <summary>T-OFF</summary>
		public int TurnOff { get; set; }
		/// <summary>電流値</summary>
		public int IP { get; set; }
		/// <summary>コンデンサ</summary>
		public int Capacitor { get; set; }
		/// <summary>サーボコントロール</summary>
		public int ServoControl { get; set; }
		/// <summary>SFR;Servo Feed Rate UP</summary>
		public int ServoFeedRateUp { get; set; }
		/// <summary>SFR;Servo Feed Rate DOWN</summary>
		public int ServoFeedRateDown { get; set; }
		/// <summary>CRS;C Rotation Speed</summary>
		public int CRotationSpeed { get; set; }
		/// <summary>JUMP UP ストローク時間</summary>
		public int JumpUpStrokeTime { get; set; }
		/// <summary>JUMP DW ストローク時間</summary>
		public int JumpDownStrokeTime { get; set; }
		/// <summary>極性</summary>
		public int Polarity { get; set; }

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
		/// <returns>複成されたインスタンス</returns>
		public object Clone()
		{
			StructureProcessConditionBasic temp = new StructureProcessConditionBasic();
			Copy( this );
			return temp;
		}
		/// <summary>コピー</summary>
		/// <param name="source">コピー元</param>
		public void Copy( StructureProcessConditionBasic source )
		{
			Number = source.Number;
			TurnOn = source.TurnOn;
			TurnOff = source.TurnOff;
			IP = source.IP;
			Capacitor = source.Capacitor;
			ServoControl = source.ServoControl;
			ServoFeedRateUp = source.ServoFeedRateUp;
			ServoFeedRateDown = source.ServoFeedRateDown;
			CRotationSpeed = source.CRotationSpeed;
			JumpUpStrokeTime = source.JumpUpStrokeTime;
			JumpDownStrokeTime = source.JumpDownStrokeTime;
			Polarity = source.Polarity;
		}
	}
}
#endif