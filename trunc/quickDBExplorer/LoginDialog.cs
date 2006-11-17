using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Soap;

namespace quickDBExplorer
{
	public class LoginDialog : quickDBExplorer.quickDBExplorerBaseForm
	{
		protected saveClass	initopt;

		private System.Windows.Forms.CheckBox chkTrust;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.Button btnServerHistory;
		private System.Windows.Forms.Button btnLogin;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.TextBox txtUser;
		private System.Windows.Forms.TextBox txtInstance;
		private System.Windows.Forms.TextBox txtServerName;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.ComponentModel.IContainer components = null;

		public LoginDialog(saveClass initialOption)
		{
			// ���̌Ăяo���� Windows �t�H�[�� �f�U�C�i�ŕK�v�ł��B
			InitializeComponent();

			this.initopt = initialOption;

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(LoginDialog));
			this.chkTrust = new System.Windows.Forms.CheckBox();
			this.label5 = new System.Windows.Forms.Label();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.btnServerHistory = new System.Windows.Forms.Button();
			this.btnLogin = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.txtUser = new System.Windows.Forms.TextBox();
			this.txtInstance = new System.Windows.Forms.TextBox();
			this.txtServerName = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// msgArea
			// 
			this.msgArea.Location = new System.Drawing.Point(152, 240);
			this.msgArea.Name = "msgArea";
			// 
			// chkTrust
			// 
			this.chkTrust.Location = new System.Drawing.Point(40, 129);
			this.chkTrust.Name = "chkTrust";
			this.chkTrust.Size = new System.Drawing.Size(160, 16);
			this.chkTrust.TabIndex = 18;
			this.chkTrust.Text = "Windows�F�؂𗘗p(&T)";
			this.chkTrust.CheckedChanged += new System.EventHandler(this.chkTrust_CheckedChanged);
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("MS UI Gothic", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(128)));
			this.label5.Location = new System.Drawing.Point(8, 249);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(48, 16);
			this.label5.TabIndex = 20;
			this.label5.Text = "C Info;";
			// 
			// checkBox1
			// 
			this.checkBox1.Location = new System.Drawing.Point(368, 49);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(144, 16);
			this.checkBox1.TabIndex = 13;
			this.checkBox1.Text = "�ڑ����ۑ�����(&R)";
			// 
			// btnServerHistory
			// 
			this.btnServerHistory.Location = new System.Drawing.Point(40, 1);
			this.btnServerHistory.Name = "btnServerHistory";
			this.btnServerHistory.Size = new System.Drawing.Size(304, 24);
			this.btnServerHistory.TabIndex = 12;
			this.btnServerHistory.Text = "�ߋ��ɐڑ������T�[�o�[����I��(&Z)";
			this.btnServerHistory.Click += new System.EventHandler(this.btnServerHistory_Click);
			// 
			// btnLogin
			// 
			this.btnLogin.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnLogin.Location = new System.Drawing.Point(40, 225);
			this.btnLogin.Name = "btnLogin";
			this.btnLogin.Size = new System.Drawing.Size(88, 24);
			this.btnLogin.TabIndex = 24;
			this.btnLogin.Text = "�ڑ�(&O)";
			this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(40, 201);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(88, 23);
			this.label4.TabIndex = 22;
			this.label4.Text = "�p�X���[�h(&P)";
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(144, 201);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(208, 19);
			this.txtPassword.TabIndex = 23;
			this.txtPassword.Text = "";
			// 
			// txtUser
			// 
			this.txtUser.Location = new System.Drawing.Point(144, 161);
			this.txtUser.Name = "txtUser";
			this.txtUser.Size = new System.Drawing.Size(208, 19);
			this.txtUser.TabIndex = 21;
			this.txtUser.Text = "sa";
			// 
			// txtInstance
			// 
			this.txtInstance.Location = new System.Drawing.Point(144, 89);
			this.txtInstance.Name = "txtInstance";
			this.txtInstance.Size = new System.Drawing.Size(208, 19);
			this.txtInstance.TabIndex = 17;
			this.txtInstance.Text = "";
			// 
			// txtServerName
			// 
			this.txtServerName.Location = new System.Drawing.Point(144, 49);
			this.txtServerName.Name = "txtServerName";
			this.txtServerName.Size = new System.Drawing.Size(208, 19);
			this.txtServerName.TabIndex = 15;
			this.txtServerName.Text = "(local)";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(40, 161);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(88, 23);
			this.label3.TabIndex = 19;
			this.label3.Text = "���[�U�[ID(&U)";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(32, 89);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(96, 23);
			this.label2.TabIndex = 16;
			this.label2.Text = "�C���X�^���X(&I)";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(32, 49);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 23);
			this.label1.TabIndex = 14;
			this.label1.Text = "�T�[�o�[�̎w��(&S)";
			// 
			// LoginDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(520, 266);
			this.Controls.Add(this.chkTrust);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.btnServerHistory);
			this.Controls.Add(this.btnLogin);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.txtPassword);
			this.Controls.Add(this.txtUser);
			this.Controls.Add(this.txtInstance);
			this.Controls.Add(this.txtServerName);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "LoginDialog";
			this.Text = "���O�C��";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.LoginDialog_Closing);
			this.Load += new System.EventHandler(this.LoginDialog_Load);
			this.Controls.SetChildIndex(this.msgArea, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.label2, 0);
			this.Controls.SetChildIndex(this.label3, 0);
			this.Controls.SetChildIndex(this.txtServerName, 0);
			this.Controls.SetChildIndex(this.txtInstance, 0);
			this.Controls.SetChildIndex(this.txtUser, 0);
			this.Controls.SetChildIndex(this.txtPassword, 0);
			this.Controls.SetChildIndex(this.label4, 0);
			this.Controls.SetChildIndex(this.btnLogin, 0);
			this.Controls.SetChildIndex(this.btnServerHistory, 0);
			this.Controls.SetChildIndex(this.checkBox1, 0);
			this.Controls.SetChildIndex(this.label5, 0);
			this.Controls.SetChildIndex(this.chkTrust, 0);
			this.ResumeLayout(false);

		}
		#endregion

		private void LoginDialog_Load(object sender, System.EventArgs e)
		{
			// ���[�J���̃t�@�C������@�I�v�V������ǂݍ���

			this.checkBox1.Checked = true;
			if( initopt.lastserverkey != "" )
			{
				ServerData sv = (ServerData)initopt.ht[initopt.lastserverkey];
				this.txtServerName.Text = sv.Servername;
				this.txtInstance.Text = sv.InstanceName;
				if( sv.IsUseTrust == true )
				{
					this.chkTrust.Checked = true;
				}
				else
				{
					this.chkTrust.Checked = false;
					this.txtUser.Text = sv.loginUser;
				}
			}
			if( this.initopt.ht.Count > 0 )
			{
				this.btnServerHistory.Enabled = true;
			}
			else
			{
				this.btnServerHistory.Enabled = false;
			}
		}

		private void btnLogin_Click(object sender, System.EventArgs e)
		{
			String myConnString;
			if( this.chkTrust.Checked == false )
			{
				// ���[�U�[���ł̐ڑ�
				if( this.txtInstance.Text != "" )
				{
					myConnString = "Server=" + this.txtServerName.Text + @"\" + this.txtInstance.Text + ";"
						+"Database=master;User ID="+this.txtUser.Text
						+";Password="+this.txtPassword.Text;			}
				else
				{
					myConnString = "Server=" + this.txtServerName.Text + ";"
						+"Database=master;User ID="+this.txtUser.Text
						+";Password="+this.txtPassword.Text;
				}
			}
			else
			{
				// �M���֌W�ڑ�
				if( this.txtInstance.Text != "" )
				{
					myConnString = "Server=" + this.txtServerName.Text + @"\" + this.txtInstance.Text + ";"
						+"Database=master;Integrated Security=SSPI;";
				}
				else
				{
					myConnString = "Server=" + this.txtServerName.Text + ";"
						+"Database=master;Integrated Security=SSPI;";
				}
			}
			
			this.InitErrMessage();

			try 
			{
				System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection();
				con.ConnectionString = myConnString;
				con.Open();

				ServerData sv = new ServerData();
				sv.Servername = this.txtServerName.Text;
				sv.InstanceName = this.txtInstance.Text;
				if( initopt.ht[sv.KeyName] == null )
				{
					initopt.ht.Add(sv.KeyName,sv);
				}
				else
				{
					sv = (ServerData)initopt.ht[sv.KeyName];
				}

				if( this.checkBox1.Checked == false )
				{
					sv.isSaveKey = false;
				}
				else
				{
					sv.isSaveKey = true;
				}
				sv.IsUseTrust = this.chkTrust.Checked;
				sv.loginUser = this.txtUser.Text;
				initopt.lastserverkey = sv.KeyName;

				MainForm mainForm = new MainForm(sv);
				mainForm.MdiParent = this.MdiParent;
				mainForm.servername = this.txtServerName.Text;
				mainForm.serverRealName = this.txtServerName.Text;
				mainForm.instanceName = this.txtInstance.Text;
				mainForm.loginUid = this.txtUser.Text;
				mainForm.loginPasswd = this.txtPassword.Text;
				mainForm.IsUseTruse = this.chkTrust.Checked;
				if( this.txtInstance.Text != "" )
				{
					mainForm.servername = this.txtServerName.Text + "@" + this.txtInstance.Text;
				}
				else
				{
					mainForm.servername = this.txtServerName.Text;
				}
				mainForm.sqlConnection1 = con;
				mainForm.Show();
				this.Close();
			}
			catch ( System.Data.SqlClient.SqlException se)
			{
				this.SetErrorMessage(se);
			}
			//finally {
			//	mainForm.sqlConnection1.Close();
			//}
		}

		private void btnServerHistory_Click(object sender, System.EventArgs e)
		{
			if( this.initopt.ht.Count > 0 )
			{
				ServerSelectDialog dlg = new ServerSelectDialog(this.initopt);
			
				if( dlg.ShowDialog() == DialogResult.OK	)
				{
					this.txtServerName.Text = dlg.selectedServer;
					this.txtInstance.Text = dlg.selectedInstance;

					ServerData sv = new ServerData();
					sv.Servername = this.txtServerName.Text;
					sv.InstanceName = this.txtInstance.Text;

					ServerData selectSv = (ServerData)this.initopt.ht[sv.KeyName];
					if( selectSv != null && selectSv.IsUseTrust == true )
					{
						this.chkTrust.Checked = true;
					}
					else
					{
						this.chkTrust.Checked = false;
						this.txtPassword.Text = "";
					}
					this.txtPassword.Focus();
				}
			}
		}

		private void LoginDialog_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
		}

		private void chkTrust_CheckedChanged(object sender, System.EventArgs e)
		{
			if( this.chkTrust.Checked == true )
			{
				this.txtUser.Enabled = false;
				this.txtPassword.Enabled = false;
			}
			else
			{
				this.txtUser.Enabled = true;
				this.txtPassword.Enabled = true;
			}
		}
	}
}

