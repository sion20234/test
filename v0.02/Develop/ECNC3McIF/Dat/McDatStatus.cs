using System;
using System.Runtime.InteropServices;
using ECNC3.Enumeration;
using ECNC3.Models.Common;
using static Rt64ecdata;

namespace ECNC3.Models.McIf
{
	/// <summary>4-4-2.ステータスデータ読み出し</summary>
	public class McDatStatus : McCommBasic, IEcnc3McDatReadOnly, IDisposable
    {
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>直前の取得結果</summary>
		/// <remarks>
		/// 連続で取得しなければ効果はありません。
		/// </remarks>
		private STATUS _dataBefore;
		/// <summary>取得結果</summary>
		private STATUS _dataLatest;
		/// <summary>ステータス情報格納領域</summary>
		public StructureMcDatStatus Status { get; private set; }

		/// <summary>コンストラクタ</summary>
		public McDatStatus()
		{
			ClassName = GetType().Name;
			DataType = Syncdef.DAT_STATUS;
			DataTypeName = "DAT_STATUS";
			_dataBefore = STATUS.Init();
			_dataLatest = STATUS.Init();
			Status = new StructureMcDatStatus();
		}
		/// <summary>直前と等しい</summary>
		/// <value>
		///		<list type="bullet" >
		///			<item>true=直前のReadメソッドの取得値と等しい。</item>
		///			<item>false=等しくない。</item>
		///		</list>
		/// </value>
		/// <remarks>
		/// 直前のReadメソッドの呼び出しに対して値が相違を確認します。
		/// 一回目の呼び出しは、前回値が存在しないので基本的にfalse=を返すことになります。
		/// </remarks>
		public bool EqualToPrevious { get { return Equals( _dataBefore, _dataLatest ); } }

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

		/// <summary>実行</summary>
		/// <returns>実行結果</returns>
		public ResultCodes Read()
		{
			STATUS data;
			ResultCodes ret = ReadData( out data );
			if( ResultCodes.Success == ret ) {
				Copy( _dataLatest, ref _dataBefore );
				Copy( data, ref _dataLatest );
				if( null == Status ) {
					Status = new StructureMcDatStatus();
				}
				Copy( data, Status );
			}
			return ret;
		}
		/// <summary>MCデータ読み込み</summary>
		/// <param name="data">MCより読み込まれたデータ</param>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// MCより情報を取得します。
		/// </remarks>
		private ResultCodes ReadData( out STATUS data )
		{
			AidLog logs = new AidLog( "McDatStatus.ReadData" );
			ResultCodes ret = ResultCodes.McNotInitialize;
			data = STATUS.Init();
			while( true == StandBy() ) {
				try {
					int size = Marshal.SizeOf( data );
					int retRt64 = 0;
					TechnoMethods method = TechnoMethods.ReceiveData;
					if( true == BootMode.HasFlag( BootModes.Machine ) ) {
						retRt64 = Rt64eccomapi.ReceiveData( CommHandle, DataType, Task, NonParameter, ref size, ref data );
						ret = CheckResultTechno( method, retRt64 );
						if( ResultCodes.Success != ret ) {
							break;
						}
					}
					if( true == BootMode.HasFlag( BootModes.Desktop ) ) {
						retRt64 = Rt64eccomapiWrap.ReceiveData( CommHandle, DataType, Task, NonParameter, ref size, ref data );
						ret = CheckResultDebug( method, retRt64 );
						if( ResultCodes.Success != ret ) {
							break;
						}
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

		#region コピー(構造体)
		/// <summary>構造体コピー</summary>
		/// <param name="source">コピー元</param>
		/// <param name="target">コピー先</param>
		/// <remarks>
		/// 構造体のコピーを行います。
		/// コピー先のメモリ領域の拡縮は行いません。コピー先の領域にあわせてコピーを行います。
		/// </remarks>
		private void Copy( MCSTATUS source, ref MCSTATUS target )
		{
			target.Status = source.Status;
			target.Alarm = source.Alarm;
			Copy( source.Reserved, ref target.Reserved );
		}
		/// <summary>構造体コピー</summary>
		/// <param name="source">コピー元</param>
		/// <param name="target">コピー先</param>
		/// <remarks>
		/// 構造体のコピーを行います。
		/// コピー先のメモリ領域の拡縮は行いません。コピー先の領域にあわせてコピーを行います。
		/// </remarks>
		private void Copy( AXSTATUS source, ref AXSTATUS target )
		{
			target.AxStatus = source.AxStatus;
			target.AxAlarm = source.AxAlarm;
			target.ComReg = source.ComReg;
			target.PosReg = source.PosReg;
			target.ErrReg = source.ErrReg;
			target.BlockSeg = source.BlockSeg;
			target.AbsReg = source.AbsReg;
			target.Trq = source.Trq;
			target.AMrReg = source.AMrReg;
			Copy( source.Reserved, ref target.Reserved );
		}
		/// <summary>構造体コピー</summary>
		/// <param name="source">コピー元</param>
		/// <param name="target">コピー先</param>
		/// <remarks>
		/// 構造体のコピーを行います。
		/// コピー先のメモリ領域の拡縮は行いません。コピー先の領域にあわせてコピーを行います。
		/// </remarks>
		private void Copy( TASKSTATUS source, ref TASKSTATUS target )
		{
			target.TaskStatus = source.TaskStatus;
			target.TaskAlarm = source.TaskAlarm;
			target.Override = source.Override;
			target.COverride = source.COverride;
			target.SOverride = source.SOverride;
			target.ProgramNo = source.ProgramNo;
			target.StepNo = source.StepNo;
			target.NNo = source.NNo;
			target.LineNo = source.LineNo;
			target.LineFlg = source.LineFlg;
			Copy( source.Reserved, ref target.Reserved );
		}
		/// <summary>構造体コピー</summary>
		/// <param name="source">コピー元</param>
		/// <param name="target">コピー先</param>
		/// <remarks>
		/// 構造体のコピーを行います。
		/// コピー先のメモリ領域の拡縮は行いません。コピー先の領域にあわせてコピーを行います。
		/// </remarks>
		private void Copy( ECNCSTATUS source, ref ECNCSTATUS target )
		{
			target.Status2 = source.Status2;
			target.Status3 = source.Status3;
			target.Alarm2 = source.Alarm2;
			target.Alarm3 = source.Alarm3;
			target.Alarm4 = source.Alarm4;
			target.Alarm5 = source.Alarm5;
			Copy( source.Reserved0, ref target.Reserved0 );
			target.WTopPos = source.WTopPos;
            target.PrcsTargetDist = source.PrcsTargetDist;
            target.PrcsNowDist = source.PrcsNowDist;
			target.CorrAng = source.CorrAng;
			Copy( source.ADVal, ref target.ADVal );
			Copy( source.Reserved1, ref target.Reserved1 );
		}
		/// <summary>構造体コピー</summary>
		/// <param name="source">コピー元</param>
		/// <param name="target">コピー先</param>
		/// <remarks>
		/// 構造体のコピーを行います。
		/// コピー先のメモリ領域の拡縮は行いません。コピー先の領域にあわせてコピーを行います。
		/// </remarks>
		private void Copy( STATUS source, ref STATUS target )
		{
			Copy( source.mc, ref target.mc );
			int index = 0;
			if( ( null != source.ax ) && ( null != target.ax ) ) {
				for( index = 0 ; ( index < source.ax.Length ) && ( index < target.ax.Length ) ; ++index ) {
					Copy( source.ax[index], ref target.ax[index] );
				}
			}
			if( ( null != source.task ) && ( null != target.task ) ) {
				for( index = 0 ; ( index < source.task.Length ) && ( index < target.task.Length ) ; ++index ) {
					Copy( source.task[index], ref target.task[index] );
				}
			}
			Copy( source.ecnc, ref target.ecnc );
		}
		#endregion

		#region コピー(UI)
		/// <summary>コピー</summary>
		/// <param name="source">コピー元</param>
		/// <param name="target">コピー先</param>
		private void Copy( MCSTATUS source, StructureMcDatStatusMc target )
		{
			target.Status = source.Status;                  // 全体ステータス
			target.Alarm = source.Alarm;                   // 全体アラーム
		}
		/// <summary>コピー</summary>
		/// <param name="source">コピー元</param>
		/// <param name="target">コピー先</param>
		private void Copy( AXSTATUS source, StructureMcDatStatusAxis target )
		{
			target.AxStatus = source.AxStatus;
			target.AxAlarm = source.AxAlarm;
			target.ComReg = source.ComReg;
			target.PosReg = source.PosReg;
			target.ErrReg = source.ErrReg;
			target.BlockSeg = source.BlockSeg;
			target.AbsReg = source.AbsReg;
			target.Trq = source.Trq;
			target.AMrReg = source.AMrReg;
		}
		/// <summary>コピー</summary>
		/// <param name="source">コピー元</param>
		/// <param name="target">コピー先</param>
		private void Copy( TASKSTATUS source, StructureMcDatStatusTask target )
		{
			target.TaskStatus = source.TaskStatus;
			target.TaskAlarm = source.TaskAlarm;
			target.Override = source.Override;
			target.COverride = source.COverride;
			target.SOverride = source.SOverride;
			target.ProgramNo = source.ProgramNo;
			target.StepNo = source.StepNo;
			target.NNo = source.NNo;
			target.LineNo = source.LineNo;
			target.LineFlg = source.LineFlg;
		}
		/// <summary>コピー</summary>
		/// <param name="source">コピー元</param>
		/// <param name="target">コピー先</param>
		private void Copy( ECNCSTATUS source, StructureMcDatStatusEcnc target )
		{
			target.Status2 = source.Status2;
			target.Status3 = source.Status3;
			target.Alarm2 = source.Alarm2;
			target.Alarm3 = source.Alarm3;
			target.Alarm4 = source.Alarm4;
			target.Alarm5 = source.Alarm5;
			target.WTopPos = source.WTopPos;
            target.PrcsTargetDist = source.PrcsTargetDist;
            target.PrcsNowDist = source.PrcsNowDist;
            target.CorrAng = source.CorrAng;
			int index = 0;
			if( ( null != source.ADVal ) && ( null != target.ADVal ) ) {
				for( index = 0 ; ( index < source.ADVal.Length ) && ( index < target.ADVal.Length ) ; ++index ) {
					target.ADVal[index] = source.ADVal[index];
				}
			}
		}
		/// <summary>コピー</summary>
		/// <param name="source">コピー元</param>
		/// <param name="target">コピー先</param>
		private void Copy( STATUS source, StructureMcDatStatus target )
		{
			if( null != target ) {
				Copy( source.mc, target.Main );
				int index = 0;
				if( ( null != source.ax ) && ( null != target.Axes ) ) {
					for( index = 0 ; ( index < source.ax.Length ) && ( index < target.Axes.Length ) ; ++index ) {
						Copy( source.ax[index], target.Axes[index] );
					}
				}
				if( ( null != source.task ) && ( null != target.Tasks ) ) {
					for( index = 0 ; ( index < source.task.Length ) && ( index < target.Tasks.Length ) ; ++index ) {
						Copy( source.task[index], target.Tasks[index] );
					}
				}
				Copy( source.ecnc, target.Ecnc );
			}
		}
		#endregion
		
		#region 構造体比較
		/// <summary>構造体比較</summary>
		/// <param name="source">比較元</param>
		/// <param name="target">比較先</param>
		/// <returns>比較結果
		///		<list type="bullet" >
		///			<item>true=一致</item>
		///			<item>false=不一致</item>
		///		</list>
		/// </returns>
		/// <remarks>
		/// 構造体の比較を行います。
		/// </remarks>
		private bool Equals( MCSTATUS source, MCSTATUS target )
		{
			if( ( target.Status != source.Status ) ||
				( target.Alarm != source.Alarm ) ) {
				return false;
			}
			return Equals( source.Reserved, target.Reserved );
		}
		/// <summary>構造体比較</summary>
		/// <param name="source">コピー元</param>
		/// <param name="target">コピー先</param>
		/// <returns>比較結果
		///		<list type="bullet" >
		///			<item>true=一致</item>
		///			<item>false=不一致</item>
		///		</list>
		/// </returns>
		/// <remarks>
		/// 構造体のコピーを行います。
		/// コピー先のメモリ領域の拡縮は行いません。コピー先の領域にあわせてコピーを行います。
		/// </remarks>
		private bool Equals( AXSTATUS source, AXSTATUS target )
		{
			if( ( target.AxStatus != source.AxStatus ) ||
				( target.AxAlarm != source.AxAlarm ) ||
				( target.ComReg != source.ComReg ) ||
				( target.PosReg != source.PosReg ) ||
				( target.ErrReg != source.ErrReg ) ||
				( target.BlockSeg != source.BlockSeg ) ||
				( target.AbsReg != source.AbsReg ) ||
				( target.Trq != source.Trq ) ||
				( target.AMrReg != source.AMrReg ) ||
				( false == Equals( source.Reserved, target.Reserved ) ) ) {
				return false;
			}
			return true;
		}
		/// <summary>構造体比較</summary>
		/// <param name="source">コピー元</param>
		/// <param name="target">コピー先</param>
		/// <returns>比較結果
		///		<list type="bullet" >
		///			<item>true=一致</item>
		///			<item>false=不一致</item>
		///		</list>
		/// </returns>
		/// <remarks>
		/// 構造体のコピーを行います。
		/// コピー先のメモリ領域の拡縮は行いません。コピー先の領域にあわせてコピーを行います。
		/// </remarks>
		private bool Equals( TASKSTATUS source, TASKSTATUS target )
		{
			if( ( target.TaskStatus != source.TaskStatus ) ||
				( target.TaskAlarm != source.TaskAlarm ) ||
				( target.Override != source.Override ) ||
				( target.COverride != source.COverride ) ||
				( target.SOverride != source.SOverride ) ||
				( target.ProgramNo != source.ProgramNo ) ||
				( target.StepNo != source.StepNo ) ||
				( target.NNo != source.NNo ) ||
				( target.LineNo != source.LineNo ) ||
				( target.LineFlg != source.LineFlg ) ||
				( false == Equals( source.Reserved, target.Reserved ) ) ) {
				return false;
			}
			return true;
		}
		/// <summary>構造体比較</summary>
		/// <param name="source">コピー元</param>
		/// <param name="target">コピー先</param>
		/// <returns>比較結果
		///		<list type="bullet" >
		///			<item>true=一致</item>
		///			<item>false=不一致</item>
		///		</list>
		/// </returns>
		/// <remarks>
		/// 構造体のコピーを行います。
		/// コピー先のメモリ領域の拡縮は行いません。コピー先の領域にあわせてコピーを行います。
		/// </remarks>
		private bool Equals( ECNCSTATUS source, ECNCSTATUS target )
		{
			if( ( target.Status2 != source.Status2 ) ||
				( target.Status3 != source.Status3 ) ||
				( target.Alarm2 != source.Alarm2 ) ||
				( target.Alarm3 != source.Alarm3 ) ||
				( target.Alarm4 != source.Alarm4 ) ||
				( target.Alarm5 != source.Alarm5 ) ||
				( false == Equals( source.Reserved0, target.Reserved0 ) ) ||
				( target.WTopPos != source.WTopPos ) ||
				( target.CorrAng != source.CorrAng ) ||
                (target.PrcsTargetDist != source.PrcsTargetDist) ||
                (target.PrcsNowDist != source.PrcsNowDist) ||
                ( false == Equals( source.ADVal, target.ADVal ) ) ||
				( false == Equals( source.Reserved1, target.Reserved1 ) ) ) {
				return false;
			}
			return true;
		}
		/// <summary>構造体比較</summary>
		/// <param name="source">コピー元</param>
		/// <param name="target">コピー先</param>
		/// <returns>比較結果
		///		<list type="bullet" >
		///			<item>true=一致</item>
		///			<item>false=不一致</item>
		///		</list>
		/// </returns>
		/// <remarks>
		/// 構造体のコピーを行います。
		/// コピー先のメモリ領域の拡縮は行いません。コピー先の領域にあわせてコピーを行います。
		/// </remarks>
		private bool Equals( STATUS source, STATUS target )
		{
			while( true ) {
				if( false == Equals( source.mc, target.mc ) ) {
					break;
				}
				int index = 0;
				if( ( null != source.ax ) && ( null != target.ax ) ) {
					if( source.ax.Length != target.ax.Length ) {
						break;
					}
					for( index = 0 ; ( index < source.ax.Length ) && ( index < target.ax.Length ) ; ++index ) {
						if( false == Equals( source.ax[index], target.ax[index] ) ) {
							return false;
						}
					}
				} else {
					if( ( null != source.ax ) || ( null != target.ax ) ) {
						break;
					}
				}
				if( ( null != source.task ) && ( null != target.task ) ) {
					if( source.task.Length != target.task.Length ) {
						break;
					}
					for( index = 0 ; ( index < source.task.Length ) && ( index < target.task.Length ) ; ++index ) {
						if( false == Equals( source.task[index], target.task[index] ) ) {
							return false;
						}
					}
				} else {
					if( ( null != source.task ) || ( null != target.task ) ) {
						break;
					}
				}
				if( false == Equals( source.ecnc, target.ecnc ) ) {
					break;
				}
				return true;
			}
			return false;
		}
		#endregion
	}
}
