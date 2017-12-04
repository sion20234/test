///////////////////////////////////////////////////////////////////////////////////////////
//
// (1) システム名称 : ELENIX ECNCⅢ
// (2) ファイル名   : MainForm.cs
// (3) 概要         : UI/MC動作モジュールアップデートモジュール
// (4) 作成日       : 2017.04.07
// (5) 作成者       : 八野
// (6) 特記事項     : VisualStudio2015 C#
// (7) 更新履歴     : 2017.04.07：柏原ひろむ
//
///////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Windows.Forms;
using ECNC3.Models;//FileVersionInfo用
using System.IO;//

/// <summary>
/// UI/MC動作モジュールアップデート
/// </summary>
namespace ECNC3Updater
{
	/// <summary>
	/// メインフォーム
	/// </summary>
	public partial class MainForm : Form
	{
		/// <summary>
		/// デバックフラグ　　　※自動設定なので設定不要
		//　リリース時「ECNC3.exe」と「ECNC3Updater.exe」は同じフォルダなので、異なる場合、デバック用(_debugFlg = true)とする。
		/// </summary>
		//private bool _debugFlg = false;//true=デバック：実行時パスに"ECNC3Updater"が含まれる場合、デバックとして実行
									   /// <summary>
									   /// バックアップフラグ　※手動設定なのでtrue/falseどちらか設定します。
									   /// </summary>
		private bool _backUpFlg = true;//true=バックアップ実行：ファイル名＋[.bak]で同一フォルダに作成
		//ログ
		private ECNC3.Models.Common.ECNC3UpdateLog _logOut= new ECNC3.Models.Common.ECNC3UpdateLog();

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public MainForm()
		{
			InitializeComponent();
			//自分自身の実行ファイルのパスを取得する
			string appPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //"bin"が有ればデバック環境：C:\開発\ECNC3Server\ECNC3\v0.02\Develop\ECNC3\bin\Debug\ECNC3.exe
            if (appPath.IndexOf("bin") > -1)
            {
                //if( appPath.IndexOf( "ECNC3Updater" ) > -1 ) {//
                //"ECNC3Updater"が含まれる場合、
                //実機環境では「ECNC3.EXE」と「ECNC3Updater.exe」は同じフォルダなので、
                //異なる場合、デバック用(_debugFlg = true)とする。
                 //_debugFlg = true;
            }
		}
	    /// <summary>
	    /// フォーム：ロード
	    /// </summary>
	    /// <param name="sender"></param>
	    /// <param name="e"></param>
	    private void MainForm_Load( object sender, EventArgs e )
		{
			InitializeUpdater();
		}
		/// <summary>
		/// Ecnc3NeoVersion.xml：現バージョン/更新バージョンの番号：取得しフォームに表示
		/// </summary>
		private void InitializeUpdater()
		{
			string masterFolder = @FilePathInfo.MasterData;
            //20170411DEL↓↓パス設定ファイルで切り替える為
			//if( _debugFlg ) masterFolder = @"C:\開発\ECNC3Server\ECNC3\v0.02\ECNC3\Master\";//デバック時

			//"Ecnc3NeoVersion.xml"からバージョン情報取得
			using( FileVersionInfo verInfo = new FileVersionInfo( masterFolder ) ) {
				//\master\フォルダ
				verInfo.Read();
				_beforeVerLabel.Text = "Ver." + verInfo.UIReleaseVersion;
				_beforeVer = "Ver." + verInfo.UIReleaseVersion;
			}
            //using( FileVersionInfo verInfo = new FileVersionInfo( masterFolder + @"\Update\" ) ) {//1個下の階層
            using (FileVersionInfo verInfo = new FileVersionInfo(@FilePathInfo.NeoUpdateData))
            {
				//\master\Update\"フォルダ
				verInfo.Read();
                _afterVerLabel.Text = "Ver." + verInfo.UIReleaseVersion;
                _afterVer = "Ver." + verInfo.UIReleaseVersion;
            }
		}
		/// <summary>
		/// バージョン
		/// </summary>
		private string _beforeVer = "";	//前回バージョン
		private string _afterVer = "";	//今回バージョン
		/// <summary>
		/// 実行ボタン：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _excuteBt_Click( object sender, EventArgs e )
		{
			try {
				//ECNC3とテクノさんの3個のバージョンを比較
				//※パス確認    //ECNC3バージョン取得

				string masterFolder = @FilePathInfo.NeoUpdateData;
                //20170411DEL↓↓パス設定ファイルで切り替える為
                //if ( _debugFlg ) masterFolder = @"C:\開発\ECNC3Server\ECNC3\v0.02\ECNC3\Master\Update\";
				FileVersionInfo fvInfo = new FileVersionInfo( masterFolder );//EXE直下\Masterに有る
				fvInfo.Read();//データ読み込み
				string MCReleaseVersion = fvInfo.MCReleaseVersion;
				string RomSWModule = fvInfo.RomSWModule;
				string SettingPCModule = fvInfo.SettingPCModule;

				//テクノさんバージョン取得
				string technoFolder = @FilePathInfo.TechnoUpdateData;
                //20170411DEL↓↓パス設定ファイルで切り替える為
                //if ( _debugFlg ) technoFolder = @"C:\開発\ECNC3Server\ECNC3\v0.02\ECNC3\Update\Rt64EC\";
				FileTechnoVer ftVer = new FileTechnoVer( technoFolder );//EXEと同じフォルダに有る
				string[] strArray = new string[3];
				ftVer.GetFileTechnoVer( ref strArray );//データ読み込み
				string strVersion = strArray[0];
				string strR_ROMSW = strArray[1];
				string strR_SXDRV = strArray[2];
				//同じバージョンか比較
				if( MCReleaseVersion != strVersion ) {
					MessageBox.Show( "下記を同じバージョンにし再度実行して下さい。"
						+ "\r" + "ECNC3 : " + MCReleaseVersion + "\n" + "Techno : " + strVersion, "[Version]番号エラー",
						MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
					return;
				}
				if( RomSWModule != strR_ROMSW ) {
					MessageBox.Show( "下記を同じバージョンにし再度実行して下さい。"
						+ "\r" + "ECNC3 : " + RomSWModule + "\n" + "Techno : " + strR_ROMSW, "[R_ROMSW]番号エラー",
						MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
					return;
				}
				if( SettingPCModule != strR_SXDRV ) {
					MessageBox.Show( "下記を同じバージョンにし再度実行して下さい。"
						+ "\r" + "ECNC3 : " + SettingPCModule + "\n" + "Techno : " + strR_SXDRV, "[R_SXDRV]番号エラー",
						MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
					return;
				}

				//※パス確認	//ファイルパス
				string ecncUpdatePath = @FilePathInfo.NeoUpdateData;  //更新ファイルパス
				string ecncExePath = @FilePathInfo.ECNC3PATH;                  //実行環境パス
                //20170411DEL↓↓パス設定ファイルで切り替える為
                //if ( _debugFlg ) {//デバック時
				//	ecncUpdatePath = @"C:\開発\ECNC3Server\ECNC3\v0.02\ECNC3\Update\Ecnc3\";
				//	ecncExePath = @"C:\開発\ECNC3Server\ECNC3\v0.02\ECNC3\TestFolder_ecnc3\";
				//}

				string rt64EcUpdatePath = @FilePathInfo.TechnoUpdateData;  //更新ファイルパス
				string rt64EcExePath = @FilePathInfo.RT64ECPATH;                   //実行環境パス
                //20170411DEL↓↓パス設定ファイルで切り替える為
                //if ( _debugFlg ) {//デバック時
				//	rt64EcUpdatePath = @"C:\開発\ECNC3Server\ECNC3\v0.02\ECNC3\Update\Rt64EC\";
				//	rt64EcExePath = @"C:\開発\ECNC3Server\ECNC3\v0.02\ECNC3\TestFolder_Rt64EC\";
				//}

				//ECNC3.exeのプロセスが起動しているかを取得
				if( System.Diagnostics.Process.GetProcessesByName( "ECNC3" ).Length > 0 ) {
					DialogResult dResult = MessageBox.Show( "ECNC3が起動中です。\r\r" + "[OK] ECNC3を強制終了し続行\r\r[キャンセル] 中止 ", "注意", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation );

					if( dResult == DialogResult.Cancel ) {//キャンセルボタンが押された
						Application.Exit();
						//MessageBox.Show( "ECNC3アップデートを中止しました。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning );
						return;
					}
					//ECNC3.exeが起動しているなら、落とす
					System.Diagnostics.Process[] returnValue = System.Diagnostics.Process.GetProcessesByName( "ECNC3" );
					foreach( System.Diagnostics.Process dp in returnValue ) {
						//プロセスを強制的に終了させる
						dp.Kill();
						//プロセス終了が遅いのでSleepで間を持たせる
						System.Threading.Thread.Sleep( 2000 );
					}
				}
				_updateProcState.Value = 0;         //プログレスバー
				_updateState.Text = "Execute...";   //進捗率

				//\ECNC3のファイル名
				string[] ecncFileName = {
					"ECNC3.exe",
					"ECNC3Apis.dll",
					"ECNC3McIF.dll",
					"Master/Ecnc3NeoVersion.xml",//バージョン番号
					"Rt64eccomnt.dll",
					"Rt64ecgcnv.dll",
					"Rt64ectcnv.dll"
				};
				//\RT64ECのファイル名
				string[] rt64ECFileName = {
					"Rt64ecdrv.exe",
					"Rt64ecswset.exe",
					"Rt64ecDM_enc.exe",
					"Version.sys",//バージョン番号
					"Rt64eccomnt.dll",
					"Rt64ecgcnv.dll",
					"Rt64ectcnv.dll",
					"Rt64EC.rta"
				};
				//D:\ECNC3フォルダ
				int icntTotal = 0;
				int icnt = 0;
				//\Updateフォルダからファイルコピー
				_logOut.SetLogNewVersion(_afterVer);
				//ログ出力
				logOut( logType.First, "################ ECNC3 UPDATE START! ###############" );

				for( ; icnt < ecncFileName.Length ; ) {
					if( _backUpFlg ) {
						//バックアップを行う
						if( File.Exists( ecncExePath + ecncFileName[icnt] + ".BAK" ) ) {
							//既に存在する場合
							//リードオンリー属性をはずす
							File.SetAttributes( ecncExePath + ecncFileName[icnt] + ".BAK", FileAttributes.Normal );
							//元のファイルが有る場合「BAK」削除、無い場合残す
							if( File.Exists( ecncExePath + ecncFileName[icnt] ) ) {
								//削除
								File.Delete( ecncExePath + ecncFileName[icnt] + ".BAK" );
							}
						}
						if( File.Exists( ecncExePath + ecncFileName[icnt] ) ) {
							//元ファイルが有る場合
							//バックアップを作成
							File.Copy( ecncExePath + ecncFileName[icnt], ecncExePath + ecncFileName[icnt] + ".BAK" );
							//リードオンリー属性を付ける
							File.SetAttributes( ecncExePath + ecncFileName[icnt] + ".BAK",FileAttributes.ReadOnly );
						}
					}
					//対象ファイル
					if(File.Exists( ecncExePath + ecncFileName[icnt] ) ) {
						//既に存在する場合
						//リードオンリー属性をはずす
						File.SetAttributes( ecncExePath + ecncFileName[icnt], FileAttributes.Normal );
						//削除
						File.Delete( ecncExePath + ecncFileName[icnt] );
					}
					//デバック時フォルダが無ければ作成
					//if( _debugFlg ) CheckFolderAndCreate( ecncExePath );//デバックフォルダ

					//対象ファイルコピー
					if( ecncFileName[icnt]  == "Master/Ecnc3NeoVersion.xml" ) {
						ecncFileName[icnt] = ecncFileName[icnt].Replace( "Master/", "" );
						ecncExePath = ecncExePath + "Master/";
					}
					File.Copy( ecncUpdatePath + ecncFileName[icnt], ecncExePath + ecncFileName[icnt] );
					if( ecncFileName[icnt] == "Ecnc3NeoVersion.xml" ) {
						ecncFileName[icnt] = "Master/" + ecncFileName[icnt];
						ecncExePath = ecncExePath.Replace("Master/", "");
					}
					//リードオンリー属性を付ける
					File.SetAttributes( ecncExePath + ecncFileName[icnt], FileAttributes.ReadOnly );
					//ログ出力
					logOut(logType.Contents,  ecncFileName[icnt]+"…Success" );

					icnt++;
					icntTotal += 6;//この数値は100/15ファイルで1本＝6％として、15回で90％ですが最後は無理やり100％にしています；
					_updateState.Text = "Completed..." + icntTotal.ToString() + "%";//進捗率
					statusStrip1.Refresh();
					_updateProcState.Value = icntTotal;//プログレスバー
				}
				//C:\RT64ECフォルダ
				icnt = 0;
				for( ; icnt < rt64ECFileName.Length ; ) {
					//\Updateフォルダからファイルコピー
					if( _backUpFlg ) {
						//バックアップを行う
						if( File.Exists( rt64EcExePath + rt64ECFileName[icnt] + ".BAK" ) ) {
							//既に存在する場合
							//リードオンリー属性をはずす
							File.SetAttributes( rt64EcExePath + rt64ECFileName[icnt] + ".BAK", FileAttributes.Normal );
							//元のファイルが有る場合「BAK」削除、無い場合残す
							if( File.Exists( rt64EcExePath + rt64ECFileName[icnt] ) ) {
								//削除
								File.Delete( rt64EcExePath + rt64ECFileName[icnt] + ".BAK" );
							}
						}
						if( File.Exists( rt64EcExePath + rt64ECFileName[icnt] ) ) {   //元ファイルが有る場合
							//バックアップを作成
							File.Copy( rt64EcExePath + rt64ECFileName[icnt], rt64EcExePath + rt64ECFileName[icnt] + ".BAK" );
							//リードオンリー属性を付ける
							File.SetAttributes( rt64EcExePath + rt64ECFileName[icnt] + ".BAK", FileAttributes.ReadOnly );
						}
					}
					//デバック時フォルダが無ければ作成
					//if( _debugFlg ) CheckFolderAndCreate( rt64EcExePath );//デバックフォルダ

					//対象ファイルコピー
					if( File.Exists( rt64EcExePath + rt64ECFileName[icnt] ) ) {
						//既に存在する場合
						//リードオンリー属性をはずす
						File.SetAttributes( rt64EcExePath + rt64ECFileName[icnt], FileAttributes.Normal );
						//削除
						File.Delete( rt64EcExePath + rt64ECFileName[icnt] );
					}
					//コピー
					File.Copy( rt64EcUpdatePath + rt64ECFileName[icnt], rt64EcExePath + rt64ECFileName[icnt] );
					//リードオンリー属性を付ける
					File.SetAttributes( rt64EcExePath + rt64ECFileName[icnt], FileAttributes.ReadOnly );
					//ログ出力
					logOut( logType.Contents, rt64ECFileName[icnt] + "…Success" );

					icnt++;
					icntTotal += 6;//この数値は100/15ファイルで1本＝6％として、15回で90％ですが最後は無理やり100％にしています；
					_updateState.Text = "Completed..." + icntTotal.ToString() + "%";//進捗率
					statusStrip1.Refresh();
					_updateProcState.Value = icntTotal;//プログレスバー
				}
				//プログレスバー
				_updateProcState.Value = 100;
				//進捗率
				_updateState.Text = "Completed...100%";
				//ログ出力
				logOut( logType.End, "################ ECNC3 UPDATE FINISH! ###############" );
				//ECNC3.EXEを起動
				ECNC3ExeStart();

			} catch( Exception exp ) {//例外発生時
				//プログレスバー
				_updateProcState.Value = 0;
				//進捗率
				_updateState.Text = "Completed...0%";
				MessageBox.Show( exp.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
				return;
			}
			this.TopMost = true;//画面前面に表示
			MessageBox.Show( "アップデート完了", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information );
			Application.Exit();//このアプリを終了
		}
		/// <summary>
		/// ログ出力
		/// </summary>
		/// <param name="intLogType">ログタイプ</param>
		/// <param name="stringMsg">出力ログ</param>
		public void logOut(logType intLogType, string stringMsg )
		{
			string logString = "";
			switch( intLogType )
			{
				case logType.First://開始
					//yyyy/mm/dd hh:mm:ss.xxx,Old=Verx.xx,New=Verx.xx################ ECNC3 UPDATE START! ###############
					logString = "Old="+_beforeVer+","+"New="+ _afterVer + stringMsg;
					_logOut.Sure((int)logType.First, logString );
					break;
				case logType.Contents://各内容
					logString = stringMsg;
					_logOut.Sure((int)logType.Contents, logString );
					break;
				case logType.End://終了
					//yyyy/mm/dd hh:mm:ss.xxx,Old = Verx.xx,New = Verx.xx################ ECNC3 UPDATE FINISH! ###############
					logString = "Old=" + _beforeVer + "," + "New=" + _afterVer + stringMsg;
					_logOut.Sure((int)logType.End, logString );
					break;
				default:
					break;
			}
		}
		/// <summary>
		/// ログタイプ
		/// </summary>
		public enum logType {
			First=0,	//開始
			Contents=1,	//各内容
			End=2		//終了
		}

		/// <summary>
		/// フォルダが無ければ作成　※デバックフォルダ
		/// </summary>
		/// <param name="path">対象パス</param>
		/// <returns>0＝成功、1＝成功(作成しない)、-1＝失敗</returns>
		public int CheckFolderAndCreate( string path )
		{
			try {
				if( System.IO.Directory.Exists( path ) ) {//pathが既に存在している場合、戻る
					return 1;
				}
				//pathフォルダ新規作成
				System.IO.Directory.CreateDirectory( path );
				return 0;
			} catch( Exception exp ) {//例外発生時
				MessageBox.Show( path + "\r" + exp.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
				return -1;
			}
		}
		/// <summary>
		/// 閉じるボタン：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _closeBt_Click( object sender, EventArgs e )
		{
			//このアプリを終了
			Application.Exit();
			ECNC3ExeStart();//ECNC3.exe起動
		}
		/// <summary>
		/// フォーム閉じた時
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_FormClosed( object sender, FormClosedEventArgs e )
		{
			ECNC3ExeStart();//ECNC3.exe起動
		}
		/// <summary>
		/// ECNC3.exe起動
		/// </summary>
		private void ECNC3ExeStart()
		{
			//ECNC3.exeのプロセスが起動していれば、戻る
			if( System.Diagnostics.Process.GetProcessesByName( "ECNC3" ).Length != 0 ) return;
		
			string ecncExeName = "ECNC3.exe";//リリース時はD:\ECNC3\「ECNC3Updater.exe」と同じフォルダ
			try {
				//if( _debugFlg ) {//デバック時は、
				//	//C:\開発\ECNC3Server\ECNC3\v0.02\Develop\ECNC3Updater\bin\Debug\「ECNC3Updater.exe」
				//	//なのでフォルダを変更します。
				//	string debFolder = @FilePathInfo.ECNC3PATH;
				//	ecncExeName = debFolder + ecncExeName;
				//	//カレントフォルダをECNC3に変更する
				//	System.Environment.CurrentDirectory = debFolder;
				//}
				//ECNC3.exeのプロセス起動
				System.Diagnostics.Process proc = System.Diagnostics.Process.Start( ecncExeName );
			} catch( Exception exp ) {//例外発生時
				MessageBox.Show( ecncExeName + "\r" + exp.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
				return;
			}
		}
        /// <summary>
        /// ログボタン：クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogButton_Click(object sender, EventArgs e)
        {
			//ECNC3UpdateLogクラスを開く
			ECNC3.Models.Common.ECNC3UpdateLog log = new ECNC3.Models.Common.ECNC3UpdateLog();
			//ログ出力：フルパス+ファイル名
			string logPath =  log.OutputFilePath;//このファイルは1本のみ ※例、年2回×30年使用×ログ20行でも1200行
			//起動モジュール名
			string notepadExeName = "Notepad.exe";
            try {
				//プロセス起動
				System.Diagnostics.Process proc = System.Diagnostics.Process.Start(notepadExeName, logPath);
            }
            catch (Exception exp)
            {//例外発生時
                MessageBox.Show(notepadExeName + "\r" + exp.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }
    }
}
