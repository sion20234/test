#pragma once
#include "stdafx.h"

//!				
//!	@brief	ファイルアクセス共通クラス
//!				
//!	@details	ファイルアクセスの共通部です。
//!				
//!	@since	2016/09/06	Takano	新規作成
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
//!	@brief	VIRPOS_EX.binファイルアクセス
//!				
//!	@since	2016/09/06	Takano	新規作成
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
//!	@brief	PCOND.binファイルアクセス
//!				
//!	@since	2016/09/06	Takano	新規作成
//!				
class CFilePCondtion : public CFileVariable<PCONDITION>
{
public:
	/// <summary>現在の加工条件 ビット定義</summary>
	enum States
	{
		/// <summary>返送要求</summary>
		RequestSendingBack = 0x0001,
		/// <summary>ドライラン有効</summary>
		DryRun = 0x0004,
		/// <summary>イニシャルセット有効</summary>
		InitialSet = 0x0008,
		/// <summary>電極消耗時の自動電極交換有効</summary>
		AecByLife = 0x0080,
		/// <summary>パーティション内一周停止有効</summary>
		PartitionRoundStop = 0x0100,
		/// <summary>放電</summary>
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
	//!	@brief	信号設定
	//!				
	//!	@details	I／O情報を設定します。
	//!				
	//!	@param[in]	state	設定値
	//!	@param[in]	address	アドレス
	//!	@param[in]	mask	マスク
	//!	
	//!	@retval	戻り値		
	//!				
	//!	@since	2016/09/06	Takano	新規作成
	//!				
	int SetFlag( short address, int mask, short state )
	{
		int offset = address + IO_OFFSET;
		if( 0>offset ) {
			//	不正
		} else if( INPUT_AREA_SIZE >= offset ) {
			//	入力
			if( 0 != state ) {
				_data.Input[offset] |= mask;
			} else {
				_data.Input[offset] &= ~mask;
			}
		} else if( ( IO_AREA_SIZE ) >= offset ) {
			offset -= INPUT_AREA_SIZE;
			//	出力
			if( 0 != state ) {
				_data.Output[offset] |= mask;
			} else {
				_data.Output[offset] &= ~mask;
			}
		} else {
			//	不正
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
		//	原点復帰完了
		CompOrgPos = 0x0008,
	};/// <summary>タスクステータス</summary>
	enum TaskStatus
	{
		/// <summary>軌跡発生完(FG完了)</summary>
		/// <remarks>以下と解釈します。
		/// <list type="bullet" >
		///		<item>0=軸移動未完了(軸移動中)</item>
		///		<item>1=軸移動完了</item>
		/// </list>
		/// </remarks>
		CompletedFg = 0x0002,
		/// <summary>プログラム運転中</summary>
		InProgram = 0x0010,
		/// <summary>ティーチングモード</summary>
		InTeachingMode = 0x0200,
		/// <summary>モード情報</summary>
		MotionMode = 0xF000,
	};
	enum EcncStatus2
	{
		/// <summary>オプショナルストップ 無効／有効</summary>
		OptionalStop = 0x0001,
		/// <summary>接触感知 無効／有効</summary>
		TouchSensor = 0x0002,
		/// <summary>相対測定点設定時軸移動 無効／有効</summary>
		IncrimentalReferenceAxisMove = 0x0004,
		/// <summary>各種シーケンス完了</summary>
		CompletedSequence = 0x0010,
		/// <summary>XYインターロック有効</summary>
		InterlockXY = 0x0020,
		/// <summary>プログラム開始時角度補正有効</summary>
		CorrectAngle = 0x0040,
		/// <summary>ブロックスキップ</summary>
		BlockSkip = 0x0100,
		/// <summary>手動パルサー動作有効</summary>
		EnableHandPulser = 0x1000,
		/// <summary>メッセージ表示要求</summary>
		RequestShowMessage = 0x4000,
	};
	/// <summary>ECNC STATUS3 ビット定義</summary>
	enum EcncStatus3
	{
		/// <summary>自動モード中出力</summary>
		AutoModeOutput = 0x0002,
		/// <summary>シャットダウン要求</summary>
		RequestShutdown = 0x0010,
	};
	//enum EcncAlarm2// for STATUS.ecnc.Alarm2
	//{
	//	S_MCA2_GUID_INTRF = 0x0008,  // ガイドホルダ干渉エラー
	//	S_MCA2_PRCS_BUCKLING = 0x0040,   // 通常電極放電加工時座屈エラー
	//	S_MCA2_Z20 = 0x0100, // Ｚ２０エラー
	//	S_MCA2_REFOVER = 0x0200, // 端面位置計算範囲外エラー
	//	S_MCA2_IDX_POSITION = 0x0400,    // インデックス位置決めエラー
	//	S_MCA2_DISCHTIMEOUT = 0x0800,    // 放電加工タイムアウトエラー
	//	S_MCA2_PUMPCOLLET_IL = 0x1000,   // ポンプＯＮ・コレットアンクランプ同時指令エラー
	//	S_MCA2_PRSKPVPOS_OVER = 0x2000,  // 放電加工スキップ発生時仮想点登録番号範囲外エラー
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
	//	0=MC	無効		マスク値	OFF/ON
	//	1=AXIS	軸番号
	//	2=TASK	タスク番号
	//	3=ECNC
	//			0=アラーム2
	//			1=アラーム3
	//			2=アラーム4
	//			3=アラーム5
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
