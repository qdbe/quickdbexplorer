using System;
using System.Data;
using System.Data.SqlClient;

namespace quickDBExplorer
{
	/// <summary>
	/// SqlServer2000 �̊T�v�̐����ł��B
	/// </summary>
	public class SqlServerDriver : ISqlInterface
	{

		System.Data.SqlClient.SqlConnection sqlConnect;

		public SqlServerDriver()
		{
			// 
			// TODO: �R���X�g���N�^ ���W�b�N�������ɒǉ����Ă��������B
			//
		}

		#region ISqlInterface �����o

		public void SetConnection(System.Data.SqlClient.SqlConnection sqlConnection1)
		{
			this.sqlConnect = sqlConnection1;
			// TODO:  SqlServer2000.SetConnection ������ǉ����܂��B
		}

		public string GetDBSelect()
		{
			return "SELECT name FROM sysdatabases order by name";
		}

		public string GetOwnerLabel1()
		{
			return "owner/Role(&O)";
		}

		public string GetOwnerLabel2()
		{
			return "�I�[�i�[���E�e�[�u����";
		}

		public string GetFieldListSelect(string tbname, string []str)
		{
			return "select syscolumns.name colname, systypes.name valtype, syscolumns.length, syscolumns.prec, syscolumns.xscale, syscolumns.colid, syscolumns.colorder, syscolumns.isnullable, syscolumns.collation, sysobjects.id  from sysobjects, syscolumns, sysusers, systypes where sysobjects.id = syscolumns.id and sysobjects.uid= sysusers.uid and syscolumns.xusertype=systypes.xusertype and sysusers.name = '" + str[0] +"' and sysobjects.name = '" + str[1] + "' order by syscolumns.colorder";
		}

		#endregion
	}
}
