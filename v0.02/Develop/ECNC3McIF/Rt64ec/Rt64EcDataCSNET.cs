using System;
using System.Runtime.InteropServices;

public class Rt64ecdata
{
    // ------------------------------------------------------------------------
    //		�T�[�{�p�����[�^�f�[�^�\���́i�X�O�W�O�޲ČŒ蒷�j
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct AXIS_PARAMETER    // �e���Ɨ��p�����[�^�\���́i�P�O�O�޲ČŒ蒷�j
    {
        public int InPos;                   // �h�m�o�n�r�� [pls]
        public int ErMax;                   // �΍�����l [pls]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public sbyte[] Reserved0;
        public int Ka;                      // ��Ԏ��萔 [msec]
        public int SKa;                     // �r����Ԏ��萔 [msec]
        public int Dx;                      // �o�s�o���萔 [msec]
        public int PtpFeed;                 // �o�s�o���x [pls/sec]
        public int JogFeed;                 // �i�n�f���葬�x[pls/sec]
        public int SoftLimP;                // �\�t�g���~�b�g�{�� [pls]
        public int SoftLimM;                // �\�t�g���~�b�g�|�� [pls]
        public int OrgDir;                  // ���_���A����
        public int OrgOfs;                  // ���_���� [pls]
        public int OrgPos;                  // ���_���A�����ʒu (���g�p) [pls]
        public int OrgFeed;                 // ���_���A�����葬�x [pls/sec]
        public int AprFeed;                 // ���_���A�A�v���[�`���x [pls/sec]
        public int SrchFeed;                // ���_���A�ŏI�T�[�`���x [pls/sec]
        public int OrgPri;                  // ���_���A����
        public int Homepos;                 // ΰ��߼޼�݈ʒu [pls]
        public int Homepri;                 // ΰ��߼޼�ݏ���
        public int BackL;                   // �o�b�N���b�V���␳�� [pls]
        public int Revise;                  // �`��␳�W��
        public int OrgCsetOfs;              // ���_���A���_�����W
        public int handle_max;              // �ޮ��è��/����ٍő呗�葬�x
        public int handle_ka;               // �ޮ��è��/����ى��������萔
        public int PrcsKa;                  // ���d���H���萔
        public int AecSoftLimP;             // �`�d�b���\�t�g���~�b�g�{��
        public int AecSoftLimM;             // �`�d�b���\�t�g���~�b�g�|��
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
    public struct ADD_PARAMETER         // ����p�����[�^���p�́i�P�Q�O�o�C�g�j
    {
        public long AxAecSoftLim;           // �`�d�b���\�t�g���~�b�g�L�����t���O
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
    public struct PARAMETER_DATA            // �i�X�O�W�O�o�C�g�j
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public AXIS_PARAMETER[] AxisParam;
        public ADD_PARAMETER AddParam;      // ����p�����[�^����`
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
    // �I�v�V�����p�����[�^�f�[�^�\���́i�T�P�Q�޲ČŒ蒷�j
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct OPTIONPRM_DATA
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
        public sbyte[] Reserved;            // ���g�p
        public static OPTIONPRM_DATA Init()
        {
            OPTIONPRM_DATA tmp = new OPTIONPRM_DATA();
            tmp.Reserved = new sbyte[512];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    // �A���T�[�X�e�[�^�X���\���́i�R�S�O�O�޲āj
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct MCSTATUS                  // �S�̏��\����
    {
        public int Status;                  // �S�̃X�e�[�^�X
        public int Alarm;                   // �S�̃A���[��
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public sbyte[] Reserved; // ���g�p
        public static MCSTATUS Init()
        {
            MCSTATUS tmp = new MCSTATUS();
            tmp.Reserved = new sbyte[8];
            return tmp;
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct AXSTATUS                  // �e�����\����
    {
        public int AxStatus;                // ���X�e�[�^�X
        public int AxAlarm;                 // ���A���[��
        public int ComReg;                  // �w�߈ʒu
        public int PosReg;                  // �@�B�ʒu
        public int ErrReg;                  // �΍���
        public int BlockSeg;                // �ŐV�u���b�N�����o����
        public int AbsReg;                  // ��Έʒu
        public int Trq;                     // �g���N
        public int AMrReg;                  // ��Έʒu(�w��:ϼ�ۯ�����)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public sbyte[] Reserved;            // ���g�p
        public static AXSTATUS Init()
        {
            AXSTATUS tmp = new AXSTATUS();
            tmp.Reserved = new sbyte[12];
            return tmp;
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct TASKSTATUS                // �^�X�N���\����
    {
        public int TaskStatus;              // �^�X�N�X�e�[�^�X
        public int TaskAlarm;               // �^�X�N�A���[��
        public short Override;              // ����I�[�o�[���C�h�ݒ�
        public short COverride;             // ��ԃI�[�o�[���C�h�ݒ�
        public short SOverride;             // �厲�I�[�o�[���C�h�ݒ�
        public short ProgramNo;             // �I���E���s�v���O�����ԍ�
        public int StepNo;                  // ���s�X�e�b�v�ԍ�
        public short NNo;                   // �ҋ@�E���s�m�ԍ�
        public short LineNo;                // �ҋ@�E���s�s�ԍ�
        public short LineFlg;               // �ҋ@�E���s�s�ԍ����
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public sbyte[] Reserved;            // ���g�p
        public static TASKSTATUS Init()
        {
            TASKSTATUS tmp = new TASKSTATUS();
            tmp.Reserved = new sbyte[6];
            return tmp;
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct ECNCSTATUS                // ECNC�X�e�[�^�X���\����
    {
        public short Status2;           // �e���ԏ��Q
        public short Status3;           // �e���ԏ��R
        public short Alarm2;                // �A���[�����Q
        public short Alarm3;                // �A���[�����R
        public short Alarm4;                // �A���[�����S
        public short Alarm5;                // �A���[�����T
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public sbyte[] Reserved0;           // �\��
        public int WTopPos;         // �v������l
        public int CorrAng;         // �␳�p�x(8-24�ޯČŒ菬���_)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public sbyte[] Reserved1;           // �\��
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        public short[] ADVal;               // �A�i���O���͒l
        public short AtDischg;          /* ���d���H��					*/
        public int PrcsTargetDist;      /* ���d���H�ڕW���H��			*/
        public int PrcsNowDist;     /* ���d���H���݉��H��			*/
        public short ExtErrSts;         /* �O���G���[���				*/
        public short ExtErrLat;         /* �O���G���[���b�`���			*/
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
    public struct STATUS                    // �A���T�[�X�e�[�^�X���\����
    {
        public MCSTATUS mc;                 // �S�̏��
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public AXSTATUS[] ax;               // �����
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public TASKSTATUS[] task;           // �^�X�N���
        public ECNCSTATUS ecnc;             // ECNC���

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
    public const int S_MCS_ALM = 0x00000001;    // �A���[��������
    public const int S_MCS_SETTING = 0x00000002;    // �S�^�X�N�Z�b�e�B���O���[�h
    public const int S_MCS_SENSE = 0x00000010;  // �����Z���T�[���b�`��
                                                //public const int						= 0x00000020;	//
    public const int S_MCS_PRGCHG = 0x00000040; // ����v���O�����f�[�^�ύX
    public const int S_MCS_RTCWARN = 0x00000080;    // ����������׃��[�j���O(87.5%)
    public const int S_MCS_RTCFAIL = 0x00000100;    // ����������׉ߑ�
    public const int S_MCS_FDTWARN = 0x00000200;    // FDT�Ǎ����[�j���O
    public const int S_MCS_CYCLETIME_EN = 0x00000400;   // �T�C�N���^�C���L��

    // for STATUS.mc.Alarm
    //	public const int S_MCA_EMS				= 0x00000001;	// ����~
    public const int S_MCA_BACKUP = 0x00000002; // �o�b�N�A�b�v�G���[
    public const int S_MCA_PARAMETER = 0x00000004;  // �p�����[�^���ݒ�G���[
    public const int S_MCA_ROMSW_BKUP = 0x00000008; // ROMSW�ޯ����ߴװ
    public const int S_MCA_ECT_INIT = 0x00000010;   // EtherCAT�������G���[
    public const int S_MCA_ECT_WCOM = 0x00000020;   // EtherCAT�S�̒ʐM�G���[
                                                    // public const int						= 0x00000040;	// 
    public const int S_MCA_FDTFAIL = 0x00000080;    // FDT�Ǎ��G���[
                                                    // public const int						= 0x00000100;	// 
    public const int S_MCA_ECT_IO_COMM = 0x00000200;    // EtherCAT IO�ʐM�G���[
    public const int S_MCA_SYSTEM = 0x00008000; // �V�X�e���G���[

    // for STATUS.ax[n].AxStatus
    public const int S_AXS_INPOS = 0x00000001;  // �C���|�W�V����
    public const int S_AXS_ACC_NZE = 0x00000002;    // ���������܂�L��
    public const int S_AXS_SVON = 0x00000004;   // �T�[�{�n�m
    public const int S_AXS_ZRN = 0x00000008;    // ���_���A����
    public const int S_AXS_AXMV = 0x00000010;   // �Ɨ��ʒu���ߒ�
    public const int S_AXS_AXMVSTP = 0x00000020;    // �Ɨ��ʒu���ߒ�~��
    public const int S_AXS_SPIN = 0x00000040;   // SPIN���쒆
    public const int S_AXS_SPINSTP = 0x00000080;    // SPIN��~��
    public const int S_AXS_TRQCTRL = 0x00000100;    // �g���N���䒆
    public const int S_AXS_ORGFIX = 0x00000200; // ���_�m��ς�
    public const int S_AXS_2ND_SLIMIT_M = 0x00000400;   // �|������Q�\�t�g���~�b�g�L���i�y���̂݁j
    public const int S_AXS_VIRACTDIS = 0x00000800;  // ���z�_/����_�ړ����얳��(A/B/C���̂�)

    // for STATUS.ax[n].AxAlarm
    public const int S_AXA_ERALM_P = 0x00000001;    // �{�����΍��ߑ�
    public const int S_AXA_ERALM_M = 0x00000002;    // �|�����΍��ߑ�
    public const int S_AXA_SALM = 0x00000004;   // �T�[�{�A���v�A���[��
    public const int S_AXA_SLIMIT_P = 0x00000008;   // �{�����\�t�g���~�b�g
    public const int S_AXA_SLIMIT_M = 0x00000010;   // �|�����\�t�g���~�b�g
    public const int S_AXA_HLIMIT_P = 0x00000020;   // �{�����n�[�h���~�b�g
    public const int S_AXA_HLIMIT_M = 0x00000040;   // �|�����n�[�h���~�b�g
    public const int S_AXA_COMLIMIT_P = 0x00000080; // �{�����p���X�����ߑ�
    public const int S_AXA_COMLIMIT_M = 0x00000100; // �|�����p���X�����ߑ�
    public const int S_AXA_SPWOFF = 0x00000200; // �T�[�{��d���n�e�e
    public const int S_AXA_ECT_AXCOM = 0x00000400;  // EtherCAT�e���ʐM�G���[
    public const int S_AXA_ECT_MLTCMD = 0x00000800; // EtherCAT���d�R�}���h
    public const int S_AXA_ECT_USCMD = 0x00001000;  // EtherCAT���Ή��R�}���h�G���[
    public const int S_AXA_ILLEGAL_ACT = 0x00002000;    // �s���w�߃G���[
    public const int S_AXA_TOUCHERR_P = 0x00004000; // �{�����ڐG���m�G���[
    public const int S_AXA_TOUCHERR_M = 0x00008000; // �|�����ڐG���m�G���[
    public const int S_AXA_CFA_INTERLOCK = 0x00010000;  // �R���b�g�t�B���K�[�A�[���O�i���C���^�[���b�N���蓮����G���[

    // for STATUS.task[n].TaskStatus
    public const int S_TKS_ALM = 0x00000001;    // �A���[��������
    public const int S_TKS_FG_END = 0x00000002; // �e�f����
    public const int S_TKS_FG_STOP = 0x00000004;    // �e�f���f��
    public const int S_TKS_FG_BUN = 0x00000008; // �e�f���z��
    public const int S_TKS_EXEC = 0x00000010;   // �v���O�����^�]��
    public const int S_TKS_STOP = 0x00000020;   // �v���O������~��
    public const int S_TKS_BLKS = 0x00000040;   // �u���b�N�r����~
    public const int S_TKS_SEQ_END = 0x00000080;    // �e��V�[�P���X����
    public const int S_TKS_SINGLE = 0x00000100; // �V���O���X�e�b�v���[�h
    public const int S_TKS_TEACH = 0x00000200;  // �e�B�[�`���O���[�h
    public const int S_TKS_CYCLE = 0x00000400;  // �T�C�N���^�]���[�h
    public const int S_TKS_MLK_STS = 0x00000800;    // �}�V�����b�N�i���Ή��j
    public const int S_TKS_MODE = 0x0000F000;   // ���[�h���G���A

    public const int S_TKS_INPOS = 0x00010000;  // ���蓖�Ď� �C���|�W�V����
    public const int S_TKS_ACC_NZE = 0x00020000;    // ���蓖�Ď� ���������܂�L��
    public const int S_TKS_SVON = 0x00040000;   // ���蓖�Ď� �T�[�{�n�m
    public const int S_TKS_ZRN = 0x00080000;    // ���蓖�Ď� ���_���A����
    public const int S_TKS_AXMV = 0x00100000;   // ���蓖�Ď� �Ɨ��ʒu���ߒ�
    public const int S_TKS_AXMVSTP = 0x00200000;    // ���蓖�Ď� �Ɨ��ʒu���ߒ�~��

    public const int S_TKS_SENSE = 0x01000000;  // �����Z���T�[���b�`��
    public const int S_TKS_TANG = 0x02000000;   // �y���ڐ�����n�m
    public const int S_TKS_REEL_END = 0x04000000;   // �ŏI�w�����ُ�x��(����)

    // for STATUS.task[n].TaskAlarm
    public const int S_TKA_PRGERR = 0x00000001; // �v���O�������s�G���[
    public const int S_TKA_MOUTERR = 0x00000002;    // �l�R�[�h���s�G���[
    public const int S_TKA_AXIS = 0x00000004;   // ���蓖�Ď��G���[
    public const int S_TKA_FGERR = 0x00000008;  // �e�f�������Z�G���[
    public const int S_TKA_POWEROFF = 0x00000010;   // �T�[�{�n�e�e�G���[
    public const int S_TKA_EXTALMA = 0x00000020;    // �O���A���[���`�G���[
    public const int S_TKA_EXTALMB = 0x00000040;    // �O���A���[���a�G���[
    public const int S_TKA_EXTALMC = 0x00000080;    // �O���A���[���b�G���[
    public const int S_TKA_EMS = 0x00000100;    // ����~
    public const int S_TKA_ECNC = 0x00000200;   // ECNC�A���[��

    // for STATUS.ecnc.Status2
    public const short S_MCS2_OPTSTOP = 0x0001; // �I�v�V���i���X�g�b�v�L��
    public const short S_MCS2_TOUCHSENSE = 0x0002;  // �ڐG���m�L��
    public const short S_MCS2_INCREFSET_MOV = 0x0004;   // ���Α���_�ݒ莞���ړ��L��
    public const short S_MCS2_SINGLE = 0x0008;  // �V���O���X�e�b�v���[�h
    public const short S_MCS2_SEQ_END = 0x0010; // �e��V�[�P���X����
    public const short S_MCS2_XY_ILOCK_DIS = 0x0020;    // X/Y������ۯ�����
    public const short S_MCS2_CORR_ANG = 0x0040;    // ��۸��ъJ�n���p�x�␳�L��
    public const short S_MCS2_IN_CORR_ANG = 0x0080; // �p�x�␳��
    public const short S_MCS2_BLOCKSKIP = 0x0100;   // BlockSkip�L��
    public const short S_MCS2_M02 = 0x0200; // M02�ɂ����۸��яI��
    public const short S_MCS2_MCNLOCK = 0x0400; // �}�V�����b�N�L��
    public const short S_MCS2_HANDLEPERMIT = 0x0800;    // �蓮�p���T�[���싖��
    public const short S_MCS2_HANDLE_EN = 0x1000;   // �蓮�p���T�[����L��
                                                    //public const short 						= 0x2000;
    public const short S_MCS2_MESSAGE_REQ = 0x4000; // ���b�Z�[�W�\���v��
                                                    //public const short 						= 0x8000;

    //for STATUS.ecnc.Status3
    public const short S_MCS3_PSTOP_INPUT = 0x0001; // STOP���͂ɂ��o���s��~��
    public const short S_MCS3_AUTOMODE_OUTPUT = 0x0002; // �������[�h���o��
    public const short S_MCS3_VARIABLE_REQ = 0x0004;    // �}�N���ϐ�����/�Ǐo�v��
    public const short S_MCS3_CAMERA_REQ = 0x0008;  // �J�����R�}���h�v��
    public const short S_MCS3_SHUTDWN_REQ = 0x0010; // �V���b�g�_�E������v��

    // for STATUS.ecnc.Alarm2
    public const short S_MCA2_EX1E602ALM = 0x0001;  // ECNC��pIF�{�[�h(EX1-E602)�A���[��
    public const short S_MCA2_EXTALM = 0x0002;  // ECNC��p�O���A���[��
    public const short S_MCA2_EXTALMFACT = 0x0004;  // ECNC��p�O���A���[���v������
    public const short S_MCA2_GUID_INTRF = 0x0008;  // �K�C�h�z���_���G���[
                                                    //public const short 						= 0x0010;
                                                    //public const short 						= 0x0020;
    public const short S_MCA2_PRCS_BUCKLING = 0x0040;   // �ʏ�d�ɕ��d���H�������G���[
                                                        //public const short 						= 0x0080;
    public const short S_MCA2_Z20 = 0x0100; // �y�Q�O�G���[
    public const short S_MCA2_REFOVER = 0x0200; // �[�ʈʒu�v�Z�͈͊O�G���[
    public const short S_MCA2_IDX_POSITION = 0x0400;    // �C���f�b�N�X�ʒu���߃G���[
    public const short S_MCA2_DISCHTIMEOUT = 0x0800;    // ���d���H�^�C���A�E�g�G���[
    public const short S_MCA2_PUMPCOLLET_IL = 0x1000;   // �|���v�n�m�E�R���b�g�A���N�����v�����w�߃G���[
    public const short S_MCA2_PRSKPVPOS_OVER = 0x2000;  // ���d���H�X�L�b�v���������z�_�o�^�ԍ��͈͊O�G���[

    // for STATUS.ecnc.Alarm3
    public const short S_MCA3_SOURCEPRSS = 0x0001;  // �����G���[
    public const short S_MCA3_GIDCHG = 0x0002;  // �K�C�h�����m�F�G���[
    public const short S_MCA3_COLINST = 0x0004; // �d�Ɏ擾�s�G���[
    public const short S_MCA3_COLREMOV = 0x0008;    // �d�ɕԋp�s�G���[
    public const short S_MCA3_ELCTDNO = 0x0010; // �d�ɔԍ����ݒ�G���[
    public const short S_MCA3_GUIDENO = 0x0020; // �K�C�h�ԍ����ݒ�G���[
    public const short S_MCA3_CFARMPOS = 0x0040;    // �`�d�b�J�n���گ�̨ݶް��шʒu�s���G���[
    public const short S_MCA3_GHARMPOS = 0x0080;    // �`�d�b�J�n���K�C�h�z���_�[�A�[���ʒu�s���G���[
    public const short S_MCA3_CFOPEN = 0x0100;  // �`�d�b�J�n���R���b�g�t�B���K�[���I�[�v���G���[
    public const short S_MCA3_COLCLUMP = 0x0200;    // �`�d�b�J�n���R���b�g���N�����v�G���[
    public const short S_MCA3_GIDCLUMP = 0x0400;    // �`�d�b�J�n���K�C�h���N�����v�G���[
    public const short S_MCA3_PATRND = 0x0800;  // �߰è��ݓ��P���װ
    public const short S_MCA3_PATRANGE = 0x1000;    // �߰è��ݔ͈͊O�װ
    public const short S_MCA3_GDTHROUGH = 0x2000;   // �K�C�h�ђʓ���G���[

    // for STATUS.ecnc.Alarm4
    public const short S_MCA4_COLFNGR_ARM_TM = 0x0001;  // �گ�̨ݶް��ѓ�����ѱ�Ĵװ
    public const short S_MCA4_COLFNGR_OPN_TM = 0x0002;  // �گ�̨ݶް�����/�۰����ѱ�Ĵװ
    public const short S_MCA4_COLCLUMP_TM = 0x0004; // �گĸ����/�ݸ������ѱ�Ĵװ
    public const short S_MCA4_GUID_ARM_TM = 0x0008; // �޲�����ް��ѓ�����ѱ�Ĵװ
    public const short S_MCA4_GUIDCLUMP_TM = 0x0010;    // �޲�޸����/�ݸ������ѱ�Ĵװ
                                                        //public const short 						= 0x0020;
                                                        //public const short 						= 0x0040;
                                                        //public const short 						= 0x0080;
    public const short S_MCA4_COLFNGR_ARM_IL = 0x0100;  // �گ�̨ݶް��я�ԕs��v
    public const short S_MCA4_COLFNGR_OPN_IL = 0x0200;  // �گ�̨ݶް�����/�۰�ޏ�ԕs��v
    public const short S_MCA4_COLCLUMP_IL = 0x0400; // �گĸ����/�ݸ���ߏ�ԕs��v
    public const short S_MCA4_GUID_ARM_IL = 0x0800; // �޲�����ް��ѓ����ԕs��v
    public const short S_MCA4_GUIDCLUMP_IL = 0x1000;    // �޲�޸����/�ݸ���ߏ�ԕs��v
                                                        //public const short 						= 0x2000;
                                                        //public const short 						= 0x4000;
                                                        //public const short 						= 0x8000;

    // for STATUS.ecnc.Alarm5
    public const short S_MCA5_MCR_UNDEFINE = 0x0001;    // �}�N���ϐ��ǂݍ���/�������ݐ��̕s���G���[
                                                        //public const short 						= 0x0002;
                                                        //public const short 						= 0x0004;
                                                        //public const short 						= 0x0008;
    public const short S_MCA5_SPXMCR_READ = 0x0010; // RTMC64(��SPX)�Ǘ��}�N���ϐ��ǂݍ��݃G���[
    public const short S_MCA5_SPXMCR_WRITE = 0x0020;    // RTMC64(��SPX)�Ǘ��}�N���ϐ��������݃G���[
    public const short S_MCA5_OUTOFRANGE = 0x0040;  // �͈͊O�}�N���ϐ��w��G���[
                                                    //public const short 						= 0x0080;
    public const short S_MCA5_OPERATION = 0x0100;   // �}�N�����Z�G���[
                                                    //public const short 						= 0x0200;
                                                    //public const short 						= 0x0400;
                                                    //public const short 						= 0x0800;
    public const short S_MCA5_MSG_SEQ = 0x1000; // ���b�Z�[�W�\������M�V�[�P���X�G���[
    public const short S_MCA5_MSG_ANS = 0x2000; // ���b�Z�[�W�\�����ʃG���[

    // ------------------------------------------------------------------------
    //	EtherCAT�X�e�[�^�X���\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct ECT_WHOLESTATUS
    {
        public short mst_init_errc;     // EtherCAT�}�X�^�[�������G���[�R�[�h
        public short mst_ESM;           // EtherCAT ESM(EtherCAT State Machine)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Alignment;        // 
        public int mst_NotifiCode;      // EtherCAT�}�X�^�[�ʒm�R�[�h
        public int mst_whcom_errc;      // EtherCAT�}�X�^�[�S�̃G���[�R�[�h
        public long mst_axcom_err;      // EtherCAT�}�X�^�[�G���[�������t���O
        public long no_rcv_err;         // �f�[�^��M�G���[�������t���O
        public long time_out_err;       // �^�C���A�E�g�G���[�������t���O
        public long watchdog_err;       // WDT�G���[�������t���O
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] Reserved;         // �\��
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
        public int mst_axNotifiCode;    // EtherCAT�}�X�^�[�ʒm�R�[�h
        public int mst_axcom_errc;      // EtherCAT�}�X�^�[�ʒm�G���[�R�[�h
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
        public byte[] Reserved;         // �\��
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
    //	EtherCAT��M�f�[�^�\����
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
    //	�ėp���o�͏��\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct IODATA
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 116)]
        public ushort[] InputData;      // �ėp����
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public ushort[] OutputData;     // �ėp�o��
        public static IODATA Init()
        {
            IODATA tmp = new IODATA();
            tmp.InputData = new ushort[116];
            tmp.OutputData = new ushort[64];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	�c�m�b�o�b�t�@���\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct DNCBUFI
    {
        public int size;                    // �o�b�t�@�g�p�e�ʁi�o�C�g�j
        public int Free;                    // �o�b�t�@�󂫗e�ʁi�o�C�g�j
        public static DNCBUFI Init()
        {
            return (new DNCBUFI());
        }
    }

    // ------------------------------------------------------------------------
    //	�Z���T�[���b�`�ʒu���\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct SENSEPOS
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] SenPos;            // �ݻ�ׯ��߼޼�݁i�_�����W�n�j
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] SenPosA;           // �ݻ�ׯ��߼޼�݁i��޿���W�n�j
        public static SENSEPOS Init()
        {
            SENSEPOS tmp = new SENSEPOS();
            tmp.SenPos = new int[9];
            tmp.SenPosA = new int[9];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	�v���O�����{�����[�����x���\���́i�P�O�S�޲ČŒ蒷�j
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct BINPRG_LABEL
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] Reserved1;        // �\��
        public short BlockNumber;       // �L���u���b�N��
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 22)]
        public byte[] Reserved2;        // �\��
        public short ProgramType;       // ��۸��Ѻ�������(0:T� 1:G)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 76)]
        public byte[] Reserved3;        // �\��
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
    //	�v���O�����P�u���b�N�f�[�^�\���́i�P�O�S�޲ČŒ蒷�j
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct BINPRG_BLOCK
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 101)]
        public byte[] Reserved;         // �\��
        public byte PrgType;            // ��۸��Ѻ�������(0:T� 1:G)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] Reserved2;        // ���g�p
        public static BINPRG_BLOCK Init()
        {
            BINPRG_BLOCK tmp = new BINPRG_BLOCK();
            tmp.Reserved = new byte[101];
            tmp.Reserved2 = new byte[2];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	�`�^�c���o�n�r���\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct AD_POS
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public int[] Ad;                // �`�^�c�l
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] Pos;               // �_�����W�n�@�B�ʒu
        public static AD_POS Init()
        {
            AD_POS tmp = new AD_POS();
            tmp.Ad = new int[4];
            tmp.Pos = new int[9];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	�s�o�b���M���O���\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct TPCINFO
    {
        public short Log;           // �s�o�b�ް�۷�ݸ��׸�
        public short Num;           // �s�o�b�ް�۷�ݸ��߲�Đ�
        public static TPCINFO Init()
        {
            return (new TPCINFO());
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct TPCINFOEX
    {
        public short Log;           // ���M���O���t���O�
        public short Reserved;      // �\��
        public int Size;            // �o�b�t�@�g�p�e�ʁi�o�C�g�j
        public int Free;            // �o�b�t�@�󂫗e�ʁi�o�C�g�j
        public static TPCINFOEX Init()
        {
            return (new TPCINFOEX());
        }
    }

    // ------------------------------------------------------------------------
    //	�s�o�b�f�[�^�\����
    // ------------------------------------------------------------------------
    //----------
    // TPCH_LOG_POS�p
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct TPCDAT_POS
    {
        public int pr1;     // ��P���@�B�ʒu
        public ushort hi1;      // �ėp���͂P���
        public ushort ho1;      // �ėp�o�͂P���
        public int pr2;     // ��Q���@�B�ʒu
        public ushort hi2;      // �ėp���͂Q���
        public ushort ho2;      // �ėp�o�͂Q���
        public static TPCDAT_POS Init()
        {
            return (new TPCDAT_POS());
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct TPC           // TPCDAT_POS�Ɠ����^
    {
        public int pr1;     // ��P���@�B�ʒu
        public short hi1;       // �ėp���͂P���
        public short ho1;       // �ėp�o�͂P���
        public int pr2;     // ��Q���@�B�ʒu
        public short hi2;       // �ėp���͂Q���
        public short ho2;       // �ėp�o�͂Q���
        public static TPC Init()
        {
            return (new TPC());
        }
    }

    //----------
    // TPCH_LOG_ECT�p
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
    //	�u�d�q�^�o�d�q���\����
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
    //	�s�b�`�G���[�␳�p�p�����[�^���\���́i�U�V�W�S�O�o�C�g�Œ蒷�j
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct REV_AX // �e���␳�p�p�����[�^
    {
        public int RevMagnify;  // �␳�{��
        public int RevSpace;    // �␳�Ԋu
        public int RevTopNo;    // �␳�f�[�^�擪�ԍ�
        public int RevMCnt;     // �|���␳��Ԑ�
        public int RevPCnt;     // �{���␳��Ԑ�
        public static REV_AX Init()
        {
            return (new REV_AX());
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct PITCH_ERR_REV
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public REV_AX[] RevAx;  // �e���␳�p�p�����[�^
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 33280)]
        public short[] RevDt;   // �␳�f�[�^
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
    //	�e���s�b�`�G���[�␳�p�p�����[�^���\���́i�U�O�O�P�U�o�C�g�Œ蒷�j
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct PITCH_ERR_REV_AX
    {
        public int RevMagnify;  // �␳�{��
        public int RevSpace;    // �␳�Ԋu
        public int RevMCnt;     // �|���␳��Ԑ�
        public int RevPCnt;     // �{���␳��Ԑ�
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 30000)]
        public short[] RevDt;   // �␳�f�[�^
        public static PITCH_ERR_REV_AX Init()
        {
            PITCH_ERR_REV_AX tmp = new PITCH_ERR_REV_AX();
            tmp.RevDt = new short[30000];
            return tmp;
        }
    }


    // ------------------------------------------------------------------------
    //	�c�[�����␳�p�p�����[�^���\����
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
    //	�}�N���ϐ��f�[�^
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
    //	�g���}�N���ϐ��f�[�^
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
    //	�g���}�N���ϐ��f�[�^
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

    // �O���[�o���}�N���ϐ����						�F#1000�`#1099(QWORD(double)�A�N�Z�X�̈�)
    public const int VARQ_GLB_STR = 1000;               // �擪�A�h���X
    public const int VARQ_GLB_NUM = 100;                // �f�[�^��

    // ���_�[���L�}�N���ϐ�(RTMC64 -> PLC)���		�F#2000�`#2499(QWORD(double)�A�N�Z�X�̈�)
    public const int VARQ_LDW_STR = 2000;               // �擪�A�h���X
    public const int VARQ_LDW_NUM = 500;                // �f�[�^��

    // ���_�[���L�}�N���ϐ�(RTMC64 <- PLC)���		�F#2500�`#2999(QWORD(double)�A�N�Z�X�̈�)
    public const int VARQ_LDR_STR = 2500;               // �擪�A�h���X
    public const int VARQ_LDR_NUM = 500;                // �f�[�^��

    // �g���O���[�o���}�N���ϐ����					�F#10000�`#99999(QWORD(double)�A�N�Z�X�̈�)
    public const int EXTVARQ_GLB_STR = 10000;           // �擪�A�h���X
    public const int EXTVARQ_GLB_NUM = 90000;           // �f�[�^��


    // ------------------------------------------------------------------------
    //	��ʃ}�N���ϐ��C�Ӑ�����/�Ǐo�p�����[�^�\��\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct NVARIABLE_PRE
    {
        public int Start;                   // ����M�J�n�}�N���ϐ��ԍ�
        public int Num;                 // ����M�}�N���ϐ���
        public static NVARIABLE_PRE Init()
        {
            return (new NVARIABLE_PRE());
        }
    }

    // ------------------------------------------------------------------------
    //	�}�N���ϐ�����/�Ǐo�v���f�[�^�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct VARIABLE_REQ
    {
        public int Start;               // ����M�J�n�}�N���ϐ��ԍ�
        public int Num;             // ����M�}�N���ϐ���
        public short Dir;               // �]������
                                        //   0:RTMC64-EC��Windows
                                        //   1:Windows��RTMC64-EC
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Reserved;         // �\��
        public static VARIABLE_REQ Init()
        {
            VARIABLE_REQ tmp = new VARIABLE_REQ();
            tmp.Reserved = new byte[6];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	��ԑO�������p�����[�^�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct ACO_PRM
    {
        public int aco_acot;                    // ��ԑO����������w��
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
        public int[] aco_accdat;     // ��ԑO�������ŏ�����ײ�ސ؊������x
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
        public short[] aco_ovrdat;  // ��ԑO�������ŏ�����ײ���ް�
        public short Reserved;   // ���g�p
        public static ACO_PRM Init()
        {
            ACO_PRM tmp = new ACO_PRM();
            tmp.aco_accdat = new int[7];
            tmp.aco_ovrdat = new short[7];
            return tmp;
        }
    }


    // for ACO_PRM.aco_acot
    public const int ACO_TMMIN = 0;         // ��ԑO���������萔�ŏ��l
    public const int ACO_TMMAX = 16383;     // ��ԑO���������萔�ő�l

    // for ACO_PRM.aco_accdat[]
    public const int ACO_ACCMIN = 0;        // ��ԑO�������؊������x�ŏ��l
                                            //	public const int ACO_ACCMAX = 8000000;	// ��ԑO�������؊������x�ő�l
    public const int ACO_ACCMAX = (1000000000 * 2);  // ��ԑO�������؊������x�ő�l

    // for ACO_PRM.aco_ovrdat[]
    public const short ACO_OVRMIN = 1;      // ��ԑO����������ײ�ލŏ��l
    public const short ACO_OVRMAX = 100;    // ��ԑO����������ײ�ލő�l

    // ------------------------------------------------------------------------
    //	�e�B�[�`���O���p�p�����[�^���\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct TEACHSTS
    {
        public int Status;  // �e�B�[�`���O�X�e�[�^�X
        public int StepNo;  // ���s�X�e�b�v�ԍ�
        public int TchStepNo; // �e�B�[�`���O�X�e�b�v�ԍ�
        public int Reserved;  // ���g�p
        public static TEACHSTS Init()
        {
            return (new TEACHSTS());
        }
    }

    // for TEACHSTS.Status
    public const int T_TEACH = 0x1; // �e�B�[�`���O���[�h
    public const int T_TEACHSTP = 0x2;  // �e�B�[�`���O�J�n�X�e�b�v
    public const int T_TEACHEN = 0x4;   // �e�B�[�`���O�\�X�e�b�v
    public const int T_TEACHSTPPRV = 0x8;   // è��ݸފJ�n�ï�߂̑O�ï��

    // ------------------------------------------------------------------------
    //	��p������\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct HANDLESTS
    {
        public short handle_mode;   // ��p�L���t���O
        public short kp;            // ��p�ݒ�{��
        public short ax1;           // ��p��P��
        public short ax2;           // ��p��Q��
        public static HANDLESTS Init()
        {
            return (new HANDLESTS());
        }
    }

    // for HANDLESTS.handle_mode
    public const short HDL_MD_HANDLE = 1;       // ��p���[�h
    public const short HDL_MD_JOYSTICK = 2;         // �W���C�X�e�B�b�N���[�h
    public const short HDL_MD_ENABLE = 0x1000;  // ��p/�W���C�X�e�B�b�N�L��

    // ------------------------------------------------------------------------
    //		�q�n�l�\�t�g�o�[�W�����f�[�^�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct ROMVERSION
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] Version;  // �o�[�W����������
        public short EvenSum;   // SUM:even (rom)
        public short OddSum;    // SUM:odd  (rom)
        public short FlashSum;  // SUM:�r�g�����e�k�`�r�g
        public short FlashFlg;  // �r�g�����e�k�`�r�g�g�p�t���O
        public short KindID;    // �@��h�c
        public int SerialID;    // �V���A���h�c
        public int ProductID;   // �v���_�N�g�h�c
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
    public const short SID_BKIND = 30;      // �V���A���i���o�[�i�@��h�c�j
    public const string SID_BKIND_STR = "RT64M3";   // �V���A���i���o�[�i�@��h�c�j

    // ------------------------------------------------------------------------
    //	�q�s�b�������Ԋi�[�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct RTCTIME
    {
        public int RtcMax;      // �ő又������[us]
        public int RtcNow;      // ���ݏ�������[us]
        public static RTCTIME Init()
        {
            return (new RTCTIME());
        }
    }

    // ------------------------------------------------------------------------
    //	�ʐM�����ݒ�l�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct CYCLETIME
    {
        public int CycleTime;   // �������[us]
        public static CYCLETIME Init()
        {
            return (new CYCLETIME());
        }
    }

    //	public const double TIMERCLOCK = 2.609375;  // �������Զ��ĸۯ�(MHx)
    public const double TIMERCLOCK = 1;     // �������Զ��ĸۯ�INtime�̏ꍇ�Aus�P�ʂŏ��擾(MHx)/

    // ------------------------------------------------------------------------
    //	�t�H�A�O�����h�������Ԋi�[�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct FORTIME
    {
        public int ForMax;      // �ő又������[us]
        public int ForNow;      // ���ݏ�������[us]
        public static FORTIME Init()
        {
            return (new FORTIME());
        }
    }

    // ------------------------------------------------------------------------
    //	�c�[���a�␳�p�p�����[�^���\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct TOOL_D    // �i�W�O�o�C�g�j
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public int[] d;     // �c�[���a�␳�f�[�^
        public static TOOL_D Init()
        {
            TOOL_D tmp = new TOOL_D();
            tmp.d = new int[20];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	�v���O�����u���b�N���\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct PRGBLK_INF_SUB
    {
        public short PrgNo;     // �v���O�����ԍ�
        public short TaskNo;    // �^�X�N�ԍ�
        public static PRGBLK_INF_SUB Init()
        {
            return (new PRGBLK_INF_SUB());
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct PRGBLK_INF    // �i�Q�T�U�o�C�g�j
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
    //	�t�B�[�h�o�b�N�J�E���^�f�[�^�\���́i�W�޲ČŒ蒷�j
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct FBCOUNT
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public int[] cntr;      // �e�a�P�E�e�a�Q�ώZ�l
        public static FBCOUNT Init()
        {
            FBCOUNT tmp = new FBCOUNT();
            tmp.cntr = new int[2];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	�}�N���ϐ��f�[�^�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct MCRREG
    {
        public double Val;                  // �}�N���ϐ��l
        public static MCRREG Init()
        {
            return (new MCRREG());
        }
    }
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct ONEVARIABLE
    {
        public double var;                  // �}�N���ϐ��l
        public static ONEVARIABLE Init()
        {
            return (new ONEVARIABLE());
        }
    }

    // ------------------------------------------------------------------------
    //	�H��␳���\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct TOOLHSTS
    {
        public short toolh_en;      // �␳�L���t���O
        public short toolh_no;      // �I�𒆕␳No
        public static TOOLHSTS Init()
        {
            return (new TOOLHSTS());
        }
    }

    // ------------------------------------------------------------------------
    //	�H��a�␳���\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct TOOLDSTS
    {
        public short toold_en;      // �␳�L���t���O
        public short toold_no;      // �I�𒆕␳No
        public static TOOLDSTS Init()
        {
            return (new TOOLDSTS());
        }
    }

    // ------------------------------------------------------------------------
    //	�H��a�␳�G���[���\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct TOOLDERR
    {
        public int StepNo;          // �G���[�����X�e�b�v�ԍ�
        public int ErrCode;         // �G���[�R�[�h
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
    public const ushort TDERR_STATE_ANOMALY = (1 << 0); // ���̕s���G���[�i������Ԉُ�j
    public const ushort TDERR_DCHG_CIR = (1 << 1);  // �~�ʎw�߂ł̌a�␳���[�h�ύX�G���[
    public const ushort TDERR_INHB_COMMAND = (1 << 2);  // �a�␳���̋֎~���ߎw��G���[
    public const ushort TDERR_MACRO = (1 << 3); // �}�N���ϐ��G���[
    public const ushort TDERR_D_RANGE = (1 << 4);   // �a�w��͈͊O�G���[
    public const ushort TDERR_LIN_NOMOVE = (1 << 5);    // �ړ��ʂȂ��G���[
    public const ushort TDERR_LIN_SE_SAME = (1 << 6);   // �n�_/�I�_��v�G���[
    public const ushort TDERR_CIR_CENT_IL = (1 << 7);   // �~�ʒ��S�_���Z�G���[
    public const ushort TDERR_LINLIN_NOCP = (1 << 8);   // ��_�Ȃ��G���[ (LIN �� LIN)
    public const ushort TDERR_LINCIR_NOCP = (1 << 9);   // ��_�Ȃ��G���[ (LIN �� CIR�ACIR �� LIN)
    public const ushort TDERR_CIRCIR_NOCP = (1 << 10);  // ��_�Ȃ��G���[ (CIR �� CIR)
    public const ushort TDERR_LIN_REVERSE = (1 << 11);  // �ړ��������]�G���[�iLIN�j
    public const ushort TDERR_CIR_REVERSE = (1 << 12);  // �ړ��������]�G���[�iCIR�j


    // ------------------------------------------------------------------------
    //	�ʒu���߃|�C���g�e�[�u���f�[�^�\���́i�R�U�޲ČŒ蒷�j
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct AXIS_POINT    // �|�C���g�f�[�^�\���́i�R�U�޲ČŒ蒷�j
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] AxisPos;   // �e���A�u�\�ʒu
        public static AXIS_POINT Init()
        {
            AXIS_POINT tmp = new AXIS_POINT();
            tmp.AxisPos = new int[9];
            return tmp;
        }
    }

    public const short POINTTBLMAX = 400;   // �ʒu���߃|�C���g�e�[�u���ő吔

    // ------------------------------------------------------------------------
    //	�����蓖�ď��\����
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
        public long axis_en;    // �L�����t���O
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
    //	���샂�[�h�f�[�^�ύX�R�}���h�p�����[�^�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct MODECHG
    {
        public short mode;      // ���샂�[�h
        public static MODECHG Init()
        {
            return (new MODECHG());
        }
    }

    // ------------------------------------------------------------------------
    //	�i�n�f�ړ��J�n�R�}���h�p�����[�^�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct JOGSTART
    {
        public short AxisFlag;  // �ړ����I���t���O
        public short JogVect;   // ���ړ������t���O
        public static JOGSTART Init()
        {
            return (new JOGSTART());
        }
    }

    // ------------------------------------------------------------------------
    //	���_���A�ړ��J�n�R�}���h�p�����[�^�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct ZRNSTART
    {
        public short AxisFlag;  // �ړ����I���t���O
        public static ZRNSTART Init()
        {
            return (new ZRNSTART());
        }
    }

    // ------------------------------------------------------------------------
    //	�o�b�N�A�b�v�������������R�}���h�p�����[�^�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct GENERATION
    {
        public short InitCmnd;  // �������f�[�^�I���t���O
        public static GENERATION Init()
        {
            return (new GENERATION());
        }
    }

    // for GENERATION.InitCmnd
    public const short GEN_PARAM = 0x1; // �p�����[�^�������w��
    public const short GEN_PROGRAM = 0x2;   // ����v���O�����������w��
    public const short GEN_POSITION = 0x4;  // �A�u�\���W�������w��
    public const short GEN_VARIABLE = 0x8;  // �}�N���ϐ��������w��

    // ------------------------------------------------------------------------
    //	�o�s�o�ړ��J�n�R�}���h�p�����[�^�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct PTPSTART
    {
        public int AxisFlag;    // �ړ����I���t���O
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] IncAxis;   // �e���ݸ���Ĉړ���
        public static PTPSTART Init()
        {
            PTPSTART tmp = new PTPSTART();
            tmp.IncAxis = new int[9];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	�o�s�o�ړ��J�n�R�}���h�p�����[�^�\���́i�`�a�r�n�j
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct PTPASTART
    {
        public int AxisFlag;    // �ړ����I���t���O
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] PosAxis;   // �e���ʒu�����߼޼��
        public static PTPASTART Init()
        {
            PTPASTART tmp = new PTPASTART();
            tmp.PosAxis = new int[9];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	��Ԉړ��J�n�R�}���h�p�����[�^�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct LINSTART
    {
        public int AxisFlag;    // �ړ����I���t���O
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] IncAxis;   // �e���ݸ���Ĉړ���
        public int Feed;        // ��ԑ��葬�x
        public static LINSTART Init()
        {
            LINSTART tmp = new LINSTART();
            tmp.IncAxis = new int[9];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	�����Z���T�[���b�`��Ԉړ��J�n�R�}���h�p�����[�^�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct SLINSTART
    {
        public int AxisFlag;    // �ړ����I���t���O
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] IncAxis;   // �e���ݸ���Ĉړ���
        public int Feed;        // ��ԑ��葬�x
        public short NoSkipF;   // �X�L�b�v�}���t���O
        public short Reserve;   // �ް����߯����邽�߂���а
        public static SLINSTART Init()
        {
            SLINSTART tmp = new SLINSTART();
            tmp.IncAxis = new int[9];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	��Ԉړ��J�n�R�}���h�p�����[�^�\���́i�`�a�r�n�j
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct LINASTART
    {
        public int AxisFlag;    // �ړ����I���t���O
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] PosAxis;   // �e���ʒu�����߼޼��
        public int Feed;        // ��ԑ��葬�x
        public static LINASTART Init()
        {
            LINASTART tmp = new LINASTART();
            tmp.PosAxis = new int[9];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	�����Z���T�[���b�`��Ԉړ��J�n�R�}���h�p�����[�^�\���́i�`�a�r�n�j
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct SLINASTART
    {
        public int AxisFlag;    // �ړ����I���t���O
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] PosAxis;   // �e���ʒu�����߼޼��
        public int Feed;        // ��ԑ��葬�x
        public short NoSkipF;   // �X�L�b�v�}���t���O
        public int Reserve;     // �ް����߯����邽�߂���а
        public static SLINASTART Init()
        {
            SLINASTART tmp = new SLINASTART();
            tmp.PosAxis = new int[9];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	�ėp�o�͒��ڐ���R�}���h�p�����[�^�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct OUTPUTPAT
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public ushort[] OutputPat;  // �ėp�o��1�`3
        public static OUTPUTPAT Init()
        {
            OUTPUTPAT tmp = new OUTPUTPAT();
            tmp.OutputPat = new ushort[12];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	�v���O�����I���R�}���h�p�����[�^�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct PROGSEL
    {
        public short ProgSel;   // �I���v���O�����ԍ�
        public static PROGSEL Init()
        {
            return (new PROGSEL());
        }
    }

    // ------------------------------------------------------------------------
    //	�����x�I�[�o�[���C�h�ύX�R�}���h�p�����[�^�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct OVERCHG
    {
        public short Override;  // �I�[�o�[���C�h�ݒ�l
        public static OVERCHG Init()
        {
            return (new OVERCHG());
        }
    }

    // ------------------------------------------------------------------------
    //	�厲�n�m�^�n�e�e�R�}���h�p�����[�^�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct SPCMND
    {
        public short SpOut;     // �厲�w�߃t���O
        public static SPCMND Init()
        {
            return (new SPCMND());
        }
    }

    // ------------------------------------------------------------------------
    //	�厲��]���ύX�R�}���h�p�����[�^�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct SPREVSET
    {
        public int SpRevo;      // �厲��]��
        public static SPREVSET Init()
        {
            return (new SPREVSET());
        }
    }

    // ------------------------------------------------------------------------
    //	�厲��]�����\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct SPREVDAT
    {
        public int ComSpRevo;   // �厲�w�߉�]��
        public int ActSpRevo;   // �厲����]��
        public static SPREVDAT Init()
        {
            return (new SPREVDAT());
        }
    }

    // ------------------------------------------------------------------------
    //	��]����]����R�}���h�p�����[�^�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct SPINAX
    {
        public short OverFlag;  // �I�[�o�[���C�h�t���O
        public short AxisFlag;  // �ړ����I���t���O
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] RevAx;     // �e����]��
        public static SPINAX Init()
        {
            SPINAX tmp = new SPINAX();
            tmp.RevAx = new int[9];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	�ėp���́^�o�͋�������R�}���h�p�����[�^�T�u�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct IO_CMD
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] IoBit;    // �ėp���o��D0~15�ޯĕύX�����
        public static IO_CMD Init()
        {
            IO_CMD tmp = new IO_CMD();
            tmp.IoBit = new byte[16];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	�ėp���͋�������R�}���h�p�����[�^�\����
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
    //	�ėp�o�͋�������R�}���h�p�����[�^�\����
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
    //	�ėp���o�͋�������R�}���h�p�����[�^�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct COMPIOBIT
    {
        public short Pno;   // ���o�̓|�[�g�ԍ�
        public ushort Bno;  // ����r�b�g�ԍ�
        public short flg;   // ����t���O
        public static COMPIOBIT Init()
        {
            return (new COMPIOBIT());
        }
    }

    // for COMPIOBIT.Pno
    public const short INPORT = 0x0;            // ���̓|�[�g�w��
    public const short OUTPORT = -1 * 0x8000;   // �o�̓|�[�g�w��

    // for COMPIOBIT.Flg
    public const short IONOTCARE = 0;           // ��ԕύX����
    public const short IORELEASE = 1;           // �����n�m�^�n�e�e�I��
    public const short IOSET = 2;           // �����n�m
    public const short IORESET = 3;         // �����n�e�e

    // ------------------------------------------------------------------------
    //		�y���ڐ�����ON/OFF�R�}���h�p�����[�^�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct TLINESW
    {
        public short tlinesw;   // �y���ڐ�����@�\����
        public static TLINESW Init()
        {
            return (new TLINESW());
        }
    }

    // ------------------------------------------------------------------------
    //		�e�B�[�`���O�X�e�b�v�ύX�R�}���h�p�����[�^�\����
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
    //		�`�c�T���v�����O�f�[�^���M���O�R�}���h�p�����[�^�\����
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
    //		�s�o�b�f�[�^�I���R�}���h�p�����[�^�\����
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
    //		�s�o�b�f�[�^�I���R�}���h�p�����[�^�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct TPCSEL_HEADER // �w�b�_���
    {
        public short LogSel;    // ���M���O��ʑI��
        public short TrigSel;   // �g���K�I��
        public short Interval;  // ���M���O����[msec]
        public short Reserved;  // �\��
        public static TPCSEL_HEADER Init()
        {
            return (new TPCSEL_HEADER());
        }
    }

    // for TPCSEL_HEADER.LogSel (���M���O���)
    public const short TPCH_LOG_POS = 0;        // �|�W�V�����E���o��
    public const short TPCH_LOG_ECT = 1;        // EtherCAT PDO
    public const short TPCH_LOG_64CH = 2;       // 64CH


    //----------
    // �|�W�V�����E���o�̓��M���O�v���i���s�o�b�f�[�^�j
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct TPCSEL_POS    // �|�W�V�����E���o�̓��M���O�v���i���s�o�b�f�[�^�j
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
    // EtherCAT�ʐM���M���O�v��
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct TPCSEL_ECT        // �w�b�_���
    {
        public TPCSEL_HEADER h;         // �w�b�_���
        public short ax_sel;        // ���M���O���I��(�_����)
        public short dt_sel;        // ���M���O�f�[�^�T�C�Y�I��
        public short sr_sel;        // ���M���O�ʐM�����I��
        public static TPCSEL_ECT Init()
        {
            TPCSEL_ECT tmp = new TPCSEL_ECT();
            tmp.h = Rt64ecdata.TPCSEL_HEADER.Init();
            return tmp;
        }
    }

    // for TPCSEL_ECT.byte_sel�i�ʐM�f�[�^�T�C�Y�I���j �� EtherCAT�ʐM�ݒ�ɍ��킹��K�v�͂���܂���B
    public const short TPCS_ECT_DT_16BYTE = 0;      // 16byte�f�[�^���M���O
    public const short TPCS_ECT_DT_32BYTE = 1;      // 32byte�f�[�^���M���O
    public const short TPCS_ECT_DT_48BYTE = 2;      // 48byte�f�[�^���M���O
    public const short TPCS_ECT_DT_64BYTE = 3;      // 64byte�f�[�^���M���O

    // for TPCSEL_ECT.sr_sel  �i����M�I���j
    public const short TPCS_ECT_RS_ALL = 0;     // ����M
    public const short TPCS_ECT_RS_SEND = 1;        // ���M�̂�
    public const short TPCS_ECT_RS_RECEIVE = 2;     // ��M�̂�

    //----------
    // 64CH���M���O�v��
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct TPCSEL_ATTR
    {
        public int item;                    // ��������
        public int val;                 // �����l
        public static TPCSEL_ATTR Init()
        {
            return (new TPCSEL_ATTR());
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct TPCSEL_CH
    {
        public short log_item;              // ���M���O����
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Reserved;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public TPCSEL_ATTR[] attribute;             // ���M���O���ڑ���
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
        public TPCSEL_HEADER h;                     // �w�b�_���
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public TPCSEL_CH[] ch;                      // ���M���O���I��(�_����)
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

    // for TPCSEL_CH.log_item  �i���M���O���ڑI���j
    //								-1			�w�薳��
    public const short TPCS_64_I_COMREG = 0;        // �w�߈ʒu
    public const short TPCS_64_I_POSREG = 1;        // �@�B�ʒu
    public const short TPCS_64_I_ERRREG = 2;        // �΍���
    public const short TPCS_64_I_BLOCKSEG = 3;      // �ŐV�u���b�N�����o����
    public const short TPCS_64_I_ABSREG = 4;        // ��Έʒu
    public const short TPCS_64_I_TRQ = 5;       // �g���N
    public const short TPCS_64_I_MCSTATUS = 6;      // �S�̃X�e�[�^�X
    public const short TPCS_64_I_MCALARM = 7;       // �S�̃A���[��
    public const short TPCS_64_I_AXSTATUS = 8;      // ���X�e�[�^�X
    public const short TPCS_64_I_AXALARM = 9;       // ���A���[��
    public const short TPCS_64_I_TASKSTATUS = 10;       // �^�X�N�X�e�[�^�X
    public const short TPCS_64_I_TASKALARM = 11;        // �^�X�N�A���[��
    public const short TPCS_64_I_MACRO = 12;        // �}�N���ϐ�
    public const short TPCS_64_I_DI = 13;       // ���͐M��
    public const short TPCS_64_I_DO = 14;       // �o�͐M��
    public const short TPCS_64_I_PDO_OBJ = 15;      // PDO�f�[�^(�I�u�W�F�N�g�w��)
    public const short TPCS_64_I_RXPDO_DAT = 16;        // RxPDO�f�[�^(�I�t�Z�b�g�w��)
    public const short TPCS_64_I_TXPDO_DAT = 17;        // TxPDO�f�[�^(�I�t�Z�b�g�w��)
    public const short TPCS_64_I_TIME = 18;     // ���v
    public const short TPCS_64_I_VCOMREG = 19;      // ���z�w�߈ʒu

    // for TPCSEL_ATTR.item  �i�������ڑI���j
    //								-1			�w�薳��
    public const int TPCS_64_A_TASK = 0;        // �^�X�N�ԍ�
    public const int TPCS_64_A_AXIS = 1;        // ���ԍ�
    public const int TPCS_64_A_MACRO = 2;       // �}�N���ϐ��ԍ�
    public const int TPCS_64_A_IO = 3;      // IO�M���ԍ�
    public const int TPCS_64_A_PDO_IDX = 4;     // PDO�I�u�W�F�N�gIndex
    public const int TPCS_64_A_PDO_SUBIDX = 5;      // PDO�I�u�W�F�N�gSubindex
    public const int TPCS_64_A_PDO_DAT_OFS = 6;     // PDO�f�[�^�I�t�Z�b�g
    public const int TPCS_64_A_PDO_DAT_SZ = 7;      // PDO�f�[�^�T�C�Y(1-4[byte])

    // ------------------------------------------------------------------------
    //		���W�n�ݒ�R�}���h�p�����[�^�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct COORDSET
    {
        public int AxisFlag; // ���I���t���O
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] PosAxis; // �e���_�����W�l
        public static COORDSET Init()
        {
            COORDSET tmp = new COORDSET();
            tmp.PosAxis = new int[9];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //		���C���^���b�N�ݒ�R�}���h�p�����[�^�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct AXINTLK
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public short[] IntlkSw; // ���C���^���b�N�w��X�C�b�`
        public static AXINTLK Init()
        {
            AXINTLK tmp = new AXINTLK();
            tmp.IntlkSw = new short[9];
            return tmp;
        }
    }


    // ------------------------------------------------------------------------
    //		���l�O���N�g�ݒ�R�}���h�p�����[�^�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct AXNGLCT
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public short[] NeglectSw; // ���l�O���N�g�w��X�C�b�`
        public static AXNGLCT Init()
        {
            AXNGLCT tmp = new AXNGLCT();
            tmp.NeglectSw = new short[9];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //		�e���T�[�{�n�m�^�n�e�e�R�}���h�p�����[�^�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct SVONOFFCHG
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public short[] SvOnSw; // ����ON/OFF�w��X�C�b�`
        public static SVONOFFCHG Init()
        {
            SVONOFFCHG tmp = new SVONOFFCHG();
            tmp.SvOnSw = new short[9];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //		�g���N�������[�h�ύX�R�}���h�p�����[�^�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct TRQLIMCHG
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public short[] TrqLimSw; // �g���N�������[�h�w��X�C�b�`
        public static TRQLIMCHG Init()
        {
            TRQLIMCHG tmp = new TRQLIMCHG();
            tmp.TrqLimSw = new short[9];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //		�e�����䃂�[�h�ύX�R�}���h�p�����[�^�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct AXCTRLCHG
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public short[] CtrlSw; // ���䃂�[�h�w��X�C�b�`
        public static AXCTRLCHG Init()
        {
            AXCTRLCHG tmp = new AXCTRLCHG();
            tmp.CtrlSw = new short[9];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //		�l�R�[�h�o�̓R�}���h�p�����[�^�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct OUTMCD
    {
        public short mcd; // �o�͂l�R�[�h
        public static OUTMCD Init()
        {
            return (new OUTMCD());
        }
    }

    // ------------------------------------------------------------------------
    //		�蓮�p���T�[���싖�R�}���h�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct HANDLEPERMIT
    {
        public short permit;        // ����/�s���t���O(0:�s���A1:����)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Reserved; // �\��
        public static HANDLEPERMIT Init()
        {
            HANDLEPERMIT tmp = new HANDLEPERMIT();
            tmp.Reserved = new byte[6];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //		��p���[�h�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct HANDLEMODE
    {
        public short mode; // ��p���[�h
        public static HANDLEMODE Init()
        {
            return (new HANDLEMODE());
        }
    }

    // ------------------------------------------------------------------------
    //		��p�{���p�����[�^�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct HANDLEKP
    {
        public int kp; // ��p�{��
        public static HANDLEKP Init()
        {
            return (new HANDLEKP());
        }
    }

    // ------------------------------------------------------------------------
    //		��p���p�����[�^�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct HANDLEAXIS
    {
        public int Axis; // ��p�L����
        public static HANDLEAXIS Init()
        {
            return (new HANDLEAXIS());
        }
    }

    // ------------------------------------------------------------------------
    //		�T�C�N���^�]���[�h�ύX�R�}���h�p�����[�^�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct CYCLECHG
    {
        public short cycle; // �T�C�N���^�]�L���t���O
        public static CYCLECHG Init()
        {
            return (new CYCLECHG());
        }
    }

    // ------------------------------------------------------------------------
    //	�t�B�[�h�o�b�N�J�E���^�N���A�R�}���h�p�����[�^�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct FBSETUP
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public int[] cntr;      // FB1�FB2�ݒ�l
        public short cntrset;   // FB1�FB2�ݒ�t���O
        public short Reserve;   // �ް����߯����邽�߂���а
        public static FBSETUP Init()
        {
            FBSETUP tmp = new FBSETUP();
            tmp.cntr = new int[2];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	�}�N���ϐ������R�}���h�p�����[�^�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct MCRVALSET
    {
        public int RegNo;       // �}�N���ϐ��ԍ�
        public int Reserve;     // �ް����߯����邽�߂���а
        public double Val;      // �}�N���ϐ��l
        public static MCRVALSET Init()
        {
            return (new MCRVALSET());
        }
    }

    // ------------------------------------------------------------------------
    //	�Ɨ��ʒu���߃R�}���h�p�����[�^�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct AXMV
    {
        public int AxFlg;           // ���t���O
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] PosAxis;       // �ړ���/�ڕW�ʒu
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] Feed;          // �ړ����x
        public static AXMV Init()
        {
            AXMV tmp = new AXMV();
            tmp.PosAxis = new int[9];
            tmp.Feed = new int[9];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	�g���N�w�߃R�}���h�p�����[�^�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct TRQCMD
    {
        public int AxisFlag;        // ���t���O
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] Torque;        // �w�߃g���N [%]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] VClamp;        // ���x���� [rpm] (-1:�ʒu���䃂�[�h)
        public static TRQCMD Init()
        {
            TRQCMD tmp = new TRQCMD();
            tmp.Torque = new int[9];
            tmp.VClamp = new int[9];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	�v���O�����u���b�N �ړ��R�}���h�p�����[�^�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct BLKMVDATA
    {
        public short SrcPno;        // �ړ��Ώ�P�ԍ�(1-32767)
        public short DstBlk;        // �ړ���擪BLOCK�ԍ�(0�`63)
        public static BLKMVDATA Init()
        {
            return (new BLKMVDATA());
        }
    }

    // ------------------------------------------------------------------------
    //	�v���O�����u���b�N �R�s�[�R�}���h�p�����[�^�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct BLKCPYDATA
    {
        public short SrcPno;        // �R�s�[��P�ԍ�(1-32767)
        public short DstBlk;        // �R�s�[��擪BLOCK�ԍ�(0�`63)
        public short DstPno;        // �R�s�[��P�ԍ�(1-32767)
        public short DstTask;       // �R�s�[��^�X�N�ԍ�(0-7)
        public static BLKCPYDATA Init()
        {
            return (new BLKCPYDATA());
        }
    }

    // ------------------------------------------------------------------------
    //	�v���O�����u���b�N �폜�R�}���h�p�����[�^�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct BLKDLDATA
    {
        public short Pno;           // �폜�Ώۂ�P�ԍ�(1-32767)
        public static BLKDLDATA Init()
        {
            return (new BLKDLDATA());
        }
    }

    // ------------------------------------------------------------------------
    //	�^�X�N�ԍ��w��萔
    // ------------------------------------------------------------------------
    public const short TASKMAX = 8; // �^�X�N�ő吔(0�`7)

    /////////////////////////////////////////////////////////////////////////
    // �d�|�b�m�b�V��p���f�[�^�\����
    // ------------------------------------------------------------------------
    //	�������p�����[�^�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct INITIALPRM    // �������p�����[�^�\���́i�Q�O�S�W�޲ČŒ蒷�j
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] ElctdExchPos;          // �d�Ɍ����ʒu
        public int ElctdExchSpdW;           // �v���d�Ɍ����ʒu�ړ����x
        public int ElctdExchSpdW1;          // �v���d�Ɍ����O�ʒu�ړ����x
        public int ElctdExchOfsW1;          // �v���d�Ɍ����O�ʒu�I�t�Z�b�g
        public int ElctdExchOfsW2;          // �v���d�Ɍ����ҋ@�ʒu�I�t�Z�b�g
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public int[] ElctdChkSpdZ1;         // �d�ɑ�����̃Z���T�[�܂ł̂y�����~���x
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public int[] ElctdChkSpdZ2;         // �d�ɑ�����̃Z���T�[����̂y�����~���x
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public int[] ElctdChkSpdS2;         // �d�ɑ�����̃Z���T�[����̎厲��]���x
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public int[] ElctdChkOfsZ;          // �d�ɑ�����̃Z���T�[����̂y�����~��
        public int ElctdDeplUpZ;            // �d�ɏ���(Z-OT��)�^�r����~���̂y���㏸��
        public short ElctdNum;              // �d�ɐ�
        public short GuideNum;              // �K�C�h��
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] GuideExchPosS;         // �K�C�h�����ʒu�n�_
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] GuideExchPosE;         // �K�C�h�����ʒu�I�_
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] GuideChkPos;           // �K�C�h�L���Z���T�[�ʒu
        public int GuideExchSpdW;           // �v���K�C�h�����ʒu�ړ����x
        public int GuideExchOfsW1;          // �v���K�C�h�����O�ʒu�I�t�Z�b�g
        public int GuideExchOfsW2;          // �v���K�C�h�����ҋ@�ʒu�I�t�Z�b�g
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] EdgeSrchOfs;           // �[�ʈʒu�������́{�������́|���ړ���
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] EdgeSrchTouchSpd1;     // �[�ʈʒu�������̂P�x�ڂ̐ڐG���x
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] EdgeSrchTouchRet1;     // �[�ʈʒu�������̂P�x�ڂ̖߂��
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] EdgeSrchTouchSpd2;     // �[�ʈʒu�������̂Q�C�R�x�ڂ̐ڐG���x
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] EdgeSrchTouchRet2;     // �[�ʈʒu�������̂Q�C�R�x�ڂ̖߂��
        public int EdgeSrchZDwnSpd;     // �[�ʈʒu�������̂y�����~���x
        public int TouchSenseTime;          // �[�ʈʒu�������ڐG���m����
        public int RefTouchUpZ;         // ����_�y�ڐG��̂y�t�o�ʐݒ�
        public int PrcsRetSpdZ;         // ���d���H�I�����y���㏸���x
        public int EdgeSrchZUpSpd;          // �[�ʈʒu�������̂y���㏸���x
        public int ElctdClumpSpdS;          // �d�ɑ���(�گĸ����)�܂ł̎厲��]���x
        public int BucklingUpOfsZ;          // �א��d�Ɋm�F���̍����ݻ�ON�ɂ��Z�㏸��
        public int BucklingUpSpdZ;          // �א��d�Ɋm�F���̍����ݻ�ON�ɂ��Z�㏸���x
        public short BucklingRetry;         // �א��d�Ɋm�F���̍����ݻ�ON�ɂ����ײ��
        public short AecEnable;             // �`�d�b�L��/����(D0:ESF�AD1:GSF)
        public int Z20ErrOfs;               // �y�Q�O�G���[���o�I�t�Z�b�g
        public short McnType;               // �@�B�^�C�v�i0:�ʏ�@�A1:���d�l�@�j
        public short GuideSensorDis;            // �K�C�h�ђʌ��o�Z���T�[�����ݒ�
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public int[] CysCheckDis;           // CYS�`�F�b�N�����ݒ�
                                            // 	CysCheckDis[0] : D00:  1 �` D31: 32
                                            // 	CysCheckDis[1] : D00: 33 �` D31: 64
                                            // 	CysCheckDis[2] : D00: 65 �` D31: 96
                                            // 	CysCheckDis[3] : D00: 97 �` D31:128
        public short AxAecActDis;           // �d�ɁE�K�C�h�������얳�����ݒ�(A/B/C���̂�)
        public short AxBrakeEn;             // �u���[�L���ݒ�(A/B/C���̂�)
        public short BrakeTimer;                // �u���[�L�쓮����[msec]
        public short TchErrCancelTime;      // �ڐG���m�G���[�L�����Z������
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public int[] CylPulseOut;           // CYL�p���X�o�͐ݒ�
                                            // 	CylPulseOut[0] : D00:  1 �` D31: 32
                                            // 	CylPulseOut[1] : D00: 33 �` D31: 64
                                            // 	CylPulseOut[2] : D00: 65 �` D31: 96
                                            // 	CylPulseOut[3] : D00: 97 �` D31:128
        public short CylPulseTime;          // CYL�p���X��[msec]
        public short PrcsFirstP99xDis;      // ���d���H��FirstSpark�܂ł̉��H����P99x(1�`6)�����ݒ�
        public short LeaveZrnFin;           // �A���[�������_���A�ς��׸ނ��c�����׸�
        public short EdgeSrchMaxMinUse;     // �[�ʈʒu�v�Z���ő�/�ŏ��l�g�p�t���O
        public short EdgeSrchOldMcnComp;        // ��Ű�o��/�c�o������̋��@��݊������׸�
        public short CylSelBfrEDeplAec;     // �d�ɏ��Վ���AEC�O CYL�o�͑I��
                                            // (0�`128:0�͖����AD08��OFF�w��)
        public short CylSelAftEDeplAec;     // �d�ɏ��Վ���AEC�� CYL�o�͑I��
                                            // (0�`128:0�͖����AD08��OFF�w��)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] Reserved556;          // �\��
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public int[] CysOffChkDis;          // CYS�I�t�`�F�b�N�����ݒ�
                                            // 	CysOffChkDis[0] : D00:  1 �` D31: 32
                                            // 	CysOffChkDis[1] : D00: 33 �` D31: 64
                                            // 	CysOffChkDis[2] : D00: 65 �` D31: 96
                                            // 	CysOffChkDis[3] : D00: 97 �` D31:128
        public int SvPrcsStpRetOfs;     // �T�[�{���d���H�r����~���߂��
        public int SvPrcsRetOfs;            // �T�[�{���d���H�I�����߂��
        public int SvPrcsRetSpd;            // �T�[�{���d���H�I�����߂葬�x
        public short AxServoPrcs;           // �T�[�{���d���H�Ώێ��ݒ�(D0:X �` )
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] Reserved588;          // �\��
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public int[] CysLatch;              // CYS�M�����b�`�I��
                                            // 	CysLatch[0] : D00:  1 �` D31: 32
                                            // 	CysLatch[1] : D00: 33 �` D31: 64
                                            // 	CysLatch[2] : D00: 65 �` D31: 96
                                            // 	CysLatch[3] : D00: 97 �` D31:128
        public short InstLastElctdFlg;      // �ŏI�d�ɑ������[�h�L���t���O
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] Reserved608;          // �\��
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] SpinMaxSpd;                // ������]����]���x����l [pps]
        public short PrcsSkipStopCys;       // ���d���H�X�L�b�v����~���[�hCYS�M���I��
        public short DischgTchErrEn;            // ���d���̐ڐG���m�G���[���o�L���t���O
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public int[] CFeedTable;                // �厲���x(0�`15)->�b�����x(0.1rpm)�e�[�u��
        public short GuideThroughChkDis;        // �K�C�h�ђʌ��o�����p�[�e�B�V�����t���O
        public short Z2ndSoftLimMSelCys;        // �y����Q�\�t�g���~�b�g�w��CYS�M���I��
        public int Z2ndSoftLimM;            // �y����Q�\�t�g���~�b�g
        public short GuidChgCylEn;          // �K�C�h�������b�x�k�o�͗L���t���O
                                            //   0:����, 1�`128:CYL�o�͐M���擪�ԍ�
        public short GuidChgCysSel;         // �K�C�h�������b�x�k�o�͌�b�x�r�M���ґI��
                                            //   0:����, 1�`128:CYS�M���ԍ�
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] GuidHolderEscOfs;      // �K�C�h�z���_�[�ޔ��
        public short GuidHolderArmDis;      // �K�C�h�z���_�[�A�[���V�����_�����t���O
        public short TouchSenseISetEn;      // �ڐG���m�ɂ��Ƽ�پ�ėL���׸�(�d�������������l)
        public short CylSelPatRndStp;       // �p�[�e�B�V�����P����~�� CYL�o�͑I��
        public short PatRndStpOnlyElctdDep; // �d�ɏ��Ղɂ��d�Ɍ������̂��߰è��݂P����~�L���׸�
        public short CylSelThrghDetect;     // ���d���H���ђʌ��o���CYL�o�͑I��
                                            // (0�`128:0�͖����AD08��OFF�w��)
        public short RefSpdSelThrghDetect;  // ���d���H���ђʌ��o����x�I��(0:�����׎����x�A1:SFR-DN)
        public short PerInitThrghDetect;        // ���d���H���ђʌ��o����I�������l   (M91 I�w��)
        public short PerInitThDetectEdge;   // ���d���H�������ی��o����I�������l (M81 I�w��)
        public int AvTimInitThDetectEdge;   // ���d���H�������ی��o���x�v���ݒ菉���l (M81 J�w��)
        public short RefSpdIgnHPerInit;     // ���d���H���ђʌ��o����x�v���㑤�����������l (M25 H�w��)
        public short RefSpdIgnLPerInit;     // ���d���H���ђʌ��o����x�v�����������������l (M25 I�w��)
        public short RefSpdAvTimInit;       // ���d���H���ђʌ��o����x�v�����ω����ԏ����l (M25 J�w��)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] Reserved788;          // �\��
        public int PreElctdChgLmtPos;       // �d�ɏ��ՑO�d�Ɍ������~�b�g���W
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public short[] ElctdExchMovOrder;       // �d�Ɍ����ʒu�ړ����i1�`9�F0�͍Ō�j
        public short CylSelInElctdPrcs;     // �P���d�ɕ��d���H��CYL�o�͑I�� (0�`128 : 0�͏o�͖���)
        public short PatliteMode;           // �p�g���C�g�o�̓��[�h�I�� (0�`2)
        public short AxVirActDis;           // ���z�_/����_�ړ��������t���O(A/B/C���̂�)(�d�������������l)
        public short SvPrcsInitialTime;     // �T�[�{���d���H����������(���d����҂�����) [msec]
        public short ThinElctdISetPumpOff;  // �א��d�ɂł̃C�j�V�����Z�b�g��
                                            // �|���v�����p�[�e�B�V�����t���O(D0:1 �` D5:6)
        public short AecWRefSel;                // �`�d�b�J�n/�I�����v���Ҕ��ʒu�I��
                                                // (0=�v�����_, 1=�v������l)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] Reserved824;          // �\��
        public short GuideThroughChkPNo;        // �޲�ފђ����������H�����ԍ��ݒ� (�d�������������l)
                                                // (0			= �]���ʂ�P998/999���g�p
                                                //  16384�`17383 = P0�`P999���g�p
                                                //				 (D14��L���t���O�Ƃ��Ďg�p))
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public short[] InitialSetPno;           // �Ƽ�پ�Ď����H�����ԍ��ݒ� (�d�������������l)
                                                // (0			= �]���ʂ�P998/999���g�p
                                                //  16384�`17383 = P0�`P999���g�p
                                                //				 (D14��L���t���O�Ƃ��Ďg�p))
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] Reserved840;              // �\��
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public int[] ElctdChkOfsZ2;         // �d�ɑ�����̾ݻ�����̂y���Q�i�ډ��~�ʐݒ�(�d�������������l)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public short[] GuideThroughChkPNoPat;   // �K�C�h�ђʃ`�F�b�N�����H�����ԍ��ݒ� (�d�������������l)
                                                // (0			= �]���ʂ�P998/999���g�p
                                                //  16384�`17383 = P0�`P999���g�p
                                                //				 (D14��L���t���O�Ƃ��Ďg�p))
        public short EUninstColReclampDis;  // �d�ɒE�����̃R���b�g�ăN�����v(��������)���얳���t���O
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] Reserved880;          // �\��
        public int AvTimInitThrghDetect;    // ���d���H���ђʌ��o���x�v���ݒ�
                                            //(���ω�����/���x�v������)�����l(M91 J�w��)
        public int EdgeLSrchTouchRet;       // ������������Ԓ[�ʈʒu�������߂��[pls](�S����������)
        public int EdgeLSrchRetSpd;     // ������������Ԓ[�ʈʒu�������߂葬�x[pps](�S���������x)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Reserved896;          // �\��
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public double[] InitPrmMacVal;          // �������p�����[�^�w��Œ�}�N���ϐ��l(#1500�`#1509)
        public short NrmElctdPrcsBuckling;  // �ʏ�d�ɂł̕��d���H�������ݻ����o�t���O
                                            // (0:�����A1:�G���[�A2:��~�A3:�d�Ɍ���)
        public short NrmElctdGuideBuckling; // �ʏ�d�ɂł̃K�C�h�ђʎ������ݻ����o�t���O
                                            // (0:�����A1:�G���[�A2:���d�Ɏ擾)
        public short RotAxMovMode;          // ������]���A�u�\�ʒu���߈ړ����@�����l (0�`5)
                                            // (0:�]���ʂ�A1:�߉��A
                                            //  2:��������(360���ȏ�Ȃ�)�A
                                            //  3:��������(360���ȏ゠��)�A
                                            //  4:�펞�{�����A5:�펞�|����)
        public short AutoModeOutSel;            // �������[�h���o�͐M���I��
                                                // (-1�`128�F0=����, -1=o#1727 D06, 1�`128=CYL�M��)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public int[] CylBitRstPlsCont;      // ���Z�b�g/�G���[������CYL�p���X�o�͌p���ݒ�
                                            // 	CylBitRstPlsCont[0] : D00:  1 �` D31: 32
                                            // 	CylBitRstPlsCont[1] : D00: 33 �` D31: 64
                                            // 	CylBitRstPlsCont[2] : D00: 65 �` D31: 96
                                            // 	CylBitRstPlsCont[3] : D00: 97 �` D31:128
        public short PrcsTmoutChkEndSel;        // ���d���H�^�C���A�E�g���o�I���^�C�~���O�I��
                                                // (0:���H�[�����B�ŏI���A
                                                //  1:���dOFF(�X�p�[�N�A�E�g�^�C�}���Ԍo��
                                                //   �����d���H���ђʌ��o���CYL�o�͊���)�ŏI��)
        public short MountedSF02FX;         // SF02FX���ڃt���O[0:����,1:�L��]
        public int EIFErr1MaskSet;          // EtherCAT IF�{�[�h �G���[1�}�X�N�ݒ�
        public int EIFErr2MaskSet;          // EtherCAT IF�{�[�h �G���[2�}�X�N�ݒ�
        public short Fanstop10DlyTim;       // �{�@��FAN��~ ���H�I����҂�����[sec]
        public short Fanstop20DlyTim;       // SF02FX��FAN��~ ���H�I����҂�����[sec]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] EIFErr1Action;            // EtherCAT IF�{�[�h �G���[1(6000h,01h)����������ݒ�
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] EIFErr2Action;            // EtherCAT IF�{�[�h �G���[2(6000h,02h)����������ݒ�
        public short MountedBP20;           // BP20���ڃt���O[0:����,1:�L��]
        public short MountedIPEnhance;      // �g��IP���ڃt���O[0:����,1:�L��]
        public short MountedACPower;            // �𗬓d�����ڃt���O[0:����,1:�L��]
        public short MountedNanoPulse;      // �i�m�p���X�d�����ڃt���O[0:����,1:�L��]
        public short MountedHVOverlay;      // �����d���d�����ڃt���O[0:����,1:�L��]
        public short MountedTouchProbe;     // �^�b�`�v���[�u���ڃt���O[0:����,1:�L��]
        public int VSLvlSetting;            // VS���x���ݒ�i�f�t�H���g�F2.0V�j
        public int VSPKLvlSetting;          // VSPK���x���ݒ�i�f�t�H���g�F2.9V�j
        public int CntactSenseLvlSetting;   // �ڐG���m���x���ݒ�i�f�t�H���g0.8V�j
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public short[] HPJogOvr;                // �蓮�p���TJOG�I�[�o�[���C�h(0�`200%)
        public short PLCShutDwonAuthEn;     // PLC�V���b�g�_�E�������L��[0:����,1:�L��]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)]
        public byte[] Reserved1144;         // �\��
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public short[] EX1E321Setting;          // EX1-E321�C���^�[�t�F�[�X�ݒ�(7500h)
                                                // EX1E321Setting[ 0] : �\�t�g�X�^�[�g�ᑬ���x�ݒ�[50�`750ms(50ms�Ԋu)]	(7500h,01h)	
                                                // EX1E321Setting[ 1] : �\�t�g�X�^�[�g�������x�ݒ�[ 2�` 30ms( 2ms�Ԋu)]	(7500h,02h)	
                                                // EX1E321Setting[ 2] : �\�t�g�X�^�[�g�J�n�d���ݒ�						(7500h,03h)	
                                                //                       0x00:IP=0A, 0x01:IP=3A����J�n����B						
                                                // EX1E321Setting[ 3] : BONDLY_E323_O�ݒ�[1�`255ms(�f�t�H���g8ms)]		(7500h,04h)	
                                                // EX1E321Setting[ 4] : BONDLY_0�ݒ�     [1�`255ms(�f�t�H���g8ms)]		(7500h,05h)	
                                                // EX1E321Setting[ 5] : BON�M���I��ݒ�									(7500h,06h)	
                                                //                       0x00:BON10=BONDLY_E323_O / BON00=BONDLY0					
                                                //                       0x01:BON10=BON0          / BON00=BON10						
                                                // EX1E321Setting[ 6] : OFF�L�΂��{���ݒ�[1�`32�{(�f�t�H���g10�{)]		(7500h,07h)	
                                                // EX1E321Setting[ 7] : Z�N���A�G���[���o�g���K�M���I��ݒ�				(7500h,08h)	
                                                //                       0x00:VSPK , 0x01:VS										
                                                // EX1E321Setting[ 8] : Z�N���A�G���[�J�E���g���ݒ�						(7500h,09h)	
                                                //                       0x00�`0xFF(�f�t�H���g0x00)									
                                                // EX1E321Setting[ 9] : EX5-E504�ؑ֐ݒ�(0:���g�p,1:�g�p)				(7500h,0Ah)	
                                                // EX1E321Setting[10] : FLS�L��/�����ݒ�								(7500h,0Bh)	
                                                //                       0x00:FLS1�M������(�f�t�H���g), 0x01:FLS1�M���ŕ��dOFF		
                                                // EX1E321Setting[11] : ���Z�b�g���d�ݒ�								(7500h,0Ch)	
                                                //                       bit0-3  : �p���X��      (0x0�`0xF�F0�`15us)				
                                                //                       bit4    : �L��/�����ݒ� (0:����, 1:�L��)					
                                                //                       bit5-15 : Reserve											
                                                // EX1E321Setting[12]-[19] : Reserve												
        public short CRS10DAMin;                // �C���o�[�^D/A�o��(7300h,01h) �ŏ��l		
        public short CRS10DAMax;                // �C���o�[�^D/A�o��(7300h,01h) �ő�l		
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public short[] CRS10DATable;                // �C���o�[�^D/A�o��(7300h,01h) �e�[�u��	
        public short SCDAMin;               // ���޺��۰�(SC�l)D/A�o��(7300h,02h) �ŏ��l
        public short SCDAMax;               // ���޺��۰�(SC�l)D/A�o��(7300h,02h) �ő�l
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public short[] SCDATable;               // ���޺��۰�(SC�l)D/A�o��(7300h,02h) �e�[�u��
        public short CRSDAMin;              // SP�����۰�(CRS�l)D/A�o��(7300h,03h) �ŏ��l
        public short CRSDAMax;              // SP�����۰�(CRS�l)D/A�o��(7300h,03h) �ő�l
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public short[] CRSDATable;              // SP�����۰�(CRS�l)D/A�o��(7300h,03h) �e�[�u��
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 660)]
        public byte[] Reserved2048;         // �\��
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
    //	���H�������\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct PCONDITION
    {
        public short Status;                    // �e���ԏ��
        public short PNo;                   // ���H�����ԍ�
        public int ZUpFeed;             // �y���㏸���x
        public int ZDwFeed;             // �y�����~���x
        public short CFeed;                 // �厲���葬�x
        public short SpinOut;               // �厲��](0:��~,1:CW,2:CCW)
        public short PumpOut;               // �|���v�o��(0:OFF,1:ON)
        public short PrCFeed;               // ���d���H���厲���x(���H����)
        public int CFeedRpm;                // �b���厲���x(0.1rpm)
        public int DryRunEnN;               // �h���C�����L���t���O(���d���H����)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Reserved;             // �\��
        public static PCONDITION Init()
        {
            PCONDITION tmp = new PCONDITION();
            tmp.Reserved = new byte[4];
            return tmp;
        }
    }

    // for PCONDITION.Status
    public const short PCS_RETURN = 0x0001; // �ԑ��R�}���h�v��
                                            //public const short				= 0x0002;
    public const short PCS_DRYRUN = 0x0004; // �h���C�����L��
    public const short PCS_INITIALSET = 0x0008; // InitialSet�L��
    public const short PCS_THINLINE = 0x0010;   // �א��ݒ�L��
                                                //public const short				= 0x0020;
    public const short PCS_CFEED = 0x0040;  // �厲��]���x�ύX�v��
    public const short PCS_AEC = 0x0080;    // �d�Ɍ����L��
    public const short PCS_PATRNDSTPEN = 0x0100;    // �߰è��ݓ��P����~�L��
    public const short PCS_PATRNDSTPIN = 0x0200;    // �߰è��ݓ��P����~��
    public const short PCS_GUIDETHROUGH = 0x0400;   // �K�C�h�ђʓ��싖�v��
    public const short PCS_IN_DISCHARGE = 0x0800;   // �P�����H��
    public const short PCS_MAN_AEC = 0x1000;    // �蓮�d�Ɍ����v��
    public const short PCS_BON = 0x2000;    // ���d�n�m��
    public const short PCS_ISET_FIN = 0x4000;   // �C�j�V�����Z�b�g�ς�

    // ------------------------------------------------------------------------
    //	�`�d�b��ԍ\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct AECDATA
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public short[] PartitionS;              // ��1�`6�߰è��݊J�n�ԍ�
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public short[] PartitionE;              // ��1�`6�߰è��ݏI���ԍ�
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public short[] Thinline;                // ��1�`6�߰è��ݍא��ݒ�(1:�א�,0:�ʏ�)
        public short PartitionDis;          // �߰è��ݖ����׸�(1:����,0:�L��)
        public short ElectrodeNo;           // �d�ɔԍ�	 (0:���ݒ�)
        public short GuideNo;               // �K�C�h�ԍ�(0:���ݒ�)
        public short IndexZrnFin;           // �C���f�b�N�X�ԍ��L���t���O
        public short IndexNo;               // �C���f�b�N�X�ԍ�
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 18)]
        public byte[] Reserved;             // �\��
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
    //	�|�C���g�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct AXPOINT
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] Pnt;                   // ��P�`�X�����W�l
        public static AXPOINT Init()
        {
            AXPOINT tmp = new AXPOINT();
            tmp.Pnt = new int[9];
            return tmp;
        }
    };

    // ------------------------------------------------------------------------
    //	�e�����z�_/����_���W�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct VIRPOS_EX
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1001)]
        public AXPOINT[] AxPnt;             // �e�����z�_/����_���W
                                            // �i0�`999�F���z�_�A1000:����_�j
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Reserved;         // �\��
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
    //	���[�N���_���W�ݒ�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct WORKORG
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] WorkOrg;               // ��P�`�X�����[�N���_���W
        public static WORKORG Init()
        {
            WORKORG tmp = new WORKORG();
            tmp.WorkOrg = new int[9];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	�q�n�l�r�v�p�����[�^�\���́i�o�b����p�j
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct ROMSW //	ROMSW�p�����[�^�\���́i�Q�T�U�O�O�޲ČŒ蒷�j
    {
        public long st_en;              // �L���ǃt���O(�����ǃt���O)
        public long axis_en;            // �L�����t���O(�����ǃt���O)
        public long io_en;              // �L��IO�t���O(�����ǃt���O)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20496)]
        public byte[] Reserved1;            // �\��
        public long spindle_ax;         // �厲�L�����t���O
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5072)]
        public byte[] Reserved2;            // �\��
        public static ROMSW Init()
        {
            ROMSW tmp = new ROMSW();
            tmp.Reserved1 = new byte[20496];
            tmp.Reserved2 = new byte[5072];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	�e��␳�f�[�^���j�^�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct CORRECTDATA
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] MachinePos;            // �p�x�␳/ϼ�ۯ��␳������W
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] MachineLock;       // �}�V�����b�N�␳�l
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] Backlash;          // �o�b�N���b�V���␳�l
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] Pitcherr;          // �s�b�`�G���[�␳�l
        public int AngSin;              // �␳�p�x�v���l�isin��,2-30bit�Œ菬���_���j
        public int AngCos;              // �␳�p�x�v���l�icos��,2-30bit�Œ菬���_���j
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public int[] Angle;             // �p�x�␳�l�iX/Y���j
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public int[] AngMlk;                // �p�x�␳��ϼ�ۯ��␳�l(X/Y��)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public int[] AngStart;          // �p�x�␳�J�n���W�iX/Y���j
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public int[] AngMeas1;          // �␳�p�x�v���l�i�P�_��X/Y���j
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public int[] AngMeas2;          // �␳�p�x�v���l�i�Q�_��X/Y���j
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public int[] Mirror;                // �~���[�␳�l�iX/Y���j
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public int[] MirCentCoord;      // �~���[����W�iX/Y���j
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 48)]
        public byte[] Reserved;         // �\��
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
    //	���d���H�X�L�b�v�����z�_�o�^�ԍ����j�^�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct PRCSSKIPVPOSNO
    {
        public short LastNo;                // �O��o�^�ԍ�					*/
        public short NextNo;                // ����o�^�ԍ�					*/
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Reserved;         // �\��							*/
        public static PRCSSKIPVPOSNO Init()
        {
            PRCSSKIPVPOSNO tmp = new PRCSSKIPVPOSNO();
            tmp.Reserved = new byte[4];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	���b�Z�[�W�\���v���f�[�^�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct MESSAGEREQ
    {
        public short LineFlg;           // ���b�Z�[�W�\�����ߎ��s�v���O�������
        public short MessageNo;         // ���b�Z�[�W�ԍ�
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Reserved;         // �\��
        public static MESSAGEREQ Init()
        {
            MESSAGEREQ tmp = new MESSAGEREQ();
            tmp.Reserved = new byte[4];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	���b�Z�[�W�\�����ʃf�[�^�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct MESSAGEANS
    {
        public MESSAGEREQ Req;          // ���b�Z�[�W�\���v�����f�[�^	*/
        public short ButtonSel;     // �I��(�N���b�N)�{�^���ԍ�		*/
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Reserved;     // �\��							*/
        public double InputNum;     // ���l���͒l					*/
        public static MESSAGEANS Init()
        {
            MESSAGEANS tmp = new MESSAGEANS();
            tmp.Req = Rt64ecdata.MESSAGEREQ.Init();
            tmp.Reserved = new byte[6];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	�J�����R�}���h�v���f�[�^�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct CAMERACMDREQ
    {
        public short CommandNo;     // �R�}���h�ԍ�
        public short Timeout;       // �^�C���A�E�g����
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Reserved;     // �\��
        public static CAMERACMDREQ Init()
        {
            CAMERACMDREQ tmp = new CAMERACMDREQ();
            tmp.Reserved = new byte[4];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	�J�����R�}���h���ʕ��������_���f�[�^�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct CAMERACMDANSF
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public double[] RDat;           // ���ʃf�[�^
        public static CAMERACMDANSF Init()
        {
            CAMERACMDANSF tmp = new CAMERACMDANSF();
            tmp.RDat = new double[10];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	���H�����e�[�u���\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct PCOND_REC
    {
        public short Ton;           // Ton[us]
        public short Toff;          // Toff[us]
        public int IPVal;           // IP(����8bit:EX1-E602�p�޲��)
        public int SFIPVal;     // SFIP(����9bit:EX1-E602�p�޲��)
        public int CAPVal;          // CAP(����10bit:EX1-E602�p�޲��)
        public short SCSel;         // �T�[�{�R���g���[��DA(0-63)
        public short CRSSel;            // SP���R���g���[��DA(0-15)
        public int SfrFr;           // ���H�T�[�{���葬�x[pls/min]
        public int SfrBk;           // ���H�T�[�{�߂葬�x[pls/min]
        public short PumpPrSel;     // �|���v���͑I��(0-3)
        public short ServoSel;      // �T�[�{�I��(0-3)
        public short PSSel;         // �d���I��(0-5)
        public short POLVal;            // �����ؑւ�
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 28)]
        public byte[] Reserved;     // �\��
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
        public PCOND_REC[] rec;                     // ���M���O���I��(�_����)
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
    //	ECNC��p�O���A���[���v���\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct ECNCEXTALM
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
        public byte[] Factor;       // �O���A���[���v��(1024Bit)
        public static ECNCEXTALM Init()
        {
            ECNCEXTALM tmp = new ECNCEXTALM();
            tmp.Factor = new byte[128];
            return tmp;
        }
    }

    /////////////////////////////////////////////////////////////////////////
    // �d�|�b�m�b�V��p���R�}���h�\����

    // ------------------------------------------------------------------------
    //	�L���^�����ݒ�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct ENABLE
    {
        public short enable;            // �L��/�����ݒ�t���O(0:�����A1:�L��)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Reserved;     // �\��
        public static ENABLE Init()
        {
            ENABLE tmp = new ENABLE();
            tmp.Reserved = new byte[6];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	�v������l�ݒ�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct WTOPPOS
    {
        public int WTopPos;     // �v������l
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Reserved;     // �\��
        public static WTOPPOS Init()
        {
            WTOPPOS tmp = new WTOPPOS();
            tmp.Reserved = new byte[4];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	���H�����ԍ��ݒ�R�}���h�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct PRNOSEL
    {
        public int PNo;         // ���H�����ԍ�
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Reserved;     // �\��
        public static PRNOSEL Init()
        {
            PRNOSEL tmp = new PRNOSEL();
            tmp.Reserved = new byte[4];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	���z�_�^����_�ύX�R�}���h�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct VIRPOSCHG
    {
        public int VirNo;               // ���z�_/����_�ԍ�
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] VirPos;                // ���z�_���W
        public static VIRPOSCHG Init()
        {
            VIRPOSCHG tmp = new VIRPOSCHG();
            tmp.VirPos = new int[9];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	�v���O�������s�J�n�R�}���h�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct PROGSTRT
    {
        public short NNo;               // ���s�J�n�m�ԍ�
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Reserved;         // �\��
        public static PROGSTRT Init()
        {
            PROGSTRT tmp = new PROGSTRT();
            tmp.Reserved = new byte[6];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	�p�[�e�B�V�����ݒ�R�}���h�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct PARTITION
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public short[] PartitionS;          // ��P�`�U�߰è��݊J�n�ԍ�
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public short[] PartitionE;          // ��P�`�U�߰è��ݏI���ԍ�
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public short[] Thinline;            // ��1�`6�߰è��ݍא��ݒ�(1:�א�,0:�ʏ�)
        public short PartitionDis;      // �߰è��ݖ����׸�(1:����,0:�L��)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Reserved;         // �\��
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
    //	�d�ɔԍ��ݒ�R�}���h�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct ELCTDNO
    {
        public short ElectrodeNo;       // �d�ɔԍ�
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Reserved;         // �\��
        public static ELCTDNO Init()
        {
            ELCTDNO tmp = new ELCTDNO();
            tmp.Reserved = new byte[6];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	�d�ɑ���/�E���R�}���h�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct ELCTDINSTALL
    {
        public short install;           // ����t���O(0:�E���A1:����)
        public short ElctdNo;           // �d�ɔԍ�
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Reserved;         // �\��
        public static ELCTDINSTALL Init()
        {
            ELCTDINSTALL tmp = new ELCTDINSTALL();
            tmp.Reserved = new byte[4];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	�d�Ɍ����ʒu�ړ��R�}���h�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct ELCTDEXCHPOS
    {
        public short axis;              // ���I���t���O
        public short pos;               // ����t���O
                                        // (0:�����ʒu�A1:�O�ʒu�A2:�ҋ@�ʒu)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Reserved;         // �\��
        public static ELCTDEXCHPOS Init()
        {
            ELCTDEXCHPOS tmp = new ELCTDEXCHPOS();
            tmp.Reserved = new byte[4];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	�d�r�e�A�[���ړ��R�}���h�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct MOVEESFARM
    {
        public short pos;               // ����t���O(0:�O�[�A1:���ԁA2:��[)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Reserved;         // �\��
        public static MOVEESFARM Init()
        {
            MOVEESFARM tmp = new MOVEESFARM();
            tmp.Reserved = new byte[6];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	�d�r�e�t�B���K�[OPEN/CLOSE�R�}���h�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct OPENESFARM
    {
        public short open;              // ����t���O(0:Close�A1:Open)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Reserved;         // �\��
        public static OPENESFARM Init()
        {
            OPENESFARM tmp = new OPENESFARM();
            tmp.Reserved = new byte[6];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	�K�C�h�ԍ��ݒ�R�}���h�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct GUIDENO
    {
        public short GuideNo;           // �K�C�h�ԍ�
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Reserved;         // �\��
        public static GUIDENO Init()
        {
            GUIDENO tmp = new GUIDENO();
            tmp.Reserved = new byte[6];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	�K�C�h����/�E���R�}���h�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct GUIDEINSTALL
    {
        public short install;           // ����t���O(0:�E���A1:����)
        public short GuideNo;           // �K�C�h�ԍ�
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Reserved;         // �\��
        public static GUIDEINSTALL Init()
        {
            GUIDEINSTALL tmp = new GUIDEINSTALL();
            tmp.Reserved = new byte[4];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	�K�C�h�����ʒu�ړ��R�}���h�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct GUIDEEXCHPOS
    {
        public short axis;              // ���I���t���O
        public short pos;               // ����t���O(0:�����ʒu�A2:�ҋ@�ʒu)
        public short GuideNo;           // �K�C�h�ԍ�
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] Reserved;         // �\��
        public static GUIDEEXCHPOS Init()
        {
            GUIDEEXCHPOS tmp = new GUIDEEXCHPOS();
            tmp.Reserved = new byte[2];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	�K�C�h�m�F�ʒu�ړ��R�}���h�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct GUIDECHKPOS
    {
        public short axis;              // ���I���t���O
        public short pos;               // ����t���O
                                        // (0:�m�F�ʒu�A1:�O�ʒu�A2:�ҋ@�ʒu)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Reserved;         // �\��
        public static GUIDECHKPOS Init()
        {
            GUIDECHKPOS tmp = new GUIDECHKPOS();
            tmp.Reserved = new byte[4];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	�f�r�e�A�[���ړ��R�}���h�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct MOVEGSFARM
    {
        public short pos;               // ����t���O(0:�O�[�A1:��[)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Reserved;         // �\��
        public static MOVEGSFARM Init()
        {
            MOVEGSFARM tmp = new MOVEGSFARM();
            tmp.Reserved = new byte[6];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	�K�C�h�N�����v/�A���N�����v�R�}���h�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct GUIDECLUMP
    {
        public int clump;               // �����׸�(0:Unclump�A1:Clump)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Reserved;         // �\��
        public static GUIDECLUMP Init()
        {
            GUIDECLUMP tmp = new GUIDECLUMP();
            tmp.Reserved = new byte[4];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	�X�s���h���N�����v/�A���N�����v�R�}���h�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct SPCLUMP
    {
        public int clump;               // �����׸�(0:Unclump�A1:Clump)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Reserved;         // �\��
        public static SPCLUMP Init()
        {
            SPCLUMP tmp = new SPCLUMP();
            tmp.Reserved = new byte[4];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	�d�r�e�}�K�W���ړ��R�}���h�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct ESFMAGMOV
    {
        public int MagazineNo;          // �ړ���}�K�W���ԍ�
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Reserved;         // �\��
        public static ESFMAGMOV Init()
        {
            ESFMAGMOV tmp = new ESFMAGMOV();
            tmp.Reserved = new byte[4];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	���[�N���_���W�ݒ�R�}���h�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct WORGPOSCHG
    {
        public int AxisFlag;            // ���I���t���O
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public int[] PosAxis;           // ��1���`��9���_�����_���W�l
        public static WORGPOSCHG Init()
        {
            WORGPOSCHG tmp = new WORGPOSCHG();
            tmp.PosAxis = new int[9];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	�K�C�h�ђʓ��싖�R�}���h�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct GUIDETHROUGH
    {
        public short move;              // �ړ������׸�(0:�s����,1:����)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Reserved;         // �\��
        public static GUIDETHROUGH Init()
        {
            GUIDETHROUGH tmp = new GUIDETHROUGH();
            tmp.Reserved = new byte[6];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	�}�V�����b�N�L���^�����ݒ�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct MCHLOCK
    {
        public short enable;                // �L��/�����ݒ�t���O(0:�����A1:�L��)
        public short move;              // ���������׸�(1:�ړ�)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Reserved;         // �\��
        public static MCHLOCK Init()
        {
            MCHLOCK tmp = new MCHLOCK();
            tmp.Reserved = new byte[4];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	�␳�p�x�ݒ�R�}���h�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct ANGLESET
    {
        public int CorrAng;         // �␳�p�x(8-24�ޯČŒ菬���_)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Reserved;         // �\��
        public static ANGLESET Init()
        {
            ANGLESET tmp = new ANGLESET();
            tmp.Reserved = new byte[4];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	�������_���A�����ݒ�R�}���h�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct FORCEZRNFIN
    {
        public short AxisFlag;          // ���I���t���O
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Reserved;         // �\��
        public static FORCEZRNFIN Init()
        {
            FORCEZRNFIN tmp = new FORCEZRNFIN();
            tmp.Reserved = new byte[6];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	�h���C�����ݒ�(���d���H�����w��)�R�}���h�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct DRYRUN_EX
    {
        public int DryRunEnN;           // �h���C�����L���t���O(���d���H����)
                                        // [0:�h���C��������,-1:�����w�薳��]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Reserved;         // �\��
        public static DRYRUN_EX Init()
        {
            DRYRUN_EX tmp = new DRYRUN_EX();
            tmp.Reserved = new byte[4];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	�|���v����R�}���h�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct PUMPCMND
    {
        public short PumpOut;           // �|���v����t���O�i0:OFF�A1:ON�j
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Reserved;         // �\��
        public static PUMPCMND Init()
        {
            PUMPCMND tmp = new PUMPCMND();
            tmp.Reserved = new byte[6];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	���d����R�}���h�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct BONCMND
    {
        public short BonOut;                // ���d����t���O�i0:OFF�A1:ON�j
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Reserved;         // �\��
        public static BONCMND Init()
        {
            BONCMND tmp = new BONCMND();
            tmp.Reserved = new byte[6];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	�������[�h���o�̓R�}���h�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct AUTOMODE_OUTPUT
    {
        public short onflg;             // �������[�h���o�̓t���O�i0:OFF�A1:ON�j
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Reserved;         // �\��
        public static AUTOMODE_OUTPUT Init()
        {
            AUTOMODE_OUTPUT tmp = new AUTOMODE_OUTPUT();
            tmp.Reserved = new byte[6];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	�V���b�g�_�E���J�n�R�}���h�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct SHUTDOWN_START
    {
        public short startflg;          // �����޳݊J�n�׸�(0:��~,1:�J�n)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Reserved;         // �\��
        public static SHUTDOWN_START Init()
        {
            SHUTDOWN_START tmp = new SHUTDOWN_START();
            tmp.Reserved = new byte[6];
            return tmp;
        }
    }

    // ------------------------------------------------------------------------
    //	�u�U�[ON/OFF�M���o�̓R�}���h�\����
    // ------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct BUZZER_ON_OUTPUT
    {
        public short onflg;             // �u�U�[ON/OFF�t���O�i0:OFF�A1:ON�j
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Reserved;         // �\��
        public static BUZZER_ON_OUTPUT Init()
        {
            BUZZER_ON_OUTPUT tmp = new BUZZER_ON_OUTPUT();
            tmp.Reserved = new byte[6];
            return tmp;
        }
    }
}
