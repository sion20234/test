using System;
using System.Diagnostics;
using ECNC3.Enumeration;
using ECNC3.Models.Common;
using static Rt64ecdata;

namespace ECNC3.Models.McIf
{
	/// <summary>MCインターフェース基底クラス</summary>
	public class McIfBasic : IDisposable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>コンストラクタ</summary>
		public McIfBasic()
		{
		}
		/// <summary>名称</summary>
		public string Name { get; protected set; }
		/// <summary>クラス名</summary>
		public string ClassName { get; internal set; }
		/// <summary>机上モード</summary>
		protected BootModes BootMode { get; set; }

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
				;   //  基底クラスのDispose()を確実に呼び出す。
					//base.Dispose( disposing );
			}
		}

		/// <summary>関数呼び出し準備</summary>
		/// <returns>
		///		<item>true=準備完了</item>
		///		<item>false=準備未完</item>
		/// </returns>
		/// <remarks>
		/// MCRT64EC 提供関数呼び出しの準備を行います。
		/// </remarks>
		protected bool StandBy()
		{
			using( ECNC3Settings es = new ECNC3Settings() ) {
				while( null != es ) {
					if( false == es.WasMcInitialzed ) {
						break;
					}
					//	机上モードを判定
					BootMode = es.BootMode;
					return true;
				}
			}
			return false;
		}

		/// <summary>配列のコピー</summary>
		/// <typeparam name="T">コピー変数の型</typeparam>
		/// <param name="source">コピー元</param>
		/// <param name="target">コピー先</param>
		protected void Copy<T>( T[] source, ref T[] target )
		{
			int index = 0;
			if( ( null != source ) && ( null != target ) ) {
				for( index = 0 ; ( index < source.Length ) && ( index < target.Length ) ; ++index ) {
					target[index] = source[index];
				}
			}
		}
		/// <summary>配列の比較</summary>
		/// <typeparam name="T">コピー変数の型</typeparam>
		/// <param name="source">比較元</param>
		/// <param name="target">比較先</param>
		/// <returns>比較結果
		///		<list type="bullet" >
		///			<item>true=一致</item>
		///			<item>false=不一致</item>
		///		</list>
		/// </returns>
		/// <remarks>
		///	sbyte配列の比較を行います。
		///	両方とも null のケースは一致と判断します。
		/// </remarks>
		protected bool Equals<T>( T[] source, T[] target )
		{
			if( ( null == source ) && ( null == target ) ) {
				return true;
			}
			if( ( null != source ) && ( null != target ) ) {
				if( source.Length != target.Length ) {
					return false;
				}
				int index = 0;
				for( index = 0 ; ( index < source.Length ) && ( index < target.Length ) ; ++index ) {
					if( false == target[index].Equals( source[index] ) ) {
						return false;
					}
				}
				return true;
			}
			return false;
		}
		/// <summary>範囲外チェック</summary>
		/// <param name="source">設定値</param>
		/// <param name="minValue">設定許可最小値</param>
		/// <param name="maxValue">設定許可最大値</param>
		/// <param name="name">項目名称</param>
		/// <returns>判定結果
		///		<list type="bullet" >
		///			<item>true=範囲外</item>
		///			<item>false=範囲内</item>
		///		</list>
		/// </returns>
		protected bool OutofRange( int source, int minValue, int maxValue, string name = null )
		{
			if( ( minValue > source ) || ( maxValue < source ) ) {
				ECNC3Log logs = new ECNC3Log( "McIfBasic.OutofRange" );
				if( true == string.IsNullOrEmpty( name ) ) {
					logs.Error( $"Value={source},Min={minValue},Max={maxValue}" );
				} else {
					logs.Error( $"Name={name},Value={source},Min={minValue},Max={maxValue}" );
				}
				return true;
			}
			return false;
		}
	}
}