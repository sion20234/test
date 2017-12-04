using System;
using System.Text;
using ECNC3.Enumeration;

namespace ECNC3.Models.Common
{
    /// <summary>ログ出力支援</summary>
    public class AidLog : ECNC3Log
	{
        public AidLog()
        { }

        /// <summary>コンストラクタ</summary>
        /// <param name="functionName">関数名
        /// 「クラス名.メソッド名」の書式で設定する。
        /// </param>
        public AidLog( string functionName )
		{
			FunctionName = "MC," + functionName;
		}
        /// <summary>例外ログ</summary>
        /// <param name="obj">例外オブジェクト</param>
        /// <param name="isCatchOfUnexpectedException">想定された例外の受信であるか
        ///		<list type="bullet" >
        ///			<item>true=想定外の例外のキャッチによるログ出力である。</item>
        ///			<item>false=想定内の例外のキャッチによるログ出力である。</item>
        ///		</list>
        /// </param>
        /// <param name="note">備考</param>
        /// <remarks>
        /// OSより発行された例外をログ出力します。
        /// 戻り値は、「OSによりキャッチされた例外」で統一しました。
        /// これは、当関数の呼び出しがある時点でなにかしらの不具合が存在すること。
        /// ログを見なければ詳細がわからず、ログを見れば詳細がわかることによります。
        /// </remarks>
        /// <returns>ResultCodes.ExceptionFromWindows</returns>
        public ResultCodes Exception( Exception obj, bool isCatchOfUnexpectedException, string note = null )
		{
			if( true == isCatchOfUnexpectedException ) {
				base.Exception( obj, note + @",Catch of the unexpected exception!" );
			} else {
				base.Exception( obj, note );
			}
			return ResultCodes.ExceptionFromWindows;
		}
		/// <summary>int配列のCSV文字配列変換</summary>
		/// <param name="source">変換元の配列</param>
		/// <returns>変換結果</returns>
		public string ToString( int[] source )
		{
			StringBuilder sb = new StringBuilder( source.Length );
			int index = 0;
			for( index = 0 ; index < source.Length ; ++index ) {
				sb.Append( source[index] );
				if( index < ( source.Length - 1 ) ) {
					sb.Append( "," );
				}
			}
			return sb.ToString();
		}
	}
}
