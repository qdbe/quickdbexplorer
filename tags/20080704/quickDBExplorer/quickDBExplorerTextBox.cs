using System;
using System.Windows.Forms;
using System.Windows;
using System.Text.RegularExpressions;

namespace quickDBExplorer
{
	/// <summary>
	/// CTRL+A�ł̑S�I���@�\�ACTRL+D�ł̕����폜��ǉ������e�L�X�g�{�b�N�X
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
			this.Enter +=new EventHandler(quickDBExplorerTextBox_Enter);
			this.TextChanged +=new EventHandler(quickDBExplorerTextBox_TextChanged);
		}

		/// <summary>
		/// ���l�̂ݓ��͉\�ɂ��邩�ۂ�
		/// </summary>
		private bool pIsDigitOnly = false;

		/// <summary>
		/// ���l�̂ݓ��͉\�ɂ��邩�ۂ�
		/// false: ���l�ȊO�����͉\
		/// true: ���l�̂݉\
		/// </summary>
		public bool IsDigitOnly
		{
			get { return this.pIsDigitOnly; }
			set { this.pIsDigitOnly = value; }
		}

		private bool pIsCTRLDelete = true;
		
		/// <summary>
		/// CTRL+D�ŕ������폜�\�ɂ��邩�ۂ�
		/// false: �폜�ł��Ȃ�
		/// true: �폜�ł���(����l)
		/// </summary>
		public bool IsCTRLDelete
		{
			get { return this.pIsCTRLDelete; }
			set { this.pIsCTRLDelete = value; }
		}

		// �ύX�O�̃e�L�X�g
		private string orgString;

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

			if( this.pIsCTRLDelete == true &&
				ev.Alt != true &&
				ev.Control == true &&
				ev.Shift != true &&
				ev.KeyCode == Keys.D )
			{
				this.Text = string.Empty;
			}
		}

		private void quickDBExplorerTextBox_Enter(object sender, EventArgs e)
		{
			// �ҏW�J�n���_�̕�������L��
			this.orgString = this.Text;
		}

		private void quickDBExplorerTextBox_TextChanged(object sender, EventArgs e)
		{
			string nowText = this.Text;
			if( this.pIsDigitOnly == true && nowText != string.Empty)
			{
				// ���l�݂̂̏ꍇ�A�����ύX�̌��� �����̂ݎc���Ă��邩�ǂ������`�F�b�N����
				if( Regex.IsMatch(nowText,"^[0-9]+$") == false )
				{
					// ��v���Ȃ��ꍇ�͌��ɖ߂��Ă��
					this.Text = this.orgString;
				}
				else
				{
					// ���݂̒l���ŐV�̕�����Ƃ��ċL�^���Ă����A��r�ޗ��ƃX��
					this.orgString = this.Text;
				}
			}
			else
			{
				this.orgString = this.Text;
			}
		}
	}
}
