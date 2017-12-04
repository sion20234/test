using System;
using System.IO;
using System.Runtime.InteropServices;
using ECNC3.Enumeration;
using ECNC3.Models.Common;
using static Rt64ecdata;

namespace ECNC3.Models.McIf
{
	/// <summary>4-4-8.ROMSW設定 書込/読出</summary>
	public class McDatRomSwitch : McCommBasic, IEcnc3McDatReadWrite, IEcnc3Backup, IDisposable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>情報保持ファイルパス</summary>
		private string _filePath = string.Empty;
		/// <summary>MCインターフェース構造体</summary>
		private ROMSW _data;
		/// <summary>コンストラクタ</summary>
		public McDatRomSwitch()
		{
			Name = this.ToString();
			ClassName = GetType().Name;
			DataType = Syncdef.DAT_ROMSWITCH;
			DataTypeName = "DAT_ROMSWITCH";
			using( ECNC3Settings cmn = new ECNC3Settings() ) {
				_filePath = Path.Combine( cmn.DirectoryRt64Ec, "initial.rom" );
			}
		}
		/// <summary>A軸有効設定</summary>
		public bool? EnableAxisA { get; set; }
		/// <summary>B軸有効設定</summary>
		public bool? EnableAxisB { get; set; }
		/// <summary>C軸有効設定</summary>
		public bool? EnableAxisC { get; set; }

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
		/// <summary>初期化</summary>
		/// <returns>実行結果</returns>
		public ResultCodes Initialize()
		{
			AidLog logs = new AidLog( "McDatRomSwitch.Initialize" );
			ROMSW data = ROMSW.Init();
			ResultCodes ret = ReadFile( ref data, _filePath );
			if( ResultCodes.Success == ret ) {
				logs.Sure( $"SendData({DataTypeName},st_en=0x{data.st_en},axis_en=0x{data.axis_en:x},io_en=0x{data.io_en:x})" );
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
			using( ECNC3Settings cmn = new ECNC3Settings() ) {
				fc.Backup( Path.Combine( cmn.DirectoryRt64Ec, @"rt64ecDrv.ini" ), backupDirectory );
			}
			ROMSW data;
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

		/// <summary>読み込み</summary>
		/// <returns>実行結果</returns>
		public ResultCodes Read()
		{
			ResultCodes ret = ReadData( out _data );
			if( ResultCodes.Success == ret ) {
				AidBit bit = new AidBit();
				if( true == bit.IsTrue( _data.axis_en, (int)AxisBits.A ) ) {
					EnableAxisA = true;
				}
				if( true == bit.IsTrue( _data.axis_en, (int)AxisBits.B ) ) {
					EnableAxisB = true;
				}
				if( true == bit.IsTrue( _data.axis_en, (int)AxisBits.C ) ) {
					EnableAxisC = true;
				}
			}
			return ret;
		}
		/// <summary>MCデータ読み込み</summary>
		/// <param name="data">MCより読み込まれたデータ</param>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// MCより情報を取得します。
		/// </remarks>
		private ResultCodes ReadData( out ROMSW data )
		{
			AidLog logs = new AidLog( "McDatRomSwitch.ReadData" );
			ResultCodes ret = ResultCodes.McNotInitialize;
			data = ROMSW.Init();
			while( true == StandBy() ) {
				try {
					int size = Marshal.SizeOf( data );
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
		/// <summary>書き込み</summary>
		/// <returns>実行結果</returns>
		public ResultCodes Write()
		{
			AidLog logs = new AidLog( "McDatRomSwitch.Write" );
			FileAccessCommon fa = new FileAccessCommon();
			ROMSW data;
			ResultCodes ret = ReadData( out data );
			if( ResultCodes.Success == ret ) {
				using( ECNC3Settings cmn = new ECNC3Settings() )
				using( AidIni fs = new AidIni( Path.Combine( cmn.DirectoryRt64Ec, "rt64ecDrv.ini" ) ) ) {
					long fileSta = fs.GetLong( "Program", "EnableStationL", 0 );
					long fileAxs = fs.GetLong( "Program", "EnableAxisL", 0 );
					AidBit bit = new AidBit();
					bool setValue = false;
					if( null != EnableAxisA ) {
						setValue = ( true == EnableAxisA ) ? true : false;
						fileSta = bit.SetBit( fileSta, (long)AxisBits.A, setValue );
						fileAxs = bit.SetBit( fileAxs, (long)AxisBits.A, setValue );
					}
					if( null != EnableAxisB ) {
						setValue = ( true == EnableAxisB ) ? true : false;
						fileSta = bit.SetBit( fileSta, (long)AxisBits.B, setValue );
						fileAxs = bit.SetBit( fileAxs, (long)AxisBits.B, setValue );
					}
					if( null != EnableAxisC ) {
						setValue = ( true == EnableAxisC ) ? true : false;
						fileSta = bit.SetBit( fileSta, (long)AxisBits.C, setValue );
						fileAxs = bit.SetBit( fileAxs, (long)AxisBits.C, setValue );
					}
					fs.SetHex( "Program", "EnableStationL", fileSta );
					fs.SetHex( "Program", "EnableAxisL", fileAxs );
                    //	パラメータ設定
                    data.st_en = fileSta;
                    data.axis_en = fileAxs;
				}
				ret = WriteData( data );
			}
			return ret;
		}
		/// <summary>書き込み</summary>
		/// <param name="data">書き込み情報</param>
		/// <returns>実行結果</returns>
		private ResultCodes WriteData( ROMSW data )
		{
			AidLog logs = new AidLog( "McDatRomSwitch.WriteData" );
			ResultCodes ret = ResultCodes.McNotInitialize;
			while( true == StandBy() ) {
				try {
					int size = Marshal.SizeOf( data );
					int retRt64 = 0;
					TechnoMethods method = TechnoMethods.SendData;
					logs.Sure( $"SendData({DataTypeName},size={size},axis_en={data.axis_en:x4})" );
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
