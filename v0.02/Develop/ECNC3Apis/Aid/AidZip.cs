using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECNC3.Models.Common
{
	/// <summary>ファイル圧縮支援</summary>
	public class AidZip
	{
		/// <summary>コンストラクタ</summary>
		public AidZip()
		{
			IfSuccessToDeleteSourceDirectory = true;
			IfFileExistsOverwrite = true;
		}
		/// <summary>ディレクトリ圧縮成功時、対象のディレクトリを削除する</summary>
		public bool IfSuccessToDeleteSourceDirectory { get; set; }
		/// <summary>ファイルが存在する場合、上書きする。</summary>
		public bool IfFileExistsOverwrite { get; set; }

		/// <summary>ディレクトリの圧縮</summary>
		/// <param name="sourceDirectory">圧縮対象のディレクトリ</param>
		/// <param name="outputFilePath">出力ファイルパス</param>
		/// <returns>
		///		<list type="bullet" >
		///			<item>0=成功</item>
		///			<item>-1=引数異常</item>
		///			<item>-2=ファイル作成失敗</item>
		///		</list>
		/// </returns>
		public int Create( string sourceDirectory, string outputFilePath )
		{
			AidLog logs = new AidLog( "McCommProc.Create" );
			if( true == string.IsNullOrEmpty( sourceDirectory ) ) {
				return -1;
			} else if( false == Directory.Exists( sourceDirectory ) ) {
				return -1;
			} else if( true == string.IsNullOrEmpty( outputFilePath ) ) {
				return -1;
			}
			
			try {
				//	圧縮先ディレクトリを作成
				string outputDirectory = Path.GetDirectoryName( outputFilePath );
				if( false == Directory.Exists( outputDirectory ) ) {
					Directory.CreateDirectory( outputDirectory );
				}
				//	Tempディレクトリを作成
				string tempAppDir = @"Temp";
				if( false == File.Exists( tempAppDir ) ) {
					Directory.CreateDirectory( tempAppDir );
				}
				string tempName = DateTime.Now.ToString( "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture );
				string tempFileName = Path.Combine( tempAppDir, $"{tempName}.zip" );
				//	指定のディレクトリを圧縮する。
				ZipFile.CreateFromDirectory( sourceDirectory, tempFileName );
				//	圧縮したファイルをコピー
				File.Copy( tempFileName, outputFilePath, IfFileExistsOverwrite );

				if( true == IfSuccessToDeleteSourceDirectory ) {
					//	圧縮元となったディレクトリをファイルごと削除する。
					DirectoryInfo di = new DirectoryInfo( sourceDirectory );
					di.Delete( true );
				}
				//	コピー元のファイルを削除する。
				File.Delete( tempFileName );
			} catch( Exception e ) {
				bool unexpected = true;
				if( ( e is ArgumentNullException ) ||
					( e is ArgumentException ) ||
					( e is UnauthorizedAccessException ) ||
					( e is PathTooLongException ) ||
					( e is DirectoryNotFoundException ) ||
					( e is IOException ) ||
					( e is System.Security.SecurityException ) ||
					( e is NotSupportedException ) ) {
					unexpected = false;   //	想定内の例外。
				}
				logs.Exception( e, unexpected );
				return -2;
			}
			return 0;
		}
		/// <summary>展開</summary>
		/// <param name="sourceFilePath">展開対象ファイルパス</param>
		/// <param name="outputDirectory">出力ディレクトリパス</param>
		/// <returns>
		///		<list type="bullet" >
		///			<item>0=成功</item>
		///			<item>-1=引数異常</item>
		///			<item>-2=ファイル作成失敗</item>
		///		</list>
		/// </returns>
		/// <remarks>
		/// 引数で指定されたZIPファイルを展開します。
		/// </remarks>
		public int Extract( string sourceFilePath, string outputDirectory )
		{
			AidLog logs = new AidLog( "McCommProc.Extract" );
			if( true == string.IsNullOrEmpty( sourceFilePath ) ) {
				return -1;
			} else if( false == File.Exists( sourceFilePath ) ) {
				return -1;
			} else if( true == string.IsNullOrEmpty( outputDirectory ) ) {
				return -1;
			}
			try {
				if( false == File.Exists( outputDirectory ) ) {
					Directory.CreateDirectory( outputDirectory );
				}
				//	指定のディレクトリを解凍、展開する。
				ZipFile.ExtractToDirectory( sourceFilePath, outputDirectory );
			} catch( Exception e ) {
				bool unexpected = true;
				if( ( e is ArgumentNullException ) ||
					( e is ArgumentException ) ||
					( e is UnauthorizedAccessException ) ||
					( e is PathTooLongException ) ||
					( e is DirectoryNotFoundException ) ||
					( e is IOException ) ||
					( e is System.Security.SecurityException ) ||
					( e is NotSupportedException ) ) {
					unexpected = false;   //	想定内の例外。
				}
				logs.Exception( e, unexpected );
				return -2;
			}
			return 0;
		}
	}
}
