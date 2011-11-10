using	System;
using	System.Data;
using quickDBExplorer.DataType;

namespace quickDBExplorer
{
	/// <summary>
	/// テーブル等のフィールド項目の情報を管理するクラス
	/// </summary>
	public class DBFieldInfo
	{

		private	DataColumn	col;
		/// <summary>
		/// 対応するDataColumn
		/// </summary>
		public	DataColumn	Col
		{
			get	{ return this.col; }
			set { this.col = value; }
		}

		/// <summary>
		/// フィールド名
		/// </summary>
		public string	Name
		{
			get { return this.col.ColumnName; }
		}

		private	string	typeName = "";
		/// <summary>
		/// フィールドの型
		/// </summary>
		public  string	TypeName
		{
			get { return this.typeName; }
			set { this.typeName = value; }
		}

		private string pRealTypeName = "";

		/// <summary>
		/// システムの型名
		/// </summary>
		public string RealTypeName
		{
			get { return pRealTypeName; }
			set { 
                pRealTypeName = value;
                dataType = TypeFactory.Create(value);
            }
		}

        private IDataType dataType;

		private	int		length = 0;
		/// <summary>
		/// フィールドの最大長(文字列の場合)
		/// </summary>
		public	int		Length
		{
			get { return this.length; }
			set { this.length = value; }
		}

		private	int		prec = 0;
		/// <summary>
		/// 小数点の場合の整数値の最大桁数
		/// </summary>
		public	int		Prec
		{
			get { return this.prec; }
			set { this.prec = value; }
		}

		private	int		xscale = 0;
		/// <summary>
		/// 小数点の場合の小数点値の最大桁数
		/// </summary>
		public	int		Xscale
		{
			get { return this.xscale; }
			set { this.xscale = value; }
		}

		private	int		colid = 0;
		/// <summary>
		/// フィールドのカラムIDを管理する
		/// </summary>
		public int		Colid
		{
			get { return this.colid; }
			set { this.colid = value; }
		}

		private	int		colorder = 0;
		/// <summary>
		/// フィールドのカラム順序を管理する
		/// </summary>
		public	int		ColOrder
		{
			get { return this.colorder; }
			set { this.colorder = value; }
		}

		/// <summary>
		/// NULLを許可するか否かを管理する
		/// </summary>
		public	bool		IsNullable
		{
			get { return this.col.AllowDBNull; }
		}

		private	string	collation = string.Empty;
		/// <summary>
		/// 文字フィールドの場合の照合順序を管理する
		/// </summary>
		public	string	Collation
		{
			get { return this.collation; }
			set { this.collation = value; }
		}

		private	int	primaryKeyOrder = -1;
		/// <summary>
		/// プライマリキーの対象になっている場合のプライマリキー内の順序
		/// 開始は0
		/// -1の場合はプライマリキーの要素でないことを指す
		/// </summary>
		public	int	PrimaryKeyOrder
		{
			get { return this.primaryKeyOrder; }
			set { this.primaryKeyOrder = value; }
		}

		private bool pIsIdentity = false;
		/// <summary>
		/// Identity 属性を持つか否か
		/// </summary>
		public bool IsIdentity
		{
			get { return this.pIsIdentity; }
			set { this.pIsIdentity = value; }
		}

		private decimal	incSeed = 0;
		/// <summary>
		/// Identityフィールドの場合の Seed値
		/// </summary>
		public  decimal	IncSeed	
		{
			get { return this.incSeed; }
			set { this.incSeed = value; }
		}

		private decimal incStep = 0;
		/// <summary>
		/// Identity フィールドの場合の Step値
		/// </summary>
		public decimal IncStep 
		{
			get { return this.incStep; }
			set { this.incStep = value; }
		}

		/// <summary>
		/// この型の作成元であるアセンブリの ID です。
		/// </summary>
		private int pAssemblyId = -1;

		/// <summary>
		/// この型の作成元であるアセンブリの ID です。
		/// </summary>
		public int AssemblyId
		{
			get { return this.pAssemblyId; }
			set { this.pAssemblyId = value; }
		}

		/// <summary>
		/// この型を定義しているアセンブリ内のクラスの名前です。
		/// </summary>
		private string pAssemblyClassName = string.Empty;

		/// <summary>
		/// この型を定義しているアセンブリ内のクラスの名前です。
		/// </summary>
		public string AssemblyClassName
		{
			get { return this.pAssemblyClassName; }
			set { this.pAssemblyClassName = value; }
		}

		/// <summary>
		/// アセンブリの修飾された型名です。この名前は、Type.GetType() に渡すのに適した形式になっています。
		/// </summary>
		private string pAssemblyQFN = string.Empty;

		/// <summary>
		/// アセンブリの修飾された型名です。この名前は、Type.GetType() に渡すのに適した形式になっています。
		/// </summary>
		public string AssemblyQFN
		{
			get { return this.pAssemblyQFN; }
			set { this.pAssemblyQFN = value; }
		}

		/// <summary>
		/// オブジェクトがアセンブリを利用しているか否か
		/// </summary>
		public bool IsAssembly
		{
			get 
			{ 
				if( this.pAssemblyQFN != string.Empty )
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
		/// コンストラクタ
		/// </summary>
		public DBFieldInfo()
		{
		}
	}
}
