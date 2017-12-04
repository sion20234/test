using ECNC3.Enumeration;
using ECNC3.Models.McIf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECNC3.Views
{
    /// <summary>
    /// 各設定処理用クラス
    /// </summary>
    internal class SettingFunction : IDisposable
    {
        internal SettingFunction()
        {

        }
        internal SettingFunction(int Param)
        {
            SettingParameter = Param;
        }
        internal SettingFunction(Models.StructureAxisCoordinate WorkOrgPos)
        {
            AxisPos = WorkOrgPos;
        }

        internal int SettingParameter { get; set; }
        internal string SettingPrograms { get; set; }
        private Models.StructureAxisCoordinate AxisPos;


      


        /// <summary>
        /// 各設定処理
        /// </summary>
        /// <param name="Target">取得対象の条件名(列挙体メンバ)</param>
        /// <param name="RetValue">設定処理の戻り値</param>
        /// <remarks>設定項目が追加される場合は、Settings列挙体メンバも追加する。</remarks>
        internal Enumeration.ResultCodes SettingMonitoring(Settings Setting)
        {
            //outパラメータの初期値
            Enumeration.ResultCodes RetValue = 0;
            //条件分岐による設定処理
            switch (Setting)
            {
                case Settings.ReturnCmd:
                    using (Models.McIf.McReqSendingBack ReqReturnCmd = new Models.McIf.McReqSendingBack())
                    {
                        RetValue = ReqReturnCmd.Execute();
                    }
                    break;

                case Settings.PulseHandle:
                    RetValue = 0;//falseを実行処理関数に置き換える。
                    break;

                case Settings.XandYaxisInterLock:
                    RetValue = 0;//falseを実行処理関数に置き換える。
                    break;

                case Settings.WaxisUpper:
                    using (Models.McIf.McReqWAxisUpperLimit WUpperSet = new Models.McIf.McReqWAxisUpperLimit())
                    {
                        WUpperSet.SettingValue = SettingParameter;
                        RetValue = WUpperSet.Execute();
                    }
                    break;

                case Settings.ElectroadNumber:
                    using (Models.McIf.McReqElectrodeNumber SetElectNum = new Models.McIf.McReqElectrodeNumber())
                    {
                        SetElectNum.ElectrodeNumber = (short)SettingParameter;
                        RetValue = SetElectNum.Execute();
                    }
                    break;

                case Settings.GuideNumber:
                    using (Models.McIf.McReqGuideNumber SetGuideNum = new Models.McIf.McReqGuideNumber())
                    {
                        SetGuideNum.GuideNumber = (short)SettingParameter;
                        RetValue = SetGuideNum.Execute();
                    }

                    break;

                case Settings.ContactSensing:
                    using (Models.McIf.McReqTouchSensorEnabled TouchSensorSet = new Models.McIf.McReqTouchSensorEnabled())
                    {
                        switch(SettingParameter)
                        {
                            case 0:
                                TouchSensorSet.Enabled = false;
                                break;

                            case 1:
                                TouchSensorSet.Enabled = true;
                                break;
                        }
                        RetValue = TouchSensorSet.Execute();
                    }
                    break;

                case Settings.InitialSet:
                    using (Models.McIf.McReqInitialSetEnabled InitialSet = new Models.McIf.McReqInitialSetEnabled())
                    {
                        switch (SettingParameter)
                        {
                            case 0:
                                InitialSet.Enabled = false;
                                break;

                            case 1:
                                InitialSet.Enabled = true;
                                break;

                        }
                        RetValue = InitialSet.Execute();
                    }
                    break;

                case Settings.Buzzer:
                    using (Models.McIf.McReqBuzzerEnabled BuzzerSet = new Models.McIf.McReqBuzzerEnabled())
                    {
                        switch (SettingParameter)
                        {
                            case 0:
                                BuzzerSet.Enabled = false;
                                break;

                            case 1:
                                BuzzerSet.Enabled = true;
                                break;

                        }
                        RetValue = BuzzerSet.Execute();
                    }
                    break;

                case Settings.ManualOverRide:
                    using (Models.McIf.McReqOverrideChange OverRideChg = new Models.McIf.McReqOverrideChange())
                    {
                        OverRideChg.OverrideMode = Enumeration.OverrideModes.Overall;
                        OverRideChg.SettingValue = (short)SettingParameter;
                        RetValue = OverRideChg.Execute();
                    }
                    RetValue = 0;
                    break;

                case Settings.AutoOverRide:
                    RetValue = 0;
                    break;

                case Settings.WorkOrgPos:
                    using (Models.McIf.McReqWorkPositionChange ReqWorkPos = new Models.McIf.McReqWorkPositionChange())
                    {
                        ReqWorkPos.WorkPosition = AxisPos;
                        RetValue = ReqWorkPos.Execute();
                    }
                    break;

                case Settings.OptionalStop:
                    using (Models.McIf.McReqOptionalStopEnabled ReqOpStopEn = new Models.McIf.McReqOptionalStopEnabled())
                    {
                        if(SettingParameter == 1)
                        {
                            ReqOpStopEn.Enabled = true;
                            RetValue = ReqOpStopEn.Execute();
                        }
                        else
                        {
                            ReqOpStopEn.Enabled = false;
                            RetValue = ReqOpStopEn.Execute();
                        }
                    }
                    break;

                case Settings.IncrimentalReferenceAxisMove:
                    using (Models.McIf.McReqIncrimentalReferenceAxisMoveEnable ReqIncRefMoveEn = new Models.McIf.McReqIncrimentalReferenceAxisMoveEnable())
                    {
                        if(SettingParameter == 1)
                        {
                            ReqIncRefMoveEn.Enabled = true;
                            RetValue = ReqIncRefMoveEn.Execute();
                        }
                        else
                        {
                            ReqIncRefMoveEn.Enabled = false;
                            RetValue = ReqIncRefMoveEn.Execute();
                        }
                    }
                    break;

                case Settings.DryRun:
                    using (Models.McIf.McReqDryRunEnabled ReqDryRunEn = new Models.McIf.McReqDryRunEnabled())
                    {
                        if(SettingParameter == 1)
                        {
                            ReqDryRunEn.Enabled = true;
                            RetValue = ReqDryRunEn.Execute();
                        }
                        else
                        {
                            ReqDryRunEn.Enabled = false;
                            RetValue = ReqDryRunEn.Execute();
                        }
                    }
                    break;

                case Settings.SingleBlock:
                    using (Models.McIf.McReqSingleStepEnabled ReqSingleStepEn = new Models.McIf.McReqSingleStepEnabled())
                    {
                        RetValue = ReqSingleStepEn.Execute();
                    }
                    break;

                case Settings.PartitionRoundStop:
                    using (Models.McIf.McReqPartitionRoundStopEnabled ReqPartRndStEn = new Models.McIf.McReqPartitionRoundStopEnabled())
                    {
                        if(SettingParameter == 1)
                        {
                            ReqPartRndStEn.Enabled = true;
                            RetValue = ReqPartRndStEn.Execute();
                        }
                        else
                        {
                            ReqPartRndStEn.Enabled = false;
                            RetValue = ReqPartRndStEn.Execute();
                        }
                    }
                    break;

                case Settings.AECByLife:
                    using (Models.McIf.McReqAecByLifeEnabled ReqAecEn = new Models.McIf.McReqAecByLifeEnabled())
                    {
                        if(SettingParameter == 1)
                        {
                            ReqAecEn.Enabled = true;
                            RetValue = ReqAecEn.Execute();
                        }
                        else
                        {
                            ReqAecEn.Enabled = false;
                            RetValue = ReqAecEn.Execute();
                        }
                    }
                    break;

                case Settings.CorrectAngleEn:
                    using (Models.McIf.McReqCorrectAngleEnabled ReqCorrAngleEn = new Models.McIf.McReqCorrectAngleEnabled())
                    {
                        if(SettingParameter == 1)
                        {
                            ReqCorrAngleEn.Enabled = true;
                            RetValue = ReqCorrAngleEn.Execute();
                        }
                        else
                        {
                            ReqCorrAngleEn.Enabled = false;
                            RetValue = ReqCorrAngleEn.Execute();
                        }
                    }
                    break;

                case Settings.BlockSkipEn:
                    using (Models.McIf.McReqBlockSkipEnabled ReqBlSkipEn = new Models.McIf.McReqBlockSkipEnabled())
                    {
                        if(SettingParameter == 1)
                        {
                            ReqBlSkipEn.Enabled = true;
                            RetValue = ReqBlSkipEn.Execute();
                        }
                        else
                        {
                            ReqBlSkipEn.Enabled = false;
                            RetValue = ReqBlSkipEn.Execute();
                        }
                    }
                    break;

                case Settings.MachineLockEn:
                    using (Models.McIf.McReqMachineLockEnabled ReqMacLockEn = new Models.McIf.McReqMachineLockEnabled())
                    {
                        if(SettingParameter == 1)
                        {
                            ReqMacLockEn.Moved = true;
                            ReqMacLockEn.Enabled = true;
                            RetValue = ReqMacLockEn.Execute();
                        }
                        else
                        {
                            ReqMacLockEn.Moved = true;
                            ReqMacLockEn.Enabled = false;
                            RetValue = ReqMacLockEn.Execute();
                        }
                    }
                    break;

                case Settings.M02En:

                    break;

                case Settings.Program:
                    
                    
                            break;

                case Settings.EsfClampCrsSpdChg:
                    using (McDatInitialPrm DatIni = new McDatInitialPrm())
                    {
                        DatIni.Read();
                        DatIni.ElctdClumpSpdS = SettingParameter;
                        RetValue = DatIni.Write();
                    }
                        break;
            }
            return RetValue;
        }


        /// <summary>インスタンスの破棄</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);//  ファイナライザによるDispose()呼び出しの抑制。
        }

        private bool _disposed = false;

        /// <summary>インスタンスの破棄</summary>
		/// <param name="disposing">呼び出し元の判別
		///     <list type="bullet" >
		///         <item>true=Dispose()関数からの呼び出し。</item>
		///         <item>false=ファイナライザによる呼び出し。</item>
		///     </list>
		/// </param>
        protected void Dispose(bool disposing)
        {
            try
            {
                if (false == _disposed)
                {
                    if (true == disposing)
                    //  マネージリソースの解放
                    { }
                    //  アンマネージリソースの解放
                }
                _disposed = true;
            }
            finally
            {
                //  基底クラスのDispose()を確実に呼び出す。
            }
        }
    }
}
