using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.Win32;

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
        /// SSMSのパラメーター形式（旧形式 or 新形式）
        /// </summary>
        public string SSMSParameterFormat { get; set; }

        /// <summary>
        /// SSMS バージョン（パラメーター形式判定用）
        /// </summary>
        public int SSMSVersion { get; set; }

        private string installedSSMSPath;

        /// <summary>
        /// SQL SERVER VERSION 最大値
        /// </summary>
        public static int MaxVer = 30;

        /// <summary>
        /// SQL SERVER VERSION 最小値
        /// </summary>
        public static int MinVer = 8;

        /// <summary>
        /// SSMSパラメーター形式（旧形式）
        /// SQL Server 2017以前: -S -d -U -P -E -nosplash
        /// </summary>
        private const string SSMS_PARAM_FORMAT_OLD = "OLD";

        /// <summary>
        /// SSMSパラメーター形式（新形式）
        /// SQL Server 2019以降: -S -d -U -P -E (nosplashなし)
        /// </summary>
        private const string SSMS_PARAM_FORMAT_NEW = "NEW";

        /// <summary>
        /// SSMS 22以降のパラメーター形式
        /// SQL Server 2022以降の SSMS: -A (認証用)、-Encrypt (暗号化用)
        /// </summary>
        private const string SSMS_PARAM_FORMAT_V22 = "V22";

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
        /// SQL Server 2019 を表すインスタンスを生成する
        /// </summary>
        public static SqlVersion SQLSERVER2019()
        {
            return new SqlVersion("15.0");
        }

        /// <summary>
        /// SQL Server 2022 を表すインスタンスを生成する
        /// </summary>
        public static SqlVersion SQLSERVER2022()
        {
            return new SqlVersion("16.0");
        }

        /// <summary>
        /// SQLSERVERの最大バージョンを取得する
        /// </summary>
        /// <returns></returns>
        public static SqlVersion GetMaxVersion()
        {
            return SQLSERVER2022();
        }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="versionStr">Connection.ServerVersion の結果を渡す</param>
		public SqlVersion(string versionStr)
		{
			// Connection.ServerVersion の結果を渡されるので、ここで判定する
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
                this.SSMSParameterFormat = SSMS_PARAM_FORMAT_OLD;
                this.SSMSVersion = 8;
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
                this.regkey = @"SOFTWARE\Microsoft\Microsoft SQL Server\90\Tools\ClientSetup\";
                this.SSMSParameterFormat = SSMS_PARAM_FORMAT_OLD;
                this.SSMSVersion = 9;
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
                this.SSMSParameterFormat = SSMS_PARAM_FORMAT_OLD;
                this.SSMSVersion = 10;
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
                this.SSMSParameterFormat = SSMS_PARAM_FORMAT_OLD;
                this.SSMSVersion = 10;
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
                this.SSMSParameterFormat = SSMS_PARAM_FORMAT_OLD;
                this.SSMSVersion = 11;
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
                this.SSMSParameterFormat = SSMS_PARAM_FORMAT_OLD;
                this.SSMSVersion = 12;
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
                this.SSMSParameterFormat = SSMS_PARAM_FORMAT_OLD;
                this.SSMSVersion = 13;
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
                this.SSMSParameterFormat = SSMS_PARAM_FORMAT_OLD;
                this.SSMSVersion = 14;
            }
            else if (versionStr.StartsWith("15.0"))
            {
                this.pPublicVersion = "2019";
                this.pFullVersionString = versionStr;
                this.pIsSynonym = true;
                this.pCanUseQueryAnalyzer = false;
                this.pIsManagementStudio = true;
                this.ProfilerExe = "profiler.exe";
                this.ManagementExe = "ssms.exe";
                this.regkey = @"SOFTWARE\Microsoft\Microsoft SQL Server\150\Tools\ClientSetup\";
                this.BinDir = @"binn\";
                this.SSMSParameterFormat = SSMS_PARAM_FORMAT_NEW;
                this.SSMSVersion = 15;
            }
            else if (versionStr.StartsWith("16.0"))
            {
                this.pPublicVersion = "2022";
                this.pFullVersionString = versionStr;
                this.pIsSynonym = true;
                this.pCanUseQueryAnalyzer = false;
                this.pIsManagementStudio = true;
                this.ProfilerExe = "profiler.exe";
                this.ManagementExe = "ssms.exe";
                this.regkey = @"SOFTWARE\Microsoft\Microsoft SQL Server\160\Tools\ClientSetup\";
                this.BinDir = @"binn\";
                this.SSMSParameterFormat = SSMS_PARAM_FORMAT_NEW;
                this.SSMSVersion = 16;
            }
            else if (versionStr.StartsWith("17.0"))
            {
                this.pPublicVersion = "2025";
                this.pFullVersionString = versionStr;
                this.pIsSynonym = true;
                this.pCanUseQueryAnalyzer = false;
                this.pIsManagementStudio = true;
                this.ProfilerExe = "profiler.exe";
                this.ManagementExe = "ssms.exe";
                this.regkey = @"SOFTWARE\Microsoft\Microsoft SQL Server\170\Tools\ClientSetup\";
                this.BinDir = @"binn\";
                this.SSMSParameterFormat = SSMS_PARAM_FORMAT_NEW;
                this.SSMSVersion = 17;
            }
            else
            {
                // 既定で 2025 にしておく
                this.pPublicVersion = "2025";
                this.pFullVersionString = versionStr;
                this.pIsSynonym = true;
                this.pCanUseQueryAnalyzer = false;
                this.pIsManagementStudio = true;
                this.ProfilerExe = "profiler.exe";
                this.ManagementExe = "ssms.exe";
                this.regkey = @"SOFTWARE\Microsoft\Microsoft SQL Server\170\Tools\ClientSetup\";
                this.BinDir = @"binn\";
                this.SSMSParameterFormat = SSMS_PARAM_FORMAT_NEW;
                this.SSMSVersion = 17;
            }
        }

        /// <summary>
        /// コマンドライン引数を Windows の引用規則に従って引用する。
        /// </summary>
        private static string QuoteCommandLineArgument(string value)
        {
            if (value == null)
            {
                value = string.Empty;
            }

            StringBuilder result = new StringBuilder();
            result.Append('"');
            int backslashCount = 0;
            foreach (char c in value)
            {
                if (c == '\\')
                {
                    backslashCount++;
                }
                else if (c == '"')
                {
                    result.Append('\\', backslashCount * 2 + 1);
                    result.Append('"');
                    backslashCount = 0;
                }
                else
                {
                    result.Append('\\', backslashCount);
                    result.Append(c);
                    backslashCount = 0;
                }
            }
            result.Append('\\', backslashCount * 2);
            result.Append('"');
            return result.ToString();
        }

        private static void AddExecutableCandidate(List<string> candidates, string path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                return;
            }

            foreach (string candidate in candidates)
            {
                if (string.Equals(candidate, path, StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }
            }
            candidates.Add(path);
        }

        private static void AddExecutableCandidatesFromCommand(List<string> candidates, string command, string[] executableNames)
        {
            if (string.IsNullOrEmpty(command))
            {
                return;
            }

            string[] quotedParts = command.Split('"');
            for (int i = 1; i < quotedParts.Length; i += 2)
            {
                AddExecutableCandidate(candidates, quotedParts[i]);
            }

            foreach (string executableName in executableNames)
            {
                int pos = command.IndexOf(executableName, StringComparison.OrdinalIgnoreCase);
                if (pos > 0)
                {
                    AddExecutableCandidate(candidates, command.Substring(0, pos + executableName.Length).Trim());
                }
            }
        }

        private static string SelectNewestExecutable(List<string> candidates)
        {
            string selected = null;
            Version selectedVersion = new Version(0, 0, 0, 0);
            foreach (string candidate in candidates)
            {
                try
                {
                    FileVersionInfo info = FileVersionInfo.GetVersionInfo(candidate);
                    Version version = new Version(
                        Math.Max(0, info.FileMajorPart),
                        Math.Max(0, info.FileMinorPart),
                        Math.Max(0, info.FileBuildPart),
                        Math.Max(0, info.FilePrivatePart));
                    if (selected == null || version.CompareTo(selectedVersion) > 0)
                    {
                        selected = candidate;
                        selectedVersion = version;
                    }
                }
                catch
                {
                    if (selected == null)
                    {
                        selected = candidate;
                    }
                }
            }
            return selected;
        }

        private static string[] GetProgramFilesRoots()
        {
            return new string[]
            {
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                Environment.GetEnvironmentVariable("ProgramFiles(x86)")
            };
        }

        /// <summary>
        /// インストール済みSSMSのバージョンを検出する。
        /// </summary>
        private void DetectInstalledSSMSVersion(string ssmsExePath)
        {
            FileVersionInfo info = FileVersionInfo.GetVersionInfo(ssmsExePath);
            this.SSMSVersion = info.FileMajorPart;
            this.installedSSMSPath = Path.GetDirectoryName(ssmsExePath);
        }

        /// <summary>
        /// インストール済みの ssms.exe を探索し、最も新しい実行ファイルを返す。
        /// </summary>
        public string FindSSMSExePath()
        {
            List<string> candidates = new List<string>();
            string[] executableNames = new string[] { "ssms.exe" };

            for (int version = MaxVer; version >= MinVer; version--)
            {
                try
                {
                    using (RegistryKey key = Registry.ClassesRoot.OpenSubKey(
                        string.Format(@"ssms.ssmssln.{0}.0\Shell\Open\Command", version)))
                    {
                        if (key != null)
                        {
                            AddExecutableCandidatesFromCommand(candidates, key.GetValue("") as string, executableNames);
                        }
                    }
                }
                catch { }
            }

            try
            {
                using (RegistryKey key = Registry.ClassesRoot.OpenSubKey(@"ssms.ssmssln\Shell\Open\Command"))
                {
                    if (key != null)
                    {
                        AddExecutableCandidatesFromCommand(candidates, key.GetValue("") as string, executableNames);
                    }
                }
            }
            catch { }

            foreach (string root in GetProgramFilesRoots())
            {
                if (string.IsNullOrEmpty(root) || !Directory.Exists(root))
                {
                    continue;
                }

                try
                {
                    foreach (string installDir in Directory.GetDirectories(root, "Microsoft SQL Server Management Studio*"))
                    {
                        AddExecutableCandidate(candidates, Path.Combine(Path.Combine(Path.Combine(Path.Combine(installDir, "Release"), "Common7"), "IDE"), "ssms.exe"));
                        AddExecutableCandidate(candidates, Path.Combine(Path.Combine(Path.Combine(installDir, "Common7"), "IDE"), "ssms.exe"));
                    }
                }
                catch { }
            }

            try
            {
                if (!string.IsNullOrEmpty(this.regkey))
                {
                    using (RegistryKey key = Registry.LocalMachine.OpenSubKey(this.regkey))
                    {
                        if (key != null)
                        {
                            foreach (string valueName in new string[] { "Path", "InstallPath", "ClientDirectory", "ConsoleRootDir" })
                            {
                                string basePath = key.GetValue(valueName) as string;
                                if (string.IsNullOrEmpty(basePath))
                                {
                                    continue;
                                }
                                AddExecutableCandidate(candidates, Path.Combine(Path.Combine(basePath, this.BinDir ?? string.Empty), "ssms.exe"));
                                AddExecutableCandidate(candidates, Path.Combine(Path.Combine(Path.Combine(basePath, this.BinDir ?? string.Empty), "ManagementStudio"), "ssms.exe"));
                            }
                        }
                    }
                }
            }
            catch { }

            string pathEnvironment = Environment.GetEnvironmentVariable("PATH") ?? string.Empty;
            foreach (string folder in pathEnvironment.Split(Path.PathSeparator))
            {
                try
                {
                    AddExecutableCandidate(candidates, Path.Combine(folder, "ssms.exe"));
                }
                catch { }
            }

            string selected = SelectNewestExecutable(candidates);
            if (!string.IsNullOrEmpty(selected))
            {
                DetectInstalledSSMSVersion(selected);
            }
            return selected;
        }

        /// <summary>
        /// 実際に起動するSSMSのバージョンに対応した引数を生成する。
        /// </summary>
        public string BuildSSMSParameters(
            string serverRealName,
            string instanceName,
            bool isUseTrust,
            string dbName,
            string logOnUserId,
            string logOnPassword,
            bool isUseEncryption,
            bool ignoreCertificateError)
        {
            List<string> arguments = new List<string>();
            string serverName = string.IsNullOrEmpty(instanceName)
                ? serverRealName
                : serverRealName + "\\" + instanceName;

            arguments.Add("-S");
            arguments.Add(QuoteCommandLineArgument(serverName));

            if (!string.IsNullOrEmpty(dbName))
            {
                arguments.Add("-d");
                arguments.Add(QuoteCommandLineArgument(dbName));
            }

            if (isUseTrust)
            {
                if (this.SSMSVersion >= 22)
                {
                    arguments.Add("-A");
                    arguments.Add("ActiveDirectoryIntegrated");
                }
                else
                {
                    arguments.Add("-E");
                }
            }
            else
            {
                arguments.Add("-U");
                arguments.Add(QuoteCommandLineArgument(logOnUserId));
                if (this.SSMSVersion > 0 && this.SSMSVersion < 18 && !string.IsNullOrEmpty(logOnPassword))
                {
                    arguments.Add("-P");
                    arguments.Add(QuoteCommandLineArgument(logOnPassword));
                }
            }

            if (this.SSMSVersion >= 18)
            {
                arguments.Add("-N");
                arguments.Add(isUseEncryption ? "Mandatory" : "Optional");
                if (ignoreCertificateError)
                {
                    arguments.Add("-C");
                }
            }

            arguments.Add("-nosplash");
            return string.Join(" ", arguments.ToArray());
        }

        /// <summary>
        /// SSMSを起動する。探索・バージョン判定・引数生成をこのクラスで一元管理する。
        /// </summary>
        public void LaunchSSMS(
            string serverRealName,
            string instanceName,
            bool isUseTrust,
            string dbName,
            string logOnUserId,
            string logOnPassword,
            bool isUseEncryption,
            bool ignoreCertificateError)
        {
            string ssmsPath = FindSSMSExePath();
            if (string.IsNullOrEmpty(ssmsPath))
            {
                throw new FileNotFoundException("SQL Server Management Studio (ssms.exe) が見つかりません。");
            }

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = ssmsPath;
            startInfo.Arguments = BuildSSMSParameters(
                serverRealName, instanceName, isUseTrust, dbName, logOnUserId, logOnPassword,
                isUseEncryption, ignoreCertificateError);
            startInfo.UseShellExecute = false;
            startInfo.WindowStyle = ProcessWindowStyle.Maximized;
            Process.Start(startInfo);
        }

        /// <summary>
        /// インストール済みの SQL Profiler 実行ファイルを探索してフルパスを返す。
        /// </summary>
        public string FindProfilerExePath()
        {
            List<string> candidates = new List<string>();
            string[] executableNames = new string[] { "profiler.exe", "profiler90.exe" };

            foreach (string commandKey in new string[]
            {
                @"SQLServerProfilerTraceData\shell\open\command",
                @".trc\shell\open\command"
            })
            {
                try
                {
                    using (RegistryKey key = Registry.ClassesRoot.OpenSubKey(commandKey))
                    {
                        if (key != null)
                        {
                            AddExecutableCandidatesFromCommand(candidates, key.GetValue("") as string, executableNames);
                        }
                    }
                }
                catch { }
            }

            string ssmsPath = FindSSMSExePath();
            if (!string.IsNullOrEmpty(ssmsPath))
            {
                string ideDirectory = Path.GetDirectoryName(ssmsPath);
                AddExecutableCandidate(candidates, Path.Combine(ideDirectory, "profiler.exe"));
                DirectoryInfo common7Directory = Directory.GetParent(ideDirectory);
                if (common7Directory != null)
                {
                    AddExecutableCandidate(candidates, Path.Combine(common7Directory.FullName, "profiler.exe"));
                }
            }

            foreach (string root in GetProgramFilesRoots())
            {
                if (string.IsNullOrEmpty(root) || !Directory.Exists(root))
                {
                    continue;
                }

                try
                {
                    foreach (string installDir in Directory.GetDirectories(root, "Microsoft SQL Server Management Studio*"))
                    {
                        AddExecutableCandidate(candidates, Path.Combine(Path.Combine(Path.Combine(installDir, "Release"), "Common7"), "profiler.exe"));
                        AddExecutableCandidate(candidates, Path.Combine(Path.Combine(installDir, "Common7"), "profiler.exe"));
                    }
                }
                catch { }

                try
                {
                    string sqlRoot = Path.Combine(root, "Microsoft SQL Server");
                    if (Directory.Exists(sqlRoot))
                    {
                        foreach (string versionDir in Directory.GetDirectories(sqlRoot))
                        {
                            foreach (string executableName in executableNames)
                            {
                                AddExecutableCandidate(candidates, Path.Combine(Path.Combine(Path.Combine(versionDir, "Tools"), "Profiler"), executableName));
                                AddExecutableCandidate(candidates, Path.Combine(Path.Combine(Path.Combine(versionDir, "Tools"), "Binn"), executableName));
                            }
                        }
                    }
                }
                catch { }
            }

            string pathEnvironment = Environment.GetEnvironmentVariable("PATH") ?? string.Empty;
            foreach (string folder in pathEnvironment.Split(Path.PathSeparator))
            {
                foreach (string executableName in executableNames)
                {
                    try
                    {
                        AddExecutableCandidate(candidates, Path.Combine(folder, executableName));
                    }
                    catch { }
                }
            }

            return SelectNewestExecutable(candidates);
        }

        /// <summary>
        /// インストール済みの SQL Server 構成マネージャーを検索する。
        /// 接続先と同じメジャーバージョンを優先し、見つからない場合は新しい版へフォールバックする。
        /// </summary>
        public string FindSqlServerConfigurationManagerPath()
        {
            List<string> fileNames = new List<string>();
            int preferredVersion;
            string majorVersion = (this.pFullVersionString ?? string.Empty).Split('.')[0];
            if (int.TryParse(majorVersion, out preferredVersion))
            {
                if (preferredVersion >= 10)
                {
                    fileNames.Add(string.Format(System.Globalization.CultureInfo.InvariantCulture, "SQLServerManager{0}.msc", preferredVersion));
                }
                else if (preferredVersion == 9)
                {
                    fileNames.Add("SQLServerManager90.msc");
                    fileNames.Add("SQLServerManager.msc");
                }
            }

            for (int version = MaxVer; version >= 10; version--)
            {
                string fileName = string.Format(System.Globalization.CultureInfo.InvariantCulture, "SQLServerManager{0}.msc", version);
                if (!fileNames.Contains(fileName))
                {
                    fileNames.Add(fileName);
                }
            }
            if (!fileNames.Contains("SQLServerManager90.msc"))
            {
                fileNames.Add("SQLServerManager90.msc");
            }
            if (!fileNames.Contains("SQLServerManager.msc"))
            {
                fileNames.Add("SQLServerManager.msc");
            }

            string windowsDirectory = Environment.GetEnvironmentVariable("WINDIR");
            if (string.IsNullOrEmpty(windowsDirectory))
            {
                DirectoryInfo systemDirectory = Directory.GetParent(Environment.SystemDirectory);
                windowsDirectory = systemDirectory == null ? string.Empty : systemDirectory.FullName;
            }

            foreach (string directoryName in new string[] { "SysWOW64", "System32" })
            {
                foreach (string fileName in fileNames)
                {
                    string candidate = Path.Combine(Path.Combine(windowsDirectory, directoryName), fileName);
                    if (File.Exists(candidate))
                    {
                        return candidate;
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// SQL Server 構成マネージャーを起動する。
        /// </summary>
        public void LaunchSqlServerConfigurationManager()
        {
            string configurationManagerPath = FindSqlServerConfigurationManagerPath();
            if (string.IsNullOrEmpty(configurationManagerPath))
            {
                throw new FileNotFoundException("SQL Server Configuration Manager snap-in was not found.");
            }

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = configurationManagerPath;
            startInfo.UseShellExecute = true;
            Process.Start(startInfo);
        }
        /// <summary>
        /// SQL Profilerを公式のスラッシュ形式の引数で起動する。
        /// </summary>
        public bool LaunchProfiler(string serverRealName, string instanceName, bool useIntegrated, string dbName, string user, string password)
        {
            try
            {
                string profilerPath = FindProfilerExePath();
                if (string.IsNullOrEmpty(profilerPath))
                {
                    return false;
                }

                string serverName = string.IsNullOrEmpty(instanceName)
                    ? serverRealName
                    : serverRealName + "\\" + instanceName;
                List<string> arguments = new List<string>();
                arguments.Add("/S");
                arguments.Add(QuoteCommandLineArgument(serverName));

                if (useIntegrated)
                {
                    arguments.Add("/E");
                }
                else
                {
                    arguments.Add("/U");
                    arguments.Add(QuoteCommandLineArgument(user));
                    arguments.Add("/P");
                    arguments.Add(QuoteCommandLineArgument(password));
                }

                if (!string.IsNullOrEmpty(dbName))
                {
                    arguments.Add("/D");
                    arguments.Add(QuoteCommandLineArgument(dbName));
                }

                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = profilerPath;
                startInfo.Arguments = string.Join(" ", arguments.ToArray());
                startInfo.UseShellExecute = false;
                startInfo.WindowStyle = ProcessWindowStyle.Maximized;
                Process.Start(startInfo);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
