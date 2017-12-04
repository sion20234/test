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
	/// <summary>加工条件パラメータアクセス</summary>
	public class FileProcessConditionParameter : FileAccessCommon, IEcnc3Backup, IDisposable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>コンストラクタ</summary>
		public FileProcessConditionParameter()
		{
			Name = this.ToString();
			RegistMasterFile( @"PdataParam.xml" );
            Limit = new StructureProcessConditionLimitItem();

        }
		/// <summary>材料名称</summary>
		public StructureMaterialNameList Materials { get; private set; }
        /// <summary>CapBit Data</summary>
        public StructurePcondCapBitItem CapsBit { get; private set; }
        /// <summary>C Data</summary>
		public StructureDataList Caps { get; private set; }
        /// <summary>IPBit Data</summary>
        public StructurePcondIpBitItem IpsBit { get; private set; }
		/// <summary>IP Data</summary>
		public StructureDataList Ips { get; private set; }
        /// <summary>SFIPBit Data</summary>
        public StructurePcondSFIpBitItem SFIpsBit { get; private set; }
        /// <summary>SFIP Data</summary>
		public StructureDataList SFIps { get; private set; }

        /// <summary>OverRide Data</summary>
		public StructureDataList OverRides { get; private set; }

        public StructureProcessConditionLimitItem Limit { get; private set; }



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
						if( null != Materials ) {
							Materials.Dispose();
							Materials = null;
						}
						if( null != Caps ) {
							Caps.Dispose();
							Caps = null;
						}
						if( null != Ips ) {
							Ips.Dispose();
							Ips = null;
						}
                        if (null != SFIps)
                        {
                            SFIps.Dispose();
                            SFIps = null;
                        }
                        if (null != OverRides)
                        {
                            OverRides.Dispose();
                            OverRides = null;
                        }
                        if(null != IpsBit)
                        {
                            IpsBit.Dispose();
                            IpsBit = null;
                        }
                        if (null != SFIpsBit)
                        {
                            SFIpsBit.Dispose();
                            SFIpsBit = null;
                        }
                        if (null != CapsBit)
                        {
                            CapsBit.Dispose();
                            CapsBit = null;
                        }
                        if(null != Limit)
                        {
                            Limit.Dispose();
                            Limit = null;
                        }
                    }
					//  アンマネージリソースの解放
				}
				_disposed = true;
			} finally {
				;   //  基底クラスのDispose()を確実に呼び出す。
					//base.Dispose( disposing );
			}
		}
		/// <summary>読み込み</summary>
		/// <returns>実行結果</returns>
		public ResultCodes Read()
		{
			if( null != Materials ) {
				Materials.Clear();
			} else {
				Materials = new StructureMaterialNameList();
			}
			if( null != Caps ) {
				Caps.Clear();
			} else {
				Caps = new StructureDataList();
			}
			if( null != Ips ) {
				Ips.Clear();
			} else {
				Ips = new StructureDataList();
			}
            if (null != SFIps)
            {
                SFIps.Clear();
            } else {
                SFIps = new StructureDataList();
            }
            if (null != OverRides) {
                OverRides.Clear();
            } else {
                OverRides = new StructureDataList();
            }
            if (null != IpsBit)
            {
                IpsBit.Clear();
            } else {
                IpsBit = new StructurePcondIpBitItem();
            }
            if (null != SFIpsBit)
            {
                SFIpsBit.Clear();
            } else {
                SFIpsBit = new StructurePcondSFIpBitItem();
            }
            if (null != CapsBit)
            {
                CapsBit.Clear();
            } else {
                CapsBit = new StructurePcondCapBitItem();
            }
            if (null != Limit)
            {
                Limit.Dispose();
                Limit = null;
            }
            Limit = new StructureProcessConditionLimitItem();

            AidXmlLinq xml = new AidXmlLinq();
			AidLog logs = new AidLog( "FileProcessConditionParameter.Load" );
			XElement file = null;
			ResultCodes ret = xml.ReadByReadOnly( ref file, FilePath );
			if( ( ResultCodes.Success == ret ) && ( null != file ) ) {
				try {
					XElement mat = file.Element( "Mat" );
					if( null != mat ) {
						Read( mat, Materials );
					}
					XElement cap = file.Element( "Cap" );
					if( null != cap ) {
						Read( cap, Caps );
                        CapsBit.Read(cap);
					}
					XElement ip = file.Element( "Ip" );
					if( null != ip ) {
						Read( ip, Ips );
                        IpsBit.Read(ip);
					}
                    XElement sfip = file.Element("SFIp");
                    if (null != sfip)
                    {
                        Read(sfip, SFIps);
                        SFIpsBit.Read(sfip);
                    }
                    XElement overrides = file.Element("OverRide");
                    if (null != overrides) {
                        Read(overrides, OverRides);
                    }
                    XElement limit = file.Element("Limit");
                    if(null != limit)
                    {
                        Read(limit, Limit);
                    }
                } catch( Exception e ) {
					bool unexpected = true;
					if( ( e is ObjectDisposedException ) ||
						( e is ArgumentOutOfRangeException ) ||
						( e is System.Threading.AbandonedMutexException ) ||
						( e is FileNotFoundException ) ||
						( e is XmlException ) ||
						( e is FormatException ) ||
						( e is NullReferenceException ) ||
						( e is IOException ) ) {
						unexpected = false;   //	想定内の例外。
					}
					ret = logs.Exception( e, unexpected );
				}
			}
			return ret;
		}
        /// <summary>上下限値読み込み</summary>
		/// <param name="root">XMLルート要素</param>
		/// <param name="result">取得結果</param>
		/// <returns>実行結果</returns>
		private ResultCodes Read(XElement root, StructureProcessConditionLimitItem result)
        {
            if ((null == root) || (null == result))
            {
                return ResultCodes.InvalidArgument;
            }
            AidXmlLinq xml = new AidXmlLinq();
            IEnumerable<XElement> upperList = root.Elements("Upper");
            foreach (XElement xe in upperList)
            {
                result.Ton.UpperLimit = xml.GetAttrDecimal(xe, "ton");
                result.Toff.UpperLimit = xml.GetAttrDecimal(xe, "toff");
                result.IPVal.UpperLimit = xml.GetAttrDecimal(xe, "ip");
                result.SFIPVal.UpperLimit = xml.GetAttrDecimal(xe, "sfip");
                result.CAPVal.UpperLimit = xml.GetAttrDecimal(xe, "cap");
                result.SCVal.UpperLimit = xml.GetAttrDecimal(xe, "sc");
                result.CRSVal.UpperLimit = xml.GetAttrDecimal(xe, "crs");
                result.SfrFrSel.UpperLimit = xml.GetAttrDecimal(xe, "sfrdn");
                result.SfrBkSel.UpperLimit = xml.GetAttrDecimal(xe, "sfrup");
                result.PompVal.UpperLimit = xml.GetAttrDecimal(xe, "pomp");
                result.ServoSel.UpperLimit = xml.GetAttrDecimal(xe, "servo");
                result.PSSel.UpperLimit = xml.GetAttrDecimal(xe, "ps");
                result.POLVal.UpperLimit = xml.GetAttrDecimal(xe, "pol");
            }
            IEnumerable<XElement> lowerList = root.Elements("Lower");
            foreach (XElement xe in lowerList)
            {
                result.Ton.LowerLimit = xml.GetAttrDecimal(xe, "ton");
                result.Toff.LowerLimit = xml.GetAttrDecimal(xe, "toff");
                result.IPVal.LowerLimit = xml.GetAttrDecimal(xe, "ip");
                result.SFIPVal.LowerLimit = xml.GetAttrDecimal(xe, "sfip");
                result.CAPVal.LowerLimit = xml.GetAttrDecimal(xe, "cap");
                result.SCVal.LowerLimit = xml.GetAttrDecimal(xe, "sc");
                result.CRSVal.LowerLimit = xml.GetAttrDecimal(xe, "crs");
                result.SfrFrSel.LowerLimit = xml.GetAttrDecimal(xe, "sfrdn");
                result.SfrBkSel.LowerLimit = xml.GetAttrDecimal(xe, "sfrup");
                result.PompVal.LowerLimit = xml.GetAttrDecimal(xe, "pomp");
                result.ServoSel.LowerLimit = xml.GetAttrDecimal(xe, "servo");
                result.PSSel.LowerLimit = xml.GetAttrDecimal(xe, "ps");
                result.POLVal.LowerLimit = xml.GetAttrDecimal(xe, "pol");
            }
            return ResultCodes.Success;
        }
        /// <summary>材料情報読み込み</summary>
        /// <param name="root">XMLルート要素</param>
        /// <param name="result">取得結果</param>
        /// <returns>実行結果</returns>
        private ResultCodes Read( XElement root, StructureMaterialNameList result )
		{
			if( ( null == root ) || ( null == result ) ) {
				return ResultCodes.InvalidArgument;
			}
			AidXmlLinq xml = new AidXmlLinq();
			IEnumerable<XElement> list = root.Elements( "Item" );
			foreach( XElement xe in list ) {
				StructureMaterialNameItem data = new StructureMaterialNameItem();
				data.Number = xml.GetAttrValue( xe, "num" );
				data.Name = xml.GetAttrText( xe, "nam" );
				result.Add( data );
			}
			return ResultCodes.Success;
		}
		/// <summary>汎用情報読み込み</summary>
		/// <param name="root">XMLルート要素</param>
		/// <param name="result">取得結果</param>
		/// <returns>実行結果</returns>
		private ResultCodes Read( XElement root, StructureDataList result )
		{
			if( ( null == root ) || ( null == result ) ) {
				return ResultCodes.InvalidArgument;
			}
			AidXmlLinq xml = new AidXmlLinq();
			IEnumerable<XElement> list = root.Elements( "Item" );
			foreach( XElement xe in list ) {
				StructureDataItem data = new StructureDataItem();
				data.Number = xml.GetAttrValue( xe, "num" );
				data.Value = xml.GetAttrDouble( xe, "val" );
				result.Add( data );
			}
			return ResultCodes.Success;
		}

        /// <summary>Cap値保存</summary>
        /// <param name="target">CAPインスタンスの参照</param>
        /// <returns>実行結果</returns>
        public ResultCodes WriteCap(StructureDataItem target)
        {
            return Write(target, "Cap");
        }
        /// <summary>Ip値保存</summary>
        /// <param name="target">IPインスタンスの参照</param>
        /// <returns>実行結果</returns>
        public ResultCodes WriteIp(StructureDataItem target)
        {
            return Write(target, "Ip");
        }
        /// <summary>Ip値保存</summary>
        /// <param name="target">SFIPインスタンスの参照</param>
        /// <returns>実行結果</returns>
        public ResultCodes WriteSFIp(StructureDataItem target)
        {
            return Write(target, "SFIp");
        }
        /// <summary>OverRide値保存</summary>
		/// <param name="target">OverRideインスタンスの参照</param>
		/// <returns>実行結果</returns>
		public ResultCodes WriteOverRide(StructureDataItem target)
        {
            return Write(target, "OverRide");
        }
        /// <summary>材料名称保存</summary>
        /// <param name="target">材料名称インスタンスの参照</param>
        /// <param name="elementName">ルート要素名</param>
        /// <returns>実行結果</returns>
        private ResultCodes Write( StructureDataItem target, string elementName )
		{
			ResultCodes ret = ResultCodes.Success;
			using( AidMutex mutex = new AidMutex( FilePath ) ) {
				while( true ) {
					AidLog logs = new AidLog( "FileProcessConditionParameter.Save" );
					try {
						AidXmlLinq xml = new AidXmlLinq();
						XElement file = null;
						//	XMLファイル読み込み
						ret = xml.ReadByReadWrite( ref file, FilePath );
						if( ResultCodes.Success != ret ) {
							break;
						}
						//	プロファイル更新
						ret = xml.UpdateProfile( file );
						if( ResultCodes.Success != ret ) {
							break;
						}
						//	対象の要素を検索
						XElement root = file.Element( elementName );
						if( null != root ) {
							XElement xe = null;
							ret = xml.FindNumber( root, target.Number, ref xe );
							if( ResultCodes.Success != ret ) {
								break;
							}
							if( null == xe ) {
                                root.Add(new XElement("Item",
                                        new XAttribute("num", $"{target.Number}"),
                                        new XAttribute("val", $"{target.Value}")));
							} else {
								xml.SetAttr( xe, "val", $"{target.Value}" );
							}
							file.Save( FilePath );
						}
					} catch( Exception e ) {
						ret = logs.Exception( e, false );
					}
					if( ResultCodes.Success != ret ) {
						break;  //	キャッチされた場合のための判定分です。
					}
					return ResultCodes.Success;
				}
			}
			return ret;
		}
		/// <summary>材料名称保存</summary>
		/// <param name="target">材料名称インスタンスの参照</param>
		/// <returns>実行結果</returns>
		public ResultCodes WriteMaterial( StructureMaterialNameItem target )
		{
			if( null == target ) {
				return ResultCodes.InvalidArgument;
			}
			//	既登録の確認。空白についてはノーチェック
			if( false == string.IsNullOrEmpty( target.Name ) ) {
				Read();
				if( 0 > Materials.FindByName( target.Name ) ) {
					;
				} else {
					return ResultCodes.AlreadyRegistered;
				}
			}
			ResultCodes ret = ResultCodes.Success;
			using( AidMutex mutex = new AidMutex( FilePath ) ) {
				while( true ) {
					AidLog logs = new AidLog( "FileProcessConditionParameter.Save" );
					try {
						AidXmlLinq xml = new AidXmlLinq();
						XElement file = null;
						//	XMLファイル読み込み
						ret = xml.ReadByReadWrite( ref file, FilePath );
						if( ResultCodes.Success != ret ) {
							break;
						}
						//	プロファイル更新
						ret = xml.UpdateProfile( file );
						if( ResultCodes.Success != ret ) {
							break;
						}
						//	対象の要素を検索
						XElement root = file.Element( "Mat" );
						if( null != root ) {
							XElement xe = null;
							ret = xml.FindNumber( root, target.Number, ref xe );
							if( ResultCodes.Success != ret ) {
								break;
							}
							if( null == xe ) {
								root.Add( new XElement( "Item",
										new XAttribute( "num", $"{target.Number}" ),
										new XAttribute( "nam", target.Name ) ) );
							} else {
								xml.SetAttr( xe, "nam", target.Name );
							}
							file.Save( FilePath );
						}
					} catch( Exception e ) {
						ret = logs.Exception( e, false );
					}
					if( ResultCodes.Success != ret ) {
						break;  //	キャッチされた場合のための判定分です。
					}
					return ResultCodes.Success;
				}
			}
			return ret;
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
	}
}
