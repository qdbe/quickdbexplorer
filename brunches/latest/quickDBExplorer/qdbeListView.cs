using System;
using System.Windows.Forms;


namespace quickDBExplorer
{
	/// <summary>
	/// qdbeListView �̊T�v�̐����ł��B
	/// </summary>
	public class qdbeListView : ListView
	{

		/// <summary>
		/// Ctrl+C �������� �f���Q�[�g
		/// </summary>
		public delegate void CopyDataHandler(
			object sender
			);

		/// <summary>
		/// Ctrl+C�������ꂽ�ꍇ�̃C�x���g
		/// </summary>
		public event CopyDataHandler CopyData = null;

		/// <summary>
		/// Ctrl + F �������̃f���Q�[�g
		/// </summary>
		public delegate void ExTendedCopyDataHandler(
			object sender
			);

		/// <summary>
		/// Ctrl+F �������̃C�x���g
		/// </summary>
		public event ExTendedCopyDataHandler ExtendedCopyData = null;


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
		public qdbeListView()
		{
			// 
			// TODO: �R���X�g���N�^ ���W�b�N�������ɒǉ����Ă��������B
			//
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
					this.CopyData(this);
				}
				return true;
			}
			// CTRL+F�Ŋg���R�s�[����
			if( (int)keyData == ( (int)Keys.Control + (int)Keys.F ) )
			{
				if( this.ExtendedCopyData != null )
				{
					this.ExtendedCopyData(this);
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
			}
			this.EndUpdate();
		}

		/// <summary>
		/// �w�肵���l�ƑS���������̂����L�[����������
		/// .NET �W���ƈႢ�A�啶���E����������ʂ���
		/// </summary>
		/// <param name="itemkey"></param>
		/// <returns></returns>
		public int FindStringExact(string itemkey)
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
	}
}
