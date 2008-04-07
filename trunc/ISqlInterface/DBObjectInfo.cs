using	System;
using	System.Data;
using	System.Collections;

namespace quickDBExplorer
{
	/// <summary>
	/// �t�B�[���h���擾���̃f�[�^�Ď擾�p�C�x���g�n���h��
	/// </summary>
	public delegate void DataGetEventHandler(object sender, System.EventArgs e);

	/// <summary>
	/// ���X�g�ɕ\������ DB �I�u�W�F�N�g�̏����Ǘ�����
	/// </summary>
	public class	DBObjectInfo
	{

		/// <summary>
		/// �t�B�[���h���擾���ɏ�񂪂܂����擾���ɔ�������C�x���g
		/// ���̃C�x���g�n���h���ɂ��A�f�[�^���Z�b�g������
		/// </summary>
		public event DataGetEventHandler DataGet = null;

		private		string objType;
		/// <summary>
		/// �I�u�W�F�N�g�̎��
		/// ��: Table
		/// V:		View
		/// S:		Synonym
		/// </summary>
		public		string	ObjType
		{
			get { return this.objType; }
			set { this.objType = value; }
		}


		/// <summary>
		/// �I�u�W�F�N�g�̎��(�\���p)
		/// ��: Table
		/// V:		View
		/// S:		Synonym
		/// </summary>
		public		string DisplayObjType
		{
			get 
			{
				switch(this.objType)
				{
					case	"U":
						return " ";
					case	"SN":
						return "S";
					default:
						return this.objType;
				}
			}
		}

		private		string	owner;
		/// <summary>
		/// �I�u�W�F�N�g�̏��L�Җ�
		/// </summary>
		public	string	Owner
		{
			get { return this.owner; }
			set { this.owner = value; }
		}

		private		string	objname;
		/// <summary>
		/// �I�u�W�F�N�g�̖���
		/// </summary>
		public	string ObjName
		{
			get { return this.objname; }
			set { this.objname = value; }
		}

		private string createTime;
		/// <summary>
		/// �I�u�W�F�N�g���������ꂽ����
		/// </summary>
		public string CreateTime
		{
			get { return this.createTime; }
			set { this.createTime = value; }
		}

		private string synonymBase;
		/// <summary>
		/// �I�u�W�F�N�g���V�m�j���̏ꍇ�A���̎Q�Ɛ�
		/// </summary>
		public string SynonymBase
		{
			get { return this.synonymBase; }
			set { this.synonymBase = value; }
		}
		private string synonymBaseType;

		/// <summary>
		/// �I�u�W�F�N�g���V�m�j���̏ꍇ�A���̎Q�Ɛ�̃I�u�W�F�N�g�̎��
		/// </summary>
		public string SynonymBaseType
		{
			get { return this.synonymBaseType; }
			set { this.synonymBaseType = value; }
		}

		/// <summary>
		/// [] �ł��������I�u�W�F�N�g�̐������̂��擾����
		/// </summary>
		public string	FormalName
		{
			get 
			{
				return "[" + this.owner + "].[" + this.objname + "]";
			}
		}

		/// <summary>
		/// select �\���ۂ����擾����
		/// </summary>
		public bool		CanSelect
		{
			get 
			{
				switch( this.objType )
				{
					case	"U":
					case	"V":
						return true;
					case	"SN":	
						// Synonym�̏ꍇ�A�x�[�X�I�u�W�F�N�g�̌^������K�v������
						if( this.synonymBaseType == "U" ||
							this.synonymBaseType == "V" )
						{
							return true;
						}
						break;
					default:
						break;
				}
				return false;
			}
		}

		/// <summary>
		/// �V�m�j�����ǂ������擾����
		/// </summary>
		public bool	IsSynonym
		{
			get 
			{
				if( this.objType == "SN" )
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		/// <summary>
		/// ���v���̍X�V���\�Ȃǂ������擾����
		/// </summary>
		public bool	CanStatistics
		{
			get 
			{
				if( this.objType == "U" )
				{
					return true;
				}
				if( this.objType == "SN" &&
					this.synonymBaseType == "U" )
				{
					return true;
				}
				return false;
			}
		}

		/// <summary>
		/// ���ۂ̃I�u�W�F�N�g�����擾����
		/// �V�m�j���̏ꍇ�͂��̎Q�Ɛ�̃I�u�W�F�N�g����Ԃ�
		/// </summary>
		public string	RealObjName
		{
			get 
			{
				if( this.objType == "SN" )
				{
					return this.synonymBase;
				}
				return this.FormalName;
			}
		}

		/// <summary>
		/// ���ۂ̃I�u�W�F�N�g�����擾����
		/// ���̂ɂ�[]�͂��Ȃ�
		/// �V�m�j���̏ꍇ�͂��̎Q�Ɛ�̃I�u�W�F�N�g����Ԃ�
		/// </summary>
		public string	RealObjNameNoPare
		{
			get
			{
				if( this.objType == "SN" )
				{
					return this.synonymBase;
				}
				return string.Format(System.Globalization.CultureInfo.CurrentCulture,"{0}.{1}", this.owner, this.objname );
			}
		}

		/// <summary>
		/// ���ۂ̃I�u�W�F�N�g�̌^���擾����
		/// �V�m�j���̏ꍇ�͂��̎Q�Ɛ�̃I�u�W�F�N�g�̌^��Ԃ�
		/// </summary>
		public string	RealObjType
		{
			get 
			{
				if( this.objType == "SN" )
				{
					return this.synonymBaseType;
				}
				else
				{
					return this.objType;
				}
			}
		}

		private	ArrayList	fieldInfo = null;
		/// <summary>
		/// �e�[�u���̃t�B�[���h�����L���b�V�����ĕێ�����
		/// �t�B�[���h���̃R���N�V�������Ǘ�����
		/// </summary>
		public	ArrayList	FieldInfo
		{
			get 
			{
				// �܂��ǂݍ���ł��Ȃ��ꍇ�́A�ǂݍ��݂������I�Ɏ��{����
				if( this.fieldInfo == null )
				{
					this.DataGet(this, new EventArgs());
				}
				return this.fieldInfo; 
			}
			set 
			{
				this.fieldInfo = value;
			}
		}

		private DataTable	pSchemaBaseInfo;
		/// <summary>
		/// �X�L�[�}����ێ����Ă��� DataTable
		/// �s�͕ێ����Ă��Ȃ�
		/// </summary>
		public	DataTable	SchemaBaseInfo
		{
			get { 
				// �܂��ǂݍ���ł��Ȃ��ꍇ�́A�ǂݍ��݂������I�Ɏ��{����
				if( this.fieldInfo == null )
				{
					this.DataGet(this, new EventArgs());
				}
				return this.pSchemaBaseInfo; 
			}
			set { this.pSchemaBaseInfo = value; }
		}

		/// <summary>
			/// �R���X�g���N�^
			/// </summary>
			/// <param name="objectType">�I�u�W�F�N�g�̌^</param>
			/// <param name="owner">�I�u�W�F�N�g�̏��L�Җ�</param>
			/// <param name="name">�I�u�W�F�N�g�̖���</param>
			/// <param name="createdTime">�I�u�W�F�N�g�̍쐬����</param>
			/// <param name="synonymBase">�V�m�j���̏ꍇ�A���̎Q�Ɛ�̃I�u�W�F�N�g��</param>
			/// <param name="synonymBaseType">�V�m�j���̏ꍇ�A���̎Q�Ɛ�̃I�u�W�F�N�g�̌^</param>
		public DBObjectInfo( string objectType, string owner, string name, string createdTime, string synonymBase, string synonymBaseType )
		{
			if( objectType == null )
			{
				throw new ArgumentNullException("objectType");
			}
			if( synonymBaseType == null )
			{
				throw new ArgumentNullException("synonymBaseType");
			}

			this.objType = objectType.TrimEnd(null);
			this.owner = owner;
			this.objname = name;
			this.createTime = createdTime;
			this.synonymBase = synonymBase;
			this.synonymBaseType = synonymBaseType.TrimEnd(null);
		}

		/// <summary>
		/// �����񉻂���B
		/// ���L�Җ�+"."+�I�u�W�F�N�g��
		/// ��Ԃ�
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format(System.Globalization.CultureInfo.CurrentCulture,"{0}.{1}", this.owner, this.objname );
		}

		/// <summary>
		/// Alias ���w�肳��Ă����ꍇ�ɁAAlias �t���̃I�u�W�F�N�g�������̂�Ԃ�
		/// </summary>
		/// <param name="alias">�e�[�u���C���q</param>
		/// <returns>��͌�̃e�[�u����([owner].[tabblname] alias �`��)</returns>
		public string GetAliasName(string alias)
		{
			string retstr;
			retstr = this.FormalName;
			if( alias != null && alias.Length != 0 )
			{
				retstr += " " + alias;
			}
			return retstr;
		}

		/// <summary>
		/// �������̂Ɏw��ꂽ�������t���������̂ɂ��ĕԂ�
		/// </summary>
		/// <param name="suffix">�t�����镶����</param>
		/// <returns></returns>
		public string	GetNameAdd(string suffix)
		{
			return string.Format(System.Globalization.CultureInfo.CurrentCulture,"[{0}].[{1}_{2}]",this.owner, this.objname, suffix);
		}

		/// <summary>
		/// �I�u�W�F�N�g�̏����擾���Ȃ���
		/// </summary>
		public	void	ReloadInfo()
		{
			this.DataGet(this, new EventArgs());
		}
	}
}
