using System;
using System.Runtime.InteropServices;

class Syncdef
{
    // ========================================================================
    //	�f�[�^��ʃR�[�h
    // ========================================================================
    public const short DAT_PARAMETER = 0x0; // �T�[�{�p�����[�^	
    public const short DAT_PROGRAM = 0x1;   // �^�]�v���O����
    public const short DAT_STATUS = 0x2;    // �T�[�{�X�e�[�^�X
    public const short DAT_IODATA = 0x3;    // I/O�M�����
    public const short DAT_OPTIONPRM = 0x9; // �I�v�V�����p�����[�^
    public const short DAT_DNCDATA = 0xE;   // �c�m�b�f�[�^
    public const short DAT_DNCBUFI = 0xF;   // �c�m�b�o�b�t�@���
    public const short DAT_PITCHERR = 0x10; // �s�b�`�G���[�␳�p�p�����[�^
    public const short DAT_SENSEPOS = 0x11; // �Z���T�[���b�`�ʒu���
    public const short DAT_TOOLHOFS = 0x12; // �H��␳�f�[�^
    public const short DAT_FORCEIO = 0x13;  // �������o�̓r�b�g�f�[�^
    public const short DAT_TEACHSTS = 0x18; // �e�B�[�`���O���
    public const short DAT_VARIABLE = 0x19; // �}�N���ϐ��f�[�^
    public const short DAT_TOOLDIA = 0x1a;  // �H��a�␳�f�[�^
    public const short DAT_PRGBLKINFO = 0x1b;   // �v���O�����u���b�N���
    public const short DAT_RTC = 0x21;  // �V�X�e���\��
    public const short DAT_ADDATA = 0x22;   // �`�^�c���o�n�r���
    public const short DAT_ADLOG = 0x23;    // �`�^�c���o�n�r���M���O���
    public const short DAT_TPCINFO = 0x24;  // �s�o�b���M���O���
    public const short DAT_TPCDATA = 0x25;  // �s�o�b���M���O�f�[�^
                                            //public const short					= 0x26;	// 
    public const short DAT_VERSION = 0x27;  // �q�n�l�o�[�W�����f�[�^
    public const short DAT_AXNEGLECT = 0x28;    // �������ݒ���
    public const short DAT_AXINTLOCK = 0x29;    // ���C���^���b�N�ݒ���
    public const short DAT_TPCINFO_EX = 0x2a;   // �s�o�b���M���O���
    public const short DAT_ONEBLOCK = 0x30; // �v���O�����P�u���b�N�f�[�^
    public const short DAT_AXSVONEN = 0x31; // ���T�[�{�n�m�L���ݒ���
    public const short DAT_HANDLESTS = 0x32;    // ��p���[�h�ݒ���
    public const short DAT_MCRREG = 0x33;   // �}�N���ϐ��ǂݏo���^��������
    public const short DAT_ECT_INFO = 0x34; // EtherCAT���ǂݏo��
    public const short DAT_ECT_MON = 0x35;  // EtherCAT����M�f�[�^�ǂݏo��
                                            // public const short					= 0x36;
    public const short DAT_ACOPARAM = 0x37; // ��ԑO���������Ұ��Ǐo/����
    public const short DAT_TOOLHSTS = 0x39; // �H��␳�ݒ���
    public const short DAT_TOOLDSTS = 0x3a; // �H��a�␳�ݒ���
    public const short DAT_POINTTABLE = 0x3b;   // �ʒu���߃|�C���g�e�[�u���ǂݏo���^��������
    public const short DAT_ASSAXISTBL = 0x3c;   // �����蓖�ď��ǂݏo��
    public const short DAT_TOOLDERR = 0x3d; // �H��a�␳�G���[���
    public const short DAT_VERPER = 0x40;   // �u�d�q�^�o�d�q�l
    public const short DAT_SPREV = 0x41;    // �厲��]���Ǐo��
    public const short DAT_FBCOUNT = 0x50;  // �t�B�[�h�o�b�N�J�E���^�ώZ�l
    public const short DAT_ROMSWITCH = 0x60;    // �r�w�V�X�e���q�n�l�X�C�b�`�f�[�^
    public const short DAT_OPTSWITCH = 0x60;    // �I�v�V�����X�C�b�`�f�[�^
    public const short DAT_FORGROUND = 0x90;    // �V�X�e���\��
    public const short DAT_CYCLETIME = 0x91;    /* ����������						*/

    public const short DAT_INITIALPRM = 0xa0;   /* �������p�����[�^����/�Ǐo		*/
    public const short DAT_PCONDITION = 0xa1;   /* ���H�������Ǐo					*/
    public const short DAT_AECDATA = 0xa2;  /* �`�d�b��ԓǏo					*/
    public const short DAT_WORKORG = 0xa4;  /* ���[�N���_�ݒ�Ǐo				*/
    public const short DAT_CORRECTDATA = 0xa6;  /* �e��␳�f�[�^���j�^���Ǐo		*/
    public const short DAT_VIRPOS_EX = 0xa7;    /* ���z�_�^����_�ݒ�Ǐo			*/

    public const short DAT_PRCSSKIPVPOSNO = 0xa9;   /* ���d���H�X�L�b�v�����z�_�o�^�ԍ��Ǐo	*/
    public const short DAT_MESSAGEREQ = 0xad;   /* ���b�Z�[�W�\���v���f�[�^�Ǐo		*/
    public const short DAT_MESSAGEANS = 0xae;   /* ���b�Z�[�W�\�����ʃf�[�^����		*/
    public const short DAT_ONEVARIABLE = 0xb1;  /* �}�N���P�ϐ�����/�Ǐo			*/
    public const short DAT_CAMERACMDREQ = 0xb2; /* �J�����R�}���h�v���f�[�^�Ǐo		*/

    public const short DAT_CAMERACMDANSF = 0xb5;    /* �J�����R�}���h���ʕ��������_���f�[�^����	*/
    public const short DAT_NVARIABLE_PRE = 0xb6;    /* �}�N���ϐ��C�Ӑ�����M���Ұ��\�񏑍�/�Ǐo*/
    public const short DAT_NVARIABLE_VAL = 0xb7;    /* �}�N���ϐ��f�[�^�C�Ӑ�����/�Ǐo			*/
    public const short DAT_VARIABLE_REQ = 0xb9; /* �}�N���ϐ�����/�Ǐo�v���f�[�^����/�Ǐo	*/
    public const short DAT_PCOND_TABLE = 0xba;  /* ���H�����e�[�u���f�[�^����/�Ǐo	*/
    public const short DAT_ECNCEXTALM = 0xbb;   /* ECNC��p�O���A���[���v���Ǐo		*/
    public const short DAT_PITCHERR_AX = 0xbc;  /* �e���s�b�`�G���[�␳�p�p�����[�^	*/

    // public const short  DAT_SRESERVED	= 0xC0;	// �V�X�e���\��̈�J�n�ԍ�
    // public const short  DAT_ERESERVED	= 0xff;	// �V�X�e���\��̈�I���ԍ�
    public const short DAT_FLASHROMINIT = 0xfd; // �V�X�e���\��i�ׯ�������ް��j
    public const short DAT_FLASHROM = 0xfe; // �V�X�e���\��i�ׯ�������ް��j
    public const short DAT_RESERVED = 0xFF; // �V�X�e���\��


    // --------------------------------------------------------------------------
    //	�R�}���h�R�[�h
    // --------------------------------------------------------------------------
    public const short REQ_LOOPBACK_TEST = 0x00;        // �c�o�ʐM ���[�v�o�b�N�e�X�g
    public const short REQ_DATA_SEND = 0x01;        // �c�o�ʐM �f�[�^���M�v��
    public const short REQ_DATA_RECEIVE = 0x02;     // �c�o�ʐM �f�[�^��M�v��
    public const short REQ_MODECHG = 0x10;      // Ӱ�ޕύX
    public const short REQ_JOGSTART = 0x11;     // �i�n�f�ړ��J�n
    public const short REQ_ZRNSTART = 0x12;     // ���_���A�J�n
    public const short REQ_STOP = 0x13;     // �ړ���~
    public const short REQ_GENE = 0x14;     // �ު�ڰ���
    public const short REQ_PTPSTART = 0x15;     // �o�s�o�ړ��J�n
    public const short REQ_PTPASTART = 0x16;        // �o�s�o�ړ��J�n�iABSO�j
    public const short REQ_LINSTART = 0x17;     // ��Ԉړ��J�n
    public const short REQ_LINASTART = 0x18;        // ��Ԉړ��J�n�iABSO�j
    public const short REQ_ORGSET = 0x19;       // ���_�ݒ�
    public const short REQ_RESET = 0x1A;        // ؾ��
    public const short REQ_RESTART = 0x1B;      // ���s�ĊJ
    public const short REQ_PRGCLEAR = 0x1C;     // ������۸��Ѹر
    public const short REQ_OUTPUT = 0x1D;       // �ėp�o�͒��ڐ���
    public const short REQ_SERVOON = 0x1E;      // ���ޓd��ON
    public const short REQ_SERVOOFF = 0x1F;     // ���ޓd��OFF
    public const short REQ_PROGSTRT = 0x20;     // ��۸��ю��s�J�n
    public const short REQ_PROGSTOP = 0x21;     // ��۸��ю��s��~
    public const short REQ_PROGSLCT = 0x22;     // ���s��۸��ёI��
    public const short REQ_ALLZRN = 0x24;       // �S�����_���A�J�n
    public const short REQ_ERCLEAR = 0x25;      // �d�q�N���A for SVM
    public const short REQ_SMPLSTRT = 0x26;     // �T���v�����O�J�n for SVM
    public const short REQ_SMPLSTOP = 0x27;     // �T���v�����O��~ for SVM
    public const short REQ_SLINSTART = 0x28;        // �����ݻ�ׯ���Ԉړ��J�n
    public const short REQ_SLINASTART = 0x29;       // �����ݻ�ׯ���Ԉړ��J�n(ABSO)
    public const short REQ_COMPINPUT = 0x2A;        // �ėp���͋�������i�ꊇ�j
    public const short REQ_COMPOUTPUT = 0x2B;       // �ėp�o�͋�������i�ꊇ�j
    public const short REQ_COMPIOBIT = 0x2C;        // �ėp���o�͋�������i�r�b�g�j
    public const short REQ_OVRDCHGP = 0x2D;     // �����x���ްײ�ށ��l�ύX
    public const short REQ_ADLOG = 0x2E;        // �`�^�c���M���O�v��
    public const short REQ_ADLOGCLR = 0x2F;     // �`�^�c���M���O�o�b�t�@�N���A�v��
    public const short REQ_ZRNSHIFT = 0x30;     // ���_�V�t�g�v��
    public const short REQ_TPCSEL = 0x31;       // �s�o�b�f�[�^�I��
    public const short REQ_TPCLOG = 0x32;       // �s�o�b�f�[�^���M���O�n�m�^�n�e�e
    public const short REQ_AXAUTOZRN = 0x33;        // �e���������_���A�R�}���h
    public const short REQ_PTPBSTART = 0x34;        // �o�s�o�ړ��J�n(�@�B���W�nABSO)
    public const short REQ_LINBSTART = 0x35;        // ��Ԉړ��J�n(�@�B���W�nABSO)
    public const short REQ_CYCLE = 0x36;        // �T�C�N���^�]���[�h�ύX�R�}���h
    public const short REQ_COORDSET = 0x37;     // ���W�n�ݒ�
    public const short REQ_AXISINTLK = 0x38;        // ���C���^���b�N�R�}���h
    public const short REQ_AXISNEG = 0x39;      // ���l�O���N�g�R�}���h
    public const short REQ_SVONOFF = 0x3A;      // �e���T�[�{�n�m�^�n�e�e
    public const short REQ_TRQLIMCHG = 0x3B;        // �g���N�������[�h�ύX
    public const short REQ_AXCTRLCHG = 0x3C;        // �e�����䃂�[�h�ύX
    public const short REQ_MCDOUT = 0x3D;       // �l�R�[�h�o��
    public const short REQ_TPCSEL_EX = 0x3E;        // �s�o�b�f�[�^�I��
    public const short REQ_PANEL = 0x40;        // �V�X�e���\��
    public const short REQ_ROMSWGEN = 0x41;     // �V�X�e���\��
    public const short REQ_TIMERRESET = 0x42;       // �V�X�e���\��
    public const short REQ_OUTPUTBIT = 0x44;        // �ėp�o�͒��ڐ���i�r�b�g�j
    public const short REQ_TRQCMD = 0x46;       // �g���N�w��
    public const short REQ_SYNEGPTPSTART = 0x49;        // �����������o�s�o�ړ��J�n
    public const short REQ_SYNEGPTPASTART = 0x4a;       // �����������o�s�o�ړ��J�n�iABSO�j
    public const short REQ_SYNEGPTPBSTART = 0x4b;       // �����������o�s�o�ړ��J�n�i�@�B���W�nABSO�j
    public const short REQ_SPCMND = 0x50;       // �厲�c�^�`�o�͎w��
    public const short REQ_SPREVSET = 0x51;     // �厲��]���ݒ�
    public const short REQ_SPINAX = 0x52;       // ��]����]����w��
    public const short REQ_TLINE = 0x54;        // �ڐ�����n�m�^�n�e�e
    public const short REQ_HANDLE = 0x5c;       // ��p���[�hON/OFF�ݒ�
    public const short REQ_HANDLEKP = 0x5d;     // ��p���[�h�{���ݒ�
    public const short REQ_HANDLEAXIS1 = 0x5e;      // ��p�L�����P
    public const short REQ_HANDLEAXIS2 = 0x5f;      // ��p�L�����Q
    public const short REQ_SINGLE = 0x60;       // �V���O���X�e�b�v���[�h
    public const short REQ_TEACH = 0x61;        // �e�B�[�`���O
    public const short REQ_PRGINS = 0x62;       // �v���O�����}��
    public const short REQ_PRGALT = 0x63;       // �v���O�����u��
    public const short REQ_PRGDEL = 0x64;       // �v���O�����폜
    public const short REQ_PRGREV = 0x65;       // �v���O�����t�s�^�]
    public const short REQ_STEPCHG = 0x66;      // �v���O�����X�e�b�v�ύX
    public const short REQ_STEPSKIP = 0x67;     // �v���O�����X�e�b�v�X�L�b�v
    public const short REQ_AXMV = 0x68;     // �Ɨ��ʒu���߃C���N���w��
    public const short REQ_AXMVA = 0x69;        // �Ɨ��ʒu���ߘ_�����W�n�A�u�\�w��
    public const short REQ_AXMVB = 0x6a;        // �Ɨ��ʒu���ߋ@�B���W�n�A�u�\�w��
    public const short REQ_PRGBLKMV = 0x6B;     // �v���O�����u���b�N�ړ�
    public const short REQ_PRGBLKCPY = 0x6C;        // �v���O�����u���b�N�R�s�[
    public const short REQ_PRGBLKDEL = 0x6D;        // �v���O�����u���b�N�폜
    public const short REQ_REFMEM = 0x6E;       // Flash��؂։^�]��۸��є��f�w��
    public const short REQ_REFPRM = 0x6F;       // Flash��؂փp�����[�^���f�w��
    public const short REQ_FBSETUP = 0x78;      // �e�a�J�E���^�����ݒ�
    public const short REQ_TASKSTART = 0x80;        // ��������۸��ъJ�n
    public const short REQ_TASKSTOP = 0x81;     // ��������۸��ђ�~
    public const short REQ_TASKRESET = 0x82;        // ��������۸���ؾ��
    public const short REQ_HOME = 0x83;     // homepos�ړ�
    public const short REQ_MCRREG = 0x84;       // �}�N���ϐ��������݃R�}���h
    public const short REQ_COVRDCHGP = 0x85;        // ��ԃI�[�o�[���C�h���l�ύX
    public const short REQ_SOVRDCHGP = 0x86;        // �厲�I�[�o�[���C�h���l�ύX
    public const short REQ_MABSCOORDSET = 0x90;     // �A�u�\���W�ݒ�R�}���h

    public const short REQ_OPTSTOP = 0xa0;      /* ��߼��ٽį�ߗL��/���������		*/
    public const short REQ_TOUCHSENSE = 0xa1;       /* �ڐG���m�L��/�����R�}���h		*/
    public const short REQ_WTOPPOS = 0xa2;      /* �v������l�ݒ�R�}���h			*/
    public const short REQ_RETURN = 0xa3;       /* �ԑ��R�}���h						*/
    public const short REQ_PNOSEL = 0xa4;       /* ���H�����ԍ��ύX�R�}���h			*/
    public const short REQ_DRYRUN = 0xa5;       /* �h���C�����L��/�����R�}���h		*/
    public const short REQ_ELCTDCHGEN = 0xa6;       /* �d�Ɍ����L��/�����R�}���h		*/
    public const short REQ_INITIALSET = 0xa7;       /* InitialSet�L��/�����R�}���h		*/
    public const short REQ_GUIDETHROUGH = 0xa8;     /* �K�C�h�ђʓ��싖�R�}���h		*/

    public const short REQ_PROGSTRTN = 0xad;        /* �v���O�������s�J�n�R�}���h		*/
    public const short REQ_MOVEPRGSTOPPOS = 0xae;       /* ��۸��ю��s��~�ʒu�ړ��J�n�����	*/
    public const short REQ_PARTITIONCHG = 0xaf;     /* �p�[�e�B�V�����ݒ�R�}���h		*/
    public const short REQ_ELCTDNOCHG = 0xb0;       /* �d�ɔԍ��ݒ�R�}���h				*/
    public const short REQ_ELCTDINSTALL = 0xb1;     /* �d�ɑ���/�E������J�n�R�}���h	*/
    public const short REQ_ELCTDEXCHPOS = 0xb2;     /* �d�Ɍ����ʒu�ړ��R�}���h			*/

    public const short REQ_ESFMAGAZINE_INC = 0xb4;      /* ESF϶޼�ݲݸ���ĺ����			*/
    public const short REQ_MOVEESFARM = 0xb5;       /* ESF��шړ��i�O�[/����/��[�j		*/
    public const short REQ_OPENESFFINGER = 0xb6;        /* ESF̨ݶްOPEN/CLOSE				*/
    public const short REQ_GUIDENOCHG = 0xb7;       /* �K�C�h�ԍ��ݒ�R�}���h			*/
    public const short REQ_GUIDEINSTALL = 0xb8;     /* �K�C�h����/�E������J�n�R�}���h	*/
    public const short REQ_GUIDEEXCHPOS = 0xb9;     /* �K�C�h�����ʒu�ړ�				*/
    public const short REQ_MOVEGSFARM = 0xbb;       /* �f�r�e�A�[���ړ��i�O�[/��[�j	*/
    public const short REQ_GUIDECLUMP = 0xbc;       /* �K�C�h�N�����v/�A���N�����v		*/
    public const short REQ_SPCLUMP = 0xbd;      /* �X�s���h���N�����v/�A���N�����v	*/

    public const short REQ_GUIDECHKPOS = 0xbf;      /* �K�C�h�m�F�ʒu�ړ��R�}���h		*/

    public const short REQ_ESFMAGAZINE_MOV = 0xc1;      /* �d�r�e�}�K�W���ړ��R�}���h		*/
    public const short REQ_INCREFSET_MOV = 0xc2;        /* ���Α���_�ݒ莞���ړ��L�������	*/
    public const short REQ_PTPASTART_W = 0xc3;      /* W������Ҕ�L���o�s�o�ړ��J�n(�_�����W�nABSO)	*/
    public const short REQ_PTPBSTART_W = 0xc4;      /* W������Ҕ�L���o�s�o�ړ��J�n(�@�B���W�nABSO)	*/
    public const short REQ_WORGPOSCHG = 0xc5;       /* ���[�N���_���W�ݒ�R�}���h		*/
    public const short REQ_PATROUNDSTOP = 0xc6;     /* �߰è��ݓ��P����~�L���R�}���h	*/
    public const short REQ_XY_ILOCK = 0xc7;     /* X/Y������ۯ��L��/�����R�}���h	*/
    public const short REQ_MCHLOCK = 0xc8;      /* �}�V�����b�N�L��/�����R�}���h	*/
    public const short REQ_CORR_ANG = 0xc9;     /* ��۸��ъJ�n���p�x�␳�L��/���������	*/
    public const short REQ_BLOCKSKIP = 0xca;        /* BlockSkip�L��/�����R�}���h		*/
    public const short REQ_ANGLESET = 0xcb;     /* �␳�p�x�ݒ�R�}���h				*/
    public const short REQ_GDTHROUGH = 0xcc;        /* �K�C�h�ђʓ���J�n�R�}���h		*/
    public const short REQ_VIRPOSCHG_EX = 0xcd;     /* ���z�_�^����_�ύX�R�}���h		*/

    public const short REQ_HANDLEPERMIT = 0xcf;     /* �蓮�p���T�[���R�}���h			*/

    public const short REQ_FORCEZRNFIN = 0xd1;      /* �������_���A�����ݒ�R�}���h		*/
    public const short REQ_DRYRUN_EX = 0xd2;        /* �h���C�����ݒ�(���d���H�����w��)�R�}���h	*/

    public const short REQ_PUMPCMND = 0xd5;     /* �|���v����R�}���h				*/
    public const short REQ_BONCMND = 0xd6;      /* ���d����R�}���h					*/
    public const short REQ_PRCSSKIP = 0xd7;     /* ���d���H�X�L�b�v�R�}���h			*/
    public const short REQ_AUTOMODE_OUTPUT = 0xd8;      /* �������[�h���o�̓R�}���h			*/
    public const short REQ_SHUTDOWN_START = 0xd9;       /* �V���b�g�_�E���J�n�R�}���h		*/
    public const short REQ_BUZZER_ON_OUTPUT = 0xda;     /* �u�U�[ON/OFF�M���o�̓R�}���h		*/
    public const short REQ_PITCHERR_CLR = 0xdb;     /* �s�b�`�G���[�␳�p�p�����[�^�N���A	*/

    // public const short  REQ_SRESERVED	= 0xE0;		// �V�X�e���\��̈�J�n�ԍ�
    // public const short  REQ_ERESERVED	= 0xff;		// �V�X�e���\��̈�I���ԍ�

    // ========================================================================
    //	�G���[�R�[�h
    // ========================================================================
    public const int E_OK = 0;      // �G���[�Ȃ�
    public const int E_DEVNRDY = 1;     // �f�o�C�X��������
    public const int E_PARAM = 2;       // �p�����[�^�G���[
    public const int E_TIME = 3;        // �ʐM�^�C���A�E�g
    public const int E_RTRY = 4;        // ���g���C�I�[�o�[
    public const int E_MLTRTRY = 5;     // ���d���g���C
    public const int E_HARDER = 6;      // �ʐM�G���[
    public const int E_NEXIST = 7;      // �v���f�[�^�����݂��Ȃ�
    public const int E_PROTECT = 8;     // �f�[�^��M�s���
    public const int E_SEQ = 9;     // �ʐM�V�[�P���X�G���[
    public const int E_PRGTERM = 10;        // �v���O������M���f
    public const int E_PRGBUFF = 11;        // �v���O�����o�b�t�@�t��
    public const int E_CMDNOT = 12;     // �R�}���h���s�s��
    public const int E_EMPTYHANDLE = 13;        // �󂫒ʐM�n���h������
    public const int E_NOHANDLE = 14;       // �����n���h��
    public const int E_BUSY = 15;       // �ʐM�r�W�[
    public const int E_PRMWRITE = 16;       // �p�����[�^�����G���[
    public const int E_PRGSTOPPOS = 17;     // ��۸��ђ�~�ʒu�łȂ�

    public const int E_USERDEF = 256;       // ���[�U�[��`�G���[�R�[�h�̈�J�n

    public const int E_ERR = -1;		// �s���G���[

}