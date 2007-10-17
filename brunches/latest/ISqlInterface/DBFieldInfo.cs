using	System;
using	System.Data;

namespace quickDBExplorer
{
	/// <summary>
	/// �e�[�u�����̃t�B�[���h���ڂ̏����Ǘ�����N���X
	/// </summary>
	public class DBFieldInfo
	{

		private	DataColumn	col;
		/// <summary>
		/// �Ή�����DataColumn
		/// </summary>
		public	DataColumn	Col
		{
			get	{ return this.col; }
			set { this.col = value; }
		}

		/// <summary>
		/// �t�B�[���h��
		/// </summary>
		public string	Name
		{
			get { return this.col.ColumnName; }
		}

		private	string	typeName = "";
		/// <summary>
		/// �t�B�[���h�̌^
		/// </summary>
		public  string	TypeName
		{
			get { return this.typeName; }
			set { this.typeName = value; }
		}

		private	int		length = 0;
		/// <summary>
		/// �t�B�[���h�̍ő咷(������̏ꍇ)
		/// </summary>
		public	int		Length
		{
			get { return this.length; }
			set { this.length = value; }
		}

		private	int		prec = 0;
		/// <summary>
		/// �����_�̏ꍇ�̐����l�̍ő包��
		/// </summary>
		public	int		Prec
		{
			get { return this.prec; }
			set { this.prec = value; }
		}

		private	int		xscale = 0;
		/// <summary>
		/// �����_�̏ꍇ�̏����_�l�̍ő包��
		/// </summary>
		public	int		Xscale
		{
			get { return this.xscale; }
			set { this.xscale = value; }
		}

		private	int		colid = 0;
		/// <summary>
		/// �t�B�[���h�̃J����ID���Ǘ�����
		/// </summary>
		public int		Colid
		{
			get { return this.colid; }
			set { this.colid = value; }
		}

		private	int		colorder = 0;
		/// <summary>
		/// �t�B�[���h�̃J�����������Ǘ�����
		/// </summary>
		public	int		Colorder
		{
			get { return this.colorder; }
			set { this.colorder = value; }
		}

		/// <summary>
		/// NULL�������邩�ۂ����Ǘ�����
		/// </summary>
		public	bool		IsNullable
		{
			get { return this.col.AllowDBNull; }
		}

		private	string	collation = string.Empty;
		/// <summary>
		/// �����t�B�[���h�̏ꍇ�̏ƍ��������Ǘ�����
		/// </summary>
		public	string	Collation
		{
			get { return this.collation; }
			set { this.collation = value; }
		}

		private	int	primaryKeyOrder = -1;
		/// <summary>
		/// �v���C�}���L�[�̑ΏۂɂȂ��Ă���ꍇ�̃v���C�}���L�[���̏���
		/// �J�n��0
		/// -1�̏ꍇ�̓v���C�}���L�[�̗v�f�łȂ����Ƃ��w��
		/// </summary>
		public	int	PrimaryKeyOrder
		{
			get { return this.primaryKeyOrder; }
			set { this.primaryKeyOrder = value; }
		}

		private decimal	incSeed = 0;
		/// <summary>
		/// Identity�t�B�[���h�̏ꍇ�� Seed�l
		/// </summary>
		public  decimal	IncSeed	
		{
			get { return this.incSeed; }
			set { this.incSeed = value; }
		}

		private decimal incStep = 0;
		/// <summary>
		/// Identity �t�B�[���h�̏ꍇ�� Step�l
		/// </summary>
		public decimal IncStep 
		{
			get { return this.incStep; }
			set { this.incStep = value; }
		}


		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public DBFieldInfo()
		{
		}
	}
}
