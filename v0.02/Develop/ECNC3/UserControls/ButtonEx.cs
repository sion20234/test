using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ECNC3.Models;
using System.Threading;
using ECNC3.Views.UserControls;

namespace ECNC3.Views
{
    /// <summary>ボタンコントロール拡張</summary>
    public partial class ButtonEx : Button
    {
        /// <summary>
        /// ButtonEx内でBoolで表現できる状態
        /// </summary>
        internal enum ButtonExStatus
        {
            //有効無効
            Enabled,
            //選択状態
            Selected,
            //背景色ONOFF
            Back,
            //LEDONOFF
            Led
        }
        private Color _defaultBackColor;
        private Color _enabledBackColor;
        private Color _defaultForeColor;
        private Color _enabledForeColor;
        private Color _defaultOutLineColor;
        private Color _selectedOutLineColor;
        private Color _disableStatusLedColor;
        private Color _enableStatusLedColor;

        /// <summary>
        /// Invoke処理管理用変数
        /// </summary>
        public IAsyncResult retInvoke = null;
        /// <summary>
        /// ボタンの枠線描画のスタイル
        /// </summary>
        Pen _outLinePen = new Pen(FileUIStyleTable.DefaultLineColor, 2);
        /// <summary>
        /// LEDのスタイル
        /// </summary>
        Pen _ledPen = new Pen(FileUIStyleTable.DisableStatusLedColor);
        SolidBrush _ledBrush = new SolidBrush(FileUIStyleTable.EnableStatusLedColor);
        PointF[] _ledLocation;
        /// <summary>
        /// プログレスバーの描画のスタイル
        /// </summary>
        Pen _progressBarLinePen = null;
        /// <summary>
        /// ボタンの枠線のオフセット値
        /// </summary>
        int r;
        int b;
        ///// <summary>
        ///// 描画テキスト
        ///// </summary>
        //[Browsable(true)]
        //[Description("テキスト")]
        //[Category("表示")]
        //public new string Text
        //{
        //    get
        //    {
        //        return _text;
        //    }
        //    set
        //    {
        //        //標準のテキスト表示を無くす。
        //        base.Text = string.Empty;
        //        _text = value;
        //    }
        //}
        //private string _text = string.Empty;
        /// <summary>
        /// 枠線表示の有効
        /// </summary>
        [Browsable(true)]
        [Description("枠線の表示")]
        [Category("表示")]
        public bool OutLineEn { get; set; }
        /// <summary>
        /// 選択状態
        /// </summary>
        [Browsable(true)]
        [Description("選択状態")]
        [Category("表示")]
        public bool Selected { get; set; }
        /// <summary>
        /// 複数選択有効
        /// </summary>
        [Browsable(true)]
        [Description("複数選択有効")]
        [Category("表示")]
        public bool MultiSelectEn { get; set; }
        /// <summary>
        /// 枠線の太さ
        /// </summary>
        [Browsable(true)]
        [Description("枠線の太さ")]
        [Category("表示")]
        public float OutLineSize
        {
            get
            {
                return _outLinePen.Width;
            }
            set
            {
                _outLinePen.Width = value;
            }
        }
        /// <summary>
        /// フォーカス制御
        /// </summary>
        [Browsable(true)]
        [Description("フォーカス制御有効")]
        [Category("動作")]
        public bool Selectable
        {
            get { return GetStyle(ControlStyles.Selectable); }
            set { SetStyle(ControlStyles.Selectable, value); }
        }
        /// <summary>
        /// 枠線の色
        /// </summary>
        [Browsable(true)]
        [Description("枠線の色")]
        [Category("表示")]
        public Color OutLineColor
        {
            get
            {
                return _outLinePen.Color;
            }
            set
            {
                _outLinePen.Color = value;
            }
        }
        /// <summary>
        /// LEDの有効
        /// </summary>
        [Browsable(true)]
        [Description("状態表示(LED)")]
        [Category("表示")]
        public bool StatusLedEnable { get; set; }
        /// <summary>
        /// LEDの大きさ
        /// </summary>
        [Browsable(true)]
        [Description("状態表示(LED)の大きさ")]
        [Category("表示")]
        public byte StatusLedSize { get; set; }
        /// <summary>
        /// LEDの色
        /// </summary>
        [Browsable(true)]
        [Description("状態表示(LED)の色")]
        [Category("表示")]
        private Color StatusLedColor
        {
            get
            {
                return _ledBrush.Color;
            }
            set
            {
                _ledBrush.Color = value;
            }
        }
        #region ProgressBarProparty
        /// <summary>
        /// プログレスバーの有効
        /// </summary>
        [Browsable(true)]
        [Description("プログレスバーの有効")]
        [Category("表示")]
        public bool ProgressBarEnable { get; set; } = false;
        /// <summary>
        /// プログレスバーの太さ
        /// </summary>
        [Browsable(true)]
        [Description("プログレスバーの太さ")]
        [Category("表示")]
        public int ProgressBarSize
        {
            get
            {
                return _progressBarSize;
            }
            set
            {
                _progressBarSize = value;
                if (_progressBarLinePen != null)
                {
                    _progressBarLinePen.Dispose();
                    _progressBarLinePen = null;
                }
                _progressBarLinePen = new Pen
                    (
                    FileUIStyleTable.SelectedLineColor,
                    ProgressBarSize
                    );
            }
        }
        private int _progressBarSize = 5;
        /// <summary>
        /// プログレスバーの色
        /// </summary>
        [Browsable(true)]
        [Description("プログレスバーの色")]
        [Category("表示")]
        public Color ProgressBarColor { get; set; } = FileUIStyleTable.SelectedLineColor;
        /// <summary>
        /// プログレスバーの値
        /// </summary>
        [Browsable(true)]
        [Description("プログレスバーの値")]
        [Category("表示")]
        public int ProgressBarValue {
            get
            {
                return _progressBarValue;
            }
            set
            {
                _progressBarValue = value;
                //プログレスバー描画
                _PaintProgressBar();
                this.Refresh();
            }
        }
        private int _progressBarValue = 0;
        /// <summary>
        /// プログレスバーの最大値
        /// </summary>
        [Browsable(true)]
        [Description("プログレスバーの最大値")]
        [Category("表示")]
        public int ProgressBarMaxValue { get; set; } = 100;
        /// <summary>
        /// プログレスバーの最小値
        /// </summary>
        [Browsable(true)]
        [Description("プログレスバーの最小値")]
        [Category("表示")]
        public int ProgressBarMinValue { get; set; } = 0;
        private int _progressBarHeight { get; set; }
        private int _progressBarWidth { get; set; }
        private int _progressBarPositionX { get; set; }
        private int _progressBarPositionY { get; set; }
        #endregion
        /// <summary>
        /// LEDをOFFした際に背景色もOFFにするか
        /// </summary>
        [Browsable(true)]
        [Description("LED-OFFで背景OFF有効")]
        [Category("表示")]
        public bool LedSyncedBackColorEnable { get; set; } = true;
        private bool _LedOn { get; set; }
        private bool _ToolModeEnable = false;
        /// <summary>コンストラクタ</summary>
        public ButtonEx()
        {
            _ToolModeEnable = false;
            Default_Init();
        }
          
        public void Default_Init()
        {
            //プログレスバー初期化処理
            _ProgressBarInit();
            _defaultBackColor = _ToolModeEnable ? FileUIStyleTable.ToolBackColor : FileUIStyleTable.DefaultBackColor;
            _enabledBackColor = FileUIStyleTable.EnabledBackColor;
            _defaultForeColor = _ToolModeEnable ? FileUIStyleTable.ToolForeColor : FileUIStyleTable.DefaultForeColor;
            _enabledForeColor = FileUIStyleTable.EnabledForeColor;
            _defaultOutLineColor = FileUIStyleTable.DefaultLineColor;
            _selectedOutLineColor = _ToolModeEnable ? FileUIStyleTable.ToolLineColor : FileUIStyleTable.SelectedLineColor;
            _disableStatusLedColor = FileUIStyleTable.DisableStatusLedColor;
            _enableStatusLedColor = FileUIStyleTable.EnableStatusLedColor;
            FlatStyle = FlatStyle.Standard;
            FlatAppearance.MouseDownBackColor = _defaultForeColor;
            FlatAppearance.BorderSize = 0;
            BackColor = _defaultBackColor;
            ForeColor = _defaultForeColor;
            OutLineEn = true;
            OutLineColor = _defaultOutLineColor;
            StatusLedSize = 15;
            StatusLedColor = _disableStatusLedColor;
            if (StatusLedSize > 0)
            {
                _ledLocation = new PointF[]{ new PointF(Location.X + OutLineSize + 2, Location.Y + OutLineSize + 2),
                                            new PointF(Location.X + OutLineSize + 2 + StatusLedSize , Location.Y + OutLineSize + 2),
                                            new PointF(Location.X + OutLineSize + 2, Location.Y + OutLineSize + 2 + StatusLedSize)};
            }
            r = 0;
            b = 0;
            Click += ButtonEx_Click;
            MouseDown += ButtonEx_MouseDown;
            MouseUp += ButtonEx_MouseUp;
            Paint += ButtonEx_Paint;
            _progressbarImage = new Bitmap(Width, Height);
            Refresh();
        }
        public void ToolModeInit(bool toolMode)
        {
            _ToolModeEnable = toolMode;
            _defaultBackColor = _ToolModeEnable ? FileUIStyleTable.ToolBackColor : FileUIStyleTable.DefaultBackColor;
            _enabledBackColor = FileUIStyleTable.EnabledBackColor;
            _defaultForeColor = _ToolModeEnable ? FileUIStyleTable.ToolForeColor : FileUIStyleTable.DefaultForeColor;
            _enabledForeColor = FileUIStyleTable.EnabledForeColor;
            _defaultOutLineColor = FileUIStyleTable.DefaultLineColor;
            _selectedOutLineColor = _ToolModeEnable ? FileUIStyleTable.ToolLineColor : FileUIStyleTable.SelectedLineColor;
            _disableStatusLedColor = FileUIStyleTable.DisableStatusLedColor;
            _enableStatusLedColor = FileUIStyleTable.EnableStatusLedColor;
            FlatStyle = FlatStyle.Standard;
            FlatAppearance.MouseDownBackColor = _defaultForeColor;
            FlatAppearance.BorderSize = 0;
            BackColor = _defaultBackColor;
            ForeColor = _defaultForeColor;
            OutLineEn = true;
            OutLineColor = _defaultOutLineColor;
            StatusLedSize = 15;
            StatusLedColor = _disableStatusLedColor;
        }
        private void _ProgressBarInit()
        {
            _progressBarLinePen = new Pen(FileUIStyleTable.SelectedLineColor, ProgressBarSize);
        }
        public new void Dispose()
        {
            try
            {
                int retryCount = 0;
                if (retInvoke != null)
                {
                    while (!retInvoke.IsCompleted)
                    {
                        if (retryCount > 10)
                        {
                            Thread.Sleep(100);
                            return;
                        }
                        retryCount++;
                    }
                }
            }
            finally
            {
                base.Dispose();
            }
        }
        /// <summary>クリックイベント</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonEx_Click(object sender, EventArgs e)
        {
            _ClickEffect(ButtonEx_ClickEffect.Construst);   //ボタンクリック時のエフェクト
            if (((ButtonEx)(sender)).MultiSelectEn == true)
            {
                SetBack(!GetBack());
            }

            ButtonEx target = sender as ButtonEx;
            ECNC3.Models.Common.ECNC3Log logs = new Models.Common.ECNC3Log("ButtonEx_Click");
            string text = target.Text.Replace(Environment.NewLine, " ");
            logs.Operate($"CLK,{text}");
        }
        public Control[] GetAllControls(Control top)
        {
            List<Control> buf = new List<Control>();
            foreach (Control c in top.Controls)
            {
                buf.Add(c);
                buf.AddRange(GetAllControls(c));
            }
            return buf.ToArray();
        }
        #region <Effect>
        public enum ButtonEx_ClickEffect
        {
            //縮小
            Construst = 1
        }
        /// <summary>
        /// ボタンクリック時のエフェクト効果
        /// </summary>
        /// <param name="mode"></param>
        private async void _ClickEffect(ButtonEx_ClickEffect mode)
        {
            switch(mode)
            {
                case ButtonEx_ClickEffect.Construst:
                    //縮小
                    this.Location = new Point(this.Location.X + 2, this.Location.Y + 2);
                    this.Size = new Size(this.Size.Width - 4, this.Size.Height - 4);
                    //フォント縮小
                    this.Font = new Font
                        (
                        this.Font.FontFamily,
                        this.Font.Size - 2.0f,
                        this.Font.Style
                        );
                    Refresh();
                    //タッチだとMouseDown-MouseUp間の時間がないためスリープ
                    await Task.Delay(200);
                    //Thread.Sleep(200);
                    //元の大きさに拡大
                    this.Location = new Point(this.Location.X - 2, this.Location.Y - 2);
                    this.Size = new Size(this.Size.Width + 4, this.Size.Height + 4);
                    //元のフォントに変更
                    this.Font = new Font
                        (
                        this.Font.FontFamily,
                        this.Font.Size + 2.0f,
                        this.Font.Style
                        );
                    break;

            }
        }

#endregion
        /// <summary>
        /// ボタンをランプとして使う場合の色切替
        /// </summary>
        /// <param name="LampOn"></param>
        public void SetLed(bool LedOn)
        {
            if (LedOn == true)
            {
                _ledBrush.Color = _enableStatusLedColor;
                _ledPen.Color = _enabledBackColor;
            }
            else
            {
                _ledBrush.Color = _disableStatusLedColor;
                _ledPen.Color = _defaultBackColor;
            }
            _LedOn = LedOn;
            if(LedSyncedBackColorEnable == true) SetBack(false);

            if ( this == null ) {
				return;
			}
			if( InvokeRequired ) {
                if ( ( this.IsDisposed == true || this.Disposing == true ) ) return; retInvoke = BeginInvoke((MethodInvoker)delegate
				{
					Refresh();
				} ); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
            } else {
				Refresh();
			}
        }

        public bool GetLed()
        {
            return _LedOn;
        }
       
        /// <summary>
        /// ボタンをランプとして使う場合の色切替
        /// </summary>
        /// <param name="BackOn"></param>
        public void SetBack(bool BackOn)
        {
            if (BackOn == true)
            {
                if(BackColor != _enabledBackColor) BackColor = _enabledBackColor;
                if(ForeColor != _enabledForeColor) ForeColor = _enabledForeColor;
            }
            else
            {
                if(BackColor != _defaultBackColor) BackColor = _defaultBackColor;
                if(ForeColor != _defaultForeColor) ForeColor = _defaultForeColor;
            }
            
        }

        public bool GetBack()
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
        /// <summary>
        /// 枠線色変更（bool値）
        /// </summary>
        /// <param name="selected"></param>
        public void SetSelected(bool selected)
        {
            Selected = selected;
            SetBack(selected);
            if (selected == true)
            {
                _outLinePen.Color = _selectedOutLineColor;
            }
            else
            {
                _outLinePen.Color = _defaultOutLineColor;
            }
        }
        /// <summary>
        /// 枠線色変更（bool値）
        /// </summary>
        /// <param name="selected"></param>
        public void SetSelected(bool selected, Color lineColor)
        {
            Selected = selected;
            SetBack(selected);
            if (selected == true)
            {
                _outLinePen.Color = lineColor;
            }
            else
            {
                _outLinePen.Color = _defaultOutLineColor;
            }
            if( (this.IsDisposed == true || this.Disposing == true)   ) return; retInvoke = BeginInvoke((MethodInvoker)delegate ()
            {
                Refresh();
            }); if ((this.IsDisposed == true || this.Disposing == true)) return; EndInvoke(retInvoke);
        }
        /// <summary>
        /// 枠線色取得（bool値）
        /// </summary>
        /// <returns></returns>
        public bool GetSelected()
        {
            return Selected;
        }
        private bool _isActive;
        /// <summary>関連付けられたエディットコントロール</summary>
        public NumericTextBox EditBox { get; set; } = null;
        /// <summary>有効状態</summary>
        public bool IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                if (true == value)
                {
                    BackColor = _enabledBackColor;
                    ForeColor = _enabledForeColor;
                    if (null != EditBox)
                    {
                        EditBox.ReadOnly = true;
                        EditBox.BackColor = _enabledBackColor;
                        EditBox.ForeColor = _enabledForeColor;
                        EditBox.Focus();
                        EditBox.SelectAll();
                    }
                }
                else
                {
                    BackColor = _defaultBackColor;
                    ForeColor = _defaultForeColor;
                    if (null != EditBox)
                    {
                        EditBox.ReadOnly = true;
                        EditBox.BackColor = _defaultBackColor;
                        EditBox.ForeColor = _defaultForeColor;
                        if (true == EditBox.Focused)
                        {
                            Focus();
                        }
                    }

                }
                _isActive = value;
            }
        }
        /// <summary>書式化</summary>
        public void Formating()
        {
            if (null != EditBox)
            {
                EditBox.Formating();
            }
        }
        /// <summary>
        /// ボタン状態の反転
        /// </summary>
        /// <returns></returns>
        public bool InvertState()
        {
            if (true == IsActive)
            {
                Formating();
                IsActive = false;
            }
            else
            {
                IsActive = true;
            }
            return IsActive;
        }
        
        /// <summary>
        /// 枠線表示（Paintイベントハンドラ）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonEx_Paint(object sender, PaintEventArgs e)
        {
            //LED描画
            _PaintLed(e.Graphics);
            //枠線描画
            _PaintOutLine(e.Graphics);
            //文字列描画
            //_PaintButtonText(e.Graphics);
        }
        Bitmap _progressbarImage = null;
        Graphics _progressbarGraphics = null;
        /// <summary>
        /// プログレスバー描画
        /// </summary>
        private void _PaintProgressBar()
        {
            //描画
            if (ProgressBarEnable == true)
            {
                //if (_progressbarGraphics != null)
                //{
                //    _progressbarGraphics.Dispose();
                //    _progressbarGraphics = null;
                //}
                if (_progressBarValue <= ProgressBarMinValue)
                {
                    Image = null;
                }
                else
                {
                    if(
                        _progressbarImage.Width != this.Width
                        || _progressbarImage.Height != this.Height
                        )
                    {
                        //Imageオブジェクトの初期化
                        if (_progressbarImage != null)
                        {
                            _progressbarImage.Dispose();
                            _progressbarImage = null;
                        }
                        _progressbarImage = new Bitmap(this.Width, this.Height);
                    }                    
                    //ImageオブジェクトのGraphicsオブジェクトを作成する
                    _progressbarGraphics = Graphics.FromImage(_progressbarImage);
                    if (ProgressBarSize != this.Height) ProgressBarSize = this.Height;
                    _progressbarGraphics.DrawLine
                        (
                        _progressBarLinePen,
                        OutLineSize,
                        b - (ProgressBarSize / 2) + 4,
                        (int)((double)((double)((r) - (OutLineSize))
                        / (double)ProgressBarMaxValue) * ProgressBarValue),
                        b - (ProgressBarSize / 2) + 4
                        ); // プログレスバー 
                           //リソースを解放する
                    _progressbarGraphics.Dispose();
                    _progressbarGraphics = null;

                    //作成した画像を表示する
                    this.Image = _progressbarImage;
                }
            }
        }
        /// <summary>
        /// LED描画
        /// </summary>
        /// <param name="g"></param>
        private void _PaintLed(Graphics g)
        {
            if (StatusLedEnable == true)
            {
                if (StatusLedColor == Color.Empty)
                {
                    StatusLedColor = _disableStatusLedColor;
                }
                g.FillPolygon(_ledBrush, _ledLocation);
                g.DrawPolygon(_ledPen, _ledLocation);
            }
        }
        /// <summary>
        /// 枠線描画
        /// </summary>
        /// <param name="g"></param>
        private void _PaintOutLine(Graphics g)
        {
            if (OutLineEn == true)
            {
                if (this.Enabled == false)
                {
                    _outLinePen.Color = (Selected == true) ? _selectedOutLineColor : Color.FromArgb(75, 75, 75);
                }
                else
                {
                    _outLinePen.Color = (Selected == true) ? _selectedOutLineColor : _defaultOutLineColor;
                }
                r = this.ClientRectangle.Right - (int)OutLineSize;
                b = this.ClientRectangle.Bottom - (int)OutLineSize;
                g.DrawLine(_outLinePen, OutLineSize - 3, OutLineSize - 2, 4 + r, OutLineSize - 2); // 上
                g.DrawLine(_outLinePen, OutLineSize - 2, OutLineSize - 1, -2 + OutLineSize, b + 3); // 左
                g.DrawLine(_outLinePen, r + 1, OutLineSize - 1, r + 1, b + 3); // 右
                g.DrawLine(_outLinePen, OutLineSize - 3, b + 1, r + 3, b + 1); // 下 
            }
        }
        /// <summary>
        /// ボタンテキスト描画
        /// </summary>
        /// <param name="g"></param>
        private void _PaintButtonText(Graphics g)
        {
            //NoPaddingにして、文字列を描画する
            TextRenderer.DrawText
                (
                g,
                this.Text,
                this.Font,
                new Point
                (
                    (
                    this.Width - (int)g.MeasureString
                        (
                            this.Text,
                            this.Font,
                            new SizeF
                            (
                                Width,
                                Height
                            ),
                            new StringFormat()
                        ).Width + 3
                    ) / 2,
                    (
                    this.Height - (int)g.MeasureString
                        (
                            this.Text,
                            this.Font,
                            new SizeF
                            (
                                Width - OutLineSize,
                                Height - OutLineSize
                            ),
                            new StringFormat()
                        ).Height
                    ) / 2
                ),
                (this.Enabled == true) ? this.ForeColor : Color.DarkGray,
                TextFormatFlags.NoPadding
                );
        }
		private void ButtonEx_MouseDown( object sender, MouseEventArgs e )
		{





        }
        private void ButtonEx_MouseUp(object sender, MouseEventArgs e)
        {
            
        }
    }
}