using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using ECNC3.Enumeration;
using ECNC3.Models.Common;

namespace ECNC3.Models
{
	/// <summary>仮想原点ファイル解析</summary>
	public class FileOrgPos : FileAccessCommon, IEcnc3Backup, IDisposable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>コンストラクタ</summary>
		public FileOrgPos()
		{
			Name = this.ToString();
			RegistMasterFile( @"OrgPos.xml" );
		}
		/// <summary>座標リスト</summary>
		public StructureAxisCoordinateList Items { get; set; }

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
				Items = new StructureAxisCoordinateList();
			}
			AidLog logs = new AidLog( "FileOrgPos.Load" );
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
							using( StructureAxisCoordinate item = new StructureAxisCoordinate() ) {
								item.Number = xml.GetAttrValue( xe, "num" );
								item.Axis1 = xml.GetAttrValue( xe, "ax1" );
								item.Axis2 = xml.GetAttrValue( xe, "ax2" );
								item.Axis3 = xml.GetAttrValue( xe, "ax3" );
								item.Axis4 = xml.GetAttrValue( xe, "ax4" );
								item.Axis5 = xml.GetAttrValue( xe, "ax5" );
								item.Axis6 = xml.GetAttrValue( xe, "ax6" );
								item.Axis7 = xml.GetAttrValue( xe, "ax7" );
								item.Axis8 = xml.GetAttrValue( xe, "ax8" );
								item.Axis9 = xml.GetAttrValue( xe, "ax9" );
                                item.Protect = xml.GetAttrValue(xe, "Protect");
								Items.Add( item.Clone() as StructureAxisCoordinate );
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
		/// <summary>ファイル書き込み</summary>
		/// <param name="target">書き換え情報</param>
		/// <returns>実行結果</returns>
		public ResultCodes Write( StructureAxisCoordinate target )
		{
			AidLog logs = new AidLog( "FileOrgPos.Write" );

			AidXmlLinq xml = new AidXmlLinq();
			XElement file = null;
			ResultCodes ret = xml.ReadByReadWrite( ref file, FilePath );
			if( ( ResultCodes.Success == ret ) && ( null != file ) ) {
				try {
					IEnumerable<XElement> list = file.Elements( "Item" );
					if( null == list ) {
						ret = ResultCodes.FailToReadFile;
					} else {
						if( true == SetData( list, target ) ) {
							xml.Write( file, FilePath );
						} else {
							ret = ResultCodes.InvalidArgument;
						}
					}
				} catch( Exception e ) {
					ret = logs.Exception( e, false );
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
			AidLog logs = new AidLog( "FileOrgPos.Write" );
			AidXmlLinq xml = new AidXmlLinq();
			XElement file = null;
			ResultCodes ret = xml.ReadByReadWrite( ref file, FilePath );
			if( ( ResultCodes.Success == ret ) && ( null != file ) ) {
				try {
					xml.UpdateProfile( file );
					IEnumerable<XElement> list = file.Elements( "Item" );
					if( null == list ) {
						ret = ResultCodes.FailToReadFile;
					} else {
						foreach( StructureAxisCoordinate target in Items ) {
							if( false == SetData( list, target ) ) {
								;	//	該当なし
							}
						}
					}
					xml.Write( file, FilePath );
				} catch( Exception e ) {
					ret = logs.Exception( e, false );
				}
			}
			return ret;
		}
		/// <summary>データ設定</summary>
		/// <param name="list">編集対象のXMLノードリスト</param>
		/// <param name="target">設定値</param>
		/// <returns>該当の有無</returns>
		/// <remarks>
		/// 引数target の該当を引数listから検索し、設定値を反映します。
		/// </remarks>
		private bool SetData( IEnumerable<XElement> list, StructureAxisCoordinate target )
		{
			AidXmlLinq xml = new AidXmlLinq();
			int number;
			foreach( XElement xe in list ) {
				number = xml.GetAttrValue( xe, "num" );
				if( target.Number != number ) {
					continue;
				}
				xml.SetAttr( xe, "ax1", $"{target.Axis1}" );
				xml.SetAttr( xe, "ax2", $"{target.Axis2}" );
				xml.SetAttr( xe, "ax3", $"{target.Axis3}" );
				xml.SetAttr( xe, "ax4", $"{target.Axis4}" );
				xml.SetAttr( xe, "ax5", $"{target.Axis5}" );
				xml.SetAttr( xe, "ax6", $"{target.Axis6}" );
				xml.SetAttr( xe, "ax7", $"{target.Axis7}" );
				xml.SetAttr( xe, "ax8", $"{target.Axis8}" );
				xml.SetAttr( xe, "ax9", $"{target.Axis9}" );
                xml.SetAttr(xe, "Protect", $"{target.Protect}");
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
