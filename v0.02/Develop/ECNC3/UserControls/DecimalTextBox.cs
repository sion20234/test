using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using ECNC3.Views.UserControls;

namespace ECNC3.Views
{
	public partial class DecimalTextBox : TextBox
	{
		/// <summary>コンストラクタ</summary>
		public DecimalTextBox()
		{
			InitializeComponent();
			Validating += DecimalTextBox_Validating;
			KeyPress += DecimalTextBox_KeyPress;
			//Enter += DecimalTextBox_Enter;

			//_defaultBackColor = Models.FileUIStyleTable.DefaultBackColor;
			//_defaultForeColor = Models.FileUIStyleTable.DefaultForeColor;

			//BackColor = _defaultBackColor;
			//ForeColor = _defaultForeColor;
		}

		/// <summary>入力許可下限値</summary>
		public decimal UpperLimit { get; set; } = -99999999;
		/// <summary>入力許可上限値</summary>
		public decimal LowerLimit { get; set; } = 99999999;
		/// <summary>既定コントロールに対する直接的な設定</summary>
		public string RawText
		{
			get { return base.Text; }
			set { base.Text = value; }
		}
		/// <summary>decimal値設定/取得</summary>
		//		public override decimal Text
		public override string Text
		{
			get
			{
				return RawText;
			}
			set
			{
				RawText = value;
			}
		}

		/// <summary>キー押下イベント</summary>
		/// <param name="sender">イベント送信元オブジェクト</param>
		/// <param name="e">イベントパラメータ</param>
		private void NumericTextBox_KeyPress( object sender, KeyPressEventArgs e )
		{
			//NumericTextBox target = sender as NumericTextBox;
			//e.Handled = DigitAlign( e.KeyChar );//桁数判定処理

		}
        /// <summary>
        /// Enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumericTextBox_Enter( object sender, EventArgs e )
		{
			//SelectAll();
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="caretPos">キャレット位置</param>
        public void setCursolPos(int caretPos)
        {
            if (caretPos < 0) caretPos = 0;
            Focus();//ここで１回 Focus()を呼ぶと、このメソッドを２回コールしないで済む
            SelectionStart = caretPos;
            SelectionLength = 0;
            Select(caretPos, 0);
            Focus();
        }
        /// <summary>
        /// 選択されている場合、その文字列を削除
        /// </summary>
        public bool setSelDel()
        {
            int selLeng = SelectionLength;
            if (selLeng == 0) return false;
            int selStart = SelectionStart;
            Text = Text.Remove(selStart, selLeng);
            return true;
        }
        /// <summary>
        /// 小数上位/下位に分けて比較
        /// </summary>
        /// <param name="compVal"></param>
        /// <param name="stringLowerLim"></param>
        /// <param name="stringUpperLim"></param>
        /// <returns></returns>
        public bool upperLowerSepComp(string compVal, string stringLowerLim, string stringUpperLim)
        {
            //マイナスを除く
            compVal = compVal.Replace("-", "");

            //比較値
            string compValL = "";
            string compValR = "";
            upperLowerSep(compVal, ref compValL, ref compValR);

            //最小値
            string stringLowerLimL = "";
            string stringLowerLimR = "";
            upperLowerSep(stringLowerLim, ref stringLowerLimL, ref stringLowerLimR);

            //最大値
            string stringUpperLimL = "";
            string stringUpperLimR = "";
            upperLowerSep(stringUpperLim, ref stringUpperLimL, ref stringUpperLimR);

            //比較
            if (compValL.Length > stringLowerLimL.Length) return false;
            if (compValR.Length > stringLowerLimR.Length) return false;

            if (compValL.Length > stringUpperLimL.Length) return false;
            if (compValR.Length > stringUpperLimR.Length) return false;
            return true;
        }
        /// <summary>
        /// 上位整数部/下位小数部を分ける
        /// </summary>
        /// <param name="stringLim"></param>
        /// <param name="stringLimL"></param>
        /// <param name="stringLimR"></param>
        private void upperLowerSep(string stringLim, ref string stringLimL, ref string stringLimR)
        {
            int stringLimLen = stringLim.Length;

            //小数位置から上位と下位桁数を取得
            int dotIndex = stringLim.IndexOf(".");//小数点位置
            if (dotIndex == -1)
            {
                stringLimL = stringLim;
                stringLimR = "";
            }
            else
            {
                stringLimL = stringLim.Substring(0, dotIndex);
                stringLimR = stringLim.Substring(dotIndex + 1, stringLimLen - dotIndex - 1);
            }
        }

        /// <summary>
        /// 桁数判定処理
        /// </summary>
        /// <param name="_keychar"></param>
        /// <returns>
        /// true = 入力不可(終了)
        /// false = 入力可能
        /// </returns>
        private bool DigitAlign( char _keychar )
		{
			//NumericTextBox.FormatTypes FormatType = new NumericTextBox.FormatTypes();

            int caretPos = SelectionStart;//キャレット位置
            while ( true ) {
				if(Char.IsDigit( _keychar ) ) {
                    //数字
                    setSelDel();                              //選択されている場合、その文字列を削除
                    string tmpString = RawText;
                    string tmpChar = _keychar.ToString();
                    tmpString = tmpString.Insert(caretPos, tmpChar);
                    decimal tmpDecimal = decimal.Parse(tmpString);
                    if (tmpDecimal > UpperLimit) return true;
                    if (tmpDecimal < LowerLimit) return true;
                    if (!upperLowerSepComp(tmpString, LowerLimit.ToString(), UpperLimit.ToString()))
                    {//フォーマットの桁がオーバーしたので戻す
                        return true;
                    }
                    setCursolPos(caretPos);                   //キャレットセット
                }
                else
                {
                    //数字以外

                    // switch (FormatType)
                    //{//フォーマット：フリー
                    //  case NumericTextBox.FormatTypes.Free:
                    switch (_keychar)
                    {
                        case '\r'://エンター・キー
                            break;

                        case '\b'://バックスペース
                            setSelDel(); //選択されている場合、その文字列を削除
                            break;

                        case '/': return true;//スラッシュ

                        case '*': return true;//アスタリスク

                        case '-'://マイナス
                            setSelDel();                              //選択されている場合、その文字列を削除
                            int index = RawText.IndexOf("-");
                            if (index < 0) RawText = "-" + RawText;
                            caretPos++;
                            setCursolPos(caretPos);
                            return true;

                        case '+'://プラス
                            setSelDel();                              //選択されている場合、その文字列を削除
                            RawText = RawText.Replace("-", "");
                            caretPos--;
                            setCursolPos(caretPos);
                            return true;

                        case '.'://ドット
                            setSelDel();                              //選択されている場合、その文字列を削除
                            int minusFlg = 0;
                            int minusIndex = Text.IndexOf("-");
                            int dotIndex = Text.IndexOf(".");

                            if (minusIndex > -1)
                            {//マイナス符号付き
                                minusFlg = 1;
                                RawText = RawText.Replace("-", ""); //"-"削除
                            }

                            //既にある場合、"."を削除
                            RawText = RawText.Replace(".", "");      //"."削除
                            if (caretPos <= 1) caretPos = 1;
                            if (caretPos - 1 < 0) caretPos = 1;
                            
                            if (dotIndex == -1)
                            {//整数
                                if (RawText == "")
                                {
                                    caretPos = 0;
                                }

                                if (caretPos > RawText.Length) caretPos = RawText.Length;//追加：柏原

                                RawText = RawText.Insert(caretPos, ".");
                                caretPos++;//キャレット位置を"."の右辺に移動、次の入力をやりやすくします。
                            }
                            else if( caretPos < dotIndex)
                            {//実数の整数部
                                RawText = RawText.Insert(caretPos - minusFlg, ".");//マイナス符号付きは、caretPosからさらに-1
                            }else
                            {//実数の小数部
                                if (caretPos - 1 - minusFlg < 0) caretPos = 1+ minusFlg;//最低値を超えた場合、最低値をセット
                                RawText = RawText.Insert(caretPos - 1- minusFlg, ".");  //マイナス符号付きは、caretPosからさらに-1
                            }

                            //再度マイナス符号をつける
                            if (minusFlg > 0) RawText = "-" + RawText;

                            //キャレット位置：戻す
                            setCursolPos(caretPos);
                            return true;//ここでは編集はキャンセル
                        default:
                            //上記以外は入力無し
                            return true;
                        }
                }
				if( MaxLength < ( TextLength - SelectionLength + 1 ) ) {
					break;
				}
				return false;
			}
			return true;
		}
		/// <summary>編集検証イベント</summary>
		/// <param name="sender">イベント送信元オブジェクト</param>
		/// <param name="e">イベントパラメータ</param>
		private void DecimalTextBox_Validating( object sender, CancelEventArgs e )
		{
			//NumericTextBox target = sender as NumericTextBox;
			//	書式チェック
		}
        /// <summary>キー押下イベント</summary>
        /// <param name="sender">イベント送信元オブジェクト</param>
        /// <param name="e">イベントパラメータ</param>
        private void DecimalTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumericTextBox target = sender as NumericTextBox;
            if (_filterCodes != null)
            {
                for (int count = 0; count < _filterCodes.Length; count++)
                {
                    if (e.KeyChar.ToString() == _filterCodes[count])
                    {
                        e.Handled = true;//ハードウェアキーボード：ソフトウェアキーで入力禁止キーはハードキーでも入力禁止：柏原
                        return;
                    }
                }
            }
            e.Handled = DigitAlign( e.KeyChar );
		}
        private string[] _filterCodes;
        /// <summary>
        /// ハードウェアキーボード：入力禁止文字列設定
        /// </summary>
        /// <param name="filterCodes"></param>
        public void SetKeyFilterCode(string[] filterCodes)
        {
            if (filterCodes == null) return;
            for (int count = 0; count < filterCodes.Length; count++)
            {
                Array.Resize(ref _filterCodes, count + 1);
                _filterCodes[count] = filterCodes[count];
            }
        }
        /// <summary>
        /// エンター・キーを押した
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DecimalTextBox_Enter(object sender, KeyPressEventArgs e)
        {

        }
    }
}
