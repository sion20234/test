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
	/// <summary>初期設定フォーム</summary>
	public partial class FormStartupSetting : Form
	{
		/// <summary>コンストラクタ</summary>
		public FormStartupSetting()
		{
			InitializeComponent();
		}
		/// <summary>ロード</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void FormStartupSetting_Load( object sender, EventArgs e )
		{
			Loading();
		}
		/// <summary>設定ボタンクリック</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void _btnSet_Click( object sender, EventArgs e )
		{
			Save();
			Loading();
		}
		/// <summary>読み込み</summary>
		private void Loading()
		{
			//	有効軸設定
			using( McDatRomSwitch mc = new McDatRomSwitch() ) {
				mc.Read();
				_chkEnableAxisA.Checked = ( true == mc.EnableAxisA ) ? true : false;
				_chkEnableAxisB.Checked = ( true == mc.EnableAxisB ) ? true : false;
				_chkEnableAxisC.Checked = ( true == mc.EnableAxisC ) ? true : false;
			}
			using( McDatInitialPrm mc = new McDatInitialPrm() ) {
				_chkEnableEsf.Checked = mc.EnableEsf;
				_chkEnableGsf.Checked = mc.EnableGsf;
			}
		}
		/// <summary>保存</summary>
		private void Save()
		{
			using( McDatInitialPrm mc = new McDatInitialPrm() ) {
				mc.Read();
				mc.EnableEsf = _chkEnableEsf.Checked;
				mc.EnableGsf = _chkEnableGsf.Checked;
				mc.Write();
			}
			using( McDatRomSwitch ms = new McDatRomSwitch() ) {
				ms.Read();
				ms.EnableAxisA = _chkEnableAxisA.Checked;
				ms.EnableAxisB = _chkEnableAxisB.Checked;
				ms.EnableAxisC = _chkEnableAxisC.Checked;
				ms.Write();
			}
		}
	}
}
