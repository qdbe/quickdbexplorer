using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace quickDBExplorer
{
	/// <summary>
	/// select ������͂���ׂ̃_�C�A���O
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public class QueryDialogSelect : quickDBExplorer.QueryDialog
	{
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public QueryDialogSelect()
		{
			// ���̌Ăяo���� Windows �t�H�[�� �f�U�C�i�ŕK�v�ł��B
			InitializeComponent();

			// TODO: InitializeComponent �Ăяo���̌�ɏ�����������ǉ����܂��B
		}

		/// <summary>
		/// �g�p����Ă��郊�\�[�X�Ɍ㏈�������s���܂��B
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region �f�U�C�i�Ő������ꂽ�R�[�h
		/// <summary>
		/// �f�U�C�i �T�|�[�g�ɕK�v�ȃ��\�b�h�ł��B���̃��\�b�h�̓��e��
		/// �R�[�h �G�f�B�^�ŕύX���Ȃ��ł��������B
		/// </summary>
		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// txtInput
			// 
			this.txtInput.Location = new System.Drawing.Point(16, 12);
			this.txtInput.Size = new System.Drawing.Size(444, 262);
			// 
			// btnGo
			// 
			this.btnGo.Location = new System.Drawing.Point(10, 280);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(342, 280);
			// 
			// chkReturn
			// 
			this.chkReturn.Location = new System.Drawing.Point(240, 288);
			this.chkReturn.Size = new System.Drawing.Size(96, 16);
			this.chkReturn.Visible = false;
			// 
			// btnHistory
			// 
			this.btnHistory.Location = new System.Drawing.Point(138, 280);
			// 
			// QueryDialogSelect
			// 
			this.ClientSize = new System.Drawing.Size(480, 317);
			this.Name = "QueryDialogSelect";
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion
	}
}

