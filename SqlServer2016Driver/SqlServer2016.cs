using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading;

namespace quickDBExplorer
{
    /// <summary>
    /// SqlServer2016 の概要の説明です。
    /// </summary>
    public class SqlServerDriver2016 : SqlServerDriver2014
    {

        public SqlServerDriver2016()
        {
        }

        public override void CallEPM(string serverRealName, string instanceName, bool isUseTrust, string dbName, string logOnUserId, string logOnPassword, bool isUseEncryption, bool ignoreCertificateError)
        {
            if (instanceName == null)
            {
                throw new ArgumentNullException("instanceName");
            }
            if (dbName == null)
            {
                throw new ArgumentNullException("dbName");
            }

            this.sqlVersion.LaunchSSMS(
                serverRealName, instanceName, isUseTrust, dbName, logOnUserId, logOnPassword,
                isUseEncryption, ignoreCertificateError);
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

            if (!this.sqlVersion.LaunchProfiler(serverRealName, instanceName, isUseTrust, dbName, logOnUserId, logOnPassword))
            {
                throw new FileNotFoundException("SQL Server Profiler (profiler.exe) が見つからないか、起動できませんでした。");
            }
        }
    }
}