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

namespace MakeInsert
{
	/// <summary>
	/// Form1 の概要の説明です。
	/// </summary>
	public class Form1 : sqaoBaseForm
	{
		private System.Windows.Forms.TextBox txtServerName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtInstance;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtUser;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnLogin;
		private System.Data.SqlClient.SqlConnection sqlConnection1;
		private System.Windows.Forms.Button btnServerHistory;
		protected saveClass	initopt;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.CheckBox chkTrust;
		private System.ComponentModel.IContainer components;

		public Form1()
		{
			//
			// Windows フォーム デザイナ サポートに必要です。
			//
			InitializeComponent();

			//
			// TODO: InitializeComponent 呼び出しの後に、コンストラクタ コードを追加してください。
			//
			FileStream fs = null;

			this.InitErrMessage();

			initopt = new saveClass();
			try
			{
				string path = Application.StartupPath;
				fs = new FileStream(path + "\\Makeinsert.xml", FileMode.Open);
				SoapFormatter sf = new SoapFormatter();
				if( fs != null && fs.CanRead )
				{
					initopt = (saveClass)sf.Deserialize(fs);
				}
			}
			catch(	System.IO.FileNotFoundException )
			{
				;
			}
			catch( Exception e )
			{
				this.SetErrorMessage(e);
			}
			finally
			{
				if( fs != null )
				{
					fs.Close();
				}
			}

		}

		/// <summary>
		/// 使用されているリソースに後処理を実行します。
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

		#region Windows フォーム デザイナで生成されたコード 
		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.txtServerName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtInstance = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtUser = new System.Windows.Forms.TextBox();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.btnLogin = new System.Windows.Forms.Button();
			this.sqlConnection1 = new System.Data.SqlClient.SqlConnection();
			this.btnServerHistory = new System.Windows.Forms.Button();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.label5 = new System.Windows.Forms.Label();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.chkTrust = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// msgArea
			// 
			this.msgArea.Location = new System.Drawing.Point(144, 240);
			this.msgArea.Name = "msgArea";
			this.msgArea.Size = new System.Drawing.Size(376, 24);
			this.msgArea.TabIndex = 9;
			// 
			// txtServerName
			// 
			this.txtServerName.Location = new System.Drawing.Point(144, 56);
			this.txtServerName.Name = "txtServerName";
			this.txtServerName.Size = new System.Drawing.Size(208, 19);
			this.txtServerName.TabIndex = 1;
			this.txtServerName.Text = "(local)";
			this.txtServerName.TextChanged += new System.EventHandler(this.txtServerName_TextChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(40, 56);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(88, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "サーバーの指定";
			this.label1.Click += new System.EventHandler(this.label1_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(40, 96);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(88, 23);
			this.label2.TabIndex = 2;
			this.label2.Text = "インスタンス";
			// 
			// txtInstance
			// 
			this.txtInstance.Location = new System.Drawing.Point(144, 96);
			this.txtInstance.Name = "txtInstance";
			this.txtInstance.Size = new System.Drawing.Size(208, 19);
			this.txtInstance.TabIndex = 2;
			this.txtInstance.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(40, 168);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(88, 23);
			this.label3.TabIndex = 4;
			this.label3.Text = "ユーザーID";
			// 
			// txtUser
			// 
			this.txtUser.Location = new System.Drawing.Point(144, 168);
			this.txtUser.Name = "txtUser";
			this.txtUser.Size = new System.Drawing.Size(208, 19);
			this.txtUser.TabIndex = 4;
			this.txtUser.Text = "sa";
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(144, 208);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(208, 19);
			this.txtPassword.TabIndex = 5;
			this.txtPassword.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(40, 208);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(88, 23);
			this.label4.TabIndex = 7;
			this.label4.Text = "パスワード";
			// 
			// btnLogin
			// 
			this.btnLogin.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnLogin.Location = new System.Drawing.Point(40, 232);
			this.btnLogin.Name = "btnLogin";
			this.btnLogin.Size = new System.Drawing.Size(88, 24);
			this.btnLogin.TabIndex = 6;
			this.btnLogin.Text = "接続";
			this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
			// 
			// btnServerHistory
			// 
			this.btnServerHistory.Location = new System.Drawing.Point(40, 8);
			this.btnServerHistory.Name = "btnServerHistory";
			this.btnServerHistory.Size = new System.Drawing.Size(304, 24);
			this.btnServerHistory.TabIndex = 0;
			this.btnServerHistory.Text = "過去に接続したサーバーから選択";
			this.btnServerHistory.Click += new System.EventHandler(this.btnServerHistory_Click);
			// 
			// checkBox1
			// 
			this.checkBox1.Location = new System.Drawing.Point(368, 56);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(128, 16);
			this.checkBox1.TabIndex = 1;
			this.checkBox1.Text = "接続先を保存する";
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("MS UI Gothic", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(128)));
			this.label5.Location = new System.Drawing.Point(8, 256);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(48, 16);
			this.label5.TabIndex = 8;
			this.label5.Text = "C Info;";
			this.toolTip1.SetToolTip(this.label5, "Copyright; Y.N(godz)  2004-2006");
			// 
			// chkTrust
			// 
			this.chkTrust.Location = new System.Drawing.Point(40, 136);
			this.chkTrust.Name = "chkTrust";
			this.chkTrust.Size = new System.Drawing.Size(136, 16);
			this.chkTrust.TabIndex = 3;
			this.chkTrust.Text = "信頼関係接続を利用";
			this.chkTrust.CheckedChanged += new System.EventHandler(this.chkTrust_CheckedChanged);
			// 
			// Form1
			// 
			this.AcceptButton = this.btnLogin;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(528, 269);
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
			this.Name = "Form1";
			this.Text = "SQL Add on Tool";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
			this.Load += new System.EventHandler(this.Form1_Load);
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
			this.Controls.SetChildIndex(this.msgArea, 0);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void label1_Click(object sender, System.EventArgs e)
		{
		
		}

		private void txtServerName_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			// ローカルのファイルから　オプションを読み込む

			this.checkBox1.Checked = true;
		}

		private void btnLogin_Click(object sender, System.EventArgs e)
		{
			String myConnString;
			if( this.chkTrust.Checked == false )
			{
				// ユーザー名での接続
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
				// 信頼関係接続
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

				MainForm mainForm = new MainForm(sv);
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
			}
			catch ( System.Data.SqlClient.SqlException se)
			{
				this.SetErrorMessage(se);
			}
			finally {
				this.sqlConnection1.Close();
			}
		}

		private void btnServerHistory_Click(object sender, System.EventArgs e)
		{
			if( this.initopt.ht.Count > 0 )
			{
				Form3 dlg = new Form3(this.initopt);
			
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

		private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			FileStream fs = null;

			try
			{
				string path = Application.StartupPath;
				fs = new FileStream(path + "\\Makeinsert.xml", FileMode.OpenOrCreate );
				fs.Close();
				fs = null;
				fs = new FileStream(path + "\\Makeinsert.xml", FileMode.Truncate, FileAccess.Write);
				SoapFormatter sf = new SoapFormatter();
				if( fs != null && fs.CanWrite )
				{
					ArrayList ar = new ArrayList();
					foreach( object keys in initopt.ht.Keys )
					{
						if( ((ServerData)(initopt.ht[keys])).isSaveKey == false )
						{
							ar.Add((string)keys);
						}
					}
					foreach( string kk in ar )
					{
						initopt.ht.Remove(kk);
					}
					if( initopt.ht.Count == 0 )
					{
						ServerData sv = new ServerData();
						sv.Servername = this.txtServerName.Text;
						sv.InstanceName = this.txtInstance.Text;
						sv.IsUseTrust = this.chkTrust.Checked;
						initopt.ht.Add(sv.KeyName,sv);
					}
					sf.Serialize(fs,(object)initopt);
				}
			}
			catch(	System.IO.FileNotFoundException )
			{
				;
			}
			catch( Exception ex )
			{
				this.SetErrorMessage(ex);
			}
			finally
			{
				if( fs != null )
				{
					fs.Close();
				}
			}
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
