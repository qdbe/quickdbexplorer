using System;
using System.Collections.Generic;

namespace quickDBExplorer
{
	/// <summary>
	/// SqlVersion �̊T�v�̐����ł��B
	/// </summary>
	/// 
	public class SqlVersion
	{

		/// <summary>
		/// �o�[�W�����ԍ�������킷������
		/// </summary>
		private string pFullVersionString;
		/// <summary>
		/// �o�[�W�����ԍ�������킷������
		/// </summary>
		public string FullVersionString
		{
			get { return this.pFullVersionString; }
		}

		/// <summary>
		/// 2000,2005,2008 ��
		/// </summary>
		private string pPublicVersion;

		/// <summary>
		/// ���i���ɂ��o�[�W�����i2000,2005,2008��)
		/// </summary>
		public string PublicVersionNo
		{
			get { return this.pPublicVersion; }
		}

		/// <summary>
		/// �ڑ��pDLL�ɂ�����
		/// </summary>
		public string AdapterNameString
		{
			get { return this.pPublicVersion.ToString(); }
		}

		/// <summary>
		/// �Y���o�[�[�W������ Synonym �𗘗p�ł��邩
		/// </summary>
		private bool pIsSynonym = true;

		/// <summary>
		/// �Y���o�[�[�W������ Synonym �𗘗p�ł��邩
		/// </summary>
		public bool CanUseSynonym
		{
			get { return this.pIsSynonym; }
		}

		/// <summary>
		/// �N�G���A�i���C�U�[�𗘗p�\���ۂ�
		/// </summary>
		private bool pCanUseQueryAnalyzer = true;
		/// <summary>
		/// �N�G���A�i���C�U�[�𗘗p�\���ۂ�
		/// </summary>
		public bool CanUseQueryAnalyzer
		{
			get { return this.pCanUseQueryAnalyzer; }
		}

		/// <summary>
		/// Management Studio �ƌĂԂ��ۂ�
		/// </summary>
		private bool pIsManagementStudio = false;

		/// <summary>
		/// Management Studio �ƌĂԂ��ۂ�
		/// </summary>
		public bool IsManagementStudio
		{
			get { return pIsManagementStudio; }
			set { pIsManagementStudio = value; }
		}

        /// <summary>
        /// �v���t�@�C���[��EXE��
        /// </summary>
        public string ProfilerExe { get; set; }

        /// <summary>
        /// �}�l�[�W�����g�X�^�W�I��EXE��
        /// </summary>
        public string ManagementExe { get; set; }

        /// <summary>
        /// �c�[���p�X
        /// </summary>
        public string BinDir { get; set; }

        /// <summary>
        /// �ݒ�l�L�����W�X�g���p�X
        /// </summary>
        public string regkey { get; set; }

		/// <summary>
		/// SQL Server 2000 ��\���C���X�^���X�𐶐�����
		/// </summary>
		/// <returns></returns>
		public static SqlVersion SQLSERVER2000()
		{
			return new SqlVersion("08.00.2039");
		}

		/// <summary>
		/// SQL Server 2005 ��\���C���X�^���X�𐶐�����
		/// </summary>
		public static SqlVersion SQLSERVER2005()
		{
			return new SqlVersion("09.00.3054");
		}

		/// <summary>
		/// SQL Server 2008 ��\���C���X�^���X�𐶐�����
		/// </summary>
		public static SqlVersion SQLSERVER2008()
		{
			return new SqlVersion("10.00");
		}

        /// <summary>
        /// SQL Server 2008R2 ��\���C���X�^���X�𐶐�����
        /// </summary>
        public static SqlVersion SQLSERVER2008R2()
        {
            return new SqlVersion("10.50");
        }


        /// <summary>
        /// SQL Server 2012 ��\���C���X�^���X�𐶐�����
        /// </summary>
        public static SqlVersion SQLSERVER2012()
        {
            return new SqlVersion("11.0");
        }

        /// <summary>
        /// SQL Server 2014 ��\���C���X�^���X�𐶐�����
        /// </summary>
        public static SqlVersion SQLSERVER2014()
        {
            return new SqlVersion("12.0");
        }

        /// <summary>
        /// SQL Server 2016 ��\���C���X�^���X�𐶐�����
        /// </summary>
        public static SqlVersion SQLSERVER2016()
        {
            return new SqlVersion("13.0");
        }

        /// <summary>
        /// SQL Server 2017 ��\���C���X�^���X�𐶐�����
        /// </summary>
        public static SqlVersion SQLSERVER2017()
        {
            return new SqlVersion("14.0");
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="versionStr">Connection.ServerVersion �̌��ʂ�n��</param>
        public SqlVersion(string versionStr)
		{
			// Connection.ServerVersion �̌��ʂ��n�����̂ŁA�����Ŕ��f����
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
                // ����� 2017 �ɂ��Ă���
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
