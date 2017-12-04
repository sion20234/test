using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ECNC3.Enumeration;
using ECNC3.Models.McIf;

namespace DebugMcIf
{
	/// <summary>AEC操作</summary>
	public partial class FormAec : Form
	{
		/// <summary>コンストラクタ</summary>
		public FormAec()
		{
			InitializeComponent();
		}
		/// <summary>画面読み込み</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void FormAec_Load( object sender, EventArgs e )
		{
			Reload();
		}
		/// <summary>再読み込み</summary>
		private void Reload()
		{
			AidControl aid = new AidControl();
			using( McDatIOData mc = new McDatIOData() ) {
				mc.Read();
				//	EFSフィンガー
				aid.SetState( _btnEsfFingerOpen, ( true == mc.ColletFingerClamp ) ? false : true );
				aid.SetState( _btnEsfFingerClose, mc.ColletFingerClamp );
				//	EFSアーム
				aid.SetState( _btnEsfArmFoward, mc.EsfArmFoward );
				aid.SetState( _btnEsfArmMiddle, mc.EsfArmMiddle );
				aid.SetState( _btnEsfArmBack, mc.EsfArmBack1 && mc.EsfArmBack1 );
			}
		}
		/// <summary>電極装着/脱着動作開始コマンド</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void _btnElectrodeInsall_Click( object sender, EventArgs e )
		{
			Button target = sender as Button;
			string name = target.Name;
			using( McReqElectrodeInsall mc = new McReqElectrodeInsall() ) {
				if( 0 == string.Compare( name, _btnElectrodeInstall.Name, true ) ) {
					mc.Install = true;
				} else if( 0 == string.Compare( name, _btnElectrodeRemove.Name, true ) ) {
					mc.Install = false;
				} else {
					return;
				}
				mc.ElectrodeNumber = (short)_edtElectrodeInstallNumber.Value;
				mc.Execute();
			}
		}
		/// <summary>電極交換位置移動コマンド</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void _btnElectrodeMove_Click( object sender, EventArgs e )
		{
			Button target = sender as Button;
			string name = target.Name;
			using( McReqElectrodeMove mc = new McReqElectrodeMove() ) {
				//	対象軸
				mc.Axis = _userAxisElectrode.Selected;
				//	動作
				if( 0 == string.Compare( name, _btnElectrodeExchange.Name, true ) ) {
					mc.Position = ElectrodePositions.Exchange;
				} else if( 0 == string.Compare( name, _btnElectrodeFoward.Name, true ) ) {
					mc.Position = ElectrodePositions.Foward;
				} else if( 0 == string.Compare( name, _btnElectrodeStandBy.Name, true ) ) {
					mc.Position = ElectrodePositions.StandBy;
				} else {
					return;
				}
				mc.Execute();
			}
		}
		/// <summary>ESFマガジン移動コマンド </summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void _btnEsfMoveMagazine_Click( object sender, EventArgs e )
		{
			Button target = sender as Button;
			string name = target.Name;
			using( McReqEsfMoveMagazine mc = new McReqEsfMoveMagazine() ) {
				if( 0 == string.Compare( name, _btnEsfMagazineIncliment.Name, true ) ) {
					;
				} else if( 0 == string.Compare( name, _btnReqEsfMoveMagazine.Name, true ) ) {
					mc.MagazineNumber = (int)_edtEsfMagazineNumber.Value;
				} else {
					return;
				}
				mc.Execute();
			}
		}
		/// <summary>ESFアーム移動コマンド</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void _btnEsfMoveArm_Click( object sender, EventArgs e )
		{
			Button target = sender as Button;
			string name = target.Name;
			using( McReqEsfMoveArm mc = new McReqEsfMoveArm() ) {
				if( 0 == string.Compare( name, _btnEsfArmFoward.Name, true ) ) {
					mc.Position = EsfArmPositions.Foward;
				} else if( 0 == string.Compare( name, _btnEsfArmMiddle.Name, true ) ) {
					mc.Position = EsfArmPositions.Middle;
				} else if( 0 == string.Compare( name, _btnEsfArmBack.Name, true ) ) {
					mc.Position = EsfArmPositions.Back;
				} else {
					return;
				}
				mc.Execute();
			}
		}

		/// <summary>ESFフィンガーOPEN/CLOSEコマンド</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void _btnEsfMoveFinger_Click( object sender, EventArgs e )
		{
			Button target = sender as Button;
			string name = target.Name;
			using( McReqEsfMoveFinger mc = new McReqEsfMoveFinger() ) {
				if( 0 == string.Compare( name, _btnEsfFingerOpen.Name, true ) ) {
					mc.FingerOpen = true;
				} else if( 0 == string.Compare( name, _btnEsfFingerClose.Name, true ) ) {
					mc.FingerOpen = false;
				} else {
					return;
				}
				mc.Execute();
			}
		}

		/// <summary>ガイド装着／脱着動作開始コマンド</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void _btnGuideInstall_Click( object sender, EventArgs e )
		{
			Button target = sender as Button;
			string name = target.Name;
			using( McReqGuideInsall mc = new McReqGuideInsall() ) {
				if( 0 == string.Compare( name, _btnGuideInstall.Name, true ) ) {
					mc.Install = true;
				} else if( 0 == string.Compare( name, _btnGuideRemove.Name, true ) ) {
					mc.Install = false;
				} else {
					return;
				}
				mc.GuideNumber = (short)_edtGuideInstallNumber.Value;
				mc.Execute();
			}
		}
		/// <summary>ガイド交換位置移動コマンド </summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void _btnGuideMove_Click( object sender, EventArgs e )
		{
			Button target = sender as Button;
			string name = target.Name;
			using( McReqGuideMove mc = new McReqGuideMove() ) {
				//	対象軸
				mc.Axis = _userAxisGuide.Selected;
				//	動作
				if( 0 == string.Compare( name, _btnGuideExchange.Name, true ) ) {
					mc.Position = GuidePositions.Exchange;
				} else if( 0 == string.Compare( name, _btnGuideStandBy.Name, true ) ) {
					mc.Position = GuidePositions.StandBy;
				} else {
					return;
				}
				mc.Execute();
			}
		}

		/// <summary>GSFアーム移動（前端/後端）コマンド</summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント引数</param>
		private void _btnGsfMoveArm_Click( object sender, EventArgs e )
		{
			Button target = sender as Button;
			string name = target.Name;
			using( McReqGsfMoveArm mc = new McReqGsfMoveArm() ) {
				if( 0 == string.Compare( name, _btnGsfArmFoward.Name, true ) ) {
					mc.Position = GsfArmPositions.Foward;
				} else if( 0 == string.Compare( name, _btnGsfArmBack.Name, true ) ) {
					mc.Position = GsfArmPositions.Back;
				} else {
					return;
				}
				mc.Execute();
			}
		}
	}
}
