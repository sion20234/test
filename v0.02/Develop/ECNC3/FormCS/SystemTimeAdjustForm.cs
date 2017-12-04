///////////////////////////////////////////////////////////////////////////////////////////
//
// (1) システム名称 : ELENIX ECNCⅢ
// (2) ファイル名   : SystemTimeAdjustForm.cs
// (3) 概要         : システム時刻設定画面
// (4) 作成日       : 2016.07.19
// (5) 作成者       : 八野
// (6) 特記事項     : VisualStudio2015 C#
// (7) 更新履歴     : 2017-07-10追加：柏原
//
///////////////////////////////////////////////////////////////////////////////////////////

using System;

namespace ECNC3.Views
{
    /// <summary>
    /// システム時刻設定画面
    /// </summary>
    public partial class SystemTimeAdjustForm : ECNC3Form
    {
		#region<初期化>
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public SystemTimeAdjustForm()
        {
            InitializeComponent();
        }
		/// <summary>
		/// Formロード時
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SystemTimeAdjustForm_Load( object sender, EventArgs e )
		{
			DispNowTime();//現在時刻をコントロールに表示
		}
		/// <summary>
		/// 現在時刻をコントロールに表示
		/// </summary>
		private void DispNowTime()
		{
			DateTime dt = DateTime.Now;//現在時刻
			string stringYear = dt.ToString( "yyyy" );
			string stringMonth = dt.ToString( "MM" );
			string stringDay = dt.ToString( "dd" );
			string stringHour = dt.ToString( "HH" );
			string stringMinute = dt.ToString( "mm" );
			string stringSecond = dt.ToString( "ss" );
			//TextBoxExコントロールに設定
			textBox_Year.Text = stringYear;     //年
			textBox_Month.Text = stringMonth;	//月
			textBox_Day.Text = stringDay;       //日
			textBox_Hour.Text = stringHour;		//時
			textBox_Minute.Text = stringMinute; //分
			textBox_Second.Text = stringSecond; //秒
		}
		#endregion
		#region<終了時：閉じるボタン：クリック>
		/// <summary>
		/// 閉じるボタン：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_Back_Click( object sender, EventArgs e )
		{
			//このFormを閉じる
			this.Close();
		}
		#endregion
		#region<登録ボタン：クリック>
		/// <summary>
		/// 登録：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_Reg_Click( object sender, EventArgs e )
		{
			//編集値でシステム値に上書き
			if( textBox_Year.Text == "" ) textBox_Year.Text = "0"; //年
			if( textBox_Month.Text == "" ) textBox_Month.Text = "1"; //月
			if( textBox_Day.Text == "" ) textBox_Day.Text = "1"; //日
			if( textBox_Hour.Text == "" ) textBox_Hour.Text = "0"; //時
			if( textBox_Minute.Text == "" ) textBox_Minute.Text = "0"; //分
			if( textBox_Second.Text == "" ) textBox_Second.Text = "0"; //秒
			int intYear = int.Parse( textBox_Year.Text );   //年
			int intMonth = int.Parse( textBox_Month.Text ); //月
			int intDay = int.Parse( textBox_Day.Text ); //日
			int intHour = int.Parse( textBox_Hour.Text );   //時
			int intMinute = int.Parse( textBox_Minute.Text );   //分
			int intSecond = int.Parse( textBox_Second.Text ); //秒
			//システム時刻:変更
			DateTime dt = new DateTime(
				intYear, intMonth, intDay, intHour, intMinute, intSecond
			);
			Microsoft.VisualBasic.DateAndTime.Today = dt;       //日付設定
			Microsoft.VisualBasic.DateAndTime.TimeOfDay = dt;   //時刻設定
			this.Close();										//このFormを閉じる
		}
		#endregion
		#region<タイマー：ファイア>
		/// <summary>
		/// timeTimer：ファイア
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void timer1_Tick( object sender, EventArgs e )
		{
			DispNowTime();//ここにファイアさせるにはtimeTimerのEnableをtrueにします。
		}
		#endregion
		#region<年月日時分秒▲ボタン：クリック>
		/// <summary>
		/// 年：▲ボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_Year_Up_Click( object sender, EventArgs e )
		{
			int intYear = int.Parse( textBox_Year.Text );   //年
			intYear++;
			textBox_Year.Text = intYear.ToString();

			//存在する日付かチェック
			string stringDate = intYear.ToString() + "/" + textBox_Month.Text + "/" + textBox_Day.Text;   // チェックする日付を設定
			DateTime dt;
			if( DateTime.TryParse( stringDate, out dt ) ) {
			} else {
				//存在しない日付
				using( MessageDialog msg = new MessageDialog() ) {
					msg.Error( 5512, this );
					{
						intYear--;
						textBox_Year.Text = String.Format( "{0:D2}", intYear );
						return;//"入力範囲外" txt="この日付では設定できません、&#xD;&#xA;他の日付に変更して下さい。
					}
				}
			}
		}
		/// <summary>
		/// 月：▲ボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_Month_Up_Click( object sender, EventArgs e )
		{
			int intMonth = int.Parse( textBox_Month.Text );   //月
			intMonth++;
			if( intMonth > 12 ) intMonth = 12;
			textBox_Month.Text = String.Format( "{0:D2}", intMonth );

			//存在する日付かチェック
			string stringDate = textBox_Year.Text + "/" + intMonth.ToString() + "/" + textBox_Day.Text;   // チェックする日付を設定
			DateTime dt;
			if( DateTime.TryParse( stringDate, out dt ) ) {
			} else {
				//存在しない日付
				using( MessageDialog msg = new MessageDialog() ) {
					msg.Error( 5512, this );
					{
						intMonth--;
						textBox_Month.Text = String.Format( "{0:D2}", intMonth );
						return;//"入力範囲外" txt="この日付では設定できません、&#xD;&#xA;他の日付に変更して下さい。
					}
				}
			}
		}
		/// <summary>
		/// 日：▲ボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_Day_Up_Click( object sender, EventArgs e )
		{
			int intDay = int.Parse( textBox_Day.Text );   //日
			intDay++;
			textBox_Day.Text = String.Format( "{0:D2}", intDay );

			//存在する日付かチェック
			string stringDate = textBox_Year.Text + "/" + textBox_Month.Text + "/" + intDay.ToString();   // チェックする日付を設定
			DateTime dt;
			if( DateTime.TryParse( stringDate, out dt ) ) {
			} else {
				//存在しない日付
				using( MessageDialog msg = new MessageDialog() ) {
					msg.Error( 5512, this );
					{
						intDay--;
						textBox_Day.Text = String.Format( "{0:D2}", intDay );
						return;//"入力範囲外" txt="この日付では設定できません、&#xD;&#xA;他の日付に変更して下さい。
					}
				}
			}
		}
		/// <summary>
		/// 時：▲ボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_Hour_Up_Click( object sender, EventArgs e )
		{
			int intHour = int.Parse( textBox_Hour.Text );   //時
			intHour++;
			if( intHour > 23 ) intHour = 23;
			textBox_Hour.Text = String.Format( "{0:D2}", intHour );
		}
		/// <summary>
		/// 分：▲ボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_Minute_Up_Click( object sender, EventArgs e )
		{
			int intMinute = int.Parse( textBox_Minute.Text );   //分
			intMinute++;
			if( intMinute > 59 ) intMinute = 59;
			textBox_Minute.Text = String.Format( "{0:D2}", intMinute );
		}
		/// <summary>
		/// 秒：▲ボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_Second_Up_Click( object sender, EventArgs e )
		{
			int intSecond = int.Parse( textBox_Second.Text );   //秒
			intSecond++;
			if( intSecond > 59 ) intSecond = 59;
			textBox_Second.Text = String.Format( "{0:D2}", intSecond );
		}
		#endregion
		#region<年月日時分秒▼ボタン：クリック>
		/// <summary>
		/// 年：▼ボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_Year_Dn_Click( object sender, EventArgs e )
		{
			int intYear = int.Parse( textBox_Year.Text );   //年
			intYear--;
			textBox_Year.Text = intYear.ToString();

			//存在する日付かチェック
			string stringDate = intYear.ToString() + "/" + textBox_Month.Text + "/" + textBox_Day.Text;   // チェックする日付を設定
			DateTime dt;
			if( DateTime.TryParse( stringDate, out dt ) ) {
			} else {
				//存在しない日付
				using( MessageDialog msg = new MessageDialog() ) {
					msg.Error( 5512, this );
					{
						intYear++;
						textBox_Year.Text = String.Format( "{0:D2}", intYear );
						return;//"入力範囲外" txt="この日付では設定できません、&#xD;&#xA;他の日付に変更して下さい。
					}
				}
			}
		}
		/// <summary>
		/// 月：▼ボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_Month_Dn_Click( object sender, EventArgs e )
		{
			int intMonth = int.Parse( textBox_Month.Text );   //月
			intMonth--;
			if( intMonth < 1 ) intMonth = 1;
			string stringDate = textBox_Year.Text + "/" + intMonth.ToString() + "/" + textBox_Day.Text;   // チェックする日付を設定
			textBox_Month.Text = String.Format( "{0:D2}", intMonth );

			//存在する日付かチェック
			DateTime dt;
			if( DateTime.TryParse( stringDate, out dt ) ) {
			} else {
				//存在しない日付
				using( MessageDialog msg = new MessageDialog() ) {
					msg.Error( 5512, this );
					{
						intMonth++;
						textBox_Month.Text = String.Format( "{0:D2}", intMonth );
						return;//"入力範囲外" txt="この日付では設定できません、&#xD;&#xA;他の日付に変更して下さい。
					}
				}
			}
		}
		/// <summary>
		/// 日：▼ボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_Day_Dn_Click( object sender, EventArgs e )
		{
			int intDay = int.Parse( textBox_Day.Text );   //日
			intDay--;
			if( intDay < 1 ) intDay = 1;
			textBox_Day.Text = String.Format( "{0:D2}", intDay );

			//存在する日付かチェック
			string stringDate = textBox_Year.Text + "/" + textBox_Month.Text + "/" + intDay.ToString();   // チェックする日付を設定
			DateTime dt;
			if( DateTime.TryParse( stringDate, out dt ) ) {
			} else {
				//存在しない日付
				using( MessageDialog msg = new MessageDialog() ) {
					msg.Error( 5512, this );
					{
						intDay++;
						textBox_Day.Text = String.Format( "{0:D2}", intDay );
						return;//"入力範囲外" txt="この日付では設定できません、&#xD;&#xA;他の日付に変更して下さい。
					}
				}
			}
		}
		/// <summary>
		/// 時：▼ボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_Hour_Dn_Click( object sender, EventArgs e )
		{
			int intHour = int.Parse( textBox_Hour.Text );   //時
			intHour--;
			if( intHour < 0 ) intHour = 0;
			textBox_Hour.Text = String.Format( "{0:D2}", intHour );
		}
		/// <summary>
		/// 分：▼ボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_Minute_Dn_Click( object sender, EventArgs e )
		{
			int intMinute = int.Parse( textBox_Minute.Text );   //分
			intMinute--;
			if( intMinute < 0 ) intMinute = 0;
			textBox_Minute.Text = String.Format( "{0:D2}", intMinute );
		}
		/// <summary>
		/// 秒：▼ボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_Second_Dn_Click( object sender, EventArgs e )
		{
			int intSecond = int.Parse( textBox_Second.Text );   //秒
			intSecond--;
			if( intSecond < 0 ) intSecond = 0;
			textBox_Second.Text = String.Format( "{0:D2}", intSecond );
		}
		#endregion
	}
}
