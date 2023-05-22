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
    /// �T�[�o�[�ʂ̊e��w�藚���f�[�^���Ǘ�����
    /// </summary>
    [Serializable]
    public class ServerData : ISerializable, IDisposable
    {
        /// <summary>
        /// �ڑ���T�[�o�[��
        /// </summary>
        protected string servername;
        /// <summary>
        /// �ڑ���C���X�^���X��
        /// </summary>
        protected string instancename;
        /// <summary>
        /// �T�[�o�[�{�C���X�^���X�𗘗p�����ڑ���T�[�o�[��Hashtable�̃L�[���
        /// </summary>
        protected string keyname;

        /// <summary>
        /// �Ō�ɗ��p����DB��
        /// </summary>
        protected string pLastDatabase;
        /// <summary>
        /// �Ō�ɗ��p����DB��
        /// </summary>
        public string LastDatabase
        {
            get { return this.pLastDatabase; }
            set { this.pLastDatabase = value; }
        }

        /// <summary>
        /// // DB ���̍ŏI���[�U�[���L�^����
        /// ������o�^�����邽�߁A������string �̗v�f��������ArrayList�Ƃ���
        /// </summary>
        protected Hashtable dbopt;
        /// <summary>
        /// // DB ���̍ŏI���[�U�[���L�^����
        /// ������o�^�����邽�߁A������string �̗v�f��������ArrayList�Ƃ���
        /// </summary>
        public Hashtable Dbopt
        {
            get { return this.dbopt; }
            //set { this.dbopt = value; }
        }

        /// <summary>
        /// �V�X�e�����[�U�[��\�����邩�ۂ�
        /// </summary>
        protected int isShowsysuser;
        /// <summary>
        /// �V�X�e�����[�U�[��\�����邩�ۂ�
        /// </summary>
        public int IsShowsysuser
        {
            get { return this.isShowsysuser; }
            set { this.isShowsysuser = value; }
        }

        /// <summary>
        ///table/view ���X�g�̃\�[�g�� 
        /// </summary>
        protected int sortKey;
        /// <summary>
        ///table/view ���X�g�̃\�[�g�� 
        /// </summary>
        public int SortKey
        {
            get { return this.sortKey; }
            set { this.sortKey = value; }
        }

        /// <summary>
        /// view ��\�������邩�ǂ���
        /// </summary>
        protected int showView;
        /// <summary>
        /// view ��\�������邩�ǂ���
        /// </summary>
        public int ShowView
        {
            get { return this.showView; }
            set { this.showView = value; }
        }

        /// <summary>
        /// �f�[�^�o�͐�̎w��
        /// </summary>
        protected Hashtable outdest;
        /// <summary>
        /// �f�[�^�o�͐�̎w��
        /// </summary>
        public Hashtable OutDest
        {
            get { return this.outdest; }
            //set { this.outdest = value; }
        }
        /// <summary>
        /// �f�[�^�o�͐�̃t�@�C���E�t�H���_��
        /// </summary>
        protected Hashtable outfile;
        /// <summary>
        /// �f�[�^�o�͐�̃t�@�C���E�t�H���_��
        /// </summary>
        public Hashtable OutFile
        {
            get { return this.outfile; }
            //set { this.outfile = value; }
        }
        /// <summary>
        /// �f�[�^�O���b�h��\�����邩�ۂ�
        /// </summary>
        protected Hashtable showgrid;
        /// <summary>
        /// �f�[�^�O���b�h��\�����邩�ۂ�
        /// </summary>
        public Hashtable ShowGrid
        {
            get { return this.showgrid; }
            //set { this.showgrid = value; }
        }

        /// <summary>
        /// �O���b�h�\������
        /// </summary>
        private Hashtable griddspcnt;
        /// <summary>
        /// �O���b�h�\������
        /// </summary>
        public Hashtable GridDispCnt
        {
            get { return this.griddspcnt; }
            //set { this.griddspcnt = value; }
        }

        /// <summary>
        /// �e�L�X�g�o�͎��̕����R�[�h
        /// </summary>
        protected Hashtable txtencode;
        /// <summary>
        /// �e�L�X�g�o�͎��̕����R�[�h
        /// </summary>
        public Hashtable TxtEncode
        {
            get { return this.txtencode; }
            //set { this.txtencode = value; }
        }
        /// <summary>
		/// �T�[�o�[�ʏ����L�����邩�ۂ�
		/// </summary>
		protected bool isSaveKey = true;
        /// <summary>
        /// �T�[�o�[�ʏ����L�����邩�ۂ�
        /// </summary>
        public bool IsSaveKey
        {
            get { return this.isSaveKey; }
            set { this.isSaveKey = value; }
        }
        /// <summary>
        /// �M���֌W�ڑ��𗘗p���邩�ۂ�
        /// </summary>
        protected bool isUseTrust = false;
        /// <summary>
        /// �M���֌W�ڑ��𗘗p���邩�ۂ�
        /// </summary>
        public bool IsUseTrust
        {
            get { return this.isUseTrust; }
            set { this.isUseTrust = value; }
        }
        /// <summary>
        /// ���O�C�����[�U�[��
        /// </summary>
        protected string pLogOnUser = "";
        /// <summary>
        /// ���O�C�����[�U�[��
        /// </summary>
        public string LogOnUser
        {
            get { return this.pLogOnUser; }
            set { this.pLogOnUser = value; }
        }

        /// <summary>
        /// �󔒂�NULL�Ƃ��ēǂݍ��ނ��ۂ�
        /// </summary>
        public bool ReadEmptyAsNull
        {
            get;
            set;
        }

        /// <summary>
        /// �O���b�h�\�����̕��̏����l
        /// </summary>
        public bool GridDefaltWidth { get; set; }



        /// <summary>
        /// �e����͗���
        /// </summary>
        public Dictionary<string, TextHistoryDataSet> InputHistories { get; set; }


        /// <summary>
        /// �R���X�g���N�^
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
        /// �������\�[�X��j������
        /// </summary>
        /// <param name="disposing">�j�����邩�ۂ��̃t���O</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                InputHistories.Clear();
            }
            // free native resources
        }

        /// <summary>
        /// �������\�[�X��j������
        /// </summary>
        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        /// <summary>
        /// �V���A���C�Y�����p�R���X�g���N�^
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
        /// �V���A���C�Y�����p
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
        /// �ڑ���T�[�o�[��
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
        /// �ڑ���C���X�^���X��
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
        /// �T�[�o�[�{�C���X�^���X�𗘗p�����ڑ���T�[�o�[��Hashtable�̃L�[���
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
    /// �T�[�o�[�ʂ̊e��w�藚���f�[�^���Ǘ�����
    /// </summary>
    public class ServerJsonData : IDisposable
    {
        /// <summary>
        /// �ڑ���T�[�o�[��
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
        /// �ڑ���C���X�^���X��
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
        /// �T�[�o�[�{�C���X�^���X�𗘗p�����ڑ���T�[�o�[��Hashtable�̃L�[���
        /// </summary>
        public string KeyName
        {
            get
            {
                return this.keyname;
            }
        }
        /// <summary>
        /// �ڑ���T�[�o�[��
        /// </summary>
        protected string servername;
        /// <summary>
        /// �ڑ���C���X�^���X��
        /// </summary>
        protected string instancename;
        /// <summary>
        /// �T�[�o�[�{�C���X�^���X�𗘗p�����ڑ���T�[�o�[��Dictionary�̃L�[���
        /// </summary>
        protected string keyname;

        /// <summary>
        /// �Ō�ɗ��p����DB��
        /// </summary>
        protected string pLastDatabase;
        /// <summary>
        /// �Ō�ɗ��p����DB��
        /// </summary>
        public string LastDatabase
        {
            get { return this.pLastDatabase; }
            set { this.pLastDatabase = value; }
        }

        /// <summary>
        /// // DB ���̍ŏI���[�U�[���L�^����
        /// ������o�^�����邽�߁A������string �̗v�f��������List�Ƃ���
        /// </summary>
        public Dictionary<string, List<string>> Dbopt
        {
            get;
            protected set;
        }

        /// <summary>
        /// �V�X�e�����[�U�[��\�����邩�ۂ�
        /// </summary>
        protected int isShowsysuser;
        /// <summary>
        /// �V�X�e�����[�U�[��\�����邩�ۂ�
        /// </summary>
        public int IsShowsysuser
        {
            get { return this.isShowsysuser; }
            set { this.isShowsysuser = value; }
        }

        /// <summary>
        ///table/view ���X�g�̃\�[�g�� 
        /// </summary>
        protected int sortKey;
        /// <summary>
        ///table/view ���X�g�̃\�[�g�� 
        /// </summary>
        public int SortKey
        {
            get { return this.sortKey; }
            set { this.sortKey = value; }
        }

        /// <summary>
        /// view ��\�������邩�ǂ���
        /// </summary>
        protected int showView;
        /// <summary>
        /// view ��\�������邩�ǂ���
        /// </summary>
        public int ShowView
        {
            get { return this.showView; }
            set { this.showView = value; }
        }

        /// <summary>
        /// �f�[�^�o�͐�̎w��
        /// </summary>
        public Dictionary<string, int> OutDest
        {
            get;
            protected set;
        }
        /// <summary>
        /// �f�[�^�o�͐�̃t�@�C���E�t�H���_��
        /// </summary>
        public Dictionary<string, string> OutFile
        {
            get;
            protected set;
        }

        /// <summary>
        /// �f�[�^�O���b�h��\�����邩�ۂ�
        /// </summary>
        public Dictionary<string, int> ShowGrid
        {
            get;
            protected set;
        }

        /// <summary>
        /// �O���b�h�\������
        /// </summary>
        public Dictionary<string, string> GridDispCnt
        {
            get;
            protected set;
        }

        /// <summary>
        /// �e�L�X�g�o�͎��̕����R�[�h
        /// </summary>
        public Dictionary<string, int> TxtEncode
        {
            get;
            protected set;
        }
        /// <summary>
		/// �T�[�o�[�ʏ����L�����邩�ۂ�
		/// </summary>
		protected bool isSaveKey = true;
        /// <summary>
        /// �T�[�o�[�ʏ����L�����邩�ۂ�
        /// </summary>
        public bool IsSaveKey
        {
            get { return this.isSaveKey; }
            set { this.isSaveKey = value; }
        }
        /// <summary>
        /// �M���֌W�ڑ��𗘗p���邩�ۂ�
        /// </summary>
        protected bool isUseTrust = false;
        /// <summary>
        /// �M���֌W�ڑ��𗘗p���邩�ۂ�
        /// </summary>
        public bool IsUseTrust
        {
            get { return this.isUseTrust; }
            set { this.isUseTrust = value; }
        }
        /// <summary>
        /// ���O�C�����[�U�[��
        /// </summary>
        protected string pLogOnUser = "";
        /// <summary>
        /// ���O�C�����[�U�[��
        /// </summary>
        public string LogOnUser
        {
            get { return this.pLogOnUser; }
            set { this.pLogOnUser = value; }
        }

        /// <summary>
        /// �󔒂�NULL�Ƃ��ēǂݍ��ނ��ۂ�
        /// </summary>
        public bool ReadEmptyAsNull { get; set; }


        /// <summary>
        /// �O���b�h�\�����̕��̏����l
        /// </summary>
        public bool GridDefaltWidth { get; set; }

        /// <summary>
        /// �e����͗���
        /// </summary>
        public Dictionary<string, TextHistoryDataSet> InputHistories { get; set; }

        /// <summary>
        /// Where�_�C�A���O �e�[�u��������
        /// database, object wherestring 
        /// </summary>
        public Dictionary<string, Dictionary<string, Dictionary<string, DataSet>>> WhereGridHistory { get; set; }


        /// <summary>
        /// �I�u�W�F�N�g��������
        /// database, search Text, object
        /// </summary>
        public Dictionary<string, Dictionary<string, ObjectSearchCondition>> ObjectSearchHistory { get; set; }


        /// <summary>
        /// �O���b�h�\������
        /// </summary>
        protected GridFormatSetting gridSetting;

        /// <summary>
        /// �O���b�h�\������
        /// </summary>
        public GridFormatSetting GridSetting
        {
            get { return this.gridSetting; }
            set { this.gridSetting = value; }
        }

        /// <summary>
        /// Grid�̃J�������̋L��
        /// </summary>
        public Dictionary<string, Dictionary<string, int>> PerTableColumnWidth { get; set; }

        /// <summary>
        /// Grid�̌��o���̃T�C�Y
        /// </summary>
        public Dictionary<string, Dictionary<string, int>> PerTableHeaderSize { get; set; }


        /// <summary>
        /// �R���X�g���N�^
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
        /// �������\�[�X��j������
        /// </summary>
        /// <param name="disposing">�j�����邩�ۂ��̃t���O</param>
        protected void Dispose(bool disposing)
        {
        }

        /// <summary>
        /// �������\�[�X��j������
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Hashtable ����Dictionary �ɕϊ�����
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
        /// Hash ��Dictionary �ɕϊ�����
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
        /// ServerData����R���o�[�g����
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
            // HashTable �̃R���o�[�g�����{����
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
        /// Hash�l��ϊ�����
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
        /// Hash�l��ϊ�����
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
        /// NULL�̏ꍇ�Ɋ���l��ݒ肷��
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
    /// �O���b�h�\������
    /// </summary>
    public class GridFormatSetting
    {
        /// <summary>
        /// �\���t�H���g�̎w��
        /// </summary>
        private Font gridFont;
        /// <summary>
        /// �\���t�H���g�̎w��
        /// </summary>
        public Font GridFont
        {
            get { return this.gridFont; }
            set { this.gridFont = value; }
        }
        /// <summary>
        /// �t�H���g�\���F�̎w��
        /// </summary>
        private Color gridForeColor;
        /// <summary>
        /// �t�H���g�\���F�̎w��
        /// </summary>
        public Color GridForeColor
        {
            get { return this.gridForeColor; }
            set { this.gridForeColor = value; }
        }
        /// <summary>
        /// ���l�ϊ������̎w��
        /// </summary>
        private string gridNumberFormat;
        /// <summary>
        /// ���l�ϊ������̎w��
        /// </summary>
        public string GridNumberFormat
        {
            get { return this.gridNumberFormat; }
            set { this.gridNumberFormat = value; }

        }


        /// <summary>
        /// ���l�ϊ�����
        /// </summary>
        public string GridNumberFormatPlain
        {
            get { return this.GetFormat(this.GridNumberFormat); }
        }
    

        /// <summary>
        /// �����_�����̎w��
        /// </summary>
        private string gridFloatFormat;
        /// <summary>
        /// �����_�����̎w��
        /// </summary>
        public string GridFloatFormat
        {
            get { return this.gridFloatFormat; }
            set { this.gridFloatFormat = value; }
        }


        /// <summary>
        /// �����_����
        /// </summary>
        public string GridFloatFormatPlain
        {
            get { return this.GetFormat(this.GridFloatFormat); }
        }

        /// <summary>
        /// ���t�ϊ������̎w��
        /// </summary>
        private string gridDateFormat;
        /// <summary>
        /// ���t�ϊ������̎w��
        /// </summary>
        public string GridDateFormat
        {
            get { return this.gridDateFormat; }
            set { this.gridDateFormat = value; }
        }
        /// <summary>
        /// ���t�ϊ�����
        /// </summary>
        public string GridDateFormatPlain
        {
            get { return this.GetFormat(this.GridDateFormat); }
        }

        /// <summary>
        /// ����l�̐ݒ���쐬����
        /// </summary>
        /// <returns></returns>
        public static GridFormatSetting Defalt()
        {
            GridFormatSetting defaultValue = new GridFormatSetting();
            defaultValue.GridFont = new Font("�l�r �S�V�b�N", 8.25f);

            defaultValue.GridDateFormat = "yyyy/MM/dd\t\t2006/01/01";
            defaultValue.GridFloatFormat = "g\t�W��";
            defaultValue.GridNumberFormat = "D\t\t�J���}�Ȃ�";
            defaultValue.GridForeColor = Color.Black;

            return defaultValue;
        }

        /// <summary>
        /// ����������������O����
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
    /// ConditionRecorder �̊T�v�̐����ł��B
    /// </summary>
    [Serializable]
    public class ConditionRecorder : ISerializable
    {
        /// <summary>
        /// �T�[�o�[�ʏ����Ǘ�����n�b�V���e�[�u��
        /// �T�[�o�[���{�C���X�^���X�����L�[�Ƃ���
        /// </summary>
        private Hashtable perServerData;
        /// <summary>
        /// �T�[�o�[�ʏ����Ǘ�����n�b�V���e�[�u��
        /// �T�[�o�[���{�C���X�^���X�����L�[�Ƃ���
        /// </summary>
        public Hashtable PerServerData
        {
            get { return this.perServerData; }
            set { this.perServerData = value; }
        }

        /// <summary>
        /// �Ō�ɐڑ������T�[�o�[��Hashkey
        /// </summary>
        private string lastserverkey = "";
        /// <summary>
        /// �Ō�ɐڑ������T�[�o�[��Hashkey
        /// </summary>
        public string LastServerKey
        {
            get { return this.lastserverkey; }
            set { this.lastserverkey = value; }
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public ConditionRecorder()
        {
            this.perServerData = new Hashtable();
        }

        /// <summary>
        /// �V���A���C�Y�����p�R���X�g���N�^
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
        /// �V���A���C�Y�����p
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
        /// �쐬����
        /// </summary>
        /// <returns></returns>
        public static ConditionRecorder Create()
        {
            ConditionRecorder _default = null;
            _default = ReadXmlConfig();
            return _default;
        }


        /// <summary>
        /// XML�`����ǂ݂���
        /// </summary>
        /// <returns></returns>
        protected static ConditionRecorder ReadXmlConfig()
        {
            // �ݒ�t�@�C���̓ǂݍ��݃X�g���[��
            FileStream fs = null;


            ConditionRecorder result = new ConditionRecorder();
            try
            {
                // �ݒ�t�@�C����ǂݍ���
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
                // Soap Serialize ���Ă���̂ŁA�����DeSerialize
                SoapFormatter sf = new SoapFormatter();
                if (fs != null && fs.CanRead)
                {
                    result = (ConditionRecorder)sf.Deserialize(fs);
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                // ����C���X�g�[�����̓t�@�C�����Ȃ��\��������
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
        /// Json�`���ŕۑ�����
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
        /// XML�`���ŕۑ�����
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
    /// ConditionRecorder �̊T�v�̐����ł��B
    /// </summary>
    [Serializable]
    public class ConditionRecorderJson 
    {
        /// <summary>
        /// �T�[�o�[�ʏ����Ǘ�����n�b�V���e�[�u��
        /// �T�[�o�[���{�C���X�^���X�����L�[�Ƃ���
        /// </summary>
        public Dictionary<string, ServerJsonData> PerServerData
        {
            get;
            protected set;
        }

        /// <summary>
        /// �Ō�ɐڑ������T�[�o�[��Hashkey
        /// </summary>
        public string LastServerKey
        {
            get;
            set;
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public ConditionRecorderJson()
        {
            this.LastServerKey = "";
            this.PerServerData = new Dictionary<string, ServerJsonData>();
        }

        /// <summary>
        /// �^�ϊ�
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
        /// �쐬����
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
        /// Json�`���̐ݒ��ǂݍ���
        /// </summary>
        /// <returns></returns>
        protected static ConditionRecorderJson ReadJsonConfig()
        {
            // �ݒ�t�@�C���̓ǂݍ��݃X�g���[��
            StreamReader sr = null;


            ConditionRecorderJson result = null;
            try
            {
                // �ݒ�t�@�C����ǂݍ���
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
                // ����C���X�g�[�����̓t�@�C�����Ȃ��\��������
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
        /// �ۑ�����
        /// </summary>
        public void Save()
        {
            SaveJson();
        }

        /// <summary>
        /// Json�`���ŕۑ�����
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
