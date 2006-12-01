using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;
using System.Diagnostics;


namespace serialFactory
{
	/// <summary>
	/// ChosakuKenDialog �̊T�v�̐����ł��B
	/// </summary>
	public class ChosakuKenDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnAbort;
		/// <summary>
		/// �K�v�ȃf�U�C�i�ϐ��ł��B
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ChosakuKenDialog()
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
			this.label1 = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnAbort = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(128)));
			this.label1.Location = new System.Drawing.Point(22, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(382, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = "���Ȃ��͈�@�R�s�[�Ƃ������t�Ƃ��̈Ӗ���m���Ă��܂����H";
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(24, 64);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(134, 24);
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "�͂��A�m���Ă��܂�";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnAbort
			// 
			this.btnAbort.Location = new System.Drawing.Point(230, 64);
			this.btnAbort.Name = "btnAbort";
			this.btnAbort.Size = new System.Drawing.Size(166, 26);
			this.btnAbort.TabIndex = 3;
			this.btnAbort.Text = "�������m��܂���(&X)";
			this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
			// 
			// ChosakuKenDialog
			// 
			this.AcceptButton = this.btnAbort;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(414, 102);
			this.Controls.Add(this.btnAbort);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.label1);
			this.Name = "ChosakuKenDialog";
			this.Text = "�m�F";
			this.Load += new System.EventHandler(this.ChosakuKenDialog_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void ChosakuKenDialog_Load(object sender, System.EventArgs e)
		{
			Random rdm = new Random(unchecked((int)DateTime.Now.Ticks));
			char shortcutkey = 'S';
			for( ;; )
			{
				int moji = rdm.Next(100);
				moji = moji % 26;
				shortcutkey = (char)('A' + moji);
				if( shortcutkey != 'X' )
				{
					break ;
				}
			}

			this.btnOK.Text = "�͂��A�����Ă��܂�(&" + shortcutkey.ToString() + ")";
		
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void btnAbort_Click(object sender, System.EventArgs e)
		{
			System.Diagnostics.Process.Start( "http://www.bsa.or.jp/" );
			this.Close();
		}
	}
}
