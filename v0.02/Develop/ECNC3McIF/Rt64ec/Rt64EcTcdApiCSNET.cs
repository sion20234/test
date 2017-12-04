using System;
using System.Runtime.InteropServices;
using System.Text;

class Rt64ectcdapi
{

    public const short TCD_PROG_SIZE_192KB = 0;     //�P�X�Q�����v���O�����d�l
    public const short TCD_PROG_SIZE_64KB = 1;      // �U�S�����v���O�����d�l
    public const short TCD_PROG_SIZE_32KB = 2;      // �R�Q�����v���O�����d�l
    public const short TCD_PROG_SIZE_16KB = 3;      // �P�U�����v���O�����d�l

    public const short TCD_SEC_UNIT = 0;                // �b�P�ʎw��
    public const short TCD_MIN_UNIT = 1;                // ���P�ʎw��

#if true    // �W�����W���[���Ƃ���"rt64ectcdapiCSNET.cs"��"rt64ecgcdapiCSNET.cs"�𗼕��g�p����ƁA
    // ���L��`�����d��`�ƂȂ��Ă��܂��܂��B
    // ���̂悤�ȏꍇ�͏�L��`���g�p���Ă��������B
    // �i�g�p���Ȃ���Α��d��`�G���[�͔������܂���B�j
    public const short PROG_SIZE_192KB = TCD_PROG_SIZE_192KB;
    public const short PROG_SIZE_64KB = TCD_PROG_SIZE_64KB;
    public const short PROG_SIZE_32KB = TCD_PROG_SIZE_32KB;
    public const short PROG_SIZE_16KB = TCD_PROG_SIZE_16KB;

    public const short SEC_UNIT = TCD_SEC_UNIT;
    public const short MIN_UNIT = TCD_MIN_UNIT;

    public const short PRG_PLMC = 4;        // �o�k�l�b�S�O
#endif


    // ------------------------------------------------------------------------
    //	�e�N�m�R�[�h�v���O�����ϊ��֐��v���g�^�C�v
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
