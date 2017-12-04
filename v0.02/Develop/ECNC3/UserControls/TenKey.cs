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
    public partial class Tenkey : UserControl
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Tenkey()
        {
            InitializeComponent();
        }

        /// <summary>
        /// テンキーのキークリック時のイベント
        /// </summary>
        public event  EventHandler keyEvent;
        public event EventHandler ACKeyEvent;
        public event EventHandler EnterEvent;
        public event EventHandler BackSpaceEvent;
        /// <summary>
        /// 参照用　テンキー入力値
        /// </summary>
        public string strRet { get; set; }
        /// <summary>
        /// エンターキークリック時：true
        /// 初期、通常時：false
        /// </summary>
        public bool EnterKeyEn { get; set; }
        /// <summary>
        /// ACキークリック時：true
        /// 初期、通常時：false
        /// </summary>
        public bool ACkeyEn { get; set; }

    /// <summary>
    /// フォームへのテンキークリックイベント発行処理
    /// </summary>
    public void keyEventStart()
        {
            if (keyEvent != null)
            {
                keyEvent(this, EventArgs.Empty);
            }
        }
        /// <summary>
        /// フォームへのACキークリックイベント発行処理
        /// </summary>
        private void ACkeyEventStart()
        {
            if(ACKeyEvent != null)
            {
                ACKeyEvent(this, EventArgs.Empty);
            }
        }
        /// <summary>
        /// フォームへのエンターキークリックイベント発行処理
        /// </summary>
        private void EnterKeyEventStart()
        {
            if(EnterEvent != null)
            {
                EnterEvent(this, EventArgs.Empty);
            }
        }
        /// <summary>
        /// フォームへのBSキークリックイベント発行処理
        /// </summary>
        private void BackSpaceKeyEventStart()
        {
            if(BackSpaceEvent != null)
            {
                BackSpaceEvent(this, EventArgs.Empty);
            }
        }
        /// <summary>
        /// keyEventの初期化(null)
        /// </summary>
        public void RefreshKeyEvent()
        {
            keyEvent = null;
        }

        /// <summary>
        /// 入力値、特殊キークリックフラグの初期化
        /// </summary>
        internal void InitializationPropaty()
        {
            strRet = "";
            EnterKeyEn = false;
            ACkeyEn = false;
        }
        /// <summary>
        /// テンキークリック時、入力値をstrRetに格納する。
        /// フォームへのイベント発行処理。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Key_Click(object sender, EventArgs e)
        {
            strRet = (sender as ButtonEx).Text;
            keyEventStart();
        }
        /// <summary>
        /// ACキークリック時、キー入力イベント発行処理。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ACKey_Click(object sender, EventArgs e)
        {
            ACkeyEventStart();
        }

        /// <summary>
        /// エンターキークリック時、キー入力イベント発行処理。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnterKey_Click(object sender, EventArgs e)
        {
            EnterKeyEventStart();
        }

        /// <summary>
        /// BackSpaceキークリック時、キー入力イベント発行処理。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackSpaceKey_Click(object sender, EventArgs e)
        {
            BackSpaceKeyEventStart();
        }
    }
}
