#pragma once
#include "stdafx.h"

//!				
//!	@brief	�t�@�C���A�N�Z�X���ʃN���X
//!				
//!	@details	�t�@�C���A�N�Z�X�̋��ʕ��ł��B
//!				
//!	@since	2016/09/06	Takano	�V�K�쐬
//!				
class CFileCommon
{
public:
	CFileCommon();
	~CFileCommon();
	int Read( VOID* result, int size, LPCTSTR path );
	int Write( VOID* target, int size, LPCTSTR path );
};


template<class VALUE>class CFileVariable : CFileCommon
{
public:
	int Load()
	{
		return CFileCommon::Read( &_data, sizeof( VALUE ), _filePath );
	};
	int Save()
	{
		return CFileCommon::Write( &_data, sizeof( _data ), _filePath );
	};
	__declspec( property( get = GetEntity ) ) VALUE& Entity;
	VALUE& GetEntity()
	{
		return _data;
	};
	void Export( LPVOID target )
	{
		CopyMemory( target, &_data, sizeof( VALUE ) );
	};
	void Import( VALUE* source )
	{
		CopyMemory( &_data, source, sizeof( VALUE ) );
	}
protected:
	void SetPath( LPCTSTR target )
	{
		strcpy_s( _filePath, sizeof( _filePath ), target );
	}
	CHAR _filePath[MAX_PATH];
	VALUE _data;
};

//!				
//!	@brief	VIRPOS_EX.bin�t�@�C���A�N�Z�X
//!				
//!	@since	2016/09/06	Takano	�V�K�쐬
//!				
class CFileVirPos : public CFileVariable<VIRPOS_EX>
{
public:
	CFileVirPos()
	{
		SetPath( "Debug\\VIRPOS_EX.bin" );
	}
	int SetVirPos( VIRPOSCHG target )
	{
		int index = 0;
		for( index = 0; index < SIZE_OF_ARRAY( target.VirPos ); ++index ) {
			_data.AxPnt[target.VirNo].Pnt[index] = target.VirPos[index];
		}
		return 0;
	}
	int SetWorkOrg( WORGPOSCHG target )
	{
		int index = 0;
		for( index = 0; index < SIZE_OF_ARRAY( target.PosAxis ); ++index ) {
			if( target.AxisFlag & ( 0x0001 << index ) ) {
				_data.AxPnt[0].Pnt[index] = target.PosAxis[index];
			}
		}
		return 0;
	}

	int GetWorkOrg( WORKORG* result )
	{
		Load();
		int index = 0;
		for( index = 0; index < 9; ++index ) {
			result->WorkOrg[index] = _data.AxPnt[0].Pnt[index];
		}
		return 0;
	};
};

//!				
//!	@brief	PCOND.bin�t�@�C���A�N�Z�X
//!				
//!	@since	2016/09/06	Takano	�V�K�쐬
//!				
class CFilePCondtion : public CFileVariable<PCONDITION>
{
public:
	/// <summary>���݂̉��H���� �r�b�g��`</summary>
	enum States
	{
		/// <summary>�ԑ��v��</summary>
		RequestSendingBack = 0x0001,
		/// <summary>�h���C�����L��</summary>
		DryRun = 0x0004,
		/// <summary>�C�j�V�����Z�b�g�L��</summary>
		InitialSet = 0x0008,
		/// <summary>�d�ɏ��Վ��̎����d�Ɍ����L��</summary>
		AecByLife = 0x0080,
		/// <summary>�p�[�e�B�V�����������~�L��</summary>
		PartitionRoundStop = 0x0100,
		/// <summary>���d</summary>
		Discharge = 0x2000,
	};
	CFilePCondtion()
	{
		SetPath( "Debug\\PCOND.bin" );
	}
	bool SetFlag( States mask, bool state )
	{
		CAid aid;
		return aid.SetBit( _data.Status, mask, state, false );
	};
};

class CFilePCondtionTable : public CFileVariable<PCOND_TBL>
{
public:
	CFilePCondtionTable()
	{
		SetPath( "Debug\\PCOND_TBL.bin" );
	}
};

class CFileAecData : public CFileVariable<AECDATA>
{
public:
	CFileAecData()
	{
		SetPath( "Debug\\AECDATA.bin" );
	};
};

class CFileIOData : public CFileVariable<IODATA>
{
public:
	CFileIOData()
	{
		SetPath( "Debug\\IODATA.bin" );
	};

	//!				
	//!	@brief	�M���ݒ�
	//!				
	//!	@details	I�^O����ݒ肵�܂��B
	//!				
	//!	@param[in]	state	�ݒ�l
	//!	@param[in]	address	�A�h���X
	//!	@param[in]	mask	�}�X�N
	//!	
	//!	@retval	�߂�l		
	//!				
	//!	@since	2016/09/06	Takano	�V�K�쐬
	//!				
	int SetFlag( short address, int mask, short state )
	{
		int offset = address + IO_OFFSET;
		if( 0>offset ) {
			//	�s��
		} else if( INPUT_AREA_SIZE >= offset ) {
			//	����
			if( 0 != state ) {
				_data.Input[offset] |= mask;
			} else {
				_data.Input[offset] &= ~mask;
			}
		} else if( ( IO_AREA_SIZE ) >= offset ) {
			offset -= INPUT_AREA_SIZE;
			//	�o��
			if( 0 != state ) {
				_data.Output[offset] |= mask;
			} else {
				_data.Output[offset] &= ~mask;
			}
		} else {
			//	�s��
		}
		return 0;
	}
private:
	const int INPUT_AREA_SIZE = 116;
	const int OUTPUT_AREA_SIZE = 64;
	const int IO_AREA_SIZE = INPUT_AREA_SIZE + OUTPUT_AREA_SIZE;
	const int IO_OFFSET = -1600;
};

class CFileStatuses : public CFileVariable<STATUS>
{
public:
	enum AxisStatus
	{
		//	���_���A����
		CompOrgPos = 0x0008,
	};/// <summary>�^�X�N�X�e�[�^�X</summary>
	enum TaskStatus
	{
		/// <summary>�O�Ք�����(FG����)</summary>
		/// <remarks>�ȉ��Ɖ��߂��܂��B
		/// <list type="bullet" >
		///		<item>0=���ړ�������(���ړ���)</item>
		///		<item>1=���ړ�����</item>
		/// </list>
		/// </remarks>
		CompletedFg = 0x0002,
		/// <summary>�v���O�����^�]��</summary>
		InProgram = 0x0010,
		/// <summary>�e�B�[�`���O���[�h</summary>
		InTeachingMode = 0x0200,
		/// <summary>���[�h���</summary>
		MotionMode = 0xF000,
	};
	enum EcncStatus2
	{
		/// <summary>�I�v�V���i���X�g�b�v �����^�L��</summary>
		OptionalStop = 0x0001,
		/// <summary>�ڐG���m �����^�L��</summary>
		TouchSensor = 0x0002,
		/// <summary>���Α���_�ݒ莞���ړ� �����^�L��</summary>
		IncrimentalReferenceAxisMove = 0x0004,
		/// <summary>�e��V�[�P���X����</summary>
		CompletedSequence = 0x0010,
		/// <summary>XY�C���^�[���b�N�L��</summary>
		InterlockXY = 0x0020,
		/// <summary>�v���O�����J�n���p�x�␳�L��</summary>
		CorrectAngle = 0x0040,
		/// <summary>�u���b�N�X�L�b�v</summary>
		BlockSkip = 0x0100,
		/// <summary>�蓮�p���T�[����L��</summary>
		EnableHandPulser = 0x1000,
		/// <summary>���b�Z�[�W�\���v��</summary>
		RequestShowMessage = 0x4000,
	};
	/// <summary>ECNC STATUS3 �r�b�g��`</summary>
	enum EcncStatus3
	{
		/// <summary>�������[�h���o��</summary>
		AutoModeOutput = 0x0002,
		/// <summary>�V���b�g�_�E���v��</summary>
		RequestShutdown = 0x0010,
	};
	//enum EcncAlarm2// for STATUS.ecnc.Alarm2
	//{
	//	S_MCA2_GUID_INTRF = 0x0008,  // �K�C�h�z���_���G���[
	//	S_MCA2_PRCS_BUCKLING = 0x0040,   // �ʏ�d�ɕ��d���H�������G���[
	//	S_MCA2_Z20 = 0x0100, // �y�Q�O�G���[
	//	S_MCA2_REFOVER = 0x0200, // �[�ʈʒu�v�Z�͈͊O�G���[
	//	S_MCA2_IDX_POSITION = 0x0400,    // �C���f�b�N�X�ʒu���߃G���[
	//	S_MCA2_DISCHTIMEOUT = 0x0800,    // ���d���H�^�C���A�E�g�G���[
	//	S_MCA2_PUMPCOLLET_IL = 0x1000,   // �|���v�n�m�E�R���b�g�A���N�����v�����w�߃G���[
	//	S_MCA2_PRSKPVPOS_OVER = 0x2000,  // ���d���H�X�L�b�v���������z�_�o�^�ԍ��͈͊O�G���[
	//};

	CFileStatuses()
	{
		SetPath( "Debug\\STATUS.bin" );
	};

	bool IsTrue( TaskStatus mask, int taskNumber )
	{
		return ( _data.task[taskNumber].TaskStatus & mask ) ? true : false;
	}
	bool IsTrue( EcncStatus2 mask )
	{
		return ( _data.ecnc.Status2 & mask ) ? true : false;
	}
	bool IsTrue( EcncStatus3 mask )
	{
		return ( _data.ecnc.Status3 & mask ) ? true : false;
	}
	bool IsTrue( AxisStatus mask, int axisNumber )
	{
		return ( _data.ax[axisNumber].AxStatus & mask ) ? true : false;
	}

	bool SetFlag( TaskStatus mask, bool state, int taskNumber = 0, bool connectB = false )
	{
		CAid aid;
		return aid.SetBit( _data.task[taskNumber].TaskStatus, mask, state, connectB );
	}
	bool SetFlag( EcncStatus2 mask, bool state, bool connectB = false )
	{
		CAid aid;
		return aid.SetBit( _data.ecnc.Status2, mask, state, connectB );
	}
	bool SetFlag( EcncStatus3 mask, bool state, bool connectB = false )
	{
		CAid aid;
		return aid.SetBit( _data.ecnc.Status3, mask, state, connectB );
	}
	bool SetFlag( AxisStatus mask, bool state, int axisNumber, bool connectB = false )
	{
		CAid aid;
		return aid.SetBit( _data.ax[axisNumber].AxStatus, mask, state, connectB );
	}


	//	0		1			2			3
	//	0=MC	����		�}�X�N�l	OFF/ON
	//	1=AXIS	���ԍ�
	//	2=TASK	�^�X�N�ԍ�
	//	3=ECNC
	//			0=�A���[��2
	//			1=�A���[��3
	//			2=�A���[��4
	//			3=�A���[��5
	//			4=EtherCAT1
	//			5=EtherCAT2
	enum AlarmType
	{
		Mc,
		Axis,
		Task,
		Ecnc,
	};
	enum AlarmEcncCategory
	{
		Alarm2,
		Alarm3,
		Alarm4,
		Alarm5,
		EtherCat1,
		EtherCat2,
	};

	bool SetAlarm( AlarmType type, int category, int mask, bool value )
	{
		CAid aid;
		if( Mc == type ) {
			aid.SetBit( _data.mc.Alarm, mask, value, false );
		}else if( Axis == type ) {
			aid.SetBit( _data.ax[category].AxAlarm, mask, value, false );
		} else if( Task == type ) {
			aid.SetBit( _data.task[category].TaskAlarm, mask, value, false );
		} else if( Ecnc == type ) {
			if( Alarm2 == category ) {
				aid.SetBit( _data.ecnc.Alarm2, mask, value, false );
			} else if( Alarm3 == category ) {
				aid.SetBit( _data.ecnc.Alarm3, mask, value, false );
			} else if( Alarm4 == category ) {
				aid.SetBit( _data.ecnc.Alarm4, mask, value, false );
			} else if( Alarm5 == category ) {
				aid.SetBit( _data.ecnc.Alarm5, mask, value, false );
			} else if( EtherCat1 == category ) {
				aid.SetBit( _data.ecnc.EIFErr1, mask, value, false );
			} else if( EtherCat2 == category ) {
				aid.SetBit( _data.ecnc.EIFErr2, mask, value, false );
			}
		}
		return true;
	}


	//bool SetFlag( EcncAlarm2 mask, bool state, bool connectB = false )
	//{
	//	CAid aid;
	//	return aid.SetBit( _data.ecnc.Alarm2, mask, state, connectB );
	//}

	void SetPosOffset( int axisNumber, int offset, int upper )
	{
		if( 0 == upper ) {
			upper = 1;
		}
		_data.ax[axisNumber].PosReg = ( _data.ax[axisNumber].PosReg + offset ) % upper;
		offset += 10000;
		_data.ax[axisNumber].AbsReg = ( _data.ax[axisNumber].AbsReg + offset ) % upper;
	}
};
