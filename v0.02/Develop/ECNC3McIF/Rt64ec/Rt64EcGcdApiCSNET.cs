using System;
using System.Runtime.InteropServices;
using System.Text;

class Rt64ecgcdapi
{

    //--------------------------------------------------------------------------
    //		���x����`
    //--------------------------------------------------------------------------
    public const short GCD_PROG_SIZE_192KB = 0;     //�P�X�Q�����v���O�����d�l
    public const short GCD_PROG_SIZE_64KB = 1;      // �U�S�����v���O�����d�l
    public const short GCD_PROG_SIZE_32KB = 2;      // �R�Q�����v���O�����d�l
    public const short GCD_PROG_SIZE_16KB = 3;      // �P�U�����v���O�����d�l

    public const short GCD_SEC_UNIT = 0;                // �b�P�ʎw��
    public const short GCD_MIN_UNIT = 1;                // ���P�ʎw��

    //--------------------------------------------------------------------------
    //		�ϊ��G���[�R�[�h��`
    //--------------------------------------------------------------------------
    public const byte PRGERR_FILE = 1;  // �v���O�����t�@�C���G���[
    public const byte PRGERR_BUFF_OVERFlOW = 2; // �v���O�����o�b�t�@�I�[�o�[�t���[
    public const byte PRGERR_FORMAT = 3;    // �v���O�����t�H�[�}�b�g�G���[
    public const byte PRGERR_CALC = 4;  // �v���O�����ϊ��v�Z�G���[
    public const byte PRGERR_WMEM_OVERFLOW = 5; // ��ƃ������I�[�o�[�t���[
    public const byte PRGERR_INITIAL = 6;   // ���������G���[
    public const byte PRGERR_CYCLIC_CALL = 7;   // ���[�U�[��`�R�[�h�z�ďo�G���[
    public const byte PRGERR_UNDEFINED_CODE = 8;    // ����`�R�[�h�w��G���[
    public const byte PRGERR_DISABLE_CODE = 9;  // �����f�^�l�R�[�h�w��G���[

    // ------------------------------------------------------------------------
    // �v���O�����ϊ����C�u�����������\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct PRGINITDATA
    {
        public int StrctSize;           // �������\���̃T�C�Y
        public short RtcTime;           // RTMC64-EC�������[us]
        public short InchMode;          // inch/mm �w��i0:mm�A1:inch�j
        [MarshalAs(UnmanagedType.LPStr)]
        public string pGUCD;                // հ�ް��`�f����̧�يi�[�ިڸ�ؖ�
        [MarshalAs(UnmanagedType.LPStr)]
        public string pMUCD;                // հ�ް��`�l����̧�يi�[�ިڸ�ؖ�
        public short InchAxis;          // inch/mm�Ώێ��t���O
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] align;                // �\��
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public byte[] GcdDis;               // �����f�R�[�h�t���O(0:�L���A1:����)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public byte[] McdDis;               // �����l�R�[�h�t���O(0:�L���A1:����)
        public static PRGINITDATA Init()
        {
            PRGINITDATA tmp = new PRGINITDATA();
            tmp.align = new byte[6];
            tmp.GcdDis = new byte[256];
            tmp.McdDis = new byte[256];
            return tmp;
        }
    }

#if true   // �W�����W���[���Ƃ���"rt64ectcdapiCSNET.cs"��"rt64ecgcdapiCSNET.cs"�𗼕��g�p����ƁA
    // ���L��`�����d��`�ƂȂ��Ă��܂��܂��B
    // ���̂悤�ȏꍇ�͏�L��`���g�p���Ă��������B
    // �i�g�p���Ȃ���Α��d��`�G���[�͔������܂���B�j
    public const short PROG_SIZE_192KB = GCD_PROG_SIZE_192KB;
    public const short PROG_SIZE_64KB = GCD_PROG_SIZE_64KB;
    public const short PROG_SIZE_32KB = GCD_PROG_SIZE_32KB;
    public const short PROG_SIZE_16KB = GCD_PROG_SIZE_16KB;

    public const short SEC_UNIT = GCD_SEC_UNIT;
    public const short MIN_UNIT = GCD_MIN_UNIT;

    public const short PRG_PLMC = 4;         // �o�k�l�b�S�O
#endif

    // ------------------------------------------------------------------------
    //	�f�R�[�h�v���O�����ϊ��֐��v���g�^�C�v
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
