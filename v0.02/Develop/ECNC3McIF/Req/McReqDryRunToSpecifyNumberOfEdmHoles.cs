﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECNC3.Enumeration;
using ECNC3.Models.Common;
using static Rt64ecdata;

namespace ECNC3.Models.McIf
{
	/// <summary>4-5-46.ドライラン設定(放電加工穴数指定)コマンド</summary>
	public class McReqDryRunToSpecifyNumberOfEdmHoles : McCommBasic, IEcnc3McCommand, IDisposable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>コンストラクタ</summary>
		public McReqDryRunToSpecifyNumberOfEdmHoles()
		{
			ClassName = GetType().Name;
			DataType = Syncdef.REQ_DRYRUN_EX;
			DataTypeName = "REQ_DRYRUN_EX";
		}
		/// <summary>無効／有効</summary>
		public bool Enabled { get; set; }
		/// <summary>放電加工穴数</summary>
		public short NumberOfEdmHoles { get; set; }

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
		/// <summary>コマンド発行</summary>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// プロパティに設定された内容によりMCボードへコマンドを発行します。
		/// </remarks>
		public ResultCodes Execute()
		{
			AidLog logs = new AidLog( "McReqDryRunToSpecifyNumberOfEdmHoles.Execute" );
			ResultCodes ret = ResultCodes.McNotInitialize;
			while( true == StandBy() ) {
				try {
					DRYRUN_EX data = DRYRUN_EX.Init();
					data.DryRunEnN = 0;
					if( true == Enabled ) {
						data.DryRunEnN = ( 0 == NumberOfEdmHoles ) ? -1 : NumberOfEdmHoles;
					}
					int retRt64 = 0;
					TechnoMethods method = TechnoMethods.SendCommand;
					logs.Sure( $"SendCommand({DataTypeName},DryRunEnN={data.DryRunEnN}" );
					if( true == BootMode.HasFlag( BootModes.Machine ) ) {
						retRt64 = Rt64eccomapi.SendCommand( CommHandle, DataType, Task, ref data );
						ret = CheckResultTechno( method, retRt64 );
						if( ResultCodes.Success != ret ) {
							break;
						}
					}
					if( true == BootMode.HasFlag( BootModes.Desktop ) ) {
						retRt64 = Rt64eccomapiWrap.SendCommand( CommHandle, DataType, Task, ref data );
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
