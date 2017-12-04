using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using ECNC3.Enumeration;
using ECNC3.Models.Common;
using static Rt64ecdata;

namespace ECNC3.Models.McIf
{
	/// <summary>4-4-1.初期化パラメータ 書込/読出</summary>
	public class McDatInitialPrm : McCommBasic, IEcnc3McDatReadWrite, IEcnc3Backup
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>情報保持ファイルパス</summary>
		private string _filePath = string.Empty;
		/// <summary>初期化パラメータ</summary>
		protected INITIALPRM _data;
		/// <summary>コンストラクタ</summary>
		public McDatInitialPrm()
		{
			Name = this.ToString();
			ClassName = GetType().Name;
			DataType = Syncdef.DAT_INITIALPRM;
			DataTypeName = "DAT_INITIALPRM";
			using( ECNC3Settings cmn = new ECNC3Settings() ) {
				_filePath = Path.Combine( cmn.DirectoryRt64Ec, "initial.ipm" );
			}
		}
        /// <summary>細線電極確認時の座屈ｾﾝｻｰONによるZ上昇量</summary>
        public int BucklingUpOfsZ { get; set; }
        /// <summary>細線電極確認時の座屈ｾﾝｻｰONによるZ上昇速度</summary>
        public int BucklingUpSpdZ { get; set; }
        /// <summary>細線電極確認時の座屈ｾﾝｻｰONによるリトライ回数</summary>
        public short BucklingRetry { get; set; }
        /// <summary>SF02 有効／無効 </summary>
        public bool EnableSF02 { get; set; }
		/// <summary>GSF有効／無効</summary>
		public bool EnableGsf { get; set; }
		/// <summary>ESF有効／無効</summary>
		public bool EnableEsf { get; set; }
        /// <summary>
        /// 電極数
        /// </summary>
        public short ElectrodeCount { get; set; }
		/// <summary>
        /// ガイド数
        /// </summary>
        public short GuideCount { get; set; }
        /// <summary>
        /// 電極交換位置
        /// </summary>
        public int[] ElctdExchPos { get; set; }
        /// <summary>
        /// W軸電極交換前位置オフセット
        /// </summary>
        public int ElctdExchOfsW1 { get; set; }
        /// <summary>
        /// W軸電極交換待機位置オフセット
        /// </summary>
        public int ElctdExchOfsW2 { get; set; }
        /// <summary>
        /// 軸電極交換位置移動速度
        /// </summary>
        public int ElctdExchSpdW { get; set; }
        /// <summary>
        /// 軸電極交換前位置移動速度
        /// </summary>
        public int ElctdExchSpdW1 { get; set; } 
        /// <summary>
        /// ガイド交換スタート位置
        /// </summary>
        public int[] GuideExchPosS { get; set; }
        /// <summary>
        /// ガイド交換エンド位置
        /// </summary>
        public int[] GuideExchPosE { get; set; }
        /// <summary>
        /// ガイド有無センサー位置
        /// </summary>
        public int[] GuideChkPos { get; set; }
        /// <summary>
        /// W軸ガイド交換前位置オフセット
        /// </summary>
        public int GuideExchOfsW1 { get; set; }
        /// <summary>
        /// W軸ガイド交換待機位置オフセット
        /// </summary>
        public int GuideExchOfsW2 { get; set; }
        /// <summary>
        /// W軸ガイド交換位置移動速度
        /// </summary>
        public int GuideExchSpdW { get; set; }
        /// <summary>
        /// 電極装着(コレットクランプ)までの主軸回転速度
        /// </summary>
        public int ElctdClumpSpdS { get; set; }
        /// <summary>
        /// 電極装着後のセンサーからの主軸回転速度
        /// </summary>
        public int[] ElctdChkSpdS2 { get; set; }
        /// <summary>電極装着後からセンサーまでのZ軸下降速度</summary>
        public int[] ElctdChkSpdZ1 { get; set; }
        /// <summary>電極装着後のセンサーからのZ軸下降速度</summary>
        public int[] ElctdChkSpdZ2 { get; set; }
        /// <summary>電極装着後のセンサーからのZ軸下降量</summary>
        public int[] ElctdChkOfsZ { get; set; }

        /// <summary>CRS10値テーブル</summary>
        public List<int> CRS10DATable
        {
            get
            {
                List<int> temp = new List<int>();
                int index = 0;
                for (index = 0; index < _data.CRS10DATable.Length; ++index)
                {
                    temp.Add(_data.CRS10DATable[index]);
                }
                return temp;
            }

            set
            {
                int index = 0;
                for (index = 0; index < _data.CRS10DATable.Length; ++index)
                {
                    _data.CRS10DATable[index] = (short)value[index];
                }
            }
        }
        /// <summary>CRS値テーブル</summary>
		public List<int> CRSDATable
        {
            get
            {
                List<int> temp = new List<int>();
                int index = 0;
                for (index = 0; index < _data.CRSDATable.Length; ++index)
                {
                    temp.Add(_data.CRSDATable[index]);
                }
                return temp;
            }

            set
            {
                int index = 0;
                for (index = 0; index < _data.CRSDATable.Length; ++index)
                {
                    _data.CRSDATable[index] = (short)value[index];
                }                
            }
        }
  //      /// <summary>加工サーボ送り速度テーブル[pls/min]</summary>
		//public List<int> SfrFrTable
		//{
		//	get
		//	{
		//		List<int> temp = new List<int>();
		//		int index = 0;
		//		for( index = 0 ; index < _data.SfrFrTable.Length ; ++index ) {
		//			temp.Add( _data.SfrFrTable[index] );
		//		}
		//		return temp;
		//	}
  //          set
  //          {
  //              int index = 0;
  //              for (index = 0; index < _data.SfrFrTable.Length; ++index)
  //              {
  //                  _data.SfrFrTable[index] = (short)value[index];
  //              }
  //          }
  //      }
		///// <summary>加工サーボ戻り速度テーブル[pls/min]</summary>
		//public List<int> SfrBkTable
		//{
		//	get
		//	{
		//		List<int> temp = new List<int>();
		//		int index = 0;
		//		for( index = 0 ; index < _data.SfrBkTable.Length ; ++index ) {
		//			temp.Add( _data.SfrBkTable[index] );
		//		}
		//		return temp;
		//	}
  //          set
  //          {
  //              int index = 0;
  //              for (index = 0; index < _data.SfrBkTable.Length; ++index)
  //              {
  //                  _data.SfrBkTable[index] = (short)value[index];
  //              }
  //          }
  //      }

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

		/// <summary>ファイルの読み込みとMCダウンロード</summary>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// initial.rom ファイルの読み込みと読み込みデータのMCへの転送を行います。
		/// </remarks>
		public ResultCodes Initialize()
		{
			INITIALPRM data = INITIALPRM.Init();
			ResultCodes ret = ReadFile( ref data, _filePath );
			if( ResultCodes.Success == ret ) {
				ret = WriteData( data );
			}
			return ret;
		}

		/// <summary>バックアップ</summary>
		/// <param name="backupDirectory">バックアップ先ディレクトリパス</param>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// MCボードよりデータを取得し、ファイルとしてバックアップします。
		/// </remarks>
		public ResultCodes Backup( string backupDirectory )
		{
			FileAccessCommon fc = new FileAccessCommon();
			fc.Backup( _filePath, backupDirectory );

			INITIALPRM data;
			ResultCodes ret = ReadData( out data );
			if( ResultCodes.Success == ret ) {
				ret = WriteFile( data, _filePath );
			}
			return ret;
		}
		/// <summary>リストア</summary>
		/// <param name="restoreDirectory">復元元情報の格納されたディレクトリパス</param>
		/// <returns>実行結果</returns>
		public ResultCodes Restore( string restoreDirectory )
		{
			FileAccessCommon fc = new FileAccessCommon();
			return fc.Restore( restoreDirectory, _filePath );
		}

		/// <summary>MCデータ読み込み</summary>
		/// <returns>実行結果</returns>
		public ResultCodes Read()
		{
			ResultCodes ret = ReadData( out _data );
			if( ResultCodes.Success == ret ) {
				EnableEsf = ( 0x0001 == ( _data.AecEnable & 0x0001 ) ) ? true : false;
				EnableGsf = ( 0x0002 == ( _data.AecEnable & 0x0002 ) ) ? true : false;
                ElectrodeCount = _data.ElctdNum;
                GuideCount = _data.GuideNum;
                ElctdExchPos = _data.ElctdExchPos;
                GuideExchPosS = _data.GuideExchPosS;
                GuideExchPosE = _data.GuideExchPosE;
                ElctdExchOfsW1 = _data.ElctdExchOfsW1;
                ElctdExchOfsW2 = _data.ElctdExchOfsW2;
                GuideExchOfsW1 = _data.GuideExchOfsW1;
                GuideExchOfsW2 = _data.GuideExchOfsW2;
                ElctdExchSpdW = _data.ElctdExchSpdW;
                ElctdExchSpdW1 = _data.ElctdExchSpdW1;
                ElctdClumpSpdS = _data.ElctdClumpSpdS;
                ElctdChkSpdS2 = _data.ElctdChkSpdS2;
                EnableSF02 = (0x0001 == (_data.MountedSF02FX & 0x0001)) ? true : false;
                BucklingUpOfsZ = _data.BucklingUpOfsZ;
                BucklingUpSpdZ = _data.BucklingUpSpdZ;
                BucklingRetry = _data.BucklingRetry;
                ElctdChkSpdZ1 = _data.ElctdChkSpdZ1;
                ElctdChkSpdS2 = _data.ElctdChkSpdS2;
                ElctdChkSpdZ2 = _data.ElctdChkSpdZ2;
                ElctdChkOfsZ = _data.ElctdChkOfsZ;
                GuideChkPos = _data.GuideChkPos;
                GuideExchSpdW = _data.GuideExchSpdW;
            }
			return ret;
		}
		/// <summary>MCデータ読み込み</summary>
		/// <param name="data">MCより読み込まれたデータ</param>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// MCより情報を取得します。
		/// </remarks>
		private ResultCodes ReadData( out INITIALPRM data )
		{
			AidLog logs = new AidLog( "McDatInitialPrm.ReadData" );
			ResultCodes ret = ResultCodes.McNotInitialize;
			data = INITIALPRM.Init();
			while( true == StandBy() ) {
				try {
					int size = Marshal.SizeOf( INITIALPRM.Init() );
					int retRt64 = 0;
					TechnoMethods method = TechnoMethods.ReceiveData;
					if( BootModes.Desktop == BootMode ) {
						retRt64 = Rt64eccomapiWrap.ReceiveData( CommHandle, DataType, Task, NonParameter, ref size, ref data );
						ret = CheckResultDebug( method, retRt64 );
						if( ResultCodes.Success != ret ) {
							break;
						}
					} else {
						retRt64 = Rt64eccomapi.ReceiveData( CommHandle, DataType, Task, NonParameter, ref size, ref data );
						ret = CheckResultTechno( method, retRt64 );
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

		/// <summary>MCデータ書き込み</summary>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// プロパティに設定された値をMCへ転送します。
		/// プロパティとして公開されていない値に関しては元の値を保持します。
		/// </remarks>
		public ResultCodes Write()
		{
			AidLog logs = new AidLog( "McDatInitialPrm.Write" );
			INITIALPRM data;
			ResultCodes ret = ReadData( out data );
			if( ResultCodes.Success == ret ) {
				AidBit bit = new AidBit();
				{
					data.AecEnable = bit.SetBit( data.AecEnable, 0x0001, EnableEsf );
					data.AecEnable = bit.SetBit( data.AecEnable, 0x0002, EnableGsf );
                    data.ElctdNum = ElectrodeCount;
                    data.GuideNum = GuideCount;
                    data.ElctdExchPos = ElctdExchPos;
                    data.GuideExchPosS = GuideExchPosS;
                    data.GuideExchPosE = GuideExchPosE;
                    data.ElctdExchOfsW1 = ElctdExchOfsW1;
                    data.ElctdExchOfsW2 = ElctdExchOfsW2;
                    data.GuideExchOfsW1 = GuideExchOfsW1;
                    data.GuideExchOfsW2 = GuideExchOfsW2;
                    data.ElctdExchSpdW = ElctdExchSpdW;
                    data.ElctdExchSpdW1 = ElctdExchSpdW1;
                    data.ElctdClumpSpdS = ElctdClumpSpdS;
                    data.ElctdChkSpdS2 = ElctdChkSpdS2;
                    data.MountedSF02FX = bit.SetBit(data.MountedSF02FX, 0x0001, EnableSF02);
                    data.BucklingUpOfsZ = BucklingUpOfsZ;
                    data.BucklingUpSpdZ = BucklingUpSpdZ;
                    data.BucklingRetry = BucklingRetry;
                    data.ElctdChkSpdZ1 = ElctdChkSpdZ1;
                    data.ElctdChkSpdS2 = ElctdChkSpdS2;
                    data.ElctdChkSpdZ2 = ElctdChkSpdZ2;
                    data.ElctdChkOfsZ = ElctdChkOfsZ;
                    data.GuideChkPos = GuideChkPos;
                    data.GuideExchSpdW = GuideExchSpdW;
                }
				ret = WriteData( data );
			}
			return ret;
		}
		/// <summary>MC書き込み</summary>
		/// <param name="data">書き込みデータ</param>
		/// <returns>実行結果</returns>
		protected ResultCodes WriteData( INITIALPRM data )
		{
			AidLog logs = new AidLog( "McDatInitialPrm.WriteData" );
			ResultCodes ret = ResultCodes.McNotInitialize;
			while( true == StandBy() ) {
				try {
					int size = Marshal.SizeOf( data );
					int retRt64 = 0;
					TechnoMethods method = TechnoMethods.SendData;
					logs.Sure( $"SendData({DataTypeName},size={size},AecEnable=0x{data.AecEnable:x2},Sf02Enable=0x{data.MountedSF02FX:x2})" );
					if( BootModes.Desktop == BootMode ) {
						retRt64 = Rt64eccomapiWrap.SendData( CommHandle, DataType, Task, NonParameter, size, ref data );
						ret = CheckResultDebug( method, retRt64 );
						if( ResultCodes.Success != ret ) {
							break;
						}
					} else {
						retRt64 = Rt64eccomapi.SendData( CommHandle, DataType, Task, NonParameter, size, ref data );
						ret = CheckResultTechno( method, retRt64 );
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
