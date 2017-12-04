///////////////////////////////////////////////////////////////////////////////////////////
//
// (1) システム名称 : ELENIX ECNCⅢ
// (2) ファイル名   : ConditionsCallSetForm.cs
// (3) 概要         : 加工条件呼出し画面
// (4) 作成日       : 2015.06.01
// (5) 作成者       : 八野
// (6) 特記事項     : VisualStudio2015 C#
// (7) 更新履歴     :
//
///////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ECNC3.Models;

namespace ECNC3.Views
{
	/// <summary>検索条件設定画面</summary>
	public partial class ConditionsCallSetForm : ECNC3Form
	{
		/// <summary>電極径 検索条件 なし 表示文字列</summary>
		private readonly string _findElectrodeDiameterFree = "ALL";
		/// <summary>コンストラクタ</summary>
		public ConditionsCallSetForm()
		{
			InitializeComponent();
		}

		/// <summary>終了通知</summary>
		private NotifyReturnDelegate _notifyReturn = null;

		/// <summary>設定結果取得関数定義</summary>
		public delegate void NotifyReturnDelegate();

		/// <summary>>設定結果取得</summary>
		public NotifyReturnDelegate NotifyReturn
		{
			set { _notifyReturn = value; }
		}
		/// <summary>実行結果</summary>
		public enum Results
		{
			/// <summary>検索実行</summary>
			Execute,
			/// <summary>閉じる</summary>
			Return,
		}
		/// <summary>実行結果</summary>
		public Results FindResult { get; private set; } = Results.Return;
		/// <summary>検索条件(材料名称)</summary>
		public List<string> FindMaterials { get; set; }
		/// <summary>検索条件(電極径)</summary>
		public decimal FindElectrodeDiameter { get; set; }

		/// <summary>ロード</summary>
		/// <param name="sender">イベント送信元オブジェクト</param>
		/// <param name="e">イベントパラメータ</param>
		private void ConditionsCallSetForm_Load( object sender, EventArgs e )
		{
			//	材料名称
			using( FileProcessConditionParameter fa = new FileProcessConditionParameter() ) {
				fa.Read();
				int index = 0;
				foreach( Control item in _panelMaterial.Controls ) {
					if( typeof( CheckBoxEx ) != item.GetType() ) {
						continue;
					}
					CheckBoxEx target = item as CheckBoxEx;
					//	コントロールの初期設定
					target.Text = string.Empty;
					target.Enabled = false;

					string name = target.Name;
					if( 2 > name.Length ) {
						continue;
					}
					int result = 0;
					if( false == int.TryParse( name.Substring( name.Length - 2, 2 ), out result ) ) {
						continue;
					}
					index = fa.Materials.FindIndex( ( x ) => x.Number == result );
					if( 0 > index ) {
						continue;
					}
					if( false == string.IsNullOrEmpty( fa.Materials[index].Name ) ) {
						target.Text = fa.Materials[index].Name;
						target.Enabled = true;
						if( null != FindMaterials ) {
							target.SetChecked( ( 0 > FindMaterials.FindIndex( ( x ) => 0 == string.Compare( x, target.Text, false ) ) ) ? false : true );
							//target.Checked = ( 0 > FindMaterials.FindIndex( ( x ) => 0 == string.Compare( x, target.Text, false ) ) ) ? false : true;
						}
					}
				}
			}
			//	電極径
			if( 1 > ( FindElectrodeDiameter * 100 ) ) {
                _edtElectrodeDiameter.TextAlign = HorizontalAlignment.Left;

                _edtElectrodeDiameter.RawText = _findElectrodeDiameterFree;
			} else {
				_edtElectrodeDiameter.Value = FindElectrodeDiameter;
			}
			_edtElectrodeDiameter.FormatType = NumericTextBox.FormatTypes.DecimalUpper1Lower2;
		}

		/// <summary>検索条件リセット</summary>
		public void Reset()
		{
			if( null != FindMaterials ) {
				FindMaterials.Clear();
				FindMaterials = null;
			}
			FindElectrodeDiameter = 0;
		}
		/// <summary>【閉じる】ボタンクリックイベント</summary>
		/// <param name="sender">イベント送信元オブジェクト</param>
		/// <param name="e">イベントパラメータ</param>
		private void _btnReturn_Click( object sender, EventArgs e )
		{
			Reset();
			FindResult = Results.Return;
			_notifyReturn?.Invoke();
		}

		/// <summary>【検索実行】ボタンクリックイベント</summary>
		/// <param name="sender">イベント送信元オブジェクト</param>
		/// <param name="e">イベントパラメータ</param>
		private void _btnExecute_Click( object sender, EventArgs e )
		{
			Reset();
			string name = string.Empty;
			//	材料名称
			foreach( Control item in _panelMaterial.Controls ) {
				if( typeof( CheckBoxEx ) != item.GetType() ) {
					continue;
				}
				CheckBoxEx target = item as CheckBoxEx;
				if( true == target.Equals( _chkMatFree ) ) {
					continue;
				}
				if( false == target.Checked ) {
					continue;
				}
				name = target.Text;
				if( true == string.IsNullOrEmpty( name ) ) {
					continue;
				}
				if( null == FindMaterials ) {
					FindMaterials = new List<string>();
				}
				FindMaterials.Add( name );
			}
			//	電極径
			FindElectrodeDiameter = _edtElectrodeDiameter.Value;
			FindResult = Results.Execute;
			_notifyReturn?.Invoke();
		}
		/// <summary>材料選択クリア</summary>
		/// <param name="sender">イベント送信元オブジェクト</param>
		/// <param name="e">イベントパラメータ</param>
		private void _chkMatFree_Click( object sender, EventArgs e )
		{
			//	押された状態のままのボタンを元に戻す
			foreach( Control item in _panelMaterial.Controls ) {
				if( typeof( CheckBoxEx ) == item.GetType() ) {
					CheckBoxEx target = item as CheckBoxEx;
					target.Checked = false;
				}
			}
		}

		/// <summary>電極径入力</summary>
		/// <param name="sender">イベント送信元オブジェクト</param>
		/// <param name="e">イベントパラメータ</param>
		private void _btnDiameterInput_Click( object sender, EventArgs e )
		{
			Button target = sender as Button;
			int currentValue = (int)( _edtElectrodeDiameter.Value * 100 );
			if( true == target.Equals( _btnDiameter001Up ) ) {
				if( 999 > currentValue ) {
                    _edtElectrodeDiameter.TextAlign = HorizontalAlignment.Right;
                    _edtElectrodeDiameter.Value += (decimal)0.01;
				}
			} else if( true == target.Equals( _btnDiameter001Down ) ) {
				if( 0 < currentValue ) {
                    _edtElectrodeDiameter.TextAlign = HorizontalAlignment.Right;
                    _edtElectrodeDiameter.Value -= (decimal)0.01;
				}
			} else if( true == target.Equals( _btnDiameter010Up ) ) {
				if( 990 > currentValue ) {
                    _edtElectrodeDiameter.TextAlign = HorizontalAlignment.Right;
                    _edtElectrodeDiameter.Value += (decimal)0.10;
				}
			} else if( true == target.Equals( _btnDiameter010Down ) ) {
				if( 9 < currentValue ) {
                    _edtElectrodeDiameter.TextAlign = HorizontalAlignment.Right;
                    _edtElectrodeDiameter.Value -= (decimal)0.10;
				}
			} else if( true == target.Equals( _btnDiameter100Up ) ) {
				if( 900 > currentValue ) {
                    _edtElectrodeDiameter.TextAlign = HorizontalAlignment.Right;
                    _edtElectrodeDiameter.Value += (decimal)1.00;
				}
			} else if( true == target.Equals( _btnDiameter100Down ) ) {
				if( 99 < currentValue ) {
                    _edtElectrodeDiameter.TextAlign = HorizontalAlignment.Right;
                    _edtElectrodeDiameter.Value -= (decimal)1.00;
				}
			} else if( true == target.Equals( _btnDiameterFree ) ) {
                _edtElectrodeDiameter.TextAlign = HorizontalAlignment.Left;
				_edtElectrodeDiameter.RawText = _findElectrodeDiameterFree;
			}
		}
	}
}
