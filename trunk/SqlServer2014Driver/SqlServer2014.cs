using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Text;

namespace quickDBExplorer
{
	/// <summary>
	/// SqlServer2005 の概要の説明です。
	/// </summary>
	public class SqlServerDriver2014 : SqlServerDriver2012
	{
        public SqlServerDriver2014()
        {
            this.regkey = @"SOFTWARE\Microsoft\Microsoft SQL Server\120\Tools\ClientSetup\";
        }
    }
}