#include "stdafx.h"
#include "McSendCommand.h"
//#include "FileDatStatus.h"

//!				
//!	@brief	コンストラクタ
//!				
//!	@since	2016/09/06	Takano	新規作成
//!				
CMcSendCommand::CMcSendCommand()
{
}

//!				
//!	@brief	デストラクタ
//!				
//!	@since	2016/09/06	Takano	新規作成
//!				
CMcSendCommand::~CMcSendCommand()
{
}

//	AECDATA
int CMcSendCommand::ElectrodeNumberChange( LPVOID data )
{
	OUTPUTLOG( "REQ_ELCTDNOCHG" );
	CDynamicMemory<ELCTDNO> mem( data );
	CFileAecData file;
	file.Load();
	file.Entity.ElectrodeNo = mem.Entity.ElectrodeNo;
	return file.Save();
}
int CMcSendCommand::GuideNumberChange( LPVOID data )
{
	OUTPUTLOG( "REQ_GUIDENOCHG" );
	CDynamicMemory<GUIDENO> mem( data );
	CFileAecData file;
	file.Load();
	file.Entity.GuideNo = mem.Entity.GuideNo;
	return file.Save();
}
//	IO
int CMcSendCommand::StartButton( LPVOID data )
{
	OUTPUTLOG( "REQ_USER_STARTBUTTON" );
	CDynamicMemory<ENABLE> mem( data );
	CFileIOData fa;
	fa.Load();
	fa.SetFlag( 1652, 0x0020, (short)mem.Entity.enable );
	return fa.Save();
}
int CMcSendCommand::AutoModeOutput( LPVOID data )
{
	OUTPUTLOG( "REQ_AUTOMODE_OUTPUT" );
	AUTOMODE_OUTPUT param;
	ZeroMemory( &param, sizeof( param ) );
	CopyMemory( &param, data, sizeof( AUTOMODE_OUTPUT ) );
	CFileStatuses status;
	status.Load();
	status.SetFlag( CFileStatuses::AutoModeOutput, ( 0 != param.onflg ) ? true : false );
	return status.Save();
}
int CMcSendCommand::Buzzer( LPVOID data )
{
	OUTPUTLOG( "BUZZER_ON_OUTPUT" );
	CDynamicMemory<BUZZER_ON_OUTPUT> mem( data );
	CFileIOData fa;
	fa.Load();
	fa.SetFlag( 1727, 0x0020, (short)mem.Entity.onflg );
	return fa.Save();
}
int CMcSendCommand::ShutDownStart( LPVOID data )
{
	OUTPUTLOG( "REQ_SHUTDOWN_START" );
	SHUTDOWN_START param;
	ZeroMemory( &param, sizeof( param ) );
	CopyMemory( &param, data, sizeof( SHUTDOWN_START ) );
	CFileStatuses status;
	status.Load();
	status.SetFlag( CFileStatuses::RequestShutdown, ( 0 != param.startflg ) ? true : false );
	return status.Save();
}

//!				
//!	@brief	自動運転開始
//!				
//!	@details	自動運転が開始されたことを想定して必要な信号の設定を行います。	
//!				
//!	@retval	戻り値
//!				
//!	@note	備考		
//!				
//!	@since	2016/09/01	Takano	新規作成
//!	
int CMcSendCommand::OpStart()
{
	CFileStatuses status;
	status.Load();
	status.SetFlag( CFileStatuses::CompletedSequence, false );
	status.SetFlag( CFileStatuses::CompletedFg, false );
	return status.Save();
}

//!				
//!	@brief	リセットコマンド発行
//!				
//!	@details	自動運転が終了したと想定して必要な信号の設定を行います。
//!				
//!	@retval	戻り値
//!				
//!	@note	備考		
//!				
//!	@since	2016/09/01	Takano	新規作成
//!	
int CMcSendCommand::Reset()
{
	OUTPUTLOG( "REQ_RESET" );
	CFileStatuses status;
	status.Load();
	status.SetFlag( CFileStatuses::CompletedSequence, true );
	status.SetFlag( CFileStatuses::CompletedFg, true );
	return status.Save();
}
int CMcSendCommand::GuideClamp( LPVOID data )
{
	OUTPUTLOG( "REQ_GUIDECLUMP" );
	CDynamicMemory<GUIDECLUMP> mem( data );
	CFileIOData fa;
	fa.Load();
	fa.SetFlag( 1608, 0x0400, (short)mem.Entity.clump );
	return fa.Save();
}
int CMcSendCommand::SpindleClamp( LPVOID data )
{
	OUTPUTLOG( "REQ_SPCLUMP" );
	CDynamicMemory<SPCLUMP> mem( data );
	CFileIOData fa;
	fa.Load();
	fa.SetFlag( 1608, 0x0001, ( 0 != mem.Entity.clump ) ? 1 : 0 );
	fa.SetFlag( 1608, 0x0002, ( 0 != mem.Entity.clump ) ? 0 : 1 );
	return fa.Save();
}

//	PCONDITiON
int CMcSendCommand::ProcessConditinNumberSelect( LPVOID data )
{
	OUTPUTLOG( "REQ_PNOSEL" );
	CDynamicMemory<PRNOSEL> mem( data );
	CFilePCondtion file;
	file.Load();
	file.Entity.PNo = (short)mem.Entity.PNo;
	return file.Save();
}
int CMcSendCommand::SpinOut( LPVOID data )
{
	OUTPUTLOG( "REQ_SPCMND" );
	CDynamicMemory<SPCMND> mem( data );
	CFilePCondtion file;
	file.Load();
	file.Entity.SpinOut = mem.Entity.SpOut;
	return file.Save();
}
int CMcSendCommand::PumpOut( LPVOID data )
{
	OUTPUTLOG( "REQ_PUMPCMND" );
	CDynamicMemory<PUMPCMND> mem( data );
	CFilePCondtion file;
	file.Load();
	file.Entity.PumpOut = mem.Entity.PumpOut;
	return file.Save();
}
int CMcSendCommand::Discharge( LPVOID data )
{
	OUTPUTLOG( "REQ_BONCMND" );
	CDynamicMemory<BONCMND> mem( data );
	CFilePCondtion file;
	file.Load();
	file.SetFlag( CFilePCondtion::Discharge, ( 0 != mem.Entity.BonOut ) ? true : false );
	return file.Save();
}
int CMcSendCommand::DryRun( LPVOID data )
{
	OUTPUTLOG( "REQ_DRYRUN" );
	CDynamicMemory<ENABLE> mem( data );
	CFilePCondtion file;
	file.Load();
	file.SetFlag( CFilePCondtion::DryRun, ( 0 != mem.Entity.enable ) ? true : false );
	return file.Save();
}
int CMcSendCommand::DryRunEx( LPVOID data )
{
	OUTPUTLOG( "REQ_DRYRUN_EX" );
	CDynamicMemory<DRYRUN_EX> mem( data );
	CFilePCondtion file;
	file.Load();
	file.Entity.DryRunEnN = mem.Entity.DryRunEnN;
	file.SetFlag( CFilePCondtion::DryRun, ( 0 != mem.Entity.DryRunEnN ) ? true : false );
	return file.Save();
}

int CMcSendCommand::InitialSet( LPVOID data )
{
	OUTPUTLOG( "REQ_INITIALSET" );
	CDynamicMemory<ENABLE> mem( data );
	CFilePCondtion file;
	file.Load();
	file.SetFlag( CFilePCondtion::InitialSet, ( 0 != mem.Entity.enable ) ? true : false );
	return file.Save();
}
int CMcSendCommand::AecByLife( LPVOID data )
{
	OUTPUTLOG( "REQ_ELCTDCHGEN" );
	CDynamicMemory<ENABLE> mem( data );
	CFilePCondtion file;
	file.Load();
	file.SetFlag( CFilePCondtion::AecByLife, ( 0 != mem.Entity.enable ) ? true : false );
	return file.Save();
}
int CMcSendCommand::SendingBack( LPVOID data )
{
	CFilePCondtion file;
	file.Load();
	if( NULL != data ) {
		OUTPUTLOG( "REQ_USER_SENDBACK" );
		CDynamicMemory<ENABLE> mem( data );
		file.SetFlag( CFilePCondtion::RequestSendingBack, ( 0 != mem.Entity.enable ) ? true : false );
	} else {
		OUTPUTLOG( "REQ_RETURN" );
		file.SetFlag( CFilePCondtion::RequestSendingBack, false );
	}
	return file.Save();
}
int CMcSendCommand::ShowMessage( LPVOID data )
{
	if( NULL != data ) {
		OUTPUTLOG( "REQ_USER_MESSAGEREQ" );
		CDynamicMemory<ENABLE> mem( data );
		CFileStatuses file;
		file.Load();
		file.SetFlag( CFileStatuses::RequestShowMessage, ( 0 != mem.Entity.enable ) ? true : false );
		return file.Save();
	}
	return 0;
}
int CMcSendCommand::SetIOData( LPVOID data )
{
	if( NULL != data ) {
		OUTPUTLOG( "REQ_USER_IODATA" );
		CDynamicMemory<VIRPOSCHG> mem( data );
		CFileIOData fa;
		fa.Load();
		//	0=アドレス(1600～1751)
		//	1=マスク
		//	2=設定値
		fa.SetFlag( (short)mem.Entity.VirPos[0], (short)mem.Entity.VirPos[1], (short)mem.Entity.VirPos[2] );
		return fa.Save();
	}
	return 0;
}
int CMcSendCommand::EcncAlarm( LPVOID data )
{
	if( NULL != data ) {
		OUTPUTLOG( "REQ_USER_STATUS_ALARM" );
		CDynamicMemory<VIRPOSCHG> mem( data );
		CFileStatuses fa;
		fa.Load();
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
		fa.SetAlarm( ( CFileStatuses::AlarmType)mem.Entity.VirPos[0], mem.Entity.VirPos[1], mem.Entity.VirPos[2], 0 != mem.Entity.VirPos[3] );
		return fa.Save();
	}
	return 0;
}
//int CMcSendCommand::WritePConditionStatus( short state, int mask )
//{
//	CFileCommon file;
//	PCONDITION target;
//	ZeroMemory( &target, sizeof( target ) );
//	file.Read( &target, sizeof( target ), FILE_PCOND );
//	{
//		if( 0 != state ) {
//			target.Status |= mask;
//		} else {
//			target.Status &= ~( mask );
//		}
//	}
//	file.Write( &target, sizeof( target ), FILE_PCOND );
//	return 0;
//}
//	STATUS
int CMcSendCommand::ModeChange( LPVOID data )
{
	OUTPUTLOG( "REQ_MODECHG" );
	CDynamicMemory<MODECHG> mem( data );
	CFileStatuses file;
	file.Load();
	file.Entity.task[0].TaskStatus &= ~( 0x1000 | 0x2000 | 0x4000 | 0x8000 );
	file.Entity.task[0].TaskStatus |= ( mem.Entity.mode << 12 );
	return file.Save();
//	CFileCommon file;
//	STATUS target;
//	ZeroMemory( &target, sizeof( target ) );
//	file.Read( &target, sizeof( target ), FILE_STATUS );
//	{
//		MODECHG param;
//		ZeroMemory( &param, sizeof( param ) );
//		CopyMemory( &param, data, sizeof( MODECHG ) );
//		target.task[0].TaskStatus &= ~( 0x1000 | 0x2000 | 0x4000 | 0x8000 );
//		target.task[0].TaskStatus |= ( param.mode << 12 );
//	}
//	file.Write( &target, sizeof( target ), FILE_STATUS );
//	return 0;
}
int CMcSendCommand::OverrideChange( int dataType, int task, LPVOID data )
{
	CDynamicMemory<OVERCHG> mem( data );
	CFileStatuses file;
	file.Load();
	if( REQ_OVRDCHGP == dataType ) {
		OUTPUTLOG( "REQ_OVRDCHGP" );
		file.Entity.task[task].Override = mem.Entity.Override;
	} else if( REQ_COVRDCHGP == dataType ) {
		OUTPUTLOG( "REQ_COVRDCHGP" );
		file.Entity.task[task].COverride = mem.Entity.Override;
	} else if( REQ_SOVRDCHGP == dataType ) {
		OUTPUTLOG( "REQ_SOVRDCHGP" );
		file.Entity.task[task].SOverride = mem.Entity.Override;
	}
	return file.Save();

//	CFileCommon file;
//	STATUS target;
//	ZeroMemory( &target, sizeof( target ) );
//	file.Read( &target, sizeof( target ), FILE_STATUS );
//	{
//		OVERCHG param;
//		ZeroMemory( &param, sizeof( param ) );
//		CopyMemory( &param, data, sizeof( OVERCHG ) );
//
//		if( REQ_OVRDCHGP == dataType ) {
//			OUTPUTLOG( "REQ_OVRDCHGP" );
//			target.task[task].Override = param.Override;
//		} else if( REQ_COVRDCHGP == dataType ) {
//			OUTPUTLOG( "REQ_COVRDCHGP" );
//			target.task[task].COverride = param.Override;
//		} else if( REQ_SOVRDCHGP == dataType ) {
//			OUTPUTLOG( "REQ_SOVRDCHGP" );
//			target.task[task].SOverride = param.Override;
//		}
//	}
//	file.Write( &target, sizeof( target ), FILE_STATUS );
//	return 0;
}
int CMcSendCommand::OptionalStop( LPVOID data )
{
	OUTPUTLOG( "REQ_OPTSTOP" );
	return WriteStatusEcncStatus2( data, CFileStatuses::OptionalStop );
}
int CMcSendCommand::TouchSensor( LPVOID data )
{
	OUTPUTLOG( "REQ_TOUCHSENSE" );
	return WriteStatusEcncStatus2( data, CFileStatuses::TouchSensor );
}
int CMcSendCommand::IncrimentalReferenceAxisMove( LPVOID data )
{
	OUTPUTLOG( "REQ_INCREFSET_MOV" );
	return WriteStatusEcncStatus2( data, CFileStatuses::IncrimentalReferenceAxisMove );
}
int CMcSendCommand::InterlockXY( LPVOID data )
{
	OUTPUTLOG( "REQ_XY_ILOCK" );
	return WriteStatusEcncStatus2( data, CFileStatuses::InterlockXY );
}
int CMcSendCommand::HandPulserPermition( LPVOID data )
{
	OUTPUTLOG( "REQ_HANDLEPERMIT" );
	return WriteStatusEcncStatus2( data, CFileStatuses::EnableHandPulser );
}
int CMcSendCommand::CompletedSequence( LPVOID data )
{
	OUTPUTLOG( "REQ_USER_CMPLSEQ" );
	return WriteStatusEcncStatus2( data, CFileStatuses::CompletedSequence );
}
int CMcSendCommand::WAxisUpperLimit( LPVOID data )
{
	OUTPUTLOG( "REQ_WTOPPOS" );
	CDynamicMemory<WTOPPOS> mem( data );
	CFileStatuses file;
	file.Load();
	file.Entity.ecnc.WTopPos = mem.Entity.WTopPos;
	return file.Save();
//	CFileCommon file;
//	STATUS target;
//	ZeroMemory( &target, sizeof( target ) );
//	file.Read( &target, sizeof( target ), FILE_STATUS );
//	{
//		OUTPUTLOG( "REQ_WTOPPOS" );
//		WTOPPOS param;
//		ZeroMemory( &param, sizeof( param ) );
//		CopyMemory( &param, data, sizeof( WTOPPOS ) );
//		target.ecnc.WTopPos = param.WTopPos;
//	}
//	file.Write( &target, sizeof( target ), FILE_STATUS );
//	return 0;
}

//!				
//!	@brief	ECNCステータス設定		
//!				
//!	@details	ECNCステータス(STATUS.ecnc.Status2)のビット情報を設定します。
//!				
//!	@param[in]	data		設定値。ENABLE構造体であることを前提としています。
//!	@param[in]	mask		設定対象のマスク値。
//!	@param[in]	connectB	B接判定であるか。
//!				
//!	@retval	戻り値		
//!				
//!	@since	2016/09/01	Takano	新規作成
//!				
int CMcSendCommand::WriteStatusEcncStatus2( LPVOID data, int mask, bool connectB )
{
	CFileStatuses file;
	file.Load();
	ENABLE param;
	ZeroMemory( &param, sizeof( param ) );
	CopyMemory( &param, data, sizeof( ENABLE ) );
	file.SetFlag( ( CFileStatuses::EcncStatus2)mask, ( 0 != param.enable ) ? true : false );
	return file.Save();
}

//!				
//!	@brief	タスクステータス設定		
//!				
//!	@details	ECNCステータス(STATUS.task[?].Status)のビット情報を設定します。
//!				
//!	@param[in]	data	設定値。ENABLE構造体であることを前提としています。
//!	@param[in]	mask	設定対象のマスク値。
//!	@param[in]	task	タスク番号
//!				
//!	@retval	戻り値		
//!				
//!	@since	2016/09/01	Takano	新規作成
//!				
int CMcSendCommand::WriteStatusTaskStatus( LPVOID data, int mask, int task )
{
	CFileStatuses file;
	file.Load();
	ENABLE param;
	ZeroMemory( &param, sizeof( param ) );
	CopyMemory( &param, data, sizeof( ENABLE ) );
	file.SetFlag( ( CFileStatuses::TaskStatus )mask, ( 0 != param.enable ) ? true : false, task );
	return file.Save();
}
int CMcSendCommand::CompletedFg( LPVOID data )
{
	OUTPUTLOG( "REQ_USER_CMPLFG" );
	return WriteStatusTaskStatus( data, CFileStatuses::CompletedFg );
}

int CMcSendCommand::ForceReturnToOrigin( LPVOID data )
{
	OUTPUTLOG( "REQ_FORCEZRNFIN" );
	FORCEZRNFIN param;
	ZeroMemory( &param, sizeof( param ) );
	CopyMemory( &param, data, sizeof( FORCEZRNFIN ) );
	WriteStatusAxisStatus( ( 0x0001 == ( param.AxisFlag & 0x0001 ) ) ? 1 : 0, CFileStatuses::CompOrgPos, 0 );
	WriteStatusAxisStatus( ( 0x0002 == ( param.AxisFlag & 0x0002 ) ) ? 1 : 0, CFileStatuses::CompOrgPos, 1 );
	WriteStatusAxisStatus( ( 0x0004 == ( param.AxisFlag & 0x0004 ) ) ? 1 : 0, CFileStatuses::CompOrgPos, 2 );
	WriteStatusAxisStatus( ( 0x0008 == ( param.AxisFlag & 0x0008 ) ) ? 1 : 0, CFileStatuses::CompOrgPos, 3 );
	WriteStatusAxisStatus( ( 0x0010 == ( param.AxisFlag & 0x0010 ) ) ? 1 : 0, CFileStatuses::CompOrgPos, 4 );
	WriteStatusAxisStatus( ( 0x0020 == ( param.AxisFlag & 0x0020 ) ) ? 1 : 0, CFileStatuses::CompOrgPos, 5 );
	WriteStatusAxisStatus( ( 0x0040 == ( param.AxisFlag & 0x0040 ) ) ? 1 : 0, CFileStatuses::CompOrgPos, 6 );
	WriteStatusAxisStatus( ( 0x0080 == ( param.AxisFlag & 0x0080 ) ) ? 1 : 0, CFileStatuses::CompOrgPos, 7 );
	return 0;
}

int CMcSendCommand::WriteStatusAxisStatus( short state, int mask, int axis )
{
	CFileStatuses file;
	file.Load();
	file.SetFlag( ( CFileStatuses::AxisStatus )mask, ( 0 != state ) ? true : false, axis );
	return file.Save();
}
