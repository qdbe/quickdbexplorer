using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using quickDBExplorer.Forms;

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
            //Application.EnableVisualStyles();
            Application.Run(new MainMdi(args));
        }
    }
}
