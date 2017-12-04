using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECNCShutdown
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイント
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("ECNCShutdownコマンドラインに引数はありません。");
            }
            else
            {
                foreach (string arg in args)
                    Console.WriteLine(arg);
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(args));
        }
    }
}
