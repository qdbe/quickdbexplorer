using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

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

		public void SetConnection(IDbConnection sqlConnection1)
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

		public string GetFieldListSelect(DBObjectInfo dboInfo)
		{
			return string.Format(
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
	sys.all_objects.object_id = OBJECT_ID('{0}')
order by colorder",
				dboInfo.RealObjName
				);
		}

		/// <summary>
		/// オブジェクト一覧の表示用SQLの取得
		/// </summary>
		/// <param name="isDspView">View を表示させるか否か true: 表示する false: 表示させない</param>
		/// <param name="ownerList">特定のOwnerのテーブルのみ表示する場合は IN句に利用するカンマ区切り文字列を渡す</param>
		/// <returns></returns>
		public string GetDspObjList(bool isDspTable, bool isDspView, bool isDspSyn, bool isDspFunc, bool isDspSP, string ownerList)
		{
			string retsql = "";

			string destObj = "";

			ArrayList ar = new ArrayList();

			if( isDspTable == true )
			{
				ar.Add("U");
			}

			if( isDspView == true )
			{
				ar.Add("V");
			}

			if( isDspSyn == true )
			{
				ar.Add("SN");
			}

			if( isDspFunc == true )
			{
				ar.Add("FN");
			}

			if( isDspSP == true )
			{
				ar.Add("P");
				ar.Add("PC");
			}

			if( ar.Count == 0 )
			{
				// 何も指定がなければ、テーブルのみにしておく
				ar.Add("U");
			}

			for( int i = 0; i < ar.Count; i++ )
			{
				if( i != 0 )
				{
					destObj += ",";
				}
				destObj += "'" + (string)ar[i] + "'";
			}

			retsql = string.Format(@"select 
	t1.name as tbname, 
	t2.name as uname ,
	t1.type as tvs,
	t1.create_date as cretime,
	isnull(t3.base_object_name,'') as synbase,
	isnull(t4.type, ' ') as syntype
from 
	sys.all_objects t1
	inner join 	sys.schemas t2 on 
		t1.schema_id = t2.schema_id
	left outer join sys.synonyms t3 on
		t1.object_id = t3.object_id 
	left outer join sys.all_objects t4 on
		OBJECT_ID(t3.base_object_name) = t4.object_id
where 
	( t1.type in ( {0} ) or
	( t1.type = 'SN' and 
	  t4.type in ( {0} ) ) )
",
				destObj );

			if( ownerList != null && 
				ownerList != string.Empty )
			{
				retsql += " and t2.name in ( " + ownerList + " ) ";
			}

			return retsql;
		}

		/// <summary>
		/// フィールドリスト取得時のダミークエリを生成する
		/// </summary>
		/// <returns></returns>
		public string GetDspFldListDummy()
		{
			return string.Format(
				@"select * from sys.indexes where 0=1" );
		}


		/// <summary>
		/// Owner の一覧を取得するSQLを生成する
		/// </summary>
		/// <param name="isDspSysUser"></param>
		/// <returns></returns>
		public string	GetOwnerList(bool isDspSysUser)
		{
			if( isDspSysUser )
			{
				return  "select * from sys.schemas order by name";
			}
			else
			{
				return @"select * from sys.schemas where name not in ( 'sys', 'INFORMATION_SCHEMA', 'guest', 'db_owner', 
							'db_accessadmin', 'db_securityadmin', 'db_ddladmin', 'db_backupoperator', 'db_datareader',
							'db_datawriter', 'db_denydatareader', 'db_denydatawriter'  ) order by name";
			}
		}

		/// <summary>
		/// テーブル情報取得の為のSQL文を生成する
		/// </summary>
		/// <param name="dboInfo"></param>
		/// <returns></returns>
		string	GetDDLTableSelectStr(DBObjectInfo dboInfo)
		{
			return string.Format(
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
	sys.all_objects.object_id = OBJECT_ID('{0}')
order by colorder",
				dboInfo.RealObjName
				);
		}


		/// <summary>
		/// オブジェクトに対するDROP 文を生成する
		/// </summary>
		/// <param name="dboInfo"></param>
		/// <returns></returns>
		public string	GetDDLDropStr(DBObjectInfo dboInfo)
		{
			switch( dboInfo.ObjType )
			{
				case	"U":
					return string.Format("DROP TABLE {0}{1}GO{1}{1}", dboInfo.FormalName,Environment.NewLine);
				case	"V":
					return string.Format("DROP VIEW {0}{1}GO{1}{1}", dboInfo.FormalName,Environment.NewLine);
				case	"SN":
					if( dboInfo.SynonymBaseType == "U" )
					{
						return string.Format("DROP TABLE {0}{1}GO{1}{1}", dboInfo.FormalName,Environment.NewLine);
					}
					else
					{
						return string.Format("DROP VIEW {0}{1}GO{1}{1}", dboInfo.FormalName,Environment.NewLine);
					}
				default:
					return	string.Empty;
			}
		}

		/// <summary>
		/// オブジェクトに対する Create 文を生成する
		/// </summary>
		/// <param name="dboInfo"></param>
		/// <param name="usekakko"></param>
		/// <returns></returns>
		public string	GetDDLCreateStr(DBObjectInfo dboInfo, bool usekakko)
		{
			StringBuilder strline =  new StringBuilder();
			TextWriter	wr = new StringWriter(strline);

			if( dboInfo.ObjType == "U" )
			{

				string sqlstr = this.GetDDLTableSelectStr(dboInfo);

				SqlDataAdapter da = new SqlDataAdapter(sqlstr, this.sqlConnect);
				DataSet ds = new DataSet();
				ds.CaseSensitive = true;
				da.Fill(ds,dboInfo.ToString());

				int		maxRow = ds.Tables[dboInfo.ToString()].Rows.Count;
				if( usekakko )
				{
					wr.Write("Create table {0} ", dboInfo.FormalName);
				}
				else
				{
					wr.Write("Create table {0} ", dboInfo.ToString());
				}
				wr.Write(" ( {0}",wr.NewLine);
				string	valtype;
				for( int i = 0; i < maxRow ; i++ )
				{
					if( i != 0 )
					{
						wr.Write(",{0}",wr.NewLine);
					}
					//フィールド名
					if( usekakko )
					{
						wr.Write("\t[{0}]", ds.Tables[dboInfo.ToString()].Rows[i][0]);
					}
					else
					{
						wr.Write("\t{0}", ds.Tables[dboInfo.ToString()].Rows[i][0]);
					}
					wr.Write("\t");
					// 型
					valtype = (string)ds.Tables[dboInfo.ToString()].Rows[i][1];

					wr.Write("\t");

					if( usekakko )
					{
						wr.Write("[{0}]",valtype);
					}
					else
					{
						wr.Write(valtype);
					}
					if( valtype == "varchar" ||
						valtype == "varbinary" ||
						valtype == "nvarchar" ||
						valtype == "char" ||
						valtype == "nchar" ||
						valtype == "binary" )
					{
						if( (Int16)ds.Tables[dboInfo.ToString()].Rows[i][3] == -1 )
						{
							wr.Write(" (max)");
						}
						else
						{
							wr.Write(" ({0})", ds.Tables[dboInfo.ToString()].Rows[i][3]);
						}
					}
					else if( valtype == "numeric" ||
						valtype == "decimal" )
					{
						wr.Write(" ({0},", ds.Tables[dboInfo.ToString()].Rows[i][3]);
						wr.Write("{0})", ds.Tables[dboInfo.ToString()].Rows[i][4]);
					}
					wr.Write("\t");
						
					if( !ds.Tables[dboInfo.ToString()].Rows[i].IsNull("collation"))
					{
						wr.Write("COLLATE {0}",ds.Tables[dboInfo.ToString()].Rows[i]["collation"]);
						wr.Write("\t");
					}
						
					if( (int)ds.Tables[dboInfo.ToString()].Rows[i]["isnullable"] == 0 )
					{
						wr.Write("\tNOT NULL");
					}
					else
					{
						wr.Write("\tNULL");
					}
				}
				wr.Write("{0}){0}Go{0}",wr.NewLine);
			}
			return strline.ToString();
		}

		/// <summary>
		/// オブジェクト情報をセットするDataTableを初期化する
		/// </summary>
		/// <param name="dt"></param>
		public void	InitObjTable(DataTable objTable)
		{
			objTable.Columns.Add("オブジェクトID");
			objTable.Columns.Add("オブジェクト名");
			objTable.Columns.Add("所有者");
			objTable.Columns.Add("オブジェクトの型");
			objTable.Columns.Add("作成日時");
			objTable.Columns.Add("更新日時");
			objTable.Columns.Add("シノニム参照先");
			objTable.Columns.Add("シノニム参照先オブジェクトの型");
		}

		/// <summary>
		/// オブジェクトの情報を、DataTable に追加する
		/// </summary>
		/// <param name="dboInfo">対象となるオブジェクト</param>
		/// <param name="dt"></param>
		public void	AddObjectInfo(DBObjectInfo dboInfo, DataTable dt)
		{
			string	strsql = string.Format("select * from sys.all_objects where object_id = OBJECT_ID('{0}') ",
				dboInfo.FormalName );
			SqlCommand	cm = new SqlCommand(strsql,this.sqlConnect);

			SqlDataAdapter	da = new SqlDataAdapter(strsql,this.sqlConnect);
			DataTable	odt = new DataTable("sysobjects");
			da.Fill(odt);



			DataRow dr = dt.NewRow();

			if( odt.Rows.Count != 0 )
			{
				dr[0] = odt.Rows[0]["object_id"];
				dr[5] = odt.Rows[0]["modify_date"].ToString();
			}
			dr[1] = dboInfo.ObjName;
			dr[2] = dboInfo.Owner;
			dr[3] = this.GetObjTypeName(dboInfo.ObjType);
			dr[4] = dboInfo.CreateTime;
			if( dboInfo.IsSynonym )
			{
				dr[6] = dboInfo.SynonymBase;
				dr[7] = this.GetObjTypeName(dboInfo.SynonymBaseType);
			}
			else
			{
				dr[6] = string.Empty;
				dr[7] = string.Empty;
			}

			dt.Rows.Add(dr);
		}

		internal string GetObjTypeName(string objType)
		{
			switch( objType )
			{

				case	"AF":	return "集計関数 (CLR)";
				case	"C":	return "CHECK 制約";
				case	"D":	return "DEFAULT (制約またはスタンドアロン)";
				case	"F":	return "FOREIGN KEY 制約";
				case	"PK":	return "PRIMARY KEY 制約";
				case	"P":	return "SQL ストアド プロシージャ";
				case	"PC":	return "アセンブリ (CLR) ストアド プロシージャ";
				case	"FN":	return "SQL スカラ関数";
				case	"FS":	return "アセンブリ (CLR) スカラ関数";
				case	"FT":	return "アセンブリ (CLR) テーブル値関数";
				case	"R":	return "ルール (旧形式、スタンドアロン)";
				case	"RF":	return "レプリケーション フィルタ プロシージャ";
				case	"SN":	return "シノニム";
				case	"SQ":	return "サービス キュー";
				case	"TA":	return "アセンブリ (CLR) トリガ";
				case	"TR":	return "SQL トリガ";
				case	"IF":	return "SQL インライン テーブル値関数";
				case	"TF":	return "SQL テーブル値関数";
				case	"U":	return "テーブル (ユーザー定義)";
				case	"UQ":	return "UNIQUE 制約";
				case	"V":	return "ビュー";
				case	"X":	return "拡張ストアド プロシージャ";
				case	"IT":	return "内部テーブル";
				default:
					return	string.Empty;
			}
		}
		#endregion
	}
}
