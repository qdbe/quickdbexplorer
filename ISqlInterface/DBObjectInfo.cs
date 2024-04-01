using	System;
using	System.Data;
using	System.Collections;
using   System.Collections.Generic;
using System.Web;

namespace quickDBExplorer
{
	/// <summary>
	/// �t�B�[���h���擾���̃f�[�^�Ď擾�p�C�x���g�n���h��
	/// </summary>
    public delegate void DataGetEventHandler(DBObjectInfo sender, System.EventArgs e);

	/// <summary>
	/// ���X�g�ɕ\������ DB �I�u�W�F�N�g�̏����Ǘ�����
	/// </summary>
    [Serializable()]
	public class	DBObjectInfo
	{

		/// <summary>
		/// �t�B�[���h���擾���ɏ�񂪂܂����擾���ɔ�������C�x���g
		/// ���̃C�x���g�n���h���ɂ��A�f�[�^���Z�b�g������
		/// </summary>
		public event DataGetEventHandler DataGet = null;

		/// <summary>
		/// �I�u�W�F�N�g�̎��
		/// ��: Table
		/// V:		View
		/// S:		Synonym
		/// </summary>
        public string ObjType{ get; private set; }


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
				switch(this.ObjType)
				{
					case	"U":
						return " ";
					case	"SN":
						return "S";
					default:
                        return this.ObjType;
				}
			}
		}

		/// <summary>
		/// �I�u�W�F�N�g�̏��L�Җ�
		/// </summary>
        public string Owner { get; private set; }

		/// <summary>
		/// �I�u�W�F�N�g�̖���
		/// </summary>
        public string ObjName { get; private set; }


        /// <summary>
        /// �I�u�W�F�N�g��ID
        /// </summary>
        public int ObjId { get; private set; }

        /// <summary>
		/// �I�u�W�F�N�g���������ꂽ����
		/// </summary>
        public string CreateTime { get; private set; }

		/// <summary>
		/// �I�u�W�F�N�g���X�V���ꂽ����
		/// </summary>
		public string ModifyTime { get; private set; }

		/// <summary>
		/// �I�u�W�F�N�g���V�m�j���̏ꍇ�A���̎Q�Ɛ�
		/// </summary>
		public string SynonymBase { get; private set; }

		/// <summary>
		/// �I�u�W�F�N�g���V�m�j���̏ꍇ�A���̎Q�Ɛ�̃I�u�W�F�N�g�̎��
		/// </summary>
        public string SynonymBaseType { get; private set; }

		/// <summary>
		/// [] �ł��������I�u�W�F�N�g�̐������̂��擾����
		/// </summary>
		public string	FormalName
		{
			get 
			{
				return "[" + this.Owner + "].[" + this.ObjName + "]";
			}
		}

		/// <summary>
		/// select �\���ۂ����擾����
		/// </summary>
		public bool		CanSelect
		{
			get 
			{
				switch( this.ObjType )
				{
					case	"U":
					case	"V":
						return true;
					case	"SN":	
						// Synonym�̏ꍇ�A�x�[�X�I�u�W�F�N�g�̌^������K�v������
						if( this.SynonymBaseType == "U" ||
                            this.SynonymBaseType == "V")
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
				if( this.ObjType == "SN" )
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
				if( this.ObjType == "U" )
				{
					return true;
				}
				if( this.ObjType == "SN" &&
					this.SynonymBaseType == "U" )
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
				if( this.ObjType == "SN" )
				{
					return this.SynonymBase;
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
				if( this.ObjType == "SN" )
				{
					return this.SynonymBase;
				}
				return string.Format(System.Globalization.CultureInfo.CurrentCulture,"{0}.{1}", this.Owner, this.ObjName );
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
				if( this.ObjType == "SN" )
				{
					return this.SynonymBaseType;
				}
				else
				{
                    return this.ObjType;
				}
			}
		}

		private	List<DBFieldInfo>	pFieldInfo = null;
        private Dictionary<string, DBFieldInfo> fieldDictionary = new Dictionary<string, DBFieldInfo>();
        private Dictionary<string, DBFieldInfo> FieldDictionary
        {
            get
            {
                SetFieldInfo();
                return this.fieldDictionary;
            }
        }

        private List<DBFieldInfo> FieldInfoList
        {
            get
            {
                // �܂��ǂݍ���ł��Ȃ��ꍇ�́A�ǂݍ��݂������I�Ɏ��{����
                SetFieldInfo(); 
                return this.pFieldInfo;
            }
        }

		/// <summary>
		/// �I�u�W�F�N�g�̃t�B�[���h�����L���b�V�����ĕێ�����
		/// �t�B�[���h���̃R���N�V�������Ǘ�����
		/// </summary>
        public IEnumerable<DBFieldInfo> FieldInfo
		{
			get 
			{
                SetFieldInfo();
                return this.FieldInfoList;
			}
		}

        /// <summary>
        /// �t�B�[���h�̍��ڐ����擾����
        /// </summary>
        public int FieldCount
        {
            get
            {
                SetFieldInfo();
                return this.FieldInfoList.Count;
            }
        }

        /// <summary>
        /// �t�B�[���h�̏���Ԃ�
        /// </summary>
        /// <param name="filedName">�t�B�[���h��</param>
        /// <returns></returns>
        public DBFieldInfo this[string filedName]{
            get {
                return this.FieldDictionary[filedName]; 
            }
        }

        /// <summary>
        /// �t�B�[���h�̏���Ԃ�
        /// </summary>
        /// <param name="fieldorder">�t�B�[���h�̏���(0�I���W��)</param>
        /// <returns></returns>
        public DBFieldInfo this[int fieldorder]
        {
            get {
                SetFieldInfo();
                return this.FieldInfoList[fieldorder]; 
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
                SetFieldInfo();
				return this.pSchemaBaseInfo; 
			}
            private set { this.pSchemaBaseInfo = value; }
		}

        private void SetFieldInfo()
        {
            if (this.pFieldInfo == null)
            {
                this.DataGet(this, new EventArgs());
                SetFieldDictionary();
            }
        }

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		/// <param name="objectid">�I�u�W�F�N�gID</param>
		/// <param name="objectType">�I�u�W�F�N�g�̌^</param>
		/// <param name="owner">�I�u�W�F�N�g�̏��L�Җ�</param>
		/// <param name="name">�I�u�W�F�N�g�̖���</param>
		/// <param name="createdTime">�I�u�W�F�N�g�̍쐬����</param>
		/// <param name="modifyTime">�I�u�W�F�N�g�̍X�V����</param>
		/// <param name="synonymBase">�V�m�j���̏ꍇ�A���̎Q�Ɛ�̃I�u�W�F�N�g��</param>
		/// <param name="synonymBaseType">�V�m�j���̏ꍇ�A���̎Q�Ɛ�̃I�u�W�F�N�g�̌^</param>
		public DBObjectInfo( int objectid, string objectType, string owner, string name, string createdTime, string modifyTime, string synonymBase, string synonymBaseType )
		{
			if( objectType == null )
			{
				throw new ArgumentNullException("objectType");
			}
			if( synonymBaseType == null )
			{
				throw new ArgumentNullException("synonymBaseType");
			}

            this.ObjId = objectid;
            this.ObjType = objectType.TrimEnd(null);
			this.Owner = owner;
			this.ObjName = name;
			this.CreateTime = createdTime;
			this.ModifyTime = modifyTime;
			this.SynonymBase = synonymBase;
			this.SynonymBaseType = synonymBaseType.TrimEnd(null);
			this.pSchemaBaseInfo = null;
		}

		/// <summary>
		/// �����񉻂���B
		/// ���L�Җ�+"."+�I�u�W�F�N�g��
		/// ��Ԃ�
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format(System.Globalization.CultureInfo.CurrentCulture,"{0}.{1}", this.Owner, this.ObjName );
		}

        /// <summary>
        /// �t�B�[���h�����N���A����
        /// </summary>
        public void ClearField()
        {
            this.pFieldInfo = new List<DBFieldInfo>();
        }

        /// <summary>
        /// �t�B�[���h����ǉ�����
        /// </summary>
        /// <param name="add"></param>
        public void AddField(DBFieldInfo add)
        {
            SetFieldInfo();
            this.pFieldInfo.Add(add);
        }

		/// <summary>
		/// Alias ���w�肳��Ă����ꍇ�ɁAAlias �t���̃I�u�W�F�N�g�������̂�Ԃ�
		/// </summary>
		/// <param name="alias">�I�u�W�F�N�g�C���q</param>
		/// <returns>��͌�̃I�u�W�F�N�g��([owner].[tabblname] alias �`��)</returns>
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
			return string.Format(System.Globalization.CultureInfo.CurrentCulture,"[{0}].[{1}_{2}]",this.Owner, this.ObjName, suffix);
		}

		/// <summary>
		/// �I�u�W�F�N�g�̏����擾���Ȃ���
		/// </summary>
		public	void	ReloadInfo()
		{
			this.DataGet(this, new EventArgs());
		}

		/// <summary>
		/// �t�B�[���h�ɃA�Z���u���𗘗p�����^�����邩�ۂ�
		/// </summary>
		public bool  IsUseAssemblyType
		{
			get 
			{
				foreach(DBFieldInfo fi in this.FieldInfo)
				{
					if( fi.IsAssembly == true )
					{
						return true;
					}
				}
				return false;
			}
		}

		/// <summary>
		/// �t�B�[���h�� Identity�𗘗p�������̂������ۂ�
		/// </summary>
		public bool IsUseIdentity
		{
			get 
			{
				foreach(DBFieldInfo fi in this.FieldInfo)
				{
					if( fi.IsIdentity == true )
					{
						return true;
					}
				}
				return false;
			}
		}

        /// <summary>
        /// View���ۂ�
        /// </summary>
        public bool IsView
        {
            get
            {

                if (this.ObjType == "V")
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// �X�L�[�}���̃e�[�u�����Z�b�g����
        /// </summary>
        /// <param name="dt"></param>
        public void SetSchemaInfo(DataTable dt)
        {
            this.pSchemaBaseInfo = dt;
        }


        private void SetFieldDictionary()
        {
            this.fieldDictionary.Clear();
            foreach (DBFieldInfo each in this.FieldInfoList)
            {
                this.fieldDictionary[each.Name] = each;
            }
        }


		/// <summary>
		/// �w�肳�ꂽ���O�Ɉ�v���邩�ǂ���
		/// </summary>
		/// <param name="schema"></param>
		/// <param name="objName"></param>
		/// <param name="isCheckSchema"></param>
		/// <param name="isCaseSensitive"></param>
		/// <param name="selectType"></param>
		/// <returns></returns>
        public bool IsMatch(string schema, string objName, bool isCheckSchema, bool isCaseSensitive, SearchType selectType)
		{
			if (isCheckSchema)
			{
				if(Owner == schema)
				{
					return CheckObjName(objName, isCaseSensitive,selectType);
				}
			}
            else
            {
                return CheckObjName(objName, isCaseSensitive,selectType);
            }
			return false;
        }

        private bool CheckObjName(string objName, bool isCaseSensitive, SearchType selectType)
        {
			string objn = this.ObjName;
			string searchName = objName;
			string[] searchNames = searchName.Split(".".ToCharArray());
			if (searchNames.Length == 2 &&
				!string.IsNullOrEmpty(searchNames[0])
				)
			{
				// �X�L�[�}�t��
				if (isCaseSensitive)
				{
					if (this.Owner != searchNames[0])
					{
						return false;
					}
				}
				else
				{
					if (this.Owner != searchNames[0])
					{
						return false;
					}
				}
                searchName = searchNames[1];
            }

            if (!isCaseSensitive)
            {
				objn = objn.ToLower();
				searchName = searchName.ToLower();
            }
            switch (selectType)
            {
                case SearchType.SearchExact:
                    if (objn == searchName)
                    {
						return true;
                    }
                    break;
                case SearchType.SearchContain:
                    if (objn.Contains(searchName))
                    {
                        return true;
                    }
                    break;
                case SearchType.SearchStartWith:
                    if (objn.StartsWith(searchName))
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
}
