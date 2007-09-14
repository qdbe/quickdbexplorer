using System;
using System.Collections;
using System.Windows.Forms;

namespace quickDBExplorer
{

	/// <summary>
	/// リストに表示する DB オブジェクトの情報を管理する
	/// </summary>
	public class	DBObjectInfo
	{
		private		string objType;
		/// <summary>
		/// オブジェクトの種類
		/// 空白: Table
		/// V:		View
		/// S:		Synonym
		/// </summary>
		public		string	ObjType
		{
			get { return this.objType; }
			set { this.objType = value; }
		}
		private		string	owner;
		/// <summary>
		/// オブジェクトの所有者名
		/// </summary>
		public	string	Owner
		{
			get { return this.owner; }
			set { this.owner = value; }
		}

		private		string	objname;
		/// <summary>
		/// オブジェクトの名称
		/// </summary>
		public	string ObjName
		{
			get { return this.objname; }
			set { this.objname = value; }
		}

		private string createTime;
		/// <summary>
		/// オブジェクトが生成された日時
		/// </summary>
		public string CreateTime
		{
			get { return this.createTime; }
			set { this.createTime = value; }
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="otype">オブジェクトの型</param>
		/// <param name="owner">オブジェクトの所有者名</param>
		/// <param name="name">オブジェクトの名称</param>
		/// <param name="cretime">オブジェクトの作成日時</param>
		public DBObjectInfo( string otype, string owner, string name, string cretime )
		{
			this.objType = otype;
			this.owner = owner;
			this.objname = name;
			this.createTime = cretime;
		}

		/// <summary>
		/// 文字列化する。
		/// 所有者名+"."+オブジェクト名
		/// を返す
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format("{0}.{1}", this.owner, this.objname );
		}
	}

	/// <summary>
	/// Table等DBオブジェクトの一覧表示専用クラス
	/// </summary>
	public class ObjectListView : qdbeListView
	{
		/// <summary>
		/// 一覧のソート順を指定する
		/// </summary>
		public	ObjectColumnSortOrder	[]SortOrder
		{
			get { return this.sorter.SortOrder; }
			set { this.sorter.SortOrder = value; }
		}

		private tableSorter sorter;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ObjectListView()
		{
			// 
			// TODO: コンストラクタ ロジックをここに追加してください。
			//

			sorter = new tableSorter();

			this.ListViewItemSorter = this.sorter;
		}

		/// <summary>
		/// 一覧に追加するアイテムを作成作成する
		/// </summary>
		/// <param name="tvs">テーブル・View・Schemaの区分</param>
		/// <param name="owner">オブジェクトの所有者</param>
		/// <param name="tbname">オブジェクト名</param>
		/// <param name="cretime">オブジェクトの作成日時</param>
		/// <returns></returns>
		public ListViewItem	CreateItem(string tvs, string owner, string tbname, string cretime)
		{

			ListViewItem it = new ListViewItem( new string[]{ 
																tvs,
																owner,
																tbname,
																cretime
															}
				);
			it.Tag = new DBObjectInfo(tvs,owner,tbname,cretime);
			return it;
		}

		/// <summary>
		/// 単一選択されているテーブル名を取得する
		/// </summary>
		/// <returns></returns>
		public string GetSelectOneTableName()
		{
			if( this.SelectedItems.Count == 1 )
			{
				return this.SelectedItems[0].Tag.ToString();
			}
			return string.Empty;
		}

		/// <summary>
		/// 選択された項目のうち、指定されたオブジェクト名を取得する
		/// </summary>
		/// <param name="i"></param>
		/// <returns></returns>
		public string GetSelectTableName(int i)
		{
			if( this.SelectedItems.Count == 0 )
			{
				return "";
			}
			return this.SelectedItems[i].Tag.ToString();
		}

		/// <summary>
		/// カラムヘッダがクリックされたことによるソート順を書き換える
		/// </summary>
		/// <param name="col"></param>
		public void	SetColumClick(int col)
		{
			ObjectColumnSortOrder []so = this.sorter.SortOrder;
			if( so.Length != 1 )
			{
				this.sorter.SortOrder = new ObjectColumnSortOrder[] {
																	   new ObjectColumnSortOrder(col,true)
																   };

			}
			else
			{
				if( this.sorter.SortOrder[0].Column == col )
				{
					// 同じカラムが指定された場合は、ソート順を逆にする
					this.sorter.SortOrder[0].SortAsc = !this.sorter.SortOrder[0].SortAsc;
				}
				else
				{
					// 別のカラムが指定された場合は、そのカラムをソートキーにして再設定
					this.sorter.SortOrder = new ObjectColumnSortOrder[] {
																		   new ObjectColumnSortOrder(col,true)
																	   };
				}
			}
			this.Sort();

		}
	}

	/// <summary>
	/// オブジェクトのソート順を管理する為のクラス
	/// </summary>
	public class	ObjectColumnSortOrder
	{
		private	int	column = 0;
		private	bool	sortAsc = true;

		/// <summary>
		/// ソートするカラムの位置
		/// </summary>
		public int Column
		{
			get { return this.column; }
			set { this.column = value; }
		}

		/// <summary>
		/// ソートを昇順で行うか、降順で行うかの指定
		/// </summary>
		public bool	SortAsc 
		{
			get { return this.sortAsc; }
			set { this.sortAsc = value; }
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="col"></param>
		/// <param name="asc"></param>
		public ObjectColumnSortOrder(int col, bool asc )
		{
			this.column = col;
			this.sortAsc = asc;
		}
	}

	/// <summary>
	/// ObjectListView の内部で利用する
	/// sort() メソッド実施時のソート順を制御する為の比較用クラス
	/// </summary>
	class tableSorter : IComparer
	{
		private ObjectColumnSortOrder[] sortOrder;

		/// <summary>
		/// ソート順を取得・設定する
		/// </summary>
		public ObjectColumnSortOrder[] SortOrder
		{
			get { return this.sortOrder; }
			set { this.sortOrder = value; }
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public tableSorter()
		{
			this.sortOrder = new ObjectColumnSortOrder[] { 
															new ObjectColumnSortOrder(2,true)
														};
		}

		/// <summary>
		/// 実際の比較メソッド
		/// </summary>
		/// <param name="x">比較元</param>
		/// <param name="y">比較元</param>
		/// <returns>1,0,-1のいづれか</returns>
		public int Compare(object x, object y)
		{
			ListViewItem xx;
			ListViewItem yy;
			xx = (ListViewItem)x;
			yy = (ListViewItem)y;

			int		ret = 0;

			// 比較対象として設定されているカラム数分、違う結果になるまで比較を行う
			for( int i = 0; i < this.sortOrder.Length && ret == 0; i++ )
			{
				if( this.sortOrder[i].SortAsc == true )
				{
					ret = xx.SubItems[this.sortOrder[i].Column].Text.CompareTo(yy.SubItems[this.sortOrder[i].Column].Text);
				}
				else
				{
					ret = yy.SubItems[this.sortOrder[i].Column].Text.CompareTo(xx.SubItems[this.sortOrder[i].Column].Text);
				}
			}
			return ret;
		}
	}

}
