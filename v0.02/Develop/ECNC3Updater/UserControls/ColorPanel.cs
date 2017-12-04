using System.Drawing;
using System.Windows.Forms;

public class ColorPanel : StatusBarPanel
{
    private Font _font = null;
    /// <summary>
    /// 使用するフォント
    /// </summary>
    public Font Font
    {
        set
        {
            _font = value;
            //StatusBarを再描画する
            if (this.Parent != null)
            {
                this.Parent.Refresh();
            }
        }
        get { return _font; }
    }

    private Color _foreColor = Color.Empty;
    /// <summary>
    /// 前景色
    /// </summary>
    public Color ForeColor
    {
        set
        {
            _foreColor = value;
            //StatusBarを再描画する
            if (this.Parent != null)
            {
                this.Parent.Refresh();
            }
        }
        get { return _foreColor; }
    }

    private Color _backColor = Color.Empty;
    /// <summary>
    /// 背景色
    /// </summary>
    public Color BackColor
    {
        set
        {
            _backColor = value;
            //StatusBarを再描画する
            if (this.Parent != null)
            {
                this.Parent.Refresh();
            }
        }
        get { return _backColor; }
    }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="sb">パネルを追加するStatusBar</param>
    public ColorPanel(StatusBar sb)
    {
        //オーナードローを指定する
        this.Style = StatusBarPanelStyle.OwnerDraw;

        //StatusBarのDrawItemイベントハンドラの追加
        sb.DrawItem +=
            new StatusBarDrawItemEventHandler(sb_DrawItem);
    }

    private void sb_DrawItem(object sender,
        StatusBarDrawItemEventArgs sbdevent)
    {
        //描画するパネルが自分自身か確かめる
        if (sbdevent.Panel == this)
        {
            //文字の配置位置を決定
            StringFormat sf = new StringFormat();
            if (this.Alignment == HorizontalAlignment.Left)
                sf.Alignment = StringAlignment.Near;
            else if (this.Alignment == HorizontalAlignment.Center)
                sf.Alignment = StringAlignment.Center;
            else if (this.Alignment == HorizontalAlignment.Right)
                sf.Alignment = StringAlignment.Far;
            sf.LineAlignment = StringAlignment.Center;

            //前景色、背景色のブラシを作成
            Brush foreBrush;
            if (_foreColor != Color.Empty)
                foreBrush = new SolidBrush(_foreColor);
            else
                foreBrush = new SolidBrush(sbdevent.ForeColor);
            Brush backBrush;
            if (_backColor != Color.Empty)
                backBrush = new SolidBrush(_backColor);
            else
                backBrush = new SolidBrush(sbdevent.BackColor);
            //使用するフォントを決定
            Font fnt;
            if (_font != null)
                fnt = _font;
            else
                fnt = sbdevent.Font;

            //背景を描画
            sbdevent.Graphics.FillRectangle(
                backBrush, sbdevent.Bounds);
            //文字列を描画
            sbdevent.Graphics.DrawString(
                this.Text, fnt, foreBrush, sbdevent.Bounds, sf);

            //破棄
            foreBrush.Dispose();
            backBrush.Dispose();
        }
    }
}
