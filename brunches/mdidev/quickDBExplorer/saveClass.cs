using System;
using System.Collections;
using System.Xml;

namespace quickDBExplorer
{
	[Serializable]
	public class ServerData
	{
		protected string		servername;
		protected string		instancename;
		protected string		keyname;

		public string		lastdb;		// �Ō�ɗ��p����DB��

		public Hashtable		dbopt;		// DB ���̍ŏI���[�U�[���L�^����
		public int	isShowsysuser;		// �V�X�e�����[�U�[��\�����邩�ۂ�
		public int	sortKey;
		public int	showView;
		public	Hashtable	outdest;
		public	Hashtable	outfile;
		public	Hashtable	showgrid;
		public 	Hashtable	griddspcnt;
		public	Hashtable	txtencode;
		public	bool	isSaveKey = true;
		public	bool	IsUseTrust = false;
		public	string	loginUser = "";

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
		}

		public string Servername
		{
			get { return this.servername; }
			set 
			{ 
				this.servername = value; 
				this.keyname = value + this.instancename;
			}
		}
		public string InstanceName
		{
			get { return this.instancename; }
			set 
			{
				this.instancename = value;
				this.keyname = this.servername + this.instancename;
			}
		}
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
	public class saveClass
	{
		public Hashtable	ht;
		public string lastserverkey = "";
		public saveClass()
		{
			ht = new Hashtable();
		}
	}
}
