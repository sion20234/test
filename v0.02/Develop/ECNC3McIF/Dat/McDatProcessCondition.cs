using System;
using System.Runtime.InteropServices;
using ECNC3.Enumeration;
using ECNC3.Models.Common;
using static Rt64ecdata;

namespace ECNC3.Models.McIf
{
	/// <summary>4-4-3.加工条件情報読出</summary>
	public class McDatProcessCondition : McCommBasic, IEcnc3McDatReadOnly, IDisposable
    {
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>直前の取得結果</summary>
		/// <remarks>
		/// 連続で取得しなければ効果はありません。
		/// </remarks>
		private PCONDITION _dataBefore;
		/// <summary>取得結果</summary>
		private PCONDITION _dataLatest;

		/// <summary>コンストラクタ</summary>
		public McDatProcessCondition()
		{
			ClassName = GetType().Name;
			DataType = Syncdef.DAT_PCONDITION;
			DataTypeName = "DAT_PCONDITION";
			_dataBefore = PCONDITION.Init();
			_dataLatest = PCONDITION.Init();
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

		/// <summary>各種状態情報</summary>
		public short Status { get { return _dataLatest.Status; } }
		/// <summary>加工条件番号</summary>
		/// <remarks>
		/// 現在カレントとなっている加工条件番号
		/// </remarks>
		public short PNo { get { return _dataLatest.PNo; } }
		/// <summary>Ｚ軸上昇速度</summary>
		public int ZUpFeed { get { return _dataLatest.ZUpFeed; } }
		/// <summary>Ｚ軸下降速度</summary>
		public int ZDwFeed { get { return _dataLatest.ZDwFeed; } }
		/// <summary>主軸送り速度</summary>
		public short CFeed { get { return _dataLatest.CFeed; } }
		/// <summary>主軸回転(0:停止,1:CW,2:CCW)</summary>
		public short SpinOutValue { get { return _dataLatest.SpinOut; } }
		/// <summary>ポンプ出力(0:OFF,1:ON)</summary>
		public short PumpOutValue { get { return _dataLatest.PumpOut; } }
		/// <summary>放電加工時主軸速度(加工条件)</summary>
		public short PrCFeed { get { return _dataLatest.PrCFeed; } }
		/// <summary>Ｃ軸主軸速度(0.1rpm)</summary>
		public int CFeedRpm { get { return _dataLatest.CFeedRpm; } }
		/// <summary>ドライラン有効フラグ(放電加工穴数)</summary>
		public int DryRunEnN { get { return _dataLatest.DryRunEnN; } }

		/// <summary>返送要求</summary>
		public bool RequestSendingBack { get { return HasFlag( Status, PCS_RETURN ); } }
		/// <summary>放電中</summary>
		public bool Discharge { get { return HasFlag( Status, PCS_BON ); } }
		/// <summary>ドライラン有効</summary>
		public bool DryRun { get { return HasFlag( Status, PCS_DRYRUN ); } }
		/// <summary>イニシャルセット有効</summary>
		public bool InitialSet { get { return HasFlag( Status, PCS_INITIALSET ); } }
		/// <summary>電極消耗時の自動電極交換有効</summary>
		public bool AecByLife { get { return HasFlag( Status, PCS_AEC ); } }
		/// <summary>パーティション内一周停止有効</summary>
		public bool PartitionRoundStop { get { return HasFlag( Status, PCS_PATRNDSTPEN ); } }
		/// <summary>細線設定有効</summary>
		public bool Thinline { get { return HasFlag( Status, PCS_THINLINE ); } }
		/// <summary>主軸回転速度変更要求</summary>
		public bool RequestChangeCFeed { get { return HasFlag( Status, PCS_CFEED ); } }
		/// <summary>パーティション内一周停止中</summary>
		public bool PartitionRoundStopping { get { return HasFlag( Status, PCS_PATRNDSTPIN ); } }
		/// <summary>ガイド貫通動作許可要求</summary>
		public bool RequestPermitGuideThrough { get { return HasFlag( Status, PCS_GUIDETHROUGH ); } }
		/// <summary>１穴加工中</summary>
		public bool Processing { get { return HasFlag( Status, PCS_IN_DISCHARGE ); } }
		/// <summary>手動電極交換要求</summary>
		public bool RequestManualExchangeElectrode { get { return HasFlag( Status, PCS_MAN_AEC ); } }
		/// <summary>イニシャルセット済み</summary>
		public bool HasBeenInitialSet { get { return HasFlag( Status, PCS_ISET_FIN ); } }

		/// <summary>主軸回転(0:停止,1:CW,2:CCW)</summary>
		public SpinStates SpinOut { get { return (SpinStates)SpinOutValue; } }
		/// <summary>ポンプ出力(0:OFF,1:ON)</summary>
		public bool PumpOut { get { return ( 0 != PumpOutValue ) ? true : false; } }

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
		/// <summary>読み込み</summary>
		/// <returns>実行結果</returns>
		public ResultCodes Read()
		{
			PCONDITION data;
			ResultCodes ret = ReadData( out data );
			if( ResultCodes.Success == ret ) {
				Copy( _dataLatest, ref _dataBefore );
				Copy( data, ref _dataLatest );
			}
			return ret;
		}
		/// <summary>MCデータ読み込み</summary>
		/// <param name="data">MCより読み込まれたデータ</param>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// MCより情報を取得します。
		/// </remarks>
		private ResultCodes ReadData( out PCONDITION data )
		{
			AidLog logs = new AidLog( "McDatProcessCondition.ReadData" );
			ResultCodes ret = ResultCodes.McNotInitialize;
			data = PCONDITION.Init();
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

		/// <summary>配列のコピー</summary>
		/// <param name="source">コピー元</param>
		/// <param name="target">コピー先</param>
		private void Copy( PCONDITION source, ref PCONDITION target )
		{
			target.Status = source.Status;
			target.PNo = source.PNo;
			target.ZUpFeed = source.ZUpFeed;
			target.ZDwFeed = source.ZDwFeed;
			target.CFeed = source.CFeed;
			target.SpinOut = source.SpinOut;
			target.PumpOut = source.PumpOut;
			target.PrCFeed = source.PrCFeed;
			target.CFeedRpm = source.CFeedRpm;
			target.DryRunEnN = source.DryRunEnN;
			Copy( source.Reserved, ref target.Reserved );
		}
		/// <summary>配列の比較</summary>
		/// <param name="source">比較元</param>
		/// <param name="target">比較先</param>
		/// <returns>比較結果
		///		<list type="bullet" >
		///			<item>true=一致</item>
		///			<item>false=不一致</item>
		///		</list>
		/// </returns>
		/// <remarks>
		///	sbyte配列の比較を行います。
		///	両方とも null のケースは一致と判断します。
		/// </remarks>
		private bool Equals( PCONDITION source, PCONDITION target )
		{
			if( ( target.Status != source.Status ) ||
				( target.PNo != source.PNo ) ||
				( target.ZUpFeed != source.ZUpFeed ) ||
				( target.ZDwFeed != source.ZDwFeed ) ||
				( target.CFeed != source.CFeed ) ||
				( target.SpinOut != source.SpinOut ) ||
				( target.PumpOut != source.PumpOut ) ||
				( target.PrCFeed != source.PrCFeed ) ||
				( target.CFeedRpm != source.CFeedRpm ) ||
				( target.DryRunEnN != source.DryRunEnN ) ||
				( false == Equals( source.Reserved, target.Reserved ) ) ) {
				return false;
			}
			return true;
		}
	}
}
