#include "stdafx.h"
#include "FileCommon.h"
#include <string>	// useful for reading and writing
#include <fstream>	// ifstream, ofstream
#include <assert.h>

//!				
//!	@brief	コンストラクタ
//!				
//!	@since	2016/09/06	Takano	新規作成
//!				
CFileCommon::CFileCommon()
{
}

//!				
//!	@brief	デストラクタ
//!				
//!	@since	2016/09/06	Takano	新規作成
//!				
CFileCommon::~CFileCommon()
{
}

//!				
//!	@brief	ファイル読み込み
//!				
//!	@details	引数 path で指定されたファイルを指定されたサイズ読み込みます。
//!				
//!	@param[out]	result	読み取られた情報
//!	@param[in]	size	読み込みサイズ
//!	@param[in]	path	読み込みファイルパス
//!	
//!	@retval	0=正常終了
//!			-1=ファイル新規作成失敗
//!	
//!	@since	2016/09/06	Takano	新規作成
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
		//	ファイルオープンに失敗した場合は、空のファイルを作成する。
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
//!	@brief	ファイル書き込み
//!				
//!	@details	引数 path で指定されたファイルを指定されたサイズ書き込みます。
//!				
//!	@param[out]	result	書き込む情報
//!	@param[in]	size	読み込みサイズ
//!	@param[in]	path	書き込みファイルパス
//!	
//!	@retval	0=正常終了
//!			-1=ファイル新規作成失敗
//!	
//!	@since	2016/09/06	Takano	新規作成
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

