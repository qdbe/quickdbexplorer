using System;
using System.Data;
using System.Data.SqlClient;

namespace quickDBExplorer
{
	/// <summary>
	/// SqlServer2000 の概要の説明です。
	/// </summary>
	public class SqlServerDriver : ISqlInterface
	{

		System.Data.SqlClient.SqlConnection sqlConnect;

		public SqlServerDriver()
		{
			// 
			// TODO: コンストラクタ ロジックをここに追加してください。
			//
		}

		#region ISqlInterface メンバ

		public void SetConnection(System.Data.SqlClient.SqlConnection sqlConnection1)
		{
			this.sqlConnect = sqlConnection1;
			// TODO:  SqlServer2000.SetConnection 実装を追加します。
		}

		public string GetDBSelect()
		{
			return "SELECT name FROM sysdatabases order by name";
		}

		/// <summary>
		/// テーブル一覧のカラムヘッダの表示文字を取得する
		/// </summary>
		/// <returns></returns>
		public string GetTbListColName()
		{
			return "Table/View";
		}


		public string GetOwnerLabel1()
		{
			return "owner/Role(&O)";
		}

		public string GetOwnerLabel2()
		{
			return "オーナー名・テーブル名";
		}

		public string GetFieldListSelect(string tbname, string []str)
		{
			return "select syscolumns.name colname, systypes.name valtype, syscolumns.length, syscolumns.prec, syscolumns.xscale, syscolumns.colid, syscolumns.colorder, syscolumns.isnullable, syscolumns.collation, sysobjects.id  from sysobjects, syscolumns, sysusers, systypes where sysobjects.id = syscolumns.id and sysobjects.uid= sysusers.uid and syscolumns.xusertype=systypes.xusertype and sysusers.name = '" + str[0] +"' and sysobjects.name = '" + str[1] + "' order by syscolumns.colorder";
		}

		/// <summary>
		/// オブジェクト一覧の表示用SQLの取得
		/// </summary>
		/// <param name="isDspView">View を表示させるか否か true: 表示する false: 表示させない</param>
		/// <param name="ownerList">特定のOwnerのテーブルのみ表示する場合は IN句に利用するカンマ区切り文字列を渡す</param>
		/// <returns></returns>
		public string GetDspObjList(bool isDspTable, bool isDspView, bool Synonym, bool isDspFunc, bool isDspSP, string ownerList)
		{
			string retsql = "";
			if( isDspView == true )
			{
				retsql = @"select 
					sysobjects.name as tbname, 
					sysusers.name as uname ,
					case
					when xtype = 'U' then ' '
					else				'V'
					end as tvs,
					sysobjects.crdate as cretime
					from sysobjects, sysusers 
					where ( xtype='U' or xtype='V' ) and sysobjects.uid = sysusers.uid ";
			}
			else
			{
				retsql = @"select 
					sysobjects.name as tbname, 
					sysusers.name as uname ,
					' ' as tvs,
					sysobjects.crdate as cretime
					from sysobjects, sysusers where xtype='U' and sysobjects.uid = sysusers.uid ";
			}

			if( ownerList != null && 
				ownerList != string.Empty )
			{
				retsql += " and sysusers.name in ( " + ownerList + " ) ";
			}

			return retsql;
		}


		#endregion
	}
}
