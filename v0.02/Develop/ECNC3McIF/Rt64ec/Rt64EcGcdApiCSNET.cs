using System;
using System.Runtime.InteropServices;
using System.Text;

class Rt64ecgcdapi
{

    //--------------------------------------------------------------------------
    //		ラベル定義
    //--------------------------------------------------------------------------
    public const short GCD_PROG_SIZE_192KB = 0;     //１９２ｋｂプログラム仕様
    public const short GCD_PROG_SIZE_64KB = 1;      // ６４ｋｂプログラム仕様
    public const short GCD_PROG_SIZE_32KB = 2;      // ３２ｋｂプログラム仕様
    public const short GCD_PROG_SIZE_16KB = 3;      // １６ｋｂプログラム仕様

    public const short GCD_SEC_UNIT = 0;                // 秒単位指定
    public const short GCD_MIN_UNIT = 1;                // 分単位指定

    //--------------------------------------------------------------------------
    //		変換エラーコード定義
    //--------------------------------------------------------------------------
    public const byte PRGERR_FILE = 1;  // プログラムファイルエラー
    public const byte PRGERR_BUFF_OVERFlOW = 2; // プログラムバッファオーバーフロー
    public const byte PRGERR_FORMAT = 3;    // プログラムフォーマットエラー
    public const byte PRGERR_CALC = 4;  // プログラム変換計算エラー
    public const byte PRGERR_WMEM_OVERFLOW = 5; // 作業メモリオーバーフロー
    public const byte PRGERR_INITIAL = 6;   // 未初期化エラー
    public const byte PRGERR_CYCLIC_CALL = 7;   // ユーザー定義コード循環呼出エラー
    public const byte PRGERR_UNDEFINED_CODE = 8;    // 未定義コード指定エラー
    public const byte PRGERR_DISABLE_CODE = 9;  // 無効Ｇ／Ｍコード指定エラー

    // ------------------------------------------------------------------------
    // プログラム変換ライブラリ初期化構造体
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct PRGINITDATA
    {
        public int StrctSize;           // 初期化構造体サイズ
        public short RtcTime;           // RTMC64-EC制御周期[us]
        public short InchMode;          // inch/mm 指定（0:mm、1:inch）
        [MarshalAs(UnmanagedType.LPStr)]
        public string pGUCD;                // ﾕｰｻﾞｰ定義Ｇｺｰﾄﾞﾌｧｲﾙ格納ﾃﾞｨﾚｸﾄﾘ名
        [MarshalAs(UnmanagedType.LPStr)]
        public string pMUCD;                // ﾕｰｻﾞｰ定義Ｍｺｰﾄﾞﾌｧｲﾙ格納ﾃﾞｨﾚｸﾄﾘ名
        public short InchAxis;          // inch/mm対象軸フラグ
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] align;                // 予約
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public byte[] GcdDis;               // 無効Ｇコードフラグ(0:有効、1:無効)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public byte[] McdDis;               // 無効Ｍコードフラグ(0:有効、1:無効)
        public static PRGINITDATA Init()
        {
            PRGINITDATA tmp = new PRGINITDATA();
            tmp.align = new byte[6];
            tmp.GcdDis = new byte[256];
            tmp.McdDis = new byte[256];
            return tmp;
        }
    }

#if true   // 標準モジュールとして"rt64ectcdapiCSNET.cs"と"rt64ecgcdapiCSNET.cs"を両方使用すると、
    // 下記定義が多重定義となってしまいます。
    // このような場合は上記定義を使用してください。
    // （使用しなければ多重定義エラーは発生しません。）
    public const short PROG_SIZE_192KB = GCD_PROG_SIZE_192KB;
    public const short PROG_SIZE_64KB = GCD_PROG_SIZE_64KB;
    public const short PROG_SIZE_32KB = GCD_PROG_SIZE_32KB;
    public const short PROG_SIZE_16KB = GCD_PROG_SIZE_16KB;

    public const short SEC_UNIT = GCD_SEC_UNIT;
    public const short MIN_UNIT = GCD_MIN_UNIT;

    public const short PRG_PLMC = 4;         // ＰＬＭＣ４０
#endif

    // ------------------------------------------------------------------------
    //	Ｇコードプログラム変換関数プロトタイプ
    // ------------------------------------------------------------------------
    [DllImport("rt64ecgcnv.dll")]
    public extern static short MemGcodeConv(string text, ref int BinSize, [Out()] byte[] bin, ref short pno);
    [DllImport("rt64ecgcnv.dll")]
    public extern static short MemTaskGcodeConv(string text, ref int BinSize, [Out()] byte[] bin, short Task, ref short pno);
    [DllImport("rt64ecgcnv.dll")]
    public extern static short FileGcodeConv(string fname, ref int BinSize, [Out()] byte[] bin, ref short pno);
    [DllImport("rt64ecgcnv.dll")]
    public extern static short FileTaskGcodeConv(string fname, ref int BinSize, [Out()] byte[] bin, short Task, ref short pno);
    [DllImport("rt64ecgcnv.dll")]
    public extern static short MemGcodeRConv([In()] byte[] bin, ref int TextSize, StringBuilder text);
    [DllImport("rt64ecgcnv.dll")]
    public extern static short FileGcodeRConv(string fname, ref int TextSize, StringBuilder text);
    [DllImport("rt64ecgcnv.dll")]
    public extern static short DncGcodeConv(string text, ref int BinSize, [Out()] byte[] bin, short cont);

    [DllImport("rt64ecgcnv.dll")]
    public extern static short GcdCircleSet(short CirSize);
    [DllImport("rt64ecgcnv.dll")]
    public extern static short GcdSetProgMemSize(short mflg);
    [DllImport("rt64ecgcnv.dll")]
    public extern static short GcdFeedUnitSet(int pulse, short timef);
    [DllImport("rt64ecgcnv.dll")]
    public extern static short GcdPositionUnitSet(short flag);
    [DllImport("rt64ecgcnv.dll")]
    public extern static short GcdPaccTimeSet(short flag);
    [DllImport("rt64ecgcnv.dll")]
    public extern static short GcdSTSelect(short SlxType);
    [DllImport("rt64ecgcnv.dll")]
    public extern static short GcdCircleMode(short mode);
    [DllImport("rt64ecgcnv.dll")]
    public extern static short GcdDiametralAxis(short ax);
    [DllImport("rt64ecgcnv.dll")]
    public extern static short GcdSetMultiplier(ref int tbl);
    [DllImport("rt64ecgcnv.dll")]
    public extern static int GcdGetErrLine();
    [DllImport("rt64ecgcnv.dll")]
    public extern static short GcdSetZXPlane(short zx_flg);
    [DllImport("rt64ecgcnv.dll")]
    public extern static short GcdInitialize(ref PRGINITDATA init);

    [DllImport("rt64ecgcnv.dll")]
    public extern static short MemTaskGcodeConvS(string text, int bufsize, ref int cnvsize, [Out()] byte[] bin, short task, ref short pno);
    [DllImport("rt64ecgcnv.dll")]
    public extern static short FileTaskGcodeConvS(string fname, int bufsize, ref int cnvsize, [Out()] byte[] bin, short task, ref short pno);
    [DllImport("rt64ecgcnv.dll")]
    public extern static short MemGcodeRConvS([In()] byte[] bin, int bufsize, ref int cnvsize, StringBuilder text);
    [DllImport("rt64ecgcnv.dll")]
    public extern static short FileGcodeRConvS(string fname, int bufsize, ref int cnvsize, StringBuilder text);
    [DllImport("rt64ecgcnv.dll")]
    public extern static short DncGcodeConvS(string text, int bufsize, ref int cnvsize, [Out()] byte[] bin, short cont);
}
