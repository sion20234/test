// stdafx.cpp : 標準インクルード RT64ECComNTWrap.pch のみを
// 含むソース ファイルは、プリコンパイル済みヘッダーになります。
// stdafx.obj にはプリコンパイル済み型情報が含まれます。

#include "stdafx.h"

// TODO: このファイルではなく、STDAFX.H で必要な
// 追加ヘッダーを参照してください。

bool CAid::SetBit( USHORT& target, USHORT mask, bool state, bool connectB )
{
	if( false == connectB ) {
		if( 0 != state ) {
			target |= mask;
		} else {
			target &= ~( mask );
		}
	} else {
		if( 0 != state ) {
			target &= ~( mask );
		} else {
			target |= mask;
		}
	}
	return true;
}
bool CAid::SetBit( SHORT& target, SHORT mask, bool state, bool connectB )
{
	if( false == connectB ) {
		if( 0 != state ) {
			target |= mask;
		} else {
			target &= ~( mask );
		}
	} else {
		if( 0 != state ) {
			target &= ~( mask );
		} else {
			target |= mask;
		}
	}
	return true;
}

bool CAid::SetBit( LONG& target, LONG mask, bool state, bool connectB )
{
	if( false == connectB ) {
		if( 0 != state ) {
			target |= mask;
		} else {
			target &= ~( mask );
		}
	} else {
		if( 0 != state ) {
			target &= ~( mask );
		} else {
			target |= mask;
		}
	}
	return true;
}

bool CAid::SetBit( ULONG& target, ULONG mask, bool state, bool connectB )
{
	if( false == connectB ) {
		if( 0 != state ) {
			target |= mask;
		} else {
			target &= ~( mask );
		}
	} else {
		if( 0 != state ) {
			target &= ~( mask );
		} else {
			target |= mask;
		}
	}
	return true;
}
