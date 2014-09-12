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
	public class SqlServerDriver2008 : SqlServerDriver2005
	{
        protected string regkey = @"SOFTWARE\Microsoft\Microsoft SQL Server\100\Tools\ClientSetup\";

		/// <summary>
		/// EnterPriseManagerを起動する
		/// </summary>
		/// <param name="serverRealName">サーバー名</param>
		/// <param name="instanceName">インスタンス名</param>
		/// <param name="isUseTrust">信頼関係接続を利用するか否か</param>
		/// <param name="dbName">データベース名</param>
		/// <param name="logOnUserId">ログインID</param>
		/// <param name="logOnPassword">ログインパスワード</param>
		public override void	CallEPM(string serverRealName, string instanceName, bool isUseTrust, string dbName, string logOnUserId, string logOnPassword)
		{
			if( instanceName == null )
			{
				throw new ArgumentNullException("instanceName");
			}
			if( dbName == null )
			{
				throw new ArgumentNullException("dbName");
			}
            string binPath = GetBinPath();
            
            Process iProcess = new Process();
            iProcess.StartInfo.FileName = binPath + "Ssms";
			string serverstr = "";
			if( instanceName.Length != 0 )
			{
				serverstr = serverRealName + "\\" + instanceName;
			}
			else
			{
				serverstr = serverRealName;
			}
			if( isUseTrust == true )
			{
				if( dbName.Length != 0 )
				{
					iProcess.StartInfo.Arguments = string.Format(System.Globalization.CultureInfo.CurrentCulture," -S {0} -d {1} -E -nosplash",
						serverstr,
						dbName
						);
				}
				else
				{
					iProcess.StartInfo.Arguments = string.Format(System.Globalization.CultureInfo.CurrentCulture," -S {0} -E -nosplash",
						serverstr
						);
				}
			}
			else
			{
				if( dbName.Length != 0 )
				{
					iProcess.StartInfo.Arguments = string.Format(System.Globalization.CultureInfo.CurrentCulture," -S {0} -d {1} -U {2} -P {3} -nosplash",
						serverstr,
						dbName,
						logOnUserId,
						logOnPassword );
				}
				else
				{
					iProcess.StartInfo.Arguments = string.Format(System.Globalization.CultureInfo.CurrentCulture," -S {0} -U {1} -P {2} -nosplash",
						serverstr,
						logOnUserId,
						logOnPassword );
				}
			}

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
                    iProcess.StartInfo.FileName = "Ssms.exe";
                    iProcess.StartInfo.ErrorDialog = true;
                    iProcess.Start();
                }
            }
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
		public override void	CallProfile(string serverRealName, string instanceName, bool isUseTrust, string dbName, string logOnUserId, string logOnPassword)
		{
			if( instanceName == null )
			{
				throw new ArgumentNullException("instanceName");
			}
			if( dbName == null )
			{
				throw new ArgumentNullException("dbName");
			}

            string binPath = GetBinPath();

			Process iProcess = new Process();
			iProcess.StartInfo.FileName = binPath + "profiler.exe";
			iProcess.StartInfo.ErrorDialog = false;
			string serverstr = "";
			if( instanceName.Length != 0 )
			{
				serverstr = serverRealName + "\\" + instanceName;
			}
			else
			{
				serverstr = serverRealName;
			}
			if( isUseTrust == true )
			{
				if( dbName.Length != 0 )
				{
					iProcess.StartInfo.Arguments = string.Format(System.Globalization.CultureInfo.CurrentCulture,"/S{0} /D{1} /E ",
						serverstr,
						dbName
						);
				}
				else
				{
					iProcess.StartInfo.Arguments = string.Format(System.Globalization.CultureInfo.CurrentCulture,"/S{0} /E ",
						serverstr
						);
				}
			}
			else
			{
				if( dbName.Length != 0 )
				{
					iProcess.StartInfo.Arguments = string.Format(System.Globalization.CultureInfo.CurrentCulture," /S{0} /D{1} /U{2} /P{3} ",
						serverstr,
						dbName,
						logOnUserId,
						logOnPassword );
				}
				else
				{
					iProcess.StartInfo.Arguments = string.Format(System.Globalization.CultureInfo.CurrentCulture," /S{0} /U{1} /P{2} ",
						serverstr,
						logOnUserId,
						logOnPassword );
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
                    iProcess.StartInfo.FileName = "profiler.exe";
                    iProcess.StartInfo.ErrorDialog = true;
                    iProcess.Start();
                }
            }
		}

        /// <summary>
        /// ツールパスを取得する
        /// </summary>
        /// <returns></returns>
        protected virtual string GetBinPath()
        {
            Microsoft.Win32.RegistryKey rkey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(this.regkey, false);
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