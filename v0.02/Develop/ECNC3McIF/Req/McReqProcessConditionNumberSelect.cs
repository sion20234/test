using System;
using ECNC3.Enumeration;
using ECNC3.Models.Common;
using static Rt64ecdata;

namespace ECNC3.Models.McIf
{
	/// <summary>4-5-5.加工条件番号変更コマンド</summary>
	public class McReqProcessConditionNumberSelect : McCommBasic, IEcnc3McCommand, IDisposable
    {
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>XMLファイル永続化要素名</summary>
		private string _xmlElementName = null;
		/// <summary>XMLファイル永続化属性名</summary>
		private string _xmlAttributeName = null;
		/// <summary>コンストラクタ</summary>
		public McReqProcessConditionNumberSelect()
		{
			ClassName = GetType().Name;
			DataType = Syncdef.REQ_PNOSEL;
			DataTypeName = "REQ_PNOSEL";
			_xmlElementName = "Root/Opr/SelProcNum";
			_xmlAttributeName = "val";
		}
		/// <summary>選択番号</summary>
		public int SelectingNumber { get; set; }
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
			return ExecuteByValue( SelectingNumber, _xmlElementName, _xmlAttributeName );
		}
		/// <summary>コマンド発行</summary>
		/// <param name="target">設定値</param>
		/// <param name="element">要素XPath</param>
		/// <param name="attr">属性名</param>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// プロパティに設定された内容によりMCボードへコマンドを発行します。
		/// </remarks>
		protected ResultCodes ExecuteByValue( int target, string element, string attr )
		{
			AidLog logs = new AidLog( "McReqProcessConditionNumberSelect.Execute" );
			ResultCodes ret = ResultCodes.McNotInitialize;
			while( true == StandBy() ) {
				try {
					PRNOSEL data = PRNOSEL.Init();
					data.PNo = target;
					int retRt64 = 0;
					TechnoMethods method = TechnoMethods.SendCommand;
					logs.Sure( $"SendCommand({DataTypeName},PNo={data.PNo}" );
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
					if( ( false == string.IsNullOrEmpty( element ) ) && ( false == string.IsNullOrEmpty( attr ) ) ) {
						//	転送処理が成功した場合、保持値を更新する。
						using( FileSettings fs = new FileSettings() ) {
							fs.Read();
							fs.WriteAttr( element, attr, $"{target}" );
							fs.Write();
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

		/// <summary>初期化</summary>
		/// <param name="fs">設定情報操作クラスの参照</param>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// XMLファイルに保持された情報をMCへ転送します。
		/// </remarks>
		public ResultCodes Initalize( FileSettings fs = null )
		{
			if( null != fs ) {
				SelectingNumber = fs.AttrValue( _xmlElementName, _xmlAttributeName );
			} else {
				using( FileSettings fs0 = new FileSettings() ) {
					SelectingNumber = fs0.AttrValue( _xmlElementName, _xmlAttributeName );
				}
			}
			return Execute();
		}
	}
}
