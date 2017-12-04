using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using ECNC3.Enumeration;
using ECNC3.Models.Common;

namespace ECNC3.Models
{
	/// <summary>設定ファイルアクセス</summary>
	public class UserSettings : FileAccessCommon, IEcnc3Backup, IDisposable
	{
		#region 初期設定
		/// <summary>SERVICE ユーザ名称</summary>
		private readonly string _userNameAsService = "SERVICE";
		/// <summary>SERVICE パスワード</summary>
		private readonly string _passwordAsService = "0000";
		/// <summary>FACTORY ユーザ名称</summary>
		private readonly string _userNameAsFactory = "FACTORY";
		/// <summary>FACTORY パスワード</summary>
		private readonly string _passwordAsFactory = "8188";
		#endregion
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;

		/// <summary>コンストラクタ</summary>
		public UserSettings()
		{
			Name = this.ToString();
			RegistMasterFile( @"Users.xml" );
		}
		/// <summary>ユーザリスト</summary>
		public StructureUserSettingList Items { get; set; }

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
		private void Dispose( bool disposing )
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
	
		/// <summary>バックアップ</summary>
		/// <param name="backupDirectory">バックアップ先ディレクトリパス</param>
		/// <returns>実行結果</returns>
		public ResultCodes Backup( string backupDirectory )
		{
			return base.Backup( FilePath, backupDirectory );
		}
		/// <summary>リストア</summary>
		/// <param name="restoreDirectory">復元元情報の格納されたディレクトリパス</param>
		/// <returns>実行結果</returns>
		public ResultCodes Restore( string restoreDirectory )
		{
			return base.Restore( restoreDirectory, FilePath );
		}
		/// <summary>読み込み</summary>
		/// <returns>実行結果</returns>
		public ResultCodes Load()
		{
			AidLog logs = new AidLog( "UserSettings.Load" );
			if( null != Items ) {
				Items.Clear();
			} else {
				Items = new StructureUserSettingList();
			}

			ResultCodes ret = ResultCodes.NotFound;
			try {
				while( true ) {
					//	ファイルの有無を確認する。
					if( false == ExistsFile() ) {
						//	ファイルなし。初期設定による確認。
						break;
					}
					AidXmlLinq xml = new AidXmlLinq();
					XElement file = null;
					//	ファイル読み込み
					ret = xml.ReadByReadOnly( ref file, FilePath );
					if( ( ResultCodes.Success == ret ) && ( null != file ) ) {
						try {
							IEnumerable<XElement> list = file.Elements( "Item" );
							if( null == list ) {
								ret = ResultCodes.FailToReadFile;
							} else {
								foreach( XElement xe in list ) {
									using( StructureUserSettingItem item = new StructureUserSettingItem() ) {
										item.UserName = xml.GetAttrText( xe, "nam" );
										item.Password = xml.GetAttrText( xe, "pw" );
										using( AidCryptograph crypt = new AidCryptograph() { Source = item.Password, Password = item.UserName } ) {
											if( 0 != crypt.Decrypt() ) {
												item.Password = crypt.Result;
											}
										}
										Items.Add( item.Clone() as StructureUserSettingItem );
									}
								}
							}
						} catch( Exception e ) {
							bool unexpected = true;
							if( ( e is ObjectDisposedException ) ||
								( e is ArgumentOutOfRangeException ) ||
								( e is System.Threading.AbandonedMutexException ) ||
								( e is FileNotFoundException ) ||
								( e is XmlException ) ||
								( e is FormatException ) ||
								( e is NullReferenceException ) ||
								( e is IOException ) ) {
								unexpected = false;   //	想定内の例外。
							}
							ret = logs.Exception( e, unexpected );
						}
					}
					break;
				}
				//	デフォルトユーザの有無の確認
				if( null == Items.FindName( _userNameAsFactory ) ) {
					Items.Add( new StructureUserSettingItem() {
						UserName = _userNameAsFactory,
						Password = _passwordAsFactory
					} );
				}
				if( null == Items.FindName( _userNameAsService ) ) {
					Items.Add( new StructureUserSettingItem() {
						UserName = _userNameAsService,
						Password = _passwordAsService
					} );
				}
			} catch( Exception e ) {
				bool unexpected = true;
				if( ( e is ArgumentNullException ) ||
					( e is InvalidOperationException ) ) {
					unexpected = false;   //	想定内の例外。
				}
				ret = logs.Exception( e, unexpected );
			}
			return ret;
		}

		/// <summary>ユーザー情報取得</summary>
		/// <param name="userName">検索対象のユーザ名</param>
		/// <param name="result">取得されたユーザ情報</param>
		/// <returns>実行結果</returns>
		public ResultCodes GetUser( string userName, StructureUserSettingItem result )
		{
			if( true == string.IsNullOrEmpty( userName ) ) {
				return ResultCodes.InvalidArgument;
			}
			if( null == result ) {
				result = new StructureUserSettingItem();
			}
			result.UserName = userName;
			AidLog logs = new AidLog( "UserSettings.GetUserInfomation" );

			ResultCodes ret = ResultCodes.NotFound;
			try {
				while( true ) {
					//	ファイルの有無を確認する。
					if( false == ExistsFile() ) {
						//	ファイルなし。初期設定による確認。
						ret = CheckDefaultUser( result );
						break;
					}
					AidXmlLinq xml = new AidXmlLinq();
					XElement file = null;
					//	ファイル読み込み
					ret = xml.ReadByReadOnly( ref file, FilePath );
					if( ResultCodes.Success != ret ) {
						break;
					}
					XElement xe = null;
					ret = xml.FindName( file, userName, ref xe );
					if( null == xe ) {
						//	該当なし。ただし、FACTROY と SERVICE はなければ初期値を持つ。
						ret = CheckDefaultUser( result );
					} else {
						//	該当あり。パスワードを抽出。
						//	パスワードなしは認めない。
						string pw = xml.GetAttrText( xe, "pw" );
						if( true == string.IsNullOrEmpty( pw ) ) {
							ret = ResultCodes.NotFound;
							break;
						}
						using( AidCryptograph aid = new AidCryptograph() ) {
							aid.Source = pw;
							aid.Password = userName;
							if( 0 != aid.Decrypt() ) {
								ret = ResultCodes.FailToDecryption;
								break;
							}
							result.Password = aid.Result;
							ret = ResultCodes.Success;
						}
					}
					break;
				}
			} catch( Exception e ) {
				bool unexpected = true;
				if( ( e is ArgumentNullException ) ||
					( e is InvalidOperationException ) ) {
					unexpected = false;   //	想定内の例外。
				}
				ret = logs.Exception( e, unexpected );
			}
			return ret;
		}
		/// <summary>初期状態時の回避措置</summary>
		/// <param name="result">判定対象となるユーザ情報</param>
		/// <returns>
		///		<list type="bullet" >
		///			<item>true=初期設定ユーザ名に該当あり。パスワード発行。</item>
		///			<item>false=初期設定ユーザ名に該当なし。</item>
		///		</list>
		/// </returns>
		/// <remarks>
		/// ファイルがない、ユーザがない場合でも初期設定において認められる名称については、パスワードを発行します。
		/// </remarks>
		private ResultCodes CheckDefaultUser( StructureUserSettingItem result )
		{
			while( true ) {
				if( 0 == string.Compare( _userNameAsFactory, result.UserName, false ) ) {
					result.Password = _passwordAsFactory;
				} else if( 0 == string.Compare( _userNameAsService, result.UserName, false ) ) {
					result.Password = _passwordAsService;
				} else {
					break;
				}
				return ResultCodes.Success;
			}
			return ResultCodes.NotFound;
		}

		/// <summary>ユーザー情報設定</summary>
		/// <param name="result">取得されたユーザ情報</param>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// ユーザ情報を更新します。
		/// 該当がない場合は、追加します。
		/// </remarks>
		private ResultCodes SetUser( StructureUserSettingItem result )
		{
			if( null == result ) {
				return ResultCodes.InvalidArgument;
			} else if( true == string.IsNullOrEmpty( result.UserName ) ) {
				return ResultCodes.InvalidArgument;
			} else if( true == string.IsNullOrEmpty( result.Password ) ) {
				return ResultCodes.InvalidArgument;
			}
			string output = string.Empty; ;
			AidLog logs = new AidLog( "UserSettings.SetUserInfomation" );
			using( AidCryptograph aid = new AidCryptograph() ) {
				aid.Source = result.Password;
				aid.Password = result.UserName;
				if( 0 == aid.Encrypt() ) {
					output = aid.Result;
				}
			}
			AidXmlLinq xml = new AidXmlLinq();
			XElement file = null;
			ResultCodes ret = ResultCodes.Success;
			try {
				if( false == ExistsFile() ) {
					file = new XElement( "Root" );
				} else {
					ret = xml.ReadByReadWrite( ref file, FilePath );
				}
				XElement xe = null;
				ret = xml.FindName( file, result.UserName, ref xe );
				if( null == xe ) {
					//	該当なし。追加する。
					file.Add( new XElement( "Item", new XAttribute( "nam", result.UserName ), new XAttribute( "pw", output ) ) );
				} else {
					//	該当あり。暗号化。パスワードなしは認めない。
					xml.SetAttr( xe, "pw", output );
				}
				xml.Write( file, FilePath );
			} catch( Exception e ) {
				bool unexpected = true;
				if( ( e is ArgumentNullException ) ||
					( e is InvalidOperationException ) ) {
					unexpected = false;   //	想定内の例外。
				}
				ret = logs.Exception( e, unexpected );
			}
			return ret;
		}
		/// <summary>ユーザの追加</summary>
		/// <param name="result">追加ユーザ情報</param>
		/// <returns>実行結果</returns>
		public ResultCodes Add( StructureUserSettingItem result )
		{
			if( null == result ) {
				return ResultCodes.InvalidArgument;
			} else if( true == string.IsNullOrEmpty( result.UserName ) ) {
				return ResultCodes.InvalidArgument;
			} else if( true == string.IsNullOrEmpty( result.Password ) ) {
				return ResultCodes.InvalidArgument;
			}
			StructureUserSettingItem temp = new StructureUserSettingItem();
			ResultCodes ret = GetUser( result.UserName, temp );
			if( ResultCodes.Success != ret ) {
				if( ResultCodes.NotFound == ret ) {
					return ResultCodes.AlreadyRegistered;
				}
				return ret;
			}
			return SetUser( result );
		}
		/// <summary>ユーザ情報の更新</summary>
		/// <param name="result">追加ユーザ情報</param>
		/// <returns>実行結果</returns>
		public ResultCodes Update( StructureUserSettingItem result )
		{
			if( null == result ) {
				return ResultCodes.InvalidArgument;
			} else if( true == string.IsNullOrEmpty( result.UserName ) ) {
				return ResultCodes.InvalidArgument;
			} else if( true == string.IsNullOrEmpty( result.Password ) ) {
				return ResultCodes.InvalidArgument;
			}
			StructureUserSettingItem temp = new StructureUserSettingItem();
			ResultCodes ret = GetUser( result.UserName, temp );
			if( ResultCodes.Success != ret ) {
				return ret;
			}
			return SetUser( result );
		}
		/// <summary>ユーザ情報の削除</summary>
		/// <param name="result">追加ユーザ情報</param>
		/// <returns>実行結果</returns>
		public ResultCodes Delete( string userName )
		{
			AidLog logs = new AidLog( "UserSettings.Delete" );
			if( true == string.IsNullOrEmpty( userName ) ) {
				return ResultCodes.InvalidArgument;
			}
			StructureUserSettingItem temp = new StructureUserSettingItem();
			ResultCodes ret = GetUser( userName, temp );
			if( ResultCodes.Success != ret ) {
				return ret;
			}
			try {
				//	ファイルの有無を確認する。
				//	ファイルなしは、設定がないので無視して無条件に成功。
				ret = ResultCodes.Success;
				while( true == ExistsFile() ) {
					AidXmlLinq xml = new AidXmlLinq();
					XElement file = null;
					//	ファイル読み込み
					ret = xml.ReadByReadWrite( ref file, FilePath );
					if( ResultCodes.Success != ret ) {
						break;
					}
					XElement xe = null;
					ret = xml.FindName( file, userName, ref xe );
					if( null != xe ) {
						//	該当あり。ノードを削除
						xe.Remove();
						return xml.Write( file, FilePath );
					}
					break;
				}
			} catch( Exception e ) {
				bool unexpected = true;
				if( ( e is ArgumentNullException ) ||
					( e is InvalidOperationException ) ) {
					unexpected = false;   //	想定内の例外。
				}
				ret = logs.Exception( e, unexpected );
			}
			return ret;
		}
	}
}
