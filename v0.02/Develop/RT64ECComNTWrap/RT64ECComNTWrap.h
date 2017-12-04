#pragma once
#pragma pack(4)

// ========================================================================
//	�f�[�^��ʃR�[�h
// ========================================================================
const short  DAT_PARAMETER = 0x0;	// �T�[�{�p�����[�^	
const short  DAT_PROGRAM = 0x1;	// �^�]�v���O����
const short  DAT_STATUS = 0x2;	// �T�[�{�X�e�[�^�X
const short  DAT_IODATA = 0x3;	// I/O�M�����
const short  DAT_OPTIONPRM = 0x9;	// �I�v�V�����p�����[�^
const short  DAT_DNCDATA = 0xE;	// �c�m�b�f�[�^
const short  DAT_DNCBUFI = 0xF;	// �c�m�b�o�b�t�@���
const short  DAT_PITCHERR = 0x10;	// �s�b�`�G���[�␳�p�p�����[�^
const short  DAT_SENSEPOS = 0x11;	// �Z���T�[���b�`�ʒu���
const short  DAT_TOOLHOFS = 0x12;	// �H��␳�f�[�^
const short  DAT_FORCEIO = 0x13;	// �������o�̓r�b�g�f�[�^
const short  DAT_TEACHSTS = 0x18;	// �e�B�[�`���O���
const short  DAT_VARIABLE = 0x19;	// �}�N���ϐ��f�[�^
const short  DAT_TOOLDIA = 0x1a;	// �H��a�␳�f�[�^
const short  DAT_PRGBLKINFO = 0x1b;	// �v���O�����u���b�N���
const short  DAT_RTC = 0x21;	// �V�X�e���\��
const short  DAT_ADDATA = 0x22;	// �`�^�c���o�n�r���
const short  DAT_ADLOG = 0x23;	// �`�^�c���o�n�r���M���O���
const short  DAT_TPCINFO = 0x24;	// �s�o�b���M���O���
const short  DAT_TPCDATA = 0x25;	// �s�o�b���M���O�f�[�^
								//public const short					= 0x26;	// 
const short  DAT_VERSION = 0x27;	// �q�n�l�o�[�W�����f�[�^
const short  DAT_AXNEGLECT = 0x28;	// �������ݒ���
const short  DAT_AXINTLOCK = 0x29;	// ���C���^���b�N�ݒ���
const short  DAT_TPCINFO_EX = 0x2a;	// �s�o�b���M���O���
const short  DAT_ONEBLOCK = 0x30;	// �v���O�����P�u���b�N�f�[�^
const short  DAT_AXSVONEN = 0x31;	// ���T�[�{�n�m�L���ݒ���
const short  DAT_HANDLESTS = 0x32;	// ��p���[�h�ݒ���
const short  DAT_MCRREG = 0x33;	// �}�N���ϐ��ǂݏo���^��������
const short  DAT_ECT_INFO = 0x34;	// EtherCAT���ǂݏo��
const short  DAT_ECT_MON = 0x35;	// EtherCAT����M�f�[�^�ǂݏo��
										// public const short					= 0x36;
const short  DAT_ACOPARAM = 0x37;	// ��ԑO���������Ұ��Ǐo/����
const short  DAT_TOOLHSTS = 0x39;	// �H��␳�ݒ���
const short  DAT_TOOLDSTS = 0x3a;	// �H��a�␳�ݒ���
const short  DAT_POINTTABLE = 0x3b;	// �ʒu���߃|�C���g�e�[�u���ǂݏo���^��������
const short  DAT_ASSAXISTBL = 0x3c;	// �����蓖�ď��ǂݏo��
const short  DAT_TOOLDERR = 0x3d;	// �H��a�␳�G���[���
const short  DAT_VERPER = 0x40;	// �u�d�q�^�o�d�q�l
const short  DAT_SPREV = 0x41;	// �厲��]���Ǐo��
const short  DAT_FBCOUNT = 0x50;	// �t�B�[�h�o�b�N�J�E���^�ώZ�l
const short  DAT_ROMSWITCH = 0x60;	// �r�w�V�X�e���q�n�l�X�C�b�`�f�[�^
const short  DAT_OPTSWITCH = 0x60;	// �I�v�V�����X�C�b�`�f�[�^
const short  DAT_FORGROUND = 0x90;	// �V�X�e���\��

const short  DAT_INITIALPRM = 0xa0;	/* �������p�����[�^����/�Ǐo		*/
const short  DAT_PCONDITION = 0xa1;	/* ���H�������Ǐo					*/
const short  DAT_AECDATA = 0xa2;	/* �`�d�b��ԓǏo					*/
const short  DAT_WORKORG = 0xa4;	/* ���[�N���_�ݒ�Ǐo				*/
const short  DAT_CORRECTDATA = 0xa6;	/* �e��␳�f�[�^���j�^���Ǐo		*/
const short  DAT_VIRPOS_EX = 0xa7;	/* ���z�_�^����_�ݒ�Ǐo			*/
const short  DAT_SERVOPCOND = 0xa8;	/* �T�[�{���H�����f�[�^����/�Ǐo	*/
const short  DAT_PRCSSKIPVPOSNO = 0xa9;	/* ���d���H�X�L�b�v�����z�_�o�^�ԍ��Ǐo	*/
const short  DAT_MESSAGEREQ = 0xad;	/* ���b�Z�[�W�\���v���f�[�^�Ǐo		*/
const short  DAT_MESSAGEANS = 0xae;	/* ���b�Z�[�W�\�����ʃf�[�^����		*/
const short  DAT_ONEVARIABLE = 0xb1;	/* �}�N���P�ϐ�����/�Ǐo			*/
const short  DAT_CAMERACMDREQ = 0xb2;	/* �J�����R�}���h�v���f�[�^�Ǐo		*/

const short  DAT_CAMERACMDANSF = 0xb5;	/* �J�����R�}���h���ʕ��������_���f�[�^����	*/
const short  DAT_NVARIABLE_PRE = 0xb6;	/* �}�N���ϐ��C�Ӑ�����M���Ұ��\�񏑍�/�Ǐo*/
const short  DAT_NVARIABLE_VAL = 0xb7;	/* �}�N���ϐ��f�[�^�C�Ӑ�����/�Ǐo			*/
const short  DAT_VARIABLE_REQ = 0xb9;	/* �}�N���ϐ�����/�Ǐo�v���f�[�^����/�Ǐo	*/
const short  DAT_PCOND_TABLE = 0xba;	/* ���H�����e�[�u���f�[�^����/�Ǐo	*/
												// public const short  DAT_SRESERVED	= 0xC0;	// �V�X�e���\��̈�J�n�ԍ�
												// public const short  DAT_ERESERVED	= 0xff;	// �V�X�e���\��̈�I���ԍ�
const short  DAT_FLASHROMINIT = 0xfd;	// �V�X�e���\��i�ׯ�������ް��j
const short  DAT_FLASHROM = 0xfe;	// �V�X�e���\��i�ׯ�������ް��j
const short  DAT_RESERVED = 0xFF;	// �V�X�e���\��


									// --------------------------------------------------------------------------
									//	�R�}���h�R�[�h
									// --------------------------------------------------------------------------
const short  REQ_LOOPBACK_TEST = 0x00;		// �c�o�ʐM ���[�v�o�b�N�e�X�g
const short  REQ_DATA_SEND = 0x01;		// �c�o�ʐM �f�[�^���M�v��
const short  REQ_DATA_RECEIVE = 0x02;		// �c�o�ʐM �f�[�^��M�v��
const short  REQ_MODECHG = 0x10;		// Ӱ�ޕύX
const short  REQ_JOGSTART = 0x11;		// �i�n�f�ړ��J�n
const short  REQ_ZRNSTART = 0x12;		// ���_���A�J�n
const short  REQ_STOP = 0x13;		// �ړ���~
const short  REQ_GENE = 0x14;		// �ު�ڰ���
const short  REQ_PTPSTART = 0x15;		// �o�s�o�ړ��J�n
const short  REQ_PTPASTART = 0x16;		// �o�s�o�ړ��J�n�iABSO�j
const short  REQ_LINSTART = 0x17;		// ��Ԉړ��J�n
const short  REQ_LINASTART = 0x18;		// ��Ԉړ��J�n�iABSO�j
const short  REQ_ORGSET = 0x19;		// ���_�ݒ�
const short  REQ_RESET = 0x1A;		// ؾ��
const short  REQ_RESTART = 0x1B;		// ���s�ĊJ
const short  REQ_PRGCLEAR = 0x1C;		// ������۸��Ѹر
const short  REQ_OUTPUT = 0x1D;		// �ėp�o�͒��ڐ���
const short  REQ_SERVOON = 0x1E;		// ���ޓd��ON
const short  REQ_SERVOOFF = 0x1F;		// ���ޓd��OFF
const short  REQ_PROGSTRT = 0x20;		// ��۸��ю��s�J�n
const short  REQ_PROGSTOP = 0x21;		// ��۸��ю��s��~
const short  REQ_PROGSLCT = 0x22;		// ���s��۸��ёI��
const short  REQ_ALLZRN = 0x24;		// �S�����_���A�J�n
const short  REQ_ERCLEAR = 0x25;		// �d�q�N���A for SVM
const short  REQ_SMPLSTRT = 0x26;		// �T���v�����O�J�n for SVM
const short  REQ_SMPLSTOP = 0x27;		// �T���v�����O��~ for SVM
const short  REQ_SLINSTART = 0x28;		// �����ݻ�ׯ���Ԉړ��J�n
const short  REQ_SLINASTART = 0x29;		// �����ݻ�ׯ���Ԉړ��J�n(ABSO)
const short  REQ_COMPINPUT = 0x2A;		// �ėp���͋�������i�ꊇ�j
const short  REQ_COMPOUTPUT = 0x2B;		// �ėp�o�͋�������i�ꊇ�j
const short  REQ_COMPIOBIT = 0x2C;		// �ėp���o�͋�������i�r�b�g�j
const short  REQ_OVRDCHGP = 0x2D;		// �����x���ްײ�ށ��l�ύX
const short  REQ_ADLOG = 0x2E;		// �`�^�c���M���O�v��
const short  REQ_ADLOGCLR = 0x2F;		// �`�^�c���M���O�o�b�t�@�N���A�v��
const short  REQ_ZRNSHIFT = 0x30;		// ���_�V�t�g�v��
const short  REQ_TPCSEL = 0x31;		// �s�o�b�f�[�^�I��
const short  REQ_TPCLOG = 0x32;		// �s�o�b�f�[�^���M���O�n�m�^�n�e�e
const short  REQ_AXAUTOZRN = 0x33;		// �e���������_���A�R�}���h
const short  REQ_PTPBSTART = 0x34;		// �o�s�o�ړ��J�n(�@�B���W�nABSO)
const short  REQ_LINBSTART = 0x35;		// ��Ԉړ��J�n(�@�B���W�nABSO)
const short  REQ_CYCLE = 0x36;		// �T�C�N���^�]���[�h�ύX�R�}���h
const short  REQ_COORDSET = 0x37;		// ���W�n�ݒ�
const short  REQ_AXISINTLK = 0x38;		// ���C���^���b�N�R�}���h
const short  REQ_AXISNEG = 0x39;		// ���l�O���N�g�R�}���h
const short  REQ_SVONOFF = 0x3A;		// �e���T�[�{�n�m�^�n�e�e
const short  REQ_TRQLIMCHG = 0x3B;		// �g���N�������[�h�ύX
const short  REQ_AXCTRLCHG = 0x3C;		// �e�����䃂�[�h�ύX
const short  REQ_MCDOUT = 0x3D;		// �l�R�[�h�o��
const short  REQ_TPCSEL_EX = 0x3E;		// �s�o�b�f�[�^�I��
const short  REQ_PANEL = 0x40;		// �V�X�e���\��
const short  REQ_ROMSWGEN = 0x41;		// �V�X�e���\��
const short  REQ_TIMERRESET = 0x42;		// �V�X�e���\��
const short  REQ_OUTPUTBIT = 0x44;		// �ėp�o�͒��ڐ���i�r�b�g�j
const short  REQ_TRQCMD = 0x46;		// �g���N�w��
const short  REQ_SYNEGPTPSTART = 0x49;		// �����������o�s�o�ړ��J�n
const short  REQ_SYNEGPTPASTART = 0x4a;		// �����������o�s�o�ړ��J�n�iABSO�j
const short  REQ_SYNEGPTPBSTART = 0x4b;		// �����������o�s�o�ړ��J�n�i�@�B���W�nABSO�j
const short  REQ_SPCMND = 0x50;		// �厲�c�^�`�o�͎w��
const short  REQ_SPREVSET = 0x51;		// �厲��]���ݒ�
const short  REQ_SPINAX = 0x52;		// ��]����]����w��
const short  REQ_TLINE = 0x54;		// �y���ڐ�����n�m�^�n�e�e
const short  REQ_HANDLE = 0x5c;		// ��p���[�hON/OFF�ݒ�
const short  REQ_HANDLEKP = 0x5d;		// ��p���[�h�{���ݒ�
const short  REQ_HANDLEAXIS1 = 0x5e;		// ��p�L�����P
const short  REQ_HANDLEAXIS2 = 0x5f;		// ��p�L�����Q
const short  REQ_SINGLE = 0x60;		// �V���O���X�e�b�v���[�h
const short  REQ_TEACH = 0x61;		// �e�B�[�`���O
const short  REQ_PRGINS = 0x62;		// �v���O�����}��
const short  REQ_PRGALT = 0x63;		// �v���O�����u��
const short  REQ_PRGDEL = 0x64;		// �v���O�����폜
const short  REQ_PRGREV = 0x65;		// �v���O�����t�s�^�]
const short  REQ_STEPCHG = 0x66;		// �v���O�����X�e�b�v�ύX
const short  REQ_STEPSKIP = 0x67;		// �v���O�����X�e�b�v�X�L�b�v
const short  REQ_AXMV = 0x68;		// �Ɨ��ʒu���߃C���N���w��
const short  REQ_AXMVA = 0x69;		// �Ɨ��ʒu���ߘ_�����W�n�A�u�\�w��
const short  REQ_AXMVB = 0x6a;		// �Ɨ��ʒu���ߋ@�B���W�n�A�u�\�w��
const short  REQ_PRGBLKMV = 0x6B;		// �v���O�����u���b�N�ړ�
const short  REQ_PRGBLKCPY = 0x6C;		// �v���O�����u���b�N�R�s�[
const short  REQ_PRGBLKDEL = 0x6D;		// �v���O�����u���b�N�폜
const short  REQ_REFMEM = 0x6E;		// Flash��؂։^�]��۸��є��f�w��
const short  REQ_FBSETUP = 0x78;		// �e�a�J�E���^�����ݒ�
const short  REQ_TASKSTART = 0x80;		// ��������۸��ъJ�n
const short  REQ_TASKSTOP = 0x81;		// ��������۸��ђ�~
const short  REQ_TASKRESET = 0x82;		// ��������۸���ؾ��
const short  REQ_HOME = 0x83;		// homepos�ړ�
const short  REQ_MCRREG = 0x84;		// �}�N���ϐ��������݃R�}���h
const short  REQ_COVRDCHGP = 0x85;		// ��ԃI�[�o�[���C�h���l�ύX
const short  REQ_SOVRDCHGP = 0x86;		// �厲�I�[�o�[���C�h���l�ύX
const short  REQ_MABSCOORDSET = 0x90;		// �A�u�\���W�ݒ�R�}���h

const short  REQ_OPTSTOP = 0xa0;		/* ��߼��ٽį�ߗL��/���������		*/
const short  REQ_TOUCHSENSE = 0xa1;		/* �ڐG���m�L��/�����R�}���h		*/
const short  REQ_WTOPPOS = 0xa2;		/* �v������l�ݒ�R�}���h			*/
const short  REQ_RETURN = 0xa3;		/* �ԑ��R�}���h						*/
const short  REQ_PNOSEL = 0xa4;		/* ���H�����ԍ��ύX�R�}���h			*/
const short  REQ_DRYRUN = 0xa5;		/* �h���C�����L��/�����R�}���h		*/
const short  REQ_ELCTDCHGEN = 0xa6;		/* �d�Ɍ����L��/�����R�}���h		*/
const short  REQ_INITIALSET = 0xa7;		/* InitialSet�L��/�����R�}���h		*/
const short  REQ_GUIDETHROUGH = 0xa8;		/* �K�C�h�ђʓ��싖�R�}���h		*/

const short  REQ_PROGSTRTN = 0xad;		/* �v���O�������s�J�n�R�}���h		*/
const short  REQ_MOVEPRGSTOPPOS = 0xae;		/* ��۸��ю��s��~�ʒu�ړ��J�n�����	*/
const short  REQ_PARTITIONCHG = 0xaf;		/* �p�[�e�B�V�����ݒ�R�}���h		*/
const short  REQ_ELCTDNOCHG = 0xb0;		/* �d�ɔԍ��ݒ�R�}���h				*/
const short  REQ_ELCTDINSTALL = 0xb1;		/* �d�ɑ���/�E������J�n�R�}���h	*/
const short  REQ_ELCTDEXCHPOS = 0xb2;		/* �d�Ɍ����ʒu�ړ��R�}���h			*/

const short  REQ_ESFMAGAZINE_INC = 0xb4;		/* ESF϶޼�ݲݸ���ĺ����			*/
const short  REQ_MOVEESFARM = 0xb5;		/* ESF��шړ��i�O�[/����/��[�j		*/
const short  REQ_OPENESFFINGER = 0xb6;		/* ESF̨ݶްOPEN/CLOSE				*/
const short  REQ_GUIDENOCHG = 0xb7;		/* �K�C�h�ԍ��ݒ�R�}���h			*/
const short  REQ_GUIDEINSTALL = 0xb8;		/* �K�C�h����/�E������J�n�R�}���h	*/
const short  REQ_GUIDEEXCHPOS = 0xb9;		/* �K�C�h�����ʒu�ړ�				*/
const short  REQ_MOVEGSFARM = 0xbb;		/* �f�r�e�A�[���ړ��i�O�[/��[�j	*/
const short  REQ_GUIDECLUMP = 0xbc;		/* �K�C�h�N�����v/�A���N�����v		*/
const short  REQ_SPCLUMP = 0xbd;		/* �X�s���h���N�����v/�A���N�����v	*/

const short  REQ_GUIDECHKPOS = 0xbf;		/* �K�C�h�m�F�ʒu�ړ��R�}���h		*/

const short  REQ_ESFMAGAZINE_MOV = 0xc1;		/* �d�r�e�}�K�W���ړ��R�}���h		*/
const short  REQ_INCREFSET_MOV = 0xc2;		/* ���Α���_�ݒ莞���ړ��L�������	*/
const short  REQ_PTPASTART_W = 0xc3;		/* W������Ҕ�L���o�s�o�ړ��J�n(�_�����W�nABSO)	*/
const short  REQ_PTPBSTART_W = 0xc4;		/* W������Ҕ�L���o�s�o�ړ��J�n(�@�B���W�nABSO)	*/
const short  REQ_WORGPOSCHG = 0xc5;		/* ���[�N���_���W�ݒ�R�}���h		*/
const short  REQ_PATROUNDSTOP = 0xc6;		/* �߰è��ݓ��P����~�L���R�}���h	*/
const short  REQ_XY_ILOCK = 0xc7;		/* X/Y������ۯ��L��/�����R�}���h	*/
const short  REQ_MCHLOCK = 0xc8;		/* �}�V�����b�N�L��/�����R�}���h	*/
const short  REQ_CORR_ANG = 0xc9;		/* ��۸��ъJ�n���p�x�␳�L��/���������	*/
const short  REQ_BLOCKSKIP = 0xca;		/* BlockSkip�L��/�����R�}���h		*/
const short  REQ_ANGLESET = 0xcb;		/* �␳�p�x�ݒ�R�}���h				*/
const short  REQ_GDTHROUGH = 0xcc;		/* �K�C�h�ђʓ���J�n�R�}���h		*/
const short  REQ_VIRPOSCHG_EX = 0xcd;		/* ���z�_�^����_�ύX�R�}���h		*/

const short  REQ_HANDLEPERMIT = 0xcf;		/* �蓮�p���T�[���R�}���h			*/
const short  REQ_PRCFEED = 0xd0;		/* ���H�����ɂ��厲���x�ʒm�R�}���h	*/
const short  REQ_FORCEZRNFIN = 0xd1;		/* �������_���A�����ݒ�R�}���h		*/
const short  REQ_DRYRUN_EX = 0xd2;		/* �h���C�����ݒ�(���d���H�����w��)�R�}���h	*/

const short  REQ_PUMPCMND = 0xd5;		/* �|���v����R�}���h				*/
const short  REQ_BONCMND = 0xd6;		/* ���d����R�}���h					*/
const short  REQ_PRCSSKIP = 0xd7;		/* ���d���H�X�L�b�v�R�}���h			*/
const short  REQ_AUTOMODE_OUTPUT = 0xd8;		/* �������[�h���o�̓R�}���h			*/
const short  REQ_SHUTDOWN_START = 0xd9;		/* �V���b�g�_�E���J�n�R�}���h		*/
const short  REQ_BUZZER_ON_OUTPUT = 0xda;		/* �u�U�[ON/OFF�M���o�̓R�}���h		*/

typedef struct
{
	int nSize;			// �ʐM�������\���̃T�C�Y
	short fComType;		// �ʐM��ʃt���O
	short fShare;		// ���L�t���O
	int fLogging;		// �ʐM���M���O�t���O
	char* pLogFile;		// �ʐM���M���O�t�@�C����
	short pNodeName;	// INtime�m�[�h��
	short Reserved[2];	// �\��
}SXDEF, *PSXDEF;

/************************************************************************/
/* �q�n�l�r�v�p�����[�^�\���́i�o�b����p�j */
/************************************************************************/
typedef struct
{ /* ROMSW�p�����[�^�\���́i�Q�T�U�O�O�޲ČŒ蒷�j */
	_int64 st_en; /* �L���ǃt���O(�����ǃt���O) */
	_int64 axis_en; /* �L�����t���O(�����ǃt���O) */
	_int64 io_en; /* �L��IO�t���O(�����ǃt���O) */
	unsigned char Reserved1[20496]; /* �\��*/
	_int64 spindle_ax; /* �厲�L�����t���O*/
	unsigned char Reserved2[5072]; /* �\��*/
} ROMSW;

/************************************************************************/
/* �������p�����[�^�\����*/
/************************************************************************/
typedef struct
{
	long	ElctdExchPos[9];			// �d�Ɍ����ʒu
	long		ElctdExchSpdW;			// �v���d�Ɍ����ʒu�ړ����x
	long		ElctdExchSpdW1;			// �v���d�Ɍ����O�ʒu�ړ����x
	long		ElctdExchOfsW1;			// �v���d�Ɍ����O�ʒu�I�t�Z�b�g
	long		ElctdExchOfsW2;			// �v���d�Ɍ����ҋ@�ʒu�I�t�Z�b�g

	long	ElctdChkSpdZ1[6];			// �d�ɑ�����̃Z���T�[�܂ł̂y�����~���x

	long	ElctdChkSpdZ2[6];			// �d�ɑ�����̃Z���T�[����̂y�����~���x

	long	ElctdChkSpdS2[6];			// �d�ɑ�����̃Z���T�[����̎厲��]���x

	long	ElctdChkOfsZ[6];			// �d�ɑ�����̃Z���T�[����̂y�����~��
	long		ElctdDeplUpZ;			// �d�ɏ���(Z-OT��)�^�r����~���̂y���㏸��
	short	ElctdNum;				// �d�ɐ�
	short	GuideNum;				// �K�C�h��

	long	GuideExchPosS[9];			// �K�C�h�����ʒu�n�_

	long	GuideExchPosE[9];			// �K�C�h�����ʒu�I�_

	long	GuideChkPos[9];			// �K�C�h�L���Z���T�[�ʒu
	long		GuideExchSpdW;			// �v���K�C�h�����ʒu�ړ����x
	long		GuideExchOfsW1;			// �v���K�C�h�����O�ʒu�I�t�Z�b�g
	long		GuideExchOfsW2;			// �v���K�C�h�����ҋ@�ʒu�I�t�Z�b�g

	long	EdgeSrchOfs[9];			// �[�ʈʒu�������́{�������́|���ړ���

	long	EdgeSrchTouchSpd1[9];		// �[�ʈʒu�������̂P�x�ڂ̐ڐG���x

	long	EdgeSrchTouchRet1[9];		// �[�ʈʒu�������̂P�x�ڂ̖߂��

	long	EdgeSrchTouchSpd2[9];		// �[�ʈʒu�������̂Q�C�R�x�ڂ̐ڐG���x

	long	EdgeSrchTouchRet2[9];		// �[�ʈʒu�������̂Q�C�R�x�ڂ̖߂��
	long		EdgeSrchZDwnSpd;		// �[�ʈʒu�������̂y�����~���x
	long		TouchSenseTime;			// �[�ʈʒu�������ڐG���m����
	long		RefTouchUpZ;			// ����_�y�ڐG��̂y�t�o�ʐݒ�
	long		PrcsRetSpdZ;			// ���d���H�I�����y���㏸���x
	long		EdgeSrchZUpSpd;			// �[�ʈʒu�������̂y���㏸���x
	long		ElctdClumpSpdS;			// �d�ɑ���(�گĸ����)�܂ł̎厲��]���x
	long		BucklingUpOfsZ;			// �א��d�Ɋm�F���̍����ݻ�ON�ɂ��Z�㏸��
	long		BucklingUpSpdZ;			// �א��d�Ɋm�F���̍����ݻ�ON�ɂ��Z�㏸���x
	short	BucklingRetry;			// �א��d�Ɋm�F���̍����ݻ�ON�ɂ����ײ��
	short	AecEnable;				// �`�d�b�L��/����(D0:ESF�AD1:GSF)
	long		Z20ErrOfs;				// �y�Q�O�G���[���o�I�t�Z�b�g
	short	McnType;				// �@�B�^�C�v�i0:�ʏ�@�A1:���d�l�@�j
	short	GuideSensorDis;			// �K�C�h�ђʌ��o�Z���T�[�����ݒ�

	long	CysCheckDis[4];			// CYS�`�F�b�N�����ݒ�
									// 	CysCheckDis[0] : D00:  1 �` D31: 32
									// 	CysCheckDis[1] : D00: 33 �` D31: 64
									// 	CysCheckDis[2] : D00: 65 �` D31: 96
									// 	CysCheckDis[3] : D00: 97 �` D31:128
	short	AxAecActDis;			// �d�ɁE�K�C�h�������얳�����ݒ�(A/B/C���̂�)
	short	AxBrakeEn;				// �u���[�L���ݒ�(A/B/C���̂�)
	short	BrakeTimer;				// �u���[�L�쓮����[msec]
	short	TchErrCancelTime;		// �ڐG���m�G���[�L�����Z������

	long	CylPulseOut[4];			// CYL�p���X�o�͐ݒ�
									// 	CylPulseOut[0] : D00:  1 �` D31: 32
									// 	CylPulseOut[1] : D00: 33 �` D31: 64
									// 	CylPulseOut[2] : D00: 65 �` D31: 96
									// 	CylPulseOut[3] : D00: 97 �` D31:128
	short	CylPulseTime;			// CYL�p���X��[msec]
	short	PrcsFirstP99xDis;		// ���d���H��FirstSpark�܂ł̉��H����P99x(1�`6)�����ݒ�
	short	LeaveZrnFin;			// �A���[�������_���A�ς��׸ނ��c�����׸�
	short	EdgeSrchMaxMinUse;		// �[�ʈʒu�v�Z���ő�/�ŏ��l�g�p�t���O
	short	EdgeSrchOldMcnComp;		// ��Ű�o��/�c�o������̋��@��݊������׸�
	short	CylSelBfrEDeplAec;		// �d�ɏ��Վ���AEC�O CYL�o�͑I��
									// (0�`128:0�͖����AD08��OFF�w��)
	short	CylSelAftEDeplAec;		// �d�ɏ��Վ���AEC�� CYL�o�͑I��
									// (0�`128:0�͖����AD08��OFF�w��)

	char	Reserved556[2];			// �\��

	long	CysOffChkDis[4];			// CYS�I�t�`�F�b�N�����ݒ�
										// 	CysOffChkDis[0] : D00:  1 �` D31: 32
										// 	CysOffChkDis[1] : D00: 33 �` D31: 64
										// 	CysOffChkDis[2] : D00: 65 �` D31: 96
										// 	CysOffChkDis[3] : D00: 97 �` D31:128
	int		SvPrcsStpRetOfs;		// �T�[�{���d���H�r����~���߂��
	int		SvPrcsRetOfs;			// �T�[�{���d���H�I�����߂��
	int		SvPrcsRetSpd;			// �T�[�{���d���H�I�����߂葬�x
	short	AxServoPrcs;			// �T�[�{���d���H�Ώێ��ݒ�(D0:X �` )

	char	Reserved588[2];			// �\��

	long	CysLatch[4];				// CYS�M�����b�`�I��
										// 	CysLatch[0] : D00:  1 �` D31: 32
										// 	CysLatch[1] : D00: 33 �` D31: 64
										// 	CysLatch[2] : D00: 65 �` D31: 96
										// 	CysLatch[3] : D00: 97 �` D31:128
	short	InstLastElctdFlg;		// �ŏI�d�ɑ������[�h�L���t���O

	char	Reserved608[2];			// �\��

	long	SpinMaxSpd[9];				// ������]����]���x����l [pps]
	short	PrcsSkipStopCys;		// ���d���H�X�L�b�v����~���[�hCYS�M���I��
	short	DischgTchErrEn;			// ���d���̐ڐG���m�G���[���o�L���t���O

	long	CFeedTable[16];				// �厲���x(0�`15)->�b�����x(0.1rpm)�e�[�u��
	short	GuideThroughChkDis;		// �K�C�h�ђʌ��o�����p�[�e�B�V�����t���O
	short	Z2ndSoftLimMSelCys;		// �y����Q�\�t�g���~�b�g�w��CYS�M���I��
	long		Z2ndSoftLimM;			// �y����Q�\�t�g���~�b�g
	short	GuidChgCylEn;			// �K�C�h�������b�x�k�o�͗L���t���O
									//   0:����, 1�`128:CYL�o�͐M���擪�ԍ�
	short	GuidChgCysSel;			// �K�C�h�������b�x�k�o�͌�b�x�r�M���ґI��
									//   0:����, 1�`128:CYS�M���ԍ�

	long	GuidHolderEscOfs[9];		// �K�C�h�z���_�[�ޔ��
	short	GuidHolderArmDis;		// �K�C�h�z���_�[�A�[���V�����_�����t���O
	short	TouchSenseISetEn;		// �ڐG���m�ɂ��Ƽ�پ�ėL���׸�(�d�������������l)
	short	CylSelPatRndStp;		// �p�[�e�B�V�����P����~�� CYL�o�͑I��
	short	PatRndStpOnlyElctdDep;	// �d�ɏ��Ղɂ��d�Ɍ������̂��߰è��݂P����~�L���׸�
	short	CylSelThrghDetect;		// ���d���H���ђʌ��o���CYL�o�͑I��
									// (0�`128:0�͖����AD08��OFF�w��)
	short	RefSpdSelThrghDetect;	// ���d���H���ђʌ��o����x�I��(0:�����׎����x�A1:SFR-DN)
	short	PerInitThrghDetect;		// ���d���H���ђʌ��o����I�������l   (M91 I�w��)
	short	PerInitThDetectEdge;	// ���d���H�������ی��o����I�������l (M81 I�w��)
	long		AvTimInitThDetectEdge;	// ���d���H�������ی��o���x�v���ݒ菉���l (M81 J�w��)
	short	RefSpdIgnHPerInit;		// ���d���H���ђʌ��o����x�v���㑤�����������l (M25 H�w��)
	short	RefSpdIgnLPerInit;		// ���d���H���ђʌ��o����x�v�����������������l (M25 I�w��)
	short	RefSpdAvTimInit;		// ���d���H���ђʌ��o����x�v�����ω����ԏ����l (M25 J�w��)

	char	Reserved788[2];			// �\��
	int		PreElctdChgLmtPos;		// �d�ɏ��ՑO�d�Ɍ������~�b�g���W

	short	ElctdExchMovOrder[9];		// �d�Ɍ����ʒu�ړ����i1�`9�F0�͍Ō�j
	short	CylSelInElctdPrcs;		// �P���d�ɕ��d���H��CYL�o�͑I�� (0�`128 : 0�͏o�͖���)
	short	PatliteMode;			// �p�g���C�g�o�̓��[�h�I�� (0�`2)
	short	AxVirActDis;			// ���z�_/����_�ړ��������t���O(A/B/C���̂�)(�d�������������l)
	short	SvPrcsInitialTime;		// �T�[�{���d���H����������(���d����҂�����) [msec]
	short	ThinElctdISetPumpOff;	// �א��d�ɂł̃C�j�V�����Z�b�g��
									// �|���v�����p�[�e�B�V�����t���O(D0:1 �` D5:6)
	short	AecWRefSel;				// �`�d�b�J�n/�I�����v���Ҕ��ʒu�I��
									// (0=�v�����_, 1=�v������l)

	char	Reserved824[2];			// �\��
	short	GuideThroughChkPNo;		// �޲�ފђ����������H�����ԍ��ݒ� (�d�������������l)
									// (0			= �]���ʂ�P998/999���g�p
									//  16384�`17383 = P0�`P999���g�p
									//				 (D14��L���t���O�Ƃ��Ďg�p))

	short	InitialSetPno[6];			// �Ƽ�پ�Ď����H�����ԍ��ݒ� (�d�������������l)
										// (0			= �]���ʂ�P998/999���g�p
										//  16384�`17383 = P0�`P999���g�p
										//				 (D14��L���t���O�Ƃ��Ďg�p))

	char	Reserved840[2];				// �\��

	long	ElctdChkOfsZ2[6];			// �d�ɑ�����̾ݻ�����̂y���Q�i�ډ��~�ʐݒ�(�d�������������l)

	short	GuideThroughChkPNoPat[6];	// �K�C�h�ђʃ`�F�b�N�����H�����ԍ��ݒ� (�d�������������l)
										// (0			= �]���ʂ�P998/999���g�p
										//  16384�`17383 = P0�`P999���g�p
										//				 (D14��L���t���O�Ƃ��Ďg�p))
	short	EUninstColReclampDis;	// �d�ɒE�����̃R���b�g�ăN�����v(��������)���얳���t���O

	char	Reserved880[2];			// �\��
	int		AvTimInitThrghDetect;	// ���d���H���ђʌ��o���x�v���ݒ�
									//(���ω�����/���x�v������)�����l(M91 J�w��)
	int		EdgeLSrchTouchRet;		// ������������Ԓ[�ʈʒu�������߂��[pls](�S����������)
	int		EdgeLSrchRetSpd;		// ������������Ԓ[�ʈʒu�������߂葬�x[pps](�S���������x)

	char	Reserved896[4];			// �\��

	double	InitPrmMacVal[10];			// �������p�����[�^�w��Œ�}�N���ϐ��l(#1500�`#1509)
	short	NrmElctdPrcsBuckling;	// �ʏ�d�ɂł̕��d���H�������ݻ����o�t���O
									// (0:�����A1:�G���[�A2:��~�A3:�d�Ɍ���)
	short	NrmElctdGuideBuckling;	// �ʏ�d�ɂł̃K�C�h�ђʎ������ݻ����o�t���O
									// (0:�����A1:�G���[�A2:���d�Ɏ擾)
	short	RotAxMovMode;			// ������]���A�u�\�ʒu���߈ړ����@�����l (0�`5)
									// (0:�]���ʂ�A1:�߉��A
									//  2:��������(360���ȏ�Ȃ�)�A
									//  3:��������(360���ȏ゠��)�A
									//  4:�펞�{�����A5:�펞�|����)
	short	AutoModeOutSel;			// �������[�h���o�͐M���I��
									// (-1�`128�F0=����, -1=o#1727 D06, 1�`128=CYL�M��)

	long	CylBitRstPlsCont[4];		// ���Z�b�g/�G���[������CYL�p���X�o�͌p���ݒ�
										// 	CylBitRstPlsCont[0] : D00:  1 �` D31: 32
										// 	CylBitRstPlsCont[1] : D00: 33 �` D31: 64
										// 	CylBitRstPlsCont[2] : D00: 65 �` D31: 96
										// 	CylBitRstPlsCont[3] : D00: 97 �` D31:128
	short	PrcsTmoutChkEndSel;		// ���d���H�^�C���A�E�g���o�I���^�C�~���O�I��
									// (0:���H�[�����B�ŏI���A
									//  1:���dOFF(�X�p�[�N�A�E�g�^�C�}���Ԍo��
									//   �����d���H���ђʌ��o���CYL�o�͊���)�ŏI��)

	char	Reserved1004[2];			// �\��
	int		EIFErr1MaskSet;			// EtherCAT IF�{�[�h �G���[1�}�X�N�ݒ�
	int		EIFErr2MaskSet;			// EtherCAT IF�{�[�h �G���[2�}�X�N�ݒ�
	short	Fanstop10DlyTim;		// �{�@��FAN��~ ���H�I����҂�����[sec]
	short	Fanstop20DlyTim;		// SF02FX��FAN��~ ���H�I����҂�����[sec]

	long	SfrFrTable[16];				// ���H�T�[�{���葬�x�e�[�u��[pls/min]

	long	SfrBkTable[16];				// ���H�T�[�{�߂葬�x�e�[�u��[pls/min]
	short	SoftStartLowSpeed;		// �\�t�g�X�^�[�g�ᑬ���x�ݒ�[50�`750ms(50ms�Ԋu)]
	short	SoftStartHighSpeed;		// �\�t�g�X�^�[�g�������x�ݒ�[ 2�` 30ms( 2ms�Ԋu)]
	short	SoftStartIP;			// �\�t�g�X�^�[�g�J�n�d���ݒ�
									//  0x00:IP=0A, 0x01:IP=3A����J�n����B
	short	BONDLY_E323_O;			// BONDLY_E323_O�ݒ�[1�`255ms(�f�t�H���g8ms)]
	short	BONDLY0;				// BONDLY_0�ݒ�     [1�`255ms(�f�t�H���g8ms)]
	short	BON_SignSel;			// BON�M���I��ݒ�
									//  0x00:BON10=BONDLY_E323_O / BON00=BONDLY0
									//  0x01:BON10=BON0          / BON00=BON10
	short	OFFExtendRatio;			// OFF�L�΂��{���ݒ�[1�`32�{(�f�t�H���g10�{)]
	short	ZClrErrTrgSelSet;		// Z�N���A�G���[���o�g���K�M���I��ݒ�
									//  0x00:VSPK , 0x01:VS
	short	ZClrErrCntSet;			// Z�N���A�G���[�J�E���g���ݒ�
									//  0x00�`0xFF(�f�t�H���g0x10)
	char	Reserved2048[886];			// �\��
} INITIALPRM;

/*******************************************************************/
/* �A���T�[�X�e�[�^�X���\����*/
/*******************************************************************/
typedef struct
{ /* �S�̏��\���̂P�U�޲�*/
	long Status; /* �S�̃X�e�[�^�X*/
	long Alarm; /* �S�̃A���[��*/
	char Reserved[8]; /* �\��*/
} MCSTATUS;
typedef struct
{ /* �e�����\���̂S�W�޲�*/
	long AxStatus; /* ���X�e�[�^�X*/
	long AxAlarm; /* ���A���[��*/
	long ComReg; /* �w�߈ʒu*/
	long PosReg; /* �@�B�ʒu*/
	long ErrReg; /* �΍���*/
	long BlockSeg; /* �ŐV�u���b�N�����o����*/
	long AbsReg; /* ��Έʒu(�w��) */
	long Trq; /* �g���N*/
	long AMrReg; /* ��Έʒu(�w��:ϼ�ۯ�����) */
	char Reserved[12]; /* �\��*/
} AXSTATUS;
typedef struct
{ /* �^�X�N���\���̂R�Q�޲�*/
	long TaskStatus; /* �^�X�N�X�e�[�^�X*/
	long TaskAlarm; /* �^�X�N�A���[��*/
	short Override; /* ����I�[�o�[���C�h�ݒ�*/
	short COverride; /* ��ԃI�[�o�[���C�h�ݒ�*/
	short SOverride; /* �厲�I�[�o�[���C�h�ݒ�*/
	short ProgramNo; /* �I���E���s�v���O�����ԍ�*/
	unsigned long StepNo; /* ���s�X�e�b�v�ԍ�*/
	short NNo; /* �ҋ@�E���s�m�ԍ�*/
	short LineNo; /* �ҋ@�E���s�s�ԍ�*/
	short LineFlg;	/* �ҋ@�E���s�s�ԍ����*/
	char Reserved[6]; /* �\��*/
} TASKSTATUS;

typedef struct
{ /* ECNC�X�e�[�^�X���\����*/
	unsigned short Status2; /* �e���ԏ��Q */
	unsigned short Status3; /* �e���ԏ��R */
	unsigned short Alarm2; /* �A���[�����Q */
	unsigned short Alarm3; /* �A���[�����R */
	unsigned short Alarm4; /* �A���[�����S */
	unsigned short Alarm5; /* �A���[�����T */
	char Reserved0[4]; /* �\��*/
	long WTopPos; /* �v������l*/
	long CorrAng; /* �␳�p�x(8-24�ޯČŒ菬���_)*/
	unsigned long EIFErr1; /* EtherCAT IF�{�[�h�G���[1 */
	unsigned long EIFErr2; /* EtherCAT IF�{�[�h�G���[2 */
	short ADVal[5]; /* �A�i���O���͒l*/
	char Reserved1[6]; /* �\��*/
} ECNCSTATUS;
typedef struct
{ /* �A���T�[�X�e�[�^�X���\����*/
	MCSTATUS mc; /* �S�̏��*/
	AXSTATUS ax[64]; /* �e�����*/
	TASKSTATUS task[8]; /* �^�X�N���*/
	ECNCSTATUS ecnc; /* ECNC���*/
} STATUS;

/************************************************************************/
/* �蓮�p���T�[���싖�R�}���h�\����*/
/************************************************************************/
typedef struct
{
	short permit; /* ����/�s���t���O(0:�s���A1:����) */
} HANDLEPERMIT;

/************************************************************************/
/* ���샂�[�h�f�[�^�ύX�R�}���h�t���f�[�^�\����*/
/************************************************************************/
typedef struct
{
	short mode; /* ���샂�[�h*/
} MODECHG;

/************************************************************************/
/* �L���^�����ݒ�\����*/
/************************************************************************/
typedef struct
{
	short enable; /* �L��/�����ݒ�t���O(0:�����A1:�L��) */
} ENABLE;

/************************************************************************/
/* ���H�����ԍ��ݒ�R�}���h�\����*/
/************************************************************************/
typedef struct
{
	long PNo; /* ���H�����ԍ�*/
	char Reserved[4]; /* �\��*/
} PRNOSEL;

/************************************************************************/
/* �|�C���g�\����*/
/************************************************************************/
typedef struct
{
	long Pnt[9]; /* ��P�`�X�����W�l*/
} AXPOINT;

/************************************************************************/
/* �e�����z�_/����_���W�\����*/
/************************************************************************/
typedef struct
{
	AXPOINT AxPnt[101]; /* �e�����z�_/����_���W*/
						/* �i0�`99�F���z�_�A100:����_�j */
	char Reserved[4];/* �\��*/
} VIRPOS_EX;
/************************************************************************/
/* ���[�N���_���W�ݒ�\����*/
/************************************************************************/
typedef struct
{
	long WorkOrg[9]; /* ��P�`�X�����[�N���_���W*/
} WORKORG;
/************************************************************************/
/* �p�����[�^�f�[�^�\����(9080�o�C�g�Œ蒷) */
/************************************************************************/
typedef struct
{ /* �e���Ɨ��p�����[�^�\����(140�o�C�g�Œ蒷) */
	long InPos; /* �h�m�o�n�r��[pls] */
	long ErMax; /* �΍�����l[pls] */
	long MPosErMax; /* �l�o�n�r�΍�����l[pls] */
	long Ka; /* ��Ԏ��萔[msec] */
	long SKa; /* �r����Ԏ��萔[msec] */
	long Dx; /* �o�s�o���萔[msec] */
	long PtpFeed; /* �o�s�o���x[pls/sec] */
	long JogFeed; /* �i�n�f���葬�x[pls/sec] */
	long SoftLimP; /* �\�t�g���~�b�g�{��[pls] */
	long SoftLimM; /* �\�t�g���~�b�g�|��[pls] */
	long OrgDir; /* ���_���A����*/
	long OrgOfs; /* ���_����[pls] */
	long OrgPos; /* ���_���A�����ʒu[pls] */
	long OrgFeed; /* ���_���A�����葬�x[pls/sec] */
	long AprFeed; /* ���_���A�A�v���[�`���x[pls/sec] */
	long SrchFeed; /* ���_���A�ŏI�T�[�`���x[pls/sec] */
	long OrgPri; /* ���_���A����*/
	long Homepos; /* ΰ��߼޼�݈ʒu[pls] */
	long Homepri; /* ΰ��߼޼�ݏ���*/
	long BackL; /* �o�b�N���b�V���␳��[pls] */
	long Revise; /* �`��␳�W��*/
	long OrgCsetOfs; /* ���_���A���_�����W*/
	long handle_max; /* �ޮ��è��/����ٍő呗�葬�x*/
	long handle_ka; /* �ޮ��è��/����ى��������萔*/
	unsigned char Reserved[44]; /* ���g�p*/
} AXIS_PARAMETER;
typedef struct
{ /* ����p�����[�^���p��(120�o�C�g) */
	unsigned char Reserved[120]; /* ����p�����[�^����`*/
} ADD_PARAMETER;
typedef struct
{ /* (1720�o�C�g) */
	AXIS_PARAMETER AxisParam[64]; /* �e���p�����[�^*/
	ADD_PARAMETER adp; /* ����p�����[�^*/
} PARAMETER_DATA;

/************************************************************************/
/* �s�b�`�G���[�␳�p�p�����[�^���\���́i34560�o�C�g�Œ蒷�j */
/************************************************************************/
typedef struct
{ /* �e���␳�p�p�����[�^�i20�o�C�g�j */
	unsigned long RevMagnify; /* �␳�{��*/
	unsigned long RevSpace; /* �␳�Ԋu*/
	unsigned long RevTopNo; /* �␳�f�[�^�擪�ԍ�*/
	unsigned long RevMCnt; /* -���␳��Ԑ�*/
	unsigned long RevPCnt; /* +���␳��Ԑ�*/
} REV_AX;
typedef struct
{ /* �s�b�`�G���[�␳�p�p�����[�^*/
	REV_AX RevAx[64]; /* �e���␳�p�p�����[�^*/
	short RevDt[33280]; /* �␳�f�[�^*/
} PITCH_ERR_REV;

/************************************************************************/
/* ���z�_�^����_�ύX�R�}���h�\����*/
/************************************************************************/
typedef struct
{
	long VirNo; /* ���z�_/����_�ԍ�*/
	long VirPos[9]; /* ���z�_���W*/
} VIRPOSCHG;


/************************************************************************/
/* �v���O�����ϊ����C�u�����������\����*/
/************************************************************************/
typedef struct
{ /* �v���O�����ϊ����C�u�����������\���́iVer2.6 �`�j*/
	short RtcTime; /* �q�s�l�b�U�S�|�d�b�������*/
	short InchMode; /* inch/mm �w��i0:mm�A1:inch�j */
	const char *pGUCD; /* հ�ް��`�f����̧�يi�[�ިڸ�ؖ�*/
	const char *pMUCD; /* հ�ް��`�l����̧�يi�[�ިڸ�ؖ�*/
	short InchAxis; /* inch/mm�Ώێ��t���O*/
	short align; /* �\��*/
	long StrctSize; /* �������\���̃T�C�Y(0:PRGINITDATA_OLD)*/
	unsigned char GcdDis[256]; /* �����f�R�[�h�t���O(0:�L���A1:����) */
	unsigned char McdDis[256]; /* �����l�R�[�h�t���O(0:�L���A1:����) */
	long ZAcServoEn; /* �y���`�b�T�[�{�@�\�L���t���O*/ // Ver3.0�`
} PRGINITDATA;

/************************************************************************/
/* �ėp���o�͏��\����*/
/************************************************************************/
typedef struct
{
	unsigned short Input[116]; /* �ėp����*/
	unsigned short Output[64]; /* �ėp�o��*/
} IODATA;

/************************************************************************/
/* �`�d�b��ԍ\����*/
/************************************************************************/
typedef struct
{
	short PartitionS[6]; /* ��P�`�U�߰è��݊J�n�ԍ�*/
	short PartitionE[6]; /* ��P�`�U�߰è��ݏI���ԍ�*/
	short Thinline[6]; /* ��1�`6�߰è��ݍא��ݒ�(1:�א�,0:�ʏ�)*/
	short PartitionDis; /* �߰è��ݖ����׸�(1:����,0:�L��) */
	short ElectrodeNo; /* �d�ɔԍ�(0:���ݒ�) */
	short GuideNo; /* �K�C�h�ԍ�(0:���ݒ�) */
	short IndexZrnFin; /* �C���f�b�N�X�ԍ��L���t���O*/
	short IndexNo; /* �C���f�b�N�X�ԍ�*/
	char Reserved[18];
} AECDATA;

/************************************************************************/
/* ���H�������\����*/
/************************************************************************/
typedef struct
{
	short Status; /* �e���ԏ��*/
	short PNo; /* ���H�����ԍ�*/
	short ZUpFeed; /* �y���㏸���x*/
	short ZDwFeed; /* �y�����~���x*/
	short CFeed; /* �厲���葬�x*/
	short SpinOut; /* �厲��](0:��~,1:CW,2:CCW) */
	short PumpOut; /* �|���v�o��(0:OFF,1:ON) */
	short PrCFeed; /* ���d���H���厲���x(���H����) */
	long CFeedRpm; /* �b���厲���x(0.1rpm) */
	long DryRunEnN; /* �h���C�����L���t���O(���d���H����) */
	char Reserved[8]; /* �\��*/
} PCONDITION;

/************************************************************************/
/* �K�C�h�ԍ��ݒ�R�}���h�\����*/
/************************************************************************/
typedef struct
{
	short GuideNo; /* �K�C�h�ԍ�*/
	char Reserved[6]; /* �\��*/
} GUIDENO;
/************************************************************************/
/* �d�ɔԍ��ݒ�R�}���h�\����*/
/************************************************************************/
typedef struct
{
	short ElectrodeNo; /* �d�ɔԍ�*/
	char Reserved[6]; /* �\��*/
} ELCTDNO;

/************************************************************************/
/* �厲��]ON/OFF�R�}���h�p�����[�^�\����*/
/************************************************************************/
typedef struct
{
	short SpOut; /* �厲�w�߃t���O*/
} SPCMND;

/************************************************************************/
/* �|���v����R�}���h�\����*/
/************************************************************************/
typedef struct
{
	short PumpOut; /* �|���v����t���O�i0:OFF�A1:ON�j */
	char Reserved[6]; /* �\��*/
} PUMPCMND;

/************************************************************************/
/* ���d����R�}���h�\����*/
/************************************************************************/
typedef struct
{
	short BonOut; /* ���d����t���O�i0:OFF�A1:ON�j*/
	char Reserved[6]; /* �\��*/
} BONCMND;

/************************************************************************/
/* ROM�o�[�W�������\����*/
/************************************************************************/
typedef struct
{
	char Version[16]; /* �o�[�W����������*/
	unsigned short EvenSum; /* SUM:even (rom) */
	unsigned short OddSum; /* SUM:odd (rom) */
	unsigned short FlashSum; /* SUM:SH����FLASH */
	short FlashFlg; /* SH����FLASH�g�p�t���O*/
	unsigned long KindID; /* �@��ID */
	unsigned long SerialID; /* �V���A��ID */
	unsigned long ProductID; /* �v���_�N�gID */
	char Reserved[28]; /* �\��*/
} ROMVERSION;

/************************************************************************/
/* �v������l�ݒ�\����*/
/************************************************************************/
typedef struct
{
	long WTopPos; /* �v������l*/
	char Reserved[4]; /* �\��*/
} WTOPPOS;
/************************************************************************/
/* �����x�I�[�o�[���C�h�ύX�R�}���h�p�����[�^�\����*/
/************************************************************************/
typedef struct
{
	unsigned short Override; /* �I�[�o�[���C�h�ݒ�l*/
} OVERCHG;

/************************************************************************/
/* ���W�n�ݒ�R�}���h�p�����[�^�\����*/
/************************************************************************/
typedef struct
{
	unsigned long AxisFlag; /* ���I���t���O*/
	long PosAxis[9]; /* ��1���`��9���_�����_���W�l*/
} WORGPOSCHG;

/************************************************************************/
/* ��p������\����*/
/************************************************************************/
typedef struct
{
	short handle_mode; /* ��p�L���t���O*/
	short kp; /* ��p�ݒ�{��*/
	short ax1; /* ��p��1��*/
	short ax2; /* ��p��2��*/
} HANDLESTS;

/************************************************************************/
/* �K�C�h�N�����v/�A���N�����v�R�}���h�\����*/
/************************************************************************/
typedef struct
{
	long clump; /* ����t���O(0:Unclump�A1:Clump) */
	char Reserved[4]; /* �\��*/
} GUIDECLUMP;
/************************************************************************/
/* �X�s���h���N�����v/�A���N�����v�R�}���h�\����*/
/************************************************************************/
typedef struct
{
	long clump; /* ����t���O(0:Unclump�A1:Clump) */
	char Reserved[4]; /* �\��*/
} SPCLUMP;

/************************************************************************/
/* ���b�Z�[�W�\���v���f�[�^�\����*/
/************************************************************************/
typedef struct
{
	short LineFlg; /* ���b�Z�[�W�\�����ߎ��s�v���O�������*/
	short MessageNo; /* ���b�Z�[�W�ԍ�*/
	char Reserved[4]; /* �\��*/
} MESSAGEREQ;

/************************************************************************/
/* ���b�Z�[�W�\�����ʃf�[�^�\����*/
/************************************************************************/
typedef struct
{
	MESSAGEREQ Req; /* ���b�Z�[�W�\���v�����f�[�^*/
	short ButtonSel; /* �I��(�N���b�N)�{�^���ԍ�*/
	char Reserved[6]; /* �\��*/
	double InputNum; /* ���l���͒l*/
} MESSAGEANS;

/************************************************************************/
/* �������_���A�����ݒ�R�}���h�\����*/
/************************************************************************/
typedef struct
{
	short AxisFlag; /* ���I���t���O*/
	char Reserved[6]; /* �\��*/
} FORCEZRNFIN;

/************************************************************************/
/* �d�r�e�A�[���ړ��R�}���h�\����*/
/************************************************************************/
typedef struct
{
	short pos; /* ����t���O(0:�O�[,1:����,2:��[) */
	char Reserved[6];/* �\��*/
} MOVEESFARM;

/************************************************************************/
/* �d�r�e�A�[���ړ��R�}���h�\����*/
/************************************************************************/
typedef struct
{
	short open; /* ����t���O(0:Close�A1:Open) */
	char Reserved[6]; /* �\��*/
} OPENESFARM;

/************************************************************************/
/* �d�r�e�}�K�W���ړ��R�}���h�\����*/
/************************************************************************/
typedef struct
{
	long MagazineNo; /* �ړ���}�K�W���ԍ�*/
	char Reserved[4]; /* �\��*/
} ESFMAGMOV;

/************************************************************************/
/* �f�r�e�A�[���ړ��R�}���h�\����*/
/************************************************************************/
typedef struct
{
	short pos; /* ����t���O(0:�O�[�A1:��[) */
	char Reserved[6]; /* �\��*/
} MOVEGSFARM;

/************************************************************************/
/* �d�ɑ���/�E���R�}���h�\����*/
/************************************************************************/
typedef struct
{
	short install; /* ����t���O(0:�E���A1:����) */
	short ElctdNo; /* �d�ɔԍ�*/
	char Reserved[4]; /* �\��*/
} ELCTDINSTALL;

/************************************************************************/
/* �d�Ɍ����ʒu�ړ��R�}���h�\����*/
/************************************************************************/
typedef struct
{
	short axis; /* ���I���t���O*/
	short pos; /* ����t���O*/
			   /* (0:�����ʒu�A1:�O�ʒu�A2:�ҋ@�ʒu) */
	char Reserved[4];/* �\��*/
} ELCTDEXCHPOS;
/************************************************************************/
/* �K�C�h�����^�E���R�}���h�\����*/
/************************************************************************/
typedef struct
{
	short install; /* ����t���O(0:�E���A1:����) */
	short GuideNo; /* �K�C�h�ԍ�*/
	char Reserved[4]; /* �\��*/
} GUIDEINSTALL;

/************************************************************************/
/* �K�C�h�����ʒu�ړ��R�}���h�\����*/
/************************************************************************/
typedef struct
{
	short axis; /* ���I���t���O*/
	short pos; /* ����t���O(0:�����ʒu�A2:�ҋ@�ʒu) */
	short GuideNo; /* �K�C�h�ԍ�*/
	char Reserved[2];/* �\��*/
} GUIDEEXCHPOS;

/************************************************************************/
/* �K�C�h�m�F�ʒu�ړ��R�}���h�\����*/
/************************************************************************/
typedef struct
{
	short axis; /* ���I���t���O*/
	short pos; /* ����t���O*/
			   /* (0:�m�F�ʒu�A1:�O�ʒu�A2:�ҋ@�ʒu) */
	char Reserved[4];/* �\��*/
} GUIDECHKPOS;

/************************************************************************/
/* �K�C�h�ђʓ��싖�R�}���h�\����*/
/************************************************************************/
typedef struct
{
	short move; /* �ړ������׸�(0:�s����,1:����)*/
	char Reserved[6]; /* �\��*/
} GUIDETHROUGH;

/************************************************************************/
/* �V���b�g�_�E���J�n�R�}���h�\����*/
/************************************************************************/
typedef struct
{
	short startflg; /* �����޳݊J�n�׸�(0:��~,1:�J�n)*/
	char Reserved[6]; /* �\��*/
} SHUTDOWN_START;

/************************************************************************/
/* �u�U�[ON/OFF�M���o�̓R�}���h�\����*/
/************************************************************************/
typedef struct
{
	short onflg; /* �u�U�[ON/OFF�t���O�i0:OFF�A1:ON�j*/
	char Reserved[6]; /* �\��*/
} BUZZER_ON_OUTPUT;

/************************************************************************/
/* �������[�h���o�̓R�}���h�\����*/
/************************************************************************/
typedef struct
{
	short onflg; /* �������[�h���o�̓t���O�i0:OFF�A1:ON�j*/
	char Reserved[6]; /* �\��*/
} AUTOMODE_OUTPUT;

/************************************************************************/
/* ���H�����e�[�u���\����*/
/************************************************************************/
typedef struct
{
	short Ton; /* Ton[us] */
	short Toff; /* Toff[us] */
	long IPVal; /* IP(SFIP)[mA] */
				/* (IP[A]*1000�̒l���i�[) */
	long CAPVal; /* CAP[nF] */
				 /* (CAP[uF]*1000�̒l���i�[) */
	short SCVal; /* �T�[�{�R���g���[��DA(0-63) */
	short CRSVal; /* SP���R���g���[��DA(0-15) */
	short SfrFrSel; /* ���H�T�[�{���葬�x�I��(0-15) */
	short SfrBkSel; /* ���H�T�[�{�߂葬�x�I��(0-15) */
	short InvVal; /* �C���o�[�^DA(0-15) */
	short ServoSel; /* �T�[�{�I��(0-3) */
	short PSSel; /* �d���I��(0-5) */
	short POLVal; /* �����ؑւ�*/
	char Reserved[36]; /* �\��*/
} PCOND_REC;
typedef struct
{
	PCOND_REC rec[1000];
} PCOND_TBL;

/************************************************************************/
/* �������_���A�����ݒ�R�}���h�\����*/
/************************************************************************/
typedef struct
{
	long DryRunEnN; /* �h���C�����L���t���O(���d���H����) */
					/* [0:�h���C��������,-1:�����w�薳��] */
	char Reserved[4];/* �\��*/
} DRYRUN_EX;
/************************************************************************/
/* ���H�����ɂ��厲���x�ʒm�R�}���h�\����*/
/************************************************************************/
typedef struct
{
	short PrCFeed; /* ���H�����ɂ��厲���x*/
	char Reserved[6]; /* �\��*/
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
