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
    public class UIDatServoPrm : McDatServoParameter, IEcnc3McDatReadWrite
    {
        public UIDatServoPrm()
        {
        }
        /// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
        public Models.StructureAxisParameterList ServoParamDataList = new Models.StructureAxisParameterList();
        PARAMETER_DATA _data = new PARAMETER_DATA();
        public const int listCtUpper = 9;
        public new ResultCodes Read()
        {
            ResultCodes ret = ResultCodes.NotFound;
            ret = base.ReadData(out _data);
            for(int readCt = 0; readCt < listCtUpper; readCt++)
            {
                ServoParamDataList.Items[readCt].InPos = _data.AxisParam[readCt].InPos.ToString();
                ServoParamDataList.Items[readCt].ErMax = _data.AxisParam[readCt].ErMax.ToString();
                ServoParamDataList.Items[readCt].Ka = _data.AxisParam[readCt].Ka.ToString();
                ServoParamDataList.Items[readCt].SKa = _data.AxisParam[readCt].SKa.ToString();
                ServoParamDataList.Items[readCt].Dx = _data.AxisParam[readCt].Dx.ToString();
                ServoParamDataList.Items[readCt].PtpFeed = _data.AxisParam[readCt].PtpFeed.ToString();
                ServoParamDataList.Items[readCt].JogFeed = _data.AxisParam[readCt].JogFeed.ToString();
                ServoParamDataList.Items[readCt].SoftLimP = _data.AxisParam[readCt].SoftLimP.ToString();
                ServoParamDataList.Items[readCt].SoftLimM = _data.AxisParam[readCt].SoftLimM.ToString();
                ServoParamDataList.Items[readCt].OrgDir = _data.AxisParam[readCt].OrgDir.ToString();
                ServoParamDataList.Items[readCt].OrgOfs = _data.AxisParam[readCt].OrgOfs.ToString();
                ServoParamDataList.Items[readCt].OrgPos = _data.AxisParam[readCt].OrgPos.ToString();
                ServoParamDataList.Items[readCt].OrgFeed = _data.AxisParam[readCt].OrgFeed.ToString();
                ServoParamDataList.Items[readCt].AprFeed = _data.AxisParam[readCt].AprFeed.ToString();
                ServoParamDataList.Items[readCt].SrchFeed = _data.AxisParam[readCt].SrchFeed.ToString();
                ServoParamDataList.Items[readCt].OrgPri = _data.AxisParam[readCt].OrgPri.ToString();
                ServoParamDataList.Items[readCt].Homepos = _data.AxisParam[readCt].Homepos.ToString();
                ServoParamDataList.Items[readCt].Homepri = _data.AxisParam[readCt].Homepri.ToString();
                ServoParamDataList.Items[readCt].BackL = _data.AxisParam[readCt].BackL.ToString();
                ServoParamDataList.Items[readCt].Revise = _data.AxisParam[readCt].Revise.ToString();
                ServoParamDataList.Items[readCt].OrgCsetOfs = _data.AxisParam[readCt].OrgCsetOfs.ToString();
                ServoParamDataList.Items[readCt].handle_max = _data.AxisParam[readCt].handle_max.ToString();
                ServoParamDataList.Items[readCt].handle_ka = _data.AxisParam[readCt].handle_ka.ToString();
                ServoParamDataList.Items[readCt].PrcsKa = _data.AxisParam[readCt].PrcsKa.ToString();
                ServoParamDataList.Items[readCt].AecSoftLimP = _data.AxisParam[readCt].AecSoftLimP.ToString();
                ServoParamDataList.Items[readCt].AecSoftLimM = _data.AxisParam[readCt].AecSoftLimM.ToString();
            }
            ServoParamDataList.AxAecSoftLim = _data.AddParam.AxAecSoftLim.ToString();
            return ret;
        }

        public ResultCodes WriteAll()
        {
            //基底クラスへ書き込み用データを送る
            for(int writeCt = 0; writeCt < listCtUpper; writeCt++)
            {
                _data.AxisParam[writeCt].InPos = SetServoPrm(ServoParamDataList.Items[writeCt].InPos, _data.AxisParam[writeCt].InPos);
                _data.AxisParam[writeCt].ErMax = SetServoPrm(ServoParamDataList.Items[writeCt].ErMax, _data.AxisParam[writeCt].ErMax);
                _data.AxisParam[writeCt].Ka = SetServoPrm(ServoParamDataList.Items[writeCt].Ka, _data.AxisParam[writeCt].Ka);
                _data.AxisParam[writeCt].SKa = SetServoPrm(ServoParamDataList.Items[writeCt].SKa, _data.AxisParam[writeCt].SKa);
                _data.AxisParam[writeCt].Dx = SetServoPrm(ServoParamDataList.Items[writeCt].Dx, _data.AxisParam[writeCt].Dx);
                _data.AxisParam[writeCt].PtpFeed = SetServoPrm(ServoParamDataList.Items[writeCt].PtpFeed, _data.AxisParam[writeCt].PtpFeed);
                _data.AxisParam[writeCt].JogFeed = SetServoPrm(ServoParamDataList.Items[writeCt].JogFeed, _data.AxisParam[writeCt].JogFeed);
                _data.AxisParam[writeCt].SoftLimP = SetServoPrm(ServoParamDataList.Items[writeCt].SoftLimP, _data.AxisParam[writeCt].SoftLimP);
                _data.AxisParam[writeCt].SoftLimM = SetServoPrm(ServoParamDataList.Items[writeCt].SoftLimM, _data.AxisParam[writeCt].SoftLimM);
                _data.AxisParam[writeCt].OrgDir = SetServoPrm(ServoParamDataList.Items[writeCt].OrgDir, _data.AxisParam[writeCt].OrgDir);
                _data.AxisParam[writeCt].OrgOfs = SetServoPrm(ServoParamDataList.Items[writeCt].OrgOfs, _data.AxisParam[writeCt].OrgOfs);
                _data.AxisParam[writeCt].OrgPos = SetServoPrm(ServoParamDataList.Items[writeCt].OrgPos, _data.AxisParam[writeCt].OrgPos);
                _data.AxisParam[writeCt].OrgFeed = SetServoPrm(ServoParamDataList.Items[writeCt].OrgFeed, _data.AxisParam[writeCt].OrgFeed);
                _data.AxisParam[writeCt].AprFeed = SetServoPrm(ServoParamDataList.Items[writeCt].AprFeed, _data.AxisParam[writeCt].AprFeed);
                _data.AxisParam[writeCt].SrchFeed = SetServoPrm(ServoParamDataList.Items[writeCt].SrchFeed, _data.AxisParam[writeCt].SrchFeed);
                _data.AxisParam[writeCt].OrgPri = SetServoPrm(ServoParamDataList.Items[writeCt].OrgPri, _data.AxisParam[writeCt].OrgPri);
                _data.AxisParam[writeCt].Homepos = SetServoPrm(ServoParamDataList.Items[writeCt].Homepos, _data.AxisParam[writeCt].Homepos);
                _data.AxisParam[writeCt].Homepri = SetServoPrm(ServoParamDataList.Items[writeCt].Homepri, _data.AxisParam[writeCt].Homepri);
                _data.AxisParam[writeCt].BackL = SetServoPrm(ServoParamDataList.Items[writeCt].BackL, _data.AxisParam[writeCt].BackL);
                _data.AxisParam[writeCt].Revise = SetServoPrm(ServoParamDataList.Items[writeCt].Revise, _data.AxisParam[writeCt].Revise);
                _data.AxisParam[writeCt].OrgCsetOfs = SetServoPrm(ServoParamDataList.Items[writeCt].OrgCsetOfs, _data.AxisParam[writeCt].OrgCsetOfs);
                _data.AxisParam[writeCt].handle_max = SetServoPrm(ServoParamDataList.Items[writeCt].handle_max, _data.AxisParam[writeCt].handle_max);
                _data.AxisParam[writeCt].handle_ka = SetServoPrm(ServoParamDataList.Items[writeCt].handle_ka, _data.AxisParam[writeCt].handle_ka);
                _data.AxisParam[writeCt].PrcsKa = SetServoPrm(ServoParamDataList.Items[writeCt].PrcsKa, _data.AxisParam[writeCt].PrcsKa);
                _data.AxisParam[writeCt].AecSoftLimP = SetServoPrm(ServoParamDataList.Items[writeCt].AecSoftLimP, _data.AxisParam[writeCt].AecSoftLimP);
                _data.AxisParam[writeCt].AecSoftLimM = SetServoPrm(ServoParamDataList.Items[writeCt].AecSoftLimM, _data.AxisParam[writeCt].AecSoftLimM);
            }
            _data.AddParam.AxAecSoftLim = SetServoPrm(ServoParamDataList.AxAecSoftLim, _data.AddParam.AxAecSoftLim);
            //基底クラスの書き込み処理
            return base.WriteData(_data);
        }

        private dynamic SetServoPrm(string source, dynamic writeData)
        {
            Type wdata = writeData.GetType();
            //型に合った変換処理
            try
            {
                if(wdata == typeof(long))
                {
                    //int64
                    if (true != UIStaticMethods.IsNumeric(source))
                    {
                        return Convert.ToInt64(source, 16).ToString();
                    }
                    else
                    {
                        return (int.Parse(source));
                    }
                }
                else if (wdata == typeof(int))
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
                else if (wdata == typeof(short))
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
                else if (wdata == typeof(ushort))
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
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return writeData;
            }
        }
        /// <summary>インスタンスの破棄</summary>
		public new void Dispose()
        {
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
