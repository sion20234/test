using ECNC3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECNC3.Models.McIf
{

    public enum PcondBitConvert
    {
        IP,
        SFIP,
        CAP
    }
    /// <summary>
    ///加工条件値ビットコンバータ
    /// </summary>
    /// <remarks>IP,SFIP,CAPに対応</remarks>
    public class UIPcondBitConverter : IDisposable
    {
        #region Constractor
        public UIPcondBitConverter()
        {
            _initialize();
        }        
        #endregion
        #region VariableMembers
        private FileProcessConditionParameter _bits = null;
        public FileProcessConditionParameter BitItem => _bits;
        #endregion
        #region DisposeMethods
        public void Dispose()
        {
            if(_bits != null)
            {
                _bits.Dispose();
                _bits = null;
            }
        }
        #endregion
        #region PrivateMethods
        private void _initialize()
        {
            _bits = new FileProcessConditionParameter();
            _bits.Read();
        }
        #endregion
        #region PublicMethods
        #region ToBit
        /// <summary>
        /// IP実数値⇒ビット値変換処理
        /// </summary>
        /// <param name="param">IP実数値</param>
        /// <returns>ビット値　-1の場合変換失敗</returns>
        public int IpToBit(int param)
        {
            //演算用の桁合わせ変数（実値）
            int paramTemp = param;
            //戻りのビット値
            int ret = 0;
            //ビット管理用配列
            bool[] ipBits = new bool[8];
            //各ビットのIP値の管理用配列
            int[] BitVals = new int[8];

            //初期化処理
            for (int bitInitCt = 0; bitInitCt < ipBits.Length; bitInitCt++)
            {
                ipBits[bitInitCt] = false;
            }
            //各ビットのIP値をセット
            BitVals[0] = _bits.IpsBit.IP10;
            BitVals[1] = _bits.IpsBit.IP20;
            BitVals[2] = _bits.IpsBit.IP30;
            BitVals[3] = _bits.IpsBit.IP40;
            BitVals[4] = _bits.IpsBit.IP50;
            BitVals[5] = _bits.IpsBit.IP60;
            BitVals[6] = _bits.IpsBit.IP70;
            BitVals[7] = _bits.IpsBit.IP80;
            //Ipはbit番号と比例して値が大きくなるので、配列の後ろから
            //（最大値）実値からBitVals[index]を引いていく。
            //最終的に（bit1つずつ判定し終えたら）余りが0にならない場合は
            //その実値は無効となり、エラーとする。
            for (int tempCt = 7; tempCt >= 0; tempCt--)
            {
                if(paramTemp - BitVals[tempCt] < 0)
                {
                    continue;
                }
                paramTemp -= BitVals[tempCt];
                //余りが0以上の場合、減算時に使用されたbitと同IndexのipBit[index]をtrueにする。
                ipBits[tempCt] = true;
            }
            //演算からビット変換処理へ移行する。
            for (int retTemp = 0; retTemp < ipBits.Length; retTemp++)
            {
                if (ipBits[retTemp] == true)
                {
                    switch (retTemp)
                    {
                        case 0: ret += 1; break;
                        case 1: ret += 2; break;
                        case 2: ret += 4; break;
                        case 3: ret += 8; break;
                        case 4: ret += 16; break;
                        case 5: ret += 32; break;
                        case 6: ret += 64; break;
                        case 7: ret += 128; break;
                    }
                }
            }
            if (paramTemp < 0)
            {
                ret = 0;
            }

            //リソース破棄
            for(int ct = 0; ct < ipBits.Length; ct++)
            {
                ipBits[ct] = false;
                BitVals[ct] = 0;
            }
            ipBits = null;
            BitVals = null;

            return ret;
        }
        /// <summary>
        /// SFIP実数値⇒ビット値変換処理
        /// </summary>
        /// <param name="param">SFIP実数値</param>
        /// <returns>ビット値　-1の場合変換失敗</returns>
        public int SFIpToBit(int param)
        {
            //演算用の桁合わせ変数（実値）
            int paramTemp = param - _bits.SFIpsBit.OFFSET;
            //戻りのビット値
            int ret = 0;
            //ビット管理用配列
            bool[] sfipBits = new bool[9];
            //各ビットのIP値の管理用配列
            int[] BitVals = new int[9];

            //初期化処理
            for (int bitInitCt = 0; bitInitCt < sfipBits.Length; bitInitCt++)
            {
                sfipBits[bitInitCt] = false;
            }
            //各ビットのIP値をセット
            BitVals[0] = _bits.SFIpsBit.SFIP10;
            BitVals[1] = _bits.SFIpsBit.SFIP20;
            BitVals[2] = _bits.SFIpsBit.SFIP30;
            BitVals[3] = _bits.SFIpsBit.SFIP40;
            BitVals[4] = _bits.SFIpsBit.SFIP50;
            BitVals[5] = _bits.SFIpsBit.SFIP60;
            BitVals[6] = _bits.SFIpsBit.SFIP70;
            BitVals[7] = _bits.SFIpsBit.SFIP80;
            BitVals[8] = _bits.SFIpsBit.SFIP90;
            //Ipはbit番号と比例して値が大きくなるので、配列の後ろから
            //（最大値）実値からBitVals[index]を引いていく。
            //最終的に（bit1つずつ判定し終えたら）余りが0にならない場合は
            //その実値は無効となり、エラーとする。
            for (int tempCt = 8; tempCt >= 0; tempCt--)
            {
                if (paramTemp - BitVals[tempCt] < 0)
                {
                    continue;
                }
                paramTemp -= BitVals[tempCt];
                //余りが0以上の場合、減算時に使用されたbitと同IndexのipBit[index]をtrueにする。
                sfipBits[tempCt] = true;
            }
            //余りが0の場合、演算からビット変換処理へ移行する。
            for (int retTemp = 0; retTemp < sfipBits.Length; retTemp++)
            {
                if (sfipBits[retTemp] == true)
                {

                    switch (retTemp)
                    {
                        case 0: ret += 1; break;
                        case 1: ret += 2; break;
                        case 2: ret += 4; break;
                        case 3: ret += 8; break;
                        case 4: ret += 16; break;
                        case 5: ret += 32; break;
                        case 6: ret += 64; break;
                        case 7: ret += 128; break;
                        case 8: ret += 256; break;
                    }
                }
            }
            if (paramTemp < 0)
            {
                ret = 0;
            }

            //リソース破棄
            for (int ct = 0; ct < sfipBits.Length; ct++)
            {
                sfipBits[ct] = false;
                BitVals[ct] = 0;
            }
            sfipBits = null;
            BitVals = null;

            return ret;
        }
        /// <summary>
        /// CAP実数値⇒ビット値変換処理
        /// </summary>
        /// <param name="param">CAP実数値</param>
        /// <returns>ビット値　-1の場合変換失敗</returns>
        public int CapToBit(int param)
        {
            //演算用の桁合わせ変数（実値）
            int paramTemp = param;      
            //戻りのビット値
            int ret = 0;
            //ビット管理用配列
            bool[] capBits = new bool[8];
            //各ビットのCAP値の管理用配列
            int[] BitVals = new int[8];

            //初期化処理
            for (int bitInitCt = 0; bitInitCt < capBits.Length; bitInitCt++)
            {
                capBits[bitInitCt] = false;
            }
            //各ビットのCAP値をセット
            BitVals[0] = _bits.CapsBit.C60;
            BitVals[1] = _bits.CapsBit.C70;
            BitVals[2] = _bits.CapsBit.C80;
            BitVals[3] = _bits.CapsBit.C10;
            BitVals[4] = _bits.CapsBit.C20;
            BitVals[5] = _bits.CapsBit.C30;
            BitVals[6] = _bits.CapsBit.C40;
            BitVals[7] = _bits.CapsBit.C50;
            //Capはbit番号と比例しているが、５～７bit目が最小値～なので順番を変更する。
            //その後、実値からBitVals[index]を引いていく。
            //最終的に（bit1つずつ判定し終えた時点で）余りが0にならない場合は
            //その実値は無効となり、エラーとする。
            for (int tempCt = 7; tempCt >= 0; tempCt--)
            {
                if (paramTemp - BitVals[tempCt] < 0)
                {
                    continue;
                }
                paramTemp -= BitVals[tempCt];
                //余りが0以上の場合、減算時に使用されたbitと同IndexのcapBit[index]をtrueにする。
                capBits[tempCt] = true;
            }
            for (int retTemp = 0; retTemp < capBits.Length; retTemp++)
            {
                if (capBits[retTemp] == true)
                {

                    //bitの並びを元に戻しビット変換
                    switch (retTemp)
                    {
                        case 0: ret += 32; break;
                        case 1: ret += 64; break;
                        case 2: ret += 128; break;
                        case 3: ret += 1; break;
                        case 4: ret += 2; break;
                        case 5: ret += 4; break;
                        case 6: ret += 8; break;
                        case 7: ret += 16; break;
                    }
                }
            }
            if (paramTemp < 0)
            {
                ret = 0;
            }

            //リソース破棄
            for (int ct = 0; ct < capBits.Length; ct++)
            {
                capBits[ct] = false;
                BitVals[ct] = 0;
            }
            capBits = null;
            BitVals = null;

            return ret;
        }
        #endregion
        #region FromBit
        public int IpFromBit(int bit)
        {
            //演算用の変数（bit）
            int bitTemp = bit;
            //戻りのIP値
            int ret = 0;
            //ビット管理用配列
            bool[] ipBits = new bool[8];
            //各ビットのIP値の管理用配列
            int[] BitVals = new int[8];

            //初期化処理
            for (int bitInitCt = 0; bitInitCt < ipBits.Length; bitInitCt++)
            {
                ipBits[bitInitCt] = false;
            }
            //各ビットのIP値をセット
            BitVals[0] = _bits.IpsBit.IP10;
            BitVals[1] = _bits.IpsBit.IP20;
            BitVals[2] = _bits.IpsBit.IP30;
            BitVals[3] = _bits.IpsBit.IP40;
            BitVals[4] = _bits.IpsBit.IP50;
            BitVals[5] = _bits.IpsBit.IP60;
            BitVals[6] = _bits.IpsBit.IP70;
            BitVals[7] = _bits.IpsBit.IP80;

            if((bitTemp > 255))
            {
                return 0;        
            }
            
            if (bitTemp >= 128 )
            {
                bitTemp -= 128;
                ipBits[7] = true;
            }
            if(bitTemp >= 64)
            {
                bitTemp -= 64;
                ipBits[6] = true;
            }
            if(bitTemp >= 32)
            {
                bitTemp -= 32;
                ipBits[5] = true;
            }
            if(bitTemp >= 16)
            {
                bitTemp -= 16;
                ipBits[4] = true;
            }
            if(bitTemp >= 8)
            {
                bitTemp -= 8;
                ipBits[3] = true;
            }
            if (bitTemp >= 4)
            {
                bitTemp -= 4;
                ipBits[2] = true;
            }
            if (bitTemp >= 2)
            {
                bitTemp -= 2;
                ipBits[1] = true;
            }
            if (bitTemp >= 1)
            {
                bitTemp -= 1;
                ipBits[0] = true;
            }

            //Ipはbit番号と比例して値が大きくなるので、配列の後ろから
            //（最大値）BitVals[index]を足していく。
            ret = 0;
            for (int bitCt = 0; bitCt < ipBits.Length; bitCt++)
            {
                if(ipBits[bitCt] == true)
                {
                    ret += BitVals[bitCt];
                }
            }

            //リソース破棄
            for (int ct = 0; ct < ipBits.Length; ct++)
            {
                ipBits[ct] = false;
                BitVals[ct] = 0;
            }
            ipBits = null;
            BitVals = null;

            return ret;//(ret < 1000) ? 0 : ret / 1000;
        }
        public int SFIpFromBit(int bit)
        {
            //演算用の変数（bit）
            int bitTemp = bit;
            //戻りのIP値
            int ret = 0;
            //ビット管理用配列
            bool[] sfipBits = new bool[9];
            //各ビットのIP値の管理用配列
            int[] BitVals = new int[9];

            //初期化処理
            for (int bitInitCt = 0; bitInitCt < sfipBits.Length; bitInitCt++)
            {
                sfipBits[bitInitCt] = false;
            }
            //各ビットのIP値をセット
            BitVals[0] = _bits.SFIpsBit.SFIP10;
            BitVals[1] = _bits.SFIpsBit.SFIP20;
            BitVals[2] = _bits.SFIpsBit.SFIP30;
            BitVals[3] = _bits.SFIpsBit.SFIP40;
            BitVals[4] = _bits.SFIpsBit.SFIP50;
            BitVals[5] = _bits.SFIpsBit.SFIP60;
            BitVals[6] = _bits.SFIpsBit.SFIP70;
            BitVals[7] = _bits.SFIpsBit.SFIP80;
            BitVals[8] = _bits.SFIpsBit.SFIP90;

            if ((bitTemp > 511))
            {
                return 0;
            }

            if (bitTemp >= 256)
            {
                bitTemp -= 256;
                sfipBits[8] = true;
            }
            if (bitTemp >= 128)
            {
                bitTemp -= 128;
                sfipBits[7] = true;
            }
            if (bitTemp >= 64)
            {
                bitTemp -= 64;
                sfipBits[6] = true;
            }
            if (bitTemp >= 32)
            {
                bitTemp -= 32;
                sfipBits[5] = true;
            }
            if (bitTemp >= 16)
            {
                bitTemp -= 16;
                sfipBits[4] = true;
            }
            if (bitTemp >= 8)
            {
                bitTemp -= 8;
                sfipBits[3] = true;
            }
            if (bitTemp >= 4)
            {
                bitTemp -= 4;
                sfipBits[2] = true;
            }
            if (bitTemp >= 2)
            {
                bitTemp -= 2;
                sfipBits[1] = true;
            }
            if (bitTemp >= 1)
            {
                bitTemp -= 1;
                sfipBits[0] = true;
            }

            //Ipはbit番号と比例して値が大きくなるので、配列の後ろから
            //（最大値）BitVals[index]を足していく。
            ret = 0;
            for (int bitCt = 0; bitCt < sfipBits.Length; bitCt++)
            {
                if (sfipBits[bitCt] == true)
                {
                    ret += BitVals[bitCt];
                }
            }

            //リソース破棄
            for (int ct = 0; ct < sfipBits.Length; ct++)
            {
                sfipBits[ct] = false;
                BitVals[ct] = 0;
            }
            sfipBits = null;
            BitVals = null;

            return ret + _bits.SFIpsBit.OFFSET; //(ret < 1000) ? 0 : ret / 1000;
        }
        public int CapFromBit(int bit)
        {
            //演算用の変数（bit）
            int bitTemp = bit;
            //戻りのIP値
            int ret = 0;
            //ビット管理用配列
            bool[] capBits = new bool[8];
            //各ビットのIP値の管理用配列
            int[] BitVals = new int[8];

            //初期化処理
            for (int bitInitCt = 0; bitInitCt < capBits.Length; bitInitCt++)
            {
                capBits[bitInitCt] = false;
            }
            //各ビットのIP値をセット
            BitVals[0] = _bits.CapsBit.C60;
            BitVals[1] = _bits.CapsBit.C70;
            BitVals[2] = _bits.CapsBit.C80;
            BitVals[3] = _bits.CapsBit.C10;
            BitVals[4] = _bits.CapsBit.C20;
            BitVals[5] = _bits.CapsBit.C30;
            BitVals[6] = _bits.CapsBit.C40;
            BitVals[7] = _bits.CapsBit.C50;

            if ((bitTemp > 255))
            {
                return 0;
            }

            if (bitTemp >= 128)
            {
                bitTemp -= 128;
                capBits[2] = true;
            }
            if (bitTemp >= 64)
            {
                bitTemp -= 64;
                capBits[1] = true;
            }
            if (bitTemp >= 32)
            {
                bitTemp -= 32;
                capBits[0] = true;
            }
            if (bitTemp >= 16)
            {
                bitTemp -= 16;
                capBits[7] = true;
            }
            if (bitTemp >= 8)
            {
                bitTemp -= 8;
                capBits[6] = true;
            }
            if (bitTemp >= 4)
            {
                bitTemp -= 4;
                capBits[5] = true;
            }
            if (bitTemp >= 2)
            {
                bitTemp -= 2;
                capBits[4] = true;
            }
            if (bitTemp >= 1)
            {
                bitTemp -= 1;
                capBits[3] = true;
            }

            //Ipはbit番号と比例して値が大きくなるので、配列の後ろから
            //（最大値）BitVals[index]を足していく。
            ret = 0;
            for (int bitCt = 0; bitCt < capBits.Length; bitCt++)
            {
                if (capBits[bitCt] == true)
                {
                    ret += BitVals[bitCt];
                }
            }

            //リソース破棄
            for (int ct = 0; ct < capBits.Length; ct++)
            {
                capBits[ct] = false;
                BitVals[ct] = 0;
            }
            capBits = null;
            BitVals = null;

            return ret; //(ret < 1000) ? 0 : ret / 1000;
        }
        #endregion
        #endregion
    }
}
