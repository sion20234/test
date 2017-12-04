///////////////////////////////////////////////////////////////////////////////////////////
//
// (1) システム名称 : ELENIX ECNCⅢ
// (2) ファイル名   : FileTechnoVer.cs
// (3) 概要         : テクノさんバージョンの取得
// (4) 作成日       : 2017.04.04
// (5) 作成者       : 柏原ひろむ
// (6) 特記事項     : VisualStudio2015 C#
// (7) 更新履歴     :
//
///////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Text;
using System.Windows.Forms;//メッセージ用

namespace ECNC3.Models
{
	/// <summary>
	/// テクノさんバージョンの取得
	/// </summary>
	public partial class FileTechnoVer
	{

		string _VersionSys;
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FileTechnoVer( string path = "" )
		{
			_VersionSys = path + "Version.sys";
		}
		/// <summary>
		/// コンストラクタ：バージョン番号、R_ROM、R_SXDRV取得
		/// </summary>
		/// <param name="strVersion">バージョン番号</param>
		/// <param name="strR_ROMSW">R_ROMSW</param>
		/// <param name="strR_SXDRV">R_SXDRV</param>
		public void GetFileTechnoVer( ref string[] strArray )
		{
			try {
				//[Version.sys]ファイル：読み込み
				System.IO.StreamReader sReader = new System.IO.StreamReader( _VersionSys, Encoding.GetEncoding( "UTF-8" ) );
				string readText = sReader.ReadToEnd();
				sReader.Close();

				//Version、R_ROM、R_SXDRV：取得              
				string tmpStr;
				for( int iCount = 0 ; iCount < readText.Length ; iCount++ ) {
					tmpStr = getLine( iCount, readText );
					if( tmpStr.IndexOf( "Version" ) > -1 ) {
						tmpStr = tmpStr.Replace( "Version", "" );
						tmpStr = tmpStr.Trim();
						strArray[0] = tmpStr;//Version 
					} else if( tmpStr.IndexOf( "R_ROMSW" ) > -1 ) {
						GetFileTechnoVerItem( "R_ROMSW", ref strArray[1] );
					} else if( tmpStr.IndexOf( "R_SXDRV" ) > -1 ) {
						GetFileTechnoVerItem( "R_SXDRV", ref strArray[2] );
					}
				}
			} catch( Exception exp ) {
				MessageBox.Show( exp.Message, "Version.sys読み込みエラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
				//ECNC3.Views.ECNC3Exception.FileIOFilter( exp, this );
			}
		}
		/// <summary>
		/// 定数を指定して行最後の番号を取得
		/// </summary>
		/// <param name="itemName"></param>
		/// <param name="dataStrng"></param>
		public void GetFileTechnoVerItem( string itemName, ref string dataStrng )
		{
			try {
				//[Version.sys]ファイル：読み込み
				System.IO.StreamReader sReader = new System.IO.StreamReader( _VersionSys, Encoding.GetEncoding( "UTF-8" ) );
				string readText = sReader.ReadToEnd();
				sReader.Close();

				//Version、R_ROM、R_SXDRV等取得              
				string tmpStr;
				for( int iCount = 0 ; iCount < readText.Length ; iCount++ ) {
					tmpStr = getLine( iCount, readText );
					if( tmpStr.IndexOf( itemName ) > -1 ) {
						tmpStr = tmpStr.Trim();                             //空白削除
						int lineLen = tmpStr.Length;                        //1行長
						int lastIndex = tmpStr.LastIndexOf( "\\" ) + 1;         //最後から\\の位置
						int dataLen = lineLen - lastIndex;                  //データ長
						dataStrng = tmpStr.Substring( lastIndex, dataLen );   //データ取得
						break;
					}
				}
			} catch( Exception exp ) {
				MessageBox.Show( exp.Message, "Version.sys読み込みエラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
				//ECNC3.Views.ECNC3Exception.FileIOFilter( exp, this );
			}
		}
		/// <summary>
		/// 指定行番号の文字列を取得
		/// </summary>
		/// <param name="iLineNum">行番号 0から</param>
		/// <param name="strReadText">読み込んだファイル内容</param>
		/// <returns></returns>
		private string getLine( int iLineNum, string strReadText )
		{
			int lineIndex = 0;
			int strCount = 0;
			for( int iCount = 0 ; iCount < strReadText.Length ; iCount++ ) {
				if( strReadText[iCount] == '\r' ) {
					iCount++;   //LFまで回す
					strCount++; //LFまで回す

					lineIndex++;//初回ここを通れば１行
					if( iLineNum == lineIndex ) {
						string tmpStr = strReadText.Substring( iCount - strCount, strCount - 1 );//CRは含めない
						return tmpStr;
					}
					strCount = 0;//列カウンタリセット
					continue;
				}
				strCount++;
			}
			return "";//エラー
		}
	}
}
