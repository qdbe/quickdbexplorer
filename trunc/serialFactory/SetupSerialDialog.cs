using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace serialFactory
{
	/// <summary>
	/// SetupSerialDialog �̊T�v�̐����ł��B
	/// </summary>
	public class SetupSerialDialog : System.Windows.Forms.Form
	{

		public SerialManager	smanager;

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnRegist;
		private System.Windows.Forms.Button btnTempUse;
		private System.Windows.Forms.Button btnAbort;
		/// <summary>
		/// �K�v�ȃf�U�C�i�ϐ��ł��B
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SetupSerialDialog()
		{
			//
			// Windows �t�H�[�� �f�U�C�i �T�|�[�g�ɕK�v�ł��B
			//
			InitializeComponent();

			//
			// TODO: InitializeComponent �Ăяo���̌�ɁA�R���X�g���N�^ �R�[�h��ǉ����Ă��������B
			//
		}

		/// <summary>
		/// �g�p����Ă��郊�\�[�X�Ɍ㏈�������s���܂��B
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows �t�H�[�� �f�U�C�i�Ő������ꂽ�R�[�h 
		/// <summary>
		/// �f�U�C�i �T�|�[�g�ɕK�v�ȃ��\�b�h�ł��B���̃��\�b�h�̓��e��
		/// �R�[�h �G�f�B�^�ŕύX���Ȃ��ł��������B
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SetupSerialDialog));
			this.label1 = new System.Windows.Forms.Label();
			this.btnRegist = new System.Windows.Forms.Button();
			this.btnTempUse = new System.Windows.Forms.Button();
			this.btnAbort = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(28, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(294, 26);
			this.label1.TabIndex = 0;
			this.label1.Text = "�V���A���L�[�͂܂��o�^����Ă��܂���B�o�^���܂���?";
			// 
			// btnRegist
			// 
			this.btnRegist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnRegist.Location = new System.Drawing.Point(28, 54);
			this.btnRegist.Name = "btnRegist";
			this.btnRegist.Size = new System.Drawing.Size(116, 28);
			this.btnRegist.TabIndex = 1;
			this.btnRegist.Text = "�o�^����(&R)";
			this.btnRegist.Click += new System.EventHandler(this.btnRegist_Click);
			// 
			// btnTempUse
			// 
			this.btnTempUse.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnTempUse.Location = new System.Drawing.Point(174, 54);
			this.btnTempUse.Name = "btnTempUse";
			this.btnTempUse.Size = new System.Drawing.Size(120, 28);
			this.btnTempUse.TabIndex = 2;
			this.btnTempUse.Text = "���p����";
			this.btnTempUse.Click += new System.EventHandler(this.btnTempUse_Click);
			// 
			// btnAbort
			// 
			this.btnAbort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAbort.Location = new System.Drawing.Point(332, 56);
			this.btnAbort.Name = "btnAbort";
			this.btnAbort.Size = new System.Drawing.Size(160, 26);
			this.btnAbort.TabIndex = 3;
			this.btnAbort.Text = "�A�v���P�[�V�����𒆒f����(&X)";
			this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
			// 
			// SetupSerialDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(510, 96);
			this.Controls.Add(this.btnAbort);
			this.Controls.Add(this.btnTempUse);
			this.Controls.Add(this.btnRegist);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "SetupSerialDialog";
			this.Text = "�V���A���L�[�o�^�m�F";
			this.Load += new System.EventHandler(this.SetupSerialDialog_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void SetupSerialDialog_Load(object sender, System.EventArgs e)
		{
			Random rdm = new Random(unchecked((int)DateTime.Now.Ticks));
			char shortcutkey = 'S';
			for( ;; )
			{
				int moji = rdm.Next(100);
				moji = moji % 26;
				shortcutkey = (char)('A' + moji);
				if( shortcutkey != 'R' &&
					shortcutkey != 'A' )
				{
					break ;
				}
			}
			this.btnTempUse.Text = "���p����(&" + shortcutkey.ToString() + ")";
		}

		private void btnRegist_Click(object sender, System.EventArgs e)
		{
			// �g�p�����̊m�F
			KeikokuDlg kdlg = new KeikokuDlg();
			if( kdlg.ShowDialog() != DialogResult.OK )
			{
				this.Close();
				return;
			}

			RegisterSerialDialog dlg = new RegisterSerialDialog();
			dlg.smanager = this.smanager;
			DialogResult ret = dlg.ShowDialog();
			if( ret == DialogResult.OK)
			{
				// �o�^����
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
		}

		private void btnAbort_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Abort;
			this.Close();
		}

		private void btnTempUse_Click(object sender, System.EventArgs e)
		{
			// �g�p�����̊m�F
			KeikokuDlg kdlg = new KeikokuDlg();
			if( kdlg.ShowDialog() != DialogResult.OK )
			{
				this.Close();
				return;
			}
			// ���p�ׂ̈̊m�F�_�C�A���O��\������
			ryoshinDialog rdlg = new ryoshinDialog();
			if( DialogResult.OK != rdlg.ShowDialog())
			{
				this.DialogResult = DialogResult.Abort;
				this.Close();
				return ;
			}
			ChosakuKenDialog cdlg = new ChosakuKenDialog();
			if( DialogResult.OK != cdlg.ShowDialog())
			{
				this.DialogResult = DialogResult.Abort;
				this.Close();
				return ;
			}

			// ���p�m�FOK
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
