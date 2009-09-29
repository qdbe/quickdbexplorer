using System;

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
		private int pPublicVersion;

		/// <summary>
		/// ���i���ɂ��o�[�W�����i2000,2005,2008��)
		/// </summary>
		public int PublicVersionNo
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
			return new SqlVersion("10.");
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
				this.pPublicVersion = 2000;
				this.pFullVersionString = versionStr;
				this.pIsSynonym = false;
				this.pCanUseQueryAnalyzer = true;
				this.pIsManagementStudio = false;
			}
			else if(versionStr.StartsWith("09") )
			{
				this.pPublicVersion = 2005;
				this.pFullVersionString = versionStr;
				this.pIsSynonym = true;
				this.pCanUseQueryAnalyzer = false;
				this.pIsManagementStudio = true;
			}
			else
			{
				// ����ȊO��2008�Ɠ����Ƃ��Ă݂Ȃ���
				this.pPublicVersion = 2008;
				this.pFullVersionString = versionStr;
				this.pIsSynonym = true;
				this.pCanUseQueryAnalyzer = false;
				this.pIsManagementStudio = true;
			}
		}
	}
}
