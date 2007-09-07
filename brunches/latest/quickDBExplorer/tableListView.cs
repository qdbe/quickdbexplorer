using System;
using System.Windows.Forms;

namespace quickDBExplorer
{
	/// <summary>
	/// tableListView の概要の説明です。
	/// </summary>
	public class tableListView : qdbeListView
	{
		public tableListView()
		{
			// 
			// TODO: コンストラクタ ロジックをここに追加してください。
			//
		}

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

		public string GetSelectOneTableName()
		{
			if( this.SelectedItems.Count == 1 )
			{
				return this.SelectedItems[0].Tag.ToString();
			}
			return string.Empty;
		}
	}
}
