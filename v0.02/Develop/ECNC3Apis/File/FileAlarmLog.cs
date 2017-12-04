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
	/// <summary>アラーム履歴ファイルアクセス</summary>
	public class FileAlarmLog : FileAccessCommon, IDisposable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>コンストラクタ</summary>
		public FileAlarmLog(string alarmFileName = "")
		{
			Name = this.ToString();
			if( alarmFileName != "" ) {
				//引数にファイル名が有る場合、こちらを使用
				RegistLogFile( alarmFileName );
			} else {
				RegistLogFile( @"CncAlarm.xml" );
			}
		}

		/// <summary>加工条件配列</summary>
		public StructureAlarmLogList Items { get; set; }
		/// <summary>登録上限件数</summary>
		public int LimitCount { get; set; } = 100000;//最大ログ行数：到達した場合、古いのから削除

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
			AidXmlLinq xml = new AidXmlLinq();
			AidLog logs = new AidLog( "FileAlarmLog.Read" );
			XElement file = null;
			ResultCodes ret = xml.ReadByReadOnly( ref file, FilePath );
			if( ( ResultCodes.Success == ret ) && ( null != file ) ) {
				ret = Read( file );
			}
			return ret;
		}
		/// <summary>読み取り(XML要素解析)</summary>
		/// <param name="file">解析対象のルート</param>
		/// <returns>実行結果</returns>
		private ResultCodes Read( XElement file )
		{
			if( null != Items ) {
				Items.Clear();
			} else {
				Items = new StructureAlarmLogList();
			}
			AidLog logs = new AidLog( "FileAlarmLog.Read.2" );
			ResultCodes ret = ResultCodes.Success;
			try {
				IEnumerable<XElement> list = file.Elements( "Item" );
				foreach( XElement xe in list ) {
					StructureAlarmLogItem data = new StructureAlarmLogItem();
					data.Read( xe );
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
			return ret;
		}
		/// <summary>書き込み</summary>
		/// <returns>未サポート</returns>
		/// <remarks>
		/// Item ノードを全削除して再作成します。
		/// </remarks>
		public ResultCodes Write()
		{
			AidLog logs = new AidLog( "FileAlarmLog.Write" );
			AidXmlLinq xml = new AidXmlLinq();
			XElement file = null;
			ResultCodes ret = ResultCodes.Success;
			if( false == ExistsFile() ) {
				file = new XElement( "Root" );
			} else {
				ret = xml.ReadByReadWrite( ref file, FilePath );
				IEnumerable<XElement> list = file.Elements( "Item" );
				if( null != list ) {
					//	XML要素削除
					list.Remove();
					try {
						xml.UpdateProfile( file );
						//	要素追加
						int index = 0;
						for( index = 0 ; index < Items.Count ; ++index ) {
							file.Add( Items[index].MakeElement() );
						}
						ret = xml.Write( file, FilePath );
					} catch( Exception e ) {
						ret = logs.Exception( e, false );
					}
				}
			}
			return ret;
		}
		/// <summary>ファイル保存</summary>
		/// <param name="target">書き換え対象のインスタンス</param>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// 引数 target の情報をファイルに追記します。
		/// </remarks>
		public ResultCodes Write( StructureAlarmLogItem target )
		{
			using( StructureAlarmLogList list = new StructureAlarmLogList() ) {
				list.Add( target );
				return Write( list );
			}
		}
		/// <summary>ファイル保存</summary>
		/// <param name="target">書き換え対象のインスタンス</param>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// 引数 target の情報をファイルに追記します。
		/// </remarks>
		public ResultCodes Write( StructureAlarmLogList target )
		{
			AidLog logs = new AidLog( "FileAlarmLog.Write" );
			AidXmlLinq xml = new AidXmlLinq();
			XElement file = null;
			ResultCodes ret = ResultCodes.Success;
			if( false == ExistsFile() ) {
				file = new XElement( "Root" );
			} else {
				ret = xml.ReadByReadWrite( ref file, FilePath );
			}
			//	プロファイルを更新
			xml.UpdateProfile( file );
			//	読み込み
			ret = Read( file );
			if( ResultCodes.Success != ret ) {
				return ret;
			}
			//	登録上限を確認
			if( LimitCount < ( Items.Count + target.Count ) ) {
				//	登録制限超過につき、古いデータから削除して、データを再作成する。

				//	まず登録すべきデータを追加。
				Items.AddRange( target );
				//	発生日時で降順並べ替え
				Items.Sort( ( a, b ) => a.Time.CompareTo( b.Time ) );
				Items.Reverse();

				int index = 0;
				for( index = Items.Count - 1 ; LimitCount <= index ; --index ) {
					Items[index].Dispose();
					Items.RemoveAt( index );
				}
				return Write();
			}
			
			if( ( ResultCodes.Success == ret ) && ( null != file ) ) {
				try {
					xml.UpdateProfile( file );
					//	要素追加
					foreach( StructureAlarmLogItem item in target ) {
						file.Add( item.MakeElement() );
					}
					ret = xml.Write( file, FilePath );
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
				} else {
					xe.Add( new XElement( "Ext",
					new XAttribute( "reg", $"{item.HasExtended}" ),
					new XAttribute( "mat", $"{item.Material}" ),
					new XAttribute( "dia", $"{item.Diameter}" ) ) );
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
