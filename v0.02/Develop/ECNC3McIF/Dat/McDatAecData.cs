using System;
using System.Runtime.InteropServices;
using ECNC3.Enumeration;
using ECNC3.Models.Common;
using static Rt64ecdata;

namespace ECNC3.Models.McIf
{
	/// <summary>4-4-4.AEC状態読み出し</summary>
	public class McDatAecData : McCommBasic, IEcnc3McDatReadOnly, IDisposable
    {
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>MCインターフェース構造体</summary>
		private AECDATA _data;
		/// <summary>パーティション設定</summary>
		private StructurePartitions _partitions;
		/// <summary>コンストラクタ</summary>
		public McDatAecData()
		{
			ClassName = GetType().Name;
			DataType = Syncdef.DAT_AECDATA;
			DataTypeName = "DAT_AECDATA";
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
						if( null != _partitions ) {
							_partitions.Dispose();
							_partitions = null;
						}
					}
					//  アンマネージリソースの解放
				}
				_disposed = true;
			} finally {
				//  基底クラスのDispose()を確実に呼び出す。
				base.Dispose( disposing );
			}
		}
		/// <summary>パーティション設定 有効／無効</summary>
		public bool PartitionEnabled { get { return ( 0 != _data.PartitionDis ) ? false : true; } }
		/// <summary>パーティション設定</summary>
		public StructurePartitions Partitions
		{
			get
			{
				if( null == _partitions ) {
					_partitions = new StructurePartitions();
					_partitions.Enabled = PartitionEnabled;
					int index = 0;
					for( index = 0 ; ( index < _data.PartitionS.Length ) && ( index < _data.PartitionE.Length ) && ( index < _data.Thinline.Length ) ; ++index ) {
						using( StructurePartitionItem item = new StructurePartitionItem() ) {
							item.Number = index + 1;
							item.IndexStart = _data.PartitionS[index];
							item.IndexEnd = _data.PartitionE[index];
							item.Thinline = ( 0 != _data.Thinline[index] ) ? true : false;
							_partitions.Items.Add( item.Clone() as StructurePartitionItem );
						}
					}
				}
				return _partitions;
			}
		}
		/// <summary>電極番号	 (0:未設定)</summary>
		public short ElectrodeNo { get { return _data.ElectrodeNo; } }
		/// <summary>ガイド番号(0:未設定)</summary>
		public short GuideNo { get { return _data.GuideNo; } }
		/// <summary>インデックス番号有効フラグ</summary>
		public short IndexZrnFin { get { return _data.IndexZrnFin; } }
		/// <summary>インデックス番号</summary>
		public short IndexNo { get { return _data.IndexNo; } }

		/// <summary>ガイド番号取得</summary>
		public short GuideNumber { get { return GuideNo; } }
		/// <summary>電極番号取得</summary>
		public short ElectrodeNumber { get { return ElectrodeNo; } }
		/// <summary>インデックス番号</summary>
		public short IndexNumber { get { return IndexNo; } }

		/// <summary>読み込み</summary>
		/// <returns>実行結果</returns>
		public ResultCodes Read()
		{
			if( null != _partitions ) {
				_partitions.Dispose();
				_partitions = null;
			}
			return ReadData( out _data );
		}
		/// <summary>MCデータ読み込み</summary>
		/// <param name="data">MCより読み込まれたデータ</param>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// MCより情報を取得します。
		/// </remarks>
		private ResultCodes ReadData( out AECDATA data )
		{
			AidLog logs = new AidLog( "McDatAecData.ReadData" );
			ResultCodes ret = ResultCodes.McNotInitialize;
			data = AECDATA.Init();
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
	}
}
