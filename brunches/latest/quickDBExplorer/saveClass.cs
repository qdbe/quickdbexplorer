using System;
using System.Collections;
using System.Xml;
using System.Data;
using System.IO;
using System.Configuration;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;

namespace quickDBExplorer
{
	/// <summary>
	/// サーバー別の各種指定履歴データを管理する
	/// </summary>
	[Serializable]
	public class ServerData : ISerializable
	{
		/// <summary>
		/// 接続先サーバー名
		/// </summary>
		protected string		servername;
		/// <summary>
		/// 接続先インスタンス名
		/// </summary>
		protected string		instancename;
		/// <summary>
		/// サーバー＋インスタンスを利用した接続先サーバーのHashtableのキー情報
		/// </summary>
		protected string		keyname;

		/// <summary>
		/// 最後に利用したDB名
		/// </summary>
		protected string		lastdb;		
		/// <summary>
		/// 最後に利用したDB名
		/// </summary>
		public string		LastDb
		{
			get { return this.lastdb; }
			set { this.lastdb = value; }
		}

		/// <summary>
		/// // DB 毎の最終ユーザーを記録する
		/// 複数を登録させるため、内部はstring の要素を持ったArrayListとする
		/// </summary>
		protected Hashtable		dbopt;	
		/// <summary>
		/// // DB 毎の最終ユーザーを記録する
		/// 複数を登録させるため、内部はstring の要素を持ったArrayListとする
		/// </summary>
		public Hashtable		Dbopt
		{
			get { return this.dbopt; }
			set { this.dbopt = value; }
		}

		/// <summary>
		/// システムユーザーを表示するか否か
		/// </summary>
		protected int	isShowsysuser;	
		/// <summary>
		/// システムユーザーを表示するか否か
		/// </summary>
		public int	IsShowsysuser
		{
			get { return this.isShowsysuser; }
			set { this.isShowsysuser = value; }
		}

		/// <summary>
		///table/view リストのソート順 
		/// </summary>
		protected int	sortKey;
		/// <summary>
		///table/view リストのソート順 
		/// </summary>
		public int	SortKey
		{
			get { return this.sortKey ; }
			set { this.sortKey = value; }
		}

		/// <summary>
		/// view を表示させるかどうか
		/// </summary>
		protected int	showView;
		/// <summary>
		/// view を表示させるかどうか
		/// </summary>
		public int	ShowView
		{
			get { return this.showView; }
			set { this.showView = value; }
		}
		
		/// <summary>
		/// データ出力先の指定
		/// </summary>
		protected	Hashtable	outdest;
		/// <summary>
		/// データ出力先の指定
		/// </summary>
		public	Hashtable	OutDest
		{
			get { return this.outdest; }
			set { this.outdest = value; }
		}
		/// <summary>
		/// データ出力先のファイル・フォルダ名
		/// </summary>
		protected	Hashtable	outfile;
		/// <summary>
		/// データ出力先のファイル・フォルダ名
		/// </summary>
		public	Hashtable	OutFile
		{
			get { return this.outfile; }
			set { this.outfile = value; }
		}
		/// <summary>
		/// データグリッドを表示するか否か
		/// </summary>
		protected	Hashtable	showgrid;
		/// <summary>
		/// データグリッドを表示するか否か
		/// </summary>
		public	Hashtable	ShowGrid
		{
			get { return this.showgrid; }
			set { this.showgrid = value; }
		}

		/// <summary>
		/// グリッド表示件数
		/// </summary>
		protected 	Hashtable	griddspcnt;
		/// <summary>
		/// グリッド表示件数
		/// </summary>
		public 	Hashtable	GridDspCnt
		{
			get { return this.griddspcnt; }
			set { this.griddspcnt = value; }
		}
		
		/// <summary>
		/// テキスト出力時の文字コード
		/// </summary>
		protected	Hashtable	txtencode;
		/// <summary>
		/// テキスト出力時の文字コード
		/// </summary>
		public	Hashtable	TxtEncode
		{
			get { return this.txtencode; }
			set { this.txtencode = value; }
		}
        /// <summary>
		/// サーバー別情報を記憶するか否か
		/// </summary>
		protected	bool	isSaveKey = true;
		/// <summary>
		/// サーバー別情報を記憶するか否か
		/// </summary>
		public	bool	IsSaveKey
		{
			get { return this.isSaveKey; }
			set { this.isSaveKey = value; }
		}
		/// <summary>
		/// 信頼関係接続を利用するか否か
		/// </summary>
		protected	bool	isUseTrust = false;
		/// <summary>
		/// 信頼関係接続を利用するか否か
		/// </summary>
		public	bool	IsUseTrust
		{
			get { return this.IsUseTrust; }
			set { this.IsUseTrust = value; }
		}
		/// <summary>
		/// ログインユーザー名
		/// </summary>
		protected	string	loginUser = "";
		/// <summary>
		/// ログインユーザー名
		/// </summary>
		public	string	LoginUser
		{
			get { return this.loginUser; }
			set { this.loginUser = value; }
		}
		/// <summary>
		/// where 句の入力履歴情報
		/// </summary>
		protected textHistory  whereHistory;
		/// <summary>
		/// where 句の入力履歴情報
		/// </summary>
		public textHistory  WhereHistory
		{
			get { return this.whereHistory; }
			set { this.whereHistory = value; }
		}

		/// <summary>
		/// order by 句の入力履歴情報
		/// </summary>
		protected textHistory  sortHistory;
		/// <summary>
		/// order by 句の入力履歴情報
		/// </summary>
		public textHistory  SortHistory
		{
			get { return this.sortHistory; }
			set { this.sortHistory = value; }
		}

		/// <summary>
		/// order by 句の入力履歴情報
		/// </summary>
		protected textHistory  aliasHistory;
		/// <summary>
		/// order by 句の入力履歴情報
		/// </summary>
		public textHistory  AliasHistory
		{
			get { return this.aliasHistory; }
			set { this.aliasHistory = value; }
		}

		/// <summary>
		/// select 実行履歴情報
		/// </summary>
		protected textHistory  selectHistory;
		/// <summary>
		/// select 実行履歴情報
		/// </summary>
		public textHistory  SelectHistory
		{
			get { return this.selectHistory; }
			set { this.selectHistory = value; }
		}

		/// <summary>
		/// クエリ発行時の入力履歴
		/// </summary>
		protected textHistory  dmlHistory;
		/// <summary>
		/// クエリ発行時の入力履歴
		/// </summary>
		public textHistory  DMLHistory
		{
			get { return this.dmlHistory; }
			set { this.dmlHistory = value; }
		}

		/// <summary>
		/// 各種コマンド入力履歴
		/// </summary>
		protected textHistory  cmdHistory;
		/// <summary>
		/// 各種コマンド入力履歴
		/// </summary>
		public textHistory  CmdHistory
		{
			get { return this.cmdHistory; }
			set { this.cmdHistory = value; }
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ServerData()
		{
			dbopt = new Hashtable();
			isShowsysuser = 0;
			sortKey = 0;
			showView = 0;
			IsUseTrust = false;
			outdest =  new Hashtable();
			outfile =  new Hashtable();
			showgrid =  new Hashtable();
			griddspcnt =  new Hashtable();
			txtencode = new Hashtable();
			whereHistory = new textHistory();
			sortHistory = new textHistory();
			aliasHistory = new textHistory();
			selectHistory = new textHistory();
			DMLHistory = new textHistory();
		}

		/// <summary>
		/// シリアライズ処理用コンストラクタ
		/// </summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		public ServerData(SerializationInfo info, StreamingContext context)
		{
			servername = "";
			instancename = "";
			keyname = "";
			lastdb = "";
			dbopt = new Hashtable();
			isShowsysuser = 0;
			sortKey = 0;
			showView = 0;
			IsUseTrust = false;
			outdest =  new Hashtable();
			outfile =  new Hashtable();
			showgrid =  new Hashtable();
			griddspcnt =  new Hashtable();
			txtencode = new Hashtable();
			whereHistory = new textHistory();
			sortHistory = new textHistory();
			aliasHistory = new textHistory();
			selectHistory = new textHistory();
			DMLHistory = new textHistory();
			cmdHistory = new textHistory();

			try
			{
				servername = info.GetString("servername");
			}
			catch {}
			try
			{
				this.instancename = info.GetString("instancename");
			}
			catch {}
			try
			{
				this.keyname = info.GetString("keyname");
			}
			catch {}
			try
			{
				this.lastdb = info.GetString("lastdb");
			}
			catch {}
			try
			{
				this.dbopt = (Hashtable)info.GetValue("dbopt",typeof(Hashtable));
			}
			catch {}

			try
			{
				this.isShowsysuser = info.GetInt32("isShowsysuser");
			}
			catch {}

			try
			{
				this.sortKey = info.GetInt32("sortKey");
			}
			catch{}

			try
			{
				this.showView = info.GetInt32("showView");
			}
			catch{}

			try
			{
				this.outdest = (Hashtable)info.GetValue("outdest",typeof(Hashtable));
			}
			catch{}

			try
			{
				this.outfile = (Hashtable)info.GetValue("outfile",typeof(Hashtable));
			}
			catch{}

			try
			{
				this.showgrid = (Hashtable)info.GetValue("showgrid",typeof(Hashtable));
			}
			catch{}

			try
			{
				this.griddspcnt = (Hashtable)info.GetValue("griddspcnt",typeof(Hashtable));
			}
			catch{}

			try
			{
				this.txtencode = (Hashtable)info.GetValue("txtencode",typeof(Hashtable));
			}
			catch{}

			try
			{
				this.isSaveKey = info.GetBoolean("isSaveKey");
			}
			catch{}


			try
			{
				this.IsUseTrust = info.GetBoolean("IsUseTrust");
			}
			catch{}

			try
			{
				this.loginUser = info.GetString("loginUser");
			}
			catch{}

			try
			{
				this.whereHistory = (textHistory)info.GetValue("whereHistory",typeof(textHistory));
			}
			catch{}
			try
			{
				this.sortHistory = (textHistory)info.GetValue("sortHistory",typeof(textHistory));
			}
			catch{}
			try
			{
				this.aliasHistory = (textHistory)info.GetValue("aliasHistory",typeof(textHistory));
			}
			catch{}
			try
			{
				this.selectHistory = (textHistory)info.GetValue("selectHistory",typeof(textHistory));
			}
			catch{}
			try
			{
				this.DMLHistory = (textHistory)info.GetValue("DMLHistory",typeof(textHistory));
			}
			catch{}
			try
			{
				this.cmdHistory = (textHistory)info.GetValue("cmdHistory",typeof(textHistory));
			}
			catch{}


		}

		/// <summary>
		/// シリアライズ処理用
		/// </summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		public virtual void GetObjectData(
			SerializationInfo info, StreamingContext context)
		{
			info.AddValue("servername",	servername );
			info.AddValue("instancename", instancename );
			info.AddValue("keyname", keyname );
			info.AddValue("lastdb", lastdb );
			info.AddValue("dbopt", dbopt);
			info.AddValue("isShowsysuser",isShowsysuser);
			info.AddValue("sortKey", sortKey );
			info.AddValue("showView", showView );
			info.AddValue("IsUseTrust", IsUseTrust );
			info.AddValue("outdest", outdest );
			info.AddValue("outfile", outfile );
			info.AddValue("showgrid", showgrid );
			info.AddValue("griddspcnt", griddspcnt );
			info.AddValue("txtencode", txtencode );
			info.AddValue("isSaveKey", isSaveKey );
			info.AddValue("loginUser", loginUser );
			info.AddValue("whereHistory", whereHistory );
			info.AddValue("sortHistory", sortHistory );
			info.AddValue("aliasHistory", aliasHistory );
			info.AddValue("selectHistory", selectHistory );
			info.AddValue("DMLHistory", DMLHistory );
			info.AddValue("cmdHistory", cmdHistory );
		}

		/// <summary>
		/// 接続先サーバー名
		/// </summary>
		public string Servername
		{
			get { return this.servername; }
			set 
			{ 
				this.servername = value; 
				this.keyname = value + this.instancename;
			}
		}
		/// <summary>
		/// 接続先インスタンス名
		/// </summary>
		public string InstanceName
		{
			get { return this.instancename; }
			set 
			{
				this.instancename = value;
				this.keyname = this.servername + this.instancename;
			}
		}
		/// <summary>
		/// サーバー＋インスタンスを利用した接続先サーバーのHashtableのキー情報
		/// </summary>
		public string KeyName
		{
			get 
			{ 
				return this.keyname; 
			}
		}
	}

	/// <summary>
	/// saveClass の概要の説明です。
	/// </summary>
	[Serializable]
	public class saveClass : ISerializable
	{
		/// <summary>
		/// サーバー別情報を管理するハッシュテーブル
		/// サーバー名＋インスタンス名をキーとする
		/// </summary>
		protected Hashtable	perServerData;
		/// <summary>
		/// サーバー別情報を管理するハッシュテーブル
		/// サーバー名＋インスタンス名をキーとする
		/// </summary>
		public Hashtable	PerServerData
		{
			get { return this.ht; }
			set { this.ht = value; }
		}

		/// <summary>
		/// 最後に接続したサーバーのHashkey
		/// </summary>
		protected string lastserverkey = "";
		/// <summary>
		/// 最後に接続したサーバーのHashkey
		/// </summary>
		public string LastServerKey
		{
			get { return this.lastserverkey; }
			set { this.lastserverkey = value; }
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public saveClass()
		{
			this.perServerData = new Hashtable();
		}

		/// <summary>
		/// シリアライズ処理用コンストラクタ
		/// </summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		public saveClass(SerializationInfo info, StreamingContext context)
		{
			try 
			{ 
				this.lastserverkey = info.GetString("LASTSERVERKEY"); 
			} 
			catch
			{
				this.lastserverkey = "";
			}
			try { 
				this.perServerData = (Hashtable)info.GetValue("SERVERDATA",typeof(Hashtable));
			} 
			catch
			{
				this.perServerData = new Hashtable();
			}
		}

		/// <summary>
		/// シリアライズ処理用
		/// </summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		public virtual void GetObjectData(
			SerializationInfo info, StreamingContext context)
		{
			info.AddValue("LASTSERVERKEY", this.lastserverkey);
			info.AddValue("SERVERDATA", this.perServerData, typeof(Hashtable) );
		}

	}
}
