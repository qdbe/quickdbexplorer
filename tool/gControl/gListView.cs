using System;
using System.Collections;
using System.Collections.Specialized;
using System.Windows.Forms;

namespace gControl
{
	/// <summary>
	/// ListView �����A�`�F�b�N�{�b�N�X�t���E�Ȃ������R�ɐ؂�ւ��\
	/// </summary>
	public class gListView : ListView
	{
		public gListView()
		{
			// 
			// TODO: �R���X�g���N�^ ���W�b�N�������ɒǉ����Ă��������B
			//

			this.View = View.Details;
		}

		System.Collections.Specialized.

		/// <summary>
		/// �I��(�`�F�b�N������܂�)��Ԃɂ��鍀�ڂ�Ԃ�
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
		/// �S�Ă̍��ڂ��w�肳�ꂽ�I���E�`�F�b�N��Ԃɂ���
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
		/// �S�Ă̍��ڂ�I���E�`�F�b�N��Ԃɂ���
		/// </summary>
		public void AllSelect()
		{
			this.AllSelectedSet(true);
		}

		/// <summary>
		/// �S�Ă̍��ڂ��I���E��`�F�b�N��Ԃɂ���
		/// </summary>
		public void AllUnSelect()
		{
			this.AllSelectedSet(false);
		}

		/// <summary>
		/// �w�肳�ꂽ���ڂ̑I���E�`�F�b�N��Ԃ�ݒ肷��
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
		/// �w�肳�ꂽ���ڂ̑I���E�`�F�b�N��Ԃ�ݒ肷��
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
