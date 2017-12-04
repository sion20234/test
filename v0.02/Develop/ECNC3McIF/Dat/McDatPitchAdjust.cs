using System;
using System.IO;
using System.Runtime.InteropServices;
using ECNC3.Enumeration;
using ECNC3.Models.Common;
using static Rt64ecdata;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ECNC3.Models.McIf
{
	/// <summary>4-4-10.ピッチエラーパラメータ書込/読出</summary>
	public class McDatPitchAdjust : McCommBasic, IEcnc3Backup, IDisposable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>情報保持ファイルパス</summary>
		private string _filePath = string.Empty;
		/// <summary>コンストラクタ</summary>
		public McDatPitchAdjust()
		{
			Name = this.ToString();
			ClassName = GetType().Name;
            //DataType = Syncdef.DAT_PITCHERR;
            DataType = Syncdef.DAT_PITCHERR_AX;
			DataTypeName = "DAT_PITCHERR_AX";
			using( ECNC3Settings cmn = new ECNC3Settings() ) {
				_filePath = Path.Combine( cmn.DirectoryRt64Ec, "initial.pit" );
			}
		}

		/// <summary>インスタンスの破棄</summary>
		public new void Dispose()
		{
			Dispose( true );
			GC.SuppressFinalize( this );    //  ファイナライザによるDispose()呼び出しの抑制。
		}
		/// <summary>インスタンスの破棄</summary>
		/// <param name="disposing">呼び出し元の判別
		///     <list type="bullet" >
		///         <item>true=Dispose()関数からの呼び出し。</item>
		///         <item>false=ファイナライザによる呼び出し。</item>
		///     </list>
		/// </param>
		protected override void Dispose( bool disposing )
		{
			try {
				if( false == _disposed ) {
					if( true == disposing ) {
						//  マネージリソースの解放
					}
					//  アンマネージリソースの解放
				}
				_disposed = true;
			} finally {
				//  基底クラスのDispose()を確実に呼び出す。
				base.Dispose( disposing );
			}
		}
		/// <summary>初期化</summary>
		/// <returns>実行結果</returns>
		public ResultCodes Initialize()
		{
			PITCH_ERR_REV data = PITCH_ERR_REV.Init();
			ResultCodes ret = ReadFile( ref data, _filePath );
            using (McReqPitcherrClr pitClear = new McReqPitcherrClr())
            {
                ret = pitClear.Execute();
            }
                if (ResultCodes.Success == ret)
                {
                    int revTopCount = 0;
                    for (int readFileCt = 0; readFileCt < data.RevAx.Length; readFileCt++)
                    {
                        PITCH_ERR_REV_AX axData = PITCH_ERR_REV_AX.Init();
                        //軸毎ピッチ補正パラメータ設定
                        axData.RevMagnify = data.RevAx[readFileCt].RevMagnify;
                        axData.RevMCnt = data.RevAx[readFileCt].RevMCnt;
                        axData.RevPCnt = data.RevAx[readFileCt].RevPCnt;
                        axData.RevSpace = data.RevAx[readFileCt].RevSpace;
                        for (int readParamCt = revTopCount; readParamCt < revTopCount + (data.RevAx[readFileCt].RevMCnt + data.RevAx[readFileCt].RevPCnt); readParamCt++)
                        {
                            //軸毎ピッチ補正値設定
                            axData.RevDt[readParamCt - revTopCount] = data.RevDt[readParamCt];
                        }
                        revTopCount += (data.RevAx[readFileCt].RevMCnt + data.RevAx[readFileCt].RevPCnt);
                        //軸毎MCへの書き込み処理実行
                        ret = WriteData(axData, readFileCt);
                    }
                }
			return ret;
		}
		/// <summary>バックアップ</summary>
		/// <param name="backupDirectory">バックアップ先ディレクトリパス</param>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// MCボードよりデータを取得し、ファイルとしてバックアップします。
		/// </remarks>
		public ResultCodes Backup( string backupDirectory )
		{
			FileAccessCommon fc = new FileAccessCommon();
			fc.Backup( _filePath, backupDirectory );
            ResultCodes ret = ResultCodes.NotExecute;
            int TopIndex = 0;
           PITCH_ERR_REV data = PITCH_ERR_REV.Init();
            for (int readFileCt = 0; readFileCt < data.RevAx.Length; readFileCt++)
            {
                PITCH_ERR_REV_AX axData = PITCH_ERR_REV_AX.Init();
                ret = ReadData(out axData, readFileCt);
                //軸毎ピッチ補正パラメータ設定
                data.RevAx[readFileCt].RevMagnify = axData.RevMagnify;
                data.RevAx[readFileCt].RevMCnt = axData.RevMCnt;
                data.RevAx[readFileCt].RevPCnt = axData.RevPCnt;
                data.RevAx[readFileCt].RevSpace = axData.RevSpace;
                data.RevAx[readFileCt].RevTopNo = TopIndex;
                for (int readParamCt = TopIndex; readParamCt < TopIndex + (axData.RevMCnt + axData.RevPCnt); readParamCt++)
                {
                    //軸毎ピッチ補正値設定
                    data.RevDt[readParamCt] = axData.RevDt[readParamCt - TopIndex];
                }
                TopIndex += (axData.RevMCnt + axData.RevPCnt);
            }
            if ( ResultCodes.Success == ret ) {
				ret = WriteFile( data, _filePath );
			}
			return ret;
		}
		/// <summary>リストア</summary>
		/// <param name="restoreDirectory">復元元情報の格納されたディレクトリパス</param>
		/// <returns>実行結果</returns>
		public ResultCodes Restore( string restoreDirectory )
		{
			FileAccessCommon fc = new FileAccessCommon();
			return fc.Restore( restoreDirectory, _filePath );
		}
        #region Binary
        // ------------------------------------------------------------------------
        //	ピッチエラー補正用パラメータ情報構造体（６７８４０バイト固定長）
        // ------------------------------------------------------------------------
        [Serializable]
        public struct REV_AXSub // 各軸補正用パラメータ
        {
            public int RevMagnify;  // 補正倍率
            public int RevSpace;    // 補正間隔
            public int RevTopNo;    // 補正データ先頭番号
            public int RevMCnt;     // －側補正区間数
            public int RevPCnt;     // ＋側補正区間数
        }

        [Serializable]
        public class PITCH_ERR_REVSub
        {
            public REV_AXSub[] RevAx = new REV_AXSub[64];  // 各軸補正用パラメータ
            public short[] RevDt = new short[33280];   // 補正データ
        }
        public ResultCodes XmlToBinary()
        {
            string xmlFilePath = FilePathInfo.MasterData + "Pitch.xml";
            string binaryFilePath = FilePathInfo.ECNC3PATH + "initial.pit";
            PITCH_ERR_REVSub pitchData = new PITCH_ERR_REVSub();
            ResultCodes ret = ResultCodes.Success;
            if(ret == ResultCodes.Success) XmlFileRead(xmlFilePath, ref pitchData);
            if (ret == ResultCodes.Success) BinaryFileWrite(binaryFilePath, ref pitchData);
            return ret;
        }
        public ResultCodes BinaryToXml()
        {
            string xmlFilePath = FilePathInfo.MasterData + "Pitch.xml";
            string binaryFilePath = FilePathInfo.ECNC3PATH + "initial.pit";
            PITCH_ERR_REVSub pitchData = new PITCH_ERR_REVSub();
            ResultCodes ret = ResultCodes.Success;
            BinaryFileRead(binaryFilePath, ref pitchData);
            XmlFileWrite(xmlFilePath, ref pitchData);
            return ret;
        }
        /// <summary>
        /// オブジェクト読み込み
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public ResultCodes BinaryFileRead(string filePath, ref PITCH_ERR_REVSub obj)
        {
            try
            {
                using (Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    stream.Position = 0;
                    obj = (PITCH_ERR_REVSub)(formatter.Deserialize(stream));
                }
            }
            catch (Exception ex)
            {
                AidLog log = new AidLog("McDatPitchAdjust.BinaryRead()");
                log.Error(ex.Message);
                return ResultCodes.FailToReadFile;
            }
            return ResultCodes.Success;
        }
        /// <summary>
        /// オブジェクト書き込み
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="obj"></param>
        public ResultCodes BinaryFileWrite(string filePath, ref PITCH_ERR_REVSub obj)
        {
            try
            {
                //バイトに変換
                List<byte> tempByte = new List<byte>();
                for(int roopCt = 0; roopCt < 2; roopCt++)
                {
                    for (int revAxCt = 0; revAxCt < obj.RevAx.Length; revAxCt++)
                    {
                        for (int dataSizeCt = 0; dataSizeCt < BitConverter.GetBytes(obj.RevAx[revAxCt].RevMagnify).Length; dataSizeCt++)
                        {
                            tempByte.Add(BitConverter.GetBytes(obj.RevAx[revAxCt].RevMagnify)[dataSizeCt]);
                        }
                        for (int dataSizeCt = 0; dataSizeCt < BitConverter.GetBytes(obj.RevAx[revAxCt].RevSpace).Length; dataSizeCt++)
                        {
                            tempByte.Add(BitConverter.GetBytes(obj.RevAx[revAxCt].RevSpace)[dataSizeCt]);
                        }
                        for (int dataSizeCt = 0; dataSizeCt < BitConverter.GetBytes(obj.RevAx[revAxCt].RevTopNo).Length; dataSizeCt++)
                        {
                            tempByte.Add(BitConverter.GetBytes(obj.RevAx[revAxCt].RevTopNo)[dataSizeCt]);
                        }
                        for (int dataSizeCt = 0; dataSizeCt < BitConverter.GetBytes(obj.RevAx[revAxCt].RevMCnt).Length; dataSizeCt++)
                        {
                            tempByte.Add(BitConverter.GetBytes(obj.RevAx[revAxCt].RevMCnt)[dataSizeCt]);
                        }
                        for (int dataSizeCt = 0; dataSizeCt < BitConverter.GetBytes(obj.RevAx[revAxCt].RevPCnt).Length; dataSizeCt++)
                        {
                            tempByte.Add(BitConverter.GetBytes(obj.RevAx[revAxCt].RevPCnt)[dataSizeCt]);
                        }
                    }
                    for (int RevDtSizeCt = 0; RevDtSizeCt < obj.RevDt.Length; RevDtSizeCt++)
                    {
                        for (int dataSizeCt = 0; dataSizeCt < BitConverter.GetBytes(obj.RevDt[RevDtSizeCt]).Length; dataSizeCt++)
                        {
                            tempByte.Add(BitConverter.GetBytes(obj.RevDt[RevDtSizeCt])[dataSizeCt]);
                        }
                    }
                }                
                //ファイルに書き込む
                using (Stream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    byte[] writeData = tempByte.ToArray();
                    //バイト型配列の内容をすべて書き込む
                    stream.Write(writeData, 0, writeData.Length);
                }
            }
            catch (Exception ex)
            {
                AidLog log = new AidLog("McDatPitchAdjust.BinaryWrite()");
                log.Error(ex.InnerException.Message);
                return ResultCodes.FailToWriteFile;
            }
            return ResultCodes.Success;
        }
        /// <summary>
        /// XML読込
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public ResultCodes XmlFileRead(string filePath, ref PITCH_ERR_REVSub data)
        {
            //保存した内容を復元する
            System.Xml.Serialization.XmlSerializer serializer2 =
                new System.Xml.Serialization.XmlSerializer(typeof(PITCH_ERR_REVSub));
            System.IO.StreamReader sr = new System.IO.StreamReader(
                filePath, new System.Text.UTF8Encoding(false));
            PITCH_ERR_REVSub temp;
            temp = (PITCH_ERR_REVSub)serializer2.Deserialize(sr);
            sr.Close();
            for(int pitchValueCt = 0; pitchValueCt < temp.RevDt.Length; pitchValueCt++)
            {
                data.RevDt[pitchValueCt] = temp.RevDt[pitchValueCt];
            }
            for(int pitchAxisCt = 0; pitchAxisCt < temp.RevAx.Length; pitchAxisCt++)
            {
                data.RevAx[pitchAxisCt].RevMagnify = temp.RevAx[pitchAxisCt].RevMagnify;
                data.RevAx[pitchAxisCt].RevMCnt = temp.RevAx[pitchAxisCt].RevMCnt;
                data.RevAx[pitchAxisCt].RevPCnt = temp.RevAx[pitchAxisCt].RevPCnt;
                data.RevAx[pitchAxisCt].RevSpace = temp.RevAx[pitchAxisCt].RevSpace;
                data.RevAx[pitchAxisCt].RevTopNo = temp.RevAx[pitchAxisCt].RevTopNo;
            }
            return ResultCodes.Success;
        }
        /// <summary>
        /// XML書き込み
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public ResultCodes XmlFileWrite(string filePath, ref PITCH_ERR_REVSub data)
        {
            //XMLファイルに保存する
            System.Xml.Serialization.XmlSerializer serializer1 =
                new System.Xml.Serialization.XmlSerializer(typeof(PITCH_ERR_REVSub));
            System.IO.StreamWriter sw = new System.IO.StreamWriter(
                filePath, false, new System.Text.UTF8Encoding(false));
            serializer1.Serialize(sw, data);
            sw.Close();
            return ResultCodes.Success;
        }
        #endregion

		/// <summary>MCデータ読み込み</summary>
		/// <param name="data">MCより読み込まれたデータ</param>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// MCより情報を取得します。
		/// </remarks>
		private ResultCodes ReadData( out PITCH_ERR_REV_AX data, int axisIndex)
		{
            AidLog logs = new AidLog("McDatPitchAdjust.ReadData");
            ResultCodes ret = ResultCodes.McNotInitialize;
            data = PITCH_ERR_REV_AX.Init();
            while (true == StandBy())
            {
                try
                {
                    int size = Marshal.SizeOf(data);
                    int retRt64 = 0;
                    TechnoMethods method = TechnoMethods.ReceiveData;
                    if (BootModes.Desktop == BootMode)
                    {
                        ret = ResultCodes.Success;
                    }
                    else
                    {
                        retRt64 = Rt64eccomapi.ReceiveData(CommHandle, DataType, Task, axisIndex, ref size, ref data);
                        ret = CheckResultTechno(method, retRt64);
                        if (ResultCodes.Success != ret)
                        {
                            break;
                        }
                    }
                }
                catch (Exception e)
                {
                    bool unexpected = true;
                    if ((e is DllNotFoundException) ||
                        (e is EntryPointNotFoundException))
                    {
                        unexpected = false;   //	想定内の例外。
                    }
                    ret = logs.Exception(e, unexpected);
                }
                break;
            }
            return ret;
        }
	    /// <summary>書き込み</summary>
		/// <param name="data">書き込み情報</param>
        /// <param name="axisIndex">軸番号
        /// X = 0
        /// Y = 1
        /// W = 2
        /// Z = 3
        /// A = 4
        /// B = 5
        /// C = 6
        /// I = 7
        /// </param>
		/// <returns>実行結果</returns>
		private ResultCodes WriteData(PITCH_ERR_REV_AX data, int axisIndex )
		{
            AidLog logs = new AidLog("McDatPitchAdjust.WriteData(axis=" + axisIndex.ToString() + ")");
            //パラメータ有効性チェック
            if( data.RevSpace <= 0 )
            {
                logs.Sure($"InvalidData(RevMagnify : " + data.RevMagnify.ToString()
                                        + ", RevSpace : " + data.RevSpace.ToString() 
                                        + ", RevCnt(M+P) : " + (data.RevMCnt + data.RevPCnt).ToString() 
                                        + ")");
                return ResultCodes.Success;
            }

            ResultCodes ret = ResultCodes.McNotInitialize;
            while (true == StandBy())
            {
                try
                {
                    int size = Marshal.SizeOf(data);
                    int retRt64 = 0;
                    TechnoMethods method = TechnoMethods.SendData;
                    logs.Sure($"SendData({DataTypeName},size={size})");
                    if (BootModes.Desktop == BootMode)
                    {
                        ret = ResultCodes.Success;
                    }
                    else
                    {
                        retRt64 = Rt64eccomapi.SendData(CommHandle, DataType, Task, axisIndex, size, ref data);
                        ret = CheckResultTechno(method, retRt64);
                        if (ResultCodes.Success != ret)
                        {
                            break;
                        }
                    }
                }
                catch (Exception e)
                {
                    bool unexpected = true;
                    if ((e is DllNotFoundException) ||
                        (e is EntryPointNotFoundException))
                    {
                        unexpected = false;   //	想定内の例外。
                    }
                    ret = logs.Exception(e, unexpected);
                }
                break;
            }
            return ret;
        }
        /// <summary>軸ごとの補正情報取得</summary>
        /// <param name="result">取得結果</param>
        /// <returns></returns>
        public ResultCodes ReadData(StructureMcDatPitchAdjustItem result)
        {
            PITCH_ERR_REV_AX data;
            ResultCodes ret = ReadData(out data, (int)(result.AxisNumber));
            if (ResultCodes.Success == ret)
            {
                int axis = (int)result.AxisNumber;              // 軸番号
                result.RevMagnify = data.RevMagnify;// 補正倍率
                result.RevSpace = data.RevSpace;    // 補正間隔
                result.RevTopNo = 0;
                result.RevMCnt = data.RevMCnt;      // －側補正区間数
                result.RevPCnt = data.RevPCnt;      // ＋側補正区間数
                result.SetDeviationsDatas(result.RevTopNo, data.RevDt);
            }
            return ret;
        }
        /// <summary>軸ごとの補正情報取得</summary>
        /// <param name="result">取得結果</param>
        /// <returns></returns>
        public ResultCodes ReadData(StructureMcDatPitchAdjustList result)
        {
            ResultCodes ret = ResultCodes.NotExecute;
            if (ResultCodes.Success == ret)
            {
                if(result != null)
                {
                    result.Clear();
                    result.Dispose();
                    result = null;
                }
                result = new StructureMcDatPitchAdjustList();
                for(int resultCt = 0; 64 > resultCt; resultCt++)
                {
                    PITCH_ERR_REV_AX data;
                    ret = ReadData(out data, resultCt);
                    StructureMcDatPitchAdjustItem item = null;
                    item = new StructureMcDatPitchAdjustItem((AxisNumbers)resultCt);
                    item.RevMagnify = data.RevMagnify;// 補正倍率
                    item.RevSpace = data.RevSpace;    // 補正間隔
                    item.RevTopNo = 0;
                    item.RevMCnt = data.RevMCnt;      // －側補正区間数
                    item.RevPCnt = data.RevPCnt;      // ＋側補正区間数
                    item.SetDeviationsDatas(item.RevTopNo, data.RevDt);
                    result.Add(item);
                }
            }
            return ret;
        }

        /// <summary>軸ごとの補正情報設定</summary>
        /// <param name="result">設定内容</param>
        /// <returns></returns>
        public ResultCodes WriteData(StructureMcDatPitchAdjustItem target)
        {
            PITCH_ERR_REV_AX data;
            ResultCodes ret = ReadData(out data, (int)(target.AxisNumber));
            if (ResultCodes.Success == ret)
            {
                data.RevMagnify = target.RevMagnify;// 補正倍率
                data.RevSpace = target.RevSpace;    // 補正間隔
                data.RevMCnt = target.RevMCnt;      // －側補正区間数
                data.RevPCnt = target.RevPCnt;      // ＋側補正区間数
                int index;
                for (index = 0; index < target.RevDt.Length; ++index)
                {
                    data.RevDt[index] = target.RevDt[index];
                }
                //パラメータ有効性チェック
                if(data.RevSpace <= 0)
                {
                    return ResultCodes.Success;
                }
                ret = WriteData(data, (int)(target.AxisNumber));
            }
            return ret;
        }
    }
}
