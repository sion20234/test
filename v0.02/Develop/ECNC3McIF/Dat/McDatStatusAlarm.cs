using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECNC3.Enumeration;
using ECNC3.Models.Common;

namespace ECNC3.Models.McIf
{
	/// <summary>アラームステータス判定</summary>
	/// <remarks>
	/// DAT_STATUSコマンドより取得される各アラーム情報の解析を行い、変化を履歴します。
	/// </remarks>
	public class McDatStatusAlarm : IDisposable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>保持されたスタータス情報</summary>
		private StructureMcDatStatus _datStatus = null;
		/// <summary>コンストラクタ</summary>
		public McDatStatusAlarm()
		{
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
						if( null != _datStatus ) {
							_datStatus.Dispose();
							_datStatus = null;
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

		/// <summary>アラーム状態変化チェック</summary>
		/// <param name="target">変化を確認すべきステータス情報</param>
		/// <returns>
		///		<list type="bullet" >
		///			<item>true=ログ出力あり</item>
		///			<item>false=ログ出力なし</item>
		///		</list>
		/// </returns>
		public bool Execute( StructureMcDatStatus target )
		{
			bool updated = false;
			if( null != target ) {
				if( null == _datStatus ) {
					_datStatus = new StructureMcDatStatus();
				}
				if( null != _datStatus ) {
					using( StructureAlarmLogList list = new StructureAlarmLogList() ) {
						CheckAlarmBit checker = new CheckAlarmBit();
						//	MAIN
						checker.Reset();
						checker.AlarmKind = AlarmLogKinds.AlarmMain;
						checker.BeforeStatus = _datStatus.Main.Alarm;
						checker.AfterStatus = target.Main.Alarm;
						checker.CheckLength = 16;
						checker.BaseNumber = 6100;
						checker.Execute( list );
						//	軸				
						int axis = 0;
						checker.Reset();
						checker.AlarmKind = AlarmLogKinds.AlarmAxis;
						checker.CheckLength = 16;
						checker.BaseNumber = 6150;
						for( axis = 0 ; axis < (int)AxisNumbers.I ; ++axis ) {
							checker.BeforeStatus = _datStatus.Axes[axis].AxAlarm;
							checker.AfterStatus = target.Axes[axis].AxAlarm;
							checker.AxisNumber = (AxisNumbers)axis;
							checker.Execute( list );
						}
						//	タスク
						int task = 0;
						checker.Reset();
						checker.AlarmKind = AlarmLogKinds.AlarmTask;
						checker.CheckLength = 32;
						checker.BaseNumber = 6200;
						for( task = 0 ; task < 8 ; ++task ) {
							checker.BeforeStatus = _datStatus.Tasks[task].TaskAlarm;
							checker.AfterStatus = target.Tasks[task].TaskAlarm;
							checker.TaskNumber = task;
							checker.Execute( list );
						}
						//	ECNC
						checker.Reset();
						checker.AlarmKind = AlarmLogKinds.AlarmEcnc;
						{
							//	ECNC.Alarm2
							checker.BeforeStatus = _datStatus.Ecnc.Alarm2;
							checker.AfterStatus = target.Ecnc.Alarm2;
							checker.CheckLength = 16;
							checker.BaseNumber = 6250;
							checker.Execute( list );
							//	ECNC.Alarm3
							checker.BeforeStatus = _datStatus.Ecnc.Alarm3;
							checker.AfterStatus = target.Ecnc.Alarm3;
							checker.CheckLength = 16;
							checker.BaseNumber = 6300;
							checker.Execute( list );
							//	ECNC.Alarm4
							checker.BeforeStatus = _datStatus.Ecnc.Alarm4;
							checker.AfterStatus = target.Ecnc.Alarm4;
							checker.CheckLength = 16;
							checker.BaseNumber = 6350;
							checker.Execute( list );
							//	ECNC.Alarm5
							checker.BeforeStatus = _datStatus.Ecnc.Alarm5;
							checker.AfterStatus = target.Ecnc.Alarm5;
							checker.CheckLength = 16;
							checker.BaseNumber = 6400;
							checker.Execute( list );
							//	ECNC.EtherCAT1
							checker.BeforeStatus = _datStatus.Ecnc.EIFErr1;
							checker.AfterStatus = target.Ecnc.EIFErr1;
							checker.CheckLength = 32;
							checker.BaseNumber = 6450;
							checker.Execute( list );
							//	ECNC.EtherCAT2
							checker.BeforeStatus = _datStatus.Ecnc.EIFErr2;
							checker.AfterStatus = target.Ecnc.EIFErr2;
							checker.CheckLength = 32;
							checker.BaseNumber = 6500;
							checker.Execute( list );
						}
						if( 0 < list.Count ) {
							using( FileAlarmLog fa = new FileAlarmLog() ) {
								fa.Write( list );
								updated = true;
							}
						}
					}
				}
				//	今回値を保持
				_datStatus.Copy( target );
			}
			return updated;
		}
	}
	/// <summary>アラーム変数ビットチェック</summary>
	internal class CheckAlarmBit
	{
		/// <summary>コンストラクタ</summary>
		public CheckAlarmBit()
		{
		}
		/// <summary>ログ出力種別</summary>
		public AlarmLogKinds AlarmKind { get; set; }
		/// <summary>基準値</summary>
		public int BeforeStatus { get; set; }
		/// <summary>比較値</summary>
		public int AfterStatus { get; set; }
		/// <summary>アラーム番号基準値</summary>
		public int BaseNumber { get; set; }
		/// <summary>チェックするビット長</summary>
		public int CheckLength { get; set; } = 16;
		/// <summary>軸番号</summary>
		public AxisNumbers AxisNumber { get; set; } = AxisNumbers.Unknown;
		/// <summary>タスク番号</summary>
		public int TaskNumber { get; set; } = -1;

		/// <summary>チェック実行</summary>
		/// <param name="list">履歴すべき情報</param>
		public void Execute( StructureAlarmLogList list )
		{
			if( BeforeStatus != AfterStatus ) {
				AidLog logs = new AidLog( "CheckAlarmBit.Execute" );
				int mask;
				int shift;
				bool occur;
				int number;
				for( shift = 0 ; shift < CheckLength ; ++shift ) {
					mask = 0x0001 << shift;
					if( true == HasEdge( BeforeStatus, AfterStatus, mask ) ) {
						occur = IsOnEdge( BeforeStatus, AfterStatus, mask );
						number = BaseNumber + shift;
						logs.Sure( $"ALM,{AlarmKind},#{number},{AxisNumber},TASK({TaskNumber}),Occur({occur})" );
						if( true == occur ) {
							//	発生のみを出力する。
							list.Add( new StructureAlarmLogItem() {
								Number = number,
								Occur = IsOnEdge( BeforeStatus, AfterStatus, mask ),
								Kind = this.AlarmKind,
								AxisNumber = this.AxisNumber,
								TaskNumber = this.TaskNumber,
							} );
						}
					}
				}
			}
		}
		/// <summary>クラスメンバ変数リセット</summary>
		public void Reset()
		{
			AlarmKind = AlarmLogKinds.Unknown;
			BeforeStatus = 0;
			AfterStatus = 0;
			BaseNumber = 0;
			CheckLength = 16;
			AxisNumber = AxisNumbers.Unknown;
			TaskNumber = -1;
		}
		/// <summary>変化の確認</summary>
		/// <param name="before">直前値</param>
		/// <param name="after">現在値</param>
		/// <param name="mask">マスク値</param>
		/// <returns>判定結果
		///		<list type="bullet" >
		///			<item>true=変化あり</item>
		///			<item>false=変化なし</item>
		///		</list>
		/// </returns>
		private bool HasEdge( int before, int after, int mask )
		{
			return ( ( before & mask ) != ( after & mask ) ) ? true : false;
		}
		/// <summary>オンエッジ判定</summary>
		/// <param name="before">直前値</param>
		/// <param name="after">現在値</param>
		/// <param name="mask">マスク値</param>
		/// <returns>
		///		<list type="bullet" >
		///			<item>true=オンエッジ検出</item>
		///			<item>false=オンエッジなし</item>
		///		</list>
		/// </returns>
		private bool IsOnEdge( int before, int after, int mask )
		{
			if( ( before & mask ) != ( after & mask ) ) {
				if( mask == ( after & mask ) ) {
					return true;
				}
			}
			return false;
		}
	}
}
