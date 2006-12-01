using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

namespace serialFactory
{
	/// <summary>
	/// RegisterSerialDialog �̊T�v�̐����ł��B
	/// </summary>
	public class RegisterSerialDialog : System.Windows.Forms.Form
	{
		public SerialManager	smanager;

		private System.Windows.Forms.TextBox serialText;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.TextBox txtUser;
		private System.Windows.Forms.Button btnAbort;
		/// <summary>
		/// �K�v�ȃf�U�C�i�ϐ��ł��B
		/// </summary>
		private System.ComponentModel.Container components = null;

		public RegisterSerialDialog()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(RegisterSerialDialog));
			this.serialText = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.txtUser = new System.Windows.Forms.TextBox();
			this.btnAbort = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// serialText
			// 
			this.serialText.Location = new System.Drawing.Point(24, 44);
			this.serialText.Multiline = true;
			this.serialText.Name = "serialText";
			this.serialText.Size = new System.Drawing.Size(412, 96);
			this.serialText.TabIndex = 1;
			this.serialText.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(24, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(238, 22);
			this.label1.TabIndex = 0;
			this.label1.Text = "�V���A���L�[�����L�ɓ��͂��ĉ�����";
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(26, 214);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(102, 24);
			this.btnOK.TabIndex = 4;
			this.btnOK.Text = "�o�^(&R)";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(26, 154);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(198, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "���[�U�[������͂��Ă�������";
			// 
			// txtUser
			// 
			this.txtUser.Location = new System.Drawing.Point(24, 184);
			this.txtUser.Name = "txtUser";
			this.txtUser.Size = new System.Drawing.Size(412, 19);
			this.txtUser.TabIndex = 3;
			this.txtUser.Text = "";
			// 
			// btnAbort
			// 
			this.btnAbort.Location = new System.Drawing.Point(318, 214);
			this.btnAbort.Name = "btnAbort";
			this.btnAbort.Size = new System.Drawing.Size(102, 24);
			this.btnAbort.TabIndex = 5;
			this.btnAbort.Text = "�o�^�𒆒f(&X)";
			this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
			// 
			// RegisterSerialDialog
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(460, 248);
			this.Controls.Add(this.txtUser);
			this.Controls.Add(this.serialText);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnAbort);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "RegisterSerialDialog";
			this.Text = "�V���A���L�[�o�^";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RegisterSerialDialog_KeyDown);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			if( smanager == null )
			{
				MessageBox.Show("���������������s���Ă��܂���");
				return;
			}
			if( this.serialText.Text == "" )
			{
				MessageBox.Show("�V���A���L�[����͂��ĉ�����");
				this.serialText.Focus();
				return;
			}
			if( this.txtUser.Text == "" )
			{
				MessageBox.Show("���[�U�[������͂��ĉ�����");
				this.txtUser.Focus();
				return;
			}

			if( this.smanager.SetupSerial(this.serialText.Text.Replace("\r\n","").Replace("\n","").Replace("\r",""),this.txtUser.Text) == false)
			{
				MessageBox.Show("�V���A���L�[���������Ȃ����A�t�@�C���ɑ΂���A�N�Z�X��������܂���B\r\n�ēx���͒l��t�H���_�̃A�N�Z�X�������m�F���ĉ�����");
				return;
			}
			MessageBox.Show("�V���A���L�[�͐������o�^����܂���");
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void btnAbort_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.No;
			this.Close();
		}

		private void RegisterSerialDialog_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if( e.Alt == true &&
				e.Control == true &&
				e.Shift == true &&
				e.KeyCode == Keys.H )
			{
				if( this.smanager.errMsg != "" )
				{
					Clipboard.SetDataObject(this.smanager.errMsg,true );
				}
			}
		}

	}
}
