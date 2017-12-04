using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECNC3.Models.Common;

namespace ECNC3.Models
{
    /// <summary>日付情報</summary>
    public class StructureDateTimeItem
	{
		/// <summary>コンストラクタ</summary>
		public StructureDateTimeItem()
		{
		}
		/// <summary>番号</summary>
		public int Number { get; set; }
		/// <summary>日付情報</summary>
		public DateTime Value { get; set; }
		/// <summary>定型化された日付書式</summary>
		internal string FixedFormDateTimeText
		{
			get
			{
				return Value.ToString( "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture );
			}
			set
			{
				DateTime dt;
				if( true == TryParse( value, out dt ) ) {
					Value = dt;
				}
			}
		}

		/// <summary>日付情報文字列の解析</summary>
		/// <param name="source">解析元</param>
		/// <param name="result">解析結果</param>
		/// <returns>変換結果
		///		<list type="bullet" >
		///			<item>true=成功</item>
		///			<item>false=失敗</item>
		///		</list>
		/// </returns>
		/// <remarks>
		/// 日付情報の解析を行います。
		/// DateTime.TryParseを使用しないのはINIファイル上の書式の詳細が不明であるため。
		/// </remarks>
		private bool TryParse( string source, out DateTime result )
		{
			if( ( true == source.Contains( "/" ) ) || ( true == source.Contains( ":" ) ) ) {
				return TryParseDateTime( source, out result );
			} else if( ( true == source.Contains( "H" ) ) || ( true == source.Contains( "M" ) ) || ( true == source.Contains( "S" ) ) ) {
				return TryParseTimeSpan( source, out result );
			}
			result = new DateTime();
			return false;
		}
		/// <summary>日付書式の解析</summary>
		/// <param name="source">変換元の文字列</param>
		/// <param name="result">変換された日付情報</param>
		/// <returns>変換結果
		///		<list type="bullet" >
		///			<item>true=成功</item>
		///			<item>false=失敗</item>
		///		</list>
		/// </returns>
		private bool TryParseDateTime( string source, out DateTime result )
		{
			//	日付
			string[] splits = source.Split( '/' );
			while( 2 < splits.Length ) {
                int year = 0;
                int month = 0;
                int day = 0;
                if( false == int.TryParse( splits[0].Trim(), out year ) ) {
					break;
				}
				if( false == int.TryParse( splits[1].Trim(), out month ) ) {
					break;
				}
				if( false == int.TryParse( splits[2].Trim(), out day ) ) {
					break;
				}
				result = new DateTime( year, month, day );
				return true;
			}
			result = new DateTime();
			return false;
		}
		/// <summary>経過時間の解析</summary>
		/// <param name="source">変換元の文字列</param>
		/// <param name="result">変換された日付情報</param>
		/// <returns>変換結果
		///		<list type="bullet" >
		///			<item>true=成功</item>
		///			<item>false=失敗</item>
		///		</list>
		/// </returns>
		private bool TryParseTimeSpan( string source, out DateTime result )
		{
			int hour = 0;
			int minute = 0;
			int sec = 0;
			//	経過時間
			string[] splits1 = source.Split( 'H' );
			while( 1 < splits1.Length ) {
				if( false == int.TryParse( splits1[0].Trim(), out hour ) ) {
					break;
				}
				string[] splits2 = splits1[1].Split( 'M' );
				while( 1 < splits2.Length ) {
					if( false == int.TryParse( splits2[0].Trim(), out minute ) ) {
						break;
					}
					string[] splits3 = splits2[1].Split( 'S' );
					if( false == int.TryParse( splits3[0].Trim(), out sec ) ) {
						break;
					}
					result = new DateTime( new TimeSpan( hour, minute, sec ).Ticks );
					return true;
				}
				break;
			}
			result = new DateTime();
			return false;
		}
	}
}
