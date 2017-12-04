using System;
using System.Runtime.InteropServices;

class Rt64eccomapi
{
    // ------------------------------------------------------------------------
    //	�ʐM�������\���̒�`
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct SXDEF
    {
        public int nSize;           // �ʐM�������\���̃T�C�Y
        public short fComType;      // �ʐM��ʃt���O
        public short fShare;        // ���L�t���O
        public int fLogging;        // �ʐM���M���O�t���O
        [MarshalAs(UnmanagedType.LPStr)]
        public string pLogFile;     // �ʐM���M���O�t�@�C����
        [MarshalAs(UnmanagedType.LPStr)]
        public string pNodeName;    // INtime�m�[�h��
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public short[] Reserved;    // �\��
        public static SXDEF Init()
        {
            SXDEF tmp = new SXDEF();
            tmp.Reserved = new short[2];
            return tmp;
        }
    }



    // ----------------------------------------------
    // �ʐM��ʃR�[�h (for SXDEF.fComType)
    // ----------------------------------------------
    public const short COMSHAREMEM = 0;     // ���L�������ʐM
    public const short COMMAILBOX = 1;      // MailBox�ʐM

    // ----------------------------------------------
    // �ʐM���M���O�t���O (for SXDEF.fLogging)
    // ----------------------------------------------
    public const int COML_DISABLE = 0x1;    // ���M���O����
    public const int COML_ALL = 0x2;    // �펞���M���O
    public const int COML_ALL_INIT = 0x4;   // �펞���M���O(������/�I���̂�)
    public const int COML_ALL_SND = 0x8;    // �펞���M���O(�f�[�^���M�̂�)
    public const int COML_ALL_RCV = 0x10;   // �펞���M���O(�f�[�^��M�̂�)
    public const int COML_ALL_REQ = 0x20;   // �펞���M���O(�R�}���h�v���̂�)
    public const int COML_ERR = 0x40;   // �G���[���M���O
    public const int COML_HERR = 0x80;  // �G���[���M���O�i�`���G���[�̂݁j
    public const int COML_RETRY = 0x100;    // ���g���C���M���O

    // ------------------------------------------------------------------------
    //	�G���f�B�A���ϊ������}�N����`
    // ------------------------------------------------------------------------
    public const short LBCDEV_M3 = 0x1;                             // �f�o�C�X��`�F�e�`�|�l�R
    public const short LBCDEV_MC = 0x2;                             // �f�o�C�X��`�F�q�s�l�b�U�S
    public const short LBCDEV_PC = 0x4;                             // �f�o�C�X��`�F�o�b
                                                                    //#define	MakeLbcType(src, dst)	(((dst)  << 4) | (src))	 // ���ި�ݕϊ��׸ލ쐬ϸ�
    public const short LBC_McToPc = ((LBCDEV_PC << 4) + LBCDEV_MC); // �l�b  �� �o�b
    public const short LBC_PcToMc = ((LBCDEV_MC << 4) + LBCDEV_PC); // �o�b  �� �l�b
    public const short LBC_McToM3 = ((LBCDEV_M3 << 4) + LBCDEV_MC); // �l�b  �� FA-M3
    public const short LBC_M3ToMc = ((LBCDEV_MC << 4) + LBCDEV_M3); // FA-M3 �� �l�b
    public const short LBC_PcToM3 = ((LBCDEV_M3 << 4) + LBCDEV_PC); // �o�b  �� FA-M3
    public const short LBC_M3ToPc = ((LBCDEV_PC << 4) + LBCDEV_M3); // FA-M3 �� �o�b

    [DllImport("rt64eccomnt.dll")] public extern static int InitCommProc(ref SXDEF psxdef, ref int phCom);
    [DllImport("rt64eccomnt.dll")] public extern static int QuitCommProc(int hCom);

    [DllImport("rt64eccomnt.dll")] public extern static int SendData(int hCom, short Dtype, short task, int prm, int size, IntPtr data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendData(int hCom, short Dtype, short task, int prm, int size, [In] byte[] data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendData(int hCom, short Dtype, short task, int prm, int size, ref Rt64ecdata.PARAMETER_DATA data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendData(int hCom, short Dtype, short task, int prm, int size, ref Rt64ecdata.PITCH_ERR_REV data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendData(int hCom, short Dtype, short task, int prm, int size, ref Rt64ecdata.TOOL_H data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendData(int hCom, short Dtype, short task, int prm, int size, ref Rt64ecdata.MCRREG data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendData(int hCom, short Dtype, short task, int prm, int size, ref Rt64ecdata.ONEVARIABLE data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendData(int hCom, short Dtype, short task, int prm, int size, ref Rt64ecdata.TOOL_D data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendData(int hCom, short Dtype, short task, int prm, int size, ref Rt64ecdata.ACO_PRM data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendData(int hCom, short Dtype, short task, int prm, int size, ref Rt64ecdata.NVARIABLE_PRE data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendData(int hCom, short Dtype, short task, int prm, int size, ref Rt64ecdata.VARIABLE_REQ data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendData(int hCom, short Dtype, short task, int prm, int size, ref Rt64ecdata.INITIALPRM data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendData(int hCom, short Dtype, short task, int prm, int size, ref Rt64ecdata.ROMSW data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendData(int hCom, short Dtype, short task, int prm, int size, ref Rt64ecdata.MESSAGEANS data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendData(int hCom, short Dtype, short task, int prm, int size, ref Rt64ecdata.CAMERACMDANSF data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendData(int hCom, short Dtype, short task, int prm, int size, ref Rt64ecdata.PCOND_REC data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendData(int hCom, short Dtype, short task, int prm, int size, ref Rt64ecdata.PCOND_TBL data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendData(int hCom, short Dtype, short task, int prm, int size, ref Rt64ecdata.VARIABLE data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendData(int hCom, short Dtype, short task, int prm, int size, ref Rt64ecdata.PITCH_ERR_REV_AX data);

    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, IntPtr data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, [Out()] byte[] data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.PARAMETER_DATA data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.STATUS data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.IODATA data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.DNCBUFI data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.PITCH_ERR_REV data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.SENSEPOS data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.TOOL_H data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.TEACHSTS data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.VARIABLE data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.TOOL_D data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.RTCTIME data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.CYCLETIME data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.TPCINFO data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.TPCINFOEX data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.ROMVERSION data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref int data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.BINPRG_BLOCK data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.MCRREG data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.ONEVARIABLE data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.ECT_INFOAX data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.ECT_INFOALL data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.ECT_MON data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.VPER data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.HANDLESTS data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.FORTIME data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.ACO_PRM data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.TOOLHSTS data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.TOOLDSTS data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.ASSAXISTBL data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.PRGBLK_INF data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.TOOLDERR data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.NVARIABLE_PRE data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.VARIABLE_REQ data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.INITIALPRM data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.PCONDITION data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.AECDATA data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.AXPOINT data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.VIRPOS_EX data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.WORKORG data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.ROMSW data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.CORRECTDATA data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.PRCSSKIPVPOSNO data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.MESSAGEREQ data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.MESSAGEANS data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.CAMERACMDREQ data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.CAMERACMDANSF data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.PCOND_REC data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.PCOND_TBL data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.ECNCEXTALM data);
    [DllImport("rt64eccomnt.dll")] public extern static int ReceiveData(int hCom, short Dtype, short task, int prm, ref int size, ref Rt64ecdata.PITCH_ERR_REV_AX data);

    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, IntPtr data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.MODECHG data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.JOGSTART data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.ZRNSTART data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.GENERATION data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.PTPSTART data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.PTPASTART data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.LINSTART data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.LINASTART data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.OUTPUTPAT data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.PROGSEL data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.SPCMND data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.SPREVSET data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.SLINSTART data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.SLINASTART data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.COMPINPUT data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.COMPOUTPUT data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.COMPIOBIT data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.OVERCHG data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.ADSC data);                // REQ_ADLOG
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.TPCSEL data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.TPCSEL_POS data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.TPCSEL_ECT data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.TPCSEL_HEADER data);       // REQ_TPCSEL_EX
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.COORDSET data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.AXINTLK data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.AXNGLCT data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.SVONOFFCHG data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.TRQLIMCHG data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.AXCTRLCHG data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.OUTMCD data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.SPINAX data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.TLINESW data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.STEPCHG data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.HANDLEMODE data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.HANDLEKP data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.HANDLEAXIS data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.CYCLECHG data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.MCRVALSET data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.AXMV data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.TRQCMD data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.BLKMVDATA data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.BLKCPYDATA data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.BLKDLDATA data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.ENABLE data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.WTOPPOS data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.PRNOSEL data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.VIRPOSCHG data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.PROGSTRT data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.PARTITION data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.ELCTDNO data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.ELCTDINSTALL data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.ELCTDEXCHPOS data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.MOVEESFARM data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.OPENESFARM data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.GUIDENO data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.GUIDEINSTALL data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.GUIDEEXCHPOS data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.GUIDECHKPOS data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.MOVEGSFARM data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.GUIDECLUMP data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.SPCLUMP data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.ESFMAGMOV data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.WORGPOSCHG data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.GUIDETHROUGH data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.MCHLOCK data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.ANGLESET data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.HANDLEPERMIT data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.FORCEZRNFIN data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.DRYRUN_EX data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.PUMPCMND data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.BONCMND data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.AUTOMODE_OUTPUT data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.SHUTDOWN_START data);
    [DllImport("rt64eccomnt.dll")] public extern static int SendCommand(int hCom, short cmnd, short task, ref Rt64ecdata.BUZZER_ON_OUTPUT data);

    [DllImport("rt64eccomnt.dll")] public extern static int ConvIMData(short type, ref Rt64ecdata.PARAMETER_DATA p_mm, ref Rt64ecdata.PARAMETER_DATA p_inch, short InchMode, long InchAxis);
    [DllImport("rt64eccomnt.dll")] public extern static int ConvIMData(short type, ref Rt64ecdata.PITCH_ERR_REV p_mm, ref Rt64ecdata.PITCH_ERR_REV p_inch, short InchMode, long InchAxis);
    [DllImport("rt64eccomnt.dll")] public extern static int ConvIMData(short type, ref Rt64ecdata.INITIALPRM p_mm, ref Rt64ecdata.INITIALPRM p_inch, short InchMode, long InchAxis);
    [DllImport("rt64eccomnt.dll")] public extern static int ConvIMData(short type, ref Rt64ecdata.ROMSW p_mm, ref Rt64ecdata.ROMSW p_inch, short InchMode, long InchAxis);

    // ------------------------------------------------------------------------
    //	�^�C�}�[�֐��v���g�^�C�v
    // ------------------------------------------------------------------------
    [DllImport("rt64eccomnt.dll")] public extern static void SetTimeOut(ref int origin);
    [DllImport("rt64eccomnt.dll")] public extern static bool CheckTimeOut(int origin, int limit);

}