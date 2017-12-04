using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECNC3.Views
{
    /// <summary>
    /// フォーム間通知イベントのイベントハンドラ
    /// </summary>
    /// <param name="e"></param>
    public delegate void PnumUpLoadEventHandler(PnumUpLoadEventAges e);
    public class PnumUpLoadEventAges : EventArgs
    {
        public PnumUpLoadEventAges(string pnum)
        {
            Pnum = pnum;
        }
        public string Pnum { get; set; }
    }
}
