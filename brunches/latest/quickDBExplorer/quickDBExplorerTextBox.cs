using System;
using System.Windows.Forms;
using System.Windows;

namespace quickDBExplorer
{
	/// <summary>
	/// CTRL+A�ł̑S�I���@�\��ǉ������e�L�X�g�{�b�N�X
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public class quickDBExplorerTextBox : TextBox
	{
		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public quickDBExplorerTextBox() : base()
		{
			this.KeyDown += new KeyEventHandler(quickDBExplorerTextBox_KeyDown);
		}

		/// <summary>
		/// �L�[�_�E���C�x���g�n���h��
		/// CTRL+A�ł̑S�I��������ǉ�����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="ev"></param>
		internal virtual void quickDBExplorerTextBox_KeyDown(object sender, KeyEventArgs ev)
		{
			if( ((TextBox)sender).Multiline != true &&
				ev.Alt != true &&
				ev.Control == true &&
				ev.Shift != true &&
				ev.KeyCode == Keys.A )
			{
				this.SelectAll();
				ev.Handled = true;
			}
		}
	}
}
