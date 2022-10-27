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

		/// <summary>
		/// 対応するDataColumn
		/// </summary>
        public DataColumn Col { get; set; }

		/// <summary>
		/// フィールド名
		/// </summary>
		public string	Name
		{
			get { return this.Col.ColumnName; }
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

		/// <summary>
		/// フィールドの型
		/// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// []付の型名称を取得する
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
		/// システムの型名
		/// </summary>
		public string RealTypeName
		{
			get { return pRealTypeName; }
			set { 
                pRealTypeName = value;

                // 引数にもらった型がサポート対象かどうかを調べる
                if (!this.IsAssembly)
                {
                    // アセンブリ型以外は対応しているはず
                    dataType = TypeFactory.Create(value);
                    object retobj = null;
                    string errmsg = "";
                    // 引数の型が対応していない場合、DefaultTypeになりエラーとなるはず
                    dataType.TryParse(dataType.DefalutParseString, this, ref retobj, ref errmsg);
                }
            }
		}

        private baseType dataType;

		/// <summary>
		/// フィールドの最大長
		/// </summary>
        public int Length { get; set; }

		/// <summary>
		/// 小数点の場合の整数値の最大桁数
		/// </summary>
        public int Prec { get; set; }

		/// <summary>
		/// 小数点の場合の小数点値の最大桁数
		/// </summary>
        public int Xscale { get; set; }

		/// <summary>
		/// フィールドのカラムIDを管理する
		/// </summary>
        public int Colid { get; set; }

		/// <summary>
		/// フィールドのカラム順序を管理する
		/// </summary>
        public int ColOrder { get; set; }

		/// <summary>
		/// NULLを許可するか否かを管理する
		/// </summary>
		public	bool		IsAllowNull
		{
			get { return this.Col.AllowDBNull; }
		}

        /// <summary>
        /// Nullableか否かを返す
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
		/// 文字フィールドの場合の照合順序を管理する
		/// </summary>
        public string Collation { get; set; }

		/// <summary>
		/// プライマリキーの対象になっている場合のプライマリキー内の順序
		/// 開始は0
		/// -1の場合はプライマリキーの要素でないことを指す
		/// </summary>
        public int PrimaryKeyOrder { get; set; }

		/// <summary>
		/// Identity 属性を持つか否か
		/// </summary>
        public bool IsIdentity { get; set; }

        /// <summary>
        /// バイナリか否か
        /// </summary>
        public bool IsBinary
        {
            get {
                return this.dataType.IsBinary; 
            }
        }

		/// <summary>
		/// Identityフィールドの場合の Seed値
		/// </summary>
        public decimal IncSeed { get; set; }

		/// <summary>
		/// Identity フィールドの場合の Step値
		/// </summary>
		public decimal IncStep { get; set; }

		/// <summary>
		/// この型の作成元であるアセンブリの ID です。
		/// </summary>
        public int AssemblyId { get; set; }

		/// <summary>
		/// この型を定義しているアセンブリ内のクラスの名前です。
		/// </summary>
        public string AssemblyClassName { get; set; }

		/// <summary>
		/// アセンブリの修飾された型名です。この名前は、Type.GetType() に渡すのに適した形式になっています。
		/// </summary>
        public string AssemblyQFN { get; set; }

		/// <summary>
		/// オブジェクトがアセンブリを利用しているか否か
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
        /// 文字データを読み込むことができるか否か
        /// </summary>
        public bool CanLoadData
        {
            get
            {
                return this.dataType.CanLoadData();
            }
        }

        /// <summary>
        /// C# の型名を返す
        /// </summary>
        public string CSharpTypeString
        {
            get
            {
                return this.dataType.GetCSharpTypeString();
            }
        }

		/// <summary>
		/// コンストラクタ
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
        /// 文字列をパースしてオブジェクトに変換する
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
                errmsg = "値の指定が必要です。";
                return false;
            }
            return this.dataType.TryParse(data, fieldInfo, ref result, ref errmsg);
        }

        /// <summary>
        /// 値が正しいかどうかチェックする
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
                errmsg = "値の指定が必要です。";
                return false;
            }
            return this.dataType.CheckValue(data, this, out errmsg);
        }

    }
}
