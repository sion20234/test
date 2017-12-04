///////////////////////////////////////////////////////////////////////////////////////////
//
// (1) システム名称 : ELENIX ECNCⅢ
// (2) ファイル名   : LogSettingVariableForm.cs
// (3) 概要         : 加工ログ変数設定画面
// (4) 作成日       : 2015.06.01
// (5) 作成者       : 八野
// (6) 特記事項     : VisualStudio2015 C#
// (7) 更新履歴     : ログ設定に追加：2017-06-15：柏原
//
///////////////////////////////////////////////////////////////////////////////////////////


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ECNC3.Views
{
    public partial class LogSettingVariableForm : ECNC3Form
    {
		/// <summary>
		/// フォーム　コンストラクタ
		/// </summary>
		public LogSettingVariableForm()
        {
            InitializeComponent();
        }
		/// <summary>
		/// フォーム　ロード時のイベントハンドラ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LogSettingVariableForm_Load(object sender, EventArgs e)
        {
        }
		/// <summary>
		/// 戻る　ボタン　クリック時のイベントハンドラ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonEx_Back_Click( object sender, EventArgs e )
		{
			this.Close();
		}
		/// <summary>
		/// データ移動：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonEx_Data_Move_Click( object sender, EventArgs e )
		{

		}
	}
}
