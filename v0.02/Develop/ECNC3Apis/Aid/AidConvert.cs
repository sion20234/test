using System.Globalization;

namespace ECNC3.Models.Common
{
	/// <summary>数値変換支援</summary>
	public class AidConvert
	{
		/// <summary>16進数文字列の整数変換</summary>
		/// <param name="source">変換元文字列</param>
		/// <returns>変換された整数値</returns>
		public int ToHex( string source )
		{
			int result = 0;
			if( false == int.TryParse( source, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out result ) ) {
				return 0;
			}
			return result;
		}
		/// <summary>整数値の取得</summary>
		/// <param name="target">変換対象の文字列</param>
		/// <param name="result">変換結果</param>
		/// <returns>取得結果</returns>
		/// <remarks>
		/// 引数targetを整数値変換します。
		/// 接頭辞に＆Ｈ，＆ｈ，０ｘで始まる場合は、16進数であると解釈し、左記の文字列を取り除いた文字列に対して数値変換を実施します。
		/// </remarks>
		public bool TryParse( string target, out int result )
		{
			result = 0;
			while( false == string.IsNullOrEmpty( target ) ) {
				if( ( true == target.StartsWith( "&H" ) ) || ( true == target.StartsWith( "&h" ) ) || ( true == target.StartsWith( "0x" ) ) ) {
					//	16進数であると解釈
					if( 2 < target.Length ) {
						if( false == int.TryParse( target.Substring( 2 ), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out result ) ) {
							break;
						}
					}
				} else {
					if( false == int.TryParse( target, out result ) ) {
						break;
					}
				}
				return true;
			}
			result = 0;
			return false;
		}
		/// <summary>整数値の取得</summary>
		/// <param name="target">変換対象の文字列</param>
		/// <param name="result">変換結果</param>
		/// <returns>取得結果</returns>
		/// <remarks>
		/// 引数targetを整数値変換します。
		/// 接頭辞に＆Ｈ，＆ｈ，０ｘで始まる場合は、16進数であると解釈し、左記の文字列を取り除いた文字列に対して数値変換を実施します。
		/// </remarks>
		public bool TryParse( string target, out long result )
		{
			result = 0;
			while( false == string.IsNullOrEmpty( target ) ) {
				if( ( true == target.StartsWith( "&H" ) ) || ( true == target.StartsWith( "&h" ) ) || ( true == target.StartsWith( "0x" ) ) ) {
					//	16進数であると解釈
					if( 2 < target.Length ) {
						if( false == long.TryParse( target.Substring( 2 ), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out result ) ) {
							break;
						}
					}
				} else {
					if( false == long.TryParse( target, out result ) ) {
						break;
					}
				}
				return true;
			}
			result = 0;
			return false;
		}

		/// <summary>実数型のの解析</summary>
		/// <param name="target">変換対象の文字列</param>
		/// <param name="result">変換結果</param>
		/// <returns>取得結果</returns>
		/// <remarks>
		/// 引数targetを実数値変換します。
		/// </remarks>
		public bool TryParse( string target, out double result )
		{
			result = 0;
			while( true ) {
				int temp0 = 0;
				if( true == int.TryParse( target, out temp0 ) ) {
					result = temp0;
					break;
				} else {
					double temp1 = 0.0;
					if( true == double.TryParse( target, out temp1 ) ) {
						result = temp1;
						break;
					}
				}
				return false;
			}
			return true;
		}
	}
}
