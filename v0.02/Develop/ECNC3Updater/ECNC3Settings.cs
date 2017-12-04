using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;
using ECNC3.Enumeration;

namespace ECNC3.Models.Common
{
	/// <summary>ECNC3設定</summary>
	/// <remarks>
	/// シングルトンパターンの ECNC3SettingsEntity クラスの隠蔽を目的としたクラスです。
	/// </remarks>
	public class ECNC3Settings : IDisposable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>設定情報の実体</summary>
		private ECNC3SettingsEntity _entity = null;
		/// <summary>コンストラクタ</summary>
		public ECNC3Settings()
		{
			_entity = ECNC3SettingsEntity.GetInstance();
		}
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
						if( null != _entity ) {
							ECNC3SettingsEntity.ReleaseInstance();
							_entity = null;
						}
					}
					//  アンマネージリソースの解放
				}
				_disposed = true;
			} finally {
				;   //  基底クラスのDispose()を確実に呼び出す。
					//base.Dispose( disposing );
			}
		}
		/// <summary>通信ハンドル</summary>
		public int CommHandle
		{
			get { return _entity.CommHandle; }
			//set { _commHandle = value; }
		}
		/// <summary>モーション部初期化済み判定</summary>
		public bool WasMcInitialzed
		{
			get { return ( 0 > _entity.CommHandle ) ? false : true; }
		}

//		/// <summary>机上モードフラグ</summary>
//		public bool DeskTopMode
//		{
//			get { return _entity.DeskTopMode; }
//			private set { _entity.DeskTopMode = value; }
//		}
		/// <summary>起動モード</summary>
		public BootModes BootMode
		{
			get { return _entity.BootMode; }
			private set { _entity.BootMode = value; }
		}
		/// <summary>ログ出力レベル</summary>
		internal LogLevels LogLevel
		{
			get { return _entity.LogLevel; }
			private set { _entity.LogLevel = value; }
		}
		/// <summary>ログファイル保持数</summary>
		public int LogSaveCount
		{
			get { return _entity.LogSaveCount; }
			private set { _entity.LogSaveCount = value; }
		}
		/// <summary>ログファイル出力ディレクトリ</summary>
		public string DirectoryLog
		{
			get { return _entity.DirectoryLog; }
			set { _entity.DirectoryLog = value; }
		}
		/// <summary>マスターファイル保存ディレクトリ</summary>
		public string DirectoryMaster
		{
			get { return _entity.DirectoryMaster; }
			set { _entity.DirectoryMaster = value; }
		}
		/// <summary>モーションコントローラ出力ファイル保存ディレクトリ</summary>
		public string DirectoryRt64Ec
		{
			get { return _entity.DirectoryRt64Ec; }
			set { _entity.DirectoryRt64Ec = value; }
		}

		/// <summary>通信ハンドル設定</summary>
		/// <param name="handle">通信ハンドル</param>
		/// <remarks>
		/// RTMC64-ECの通信ハンドルを設定します。
		/// 誤設定回避のため、set プロパティは公開しません。
		/// </remarks>
		public void SetMcCommHandle( int handle )
		{
			_entity.CommHandle = handle;
		}
	}

	/// <summary>ECNC3設定実体</summary>
	/// <remarks>
	/// ログ出力は循環参照になるので禁止。
	/// </remarks>
	internal class ECNC3SettingsEntity
	{
		/// <summary>当インスタンス実体</summary>
		private static ECNC3SettingsEntity _instance = null;
		/// <summary>インスタンス参照カウンタ</summary>
		private static int _refCount = 0;
		/// <summary>RTMC64通信ハンドル</summary>
		private int _commHandle = -1;
		/// <summary>起動モード</summary>
		private BootModes _bootMode;
		/// <summary>ログ出力レベル</summary>
		private LogLevels _logLevel = LogLevels.Error;
		/// <summary>出力ログ保持数</summary>
		private int _logSaveCount = 5;
		/// <summary>ログファイル出力ディレクトリ</summary>
		private string _directoryLog = null;
		/// <summary>マスターファイル保存ディレクトリ</summary>
		private string _directoryMaster = null;
		/// <summary>モーションコントローラ出力ファイル保存ディレクトリ</summary>
		private string _directoryRt64Ec = null;
		/// <summary>コンストラクタ</summary>
		private ECNC3SettingsEntity()
		{
			CommHandle = -1;
			BootMode = BootModes.Machine;
			LogLevel = LogLevels.Debug;
			LogSaveCount = 5;
		}
		
		/// <summary>通信ハンドル</summary>
		public int CommHandle
		{
			get { return _commHandle; }
			set { _commHandle = value; }
		}
		/// <summary>起動モード</summary>
		public BootModes BootMode
		{
			get { return _bootMode; }
			set { _bootMode = value; }
		}
		/// <summary>ログ出力レベル</summary>
		public LogLevels LogLevel
		{
			get { return _logLevel; }
			set { _logLevel = value; }
		}
		/// <summary>出力ログ保持数</summary>
		public int LogSaveCount
		{
			get { return _logSaveCount; }
			set { _logSaveCount = value; }
		}
		/// <summary>ログファイル出力ディレクトリ</summary>
		public string DirectoryLog
		{
			get { return _directoryLog; }
			set { _directoryLog = value; }
		}
		/// <summary>マスターファイル保存ディレクトリ</summary>
		public string DirectoryMaster
		{
			get { return _directoryMaster; }
			set { _directoryMaster = value; }
		}
		/// <summary>モーションコントローラ出力ファイル保存ディレクトリ</summary>
		public string DirectoryRt64Ec
		{
			get { return _directoryRt64Ec; }
			set { _directoryRt64Ec = value; }
		}
		/// <summary>インスタンスの取得</summary>
		/// <returns>実体</returns>
		public static ECNC3SettingsEntity GetInstance()
		{
			if( null == _instance ) {
				//	最初のインスタンスの作成では、各種初期化処理を実施する。
				//  依存関係の都合、XMLファイルの解析クラスは使用せず、直接解析を行う。
				_instance = new ECNC3SettingsEntity();
				bool initialized = false;
				//	フォルダ情報を初期化
				string currentDirectory = Directory.GetCurrentDirectory();
				_instance._directoryRt64Ec = currentDirectory;
				_instance._directoryLog = Path.Combine( currentDirectory, "Log" );
				if( false == Directory.Exists( _instance._directoryLog ) ) {
					Directory.CreateDirectory( _instance._directoryLog );
				}
				_instance._directoryMaster = Path.Combine( currentDirectory, "Master" );

				while( true ) {
					XmlDocument doc = new XmlDocument();
					try {
						string path = Path.Combine( _instance._directoryMaster, "CncSys.xml");
						doc.Load( path );
						if( null == doc ) {
							break;
						}
						XmlNodeList list1 = doc.SelectNodes( "Root/Apl" );
						if( null == list1 ) {
							break;
						}
						if( 0 < list1.Count ) {
							//	複数検出されたとしても上端の取得結果のみを解析する。
							//AidConvert cnv = new AidConvert();
							if( null != list1[0].Attributes ) {
								//	属性がある→属性値を検索する。
								int index = 0;
								string name = string.Empty;
								for( index = 0 ; index < list1[0].Attributes.Count ; ++index ) {
									name = list1[0].Attributes[index].Name;
									//if( 0 == string.Compare( name, "deskTop" ) ) {
									//	//	机上モード
									//	_instance.DeskTopMode = ( 0 == string.Compare( "1", list1[0].Attributes[index].Value ) ) ? true : false;
									//}
									if( 0 == string.Compare( name, "boot" ) ) {
										//	起動モードの確認
										if( 0 == string.Compare( "DESKTOP", list1[0].Attributes[index].Value ) ) {
											_instance.BootMode = BootModes.Desktop;
										} else if( 0 == string.Compare( "SIM", list1[0].Attributes[index].Value ) ) {
											_instance.BootMode = BootModes.Simulator;
										} else {
											_instance.BootMode = BootModes.Machine;
										}
									}
								}
							}
						}
						//	ログ設定情報
						XmlNodeList list2 = doc.SelectNodes( "Root/Apl/Log" );
						if( null == list2 ) {
							break;
						}
						if( 0 < list2.Count ) {
							int val;
							//	複数検出されたとしても上端の取得結果のみを解析する。
							//AidConvert cnv = new AidConvert();
							if( null != list2[0].Attributes ) {
								//	属性がある→属性値を検索する。
								int index = 0;
								string name = string.Empty;
								for( index = 0 ; index < list2[0].Attributes.Count ; ++index ) {
									name = list2[0].Attributes[index].Name;
									if( 0 == string.Compare( name, "lv" ) ) {
										//	ログ出力レベル
										if( 0 == string.Compare( "DEBUG", list2[0].Attributes[index].Value, StringComparison.OrdinalIgnoreCase ) ) {
											_instance.LogLevel = LogLevels.Debug;
										} else {
											_instance.LogLevel = LogLevels.Error;
										}
									} else if( 0 == string.Compare( name, "cnt" ) ) {
										//	出力ログ保持数
										if( true == int.TryParse( list2[0].Attributes[index].Value, out val ) ) {
											_instance.LogSaveCount = val;
										} else {
											_instance.LogSaveCount = 5;
										}
									}
								}
								initialized = true;
								break;
							}
						}
					} catch( XPathException e ) {
						System.Diagnostics.Debug.WriteLine( $"!!! ECNC3 INITIALIZE !!!,EXP,{e.Message},{e.StackTrace}" );
					} catch( Exception e ) {
						System.Diagnostics.Debug.WriteLine( $"!!! ECNC3 INITIALIZE !!!,EXP,{e.Message},{e.StackTrace}" );
					}
					break;
				}
				if( false == initialized ) {
					//	初期化情報を取得できなかったので初期設定で起動。
					_instance.LogLevel = LogLevels.Error;
					_instance.LogSaveCount = 5;
					_instance.BootMode = BootModes.Machine;
//					_instance.DeskTopMode = false;
				}
				//if( true == _instance.DeskTopMode ) {
				if( true == _instance.BootMode.HasFlag( BootModes.Desktop ) ) {
					//	机上モード用のデバッグ情報格納ディレクトリを作成する。
					string directoryDebug = Path.Combine( currentDirectory, "Debug" );
					if( false == Directory.Exists( directoryDebug ) ) {
						Directory.CreateDirectory( directoryDebug );
					}
				}
			}
			++_refCount;
			return _instance;
		}

         /// <summary>インスタンスの解放</summary>
        /// <remarks>
        /// インスタンスを解放します。
        /// 参照カウンタが0以下になって初めて実体の解放を行います。
        /// </remarks>
        public static void ReleaseInstance()
		{
			--_refCount;
			if( 0 >= _refCount ) {
				DeleteInstance();
			}
		}
		/// <summary>インスタンスの破棄</summary>
		/// <remarks>
		/// インスタンスの破棄を行います。
		/// IDisposable にしないのは、Disposeが必要なクラスと勘違いしないため。
		/// </remarks>
		public static void DeleteInstance()
		{
			//AidLog logs = new AidLog( "ToolingInnerCaches.DeleteInstance" );
			if( null != _instance ) {
				_instance = null;
			//	logs.Report( "Instance has been disposed." );
			//} else {
			//	logs.Report( "Instance had not been created." );
			}
		}
	}
}
