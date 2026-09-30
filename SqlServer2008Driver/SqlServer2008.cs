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
	/// SqlServer2008 の概要の説明です。
	/// </summary>
	public class SqlServerDriver2008 : SqlServerDriver2005
	{
        public SqlServerDriver2008()
        {
        }


        /// <summary>
        /// EnterPriseManager を N 開く（暗号化・証明書検証オプション対応）
        /// </summary>
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
		/// <summary>
		/// Profilerを起動する
		/// </summary>
		/// <param name="serverRealName">サーバー名</param>
		/// <param name="instanceName">インスタンス名</param>
		/// <param name="isUseTrust">信頼関係接続を利用するか否か</param>
		/// <param name="dbName">データベース名</param>
		/// <param name="logOnUserId">ログインID</param>
		/// <param name="logOnPassword">ログインパスワード</param>
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
        /// <summary>
        /// ツールパスを取得する
        /// </summary>
        /// <returns></returns>
        protected virtual string GetBinPath()
        {
            Microsoft.Win32.RegistryKey rkey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(this.sqlVersion.regkey, false);
            string binPath = string.Empty;
            if (rkey != null)
            {
                bool isPathExists = true;
                object robj = rkey.GetValue("Path");
                if (robj != null)
                {
                    binPath = robj.ToString();
                }
                if (binPath == string.Empty)
                {
                    isPathExists = false;
                    robj = rkey.GetValue("SQLPath");
                    if (robj != null)
                    {
                        binPath = robj.ToString();
                    }
                }
                if (binPath != null)
                {
                    if (binPath.EndsWith(@"\") == false)
                    {
                        binPath += @"\";
                    }
                    if (isPathExists == false)
                    {
                        binPath += @"bin\";
                    }
                }
            }
            if (binPath != string.Empty)
            {
                if (!Directory.Exists(binPath))
                {
                    binPath = string.Empty;
                }
            }
            return binPath;
        }

		/// <summary>
		/// DataReaderからDateTimeOffset値を読み込む。
		/// </summary>
		/// <param name="dr"></param>
		/// <param name="col"></param>
		/// <returns></returns>
		public override DateTimeOffset GetDataReaderDateTimeOffSet(IDataReader dr, int col)
		{
			if (dr == null)
			{
				throw new ArgumentNullException("dr");
			}
			if (col < 0)
			{
				throw new ArgumentException("col must greater equal 0");
			}
			return ((SqlDataReader)dr).GetDateTimeOffset(col);
		}
	}
}