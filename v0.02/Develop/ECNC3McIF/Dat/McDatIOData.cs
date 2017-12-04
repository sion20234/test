using System;
using System.Runtime.InteropServices;
using ECNC3.Enumeration;
using ECNC3.Models.Common;
using static Rt64ecdata;

namespace ECNC3.Models.McIf
{
    /// <summary>4-1-07.入出力状態データ読み出し</summary>
    /// <remarks>
    /// RTM64の汎用入出力および強制入出力の状態を読み出します。
    /// </remarks>
    public class McDatIOData : McCommBasic, IEcnc3McDatReadOnly, IDisposable
	{
		/// <summary>Ｉ／Ｏ番号のオフセット量</summary>
		/// <remarks>
		/// 「RTMC64専用レジスタ」は、仕様書の定義番号に対して1600番オフセットし、1600＝0番となります。
		/// </remarks>
		private const int IoMapOffset = -1600;
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>信号設定の直前値</summary>
		/// <remarks>
		/// 連続で取得しなければ効果はありません。
		/// </remarks>
		private IODATA _dataBefore;
		/// <summary>信号設定の現在値</summary>
		private IODATA _dataLatest;
		/// <summary>強制設定の直前値</summary>
		private IODATA _dataForceBefore;
		/// <summary>強制設定の現在値</summary>
		private IODATA _dataForceLatest;


        /// <summary>コンストラクタ</summary>
        public McDatIOData()
		{
			ClassName = GetType().Name;
			Forced = false;
			_dataBefore = IODATA.Init();
			_dataLatest = IODATA.Init();
			_dataForceBefore = IODATA.Init();
			_dataForceLatest = IODATA.Init();

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
		public bool EqualToPrevious
		{
			get
			{
				return ( ( false == Equals( _dataBefore, _dataLatest ) )
					  || ( false == Equals( _dataForceBefore, _dataForceLatest ) ) ) ? false : true;
			}
		}

		/// <summary>Ｉ／０情報リスト</summary>
		public StructureIODataList Items { get; private set; }

		/// <summary>モード切替</summary>
		/// <value>
		///		<list type="bullet" >
		///			<item>true=強制入出力</item>
		///			<item>false=汎用入出力</item>
		///		</list>
		/// </value>
		public bool Forced
		{
			set
			{
				if( false == value ) {
					DataType = Syncdef.DAT_IODATA;
					DataTypeName = "DAT_IODATA";
				} else {
					DataType = Syncdef.DAT_FORCEIO;
					DataTypeName = "DAT_FORCEIO";
				}
			}
		}
		/// <summary>スタートキー状態</summary>
		/// <remarks>
		/// i#1652 D05
		/// </remarks>
		public bool StartButton { get { return IsTrue( IOAccessTargets.Input, 1652, 0x0020 ); } }
		/// <summary>ガイドホルダークランプ状態</summary>
		/// <remarks>
		/// i#1608 D10 PS SW3(HLDR) 圧力SW3(ホルダ圧)
		/// </remarks>
		public bool GuideClamp { get { return IsTrue( IOAccessTargets.Input, 1608, 0x0400 ); } }
		/// <summary>コレットクランプ状態</summary>
		/// <remarks>
		/// i#1608 D00 SPN CLAMP コレットクランプ確認
		/// </remarks>
		public bool ColletClamp { get { return IsTrue( IOAccessTargets.Input, 1608, 0x0001 ); } }
		/// <summary>コレットアンクランプ状態</summary>
		/// <remarks>
		///	i#1608 D01 SPN UNCLAMP コレットアンクランプ確認
		/// </remarks>
		public bool ColletUnclamp { get { return IsTrue( IOAccessTargets.Input, 1608, 0x0002 ); } }
		/// <summary>ブザーON</summary>
		public bool Buzzer { get { return IsTrue( IOAccessTargets.Output, 1727, 0x0020 ); } }

		/// <summary>コレットフィンガ</summary>
		/// <remarks>
		/// i#1608 D09 PS SW2(COLLET) 圧力SW2(コレット圧)
		/// </remarks>
		public bool ColletFingerClamp { get { return IsTrue( IOAccessTargets.Input, 1608, 0x0200 ); } }

		/// <summary>ESFアーム前進端</summary>
		/// <remarks>
		/// i#1609 D03 
		/// </remarks>
		public bool EsfArmFoward { get { return IsTrue( IOAccessTargets.Input, 1609, 0x0008 ); } }
		/// <summary>ESFアーム中間位置</summary>
		/// <remarks>
		/// i#1609 D02 
		/// </remarks>
		public bool EsfArmMiddle { get { return IsTrue( IOAccessTargets.Input, 1609, 0x0004 ); } }
		/// <summary>ESFアーム後退端1</summary>
		/// <remarks>
		/// i#1609 D00 
		/// </remarks>
		public bool EsfArmBack1 { get { return IsTrue( IOAccessTargets.Input, 1609, 0x0001 ); } }
		/// <summary>ESFアーム後退端2</summary>
		/// <remarks>
		/// i#1609 D01 
		/// </remarks>
		public bool EsfArmBack2 { get { return IsTrue( IOAccessTargets.Input, 1609, 0x0002 ); } }
		/// <summary>ESFアーム位置</summary>
		public EsfArmPositions EsfArmPosition
		{
			get
			{
				return ( true == EsfArmFoward ) ? EsfArmPositions.Foward
					: ( ( true == EsfArmMiddle ) ? EsfArmPositions.Middle
					: ( ( ( true == EsfArmBack1 ) && ( true == EsfArmBack2 ) ) ? EsfArmPositions.Back
					: EsfArmPositions.Unknown ) );
			}
		}
		/// <summary>GSFアーム前進端</summary>
		public bool GsfArmFoward { get { return IsTrue( IOAccessTargets.Input, 1610, 0x0001 ); } }
		/// <summary>GSFアーム後退端</summary>
		public bool GsfArmBack { get { return IsTrue( IOAccessTargets.Input, 1610, 0x0002 ); } }

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
						if( null != Items ) {
							Items.Dispose();
							Items = null;
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
		/// <summary>ファイル読み込み</summary>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// 定義ファイルを含めたファイルの読み込みを行います。
		/// </remarks>
//		public ResultCodes Initialize()
//		{
//			using( FileCncIOCheck fi = new FileCncIOCheck() ) {
//				fi.Load();
//				if( null != Items ) {
//					Items.Dispose();
//					Items = null;
//				}
//				Items = fi.Items.Clone() as StructureIODataList;
//			}
//			return ResultCodes.Success;
//		}
		public ResultCodes Initialize()
		{
			ResultCodes ret = Read();
			while( true ) {
				if( ResultCodes.Success != ret ) {
					break;
				}
				using( FileCncIOCheck fi = new FileCncIOCheck() ) {
					fi.Read();
					foreach( StructureIODataItem item in fi.Items ) {
						StructureIODataItem result = Items.Find( item );
						if( null != result ) {
							result.Name = item.Name;
							result.Note = item.Note;
						}
					}
				}
				break;
			}
			return ret;
		}
		/// <summary>読み込み</summary>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// MCよりＩ／Ｏ情報を取得します。
		/// I/O状態とあわせて強制設定情報を取得します。
		/// </remarks>
		public ResultCodes ReadEx()
		{
			//NULLセット
			if( Items != null ) {
				if(Items.Count < 2880) {
					Items.Clear();
					Items.TrimExcess();
					Items = null;
				}
			}

			Forced = false;
			IODATA dataRaw;
			//	標準情報を取得
			ResultCodes ret = ReadData( out dataRaw );
			if( ResultCodes.Success == ret ) {
				Copy( _dataLatest, ref _dataBefore );
				Copy( dataRaw, ref _dataLatest );
				//	強制設定情報を取得
				Forced = true;
				IODATA dataForced;
				ret = ReadData( out dataForced );
				if( ResultCodes.Success == ret ) {
					Copy( _dataForceLatest, ref _dataForceBefore );
					Copy( dataForced, ref _dataForceLatest );
                    //	クラスメンバ変数に格納する。
                    ushort index = 0;
                    ushort number = 0;
                    if (null == Items)
                    {
                        Items = new StructureIODataList();
                        for (index = 0; index < dataRaw.InputData.Length; ++index, ++number)
                        {
                            Items.AddByWord(IOAccessTargets.Input, number, dataRaw.InputData[index]);
                        }
                        for (index = 0; index < dataRaw.OutputData.Length; ++index, ++number)
                        {
                            Items.AddByWord(IOAccessTargets.Output, number, dataRaw.OutputData[index]);
                        }
                    }
                    else
                    {
                        //	MC情報を現在情報として扱い、上書きする。
                        for (index = 0; index < dataRaw.InputData.Length; ++index, ++number)
                        {
                            Items.UpdateByWord(IOAccessTargets.Input, number, dataRaw.InputData[index], dataForced.InputData[index]);
                        }
                        for (index = 0; index < dataRaw.OutputData.Length; ++index, ++number)
                        {
                            Items.UpdateByWord(IOAccessTargets.Output, number, dataRaw.OutputData[index], dataForced.OutputData[index]);
                        }
                    }
                }
			}
			return ret;
		}

		/// <summary>読み込み</summary>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// MCよりＩ／Ｏ情報を取得します。
		/// </remarks>
		public ResultCodes Read()
		{
			Forced = false;
			IODATA data;
			ResultCodes ret = ReadData( out data );
			if( ResultCodes.Success == ret ) {
				Copy( _dataLatest, ref _dataBefore );
				Copy( data, ref _dataLatest );
				//	クラスメンバ変数に格納する。
				//ushort index = 0;
				//ushort number = 0;
				//if( null == Items ) {
				//	Items = new StructureIODataList();
				//	for( index = 0 ; index < data.InputData.Length ; ++index, ++number ) {
				//		Items.AddByWord( IOAccessTargets.Input, number, data.InputData[index] );
				//	}
				//	for( index = 0 ; index < data.OutputData.Length ; ++index, ++number ) {
				//		Items.AddByWord( IOAccessTargets.Output, number, data.OutputData[index] );
				//	}
				//} else {
				//	//	MC情報を現在情報として扱い、上書きする。
				//	for( index = 0 ; index < data.InputData.Length ; ++index, ++number ) {
				//		Items.UpdateByWord( IOAccessTargets.Input, number, data.InputData[index] );
				//	}
				//	for( index = 0 ; index < data.OutputData.Length ; ++index, ++number ) {
				//		Items.UpdateByWord( IOAccessTargets.Output, number, data.OutputData[index] );
				//	}
				//}
				//20170220 Hachino Rep↑↓
				if( null == Items ) {
					Items = new StructureIODataList();
					Items.AddByWord( IOAccessTargets.Input, 8, data.InputData[8] );
					Items.AddByWord( IOAccessTargets.Input, 9, data.InputData[9] );
					Items.AddByWord( IOAccessTargets.Input, 10, data.InputData[10] );
					Items.AddByWord( IOAccessTargets.Input, 11, data.InputData[11] );
					Items.AddByWord( IOAccessTargets.Input, 52, data.InputData[52] );

					Items.AddByWord( IOAccessTargets.Output, 124, data.OutputData[8] );
					Items.AddByWord( IOAccessTargets.Output, 125, data.OutputData[9] );
					Items.AddByWord( IOAccessTargets.Output, 126, data.OutputData[10] );
					Items.AddByWord( IOAccessTargets.Output, 127, data.OutputData[11] );
					Items.AddByWord( IOAccessTargets.Output, 128, data.OutputData[12] );
				} else {
					//	MC情報を現在情報として扱い、上書きする。                
					Items.UpdateByWord( IOAccessTargets.Input, 8, data.InputData[8] );
					Items.UpdateByWord( IOAccessTargets.Input, 9, data.InputData[9] );
					Items.UpdateByWord( IOAccessTargets.Input, 10, data.InputData[10] );
					Items.UpdateByWord( IOAccessTargets.Input, 11, data.InputData[11] );
					Items.UpdateByWord( IOAccessTargets.Input, 52, data.InputData[52] );

					Items.UpdateByWord( IOAccessTargets.Output, 124, data.OutputData[8] );
					Items.UpdateByWord( IOAccessTargets.Output, 125, data.OutputData[9] );
					Items.UpdateByWord( IOAccessTargets.Output, 126, data.OutputData[10] );
					Items.UpdateByWord( IOAccessTargets.Output, 127, data.OutputData[11] );
					Items.UpdateByWord( IOAccessTargets.Output, 128, data.OutputData[12] );
				}
			}
			return ret;
		}
        /// <summary>信号オン判定</summary>
        /// <param name="ioTarget">入力／出力</param>
        /// <param name="address">アドレス</param>
        /// <param name="mask">マスク値</param>
        /// <returns>判定結果</returns>
        /// <remarks>
        /// 引数で指定された信号がオン状態であるかを確認します。
        /// </remarks>
        public bool IsTrue( IOAccessTargets ioTarget, ushort address, ushort mask )
		{
			if( null != Items ) {
				StructureIODataItem item = Items.Find( ioTarget, ( Math.Abs( IoMapOffset ) <= address ) ? (ushort)( address + IoMapOffset ) : address, mask );
				if( null != item ) {
					if( mask == ( item.Value & mask ) ) {
						return true; ;
					}
				}
			}
			return false;
		}
		/// <summary>信号オフ判定</summary>
		/// <param name="ioTarget">入力／出力</param>
		/// <param name="address">アドレス</param>
		/// <param name="mask">マスク値</param>
		/// <returns>判定結果</returns>
		/// <remarks>
		/// 引数で指定された信号がオフ状態であるかを確認します。
		/// </remarks>
		public bool IsFalse( IOAccessTargets ioTarget, ushort address, ushort mask )
		{
			return ( true == IsTrue( ioTarget, address, mask ) ) ? false : true;
		}
		/// <summary>MCデータ読み込み</summary>
		/// <param name="data">MCより読み込まれたデータ</param>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// MCより情報を取得します。
		/// シミュレータモードの場合、読み取り専用メソッドは必ず空の情報が取得される(2016/09/09)ので、
		/// さらに机上デバッグ用のパラメータの読み出しを行い、これを上書きします。
		/// これにより、メソッド呼び出しの成否の確認と、UI上の動作確認を両立します。
		/// </remarks>
		internal ResultCodes ReadData( out IODATA data )
		{
			AidLog logs = new AidLog( "McDatIOData.ReadData" );
			ResultCodes ret = ResultCodes.McNotInitialize;
			data = IODATA.Init();
			while( true == StandBy() ) {
				try {
					if( ( Syncdef.DAT_IODATA != DataType ) &&
						( Syncdef.DAT_FORCEIO != DataType ) ) {
						break;
					}
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
		/// <summary>構造体コピー</summary>
		/// <param name="source">コピー元</param>
		/// <param name="target">コピー先</param>
		/// <remarks>
		/// 構造体のコピーを行います。
		/// コピー先のメモリ領域の拡縮は行いません。コピー先の領域にあわせてコピーを行います。
		/// </remarks>
		private void Copy( IODATA source, ref IODATA target )
		{
			Copy( source.InputData, ref target.InputData );
			Copy( source.OutputData, ref target.OutputData );
		}
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
		private bool Equals( IODATA source, IODATA target )
		{
			if( ( false == Equals( source.InputData, target.InputData ) ) ||
				( false == Equals( source.OutputData, target.OutputData ) ) ) {
				return false;
			}
			return true;
		}
	}
}
