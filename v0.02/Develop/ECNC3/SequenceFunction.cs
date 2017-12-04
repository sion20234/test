using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECNC3.Models.McIf;
using System.Windows.Forms;

namespace ECNC3.Views
{
    /// <summary>
    /// 各動作処理用クラス
    /// </summary>
    internal class SequenceFunction : IDisposable
    {
        /// <summary>
        /// 動作パラメータ無
        /// </summary>
        internal SequenceFunction()
        {

        }

        internal SequenceFunction(int param)
        {
            SeqMode = param;
        }

        /// <summary>
        /// 軸移動用パラメータ有
        /// </summary>
        /// <param name="AxisInf">位置決め用パラメータクラス</param>
        internal SequenceFunction(List<Models.StructurePositioniingItem>AxisInf)
        {
            PosItemList = AxisInf;
        }
        //軸移動用パラメータ用リスト
        private List<Models.StructurePositioniingItem> PosItemList { get; set; }
        /// <summary>
        /// 動作条件用パラメータ
        /// </summary>
        public int SeqMode { get; set; }

        /// <summary>
        /// 各動作処理の実行
        /// </summary>
        /// <param name="Target">実行対象の動作名(列挙体メンバ)</param>
        /// <param name="RetValue">動作の戻り値</param>
        /// <remarks>動作項目が追加される場合は、Sequences列挙体メンバも追加する。</remarks>
        internal Enumeration.ResultCodes SequenceMonitoring(Sequences Target)
        {
            //outパラメータの初期値
            Enumeration.ResultCodes RetValue = 0;
            //条件分岐による動作処理
            switch (Target)
            {

            case Sequences.ProgramSelect:
                using (McReqProgramSelect ReqProgSel = new McReqProgramSelect())
                {
                    ReqProgSel.ProgramNumber = 1;
                    RetValue = ReqProgSel.Execute();
                }
                    break;

                case Sequences.ProgramStart:
                using (McReqProgramStart ReqProgStart = new McReqProgramStart())
                {
                    ReqProgStart.StartNNumber = (short)SeqMode;
                    RetValue = ReqProgStart.Execute();
                }
                    break;

                case Sequences.OriginAll:
                    using (Models.McIf.McReqReturnToOrigin RetOrigin = new McReqReturnToOrigin())
                    {
                        RetValue = RetOrigin.Execute();
                    }
                    break;

                case Sequences.ZOrigin:
                    using (McReqReturnToOrigin _zorigin = new McReqReturnToOrigin(Enumeration.AxisNumbers.Z))
                    {
                        RetValue = _zorigin.Execute();
                    }
                        break;

                case Sequences.AbsoluteMacMovePointToPoint:
                    using (McReqPositioningPointToPoint ReqPosPntToPnt = new McReqPositioningPointToPoint())
                    {
                        ReqPosPntToPnt.AxisX = PosItemList[0];
                        ReqPosPntToPnt.AxisY = PosItemList[1];
                        ReqPosPntToPnt.AxisW = PosItemList[2];
                        ReqPosPntToPnt.AxisZ = PosItemList[3];
                        ReqPosPntToPnt.AxisA = PosItemList[4];
                        ReqPosPntToPnt.AxisB = PosItemList[5];
                        ReqPosPntToPnt.AxisC = PosItemList[6];

                        ReqPosPntToPnt.CoordType = Enumeration.CoordTypes.Machine;

                        RetValue = ReqPosPntToPnt.Execute();
                    }
                    break;

                case Sequences.AbsoluteWorkMovePointToPoint:
                    using (McReqPositioningPointToPoint ReqPosPntToPnt = new McReqPositioningPointToPoint())
                    {
                        ReqPosPntToPnt.AxisX = PosItemList[0];
                        ReqPosPntToPnt.AxisY = PosItemList[1];
                        ReqPosPntToPnt.AxisW = PosItemList[2];
                        ReqPosPntToPnt.AxisZ = PosItemList[3];
                        ReqPosPntToPnt.AxisA = PosItemList[4];
                        ReqPosPntToPnt.AxisB = PosItemList[5];
                        ReqPosPntToPnt.AxisC = PosItemList[6];

                        ReqPosPntToPnt.CoordType = Enumeration.CoordTypes.Work;

                        RetValue = ReqPosPntToPnt.Execute();
                    }
                    break;

                case Sequences.IncMovePointToPoint:
                    using (McReqPositioningPointToPointInc ReqPosPntToPnt = new McReqPositioningPointToPointInc())
                    {
                        ReqPosPntToPnt.AxisX = PosItemList[0];
                        ReqPosPntToPnt.AxisY = PosItemList[1];
                        ReqPosPntToPnt.AxisW = PosItemList[2];
                        ReqPosPntToPnt.AxisZ = PosItemList[3];
                        ReqPosPntToPnt.AxisA = PosItemList[4];
                        ReqPosPntToPnt.AxisB = PosItemList[5];
                        ReqPosPntToPnt.AxisC = PosItemList[6];

                    ReqPosPntToPnt.CoordType = Enumeration.CoordTypes.NotSet;

                        RetValue = ReqPosPntToPnt.Execute();
                    }
                    break;

                case Sequences.WaxisUpperSavedAbsMacMovePntToPnt:
                    using (McReqPositioningPointToPointWAxisUpperLimitValid ReqPosPntToPntWUpper = new McReqPositioningPointToPointWAxisUpperLimitValid())
                    {
                        ReqPosPntToPntWUpper.AxisX = PosItemList[0];
                        ReqPosPntToPntWUpper.AxisY = PosItemList[1];
                        ReqPosPntToPntWUpper.AxisW = PosItemList[2];
                        ReqPosPntToPntWUpper.AxisZ = PosItemList[3];
                        ReqPosPntToPntWUpper.AxisA = PosItemList[4];
                        ReqPosPntToPntWUpper.AxisB = PosItemList[5];
                        ReqPosPntToPntWUpper.AxisC = PosItemList[6];

                        ReqPosPntToPntWUpper.CoordType = Enumeration.CoordTypes.Machine;

                        RetValue = ReqPosPntToPntWUpper.Execute();
                    }
                    break;

                case Sequences.WaxisUpperSavedAbsWorkMovePntToPnt:
                    using (McReqPositioningPointToPointWAxisUpperLimitValid ReqPosPntToPntWUpper = new McReqPositioningPointToPointWAxisUpperLimitValid())
                    {
                        ReqPosPntToPntWUpper.AxisX = PosItemList[0];
                        ReqPosPntToPntWUpper.AxisY = PosItemList[1];
                        ReqPosPntToPntWUpper.AxisW = PosItemList[2];
                        ReqPosPntToPntWUpper.AxisZ = PosItemList[3];
                        ReqPosPntToPntWUpper.AxisA = PosItemList[4];
                        ReqPosPntToPntWUpper.AxisB = PosItemList[5];
                        ReqPosPntToPntWUpper.AxisC = PosItemList[6];

                        ReqPosPntToPntWUpper.CoordType = Enumeration.CoordTypes.Work;

                        RetValue = ReqPosPntToPntWUpper.Execute();
                    }
                    break;

                case Sequences.GuideClamp:
                    using (McReqClumpGuide GuideClump = new McReqClumpGuide())
                    {
                        GuideClump.Clamped = true;
                        RetValue = GuideClump.Execute();
                    }
                    break;

                case Sequences.GuideUnClamp:
                    using (McReqClumpGuide GuideClump = new McReqClumpGuide())
                    {
                        GuideClump.Clamped = false;
                        RetValue = GuideClump.Execute();
                    }
                    break;

                case Sequences.SpindleClamp:
                    using (Models.McIf.McReqClumpSpindle SpindleClump = new McReqClumpSpindle())
                    {
                        SpindleClump.Clamped = true;
                        RetValue = SpindleClump.Execute();
                    }
                    break;

                case Sequences.SpindleUnClamp:
                    using (Models.McIf.McReqClumpSpindle SpindleClump = new McReqClumpSpindle())
                    {
                        SpindleClump.Clamped = false;
                        RetValue = SpindleClump.Execute();
                    }
                    break;

                case Sequences.FingerArmClamp:
                    using (Models.McIf.McReqEsfMoveFinger FingerArmClump = new McReqEsfMoveFinger())
                    {
                        FingerArmClump.FingerOpen = false;
                        RetValue = FingerArmClump.Execute();
                    }
                    break;

                case Sequences.FingerArmUnClamp:
                    using (Models.McIf.McReqEsfMoveFinger FingerArmUnClump = new McReqEsfMoveFinger())
                    {
                        FingerArmUnClump.FingerOpen = true;
                        RetValue = FingerArmUnClump.Execute();
                    }
                    break;

                case Sequences.FingerArmBack:
                    using (McReqEsfMoveArm ArmPos = new McReqEsfMoveArm())
                    {
                        ArmPos.Position = Enumeration.EsfArmPositions.Back;
                        RetValue = ArmPos.Execute();
                    }
                        break;

                case Sequences.FingerArmFront1:
                    using (McReqEsfMoveArm ArmPos = new McReqEsfMoveArm())
                    {
                        ArmPos.Position = Enumeration.EsfArmPositions.Middle;
                        RetValue = ArmPos.Execute();
                    }
                    break;

                case Sequences.FingerArmFront2:
                    using (McReqEsfMoveArm ArmPos = new McReqEsfMoveArm())
                    {
                        ArmPos.Position = Enumeration.EsfArmPositions.Foward;
                        RetValue = ArmPos.Execute();
                    }
                    break;

                case Sequences.EsfInstall:
                    using (Models.McIf.McReqElectrodeInsall EsfInst = new McReqElectrodeInsall())
                    {
                        EsfInst.ElectrodeNumber = (short)SeqMode;
                        EsfInst.Install = true;
                        RetValue = EsfInst.Execute();
                    }
                    break;

                case Sequences.EsfUnInstall:
                    using (Models.McIf.McReqElectrodeInsall EsfUnInst = new McReqElectrodeInsall())
                    {
                        EsfUnInst.ElectrodeNumber = (short)SeqMode;
                        EsfUnInst.Install = false;
                        RetValue = EsfUnInst.Execute();
                    }
                    break;

                case Sequences.GsfInstall:
                    using (Models.McIf.McReqGuideInsall GsfInst = new McReqGuideInsall())
                    {
                        GsfInst.GuideNumber = (short)SeqMode;
                        GsfInst.Install = true;
                        RetValue = GsfInst.Execute();
                    }
                    break;

                case Sequences.GsfUnInstall:
                    using (Models.McIf.McReqGuideInsall GsfUnInst = new McReqGuideInsall())
                    {
                        GsfUnInst.GuideNumber = (short)SeqMode;
                        GsfUnInst.Install = false;
                        RetValue = GsfUnInst.Execute();
                    }
                    break;

            case Sequences.DischargeOn:
                    using (McReqControllingDischarge ReqDischarge = new McReqControllingDischarge())
                    {
                        if (SeqMode == 0)
                        {
                            ReqDischarge.Enabled = false;
                        }
                        else
                        {
                            ReqDischarge.Enabled = true;
                        }
                        RetValue = ReqDischarge.Execute();
                    }
                    break;

                case Sequences.PompOn:
                    using (McReqControllingPump ReqPomp = new McReqControllingPump())
                    {
                        if (SeqMode == 0)
                        {
                            ReqPomp.Enabled = false;
                        }
                        else
                        {
                            ReqPomp.Enabled = true;
                        }
                        RetValue = ReqPomp.Execute();
                    }
                    break;

                case Sequences.SpindleOn:
                    using (McReqControllingSpin ReqSpin = new McReqControllingSpin())
                    {
                        switch (SeqMode)
                        {
                            case 0:
                                ReqSpin.SpinAction = Enumeration.SpinStates.Stop;
                                break;

                            case 1:
                                ReqSpin.SpinAction = Enumeration.SpinStates.Clockwise;
                                break;

                            case 2:
                                ReqSpin.SpinAction = Enumeration.SpinStates.Counterclockwise;
                                break;
                        }
                        RetValue = ReqSpin.Execute();
                    }
                    break;

                case Sequences.MagazineIncFront:
                    using (Models.McIf.McReqEsfMoveMagazine ReqEsfMagzInc = new Models.McIf.McReqEsfMoveMagazine())
                    {
                        ReqEsfMagzInc.MagazineNumber = 0;
                        RetValue = ReqEsfMagzInc.Execute();
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
