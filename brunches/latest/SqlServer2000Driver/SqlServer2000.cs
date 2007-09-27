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
	/// SqlServer2000 の概要の説明です。
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
		/// DataAdapter に IDbCommand を SelectCommandとして関連づける
		/// </summary>
		/// <param name="da"></param>
		/// <param name="cmd"></param>
		public void	SetSelectCmd(DbDataAdapter da, IDbCommand cmd)
		{
			((SqlDataAdapter)da).SelectCommand = (SqlCommand)cmd;
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
			return "SELECT name FROM sysdatabases order by name";
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
			return "Table/View";
		}


		/// <summary>
		/// DBオーナーのラベルを返す
		/// </summary>
		/// <returns></returns>
		public string GetOwnerLabel1()
		{
			return "owner/Role(&O)";
		}

		/// <summary>
		/// ラジオボタンのラベルを返す
		/// </summary>
		/// <returns></returns>
		public string GetOwnerLabel2()
		{
			return "オーナー名・テーブル名";
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
					sysobjects.name as tbname, 
					sysusers.name as uname ,
					xtype as tvs,
					sysobjects.crdate as cretime,
					'' as synbase,
					'' as syntype
					from sysobjects, sysusers 
					where xtype in ( {0} ) and sysobjects.uid = sysusers.uid ",
				destObj );

			if( ownerList != null && 
				ownerList != string.Empty )
			{
				retsql += " and sysusers.name in ( " + ownerList + " ) ";
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
				@"select * from sysindexes where 0=1" );
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
				return "select * from sysusers order by name";
			}
			else
			{
				return "select * from sysusers where name not like 'db[_]%' order by name";
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

				string sqlstr = "select syscolumns.name colname, systypes.name valtype, syscolumns.length, syscolumns.prec, syscolumns.xscale, syscolumns.colid, syscolumns.colorder, syscolumns.isnullable, syscolumns.collation  from sysobjects, syscolumns, sysusers, systypes where sysobjects.id = syscolumns.id and sysobjects.uid= sysusers.uid and syscolumns.xusertype=systypes.xusertype and sysusers.name = '" + dboInfo.Owner +"' and sysobjects.name = '" + dboInfo.ObjName + "' order by syscolumns.colorder";

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
		/// <param name="objTable">対象とするDataTable</param>
		public void	InitObjTable(DataTable objTable)
		{
			objTable.Columns.Add("オブジェクトID",typeof(int));
			objTable.Columns.Add("オブジェクト名");
			objTable.Columns.Add("所有者");
			objTable.Columns.Add("オブジェクトの型");
			objTable.Columns.Add("作成日時");
		}

		/// <summary>
		/// オブジェクトの情報を、DataTable に追加する
		/// </summary>
		/// <param name="dboInfo">対象となるオブジェクト</param>
		/// <param name="dt"></param>
		public void	AddObjectInfo(DBObjectInfo dboInfo, DataTable dt)
		{
			string	strsql = string.Format("select * from sysobjects where id = OBJECT_ID('{0}') ",
				dboInfo.FormalName );
			SqlCommand	cm = new SqlCommand(strsql,this.sqlConnect);

			SqlDataAdapter	da = new SqlDataAdapter(strsql,this.sqlConnect);
			DataTable	odt = new DataTable("sysobjects");
			da.Fill(odt);



			DataRow dr = dt.NewRow();

			if( odt.Rows.Count != 0 )
			{
				dr[0] = odt.Rows[0]["id"];
			}
			dr[1] = dboInfo.ObjName;
			dr[2] = dboInfo.Owner;
			dr[4] = dboInfo.CreateTime;

			switch( dboInfo.ObjType )
			{
				case	"C":
					dr[3] = "CHECK 制約";
					break;
				case	"D":
					dr[3] = "Default または DEFAULT 制約";
					break;
				case	"F":
					dr[3] = "FOREIGN KEY 制約";
					break;
				case	"L":
					dr[3] = "ログ";
					break;
				case	"FN":
					dr[3] = "スカラ関数";
					break;
				case	"IF":
					dr[3] = "インライン テーブル関数";
					break;
				case	"P":
					dr[3] = "ストアド プロシージャ";
					break;
				case	"PK":
					dr[3] = "PRIMARY KEY 制約 (タイプ K)";
					break;
				case	"RF":
					dr[3] = "レプリケーション フィルタ ストアド プロシージャ";
					break;
				case	"S":
					dr[3] = "システム テーブル";
					break;
				case	"TF":
					dr[3] = "テーブル関数";
					break;
				case	"TR":
					dr[3] = "トリガ";
					break;
				case	"U":
					dr[3] = "ユーザー テーブル";
					break;
				case	"UQ":
					dr[3] = "UNIQUE 制約 (タイプ K)";
					break;
				case	"V":
					dr[3] = "ビュー";
					break;
				case	"X":
					dr[3] = "拡張ストアド プロシージャ";
					break;
			}
			dt.Rows.Add(dr);
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
syscolumns.name colname, 
systypes.name valtype, 
convert(int,syscolumns.length) as length, 
isnull(convert(int,syscolumns.prec),0) as prec, 
isnull(convert(int,syscolumns.xscale),0) as xscale, 
convert(int,syscolumns.colid) as colid, 
convert(int,syscolumns.colorder) as colorder, 
syscolumns.isnullable, 
syscolumns.collation, 
sysobjects.id  
from 
	sysobjects, 
	syscolumns, 
	sysusers, 
	systypes 
where 
	sysobjects.id = syscolumns.id 
and sysobjects.uid= sysusers.uid 
and syscolumns.xusertype=systypes.xusertype 
and sysobjects.id = OBJECT_ID('{0}')
order by syscolumns.colorder",
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
				if( addInfo.Col.MaxLength < 0 &&
					(int)fdr["length"] > 0 )
				{
					addInfo.Length = (int)fdr["length"];
				}
				else
				{
					addInfo.Length = addInfo.Col.MaxLength;
				}
				addInfo.Prec = (int)fdr["prec"];
				addInfo.Xscale = (int)fdr["xscale"];
				addInfo.TypeName = (string)fdr["valtype"];
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
