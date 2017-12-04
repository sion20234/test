#pragma once
class CMcReceiveData
{
public:
	CMcReceiveData();
	~CMcReceiveData();

	int Status( LPVOID data, int size );
	int RomVersion( LPVOID data );
};

