///////////////////////////////////////////////////////////////////////////////////////////
//
// (1) システム名称 : ELENIX ECNCⅢ
// (2) ファイル名   : Tenkey.cs
// (3) 概要         : テンキーコントロール
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
    /// テンキーコントロール
    /// </summary>
    public partial class LabelExAndButtonEx : UserControl
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LabelExAndButtonEx()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 選択状態設定/取得
        /// </summary>
        public bool Selected 
        {
            get { return ((buttonEx1.GetBack() == true) && (labelEx1.GetLamp() == true)) ? true : false; }
            set
            {
                if (buttonEx1.GetBack() != value) buttonEx1.SetBack(value);
                if (labelEx1.GetLamp() != value) labelEx1.SetLamp(value);
            }
        }
        /// <summary>
        /// 値の設定/取得
        /// </summary>
        public string Value
        {
            get { return buttonEx1.Text; }
            set { if (value != Value) buttonEx1.Text = value; }
        }
        /// <summary>
        /// タイトルの設定/取得
        /// </summary>
        public string Title
        {
            get { return labelEx1.Text; }
            set { if (value != Title) labelEx1.Text = value; }
        }
        /// <summary>
        /// ボタンとラベルにイベント処理を設定する。
        /// </summary>
        /// <param name="eventMethod"></param>
        public void ClickEventSet(EventHandler eventMethod)
        {
            labelEx1.Click += eventMethod;
            buttonEx1.Click += eventMethod;
        }

        private void LabelExAndButtonEx_Load(object sender, EventArgs e)
        {
            //初期値設定
            Selected = false;
            Value = "";
            Title = "";
        }
    }
}
