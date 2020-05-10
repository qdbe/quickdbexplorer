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
	/// SqlServer2008 �̊T�v�̐����ł��B
	/// </summary>
	public class SqlServerDriver2008 : SqlServerDriver2005
	{
        public SqlServerDriver2008()
        {
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
            string binPath = GetManagementStudioPath();
            
            Process iProcess = new Process();
            iProcess.StartInfo.FileName = binPath + this.sqlVersion.ManagementExe;
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

        protected virtual string GetManagementStudioParam(string serverRealName, string instanceName, bool isUseTrust, string dbName, string logOnUserId, string logOnPassword)
        {
            string result = string.Empty;
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
                    result = string.Format(System.Globalization.CultureInfo.CurrentCulture, " -S {0} -d {1} -E -nosplash",
                        serverstr,
                        dbName
                        );
                }
                else
                {
                    result = string.Format(System.Globalization.CultureInfo.CurrentCulture, " -S {0} -E -nosplash",
                        serverstr
                        );
                }
            }
            else
            {
                if (dbName.Length != 0)
                {
                    result = string.Format(System.Globalization.CultureInfo.CurrentCulture, " -S {0} -d {1} -U {2} -P {3} -nosplash",
                        serverstr,
                        dbName,
                        logOnUserId,
                        logOnPassword);
                }
                else
                {
                    result = string.Format(System.Globalization.CultureInfo.CurrentCulture, " -S {0} -U {1} -P {2} -nosplash",
                        serverstr,
                        logOnUserId,
                        logOnPassword);
                }
            }

            return result;
        }


        /// <summary>
        /// �}�l�[�W�����g�X�^�W�I�̃p�X���擾����
        /// </summary>
        /// <returns></returns>
        protected virtual string GetManagementStudioPath()
        {
            string binPath = GetBinPath();
            if (binPath != string.Empty)
            {
                string msdir = binPath + @"ManagementStudio\";
                if (Directory.Exists(msdir))
                {
                    binPath = msdir;
                }
            }
            return binPath;
        }

		/// <summary>
		/// Profiler���N������
		/// </summary>
		/// <param name="serverRealName">�T�[�o�[��</param>
		/// <param name="instanceName">�C���X�^���X��</param>
		/// <param name="isUseTrust">�M���֌W�ڑ��𗘗p���邩�ۂ�</param>
		/// <param name="dbName">�f�[�^�x�[�X��</param>
		/// <param name="logOnUserId">���O�C��ID</param>
		/// <param name="logOnPassword">���O�C���p�X���[�h</param>
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

            string binPath = GetProfilerPath();

			Process iProcess = new Process();
            iProcess.StartInfo.FileName = binPath + this.sqlVersion.ProfilerExe;
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
                    iProcess.StartInfo.FileName = this.sqlVersion.ProfilerExe;
                    iProcess.StartInfo.ErrorDialog = true;
                    iProcess.Start();
                }
            }
		}

        /// <summary>
        /// �v���t�@�C���[�̃p�X���擾����
        /// </summary>
        /// <returns></returns>
        protected virtual string GetProfilerPath()
        {
            return GetBinPath();
        }

        /// <summary>
        /// �c�[���p�X���擾����
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
		/// DataReader����DateTimeOffset�l��ǂݍ��ށB
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