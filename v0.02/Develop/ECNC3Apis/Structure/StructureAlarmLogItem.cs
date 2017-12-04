using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ECNC3.Enumeration;
using ECNC3.Models.Common;

namespace ECNC3.Models
{
	/// <summary>アラーム履歴情報</summary>
	public class StructureAlarmLogItem : IDisposable, ICloneable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>コンストラクタ</summary>
		public StructureAlarmLogItem()
		{
		}
		/// <summary>発生日時</summary>
		public DateTime Time { get; set; } = DateTime.Now;
		/// <summary>アラーム種別</summary>
		public AlarmLogKinds Kind { get; set; } = AlarmLogKinds.Unknown;
		/// <summary>軸番号</summary>
		public AxisNumbers AxisNumber { get; set; } = AxisNumbers.Unknown;
		/// <summary>タスク番号</summary>
		public int TaskNumber { get; set; } = -1;

		/// <summary>発生方向</summary>
		/// <value>
		///		<list type="bullet" >
		///			<item>true=発生</item>
		///			<item>false=回復</item>
		///		</list>
		/// </value>
		public bool Occur { get; set; }
		/// <summary>アラーム番号</summary>
		public int Number { get; set; }
		/// <summary>値</summary>
		public string Text { get; set; }
		/// <summary>詳細情報 </summary>
		public string Comment { get; set; }
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
		protected virtual void Dispose( bool disposing )
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
		/// <summary>クローン</summary>
		/// <returns>クローンされたインスタンス</returns>
		public object Clone()
		{
			StructureAlarmLogItem temp = new StructureAlarmLogItem();
			temp.Time = Time;
			temp.Number = Number;
			temp.Occur = Occur;
			temp.Text = Text;
			return temp;
		}
		///	<summary>XML要素読み込み</summary>
		/// <param name="root">読み込み対象となるXML要素</param>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// 引数のXML要素の解析を行います。
		/// </remarks>
		public ResultCodes Read( XElement root )
		{
			AidXmlLinq xml = new AidXmlLinq();
			DateTime dt;
			TryParse( xml.GetAttrText( root, "tim" ), out dt );
			Time = dt;
			Number = xml.GetAttrValue( root, "num" );
			Occur = xml.GetAttrBool( root, "ocr" );
			int kind = xml.GetAttrValue( root, "kind" );
			Kind = (AlarmLogKinds)kind;
			if( AlarmLogKinds.AlarmAxis == Kind ) {
				AxisNumber = (AxisNumbers)xml.GetAttrValue( root, "axis" );
				TaskNumber = 0;
			} else if( AlarmLogKinds.AlarmTask == Kind ) {
				AxisNumber = AxisNumbers.Unknown;
				TaskNumber = xml.GetAttrValue( root, "task" );
			}
			Text = xml.GetAttrText( root, "txt" );
			return ResultCodes.Success;
		}
		/// <summary>要素の作成</summary>
		/// <remarks>
		/// クラス構造に沿ったXML要素を作成します。
		/// </remarks>
		/// <returns>生成された要素</returns>
		public XElement MakeElement()
		{
			XElement node = new XElement( "Item",
				new XAttribute( "tim", Time.ToString( "yyyy/MM/dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture ) ),
				new XAttribute( "kind", $"{(int)Kind}" ) );
			if( true == IsAlarm( Kind ) ) {
				node.Add( new XAttribute( "num", $"{Number}" ),
						new XAttribute( "ocr", ( true == Occur ) ? "1" : "0" ) );
				if( AlarmLogKinds.AlarmAxis == Kind ) {
					node.Add( new XAttribute( "axis", $"{(int)AxisNumber}" ) );
				} else if( AlarmLogKinds.AlarmTask == Kind ) {
					node.Add( new XAttribute( "task", $"{TaskNumber}" ) );
				}
			}
			if( false == string.IsNullOrEmpty( Text ) ) {
				node.Add( new XAttribute( "txt", Text ) );
			}
			return node;
		}
		/// <summary>ログ出力種別アラーム判定</summary>
		/// <param name="target">判定対象</param>
		/// <returns>
		///		<list type="bullet" >
		///			<item>true=アラームである</item>
		///			<item>false=アラームでない。</item>
		///		</list>
		/// </returns>
		private bool IsAlarm( AlarmLogKinds target )
		{
			if(
				( AlarmLogKinds.AlarmMain == target ) ||
				( AlarmLogKinds.AlarmAxis == target ) ||
				( AlarmLogKinds.AlarmTask == target ) ||
				( AlarmLogKinds.AlarmEcnc == target ) ) {
				return true;
			}
			return false;
		}

		/// <summary>日付情報文字列の解析</summary>
		/// <param name="source">解析元</param>
		/// <param name="result">解析結果</param>
		/// <returns>変換結果
		///		<list type="bullet" >
		///			<item>true=成功</item>
		///			<item>false=失敗</item>
		///		</list>
		/// </returns>
		/// <remarks>
		/// 日付情報の解析を行います。
		/// DateTime.TryParseを使用しないのはINIファイル上の書式の詳細が不明であるため。
		/// </remarks>
		private bool TryParse( string source, out DateTime result )
		{
			if( ( true == source.Contains( "/" ) ) || ( true == source.Contains( ":" ) ) ) {
				return TryParseDateTime( source, out result );
			} else if( ( true == source.Contains( "H" ) ) || ( true == source.Contains( "M" ) ) || ( true == source.Contains( "S" ) ) ) {
				return TryParseTimeSpan( source, out result );
			}
			result = new DateTime();
			return false;
		}
		/// <summary>日付書式の解析</summary>
		/// <param name="source">変換元の文字列</param>
		/// <param name="result">変換された日付情報</param>
		/// <returns>変換結果
		///		<list type="bullet" >
		///			<item>true=成功</item>
		///			<item>false=失敗</item>
		///		</list>
		/// </returns>
		private bool TryParseDateTime( string source, out DateTime result )
		{
			int lenght = source.Length;

			if( 8 == lenght ) {
				//	時刻(HH:mm:ss)
				return TryParseTime( source, out result );
			} else if( 10 == lenght ) {
				//	日付(yyyy/MM/dd)
				return TryParseDate( source, out result );
			} else if( 12 == lenght ) {
				//	時刻(HH:mm:ss.fff)
				return TryParseTime( source, out result );
			} else if( ( 19 == lenght ) || ( 23 == lenght ) ) {
				DateTime date;
				DateTime time;
				string[] dt = source.Split( ' ' );
				while( 1 < dt.Length ) {
					//	yyyy/MM/dd HH:mm:ss
					if( false == TryParseDate( dt[0], out date ) ) {
						break;
					}
					if( false == TryParseTime( dt[1], out time ) ) {
						break;
					}
					result = new DateTime( date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second, time.Millisecond );
					return true;
				}
			}
			result = new DateTime();
			return false;
		}
		/// <summary>日付文字列解析</summary>
		/// <param name="source">変換元文字列</param>
		/// <param name="result">変換結果 日付情報</param>
		/// <returns>実行結果
		///		<list type="bullet" >
		///			<item>true=変換成功</item>
		///			<item>false=変換失敗</item>
		///		</list>
		/// </returns>
		private bool TryParseDate( string source, out DateTime result )
		{
			string[] splits = source.Split( '/' );
			while( 2 < splits.Length ) {
				int year = 0;
				int month = 0;
				int day = 0;
				if( false == int.TryParse( splits[0].Trim(), out year ) ) {
					break;
				}
				if( false == int.TryParse( splits[1].Trim(), out month ) ) {
					break;
				}
				if( false == int.TryParse( splits[2].Trim(), out day ) ) {
					break;
				}
				result = new DateTime( year, month, day );
				return true;
			}
			result = new DateTime();
			return false;
		}
		/// <summary>時刻文字列解析</summary>
		/// <param name="source">変換元文字列</param>
		/// <param name="result">変換結果 時刻情報</param>
		/// <returns>実行結果
		///		<list type="bullet" >
		///			<item>true=変換成功</item>
		///			<item>false=変換失敗</item>
		///		</list>
		/// </returns>
		private bool TryParseTime( string source, out DateTime result )
		{
			string[] splits = source.Split( ':' );
			while( 2 < splits.Length ) {
				int hour = 0;
				int minute = 0;
				int second = 0;
				int msec = 0;
				if( false == int.TryParse( splits[0].Trim(), out hour ) ) {
					break;
				}
				if( false == int.TryParse( splits[1].Trim(), out minute ) ) {
					break;
				}
				if( true == splits[2].Contains( "." ) ) {
					string[] sec = splits[2].Split( '.' );
					if( false == int.TryParse( sec[0].Trim(), out second ) ) {
						break;
					}
					if( false == int.TryParse( sec[1].Trim(), out msec ) ) {
						break;
					}
				} else {
					if( false == int.TryParse( splits[2].Trim(), out second ) ) {
						break;
					}
				}
				result = new DateTime( 1, 1, 1, hour, minute, second );
				return true;
			}
			result = new DateTime();
			return false;
		}
		/// <summary>経過時間の解析</summary>
		/// <param name="source">変換元の文字列</param>
		/// <param name="result">変換された日付情報</param>
		/// <returns>変換結果
		///		<list type="bullet" >
		///			<item>true=成功</item>
		///			<item>false=失敗</item>
		///		</list>
		/// </returns>
		private bool TryParseTimeSpan( string source, out DateTime result )
		{
			int hour = 0;
			int minute = 0;
			int sec = 0;
			//	経過時間
			string[] splits1 = source.Split( 'H' );
			while( 1 < splits1.Length ) {
				if( false == int.TryParse( splits1[0].Trim(), out hour ) ) {
					break;
				}
				string[] splits2 = splits1[1].Split( 'M' );
				while( 1 < splits2.Length ) {
					if( false == int.TryParse( splits2[0].Trim(), out minute ) ) {
						break;
					}
					string[] splits3 = splits2[1].Split( 'S' );
					if( false == int.TryParse( splits3[0].Trim(), out sec ) ) {
						break;
					}
					result = new DateTime( new TimeSpan( hour, minute, sec ).Ticks );
					return true;
				}
				break;
			}
			result = new DateTime();
			return false;
		}
	}
}
