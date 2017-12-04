using System;
using System.Drawing;
using System.Windows.Forms;
using ECNC3.Enumeration;
using ECNC3.Models;
using ECNC3.Models.Common;
using ECNC3.Models.McIf;

namespace DebugMcIf
{
	/// <summary>アプリケーションメインフォーム</summary>
	public partial class FormMain : Form
	{
		/// <summary>再読み込み中フラグ</summary>
		private bool _reloading = false;
		/// <summary>MCボードインターフェースクラス</summary>
		private McCommProc _mcIf = null;
		/// <summary>コンストラクタ</summary>
		public FormMain()
		{
			InitializeComponent();
			Application.ApplicationExit += new EventHandler( this.OnApplicationExit );
		}
		/// <summary>画面ロード</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void FormMain_Load( object sender, EventArgs e )
		{
			_reloading = true;
			System.Threading.Thread.CurrentThread.Name = "DebugMcIf.FormMain";
			AidControl aid = new AidControl();
			aid.SetEnables( this.Controls, false, _chkMcConnect );
			_userCoord.VisibleSelectionCheckBox = false;
			//	関数呼び出しの列挙定義値をコンボボックスに設定する。
			foreach( CallScreenTypes item in Enum.GetValues( typeof( CallScreenTypes ) ) ) {
				_cmbCallScreen.Items.Add( item );
			}
			_cmbCallScreen.SelectedIndex = 0;
			_cmbTaskMode.Items.Clear();
			foreach( McTaskModes item in Enum.GetValues( typeof( McTaskModes ) ) ) {
				if( McTaskModes.NotSupported != item ) {
					_cmbTaskMode.Items.Add( item );
				}
			}
			_cmbCoordMode.SelectedIndex = 0;
			_reloading = false;
		}
		/// <summary>アプリケーションの終了</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void OnApplicationExit( object sender, EventArgs e )
		{
			try {
				if( null != _mcIf ) {
					_mcIf.Dispose();
					_mcIf = null;
				}
			} finally {
				;
			}
		}
		/// <summary>MCボード接続</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void _chkMcConnect_CheckedChanged( object sender, EventArgs e )
		{
			Cursor preCursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;
			{
				bool state = ( sender as CheckBox ).Checked;
				if( true == state ) {
					//	接続
					if( null == _mcIf ) {
						_mcIf = new McCommProc();
					}
					_mcIf.Initialize();
					//	初期化が完了したので設定可能なステータスを反映する。
					Reload();
				} else {
					if( null != _mcIf ) {
						_mcIf.Terminate();
					}
				}
				AidControl aid = new AidControl();
				aid.SetEnables( this.Controls, state, _chkMcConnect );
			}
			Cursor.Current = preCursor;
		}
		/// <summary>表示更新ボタン押下イベント</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void _btnRefresh_Click( object sender, EventArgs e )
		{
			Reload();
		}
		/// <summary>アプリケーション TOP MOST設定</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void _btnTopMost_Click( object sender, EventArgs e )
		{
			TopMost = ( true == TopMost ) ? false : true;
		}
		/// <summary>各種状態更新</summary>
		private void Reload()
		{
			_reloading = true;
			AidControl aid = new AidControl();
			using( McDatStatus mc = new McDatStatus() ) {
				mc.Read();
				_cmbTaskMode.SelectedItem = mc.Status.MotionMode;
				int coordMode = _cmbCoordMode.SelectedIndex;
				if( 0 == coordMode ) {
					using( StructureAxisCoordinate coord = mc.Status.CoordinateAsAbsReg ) {
						_userCoord.SetParam( coord );
					}
				} else if( 1 == coordMode ) {
					using( StructureAxisCoordinate coord = mc.Status.CoordinateAsAbsReg )
					using( McDatWorkOrigin dat = new McDatWorkOrigin() ) {
						dat.Read();
						dat.Coordinate.EnableAxis = 0x00FF;
						coord.OffSset( dat.Coordinate );
						_userCoord.SetParam( coord );
					}
				} else if( 2 == coordMode ) {
					using( McDatWorkOrigin dat = new McDatWorkOrigin() ) {
						dat.Read();
						_userCoord.SetParam( dat.Coordinate );
					}
				}
				//	オーバーライド
				_edtOverrideOverall.Value = mc.Status.OverrideAsOverall;
				_edtOverrideInterpolation.Value = mc.Status.OverrideAsInterpolation;
				_edtOverrideSpindle.Value = mc.Status.OverrideAsSpindle;
				//	原点復帰済み
				_userAxis.Selected = mc.Status.CompletedReturnToOrigins;
				//	W軸上限値設定
				_edtReqWPosLimit.Value = (int)mc.Status.WAxisUpperLimit;

				aid.SetState( _chkCompletedFg, mc.Status.CompletedFg );
				aid.SetState( _chkMcCompletedSequence, mc.Status.CompletedSequence );
			}
			//	加工条件情報
			using( McDatProcessCondition mc = new McDatProcessCondition() ) {
				mc.Read();
				AidControl ctrl = new AidControl();
				ctrl.SetColorRadio3( _btnReqSpinStop, _btnReqSpinCw, _btnReqSpinCcw, (int)mc.SpinOut );
				aid.SetState( _btnReqDischargeOn, mc.Discharge );
				aid.SetState( _btnReqPumpOn, mc.PumpOut );
				_edtSelectedProcessConditionNumber.Value = mc.PNo;
			}
			//	AEC情報
			using( McDatAecData mc = new McDatAecData() ) {
				mc.Read();
				_edtAecElectrodeNumber.Value = mc.ElectrodeNumber;
				_edtAecGuideNumber.Value = mc.GuideNumber;
			}
			//	I/O情報
			using( McDatIOData mc = new McDatIOData() ) {
				mc.Read();
				aid.SetState( _btnMcStart, mc.StartButton );
				aid.SetState( _btnGuideClamp, mc.GuideClamp );
				_btnColletClamp.BackColor = ( true == mc.ColletClamp ) && ( false == mc.ColletUnclamp ) ? Color.LawnGreen :
					 ( false == mc.ColletClamp ) && ( true == mc.ColletUnclamp ) ? Color.LightGray : Color.Yellow;
			}
			_reloading = false;
		}
		/// <summary>切り替え画面呼び出し</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void _btnReqSwithing_Click( object sender, EventArgs e )
		{
			FormSwitching form = new FormSwitching();
			form.Show( this );
		}

		/// <summary>呼び出し機能タイプ</summary>
		private enum CallScreenTypes
		{
			/// <summary>未選択</summary>
			Free,
			/// <summary>加工条件</summary>
			ProcessConditionTable,
			/// <summary>AEC制御</summary>
			AecControll,
			/// <summary>パーティション設定</summary>
			PartitionSetting,
			/// <summary>軸移動</summary>
			AxisMoving,
			/// <summary>プログラム呼び出し</summary>
			OpenProgram,
			/// <summary>初期設定</summary>
			StartupSetting,
		}
		/// <summary>機能画面呼び出し</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void _cmbCallScreen_SelectionChangeCommitted( object sender, EventArgs e )
		{
			CallScreenTypes selected = (CallScreenTypes)_cmbCallScreen.SelectedItem;
			ComboBox target = sender as ComboBox;
			if( CallScreenTypes.PartitionSetting == selected ) {
				FormPartitionSetting form = new FormPartitionSetting();
				form.ShowDialog( this );
			} else if( CallScreenTypes.AxisMoving == selected ) {
				FormAxisMoving form = new FormAxisMoving();
				form.ShowDialog( this );
			} else if( CallScreenTypes.StartupSetting == selected ) {
				FormStartupSetting form = new FormStartupSetting();
				form.ShowDialog( this );
			} else if( CallScreenTypes.OpenProgram == selected ) {
				FormProgram form = new FormProgram();
				form.ShowDialog( this );
			} else if( CallScreenTypes.AecControll == selected ) {
				FormAec form = new FormAec();
				form.ShowDialog( this );
			} else if( CallScreenTypes.ProcessConditionTable == selected ) {
				FormProcessConditionTable form = new FormProcessConditionTable();
				form.ShowDialog( this );
			}
			target.SelectedIndex = 0;
			Reload();
		}

		/// <summary>スタートボタンマウスダウン</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void _btnMcStart_MouseDown( object sender, MouseEventArgs e )
		{
			using( McIfDebugEmulation mc = new McIfDebugEmulation() ) {
				mc.SignalStartButton( true );
			}
			Reload();
		}
		/// <summary>スタートボタンマウスアップ</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void _btnMcStart_MouseUp( object sender, MouseEventArgs e )
		{
			using( McIfDebugEmulation mc = new McIfDebugEmulation() ) {
				mc.SignalStartButton( false );
			}
			Reload();
		}

		/// <summary>動作モード切替</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void _cmbTaskMode_SelectedIndexChanged( object sender, EventArgs e )
		{
			if( true == _reloading ) {
				return;
			}
			using( McReqModeChange ms = new McReqModeChange() ) {
				ms.TaskMode = (McTaskModes)_cmbTaskMode.SelectedItem;
				ms.Execute();
			}
			Reload();
		}

		/// <summary>ガイド クランプ／アンクランプ</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void _btnGuideClamp_Click( object sender, EventArgs e )
		{
			using( McReqClumpGuide mc = new McReqClumpGuide() )
			using( McDatIOData status = new McDatIOData() ) {
				status.Read();
				mc.Clamped = ( true == status.GuideClamp ) ? false : true;
				mc.Execute();
			}
			Reload();
		}
		/// <summary>スピンドル クランプ／アンクランプ</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void _btnColletClamp_Click( object sender, EventArgs e )
		{
			using( McReqClumpSpindle mc = new McReqClumpSpindle() )
			using( McDatIOData status = new McDatIOData() ) {
				status.Read();
				mc.Clamped = ( true == status.ColletClamp ) ? false : true;
				mc.Execute();
			}
			Reload();
		}

		/// <summary>回転設定</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void _btnReqSpin_Click( object sender, EventArgs e )
		{
			using( McReqControllingSpin mc = new McReqControllingSpin() ) {
				if( true == _btnReqSpinStop.Equals( sender ) ) {
					mc.SpinAction = SpinStates.Stop;
				} else if( true == _btnReqSpinCw.Equals( sender ) ) {
					mc.SpinAction = SpinStates.Clockwise;
				} else if( true == _btnReqSpinCcw.Equals( sender ) ) {
					mc.SpinAction = SpinStates.Counterclockwise;
				}
				mc.Execute();
			}
			Reload();
		}
		/// <summary>ポンプ設定</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void _btnReqPump_Click( object sender, EventArgs e )
		{
			using( McReqControllingPump mc = new McReqControllingPump() )
			using( McDatProcessCondition status = new McDatProcessCondition() ) {
				status.Read();
				mc.Enabled = ( true == status.PumpOut ) ? false : true;
				mc.Execute();
			}
			Reload();
		}
		/// <summary>放電設定</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void _btnReqDischarge_Click( object sender, EventArgs e )
		{
			using( McReqControllingDischarge mc = new McReqControllingDischarge() )
			using( McDatProcessCondition status = new McDatProcessCondition() ) {
				status.Read();
				mc.Enabled = ( true == status.Discharge ) ? false : true;
				mc.Execute();
			}
			Reload();
		}
		/// <summary>原点復帰</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void _btnReqReturnToOrigin_Click( object sender, EventArgs e )
		{
			using( McReqForceReturnToOrigin mc = new McReqForceReturnToOrigin() ) {
				mc.EnableAxis = _userAxis.Selected;
				mc.Execute();
			}
			Reload();
		}

		/// <summary>オーバライド値編集</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void _edtOverride_ValueChanged( object sender, EventArgs e )
		{
			using( McReqOverrideChange mc = new McReqOverrideChange() ) {
				if( true == _edtOverrideOverall.Equals( sender ) ) {
					mc.OverrideMode = OverrideModes.Overall;
				} else if( true == _edtOverrideInterpolation.Equals( sender ) ) {
					mc.OverrideMode = OverrideModes.Interpolation;
				} else if( true == _edtOverrideSpindle.Equals( sender ) ) {
					mc.OverrideMode = OverrideModes.Spindle;
				} else {
					return;
				}
				mc.SettingValue = (short)( sender as NumericUpDown ).Value;
				mc.Execute();
			}
		}
		/// <summary>電極番号設定</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void _edtAecElectrodeNumber_ValueChanged( object sender, EventArgs e )
		{
			using( McReqElectrodeNumber mc = new McReqElectrodeNumber() ) {
				mc.ElectrodeNumber = (short)_edtAecElectrodeNumber.Value;
				mc.Execute();
			}
		}
		/// <summary>ガイド番号設定</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void _edtAecGuideNumber_ValueChanged( object sender, EventArgs e )
		{
			using( McReqGuideNumber mc = new McReqGuideNumber() ) {
				mc.GuideNumber = (short)_edtAecGuideNumber.Value;
				mc.Execute();
			}
		}
		/// <summary>座標表示モード</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void _cmbCoordMode_SelectedIndexChanged( object sender, EventArgs e )
		{
			if( true == _reloading ) {
				return;
			}
			Reload();
		}
		/// <summary>>W軸上限値設定</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void _edtReqWPosLimit_ValueChanged( object sender, EventArgs e )
		{
			using( McReqWAxisUpperLimit mc = new McReqWAxisUpperLimit() ) {
				mc.SettingValue = (int)_edtReqWPosLimit.Value;
				mc.Execute();
			}
		}
		/// <summary>加工条件番号変更</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void _edtSelectedProcessConditionNumber_ValueChanged( object sender, EventArgs e )
		{
			using( McReqProcessConditionNumberSelect mc = new McReqProcessConditionNumberSelect() ) {
				mc.SelectingNumber = (int)_edtSelectedProcessConditionNumber.Value;
				mc.Execute();
			}
		}
		/// <summary>MC状態設定</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void _chkMcCompletedSequence_CheckedChanged( object sender, EventArgs e )
		{
			using( McIfDebugEmulation mc = new McIfDebugEmulation() ) {
				CheckBox target = sender as CheckBox;
				bool checking = target.Checked;
				if( true == target.Equals( _chkMcCompletedSequence ) ) {
					mc.SignalCompletedSequence( checking );	//	シークエンス完
				} else if( true == target.Equals( _chkCompletedFg ) ) {
					mc.SignalCompletedFg( checking );		//	FG完
				}
			}
			Reload();
		}
		/// <summary>リセットコマンド</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void _btnReqReset_Click( object sender, EventArgs e )
		{
			using( McReqReset mc = new McReqReset() ) {
				mc.Execute();
			}
			Reload();
		}

		private void _btnIoWrite_Click( object sender, EventArgs e )
		{
			AidConvert aid = new AidConvert();
			int address = 0;
			int mask = 0;
			int value = 0;
			while( true ) {
				if( false==aid.TryParse( _edtIoAddress.Text, out address ) ) {
					break;
				}
				if( false == aid.TryParse( _edtIoMask.Text, out mask ) ) {
					break;
				}
				if( false == aid.TryParse( _edtIoValue.Text, out value ) ) {
					break;
				}
				using( McIfDebugEmulation mc = new McIfDebugEmulation() ) {
					mc.SignalIOData( (ushort)address, (ushort)mask, (ushort)value );
				}
				break;
			}
		}

		private void _chkAlarm_Click( object sender, EventArgs e )
		{
//			using( McIfDebugEmulation mc = new McIfDebugEmulation() ) {
//				mc.SignalAlarm( 3, 0, 0x0008, ( sender as CheckBox ).Checked );
//			}
		}

		private void _btnAlarm_Click( object sender, EventArgs e )
		{
			using( FormAlarm form = new FormAlarm() ) {
				form.ShowDialog( this );
			}
		}
	}
}
