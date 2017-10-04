using System;
using System.Collections.Generic;

namespace quickDBExplorer
{
	/// <summary>
	/// SqlVersion の概要の説明です。
	/// </summary>
	/// 
	public class SqlVersion
	{

		/// <summary>
		/// バージョン番号をあらわす文字列
		/// </summary>
		private string pFullVersionString;
		/// <summary>
		/// バージョン番号をあらわす文字列
		/// </summary>
		public string FullVersionString
		{
			get { return this.pFullVersionString; }
		}

		/// <summary>
		/// 2000,2005,2008 等
		/// </summary>
		private string pPublicVersion;

		/// <summary>
		/// 製品名につくバージョン（2000,2005,2008等)
		/// </summary>
		public string PublicVersionNo
		{
			get { return this.pPublicVersion; }
		}

		/// <summary>
		/// 接続用DLLにつく名称
		/// </summary>
		public string AdapterNameString
		{
			get { return this.pPublicVersion.ToString(); }
		}

		/// <summary>
		/// 該当バーージョンで Synonym を利用できるか
		/// </summary>
		private bool pIsSynonym = true;

		/// <summary>
		/// 該当バーージョンで Synonym を利用できるか
		/// </summary>
		public bool CanUseSynonym
		{
			get { return this.pIsSynonym; }
		}

		/// <summary>
		/// クエリアナライザーを利用可能か否か
		/// </summary>
		private bool pCanUseQueryAnalyzer = true;
		/// <summary>
		/// クエリアナライザーを利用可能か否か
		/// </summary>
		public bool CanUseQueryAnalyzer
		{
			get { return this.pCanUseQueryAnalyzer; }
		}

		/// <summary>
		/// Management Studio と呼ぶか否か
		/// </summary>
		private bool pIsManagementStudio = false;

		/// <summary>
		/// Management Studio と呼ぶか否か
		/// </summary>
		public bool IsManagementStudio
		{
			get { return pIsManagementStudio; }
			set { pIsManagementStudio = value; }
		}

        /// <summary>
        /// プロファイラーのEXE名
        /// </summary>
        public string ProfilerExe { get; set; }

        /// <summary>
        /// マネージメントスタジオのEXE名
        /// </summary>
        public string ManagementExe { get; set; }

        /// <summary>
        /// ツールパス
        /// </summary>
        public string BinDir { get; set; }

        /// <summary>
        /// 設定値記憶レジストリパス
        /// </summary>
        public string regkey { get; set; }

		/// <summary>
		/// SQL Server 2000 を表すインスタンスを生成する
		/// </summary>
		/// <returns></returns>
		public static SqlVersion SQLSERVER2000()
		{
			return new SqlVersion("08.00.2039");
		}

		/// <summary>
		/// SQL Server 2005 を表すインスタンスを生成する
		/// </summary>
		public static SqlVersion SQLSERVER2005()
		{
			return new SqlVersion("09.00.3054");
		}

		/// <summary>
		/// SQL Server 2008 を表すインスタンスを生成する
		/// </summary>
		public static SqlVersion SQLSERVER2008()
		{
			return new SqlVersion("10.00");
		}

        /// <summary>
        /// SQL Server 2008R2 を表すインスタンスを生成する
        /// </summary>
        public static SqlVersion SQLSERVER2008R2()
        {
            return new SqlVersion("10.50");
        }


        /// <summary>
        /// SQL Server 2012 を表すインスタンスを生成する
        /// </summary>
        public static SqlVersion SQLSERVER2012()
        {
            return new SqlVersion("11.0");
        }

        /// <summary>
        /// SQL Server 2014 を表すインスタンスを生成する
        /// </summary>
        public static SqlVersion SQLSERVER2014()
        {
            return new SqlVersion("12.0");
        }

        /// <summary>
        /// SQL Server 2016 を表すインスタンスを生成する
        /// </summary>
        public static SqlVersion SQLSERVER2016()
        {
            return new SqlVersion("13.0");
        }

        /// <summary>
        /// SQL Server 2017 を表すインスタンスを生成する
        /// </summary>
        public static SqlVersion SQLSERVER2017()
        {
            return new SqlVersion("14.0");
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="versionStr">Connection.ServerVersion の結果を渡す</param>
        public SqlVersion(string versionStr)
		{
			// Connection.ServerVersion の結果が渡されるので、ここで判断する
			if(versionStr.StartsWith("08") )
			{
				// SQL Server 2000
				this.pPublicVersion = "2000";
				this.pFullVersionString = versionStr;
				this.pIsSynonym = false;
				this.pCanUseQueryAnalyzer = true;
				this.pIsManagementStudio = false;
                this.ProfilerExe = "profiler.exe";
                this.ManagementExe = "SQL Server Enterprise Manager.MSC";
                this.BinDir = "";
                this.regkey = @"SOFTWARE\Microsoft\Microsoft SQL Server\80\Tools\ClientSetup\";
            }
			else if(versionStr.StartsWith("09") )
			{
				this.pPublicVersion = "2005";
				this.pFullVersionString = versionStr;
				this.pIsSynonym = true;
				this.pCanUseQueryAnalyzer = false;
				this.pIsManagementStudio = true;
                this.ProfilerExe = "profiler90.exe";
                this.ManagementExe = "SqlWb";
                this.BinDir = @"bin\";
                this.regkey = @"SOFTWARE\Microsoft\Microsoft SQL Server\90\Tools\ClientSetup\"; ;
            }
            else if (versionStr.StartsWith("10.0"))
			{
				this.pPublicVersion = "2008";
				this.pFullVersionString = versionStr;
				this.pIsSynonym = true;
				this.pCanUseQueryAnalyzer = false;
				this.pIsManagementStudio = true;
                this.ProfilerExe = "profiler.exe";
                this.ManagementExe = "ssms.exe";
                this.BinDir = @"bin\";
                this.regkey = @"SOFTWARE\Microsoft\Microsoft SQL Server\100\Tools\ClientSetup\";
            }
            else if (versionStr.StartsWith("10.5"))
            {
                this.pPublicVersion = "2008R2";
                this.pFullVersionString = versionStr;
                this.pIsSynonym = true;
                this.pCanUseQueryAnalyzer = false;
                this.pIsManagementStudio = true;
                this.ProfilerExe = "profiler.exe";
                this.ManagementExe = "ssms.exe";
                this.BinDir = @"bin\";
                this.regkey = @"SOFTWARE\Microsoft\Microsoft SQL Server\100\Tools\ClientSetup\";
            }
            else if (versionStr.StartsWith("11.0"))
            {
                this.pPublicVersion = "2012";
                this.pFullVersionString = versionStr;
                this.pIsSynonym = true;
                this.pCanUseQueryAnalyzer = false;
                this.pIsManagementStudio = true;
                this.ProfilerExe = "profiler.exe";
                this.ManagementExe = "ssms.exe";
                this.BinDir = @"bin\";
                this.regkey = @"SOFTWARE\Microsoft\Microsoft SQL Server\110\Tools\ClientSetup\";
            }
            else if (versionStr.StartsWith("12.0"))
            {
                this.pPublicVersion = "2014";
                this.pFullVersionString = versionStr;
                this.pIsSynonym = true;
                this.pCanUseQueryAnalyzer = false;
                this.pIsManagementStudio = true;
                this.ProfilerExe = "profiler.exe";
                this.ManagementExe = "ssms.exe";
                this.regkey = @"SOFTWARE\Microsoft\Microsoft SQL Server\120\Tools\ClientSetup\";
                this.BinDir = @"binn\";
            }
            else if (versionStr.StartsWith("13.0"))
            {
                this.pPublicVersion = "2016";
                this.pFullVersionString = versionStr;
                this.pIsSynonym = true;
                this.pCanUseQueryAnalyzer = false;
                this.pIsManagementStudio = true;
                this.ProfilerExe = "profiler.exe";
                this.ManagementExe = "ssms.exe";
                this.regkey = @"SOFTWARE\Microsoft\Microsoft SQL Server\130\Tools\ClientSetup\";
                this.BinDir = @"binn\";
            }
            else if (versionStr.StartsWith("14.0"))
            {
                this.pPublicVersion = "2017";
                this.pFullVersionString = versionStr;
                this.pIsSynonym = true;
                this.pCanUseQueryAnalyzer = false;
                this.pIsManagementStudio = true;
                this.ProfilerExe = "profiler.exe";
                this.ManagementExe = "ssms.exe";
                this.regkey = @"SOFTWARE\Microsoft\Microsoft SQL Server\140\Tools\ClientSetup\";
                this.BinDir = @"binn\";
            }
            else
            {
                // 既定で 2017 にしておく
                this.pPublicVersion = "2017";
                this.pFullVersionString = versionStr;
                this.pIsSynonym = true;
                this.pCanUseQueryAnalyzer = false;
                this.pIsManagementStudio = true;
                this.ProfilerExe = "profiler.exe";
                this.ManagementExe = "ssms.exe";
                this.regkey = @"SOFTWARE\Microsoft\Microsoft SQL Server\140\Tools\ClientSetup\";
                this.BinDir = @"binn\";
            }
        }
    }
}
