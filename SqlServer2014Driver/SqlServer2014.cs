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
	/// SqlServer2005 �̊T�v�̐����ł��B
	/// </summary>
	public class SqlServerDriver2014 : SqlServerDriver2012
	{
        public SqlServerDriver2014()
        {
            this.regkey = @"SOFTWARE\Microsoft\Microsoft SQL Server\120\Tools\ClientSetup\";
        }
        /// <summary>
        /// EnterPriseManager���N������
        /// </summary>
        /// <param name="serverRealName">�T�[�o�[��</param>
        /// <param name="instanceName">�C���X�^���X��</param>
        /// <param name="isUseTrust">�M���֌W�ڑ��𗘗p���邩�ۂ�</param>
        /// <param name="dbName">�f�[�^�x�[�X��</param>
        /// <param name="logOnUserId">���O�C��ID</param>
        /// <param name="logOnPassword">���O�C���p�X���[�h</param>
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
            string binPath = GetBinPath();
            if (binPath != string.Empty)
            {
                binPath += @"ManagementStudio\";
            }

            Process iProcess = new Process();
            iProcess.StartInfo.FileName = binPath + "Ssms";
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
                    iProcess.StartInfo.Arguments = string.Format(System.Globalization.CultureInfo.CurrentCulture, " -S {0} -d {1} -E -nosplash",
                        serverstr,
                        dbName
                        );
                }
                else
                {
                    iProcess.StartInfo.Arguments = string.Format(System.Globalization.CultureInfo.CurrentCulture, " -S {0} -E -nosplash",
                        serverstr
                        );
                }
            }
            else
            {
                if (dbName.Length != 0)
                {
                    iProcess.StartInfo.Arguments = string.Format(System.Globalization.CultureInfo.CurrentCulture, " -S {0} -d {1} -U {2} -P {3} -nosplash",
                        serverstr,
                        dbName,
                        logOnUserId,
                        logOnPassword);
                }
                else
                {
                    iProcess.StartInfo.Arguments = string.Format(System.Globalization.CultureInfo.CurrentCulture, " -S {0} -U {1} -P {2} -nosplash",
                        serverstr,
                        logOnUserId,
                        logOnPassword);
                }
            }

            iProcess.StartInfo.ErrorDialog = false;
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
                    iProcess.StartInfo.FileName = "Ssms.exe";
                    iProcess.StartInfo.ErrorDialog = true;
                    iProcess.Start();
                }
            }
        }

        protected override string GetBinPath()
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
                        binPath += @"binn\";
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
    }
}