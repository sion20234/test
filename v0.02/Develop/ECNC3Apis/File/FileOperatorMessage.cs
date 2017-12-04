using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using ECNC3.Enumeration;
using ECNC3.Models.Common;

namespace ECNC3.Models
{
	/// <summary>オペレータメッセージファイル解析</summary>
	public class FileOperatorMessage : FileAccessCommon, IEcnc3Backup, IDisposable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>コンストラクタ</summary>
		public FileOperatorMessage()
		{
			Name = this.ToString();
			RegistMasterFile( @"CncMsg.xml" );
		}
		/// <summary>メッセージリスト</summary>
		public StructureOperatorMessageList Items { get; set; }

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
				;   //  基底クラスのDispose()を確実に呼び出す。
					//base.Dispose( disposing );
			}
		}
		/// <summary>読み込み</summary>
		/// <returns>実行結果</returns>
		public ResultCodes Read()
		{
			if( null != Items ) {
				Items.Clear();
			} else {
				Items = new StructureOperatorMessageList();
			}
			AidLog logs = new AidLog( "FileOperatorMessage.Load" );
			AidXmlLinq xml = new AidXmlLinq();
			XElement file = null;

			ResultCodes ret = xml.ReadByReadOnly( ref file, FilePath );
			if( ( ResultCodes.Success == ret ) && ( null != file ) ) {
				try {
					IEnumerable<XElement> list = file.Elements( "Item" );
					if( null == list ) {
						ret = ResultCodes.FailToReadFile;
					} else {
						foreach( XElement xe in list ) {
							using( StructureOperatorMessageItem item = new StructureOperatorMessageItem() ) {
								item.Number = xml.GetAttrValue( xe, "num" );
								item.Title = xml.GetAttrText( xe, "title" );
								item.Text = xml.GetAttrText( xe, "txt" );
								Items.Add( item.Clone() as StructureOperatorMessageItem );
							}
						}
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

		/// <summary>メッセージ情報取得</summary>
		/// <param name="number">メッセージ番号</param>
		/// <returns>メッセージ情報を格納したインスタンスの参照</returns>
		public StructureOperatorMessageItem Find( int number )
		{
			while( null != Items ) {
				int index = Items.FindIndex( ( x ) => x.Number == number );
				if( 0 > index ) {
					break;
				}
				return Items[index];
			}
			return null;
		}
	}
}
