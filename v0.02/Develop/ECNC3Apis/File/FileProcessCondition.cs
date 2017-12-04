using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using ECNC3.Enumeration;
using ECNC3.Models.Common;

namespace ECNC3.Models
{
	/// <summary>加工条件ファイルデータアクセス</summary>
	public class FileProcessCondition : FileAccessCommon, IEcnc3Backup, IDisposable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>コンストラクタ</summary>
		public FileProcessCondition()
		{
			Name = this.ToString();
			RegistMasterFile( @"PData.xml" );
		}
		/// <summary>加工条件配列</summary>
		public StructureProcessConditionList Items { get; set; }

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
						if( null != Items ) {
							Items.Dispose();
							Items = null;
						}
					}
					//  アンマネージリソースの解放
				}
				_disposed = true;
			} finally {
				;	//  基底クラスのDispose()を確実に呼び出す。
					//base.Dispose( disposing );
			}
		}
        /// <summary>読み込み加工条件番号指定</summary>
        /// <returns>実行結果</returns>
        /// <remarks>
        /// ECNC3のパラメータのファイルの読み込みを行います。
        /// </remarks>
        public StructureProcessConditionItem Read(int pnum)
        {
            StructureProcessConditionItem retItem = new StructureProcessConditionItem();
            AidXmlLinq xml = new AidXmlLinq();
            AidLog logs = new AidLog("FileProcessCondition.Read");
            XElement file = null;
            ResultCodes result = xml.ReadByReadOnly(ref file, FilePath);
            if ((ResultCodes.Success == result) && (null != file))
            {
                try
                {
                    IEnumerable<XElement> list = file.Elements("Item");
                    foreach (XElement xe in list)
                    {
                        if (xml.GetAttrValue(xe, "num") != pnum) continue;
                        retItem.Number = xml.GetAttrValue(xe, "num");
                        retItem.Ton = (short)xml.GetAttrValue(xe, "tOn");
                        retItem.Toff = (short)xml.GetAttrValue(xe, "tOff");
                        retItem.IPVal = xml.GetAttrValue(xe, "ip");
                        retItem.SFIPVal = xml.GetAttrValue(xe, "sfip");
                        retItem.CAPVal = xml.GetAttrValue(xe, "cap");
                        retItem.SCVal = (short)xml.GetAttrValue(xe, "sc");
                        retItem.CRSVal = (short)xml.GetAttrValue(xe, "crs");
                        retItem.SfrFrSel = xml.GetAttrValue(xe, "sfrDn");
                        retItem.SfrBkSel = xml.GetAttrValue(xe, "sfrUp");
                        retItem.PompVal = (short)xml.GetAttrValue(xe, "pomp");
                        retItem.ServoSel = (short)xml.GetAttrValue(xe, "srv");
                        retItem.PSSel = (short)xml.GetAttrValue(xe, "ps");
                        retItem.POLVal = (short)xml.GetAttrValue(xe, "pol");
                        foreach (XElement xe1 in xe.Elements("Ext"))
                        {
                            retItem.HasExtended = xml.GetAttrValue(xe1, "reg");
                            retItem.Material = xml.GetAttrValue(xe1, "mat");
                            retItem.Diameter = xml.GetAttrDouble(xe1, "dia");
                            //外部ファイルから書き込み保護設定を読込む。
                            retItem.Protect = xml.GetAttrValue(xe1, "Protect");
                        }
                        //	コメント
                        foreach (XElement xe2 in xe.Elements("Cmt"))
                        {
                            retItem.Comment = xe2.Value;
                        }
                        break;
                    }
                }
                catch (Exception e)
                {
                    bool unexpected = true;
                    if ((e is ObjectDisposedException) ||
                        (e is ArgumentOutOfRangeException) ||
                        (e is System.Threading.AbandonedMutexException) ||
                        (e is FileNotFoundException) ||
                        (e is XmlException) ||
                        (e is FormatException) ||
                        (e is NullReferenceException) ||
                        (e is IOException))
                    {
                        unexpected = false;   //	想定内の例外。
                    }
                    result = logs.Exception(e, unexpected);
                }
            }
            return retItem;
        }
        /// <summary>読み込み</summary>
        /// <returns>実行結果</returns>
        /// <remarks>
        /// ECNC3のパラメータのファイルの読み込みを行います。
        /// </remarks>
        public ResultCodes Read()
		{
			if( null != Items ) {
				Items.Clear();
			} else {
				Items = new StructureProcessConditionList();
			}
			AidXmlLinq xml = new AidXmlLinq();
			AidLog logs = new AidLog( "FileProcessCondition.Read" );
			XElement file = null;
			ResultCodes ret = xml.ReadByReadOnly( ref file, FilePath );
			if( ( ResultCodes.Success == ret ) && ( null != file ) ) {
				//int addr = 0;
				//int value = 0;
				try {
					IEnumerable<XElement> list = file.Elements( "Item" );
					foreach( XElement xe in list ) {
						StructureProcessConditionItem data = new StructureProcessConditionItem();
						data.Number = xml.GetAttrValue( xe, "num" );
						data.Ton = (short)xml.GetAttrValue( xe, "tOn" );
						data.Toff = (short)xml.GetAttrValue( xe, "tOff" );
						data.IPVal = xml.GetAttrValue( xe, "ip" );
                        data.SFIPVal = xml.GetAttrValue(xe, "sfip");
                        data.CAPVal = xml.GetAttrValue( xe, "cap" );
						data.SCVal = (short)xml.GetAttrValue( xe, "sc" );
						data.CRSVal = (short)xml.GetAttrValue( xe, "crs" );
						data.SfrFrSel = xml.GetAttrValue( xe, "sfrDn" );
						data.SfrBkSel = xml.GetAttrValue( xe, "sfrUp" );
						data.PompVal = (short)xml.GetAttrValue( xe, "pomp" );
						data.ServoSel = (short)xml.GetAttrValue( xe, "srv" );
						data.PSSel = (short)xml.GetAttrValue( xe, "ps" );
						data.POLVal = (short)xml.GetAttrValue( xe, "pol" );
						foreach( XElement xe1 in xe.Elements( "Ext" ) ) {
							data.HasExtended = xml.GetAttrValue( xe1, "reg" );
							data.Material = xml.GetAttrValue( xe1, "mat" );
							data.Diameter = xml.GetAttrDouble( xe1, "dia" );
                            //外部ファイルから書き込み保護設定を読込む。
                            data.Protect = xml.GetAttrValue(xe1, "Protect");
						}
						//	コメント
						foreach( XElement xe2 in xe.Elements( "Cmt" ) ) {
							data.Comment = xe2.Value;
						}
						Items.Add( data );
					}
				} catch( Exception e ) {
					bool unexpected = true;
					if( ( e is ObjectDisposedException ) ||
						( e is ArgumentOutOfRangeException ) ||
						( e is System.Threading.AbandonedMutexException ) ||
						( e is FileNotFoundException ) ||
						( e is XmlException ) ||
						( e is FormatException ) ||
						( e is NullReferenceException ) ||
						( e is IOException ) ) {
						unexpected = false;   //	想定内の例外。
					}
					ret = logs.Exception( e, unexpected );
				}
			}
			return ret;
		}
		/// <summary>書き込み</summary>
		/// <returns>未サポート</returns>
		/// <remarks>
		/// ファイルの全体書き込みはサポートしていません。
		/// </remarks>
		public ResultCodes Write()
		{
			return ResultCodes.NotSupported;
		}
		/// <summary>ファイル保存</summary>
		/// <param name="target">書き換え対象のインスタンス</param>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// 引数 target で指定された加工条件の書き換えを行います。
		/// </remarks>
		public ResultCodes Write( StructureProcessConditionItem target )
		{
			AidLog logs = new AidLog( "FileProcessCondition.Write" );
			AidXmlLinq xml = new AidXmlLinq();
			XElement file = null;
			ResultCodes ret = xml.ReadByReadWrite( ref file, FilePath );
			if( ( ResultCodes.Success == ret ) && ( null != file ) ) {
				try {
					xml.UpdateProfile( file );
					IEnumerable<XElement> list = file.Elements( "Item" );
					if( null != list ) {
						if( true == EditToNode( list, target ) ) {
							//	該当あり。書き換え実行
							ret = xml.Write( file, FilePath );
						}
					} else {
						ret = ResultCodes.FailToReadFile;
					}
				} catch( Exception e ) {
					ret = logs.Exception( e, false );
				}
			}
			return ret;
		}

		/// <summary>データ設定</summary>
		/// <param name="list">編集対象のXMLノードリスト</param>
		/// <param name="item">設定値</param>
		/// <returns>該当の有無</returns>
		/// <remarks>
		/// 引数 target に該当する加工条件を引数 list から検索し、設定値を反映します。
		/// </remarks>
		private bool EditToNode( IEnumerable<XElement> list, StructureProcessConditionItem item )
		{
			AidXmlLinq xml = new AidXmlLinq();
			int number;
			foreach( XElement xe in list ) {
				number = xml.GetAttrValue( xe, "num" );
				if( item.Number != number ) {
					continue;
				}
				xml.SetAttr( xe, "tOn", $"{item.Ton}" );
				xml.SetAttr( xe, "tOff", $"{item.Toff}" );
				xml.SetAttr( xe, "ip", $"{item.IPVal}" );
                xml.SetAttr(xe, "sfip", $"{item.SFIPVal}");
                xml.SetAttr( xe, "cap", $"{item.CAPVal}" );
				xml.SetAttr( xe, "sc", $"{item.SCVal}" );
				xml.SetAttr( xe, "crs", $"{item.CRSVal}" );
				xml.SetAttr( xe, "sfrDn", $"{item.SfrFrSel}" );
				xml.SetAttr( xe, "sfrUp", $"{item.SfrBkSel}" );
				xml.SetAttr( xe, "pomp", $"{item.PompVal}" );
				xml.SetAttr( xe, "srv", $"{item.ServoSel}" );
				xml.SetAttr( xe, "ps", $"{item.PSSel}" );
				xml.SetAttr( xe, "pol", $"{item.POLVal}" );
				XElement ext = xe.Element( "Ext" );
				if( null != ext ) {
					xml.SetAttr( ext, "reg", $"{item.HasExtended}" );
					xml.SetAttr( ext, "mat", $"{item.Material}" );
					xml.SetAttr( ext, "dia", $"{item.Diameter}" );
                    xml.SetAttr(ext, "Protect", $"{item.Protect}");
				} else {
					xe.Add( new XElement( "Ext",
					new XAttribute( "reg", $"{item.HasExtended}" ),
					new XAttribute( "mat", $"{item.Material}" ),
					new XAttribute( "dia", $"{item.Diameter}" ),
                    new XAttribute("Protect", $"{item.Protect}")) );

				}
				XElement cmt = xe.Element( "Cmt" );
				if( null != ext ) {
					if( null != item.Comment ) {
						cmt.Value = item.Comment;
					} else {
						cmt.Value = string.Empty;
					}
				} else {
					xe.Add( new XElement( "Cmt", item.Comment ) );
				}
				return true;
			}
			return false;
		}
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
	}
}
