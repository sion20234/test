using ECNC3.Enumeration;
using ECNC3.Models.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Rt64ecdata;

namespace ECNC3.Models.McIf
{
	public class McDatInitialPrm : McIfBasic, IDisposable
	{
//		[DllImport( "RT64ECComNTWrap.dll" )]
//		private extern static int SendDataWrap( int hCom, short type, short task, short prm, int size, [In] ref INITIALPRM data );
//		[DllImport( "RT64ECComNTWrap.dll" )]
//		private extern static int ReceiveDataWrap( int hCom, short type, short task, short prm, [Out] out int size, [Out] out INITIALPRM data );

		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		private string _path = string.Empty;
		/// <summary>コンストラクタ</summary>
		public McDatInitialPrm()
		{
			McDataType = Syncdef.DAT_INITIALPRM;
			FileAccessCommon fa = new FileAccessCommon();
			_path = fa.MakeFilePath( "initial.ipm" );
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

		/// <summary>ファイルの読み込みとMCダウンロード</summary>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// initial.rom ファイルの読み込みと読み込みデータのMCへの転送を行います。
		/// </remarks>
		public ResultCodes Initialize()
		{
			if( true == StandBy() ) {
				INITIALPRM data;
				ReadFile( out data );
				int size = Marshal.SizeOf( data );
				int ret = 0;
				if( true == DeskTopMode ) {
					ret = Rt64eccomapiWrap.SendData( CommHandle, McDataType, Task, 0, size, ref data );
				} else {
					ret = Rt64eccomapi.SendData( CommHandle, McDataType, Task, 0, size, ref data );
				}
				if( 0 != ret ) {
					return ResultCodes.McError;
				}
				return ResultCodes.Success;
			}
			return ResultCodes.McNotInitialize;
		}

		private void ReadFile( out INITIALPRM data )
		{
			data = INITIALPRM.Init();
			FileStream fs = new FileStream( _path, FileMode.Open );
			using( BinaryReader br = new BinaryReader( fs ) ) {
				long size1 = Marshal.SizeOf( data );
				long size2 = fs.Length;
				var size = Marshal.SizeOf( typeof( INITIALPRM ) );
				var ptr = IntPtr.Zero;
				try {
					//	ポインタのためのコピー用領域の作成。
					ptr = Marshal.AllocHGlobal( size );
					//	バイト配列を構造体に変換してコピーする。
					Marshal.Copy( br.ReadBytes( size ), 0, ptr, size );
					data = (INITIALPRM)Marshal.PtrToStructure( ptr, typeof( INITIALPRM ) );
				} finally {
					if( ptr != IntPtr.Zero ) {
						Marshal.FreeHGlobal( ptr );
					}
					br.Close();
				}
			}
		}
		private void WriteFile( INITIALPRM data )
		{
			FileStream fs = new FileStream( _path, FileMode.OpenOrCreate );
			using( BinaryWriter bw = new BinaryWriter( fs ) ) {
				var size = Marshal.SizeOf( typeof( INITIALPRM ) );
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
		}

		/// <summary>ファイル出力</summary>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// MCより初期化情報を取得し、ファイルに出力します。
		/// 
		/// </remarks>
		public ResultCodes Export()
		{
			if( true == StandBy() ) {
				int ret = 0;
				INITIALPRM data = INITIALPRM.Init();
				int size1 = Marshal.SizeOf( typeof( INITIALPRM ) );
				//	MCより情報を取得
				if( true == DeskTopMode ) {
					ret = Rt64eccomapiWrap.ReceiveData( CommHandle, McDataType, 0, 0, ref size1, ref data );
				} else {
					ret = Rt64eccomapi.ReceiveData( CommHandle, McDataType, 0, 0, ref size1, ref data );
				}
				if( 0 != ret ) {
					return ResultCodes.McError;
				}
				//	ファイル出力
				WriteFile( data );
				return ResultCodes.Success;
			}
			return ResultCodes.McNotInitialize;
		}
	}
}
