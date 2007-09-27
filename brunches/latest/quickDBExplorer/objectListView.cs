using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Windows.Forms;

namespace quickDBExplorer
{
	/// <summary>
	/// Table��DB�I�u�W�F�N�g�̈ꗗ�\����p�N���X
	/// </summary>
	public class ObjectListView : qdbeListView
	{
		/// <summary>
		/// �ꗗ�̃\�[�g�����w�肷��
		/// </summary>
		public	ObjectColumnSortOrder	[]SortOrder
		{
			get { return this.sorter.SortOrder; }
			set { this.sorter.SortOrder = value; }
		}

		private tableSorter sorter;

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public ObjectListView()
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
		/// <param name="dr">�f�[�^���Q�Ƃ���DataReader</param>
		/// <param name="dataSetter">�I�u�W�F�N�g�̏ڍׂ�ݒ肷�郁�\�b�h</param>
		/// <returns></returns>
		public ListViewItem	CreateItem(IDataReader dr, DBObjectInfo.DataGetEventHandler dataSetter)
		{
			DBObjectInfo dboInfo = new DBObjectInfo(
				(string)dr["tvs"],
				(string)dr["uname"],
				(string)dr["tbname"],
				(string)dr["cretime"].ToString(),
				(string)dr["synbase"],
				(string)dr["syntype"]
				);
			dboInfo.DataGet	+= dataSetter;

			ListViewItem it = new ListViewItem( new string[]{ 
																dboInfo.DspObjType,
																dboInfo.Owner,
																dboInfo.ObjName,
																dboInfo.CreateTime
															}
				);
			it.Tag = dboInfo;
			return it;
		}

		/// <summary>
		/// �P��I������Ă���e�[�u�������擾����
		/// </summary>
		/// <returns></returns>
		public string GetSelectOneObjectName()
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
		public string GetSelectObjectName(int i)
		{
			if( this.SelectedItems.Count == 0 )
			{
				return "";
			}
			return this.SelectedItems[i].Tag.ToString();
		}

		/// <summary>
		/// �I�����ꂽ���ڂ̂����A�w�肳�ꂽ�I�u�W�F�N�g���̏����擾����
		/// </summary>
		/// <param name="i"></param>
		/// <returns></returns>
		public DBObjectInfo	GetSelectObject(int i)
		{
			if( this.SelectedItems.Count == 0 )
			{
				return null;
			}
			return (DBObjectInfo)this.SelectedItems[i].Tag;
		}

		/// <summary>
		/// �ꗗ�őI������Ă���I�u�W�F�N�g�̏����Ď擾���Ȃ���
		/// </summary>
		public void	ReloadSelectObjectInfo()
		{
			if( this.SelectedItems.Count == 0 )
			{
				return;
			}
			for( int i = 0; i < this.SelectedItems.Count; i++ )
			{
				((DBObjectInfo)this.SelectedItems[i].Tag).ReloadInfo();
			}
		}

		/// <summary>
		/// �J�����w�b�_���N���b�N���ꂽ���Ƃɂ��\�[�g��������������
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
					// �����J�������w�肳�ꂽ�ꍇ�́A�\�[�g�����t�ɂ���
					this.sorter.SortOrder[0].SortAsc = !this.sorter.SortOrder[0].SortAsc;
				}
				else
				{
					// �ʂ̃J�������w�肳�ꂽ�ꍇ�́A���̃J�������\�[�g�L�[�ɂ��čĐݒ�
					this.sorter.SortOrder = new ObjectColumnSortOrder[] {
																		   new ObjectColumnSortOrder(col,true)
																	   };
				}
			}
			this.Sort();

		}
	}

	/// <summary>
	/// �I�u�W�F�N�g�̃\�[�g�����Ǘ�����ׂ̃N���X
	/// </summary>
	public class	ObjectColumnSortOrder
	{
		private	int	column = 0;
		private	bool	sortAsc = true;

		/// <summary>
		/// �\�[�g����J�����̈ʒu
		/// </summary>
		public int Column
		{
			get { return this.column; }
			set { this.column = value; }
		}

		/// <summary>
		/// �\�[�g�������ōs�����A�~���ōs�����̎w��
		/// </summary>
		public bool	SortAsc 
		{
			get { return this.sortAsc; }
			set { this.sortAsc = value; }
		}

		/// <summary>
		/// �R���X�g���N�^
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
	/// ObjectListView �̓����ŗ��p����
	/// sort() ���\�b�h���{���̃\�[�g���𐧌䂷��ׂ̔�r�p�N���X
	/// </summary>
	class tableSorter : IComparer
	{
		private ObjectColumnSortOrder[] sortOrder;

		/// <summary>
		/// �\�[�g�����擾�E�ݒ肷��
		/// </summary>
		public ObjectColumnSortOrder[] SortOrder
		{
			get { return this.sortOrder; }
			set { this.sortOrder = value; }
		}

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public tableSorter()
		{
			this.sortOrder = new ObjectColumnSortOrder[] { 
															new ObjectColumnSortOrder(2,true)
														};
		}

		/// <summary>
		/// ���ۂ̔�r���\�b�h
		/// </summary>
		/// <param name="x">��r��</param>
		/// <param name="y">��r��</param>
		/// <returns>1,0,-1�̂��Âꂩ</returns>
		public int Compare(object x, object y)
		{
			ListViewItem xx;
			ListViewItem yy;
			xx = (ListViewItem)x;
			yy = (ListViewItem)y;

			int		ret = 0;

			// ��r�ΏۂƂ��Đݒ肳��Ă���J���������A�Ⴄ���ʂɂȂ�܂Ŕ�r���s��
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
