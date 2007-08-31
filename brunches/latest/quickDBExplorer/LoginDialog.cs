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
using System.Reflection;

namespace quickDBExplorer
{
	/// <summary>
	/// SQL Serverへのログイン指定ダイアログ
	/// </summary>
	public class LoginDialog : quickDBExplorer.quickDBExplorerBaseForm
	{
		/// <summary>
		/// 以前までの接続時記録情報
		/// </summary>
		protected saveClass	initopt;

		private System.Windows.Forms.CheckBox chkTrust;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox chkSaveInfo;
		private System.Windows.Forms.Button btnServerHistory;
		private System.Windows.Forms.Button btnLogin;
		private System.Windows.Forms.Label label4;
		private quickDBExplorerTextBox txtPassword;
		private quickDBExplorerTextBox txtUser;
		private quickDBExplorerTextBox txtInstance;
		private quickDBExplorerTextBox txtServerName;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="initialOption">記憶された設定情報</param>
		public LoginDialog(saveClass initialOption)
		{
			// この呼び出しは Windows フォーム デザイナで必要です。
			InitializeComponent();

			this.initopt = initialOption;

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

		#region デザイナで生成されたコード
		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(LoginDialog));
			this.chkTrust = new System.Windows.Forms.CheckBox();
			this.label5 = new System.Windows.Forms.Label();
			this.chkSaveInfo = new System.Windows.Forms.CheckBox();
			this.btnServerHistory = new System.Windows.Forms.Button();
			this.btnLogin = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.txtPassword = new quickDBExplorerTextBox();
			this.txtUser = new quickDBExplorerTextBox();
			this.txtInstance = new quickDBExplorerTextBox();
			this.txtServerName = new quickDBExplorerTextBox();
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
			this.chkTrust.Text = "Windows認証を利用(&T)";
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
			// chkSaveInfo
			// 
			this.chkSaveInfo.Location = new System.Drawing.Point(368, 49);
			this.chkSaveInfo.Name = "chkSaveInfo";
			this.chkSaveInfo.Size = new System.Drawing.Size(144, 16);
			this.chkSaveInfo.TabIndex = 13;
			this.chkSaveInfo.Text = "接続先を保存する(&R)";
			// 
			// btnServerHistory
			// 
			this.btnServerHistory.Location = new System.Drawing.Point(40, 1);
			this.btnServerHistory.Name = "btnServerHistory";
			this.btnServerHistory.Size = new System.Drawing.Size(304, 24);
			this.btnServerHistory.TabIndex = 12;
			this.btnServerHistory.Text = "過去に接続したサーバーから選択(&Z)";
			this.btnServerHistory.Click += new System.EventHandler(this.btnServerHistory_Click);
			// 
			// btnLogin
			// 
			this.btnLogin.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnLogin.Location = new System.Drawing.Point(40, 225);
			this.btnLogin.Name = "btnLogin";
			this.btnLogin.Size = new System.Drawing.Size(88, 24);
			this.btnLogin.TabIndex = 24;
			this.btnLogin.Text = "接続(&O)";
			this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(40, 201);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(88, 23);
			this.label4.TabIndex = 22;
			this.label4.Text = "パスワード(&P)";
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
			this.label3.Text = "ユーザーID(&U)";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(32, 89);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(96, 23);
			this.label2.TabIndex = 16;
			this.label2.Text = "インスタンス(&I)";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(32, 49);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 23);
			this.label1.TabIndex = 14;
			this.label1.Text = "サーバーの指定(&S)";
			// 
			// LoginDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(520, 266);
			this.Controls.Add(this.chkTrust);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.chkSaveInfo);
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
			this.Text = "ログイン";
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
			this.Controls.SetChildIndex(this.chkSaveInfo, 0);
			this.Controls.SetChildIndex(this.label5, 0);
			this.Controls.SetChildIndex(this.chkTrust, 0);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// 画面初期表示時処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LoginDialog_Load(object sender, System.EventArgs e)
		{
			// ローカルのファイルから　オプションを読み込む

			this.chkSaveInfo.Checked = true;
			// 最後に表示したサーバーの情報があれば、それを表示する
			if( initopt.LastServerKey != "" )
			{
				// 記憶されたサーバー別の記憶情報
				ServerData sv = (ServerData)initopt.PerServerData[initopt.LastServerKey];
				this.txtServerName.Text = sv.Servername;
				this.txtInstance.Text = sv.InstanceName;
				if( sv.IsUseTrust == true )
				{
					this.chkTrust.Checked = true;
				}
				else
				{
					this.chkTrust.Checked = false;
					this.txtUser.Text = sv.LoginUser;
				}
				// パスワードは記憶していないので戻す必要なし
			}
			if( this.initopt.PerServerData.Count > 0 )
			{
				this.btnServerHistory.Enabled = true;
			}
			else
			{
				this.btnServerHistory.Enabled = false;
			}
		}

		/// <summary>
		/// ログインボタン押下時処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnLogin_Click(object sender, System.EventArgs e)
		{
			String myConnString;
			if( this.chkTrust.Checked == false )
			{
				// ユーザー名での接続
				if( this.txtInstance.Text != "" )
				{
					// インスタンス名あり
					myConnString = "Server=" + this.txtServerName.Text + @"\" + this.txtInstance.Text + ";"
						+"Database=master;User ID="+this.txtUser.Text
						+";Password="+this.txtPassword.Text;			}
				else
				{
					// インスタンス名なし
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
					// インスタンス名あり
					myConnString = "Server=" + this.txtServerName.Text + @"\" + this.txtInstance.Text + ";"
						+"Database=master;Integrated Security=SSPI;";
				}
				else
				{
					// インスタンス名なし
					myConnString = "Server=" + this.txtServerName.Text + ";"
						+"Database=master;Integrated Security=SSPI;";
				}
			}

			// エラーメッセージクリア
			this.InitErrMessage();

			try 
			{
				Assembly asm = null;
				string dllName = "";
				string className = "";

				// SQL Server とのコネクションを確立する
				System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection();
				con.ConnectionString = myConnString;
				con.Open();

				// 以前のサーバー別情報があれば、それを再作成する
				ServerData sv = new ServerData();
				sv.Servername = this.txtServerName.Text;
				sv.InstanceName = this.txtInstance.Text;
				if( initopt.PerServerData[sv.KeyName] == null )
				{
					initopt.PerServerData.Add(sv.KeyName,sv);
				}
				else
				{
					sv = (ServerData)initopt.PerServerData[sv.KeyName];
				}

				if( this.chkSaveInfo.Checked == false )
				{
					sv.IsSaveKey = false;
				}
				else
				{
					sv.IsSaveKey = true;
				}
				sv.IsUseTrust = this.chkTrust.Checked;
				sv.LoginUser = this.txtUser.Text;
				// 最後に接続したサーバーを更新
				initopt.LastServerKey = sv.KeyName;

				// メインダイアログを表示
				MainForm mainForm = new MainForm(sv);
				mainForm.MdiParent = this.MdiParent;
				mainForm.ServerName = this.txtServerName.Text;
				mainForm.ServerRealName = this.txtServerName.Text;
				mainForm.InstanceName = this.txtInstance.Text;
				mainForm.LoginUid = this.txtUser.Text;
				mainForm.LoginPasswd = this.txtPassword.Text;
				mainForm.IsUseTruse = this.chkTrust.Checked;
				if( this.txtInstance.Text != "" )
				{
					mainForm.ServerName = this.txtServerName.Text + "@" + this.txtInstance.Text;
				}
				else
				{
					mainForm.ServerName = this.txtServerName.Text;
				}
				mainForm.sqlConnection1 = con;

				if(con.ServerVersion.StartsWith("08") )
				{
					mainForm.SqlVersion = 2000;
				}
				else if(con.ServerVersion.StartsWith("09") )
				{
					mainForm.SqlVersion = 2005;
				}
				dllName = string.Format("SqlServer{0}Driver.dll", mainForm.SqlVersion );
				className = "quickDBExplorer.SqlServerDriver";
				asm = Assembly.LoadFrom(dllName);
				mainForm.SqlDriver = (ISqlInterface)asm.CreateInstance(className,true);

				mainForm.SqlDriver.SetConnection(mainForm.sqlConnection1);

				mainForm.Show();
				// メインダイアログを表示すれば、このダイアログは不要
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

		/// <summary>
		/// 過去のサーバー接続履歴から、選択する
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnServerHistory_Click(object sender, System.EventArgs e)
		{
			if( this.initopt.PerServerData.Count > 0 )
			{
				ServerSelectDialog dlg = new ServerSelectDialog(this.initopt);
			
				if( dlg.ShowDialog() == DialogResult.OK	)
				{
					this.txtServerName.Text = dlg.SelectedServer;
					this.txtInstance.Text = dlg.SelectedInstance;

					ServerData sv = new ServerData();
					sv.Servername = this.txtServerName.Text;
					sv.InstanceName = this.txtInstance.Text;

					ServerData selectSv = (ServerData)this.initopt.PerServerData[sv.KeyName];
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

