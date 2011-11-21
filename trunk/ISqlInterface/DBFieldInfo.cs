using	System;
using	System.Data;
using quickDBExplorer.DataType;
using System.Text;

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


        /// <summary>
        /// []付のフィールド名称を取得する
        /// </summary>
        public string FormalName
        {
            get
            {
                return string.Format("[{0}]", this.Name);
            }
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

        /// <summary>
        /// []付の型名称を取得する
        /// </summary>
        public string FormalTypeName
        {
            get
            {
                return string.Format("[{0}]", this.TypeName);
            }
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
                if (!this.IsAssembly)
                {
                    dataType = TypeFactory.Create(value);
                    object retobj = null;
                    string errmsg = "";
                    dataType.TryParse("1", this, ref retobj, ref errmsg);
                }
            }
		}

        private baseType dataType;

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


        /// <summary>
        /// 出力用にデータを加工する
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
        /// フィールドの表示形式を取得する
        /// </summary>
        /// <returns></returns>
        public string GetFieldTypeString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("  ").Append(this.dataType.GetFieldTypeString(this.TypeName, this.length, this.Prec, this.Xscale)).Append(" ");

            if( this.IncSeed != 0)
			{
				sb.AppendFormat(System.Globalization.CultureInfo.CurrentCulture," IDENTITY({0},{1})",
					this.IncSeed,
					this.IncStep );
			}

            if (this.IsNullable == false)
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
        /// DDL用の型定義文字列を取得する
        /// </summary>
        /// <param name="useParentheses"></param>
        /// <returns></returns>
        public string GetDDLTypeString(bool useParentheses)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("  ");
            if (useParentheses)
            {
                sb.Append(this.dataType.GetFieldTypeString(this.FormalTypeName, this.length, this.Prec, this.Xscale));
            }
            else
            {
                sb.Append(this.dataType.GetFieldTypeString(this.TypeName, this.length, this.Prec, this.Xscale));
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
            if (this.IsNullable == false)
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
        /// 文字列をパースしてオブジェクトに変換する
        /// </summary>
        /// <param name="data"></param>
        /// <param name="fieldInfo"></param>
        /// <param name="result"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public bool TryParse(string data, DBFieldInfo fieldInfo, ref object result, ref string errmsg)
        {
            if (this.IsNullable == false && data == string.Empty)
            {
                errmsg = "値の指定が必要です。";
                return false;
            }
            if (this.IsIdentity == true && data != string.Empty)
            {
                errmsg = "自動採番されるので値は指定できません。";
                return false;
            }
            return this.dataType.TryParse(data, fieldInfo, ref result, ref errmsg);
        }
	}
}
