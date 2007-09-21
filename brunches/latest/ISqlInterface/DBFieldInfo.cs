using System;

namespace ISqlInterface
{
	/// <summary>
	/// テーブル等のフィールド項目の情報を管理するクラス
	/// </summary>
	public class DBFieldInfo
	{
		private string	name = "";
		public string	Name
		{
			get { return this.Name; }
			set { this.Name = value; }
		}

		private	string	typeName = "";
		public  string	TypeName
		{
			get { return this.typeName; }
			set { this.typeName = value; }
		}

		private	int		length = 0;
		public	int		Length
		{
			get { return this.length; }
			set { this.length = 0; }
		}

		private	int		prec = 0;
		public	int		Prec
		{
			get { return this.prec; }
			set { this.prec = value; }
		}

		private	int		xscale = 0;
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

		private	bool	isnullable = false;
		public	int		IsNullable
		{
			get { return this.isnullable; }
			set { this.isnullable = value; }
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

		public DBFieldInfo()
		{
			// 
			// TODO: コンストラクタ ロジックをここに追加してください。
			//
		}
	}
}
