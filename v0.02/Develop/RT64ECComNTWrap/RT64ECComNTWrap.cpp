// RT64ECComNTWrap.cpp : DLL アプリケーション用にエクスポートされる関数を定義します。
//
#include "stdafx.h"
#include "RT64ECComNTWrap.h"
#include "McSendCommand.h"
#include "McReceiveData.h"
//#include "FileDatStatus.h"
#include <string>	// useful for reading and writing
#include <fstream>	// ifstream, ofstream



//!
//!	@brief	接続
//!
//!	@details	モーション部とのインターフェースを開始します。
//!	
//!	@param[in]	psxdef	初期化情報
//!	@param[out]	phCom	通信ハンドル
//!
//!	@retval	0=正常終了(子ノードがない場合を含む)
//!
//!	@note	
//!	
//!	@since	2016/07/27	Takano	新規作成
//!
extern "C" __declspec ( dllexport )int _stdcall InitCommProc( PSXDEF psxdef, int *phCom )
{
	char buf[MAX_PATH];
	ZeroMemory( buf, sizeof( buf ) );
	wsprintf( buf, ">>>> START! InitCommProcWrap(nSize=%d/nCpuNo=%d/fLogging=%04x/pLogFile=[%s])\n", psxdef->nSize, psxdef->pNodeName, psxdef->fLogging, psxdef->pLogFile );
	OUTPUTLOG( buf );

	if( sizeof( SXDEF ) != psxdef->nSize ) {
		OUTPUTLOG( "ERROR! SendDataWrap SIZE UNMATCH\n" );
		return -1;
	}
	OUTPUTLOG( "<<<< END! InitCommProcWrap\n" );
	*phCom = 201;
#if __KEY_FILE_CREATE__
	FILE *fpw = NULL;
	{
		INITIALPRM paramIpm;
		ZeroMemory( &paramIpm, sizeof( paramIpm ) );
		fopen_s( &fpw, FILE_MC_INITIALPRM, "wb" );
		fwrite( &paramIpm, sizeof( paramIpm ), 1, fpw );
		fclose( fpw );
	}
	fpw = NULL;
	{
		ROMSW paramRom;
		ZeroMemory( &paramRom, sizeof( paramRom ) );
		fopen_s( &fpw, FILE_MC_ROMSW, "wb" );
		fwrite( &paramRom, sizeof( paramRom ), 1, fpw );
		fclose( fpw );
	}
	fpw = NULL;
	{
		PARAMETER_DATA paramPrm;
		ZeroMemory( &paramPrm, sizeof( paramPrm ) );
		fopen_s( &fpw, FILE_MC_PARAMETER, "wb" );
		fwrite( &paramPrm, sizeof( paramPrm ), 1, fpw );
		fclose( fpw );
	}
	fpw = NULL;
	{
		PITCH_ERR_REV paramPit;
		ZeroMemory( &paramPit, sizeof( paramPit ) );
		fopen_s( &fpw, FILE_MC_PITCH, "wb" );
		fwrite( &paramPit, sizeof( paramPit ), 1, fpw );
		fclose( fpw );
	}
	fpw = NULL;
#endif
	return 0;
}

//!
//!	@brief	切断
//!
//!	@details	モーション部とのインターフェースを終了します。
//!	
//!	@param[out]	hCom	通信ハンドル
//!
//!	@retval	0=正常終了(子ノードがない場合を含む)
//!
//!	@note	
//!	
//!	@since	2016/07/27	Takano	新規作成
//!
extern "C" __declspec ( dllexport )int _stdcall QuitCommProc( int hCom )
{
	char buf[MAX_PATH];
	ZeroMemory( buf, sizeof( buf ) );
	wsprintf( buf, ">>>> START! QuitCommProcWrap(hCom=%d)\n", hCom );
	OUTPUTLOG( buf );

	OUTPUTLOG( "<<<< END! QuitCommProcWrap\n" );
	return 0;
}

//!
//!	@brief	データ送信
//!
//!	@details	モーション部へのデータの送信を行います。
//!	
//!	@param[out]	hCom	通信ハンドル。
//!						通信初期化処理で取得したハンドルを指定します。
//!	@param[out]	type	データタイプ。
//!						DAT_PARAMETER等、送信するデータのタイプを指定します。
//!	@param[out]	task	タスク番号。
//!						設定対象のタスク番号(0～7)を指定します。タスク指定が関係ない設定の場合は0を指定して下さい。
//!	@param[out]	prm		付加パラメータ。
//!						データタイプによって、指定する値が異なります。
//!						送受信データ説明書の各データタイプの説明を参照して設定下さい。
//!	@param[out]	size	送信データサイズ
//!	@param[out]	data	送信データ格納バッファへのポインタ
//!
//!	@retval	0=正常終了(子ノードがない場合を含む)
//!			-1=構造体サイズ不一致
//!
//!	@note	
//!	
//!	@since	2016/07/27	Takano	新規作成
//!
extern "C" __declspec ( dllexport )int _stdcall SendData( int hCom, short type, short task, short prm, DWORD size, LPVOID data )
{
	char buf[MAX_PATH];
	ZeroMemory( buf, sizeof( buf ) );
	wsprintf( buf, ">>>> START! SendDataWrap(hCom=%d/type=0x%02x/task=%d/prm=%d/size=%d)\n", hCom, type, task, prm, size );
	OUTPUTLOG( buf );
	CFileCommon file;
	if( DAT_INITIALPRM == type ) {
		CHECK_SIZE( sizeof( INITIALPRM ), size );
		file.Write( data, sizeof( INITIALPRM ), FILE_MC_INITIALPRM );
	}else if( DAT_ROMSWITCH == type ) {
		CHECK_SIZE( sizeof( ROMSW ), size );
		file.Write( data, sizeof( ROMSW ), FILE_MC_ROMSW );
	} else if( DAT_PARAMETER == type ) {
		CHECK_SIZE( sizeof( PARAMETER_DATA ), size );
		file.Write( data, sizeof( PARAMETER_DATA ), FILE_MC_PARAMETER );
	} else if( DAT_PITCHERR == type ) {
		CHECK_SIZE( sizeof( PITCH_ERR_REV ), size );
		file.Write( data, sizeof( PITCH_ERR_REV ), FILE_MC_PITCH );
	} else if( DAT_STATUS == type ) {
		CHECK_SIZE( sizeof( STATUS ), size );
		CFileStatuses fa;
		fa.Load();
		fa.Import( (STATUS*)data );
		fa.Save();
//		file.Write( data, sizeof( STATUS ), FILE_STATUS );
	} else if( DAT_AECDATA == type ) {
		CHECK_SIZE( sizeof( AECDATA ), size );
		CFileAecData fa;
		fa.Import( (AECDATA*)data );
		fa.Save();
	} else if( DAT_PCONDITION == type ) {
		CHECK_SIZE( sizeof( PCONDITION ), size );
		CFilePCondtion fa;
		fa.Import( (PCONDITION*)data );
		fa.Save();
	} else if( DAT_MESSAGEANS == type ) {
		CHECK_SIZE( sizeof( MESSAGEANS ), size );
		MESSAGEREQ req;
		ZeroMemory( &req, sizeof( MESSAGEREQ ) );
		file.Read( &req, sizeof( MESSAGEREQ ), FILE_MESSAGEREQ );
		if( 0 != memcmp( &( (MESSAGEANS*)data )->Req, &req, sizeof( MESSAGEREQ ) ) ) {
			return -2;
		}
		CFileStatuses file;
		file.Load();
		file.SetFlag( CFileStatuses::RequestShowMessage, false );
		file.Save();
	} else if( DAT_PROGRAM == type ) {
		;
	} else if( DAT_PCOND_TABLE == type ) {
		CHECK_SIZE( sizeof( PCOND_TBL ), size );
		CFilePCondtionTable fa;
		fa.Import( (PCOND_TBL*)data );
		fa.Save();
//		file.Write( data, sizeof( PCOND_TBL ), FILE_PCOND_TBL );
	} else {
		return -1;
	}
	OUTPUTLOG( "<<<< END! SendDataWrap\n" );
	return 0;
}

//!
//!	@brief	データ受信
//!
//!	@details	モーション部よりデータを受信します。
//!	
//!	@param[out]	hCom	通信ハンドル。
//!						通信初期化処理で取得したハンドルを指定します。
//!	@param[out]	type	データタイプ。
//!						DAT_PARAMETER等、送信するデータのタイプを指定します。
//!	@param[out]	task	タスク番号。
//!						設定対象のタスク番号(0～7)を指定します。タスク指定が関係ない設定の場合は0を指定して下さい。
//!	@param[out]	prm		付加パラメータ。
//!						データタイプによって、指定する値が異なります。
//!						送受信データ説明書の各データタイプの説明を参照して設定下さい。
//!	@param[out]	size	受信データサイズ
//!	@param[out]	data	受信データ格納バッファへのポインタ
//!
//!	@retval	0=正常終了(子ノードがない場合を含む)
//!
//!	@note	
//!	
//!	@since	2016/07/27	Takano	新規作成
//!
extern "C" __declspec ( dllexport )int _stdcall ReceiveData( int hCom, short type, short task, short prm, LPDWORD size, LPVOID data )
{
	CMcReceiveData rcv;
	CFileCommon file;
	if( DAT_INITIALPRM == type ) {
		CHECK_SIZE( sizeof( INITIALPRM ), *size );
		file.Read( data, sizeof( INITIALPRM ), FILE_MC_INITIALPRM );
	} else if( DAT_ROMSWITCH == type ) {
		CHECK_SIZE( sizeof( ROMSW ), *size );
		file.Read( data, sizeof( ROMSW ), FILE_MC_ROMSW );
	} else if( DAT_PARAMETER == type ) {
		CHECK_SIZE( sizeof( PARAMETER_DATA ), *size );
		file.Read( data, sizeof( PARAMETER_DATA ), FILE_MC_PARAMETER );
	} else if( DAT_PITCHERR == type ) {
		CHECK_SIZE( sizeof( PITCH_ERR_REV ), *size );
		file.Read( data, sizeof( PITCH_ERR_REV ), FILE_MC_PITCH );
	} else if( DAT_IODATA == type ) {
		CHECK_SIZE( sizeof( IODATA ), *size );
		CDynamicMemory<GUIDECLUMP> mem( data );
		CFileIOData fa;
		fa.Load();
		fa.Export( data );
	} else if( DAT_STATUS == type ) {
		rcv.Status( data, *size );
	} else if( DAT_AECDATA == type ) {
		CHECK_SIZE( sizeof( AECDATA ), *size );
		CFileAecData fa;
		fa.Load();
		fa.Export( data );
	} else if( DAT_PCONDITION == type ) {
		CHECK_SIZE( sizeof( PCONDITION ), *size );
		CFilePCondtion fa;
		fa.Load();
		fa.Export( data );
	} else if( DAT_HANDLESTS == type ) {
		CHECK_SIZE( sizeof( HANDLESTS ), *size );
		file.Read( data, sizeof( HANDLESTS ), FILE_HANDLESTS );
	} else if( DAT_PCOND_TABLE == type ) {
		CHECK_SIZE( sizeof( PCOND_TBL ), *size );
		CFilePCondtionTable fa;
		fa.Load();
		fa.Export( data );
		//		file.Read( data, sizeof( PCOND_TBL ), FILE_PCOND_TBL );
	} else if( DAT_FORCEIO == type ) {
		;

	//	再確認OK

	} else if( DAT_MESSAGEREQ == type ) {
		CHECK_SIZE( sizeof( MESSAGEREQ ), *size );
		LPCTSTR AppName = "MESSAGEREQ";
		( (MESSAGEREQ*)data )->LineFlg = ( GetPrivateProfileInt( AppName, "LineFlgType", 0, FILE_INI ) << 8 )
			| GetPrivateProfileInt( AppName, "LineFlgNum", 0, FILE_INI );
		( (MESSAGEREQ*)data )->MessageNo = GetPrivateProfileInt( AppName, "MessageNo", 1001, FILE_INI );
		file.Write( data, sizeof( MESSAGEREQ ), FILE_MESSAGEREQ );
		file.Read( data, sizeof( MESSAGEREQ ), FILE_MESSAGEREQ );
	} else if( DAT_VERSION == type ) {
		CHECK_SIZE( sizeof( ROMVERSION ), *size );
		rcv.RomVersion( data );
	} else if( DAT_VIRPOS_EX == type ) {
		CHECK_SIZE( sizeof( VIRPOS_EX ), *size );
		CFileVirPos fa;
		fa.Load();
		fa.Export( data );
	} else if( DAT_WORKORG == type ) {
		CHECK_SIZE( sizeof( WORKORG ), *size );
		CFileVirPos fa;
		fa.GetWorkOrg( (WORKORG*)data );
	} else {
		return -1;
	}
	return 0;
}

//!
//!	@brief	コマンド送信
//!
//!	@details	モーション部へコマンドを送信します。
//!	
//!	@param[out]	hCom	通信ハンドル。
//!						通信初期化処理で取得したハンドルを指定します。
//!	@param[out]	cmnd	コマンドタイプ。
//!						REQ_ALLZRN等、コマンドのタイプを指定します。
//!	@param[out]	task	タスク番号。
//!						設定対象のタスク番号(0～7)を指定します。タスク指定が関係ない設定の場合は0を指定して下さい。
//!	@param[out]	data	コマンドデータ格納バッファへのポインタ
//!
//!	@retval	0=正常終了(子ノードがない場合を含む)
//!
//!	@note	
//!	
//!	@since	2016/07/27	Takano	新規作成
//!
extern "C" __declspec ( dllexport )int _stdcall SendCommand( int hCom, short cmnd, short task, LPVOID data )
{
	CMcSendCommand send;
	char buf[MAX_PATH];
	ZeroMemory( buf, sizeof( buf ) );
	wsprintf( buf, "START! SendCommandWrap(hCom=%d/cmnd=0x%04x)", hCom, cmnd );
	OUTPUTLOG( buf );

	int ret = 0;
	CFileCommon file;
	//	0x4000以降は、デバッグ用ユーザ定義
	if( 0x4000 < cmnd ) {
		if( 0x4001 == cmnd ) {
			send.StartButton( data );
		} else if( 0x4002 == cmnd ) {
			send.CompletedSequence( data );
		} else if( 0x4003 == cmnd ) {
			send.CompletedFg( data );
		} else if( 0x4005 == cmnd ) {
			send.SendingBack( data );
		} else if( 0x4006 == cmnd ) {
			send.ShowMessage( data );
		} else if( 0x4007 == cmnd ) {
			send.SetIOData( data );
		} else if( 0x4008 == cmnd ) {
			send.EcncAlarm( data );
		}
	} else {
		if( REQ_RESET == cmnd ) {
			send.Reset();
		} else if( REQ_RETURN == cmnd ) {
			send.SendingBack( NULL );
		} else if( REQ_ALLZRN == cmnd ) {
			OUTPUTLOG( "REQ_ALLZRN" );
			send.OpStart();
		} else if( REQ_PARTITIONCHG == cmnd ) {
			OUTPUTLOG( "REQ_PARTITIONCHG" );
		} else if( REQ_PROGSTRT == cmnd ) {
			OUTPUTLOG( "REQ_PROGSTRT" );
			send.OpStart();
		} else if( REQ_PROGSTRTN == cmnd ) {
			OUTPUTLOG( "REQ_PROGSTRTN" );
			send.OpStart();
		} else if( REQ_PTPASTART == cmnd ) {
			OUTPUTLOG( "REQ_PTPASTART" );
			send.OpStart();
		} else if( REQ_PTPBSTART == cmnd ) {
			OUTPUTLOG( "REQ_PTPBSTART" );
			send.OpStart();
		} else if( REQ_LINASTART == cmnd ) {
			OUTPUTLOG( "REQ_LINASTART" );
			send.OpStart();
		} else if( REQ_LINASTART == cmnd ) {
			OUTPUTLOG( "REQ_LINASTART" );
			send.OpStart();
		} else if( REQ_PTPBSTART_W == cmnd ) {
			OUTPUTLOG( "REQ_PTPBSTART_W" );
			send.OpStart();
		} else if( REQ_DRYRUN_EX == cmnd ) {
			OUTPUTLOG( "REQ_DRYRUN_EX" );

		} else if( REQ_PATROUNDSTOP == cmnd ) {
			OUTPUTLOG( "REQ_PATROUNDSTOP" );
			CDynamicMemory<ENABLE> mem( data );
			CFilePCondtion file;
			file.Load();
			file.SetFlag( CFilePCondtion::PartitionRoundStop, ( 0 != mem.Entity.enable ) ? true : false );
			file.Save();
		} else if( REQ_CORR_ANG == cmnd ) {
			OUTPUTLOG( "REQ_CORR_ANG" );
			CDynamicMemory<ENABLE> mem( data );
			CFileStatuses file;
			file.Load();
			file.SetFlag( CFileStatuses::CorrectAngle, ( 0 != mem.Entity.enable ) ? true : false );
			file.Save();
		} else if( REQ_BLOCKSKIP == cmnd ) {
			OUTPUTLOG( "REQ_BLOCKSKIP" );
			CDynamicMemory<ENABLE> mem( data );
			CFileStatuses file;
			file.Load();
			file.SetFlag( CFileStatuses::BlockSkip, ( 0 != mem.Entity.enable ) ? true : false );
//			file.SetFlag( CFileStatuses::S_MCA2_GUID_INTRF, ( 0 != mem.Entity.enable ) ? true : false );
			file.Save();
		} else if( REQ_AUTOMODE_OUTPUT == cmnd ) {
			send.AutoModeOutput( data );
		} else if( REQ_BUZZER_ON_OUTPUT == cmnd ) {
			send.Buzzer( data );
		} else if( REQ_SHUTDOWN_START == cmnd ) {
			send.ShutDownStart( data );
		} else if( REQ_PTPASTART_W == cmnd ) {
			send.OpStart();
		} else if( REQ_FORCEZRNFIN == cmnd ) {
			send.ForceReturnToOrigin( data );
		} else if( REQ_WTOPPOS == cmnd ) {
			send.WAxisUpperLimit( data );
		} else if( REQ_OPTSTOP == cmnd ) {
			send.OptionalStop( data );
		} else if( REQ_INCREFSET_MOV == cmnd ) {
			send.IncrimentalReferenceAxisMove( data );
		} else if( REQ_TOUCHSENSE == cmnd ) {
			send.TouchSensor( data );
		} else if( REQ_XY_ILOCK == cmnd ) {
			send.InterlockXY( data );
		} else if( REQ_ELCTDCHGEN == cmnd ) {
			send.AecByLife( data );
		} else if( REQ_INITIALSET == cmnd ) {
			send.InitialSet( data );
		} else if( REQ_DRYRUN == cmnd ) {
			send.DryRun( data );
		} else if( REQ_PNOSEL == cmnd ) {
			send.ProcessConditinNumberSelect( data );
		} else if( REQ_GUIDECLUMP == cmnd ) {
			send.GuideClamp( data );
		} else if( REQ_SPCLUMP == cmnd ) {
			send.SpindleClamp( data );
		} else if( REQ_HANDLEPERMIT == cmnd ) {
			send.HandPulserPermition( data );
		} else if( REQ_GUIDENOCHG == cmnd ) {
			send.GuideNumberChange( data );
		} else if( REQ_ELCTDNOCHG == cmnd ) {
			send.ElectrodeNumberChange( data );
		} else if( REQ_SPCMND == cmnd ) {
			send.SpinOut( data );
		} else if( REQ_PUMPCMND == cmnd ) {
			send.PumpOut( data );
		} else if( REQ_BONCMND == cmnd ) {
			send.Discharge( data );
		} else if( REQ_VIRPOSCHG_EX == cmnd ) {
			OUTPUTLOG( "REQ_VIRPOSCHG_EX" );
			CDynamicMemory<VIRPOSCHG> mem( data );
			CFileVirPos file;
			file.Load();
			file.SetVirPos( mem.Entity );
			file.Save();
		} else if( REQ_WORGPOSCHG == cmnd ) {
			OUTPUTLOG( "REQ_WORGPOSCHG" );
			CDynamicMemory<WORGPOSCHG> mem( data );
			CFileVirPos file;
			file.Load();
			file.SetWorkOrg( mem.Entity );
			file.Save();
		} else if( ( REQ_OVRDCHGP == cmnd ) || ( REQ_COVRDCHGP == cmnd ) || ( REQ_SOVRDCHGP == cmnd ) ) {
			send.OverrideChange( cmnd, 0, data );
		} else if( REQ_MODECHG == cmnd ) {
			send.ModeChange( data );
		} else {
			OUTPUTLOG( "CALL! NOT CHECK NUMBER! <<<< END <<<<\n" );
			return -1;
		}
	}
	OUTPUTLOG( "<<<< END! SendCommandWrap\n" );
	return 0;
}
extern "C" __declspec ( dllexport )int _stdcall ConvIMData( int hCom, short type, void *p_mm, void *p_inch, short InchMode, short InchAxis )
{
	OUTPUTLOG( "CALL! ConvIMDataWrap >>>> START >>>>\n" );
	char buf[256];
	ZeroMemory( buf, sizeof( buf ) );
	OUTPUTLOG( "CALL! ConvIMDataWrap <<<< END <<<<\n" );
	return 0;
}

extern "C" __declspec ( dllexport )int _stdcall GcdInitialize( LPVOID data )
{
	PRGINITDATA param;
	ZeroMemory( &param, sizeof( param ) );
	CopyMemory( &param, data, sizeof( PRGINITDATA ) );
	return 0;
}

extern "C" __declspec ( dllexport )int _stdcall GcdGetErrLine()
{
	return 123;
}

extern "C" __declspec ( dllexport )int _stdcall FileGcodeConv( LPBYTE fname, LPDWORD size, LPVOID bin, short *pno )
{
	OUTPUTLOG( "CALL! FileProgramConv >>>> START >>>>\n" );
	char buf[256];
	ZeroMemory( buf, sizeof( buf ) );
	wsprintf( buf, "fname[%s]", fname );
	OUTPUTLOG( buf );

	FILE *fpr = NULL;
		fopen_s( &fpr, (char*)fname, "rb" );
		if( NULL != fpr ) {
;
		}
		int fd = _fileno( fpr );
		struct stat stbuf;
		fstat( fd, &stbuf );

		fread( bin, stbuf.st_size, 1, fpr );

		*size = stbuf.st_size;
	
	fclose( fpr );

	OUTPUTLOG( "CALL! FileProgramConv <<<< END <<<<\n" );
	return 0;
}
