using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;
using ECNC3.Enumeration;
using ECNC3.Models.Common;
using static Rt64ecdata;
using static Rt64ecgcdapi;

namespace ECNC3.Models.McIf
{
	/// <summary>4-6-1.プログラム変換ライブラリ</summary>
	public class McGcdInitData : McGcdBasic, IDisposable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>コンストラクタ</summary>
		public McGcdInitData()
		{
			ClassName = GetType().Name;
		}
		/// <summary>ユーザ定義コードファイル格納ディレクトリ</summary>
		public string PathUcd
		{
			get
			{
				string path = Path.Combine( Directory.GetCurrentDirectory(), @"\UserPrg\Ucd" );
				if( false == Directory.Exists( path ) ) {
					Directory.CreateDirectory( path );
				}
				return path;
			}
		}
		/// <summary>インスタンスの破棄</summary>
		public new void Dispose()
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
		protected override void Dispose( bool disposing )
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
				base.Dispose( disposing );
			}
		}
		/// <summary>初期化</summary>
		/// <param name="fs">設定ファイルアクセスクラスの参照</param>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// インチモードは考慮していません。メトリックのみであるとしています。
		/// </remarks>
		public ResultCodes Initialize( FileSettings fs )
		{
			AidLog logs = new AidLog( "McGcdInitData.Initialize" );
			ResultCodes ret = ResultCodes.McNotInitialize;
			while( true == StandBy() ) {
				try {
					PRGINITDATA data = PRGINITDATA.Init();
					//	ＲＴＭＣ６４－ＥＣ制御周期
					//	取得元不明。従来の構造体からは取得できない。
					//	暫定でアプリケーション外部パラメータ化。既定値不明。さらに暫定で1000msをとする。
#if __KEY_UNKNOWN__
					//	DAT_RTC による取得値を設定する？
					data.RtcTime = ; /* */    
#else
					short rtc = (short)fs.AttrValue( "Root/Prog", "rtc" );
					if( ( 25 > rtc ) || ( 4000 < rtc ) ) {
						logs.Error( $"RtcTime={rtc},Collect=1000." );
						rtc = 1000;
					}
					data.RtcTime = rtc;
#endif
					//	inch/mm 指定（0:mm、1:inch）
					data.InchMode = 0;
					//	ユーザ定義Gコードファイル格納ディレクトリ
					string path = fs.AttrText( "Root/Prog/GCodDsbl", "path" );
					if( true == string.IsNullOrEmpty( path ) ) {
						path = PathUcd;
					}
					data.pGUCD = path;
					//	ユーザ定義Mコードファイル格納ディレクトリ
					path = fs.AttrText( "Root/Prog/MCodDsbl", "path" );
					if( true == string.IsNullOrEmpty( path ) ) {
						path = PathUcd;
					}
					data.pMUCD = path;
					//	inch/mm対象軸フラグ
					data.InchAxis = 0x0f;
					//	構造体サイズ
					data.StrctSize = Marshal.SizeOf( typeof( PRGINITDATA ) );
					//	無効Gコード配列
					using( XmlNodeList list = fs.GetList( "Root/Prog/GCodDsbl/Item" ) ) {
						SetCodeDisable( list, data.GcdDis );
					}
					//	無効Mコード配列
					using( XmlNodeList list = fs.GetList( "Root/Prog/MCodDsbl/Item" ) ) {
						SetCodeDisable( list, data.McdDis );
					}
#if __KEY_UNKNOWN__ //	取得元不明。
			prg.ZAcServoEn = 0; /* Ｚ軸ＡＣサーボ機能有効フラグ*/ // Ver3.0～
#endif
					int retRt64 = Rt64ecgcdapi.GcdInitialize( ref data );
					if( Syncdef.E_OK != retRt64 ) {
						ret = ConvertReturnCode( retRt64 );
					} else {
						ret = ResultCodes.Success;
					}
				} catch( Exception e ) {
					bool unexpected = true;
					if( ( e is DllNotFoundException ) ||
						( e is EntryPointNotFoundException ) ) {
						unexpected = false;   //	想定内の例外。
					}
					ret = logs.Exception( e, unexpected );
				}
				break;
			}
			return ret;
		}

		/// <summary>初期化</summary>
		/// <param name="fs">設定ファイルアクセスクラスの参照</param>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// インチモードは考慮していません。メトリックのみであるとしています。
		/// </remarks>
		public ResultCodes InitializeGMCodeDisable( FileSettingsGMCodeDisable fs )
		{
			AidLog logs = new AidLog( "McGcdInitData.InitializeGMCodeDisable" );
			ResultCodes ret = ResultCodes.McNotInitialize;
			while( true == StandBy() ) {
				try {
					PRGINITDATA data = PRGINITDATA.Init();
					//	ＲＴＭＣ６４－ＥＣ制御周期
					//	取得元不明。従来の構造体からは取得できない。
					//	暫定でアプリケーション外部パラメータ化。既定値不明。さらに暫定で1000msをとする。
#if __KEY_UNKNOWN__
					//	DAT_RTC による取得値を設定する？
					data.RtcTime = ; /* */    
#else
					short rtc = (short)fs.AttrValue( "Root/Prog", "rtc" );
					if( ( 25 > rtc ) || ( 4000 < rtc ) ) {
						logs.Error( $"RtcTime={rtc},Collect=1000." );
						rtc = 1000;
					}
					data.RtcTime = rtc;
#endif
					//	inch/mm 指定（0:mm、1:inch）
					data.InchMode = 0;
					//	ユーザ定義Gコードファイル格納ディレクトリ
					string path = fs.AttrText( "Root/Prog/GCodDsbl", "path" );
					if( true == string.IsNullOrEmpty( path ) ) {
						path = PathUcd;
					}
					data.pGUCD = path;
					//	ユーザ定義Mコードファイル格納ディレクトリ
					path = fs.AttrText( "Root/Prog/MCodDsbl", "path" );
					if( true == string.IsNullOrEmpty( path ) ) {
						path = PathUcd;
					}
					data.pMUCD = path;
					//	inch/mm対象軸フラグ
					data.InchAxis = 0x0f;
					//	構造体サイズ
					data.StrctSize = Marshal.SizeOf( typeof( PRGINITDATA ) );
					//	無効Gコード配列
					using( XmlNodeList list = fs.GetList( "Root/Prog/GCodDsbl/Item" ) ) {
						SetCodeDisable( list, data.GcdDis );
					}
					//	無効Mコード配列
					using( XmlNodeList list = fs.GetList( "Root/Prog/MCodDsbl/Item" ) ) {
						SetCodeDisable( list, data.McdDis );
					}
#if __KEY_UNKNOWN__ //	取得元不明。
			prg.ZAcServoEn = 0; /* Ｚ軸ＡＣサーボ機能有効フラグ*/ // Ver3.0～
#endif
					int retRt64 = Rt64ecgcdapi.GcdInitialize( ref data );
					if( Syncdef.E_OK != retRt64 ) {
						ret = ConvertReturnCode( retRt64 );
					} else {
						ret = ResultCodes.Success;
					}
				} catch( Exception e ) {
					bool unexpected = true;
					if( ( e is DllNotFoundException ) ||
						( e is EntryPointNotFoundException ) ) {
						unexpected = false;   //	想定内の例外。
					}
					ret = logs.Exception( e, unexpected );
				}
				break;
			}
			return ret;
		}

		/// <summary>Gコード，Mコード無効配列設定</summary>
		/// <param name="nodeList">無効情報が設定されているノードリスト</param>
		/// <param name="target">設定対象の配列</param>
		/// <remarks>
		/// 無効情報のみを設定します。引数はあらかじめ有効で初期化したものにしてください。
		/// </remarks>
		private void SetCodeDisable( XmlNodeList nodeList, byte[] target )
		{
			if( null != nodeList ) {
				int length = target.Length;
				AidConvert cnv = new AidConvert();
				int number;
				byte disabled;
				int val = 0;
				foreach( XmlNode element in nodeList ) {
					using( StructurePartitionItem item = new StructurePartitionItem() ) {
						number = -1;
						disabled = (byte)0;
						foreach( XmlNode attr in element.Attributes ) {
							if( "num" == attr.Name ) {
								if( true == cnv.TryParse( attr.Value, out val ) ) {
									number = val;
								}
							} else if( "dsbl" == attr.Name ) {
								if( true == cnv.TryParse( attr.Value, out val ) ) {
									disabled = (byte)( ( 0 != val ) ? 1 : 0 );
								}
							}
						}
						if( ( 0 > number ) || ( length < number ) ) {
							continue;
						}
						target[number] = disabled;
					}
				}
			}
		}
	}
}
