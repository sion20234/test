using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECNC3.Models.McIf
{
	/// <summary>タスク情報構造体</summary>
	public class StructureMcDatStatusTask : IDisposable, ICloneable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>タスクステータス</summary>
		public int TaskStatus { get; set; }
		/// <summary>タスクアラーム</summary>
		public int TaskAlarm { get; set; }
		/// <summary>送りオーバーライド設定</summary>
		public short Override { get; set; }
		/// <summary>補間オーバーライド設定</summary>
		public short COverride { get; set; }
		/// <summary>主軸オーバーライド設定</summary>
		public short SOverride { get; set; }
		/// <summary>選択・実行プログラム番号</summary>
		public short ProgramNo { get; set; }
		/// <summary>実行ステップ番号</summary>
		public int StepNo { get; set; }
		/// <summary>待機・実行Ｎ番号</summary>
		public short NNo { get; set; }
		/// <summary>待機・実行行番号</summary>
		public short LineNo { get; set; }
		/// <summary>待機・実行行番号種別</summary>
		public short LineFlg { get; set; }

		/// <summary>インスタンスの破棄</summary>
		public void Dispose()
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
		private void Dispose( bool disposing )
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
				;   //  基底クラスのDispose()を確実に呼び出す。
					//base.Dispose( disposing );
			}
		}
		/// <summary>コピー</summary>
		/// <param name="source">コピー元</param>
		public void Copy( StructureMcDatStatusTask source )
		{
			TaskStatus = source.TaskStatus;
			TaskAlarm = source.TaskAlarm;
			Override = source.Override;
			COverride = source.COverride;
			SOverride = source.SOverride;
			ProgramNo = source.ProgramNo;
			StepNo = source.StepNo;
			NNo = source.NNo;
			LineNo = source.LineNo;
			LineFlg = source.LineFlg;
		}
		/// <summary>クローン</summary>
		/// <returns>クローンされたインスタンス</returns>
		public object Clone()
		{
			StructureMcDatStatusTask temp = new StructureMcDatStatusTask();
			temp.Copy( this );
			return temp;
		}
		public int Compare( StructureMcDatStatusTask source )
		{
			int ret = 0;
			while( true ) {
				ret = TaskStatus.CompareTo( source.TaskStatus );
				if( 0 != ret ) {
					break;
				}
				ret = TaskAlarm.CompareTo( source.TaskAlarm );
				if( 0 != ret ) {
					break;
				}
				ret = Override.CompareTo( source.Override );
				if( 0 != ret ) {
					break;
				}
				ret = COverride.CompareTo( source.COverride );
				if( 0 != ret ) {
					break;
				}
				ret = SOverride.CompareTo( source.SOverride );
				if( 0 != ret ) {
					break;
				}
				ret = ProgramNo.CompareTo( source.ProgramNo );
				if( 0 != ret ) {
					break;
				}
				ret = StepNo.CompareTo( source.StepNo );
				if( 0 != ret ) {
					break;
				}
				ret = NNo.CompareTo( source.NNo );
				if( 0 != ret ) {
					break;
				}
				ret = LineNo.CompareTo( source.LineNo );
				if( 0 != ret ) {
					break;
				}
				ret = LineFlg.CompareTo( source.LineFlg );
				if( 0 != ret ) {
					break;
				}
				break;
			}
			return ret;
		}
	}
}
