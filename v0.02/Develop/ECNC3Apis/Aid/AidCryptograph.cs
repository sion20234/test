using System;
using System.Security.Cryptography;
using System.Text;

namespace ECNC3.Models.Common
{
	/// <summary>暗号化支援</summary>
	/// <example>
	/// 暗号化
	/// using( AidCryptograph aid = new AidCryptograph() ) {
	///		aid.Source = "暗号化する文字列";
	///		aid.Password = "パスワード";
	///		//	暗号化
	///		if( 0 == aid.Encrypt() ) {
	///		output = aid.Result;	//	暗号化された文字列
	///		}
	///	}
	///	</example>
	/// <example>
	/// 復号化
	/// using( AidCryptograph aid = new AidCryptograph() ) {
	/// 	aid.Source = "復号化する文字列";
	/// 	aid.Password = "パスワード";
	/// 	//	復号化
	/// 	if( 0 == aid.Decrypt() ) {
	/// 		result = aid.Result;	//	復号化された文字列
	/// 	}
	/// }
	/// </example>
	public class AidCryptograph : IDisposable
	{
		/// <summary>ソルト値</summary>
		/// <remarks>
		/// ソルト( salt )とは、パスワードを暗号化する際に付与されるデータのこと。
		/// 通常、パスワードを保存する際には、何らかの「非可逆処理」を行うことが多い。
		///	例えば、ハッシュ関数と呼ばれる一定の関数により算出されたハッシュ値などが代表的なもので、
		///	ハッシュ値から入力値( 平文のパスワード )を再現することはできない。
		///	しかし、予め任意の文字列をハッシュ値に変換しておき、標的のパスワードのハッシュ値と比較することで
		///	パスワードを推察するパスワード破りの手法があり、こうした手法に対し、ランダムなデータをパスワードに付与してから非可逆処理を行うと、
		///	同じパスワードであっても、算出されるハッシュ値は違ったものになり、パスワードを解析されにくくする効果が期待できる。
		///	この付与されるデータをソルトという。
		///	なお、このソルト値は8バイト以上のサイズが必要である。
		///	</remarks>
		private readonly string _salt = "ELENIXCOElectricDischargeMachinesECNC3UserInterfaceAppliationSince2016";
		/// <summary>演算の反復処理回数</summary>
		private readonly int _iterations = 1000;
		/// <summary>Dispose()関数は呼び出し済みかどうか</summary>
		private bool _disposed = false;

		/// <summary>コンストラクタ</summary>
		public AidCryptograph()
		{
		}

		/// <summary>変換元の文字列</summary>
		public string Source { private get; set; }
		/// <summary>パスワード</summary>
		public string Password { private get; set; }
		/// <summary>パスワードにより変換された文字列</summary>
		public string Result { get; private set; }

		/// <summary>インスタンス破棄</summary>
		public void Dispose()
		{
			Dispose( true );
			GC.SuppressFinalize( this );
		}
		/// <summary>インスタンスの破棄</summary>
		/// <param name="disposing">Dispose()実行の是非
		///		<list type="bullet">
		///			<item>true=Dispose()関数よりの呼び出し。</item>
		///			<item>false=ファイナライザによる呼び出し。</item>
		///		</list>
		/// </param>
		protected virtual void Dispose( bool disposing )
		{
			try {
				if( false == _disposed ) {
					if( true == disposing ) {
						//	マネージリソースの解放
						Source = string.Empty;
						Password = string.Empty;
						Result = string.Empty;
					}
					//	アンマネージリソースの解放 
				}
				_disposed = true;
			} finally {
				;   //base.Dispose( disposing );	//	基底クラスのDispose() を確実にを呼び出す。
			}
		}
		/// <summary>文字列を暗号化する</summary>
		/// <returns>
		///		<list type="bullet" >
		///			<item>0=成功</item>
		///		<item>-1=失敗</item>
		///		</list>
		/// </returns>
		public int Encrypt()
		{
			try {
				using( RijndaelManaged rijndael = GenerateRijndael( Password ) ) {
					if( null != rijndael ) {
						//	文字列をバイト型配列に変換する
						byte[] strBytes = System.Text.Encoding.UTF8.GetBytes( Source );

						//対称暗号化オブジェクトの作成
						using( ICryptoTransform encryptor = rijndael.CreateEncryptor() ) {
							//バイト型配列を暗号化する
							byte[] encBytes = encryptor.TransformFinalBlock( strBytes, 0, strBytes.Length );
							//バイト型配列を文字列に変換して返す
							Result = System.Convert.ToBase64String( encBytes );
						}
					}
				}
			} catch( Exception e ) {
				bool unexpected = true;
				if( ( e is ArgumentNullException ) ||
					( e is EncoderFallbackException ) ) {
					unexpected = false;   //	想定内の例外。
				}
				AidLog logs = new AidLog( "AidCryptograph.Encrypt" );
				logs.Exception( e, unexpected );
				Result = string.Empty;
				return -1;
			}
			return 0;
		}
		/// <summary>暗号化された文字列を復号化する</summary>
		/// <returns>
		///		<list type="bullet" >
		///			<item>0=成功</item>
		///		<item>-1=失敗</item>
		///		</list>
		/// </returns>
		public int Decrypt()
		{
			try {
				using( RijndaelManaged rijndael = GenerateRijndael( Password ) ) {
					if( null != rijndael ) {
						//文字列をバイト型配列に戻す
						byte[] strBytes = System.Convert.FromBase64String( Source );

						//対称暗号化オブジェクトの作成
						using( ICryptoTransform decryptor = rijndael.CreateDecryptor() ) {
							//バイト型配列を復号化する
							//復号化に失敗すると例外CryptographicExceptionが発生
							byte[] decBytes = decryptor.TransformFinalBlock( strBytes, 0, strBytes.Length );
							//バイト型配列を文字列に戻して返す
							Result = System.Text.Encoding.UTF8.GetString( decBytes );
						}
					}
				}
			} catch( Exception e ) {
				bool unexpected = true;
				if( ( e is ArgumentNullException ) ||
					( e is EncoderFallbackException ) ) {
					unexpected = false;   //	想定内の例外。
				}
				AidLog logs = new AidLog( "AidCryptograph.Decrypt" );
				logs.Exception( e, unexpected );
				Result = string.Empty;
				return -1;
			}
			return 0;
		}
		/// <summary>パスワードから共有キーと初期化ベクタを作成</summary>
		/// <param name="password">パスワード</param>
		/// <returns>Rijndael対称暗号化アルゴリズムのオブジェクト</returns>
		private RijndaelManaged GenerateRijndael( string password )
		{
			RijndaelManaged rijndael = new RijndaelManaged();
			try {
				//	パスワードから共有キーと初期化ベクタを作成する
				byte[] salt = System.Text.Encoding.UTF8.GetBytes( _salt );
				Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes( password, salt, _iterations );
				//	共有キーと初期化ベクタを生成する
				rijndael.Key = deriveBytes.GetBytes( rijndael.KeySize / 8 );
				rijndael.IV = deriveBytes.GetBytes( rijndael.BlockSize / 8 );
			} catch( Exception e ) {
				bool unexpected = true;
				if( ( e is ArgumentNullException ) ||
					( e is ArgumentException ) ||
					( e is EncoderFallbackException ) ||
					( e is ArgumentOutOfRangeException ) ) {
					unexpected = false;   //	想定内の例外。
				}
				AidLog logs = new AidLog( "AidCryptograph.GenerateRijndael" );
				logs.Exception( e, unexpected );
				if( null != rijndael ) {
					rijndael.Dispose();
					rijndael = null;
				}
				return null;
			}
			return rijndael;
		}
	}
}
