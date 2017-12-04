///////////////////////////////////////////////////////////////////////////////////////////
//
// (1) システム名称 : ELENIX ECNCⅢ
// (2) ファイル名   : MakerServiceSettingSoftForm.cs
// (3) 概要         : メーカーサービス：設定ソフト画面
// (4) 作成日       : 2017.04.03
// (5) 作成者       : 柏原
// (6) 特記事項     : VisualStudio2015 C#
// (7) 更新履歴     :
//
///////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Windows.Forms;

namespace ECNC3.Views
{
	public partial class MakerServiceSettingSoftForm : ECNC3Form
	{
        /// <summary>
        /// コンストラクタ/コンポーネント初期化
        /// </summary>
		public MakerServiceSettingSoftForm()
		{
			InitializeComponent();
		}
        /// <summary>
        /// セッティングPC：ボタン：クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEx_SettingPC_Click( object sender, EventArgs e )
        {//Rt64ecdrv.exeを起動する
            try
            {
                System.Diagnostics.Process rtProcess = System.Diagnostics.Process.Start("Rt64ecdrv.exe");//EXEと同じフォルダ
                this.Close();//この画面を閉じる
            }
            catch (Exception exp)
            {
				ECNC3Exception.FileIOFilter( exp, this);
//				MessageBox.Show(exp.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
        }
		/// <summary>
		/// ROM SW：ボタン：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonEx_RomSW_Click( object sender, EventArgs e )
        {//Rt64ecswset.exeを起動する
            try
            {
                System.Diagnostics.Process rtProcess = System.Diagnostics.Process.Start("Rt64ecswset.exe");//EXEと同じフォルダ
                this.Close();//この画面を閉じる
            }
            catch (Exception exp)
            {
				ECNC3Exception.FileIOFilter( exp, this);
//				MessageBox.Show(exp.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        /// <summary>
        /// 閉じる：ボタン：クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEx_Back_Click( object sender, EventArgs e )
		{
			this.Close();
        }
		/// <summary>
		/// フォーム：閉じる
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MakerServiceSettingSoftForm_FormClosed( object sender, FormClosedEventArgs e )
		{
			this.Dispose();
		}
	}
}
