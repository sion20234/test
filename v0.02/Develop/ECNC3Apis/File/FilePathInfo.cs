﻿using System;
using System.IO;
using System.Xml;
using ECNC3.Enumeration;
using ECNC3.Models.Common;
using System.Drawing;

namespace ECNC3.Models
{
	/// <summary>設定ファイルアクセス</summary>
	public class FilePathInfo : FileAccessCommon, IEcnc3Backup, IDisposable
	{//Add-1/3
		public static string SystemFileIO { get; private set; }
		public static string Usb { get; private set; }
		public static string MasterData { get; private set; }
		public static string ProgramData { get; private set; }
		public static string MacroData { get; private set; }
		public static string UpdateData { get; private set; }
		public static string NeoUpdateData { get; private set; }
		public static string TechnoUpdateData { get; private set; }
		public static string BackUpData { get; private set; }
		public static string RT64ECPATH { get; private set; }
		public static string ECNC3PATH { get; private set; }
		public static string GCodeHelpFile { get; private set; }
        public static string MCodeHelpFile { get; private set; }
		public static string LogSetting { get; set; }
		public static string ProcLog { get; set; }				//2017-06-23追加：柏原
		public static string NullPath { get; private set; }		//2017-06-23追加：柏原
		public static string User { get; private set; }			//2017-06-26追加：柏原
		public static string Log  { get; private set; }			//2017-06-29追加：柏原

		/// <summary>XMLファイルドキュメントポインタ</summary>
		private XmlDocument _xmlDoc = null;
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>編集あり</summary>
		private bool _edted = false;


        /// <summary>コンストラクタ</summary>
        public FilePathInfo()
		{
			Name = this.ToString();
			RegistMasterFile( @"Path.xml" );
		}
		/// <summary>読み込み</summary>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// XMLファイルの読み込みを行います。
		/// </remarks>
		public ResultCodes Read()
		{
			ResultCodes ret = ResultCodes.Success;
			if( null == _xmlDoc ) {
				_xmlDoc = new XmlDocument();
			}
			AidXml xml = new AidXml();
			ret = xml.Read( ref _xmlDoc, FilePath );
			{//Add-2/3
				if( AttrText( "Root/SystemFileIO", "path" ) != "" ) {
					SystemFileIO = AttrText( "Root/SystemFileIO", "path" );
				}
				if( AttrText( "Root/Usb", "path" ) != "" ) {
					Usb = AttrText( "Root/Usb", "path" );
				}
				if( AttrText( "Root/MasterData", "path" ) != "" ) {
					MasterData = AttrText( "Root/MasterData", "path" );
				}
				if( AttrText( "Root/ProgramData", "path" ) != "" ) {
					ProgramData = AttrText( "Root/ProgramData", "path" );
				}
				if( AttrText( "Root/MacroData", "path" ) != "" ) {
					MacroData = AttrText( "Root/MacroData", "path" );
				}
				if( AttrText( "Root/UpdateData", "path" ) != "" ) {
					UpdateData = AttrText( "Root/UpdateData", "path" );
				}
				if( AttrText( "Root/UpdateData", "neo" ) != "" ) {
					NeoUpdateData = AttrText( "Root/UpdateData", "neo" );
				}
				if( AttrText( "Root/UpdateData", "techno" ) != "" ) {
					TechnoUpdateData = AttrText( "Root/UpdateData", "techno" );
				}
				if( AttrText( "Root/BackUpData", "path" ) != "" ) {
					BackUpData = AttrText( "Root/BackUpData", "path" );
				}
				if( AttrText( "Root/RT64EC", "path" ) != "" ) {
					RT64ECPATH = AttrText( "Root/RT64EC", "path" );
				}
				if( AttrText( "Root/ECNC3", "path" ) != "" ) {
					ECNC3PATH = AttrText( "Root/ECNC3", "path" );
				}
				if( AttrText( "Root/GCodeHelpFile", "path" ) != "" ) {
					GCodeHelpFile = AttrText( "Root/GCodeHelpFile", "path" );
				}
				if( AttrText( "Root/MCodeHelpFile", "path" ) != "" ) {
					MCodeHelpFile = AttrText( "Root/MCodeHelpFile", "path" );
				}
				if( AttrText( "Root/LogSetting", "path" ) != "" ) {
					LogSetting = AttrText( "Root/LogSetting", "path" );
				}
				if( AttrText( "Root/ProcLog", "path" ) != "" ) {
					ProcLog = AttrText( "Root/ProcLog", "path" );
				}
				if( AttrText( "Root/NullPath", "path" ) != "" ) {
					NullPath = AttrText( "Root/NullPath", "path" );
				}
				if( AttrText( "Root/User", "path" ) != "" ) {
					User = AttrText( "Root/User", "path" );
				}
				if( AttrText( "Root/Log", "path" ) != "" ) {
					Log = AttrText( "Root/Log", "path" );
				}
				return ret;
			}
		}
        /// <summary>書き込み</summary>
        /// <returns>実行結果</returns>
        /// <remarks>
        /// ロードされているXMLノードをファイル出力します。
        /// Read()により読込がされていない場合、書込みされずに
        /// 処理を抜けます。
        /// </remarks>
        public ResultCodes Write()
        {
            _edted = true;
			//Add-3/3
			WriteAttr( "Root/SystemFileIO", "path", SystemFileIO);
            WriteAttr("Root/Usb", "path", Usb);
            WriteAttr("Root/MasterData", "path", MasterData);
            WriteAttr("Root/ProgramData", "path", ProgramData);
            WriteAttr("Root/MacroData", "path", MacroData);
            WriteAttr("Root/UpdateData", "path" , UpdateData);
            WriteAttr("Root/UpdateData", "neo", NeoUpdateData);
            WriteAttr("Root/UpdateData", "techno", TechnoUpdateData);
            WriteAttr("Root/BackUpData", "path", BackUpData);
            WriteAttr("Root/RT64EC", "path", RT64ECPATH);
            WriteAttr("Root/ECNC3", "path", ECNC3PATH);
            WriteAttr("Root/GCodeHelpFile", "path", GCodeHelpFile);
            WriteAttr("Root/MCodeHelpFile", "path", MCodeHelpFile);
			WriteAttr( "Root/LogSetting", "path", LogSetting );
			WriteAttr( "Root/ProcLog", "path", ProcLog );
			WriteAttr( "Root/NullPath", "path", NullPath );
			WriteAttr( "Root/User", "path", User );
			WriteAttr( "Root/Log", "path", Log );

			ResultCodes ret = ResultCodes.Success;
            while (true == _edted)
            {
                ret = ResultCodes.McNotInitialize;
                if (null != _xmlDoc)
                {
                    AidLog logs = new AidLog("PathInfo.Save");
                    AidXml xml = new AidXml();
                    return xml.Write(_xmlDoc, FilePath);
                }
                if (true == _edted)
                {
                    _edted = false;
                }
                break;
            }
            return ret;
        }

        //public ResultCodes Create()
        //{
        //    using (StreamWriter sw = new StreamWriter(FilePath))
        //        DefaultBackColor = ColorTranslator.FromHtml("#232300");
        //    EnabledBackColor = ColorTranslator.FromHtml("#dcdcdc");
        //    DefaultForeColor = ColorTranslator.FromHtml("#dcdcdc");
        //    EnabledForeColor = ColorTranslator.FromHtml("#232300");
        //    DefaultLineColor = ColorTranslator.FromHtml("#dcdcdc");
        //    SelectedLineColor = ColorTranslator.FromHtml("#00ff00");
        //    Read();
        //    _xmlDoc.CreateTextNode("Root/ColorTable");

        //    return Write();
        //}

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

		/// <summary>ノードリスト取得</summary>
		/// <param name="pathXe">要素のXPath</param>
		/// <returns>取得されたノードリスト</returns>
		public XmlNodeList GetList( string pathXe )
		{
			XmlNodeList list = _xmlDoc.SelectNodes( pathXe );
			return list;
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
					}
					//  アンマネージリソースの解放
				}
				_disposed = true;
			} finally {
				;   //  基底クラスのDispose()を確実に呼び出す。
					//base.Dispose( disposing );
			}
		}
		/// <summary>属性値取得(実数)</summary>
		/// <param name="element">要素XPath</param>
		/// <param name="attr">属性名</param>
		/// <returns>取得結果</returns>
		public double AttrDouble( string element, string attr )
		{
			double result = 0.0;
			string text = AttrText( element, attr );
			AidConvert cnv = new AidConvert();
			if( false == cnv.TryParse( text, out result ) ) {
				result = 0.0;
			}
			return result;
		}
		/// <summary>属性値取得(整数)</summary>
		/// <param name="element">要素XPath</param>
		/// <param name="attr">属性名</param>
		/// <returns>取得結果</returns>
		public int AttrValue( string element, string attr )
		{
			AidXml xml = new AidXml();
#if __KEY_LOG_PARSE_XML__
			AidLog logs = new AidLog( "FileSetting.AttrValue" );
			int result = xml.AttrValue( _xmlDoc, element, attr );
			if( 0 == result ) {
				logs.Error( $"{element}/@{attr}= <FAIL TO GET!>." );
			} else {
				logs.Debug( $"{element}/@{attr}={result}." );
			}
			return result;
#else
			return xml.AttrValue( _xmlDoc, element, attr );
#endif
		}
		/// <summary>属性値取得(bool)</summary>
		/// <param name="element">要素XPath</param>
		/// <param name="attr">属性名</param>
		/// <returns>取得結果</returns>
		public bool AttrBool( string element, string attr )
		{
			return ( 0 != AttrValue( element, attr ) ) ? true : false;
		}
		/// <summary>属性値取得(文字列)</summary>
		/// <param name="element">要素XPath</param>
		/// <param name="attr">属性名</param>
		/// <returns>取得結果</returns>
		public string AttrText( string element, string attr )
		{
			AidXml xml = new AidXml();
#if __KEY_LOG_PARSE_XML__
			AidLog logs = new AidLog( "FileSetting.AttrText" );
			string result = xml.AttrText( _xmlDoc, element, attr );
			if( true == string.IsNullOrEmpty( result ) ) {
				logs.Error( $"{element}/@{attr}= <FAIL TO GET!>." );
			} else {
				logs.Debug( $"{element}/@{attr}={result}." );
			}
			return result;
#else
			return xml.AttrText( _xmlDoc, element, attr );
#endif
		}
		//      /// <summary>属性値取得(文字列)</summary>
		///// <param name="element">要素XPath</param>
		///// <param name="attr">属性名</param>
		///// <returns>取得結果</returns>
		//public string AttrColor(string element, string attr)
		//      {
		//          AidXml xml = new AidXml();
		//          string strColor = "";
		//          Color ret;
		//          strColor = xml.AttrText(_xmlDoc, element, attr);
		//          if()
		//          {

		//          }
		//          return 
		//      }
		/// <summary>属性編集</summary>
		/// <param name="element">要素名</param>
		/// <param name="attr">属性名</param>
		/// <param name="val">設定値</param>
		/// <returns>
		///		<list type="bullet" >
		///			<item>true=編集あり</item>
		///			<item>false=編集なし</item>
		///		</list>
		/// </returns>
		public bool WriteAttr( string element, string attr, string val )
		{
			if( null != _xmlDoc ) {
				XmlNodeList list = _xmlDoc.SelectNodes( element );
				if( null != list ) {
					if( 0 < list.Count ) {
						//	複数検出されたとしても上端の取得結果のみを解析する。
						if( null != list[0].Attributes ) {
							//	属性がある→属性値を検索する。
							return WriteAttr( list[0].Attributes, attr, val );
						}
					}
				}
			}
			return false;
		}
		/// <summary>属性値編集</summary>
		/// <param name="element">要素パス</param>
		/// <param name="attr">属性名</param>
		/// <param name="val">設定値</param>
		/// <param name="number">リスト番号</param>
		/// <returns>
		///		<list type="bullet" >
		///			<item>true=編集あり</item>
		///			<item>false=編集なし</item>
		///		</list>
		/// </returns>
		public bool WriteAttr( string element, string attr, string val, int number )
		{
			if( null != _xmlDoc ) {
				XmlNodeList list = _xmlDoc.SelectNodes( element );
				if( null != list ) {
					if( 0 < list.Count ) {
						foreach( XmlNode item in list ) {
							if( null != item.Attributes ) {
								if( false == IsMatchIndexNumber( item.Attributes, number ) ) {
									continue;
								}
								return WriteAttr( item.Attributes, attr, val );
							}
						}
					}
				}
			}
			return false;
		}
		/// <summary>属性書き込み</summary>
		/// <param name="list">属性リスト</param>
		/// <param name="attr">属性名</param>
		/// <param name="val">設定値</param>
		/// <returns>書き込みの有無</returns>
		private bool WriteAttr( XmlAttributeCollection list, string attr, string val )
		{
			string name = string.Empty;
			foreach( XmlAttribute attr1 in list ) {
				name = attr1.Name;
				if( 0 != string.Compare( name, attr, false ) ) {
					continue;
				}
				if( 0 != string.Compare( attr1.Value, val, StringComparison.OrdinalIgnoreCase ) ) {
					attr1.Value = val;
					_edted = true;
					return true;
				}
				break;
			}
			return false;
		}
		/// <summary>インデックス番号の検索</summary>
		/// <param name="list">検索先の属性値リスト</param>
		/// <param name="number">検索キー</param>
		/// <returns>
		///		<list type="bullet" >
		///			<item>true=一致</item>
		///			<item>false=不一致</item>
		///		</list>
		/// </returns>
		/// <remarks>
		/// リスト構造を持つノードのインデックス番号の一致判定を行います。
		/// ノードにはインデックス番号の定義として「num」属性値を存在することを前提としています。
		/// </remarks>
		private bool IsMatchIndexNumber( XmlAttributeCollection list, int number )
		{
			string key = number.ToString();
			foreach( XmlAttribute attr in list ) {
				if( 0 == string.Compare( "num", attr.Name, false ) ) {
					if( 0 == string.Compare( key, attr.Value, false ) ) {
						return true;
					}
				}
			}
			return false;
		}
	}
}
