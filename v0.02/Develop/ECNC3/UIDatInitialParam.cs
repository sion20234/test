using ECNC3.Enumeration;
using ECNC3.Models.McIf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Rt64ecdata;


namespace ECNC3.Views
{
    public class UIDatInitialParam : McDatInitialPrm, IEcnc3McDatReadWrite
    {
        public UIDatInitialParam()
        {
        }
        /// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
        public List<string> InitialParamDataList = new List<string>();
        //INITIALPRM UIdata = new INITIALPRM();



        public new ResultCodes Read()
        {
            InitialParamDataList.Clear();
            InitialParamDataList.TrimExcess();
            ResultCodes ret = ResultCodes.NotFound;
            ret = base.Read();
            //UIdata = _data;
            InitialParamDataList.Add(_data.ElctdExchPos[0].ToString());
            InitialParamDataList.Add(_data.ElctdExchPos[1].ToString());
            InitialParamDataList.Add(_data.ElctdExchPos[2].ToString());
            InitialParamDataList.Add(_data.ElctdExchPos[3].ToString());
            InitialParamDataList.Add(_data.ElctdExchPos[4].ToString());
            InitialParamDataList.Add(_data.ElctdExchPos[5].ToString());
            InitialParamDataList.Add(_data.ElctdExchPos[6].ToString());
            InitialParamDataList.Add(_data.ElctdExchPos[7].ToString());
            InitialParamDataList.Add(_data.ElctdExchPos[8].ToString());
            InitialParamDataList.Add(_data.ElctdExchSpdW.ToString());
            InitialParamDataList.Add(_data.ElctdExchSpdW1.ToString());
            InitialParamDataList.Add(_data.ElctdExchOfsW1.ToString());
            InitialParamDataList.Add(_data.ElctdExchOfsW2.ToString());
            InitialParamDataList.Add(_data.ElctdChkSpdZ1[0].ToString());
            InitialParamDataList.Add(_data.ElctdChkSpdZ1[1].ToString());
            InitialParamDataList.Add(_data.ElctdChkSpdZ1[2].ToString());
            InitialParamDataList.Add(_data.ElctdChkSpdZ1[3].ToString());
            InitialParamDataList.Add(_data.ElctdChkSpdZ1[4].ToString());
            InitialParamDataList.Add(_data.ElctdChkSpdZ1[5].ToString());
            InitialParamDataList.Add(_data.ElctdChkSpdZ2[0].ToString());
            InitialParamDataList.Add(_data.ElctdChkSpdZ2[1].ToString());
            InitialParamDataList.Add(_data.ElctdChkSpdZ2[2].ToString());
            InitialParamDataList.Add(_data.ElctdChkSpdZ2[3].ToString());
            InitialParamDataList.Add(_data.ElctdChkSpdZ2[4].ToString());
            InitialParamDataList.Add(_data.ElctdChkSpdZ2[5].ToString());
            InitialParamDataList.Add(_data.ElctdChkSpdS2[0].ToString());
            InitialParamDataList.Add(_data.ElctdChkSpdS2[1].ToString());
            InitialParamDataList.Add(_data.ElctdChkSpdS2[2].ToString());
            InitialParamDataList.Add(_data.ElctdChkSpdS2[3].ToString());
            InitialParamDataList.Add(_data.ElctdChkSpdS2[4].ToString());
            InitialParamDataList.Add(_data.ElctdChkSpdS2[5].ToString());
            InitialParamDataList.Add(_data.ElctdChkOfsZ[0].ToString());
            InitialParamDataList.Add(_data.ElctdChkOfsZ[1].ToString());
            InitialParamDataList.Add(_data.ElctdChkOfsZ[2].ToString());
            InitialParamDataList.Add(_data.ElctdChkOfsZ[3].ToString());
            InitialParamDataList.Add(_data.ElctdChkOfsZ[4].ToString());
            InitialParamDataList.Add(_data.ElctdChkOfsZ[5].ToString());
            InitialParamDataList.Add(_data.ElctdDeplUpZ.ToString());
            InitialParamDataList.Add(_data.ElctdNum.ToString());
            InitialParamDataList.Add(_data.GuideNum.ToString());
            InitialParamDataList.Add(_data.GuideExchPosS[0].ToString());
            InitialParamDataList.Add(_data.GuideExchPosS[1].ToString());
            InitialParamDataList.Add(_data.GuideExchPosS[2].ToString());
            InitialParamDataList.Add(_data.GuideExchPosS[3].ToString());
            InitialParamDataList.Add(_data.GuideExchPosS[4].ToString());
            InitialParamDataList.Add(_data.GuideExchPosS[5].ToString());
            InitialParamDataList.Add(_data.GuideExchPosS[6].ToString());
            InitialParamDataList.Add(_data.GuideExchPosS[7].ToString());
            InitialParamDataList.Add(_data.GuideExchPosS[8].ToString());
            InitialParamDataList.Add(_data.GuideExchPosE[0].ToString());
            InitialParamDataList.Add(_data.GuideExchPosE[1].ToString());
            InitialParamDataList.Add(_data.GuideExchPosE[2].ToString());
            InitialParamDataList.Add(_data.GuideExchPosE[3].ToString());
            InitialParamDataList.Add(_data.GuideExchPosE[4].ToString());
            InitialParamDataList.Add(_data.GuideExchPosE[5].ToString());
            InitialParamDataList.Add(_data.GuideExchPosE[6].ToString());
            InitialParamDataList.Add(_data.GuideExchPosE[7].ToString());
            InitialParamDataList.Add(_data.GuideExchPosE[8].ToString());
            InitialParamDataList.Add(_data.GuideChkPos[0].ToString());
            InitialParamDataList.Add(_data.GuideChkPos[1].ToString());
            InitialParamDataList.Add(_data.GuideChkPos[2].ToString());
            InitialParamDataList.Add(_data.GuideChkPos[3].ToString());
            InitialParamDataList.Add(_data.GuideChkPos[4].ToString());
            InitialParamDataList.Add(_data.GuideChkPos[5].ToString());
            InitialParamDataList.Add(_data.GuideChkPos[6].ToString());
            InitialParamDataList.Add(_data.GuideChkPos[7].ToString());
            InitialParamDataList.Add(_data.GuideChkPos[8].ToString());
            InitialParamDataList.Add(_data.GuideExchSpdW.ToString());
            InitialParamDataList.Add(_data.GuideExchOfsW1.ToString());
            InitialParamDataList.Add(_data.GuideExchOfsW2.ToString());
            InitialParamDataList.Add(_data.EdgeSrchOfs[0].ToString());
            InitialParamDataList.Add(_data.EdgeSrchOfs[1].ToString());
            InitialParamDataList.Add(_data.EdgeSrchOfs[2].ToString());
            InitialParamDataList.Add(_data.EdgeSrchOfs[3].ToString());
            InitialParamDataList.Add(_data.EdgeSrchOfs[4].ToString());
            InitialParamDataList.Add(_data.EdgeSrchOfs[5].ToString());
            InitialParamDataList.Add(_data.EdgeSrchOfs[6].ToString());
            InitialParamDataList.Add(_data.EdgeSrchOfs[7].ToString());
            InitialParamDataList.Add(_data.EdgeSrchOfs[8].ToString());
            InitialParamDataList.Add(_data.EdgeSrchTouchSpd1[0].ToString());
            InitialParamDataList.Add(_data.EdgeSrchTouchSpd1[1].ToString());
            InitialParamDataList.Add(_data.EdgeSrchTouchSpd1[2].ToString());
            InitialParamDataList.Add(_data.EdgeSrchTouchSpd1[3].ToString());
            InitialParamDataList.Add(_data.EdgeSrchTouchSpd1[4].ToString());
            InitialParamDataList.Add(_data.EdgeSrchTouchSpd1[5].ToString());
            InitialParamDataList.Add(_data.EdgeSrchTouchSpd1[6].ToString());
            InitialParamDataList.Add(_data.EdgeSrchTouchSpd1[7].ToString());
            InitialParamDataList.Add(_data.EdgeSrchTouchSpd1[8].ToString());
            InitialParamDataList.Add(_data.EdgeSrchTouchRet1[0].ToString());
            InitialParamDataList.Add(_data.EdgeSrchTouchRet1[1].ToString());
            InitialParamDataList.Add(_data.EdgeSrchTouchRet1[2].ToString());
            InitialParamDataList.Add(_data.EdgeSrchTouchRet1[3].ToString());
            InitialParamDataList.Add(_data.EdgeSrchTouchRet1[4].ToString());
            InitialParamDataList.Add(_data.EdgeSrchTouchRet1[5].ToString());
            InitialParamDataList.Add(_data.EdgeSrchTouchRet1[6].ToString());
            InitialParamDataList.Add(_data.EdgeSrchTouchRet1[7].ToString());
            InitialParamDataList.Add(_data.EdgeSrchTouchRet1[8].ToString());
            InitialParamDataList.Add(_data.EdgeSrchTouchSpd2[0].ToString());
            InitialParamDataList.Add(_data.EdgeSrchTouchSpd2[1].ToString());
            InitialParamDataList.Add(_data.EdgeSrchTouchSpd2[2].ToString());
            InitialParamDataList.Add(_data.EdgeSrchTouchSpd2[3].ToString());
            InitialParamDataList.Add(_data.EdgeSrchTouchSpd2[4].ToString());
            InitialParamDataList.Add(_data.EdgeSrchTouchSpd2[5].ToString());
            InitialParamDataList.Add(_data.EdgeSrchTouchSpd2[6].ToString());
            InitialParamDataList.Add(_data.EdgeSrchTouchSpd2[7].ToString());
            InitialParamDataList.Add(_data.EdgeSrchTouchSpd2[8].ToString());
            InitialParamDataList.Add(_data.EdgeSrchTouchRet2[0].ToString());
            InitialParamDataList.Add(_data.EdgeSrchTouchRet2[1].ToString());
            InitialParamDataList.Add(_data.EdgeSrchTouchRet2[2].ToString());
            InitialParamDataList.Add(_data.EdgeSrchTouchRet2[3].ToString());
            InitialParamDataList.Add(_data.EdgeSrchTouchRet2[4].ToString());
            InitialParamDataList.Add(_data.EdgeSrchTouchRet2[5].ToString());
            InitialParamDataList.Add(_data.EdgeSrchTouchRet2[6].ToString());
            InitialParamDataList.Add(_data.EdgeSrchTouchRet2[7].ToString());
            InitialParamDataList.Add(_data.EdgeSrchTouchRet2[8].ToString());
            InitialParamDataList.Add(_data.EdgeSrchZDwnSpd.ToString());
            InitialParamDataList.Add(_data.TouchSenseTime.ToString());
            InitialParamDataList.Add(_data.RefTouchUpZ.ToString());
            InitialParamDataList.Add(_data.PrcsRetSpdZ.ToString());
            InitialParamDataList.Add(_data.EdgeSrchZUpSpd.ToString());
            InitialParamDataList.Add(_data.ElctdClumpSpdS.ToString());
            InitialParamDataList.Add(_data.BucklingUpOfsZ.ToString());
            InitialParamDataList.Add(_data.BucklingUpSpdZ.ToString());
            InitialParamDataList.Add(_data.BucklingRetry.ToString());
            InitialParamDataList.Add(_data.AecEnable.ToString());
            InitialParamDataList.Add(_data.Z20ErrOfs.ToString());
            InitialParamDataList.Add(_data.McnType.ToString());
            InitialParamDataList.Add(_data.GuideSensorDis.ToString());
            InitialParamDataList.Add(_data.CysCheckDis[0].ToString());
            InitialParamDataList.Add(_data.CysCheckDis[1].ToString());
            InitialParamDataList.Add(_data.CysCheckDis[2].ToString());
            InitialParamDataList.Add(_data.CysCheckDis[3].ToString());
            InitialParamDataList.Add(_data.AxAecActDis.ToString());
            InitialParamDataList.Add(_data.AxBrakeEn.ToString());
            InitialParamDataList.Add(_data.BrakeTimer.ToString());
            InitialParamDataList.Add(_data.TchErrCancelTime.ToString());
            InitialParamDataList.Add(_data.CylPulseOut[0].ToString());
            InitialParamDataList.Add(_data.CylPulseOut[1].ToString());
            InitialParamDataList.Add(_data.CylPulseOut[2].ToString());
            InitialParamDataList.Add(_data.CylPulseOut[3].ToString());
            InitialParamDataList.Add(_data.CylPulseTime.ToString());
            InitialParamDataList.Add(_data.PrcsFirstP99xDis.ToString());
            InitialParamDataList.Add(_data.LeaveZrnFin.ToString());
            InitialParamDataList.Add(_data.EdgeSrchMaxMinUse.ToString());
            InitialParamDataList.Add(_data.EdgeSrchOldMcnComp.ToString());
            InitialParamDataList.Add(_data.CylSelBfrEDeplAec.ToString());
            InitialParamDataList.Add(_data.CylSelAftEDeplAec.ToString());
            InitialParamDataList.Add(_data.CysOffChkDis[0].ToString());
            InitialParamDataList.Add(_data.CysOffChkDis[1].ToString());
            InitialParamDataList.Add(_data.CysOffChkDis[2].ToString());
            InitialParamDataList.Add(_data.CysOffChkDis[3].ToString());
            InitialParamDataList.Add(_data.SvPrcsStpRetOfs.ToString());
            InitialParamDataList.Add(_data.SvPrcsRetOfs.ToString());
            InitialParamDataList.Add(_data.SvPrcsRetSpd.ToString());
            InitialParamDataList.Add(_data.AxServoPrcs.ToString());
            InitialParamDataList.Add(_data.CysLatch[0].ToString());
            InitialParamDataList.Add(_data.CysLatch[1].ToString());
            InitialParamDataList.Add(_data.CysLatch[2].ToString());
            InitialParamDataList.Add(_data.CysLatch[3].ToString());
            InitialParamDataList.Add(_data.InstLastElctdFlg.ToString());
            InitialParamDataList.Add(_data.SpinMaxSpd[0].ToString());
            InitialParamDataList.Add(_data.SpinMaxSpd[1].ToString());
            InitialParamDataList.Add(_data.SpinMaxSpd[2].ToString());
            InitialParamDataList.Add(_data.SpinMaxSpd[3].ToString());
            InitialParamDataList.Add(_data.SpinMaxSpd[4].ToString());
            InitialParamDataList.Add(_data.SpinMaxSpd[5].ToString());
            InitialParamDataList.Add(_data.SpinMaxSpd[6].ToString());
            InitialParamDataList.Add(_data.SpinMaxSpd[7].ToString());
            InitialParamDataList.Add(_data.SpinMaxSpd[8].ToString());
            InitialParamDataList.Add(_data.PrcsSkipStopCys.ToString());
            InitialParamDataList.Add(_data.DischgTchErrEn.ToString());
            InitialParamDataList.Add(_data.CFeedTable[0].ToString());
            InitialParamDataList.Add(_data.CFeedTable[1].ToString());
            InitialParamDataList.Add(_data.CFeedTable[2].ToString());
            InitialParamDataList.Add(_data.CFeedTable[3].ToString());
            InitialParamDataList.Add(_data.CFeedTable[4].ToString());
            InitialParamDataList.Add(_data.CFeedTable[5].ToString());
            InitialParamDataList.Add(_data.CFeedTable[6].ToString());
            InitialParamDataList.Add(_data.CFeedTable[7].ToString());
            InitialParamDataList.Add(_data.CFeedTable[8].ToString());
            InitialParamDataList.Add(_data.CFeedTable[9].ToString());
            InitialParamDataList.Add(_data.CFeedTable[10].ToString());
            InitialParamDataList.Add(_data.CFeedTable[11].ToString());
            InitialParamDataList.Add(_data.CFeedTable[12].ToString());
            InitialParamDataList.Add(_data.CFeedTable[13].ToString());
            InitialParamDataList.Add(_data.CFeedTable[14].ToString());
            InitialParamDataList.Add(_data.CFeedTable[15].ToString());
            InitialParamDataList.Add(_data.GuideThroughChkDis.ToString());
            InitialParamDataList.Add(_data.Z2ndSoftLimMSelCys.ToString());
            InitialParamDataList.Add(_data.Z2ndSoftLimM.ToString());
            InitialParamDataList.Add(_data.GuidChgCylEn.ToString());
            InitialParamDataList.Add(_data.GuidChgCysSel.ToString());
            InitialParamDataList.Add(_data.GuidHolderEscOfs[0].ToString());
            InitialParamDataList.Add(_data.GuidHolderEscOfs[1].ToString());
            InitialParamDataList.Add(_data.GuidHolderEscOfs[2].ToString());
            InitialParamDataList.Add(_data.GuidHolderEscOfs[3].ToString());
            InitialParamDataList.Add(_data.GuidHolderEscOfs[4].ToString());
            InitialParamDataList.Add(_data.GuidHolderEscOfs[5].ToString());
            InitialParamDataList.Add(_data.GuidHolderEscOfs[6].ToString());
            InitialParamDataList.Add(_data.GuidHolderEscOfs[7].ToString());
            InitialParamDataList.Add(_data.GuidHolderEscOfs[8].ToString());
            InitialParamDataList.Add(_data.GuidHolderArmDis.ToString());
            InitialParamDataList.Add(_data.TouchSenseISetEn.ToString());
            InitialParamDataList.Add(_data.CylSelPatRndStp.ToString());
            InitialParamDataList.Add(_data.PatRndStpOnlyElctdDep.ToString());
            InitialParamDataList.Add(_data.CylSelThrghDetect.ToString());
            InitialParamDataList.Add(_data.RefSpdSelThrghDetect.ToString());
            InitialParamDataList.Add(_data.PerInitThrghDetect.ToString());
            InitialParamDataList.Add(_data.PerInitThDetectEdge.ToString());
            InitialParamDataList.Add(_data.AvTimInitThDetectEdge.ToString());
            InitialParamDataList.Add(_data.RefSpdIgnHPerInit.ToString());
            InitialParamDataList.Add(_data.RefSpdIgnLPerInit.ToString());
            InitialParamDataList.Add(_data.RefSpdAvTimInit.ToString());
            InitialParamDataList.Add(_data.PreElctdChgLmtPos.ToString());
            InitialParamDataList.Add(_data.ElctdExchMovOrder[0].ToString());
            InitialParamDataList.Add(_data.ElctdExchMovOrder[1].ToString());
            InitialParamDataList.Add(_data.ElctdExchMovOrder[2].ToString());
            InitialParamDataList.Add(_data.ElctdExchMovOrder[3].ToString());
            InitialParamDataList.Add(_data.ElctdExchMovOrder[4].ToString());
            InitialParamDataList.Add(_data.ElctdExchMovOrder[5].ToString());
            InitialParamDataList.Add(_data.ElctdExchMovOrder[6].ToString());
            InitialParamDataList.Add(_data.ElctdExchMovOrder[7].ToString());
            InitialParamDataList.Add(_data.ElctdExchMovOrder[8].ToString());
            InitialParamDataList.Add(_data.CylSelInElctdPrcs.ToString());
            InitialParamDataList.Add(_data.PatliteMode.ToString());
            InitialParamDataList.Add(_data.AxVirActDis.ToString());
            InitialParamDataList.Add(_data.SvPrcsInitialTime.ToString());
            InitialParamDataList.Add(_data.ThinElctdISetPumpOff.ToString());
            InitialParamDataList.Add(_data.AecWRefSel.ToString());
            InitialParamDataList.Add(_data.GuideThroughChkPNo.ToString());
            InitialParamDataList.Add(_data.InitialSetPno[0].ToString());
            InitialParamDataList.Add(_data.InitialSetPno[1].ToString());
            InitialParamDataList.Add(_data.InitialSetPno[2].ToString());
            InitialParamDataList.Add(_data.InitialSetPno[3].ToString());
            InitialParamDataList.Add(_data.InitialSetPno[4].ToString());
            InitialParamDataList.Add(_data.InitialSetPno[5].ToString());
            InitialParamDataList.Add(_data.ElctdChkOfsZ2[0].ToString());
            InitialParamDataList.Add(_data.ElctdChkOfsZ2[1].ToString());
            InitialParamDataList.Add(_data.ElctdChkOfsZ2[2].ToString());
            InitialParamDataList.Add(_data.ElctdChkOfsZ2[3].ToString());
            InitialParamDataList.Add(_data.ElctdChkOfsZ2[4].ToString());
            InitialParamDataList.Add(_data.ElctdChkOfsZ2[5].ToString());
            InitialParamDataList.Add(_data.GuideThroughChkPNoPat[0].ToString());
            InitialParamDataList.Add(_data.GuideThroughChkPNoPat[1].ToString());
            InitialParamDataList.Add(_data.GuideThroughChkPNoPat[2].ToString());
            InitialParamDataList.Add(_data.GuideThroughChkPNoPat[3].ToString());
            InitialParamDataList.Add(_data.GuideThroughChkPNoPat[4].ToString());
            InitialParamDataList.Add(_data.GuideThroughChkPNoPat[5].ToString());
            InitialParamDataList.Add(_data.EUninstColReclampDis.ToString());
            InitialParamDataList.Add(_data.AvTimInitThrghDetect.ToString());
            InitialParamDataList.Add(_data.EdgeLSrchTouchRet.ToString());
            InitialParamDataList.Add(_data.EdgeLSrchRetSpd.ToString());
            InitialParamDataList.Add(_data.InitPrmMacVal[0].ToString());
            InitialParamDataList.Add(_data.InitPrmMacVal[1].ToString());
            InitialParamDataList.Add(_data.InitPrmMacVal[2].ToString());
            InitialParamDataList.Add(_data.InitPrmMacVal[3].ToString());
            InitialParamDataList.Add(_data.InitPrmMacVal[4].ToString());
            InitialParamDataList.Add(_data.InitPrmMacVal[5].ToString());
            InitialParamDataList.Add(_data.InitPrmMacVal[6].ToString());
            InitialParamDataList.Add(_data.InitPrmMacVal[7].ToString());
            InitialParamDataList.Add(_data.InitPrmMacVal[8].ToString());
            InitialParamDataList.Add(_data.InitPrmMacVal[9].ToString());
            InitialParamDataList.Add(_data.NrmElctdPrcsBuckling.ToString());
            InitialParamDataList.Add(_data.NrmElctdGuideBuckling.ToString());
            InitialParamDataList.Add(_data.RotAxMovMode.ToString());
            InitialParamDataList.Add(_data.AutoModeOutSel.ToString());
            InitialParamDataList.Add(_data.CylBitRstPlsCont[0].ToString());
            InitialParamDataList.Add(_data.CylBitRstPlsCont[1].ToString());
            InitialParamDataList.Add(_data.CylBitRstPlsCont[2].ToString());
            InitialParamDataList.Add(_data.CylBitRstPlsCont[3].ToString());
            InitialParamDataList.Add(_data.PrcsTmoutChkEndSel.ToString());
            InitialParamDataList.Add(_data.MountedSF02FX.ToString());
            InitialParamDataList.Add(_data.EIFErr1MaskSet.ToString());
            InitialParamDataList.Add(_data.EIFErr2MaskSet.ToString());
            InitialParamDataList.Add(_data.Fanstop10DlyTim.ToString());
            InitialParamDataList.Add(_data.Fanstop20DlyTim.ToString());
            InitialParamDataList.Add(_data.EIFErr1Action[0].ToString());
            InitialParamDataList.Add(_data.EIFErr1Action[1].ToString());
            InitialParamDataList.Add(_data.EIFErr1Action[2].ToString());
            InitialParamDataList.Add(_data.EIFErr1Action[3].ToString());
            InitialParamDataList.Add(_data.EIFErr1Action[4].ToString());
            InitialParamDataList.Add(_data.EIFErr1Action[5].ToString());
            InitialParamDataList.Add(_data.EIFErr1Action[6].ToString());
            InitialParamDataList.Add(_data.EIFErr1Action[7].ToString());
            InitialParamDataList.Add(_data.EIFErr1Action[8].ToString());
            InitialParamDataList.Add(_data.EIFErr1Action[9].ToString());
            InitialParamDataList.Add(_data.EIFErr1Action[10].ToString());
            InitialParamDataList.Add(_data.EIFErr1Action[11].ToString());
            InitialParamDataList.Add(_data.EIFErr1Action[12].ToString());
            InitialParamDataList.Add(_data.EIFErr1Action[13].ToString());
            InitialParamDataList.Add(_data.EIFErr1Action[14].ToString());
            InitialParamDataList.Add(_data.EIFErr1Action[15].ToString());
            InitialParamDataList.Add(_data.EIFErr1Action[16].ToString());
            InitialParamDataList.Add(_data.EIFErr1Action[17].ToString());
            InitialParamDataList.Add(_data.EIFErr1Action[18].ToString());
            InitialParamDataList.Add(_data.EIFErr1Action[19].ToString());
            InitialParamDataList.Add(_data.EIFErr1Action[20].ToString());
            InitialParamDataList.Add(_data.EIFErr1Action[21].ToString());
            InitialParamDataList.Add(_data.EIFErr1Action[22].ToString());
            InitialParamDataList.Add(_data.EIFErr1Action[23].ToString());
            InitialParamDataList.Add(_data.EIFErr1Action[24].ToString());
            InitialParamDataList.Add(_data.EIFErr1Action[25].ToString());
            InitialParamDataList.Add(_data.EIFErr1Action[26].ToString());
            InitialParamDataList.Add(_data.EIFErr1Action[27].ToString());
            InitialParamDataList.Add(_data.EIFErr1Action[28].ToString());
            InitialParamDataList.Add(_data.EIFErr1Action[29].ToString());
            InitialParamDataList.Add(_data.EIFErr1Action[30].ToString());
            InitialParamDataList.Add(_data.EIFErr1Action[31].ToString());
            InitialParamDataList.Add(_data.EIFErr2Action[0].ToString());
            InitialParamDataList.Add(_data.EIFErr2Action[1].ToString());
            InitialParamDataList.Add(_data.EIFErr2Action[2].ToString());
            InitialParamDataList.Add(_data.EIFErr2Action[3].ToString());
            InitialParamDataList.Add(_data.EIFErr2Action[4].ToString());
            InitialParamDataList.Add(_data.EIFErr2Action[5].ToString());
            InitialParamDataList.Add(_data.EIFErr2Action[6].ToString());
            InitialParamDataList.Add(_data.EIFErr2Action[7].ToString());
            InitialParamDataList.Add(_data.EIFErr2Action[8].ToString());
            InitialParamDataList.Add(_data.EIFErr2Action[9].ToString());
            InitialParamDataList.Add(_data.EIFErr2Action[10].ToString());
            InitialParamDataList.Add(_data.EIFErr2Action[11].ToString());
            InitialParamDataList.Add(_data.EIFErr2Action[12].ToString());
            InitialParamDataList.Add(_data.EIFErr2Action[13].ToString());
            InitialParamDataList.Add(_data.EIFErr2Action[14].ToString());
            InitialParamDataList.Add(_data.EIFErr2Action[15].ToString());
            InitialParamDataList.Add(_data.EIFErr2Action[16].ToString());
            InitialParamDataList.Add(_data.EIFErr2Action[17].ToString());
            InitialParamDataList.Add(_data.EIFErr2Action[18].ToString());
            InitialParamDataList.Add(_data.EIFErr2Action[19].ToString());
            InitialParamDataList.Add(_data.EIFErr2Action[20].ToString());
            InitialParamDataList.Add(_data.EIFErr2Action[21].ToString());
            InitialParamDataList.Add(_data.EIFErr2Action[22].ToString());
            InitialParamDataList.Add(_data.EIFErr2Action[23].ToString());
            InitialParamDataList.Add(_data.EIFErr2Action[24].ToString());
            InitialParamDataList.Add(_data.EIFErr2Action[25].ToString());
            InitialParamDataList.Add(_data.EIFErr2Action[26].ToString());
            InitialParamDataList.Add(_data.EIFErr2Action[27].ToString());
            InitialParamDataList.Add(_data.EIFErr2Action[28].ToString());
            InitialParamDataList.Add(_data.EIFErr2Action[29].ToString());
            InitialParamDataList.Add(_data.EIFErr2Action[30].ToString());
            InitialParamDataList.Add(_data.EIFErr2Action[31].ToString());
            InitialParamDataList.Add(_data.MountedBP20.ToString());
            InitialParamDataList.Add(_data.MountedIPEnhance.ToString());
            InitialParamDataList.Add(_data.MountedACPower.ToString());
            InitialParamDataList.Add(_data.MountedNanoPulse.ToString());
            InitialParamDataList.Add(_data.MountedHVOverlay.ToString());
            InitialParamDataList.Add(_data.MountedTouchProbe.ToString());
            InitialParamDataList.Add(_data.VSLvlSetting.ToString());
            InitialParamDataList.Add(_data.VSPKLvlSetting.ToString());
            InitialParamDataList.Add(_data.CntactSenseLvlSetting.ToString());
            InitialParamDataList.Add(_data.HPJogOvr[0].ToString());
            InitialParamDataList.Add(_data.HPJogOvr[1].ToString());
            InitialParamDataList.Add(_data.HPJogOvr[2].ToString());
            InitialParamDataList.Add(_data.HPJogOvr[3].ToString());
            InitialParamDataList.Add(_data.PLCShutDwonAuthEn.ToString());
            InitialParamDataList.Add(_data.EX1E321Setting[0].ToString());
            InitialParamDataList.Add(_data.EX1E321Setting[1].ToString());
            InitialParamDataList.Add(_data.EX1E321Setting[2].ToString());
            InitialParamDataList.Add(_data.EX1E321Setting[3].ToString());
            InitialParamDataList.Add(_data.EX1E321Setting[4].ToString());
            InitialParamDataList.Add(_data.EX1E321Setting[5].ToString());
            InitialParamDataList.Add(_data.EX1E321Setting[6].ToString());
            InitialParamDataList.Add(_data.EX1E321Setting[7].ToString());
            InitialParamDataList.Add(_data.EX1E321Setting[8].ToString());
            InitialParamDataList.Add(_data.EX1E321Setting[9].ToString());
            InitialParamDataList.Add(_data.EX1E321Setting[10].ToString());
            InitialParamDataList.Add(_data.EX1E321Setting[11].ToString());
            InitialParamDataList.Add(_data.CRS10DAMin.ToString());
            InitialParamDataList.Add(_data.CRS10DAMax.ToString());
            InitialParamDataList.Add(_data.CRS10DATable[0].ToString());
            InitialParamDataList.Add(_data.CRS10DATable[1].ToString());
            InitialParamDataList.Add(_data.CRS10DATable[2].ToString());
            InitialParamDataList.Add(_data.CRS10DATable[3].ToString());
            InitialParamDataList.Add(_data.CRS10DATable[4].ToString());
            InitialParamDataList.Add(_data.CRS10DATable[5].ToString());
            InitialParamDataList.Add(_data.CRS10DATable[6].ToString());
            InitialParamDataList.Add(_data.CRS10DATable[7].ToString());
            InitialParamDataList.Add(_data.CRS10DATable[8].ToString());
            InitialParamDataList.Add(_data.CRS10DATable[9].ToString());
            InitialParamDataList.Add(_data.CRS10DATable[10].ToString());
            InitialParamDataList.Add(_data.CRS10DATable[11].ToString());
            InitialParamDataList.Add(_data.CRS10DATable[12].ToString());
            InitialParamDataList.Add(_data.CRS10DATable[13].ToString());
            InitialParamDataList.Add(_data.CRS10DATable[14].ToString());
            InitialParamDataList.Add(_data.CRS10DATable[15].ToString());
            InitialParamDataList.Add(_data.SCDAMin.ToString());
            InitialParamDataList.Add(_data.SCDAMax.ToString());
            InitialParamDataList.Add(_data.SCDATable[0].ToString());
            InitialParamDataList.Add(_data.SCDATable[1].ToString());
            InitialParamDataList.Add(_data.SCDATable[2].ToString());
            InitialParamDataList.Add(_data.SCDATable[3].ToString());
            InitialParamDataList.Add(_data.SCDATable[4].ToString());
            InitialParamDataList.Add(_data.SCDATable[5].ToString());
            InitialParamDataList.Add(_data.SCDATable[6].ToString());
            InitialParamDataList.Add(_data.SCDATable[7].ToString());
            InitialParamDataList.Add(_data.SCDATable[8].ToString());
            InitialParamDataList.Add(_data.SCDATable[9].ToString());
            InitialParamDataList.Add(_data.SCDATable[10].ToString());
            InitialParamDataList.Add(_data.SCDATable[11].ToString());
            InitialParamDataList.Add(_data.SCDATable[12].ToString());
            InitialParamDataList.Add(_data.SCDATable[13].ToString());
            InitialParamDataList.Add(_data.SCDATable[14].ToString());
            InitialParamDataList.Add(_data.SCDATable[15].ToString());
            InitialParamDataList.Add(_data.SCDATable[16].ToString());
            InitialParamDataList.Add(_data.SCDATable[17].ToString());
            InitialParamDataList.Add(_data.SCDATable[18].ToString());
            InitialParamDataList.Add(_data.SCDATable[19].ToString());
            InitialParamDataList.Add(_data.SCDATable[20].ToString());
            InitialParamDataList.Add(_data.SCDATable[21].ToString());
            InitialParamDataList.Add(_data.SCDATable[22].ToString());
            InitialParamDataList.Add(_data.SCDATable[23].ToString());
            InitialParamDataList.Add(_data.SCDATable[24].ToString());
            InitialParamDataList.Add(_data.SCDATable[25].ToString());
            InitialParamDataList.Add(_data.SCDATable[26].ToString());
            InitialParamDataList.Add(_data.SCDATable[27].ToString());
            InitialParamDataList.Add(_data.SCDATable[28].ToString());
            InitialParamDataList.Add(_data.SCDATable[29].ToString());
            InitialParamDataList.Add(_data.SCDATable[30].ToString());
            InitialParamDataList.Add(_data.SCDATable[31].ToString());
            InitialParamDataList.Add(_data.SCDATable[32].ToString());
            InitialParamDataList.Add(_data.SCDATable[33].ToString());
            InitialParamDataList.Add(_data.SCDATable[34].ToString());
            InitialParamDataList.Add(_data.SCDATable[35].ToString());
            InitialParamDataList.Add(_data.SCDATable[36].ToString());
            InitialParamDataList.Add(_data.SCDATable[37].ToString());
            InitialParamDataList.Add(_data.SCDATable[38].ToString());
            InitialParamDataList.Add(_data.SCDATable[39].ToString());
            InitialParamDataList.Add(_data.SCDATable[40].ToString());
            InitialParamDataList.Add(_data.SCDATable[41].ToString());
            InitialParamDataList.Add(_data.SCDATable[42].ToString());
            InitialParamDataList.Add(_data.SCDATable[43].ToString());
            InitialParamDataList.Add(_data.SCDATable[44].ToString());
            InitialParamDataList.Add(_data.SCDATable[45].ToString());
            InitialParamDataList.Add(_data.SCDATable[46].ToString());
            InitialParamDataList.Add(_data.SCDATable[47].ToString());
            InitialParamDataList.Add(_data.SCDATable[48].ToString());
            InitialParamDataList.Add(_data.SCDATable[49].ToString());
            InitialParamDataList.Add(_data.SCDATable[50].ToString());
            InitialParamDataList.Add(_data.SCDATable[51].ToString());
            InitialParamDataList.Add(_data.SCDATable[52].ToString());
            InitialParamDataList.Add(_data.SCDATable[53].ToString());
            InitialParamDataList.Add(_data.SCDATable[54].ToString());
            InitialParamDataList.Add(_data.SCDATable[55].ToString());
            InitialParamDataList.Add(_data.SCDATable[56].ToString());
            InitialParamDataList.Add(_data.SCDATable[57].ToString());
            InitialParamDataList.Add(_data.SCDATable[58].ToString());
            InitialParamDataList.Add(_data.SCDATable[59].ToString());
            InitialParamDataList.Add(_data.SCDATable[60].ToString());
            InitialParamDataList.Add(_data.SCDATable[61].ToString());
            InitialParamDataList.Add(_data.SCDATable[62].ToString());
            InitialParamDataList.Add(_data.SCDATable[63].ToString());
            InitialParamDataList.Add(_data.CRSDAMin.ToString());
            InitialParamDataList.Add(_data.CRSDAMax.ToString());
            InitialParamDataList.Add(_data.CRSDATable[0].ToString());
            InitialParamDataList.Add(_data.CRSDATable[1].ToString());
            InitialParamDataList.Add(_data.CRSDATable[2].ToString());
            InitialParamDataList.Add(_data.CRSDATable[3].ToString());
            InitialParamDataList.Add(_data.CRSDATable[4].ToString());
            InitialParamDataList.Add(_data.CRSDATable[5].ToString());
            InitialParamDataList.Add(_data.CRSDATable[6].ToString());
            InitialParamDataList.Add(_data.CRSDATable[7].ToString());
            InitialParamDataList.Add(_data.CRSDATable[8].ToString());
            InitialParamDataList.Add(_data.CRSDATable[9].ToString());
            InitialParamDataList.Add(_data.CRSDATable[10].ToString());
            InitialParamDataList.Add(_data.CRSDATable[11].ToString());
            InitialParamDataList.Add(_data.CRSDATable[12].ToString());
            InitialParamDataList.Add(_data.CRSDATable[13].ToString());
            InitialParamDataList.Add(_data.CRSDATable[14].ToString());
            InitialParamDataList.Add(_data.CRSDATable[15].ToString());
            InitialParamDataList.TrimExcess();
            return ret;
        }

        public ResultCodes WriteAll()
        {
            //基底クラスへ書き込み用データを送る
            _data.ElctdExchPos[0] = SetInitialPrm(InitialParamDataList[0], _data.ElctdExchPos[0]);
            _data.ElctdExchPos[1] = SetInitialPrm(InitialParamDataList[1], _data.ElctdExchPos[1]);
            _data.ElctdExchPos[2] = SetInitialPrm(InitialParamDataList[2], _data.ElctdExchPos[2]);
            _data.ElctdExchPos[3] = SetInitialPrm(InitialParamDataList[3], _data.ElctdExchPos[3]);
            _data.ElctdExchPos[4] = SetInitialPrm(InitialParamDataList[4], _data.ElctdExchPos[4]);
            _data.ElctdExchPos[5] = SetInitialPrm(InitialParamDataList[5], _data.ElctdExchPos[5]);
            _data.ElctdExchPos[6] = SetInitialPrm(InitialParamDataList[6], _data.ElctdExchPos[6]);
            _data.ElctdExchPos[7] = SetInitialPrm(InitialParamDataList[7], _data.ElctdExchPos[7]);
            _data.ElctdExchPos[8] = SetInitialPrm(InitialParamDataList[8], _data.ElctdExchPos[8]);
            _data.ElctdExchSpdW = SetInitialPrm(InitialParamDataList[9], _data.ElctdExchSpdW);
            _data.ElctdExchSpdW1 = SetInitialPrm(InitialParamDataList[10], _data.ElctdExchSpdW1);
            _data.ElctdExchOfsW1 = SetInitialPrm(InitialParamDataList[11], _data.ElctdExchOfsW1);
            _data.ElctdExchOfsW2 = SetInitialPrm(InitialParamDataList[12], _data.ElctdExchOfsW2);
            _data.ElctdChkSpdZ1[0] = SetInitialPrm(InitialParamDataList[13], _data.ElctdChkSpdZ1[0]);
            _data.ElctdChkSpdZ1[1] = SetInitialPrm(InitialParamDataList[14], _data.ElctdChkSpdZ1[1]);
            _data.ElctdChkSpdZ1[2] = SetInitialPrm(InitialParamDataList[15], _data.ElctdChkSpdZ1[2]);
            _data.ElctdChkSpdZ1[3] = SetInitialPrm(InitialParamDataList[16], _data.ElctdChkSpdZ1[3]);
            _data.ElctdChkSpdZ1[4] = SetInitialPrm(InitialParamDataList[17], _data.ElctdChkSpdZ1[4]);
            _data.ElctdChkSpdZ1[5] = SetInitialPrm(InitialParamDataList[18], _data.ElctdChkSpdZ1[5]);
            _data.ElctdChkSpdZ2[0] = SetInitialPrm(InitialParamDataList[19], _data.ElctdChkSpdZ2[0]);
            _data.ElctdChkSpdZ2[1] = SetInitialPrm(InitialParamDataList[20], _data.ElctdChkSpdZ2[1]);
            _data.ElctdChkSpdZ2[2] = SetInitialPrm(InitialParamDataList[21], _data.ElctdChkSpdZ2[2]);
            _data.ElctdChkSpdZ2[3] = SetInitialPrm(InitialParamDataList[22], _data.ElctdChkSpdZ2[3]);
            _data.ElctdChkSpdZ2[4] = SetInitialPrm(InitialParamDataList[23], _data.ElctdChkSpdZ2[4]);
            _data.ElctdChkSpdZ2[5] = SetInitialPrm(InitialParamDataList[24], _data.ElctdChkSpdZ2[5]);
            _data.ElctdChkSpdS2[0] = SetInitialPrm(InitialParamDataList[25], _data.ElctdChkSpdS2[0]);
            _data.ElctdChkSpdS2[1] = SetInitialPrm(InitialParamDataList[26], _data.ElctdChkSpdS2[1]);
            _data.ElctdChkSpdS2[2] = SetInitialPrm(InitialParamDataList[27], _data.ElctdChkSpdS2[2]);
            _data.ElctdChkSpdS2[3] = SetInitialPrm(InitialParamDataList[28], _data.ElctdChkSpdS2[3]);
            _data.ElctdChkSpdS2[4] = SetInitialPrm(InitialParamDataList[29], _data.ElctdChkSpdS2[4]);
            _data.ElctdChkSpdS2[5] = SetInitialPrm(InitialParamDataList[30], _data.ElctdChkSpdS2[5]);
            _data.ElctdChkOfsZ[0] = SetInitialPrm(InitialParamDataList[31], _data.ElctdChkOfsZ[0]);
            _data.ElctdChkOfsZ[1] = SetInitialPrm(InitialParamDataList[32], _data.ElctdChkOfsZ[1]);
            _data.ElctdChkOfsZ[2] = SetInitialPrm(InitialParamDataList[33], _data.ElctdChkOfsZ[2]);
            _data.ElctdChkOfsZ[3] = SetInitialPrm(InitialParamDataList[34], _data.ElctdChkOfsZ[3]);
            _data.ElctdChkOfsZ[4] = SetInitialPrm(InitialParamDataList[35], _data.ElctdChkOfsZ[4]);
            _data.ElctdChkOfsZ[5] = SetInitialPrm(InitialParamDataList[36], _data.ElctdChkOfsZ[5]);
            _data.ElctdDeplUpZ = SetInitialPrm(InitialParamDataList[37], _data.ElctdDeplUpZ);
            _data.ElctdNum = SetInitialPrm(InitialParamDataList[38], _data.ElctdNum);
            _data.GuideNum = SetInitialPrm(InitialParamDataList[39], _data.GuideNum);
            _data.GuideExchPosS[0] = SetInitialPrm(InitialParamDataList[40], _data.GuideExchPosS[0]);
            _data.GuideExchPosS[1] = SetInitialPrm(InitialParamDataList[41], _data.GuideExchPosS[1]);
            _data.GuideExchPosS[2] = SetInitialPrm(InitialParamDataList[42], _data.GuideExchPosS[2]);
            _data.GuideExchPosS[3] = SetInitialPrm(InitialParamDataList[43], _data.GuideExchPosS[3]);
            _data.GuideExchPosS[4] = SetInitialPrm(InitialParamDataList[44], _data.GuideExchPosS[4]);
            _data.GuideExchPosS[5] = SetInitialPrm(InitialParamDataList[45], _data.GuideExchPosS[5]);
            _data.GuideExchPosS[6] = SetInitialPrm(InitialParamDataList[46], _data.GuideExchPosS[6]);
            _data.GuideExchPosS[7] = SetInitialPrm(InitialParamDataList[47], _data.GuideExchPosS[7]);
            _data.GuideExchPosS[8] = SetInitialPrm(InitialParamDataList[48], _data.GuideExchPosS[8]);
            _data.GuideExchPosE[0] = SetInitialPrm(InitialParamDataList[49], _data.GuideExchPosE[0]);
            _data.GuideExchPosE[1] = SetInitialPrm(InitialParamDataList[50], _data.GuideExchPosE[1]);
            _data.GuideExchPosE[2] = SetInitialPrm(InitialParamDataList[51], _data.GuideExchPosE[2]);
            _data.GuideExchPosE[3] = SetInitialPrm(InitialParamDataList[52], _data.GuideExchPosE[3]);
            _data.GuideExchPosE[4] = SetInitialPrm(InitialParamDataList[53], _data.GuideExchPosE[4]);
            _data.GuideExchPosE[5] = SetInitialPrm(InitialParamDataList[54], _data.GuideExchPosE[5]);
            _data.GuideExchPosE[6] = SetInitialPrm(InitialParamDataList[55], _data.GuideExchPosE[6]);
            _data.GuideExchPosE[7] = SetInitialPrm(InitialParamDataList[56], _data.GuideExchPosE[7]);
            _data.GuideExchPosE[8] = SetInitialPrm(InitialParamDataList[57], _data.GuideExchPosE[8]);
            _data.GuideChkPos[0] = SetInitialPrm(InitialParamDataList[58], _data.GuideChkPos[0]);
            _data.GuideChkPos[1] = SetInitialPrm(InitialParamDataList[59], _data.GuideChkPos[1]);
            _data.GuideChkPos[2] = SetInitialPrm(InitialParamDataList[60], _data.GuideChkPos[2]);
            _data.GuideChkPos[3] = SetInitialPrm(InitialParamDataList[61], _data.GuideChkPos[3]);
            _data.GuideChkPos[4] = SetInitialPrm(InitialParamDataList[62], _data.GuideChkPos[4]);
            _data.GuideChkPos[5] = SetInitialPrm(InitialParamDataList[63], _data.GuideChkPos[5]);
            _data.GuideChkPos[6] = SetInitialPrm(InitialParamDataList[64], _data.GuideChkPos[6]);
            _data.GuideChkPos[7] = SetInitialPrm(InitialParamDataList[65], _data.GuideChkPos[7]);
            _data.GuideChkPos[8] = SetInitialPrm(InitialParamDataList[66], _data.GuideChkPos[8]);
            _data.GuideExchSpdW = SetInitialPrm(InitialParamDataList[67], _data.GuideExchSpdW);
            _data.GuideExchOfsW1 = SetInitialPrm(InitialParamDataList[68], _data.GuideExchOfsW1);
            _data.GuideExchOfsW2 = SetInitialPrm(InitialParamDataList[69], _data.GuideExchOfsW2);
            _data.EdgeSrchOfs[0] = SetInitialPrm(InitialParamDataList[70], _data.EdgeSrchOfs[0]);
            _data.EdgeSrchOfs[1] = SetInitialPrm(InitialParamDataList[71], _data.EdgeSrchOfs[1]);
            _data.EdgeSrchOfs[2] = SetInitialPrm(InitialParamDataList[72], _data.EdgeSrchOfs[2]);
            _data.EdgeSrchOfs[3] = SetInitialPrm(InitialParamDataList[73], _data.EdgeSrchOfs[3]);
            _data.EdgeSrchOfs[4] = SetInitialPrm(InitialParamDataList[74], _data.EdgeSrchOfs[4]);
            _data.EdgeSrchOfs[5] = SetInitialPrm(InitialParamDataList[75], _data.EdgeSrchOfs[5]);
            _data.EdgeSrchOfs[6] = SetInitialPrm(InitialParamDataList[76], _data.EdgeSrchOfs[6]);
            _data.EdgeSrchOfs[7] = SetInitialPrm(InitialParamDataList[77], _data.EdgeSrchOfs[7]);
            _data.EdgeSrchOfs[8] = SetInitialPrm(InitialParamDataList[78], _data.EdgeSrchOfs[8]);
            _data.EdgeSrchTouchSpd1[0] = SetInitialPrm(InitialParamDataList[79], _data.EdgeSrchTouchSpd1[0]);
            _data.EdgeSrchTouchSpd1[1] = SetInitialPrm(InitialParamDataList[80], _data.EdgeSrchTouchSpd1[1]);
            _data.EdgeSrchTouchSpd1[2] = SetInitialPrm(InitialParamDataList[81], _data.EdgeSrchTouchSpd1[2]);
            _data.EdgeSrchTouchSpd1[3] = SetInitialPrm(InitialParamDataList[82], _data.EdgeSrchTouchSpd1[3]);
            _data.EdgeSrchTouchSpd1[4] = SetInitialPrm(InitialParamDataList[83], _data.EdgeSrchTouchSpd1[4]);
            _data.EdgeSrchTouchSpd1[5] = SetInitialPrm(InitialParamDataList[84], _data.EdgeSrchTouchSpd1[5]);
            _data.EdgeSrchTouchSpd1[6] = SetInitialPrm(InitialParamDataList[85], _data.EdgeSrchTouchSpd1[6]);
            _data.EdgeSrchTouchSpd1[7] = SetInitialPrm(InitialParamDataList[86], _data.EdgeSrchTouchSpd1[7]);
            _data.EdgeSrchTouchSpd1[8] = SetInitialPrm(InitialParamDataList[87], _data.EdgeSrchTouchSpd1[8]);
            _data.EdgeSrchTouchRet1[0] = SetInitialPrm(InitialParamDataList[88], _data.EdgeSrchTouchRet1[0]);
            _data.EdgeSrchTouchRet1[1] = SetInitialPrm(InitialParamDataList[89], _data.EdgeSrchTouchRet1[1]);
            _data.EdgeSrchTouchRet1[2] = SetInitialPrm(InitialParamDataList[90], _data.EdgeSrchTouchRet1[2]);
            _data.EdgeSrchTouchRet1[3] = SetInitialPrm(InitialParamDataList[91], _data.EdgeSrchTouchRet1[3]);
            _data.EdgeSrchTouchRet1[4] = SetInitialPrm(InitialParamDataList[92], _data.EdgeSrchTouchRet1[4]);
            _data.EdgeSrchTouchRet1[5] = SetInitialPrm(InitialParamDataList[93], _data.EdgeSrchTouchRet1[5]);
            _data.EdgeSrchTouchRet1[6] = SetInitialPrm(InitialParamDataList[94], _data.EdgeSrchTouchRet1[6]);
            _data.EdgeSrchTouchRet1[7] = SetInitialPrm(InitialParamDataList[95], _data.EdgeSrchTouchRet1[7]);
            _data.EdgeSrchTouchRet1[8] = SetInitialPrm(InitialParamDataList[96], _data.EdgeSrchTouchRet1[8]);
            _data.EdgeSrchTouchSpd2[0] = SetInitialPrm(InitialParamDataList[97], _data.EdgeSrchTouchSpd2[0]);
            _data.EdgeSrchTouchSpd2[1] = SetInitialPrm(InitialParamDataList[98], _data.EdgeSrchTouchSpd2[1]);
            _data.EdgeSrchTouchSpd2[2] = SetInitialPrm(InitialParamDataList[99], _data.EdgeSrchTouchSpd2[2]);
            _data.EdgeSrchTouchSpd2[3] = SetInitialPrm(InitialParamDataList[100], _data.EdgeSrchTouchSpd2[3]);
            _data.EdgeSrchTouchSpd2[4] = SetInitialPrm(InitialParamDataList[101], _data.EdgeSrchTouchSpd2[4]);
            _data.EdgeSrchTouchSpd2[5] = SetInitialPrm(InitialParamDataList[102], _data.EdgeSrchTouchSpd2[5]);
            _data.EdgeSrchTouchSpd2[6] = SetInitialPrm(InitialParamDataList[103], _data.EdgeSrchTouchSpd2[6]);
            _data.EdgeSrchTouchSpd2[7] = SetInitialPrm(InitialParamDataList[104], _data.EdgeSrchTouchSpd2[7]);
            _data.EdgeSrchTouchSpd2[8] = SetInitialPrm(InitialParamDataList[105], _data.EdgeSrchTouchSpd2[8]);
            _data.EdgeSrchTouchRet2[0] = SetInitialPrm(InitialParamDataList[106], _data.EdgeSrchTouchRet2[0]);
            _data.EdgeSrchTouchRet2[1] = SetInitialPrm(InitialParamDataList[107], _data.EdgeSrchTouchRet2[1]);
            _data.EdgeSrchTouchRet2[2] = SetInitialPrm(InitialParamDataList[108], _data.EdgeSrchTouchRet2[2]);
            _data.EdgeSrchTouchRet2[3] = SetInitialPrm(InitialParamDataList[109], _data.EdgeSrchTouchRet2[3]);
            _data.EdgeSrchTouchRet2[4] = SetInitialPrm(InitialParamDataList[110], _data.EdgeSrchTouchRet2[4]);
            _data.EdgeSrchTouchRet2[5] = SetInitialPrm(InitialParamDataList[111], _data.EdgeSrchTouchRet2[5]);
            _data.EdgeSrchTouchRet2[6] = SetInitialPrm(InitialParamDataList[112], _data.EdgeSrchTouchRet2[6]);
            _data.EdgeSrchTouchRet2[7] = SetInitialPrm(InitialParamDataList[113], _data.EdgeSrchTouchRet2[7]);
            _data.EdgeSrchTouchRet2[8] = SetInitialPrm(InitialParamDataList[114], _data.EdgeSrchTouchRet2[8]);
            _data.EdgeSrchZDwnSpd = SetInitialPrm(InitialParamDataList[115], _data.EdgeSrchZDwnSpd);
            _data.TouchSenseTime = SetInitialPrm(InitialParamDataList[116], _data.TouchSenseTime);
            _data.RefTouchUpZ = SetInitialPrm(InitialParamDataList[117], _data.RefTouchUpZ);
            _data.PrcsRetSpdZ = SetInitialPrm(InitialParamDataList[118], _data.PrcsRetSpdZ);
            _data.EdgeSrchZUpSpd = SetInitialPrm(InitialParamDataList[119], _data.EdgeSrchZUpSpd);
            _data.ElctdClumpSpdS = SetInitialPrm(InitialParamDataList[120], _data.ElctdClumpSpdS);
            _data.BucklingUpOfsZ = SetInitialPrm(InitialParamDataList[121], _data.BucklingUpOfsZ);
            _data.BucklingUpSpdZ = SetInitialPrm(InitialParamDataList[122], _data.BucklingUpSpdZ);
            _data.BucklingRetry = SetInitialPrm(InitialParamDataList[123], _data.BucklingRetry);
            _data.AecEnable = SetInitialPrm(InitialParamDataList[124], _data.AecEnable);
            _data.Z20ErrOfs = SetInitialPrm(InitialParamDataList[125], _data.Z20ErrOfs);
            _data.McnType = SetInitialPrm(InitialParamDataList[126], _data.McnType);
            _data.GuideSensorDis = SetInitialPrm(InitialParamDataList[127], _data.GuideSensorDis);
            _data.CysCheckDis[0] = SetInitialPrm(InitialParamDataList[128], _data.CysCheckDis[0]);
            _data.CysCheckDis[1] = SetInitialPrm(InitialParamDataList[129], _data.CysCheckDis[1]);
            _data.CysCheckDis[2] = SetInitialPrm(InitialParamDataList[130], _data.CysCheckDis[2]);
            _data.CysCheckDis[3] = SetInitialPrm(InitialParamDataList[131], _data.CysCheckDis[3]);
            _data.AxAecActDis = SetInitialPrm(InitialParamDataList[132], _data.AxAecActDis);
            _data.AxBrakeEn = SetInitialPrm(InitialParamDataList[133], _data.AxBrakeEn);
            _data.BrakeTimer = SetInitialPrm(InitialParamDataList[134], _data.BrakeTimer);
            _data.TchErrCancelTime = SetInitialPrm(InitialParamDataList[135], _data.TchErrCancelTime);
            _data.CylPulseOut[0] = SetInitialPrm(InitialParamDataList[136], _data.CylPulseOut[0]);
            _data.CylPulseOut[1] = SetInitialPrm(InitialParamDataList[137], _data.CylPulseOut[1]);
            _data.CylPulseOut[2] = SetInitialPrm(InitialParamDataList[138], _data.CylPulseOut[2]);
            _data.CylPulseOut[3] = SetInitialPrm(InitialParamDataList[139], _data.CylPulseOut[3]);
            _data.CylPulseTime = SetInitialPrm(InitialParamDataList[140], _data.CylPulseTime);
            _data.PrcsFirstP99xDis = SetInitialPrm(InitialParamDataList[141], _data.PrcsFirstP99xDis);
            _data.LeaveZrnFin = SetInitialPrm(InitialParamDataList[142], _data.LeaveZrnFin);
            _data.EdgeSrchMaxMinUse = SetInitialPrm(InitialParamDataList[143], _data.EdgeSrchMaxMinUse);
            _data.EdgeSrchOldMcnComp = SetInitialPrm(InitialParamDataList[144], _data.EdgeSrchOldMcnComp);
            _data.CylSelBfrEDeplAec = SetInitialPrm(InitialParamDataList[145], _data.CylSelBfrEDeplAec);
            _data.CylSelAftEDeplAec = SetInitialPrm(InitialParamDataList[146], _data.CylSelAftEDeplAec);
            _data.CysOffChkDis[0] = SetInitialPrm(InitialParamDataList[147], _data.CysOffChkDis[0]);
            _data.CysOffChkDis[1] = SetInitialPrm(InitialParamDataList[148], _data.CysOffChkDis[1]);
            _data.CysOffChkDis[2] = SetInitialPrm(InitialParamDataList[149], _data.CysOffChkDis[2]);
            _data.CysOffChkDis[3] = SetInitialPrm(InitialParamDataList[150], _data.CysOffChkDis[3]);
            _data.SvPrcsStpRetOfs = SetInitialPrm(InitialParamDataList[151], _data.SvPrcsStpRetOfs);
            _data.SvPrcsRetOfs = SetInitialPrm(InitialParamDataList[152], _data.SvPrcsRetOfs);
            _data.SvPrcsRetSpd = SetInitialPrm(InitialParamDataList[153], _data.SvPrcsRetSpd);
            _data.AxServoPrcs = SetInitialPrm(InitialParamDataList[154], _data.AxServoPrcs);
            _data.CysLatch[0] = SetInitialPrm(InitialParamDataList[155], _data.CysLatch[0]);
            _data.CysLatch[1] = SetInitialPrm(InitialParamDataList[156], _data.CysLatch[1]);
            _data.CysLatch[2] = SetInitialPrm(InitialParamDataList[157], _data.CysLatch[2]);
            _data.CysLatch[3] = SetInitialPrm(InitialParamDataList[158], _data.CysLatch[3]);
            _data.InstLastElctdFlg = SetInitialPrm(InitialParamDataList[159], _data.InstLastElctdFlg);
            _data.SpinMaxSpd[0] = SetInitialPrm(InitialParamDataList[160], _data.SpinMaxSpd[0]);
            _data.SpinMaxSpd[1] = SetInitialPrm(InitialParamDataList[161], _data.SpinMaxSpd[1]);
            _data.SpinMaxSpd[2] = SetInitialPrm(InitialParamDataList[162], _data.SpinMaxSpd[2]);
            _data.SpinMaxSpd[3] = SetInitialPrm(InitialParamDataList[163], _data.SpinMaxSpd[3]);
            _data.SpinMaxSpd[4] = SetInitialPrm(InitialParamDataList[164], _data.SpinMaxSpd[4]);
            _data.SpinMaxSpd[5] = SetInitialPrm(InitialParamDataList[165], _data.SpinMaxSpd[5]);
            _data.SpinMaxSpd[6] = SetInitialPrm(InitialParamDataList[166], _data.SpinMaxSpd[6]);
            _data.SpinMaxSpd[7] = SetInitialPrm(InitialParamDataList[167], _data.SpinMaxSpd[7]);
            _data.SpinMaxSpd[8] = SetInitialPrm(InitialParamDataList[168], _data.SpinMaxSpd[8]);
            _data.PrcsSkipStopCys = SetInitialPrm(InitialParamDataList[169], _data.PrcsSkipStopCys);
            _data.DischgTchErrEn = SetInitialPrm(InitialParamDataList[170], _data.DischgTchErrEn);
            _data.CFeedTable[0] = SetInitialPrm(InitialParamDataList[171], _data.CFeedTable[0]);
            _data.CFeedTable[1] = SetInitialPrm(InitialParamDataList[172], _data.CFeedTable[1]);
            _data.CFeedTable[2] = SetInitialPrm(InitialParamDataList[173], _data.CFeedTable[2]);
            _data.CFeedTable[3] = SetInitialPrm(InitialParamDataList[174], _data.CFeedTable[3]);
            _data.CFeedTable[4] = SetInitialPrm(InitialParamDataList[175], _data.CFeedTable[4]);
            _data.CFeedTable[5] = SetInitialPrm(InitialParamDataList[176], _data.CFeedTable[5]);
            _data.CFeedTable[6] = SetInitialPrm(InitialParamDataList[177], _data.CFeedTable[6]);
            _data.CFeedTable[7] = SetInitialPrm(InitialParamDataList[178], _data.CFeedTable[7]);
            _data.CFeedTable[8] = SetInitialPrm(InitialParamDataList[179], _data.CFeedTable[8]);
            _data.CFeedTable[9] = SetInitialPrm(InitialParamDataList[180], _data.CFeedTable[9]);
            _data.CFeedTable[10] = SetInitialPrm(InitialParamDataList[181], _data.CFeedTable[10]);
            _data.CFeedTable[11] = SetInitialPrm(InitialParamDataList[182], _data.CFeedTable[11]);
            _data.CFeedTable[12] = SetInitialPrm(InitialParamDataList[183], _data.CFeedTable[12]);
            _data.CFeedTable[13] = SetInitialPrm(InitialParamDataList[184], _data.CFeedTable[13]);
            _data.CFeedTable[14] = SetInitialPrm(InitialParamDataList[185], _data.CFeedTable[14]);
            _data.CFeedTable[15] = SetInitialPrm(InitialParamDataList[186], _data.CFeedTable[15]);
            _data.GuideThroughChkDis = SetInitialPrm(InitialParamDataList[187], _data.GuideThroughChkDis);
            _data.Z2ndSoftLimMSelCys = SetInitialPrm(InitialParamDataList[188], _data.Z2ndSoftLimMSelCys);
            _data.Z2ndSoftLimM = SetInitialPrm(InitialParamDataList[189], _data.Z2ndSoftLimM);
            _data.GuidChgCylEn = SetInitialPrm(InitialParamDataList[190], _data.GuidChgCylEn);
            _data.GuidChgCysSel = SetInitialPrm(InitialParamDataList[191], _data.GuidChgCysSel);
            _data.GuidHolderEscOfs[0] = SetInitialPrm(InitialParamDataList[192], _data.GuidHolderEscOfs[0]);
            _data.GuidHolderEscOfs[1] = SetInitialPrm(InitialParamDataList[193], _data.GuidHolderEscOfs[1]);
            _data.GuidHolderEscOfs[2] = SetInitialPrm(InitialParamDataList[194], _data.GuidHolderEscOfs[2]);
            _data.GuidHolderEscOfs[3] = SetInitialPrm(InitialParamDataList[195], _data.GuidHolderEscOfs[3]);
            _data.GuidHolderEscOfs[4] = SetInitialPrm(InitialParamDataList[196], _data.GuidHolderEscOfs[4]);
            _data.GuidHolderEscOfs[5] = SetInitialPrm(InitialParamDataList[197], _data.GuidHolderEscOfs[5]);
            _data.GuidHolderEscOfs[6] = SetInitialPrm(InitialParamDataList[198], _data.GuidHolderEscOfs[6]);
            _data.GuidHolderEscOfs[7] = SetInitialPrm(InitialParamDataList[199], _data.GuidHolderEscOfs[7]);
            _data.GuidHolderEscOfs[8] = SetInitialPrm(InitialParamDataList[200], _data.GuidHolderEscOfs[8]);
            _data.GuidHolderArmDis = SetInitialPrm(InitialParamDataList[201], _data.GuidHolderArmDis);
            _data.TouchSenseISetEn = SetInitialPrm(InitialParamDataList[202], _data.TouchSenseISetEn);
            _data.CylSelPatRndStp = SetInitialPrm(InitialParamDataList[203], _data.CylSelPatRndStp);
            _data.PatRndStpOnlyElctdDep = SetInitialPrm(InitialParamDataList[204], _data.PatRndStpOnlyElctdDep);
            _data.CylSelThrghDetect = SetInitialPrm(InitialParamDataList[205], _data.CylSelThrghDetect);
            _data.RefSpdSelThrghDetect = SetInitialPrm(InitialParamDataList[206], _data.RefSpdSelThrghDetect);
            _data.PerInitThrghDetect = SetInitialPrm(InitialParamDataList[207], _data.PerInitThrghDetect);
            _data.PerInitThDetectEdge = SetInitialPrm(InitialParamDataList[208], _data.PerInitThDetectEdge);
            _data.AvTimInitThDetectEdge = SetInitialPrm(InitialParamDataList[209], _data.AvTimInitThDetectEdge);
            _data.RefSpdIgnHPerInit = SetInitialPrm(InitialParamDataList[210], _data.RefSpdIgnHPerInit);
            _data.RefSpdIgnLPerInit = SetInitialPrm(InitialParamDataList[211], _data.RefSpdIgnLPerInit);
            _data.RefSpdAvTimInit = SetInitialPrm(InitialParamDataList[212], _data.RefSpdAvTimInit);
            _data.PreElctdChgLmtPos = SetInitialPrm(InitialParamDataList[213], _data.PreElctdChgLmtPos);
            _data.ElctdExchMovOrder[0] = SetInitialPrm(InitialParamDataList[214], _data.ElctdExchMovOrder[0]);
            _data.ElctdExchMovOrder[1] = SetInitialPrm(InitialParamDataList[215], _data.ElctdExchMovOrder[1]);
            _data.ElctdExchMovOrder[2] = SetInitialPrm(InitialParamDataList[216], _data.ElctdExchMovOrder[2]);
            _data.ElctdExchMovOrder[3] = SetInitialPrm(InitialParamDataList[217], _data.ElctdExchMovOrder[3]);
            _data.ElctdExchMovOrder[4] = SetInitialPrm(InitialParamDataList[218], _data.ElctdExchMovOrder[4]);
            _data.ElctdExchMovOrder[5] = SetInitialPrm(InitialParamDataList[219], _data.ElctdExchMovOrder[5]);
            _data.ElctdExchMovOrder[6] = SetInitialPrm(InitialParamDataList[220], _data.ElctdExchMovOrder[6]);
            _data.ElctdExchMovOrder[7] = SetInitialPrm(InitialParamDataList[221], _data.ElctdExchMovOrder[7]);
            _data.ElctdExchMovOrder[8] = SetInitialPrm(InitialParamDataList[222], _data.ElctdExchMovOrder[8]);
            _data.CylSelInElctdPrcs = SetInitialPrm(InitialParamDataList[223], _data.CylSelInElctdPrcs);
            _data.PatliteMode = SetInitialPrm(InitialParamDataList[224], _data.PatliteMode);
            _data.AxVirActDis = SetInitialPrm(InitialParamDataList[225], _data.AxVirActDis);
            _data.SvPrcsInitialTime = SetInitialPrm(InitialParamDataList[226], _data.SvPrcsInitialTime);
            _data.ThinElctdISetPumpOff = SetInitialPrm(InitialParamDataList[227], _data.ThinElctdISetPumpOff);
            _data.AecWRefSel = SetInitialPrm(InitialParamDataList[228], _data.AecWRefSel);
            _data.GuideThroughChkPNo = SetInitialPrm(InitialParamDataList[229], _data.GuideThroughChkPNo);
            _data.InitialSetPno[0] = SetInitialPrm(InitialParamDataList[230], _data.InitialSetPno[0]);
            _data.InitialSetPno[1] = SetInitialPrm(InitialParamDataList[231], _data.InitialSetPno[1]);
            _data.InitialSetPno[2] = SetInitialPrm(InitialParamDataList[232], _data.InitialSetPno[2]);
            _data.InitialSetPno[3] = SetInitialPrm(InitialParamDataList[233], _data.InitialSetPno[3]);
            _data.InitialSetPno[4] = SetInitialPrm(InitialParamDataList[234], _data.InitialSetPno[4]);
            _data.InitialSetPno[5] = SetInitialPrm(InitialParamDataList[235], _data.InitialSetPno[5]);
            _data.ElctdChkOfsZ2[0] = SetInitialPrm(InitialParamDataList[236], _data.ElctdChkOfsZ2[0]);
            _data.ElctdChkOfsZ2[1] = SetInitialPrm(InitialParamDataList[237], _data.ElctdChkOfsZ2[1]);
            _data.ElctdChkOfsZ2[2] = SetInitialPrm(InitialParamDataList[238], _data.ElctdChkOfsZ2[2]);
            _data.ElctdChkOfsZ2[3] = SetInitialPrm(InitialParamDataList[239], _data.ElctdChkOfsZ2[3]);
            _data.ElctdChkOfsZ2[4] = SetInitialPrm(InitialParamDataList[240], _data.ElctdChkOfsZ2[4]);
            _data.ElctdChkOfsZ2[5] = SetInitialPrm(InitialParamDataList[241], _data.ElctdChkOfsZ2[5]);
            _data.GuideThroughChkPNoPat[0] = SetInitialPrm(InitialParamDataList[242], _data.GuideThroughChkPNoPat[0]);
            _data.GuideThroughChkPNoPat[1] = SetInitialPrm(InitialParamDataList[243], _data.GuideThroughChkPNoPat[1]);
            _data.GuideThroughChkPNoPat[2] = SetInitialPrm(InitialParamDataList[244], _data.GuideThroughChkPNoPat[2]);
            _data.GuideThroughChkPNoPat[3] = SetInitialPrm(InitialParamDataList[245], _data.GuideThroughChkPNoPat[3]);
            _data.GuideThroughChkPNoPat[4] = SetInitialPrm(InitialParamDataList[246], _data.GuideThroughChkPNoPat[4]);
            _data.GuideThroughChkPNoPat[5] = SetInitialPrm(InitialParamDataList[247], _data.GuideThroughChkPNoPat[5]);
            _data.EUninstColReclampDis = SetInitialPrm(InitialParamDataList[248], _data.EUninstColReclampDis);
            _data.AvTimInitThrghDetect = SetInitialPrm(InitialParamDataList[249], _data.AvTimInitThrghDetect);
            _data.EdgeLSrchTouchRet = SetInitialPrm(InitialParamDataList[250], _data.EdgeLSrchTouchRet);
            _data.EdgeLSrchRetSpd = SetInitialPrm(InitialParamDataList[251], _data.EdgeLSrchRetSpd);
            _data.InitPrmMacVal[0] = SetInitialPrm(InitialParamDataList[252], _data.InitPrmMacVal[0]);
            _data.InitPrmMacVal[1] = SetInitialPrm(InitialParamDataList[253], _data.InitPrmMacVal[1]);
            _data.InitPrmMacVal[2] = SetInitialPrm(InitialParamDataList[254], _data.InitPrmMacVal[2]);
            _data.InitPrmMacVal[3] = SetInitialPrm(InitialParamDataList[255], _data.InitPrmMacVal[3]);
            _data.InitPrmMacVal[4] = SetInitialPrm(InitialParamDataList[256], _data.InitPrmMacVal[4]);
            _data.InitPrmMacVal[5] = SetInitialPrm(InitialParamDataList[257], _data.InitPrmMacVal[5]);
            _data.InitPrmMacVal[6] = SetInitialPrm(InitialParamDataList[258], _data.InitPrmMacVal[6]);
            _data.InitPrmMacVal[7] = SetInitialPrm(InitialParamDataList[259], _data.InitPrmMacVal[7]);
            _data.InitPrmMacVal[8] = SetInitialPrm(InitialParamDataList[260], _data.InitPrmMacVal[8]);
            _data.InitPrmMacVal[9] = SetInitialPrm(InitialParamDataList[261], _data.InitPrmMacVal[9]);
            _data.NrmElctdPrcsBuckling = SetInitialPrm(InitialParamDataList[262], _data.NrmElctdPrcsBuckling);
            _data.NrmElctdGuideBuckling = SetInitialPrm(InitialParamDataList[263], _data.NrmElctdGuideBuckling);
            _data.RotAxMovMode = SetInitialPrm(InitialParamDataList[264], _data.RotAxMovMode);
            _data.AutoModeOutSel = SetInitialPrm(InitialParamDataList[265], _data.AutoModeOutSel);
            _data.CylBitRstPlsCont[0] = SetInitialPrm(InitialParamDataList[266], _data.CylBitRstPlsCont[0]);
            _data.CylBitRstPlsCont[1] = SetInitialPrm(InitialParamDataList[267], _data.CylBitRstPlsCont[1]);
            _data.CylBitRstPlsCont[2] = SetInitialPrm(InitialParamDataList[268], _data.CylBitRstPlsCont[2]);
            _data.CylBitRstPlsCont[3] = SetInitialPrm(InitialParamDataList[269], _data.CylBitRstPlsCont[3]);
            _data.PrcsTmoutChkEndSel = SetInitialPrm(InitialParamDataList[270], _data.PrcsTmoutChkEndSel);
            _data.MountedSF02FX = SetInitialPrm(InitialParamDataList[271], _data.MountedSF02FX);
            _data.EIFErr1MaskSet = SetInitialPrm(InitialParamDataList[272], _data.EIFErr1MaskSet);
            _data.EIFErr2MaskSet = SetInitialPrm(InitialParamDataList[273], _data.EIFErr2MaskSet);
            _data.Fanstop10DlyTim = SetInitialPrm(InitialParamDataList[274], _data.Fanstop10DlyTim);
            _data.Fanstop20DlyTim = SetInitialPrm(InitialParamDataList[275], _data.Fanstop20DlyTim);
            _data.EIFErr1Action[0] = SetInitialPrm(InitialParamDataList[276], _data.EIFErr1Action[0]);
            _data.EIFErr1Action[1] = SetInitialPrm(InitialParamDataList[277], _data.EIFErr1Action[1]);
            _data.EIFErr1Action[2] = SetInitialPrm(InitialParamDataList[278], _data.EIFErr1Action[2]);
            _data.EIFErr1Action[3] = SetInitialPrm(InitialParamDataList[279], _data.EIFErr1Action[3]);
            _data.EIFErr1Action[4] = SetInitialPrm(InitialParamDataList[280], _data.EIFErr1Action[4]);
            _data.EIFErr1Action[5] = SetInitialPrm(InitialParamDataList[281], _data.EIFErr1Action[5]);
            _data.EIFErr1Action[6] = SetInitialPrm(InitialParamDataList[282], _data.EIFErr1Action[6]);
            _data.EIFErr1Action[7] = SetInitialPrm(InitialParamDataList[283], _data.EIFErr1Action[7]);
            _data.EIFErr1Action[8] = SetInitialPrm(InitialParamDataList[284], _data.EIFErr1Action[8]);
            _data.EIFErr1Action[9] = SetInitialPrm(InitialParamDataList[285], _data.EIFErr1Action[9]);
            _data.EIFErr1Action[10] = SetInitialPrm(InitialParamDataList[286], _data.EIFErr1Action[10]);
            _data.EIFErr1Action[11] = SetInitialPrm(InitialParamDataList[287], _data.EIFErr1Action[11]);
            _data.EIFErr1Action[12] = SetInitialPrm(InitialParamDataList[288], _data.EIFErr1Action[12]);
            _data.EIFErr1Action[13] = SetInitialPrm(InitialParamDataList[289], _data.EIFErr1Action[13]);
            _data.EIFErr1Action[14] = SetInitialPrm(InitialParamDataList[290], _data.EIFErr1Action[14]);
            _data.EIFErr1Action[15] = SetInitialPrm(InitialParamDataList[291], _data.EIFErr1Action[15]);
            _data.EIFErr1Action[16] = SetInitialPrm(InitialParamDataList[292], _data.EIFErr1Action[16]);
            _data.EIFErr1Action[17] = SetInitialPrm(InitialParamDataList[293], _data.EIFErr1Action[17]);
            _data.EIFErr1Action[18] = SetInitialPrm(InitialParamDataList[294], _data.EIFErr1Action[18]);
            _data.EIFErr1Action[19] = SetInitialPrm(InitialParamDataList[295], _data.EIFErr1Action[19]);
            _data.EIFErr1Action[20] = SetInitialPrm(InitialParamDataList[296], _data.EIFErr1Action[20]);
            _data.EIFErr1Action[21] = SetInitialPrm(InitialParamDataList[297], _data.EIFErr1Action[21]);
            _data.EIFErr1Action[22] = SetInitialPrm(InitialParamDataList[298], _data.EIFErr1Action[22]);
            _data.EIFErr1Action[23] = SetInitialPrm(InitialParamDataList[299], _data.EIFErr1Action[23]);
            _data.EIFErr1Action[24] = SetInitialPrm(InitialParamDataList[300], _data.EIFErr1Action[24]);
            _data.EIFErr1Action[25] = SetInitialPrm(InitialParamDataList[301], _data.EIFErr1Action[25]);
            _data.EIFErr1Action[26] = SetInitialPrm(InitialParamDataList[302], _data.EIFErr1Action[26]);
            _data.EIFErr1Action[27] = SetInitialPrm(InitialParamDataList[303], _data.EIFErr1Action[27]);
            _data.EIFErr1Action[28] = SetInitialPrm(InitialParamDataList[304], _data.EIFErr1Action[28]);
            _data.EIFErr1Action[29] = SetInitialPrm(InitialParamDataList[305], _data.EIFErr1Action[29]);
            _data.EIFErr1Action[30] = SetInitialPrm(InitialParamDataList[306], _data.EIFErr1Action[30]);
            _data.EIFErr1Action[31] = SetInitialPrm(InitialParamDataList[307], _data.EIFErr1Action[31]);
            _data.EIFErr2Action[0] = SetInitialPrm(InitialParamDataList[308], _data.EIFErr2Action[0]);
            _data.EIFErr2Action[1] = SetInitialPrm(InitialParamDataList[309], _data.EIFErr2Action[1]);
            _data.EIFErr2Action[2] = SetInitialPrm(InitialParamDataList[310], _data.EIFErr2Action[2]);
            _data.EIFErr2Action[3] = SetInitialPrm(InitialParamDataList[311], _data.EIFErr2Action[3]);
            _data.EIFErr2Action[4] = SetInitialPrm(InitialParamDataList[312], _data.EIFErr2Action[4]);
            _data.EIFErr2Action[5] = SetInitialPrm(InitialParamDataList[313], _data.EIFErr2Action[5]);
            _data.EIFErr2Action[6] = SetInitialPrm(InitialParamDataList[314], _data.EIFErr2Action[6]);
            _data.EIFErr2Action[7] = SetInitialPrm(InitialParamDataList[315], _data.EIFErr2Action[7]);
            _data.EIFErr2Action[8] = SetInitialPrm(InitialParamDataList[316], _data.EIFErr2Action[8]);
            _data.EIFErr2Action[9] = SetInitialPrm(InitialParamDataList[317], _data.EIFErr2Action[9]);
            _data.EIFErr2Action[10] = SetInitialPrm(InitialParamDataList[318], _data.EIFErr2Action[10]);
            _data.EIFErr2Action[11] = SetInitialPrm(InitialParamDataList[319], _data.EIFErr2Action[11]);
            _data.EIFErr2Action[12] = SetInitialPrm(InitialParamDataList[320], _data.EIFErr2Action[12]);
            _data.EIFErr2Action[13] = SetInitialPrm(InitialParamDataList[321], _data.EIFErr2Action[13]);
            _data.EIFErr2Action[14] = SetInitialPrm(InitialParamDataList[322], _data.EIFErr2Action[14]);
            _data.EIFErr2Action[15] = SetInitialPrm(InitialParamDataList[323], _data.EIFErr2Action[15]);
            _data.EIFErr2Action[16] = SetInitialPrm(InitialParamDataList[324], _data.EIFErr2Action[16]);
            _data.EIFErr2Action[17] = SetInitialPrm(InitialParamDataList[325], _data.EIFErr2Action[17]);
            _data.EIFErr2Action[18] = SetInitialPrm(InitialParamDataList[326], _data.EIFErr2Action[18]);
            _data.EIFErr2Action[19] = SetInitialPrm(InitialParamDataList[327], _data.EIFErr2Action[19]);
            _data.EIFErr2Action[20] = SetInitialPrm(InitialParamDataList[328], _data.EIFErr2Action[20]);
            _data.EIFErr2Action[21] = SetInitialPrm(InitialParamDataList[329], _data.EIFErr2Action[21]);
            _data.EIFErr2Action[22] = SetInitialPrm(InitialParamDataList[330], _data.EIFErr2Action[22]);
            _data.EIFErr2Action[23] = SetInitialPrm(InitialParamDataList[331], _data.EIFErr2Action[23]);
            _data.EIFErr2Action[24] = SetInitialPrm(InitialParamDataList[332], _data.EIFErr2Action[24]);
            _data.EIFErr2Action[25] = SetInitialPrm(InitialParamDataList[333], _data.EIFErr2Action[25]);
            _data.EIFErr2Action[26] = SetInitialPrm(InitialParamDataList[334], _data.EIFErr2Action[26]);
            _data.EIFErr2Action[27] = SetInitialPrm(InitialParamDataList[335], _data.EIFErr2Action[27]);
            _data.EIFErr2Action[28] = SetInitialPrm(InitialParamDataList[336], _data.EIFErr2Action[28]);
            _data.EIFErr2Action[29] = SetInitialPrm(InitialParamDataList[337], _data.EIFErr2Action[29]);
            _data.EIFErr2Action[30] = SetInitialPrm(InitialParamDataList[338], _data.EIFErr2Action[30]);
            _data.EIFErr2Action[31] = SetInitialPrm(InitialParamDataList[339], _data.EIFErr2Action[31]);
            _data.MountedBP20 = SetInitialPrm(InitialParamDataList[340], _data.MountedBP20);
            _data.MountedIPEnhance = SetInitialPrm(InitialParamDataList[341], _data.MountedIPEnhance);
            _data.MountedACPower = SetInitialPrm(InitialParamDataList[342], _data.MountedACPower);
            _data.MountedNanoPulse = SetInitialPrm(InitialParamDataList[343], _data.MountedNanoPulse);
            _data.MountedHVOverlay = SetInitialPrm(InitialParamDataList[344], _data.MountedHVOverlay);
            _data.MountedTouchProbe = SetInitialPrm(InitialParamDataList[345], _data.MountedTouchProbe);
            _data.VSLvlSetting = SetInitialPrm(InitialParamDataList[346], _data.VSLvlSetting);
            _data.VSPKLvlSetting = SetInitialPrm(InitialParamDataList[347], _data.VSPKLvlSetting);
            _data.CntactSenseLvlSetting = SetInitialPrm(InitialParamDataList[348], _data.CntactSenseLvlSetting);
            _data.HPJogOvr[0] = SetInitialPrm(InitialParamDataList[349], _data.HPJogOvr[0]);
            _data.HPJogOvr[1] = SetInitialPrm(InitialParamDataList[350], _data.HPJogOvr[1]);
            _data.HPJogOvr[2] = SetInitialPrm(InitialParamDataList[351], _data.HPJogOvr[2]);
            _data.HPJogOvr[3] = SetInitialPrm(InitialParamDataList[352], _data.HPJogOvr[3]);
            _data.PLCShutDwonAuthEn = SetInitialPrm(InitialParamDataList[353], _data.PLCShutDwonAuthEn);
            _data.EX1E321Setting[0] = SetInitialPrm(InitialParamDataList[354], _data.EX1E321Setting[0]);
            _data.EX1E321Setting[1] = SetInitialPrm(InitialParamDataList[355], _data.EX1E321Setting[1]);
            _data.EX1E321Setting[2] = SetInitialPrm(InitialParamDataList[356], _data.EX1E321Setting[2]);
            _data.EX1E321Setting[3] = SetInitialPrm(InitialParamDataList[357], _data.EX1E321Setting[3]);
            _data.EX1E321Setting[4] = SetInitialPrm(InitialParamDataList[358], _data.EX1E321Setting[4]);
            _data.EX1E321Setting[5] = SetInitialPrm(InitialParamDataList[359], _data.EX1E321Setting[5]);
            _data.EX1E321Setting[6] = SetInitialPrm(InitialParamDataList[360], _data.EX1E321Setting[6]);
            _data.EX1E321Setting[7] = SetInitialPrm(InitialParamDataList[361], _data.EX1E321Setting[7]);
            _data.EX1E321Setting[8] = SetInitialPrm(InitialParamDataList[362], _data.EX1E321Setting[8]);
            _data.EX1E321Setting[9] = SetInitialPrm(InitialParamDataList[363], _data.EX1E321Setting[9]);
            _data.EX1E321Setting[10] = SetInitialPrm(InitialParamDataList[364], _data.EX1E321Setting[10]);
            _data.EX1E321Setting[11] = SetInitialPrm(InitialParamDataList[365], _data.EX1E321Setting[11]);
            _data.CRS10DAMin = SetInitialPrm(InitialParamDataList[366], _data.CRS10DAMin);
            _data.CRS10DAMax = SetInitialPrm(InitialParamDataList[367], _data.CRS10DAMax);
            _data.CRS10DATable[0] = SetInitialPrm(InitialParamDataList[368], _data.CRS10DATable[0]);
            _data.CRS10DATable[1] = SetInitialPrm(InitialParamDataList[369], _data.CRS10DATable[1]);
            _data.CRS10DATable[2] = SetInitialPrm(InitialParamDataList[370], _data.CRS10DATable[2]);
            _data.CRS10DATable[3] = SetInitialPrm(InitialParamDataList[371], _data.CRS10DATable[3]);
            _data.CRS10DATable[4] = SetInitialPrm(InitialParamDataList[372], _data.CRS10DATable[4]);
            _data.CRS10DATable[5] = SetInitialPrm(InitialParamDataList[373], _data.CRS10DATable[5]);
            _data.CRS10DATable[6] = SetInitialPrm(InitialParamDataList[374], _data.CRS10DATable[6]);
            _data.CRS10DATable[7] = SetInitialPrm(InitialParamDataList[375], _data.CRS10DATable[7]);
            _data.CRS10DATable[8] = SetInitialPrm(InitialParamDataList[376], _data.CRS10DATable[8]);
            _data.CRS10DATable[9] = SetInitialPrm(InitialParamDataList[377], _data.CRS10DATable[9]);
            _data.CRS10DATable[10] = SetInitialPrm(InitialParamDataList[378], _data.CRS10DATable[10]);
            _data.CRS10DATable[11] = SetInitialPrm(InitialParamDataList[379], _data.CRS10DATable[11]);
            _data.CRS10DATable[12] = SetInitialPrm(InitialParamDataList[380], _data.CRS10DATable[12]);
            _data.CRS10DATable[13] = SetInitialPrm(InitialParamDataList[381], _data.CRS10DATable[13]);
            _data.CRS10DATable[14] = SetInitialPrm(InitialParamDataList[382], _data.CRS10DATable[14]);
            _data.CRS10DATable[15] = SetInitialPrm(InitialParamDataList[383], _data.CRS10DATable[15]);
            _data.SCDAMin = SetInitialPrm(InitialParamDataList[384], _data.SCDAMin);
            _data.SCDAMax = SetInitialPrm(InitialParamDataList[385], _data.SCDAMax);
            _data.SCDATable[0] = SetInitialPrm(InitialParamDataList[386], _data.SCDATable[0]);
            _data.SCDATable[1] = SetInitialPrm(InitialParamDataList[387], _data.SCDATable[1]);
            _data.SCDATable[2] = SetInitialPrm(InitialParamDataList[388], _data.SCDATable[2]);
            _data.SCDATable[3] = SetInitialPrm(InitialParamDataList[389], _data.SCDATable[3]);
            _data.SCDATable[4] = SetInitialPrm(InitialParamDataList[390], _data.SCDATable[4]);
            _data.SCDATable[5] = SetInitialPrm(InitialParamDataList[391], _data.SCDATable[5]);
            _data.SCDATable[6] = SetInitialPrm(InitialParamDataList[392], _data.SCDATable[6]);
            _data.SCDATable[7] = SetInitialPrm(InitialParamDataList[393], _data.SCDATable[7]);
            _data.SCDATable[8] = SetInitialPrm(InitialParamDataList[394], _data.SCDATable[8]);
            _data.SCDATable[9] = SetInitialPrm(InitialParamDataList[395], _data.SCDATable[9]);
            _data.SCDATable[10] = SetInitialPrm(InitialParamDataList[396], _data.SCDATable[10]);
            _data.SCDATable[11] = SetInitialPrm(InitialParamDataList[397], _data.SCDATable[11]);
            _data.SCDATable[12] = SetInitialPrm(InitialParamDataList[398], _data.SCDATable[12]);
            _data.SCDATable[13] = SetInitialPrm(InitialParamDataList[399], _data.SCDATable[13]);
            _data.SCDATable[14] = SetInitialPrm(InitialParamDataList[400], _data.SCDATable[14]);
            _data.SCDATable[15] = SetInitialPrm(InitialParamDataList[401], _data.SCDATable[15]);
            _data.SCDATable[16] = SetInitialPrm(InitialParamDataList[402], _data.SCDATable[16]);
            _data.SCDATable[17] = SetInitialPrm(InitialParamDataList[403], _data.SCDATable[17]);
            _data.SCDATable[18] = SetInitialPrm(InitialParamDataList[404], _data.SCDATable[18]);
            _data.SCDATable[19] = SetInitialPrm(InitialParamDataList[405], _data.SCDATable[19]);
            _data.SCDATable[20] = SetInitialPrm(InitialParamDataList[406], _data.SCDATable[20]);
            _data.SCDATable[21] = SetInitialPrm(InitialParamDataList[407], _data.SCDATable[21]);
            _data.SCDATable[22] = SetInitialPrm(InitialParamDataList[408], _data.SCDATable[22]);
            _data.SCDATable[23] = SetInitialPrm(InitialParamDataList[409], _data.SCDATable[23]);
            _data.SCDATable[24] = SetInitialPrm(InitialParamDataList[410], _data.SCDATable[24]);
            _data.SCDATable[25] = SetInitialPrm(InitialParamDataList[411], _data.SCDATable[25]);
            _data.SCDATable[26] = SetInitialPrm(InitialParamDataList[412], _data.SCDATable[26]);
            _data.SCDATable[27] = SetInitialPrm(InitialParamDataList[413], _data.SCDATable[27]);
            _data.SCDATable[28] = SetInitialPrm(InitialParamDataList[414], _data.SCDATable[28]);
            _data.SCDATable[29] = SetInitialPrm(InitialParamDataList[415], _data.SCDATable[29]);
            _data.SCDATable[30] = SetInitialPrm(InitialParamDataList[416], _data.SCDATable[30]);
            _data.SCDATable[31] = SetInitialPrm(InitialParamDataList[417], _data.SCDATable[31]);
            _data.SCDATable[32] = SetInitialPrm(InitialParamDataList[418], _data.SCDATable[32]);
            _data.SCDATable[33] = SetInitialPrm(InitialParamDataList[419], _data.SCDATable[33]);
            _data.SCDATable[34] = SetInitialPrm(InitialParamDataList[420], _data.SCDATable[34]);
            _data.SCDATable[35] = SetInitialPrm(InitialParamDataList[421], _data.SCDATable[35]);
            _data.SCDATable[36] = SetInitialPrm(InitialParamDataList[422], _data.SCDATable[36]);
            _data.SCDATable[37] = SetInitialPrm(InitialParamDataList[423], _data.SCDATable[37]);
            _data.SCDATable[38] = SetInitialPrm(InitialParamDataList[424], _data.SCDATable[38]);
            _data.SCDATable[39] = SetInitialPrm(InitialParamDataList[425], _data.SCDATable[39]);
            _data.SCDATable[40] = SetInitialPrm(InitialParamDataList[426], _data.SCDATable[40]);
            _data.SCDATable[41] = SetInitialPrm(InitialParamDataList[427], _data.SCDATable[41]);
            _data.SCDATable[42] = SetInitialPrm(InitialParamDataList[428], _data.SCDATable[42]);
            _data.SCDATable[43] = SetInitialPrm(InitialParamDataList[429], _data.SCDATable[43]);
            _data.SCDATable[44] = SetInitialPrm(InitialParamDataList[430], _data.SCDATable[44]);
            _data.SCDATable[45] = SetInitialPrm(InitialParamDataList[431], _data.SCDATable[45]);
            _data.SCDATable[46] = SetInitialPrm(InitialParamDataList[432], _data.SCDATable[46]);
            _data.SCDATable[47] = SetInitialPrm(InitialParamDataList[433], _data.SCDATable[47]);
            _data.SCDATable[48] = SetInitialPrm(InitialParamDataList[434], _data.SCDATable[48]);
            _data.SCDATable[49] = SetInitialPrm(InitialParamDataList[435], _data.SCDATable[49]);
            _data.SCDATable[50] = SetInitialPrm(InitialParamDataList[436], _data.SCDATable[50]);
            _data.SCDATable[51] = SetInitialPrm(InitialParamDataList[437], _data.SCDATable[51]);
            _data.SCDATable[52] = SetInitialPrm(InitialParamDataList[438], _data.SCDATable[52]);
            _data.SCDATable[53] = SetInitialPrm(InitialParamDataList[439], _data.SCDATable[53]);
            _data.SCDATable[54] = SetInitialPrm(InitialParamDataList[440], _data.SCDATable[54]);
            _data.SCDATable[55] = SetInitialPrm(InitialParamDataList[441], _data.SCDATable[55]);
            _data.SCDATable[56] = SetInitialPrm(InitialParamDataList[442], _data.SCDATable[56]);
            _data.SCDATable[57] = SetInitialPrm(InitialParamDataList[443], _data.SCDATable[57]);
            _data.SCDATable[58] = SetInitialPrm(InitialParamDataList[444], _data.SCDATable[58]);
            _data.SCDATable[59] = SetInitialPrm(InitialParamDataList[445], _data.SCDATable[59]);
            _data.SCDATable[60] = SetInitialPrm(InitialParamDataList[446], _data.SCDATable[60]);
            _data.SCDATable[61] = SetInitialPrm(InitialParamDataList[447], _data.SCDATable[61]);
            _data.SCDATable[62] = SetInitialPrm(InitialParamDataList[448], _data.SCDATable[62]);
            _data.SCDATable[63] = SetInitialPrm(InitialParamDataList[449], _data.SCDATable[63]);
            _data.CRSDAMin = SetInitialPrm(InitialParamDataList[450], _data.CRSDAMin);
            _data.CRSDAMax = SetInitialPrm(InitialParamDataList[451], _data.CRSDAMax);
            _data.CRSDATable[0] = SetInitialPrm(InitialParamDataList[452], _data.CRSDATable[0]);
            _data.CRSDATable[1] = SetInitialPrm(InitialParamDataList[453], _data.CRSDATable[1]);
            _data.CRSDATable[2] = SetInitialPrm(InitialParamDataList[454], _data.CRSDATable[2]);
            _data.CRSDATable[3] = SetInitialPrm(InitialParamDataList[455], _data.CRSDATable[3]);
            _data.CRSDATable[4] = SetInitialPrm(InitialParamDataList[456], _data.CRSDATable[4]);
            _data.CRSDATable[5] = SetInitialPrm(InitialParamDataList[457], _data.CRSDATable[5]);
            _data.CRSDATable[6] = SetInitialPrm(InitialParamDataList[458], _data.CRSDATable[6]);
            _data.CRSDATable[7] = SetInitialPrm(InitialParamDataList[459], _data.CRSDATable[7]);
            _data.CRSDATable[8] = SetInitialPrm(InitialParamDataList[460], _data.CRSDATable[8]);
            _data.CRSDATable[9] = SetInitialPrm(InitialParamDataList[461], _data.CRSDATable[9]);
            _data.CRSDATable[10] = SetInitialPrm(InitialParamDataList[462], _data.CRSDATable[10]);
            _data.CRSDATable[11] = SetInitialPrm(InitialParamDataList[463], _data.CRSDATable[11]);
            _data.CRSDATable[12] = SetInitialPrm(InitialParamDataList[464], _data.CRSDATable[12]);
            _data.CRSDATable[13] = SetInitialPrm(InitialParamDataList[465], _data.CRSDATable[13]);
            _data.CRSDATable[14] = SetInitialPrm(InitialParamDataList[466], _data.CRSDATable[14]);
            _data.CRSDATable[15] = SetInitialPrm(InitialParamDataList[467], _data.CRSDATable[15]);

            //基底クラスの書き込み処理
            return base.WriteData(_data);
        }


        private dynamic SetInitialPrm(string source, dynamic writeData)
        {
            Type wdata = writeData.GetType();
            try
            {
                //型に合った変換処理
                if (wdata == typeof(int) )
                {
                    //int
                    if (true != UIStaticMethods.IsNumeric(source))
                    {
                        return Convert.ToInt32(source, 16).ToString();
                    }
                    else
                    {
                        return (int.Parse(source));
                    }
                }
                else if(wdata == typeof(short))
                {
                    //short
                    if (true != UIStaticMethods.IsNumeric(source))
                    {
                        return Convert.ToInt16(source, 16).ToString();
                    }
                    else
                    {
                        return (short.Parse(source));
                    }
                }
                else if(wdata == typeof(ushort))
                {
                    //ushort
                    if (true != UIStaticMethods.IsNumeric(source))
                    {
                        return Convert.ToUInt16(source, 16).ToString();
                    }
                    else
                    {
                        return (ushort.Parse(source));
                    }
                }
                else if (wdata == typeof(uint))
                {
                    //uint
                    if (true != UIStaticMethods.IsNumeric(source))
                    {
                        return Convert.ToUInt32(source, 16).ToString();
                    }
                    else
                    {
                        return (uint.Parse(source));
                    }
                }
                else if (wdata == typeof(double))
                {
                    //double                
                    return (double.Parse(source));
                }
                else
                {
                    return writeData;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return writeData;
            }
        }
        /// <summary>インスタンスの破棄</summary>
		public new void Dispose()
        {
            InitialParamDataList.Clear();
            InitialParamDataList.TrimExcess();

            Dispose(true);
            GC.SuppressFinalize(this);    //  ファイナライザによるDispose()呼び出しの抑制。
        }

        /// <summary>インスタンスの破棄</summary>
        /// <param name="disposing">呼び出し元の判別
        ///     <list type="bullet" >
        ///         <item>true=Dispose()関数からの呼び出し。</item>
        ///         <item>false=ファイナライザによる呼び出し。</item>
        ///     </list>
        /// </param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (false == _disposed)
                {
                    if (true == disposing)
                    {
                        //  マネージリソースの解放
                    }
                    //  アンマネージリソースの解放
                    
                }
                _disposed = true;
            }
            finally
            {
                //  基底クラスのDispose()を確実に呼び出す。
                base.Dispose(disposing);
            }
        }
    }
}
