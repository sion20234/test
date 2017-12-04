using System;
using System.Xml;
using ECNC3.Enumeration;
using ECNC3.Models.Common;
using static Rt64ecdata;

namespace ECNC3.Models.McIf
{
	/// <summary>４-５-１６．パーティション設定コマンド </summary>
	public class McReqPartitionChange : McCommBasic, IEcnc3McCommand, IDisposable
    {
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;

		/// <summary>コンストラクタ</summary>
		public McReqPartitionChange()
		{
			ClassName = GetType().Name;
			DataType = Syncdef.REQ_PARTITIONCHG;
			DataTypeName = "REQ_PARTITIONCHG";
		}
		/// <summary>パーティション設定</summary>
		public StructurePartitions Partitions { get; set; }

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
						if( null != Partitions ) {
							Partitions.Dispose();
							Partitions = null;
						}
					}
					//  アンマネージリソースの解放
				}
				_disposed = true;
			} finally {
				//  基底クラスのDispose()を確実に呼び出す。
				base.Dispose( disposing );
			}
		}

		/// <summary>設定読み込み</summary>
		/// <param name="fs">設定ファイルクラスの参照</param>
		/// <returns>実行結果</returns>
		public ResultCodes Initialize( FileSettings fs )
		{
			//	ファイル読み込み
			ResultCodes ret = ReadFile( fs );
			//	MC転送
			ret = Execute();

			return ret;
		}

		/// <summary>読み込み</summary>
		/// <returns>実行結果</returns>
		public ResultCodes Read()
		{
			using( FileSettings fs = new FileSettings() ) {
				fs.Read();
				return ReadFile( fs );
			}
		}

		/// <summary>書き込み</summary>
		/// <returns>実行結果</returns>
		public ResultCodes Write()
		{
			using( FileSettings fs = new FileSettings() ) {
				fs.Read();
				fs.WriteAttr( "Root/Opr/Ptn", "enbl", ( true == Partitions.Enabled ) ? "1" : "0" );
				foreach( StructurePartitionItem item in Partitions.Items ) {
					fs.WriteAttr( "Root/Opr/Ptn/Item", "st", item.IndexStart.ToString(), item.Number );
					fs.WriteAttr( "Root/Opr/Ptn/Item", "end", item.IndexEnd.ToString(), item.Number );
					fs.WriteAttr( "Root/Opr/Ptn/Item", "thin", ( true == item.Thinline ) ? "1" : "0", item.Number );
				}
				fs.Write();
			}
			return ResultCodes.Success;
		}

		/// <summary>ファイル読み込み</summary>
		/// <param name="fs">設定ファイル操作クラスの参照</param>
		/// <returns>実行結果</returns>
		private ResultCodes ReadFile( FileSettings fs )
		{
			if( null != Partitions ) {
				Partitions.Dispose();
				Partitions = null;
			}
			Partitions = new StructurePartitions();
			Partitions.Enabled = fs.AttrBool( "Root/Opr/Ptn", "enbl" );
            AidConvert cnv = new AidConvert();
            int val = 0;
            XmlNodeList list = fs.GetList("Root/Opr/Ptn/Item");
            if (null != list)
            {
                foreach (XmlNode element in list)
                {
                    using (StructurePartitionItem item = new StructurePartitionItem())
                    {
                        foreach (XmlNode attr in element.Attributes)
                        {
                            if ("num" == attr.Name)
                            {
                                if (true == cnv.TryParse(attr.Value, out val))
                                {
                                    item.Number = val;
                                }
                            }
                            else if ("st" == attr.Name)
                            {
                                if (true == cnv.TryParse(attr.Value, out val))
                                {
                                    item.IndexStart = (short)val;
                                }
                            }
                            else if ("end" == attr.Name)
                            {
                                if (true == cnv.TryParse(attr.Value, out val))
                                {
                                    item.IndexEnd = (short)val;
                                }
                            }
                            else if ("thin" == attr.Name)
                            {
                                if (true == cnv.TryParse(attr.Value, out val))
                                {
                                    item.Thinline = (0 == val) ? false : true;
                                }
                            }
                        }
                        if (0 > item.Number)
                        {
                            continue;
                        }
                        Partitions.Items.Add(item.Clone() as StructurePartitionItem);
                    }
                }
                if (1 < Partitions.Items.Count)
                {
                    Partitions.Items.Sort((a, b) => (a.Number - b.Number));
                }
            }
            return ResultCodes.Success;
		}

		/// <summary>コマンド発行</summary>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// プロパティに設定された内容によりMCボードへコマンドを発行します。
		/// </remarks>
		public ResultCodes Execute()
		{
			AidLog logs = new AidLog( "McReqPartitionChange.Execute" );
			ResultCodes ret = ResultCodes.McNotInitialize;
			while( true == StandBy() ) {
				try {
					PARTITION data = PARTITION.Init();
					{
						if( null != Partitions.Items ) {
							if( 0 < Partitions.Items.Count ) {
								int index = 0;
								foreach( StructurePartitionItem item in Partitions.Items ) {
									index = item.Number - 1;
									if( ( 0 > index ) || ( 5 < index ) ) {
										continue;
									}
									data.PartitionS[index] = item.IndexStart;
									data.PartitionE[index] = item.IndexEnd;
									data.Thinline[index] = (short)( ( true == item.Thinline ) ? 1 : 0 );
								}
							}
						}
						data.PartitionDis = (short)( ( true == Partitions.Enabled ) ? 0 : 1 );
					}
					int retRt64 = 0;
					TechnoMethods method = TechnoMethods.SendCommand;
					logs.Sure( $"SendCommand({DataTypeName},PartitionDis={data.PartitionDis},"
							+ $"0:[{data.PartitionS[0]}-{data.PartitionE[0]},Thin={data.Thinline[0]}]"
							+ $"1:[{data.PartitionS[1]}-{data.PartitionE[1]},Thin={data.Thinline[1]}]"
							+ $"2:[{data.PartitionS[2]}-{data.PartitionE[2]},Thin={data.Thinline[2]}]"
							+ $"3:[{data.PartitionS[3]}-{data.PartitionE[3]},Thin={data.Thinline[3]}]"
							+ $"4:[{data.PartitionS[4]}-{data.PartitionE[4]},Thin={data.Thinline[4]}]"
							+ $"5:[{data.PartitionS[5]}-{data.PartitionE[5]},Thin={data.Thinline[5]}])" );
					if( true == BootMode.HasFlag( BootModes.Machine ) ) {
						retRt64 = Rt64eccomapi.SendCommand( CommHandle, DataType, Task, ref data );
						ret = CheckResultTechno( method, retRt64 );
						if( ResultCodes.Success != ret ) {
							break;
						}
					}
					if( true == BootMode.HasFlag( BootModes.Desktop ) ) {
						retRt64 = Rt64eccomapiWrap.SendCommand( CommHandle, DataType, Task, ref data );
						ret = CheckResultDebug( method, retRt64 );
						if( ResultCodes.Success != ret ) {
							break;
						}
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
	}
}
