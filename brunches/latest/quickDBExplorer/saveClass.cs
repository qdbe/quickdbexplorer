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
	/// �T�[�o�[�ʂ̊e��w�藚���f�[�^���Ǘ�����
	/// </summary>
	[Serializable]
	public class ServerData : ISerializable
	{
		/// <summary>
		/// �ڑ���T�[�o�[��
		/// </summary>
		protected string		servername;
		/// <summary>
		/// �ڑ���C���X�^���X��
		/// </summary>
		protected string		instancename;
		/// <summary>
		/// �T�[�o�[�{�C���X�^���X�𗘗p�����ڑ���T�[�o�[��Hashtable�̃L�[���
		/// </summary>
		protected string		keyname;

		/// <summary>
		/// �Ō�ɗ��p����DB��
		/// </summary>
		protected string		lastdb;		
		/// <summary>
		/// �Ō�ɗ��p����DB��
		/// </summary>
		public string		LastDb
		{
			get { return this.lastdb; }
			set { this.lastdb = value; }
		}

		/// <summary>
		/// // DB ���̍ŏI���[�U�[���L�^����
		/// ������o�^�����邽�߁A������string �̗v�f��������ArrayList�Ƃ���
		/// </summary>
		protected Hashtable		dbopt;	
		/// <summary>
		/// // DB ���̍ŏI���[�U�[���L�^����
		/// ������o�^�����邽�߁A������string �̗v�f��������ArrayList�Ƃ���
		/// </summary>
		public Hashtable		Dbopt
		{
			get { return this.dbopt; }
			set { this.dbopt = value; }
		}

		/// <summary>
		/// �V�X�e�����[�U�[��\�����邩�ۂ�
		/// </summary>
		protected int	isShowsysuser;	
		/// <summary>
		/// �V�X�e�����[�U�[��\�����邩�ۂ�
		/// </summary>
		public int	IsShowsysuser
		{
			get { return this.isShowsysuser; }
			set { this.isShowsysuser = value; }
		}

		/// <summary>
		///table/view ���X�g�̃\�[�g�� 
		/// </summary>
		protected int	sortKey;
		/// <summary>
		///table/view ���X�g�̃\�[�g�� 
		/// </summary>
		public int	SortKey
		{
			get { return this.sortKey ; }
			set { this.sortKey = value; }
		}

		/// <summary>
		/// view ��\�������邩�ǂ���
		/// </summary>
		protected int	showView;
		/// <summary>
		/// view ��\�������邩�ǂ���
		/// </summary>
		public int	ShowView
		{
			get { return this.showView; }
			set { this.showView = value; }
		}
		
		/// <summary>
		/// �f�[�^�o�͐�̎w��
		/// </summary>
		protected	Hashtable	outdest;
		/// <summary>
		/// �f�[�^�o�͐�̎w��
		/// </summary>
		public	Hashtable	OutDest
		{
			get { return this.outdest; }
			set { this.outdest = value; }
		}
		/// <summary>
		/// �f�[�^�o�͐�̃t�@�C���E�t�H���_��
		/// </summary>
		protected	Hashtable	outfile;
		/// <summary>
		/// �f�[�^�o�͐�̃t�@�C���E�t�H���_��
		/// </summary>
		public	Hashtable	OutFile
		{
			get { return this.outfile; }
			set { this.outfile = value; }
		}
		/// <summary>
		/// �f�[�^�O���b�h��\�����邩�ۂ�
		/// </summary>
		protected	Hashtable	showgrid;
		/// <summary>
		/// �f�[�^�O���b�h��\�����邩�ۂ�
		/// </summary>
		public	Hashtable	ShowGrid
		{
			get { return this.showgrid; }
			set { this.showgrid = value; }
		}

		/// <summary>
		/// �O���b�h�\������
		/// </summary>
		protected 	Hashtable	griddspcnt;
		/// <summary>
		/// �O���b�h�\������
		/// </summary>
		public 	Hashtable	GridDspCnt
		{
			get { return this.griddspcnt; }
			set { this.griddspcnt = value; }
		}
		
		/// <summary>
		/// �e�L�X�g�o�͎��̕����R�[�h
		/// </summary>
		protected	Hashtable	txtencode;
		/// <summary>
		/// �e�L�X�g�o�͎��̕����R�[�h
		/// </summary>
		public	Hashtable	TxtEncode
		{
			get { return this.txtencode; }
			set { this.txtencode = value; }
		}
        /// <summary>
		/// �T�[�o�[�ʏ����L�����邩�ۂ�
		/// </summary>
		protected	bool	isSaveKey = true;
		/// <summary>
		/// �T�[�o�[�ʏ����L�����邩�ۂ�
		/// </summary>
		public	bool	IsSaveKey
		{
			get { return this.isSaveKey; }
			set { this.isSaveKey = value; }
		}
		/// <summary>
		/// �M���֌W�ڑ��𗘗p���邩�ۂ�
		/// </summary>
		protected	bool	isUseTrust = false;
		/// <summary>
		/// �M���֌W�ڑ��𗘗p���邩�ۂ�
		/// </summary>
		public	bool	IsUseTrust
		{
			get { return this.IsUseTrust; }
			set { this.IsUseTrust = value; }
		}
		/// <summary>
		/// ���O�C�����[�U�[��
		/// </summary>
		protected	string	loginUser = "";
		/// <summary>
		/// ���O�C�����[�U�[��
		/// </summary>
		public	string	LoginUser
		{
			get { return this.loginUser; }
			set { this.loginUser = value; }
		}
		/// <summary>
		/// where ��̓��͗������
		/// </summary>
		protected textHistory  whereHistory;
		/// <summary>
		/// where ��̓��͗������
		/// </summary>
		public textHistory  WhereHistory
		{
			get { return this.whereHistory; }
			set { this.whereHistory = value; }
		}

		/// <summary>
		/// order by ��̓��͗������
		/// </summary>
		protected textHistory  sortHistory;
		/// <summary>
		/// order by ��̓��͗������
		/// </summary>
		public textHistory  SortHistory
		{
			get { return this.sortHistory; }
			set { this.sortHistory = value; }
		}

		/// <summary>
		/// order by ��̓��͗������
		/// </summary>
		protected textHistory  aliasHistory;
		/// <summary>
		/// order by ��̓��͗������
		/// </summary>
		public textHistory  AliasHistory
		{
			get { return this.aliasHistory; }
			set { this.aliasHistory = value; }
		}

		/// <summary>
		/// select ���s�������
		/// </summary>
		protected textHistory  selectHistory;
		/// <summary>
		/// select ���s�������
		/// </summary>
		public textHistory  SelectHistory
		{
			get { return this.selectHistory; }
			set { this.selectHistory = value; }
		}

		/// <summary>
		/// �N�G�����s���̓��͗���
		/// </summary>
		protected textHistory  dmlHistory;
		/// <summary>
		/// �N�G�����s���̓��͗���
		/// </summary>
		public textHistory  DMLHistory
		{
			get { return this.dmlHistory; }
			set { this.dmlHistory = value; }
		}

		/// <summary>
		/// �e��R�}���h���͗���
		/// </summary>
		protected textHistory  cmdHistory;
		/// <summary>
		/// �e��R�}���h���͗���
		/// </summary>
		public textHistory  CmdHistory
		{
			get { return this.cmdHistory; }
			set { this.cmdHistory = value; }
		}

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
		/// �V���A���C�Y�����p�R���X�g���N�^
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
		/// �V���A���C�Y�����p
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
	/// saveClass �̊T�v�̐����ł��B
	/// </summary>
	[Serializable]
	public class saveClass : ISerializable
	{
		/// <summary>
		/// �T�[�o�[�ʏ����Ǘ�����n�b�V���e�[�u��
		/// �T�[�o�[���{�C���X�^���X�����L�[�Ƃ���
		/// </summary>
		protected Hashtable	perServerData;
		/// <summary>
		/// �T�[�o�[�ʏ����Ǘ�����n�b�V���e�[�u��
		/// �T�[�o�[���{�C���X�^���X�����L�[�Ƃ���
		/// </summary>
		public Hashtable	PerServerData
		{
			get { return this.ht; }
			set { this.ht = value; }
		}

		/// <summary>
		/// �Ō�ɐڑ������T�[�o�[��Hashkey
		/// </summary>
		protected string lastserverkey = "";
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
		public saveClass()
		{
			this.perServerData = new Hashtable();
		}

		/// <summary>
		/// �V���A���C�Y�����p�R���X�g���N�^
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
		/// �V���A���C�Y�����p
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
