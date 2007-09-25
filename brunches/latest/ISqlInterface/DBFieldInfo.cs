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
		public int		Colid
		{
			get { return this.colid; }
			set { this.colid = value; }
		}

		private	int		colorder = 0;
		public	int		Colorder
		{
			get { return this.colorder; }
			set { this.colorder = value; }
		}

		public	bool		IsNullable
		{
			get { return this.col.AllowDBNull; }
		}

		private	string	collation = string.Empty;
		public	string	Collation
		{
			get { return this.collation; }
			set { this.collation = value; }
		}

		private	int	primaryKeyOrder = -1;
		public	int	PrimaryKeyOrder
		{
			get { return this.primaryKeyOrder; }
			set { this.primaryKeyOrder = value; }
		}

		private decimal	incSeed = 0;
		public  decimal	IncSeed	
		{
			get { return this.incSeed; }
			set { this.incSeed = value; }
		}

		private decimal incStep = 0;
		public decimal IncStep 
		{
			get { return this.incStep; }
			set { this.incStep = value; }
		}


		public DBFieldInfo()
		{
			// 
			// TODO: �R���X�g���N�^ ���W�b�N�������ɒǉ����Ă��������B
			//
		}
	}
}
