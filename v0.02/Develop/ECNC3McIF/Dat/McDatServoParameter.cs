using System;
using System.IO;
using System.Runtime.InteropServices;
using ECNC3.Enumeration;
using ECNC3.Models.Common;
using static Rt64ecdata;

namespace ECNC3.Models.McIf
{
	/// <summary>4-1-1.サーボパラメータ書込/読出</summary>
	public class McDatServoParameter : McCommBasic, IEcnc3McDatReadWrite, IEcnc3Backup, IDisposable
    {
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>情報保持ファイルパス</summary>
		private string _filePath = string.Empty;
		/// <summary>コンストラクタ</summary>
		public McDatServoParameter()
		{
			Name = this.ToString();
			ClassName = GetType().Name;
			DataType = Syncdef.DAT_PARAMETER;
			DataTypeName = "DAT_PARAMETER";
			using( ECNC3Settings cmn = new ECNC3Settings() ) {
				_filePath = Path.Combine( cmn.DirectoryRt64Ec, "initial.prm" );
			}
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
		/// <summary>初期化</summary>
		/// <returns>実行結果</returns>
		public ResultCodes Initialize()
		{
			PARAMETER_DATA data = PARAMETER_DATA.Init();
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

			PARAMETER_DATA data;
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
			PARAMETER_DATA data;
			return ReadData( out data );
		}
		/// <summary>MCデータ読み込み</summary>
		/// <param name="data">MCより読み込まれたデータ</param>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// MCより情報を取得します。
		/// </remarks>
		protected ResultCodes ReadData( out PARAMETER_DATA data )
		{
			AidLog logs = new AidLog( "McDatServoParameter.ReadData" );
			ResultCodes ret = ResultCodes.McNotInitialize;
			data = PARAMETER_DATA.Init();
			while( true == StandBy() ) {
				try {
					int size = Marshal.SizeOf( data );
					int retRt64 = 0;
					TechnoMethods method = TechnoMethods.ReceiveData;
					logs.Sure( $"ReceiveData({DataTypeName},size={size})" );
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
			PARAMETER_DATA data;
			ResultCodes ret = ReadData( out data );
			if( ResultCodes.Success == ret ) {
				ret = WriteData( data );
			}
			return ret;
		}
		/// <summary>書き込み</summary>
		/// <param name="data">書き込み情報</param>
		/// <returns>実行結果</returns>
		public ResultCodes WriteData( PARAMETER_DATA data )
		{
			AidLog logs = new AidLog( "McDatServoParameter.WriteData" );
			ResultCodes ret = ResultCodes.McNotInitialize;
			while( true == StandBy() ) {
				try {
					int size = Marshal.SizeOf( data );
					int retRt64 = 0;
					TechnoMethods method = TechnoMethods.SendData;
					logs.Sure( $"SendData({DataTypeName})" );
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
#if false
		/// <summary>ファイル読み込み</summary>
		/// <param name="data">取得結果</param>
		/// <remarks>
		/// ファイルに永続化されたMCデータの読み込みを行います。
		/// </remarks>
		private void ReadFile( out PARAMETER_DATA data )
		{
			data = PARAMETER_DATA.Init();
			FileStream fs = new FileStream( _filePath, FileMode.Open );
			using( BinaryReader br = new BinaryReader( fs ) ) {
				long size1 = Marshal.SizeOf( data );
				long size2 = fs.Length;
				var size = Marshal.SizeOf( typeof( PARAMETER_DATA ) );
				var ptr = IntPtr.Zero;
				try {
					//	ポインタのためのコピー用領域の作成。
					ptr = Marshal.AllocHGlobal( size );
					//	バイト配列を構造体に変換してコピーする。
					Marshal.Copy( br.ReadBytes( size ), 0, ptr, size );
					data = (PARAMETER_DATA)Marshal.PtrToStructure( ptr, typeof( PARAMETER_DATA ) );
				} finally {
					if( ptr != IntPtr.Zero ) {
						Marshal.FreeHGlobal( ptr );
					}
					br.Close();
				}
			}
			fs.Close();
		}
		/// <summary>ファイル書き込み</summary>
		/// <param name="data">設定値</param>
		/// <returns>実行結果</returns>
		private ResultCodes WriteFile( PARAMETER_DATA data )
		{
			FileStream fs = new FileStream( _filePath, FileMode.OpenOrCreate );
			using( BinaryWriter bw = new BinaryWriter( fs ) ) {
				var size = Marshal.SizeOf( typeof( PARAMETER_DATA ) );
				var buffer = new byte[size];
				var ptr = IntPtr.Zero;
				try {
					//	ポインタのためのコピー用領域の作成。
					ptr = Marshal.AllocHGlobal( size );
					//	構造体をバイト配列に変換してコピーする。
					Marshal.StructureToPtr( data, ptr, false );
					Marshal.Copy( ptr, buffer, 0, size );
				} finally {
					if( ptr != IntPtr.Zero ) {
						Marshal.FreeHGlobal( ptr );
					}
				}
				bw.Write( buffer );
				bw.Close();
			}
			fs.Close();
			return ResultCodes.Success;
		}
#endif
	}
}
