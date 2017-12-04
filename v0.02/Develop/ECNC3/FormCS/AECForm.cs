///////////////////////////////////////////////////////////////////////////////////////////
//
// (1) システム名称 : ELENIX ECNCⅢ
// (2) ファイル名   : AECForm.cs
// (3) 概要         : AEC画面（電極交換）
// (4) 作成日       : 2015.06.01
// (5) 作成者       : 八野
// (6) 特記事項     : VisualStudio2015 C#
// (7) 更新履歴     :
///////////
///////////////////////////////////////////////////////////////////////////////////////////


using ECNC3.Enumeration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ECNC3.Models.McIf;//McReqGsfMoveArm用

namespace ECNC3.Views
{
    public partial class AECForm : ECNC3Form
    {
        /// <summary>ポップアップテンキー</summary>
        private TenKeyDialog _popupTenkey = new TenKeyDialog("", 0, 0, 0);//初回インスタンスを作っておく
		PartitionForm Partition;
		private int ElctCountUpp = 0;
        private bool _EditingElctNum = false;
        private bool _EditingGuideNum = false;
        private bool _SelectElctNumberResetFlag = false;
		/// <summary>
		/// スタートボタンのオフエッジ検出イベント
		/// </summary>
		internal event StatusMonitoringEventHandler StaMonitorEvent;
		private AECSequences AecSeq = AECSequences.Null;

		//コンストラクタ
		public AECForm()
        {
            InitializeComponent();
            AecSeq = AECSequences.Null;
        }
		/// <summary>
		/// フォーム：ロード
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AECForm_Load_1( object sender, EventArgs e )
		{
			UnitOpeForm( false );
		}
		private bool _boolUnitOpe = false;
		/// <summary>
		/// フォーム表示：通常=false、単体操作=true：
		/// </summary>
		/// <param name="boolUnitOpe"></param>
		void UnitOpeForm( bool boolUnitOpe )
		{
			_boolUnitOpe = boolUnitOpe;
			int intAddWidth = 430;
			int intAddPanelMove = 203;
            //このフォームのサイズ
            this.Size = new System.Drawing.Size( 1024 - intAddWidth, 531 );
			//このフォームの表示位置
			this.Location = new Point( intAddWidth, 90 );//表示位置設定
			//ラベル：タイトル
			label_AEC.Size = new Size( 1002 - intAddWidth, 21 );
            //表示/非表示
            //単体操作パネル
            //ESF:フィンガーアーム
            PanelVisibleChange(PanelEx_FingerArm, boolUnitOpe);
            //ESF：シリンダー
            PanelVisibleChange(PanelEx_ESFCylinder, boolUnitOpe);
            //GSF：シリンダー
            PanelVisibleChange(panelEx_GSFCylinder, boolUnitOpe);
            //GSF：貫通動作
            PanelVisibleChange(PanelEx_OptionCommands, boolUnitOpe);

            //交換動作パネル
            //GSF：ガイド交換
            PanelVisibleChange(PanelEx_GSFGuidChange, !boolUnitOpe);
            //GSF：ガイドホルダー
            PanelVisibleChange(PanelEx_GSFGuideHolder, !boolUnitOpe);
            //ESF：電極交換
            PanelVisibleChange(PanelEx_ESFChanger, !boolUnitOpe);
            //ESF：コレットホルダー
            PanelVisibleChange(PanelEX_ColletHolder, !boolUnitOpe);
            //移動
            //GSF
            PanelEx_GSFGuideHolder.Location = new Point( 509 - intAddPanelMove, PanelEx_GSFGuideHolder.Location.Y );//表示位置設定
			PanelEx_GSFGuidChange.Location = new Point( 509 - intAddPanelMove, PanelEx_GSFGuidChange.Location.Y );//表示位置設定
            panel_AECButton.Location = new Point(573 - intAddWidth, panel_AECButton.Location.Y);
            _ClosePanel.Location = new Point(868 - intAddWidth, _ClosePanel.Location.Y);
            PanelEx_ESFCylinder.Location = PanelEx_ESFChanger.Location;
            panelEx_GSFCylinder.Location = PanelEx_GSFGuideHolder.Location;
            PanelEx_FingerArm.Location = PanelEX_ColletHolder.Location;
            PanelEx_OptionCommands.Location = new Point(panelEx_GSFCylinder.Location.X, PanelEx_ESFCylinder.Location.Y);

            //ボタン
            if ( boolUnitOpe ) {
                if (AECFuncBt.GetBack() == false) AECFuncBt.SetBack(true);
			} else {
                if (AECFuncBt.GetBack() == true) AECFuncBt.SetBack(false);
            }
            //電極数/ガイド数表示
            using (MonitoringFunction MonitorFunc = new MonitoringFunction())
            {
                ElctCountUpp = MonitorFunc.RetIntegerMonitoringResult(MonitorTargets.ElectrodeCount);
                ElectrodeCountSub.Text = ElectrodeCount.Text = "ESF" + ElctCountUpp.ToString("000");
                GuideCountSub.Text = GuideCount.Text = "GSF" + MonitorFunc.RetIntegerMonitoringResult(MonitorTargets.GuideCount).ToString();
            }

        }
        /// <summary>
        /// パネルコントロール内の全コントロールの表示を切り替えます。
        /// </summary>
        /// <param name="target">
        /// 対象のパネルコントロール
        /// </param>
        /// <param name="visible">
        /// true：表示
        /// false：非表示
        /// </param>
        private void PanelVisibleChange(PanelEx target, bool visible)
        {
            //インスタンスが無ければ処理を抜ける。
            if (target == null) return;
            if(target.Controls.Count > 0)
            {
                //パネルに子コントロールがあれば子コントロールも切り替える。
                //無ければパネルのみ切り替える。
                foreach (Control ctrl in target.Controls)
                {
                    ctrl.Visible = visible;
                }
            }            
            target.Visible = visible;
        }
        /// <summary>
        /// フォーム：閉じた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ChildFormClosed(object sender, EventArgs e)
        {
            StaMonitorEvent = null;
        }
        bool _threadStop = false;
		/// <summary>
		/// ステータスモニター
		/// </summary>
		/// <param name="e"></param>
        internal void StatusMonitoring(StatusMonitorEventArgs e)
        {
            if (_threadStop == true)
            {
                if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                {
                    this.Close();
                }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                return;
            }
            if (StaMonitorEvent!= null)
            {
                StaMonitorEvent(e);
            }
            //if (true == InvokeRequired)
            //{
                if ((this.IsDisposed == true || this.Disposing == true)) return;
                retInvoke = BeginInvoke((MethodInvoker)delegate
                {
                    if (_EditingElctNum == false)ElectrodeNumberSetTextBox.Text = e.Items.ElectrodeNumber.ToString("000");
                    if (_EditingGuideNum == false) GuideNumberSetTextBox.Text = e.Items.GuideNumber.ToString();
                });
            if ((this.IsDisposed == true || this.Disposing == true))
            {
                return;
            }
            else
            {
                EndInvoke(retInvoke);
            }
             
            //}
            if (e.Items.FGRunning == true)
            {
                //if (true == InvokeRequired)
                //{
                //    if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                //    {
                        if(_SelectElctNumberResetFlag == false)_SelectElctNumberResetFlag = true;
                    //}); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                //}
            }
            else
            {
                if(_SelectElctNumberResetFlag == true)
                {
                    //if (true == InvokeRequired)
                    //{
                        if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                        {
                            //完了時、選択された電極番号をリセットする。
                            _EsfCylinderNumberText.Text = "000";
                        }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                    //}
                    _SelectElctNumberResetFlag = false;
                }
            }
            //if (true == InvokeRequired)
            //{
                if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                {
                    AecLampChg(e.Items);
                    //番号設定が未設定の場合、設定ボタンの枠線色を変更する
                    if (e.Items.ElectrodeNumber != 0)
                    {
                        //電極番号設定ボタンの背景色を白くする。
                        ElectrodeNumberSetBt.SetLed(true);
                    }
                    else
                    {
                        ElectrodeNumberSetBt.SetLed(false);
                    }

                    if (e.Items.GuideNumber != 0)
                    {
                        //ガイド番号設定ボタンの背景色を白くする。
                        GuideNumberSetBt.SetLed(true);
                    }
                    else
                    {
                        GuideNumberSetBt.SetLed(true);
                    }
                }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
            //}
            if (_popupTenkey.Visible == false)
            {
                if (e.Items.StartSwBtnOffEdge == true)
                {
                    using (SettingFunction SetFunc = new SettingFunction())
                    using (SequenceFunction SeqFunc = new SequenceFunction())
                    using (MonitoringFunction MonitorFunc = new MonitoringFunction())
                    {
                        switch (AecSeq)
                        {
                            case AECSequences.Null:
                                StaMonitorEvent?.Invoke(new StatusMonitorEventArgs(e.Items));                                                     //"StartSwOffEdgeEvent"イベントの発生
                                break;

                            case AECSequences.GuideClamp:
                                if (MonitorFunc.SequenceConditionMonitoring(MonitorTargets.CompletedOrigin) == true)
                                {
                                    SeqFunc.SequenceMonitoring(Sequences.GuideClamp);
                                }

                                break;

                            case AECSequences.GuideUnClamp:
                                if (MonitorFunc.SequenceConditionMonitoring(MonitorTargets.CompletedOrigin) == true)
                                {
                                    SeqFunc.SequenceMonitoring(Sequences.GuideUnClamp);
                                }

                                break;

                            case AECSequences.SpindleClamp:
                                if (MonitorFunc.SequenceConditionMonitoring(MonitorTargets.CompletedOrigin) == true)
                                {
                                    SeqFunc.SequenceMonitoring(Sequences.SpindleClamp);
                                }

                                break;

                            case AECSequences.SpindleUnClamp:
                                if (MonitorFunc.SequenceConditionMonitoring(MonitorTargets.CompletedOrigin) == true)
                                {
                                    SeqFunc.SequenceMonitoring(Sequences.SpindleUnClamp);
                                }

                                break;

                            case AECSequences.EsfInstall:
                                using (Models.McIf.McReqElectrodeInsall ReqElectInst = new Models.McIf.McReqElectrodeInsall())
                                {
                                    ReqElectInst.ElectrodeNumber = (short)int.Parse(_EsfCylinderNumberText.Text);
                                    ReqElectInst.Install = true;
                                    ResultCodes _retElectInst = ReqElectInst.Execute();
                                    //ログ処理
                                    UILog logs = new UILog("AECForm.SeqFunc.SequenceMonitoring");
                                    if (_retElectInst == ResultCodes.Success)
                                    {
                                        logs.Sure("ESF-ON Result = " + _retElectInst);
                                    }
                                    else
                                    {
                                        logs.Error("ESF-ON Result = " + _retElectInst);
                                    }
                                }
                                break;

                            case AECSequences.EsfUnInstall:
                                using (Models.McIf.McReqElectrodeInsall ReqElectInst = new Models.McIf.McReqElectrodeInsall())
                                {
                                    ReqElectInst.ElectrodeNumber = (short)int.Parse(_EsfCylinderNumberText.Text);
                                    ReqElectInst.Install = false;
                                    ResultCodes _retElectInst = ReqElectInst.Execute();
                                    //ログ処理
                                    UILog logs = new UILog("AECForm.SeqFunc.SequenceMonitoring");
                                    if (_retElectInst == ResultCodes.Success)
                                    {
                                        logs.Sure("ESF-OFF Result = " + _retElectInst);
                                    }
                                    else
                                    {
                                        logs.Error("ESF-OFF Result = " + _retElectInst);
                                    }
                                }
                                break;

                            case AECSequences.GsfInstall:
                                using (Models.McIf.McReqGuideInsall ReqGuideInst = new Models.McIf.McReqGuideInsall())
                                {
                                    ReqGuideInst.GuideNumber = (short)int.Parse(GuideHolderNumber.Text);
                                    ReqGuideInst.Install = true;
                                    ResultCodes _retElectInst = ReqGuideInst.Execute();
                                    //ログ処理
                                    UILog logs = new UILog("AECForm.SeqFunc.SequenceMonitoring");
                                    if (_retElectInst == ResultCodes.Success)
                                    {
                                        logs.Sure("GSF-ON Result = " + _retElectInst);
                                    }
                                    else
                                    {
                                        logs.Error("GSF-ON Result = " + _retElectInst);
                                    }
                                }
                                break;

                            case AECSequences.GsfUnInstall:
                                using (Models.McIf.McReqGuideInsall ReqGuideInst = new Models.McIf.McReqGuideInsall())
                                {
                                    ReqGuideInst.GuideNumber = (short)int.Parse(GuideHolderNumber.Text);
                                    ReqGuideInst.Install = false;
                                    ResultCodes _retElectInst = ReqGuideInst.Execute();
                                    //ログ処理
                                    UILog logs = new UILog("AECForm.SeqFunc.SequenceMonitoring");
                                    if (_retElectInst == ResultCodes.Success)
                                    {
                                        logs.Sure("GSF-OFF Result = " + _retElectInst);
                                    }
                                    else
                                    {
                                        logs.Error("GSF-OFF Result = " + _retElectInst);
                                    }
                                }
                                break;

                            case AECSequences.MagazineIncFront:
                                using (Models.McIf.McReqEsfMoveMagazine ReqEsfMagzInc = new Models.McIf.McReqEsfMoveMagazine())
                                {
                                    int targetNumber = 0;
                                    int.TryParse(_EsfCylinderNumberText.Text, out targetNumber);
                                    ReqEsfMagzInc.MagazineNumber = targetNumber;
                                    ResultCodes _retElectInst = ReqEsfMagzInc.Execute();
                                    //ログ処理
                                    UILog logs = new UILog("AECForm.SeqFunc.SequenceMonitoring");
                                    if (_retElectInst == ResultCodes.Success)
                                    {
                                        logs.Sure("MAGAZINE_INC-No" + targetNumber.ToString() + " Result = " + _retElectInst);
                                    }
                                    else
                                    {
                                        logs.Error("MAGAZINE_INC-No" + targetNumber.ToString() + " Result = " + _retElectInst);
                                    }
                                }
                                break;

                            //ESF：フィンガーアーム
                            case AECSequences.FingerArmUnClamp://開く
                                using (Models.McIf.McReqEsfMoveFinger ReqEsfFingerArm = new Models.McIf.McReqEsfMoveFinger())
                                {
                                    ReqEsfFingerArm.FingerOpen = true;//開く
                                    ResultCodes _retResultCodes = ReqEsfFingerArm.Execute();
                                    //ログ処理
                                    UILog logs = new UILog("AECForm.SeqFunc.SequenceMonitoring");
                                    if (_retResultCodes == ResultCodes.Success)
                                    {
                                        logs.Sure("ESF_FingerArm_Open Result = " + _retResultCodes);
                                    }
                                    else
                                    {
                                        logs.Error("ESF_FingerArm_Open Result = " + _retResultCodes);
                                    }
                                }
                                break;

                            case AECSequences.FingerArmClamp://閉じる
                                using (Models.McIf.McReqEsfMoveFinger ReqEsfFingerArm = new Models.McIf.McReqEsfMoveFinger())
                                {
                                    ReqEsfFingerArm.FingerOpen = false;//閉じる
                                    ResultCodes _retResultCodes = ReqEsfFingerArm.Execute();
                                    //ログ処理
                                    UILog logs = new UILog("AECForm.SeqFunc.SequenceMonitoring");
                                    if (_retResultCodes == ResultCodes.Success)
                                    {
                                        logs.Sure("ESF_FingerArm_Close Result = " + _retResultCodes);
                                    }
                                    else
                                    {
                                        logs.Error("ESF_FingerArm_Close Result = " + _retResultCodes);
                                    }
                                }
                                break;

                            //ESF：シリンダ
                            case AECSequences.FingerArmBack://後退：収納位置 
                                using (Models.McIf.McReqEsfMoveArm ReqEsfMoveArm = new Models.McIf.McReqEsfMoveArm())
                                {
                                    ReqEsfMoveArm.Position = EsfArmPositions.Back;//後退
                                    ResultCodes _retResultCodes = ReqEsfMoveArm.Execute();
                                    //ログ処理
                                    UILog logs = new UILog("AECForm.SeqFunc.SequenceMonitoring");
                                    if (_retResultCodes == ResultCodes.Success)
                                    {
                                        logs.Sure("ESF_FingerArm_Back Result = " + _retResultCodes);
                                    }
                                    else
                                    {
                                        logs.Error("ESF_FingerArm_Back Result = " + _retResultCodes);
                                    }
                                }
                                break;

                            case AECSequences.FingerArmFront1://前進1：真ん中
                                using (Models.McIf.McReqEsfMoveArm ReqEsfMoveArm = new Models.McIf.McReqEsfMoveArm())
                                {
                                    ReqEsfMoveArm.Position = EsfArmPositions.Middle;//ミドル(前進1)
                                    ResultCodes _retResultCodes = ReqEsfMoveArm.Execute();
                                    //ログ処理
                                    UILog logs = new UILog("AECForm.SeqFunc.SequenceMonitoring");
                                    if (_retResultCodes == ResultCodes.Success)
                                    {
                                        logs.Sure("ESF_FingerArm_Front1 Result = " + _retResultCodes);
                                    }
                                    else
                                    {
                                        logs.Error("ESF_FingerArm_Front1 Result = " + _retResultCodes);
                                    }
                                }
                                break;

                            case AECSequences.FingerArmFront2://前進2：手前
                                using (Models.McIf.McReqEsfMoveArm ReqEsfMoveArm = new Models.McIf.McReqEsfMoveArm())
                                {
                                    ReqEsfMoveArm.Position = EsfArmPositions.Foward;//前進2
                                    ResultCodes _retResultCodes = ReqEsfMoveArm.Execute();
                                    //ログ処理
                                    UILog logs = new UILog("AECForm.SeqFunc.SequenceMonitoring");
                                    if (_retResultCodes == ResultCodes.Success)
                                    {
                                        logs.Sure("ESF_FingerArm_Front2 Result = " + _retResultCodes);
                                    }
                                    else
                                    {
                                        logs.Error("ESF_FingerArm_Front2 Result = " + _retResultCodes);
                                    }
                                }
                                break;

                            //GSF：シリンダー
                            case AECSequences.GsfArmBack://後退
                                using (Models.McIf.McReqGsfMoveArm ReqGsfMoveArm = new Models.McIf.McReqGsfMoveArm())
                                {
                                    ReqGsfMoveArm.Position = GsfArmPositions.Back;//後退
                                    ResultCodes _retResultCodes = ReqGsfMoveArm.Execute();
                                    //ログ処理
                                    UILog logs = new UILog("AECForm.SeqFunc.SequenceMonitoring");
                                    if (_retResultCodes == ResultCodes.Success)
                                    {
                                        logs.Sure("GSF_Arm_Back Result = " + _retResultCodes);
                                    }
                                    else
                                    {
                                        logs.Error("GSF_Arm_Back Result = " + _retResultCodes);
                                    }
                                }
                                break;

                            case AECSequences.GsfArmFront://前進
                                using (Models.McIf.McReqGsfMoveArm ReqGsfMoveArm = new Models.McIf.McReqGsfMoveArm())
                                {
                                    ReqGsfMoveArm.Position = GsfArmPositions.Foward;//前進
                                    ResultCodes _retResultCodes = ReqGsfMoveArm.Execute();
                                    //ログ処理
                                    UILog logs = new UILog("AECForm.SeqFunc.SequenceMonitoring");
                                    if (_retResultCodes == ResultCodes.Success)
                                    {
                                        logs.Sure("GSF_Arm_Front Result = " + _retResultCodes);
                                    }
                                    else
                                    {
                                        logs.Error("GSF_Arm_Front Result = " + _retResultCodes);
                                    }
                                }
                                break;

                            case AECSequences.GuideThrough://ガイド貫通動作
                                using (Models.McIf.McReqGuideThroughStart ReqGuideThrough = new Models.McIf.McReqGuideThroughStart())
                                {
                                    ResultCodes _retResultCodes = ReqGuideThrough.Execute();
                                    //ログ処理
                                    UILog logs = new UILog("AECForm.SeqFunc.SequenceMonitoring");
                                    if (_retResultCodes == ResultCodes.Success)
                                    {
                                        logs.Sure("GUIDE_THROUGH Result = " + _retResultCodes);
                                    }
                                    else
                                    {
                                        logs.Error("GUIDE_THROUGH Result = " + _retResultCodes);
                                    }
                                }
                                break;

                        }
                    }
                    //全ボタン背景色を解除する。
                    foreach (Control ctrls in this.Controls)
                    {
                        if (ctrls.GetType() != typeof(PanelEx)) continue;
                        foreach (Control subCtrls in ctrls.Controls)
                        {
                            if (subCtrls.GetType() != typeof(ButtonEx)) continue;
                            (subCtrls as ButtonEx).SetBack(false);
                        }
                    }
                }
            }
            if (e.Items.SequenceEnd == true)
            {
                if ((this.IsDisposed == true || this.Disposing == true)) return;
                retInvoke = BeginInvoke((MethodInvoker)delegate
                {
                    _AllButtonsActivate(this.Controls);
                }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                if ((this.IsDisposed == true || this.Disposing == true)) return;
                EndInvoke(retInvoke);
            }
            else
            {
                if ((this.IsDisposed == true || this.Disposing == true)) return;
                retInvoke = BeginInvoke((MethodInvoker)delegate
                {
                    _AllButtonsDeActivate(this.Controls);
                }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                if ((this.IsDisposed == true || this.Disposing == true)) return;
                EndInvoke(retInvoke);
            }
        }
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
        /// Aec操作用ボタンランプ変更
        /// </summary>
        /// <param name="items"></param>
        private void AecLampChg(MonitoringItems items)
        {
            //コレットクランプ
            if (items.ColletClampOn != ColetClampCloseBt.GetLed())
            {
                ColetClampCloseBt.SetLed(items.ColletClampOn);
                ColetClampCloseBtSub.SetLed(items.ColletClampOn);
            }

            if (!items.ColletClampOn != ColetClampOpenBt.GetLed())
            {
                ColetClampOpenBt.SetLed(!items.ColletClampOn);
                ColetClampOpenBtSub.SetLed(!items.ColletClampOn);
            }

            //ガイドホルダークランプ
            if (items.GuideHolderClampOn != GuideHolderClampCloseBt.GetLed())
            {
                GuideHolderClampCloseBt.SetLed(items.GuideHolderClampOn);
                GuideHolderClampCloseBtSub.SetLed(items.GuideHolderClampOn);
            }

            if (!items.GuideHolderClampOn != GuideHolderClampOpenBt.GetLed())
            {
                GuideHolderClampOpenBt.SetLed(!items.GuideHolderClampOn);
                GuideHolderClampOpenBtSub.SetLed(!items.GuideHolderClampOn);
            }

            //ガイドホルダーアーム
            if (items.GsfArmBack != buttonEx_GSFCylinderRetract.GetLed())
            {
                buttonEx_GSFCylinderRetract.SetLed(items.GsfArmBack);
            }

            if (items.GsfArmFoward != buttonEx_GSFCylinderExtend.GetLed())
            {
                buttonEx_GSFCylinderExtend.SetLed(items.GsfArmFoward);
            }

            //ESFフィンガーアームクランプ
            if (!items.FingerArmClampOn != buttonEx_ESFFingerArmOpen.GetLed())
            {
                buttonEx_ESFFingerArmOpen.SetLed(!items.FingerArmClampOn);
            }

            if (items.FingerArmClampOn != buttonEx_ESFFingerArmClose.GetLed())
            {
                buttonEx_ESFFingerArmClose.SetLed(items.FingerArmClampOn);
            }

            //ESFフィンガーアーム位置
            switch(items.FingerArmPos)
            {
                case EsfArmPositions.Unknown:
                    if(true == buttonEx_ESFCylinderRetract.GetLed())
                    {
                        buttonEx_ESFCylinderRetract.SetLed(false);
                    }
                    if (true == buttonEx_ESFCylinderExtend1.GetLed())
                    {
                        buttonEx_ESFCylinderExtend1.SetLed(false);
                    }
                    if (true == buttonEx_ESFCylinderExtend2.GetLed())
                    {
                        buttonEx_ESFCylinderExtend2.SetLed(false);
                    }
                    break;

                case EsfArmPositions.Back:
                    if (false == buttonEx_ESFCylinderRetract.GetLed())
                    {
                        buttonEx_ESFCylinderRetract.SetLed(true);
                    }
                    if (true == buttonEx_ESFCylinderExtend1.GetLed())
                    {
                        buttonEx_ESFCylinderExtend1.SetLed(false);
                    }
                    if (true == buttonEx_ESFCylinderExtend2.GetLed())
                    {
                        buttonEx_ESFCylinderExtend2.SetLed(false);
                    }
                    break;

                case EsfArmPositions.Middle:
                    if (true == buttonEx_ESFCylinderRetract.GetLed())
                    {
                        buttonEx_ESFCylinderRetract.SetLed(false);
                    }
                    if (false == buttonEx_ESFCylinderExtend1.GetLed())
                    {
                        buttonEx_ESFCylinderExtend1.SetLed(true);
                    }
                    if (true == buttonEx_ESFCylinderExtend2.GetLed())
                    {
                        buttonEx_ESFCylinderExtend2.SetLed(false);
                    }
                    break;

                case EsfArmPositions.Foward:
                    if (true == buttonEx_ESFCylinderRetract.GetLed())
                    {
                        buttonEx_ESFCylinderRetract.SetLed(false);
                    }
                    if (true == buttonEx_ESFCylinderExtend1.GetLed())
                    {
                        buttonEx_ESFCylinderExtend1.SetLed(false);
                    }
                    if (false == buttonEx_ESFCylinderExtend2.GetLed())
                    {
                        buttonEx_ESFCylinderExtend2.SetLed(true);
                    }
                    break;

            }
        }
        /// <summary>
        /// 動作名表示とスタートボタンのトリガーに設定
        /// </summary>
        /// <param name="_seq">動作名</param>
        private void SettingSeqNameView(AECSequences _seq, ButtonEx button)
        {
            //ボタンが押されていれば解除する。
            if (button.GetBack() == true)
            {
                button.SetBack(false);
                AecSeq = _seq = AECSequences.Null;
                return;
            }
            //押されたボタン以外のボタン背景色を解除する。
            foreach(Control ctrls in this.Controls)
            {
                if (ctrls.GetType() != typeof(PanelEx)) continue;
                foreach(Control subCtrls in ctrls.Controls)
                {
                    if (subCtrls.Name == button.Name) continue;
                    if (subCtrls.GetType() != typeof(ButtonEx)) continue;
                    (subCtrls as ButtonEx).SetBack(false);
                }
            }
            //押されたボタンの背景色を変更する。
            button.SetBack(true);
            //シーケンス設定
            AecSeq = _seq;
		}

        private void GuideHolderClampOpen_Click(object sender, EventArgs e)
        {
            SettingSeqNameView(AECSequences.GuideUnClamp, sender as ButtonEx);
        }

        private void GuideHolderClampClose_Click(object sender, EventArgs e)
        {
            SettingSeqNameView(AECSequences.GuideClamp, sender as ButtonEx);
        }

        private void ColetClampOpenBt_Click(object sender, EventArgs e)
        {
            SettingSeqNameView(AECSequences.SpindleUnClamp, sender as ButtonEx);
        }

        private void ColetClampCloseBt_Click(object sender, EventArgs e)
        {
            SettingSeqNameView(AECSequences.SpindleClamp, sender as ButtonEx);
        }

        private void ElectroadNumberSetBt_Click(object sender, EventArgs e)
        {
            using (MonitoringFunction MonitorFunc = new MonitoringFunction())
            using (SettingFunction SetFunc = new SettingFunction())
            {
                SetFunc.SettingParameter = int.Parse(ElectrodeNumberSetTextBox.Text);
                SetFunc.SettingMonitoring(Settings.ElectroadNumber);
                _EditingElctNum = false;
            }
        }

        private void GuideNumberSetBt_Click(object sender, EventArgs e)
        {
            using (MonitoringFunction MonitorFunc = new MonitoringFunction())
            using (SettingFunction SetFunc = new SettingFunction())
            {
                SetFunc.SettingParameter = int.Parse(GuideNumberSetTextBox.Text);
                SetFunc.SettingMonitoring(Settings.GuideNumber);
                _EditingGuideNum = false;
            }
        }

        private void ElectrodeNumberUpBt_Click(object sender, EventArgs e)
        {
            if (int.Parse(ElectrodeNumberSetTextBox.Text) < ElctCountUpp)
            {
                _EditingElctNum = true;
                ElectrodeNumberSetTextBox.Text = (int.Parse(ElectrodeNumberSetTextBox.Text) + 1).ToString("000");
            }
            else
            {
                ElectrodeNumberSetTextBox.Text = "001";
            }
        }

        private void ElectrodeNumberDownBt_Click(object sender, EventArgs e)
        {
            if (int.Parse(ElectrodeNumberSetTextBox.Text) > 1)
            {
                _EditingElctNum = true;
                ElectrodeNumberSetTextBox.Text = (int.Parse(ElectrodeNumberSetTextBox.Text) - 1).ToString("000");
            }
            else
            {
                ElectrodeNumberSetTextBox.Text = ElctCountUpp.ToString("000");
            }
        }


        private void GuideNumberUpBt_Click(object sender, EventArgs e)
        {
            if (int.Parse(GuideNumberSetTextBox.Text) < 4)
            {
                _EditingGuideNum = true;
                GuideNumberSetTextBox.Text = (int.Parse(GuideNumberSetTextBox.Text) + 1).ToString();
            }
            else
            {
                GuideNumberSetTextBox.Text = "1";
            }
        }

        private void GuideNumberDownBt_Click(object sender, EventArgs e)
        {
            if (int.Parse(GuideNumberSetTextBox.Text) > 1)
            {
                _EditingGuideNum = true;
                GuideNumberSetTextBox.Text = (int.Parse(GuideNumberSetTextBox.Text) - 1).ToString();
            }
            else
            {
                GuideNumberSetTextBox.Text = "4";
            }
        }

        private void EsfCylinderNumberUpBt_Click(object sender, EventArgs e)
        {
            if (int.Parse(_EsfCylinderNumberText.Text) < 24)
            {
                _EsfCylinderNumberText.Text = (int.Parse(_EsfCylinderNumberText.Text) + 1).ToString("000");
            }
            else
            {
                _EsfCylinderNumberText.Text = "000";
            }
        }

        private void EsfCylinderNumberDownBt_Click(object sender, EventArgs e)
        {
            if (int.Parse(_EsfCylinderNumberText.Text) > 0)
            {
                _EsfCylinderNumberText.Text = (int.Parse(_EsfCylinderNumberText.Text) - 1).ToString("000");
            }
            else
            {
                _EsfCylinderNumberText.Text = "024";
            }
        }


        private void GuideHolderUpBt_Click(object sender, EventArgs e)
        {
            if (int.Parse(GuideHolderNumber.Text) < 4)
            {
                GuideHolderNumber.Text = (int.Parse(GuideHolderNumber.Text) + 1).ToString();
            }
            else
            {
                GuideHolderNumber.Text = "1";
            }
        }

        private void GuideHolderDownBt_Click(object sender, EventArgs e)
        {
            if (int.Parse(GuideHolderNumber.Text) > 1)
            {
                GuideHolderNumber.Text = (int.Parse(GuideHolderNumber.Text) - 1).ToString();
            }
            else
            {
                GuideHolderNumber.Text = "4";
            }
        }

        public void BackMANUALForm_Click()
        {
            if (ElectrodeNumberSetBt.FlatAppearance.BorderColor == Color.Red
                || GuideNumberSetBt.FlatAppearance.BorderColor == Color.Red)
            {
                using (MessageDialog msg = new MessageDialog())
                {
                    //番号設定が未設定の場合、確認メッセージを表示する。
                    if (msg.Question(5025, this) == true)
                    {
                        this._threadStop = true;
                    }
                    else
                    {
                        return;
                    }
                }
            }
            else
            {
                this._threadStop = true;
            }

        }

		/// <summary>
		/// パーティーション：ボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void PartitionBt_Click(object sender, EventArgs e)
        {
            Partition = new PartitionForm();
            StaMonitorEvent += Partition.StatusMonitoring;
            Partition.Show(this);
        }
		/// <summary>
		/// ESF：シリンダー：装着(ESF:Install)ボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EsfCylinderSetBt_Click(object sender, EventArgs e)
        {
            SettingSeqNameView(AECSequences.EsfInstall, sender as ButtonEx);
        }
		/// <summary>
		/// ESF：シリンダー：脱着(ESF:UnInstall)ボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EsfCylinderDesorptionBt_Click(object sender, EventArgs e)
        {
            SettingSeqNameView(AECSequences.EsfUnInstall, sender as ButtonEx);
        }
		/// <summary>
		/// GSF：ガイド交換：装着(GSF:Install)ボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonEx1_Click(object sender, EventArgs e)
        {
            SettingSeqNameView(AECSequences.GsfInstall, sender as ButtonEx);
        }
		/// <summary>
		/// GSF：ガイド交換：脱着(GSF:UnInstall)ボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonEx3_Click(object sender, EventArgs e)
        {
            SettingSeqNameView(AECSequences.GsfUnInstall, sender as ButtonEx);
        }
		/// <summary>
		/// ESF：INDEX(マガジン送り)：ボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void IndexMoveBt_Click(object sender, EventArgs e)
        {
            SettingSeqNameView(AECSequences.MagazineIncFront, sender as ButtonEx);
        }

        /// <summary>
        /// ESF：ElectrodeNumberSetTextBox：クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ElectrodeNumberSetTextBox_Click(object sender, EventArgs e)
        {
            popupTenkeyOn( "ElectrodeNumberSetTextBox" );
        }
        /// <summary>
        /// 電極交換：EsfCylinderNumberText：クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EsfCylinderNumberText_Click(object sender, EventArgs e)
        {
            popupTenkeyOn( "EsfCylinderNumberText" );
        }
		/// <summary>
		/// 単体操作/閉じる：ボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AECFuncBt_Click( object sender, EventArgs e )
		{
			if( _boolUnitOpe ) {
				_boolUnitOpe = false;
			} else {
				_boolUnitOpe = true;
			}
			//フォーム表示：通常=false、単体操作=true：
			UnitOpeForm( _boolUnitOpe );
			//線が残っているので再描画
			this.Refresh();
		}
		#region<ポップアップテンキー>
		private string _controlName;
		/// <summary>
		/// ポップアップテンキー：表示/非表示
		/// </summary>
		/// <param name="val"></param>
		/// <returns>false=上下ポップアップを表示</returns>
		private bool popupTenkeyOn(string controlName )
        {
			_controlName = controlName;
			string changeVal = "";	//編集値
            Decimal lowerLimitDec = 0;//最小値
            Decimal upperLimitDec = 0;//最大値

			if( _popupTenkey != null)
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
			switch( _controlName ) {
				case "ElectrodeNumberSetTextBox":   //ESF
                    titleString = ElectrodeCount.Text;
                    //titleString = ElectrodeCount.Text.Substring(0, 3);//先頭から3桁
                    changeVal = ElectrodeNumberSetTextBox.Text;
                    lowerLimitDec = (decimal)1;
                    upperLimitDec = (decimal)ElctCountUpp;
                    strTenkeyMode = "aec_Up";
                    break;

                case "EsfCylinderNumberText":       //電極交換
                    titleString = labelEx1.Text;
                    changeVal = _EsfCylinderNumberText.Text;
                    lowerLimitDec = (decimal)0;
                    upperLimitDec = (decimal)ElctCountUpp;
                    strTenkeyMode = "aec_Dn";
                    break;
            }
			//フォーマット：ゼロ埋め整数3桁
			NumericTextBox.FormatTypes formatType = NumericTextBox.FormatTypes.IntegerZeroPlace3;
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
            switch (_controlName)
            {
                case "ElectrodeNumberSetTextBox":
                    _EditingElctNum = true;
                    ElectrodeNumberSetTextBox.Text = retVal;
                  

                    break;//ESF
                case "EsfCylinderNumberText": _EsfCylinderNumberText.Text = retVal; break;//電極交換
            }
        }
		#endregion
		#region<単体操作：ボタンイベントハンドラ>
		/// <summary>
		///	ESF：フィンガーアーム：開く(OPEN)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonEx_ESFFingerArmOpen_Click( object sender, EventArgs e )
		{
			SettingSeqNameView( AECSequences.FingerArmUnClamp, sender as ButtonEx);
		}
		/// <summary>
		///	ESF：フィンガーアーム：閉じる(CLOSE)：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonEx_ESFFingerArmClose_Click( object sender, EventArgs e )
		{
			SettingSeqNameView( AECSequences.FingerArmClamp, sender as ButtonEx);
		}
		/// <summary>
		///	ESF：シリンダー：前進1：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonEx_ESFCylinderExtend1_Click( object sender, EventArgs e )
		{
			SettingSeqNameView( AECSequences.FingerArmFront1, sender as ButtonEx);
		}
		/// <summary>
		///	ESF：シリンダー：前進2：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonEx_ESFCylinderExtend2_Click( object sender, EventArgs e )
		{
			SettingSeqNameView( AECSequences.FingerArmFront2, sender as ButtonEx);
		}
		/// <summary>
		///	ESF：シリンダー：後退：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonEx_ESFCylinderRetract_Click( object sender, EventArgs e )
		{
			SettingSeqNameView( AECSequences.FingerArmBack, sender as ButtonEx);
		}
		///	GSF：シリンダー：前進：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonEx_GSFCylinderExtend_Click( object sender, EventArgs e )
		{
			SettingSeqNameView( AECSequences.GsfArmFront, sender as ButtonEx);
		}
		///	GSF：シリンダー：後退：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonEx_GSFCylinderRetract_Click( object sender, EventArgs e )
		{
			SettingSeqNameView( AECSequences.GsfArmBack, sender as ButtonEx);
		}
        /// <summary>
        /// ガイド貫通動作スタート
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonEx_GuideThroughSequence_Click(object sender, EventArgs e)
        {
            SettingSeqNameView(AECSequences.GuideThrough, sender as ButtonEx);
        }
        #endregion

        private void AECForm_Deactivate(object sender, EventArgs e)
        {
            AecSeq = AECSequences.Null;
        }

        private void _CloseBtn_Click(object sender, EventArgs e)
        {
            _threadStop = true;
        }
    }
}
