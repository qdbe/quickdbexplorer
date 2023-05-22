using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Data;
using System.IO;
using System.Configuration;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Drawing;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;

namespace quickDBExplorer
{
    /// <summary>
    /// サーバー別の各種指定履歴データを管理する
    /// </summary>
    [Serializable]
    public class ServerData : ISerializable, IDisposable
    {
        /// <summary>
        /// 接続先サーバー名
        /// </summary>
        protected string servername;
        /// <summary>
        /// 接続先インスタンス名
        /// </summary>
        protected string instancename;
        /// <summary>
        /// サーバー＋インスタンスを利用した接続先サーバーのHashtableのキー情報
        /// </summary>
        protected string keyname;

        /// <summary>
        /// 最後に利用したDB名
        /// </summary>
        protected string pLastDatabase;
        /// <summary>
        /// 最後に利用したDB名
        /// </summary>
        public string LastDatabase
        {
            get { return this.pLastDatabase; }
            set { this.pLastDatabase = value; }
        }

        /// <summary>
        /// // DB 毎の最終ユーザーを記録する
        /// 複数を登録させるため、内部はstring の要素を持ったArrayListとする
        /// </summary>
        protected Hashtable dbopt;
        /// <summary>
        /// // DB 毎の最終ユーザーを記録する
        /// 複数を登録させるため、内部はstring の要素を持ったArrayListとする
        /// </summary>
        public Hashtable Dbopt
        {
            get { return this.dbopt; }
            //set { this.dbopt = value; }
        }

        /// <summary>
        /// システムユーザーを表示するか否か
        /// </summary>
        protected int isShowsysuser;
        /// <summary>
        /// システムユーザーを表示するか否か
        /// </summary>
        public int IsShowsysuser
        {
            get { return this.isShowsysuser; }
            set { this.isShowsysuser = value; }
        }

        /// <summary>
        ///table/view リストのソート順 
        /// </summary>
        protected int sortKey;
        /// <summary>
        ///table/view リストのソート順 
        /// </summary>
        public int SortKey
        {
            get { return this.sortKey; }
            set { this.sortKey = value; }
        }

        /// <summary>
        /// view を表示させるかどうか
        /// </summary>
        protected int showView;
        /// <summary>
        /// view を表示させるかどうか
        /// </summary>
        public int ShowView
        {
            get { return this.showView; }
            set { this.showView = value; }
        }

        /// <summary>
        /// データ出力先の指定
        /// </summary>
        protected Hashtable outdest;
        /// <summary>
        /// データ出力先の指定
        /// </summary>
        public Hashtable OutDest
        {
            get { return this.outdest; }
            //set { this.outdest = value; }
        }
        /// <summary>
        /// データ出力先のファイル・フォルダ名
        /// </summary>
        protected Hashtable outfile;
        /// <summary>
        /// データ出力先のファイル・フォルダ名
        /// </summary>
        public Hashtable OutFile
        {
            get { return this.outfile; }
            //set { this.outfile = value; }
        }
        /// <summary>
        /// データグリッドを表示するか否か
        /// </summary>
        protected Hashtable showgrid;
        /// <summary>
        /// データグリッドを表示するか否か
        /// </summary>
        public Hashtable ShowGrid
        {
            get { return this.showgrid; }
            //set { this.showgrid = value; }
        }

        /// <summary>
        /// グリッド表示件数
        /// </summary>
        private Hashtable griddspcnt;
        /// <summary>
        /// グリッド表示件数
        /// </summary>
        public Hashtable GridDispCnt
        {
            get { return this.griddspcnt; }
            //set { this.griddspcnt = value; }
        }

        /// <summary>
        /// テキスト出力時の文字コード
        /// </summary>
        protected Hashtable txtencode;
        /// <summary>
        /// テキスト出力時の文字コード
        /// </summary>
        public Hashtable TxtEncode
        {
            get { return this.txtencode; }
            //set { this.txtencode = value; }
        }
        /// <summary>
		/// サーバー別情報を記憶するか否か
		/// </summary>
		protected bool isSaveKey = true;
        /// <summary>
        /// サーバー別情報を記憶するか否か
        /// </summary>
        public bool IsSaveKey
        {
            get { return this.isSaveKey; }
            set { this.isSaveKey = value; }
        }
        /// <summary>
        /// 信頼関係接続を利用するか否か
        /// </summary>
        protected bool isUseTrust = false;
        /// <summary>
        /// 信頼関係接続を利用するか否か
        /// </summary>
        public bool IsUseTrust
        {
            get { return this.isUseTrust; }
            set { this.isUseTrust = value; }
        }
        /// <summary>
        /// ログインユーザー名
        /// </summary>
        protected string pLogOnUser = "";
        /// <summary>
        /// ログインユーザー名
        /// </summary>
        public string LogOnUser
        {
            get { return this.pLogOnUser; }
            set { this.pLogOnUser = value; }
        }

        /// <summary>
        /// 空白をNULLとして読み込むか否か
        /// </summary>
        public bool ReadEmptyAsNull
        {
            get;
            set;
        }

        /// <summary>
        /// グリッド表示時の幅の初期値
        /// </summary>
        public bool GridDefaltWidth { get; set; }



        /// <summary>
        /// 各種入力履歴
        /// </summary>
        public Dictionary<string, TextHistoryDataSet> InputHistories { get; set; }


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
            ReadEmptyAsNull = true;
            GridDefaltWidth = true;
            outdest = new Hashtable();
            outfile = new Hashtable();
            showgrid = new Hashtable();
            griddspcnt = new Hashtable();
            txtencode = new Hashtable();
            InputHistories = new Dictionary<string, TextHistoryDataSet>();
            //whereHistory = new TextHistoryDataSet();
            //sortHistory = new TextHistoryDataSet();
            //aliasHistory = new TextHistoryDataSet();
            //selectHistory = new TextHistoryDataSet();
            //DMLHistory = new TextHistoryDataSet();
            //cmdHistory = new TextHistoryDataSet();
            //pSearchHistory = new TextHistoryDataSet();
        }

        /// <summary>
        /// 内部リソースを破棄する
        /// </summary>
        /// <param name="disposing">破棄するか否かのフラグ</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                InputHistories.Clear();
            }
            // free native resources
        }

        /// <summary>
        /// 内部リソースを破棄する
        /// </summary>
        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        /// <summary>
        /// シリアライズ処理用コンストラクタ
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected ServerData(SerializationInfo info, StreamingContext context)
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
            ReadEmptyAsNull = true;
            GridDefaltWidth = true;
            outdest = new Hashtable();
            outfile = new Hashtable();
            showgrid = new Hashtable();
            griddspcnt = new Hashtable();
            txtencode = new Hashtable();
            InputHistories = new Dictionary<string, TextHistoryDataSet>();

            try
            {
                servername = info.GetString("servername");
            }
            catch { }
            try
            {
                this.instancename = info.GetString("instancename");
            }
            catch { }
            try
            {
                this.keyname = info.GetString("keyname");
            }
            catch { }
            try
            {
                this.pLastDatabase = info.GetString("pLastDatabase");
            }
            catch { }
            try
            {
                this.dbopt = (Hashtable)info.GetValue("dbopt", typeof(Hashtable));
            }
            catch { }

            try
            {
                this.isShowsysuser = info.GetInt32("isShowsysuser");
            }
            catch { }

            try
            {
                this.sortKey = info.GetInt32("sortKey");
            }
            catch { }

            try
            {
                this.showView = info.GetInt32("showView");
            }
            catch { }

            try
            {
                this.outdest = (Hashtable)info.GetValue("outdest", typeof(Hashtable));
            }
            catch { }

            try
            {
                this.outfile = (Hashtable)info.GetValue("outfile", typeof(Hashtable));
            }
            catch { }

            try
            {
                this.showgrid = (Hashtable)info.GetValue("showgrid", typeof(Hashtable));
            }
            catch { }

            try
            {
                this.griddspcnt = (Hashtable)info.GetValue("griddspcnt", typeof(Hashtable));
            }
            catch { }

            try
            {
                this.txtencode = (Hashtable)info.GetValue("txtencode", typeof(Hashtable));
            }
            catch { }

            try
            {
                this.isSaveKey = info.GetBoolean("isSaveKey");
            }
            catch { }


            try
            {
                this.IsUseTrust = info.GetBoolean("IsUseTrust");
            }
            catch { }

            try
            {
                this.pLogOnUser = info.GetString("pLogOnUser");
            }
            catch { }

            try
            {
                InputHistories.Add("txtWhere", (TextHistoryDataSet)info.GetValue("whereHistory", typeof(TextHistoryDataSet)));
            }
            catch { }
            try
            {
                InputHistories.Add("txtSort", (TextHistoryDataSet)info.GetValue("sortHistory", typeof(TextHistoryDataSet)));
            }
            catch { }
            try
            {
                InputHistories.Add("txtAlias", (TextHistoryDataSet)info.GetValue("aliasHistory", typeof(TextHistoryDataSet)));
            }
            catch { }
            try
            {
                InputHistories.Add("selectHistory", (TextHistoryDataSet)info.GetValue("selectHistory", typeof(TextHistoryDataSet)));
            }
            catch { }
            try
            {
                InputHistories.Add("DMLHistory", (TextHistoryDataSet)info.GetValue("DMLHistory", typeof(TextHistoryDataSet)));
            }
            catch { }
            try
            {
                InputHistories.Add("cmdHistory", (TextHistoryDataSet)info.GetValue("cmdHistory", typeof(TextHistoryDataSet)));
            }
            catch { }
            try
            {
                InputHistories.Add("SearchHistory", (TextHistoryDataSet)info.GetValue("SearchHistory", typeof(TextHistoryDataSet)));
            }
            catch { }

            try
            {
                ArrayList keys = (ArrayList)info.GetValue("InputHistoryKey", typeof(ArrayList));
                ArrayList values = (ArrayList)info.GetValue("InputHistoryValue", typeof(ArrayList));
                for (int i = 0; i < keys.Count; i++)
                {
                    InputHistories.Add((string)keys[i], (TextHistoryDataSet)values[i]);
                }
            }
            catch { }
        }

        /// <summary>
        /// シリアライズ処理用
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public void GetObjectData(
            SerializationInfo info, StreamingContext context)
        {
            info.AddValue("servername", servername);
            info.AddValue("instancename", instancename);
            info.AddValue("keyname", keyname);
            info.AddValue("pLastDatabase", pLastDatabase);
            info.AddValue("dbopt", dbopt);
            info.AddValue("isShowsysuser", isShowsysuser);
            info.AddValue("sortKey", sortKey);
            info.AddValue("showView", showView);
            info.AddValue("IsUseTrust", IsUseTrust);
            info.AddValue("outdest", outdest);
            info.AddValue("outfile", outfile);
            info.AddValue("showgrid", showgrid);
            info.AddValue("griddspcnt", griddspcnt);
            info.AddValue("txtencode", txtencode);
            info.AddValue("isSaveKey", isSaveKey);
            info.AddValue("pLogOnUser", pLogOnUser);
            ArrayList keys = new ArrayList();
            ArrayList values = new ArrayList();
            foreach (string each in InputHistories.Keys)
            {
                keys.Add(each);
                values.Add(InputHistories[each]);
            }
            info.AddValue("InputHistoryKey", keys);
            info.AddValue("InputHistoryValue", values);
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
    /// サーバー別の各種指定履歴データを管理する
    /// </summary>
    public class ServerJsonData : IDisposable
    {
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
        /// <summary>
        /// 接続先サーバー名
        /// </summary>
        protected string servername;
        /// <summary>
        /// 接続先インスタンス名
        /// </summary>
        protected string instancename;
        /// <summary>
        /// サーバー＋インスタンスを利用した接続先サーバーのDictionaryのキー情報
        /// </summary>
        protected string keyname;

        /// <summary>
        /// 最後に利用したDB名
        /// </summary>
        protected string pLastDatabase;
        /// <summary>
        /// 最後に利用したDB名
        /// </summary>
        public string LastDatabase
        {
            get { return this.pLastDatabase; }
            set { this.pLastDatabase = value; }
        }

        /// <summary>
        /// // DB 毎の最終ユーザーを記録する
        /// 複数を登録させるため、内部はstring の要素を持ったListとする
        /// </summary>
        public Dictionary<string, List<string>> Dbopt
        {
            get;
            protected set;
        }

        /// <summary>
        /// システムユーザーを表示するか否か
        /// </summary>
        protected int isShowsysuser;
        /// <summary>
        /// システムユーザーを表示するか否か
        /// </summary>
        public int IsShowsysuser
        {
            get { return this.isShowsysuser; }
            set { this.isShowsysuser = value; }
        }

        /// <summary>
        ///table/view リストのソート順 
        /// </summary>
        protected int sortKey;
        /// <summary>
        ///table/view リストのソート順 
        /// </summary>
        public int SortKey
        {
            get { return this.sortKey; }
            set { this.sortKey = value; }
        }

        /// <summary>
        /// view を表示させるかどうか
        /// </summary>
        protected int showView;
        /// <summary>
        /// view を表示させるかどうか
        /// </summary>
        public int ShowView
        {
            get { return this.showView; }
            set { this.showView = value; }
        }

        /// <summary>
        /// データ出力先の指定
        /// </summary>
        public Dictionary<string, int> OutDest
        {
            get;
            protected set;
        }
        /// <summary>
        /// データ出力先のファイル・フォルダ名
        /// </summary>
        public Dictionary<string, string> OutFile
        {
            get;
            protected set;
        }

        /// <summary>
        /// データグリッドを表示するか否か
        /// </summary>
        public Dictionary<string, int> ShowGrid
        {
            get;
            protected set;
        }

        /// <summary>
        /// グリッド表示件数
        /// </summary>
        public Dictionary<string, string> GridDispCnt
        {
            get;
            protected set;
        }

        /// <summary>
        /// テキスト出力時の文字コード
        /// </summary>
        public Dictionary<string, int> TxtEncode
        {
            get;
            protected set;
        }
        /// <summary>
		/// サーバー別情報を記憶するか否か
		/// </summary>
		protected bool isSaveKey = true;
        /// <summary>
        /// サーバー別情報を記憶するか否か
        /// </summary>
        public bool IsSaveKey
        {
            get { return this.isSaveKey; }
            set { this.isSaveKey = value; }
        }
        /// <summary>
        /// 信頼関係接続を利用するか否か
        /// </summary>
        protected bool isUseTrust = false;
        /// <summary>
        /// 信頼関係接続を利用するか否か
        /// </summary>
        public bool IsUseTrust
        {
            get { return this.isUseTrust; }
            set { this.isUseTrust = value; }
        }
        /// <summary>
        /// ログインユーザー名
        /// </summary>
        protected string pLogOnUser = "";
        /// <summary>
        /// ログインユーザー名
        /// </summary>
        public string LogOnUser
        {
            get { return this.pLogOnUser; }
            set { this.pLogOnUser = value; }
        }

        /// <summary>
        /// 空白をNULLとして読み込むか否か
        /// </summary>
        public bool ReadEmptyAsNull { get; set; }


        /// <summary>
        /// グリッド表示時の幅の初期値
        /// </summary>
        public bool GridDefaltWidth { get; set; }

        /// <summary>
        /// 各種入力履歴
        /// </summary>
        public Dictionary<string, TextHistoryDataSet> InputHistories { get; set; }

        /// <summary>
        /// Whereダイアログ テーブル毎履歴
        /// database, object wherestring 
        /// </summary>
        public Dictionary<string, Dictionary<string, Dictionary<string, DataSet>>> WhereGridHistory { get; set; }


        /// <summary>
        /// オブジェクト検索履歴
        /// database, search Text, object
        /// </summary>
        public Dictionary<string, Dictionary<string, ObjectSearchCondition>> ObjectSearchHistory { get; set; }


        /// <summary>
        /// グリッド表示書式
        /// </summary>
        protected GridFormatSetting gridSetting;

        /// <summary>
        /// グリッド表示書式
        /// </summary>
        public GridFormatSetting GridSetting
        {
            get { return this.gridSetting; }
            set { this.gridSetting = value; }
        }

        /// <summary>
        /// Gridのカラム幅の記憶
        /// </summary>
        public Dictionary<string, Dictionary<string, int>> PerTableColumnWidth { get; set; }

        /// <summary>
        /// Gridの見出しのサイズ
        /// </summary>
        public Dictionary<string, Dictionary<string, int>> PerTableHeaderSize { get; set; }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ServerJsonData()
        {
            this.IsShowsysuser = 0;
            this.SortKey = 0;
            this.ShowView = 0;
            this.IsUseTrust = false;
            this.Dbopt = new Dictionary<string, List<string>>();
            this.OutDest = new Dictionary<string, int>();
            this.OutFile = new Dictionary<string, string>();
            this.ShowGrid = new Dictionary<string, int>();
            this.GridDispCnt = new Dictionary<string, string>();
            this.TxtEncode = new Dictionary<string, int>();
            this.InputHistories = new Dictionary<string, TextHistoryDataSet>();
            this.PerTableColumnWidth = new Dictionary<string, Dictionary<string, int>>();
            this.PerTableHeaderSize = new Dictionary<string, Dictionary<string, int>>();
            this.GridSetting = GridFormatSetting.Defalt();
            this.WhereGridHistory = new Dictionary<string, Dictionary<string, Dictionary<string, DataSet>>>();
            this.ObjectSearchHistory = new Dictionary<string, Dictionary<string, ObjectSearchCondition>>();
        }


        /// <summary>
        /// 内部リソースを破棄する
        /// </summary>
        /// <param name="disposing">破棄するか否かのフラグ</param>
        protected void Dispose(bool disposing)
        {
        }

        /// <summary>
        /// 内部リソースを破棄する
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Hashtable からDictionary に変換する
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static Dictionary<K, V> HashtableToDictionary<K, V>(Hashtable table)
        {
            Dictionary<K, V> d = new Dictionary<K, V>();
            foreach (var key in table.Keys)
            {
                d.Add((K)key, (V)table[key]);
            }
            return d;
        }
        
        /// <summary>
        /// Hash をDictionary に変換する
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static Dictionary<K, List<string>> HashtableToDictionaryListString<K>(Hashtable table)
        {
            Dictionary<K, List<string>> d = new Dictionary<K, List<string>>();
            foreach (var key in table.Keys)
            {
                if (table[key] != null)
                {
                    ArrayList ar = (ArrayList)table[key];
                    List<string> vals = new List<string>();
                    foreach (string str in ar)
                    {
                        vals.Add(str);
                    }
                    d.Add((K)key, vals);
                }
                else
                {
                    d.Add((K)key, null);
                }
            }
            return d;
        }

        /// <summary>
        /// ServerDataからコンバートする
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ServerJsonData CreateFrom(ServerData data)
        {
            ServerJsonData result = new ServerJsonData();

            Type t = data.GetType();
            Type d = typeof(ServerJsonData);
            PropertyInfo[] props = t.GetProperties();
            foreach (PropertyInfo each in props)
            {
                object o = each.GetValue(data, null);
                PropertyInfo dp = d.GetProperty(each.Name);
                if (each.PropertyType != typeof(Hashtable) && dp.CanWrite)
                {
                    dp.SetValue(result, o, null);
                }
            }
            // HashTable のコンバートを実施する
            result.Dbopt = HashtableToDictionaryListString<string>(data.Dbopt);
            result.OutDest = HashtableToDictionary<string, int>(data.OutDest);
            result.OutFile = HashtableToDictionary<string, string>(data.OutFile);
            result.ShowGrid = HashtableToDictionary<string, int>(data.ShowGrid);
            result.GridDispCnt = HashtableToDictionary<string, string>(data.GridDispCnt);
            result.TxtEncode = HashtableToDictionary<string, int>(data.TxtEncode);
            //result.InputHistories = HashtableToDictionary<string, TextHistoryDataSet>(data.InputHistories);
            //result.GridSetting = GridFormatSetting.Defalt();

            return result;
        }

        /// <summary>
        /// Hash値を変換する
        /// </summary>
        public void ConvertHashValues()
        {
            Type d = typeof(ServerJsonData);
            PropertyInfo[] props = d.GetProperties();
            foreach (PropertyInfo eachprop in props)
            {
                if (eachprop.PropertyType == typeof(Hashtable))
                {
                    Hashtable ht = (Hashtable)eachprop.GetValue(this, null);
                    ConvertHashValuesSub(ht, typeof(int));
                }
            }

        }

        /// <summary>
        /// Hash値を変換する
        /// </summary>
        /// <param name="ht"></param>
        /// <param name="t"></param>
        public void ConvertHashValuesSub(Hashtable ht, Type t)
        {
            List<object> keys = new List<object>();
            foreach (object eachkey in ht.Keys)
            {
                keys.Add(eachkey);
            }

            foreach (object dkey in keys)
            {
                JObject svobj = (JObject)ht[dkey];
                string objstr = svobj.ToString();

                object eachData = JsonConvert.DeserializeObject(objstr, t);
                ht[dkey] = eachData;
            }
        }

        /// <summary>
        /// NULLの場合に既定値を設定する
        /// </summary>
        public void SetDefaultValueIfNeed()
        {
            if (this.PerTableColumnWidth == null)
            {
                this.PerTableColumnWidth = new Dictionary<string, Dictionary<string, int>>();
            }
            if (this.PerTableHeaderSize == null)
            {
                this.PerTableHeaderSize = new Dictionary<string, Dictionary<string, int>>();
            }
            if (this.GridSetting == null)
            {
                this.GridSetting = GridFormatSetting.Defalt();
            }
            if (this.WhereGridHistory == null)
            {
                this.WhereGridHistory = new Dictionary<string, Dictionary<string, Dictionary<string, DataSet>>>();
            }
            if (this.ObjectSearchHistory == null)
            {
                this.ObjectSearchHistory = new Dictionary<string, Dictionary<string, ObjectSearchCondition>>();
            }

        }
    }

    /// <summary>
    /// グリッド表示書式
    /// </summary>
    public class GridFormatSetting
    {
        /// <summary>
        /// 表示フォントの指定
        /// </summary>
        private Font gridFont;
        /// <summary>
        /// 表示フォントの指定
        /// </summary>
        public Font GridFont
        {
            get { return this.gridFont; }
            set { this.gridFont = value; }
        }
        /// <summary>
        /// フォント表示色の指定
        /// </summary>
        private Color gridForeColor;
        /// <summary>
        /// フォント表示色の指定
        /// </summary>
        public Color GridForeColor
        {
            get { return this.gridForeColor; }
            set { this.gridForeColor = value; }
        }
        /// <summary>
        /// 数値変換書式の指定
        /// </summary>
        private string gridNumberFormat;
        /// <summary>
        /// 数値変換書式の指定
        /// </summary>
        public string GridNumberFormat
        {
            get { return this.gridNumberFormat; }
            set { this.gridNumberFormat = value; }

        }


        /// <summary>
        /// 数値変換書式
        /// </summary>
        public string GridNumberFormatPlain
        {
            get { return this.GetFormat(this.GridNumberFormat); }
        }
    

        /// <summary>
        /// 小数点書式の指定
        /// </summary>
        private string gridFloatFormat;
        /// <summary>
        /// 小数点書式の指定
        /// </summary>
        public string GridFloatFormat
        {
            get { return this.gridFloatFormat; }
            set { this.gridFloatFormat = value; }
        }


        /// <summary>
        /// 小数点書式
        /// </summary>
        public string GridFloatFormatPlain
        {
            get { return this.GetFormat(this.GridFloatFormat); }
        }

        /// <summary>
        /// 日付変換書式の指定
        /// </summary>
        private string gridDateFormat;
        /// <summary>
        /// 日付変換書式の指定
        /// </summary>
        public string GridDateFormat
        {
            get { return this.gridDateFormat; }
            set { this.gridDateFormat = value; }
        }
        /// <summary>
        /// 日付変換書式
        /// </summary>
        public string GridDateFormatPlain
        {
            get { return this.GetFormat(this.GridDateFormat); }
        }

        /// <summary>
        /// 既定値の設定を作成する
        /// </summary>
        /// <returns></returns>
        public static GridFormatSetting Defalt()
        {
            GridFormatSetting defaultValue = new GridFormatSetting();
            defaultValue.GridFont = new Font("ＭＳ ゴシック", 8.25f);

            defaultValue.GridDateFormat = "yyyy/MM/dd\t\t2006/01/01";
            defaultValue.GridFloatFormat = "g\t標準";
            defaultValue.GridNumberFormat = "D\t\tカンマなし";
            defaultValue.GridForeColor = Color.Black;

            return defaultValue;
        }

        /// <summary>
        /// 書式から説明を除外する
        /// </summary>
        /// <param name="fstr"></param>
        /// <returns></returns>
        protected string GetFormat(string fstr)
        {
            if (fstr == null)
            {
                return "";
            }
            int termp = fstr.IndexOf("	");
            if (termp == -1)
            {
                return fstr;
            }
            return fstr.Substring(0, termp);
        }

    }

    /// <summary>
    /// ConditionRecorder の概要の説明です。
    /// </summary>
    [Serializable]
    public class ConditionRecorder : ISerializable
    {
        /// <summary>
        /// サーバー別情報を管理するハッシュテーブル
        /// サーバー名＋インスタンス名をキーとする
        /// </summary>
        private Hashtable perServerData;
        /// <summary>
        /// サーバー別情報を管理するハッシュテーブル
        /// サーバー名＋インスタンス名をキーとする
        /// </summary>
        public Hashtable PerServerData
        {
            get { return this.perServerData; }
            set { this.perServerData = value; }
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
                this.perServerData = (Hashtable)info.GetValue("SERVERDATA", typeof(Hashtable));
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
            info.AddValue("SERVERDATA", this.perServerData, typeof(Hashtable));
        }

        /// <summary>
        /// 作成する
        /// </summary>
        /// <returns></returns>
        public static ConditionRecorder Create()
        {
            ConditionRecorder _default = null;
            _default = ReadXmlConfig();
            return _default;
        }


        /// <summary>
        /// XML形式を読みこむ
        /// </summary>
        /// <returns></returns>
        protected static ConditionRecorder ReadXmlConfig()
        {
            // 設定ファイルの読み込みストリーム
            FileStream fs = null;


            ConditionRecorder result = new ConditionRecorder();
            try
            {
                // 設定ファイルを読み込み
                string path = Application.StartupPath;
                string filename = path + "\\quickDBExplorer." + System.Environment.MachineName + ".xml";
                if (File.Exists(filename))
                {
                    fs = new FileStream(filename, FileMode.Open);
                }
                if (fs == null)
                {
                    fs = new FileStream(path + "\\quickDBExplorer.xml", FileMode.Open);
                }
                // Soap Serialize しているので、それをDeSerialize
                SoapFormatter sf = new SoapFormatter();
                if (fs != null && fs.CanRead)
                {
                    result = (ConditionRecorder)sf.Deserialize(fs);
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                // 初回インストール時はファイルがない可能性がある
                ;
            }
            catch (Exception exp)
            {
                throw;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }

            return result;
        }

        /// <summary>
        /// Json形式で保存する
        /// </summary>
        protected void SaveJson()
        {
            StreamWriter sw = null;

            string machinename = System.Environment.MachineName;

            try
            {
                string path = Application.StartupPath;
                string filename = path + "\\quickDBExplorer." + System.Environment.MachineName + ".json";
                sw = new StreamWriter(filename, false);
                if (sw != null)
                {
                    ArrayList ar = new ArrayList();
                    foreach (object keys in this.PerServerData.Keys)
                    {
                        if (((ServerJsonData)(this.PerServerData[keys])).IsSaveKey == false)
                        {
                            ar.Add((string)keys);
                        }
                    }
                    foreach (string kk in ar)
                    {
                        this.PerServerData.Remove(kk);
                    }
                    if (this.PerServerData.Count == 0)
                    {
                        ServerJsonData sv = new ServerJsonData();
                        sv.Servername = "(local)";
                        sv.InstanceName = "";
                        sv.IsUseTrust = false;
                        this.PerServerData.Add(sv.KeyName, sv);
                    }
                    string str = JsonConvert.SerializeObject(this);
                    sw.Write(str);
                }
            }
            catch (Exception exp)
            {
                throw;
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
        }

        /// <summary>
        /// XML形式で保存する
        /// </summary>
        protected void SaveXml()
        {
            FileStream fs = null;

            string machinename = System.Environment.MachineName;

            try
            {
                string path = Application.StartupPath;
                fs = new FileStream(path + "\\quickDBExplorer." + machinename + ".xml", FileMode.OpenOrCreate);
                fs.Close();
                fs = null;
                fs = new FileStream(path + "\\quickDBExplorer." + machinename + ".xml", FileMode.Truncate, FileAccess.Write);
                SoapFormatter sf = new SoapFormatter();
                if (fs != null && fs.CanWrite)
                {
                    ArrayList ar = new ArrayList();
                    foreach (object keys in this.PerServerData.Keys)
                    {
                        if (((ServerData)(this.PerServerData[keys])).IsSaveKey == false)
                        {
                            ar.Add((string)keys);
                        }
                    }
                    foreach (string kk in ar)
                    {
                        this.PerServerData.Remove(kk);
                    }
                    if (this.PerServerData.Count == 0)
                    {
                        ServerData sv = new ServerData();
                        sv.Servername = "(local)";
                        sv.InstanceName = "";
                        sv.IsUseTrust = false;
                        this.PerServerData.Add(sv.KeyName, sv);
                    }
                    sf.Serialize(fs, (object)this);
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                ;
            }
            catch (Exception exp)
            {
                throw;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }
    }

    /// <summary>
    /// ConditionRecorder の概要の説明です。
    /// </summary>
    [Serializable]
    public class ConditionRecorderJson 
    {
        /// <summary>
        /// サーバー別情報を管理するハッシュテーブル
        /// サーバー名＋インスタンス名をキーとする
        /// </summary>
        public Dictionary<string, ServerJsonData> PerServerData
        {
            get;
            protected set;
        }

        /// <summary>
        /// 最後に接続したサーバーのHashkey
        /// </summary>
        public string LastServerKey
        {
            get;
            set;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ConditionRecorderJson()
        {
            this.LastServerKey = "";
            this.PerServerData = new Dictionary<string, ServerJsonData>();
        }

        /// <summary>
        /// 型変換
        /// </summary>
        /// <param name="srcdata"></param>
        public void ConvertFrom(ConditionRecorder srcdata)
        {
            this.LastServerKey = srcdata.LastServerKey;
            List<string> keys = new List<string>();
            foreach (string eachKey in srcdata.PerServerData.Keys)
            {
                keys.Add(eachKey);
            }

            foreach (string eachKey in keys)
            {
                Object sdata = srcdata.PerServerData[eachKey];

                if (sdata is ServerData)
                {
                    ServerJsonData newconfig = ServerJsonData.CreateFrom((ServerData)sdata);
                    this.PerServerData[eachKey] = newconfig;
                }
            }
        }

        /// <summary>
        /// 作成する
        /// </summary>
        /// <returns></returns>
        public static ConditionRecorderJson Create()
        {
            ConditionRecorderJson _default = null;
            _default = ReadJsonConfig();
            if (_default == null)
            {
                ConditionRecorder xmlsrc = ConditionRecorder.Create();
                _default = new ConditionRecorderJson();
                _default.ConvertFrom(xmlsrc);
            }
            return _default;
        }

        /// <summary>
        /// Json形式の設定を読み込む
        /// </summary>
        /// <returns></returns>
        protected static ConditionRecorderJson ReadJsonConfig()
        {
            // 設定ファイルの読み込みストリーム
            StreamReader sr = null;


            ConditionRecorderJson result = null;
            try
            {
                // 設定ファイルを読み込み
                string path = Application.StartupPath;
                string filename = path + "\\quickDBExplorer." + System.Environment.MachineName + ".json";
                if (File.Exists(filename))
                {
                    sr = new StreamReader(filename, true);

                    string str = sr.ReadToEnd();
                    if (str.Length != 0)
                    {
                        result = JsonConvert.DeserializeObject<ConditionRecorderJson>(str);
                        foreach(string each in result.PerServerData.Keys)
                        {
                            ServerJsonData s = result.PerServerData[each];
                            s.SetDefaultValueIfNeed();
                        }
                    }
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                // 初回インストール時はファイルがない可能性がある
                return null;
            }
            catch (Exception exp)
            {
                throw;
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                }
            }

            return result;
        }


        /// <summary>
        /// 保存する
        /// </summary>
        public void Save()
        {
            SaveJson();
        }

        /// <summary>
        /// Json形式で保存する
        /// </summary>
        protected void SaveJson()
        {
            StreamWriter sw = null;

            string machinename = System.Environment.MachineName;

            try
            {
                string path = Application.StartupPath;
                string filename = path + "\\quickDBExplorer." + System.Environment.MachineName + ".json";
                sw = new StreamWriter(filename, false);
                if (sw != null)
                {
                    ArrayList ar = new ArrayList();
                    foreach (string keys in this.PerServerData.Keys)
                    {
                        if (((ServerJsonData)(this.PerServerData[keys])).IsSaveKey == false)
                        {
                            ar.Add((string)keys);
                        }
                    }
                    foreach (string kk in ar)
                    {
                        this.PerServerData.Remove(kk);
                    }
                    if (this.PerServerData.Count == 0)
                    {
                        ServerJsonData sv = new ServerJsonData();
                        sv.Servername = "(local)";
                        sv.InstanceName = "";
                        sv.IsUseTrust = false;
                        this.PerServerData.Add(sv.KeyName, sv);
                    }
                    string str = JsonConvert.SerializeObject(this);
                    sw.Write(str);
                }
            }
            catch (Exception exp)
            {
                throw;
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
        }
    }
}
