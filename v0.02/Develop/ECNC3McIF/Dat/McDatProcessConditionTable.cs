using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ECNC3.Enumeration;
using ECNC3.Models.Common;
using static Rt64ecdata;

namespace ECNC3.Models.McIf
{
	/// <summary>4-4-28.加工条件テーブルデータ書込/読出</summary>
	public class McDatProcessConditionTable : McCommBasic, IEcnc3McDatReadWrite, IDisposable
	{
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;
		/// <summary>情報保持ファイルパス</summary>
		private string _filePath = string.Empty;
		/// <summary>コンストラクタ</summary>
		public McDatProcessConditionTable()
		{
			ClassName = GetType().Name;
			DataType = Syncdef.DAT_PCOND_TABLE;
			DataTypeName = "DAT_PCOND_TABLE";
        }

        /// <summary>加工条件リスト</summary>
        public StructureProcessConditionList Items { get; private set; }

        private McDatInitialPrm datini = new McDatInitialPrm();
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
						if( null != Items ) {
							Items.Dispose();
							Items = null;
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
		/// <summary>初期化</summary>
		/// <returns>実行結果</returns>
		public ResultCodes Initialize()
		{
			AidLog logs = new AidLog( "McDatProcessConditionTable.Initialize" );
			ResultCodes ret = ResultCodes.Success;
			using( FileProcessCondition file = new FileProcessCondition() ) {
				ret = file.Read();
                if ( ResultCodes.Success == ret ) {
					PCOND_TBL data = PCOND_TBL.Init();
					int number;
					foreach( StructureProcessConditionItem item in file.Items ) {
						number = item.Number;
						if( data.rec.Length > number ) {
							Copy( ref data.rec[number], item );
						}
					}
					ret = WriteData( data );
				}
			}
			return ret;
		}
		/// <summary>StructureProcessConditionItem → PCOND_REC コピー</summary>
		/// <param name="target">コピー元</param>
		/// <param name="source">コピー先</param>
		private void Copy( ref PCOND_REC target, StructureProcessConditionItem source )
		{
            target.Ton = source.Ton;
			target.Toff = source.Toff;
			target.IPVal = source.IPVal;
            target.SFIPVal = source.SFIPVal;
			target.CAPVal = source.CAPVal;
			target.SCSel = source.SCVal;
			target.CRSSel = source.CRSVal;
			target.SfrFr = source.SfrFrSel;
			target.SfrBk = source.SfrBkSel;
			target.PumpPrSel = source.PompVal;
			target.ServoSel = source.ServoSel;
			target.PSSel = source.PSSel;
			target.POLVal = source.POLVal;
		}
		/// <summary>PCOND_REC → StructureProcessConditionItem コピー</summary>
		/// <param name="target">コピー元</param>
		/// <param name="source">コピー先</param>
		private void Copy( StructureProcessConditionItem target, PCOND_REC source )
		{
			target.Ton = source.Ton;
			target.Toff = source.Toff;
			target.IPVal = source.IPVal;
            target.SFIPVal = source.SFIPVal;
            target.CAPVal = source.CAPVal;
			target.SCVal = source.SCSel;
			target.CRSVal = source.CRSSel;
			target.SfrFrSel = source.SfrFr;
			target.SfrBkSel = source.SfrBk;
			target.PompVal = source.PumpPrSel;
			target.ServoSel = source.ServoSel;
			target.PSSel = source.PSSel;
			target.POLVal = source.POLVal;
		}

		/// <summary>バックアップ</summary>
		/// <param name="backupDirectory">バックアップ先ディレクトリパス</param>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// MCボードよりデータを取得し、ファイルとしてバックアップします。
		/// </remarks>
		public ResultCodes Backup( string backupDirectory )
		{
			using( FileProcessCondition file = new FileProcessCondition() ) {
				return file.Backup( backupDirectory );
			}
		}
		/// <summary>読み込み</summary>
		/// <returns>実行結果</returns>
		public ResultCodes Read()
		{
			//	ファイルを読み込み
			//	基準となるテーブルを作成するためにファイルを読み込む。
			//	基本的にMCから取得した値ですべて上書きされることになる。
			using( FileProcessCondition file = new FileProcessCondition() ) {
				file.Read();
				if( null != Items ) {
					Items.Dispose();
					Items = null;
				}
				Items = file.Items.Clone() as StructureProcessConditionList;
			}
			//	MC情報を読み込み
			PCOND_TBL data;
			ReadData( out data );
            using (UIPcondBitConverter bitConv = new UIPcondBitConverter())
            {
                datini.Read();
                for (int ict = 0; ict < 1000; ict++)
                {
                    data.rec[ict].IPVal = bitConv.IpFromBit(data.rec[ict].IPVal);
                    data.rec[ict].SFIPVal = bitConv.SFIpFromBit(data.rec[ict].SFIPVal);
                    data.rec[ict].CAPVal = bitConv.CapFromBit(data.rec[ict].CAPVal);
                }
            }
            //	MC情報を現在情報として扱い、上書きする。
            int index = 0;
			int pos = 0;
			for( index = 0 ; index < data.rec.Length ; ++index ) {
				pos = Items.FindIndex( ( x ) => ( x.Number == index ) );
				if( 0 > pos ) {
					continue;
				}
				//	加工条件番号に該当があったので取得情報を上書き
				Copy( Items[pos], data.rec[index] );
			}
			return ResultCodes.Success;
		}

		/// <summary>MCデータ読み込み</summary>
		/// <param name="data">MCより読み込まれたデータ</param>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// MCより情報を取得します。
		/// </remarks>
		private ResultCodes ReadData( out PCOND_TBL data )
		{
			AidLog logs = new AidLog( "McDatProcessConditionTable.ReadData" );
			ResultCodes ret = ResultCodes.McNotInitialize;
			data = PCOND_TBL.Init();
			while( true == StandBy() ) {
				try {
					int size = Marshal.SizeOf( data );
					int retRt64 = 0;
					TechnoMethods method = TechnoMethods.ReceiveData;
					if( BootModes.Desktop == BootMode ) {
						retRt64 = Rt64eccomapiWrap.ReceiveData( CommHandle, DataType, Task, NonParameter, ref size, ref data );
						ret = CheckResultDebug( method, retRt64 );
						if( ResultCodes.Success != ret ) {
							break;
						}
					} else {
						retRt64 = Rt64eccomapi.ReceiveData( CommHandle, DataType, Task, -1, ref size, ref data );
						ret = CheckResultTechno( method, retRt64 );
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
		/// <summary>書き込み</summary>
		/// <returns>実行結果</returns>
		/// <remarks>
		/// 全件の書き込みはサポートしません。
		/// </remarks>
		public ResultCodes Write()
		{
			return ResultCodes.NotSupported;
		}
		/// <summary>書き込み</summary>
		/// <param name="target">書き込み対象の加工条件情報</param>
		/// <param name="perpetuation">永続化の要否
		///		<list type="bullet" >
		///			<item>true=永続化する。</item>
		///			<item>false=永続化しない。</item>
		///		</list>
		/// </param>
		/// <returns>実行結果</returns>
		public ResultCodes Write( StructureProcessConditionItem target, bool perpetuation )
		{
           
            AidLog logs = new AidLog( "McDatProcessConditionTable.Write" );
			ResultCodes ret = ResultCodes.InvalidArgument;
			while( true == Verify( target ) ) {
                PCOND_REC data = PCOND_REC.Init();
				int number = target.Number;
				logs.Sure( $"P#{number},perpetuation=({perpetuation})" );
				logs.Sure( "NOW:" + Format( data ) );
                Copy( ref data, target );
                logs.Sure("NEW:" + Format(data));
                logs.Sure($"+ Dia={target.Diameter},Mat={target.Material},[{target.Comment}],Protected={target.Protect}");
                using (UIPcondBitConverter bitConv = new UIPcondBitConverter())
                {
                    datini.Read();
                    if (datini.EnableSF02 == false)
                    {
                        data.IPVal = bitConv.IpToBit(data.IPVal);
                        data.SFIPVal = 0;
                        if(data.CAPVal <= bitConv.BitItem.Caps.Find(x => x.Number == bitConv.BitItem.CapsBit.BoundIndex).Value )
                        {
                            data.CAPVal = 0;
                        }
                        else
                        {
                            data.CAPVal = bitConv.CapToBit(data.CAPVal);
                        }
                    }
                    else
                    {
                        if (data.SFIPVal >= 3000)
                        {
                            data.IPVal = bitConv.IpToBit(data.SFIPVal);
                            data.SFIPVal = 0;
                            data.CAPVal = bitConv.CapToBit(data.CAPVal);
                        }
                        else if (data.SFIPVal > 0)
                        {
                            data.IPVal = 0;
                            data.SFIPVal = bitConv.SFIpToBit(data.SFIPVal);
                            data.CAPVal = bitConv.CapToBit(data.CAPVal);
                        }
                        else if (data.SFIPVal == 0)
                        {
                            data.IPVal = bitConv.IpToBit(data.IPVal);
                            data.SFIPVal = 0;
                            data.CAPVal = bitConv.CapToBit(data.CAPVal);
                        }
                    }
                }
                ////	MCへ対象の加工条件を転送
                ret =　WriteDataOnly(data, number);
                if ( ResultCodes.Success != ret ) {	break;}
				if( true == perpetuation ) {
					//	転送を確認できたらファイルへ反映
					using( FileProcessCondition fa = new FileProcessCondition() ) {
						ret = fa.Write( target );
					}
				}
				break;
			}
			return ret;
		}
		/// <summary>設定値正当性チェック</summary>
		/// <param name="target">判定対象のオブジェクト</param>
		/// <returns>判定結果
		///		<list type="bullet" >
		///			<item>true=正当</item>
		///			<item>false=不当</item>
		///		</list>
		/// </returns>
		/// <remarks>
		/// 201/10/31
		/// InvVal、ServoSel、PSSelについては、I/F構造体上にはあるが、UI上に具体的な仕様がない状況であるが、
		/// 手動画面で操作がおぼつかなくなるのでチェック対象に追加した。
		/// </remarks>
		public bool Verify( StructureProcessConditionItem target )
		{
			if( ( true == OutofRange( target.Number, 0, 999, "Number" ) ) ||
				( true == OutofRange( target.Ton, 0, 999, "Ton" ) ) ||
				( true == OutofRange( target.Toff, 0, 999, "Toff" ) ) ||
				( true == OutofRange( target.IPVal, 0, 114000, "IPVal" ) ) ||
                (true == OutofRange(target.SFIPVal, 0, 114000, "SFIPVal")) ||
                ( true == OutofRange( target.CAPVal, 0, 1100, "CAPVal" ) ) ||
				( true == OutofRange( target.SCVal, 0, 63, "SCVal" ) ) ||
				( true == OutofRange( target.CRSVal, 0, 15, "CRSVal" ) ) ||
				( true == OutofRange( target.SfrFrSel, 0, 4000000, "SfrFrSel" ) ) ||
				( true == OutofRange( target.SfrBkSel, 0, 4000000, "SfrBkSel" ) ) ||
				( true == OutofRange( target.PompVal, 0, 15, "PompVal") ) ||
				( true == OutofRange( target.ServoSel, 0, 3, "ServoSel" ) ) ||
				( true == OutofRange( target.PSSel, 0, 5, "PSSel" ) ) ||
				( true == OutofRange( target.POLVal, 0, 1, "POLVal" ) ) ) {
				return false;
			}

			//	付帯情報、	具体的な仕様がないため、今回、チェックを行わない
			//	Comment
			//	Diameter
			//	Material

			//	UI上にはあるが、I/F構造体上に具体的な仕様がないため、今回、チェックを行わない
			//	- Hydraulic
			return true;
		}
		/// <summary>書き込み情報の書式化</summary>
		/// <param name="data">コンディション情報</param>
		/// <returns>書式化された文字列</returns>
		private string Format( PCOND_REC data )
		{
			return $"Turn(On={data.Ton},Off={data.Toff}),IP={data.IPVal},SFIP={data.SFIPVal},CAP={data.CAPVal},CRS={data.CRSSel},"
				+ $"SFR(Fr={data.SfrFr},Bk={data.SfrBk}),Inv={data.PumpPrSel},Svo(Ctl={data.SCSel},Sel={data.ServoSel}),"
				+ $"PS={data.PSSel},POL={data.POLVal}";
		}

		/// <summary>書き込み</summary>
		/// <param name="data">書き込み対象となる全加工条件情報</param>
		/// <returns>実行結果</returns>
		private ResultCodes WriteData( PCOND_TBL data )
		{
            using (UIPcondBitConverter bitConv = new UIPcondBitConverter()) 
            {
                datini.Read();
                for (int ict = 0; ict < 1000; ict++)
                {
                    if (datini.EnableSF02 == false)
                    {
                        data.rec[ict].IPVal = bitConv.IpToBit(data.rec[ict].IPVal);
                        data.rec[ict].SFIPVal = 0;
                        if (data.rec[ict].CAPVal <= bitConv.BitItem.Caps.Find(x => x.Number == bitConv.BitItem.CapsBit.BoundIndex).Value)
                        {
                            data.rec[ict].CAPVal = 0;
                        }
                        else
                        {
                            data.rec[ict].CAPVal = bitConv.CapToBit(data.rec[ict].CAPVal);
                        }
                    }
                    else
                    {
                        if (data.rec[ict].SFIPVal >= 3000)
                        {
                            data.rec[ict].IPVal = bitConv.IpToBit(data.rec[ict].SFIPVal);
                            data.rec[ict].SFIPVal = 0;
                        }
                        else if (data.rec[ict].SFIPVal > 0)
                        {
                            data.rec[ict].IPVal = 0;
                            data.rec[ict].SFIPVal = bitConv.SFIpToBit(data.rec[ict].SFIPVal);
                        }
                        else if (data.rec[ict].SFIPVal == 0)
                        {
                            data.rec[ict].IPVal = bitConv.IpToBit(data.rec[ict].IPVal);
                            data.rec[ict].SFIPVal = 0;
                        }
                        data.rec[ict].CAPVal = bitConv.CapToBit(data.rec[ict].CAPVal);
                    }
                }
            }


            AidLog logs = new AidLog( "McDatProcessConditionTable.Write" );
			ResultCodes ret = ResultCodes.McNotInitialize;
			while( true == StandBy() ) {
				try {
					int size = Marshal.SizeOf( data );
					int retRt64 = 0;
					TechnoMethods method = TechnoMethods.SendData;
					logs.Sure( $"SendData({DataTypeName})" );
					if( BootModes.Desktop == BootMode ) {
						retRt64 = Rt64eccomapiWrap.SendData( CommHandle, DataType, Task, NonParameter, size, ref data );
						ret = CheckResultDebug( method, retRt64 );
						if( ResultCodes.Success != ret ) {
							break;
						}
					} else {
						retRt64 = Rt64eccomapi.SendData( CommHandle, DataType, Task, -1, size, ref data );
						ret = CheckResultTechno( method, retRt64 );
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
        /// <summary>書き込み</summary>
		/// <param name="data">書き込み対象となる全加工条件情報</param>
		/// <returns>実行結果</returns>
		private ResultCodes WriteDataOnly(PCOND_REC data, int pnum)
        {
            AidLog logs = new AidLog("McDatProcessConditionTable.Write");
            ResultCodes ret = ResultCodes.McNotInitialize;
            while (true == StandBy())
            {
                try
                {
                    int size = Marshal.SizeOf(data);
                    int retRt64 = 0;
                    TechnoMethods method = TechnoMethods.SendData;
                    logs.Sure($"SendData({DataTypeName})");
                    if (BootModes.Desktop == BootMode)
                    {
                        retRt64 = Rt64eccomapiWrap.SendData(CommHandle, DataType, Task, pnum, size, ref data);
                        ret = CheckResultDebug(method, retRt64);
                        if (ResultCodes.Success != ret) break;
                    }
                    else
                    {
                        retRt64 = Rt64eccomapi.SendData(CommHandle, DataType, Task, pnum, size, ref data);
                        ret = CheckResultTechno(method, retRt64);
                        if (ResultCodes.Success != ret) break;
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
    }
}
