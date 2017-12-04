using ECNC3.Enumeration;
using ECNC3.Models;
using ECNC3.Models.McIf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace ECNC3.Models.McIf
{
    public class McDatProcessLog
    {
        public McDatProcessLog()
        {
            
        }
        #region VariableMember
        private System.Timers.Timer timer = null;
        private StructureProcessLogItem item = null;
        private string outLogFilePath = "";
        private string outLogString = "";   //出力ログ
        const string crLf = "\r\n";       //CR-LF：改行
        const string crLf2 = crLf + crLf;   //CR-LF + CR-LF：改行＋改行
        #endregion
        #region PrivateMethod
        /// <summary>
        /// 処理間隔指定でタイマーを設定するためのメソッド（初期処理などで呼ぶ）
        /// </summary>
        /// <param name="Interval">処理間隔(単位：ﾐﾘ秒)</param>
        /// <remarks>引数指定で処理間隔指定処理。引数なしで1ミリ秒固定。</remarks>
        private void setTimer(int Interval)
        {
            timer = new System.Timers.Timer();
            timer.Interval = Interval;                                                              // 上のIntervalで指定した処理を行う間隔を指定します。(値はﾐﾘ秒)
            timer.AutoReset = true;
            timer.Elapsed += new ElapsedEventHandler(onTimer);
        }
        /// <summary>
        /// インターバルごとの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onTimer(object sender, ElapsedEventArgs e)
        {
            //加工深度取得
            Sampling();
        }
        /// <summary>
        /// ログ設定ファイル：グラフやタイトル、コメントを1から4取得
        /// </summary>
        /// <param name="xTimeAxisMax"></param>
        /// <param name="yProsessDepthMax"></param>
        /// <param name="samplingCycle"></param>
        /// <param name="upDownInverted"></param>
        /// <param name="title1"></param>
        /// <param name="title2"></param>
        /// <param name="title3"></param>
        /// <param name="title4"></param>
        /// <param name="comment1"></param>
        /// <param name="comment2"></param>
        /// <param name="comment3"></param>
        /// <param name="comment4"></param>
        private void GraphLogTitleAndCommentRead(
            ref string xTimeAxisMax, ref string yProsessDepthMax, ref string samplingCycle, ref string upDownInverted,
            ref string title1, ref string title2, ref string title3, ref string title4,
            ref string comment1, ref string comment2, ref string comment3, ref string comment4
            )
        {
            try
            {
                using (FileSettings fs = new FileSettings())
                {
                    //ファイル読み込み
                    fs.Read();
                    //グラフ
                    xTimeAxisMax = fs.AttrText("Root/LogSettingFile/Graph", "xTimeAxisMax");//
                    yProsessDepthMax = fs.AttrText("Root/LogSettingFile/Graph", "yProsessDepthMax");//
                    item.SamplingInterval = fs.AttrValue("Root/LogSettingFile/Graph", "samplingCycle");//
                    samplingCycle = item.SamplingInterval.ToString();
                    upDownInverted = fs.AttrText("Root/upDownInverted/Graph", "xTimeAxisMax");//
                                                                                              //ログタイトル設定
                    title1 = fs.AttrText("Root/LogSettingFile/LogTitleNumSet", "title1");//
                    title2 = fs.AttrText("Root/LogSettingFile/LogTitleNumSet", "title2");//
                    title3 = fs.AttrText("Root/LogSettingFile/LogTitleNumSet", "title3");//
                    title4 = fs.AttrText("Root/LogSettingFile/LogTitleNumSet", "title4");//
                                                                                         //ログコメント設定
                    comment1 = fs.AttrText("Root/LogSettingFile/LogComment", "comment1");//
                    comment2 = fs.AttrText("Root/LogSettingFile/LogComment", "comment2");//
                    comment3 = fs.AttrText("Root/LogSettingFile/LogComment", "comment3");//
                    comment4 = fs.AttrText("Root/LogSettingFile/LogComment", "comment4");//
                }
            }
            catch (Exception exc)
            {
                //例外処理
                MessageBox.Show(exc.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        ///<summary>データセットする行数(インデックス)NC/File両方</summary>
		private int _intCount = 0;  //NC/File両方
        private int _startCount = 0;//NC/File両方
        private int _endCount = 0;  //NC/File両方
                                    ///<summary>データセットする行数(インデックス)NC</summary>
        private int _startCountNc = 0;//NC
        private int _endCountNc = 0;//NC
                                    /// <summary>
                                    ///ログ変数設定： Log_VAR_LISTファイル読込みと取得
                                    /// </summary>
        private void LogVarListRead(ref string stringLogRec)
        {
            string filePath = FilePathInfo.MasterData + "Log_VAR_LIST.inf";
            string stringContent = "";
            System.Collections.ArrayList al = new System.Collections.ArrayList();
            //読込み
            using (StreamReader sr = new StreamReader(filePath, Encoding.GetEncoding("UTF-8")))
            {
                while ((stringContent = sr.ReadLine()) != null)
                {
                    //ログ記録変数：行追加
                    stringLogRec += stringContent + "\r\n";
                }
            }
        }
        /// <summary>
		/// "数字-数字"を解析
		/// </summary>
		/// <param name="stringTemp"></param>
		/// <param name="intStart"></param>
		/// <param name="intEnd"></param>
		/// <param name="numCount"></param>
		private void anzValToVal(string stringTemp, ref int intStart, ref int intEnd, ref int numCount)
        {
            int intIndex = stringTemp.IndexOf("-");
            int intLastIndex = stringTemp.LastIndexOf("-");
            string stringStart = stringTemp.Substring(0, intIndex);
            string stringEnd = stringTemp.Substring(intIndex + 1, stringTemp.Length - intIndex - 1);
            intStart = int.Parse(stringStart);
            intEnd = int.Parse(stringEnd);
            numCount = intEnd - intStart;
        }
        /// <summary>
        /// 文字列内に改行が有る場合、１行データとし、string配列にして返す
        /// </summary>
        /// <param name="inString"></param>
        /// <param name="outArray"></param>
        private void stringChangeArray(string inString, ref string[] outArray)
        {
            outArray = inString.Split(new string[] { "\r\n" }, StringSplitOptions.None);//区切りを"\r\n"とする。 
        }
        /// <summary>
		/// NC読み込みからログ内容を返す
		/// </summary>
		private void LogPartFromNcRead(string getLogNums, ref string selectLog)
        {
            // 元のカーソルを保持
            System.Windows.Forms.Cursor preCursol = System.Windows.Forms.Cursor.Current;
            // カーソルを待機カーソルに変更
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            McDatMacroManage macroManage = new McDatMacroManage(MacroManage.ExtensionMacro);  //クラス作成
            try
            {
                ResultCodes ret = macroManage.Read();
                if (ret == ResultCodes.Success)
                {
                    //読み込み成功時
                    _startCount = macroManage.Items[0].Number;
                    _startCountNc = _startCount;
                    _endCount = macroManage.Items.Count;
                    _endCountNc = _endCount;
                    _intCount = 0;//ファイル先頭
                    string[] outArray = { "" };
                    getLogNums = getLogNums.Replace("#", "");
                    stringChangeArray(getLogNums, ref outArray);
                    for (; _intCount < _startCount; _intCount++)
                    {
                        int index = macroManage.Items[_intCount].Number;//番号取得
                        string temp1 = "";
                        string temp2 = "";
                        string temp3 = "";
                        for (int iCount = 0; iCount < outArray.Length; iCount++)
                        {
                            //"-"無し
                            if (outArray[iCount] == macroManage.Items[_intCount].Number.ToString())
                            {//1個
                                temp1 = "#" + macroManage.Items[_intCount].Number.ToString();
                                temp2 = ",&";
                                temp3 = "1," + macroManage.Items[_intCount].Value;
                                selectLog += temp1 + temp2 + temp3 + "\r\n";
                                break;
                            }
                            if (outArray[iCount].Contains("-"))
                            {//1個以上
                                string stringTemp = outArray[iCount];
                                int intStart = 0, intEnd = 0, numCount = 0;
                                anzValToVal(stringTemp, ref intStart, ref intEnd, ref numCount);// "数字-数字"を解析
                                if (intStart.ToString() == macroManage.Items[_intCount].Number.ToString())
                                {
                                    //N1-N2:N1で比較
                                    temp1 = "#" + macroManage.Items[_intCount].Number.ToString();
                                    temp2 = ",&" + numCount;
                                    for (int iCount2 = intStart; iCount2 < intEnd; iCount2++)
                                    {
                                        //1個以上
                                        temp3 += "," + macroManage.Items[_intCount + iCount2].Value;
                                    }
                                    selectLog += temp1 + temp2 + temp3 + "\r\n";
                                }
                                break;
                            }
                        }
                    }
                }
                else
                {//エラー
                    macroManage.Dispose();
                    macroManage = null;
                    MessageBox.Show("読み込みエラー発生", "失敗", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            catch (Exception ex)
            {//その他例外
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            //破棄
            macroManage.Dispose();
            macroManage = null;
            //カーソルを元に戻す
            System.Windows.Forms.Cursor.Current = preCursol;         //カーソルを元に戻す 
        }
        private void Sampling()
        {
            //④加工座標
            McDatStatus mcDatStatus = new McDatStatus();
            mcDatStatus.Read();
            //書込み
            StreamWriter sw = new StreamWriter(outLogFilePath, true, Encoding.GetEncoding("UTF-8"));
            sw.Write((decimal.Parse(mcDatStatus.Status.CoordinateAsPosReg.AxisZ.ToString()) / 1000).ToString() + crLf);
            sw.Close();
        }
        #endregion
        #region PublicMethod
        public ResultCodes StartLog(string programName = "")
        {
            ResultCodes ret = ResultCodes.Success;
            //初期化
            if(item != null)
            {
                item.Dispose();
                item = null;
            }
            item = new StructureProcessLogItem();
            outLogString = "";
            //ログ作成
            //ファイルフォーマット：yyyyMMdd_HHmmss.txt文字列作成
            //放電加工を開始した日時
            //①加工開始日時：１放電加工を開始した日時
            DateTime dt = DateTime.Now;
            string filePathTemp = dt.ToString("yyyyMMdd_HHmmss");
            string filePath = FilePathInfo.ProcLog
                            + ((programName == "") ? "\\" : ("\\" + programName + "\\"));
            outLogString += dt.ToString("yyyy/MM/dd HH:mm:ss") + crLf2;////文字列連結
            //②ログタイトル
            string xTimeAxisMax = "", yProsessDepthMax = "", samplingCycle = "", upDownInverted = "";
            string title1 = "", title2 = "", title3 = "", title4 = "";
            string comment1 = "", comment2 = "", comment3 = "", comment4 = "";
            //CncSys.xmlファイルから読込み
            GraphLogTitleAndCommentRead(
                ref xTimeAxisMax, ref yProsessDepthMax, ref samplingCycle, ref upDownInverted,
                ref title1, ref title2, ref title3, ref title4,
                ref comment1, ref comment2, ref comment3, ref comment4
            );
            item.OutLogTitle += "#" + title1 + "=" + comment1 + "/* COMMENT#" + title1 + " */" + crLf;//改行
            item.OutLogTitle += "#" + title2 + "=" + comment2 + "/* COMMENT#" + title2 + " */" + crLf;//改行
            item.OutLogTitle += "#" + title3 + "=" + comment3 + "/* COMMENT#" + title3 + " */" + crLf;//改行
            item.OutLogTitle += "#" + title4 + "=" + comment4 + "/* COMMENT#" + title4 + " */" + crLf2;//改行
            //③ログ変数
            string getLogNums = "";
            string selectLog = "";
            LogVarListRead(ref getLogNums);             //Log_VAR_LIST.infよりLOGする#番号取得
            LogPartFromNcRead(getLogNums, ref selectLog); //NC読み込みからログ内容を返す
            outLogString += selectLog + crLf;               //改行
            //④加工座標
            McDatStatus mcDatStatus = new McDatStatus();
            mcDatStatus.Read();
            //読込み
            //機械座標
            item.dmAxisX = decimal.Parse(mcDatStatus.Status.CoordinateAsAbsReg.AxisX.ToString()) / 1000;
            item.dmAxisY = decimal.Parse(mcDatStatus.Status.CoordinateAsAbsReg.AxisY.ToString()) / 1000;
            item.dmAxisW = decimal.Parse(mcDatStatus.Status.CoordinateAsAbsReg.AxisW.ToString()) / 1000;
            item.dmAxisZ = decimal.Parse(mcDatStatus.Status.CoordinateAsAbsReg.AxisZ.ToString()) / 1000;
            item.dmAxisA = decimal.Parse(mcDatStatus.Status.CoordinateAsAbsReg.AxisA.ToString()) / 1000;
            item.dmAxisB = decimal.Parse(mcDatStatus.Status.CoordinateAsAbsReg.AxisB.ToString()) / 1000;
            item.dmAxisC = decimal.Parse(mcDatStatus.Status.CoordinateAsAbsReg.AxisC.ToString()) / 1000;
            //ワーク座標
            item.dwAxisX = decimal.Parse(mcDatStatus.Status.CoordinateAsPosReg.AxisX.ToString()) / 1000;
            item.dwAxisY = decimal.Parse(mcDatStatus.Status.CoordinateAsPosReg.AxisY.ToString()) / 1000;
            item.dwAxisW = decimal.Parse(mcDatStatus.Status.CoordinateAsPosReg.AxisW.ToString()) / 1000;
            item.dwAxisZ = decimal.Parse(mcDatStatus.Status.CoordinateAsPosReg.AxisZ.ToString()) / 1000;
            item.dwAxisA = decimal.Parse(mcDatStatus.Status.CoordinateAsPosReg.AxisA.ToString()) / 1000;
            item.dwAxisB = decimal.Parse(mcDatStatus.Status.CoordinateAsPosReg.AxisB.ToString()) / 1000;
            item.dwAxisC = decimal.Parse(mcDatStatus.Status.CoordinateAsPosReg.AxisC.ToString()) / 1000;
            //機械座標
            outLogString += "MX = " + item.dmAxisX.ToString() + crLf;//改行
            outLogString += "MY = " + item.dmAxisY.ToString() + crLf;
            outLogString += "MW = " + item.dmAxisW.ToString() + crLf;
            outLogString += "MZ = " + item.dmAxisZ.ToString() + crLf;
            outLogString += "MA = " + item.dmAxisA.ToString() + crLf;
            outLogString += "MB = " + item.dmAxisB.ToString() + crLf;
            outLogString += "MC = " + item.dmAxisC.ToString() + crLf;
            //ワーク座標             
            outLogString += "WX = " + item.dwAxisX.ToString() + crLf;//改行
            outLogString += "WY = " + item.dwAxisX.ToString() + crLf;
            outLogString += "WW = " + item.dwAxisX.ToString() + crLf;
            outLogString += "WZ = " + item.dwAxisX.ToString() + crLf;
            outLogString += "WA = " + item.dwAxisX.ToString() + crLf;
            outLogString += "WB = " + item.dwAxisX.ToString() + crLf;
            outLogString += "WC = " + item.dwAxisX.ToString() + crLf;
            //⑤サンプリング周期
            outLogString += "SP = " + samplingCycle + crLf2;    //改行
            //⑥加工ログ
            outLogString += "START" + crLf;

            //書込み
            Directory.CreateDirectory(filePath);
            filePath += filePathTemp + ".txt";
            StreamWriter sw = new StreamWriter(filePath, false, Encoding.GetEncoding("UTF-8"));
            sw.Write(outLogString);
            sw.Close();
            outLogFilePath = filePath;

            setTimer(item.SamplingInterval * 100);
            timer.Start();
            return ret;
        }
        public ResultCodes EndLog()
        {
            ResultCodes ret = ResultCodes.Success;
            timer.Stop();
            string outEndLogString = "";
            outEndLogString += "END" + crLf2;
            //⑦加工終了日時
            DateTime dt = DateTime.Now;
            outEndLogString += dt.ToString("yyyy/MM/dd HH:mm:ss") + crLf2;//改行
            //書込み
            StreamWriter sw = new StreamWriter(outLogFilePath, true, Encoding.GetEncoding("UTF-8"));
            sw.Write(outEndLogString);
            sw.Close();

            return ret;
        }
        #endregion       
    }
}
