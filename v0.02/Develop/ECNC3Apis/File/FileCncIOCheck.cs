using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using ECNC3.Enumeration;
using ECNC3.Models.Common;

namespace ECNC3.Models
{
	/// <summary>I／O定義ファイルアクセス</summary>
	public class FileCncIOCheck : FileAccessCommon, IEcnc3Backup, IDisposable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>コンストラクタ</summary>
		public FileCncIOCheck()
		{
			Name = this.ToString();
			RegistMasterFile( @"CncIO.xml" );
		}
		/// <summary>Ｉ／Ｏ定義リスト</summary>
		public StructureIODataList Items { get; set; }
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
		/// <remarks>
		/// ECNC3のパラメータのファイルの読み込みを行います。
		/// </remarks>
		public ResultCodes Read()
		{
			if( null != Items ) {
				Items.Clear();
			} else {
				Items = new StructureIODataList();
			}
			AidLog logs = new AidLog( "FileCncIOCheck.Load" );
			AidXmlLinq xml = new AidXmlLinq();
			XElement file = null;

			ResultCodes ret = xml.ReadByReadOnly( ref file, FilePath );
			if( ( ResultCodes.Success == ret ) && ( null != file ) ) {
				int bit = 0;
				try {
					IEnumerable<XElement> list = file.Elements( "Item" );
					foreach( XElement xe in list ) {
						using( StructureIODataItem item = new StructureIODataItem() ) {
							item.AccessTarget = (IOAccessTargets)xml.GetAttrValue( xe, "tgt" );
							item.Address = (ushort)xml.GetAttrValue( xe, "addr" );
							bit = xml.GetAttrValue( xe, "bit" );
							item.Mask = (ushort)( 0x01 << bit );
							item.Name = xml.GetAttrText( xe, "nam" );
							item.Note = xml.GetAttrText( xe, "note" );
							//logs.Sure( $"{item.AccessTarget}[{item.Address:X2}.{item.Mask:X1}]={item.Name}" );
							Items.Add( item.Clone() as StructureIODataItem );
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
						unexpected = false;
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
	}
}
