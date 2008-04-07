using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Text;

namespace quickDBExplorer
{
	/// <summary>
	/// SqlServer2000 の概要の説明です。
	/// </summary>
	public class SqlServerDriver2000 : ISqlInterface
	{

		/// <summary>
		/// コネクション
		/// </summary>
		protected System.Data.SqlClient.SqlConnection sqlConnect;

		/// <summary>
		/// SelectCommand 等のタイムアウト値
		/// </summary>
		protected int	queryTimeout;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public SqlServerDriver2000()
		{
			// 
			// TODO: コンストラクタ ロジックをここに追加してください。
			//
		}

		#region ISqlInterface メンバ

		/// <summary>
		/// SQLServerに対するコネクション情報を管理する
		/// </summary>
		/// <param name="sqlConnection">コネクション情報</param>
		/// <param name="timeout">コマンド実行タイムアウト値</param>
		public void SetConnection(IDbConnection sqlConnection, int timeout)
		{
			this.sqlConnect = (System.Data.SqlClient.SqlConnection)sqlConnection;
			this.queryTimeout = timeout;
		}

		/// <summary>
		/// SQLSERVERに対するコネクション情報を閉じる
		/// </summary>
		public void CloseConnection()
		{
			this.sqlConnect.Close();
		}

		/// <summary>
		/// タイムアウト値を設定しなおす
		/// </summary>
		/// <param name="timeout"></param>
		public void SetTimeout(int timeout)
		{
			this.queryTimeout = timeout;
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
			sqlCmd.CommandTimeout = this.queryTimeout;
			return sqlCmd;
		}

		/// <summary>
		/// IDbCommand を新規に作成する。
		/// ただし、コマンド文字列、コネクション情報とタイムアウト値はすでにセットされている
		/// </summary>
		/// <param name="stSql">実行するコマンド文字列</param>
		/// <returns></returns>
		public IDbCommand		NewSqlCommand(string stSql)
		{
			SqlCommand	sqlCmd = new SqlCommand(stSql,this.sqlConnect);
			sqlCmd.CommandTimeout = this.queryTimeout;
			return sqlCmd;
		}

		/// <summary>
		/// DataAdapter に IDbCommand を SelectCommandとして関連づける
		/// </summary>
		/// <param name="da"></param>
		/// <param name="cmd"></param>
		public void	SetSelectCmd(DbDataAdapter da, IDbCommand cmd)
		{
			if( da == null )
			{
				throw new ArgumentNullException("da");
			}
			if( cmd == null )
			{
				throw new ArgumentNullException("cmd");
			}
			((SqlDataAdapter)da).SelectCommand = (SqlCommand)cmd;
		}

		/// <summary>
		/// トランザクション情報を取得する
		/// </summary>
		public IDbTransaction	SetTransaction(IDbCommand cmd)
		{
			if( cmd == null )
			{
				throw new ArgumentNullException("cmd");
			}
			cmd.Transaction = this.sqlConnect.BeginTransaction();
			return cmd.Transaction;
		}

		/// <summary>
		/// select コマンドから、update, insert, delete コマンドを生成しなおす
		/// </summary>
		/// <param name="da"></param>
		public void	SetCommandBuilder(DbDataAdapter da)
		{
			if( da == null )
			{
				throw new ArgumentNullException("da");
			}
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
			if( dr == null )
			{
				throw new ArgumentNullException("dr");
			}
			if( col < 0 )
			{
				throw new ArgumentException("col must greater equal 0");
			}
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
		public void SetDatabase(string dbName)
		{
			this.sqlConnect.ChangeDatabase(dbName);
		}

		/// <summary>
		/// テーブル一覧のカラムヘッダの表示文字を取得する
		/// </summary>
		/// <returns></returns>
		public string GetTableListColumnName()
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
		/// <param name="isDisplayTable">テーブルを表示させるか否か true: 表示する false: 表示させない</param>
		/// <param name="isDisplayView">View を表示させるか否か true: 表示する false: 表示させない</param>
		/// <param name="isSynonym">シノニムを表示させるか否か true: 表示する false: 表示させない</param>
		/// <param name="isDisplayFunction">Functionを表示させるか否か true: 表示する false: 表示させない</param>
		/// <param name="isDisplaySP">ストアドプロシージャを表示させるか否か true: 表示する false: 表示させない</param>
		/// <param name="ownerList">特定のOwnerのテーブルのみ表示する場合は IN句に利用するカンマ区切り文字列を渡す</param>
		/// <returns></returns>
		public string GetDisplayObjList(bool isDisplayTable, bool isDisplayView, bool isSynonym, bool isDisplayFunction, bool isDisplaySP, string ownerList)
		{
			string destObj = "";

			ArrayList ar = new ArrayList();

			if( isDisplayTable == true )
			{
				ar.Add("U");
			}

			if( isDisplayView == true )
			{
				ar.Add("V");
			}

			if( isSynonym == true )
			{
				ar.Add("SN");
			}

			if( isDisplayFunction == true )
			{
				ar.Add("FN");
			}

			if( isDisplaySP == true )
			{
				ar.Add("P");
				ar.Add("PC");
			}

			if( ar.Count == 0 )
			{
				// 何も指定がなければ、テーブルのみにしておく
				ar.Add("U");
			}

			StringBuilder sb = new StringBuilder();
			for( int i = 0; i < ar.Count; i++ )
			{
				if( i != 0 )
				{
					sb.Append(",");
				}
				sb.Append("'").Append((string)ar[i]).Append("'");
			}
			destObj = sb.ToString();
			sb.Length  = 0;
			sb.AppendFormat(@"select 
					sysobjects.name as tbname, 
					sysusers.name as uname ,
					xtype as tvs,
					sysobjects.crdate as cretime,
					'' as synbase,
					'' as synType
					from sysobjects, sysusers 
					where xtype in ( {0} ) and sysobjects.uid = sysusers.uid ",
				destObj );

			if( ownerList != null && 
				ownerList.Length != 0 )
			{
				sb.AppendFormat(" and sysusers.name in ( {0} ) ", ownerList);
			}

			return sb.ToString();
		}

		/// <summary>
		/// Owner の一覧を取得するSQLを生成する
		/// </summary>
		/// <param name="isDisplaySysUser"></param>
		/// <returns></returns>
		public string	GetOwnerList(bool isDisplaySysUser)
		{
			if( isDisplaySysUser )
			{
				return "select * from sysusers order by name";
			}
			else
			{
				return "select * from sysusers where name not like 'db[_]%' order by name";
			}
		}

		/// <summary>
		/// ISQL を起動する。
		/// </summary>
		/// <param name="serverRealName">サーバー名</param>
		/// <param name="instanceName">インスタンス名</param>
		/// <param name="isUseTrust">信頼関係接続を利用するか否か</param>
		/// <param name="dbName">データベース名</param>
		/// <param name="logOnUserId">ログインID</param>
		/// <param name="logOnPassword">ログインパスワード</param>
		public void	CallISQL(string serverRealName, string instanceName, bool isUseTrust, string dbName, string logOnUserId, string logOnPassword)
		{
			if( instanceName == null )
			{
				throw new ArgumentNullException("instanceName");
			}
			if( dbName == null )
			{
				throw new ArgumentNullException("dbName");
			}
			Process isqlProcess = new Process();
			isqlProcess.StartInfo.FileName = "isqlw";
			isqlProcess.StartInfo.ErrorDialog = true;
			string serverstr = "";
			if( instanceName.Length != 0 )
			{
				serverstr = serverRealName + "\\" + instanceName;
			}
			else
			{
				serverstr = serverRealName;
			}
			if( isUseTrust == true )
			{
				if( dbName.Length != 0 )
				{
					isqlProcess.StartInfo.Arguments = string.Format(System.Globalization.CultureInfo.CurrentCulture," -S {0} -d {1} -E ",
						serverstr,
						dbName
						);
				}
				else
				{
					isqlProcess.StartInfo.Arguments = string.Format(System.Globalization.CultureInfo.CurrentCulture," -S {0} -E ",
						serverstr
						);
				}
			}
			else
			{
				if( dbName.Length != 0 )
				{
					isqlProcess.StartInfo.Arguments = string.Format(System.Globalization.CultureInfo.CurrentCulture," -S {0} -d {1} -U {2} -P {3} ",
						serverstr,
						dbName,
						logOnUserId,
						logOnPassword );
				}
				else
				{
					isqlProcess.StartInfo.Arguments = string.Format(System.Globalization.CultureInfo.CurrentCulture," -S {0} -U {1} -P {2} ",
						serverstr,
						logOnUserId,
						logOnPassword );
				}
			}
			isqlProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
			isqlProcess.Start();
		}

		/// <summary>
		/// EnterPriseManagerを起動する
		/// </summary>
		/// <param name="serverRealName">サーバー名</param>
		/// <param name="instanceName">インスタンス名</param>
		/// <param name="isUseTrust">信頼関係接続を利用するか否か</param>
		/// <param name="dbName">データベース名</param>
		/// <param name="logOnUserId">ログインID</param>
		/// <param name="logOnPassword">ログインパスワード</param>
		public void	CallEPM(string serverRealName, string instanceName, bool isUseTrust, string dbName, string logOnUserId, string logOnPassword)
		{
			Process isqlProcess = new Process();
			isqlProcess.StartInfo.FileName = "SQL Server Enterprise Manager.MSC";
			isqlProcess.StartInfo.ErrorDialog = true;

			isqlProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
			isqlProcess.Start();
		}

		/// <summary>
		/// Profilerを起動する
		/// </summary>
		/// <param name="serverRealName">サーバー名</param>
		/// <param name="instanceName">インスタンス名</param>
		/// <param name="isUseTrust">信頼関係接続を利用するか否か</param>
		/// <param name="dbName">データベース名</param>
		/// <param name="logOnUserId">ログインID</param>
		/// <param name="logOnPassword">ログインパスワード</param>
		public void	CallProfile(string serverRealName, string instanceName, bool isUseTrust, string dbName, string logOnUserId, string logOnPassword)
		{
			if( instanceName == null )
			{
				throw new ArgumentNullException("instanceName");
			}
			if( dbName == null )
			{
				throw new ArgumentNullException("dbName");
			}

			
			Process isqlProcess = new Process();
			isqlProcess.StartInfo.FileName = "profiler.exe";
			isqlProcess.StartInfo.ErrorDialog = true;
			string serverstr = "";
			if( instanceName.Length != 0 )
			{
				serverstr = serverRealName + "\\" + instanceName;
			}
			else
			{
				serverstr = serverRealName;
			}
			if( isUseTrust == true )
			{
				if( dbName.Length != 0 )
				{
					isqlProcess.StartInfo.Arguments = string.Format(System.Globalization.CultureInfo.CurrentCulture,"/S{0} /D{1} /E ",
						serverstr,
						dbName
						);
				}
				else
				{
					isqlProcess.StartInfo.Arguments = string.Format(System.Globalization.CultureInfo.CurrentCulture,"/S{0} /E ",
						serverstr
						);
				}
			}
			else
			{
				if( dbName.Length != 0 )
				{
					isqlProcess.StartInfo.Arguments = string.Format(System.Globalization.CultureInfo.CurrentCulture," /S{0} D{1} /U{2} /P{3} ",
						serverstr,
						dbName,
						logOnUserId,
						logOnPassword );
				}
				else
				{
					isqlProcess.StartInfo.Arguments = string.Format(System.Globalization.CultureInfo.CurrentCulture," /S{0} /U{1} /P{2} ",
						serverstr,
						logOnUserId,
						logOnPassword );
				}
			}
			isqlProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
			isqlProcess.Start();
		}


		/// <summary>
		/// オブジェクトに対するDROP 文を生成する
		/// </summary>
		/// <param name="databaseObjectInfo"></param>
		/// <returns></returns>
		public string	GetDDLDropStr(DBObjectInfo databaseObjectInfo)
		{
			if( databaseObjectInfo == null )
			{
				throw new ArgumentNullException("databaseObjectInfo");
			}
			switch( databaseObjectInfo.ObjType )
			{
				case	"U":
					return string.Format(System.Globalization.CultureInfo.CurrentCulture,"DROP TABLE {0}{1}GO{1}{1}", databaseObjectInfo.FormalName,Environment.NewLine);
				case	"V":
					return string.Format(System.Globalization.CultureInfo.CurrentCulture,"DROP VIEW {0}{1}GO{1}{1}", databaseObjectInfo.FormalName,Environment.NewLine);
				case	"SN":
					if( databaseObjectInfo.SynonymBaseType == "U" )
					{
						return string.Format(System.Globalization.CultureInfo.CurrentCulture,"DROP TABLE {0}{1}GO{1}{1}", databaseObjectInfo.FormalName,Environment.NewLine);
					}
					else
					{
						return string.Format(System.Globalization.CultureInfo.CurrentCulture,"DROP VIEW {0}{1}GO{1}{1}", databaseObjectInfo.FormalName,Environment.NewLine);
					}
				default:
					return	string.Empty;
			}
		}


		/// <summary>
		/// オブジェクトに対する Create 文を生成する
		/// </summary>
		/// <param name="databaseObjectInfo"></param>
		/// <param name="useParentheses"></param>
		/// <returns></returns>
		public string	GetDdlCreateString(DBObjectInfo databaseObjectInfo, bool useParentheses)
		{
			if( databaseObjectInfo == null )
			{
				throw new ArgumentNullException("databaseObjectInfo");
			}
			StringBuilder strline =  new StringBuilder();
			TextWriter	wr = new StringWriter(strline,System.Globalization.CultureInfo.CurrentCulture);

			if( databaseObjectInfo.ObjType == "U" )
			{

				string stSql = "select syscolumns.name colname, systypes.name valtype, syscolumns.length, syscolumns.prec, syscolumns.xscale, syscolumns.colid, syscolumns.colorder, syscolumns.isnullable, syscolumns.collation  from sysobjects, syscolumns, sysusers, systypes where sysobjects.id = syscolumns.id and sysobjects.uid= sysusers.uid and syscolumns.xusertype=systypes.xusertype and sysusers.name = '" + databaseObjectInfo.Owner +"' and sysobjects.name = '" + databaseObjectInfo.ObjName + "' order by syscolumns.colorder";

				SqlDataAdapter da = new SqlDataAdapter(stSql, this.sqlConnect);
				DataSet ds = new DataSet();
				ds.CaseSensitive = true;
				ds.Locale = System.Globalization.CultureInfo.CurrentCulture;
				da.Fill(ds,databaseObjectInfo.ToString());

				int		maxRow = ds.Tables[databaseObjectInfo.ToString()].Rows.Count;
				if( useParentheses )
				{
					wr.Write("Create table {0} ", databaseObjectInfo.FormalName);
				}
				else
				{
					wr.Write("Create table {0} ", databaseObjectInfo.ToString());
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
					if( useParentheses )
					{
						wr.Write("\t[{0}]", ds.Tables[databaseObjectInfo.ToString()].Rows[i][0]);
					}
					else
					{
						wr.Write("\t{0}", ds.Tables[databaseObjectInfo.ToString()].Rows[i][0]);
					}
					wr.Write("\t");
					// 型
					valtype = (string)ds.Tables[databaseObjectInfo.ToString()].Rows[i][1];

					wr.Write("\t");

					if( useParentheses )
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
						if( (Int16)ds.Tables[databaseObjectInfo.ToString()].Rows[i][3] == -1 )
						{
							wr.Write(" (max)");
						}
						else
						{
							wr.Write(" ({0})", ds.Tables[databaseObjectInfo.ToString()].Rows[i][3]);
						}
					}
					else if( valtype == "numeric" ||
						valtype == "decimal" )
					{
						wr.Write(" ({0},", ds.Tables[databaseObjectInfo.ToString()].Rows[i][3]);
						wr.Write("{0})", ds.Tables[databaseObjectInfo.ToString()].Rows[i][4]);
					}
					wr.Write("\t");
						
					if( !ds.Tables[databaseObjectInfo.ToString()].Rows[i].IsNull("collation"))
					{
						wr.Write("COLLATE {0}",ds.Tables[databaseObjectInfo.ToString()].Rows[i]["collation"]);
						wr.Write("\t");
					}
						
					if( (int)ds.Tables[databaseObjectInfo.ToString()].Rows[i]["isnullable"] == 0 )
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
			else
			{
				DataTable dt = new DataTable();
				dt.CaseSensitive = true;
				dt.Locale = System.Globalization.CultureInfo.CurrentCulture;

				string strsql = string.Format(System.Globalization.CultureInfo.CurrentCulture,"sp_helptext '{0}'", databaseObjectInfo.FormalName );
				SqlDataAdapter	da = new SqlDataAdapter(strsql,this.sqlConnect);
				da.SelectCommand.CommandTimeout = this.queryTimeout;
				da.Fill(dt);
				foreach(DataRow dr in dt.Rows)
				{
					wr.Write(dr["Text"].ToString());
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
			if( objTable == null )
			{
				throw new ArgumentNullException("objTable");
			}
			objTable.Columns.Add("オブジェクトID",typeof(int));
			objTable.Columns.Add("オブジェクト名");
			objTable.Columns.Add("所有者");
			objTable.Columns.Add("オブジェクトの型");
			objTable.Columns.Add("作成日時");
		}

		/// <summary>
		/// オブジェクトの情報を、DataTable に追加する
		/// </summary>
		/// <param name="databaseObjectInfo">対象となるオブジェクト</param>
		/// <param name="dt"></param>
		public void	AddObjectInfo(DBObjectInfo databaseObjectInfo, DataTable dt)
		{
			if( databaseObjectInfo == null )
			{
				throw new ArgumentNullException("databaseObjectInfo");
			}
			if( dt == null )
			{
				throw new ArgumentNullException("dt");
			}
			string	strsql = string.Format(System.Globalization.CultureInfo.CurrentCulture,"select * from sysobjects where id = OBJECT_ID('{0}') ",
				databaseObjectInfo.FormalName );
			SqlCommand	cm = new SqlCommand(strsql,this.sqlConnect);

			SqlDataAdapter	da = new SqlDataAdapter(strsql,this.sqlConnect);
			DataTable	odt = new DataTable("sysobjects");
			odt.Locale = System.Globalization.CultureInfo.CurrentCulture;
			da.Fill(odt);



			DataRow dr = dt.NewRow();

			if( odt.Rows.Count != 0 )
			{
				dr[0] = odt.Rows[0]["id"];
			}
			dr[1] = databaseObjectInfo.ObjName;
			dr[2] = databaseObjectInfo.Owner;
			dr[4] = databaseObjectInfo.CreateTime;

			switch( databaseObjectInfo.ObjType )
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
		public DataGetEventHandler ObjectDetailSet()
		{
			return new DataGetEventHandler(this.dbObjSet);
		}

		private	void	dbObjSet(object sender, System.EventArgs e)
		{
			DBObjectInfo	databaseObjectInfo = (DBObjectInfo)sender;
			DataSet		ds = new DataSet(databaseObjectInfo.ObjName);
			ds.CaseSensitive = true;
			ds.Locale = System.Globalization.CultureInfo.CurrentCulture;


			// まずは必要な情報を全て収集する

			// FillSchema での情報収集
			string strsql = string.Format(System.Globalization.CultureInfo.CurrentCulture,"select * from {0} where 0=1",
				databaseObjectInfo.FormalName );
			SqlDataAdapter da = new SqlDataAdapter(strsql,this.sqlConnect);
			DataTable []dt = da.FillSchema(ds,SchemaType.Mapped,"schema");
			databaseObjectInfo.SchemaBaseInfo = dt[0];

			// 実際の細かい情報を直接取得する
			SqlDataAdapter tableda = new SqlDataAdapter(
				string.Format(System.Globalization.CultureInfo.CurrentCulture,
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
				databaseObjectInfo.RealObjName
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
				addInfo.ColOrder = (int)fdr["colorder"];
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
			databaseObjectInfo.FieldInfo = ar;
		}

		#endregion
	}
}
