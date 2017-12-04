using ECNC3.Models;
using ECNC3.Views.Popup;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ECNC3.Views
{
    #region Enums
    /// <summary>
    /// ProtectCommandsDialogを何のデータ用で開くか
    /// </summary>
    public enum ProtectParamCategory
    {
        /// <summary>
        /// 加工条件データ用
        /// </summary>
        ProcessConditions,
        /// <summary>
        /// 仮想点データ用
        /// </summary>
        VirtualPositions,
        /// <summary>
        /// ティーチングテーブル用
        /// </summary>
        TeachingTable
    }
    #endregion
    /// <summary>
    /// 2017/06/02 作成者：八野
    /// リスト項目書き込み保護設定ダイアログクラス
    /// </summary>
    /// <remarks>
    /// 各データ用の処理になっているので、
    /// データを追加する場合はProtectParamCategoryに項目を追加し、
    /// クラス内処理のCase文を追加する。 
    /// </remarks>
    public partial class ProtectCommandsDialog : ECNC3Form
    {
        #region Constractor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="title"></param>
        /// <param name="paramCat"></param>
        /// <param name="parentButton"></param>
        /// <param name="visibleList"></param>
        public ProtectCommandsDialog(string title, ProtectParamCategory paramCat, 
                                        ButtonEx parentButton,
                                        bool visibleList)
        {
            InitializeComponent();
            _visibleList = visibleList;
            _titleLabel.Text = title;
            //枠線表示
            SelectFormInit();
            base.targetBtn = parentButton;
            _mode = paramCat;
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="title"></param>
        /// <param name="paramCat"></param>
        /// <param name="parentButton"></param>
        /// <param name="visibleList"></param>
        public ProtectCommandsDialog(string title, ProtectParamCategory paramCat,
                                        ButtonEx parentButton,
                                        int startIndex,
                                        bool visibleList)
        {
            InitializeComponent();
            _visibleList = visibleList;
            _titleLabel.Text = title;
            //初期値
            _singleSelectEdit.Text = startIndex.ToString();
            _multiSelectStartEdit.Text = startIndex.ToString();
            //枠線表示
            SelectFormInit();
            base.targetBtn = parentButton;
            _mode = paramCat;
        }
        #endregion

        #region ValiableMembers
        /// <summary>
        /// 入力値のリスト項目にたいする処理
        /// </summary>
        private enum Commands
        {
            /// <summary>
            /// 項目をロックする
            /// </summary>
            Lock,
            /// <summary>
            /// 項目をアンロックする
            /// </summary>
            Unlock
        }

        /// <summary>
        /// ロックするIndexの入力方式
        /// </summary>
        private enum LockMode
        {
            /// <summary>
            /// 単/複数入力
            /// </summary>
            singleSelect,
            /// <summary>
            /// 範囲入力
            /// </summary>
            multiSelect
        }
        /// <summary>
        /// リスト表示ON
        /// </summary>
        bool _visibleList = false;
        /// <summary>
        /// パラメーター種別
        /// </summary>
        ProtectParamCategory _mode;
        /// <summary>
        /// 入力方式
        /// </summary>
        LockMode _lockMode;
        /// <summary>
        /// 最大値
        /// </summary>
        private int _upperValue = -1;
        /// <summary>
        /// 最小値
        /// </summary>
        private int _minValue = -1;
        /// <summary>
        /// 加工条件Tempデータ
        /// </summary>
        FileProcessCondition PcondItems = null;
        /// <summary>
        /// 仮想点Tempデータ
        /// </summary>
        FileOrgPos VirPosItems = null;
        /// <summary>
        /// テンキー
        /// </summary>
        TenKeyDialog _popupTenkey = null;
        /// <summary>
        /// 編集中テキストボックス名保持
        /// </summary>
        private string _editingTextBoxName = "";
        /// <summary>
        /// パラメーターのリスト表示時に使用
        /// </summary>
        ListViewDialog listDialog = null;
        #endregion

        #region PublicMembers
        /// <summary>
        /// ダイアログモードで表示する。
        /// </summary>
        /// <param name="owner">呼び出し元のフォーム（親子関係をつける）</param>
        /// <param name="title">タイトル</param>
        /// <param name="paramCat">
        /// ProtectParamCategory.ProcessConditions = 加工条件モード
        /// ProtectParamCategory.VirtualPositions = 仮想点モード
        /// </param>
        /// <param name="parentButton">呼び出し元のボタン</param>
        static public void ShowSubForm(IWin32Window owner, string title,
                                       ProtectParamCategory paramCat,
                                       ButtonEx parentButton,
                                       bool visibleList = false)
        {
            ProtectCommandsDialog f = new ProtectCommandsDialog(title, paramCat, parentButton, visibleList);
            f.ShowDialog(owner);
            f.Dispose();
        }
        /// <summary>
        /// ダイアログモードで表示する。
        /// </summary>
        /// <param name="owner">呼び出し元のフォーム（親子関係をつける）</param>
        /// <param name="title">タイトル</param>
        /// <param name="paramCat">
        /// ProtectParamCategory.ProcessConditions = 加工条件モード
        /// ProtectParamCategory.VirtualPositions = 仮想点モード
        /// </param>
        /// <param name="parentButton">呼び出し元のボタン</param>
        static public void ShowSubForm(IWin32Window owner, 
                                       string title,
                                       ProtectParamCategory paramCat,
                                       ButtonEx parentButton,
                                       int startIndex,
                                       bool visibleList = false)
        {
            ProtectCommandsDialog f = new ProtectCommandsDialog
                (
                title,
                paramCat, 
                parentButton,
                startIndex,
                visibleList
                );
            f.ShowDialog(owner);
            f.Dispose();
        }
        #endregion

        #region EventHandler
        private void ProtectCommandsDialog_Load(object sender, EventArgs e)
        {
            _initializeData();
            _init();
            _lockSelectModeChg(LockMode.singleSelect);
            if (_visibleList == true)
            {
                string title = (_mode == ProtectParamCategory.ProcessConditions) ? "加工条件テーブル" : "仮想点テーブル";
                if (listDialog != null) listDialog.Dispose(); listDialog = null;
                if (listDialog == null) listDialog = new ListViewDialog(title, _mode);
                listDialog.Location = new System.Drawing.Point(this.Location.X + this.Width, this.Location.Y);

                listDialog.Show(this);
            }
        }

        private void CancelBt_Click(object sender, EventArgs e)
        {
            DisposeMember();
            this.Close();
        }

        private void _lockBtn_MouseUp(object sender, MouseEventArgs e)
        {
            Close(true, true, Commands.Lock);
        }
        private void _unlockBtn_MouseUp(object sender, MouseEventArgs e)
        {
            Close(true, true, Commands.Unlock);
        }
        /// <summary>
        /// フォームを閉じる
        /// </summary>
        /// <param name="writeData"></param>
        /// <param name="writeCmd"></param>
        private void Close
            (
            bool checkSum,                          //入力値チェック
            bool writeData,                         //書込み保護設定の有り無し
            Commands writeCmd = Commands.Unlock     //設定/解除
            )
        {
            if (!_chkEditValue())
            {
                using (MessageDialog msgDir = new MessageDialog())
                {
                    msgDir.Information(5029, this);
                }
                return;
            }
            if (writeData == true) _writeData(writeCmd);
            if (_visibleList == true)
            {
                listDialog.Close();
                listDialog = null;

            }
            DisposeMember();
            this.Close();
        }
        private void _multiSelectEndEdit_MouseUp(object sender, MouseEventArgs e)
        {
			_editingTextBoxName = ( sender as Control ).Name;
            _popupTenkey = new TenKeyDialog((sender as Control).Text, NumericTextBox.FormatTypes.Integer5,
                               _minValue, _upperValue, true, false, false, "protectDlg_Edit", (sender as Control));
            _popupTenkey.NotifyReturnOk = popupTenkey_OnNotifyReturnOk; //Tenkeyからのイベント通知：OK
            _popupTenkey.FormClosing += popupTenkey_FormClosing;        //Tenkeyからのイベント通知：閉じる
            _popupTenkey.ShowDialog(this);
        }
        #endregion

        #region Initialize
        /// <summary>
        /// 初期化（パラメータ種別確認、パラメータ読み込み、反映）
        /// </summary>
        private void _init()
        {
            switch (_mode)
            {
                case ProtectParamCategory.ProcessConditions:
                    _minValue = 0;
                    _upperValue = 999;
                    break;

                case ProtectParamCategory.VirtualPositions:
                    _minValue = 1;
                    _upperValue = 1000;
                    break;

            }
            _enableValueLabel.Text = _minValue.ToString() + " - " + _upperValue.ToString();
            _lockSelectModeChg(LockMode.singleSelect);
        }
        /// <summary>
        /// リストデータの初期化（読込み）
        /// </summary>
        private void _initializeData()
        {
            switch (_mode)
            {
                case ProtectParamCategory.ProcessConditions:
                    //加工条件ファイル読み書きクラス
                    PcondItems = new FileProcessCondition();
                    //加工条件ファイルの読み込み。
                    PcondItems.Read();
                    break;

                case ProtectParamCategory.VirtualPositions:
                    //仮想点ファイル読み書きクラス
                    VirPosItems = new FileOrgPos();
                    //仮想点ファイルの読み込み。
                    VirPosItems.Read();
                    break;

            }
        }
        #endregion

        #region Lock/UnLock Process
        /// <summary>
        /// 入力値が有効かの確認
        /// </summary>
        /// <returns>true:false == 有効：無効</returns>
        private bool _chkEditValue()
        {
            int singleValue = 0;
            int multiStartValue = 0;
            int multiEndValue = 0;
            //入力方式分岐
            switch (_lockMode)
            {
                case LockMode.singleSelect:
                    //数字のみか
                    if (!int.TryParse(_singleSelectEdit.Text, out singleValue)) return false;
                    //範囲内か
                    if (singleValue < 0 || singleValue > _upperValue) return false;
                    break;

                case LockMode.multiSelect:
                    //数字のみか
                    if (!int.TryParse(_multiSelectStartEdit.Text, out multiStartValue)) return false;
                    if (!int.TryParse(_multiSelectEndEdit.Text, out multiEndValue)) return false;
                    //範囲内か
                    if (multiStartValue < 0 || multiStartValue > _upperValue) return false;
                    if (multiEndValue < 0 || multiEndValue > _upperValue) return false;
                    //範囲入力の開始と終了の値が正しいか（開始<=終了）
                    if (multiStartValue > multiEndValue) return false;
                    break;

            }
            return true;
        }
        /// <summary>
        /// データのファイル反映（書込み）
        /// </summary>
        /// <param name="cmd">
        /// Commands.Lock = 入力値をロックして反映
        /// Commands.UnLock = 入力値をアンロックして反映
        /// </param>
        private void _writeData(Commands cmd)
        {
            int lockCommand = (cmd == Commands.Lock) ? 1 : 0;
            List<int> tempData = _lockDataTemp();
            if( tempData == null )
            {
                using (MessageDialog msgDir = new MessageDialog())
                {
                    msgDir.Error(5005, this);
                    return;
                }
            }
            int dataCt = 0;
            switch (_mode)
            {
                //加工条件の場合
                case ProtectParamCategory.ProcessConditions:
                    //加工条件ファイル読み書きクラス
                    if (PcondItems == null) PcondItems = new FileProcessCondition();
                    //加工条件ファイルの読み込み。
                    PcondItems.Read();

                    for (int ct = 0; ct <= PcondItems.Items.Count - 1; ct++)
                    {
                        if (tempData.Contains(PcondItems.Items[ct].Number))
                        {
                            //プロテクト値変更
                            PcondItems.Items[ct].Protect = lockCommand;
                            PcondItems.Write(PcondItems.Items[ct]);
                            dataCt++;
                        }
                    }
                    break;

                //仮想点の場合
                case ProtectParamCategory.VirtualPositions:
                    //加工条件ファイル読み書きクラス
                    if (VirPosItems == null) VirPosItems = new FileOrgPos();
                    //加工条件ファイルの読み込み。
                    VirPosItems.Read();
                    for (int ct = 1; ct <= VirPosItems.Items.Count - 2; ct++)
                    {
                        if (tempData.Contains(VirPosItems.Items[ct].Number))
                        {
                            //プロテクト値変更
                            VirPosItems.Items[ct].Protect = lockCommand;
                            VirPosItems.Write(VirPosItems.Items[ct]);
                            dataCt++;
                        }
                    }
                    break;

            }
        }
        /// <summary>
        /// エディタから値をとってくる
        /// </summary>
        /// <returns>テキストボックスの入力値を配列（List<int>）で返す</returns>
        private List<int> _lockDataTemp()
        {
            List<int> retData = new List<int>();
            switch (_lockMode)
            {
                case LockMode.singleSelect:
                    int tempTextData = 0;
                    if (false == int.TryParse(_singleSelectEdit.Text, out tempTextData)) return null;
                    if (_singleSelectEdit.Text.Contains("."))
                    {
                        foreach (string temp in _singleSelectEdit.Text.Split('.'))
                        {
                            retData.Add(int.Parse(temp));
                        }
                    }
                    else
                    {
                        retData.Add(tempTextData);
                    }
                    break;

                case LockMode.multiSelect:
                    int tempTextStartData = 0;
                    int tempTextEndData = 0;
                    if (false == int.TryParse(_multiSelectStartEdit.Text, out tempTextStartData)) return null;
                    if (false == int.TryParse(_multiSelectEndEdit.Text, out tempTextEndData)) return null;
                    for (int countStart = tempTextStartData;
                        countStart <= tempTextEndData;
                        countStart++)
                    {
                        retData.Add(countStart);
                    }
                    break;

            }
            return retData;
        }
        /// <summary>
        /// クラス内破棄処理
        /// </summary>
        private void DisposeMember()
        {
            if (PcondItems != null)
            {
                PcondItems.Dispose();
                PcondItems = null;
            }
            if (VirPosItems != null)
            {
                VirPosItems.Dispose();
                VirPosItems = null;
            }
        }

        private void _multiSelectBtn_MouseUp(object sender, MouseEventArgs e)
        {
            if (!_multiSelectBtn.GetBack())
            {
                _lockSelectModeChg(LockMode.multiSelect);
            }
            else
            {
                _lockSelectModeChg(LockMode.singleSelect);
            }
        }
        /// <summary>
        /// ロック値編集入力方式変更
        /// </summary>
        /// <param name="mode">
        /// singleSelect = 単/複数入力
        /// multiSelect = 範囲入力
        /// </param>
        private void _lockSelectModeChg(LockMode mode)
        {
            switch (mode)
            {
                case LockMode.singleSelect:
                    _singleSelectEdit.Visible = true;
                    _multiSelectBtn.SetBack(false);
                    _multiSelectStartEdit.Visible = false;
                    _multiSelectEndEdit.Visible = false;
                    _multiSelectSubLabel.Visible = false;
                    _multiSelectStartLabel.Visible = false;
                    _multiSelectEndLabel.Visible = false;
                    break;

                case LockMode.multiSelect:
                    _singleSelectEdit.Visible = false;
                    _multiSelectBtn.SetBack(true);
                    _multiSelectStartEdit.Visible = true;
                    _multiSelectEndEdit.Visible = true;
                    _multiSelectSubLabel.Visible = true;
                    _multiSelectStartLabel.Visible = true;
                    _multiSelectEndLabel.Visible = true;
                    break;

            }
            _lockMode = mode;
        }
        #endregion

        #region 《ポップアップテンキー》
        /// <summary>
        /// ポップアップテンキー：「OK」ボタンを押した時、入力値をこのクラスに反映
        /// </summary>
        private void popupTenkey_OnNotifyReturnOk()
        {
            string retVal = _popupTenkey._tenkeyValReturn;  //ポップアップテンキーで編集された値
            if (_singleSelectEdit.Name == _editingTextBoxName)
            {
                _singleSelectEdit.Text = retVal;
                return;
            }
            if (_multiSelectStartEdit.Name == _editingTextBoxName)
            {
                _multiSelectStartEdit.Text = retVal;
                return;
            }
            if (_multiSelectEndEdit.Name == _editingTextBoxName)
            {
                _multiSelectEndEdit.Text = retVal;
                return;
            }
        }
        /// <summary>
        /// ポップアップテンキー：フォーム閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popupTenkey_FormClosing(object sender, FormClosingEventArgs e)
        {
            _popupTenkey = null;
            foreach (Control Ctrl in this.Controls)
            {
                if (typeof(ButtonEx) == Ctrl.GetType())
                {
                    ButtonEx SelBt = Ctrl as ButtonEx;
                    if (SelBt.GetBack() == true)
                    {
                        (Ctrl as ButtonEx).IsActive = false;
                        (Ctrl as ButtonEx).SetSelected(false);
                    }
                }
            }
        }
        #endregion
    }
}
