using System;
using System.Collections;
using System.Collections.Generic;
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
		private System.Data.SqlClient.SqlConnection pSqlConnect;

		/// <summary>
		/// コネクション
		/// </summary>
		protected System.Data.SqlClient.SqlConnection SqlConnect
		{
			get { return pSqlConnect; }
			set { pSqlConnect = value; }
		}

		/// <summary>
		/// SelectCommand 等のタイムアウト値
		/// </summary>
		private int pQueryTimeout;

		/// <summary>
		/// SelectCommand 等のタイムアウト値
		/// </summary>
		protected int QueryTimeout
		{
			get { return pQueryTimeout; }
			set { pQueryTimeout = value; }
		}

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
			this.pSqlConnect = (System.Data.SqlClient.SqlConnection)sqlConnection;
			this.QueryTimeout = timeout;
		}

		/// <summary>
		/// SQLSERVERに対するコネクション情報を閉じる
		/// </summary>
		public void CloseConnection()
		{
			this.pSqlConnect.Close();
		}

		/// <summary>
		/// タイムアウト値を設定しなおす
		/// </summary>
		/// <param name="timeout"></param>
		public void SetTimeout(int timeout)
		{
			this.QueryTimeout = timeout;
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
			sqlCmd.Connection = this.pSqlConnect;
			sqlCmd.CommandTimeout = this.QueryTimeout;
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
			SqlCommand	sqlCmd = new SqlCommand(stSql,this.pSqlConnect);
			sqlCmd.CommandTimeout = this.QueryTimeout;
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
			cmd.Transaction = this.pSqlConnect.BeginTransaction();
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
		/// DataReaderからDateTimeOffset値を読み込む。
		/// </summary>
		/// <param name="dr"></param>
		/// <param name="col"></param>
		/// <returns></returns>
		public virtual DateTimeOffset GetDataReaderDateTimeOffSet(IDataReader dr, int col)
		{
			throw new InvalidOperationException("GetDataReaderDateTimeOffSet は SQL Server 2005 では利用できません");
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
			this.pSqlConnect.ChangeDatabase(dbName);
		}

		/// <summary>
		/// オブジェクト一覧のカラムヘッダの表示文字を取得する
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
			return "オーナー名・オブジェクト名";
		}

		/// <summary>
		/// オブジェクト一覧の表示用SQLの取得
		/// </summary>
		/// <param name="isDisplayTable">テーブルを表示させるか否か true: 表示する false: 表示させない</param>
		/// <param name="isDisplayView">View を表示させるか否か true: 表示する false: 表示させない</param>
		/// <param name="isSynonym">シノニムを表示させるか否か true: 表示する false: 表示させない</param>
		/// <param name="isDisplayFunction">Functionを表示させるか否か true: 表示する false: 表示させない</param>
		/// <param name="isDisplaySP">ストアドプロシージャを表示させるか否か true: 表示する false: 表示させない</param>
		/// <param name="ownerList">特定のOwnerのオブジェクトのみ表示する場合は IN句に利用するカンマ区切り文字列を渡す</param>
		/// <returns></returns>
		public string GetDisplayObjList(bool isDisplayTable, bool isDisplayView, bool isSynonym, bool isDisplayFunction, bool isDisplaySP, string ownerList)
		{
			string destObj = "";

			List<string> ar = new List<string>();

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
				sb.Append("'").Append(ar[i]).Append("'");
			}
			destObj = sb.ToString();
			sb.Length  = 0;
			sb.AppendFormat(@"select 
					sysobjects.name as tbname, 
					sysusers.name as uname ,
					xtype as tvs,
					sysobjects.crdate as cretime,
					'' as synbase,
					'' as synType,
                    id as objectid
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

			Microsoft.Win32.RegistryKey rkey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server\80\Tools\ClientSetup\", false);
			string profilerPath = string.Empty;
			if (rkey != null)
			{
				bool isPathExists = true;
				object robj = rkey.GetValue("Path");
				if (robj != null)
				{
					profilerPath = robj.ToString();
				}
				if( profilerPath == string.Empty )
				{
					isPathExists = false;
					robj = rkey.GetValue("SQLPath");
					if (robj != null)
					{
						profilerPath = robj.ToString();
					}
				}
				if (profilerPath != null)
				{
					if (profilerPath.EndsWith(@"\") == false)
					{
						profilerPath += @"\";
					}
					if (isPathExists == false)
					{
						profilerPath += @"binn\";
					}
				}
			}

			Process isqlProcess = new Process();
			isqlProcess.StartInfo.FileName = profilerPath + "profiler.exe";
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
				if( useParentheses )
				{
					wr.Write("Create table {0} ", databaseObjectInfo.RealObjName);
				}
				else
				{
					wr.Write("Create table {0} ", databaseObjectInfo.RealObjNameNoPare);
				}
				wr.Write(" ( {0}",wr.NewLine);

                int i = 0;
                foreach(DBFieldInfo each in databaseObjectInfo.FieldInfo)
                {
					if( i != 0 )
					{
						wr.Write(",{0}",wr.NewLine);
					}
					wr.Write("\t");
					//フィールド名
					if( useParentheses )
					{
						wr.Write(each.FormalName);
					}
					else
					{
						wr.Write(each.Name);
					}
					wr.Write("\t");

                    wr.Write(each.GetDDLTypeString(useParentheses));

                    i++;
				}
				wr.Write("{0}){0}Go{0}",wr.NewLine);
			}
			else
			{
				DataTable dt = new DataTable();
				dt.CaseSensitive = true;
				dt.Locale = System.Globalization.CultureInfo.CurrentCulture;


				string strsql = string.Format(System.Globalization.CultureInfo.CurrentCulture,"sp_helptext '{0}'", databaseObjectInfo.RealObjName );
				SqlDataAdapter	da = new SqlDataAdapter(strsql,this.pSqlConnect);
				da.SelectCommand.CommandTimeout = this.QueryTimeout;
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
			SqlCommand	cm = new SqlCommand(strsql,this.pSqlConnect);

			SqlDataAdapter	da = new SqlDataAdapter(strsql,this.pSqlConnect);
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
			return new DataGetEventHandler(this.DatabaseObjSet);
		}

		/// <summary>
		/// 指定されたオブジェクトの詳細情報を取得する
		/// sender に 対象オブジェクトが DBObjectInfo 型としてセットされていることが前提
		/// </summary>
		/// <param name="sender">対象のオブジェクト</param>
		/// <param name="e">ダミー</param>
        private void DatabaseObjSet(DBObjectInfo sender, System.EventArgs e)
		{
			DBObjectInfo	databaseObjectInfo = (DBObjectInfo)sender;
			DataSet		ds = new DataSet(databaseObjectInfo.ObjName);
			ds.CaseSensitive = true;
			ds.Locale = System.Globalization.CultureInfo.CurrentCulture;


			// まずは必要な情報を全て収集する

			// FillSchema での情報収集
			string strsql = string.Format(System.Globalization.CultureInfo.CurrentCulture,"select * from {0} where 0=1",
				databaseObjectInfo.FormalName );
			SqlDataAdapter da = new SqlDataAdapter(strsql,this.pSqlConnect);
			DataTable []dt = da.FillSchema(ds,SchemaType.Mapped,"schema");
            databaseObjectInfo.SetSchemaInfo(dt[0]);

			// 実際の細かい情報を直接取得する
			SqlDataAdapter tableda = new SqlDataAdapter(
				string.Format(System.Globalization.CultureInfo.CurrentCulture,
				@"select 
syscolumns.name colname, 
t1.name valtype, 
t2.name baseValType, 
convert(int,syscolumns.length) as length, 
isnull(convert(int,syscolumns.prec),0) as prec, 
isnull(convert(int,syscolumns.xscale),0) as xscale, 
convert(int,syscolumns.colid) as colid, 
convert(int,syscolumns.colorder) as colorder, 
syscolumns.isnullable, 
syscolumns.collation, 
sysobjects.id  ,
case
when syscolumns.colstat & 1 = 1 then 1
else								 0
end		as is_identity,
case
when syscolumns.colstat & 1 = 1 
	then ident_seed('{0}')
else 0
end     as seed,
case
when syscolumns.colstat & 1 = 1 
	then ident_incr('{0}')
else 0
end     as incr
from 
	sysobjects, 
	syscolumns, 
	sysusers, 
	systypes t1,
	systypes t2
where 
	sysobjects.id = syscolumns.id 
and sysobjects.uid= sysusers.uid 
and syscolumns.xusertype=t1.xusertype 
and syscolumns.xtype=t2.xusertype 
and sysobjects.id = OBJECT_ID('{0}')
order by syscolumns.colorder",
				databaseObjectInfo.RealObjName
				),
				this.pSqlConnect );
			tableda.Fill(ds,"fieldList");

			DBFieldInfo addInfo;
            databaseObjectInfo.ClearField();
            foreach (DataRow fdr in ds.Tables["fieldList"].Rows)
			{
                addInfo = GetDBFieldInfo(ds, fdr);
                databaseObjectInfo.AddField(addInfo);
			}
		}

        private DBFieldInfo GetDBFieldInfo(DataSet ds, DataRow fdr)
        {
            DBFieldInfo addInfo;

            addInfo = new DBFieldInfo();
            addInfo.Col = ds.Tables["schema"].Columns[fdr["colname"].ToString()];
            addInfo.Colid = (int)fdr["colid"];
            if (fdr["collation"] != DBNull.Value)
            {
                addInfo.Collation = (string)fdr["collation"];
            }
            addInfo.ColOrder = (int)fdr["colorder"];
            if (addInfo.Col.MaxLength < 0 &&
                (int)fdr["length"] > 0)
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
            addInfo.RealTypeName = (string)fdr["baseValType"];

            if (fdr["is_identity"] != DBNull.Value &&
                (int)fdr["is_identity"] == 1)
            {
                addInfo.IsIdentity = true;
            }
            else
            {
                addInfo.IsIdentity = false;
            }
            if (fdr["seed"] != DBNull.Value)
            {
                addInfo.IncSeed = (decimal)fdr["seed"];
            }
            if (fdr["incr"] != DBNull.Value)
            {
                addInfo.IncStep = (decimal)fdr["incr"];
            }

            // プライマリキーかどうかをチェック
            for (int i = 0; i < ds.Tables["schema"].PrimaryKey.Length; i++)
            {
                if (addInfo.Col.ColumnName == ds.Tables["schema"].PrimaryKey[i].ColumnName)
                {
                    addInfo.PrimaryKeyOrder = i;
                    break;
                }
            }
            return addInfo;
        }

		/// <summary>
		/// フィールド名を検索する SQL文を生成する
		/// </summary>
		/// <param name="searchCondition">検索対象の文字</param>
		/// <param name="searchType">検索方法</param>
		/// <param name="isCaseSensitive">大文字小文字を区別するか否か</param>
		/// <param name="limitSchema">スキーマの絞込み対象</param>
		/// <returns></returns>
		public string	GetSearchFieldSql(
			string searchCondition, 
			quickDBExplorer.SearchType searchType, 
			bool isCaseSensitive,
			List<string> limitSchema)
		{
			searchCondition = searchCondition.Replace("'","''");
			if( isCaseSensitive == false )
			{
				searchCondition = searchCondition.ToLower(System.Globalization.CultureInfo.CurrentCulture);
			}

			string schemaFilter = string.Empty;
			if( limitSchema != null && 
				limitSchema.Count != 0 )
			{
				schemaFilter = "and t2.name in ( ";
				for( int j = 0; j < limitSchema.Count; j++ )
				{
					if( j != 0 )
					{
						schemaFilter += ",";
					}
					schemaFilter += "'" + limitSchema[j] + "'";
				}
				schemaFilter += ")";
			}

			string condSql = string.Empty;

			switch(searchType)
			{
				case SearchType.SearchContain:
					condSql = string.Format(System.Globalization.CultureInfo.CurrentCulture, " like '%{0}%' ", searchCondition );
					break;
				case SearchType.SearchStartWith:
					condSql = string.Format(System.Globalization.CultureInfo.CurrentCulture, " like '{0}%' ", searchCondition);
					break;
				case SearchType.SearchExact:
					condSql = string.Format(System.Globalization.CultureInfo.CurrentCulture, " = '{0}' ", searchCondition);
					break;
				default:
					break;
			}

			string addCondition = string.Empty;
			if( isCaseSensitive == true )
			{
				addCondition = " t3.name ";
			}
			else
			{
				addCondition = " LOWER(t3.name) ";
			}

			return string.Format(System.Globalization.CultureInfo.CurrentCulture,
				@"select t2.name as UserName, t1.name as ObjName, t3.name as FieldName
from
	sysobjects t1
	inner join sysusers  t2 on
		t1.uid = t2.uid
	inner join syscolumns t3 on
		t1.id = t3.id		
where
	{0} {1} {2} ", addCondition, condSql , schemaFilter
				);
		}

		/// <summary>
		/// オブジェクト名を検索する SQL文を生成する
		/// </summary>
		/// <param name="searchCondition">検索対象の文字</param>
		/// <param name="searchType">検索方法</param>
		/// <param name="isCaseSensitive">大文字小文字を区別するか否か</param>
		/// <param name="limitSchema">スキーマの絞込み対象</param>
		/// <param name="isTable">テーブルを検索するか否か</param>
		/// <param name="isView">Viewを検索するか否か</param>
		/// <param name="isSynonym">シノニムを検索するか否か</param>
		/// <param name="isFunction">ファンクションを検索するか否か</param>
		/// <param name="isProcedure">ストアドプロシージャーを検索するか否か</param>
		/// <returns></returns>
		public string	GetSearchObjectSql(
			string searchCondition, 
			quickDBExplorer.SearchType searchType, 
			bool isCaseSensitive,
			List<string> limitSchema,
			bool isTable, 
			bool isView, 
			bool isSynonym, 
			bool isFunction, 
			bool isProcedure)
		{
			searchCondition = searchCondition.Replace("'","''");
			if( isCaseSensitive == false )
			{
				searchCondition = searchCondition.ToLower(System.Globalization.CultureInfo.CurrentCulture);
			}

			string condSql = string.Empty;

			switch(searchType)
			{
				case SearchType.SearchContain:
					condSql = string.Format(System.Globalization.CultureInfo.CurrentCulture, " like '%{0}%' ", searchCondition);
					break;
				case SearchType.SearchStartWith:
					condSql = string.Format(System.Globalization.CultureInfo.CurrentCulture, " like '{0}%' ", searchCondition);
					break;
				case SearchType.SearchExact:
					condSql = string.Format(System.Globalization.CultureInfo.CurrentCulture, " = '{0}' ", searchCondition);
					break;
				default:
					break;
			}

			List<string> ar = new List<string>();

			if( isTable == true )
			{
				ar.Add("U ");
				ar.Add("S ");
			}
			if( isView == true )
			{
				ar.Add("V ");
			}
			if( isSynonym == true )
			{
				ar.Add("SN");
			}

			if( isFunction == true )
			{
				ar.Add("FN");
				ar.Add("TF");
				ar.Add("IF");
			}

			if( isProcedure == true )
			{
				ar.Add("P ");
				ar.Add("X ");
			}

			string typeCondition = string.Empty;
			for( int i = 0; i < ar.Count; i++ )
			{
				if( i != 0 )
				{
					typeCondition += ",";
				}
				typeCondition += "'" + ar[i] + "'";
			}

			string addCondition = string.Empty;
			if( isCaseSensitive == true )
			{
				addCondition = " t1.name ";
			}
			else
			{
				addCondition = " LOWER(t1.name) ";
			}

			string schemaFilter = string.Empty;
			if( limitSchema != null && 
				limitSchema.Count != 0 )
			{
				schemaFilter = " and t2.name in ( ";
				for( int j = 0; j < limitSchema.Count; j++ )
				{
					if( j != 0 )
					{
						schemaFilter += ",";
					}
					schemaFilter += "'" + limitSchema[j] + "'";
				}
				schemaFilter += ")";
			}

			return string.Format(System.Globalization.CultureInfo.CurrentCulture,
				@"select t2.name as UserName, t1.name as ObjName, null as FieldName
from
	sysobjects t1
	inner join sysusers  t2 on
		t1.uid = t2.uid
where
	{0} {1} and t1.xtype in ( {2} ) {3} ", addCondition, condSql, typeCondition, schemaFilter
				);		
		}
		#endregion
	}
}
