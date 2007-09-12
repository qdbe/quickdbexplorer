using System;
using System.Collections;
using System.Windows.Forms;

namespace quickDBExplorer
{
	/// <summary>
	/// Table等の一覧表示専用クラス
	/// </summary>
	public class tableListView : qdbeListView
	{
		/// <summary>
		/// 一覧のソート順を指定する
		/// </summary>
		public	TableColumnSortOrder	[]SortOrder
		{
			get { return this.sorter.SortOrder; }
			set { this.sorter.SortOrder = value; }
		}

		private tableSorter sorter;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public tableListView()
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
		/// <returns></returns>
		public ListViewItem	CreateItem(string tvs, string owner, string tbname)
		{

			ListViewItem it = new ListViewItem( new string[]{ 
																tvs,
																owner,
																tbname
															}
				);
			it.Tag = string.Format("{0}.{1}", owner, tbname );
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
			TableColumnSortOrder []so = this.sorter.SortOrder;
			if( so.Length != 1 )
			{
				this.sorter.SortOrder = new TableColumnSortOrder[] {
																	   new TableColumnSortOrder(col,true)
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
					this.sorter.SortOrder = new TableColumnSortOrder[] {
																		   new TableColumnSortOrder(col,true)
																	   };
				}
			}
			this.Sort();

		}
	}

	public class	TableColumnSortOrder
	{
		private	int	column = 0;
		private	bool	sortAsc = true;

		public int Column
		{
			get { return this.column; }
			set { this.column = value; }
		}

		public bool	SortAsc 
		{
			get { return this.sortAsc; }
			set { this.sortAsc = value; }
		}

		public TableColumnSortOrder(int col, bool asc )
		{
			this.column = col;
			this.sortAsc = asc;
		}
	}

	class tableSorter : IComparer
	{
		private TableColumnSortOrder[] sortOrder;

		/// <summary>
		/// ソート順を取得・設定する
		/// </summary>
		public TableColumnSortOrder[] SortOrder
		{
			get { return this.sortOrder; }
			set { this.sortOrder = value; }
		}

		public tableSorter()
		{
			this.sortOrder = new TableColumnSortOrder[] { 
															new TableColumnSortOrder(2,true)
														};
		}
		public int Compare(object x, object y)
		{
			ListViewItem xx;
			ListViewItem yy;
			xx = (ListViewItem)x;
			yy = (ListViewItem)y;

			int		ret = 0;

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
