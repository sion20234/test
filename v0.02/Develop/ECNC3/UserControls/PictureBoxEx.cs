using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ECNC3.Models.Common;
using System.IO;
using System.Threading;
using System.Diagnostics;
using ECNC3.Models;

namespace ECNC3.Views
{
	/// <summary>PictureBox拡張</summary>
	public partial class PictureBoxEx : PictureBox
	{
		/// <summary>コンストラクタ</summary>
		public PictureBoxEx()
		{
			InitializeComponent();

			DirectoryPicture = Path.Combine(Application.StartupPath, @"Picture" );
            MouseDown += PictureBoxEx_MouseDown;
            MouseUp += PictureBoxEx_MouseUp;
            _ProgressBarInit();
        }
        #region ProgressBarProparty
        /// <summary>
        /// プログレスバーの描画のスタイル
        /// </summary>
        Pen _progressBarLinePen = null;
        /// <summary>
        /// ボタンの枠線のオフセット値
        /// </summary>
        int r;
        int b;
        /// <summary>
        /// プログレスバーの有効
        /// </summary>
        [Browsable(true)]
        [Description("プログレスバーの有効")]
        [Category("表示")]
        public bool ProgressBarEnable
        {
            get
            {
                return (_progressbarImage != null);
            }
            set
            {
                if (_progressbarImage == null)
                {
                    _progressbarImage = new Bitmap(Width, Height);
                }
            }
        }
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
        public int ProgressBarValue
        {
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
                r = this.ClientRectangle.Right - (int)OutLineSize;
                b = this.ClientRectangle.Bottom - (int)OutLineSize;
            }
        }
        /// <summary>
        /// ボタンの枠線描画のスタイル
        /// </summary>
        Pen _outLinePen = new Pen(FileUIStyleTable.DefaultLineColor, 2);

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
                    if (
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
                    this.BackgroundImage = _progressbarImage;
                }
            }
        }
        private void _ProgressBarInit()
        {
            _progressBarLinePen = new Pen(FileUIStyleTable.SelectedLineColor, ProgressBarSize);
        }
        #endregion

        /// <summary>画像ファイル格納ディレクトリ</summary>
        private string DirectoryPicture { get; set; }
        private void PictureBoxEx_MouseDown( object sender, MouseEventArgs e )
        {
            //縮小
            this.Bounds = new Rectangle
                (
                new Point(this.Location.X + 2, this.Location.Y + 2),
                new Size(this.Size.Width - 4, this.Size.Height - 4)
                );
            //フォント縮小
            this.Font = new Font
                (
                this.Font.FontFamily,
                this.Font.Size - 2.0f,
                this.Font.Style
                );
        }
        private void PictureBoxEx_MouseUp(object sender, MouseEventArgs e)
        {
            //タッチだとMouseDown-MouseUp間の時間がないためスリープ
            Thread.Sleep(100);
            //元の大きさに拡大
            this.Bounds = new Rectangle
                (
                new Point(this.Location.X - 2, this.Location.Y - 2),
                new Size(this.Size.Width + 4, this.Size.Height + 4)
                );
            //元のフォントに変更
            this.Font = new Font
                (
                this.Font.FontFamily,
                this.Font.Size + 2.0f,
                this.Font.Style
                );
        }
        /// <summary>
        /// コントロール上にマウスポインタがあるかどうか
        /// </summary>
        public bool IsMouseOver
        {
            get
            {
                return ClientRectangle.Contains(PointToClient(Cursor.Position));
            }
            private set { }
        }
        /// <summary>イメージのロード</summary>
        /// <param name="path">ファイルパス</param>
        public bool LoadImage( string path = null )
		{
			while( true ) {
				if( true == string.IsNullOrEmpty( path ) ) {
					Dispose();
					Image = null;
				} else {
					if( false == System.IO.File.Exists( path ) ) {
						break;
					}
					if( false == string.IsNullOrEmpty( ImageLocation ) ) {
						if( true == ImageLocation.Equals( path ) ) {
							return true;    //	更新不要
						}
					}
					Load( path );
                    this.SizeMode = PictureBoxSizeMode.StretchImage;
				}
				return true;
			}
			ECNC3Log logs = new ECNC3Log( "PictureBoxEx.LoadImage" );
			logs.Error( $"{path}" );
			return false;
		}
		/// <summary>アイコン表示タイプ</summary>
		public enum IconTypes
		{
			/// <summary>異常(×)</summary>
			CriticalError,
			/// <summary>ヘルプ(？)</summary>
			Help,
			/// <summary>通知(ｉ)</summary>
			Information,
			/// <summary>警告(！)</summary>
			Warning,
			/// <summary>非表示</summary>
			Clear,
		}
		/// <summary>表示アイコン設定</summary>
		public IconTypes IconType
		{
			get
			{
				if( false == string.IsNullOrEmpty( ImageLocation ) ) {
					if( true == ImageLocation.Equals( Path.Combine( DirectoryPicture, @"StatusCriticalError_cyan_48x.png" ) ) ) {
						return IconTypes.CriticalError;
					} else if( true == ImageLocation.Equals( Path.Combine( DirectoryPicture, @"StatusHelp_cyan_48x.png" ) ) ) {
						return IconTypes.Help;
					} else if( true == ImageLocation.Equals( Path.Combine( DirectoryPicture, @"StatusInformation_cyan_48x.png" ) ) ) {
						return IconTypes.Information;
					} else if( true == ImageLocation.Equals( Path.Combine( DirectoryPicture, @"StatusWarning_cyan_48x.png" ) ) ) {
						return IconTypes.Warning;
					}
				}
				return IconTypes.Clear;
			}
			set
			{
				switch( value ) {
					case IconTypes.CriticalError:
						LoadImage( Path.Combine( DirectoryPicture, @"StatusCriticalError_cyan_48x.png" ) );
						break;
					case IconTypes.Help:
						LoadImage( Path.Combine( DirectoryPicture, @"StatusHelp_cyan_48x.png" ) );
						break;
					case IconTypes.Information:
						LoadImage( Path.Combine( DirectoryPicture, @"StatusInformation_cyan_48x.png" ) );
						break;
					case IconTypes.Warning:
						LoadImage( Path.Combine( DirectoryPicture, @"StatusWarning_cyan_48x.png" ) );
						break;
					case IconTypes.Clear:
					default:
						if( true == Visible ) {
							Visible = false;
                            ImageLocation = null;
						}
						return;
				}
				if( false == Visible ) {
					Visible = true;
				}
			}
		}
	}
}
