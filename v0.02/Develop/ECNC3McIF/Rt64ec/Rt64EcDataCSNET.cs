using System;
using System.Runtime.InteropServices;

public class Rt64ecdata
{
    // ------------------------------------------------------------------------
    //		サーボパラメータデータ構造体（９０８０ﾊﾞｲﾄ固定長）
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct AXIS_PARAMETER    // 各軸独立パラメータ構造体（１００ﾊﾞｲﾄ固定長）
    {
        public int InPos;                   // ＩＮＰＯＳ量 [pls]
        public int ErMax;                   // 偏差上限値 [pls]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public sbyte[] Reserved0;
        public int Ka;                      // 補間時定数 [msec]
        public int SKa;                     // Ｓ字補間時定数 [msec]
        public int Dx;                      // ＰＴＰ時定数 [msec]
        public int PtpFeed;                 // ＰＴＰ速度 [pls/sec]
        public int JogFeed;                 // ＪＯＧ送り速度[pls/sec]
        public int SoftLimP;                // ソフトリミット＋側 [pls]
        public int SoftLimM;                // ソフトリミット－側 [pls]
        public int OrgDir;                  // 原点復帰方向
        public int OrgOfs;                  // 原点距離 [pls]
        public int OrgPos;                  // 原点復帰逃げ位置 (未使用) [pls]
        public int OrgFeed;                 // 原点復帰早送り速度 [pls/sec]
        public int AprFeed;                 // 原点復帰アプローチ速度 [pls/sec]
        public int SrchFeed;                // 原点復帰最終サーチ速度 [pls/sec]
        public int OrgPri;                  // 原点復帰順位
        public int Homepos;                 // ﾎｰﾑﾎﾟｼﾞｼｮﾝ位置 [pls]
        public int Homepri;                 // ﾎｰﾑﾎﾟｼﾞｼｮﾝ順位
        public int BackL;                   // バックラッシュ補正量 [pls]
        public int Revise;                  // 形状補正係数
        public int OrgCsetOfs;              // 原点復帰時論理座標
        public int handle_max;              // ｼﾞｮｲｽﾃｨｯｸ/ﾊﾝﾄﾞﾙ最大送り速度
        public int handle_ka;               // ｼﾞｮｲｽﾃｨｯｸ/ﾊﾝﾄﾞﾙ加減速時定数
        public int PrcsKa;                  // 放電加工時定数
        public int AecSoftLimP;             // ＡＥＣ時ソフトリミット＋側
        public int AecSoftLimM;             // ＡＥＣ時ソフトリミット－側
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public sbyte[] Reserved1;
        public static AXIS_PARAMETER Init()
        {
            AXIS_PARAMETER tmp = new AXIS_PARAMETER();
            tmp.Reserved0 = new sbyte[4];
            tmp.Reserved1 = new sbyte[32];
            return tmp;
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct ADD_PARAMETER         // 特殊パラメータ共用体（１２０バイト）
    {
        public long AxAecSoftLim;           // ＡＥＣ時ソフトリミット有効軸フラグ
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 112)]
        public sbyte[] Reserved;
        public static ADD_PARAMETER Init()
        {
            ADD_PARAMETER tmp = new ADD_PARAMETER();
            tmp.Reserved = new sbyte[112];
            return tmp;
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct PARAMETER_DATA            // （９０８０バイト）
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public AXIS_PARAMETER[] AxisParam;
        public ADD_PARAMETER AddParam;      // 特殊パラメータ未定義
        public static PARAMETER_DATA Init()
        {
            PARAMETER_DATA tmp = new PARAMETER_DATA();
            tmp.AxisParam = new AXIS_PARAMETER[64];
            for (int cnt = tmp.AxisParam.GetLowerBound(0); cnt <= tmp.AxisParam.GetUpperBound(0); cnt++)
                tmp.AxisParam[cnt] = Rt64ecdata.AXIS_PARAMETER.Init();
            tmp.AddParam = Rt64ecdata.ADD_PARAMETER.Init();
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    // オプションパラメータデータ構造体（５１２ﾊﾞｲﾄ固定長）
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct OPTIONPRM_DATA
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
        public sbyte[] Reserved;            // 未使用
        public static OPTIONPRM_DATA Init()
        {
            OPTIONPRM_DATA tmp = new OPTIONPRM_DATA();
            tmp.Reserved = new sbyte[512];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    // アンサーステータス情報構造体（３４００ﾊﾞｲﾄ）
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct MCSTATUS                  // 全体情報構造体
    {
        public int Status;                  // 全体ステータス
        public int Alarm;                   // 全体アラーム
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public sbyte[] Reserved; // 未使用
        public static MCSTATUS Init()
        {
            MCSTATUS tmp = new MCSTATUS();
            tmp.Reserved = new sbyte[8];
            return tmp;
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct AXSTATUS                  // 各軸情報構造体
    {
        public int AxStatus;                // 軸ステータス
        public int AxAlarm;                 // 軸アラーム
        public int ComReg;                  // 指令位置
        public int PosReg;                  // 機械位置
        public int ErrReg;                  // 偏差量
        public int BlockSeg;                // 最新ブロック払い出し量
        public int AbsReg;                  // 絶対位置
        public int Trq;                     // トルク
        public int AMrReg;                  // 絶対位置(指令:ﾏｼﾝﾛｯｸ込み)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public sbyte[] Reserved;            // 未使用
        public static AXSTATUS Init()
        {
            AXSTATUS tmp = new AXSTATUS();
            tmp.Reserved = new sbyte[12];
            return tmp;
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct TASKSTATUS                // タスク情報構造体
    {
        public int TaskStatus;              // タスクステータス
        public int TaskAlarm;               // タスクアラーム
        public short Override;              // 送りオーバーライド設定
        public short COverride;             // 補間オーバーライド設定
        public short SOverride;             // 主軸オーバーライド設定
        public short ProgramNo;             // 選択・実行プログラム番号
        public int StepNo;                  // 実行ステップ番号
        public short NNo;                   // 待機・実行Ｎ番号
        public short LineNo;                // 待機・実行行番号
        public short LineFlg;               // 待機・実行行番号種別
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public sbyte[] Reserved;            // 未使用
        public static TASKSTATUS Init()
        {
            TASKSTATUS tmp = new TASKSTATUS();
            tmp.Reserved = new sbyte[6];
            return tmp;
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct ECNCSTATUS                // ECNCステータス情報構造体
    {
        public short Status2;           // 各種状態情報２
        public short Status3;           // 各種状態情報３
        public short Alarm2;                // アラーム情報２
        public short Alarm3;                // アラーム情報３
        public short Alarm4;                // アラーム情報４
        public short Alarm5;                // アラーム情報５
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public sbyte[] Reserved0;           // 予約
        public int WTopPos;         // Ｗ軸上限値
        public int CorrAng;         // 補正角度(8-24ﾋﾞｯﾄ固定小数点)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public sbyte[] Reserved1;           // 予約
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        public short[] ADVal;               // アナログ入力値
        public short AtDischg;          /* 放電加工中					*/
        public int PrcsTargetDist;      /* 放電加工目標加工量			*/
        public int PrcsNowDist;     /* 放電加工現在加工量			*/
        public short ExtErrSts;         /* 外部エラー状態				*/
        public short ExtErrLat;         /* 外部エラーラッチ状態			*/
        public static ECNCSTATUS Init()
        {
            ECNCSTATUS tmp = new ECNCSTATUS();
            tmp.Reserved0 = new sbyte[4];
            tmp.Reserved1 = new sbyte[8];
            tmp.ADVal = new short[5];
            return tmp;
        }
    }


    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct STATUS                    // アンサーステータス情報構造体
    {
        public MCSTATUS mc;                 // 全体情報
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public AXSTATUS[] ax;               // 軸情報
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public TASKSTATUS[] task;           // タスク情報
        public ECNCSTATUS ecnc;             // ECNC情報

        public static STATUS Init()
        {
            STATUS tmp = new STATUS();
            tmp.mc = Rt64ecdata.MCSTATUS.Init();
            tmp.ax = new AXSTATUS[64];
            for (int cnt = tmp.ax.GetLowerBound(0); cnt <= tmp.ax.GetUpperBound(0); cnt++)
                tmp.ax[cnt] = Rt64ecdata.AXSTATUS.Init();
            tmp.task = new TASKSTATUS[8];
            for (int cnt = tmp.task.GetLowerBound(0); cnt <= tmp.task.GetUpperBound(0); cnt++)
                tmp.task[cnt] = Rt64ecdata.TASKSTATUS.Init();
            tmp.ecnc = Rt64ecdata.ECNCSTATUS.Init();
            return tmp;
        }
    }

    // for STATUS.mc.Status
    public const int S_MCS_ALM = 0x00000001;    // アラーム発生中
    public const int S_MCS_SETTING = 0x00000002;    // 全タスクセッティングモード
    public const int S_MCS_SENSE = 0x00000010;  // 高速センサーラッチ完
                                                //public const int						= 0x00000020;	//
    public const int S_MCS_PRGCHG = 0x00000040; // 動作プログラムデータ変更
    public const int S_MCS_RTCWARN = 0x00000080;    // 制御周期負荷ワーニング(87.5%)
    public const int S_MCS_RTCFAIL = 0x00000100;    // 制御周期負荷過大
    public const int S_MCS_FDTWARN = 0x00000200;    // FDT読込ワーニング
    public const int S_MCS_CYCLETIME_EN = 0x00000400;   // サイクルタイム有効

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
    public const int S_TKA_ECNC = 0x00000200;   // ECNCアラーム

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
    public const short S_MCA2_EX1E602ALM = 0x0001;  // ECNC専用IFボード(EX1-E602)アラーム
    public const short S_MCA2_EXTALM = 0x0002;  // ECNC専用外部アラーム
    public const short S_MCA2_EXTALMFACT = 0x0004;  // ECNC専用外部アラーム要因あり
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

    // ------------------------------------------------------------------------
    //	EtherCATステータス情報構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct ECT_WHOLESTATUS
    {
        public short mst_init_errc;     // EtherCATマスター初期化エラーコード
        public short mst_ESM;           // EtherCAT ESM(EtherCAT State Machine)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Alignment;        // 
        public int mst_NotifiCode;      // EtherCATマスター通知コード
        public int mst_whcom_errc;      // EtherCATマスター全体エラーコード
        public long mst_axcom_err;      // EtherCATマスターエラー発生軸フラグ
        public long no_rcv_err;         // データ受信エラー発生軸フラグ
        public long time_out_err;       // タイムアウトエラー発生軸フラグ
        public long watchdog_err;       // WDTエラー発生軸フラグ
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] Reserved;         // 予約
        public static ECT_WHOLESTATUS Init()
        {
            ECT_WHOLESTATUS tmp = new ECT_WHOLESTATUS();
            tmp.Alignment = new byte[4];
            tmp.Reserved = new byte[16];
            return tmp;
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct ECT_AXSTATUS
    {
        public int mst_axNotifiCode;    // EtherCATマスター通知コード
        public int mst_axcom_errc;      // EtherCATマスター通知エラーコード
        public short ControlWord;       // ControlWord(6040h)
        public short StatusWord;        // StatusWord(6041h)
        public byte ModesOfOpe;         // ModesOfOperation(6060h)
        public byte ModesOfOpeDisp;     // ModesOfOperationDisplay(6061h)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] Alignment;        // 
        public short TouchProbeFunc;    // TouchProbeFunction(60B8h)
        public short TouchProbeStat;    // TouchProbeStatus(60B9h)
        public int DigitalInputs;       // Digital inputs(60FDh)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] Reserved;         // 予約
        public static ECT_AXSTATUS Init()
        {
            ECT_AXSTATUS tmp = new ECT_AXSTATUS();
            tmp.Alignment = new byte[2];
            tmp.Reserved = new byte[8];
            return tmp;
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct ECT_INFOAX
    {
        public ECT_WHOLESTATUS WholeStat;
        public ECT_AXSTATUS AxStat;
        public static ECT_INFOAX Init()
        {
            ECT_INFOAX tmp = new ECT_INFOAX();
            tmp.WholeStat = Rt64ecdata.ECT_WHOLESTATUS.Init();
            tmp.AxStat = Rt64ecdata.ECT_AXSTATUS.Init();
            return tmp;
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct ECT_INFOALL
    {
        public ECT_WHOLESTATUS WholeStat;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public ECT_AXSTATUS[] AxStat;
        public static ECT_INFOALL Init()
        {
            ECT_INFOALL tmp = new ECT_INFOALL();
            tmp.WholeStat = Rt64ecdata.ECT_WHOLESTATUS.Init();
            tmp.AxStat = new ECT_AXSTATUS[9];
            for (int cnt = tmp.AxStat.GetLowerBound(0); cnt <= tmp.AxStat.GetUpperBound(0); cnt++)
                tmp.AxStat[cnt] = Rt64ecdata.ECT_AXSTATUS.Init();
            return tmp;
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct ECT_INFO64ALL
    {
        public ECT_WHOLESTATUS WholeStat;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public ECT_AXSTATUS[] AxStat;
        public static ECT_INFO64ALL Init()
        {
            ECT_INFO64ALL tmp = new ECT_INFO64ALL();
            tmp.WholeStat = Rt64ecdata.ECT_WHOLESTATUS.Init();
            tmp.AxStat = new ECT_AXSTATUS[64];
            for (int cnt = tmp.AxStat.GetLowerBound(0); cnt <= tmp.AxStat.GetUpperBound(0); cnt++)
                tmp.AxStat[cnt] = Rt64ecdata.ECT_AXSTATUS.Init();
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	EtherCAT受信データ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct ECT_MON
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public byte[] dat;
        public static ECT_MON Init()
        {
            ECT_MON tmp = new ECT_MON();
            tmp.dat = new byte[64];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	汎用入出力情報構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct IODATA
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 116)]
        public ushort[] InputData;      // 汎用入力
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public ushort[] OutputData;     // 汎用出力
        public static IODATA Init()
        {
            IODATA tmp = new IODATA();
            tmp.InputData = new ushort[116];
            tmp.OutputData = new ushort[64];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	ＤＮＣバッファ情報構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct DNCBUFI
    {
        public int size;                    // バッファ使用容量（バイト）
        public int Free;                    // バッファ空き容量（バイト）
        public static DNCBUFI Init()
        {
            return (new DNCBUFI());
        }
    }

    // ------------------------------------------------------------------------
    //	センサーラッチ位置情報構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct SENSEPOS
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] SenPos;            // ｾﾝｻｰﾗｯﾁﾎﾟｼﾞｼｮﾝ（論理座標系）
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] SenPosA;           // ｾﾝｻｰﾗｯﾁﾎﾟｼﾞｼｮﾝ（ｱﾌﾞｿ座標系）
        public static SENSEPOS Init()
        {
            SENSEPOS tmp = new SENSEPOS();
            tmp.SenPos = new int[9];
            tmp.SenPosA = new int[9];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	プログラムボリュームラベル構造体（１０４ﾊﾞｲﾄ固定長）
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct BINPRG_LABEL
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] Reserved1;        // 予約
        public short BlockNumber;       // 有効ブロック長
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 22)]
        public byte[] Reserved2;        // 予約
        public short ProgramType;       // ﾌﾟﾛｸﾞﾗﾑｺｰﾄﾞﾀｲﾌﾟ(0:T､ 1:G)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 76)]
        public byte[] Reserved3;        // 予約
        public static BINPRG_LABEL Init()
        {
            BINPRG_LABEL tmp = new BINPRG_LABEL();
            tmp.Reserved1 = new byte[2];
            tmp.Reserved2 = new byte[22];
            tmp.Reserved3 = new byte[76];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	プログラム１ブロックデータ構造体（１０４ﾊﾞｲﾄ固定長）
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct BINPRG_BLOCK
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 101)]
        public byte[] Reserved;         // 予約
        public byte PrgType;            // ﾌﾟﾛｸﾞﾗﾑｺｰﾄﾞﾀｲﾌﾟ(0:T､ 1:G)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] Reserved2;        // 未使用
        public static BINPRG_BLOCK Init()
        {
            BINPRG_BLOCK tmp = new BINPRG_BLOCK();
            tmp.Reserved = new byte[101];
            tmp.Reserved2 = new byte[2];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	Ａ／Ｄ＆ＰＯＳ情報構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct AD_POS
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public int[] Ad;                // Ａ／Ｄ値
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] Pos;               // 論理座標系機械位置
        public static AD_POS Init()
        {
            AD_POS tmp = new AD_POS();
            tmp.Ad = new int[4];
            tmp.Pos = new int[9];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	ＴＰＣロギング情報構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct TPCINFO
    {
        public short Log;           // ＴＰＣﾃﾞｰﾀﾛｷﾞﾝｸﾞﾌﾗｸﾞ
        public short Num;           // ＴＰＣﾃﾞｰﾀﾛｷﾞﾝｸﾞﾎﾟｲﾝﾄ数
        public static TPCINFO Init()
        {
            return (new TPCINFO());
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct TPCINFOEX
    {
        public short Log;           // ロギング中フラグﾞ
        public short Reserved;      // 予約
        public int Size;            // バッファ使用容量（バイト）
        public int Free;            // バッファ空き容量（バイト）
        public static TPCINFOEX Init()
        {
            return (new TPCINFOEX());
        }
    }

    // ------------------------------------------------------------------------
    //	ＴＰＣデータ構造体
    // ------------------------------------------------------------------------
    //----------
    // TPCH_LOG_POS用
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct TPCDAT_POS
    {
        public int pr1;     // 第１軸機械位置
        public ushort hi1;      // 汎用入力１状態
        public ushort ho1;      // 汎用出力１状態
        public int pr2;     // 第２軸機械位置
        public ushort hi2;      // 汎用入力２状態
        public ushort ho2;      // 汎用出力２状態
        public static TPCDAT_POS Init()
        {
            return (new TPCDAT_POS());
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct TPC           // TPCDAT_POSと同じ型
    {
        public int pr1;     // 第１軸機械位置
        public short hi1;       // 汎用入力１状態
        public short ho1;       // 汎用出力１状態
        public int pr2;     // 第２軸機械位置
        public short hi2;       // 汎用入力２状態
        public short ho2;       // 汎用出力２状態
        public static TPC Init()
        {
            return (new TPC());
        }
    }

    //----------
    // TPCH_LOG_ECT用
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct TPCDAT_ECT_16BYTE
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] dt;
        public static TPCDAT_ECT_16BYTE Init()
        {
            TPCDAT_ECT_16BYTE tmp = new TPCDAT_ECT_16BYTE();
            tmp.dt = new byte[16];
            return tmp;
        }
    }
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct TPCDAT_ECT_32BYTE
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] dt;
        public static TPCDAT_ECT_32BYTE Init()
        {
            TPCDAT_ECT_32BYTE tmp = new TPCDAT_ECT_32BYTE();
            tmp.dt = new byte[32];
            return tmp;
        }
    }
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct TPCDAT_ECT_48BYTE
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 48)]
        public byte[] dt;
        public static TPCDAT_ECT_48BYTE Init()
        {
            TPCDAT_ECT_48BYTE tmp = new TPCDAT_ECT_48BYTE();
            tmp.dt = new byte[48];
            return tmp;
        }
    }
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct TPCDAT_ECT_64BYTE
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public byte[] dt;
        public static TPCDAT_ECT_64BYTE Init()
        {
            TPCDAT_ECT_64BYTE tmp = new TPCDAT_ECT_64BYTE();
            tmp.dt = new byte[64];
            return tmp;
        }
    }


    // ------------------------------------------------------------------------
    //	ＶＥＲ／ＰＥＲ情報構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct VPER
    {
        public short ver; //
        public short vmp; //
        public short vmm; //
        public short per; //
        public short pmp; //
        public short pmm; //
        public static VPER Init()
        {
            return (new VPER());
        }
    }

    // ------------------------------------------------------------------------
    //	ピッチエラー補正用パラメータ情報構造体（６７８４０バイト固定長）
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct REV_AX // 各軸補正用パラメータ
    {
        public int RevMagnify;  // 補正倍率
        public int RevSpace;    // 補正間隔
        public int RevTopNo;    // 補正データ先頭番号
        public int RevMCnt;     // －側補正区間数
        public int RevPCnt;     // ＋側補正区間数
        public static REV_AX Init()
        {
            return (new REV_AX());
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct PITCH_ERR_REV
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public REV_AX[] RevAx;  // 各軸補正用パラメータ
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 33280)]
        public short[] RevDt;   // 補正データ
        public static PITCH_ERR_REV Init()
        {
            PITCH_ERR_REV tmp = new PITCH_ERR_REV();
            tmp.RevAx = new REV_AX[64];
            for (int cnt = tmp.RevAx.GetLowerBound(0); cnt <= tmp.RevAx.GetUpperBound(0); cnt++)
                tmp.RevAx[cnt] = Rt64ecdata.REV_AX.Init();
            tmp.RevDt = new short[33280];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	各軸ピッチエラー補正用パラメータ情報構造体（６００１６バイト固定長）
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct PITCH_ERR_REV_AX
    {
        public int RevMagnify;  // 補正倍率
        public int RevSpace;    // 補正間隔
        public int RevMCnt;     // －側補正区間数
        public int RevPCnt;     // ＋側補正区間数
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 30000)]
        public short[] RevDt;   // 補正データ
        public static PITCH_ERR_REV_AX Init()
        {
            PITCH_ERR_REV_AX tmp = new PITCH_ERR_REV_AX();
            tmp.RevDt = new short[30000];
            return tmp;
        }
    }


    // ------------------------------------------------------------------------
    //	ツール長補正用パラメータ情報構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct TOOL_H
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public int[] h;
        public static TOOL_H Init()
        {
            TOOL_H tmp = new TOOL_H();
            tmp.h = new int[20];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	マクロ変数データ
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct VARIABLE
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
        public double[] Var;
        public static VARIABLE Init()
        {
            VARIABLE tmp = new VARIABLE();
            tmp.Var = new double[100];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	拡張マクロ変数データ
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct EXTVARIABLE
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 90000)]
        public double[] Var;
        public static EXTVARIABLE Init()
        {
            EXTVARIABLE tmp = new EXTVARIABLE();
            tmp.Var = new double[90000];
            return tmp;
        }
    }
    // ------------------------------------------------------------------------
    //	拡張マクロ変数データ
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct EXTONEVARIABLE
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public double[] Var;
        public static EXTONEVARIABLE Init()
        {
            EXTONEVARIABLE tmp = new EXTONEVARIABLE();
            tmp.Var = new double[1];
            return tmp;
        }
    }

    // グローバルマクロ変数情報						：#1000～#1099(QWORD(double)アクセス領域)
    public const int VARQ_GLB_STR = 1000;               // 先頭アドレス
    public const int VARQ_GLB_NUM = 100;                // データ数

    // ラダー共有マクロ変数(RTMC64 -> PLC)情報		：#2000～#2499(QWORD(double)アクセス領域)
    public const int VARQ_LDW_STR = 2000;               // 先頭アドレス
    public const int VARQ_LDW_NUM = 500;                // データ数

    // ラダー共有マクロ変数(RTMC64 <- PLC)情報		：#2500～#2999(QWORD(double)アクセス領域)
    public const int VARQ_LDR_STR = 2500;               // 先頭アドレス
    public const int VARQ_LDR_NUM = 500;                // データ数

    // 拡張グローバルマクロ変数情報					：#10000～#99999(QWORD(double)アクセス領域)
    public const int EXTVARQ_GLB_STR = 10000;           // 先頭アドレス
    public const int EXTVARQ_GLB_NUM = 90000;           // データ数


    // ------------------------------------------------------------------------
    //	一般マクロ変数任意数書込/読出パラメータ予約構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct NVARIABLE_PRE
    {
        public int Start;                   // 送受信開始マクロ変数番号
        public int Num;                 // 送受信マクロ変数数
        public static NVARIABLE_PRE Init()
        {
            return (new NVARIABLE_PRE());
        }
    }

    // ------------------------------------------------------------------------
    //	マクロ変数書込/読出要求データ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct VARIABLE_REQ
    {
        public int Start;               // 送受信開始マクロ変数番号
        public int Num;             // 送受信マクロ変数数
        public short Dir;               // 転送方向
                                        //   0:RTMC64-EC→Windows
                                        //   1:Windows→RTMC64-EC
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Reserved;         // 予約
        public static VARIABLE_REQ Init()
        {
            VARIABLE_REQ tmp = new VARIABLE_REQ();
            tmp.Reserved = new byte[6];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	補間前加減速パラメータ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct ACO_PRM
    {
        public int aco_acot;                    // 補間前加減速時定指数
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
        public int[] aco_accdat;     // 補間前加減速最小ｵｰﾊﾞﾗｲﾄﾞ切換加速度
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
        public short[] aco_ovrdat;  // 補間前加減速最小ｵｰﾊﾞﾗｲﾄﾞﾃﾞｰﾀ
        public short Reserved;   // 未使用
        public static ACO_PRM Init()
        {
            ACO_PRM tmp = new ACO_PRM();
            tmp.aco_accdat = new int[7];
            tmp.aco_ovrdat = new short[7];
            return tmp;
        }
    }


    // for ACO_PRM.aco_acot
    public const int ACO_TMMIN = 0;         // 補間前加減速時定数最小値
    public const int ACO_TMMAX = 16383;     // 補間前加減速時定数最大値

    // for ACO_PRM.aco_accdat[]
    public const int ACO_ACCMIN = 0;        // 補間前加減速切換加速度最小値
                                            //	public const int ACO_ACCMAX = 8000000;	// 補間前加減速切換加速度最大値
    public const int ACO_ACCMAX = (1000000000 * 2);  // 補間前加減速切換加速度最大値

    // for ACO_PRM.aco_ovrdat[]
    public const short ACO_OVRMIN = 1;      // 補間前加減速ｵｰﾊﾞﾗｲﾄﾞ最小値
    public const short ACO_OVRMAX = 100;    // 補間前加減速ｵｰﾊﾞﾗｲﾄﾞ最大値

    // ------------------------------------------------------------------------
    //	ティーチング情報用パラメータ情報構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct TEACHSTS
    {
        public int Status;  // ティーチングステータス
        public int StepNo;  // 実行ステップ番号
        public int TchStepNo; // ティーチングステップ番号
        public int Reserved;  // 未使用
        public static TEACHSTS Init()
        {
            return (new TEACHSTS());
        }
    }

    // for TEACHSTS.Status
    public const int T_TEACH = 0x1; // ティーチングモード
    public const int T_TEACHSTP = 0x2;  // ティーチング開始ステップ
    public const int T_TEACHEN = 0x4;   // ティーチング可能ステップ
    public const int T_TEACHSTPPRV = 0x8;   // ﾃｨｰﾁﾝｸﾞ開始ｽﾃｯﾌﾟの前ｽﾃｯﾌﾟ

    // ------------------------------------------------------------------------
    //	手パ操作情報構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct HANDLESTS
    {
        public short handle_mode;   // 手パ有効フラグ
        public short kp;            // 手パ設定倍率
        public short ax1;           // 手パ第１軸
        public short ax2;           // 手パ第２軸
        public static HANDLESTS Init()
        {
            return (new HANDLESTS());
        }
    }

    // for HANDLESTS.handle_mode
    public const short HDL_MD_HANDLE = 1;       // 手パモード
    public const short HDL_MD_JOYSTICK = 2;         // ジョイスティックモード
    public const short HDL_MD_ENABLE = 0x1000;  // 手パ/ジョイスティック有効

    // ------------------------------------------------------------------------
    //		ＲＯＭソフトバージョンデータ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct ROMVERSION
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] Version;  // バージョン文字列
        public short EvenSum;   // SUM:even (rom)
        public short OddSum;    // SUM:odd  (rom)
        public short FlashSum;  // SUM:ＳＨ内部ＦＬＡＳＨ
        public short FlashFlg;  // ＳＨ内部ＦＬＡＳＨ使用フラグ
        public short KindID;    // 機種ＩＤ
        public int SerialID;    // シリアルＩＤ
        public int ProductID;   // プロダクトＩＤ
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 28)]
        public byte[] Reserved;
        public static ROMVERSION Init()
        {
            ROMVERSION tmp = new ROMVERSION();
            tmp.Version = new byte[16];
            tmp.Reserved = new byte[28];
            return tmp;
        }
    }

    // for ROMVERSION.KindID
    public const short SID_BKIND = 30;      // シリアルナンバー（機種ＩＤ）
    public const string SID_BKIND_STR = "RT64M3";   // シリアルナンバー（機種ＩＤ）

    // ------------------------------------------------------------------------
    //	ＲＴＣ処理時間格納構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct RTCTIME
    {
        public int RtcMax;      // 最大処理時間[us]
        public int RtcNow;      // 現在処理時間[us]
        public static RTCTIME Init()
        {
            return (new RTCTIME());
        }
    }

    // ------------------------------------------------------------------------
    //	通信周期設定値構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct CYCLETIME
    {
        public int CycleTime;   // 制御周期[us]
        public static CYCLETIME Init()
        {
            return (new CYCLETIME());
        }
    }

    //	public const double TIMERCLOCK = 2.609375;  // 処理時間ｶｳﾝﾄｸﾛｯｸ(MHx)
    public const double TIMERCLOCK = 1;     // 処理時間ｶｳﾝﾄｸﾛｯｸINtimeの場合、us単位で情報取得(MHx)/

    // ------------------------------------------------------------------------
    //	フォアグランド処理時間格納構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct FORTIME
    {
        public int ForMax;      // 最大処理時間[us]
        public int ForNow;      // 現在処理時間[us]
        public static FORTIME Init()
        {
            return (new FORTIME());
        }
    }

    // ------------------------------------------------------------------------
    //	ツール径補正用パラメータ情報構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct TOOL_D    // （８０バイト）
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public int[] d;     // ツール径補正データ
        public static TOOL_D Init()
        {
            TOOL_D tmp = new TOOL_D();
            tmp.d = new int[20];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	プログラムブロック情報構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct PRGBLK_INF_SUB
    {
        public short PrgNo;     // プログラム番号
        public short TaskNo;    // タスク番号
        public static PRGBLK_INF_SUB Init()
        {
            return (new PRGBLK_INF_SUB());
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct PRGBLK_INF    // （２５６バイト）
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public PRGBLK_INF_SUB[] inf;
        public static PRGBLK_INF Init()
        {
            PRGBLK_INF tmp = new PRGBLK_INF();
            tmp.inf = new PRGBLK_INF_SUB[64];
            for (int cnt = tmp.inf.GetLowerBound(0); cnt <= tmp.inf.GetUpperBound(0); cnt++)
                tmp.inf[cnt] = Rt64ecdata.PRGBLK_INF_SUB.Init();
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	フィードバックカウンタデータ構造体（８ﾊﾞｲﾄ固定長）
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct FBCOUNT
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public int[] cntr;      // ＦＢ１・ＦＢ２積算値
        public static FBCOUNT Init()
        {
            FBCOUNT tmp = new FBCOUNT();
            tmp.cntr = new int[2];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	マクロ変数データ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct MCRREG
    {
        public double Val;                  // マクロ変数値
        public static MCRREG Init()
        {
            return (new MCRREG());
        }
    }
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct ONEVARIABLE
    {
        public double var;                  // マクロ変数値
        public static ONEVARIABLE Init()
        {
            return (new ONEVARIABLE());
        }
    }

    // ------------------------------------------------------------------------
    //	工具長補正情報構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct TOOLHSTS
    {
        public short toolh_en;      // 補正有効フラグ
        public short toolh_no;      // 選択中補正No
        public static TOOLHSTS Init()
        {
            return (new TOOLHSTS());
        }
    }

    // ------------------------------------------------------------------------
    //	工具径補正情報構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct TOOLDSTS
    {
        public short toold_en;      // 補正有効フラグ
        public short toold_no;      // 選択中補正No
        public static TOOLDSTS Init()
        {
            return (new TOOLDSTS());
        }
    }

    // ------------------------------------------------------------------------
    //	工具径補正エラー情報構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct TOOLDERR
    {
        public int StepNo;          // エラー発生ステップ番号
        public int ErrCode;         // エラーコード
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] Reserved;
        public static TOOLDERR Init()
        {
            TOOLDERR tmp = new TOOLDERR();
            tmp.Reserved = new byte[8];
            return tmp;
        }
    }

    // for TOOLDERR.ErrCode
    public const ushort TDERR_STATE_ANOMALY = (1 << 0); // 正体不明エラー（内部状態異常）
    public const ushort TDERR_DCHG_CIR = (1 << 1);  // 円弧指令での径補正モード変更エラー
    public const ushort TDERR_INHB_COMMAND = (1 << 2);  // 径補正中の禁止命令指定エラー
    public const ushort TDERR_MACRO = (1 << 3); // マクロ変数エラー
    public const ushort TDERR_D_RANGE = (1 << 4);   // 径指定範囲外エラー
    public const ushort TDERR_LIN_NOMOVE = (1 << 5);    // 移動量なしエラー
    public const ushort TDERR_LIN_SE_SAME = (1 << 6);   // 始点/終点一致エラー
    public const ushort TDERR_CIR_CENT_IL = (1 << 7);   // 円弧中心点演算エラー
    public const ushort TDERR_LINLIN_NOCP = (1 << 8);   // 交点なしエラー (LIN → LIN)
    public const ushort TDERR_LINCIR_NOCP = (1 << 9);   // 交点なしエラー (LIN → CIR、CIR → LIN)
    public const ushort TDERR_CIRCIR_NOCP = (1 << 10);  // 交点なしエラー (CIR → CIR)
    public const ushort TDERR_LIN_REVERSE = (1 << 11);  // 移動方向反転エラー（LIN）
    public const ushort TDERR_CIR_REVERSE = (1 << 12);  // 移動方向反転エラー（CIR）


    // ------------------------------------------------------------------------
    //	位置決めポイントテーブルデータ構造体（３６ﾊﾞｲﾄ固定長）
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct AXIS_POINT    // ポイントデータ構造体（３６ﾊﾞｲﾄ固定長）
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] AxisPos;   // 各軸アブソ位置
        public static AXIS_POINT Init()
        {
            AXIS_POINT tmp = new AXIS_POINT();
            tmp.AxisPos = new int[9];
            return tmp;
        }
    }

    public const short POINTTBLMAX = 400;   // 位置決めポイントテーブル最大数

    // ------------------------------------------------------------------------
    //	軸割り当て情報構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct ASS_AXIS
    {
        public sbyte tsk;
        public sbyte axn;
        public static ASS_AXIS Init()
        {
            return (new ASS_AXIS());
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct ASSAXISTBL
    {
        public long axis_en;    // 有効軸フラグ
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public ASS_AXIS[] ass_axis;
        public static ASSAXISTBL Init()
        {
            ASSAXISTBL tmp = new ASSAXISTBL();
            tmp.ass_axis = new ASS_AXIS[64];
            for (int cnt = tmp.ass_axis.GetLowerBound(0); cnt <= tmp.ass_axis.GetUpperBound(0); cnt++)
                tmp.ass_axis[cnt] = Rt64ecdata.ASS_AXIS.Init();
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	動作モードデータ変更コマンドパラメータ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct MODECHG
    {
        public short mode;      // 動作モード
        public static MODECHG Init()
        {
            return (new MODECHG());
        }
    }

    // ------------------------------------------------------------------------
    //	ＪＯＧ移動開始コマンドパラメータ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct JOGSTART
    {
        public short AxisFlag;  // 移動軸選択フラグ
        public short JogVect;   // 軸移動方向フラグ
        public static JOGSTART Init()
        {
            return (new JOGSTART());
        }
    }

    // ------------------------------------------------------------------------
    //	原点復帰移動開始コマンドパラメータ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct ZRNSTART
    {
        public short AxisFlag;  // 移動軸選択フラグ
        public static ZRNSTART Init()
        {
            return (new ZRNSTART());
        }
    }

    // ------------------------------------------------------------------------
    //	バックアップメモリ初期化コマンドパラメータ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct GENERATION
    {
        public short InitCmnd;  // 初期化データ選択フラグ
        public static GENERATION Init()
        {
            return (new GENERATION());
        }
    }

    // for GENERATION.InitCmnd
    public const short GEN_PARAM = 0x1; // パラメータ初期化指定
    public const short GEN_PROGRAM = 0x2;   // 動作プログラム初期化指定
    public const short GEN_POSITION = 0x4;  // アブソ座標初期化指定
    public const short GEN_VARIABLE = 0x8;  // マクロ変数初期化指定

    // ------------------------------------------------------------------------
    //	ＰＴＰ移動開始コマンドパラメータ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct PTPSTART
    {
        public int AxisFlag;    // 移動軸選択フラグ
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] IncAxis;   // 各軸ｲﾝｸﾘﾒﾝﾄ移動量
        public static PTPSTART Init()
        {
            PTPSTART tmp = new PTPSTART();
            tmp.IncAxis = new int[9];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	ＰＴＰ移動開始コマンドパラメータ構造体（ＡＢＳＯ）
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct PTPASTART
    {
        public int AxisFlag;    // 移動軸選択フラグ
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] PosAxis;   // 各軸位置決めﾎﾟｼﾞｼｮﾝ
        public static PTPASTART Init()
        {
            PTPASTART tmp = new PTPASTART();
            tmp.PosAxis = new int[9];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	補間移動開始コマンドパラメータ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct LINSTART
    {
        public int AxisFlag;    // 移動軸選択フラグ
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] IncAxis;   // 各軸ｲﾝｸﾘﾒﾝﾄ移動量
        public int Feed;        // 補間送り速度
        public static LINSTART Init()
        {
            LINSTART tmp = new LINSTART();
            tmp.IncAxis = new int[9];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	高速センサーラッチ補間移動開始コマンドパラメータ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct SLINSTART
    {
        public int AxisFlag;    // 移動軸選択フラグ
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] IncAxis;   // 各軸ｲﾝｸﾘﾒﾝﾄ移動量
        public int Feed;        // 補間送り速度
        public short NoSkipF;   // スキップ抑制フラグ
        public short Reserve;   // ﾃﾞｰﾀをﾊﾟｯｸするためのﾀﾞﾐｰ
        public static SLINSTART Init()
        {
            SLINSTART tmp = new SLINSTART();
            tmp.IncAxis = new int[9];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	補間移動開始コマンドパラメータ構造体（ＡＢＳＯ）
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct LINASTART
    {
        public int AxisFlag;    // 移動軸選択フラグ
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] PosAxis;   // 各軸位置決めﾎﾟｼﾞｼｮﾝ
        public int Feed;        // 補間送り速度
        public static LINASTART Init()
        {
            LINASTART tmp = new LINASTART();
            tmp.PosAxis = new int[9];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	高速センサーラッチ補間移動開始コマンドパラメータ構造体（ＡＢＳＯ）
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct SLINASTART
    {
        public int AxisFlag;    // 移動軸選択フラグ
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] PosAxis;   // 各軸位置決めﾎﾟｼﾞｼｮﾝ
        public int Feed;        // 補間送り速度
        public short NoSkipF;   // スキップ抑制フラグ
        public int Reserve;     // ﾃﾞｰﾀをﾊﾟｯｸするためのﾀﾞﾐｰ
        public static SLINASTART Init()
        {
            SLINASTART tmp = new SLINASTART();
            tmp.PosAxis = new int[9];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	汎用出力直接制御コマンドパラメータ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct OUTPUTPAT
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public ushort[] OutputPat;  // 汎用出力1～3
        public static OUTPUTPAT Init()
        {
            OUTPUTPAT tmp = new OUTPUTPAT();
            tmp.OutputPat = new ushort[12];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	プログラム選択コマンドパラメータ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct PROGSEL
    {
        public short ProgSel;   // 選択プログラム番号
        public static PROGSEL Init()
        {
            return (new PROGSEL());
        }
    }

    // ------------------------------------------------------------------------
    //	軸速度オーバーライド変更コマンドパラメータ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct OVERCHG
    {
        public short Override;  // オーバーライド設定値
        public static OVERCHG Init()
        {
            return (new OVERCHG());
        }
    }

    // ------------------------------------------------------------------------
    //	主軸ＯＮ／ＯＦＦコマンドパラメータ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct SPCMND
    {
        public short SpOut;     // 主軸指令フラグ
        public static SPCMND Init()
        {
            return (new SPCMND());
        }
    }

    // ------------------------------------------------------------------------
    //	主軸回転数変更コマンドパラメータ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct SPREVSET
    {
        public int SpRevo;      // 主軸回転数
        public static SPREVSET Init()
        {
            return (new SPREVSET());
        }
    }

    // ------------------------------------------------------------------------
    //	主軸回転数情報構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct SPREVDAT
    {
        public int ComSpRevo;   // 主軸指令回転数
        public int ActSpRevo;   // 主軸実回転数
        public static SPREVDAT Init()
        {
            return (new SPREVDAT());
        }
    }

    // ------------------------------------------------------------------------
    //	回転軸回転動作コマンドパラメータ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct SPINAX
    {
        public short OverFlag;  // オーバーライドフラグ
        public short AxisFlag;  // 移動軸選択フラグ
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] RevAx;     // 各軸回転数
        public static SPINAX Init()
        {
            SPINAX tmp = new SPINAX();
            tmp.RevAx = new int[9];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	汎用入力／出力強制制御コマンドパラメータサブ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct IO_CMD
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] IoBit;    // 汎用入出力D0~15ﾋﾞｯﾄ変更ｺﾏﾝﾄﾞ
        public static IO_CMD Init()
        {
            IO_CMD tmp = new IO_CMD();
            tmp.IoBit = new byte[16];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	汎用入力強制制御コマンドパラメータ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct COMPINPUT
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 116)]
        public IO_CMD[] InCmd;
        public static COMPINPUT Init()
        {
            COMPINPUT tmp = new COMPINPUT();
            tmp.InCmd = new IO_CMD[116];
            for (int cnt = tmp.InCmd.GetLowerBound(0); cnt <= tmp.InCmd.GetUpperBound(0); cnt++)
                tmp.InCmd[cnt] = Rt64ecdata.IO_CMD.Init();
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	汎用出力強制制御コマンドパラメータ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct COMPOUTPUT
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public IO_CMD[] OutCmd;
        public static COMPOUTPUT Init()
        {
            COMPOUTPUT tmp = new COMPOUTPUT();
            tmp.OutCmd = new IO_CMD[64];
            for (int cnt = tmp.OutCmd.GetLowerBound(0); cnt <= tmp.OutCmd.GetUpperBound(0); cnt++)
                tmp.OutCmd[cnt] = Rt64ecdata.IO_CMD.Init();
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	汎用入出力強制制御コマンドパラメータ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct COMPIOBIT
    {
        public short Pno;   // 入出力ポート番号
        public ushort Bno;  // 制御ビット番号
        public short flg;   // 制御フラグ
        public static COMPIOBIT Init()
        {
            return (new COMPIOBIT());
        }
    }

    // for COMPIOBIT.Pno
    public const short INPORT = 0x0;            // 入力ポート指定
    public const short OUTPORT = -1 * 0x8000;   // 出力ポート指定

    // for COMPIOBIT.Flg
    public const short IONOTCARE = 0;           // 状態変更無し
    public const short IORELEASE = 1;           // 強制ＯＮ／ＯＦＦ終了
    public const short IOSET = 2;           // 強制ＯＮ
    public const short IORESET = 3;         // 強制ＯＦＦ

    // ------------------------------------------------------------------------
    //		Ｚ軸接線制御ON/OFFコマンドパラメータ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct TLINESW
    {
        public short tlinesw;   // Ｚ軸接線制御機能ｽｲｯﾁ
        public static TLINESW Init()
        {
            return (new TLINESW());
        }
    }

    // ------------------------------------------------------------------------
    //		ティーチングステップ変更コマンドパラメータ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct STEPCHG
    {
        public short StepNo;
        public static STEPCHG Init()
        {
            return (new STEPCHG());
        }
    }

    // ------------------------------------------------------------------------
    //		ＡＤサンプリングデータロギングコマンドパラメータ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct ADSC
    {
        public short sc;
        public static ADSC Init()
        {
            return (new ADSC());
        }
    }

    // ------------------------------------------------------------------------
    //		ＴＰＣデータ選択コマンドパラメータ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct TPCSEL
    {
        public short ax1;
        public short hi1;
        public short ho1;
        public short ax2;
        public short hi2;
        public short ho2;
        public static TPCSEL Init()
        {
            return (new TPCSEL());
        }
    }

    // ------------------------------------------------------------------------
    //		ＴＰＣデータ選択コマンドパラメータ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct TPCSEL_HEADER // ヘッダ情報
    {
        public short LogSel;    // ロギング種別選択
        public short TrigSel;   // トリガ選択
        public short Interval;  // ロギング周期[msec]
        public short Reserved;  // 予約
        public static TPCSEL_HEADER Init()
        {
            return (new TPCSEL_HEADER());
        }
    }

    // for TPCSEL_HEADER.LogSel (ロギング種別)
    public const short TPCH_LOG_POS = 0;        // ポジション・入出力
    public const short TPCH_LOG_ECT = 1;        // EtherCAT PDO
    public const short TPCH_LOG_64CH = 2;       // 64CH


    //----------
    // ポジション・入出力ロギング要求（旧ＴＰＣデータ）
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct TPCSEL_POS    // ポジション・入出力ロギング要求（旧ＴＰＣデータ）
    {
        public TPCSEL_HEADER h;
        public TPCSEL sel;
        public static TPCSEL_POS Init()
        {
            TPCSEL_POS tmp = new TPCSEL_POS();
            tmp.h = Rt64ecdata.TPCSEL_HEADER.Init();
            tmp.sel = Rt64ecdata.TPCSEL.Init();
            return tmp;
        }
    }


    //----------
    // EtherCAT通信ロギング要求
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct TPCSEL_ECT        // ヘッダ情報
    {
        public TPCSEL_HEADER h;         // ヘッダ情報
        public short ax_sel;        // ロギング軸選択(論理軸)
        public short dt_sel;        // ロギングデータサイズ選択
        public short sr_sel;        // ロギング通信方向選択
        public static TPCSEL_ECT Init()
        {
            TPCSEL_ECT tmp = new TPCSEL_ECT();
            tmp.h = Rt64ecdata.TPCSEL_HEADER.Init();
            return tmp;
        }
    }

    // for TPCSEL_ECT.byte_sel（通信データサイズ選択） ※ EtherCAT通信設定に合わせる必要はありません。
    public const short TPCS_ECT_DT_16BYTE = 0;      // 16byteデータロギング
    public const short TPCS_ECT_DT_32BYTE = 1;      // 32byteデータロギング
    public const short TPCS_ECT_DT_48BYTE = 2;      // 48byteデータロギング
    public const short TPCS_ECT_DT_64BYTE = 3;      // 64byteデータロギング

    // for TPCSEL_ECT.sr_sel  （送受信選択）
    public const short TPCS_ECT_RS_ALL = 0;     // 送受信
    public const short TPCS_ECT_RS_SEND = 1;        // 送信のみ
    public const short TPCS_ECT_RS_RECEIVE = 2;     // 受信のみ

    //----------
    // 64CHロギング要求
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct TPCSEL_ATTR
    {
        public int item;                    // 属性項目
        public int val;                 // 属性値
        public static TPCSEL_ATTR Init()
        {
            return (new TPCSEL_ATTR());
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct TPCSEL_CH
    {
        public short log_item;              // ロギング項目
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Reserved;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public TPCSEL_ATTR[] attribute;             // ロギング項目属性
        public static TPCSEL_CH Init()
        {
            TPCSEL_CH tmp = new TPCSEL_CH();
            tmp.Reserved = new byte[6];
            tmp.attribute = new TPCSEL_ATTR[4];
            for (int cnt = tmp.attribute.GetLowerBound(0); cnt <= tmp.attribute.GetUpperBound(0); cnt++)
                tmp.attribute[cnt] = Rt64ecdata.TPCSEL_ATTR.Init();
            return tmp;
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct TPCSEL_64CH
    {
        public TPCSEL_HEADER h;                     // ヘッダ情報
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public TPCSEL_CH[] ch;                      // ロギング軸選択(論理軸)
        public static TPCSEL_64CH Init()
        {
            TPCSEL_64CH tmp = new TPCSEL_64CH();
            tmp.h = Rt64ecdata.TPCSEL_HEADER.Init();
            tmp.ch = new TPCSEL_CH[64];
            for (int cnt = tmp.ch.GetLowerBound(0); cnt <= tmp.ch.GetUpperBound(0); cnt++)
                tmp.ch[cnt] = Rt64ecdata.TPCSEL_CH.Init();
            return tmp;
        }
    }

    // for TPCSEL_CH.log_item  （ロギング項目選択）
    //								-1			指定無し
    public const short TPCS_64_I_COMREG = 0;        // 指令位置
    public const short TPCS_64_I_POSREG = 1;        // 機械位置
    public const short TPCS_64_I_ERRREG = 2;        // 偏差量
    public const short TPCS_64_I_BLOCKSEG = 3;      // 最新ブロック払い出し量
    public const short TPCS_64_I_ABSREG = 4;        // 絶対位置
    public const short TPCS_64_I_TRQ = 5;       // トルク
    public const short TPCS_64_I_MCSTATUS = 6;      // 全体ステータス
    public const short TPCS_64_I_MCALARM = 7;       // 全体アラーム
    public const short TPCS_64_I_AXSTATUS = 8;      // 軸ステータス
    public const short TPCS_64_I_AXALARM = 9;       // 軸アラーム
    public const short TPCS_64_I_TASKSTATUS = 10;       // タスクステータス
    public const short TPCS_64_I_TASKALARM = 11;        // タスクアラーム
    public const short TPCS_64_I_MACRO = 12;        // マクロ変数
    public const short TPCS_64_I_DI = 13;       // 入力信号
    public const short TPCS_64_I_DO = 14;       // 出力信号
    public const short TPCS_64_I_PDO_OBJ = 15;      // PDOデータ(オブジェクト指定)
    public const short TPCS_64_I_RXPDO_DAT = 16;        // RxPDOデータ(オフセット指定)
    public const short TPCS_64_I_TXPDO_DAT = 17;        // TxPDOデータ(オフセット指定)
    public const short TPCS_64_I_TIME = 18;     // 時計
    public const short TPCS_64_I_VCOMREG = 19;      // 仮想指令位置

    // for TPCSEL_ATTR.item  （属性項目選択）
    //								-1			指定無し
    public const int TPCS_64_A_TASK = 0;        // タスク番号
    public const int TPCS_64_A_AXIS = 1;        // 軸番号
    public const int TPCS_64_A_MACRO = 2;       // マクロ変数番号
    public const int TPCS_64_A_IO = 3;      // IO信号番号
    public const int TPCS_64_A_PDO_IDX = 4;     // PDOオブジェクトIndex
    public const int TPCS_64_A_PDO_SUBIDX = 5;      // PDOオブジェクトSubindex
    public const int TPCS_64_A_PDO_DAT_OFS = 6;     // PDOデータオフセット
    public const int TPCS_64_A_PDO_DAT_SZ = 7;      // PDOデータサイズ(1-4[byte])

    // ------------------------------------------------------------------------
    //		座標系設定コマンドパラメータ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct COORDSET
    {
        public int AxisFlag; // 軸選択フラグ
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] PosAxis; // 各軸論理座標値
        public static COORDSET Init()
        {
            COORDSET tmp = new COORDSET();
            tmp.PosAxis = new int[9];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //		軸インタロック設定コマンドパラメータ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct AXINTLK
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public short[] IntlkSw; // 軸インタロック指定スイッチ
        public static AXINTLK Init()
        {
            AXINTLK tmp = new AXINTLK();
            tmp.IntlkSw = new short[9];
            return tmp;
        }
    }


    // ------------------------------------------------------------------------
    //		軸ネグレクト設定コマンドパラメータ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct AXNGLCT
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public short[] NeglectSw; // 軸ネグレクト指定スイッチ
        public static AXNGLCT Init()
        {
            AXNGLCT tmp = new AXNGLCT();
            tmp.NeglectSw = new short[9];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //		各軸サーボＯＮ／ＯＦＦコマンドパラメータ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct SVONOFFCHG
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public short[] SvOnSw; // ｻｰﾎﾞON/OFF指定スイッチ
        public static SVONOFFCHG Init()
        {
            SVONOFFCHG tmp = new SVONOFFCHG();
            tmp.SvOnSw = new short[9];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //		トルク制限モード変更コマンドパラメータ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct TRQLIMCHG
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public short[] TrqLimSw; // トルク制限モード指定スイッチ
        public static TRQLIMCHG Init()
        {
            TRQLIMCHG tmp = new TRQLIMCHG();
            tmp.TrqLimSw = new short[9];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //		各軸制御モード変更コマンドパラメータ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct AXCTRLCHG
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public short[] CtrlSw; // 制御モード指定スイッチ
        public static AXCTRLCHG Init()
        {
            AXCTRLCHG tmp = new AXCTRLCHG();
            tmp.CtrlSw = new short[9];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //		Ｍコード出力コマンドパラメータ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct OUTMCD
    {
        public short mcd; // 出力Ｍコード
        public static OUTMCD Init()
        {
            return (new OUTMCD());
        }
    }

    // ------------------------------------------------------------------------
    //		手動パルサー動作許可コマンド構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct HANDLEPERMIT
    {
        public short permit;        // 許可/不許可フラグ(0:不許可、1:許可)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Reserved; // 予約
        public static HANDLEPERMIT Init()
        {
            HANDLEPERMIT tmp = new HANDLEPERMIT();
            tmp.Reserved = new byte[6];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //		手パモード構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct HANDLEMODE
    {
        public short mode; // 手パモード
        public static HANDLEMODE Init()
        {
            return (new HANDLEMODE());
        }
    }

    // ------------------------------------------------------------------------
    //		手パ倍率パラメータ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct HANDLEKP
    {
        public int kp; // 手パ倍率
        public static HANDLEKP Init()
        {
            return (new HANDLEKP());
        }
    }

    // ------------------------------------------------------------------------
    //		手パ軸パラメータ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct HANDLEAXIS
    {
        public int Axis; // 手パ有効軸
        public static HANDLEAXIS Init()
        {
            return (new HANDLEAXIS());
        }
    }

    // ------------------------------------------------------------------------
    //		サイクル運転モード変更コマンドパラメータ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct CYCLECHG
    {
        public short cycle; // サイクル運転有効フラグ
        public static CYCLECHG Init()
        {
            return (new CYCLECHG());
        }
    }

    // ------------------------------------------------------------------------
    //	フィードバックカウンタクリアコマンドパラメータ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct FBSETUP
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public int[] cntr;      // FB1･FB2設定値
        public short cntrset;   // FB1･FB2設定フラグ
        public short Reserve;   // ﾃﾞｰﾀをﾊﾟｯｸするためのﾀﾞﾐｰ
        public static FBSETUP Init()
        {
            FBSETUP tmp = new FBSETUP();
            tmp.cntr = new int[2];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	マクロ変数書込コマンドパラメータ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct MCRVALSET
    {
        public int RegNo;       // マクロ変数番号
        public int Reserve;     // ﾃﾞｰﾀをﾊﾟｯｸするためのﾀﾞﾐｰ
        public double Val;      // マクロ変数値
        public static MCRVALSET Init()
        {
            return (new MCRVALSET());
        }
    }

    // ------------------------------------------------------------------------
    //	独立位置決めコマンドパラメータ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct AXMV
    {
        public int AxFlg;           // 軸フラグ
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] PosAxis;       // 移動量/目標位置
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] Feed;          // 移動速度
        public static AXMV Init()
        {
            AXMV tmp = new AXMV();
            tmp.PosAxis = new int[9];
            tmp.Feed = new int[9];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	トルク指令コマンドパラメータ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct TRQCMD
    {
        public int AxisFlag;        // 軸フラグ
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] Torque;        // 指令トルク [%]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] VClamp;        // 速度制限 [rpm] (-1:位置制御モード)
        public static TRQCMD Init()
        {
            TRQCMD tmp = new TRQCMD();
            tmp.Torque = new int[9];
            tmp.VClamp = new int[9];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	プログラムブロック 移動コマンドパラメータ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct BLKMVDATA
    {
        public short SrcPno;        // 移動対象P番号(1-32767)
        public short DstBlk;        // 移動先先頭BLOCK番号(0～63)
        public static BLKMVDATA Init()
        {
            return (new BLKMVDATA());
        }
    }

    // ------------------------------------------------------------------------
    //	プログラムブロック コピーコマンドパラメータ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct BLKCPYDATA
    {
        public short SrcPno;        // コピー元P番号(1-32767)
        public short DstBlk;        // コピー先先頭BLOCK番号(0～63)
        public short DstPno;        // コピー後P番号(1-32767)
        public short DstTask;       // コピー後タスク番号(0-7)
        public static BLKCPYDATA Init()
        {
            return (new BLKCPYDATA());
        }
    }

    // ------------------------------------------------------------------------
    //	プログラムブロック 削除コマンドパラメータ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct BLKDLDATA
    {
        public short Pno;           // 削除対象のP番号(1-32767)
        public static BLKDLDATA Init()
        {
            return (new BLKDLDATA());
        }
    }

    // ------------------------------------------------------------------------
    //	タスク番号指定定数
    // ------------------------------------------------------------------------
    public const short TASKMAX = 8; // タスク最大数(0～7)

    /////////////////////////////////////////////////////////////////////////
    // Ｅ－ＣＮＣⅢ専用化データ構造体
    // ------------------------------------------------------------------------
    //	初期化パラメータ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct INITIALPRM    // 初期化パラメータ構造体（２０４８ﾊﾞｲﾄ固定長）
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] ElctdExchPos;          // 電極交換位置
        public int ElctdExchSpdW;           // Ｗ軸電極交換位置移動速度
        public int ElctdExchSpdW1;          // Ｗ軸電極交換前位置移動速度
        public int ElctdExchOfsW1;          // Ｗ軸電極交換前位置オフセット
        public int ElctdExchOfsW2;          // Ｗ軸電極交換待機位置オフセット
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public int[] ElctdChkSpdZ1;         // 電極装着後のセンサーまでのＺ軸下降速度
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public int[] ElctdChkSpdZ2;         // 電極装着後のセンサーからのＺ軸下降速度
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public int[] ElctdChkSpdS2;         // 電極装着後のセンサーからの主軸回転速度
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public int[] ElctdChkOfsZ;          // 電極装着後のセンサーからのＺ軸下降量
        public int ElctdDeplUpZ;            // 電極消耗(Z-OTｵﾝ)／途中停止時のＺ軸上昇量
        public short ElctdNum;              // 電極数
        public short GuideNum;              // ガイド数
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] GuideExchPosS;         // ガイド交換位置始点
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] GuideExchPosE;         // ガイド交換位置終点
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] GuideChkPos;           // ガイド有無センサー位置
        public int GuideExchSpdW;           // Ｗ軸ガイド交換位置移動速度
        public int GuideExchOfsW1;          // Ｗ軸ガイド交換前位置オフセット
        public int GuideExchOfsW2;          // Ｗ軸ガイド交換待機位置オフセット
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] EdgeSrchOfs;           // 端面位置だし時の＋もしくは－分移動量
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] EdgeSrchTouchSpd1;     // 端面位置だし時の１度目の接触速度
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] EdgeSrchTouchRet1;     // 端面位置だし時の１度目の戻り量
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] EdgeSrchTouchSpd2;     // 端面位置だし時の２，３度目の接触速度
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] EdgeSrchTouchRet2;     // 端面位置だし時の２，３度目の戻り量
        public int EdgeSrchZDwnSpd;     // 端面位置だし時のＺ軸下降速度
        public int TouchSenseTime;          // 端面位置だし時接触感知時間
        public int RefTouchUpZ;         // 測定点Ｚ接触後のＺＵＰ量設定
        public int PrcsRetSpdZ;         // 放電加工終了時Ｚ軸上昇速度
        public int EdgeSrchZUpSpd;          // 端面位置だし時のＺ軸上昇速度
        public int ElctdClumpSpdS;          // 電極装着(ｺﾚｯﾄｸﾗﾝﾌﾟ)までの主軸回転速度
        public int BucklingUpOfsZ;          // 細線電極確認時の座屈ｾﾝｻｰONによるZ上昇量
        public int BucklingUpSpdZ;          // 細線電極確認時の座屈ｾﾝｻｰONによるZ上昇速度
        public short BucklingRetry;         // 細線電極確認時の座屈ｾﾝｻｰONによるﾘﾄﾗｲ回数
        public short AecEnable;             // ＡＥＣ有効/無効(D0:ESF、D1:GSF)
        public int Z20ErrOfs;               // Ｚ２０エラー検出オフセット
        public short McnType;               // 機械タイプ（0:通常機、1:油仕様機）
        public short GuideSensorDis;            // ガイド貫通検出センサー無効設定
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public int[] CysCheckDis;           // CYSチェック無効設定
                                            // 	CysCheckDis[0] : D00:  1 ～ D31: 32
                                            // 	CysCheckDis[1] : D00: 33 ～ D31: 64
                                            // 	CysCheckDis[2] : D00: 65 ～ D31: 96
                                            // 	CysCheckDis[3] : D00: 97 ～ D31:128
        public short AxAecActDis;           // 電極・ガイド交換動作無効軸設定(A/B/C軸のみ)
        public short AxBrakeEn;             // ブレーキ軸設定(A/B/C軸のみ)
        public short BrakeTimer;                // ブレーキ作動時間[msec]
        public short TchErrCancelTime;      // 接触感知エラーキャンセル時間
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public int[] CylPulseOut;           // CYLパルス出力設定
                                            // 	CylPulseOut[0] : D00:  1 ～ D31: 32
                                            // 	CylPulseOut[1] : D00: 33 ～ D31: 64
                                            // 	CylPulseOut[2] : D00: 65 ～ D31: 96
                                            // 	CylPulseOut[3] : D00: 97 ～ D31:128
        public short CylPulseTime;          // CYLパルス幅[msec]
        public short PrcsFirstP99xDis;      // 放電加工時FirstSparkまでの加工条件P99x(1～6)無効設定
        public short LeaveZrnFin;           // アラーム時原点復帰済みﾌﾗｸﾞを残す軸ﾌﾗｸﾞ
        public short EdgeSrchMaxMinUse;     // 端面位置計算時最大/最小値使用フラグ
        public short EdgeSrchOldMcnComp;        // ｺｰﾅｰ出し/芯出し動作の旧機種互換動作ﾌﾗｸﾞ
        public short CylSelBfrEDeplAec;     // 電極消耗時のAEC前 CYL出力選択
                                            // (0～128:0は無効、D08はOFF指定)
        public short CylSelAftEDeplAec;     // 電極消耗時のAEC後 CYL出力選択
                                            // (0～128:0は無効、D08はOFF指定)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] Reserved556;          // 予約
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public int[] CysOffChkDis;          // CYSオフチェック無効設定
                                            // 	CysOffChkDis[0] : D00:  1 ～ D31: 32
                                            // 	CysOffChkDis[1] : D00: 33 ～ D31: 64
                                            // 	CysOffChkDis[2] : D00: 65 ～ D31: 96
                                            // 	CysOffChkDis[3] : D00: 97 ～ D31:128
        public int SvPrcsStpRetOfs;     // サーボ放電加工途中停止時戻り量
        public int SvPrcsRetOfs;            // サーボ放電加工終了時戻り量
        public int SvPrcsRetSpd;            // サーボ放電加工終了時戻り速度
        public short AxServoPrcs;           // サーボ放電加工対象軸設定(D0:X ～ )
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] Reserved588;          // 予約
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public int[] CysLatch;              // CYS信号ラッチ選択
                                            // 	CysLatch[0] : D00:  1 ～ D31: 32
                                            // 	CysLatch[1] : D00: 33 ～ D31: 64
                                            // 	CysLatch[2] : D00: 65 ～ D31: 96
                                            // 	CysLatch[3] : D00: 97 ～ D31:128
        public short InstLastElctdFlg;      // 最終電極装着モード有効フラグ
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] Reserved608;          // 予約
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] SpinMaxSpd;                // 無限回転軸回転速度上限値 [pps]
        public short PrcsSkipStopCys;       // 放電加工スキップ時停止モードCYS信号選択
        public short DischgTchErrEn;            // 放電中の接触感知エラー検出有効フラグ
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public int[] CFeedTable;                // 主軸速度(0～15)->Ｃ軸速度(0.1rpm)テーブル
        public short GuideThroughChkDis;        // ガイド貫通検出無効パーティションフラグ
        public short Z2ndSoftLimMSelCys;        // Ｚ軸第２ソフトリミット指定CYS信号選択
        public int Z2ndSoftLimM;            // Ｚ軸第２ソフトリミット
        public short GuidChgCylEn;          // ガイド交換時ＣＹＬ出力有効フラグ
                                            //   0:無効, 1～128:CYL出力信号先頭番号
        public short GuidChgCysSel;         // ガイド交換時ＣＹＬ出力後ＣＹＳ信号待選択
                                            //   0:無効, 1～128:CYS信号番号
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] GuidHolderEscOfs;      // ガイドホルダー退避量
        public short GuidHolderArmDis;      // ガイドホルダーアームシリンダ無効フラグ
        public short TouchSenseISetEn;      // 接触感知によるｲﾆｼｬﾙｾｯﾄ有効ﾌﾗｸﾞ(電源投入時初期値)
        public short CylSelPatRndStp;       // パーティション１周停止中 CYL出力選択
        public short PatRndStpOnlyElctdDep; // 電極消耗による電極交換時のみﾊﾟｰﾃｨｼｮﾝ１周停止有効ﾌﾗｸﾞ
        public short CylSelThrghDetect;     // 放電加工時貫通検出後のCYL出力選択
                                            // (0～128:0は無効、D08はOFF指定)
        public short RefSpdSelThrghDetect;  // 放電加工時貫通検出基準速度選択(0:無負荷時速度、1:SFR-DN)
        public short PerInitThrghDetect;        // 放電加工時貫通検出基準％選択初期値   (M91 I指定)
        public short PerInitThDetectEdge;   // 放電加工時抜け際検出基準％選択初期値 (M81 I指定)
        public int AvTimInitThDetectEdge;   // 放電加工時抜け際検出速度計測設定初期値 (M81 J指定)
        public short RefSpdIgnHPerInit;     // 放電加工時貫通検出基準速度計測上側無視％初期値 (M25 H指定)
        public short RefSpdIgnLPerInit;     // 放電加工時貫通検出基準速度計測下側無視％初期値 (M25 I指定)
        public short RefSpdAvTimInit;       // 放電加工時貫通検出基準速度計測平均化時間初期値 (M25 J指定)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] Reserved788;          // 予約
        public int PreElctdChgLmtPos;       // 電極消耗前電極交換リミット座標
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public short[] ElctdExchMovOrder;       // 電極交換位置移動順（1～9：0は最後）
        public short CylSelInElctdPrcs;     // １穴電極放電加工中CYL出力選択 (0～128 : 0は出力無効)
        public short PatliteMode;           // パトライト出力モード選択 (0～2)
        public short AxVirActDis;           // 仮想点/測定点移動無効軸フラグ(A/B/C軸のみ)(電源投入時初期値)
        public short SvPrcsInitialTime;     // サーボ放電加工初期化時間(放電安定待ち時間) [msec]
        public short ThinElctdISetPumpOff;  // 細線電極でのイニシャルセット時
                                            // ポンプ無効パーティションフラグ(D0:1 ～ D5:6)
        public short AecWRefSel;                // ＡＥＣ開始/終了時Ｗ軸待避位置選択
                                                // (0=Ｗ軸原点, 1=Ｗ軸上限値)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] Reserved824;          // 予約
        public short GuideThroughChkPNo;        // ｶﾞｲﾄﾞ貫通ﾁｪｯｸ時加工条件番号設定 (電源投入時初期値)
                                                // (0			= 従来通りP998/999を使用
                                                //  16384～17383 = P0～P999を使用
                                                //				 (D14を有効フラグとして使用))
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public short[] InitialSetPno;           // ｲﾆｼｬﾙｾｯﾄ時加工条件番号設定 (電源投入時初期値)
                                                // (0			= 従来通りP998/999を使用
                                                //  16384～17383 = P0～P999を使用
                                                //				 (D14を有効フラグとして使用))
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] Reserved840;              // 予約
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public int[] ElctdChkOfsZ2;         // 電極装着後のｾﾝｻｰからのＺ軸２段目下降量設定(電源投入時初期値)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public short[] GuideThroughChkPNoPat;   // ガイド貫通チェック時加工条件番号設定 (電源投入時初期値)
                                                // (0			= 従来通りP998/999を使用
                                                //  16384～17383 = P0～P999を使用
                                                //				 (D14を有効フラグとして使用))
        public short EUninstColReclampDis;  // 電極脱着時のコレット再クランプ(咥え直し)動作無効フラグ
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] Reserved880;          // 予約
        public int AvTimInitThrghDetect;    // 放電加工時貫通検出速度計測設定
                                            //(平均化時間/速度計測距離)初期値(M91 J指定)
        public int EdgeLSrchTouchRet;       // 複数軸直線補間端面位置だし時戻り量[pls](全軸合成距離)
        public int EdgeLSrchRetSpd;     // 複数軸直線補間端面位置だし時戻り速度[pps](全軸合成速度)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Reserved896;          // 予約
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public double[] InitPrmMacVal;          // 初期化パラメータ指定固定マクロ変数値(#1500～#1509)
        public short NrmElctdPrcsBuckling;  // 通常電極での放電加工時座屈ｾﾝｻｰ検出フラグ
                                            // (0:無効、1:エラー、2:停止、3:電極交換)
        public short NrmElctdGuideBuckling; // 通常電極でのガイド貫通時座屈ｾﾝｻｰ検出フラグ
                                            // (0:無効、1:エラー、2:次電極取得)
        public short RotAxMovMode;          // 無限回転軸アブソ位置決め移動方法初期値 (0～5)
                                            // (0:従来通り、1:近回り、
                                            //  2:符号判別(360°以上なし)、
                                            //  3:符号判別(360°以上あり)、
                                            //  4:常時＋方向、5:常時－方向)
        public short AutoModeOutSel;            // 自動モード中出力信号選択
                                                // (-1～128：0=無効, -1=o#1727 D06, 1～128=CYL信号)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public int[] CylBitRstPlsCont;      // リセット/エラー発生時CYLパルス出力継続設定
                                            // 	CylBitRstPlsCont[0] : D00:  1 ～ D31: 32
                                            // 	CylBitRstPlsCont[1] : D00: 33 ～ D31: 64
                                            // 	CylBitRstPlsCont[2] : D00: 65 ～ D31: 96
                                            // 	CylBitRstPlsCont[3] : D00: 97 ～ D31:128
        public short PrcsTmoutChkEndSel;        // 放電加工タイムアウト検出終了タイミング選択
                                                // (0:加工深さ到達で終了、
                                                //  1:放電OFF(スパークアウトタイマ時間経過
                                                //   ＆放電加工時貫通検出後のCYL出力完了)で終了)
        public short MountedSF02FX;         // SF02FX搭載フラグ[0:無し,1:有り]
        public int EIFErr1MaskSet;          // EtherCAT IFボード エラー1マスク設定
        public int EIFErr2MaskSet;          // EtherCAT IFボード エラー2マスク設定
        public short Fanstop10DlyTim;       // 本機内FAN停止 加工終了後待ち時間[sec]
        public short Fanstop20DlyTim;       // SF02FX内FAN停止 加工終了後待ち時間[sec]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] EIFErr1Action;            // EtherCAT IFボード エラー1(6000h,01h)発生時動作設定
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] EIFErr2Action;            // EtherCAT IFボード エラー2(6000h,02h)発生時動作設定
        public short MountedBP20;           // BP20搭載フラグ[0:無し,1:有り]
        public short MountedIPEnhance;      // 拡張IP搭載フラグ[0:無し,1:有り]
        public short MountedACPower;            // 交流電源搭載フラグ[0:無し,1:有り]
        public short MountedNanoPulse;      // ナノパルス電源搭載フラグ[0:無し,1:有り]
        public short MountedHVOverlay;      // 高圧重畳電源搭載フラグ[0:無し,1:有り]
        public short MountedTouchProbe;     // タッチプローブ搭載フラグ[0:無し,1:有り]
        public int VSLvlSetting;            // VSレベル設定（デフォルト：2.0V）
        public int VSPKLvlSetting;          // VSPKレベル設定（デフォルト：2.9V）
        public int CntactSenseLvlSetting;   // 接触感知レベル設定（デフォルト0.8V）
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public short[] HPJogOvr;                // 手動パルサJOGオーバーライド(0～200%)
        public short PLCShutDwonAuthEn;     // PLCシャットダウン権限有効[0:無効,1:有効]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)]
        public byte[] Reserved1144;         // 予約
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public short[] EX1E321Setting;          // EX1-E321インターフェース設定(7500h)
                                                // EX1E321Setting[ 0] : ソフトスタート低速速度設定[50～750ms(50ms間隔)]	(7500h,01h)	
                                                // EX1E321Setting[ 1] : ソフトスタート高速速度設定[ 2～ 30ms( 2ms間隔)]	(7500h,02h)	
                                                // EX1E321Setting[ 2] : ソフトスタート開始電流設定						(7500h,03h)	
                                                //                       0x00:IP=0A, 0x01:IP=3Aから開始する。						
                                                // EX1E321Setting[ 3] : BONDLY_E323_O設定[1～255ms(デフォルト8ms)]		(7500h,04h)	
                                                // EX1E321Setting[ 4] : BONDLY_0設定     [1～255ms(デフォルト8ms)]		(7500h,05h)	
                                                // EX1E321Setting[ 5] : BON信号選択設定									(7500h,06h)	
                                                //                       0x00:BON10=BONDLY_E323_O / BON00=BONDLY0					
                                                //                       0x01:BON10=BON0          / BON00=BON10						
                                                // EX1E321Setting[ 6] : OFF伸ばし倍率設定[1～32倍(デフォルト10倍)]		(7500h,07h)	
                                                // EX1E321Setting[ 7] : Zクリアエラー検出トリガ信号選択設定				(7500h,08h)	
                                                //                       0x00:VSPK , 0x01:VS										
                                                // EX1E321Setting[ 8] : Zクリアエラーカウント数設定						(7500h,09h)	
                                                //                       0x00～0xFF(デフォルト0x00)									
                                                // EX1E321Setting[ 9] : EX5-E504切替設定(0:未使用,1:使用)				(7500h,0Ah)	
                                                // EX1E321Setting[10] : FLS有効/無効設定								(7500h,0Bh)	
                                                //                       0x00:FLS1信号無効(デフォルト), 0x01:FLS1信号で放電OFF		
                                                // EX1E321Setting[11] : リセット放電設定								(7500h,0Ch)	
                                                //                       bit0-3  : パルス幅      (0x0～0xF：0～15us)				
                                                //                       bit4    : 有効/無効設定 (0:無効, 1:有効)					
                                                //                       bit5-15 : Reserve											
                                                // EX1E321Setting[12]-[19] : Reserve												
        public short CRS10DAMin;                // インバータD/A出力(7300h,01h) 最小値		
        public short CRS10DAMax;                // インバータD/A出力(7300h,01h) 最大値		
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public short[] CRS10DATable;                // インバータD/A出力(7300h,01h) テーブル	
        public short SCDAMin;               // ｻｰﾎﾞｺﾝﾄﾛｰﾙ(SC値)D/A出力(7300h,02h) 最小値
        public short SCDAMax;               // ｻｰﾎﾞｺﾝﾄﾛｰﾙ(SC値)D/A出力(7300h,02h) 最大値
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public short[] SCDATable;               // ｻｰﾎﾞｺﾝﾄﾛｰﾙ(SC値)D/A出力(7300h,02h) テーブル
        public short CRSDAMin;              // SP軸ｺﾝﾄﾛｰﾙ(CRS値)D/A出力(7300h,03h) 最小値
        public short CRSDAMax;              // SP軸ｺﾝﾄﾛｰﾙ(CRS値)D/A出力(7300h,03h) 最大値
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public short[] CRSDATable;              // SP軸ｺﾝﾄﾛｰﾙ(CRS値)D/A出力(7300h,03h) テーブル
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 660)]
        public byte[] Reserved2048;         // 予約
        public static INITIALPRM Init()
        {
            INITIALPRM tmp = new INITIALPRM();
            tmp.ElctdExchPos = new int[9];
            tmp.ElctdChkSpdZ1 = new int[6];
            tmp.ElctdChkSpdZ2 = new int[6];
            tmp.ElctdChkSpdS2 = new int[6];
            tmp.ElctdChkOfsZ = new int[6];
            tmp.GuideExchPosS = new int[9];
            tmp.GuideExchPosE = new int[9];
            tmp.GuideChkPos = new int[9];
            tmp.EdgeSrchOfs = new int[9];
            tmp.EdgeSrchTouchSpd1 = new int[9];
            tmp.EdgeSrchTouchRet1 = new int[9];
            tmp.EdgeSrchTouchSpd2 = new int[9];
            tmp.EdgeSrchTouchRet2 = new int[9];
            tmp.CysCheckDis = new int[4];
            tmp.CylPulseOut = new int[4];
            tmp.Reserved556 = new byte[2];
            tmp.CysOffChkDis = new int[4];
            tmp.Reserved588 = new byte[2];
            tmp.CysLatch = new int[4];
            tmp.Reserved608 = new byte[2];
            tmp.SpinMaxSpd = new int[9];
            tmp.CFeedTable = new int[16];
            tmp.GuidHolderEscOfs = new int[9];
            tmp.Reserved788 = new byte[2];
            tmp.ElctdExchMovOrder = new short[9];
            tmp.Reserved824 = new byte[2];
            tmp.Reserved840 = new byte[2];
            tmp.ElctdChkOfsZ2 = new int[6];
            tmp.GuideThroughChkPNoPat = new short[6];
            tmp.Reserved880 = new byte[2];
            tmp.Reserved896 = new byte[4];
            tmp.InitPrmMacVal = new double[10];
            tmp.CylBitRstPlsCont = new int[4];
            tmp.EIFErr1Action = new byte[32];
            tmp.EIFErr2Action = new byte[32];
            tmp.HPJogOvr = new short[4];
            tmp.Reserved1144 = new byte[30];
            tmp.EX1E321Setting = new short[20];
            tmp.CRS10DATable = new short[16];
            tmp.SCDATable = new short[64];
            tmp.CRSDATable = new short[16];
            tmp.Reserved2048 = new byte[660];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	加工条件情報構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct PCONDITION
    {
        public short Status;                    // 各種状態情報
        public short PNo;                   // 加工条件番号
        public int ZUpFeed;             // Ｚ軸上昇速度
        public int ZDwFeed;             // Ｚ軸下降速度
        public short CFeed;                 // 主軸送り速度
        public short SpinOut;               // 主軸回転(0:停止,1:CW,2:CCW)
        public short PumpOut;               // ポンプ出力(0:OFF,1:ON)
        public short PrCFeed;               // 放電加工時主軸速度(加工条件)
        public int CFeedRpm;                // Ｃ軸主軸速度(0.1rpm)
        public int DryRunEnN;               // ドライラン有効フラグ(放電加工穴数)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Reserved;             // 予約
        public static PCONDITION Init()
        {
            PCONDITION tmp = new PCONDITION();
            tmp.Reserved = new byte[4];
            return tmp;
        }
    }

    // for PCONDITION.Status
    public const short PCS_RETURN = 0x0001; // 返送コマンド要求
                                            //public const short				= 0x0002;
    public const short PCS_DRYRUN = 0x0004; // ドライラン有効
    public const short PCS_INITIALSET = 0x0008; // InitialSet有効
    public const short PCS_THINLINE = 0x0010;   // 細線設定有効
                                                //public const short				= 0x0020;
    public const short PCS_CFEED = 0x0040;  // 主軸回転速度変更要求
    public const short PCS_AEC = 0x0080;    // 電極交換有効
    public const short PCS_PATRNDSTPEN = 0x0100;    // ﾊﾟｰﾃｨｼｮﾝ内１周停止有効
    public const short PCS_PATRNDSTPIN = 0x0200;    // ﾊﾟｰﾃｨｼｮﾝ内１周停止中
    public const short PCS_GUIDETHROUGH = 0x0400;   // ガイド貫通動作許可要求
    public const short PCS_IN_DISCHARGE = 0x0800;   // １穴加工中
    public const short PCS_MAN_AEC = 0x1000;    // 手動電極交換要求
    public const short PCS_BON = 0x2000;    // 放電ＯＮ中
    public const short PCS_ISET_FIN = 0x4000;   // イニシャルセット済み

    // ------------------------------------------------------------------------
    //	ＡＥＣ状態構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct AECDATA
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public short[] PartitionS;              // 第1～6ﾊﾟｰﾃｨｼｮﾝ開始番号
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public short[] PartitionE;              // 第1～6ﾊﾟｰﾃｨｼｮﾝ終了番号
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public short[] Thinline;                // 第1～6ﾊﾟｰﾃｨｼｮﾝ細線設定(1:細線,0:通常)
        public short PartitionDis;          // ﾊﾟｰﾃｨｼｮﾝ無効ﾌﾗｸﾞ(1:無効,0:有効)
        public short ElectrodeNo;           // 電極番号	 (0:未設定)
        public short GuideNo;               // ガイド番号(0:未設定)
        public short IndexZrnFin;           // インデックス番号有効フラグ
        public short IndexNo;               // インデックス番号
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 18)]
        public byte[] Reserved;             // 予約
        public static AECDATA Init()
        {
            AECDATA tmp = new AECDATA();
            tmp.PartitionS = new short[6];
            tmp.PartitionE = new short[6];
            tmp.Thinline = new short[6];
            tmp.Reserved = new byte[18];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	ポイント構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct AXPOINT
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] Pnt;                   // 第１～９軸座標値
        public static AXPOINT Init()
        {
            AXPOINT tmp = new AXPOINT();
            tmp.Pnt = new int[9];
            return tmp;
        }
    };

    // ------------------------------------------------------------------------
    //	各軸仮想点/測定点座標構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct VIRPOS_EX
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1001)]
        public AXPOINT[] AxPnt;             // 各軸仮想点/測定点座標
                                            // （0～999：仮想点、1000:測定点）
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Reserved;         // 予約
        public static VIRPOS_EX Init()
        {
            VIRPOS_EX tmp = new VIRPOS_EX();
            tmp.AxPnt = new AXPOINT[1001];
            for (int cnt = tmp.AxPnt.GetLowerBound(0); cnt <= tmp.AxPnt.GetUpperBound(0); cnt++)
                tmp.AxPnt[cnt] = Rt64ecdata.AXPOINT.Init();
            tmp.Reserved = new byte[4];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	ワーク原点座標設定構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct WORKORG
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] WorkOrg;               // 第１～９軸ワーク原点座標
        public static WORKORG Init()
        {
            WORKORG tmp = new WORKORG();
            tmp.WorkOrg = new int[9];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	ＲＯＭＳＷパラメータ構造体（ＰＣ解放用）
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct ROMSW //	ROMSWパラメータ構造体（２５６００ﾊﾞｲﾄ固定長）
    {
        public long st_en;              // 有効局フラグ(物理局フラグ)
        public long axis_en;            // 有効軸フラグ(物理局フラグ)
        public long io_en;              // 有効IOフラグ(物理局フラグ)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20496)]
        public byte[] Reserved1;            // 予約
        public long spindle_ax;         // 主軸有効軸フラグ
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5072)]
        public byte[] Reserved2;            // 予約
        public static ROMSW Init()
        {
            ROMSW tmp = new ROMSW();
            tmp.Reserved1 = new byte[20496];
            tmp.Reserved2 = new byte[5072];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	各種補正データモニタ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct CORRECTDATA
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] MachinePos;            // 角度補正/ﾏｼﾝﾛｯｸ補正後実座標
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] MachineLock;       // マシンロック補正値
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] Backlash;          // バックラッシュ補正値
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] Pitcherr;          // ピッチエラー補正値
        public int AngSin;              // 補正角度計測値（sinθ,2-30bit固定小数点数）
        public int AngCos;              // 補正角度計測値（cosθ,2-30bit固定小数点数）
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public int[] Angle;             // 角度補正値（X/Y軸）
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public int[] AngMlk;                // 角度補正時ﾏｼﾝﾛｯｸ補正値(X/Y軸)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public int[] AngStart;          // 角度補正開始座標（X/Y軸）
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public int[] AngMeas1;          // 補正角度計測値（１点目X/Y軸）
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public int[] AngMeas2;          // 補正角度計測値（２点目X/Y軸）
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public int[] Mirror;                // ミラー補正値（X/Y軸）
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public int[] MirCentCoord;      // ミラー基準座標（X/Y軸）
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 48)]
        public byte[] Reserved;         // 予約
        public static CORRECTDATA Init()
        {
            CORRECTDATA tmp = new CORRECTDATA();
            tmp.MachinePos = new int[9];
            tmp.MachineLock = new int[9];
            tmp.Backlash = new int[9];
            tmp.Pitcherr = new int[9];
            tmp.Angle = new int[2];
            tmp.AngMlk = new int[2];
            tmp.AngStart = new int[2];
            tmp.AngMeas1 = new int[2];
            tmp.AngMeas2 = new int[2];
            tmp.Mirror = new int[2];
            tmp.MirCentCoord = new int[2];
            tmp.Reserved = new byte[48];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	放電加工スキップ時仮想点登録番号モニタ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct PRCSSKIPVPOSNO
    {
        public short LastNo;                // 前回登録番号					*/
        public short NextNo;                // 次回登録番号					*/
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Reserved;         // 予約							*/
        public static PRCSSKIPVPOSNO Init()
        {
            PRCSSKIPVPOSNO tmp = new PRCSSKIPVPOSNO();
            tmp.Reserved = new byte[4];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	メッセージ表示要求データ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct MESSAGEREQ
    {
        public short LineFlg;           // メッセージ表示命令実行プログラム種別
        public short MessageNo;         // メッセージ番号
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Reserved;         // 予約
        public static MESSAGEREQ Init()
        {
            MESSAGEREQ tmp = new MESSAGEREQ();
            tmp.Reserved = new byte[4];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	メッセージ表示結果データ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct MESSAGEANS
    {
        public MESSAGEREQ Req;          // メッセージ表示要求元データ	*/
        public short ButtonSel;     // 選択(クリック)ボタン番号		*/
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Reserved;     // 予約							*/
        public double InputNum;     // 数値入力値					*/
        public static MESSAGEANS Init()
        {
            MESSAGEANS tmp = new MESSAGEANS();
            tmp.Req = Rt64ecdata.MESSAGEREQ.Init();
            tmp.Reserved = new byte[6];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	カメラコマンド要求データ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct CAMERACMDREQ
    {
        public short CommandNo;     // コマンド番号
        public short Timeout;       // タイムアウト時間
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Reserved;     // 予約
        public static CAMERACMDREQ Init()
        {
            CAMERACMDREQ tmp = new CAMERACMDREQ();
            tmp.Reserved = new byte[4];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	カメラコマンド結果浮動小数点数データ構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct CAMERACMDANSF
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public double[] RDat;           // 結果データ
        public static CAMERACMDANSF Init()
        {
            CAMERACMDANSF tmp = new CAMERACMDANSF();
            tmp.RDat = new double[10];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	加工条件テーブル構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct PCOND_REC
    {
        public short Ton;           // Ton[us]
        public short Toff;          // Toff[us]
        public int IPVal;           // IP(下位8bit:EX1-E602用ﾊﾞｲﾅﾘ)
        public int SFIPVal;     // SFIP(下位9bit:EX1-E602用ﾊﾞｲﾅﾘ)
        public int CAPVal;          // CAP(下位10bit:EX1-E602用ﾊﾞｲﾅﾘ)
        public short SCSel;         // サーボコントロールDA(0-63)
        public short CRSSel;            // SP軸コントロールDA(0-15)
        public int SfrFr;           // 加工サーボ送り速度[pls/min]
        public int SfrBk;           // 加工サーボ戻り速度[pls/min]
        public short PumpPrSel;     // ポンプ圧力選択(0-3)
        public short ServoSel;      // サーボ選択(0-3)
        public short PSSel;         // 電源選択(0-5)
        public short POLVal;            // 条件切替え
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 28)]
        public byte[] Reserved;     // 予約
        public static PCOND_REC Init()
        {
            PCOND_REC tmp = new PCOND_REC();
            tmp.Reserved = new byte[28];
            return tmp;
        }
    }
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct PCOND_TBL
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1000)]
        public PCOND_REC[] rec;                     // ロギング軸選択(論理軸)
        public static PCOND_TBL Init()
        {
            PCOND_TBL tmp = new PCOND_TBL();
            tmp.rec = new PCOND_REC[1000];
            for (int cnt = tmp.rec.GetLowerBound(0); cnt <= tmp.rec.GetUpperBound(0); cnt++)
                tmp.rec[cnt] = Rt64ecdata.PCOND_REC.Init();
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	ECNC専用外部アラーム要因構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct ECNCEXTALM
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
        public byte[] Factor;       // 外部アラーム要因(1024Bit)
        public static ECNCEXTALM Init()
        {
            ECNCEXTALM tmp = new ECNCEXTALM();
            tmp.Factor = new byte[128];
            return tmp;
        }
    }

    /////////////////////////////////////////////////////////////////////////
    // Ｅ－ＣＮＣⅢ専用化コマンド構造体

    // ------------------------------------------------------------------------
    //	有効／無効設定構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct ENABLE
    {
        public short enable;            // 有効/無効設定フラグ(0:無効、1:有効)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Reserved;     // 予約
        public static ENABLE Init()
        {
            ENABLE tmp = new ENABLE();
            tmp.Reserved = new byte[6];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	Ｗ軸上限値設定構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct WTOPPOS
    {
        public int WTopPos;     // Ｗ軸上限値
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Reserved;     // 予約
        public static WTOPPOS Init()
        {
            WTOPPOS tmp = new WTOPPOS();
            tmp.Reserved = new byte[4];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	加工条件番号設定コマンド構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct PRNOSEL
    {
        public int PNo;         // 加工条件番号
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Reserved;     // 予約
        public static PRNOSEL Init()
        {
            PRNOSEL tmp = new PRNOSEL();
            tmp.Reserved = new byte[4];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	仮想点／測定点変更コマンド構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct VIRPOSCHG
    {
        public int VirNo;               // 仮想点/測定点番号
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] VirPos;                // 仮想点座標
        public static VIRPOSCHG Init()
        {
            VIRPOSCHG tmp = new VIRPOSCHG();
            tmp.VirPos = new int[9];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	プログラム実行開始コマンド構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct PROGSTRT
    {
        public short NNo;               // 実行開始Ｎ番号
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Reserved;         // 予約
        public static PROGSTRT Init()
        {
            PROGSTRT tmp = new PROGSTRT();
            tmp.Reserved = new byte[6];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	パーティション設定コマンド構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct PARTITION
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public short[] PartitionS;          // 第１～６ﾊﾟｰﾃｨｼｮﾝ開始番号
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public short[] PartitionE;          // 第１～６ﾊﾟｰﾃｨｼｮﾝ終了番号
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public short[] Thinline;            // 第1～6ﾊﾟｰﾃｨｼｮﾝ細線設定(1:細線,0:通常)
        public short PartitionDis;      // ﾊﾟｰﾃｨｼｮﾝ無効ﾌﾗｸﾞ(1:無効,0:有効)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Reserved;         // 予約
        public static PARTITION Init()
        {
            PARTITION tmp = new PARTITION();
            tmp.PartitionS = new short[6];
            tmp.PartitionE = new short[6];
            tmp.Thinline = new short[6];
            tmp.Reserved = new byte[6];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	電極番号設定コマンド構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct ELCTDNO
    {
        public short ElectrodeNo;       // 電極番号
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Reserved;         // 予約
        public static ELCTDNO Init()
        {
            ELCTDNO tmp = new ELCTDNO();
            tmp.Reserved = new byte[6];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	電極装着/脱着コマンド構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct ELCTDINSTALL
    {
        public short install;           // 動作フラグ(0:脱着、1:装着)
        public short ElctdNo;           // 電極番号
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Reserved;         // 予約
        public static ELCTDINSTALL Init()
        {
            ELCTDINSTALL tmp = new ELCTDINSTALL();
            tmp.Reserved = new byte[4];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	電極交換位置移動コマンド構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct ELCTDEXCHPOS
    {
        public short axis;              // 軸選択フラグ
        public short pos;               // 動作フラグ
                                        // (0:交換位置、1:前位置、2:待機位置)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Reserved;         // 予約
        public static ELCTDEXCHPOS Init()
        {
            ELCTDEXCHPOS tmp = new ELCTDEXCHPOS();
            tmp.Reserved = new byte[4];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	ＥＳＦアーム移動コマンド構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct MOVEESFARM
    {
        public short pos;               // 動作フラグ(0:前端、1:中間、2:後端)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Reserved;         // 予約
        public static MOVEESFARM Init()
        {
            MOVEESFARM tmp = new MOVEESFARM();
            tmp.Reserved = new byte[6];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	ＥＳＦフィンガーOPEN/CLOSEコマンド構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct OPENESFARM
    {
        public short open;              // 動作フラグ(0:Close、1:Open)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Reserved;         // 予約
        public static OPENESFARM Init()
        {
            OPENESFARM tmp = new OPENESFARM();
            tmp.Reserved = new byte[6];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	ガイド番号設定コマンド構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct GUIDENO
    {
        public short GuideNo;           // ガイド番号
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Reserved;         // 予約
        public static GUIDENO Init()
        {
            GUIDENO tmp = new GUIDENO();
            tmp.Reserved = new byte[6];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	ガイド装着/脱着コマンド構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct GUIDEINSTALL
    {
        public short install;           // 動作フラグ(0:脱着、1:装着)
        public short GuideNo;           // ガイド番号
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Reserved;         // 予約
        public static GUIDEINSTALL Init()
        {
            GUIDEINSTALL tmp = new GUIDEINSTALL();
            tmp.Reserved = new byte[4];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	ガイド交換位置移動コマンド構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct GUIDEEXCHPOS
    {
        public short axis;              // 軸選択フラグ
        public short pos;               // 動作フラグ(0:交換位置、2:待機位置)
        public short GuideNo;           // ガイド番号
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] Reserved;         // 予約
        public static GUIDEEXCHPOS Init()
        {
            GUIDEEXCHPOS tmp = new GUIDEEXCHPOS();
            tmp.Reserved = new byte[2];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	ガイド確認位置移動コマンド構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct GUIDECHKPOS
    {
        public short axis;              // 軸選択フラグ
        public short pos;               // 動作フラグ
                                        // (0:確認位置、1:前位置、2:待機位置)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Reserved;         // 予約
        public static GUIDECHKPOS Init()
        {
            GUIDECHKPOS tmp = new GUIDECHKPOS();
            tmp.Reserved = new byte[4];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	ＧＳＦアーム移動コマンド構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct MOVEGSFARM
    {
        public short pos;               // 動作フラグ(0:前端、1:後端)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Reserved;         // 予約
        public static MOVEGSFARM Init()
        {
            MOVEGSFARM tmp = new MOVEGSFARM();
            tmp.Reserved = new byte[6];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	ガイドクランプ/アンクランプコマンド構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct GUIDECLUMP
    {
        public int clump;               // 動作ﾌﾗｸﾞ(0:Unclump、1:Clump)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Reserved;         // 予約
        public static GUIDECLUMP Init()
        {
            GUIDECLUMP tmp = new GUIDECLUMP();
            tmp.Reserved = new byte[4];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	スピンドルクランプ/アンクランプコマンド構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct SPCLUMP
    {
        public int clump;               // 動作ﾌﾗｸﾞ(0:Unclump、1:Clump)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Reserved;         // 予約
        public static SPCLUMP Init()
        {
            SPCLUMP tmp = new SPCLUMP();
            tmp.Reserved = new byte[4];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	ＥＳＦマガジン移動コマンド構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct ESFMAGMOV
    {
        public int MagazineNo;          // 移動先マガジン番号
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Reserved;         // 予約
        public static ESFMAGMOV Init()
        {
            ESFMAGMOV tmp = new ESFMAGMOV();
            tmp.Reserved = new byte[4];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	ワーク原点座標設定コマンド構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct WORGPOSCHG
    {
        public int AxisFlag;            // 軸選択フラグ
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] PosAxis;           // 第1軸～第9軸論理原点座標値
        public static WORGPOSCHG Init()
        {
            WORGPOSCHG tmp = new WORGPOSCHG();
            tmp.PosAxis = new int[9];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	ガイド貫通動作許可コマンド構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct GUIDETHROUGH
    {
        public short move;              // 移動許可ﾌﾗｸﾞ(0:不許可,1:許可)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Reserved;         // 予約
        public static GUIDETHROUGH Init()
        {
            GUIDETHROUGH tmp = new GUIDETHROUGH();
            tmp.Reserved = new byte[6];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	マシンロック有効／無効設定構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct MCHLOCK
    {
        public short enable;                // 有効/無効設定フラグ(0:無効、1:有効)
        public short move;              // 無効処理ﾌﾗｸﾞ(1:移動)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Reserved;         // 予約
        public static MCHLOCK Init()
        {
            MCHLOCK tmp = new MCHLOCK();
            tmp.Reserved = new byte[4];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	補正角度設定コマンド構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct ANGLESET
    {
        public int CorrAng;         // 補正角度(8-24ﾋﾞｯﾄ固定小数点)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Reserved;         // 予約
        public static ANGLESET Init()
        {
            ANGLESET tmp = new ANGLESET();
            tmp.Reserved = new byte[4];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	強制原点復帰完了設定コマンド構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct FORCEZRNFIN
    {
        public short AxisFlag;          // 軸選択フラグ
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Reserved;         // 予約
        public static FORCEZRNFIN Init()
        {
            FORCEZRNFIN tmp = new FORCEZRNFIN();
            tmp.Reserved = new byte[6];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	ドライラン設定(放電加工穴数指定)コマンド構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct DRYRUN_EX
    {
        public int DryRunEnN;           // ドライラン有効フラグ(放電加工穴数)
                                        // [0:ドライラン無効,-1:穴数指定無効]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Reserved;         // 予約
        public static DRYRUN_EX Init()
        {
            DRYRUN_EX tmp = new DRYRUN_EX();
            tmp.Reserved = new byte[4];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	ポンプ制御コマンド構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct PUMPCMND
    {
        public short PumpOut;           // ポンプ制御フラグ（0:OFF、1:ON）
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Reserved;         // 予約
        public static PUMPCMND Init()
        {
            PUMPCMND tmp = new PUMPCMND();
            tmp.Reserved = new byte[6];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	放電制御コマンド構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct BONCMND
    {
        public short BonOut;                // 放電制御フラグ（0:OFF、1:ON）
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Reserved;         // 予約
        public static BONCMND Init()
        {
            BONCMND tmp = new BONCMND();
            tmp.Reserved = new byte[6];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	自動モード中出力コマンド構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct AUTOMODE_OUTPUT
    {
        public short onflg;             // 自動モード中出力フラグ（0:OFF、1:ON）
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Reserved;         // 予約
        public static AUTOMODE_OUTPUT Init()
        {
            AUTOMODE_OUTPUT tmp = new AUTOMODE_OUTPUT();
            tmp.Reserved = new byte[6];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	シャットダウン開始コマンド構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct SHUTDOWN_START
    {
        public short startflg;          // ｼｬｯﾄﾀﾞｳﾝ開始ﾌﾗｸﾞ(0:停止,1:開始)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Reserved;         // 予約
        public static SHUTDOWN_START Init()
        {
            SHUTDOWN_START tmp = new SHUTDOWN_START();
            tmp.Reserved = new byte[6];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	ブザーON/OFF信号出力コマンド構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct BUZZER_ON_OUTPUT
    {
        public short onflg;             // ブザーON/OFFフラグ（0:OFF、1:ON）
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Reserved;         // 予約
        public static BUZZER_ON_OUTPUT Init()
        {
            BUZZER_ON_OUTPUT tmp = new BUZZER_ON_OUTPUT();
            tmp.Reserved = new byte[6];
            return tmp;
        }
    }
}
