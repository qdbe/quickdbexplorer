using System;
using System.Data;
using System.Data.SqlClient;

namespace quickDBExplorer
{
	/// <summary>
	/// ISqlInterface の概要の説明です。
	/// </summary>
	public interface ISqlInterface
	{
		void SetConnection(SqlConnection sqlConnection1);

		string GetDBSelect();

		string GetOwnerLabel1();
		string GetOwnerLabel2();
		string GetFieldListSelect(string tbname, string []tbnamelist);
	}
}
