///////////////////////////////////////////////////////////////////////////////////////////
//
// (1) システム名称 : ELENIX ECNCⅢ
// (2) ファイル名   : ESFMainForm.cs
// (3) 概要         : ESF手動操作画面
// (4) 作成日       : 2015.06.01
// (5) 作成者       : 八野
// (6) 特記事項     : VisualStudio2015 C#
// (7) 更新履歴     :
//                    2017.11.27：ESF画像処理追加：柏原
///////////////////////////////////////////////////////////////////////////////////////////


using ECNC3.Enumeration;
using ECNC3.Models.McIf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ECNC3.Models;         //FileSettings用

namespace ECNC3.Views
{
    #region Enums
    enum EsfSetting
    {
        /// <summary>
        /// 電極交換位置X
        /// </summary>
        ElctdExchPosX,
        /// <summary>
        /// 電極交換位置Y
        /// </summary>
        ElctdExchPosY,
        /// <summary>
        /// 電極交換位置W
        /// </summary>
        ElctdExchPosW,
        /// <summary>
        /// 電極交換位置ABC
        /// </summary>
        ElctdExchPosABC,
        /// <summary>
        /// W軸電極交換前位置オフセット
        /// </summary>
        ElctdExchOfsW1,
        /// <summary>
        /// W軸電極交換待機位置オフセット
        /// </summary>
        ElctdExchOfsW2,
        /// <summary>
        /// 軸電極交換位置移動速度
        /// </summary>
        ElctdExchSpdW,
        /// <summary>
        /// 軸電極交換前位置移動速度
        /// </summary>
        ElctdExchSpdW1,
        /// <summary>
        /// ESF電極搭載数
        /// </summary>
        EsfMaxNumSet,
        /// <summary>
        /// 有効軸
        /// </summary>
        EnableAxisSet,
        /// <summary>
        /// 電極交換時CRS速度
        /// </summary>
        CrsSpdSet
    }
    #endregion

    public partial class ESFMainForm : ECNC3Form
    {
		#region Constructor
		private string _strSubmicronValue = "";
		//-----------------------------------------------------------------------------------------
		//
		//　フォーム　コンストラクタ
		//
		//-----------------------------------------------------------------------------------------
		public ESFMainForm()
        {
            InitializeComponent();
            Disposed += ESFMainForm_Disposed;

			//下3桁か4桁か取得：サブミクロン
			CncSys_AxisInfomation_XmlRead( ref _submicronFlg );
			if( _submicronFlg ) _strSubmicronValue = "###0'.'0000";
			else _strSubmicronValue = "###0'.'000";
            //ESF画像読み込み
            ImageDisplay("ESF");
        }
        #endregion

        #region Member
        /// <summary>ポップアップテンキー</summary>
        private TenKeyDialog _popupTenkey = new TenKeyDialog("", 0, 0, 0);//初回インスタンスを作っておく
        /// <summary>搭載電極数</summary>
        private int ElctCountUpp = 0;
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

        #region LoopMethod
        internal void StatusMonitoring(StatusMonitorEventArgs e)
        {
            if(Disposing)
            {
                return;
            }
            if(this == null)
            {
                return;
            }
            if( (this.IsDisposed == true || this.Disposing == true)   ) return; retInvoke = BeginInvoke((MethodInvoker)delegate
            {
                //機械座標表示
                UpdateAxisPosition(e.Items.MacAxisPos);
                //有効軸表示
                UpdateEnAxis(e.Items.AxisAEn, e.Items.AxisBEn, e.Items.AxisCEn);
                //AEC表示更新
                AecLampChg(e.Items);
            }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
            if(e.Items.StartSwBtnOffEdge == true
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
                    if((ctrl as ButtonEx).Enabled != true) (ctrl as ButtonEx).Enabled = true;
                }
            }
        }
        private void _AllButtonsDeActivate(Control.ControlCollection target)
        {
            if (target == null) return;
            foreach(Control ctrl in target)
            {
                if (ctrl.Controls.Count != 0) _AllButtonsDeActivate(ctrl.Controls);
                if(ctrl.GetType() == typeof(ButtonEx))
                {
                    if((ctrl as ButtonEx).Enabled != false) (ctrl as ButtonEx).Enabled = false;
                }
            }
        }
		private bool _submicronFlg = false;
		/// <summary>
		///  CncSys.xml：軸関係情報設定<AxisInfomation>からサブミクロン(下3か4桁)表示を取得
		/// </summary>
		/// <param name="submicronFlg"></param>
		private void CncSys_AxisInfomation_XmlRead( ref bool submicronFlg )
		{
			int intDigit = 0;
			try {
				using( FileSettings fs = new FileSettings() ) {
					//ファイル読み込み
					fs.Read();
					//サブミクロン表示(submicron)：取得
					intDigit = fs.AttrValue( "Root/AxisInfomation/Position ", "digit" );
					if( intDigit == 4 ) {
						submicronFlg = true;
					} else {
						submicronFlg = false;
					}
				}
			} catch( Exception exc ) {
				//例外処理
				MessageBox.Show( exc.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error );
			}
		}
		#endregion

		#region EventHandler
		//-----------------------------------------------------------------------------------------
		//
		//　フォーム　ロード時のイベントハンドラ
		//
		//-----------------------------------------------------------------------------------------
		private void ESFMainForm_Load(object sender, EventArgs e)
        {
            InitInput(_esfMaxNumText, NumericTextBox.FormatTypes.IntegerZeroPlace3);
            _esfMaxNumText.LowerLimit = 0;
            _esfMaxNumText.UpperLimit = 999;
            _esfMaxNumText.Text = ElctCountUpp.ToString("000");

            InitInput(_crsSpdText, NumericTextBox.FormatTypes.IntegerZeroPlace2);
            _crsSpdText.LowerLimit = 0;
            _crsSpdText.UpperLimit = 15;
            _crsSpdText.FormatType = NumericTextBox.FormatTypes.IntegerZeroPlace2;
            UpdateSettingParameter();
            UpdateAxisParameter();
        }
        private void ESFMainForm_Disposed(object sender, EventArgs e)
        {
            if (null != _notifyReturn)
            {
                _notifyReturn = null;
            }
        }
        private void ColetClampOpenBt_Click(object sender, EventArgs e)
        {
            SettingSeqLampChg(AECSequences.SpindleUnClamp);
        }
        private void ColetClampCloseBt_Click(object sender, EventArgs e)
        {
            SettingSeqLampChg(AECSequences.SpindleClamp);
        }
		/// <summary>
		/// フィンガーアーム：開く(OPEN)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _fingerArmOpBt_Click(object sender, EventArgs e)
        {
            SettingSeqLampChg(AECSequences.FingerArmUnClamp);
        }
		/// <summary>
		/// ESF：フィンガーアーム：閉じる(CLOSE)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _fingerArmClBt_Click(object sender, EventArgs e)
        {
            SettingSeqLampChg(AECSequences.FingerArmClamp);
        }
		/// <summary>
		/// ESF：フィンガーアーム：シリンダー：後退(RERACT)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _fingerArmBkBt_Click(object sender, EventArgs e)
        {
            SettingSeqLampChg(AECSequences.FingerArmBack);
        }
		/// <summary>
		/// ESF：フィンガーアーム：シリンダー：前進1(EXTEND1)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _fingerArmFr1Bt_Click(object sender, EventArgs e)
        {
            SettingSeqLampChg(AECSequences.FingerArmFront1);
        }
		/// <summary>
		/// ESF：ィンガーアーム：シリンダー：前進1(EXTEND2)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _fingerArmFr2Bt_Click(object sender, EventArgs e)
        {
            SettingSeqLampChg(AECSequences.FingerArmFront2);
        }
        private void IndexMoveBt_Click(object sender, EventArgs e)
        {
            if (_CrsSpinBt.GetLed() == false)
            {
                SettingSeqLampChg(AECSequences.CrsSpin);
                return;
            }
            using (SequenceFunction SeqFunc = new SequenceFunction(0))
            {
                ResultCodes Error = (SeqFunc.SequenceMonitoring(Sequences.SpindleOn));
                //ログ処理
                UILog logs = new UILog("MANUALForm.SeqFunc.SequenceMonitoring");
                if (Error == ResultCodes.Success)
                {
                    logs.Sure("SPINDLE_CW-OFF Result = " + Error);
                }
                else
                {
                    logs.Error("SPINDLE_CW-OFF Result = " + Error);
                }
            }
        }
        private void _zOriginMoveBt_Click(object sender, EventArgs e)
        {
            SettingSeqLampChg(AECSequences.ZOriginMove);
        }
        private void _elctdExchOfsW2_WriteBt_Click(object sender, EventArgs e)
        {
            _paramChg(EsfSetting.ElctdExchOfsW2);
        }

        private void _elctdExchOfsW1_WriteBt_Click(object sender, EventArgs e)
        {
            _paramChg(EsfSetting.ElctdExchOfsW1);
        }

        private void _elctdExchPosW_WriteBt_Click(object sender, EventArgs e)
        {
            _paramChg(EsfSetting.ElctdExchPosW);
        }

        private void _elctdExchPosABC_WriteBt_Click(object sender, EventArgs e)
        {
            _paramChg(EsfSetting.ElctdExchPosABC);
        }

        private void _elctdExchPosX_WriteBt_Click(object sender, EventArgs e)
        {
            _paramChg(EsfSetting.ElctdExchPosX);
        }

        private void _elctdExchPosY_WriteBt_Click(object sender, EventArgs e)
        {
            _paramChg(EsfSetting.ElctdExchPosY);
        }

        private void _elctdExchOfsW2_MoveBt_Click(object sender, EventArgs e)
        {
            SettingSeqLampChg( AECSequences.ElctdExchOfsW2);
        }

        private void _elctdExchOfsW1_MoveBt_Click(object sender, EventArgs e)
        {
            SettingSeqLampChg(AECSequences.ElctdExchOfsW1);
        }

        private void _elctdExchPosW_MoveBt_Click(object sender, EventArgs e)
        {
            SettingSeqLampChg(AECSequences.ElctdExchPosW);
        }

        private void _elctdExchPosABC_MoveBt_Click(object sender, EventArgs e)
        {
            SettingSeqLampChg(AECSequences.ElctdExchPosABC);
        }

        private void _elctdExchPosX_MoveBt_Click(object sender, EventArgs e)
        {
            SettingSeqLampChg(AECSequences.ElctdExchPosX);
        }

        private void _elctdExchPosY_MoveBt_Click(object sender, EventArgs e)
        {
            SettingSeqLampChg(AECSequences.ElctdExchPosY);
        }
        /// <summary>
        /// 有効軸切替ボタンクリック時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnableAxisBt_Click(object sender, EventArgs e)
        {
            _paramChg(EsfSetting.EnableAxisSet);
        }

        private void _crsSpdText_Click(object sender, EventArgs e)
        {
            if (_crsSpdText.GetLamp() == false)
            {
                _crsSpdText.SetLamp(true);
                popupTenkeyOn(_crsSpdText);
            }
        }
        private void _esfMaxNumText_Click(object sender, EventArgs e)
        {
            if (_esfMaxNumText.GetLamp() == false)
            {
                _esfMaxNumText.SetLamp(true);
                popupTenkeyOn(_esfMaxNumText);
            }
        }
        //-----------------------------------------------------------------------------------------
        //
        //　閉じる　ボタン　クリック時のイベントハンドラ
        //
        //-----------------------------------------------------------------------------------------
        private void BackToServiceFormBt_Click(object sender, EventArgs e)
        {
            _notifyReturn?.Invoke();
        }
         #endregion

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

            //コレットクランプ
            if (items.ColletClampOn != _elctClampClBt.GetLed())
            {
                _elctClampClBt.SetLed(items.ColletClampOn);
            }
            if (!items.ColletClampOn != _elctClampOpBt.GetLed())
            {
                _elctClampOpBt.SetLed(!items.ColletClampOn);
            }
            //フィンガーアーム
            if (items.FingerArmClampOn != _fingerArmClBt.GetLed())
            {
                _fingerArmClBt.SetLed(items.FingerArmClampOn);
            }
            if (!items.FingerArmClampOn != _fingerArmOpBt.GetLed())
            {
                _fingerArmOpBt.SetLed(!items.FingerArmClampOn);
            }
            //フィンガーアーム位置
            if(items.FingerArmPos == EsfArmPositions.Back)
            {
                if(!_fingerArmBkBt.GetLed()) _fingerArmBkBt.SetLed(true);
            }
            else
            {
                if (_fingerArmBkBt.GetLed()) _fingerArmBkBt.SetLed(false);
            }
            if (items.FingerArmPos == EsfArmPositions.Middle)
            {
                if(!_fingerArmFr1Bt.GetLed()) _fingerArmFr1Bt.SetLed(true);
            }
            else
            {
                if(_fingerArmFr1Bt.GetLed()) _fingerArmFr1Bt.SetLed(false);
            }
            if (items.FingerArmPos == EsfArmPositions.Foward)
            {
                if(!_fingerArmFr2Bt.GetLed()) _fingerArmFr2Bt.SetLed(true);
            }
            else
            {
                if (_fingerArmFr2Bt.GetLed()) _fingerArmFr2Bt.SetLed(false);
            }
            //主軸回転
            if (items.SpindleOn != _CrsSpinBt.GetLed())
            {
                _CrsSpinBt.SetLed(items.SpindleOn);
            }
        }

        /// <summary>
        /// 動作名表示とスタートボタンのトリガーに設定
        /// </summary>
        /// <param name="_seq">動作名</param>
        private void SettingSeqLampChg(AECSequences _seq)
        {
            AecSeq = _seq;
        }
        #endregion

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

            if( (this.IsDisposed == true || this.Disposing == true)   ) return ResultCodes.NotFound;
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
                        foreach(Models.StructurePositioniingItem positem in PosItems)
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

                        case AECSequences.SpindleClamp:
                            if (MonitorFunc.SequenceConditionMonitoring(MonitorTargets.CompletedOrigin) == true)
                            {
                                ret = SeqFunc.SequenceMonitoring(Sequences.SpindleClamp);
                            }

                            break;

                        case AECSequences.SpindleUnClamp:
                            if (MonitorFunc.SequenceConditionMonitoring(MonitorTargets.CompletedOrigin) == true)
                            {
                                ret = SeqFunc.SequenceMonitoring(Sequences.SpindleUnClamp);
                            }

                            break;

                        case AECSequences.FingerArmClamp:
                            if (MonitorFunc.SequenceConditionMonitoring(MonitorTargets.CompletedOrigin) == true)
                            {
                                ret = SeqFunc.SequenceMonitoring(Sequences.FingerArmClamp);
                            }
                            break;

                        case AECSequences.FingerArmUnClamp:
                            if (MonitorFunc.SequenceConditionMonitoring(MonitorTargets.CompletedOrigin) == true)
                            {
                                ret = SeqFunc.SequenceMonitoring(Sequences.FingerArmUnClamp);
                            }
                            break;

                        case AECSequences.FingerArmBack:
                            if(MonitorFunc.SequenceConditionMonitoring(MonitorTargets.CompletedOrigin) == true)
                            {
                                ret = SeqFunc.SequenceMonitoring(Sequences.FingerArmBack);
                            }

                            break;

                        case AECSequences.FingerArmFront1:
                            if (MonitorFunc.SequenceConditionMonitoring(MonitorTargets.CompletedOrigin) == true)
                            {
                                ret = SeqFunc.SequenceMonitoring(Sequences.FingerArmFront1);
                            }

                            break;

                        case AECSequences.FingerArmFront2:
                            if (MonitorFunc.SequenceConditionMonitoring(MonitorTargets.CompletedOrigin) == true)
                            {
                                ret = SeqFunc.SequenceMonitoring(Sequences.FingerArmFront2);
                            }

                            break;

                        case AECSequences.MagazineIncFront:
                            if (MonitorFunc.SequenceConditionMonitoring(MonitorTargets.CompletedOrigin) == true)
                            {
                                ret = SeqFunc.SequenceMonitoring(Sequences.MagazineIncFront);
                            }
                            break;

                        case AECSequences.CrsSpin:
                            if (MonitorFunc.SequenceConditionMonitoring(MonitorTargets.CompletedOrigin) == true)
                            {
                                SeqFunc.SeqMode = 1;
                                ret = SeqFunc.SequenceMonitoring(Sequences.SpindleOn);
                            }
                            break;


                        case AECSequences.ZOriginMove:
                            ret = SeqFunc.SequenceMonitoring(Sequences.ZOrigin);
                            break;

                        case AECSequences.ElctdExchPosX:
                            ChkPosText(_elctdExchPosXValue.Text, out Target);
                            ret = PositioningAxis(items, Target, AxisName.AxisX, AxisMoveMode.Abs);
                            break;

                        case AECSequences.ElctdExchPosY:
                            ChkPosText(_elctdExchPosYValue.Text, out Target);
                            ret = PositioningAxis(items, Target, AxisName.AxisY, AxisMoveMode.Abs);
                            break;

                        case AECSequences.ElctdExchPosW:
                            ChkPosText(_elctdExchPosWValue.Text, out Target);
                            ret = PositioningAxis(items, Target, AxisName.AxisW, AxisMoveMode.Abs);
                            break;

                        case AECSequences.ElctdExchPosABC:
                            int targetA = 0;
                            int targetB = 0;
                            int targetC = 0;
                            ChkPosText(_elctdExchPosAValue.Text, out targetA);
                            ChkPosText(_elctdExchPosBValue.Text, out targetB);
                            ChkPosText(_elctdExchPosCValue.Text, out targetC);
                            ret = PositioningAxis(items, targetA, AxisName.AxisA, AxisMoveMode.Abs,
                                                 targetB, AxisName.AxisB,
                                                 targetC, AxisName.AxisC
                                                 );
                            break;

                        case AECSequences.ElctdExchOfsW1:
                            int targetW1 = 0;
                            int targetW1Ofs1 = 0;
                            ChkPosText(_elctdExchPosWValue.Text, out targetW1);
                            ChkPosText(_elctdExchOfsW1Value.Text, out targetW1Ofs1);
                            ret = PositioningAxis(items, targetW1 + targetW1Ofs1, AxisName.AxisW, AxisMoveMode.Abs);
                            break;

                        case AECSequences.ElctdExchOfsW2:
                            int targetW2 = 0;
                            int targetW2Ofs2 = 0;
                            ChkPosText(_elctdExchPosWValue.Text, out targetW2);
                            ChkPosText(_elctdExchOfsW2Value.Text, out targetW2Ofs2);
                            ret = PositioningAxis(items, targetW2 + targetW2Ofs2, AxisName.AxisW, AxisMoveMode.Abs);
                            break;

                    }
                }
            }
            if (ret == Enumeration.ResultCodes.Success)
            {
                if( (this.IsDisposed == true || this.Disposing == true)   ) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                {
                    SettingSeqLampChg(AECSequences.Null);
                }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
            }
        }

        private ResultCodes SetInitialParam(EsfSetting esfSetting)
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
                    case EsfSetting.ElctdExchPosX:
                        ChkPosText(_macPosXLabel.Text, out outParameter);
                        ms.ElctdExchPos[0] = outParameter;
                        break;

                    case EsfSetting.ElctdExchPosY:
                        ChkPosText(_macPosYLabel.Text, out outParameter);
                        ms.ElctdExchPos[1] = outParameter;
                        break;

                    case EsfSetting.ElctdExchPosW:
                        ChkPosText(_macPosWLabel.Text, out outParameter);
                        ms.ElctdExchPos[2] = outParameter;
                        break;

                    case EsfSetting.ElctdExchPosABC:
                        ChkPosText(_macPosALabel.Text, out outParameter);
                        ms.ElctdExchPos[4] = outParameter;
                        ChkPosText(_macPosBLabel.Text, out outParameter);
                        ms.ElctdExchPos[5] = outParameter;
                        ChkPosText(_macPosCLabel.Text, out outParameter);
                        ms.ElctdExchPos[6] = outParameter;

                        break;

                        //case EsfSetting.ElctdExchSpdW:
                        //    ChkPosText(_elctdExchOfsW2Value.Text, out outParameter);
                        //    ms.ElctdExchOfsW2 = outParameter;
                        //    break;

                        //case EsfSetting.ElctdExchSpdW1:
                        //    ChkPosText(_elctdExchOfsW2Value.Text, out outParameter);
                        //    ms.ElctdExchOfsW2 = outParameter;
                        //    break;
                        
                    case EsfSetting.ElctdExchOfsW1:
                        ChkPosText(_elctdExchPosWValue.Text, out outParameter);
                        ChkPosText(_macPosWLabel.Text, out outParameter2);
                        ms.ElctdExchOfsW1 = outParameter2 - outParameter;
                        break;

                    case EsfSetting.ElctdExchOfsW2:
                        ChkPosText(_elctdExchPosWValue.Text, out outParameter);
                        ChkPosText(_macPosWLabel.Text, out outParameter2);
                        ChkPosText(_elctdExchOfsW1Value.Text, out outParameter3);
                        ms.ElctdExchOfsW2 = outParameter2 - outParameter;
                        break;

                    case EsfSetting.EsfMaxNumSet:
                        ms.ElectrodeCount = (short)(_esfMaxNumText.Value);
                        break;

                    case EsfSetting.EnableAxisSet:
                        using (McDatRomSwitch rom = new McDatRomSwitch())
                        {
                            rom.Read();
                            if (rom.EnableAxisA == _AxisAEn.GetLed())
                            {
                                rom.EnableAxisA = !_AxisAEn.GetLed();
                            }
                            if (rom.EnableAxisA == _AxisBEn.GetLed())
                            {
                                rom.EnableAxisB = !_AxisBEn.GetLed();
                            }
                            if (rom.EnableAxisA == _AxisCEn.GetLed())
                            {
                                rom.EnableAxisC = !_AxisCEn.GetLed();
                            }
                            return rom.Write();
                        }

                    case EsfSetting.CrsSpdSet:
                        ms.ElctdClumpSpdS = int.Parse(_crsSpdText.Text);
                        break;
                }
                return ms.Write();
            }
        }

        #endregion
        
        #region UpdateMethods
        private void UpdateAxisPosition(Models.StructureAxisCoordinate _macPos)
        {
			int bairitsu = 1;//倍率
			if( _submicronFlg ) bairitsu = 10;
			_macPosXLabel.Text = ( _macPos.AxisX * bairitsu ).ToString( _strSubmicronValue );//.xxxか.xxxx表示
			_macPosYLabel.Text = ( _macPos.AxisY * bairitsu ).ToString( _strSubmicronValue );
			_macPosWLabel.Text = ( _macPos.AxisW * bairitsu ).ToString( _strSubmicronValue );
			_macPosZLabel.Text = ( _macPos.AxisZ * bairitsu ).ToString( _strSubmicronValue );
			_macPosALabel.Text = ( _macPos.AxisA * bairitsu ).ToString( _strSubmicronValue );
			_macPosBLabel.Text = ( _macPos.AxisB * bairitsu ).ToString( _strSubmicronValue );
			_macPosCLabel.Text = ( _macPos.AxisC * bairitsu ).ToString( _strSubmicronValue );
		}
        private void UpdateEnAxis(bool axisA, bool axisB, bool axisC)
        {
            if (_AxisAEn.GetLed() != axisA)
            {
                _AxisAEn.SetLed(axisA);
            }
            if (_AxisBEn.GetLed() != axisB)
            {
                _AxisBEn.SetLed(axisB);
            }
            if (_AxisCEn.GetLed() != axisC)
            {
                _AxisCEn.SetLed(axisC);
            }
        }
        private void UpdateSettingParameter()
        {
            using (MonitoringFunction MonitorFunc = new MonitoringFunction())
            {
                ElctCountUpp = MonitorFunc.RetIntegerMonitoringResult(MonitorTargets.ElectrodeCount);
            }
            using (McDatInitialPrm DatIni = new McDatInitialPrm())
            {
                DatIni.Read();
                if (_crsSpdText.Text == DatIni.ElctdClumpSpdS.ToString("00"))
                {
                    return;
                }
                _crsSpdText.Text = DatIni.ElctdClumpSpdS.ToString("00");
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
				if( _submicronFlg ) bairitsu = 10;
				_elctdExchPosXValue.Text = ( IniPrm.ElctdExchPos[0] * bairitsu ).ToString( _strSubmicronValue );
				_elctdExchPosYValue.Text = ( IniPrm.ElctdExchPos[1] * bairitsu ).ToString( _strSubmicronValue );
				_elctdExchPosWValue.Text = ( IniPrm.ElctdExchPos[2] * bairitsu ).ToString( _strSubmicronValue );
				_elctdExchPosAValue.Text = ( IniPrm.ElctdExchPos[4] * bairitsu ).ToString( _strSubmicronValue );
				_elctdExchPosBValue.Text = ( IniPrm.ElctdExchPos[5] * bairitsu ).ToString( _strSubmicronValue );
				_elctdExchPosCValue.Text = ( IniPrm.ElctdExchPos[6] * bairitsu ).ToString( _strSubmicronValue );
				_elctdExchOfsW1Value.Text = ( IniPrm.ElctdExchOfsW1 * bairitsu ).ToString( _strSubmicronValue );
				_elctdExchOfsW2Value.Text = ( IniPrm.ElctdExchOfsW2 * bairitsu ).ToString( _strSubmicronValue );
				_elctdExchSpdWValue.Text = ( IniPrm.ElctdExchSpdW ).ToString();
				_elctdExchSpdW1Value.Text = ( IniPrm.ElctdExchSpdW1 ).ToString();
				//ダミー表示
				_throughSenserLabel.Text = (0 * bairitsu ).ToString( _strSubmicronValue );

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
                case "_crsSpdText":   //ESF
                    titleString = _CrsGroupBox.Text;
                    changeVal = _crsSpdText.Text;
                    lowerLimitDec = (decimal)0;
                    upperLimitDec = (decimal)ElctCountUpp;
                    strTenkeyMode = "esf_crs";
                    //フォーマット：ゼロ埋め整数2桁
                    formatType = NumericTextBox.FormatTypes.IntegerZeroPlace2;
                    break;

                case "_esfMaxNumText":       //電極交換
                    titleString = _EsfMaxGroupBox.Text;
                    changeVal = _esfMaxNumText.Text;
                    lowerLimitDec = (decimal)0;
                    upperLimitDec = (decimal)ElctCountUpp;
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
            _popupTenkey.FormClosed += popupTenkey_Closed;
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
                case "_crsSpdText":
                    _crsSpdText.Text = retVal;
                    //CRS書き込み
                    _paramChg(EsfSetting.CrsSpdSet);
                    //CRS書き込み確認
                    UpdateSettingParameter();
                    _crsSpdText.SetLamp(false);
                    break;//ESF
                case "_esfMaxNumText":
                    _esfMaxNumText.Text = retVal;
                    //CRS書き込み
                    _paramChg(EsfSetting.EsfMaxNumSet);
                    //CRS書き込み確認
                    UpdateSettingParameter();
                    _esfMaxNumText.SetLamp(false);
                    break;//電極交換
            }
        }
        private void popupTenkey_Closed(object sender, FormClosedEventArgs e)
        {
            if(_crsSpdText.GetLamp() == true) _crsSpdText.SetLamp(false);
            if(_esfMaxNumText.GetLamp() == true) _esfMaxNumText.SetLamp(false);
        }
        #endregion

        #region 画像読み込みと表示
        /// <summary>
        /// 画像ファイル表示
        /// </summary>
        /// <param name="strName"></param>
        private void ImageDisplay(string strName)
        {
            string pathName = FilePathInfo.ECNC3PATH + @"Picture\" + strName + ".png";                                        //相対パス"Picture\\"+ ModeName +".png"の絶対パスを取得する
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
        private void _paramChg(EsfSetting setting)
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
            if(((ButtonEx)sender).GetBack())
            {
                ((ButtonEx)sender).SetBack(false);
            }
            else
            {
                _AllControlsBackColorChg(false);
                ((ButtonEx)sender).SetBack(true);
            }
        }

    }
}
