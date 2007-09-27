using System;
using System.Collections;
using System.Data;
using System.Data.Common;
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

		/// <summary>
		/// コネクション
		/// </summary>
		protected System.Data.SqlClient.SqlConnection sqlConnect;

		/// <summary>
		/// SelectCommand 等のタイムアウト値
		/// </summary>
		protected int	_timeout;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public SqlServerDriver()
		{
			// 
			// TODO: コンストラクタ ロジックをここに追加してください。
			//
		}
		#region ISqlInterface メンバ

		/// <summary>
		/// SQLServerに対するコネクション情報を管理する
		/// </summary>
		/// <param name="sqlConnection1"></param>
		public void SetConnection(IDbConnection sqlConnection1)
		{
			this.sqlConnect = (System.Data.SqlClient.SqlConnection)sqlConnection1;
		}

		/// <summary>
		/// タイムアウト値を設定しなおす
		/// </summary>
		/// <param name="timeout"></param>
		public void SetTimeout(int timeout)
		{
			this._timeout = timeout;
		}

		/// <summary>
		/// DataAdapter を取得する
		/// </summary>
		public DbDataAdapter NewDataAdapter()
		{
			return new SqlDataAdapter();
		}

		/// <summary>
		/// IDbCommand を新規に作成する。
		/// ただし、コネクション情報とタイムアウト値はすでにセットされている
		/// </summary>
		/// <returns></returns>
		public IDbCommand	NewSqlCommand()
		{
			SqlCommand	sqlCmd = new SqlCommand();
			sqlCmd.Connection = this.sqlConnect;
			sqlCmd.CommandTimeout = this._timeout;
			return sqlCmd;
		}

		/// <summary>
		/// DataAdapter に IDbCommand を SelectCommandとして関連づける
		/// </summary>
		/// <param name="da"></param>
		/// <param name="cmd"></param>
		public void	SetSelectCmd(DbDataAdapter da, IDbCommand cmd)
		{
			((SqlDataAdapter)da).SelectCommand = (SqlCommand)cmd;
		}

		/// <summary>
		/// IDbCommand を新規に作成する。
		/// ただし、コマンド文字列、コネクション情報とタイムアウト値はすでにセットされている
		/// </summary>
		/// <param name="sqlstr">実行するコマンド文字列</param>
		/// <returns></returns>
		public IDbCommand		NewSqlCommand(string sqlstr)
		{
			SqlCommand	sqlCmd = new SqlCommand(sqlstr,this.sqlConnect);
			sqlCmd.CommandTimeout = this._timeout;
			return sqlCmd;
		}

		/// <summary>
		/// トランザクション情報を取得する
		/// </summary>
		public IDbTransaction	SetTransaction(IDbCommand cmd)
		{
			cmd.Transaction = this.sqlConnect.BeginTransaction();
			return cmd.Transaction;
		}

		/// <summary>
		/// select コマンドから、update, insert, delete コマンドを生成しなおす
		/// </summary>
		/// <param name="da"></param>
		public void	SetCommandBuilder(DbDataAdapter da)
		{
			SqlCommandBuilder  cb = new SqlCommandBuilder((SqlDataAdapter)da);
		}


		/// <summary>
		/// DataReaderからbyte配列を読み込む。
		/// 指定されたフィールドはもともとバイナリデータであることが前提
		/// </summary>
		/// <param name="dr"></param>
		/// <param name="col"></param>
		/// <returns></returns>
		public byte[]	GetDataReaderBytes(IDataReader dr, int col)
		{
			return ((SqlDataReader)dr).GetSqlBinary(col).Value;
		}

		/// <summary>
		/// DBの一覧表示を取得するSQL文を返す
		/// </summary>
		/// <returns></returns>
		public string GetDBSelect()
		{
			return "SELECT name FROM sys.databases  order by name";;
		}

		/// <summary>
		/// 指定されたデータベースへと接続を変更する
		/// </summary>
		/// <param name="dbName">変更先のデータベース名</param>
		/// <returns></returns>
		public void SetDataBase(string dbName)
		{
			this.sqlConnect.ChangeDatabase(dbName);
		}

		/// <summary>
		/// テーブル一覧のカラムヘッダの表示文字を取得する
		/// </summary>
		/// <returns></returns>
		public string GetTbListColName()
		{
			return "Table/View/Synonym";
		}


		/// <summary>
		/// DBオーナーのラベルを返す
		/// </summary>
		/// <returns></returns>
		public string GetOwnerLabel1()
		{
			return "Schema(&O)";
		}

		/// <summary>
		/// ラジオボタンのラベルを返す
		/// </summary>
		/// <returns></returns>
		public string GetOwnerLabel2()
		{
			return "スキーマ名・テーブル名";
		}

		/// <summary>
		/// オブジェクト一覧の表示用SQLの取得
		/// </summary>
		/// <param name="isDspTable">テーブルを表示させるか否か true: 表示する false: 表示させない</param>
		/// <param name="isDspView">View を表示させるか否か true: 表示する false: 表示させない</param>
		/// <param name="isDspSyn">シノニムを表示させるか否か true: 表示する false: 表示させない</param>
		/// <param name="isDspFunc">Functionを表示させるか否か true: 表示する false: 表示させない</param>
		/// <param name="isDspSP">ストアドプロシージャを表示させるか否か true: 表示する false: 表示させない</param>
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
				int		maxRow = dboInfo.FieldInfo.Count;
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
						wr.Write("\t[{0}]", ((DBFieldInfo)dboInfo.FieldInfo[i]).Name);
					}
					else
					{
						wr.Write("\t{0}", ((DBFieldInfo)dboInfo.FieldInfo[i]).Name);
					}
					wr.Write("\t");
					// 型
					valtype = ((DBFieldInfo)dboInfo.FieldInfo[i]).TypeName;

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
						if( ((DBFieldInfo)dboInfo.FieldInfo[i]).Length == -1 )
						{
							wr.Write(" (max)");
						}
						else
						{
							wr.Write(" ({0})", ((DBFieldInfo)dboInfo.FieldInfo[i]).Length);
						}
					}
					else if( valtype == "numeric" ||
						valtype == "decimal" )
					{
						wr.Write(" ({0},{1})", ((DBFieldInfo)dboInfo.FieldInfo[i]).Prec,
							((DBFieldInfo)dboInfo.FieldInfo[i]).Xscale);
					}
					wr.Write("\t");
						
					if( ((DBFieldInfo)dboInfo.FieldInfo[i]).Collation != string.Empty)
					{
						wr.Write("COLLATE {0}",((DBFieldInfo)dboInfo.FieldInfo[i]).Collation);
						wr.Write("\t");
					}

					if( ((DBFieldInfo)dboInfo.FieldInfo[i]).IncSeed != 0)
					{
						wr.Write("\tIDENTITY({0},{1})",
							((DBFieldInfo)dboInfo.FieldInfo[i]).IncSeed,
							((DBFieldInfo)dboInfo.FieldInfo[i]).IncStep );
					}
						
					if( ((DBFieldInfo)dboInfo.FieldInfo[i]).IsNullable == false )
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
		/// <param name="objTable">対象とするDataTable</param>
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

		/// <summary>
		/// オブジェクトの詳細情報をセットするイベントハンドラを返す
		/// </summary>
		/// <returns></returns>
		public DBObjectInfo.DataGetEventHandler ObjectDetailSet()
		{
			return new DBObjectInfo.DataGetEventHandler(this.dbObjSet);
		}

		private	void	dbObjSet(object sender)
		{
			DBObjectInfo	dboInfo = (DBObjectInfo)sender;
			DataSet		ds = new DataSet(dboInfo.ObjName);


			// まずは必要な情報を全て収集する

			// FillSchema での情報収集
			string strsql = string.Format("select * from {0} where 0=1",
				dboInfo.FormalName );
			SqlDataAdapter da = new SqlDataAdapter(strsql,this.sqlConnect);
			DataTable []dt = da.FillSchema(ds,SchemaType.Mapped,"schema");
			dboInfo.SchemaBaseInfo = dt[0];

			// 実際の細かい情報を直接取得する
			SqlDataAdapter tableda = new SqlDataAdapter(
				string.Format(
				@"select 
	t1.name colname, 
	case 
	when t1.user_type_id != t1.system_type_id then 1
	else	0
	end	as isUserType,
	t2.name valtype, 
	t3.name	typeSchema, 
	t1.max_length	,
	t4.name as baseValType,
 convert(int,
	CASE 
	WHEN t1.user_type_id = t1.system_type_id and  t2.name IN (N'nchar', N'nvarchar') and t1.max_length != -1 THEN t1.max_length/2 
	WHEN t1.user_type_id != t1.system_type_id and  t4.name IN (N'nchar', N'nvarchar') and t1.max_length != -1 THEN t1.max_length/2 
	ELSE t1.max_length	
	end) as length, 
	convert(int,
	CASE 
	WHEN t1.user_type_id = t1.system_type_id and t2.name IN (N'nchar', N'nvarchar') THEN t1.max_length/2 
	WHEN t1.user_type_id != t1.system_type_id and t2.name IN (N'nchar', N'nvarchar') THEN t1.max_length/2 
	ELSE t1.precision	
	end ) as [prec], 
	convert(int,t1.scale) as xscale, 
	t1.column_id as colid, 
	t1.column_id as colorder, 
	convert(int,t1.is_nullable) as isnullable, 
	t1.collation_name as collation, 
	t1.object_id as id,
	case 
	when t5.object_id is not null then ident_seed('{0}') 
	else null
	end as seed,
	case 
	when t5.object_id is not null then ident_incr('{0}') 
	else null
	end as incr
from 
	sys.all_columns t1 
	inner join sys.types t2 on 
		t1.user_type_id  = t2.user_type_id  and 
		t1.system_type_id = t2.system_type_id 
	inner JOIN sys.schemas as t3 on 
		t2.schema_id = t3.schema_id
	left outer join sys.types t4 on
		t4.user_type_id = t1.system_type_id and
		t4.system_type_id = t4.user_type_id 
	left outer join sys.identity_columns t5 on
		t1.object_id = t5.object_id and 
		t1.column_id = t5.column_id
where 
	t1.object_id = OBJECT_ID('{0}')
order by colorder",
				dboInfo.RealObjName
				),
				this.sqlConnect );
			tableda.Fill(ds,"fieldList");

			DBFieldInfo addInfo;
			ArrayList	ar = new ArrayList();
			foreach(DataRow fdr in ds.Tables["fieldList"].Rows )
			{
				// フィールドの情報でぐるぐるまわって、セットしていく
				addInfo = new DBFieldInfo();
				addInfo.Col = ds.Tables["schema"].Columns[fdr["colname"].ToString()];
				addInfo.Colid = (int)fdr["colid"];
				if( fdr["collation"] != DBNull.Value )
				{
					addInfo.Collation = (string)fdr["collation"];
				}
				addInfo.Colorder = (int)fdr["colorder"];
				addInfo.Length = (int)fdr["length"];
				addInfo.Prec = (int)fdr["prec"];
				addInfo.Xscale = (int)fdr["xscale"];
				if( (int)fdr["isUserType"] == 0 )
				{
					// システムの定義
					addInfo.TypeName = (string)fdr["valtype"];
				}
				else
				{
					// ユーザー定義型
					addInfo.TypeName = string.Format("[{0}].[{1}]",
							(string)fdr["typeSchema"] ,
							(string)fdr["valtype"] );
				}
				if( fdr["seed"] != DBNull.Value )
				{
					addInfo.IncSeed = (decimal)fdr["seed"];
				}
				if( fdr["incr"] != DBNull.Value )
				{
					addInfo.IncStep = (decimal)fdr["incr"];
				}
				// プライマリキーかどうかをチェック
				for(int i = 0; i < ds.Tables["schema"].PrimaryKey.Length; i++ )
				{
					if( addInfo.Col.ColumnName == ds.Tables["schema"].PrimaryKey[i].ColumnName )
					{
						addInfo.PrimaryKeyOrder = i;
						break;
					}
				}
				ar.Add(addInfo);
			}
			dboInfo.FieldInfo = ar;
		}


		#endregion
	}
}
