﻿///////////////////////////////////////////////////////////////////////////////////////////
//
// (1) システム名称 : ELENIX ECNCⅢ
// (2) ファイル名   : MDIAUTOForm.cs
// (3) 概要         : MDIAUTO画面
// (4) 作成日       : 2015.06.01
// (5) 作成者       : 八野
// (6) 特記事項     : VisualStudio2015 C#
// (7) 更新履歴     :
//
///////////////////////////////////////////////////////////////////////////////////////////


using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ECNC3.Enumeration;
using ECNC3.Models;
using ECNC3.Models.McIf;
using System.IO;
using ECNC3.Views.Popup;
using System.Collections.Generic;
using System.Xml;
using System.Runtime.InteropServices;	//DLL Import

namespace ECNC3.Views
{
    public partial class MDIAUTOForm : ECNC3Form
    {


		#region Constractor
		/// <summary>ポップアップテンキー</summary>
		private TenKeyDialog _popupTenkey = null;//初回インスタンスを作っておく
		public MDIAUTOForm()
        {
            InitializeComponent();
            //メモリ解放処理の追加
            Disposed += MDIAUTOForm_Disposed;
        }
        #endregion

        #region VariableMember
        /// <summary>
        /// オプション画面
        /// </summary>
        OptionForm OpForm = null;
        /// <summary>
        /// ファイル画面
        /// </summary>
        FileForm FileForm = null;//追加：柏原

        /// <summary>
        /// 加工条件画面
        /// </summary>
        ConditionsForm CondForm = null;
        internal bool RegistOnlyProc = false;
        internal bool MdiModeEn = false;
        /// <summary>装置状態監視用イベント</summary>
        internal event StatusMonitoringEventHandler StatusMonitoringEvent;
        /// <summary>加工時間タイマーリセットフラグ</summary>
        internal bool ResetDischargeTimer { get; set; }
        /// <summary>実行プログラム読み取り用</summary>
        internal int ProgramNo = 0;
        FileOperatorMessage fileMessage = new FileOperatorMessage();
        /// <summary>設定結果取得関数定義</summary>
        public delegate void NotifyReturnDelegate();
        /// <summary>終了通知</summary>
        private NotifyReturnDelegate _notifyReturn = null;
        /// <summary>>設定結果取得</summary>
        public NotifyReturnDelegate NotifyReturn
        {
            set { _notifyReturn = value; }
        }
        public StructureAutoModeOptions Options = new StructureAutoModeOptions();
        /// <summary>
        /// 現在の有効桁数
        /// </summary>
        private DigitSelect _digit = DigitSelect.Four;
        #endregion
        #region Monitoring
        public void StatusMonitoring(StatusMonitorEventArgs e)
        {
            if (StatusMonitoringEvent != null)
            {
                StatusMonitoringEvent(e);
            }
            if(e.Items.StartSwBtnOffEdge == true)
            {
                if(MdiModeEn == true)
                {
                }
            }
            //プログラム実行行表示
            if ( (this.IsDisposed == true || this.Disposing == true)   ) return; retInvoke = BeginInvoke((MethodInvoker)delegate
            {
                if ( e.Items.ProgRowNum != 0
                && ProgramTextList.Items.Count != 0
                && ProgramTextList.Items.Count >= e.Items.ProgRowNum)
                {
                    if (e.Items.ProgRowNum != ProgramNo)
                    {
                        ProgramNo = e.Items.ProgRowNum;
                        ProgramTextList.Refresh();
                    }
					if( e.Items.ProgRowNum - 1 < 0 ) {
					} else {
                        
                        //ラベル表示：カレント行：ABS/INC/他
                        if(_readLineProgram != null) LineCodeLabel.Text = _readLineProgram[e.Items.ProgRowNum - 1].content;
                    }
                }
                else
                {
					//ラベル表示
					LineCodeLabel.Text = "";
                }
                if (ProgramStatusLabel.Text.Contains("SUCCESS"))
                {
                    if (ProgramStatusLabel.BackColor != FileUIStyleTable.SelectedLineColor) ProgramStatusLabel.BackColor = FileUIStyleTable.SelectedLineColor;
                }
                else
                {
                    if (ProgramStatusLabel.BackColor != FileUIStyleTable.DefaultBackColor) ProgramStatusLabel.BackColor = FileUIStyleTable.DefaultBackColor;
                }
   			} );
            LogForm_UpdateStatus(e);
            //キーボードボタン表示
            retInvoke = BeginInvoke((MethodInvoker)delegate
            {
                if (ProgramEditBox != null)
                {
                    if (ProgramEditBox.GetKeyboadIsVisible() == false)
                    {
                        if (FileKeybordFormBt.GetBack() == true) FileKeybordFormBt.SetBack(false);
                    }
                }
            }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);

            //OVERRIDE表示処理
            if ( (this.IsDisposed == true || this.Disposing == true)   ) return; retInvoke = BeginInvoke((MethodInvoker)delegate
            {
                if (editProcessCondition1.GetOverride() != e.Items.OverRide)
                {
                    editProcessCondition1.SetOverride(e.Items.OverRide);
                }
            }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);

            //加工条件番号変更時表示処理
            if (int.Parse(PnumTextBox.Text) != e.Items.ProcCondNum)
            {
                RefreshProcessCondition();
            }

            //W軸上限値変更時表示処理
            if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
            {
                if (WAxisUpperVal.Text != PositionToString(e.Items.WAxisUpperLimit))
                {
                    WAxisUpperVal.Text = PositionToString(e.Items.WAxisUpperLimit);
                }
            }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);

            //OPTION設定有効表示
            if ( (this.IsDisposed == true || this.Disposing == true)   ) return; retInvoke = BeginInvoke((MethodInvoker)delegate
            {
                //オプション画面のClose信号→trueなら画面を閉じる
                if (OpForm != null)
                {
                    if (OpForm.GetCloseSygnal() == true)
                    {
                        OpForm.Close();
                        OpForm = null;
                    }
                }
                OptionSetLamp(e);
            }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
            //OPTION画面設定有効表示
            if (OpForm != null && Options != null)
            {
                if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                {
                    if (OpForm != null && Options != null) OpForm.StatusChange(Options);
                }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
            }

            //ドライランON表示処理
            if (e.Items.DryRun == true)
            {
                if (DryRunBt.GetLed() == false)
                {
                    DryRunBt.SetLed(true);
                    UILog logs = new UILog("MDIAUTOForm.SeqFunc.SequenceMonitoring");
                    logs.Error("CHG_DRYRUN_ON");
                }
            }
            else
            {
                if (DryRunBt.GetLed() == true)
                {
                    DryRunBt.SetLed(false);
                    UILog logs = new UILog("MDIAUTOForm.SeqFunc.SequenceMonitoring");
                    logs.Error("CHG_DRYRUN_OFF");
                }
            }
            //シングルステップON表示処理
            if (e.Items.SingleStep == true)
            {
                if (SingleBlockBt.GetLed() == false)
                {
                    SingleBlockBt.SetLed(true);
                    UILog logs = new UILog("MDIAUTOForm.SeqFunc.SequenceMonitoring");
                    logs.Error("CHG_SINGLE_ON");
                }
            }
            else
            {
                if (SingleBlockBt.GetLed() == true)
                {
                    SingleBlockBt.SetLed(false);
                    UILog logs = new UILog("MDIAUTOForm.SeqFunc.SequenceMonitoring");
                    logs.Error("CHG_SINGLE_OFF");
                }
            }
        }
		#endregion

		#region 加工プログラムの表示
		private string _Abs = "Absolute";
		private string _Inc = "Increment";
		/// <summary>
		/// 加工プログラムの表示
		/// </summary>
		/// <param name="_lnkFilePath"></param>
		private void ShowProgram( string _lnkFilePath )
		{
			string path = _lnkFilePath;
			if( false == string.IsNullOrEmpty( path ) ) {
				if( true == System.IO.File.Exists( path ) ) {
					ProgramTextList.Items.Clear();
					FileStream fs = new FileStream( path, FileMode.Open, FileAccess.Read );
					using( TextReader sr = new StreamReader( fs ) ) {
						string Line = sr.ReadLine();
						//string Line = Line = sr.ReadLine(); ;
						int iCount = 0;
						while( Line != null ) {
							if( Line != null ) {
								ProgramTextList.Items.Add( Line );
								iCount++;
							}
							Line = sr.ReadLine();
						}
						//ABS/INC/ユーザー定義表示用テーブル
						_readLineProgram = new StructureReadLineProgramTable[iCount];
						string stringAbsIncEtc = _Inc;
						for( int indexCount = 0 ; indexCount < iCount ; indexCount++ ) {
							_readLineProgram[indexCount].index = indexCount;
							string tmpString = ProgramTextList.Items[indexCount].ToString();
							_readLineProgram[indexCount].lineText = tmpString;

							if( _readLineProgram[indexCount].lineText.IndexOf( "G90" ) > -1 ) {
								_readLineProgram[indexCount].content = _Abs;
								stringAbsIncEtc = _Abs;
							} else if( _readLineProgram[indexCount].lineText.IndexOf( "G91" ) > -1 ) {
								_readLineProgram[indexCount].content = _Inc;
								stringAbsIncEtc = _Inc;
							} else {
								//行に指定コードが無い場合、継承
								_readLineProgram[indexCount].content = stringAbsIncEtc;
							}
						}
					}
				}
			}
		}
		//ABS/INC/ユーザー定義表示用テーブル
		public StructureReadLineProgramTable[] _readLineProgram;
		/// <summary>
		/// 構造体：プログラム１行：インデックス＋内容
		/// </summary>
		public struct StructureReadLineProgramTable
		{
			public int index;       //インデックス
			public string lineText; //行内容
			public string content;  //表示内容
		}

		#region EventHandler
		private void MDIAUTOForm_Disposed(object sender, EventArgs e)
        {
            if (null != CondForm)
            {
                CondForm.Dispose();
                CondForm = null;
            }
            if (null != OpForm)
            {
                OpForm.Close();
                OpForm = null;
            }
        }
        /// <summary>
		/// フォーム　ロード時のイベントハンドラ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MDIAUTOForm_Load(object sender, EventArgs e)
        {
            fileMessage.Read();
            //Plot.csからプロット
            plot1.plotNotify += PlotNotifyClick; //イベント通知

            this.OutLineEnable = false;
            plot1.OutLineEnable = false;
            ResetDischargeTimer = false;

            levelGage1.VCatName = "電圧";
            levelGage1.ACatName = "電流";

            //加工条件編集/表示用コントロールのリフレッシュ処理
            //これがないと加工条件欄の数値の桁が合わなくなる。
            RefreshProcessCondition();

            UILog autoFormInitLog = new UILog(", MANUALForm()");
            //装置状態監視処理に座標取得処理の追加
            StatusMonitoringEvent = axisMonitor1.SetAllMacAxisValue;
            Delegate[] DlList = StatusMonitoringEvent.GetInvocationList();
            string strList = "";
            foreach (Delegate Dl in DlList)
            {
                strList += Dl.Method.Name + "\r\n";
            }
            if (strList == "")
            {
                //装置状態監視処理に関数が無ければログを出力する。
                autoFormInitLog.Error("StatusMonitoringEvent" + " IS NULL" + ", " + "Value= " + strList);
            }
            //有効桁数の取得
            string[] axisName = { "X", "Y", "W", "Z", "A", "B", "C", "I" };
            using (FileSettings fs = new FileSettings())
            {
                //ファイル読み込み
                fs.Read();
                LoadDigit(fs);
            }

#if __V001_INHIBIT__
#else
            foreach ( Control item in this.Controls ) {
				if( ( true == item.Equals( panel1 ) ) || ( true == item.Equals( _btnReset ) ) ) {
					continue;
				}
				item.Enabled = false;
			}
#endif
        }
        /// <summary>
        /// Plot.csからプロット：クリック
        /// </summary>
        private void PlotNotifyClick()
        {
            //ManualFunctionFormsClose();
        }

        /// <summary>
        /// プログラム表示処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgramTextList_DrawItem(object sender, DrawItemEventArgs e)
        {
            //項目が選択されている時は強調表示される
            e.DrawBackground();

            //ListBoxが空のときにListBoxが選択されるとe.Indexが-1になる
            if (e.Index > -1)
            {
                //文字を描画する色の選択
                Brush b = null;
                //選択されていない時
                if ((e.State & DrawItemState.Selected) != DrawItemState.Selected)
                {
                    //プログラム実行行
                    if (e.Index + 1 == ProgramNo)
                    {
                        //文字色を変更する。
                        b = new SolidBrush(FileUIStyleTable.EnabledForeColor);
                        //背景色を変更する。
                        e.Graphics.FillRectangle(new SolidBrush(FileUIStyleTable.EnabledBackColor), e.Bounds);
                    }
                    else
                    {
                        b = new SolidBrush(FileUIStyleTable.DefaultForeColor);
                    }
                }
                else
                {
                    //プログラム実行行
                    if (e.Index + 1 == ProgramNo)
                    {
                        //文字色を変更する。
                        b = new SolidBrush(FileUIStyleTable.EnabledForeColor);
                        //背景色を変更する。
                        e.Graphics.FillRectangle(new SolidBrush(FileUIStyleTable.EnabledBackColor), e.Bounds);
                    }
                    //選択されている時はそのままの前景色を使う
                    b = new SolidBrush(e.ForeColor);
                    e.Graphics.FillRectangle(new SolidBrush(FileUIStyleTable.DefaultBackColor), e.Bounds);
                }
                //描画する文字列の取得
                string txt = ((ListBox)sender).Items[e.Index].ToString();
                //文字列の描画
                e.Graphics.DrawString(txt, e.Font, b, e.Bounds);
                //後始末
                b.Dispose();
            }

            //フォーカスを示す四角形を描画
            e.DrawFocusRectangle();
        }
        private void MDIAUTOForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                RefreshProcessCondition();
            }
            else
            {
                editProcessCondition1.CancelEdit();
                FunctionFormsClose();
            }
        }

        /// <summary>
        /// OPTION有効表示
        /// </summary>
        /// <param name="e"></param>
        internal void OptionSetLamp(StatusMonitorEventArgs e)
        {
            //電極交換
            if (e.Items.AecByLife != AecByLifeLabel.GetLamp())
            {
                AecByLifeLabel.SetLamp(e.Items.AecByLife);
                Options.AecEn = e.Items.AecByLife;
            }
            //AEC1週停止
            if (e.Items.PartitionRoundStop != PartitionRoundStopLabel.GetLamp())
            {
                PartitionRoundStopLabel.SetLamp(e.Items.PartitionRoundStop);
                Options.AecWaitEn = e.Items.PartitionRoundStop;
            }
            //ブザー
            if (e.Items.Buzzer != BuzzerLabel.GetLamp())
            {
                BuzzerLabel.SetLamp(e.Items.Buzzer);
                Options.BuzzerEn = e.Items.Buzzer;
            }
            //O.Stop
            if (e.Items.OptionalStop != OptionalStopLabel.GetLamp())
            {
                OptionalStopLabel.SetLamp(e.Items.OptionalStop);
                Options.OptionalStopEn = e.Items.OptionalStop;
            }
            //B.Skip
            if (e.Items.BlockSkipEn != BlockSkipLabel.GetLamp())
            {
                BlockSkipLabel.SetLamp(e.Items.BlockSkipEn);
                Options.BlockSkipEn = e.Items.BlockSkipEn;
            }
            //G07移動
            if (e.Items.IncrimentalReferenceAxisMove != IncRefMoveLabel.GetLamp())
            {
                IncRefMoveLabel.SetLamp(e.Items.IncrimentalReferenceAxisMove);
                Options.RefMoveEn = e.Items.IncrimentalReferenceAxisMove;
            }
            //角度補正
            if (e.Items.CorrectAngleEn != CorrectAngleLabel.GetLamp())
            {
                CorrectAngleLabel.SetLamp(e.Items.CorrectAngleEn);
                Options.RadOffsetEn = e.Items.CorrectAngleEn;
            }
            //マシンロック
            if (e.Items.MachineLockEn != MachineLockLabel.GetLamp())
            {
                MachineLockLabel.SetLamp(e.Items.MachineLockEn);
                Options.MachineLockEn = e.Items.MachineLockEn;
            }
            //自動電源断
            if (!e.Items.M02Dis != M02Label.GetLamp())
            {
                M02Label.SetLamp(!e.Items.M02Dis);
                Options.M02En = !e.Items.M02Dis;
            }
            if (Options.StartNoEn != StartNoLabel.GetLamp())
            {
                StartNoLabel.SetLamp(Options.StartNoEn);
            }

            if (OpForm != null)
            {
                OpForm._options = Options;
            }
        }
        /// <summary>
        /// オプション　ボタン　クリック時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OptionFormBt_Click(object sender, EventArgs e)
        {
            if (OpForm != null)
            {
                OpForm.Close();
                OpForm = null;
                return;
            }
            else
            {
                OpForm = new OptionForm();
                OpForm.FormClosed += OpForm_Closed;
                OpForm.FormClosing += ChildFormClosing;
                OpForm.Show(this);
                OptionBt.SetBack(true);
            }
        }
        private void OpForm_Closed(object sender, FormClosedEventArgs e)
        {
            OptionBt.SetBack(false);
        }
        private void ChildFormClosing(object sender, FormClosingEventArgs e)
        {
            (sender as ECNC3Form).FormClosing -= ChildFormClosing;
        }
        /// <summary>リセットボタン押下</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnReset_Click(object sender, EventArgs e)
        {
            //リセットを実行します。よろしいですか？
            if (DialogResult.No == _MessageShow(MessageBoxIcon.Question, 5500, _btnReset)) return;
            ResultCodes ret = ResultCodes.Success;
            //	汚水槽エラーかつSPXリセット無効設定のときはリセットを実行しない。
            while (true)
            {
                using (McDatStatus mc = new McDatStatus())
                {
                    ret = mc.Read();
                    if (ResultCodes.Success != ret) break;
                    if (true == mc.Status.EtherCatErrorHoldingTankLiquidEmpty)
                    {
                        using (FileSettings fs = new FileSettings())
                        {
                            ret = fs.Read();
                            if (ResultCodes.Success != ret) break;
                            if (true == fs.AttrBool("Root/Motions/Spx/Reset", "enbl")) break;  //	リセットしない
                        }
                    }
                    if (true == mc.Status.ProgramRunning)
                            if (DialogResult.No == _MessageShow(MessageBoxIcon.Question, 527, _btnReset)) break;
                }
                using (McReqReset mc = new McReqReset())
                {
                    ret = mc.Execute();
                    if (ResultCodes.Success != ret) break;
                }
                return;
            }
            if (ResultCodes.Success != ret) _MessageShow(MessageBoxIcon.Error, 110, _btnReset);
        }
        private void DryRunBt_Click(object sender, EventArgs e)
        {
            if ((sender as ButtonEx).GetLed() == false)
            {
                using (SettingFunction SetFunc = new SettingFunction(1))
                {
                    ResultCodes Error = SetFunc.SettingMonitoring(Settings.DryRun);
                    //ログ処理
                    UILog logs = new UILog("MDIAUTOForm.SetFunc.SettingMonitoring");
                    if (Error == ResultCodes.Success)
                    {
                        logs.Sure("DRYRUN-ON, " + "Result = " + Error);
                    }
                    else
                    {
                        logs.Error("DRYRUN-ON, " + "Result = " + Error);
                    }
                }
            }
            else
            {
                using (SettingFunction SetFunc = new SettingFunction(0))
                {
                    ResultCodes Error = SetFunc.SettingMonitoring(Settings.DryRun);
                    //ログ処理
                    UILog logs = new UILog("MDIAUTOForm.SetFunc.SettingMonitoring");
                    if (Error == ResultCodes.Success)
                    {
                        logs.Sure("DRYRUN-OFF, " + "Result = " + Error);
                    }
                    else
                    {
                        logs.Error("DRYRUN-OFF, " + "Result = " + Error);
                    }
                }
            }
        }
        private void SingleBlockBt_Click(object sender, EventArgs e)
        {
            if ((sender as ButtonEx).GetLed() == false)
            {
                using (SettingFunction SetFunc = new SettingFunction(1))
                {
                    ResultCodes Error = SetFunc.SettingMonitoring(Settings.SingleBlock);
                    //ログ処理
                    UILog logs = new UILog("MDIAUTOForm.SetFunc.SettingMonitoring");
                    if (Error == ResultCodes.Success)
                    {
                        logs.Sure("SINGLEBLOCK-ON, " + "Result = " + Error);
                    }
                    else
                    {
                        logs.Error("SINGLEBLOCK-ON, " + "Result = " + Error);
                    }
                }
            }
            else
            {
                using (SettingFunction SetFunc = new SettingFunction(0))
                {
                    ResultCodes Error = SetFunc.SettingMonitoring(Settings.SingleBlock);
                    //ログ処理
                    UILog logs = new UILog("MDIAUTOForm.SetFunc.SettingMonitoring");
                    if (Error == ResultCodes.Success)
                    {
                        logs.Sure("SINGLEBLOCK-OFF, " + "Result = " + Error);
                    }
                    else
                    {
                        logs.Error("SINGLEBLOCK-OFF, " + "Result = " + Error);
                    }
                }
            }
        }
        private FileForm _fileForm;
        private void FileKeybordFormBt_Click(object sender, EventArgs e)
        {
            if (FileKeybordFormBt.Text == "ファイル")
            {//モーダレス化
                _fileForm = FileForm.ShowSubFormMod(FileFormMode.OpenProgram, System.IO.Path.GetFullPath(@"Program"));
                _fileForm.NotifyReturn = FileForm_OnNotifyReturnOk;//OK時の通知セット
                //string _programPath = FileForm.ShowSubForm(this,FileFormMode.OpenProgram, System.IO.Path.GetFullPath(@"Program"));
                // ReceivedProgram(_programPath);
            }
            else
            {
                if (FileKeybordFormBt.GetBack() == true)
                {
                    ProgramEditBox.KeybordVisible(false);
                    FileKeybordFormBt.SetBack(false);
                }
                else
                {
                    ProgramEditBox.KeybordVisible(true);
                    FileKeybordFormBt.SetBack(true);
                }
            }
        }
        /// <summary>
        /// FileFormから通知
        /// </summary>
        private void FileForm_OnNotifyReturnOk()
        {
            if (_fileForm != null)
            {
                ReceivedProgram(_fileForm._returnPath);
                _fileForm.Close();//ここで閉じる
                _fileForm = null;
            }
       }
        #endregion

        /// <summary>
        /// ポップアップテンキー：表示/非表示
        /// </summary>
        /// <param name="val"></param>
        /// <returns>false=上下ポップアップを表示</returns>
        private bool popupTenkeyOn( object val, string title )
		{
            //フォーマットタイプ
            NumericTextBox.FormatTypes formatType = NumericTextBox.FormatTypes.Integer1;
            if (_popupTenkey != null)
            {
                _popupTenkey.Close();   //画面を閉じる
                _popupTenkey = null;    //null初期化
                return false;
            }
            string changeVal = val.ToString();  //編集値
            Decimal lowerLimitDec = (decimal)-9999.999;//最小値
            Decimal upperLimitDec = (decimal)9999.999;//最大値
            formatType = NumericTextBox.FormatTypes.SignDecimalUpper3Lower3;

            //ポップアップTenKey：2017-1-12:柏原
            _popupTenkey = new TenKeyDialog(changeVal, formatType, lowerLimitDec, upperLimitDec, true, true, true);
            _popupTenkey.NotifyReturnOk = popupTenkey_OnNotifyReturnOk; //イベント通知：OK
            _popupTenkey.FormClosed += popupTenkey_Closed;

            _popupTenkey.Text = title;                                  //テンキータイトル表示
            _popupTenkey.ShowDialog(this);                            //画面を開く
            return true;
        }
		private string _int32Val="";
        private void popupTenkey_Closed(object sender, FormClosedEventArgs e)
        {
            if (_popupTenkey != null)
            {
                _popupTenkey = null;
            }
        }
        /// <summary>
		/// ポップアップテンキーで「OK」ボタンを押した時、入力値をこのクラスに反映
		/// </summary>
		private void popupTenkey_OnNotifyReturnOk()
		{
			_int32Val = _popupTenkey._tenkeyValReturn;//ポップアップテンキーで編集された値

		}
		/// <summary>
		/// フォーム：閉じる
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MDIAUTOForm_FormClosed( object sender, FormClosedEventArgs e )
		{
			//ポップアップテンキー
			if( null != _popupTenkey ) {
				_popupTenkey.Close();
				_popupTenkey = null;
			}
		}
        SelectCommandsDialogSimple selDlg = null;
        int WLim = 0;
        private void WAxisUpperSetBt_Click(object sender, EventArgs e)
        {
            if (selDlg != null)
            {
                if (selDlg.IsDisposed == false) selDlg.Dispose();
                selDlg = null;
            }
            else
            {
                selDlg = new SelectCommandsDialogSimple("現在位置設定", "クリア", "上限値入力", sender as ButtonEx);
                WLim = axisMonitor1.GetWMacVal();
                selDlg.FormClosing += WaxisUpperSetSelDlg_Closing;
                selDlg.Show(this);
            }
        }
        private void WaxisUpperSetSelDlg_Closing(object sender, FormClosingEventArgs e)
        {
            if (selDlg != null)
                switch (selDlg.retMessage)
                {
                    case ReturnMessage.ExecuteA1:
                        switch (_digit)
                        {
                            case DigitSelect.Three: WLim /= 1000; break;//下3桁
                            case DigitSelect.Four: WLim /= 10000; break;//下4桁 
                        }
                        break;

                    case ReturnMessage.ExecuteA2:
                        WLim = 0;
                        break;

                    case ReturnMessage.ExecuteA3:
                        //string tempStr = EditTextDialog.ShowSubForm("W軸上限値を入力してください。");
                        //ポップアップTenKey
                        popupTenkeyOn((object)WAxisUpperVal.Text, "W軸上限値");
                        string tempStr = _int32Val;//ポップアップテンキーで編集された値
                        if (tempStr == "")
                        {
                            return;
                        }
                        decimal decWLim = decimal.Parse(tempStr) * 1000;//整数化;
                        //小数以下を取り除き、整数に変換
                        string stringWLim = decWLim.ToString();
                        int dotIndex = stringWLim.IndexOf(".");
                        stringWLim = stringWLim.Substring(0, dotIndex);
                        //NCへデータセット
                        WLim = int.Parse(stringWLim);
                        break;

                    case ReturnMessage.Cancel:
                        return;
                }
            if (selDlg.retMessage == ReturnMessage.Cancel)
            {
                return;
            }

            using (SettingFunction SetFunc = new SettingFunction(WLim))
            {
                ResultCodes Error = SetFunc.SettingMonitoring(Settings.WaxisUpper);
                //ログ処理
                UILog logs = new UILog("MANUALForm.SetFunc.SettingMonitoring");
                if (Error == ResultCodes.Success)
                {
                    logs.Sure("WAXISUPPER-SET " + "Value = " + WLim + ", Result = " + Error);
                }
                else
                {
                    logs.Error("WAXISUPPER-SET " + "Value = " + WLim + ", Result = " + Error);
                }
            }
        }
        /// <summary>
        /// プログラム運転時間タイマのリセット
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _programRunTimeBt_Click(object sender, EventArgs e)
        {
			using( MessageDialog msgDia = new MessageDialog(sender as ButtonEx) ) {
                //_processingTimeBt.SetSelected(true, Color.SkyBlue);
                if ( !msgDia.Question( 5503, this ) ) {
                    //プログラム運転時間リセット確認" 
                    return;
				}
			}
			TimerView.ProgramProcessingTimeReset(McTaskModes.Auto);
            //初期値セット
            MachineTimerTextBox.Text = "0H 0M 0S";
            //_processingTimeBt.SetSelected(false);
        }
		/// <summary>
		/// 加工時間タイマのリセット
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _processingTimeBt_Click( object sender, EventArgs e )
		{
		using( MessageDialog msgDia = new MessageDialog((sender as ButtonEx)) ) {
                if ( !msgDia.Question( 5504, this ) ) {
                    //加工時間リセット確認" 
                    return;
				}
			}
			TimerView.DischargeTimeReset(McTaskModes.Auto);
			//初期値セット
			DischargeTimerTextBox.Text = "0H 0M 0S";
		}
        private void _oneProcessingTimeBt_Click(object sender, EventArgs e)
        {
            TimerView.OneProcessingTimeReset(McTaskModes.Auto);
            //初期値セット
            ProcTimerTextBox.Text = "0.0S";
        }
        #endregion
        #region Tools
        /// <summary>
        /// 有効軸数の取得
        /// </summary>
        /// <param name="fs"> ファイルデータ</param>
        /// <returns>有効軸数</returns>
        private int LoadAxisCount(FileSettings fs)
        {
            int ret = 0;
            {   //ファイル読み込み
                ret = fs.AttrValue("Root/AxisInfomation/EnableAxis", "count");//0=上下のみ、1=上下+テンキー、2=テンキーのみ
            }
            return ret;
        }
        /// <summary>
        /// 有効桁数の取得
        /// </summary>
        /// <param name="fs">ファイルデータ</param>
        private void LoadDigit(FileSettings fs)
        {
            int tempDigit = 4;
            tempDigit = fs.AttrValue("Root/AxisInfomation/Position", "digit");
            switch (tempDigit)
            {
                case 3: _digit = DigitSelect.Three; break;
                case 4: _digit = DigitSelect.Four; break;
            }
        }
        /// <summary>
        /// 整数の座標値を有効桁数によって変化する文字列に変換する。
        /// </summary>
        /// <param name="position">座標値</param>
        /// <returns>座標値の文字列</returns>
        private string PositionToString(int position)
        {
            switch (_digit)
            {
                case DigitSelect.Three: return position.ToString("###0'.'000");
                case DigitSelect.Four: return (position * 10).ToString("###0'.'0000");
                default: return "0.000";
            }
        }
        /// <summary>
        /// MDI/AUTOモード別フラグ設定
        /// </summary>
        /// <param name="Mode"></param>
        /// <remarks>MDI/AUTOモードのコントロール設定を変える。</remarks>
        internal void ModeChg(MAINFormCategory mode)
        {
            if ((this.IsDisposed == true || this.Disposing == true)) return; retInvoke = BeginInvoke((MethodInvoker)delegate
            {
                switch (mode)
                {
                    case MAINFormCategory.MDI:
                            FileKeybordFormBt.Text = "キーボード";
                            ProgramEditBox.Visible = true;
                            ProgramTextList.Visible = false;
                            MdiModeEn = true;
                            _HelpBtn.Enabled = true;//ヘルプ使用可
                        break;

                    case MAINFormCategory.Auto:
                            FileKeybordFormBt.Text = "ファイル";
                            ProgramEditBox.Visible = false;
                            ProgramTextList.Visible = true;
                            MdiModeEn = false;
                            _HelpBtn.Enabled = false;//ヘルプ使用不可
                            if (Help != null)
                            {
                                //ヘルプ
                                Help.Close();
                                Help = null;
                            }
                            break;

                }
            }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);            
        }
        /// <summary>カレントの加工条件表示の更新</summary>
        private void RefreshProcessCondition()
        {
            UILog editPCondInitLog = new UILog(", MDIAUTOForm()");
            using (McDatProcessCondition mc = new McDatProcessCondition())
            {
                ResultCodes ret = mc.Read();
                if (ResultCodes.Success != ret)
                {
                    //加工条件を取得した時のエラーメッセージ
                    editPCondInitLog.Error("editProcessCondition1.private void RefreshProcessCondition(), PCondition UpdateData Error" + ", " + "Result= " + ret);
                    return;
                }
                //加工条件番号の取得、表示
                if( (this.IsDisposed == true || this.Disposing == true)   ) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                {
                    PnumTextBox.Text = $"{mc.PNo:d3}";
                    //加工条件の表示更新    
                    editProcessCondition1.UpdateData(mc.PNo);
                }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);                
            }
        }
        /// <summary>入力コントロールの初期化</summary>
        /// <param name="selSw">入力遷移させるボタンコントロールの参照</param>
        /// <param name="textBox">入力コントロールの参照</param>
        /// <param name="format">入力コントロールの書式</param>
        private void InitInput(ButtonEx selSw, NumericTextBox textBox, NumericTextBox.FormatTypes format)
        {
            textBox.FormatType = format;
            textBox.ReadOnly = true;
            selSw.EditBox = textBox;
        }
        /// <summary>
        /// 折れ線グラフのフォーム外からのSetアクセサ
        /// </summary>
        /// <param name="InfoName"></param>
        /// <param name="iParam"></param>
        public void SetPlotInfo(string InfoName, double dParam)
        {
            switch (InfoName)
            {
                case "Zaxis":
                    if( (this.IsDisposed == true || this.Disposing == true)   ) return; retInvoke = BeginInvoke((MethodInvoker)delegate
                    {
                        plot1.SetPlotValue(dParam);
                    }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                    break;

            }
        }
        /// <summary>
        /// 棒グラフのフォーム外からのSetアクセサ
        /// </summary>
        /// <param name="Avalue"></param>
        public void SetLevelGageValue(double Avalue, double Vvalue)
        {
            if( (this.IsDisposed == true || this.Disposing == true)   ) return; retInvoke = BeginInvoke((MethodInvoker)delegate
            {
                if (Avalue != levelGage1.AValue)
                {
                    levelGage1.AValue = Avalue;

                }
                if (Vvalue != levelGage1.VValue)
                {
                    levelGage1.VValue = Vvalue;
                }
                levelGage1.setDummyData();
            }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
        }
        /// <summary>
        /// タイマー表示のSetアクセサ
        /// </summary>
        /// <param name="timer"></param>
        /// <param name="value"></param>
        internal void SetTimer(TimerCategory timer, string value)
        {
            switch (timer)
            {
                case TimerCategory.ProgramTime:
                    MachineTimerTextBox.Text = value;
                    break;

                case TimerCategory.Discharge:
                    DischargeTimerTextBox.Text = value;
                    break;

                case TimerCategory.OneProcessingTime:
                    ProcTimerTextBox.Text = value;
                    break;
            }
        }
        /// <summary>
		/// プログラムファイルをプログラム表示する。
		/// </summary>
		/// <param name="ProgramList"></param>
		public void SetProgramTexts(string path)
        {
            ProgramTextList.Items.Clear();
            ReceivedProgram(path);
        }
        private List<string> UpdateGMCodeDisable()
        {
            List<string> retList = new List<string>();
            using (FileSettings fs = new FileSettings("GMCodeDisable.xml"))
            {
                byte[] gcdValue = new byte[100];
                byte[] mcdValue = new byte[100];

                //ファイル読込
                fs.Read();
                XmlNodeList gcdList = fs.GetList("Prog/GCodDsbl/Item");
                XmlNodeList mcdList = fs.GetList("Prog/MCodDsbl/Item");
                foreach (XmlNode gcditem in gcdList)
                {
                    gcdValue[int.Parse(gcditem.Attributes[0].Value)] = byte.Parse(gcditem.Attributes[1].Value);
                }
                foreach (XmlNode mcditem in mcdList)
                {
                    mcdValue[int.Parse(mcditem.Attributes[0].Value)] = byte.Parse(mcditem.Attributes[1].Value);
                }
                //リスト表示
                int totalCt = 0;
                for (int index = 0; index < gcdValue.Length; index++, totalCt++)
                {
                    if (gcdValue[index] == 1) retList.Add("G" + index.ToString("00"));
                }
                for (int index = 0; index < gcdValue.Length; index++, totalCt++)
                {
                    if (mcdValue[index] == 1) retList.Add("M" + index.ToString("00")); ;
                }

                //解放処理
                for (int ct = 0; ct < 100; ct++)
                {
                    gcdValue[ct] = 0;
                    mcdValue[ct] = 0;
                }
                gcdValue = null;
                mcdValue = null;
            }
            return retList;
        }
        public string GetReadPath()
        {
            return _readPath;
        }
        string _readPath = "";
        /// <summary>
        /// ファイルを開いてプログラム表示
        /// </summary>
        /// <param name="path"></param>
        private void ReceivedProgram(string path)
        {
            ProgramStatusLabel.Text = "Loading....Wait.....";
            ResultCodes ret = ResultCodes.Success;
            string errCode = "";
            int errIndex = -1;
            //無効G/Mコードチェック
            List<string> GMCodeList = UpdateGMCodeDisable();
            List<string> progList = ProgramEditBox.TextToArray().ToList();
            int progListIndex = 0;
            foreach (string rowText in progList)
            {
                foreach (string chkCode in GMCodeList)
                {
                    if (rowText.Contains(chkCode))
                    {
                        //無効GMコード検知
                        errCode = chkCode;
                        //行番号取得
                        errIndex = progListIndex;
                        break;
                    }
                }
                if (errCode != "") break;
                progListIndex++;
            }

            ProgramStatusLabel.Text = "ProgramEdit  info: " + fileMessage.Find(4109).Text + "[" + errCode + "]"
                                    + errIndex.ToString() + "行目";

            if (errCode == "")
            {
                //MC内プログラム削除
                using (McReqProgramDelete progDel = new McReqProgramDelete())
                {
                    progDel.Execute();
                }
                //テクノコンパイル
                using (McDatProgram mc = new McDatProgram())
                {
                    mc.ProgramFilePath = path;
                    mc.BlockNumber = 0;
                    _readPath = path;
                    ret = mc.Write();
                    //	実行結果の表示
                    ProgramStatusLabel.Text = "ProgramEdit  info: " + $"{ret}";
                    if (ResultCodes.Success != ret)
                    {
                        ProgramStatusLabel.Text = fileMessage.Find(4104).Text + "[" + errCode + "]"
                                        + errIndex.ToString() + "行目";
                    }
                    else
                    {
                        ProgramStatusLabel.Text = "ProgramEdit  info: " + "SUCCESS";
                    }
                }
            }
            ProgramStatusLabel.Text = ProgramStatusLabel.Text.Replace("\r\n", "");
            ShowProgram(path);
        }

        /// <summary>
		/// 加工プログラム：保存
		/// </summary>
		/// <param name="_lnkFilePath"></param>
		public void SaveProgram(string _lnkFilePath)
        {
            string path = _lnkFilePath;
            if (false == string.IsNullOrEmpty(path))
            {
                try
                {
                    if (true == System.IO.File.Exists(path))
                    {
                        using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                        {
                            using (TextWriter sw = new StreamWriter(fs))
                            {
                                foreach (string Row in ProgramEditBox.TextToArray())
                                {
                                    sw.WriteLine(Row);
                                }
                            }
                        }
                    }
                    else
                    {
                        using (FileStream fs = new FileStream(path, FileMode.CreateNew, FileAccess.Write))
                        {
                            using (TextWriter sw = new StreamWriter(fs))
                            {
                                foreach (string Row in ProgramEditBox.TextToArray())
                                {
                                    sw.WriteLine(Row);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ECNC3Exception.FileIOFilter(ex);
                }
            }
        }
        /// <summary>
        /// 編集フォームから表示プログラムパスを取得するGetアクセサ
        /// </summary>
        /// <returns></returns>
        public void ResistProgram()
        {
            string Path = FilePathInfo.ProgramData + DateTime.Today.ToString("yyyyMMdd") + "MDI" + ".PGM";
            if (ProgramEditBox.TextToArray()[0] == "") return;
            SaveProgram(Path);
            ReceivedProgram(Path);
        }
        /// <summary>
        /// 座標の表示桁数が6桁以上の場合、6桁に合わせる。
        /// </summary>
        /// <param name="AxisVal">座標値</param>
        /// <returns></returns>
        private int DigitAlign(string AxisVal)
        {
            int ret = 0;

            //座標の表示桁数を調べる
            switch ((AxisVal.Count()
                 - (AxisVal.IndexOf(".") + 1)))
            {
                //下3桁の場合
                case 3:
                    ret = int.Parse(AxisVal.Replace(".", ""));
                    break;

                //下4桁の場合
                case 4:
                    string AlignVal = AxisVal.Remove(AxisVal.IndexOf(".") + 4).Replace(".", "");

                    if ((AlignVal.Count() <= 6)
                     || (AlignVal.StartsWith("-") && AlignVal.Count() == 7))
                    {
                        ret = int.Parse(AlignVal);
                    }
                    else
                    {
                        while (AlignVal.Count() > 6)
                        {
                            AlignVal = AlignVal.Remove(0, 1);
                            if (AlignVal.Count() == 6
                                || (AlignVal.StartsWith("-") && AlignVal.Count() == 7))
                            {
                                break;
                            }
                        }
                        ret = int.Parse(AlignVal);
                    }
                    break;
            }
            return ret;
        }
        #endregion

        /// <summary>加工条件　ボタン　クリック時のイベントハンドラ</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConditionsFormOpenBt_Click(object sender, EventArgs e)
        {
            if (null == CondForm)
            {
                CondForm = new ConditionsForm(PnumTextBox.Text);
                CondForm.NotifyReturn = ConditionsFormOpenBt_ClickCallBack;
            }
            //親子関係設定
            CondForm.Show(this);
            UILog logs = new UILog("MDIAUTOForm.ConditionsFormOpenBt_Click");
            logs.Operate("Move_ConditionsForm" + "Pnum = " + PnumTextBox.Text);
        }
        /// <summary>加工条件ボタン コールバック</summary>
        private void ConditionsFormOpenBt_ClickCallBack()
        {
            using (McDatStatus mcSta = new McDatStatus())
            {
                mcSta.Read();
                if (mcSta.Status.ProgramRunning == true)
                {
                    using (McDatProcessConditionTable datPcondTbl = new McDatProcessConditionTable())
                    using (FileProcessCondition filePcond = new FileProcessCondition())
                    using (McDatProcessCondition datPcond = new McDatProcessCondition())
                    {
                        datPcondTbl.Read();
                        datPcond.Read();
                        foreach (StructureProcessConditionItem item in datPcondTbl.Items)
                        {
                            if (item.Number != datPcond.PNo) continue;
                            if (filePcond.Read(item.Number).Equals(item)) break;
                            ReturnMessage retMessage = SelectCommandsDialog.ShowSubForm( this,"加工条件の適用範囲選択", new string[] { "全適用", "一穴適用", "", "" }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
                            switch (retMessage)
                            {
                                case ReturnMessage.ExecuteA1:
                                    for (int ct = 0; ct < datPcondTbl.Items.Count; ct++)
                                    {
                                        if (datPcondTbl.Items[ct].Number == datPcond.PNo)
                                        {
                                            filePcond.Write(datPcondTbl.Items[ct]);
                                        }
                                    }
                                    break;

                                case ReturnMessage.ExecuteA2: break;
                            }
                            RegistOnlyProc = true;
                        }
                    }
                }
            }
            CondForm.Close();
			CondForm = null;
			RefreshProcessCondition();
        }
        private void PcondWriteAndRegist()
        {
            editProcessCondition1.PerpetuatingRegister();
            editProcessCondition1.Download();
            editProcessCondition1.Refresh();
        }

        #region<加工ログ表示切替>
        /// <summary>
        /// 加工ログ表示番号
        /// </summary>
        int _plotDisplyMode = 0;//0＝通常(プロット小)、１＝プロット右半分、２＝ログ表示
        public void LogForm_UpdateStatus(StatusMonitorEventArgs e)
        {
            if ((this.IsDisposed == true || this.Disposing == true)) return;
            retInvoke = BeginInvoke((MethodInvoker)delegate
            {
                plot1.LogStatusView(e, _digit);
            }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
        }

        /// <summary>
        /// 画面切り替え時、画面を非表示/閉じる処理
        /// </summary>
        public void FunctionFormsClose()
        {
            //オプション画面の非表示
            if (OpForm != null)
            {
                OpForm.Close();
                OpForm = null;
            }
            //ログ表示画面の非表示
            if (plot1.ExpandEn == true)
            {
                plot1.ExpandEn = false;
            }
            //テンキー非表示
            if (_popupTenkey != null)
            {
                _popupTenkey.Close();
                _popupTenkey = null;
            }
            //ヘルプ
            if (Help != null)
            {
                Help.Close();
                Help = null;
            }
            // 加工条件編集欄のテンキー非表示
            editProcessCondition1.CancelEdit();
            //加工条件画面非表示
            if (CondForm != null)
            {
                CondForm._btnReturn_Click(null, null);
            }
            //ファイルフォーム
            if (_fileForm != null)
            {
                _fileForm.Close();
                _fileForm = null;
            }
        }
        #endregion
        #region<ヘルプ>
        /// <summary>ヘルプ表示画面</summary>
        HelpForm Help = null;
        /// <summary>
        /// ヘルプ：ボタンをクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtunEx_Help_Click(object sender, EventArgs e)
        {
            if(Help == null)Help = new HelpForm();
            Help.FormClosing += ChildFormClosing;
            Help.FormClosed += HelpFormClosed;
            Help.Show(this);
        }
        private void HelpFormClosed(object sender, FormClosedEventArgs e)
        {
            //sender = null;
            if (Help != null)
            {
                Help.FormClosing -= ChildFormClosing;
                Help.FormClosed -= HelpFormClosed;
                Help = null;
            }
        }

        #endregion

        private void MDIAUTOForm_Activated(object sender, EventArgs e)
        {
            editProcessCondition1.Sfip_Init();
            editProcessCondition1.UpdateData();
        }
    }
}