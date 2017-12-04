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
    public class FileAccountPasswords
    {
        public FileAccountPasswords()
        {
            _filePath = FilePathInfo.MasterData + "Maintenance/AccountSetting.xml";
            Items = new StructureAccountList();
        }
        /// <summary>
        /// ファイルパス
        /// </summary>
        private string _filePath;
        /// <summary>
        /// アカウント設定
        /// </summary>
        public StructureAccountList Items = null;
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
            Items = new StructureAccountList();
            XElement tempData = null;
            AidXmlLinq xml = new AidXmlLinq();
            try
            {
                xml.ReadByReadOnly(ref tempData, _filePath);
                foreach(XElement tempItem in tempData.Nodes())
                {
                    if(tempItem.Name == "Account")
                    {
                        StructureAccountItem item = new StructureAccountItem();
                        item.Number = xml.GetAttrValue(tempItem, "num");
                        item.ID = xml.GetAttrText(tempItem, "id");
                        item.Password = xml.GetAttrText(tempItem, "pass");
                        switch (xml.GetAttrText(tempItem, "level"))
                        {
                            case "Admin": item.Level = AccountLevel.Admin; break;
                            case "MakerAdmin": item.Level = AccountLevel.MakerAdmin; break;
                            case "Maker": item.Level = AccountLevel.Maker; break;
                            case "UserAdmin": item.Level = AccountLevel.UserAdmin; break;
                            case "User": item.Level = AccountLevel.User; break;
                            case "Guest": item.Level = AccountLevel.Guest; break;
                            default: item.Level = AccountLevel.Guest; break;
                        }
                        Items.Add(item);
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
            XElement writeData = new XElement("Root");
            AidXmlLinq xml = new AidXmlLinq();
            try
            {
                foreach (StructureAccountItem tempItem in Items)
                {
                    XElement tempChild = new XElement("Account");
                    xml.SetAttr(tempChild, "num", tempItem.Number.ToString());
                    xml.SetAttr(tempChild, "id", tempItem.ID);
                    xml.SetAttr(tempChild, "pass", tempItem.Password);
                    xml.SetAttr(tempChild, "level", tempItem.Level.ToString());
                    writeData.Add(tempChild);
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
        /// <summary>
        /// 新規作成
        /// </summary>
        /// <returns></returns>
        public ResultCodes Create()
        {
            ResultCodes ret = ResultCodes.Success;
            if(File.Exists(_filePath) == true) return ResultCodes.NotExecute;
            using (FileStream fs = new FileStream(_filePath, FileMode.Create))
            {
                fs.Close();
            }                
            if (Items != null)
            {
                Items.Clear();
                Items.Dispose();
                Items = null;
            }
            Items = new StructureAccountList();
            for(int ct = 1; ct <= 2; ct++)
            {
                StructureAccountItem item = new StructureAccountItem();
                switch (ct)
                {
                    case 1:
                        item.Number = ct;
                        item.ID = "ADMIN";
                        item.Level = AccountLevel.MakerAdmin;
                        item.Password = "8188";
                        break;

                    case 2:
                        item.Number = ct;
                        item.ID = "USER";
                        item.Level = AccountLevel.User;
                        item.Password = "0000";
                        break;

                }
                Items.Add(item);
            }
            ret = Write();
            return ret;
        }
    }
}
