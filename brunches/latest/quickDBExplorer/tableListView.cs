using System;
using System.Collections;
using System.Windows.Forms;

namespace quickDBExplorer
{
	/// <summary>
	/// Table���̈ꗗ�\����p�N���X
	/// </summary>
	public class tableListView : qdbeListView
	{
		/// <summary>
		/// �ꗗ�̃\�[�g�����w�肷��
		/// </summary>
		public	TableColumnSortOrder	[]SortOrder
		{
			get { return this.sorter.SortOrder; }
			set { this.sorter.SortOrder = value; }
		}

		private tableSorter sorter;

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public tableListView()
		{
			// 
			// TODO: �R���X�g���N�^ ���W�b�N�������ɒǉ����Ă��������B
			//

			sorter = new tableSorter();

			this.ListViewItemSorter = this.sorter;
		}

		/// <summary>
		/// �ꗗ�ɒǉ�����A�C�e�����쐬�쐬����
		/// </summary>
		/// <param name="tvs">�e�[�u���EView�ESchema�̋敪</param>
		/// <param name="owner">�I�u�W�F�N�g�̏��L��</param>
		/// <param name="tbname">�I�u�W�F�N�g��</param>
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
		/// �P��I������Ă���e�[�u�������擾����
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
		/// �I�����ꂽ���ڂ̂����A�w�肳�ꂽ�I�u�W�F�N�g�����擾����
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
		/// �J�����w�b�_���N���b�N���ꂽ���Ƃɂ��\�[�g��������������
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
					// �����J�������w�肳�ꂽ�ꍇ�́A�\�[�g�����t�ɂ���
					this.sorter.SortOrder[0].SortAsc = !this.sorter.SortOrder[0].SortAsc;
				}
				else
				{
					// �ʂ̃J�������w�肳�ꂽ�ꍇ�́A���̃J�������\�[�g�L�[�ɂ��čĐݒ�
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
		/// �\�[�g�����擾�E�ݒ肷��
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
