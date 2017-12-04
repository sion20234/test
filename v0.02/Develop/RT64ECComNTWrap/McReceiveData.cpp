#include "stdafx.h"
#include "McReceiveData.h"
//#include "FileDatStatus.h"
//#include <stdio.h>
#include <stdlib.h>
#include <time.h>

CMcReceiveData::CMcReceiveData()
{
}


CMcReceiveData::~CMcReceiveData()
{
}

int CMcReceiveData::Status( LPVOID data, int size )
{
	CFileStatuses file;
	file.Load();
	if( false == file.IsTrue( CFileStatuses::CompletedSequence ) ) {
		//	シークエンス完了がOFFである場合、軸情報を更新する。
		file.SetPosOffset( 0, 528, 300000 );	//	X
		file.SetPosOffset( 1, 369, 200000 );	//	Y
		file.SetPosOffset( 2, 123, file.Entity.ecnc.WTopPos );	//	W
		file.SetPosOffset( 3, 357, 240000 );	//	Z
		file.SetPosOffset( 4, 316, 360000 );	//	A
		file.SetPosOffset( 5, 224, 180000 );	//	B
		file.SetPosOffset( 6, 119, 100000 );	//	C
		file.SetPosOffset( 7, 1234, 10000000 );	//	I
	}
	file.Save();
	file.Export( data );
	return 0;
}

int CMcReceiveData::RomVersion( LPVOID data )
{
	ROMVERSION target;
	ZeroMemory( &target, sizeof( target ) );
	LPCTSTR AppName = "ROMVERSION";
	GetPrivateProfileString( AppName, "Version", "ROM VERSION", target.Version, sizeof( target.Version ), FILE_INI );
	target.EvenSum = GetPrivateProfileInt( AppName, "EvenSum", 101, FILE_INI );
	target.OddSum = GetPrivateProfileInt( AppName, "OddSum", 102, FILE_INI );
	target.FlashSum = GetPrivateProfileInt( AppName, "FlashSum", 103, FILE_INI );
	target.FlashFlg = GetPrivateProfileInt( AppName, "FlashFlg", 104, FILE_INI );
	target.KindID = GetPrivateProfileInt( AppName, "KindID", 105, FILE_INI );
	target.SerialID = GetPrivateProfileInt( AppName, "SerialID", 106, FILE_INI );
	target.ProductID = GetPrivateProfileInt( AppName, "ProductID", 107, FILE_INI );
	CopyMemory( data, &target, sizeof( ROMVERSION ) );
	return 0;
}