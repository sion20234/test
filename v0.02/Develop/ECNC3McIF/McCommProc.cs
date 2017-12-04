using System;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices;
using ECNC3.Enumeration;
using ECNC3.Models.Common;
using static Rt64eccomapi;

namespace ECNC3.Models.McIf
{
	/// <summary>モーションコントロールインターフェース</summary>
	/// <remarks>
	/// モーションコントロールとのインターフェースを行うための基盤となるクラスです。
	/// アプリケーション起動時に初期化を、終了時に終了処理を実施し、
	/// 初期化を行ったクラスのインスタンスは起動中は維持し続け、終了処理においても同様のインスタンスにより終了処理を実施してください。
	/// </remarks>
	public class McCommProc : IDisposable
	{
		#region メンバ変数
		/// <summary>ECNC設定クラスの実体</summary>
		private ECNC3Settings _ecnc3Setting = null;
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		#endregion

		/// <summary>コンストラクタ</summary>
		public McCommProc()
		{
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
						if( null != _ecnc3Setting ) {
							Terminate();
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
        #region <進捗状態通知処理>
        public int _progressValue = 0;
        public int ProgressValue
        {
            get
            {
                return _progressValue;
            }
            set
            {
                _progressValue = value;
                UpdateProcessing();
            }
        }
        public delegate void NotifyUpdateProcessing();
        /// <summary>進捗値変更通知</summary>
        private NotifyUpdateProcessing _notifyUpdate = null;
        /// <summary>>進捗値取得</summary>
        public NotifyUpdateProcessing NotifyUpdate
        {
            get
            {
                return _notifyUpdate;
            }
            set
            {
                _notifyUpdate = value;
            }
        }
        public void UpdateProcessing()
        {
            _notifyUpdate?.Invoke();
        }
#endregion
        /// <summary>通信開始</summary>
        /// <returns>実行結果</returns>
        public ResultCodes Initialize()
		{
            ProgressValue = 0;
            //	通信の開始
            if ( null == _ecnc3Setting ) {
				_ecnc3Setting = new ECNC3Settings();
			}
            ProgressValue = 1;
            //	ログ出力初期化
            AidLog log0 = new AidLog( "McCommProc.Initialize" );
			{
				log0.Sure( $"################ ECNC3 UI APLICATION START! ({ _ecnc3Setting.BootMode} MODE) ###############" );
				log0.Initialize();
				using( FileAlarmLog mc = new FileAlarmLog() ) {
					mc.Write( new StructureAlarmLogItem() {
						Kind = AlarmLogKinds.AppStart,
					} );
				}
			}
            ProgressValue = 4;
            AidLog logs = new AidLog( "McCommProc.Initialize" );
			ResultCodes ret = ResultCodes.Success;
			while( true ) {
				//	RTMC64-EC通信の初期化
				if( false == _ecnc3Setting.WasMcInitialzed ) {
					//	モーション部の初期化が済んでいなければ接続処理を実行
					ret = McConnect();
					if( ResultCodes.Success != ret ) {
						break;
					}
				} else {
					logs.Debug( "Initialize has already executed." );
				}
                ProgressValue = 7;
                //	リセット(REQ_RESET)
                using ( McReqReset mc = new McReqReset() ) {
					ret = mc.Execute();
					if( ResultCodes.Success != ret ) {
						break;
					}
				}
                ProgressValue = 12;
                //	セッティングモードにする(REQ_MODECHG)
                using ( McReqModeChange mc = new McReqModeChange() ) {
					mc.TaskMode = McTaskModes.Setting;
					ret = mc.Execute();
					if( ResultCodes.Success != ret ) {
						break;
					}
				}
                ProgressValue = 16;
                using ( FileSettings fs = new FileSettings() ) {
					ret = fs.Read();
					if( false == fs.AttrBool( "Root/Motions/NotInit", "romSw" ) ) {
						//	ROMSWデータ転送(DAT_ROMSWITCH)
						using( McDatRomSwitch mc = new McDatRomSwitch() ) {
							ret = mc.Initialize();
							if( ResultCodes.Success != ret ) {
								break;
							}
						}
					}
                    ProgressValue = 18;
                    //	サーボパラメータ転送(DAT_PARAMETER)
                    if ( false == fs.AttrBool( "Root/Motions/NotInit", "svo" ) ) {
						using( McDatServoParameter mc = new McDatServoParameter() ) {
							ret = mc.Initialize();
							if( ResultCodes.Success != ret ) {
								break;
							}
						}
					}
                    ProgressValue = 21;
                    if ( false == fs.AttrBool( "Root/Motions/NotInit", "pErr" ) ) {
						//	ピッチエラー補正用パラメータ転送(DAT_PITCHERR)
						using( McDatPitchAdjust mc = new McDatPitchAdjust() ) {
							ret = mc.Initialize();
							if( ResultCodes.Success != ret ) {
								break;
							}
						}
					}
                    ProgressValue = 23;
                    //	SPX ROMバージョン情報取得(DAT_VERSION)
                    using ( McDatRomVersion mc = new McDatRomVersion() ) {
						ret = mc.Read();
						if( ResultCodes.Success != ret ) {
							break;
						}
					}
                    ProgressValue = 26;
                    //	初期化パラメータ転送(DAT_INITIALPRM)
                    using ( McDatInitialPrm mc = new McDatInitialPrm() ) {
						ret = mc.Initialize();
						if( ResultCodes.Success != ret ) {
							break;
						}
					}
                    ProgressValue = 28;
                    //	加工条件テーブル
                    using ( McDatProcessConditionTable mc = new McDatProcessConditionTable() ) {
						mc.Initialize();
						if( ResultCodes.Success != ret ) {
							break;
						}
					}
                    ProgressValue = 34;
                    //	手動モードにする(REQ_MODECHG)
                    using ( McReqModeChange mc = new McReqModeChange() ) {
						mc.TaskMode = McTaskModes.Manual;
						ret = mc.Execute();
						if( ResultCodes.Success != ret ) {
							break;
						}
					}
                    ProgressValue = 36;
                    //	オプショナルストップ有効／無効設定(REQ_OPTSTOP)
                    using ( McReqOptionalStopEnabled mc = new McReqOptionalStopEnabled() ) {
						ret = mc.Initalize( fs );
						if( ResultCodes.Success != ret ) {
							break;
						}
					}
                    ProgressValue = 37;
                    //	接触関知有効／無効設定(REQ_TOUCHSENSE)
                    using ( McReqTouchSensorEnabled mc = new McReqTouchSensorEnabled() ) {
						ret = mc.Initalize( fs );
						if( ResultCodes.Success != ret ) {
							break;
						}
					}
                    ProgressValue = 39;
                    //	Ｗ軸上限値設定(REQ_WTOPPOS)
                    using ( McReqWAxisUpperLimit mc = new McReqWAxisUpperLimit() ) {
						ret = mc.Initalize( fs );
						if( ResultCodes.Success != ret ) {
							break;
						}
					}
                    ProgressValue = 41;
                    //	加工条件番号変更設定(REQ_PNOSEL)
                    using ( McReqProcessConditionNumberSelect mc = new McReqProcessConditionNumberSelect() ) {
						ret = mc.Initalize( fs );
						if( ResultCodes.Success != ret ) {
							break;
						}
					}
                    ProgressValue = 46;
                    //	ドライラン有効／無効設定(REQ_DRYRUN)
                    using ( McReqDryRunEnabled mc = new McReqDryRunEnabled() ) {
						ret = mc.Initalize( fs );
						if( ResultCodes.Success != ret ) {
							break;
						}
					}
                    ProgressValue = 52;
                    //	電極交換有効／無効設定(REQ_ELCTDCHGEN)
                    using ( McReqAecByLifeEnabled mc = new McReqAecByLifeEnabled() ) {
						ret = mc.Initalize( fs );
						if( ResultCodes.Success != ret ) {
							break;
						}
					}
                    ProgressValue = 58;
                    //	相対測定点設定時軸移動有効／無効設定(REQ_INCREFSET_MOV)
                    using ( McReqIncrimentalReferenceAxisMoveEnable mc = new McReqIncrimentalReferenceAxisMoveEnable() ) {
						ret = mc.Initalize( fs );
						if( ResultCodes.Success != ret ) {
							break;
						}
					}
                    ProgressValue = 65;
                    //	パーティション設定(REQ_PARTITIONCHG)
                    using ( McReqPartitionChange mc = new McReqPartitionChange() ) {
						ret = mc.Initialize( fs );
						if( ResultCodes.Success != ret ) {
							break;
						}
					}
                    ProgressValue = 72;

                    //	仮想点／測定点設定(REQ_VIRPOSCHG_EX)
                    using ( McReqVirtualPositionChange mc = new McReqVirtualPositionChange() ) {
						ret = mc.Initialize();
						if( ResultCodes.Success != ret ) {
							break;
						}
					}
                    ProgressValue = 84;
                    //	ワーク原点設定(REQ_WORGPOSCHG)
                    using ( McReqWorkPositionChange mc = new McReqWorkPositionChange() ) {
						ret = mc.Initialize();
						if( ResultCodes.Success != ret ) {
							break;
						}
					}
                    ProgressValue = 91;
                    //	変換ライブラリー初期化
                    using ( McGcdInitData mc = new McGcdInitData() ) {
						ret = mc.Initialize( fs );
						if( ResultCodes.Success != ret ) {
							break;
						}
					}
                    ProgressValue = 98;
                    //MC内プログラム削除
                    using (McReqProgramDelete progDel = new McReqProgramDelete())
                    {
                        progDel.Execute();
                    }
                }
                ProgressValue = 100;
                return ResultCodes.Success;
			}
			return ret;
		}
		/// <summary>通信終了</summary>
		/// <returns>実行結果</returns>
		public ResultCodes Terminate()
		{
			AidLog logs = new AidLog( "McCommProc.Terminate" );
			try {
				if( null != _ecnc3Setting ) {
					//	アプリケーション終了前に設定ファイル類をバックアップ
					Backup();

					int retRt64 = 0;
					if( BootModes.Desktop == _ecnc3Setting.BootMode ) {
						retRt64 = Rt64eccomapiWrap.QuitCommProc( _ecnc3Setting.CommHandle );
						//if( Syncdef.E_OK != retRt64 ) {
							using( McCommBasic mc = new McCommBasic() ) {
								mc.ConvertReturnCode( retRt64, $@"QuitCommProc(Wrap)(hCom={_ecnc3Setting.CommHandle})" );
							}
						//}
					} else {
						retRt64 = Rt64eccomapi.QuitCommProc( _ecnc3Setting.CommHandle );
						//if( Syncdef.E_OK != retRt64 ) {
							using( McCommBasic mc = new McCommBasic() ) {
								mc.ConvertReturnCode( retRt64, $@"QuitCommProc(hCom={_ecnc3Setting.CommHandle})" );
                            logs.Sure("QuitCommProc(hCom={" + _ecnc3Setting.CommHandle.ToString() + "})");
							}
						//}
					}
					//	通信の切断
					_ecnc3Setting.SetMcCommHandle( -1 );
					_ecnc3Setting.Dispose();
					_ecnc3Setting = null;
				}
			} catch( Exception e ) {
				bool unexpected = true;
				if( ( e is DllNotFoundException ) ||
					( e is EntryPointNotFoundException ) ) {
					unexpected = false;   //	想定内の例外。
				}
				return logs.Exception( e, unexpected );
			} finally {
				logs.Sure( "############################### ECNC3 UI APLICATION END! ###################################" );
				using( FileAlarmLog mc = new FileAlarmLog() ) {
					mc.Write( new StructureAlarmLogItem() {
						Kind = AlarmLogKinds.AppEnd,
					} );
				}
			}
			return ResultCodes.Success;
		}

		/// <summary>パラメータ読み込み</summary>
		/// <param name="data">取得情報の格納構造体</param>
		private void ReadFile( out SXDEF data )
		{
			data = SXDEF.Init();
			data.fComType = Rt64eccomapi.COMSHAREMEM;
			data.fShare = 1;
			data.nSize = Marshal.SizeOf( data );
			using( FileSettings fs = new FileSettings() ) {
				fs.Read();
				// 通信ロギングフラグ
				short result = 0x00;
				const string ElementName = "/Root/Motions/Log";
				if( 0 != fs.AttrValue( ElementName, "dsbl" ) ) {
					result |= 0x0001;
				}
				if( 0 != fs.AttrValue( ElementName, "inf" ) ) {
					result |= 0x0002;
				}
				if( 0 != fs.AttrValue( ElementName, "infSandE" ) ) {
					result |= 0x0004;
				}
				if( 0 != fs.AttrValue( ElementName, "infSnd" ) ) {
					result |= 0x0008;
				}
				if( 0 != fs.AttrValue( ElementName, "infRcv" ) ) {
					result |= 0x0010;
				}
				if( 0 != fs.AttrValue( ElementName, "infCmd" ) ) {
					result |= 0x0020;
				}
				if( 0 != fs.AttrValue( ElementName, "err" ) ) {
					result |= 0x0040;
				}
				if( 0 != fs.AttrValue( ElementName, "errXfr" ) ) {
					result |= 0x0080;
				}
				if( 0 != fs.AttrValue( ElementName, "retry" ) ) {
					result |= 0x0100;
				}
				data.fLogging = result;
				// 通信ロギングファイル名
				data.pLogFile = fs.AttrText( "/Root/Motions/Log", "fileNam" );
				data.pNodeName = fs.AttrText( "/Root/Motions/Node", "nam" );
			}
		}
		/// <summary>MC接続</summary>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// モーションコントローラへの接続を行います。
		/// </remarks>
		private ResultCodes McConnect()
		{
			AidLog logs = new AidLog( "McCommProc.McConnect" );
			Restore();
			int retryCount = 0;		//	ロード失敗時のリトライ回数
			int retryDelayTime = 0; //	ロード失敗時のリトライ待機時間(ms)
			int delay1st = 0;       //	一回目の呼び出しのディレイ
			int delay2nd = 0;       //	二回目の呼び出しのディレイ
			SXDEF data;
			ReadFile( out data );
			//	リトライパラメータを取得
			using( FileSettings fs = new FileSettings() ) {
				fs.Read();
				const string ElementName = "/Root/Motions/Boot";
				delay1st = fs.AttrValue( ElementName, "dly1st" );
				delay2nd = fs.AttrValue( ElementName, "dly2nd" );
				retryCount = fs.AttrValue( ElementName, "retry" );
				retryDelayTime = fs.AttrValue( ElementName, "span" );
				if( ( 0 == retryCount ) && ( 0 == retryDelayTime ) ) {
					//	パラメータが両方0の場合は、初期値を設定する。
					retryCount = 10;
					retryDelayTime = 3000;
				} else {
					if( 0 > delay1st ) {
						delay1st = 0;
					}
					if( 3 > retryCount ) {
						retryCount = 3;
					}
					if( 1000 > retryDelayTime ) {
						retryDelayTime = 1000;
					}
				}
				logs.Debug( $"RetryDelayTime={retryDelayTime}(1st={delay1st},2nd={delay2nd}),RetryCount={retryCount}" );
			}
			int commHandle = -1;
			int ret = 0;
			logs.Sure( $"InitCommProc(NodeName={data.pNodeName},Flag=0x{data.fLogging:x},LogFile={data.pLogFile},Size={data.nSize})" );
			if( 0 < delay1st ) {
				System.Threading.Thread.Sleep( delay1st );
			}
			try {
				McCommBasic.TechnoMethods method = McCommBasic.TechnoMethods.InitCommProc;
				int retry = 0;
				for( retry = 0 ; retry < retryCount ; ++retry ) {
					if( 0 < retry ) {
						System.Threading.Thread.Sleep( ( ( 2 > retry ) && ( 0 < delay2nd ) ) ? delay2nd : retryDelayTime );
						logs.Error( string.Format( CultureInfo.InvariantCulture, "Retry ({0}/{1}).", retry, retryCount ) );
					}
					if( BootModes.Desktop == _ecnc3Setting.BootMode ) {
						ret = Rt64eccomapiWrap.InitCommProc( ref data, ref commHandle );
					} else {
						ret = Rt64eccomapi.InitCommProc( ref data, ref commHandle );
					}
					if( Syncdef.E_OK != ret ) {
						if( retry > retryCount ) {
							using( McCommBasic mc = new McCommBasic() ) {
								if( BootModes.Desktop == _ecnc3Setting.BootMode ) {
									return mc.CheckResultDebug( method, ret );
								}
								return mc.CheckResultTechno( method, ret );
							}
						}
						continue;
					}
					break;
				}
			} catch( Exception e ) {
				bool unexpected = true;
				if( ( e is DllNotFoundException ) ||
					( e is EntryPointNotFoundException ) ) {
					unexpected = false;   //	想定内の例外。
				}
				return logs.Exception( e, unexpected );
			}
			_ecnc3Setting.SetMcCommHandle( commHandle );
			logs.Sure( $"Comm.handle={commHandle}" );
			return ResultCodes.Success;
		}
		/// <summary>バックアップ</summary>
		/// <param name="backupFilePath">
		///		<list type="bullet" >
		///			<item>null=アプリケーションによるデフォルト実行。</item>
		///			<item>他=バックアップファイルパス</item>
		///		</list>
		/// </param>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// アプリケーションのパラメータのバックアップを実施します。
		/// </remarks>
		public ResultCodes Backup( string backupFilePath = null )
		{
			AidLog logs = new AidLog( "McCommProc.Backup" );
			using( ECNC3Settings us = new ECNC3Settings() ) {
				//	Tempディレクトリを作成
				string tempAppDir = @"Temp";
				if( false == File.Exists( tempAppDir ) ) {
					Directory.CreateDirectory( tempAppDir );
				}
				int saveCount = 0;
				string backupDirectory = @"Backup";
				string tempName = DateTime.Now.ToString( "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture );
				string tempDir = Path.Combine( tempAppDir, tempName );

				bool isApplicationTerminate = false;
				if( true == string.IsNullOrEmpty( backupFilePath ) ) {
					isApplicationTerminate = true;
					using( FileSettings fs = new FileSettings() ) {
						fs.Read();
						//	バックアップパス
						backupDirectory = fs.AttrText( "Root/Apl/Bkup", "path" );
						if( true == string.IsNullOrEmpty( backupDirectory ) ) {
							backupDirectory = @"Backup";
						}
						//	バックアップ数
						saveCount = fs.AttrValue( "Root/Apl/Bkup", "cnt" );
						if( 1 > saveCount ) {
							saveCount = 1;
						}
					}
					//	アプリケーション終了によるバックアップであれば、自動生成したファイル名を使用する。
					backupFilePath = Path.Combine( backupDirectory, $"{tempName}.zip" );
				}
				ResultCodes ret = ResultCodes.Success;
				//	各ファイルのバックアップを実行
				AidAssembly<IEcnc3Backup> ifs = new AidAssembly<IEcnc3Backup>();
				foreach( Type type in ifs.Interfaces ) {
					IEcnc3Backup ifc = Activator.CreateInstance( type ) as IEcnc3Backup;
					if( null != ifc ) {
						//	Tempディレクトリへ一時的にコピー
						ret = ifc.Backup( tempDir );
						if( ResultCodes.Success == ret ) {
							logs.Sure( $"BACKUP {ifc.Name}...{ret}" );
						} else {
							logs.Sure( $"BACKUP {ifc.Name}...{ret}" );
						}
					}
				}
				//	ファイルを圧縮
				AidZip zip = new AidZip();
				if( 0 != zip.Create( tempDir, backupFilePath ) ) {
					return ResultCodes.FailToWriteFile;
				}
				if( true == isApplicationTerminate ) {
					//	バックアップファイルの既定をそってファイルの削除を行う。
					FileAccessCommon fa = new FileAccessCommon();
					fa.BackupStandBy( backupDirectory, saveCount, @"??????????????.zip" );
				}
			}
			return ResultCodes.Success;
		}
		/// <summary>リストア</summary>
		/// <param name="sourceDirectory">復元元情報の格納されたディレクトリパス</param>
		/// <returns>実行結果</returns>
		private ResultCodes Restore( string sourceDirectory = null )
		{
			AidLog logs = new AidLog( "McCommProc.Restore" );
			ResultCodes ret = ResultCodes.Success;
			if( true == string.IsNullOrEmpty( sourceDirectory ) ) {
				sourceDirectory = "Restore";
			}
			if( false == Directory.Exists( sourceDirectory ) ) {
				return ResultCodes.Success;
			}
			//	リストア処理の実行。
			//	展開されているファイル名に該当があれば、現在値をバックアップした上で事前に上書きコピーを実施する。
			Backup( @"Backup\BEFORERESTORE.zip" );
			//	各ファイルのリストアを実行
			AidAssembly<IEcnc3Backup> ifs = new AidAssembly<IEcnc3Backup>();
			foreach( Type type in ifs.Interfaces ) {
				IEcnc3Backup ifc = Activator.CreateInstance( type ) as IEcnc3Backup;
				if( null != ifc ) {
					ret = ifc.Restore( sourceDirectory );
					if( ResultCodes.Success != ret ) {
						break;
					}
					logs.Sure( $"RESTORE... {ifc.Name}." );
				}
			}
			if( ResultCodes.Success == ret ) {
				//	無事に処理が完了した場合は、デフォルトのディレクトリである場合に限り、ディレクトリごと削除する。
				if( 0 == string.Compare( "Restore", sourceDirectory, true ) ) {
					DirectoryInfo di = new DirectoryInfo( sourceDirectory );
					di.Delete( true );
				}
			}
			return ret;
		}
	}
}
