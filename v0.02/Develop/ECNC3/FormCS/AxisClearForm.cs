///////////////////////////////////////////////////////////////////////////////////////////
//
// (1) システム名称 : ELENIX ECNCⅢ
// (2) ファイル名   : AxisClearForm.cs
// (3) 概要         : 機械/ワーク座標設定画面
// (4) 作成日       : 2016.07.19
// (5) 作成者       : 八野
// (6) 特記事項     : VisualStudio2015 C#
// (7) 更新履歴     : 2017-03-23：柏原
//
///////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ECNC3.Enumeration;
using ECNC3.Models;

namespace ECNC3.Views
{
    /// <summary>
    /// 機械/ワーク座標設定画面
    /// </summary>
    public partial class AxisClearForm : ECNC3Form
    {
        #region <region<初期化>region>
        /// <summary>
        /// 現在の機械座標値
        /// </summary>
        private Models.StructureAxisCoordinate WorkPosition = new Models.StructureAxisCoordinate();
        /// <summary>ポップアップテンキー</summary>
        private TenKeyDialog _popupTenkey = new TenKeyDialog("", 0, 0, 0);//初回インスタンスを作っておく
       // private decimal decimalInitVal = 0.0000m;   //初期化値
        private int _cordinateSystemType = 0;       //0=機械座標、1=ワーク座標
        private bool _tenkeyEdit = false;           //fales=編集不可、true=編集可
        int intAxisCount = 7;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="cordinateSystemType">0=機械座標、1=ワーク座標</param>
        /// <param name="WorkPos"></param>
        /// <param name="tenkeyEdit">fales=編集不可、true=編集可</param>
        public AxisClearForm(int cordinateSystemType, Models.StructureAxisCoordinate WorkPos, bool tenkeyEdit = false)
        {
            //コンポーネント初期化
            InitializeComponent();
			//機械座標/ワーク座標
			_cordinateSystemType = cordinateSystemType;
            _tenkeyEdit = tenkeyEdit;
            //座標値取得
            WorkPosition.AxisX = WorkPos.AxisX;
            WorkPosition.AxisY = WorkPos.AxisY;
            WorkPosition.AxisW = WorkPos.AxisW;
            WorkPosition.AxisZ = WorkPos.AxisZ;
            WorkPosition.AxisA = WorkPos.AxisA;
            WorkPosition.AxisB = WorkPos.AxisB;
            WorkPosition.AxisC = WorkPos.AxisC;
            WorkPosition.Axis8 = WorkPos.Axis8;
            WorkPosition.Axis9 = WorkPos.Axis9;

            Disposed += WorkSpinOpForm_Disposed;

            // 元のカーソルを保持
            Cursor preCursor = Cursor.Current;
            // カーソルを待機カーソルに変更
            Cursor.Current = Cursors.WaitCursor;
            //下3桁か4桁か取得：サブミクロン
            CncSys_AxisInfomation_XmlRead(ref _submicronFlg);
            int rowHeight = 60; //行高さ
            int hederFontSize = 20; //ヘッダーフォントサイズ
            int cellFontSize = 40; //セルフォントサイズ
                                   //dataGridViewEx：初期化
            dataGridViewEx1.Initialize(hederFontSize, rowHeight, true);//true=編集不可
                                                                       //dataGridViewEx：セルサイズ設定
            dataGridViewEx1.DefaultCellStyle.Font = new Font("Meiryo UI", cellFontSize, FontStyle.Bold);
            //dataGridViewEx：行数設定
            dataGridViewEx1.RowCount = intAxisCount + 1;//XYWZABC+空データ(最終行高さの不具合対策)
                                             //dataGridViewEx1：各種設定
            dataGridViewEx1.MultiSelect = true;                 //複数選択＝可
                                                                //最終行の行追加は非表示
            dataGridViewEx1.AllowUserToAddRows = false;         //以前はReadOnly=Trueだけで
                                                                //問題有りませんでしたが、
                                                                //現在はここをfalseにしないと
                                                                //最終行に追加行が表示する。
                                                                //dataGridViewEx1：各列ソート：不可
            foreach (DataGridViewRow row in dataGridViewEx1.Rows)
            {
                if (row.Index >= intAxisCount)
                {
                    dataGridViewEx1.Rows[row.Index].Visible = false;
                }
            }
            foreach (DataGridViewColumn item in dataGridViewEx1.Columns)
            {
                item.SortMode = DataGridViewColumnSortMode.NotSortable;
            };
            //dataGridViewEx1：セルに代入
            int axisNameIndex = 1;//軸名称
            int axisValIndex = 2;//軸値
            string[] axisName = new string[] { "X", "Y", "W", "Z", "A", "B", "C", "I", "*", "" };
            int[] axisVal = new int[] { WorkPos.AxisX, WorkPos.AxisY, WorkPos.AxisW, WorkPos.AxisZ, WorkPos.AxisA, WorkPos.AxisB, WorkPos.AxisC, WorkPos.Axis8, WorkPos.Axis9, 0 };

			string textValue = "##0.0000";
			if( _submicronFlg ) textValue = "##0.0000";
			else                textValue = "##0.000";

			for( int count = 0; count < dataGridViewEx1.Rows.Count; count++)
            {
                //セル設定
                dataGridViewEx1.Rows[count].Cells[axisNameIndex].Value = axisName[count];
                //decimal decimalVal = axisVal[count] / 1000;//整数から実数に戻す
                //dataGridViewEx1.Rows[count].Cells[axisValIndex].Value = decimalVal.ToString("##0'.'0000");
                //20170329 hachino Rep↑↑to↓↓ 書式指定と整数⇒実数変換修正。
                decimal decimalVal = Convert.ToDecimal(axisVal[count]) / 1000;//整数から実数に戻す
                dataGridViewEx1.Rows[count].Cells[axisValIndex].Value = decimalVal.ToString( textValue );
                //RepEnd
                //色設定
                dataGridViewEx1.Rows[count].DefaultCellStyle.ForeColor = FileUIStyleTable.DefaultForeColor;
                dataGridViewEx1.Rows[count].DefaultCellStyle.BackColor = FileUIStyleTable.DefaultBackColor;
                //編集不可の場合、選択した時の色を、行の背景色と同じにする
                if (tenkeyEdit == false) dataGridViewEx1.Rows[count].DefaultCellStyle.SelectionBackColor = FileUIStyleTable.DefaultBackColor;
            }
            //dataGridViewEx1：セルの文字表示位置
            dataGridViewEx1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewEx1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewEx1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;//左寄せ

            //ボタン使用不可設定
            buttonEx_Clear.Enabled = false;     //選択クリアボタン：無効
            buttonEx_SelectOff.Enabled = false; //選択解除ボタン：無効
                                                //カーソルを元に戻す
            Cursor.Current = preCursor;
        }
		/// <summary>
		/// ロード時
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AxisClearForm_Load(object sender, EventArgs e)
        {
            //機械、ワーク別タイトル表示
            switch (_cordinateSystemType)
            {
                case 0://機械座標
                    label_Title.Text = "MACHINE";
                    button_GridTitle.Text = "機械座標";
                    break;
                case 1://ワーク座標
                    label_Title.Text = "WORK";
                    button_GridTitle.Text = "ワーク座標";
                    break;
                default:
                    break;
            }

		}
		private bool _submicronFlg = false;
		/// <summary>
		///  CncSys.xml：軸関係情報設定<AxisInfomation>からサブミクロン(下3か4桁)表示を取得
		/// </summary>
		/// <param name="submicronFlg"></param>
		private void CncSys_AxisInfomation_XmlRead( ref bool submicronFlg )
		{
			int intDigit = 0;
			try {
				using( FileSettings fs = new FileSettings() ) {
					//ファイル読み込み
					fs.Read();
                    //サブミクロン表示(submicron)：取得
                    intDigit = fs.AttrValue("Root/AxisInfomation/Position ", "digit");
                    intAxisCount = fs.AttrValue("Root/AxisInfomation/EnableAxis ", "count");
                    if ( intDigit == 4 ) {
						submicronFlg = true;
					} else {
						submicronFlg = false;
					}
				}
			} catch( Exception exc ) {
				//例外処理
				MessageBox.Show( exc.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error );
			}
		}

		#endregion
		#region <region<終了時>region>
		/// <summary>
		/// 閉じるボタン：クリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonEx_Back_Click(object sender, EventArgs e)
        {
            //this.Close();
            _notifyReturn?.Invoke();
        }
        /// <summary>
        /// Disposed時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkSpinOpForm_Disposed(object sender, EventArgs e)
        {
            if (null != WorkPosition)
            {
                WorkPosition.Dispose();
                WorkPosition = null;
            }
            if (null != _notifyReturn)
            {
                _notifyReturn = null;
            }
            if (null != _popupTenkey)
            {//ポップアップテンキー
                _popupTenkey = null;
            }
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
        #endregion
        #region <region<ポップアップテンキー>region>
        /// <summary>
        /// ポップアップテンキー：表示
        /// </summary>
        /// <param name="val"></param>
        /// <returns>false=上下ポップアップを表示</returns>
        private bool popupTenkeyOn(object val, string popupTitle = "")
        {
            if (_popupTenkey != null)
            {
                _popupTenkey.Close();   //画面を閉じる
                _popupTenkey = null;    //null初期化
            }
            //クリックしたコントロールの値を取得
            string changeVal = val.ToString();          //編集値
            Decimal lowerLimitDec = (decimal)-99999.999;//最小値
            Decimal upperLimitDec = (decimal)99999.999; //最大値
                                                        //フォーマットタイプ
            NumericTextBox.FormatTypes formatType = NumericTextBox.FormatTypes.SignDecimalUpper5Lower3;
            //ポップアップTenKey表示
            _popupTenkey = new TenKeyDialog(changeVal, formatType, lowerLimitDec, upperLimitDec, true, true, _submicronFlg, "axisClear");
            _popupTenkey.NotifyReturnOk = popupTenkey_OnNotifyReturnOk; //イベント通知：OK
            _popupTenkey.Text = popupTitle;                             //テンキータイトル表示
            _popupTenkey.ShowDialog(this);                              //画面を開く
            return true;
        }
        /// <summary>
        /// ポップアップテンキー：「OK」ボタンを押した時、入力値をこのクラスに反映
        /// </summary>
        private void popupTenkey_OnNotifyReturnOk()
        {
            string retVal = _popupTenkey._tenkeyValReturn;//ポップアップテンキーで編集された値
            decimal retDecimal = decimal.Parse(retVal);
            //クリックセルに値をセット
            dataGridViewEx1[2, _rowIndex].Value = retDecimal.ToString("F4");//ポップアップキーボード値をセット
            string tmpString = dataGridViewEx1[2, _rowIndex].Value.ToString();
            int intWorkPosition = int.Parse(tmpString.Replace(".", "")) / 10;//小数点を削除し1000倍
            //switch( _rowIndex ) {
            //	case 0: WorkPosition.AxisX = intWorkPosition; break;
            //	case 1: WorkPosition.AxisX = intWorkPosition; break;
            //	case 2: WorkPosition.AxisY = intWorkPosition; break;
            //	case 3: WorkPosition.AxisW = intWorkPosition; break;
            //	case 4: WorkPosition.AxisZ = intWorkPosition; break;
            //	case 5: WorkPosition.AxisA = intWorkPosition; break;
            //	case 6: WorkPosition.AxisB = intWorkPosition; break;
            //	case 7: WorkPosition.AxisC = intWorkPosition; break;
            //	default: break;
            //}
            //20170329 hachino Rep↑↑to↓↓
            switch (_cordinateSystemType)
            {
                case 0: return;
                case 1:
                    using (Models.McIf.McDatWorkOrigin mcWorkOrg = new Models.McIf.McDatWorkOrigin())
                    using (Models.McIf.McReqWorkPositionChange workOrg = new Models.McIf.McReqWorkPositionChange())
                    {
                        mcWorkOrg.Read();
                        workOrg.WorkPosition = new StructureAxisCoordinate();
                        //ワーク座標原点オフセット値計算
                        switch (_rowIndex)
                        {
                            //20増加する場合、入力は「20」オフセット値は「現オフセット値 - 20」
                            case 0:
                                workOrg.WorkPosition.AxisX = mcWorkOrg.Coordinate.AxisX - intWorkPosition;
                                workOrg.WorkPosition.EnableAxis = 1;
                                break;

                            case 1:
                                workOrg.WorkPosition.AxisY = mcWorkOrg.Coordinate.AxisY - intWorkPosition;
                                workOrg.WorkPosition.EnableAxis = 2;
                                break;

                            case 2:
                                workOrg.WorkPosition.AxisW = mcWorkOrg.Coordinate.AxisW - intWorkPosition;
                                workOrg.WorkPosition.EnableAxis = 4;
                                break;

                            case 3:
                                workOrg.WorkPosition.AxisZ = mcWorkOrg.Coordinate.AxisZ - intWorkPosition;
                                workOrg.WorkPosition.EnableAxis = 8;
                                break;

                            case 4:
                                workOrg.WorkPosition.AxisA = mcWorkOrg.Coordinate.AxisA - intWorkPosition;
                                workOrg.WorkPosition.EnableAxis = 16;
                                break;

                            case 5:
                                workOrg.WorkPosition.AxisB = mcWorkOrg.Coordinate.AxisB - intWorkPosition;
                                workOrg.WorkPosition.EnableAxis = 32;
                                break;

                            case 6:
                                workOrg.WorkPosition.AxisC = mcWorkOrg.Coordinate.AxisC - intWorkPosition;
                                workOrg.WorkPosition.EnableAxis = 64;
                                break;

                            default: break;
                        }
                        //オフセット値設定
                        if (_rowIndex != -1)
                        {
                            ResultCodes result = workOrg.Execute();
                            if (result != ResultCodes.Success)
                            {
                                return;
                            }
                            //リストへ表示
                            GetAxisPos();
                        }
                    }
                    break;
            }
        }
        #endregion
        #region <region<データグリッド：dataGridViewEx>region>
        private int _rowIndex = 0;
        /// <summary>
        /// dataGridViewExセル：マウス：ダウン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewEx1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = ((DataGridViewCellMouseEventArgs)e).RowIndex;  //行
            int colIndex = ((DataGridViewCellMouseEventArgs)e).ColumnIndex; //列
            if (rowIndex == -1) return;                                             //ヘッダークリック時、処理無し
                                                                                    //選択セル：非表示
            dataGridViewEx1.CurrentCell = null;
            try
            {
                //各列
                switch (colIndex)
                {
                    case 0://選択ボタン
                    case 1://軸名称
                           //セル上のボタン
                           //DGVEXはチェック無効なのでここでON/OFF※初回はnull
                        if (dataGridViewEx1[0, rowIndex].Value == null) dataGridViewEx1[0, rowIndex].Value = "";
                        if ((string)dataGridViewEx1[0, rowIndex].Value == "●")
                        {
                            //OFF
                            dataGridViewEx1[0, rowIndex].Value = "";
                        }
                        else
                        {
                            //ON
                            dataGridViewEx1[0, rowIndex].Value = "●";
                        }
                        break;
                    case 2://値
                        if (_tenkeyEdit == false) return;//false時はテンキー表示無し

                        _rowIndex = rowIndex;                                                    //編集行数記録
                        string popupTitle = dataGridViewEx1[1, rowIndex].Value.ToString() + "軸";//タイトル取得
                                                                                                //0.0000の最小桁を削り0.000にする
                        string stringTmp = dataGridViewEx1[2, rowIndex].Value.ToString();       //編集セル内容取得
                        int indexLastZero = stringTmp.LastIndexOf("0");
                        object objVal;
                        if (indexLastZero == stringTmp.Length - 1)
                        {
                            objVal = stringTmp.Remove(indexLastZero, 1);                     //最後の1桁を削除   
                        }
                        objVal = stringTmp;
                        popupTenkeyOn(objVal, popupTitle);
                        break;
                    default:
                        //エラー
                        break;
                }
            }
            catch(Exception ex)
            {
                ECNC3Exception.CollectionsFilter(ex, ExceptionHandling.LogOnly);
                using (MessageDialog msgDir = new MessageDialog())
                {
                    msgDir.Error(5034, this);
                }
            }
           
            //ボタン：有効/無効
            buttonEx_Clear_Check();
        }
        #endregion
        #region <region<ボタンイベント処理>region>
        /// <summary>
        /// 選択クリア
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEx_Clear_Click(object sender, EventArgs e)
        {
            //foreach( DataGridViewRow rows in dataGridViewEx1.Rows ) {
            //	if( (string)rows.Cells[0].Value == "●" ) rows.Cells[2].Value = decimalInitVal;//文字列削除
            //}
            //20170329 hachino Rep↑↑to↓↓テクノコマンド実装
            int EnableAxis = 0x7f;
            foreach (DataGridViewRow rows in dataGridViewEx1.Rows)
            {
                switch (rows.Index)
                {
                    case 0: if ((string)rows.Cells[0].Value != "●") EnableAxis -= 1; break;
                    case 1: if ((string)rows.Cells[0].Value != "●") EnableAxis -= 2; break;
                    case 2: if ((string)rows.Cells[0].Value != "●") EnableAxis -= 4; break;
                    case 3: if ((string)rows.Cells[0].Value != "●") EnableAxis -= 8; break;
                    case 4: if ((string)rows.Cells[0].Value != "●") EnableAxis -= 16; break;
                    case 5: if ((string)rows.Cells[0].Value != "●") EnableAxis -= 32; break;
                    case 6: if ((string)rows.Cells[0].Value != "●") EnableAxis -= 64; break;
                }
                //文字列削除
            }
            switch (_cordinateSystemType)
            {
                case 0://機械座標
                    using (Models.McIf.McReqModeChange mcModeChg = new Models.McIf.McReqModeChange())
                    using (Models.McIf.McDatStatus mcSta = new Models.McIf.McDatStatus())
                    using (Models.McIf.McReqAbsOriginSet absOrg = new Models.McIf.McReqAbsOriginSet())
                    {
                        mcSta.Read();
                        absOrg.AbsPosition = new StructureAxisCoordinate();
                        absOrg.AbsPosition.EnableAxis = EnableAxis;
                        mcModeChg.TaskMode = McTaskModes.Setting;
                        mcModeChg.Execute();
                        System.Threading.Thread.Sleep(500);
                        absOrg.Execute();
                        System.Threading.Thread.Sleep(500);
                        mcModeChg.TaskMode = McTaskModes.Manual;
                        mcModeChg.Execute();
                    }
                    break;

                case 1://ワーク座標
                    using (Models.McIf.McDatStatus mcSta = new Models.McIf.McDatStatus())
                    using (Models.McIf.McReqWorkPositionChange workOrg = new Models.McIf.McReqWorkPositionChange())
                    {
                        mcSta.Read();
                        workOrg.WorkPosition = mcSta.Status.CoordinateAsAbsReg.Clone() as StructureAxisCoordinate;
                        workOrg.WorkPosition.EnableAxis = EnableAxis;
                        workOrg.Execute();
                    }
                    break;

            }
            GetAxisPos();
        }
        /// <summary>
        /// 全クリア
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEx_AllClear_Click(object sender, EventArgs e)
        {
            //foreach( DataGridViewRow rows in dataGridViewEx1.Rows ) {
            //	rows.Cells[2].Value = decimalInitVal;//文字列削除
            //}
            //20170329 hachino Rep↑↑to↓↓テクノコマンド実装
            switch (_cordinateSystemType)
            {
                case 0://機械座標
                    using (Models.McIf.McReqModeChange mcModeChg = new Models.McIf.McReqModeChange())
                    using (Models.McIf.McDatStatus mcSta = new Models.McIf.McDatStatus())
                    using (Models.McIf.McReqAbsOriginSet absOrg = new Models.McIf.McReqAbsOriginSet())
                    {
                        mcSta.Read();
                        absOrg.AbsPosition = new StructureAxisCoordinate();
                        absOrg.AbsPosition.EnableAxis = 0x7f;
                        mcModeChg.TaskMode = McTaskModes.Setting;
                        mcModeChg.Execute();
                        System.Threading.Thread.Sleep(500);
                        absOrg.Execute();
                        System.Threading.Thread.Sleep(500);
                        mcModeChg.TaskMode = McTaskModes.Manual;
                        mcModeChg.Execute();
                    }
                    break;

                case 1://ワーク座標
                    using (Models.McIf.McDatStatus mcSta = new Models.McIf.McDatStatus())
                    using (Models.McIf.McReqWorkPositionChange workOrg = new Models.McIf.McReqWorkPositionChange())
                    {
                        mcSta.Read();
                        workOrg.WorkPosition = mcSta.Status.CoordinateAsAbsReg.Clone() as StructureAxisCoordinate;
                        workOrg.WorkPosition.EnableAxis = 0x7f;
                        workOrg.Execute();
                    }
                    break;

            }
            GetAxisPos();
        }
        /// <summary>
        /// 選択解除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEx_SelectOff_Click(object sender, EventArgs e)
        {//OFF
            foreach (DataGridViewRow rows in dataGridViewEx1.Rows)
            {
                rows.Cells[0].Value = "";//文字列削除
            }
            buttonEx_Clear.Enabled = false;//選択クリアボタン：無効
            buttonEx_SelectOff.Enabled = false;//選択解除ボタン：無効
        }
        /// <summary>
        /// "●"が1個でもある場合、「選択クリア」「選択解除」ボタン有効なければ無効
        /// </summary>
        private void buttonEx_Clear_Check()
        {
            buttonEx_Clear.Enabled = false;//選択クリアボタン：無効
            buttonEx_SelectOff.Enabled = false;//選択解除ボタン：無効
            foreach (DataGridViewRow rows in dataGridViewEx1.Rows)
            {
                if ((string)rows.Cells[0].Value == "●")
                {
                    //1個でもある場合、「選択クリア」ボタン有効
                    buttonEx_Clear.Enabled = true;      //選択クリアボタン：有効
                    buttonEx_SelectOff.Enabled = true;  //選択解除ボタン：有効

                }
            }
        }
        #endregion
        #region <region<テクノコマンド>region>
        private void GetAxisPos()
        {
            using (Models.McIf.McDatStatus mcSta = new Models.McIf.McDatStatus())
            {
                StructureAxisCoordinate posTemp = new StructureAxisCoordinate();
                mcSta.Read();
                switch (_cordinateSystemType)
                {
                    case 0: posTemp = mcSta.Status.CoordinateAsAbsReg.Clone() as StructureAxisCoordinate; break;

                    case 1: posTemp = mcSta.Status.CoordinateAsPosReg.Clone() as StructureAxisCoordinate; break;
                }
                dataGridViewEx1[2, 0].Value = ((decimal)posTemp.AxisX / 1000).ToString("###0.000") + "0";
                dataGridViewEx1[2, 1].Value = ((decimal)posTemp.AxisY / 1000).ToString("###0.000") + "0";
                dataGridViewEx1[2, 2].Value = ((decimal)posTemp.AxisW / 1000).ToString("###0.000") + "0";
                dataGridViewEx1[2, 3].Value = ((decimal)posTemp.AxisZ / 1000).ToString("###0.000") + "0";
                dataGridViewEx1[2, 4].Value = ((decimal)posTemp.AxisA / 1000).ToString("###0.000") + "0";
                dataGridViewEx1[2, 5].Value = ((decimal)posTemp.AxisB / 1000).ToString("###0.000") + "0";
                dataGridViewEx1[2, 6].Value = ((decimal)posTemp.AxisC / 1000).ToString("###0.000") + "0";
                dataGridViewEx1[2, 7].Value = ((decimal)posTemp.Axis8 / 1000).ToString("###0.000") + "0";
                dataGridViewEx1[2, 8].Value = ((decimal)posTemp.Axis9 / 1000).ToString("###0.000") + "0";
            }
        }
        private int GetWorkAxisPos(int index)
        {
            int ret = 0;
            using (Models.McIf.McDatStatus mcSta = new Models.McIf.McDatStatus())
            {
                mcSta.Read();
                StructureAxisCoordinate posTemp = mcSta.Status.CoordinateAsPosReg.Clone() as StructureAxisCoordinate;
                switch (index)
                {
                    case 0:
                        ret = WorkPosition.AxisX = posTemp.AxisX;
                        break;

                    case 1:
                        ret = WorkPosition.AxisY = posTemp.AxisY;
                        break;

                    case 2:
                        ret = WorkPosition.AxisW = posTemp.AxisW;
                        break;

                    case 3:
                        ret = WorkPosition.AxisZ = posTemp.AxisZ;
                        break;

                    case 4:
                        ret = WorkPosition.AxisA = posTemp.AxisA;
                        break;

                    case 5:
                        ret = WorkPosition.AxisB = posTemp.AxisB;
                        break;

                    case 6:
                        ret = WorkPosition.AxisC = posTemp.AxisC;
                        break;

                    case 7:
                        ret = WorkPosition.Axis8 = posTemp.Axis8;
                        break;

                    case 8:
                        ret = WorkPosition.Axis9 = posTemp.Axis9;
                        break;
                }
            }
            return ret;
        }
        #endregion
    }
}

