using System;
using System.Collections;
using System.Windows.Forms;


namespace quickDBExplorer
{
	/// <summary>
	/// qdbeListView �̊T�v�̐����ł��B
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public class qdbeListView : ListView
	{

		/// <summary>
		/// Ctrl+C �������� �f���Q�[�g
		/// </summary>
		public delegate void CopyDataEventHandler(
			object sender,
			System.EventArgs e
			);

		/// <summary>
		/// Ctrl+C�������ꂽ�ꍇ�̃C�x���g
		/// </summary>
		public event CopyDataEventHandler CopyData = null;

		/// <summary>
		/// Ctrl + F �������̃f���Q�[�g
		/// </summary>
		public delegate void ExtendedCopyDataEventHandler(
			object sender,
			System.EventArgs e
			);

		/// <summary>
		/// Ctrl+F �������̃C�x���g
		/// </summary>
		public event ExtendedCopyDataEventHandler ExtendedCopyData = null;


		private bool isAllSelecting = false;

		/// <summary>
		/// �S�Ă̍��ڂ��I������Ă��邩�ۂ�
		/// </summary>
		public bool IsAllSelecting 
		{
			get { return this.isAllSelecting; }
		}

        /// <summary>
        /// �ꗗ�̃\�[�g�����w�肷��
        /// </summary>
        public ColumnSortOrder[] SortOrder
        {
            get { return this.sorter.SortOrder; }
            set { this.sorter.SortOrder = value; }
        }

        private ListSorter sorter;

        /// <summary>
        /// �J�����𗘗p�����\�[�g���\�Ƃ��邩
        /// </summary>
        public bool IsAutoColumSort { get; set; }

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public qdbeListView()
		{
            sorter = new ListSorter();
            IsAutoColumSort = false;
            this.ListViewItemSorter = this.sorter;
		}

		/// <summary>
		/// ����L�[�������C�x���g�n���h��
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="keyData"></param>
		/// <returns></returns>
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			// CTRL+C�ŃR�s�[����
			if( (int)keyData == ( (int)Keys.Control + (int)Keys.C ) )
			{
				if( this.CopyData != null )
				{
					this.CopyData(this, new System.EventArgs());
				}
				return true;
			}
			// CTRL+F�Ŋg���R�s�[����
			if( (int)keyData == ( (int)Keys.Control + (int)Keys.F ) )
			{
				if( this.ExtendedCopyData != null )
				{
					this.ExtendedCopyData(this, new System.EventArgs());
				}
				return true;
			}
			// CTRL+A�őS�I��
			if( (int)keyData == ( (int)Keys.Control + (int)Keys.A ) )
			{
				if( this.MultiSelect != true  )
				{
					return true;
				}
				this.isAllSelecting = true;
				this.BeginUpdate();
				for( int i = 0; i <this.Items.Count; i++ )
				{
					this.Items[i].Selected = true;
				}
				this.EndUpdate();
				this.isAllSelecting = false;

				this.OnSelectedIndexChanged (new System.EventArgs());
				return true;
			}
			return base.ProcessCmdKey (ref msg, keyData);
		}

		/// <summary>
		/// �S�Ă̑I����Ԃ��N���A����
		/// </summary>
		public void ClearSelected()
		{
			this.BeginUpdate();
			for( int i = 0; i < this.Items.Count; i++ )
			{
				this.Items[i].Selected = false;
				this.Items[i].Focused = false;
			}
			this.EndUpdate();
		}

		/// <summary>
		/// �w�肵���l�ƑS���������̂����L�[����������
		/// .NET �W���ƈႢ�A�啶���E����������ʂ���
		/// </summary>
		/// <param name="itemkey"></param>
		/// <returns></returns>
		public virtual int FindStringExact(string itemkey)
		{
			for( int i = 0; i < this.Items.Count; i++ )
			{
				if( (string)this.Items[i].Tag.ToString() == itemkey )
				{
					return i;
				}
			}
			return -1;
		}

        /// <summary>
        /// �J�����w�b�_���N���b�N���ꂽ���Ƃɂ��\�[�g��������������
        /// </summary>
        /// <param name="col"></param>
        public void SetColumClick(int col)
        {
            ColumnSortOrder[] so = this.sorter.SortOrder;
            if (so.Length != 1)
            {
                this.sorter.SortOrder = new ColumnSortOrder[] {
																	   new ColumnSortOrder(col,true)
																   };

            }
            else
            {
                if (this.sorter.SortOrder[0].Column == col)
                {
                    // �����J�������w�肳�ꂽ�ꍇ�́A�\�[�g�����t�ɂ���
                    this.sorter.SortOrder[0].SortAsc = !this.sorter.SortOrder[0].SortAsc;
                }
                else
                {
                    // �ʂ̃J�������w�肳�ꂽ�ꍇ�́A���̃J�������\�[�g�L�[�ɂ��čĐݒ�
                    this.sorter.SortOrder = new ColumnSortOrder[] {
																		   new ColumnSortOrder(col,true)
																	   };
                }
            }
            this.Sort();
        }
        /// <summary>
        /// �J�����w�b�_���N���b�N���ꂽ
        /// </summary>
        /// <param name="e"></param>
        protected override void OnColumnClick(ColumnClickEventArgs e)
        {
            if (!this.IsAutoColumSort) return;

            SetColumClick(e.Column);
        }

	}

    /// <summary>
    /// �I�u�W�F�N�g�̃\�[�g�����Ǘ�����ׂ̃N���X
    /// </summary>
    public class ColumnSortOrder
    {
        private int column = 0;
        private bool sortAsc = true;

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
        public bool SortAsc
        {
            get { return this.sortAsc; }
            set { this.sortAsc = value; }
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="col"></param>
        /// <param name="asc"></param>
        public ColumnSortOrder(int col, bool asc)
        {
            this.column = col;
            this.sortAsc = asc;
        }
    }

    /// <summary>
    /// qdbeListView �̓����ŗ��p����
    /// sort() ���\�b�h���{���̃\�[�g���𐧌䂷��ׂ̔�r�p�N���X
    /// </summary>
    class ListSorter : IComparer
    {
        private ColumnSortOrder[] sortOrder;

        /// <summary>
        /// �\�[�g�����擾�E�ݒ肷��
        /// </summary>
        public ColumnSortOrder[] SortOrder
        {
            get { return this.sortOrder; }
            set { this.sortOrder = value; }
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public ListSorter()
        {
            // �K��̃\�[�g���̎w��
            this.sortOrder = new ColumnSortOrder[] { 
                new ColumnSortOrder(0,true)
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

            int ret = 0;

            // ��r�ΏۂƂ��Đݒ肳��Ă���J���������A�Ⴄ���ʂɂȂ�܂Ŕ�r���s��
            for (int i = 0; i < this.sortOrder.Length && ret == 0; i++)
            {
                if (this.sortOrder[i].SortAsc == true)
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
