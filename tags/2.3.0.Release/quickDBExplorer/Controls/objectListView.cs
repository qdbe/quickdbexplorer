using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;


namespace quickDBExplorer
{
	/// <summary>
	/// Table��DB�I�u�W�F�N�g�̈ꗗ�\����p�N���X
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public class ObjectListView : qdbeListView
	{
        private List<ListViewItem> currentItems;

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public ObjectListView()
		{
            IsAutoColumSort = true;
            this.currentItems = new List<ListViewItem>();
		}

		/// <summary>
		/// �ꗗ�ɒǉ�����A�C�e�����쐬�쐬����
		/// </summary>
		/// <param name="dr">�f�[�^���Q�Ƃ���DataReader</param>
		/// <param name="dataSetter">�I�u�W�F�N�g�̏ڍׂ�ݒ肷�郁�\�b�h</param>
		/// <returns></returns>
		private ListViewItem	CreateItem(IDataRecord dr, DataGetEventHandler dataSetter)
		{
			DBObjectInfo dboInfo = new DBObjectInfo(
                (int)dr["objectid"],
				(string)dr["tvs"],
				(string)dr["uname"],
				(string)dr["tbname"],
				(string)((DateTime)dr["cretime"]).ToString("yyyy/MM/dd HH:mm:ss"),
				(string)dr["synbase"],
				(string)dr["synType"]
				);
			dboInfo.DataGet	+= dataSetter;

			ListViewItem it = new ListViewItem( new string[]{ 
																dboInfo.DisplayObjType,
																dboInfo.Owner,
																dboInfo.ObjName,
																dboInfo.CreateTime
															}
				);
			it.Tag = dboInfo;
			return it;
		}

        /// <summary>
        /// DataReader ���� �I�u�W�F�N�g���X�g�̊e���ڂ��쐬���ݒ肷��
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="sqlInterface"></param>
        public void SetObjectListItem(IDataReader dr, ISqlInterface sqlInterface)
        {
            this.BeginUpdate();
            this.Items.Clear();
            this.currentItems.Clear();
            DataGetEventHandler datahandler = sqlInterface.ObjectDetailSet();

            while (dr.Read())
            {
                currentItems.Add(this.CreateItem(dr, datahandler));
            }
            this.Items.AddRange(currentItems.ToArray());
            this.EndUpdate();
        }

        /// <summary>
        /// ���X�g�̓��e���t�B���^�����O���ĕ\������
        /// </summary>
        /// <param name="filterText"></param>
        public void FilterObjectList(string filterText)
        {
            this.BeginUpdate();
            this.Items.Clear();
            if (filterText == string.Empty)
            {
                this.Items.AddRange(currentItems.ToArray());
                this.EndUpdate();
                return;
            }
            List<ListViewItem> newlist = new List<ListViewItem>();
            foreach (ListViewItem itm in currentItems)
            {
                if (((DBObjectInfo)itm.Tag).ObjName.Contains(filterText))
                {
                    newlist.Add(itm);
                }
            }

            this.Items.AddRange(newlist.ToArray());
            this.EndUpdate();
        }


		/// <summary>
		/// �P��I������Ă���I�u�W�F�N�g�����擾����([]�Ȃ�)
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
		/// �P��I������Ă���I�u�W�F�N�g�����擾����([]����)
		/// </summary>
		/// <returns></returns>
		public string GetSelectOneObjectFormalName()
		{
			if( this.SelectedItems.Count == 1 )
			{
				return ((DBObjectInfo)this.SelectedItems[0].Tag).FormalName;
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
        /// �I�����ꂽ���ڂ̂����A�I�����ꂽ�I�u�W�F�N�g�̏����擾����
        /// </summary>
        /// <returns></returns>
        public List<DBObjectInfo> GetSelectObjects()
        {
            if (this.SelectedItems.Count == 0)
            {
                return new List<DBObjectInfo>();
            }
            List<DBObjectInfo> selectList = new List<DBObjectInfo>();
            foreach(ListViewItem each in this.SelectedItems)
            {
                selectList.Add((DBObjectInfo)each.Tag);
            }
            return selectList;
        }

        /// <summary>
        /// �w�肳�ꂽ�I�u�W�F�N�g��I����Ԃɂ���
        /// </summary>
        /// <param name="list"></param>
        public void SetSelectedObjects(List<DBObjectInfo> list)
        {
            if( list == null || list.Count == 0 || this.Items.Count == 0 )
            {
                return;
            }
            this.BeginUpdate();
            this.ClearSelected();
            int idx = 0;
            foreach (DBObjectInfo each in list)
            {
                idx = this.FindStringExact(each.FormalName);
                if (idx >= 0)
                {
                    this.Items[idx].Selected = true;
                }
            }

            if (idx >= 0)
            {
                this.EnsureVisible(idx);
            }
            
            this.EndUpdate();
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
		/// �w�肵���l�ƑS���������̂����L�[����������
		/// .NET �W���ƈႢ�A�啶���E����������ʂ���
		/// </summary>
		/// <param name="itemkey"></param>
		/// <returns></returns>
		public override int FindStringExact(string itemkey)
		{
			for( int i = 0; i < this.Items.Count; i++ )
			{
				if( (string)this.Items[i].Tag.ToString() == itemkey ||
					((DBObjectInfo)this.Items[i].Tag).FormalName == itemkey
					)
				{
					return i;
				}
			}
			return -1;
		}

        /// <summary>
        /// �I�u�W�F�N�g�݂̂̃\�[�g���@�ɃZ�b�g����
        /// </summary>
        public void SetSortSchemaObject()
        {
            this.SortOrder = new ColumnSortOrder[] {
																		  new ColumnSortOrder(1,true),
																		  new ColumnSortOrder(2,true)
																	  };
        }

        /// <summary>
        /// �X�L�[�}�ƃI�u�W�F�N�g�̃\�[�g���@�ɃZ�b�g����
        /// </summary>
        public void SetSortObjectOnly()
        {
            this.SortOrder = new ColumnSortOrder[] {
																		  new ColumnSortOrder(2,true)
																	  };
        }
	}
}
