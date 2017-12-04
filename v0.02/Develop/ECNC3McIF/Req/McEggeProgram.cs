using ECNC3.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECNC3.Models.McIf
{
	/// <summary>
	/// 位置出し：処理実行
	/// </summary>
	public class McEggeProgram
	{
		//改行
		private string _csNewLine = "\r\n";

        /// <summary>
        ///  位置出し処理実行
        /// </summary>
        /// <param name="intMode"></param>
        /// <param name="decimalX"></param>
        /// <param name="decimalY"></param>
        /// <param name="decimalZ"></param>
        /// <param name="decimalH"></param>
        /// <param name="decimalI"></param>
		/// <param name="boolAngleCorrectionXorY">補正角度：false=X軸選択、true=Y軸選択</param>
        /// <returns></returns>
        public int EdgeSerchExec( int intMode,
							decimal decimalX,
							decimal decimalY,
							decimal decimalZ,
							decimal decimalH,
							decimal decimalI,
                            bool boolAngleCorrectionXorY
        )
        {
            string strText = "";
			//PrgCompileReturn CompRet;
			string strMsg = _csNewLine;//改行コード
			long Ret = 0;

			//加工プログラムコード組み立て
			//エラーメッセージ題名設定
			strMsg = strMsg + "Program String Make.";
			strText = EdgePrgMake( intMode, decimalX, decimalY, decimalZ, decimalH, decimalI, boolAngleCorrectionXorY);

			//戻り値エラー判断
			if( strText == "" ) {
				strMsg = strMsg + " --> NG" + _csNewLine;
				//エラー
				return EdgeSerchExec_Err();
			} else {
				strMsg = strMsg + " --> OK" + _csNewLine;
			}
			//ファイル保存処理
			//エラーメッセージ題名設定
			strMsg = strMsg + "Program File Save.";
			//処理実行
			string PathStrstrPRGPath = System.IO.Path.GetFullPath( @"Program" ) + "\\";
			Ret = File_Save( PathStrstrPRGPath, "EdgeSrchTmp.PRG", ref strText );
			//戻り値エラー判断
			if( Ret != 0 ) {
				strMsg = strMsg + " --> NG" + _csNewLine;
				//GoTo EdgeSerchExec_Err
			} else {
				strMsg = strMsg + " --> OK" + _csNewLine;
			}
			//プログラムコンパイル処理
			//エラーメッセージ題名設定
			strMsg = strMsg + "Program Compile.";
			//処理実行

			PrgCompileReturn CompRet = new PrgCompileReturn();
			
			Ret = DLLPrgCompile( PathStrstrPRGPath, "EdgeSrchTmp.PRG", ref CompRet);
			//戻り値エラー判断
			if( Ret != 0 ) {
				strMsg = strMsg + " --> NG" + _csNewLine;
				//	GoTo EdgeSerchExec_Err
			} else {
				strMsg = strMsg + " --> OK" + _csNewLine; ;
			}
			//モード切り替え→AUTO
			//エラーメッセージ題名設定
			strMsg = strMsg + "SPX Mode Change.";

			//OverRide変更フラグ True = 変更 / Flse = 変更不可
			bool bolMoveOverRideFlg_cen = false;
			bolMoveOverRideFlg_cen = true;   //OverRide変更フラグ初期化 SPXOverRide変化がタイマーの中なので、フラグでスイッチ True = 変更有効／False = 変更無効

			//アンサーステータス取得分岐
			//lngSPXOverrideManu_Cen = CLng( AnsSts.bytOverride );  //現在のOverRide(マニュアル時)の値マニュアルOverRide変数に待避
			//lngSPXOverrideAuto_Cen = CLng( AnsSts.bytOverride );  //現在のOverRide(オート時)の値オートOverRide変数に待避(Auto Manu共に同じ値だった時のため)

			//処理実行  '端面位置だしAUTOへ移行
			//Ret = DLLModeChg( SPXMODE_AUTO );
			if( Ret != 0 ) {
				strMsg = strMsg + " --> NG" + _csNewLine;
				//GoTo EdgeSerchExec_Err
			} else {
				strMsg = strMsg + " --> OK" + _csNewLine;
			}

			//角度補正モード?
			if( intMode >= 20 && intMode <= 23 ) {
				//Yes-->
				//アンサーステータス取得分岐
				/*
								//現在の角度補正コード有効にセット
								if( DLLStsBitExt( AnsSts.lngStatus2, SPX_STS2_CORR_ANG ) = = false ) {
									//オプション設定実行(13:角度補正コード有効/無効)
									Ret = DLLOptionSet( 13, 1 );
				*/
			}
			//プログラムスタート(1:運転再開(通常起動))

			//エラーメッセージ題名設定
			strMsg = strMsg + "Program Start.";
            //処理実行
            //Ret = DLLPrgStart( 1, 0 );
            ProgramDownload(PathStrstrPRGPath + "EdgeSrchTmp.PRG");
            if ( Ret != 0 ) {
				strMsg = strMsg + "  --> NG" + _csNewLine;
				//GoTo EdgeSerchExec_Err
			}
			//ログ出力
			//AlmLogAUTOEvWrite( 0 );
			//戻り値セット(正常)
			return 0;
		}
        /// <summary>
        /// プログラムダウンロード
        /// </summary>
        /// <param name="path"></param>
        /// <param name="ret"></param>
        private void ProgramDownload(string path)
        {
            ResultCodes ret = ResultCodes.Success;
            using (McDatProgram mc = new McDatProgram())
            {
                mc.ProgramFilePath = path;
                mc.BlockNumber = 0;
                ret = mc.Write();
                //	実行結果の表示
               
                if (ResultCodes.Success != ret)
                {
                    //	エラー詳細を反映
                    if (ProgramCodeTypes.Main == mc.ErrorCodeType)
                    {
                        MessageBox.Show("/" + $"{ret}({(int)ret})/"
                                                        + $"Type={mc.ErrorCodeType}#{mc.ErrorCodeNumber}/"
                                                        + $"LINE={mc.ErrorLineNumber}");
                    }
                }
            }
        }
        /// <summary>
        /// 端面位置出し加工プログラム文字列生成
        /// </summary>
        /// <param name="intMode">
        ///モード番号一覧
        /// １：G22;+X端面位置出し(Z,H)
        /// ２：G23;-X端面位置出し(Z,H)
        /// ３：G24;+Y端面位置出し(Z,H)
        /// ４：G25;-Y端面位置出し(Z,H)
        /// ５：G26;-X,-Y送りコーナー位置出し(第一象限)(X,Y,Z,H)
        /// ６：G27;+X,-Y送りコーナー位置出し(第二象限)(X,Y,Z,H)
        /// ７：G28;+X,+Y送りコーナー位置出し(第三象限)(X,Y,Z,H)
        /// ８：G29;-X,+Y送りコーナー位置出し(第四象限)(X,Y,Z,H)
        /// ９：第一象限内コーナー位置出し(G28)
        /// 10：第二象限内コーナー位置出し(G29)
        /// 11：第三象限内コーナー位置出し(G26)
        /// 12：第四象限内コーナー位置出し(G27)
        /// 13：G30;内径芯だし(Z,H,I)
        /// 14：G31;外径芯だし(Z,H,I)
        /// 15：G32;X軸外径芯だし(Z,H)
        /// 16：G33;Y軸外径芯だし(Z,H)
        /// 17：G32;X軸内径外径芯だし(Z,H)
        /// 18：G33;Y軸内径芯だし(Z,H)
        /// 19：G34;W軸端面位置出し(X,H)
        /// 19：G34;W軸端面位置出し(Z,H)
        /// 20：G46;補正角度測定(+送り端面位置出し)
        /// 21：G47;補正角度測定(-送り端面位置出し)
        /// 22：G48;補正角度測定(内径芯出し)
        /// 23：G49;補正角度測定(外形芯出し)
        /// </param>
        /// <param name="decimalX"></param>
        /// <param name="decimalY"></param>
        /// <param name="decimalZ"></param>
        /// <param name="decimalH"></param>
        /// <param name="decimalI"></param>
		/// <param name="boolAngleCorrectionXorY">補正角度：false=X軸選択、true=Y軸選択</param>
        /// <returns></returns>
        private string EdgePrgMake( int intMode,
									decimal decimalX,
									decimal decimalY,
									decimal decimalZ,
									decimal decimalH,
									decimal decimalI,
                                    bool boolAngleCorrectionXorY
        )
		{
			string strProg = "";
			string[] strPrm = new string[5];// ( 0 To 4) As String
			decimal decimalXTmp = 0.0m;
			decimal decimalYTmp = 0.0m;
			decimal decimalZTmp = 0.0m;
			decimal decimalHTmp = 0.0m;
			decimal decimalITmp = 0.0m;
			//引数を絶対値に変換
			decimalXTmp = Math.Abs( decimalX );
			decimalYTmp = Math.Abs( decimalY );
			decimalZTmp = Math.Abs( decimalZ );
			decimalHTmp = Math.Abs( decimalH );
			decimalITmp = Math.Abs( decimalI );
			//Z指定値の符号を変換
			decimalZTmp = decimalZTmp * -1;

			//各位置出しモードでの符号指定
			//及び位置出しモード番号チェック
			switch( intMode ) {
				case 1: //G22;+X端面位置出し(Z,H)
				case 2: //G23;-X端面位置出し(Z,H)
				case 3: //G24;+Y端面位置出し(Z,H)
				case 4: //G25;-Y端面位置出し(Z,H)
				case 5: //G26;-X,-Y送りコーナー位置出し(第一現象)(X,Y,Z,H)
					decimalXTmp = decimalXTmp * -1;
					decimalYTmp = decimalYTmp * -1;
					break;
				case 6://G27;+X,-Y送りコーナー位置出し(第二現象)(X,Y,Z,H)
					decimalYTmp = decimalYTmp * -1;
					break;
				case 7://G28;+X,+Y送りコーナー位置出し(第三現象)(X,Y,Z,H)
				case 8://G29;-X,+Y送りコーナー位置出し(第四現象)(X,Y,Z,H)
					decimalXTmp = decimalXTmp * -1;
					break;
				case 9://第一象限内コーナー位置出し(G28)
					decimalXTmp = decimalXTmp * -1;
					decimalYTmp = decimalYTmp * -1;
					break;
				case 10://第二象限内コーナー位置出し(G29)
					decimalYTmp = decimalYTmp * -1;
					break;
				case 11://第三象限内コーナー位置出し(G26)
				case 12://第四象限内コーナー位置出し(G27)
					decimalXTmp = decimalXTmp * -1;
					break;
				case 13://G30;内径芯だし(Z,H,I)
				case 14://G31;外径芯だし(Z,H,I)
				case 15://G32;X軸外径芯だし(Z,H)
				case 16://G33;Y軸外径芯だし(Z,H)
				case 17://G32;X軸内径芯だし(Z,H)
				case 18://G33;Y軸内径芯だし(Z,H)
				case 19://G34;W軸端面位置出し(Z,H)
				case 20://G46;補正角度測定(+送り端面位置出し
				case 21://G47;補正角度測定(-送り端面位置出し)
				case 22://G48;補正角度測定(内径芯出し)
				case 23://G49;補正角度測定(外形芯出し)
					decimalXTmp = decimalX;
					decimalYTmp = decimalY;
					break;
				default://その他
						//モード番号違反 -->コードを出力せず終了
					return "";
			}
			//プログラム組み立て
			//全指定値文字列変換
			strPrm[0] = decimalXTmp.ToString();
			strPrm[1] = decimalYTmp.ToString();
			strPrm[2] = decimalZTmp.ToString();
			strPrm[3] = decimalHTmp.ToString();
			strPrm[4] = decimalITmp.ToString();
			//整数の場合、右端に小数点を付加
			for( int count = 0 ; count < 4 ; count++ ) {
				if( strPrm[count].IndexOf( "." ) < 0 ) {
					strPrm[count] = strPrm[count] + ".";
				}
			}
			//コード組み立て
			switch( intMode ) {
				case 1://G22;+X端面位置出し(Z,H)
					strProg = "G22";
					strProg = strProg + "Z" + strPrm[2] + "H" + strPrm[3];
					break;
				case 2://G23;-X端面位置出し(Z,H)
					strProg = "G23";
					strProg = strProg + "Z" + strPrm[2] + "H" + strPrm[3];
					break;
				case 3://G24;+Y端面位置出し(Z,H)
					strProg = "G24";
					strProg = strProg + "Z" + strPrm[2] + "H" + strPrm[3];
					break;
				case 4://'G25;-Y端面位置出し(Z,H)
					strProg = "G25";
					strProg = strProg + "Z" + strPrm[2] + "H" + strPrm[3];
					break;
				case 5://G26;-X,-Y送りコーナー位置出し(第一現象)(X,Y,Z,H)
				case 11://第三象限 内コーナー位置出し
					strProg = "G26";
					strProg = strProg + "X" + strPrm[0] + "Y" + strPrm[1];
					strProg = strProg + "Z" + strPrm[2] + "H" + strPrm[3];
					break;
				case 6://G27;+X,-Y送りコーナー位置出し(第二現象)(X,Y,Z,H)
				case 12://第四象限 内コーナー位置出し
					strProg = "G27";
					strProg = strProg + "X" + strPrm[0] + "Y" + strPrm[1];
					strProg = strProg + "Z" + strPrm[2] + "H" + strPrm[3];
					break;
				case 7://G28;+X,+Y送りコーナー位置出し(第三現象)(X,Y,Z,H)
				case 9://第一象限 内コーナー位置出し
					strProg = "G28";
					strProg = strProg + "X" + strPrm[0] + "Y" + strPrm[1];
					strProg = strProg + "Z" + strPrm[2] + "H" + strPrm[3];
					break;
				case 8: //G29;-X,+Y送りコーナー位置出し(第四現象)(X,Y,Z,H)
				case 10://第二象限 内コーナー位置出し
					strProg = "G29";
					strProg = strProg + "X" + strPrm[0] + "Y" + strPrm[1];
					strProg = strProg + "Z" + strPrm[2] + "H" + strPrm[3];
					break;
				case 13://G30;内径芯だし(Z,H,I)
					strProg = "G30";
					strProg = strProg + "Z" + strPrm[2] + "H" + strPrm[3] + "I" + strPrm[4];
					break;
				case 14://G31;外径芯だし(Z,H,I)
					strProg = "G31";
					strProg = strProg + "Z" + strPrm[2] + "H" + strPrm[3] + "I" + strPrm[4];
					break;
				case 15://G32;X軸外径芯だし(Z,H)
					strProg = "G21H0" + _csNewLine;
					strProg = strProg + "G32";
					strProg = strProg + "Z" + strPrm[2] + "H" + strPrm[3];
					break;
				case 16://G33;Y軸外径芯だし(Z,H)
					strProg = "G21H0" + _csNewLine;
					strProg = strProg + "G33";
					strProg = strProg + "Z" + strPrm[2] + "H" + strPrm[3];
					break;
				case 17://G32;X軸内径芯だし(Z,H)
					strProg = "G21H1" + _csNewLine;
					strProg = strProg + "G32";
					strProg = strProg + "Z" + strPrm[2] + "H" + strPrm[3];
					break;
				case 18://'G33;Y軸内径芯だし(Z,H
					strProg = "G21H1" + _csNewLine;
					strProg = strProg + "G33";
					strProg = strProg + "Z" + strPrm[2] + "H" + strPrm[3];
					break;
				case 19://G34;W軸端面位置出し(Z,H) 
					strProg = "G34";
					strProg = strProg + "Z" + strPrm[2] + "H" + strPrm[3];
					break;
				case 20://G46;補正角度測定(+送り端面位置出し)
					strProg = "G46";
					if(boolAngleCorrectionXorY==false) strProg = strProg + "X" + strPrm[0];
					else                               strProg = strProg + "Y" + strPrm[1];
					strProg = strProg + "Z" + strPrm[2];
					strProg = strProg + "H" + strPrm[3];
					break;

				case 21://G47;補正角度測定(-送り端面位置出し)
					strProg = "G47";
                    if (boolAngleCorrectionXorY == false) strProg = strProg + "X" + strPrm[0];
                    else                                  strProg = strProg + "Y" + strPrm[1];
                    strProg = strProg + "Z" + strPrm[2];
					strProg = strProg + "H" + strPrm[3];
					break;

				case 22://G48;補正角度測定(内径芯出し)
					strProg = "G48";
                    if (boolAngleCorrectionXorY == false) strProg = strProg + "X" + strPrm[0];
                    else                                  strProg = strProg + "Y" + strPrm[1];
                    strProg = strProg + "Z" + strPrm[2];
					strProg = strProg + "H" + strPrm[3] + "I" + strPrm[4];
					break;

				case 23://G49;補正角度測定(外形芯出し)
					strProg = "G49";
                    if (boolAngleCorrectionXorY == false) strProg = strProg + "X" + strPrm[0];
                    else                                  strProg = strProg + "Y" + strPrm[1];
                    strProg = strProg + "Z" + strPrm[2];
					strProg = strProg + "H" + strPrm[3] + "I" + strPrm[4];
					break;
			}
			return strProg;
		}

		/// <summary>
		/// エラー処理
		/// </summary>
		/// <returns></returns>
		private int EdgeSerchExec_Err()
		{
			/*			//"端面検出実行処理でエラーが発生しました。" + _csNewLine + "処理を続行できません。", vbCritical)
						InfoMsgShow( 245, vbOKOnly, strMsg );
						//アラーム発生中チェック
						//アンサーステータス取得分岐
						//アラーム発生中チェック
						if( DLLStsBitExt( AnsSts.lngStatus, SPX_STS_ALARM ) ) {
							//SPXリセット発行
							Ret = DLLReset();
						}
					//マニュアルモードへ変更
					Ret = DLLModeChg( SPXMODE_MANUAL);*/
			//戻り値セット(エラー)
			return -1;
		}
		/// <summary>
		/// ファイル：保存
		/// </summary>
		/// <param name="strPath"></param>
		/// <param name="strFileName"></param>
		/// <param name="strText"></param>
		/// <returns></returns>
		public long File_Save( string strPath, string strFileName, ref string strText )
		{
			try {
				//入力テキスト、ファイル出力処理
				//引数取得
				string strSavePath = strPath;
				string strSaveName = strFileName;

				//Textデータ最終文字確認/整形
				if( strText.Length > 1 ) {
					//データ2キャラクタ以上 & 最右キャラクタ=EOF(1Ah) ?
					string tmpString = strText.Substring( strText.Length - 2, 2 );
					if( tmpString == "\x1a" ) {
						//最後のEOF(1Ah)削除
						strText = strText.Remove( '\x1a' );
					}
				}
				if( strText.Length > 1 ) {
					//データ2キャラクタ以上 & 最右キャラクタ=CR(0Dh)+LF(0Ah)以外 ?
					string tmpString = strText.Substring( strText.Length - 2, 2 );
					if( tmpString != _csNewLine ) {
						//最後にCR(0Dh)+LF(0Ah)付加
						strText = strText + _csNewLine;
                        //20170417 Hachino ADD プログラム番号指定行追加
                        strText = "PNO1" + _csNewLine + strText;

                    }
				}
				//ファイル：書き込み：UTF8　※シュミレータで読めない
//				System.IO.StreamWriter writer = new System.IO.StreamWriter( strSavePath + strSaveName, true, Encoding.UTF8 );//行追加はfalseをtrue
				System.IO.StreamWriter writer = new System.IO.StreamWriter( strSavePath + strSaveName, false, Encoding.ASCII );//行追加はfalseをtrue
				writer.WriteLine( strText );
				writer.Close();
				return 0;//成功
			} catch( Exception ex ) {//エラー処理
				MessageBox.Show( ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
			}
			//戻り値=エラー セット
			return -1;
		}

		//プログラム・コンパイル戻り値・構造体
		private struct PrgCompileReturn
		{
			public int lngErrCode;    //発生エラーコード
										//2:プログラムバッファオーバーフロー
										//3:テキストプログラムフォーマットエラー
										//4:プログラム変換演算エラー
										//5:作業メモリオーバーフロー
			public int lngErrLineNo;    //エラー発生行番号
										//D00~D15:エラー発生行番号
										//D16~D23:エラー発生G/Mコード
										//D24~D25:G/Mコードフラグ
										//00::ユーザプログラム
										//01::ユーザ定義Gコードプログラム
										//02::ユーザ定義Mコードプログラム
			public int lngErrGMCode;
			public int lngErrGMFlag;
			public int lngPrgSetErr;    //動作プログラム書込エラー戻り値
		}

		//メッセージ定義：VB演算命令定義コンパイル結果データ構造体
		private struct PrgCompileVBReturn {
			int lngErrLineNo;        //エラー発生行番号
			int lngErrCode;          //発生エラーコード
		}
		/// <summary>
		/// 加工プログラムコンパイルおよびセット
		/// </summary>
		/// <param name="strPathName">コンパイルファイルパス名</param>
		/// <param name="strFileName">コンパイルファイル名</param>
		/// <param name="PrgCompileRet">実行結果構造体</param>
		/// <returns>成功-1以外、失敗=-1</returns>
		/// <para>
		///		加工プログラムのコンパイル要求を行う。
		///		同時にSPXへ加工プログラムコンパイル済みバイナリデータの
		///		転送も行う。
		/// </para>
		private int DLLPrgCompile( string strPathName, string strFileName, ref PrgCompileReturn PrgCompileRet )
		{
			try {
				string strFile = "";
				PrgCompileVBReturn VBCompRet;
				int Ret = 0;
				//パス名取得
				strFile = strPathName;
				//パス設定終端"\"有無確認
				if( strFile.IndexOf( "\\" ) == -1){
					// 無い場合は追加する。
					strFile = strFile + "\\";
				}
				//ファイル名取得、フルパス作成
				strFile = strFile + strFileName;
				//コンパイル実行
				//Ret = CNC_PRGCOMPILE( hCom, strFile, PrgCompileRet );
				//if( Ret != 0 ) {
					//戻り値セット
					//return Ret;
				//}
				//VB演算コンパイル
				//Ret = VBCalc_Compile( strPathName, strFileName, VBCompRet )
				//if( Ret != 0 ) {
//					PrgCompileRet.lngErrCode = VBCompRet.lngErrCode;
//					PrgCompileRet.lngErrLineNo = VBCompRet.lngErrLineNo;
				//}
				//戻り値セット
				return Ret;
			} catch( Exception exp ) {
				System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace( exp, true ); //第二引数のtrueがファイルや行番号をキャプチャするため必要
				string mes = exp.Message + "\r\n";


				//foreach( var frame in trace.GetFrames )
				//{
				//	Console.WriteLine( frame.GetFileName() );     //filename
				//	Console.WriteLine( frame.GetFileLineNumber() );   //line number
				//	Console.WriteLine( frame.GetFileColumnNuber() );  //column number

				//	//mes += "ファイル名：" + frame.GetFileName() + "\r\n" ;     //filename
				//	//mes += "行番号：" + frame.GetFileLineNumber() + "\r\n";  //line number
				//	//mes += "列位置：" + frame.GetFileColumnNuber() + "\r\n";	//column number
				//}
				MessageBox.Show( mes, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
			}
			return -1;
		}
	}
}
