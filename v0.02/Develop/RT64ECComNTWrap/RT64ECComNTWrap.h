#pragma once
#pragma pack(4)

// ========================================================================
//	データ種別コード
// ========================================================================
const short  DAT_PARAMETER = 0x0;	// サーボパラメータ	
const short  DAT_PROGRAM = 0x1;	// 運転プログラム
const short  DAT_STATUS = 0x2;	// サーボステータス
const short  DAT_IODATA = 0x3;	// I/O信号状態
const short  DAT_OPTIONPRM = 0x9;	// オプションパラメータ
const short  DAT_DNCDATA = 0xE;	// ＤＮＣデータ
const short  DAT_DNCBUFI = 0xF;	// ＤＮＣバッファ情報
const short  DAT_PITCHERR = 0x10;	// ピッチエラー補正用パラメータ
const short  DAT_SENSEPOS = 0x11;	// センサーラッチ位置情報
const short  DAT_TOOLHOFS = 0x12;	// 工具長補正データ
const short  DAT_FORCEIO = 0x13;	// 強制入出力ビットデータ
const short  DAT_TEACHSTS = 0x18;	// ティーチング情報
const short  DAT_VARIABLE = 0x19;	// マクロ変数データ
const short  DAT_TOOLDIA = 0x1a;	// 工具径補正データ
const short  DAT_PRGBLKINFO = 0x1b;	// プログラムブロック情報
const short  DAT_RTC = 0x21;	// システム予約
const short  DAT_ADDATA = 0x22;	// Ａ／Ｄ＆ＰＯＳ情報
const short  DAT_ADLOG = 0x23;	// Ａ／Ｄ＆ＰＯＳロギング情報
const short  DAT_TPCINFO = 0x24;	// ＴＰＣロギング情報
const short  DAT_TPCDATA = 0x25;	// ＴＰＣロギングデータ
								//public const short					= 0x26;	// 
const short  DAT_VERSION = 0x27;	// ＲＯＭバージョンデータ
const short  DAT_AXNEGLECT = 0x28;	// 軸無効設定情報
const short  DAT_AXINTLOCK = 0x29;	// 軸インタロック設定情報
const short  DAT_TPCINFO_EX = 0x2a;	// ＴＰＣロギング情報
const short  DAT_ONEBLOCK = 0x30;	// プログラム１ブロックデータ
const short  DAT_AXSVONEN = 0x31;	// 軸サーボＯＮ有効設定情報
const short  DAT_HANDLESTS = 0x32;	// 手パモード設定情報
const short  DAT_MCRREG = 0x33;	// マクロ変数読み出し／書き込み
const short  DAT_ECT_INFO = 0x34;	// EtherCAT情報読み出し
const short  DAT_ECT_MON = 0x35;	// EtherCAT送受信データ読み出し
										// public const short					= 0x36;
const short  DAT_ACOPARAM = 0x37;	// 補間前加減速ﾊﾟﾗﾒｰﾀ読出/書込
const short  DAT_TOOLHSTS = 0x39;	// 工具長補正設定情報
const short  DAT_TOOLDSTS = 0x3a;	// 工具径補正設定情報
const short  DAT_POINTTABLE = 0x3b;	// 位置決めポイントテーブル読み出し／書き込み
const short  DAT_ASSAXISTBL = 0x3c;	// 軸割り当て情報読み出し
const short  DAT_TOOLDERR = 0x3d;	// 工具径補正エラー情報
const short  DAT_VERPER = 0x40;	// ＶＥＲ／ＰＥＲ値
const short  DAT_SPREV = 0x41;	// 主軸回転数読出し
const short  DAT_FBCOUNT = 0x50;	// フィードバックカウンタ積算値
const short  DAT_ROMSWITCH = 0x60;	// ＳＸシステムＲＯＭスイッチデータ
const short  DAT_OPTSWITCH = 0x60;	// オプションスイッチデータ
const short  DAT_FORGROUND = 0x90;	// システム予約

const short  DAT_INITIALPRM = 0xa0;	/* 初期化パラメータ書込/読出		*/
const short  DAT_PCONDITION = 0xa1;	/* 加工条件情報読出					*/
const short  DAT_AECDATA = 0xa2;	/* ＡＥＣ状態読出					*/
const short  DAT_WORKORG = 0xa4;	/* ワーク原点設定読出				*/
const short  DAT_CORRECTDATA = 0xa6;	/* 各種補正データモニタ情報読出		*/
const short  DAT_VIRPOS_EX = 0xa7;	/* 仮想点／測定点設定読出			*/
const short  DAT_SERVOPCOND = 0xa8;	/* サーボ加工条件データ書込/読出	*/
const short  DAT_PRCSSKIPVPOSNO = 0xa9;	/* 放電加工スキップ時仮想点登録番号読出	*/
const short  DAT_MESSAGEREQ = 0xad;	/* メッセージ表示要求データ読出		*/
const short  DAT_MESSAGEANS = 0xae;	/* メッセージ表示結果データ書込		*/
const short  DAT_ONEVARIABLE = 0xb1;	/* マクロ１変数書込/読出			*/
const short  DAT_CAMERACMDREQ = 0xb2;	/* カメラコマンド要求データ読出		*/

const short  DAT_CAMERACMDANSF = 0xb5;	/* カメラコマンド結果浮動小数点数データ書込	*/
const short  DAT_NVARIABLE_PRE = 0xb6;	/* マクロ変数任意数送受信ﾊﾟﾗﾒｰﾀ予約書込/読出*/
const short  DAT_NVARIABLE_VAL = 0xb7;	/* マクロ変数データ任意数書込/読出			*/
const short  DAT_VARIABLE_REQ = 0xb9;	/* マクロ変数書込/読出要求データ書込/読出	*/
const short  DAT_PCOND_TABLE = 0xba;	/* 加工条件テーブルデータ書込/読出	*/
												// public const short  DAT_SRESERVED	= 0xC0;	// システム予約領域開始番号
												// public const short  DAT_ERESERVED	= 0xff;	// システム予約領域終了番号
const short  DAT_FLASHROMINIT = 0xfd;	// システム予約（ﾌﾗｯｼｭｼｽﾃﾑﾃﾞｰﾀ）
const short  DAT_FLASHROM = 0xfe;	// システム予約（ﾌﾗｯｼｭｼｽﾃﾑﾃﾞｰﾀ）
const short  DAT_RESERVED = 0xFF;	// システム予約


									// --------------------------------------------------------------------------
									//	コマンドコード
									// --------------------------------------------------------------------------
const short  REQ_LOOPBACK_TEST = 0x00;		// ＤＰ通信 ループバックテスト
const short  REQ_DATA_SEND = 0x01;		// ＤＰ通信 データ送信要求
const short  REQ_DATA_RECEIVE = 0x02;		// ＤＰ通信 データ受信要求
const short  REQ_MODECHG = 0x10;		// ﾓｰﾄﾞ変更
const short  REQ_JOGSTART = 0x11;		// ＪＯＧ移動開始
const short  REQ_ZRNSTART = 0x12;		// 原点復帰開始
const short  REQ_STOP = 0x13;		// 移動停止
const short  REQ_GENE = 0x14;		// ｼﾞｪﾈﾚｰｼｮﾝ
const short  REQ_PTPSTART = 0x15;		// ＰＴＰ移動開始
const short  REQ_PTPASTART = 0x16;		// ＰＴＰ移動開始（ABSO）
const short  REQ_LINSTART = 0x17;		// 補間移動開始
const short  REQ_LINASTART = 0x18;		// 補間移動開始（ABSO）
const short  REQ_ORGSET = 0x19;		// 原点設定
const short  REQ_RESET = 0x1A;		// ﾘｾｯﾄ
const short  REQ_RESTART = 0x1B;		// 順行再開
const short  REQ_PRGCLEAR = 0x1C;		// 内蔵ﾌﾟﾛｸﾞﾗﾑｸﾘｱ
const short  REQ_OUTPUT = 0x1D;		// 汎用出力直接制御
const short  REQ_SERVOON = 0x1E;		// ｻｰﾎﾞ電源ON
const short  REQ_SERVOOFF = 0x1F;		// ｻｰﾎﾞ電源OFF
const short  REQ_PROGSTRT = 0x20;		// ﾌﾟﾛｸﾞﾗﾑ実行開始
const short  REQ_PROGSTOP = 0x21;		// ﾌﾟﾛｸﾞﾗﾑ実行停止
const short  REQ_PROGSLCT = 0x22;		// 実行ﾌﾟﾛｸﾞﾗﾑ選択
const short  REQ_ALLZRN = 0x24;		// 全軸原点復帰開始
const short  REQ_ERCLEAR = 0x25;		// ＥＲクリア for SVM
const short  REQ_SMPLSTRT = 0x26;		// サンプリング開始 for SVM
const short  REQ_SMPLSTOP = 0x27;		// サンプリング停止 for SVM
const short  REQ_SLINSTART = 0x28;		// 高速ｾﾝｻｰﾗｯﾁ補間移動開始
const short  REQ_SLINASTART = 0x29;		// 高速ｾﾝｻｰﾗｯﾁ補間移動開始(ABSO)
const short  REQ_COMPINPUT = 0x2A;		// 汎用入力強制制御（一括）
const short  REQ_COMPOUTPUT = 0x2B;		// 汎用出力強制制御（一括）
const short  REQ_COMPIOBIT = 0x2C;		// 汎用入出力強制制御（ビット）
const short  REQ_OVRDCHGP = 0x2D;		// 軸速度ｵｰﾊﾞｰﾗｲﾄﾞ％値変更
const short  REQ_ADLOG = 0x2E;		// Ａ／Ｄロギング要求
const short  REQ_ADLOGCLR = 0x2F;		// Ａ／Ｄロギングバッファクリア要求
const short  REQ_ZRNSHIFT = 0x30;		// 原点シフト要求
const short  REQ_TPCSEL = 0x31;		// ＴＰＣデータ選択
const short  REQ_TPCLOG = 0x32;		// ＴＰＣデータロギングＯＮ／ＯＦＦ
const short  REQ_AXAUTOZRN = 0x33;		// 各軸自動原点復帰コマンド
const short  REQ_PTPBSTART = 0x34;		// ＰＴＰ移動開始(機械座標系ABSO)
const short  REQ_LINBSTART = 0x35;		// 補間移動開始(機械座標系ABSO)
const short  REQ_CYCLE = 0x36;		// サイクル運転モード変更コマンド
const short  REQ_COORDSET = 0x37;		// 座標系設定
const short  REQ_AXISINTLK = 0x38;		// 軸インタロックコマンド
const short  REQ_AXISNEG = 0x39;		// 軸ネグレクトコマンド
const short  REQ_SVONOFF = 0x3A;		// 各軸サーボＯＮ／ＯＦＦ
const short  REQ_TRQLIMCHG = 0x3B;		// トルク制限モード変更
const short  REQ_AXCTRLCHG = 0x3C;		// 各軸制御モード変更
const short  REQ_MCDOUT = 0x3D;		// Ｍコード出力
const short  REQ_TPCSEL_EX = 0x3E;		// ＴＰＣデータ選択
const short  REQ_PANEL = 0x40;		// システム予約
const short  REQ_ROMSWGEN = 0x41;		// システム予約
const short  REQ_TIMERRESET = 0x42;		// システム予約
const short  REQ_OUTPUTBIT = 0x44;		// 汎用出力直接制御（ビット）
const short  REQ_TRQCMD = 0x46;		// トルク指令
const short  REQ_SYNEGPTPSTART = 0x49;		// 同期軸無視ＰＴＰ移動開始
const short  REQ_SYNEGPTPASTART = 0x4a;		// 同期軸無視ＰＴＰ移動開始（ABSO）
const short  REQ_SYNEGPTPBSTART = 0x4b;		// 同期軸無視ＰＴＰ移動開始（機械座標系ABSO）
const short  REQ_SPCMND = 0x50;		// 主軸Ｄ／Ａ出力指令
const short  REQ_SPREVSET = 0x51;		// 主軸回転数設定
const short  REQ_SPINAX = 0x52;		// 回転軸回転動作指令
const short  REQ_TLINE = 0x54;		// Ｚ軸接線制御ＯＮ／ＯＦＦ
const short  REQ_HANDLE = 0x5c;		// 手パモードON/OFF設定
const short  REQ_HANDLEKP = 0x5d;		// 手パモード倍率設定
const short  REQ_HANDLEAXIS1 = 0x5e;		// 手パ有効軸１
const short  REQ_HANDLEAXIS2 = 0x5f;		// 手パ有効軸２
const short  REQ_SINGLE = 0x60;		// シングルステップモード
const short  REQ_TEACH = 0x61;		// ティーチング
const short  REQ_PRGINS = 0x62;		// プログラム挿入
const short  REQ_PRGALT = 0x63;		// プログラム置換
const short  REQ_PRGDEL = 0x64;		// プログラム削除
const short  REQ_PRGREV = 0x65;		// プログラム逆行運転
const short  REQ_STEPCHG = 0x66;		// プログラムステップ変更
const short  REQ_STEPSKIP = 0x67;		// プログラムステップスキップ
const short  REQ_AXMV = 0x68;		// 独立位置決めインクレ指令
const short  REQ_AXMVA = 0x69;		// 独立位置決め論理座標系アブソ指令
const short  REQ_AXMVB = 0x6a;		// 独立位置決め機械座標系アブソ指令
const short  REQ_PRGBLKMV = 0x6B;		// プログラムブロック移動
const short  REQ_PRGBLKCPY = 0x6C;		// プログラムブロックコピー
const short  REQ_PRGBLKDEL = 0x6D;		// プログラムブロック削除
const short  REQ_REFMEM = 0x6E;		// Flashﾒﾓﾘへ運転ﾌﾟﾛｸﾞﾗﾑ反映指令
const short  REQ_FBSETUP = 0x78;		// ＦＢカウンタ初期設定
const short  REQ_TASKSTART = 0x80;		// ﾏﾙﾁﾀｽｸﾌﾟﾛｸﾞﾗﾑ開始
const short  REQ_TASKSTOP = 0x81;		// ﾏﾙﾁﾀｽｸﾌﾟﾛｸﾞﾗﾑ停止
const short  REQ_TASKRESET = 0x82;		// ﾏﾙﾁﾀｽｸﾌﾟﾛｸﾞﾗﾑﾘｾｯﾄ
const short  REQ_HOME = 0x83;		// homepos移動
const short  REQ_MCRREG = 0x84;		// マクロ変数書き込みコマンド
const short  REQ_COVRDCHGP = 0x85;		// 補間オーバーライド％値変更
const short  REQ_SOVRDCHGP = 0x86;		// 主軸オーバーライド％値変更
const short  REQ_MABSCOORDSET = 0x90;		// アブソ座標設定コマンド

const short  REQ_OPTSTOP = 0xa0;		/* ｵﾌﾟｼｮﾅﾙｽﾄｯﾌﾟ有効/無効ｺﾏﾝﾄﾞ		*/
const short  REQ_TOUCHSENSE = 0xa1;		/* 接触感知有効/無効コマンド		*/
const short  REQ_WTOPPOS = 0xa2;		/* Ｗ軸上限値設定コマンド			*/
const short  REQ_RETURN = 0xa3;		/* 返送コマンド						*/
const short  REQ_PNOSEL = 0xa4;		/* 加工条件番号変更コマンド			*/
const short  REQ_DRYRUN = 0xa5;		/* ドライラン有効/無効コマンド		*/
const short  REQ_ELCTDCHGEN = 0xa6;		/* 電極交換有効/無効コマンド		*/
const short  REQ_INITIALSET = 0xa7;		/* InitialSet有効/無効コマンド		*/
const short  REQ_GUIDETHROUGH = 0xa8;		/* ガイド貫通動作許可コマンド		*/

const short  REQ_PROGSTRTN = 0xad;		/* プログラム実行開始コマンド		*/
const short  REQ_MOVEPRGSTOPPOS = 0xae;		/* ﾌﾟﾛｸﾞﾗﾑ実行停止位置移動開始ｺﾏﾝﾄﾞ	*/
const short  REQ_PARTITIONCHG = 0xaf;		/* パーティション設定コマンド		*/
const short  REQ_ELCTDNOCHG = 0xb0;		/* 電極番号設定コマンド				*/
const short  REQ_ELCTDINSTALL = 0xb1;		/* 電極装着/脱着動作開始コマンド	*/
const short  REQ_ELCTDEXCHPOS = 0xb2;		/* 電極交換位置移動コマンド			*/

const short  REQ_ESFMAGAZINE_INC = 0xb4;		/* ESFﾏｶﾞｼﾞﾝｲﾝｸﾘﾒﾝﾄｺﾏﾝﾄﾞ			*/
const short  REQ_MOVEESFARM = 0xb5;		/* ESFｱｰﾑ移動（前端/中間/後端）		*/
const short  REQ_OPENESFFINGER = 0xb6;		/* ESFﾌｨﾝｶﾞｰOPEN/CLOSE				*/
const short  REQ_GUIDENOCHG = 0xb7;		/* ガイド番号設定コマンド			*/
const short  REQ_GUIDEINSTALL = 0xb8;		/* ガイド装着/脱着動作開始コマンド	*/
const short  REQ_GUIDEEXCHPOS = 0xb9;		/* ガイド交換位置移動				*/
const short  REQ_MOVEGSFARM = 0xbb;		/* ＧＳＦアーム移動（前端/後端）	*/
const short  REQ_GUIDECLUMP = 0xbc;		/* ガイドクランプ/アンクランプ		*/
const short  REQ_SPCLUMP = 0xbd;		/* スピンドルクランプ/アンクランプ	*/

const short  REQ_GUIDECHKPOS = 0xbf;		/* ガイド確認位置移動コマンド		*/

const short  REQ_ESFMAGAZINE_MOV = 0xc1;		/* ＥＳＦマガジン移動コマンド		*/
const short  REQ_INCREFSET_MOV = 0xc2;		/* 相対測定点設定時軸移動有効ｺﾏﾝﾄﾞ	*/
const short  REQ_PTPASTART_W = 0xc3;		/* W軸上限待避有効ＰＴＰ移動開始(論理座標系ABSO)	*/
const short  REQ_PTPBSTART_W = 0xc4;		/* W軸上限待避有効ＰＴＰ移動開始(機械座標系ABSO)	*/
const short  REQ_WORGPOSCHG = 0xc5;		/* ワーク原点座標設定コマンド		*/
const short  REQ_PATROUNDSTOP = 0xc6;		/* ﾊﾟｰﾃｨｼｮﾝ内１周停止有効コマンド	*/
const short  REQ_XY_ILOCK = 0xc7;		/* X/Y軸ｲﾝﾀｰﾛｯｸ有効/無効コマンド	*/
const short  REQ_MCHLOCK = 0xc8;		/* マシンロック有効/無効コマンド	*/
const short  REQ_CORR_ANG = 0xc9;		/* ﾌﾟﾛｸﾞﾗﾑ開始時角度補正有効/無効ｺﾏﾝﾄﾞ	*/
const short  REQ_BLOCKSKIP = 0xca;		/* BlockSkip有効/無効コマンド		*/
const short  REQ_ANGLESET = 0xcb;		/* 補正角度設定コマンド				*/
const short  REQ_GDTHROUGH = 0xcc;		/* ガイド貫通動作開始コマンド		*/
const short  REQ_VIRPOSCHG_EX = 0xcd;		/* 仮想点／測定点変更コマンド		*/

const short  REQ_HANDLEPERMIT = 0xcf;		/* 手動パルサー許可コマンド			*/
const short  REQ_PRCFEED = 0xd0;		/* 加工条件による主軸速度通知コマンド	*/
const short  REQ_FORCEZRNFIN = 0xd1;		/* 強制原点復帰完了設定コマンド		*/
const short  REQ_DRYRUN_EX = 0xd2;		/* ドライラン設定(放電加工穴数指定)コマンド	*/

const short  REQ_PUMPCMND = 0xd5;		/* ポンプ制御コマンド				*/
const short  REQ_BONCMND = 0xd6;		/* 放電制御コマンド					*/
const short  REQ_PRCSSKIP = 0xd7;		/* 放電加工スキップコマンド			*/
const short  REQ_AUTOMODE_OUTPUT = 0xd8;		/* 自動モード中出力コマンド			*/
const short  REQ_SHUTDOWN_START = 0xd9;		/* シャットダウン開始コマンド		*/
const short  REQ_BUZZER_ON_OUTPUT = 0xda;		/* ブザーON/OFF信号出力コマンド		*/

typedef struct
{
	int nSize;			// 通信初期化構造体サイズ
	short fComType;		// 通信種別フラグ
	short fShare;		// 共有フラグ
	int fLogging;		// 通信ロギングフラグ
	char* pLogFile;		// 通信ロギングファイル名
	short pNodeName;	// INtimeノード名
	short Reserved[2];	// 予約
}SXDEF, *PSXDEF;

/************************************************************************/
/* ＲＯＭＳＷパラメータ構造体（ＰＣ解放用） */
/************************************************************************/
typedef struct
{ /* ROMSWパラメータ構造体（２５６００ﾊﾞｲﾄ固定長） */
	_int64 st_en; /* 有効局フラグ(物理局フラグ) */
	_int64 axis_en; /* 有効軸フラグ(物理局フラグ) */
	_int64 io_en; /* 有効IOフラグ(物理局フラグ) */
	unsigned char Reserved1[20496]; /* 予約*/
	_int64 spindle_ax; /* 主軸有効軸フラグ*/
	unsigned char Reserved2[5072]; /* 予約*/
} ROMSW;

/************************************************************************/
/* 初期化パラメータ構造体*/
/************************************************************************/
typedef struct
{
	long	ElctdExchPos[9];			// 電極交換位置
	long		ElctdExchSpdW;			// Ｗ軸電極交換位置移動速度
	long		ElctdExchSpdW1;			// Ｗ軸電極交換前位置移動速度
	long		ElctdExchOfsW1;			// Ｗ軸電極交換前位置オフセット
	long		ElctdExchOfsW2;			// Ｗ軸電極交換待機位置オフセット

	long	ElctdChkSpdZ1[6];			// 電極装着後のセンサーまでのＺ軸下降速度

	long	ElctdChkSpdZ2[6];			// 電極装着後のセンサーからのＺ軸下降速度

	long	ElctdChkSpdS2[6];			// 電極装着後のセンサーからの主軸回転速度

	long	ElctdChkOfsZ[6];			// 電極装着後のセンサーからのＺ軸下降量
	long		ElctdDeplUpZ;			// 電極消耗(Z-OTｵﾝ)／途中停止時のＺ軸上昇量
	short	ElctdNum;				// 電極数
	short	GuideNum;				// ガイド数

	long	GuideExchPosS[9];			// ガイド交換位置始点

	long	GuideExchPosE[9];			// ガイド交換位置終点

	long	GuideChkPos[9];			// ガイド有無センサー位置
	long		GuideExchSpdW;			// Ｗ軸ガイド交換位置移動速度
	long		GuideExchOfsW1;			// Ｗ軸ガイド交換前位置オフセット
	long		GuideExchOfsW2;			// Ｗ軸ガイド交換待機位置オフセット

	long	EdgeSrchOfs[9];			// 端面位置だし時の＋もしくは－分移動量

	long	EdgeSrchTouchSpd1[9];		// 端面位置だし時の１度目の接触速度

	long	EdgeSrchTouchRet1[9];		// 端面位置だし時の１度目の戻り量

	long	EdgeSrchTouchSpd2[9];		// 端面位置だし時の２，３度目の接触速度

	long	EdgeSrchTouchRet2[9];		// 端面位置だし時の２，３度目の戻り量
	long		EdgeSrchZDwnSpd;		// 端面位置だし時のＺ軸下降速度
	long		TouchSenseTime;			// 端面位置だし時接触感知時間
	long		RefTouchUpZ;			// 測定点Ｚ接触後のＺＵＰ量設定
	long		PrcsRetSpdZ;			// 放電加工終了時Ｚ軸上昇速度
	long		EdgeSrchZUpSpd;			// 端面位置だし時のＺ軸上昇速度
	long		ElctdClumpSpdS;			// 電極装着(ｺﾚｯﾄｸﾗﾝﾌﾟ)までの主軸回転速度
	long		BucklingUpOfsZ;			// 細線電極確認時の座屈ｾﾝｻｰONによるZ上昇量
	long		BucklingUpSpdZ;			// 細線電極確認時の座屈ｾﾝｻｰONによるZ上昇速度
	short	BucklingRetry;			// 細線電極確認時の座屈ｾﾝｻｰONによるﾘﾄﾗｲ回数
	short	AecEnable;				// ＡＥＣ有効/無効(D0:ESF、D1:GSF)
	long		Z20ErrOfs;				// Ｚ２０エラー検出オフセット
	short	McnType;				// 機械タイプ（0:通常機、1:油仕様機）
	short	GuideSensorDis;			// ガイド貫通検出センサー無効設定

	long	CysCheckDis[4];			// CYSチェック無効設定
									// 	CysCheckDis[0] : D00:  1 ～ D31: 32
									// 	CysCheckDis[1] : D00: 33 ～ D31: 64
									// 	CysCheckDis[2] : D00: 65 ～ D31: 96
									// 	CysCheckDis[3] : D00: 97 ～ D31:128
	short	AxAecActDis;			// 電極・ガイド交換動作無効軸設定(A/B/C軸のみ)
	short	AxBrakeEn;				// ブレーキ軸設定(A/B/C軸のみ)
	short	BrakeTimer;				// ブレーキ作動時間[msec]
	short	TchErrCancelTime;		// 接触感知エラーキャンセル時間

	long	CylPulseOut[4];			// CYLパルス出力設定
									// 	CylPulseOut[0] : D00:  1 ～ D31: 32
									// 	CylPulseOut[1] : D00: 33 ～ D31: 64
									// 	CylPulseOut[2] : D00: 65 ～ D31: 96
									// 	CylPulseOut[3] : D00: 97 ～ D31:128
	short	CylPulseTime;			// CYLパルス幅[msec]
	short	PrcsFirstP99xDis;		// 放電加工時FirstSparkまでの加工条件P99x(1～6)無効設定
	short	LeaveZrnFin;			// アラーム時原点復帰済みﾌﾗｸﾞを残す軸ﾌﾗｸﾞ
	short	EdgeSrchMaxMinUse;		// 端面位置計算時最大/最小値使用フラグ
	short	EdgeSrchOldMcnComp;		// ｺｰﾅｰ出し/芯出し動作の旧機種互換動作ﾌﾗｸﾞ
	short	CylSelBfrEDeplAec;		// 電極消耗時のAEC前 CYL出力選択
									// (0～128:0は無効、D08はOFF指定)
	short	CylSelAftEDeplAec;		// 電極消耗時のAEC後 CYL出力選択
									// (0～128:0は無効、D08はOFF指定)

	char	Reserved556[2];			// 予約

	long	CysOffChkDis[4];			// CYSオフチェック無効設定
										// 	CysOffChkDis[0] : D00:  1 ～ D31: 32
										// 	CysOffChkDis[1] : D00: 33 ～ D31: 64
										// 	CysOffChkDis[2] : D00: 65 ～ D31: 96
										// 	CysOffChkDis[3] : D00: 97 ～ D31:128
	int		SvPrcsStpRetOfs;		// サーボ放電加工途中停止時戻り量
	int		SvPrcsRetOfs;			// サーボ放電加工終了時戻り量
	int		SvPrcsRetSpd;			// サーボ放電加工終了時戻り速度
	short	AxServoPrcs;			// サーボ放電加工対象軸設定(D0:X ～ )

	char	Reserved588[2];			// 予約

	long	CysLatch[4];				// CYS信号ラッチ選択
										// 	CysLatch[0] : D00:  1 ～ D31: 32
										// 	CysLatch[1] : D00: 33 ～ D31: 64
										// 	CysLatch[2] : D00: 65 ～ D31: 96
										// 	CysLatch[3] : D00: 97 ～ D31:128
	short	InstLastElctdFlg;		// 最終電極装着モード有効フラグ

	char	Reserved608[2];			// 予約

	long	SpinMaxSpd[9];				// 無限回転軸回転速度上限値 [pps]
	short	PrcsSkipStopCys;		// 放電加工スキップ時停止モードCYS信号選択
	short	DischgTchErrEn;			// 放電中の接触感知エラー検出有効フラグ

	long	CFeedTable[16];				// 主軸速度(0～15)->Ｃ軸速度(0.1rpm)テーブル
	short	GuideThroughChkDis;		// ガイド貫通検出無効パーティションフラグ
	short	Z2ndSoftLimMSelCys;		// Ｚ軸第２ソフトリミット指定CYS信号選択
	long		Z2ndSoftLimM;			// Ｚ軸第２ソフトリミット
	short	GuidChgCylEn;			// ガイド交換時ＣＹＬ出力有効フラグ
									//   0:無効, 1～128:CYL出力信号先頭番号
	short	GuidChgCysSel;			// ガイド交換時ＣＹＬ出力後ＣＹＳ信号待選択
									//   0:無効, 1～128:CYS信号番号

	long	GuidHolderEscOfs[9];		// ガイドホルダー退避量
	short	GuidHolderArmDis;		// ガイドホルダーアームシリンダ無効フラグ
	short	TouchSenseISetEn;		// 接触感知によるｲﾆｼｬﾙｾｯﾄ有効ﾌﾗｸﾞ(電源投入時初期値)
	short	CylSelPatRndStp;		// パーティション１周停止中 CYL出力選択
	short	PatRndStpOnlyElctdDep;	// 電極消耗による電極交換時のみﾊﾟｰﾃｨｼｮﾝ１周停止有効ﾌﾗｸﾞ
	short	CylSelThrghDetect;		// 放電加工時貫通検出後のCYL出力選択
									// (0～128:0は無効、D08はOFF指定)
	short	RefSpdSelThrghDetect;	// 放電加工時貫通検出基準速度選択(0:無負荷時速度、1:SFR-DN)
	short	PerInitThrghDetect;		// 放電加工時貫通検出基準％選択初期値   (M91 I指定)
	short	PerInitThDetectEdge;	// 放電加工時抜け際検出基準％選択初期値 (M81 I指定)
	long		AvTimInitThDetectEdge;	// 放電加工時抜け際検出速度計測設定初期値 (M81 J指定)
	short	RefSpdIgnHPerInit;		// 放電加工時貫通検出基準速度計測上側無視％初期値 (M25 H指定)
	short	RefSpdIgnLPerInit;		// 放電加工時貫通検出基準速度計測下側無視％初期値 (M25 I指定)
	short	RefSpdAvTimInit;		// 放電加工時貫通検出基準速度計測平均化時間初期値 (M25 J指定)

	char	Reserved788[2];			// 予約
	int		PreElctdChgLmtPos;		// 電極消耗前電極交換リミット座標

	short	ElctdExchMovOrder[9];		// 電極交換位置移動順（1～9：0は最後）
	short	CylSelInElctdPrcs;		// １穴電極放電加工中CYL出力選択 (0～128 : 0は出力無効)
	short	PatliteMode;			// パトライト出力モード選択 (0～2)
	short	AxVirActDis;			// 仮想点/測定点移動無効軸フラグ(A/B/C軸のみ)(電源投入時初期値)
	short	SvPrcsInitialTime;		// サーボ放電加工初期化時間(放電安定待ち時間) [msec]
	short	ThinElctdISetPumpOff;	// 細線電極でのイニシャルセット時
									// ポンプ無効パーティションフラグ(D0:1 ～ D5:6)
	short	AecWRefSel;				// ＡＥＣ開始/終了時Ｗ軸待避位置選択
									// (0=Ｗ軸原点, 1=Ｗ軸上限値)

	char	Reserved824[2];			// 予約
	short	GuideThroughChkPNo;		// ｶﾞｲﾄﾞ貫通ﾁｪｯｸ時加工条件番号設定 (電源投入時初期値)
									// (0			= 従来通りP998/999を使用
									//  16384～17383 = P0～P999を使用
									//				 (D14を有効フラグとして使用))

	short	InitialSetPno[6];			// ｲﾆｼｬﾙｾｯﾄ時加工条件番号設定 (電源投入時初期値)
										// (0			= 従来通りP998/999を使用
										//  16384～17383 = P0～P999を使用
										//				 (D14を有効フラグとして使用))

	char	Reserved840[2];				// 予約

	long	ElctdChkOfsZ2[6];			// 電極装着後のｾﾝｻｰからのＺ軸２段目下降量設定(電源投入時初期値)

	short	GuideThroughChkPNoPat[6];	// ガイド貫通チェック時加工条件番号設定 (電源投入時初期値)
										// (0			= 従来通りP998/999を使用
										//  16384～17383 = P0～P999を使用
										//				 (D14を有効フラグとして使用))
	short	EUninstColReclampDis;	// 電極脱着時のコレット再クランプ(咥え直し)動作無効フラグ

	char	Reserved880[2];			// 予約
	int		AvTimInitThrghDetect;	// 放電加工時貫通検出速度計測設定
									//(平均化時間/速度計測距離)初期値(M91 J指定)
	int		EdgeLSrchTouchRet;		// 複数軸直線補間端面位置だし時戻り量[pls](全軸合成距離)
	int		EdgeLSrchRetSpd;		// 複数軸直線補間端面位置だし時戻り速度[pps](全軸合成速度)

	char	Reserved896[4];			// 予約

	double	InitPrmMacVal[10];			// 初期化パラメータ指定固定マクロ変数値(#1500～#1509)
	short	NrmElctdPrcsBuckling;	// 通常電極での放電加工時座屈ｾﾝｻｰ検出フラグ
									// (0:無効、1:エラー、2:停止、3:電極交換)
	short	NrmElctdGuideBuckling;	// 通常電極でのガイド貫通時座屈ｾﾝｻｰ検出フラグ
									// (0:無効、1:エラー、2:次電極取得)
	short	RotAxMovMode;			// 無限回転軸アブソ位置決め移動方法初期値 (0～5)
									// (0:従来通り、1:近回り、
									//  2:符号判別(360°以上なし)、
									//  3:符号判別(360°以上あり)、
									//  4:常時＋方向、5:常時－方向)
	short	AutoModeOutSel;			// 自動モード中出力信号選択
									// (-1～128：0=無効, -1=o#1727 D06, 1～128=CYL信号)

	long	CylBitRstPlsCont[4];		// リセット/エラー発生時CYLパルス出力継続設定
										// 	CylBitRstPlsCont[0] : D00:  1 ～ D31: 32
										// 	CylBitRstPlsCont[1] : D00: 33 ～ D31: 64
										// 	CylBitRstPlsCont[2] : D00: 65 ～ D31: 96
										// 	CylBitRstPlsCont[3] : D00: 97 ～ D31:128
	short	PrcsTmoutChkEndSel;		// 放電加工タイムアウト検出終了タイミング選択
									// (0:加工深さ到達で終了、
									//  1:放電OFF(スパークアウトタイマ時間経過
									//   ＆放電加工時貫通検出後のCYL出力完了)で終了)

	char	Reserved1004[2];			// 予約
	int		EIFErr1MaskSet;			// EtherCAT IFボード エラー1マスク設定
	int		EIFErr2MaskSet;			// EtherCAT IFボード エラー2マスク設定
	short	Fanstop10DlyTim;		// 本機内FAN停止 加工終了後待ち時間[sec]
	short	Fanstop20DlyTim;		// SF02FX内FAN停止 加工終了後待ち時間[sec]

	long	SfrFrTable[16];				// 加工サーボ送り速度テーブル[pls/min]

	long	SfrBkTable[16];				// 加工サーボ戻り速度テーブル[pls/min]
	short	SoftStartLowSpeed;		// ソフトスタート低速速度設定[50～750ms(50ms間隔)]
	short	SoftStartHighSpeed;		// ソフトスタート高速速度設定[ 2～ 30ms( 2ms間隔)]
	short	SoftStartIP;			// ソフトスタート開始電流設定
									//  0x00:IP=0A, 0x01:IP=3Aから開始する。
	short	BONDLY_E323_O;			// BONDLY_E323_O設定[1～255ms(デフォルト8ms)]
	short	BONDLY0;				// BONDLY_0設定     [1～255ms(デフォルト8ms)]
	short	BON_SignSel;			// BON信号選択設定
									//  0x00:BON10=BONDLY_E323_O / BON00=BONDLY0
									//  0x01:BON10=BON0          / BON00=BON10
	short	OFFExtendRatio;			// OFF伸ばし倍率設定[1～32倍(デフォルト10倍)]
	short	ZClrErrTrgSelSet;		// Zクリアエラー検出トリガ信号選択設定
									//  0x00:VSPK , 0x01:VS
	short	ZClrErrCntSet;			// Zクリアエラーカウント数設定
									//  0x00～0xFF(デフォルト0x10)
	char	Reserved2048[886];			// 予約
} INITIALPRM;

/*******************************************************************/
/* アンサーステータス情報構造体*/
/*******************************************************************/
typedef struct
{ /* 全体情報構造体１６ﾊﾞｲﾄ*/
	long Status; /* 全体ステータス*/
	long Alarm; /* 全体アラーム*/
	char Reserved[8]; /* 予約*/
} MCSTATUS;
typedef struct
{ /* 各軸情報構造体４８ﾊﾞｲﾄ*/
	long AxStatus; /* 軸ステータス*/
	long AxAlarm; /* 軸アラーム*/
	long ComReg; /* 指令位置*/
	long PosReg; /* 機械位置*/
	long ErrReg; /* 偏差量*/
	long BlockSeg; /* 最新ブロック払い出し量*/
	long AbsReg; /* 絶対位置(指令) */
	long Trq; /* トルク*/
	long AMrReg; /* 絶対位置(指令:ﾏｼﾝﾛｯｸ込み) */
	char Reserved[12]; /* 予約*/
} AXSTATUS;
typedef struct
{ /* タスク情報構造体３２ﾊﾞｲﾄ*/
	long TaskStatus; /* タスクステータス*/
	long TaskAlarm; /* タスクアラーム*/
	short Override; /* 送りオーバーライド設定*/
	short COverride; /* 補間オーバーライド設定*/
	short SOverride; /* 主軸オーバーライド設定*/
	short ProgramNo; /* 選択・実行プログラム番号*/
	unsigned long StepNo; /* 実行ステップ番号*/
	short NNo; /* 待機・実行Ｎ番号*/
	short LineNo; /* 待機・実行行番号*/
	short LineFlg;	/* 待機・実行行番号種別*/
	char Reserved[6]; /* 予約*/
} TASKSTATUS;

typedef struct
{ /* ECNCステータス情報構造体*/
	unsigned short Status2; /* 各種状態情報２ */
	unsigned short Status3; /* 各種状態情報３ */
	unsigned short Alarm2; /* アラーム情報２ */
	unsigned short Alarm3; /* アラーム情報３ */
	unsigned short Alarm4; /* アラーム情報４ */
	unsigned short Alarm5; /* アラーム情報５ */
	char Reserved0[4]; /* 予約*/
	long WTopPos; /* Ｗ軸上限値*/
	long CorrAng; /* 補正角度(8-24ﾋﾞｯﾄ固定小数点)*/
	unsigned long EIFErr1; /* EtherCAT IFボードエラー1 */
	unsigned long EIFErr2; /* EtherCAT IFボードエラー2 */
	short ADVal[5]; /* アナログ入力値*/
	char Reserved1[6]; /* 予約*/
} ECNCSTATUS;
typedef struct
{ /* アンサーステータス情報構造体*/
	MCSTATUS mc; /* 全体情報*/
	AXSTATUS ax[64]; /* 各軸情報*/
	TASKSTATUS task[8]; /* タスク情報*/
	ECNCSTATUS ecnc; /* ECNC情報*/
} STATUS;

/************************************************************************/
/* 手動パルサー動作許可コマンド構造体*/
/************************************************************************/
typedef struct
{
	short permit; /* 許可/不許可フラグ(0:不許可、1:許可) */
} HANDLEPERMIT;

/************************************************************************/
/* 動作モードデータ変更コマンド付加データ構造体*/
/************************************************************************/
typedef struct
{
	short mode; /* 動作モード*/
} MODECHG;

/************************************************************************/
/* 有効／無効設定構造体*/
/************************************************************************/
typedef struct
{
	short enable; /* 有効/無効設定フラグ(0:無効、1:有効) */
} ENABLE;

/************************************************************************/
/* 加工条件番号設定コマンド構造体*/
/************************************************************************/
typedef struct
{
	long PNo; /* 加工条件番号*/
	char Reserved[4]; /* 予約*/
} PRNOSEL;

/************************************************************************/
/* ポイント構造体*/
/************************************************************************/
typedef struct
{
	long Pnt[9]; /* 第１～９軸座標値*/
} AXPOINT;

/************************************************************************/
/* 各軸仮想点/測定点座標構造体*/
/************************************************************************/
typedef struct
{
	AXPOINT AxPnt[101]; /* 各軸仮想点/測定点座標*/
						/* （0～99：仮想点、100:測定点） */
	char Reserved[4];/* 予約*/
} VIRPOS_EX;
/************************************************************************/
/* ワーク原点座標設定構造体*/
/************************************************************************/
typedef struct
{
	long WorkOrg[9]; /* 第１～９軸ワーク原点座標*/
} WORKORG;
/************************************************************************/
/* パラメータデータ構造体(9080バイト固定長) */
/************************************************************************/
typedef struct
{ /* 各軸独立パラメータ構造体(140バイト固定長) */
	long InPos; /* ＩＮＰＯＳ量[pls] */
	long ErMax; /* 偏差上限値[pls] */
	long MPosErMax; /* ＭＰＯＳ偏差上限値[pls] */
	long Ka; /* 補間時定数[msec] */
	long SKa; /* Ｓ字補間時定数[msec] */
	long Dx; /* ＰＴＰ時定数[msec] */
	long PtpFeed; /* ＰＴＰ速度[pls/sec] */
	long JogFeed; /* ＪＯＧ送り速度[pls/sec] */
	long SoftLimP; /* ソフトリミット＋側[pls] */
	long SoftLimM; /* ソフトリミット－側[pls] */
	long OrgDir; /* 原点復帰方向*/
	long OrgOfs; /* 原点距離[pls] */
	long OrgPos; /* 原点復帰逃げ位置[pls] */
	long OrgFeed; /* 原点復帰早送り速度[pls/sec] */
	long AprFeed; /* 原点復帰アプローチ速度[pls/sec] */
	long SrchFeed; /* 原点復帰最終サーチ速度[pls/sec] */
	long OrgPri; /* 原点復帰順位*/
	long Homepos; /* ﾎｰﾑﾎﾟｼﾞｼｮﾝ位置[pls] */
	long Homepri; /* ﾎｰﾑﾎﾟｼﾞｼｮﾝ順位*/
	long BackL; /* バックラッシュ補正量[pls] */
	long Revise; /* 形状補正係数*/
	long OrgCsetOfs; /* 原点復帰時論理座標*/
	long handle_max; /* ｼﾞｮｲｽﾃｨｯｸ/ﾊﾝﾄﾞﾙ最大送り速度*/
	long handle_ka; /* ｼﾞｮｲｽﾃｨｯｸ/ﾊﾝﾄﾞﾙ加減速時定数*/
	unsigned char Reserved[44]; /* 未使用*/
} AXIS_PARAMETER;
typedef struct
{ /* 特殊パラメータ共用体(120バイト) */
	unsigned char Reserved[120]; /* 特殊パラメータ未定義*/
} ADD_PARAMETER;
typedef struct
{ /* (1720バイト) */
	AXIS_PARAMETER AxisParam[64]; /* 各軸パラメータ*/
	ADD_PARAMETER adp; /* 特殊パラメータ*/
} PARAMETER_DATA;

/************************************************************************/
/* ピッチエラー補正用パラメータ情報構造体（34560バイト固定長） */
/************************************************************************/
typedef struct
{ /* 各軸補正用パラメータ（20バイト） */
	unsigned long RevMagnify; /* 補正倍率*/
	unsigned long RevSpace; /* 補正間隔*/
	unsigned long RevTopNo; /* 補正データ先頭番号*/
	unsigned long RevMCnt; /* -側補正区間数*/
	unsigned long RevPCnt; /* +側補正区間数*/
} REV_AX;
typedef struct
{ /* ピッチエラー補正用パラメータ*/
	REV_AX RevAx[64]; /* 各軸補正用パラメータ*/
	short RevDt[33280]; /* 補正データ*/
} PITCH_ERR_REV;

/************************************************************************/
/* 仮想点／測定点変更コマンド構造体*/
/************************************************************************/
typedef struct
{
	long VirNo; /* 仮想点/測定点番号*/
	long VirPos[9]; /* 仮想点座標*/
} VIRPOSCHG;


/************************************************************************/
/* プログラム変換ライブラリ初期化構造体*/
/************************************************************************/
typedef struct
{ /* プログラム変換ライブラリ初期化構造体（Ver2.6 ～）*/
	short RtcTime; /* ＲＴＭＣ６４－ＥＣ制御周期*/
	short InchMode; /* inch/mm 指定（0:mm、1:inch） */
	const char *pGUCD; /* ﾕｰｻﾞｰ定義Ｇｺｰﾄﾞﾌｧｲﾙ格納ﾃﾞｨﾚｸﾄﾘ名*/
	const char *pMUCD; /* ﾕｰｻﾞｰ定義Ｍｺｰﾄﾞﾌｧｲﾙ格納ﾃﾞｨﾚｸﾄﾘ名*/
	short InchAxis; /* inch/mm対象軸フラグ*/
	short align; /* 予約*/
	long StrctSize; /* 初期化構造体サイズ(0:PRGINITDATA_OLD)*/
	unsigned char GcdDis[256]; /* 無効Ｇコードフラグ(0:有効、1:無効) */
	unsigned char McdDis[256]; /* 無効Ｍコードフラグ(0:有効、1:無効) */
	long ZAcServoEn; /* Ｚ軸ＡＣサーボ機能有効フラグ*/ // Ver3.0～
} PRGINITDATA;

/************************************************************************/
/* 汎用入出力情報構造体*/
/************************************************************************/
typedef struct
{
	unsigned short Input[116]; /* 汎用入力*/
	unsigned short Output[64]; /* 汎用出力*/
} IODATA;

/************************************************************************/
/* ＡＥＣ状態構造体*/
/************************************************************************/
typedef struct
{
	short PartitionS[6]; /* 第１～６ﾊﾟｰﾃｨｼｮﾝ開始番号*/
	short PartitionE[6]; /* 第１～６ﾊﾟｰﾃｨｼｮﾝ終了番号*/
	short Thinline[6]; /* 第1～6ﾊﾟｰﾃｨｼｮﾝ細線設定(1:細線,0:通常)*/
	short PartitionDis; /* ﾊﾟｰﾃｨｼｮﾝ無効ﾌﾗｸﾞ(1:無効,0:有効) */
	short ElectrodeNo; /* 電極番号(0:未設定) */
	short GuideNo; /* ガイド番号(0:未設定) */
	short IndexZrnFin; /* インデックス番号有効フラグ*/
	short IndexNo; /* インデックス番号*/
	char Reserved[18];
} AECDATA;

/************************************************************************/
/* 加工条件情報構造体*/
/************************************************************************/
typedef struct
{
	short Status; /* 各種状態情報*/
	short PNo; /* 加工条件番号*/
	short ZUpFeed; /* Ｚ軸上昇速度*/
	short ZDwFeed; /* Ｚ軸下降速度*/
	short CFeed; /* 主軸送り速度*/
	short SpinOut; /* 主軸回転(0:停止,1:CW,2:CCW) */
	short PumpOut; /* ポンプ出力(0:OFF,1:ON) */
	short PrCFeed; /* 放電加工時主軸速度(加工条件) */
	long CFeedRpm; /* Ｃ軸主軸速度(0.1rpm) */
	long DryRunEnN; /* ドライラン有効フラグ(放電加工穴数) */
	char Reserved[8]; /* 予約*/
} PCONDITION;

/************************************************************************/
/* ガイド番号設定コマンド構造体*/
/************************************************************************/
typedef struct
{
	short GuideNo; /* ガイド番号*/
	char Reserved[6]; /* 予約*/
} GUIDENO;
/************************************************************************/
/* 電極番号設定コマンド構造体*/
/************************************************************************/
typedef struct
{
	short ElectrodeNo; /* 電極番号*/
	char Reserved[6]; /* 予約*/
} ELCTDNO;

/************************************************************************/
/* 主軸回転ON/OFFコマンドパラメータ構造体*/
/************************************************************************/
typedef struct
{
	short SpOut; /* 主軸指令フラグ*/
} SPCMND;

/************************************************************************/
/* ポンプ制御コマンド構造体*/
/************************************************************************/
typedef struct
{
	short PumpOut; /* ポンプ制御フラグ（0:OFF、1:ON） */
	char Reserved[6]; /* 予約*/
} PUMPCMND;

/************************************************************************/
/* 放電制御コマンド構造体*/
/************************************************************************/
typedef struct
{
	short BonOut; /* 放電制御フラグ（0:OFF、1:ON）*/
	char Reserved[6]; /* 予約*/
} BONCMND;

/************************************************************************/
/* ROMバージョン情報構造体*/
/************************************************************************/
typedef struct
{
	char Version[16]; /* バージョン文字列*/
	unsigned short EvenSum; /* SUM:even (rom) */
	unsigned short OddSum; /* SUM:odd (rom) */
	unsigned short FlashSum; /* SUM:SH内部FLASH */
	short FlashFlg; /* SH内部FLASH使用フラグ*/
	unsigned long KindID; /* 機種ID */
	unsigned long SerialID; /* シリアルID */
	unsigned long ProductID; /* プロダクトID */
	char Reserved[28]; /* 予約*/
} ROMVERSION;

/************************************************************************/
/* Ｗ軸上限値設定構造体*/
/************************************************************************/
typedef struct
{
	long WTopPos; /* Ｗ軸上限値*/
	char Reserved[4]; /* 予約*/
} WTOPPOS;
/************************************************************************/
/* 軸速度オーバーライド変更コマンドパラメータ構造体*/
/************************************************************************/
typedef struct
{
	unsigned short Override; /* オーバーライド設定値*/
} OVERCHG;

/************************************************************************/
/* 座標系設定コマンドパラメータ構造体*/
/************************************************************************/
typedef struct
{
	unsigned long AxisFlag; /* 軸選択フラグ*/
	long PosAxis[9]; /* 第1軸～第9軸論理原点座標値*/
} WORGPOSCHG;

/************************************************************************/
/* 手パ操作情報構造体*/
/************************************************************************/
typedef struct
{
	short handle_mode; /* 手パ有効フラグ*/
	short kp; /* 手パ設定倍率*/
	short ax1; /* 手パ第1軸*/
	short ax2; /* 手パ第2軸*/
} HANDLESTS;

/************************************************************************/
/* ガイドクランプ/アンクランプコマンド構造体*/
/************************************************************************/
typedef struct
{
	long clump; /* 動作フラグ(0:Unclump、1:Clump) */
	char Reserved[4]; /* 予約*/
} GUIDECLUMP;
/************************************************************************/
/* スピンドルクランプ/アンクランプコマンド構造体*/
/************************************************************************/
typedef struct
{
	long clump; /* 動作フラグ(0:Unclump、1:Clump) */
	char Reserved[4]; /* 予約*/
} SPCLUMP;

/************************************************************************/
/* メッセージ表示要求データ構造体*/
/************************************************************************/
typedef struct
{
	short LineFlg; /* メッセージ表示命令実行プログラム種別*/
	short MessageNo; /* メッセージ番号*/
	char Reserved[4]; /* 予約*/
} MESSAGEREQ;

/************************************************************************/
/* メッセージ表示結果データ構造体*/
/************************************************************************/
typedef struct
{
	MESSAGEREQ Req; /* メッセージ表示要求元データ*/
	short ButtonSel; /* 選択(クリック)ボタン番号*/
	char Reserved[6]; /* 予約*/
	double InputNum; /* 数値入力値*/
} MESSAGEANS;

/************************************************************************/
/* 強制原点復帰完了設定コマンド構造体*/
/************************************************************************/
typedef struct
{
	short AxisFlag; /* 軸選択フラグ*/
	char Reserved[6]; /* 予約*/
} FORCEZRNFIN;

/************************************************************************/
/* ＥＳＦアーム移動コマンド構造体*/
/************************************************************************/
typedef struct
{
	short pos; /* 動作フラグ(0:前端,1:中間,2:後端) */
	char Reserved[6];/* 予約*/
} MOVEESFARM;

/************************************************************************/
/* ＥＳＦアーム移動コマンド構造体*/
/************************************************************************/
typedef struct
{
	short open; /* 動作フラグ(0:Close、1:Open) */
	char Reserved[6]; /* 予約*/
} OPENESFARM;

/************************************************************************/
/* ＥＳＦマガジン移動コマンド構造体*/
/************************************************************************/
typedef struct
{
	long MagazineNo; /* 移動先マガジン番号*/
	char Reserved[4]; /* 予約*/
} ESFMAGMOV;

/************************************************************************/
/* ＧＳＦアーム移動コマンド構造体*/
/************************************************************************/
typedef struct
{
	short pos; /* 動作フラグ(0:前端、1:後端) */
	char Reserved[6]; /* 予約*/
} MOVEGSFARM;

/************************************************************************/
/* 電極装着/脱着コマンド構造体*/
/************************************************************************/
typedef struct
{
	short install; /* 動作フラグ(0:脱着、1:装着) */
	short ElctdNo; /* 電極番号*/
	char Reserved[4]; /* 予約*/
} ELCTDINSTALL;

/************************************************************************/
/* 電極交換位置移動コマンド構造体*/
/************************************************************************/
typedef struct
{
	short axis; /* 軸選択フラグ*/
	short pos; /* 動作フラグ*/
			   /* (0:交換位置、1:前位置、2:待機位置) */
	char Reserved[4];/* 予約*/
} ELCTDEXCHPOS;
/************************************************************************/
/* ガイド装着／脱着コマンド構造体*/
/************************************************************************/
typedef struct
{
	short install; /* 動作フラグ(0:脱着、1:装着) */
	short GuideNo; /* ガイド番号*/
	char Reserved[4]; /* 予約*/
} GUIDEINSTALL;

/************************************************************************/
/* ガイド交換位置移動コマンド構造体*/
/************************************************************************/
typedef struct
{
	short axis; /* 軸選択フラグ*/
	short pos; /* 動作フラグ(0:交換位置、2:待機位置) */
	short GuideNo; /* ガイド番号*/
	char Reserved[2];/* 予約*/
} GUIDEEXCHPOS;

/************************************************************************/
/* ガイド確認位置移動コマンド構造体*/
/************************************************************************/
typedef struct
{
	short axis; /* 軸選択フラグ*/
	short pos; /* 動作フラグ*/
			   /* (0:確認位置、1:前位置、2:待機位置) */
	char Reserved[4];/* 予約*/
} GUIDECHKPOS;

/************************************************************************/
/* ガイド貫通動作許可コマンド構造体*/
/************************************************************************/
typedef struct
{
	short move; /* 移動許可ﾌﾗｸﾞ(0:不許可,1:許可)*/
	char Reserved[6]; /* 予約*/
} GUIDETHROUGH;

/************************************************************************/
/* シャットダウン開始コマンド構造体*/
/************************************************************************/
typedef struct
{
	short startflg; /* ｼｬｯﾄﾀﾞｳﾝ開始ﾌﾗｸﾞ(0:停止,1:開始)*/
	char Reserved[6]; /* 予約*/
} SHUTDOWN_START;

/************************************************************************/
/* ブザーON/OFF信号出力コマンド構造体*/
/************************************************************************/
typedef struct
{
	short onflg; /* ブザーON/OFFフラグ（0:OFF、1:ON）*/
	char Reserved[6]; /* 予約*/
} BUZZER_ON_OUTPUT;

/************************************************************************/
/* 自動モード中出力コマンド構造体*/
/************************************************************************/
typedef struct
{
	short onflg; /* 自動モード中出力フラグ（0:OFF、1:ON）*/
	char Reserved[6]; /* 予約*/
} AUTOMODE_OUTPUT;

/************************************************************************/
/* 加工条件テーブル構造体*/
/************************************************************************/
typedef struct
{
	short Ton; /* Ton[us] */
	short Toff; /* Toff[us] */
	long IPVal; /* IP(SFIP)[mA] */
				/* (IP[A]*1000の値を格納) */
	long CAPVal; /* CAP[nF] */
				 /* (CAP[uF]*1000の値を格納) */
	short SCVal; /* サーボコントロールDA(0-63) */
	short CRSVal; /* SP軸コントロールDA(0-15) */
	short SfrFrSel; /* 加工サーボ送り速度選択(0-15) */
	short SfrBkSel; /* 加工サーボ戻り速度選択(0-15) */
	short InvVal; /* インバータDA(0-15) */
	short ServoSel; /* サーボ選択(0-3) */
	short PSSel; /* 電源選択(0-5) */
	short POLVal; /* 条件切替え*/
	char Reserved[36]; /* 予約*/
} PCOND_REC;
typedef struct
{
	PCOND_REC rec[1000];
} PCOND_TBL;

/************************************************************************/
/* 強制原点復帰完了設定コマンド構造体*/
/************************************************************************/
typedef struct
{
	long DryRunEnN; /* ドライラン有効フラグ(放電加工穴数) */
					/* [0:ドライラン無効,-1:穴数指定無効] */
	char Reserved[4];/* 予約*/
} DRYRUN_EX;
/************************************************************************/
/* 加工条件による主軸速度通知コマンド構造体*/
/************************************************************************/
typedef struct
{
	short PrCFeed; /* 加工条件による主軸速度*/
	char Reserved[6]; /* 予約*/
} PRCFEED;

extern "C" __declspec ( dllexport )int _stdcall InitCommProc( PSXDEF psxdef, int *phCom );
extern "C" __declspec ( dllexport )int _stdcall QuitCommProc( int hCom );
extern "C" __declspec ( dllexport )int _stdcall SendData( int hCom, short type, short task, short prm, DWORD size, LPVOID data );
extern "C" __declspec ( dllexport )int _stdcall ReceiveData( int hCom, short type, short task, short prm, LPDWORD size, LPVOID data );
extern "C" __declspec ( dllexport )int _stdcall SendCommand( int hCom, short cmnd, short task, LPVOID data );
extern "C" __declspec ( dllexport )int _stdcall ConvIMData( int hCom, short type, void *p_mm, void *p_inch, short InchMode, short InchAxis );
extern "C" __declspec ( dllexport )int _stdcall GcdInitialize( LPVOID data );
extern "C" __declspec ( dllexport )int _stdcall GcdGetErrLine();
extern "C" __declspec ( dllexport )int _stdcall FileGcodeConv( LPBYTE fname, LPDWORD size, LPVOID bin, short *pno );
