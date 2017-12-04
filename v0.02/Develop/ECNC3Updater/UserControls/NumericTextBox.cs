using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ECNC3.Views
{
	/// <summary>数値入力専用テキストボックス</summary>
	public partial class NumericTextBox : TextBox
	{
		/// <summary>コンストラクタ</summary>
		public NumericTextBox()
		{
			InitializeComponent();
			Validating += NumericTextBox_Validating;
			KeyPress += NumericTextBox_KeyPress;
			Enter += NumericTextBox_Enter;

            _defaultBackColor = Models.FileUIStyleTable.DefaultBackColor;
            _defaultForeColor = Models.FileUIStyleTable.DefaultForeColor;
            _enabledBackColor = Models.FileUIStyleTable.EnabledBackColor;
            _enabledForeColor = Models.FileUIStyleTable.EnabledForeColor;

            BackColor = _defaultBackColor;
            ForeColor = _defaultForeColor;
        }

		/// <summary>フォーマットタイプ</summary>
		public enum FormatTypes
		{
			/// <summary>制限なし</summary>
			Free,
			/// <summary>ゼロ埋め整数2桁</summary>
			IntegerZeroPlace2,
			/// <summary>ゼロ埋め整数3桁</summary>
			IntegerZeroPlace3,
			/// <summary>整数1桁 符号なし</summary>
			Integer1,
			/// <summary>整数3桁 符号なし</summary>
			Integer3,
            /// <summary>整数5桁 符号なし</summary>
            Integer5,
			/// <summary>実数 小数点以上1桁／小数点以下2桁 符号なし</summary>
			DecimalUpper1Lower2,
			/// <summary>実数 小数点以上1桁／小数点以下3桁 符号なし</summary>
			DecimalUpper1Lower3,
			/// <summary>ゼロ埋め実数 小数点以上3桁／小数点以下3桁 符号あり</summary>
			SignZeroDecimalUpper3Lower3,//2017-02-07：追加：柏原
			/// <summary>実数 小数点以上3桁／小数点以下3桁 符号あり</summary>
			SignDecimalUpper3Lower3,//2017-02-10：追加：柏原
            /// <summary>実数 小数点以上3桁／小数点以下3桁 符号あり</summary>
            SignDecimalUpper3Lower4,//2017-02-03：追加：柏原
            /// <summary>実数 小数点以上5桁／小数点以下3桁 符号あり</summary>
            SignDecimalUpper5Lower3,
			/// <summary>整数10桁 符号あり</summary>//2017-03-03：追加：柏原
			SignLong10,
		}
        private Color _defaultBackColor;
        private Color _defaultForeColor;
        private Color _enabledBackColor;
        private Color _enabledForeColor;

        private FormatTypes _formatType = FormatTypes.Free;
		/// <summary>入力許可下限値</summary>
		public decimal UpperLimit { get; set; } = 0;
		/// <summary>入力許可上限値</summary>
		public decimal LowerLimit { get; set; } = 99999999;
		/// <summary>少数点以上 桁数</summary>
		private int UpperDigit { get; set; } = 8;
		/// <summary>少数点以下 桁数</summary>
		private int LowerDigit { get; set; } = 0;
		/// <summary>入力操作時の倍率</summary>
		public decimal InputDataRatio { get; set; } = 1;
		/// <summary>書式</summary>
		public FormatTypes FormatType
		{
			get
			{
				return _formatType;
			}
			set
			{
				switch( value ) {
					case FormatTypes.Integer1:
						MaxLength = 1;
						UpperDigit = 1;
						LowerDigit = 0;
						LowerLimit = 0;
						UpperLimit = 9;
						break;
					case FormatTypes.Integer3:
						MaxLength = 3;
						UpperDigit = 3;
						LowerDigit = 0;
						LowerLimit = 0;
						UpperLimit = 999;
						break;

                    case FormatTypes.Integer5:
                        MaxLength = 5;
                        UpperDigit = 5;
                        LowerDigit = 0;
                        LowerLimit = 0;
                        UpperLimit = 99999;
                        break;
                    case FormatTypes.IntegerZeroPlace2:
						MaxLength = 2;
						UpperDigit = 2;
						LowerDigit = 0;
						LowerLimit = 0;
						UpperLimit = 99;
						break;
					case FormatTypes.IntegerZeroPlace3:
						MaxLength = 3;
						UpperDigit = 3;
						LowerDigit = 0;
						LowerLimit = 0;
						UpperLimit = 999;
						break;
					case FormatTypes.DecimalUpper1Lower2:
						MaxLength = 4;
						UpperDigit = 1;
						LowerDigit = 2;
						LowerLimit = 0.00m;
						UpperLimit = 9.99m;
						break;
					case FormatTypes.DecimalUpper1Lower3:
						MaxLength = 5;
						UpperDigit = 1;
						LowerDigit = 3;
						LowerLimit = 0.000m;
						UpperLimit = 9.999m;
						break;
					case FormatTypes.SignZeroDecimalUpper3Lower3://2017-02-07：追加：柏原
						MaxLength = 7;
						UpperDigit = 3;
						LowerDigit = 3;
						LowerLimit = -999.999m;
						UpperLimit = 999.999m;
						break;
                    case FormatTypes.SignDecimalUpper3Lower3://2017-02-10：追加：柏原
                        MaxLength = 7;
                        UpperDigit = 3;
                        LowerDigit = 3;
                        LowerLimit = -999.999m;
                        UpperLimit = 999.999m;
                        break;
                    case FormatTypes.SignDecimalUpper3Lower4://2017-02-03：追加：柏原
						MaxLength = 8;
						UpperDigit = 3;
						LowerDigit = 4;
						LowerLimit = -999.9999m;
						UpperLimit = 999.9999m;
						break;
					case FormatTypes.SignDecimalUpper5Lower3:
						MaxLength = 10;
						UpperDigit = 5;
						LowerDigit = 3;
						LowerLimit = -99999.999m;
						UpperLimit = 99999.999m;
						break;
					case FormatTypes.SignLong10:
						MaxLength = 10;
						UpperDigit = 10;
						LowerDigit = 0;
						LowerLimit = -9999999999;
						UpperLimit = 9999999999;
						break;
					default:
						break;
				}
				_formatType = value;
			}
		}
		/// <summary>実数値判定</summary>
		public bool IsDecimal
		{
			get { return ( 0 != LowerDigit ) ? true : false; }
			//			get { return ( ( FormatTypes.DecimalUpper1Lower2 == _formatType ) || ( FormatTypes.DecimalUpper1Lower3 == _formatType ) ||
			//					( FormatTypes.SignDecimalUpper5Lower3 == _formatType ) ); }
		}
		/// <summary>整数値判定</summary>
		public bool IsInteger
		{
			get { return ( 0 == LowerDigit ) ? true : false; }
			//get			{ return ( ( FormatTypes.IntegerZeroPlace2 == _formatType ) || ( FormatTypes.IntegerZeroPlace3 == _formatType ) ||
			//		( FormatTypes.Integer1 == _formatType ) || ( FormatTypes.Integer3 == _formatType ) ); }
		}
		/// <summary>文字列設定</summary>
		public override string Text
		{
			get
			{
				return RawText;
			}
			set
			{
				int tempInt;
				decimal tempDec;
				if( true == IsInteger ) {
					//	整数
					if( false == int.TryParse( value, out tempInt ) ) {
						tempInt = 0;	//	数値変換できない場合は、0を強制する。
					}
					Value = tempInt;
				} else if( true== IsDecimal ) {
					//	実数
					if( true == decimal.TryParse( value, out tempDec ) ) {
						Value = tempDec;
					} else {
						if( true == int.TryParse( value, out tempInt ) ) {
							tempInt = 0;   //	数値変換できない場合は、0を強制する。
						}
						Value = tempInt;
					}
				} else {
					RawText = value;
				}
			}
		}
		/// <summary>値によるテキスト設定</summary>
		public decimal Value
		{
			get
			{
				decimal temp = 0;
				if( true == TryTextToValue( RawText, out temp ) ) {
					return temp;
				}
				return 0;
			}
			set
			{
				string temp = string.Empty;
				if( true == TryValueToText( value, out temp ) ) {
					RawText = temp;
				}
			}
		}

		/// <summary>既定コントロールに対する直接的な設定</summary>
		public string RawText
		{
			get { return base.Text; }
			set { base.Text = value; }
		}

		/// <summary>キー押下イベント</summary>
		/// <param name="sender">イベント送信元オブジェクト</param>
		/// <param name="e">イベントパラメータ</param>
		private void NumericTextBox_KeyPress( object sender, KeyPressEventArgs e )
		{
			NumericTextBox target = sender as NumericTextBox;
            e.Handled = DigitAlign(e.KeyChar);
			//while( true ) {
			//	if( false == Char.IsDigit( e.KeyChar ) ) {
			//		if( ( FormatTypes.DecimalUpper1Lower2 == FormatType ) || ( FormatTypes.DecimalUpper1Lower3 == FormatType ) ) {
			//			if( true == RawText.Contains( "." ) ) {
			//				if( '.' == e.KeyChar ) {
			//					break;
			//				}
			//			} else {
			//				if( '.' != e.KeyChar ) {
			//					break;
			//				}
			//			}
			//		}
			//		break;
			//	}
			//	if( MaxLength < ( TextLength - SelectionLength + 1 ) ) {
			//		break;
			//	}
			//	return;
			//}
			//e.Handled = true;
		}
        /// <summary>
        /// 桁数判定処理
        /// </summary>
        /// <param name="_keychar"></param>
        /// <returns>
        /// true = 入力不可(終了)
        /// false = 入力可能
        /// </returns>
        private bool DigitAlign(char _keychar)
        {
            while (true)
            {
                if (false == Char.IsDigit(_keychar))
                {
                    if ((FormatTypes.DecimalUpper1Lower2 == FormatType) || (FormatTypes.DecimalUpper1Lower3 == FormatType))
                    {
                        if (true == RawText.Contains("."))
                        {
                            if ('.' == _keychar)
                            {
                                break;
                            }
                        }
                        else
                        {
                            if ('.' != _keychar)
                            {
                                break;
                            }
                        }
                    }
                    break;
                }
                if (MaxLength < (TextLength - SelectionLength + 1))
                {
                    break;
                }
                return false;
            }
            return true;
        }
        /// <summary>編集検証イベント</summary>
        /// <param name="sender">イベント送信元オブジェクト</param>
        /// <param name="e">イベントパラメータ</param>
        private void NumericTextBox_Validating( object sender, CancelEventArgs e )
		{
			NumericTextBox target = sender as NumericTextBox;
			//	書式チェック


		}

		private void NumericTextBox_Enter( object sender, EventArgs e )
		{
			SelectAll();
		}

		/// <summary>値→文字列変換</summary>
		/// <param name="source">変換元</param>
		/// <param name="result">変換結果</param>
		/// <returns>実行結果
		///		<list type="bullet" >
		///			<item>true=成功</item>
		///			<item>false=失敗</item>
		///		</list>
		/// </returns>
		private bool TryValueToText( decimal source, out string result )
		{
			result = string.Empty;
			if( true == IsInteger ) {
				//	整数
				result = ( FormatTypes.IntegerZeroPlace2 == FormatType ) ? $"{(int)source:d2}" :
							( ( FormatTypes.IntegerZeroPlace3 == FormatType ) ? $"{(int)source:d3}" : $"{(int)source}" );
			} else if( true == IsDecimal ) {
				//	実数
				result =
					( FormatTypes.DecimalUpper1Lower2 == FormatType ) ? $"{source:f2}" :
					( FormatTypes.SignZeroDecimalUpper3Lower3 == FormatType ) ? $"{source:000.000}"://2017-02-07追加：柏原
                    (FormatTypes.SignDecimalUpper3Lower3 == FormatType) ? $"{source:f3}" :      //2017-02-07追加：柏原
                    (FormatTypes.SignDecimalUpper3Lower4 == FormatType) ? $"{source:f4}" :		//2017-02-07追加：柏原
                    (( FormatTypes.DecimalUpper1Lower3 == FormatType ) ? $"{source:f3}" : $"{source}" );
			} else {
				result = $"{source}";
			}
			return true;
		}
		/// <summary>文字列→値変換</summary>
		/// <param name="source">変換元</param>
		/// <param name="result">変換結果</param>
		/// <returns>実行結果
		///		<list type="bullet" >
		///			<item>true=成功</item>
		///			<item>false=失敗</item>
		///		</list>
		/// </returns>
		private bool TryTextToValue( string source, out decimal result )
		{
			result = 0;
			while( true ) {
				int tempInt;
				decimal tempDec;
				if( true == IsInteger ) {
					//	整数
					if( false == int.TryParse( source, out tempInt ) ) {
						break;
					}
					result = tempInt;
				} else if( true == IsDecimal ) {
					//	実数
					if( true == decimal.TryParse( source, out tempDec ) ) {
						result = tempDec;
					} else {
						if( true == int.TryParse( source, out tempInt ) ) {
							break;
						}
					}
				} else {
					break;
				}
				return true;
			}
			return false;
		}

		/// <summary>文字入力</summary>
		/// <param name="input">入力文字列</param>
		public void InputText( string input )
		{
			input = input.Trim();
			int start = base.SelectionStart;
			string source = RawText;
			while( false == string.IsNullOrEmpty( input ) ) {
				if( 0 == string.Compare( "BS", input, false ) ) {
					//	BS
					if( true == string.IsNullOrEmpty( source ) ) {
						break;
					}
					string insert = string.Empty;
					if( 0 < base.SelectionLength ) {
						//	選択状態にある部分を削除した上で追加。
						string text = source.Remove( start, base.SelectionLength );
						if( true == IsDecimal ) {
							//	入力桁数をチェック
							if( false == CheckDigit( text ) ) {
								break;
							}
						}
						RawText = text;
					} else {
						if( 1 > start ) {
							break;  //	左端からの削除はできない。
						}
						string text = source.Remove( start - 1, 1 );
						if( true == IsDecimal ) {
							//	入力桁数をチェック
							if( false == CheckDigit( text ) ) {
								break;
							}
						}
						RawText = text;
						base.Select( start + input.Length, 0 );
					}
				} else {
					if( 0 == string.Compare( ".", input, false ) ) {
						//	ピリオドの入力
						if( 0 == start ) {
							input = "0.";
						} else {
							if( true == RawText.Contains( "." ) ) {
								break;
							}
							if( 0 == string.Compare( "-", RawText ) ) {
								input = "0.";
							}
						}
					} else if( 0 == string.Compare( "-", input, false ) ) {
						//	マイナスの入力
						if( true == RawText.Contains( "-" ) ) {
							break;
						}
						if( 0 != start ) {
							break;
						}
					}
					//	その他
					string remove = string.Empty;
					string insert = string.Empty;
					if( 0 < base.SelectionLength ) {
						remove = source.Remove( start, base.SelectionLength );
					} else {
						remove = source;
					}
					if( MaxLength > remove.Length ) {
						string text = remove.Insert( start, input );
						if( true == IsDecimal ) {
							//	入力桁数をチェック
							if( false == CheckDigit( text ) ) {
								break;
							}
							decimal update;
							if( false == decimal.TryParse( text, out update ) ) {
								break;
							}
							if( LowerLimit > update ) {
								if( true == NeedCancel( LowerLimit, true ) ) {
									break;
								}
							} else if( UpperLimit < update ) {
								if( true == NeedCancel( UpperLimit, true ) ) {
									break;
								}
							}
						} else {
							int update;
							if( false == int.TryParse( text, out update ) ) {
								break;
							}
							if( LowerLimit > update ) {
								if( true == NeedCancel( LowerLimit, true ) ) {
									break;
								}
							} else if( UpperLimit < update ) {
								if( true == NeedCancel( UpperLimit, true ) ) {
									break;
								}
							}
						}
						RawText = text;
						base.Select( start + input.Length, 0 );
					}
				}
				break;
			}
		}
		/// <summary>入力桁数チェック</summary>
		/// <param name="source">チェック対象の文字列</param>
		/// <returns>
		///		<list type="bullet" >
		///			<item>true=正当</item>
		///			<item>false=不当</item>
		///		</list>
		/// </returns>
		private bool CheckDigit( string source )
		{
			string text = string.Empty;
			if( true == source.Contains( "-" ) ) {
				text = source.Remove( 0, 1 );
			} else {
				text = source;
			}
			while( true ) {
				if( true == text.Contains( "." ) ) {
					if( UpperDigit < ( text.IndexOf( '.' ) - 1 ) ) {
						break;
					}
					if( LowerDigit < ( text.Length - ( text.LastIndexOf( '.' ) + 1 ) ) ) {
						break;
					}
				} else {
					if( UpperDigit < text.Length ) {
						break;
					}
				}
				return true;
			}
			return false;
		}
		/// <summary>書式化</summary>
		public void Formating()
		{
			string soutce = RawText;
			this.Text = soutce;
		}
		
		/// <summary>方向キーによる入力</summary>
		/// <param name="inputKey">入力キー</param>
		public void InputDirectionKey( Keys inputKey )
		{
			string source = RawText;
			//int length = source.Length;
			int posStart = base.SelectionStart;		//	選択開始位置
			int posLength = base.SelectionLength;	//	選択長
			int currentCursor = posStart + posLength;	//	入力カーソル位置
			decimal value;
			if( true == decimal.TryParse( source, out value ) ) {
				if( 0 < SelectionLength ) {
					//	選択部をインクリメント
					decimal rate = (decimal)Math.Pow( 10, GetDigit( source, currentCursor ) );
					UpdateData( inputKey, rate );
					Select( posStart, posLength );
				} else {
					//	カーソル左をインクリメント
					if( 0 < posStart ) {
						decimal rate = (decimal)Math.Pow( 10, GetDigit( source, currentCursor ) );
						UpdateData( inputKey, rate );
						SelectionStart = posStart;
					}
				}
			}
		}
		/// <summary>小数点を基準とした桁位置の取得</summary>
		/// <param name="source">解析対象の文字列</param>
		/// <param name="posTail">現在のカーソル位置</param>
		/// <returns>
		///		<list type="bullet" >
		///			<item>正数=小数点以上桁位置</item>
		///			<item>負数=小数点以下桁位置</item>
		///		</list>
		/// </returns>
		private int GetDigit( string source, int posTail )
		{
			int lenght = source.Length;
			int point = source.IndexOf( "." );
			if( 0 < point ) {
				//	小数点を取り除いた桁を判定しなければならない。
				if( point < posTail ) {
					++point;
				}
				return -( posTail - point );
			} else {
				return lenght - posTail;
			}
		}
		/// <summary>設定値の更新</summary>
		/// <param name="inputKey">入力キー</param>
		/// <param name="value">加算／減算値</param>
		public bool UpdateData( Keys inputKey, decimal value, bool overIsCancel = true )
		{
			if( 0 < InputDataRatio ) {
				value *= InputDataRatio;
			}
			while( true ) {
				string now = Text;
				if( ( Keys.Up == inputKey ) || ( Keys.Right == inputKey ) ) {
					decimal update = Value + value;
					if( UpperLimit < update ) {
						if( true == NeedCancel( UpperLimit, overIsCancel ) ) {
							break;
						}
					} else if( LowerLimit > update ) {
						if( true == NeedCancel( LowerLimit, overIsCancel ) ) {
							break;
						}
					} else {
						Value = update;
					}
				} else if( ( Keys.Down == inputKey ) || ( Keys.Left == inputKey ) ) {
					decimal update = Value - value;
					if( LowerLimit > update ) {
						if( true == NeedCancel( LowerLimit, overIsCancel ) ) {
							break;
						}
					} else if( UpperLimit < update ) {
						if( true == NeedCancel( UpperLimit, overIsCancel ) ) {
							break;
						}
					} else {
						Value = update;
					}
				} else {
					break;
				}
				return true;
			}
			return false;
		}
		/// <summary>相対値入力</summary>
		/// <param name="value">入力値</param>
		/// <remarks>
		/// 現在値に対して相対的に値を設定します。
		/// 加算したい場合は、正数を減算したい場合は、負数を設定してください。
		/// </remarks>
		public bool InputRelative( decimal value )
		{
			bool result = false;
			if( 0 != value ) {
				if( 0 > value ) {
					result = UpdateData( Keys.Down, Math.Abs( value ), false );
				} else {
					result = UpdateData( Keys.Up, value, false );
				}
			}
			return result;
		}
		/// <summary>キャンセル判定</summary>
		/// <param name="limitValue">入力の上限もしくh下限値</param>
		/// <param name="overIsCancel">入力値の超過に対する措置
		///		<list type="bullet" >
		///			<item>true=異常であり、入力をキャンセルする。</item>
		///			<item>false=異常ではあるが、制限値を設定して入力はキャンセルしない。。</item>
		///		</list>
		/// </param>
		/// <returns>
		///		<list type="bullet" >
		///			<item>true=要キャンセル</item>
		///			<item>false=キャンセル不要</item>
		///		</list>
		/// </returns>
		/// <remarks>
		/// 異常設定された値がキャンセルすべきであるかを判定します。
		/// 当関数を呼び出された時点で不適切な入力が行われていることを前提としています。
		/// </remarks>
		private bool NeedCancel( decimal limitValue, bool overIsCancel )
		{
			//	現在状態の保持
			string now = Text;
			while( true ) {
				if( true == overIsCancel ) {
					break;	//	制限超過は、入力のキャンセルと扱う。
				}
				Value = limitValue;
				if( 0 == string.Compare( now, Text ) ) {
					break;	//	入力値に変化が見られない。
				}
				return false;
			}
			return true;

		}
        /// <summary>
        /// ボタンをランプとして使う場合の色切替
        /// </summary>
        /// <param name="LampOn"></param>
        public void SetLamp(bool LampOn)
        {
            if (LampOn == true)
            {
                BackColor = _enabledBackColor;
                ForeColor = _enabledForeColor;
            }
            else
            {
                BackColor = _defaultBackColor;
                ForeColor = _defaultForeColor;
            }
        }

        public bool GetLamp()
        {
            if (BackColor == _defaultBackColor)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
