///////////////////////////////////////////////////////////////////////////////////////////
//
// (1) システム名称 : ELENIX ECNCⅢ
// (2) ファイル名   : GSFMainForm.cs
// (3) 概要         : GSF設定画面
// (4) 作成日       : 2015.06.01
// (5) 作成者       : 八野
// (6) 特記事項     : VisualStudio2015 C#
// (7) 更新履歴     : 2017.02.07：ポップアップテンキー処理追加：柏原
//                    2017.11.27：GSF画像処理追加：柏原
///////////////////////////////////////////////////////////////////////////////////////////
using ECNC3.Enumeration;
using ECNC3.Models;
using ECNC3.Models.McIf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ECNC3.Views
{
    public partial class GSFMainForm : ECNC3Form
    {
        #region Enums
        enum GsfSetting
        {
            /// <summary>
            /// ガイド交換位置始点X
            /// </summary>
            GuideExchStartPosX,
            /// <summary>
            /// ガイド交換位置始点Y
            /// </summary>
            GuideExchStartPosY,
            /// <summary>
            /// ガイド交換位置始点W
            /// </summary>
            GuideExchStartPosW,
            /// <summary>
            /// ガイド交換位置始点ABC
            /// </summary>
            GuideExchStartPosABC,
            /// <summary>
            /// ガイド交換位置始点X
            /// </summary>
            GuideExchEndPosX,
            /// <summary>
            /// ガイド交換位置始点Y
            /// </summary>
            GuideExchEndPosY,
            /// <summary>
            /// W軸電極交換前位置オフセット
            /// </summary>
            GuideExchOfsW1,
            /// <summary>
            /// W軸電極交換待機位置オフセット
            /// </summary>
            GuideExchOfsW2,
            /// <summary>
            /// 軸ガイド交換位置移動速度
            /// </summary>
            GuideExchSpdW,
            /// <summary>
            /// ガイド有無センサー位置X
            /// </summary>
            GuideChkPosX,
            /// <summary>
            /// ガイド有無センサー位置Y
            /// </summary>
            GuideChkPosY,
            /// <summary>
            /// ESF電極搭載数
            /// </summary>
            GsfMaxNumSet
        }
        #endregion
        #region Constractor
        /// <summary>
        /// フォーム　コンストラクタ
        /// </summary>
        public GSFMainForm()
        {
            InitializeComponent();
            Disposed += GSFMainForm_Disposed;

            //下3桁か4桁か取得：サブミクロン
            CncSys_AxisInfomation_XmlRead(ref _submicronFlg);
            if (_submicronFlg) _strSubmicronValue = "###0'.'0000";
            else _strSubmicronValue = "###0'.'000";
            //GSF画像読み込み
            ImageDisplay("GSF");
            //ImageDisplay("pictureBox1.BackgroundImage");
        }
        #endregion
        #region VariableMember
        private string _strSubmicronValue = "";
        private bool _submicronFlg = false;
        /// <summary>ポップアップテンキー</summary>
        private TenKeyDialog _popupTenkey = new TenKeyDialog("", 0, 0, 0);//初回インスタンスを作っておく
        /// <summary>搭載電極数</summary>
        private int GuideCountUpp = 0;
        /// <summary>AECシーケンス項目</summary>
        private AECSequences AecSeq = AECSequences.Null;

        /// <summary>設定結果取得関数定義</summary>
        public delegate void NotifyReturnDelegate();
        /// <summary>終了通知</summary>
        private NotifyReturnDelegate _notifyReturn = null;
        /// <summary>>設定結果取得</summary>
        public NotifyReturnDelegate NotifyReturn
        {
            set { _notifyReturn = value; }
        }
        #endregion
        #region Dispose
        private void GSFMainForm_Disposed(object sender, EventArgs e)
        {
            if (null != _notifyReturn)
            {
                _notifyReturn = null;
            }
        }
        #endregion
        #region LoopMethod
        internal void StatusMonitoring(StatusMonitorEventArgs e)
        {
            if (Disposing)
            {
                return;
            }
            if (this == null)
            {
                return;
            }
            if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
            {
                //機械座標表示
                UpdateAxisPosition(e.Items.MacAxisPos);
                //AEC表示更新
                AecLampChg(e.Items);
            }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
            if (e.Items.StartSwBtnOffEdge == true
                && _popupTenkey.Visible == false)
            {
                enableSequence = true;
                //AEC動作
                AecSequence(e.Items);
            }
            if (e.Items.FGEnd == true)
            {
                if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                {
                    _AllButtonsActivate(this.Controls);
                    if (enableSequence == true)
                    {
                        _AllControlsBackColorChg(false);
                        enableSequence = false;
                    }
                }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
            }
            else
            {
                if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                {
                    _AllButtonsDeActivate(this.Controls);
                }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
            }
        }
        bool enableSequence = false;
        private void _AllButtonsActivate(Control.ControlCollection target)
        {
            if (target == null) return;
            foreach (Control ctrl in target)
            {
                if (ctrl.Controls.Count != 0) _AllButtonsActivate(ctrl.Controls);
                if (ctrl.GetType() == typeof(ButtonEx))
                {
                    if ((ctrl as ButtonEx).Enabled != true) (ctrl as ButtonEx).Enabled = true;
                }
            }
        }
        private void _AllButtonsDeActivate(Control.ControlCollection target)
        {
            if (target == null) return;
            foreach (Control ctrl in target)
            {
                if (ctrl.Controls.Count != 0) _AllButtonsDeActivate(ctrl.Controls);
                if (ctrl.GetType() == typeof(ButtonEx))
                {
                    if ((ctrl as ButtonEx).Enabled != false) (ctrl as ButtonEx).Enabled = false;
                }
            }
        }
        /// <summary>
        ///  CncSys.xml：軸関係情報設定<AxisInfomation>からサブミクロン(下3か4桁)表示を取得
        /// </summary>
        /// <param name="submicronFlg"></param>
        private void CncSys_AxisInfomation_XmlRead(ref bool submicronFlg)
        {
            int intDigit = 0;
            try
            {
                using (FileSettings fs = new FileSettings())
                {
                    //ファイル読み込み
                    fs.Read();
                    //サブミクロン表示(submicron)：取得
                    intDigit = fs.AttrValue("Root/AxisInfomation/Position ", "digit");
                    if (intDigit == 4)
                    {
                        submicronFlg = true;
                    }
                    else
                    {
                        submicronFlg = false;
                    }
                }
            }
            catch (Exception exc)
            {
                //例外処理
                MessageBox.Show(exc.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        /// <summary>
        /// ポップアップテンキーで「OK」ボタンを押した時、入力値をこのクラスに反映
        /// </summary>
        private void popupTenkey_OnNotifyReturn()
        {
        }
        /// <summary>
        /// 閉じる　ボタン　クリック時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackToServiceFormBt_Click(object sender, EventArgs e)
        {
            _notifyReturn?.Invoke();
        }

        private void GSFMainForm_Load(object sender, EventArgs e)
        {
            UpdateSettingParameter();
            UpdateAxisParameter();
            InitInput(_GsfMaxNomTextBox, NumericTextBox.FormatTypes.Integer1);
            _GsfMaxNomTextBox.LowerLimit = 0;
            _GsfMaxNomTextBox.UpperLimit = 6;

            InitInput(_GsfPositioningMoveTextBox, NumericTextBox.FormatTypes.Integer1);
            _GsfPositioningMoveTextBox.LowerLimit = 0;
            _GsfPositioningMoveTextBox.UpperLimit = 15;
        }

        #region ControlsMethod
        private void _AllControlsBackColorChg(bool backOn)
        {
            foreach (Control ctrl in Controls)
            {
                if (ctrl.GetType() == typeof(ButtonEx))
                {
                    (ctrl as ButtonEx).SetBack(backOn);
                }
                if (ctrl.Controls.Count == 0) continue;
                foreach (Control subCtrl in ctrl.Controls)
                {
                    if (subCtrl.GetType() == typeof(ButtonEx))
                    {
                        (subCtrl as ButtonEx).SetBack(backOn);
                    }
                }
            }
        }

        /// <summary>
        /// Aec操作用ボタンランプ変更
        /// </summary>
        /// <param name="items"></param>
        private void AecLampChg(MonitoringItems items)
        {

            //GSFクランプ
            if (items.GuideHolderClampOn != _GuideClampBtn.GetLed())
            {
                _GuideClampBtn.SetLed(items.GuideHolderClampOn);
            }
            if (!items.GuideHolderClampOn != _GuideUnClampBtn.GetLed())
            {
                _GuideUnClampBtn.SetLed(!items.GuideHolderClampOn);
            }
            //GSFアーム位置
            if (items.GsfArmBack != _GsfArmBackBtn.GetLed())
            {
                _GsfArmBackBtn.SetLed(items.GsfArmBack);
            }
            if (items.GsfArmFoward != _GsfArmFrontBtn.GetLed())
            {
                _GsfArmFrontBtn.SetLed(items.GsfArmFoward);
            }
        }
        #region ExcuteSet&SeqMethods

        internal ResultCodes PositioningAxis(
                                            MonitoringItems Items,
                                            int targetPos,
                                            AxisName axis,
                                            AxisMoveMode mode,
                                            int targetPos2 = 0,
                                            AxisName axis2 = AxisName.Null,
                                            int targetPos3 = 0,
                                            AxisName axis3 = AxisName.Null
                                            )
        {
            //座標クラス定義
            List<Models.StructurePositioniingItem> PosItems = new List<Models.StructurePositioniingItem>();
            //初期化
            for (int ct = 0; ct < 9; ct++)
            {
                PosItems.Add(new Models.StructurePositioniingItem());
            }
            foreach (Models.StructurePositioniingItem positem in PosItems)
            {
                positem.Movable = false;
                positem.TargetPosition = 0;
            }

            if ((this.IsDisposed == true || this.Disposing == true)) return ResultCodes.NotFound;
            retInvoke = BeginInvoke((MethodInvoker)delegate
            {
                if (axis == AxisName.Null)
                {
                    MessageBox.Show("対象軸が未選択です。"); return;
                }

                switch (axis)
                {
                    case AxisName.AxisX:
                        PosItems[0].Movable = true;
                        PosItems[0].TargetPosition = targetPos;
                        break;

                    case AxisName.AxisY:
                        PosItems[1].Movable = true;
                        PosItems[1].TargetPosition = targetPos;
                        break;

                    case AxisName.AxisW:
                        PosItems[2].Movable = true;
                        PosItems[2].TargetPosition = targetPos;
                        break;

                    case AxisName.AxisZ:
                        PosItems[3].Movable = true;
                        PosItems[3].TargetPosition = targetPos;
                        break;

                    case AxisName.AxisA:
                        PosItems[4].Movable = true;
                        PosItems[4].TargetPosition = targetPos;
                        break;

                    case AxisName.AxisB:
                        PosItems[5].Movable = true;
                        PosItems[5].TargetPosition = targetPos;
                        break;

                    case AxisName.AxisC:
                        PosItems[6].Movable = true;
                        PosItems[6].TargetPosition = targetPos;
                        break;

                    case AxisName.AxisI:
                        PosItems[7].Movable = true;
                        PosItems[7].TargetPosition = targetPos;
                        break;

                }
                //2軸移動
                if (axis2 != AxisName.Null)
                {
                    switch (axis2)
                    {
                        case AxisName.AxisX:
                            PosItems[0].Movable = true;
                            PosItems[0].TargetPosition = targetPos2;
                            break;

                        case AxisName.AxisY:
                            PosItems[1].Movable = true;
                            PosItems[1].TargetPosition = targetPos2;
                            break;

                        case AxisName.AxisW:
                            PosItems[2].Movable = true;
                            PosItems[2].TargetPosition = targetPos2;
                            break;

                        case AxisName.AxisZ:
                            PosItems[3].Movable = true;
                            PosItems[3].TargetPosition = targetPos2;
                            break;

                        case AxisName.AxisA:
                            PosItems[4].Movable = true;
                            PosItems[4].TargetPosition = targetPos2;
                            break;

                        case AxisName.AxisB:
                            PosItems[5].Movable = true;
                            PosItems[5].TargetPosition = targetPos2;
                            break;

                        case AxisName.AxisC:
                            PosItems[6].Movable = true;
                            PosItems[6].TargetPosition = targetPos2;
                            break;

                        case AxisName.AxisI:
                            PosItems[7].Movable = true;
                            PosItems[7].TargetPosition = targetPos2;
                            break;

                    }
                }
                //3軸移動
                if (axis3 != AxisName.Null)
                {
                    switch (axis3)
                    {
                        case AxisName.AxisX:
                            PosItems[0].Movable = true;
                            PosItems[0].TargetPosition = targetPos3;
                            break;

                        case AxisName.AxisY:
                            PosItems[1].Movable = true;
                            PosItems[1].TargetPosition = targetPos3;
                            break;

                        case AxisName.AxisW:
                            PosItems[2].Movable = true;
                            PosItems[2].TargetPosition = targetPos3;
                            break;

                        case AxisName.AxisZ:
                            PosItems[3].Movable = true;
                            PosItems[3].TargetPosition = targetPos3;
                            break;

                        case AxisName.AxisA:
                            PosItems[4].Movable = true;
                            PosItems[4].TargetPosition = targetPos3;
                            break;

                        case AxisName.AxisB:
                            PosItems[5].Movable = true;
                            PosItems[5].TargetPosition = targetPos3;
                            break;

                        case AxisName.AxisC:
                            PosItems[6].Movable = true;
                            PosItems[6].TargetPosition = targetPos3;
                            break;

                        case AxisName.AxisI:
                            PosItems[7].Movable = true;
                            PosItems[7].TargetPosition = targetPos3;
                            break;

                    }
                }
            }); if ((this.IsDisposed == true || this.Disposing == true)) return ResultCodes.NotFound; EndInvoke(retInvoke);
            ResultCodes Error = ResultCodes.NotFound;
            //軸移動パラメータ有
            using (SequenceFunction SeqFunc = new SequenceFunction(PosItems))
            {
                using (MonitoringFunction MonitorFunc = new MonitoringFunction())
                {
                    try
                    {
                        //動作完確認
                        if (Items.FGEnd == true)
                        {

                            UILog logs = new UILog("MAINForm.MANUAL.NumForm.SeqFunc.SequenceMonitoring");
                            //	入力座標
                            switch (mode)
                            {
                                //軸移動実行
                                case AxisMoveMode.Abs:
                                    Error = SeqFunc.SequenceMonitoring(Sequences.AbsoluteMacMovePointToPoint);
                                    //ログ処理
                                    logs.Error("ABSPTPMOVE_MACHINE_START, " + "Result = " + Error);
                                    break;

                                case AxisMoveMode.Inc:
                                    Error = SeqFunc.SequenceMonitoring(Sequences.IncMovePointToPoint);
                                    //ログ処理
                                    logs.Error("INCPTPMOVE_START, " + "Result = " + Error);
                                    break;
                            }
                        }
                        else
                        {
                            if (true == InvokeRequired)
                            {
                                Invoke((MethodInvoker)delegate
                                {
                                    UILog logs = new UILog("MAINForm.MANUAL.NumForm.SeqFunc.SequenceMonitoring");
                                    logs.Error("FGEnd == false");
                                    using (MessageDialog msg = new MessageDialog())
                                    {
                                        msg.Warning(5022, this);
                                    }
                                }); if ((this.IsDisposed == true || this.Disposing == true)) return ResultCodes.NotFound; EndInvoke(retInvoke);
                            }
                        }
                    }
                    //例外表示
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        //座標データのメモリ解放
                        foreach (Models.StructurePositioniingItem positem in PosItems)
                        {
                            positem.Dispose();
                        }
                        PosItems.Clear();
                    }
                }
            }
            //動作後の処理
            if (true == InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {

                }); if ((this.IsDisposed == true || this.Disposing == true)) return ResultCodes.NotFound; EndInvoke(retInvoke);
            }
            return Error;
        }
        private void AecSequence(MonitoringItems items)
        {
            int Target = 0;
            //実行関数名の取得
            string _thisFuncName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            ECNC3.Enumeration.ResultCodes ret = Enumeration.ResultCodes.NotExecute;
            using (SequenceFunction SeqFunc = new SequenceFunction())
            {
                using (MonitoringFunction MonitorFunc = new MonitoringFunction())
                {
                    switch (AecSeq)
                    {
                        case AECSequences.Null:
                            {

                            }

                            break;

                        case AECSequences.GuideClamp:
                            if (MonitorFunc.SequenceConditionMonitoring(MonitorTargets.CompletedOrigin) == true)
                            {
                                ret = SeqFunc.SequenceMonitoring(Sequences.GuideClamp);
                            }
                            break;

                        case AECSequences.GuideUnClamp:
                            if (MonitorFunc.SequenceConditionMonitoring(MonitorTargets.CompletedOrigin) == true)
                            {
                                ret = SeqFunc.SequenceMonitoring(Sequences.GuideUnClamp);
                            }
                            break;

                        case AECSequences.GsfArmBack:
                            if (MonitorFunc.SequenceConditionMonitoring(MonitorTargets.CompletedOrigin) == true)
                            {
                                ret = SeqFunc.SequenceMonitoring(Sequences.GsfArmBack);
                            }
                            break;

                        case AECSequences.GsfArmFront:
                            if (MonitorFunc.SequenceConditionMonitoring(MonitorTargets.CompletedOrigin) == true)
                            {
                                ret = SeqFunc.SequenceMonitoring(Sequences.GsfArmFront);
                            }
                            break;

                        case AECSequences.GuideExchStartPosX:
                            ChkPosText(label_XSt_Val.Text, out Target);
                            ret = PositioningAxis(items, Target, AxisName.AxisX, AxisMoveMode.Abs);
                            break;

                        case AECSequences.GuideExchEndPosX:
                            ChkPosText(label_XEnd_Val.Text, out Target);
                            ret = PositioningAxis(items, Target, AxisName.AxisX, AxisMoveMode.Abs);
                            break;

                        case AECSequences.GuideExchStartPosY:
                            ChkPosText(label_YSt_Val.Text, out Target);
                            ret = PositioningAxis(items, Target, AxisName.AxisY, AxisMoveMode.Abs);
                            break;

                        case AECSequences.GuideExchEndPosY:
                            ChkPosText(label_YEnd_Val.Text, out Target);
                            ret = PositioningAxis(items, Target, AxisName.AxisY, AxisMoveMode.Abs);
                            break;

                        case AECSequences.GuideExchStartPosW:
                            ChkPosText(label_W.Text, out Target);
                            ret = PositioningAxis(items, Target, AxisName.AxisW, AxisMoveMode.Abs);
                            break;

                        case AECSequences.GuideExchStartPosABC:
                            int targetA = 0;
                            int targetB = 0;
                            int targetC = 0;
                            ChkPosText(_guideExchPosAValue.Text, out targetA);
                            ChkPosText(_guideExchPosBValue.Text, out targetB);
                            ChkPosText(_guideExchPosCValue.Text, out targetC);
                            ret = PositioningAxis(items, targetA, AxisName.AxisA, AxisMoveMode.Abs,
                                                 targetB, AxisName.AxisB,
                                                 targetC, AxisName.AxisC
                                                 );
                            break;

                        case AECSequences.GuideExchOfsW1:
                            int targetW1 = 0;
                            int targetW1Ofs1 = 0;
                            ChkPosText(label_W_Val.Text, out targetW1);
                            ChkPosText(label_WOfs1_Val.Text, out targetW1Ofs1);
                            ret = PositioningAxis(items, targetW1 + targetW1Ofs1, AxisName.AxisW, AxisMoveMode.Abs);
                            break;

                        case AECSequences.GuideExchOfsW2:
                            int targetW2 = 0;
                            int targetW2Ofs2 = 0;
                            ChkPosText(label_W_Val.Text, out targetW2);
                            ChkPosText(label_WOfs2_Val.Text, out targetW2Ofs2);
                            ret = PositioningAxis(items, targetW2 + targetW2Ofs2, AxisName.AxisW, AxisMoveMode.Abs);
                            break;

                    }
                }
            }
            if (ret == Enumeration.ResultCodes.Success)
            {
                if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                {
                    SettingSeqLampChg(AECSequences.Null);
                }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
            }
        }

        private ResultCodes SetInitialParam(GsfSetting esfSetting)
        {
            //動作モードが「Setting」モードになったら変更する。
            using (McDatInitialPrm ms = new McDatInitialPrm())
            {
                int outParameter = 0;
                int outParameter2 = 0;
                int outParameter3 = 0;
                ms.Read();
                switch (esfSetting)
                {
                    case GsfSetting.GuideExchStartPosX:
                        ChkPosText(_macPosXLabel.Text, out outParameter);
                        ms.GuideExchPosS[0] = outParameter;
                        break;

                    case GsfSetting.GuideExchEndPosX:
                        ChkPosText(_macPosXLabel.Text, out outParameter);
                        ms.GuideExchPosE[0] = outParameter;
                        break;

                    case GsfSetting.GuideExchStartPosY:
                        ChkPosText(_macPosYLabel.Text, out outParameter);
                        ms.GuideExchPosS[1] = outParameter;
                        break;

                    case GsfSetting.GuideExchEndPosY:
                        ChkPosText(_macPosXLabel.Text, out outParameter);
                        ms.GuideExchPosE[1] = outParameter;
                        break;

                    case GsfSetting.GuideExchStartPosW:
                        ChkPosText(_macPosWLabel.Text, out outParameter);
                        ms.GuideExchPosS[2] = outParameter;
                        break;

                    case GsfSetting.GuideExchStartPosABC:
                        ChkPosText(_macPosALabel.Text, out outParameter);
                        ms.GuideExchPosS[4] = outParameter;
                        ChkPosText(_macPosBLabel.Text, out outParameter);
                        ms.GuideExchPosS[5] = outParameter;
                        ChkPosText(_macPosCLabel.Text, out outParameter);
                        ms.GuideExchPosS[6] = outParameter;
                        break;

                    case GsfSetting.GuideExchOfsW1:
                        ChkPosText(label_W_Val.Text, out outParameter);
                        ChkPosText(_macPosWLabel.Text, out outParameter2);
                        ms.GuideExchOfsW1 = outParameter2 - outParameter;
                        break;

                    case GsfSetting.GuideExchOfsW2:
                        ChkPosText(label_W_Val.Text, out outParameter);
                        ChkPosText(_macPosWLabel.Text, out outParameter2);
                        ChkPosText(label_WOfs1_Val.Text, out outParameter3);
                        ms.GuideExchOfsW2 = outParameter2 - outParameter;
                        break;

                    case GsfSetting.GuideChkPosX:
                        ChkPosText(_macPosXLabel.Text, out outParameter);
                        ms.GuideChkPos[0] = outParameter;
                        break;

                    case GsfSetting.GuideChkPosY:
                        ChkPosText(_macPosXLabel.Text, out outParameter);
                        ms.GuideChkPos[1] = outParameter;
                        break;

                    case GsfSetting.GsfMaxNumSet:
                        ms.ElectrodeCount = (short)(_GsfMaxNomTextBox.Value);
                        break;

                }
                return ms.Write();
            }
        }

        #endregion

        /// <summary>
        /// 動作名表示とスタートボタンのトリガーに設定
        /// </summary>
        /// <param name="_seq">動作名</param>
        private void SettingSeqLampChg(AECSequences _seq)
        {
            AecSeq = _seq;
        }
        #endregion
        #region UpdateMethods
        private void UpdateAxisPosition(Models.StructureAxisCoordinate _macPos)
        {
            int bairitsu = 1;//倍率
            if (_submicronFlg) bairitsu = 10;
            _macPosXLabel.Text = (_macPos.AxisX * bairitsu).ToString(_strSubmicronValue);//.xxxか.xxxx表示
            _macPosYLabel.Text = (_macPos.AxisY * bairitsu).ToString(_strSubmicronValue);
            _macPosWLabel.Text = (_macPos.AxisW * bairitsu).ToString(_strSubmicronValue);
            _macPosZLabel.Text = (_macPos.AxisZ * bairitsu).ToString(_strSubmicronValue);
            _macPosALabel.Text = (_macPos.AxisA * bairitsu).ToString(_strSubmicronValue);
            _macPosBLabel.Text = (_macPos.AxisB * bairitsu).ToString(_strSubmicronValue);
            _macPosCLabel.Text = (_macPos.AxisC * bairitsu).ToString(_strSubmicronValue);
        }
        private void UpdateSettingParameter()
        {
            using (MonitoringFunction MonitorFunc = new MonitoringFunction())
            {
                GuideCountUpp = MonitorFunc.RetIntegerMonitoringResult(MonitorTargets.GuideCount);
            }
            using (McDatInitialPrm DatIni = new McDatInitialPrm())
            {
                DatIni.Read();
                if (_GsfMaxNomTextBox.Text == DatIni.GuideCount.ToString("0"))
                {
                    return;
                }
                _GsfMaxNomTextBox.Text = DatIni.GuideCount.ToString("0"); ;
            }
        }

        private void UpdateAxisParameter()
        {
            using (Models.McIf.McDatInitialPrm IniPrm = new Models.McIf.McDatInitialPrm())
            using (Models.McIf.McDatAecData AecDat = new Models.McIf.McDatAecData())
            {
                //初期化構造体のデータ読み込み
                IniPrm.Read();
                //AEC構造体のデータ読み込み
                AecDat.Read();

                int _partitionNo = AecDat.GuideNo;
                int bairitsu = 1;//倍率
                if (_submicronFlg) bairitsu = 10;
                label_XSt_Val.Text = (IniPrm.GuideExchPosS[0] * bairitsu).ToString(_strSubmicronValue);
                label_YSt_Val.Text = (IniPrm.GuideExchPosS[1] * bairitsu).ToString(_strSubmicronValue);
                label_XEnd_Val.Text = (IniPrm.GuideExchPosE[0] * bairitsu).ToString(_strSubmicronValue);
                label_YEnd_Val.Text = (IniPrm.GuideExchPosE[1] * bairitsu).ToString(_strSubmicronValue);
                label_W_Val.Text = (IniPrm.GuideExchPosS[2] * bairitsu).ToString(_strSubmicronValue);
                _guideExchPosAValue.Text = (IniPrm.GuideExchPosS[4] * bairitsu).ToString(_strSubmicronValue);
                _guideExchPosBValue.Text = (IniPrm.GuideExchPosS[5] * bairitsu).ToString(_strSubmicronValue);
                _guideExchPosCValue.Text = (IniPrm.GuideExchPosS[6] * bairitsu).ToString(_strSubmicronValue);
                label_WOfs1_Val.Text = (IniPrm.GuideExchOfsW1 * bairitsu).ToString(_strSubmicronValue);
                label_WOfs2_Val.Text = (IniPrm.GuideExchOfsW2 * bairitsu).ToString(_strSubmicronValue);
                label_WSpd_Val.Text = (IniPrm.GuideExchSpdW).ToString();
            }
        }
        #endregion
        #region<ポップアップテンキー>
        /// <summary>
        /// 記録する加工条件値
        /// </summary>
        object _controlName = "";
        /// <summary>
		/// ポップアップテンキー：表示/非表示
		/// </summary>
		/// <param name="val"></param>
		/// <returns>false=上下ポップアップを表示</returns>
		private bool popupTenkeyOn(object val)
        {
            _controlName = val;     //コントロール名を記録
            string changeVal = "";	//編集値
            Decimal lowerLimitDec = 0;//最小値
            Decimal upperLimitDec = 0;//最大値

            //フォーマットタイプ
            NumericTextBox.FormatTypes formatType = NumericTextBox.FormatTypes.Integer1;
            if (_popupTenkey != null)
            {
                _popupTenkey.Close();   //画面を閉じる
                _popupTenkey = null;    //null初期化
            }
            //ウインドタイトル
            string titleString = "";
            //最小/最大デフォルト
            lowerLimitDec = (decimal)000;
            upperLimitDec = (decimal)999;
            string strTenkeyMode = "";
            //クリックしたコントロールの下段コントロール値を取得
            switch (((Control)_controlName).Name)
            {
                case "_GsfPositioningMoveTextBox":   //ESF
                    titleString = _PositioningGsfGroupBox.Text;
                    changeVal = _GsfPositioningMoveTextBox.Text;
                    lowerLimitDec = (decimal)0;
                    upperLimitDec = (decimal)GuideCountUpp;
                    strTenkeyMode = "esf_crs";
                    //フォーマット：ゼロ埋め整数2桁
                    formatType = NumericTextBox.FormatTypes.IntegerZeroPlace2;
                    break;

                case "_GsfMaxNomTextBox":       //電極交換
                    titleString = _GsfMaxGroupBox.Text;
                    changeVal = _GsfMaxNomTextBox.Text;
                    lowerLimitDec = (decimal)0;
                    upperLimitDec = (decimal)GuideCountUpp;
                    strTenkeyMode = "esf_max";
                    //フォーマット：ゼロ埋め整数3桁
                    formatType = NumericTextBox.FormatTypes.IntegerZeroPlace3;
                    break;
            }

            //ポップアップTenKey：2017-1-12:柏原
            _popupTenkey = new TenKeyDialog(
                changeVal,
                formatType,
                lowerLimitDec,
                upperLimitDec,
                true,
                false,
                false,
                strTenkeyMode
                );
            _popupTenkey.NotifyReturnOk = popupTenkey_OnNotifyReturnOk; //イベント通知：OK
            _popupTenkey.Text = titleString;	                        //テンキータイトル表示：改行が有れば空白に変換　※ここは項目名が無い
            _popupTenkey.ShowDialog(this);                              //画面を開く
            return true;
        }
        /// <summary>
        /// ポップアップテンキーで「OK」ボタンを押した時、入力値をこのクラスに反映
        /// </summary>
        private void popupTenkey_OnNotifyReturnOk()
        {
            string retVal = _popupTenkey._tenkeyValReturn;//ポップアップテンキーで編集された値
            switch (((Control)_controlName).Name)
            {
                case "_GsfPositioningMoveTextBox":
                    _GsfPositioningMoveTextBox.Text = retVal;
                   
                    //CRS書き込み確認
                    UpdateSettingParameter();
                    _GsfPositioningMoveTextBox.SetLamp(false);
                    break;//ESF
                case "_GsfMaxNomTextBox":
                    _GsfMaxNomTextBox.Text = retVal;
                    //CRS書き込み
                    _paramChg(GsfSetting.GsfMaxNumSet);
                    //CRS書き込み確認
                    UpdateSettingParameter();
                    _GsfMaxNomTextBox.SetLamp(false);
                    break;//電極交換
            }
        }
        private void popupTenkey_Closing()
        {
            _GsfPositioningMoveTextBox.SetLamp(false);
            _GsfMaxNomTextBox.SetLamp(false);
        }
        #endregion
        #region 画像読み込みと表示
        /// <summary>
        /// 画像ファイル表示
        /// </summary>
        /// <param name="strName"></param>
        private void ImageDisplay(string strName)
        {
            string pathName =   FilePathInfo.ECNC3PATH + @"Picture\" + strName + ".png";                                        //相対パス"Picture\\"+ ModeName +".png"の絶対パスを取得する
            pictureBox1.LoadImage(pathName);
        }
        #endregion
        #region OtherMethods
        /// <summary>
        /// 桁数チェック関数
        /// </summary>
        /// <param name="_pos">入力値</param>
        /// <param name="_target">ﾊﾟﾙｽ変換値</param>
        /// <returns>true/false = 正常/異常</returns>
        private bool ChkPosText(string _pos, out int _target)
        {
            //符号有無
            bool unsigned = true;
            //小数点有無
            bool point = true;
            //整数の桁数
            int pointIndex = 0;
            //小数点以下の桁数
            int digitCt = 0;
            //少数⇒整数の変換係数
            int coef = 10;
            //符号チェック
            if (_pos.Contains("-"))
            {
                unsigned = false;
            }
            else
            {
                unsigned = true;
            }
            //小数点チェック
            if (_pos.Contains("."))
            {
                point = true;
            }
            else
            {
                point = false;
            }
            //パルスへ変換
            if (point == true)
            {
                //小数点がある場合は桁数確認後変換
                if (unsigned == false)
                {
                    pointIndex = _pos.Replace("-", "").IndexOf(".");
                }
                else
                {
                    pointIndex = _pos.IndexOf(".");
                }
                if (!_pos.EndsWith(".")
                    && _pos.EndsWith("0"))
                {
                    _pos = _pos.Remove(_pos.LastIndexOf("0"), 1);
                }
                digitCt = _pos.Count() - (pointIndex + 1);
                if (unsigned == false)
                {
                    digitCt = digitCt - 1;
                }

                //桁数エラーの場合は入力値不正で処理中断
                if (pointIndex > 3
                || pointIndex < 0
                || digitCt > 3)
                {
                    _target = 0;
                    return false;
                }
                //小数点以下の桁がある場合
                if (digitCt != 0)
                {
                    //ﾊﾟﾙｽに変換するための係数を演算
                    for (int iCt = 1; iCt < digitCt; iCt++)
                    {
                        coef *= 10;
                    }
                    //ﾊﾟﾙｽ（整数）に変換
                    _target = ((int)(double.Parse(_pos) * coef));
                }
                else
                {
                    _target = int.Parse(_pos.Replace(".", "")) * 1000;
                }
                return true;
            }
            //小数点がない場合はそのままﾊﾟﾙｽに変換
            else
            {
                _target = int.Parse(_pos) * 1000;
                return true;
            }
        }
        private void RetrySequence(int _retryCt)
        {
            if (_retryCt != 0 && _retryCt <= 10)
            {
                System.Threading.Thread.Sleep(500);
            }
            else if (_retryCt == 0) { }
            else
            {
                //テクノのセッティングPCを立ち上げるか、PCアプリを再起動する
            }
        }
        private void _paramChg(GsfSetting setting)
        {
            ECNC3.Enumeration.ResultCodes writeResult = Enumeration.ResultCodes.Success;
            //RTMC64ECの動作モードを「Setting」にする。
            using (McReqModeChange ReqModeChg = new McReqModeChange())
            {
                ReqModeChg.TaskMode = Enumeration.McTaskModes.Setting;
                if (ReqModeChg.Execute() != Enumeration.ResultCodes.Success)
                {
                    //動作モード変更に失敗したら有効軸切替を行わない。
                    return;
                }
            }
            using (Models.McIf.McDatStatus status = new McDatStatus())
            {
                status.Read();

                //settingモード待機
                while (status.Status.MotionMode != Enumeration.McTaskModes.Setting)
                {
                    status.Read();
                }
            }
            //実行
            writeResult = SetInitialParam(setting);
            UpdateAxisParameter();

            if (writeResult == Enumeration.ResultCodes.Success)
            {
                //RTMC64ECの動作モードを「Manual」にする。
                using (McReqModeChange ReqModeChg = new McReqModeChange())
                {
                    ReqModeChg.TaskMode = Enumeration.McTaskModes.Manual;
                    Enumeration.ResultCodes modeManualResult = Enumeration.ResultCodes.NotExecute;
                    int retryCt = 0;
                    //動作モード変更に失敗したらリトライする。
                    while (modeManualResult != Enumeration.ResultCodes.Success)
                    {
                        RetrySequence(retryCt);
                        modeManualResult = ReqModeChg.Execute();
                        retryCt++;
                    }
                }
                using (Models.McIf.McDatStatus status = new McDatStatus())
                {
                    status.Read();
                    int retryCt = 0;
                    //settingモード待機
                    while (status.Status.MotionMode != Enumeration.McTaskModes.Manual)
                    {
                        RetrySequence(retryCt);
                        status.Read();
                        retryCt++;
                    }
                }
            }
        }
        private void InitInput(NumericTextBox textBox, NumericTextBox.FormatTypes format)
        {
            textBox.FormatType = format;
            textBox.ReadOnly = true;
        }

        #endregion

        private void _AllBt_MouseUp(object sender, MouseEventArgs e)
        {
            if (((ButtonEx)sender).GetBack())
            {
                ((ButtonEx)sender).SetBack(false);
            }
            else
            {
                _AllControlsBackColorChg(false);
                ((ButtonEx)sender).SetBack(true);
            }
        }

        private void _GsfMaxNomTextBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (_GsfMaxNomTextBox.GetLamp() == false)
            {
                _GsfMaxNomTextBox.SetLamp(true);
                popupTenkeyOn(_GsfMaxNomTextBox);
            }           
        }

        private void _GsfPositioningMoveTextBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (_GsfPositioningMoveTextBox.GetLamp() == false)
            {
                _GsfPositioningMoveTextBox.SetLamp(true);
                popupTenkeyOn(_GsfPositioningMoveTextBox);
            }
        }

        private void _GuideClampBtn_Click(object sender, EventArgs e)
        {
            SettingSeqLampChg(AECSequences.GuideClamp);
        }

        private void _GuideUnClampBtn_Click(object sender, EventArgs e)
        {
            SettingSeqLampChg(AECSequences.GuideUnClamp);
        }

        private void _GsfArmBackBtn_Click(object sender, EventArgs e)
        {
            SettingSeqLampChg(AECSequences.GsfArmBack);
        }

        private void _GsfArmFrontBtn_Click(object sender, EventArgs e)
        {
            SettingSeqLampChg(AECSequences.GsfArmFront);
        }

        private void button_XSt_WRITE_Click(object sender, EventArgs e)
        {
            _paramChg(GsfSetting.GuideExchStartPosX);
        }

        private void button_YSt_WRITE_Click(object sender, EventArgs e)
        {
            _paramChg(GsfSetting.GuideExchStartPosY);
        }

        private void button_XChk_WRITE_Click(object sender, EventArgs e)
        {
            _paramChg(GsfSetting.GuideChkPosX);
        }

        private void button_YChk_WRITE_Click(object sender, EventArgs e)
        {
            _paramChg(GsfSetting.GuideChkPosY);

        }

        private void button_XEnd_WRITE_Click(object sender, EventArgs e)
        {
            _paramChg(GsfSetting.GuideExchEndPosX);
        }

        private void button_YEnd_WRITE_Click(object sender, EventArgs e)
        {
            _paramChg(GsfSetting.GuideExchEndPosY);
        }

        private void _GsfPositioningMoveBtn_Click(object sender, EventArgs e)
        {
            
        }

        private void button_W_WRITE_Click(object sender, EventArgs e)
        {
            _paramChg(GsfSetting.GuideExchStartPosW);
        }

        private void button_WOfs1_WRITE_Click(object sender, EventArgs e)
        {
            _paramChg(GsfSetting.GuideExchOfsW1);
        }

        private void button_WOfs2_WRITE_Click(object sender, EventArgs e)
        {
            _paramChg(GsfSetting.GuideExchOfsW2);
        }

        private void button_ABC_WRITE_Click(object sender, EventArgs e)
        {
            _paramChg(GsfSetting.GuideExchStartPosABC);
        }

        private void button_W_MOVE_Click(object sender, EventArgs e)
        {
            SettingSeqLampChg(AECSequences.GuideExchStartPosW);
        }

        private void button_WOfs1_MOVE_Click(object sender, EventArgs e)
        {
            SettingSeqLampChg(AECSequences.GuideExchOfsW1);
        }

        private void button_WOfs2_MOVE_Click(object sender, EventArgs e)
        {
            SettingSeqLampChg(AECSequences.GuideExchOfsW2);
        }
    }
}
