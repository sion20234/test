namespace ECNC3.Models.Common
{
	/// <summary>ビット操作支援クラス</summary>
	public class AidBit
	{
		/// <summary>コンストラクタ</summary>
		public AidBit()
		{
		}
		/// <summary>有効判定</summary>
		/// <param name="source">判定対象</param>
		/// <param name="mask">マスク値</param>
		/// <returns>判定結果
		///		<list type="bullet" >
		///			<item>true=オン</item>
		///			<item>false=オフ</item>
		///		</list>
		/// </returns>
		public bool IsTrue( ushort source, int mask )
		{
			return ( mask == ( source & mask ) ) ? true : false;
		}
		/// <summary>有効判定</summary>
		/// <param name="source">判定対象</param>
		/// <param name="mask">マスク値</param>
		/// <returns>判定結果
		///		<list type="bullet" >
		///			<item>true=オン</item>
		///			<item>false=オフ</item>
		///		</list>
		/// </returns>
		public bool IsTrue( int source, int mask )
		{
			return ( mask == ( source & mask ) ) ? true : false;
		}
		/// <summary>有効判定</summary>
		/// <param name="source">判定対象</param>
		/// <param name="mask">マスク値</param>
		/// <returns>判定結果
		///		<list type="bullet" >
		///			<item>true=オン</item>
		///			<item>false=オフ</item>
		///		</list>
		/// </returns>
		public bool IsTrue( long source, int mask )
		{
			return ( mask == ( source & mask ) ) ? true : false;
		}
		/// <summary>ビット設定</summary>
		/// <param name="target">設定対象</param>
		/// <param name="mask">マスク値</param>
		/// <param name="setOn">設定内容
		///		<list type="bullet" >
		///			<item>true=オン</item>
		///			<item>false=オフ</item>
		///		</list>
		/// </param>
		/// <returns>設定結果</returns>
		public short SetBit( short target, int mask, bool setOn )
		{
			int source = target;
			if( true == setOn ) {
				source |= mask;
			} else {
				source &= ~mask;
			}
			return (short)source;
		}
		/// <summary>ビット設定</summary>
		/// <param name="target">設定対象</param>
		/// <param name="mask">マスク値</param>
		/// <param name="setOn">設定内容
		///		<list type="bullet" >
		///			<item>true=オン</item>
		///			<item>false=オフ</item>
		///		</list>
		/// </param>
		/// <returns>設定結果</returns>
		public ushort SetBit( ushort target, int mask, bool setOn )
		{
			int source = target;
			if( true == setOn ) {
				source |= mask;
			} else {
				source &= ~mask;
			}
			return (ushort)source;
		}
		/// <summary>ビット設定</summary>
		/// <param name="target">設定対象</param>
		/// <param name="mask">マスク値</param>
		/// <param name="setOn">設定内容
		///		<list type="bullet" >
		///			<item>true=オン</item>
		///			<item>false=オフ</item>
		///		</list>
		/// </param>
		/// <returns>設定結果</returns>
		public int SetBit( int target, int mask, bool setOn )
		{
			int source = target;
			if( true == setOn ) {
				source |= mask;
			} else {
				source &= ~mask;
			}
			return source;
		}
		/// <summary>ビット設定</summary>
		/// <param name="target">設定対象</param>
		/// <param name="mask">マスク値</param>
		/// <param name="setOn">設定内容
		///		<list type="bullet" >
		///			<item>true=オン</item>
		///			<item>false=オフ</item>
		///		</list>
		/// </param>
		/// <returns>設定結果</returns>
		public long SetBit( long target, long mask, bool setOn )
		{
			long source = target;
			if( true == setOn ) {
				source |= mask;
			} else {
				source &= ~mask;
			}
			return source;
		}
	}
}
