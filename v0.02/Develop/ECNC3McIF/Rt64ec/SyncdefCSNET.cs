using System;
using System.Runtime.InteropServices;

class Syncdef
{
    // ========================================================================
    //	データ種別コード
    // ========================================================================
    public const short DAT_PARAMETER = 0x0; // サーボパラメータ	
    public const short DAT_PROGRAM = 0x1;   // 運転プログラム
    public const short DAT_STATUS = 0x2;    // サーボステータス
    public const short DAT_IODATA = 0x3;    // I/O信号状態
    public const short DAT_OPTIONPRM = 0x9; // オプションパラメータ
    public const short DAT_DNCDATA = 0xE;   // ＤＮＣデータ
    public const short DAT_DNCBUFI = 0xF;   // ＤＮＣバッファ情報
    public const short DAT_PITCHERR = 0x10; // ピッチエラー補正用パラメータ
    public const short DAT_SENSEPOS = 0x11; // センサーラッチ位置情報
    public const short DAT_TOOLHOFS = 0x12; // 工具長補正データ
    public const short DAT_FORCEIO = 0x13;  // 強制入出力ビットデータ
    public const short DAT_TEACHSTS = 0x18; // ティーチング情報
    public const short DAT_VARIABLE = 0x19; // マクロ変数データ
    public const short DAT_TOOLDIA = 0x1a;  // 工具径補正データ
    public const short DAT_PRGBLKINFO = 0x1b;   // プログラムブロック情報
    public const short DAT_RTC = 0x21;  // システム予約
    public const short DAT_ADDATA = 0x22;   // Ａ／Ｄ＆ＰＯＳ情報
    public const short DAT_ADLOG = 0x23;    // Ａ／Ｄ＆ＰＯＳロギング情報
    public const short DAT_TPCINFO = 0x24;  // ＴＰＣロギング情報
    public const short DAT_TPCDATA = 0x25;  // ＴＰＣロギングデータ
                                            //public const short					= 0x26;	// 
    public const short DAT_VERSION = 0x27;  // ＲＯＭバージョンデータ
    public const short DAT_AXNEGLECT = 0x28;    // 軸無効設定情報
    public const short DAT_AXINTLOCK = 0x29;    // 軸インタロック設定情報
    public const short DAT_TPCINFO_EX = 0x2a;   // ＴＰＣロギング情報
    public const short DAT_ONEBLOCK = 0x30; // プログラム１ブロックデータ
    public const short DAT_AXSVONEN = 0x31; // 軸サーボＯＮ有効設定情報
    public const short DAT_HANDLESTS = 0x32;    // 手パモード設定情報
    public const short DAT_MCRREG = 0x33;   // マクロ変数読み出し／書き込み
    public const short DAT_ECT_INFO = 0x34; // EtherCAT情報読み出し
    public const short DAT_ECT_MON = 0x35;  // EtherCAT送受信データ読み出し
                                            // public const short					= 0x36;
    public const short DAT_ACOPARAM = 0x37; // 補間前加減速ﾊﾟﾗﾒｰﾀ読出/書込
    public const short DAT_TOOLHSTS = 0x39; // 工具長補正設定情報
    public const short DAT_TOOLDSTS = 0x3a; // 工具径補正設定情報
    public const short DAT_POINTTABLE = 0x3b;   // 位置決めポイントテーブル読み出し／書き込み
    public const short DAT_ASSAXISTBL = 0x3c;   // 軸割り当て情報読み出し
    public const short DAT_TOOLDERR = 0x3d; // 工具径補正エラー情報
    public const short DAT_VERPER = 0x40;   // ＶＥＲ／ＰＥＲ値
    public const short DAT_SPREV = 0x41;    // 主軸回転数読出し
    public const short DAT_FBCOUNT = 0x50;  // フィードバックカウンタ積算値
    public const short DAT_ROMSWITCH = 0x60;    // ＳＸシステムＲＯＭスイッチデータ
    public const short DAT_OPTSWITCH = 0x60;    // オプションスイッチデータ
    public const short DAT_FORGROUND = 0x90;    // システム予約
    public const short DAT_CYCLETIME = 0x91;    /* 制御周期情報						*/

    public const short DAT_INITIALPRM = 0xa0;   /* 初期化パラメータ書込/読出		*/
    public const short DAT_PCONDITION = 0xa1;   /* 加工条件情報読出					*/
    public const short DAT_AECDATA = 0xa2;  /* ＡＥＣ状態読出					*/
    public const short DAT_WORKORG = 0xa4;  /* ワーク原点設定読出				*/
    public const short DAT_CORRECTDATA = 0xa6;  /* 各種補正データモニタ情報読出		*/
    public const short DAT_VIRPOS_EX = 0xa7;    /* 仮想点／測定点設定読出			*/

    public const short DAT_PRCSSKIPVPOSNO = 0xa9;   /* 放電加工スキップ時仮想点登録番号読出	*/
    public const short DAT_MESSAGEREQ = 0xad;   /* メッセージ表示要求データ読出		*/
    public const short DAT_MESSAGEANS = 0xae;   /* メッセージ表示結果データ書込		*/
    public const short DAT_ONEVARIABLE = 0xb1;  /* マクロ１変数書込/読出			*/
    public const short DAT_CAMERACMDREQ = 0xb2; /* カメラコマンド要求データ読出		*/

    public const short DAT_CAMERACMDANSF = 0xb5;    /* カメラコマンド結果浮動小数点数データ書込	*/
    public const short DAT_NVARIABLE_PRE = 0xb6;    /* マクロ変数任意数送受信ﾊﾟﾗﾒｰﾀ予約書込/読出*/
    public const short DAT_NVARIABLE_VAL = 0xb7;    /* マクロ変数データ任意数書込/読出			*/
    public const short DAT_VARIABLE_REQ = 0xb9; /* マクロ変数書込/読出要求データ書込/読出	*/
    public const short DAT_PCOND_TABLE = 0xba;  /* 加工条件テーブルデータ書込/読出	*/
    public const short DAT_ECNCEXTALM = 0xbb;   /* ECNC専用外部アラーム要因読出		*/
    public const short DAT_PITCHERR_AX = 0xbc;  /* 各軸ピッチエラー補正用パラメータ	*/

    // public const short  DAT_SRESERVED	= 0xC0;	// システム予約領域開始番号
    // public const short  DAT_ERESERVED	= 0xff;	// システム予約領域終了番号
    public const short DAT_FLASHROMINIT = 0xfd; // システム予約（ﾌﾗｯｼｭｼｽﾃﾑﾃﾞｰﾀ）
    public const short DAT_FLASHROM = 0xfe; // システム予約（ﾌﾗｯｼｭｼｽﾃﾑﾃﾞｰﾀ）
    public const short DAT_RESERVED = 0xFF; // システム予約


    // --------------------------------------------------------------------------
    //	コマンドコード
    // --------------------------------------------------------------------------
    public const short REQ_LOOPBACK_TEST = 0x00;        // ＤＰ通信 ループバックテスト
    public const short REQ_DATA_SEND = 0x01;        // ＤＰ通信 データ送信要求
    public const short REQ_DATA_RECEIVE = 0x02;     // ＤＰ通信 データ受信要求
    public const short REQ_MODECHG = 0x10;      // ﾓｰﾄﾞ変更
    public const short REQ_JOGSTART = 0x11;     // ＪＯＧ移動開始
    public const short REQ_ZRNSTART = 0x12;     // 原点復帰開始
    public const short REQ_STOP = 0x13;     // 移動停止
    public const short REQ_GENE = 0x14;     // ｼﾞｪﾈﾚｰｼｮﾝ
    public const short REQ_PTPSTART = 0x15;     // ＰＴＰ移動開始
    public const short REQ_PTPASTART = 0x16;        // ＰＴＰ移動開始（ABSO）
    public const short REQ_LINSTART = 0x17;     // 補間移動開始
    public const short REQ_LINASTART = 0x18;        // 補間移動開始（ABSO）
    public const short REQ_ORGSET = 0x19;       // 原点設定
    public const short REQ_RESET = 0x1A;        // ﾘｾｯﾄ
    public const short REQ_RESTART = 0x1B;      // 順行再開
    public const short REQ_PRGCLEAR = 0x1C;     // 内蔵ﾌﾟﾛｸﾞﾗﾑｸﾘｱ
    public const short REQ_OUTPUT = 0x1D;       // 汎用出力直接制御
    public const short REQ_SERVOON = 0x1E;      // ｻｰﾎﾞ電源ON
    public const short REQ_SERVOOFF = 0x1F;     // ｻｰﾎﾞ電源OFF
    public const short REQ_PROGSTRT = 0x20;     // ﾌﾟﾛｸﾞﾗﾑ実行開始
    public const short REQ_PROGSTOP = 0x21;     // ﾌﾟﾛｸﾞﾗﾑ実行停止
    public const short REQ_PROGSLCT = 0x22;     // 実行ﾌﾟﾛｸﾞﾗﾑ選択
    public const short REQ_ALLZRN = 0x24;       // 全軸原点復帰開始
    public const short REQ_ERCLEAR = 0x25;      // ＥＲクリア for SVM
    public const short REQ_SMPLSTRT = 0x26;     // サンプリング開始 for SVM
    public const short REQ_SMPLSTOP = 0x27;     // サンプリング停止 for SVM
    public const short REQ_SLINSTART = 0x28;        // 高速ｾﾝｻｰﾗｯﾁ補間移動開始
    public const short REQ_SLINASTART = 0x29;       // 高速ｾﾝｻｰﾗｯﾁ補間移動開始(ABSO)
    public const short REQ_COMPINPUT = 0x2A;        // 汎用入力強制制御（一括）
    public const short REQ_COMPOUTPUT = 0x2B;       // 汎用出力強制制御（一括）
    public const short REQ_COMPIOBIT = 0x2C;        // 汎用入出力強制制御（ビット）
    public const short REQ_OVRDCHGP = 0x2D;     // 軸速度ｵｰﾊﾞｰﾗｲﾄﾞ％値変更
    public const short REQ_ADLOG = 0x2E;        // Ａ／Ｄロギング要求
    public const short REQ_ADLOGCLR = 0x2F;     // Ａ／Ｄロギングバッファクリア要求
    public const short REQ_ZRNSHIFT = 0x30;     // 原点シフト要求
    public const short REQ_TPCSEL = 0x31;       // ＴＰＣデータ選択
    public const short REQ_TPCLOG = 0x32;       // ＴＰＣデータロギングＯＮ／ＯＦＦ
    public const short REQ_AXAUTOZRN = 0x33;        // 各軸自動原点復帰コマンド
    public const short REQ_PTPBSTART = 0x34;        // ＰＴＰ移動開始(機械座標系ABSO)
    public const short REQ_LINBSTART = 0x35;        // 補間移動開始(機械座標系ABSO)
    public const short REQ_CYCLE = 0x36;        // サイクル運転モード変更コマンド
    public const short REQ_COORDSET = 0x37;     // 座標系設定
    public const short REQ_AXISINTLK = 0x38;        // 軸インタロックコマンド
    public const short REQ_AXISNEG = 0x39;      // 軸ネグレクトコマンド
    public const short REQ_SVONOFF = 0x3A;      // 各軸サーボＯＮ／ＯＦＦ
    public const short REQ_TRQLIMCHG = 0x3B;        // トルク制限モード変更
    public const short REQ_AXCTRLCHG = 0x3C;        // 各軸制御モード変更
    public const short REQ_MCDOUT = 0x3D;       // Ｍコード出力
    public const short REQ_TPCSEL_EX = 0x3E;        // ＴＰＣデータ選択
    public const short REQ_PANEL = 0x40;        // システム予約
    public const short REQ_ROMSWGEN = 0x41;     // システム予約
    public const short REQ_TIMERRESET = 0x42;       // システム予約
    public const short REQ_OUTPUTBIT = 0x44;        // 汎用出力直接制御（ビット）
    public const short REQ_TRQCMD = 0x46;       // トルク指令
    public const short REQ_SYNEGPTPSTART = 0x49;        // 同期軸無視ＰＴＰ移動開始
    public const short REQ_SYNEGPTPASTART = 0x4a;       // 同期軸無視ＰＴＰ移動開始（ABSO）
    public const short REQ_SYNEGPTPBSTART = 0x4b;       // 同期軸無視ＰＴＰ移動開始（機械座標系ABSO）
    public const short REQ_SPCMND = 0x50;       // 主軸Ｄ／Ａ出力指令
    public const short REQ_SPREVSET = 0x51;     // 主軸回転数設定
    public const short REQ_SPINAX = 0x52;       // 回転軸回転動作指令
    public const short REQ_TLINE = 0x54;        // 接線制御ＯＮ／ＯＦＦ
    public const short REQ_HANDLE = 0x5c;       // 手パモードON/OFF設定
    public const short REQ_HANDLEKP = 0x5d;     // 手パモード倍率設定
    public const short REQ_HANDLEAXIS1 = 0x5e;      // 手パ有効軸１
    public const short REQ_HANDLEAXIS2 = 0x5f;      // 手パ有効軸２
    public const short REQ_SINGLE = 0x60;       // シングルステップモード
    public const short REQ_TEACH = 0x61;        // ティーチング
    public const short REQ_PRGINS = 0x62;       // プログラム挿入
    public const short REQ_PRGALT = 0x63;       // プログラム置換
    public const short REQ_PRGDEL = 0x64;       // プログラム削除
    public const short REQ_PRGREV = 0x65;       // プログラム逆行運転
    public const short REQ_STEPCHG = 0x66;      // プログラムステップ変更
    public const short REQ_STEPSKIP = 0x67;     // プログラムステップスキップ
    public const short REQ_AXMV = 0x68;     // 独立位置決めインクレ指令
    public const short REQ_AXMVA = 0x69;        // 独立位置決め論理座標系アブソ指令
    public const short REQ_AXMVB = 0x6a;        // 独立位置決め機械座標系アブソ指令
    public const short REQ_PRGBLKMV = 0x6B;     // プログラムブロック移動
    public const short REQ_PRGBLKCPY = 0x6C;        // プログラムブロックコピー
    public const short REQ_PRGBLKDEL = 0x6D;        // プログラムブロック削除
    public const short REQ_REFMEM = 0x6E;       // Flashﾒﾓﾘへ運転ﾌﾟﾛｸﾞﾗﾑ反映指令
    public const short REQ_REFPRM = 0x6F;       // Flashﾒﾓﾘへパラメータ反映指令
    public const short REQ_FBSETUP = 0x78;      // ＦＢカウンタ初期設定
    public const short REQ_TASKSTART = 0x80;        // ﾏﾙﾁﾀｽｸﾌﾟﾛｸﾞﾗﾑ開始
    public const short REQ_TASKSTOP = 0x81;     // ﾏﾙﾁﾀｽｸﾌﾟﾛｸﾞﾗﾑ停止
    public const short REQ_TASKRESET = 0x82;        // ﾏﾙﾁﾀｽｸﾌﾟﾛｸﾞﾗﾑﾘｾｯﾄ
    public const short REQ_HOME = 0x83;     // homepos移動
    public const short REQ_MCRREG = 0x84;       // マクロ変数書き込みコマンド
    public const short REQ_COVRDCHGP = 0x85;        // 補間オーバーライド％値変更
    public const short REQ_SOVRDCHGP = 0x86;        // 主軸オーバーライド％値変更
    public const short REQ_MABSCOORDSET = 0x90;     // アブソ座標設定コマンド

    public const short REQ_OPTSTOP = 0xa0;      /* ｵﾌﾟｼｮﾅﾙｽﾄｯﾌﾟ有効/無効ｺﾏﾝﾄﾞ		*/
    public const short REQ_TOUCHSENSE = 0xa1;       /* 接触感知有効/無効コマンド		*/
    public const short REQ_WTOPPOS = 0xa2;      /* Ｗ軸上限値設定コマンド			*/
    public const short REQ_RETURN = 0xa3;       /* 返送コマンド						*/
    public const short REQ_PNOSEL = 0xa4;       /* 加工条件番号変更コマンド			*/
    public const short REQ_DRYRUN = 0xa5;       /* ドライラン有効/無効コマンド		*/
    public const short REQ_ELCTDCHGEN = 0xa6;       /* 電極交換有効/無効コマンド		*/
    public const short REQ_INITIALSET = 0xa7;       /* InitialSet有効/無効コマンド		*/
    public const short REQ_GUIDETHROUGH = 0xa8;     /* ガイド貫通動作許可コマンド		*/

    public const short REQ_PROGSTRTN = 0xad;        /* プログラム実行開始コマンド		*/
    public const short REQ_MOVEPRGSTOPPOS = 0xae;       /* ﾌﾟﾛｸﾞﾗﾑ実行停止位置移動開始ｺﾏﾝﾄﾞ	*/
    public const short REQ_PARTITIONCHG = 0xaf;     /* パーティション設定コマンド		*/
    public const short REQ_ELCTDNOCHG = 0xb0;       /* 電極番号設定コマンド				*/
    public const short REQ_ELCTDINSTALL = 0xb1;     /* 電極装着/脱着動作開始コマンド	*/
    public const short REQ_ELCTDEXCHPOS = 0xb2;     /* 電極交換位置移動コマンド			*/

    public const short REQ_ESFMAGAZINE_INC = 0xb4;      /* ESFﾏｶﾞｼﾞﾝｲﾝｸﾘﾒﾝﾄｺﾏﾝﾄﾞ			*/
    public const short REQ_MOVEESFARM = 0xb5;       /* ESFｱｰﾑ移動（前端/中間/後端）		*/
    public const short REQ_OPENESFFINGER = 0xb6;        /* ESFﾌｨﾝｶﾞｰOPEN/CLOSE				*/
    public const short REQ_GUIDENOCHG = 0xb7;       /* ガイド番号設定コマンド			*/
    public const short REQ_GUIDEINSTALL = 0xb8;     /* ガイド装着/脱着動作開始コマンド	*/
    public const short REQ_GUIDEEXCHPOS = 0xb9;     /* ガイド交換位置移動				*/
    public const short REQ_MOVEGSFARM = 0xbb;       /* ＧＳＦアーム移動（前端/後端）	*/
    public const short REQ_GUIDECLUMP = 0xbc;       /* ガイドクランプ/アンクランプ		*/
    public const short REQ_SPCLUMP = 0xbd;      /* スピンドルクランプ/アンクランプ	*/

    public const short REQ_GUIDECHKPOS = 0xbf;      /* ガイド確認位置移動コマンド		*/

    public const short REQ_ESFMAGAZINE_MOV = 0xc1;      /* ＥＳＦマガジン移動コマンド		*/
    public const short REQ_INCREFSET_MOV = 0xc2;        /* 相対測定点設定時軸移動有効ｺﾏﾝﾄﾞ	*/
    public const short REQ_PTPASTART_W = 0xc3;      /* W軸上限待避有効ＰＴＰ移動開始(論理座標系ABSO)	*/
    public const short REQ_PTPBSTART_W = 0xc4;      /* W軸上限待避有効ＰＴＰ移動開始(機械座標系ABSO)	*/
    public const short REQ_WORGPOSCHG = 0xc5;       /* ワーク原点座標設定コマンド		*/
    public const short REQ_PATROUNDSTOP = 0xc6;     /* ﾊﾟｰﾃｨｼｮﾝ内１周停止有効コマンド	*/
    public const short REQ_XY_ILOCK = 0xc7;     /* X/Y軸ｲﾝﾀｰﾛｯｸ有効/無効コマンド	*/
    public const short REQ_MCHLOCK = 0xc8;      /* マシンロック有効/無効コマンド	*/
    public const short REQ_CORR_ANG = 0xc9;     /* ﾌﾟﾛｸﾞﾗﾑ開始時角度補正有効/無効ｺﾏﾝﾄﾞ	*/
    public const short REQ_BLOCKSKIP = 0xca;        /* BlockSkip有効/無効コマンド		*/
    public const short REQ_ANGLESET = 0xcb;     /* 補正角度設定コマンド				*/
    public const short REQ_GDTHROUGH = 0xcc;        /* ガイド貫通動作開始コマンド		*/
    public const short REQ_VIRPOSCHG_EX = 0xcd;     /* 仮想点／測定点変更コマンド		*/

    public const short REQ_HANDLEPERMIT = 0xcf;     /* 手動パルサー許可コマンド			*/

    public const short REQ_FORCEZRNFIN = 0xd1;      /* 強制原点復帰完了設定コマンド		*/
    public const short REQ_DRYRUN_EX = 0xd2;        /* ドライラン設定(放電加工穴数指定)コマンド	*/

    public const short REQ_PUMPCMND = 0xd5;     /* ポンプ制御コマンド				*/
    public const short REQ_BONCMND = 0xd6;      /* 放電制御コマンド					*/
    public const short REQ_PRCSSKIP = 0xd7;     /* 放電加工スキップコマンド			*/
    public const short REQ_AUTOMODE_OUTPUT = 0xd8;      /* 自動モード中出力コマンド			*/
    public const short REQ_SHUTDOWN_START = 0xd9;       /* シャットダウン開始コマンド		*/
    public const short REQ_BUZZER_ON_OUTPUT = 0xda;     /* ブザーON/OFF信号出力コマンド		*/
    public const short REQ_PITCHERR_CLR = 0xdb;     /* ピッチエラー補正用パラメータクリア	*/

    // public const short  REQ_SRESERVED	= 0xE0;		// システム予約領域開始番号
    // public const short  REQ_ERESERVED	= 0xff;		// システム予約領域終了番号

    // ========================================================================
    //	エラーコード
    // ========================================================================
    public const int E_OK = 0;      // エラーなし
    public const int E_DEVNRDY = 1;     // デバイス未初期化
    public const int E_PARAM = 2;       // パラメータエラー
    public const int E_TIME = 3;        // 通信タイムアウト
    public const int E_RTRY = 4;        // リトライオーバー
    public const int E_MLTRTRY = 5;     // 多重リトライ
    public const int E_HARDER = 6;      // 通信エラー
    public const int E_NEXIST = 7;      // 要求データが存在しない
    public const int E_PROTECT = 8;     // データ受信不可状態
    public const int E_SEQ = 9;     // 通信シーケンスエラー
    public const int E_PRGTERM = 10;        // プログラム受信中断
    public const int E_PRGBUFF = 11;        // プログラムバッファフル
    public const int E_CMDNOT = 12;     // コマンド実行不可
    public const int E_EMPTYHANDLE = 13;        // 空き通信ハンドル無し
    public const int E_NOHANDLE = 14;       // 無効ハンドル
    public const int E_BUSY = 15;       // 通信ビジー
    public const int E_PRMWRITE = 16;       // パラメータ書込エラー
    public const int E_PRGSTOPPOS = 17;     // ﾌﾟﾛｸﾞﾗﾑ停止位置でない

    public const int E_USERDEF = 256;       // ユーザー定義エラーコード領域開始

    public const int E_ERR = -1;		// 不明エラー

}