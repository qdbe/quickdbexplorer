using System;

namespace quickDBExplorer
{
	/// <summary>
	/// ���X�g�ɕ\������ DB �I�u�W�F�N�g�̏����Ǘ�����
	/// </summary>
	public class	DBObjectInfo
	{
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
		public		string DspObjType
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

		private	DBFieldInfo	fieldInfo;
		/// <summary>
		/// �e�[�u���̃t�B�[���h�����L���b�V�����ĕێ�����
		/// </summary>
		public	DBFieldInfo	FieldInfo
		{
			get { return this.fieldInfo; }
			set { this.fieldInfo = value; }
		}

			/// <summary>
			/// �R���X�g���N�^
			/// </summary>
			/// <param name="otype">�I�u�W�F�N�g�̌^</param>
			/// <param name="owner">�I�u�W�F�N�g�̏��L�Җ�</param>
			/// <param name="name">�I�u�W�F�N�g�̖���</param>
			/// <param name="cretime">�I�u�W�F�N�g�̍쐬����</param>
			/// <param name="synbase">�V�m�j���̏ꍇ�A���̎Q�Ɛ�̃I�u�W�F�N�g��</param>
			/// <param name="synbtype">�V�m�j���̏ꍇ�A���̎Q�Ɛ�̃I�u�W�F�N�g�̌^</param>
			public DBObjectInfo( string otype, string owner, string name, string cretime, string synbase, string synbtype )
		{
			this.objType = otype.TrimEnd(null);
			this.owner = owner;
			this.objname = name;
			this.createTime = cretime;
			this.synonymBase = synbase;
			this.synonymBaseType = synbtype.TrimEnd(null);
		}

		/// <summary>
		/// �����񉻂���B
		/// ���L�Җ�+"."+�I�u�W�F�N�g��
		/// ��Ԃ�
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format("{0}.{1}", this.owner, this.objname );
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
			if( alias != "" )
			{
				retstr += " " + alias;
			}
			return retstr;
		}

		/// <summary>
		/// �������̂Ɏw��ꂽ�������t���������̂ɂ��ĕԂ�
		/// </summary>
		/// <param name="addstr">�t�����镶����</param>
		/// <returns></returns>
		public string	GetNameAdd(string addstr)
		{
			return string.Format("[{0}].[{1}_{2}]",this.owner, this.objname, addstr);
		}
	}
}
