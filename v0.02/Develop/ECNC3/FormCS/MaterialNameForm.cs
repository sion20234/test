///////////////////////////////////////////////////////////////////////////////////////////
//
// (1) システム名称 : ELENIX ECNCⅢ
// (2) ファイル名   : MaterialNameForm.cs
// (3) 概要         : 材質名登録画面
// (4) 作成日       : 2015.06.01
// (5) 作成者       : 八野
// (6) 特記事項     : VisualStudio2015 C#
// (7) 更新履歴     :
//
///////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Windows.Forms;
using ECNC3.Enumeration;
using ECNC3.Models;

namespace ECNC3.Views
{
	/// <summary>材料名称選択フォーム</summary>
	public partial class MaterialNameForm : ECNC3Form
    {
		/// <summary>コンストラクタ</summary>
		/// <param name="mode">呼び出しモード
		///		<list type="bullet" >
		///			<item>0=材料名称設定</item>
		///			<item>1=材料名称選択</item>
		///		</list>
		/// </param>
		public MaterialNameForm(int mode)
        {
            InitializeComponent();
			if( 1 == mode ) {
				OperationMode = OperationModes.Selectable;
            }
            standardKeyBord1.TextBoxVisible(true);
        }
        /// <summary>コンストラクタ</summary>
        /// <param name="mode">呼び出しモード
        ///		<list type="bullet" >
        ///			<item>0=材料名称設定</item>
        ///			<item>1=材料名称選択</item>
        ///		</list>
        /// </param>
        public MaterialNameForm( OperationModes mode = OperationModes.Setting )
		{
			InitializeComponent();
			OperationMode = mode;
            standardKeyBord1.TextBoxVisible(true);
		}

		/// <summary>設定結果取得関数定義</summary>
		public delegate void NotifyReturnDelegate();
		/// <summary>終了通知</summary>
		private NotifyReturnDelegate _notifyReturn = null;
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
		public Results Result { get; private set; } = Results.Return;

		/// <summary>呼び出しモード</summary>
		public enum OperationModes
		{
			/// <summary>設定モード</summary>
			Setting = 0,
			/// <summary>選択モード</summary>
			Selectable = 1,
		}

		private OperationModes OperationMode { get; set; } = OperationModes.Setting;
		/// <summary>選択された材料名称</summary>
		public string SelectedMaterialName { get; set; } = string.Empty;

		/// <summary>選択状態材料名称ボタンコントロール</summary>
		private RadioButtonEx SelectedItem
		{
			get
			{
				foreach( Control item in _panelName.Controls ) {
					if( typeof( RadioButtonEx ) == item.GetType() ) {
						if( true == ( item as RadioButtonEx ).Checked ) {
							return ( item as RadioButtonEx );
						}
					}
				}
				return null;
			}
		}

		/// <summary>ロードイベント</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MaterialNameForm_Load(object sender, EventArgs e)
        {
			_panelSelect.Visible = ( OperationModes.Selectable == OperationMode ) ? true : false;

			using( FileProcessConditionParameter fa = new FileProcessConditionParameter() ) {
				fa.Read();
				int index = 0;
				foreach( Control item in _panelName.Controls ) {
					if( typeof( RadioButtonEx ) != item.GetType() ) {
						continue;
					}
					RadioButtonEx target = item as RadioButtonEx;
					target.Text = string.Empty;
					//	インデックス番号を確認
					int result = target.GetIndexNumber( 2 );
					//	材料名称リストの該当を検索
					index = fa.Materials.FindByNumber( result );
					if( 0 > index ) {
						continue;
					}
					//	文字列の有効性を確認
					if( false == string.IsNullOrEmpty( fa.Materials[index].Name ) ) {
						target.Text = fa.Materials[index].Name;
					}
					//	呼び出し元の選択済み名称を一致した場合は、ボタンを点灯。
					if( false == string.IsNullOrEmpty( SelectedMaterialName ) ) {
						if( 0 == string.Compare( SelectedMaterialName, target.Text, false ) ) {
							target.Checked = true;
						}
					}
				}
			}
		}
		/// <summary>閉じるボタンクリックイベント</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _btnReturn_Click( object sender, EventArgs e )
		{
			Result = Results.Return;
			_notifyReturn?.Invoke();
			if( OperationModes.Selectable == OperationMode ) {
				return;
			}
			this.Close();
		}

		/// <summary>登録ボタンクリック</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void _btnRegister_Click(object sender, EventArgs e)
        {
			RadioButtonEx target = SelectedItem;
			while( null != target ) {
				using( StructureMaterialNameItem item = new StructureMaterialNameItem() )
				using( FileProcessConditionParameter fa = new FileProcessConditionParameter() ) {
					item.Number = target.GetIndexNumber( 2 );
					if( 0 > item.Number ) {
						break;
					}
					item.Name = standardKeyBord1.GetText();
					ResultCodes ret = fa.WriteMaterial( item );
					if( ResultCodes.Success != ret ) {
						using( MessageDialog msg = new MessageDialog() ) {
							msg.Information( 5003, this );
						}
						break;
					}
                    //	コントロールへの反映
                    target.Text = standardKeyBord1.GetText();
                }
                return;
			}
        }

		/// <summary>材料名称ラジオボタン選択状態変化イベント</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void IntegratedMaterialSelect_CheckedChanged(object sender, EventArgs e)
        {
			RadioButtonEx target = ( sender as RadioButtonEx );
			target.SetChecked( target.Checked );
            standardKeyBord1.SetText((sender as Control).Text);
		}

		/// <summary>選択ボタン押下イベント</summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _btnSelect_Click( object sender, EventArgs e )
		{
			if( OperationModes.Selectable != OperationMode ) {
				return;
			}
			string text = SelectedItem?.Text;
			if( true == string.IsNullOrEmpty( text ) ) {
				return;
			}
			SelectedMaterialName = text;
			Result = Results.Execute;
			_notifyReturn?.Invoke();
		}

        private void _btnDelete_Click(object sender, EventArgs e)
        {
            RadioButtonEx target = SelectedItem;
            while (null != target)
            {
                //	コントロールへの反映
                target.Text = string.Empty;
                return;
            }
        }
    }
}
