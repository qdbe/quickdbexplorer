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
	public sealed class ServerData : ISerializable, IDisposable 
	{
		/// <summary>
		/// 接続先サーバー名
		/// </summary>
		private string		servername;
		/// <summary>
		/// 接続先インスタンス名
		/// </summary>
		private string		instancename;
		/// <summary>
		/// サーバー＋インスタンスを利用した接続先サーバーのHashtableのキー情報
		/// </summary>
		private string		keyname;

		/// <summary>
		/// 最後に利用したDB名
		/// </summary>
		private string		pLastDatabase;		
		/// <summary>
		/// 最後に利用したDB名
		/// </summary>
		public string		LastDatabase
		{
			get { return this.pLastDatabase; }
			set { this.pLastDatabase = value; }
		}

		/// <summary>
		/// // DB 毎の最終ユーザーを記録する
		/// 複数を登録させるため、内部はstring の要素を持ったArrayListとする
		/// </summary>
		private Hashtable		dbopt;	
		/// <summary>
		/// // DB 毎の最終ユーザーを記録する
		/// 複数を登録させるため、内部はstring の要素を持ったArrayListとする
		/// </summary>
		public Hashtable		Dbopt
		{
			get { return this.dbopt; }
			//set { this.dbopt = value; }
		}

		/// <summary>
		/// システムユーザーを表示するか否か
		/// </summary>
		private int	isShowsysuser;	
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
		private int	sortKey;
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
		private int	showView;
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
		private Hashtable	outdest;
		/// <summary>
		/// データ出力先の指定
		/// </summary>
		public	Hashtable	OutDest
		{
			get { return this.outdest; }
			//set { this.outdest = value; }
		}
		/// <summary>
		/// データ出力先のファイル・フォルダ名
		/// </summary>
		private Hashtable	outfile;
		/// <summary>
		/// データ出力先のファイル・フォルダ名
		/// </summary>
		public	Hashtable	OutFile
		{
			get { return this.outfile; }
			//set { this.outfile = value; }
		}
		/// <summary>
		/// データグリッドを表示するか否か
		/// </summary>
		private Hashtable	showgrid;
		/// <summary>
		/// データグリッドを表示するか否か
		/// </summary>
		public	Hashtable	ShowGrid
		{
			get { return this.showgrid; }
			//set { this.showgrid = value; }
		}

		/// <summary>
		/// グリッド表示件数
		/// </summary>
		private Hashtable	griddspcnt;
		/// <summary>
		/// グリッド表示件数
		/// </summary>
		public 	Hashtable	GridDispCnt
		{
			get { return this.griddspcnt; }
			//set { this.griddspcnt = value; }
		}
		
		/// <summary>
		/// テキスト出力時の文字コード
		/// </summary>
		private Hashtable	txtencode;
		/// <summary>
		/// テキスト出力時の文字コード
		/// </summary>
		public	Hashtable	TxtEncode
		{
			get { return this.txtencode; }
			//set { this.txtencode = value; }
		}
        /// <summary>
		/// サーバー別情報を記憶するか否か
		/// </summary>
		private bool	isSaveKey = true;
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
		private bool	isUseTrust = false;
		/// <summary>
		/// 信頼関係接続を利用するか否か
		/// </summary>
		public	bool	IsUseTrust
		{
			get { return this.isUseTrust; }
			set { this.isUseTrust = value; }
		}
		/// <summary>
		/// ログインユーザー名
		/// </summary>
		private string	pLogOnUser = "";
		/// <summary>
		/// ログインユーザー名
		/// </summary>
		public	string	LogOnUser
		{
			get { return this.pLogOnUser; }
			set { this.pLogOnUser = value; }
		}
		/// <summary>
		/// where 句の入力履歴情報
		/// </summary>
		private TextHistoryDataSet  whereHistory;
		/// <summary>
		/// where 句の入力履歴情報
		/// </summary>
		public TextHistoryDataSet  WhereHistory
		{
			get { return this.whereHistory; }
			set { this.whereHistory = value; }
		}

		/// <summary>
		/// order by 句の入力履歴情報
		/// </summary>
		private TextHistoryDataSet  sortHistory;
		/// <summary>
		/// order by 句の入力履歴情報
		/// </summary>
		public TextHistoryDataSet  SortHistory
		{
			get { return this.sortHistory; }
			set { this.sortHistory = value; }
		}

		/// <summary>
		/// order by 句の入力履歴情報
		/// </summary>
		private TextHistoryDataSet  aliasHistory;
		/// <summary>
		/// order by 句の入力履歴情報
		/// </summary>
		public TextHistoryDataSet  AliasHistory
		{
			get { return this.aliasHistory; }
			set { this.aliasHistory = value; }
		}

		/// <summary>
		/// select 実行履歴情報
		/// </summary>
		private TextHistoryDataSet  selectHistory;
		/// <summary>
		/// select 実行履歴情報
		/// </summary>
		public TextHistoryDataSet  SelectHistory
		{
			get { return this.selectHistory; }
			set { this.selectHistory = value; }
		}

		/// <summary>
		/// クエリ発行時の入力履歴
		/// </summary>
		private TextHistoryDataSet  dmlHistory;
		/// <summary>
		/// クエリ発行時の入力履歴
		/// </summary>
		public TextHistoryDataSet  DMLHistory
		{
			get { return this.dmlHistory; }
			set { this.dmlHistory = value; }
		}

		/// <summary>
		/// 各種コマンド入力履歴
		/// </summary>
		private TextHistoryDataSet  cmdHistory;
		/// <summary>
		/// 各種コマンド入力履歴
		/// </summary>
		public TextHistoryDataSet  CmdHistory
		{
			get { return this.cmdHistory; }
			set { this.cmdHistory = value; }
		}

		/// <summary>
		/// 検索入力履歴
		/// </summary>
		private TextHistoryDataSet  pSearchHistory;
		/// <summary>
		/// 検索入力履歴
		/// </summary>
		public TextHistoryDataSet  SearchHistory
		{
			get { return this.pSearchHistory; }
			set { this.pSearchHistory = value; }
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
			whereHistory = new TextHistoryDataSet();
			sortHistory = new TextHistoryDataSet();
			aliasHistory = new TextHistoryDataSet();
			selectHistory = new TextHistoryDataSet();
			DMLHistory = new TextHistoryDataSet();
			cmdHistory = new TextHistoryDataSet();
			pSearchHistory = new TextHistoryDataSet();
		}

		/// <summary>
		/// 内部リソースを破棄する
		/// </summary>
		/// <param name="disposing">破棄するか否かのフラグ</param>
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				// dispose managed resources
				whereHistory.Dispose();
				sortHistory.Dispose();
				aliasHistory.Dispose();
				selectHistory.Dispose();
				DMLHistory.Dispose();
				cmdHistory.Dispose();
				pSearchHistory.Dispose();
			}
			// free native resources
		}

		/// <summary>
		/// 内部リソースを破棄する
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}


		/// <summary>
		/// シリアライズ処理用コンストラクタ
		/// </summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		private ServerData(SerializationInfo info, StreamingContext context)
		{
			servername = "";
			instancename = "";
			keyname = "";
			pLastDatabase = "";
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
			whereHistory = new TextHistoryDataSet();
			sortHistory = new TextHistoryDataSet();
			aliasHistory = new TextHistoryDataSet();
			selectHistory = new TextHistoryDataSet();
			DMLHistory = new TextHistoryDataSet();
			cmdHistory = new TextHistoryDataSet();
			pSearchHistory = new TextHistoryDataSet();

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
				this.pLastDatabase = info.GetString("pLastDatabase");
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
				this.pLogOnUser = info.GetString("pLogOnUser");
			}
			catch{}

			try
			{
				this.whereHistory = (TextHistoryDataSet)info.GetValue("whereHistory",typeof(TextHistoryDataSet));
			}
			catch{}
			try
			{
				this.sortHistory = (TextHistoryDataSet)info.GetValue("sortHistory",typeof(TextHistoryDataSet));
			}
			catch{}
			try
			{
				this.aliasHistory = (TextHistoryDataSet)info.GetValue("aliasHistory",typeof(TextHistoryDataSet));
			}
			catch{}
			try
			{
				this.selectHistory = (TextHistoryDataSet)info.GetValue("selectHistory",typeof(TextHistoryDataSet));
			}
			catch{}
			try
			{
				this.DMLHistory = (TextHistoryDataSet)info.GetValue("DMLHistory",typeof(TextHistoryDataSet));
			}
			catch{}
			try
			{
				this.cmdHistory = (TextHistoryDataSet)info.GetValue("cmdHistory",typeof(TextHistoryDataSet));
			}
			catch{}
			try
			{
				this.pSearchHistory = (TextHistoryDataSet)info.GetValue("SearchHistory",typeof(TextHistoryDataSet));
			}
			catch{}

			if( whereHistory == null ) whereHistory = new TextHistoryDataSet();
			if( sortHistory == null ) sortHistory = new TextHistoryDataSet();
			if( aliasHistory == null ) aliasHistory = new TextHistoryDataSet();
			if( selectHistory == null ) selectHistory = new TextHistoryDataSet();
			if( DMLHistory == null ) DMLHistory = new TextHistoryDataSet();
			if( cmdHistory == null ) cmdHistory = new TextHistoryDataSet();
			if( pSearchHistory == null ) pSearchHistory = new TextHistoryDataSet();

		}

		/// <summary>
		/// シリアライズ処理用
		/// </summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		public void GetObjectData(
			SerializationInfo info, StreamingContext context)
		{
			info.AddValue("servername",	servername );
			info.AddValue("instancename", instancename );
			info.AddValue("keyname", keyname );
			info.AddValue("pLastDatabase", pLastDatabase );
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
			info.AddValue("pLogOnUser", pLogOnUser );
			info.AddValue("whereHistory", whereHistory );
			info.AddValue("sortHistory", sortHistory );
			info.AddValue("aliasHistory", aliasHistory );
			info.AddValue("selectHistory", selectHistory );
			info.AddValue("DMLHistory", DMLHistory );
			info.AddValue("cmdHistory", cmdHistory );
			info.AddValue("SearchHistory", pSearchHistory );
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
	/// ConditionRecorder の概要の説明です。
	/// </summary>
	[Serializable]
	public sealed class ConditionRecorder : ISerializable
	{
		/// <summary>
		/// サーバー別情報を管理するハッシュテーブル
		/// サーバー名＋インスタンス名をキーとする
		/// </summary>
		private Hashtable	perServerData;
		/// <summary>
		/// サーバー別情報を管理するハッシュテーブル
		/// サーバー名＋インスタンス名をキーとする
		/// </summary>
		public Hashtable	PerServerData
		{
			get { return this.perServerData; }
			//set { this.perServerData = value; }
		}

		/// <summary>
		/// 最後に接続したサーバーのHashkey
		/// </summary>
		private string lastserverkey = "";
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
		public ConditionRecorder()
		{
			this.perServerData = new Hashtable();
		}

		/// <summary>
		/// シリアライズ処理用コンストラクタ
		/// </summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		private ConditionRecorder(SerializationInfo info, StreamingContext context)
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
		public void GetObjectData(
			SerializationInfo info, StreamingContext context)
		{
			info.AddValue("LASTSERVERKEY", this.lastserverkey);
			info.AddValue("SERVERDATA", this.perServerData, typeof(Hashtable) );
		}

	}
}
