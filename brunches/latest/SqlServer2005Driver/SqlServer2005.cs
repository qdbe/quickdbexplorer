using System;
using System.Data;
using System.Data.SqlClient;

namespace quickDBExplorer
{
	/// <summary>
	/// SqlServer2005 の概要の説明です。
	/// </summary>
	public class SqlServer2005 : ISqlInterface
	{

		System.Data.SqlClient.SqlConnection sqlConnect;

		public SqlServer2005()
		{
			// 
			// TODO: コンストラクタ ロジックをここに追加してください。
			//
		}
		#region ISqlInterface メンバ

		public void SetConnection(System.Data.SqlClient.SqlConnection sqlConnection1)
		{
			this.sqlConnect = sqlConnection1;
		}

		public string GetDBSelect()
		{
			return "SELECT name FROM sys.databases  order by name";;
		}

		public string GetOwnerLabel1()
		{
			return "Schema(&O)";
		}

		public string GetOwnerLabel2()
		{
			return "スキーマ名・テーブル名";
		}

		#endregion
	}
}
