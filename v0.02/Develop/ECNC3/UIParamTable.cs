using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using ECNC3.Enumeration;
using ECNC3.Models.Common;

namespace ECNC3.Models
{
    public enum ParamCategory
    {
        INITIAL,
        SERVO,
        GMCODE,
        PITCH
    }
    /// <summary>加工条件パラメータアクセス</summary>
    public class UIParamTable : FileAccessCommon, IEcnc3Backup, IDisposable
    {
        /// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
        private bool _disposed = false;
        /// <summary>コンストラクタ</summary>
        public UIParamTable()
        {
            Name = this.ToString();
            RegistMasterFile(@"ParamTable.xml");
        }
        /// <summary>番号</summary>
        public StructureParamTableList Items { get; set; }
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
        private void Dispose(bool disposing)
        {
            try
            {
                if (false == _disposed)
                {
                    if (true == disposing)
                    {
                        //  マネージリソースの解放
                        if (null != Items)
                        {
                            Items.Dispose();
                            Items = null;
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
        /// <summary>読み込み</summary>
        /// <returns>実行結果</returns>
        public ResultCodes Read(ParamCategory cat)
        {
            if (null != Items)
            {
                Items.Clear();
            }
            else
            {
                Items = new StructureParamTableList();
            }
            AidXmlLinq xml = new AidXmlLinq();
            AidLog logs = new AidLog("UIParamTable.Load");
            XElement file = null;
            ResultCodes ret = xml.ReadByReadOnly(ref file, FilePath);
            if ((ResultCodes.Success == ret) && (null != file))
            {
                try
                {
                    switch(cat)
                    {
                        case ParamCategory.INITIAL:
                            XElement _initialXml = file.Element("Initial");
                            if (null != _initialXml)
                            {
                                Read(_initialXml);
                            }
                            break;

                        case ParamCategory.SERVO:
                            XElement _servoXml = file.Element("Servo");
                            if (null != _servoXml)
                            {
                                Read(_servoXml);
                            }
                            break;
                    }                    
                }
                catch (Exception e)
                {
                    bool unexpected = true;
                    if ((e is ObjectDisposedException) ||
                        (e is ArgumentOutOfRangeException) ||
                        (e is System.Threading.AbandonedMutexException) ||
                        (e is FileNotFoundException) ||
                        (e is XmlException) ||
                        (e is FormatException) ||
                        (e is NullReferenceException) ||
                        (e is IOException))
                    {
                        unexpected = false;   //	想定内の例外。
                    }
                    ret = logs.Exception(e, unexpected);
                }
            }
            return ret;
        }
        /// <summary>材料情報読み込み</summary>
        /// <param name="root">XMLルート要素</param>
        /// <param name="result">取得結果</param>
        /// <returns>実行結果</returns>
        private ResultCodes Read(XElement root)
        {
            if ((null == root))
            {
                return ResultCodes.InvalidArgument;
            }
            AidXmlLinq xml = new AidXmlLinq();
            IEnumerable<XElement> list = root.Elements("Item");
            foreach (XElement xe in list)
            {
                StructureParamTableItem item = new StructureParamTableItem();
                item.Number = xml.GetAttrValue(xe, "num");
                item.ViewName = xml.GetAttrText(xe, "nam");
                item.Lower = xml.GetAttrText(xe, "lower");
                item.Upper = xml.GetAttrText(xe, "upper");
                Items.Add(item);
            }
            return ResultCodes.Success;
        }


        ///// <summary>材料名称保存</summary>
        ///// <param name="target">CAPインスタンスの参照</param>
        ///// <returns>実行結果</returns>
        //public ResultCodes WriteCap(StructureDataItem target)
        //{
        //    return Write(target, "Cap");
        //}
        ///// <summary>材料名称保存</summary>
        ///// <param name="target">IPインスタンスの参照</param>
        ///// <returns>実行結果</returns>
        //public ResultCodes WriteIp(StructureDataItem target)
        //{
        //    return Write(target, "Ip");
        //}
        ///// <summary>材料名称保存</summary>
        ///// <param name="target">材料名称インスタンスの参照</param>
        ///// <param name="elementName">ルート要素名</param>
        ///// <returns>実行結果</returns>
        //private ResultCodes Write(StructureDataItem target, string elementName)
        //{
        //    ResultCodes ret = ResultCodes.Success;
        //    using (AidMutex mutex = new AidMutex(FilePath))
        //    {
        //        while (true)
        //        {
        //            AidLog logs = new AidLog("FileProcessConditionPrameter.Save");
        //            try
        //            {
        //                AidXmlLinq xml = new AidXmlLinq();
        //                XElement file = null;
        //                //	XMLファイル読み込み
        //                ret = xml.ReadByReadWrite(ref file, FilePath);
        //                if (ResultCodes.Success != ret)
        //                {
        //                    break;
        //                }
        //                //	プロファイル更新
        //                ret = xml.UpdateProfile(file);
        //                if (ResultCodes.Success != ret)
        //                {
        //                    break;
        //                }
        //                //	対象の要素を検索
        //                XElement root = file.Element(elementName);
        //                if (null != root)
        //                {
        //                    XElement xe = null;
        //                    ret = xml.FindNumber(root, target.Number, ref xe);
        //                    if (ResultCodes.Success != ret)
        //                    {
        //                        break;
        //                    }
        //                    if (null == xe)
        //                    {
        //                        root.Add(new XElement("Item",
        //                                new XAttribute("num", $"{target.Number}"),
        //                                new XAttribute("val", $"{target.Value}"),
        //                                new XAttribute("sf02", $"{target.SF02}")));
        //                    }
        //                    else
        //                    {
        //                        xml.SetAttr(xe, "val", $"{target.Value}");
        //                        xml.SetAttr(xe, "sf02", $"{target.SF02}");
        //                    }
        //                    file.Save(FilePath);
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                ret = logs.Exception(e, false);
        //            }
        //            if (ResultCodes.Success != ret)
        //            {
        //                break;  //	キャッチされた場合のための判定分です。
        //            }
        //            return ResultCodes.Success;
        //        }
        //    }
        //    return ret;
        //}
        /// <summary>バックアップ</summary>
        /// <param name="backupDirectory">バックアップ先ディレクトリパス</param>
        /// <returns>実行結果</returns>
        public ResultCodes Backup(string backupDirectory)
        {
            return base.Backup(FilePath, backupDirectory);
        }
        /// <summary>リストア</summary>
        /// <param name="restoreDirectory">復元元情報の格納されたディレクトリパス</param>
        /// <returns>実行結果</returns>
        public ResultCodes Restore(string restoreDirectory)
        {
            return base.Restore(restoreDirectory, FilePath);
        }
    }
}






