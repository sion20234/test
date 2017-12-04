using ECNC3.Enumeration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECNC3.Models.Common;
using System.Xml.Linq;

namespace ECNC3.Models
{
    public class FileMaintenanceTimer:IDisposable
    {
        /// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
        private bool _disposed = false;

        public FileMaintenanceTimer()
        {
            _filePath = FilePathInfo.MasterData + "UIMaintenanse.xml";
            Items = new StructureMaintenanceTimerList();
        }
        /// <summary>
        /// ファイルパス
        /// </summary>
        private string _filePath;
        /// <summary>
        /// メンテナンスタイマー設定
        /// </summary>
        public StructureMaintenanceTimerList Items = null;
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
        /// <summary>
        /// ファイル読込み
        /// </summary>
        /// <returns></returns>
        public ResultCodes Read()
        {
            ResultCodes ret = ResultCodes.Success;
            if(File.Exists(_filePath) == false){ return ResultCodes.FailToReadFile; }
            if(Items != null)
            {
                Items.Clear();
                Items.Dispose();
                Items = null;
            }
            Items = new StructureMaintenanceTimerList();
            XElement tempData = null;
            AidXmlLinq xml = new AidXmlLinq();
            try
            {
                xml.ReadByReadOnly(ref tempData, _filePath);
                int itemNum = 1;
                Items.MainPowerCount = UInt64.Parse(xml.GetAttrText(tempData, "MainPowerOnCount"));
                Items.ProcessPowerCount = UInt64.Parse(xml.GetAttrText(tempData, "ProcessPowerOnCount"));
                foreach (XElement tempItem in tempData.Nodes())
                {
                    if(tempItem.Name == "Item" + itemNum.ToString())
                    {
                        
                        string tempUnit = xml.GetAttrText(tempItem, "Unit");
                        switch (tempUnit)
                        {
                            case "運転時間": 
                            case "放電時間":
                            case "期間":
                                StructureMaintenanceTimerItem item = new StructureMaintenanceTimerItem();
                                item.Number = itemNum;
                                switch(tempUnit)
                                {
                                    case "運転時間": item.Category = MaintenanceTimerCategory.PowerON; break;
                                    case "放電時間": item.Category = MaintenanceTimerCategory.DischargeON; break;
                                    case "期間": item.Category = MaintenanceTimerCategory.DateTime; break;
                                }
                                item.Name = xml.GetAttrText(tempItem, "ItemName");
                               switch(tempUnit)
                                {
                                    case "運転時間":
                                    case "放電時間":
                                        UInt64 tempLimit = 0;
                                        bool retLimit = UInt64.TryParse(xml.GetAttrText(tempItem, "Life"), out tempLimit);
                                        if (retLimit == true) item.Limit = tempLimit;
                                        UInt64 tempValue = 0;
                                        bool retValue = UInt64.TryParse(xml.GetAttrText(tempItem, "Now"), out tempValue);
                                        if (retValue == true) item.Value = tempValue;
                                        break;

                                    case "期間":
                                        item.Limit = ulong.Parse(xml.GetAttrText(tempItem, "Life").Replace("/", ""));
                                        item.Value = ulong.Parse(DateTime.Today.ToShortDateString().Replace("/", ""));
                                        break;
                                }
                                Items.Add(item);
                                itemNum++;
                                break;

                            default: itemNum++; continue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AidException.XmlFilter(ex, ExceptionHandling.MessageDialog);
                ret = ResultCodes.FailToReadFile;
            }
            return ret;
        }
        /// <summary>
        /// ファイル書込み
        /// </summary>
        /// <returns></returns>
        public ResultCodes Write()
        {
            ResultCodes ret = ResultCodes.Success;
            if (File.Exists(_filePath) == false) { return ResultCodes.FailToWriteFile; }
            if (Items == null) return ResultCodes.FailToWriteFile;
            XElement readData = new XElement("Root");
            XElement writeData = new XElement("Root");
            AidXmlLinq xml = new AidXmlLinq();
            try
            {
                xml.SetAttr(writeData, "MainPowerOnCount", Items.MainPowerCount.ToString());
                xml.SetAttr(writeData, "ProcessPowerOnCount", Items.ProcessPowerCount.ToString());
                xml.ReadByReadWrite(ref readData, _filePath);
                foreach (XElement tempReadItem in readData.Nodes())
                {
                    bool added = false;
                    foreach (StructureMaintenanceTimerItem tempItem in Items)
                    {
                        if (tempReadItem.Name == "Item" + tempItem.Number.ToString())
                        {
                            XElement tempChild = new XElement(tempReadItem.Name);
                            xml.SetAttr(tempChild, "ItemName", tempItem.Name);
                           
                            switch (tempItem.Category)
                            {
                                case MaintenanceTimerCategory.PowerON:
                                    xml.SetAttr(tempChild, "Life", tempItem.Limit.ToString());
                                    xml.SetAttr(tempChild, "Now", tempItem.Value.ToString());
                                    xml.SetAttr(tempChild, "Unit", "運転時間");
                                    break;

                                case MaintenanceTimerCategory.DischargeON:
                                    xml.SetAttr(tempChild, "Life", tempItem.Limit.ToString());
                                    xml.SetAttr(tempChild, "Now", tempItem.Value.ToString());
                                    xml.SetAttr(tempChild, "Unit", "放電時間");
                                    break;

                                case MaintenanceTimerCategory.DateTime:
                                    xml.SetAttr(tempChild, "Life", (_DateTimeFrommNumber(tempItem.Limit)));
                                    xml.SetAttr(tempChild, "Unit", "期間");
                                    break;

                                case MaintenanceTimerCategory.NULL:
                                    xml.SetAttr(tempChild, "Unit", "NULL");
                                    break;

                            }
                            writeData.Add(tempChild);
                            added = true;
                        }
                    }
                    if (added == false)
                    {
                        writeData.Add(tempReadItem);
                    }
                }
                //書き込み実行
                xml.Write(writeData, _filePath);
            }
            catch (Exception ex)
            {
                AidException.XmlFilter(ex, ExceptionHandling.MessageDialog);
                ret = ResultCodes.FailToWriteFile;
                return ret;
            }
            return ret;
        }
        private string _DateTimeFrommNumber(ulong date)
        {
            if (date < 19010101) return "1901/01/01";
            string tempDate = date.ToString();
            tempDate = (tempDate.Insert(6, "/")).Insert(4, "/");
            return tempDate;
        }
    }
}
