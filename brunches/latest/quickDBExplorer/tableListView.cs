using System;
using System.Windows.Forms;

namespace quickDBExplorer
{
	/// <summary>
	/// tableListView �̊T�v�̐����ł��B
	/// </summary>
	public class tableListView : qdbeListView
	{
		public tableListView()
		{
			// 
			// TODO: �R���X�g���N�^ ���W�b�N�������ɒǉ����Ă��������B
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
