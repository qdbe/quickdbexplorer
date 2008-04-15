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
	/// SqlServer2005 の概要の説明です。
	/// </summary>
	public class SqlServerDriver2005 : ISqlInterface
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
		public SqlServerDriver2005()
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
		public virtual void SetConnection(IDbConnection sqlConnection, int timeout)
		{
			this.sqlConnect = (System.Data.SqlClient.SqlConnection)sqlConnection;
			this.queryTimeout = timeout;
		}

		/// <summary>
		/// SQLSERVERに対するコネクション情報を閉じる
		/// </summary>
		public virtual void CloseConnection()
		{
			this.sqlConnect.Close();
		}

		/// <summary>
		/// タイムアウト値を設定しなおす
		/// </summary>
		/// <param name="timeout"></param>
		public virtual void SetTimeout(int timeout)
		{
			this.queryTimeout = timeout;
		}

		/// <summary>
		/// DataAdapter を取得する
		/// </summary>
		public virtual DbDataAdapter NewDataAdapter()
		{
			return new SqlDataAdapter();
		}

		/// <summary>
		/// IDbCommand を新規に作成する。
		/// ただし、コネクション情報とタイムアウト値はすでにセットされている
		/// </summary>
		/// <returns></returns>
		public virtual IDbCommand	NewSqlCommand()
		{
			SqlCommand	sqlCmd = new SqlCommand();
			sqlCmd.Connection = this.sqlConnect;
			sqlCmd.CommandTimeout = this.queryTimeout;
			return sqlCmd;
		}

		/// <summary>
		/// DataAdapter に IDbCommand を SelectCommandとして関連づける
		/// </summary>
		/// <param name="da"></param>
		/// <param name="cmd"></param>
		public virtual void	SetSelectCmd(DbDataAdapter da, IDbCommand cmd)
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
		/// IDbCommand を新規に作成する。
		/// ただし、コマンド文字列、コネクション情報とタイムアウト値はすでにセットされている
		/// </summary>
		/// <param name="stSql">実行するコマンド文字列</param>
		/// <returns></returns>
		public virtual IDbCommand		NewSqlCommand(string stSql)
		{
			SqlCommand	sqlCmd = new SqlCommand(stSql,this.sqlConnect);
			sqlCmd.CommandTimeout = this.queryTimeout;
			return sqlCmd;
		}

		/// <summary>
		/// トランザクション情報を取得する
		/// </summary>
		public virtual IDbTransaction	SetTransaction(IDbCommand cmd)
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
		public virtual void	SetCommandBuilder(DbDataAdapter da)
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
		public virtual byte[]	GetDataReaderBytes(IDataReader dr, int col)
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
		public virtual string GetDBSelect()
		{
			return "SELECT name FROM sys.databases  order by name";;
		}

		/// <summary>
		/// 指定されたデータベースへと接続を変更する
		/// </summary>
		/// <param name="dbName">変更先のデータベース名</param>
		/// <returns></returns>
		public virtual void SetDatabase(string dbName)
		{
			this.sqlConnect.ChangeDatabase(dbName);
		}

		/// <summary>
		/// テーブル一覧のカラムヘッダの表示文字を取得する
		/// </summary>
		/// <returns></returns>
		public virtual string GetTableListColumnName()
		{
			return "Table/View/Synonym";
		}


		/// <summary>
		/// DBオーナーのラベルを返す
		/// </summary>
		/// <returns></returns>
		public virtual string GetOwnerLabel1()
		{
			return "Schema(&O)";
		}

		/// <summary>
		/// ラジオボタンのラベルを返す
		/// </summary>
		/// <returns></returns>
		public virtual string GetOwnerLabel2()
		{
			return "スキーマ名・テーブル名";
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
		public virtual string GetDisplayObjList(bool isDisplayTable, bool isDisplayView, bool isSynonym, bool isDisplayFunction, bool isDisplaySP, string ownerList)
		{
			string retsql = "";

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

			for( int i = 0; i < ar.Count; i++ )
			{
				if( i != 0 )
				{
					destObj += ",";
				}
				destObj += "'" + (string)ar[i] + "'";
			}

			retsql = string.Format(System.Globalization.CultureInfo.CurrentCulture,@"select 
	t1.name as tbname, 
	t2.name as uname ,
	t1.type as tvs,
	t1.create_date as cretime,
	isnull(t3.base_object_name,'') as synbase,
	isnull(t4.type, ' ') as synType
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
				ownerList.Length != 0 )
			{
				retsql += " and t2.name in ( " + ownerList + " ) ";
			}

			return retsql;
		}


		/// <summary>
		/// Owner の一覧を取得するSQLを生成する
		/// </summary>
		/// <param name="isDisplaySysUser"></param>
		/// <returns></returns>
		public virtual string	GetOwnerList(bool isDisplaySysUser)
		{
			if( isDisplaySysUser )
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
		/// ISQL を起動する。
		/// </summary>
		/// <param name="serverRealName">サーバー名</param>
		/// <param name="instanceName">インスタンス名</param>
		/// <param name="isUseTrust">信頼関係接続を利用するか否か</param>
		/// <param name="dbName">データベース名</param>
		/// <param name="logOnUserId">ログインID</param>
		/// <param name="logOnPassword">ログインパスワード</param>
		public virtual void	CallISQL(string serverRealName, string instanceName, bool isUseTrust, string dbName, string logOnUserId, string logOnPassword)
		{
			this.CallEPM(serverRealName, instanceName, isUseTrust, dbName, logOnUserId, logOnPassword);
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
		public virtual void	CallEPM(string serverRealName, string instanceName, bool isUseTrust, string dbName, string logOnUserId, string logOnPassword)
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
			isqlProcess.StartInfo.FileName = "SqlWb";
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
					isqlProcess.StartInfo.Arguments = string.Format(System.Globalization.CultureInfo.CurrentCulture," -S {0} -d {1} -E -nosplash",
						serverstr,
						dbName
						);
				}
				else
				{
					isqlProcess.StartInfo.Arguments = string.Format(System.Globalization.CultureInfo.CurrentCulture," -S {0} -E -nosplash",
						serverstr
						);
				}
			}
			else
			{
				if( dbName.Length != 0 )
				{
					isqlProcess.StartInfo.Arguments = string.Format(System.Globalization.CultureInfo.CurrentCulture," -S {0} -d {1} -U {2} -P {3} -nosplash",
						serverstr,
						dbName,
						logOnUserId,
						logOnPassword );
				}
				else
				{
					isqlProcess.StartInfo.Arguments = string.Format(System.Globalization.CultureInfo.CurrentCulture," -S {0} -U {1} -P {2} -nosplash",
						serverstr,
						logOnUserId,
						logOnPassword );
				}
			}
			
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
		public virtual void	CallProfile(string serverRealName, string instanceName, bool isUseTrust, string dbName, string logOnUserId, string logOnPassword)
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
			isqlProcess.StartInfo.FileName = "profiler90.exe";
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
		public virtual string	GetDDLDropStr(DBObjectInfo databaseObjectInfo)
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
						return string.Format(System.Globalization.CultureInfo.CurrentCulture,"DROP TABLE {0}{1}GO{1}{1}", databaseObjectInfo.RealObjName,Environment.NewLine);
					}
					else
					{
						return string.Format(System.Globalization.CultureInfo.CurrentCulture,"DROP VIEW {0}{1}GO{1}{1}", databaseObjectInfo.RealObjName,Environment.NewLine);
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
		public virtual string	GetDdlCreateString(DBObjectInfo databaseObjectInfo, bool useParentheses)
		{
			if( databaseObjectInfo == null )
			{
				throw new ArgumentNullException("databaseObjectInfo");
			}
			StringBuilder strline =  new StringBuilder();
			TextWriter	wr = new StringWriter(strline,System.Globalization.CultureInfo.CurrentCulture);

			if( databaseObjectInfo.RealObjType == "U" )
			{
				int		maxRow = databaseObjectInfo.FieldInfo.Count;
				if( useParentheses )
				{
					wr.Write("Create table {0} ", databaseObjectInfo.RealObjName);
				}
				else
				{
					wr.Write("Create table {0} ", databaseObjectInfo.RealObjNameNoPare);
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
						wr.Write("\t[{0}]", ((DBFieldInfo)databaseObjectInfo.FieldInfo[i]).Name);
					}
					else
					{
						wr.Write("\t{0}", ((DBFieldInfo)databaseObjectInfo.FieldInfo[i]).Name);
					}
					wr.Write("\t");
					// 型
					valtype = ((DBFieldInfo)databaseObjectInfo.FieldInfo[i]).TypeName;

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
						if( ((DBFieldInfo)databaseObjectInfo.FieldInfo[i]).Length == -1 )
						{
							wr.Write(" (max)");
						}
						else
						{
							wr.Write(" ({0})", ((DBFieldInfo)databaseObjectInfo.FieldInfo[i]).Length);
						}
					}
					else if( valtype == "numeric" ||
						valtype == "decimal" )
					{
						wr.Write(" ({0},{1})", ((DBFieldInfo)databaseObjectInfo.FieldInfo[i]).Prec,
							((DBFieldInfo)databaseObjectInfo.FieldInfo[i]).Xscale);
					}
					wr.Write("\t");
						
					if( ((DBFieldInfo)databaseObjectInfo.FieldInfo[i]).Collation.Length != 0)
					{
						wr.Write("COLLATE {0}",((DBFieldInfo)databaseObjectInfo.FieldInfo[i]).Collation);
						wr.Write("\t");
					}

					if( ((DBFieldInfo)databaseObjectInfo.FieldInfo[i]).IncSeed != 0)
					{
						wr.Write("\tIDENTITY({0},{1})",
							((DBFieldInfo)databaseObjectInfo.FieldInfo[i]).IncSeed,
							((DBFieldInfo)databaseObjectInfo.FieldInfo[i]).IncStep );
					}
						
					if( ((DBFieldInfo)databaseObjectInfo.FieldInfo[i]).IsNullable == false )
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


				string strsql = string.Format(System.Globalization.CultureInfo.CurrentCulture,"sp_helptext '{0}'", databaseObjectInfo.RealObjName );
				SqlDataAdapter	da = new SqlDataAdapter(strsql,this.sqlConnect);
				da.SelectCommand.CommandTimeout = this.queryTimeout;
				da.Fill(dt);
				// 連続した空白行は抑制するようにする
				string pretext = "";
				foreach(DataRow dr in dt.Rows)
				{
					if( dr["Text"] != DBNull.Value &&
						dr["Text"] is string &&
						pretext == string.Empty &&
						dr["Text"].ToString().Trim() == string.Empty )
					{
						continue;
					}
					pretext = dr["Text"].ToString().Trim();
					wr.WriteLine(dr["Text"].ToString().Trim());
				}
				wr.WriteLine("Go");
			}
			return strline.ToString();
		}

		/// <summary>
		/// オブジェクト情報をセットするDataTableを初期化する
		/// </summary>
		/// <param name="objTable">対象とするDataTable</param>
		public virtual void	InitObjTable(DataTable objTable)
		{
			if( objTable == null )
			{
				throw new ArgumentNullException("objTable");
			}
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
		/// <param name="databaseObjectInfo">対象となるオブジェクト</param>
		/// <param name="dt"></param>
		public virtual void	AddObjectInfo(DBObjectInfo databaseObjectInfo, DataTable dt)
		{
			if( databaseObjectInfo == null )
			{
				throw new ArgumentNullException("databaseObjectInfo");
			}
			if( dt == null )
			{
				throw new ArgumentNullException("dt");
			}
			string	strsql = string.Format(System.Globalization.CultureInfo.CurrentCulture,"select * from sys.all_objects where object_id = OBJECT_ID('{0}') ",
				databaseObjectInfo.FormalName );
			SqlCommand	cm = new SqlCommand(strsql,this.sqlConnect);

			SqlDataAdapter	da = new SqlDataAdapter(strsql,this.sqlConnect);
			DataTable	odt = new DataTable("sysobjects");
			odt.Locale = System.Globalization.CultureInfo.CurrentCulture;
			da.Fill(odt);



			DataRow dr = dt.NewRow();

			if( odt.Rows.Count != 0 )
			{
				dr[0] = odt.Rows[0]["object_id"];
				dr[5] = odt.Rows[0]["modify_date"].ToString();
			}
			dr[1] = databaseObjectInfo.ObjName;
			dr[2] = databaseObjectInfo.Owner;
			dr[3] = GetObjTypeName(databaseObjectInfo.ObjType);
			dr[4] = databaseObjectInfo.CreateTime;
			if( databaseObjectInfo.IsSynonym )
			{
				dr[6] = databaseObjectInfo.SynonymBase;
				dr[7] = GetObjTypeName(databaseObjectInfo.SynonymBaseType);
			}
			else
			{
				dr[6] = string.Empty;
				dr[7] = string.Empty;
			}
			dt.Rows.Add(dr);
		}

		internal static string GetObjTypeName(string objType)
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
		public virtual DataGetEventHandler ObjectDetailSet()
		{
			return new DataGetEventHandler(this.DatabaseObjSet);
		}

		/// <summary>
		/// DBオブジェクトを取得する
		/// </summary>
		/// <param name="sender">-</param>
		/// <param name="e">-</param>
		protected	virtual void	DatabaseObjSet(object sender, System.EventArgs e)
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
	t1.is_identity,
	case 
	when t5.object_id is not null then ident_seed('{0}') 
	else 0
	end as seed,
	case 
	when t5.object_id is not null then ident_incr('{0}') 
	else 0
	end as incr,
	t6.assembly_id,
	t6.assembly_class,
	t6.assembly_qualified_name
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
	left outer join sys.assembly_types t6 on
		t6.user_type_id = t1.user_type_id
where 
	t1.object_id = OBJECT_ID('{0}')
order by colorder",
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
					addInfo.TypeName = string.Format(System.Globalization.CultureInfo.CurrentCulture,"[{0}].[{1}]",
							(string)fdr["typeSchema"] ,
							(string)fdr["valtype"] );
				}
				if( fdr["is_identity"] != DBNull.Value &&
					(bool)fdr["is_identity"] == true )
				{
					addInfo.IsIdentity = true;
				}
				else
				{
					addInfo.IsIdentity = false;
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
				if( fdr["assembly_id"] != DBNull.Value )
				{
					addInfo.AssemblyId = (int)fdr["assembly_id"];
				}
				if( fdr["assembly_class"] != DBNull.Value )
				{
					addInfo.AssemblyClassName = (string)fdr["assembly_class"];
				}
				if( fdr["assembly_qualified_name"] != DBNull.Value )
				{
					addInfo.AssemblyQFN = (string)fdr["assembly_qualified_name"];
				}
				ar.Add(addInfo);
			}
			databaseObjectInfo.FieldInfo = ar;
		}


		#endregion
	}
}
