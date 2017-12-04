using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECNC3Updater
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //パス設定読み込み
            ECNC3.Models.FilePathInfo pathInfo = new ECNC3.Models.FilePathInfo();
            pathInfo.Read();
            Application.Run(new MainForm());
        }
    }
}
