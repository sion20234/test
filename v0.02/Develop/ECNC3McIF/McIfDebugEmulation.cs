using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECNC3.Enumeration;
using ECNC3.Models.Common;
using static Rt64ecdata;

namespace ECNC3.Models.McIf
{
	/// <summary>机上デバッグ用MCボードエミュレーション関数群</summary>
	public class McIfDebugEmulation : McCommBasic, IDisposable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>コンストラクタ</summary>
		public McIfDebugEmulation()
		{
		}
		/// <summary>アラーム種別</summary>
		public enum AlarmKinds
		{
			/// <summary>全体</summary>
			Main,
			/// <summary>軸</summary>
			Axis,
			/// <summary>タスク</summary>
			Task,
			/// <summary>ECNC</summary>
			Ecnc,
			/// <summary>不明</summary>
			Unknown = -1,
		}
		/// <summary>インスタンスの破棄</summary>
		public new void Dispose()
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
		protected override void Dispose( bool disposing )
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
				//  基底クラスのDispose()を確実に呼び出す。
				base.Dispose( disposing );
			}
		}
		/// <summary>スターボタン押下信号変更コマンド発行</summary>
		/// <param name="state">設定値</param>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// プロパティに設定された内容によりMCボードへコマンドを発行します。
		/// </remarks>
		public ResultCodes SignalStartButton( bool state )
		{
			if( true == StandBy() ) {
				if( false == BootMode.HasFlag( BootModes.Desktop ) ) {
					return ResultCodes.NotSupported;
				}
				DataType = 0x4001;
				DataTypeName = "REQ_USER_STARTBUTTON";
				return ExecuteByEnable( state, null, null );
			}
			return ResultCodes.McNotInitialize;
		}
		/// <summary>シーケンス完了信号変更コマンド発行</summary>
		/// <param name="state">設定値</param>
		/// <returns>実行結果</returns>
		public ResultCodes SignalCompletedSequence( bool state )
		{
			if( true == StandBy() ) {
				if( false == BootMode.HasFlag( BootModes.Desktop ) ) {
					return ResultCodes.NotSupported;
				}
				DataType = 0x4002;
				DataTypeName = "REQ_USER_CMPLSEQ";
				return ExecuteByEnable( state, null, null );
			}
			return ResultCodes.McNotInitialize;
		}
		/// <summary>FG完了信号変更コマンド発行</summary>
		/// <param name="state">設定値</param>
		/// <returns>実行結果</returns>
		public ResultCodes SignalCompletedFg( bool state )
		{
			if( true == StandBy() ) {
				if( false == BootMode.HasFlag( BootModes.Desktop ) ) {
					return ResultCodes.NotSupported;
				}
				DataType = 0x4003;
				DataTypeName = "REQ_USER_CMPLFG";
				return ExecuteByEnable( state, null, null );
			}
			return ResultCodes.McNotInitialize;
		}

		/// <summary>返送要求信号変更コマンド発行</summary>
		/// <param name="state">設定値</param>
		/// <returns>実行結果</returns>
		public ResultCodes SignalSendingBack( bool state )
		{
			if( true == StandBy() ) {
				if( false == BootMode.HasFlag( BootModes.Desktop ) ) {
					return ResultCodes.NotSupported;
				}
				DataType = 0x4005;
				DataTypeName = "REQ_USER_SENDBACK";
				return ExecuteByEnable( state, null, null );
			}
			return ResultCodes.McNotInitialize;
		}
		/// <summary>メッセージ表示要求信号変更コマンド発行</summary>
		/// <param name="state">設定値</param>
		/// <returns>実行結果</returns>
		public ResultCodes SignalShowMessage( bool state )
		{
			if( true == StandBy() ) {
				if( false == BootMode.HasFlag( BootModes.Desktop ) ) {
					return ResultCodes.NotSupported;
				}
				DataType = 0x4006;
				DataTypeName = "REQ_USER_MESSAGEREQ";
				return ExecuteByEnable( state, null, null );
			}
			return ResultCodes.McNotInitialize;
		}
		/// <summary>I/O信号設定</summary>
		/// <param name="address">アドレス</param>
		/// <param name="mask">マスク値</param>
		/// <param name="value">設定値</param>
		/// <returns>実行結果</returns>
		public ResultCodes SignalIOData( ushort address, ushort mask, ushort value )
		{
			AidLog logs = new AidLog( "McIfDebugEmulation.SignalIOData" );
			ResultCodes ret = ResultCodes.McNotInitialize;
			while( true == StandBy() ) {
				if( false == BootMode.HasFlag( BootModes.Desktop ) ) {
					return ResultCodes.NotSupported;
				}
				try {
					DataType = 0x4007;
					DataTypeName = "REQ_USER_IODATA";
					VIRPOSCHG data = VIRPOSCHG.Init();
					data.VirPos[0] = address;
					data.VirPos[1] = mask;
					data.VirPos[2] = value;

					int retRt64 = 0;
					TechnoMethods method = TechnoMethods.SendCommand;
					logs.Sure( $"SendCommand({DataTypeName},VirNo={data.VirNo},[" + logs.ToString( data.VirPos ) + "]" );
					retRt64 = Rt64eccomapiWrap.SendCommand( CommHandle, DataType, Task, ref data );
					ret = CheckResultDebug( method, retRt64 );
					if( ResultCodes.Success != ret ) {
						break;
					}
				} catch( Exception e ) {
					bool unexpected = true;
					if( ( e is DllNotFoundException ) ||
						( e is EntryPointNotFoundException ) ) {
						unexpected = false;   //	想定内の例外。
					}
					ret = logs.Exception( e, unexpected );
				}
				break;
			}
			return ret;
		}
		/// <summary>アラーム種別</summary>
		public enum AlarmCategories
		{
			/// <summary>アラーム2</summary>
			Alarm2,
			/// <summary>アラーム3</summary>
			Alarm3,
			/// <summary>アラーム4</summary>
			Alarm4,
			/// <summary>アラーム5</summary>
			Alarm5,
			/// <summary>EtherCAT1</summary>
			EtherCAT1,
			/// <summary>EtherCAT2</summary>
			EtherCAT2,
			/// <summary>未使用</summary>
			NotUse = 0,
		}
		/// <summary>アラーム信号の設定</summary>
		/// <param name="type">アラーム種別</param>
		/// <param name="taskNumber">タスク番号</param>
		/// <param name="mask">マスク値</param>
		/// <param name="value">設定値</param>
		/// <returns>実行結果</returns>
		public ResultCodes SignalAlarm( AlarmKinds type, int taskNumber, int mask, bool value )
		{
			return SignalAlarm( type, (ushort)taskNumber, mask, value );
		}
		/// <summary>アラーム信号の設定</summary>
		/// <param name="type">アラーム種別</param>
		/// <param name="axis">タスク番号</param>
		/// <param name="mask">マスク値</param>
		/// <param name="value">設定値</param>
		/// <returns>実行結果</returns>
		public ResultCodes SignalAlarm( AlarmKinds type, AxisNumbers axis, int mask, bool value )
		{
			return SignalAlarm( type, (ushort)axis, mask, value );
		}
		/// <summary>アラーム信号の設定</summary>
		/// <param name="type">アラーム種別</param>
		/// <param name="category">カテゴリ</param>
		/// <param name="mask">マスク値</param>
		/// <param name="value">設定値</param>
		/// <returns>実行結果</returns>
		public ResultCodes SignalAlarm( AlarmKinds type, AlarmCategories category, int mask, bool value )
		{
			return SignalAlarm( type, (ushort)category, mask, value );
		}
		/// <summary>アラーム信号の設定</summary>
		/// <param name="type">アラーム種別</param>
		/// <param name="category">カテゴリ／軸／タスク番号</param>
		/// <param name="mask">マスク値</param>
		/// <param name="value">設定値</param>
		/// <returns>実行結果</returns>
		private ResultCodes SignalAlarm( AlarmKinds type, ushort category, int mask, bool value )
		{
			AidLog logs = new AidLog( "McIfDebugEmulation.SignalAlarm" );
			ResultCodes ret = ResultCodes.McNotInitialize;
			while( true == StandBy() ) {
				if( false == BootMode.HasFlag( BootModes.Desktop ) ) {
					return ResultCodes.NotSupported;
				}
				try {
					DataType = 0x4008;
					DataTypeName = "REQ_USER_STATUS_ALARM";
					VIRPOSCHG data = VIRPOSCHG.Init();
					data.VirPos[0] = (int)type;
					data.VirPos[1] = category;
					data.VirPos[2] = mask;
					data.VirPos[3] = true == value ? 1 : 0;

					int retRt64 = 0;
					TechnoMethods method = TechnoMethods.SendCommand;
					logs.Sure( $"SendCommand({DataTypeName},VirNo={data.VirNo},[" + logs.ToString( data.VirPos ) + "]" );
					retRt64 = Rt64eccomapiWrap.SendCommand( CommHandle, DataType, Task, ref data );
					ret = CheckResultDebug( method, retRt64 );
					if( ResultCodes.Success != ret ) {
						break;
					}
				} catch( Exception e ) {
					bool unexpected = true;
					if( ( e is DllNotFoundException ) ||
						( e is EntryPointNotFoundException ) ) {
						unexpected = false;   //	想定内の例外。
					}
					ret = logs.Exception( e, unexpected );
				}
				break;
			}
			return ret;
		}
	}
}
