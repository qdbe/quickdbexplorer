using System;
using System.Data;
using System.Data.SqlClient;

namespace quickDBExplorer
{
	/// <summary>
	/// SqlServer2005 の概要の説明です。
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
		}

		public string GetDBSelect()
		{
			return "SELECT name FROM sys.databases  order by name";;
		}

		/// <summary>
		/// テーブル一覧のカラムヘッダの表示文字を取得する
		/// </summary>
		/// <returns></returns>
		public string GetTbListColName()
		{
			return "Table/View/Synonym";
		}


		public string GetOwnerLabel1()
		{
			return "Schema(&O)";
		}

		public string GetOwnerLabel2()
		{
			return "スキーマ名・テーブル名";
		}

		public string GetFieldListSelect(string tbname, string []str)
		{
			string sqlstr = string.Format( @"select OBJECT_ID(base_object_name) from sys.synonyms 
	inner join sys.schemas on sys.synonyms.schema_id= sys.schemas.schema_id 
	where
	sys.schemas.name = '{0}' and 
	sys.synonyms.name = '{1}' ",
				str[0],
				str[1]
				);
			SqlDataAdapter dasyn = new SqlDataAdapter(sqlstr, this.sqlConnect);
			DataSet dssyn = new DataSet();
			dssyn.CaseSensitive = true;
			dasyn.Fill(dssyn,tbname);
			if( dssyn.Tables[tbname].Rows.Count > 0 &&
				(int)dssyn.Tables[tbname].Rows[0][0] != 0 )
			{
				// Synonym 
				sqlstr = string.Format(
					@"select 
	sys.all_columns.name colname, 
	st.name valtype, 
	convert(smallint,sys.all_columns.max_length) as length, 
	convert(smallint,
	CASE 
	WHEN st.user_type_id = st.system_type_id and st.name IN (N'nchar', N'nvarchar') THEN sys.all_columns.max_length/2 
	WHEN st.user_type_id != st.system_type_id and baset.name IN (N'nchar', N'nvarchar') THEN sys.all_columns.max_length/2 
	ELSE sys.all_columns.precision	
	end ) as [prec], 
	convert(smallint,sys.all_columns.scale) as xscale, 
	sys.all_columns.column_id as colid, 
	sys.all_columns.column_id as colorder, 
	convert(int,sys.all_columns.is_nullable) as isnullable, 
	sys.all_columns.collation_name as collation, 
	sys.all_objects.object_id as id
from 
	sys.all_objects 
	inner join sys.all_columns on sys.all_objects.object_id = sys.all_columns.object_id 
	inner join sys.types st on sys.all_columns.user_type_id  = st.user_type_id  
	LEFT OUTER JOIN sys.types AS baset ON 
		baset.user_type_id = st.system_type_id and baset.user_type_id = baset.system_type_id
where 
	sys.all_objects.object_id = {0} 
order by colorder",
					(int)dssyn.Tables[tbname].Rows[0][0]
					);
			}
			else
			{
				// Not Synonym

	

				sqlstr = string.Format(
					@"select 
	sys.all_columns.name colname, 
	st.name valtype, 
	convert(smallint,sys.all_columns.max_length) as length, 
	convert(smallint,
	CASE 
	WHEN st.user_type_id = st.system_type_id and st.name IN (N'nchar', N'nvarchar') THEN sys.all_columns.max_length/2 
	WHEN st.user_type_id != st.system_type_id and baset.name IN (N'nchar', N'nvarchar') THEN sys.all_columns.max_length/2 
	ELSE sys.all_columns.precision	
	end ) as [prec], 
	convert(smallint,sys.all_columns.scale) as xscale, 
	sys.all_columns.column_id as colid, 
	sys.all_columns.column_id as colorder, 
	convert(int,sys.all_columns.is_nullable) as isnullable, 
	sys.all_columns.collation_name as collation, 
	sys.all_objects.object_id as id
from 
	sys.all_objects 
	inner join sys.all_columns on sys.all_objects.object_id = sys.all_columns.object_id 
	inner join sys.schemas on sys.all_objects.schema_id= sys.schemas.schema_id 
	inner join sys.types st on sys.all_columns.user_type_id  = st.user_type_id  
	LEFT OUTER JOIN sys.types AS baset ON 
		baset.user_type_id = st.system_type_id and baset.user_type_id = baset.system_type_id
where 
	sys.schemas.name = '{0}' and 
	sys.all_objects.name = '{1}' 
order by colorder",
					str[0],
					str[1]
					);
			}
			return sqlstr;
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
	sys.all_objects.name as tbname, 
	sys.schemas.name as uname ,
	case
	when sys.all_objects.type = 'U' then ' '
	when sys.all_objects.type = 'V' then 'V'
	when sys.all_objects.type = 'SN' then 'S'
	end as tvs,
	sys.all_objects.create_date as cretime
from 
	sys.all_objects, 
	sys.schemas 
where 
	( sys.all_objects.type='U' 
	  or sys.all_objects.type='V' 
	  or
	  (sys.all_objects.type='SN' and 
		exists ( select 'X' from sys.synonyms t2 
			inner join sys.all_objects t3 on
			OBJECT_ID(t2.base_object_name) = t3.object_id
			and (t3.type='U' or t3.type='V' )
				where
				sys.all_objects.object_id = t2.object_id 
				)
	   )
	) and 
	sys.all_objects.schema_id = sys.schemas.schema_id";
			}
			else
			{
				retsql = @"select 
	sys.all_objects.name as tbname, 
	sys.schemas.name as uname ,
	case
	when sys.all_objects.type = 'U' then ' '
	when sys.all_objects.type = 'V' then 'V'
	when sys.all_objects.type = 'SN' then 'S'
	end as tvs
from 
	sys.all_objects, 
	sys.schemas 
where 
	( sys.all_objects.type='U' 
	  or
	  (sys.all_objects.type='SN' and 
		exists ( select 'X' from sys.synonyms t2 
			inner join sys.all_objects t3 on
			OBJECT_ID(t2.base_object_name) = t3.object_id
			and (t3.type='U')
				where
				sys.all_objects.object_id = t2.object_id 
				)
	   )
	) and 
	sys.all_objects.schema_id = sys.schemas.schema_id";
			}

			if( ownerList != null && 
				ownerList != string.Empty )
			{
				retsql += " and sys.schemas.name in ( " + ownerList + " ) ";
			}

			return retsql;
		}

		#endregion
	}
}
