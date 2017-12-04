using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using ECNC3.Enumeration;

namespace ECNC3.Models.Common
{
	/// <summary>LINQ操作支援</summary>
	public class AidXmlLinq
	{
		/// <summary>ロード失敗時のリトライ回数</summary>
		private const int RetryCount = 3;
		/// <summary>ロード失敗時のリトライ待機時間(ms)</summary>
		private const int RetryDelayTime = 100;
		/// <summary>コンストラクタ</summary>
		public AidXmlLinq()
		{
		}
		/// <summary>XMLファイル読込</summary>
		/// <param name="root">取得された要素</param>
		/// <param name="path">読み込みファイルパス</param>
		/// <returns>
		///		<list type="bullet">
		///			<item>0=成功</item>
		///			<item>-1=引数異常</item>
		///			<item>-2=例外発生。</item>
		///			<item>-3=例外発生。ミューテックスタイムアウト</item>
		///		</list>
		/// </returns>
		/// <remarks>
		/// XMLファイルの読込みを行います。
		/// 読み取り専用で開くことを前提とします。
		/// </remarks>
		public ResultCodes ReadByReadOnly( ref XElement root, string path )
		{
			AidLog logs = new AidLog( "AidXml.ReadElementByReadOnly" );
			ResultCodes ret = 0;
			int retry = 0;
			for( retry = 0 ; retry < RetryCount ; ++retry ) {
				if( 0 < retry ) {
					System.Threading.Thread.Sleep( RetryDelayTime );
					logs.Error( string.Format( CultureInfo.InvariantCulture, "Retry ({0}/{1}).", retry, RetryCount ) );
				}
				try {
					ret = 0;
					//	XMLファイル読み込み
					FileStream fs = new FileStream( path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite );
					using( TextReader sr = new StreamReader( fs ) ) {
						using( new AidMutex( path ) ) {
							root = XElement.Load( sr );
						}
					}
				} catch( System.Xml.XmlException e ) {
					if( retry > RetryCount ) {
						ret = logs.Exception( e, false );
					}
					continue;
				} catch( IOException e ) {
					if( retry > RetryCount ) {
						ret = logs.Exception( e, false );
					}
					continue;
				} catch( Exception e ) {
					bool unexpected = true;
					if( ( e is ObjectDisposedException ) ||
						( e is ArgumentOutOfRangeException ) ||
						( e is System.Threading.AbandonedMutexException ) ||
						( e is FileNotFoundException ) ||
						( e is FormatException ) ||
						( e is NullReferenceException ) ) {
						unexpected = false;   //	想定内の例外。
					}
					ret = logs.Exception( e, unexpected );
				}
				return ret;
			}
			logs.Error( string.Format( CultureInfo.InvariantCulture, "Retry count({0}) over. Failed to load file({1}).", RetryCount, path ) );
			return ret;
		}
		/// <summary>XMLファイル読込</summary>
		/// <param name="root">取得された要素</param>
		/// <param name="path">読み込みファイルパス</param>
		/// <returns>
		///		<list type="bullet">
		///			<item>0=成功</item>
		///			<item>-1=引数異常</item>
		///			<item>-2=例外発生。</item>
		///			<item>-3=例外発生。ミューテックスタイムアウト</item>
		///		</list>
		/// </returns>
		/// <remarks>
		/// XMLファイルの読込みを行います。
		/// 編集を目的とします。排他制御および書込み処理は呼出元で行ってください。
		/// </remarks>
		public ResultCodes ReadByReadWrite( ref XElement root, string path )
		{
			AidLog logs = new AidLog( "AidXml.ReadElementByReadWrite" );
			ResultCodes ret = ResultCodes.Success;
			int retry = 0;
			for( retry = 0 ; retry < RetryCount ; ++retry ) {
				if( 0 < retry ) {
					System.Threading.Thread.Sleep( RetryDelayTime );
					logs.Error( string.Format( CultureInfo.InvariantCulture, "Retry ({0}/{1}).", retry, RetryCount ) );
				}
				try {
					ret = ResultCodes.Success;
					//	XMLファイル読み込み
					root = XElement.Load( path );
				} catch( IOException e ) {
					if( retry > RetryCount ) {
						ret = logs.Exception( e, false );
					}
					continue;
				} catch( Exception e ) {
					bool unexpected = true;
					if( ( e is ObjectDisposedException ) ||
						( e is ArgumentOutOfRangeException ) ||
						( e is System.Threading.AbandonedMutexException ) ||
						( e is FileNotFoundException ) ||
						( e is System.Xml.XmlException ) ||
						( e is FormatException ) ||
						( e is NullReferenceException ) ) {
						unexpected = false;	//	想定内の例外。
					}
					ret = logs.Exception( e, unexpected );
				}
				return ret;
			}
			logs.Error( string.Format( CultureInfo.InvariantCulture, "Retry count({0}) over. Failed to load file({1}).", RetryCount, path ) );
			return ret;
		}
		/// <summary>XMLファイル書込み</summary>
		/// <param name="root">出力対象の要素</param>
		/// <param name="path">書き込みファイルパス</param>
		/// <returns>
		///		<list type="bullet">
		///			<item>0=成功</item>
		///			<item>-1=引数異常</item>
		///			<item>-2=例外発生。</item>
		///			<item>-3=例外発生。ミューテックスタイムアウト</item>
		///		</list>
		/// </returns>
		/// <remarks>
		/// XMLファイルの書込みを行います。新規作成を前提とします。
		/// 編集を行う場合は、ReadElementByReadWrite 関数を使用してください。
		/// </remarks>
		public ResultCodes Write( XElement root, string path )
		{
			AidLog logs = new AidLog( "AidXml.WriteElement" );
			ResultCodes ret = ResultCodes.Success;
			if( null == root ) {
				return ResultCodes.InvalidArgument;
			}
			int retry = 0;
			for( retry = 0 ; retry < RetryCount ; ++retry ) {
				if( 0 < retry ) {
					System.Threading.Thread.Sleep( RetryDelayTime );
					logs.Error( string.Format( CultureInfo.InvariantCulture, "Retry ({0}/{1}).", retry, RetryCount ) );
				}
				try {
					ret = ResultCodes.Success;
					using( new AidMutex( path ) ) {
						using( FileStream fs = new FileStream( path, FileMode.Create ) ) {
							root.Save( fs );
						}
					}
				} catch( IOException e ) {
					if( retry > RetryCount ) {
						ret = logs.Exception( e, false );
					}
					continue;
				} catch( Exception e ) {
					bool unexpected = true;
					if( ( e is ObjectDisposedException ) ||
						( e is System.Threading.AbandonedMutexException ) ||
						( e is DirectoryNotFoundException ) ||
						( e is PathTooLongException ) ||
						( e is ArgumentOutOfRangeException ) ||
						( e is ArgumentNullException ) ||
						( e is ArgumentException ) ||
						( e is NotSupportedException ) ||
						( e is FileNotFoundException ) ||
						( e is System.Security.SecurityException ) ||
						( e is UnauthorizedAccessException ) ) {
						unexpected = false;	//	想定内の例外。
					}
					ret = logs.Exception( e, unexpected );
				}
				break;
			}
			return ret;
		}
		/// <summary>プロファイル情報の更新</summary>
		/// <param name="root">XMLルート要素</param>
		/// <returns>実行結果</returns>
		public ResultCodes UpdateProfile( XElement root )
		{
			XElement xe = root.Element( "Prof" );
			if( null != xe ) {
				SetAttr( xe, "dt", DateTime.Now.ToString( "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture ) );
			} else {
				root.Add( new XElement( "Prof", new XAttribute( "dt", DateTime.Now.ToString( "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture ) ) ) );
			}
			return ResultCodes.Success;
		}
		/// <summary>検索</summary>
		/// <param name="root">検索対象のルートノードの参照</param>
		/// <param name="number">検索番号(キー番号)</param>
		/// <param name="result">検索した結果得られたノードの参照</param>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// 既定の書式に沿っていることを前提に検索を行います。
		/// 引数 root 下に Item 要素値が一覧されている構造に num 属性値が存在することを前提に検索を行い、
		/// 該当のXML要素の参照を返します。
		/// </remarks>
		public ResultCodes FindNumber( XElement root, int number, ref XElement result )
		{
			ResultCodes ret = ResultCodes.Success;
			AidLog logs = new AidLog( "AidXmlLinq.FindNumber" );
			try {
				result = ( from item in root.Elements( "Item" )
						   where item.Attribute( "num" ).Value == $"{number}"
						   select item ).SingleOrDefault();
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

		/// <summary>検索</summary>
		/// <param name="root">検索対象のルートノードの参照</param>
		/// <param name="name">検索文字列(キー名称)</param>
		/// <param name="result">検索した結果得られたノードの参照</param>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// 既定の書式に沿っていることを前提に検索を行います。
		/// 引数 root 下に Item 要素値が一覧されている構造に nam 属性値が存在することを前提に検索を行い、
		/// 該当のXML要素の参照を返します。
		/// </remarks>
		public ResultCodes FindName( XElement root, string name, ref XElement result )
		{
			ResultCodes ret = ResultCodes.Success;
			AidLog logs = new AidLog( "AidXmlLinq.FindName" );
			try {
				result = ( from item in root.Elements( "Item" )
						   where item.Attribute( "nam" ).Value == name
						   select item ).SingleOrDefault();
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

		#region 属性値の取得
		/// <summary>String値による取得</summary>
		/// <param name="root">検索対象となる要素</param>
		/// <param name="name">検索属性名</param>
		/// <returns>属性値。取得できない場合は、string.Empty を返す。</returns>
		public string GetAttrText( XElement root, string name )
		{
			if( ( null == root ) || ( true == string.IsNullOrEmpty( name ) ) ) {
				return string.Empty;
			}
			XAttribute attr = root.Attribute( name );
			if( null != attr ) {
				return attr.Value;
			}
			return string.Empty;
		}

		/// <summary>テキストによる属性値取得</summary>
		/// <param name="root">解析対象のルートノード</param>
		/// <param name="elementName">要素名</param>
		/// <param name="attrName">属性名</param>
		/// <returns>取得結果</returns>
		public string GetAttrText( XElement root, string elementName, string attrName )
		{
			XElement xe = root.Element( elementName );
			if( null != xe ) {
				return GetAttrText( xe, attrName );
			}
			return string.Empty;
		}
		/// <summary>int値による取得</summary>
		/// <param name="root">検索対象となる要素</param>
		/// <param name="name">検索属性名</param>
		/// <returns>int値 変換された属性値。取得できない場合は、0 を返す。</returns>
		public int GetAttrValue( XElement root, string name )
		{
			if( ( null == root ) || ( true == string.IsNullOrEmpty( name ) ) ) {
				return 0;
			}
			int result = 0;
			XAttribute attr = root.Attribute( name );
			if( null != attr ) {
				if( false == int.TryParse( attr.Value, out result ) ) {
					AidLog logs = new AidLog( "AidXml.AttrValue" );
					logs.Error( string.Format( System.Globalization.CultureInfo.InvariantCulture, "Int32.TryParse(INT,{0})", attr.Value ) );
				}
			}
			return result;
		}
		/// <summary>bool値による取得</summary>
		/// <param name="root">検索対象となる要素</param>
		/// <param name="name">検索属性名</param>
		/// <returns>int値 変換された属性値。取得できない場合は、0 を返す。</returns>
		public bool GetAttrBool( XElement root, string name )
		{
			return ( 0 != GetAttrValue( root, name ) ) ? true : false;
		}
        /// <summary>16進数のint値による取得</summary>
        /// <param name="root">検索対象となる要素</param>
        /// <param name="name">検索属性名</param>
        /// <returns>int値 変換された属性値。取得できない場合は、0 を返す。</returns>
        public int GetAttrHex( XElement root, string name )
		{
			int result = 0;
			string text = GetAttrText( root, name );
			if( false == string.IsNullOrEmpty( text ) ) {
				if( false == int.TryParse( text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out result ) ) {
					AidLog logs = new AidLog( "AidXml.AttrHex" );
					logs.Error( string.Format( System.Globalization.CultureInfo.InvariantCulture, "Int32.TryParse(HEX,{0})", text ) );
					result = 0;
				}
			}
			return result;
		}
        /// <summary>属性値取得(実数)</summary>
        /// <param name="root">検索対象となる要素</param>
        /// <param name="name">検索属性名</param>
        /// <returns>int値 変換された属性値。取得できない場合は、0 を返す。</returns>
        public decimal GetAttrDecimal(XElement root, string name)
        {
            decimal result = 0;
            string text = GetAttrText(root, name);
            if (false == string.IsNullOrEmpty(text))
            {
                if (false == decimal.TryParse(text, out result))
                {
                    int temp = 0;
                    if (false == int.TryParse(text, out temp))
                    {
                        AidLog logs = new AidLog("AidXml.AttrDouble");
                        logs.Error(string.Format(System.Globalization.CultureInfo.InvariantCulture, "Int32.TryParse(HEX,{0})", text));
                        result = 0.0M;
                    }
                    else
                    {
                        result = temp;
                    }
                }
            }
            return result;
        }
        /// <summary>属性値取得(実数)</summary>
        /// <param name="root">検索対象となる要素</param>
        /// <param name="name">検索属性名</param>
        /// <returns>int値 変換された属性値。取得できない場合は、0 を返す。</returns>
		public double GetAttrDouble( XElement root, string name )
		{
			double result = 0;
			string text = GetAttrText( root, name );
			if( false == string.IsNullOrEmpty( text ) ) {
				if( false == double.TryParse( text, out result ) ) {
					int temp = 0;
					if( false == int.TryParse( text, out temp ) ) {
						AidLog logs = new AidLog( "AidXml.AttrDouble" );
						logs.Error( string.Format( System.Globalization.CultureInfo.InvariantCulture, "Int32.TryParse(HEX,{0})", text ) );
						result = 0.0;
					} else {
						result = temp;
					}
				}
			}
			return result;
		}
        #endregion

        #region 属性値の設定
        /// <summary>属性値の設定</summary>
        /// <param name="xe">対象となる要素</param>
        /// <param name="name">設定対象となる属性名</param>
        /// <param name="val">設定値</param>
        /// <returns>変更の有無
        ///     <list type="bullet" >
        ///         <item>true=値に変更あり</item>
        ///         <item>false=値に変更なし</item>
        ///     </list>
        /// </returns>
        public bool SetAttr( XElement xe, string name, string val )
		{
			XAttribute attr = xe.Attribute( name );
			if( null != attr ) {
				if( 0 != string.Compare( attr.Value, val, false ) ) {
					attr.Value = val;
					return true;
				}
			} else {
				xe.Add( new XAttribute( name, $"{val}" ) );
			}
			return false;
		}
		#endregion
	}
}
