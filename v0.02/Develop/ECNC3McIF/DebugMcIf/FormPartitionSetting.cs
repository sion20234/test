using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ECNC3.Models.McIf;

namespace DebugMcIf
{
	/// <summary>パーティション設定フォーム</summary>
	public partial class FormPartitionSetting : Form
	{
		/// <summary>コンストラクタ</summary>
		public FormPartitionSetting()
		{
			InitializeComponent();
		}
		/// <summary>ロード</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void FormPartitionSetting_Load( object sender, EventArgs e )
		{
			Loading();
		}
		/// <summary>パーティション有効／無効 クリック</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void _chkEnable_Click( object sender, EventArgs e )
		{
			AidControl aid = new AidControl();
			Save();
			Loading();
		}
		/// <summary>パーティション登録</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void _btnSet_Click( object sender, EventArgs e )
		{
			Save();
			Loading();
		}
		/// <summary>パーティション情報表示更新</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void _btnGet_Click( object sender, EventArgs e )
		{
			Loading();
		}
		/// <summary>再取得</summary>
		private void Loading()
		{
			using( McReqPartitionChange mc = new McReqPartitionChange() ) {
				mc.Read();
				_chkEnable.Checked = mc.Partitions.Enabled;
				foreach( StructurePartitionItem item in mc.Partitions.Items ) {
					if( 1 == item.Number ) {
						SetItems( item, _edtStart1, _edtEnd1, _chkThin1 );
					} else if( 2 == item.Number ) {
						SetItems( item, _edtStart2, _edtEnd2, _chkThin2 );
					} else if( 3 == item.Number ) {
						SetItems( item, _edtStart3, _edtEnd3, _chkThin3 );
					} else if( 4 == item.Number ) {
						SetItems( item, _edtStart4, _edtEnd4, _chkThin4 );
					} else if( 5 == item.Number ) {
						SetItems( item, _edtStart5, _edtEnd5, _chkThin5 );
					} else if( 6 == item.Number ) {
						SetItems( item, _edtStart6, _edtEnd6, _chkThin6 );
					} else {
						;
					}
				}
			}
			AidControl aid = new AidControl();
			aid.SetEnables( this.Controls, _chkEnable.Checked, _chkEnable );
		}
		/// <summary>保存</summary>
		private void Save()
		{
			using( McReqPartitionChange mc = new McReqPartitionChange() ) {
				mc.Read();
				mc.Partitions.Enabled = _chkEnable.Checked;
				foreach( StructurePartitionItem item in mc.Partitions.Items ) {
					if( 1 == item.Number ) {
						GetItems( item, _edtStart1, _edtEnd1, _chkThin1 );
					} else if( 2 == item.Number ) {
						GetItems( item, _edtStart2, _edtEnd2, _chkThin2 );
					} else if( 3 == item.Number ) {
						GetItems( item, _edtStart3, _edtEnd3, _chkThin3 );
					} else if( 4 == item.Number ) {
						GetItems( item, _edtStart4, _edtEnd4, _chkThin4 );
					} else if( 5 == item.Number ) {
						GetItems( item, _edtStart5, _edtEnd5, _chkThin5 );
					} else if( 6 == item.Number ) {
						GetItems( item, _edtStart6, _edtEnd6, _chkThin6 );
					} else {
						;
					}
				}
				//mc.Write();
				mc.Execute();
			}
		}
		/// <summary>パーティション単位の設定</summary>
		/// <param name="target">対象のパーティション</param>
		/// <param name="start">開始電極番号</param>
		/// <param name="end">終了電極番号</param>
		/// <param name="thinline">細線モードの有無</param>
		private void SetItems( StructurePartitionItem target, NumericUpDown start, NumericUpDown end, CheckBox thinline )
		{
			start.Value = target.IndexStart;
			end.Value = target.IndexEnd;
			thinline.Enabled = target.Thinline;
		}
		/// <summary>パーティション単位の取得</summary>
		/// <param name="target">対象のパーティション</param>
		/// <param name="start">開始電極番号</param>
		/// <param name="end">終了電極番号</param>
		/// <param name="thinline">細線モードの有無</param>
		private void GetItems( StructurePartitionItem target, NumericUpDown start, NumericUpDown end, CheckBox thinline )
		{
			target.IndexStart = (short)start.Value;
			target.IndexEnd = (short)end.Value;
			target.Thinline = thinline.Checked;
		}
	}
}
