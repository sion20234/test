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
	/// <summary>4-5-20,29.ESFマガジン移動コマンド</summary>
	public class McReqEsfMoveMagazine : McCommBasic, IEcnc3McCommand, IDisposable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>電極マガジン番号</summary>
		private int _magazineNumber = -1;

		/// <summary>コンストラクタ</summary>
		public McReqEsfMoveMagazine()
		{
			ClassName = GetType().Name;
			MagazineNumber = -1;
		}

		/// <summary>電極マガジン番号</summary>
		/// <value>
		/// 移動させたい電極マガジン番号を設定します。
		/// ひとつだけインクリメントさせたい場合は、-1を設定してください。
		/// 当プロパティを設定しない場合、無条件にインクリメントとなります。
		/// </value>
		public int MagazineNumber
		{
			get { return _magazineNumber; }
			set
			{
				if( 0 > value ) {
					DataType = Syncdef.REQ_ESFMAGAZINE_INC;
					DataTypeName = "REQ_ESFMAGAZINE_INC";
					_magazineNumber = -1;
				} else {
					DataType = Syncdef.REQ_ESFMAGAZINE_MOV;
					DataTypeName = "REQ_ESFMAGAZINE_MOV";
					_magazineNumber = value;
				}
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
		/// <summary>コマンド発行</summary>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// プロパティに設定された内容によりMCボードへコマンドを発行します。
		/// </remarks>
		public ResultCodes Execute()
		{
			if( Syncdef.REQ_ESFMAGAZINE_INC == DataType ) {
				return ExecuteByNonParam();
			}
			AidLog logs = new AidLog( "McReqElectrodeInsall.Execute" );
			ResultCodes ret = ResultCodes.McNotInitialize;
			while( true == StandBy() ) {
				try {
					if( ( Syncdef.REQ_ESFMAGAZINE_INC != DataType ) &&
						( Syncdef.REQ_ESFMAGAZINE_MOV != DataType ) ) {
						break;
					}
					ESFMAGMOV data = ESFMAGMOV.Init();
					data.MagazineNo = MagazineNumber;
					int retRt64 = 0;
					TechnoMethods method = TechnoMethods.SendCommand;
					logs.Sure( $"SendCommand({DataTypeName},MagazineNo={data.MagazineNo})" );
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
