using ECNC3.Models.McIf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECNC3.Enumeration;
using ECNC3.Models;
using static Rt64ecdata;
using static Rt64ecgcdapi;
using System.IO;
using ECNC3.Models.Common;
using System.Runtime.InteropServices;
using static ECNC3.Models.McIf.McCommBasic;
using System.Xml;

namespace ECNC3.Models.McIf
{
    public class McDatGcd : McGcdBasic, IEcnc3McDatReadWrite
    {
        /// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
        private bool _disposed = false;
        /// <summary>情報保持ファイルパス</summary>
        //private string _filePath = string.Empty;
        /// <summary>初期化パラメータ</summary>
        private PRGINITDATA _data;
        public bool M02Dis { get; set; }

        /// <summary>ユーザ定義コードファイル格納ディレクトリ</summary>
		public string PathUcd
        {
            get
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), @"\UserPrg\Ucd");
                if (false == Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return path;
            }
        }
        /// <summary>コンストラクタ</summary>
        public McDatGcd()
        {
            Name = this.ToString();
            ClassName = GetType().Name;
            //using (ECNC3Settings cmn = new ECNC3Settings())
            //{
            //    _filePath = .CurrentDirectry "initial.ipm");
            //}
        }

        
        /// <summary>インスタンスの破棄</summary>
        public new void Dispose()
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
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (false == _disposed)
                {
                    if (true == disposing)
                    {
                        //  マネージリソースの解放
                    }
                    //  アンマネージリソースの解放
                }
                _disposed = true;
            }
            finally
            {
                //  基底クラスのDispose()を確実に呼び出す。
                base.Dispose(disposing);
            }
        }

        ///// <summary>ファイルの読み込みとMCダウンロード</summary>
        ///// <returns>実行結果</returns>
        ///// <remarks>
        ///// initial.rom ファイルの読み込みと読み込みデータのMCへの転送を行います。
        ///// </remarks>
        //public ResultCodes Initialize()
        //{
        //    INITIALPRM data = INITIALPRM.Init();
        //    ResultCodes ret = ReadFile(ref data, _filePath);
        //    if (ResultCodes.Success == ret)
        //    {
        //        ret = WriteData(data);
        //    }
        //    return ret;
        //}

        ///// <summary>バックアップ</summary>
        ///// <param name="backupDirectory">バックアップ先ディレクトリパス</param>
        ///// <returns>実行結果</returns>
        ///// <remarks>
        ///// MCボードよりデータを取得し、ファイルとしてバックアップします。
        ///// </remarks>
        //public ResultCodes Backup(string backupDirectory)
        //{
        //    FileAccessCommon fc = new FileAccessCommon();
        //    fc.Backup(_filePath, backupDirectory);

        //    INITIALPRM data;
        //    ResultCodes ret = ReadData(out data);
        //    if (ResultCodes.Success == ret)
        //    {
        //        ret = WriteFile(data, _filePath);
        //    }
        //    return ret;
        //}

        ///// <summary>リストア</summary>
        ///// <param name="restoreDirectory">復元元情報の格納されたディレクトリパス</param>
        ///// <returns>実行結果</returns>
        //public ResultCodes Restore(string restoreDirectory)
        //{
        //    FileAccessCommon fc = new FileAccessCommon();
        //    return fc.Restore(restoreDirectory, _filePath);
        //}

        /// <summary>MCデータ読み込み</summary>
        /// <returns>実行結果</returns>
        public ResultCodes Read()
        {
            ResultCodes ret = ReadData(out _data);
            if (ResultCodes.Success == ret)
            {
                M02Dis = (_data.McdDis[2] == 0)? false : true;
            }
            return ret;
        }
        /// <summary>MCデータ読み込み</summary>
        /// <param name="data">MCより読み込まれたデータ</param>
        /// <returns>実行結果</returns>
        /// <remarks>
        /// MCより情報を取得します。
        /// </remarks>
        private ResultCodes ReadData(out PRGINITDATA data)
        {
            AidLog logs = new AidLog("McDatGcd.ReadData");
            ResultCodes ret = ResultCodes.McNotInitialize;
            data = PRGINITDATA.Init();
            while (true == StandBy())
            {
                try
                {
                    int retRt64 = 0;
                    retRt64 = GcdInitialize(ref data);
                    ret = ResultCodes.Success;
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

        /// <summary>MCデータ書き込み</summary>
        /// <returns>実行結果</returns>
        /// <remarks>
        /// プロパティに設定された値をMCへ転送します。
        /// プロパティとして公開されていない値に関しては元の値を保持します。
        /// </remarks>
        public ResultCodes Write()
        {
            AidLog logs = new AidLog( "McDatGcd.Write" );
            PRGINITDATA data;
            ResultCodes ret = ReadData(out data);
            if (ResultCodes.Success == ret)
            {
                AidBit bit = new AidBit();
                {
                    data.McdDis[2] = (M02Dis == false)? (byte)0 : (byte)1;
                }
				//	変換ライブラリー
//				using( FileSettings fs = new FileSettings() )//変更：柏原
				using( FileSettingsGMCodeDisable fs = new FileSettingsGMCodeDisable() ) {
					ret = fs.Read();
					using( McGcdInitData mc = new McGcdInitData() ) {
						//ret = mc.Initialize( fs );
						//if( ResultCodes.Success == ret ) {
						//	ret = WriteData( fs );
						//}
						ret = mc.InitializeGMCodeDisable( fs );
						if( ResultCodes.Success == ret ) {
							ret = WriteDataGMCodeDisable( fs );
						}
					}
				}
                
            }
            return ret;
        }
        /// <summary>初期化</summary>
		/// <param name="fs">設定ファイルアクセスクラスの参照</param>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// インチモードは考慮していません。メトリックのみであるとしています。
		/// </remarks>
		public ResultCodes WriteData(FileSettings fs)
        {
            AidLog logs = new AidLog( "McDatGcd.WriteData" );
            ResultCodes ret = ResultCodes.McNotInitialize;
            while (true == StandBy())
            {
                try
                {
                    PRGINITDATA data = PRGINITDATA.Init();
                    //	無効Gコード配列
                    using (XmlNodeList list = fs.GetList("Root/Prog/GCodDsbl/Item"))
                    {
                        SetCodeDisable(list, data.GcdDis);
                    }
                    //	無効Mコード配列
                    using (XmlNodeList list = fs.GetList("Root/Prog/MCodDsbl/Item"))
                    {
                        SetCodeDisable(list, data.McdDis);
                    }
                    int retRt64 = Rt64ecgcdapi.GcdInitialize(ref data);
                    if (Syncdef.E_OK != retRt64)
                    {
                        ret = ConvertReturnCode(retRt64);
                    }
                    else
                    {
                        ret = ResultCodes.Success;
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
		/// <summary>初期化</summary>
		/// <param name="fs">設定ファイルアクセスクラスの参照</param>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// インチモードは考慮していません。メトリックのみであるとしています。
		/// </remarks>
		public ResultCodes WriteDataGMCodeDisable( FileSettingsGMCodeDisable fs )
		{
			AidLog logs = new AidLog( "McDatGcd.WriteDataGMCodeDisable" );
			ResultCodes ret = ResultCodes.McNotInitialize;
			while( true == StandBy() ) {
				try {
					PRGINITDATA data = PRGINITDATA.Init();
					//	無効Gコード配列
					using( XmlNodeList list = fs.GetList( "Root/Prog/GCodDsbl/Item" ) ) {
						SetCodeDisable( list, data.GcdDis );
					}
					//	無効Mコード配列
					using( XmlNodeList list = fs.GetList( "Root/Prog/MCodDsbl/Item" ) ) {
						SetCodeDisable( list, data.McdDis );
					}
					int retRt64 = Rt64ecgcdapi.GcdInitialize( ref data );
					if( Syncdef.E_OK != retRt64 ) {
						ret = ConvertReturnCode( retRt64 );
					} else {
						ret = ResultCodes.Success;
					}
				} catch( Exception e ) {
					bool unexpected = true;
					if( ( e is DllNotFoundException ) ||
						( e is EntryPointNotFoundException ) ) {
						unexpected = false;   //	想定内の例外。
					}
					ret = logs.Exception( e, unexpected );
				}
				break;
			}
			return ret;
		}

		/// <summary>Gコード，Mコード無効配列設定</summary>
		/// <param name="nodeList">無効情報が設定されているノードリスト</param>
		/// <param name="target">設定対象の配列</param>
		/// <remarks>
		/// 無効情報のみを設定します。引数はあらかじめ有効で初期化したものにしてください。
		/// </remarks>
		private void SetCodeDisable(XmlNodeList nodeList, byte[] target)
        {
            if (null != nodeList)
            {
                int length = target.Length;
                AidConvert cnv = new AidConvert();
                int number;
                byte disabled;
                int val = 0;
                foreach (XmlNode element in nodeList)
                {
                    using (StructurePartitionItem item = new StructurePartitionItem())
                    {
                        number = -1;
                        disabled = (byte)0;
                        foreach (XmlNode attr in element.Attributes)
                        {
                            if ("num" == attr.Name)
                            {
                                if (true == cnv.TryParse(attr.Value, out val))
                                {
                                    number = val;
                                }
                            }
                            else if ("dsbl" == attr.Name)
                            {
                                if (true == cnv.TryParse(attr.Value, out val))
                                {
                                    disabled = (byte)((0 != val) ? 1 : 0);
                                }
                            }
                        }
                        if ((0 > number) || (length < number))
                        {
                            continue;
                        }
                        target[number] = disabled;
                    }
                }
            }
        }

    }
}
