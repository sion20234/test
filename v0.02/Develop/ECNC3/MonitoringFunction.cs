using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECNC3.Models;

namespace ECNC3.Views
{
    /// <summary>
    /// 各動作条件監視用クラス
    /// </summary>
    /// <remarks>都度インスタンス化して使用する。</remarks>
    internal class MonitoringFunction : IDisposable
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        internal MonitoringFunction()
        {

        }

        internal int RetIntegerMonitoringResult(MonitorTargets Target)
        {
            switch (Target)
            {
                case MonitorTargets.Override:
                using (Models.McIf.McDatStatus McSta = new Models.McIf.McDatStatus())
                {
                    McSta.Read();
                    return McSta.Status.OverrideAsOverall;
                }

                case MonitorTargets.ElectrodeCount:
                    using (Models.McIf.McDatInitialPrm McIni = new Models.McIf.McDatInitialPrm())
                    {
                        McIni.Read();
                        return McIni.ElectrodeCount;
                    }

                case MonitorTargets.GuideCount:
                    using (Models.McIf.McDatInitialPrm McIni = new Models.McIf.McDatInitialPrm())
                    {
                        McIni.Read();
                        return McIni.GuideCount;
                    }

                default:
                    return 0;
            }
        }

        /// <summary>
        /// 座標の取得
        /// </summary>
        /// <param name="Target"></param>
        /// <returns></returns>
        internal Models.StructureAxisCoordinate AxisSequenceMonitoring(MonitorTargets Target)
        {
			Models.StructureAxisCoordinate StrAxis = null;// new Models.StructureAxisCoordinate();
            //条件分岐による状態取得処理
            switch (Target)
            {
                case MonitorTargets.WorkOrgPos:
                    using (Models.McIf.McDatWorkOrigin WorkOrg = new Models.McIf.McDatWorkOrigin())
                    {
                        WorkOrg.Read();
						StrAxis = WorkOrg.Coordinate.Clone() as StructureAxisCoordinate;
					}
					return StrAxis;

                //機械座標
                case MonitorTargets.MachineAxis:
                    using (Models.McIf.McDatStatus McState = new Models.McIf.McDatStatus())
                    {
                        //座標の読み出し
                        McState.Read();
                        //現座標を戻り値に格納
                        StrAxis = McState.Status.CoordinateAsAbsReg.Clone() as StructureAxisCoordinate;
						//インスタンス破棄
						McState.Status.CoordinateAsAbsReg.Dispose();
                    }
                    return StrAxis;

                //ワーク座標
                case MonitorTargets.WorkAxis:
                    using (Models.McIf.McDatStatus McState = new Models.McIf.McDatStatus())
                    {
                        //座標の読み出し
                        McState.Read();
                        //現座標を戻り値に格納
                        StrAxis = McState.Status.CoordinateAsPosReg.Clone() as StructureAxisCoordinate;
                        //インスタンス破棄
                        McState.Status.CoordinateAsPosReg.Dispose();
                    }
                    return StrAxis;

                default:
                    StrAxis = new Models.StructureAxisCoordinate();
                    return StrAxis;


            }
        }

		/// <summary>
		/// 各動作条件の状態取得
		/// </summary>
		/// <param name="Target">取得対象の条件名(列挙体メンバ)</param>
		/// <param name="RetValue">動作条件のbool値</param>
		/// <remarks>状態取得の項目が追加される場合は、MonitorTargets列挙体メンバも追加する。
		/// <br>2016/08/29 v0.01	Takano	McStatusクラス破棄により各個のクラスを呼び出すように修正。</br>
		/// </remarks>
		internal bool SequenceConditionMonitoring(MonitorTargets Target)
        {
            using (Models.McIf.McDatIOData McIO = new Models.McIf.McDatIOData())
            {   using (Models.McIf.McDatStatus McSta = new Models.McIf.McDatStatus())
                {   using (Models.McIf.McDatProcessCondition McPcond = new Models.McIf.McDatProcessCondition())
                    {
                        
                        McIO.Read();
                        McSta.Read();
                        //outパラメータの初期値
                        bool RetValue = true;
                        //条件分岐による状態取得処理
                        switch (Target)
                        {
                            case MonitorTargets.StartSwBtn:
                                RetValue = McIO.StartButton;
                                break;

                            case MonitorTargets.SequenceEnd:
                                RetValue = McSta.Status.CompletedSequence;
                                break;

                            case MonitorTargets.SpindleOn:

                                switch (McPcond.SpinOut)
                                {
                                    case Enumeration.SpinStates.Clockwise:
                                        RetValue = true;
                                        break;

                                    case Enumeration.SpinStates.Counterclockwise:
                                        RetValue = true;
                                        break;

                                    case Enumeration.SpinStates.Stop:
                                        RetValue = false;
                                        break;
                                }
                                break;

                            case MonitorTargets.PumpOn:
                                    RetValue = McPcond.PumpOut;
                                break;

                            case MonitorTargets.DischargeOn:
                                    RetValue = McPcond.Discharge;
                                break;

                            case MonitorTargets.FgEnd:
                                    RetValue = McSta.Status.CompletedFg;
                                break;

                            case MonitorTargets.GuideHolderClampOn:
                                RetValue = McIO.GuideClamp;//falseを実行処理関数に置き換える。
                                break;

                            case MonitorTargets.ColletClampOn:
                                RetValue = McIO.ColletClamp;//falseを実行処理関数に置き換える。
                                break;

                            case MonitorTargets.GuideNumber:
                                RetValue = true;//falseを実行処理関数に置き換える。
                                break;

                            case MonitorTargets.ElectrodeNumber:
                                RetValue = true;//falseを実行処理関数に置き換える。
                                break;

                            case MonitorTargets.GsfEnable:
                                RetValue = true;//falseを実行処理関数に置き換える。
                                break;

                            case MonitorTargets.EsfEnable:
                                RetValue = true;//falseを実行処理関数に置き換える。
                                break;

                            case MonitorTargets.CompletedOrigin:
                                RetValue = true;//falseを実行処理関数に置き換える。
                                break;

                            case MonitorTargets.AaxisEnable:
                                RetValue = true;//falseを実行処理関数に置き換える。
                                break;

                            case MonitorTargets.BaxisEnable:
                                RetValue = true;//falseを実行処理関数に置き換える。
                                break;

                            case MonitorTargets.CaxisEnable:
                                RetValue = true;//falseを実行処理関数に置き換える。
                                break;

                            case MonitorTargets.ContactSensing:
                                    RetValue = McSta.Status.TouchSensor;
                                break;

                            case MonitorTargets.InitialSet:
                                    RetValue = McPcond.InitialSet;
                                break;

                            case MonitorTargets.Buzzer:
                                    RetValue = McIO.Buzzer;
                                break;

                            case MonitorTargets.ReturnCmd:
                                RetValue = McPcond.RequestSendingBack;
                                break;

                            
                        }
                        return RetValue;
                    }
                }
            }
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
