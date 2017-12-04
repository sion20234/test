// stdafx.h : �W���̃V�X�e�� �C���N���[�h �t�@�C���̃C���N���[�h �t�@�C���A�܂���
// �Q�Ɖ񐔂������A�����܂�ύX����Ȃ��A�v���W�F�N�g��p�̃C���N���[�h �t�@�C��
// ���L�q���܂��B
//

#pragma once

#include "targetver.h"

#define WIN32_LEAN_AND_MEAN             // Windows �w�b�_�[����g�p����Ă��Ȃ����������O���܂��B
// Windows �w�b�_�[ �t�@�C��:
#include <windows.h>
#include "RT64ECComNTWrap.h"
#include "DynamicMemory.h"

#define FILE_INI		"Debug\\McIfDebug.ini"
//#define FILE_STATUS		"Debug\\STATUS.bin"
//#define FILE_AECDATA	"Debug\\AECDATA.bin"
//#define FILE_PCOND		"Debug\\PCOND.bin"
//#define FILE_PCOND_TBL	"Debug\\PCOND_TBL.bin"
//#define FILE_ORGPOS		"Debug\\VIRPOS_EX.bin"
#define FILE_HANDLESTS	"Debug\\HANDLESTS.bin"
#define FILE_MESSAGEREQ		"Debug\\MESSAGEREQ.bin"
#define FILE_MC_INITIALPRM	"Debug\\initial.ipm"
#define FILE_MC_ROMSW		"Debug\\initial.rom"
#define FILE_MC_PARAMETER	"Debug\\initial.prm"
#define FILE_MC_PITCH		"Debug\\initial.pit"

#define OUTPUTLOG( outputMessage )							\
{															\
	char macroBuf[MAX_PATH];								\
	ZeroMemory( macroBuf, sizeof( macroBuf ) );				\
	wsprintf( macroBuf, "DEBUG>>> %s\n", outputMessage );	\
	OutputDebugString( macroBuf );							\
}

#define CHECK_SIZE( bySizeOf, byParameter )	\
{											\
	if( bySizeOf != byParameter ) {			\
		char buf[MAX_PATH];					\
		wsprintf( buf, "ERROR SendDataWrap SIZE UNMATCH (sizeof=%d/param=%d)\n", bySizeOf, byParameter );	\
		OUTPUTLOG( buf );			\
		return -1;							\
	}										\
}
//	�z��̃T�C�Y�𔻒�
#define SIZE_OF_ARRAY( x )	( sizeof( x ) / sizeof( x[0] ) )

class CAid
{
public:
	CAid(){};
	~CAid() {}

	bool SetBit( SHORT& target, SHORT mask, bool state, bool connectB );
	bool SetBit( USHORT& target, USHORT mask, bool state, bool connectB );
	bool SetBit( LONG& target, LONG mask, bool state, bool connectB );
	bool SetBit( ULONG& target, ULONG mask, bool state, bool connectB );
};

#include "FileCommon.h"

// TODO: �v���O�����ɕK�v�Ȓǉ��w�b�_�[�������ŎQ�Ƃ��Ă�������
