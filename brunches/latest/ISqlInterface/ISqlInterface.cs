using System;
using System.Data;
using System.Data.SqlClient;

namespace quickDBExplorer
{
	/// <summary>
	/// ISqlInterface �̊T�v�̐����ł��B
	/// </summary>
	public interface ISqlInterface
	{
		void SetConnection(SqlConnection sqlConnection1);

		string GetDBSelect();

		string GetOwnerLabel1();
		string GetOwnerLabel2();
	}
}
