using System;
using System.IO;
using System.Xml;
using ECNC3.Enumeration;
using ECNC3.Models.Common;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Reflection;

namespace ECNC3.Models
{
	/// <summary>設定ファイルアクセス</summary>
	public class FileUIStringTable : FileAccessCommon, IEcnc3Backup, IDisposable
	{
        public static StructureStringList AECForm = null;
        public static StructureStringList AECFuncForm = null;
        public static StructureStringList AlarmLogForm = null;
        public static StructureStringList AxisClearForm = null;
        public static StructureStringList ConditionsCallSetForm = null;
        public static StructureStringList ConditionsForm = null;
        public static StructureStringList EDITForm = null;
        public static StructureStringList ESFMainForm = null;
        public static StructureStringList FileForm = null;
        public static StructureStringList FuncPassForm = null;
        public static StructureStringList GSFMainForm = null;
        public static StructureStringList HelpForm = null;
        public static StructureStringList IOCheckForm = null;
        public static StructureStringList LogForm = null;
        public static StructureStringList LogSettingForm = null;
        public static StructureStringList LogSettingVariableForm = null;
        public static StructureStringList MacroVarSetForm = null;
        public static StructureStringList MAINForm = null;
        public static StructureStringList MaintenanceEditForm = null;
        public static StructureStringList MaintenanceForm = null;
        public static StructureStringList MakerServiceForm = null;
        public static StructureStringList MANUALForm = null;
        public static StructureStringList MaterialNameForm = null;
        public static StructureStringList MDIAUTOForm = null;
        public static StructureStringList NumericFeedForm = null;
        public static StructureStringList OptionForm = null;
        public static StructureStringList ParameterViewForm = null;
        public static StructureStringList PartitionForm = null;
        public static StructureStringList PasswordChangeForm = null;
        public static StructureStringList PitchCompensationForm = null;
        public static StructureStringList PitchSettingForm = null;
        public static StructureStringList PlotForm = null;
        public static StructureStringList ReferencingForm = null;
        public static StructureStringList ReturnToOriginForm = null;
        public static StructureStringList SettingSoft = null;
        public static StructureStringList SystemFileInputForm = null;
        public static StructureStringList SystemTimeAdjustForm = null;
        public static StructureStringList TeachTableForm = null;
        public static StructureStringList ThinLineSettingForm = null;
        public static StructureStringList UserFuncForm = null;
        public static StructureStringList UserServiceForm = null;
        public static StructureStringList ValiableFileForm = null;
        public static StructureStringList ValiableListForm = null;
        public static StructureStringList VersionCHeckForm = null;
        public static StructureStringList WorkSpinOpForm = null;




        /// <summary>XMLファイルドキュメントポインタ</summary>
        private XmlDocument _xmlDoc = null;
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>編集あり</summary>
		private bool _edted = false;
		/// <summary>コンストラクタ</summary>
		public FileUIStringTable()
		{
			Name = this.ToString();
			RegistMasterFile( @"UIStringTable.xml" );
        }

        /// <summary>読み込み</summary>
        /// <returns>実行結果</returns>
        /// <remarks>
        /// XMLファイルの読み込みを行います。
        /// </remarks>
        public ResultCodes Read()
        {
            ResultCodes ret = ResultCodes.Success;
            ret = ReadData(ref AECForm, "AECForm");
            ret = ReadData(ref AECFuncForm, "AECFuncForm");
            ret = ReadData(ref AlarmLogForm, "AlarmLogForm");
            ret = ReadData(ref AxisClearForm, "AxisClearForm");
            ret = ReadData(ref ConditionsCallSetForm, "ConditionsCallSetForm");
            ret = ReadData(ref ConditionsForm, "ConditionsForm");
            ret = ReadData(ref EDITForm, "EDITForm");
            ret = ReadData(ref ESFMainForm, "ESFMainForm");
            ret = ReadData(ref FileForm, "FileForm");
            ret = ReadData(ref FuncPassForm, "FuncPassForm");
            ret = ReadData(ref GSFMainForm, "GSFMainForm");
            ret = ReadData(ref HelpForm, "HelpForm");
            ret = ReadData(ref IOCheckForm, "IOCheckForm");
            ret = ReadData(ref LogForm, "LogForm");
            ret = ReadData(ref LogSettingForm, "LogSettingForm");
            ret = ReadData(ref LogSettingVariableForm, "LogSettingVariableForm");
            ret = ReadData(ref MacroVarSetForm, "MacroVarSetForm");
            ret = ReadData(ref MAINForm, "MAINForm");
            ret = ReadData(ref MaintenanceEditForm, "MaintenanceEditForm");
            ret = ReadData(ref MaintenanceForm, "MaintenanceForm");
            ret = ReadData(ref MakerServiceForm, "MakerServiceForm");
            ret = ReadData(ref MANUALForm, "MANUALForm");
            ret = ReadData(ref MaterialNameForm, "MaterialNameForm");
            ret = ReadData(ref MDIAUTOForm, "MDIAUTOForm");
            ret = ReadData(ref NumericFeedForm, "NumericFeedForm");
            ret = ReadData(ref OptionForm, "OptionForm");
            ret = ReadData(ref ParameterViewForm, "ParameterViewForm");
            ret = ReadData(ref PartitionForm, "PartitionForm");
            ret = ReadData(ref PasswordChangeForm, "PasswordChangeForm");
            ret = ReadData(ref PitchCompensationForm, "PitchCompensationForm");
            ret = ReadData(ref PitchSettingForm, "PitchSettingForm");
            ret = ReadData(ref PlotForm, "PlotForm");
            ret = ReadData(ref ReferencingForm, "ReferencingForm");
            ret = ReadData(ref ReturnToOriginForm, "ReturnToOriginForm");
            ret = ReadData(ref SettingSoft, "SettingSoft");
            ret = ReadData(ref SystemFileInputForm, "SystemFileInputForm");
            ret = ReadData(ref SystemTimeAdjustForm, "SystemTimeAdjustForm");
            ret = ReadData(ref TeachTableForm, "TeachTableForm");
            ret = ReadData(ref ThinLineSettingForm, "ThinLineSettingForm");
            ret = ReadData(ref UserFuncForm, "UserFuncForm");
            ret = ReadData(ref UserServiceForm, "UserServiceForm");
            ret = ReadData(ref ValiableFileForm, "ValiableFileForm");
            ret = ReadData(ref ValiableListForm, "ValiableListForm");
            ret = ReadData(ref VersionCHeckForm, "VersionCHeckForm");
            ret = ReadData(ref WorkSpinOpForm, "WorkSpinOpForm");
            return ret;
        }
        /// <summary>読み込み</summary>
        /// <returns>実行結果</returns>
        /// <remarks>
        /// XMLファイルの読み込みを行います。
        /// </remarks>
        public ResultCodes ReadData(ref StructureStringList Items, string Name)
		{
            if (null != Items)
            {
                Items.Clear();
            }
            else
            {
                Items = new StructureStringList();
            }
            AidLog logs = new AidLog("UIStringTable.Load");
            AidXmlLinq xml = new AidXmlLinq();
            XElement file = null;

            ResultCodes ret = xml.ReadByReadOnly(ref file, FilePath);
            if ((ResultCodes.Success == ret) && (null != file))
            {
                try
                {
                    XElement parent = file.Element(Name);
                    IEnumerable<XElement> list = parent.Elements("Item");
                    if (null == list)
                    {
                        ret = ResultCodes.FailToReadFile;
                    }
                    else
                    {
                        foreach (XElement xe in list)
                        {
                            using (StructureStringItem item = new StructureStringItem())
                            {
                                item.Name = xml.GetAttrText(xe, "name");
                                item.Value = xml.GetAttrText(xe, "value");
                                Items.Add(item.Clone() as StructureStringItem);
                            }
                        }
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

        /// <summary>ファイル保存</summary>
        /// <param name="target">書き換え対象のインスタンス</param>
        /// <returns>実行結果</returns>
        /// <remarks>
        /// 引数 target の情報をファイルに追記します。
        /// </remarks>
        public ResultCodes Write()
        {
            AidLog logs = new AidLog("UIStringTable.Write");
            AidXmlLinq xml = new AidXmlLinq();
            ResultCodes ret = ResultCodes.Success;
            //if(File.Exists(FilePath))
            //{
            //    File.Delete(FilePath);
            //}
            //File.Create(FilePath);
            try
            {
                MakeElement().Save(FilePath);
                //ret = xml.Write(MakeElement(), FilePath);
            }
            catch (Exception e)
            {
                ret = logs.Exception(e, false);
            }
            return ret;
        }
        /// <summary>要素の作成</summary>
        /// <remarks>
        /// クラス構造に沿ったXML要素を作成します。
        /// </remarks>
        /// <returns>生成された要素</returns>
        public XElement MakeElement()
        {
            XElement file = new XElement("Root");
            file.Add(new XElement(MakeElement(ref AECForm, "AECForm")));
            file.Add(new XElement(MakeElement(ref AECFuncForm, "AECFuncForm")));
            file.Add(new XElement(MakeElement(ref AlarmLogForm, "AlarmLogForm")));
            file.Add(new XElement(MakeElement(ref AxisClearForm, "AxisClearForm")));
            file.Add(new XElement(MakeElement(ref ConditionsCallSetForm, "ConditionsCallSetForm")));
            file.Add(new XElement(MakeElement(ref ConditionsForm, "ConditionsForm")));
            file.Add(new XElement(MakeElement(ref EDITForm, "EDITForm")));
            file.Add(new XElement(MakeElement(ref ESFMainForm, "ESFMainForm")));
            file.Add(new XElement(MakeElement(ref FileForm, "FileForm")));
            file.Add(new XElement(MakeElement(ref FuncPassForm, "FuncPassForm")));
            file.Add(new XElement(MakeElement(ref GSFMainForm, "GSFMainForm")));
            file.Add(new XElement(MakeElement(ref HelpForm, "HelpForm")));
            file.Add(new XElement(MakeElement(ref IOCheckForm, "IOCheckForm")));
            file.Add(new XElement(MakeElement(ref LogForm, "LogForm")));
            file.Add(new XElement(MakeElement(ref LogSettingForm, "LogSettingForm")));
            file.Add(new XElement(MakeElement(ref LogSettingVariableForm, "LogSettingVariableForm")));
            file.Add(new XElement(MakeElement(ref MacroVarSetForm, "MacroVarSetForm")));
            file.Add(new XElement(MakeElement(ref MAINForm, "MAINForm")));
            file.Add(new XElement(MakeElement(ref MaintenanceEditForm, "MaintenanceEditForm")));
            file.Add(new XElement(MakeElement(ref MaintenanceForm, "MaintenanceForm")));
            file.Add(new XElement(MakeElement(ref MakerServiceForm, "MakerServiceForm")));
            file.Add(new XElement(MakeElement(ref MANUALForm, "MANUALForm")));
            file.Add(new XElement(MakeElement(ref MaterialNameForm, "MaterialNameForm")));
            file.Add(new XElement(MakeElement(ref MDIAUTOForm, "MDIAUTOForm")));
            file.Add(new XElement(MakeElement(ref NumericFeedForm, "NumericFeedForm")));
            file.Add(new XElement(MakeElement(ref OptionForm, "OptionForm")));
            file.Add(new XElement(MakeElement(ref ParameterViewForm, "ParameterViewForm")));
            file.Add(new XElement(MakeElement(ref PartitionForm, "PartitionForm")));
            file.Add(new XElement(MakeElement(ref PasswordChangeForm, "PasswordChangeForm")));
            file.Add(new XElement(MakeElement(ref PitchCompensationForm, "PitchCompensationForm")));
            file.Add(new XElement(MakeElement(ref PitchSettingForm, "PitchSettingForm")));
            file.Add(new XElement(MakeElement(ref PlotForm, "PlotForm")));
            file.Add(new XElement(MakeElement(ref ReferencingForm, "ReferencingForm")));
            file.Add(new XElement(MakeElement(ref ReturnToOriginForm, "ReturnToOriginForm")));
            file.Add(new XElement(MakeElement(ref SettingSoft, "SettingSoft")));
            file.Add(new XElement(MakeElement(ref SystemFileInputForm, "SystemFileInputForm")));
            file.Add(new XElement(MakeElement(ref SystemTimeAdjustForm, "SystemTimeAdjustForm")));
            file.Add(new XElement(MakeElement(ref TeachTableForm, "TeachTableForm")));
            file.Add(new XElement(MakeElement(ref ThinLineSettingForm, "ThinLineSettingForm")));
            file.Add(new XElement(MakeElement(ref UserFuncForm, "UserFuncForm")));
            file.Add(new XElement(MakeElement(ref UserServiceForm, "UserServiceForm")));
            file.Add(new XElement(MakeElement(ref ValiableFileForm, "ValiableFileForm")));
            file.Add(new XElement(MakeElement(ref ValiableListForm, "ValiableListForm")));
            file.Add(new XElement(MakeElement(ref VersionCHeckForm, "VersionCHeckForm")));
            file.Add(new XElement(MakeElement(ref WorkSpinOpForm, "WorkSpinOpForm")));
            return file;
        }
        /// <summary>要素の作成</summary>
        /// <remarks>
        /// クラス構造に沿ったXML要素を作成します。
        /// </remarks>
        /// <returns>生成された要素</returns>
        public XElement MakeElement(ref StructureStringList Items, string Name)
        {
            if(Items == null)
            {
                Items = new StructureStringList();
                for (int ict = 0; ict < 10; ict++)
                {
                    StructureStringItem item = new StructureStringItem();
                    item.Name = ict.ToString();
                    item.Value = (ict + 1).ToString();
                    Items.Add(item);
                }
            }
            
            XElement ret = new XElement(Name);
            
            foreach(StructureStringItem item in Items)
            {
                XElement node = new XElement("Item", 
                new XAttribute("name", item.Name), 
                new XAttribute("value", item.Value));
                ret.Add(node);
            }
            
            return ret;
        }
        /// <summary>
        /// 全リストのクリア関数呼び出し
        /// </summary>
        public static void StringTablesClear()
        {
            if(AECForm != null) AECForm.Clear();
            if(AECFuncForm != null)AECFuncForm.Clear();
            if(AlarmLogForm != null)AlarmLogForm.Clear();
            if(AxisClearForm != null)AxisClearForm.Clear();
            if(ConditionsCallSetForm != null)ConditionsCallSetForm.Clear();
            if(ConditionsForm != null)ConditionsForm.Clear();
            if(EDITForm != null)EDITForm.Clear();
            if(ESFMainForm != null)ESFMainForm.Clear();
            if(FileForm != null)FileForm.Clear();
            if(FuncPassForm != null)FuncPassForm.Clear();
            if(GSFMainForm != null)GSFMainForm.Clear();
            if(HelpForm != null)HelpForm.Clear();
            if(IOCheckForm != null)IOCheckForm.Clear();
            if(LogForm != null)LogForm.Clear();
            if(LogSettingForm != null)LogSettingForm.Clear();
            if(LogSettingVariableForm != null)LogSettingVariableForm.Clear();
            if(MacroVarSetForm != null)MacroVarSetForm.Clear();
            if(MAINForm != null)MAINForm.Clear();
            if(MaintenanceEditForm != null)MaintenanceEditForm.Clear();
            if(MaintenanceForm != null)MaintenanceForm.Clear();
            if(MakerServiceForm != null)MakerServiceForm.Clear();
            if(MANUALForm != null)MANUALForm.Clear();
            if(MaterialNameForm != null)MaterialNameForm.Clear();
            if(MDIAUTOForm != null)MDIAUTOForm.Clear();
            if(NumericFeedForm != null)NumericFeedForm.Clear();
            if(OptionForm != null)OptionForm.Clear();
            if(ParameterViewForm != null)ParameterViewForm.Clear();
            if(PartitionForm != null)PartitionForm.Clear();
            if(PasswordChangeForm != null)PasswordChangeForm.Clear();
            if(PitchCompensationForm != null)PitchCompensationForm.Clear();
            if(PitchSettingForm != null)PitchSettingForm.Clear();
            if(PlotForm != null)PlotForm.Clear();
            if(ReferencingForm != null)ReferencingForm.Clear();
            if(ReturnToOriginForm != null)ReturnToOriginForm.Clear();
            if(SettingSoft != null)SettingSoft.Clear();
            if(SystemFileInputForm != null)SystemFileInputForm.Clear();
            if(SystemTimeAdjustForm != null)SystemTimeAdjustForm.Clear();
            if(TeachTableForm != null)TeachTableForm.Clear();
            if(ThinLineSettingForm != null)ThinLineSettingForm.Clear();
            if(UserFuncForm != null)UserFuncForm.Clear();
            if(UserServiceForm != null)UserServiceForm.Clear();
            if(ValiableFileForm != null)ValiableFileForm.Clear();
            if(ValiableListForm != null)ValiableListForm.Clear();
            if(VersionCHeckForm != null)VersionCHeckForm.Clear();
            if(WorkSpinOpForm != null)WorkSpinOpForm.Clear();
        }
        /// <summary>
        /// 全リストのTrimExess関数呼び出し
        /// </summary>
        public static void StringTablesTrimExcess()
        {
            if (AECForm != null) AECForm.TrimExcess();
            if (AECFuncForm != null) AECFuncForm.TrimExcess();
            if (AlarmLogForm != null) AlarmLogForm.TrimExcess();
            if (AxisClearForm != null) AxisClearForm.TrimExcess();
            if (ConditionsCallSetForm != null) ConditionsCallSetForm.TrimExcess();
            if (ConditionsForm != null) ConditionsForm.TrimExcess();
            if (EDITForm != null) EDITForm.TrimExcess();
            if (ESFMainForm != null) ESFMainForm.TrimExcess();
            if (FileForm != null) FileForm.TrimExcess();
            if (FuncPassForm != null) FuncPassForm.TrimExcess();
            if (GSFMainForm != null) GSFMainForm.TrimExcess();
            if (HelpForm != null) HelpForm.TrimExcess();
            if (IOCheckForm != null) IOCheckForm.TrimExcess();
            if (LogForm != null) LogForm.TrimExcess();
            if (LogSettingForm != null) LogSettingForm.TrimExcess();
            if (LogSettingVariableForm != null) LogSettingVariableForm.TrimExcess();
            if (MacroVarSetForm != null) MacroVarSetForm.TrimExcess();
            if (MAINForm != null) MAINForm.TrimExcess();
            if (MaintenanceEditForm != null) MaintenanceEditForm.TrimExcess();
            if (MaintenanceForm != null) MaintenanceForm.TrimExcess();
            if (MakerServiceForm != null) MakerServiceForm.TrimExcess();
            if (MANUALForm != null) MANUALForm.TrimExcess();
            if (MaterialNameForm != null) MaterialNameForm.TrimExcess();
            if (MDIAUTOForm != null) MDIAUTOForm.TrimExcess();
            if (NumericFeedForm != null) NumericFeedForm.TrimExcess();
            if (OptionForm != null) OptionForm.TrimExcess();
            if (ParameterViewForm != null) ParameterViewForm.TrimExcess();
            if (PartitionForm != null) PartitionForm.TrimExcess();
            if (PasswordChangeForm != null) PasswordChangeForm.TrimExcess();
            if (PitchCompensationForm != null) PitchCompensationForm.TrimExcess();
            if (PitchSettingForm != null) PitchSettingForm.TrimExcess();
            if (PlotForm != null) PlotForm.TrimExcess();
            if (ReferencingForm != null) ReferencingForm.TrimExcess();
            if (ReturnToOriginForm != null) ReturnToOriginForm.TrimExcess();
            if (SettingSoft != null) SettingSoft.TrimExcess();
            if (SystemFileInputForm != null) SystemFileInputForm.TrimExcess();
            if (SystemTimeAdjustForm != null) SystemTimeAdjustForm.TrimExcess();
            if (TeachTableForm != null) TeachTableForm.TrimExcess();
            if (ThinLineSettingForm != null) ThinLineSettingForm.TrimExcess();
            if (UserFuncForm != null) UserFuncForm.TrimExcess();
            if (UserServiceForm != null) UserServiceForm.TrimExcess();
            if (ValiableFileForm != null) ValiableFileForm.TrimExcess();
            if (ValiableListForm != null) ValiableListForm.TrimExcess();
            if (VersionCHeckForm != null) VersionCHeckForm.TrimExcess();
            if (WorkSpinOpForm != null) WorkSpinOpForm.TrimExcess();
        }
        /// <summary>
        /// 全リストのNull設定
        /// </summary>
        public static void StringTablesNullSet()
        {
            if (AECForm != null) AECForm = null;
            if (AECFuncForm != null) AECFuncForm = null;
            if (AlarmLogForm != null) AlarmLogForm = null;
            if (AxisClearForm != null) AxisClearForm = null;
            if (ConditionsCallSetForm != null) ConditionsCallSetForm = null;
            if (ConditionsForm != null) ConditionsForm = null;
            if (EDITForm != null) EDITForm = null;
            if (ESFMainForm != null) ESFMainForm = null;
            if (FileForm != null) FileForm = null;
            if (FuncPassForm != null) FuncPassForm = null;
            if (GSFMainForm != null) GSFMainForm = null;
            if (HelpForm != null) HelpForm = null;
            if (IOCheckForm != null) IOCheckForm = null;
            if (LogForm != null) LogForm = null;
            if (LogSettingForm != null) LogSettingForm = null;
            if (LogSettingVariableForm != null) LogSettingVariableForm = null;
            if (MacroVarSetForm != null) MacroVarSetForm = null;
            if (MAINForm != null) MAINForm = null;
            if (MaintenanceEditForm != null) MaintenanceEditForm = null;
            if (MaintenanceForm != null) MaintenanceForm = null;
            if (MakerServiceForm != null) MakerServiceForm = null;
            if (MANUALForm != null) MANUALForm = null;
            if (MaterialNameForm != null) MaterialNameForm = null;
            if (MDIAUTOForm != null) MDIAUTOForm = null;
            if (NumericFeedForm != null) NumericFeedForm = null;
            if (OptionForm != null) OptionForm = null;
            if (ParameterViewForm != null) ParameterViewForm = null;
            if (PartitionForm != null) PartitionForm = null;
            if (PasswordChangeForm != null) PasswordChangeForm = null;
            if (PitchCompensationForm != null) PitchCompensationForm = null;
            if (PitchSettingForm != null) PitchSettingForm = null;
            if (PlotForm != null) PlotForm = null;
            if (ReferencingForm != null) ReferencingForm = null;
            if (ReturnToOriginForm != null) ReturnToOriginForm = null;
            if (SettingSoft != null) SettingSoft = null;
            if (SystemFileInputForm != null) SystemFileInputForm = null;
            if (SystemTimeAdjustForm != null) SystemTimeAdjustForm = null;
            if (TeachTableForm != null) TeachTableForm = null;
            if (ThinLineSettingForm != null) ThinLineSettingForm = null;
            if (UserFuncForm != null) UserFuncForm = null;
            if (UserServiceForm != null) UserServiceForm = null;
            if (ValiableFileForm != null) ValiableFileForm = null;
            if (ValiableListForm != null) ValiableListForm = null;
            if (VersionCHeckForm != null) VersionCHeckForm = null;
            if (WorkSpinOpForm != null) WorkSpinOpForm = null;
        }
        /// <summary>
        /// 全リストのTrimExess関数呼び出し
        /// </summary>
        public static void StringTablesInit()
        {
            if (AECForm == null) AECForm= new StructureStringList();
            if (AECFuncForm == null) AECFuncForm= new StructureStringList();
            if (AlarmLogForm == null) AlarmLogForm= new StructureStringList();
            if (AxisClearForm == null) AxisClearForm= new StructureStringList();
            if (ConditionsCallSetForm == null) ConditionsCallSetForm= new StructureStringList();
            if (ConditionsForm == null) ConditionsForm= new StructureStringList();
            if (EDITForm == null) EDITForm= new StructureStringList();
            if (ESFMainForm == null) ESFMainForm= new StructureStringList();
            if (FileForm == null) FileForm= new StructureStringList();
            if (FuncPassForm == null) FuncPassForm= new StructureStringList();
            if (GSFMainForm == null) GSFMainForm= new StructureStringList();
            if (HelpForm == null) HelpForm= new StructureStringList();
            if (IOCheckForm == null) IOCheckForm= new StructureStringList();
            if (LogForm == null) LogForm= new StructureStringList();
            if (LogSettingForm == null) LogSettingForm= new StructureStringList();
            if (LogSettingVariableForm == null) LogSettingVariableForm= new StructureStringList();
            if (MacroVarSetForm == null) MacroVarSetForm= new StructureStringList();
            if (MAINForm == null) MAINForm= new StructureStringList();
            if (MaintenanceEditForm == null) MaintenanceEditForm= new StructureStringList();
            if (MaintenanceForm == null) MaintenanceForm= new StructureStringList();
            if (MakerServiceForm == null) MakerServiceForm= new StructureStringList();
            if (MANUALForm == null) MANUALForm= new StructureStringList();
            if (MaterialNameForm == null) MaterialNameForm= new StructureStringList();
            if (MDIAUTOForm == null) MDIAUTOForm= new StructureStringList();
            if (NumericFeedForm == null) NumericFeedForm= new StructureStringList();
            if (OptionForm == null) OptionForm= new StructureStringList();
            if (ParameterViewForm == null) ParameterViewForm= new StructureStringList();
            if (PartitionForm == null) PartitionForm= new StructureStringList();
            if (PasswordChangeForm == null) PasswordChangeForm= new StructureStringList();
            if (PitchCompensationForm == null) PitchCompensationForm= new StructureStringList();
            if (PitchSettingForm == null) PitchSettingForm= new StructureStringList();
            if (PlotForm == null) PlotForm= new StructureStringList();
            if (ReferencingForm == null) ReferencingForm= new StructureStringList();
            if (ReturnToOriginForm == null) ReturnToOriginForm= new StructureStringList();
            if (SettingSoft == null) SettingSoft= new StructureStringList();
            if (SystemFileInputForm == null) SystemFileInputForm= new StructureStringList();
            if (SystemTimeAdjustForm == null) SystemTimeAdjustForm= new StructureStringList();
            if (TeachTableForm == null) TeachTableForm= new StructureStringList();
            if (ThinLineSettingForm == null) ThinLineSettingForm= new StructureStringList();
            if (UserFuncForm == null) UserFuncForm= new StructureStringList();
            if (UserServiceForm == null) UserServiceForm= new StructureStringList();
            if (ValiableFileForm == null) ValiableFileForm= new StructureStringList();
            if (ValiableListForm == null) ValiableListForm= new StructureStringList();
            if (VersionCHeckForm == null) VersionCHeckForm= new StructureStringList();
            if (WorkSpinOpForm == null) WorkSpinOpForm= new StructureStringList();
        }

        /// <summary>バックアップ</summary>
        /// <param name="backupDirectory">バックアップ先ディレクトリパス</param>
        /// <returns>実行結果</returns>
        public ResultCodes Backup( string backupDirectory )
		{
			return base.Backup( FilePath, backupDirectory );
		}
		/// <summary>リストア</summary>
		/// <param name="restoreDirectory">復元元情報の格納されたディレクトリパス</param>
		/// <returns>実行結果</returns>
		public ResultCodes Restore( string restoreDirectory )
		{
			return base.Restore( restoreDirectory, FilePath );
		}

		/// <summary>ノードリスト取得</summary>
		/// <param name="pathXe">要素のXPath</param>
		/// <returns>取得されたノードリスト</returns>
		public XmlNodeList GetList( string pathXe )
		{
			XmlNodeList list = _xmlDoc.SelectNodes( pathXe );
			return list;
		}

		/// <summary>インスタンスの破棄</summary>
		public void Dispose()
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
		private void Dispose( bool disposing )
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
				;   //  基底クラスのDispose()を確実に呼び出す。
					//base.Dispose( disposing );
			}
		}
		/// <summary>属性値取得(実数)</summary>
		/// <param name="element">要素XPath</param>
		/// <param name="attr">属性名</param>
		/// <returns>取得結果</returns>
		public double AttrDouble( string element, string attr )
		{
			double result = 0.0;
			string text = AttrText( element, attr );
			AidConvert cnv = new AidConvert();
			if( false == cnv.TryParse( text, out result ) ) {
				result = 0.0;
			}
			return result;
		}
		/// <summary>属性値取得(整数)</summary>
		/// <param name="element">要素XPath</param>
		/// <param name="attr">属性名</param>
		/// <returns>取得結果</returns>
		public int AttrValue( string element, string attr )
		{
			AidXml xml = new AidXml();
#if __KEY_LOG_PARSE_XML__
			AidLog logs = new AidLog( "FileSetting.AttrValue" );
			int result = xml.AttrValue( _xmlDoc, element, attr );
			if( 0 == result ) {
				logs.Error( $"{element}/@{attr}= <FAIL TO GET!>." );
			} else {
				logs.Debug( $"{element}/@{attr}={result}." );
			}
			return result;
#else
			return xml.AttrValue( _xmlDoc, element, attr );
#endif
		}
		/// <summary>属性値取得(bool)</summary>
		/// <param name="element">要素XPath</param>
		/// <param name="attr">属性名</param>
		/// <returns>取得結果</returns>
		public bool AttrBool( string element, string attr )
		{
			return ( 0 != AttrValue( element, attr ) ) ? true : false;
		}
		/// <summary>属性値取得(文字列)</summary>
		/// <param name="element">要素XPath</param>
		/// <param name="attr">属性名</param>
		/// <returns>取得結果</returns>
		public string AttrText( string element, string attr )
		{
			AidXml xml = new AidXml();
#if __KEY_LOG_PARSE_XML__
			AidLog logs = new AidLog( "FileSetting.AttrText" );
			string result = xml.AttrText( _xmlDoc, element, attr );
			if( true == string.IsNullOrEmpty( result ) ) {
				logs.Error( $"{element}/@{attr}= <FAIL TO GET!>." );
			} else {
				logs.Debug( $"{element}/@{attr}={result}." );
			}
			return result;
#else
			return xml.AttrText( _xmlDoc, element, attr );
#endif
		}
		/// <summary>属性編集</summary>
		/// <param name="element">要素名</param>
		/// <param name="attr">属性名</param>
		/// <param name="val">設定値</param>
		/// <returns>
		///		<list type="bullet" >
		///			<item>true=編集あり</item>
		///			<item>false=編集なし</item>
		///		</list>
		/// </returns>
		public bool WriteAttr( string element, string attr, string val )
		{
			if( null != _xmlDoc ) {
				XmlNodeList list = _xmlDoc.SelectNodes( element );
				if( null != list ) {
					if( 0 < list.Count ) {
						//	複数検出されたとしても上端の取得結果のみを解析する。
						if( null != list[0].Attributes ) {
							//	属性がある→属性値を検索する。
							return WriteAttr( list[0].Attributes, attr, val );
						}
					}
				}
			}
			return false;
		}
		/// <summary>属性値編集</summary>
		/// <param name="element">要素パス</param>
		/// <param name="attr">属性名</param>
		/// <param name="val">設定値</param>
		/// <param name="number">リスト番号</param>
		/// <returns>
		///		<list type="bullet" >
		///			<item>true=編集あり</item>
		///			<item>false=編集なし</item>
		///		</list>
		/// </returns>
		public bool WriteAttr( string element, string attr, string val, string name )
		{
			if( null != _xmlDoc ) {
				XmlNodeList list = _xmlDoc.SelectNodes( element );
				if( null != list ) {
					if( 0 < list.Count ) {
						foreach( XmlNode item in list ) {
							if( null != item.Attributes ) {
								if( false == IsMatchIndexNumber( item.Attributes, name ) ) {
									continue;
								}
								return WriteAttr( item.Attributes, attr, val );
							}
						}
					}
				}
			}
			return false;
		}
		/// <summary>属性書き込み</summary>
		/// <param name="list">属性リスト</param>
		/// <param name="attr">属性名</param>
		/// <param name="val">設定値</param>
		/// <returns>書き込みの有無</returns>
		private bool WriteAttr( XmlAttributeCollection list, string attr, string val )
		{
			string name = string.Empty;
			foreach( XmlAttribute attr1 in list ) {
				name = attr1.Name;
				if( 0 != string.Compare( name, attr, false ) ) {
					continue;
				}
				if( 0 != string.Compare( attr1.Value, val, StringComparison.OrdinalIgnoreCase ) ) {
					attr1.Value = val;
					_edted = true;
					return true;
				}
				break;
			}
			return false;
		}
        /// <summary>インデックス番号の検索</summary>
        /// <param name="list">検索先の属性値リスト</param>
        /// <param name="name">検索キー</param>
        /// <returns>
        ///		<list type="bullet" >
        ///			<item>true=一致</item>
        ///			<item>false=不一致</item>
        ///		</list>
        /// </returns>
        /// <remarks>
        /// リスト構造を持つノードのインデックス番号の一致判定を行います。
        /// ノードにはインデックス番号の定義として「num」属性値を存在することを前提としています。
        /// </remarks>
        private bool IsMatchIndexNumber(XmlAttributeCollection list, string name)
        {
            string key = name.ToString();
            foreach (XmlAttribute attr in list)
            {
                if (0 == string.Compare("name", attr.Name, false))
                {
                    if (0 == string.Compare(key, attr.Value, false))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
