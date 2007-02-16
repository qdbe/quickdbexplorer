using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace serialFactory
{
	/// <summary>
	/// KeikokuDlg �̊T�v�̐����ł��B
	/// </summary>
	public class KeikokuDlg : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnAbort;
		private System.Windows.Forms.RadioButton rdoCommit;
		private System.Windows.Forms.RadioButton rdoNotCommit;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnContinue;
		private System.Windows.Forms.TextBox txtLicense;
		private	bool	isBeforeRegist = false;
		/// <summary>
		/// �K�v�ȃf�U�C�i�ϐ��ł��B
		/// </summary>
		private System.ComponentModel.Container components = null;

		public KeikokuDlg()
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
			this.btnAbort = new System.Windows.Forms.Button();
			this.btnContinue = new System.Windows.Forms.Button();
			this.rdoCommit = new System.Windows.Forms.RadioButton();
			this.rdoNotCommit = new System.Windows.Forms.RadioButton();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtLicense = new System.Windows.Forms.TextBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnAbort
			// 
			this.btnAbort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAbort.Location = new System.Drawing.Point(372, 300);
			this.btnAbort.Name = "btnAbort";
			this.btnAbort.Size = new System.Drawing.Size(164, 24);
			this.btnAbort.TabIndex = 6;
			this.btnAbort.Text = "�o�^�𒆒f(&X)";
			this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
			// 
			// btnContinue
			// 
			this.btnContinue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnContinue.Enabled = false;
			this.btnContinue.Location = new System.Drawing.Point(20, 304);
			this.btnContinue.Name = "btnContinue";
			this.btnContinue.Size = new System.Drawing.Size(112, 24);
			this.btnContinue.TabIndex = 7;
			this.btnContinue.Text = "�o�^�𑱍s����";
			this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
			// 
			// rdoCommit
			// 
			this.rdoCommit.Location = new System.Drawing.Point(12, 16);
			this.rdoCommit.Name = "rdoCommit";
			this.rdoCommit.Size = new System.Drawing.Size(84, 24);
			this.rdoCommit.TabIndex = 8;
			this.rdoCommit.Text = "���ӂ���";
			this.rdoCommit.CheckedChanged += new System.EventHandler(this.rdoCommit_CheckedChanged);
			// 
			// rdoNotCommit
			// 
			this.rdoNotCommit.Checked = true;
			this.rdoNotCommit.Location = new System.Drawing.Point(108, 16);
			this.rdoNotCommit.Name = "rdoNotCommit";
			this.rdoNotCommit.Size = new System.Drawing.Size(88, 24);
			this.rdoNotCommit.TabIndex = 9;
			this.rdoNotCommit.TabStop = true;
			this.rdoNotCommit.Text = "���ӂ��Ȃ�";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox1.Controls.Add(this.rdoCommit);
			this.groupBox1.Controls.Add(this.rdoNotCommit);
			this.groupBox1.Location = new System.Drawing.Point(16, 244);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(200, 48);
			this.groupBox1.TabIndex = 10;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "��L�g�p������";
			// 
			// txtLicense
			// 
			this.txtLicense.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtLicense.Location = new System.Drawing.Point(20, 16);
			this.txtLicense.Multiline = true;
			this.txtLicense.Name = "txtLicense";
			this.txtLicense.ReadOnly = true;
			this.txtLicense.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtLicense.Size = new System.Drawing.Size(532, 224);
			this.txtLicense.TabIndex = 11;
			this.txtLicense.Text = "";
			// 
			// KeikokuDlg
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(576, 333);
			this.Controls.Add(this.txtLicense);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnContinue);
			this.Controls.Add(this.btnAbort);
			this.Name = "KeikokuDlg";
			this.Text = "�g�p�������m�F";
			this.Load += new System.EventHandler(this.KeikokuDlg_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		public bool IsBeforeRegist
		{
			get { return this.isBeforeRegist; }
			set { this.isBeforeRegist = value; }
		}

		private void KeikokuDlg_Load(object sender, System.EventArgs e)
		{
			string licensestr = "";
			StreamReader sr = new StreamReader(Application.StartupPath + "\\license.txt",Encoding.GetEncoding(932));
			licensestr = sr.ReadToEnd();
			sr.Close();

			if( this.IsBeforeRegist == true )
			{
				this.btnAbort.Text = "�o�^�𒆒f(&X)";
			}
			else
			{
				this.btnAbort.Text = "�A�v���P�[�V�����𒆒f����(&X)";
			}

			this.txtLicense.Text = licensestr;
		}

		private void rdoCommit_CheckedChanged(object sender, System.EventArgs e)
		{
			if( this.rdoCommit.Checked == true )
			{
				this.btnContinue.Enabled = true;
			}
			else
			{
				this.btnContinue.Enabled = false;
			}
		}

		private void btnContinue_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void btnAbort_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
