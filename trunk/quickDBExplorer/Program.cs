using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace quickDBExplorer
{
    class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.Run(new MainMdi(args));
        }
    }
}
