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
		public string		lastdb;		

		/// <summary>
		/// // DB ���̍ŏI���[�U�[���L�^����
		/// ������o�^�����邽�߁A������string �̗v�f��������ArrayList�Ƃ���
		/// </summary>
		public Hashtable		dbopt;	

		/// <summary>
		/// �V�X�e�����[�U�[��\�����邩�ۂ�
		/// </summary>
		public int	isShowsysuser;	

		/// <summary>
		///table/view ���X�g�̃\�[�g�� 
		/// </summary>
		public int	sortKey;

		/// <summary>
		/// view ��\�������邩�ǂ���
		/// </summary>
		public int	showView;
		/// <summary>
		/// �f�[�^�o�͐�̎w��
		/// </summary>
		public	Hashtable	outdest;
		/// <summary>
		/// �f�[�^�o�͐�̃t�@�C���E�t�H���_��
		/// </summary>
		public	Hashtable	outfile;
		/// <summary>
		/// �f�[�^�O���b�h��\�����邩�ۂ�
		/// </summary>
		public	Hashtable	showgrid;
		/// <summary>
		/// �O���b�h�\������
		/// </summary>
		public 	Hashtable	griddspcnt;
		/// <summary>
		/// �e�L�X�g�o�͎��̕����R�[�h
		/// </summary>
		public	Hashtable	txtencode;
		/// <summary>
		/// �T�[�o�[�ʏ����L�����邩�ۂ�
		/// </summary>
		public	bool	isSaveKey = true;
		/// <summary>
		/// �M���֌W�ڑ��𗘗p���邩�ۂ�
		/// </summary>
		public	bool	IsUseTrust = false;
		/// <summary>
		/// ���O�C�����[�U�[��
		/// </summary>
		public	string	loginUser = "";
		/// <summary>
		/// where ��̓��͗������
		/// </summary>
		public textHistory  whereHistory;

		/// <summary>
		/// order by ��̓��͗������
		/// </summary>
		public textHistory  sortHistory;

		/// <summary>
		/// order by ��̓��͗������
		/// </summary>
		public textHistory  aliasHistory;

		/// <summary>
		/// select ���s�������
		/// </summary>
		public textHistory  selectHistory;

		/// <summary>
		/// �N�G�����s���̓��͗���
		/// </summary>
		public textHistory  DMLHistory;

		/// <summary>
		/// �e��R�}���h���͗���
		/// </summary>
		public textHistory  cmdHistory;

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
		public Hashtable	ht;

		/// <summary>
		/// �Ō�ɐڑ������T�[�o�[��Hashkey
		/// </summary>
		public string lastserverkey = "";

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public saveClass()
		{
			ht = new Hashtable();
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
				this.ht = (Hashtable)info.GetValue("SERVERDATA",typeof(Hashtable));
			} 
			catch
			{
				this.ht = new Hashtable();
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
			info.AddValue("SERVERDATA", this.ht, typeof(Hashtable) );
		}

	}
}
