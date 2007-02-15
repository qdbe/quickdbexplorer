using System;
using System.Windows.Forms;

namespace quickDBExplorer
{
	/// <summary>
	/// qdbeListBox �̊T�v�̐����ł��B
	/// </summary>
	public class qdbeListBox : System.Windows.Forms.ListBox
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

		private bool isAllSelecting = false;

		public bool IsAllSelecting 
		{
			get { return this.isAllSelecting; }
		}

		public qdbeListBox() : base()
		{
			// 
			// TODO: �R���X�g���N�^ ���W�b�N�������ɒǉ����Ă��������B
			//
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if( (int)keyData == ( (int)Keys.Control + (int)Keys.C ) )
			{
				if( this.CopyData != null )
				{
					this.CopyData(this);
				}
				return true;
			}
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


	}
}
