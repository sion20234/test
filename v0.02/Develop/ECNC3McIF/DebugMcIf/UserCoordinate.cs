﻿// <auto-generated />
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ECNC3.Models.McIf;
using ECNC3.Models;
using ECNC3.Enumeration;

namespace DebugMcIf
{
	public partial class UserCoordinate : UserControl
	{
		public UserCoordinate()
		{
			InitializeComponent();
		}
		public bool VisibleSelectionCheckBox
		{
			set
			{
				_chkAxisX.Visible = value;
				_chkAxisY.Visible = value;
				_chkAxisW.Visible = value;
				_chkAxisZ.Visible = value;
				_chkAxisA.Visible = value;
				_chkAxisB.Visible = value;
				_chkAxisC.Visible = value;
				_chkAxisI.Visible = value;
			}
		}
		public void GetParam( StructureAxisCoordinate target )
		{
			if( null != target ) {
				target.EnableAxis = (short)EnabledAxis;
				target.AxisX = (int)_edtAxisX.Value;
				target.AxisY = (int)_edtAxisY.Value;
				target.AxisW = (int)_edtAxisW.Value;
				target.AxisZ = (int)_edtAxisZ.Value;
				target.AxisA = (int)_edtAxisA.Value;
				target.AxisB = (int)_edtAxisB.Value;
				target.AxisC = (int)_edtAxisC.Value;
			}
		}
		public void SetParam( StructureAxisCoordinate target )
		{
			_edtAxisX.Value = target.AxisX;
			_edtAxisY.Value = target.AxisY;
			_edtAxisW.Value = target.AxisW;
			_edtAxisZ.Value = target.AxisZ;
			_edtAxisA.Value = target.AxisA;
			_edtAxisB.Value = target.AxisB;
			_edtAxisC.Value = target.AxisC;
		}
		private AxisBits EnabledAxis
		{
			get
			{
				AxisBits axis = AxisBits.Free;
				if( true == _chkAxisX.Checked ) {
					axis |= AxisBits.X;
				}
				if( true == _chkAxisY.Checked ) {
					axis |= AxisBits.Y;
				}
				if( true == _chkAxisW.Checked ) {
					axis |= AxisBits.W;
				}
				if( true == _chkAxisZ.Checked ) {
					axis |= AxisBits.Z;
				}
				if( true == _chkAxisA.Checked ) {
					axis |= AxisBits.A;
				}
				if( true == _chkAxisB.Checked ) {
					axis |= AxisBits.B;
				}
				if( true == _chkAxisC.Checked ) {
					axis |= AxisBits.C;
				}
				if( true == _chkAxisI.Checked ) {
					axis |= AxisBits.I;
				}
				return axis;
			}
		}
		public void GetParam( McReqPositioningPointToPoint target )
		{
			if( true == _chkAxisX.Checked ) {
				if( null == target.AxisX ) {
					target.AxisX = new StructurePositioniingItem();
				}
				target.AxisX.Movable = _chkAxisX.Checked;
				target.AxisX.TargetPosition = (int)_edtAxisX.Value;
			}
			if( true == _chkAxisY.Checked ) {
				if( null == target.AxisY ) {
					target.AxisY = new StructurePositioniingItem();
				}
				target.AxisY.Movable = _chkAxisY.Checked;
				target.AxisY.TargetPosition = (int)_edtAxisY.Value;
			}

			if( true == _chkAxisW.Checked ) {
				if( null == target.AxisW ) {
					target.AxisW = new StructurePositioniingItem();
				}
				target.AxisW.Movable = _chkAxisW.Checked;
				target.AxisW.TargetPosition = (int)_edtAxisW.Value;
			}
			if( true == _chkAxisZ.Checked ) {
				if( null == target.AxisZ ) {
					target.AxisZ = new StructurePositioniingItem();
				}
				target.AxisZ.Movable = _chkAxisZ.Checked;
				target.AxisZ.TargetPosition = (int)_edtAxisZ.Value;
			}
			if( true == _chkAxisA.Checked ) {
				if( null == target.AxisA ) {
					target.AxisA = new StructurePositioniingItem();
				}
				target.AxisA.Movable = _chkAxisA.Checked;
				target.AxisA.TargetPosition = (int)_edtAxisA.Value;
			}
			if( true == _chkAxisB.Checked ) {
				if( null == target.AxisB ) {
					target.AxisB = new StructurePositioniingItem();
				}
				target.AxisB.Movable = _chkAxisB.Checked;
				target.AxisB.TargetPosition = (int)_edtAxisB.Value;
			}
			if( true == _chkAxisC.Checked ) {
				if( null == target.AxisC ) {
					target.AxisC = new StructurePositioniingItem();
				}
				target.AxisC.Movable = _chkAxisC.Checked;
				target.AxisC.TargetPosition = (int)_edtAxisC.Value;
			}
			if( true == _chkAxisI.Checked ) {
				if( null == target.AxisI ) {
					target.AxisI = new StructurePositioniingItem();
				}
				target.AxisI.Movable = _chkAxisI.Checked;
				target.AxisI.TargetPosition = (int)_edtAxisI.Value;
			}
		}

		private void UserCoordinate_Load( object sender, EventArgs e )
		{
			using( McDatRomSwitch mc = new McDatRomSwitch() ) {
				mc.Read();
				_edtAxisA.Enabled = ( true == mc.EnableAxisA ) ? true : false;
				_chkAxisA.Enabled = ( true == mc.EnableAxisA ) ? true : false;
				_edtAxisB.Enabled = ( true == mc.EnableAxisB ) ? true : false;
				_chkAxisB.Enabled = ( true == mc.EnableAxisB ) ? true : false;
				_edtAxisC.Enabled = ( true == mc.EnableAxisC ) ? true : false;
				_chkAxisC.Enabled = ( true == mc.EnableAxisC ) ? true : false;
				_chkAxisI.Enabled = false;
				_edtAxisI.Enabled = false;
			}
		}
	}
}