using System;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using ECNC3.Enumeration;

namespace ECNC3.Models.Common
{
	/// <summary>XML操作</summary>
	/// <remarks>
	/// XMLファイルの操作を行うための支援クラスです。
	/// </remarks>
	internal class AidXml
	{
        /// <summary>ファイル読み込み</summary>
        /// <param name="root">XMLルート要素</param>
        /// <param name="path">読み込みファイルパス</param>
        /// <returns>実行結果</returns>
		public ResultCodes Read( ref XmlDocument root, string path )
		{
			ResultCodes ret = ResultCodes.Success;
			if( null != root ) {
				//AidLog logs = new AidLog( "AidXml.Read" );
				try {
					root.Load( path );
				} catch( Exception e ) {
					bool unexpected = true;
					if( ( e is ArgumentNullException ) ||
						( e is XmlException ) ||
						( e is ArgumentException ) ||
						( e is PathTooLongException ) ||
						( e is DirectoryNotFoundException ) ||
						( e is FileNotFoundException ) ||
						( e is IOException ) ||
						( e is UnauthorizedAccessException ) ||
						( e is NotSupportedException ) ||
						( e is System.Security.SecurityException ) ) {
						unexpected = false;   //	想定内の例外。
					}
					//ret = logs.Exception( e, unexpected );
				}
			}
			return ret;
		}
        /// <summary>XMLファイル書き込み</summary>
        /// <param name="root">XMLルート要素</param>
        /// <param name="path">書き込みファイルパス</param>
        /// <returns>実行結果</returns>
		public ResultCodes Write( XmlDocument root, string path )
		{
			ResultCodes ret = ResultCodes.Success;
			if( null != root ) {
				//AidLog logs = new AidLog( "AidXml.Write" );
				try {
					root.Save( path );
				} catch( Exception e ) {
					bool unexpected = true;
					if( e is XmlException ) {
						unexpected = false;   //	想定内の例外。
					}
					//ret = logs.Exception( e, unexpected );
				}
			}
			return ret;
		}

		/// <summary>XPath解析</summary>
		/// <param name="doc">解析対象のXMLドキュメントオブジェクト</param>
		/// <param name="pathXe">取得したい要素へのパス</param>
		/// <param name="attr">取得したい pathXe 内の属性名</param>
		/// <returns>取得された値</returns>
		public string AttrText( XmlDocument doc, string pathXe, string attr )
		{
			//AidLog logs = new AidLog( "AidXml.AttrText(XPath)" );
			string result = string.Empty;
			while( null != doc ) {
				try {
					XmlNodeList list = doc.SelectNodes( pathXe );
					if( 0 < list.Count ) {
						//	複数検出されたとしても上端の取得結果のみを解析する。
						AidConvert cnv = new AidConvert();
						if( null != list[0].Attributes ) {
							//	属性がある→属性値を検索する。
							int index = 0;
							string name = string.Empty;
							for( index = 0 ; index < list[0].Attributes.Count ; ++index ) {
								name = list[0].Attributes[index].Name;
								if( 0 != string.Compare( name, attr ) ) {
									continue;
								}
								result = list[0].Attributes[index].Value;
								break;
							}
						} else {
							//	属性がない→要素内の値を判定する。
							result = list[0].Value;
						}
						return result;
					}
				} catch( Exception e ) {
					string note = string.Empty;
					if( e is System.Xml.XPath.XPathException ) {
						;   //	想定内の例外。
					} else {
						note = @"Catch of the unexpected exception!";   //	想定外の例外
					}
					//logs.Exception( e, note );
				}
				break;
			}
			return string.Empty;
		}
		/// <summary>XPath解析</summary>
		/// <param name="doc">解析対象のXMLドキュメントオブジェクト</param>
		/// <param name="pathXe">取得したい要素へのパス</param>
		/// <param name="attr">取得したい pathXe 内の属性名</param>
		/// <returns>取得された値</returns>
		public double AttrDouble( XmlDocument doc, string pathXe, string attr )
		{
			double result = 0.0;
			string text = AttrText( doc, pathXe, attr );
			AidConvert cnv = new AidConvert();
			if( true == cnv.TryParse( text, out result ) ) {
				return result;
			}
			return 0.0;
		}
		/// <summary>XPath解析</summary>
		/// <param name="doc">解析対象のXMLドキュメントオブジェクト</param>
		/// <param name="pathXe">取得したい要素へのパス</param>
		/// <param name="attr">取得したい pathXe 内の属性名</param>
		/// <returns>取得された値</returns>
		public int AttrValue( XmlDocument doc, string pathXe, string attr )
		{
			return (int)AttrDouble( doc, pathXe, attr );
		}
	}
}
