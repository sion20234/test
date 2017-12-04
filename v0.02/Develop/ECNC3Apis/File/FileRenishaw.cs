using ECNC3.Enumeration;
using ECNC3.Models.Common;
using System;
using System.Collections.Generic;
using System.IO;

namespace ECNC3.Models
{
    /// <summary>DEVIATIONSセクションデータ</summary>
    public class StructureDeviationsItem
    {
        public int Run { get; set; }
        public int Target { get; set; }
        public double Data { get; set; }
    }
    /// <summary>DEVIATIONSセクションデータリスト</summary>
    public class StructureDeviationsList : List<StructureDeviationsItem>
    {
    }
    /// <summary>レニショーファイル読み込み</summary>
    /// <example>
    /// using (FileRenishaw fr = new FileRenishaw())
    /// {
    ///     fr.AxisNumber = Enumeration.AxisNumbers.Z;
    ///     fr.FilePath = @"D:\Users\160516補正前X軸.rtl";
    ///     fr.Read();
    /// }
    /// </example>
    public class FileRenishaw : IDisposable, IEcnc3FileReadOnly
    {
        /// <summary>レニショーファイル内セクション名</summary>
        private enum FileSections
        {
            Header = 0,
            TargetData,
            UserText,
            Runs,
            Deviations,
            Enviroment,
            Unknown = -1
        };

        /// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
        private bool _disposed = false;

        /// <summary>読み込みレニショーファイルパス</summary>
        public string FilePath { get; set; }
        /// <summary>指定軸</summary>
        public AxisNumbers AxisNumber { get; set; }
        /// <summary>ピッチ補正データ</summary>
        public StructureMcDatPitchAdjustItem Data { get; private set; }

        /// <summary>TARGET DATA::Target-count:</summary>
        public int TargetCount { get; private set; }
        /// <summary>RUNS::Run-count:</summary>
        public int RunCount { get; private set; }
        /// <summary>DEVIATIONS::Run Target Data:</summary>
        public StructureDeviationsList Deviations { get; private set; }

        /// <summary>
        /// 区間座標
        /// </summary>
        private List<double> Intervals { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FileRenishaw()
        {
            Reset();
        }
        private void Reset()
        {
            //AxisNumber = AxisNumbers.Unknown;
            TargetCount = -1;
            if (null != Data)
            {
                Data.Dispose();
                Data = null;
            }
            if (null != Intervals)
            {
                Intervals.Clear();
                Intervals = null;
            }
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
                        if (null != Data)
                        {
                            Data.Dispose();
                            Data = null;
                        }
                        if (null != Deviations)
                        {
                            Deviations.Clear();
                            Deviations = null;
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

        /// <summary>ファイル読み込み</summary>
        /// <returns></returns>
        public ResultCodes Read()
        {
            while (true)
            {
                if (AxisNumbers.Unknown == AxisNumber)
                {
                    break;
                }
                if (true == string.IsNullOrEmpty(FilePath))
                {
                    break;
                }
                else if (false == File.Exists(FilePath))
                {
                    break;
                }
                Reset();
                FileSections section = FileSections.Unknown;
                Deviations = new StructureDeviationsList();
                Intervals = new List<double>();
                using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
                using (TextReader sr = new StreamReader(fs))
                {
                    string line = sr.ReadLine();
                    bool inTagRunTargetData = false;
                    bool inTagTargets = false;
                    while (null != line)
                    {
                        //  セクション名を確認
                        if (false == CheckChangeSection(line, ref section))
                        {
                            //  セクション毎の分岐
                            switch (section)
                            {
                                case FileSections.TargetData:
                                    ReadSectionTargetData(line, ref inTagTargets);
                                    break;
                                case FileSections.Runs:
                                    ReadSectionRuns(line);
                                    break;
                                case FileSections.Deviations:
                                    ReadSectionDeviations(line, Deviations, ref inTagRunTargetData);
                                    break;
                                default:
                                    break;
                            }
                        }
                        line = sr.ReadLine();
                    }
                }
                Data = new StructureMcDatPitchAdjustItem(AxisNumber);
                return Analyze(Data);
            }
            return ResultCodes.InvalidArgument;
        }
        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        /// <remarks>
        /// ファイルの読み取り結果を解析して、引数 target に値を設定します。
        /// 軸番号は呼び出し元で設定します。
        /// </remarks>
        private ResultCodes Analyze(StructureMcDatPitchAdjustItem target)
        {
            //  補正区間
            double correctionInterval;
            ResultCodes ret = AnalyzeTagTargets(Intervals, out correctionInterval);
            if (ResultCodes.Success == ret)
            {
                target.RevSpace = (int)correctionInterval * 1000;      //  補正間隔
                target.SetCorrectionSectionCount(Intervals);    //  ＋側および－側補正区間数
            }
            //  補正値
            if (0 < Deviations.Count)
            {
                //  区間毎に取得された補正値を実行回数に応じて按分する。
                List<StructureDeviationsItem> intervals = Deviations.FindAll((x) => (1 == x.Run));
                if (null != intervals)
                {
                    foreach (StructureDeviationsItem item in intervals)
                    {
                        //  区間番号が一致するデータを取得する。
                        List<StructureDeviationsItem> found = Deviations.FindAll((x) => (item.Target == x.Target));
                        if (null != found)
                        {
                            double total = 0;
                            found.ForEach((x) => total += x.Data);
                            target.SetDeviationsData(item.Target, (int)Math.Round((total / found.Count), 0, MidpointRounding.AwayFromZero));
                        }
                    }
                }
            }
            return ResultCodes.Success;
        }

        /// <summary>
        /// セクションの切り替えチェック
        /// </summary>
        /// <param name="source"></param>
        /// <param name="section"></param>
        /// <returns>true=セクション名検出／false=未検出</returns>
        bool CheckChangeSection(string source, ref FileSections section)
        {
            if (0 == string.Compare(source, "HEADER::", true))
            {
                section = FileSections.Header;
            }
            else if (0 == string.Compare(source, "TARGET DATA::", true))
            {
                section = FileSections.TargetData;
            }
            else if (0 == string.Compare(source, "USER-TEXT::", true))
            {
                section = FileSections.UserText;
            }

            else if (0 == string.Compare(source, "RUNS::", true))
            {
                section = FileSections.Runs;
            }
            else if (0 == string.Compare(source, "DEVIATIONS::", true))
            {
                section = FileSections.Deviations;
            }
            else if (0 == string.Compare(source, "ENVIRONMENT::", true))
            {
                section = FileSections.Enviroment;
            }
            else
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// TARGET DATA::セクション解析
        /// </summary>
        /// <param name="source">解析対象となる文字列</param>
        /// <param name="inTagTargets">Targetsタグ内の解析であるか</param>
        /// <returns></returns>
        private ResultCodes ReadSectionTargetData(string source, ref bool inTagTargets)
        {
            const string TagTargetCount = "Target-count:";
            const string TagTargets = "Targets :";
            const string TagFlags = "Flags:";
            if (0 > TargetCount)
            {
                if (true == source.StartsWith(TagTargetCount))
                {
                    int targetCount;
                    if (true == int.TryParse(source.Substring(TagTargetCount.Length, source.Length - TagTargetCount.Length), out targetCount))
                    {
                        TargetCount = targetCount;
                    }
                }
            }
            else
            {
                if (false == inTagTargets)
                {
                    if (true == source.StartsWith(TagTargets))
                    {
                        inTagTargets = true;
                    }
                }
                else
                {
                    if (true == source.StartsWith(TagFlags))
                    {
                        inTagTargets = false;
                    }
                    else
                    {
                        ReadTagTargets(source, Intervals);
                    }
                }
            }
            return ResultCodes.Success;
        }

        /// <summary>
        /// 補正間隔の読み取り
        /// </summary>
        /// <param name="source">解析対象となる文字列</param>
        /// <param name="target">解析結果を格納する配列</param>
        /// <returns>実行結果</returns>
        /// <remarks>
        /// レニショーファイルの補正間隔情報(Targets)を解析します。
        /// 補正間隔を示すレコードが一行でない可能性があります。
        /// 引数targetの初期化は呼び出し元で行ってください。
        /// </remarks>
        private ResultCodes ReadTagTargets(string source, List<double> target)
        {
            if ((null == target) || (true == string.IsNullOrEmpty(source)))
            {
                return ResultCodes.InvalidArgument;
            }
            string[] splitedArray = source.Split(' ');
            int index;
            string splitedItem;
            double value;
            AidConvert ac = new AidConvert();
            for (index = 0; index < splitedArray.Length; ++index)
            {
                splitedItem = splitedArray[index];
                if (false == string.IsNullOrWhiteSpace(splitedItem))
                {
                    if (true == ac.TryParse(splitedItem, out value))
                    {
                        target.Add(value);
                    }
                }
            }
            return ResultCodes.Success;
        }

        /// <summary>
        /// 補正間隔の正当性チェック
        /// </summary>
        /// <param name="source">解析対象となる補正間隔情報</param>
        /// <param name="result">補正間隔値</param>
        /// <param name="precisionDigit">比較精度</param>
        /// <returns></returns>
        private ResultCodes AnalyzeTagTargets(List<double> source, out double result, int precisionDigit = 6)
        {
            ResultCodes ret = ResultCodes.InvalidArgument;
            result = 0.0;
            while (1 < source.Count)
            {
                ret = ResultCodes.Success;
                double precision = Math.Pow(10, precisionDigit);
                result = Math.Abs(source[0] - source[1]);
                if (2 < source.Count)
                {
                    int baseLine = (int)(result * precision);
                    int index;
                    for (index = 2; index < source.Count; ++index)
                    {
                        if (baseLine != Math.Abs((int)((source[index - 1] - source[index]) * precision)))
                        {
                            ret = ResultCodes.InvalidArgument;
                            break;
                        }
                    }
                }
                break;
            }
            return ret;
        }

        /// <summary>RUNS::セクション解析</summary>
        /// <param name="source">解析対象となる文字列</param>
        /// <returns></returns>
        private ResultCodes ReadSectionRuns(string source)
        {
            const string TagRunCount = "Run-count:";
            if (true == source.StartsWith(TagRunCount))
            {
                int runCount;
                if (true == int.TryParse(source.Substring(TagRunCount.Length, source.Length - TagRunCount.Length), out runCount))
                {
                    RunCount = runCount;
                }
            }
            return ResultCodes.Success;
        }

        /// <summary>
        /// DEVIATIONS::セクション解析
        /// </summary>
        /// <param name="source">解析対象となる文字列</param>
        /// <returns></returns>
        private ResultCodes ReadSectionDeviations(string source, StructureDeviationsList target, ref bool inTagRunTargetData)
        {
            if (false == inTagRunTargetData) {
                const string TagRunCount = "Run Target Data:";
                if (true == source.StartsWith(TagRunCount))
                {
                    inTagRunTargetData = true;
                }
            } else {
                if ((null == target) || (true == string.IsNullOrEmpty(source)))
                {
                    return ResultCodes.InvalidArgument;
                }
                string[] splitedArray = source.Split(' ');
                int index;
                string splitedItem;
                double value;
                AidConvert ac = new AidConvert();
                StructureDeviationsItem item = new StructureDeviationsItem();
                int col = 0;
                for (index = 0; (index < splitedArray.Length) && (col < 3); ++index)
                {
                    splitedItem = splitedArray[index];
                    if (false == string.IsNullOrWhiteSpace(splitedItem))
                    {
                        if (true == ac.TryParse(splitedItem, out value))
                        {
                            switch (col) {
                                case 0:
                                    item.Run = (int)value;
                                    ++col;
                                    break;
                                case 1:
                                    item.Target = (int)value;
                                    ++col;
                                    break;
                                case 2:
                                    item.Data = value;
                                    ++col;
                                    target.Add(item);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            return ResultCodes.Success;
        }
    }
}
