using ECNC3.Enumeration;
using System;
using System.Collections.Generic;

namespace ECNC3.Models
{
    /// <summary>ピッチエラー補正情報</summary>
    public class StructureMcDatPitchAdjustItem : IDisposable
    {
        /// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
        private bool _disposed = false;
        /// <summary>補正点の最大数</summary>
        public int PointMaxCount { get; private set; }
        /// <summary>軸番号</summary>
        public AxisNumbers AxisNumber { get; private set; }

        /// <summary>補正倍率</summary>
        public int RevMagnify { get; set; }
        /// <summary>補正間隔</summary>
        public int RevSpace { get; set; }
        /// <summary>補正データ先頭番号</summary>
        public int RevTopNo { get; set; }
        /// <summary>－側補正区間数</summary>
        public int RevMCnt { get; set; }
        /// <summary>＋側補正区間数</summary>
        public int RevPCnt { get; set; }
        /// <summary>補正データ</summary>
        public short[] RevDt { get; set; }

        /// <summary>コンストラクタ</summary>
        /// <param name="axis">軸番号</param>
        public StructureMcDatPitchAdjustItem(AxisNumbers axis)
        {
            AxisNumber = axis;
            RevMagnify = 1;
            PointMaxCount = 30000;
            RevTopNo = (int)axis * PointMaxCount;
            RevDt = new short[PointMaxCount];
        }
        /// <summary>インスタンスの破棄</summary>
        public void Dispose()
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
        protected virtual void Dispose(bool disposing)
        {
            try
            {
                if (false == _disposed)
                {
                    if (true == disposing)
                    {
                        //  マネージリソースの解放
                        RevDt = null;
                    }
                    //  アンマネージリソースの解放
                }
                _disposed = true;
            }
            finally
            {
                ;   //  基底クラスのDispose()を確実に呼び出す。
                    //base.Dispose( disposing );
            }
        }
        /// <summary>
        /// 補正間隔数の登録
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        /// <remarks>
        /// 0.0を基準にしたプラス座標を持つ計測地点の数とマイナス座標をもつ計測地点の数を登録します。
        /// </remarks>
        public ResultCodes SetCorrectionSectionCount(List<double> source, int precisionDigit = 6)
        {
            RevMCnt = 0;
            RevPCnt = 0;
            if (1 > source.Count)
            {
                return ResultCodes.InvalidArgument;
            }
            double precision = Math.Pow(10, precisionDigit);
            int present;
            int next;
            int index;
            for (index = 1; index < source.Count; ++index)
            {
                present = (int)Math.Abs((int)(source[index - 1] * precision));
                next = (int)Math.Abs((int)(source[index] * precision));
                if (0 > next)
                {
                    ++RevMCnt;
                    if (0 == present)
                    {
                        ++RevMCnt;
                    }
                }
                else if (0 < next)
                {
                    ++RevPCnt;
                    if (0 == present)
                    {
                        ++RevPCnt;
                    }
                }
            }
            return ResultCodes.Success;
        }

        /// <summary>
        /// 補正値の設定
        /// </summary>
        /// <param name="target">補正データ番号</param>
        /// <param name="data">設定値</param>
        /// <returns></returns>
        public ResultCodes SetDeviationsData(int target, int data)
        {
            if ((0 > target) || (PointMaxCount <= target))
            {
                return ResultCodes.OutOfRange;
            }
            //if ((-127 > data) || (127 < data))
            //{
            //    return ResultCodes.OutOfRange;
            //}
            RevDt[target] = (short)data;
            return ResultCodes.Success;
        }
        /// <summary>補正値の設定</summary>
        /// <param name="data">MCから取得された補正値</param>
        /// <returns></returns>
        public ResultCodes SetDeviationsDatas(int headNumber, short[] data)
        {
            ResultCodes ret = ResultCodes.InvalidArgument;
            while (null != data)
            {
                RevTopNo = headNumber;
                int revIndex = headNumber;
                int revSize = data.Length;
                if (revSize < revIndex)
                {
                    break;
                }
                int index = 0;
                for (; (revIndex < revSize) && (index < PointMaxCount); ++index, ++revIndex)
                {
                    ret = SetDeviationsData(index, data[revIndex]);
                    if (ResultCodes.Success != ret)
                    {
                        break;
                    }
                }
                if (ResultCodes.Success != ret)
                {
                    break;
                }
                if (index < PointMaxCount)
                {
                    ret = ResultCodes.OutOfRange;
                }
                break;
            }
            return ret;
        }
    }

    /// <summary>ピッチエラー補正情報</summary>
    public class StructureMcDatPitchAdjustList : List<StructureMcDatPitchAdjustItem>, IDisposable
    {
        /// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
        private bool _disposed = false;
        /// <summary>インスタンスの破棄</summary>
        public void Dispose()
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
        protected virtual void Dispose(bool disposing)
        {
            try
            {
                if (false == _disposed)
                {
                    if (true == disposing)
                    {
                        //  マネージリソースの解放
                        if (0 < this.Count)
                        {
                            foreach (StructureMcDatPitchAdjustItem item in this)
                            {
                                item.Dispose();
                            }
                            this.Clear();
                        }
                    }
                    //  アンマネージリソースの解放
                }
                _disposed = true;
            }
            finally
            {
                ;   //  基底クラスのDispose()を確実に呼び出す。
                    //base.Dispose( disposing );
            }
        }
    }
}
