// stdafx.cpp : �W���C���N���[�h RT64ECComNTWrap.pch �݂̂�
// �܂ރ\�[�X �t�@�C���́A�v���R���p�C���ς݃w�b�_�[�ɂȂ�܂��B
// stdafx.obj �ɂ̓v���R���p�C���ς݌^��񂪊܂܂�܂��B

#include "stdafx.h"

// TODO: ���̃t�@�C���ł͂Ȃ��ASTDAFX.H �ŕK�v��
// �ǉ��w�b�_�[���Q�Ƃ��Ă��������B

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
