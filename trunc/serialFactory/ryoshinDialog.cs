using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Security.Cryptography;


namespace serialFactory
{
	/// <summary>
	/// ryoshinDialog �̊T�v�̐����ł��B
	/// </summary>
	public class ryoshinDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnAbort;
		private System.Windows.Forms.Button btnOk;
		/// <summary>
		/// �K�v�ȃf�U�C�i�ϐ��ł��B
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ryoshinDialog()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ryoshinDialog));
			this.label1 = new System.Windows.Forms.Label();
			this.btnAbort = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(128)));
			this.label1.Location = new System.Drawing.Point(16, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(338, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = "���Ȃ��ɂ͗ǐS������܂����H";
			// 
			// btnAbort
			// 
			this.btnAbort.Location = new System.Drawing.Point(184, 56);
			this.btnAbort.Name = "btnAbort";
			this.btnAbort.Size = new System.Drawing.Size(166, 26);
			this.btnAbort.TabIndex = 1;
			this.btnAbort.Text = "�A�v���P�[�V�����𒆒f����(&X)";
			this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(22, 56);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(142, 26);
			this.btnOk.TabIndex = 2;
			this.btnOk.Text = "�͂��A����܂�";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// ryoshinDialog
			// 
			this.AcceptButton = this.btnAbort;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(364, 94);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.btnAbort);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ryoshinDialog";
			this.Text = "�m�F";
			this.Load += new System.EventHandler(this.ryoshinDialog_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void ryoshinDialog_Load(object sender, System.EventArgs e)
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

			this.btnOk.Text = "�͂��A����܂�(&" + shortcutkey.ToString() + ")";
		
		}

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();

		}

		private void btnAbort_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Abort;
			this.Close();
		}
	}
}
