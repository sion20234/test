using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using ECNC3.Enumeration;
using ECNC3.Models.Common;
using System.Linq;

namespace ECNC3.Models
{
	/// <summary>加工条件ファイルデータアクセス</summary>
	public class FileMacroManage : FileAccessCommon, IDisposable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>コンストラクタ</summary>
		public FileMacroManage(string fileName)
		{
			Name = this.ToString();
            RegistMacroFile(@fileName + ".VAR");

		}
		/// <summary>加工条件配列</summary>
		public StructureMacroManageList Items { get; set; }

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
				Items = new StructureMacroManageList();
			}
			AidXmlLinq xml = new AidXmlLinq();
			AidLog logs = new AidLog("FileMacroManage.Read");
			XElement file = null;
			ResultCodes ret = xml.ReadByReadOnly( ref file, FilePath );
			if( ( ResultCodes.Success == ret ) && ( null != file ) ) {
				//int addr = 0;
				//int value = 0;
				try {
					IEnumerable<XElement> list = file.Elements( "Item" );
					foreach( XElement xe in list ) {
                        StructureMacroManageItem data = new StructureMacroManageItem();
						data.Number = xml.GetAttrValue( xe, "num" );
                        data.Comment = xml.GetAttrText( xe, "comment");
						data.Value = xml.GetAttrText( xe, "value" );
						data.Protect = xml.GetAttrValue( xe, "protect" );
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
        /// <summary>読み込み</summary>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// ECNC3のパラメータのファイルの読み込みを行います。
		/// </remarks>
		public ResultCodes Read(string filePath)
        {
            if (null != Items)
            {
                Items.Clear();
            }
            else
            {
                Items = new StructureMacroManageList();
            }
            AidXmlLinq xml = new AidXmlLinq();
            AidLog logs = new AidLog("FileMacroManage.Read");
            XElement file = null;
            ResultCodes ret = xml.ReadByReadOnly(ref file, filePath);
            if ((ResultCodes.Success == ret) && (null != file))
            {
                try
                {
                    IEnumerable<XElement> list = file.Elements("Item");
                    foreach (XElement xe in list)
                    {
                        StructureMacroManageItem data = new StructureMacroManageItem();
                        data.Number = xml.GetAttrValue(xe, "num");
                        data.Comment = xml.GetAttrText(xe, "comment");
                        data.Value = xml.GetAttrText(xe, "value");
                        data.Protect = xml.GetAttrValue(xe, "protect");
                        Items.Add(data);
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
                    ret = logs.Exception(e, unexpected);
                }
            }

            return ret;
        }
        /// <summary>ファイル保存</summary>
        /// <returns>実行結果</returns>
        /// <remarks>
        /// 現在ロードされている情報をファイルに永続化します。
        /// 事前のロードが実施されていない場合、未設定な部分は初期化されることになります。
        /// </remarks>
        public ResultCodes Write()
        {
            AidLog logs = new AidLog("FileMacroManage.Write");
            AidXmlLinq xml = new AidXmlLinq();
            XElement file = null;
            ResultCodes ret = xml.ReadByReadWrite(ref file, FilePath);
            if ((ResultCodes.Success == ret) && (null != file))
            {
                try
                {
                    xml.UpdateProfile(file);
                    IEnumerable<XElement> list = file.Elements("Item");
                    if (null == list)
                    {
                        ret = ResultCodes.FailToReadFile;
                    }
                    else
                    {
                        foreach (var target in list.Select((xe, ct) => new { xe, ct }))
                        {
                            xml.SetAttr(target.xe, "comment", $"{Items[target.ct].Comment}");
                            xml.SetAttr(target.xe, "value", $"{Items[target.ct].Value}");
                            xml.SetAttr(target.xe, "protect", $"{Items[target.ct].Protect}");
                        }
                    }
                    xml.Write(file, FilePath);
                }
                catch (Exception e)
                {
                    ret = logs.Exception(e, false);
                }
            }
            return ret;
        }

        /// <summary>ファイル保存</summary>
        /// <returns>実行結果</returns>
        /// <remarks>
        /// 現在ロードされている情報をファイルに永続化します。
        /// 事前のロードが実施されていない場合、未設定な部分は初期化されることになります。
        /// </remarks>
        public ResultCodes CreateFile()
        {
            Items.Clear();
            Items.TrimExcess();
            for (int ct = 10000; ct <= 99999; ct++)
            {
                StructureMacroManageItem data = new StructureMacroManageItem();
                data.Number = ct;
                data.Comment = "";
                data.Value ="";
                data.Protect = 0;
                Items.Add(data);
            }

            ResultCodes ret = ResultCodes.Success;

            AidLog logs = new AidLog("FileMacroManage.Write");
            
            XElement file = new XElement("macro");
                try
                {
                    foreach (StructureMacroManageItem target in Items)
                    {
                        XElement xe = new XElement("Item");
                        SetAttr(xe, "num", $"{target.Number}");
                        SetAttr(xe, "comment", $"{target.Comment}");
                        SetAttr(xe, "value", $"{target.Value}");
                        SetAttr(xe, "protect", $"{target.Protect}");
                        file.Add(xe);
                    }
                    Write(file, FilePath);
                }
                catch (Exception e)
                {
                    ret = logs.Exception(e, false);
                }
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
		public ResultCodes Write(XElement root, string path)
        {
            AidLog logs = new AidLog("AidXml.WriteElement");
            ResultCodes ret = ResultCodes.Success;
            if (null == root)
            {
                return ResultCodes.InvalidArgument;
            }
                try
                {
                    ret = ResultCodes.Success;
                    using (new AidMutex(path))
                    {
                        using (FileStream fs = new FileStream(path, FileMode.Create))
                        {
                            root.Save(fs);
                        }
                    }
                }
                catch (IOException e)
                {
                    ret = logs.Exception(e, false);
                }
                catch (Exception e)
                {
                    bool unexpected = true;
                    if ((e is ObjectDisposedException) ||
                        (e is System.Threading.AbandonedMutexException) ||
                        (e is DirectoryNotFoundException) ||
                        (e is PathTooLongException) ||
                        (e is ArgumentOutOfRangeException) ||
                        (e is ArgumentNullException) ||
                        (e is ArgumentException) ||
                        (e is NotSupportedException) ||
                        (e is FileNotFoundException) ||
                        (e is System.Security.SecurityException) ||
                        (e is UnauthorizedAccessException))
                    {
                        unexpected = false; //	想定内の例外。
                    }
                    ret = logs.Exception(e, unexpected);
                }
            return ret;
        }
        /// <summary>ファイル保存</summary>
        /// <param name="target">書き換え対象のインスタンス</param>
        /// <returns>実行結果</returns>
        /// <remarks>
        /// 引数 target で指定された加工条件の書き換えを行います。
        /// </remarks>
        public ResultCodes Write(StructureMacroManageItem target )
		{
			AidLog logs = new AidLog("FileMacroManage.Write");
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
		private bool EditToNode( IEnumerable<XElement> list, StructureMacroManageItem item )
		{
			AidXmlLinq xml = new AidXmlLinq();
			int number;
			foreach( XElement xe in list )
            {
				number = xml.GetAttrValue( xe, "num" );
				if( item.Number != number )
                {
					continue;
				}
                xml.SetAttr(xe, "comment", $"{item.Comment}");
                xml.SetAttr( xe, "value", $"{item.Value}" );
                xml.SetAttr( xe, "protect", $"{item.Protect}" );
                
                //XElement cmt = xe.Element("comment");
                //if (null != cmt)
                //{
                //    if (null != item.Comment)
                //    {
                //        cmt.Value = item.Comment;
                //    }
                //    else
                //    {
                //        cmt.Value = string.Empty;
                //    }
                //}
                //else
                //{
                //    xe.Add(new XElement("comment", item.Comment));
                //}
				return true;
			}
			return false;
		}
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
        public bool SetAttr(XElement xe, string name, string val)
        {
            XAttribute attr = xe.Attribute(name);
            if (null != attr)
            {
                if (0 != string.Compare(attr.Value, val, false))
                {
                    attr.Value = val;
                    return true;
                }
            }
            else
            {
                xe.Add(new XAttribute(name, $"{val}"));
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
