using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using ECNC3.Enumeration;
using ECNC3.Models.Common;

namespace ECNC3.Models
{
	/// <summary>XMLファイルアクセス共通クラス</summary>
	public class FileAccessCommon
	{
		/// <summary>コンストラクタ</summary>
		public FileAccessCommon()
		{
		}
		/// <summary>名称</summary>
		public string Name { get; protected set; }
		/// <summary>アクセス対象のファイルパス</summary>
		protected string FilePath { get; private set; }

		/// <summary>XMLファイルのヘッダ要素を作成</summary>
		/// <returns>作成されたXML要素</returns>
		protected XElement MakeProfile()
		{
			return new XElement( "Prof", new XAttribute( "dt", DateTime.Now.ToString( "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture ) ) );
		}

		/// <summary>データベースファイル名登録</summary>
		/// <param name="fileName">ファイル名</param>
		/// <remarks>
		/// MASTERフォルダに保存されるべきファイルを登録します。
		/// </remarks>
		protected void RegistMasterFile( string fileName )
		{
			using( ECNC3Settings cmn = new ECNC3Settings() ) {
				FilePath = Path.Combine( cmn.DirectoryMaster, fileName );
			}
		}
        /// <summary>ログフォルダの登録</summary>
        /// <param name="fileName">ファイル名</param>
        protected void RegistLogFile( string fileName )
		{
			using( ECNC3Settings cmn = new ECNC3Settings() ) {
				FilePath = Path.Combine( cmn.DirectoryLog, fileName );
			}
		}
		/// <summary>ファイル有無判定</summary>
		/// <returns>
		///		<list type="bullet" >
		///			<item>true=ファイルあり</item>
		///			<item>false=ファイルなし</item>
		///		</list>
		/// </returns>
		protected bool ExistsFile()
		{
			if( true == string.IsNullOrEmpty( FilePath ) ) {
				return false;
			}
			return System.IO.File.Exists( FilePath );
		}
		/// <summary>バックアップ</summary>
		/// <param name="path">バックアップ対象のファイルパス</param>
		/// <param name="targetDirectory">バックアップ先となるディレクトリパス</param>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// 既定のバックアップフォルダにファイルをバックアップします。
		/// ファイル名は同名であるとします。
		/// </remarks>
		public ResultCodes Backup( string path, string targetDirectory )
		{
			while( true ) {
				if( false == File.Exists( path ) ) {
					break;
				}
				if( false == Directory.Exists( targetDirectory ) ) {
					Directory.CreateDirectory( targetDirectory );
				}
				string fileName = Path.GetFileName( path );
				File.Copy( path, Path.Combine( targetDirectory, fileName ), true );
				break;
			}
			return ResultCodes.Success;
		}
		/// <summary>リストア</summary>
		/// <param name="sourceDirectory">リストア元のディレクトリパス</param>
		/// <param name="targetPath">リストア対象のファイルパス</param>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// 既定のディレクトリに対してリストアを実行します。
		/// ファイル名は同一であるとします。
		/// ファイルがない＝失敗ではなく、「リストア対象ではない」と解釈します。
		/// </remarks>
		public ResultCodes Restore( string sourceDirectory, string targetPath )
		{
			ResultCodes ret = ResultCodes.NotExecute;
			try {
				while( true ) {
					if( false == Directory.Exists( sourceDirectory ) ) {
						break;
					}
					string fileName = Path.GetFileName( targetPath );
					if( false == File.Exists( Path.Combine( sourceDirectory, fileName ) ) ) {
						break;
					}
					File.Copy( Path.Combine( sourceDirectory, fileName ), targetPath, true );
					ret = ResultCodes.Success;
					break;
				}
			} catch( Exception e ) {
				bool unexpected = true;
				if( ( e is ArgumentException ) ||
					( e is ArgumentNullException ) ||
					( e is PathTooLongException ) ||
					( e is DirectoryNotFoundException ) ||
					( e is FileNotFoundException ) ||
					( e is IOException ) ||
					( e is NotSupportedException ) ||
					( e is UnauthorizedAccessException ) ) {
					unexpected = false;   //	想定内の例外。
				}
				//AidLog logs = new AidLog( "FileAccessCommon.Restore" );
				//ret = logs.Exception( e, unexpected );
			}
			return ret;
		}
		/// <summary>バックアップ数整合</summary>
		/// <param name="backupRoot">バックアップ先ディレクトリパス</param>
		/// <param name="saveCount">バックアップファイル数</param>
		/// <param name="fileNameFormat">バックアップファイル名書式</param>
		/// <returns>実行結果</returns>
		public ResultCodes BackupStandBy( string backupRoot, int saveCount, string fileNameFormat )
		{
			//	拡張子の制限
			string filteringExtension = Path.GetExtension( fileNameFormat );
			//	ファイル名の長さ制限
			int filteringNameLength = Path.GetFileNameWithoutExtension( fileNameFormat ).Length;

			List<string> files = new List<string>();
			while( true ) {
				string[] fileList = Directory.GetFileSystemEntries( backupRoot, fileNameFormat );
				if( null == fileList ) {
					break;
				}
				if( 2 > fileList.Length ) {
					break;  //	処理不要
				}
				string name = string.Empty;
				//	既定のファイル名に該当するファイルのみをログファイルとして管理する。
				//	リネームされたファイルは対象としない。
				foreach( string path in fileList ) {
					if( 0 != string.Compare( filteringExtension, Path.GetExtension( path ), true ) ) {
						continue;
					}
					name = Path.GetFileNameWithoutExtension( path );
					if( filteringNameLength != name.Length ) {
						continue;
					}
					if( false == name.All( ( x ) => char.IsDigit( x ) ) ) {
						continue;
					}
					files.Add( path );
				}
				if( 0 < files.Count ) {
					int index;
					files.Sort( ( a, b ) => ( 0 > string.Compare( a, b, true ) ? 1 : -1 ) );
					for( index = saveCount ; index < files.Count ; ++index ) {
						File.Delete( files[index] );
					}
				}
				break;
			}
			return ResultCodes.Success;
		}
	}
}
