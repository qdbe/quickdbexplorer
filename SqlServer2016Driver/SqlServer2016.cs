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
    public class SqlServerDriver2016 : SqlServerDriver2014
    {
        public SqlServerDriver2016()
        {
        }

        public override void CallEPM(string serverRealName, string instanceName, bool isUseTrust, string dbName, string logOnUserId, string logOnPassword)
        {
            if (instanceName == null)
            {
                throw new ArgumentNullException("instanceName");
            }
            if (dbName == null)
            {
                throw new ArgumentNullException("dbName");
            }
            string binPath = GetManagementStudioPath();

            Process iProcess = new Process();
            iProcess.StartInfo.FileName = Path.Combine( binPath ,this.sqlVersion.ManagementExe);
            iProcess.StartInfo.Arguments = GetManagementStudioParam(serverRealName, instanceName, isUseTrust, dbName, logOnUserId, logOnPassword);

            try
            {
                iProcess.Start();
            }
            catch
            {
                try
                {
                    iProcess.StartInfo.FileName = iProcess.StartInfo.FileName.Replace("Program Files", "Program Files (x86)");
                    iProcess.Start();
                }
                catch
                {
                    iProcess.StartInfo.FileName = this.sqlVersion.ManagementExe;
                    iProcess.StartInfo.ErrorDialog = true;
                    iProcess.Start();
                }
            }
        }

        protected override string GetManagementStudioPath()
        {
            return GetBinPath();
        }

        /// <summary>
        /// SSMSが別になったのでそのパスを取得
        /// </summary>
        /// <returns></returns>
        protected override string GetBinPath()
        {
            for (int i = SqlVersion.MaxVer;  i >= SqlVersion.MinVer; i--)
            {
                Microsoft.Win32.RegistryKey rkey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(
                    string.Format(
                        @"SOFTWARE\Classes\ssms.ssmssln.{0}.0\Shell\Open\Command",
                        i), 
                    false);
                string binPath = string.Empty;
                if (rkey != null)
                {
                    string rstr = string.Empty;
                    object robj = rkey.GetValue("");
                    if (robj != null)
                    {
                        rstr = robj.ToString();
                    }
                    if (rstr != null)
                    {
                        // "C:\Program Files (x86)\Microsoft SQL Server Management Studio 18\Common7\IDE\ssms.exe" "%1"
                        // の形式で登録されている
                        string[] bstr = rstr.Split("\"".ToCharArray());
                        if (bstr.Length == 5)
                        {
                            if (File.Exists(bstr[1]))
                            {
                                FileInfo fi = new FileInfo(bstr[1]);
                                binPath = fi.Directory.FullName;
                                return binPath;
                            }
                        }
                    }
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// プロファイラーのパスを取得する
        /// </summary>
        /// <returns></returns>
        protected override string GetProfilerPath()
        {
            Microsoft.Win32.RegistryKey rkey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(
                @"SOFTWARE\Classes\SQLServerProfilerTraceData\shell\open\command",
                false);
            string binPath = string.Empty;
            if (rkey != null)
            {
                string rstr = string.Empty;
                object robj = rkey.GetValue("");
                if (robj != null)
                {
                    rstr = robj.ToString();
                }
                if (rstr != null)
                {
                    // "C:\Program Files (x86)\Microsoft SQL Server Management Studio 18\Common7\profiler.exe" /f"%1"
                    // の形式で登録されている
                    string[] bstr = rstr.Split("\"".ToCharArray());
                    if (bstr.Length == 5)
                    {
                        if (File.Exists(bstr[1]))
                        {
                            FileInfo fi = new FileInfo(bstr[1]);
                            binPath = fi.Directory.FullName;
                            return binPath;
                        }
                    }
                }
            }
            return string.Empty;
        }


        public override void CallProfile(string serverRealName, string instanceName, bool isUseTrust, string dbName, string logOnUserId, string logOnPassword)
        {
            if (instanceName == null)
            {
                throw new ArgumentNullException("instanceName");
            }
            if (dbName == null)
            {
                throw new ArgumentNullException("dbName");
            }

            string binPath = GetProfilerPath();

            Process iProcess = new Process();
            iProcess.StartInfo.FileName = Path.Combine(binPath,this.sqlVersion.ProfilerExe);
            iProcess.StartInfo.ErrorDialog = false;
            string serverstr = "";
            if (instanceName.Length != 0)
            {
                serverstr = serverRealName + "\\" + instanceName;
            }
            else
            {
                serverstr = serverRealName;
            }
            if (isUseTrust == true)
            {
                if (dbName.Length != 0)
                {
                    iProcess.StartInfo.Arguments = string.Format(System.Globalization.CultureInfo.CurrentCulture, "/S{0} /D{1} /E ",
                        serverstr,
                        dbName
                        );
                }
                else
                {
                    iProcess.StartInfo.Arguments = string.Format(System.Globalization.CultureInfo.CurrentCulture, "/S{0} /E ",
                        serverstr
                        );
                }
            }
            else
            {
                if (dbName.Length != 0)
                {
                    iProcess.StartInfo.Arguments = string.Format(System.Globalization.CultureInfo.CurrentCulture, " /S{0} /D{1} /U{2} /P{3} ",
                        serverstr,
                        dbName,
                        logOnUserId,
                        logOnPassword);
                }
                else
                {
                    iProcess.StartInfo.Arguments = string.Format(System.Globalization.CultureInfo.CurrentCulture, " /S{0} /U{1} /P{2} ",
                        serverstr,
                        logOnUserId,
                        logOnPassword);
                }
            }
            iProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            try
            {
                iProcess.Start();
            }
            catch
            {
                try
                {
                    iProcess.StartInfo.FileName = iProcess.StartInfo.FileName.Replace("Program Files", "Program Files (x86)");
                    iProcess.Start();
                }
                catch
                {
                    iProcess.StartInfo.FileName = this.sqlVersion.ProfilerExe;
                    iProcess.StartInfo.ErrorDialog = true;
                    iProcess.Start();
                }
            }
        }
    }
}