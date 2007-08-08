using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace quickDBExplorer
{
	/// <summary>
	/// �e�[�u���ɑ΂���e��R�}���h���s �̃R�}���h���͗p�_�C�A���O
	/// </summary>
	public class CmdInputDialog : quickDBExplorer.QueryDialog
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public CmdInputDialog()
		{
			// ���̌Ăяo���� Windows �t�H�[�� �f�U�C�i�ŕK�v�ł��B
			InitializeComponent();
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dHistory)).BeginInit();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(16, 48);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(444, 218);
			// 
			// button1
			// 
			this.button1.Name = "button1";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(14, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(434, 18);
			this.label1.TabIndex = 4;
			this.label1.Text = "{0}���N�G�����ɋL�ڂ��邱�ƂŁA�I�����Ă���e�[�u�����Ɏ��s���ɕϊ�����܂�";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(14, 28);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(434, 18);
			this.label2.TabIndex = 4;
			this.label2.Text = "��F truncate table {0}";
			// 
			// CmdInputDialog
			// 
			this.AcceptButton = null;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(480, 325);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Name = "CmdInputDialog";
			this.Text = "�e��N�G�����s(�e�[�u������)";
			this.Controls.SetChildIndex(this.label2, 0);
			this.Controls.SetChildIndex(this.button1, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.textBox1, 0);
			((System.ComponentModel.ISupportInitialize)(this.dHistory)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// OK�{�^������������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected override void button1_Click(object sender, EventArgs e)
		{
			if( this.textBox1.Text != "" &&
				this.textBox1.Text.IndexOf("{0}") < 0 )
			{
				return;
			}
			base.button1_Click (sender, e);
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

	}
}

