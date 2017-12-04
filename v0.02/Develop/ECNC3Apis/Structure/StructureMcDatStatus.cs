using System;
using ECNC3.Enumeration;

namespace ECNC3.Models.McIf
{
	/// <summary>ステータス情報</summary>
	public class StructureMcDatStatus : IDisposable, ICloneable
	{
		/// <summary>軸数</summary>
		/// <remarks>
		/// 構造体定義の反映であるため固定値
		/// </remarks>
		private readonly int _axisCount = 64;
		/// <summary>タスク数</summary>
		/// <remarks>
		/// 構造体定義の反映であるため固定値
		/// </remarks>
		private readonly int _taskCount = 8;
		/// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
		private bool _disposed = false;

		/// <summary>コンストラクタ</summary>
		public StructureMcDatStatus()
		{
			Main = new StructureMcDatStatusMc();
			int index = 0;
			Axes = new StructureMcDatStatusAxis[_axisCount];
			for( index = 0 ; index < _axisCount ; ++index ) {
				Axes[index] = new StructureMcDatStatusAxis();
			}
			Tasks = new StructureMcDatStatusTask[_taskCount];
			for( index = 0 ; index < _taskCount ; ++index ) {
				Tasks[index] = new StructureMcDatStatusTask();
			}
			Ecnc = new StructureMcDatStatusEcnc();
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
		/// <summary>コピー</summary>
		/// <param name="source">コピー元</param>
		public void Copy( StructureMcDatStatus source )
		{
			Main.Copy( source.Main );
			int index = 0;
			if( ( null != source.Axes ) && ( null != Axes ) ) {
				for( index = 0 ; ( index < source.Axes.Length ) && ( index < Axes.Length ) ; ++index ) {
					Axes[index].Copy( source.Axes[index] );
				}
			}
			if( ( null != source.Tasks ) && ( null != Tasks ) ) {
				for( index = 0 ; ( index < source.Tasks.Length ) && ( index < Tasks.Length ) ; ++index ) {
					Tasks[index].Copy( source.Tasks[index] );
				}
			}
			Ecnc.Copy( source.Ecnc );
		}
		/// <summary>クローン</summary>
		/// <returns>クローンされたインスタンス</returns>
		public object Clone()
		{
			StructureMcDatStatus temp = new StructureMcDatStatus();
			temp.Copy( this );
			return temp;
		}
		/// <summary>比較</summary>
		/// <param name="source">比較元</param>
		/// <returns>比較結果</returns>
		public int Compare( StructureMcDatStatus source )
		{
			int ret = 0;
			while( true ) {
				ret = Main.Compare( source.Main );
				if( 0 != ret ) {
					break;
				}
				int index = 0;
				if( ( null != source.Axes ) && ( null != Axes ) ) {
					for( index = 0 ; ( index < source.Axes.Length ) && ( index < Axes.Length ) ; ++index ) {
						ret = Axes[index].Compare( source.Axes[index] );
						if( 0 != ret ) {
							break;
						}
					}
					if( 0 != ret ) {
						break;
					}
				}
				if( ( null != source.Tasks ) && ( null != Tasks ) ) {
					for( index = 0 ; ( index < source.Tasks.Length ) && ( index < Tasks.Length ) ; ++index ) {
						ret = Tasks[index].Compare( source.Tasks[index] );
						if( 0 != ret ) {
							break;
						}
					}
					if( 0 != ret ) {
						break;
					}
				}
				ret = Ecnc.Compare( source.Ecnc );
				if( 0 != ret ) {
					break;
				}
				break;
			}
			return ret;
		}
		/// <summary>アラーム情報のみの差異を比較</summary>
		/// <param name="source">比較元</param>
		/// <returns>比較結果
		///		<list type="bullet" >
		///			<item>true=等しい</item>
		///			<item>false=等しくない</item>
		///		</list>
		/// </returns>
		public bool EqualsAlarmOnly( StructureMcDatStatus source )
		{
			while( true ) {
				if( ( null != source.Main ) && ( null != Main ) ) {
					if( 0 != Main.Alarm.CompareTo( source.Main.Alarm ) ) {
						break;
					}
				}
				int index = 0;
				if( ( null != source.Axes ) && ( null != Axes ) ) {
					for( index = 0 ; ( index < source.Axes.Length ) && ( index < Axes.Length ) ; ++index ) {
						if( 0 != Axes[index].AxAlarm.CompareTo( source.Axes[index].AxAlarm ) ) {
							return false;
						}
					}
				}
				if( ( null != source.Tasks ) && ( null != Tasks ) ) {
					for( index = 0 ; ( index < source.Tasks.Length ) && ( index < Tasks.Length ) ; ++index ) {
						if( 0 != Tasks[index].TaskAlarm.CompareTo( source.Tasks[index].TaskAlarm ) ) {
							return false;
						}
					}
				}
				if( ( null != source.Ecnc ) && ( null != Ecnc ) ) {
					if( 0 != Ecnc.Alarm2.CompareTo( source.Ecnc.Alarm2 ) ) {
						break;
					}
					if( 0 != Ecnc.Alarm3.CompareTo( source.Ecnc.Alarm3 ) ) {
						break;
					}
					if( 0 != Ecnc.Alarm4.CompareTo( source.Ecnc.Alarm4 ) ) {
						break;
					}
					if( 0 != Ecnc.Alarm5.CompareTo( source.Ecnc.Alarm5 ) ) {
						break;
					}
					if( 0 != Ecnc.EIFErr1.CompareTo( source.Ecnc.EIFErr1 ) ) {
						break;
					}
					if( 0 != Ecnc.EIFErr2.CompareTo( source.Ecnc.EIFErr2 ) ) {
						break;
					}
				}
				return true;
			}
			return false;
		}
		/// <summary>アラーム有無</summary>
		public bool HasAlarm
		{
			get
			{
				while( true ) {
					if( null != Main ) {
						if( 0 != Main.Alarm ) {
							break;
						}
					}
					if( null != Axes ) {
						if( false == Array.TrueForAll( Axes, ( x ) => 0 == x.AxAlarm ) ) {
							break;
						}
					}
					if( null != Tasks ) {
						if( false == Array.TrueForAll( Tasks, ( x ) => 0 == x.TaskAlarm ) ) {
							break;
						}
					}
					if( null != Ecnc ) {
						if( 0 != Ecnc.Alarm2 ) {
							break;
						}
						if( 0 != Ecnc.Alarm3 ) {
							break;
						}
						if( 0 != Ecnc.Alarm4 ) {
							break;
						}
						if( 0 != Ecnc.Alarm5 ) {
							break;
						}
						if( 0 != Ecnc.EIFErr1 ) {
							break;
						}
						if( 0 != Ecnc.EIFErr2 ) {
							break;
						}
					}
					return false;
				}
				return true;
			}
		}
		/// <summary>全体情報</summary>
		public StructureMcDatStatusMc Main { get; private set; }
		/// <summary>軸情報</summary>
		public StructureMcDatStatusAxis[] Axes { get; private set; }
		/// <summary>タスク情報</summary>
		public StructureMcDatStatusTask[] Tasks { get; private set; }
		/// <summary>ECNC情報</summary>
		public StructureMcDatStatusEcnc Ecnc { get; private set; }

		/// <summary>タスク</summary>
		public short TargetTaskNumber { get; set; } = 0;

		// for STATUS.mc.Status
		public const int S_MCS_ALM = 0x00000001;    // アラーム発生中
		public const int S_MCS_SETTING = 0x00000002;    // 全タスクセッティングモード
		public const int S_MCS_SENSE = 0x00000010;  // 高速センサーラッチ完
													//public const int						= 0x00000020;	//
		public const int S_MCS_PRGCHG = 0x00000040; // 動作プログラムデータ変更
		public const int S_MCS_RTCWARN = 0x00000080;    // 制御周期負荷ワーニング(87.5%)
		public const int S_MCS_RTCFAIL = 0x00000100;    // 制御周期負荷過大
		public const int S_MCS_FDTWARN = 0x00000200;    // FDT読込ワーニング

		// for STATUS.mc.Alarm
		//	public const int S_MCA_EMS				= 0x00000001;	// 非常停止
		public const int S_MCA_BACKUP = 0x00000002; // バックアップエラー
		public const int S_MCA_PARAMETER = 0x00000004;  // パラメータ未設定エラー
		public const int S_MCA_ROMSW_BKUP = 0x00000008; // ROMSWﾊﾞｯｸｱｯﾌﾟｴﾗｰ
		public const int S_MCA_ECT_INIT = 0x00000010;   // EtherCAT初期化エラー
		public const int S_MCA_ECT_WCOM = 0x00000020;   // EtherCAT全体通信エラー
														// public const int						= 0x00000040;	// 
		public const int S_MCA_FDTFAIL = 0x00000080;    // FDT読込エラー
														// public const int						= 0x00000100;	// 
		public const int S_MCA_ECT_IO_COMM = 0x00000200;    // EtherCAT IO通信エラー
		public const int S_MCA_SYSTEM = 0x00008000; // システムエラー

		// for STATUS.ax[n].AxStatus
		public const int S_AXS_INPOS = 0x00000001;  // インポジション
		public const int S_AXS_ACC_NZE = 0x00000002;    // 加減速たまり有り
		public const int S_AXS_SVON = 0x00000004;   // サーボＯＮ
		public const int S_AXS_ZRN = 0x00000008;    // 原点復帰完了
		public const int S_AXS_AXMV = 0x00000010;   // 独立位置決め中
		public const int S_AXS_AXMVSTP = 0x00000020;    // 独立位置決め停止中
		public const int S_AXS_SPIN = 0x00000040;   // SPIN動作中
		public const int S_AXS_SPINSTP = 0x00000080;    // SPIN停止中
		public const int S_AXS_TRQCTRL = 0x00000100;    // トルク制御中
		public const int S_AXS_ORGFIX = 0x00000200; // 原点確定済み
		public const int S_AXS_2ND_SLIMIT_M = 0x00000400;   // －方向第２ソフトリミット有効（Ｚ軸のみ）
		public const int S_AXS_VIRACTDIS = 0x00000800;  // 仮想点/測定点移動動作無効(A/B/C軸のみ)

		// for STATUS.ax[n].AxAlarm
		public const int S_AXA_ERALM_P = 0x00000001;    // ＋方向偏差過大
		public const int S_AXA_ERALM_M = 0x00000002;    // －方向偏差過大
		public const int S_AXA_SALM = 0x00000004;   // サーボアンプアラーム
		public const int S_AXA_SLIMIT_P = 0x00000008;   // ＋方向ソフトリミット
		public const int S_AXA_SLIMIT_M = 0x00000010;   // －方向ソフトリミット
		public const int S_AXA_HLIMIT_P = 0x00000020;   // ＋方向ハードリミット
		public const int S_AXA_HLIMIT_M = 0x00000040;   // －方向ハードリミット
		public const int S_AXA_COMLIMIT_P = 0x00000080; // ＋方向パルス発生過大
		public const int S_AXA_COMLIMIT_M = 0x00000100; // －方向パルス発生過大
		public const int S_AXA_SPWOFF = 0x00000200; // サーボ主電源ＯＦＦ
		public const int S_AXA_ECT_AXCOM = 0x00000400;  // EtherCAT各軸通信エラー
		public const int S_AXA_ECT_MLTCMD = 0x00000800; // EtherCAT多重コマンド
		public const int S_AXA_ECT_USCMD = 0x00001000;  // EtherCAT未対応コマンドエラー
		public const int S_AXA_ILLEGAL_ACT = 0x00002000;    // 不正指令エラー
		public const int S_AXA_TOUCHERR_P = 0x00004000; // ＋方向接触感知エラー
		public const int S_AXA_TOUCHERR_M = 0x00008000; // －方向接触感知エラー
		public const int S_AXA_CFA_INTERLOCK = 0x00010000;  // コレットフィンガーアーム前進中インターロック時手動操作エラー

		// for STATUS.task[n].TaskStatus
		public const int S_TKS_ALM = 0x00000001;    // アラーム発生中
		public const int S_TKS_FG_END = 0x00000002; // ＦＧ完了
		public const int S_TKS_FG_STOP = 0x00000004;    // ＦＧ中断中
		public const int S_TKS_FG_BUN = 0x00000008; // ＦＧ分配中
		public const int S_TKS_EXEC = 0x00000010;   // プログラム運転中
		public const int S_TKS_STOP = 0x00000020;   // プログラム停止中
		public const int S_TKS_BLKS = 0x00000040;   // ブロック途中停止
		public const int S_TKS_SEQ_END = 0x00000080;    // 各種シーケンス完了
		public const int S_TKS_SINGLE = 0x00000100; // シングルステップモード
		public const int S_TKS_TEACH = 0x00000200;  // ティーチングモード
		public const int S_TKS_CYCLE = 0x00000400;  // サイクル運転モード
		public const int S_TKS_MLK_STS = 0x00000800;    // マシンロック（未対応）
		public const int S_TKS_MODE = 0x0000F000;   // モード情報エリア

		public const int S_TKS_INPOS = 0x00010000;  // 割り当て軸 インポジション
		public const int S_TKS_ACC_NZE = 0x00020000;    // 割り当て軸 加減速たまり有り
		public const int S_TKS_SVON = 0x00040000;   // 割り当て軸 サーボＯＮ
		public const int S_TKS_ZRN = 0x00080000;    // 割り当て軸 原点復帰完了
		public const int S_TKS_AXMV = 0x00100000;   // 割り当て軸 独立位置決め中
		public const int S_TKS_AXMVSTP = 0x00200000;    // 割り当て軸 独立位置決め停止中

		public const int S_TKS_SENSE = 0x01000000;  // 高速センサーラッチ完
		public const int S_TKS_TANG = 0x02000000;   // Ｚ軸接線制御ＯＮ
		public const int S_TKS_REEL_END = 0x04000000;   // 最終層巻数異常警告(巻線)

		// for STATUS.task[n].TaskAlarm
		public const int S_TKA_PRGERR = 0x00000001; // プログラム実行エラー
		public const int S_TKA_MOUTERR = 0x00000002;    // Ｍコード実行エラー
		public const int S_TKA_AXIS = 0x00000004;   // 割り当て軸エラー
		public const int S_TKA_FGERR = 0x00000008;  // ＦＧ内部演算エラー
		public const int S_TKA_POWEROFF = 0x00000010;   // サーボＯＦＦエラー
		public const int S_TKA_EXTALMA = 0x00000020;    // 外部アラームＡエラー
		public const int S_TKA_EXTALMB = 0x00000040;    // 外部アラームＢエラー
		public const int S_TKA_EXTALMC = 0x00000080;    // 外部アラームＣエラー
		public const int S_TKA_EMS = 0x00000100;    // 非常停止

		// for STATUS.ecnc.Status2
		public const short S_MCS2_OPTSTOP = 0x0001; // オプショナルストップ有効
		public const short S_MCS2_TOUCHSENSE = 0x0002;  // 接触感知有効
		public const short S_MCS2_INCREFSET_MOV = 0x0004;   // 相対測定点設定時軸移動有効
		public const short S_MCS2_SINGLE = 0x0008;  // シングルステップモード
		public const short S_MCS2_SEQ_END = 0x0010; // 各種シーケンス完了
		public const short S_MCS2_XY_ILOCK_DIS = 0x0020;    // X/Y軸ｲﾝﾀｰﾛｯｸ無効
		public const short S_MCS2_CORR_ANG = 0x0040;    // ﾌﾟﾛｸﾞﾗﾑ開始時角度補正有効
		public const short S_MCS2_IN_CORR_ANG = 0x0080; // 角度補正中
		public const short S_MCS2_BLOCKSKIP = 0x0100;   // BlockSkip有効
		public const short S_MCS2_M02 = 0x0200; // M02によるﾌﾟﾛｸﾞﾗﾑ終了
		public const short S_MCS2_MCNLOCK = 0x0400; // マシンロック有効
		public const short S_MCS2_HANDLEPERMIT = 0x0800;    // 手動パルサー動作許可
		public const short S_MCS2_HANDLE_EN = 0x1000;   // 手動パルサー動作有効
														//public const short 						= 0x2000;
		public const short S_MCS2_MESSAGE_REQ = 0x4000; // メッセージ表示要求
														//public const short 						= 0x8000;

		//for STATUS.ecnc.Status3
		public const short S_MCS3_PSTOP_INPUT = 0x0001; // STOP入力によるＰ実行停止中
		public const short S_MCS3_AUTOMODE_OUTPUT = 0x0002; // 自動モード中出力
		public const short S_MCS3_VARIABLE_REQ = 0x0004;    // マクロ変数書込/読出要求
		public const short S_MCS3_CAMERA_REQ = 0x0008;  // カメラコマンド要求
		public const short S_MCS3_SHUTDWN_REQ = 0x0010; // シャットダウン動作要求

		// for STATUS.ecnc.Alarm2
		//public const short 						= 0x0001;
		//public const short 						= 0x0002;
		//public const short 						= 0x0004;
		public const short S_MCA2_GUID_INTRF = 0x0008;  // ガイドホルダ干渉エラー
														//public const short 						= 0x0010;
														//public const short 						= 0x0020;
		public const short S_MCA2_PRCS_BUCKLING = 0x0040;   // 通常電極放電加工時座屈エラー
															//public const short 						= 0x0080;
		public const short S_MCA2_Z20 = 0x0100; // Ｚ２０エラー
		public const short S_MCA2_REFOVER = 0x0200; // 端面位置計算範囲外エラー
		public const short S_MCA2_IDX_POSITION = 0x0400;    // インデックス位置決めエラー
		public const short S_MCA2_DISCHTIMEOUT = 0x0800;    // 放電加工タイムアウトエラー
		public const short S_MCA2_PUMPCOLLET_IL = 0x1000;   // ポンプＯＮ・コレットアンクランプ同時指令エラー
		public const short S_MCA2_PRSKPVPOS_OVER = 0x2000;  // 放電加工スキップ発生時仮想点登録番号範囲外エラー

		// for STATUS.ecnc.Alarm3
		public const short S_MCA3_SOURCEPRSS = 0x0001;  // 元圧エラー
		public const short S_MCA3_GIDCHG = 0x0002;  // ガイド交換確認エラー
		public const short S_MCA3_COLINST = 0x0004; // 電極取得不可エラー
		public const short S_MCA3_COLREMOV = 0x0008;    // 電極返却不可エラー
		public const short S_MCA3_ELCTDNO = 0x0010; // 電極番号未設定エラー
		public const short S_MCA3_GUIDENO = 0x0020; // ガイド番号未設定エラー
		public const short S_MCA3_CFARMPOS = 0x0040;    // ＡＥＣ開始時ｺﾚｯﾄﾌｨﾝｶﾞｰｱｰﾑ位置不正エラー
		public const short S_MCA3_GHARMPOS = 0x0080;    // ＡＥＣ開始時ガイドホルダーアーム位置不正エラー
		public const short S_MCA3_CFOPEN = 0x0100;  // ＡＥＣ開始時コレットフィンガー未オープンエラー
		public const short S_MCA3_COLCLUMP = 0x0200;    // ＡＥＣ開始時コレット未クランプエラー
		public const short S_MCA3_GIDCLUMP = 0x0400;    // ＡＥＣ開始時ガイド未クランプエラー
		public const short S_MCA3_PATRND = 0x0800;  // ﾊﾟｰﾃｨｼｮﾝ内１周ｴﾗｰ
		public const short S_MCA3_PATRANGE = 0x1000;    // ﾊﾟｰﾃｨｼｮﾝ範囲外ｴﾗｰ
		public const short S_MCA3_GDTHROUGH = 0x2000;   // ガイド貫通動作エラー

		// for STATUS.ecnc.Alarm4
		public const short S_MCA4_COLFNGR_ARM_TM = 0x0001;  // ｺﾚｯﾄﾌｨﾝｶﾞｰｱｰﾑ動作ﾀｲﾑｱｳﾄｴﾗｰ
		public const short S_MCA4_COLFNGR_OPN_TM = 0x0002;  // ｺﾚｯﾄﾌｨﾝｶﾞｰｵｰﾌﾟﾝ/ｸﾛｰｽﾞﾀｲﾑｱｳﾄｴﾗｰ
		public const short S_MCA4_COLCLUMP_TM = 0x0004; // ｺﾚｯﾄｸﾗﾝﾌﾟ/ｱﾝｸﾗﾝﾌﾟﾀｲﾑｱｳﾄｴﾗｰ
		public const short S_MCA4_GUID_ARM_TM = 0x0008; // ｶﾞｲﾄﾞﾎﾙﾀﾞｰｱｰﾑ動作ﾀｲﾑｱｳﾄｴﾗｰ
		public const short S_MCA4_GUIDCLUMP_TM = 0x0010;    // ｶﾞｲﾄﾞｸﾗﾝﾌﾟ/ｱﾝｸﾗﾝﾌﾟﾀｲﾑｱｳﾄｴﾗｰ
															//public const short 						= 0x0020;
															//public const short 						= 0x0040;
															//public const short 						= 0x0080;
		public const short S_MCA4_COLFNGR_ARM_IL = 0x0100;  // ｺﾚｯﾄﾌｨﾝｶﾞｰｱｰﾑ状態不一致
		public const short S_MCA4_COLFNGR_OPN_IL = 0x0200;  // ｺﾚｯﾄﾌｨﾝｶﾞｰｵｰﾌﾟﾝ/ｸﾛｰｽﾞ状態不一致
		public const short S_MCA4_COLCLUMP_IL = 0x0400; // ｺﾚｯﾄｸﾗﾝﾌﾟ/ｱﾝｸﾗﾝﾌﾟ状態不一致
		public const short S_MCA4_GUID_ARM_IL = 0x0800; // ｶﾞｲﾄﾞﾎﾙﾀﾞｰｱｰﾑ動作状態不一致
		public const short S_MCA4_GUIDCLUMP_IL = 0x1000;    // ｶﾞｲﾄﾞｸﾗﾝﾌﾟ/ｱﾝｸﾗﾝﾌﾟ状態不一致
															//public const short 						= 0x2000;
															//public const short 						= 0x4000;
															//public const short 						= 0x8000;

		// for STATUS.ecnc.Alarm5
		public const short S_MCA5_MCR_UNDEFINE = 0x0001;    // マクロ変数読み込み/書き込み正体不明エラー
															//public const short 						= 0x0002;
															//public const short 						= 0x0004;
															//public const short 						= 0x0008;
		public const short S_MCA5_SPXMCR_READ = 0x0010; // RTMC64(旧SPX)管理マクロ変数読み込みエラー
		public const short S_MCA5_SPXMCR_WRITE = 0x0020;    // RTMC64(旧SPX)管理マクロ変数書き込みエラー
		public const short S_MCA5_OUTOFRANGE = 0x0040;  // 範囲外マクロ変数指定エラー
														//public const short 						= 0x0080;
		public const short S_MCA5_OPERATION = 0x0100;   // マクロ演算エラー
														//public const short 						= 0x0200;
														//public const short 						= 0x0400;
														//public const short 						= 0x0800;
		public const short S_MCA5_MSG_SEQ = 0x1000; // メッセージ表示送受信シーケンスエラー
		public const short S_MCA5_MSG_ANS = 0x2000; // メッセージ表示結果エラー


		public const short S_MCE2_HLDTNK_EMP = 0x1000; // メッセージ表示結果エラー
		
		//	全体
		#region MC.STATUS
		/// <summary>アラーム発生中</summary>
		/// <remarks>
		/// 全タスクの全軸の中でアラームがひとつでも発生しているとONします。
		/// </remarks>
		public bool McStatusAlarm { get { return HasFlag( Main.Status, S_MCS_ALM ); } }
		/// <summary>全タスクセッティングモード</summary>
		public bool McStatusSetting { get { return HasFlag( Main.Status, S_MCS_SETTING ); } }
		/// <summary>運転プログラムデータ変更</summary>
		public bool McStatusProgramChanged { get { return HasFlag( Main.Status, S_MCS_PRGCHG ); } }
		/// <summary>制御周期負荷ワーニング</summary>
		public bool McStatusRtcWarn { get { return HasFlag( Main.Status, S_MCS_RTCWARN ); } }
		/// <summary>制御周期負荷過大</summary>
		public bool McStatusRtcFail { get { return HasFlag( Main.Status, S_MCS_RTCFAIL ); } }
		/// <summary>FDT読み込みワーニング</summary>
		public bool McStatusFdtWarn { get { return HasFlag( Main.Status, S_MCS_FDTWARN ); } }
		#endregion
		#region MC.ALARM
		/// <summary>バックアップエラー</summary>
		public bool McAlarmBackup { get { return HasFlag( Main.Alarm, S_MCA_BACKUP ); } }
		/// <summary>ROMSWバックアップエラー</summary>
		public bool McAlarmBackupRomSw { get { return HasFlag( Main.Alarm, S_MCA_ROMSW_BKUP ); } }
		/// <summary>パラメータ未設定エラー</summary>
		public bool McAlarmParameter { get { return HasFlag( Main.Alarm, S_MCA_PARAMETER ); } }
		/// <summary>EtherCAT 初期化エラー</summary>
		public bool McAlarmEtherCatInit { get { return HasFlag( Main.Alarm, S_MCA_ECT_INIT ); } }
		/// <summary>EtherCAT 全体通信エラー</summary>
		public bool McAlarmEtherCatWcom { get { return HasFlag( Main.Alarm, S_MCA_ECT_WCOM ); } }
		/// <summary>EtherCAT IO通信エラー</summary>
		public bool McAlarmEtherCatIoComm { get { return HasFlag( Main.Alarm, S_MCA_ECT_IO_COMM ); } }
		/// <summary>FDT読み込みエラー</summary>
		public bool McAlarmFdtFail { get { return HasFlag( Main.Alarm, S_MCA_FDTFAIL ); } }
		/// <summary>システムエラー</summary>
		public bool McAlarmSystem { get { return HasFlag( Main.Alarm, S_MCA_SYSTEM ); } }
		#endregion

		//	AXIS
		#region AXIS.STATUS
		/// <summary>原点復帰完了</summary>
		/// <param name="number">軸番号</param>
		/// <returns>原点復帰完了の是非</returns>
		public bool CompletedReturnToOrigin( AxisNumbers number )
		{
			return HasFlag( AxisStatus( number ), S_AXS_ZRN );
		}
		/// <summary>軸ごとの原点復帰状態</summary>
		public AxisBits CompletedReturnToOrigins
		{
			get
			{
				AxisBits result = AxisBits.Free;
				if( true == CompletedReturnToOrigin( AxisNumbers.X ) ) {
					result |= AxisBits.X;
				}
				if( true == CompletedReturnToOrigin( AxisNumbers.Y ) ) {
					result |= AxisBits.Y;
				}
				if( true == CompletedReturnToOrigin( AxisNumbers.W ) ) {
					result |= AxisBits.W;
				}
				if( true == CompletedReturnToOrigin( AxisNumbers.Z ) ) {
					result |= AxisBits.Z;
				}
				if( true == CompletedReturnToOrigin( AxisNumbers.A ) ) {
					result |= AxisBits.A;
				}
				if( true == CompletedReturnToOrigin( AxisNumbers.B ) ) {
					result |= AxisBits.B;
				}
				if( true == CompletedReturnToOrigin( AxisNumbers.C ) ) {
					result |= AxisBits.C;
				}
				if( true == CompletedReturnToOrigin( AxisNumbers.I ) ) {
					result |= AxisBits.I;
				}
				return result;
			}
		}
		#endregion

		#region 座標
		/// <summary>機械位置</summary>
		public StructureAxisCoordinate CoordinateAsPosReg
		{
			get
			{
				StructureAxisCoordinate temp = new StructureAxisCoordinate();
				temp.Axis1 = Axes[0].PosReg;
				temp.Axis2 = Axes[1].PosReg;
				temp.Axis3 = Axes[2].PosReg;
				temp.Axis4 = Axes[3].PosReg;
				temp.Axis5 = Axes[4].PosReg;
				temp.Axis6 = Axes[5].PosReg;
				temp.Axis7 = Axes[6].PosReg;
				temp.Axis8 = Axes[7].PosReg;
				return temp;
			}
		}
		/// <summary>絶対位置</summary>
		public StructureAxisCoordinate CoordinateAsAbsReg
		{
			get
			{
				StructureAxisCoordinate temp = new StructureAxisCoordinate();
				temp.Axis1 = Axes[0].AbsReg;
				temp.Axis2 = Axes[1].AbsReg;
				temp.Axis3 = Axes[2].AbsReg;
				temp.Axis4 = Axes[3].AbsReg;
				temp.Axis5 = Axes[4].AbsReg;
				temp.Axis6 = Axes[5].AbsReg;
				temp.Axis7 = Axes[6].AbsReg;
				temp.Axis8 = Axes[7].AbsReg;
				return temp;
			}
		}
		#endregion

		#region AXIS.ALARM
		/// <summary>＋方向偏差過大</summary>
		/// <param name="number">軸番号</param>
		/// <returns>判定結果</returns>
		public bool AxisAlarmExcessiveDeviationPlus( AxisNumbers number )
		{
			return HasFlag( AxisAlarm( number ), S_AXA_ERALM_P );
		}
		/// <summary>－方向偏差過大</summary>
		/// <param name="number">軸番号</param>
		/// <returns>判定結果</returns>
		public bool AxisAlarmExcessiveDeviationMinus( AxisNumbers number )
		{
			return HasFlag( AxisAlarm( number ), S_AXA_ERALM_M );
		}
		/// <summary>サーボアンプアラーム</summary>
		/// <param name="number">軸番号</param>
		/// <returns>判定結果</returns>
		public bool AxisAlarmServoAmp( AxisNumbers number )
		{
			return HasFlag( AxisAlarm( number ), S_AXA_SALM );
		}
		/// <summary>＋方向ソフトリミット</summary>
		/// <param name="number">軸番号</param>
		/// <returns>判定結果</returns>
		public bool AxisAlarmSoftLimitPlus( AxisNumbers number )
		{
			return HasFlag( AxisAlarm( number ), S_AXA_SLIMIT_P );
		}
		/// <summary>－方向ソフトリミット</summary>
		/// <param name="number">軸番号</param>
		/// <returns>判定結果</returns>
		public bool AxisAlarmSoftLimitMinus( AxisNumbers number )
		{
			return HasFlag( AxisAlarm( number ), S_AXA_SLIMIT_M );
		}
		/// <summary>＋方向ハードリミット</summary>
		/// <param name="number">軸番号</param>
		/// <returns>判定結果</returns>
		public bool AxisAlarmHardLimitPlus( AxisNumbers number )
		{
			return HasFlag( AxisAlarm( number ), S_AXA_HLIMIT_P );
		}
		/// <summary>－方向ハードリミット</summary>
		/// <param name="number">軸番号</param>
		/// <returns>判定結果</returns>
		public bool AxisAlarmHardLimitMinus( AxisNumbers number )
		{
			return HasFlag( AxisAlarm( number ), S_AXA_HLIMIT_M );
		}
		/// <summary>＋方向パルス発生過大</summary>
		/// <param name="number">軸番号</param>
		/// <returns>判定結果</returns>
		public bool AxisAlarmExcessivePulsePlus( AxisNumbers number )
		{
			return HasFlag( AxisAlarm( number ), S_AXA_COMLIMIT_P );
		}
		/// <summary>－方向パルス発生過大</summary>
		/// <param name="number">軸番号</param>
		/// <returns>判定結果</returns>
		public bool AxisAlarmExcessivePulseMinus( AxisNumbers number )
		{
			return HasFlag( AxisAlarm( number ), S_AXA_COMLIMIT_M );
		}
		/// <summary>サーボ主電源ＯＦＦ</summary>
		/// <param name="number">軸番号</param>
		/// <returns>判定結果</returns>
		public bool AxisAlarmServoPowerOff( AxisNumbers number )
		{
			return HasFlag( AxisAlarm( number ), S_AXA_SPWOFF );
		}
		/// <summary>EtherCAT各軸通信エラー</summary>
		/// <param name="number">軸番号</param>
		/// <returns>判定結果</returns>
		public bool AxisAlarmEtherCatComm( AxisNumbers number )
		{
			return HasFlag( AxisAlarm( number ), S_AXA_ECT_AXCOM );
		}
		/// <summary>EtherCAT多重コマンド</summary>
		/// <param name="number">軸番号</param>
		/// <returns>判定結果</returns>
		public bool AxisAlarmEtherCatCommandMultiple( AxisNumbers number )
		{
			return HasFlag( AxisAlarm( number ), S_AXA_ECT_MLTCMD );
		}
		/// <summary>EtherCAT未対応コマンドエラー</summary>
		/// <param name="number">軸番号</param>
		/// <returns>判定結果</returns>
		public bool AxisAlarmEtherCatCommandNotSupported( AxisNumbers number )
		{
			return HasFlag( AxisAlarm( number ), S_AXA_ECT_MLTCMD );
		}
		/// <summary>不正指令エラー</summary>
		/// <param name="number">軸番号</param>
		/// <returns>判定結果</returns>
		public bool AxisAlarmIlligalAction( AxisNumbers number )
		{
			return HasFlag( AxisAlarm( number ), S_AXA_ILLEGAL_ACT );
		}
		/// <summary>＋方向接触感知エラー</summary>
		/// <param name="number">軸番号</param>
		/// <returns>判定結果</returns>
		public bool AxisAlarmTouchSensorPlus( AxisNumbers number )
		{
			return HasFlag( AxisAlarm( number ), S_AXA_TOUCHERR_P );
		}
		/// <summary>－方向接触感知エラー</summary>
		/// <param name="number">軸番号</param>
		/// <returns>判定結果</returns>
		public bool AxisAlarmTouchSensorMinus( AxisNumbers number )
		{
			return HasFlag( AxisAlarm( number ), S_AXA_TOUCHERR_M );
		}
		/// <summary>コレットフィンガーアーム前進中インターロック時手動操作エラー</summary>
		/// <param name="number">軸番号</param>
		/// <returns>判定結果</returns>
		public bool AxisAlarmColletFingerArmInterlock( AxisNumbers number ) { return HasFlag( AxisAlarm( number ), S_AXA_CFA_INTERLOCK ); }
		#endregion

		//	TASK
		#region TASK.STATUS
		/// <summary>動作モード</summary>
		public McTaskModes MotionMode
		{
			get
			{
				int mode = ( TaskStatus & S_TKS_MODE ) >> 12;
				if( ( 0 > mode ) || ( 4 < mode ) ) {
					return McTaskModes.NotSupported;
				}
				return (McTaskModes)mode;
			}
		}
		/// <summary>FG完</summary>
		public bool CompletedFg { get { return HasFlag( TaskStatus, S_TKS_FG_END ); } }
        /// <summary>FG停止中</summary>
        public bool StoppedFg { get { return HasFlag(TaskStatus, S_TKS_FG_STOP); } }
        /// <summary>FG中</summary>
		public bool RunningFg { get { return HasFlag(TaskStatus, S_TKS_FG_BUN); } }
        /// <summary>プログラム実行中</summary>
        public bool ProgramRunning { get { return HasFlag( TaskStatus, S_TKS_EXEC ); } }
        #endregion

        #region TASK.ALARM
        /// <summary>プログラム実行エラー</summary>
        public bool TaskAlarmProgram { get { return HasFlag( TaskAlarm, S_TKA_PRGERR ); } }
		/// <summary>Ｍコード実行エラー</summary>
		public bool TaskAlarmMCode { get { return HasFlag( TaskAlarm, S_TKA_MOUTERR ); } }
		/// <summary>割り当て軸エラー</summary>
		public bool TaskAlarmAxis { get { return HasFlag( TaskAlarm, S_TKA_AXIS ); } }
		/// <summary>ＦＧ内部演算エラー</summary>
		public bool TaskAlarmFg { get { return HasFlag( TaskAlarm, S_TKA_FGERR ); } }
		/// <summary>サーボＯＦＦエラー</summary>
		public bool TaskAlarmServoPowerOff { get { return HasFlag( TaskAlarm, S_TKA_POWEROFF ); } }
		/// <summary>外部アラームＡエラー</summary>
		public bool TaskAlarmExternalA { get { return HasFlag( TaskAlarm, S_TKA_EXTALMA ); } }
		/// <summary>外部アラームＢエラー</summary>
		public bool TaskAlarmExternalB { get { return HasFlag( TaskAlarm, S_TKA_EXTALMB ); } }
		/// <summary>外部アラームＣエラー</summary>
		public bool TaskAlarmExternalC { get { return HasFlag( TaskAlarm, S_TKA_EXTALMC ); } }
		/// <summary>非常停止</summary>
		public bool TaskAlarmEmergencyStop { get { return HasFlag( TaskAlarm, S_TKA_EMS ); } }
		#endregion

		#region オーバライド
		/// <summary>オーバライド(全体)</summary>
		public short OverrideAsOverall { get { return Tasks[0].Override; } }
		/// <summary>オーバライド(補間)</summary>
		public short OverrideAsInterpolation { get { return Tasks[0].COverride; } }
		/// <summary>オーバライド(主軸)</summary>
		public short OverrideAsSpindle { get { return Tasks[0].SOverride; } }
        #endregion

        #region プログラム実行番号
        /// <summary>プログラム待機・実行行取得</summary>
        public short LineNo { get { return Tasks[0].LineNo; } }
        #endregion

        //	ECNC
        #region ECNC.STATUS2
        /// <summary>シークエンス完了</summary>
        public bool CompletedSequence { get { return HasFlag( Ecnc.Status2, S_MCS2_SEQ_END ); } }
		/// <summary>オプショナルストップ 無効／有効</summary>
		public bool OptionalStop { get { return HasFlag( Ecnc.Status2, S_MCS2_OPTSTOP ); } }
		/// <summary>接触感知 無効／有効</summary>
		public bool TouchSensor { get { return HasFlag( Ecnc.Status2, S_MCS2_TOUCHSENSE ); } }
		/// <summary>相対測定点設定時軸移動 無効／有効</summary>
		public bool IncrimentalReferenceAxisMove { get { return HasFlag( Ecnc.Status2, S_MCS2_INCREFSET_MOV ); } }
        /// <summary>シングルステップモード 無効／有効</summary>
        public bool SingleStepMode { get { return HasFlag(Ecnc.Status2, S_MCS2_SINGLE ); } }
        /// <summary>X,Y軸インターロック無効／有効</summary>
        public bool InterlockXY { get { return ( true == HasFlag( Ecnc.Status2, S_MCS2_XY_ILOCK_DIS ) ) ? false : true; } }
		/// <summary>手動パルサ選択無効／有効</summary>
		/// <value>
		/// <list type="bullet" >
		///		<item>0=無効</item>
		///		<item>1=有効</item>
		/// </list>
		/// </value>
		public bool HandPulserPermition { get { return HasFlag( Ecnc.Status2, S_MCS2_HANDLEPERMIT ); } }
		/// <summary>W軸上限値設定値</summary>
		public int WAxisUpperLimit { get { return Ecnc.WTopPos; } }
        /// <summary>W軸上限値設定値</summary>
		public int CorrectAngleValue { get { return Ecnc.CorrAng; } }
        /// <summary>メッセージ表示要求</summary>
        public bool RequestShowMessage { get { return HasFlag( Ecnc.Status2, S_MCS2_MESSAGE_REQ ); } }
		/// <summary>プログラム開始時角度補正有効</summary>
		public bool CorrectAngle { get { return HasFlag( Ecnc.Status2, S_MCS2_CORR_ANG ); } }
		/// <summary>ブロックスキップ</summary>
		public bool BlockSkip { get { return HasFlag( Ecnc.Status2, S_MCS2_BLOCKSKIP ); } }
		/// <summary>マシンロック</summary>
		public bool MachineLock { get { return HasFlag( Ecnc.Status2, S_MCS2_MCNLOCK ); } }
        /// <summary>M02によるプログラム終了</summary>
        public bool M02En { get { return HasFlag(Ecnc.Status2, S_MCS2_M02); } }

		#endregion

		#region ECNC.STATUS3
		/// <summary>自動モード中出力</summary>
		public bool AutoModeOutput { get { return HasFlag( Ecnc.Status3, S_MCS3_AUTOMODE_OUTPUT ); } }
		/// <summary>シャットダウン動作要求</summary>
		public bool RequestShutDown { get { return HasFlag( Ecnc.Status3, S_MCS3_SHUTDWN_REQ ); } }
		#endregion

		#region ECNC.ALARM2
		/// <summary>ガイドホルダ干渉エラー</summary>
		public bool EcncAlarmGuideHolderInterference { get { return HasFlag( Ecnc.Alarm2, S_MCA2_GUID_INTRF ); } }
		/// <summary>通常電極放電加工時座屈エラー</summary>
		public bool EcncAlarmProcessBuckling { get { return HasFlag( Ecnc.Alarm2, S_MCA2_PRCS_BUCKLING ); } }
		/// <summary>Ｚ２０エラー</summary>
		public bool EcncAlarmZ20 { get { return HasFlag( Ecnc.Alarm2, S_MCA2_Z20 ); } }
		/// <summary>端面位置計算範囲外エラー</summary>
		public bool EcncAlarmReferenceOutOfRange { get { return HasFlag( Ecnc.Alarm2, S_MCA2_REFOVER ); } }
		/// <summary>インデックス位置決めエラー</summary>
		public bool EcncAlarmIndexPosition { get { return HasFlag( Ecnc.Alarm2, S_MCA2_IDX_POSITION ); } }
		/// <summary>放電加工タイムアウトエラー</summary>
		public bool EcncAlarmDischargeTimeout { get { return HasFlag( Ecnc.Alarm2, S_MCA2_DISCHTIMEOUT ); } }
		/// <summary>ポンプＯＮ・コレットアンクランプ同時指令エラー</summary>
		public bool EcncAlarmPumpOnAndColletUnclamp { get { return HasFlag( Ecnc.Alarm2, S_MCA2_PUMPCOLLET_IL ); } }
		/// <summary>放電加工スキップ発生時仮想点登録番号範囲外エラー</summary>
		public bool EcncAlarmVirtualPositionOutOfRange { get { return HasFlag( Ecnc.Alarm2, S_MCA2_PRSKPVPOS_OVER ); } }
		#endregion

		#region ECNC.ALARM3
		/// <summary>元圧エラー</summary>
		public bool EcncAlarmSourcePressure { get { return HasFlag( Ecnc.Alarm3, S_MCA3_SOURCEPRSS ); } }
		/// <summary>ガイド交換確認エラー</summary>
		public bool EcncAlarmGuideExchange { get { return HasFlag( Ecnc.Alarm3, S_MCA3_GIDCHG ); } }
		/// <summary>電極取得不可エラー</summary>
		public bool EcncAlarmCannotInstallCollet { get { return HasFlag( Ecnc.Alarm3, S_MCA3_COLINST ); } }
		/// <summary>電極返却不可エラー</summary>
		public bool EcncAlarmCannotRemoveCollet { get { return HasFlag( Ecnc.Alarm3, S_MCA3_COLREMOV ); } }
		/// <summary>電極番号未設定エラー</summary>
		public bool EcncAlarmElectrodeNumberNotSet { get { return HasFlag( Ecnc.Alarm3, S_MCA3_ELCTDNO ); } }
		/// <summary>ガイド番号未設定エラー</summary>
		public bool EcncAlarmGuideNumberNotSet { get { return HasFlag( Ecnc.Alarm3, S_MCA3_GUIDENO ); } }
		/// <summary>ＡＥＣ開始時コレットフィンガーアーム位置不正エラー</summary>
		public bool EcncAlarmAecColletFingerArmIllegalPosition { get { return HasFlag( Ecnc.Alarm3, S_MCA3_CFARMPOS ); } }
		/// <summary>ＡＥＣ開始時ガイドホルダーアーム位置不正エラー</summary>
		public bool EcncAlarmAecGuideHolderArmIllegalPosition { get { return HasFlag( Ecnc.Alarm3, S_MCA3_GHARMPOS ); } }
		/// <summary>ＡＥＣ開始時コレットフィンガー未オープンエラー</summary>
		public bool EcncAlarmAecColletFingerNotOpen { get { return HasFlag( Ecnc.Alarm3, S_MCA3_CFOPEN ); } }
		/// <summary>ＡＥＣ開始時コレット未クランプエラー</summary>
		public bool EcncAlarmAecColletFingerNotClamp { get { return HasFlag( Ecnc.Alarm3, S_MCA3_COLCLUMP ); } }
		/// <summary>ＡＥＣ開始時ガイド未クランプエラー</summary>
		public bool EcncAlarmAecGuideHolderNotClamp { get { return HasFlag( Ecnc.Alarm3, S_MCA3_GIDCLUMP ); } }
		/// <summary>パーティション内１周エラー</summary>
		public bool EcncAlarmPartitionAround { get { return HasFlag( Ecnc.Alarm3, S_MCA3_PATRND ); } }
		/// <summary>パーティション範囲外エラー</summary>
		public bool EcncAlarmPartitionOutOfRange { get { return HasFlag( Ecnc.Alarm3, S_MCA3_PATRANGE ); } }
		/// <summary>ガイド貫通動作エラー</summary>
		public bool EcncAlarmGuideThroughAction { get { return HasFlag( Ecnc.Alarm3, S_MCA3_GDTHROUGH ); } }
		#endregion

		#region ECNC.ALARM4
		/// <summary>コレットフィンガーアーム動作タイムアウトエラー</summary>
		public bool EcncAlarmColletFingerArmActionTimeout { get { return HasFlag( Ecnc.Alarm4, S_MCA4_COLFNGR_ARM_TM ); } }
		/// <summary>コレットフィンガー オープン／クローズ タイムアウトエラー</summary>
		public bool EcncAlarmColletFingerArmOpenCloseTimeout { get { return HasFlag( Ecnc.Alarm4, S_MCA4_COLFNGR_OPN_TM ); } }
		/// <summary>コレット クランプ／アンクランプ タイムアウトエラー</summary>
		public bool EcncAlarmColletClampUnclampTimout { get { return HasFlag( Ecnc.Alarm4, S_MCA4_COLCLUMP_TM ); } }
		/// <summary>ガイドホルダーアーム動作タイムアウトエラー</summary>
		public bool EcncAlarmGuideHolderArmActionTimeout { get { return HasFlag( Ecnc.Alarm4, S_MCA4_GUID_ARM_TM ); } }
		/// <summary>ガイド クランプ／アンクランプ タイムアウトエラー</summary>
		public bool EcncAlarmGuideClampUnclampTimeout { get { return HasFlag( Ecnc.Alarm4, S_MCA4_GUIDCLUMP_TM ); } }
		/// <summary>コレットフィンガーアーム 状態不一致</summary>
		public bool EcncAlarmColletFingerArmUnmatch { get { return HasFlag( Ecnc.Alarm4, S_MCA4_COLFNGR_ARM_IL ); } }
		/// <summary>コレットフィンガー オープン／クローズ 状態不一致</summary>
		public bool EcncAlarmColletFingerOpenCloseUnmatch { get { return HasFlag( Ecnc.Alarm4, S_MCA4_COLFNGR_OPN_IL ); } }
		/// <summary>コレット クランプ／アンクランプ 状態不一致</summary>
		public bool EcncAlarmColletClampUnclampUnmatch { get { return HasFlag( Ecnc.Alarm4, S_MCA4_COLCLUMP_IL ); } }
		/// <summary>ガイドホルダーアーム動作 状態不一致</summary>
		public bool EcncAlarmGuideHolderArmActionUnmatch { get { return HasFlag( Ecnc.Alarm4, S_MCA4_GUID_ARM_IL ); } }
		/// <summary>ガイドクランプ／アンクランプ 状態不一致</summary>
		public bool EcncAlarmGuideClampUnclampUnmatch { get { return HasFlag( Ecnc.Alarm4, S_MCA4_GUIDCLUMP_IL ); } }
		#endregion

		#region ECNC.ALARM5
		/// <summary>マクロ変数読み込み/書き込み正体不明エラー</summary>
		public bool EcncAlarmMacroVariableReadWriteUndefine { get { return HasFlag( Ecnc.Alarm5, S_MCA5_MCR_UNDEFINE ); } }
		/// <summary>RTMC64(旧SPX)管理マクロ変数読み込みエラー</summary>
		public bool EcncAlarmMacroVariableRead { get { return HasFlag( Ecnc.Alarm5, S_MCA5_SPXMCR_READ ); } }
		/// <summary>RTMC64(旧SPX)管理マクロ変数書き込みエラー</summary>
		public bool EcncAlarmMacroVariableWrite { get { return HasFlag( Ecnc.Alarm5, S_MCA5_SPXMCR_WRITE ); } }
		/// <summary>範囲外マクロ変数指定エラー</summary>
		public bool EcncAlarmMacroVariableOutOfRange { get { return HasFlag( Ecnc.Alarm5, S_MCA5_OUTOFRANGE ); } }
		/// <summary>マクロ演算エラー</summary>
		public bool EcncAlarmMacroOperation { get { return HasFlag( Ecnc.Alarm5, S_MCA5_OPERATION ); } }
		/// <summary>メッセージ表示送受信シーケンスエラー</summary>
		public bool EcncAlarmMessageRequest { get { return HasFlag( Ecnc.Alarm5, S_MCA5_MSG_SEQ ); } }
		/// <summary>メッセージ表示結果エラー</summary>
		public bool EcncAlarmMessageAnswer { get { return HasFlag( Ecnc.Alarm5, S_MCA5_MSG_ANS ); } }
		#endregion

		#region  ECNC.EtherCAT
		public bool EtherCatErrorHoldingTankLiquidEmpty { get { return HasFlag( Ecnc.EIFErr2, S_MCE2_HLDTNK_EMP ); } }
		#endregion

				/// <summary>タスクステータス取得</summary>
				/// <returns>タスクステータス値</returns>
				/// <remarks>
				/// 選択されているタスク番号のステータス値を取得します。
				/// </remarks>
		private int TaskStatus
		{
			get { return ( true == VerifyTaskNumber( TargetTaskNumber ) ) ? Tasks[TargetTaskNumber].TaskStatus : 0; }
		}
		/// <summary>タスクアラーム</summary>
		private int TaskAlarm
		{
			get { return ( true == VerifyTaskNumber( TargetTaskNumber ) ) ? Tasks[TargetTaskNumber].TaskAlarm : 0; }
		}
		/// <summary>軸ステータス取得</summary>
		/// <param name="axisNumber">軸番号</param>
		/// <returns>ステータス値</returns>
		private int AxisStatus( AxisNumbers axisNumber )
		{
			return ( true == VerifyAxisNumber( (short)axisNumber ) ) ? Axes[(short)axisNumber].AxStatus : 0;
		}
		/// <summary>軸アラーム取得</summary>
		/// <param name="axisNumber">軸番号</param>
		/// <returns>アラーム値</returns>
		private int AxisAlarm( AxisNumbers axisNumber )
		{
			return ( true == VerifyAxisNumber( (short)axisNumber ) ) ? Axes[(short)axisNumber].AxAlarm : 0;
		}

		/// <summary>軸番号正当性チェック</summary>
		/// <param name="number">判定対象となるタスク番号</param>
		/// <returns>判定結果
		///		<list type="bullet" >
		///			<item>true=正当</item>
		///			<item>false=不当</item>
		///		</list>
		/// </returns>
		private bool VerifyAxisNumber( int number )
		{
			while( null != Axes ) {
				if( 0 > number ) {
					break;
				}
				int length = Axes.Length;
				if( 1 > length ) {
					break;
				}
				if( length > (int)number ) {
					return true;
				}
				break;
			}
			return false;
		}
		/// <summary>タスク番号正当性チェック</summary>
		/// <param name="number">判定対象となるタスク番号</param>
		/// <returns>判定結果
		///		<list type="bullet" >
		///			<item>true=正当</item>
		///			<item>false=不当</item>
		///		</list>
		/// </returns>
		private bool VerifyTaskNumber( int number )
		{
			while( null != Tasks ) {
				if( 0 > TargetTaskNumber ) {
					break;
				}
				int length = Tasks.Length;
				if( 1 > length ) {
					break;
				}
				if( length > TargetTaskNumber ) {
					return true;
				}
				break;
			}
			return false;
		}
		/// <summary>ヒットフラグ判定</summary>
		/// <param name="target">判定対象</param>
		/// <param name="mask">マスク値</param>
		/// <returns>判定結果</returns>
		protected bool HasFlag( short target, short mask )
		{
			return ( mask == ( target & mask ) ) ? true : false;
		}
		/// <summary>ヒットフラグ判定</summary>
		/// <param name="target">判定対象</param>
		/// <param name="mask">マスク値</param>
		/// <returns>判定結果</returns>
		protected bool HasFlag( int target, int mask )
		{
			return ( mask == ( target & mask ) ) ? true : false;
		}
	}
}
