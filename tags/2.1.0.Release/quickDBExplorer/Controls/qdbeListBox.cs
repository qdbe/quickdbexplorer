using System;
using System.Collections;
using System.Windows.Forms;

namespace quickDBExplorer
{
	/// <summary>
	/// Ctrl+C �������� �f���Q�[�g
	/// </summary>
	public delegate void CopyDataEventHandler(
	object sender,
	System.EventArgs e
	);

	/// <summary>
	/// Ctrl + F �������̃f���Q�[�g
	/// </summary>
	public delegate void ExtendedCopyDataEventHandler(
	object sender,
	System.EventArgs e
	);

	/// <summary>
	/// ���X�g�{�b�N�X�̊g���@�\��
	/// �L�[�������̓��ꏈ��(delegate�ł̃C�x���g�n���h���Ăяo������)���g�ݍ���ł���
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public class qdbeListBox : System.Windows.Forms.ListBox
	{


		/// <summary>
		/// Ctrl+C�������ꂽ�ꍇ�̃C�x���g
		/// </summary>
		public event CopyDataEventHandler CopyData = null;


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
		/// �R���X�g���N�^
		/// </summary>
		public qdbeListBox() : base()
		{
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
				if( this.SelectionMode != SelectionMode.MultiExtended &&
					this.SelectionMode != SelectionMode.MultiSimple )
				{
					return true;
				}
				this.isAllSelecting = true;
				this.BeginUpdate();
				for( int i = 0; i <this.Items.Count; i++ )
				{
					this.SetSelected(i, true);
				}
				this.EndUpdate();
				this.isAllSelecting = false;

				this.OnSelectedIndexChanged (new System.EventArgs());
				return true;
			}
			return base.ProcessCmdKey (ref msg, keyData);
		}

        /// <summary>
        /// �I�����ꂽ�A�C�e���̈ꗗ��Ԃ�
        /// </summary>
        /// <returns></returns>
        public string[] GetSelectedItemAsStringArray()
        {
            ArrayList ar = new ArrayList();
            foreach(object each in this.SelectedItems)
            {
                ar.Add(each.ToString());
            }
            return (string[])ar.ToArray(typeof(string));
        }

        /// <summary>
        /// �w�肵���A�C�e����I����Ԃɂ���
        /// </summary>
        /// <param name="itemlist"></param>
        public void SetSelectedItems(string[] itemlist)
        {
            this.ClearSelected();
            for (int i = 0; i < itemlist.Length; i++)
            {
                int idx = this.FindString(itemlist[i]);
                if (idx >= 0)
                {
                    this.SetSelected(idx, true);
                }
            }
        }
	}
}
