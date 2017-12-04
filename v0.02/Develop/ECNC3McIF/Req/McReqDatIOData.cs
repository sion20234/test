using System;
using ECNC3.Enumeration;
using static Rt64ecdata;

namespace ECNC3.Models.McIf
{
	public class McReqDatIOData : McCommBasic, IDisposable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public McReqDatIOData()
		{
			ClassName = GetType().Name;
			Forced = false;
		}
		/// <summary>
		/// インスタンスの破棄
		/// </summary>
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
		/// <summary>
		/// NC書き込み：COMPIOBIT
		/// </summary>
		/// <param name="oneAddata"></param>
		/// <returns></returns>
		public ResultCodes WriteEx( COMPIOBIT oneAddata)
		{
			Forced = true;//強制
			int intCount = 0;//カウンタ
			int retRt64 = 0;
			while( true == StandBy() ) {
				//int size = Marshal.SizeOf( dataRaw );
				TechnoMethods method = TechnoMethods.SendCommand;
				if( true == BootMode.HasFlag( BootModes.Machine ) ) {
					retRt64 = Rt64eccomapi.SendCommand( CommHandle, Syncdef.REQ_COMPIOBIT, Task, ref oneAddata );
					ResultCodes ret = CheckResultTechno( method, retRt64 );
					if( ResultCodes.Success != ret ) {
						return ret;
					}
					return ResultCodes.Success;//成功
				}
                //リトライ
                if (intCount > 10000)
                {
#if DEBUG
                    return ResultCodes.Success;//デバック時は成功にします
#else
					return ResultCodes.McCommErrorRetryOver;//リトライ：オーバー
#endif
                }
                intCount++;
			}
#if DEBUG
            return ResultCodes.Success;//デバック時は成功にします
#else
            return ResultCodes.McCommErrorUnknown;//不明
#endif
        }
    }
}

