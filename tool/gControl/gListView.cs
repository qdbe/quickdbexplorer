using System;
using System.Collections;
using System.Collections.Specialized;
using System.Windows.Forms;

namespace gControl
{
	/// <summary>
	/// ListView だが、チェックボックス付き・なしを自由に切り替え可能
	/// </summary>
	public class gListView : ListView
	{
		public gListView()
		{
			// 
			// TODO: コンストラクタ ロジックをここに追加してください。
			//

			this.View = View.Details;
		}

		System.Collections.Specialized.

		/// <summary>
		/// 選択(チェックありを含む)状態にある項目を返す
		/// </summary>
		/// <returns></returns>
		public ListView.SelectedListViewItemCollection GetSelectItem()
		{
			ListViewItemCollection retitem = new ListViewItemCollection(this);
			if( this.CheckBoxes == true )
			{
				foreach( ListViewItem it in this.Items )
				{
					if( it.Checked == true )
					{
						retitem.Add(it);
					}
				}
			}
			else
			{
				foreach( ListViewItem it in  this.SelectedItems)
				{
					retitem.Add(it);
				}
			}
			return retitem;
		}

		/// <summary>
		/// 全ての項目を指定された選択・チェック状態にする
		/// </summary>
		/// <param name="isSet"></param>
		public void AllSelectedSet(bool isSet)
		{
			if( this.CheckBoxes == true )
			{
				foreach( ListViewItem it in this.Items )
				{
					it.Checked = isSet;
				}
			}
			else
			{
				foreach( ListViewItem it in this.Items )
				{
					it.Selected = isSet;
				}
			}
		}

		/// <summary>
		/// 全ての項目を選択・チェック状態にする
		/// </summary>
		public void AllSelect()
		{
			this.AllSelectedSet(true);
		}

		/// <summary>
		/// 全ての項目を非選択・非チェック状態にする
		/// </summary>
		public void AllUnSelect()
		{
			this.AllSelectedSet(false);
		}

		/// <summary>
		/// 指定された項目の選択・チェック状態を設定する
		/// </summary>
		/// <param name="it"></param>
		/// <param name="isSet"></param>
		public void	SetSelectState(ListViewItem it, bool isSet)
		{
			if( this.CheckBoxes == true )
			{
				it.Checked = isSet;
			}
			else
			{
				it.Selected = isSet;
			}
		}

		/// <summary>
		/// 指定された項目の選択・チェック状態を設定する
		/// </summary>
		/// <param name="it"></param>
		/// <param name="isSet"></param>
		public void SetSleectState(int no, bool isSet)
		{
			if( this.CheckBoxes == true )
			{
				this.Items[no].Checked = isSet;
			}
			else
			{
				this.Items[no].Selected = isSet;
			}
		}

		public void SetSelectState(string key, bool isSet)
		{
			if( this.CheckBoxes == true )
			{
				foreach( ListViewItem it in this.Items )
				{
					if( it.SubItems.Count == 0 )
					{
						if( it.Text == key )
						{
							it.Checked = isSet;
						}
					}
					else
					{
						foreach( ListViewItem.ListViewSubItem sit in it.SubItems )
						{
							if( sit.Text == key )
							{
								it.Checked = isSet;
							}
						}
					}
				}
			}
			else
			{
			}
		}
	}
}
