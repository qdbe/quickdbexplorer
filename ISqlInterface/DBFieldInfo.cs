using	System;
using	System.Data;
using quickDBExplorer.DataType;
using System.Text;

namespace quickDBExplorer
{
	/// <summary>
	/// �e�[�u�����̃t�B�[���h���ڂ̏����Ǘ�����N���X
	/// </summary>
	public class DBFieldInfo
	{

		/// <summary>
		/// �Ή�����DataColumn
		/// </summary>
        public DataColumn Col { get; set; }

		/// <summary>
		/// �t�B�[���h��
		/// </summary>
		public string	Name
		{
			get { return this.Col.ColumnName; }
		}


        /// <summary>
        /// []�t�̃t�B�[���h���̂��擾����
        /// </summary>
        public string FormalName
        {
            get
            {
                return string.Format("[{0}]", this.Name);
            }
        }

		/// <summary>
		/// �t�B�[���h�̌^
		/// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// []�t�̌^���̂��擾����
        /// </summary>
        public string FormalTypeName
        {
            get
            {
                if (this.TypeName.StartsWith("[") &&
                    this.TypeName.EndsWith("]"))
                {
                    return this.TypeName;
                }
                return string.Format("[{0}]", this.TypeName);
            }
        }

		private string pRealTypeName = "";

		/// <summary>
		/// �V�X�e���̌^��
		/// </summary>
		public string RealTypeName
		{
			get { return pRealTypeName; }
			set { 
                pRealTypeName = value;

                // �����ɂ�������^���T�|�[�g�Ώۂ��ǂ����𒲂ׂ�
                if (!this.IsAssembly)
                {
                    // �A�Z���u���^�ȊO�͑Ή����Ă���͂�
                    dataType = TypeFactory.Create(value);
                    object retobj = null;
                    string errmsg = "";
                    // �����̌^���Ή����Ă��Ȃ��ꍇ�ADefaultType�ɂȂ�G���[�ƂȂ�͂�
                    dataType.TryParse(dataType.DefalutParseString, this, ref retobj, ref errmsg);
                }
            }
		}

        private baseType dataType;

		/// <summary>
		/// �t�B�[���h�̍ő咷
		/// </summary>
        public int Length { get; set; }

		/// <summary>
		/// �����_�̏ꍇ�̐����l�̍ő包��
		/// </summary>
        public int Prec { get; set; }

		/// <summary>
		/// �����_�̏ꍇ�̏����_�l�̍ő包��
		/// </summary>
        public int Xscale { get; set; }

		/// <summary>
		/// �t�B�[���h�̃J����ID���Ǘ�����
		/// </summary>
        public int Colid { get; set; }

		/// <summary>
		/// �t�B�[���h�̃J�����������Ǘ�����
		/// </summary>
        public int ColOrder { get; set; }

		/// <summary>
		/// NULL�������邩�ۂ����Ǘ�����
		/// </summary>
		public	bool		IsAllowNull
		{
			get { return this.Col.AllowDBNull; }
		}

        /// <summary>
        /// Nullable���ۂ���Ԃ�
        /// </summary>
        public bool IsNullable
        {
            get
            {
                try
                {
                    //object testobj = System.Nullable<dataType.Type>();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

		/// <summary>
		/// �����t�B�[���h�̏ꍇ�̏ƍ��������Ǘ�����
		/// </summary>
        public string Collation { get; set; }

		/// <summary>
		/// �v���C�}���L�[�̑ΏۂɂȂ��Ă���ꍇ�̃v���C�}���L�[���̏���
		/// �J�n��0
		/// -1�̏ꍇ�̓v���C�}���L�[�̗v�f�łȂ����Ƃ��w��
		/// </summary>
        public int PrimaryKeyOrder { get; set; }

		/// <summary>
		/// Identity �����������ۂ�
		/// </summary>
        public bool IsIdentity { get; set; }

        /// <summary>
        /// �o�C�i�����ۂ�
        /// </summary>
        public bool IsBinary
        {
            get {
                return this.dataType.IsBinary; 
            }
        }

		/// <summary>
		/// Identity�t�B�[���h�̏ꍇ�� Seed�l
		/// </summary>
        public decimal IncSeed { get; set; }

		/// <summary>
		/// Identity �t�B�[���h�̏ꍇ�� Step�l
		/// </summary>
		public decimal IncStep { get; set; }

		/// <summary>
		/// ���̌^�̍쐬���ł���A�Z���u���� ID �ł��B
		/// </summary>
        public int AssemblyId { get; set; }

		/// <summary>
		/// ���̌^���`���Ă���A�Z���u�����̃N���X�̖��O�ł��B
		/// </summary>
        public string AssemblyClassName { get; set; }

		/// <summary>
		/// �A�Z���u���̏C�����ꂽ�^���ł��B���̖��O�́AType.GetType() �ɓn���̂ɓK�����`���ɂȂ��Ă��܂��B
		/// </summary>
        public string AssemblyQFN { get; set; }

		/// <summary>
		/// �I�u�W�F�N�g���A�Z���u���𗘗p���Ă��邩�ۂ�
		/// </summary>
		public bool IsAssembly
		{
			get 
			{ 
				if( this.AssemblyQFN != string.Empty )
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
        /// �����f�[�^��ǂݍ��ނ��Ƃ��ł��邩�ۂ�
        /// </summary>
        public bool CanLoadData
        {
            get
            {
                return this.dataType.CanLoadData();
            }
        }

        /// <summary>
        /// C# �̌^����Ԃ�
        /// </summary>
        public string CSharpTypeString
        {
            get
            {
                return this.dataType.GetCSharpTypeString();
            }
        }

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public DBFieldInfo()
		{
            AssemblyQFN = string.Empty;
            AssemblyClassName = string.Empty;
            AssemblyId = -1;
            IncStep = 0;
            IncSeed = 0;
            IsIdentity = false;
            PrimaryKeyOrder = -1;
            Collation = string.Empty;
            ColOrder = 0;
            Colid = 0;
            TypeName = string.Empty;
            Length = 0;
            Prec = 0;
            Xscale = 0;
		}


        /// <summary>
        /// �o�͗p�Ƀf�[�^�����H����
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="col"></param>
        /// <param name="addstr"></param>
        /// <param name="unichar"></param>
        /// <param name="outNull"></param>
        /// <returns></returns>
        public string ConvData(IDataReader dr, int col, string addstr, string unichar, bool outNull)
        {
            if (dr.IsDBNull(col))
            {
                if (outNull)
                {
                    return "null";
                }
                else
                {
                    return "";
                }
            }
            else if (this.IsAssembly == true)
            {
                if (outNull == true)
                {
                    return this.TypeName + "::Parse(N'" + dr.GetString(col) + "')";
                }
                else
                {
                    return dr.GetString(col);
                }
            }
            return this.dataType.Convert(dr, col, addstr, unichar, outNull, this);
        }

        /// <summary>
        /// �t�B�[���h�̕\���`�����擾����
        /// </summary>
        /// <returns></returns>
        public string GetFieldTypeString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("  ").Append(this.dataType.GetFieldTypeString(this.TypeName, this.Length, this.Prec, this.Xscale)).Append(" ");

            if( this.IncSeed != 0)
			{
				sb.AppendFormat(System.Globalization.CultureInfo.CurrentCulture," IDENTITY({0},{1})",
					this.IncSeed,
					this.IncStep );
			}

            if (this.IsAllowNull == false)
            {
                sb.Append(" NOT NULL");
            }
            else
            {
                sb.Append(" NULL");
            }
            if (this.PrimaryKeyOrder >= 0)
            {
                sb.Append(" PRIMARY KEY");
            }

            return sb.ToString();
        }

        /// <summary>
        /// DDL�p�̌^��`��������擾����
        /// </summary>
        /// <param name="useParentheses"></param>
        /// <returns></returns>
        public string GetDDLTypeString(bool useParentheses)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("  ");
            if (useParentheses)
            {
                sb.Append(this.dataType.GetFieldTypeString(this.FormalTypeName, this.Length, this.Prec, this.Xscale));
            }
            else
            {
                sb.Append(this.dataType.GetFieldTypeString(this.TypeName, this.Length, this.Prec, this.Xscale));
            }

            if (this.Collation.Length != 0)
            {
                sb.Append("\t");
                sb.AppendFormat("COLLATE {0}", this.Collation);
            }

            if (this.IncSeed != 0)
            {
                sb.Append("\t");
                sb.AppendFormat("IDENTITY({0},{1})",
                    this.IncSeed,
                    this.IncStep);
            }

            sb.Append("\t");
            if (this.IsAllowNull == false)
            {
                sb.Append("NOT NULL");
            }
            else
            {
                sb.Append("NULL");
            }

            return sb.ToString();
        }

        /// <summary>
        /// ��������p�[�X���ăI�u�W�F�N�g�ɕϊ�����
        /// </summary>
        /// <param name="data"></param>
        /// <param name="fieldInfo"></param>
        /// <param name="result"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public bool TryParse(string data, DBFieldInfo fieldInfo, ref object result, ref string errmsg)
        {
            if (this.IsAllowNull == false && string.IsNullOrEmpty(data))
            {
                errmsg = "�l�̎w�肪�K�v�ł��B";
                return false;
            }
            return this.dataType.TryParse(data, fieldInfo, ref result, ref errmsg);
        }

        /// <summary>
        /// �l�����������ǂ����`�F�b�N����
        /// </summary>
        /// <param name="data"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public bool CheckValue(object data, out string errmsg)
        {
            errmsg = string.Empty;
            if (this.IsAllowNull == false && 
                ( data == null ||
                data == DBNull.Value)
                )
            {
                errmsg = "�l�̎w�肪�K�v�ł��B";
                return false;
            }
            return this.dataType.CheckValue(data, this, out errmsg);
        }

    }
}
