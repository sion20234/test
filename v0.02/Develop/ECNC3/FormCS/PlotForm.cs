///////////////////////////////////////////////////////////////////////////////////////////
//
// (1) システム名称 : ELENIX ECNCⅢ
// (2) ファイル名   : PlotForm.cs
// (3) 概要         : プロット画面
// (4) 作成日       : 2016.07.19
// (5) 作成者       : 八野
// (6) 特記事項     : VisualStudio2015 C#
// (7) 更新履歴     :
//
///////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Windows.Forms;

namespace ECNC3.Views
{
    /// <summary>
    /// プロット画面
    /// </summary>
    public partial class PlotForm : ECNC3Form
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PlotForm()
        {
            InitializeComponent();
        }
       
        /// <summary>
        /// フォームを閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">BackMDIAUTOFormBtのボタンクリック時のイベント</param>
        private void BackMDIAUTOFormBt_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
