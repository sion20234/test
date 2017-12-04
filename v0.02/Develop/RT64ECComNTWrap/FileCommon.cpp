#include "stdafx.h"
#include "FileCommon.h"
#include <string>	// useful for reading and writing
#include <fstream>	// ifstream, ofstream
#include <assert.h>

//!				
//!	@brief	�R���X�g���N�^
//!				
//!	@since	2016/09/06	Takano	�V�K�쐬
//!				
CFileCommon::CFileCommon()
{
}

//!				
//!	@brief	�f�X�g���N�^
//!				
//!	@since	2016/09/06	Takano	�V�K�쐬
//!				
CFileCommon::~CFileCommon()
{
}

//!				
//!	@brief	�t�@�C���ǂݍ���
//!				
//!	@details	���� path �Ŏw�肳�ꂽ�t�@�C�����w�肳�ꂽ�T�C�Y�ǂݍ��݂܂��B
//!				
//!	@param[out]	result	�ǂݎ��ꂽ���
//!	@param[in]	size	�ǂݍ��݃T�C�Y
//!	@param[in]	path	�ǂݍ��݃t�@�C���p�X
//!	
//!	@retval	0=����I��
//!			-1=�t�@�C���V�K�쐬���s
//!	
//!	@since	2016/09/06	Takano	�V�K�쐬
//!				
int CFileCommon::Read( VOID* result, int size, LPCTSTR path )
{
	assert( NULL != result );
	assert( 0< size );
	assert( NULL != path );
	assert( NULL != path[0] );

	FILE *fpr = NULL;
	while( NULL == fpr ) {
		fopen_s( &fpr, path, "rb" );
		if( NULL != fpr ) {
			break;
		}
		//	�t�@�C���I�[�v���Ɏ��s�����ꍇ�́A��̃t�@�C�����쐬����B
		ZeroMemory( result, size );
		if( 0 > Write( result, size, path ) ) {
			return -1;
		}
	}
	fread( result, size, 1, fpr );
	fclose( fpr );
	return 0;
}

//!				
//!	@brief	�t�@�C����������
//!				
//!	@details	���� path �Ŏw�肳�ꂽ�t�@�C�����w�肳�ꂽ�T�C�Y�������݂܂��B
//!				
//!	@param[out]	result	�������ޏ��
//!	@param[in]	size	�ǂݍ��݃T�C�Y
//!	@param[in]	path	�������݃t�@�C���p�X
//!	
//!	@retval	0=����I��
//!			-1=�t�@�C���V�K�쐬���s
//!	
//!	@since	2016/09/06	Takano	�V�K�쐬
//!				
int CFileCommon::Write( VOID* target, int size, LPCTSTR path )
{
	assert( NULL != target );
	assert( 0< size );
	assert( NULL != path );
	assert( NULL != path[0] );

	FILE *fpr = NULL;
	fopen_s( &fpr, path, "wb" );
	if( NULL == fpr ) {
		return -1;
	}
	fwrite( target, size, 1, fpr );
	fclose( fpr );
	return 0;
}

