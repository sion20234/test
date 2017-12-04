using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ECNC3.Enumeration;
using ECNC3.Models.Common;
using static Rt64ecdata;

namespace ECNC3.Models.McIf
{
	/// <summary>4-4-18.メッセージ表示要求データ読出／書き込み</summary>
	public class McDatShowMessage : McCommBasic, IEcnc3McDatReadWrite, IDisposable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>メッセージ表示要求データ読み出し内容</summary>
		private MESSAGEREQ _requset;
		/// <summary>コンストラクタ</summary>
		public McDatShowMessage()
		{
			ClassName = GetType().Name;
		}
		/// <summary>メッセージ表示命令実行プログラム種別</summary>
		public ProgramCodeTypes RequsetProgramType { get; private set; }
		/// <summary>待機、実行Gコード／Mコード</summary>
		public short RequsetCode { get; private set; }
		/// <summary>メッセージ番号</summary>
		public short RequsetMessageNumber { get; private set; }

		/// <summary>選択ボタン番号</summary>
		public short AnswerSelectedButtonNumber { get; set; }
		/// <summary>入力数値</summary>
		public double AnswerInputNumber { get; set; }
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
			ResultCodes ret = ReadData( out _requset );
			if( ResultCodes.Success == ret ) {
				RequsetProgramType = (ProgramCodeTypes)( ( _requset.LineFlg & 0x0300 ) >> 8 );
				RequsetCode = (short)( _requset.LineFlg & 0x00FF );
				RequsetMessageNumber = _requset.MessageNo;
			}
			return ret;
		}
		/// <summary>MCデータ読み込み</summary>
		/// <param name="data">MCより読み込まれたデータ</param>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// MCより情報を取得します。
		/// </remarks>
		private ResultCodes ReadData( out MESSAGEREQ data )
		{
			DataType = Syncdef.DAT_MESSAGEREQ;
			DataTypeName = "DAT_MESSAGEREQ";
			AidLog logs = new AidLog( "McDatShowMessage.ReadData" );
			ResultCodes ret = ResultCodes.McNotInitialize;
			data = MESSAGEREQ.Init();
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
					logs.Sure( $"LineFlg={data.LineFlg:x},MessageNo={data.MessageNo}" );
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
			while( null != _requset.Reserved ) {
				if( 4 != _requset.Reserved.Length ) {
					break;
				}
				MESSAGEANS data = MESSAGEANS.Init();
				data.Req.LineFlg = _requset.LineFlg;
				data.Req.MessageNo = _requset.MessageNo;
				data.Req.Reserved[0] = _requset.Reserved[0];
				data.Req.Reserved[1] = _requset.Reserved[1];
				data.Req.Reserved[2] = _requset.Reserved[2];
				data.Req.Reserved[3] = _requset.Reserved[3];
				data.ButtonSel = AnswerSelectedButtonNumber;
				data.InputNum = AnswerInputNumber;
				return WriteData( data );
			}
			return ResultCodes.LackOfPreparation;
		}
		/// <summary>書き込み</summary>
		/// <param name="data">書き込み情報</param>
		/// <returns>実行結果</returns>
		private ResultCodes WriteData( MESSAGEANS data )
		{
			DataType = Syncdef.DAT_MESSAGEANS;
			DataTypeName = "DAT_MESSAGEANS";
			AidLog logs = new AidLog( "McDatShowMessage.WriteData" );
			ResultCodes ret = ResultCodes.McNotInitialize;
			data = MESSAGEANS.Init();
			while( true == StandBy() ) {
				try {
					int size = Marshal.SizeOf( data );
					int retRt64 = 0;
					TechnoMethods method = TechnoMethods.SendData;
					logs.Sure( $"SendData({DataTypeName},ButtonSel={data.ButtonSel},InputNum ={data.InputNum})" );
					if( true == BootMode.HasFlag( BootModes.Machine ) ) {
						retRt64 = Rt64eccomapi.SendData( CommHandle, DataType, Task, NonParameter, size, ref data );
						ret = CheckResultTechno( method, retRt64 );
						if( ResultCodes.Success != ret ) {
							break;
						}
					}
					if( true == BootMode.HasFlag( BootModes.Desktop ) ) {
						retRt64 = Rt64eccomapiWrap.SendData( CommHandle, DataType, Task, NonParameter, size, ref data );
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
