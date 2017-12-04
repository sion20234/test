using System;
using System.Runtime.InteropServices;
using System.Text;

class Rt64ectcdapi
{

    public const short TCD_PROG_SIZE_192KB = 0;     //１９２ｋｂプログラム仕様
    public const short TCD_PROG_SIZE_64KB = 1;      // ６４ｋｂプログラム仕様
    public const short TCD_PROG_SIZE_32KB = 2;      // ３２ｋｂプログラム仕様
    public const short TCD_PROG_SIZE_16KB = 3;      // １６ｋｂプログラム仕様

    public const short TCD_SEC_UNIT = 0;                // 秒単位指定
    public const short TCD_MIN_UNIT = 1;                // 分単位指定

#if true    // 標準モジュールとして"rt64ectcdapiCSNET.cs"と"rt64ecgcdapiCSNET.cs"を両方使用すると、
    // 下記定義が多重定義となってしまいます。
    // このような場合は上記定義を使用してください。
    // （使用しなければ多重定義エラーは発生しません。）
    public const short PROG_SIZE_192KB = TCD_PROG_SIZE_192KB;
    public const short PROG_SIZE_64KB = TCD_PROG_SIZE_64KB;
    public const short PROG_SIZE_32KB = TCD_PROG_SIZE_32KB;
    public const short PROG_SIZE_16KB = TCD_PROG_SIZE_16KB;

    public const short SEC_UNIT = TCD_SEC_UNIT;
    public const short MIN_UNIT = TCD_MIN_UNIT;

    public const short PRG_PLMC = 4;        // ＰＬＭＣ４０
#endif


    // ------------------------------------------------------------------------
    //	テクノコードプログラム変換関数プロトタイプ
    // ------------------------------------------------------------------------
    [DllImport("rt64ectcnv.dll")]
    public extern static short MemProgramConv(string text, ref int BinSize, [Out()] byte[] bin, ref short pno);
    [DllImport("rt64ectcnv.dll")]
    public extern static short MemTaskProgramConv(string text, ref int BinSize, [Out()] byte[] bin, short Task, ref short pno);
    [DllImport("rt64ectcnv.dll")]
    public extern static short FileProgramConv(string fname, ref int BinSize, [Out()] byte[] bin, ref short pno);
    [DllImport("rt64ectcnv.dll")]
    public extern static short FileTaskProgramConv(string fname, ref int BinSize, [Out()] byte[] bin, short Task, ref short pno);
    [DllImport("rt64ectcnv.dll")]
    public extern static short MemProgramRConv([In()] byte[] bin, ref int TextSize, StringBuilder text);
    [DllImport("rt64ectcnv.dll")]
    public extern static short FileProgramRConv(string fname, ref int TextSize, StringBuilder text);
    [DllImport("rt64ectcnv.dll")]
    public extern static short DncProgramConv(string text, ref int BinSize, [Out()] byte[] bin, short cont);

    [DllImport("rt64ectcnv.dll")]
    public extern static short TcdCircleSet(short CirSize);
    [DllImport("rt64ectcnv.dll")]
    public extern static short TcdSetProgMemSize(short mflg);
    [DllImport("rt64ectcnv.dll")]
    public extern static short TcdFeedUnitSet(int pulse, short timef);
    [DllImport("rt64ectcnv.dll")]
    public extern static short TcdPositionUnitSet(short flag);
    [DllImport("rt64ectcnv.dll")]
    public extern static short TcdPaccTimeSet(short flag);
    [DllImport("rt64ectcnv.dll")]
    public extern static short TcdSTSelect(short SlxType);
    [DllImport("rt64ectcnv.dll")]
    public extern static short TcdCircleMode(short mode);
    [DllImport("rt64ectcnv.dll")]
    public extern static short TcdDiametralAxis(short ax);
    [DllImport("rt64ectcnv.dll")]
    public extern static short TcdSetMultiplier(ref int tbl);
    [DllImport("rt64ectcnv.dll")]
    public extern static int TcdGetErrLine();
    [DllImport("rt64ectcnv.dll")]
    public extern static short TcdSetZXPlane(short zx_flg);

    [DllImport("rt64ectcnv.dll")]
    public extern static short MemTaskProgramConvS(string text, int bufsize, ref int cnvsize, [Out()] byte[] bin, short task, ref short pno);
    [DllImport("rt64ectcnv.dll")]
    public extern static short FileTaskProgramConvS(string fname, int bufsize, ref int cnvsize, [Out()] byte[] bin, short task, ref short pno);
    [DllImport("rt64ectcnv.dll")]
    public extern static short MemProgramRConvS([In()] byte[] bin, int bufsize, ref int cnvsize, StringBuilder text);
    [DllImport("rt64ectcnv.dll")]
    public extern static short FileProgramRConvS(string fname, int bufsize, ref int cnvsize, StringBuilder text);
    [DllImport("rt64ectcnv.dll")]
    public extern static short DncProgramConvS(string text, int bufsize, ref int cnvsize, [Out()] byte[] bin, short cont);

}
