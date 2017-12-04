#pragma once
class CMcSendCommand
{
public:
	CMcSendCommand();
	~CMcSendCommand();
	//	AECDATA
	int ElectrodeNumberChange( LPVOID data );
	int GuideNumberChange( LPVOID data );
	//	IO
	int StartButton( LPVOID data );
	int Reset();
	int GuideClamp( LPVOID data );
	int SpindleClamp( LPVOID data );
	int Buzzer( LPVOID data );
	

	//	PCONDITION
	int ProcessConditinNumberSelect( LPVOID data );
	int SpinOut( LPVOID data );
	int PumpOut( LPVOID data );
	//	PCONDITION.Status
	int Discharge( LPVOID data );
	int DryRun( LPVOID data );
	int DryRunEx( LPVOID data );
	int InitialSet( LPVOID data );
	int AecByLife( LPVOID data );
	int SendingBack( LPVOID data );
	//	STATUS.ECNC.Status2
	int ModeChange( LPVOID data );
	int OverrideChange( int dataType, int task, LPVOID data );
	int OptionalStop ( LPVOID data );
	int TouchSensor( LPVOID data );
	int IncrimentalReferenceAxisMove( LPVOID data );
	int InterlockXY( LPVOID data );
	int HandPulserPermition( LPVOID data );
	int CompletedSequence( LPVOID data );
	int WAxisUpperLimit( LPVOID data );
	int ShowMessage( LPVOID data );
	//	STATUS.ECNC.Status3
	int ShutDownStart( LPVOID data );
	int AutoModeOutput( LPVOID data );

	int EcncAlarm( LPVOID data );

	//	STATUS.Task
	int CompletedFg( LPVOID data );
	//	STATUS.Axis
	int ForceReturnToOrigin( LPVOID data );
	int OpStart();
	//	IOData
	int SetIOData( LPVOID data );
private:
	int WriteStatusTaskStatus( LPVOID data, int mask, int task = 0 );
	int WriteStatusEcncStatus2( LPVOID data, int mask, bool connectB = false );
	int WriteStatusAxisStatus( short state, int mask, int axis );
//	int WritePConditionStatus( short state, int mask );
//	int WriteIOData( short state, short address, int mask );

//	int WriteVirPos( int number, long* pos, int size, int axis = -1 );
};

