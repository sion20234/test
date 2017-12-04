///////////////////////////////////////////////////////////////////////////////////////////
//
// (1) システム名称 : ELENIX ECNCⅢ
// (2) ファイル名   : OptionForm.cs
// (3) 概要         : オプション画面
// (4) 作成日       : 2015.06.01
// (5) 作成者       : 八野
// (6) 特記事項     : VisualStudio2015 C#
// (7) 更新履歴     :
//
///////////////////////////////////////////////////////////////////////////////////////////


using ECNC3.Enumeration;
using ECNC3.Models;
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
    public partial class OptionForm : ECNC3Form
    {
        //-----------------------------------------------------------------------------------------
        //
        //　フォーム　コンストラクタ
        //
        //-----------------------------------------------------------------------------------------
        public OptionForm()
        {
            InitializeComponent();
        }

        PlotForm PlForm;
        ValiableListForm SelectMacro;
        public StructureAutoModeOptions _options = new StructureAutoModeOptions();
        /// <summary>ポップアップテンキー</summary>
        private TenKeyDialog _popupTenkey = new TenKeyDialog("0" , 0, 0, 0);//初回インスタンスを作っておく
        bool _threadStop = false;


        //-----------------------------------------------------------------------------------------
        //
        //　閉じる　ボタン　クリック時のイベントハンドラ
        //
        //-----------------------------------------------------------------------------------------
        private void BackMDIAUTOFormBt_Click()
        {
            this.Close();
        }

        private void PlotBt_Click(object sender, EventArgs e)
        {
            PlForm = new PlotForm();
            PlForm.Show(this);
        }

        private void ValiableListBt_Click(object sender, EventArgs e)
        {
            SelectMacro = new ValiableListForm();
            SelectMacro.Show(this);
        }

        private void AecByLifeBt_Click(object sender, EventArgs e)
        {
            if (_options.AecEn == false)
            {
                using (SettingFunction SetFunc = new SettingFunction(1))
                {
                    ResultCodes Error = SetFunc.SettingMonitoring(Settings.AECByLife);
                    UILog logs = new UILog("OptionForm.SetFunc.SettingMonitoring");
                    if (Error == ResultCodes.Success)
                    {
                        logs.Sure("AEC-ON Result = " + Error);
                    }
                    else
                    {
                        logs.Error("AEC-OFF Result = " + Error);
                    }
                }
            }
            else
            {
                using (SettingFunction SetFunc = new SettingFunction(0))
                {
                    ResultCodes Error = SetFunc.SettingMonitoring(Settings.AECByLife);
                    UILog logs = new UILog("OptionForm.SetFunc.SettingMonitoring");
                    if (Error == ResultCodes.Success)
                    {
                        logs.Sure("AEC-OFF Result = " + Error);
                    }
                    else
                    {
                        logs.Error("AEC-OFF Result = " + Error);
                    }
                }
            }
        }

        private void PartitionRoundStopBt_Click(object sender, EventArgs e)
        {
            if (_options.AecWaitEn == false)
            {
                using (SettingFunction SetFunc = new SettingFunction(1))
                {
                    ResultCodes Error = SetFunc.SettingMonitoring(Settings.PartitionRoundStop);
                    UILog logs = new UILog("OptionForm.SetFunc.SettingMonitoring");
                    if (Error == ResultCodes.Success)
                    {
                        logs.Sure("PARTITIONROUNDSTOP-ON Result = " + Error);
                    }
                    else
                    {
                        logs.Error("PARTITIONROUNDSTOP-ON Result = " + Error);
                    }
                }
            }
            else
            {
                using (SettingFunction SetFunc = new SettingFunction(0))
                {
                    ResultCodes Error = SetFunc.SettingMonitoring(Settings.PartitionRoundStop);
                    UILog logs = new UILog("OptionForm.SetFunc.SettingMonitoring");
                    if (Error == ResultCodes.Success)
                    {
                        logs.Sure("PARTITIONROUNDSTOP-OFF Result = " + Error);
                    }
                    else
                    {
                        logs.Error("PARTITIONROUNDSTOP-OFF Result = " + Error);
                    }
                }
            }
        }

        private void BuzzerBt_Click(object sender, EventArgs e)
        {
            if (_options.BuzzerEn == false)
            {
                using (SettingFunction SetFunc = new SettingFunction(1))
                {
                    ResultCodes Error = SetFunc.SettingMonitoring(Settings.Buzzer);
                    UILog logs = new UILog("OptionForm.SetFunc.SettingMonitoring");
                    if (Error == ResultCodes.Success)
                    {
                        logs.Sure("BUZZER-ON Result = " + Error);
                    }
                    else
                    {
                        logs.Error("BUZZER-ON Result = " + Error);
                    }
                }
            }
            else
            {
                using (SettingFunction SetFunc = new SettingFunction(0))
                {
                    ResultCodes Error = SetFunc.SettingMonitoring(Settings.Buzzer);
                    UILog logs = new UILog("OptionForm.SetFunc.SettingMonitoring");
                    if (Error == ResultCodes.Success)
                    {
                        logs.Sure("BUZZER-OFF Result = " + Error);
                    }
                    else
                    {
                        logs.Error("BUZZER-OFF Result = " + Error);
                    }
                }
            }
        }

        private void OptionalStopBt_Click(object sender, EventArgs e)
        {
            if (_options.OptionalStopEn == false)
            {
                using (SettingFunction SetFunc = new SettingFunction(1))
                {
                    ResultCodes Error = SetFunc.SettingMonitoring(Settings.OptionalStop);
                    UILog logs = new UILog("OptionForm.SetFunc.SettingMonitoring");
                    if (Error == ResultCodes.Success)
                    {
                        logs.Sure("OPTIONALSTOP-ON Result = " + Error);
                    }
                    else
                    {
                        logs.Error("OPTIONALSTOP-ON Result = " + Error);
                    }
                }
            }
            else
            {
                using (SettingFunction SetFunc = new SettingFunction(0))
                {
                    ResultCodes Error = SetFunc.SettingMonitoring(Settings.OptionalStop);
                    UILog logs = new UILog("OptionForm.SetFunc.SettingMonitoring");
                    if (Error == ResultCodes.Success)
                    {
                        logs.Sure("OPTIONALSTOP-OFF Result = " + Error);
                    }
                    else
                    {
                        logs.Error("OPTIONALSTOP-OFF Result = " + Error);
                    }
                }
            }
        }

        private void BlockSkipBt_Click(object sender, EventArgs e)
        {
            if (_options.BlockSkipEn == false)
            {
                using (SettingFunction SetFunc = new SettingFunction(1))
                {
                    ResultCodes Error = SetFunc.SettingMonitoring(Settings.BlockSkipEn);
                    UILog logs = new UILog("OptionForm.SetFunc.SettingMonitoring");
                    if (Error == ResultCodes.Success)
                    {
                        logs.Sure("BLOCKSKIP-ON Result = " + Error);
                    }
                    else
                    {
                        logs.Error("BLOCKSKIP-ON Result = " + Error);
                    }
                }
            }
            else
            {
                using (SettingFunction SetFunc = new SettingFunction(0))
                {
                    ResultCodes Error = SetFunc.SettingMonitoring(Settings.BlockSkipEn);
                    UILog logs = new UILog("OptionForm.SetFunc.SettingMonitoring");
                    if (Error == ResultCodes.Success)
                    {
                        logs.Sure("BLOCKSKIP-OFF Result = " + Error);
                    }
                    else
                    {
                        logs.Error("BLOCKSKIP-OFF Result = " + Error);
                    }
                }
            }
        }

        private void IncRefMoveBt_Click(object sender, EventArgs e)
        {
            if (_options.RefMoveEn == false)
            {
                using (SettingFunction SetFunc = new SettingFunction(1))
                {
                    ResultCodes Error = SetFunc.SettingMonitoring(Settings.IncrimentalReferenceAxisMove);
                    UILog logs = new UILog("OptionForm.SetFunc.SettingMonitoring");
                    if (Error == ResultCodes.Success)
                    {
                        logs.Sure("INCREFAXISMOVE-ON Result = " + Error);
                    }
                    else
                    {
                        logs.Error("INCREFAXISMOVE-ON Result = " + Error);
                    }
                }
            }
            else
            {
                using (SettingFunction SetFunc = new SettingFunction(0))
                {
                    ResultCodes Error = SetFunc.SettingMonitoring(Settings.IncrimentalReferenceAxisMove);
                    UILog logs = new UILog("OptionForm.SetFunc.SettingMonitoring");
                    if (Error == ResultCodes.Success)
                    {
                        logs.Sure("INCREFAXISMOVE-OFF Result = " + Error);
                    }
                    else
                    {
                        logs.Error("INCREFAXISMOVE-OFF Result = " + Error);
                    }
                }
            }
        }

        private void CorrectAngleBt_Click(object sender, EventArgs e)
        {
            if (_options.RadOffsetEn == false)
            {
                using (SettingFunction SetFunc = new SettingFunction(1))
                {
                    ResultCodes Error = SetFunc.SettingMonitoring(Settings.CorrectAngleEn);
                    UILog logs = new UILog("OptionForm.SetFunc.SettingMonitoring");
                    if (Error == ResultCodes.Success)
                    {
                        logs.Sure("CORRECTANGLE-ON Result = " + Error);
                    }
                    else
                    {
                        logs.Error("CORRECTANGLE-ON Result = " + Error);
                    }
                }
            }
            else
            {
                using (SettingFunction SetFunc = new SettingFunction(0))
                {
                    ResultCodes Error = SetFunc.SettingMonitoring(Settings.CorrectAngleEn);
                    UILog logs = new UILog("OptionForm.SetFunc.SettingMonitoring");
                    if (Error == ResultCodes.Success)
                    {
                        logs.Sure("CORRECTANGLE-OFF Result = " + Error);
                    }
                    else
                    {
                        logs.Error("CORRECTANGLE-OFF Result = " + Error);
                    }
                }
            }
        }

        private void MachineLockBt_Click(object sender, EventArgs e)
        {
            if (_options.MachineLockEn == false)
            {
                using (SettingFunction SetFunc = new SettingFunction(1))
                {
                    ResultCodes Error = SetFunc.SettingMonitoring(Settings.MachineLockEn);
                    UILog logs = new UILog("OptionForm.SetFunc.SettingMonitoring");
                    if (Error == ResultCodes.Success)
                    {
                        logs.Sure("MACHINELOCK-ON Result = " + Error);
                    }
                    else
                    {
                        logs.Error("MACHINELOCK-ON Result = " + Error);
                    }
                }
            }
            else
            {
                using (SettingFunction SetFunc = new SettingFunction(0))
                {
                    ResultCodes Error = SetFunc.SettingMonitoring(Settings.MachineLockEn);
                    UILog logs = new UILog("OptionForm.SetFunc.SettingMonitoring");
                    if (Error == ResultCodes.Success)
                    {
                        logs.Sure("MACHINELOCK-OFF Result = " + Error);
                    }
                    else
                    {
                        logs.Error("MACHINELOCK-OFF Result = " + Error);
                    }
                }
            }
        }

        private void M02Bt_Click(object sender, EventArgs e)
        {
            using (Models.McIf.McDatGcd mc = new Models.McIf.McDatGcd())
            {
                mc.Read();
                mc.M02Dis = !_options.M02En;
                mc.Write();
            }
        }

        private void OptionForm_Load(object sender, EventArgs e)
        {
            if (_options.StartNoEn == false)
            {
                _startNoLabel.Text = ((MDIAUTOForm)(Owner)).Options.StartNo.ToString();
            }
            else
            {
                _startNoLabel.Text = ((MDIAUTOForm)(Owner)).Options.StartNo.ToString();
            }
        }

        private void _startNoEnBt_Click(object sender, EventArgs e)
        {
            if(((MDIAUTOForm)(Owner)).Options.StartNoEn == false)
            {
                ((MDIAUTOForm)(Owner)).Options.StartNoEn = true;
                ((MDIAUTOForm)(Owner)).Options.StartNo = short.Parse(_startNoLabel.Text);
            }
            else
            {
                ((MDIAUTOForm)(Owner)).Options.StartNoEn = false;
                ((MDIAUTOForm)(Owner)).Options.StartNo = 0;
            }
            
        }

        private void _startNoLabel_Click(object sender, EventArgs e)
        {
            popupTenkeyOn();
        }
        /// <summary>
		/// ポップアップテンキーで「OK」ボタンを押した時、入力値をこのクラスに反映
		/// </summary>
		private void popupTenkey_OnNotifyReturnOk()
        {
            string retVal = _popupTenkey._tenkeyValReturn;//ポップアップテンキーで編集された値
            _startNoLabel.Text = retVal;
            if (((MDIAUTOForm)(Owner)).Options.StartNoEn == false)
            {
                ((MDIAUTOForm)(Owner)).Options.StartNo = short.Parse(_startNoLabel.Text);
            }
            else
            {
                ((MDIAUTOForm)(Owner)).Options.StartNo = 0;
            }
        }

        /// <summary>
		/// 記録する加工条件値
		/// </summary>
		object _controlName = "";
        /// <summary>
		/// ポップアップテンキー：表示/非表示
		/// </summary>
		/// <param name="val"></param>
		/// <returns>false=上下ポップアップを表示</returns>
		private bool popupTenkeyOn()
        {
            string changeVal = "";	//編集値
            Decimal lowerLimitDec = 0;//最小値
            Decimal upperLimitDec = 0;//最大値


            //フォーマットタイプ
            NumericTextBox.FormatTypes formatType = NumericTextBox.FormatTypes.Integer5;
            if (_popupTenkey != null)
            {
                _popupTenkey.Close();   //画面を閉じる
                _popupTenkey = null;    //null初期化
            }

            
            changeVal = _startNoLabel.Text;
            lowerLimitDec = (decimal)0;
            upperLimitDec = (decimal)50000;
            formatType = NumericTextBox.FormatTypes.Integer5;

            //ポップアップTenKey：2017-1-12:柏原
            _popupTenkey = new TenKeyDialog(changeVal, formatType, lowerLimitDec, upperLimitDec, true, true);
            _popupTenkey.Location = new Point(420, 80);				    //真ん中：400,200
            _popupTenkey.NotifyReturnOk = popupTenkey_OnNotifyReturnOk; //イベント通知：OK
            _popupTenkey.Text = "StartNo.";	                            //テンキータイトル表示：改行が有れば空白に変換　※ここは項目名が無い
            _popupTenkey.ShowDialog(this);                              //画面を開く
            return true;
        }
        private void NumericFeedForm_Load(object sender, EventArgs e)
        {
           
        }

        private void _CloseBtn_Click(object sender, EventArgs e)
        {
            _threadStop = true;
        }
        public bool GetCloseSygnal()
        {
            return _threadStop;
        }
        /// <summary>
        /// ボタンのLED変更処理
        /// </summary>
        /// <param name="optionStatus">自動モードオプション画面項目のONOFF値</param>
        public void StatusChange(StructureAutoModeOptions optionStatus)
        {
            if (optionStatus.AecEn != AecByLifeBt.GetLed()) AecByLifeBt.SetLed(optionStatus.AecEn);
            if (optionStatus.AecWaitEn != PartitionRoundStopBt.GetLed()) PartitionRoundStopBt.SetLed(optionStatus.AecWaitEn);
            if (optionStatus.BlockSkipEn != BlockSkipBt.GetLed()) BlockSkipBt.SetLed(optionStatus.BlockSkipEn);
            if (optionStatus.BuzzerEn != BuzzerBt.GetLed()) BuzzerBt.SetLed(optionStatus.BuzzerEn);
            if (optionStatus.M02En != M02Bt.GetLed()) M02Bt.SetLed(optionStatus.M02En);
            if (optionStatus.MachineLockEn != MachineLockBt.GetLed()) MachineLockBt.SetLed(optionStatus.MachineLockEn);
            if (optionStatus.OptionalStopEn != OptionalStopBt.GetLed()) OptionalStopBt.SetLed(optionStatus.OptionalStopEn);
            if (optionStatus.RadOffsetEn != CorrectAngleBt.GetLed()) CorrectAngleBt.SetLed(optionStatus.RadOffsetEn);
            if (optionStatus.RefMoveEn != IncRefMoveBt.GetLed()) IncRefMoveBt.SetLed(optionStatus.RefMoveEn);
            if (optionStatus.StartNoEn != _startNoEnBt.GetLed()) _startNoEnBt.SetLed(optionStatus.StartNoEn);
        }
    }
}
