using System;
using System.Collections;
using System.Data;
using System.Data.Sql;
using System.Reflection;
using System.Windows.Forms;

namespace quickDBExplorer
{
	/// <summary>
	/// SQL Serverへのログイン指定ダイアログ
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public class LogOnDialog : quickDBExplorer.quickDBExplorerBaseForm
	{
		/// <summary>
		/// 以前までの接続時記録情報
		/// </summary>
		private ConditionRecorder	initOpt;

		private System.Windows.Forms.CheckBox chkTrust;
		private System.Windows.Forms.Label labelSchema;
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
		/// コマンド引数 サーバー指定
		/// </summary>
		public static string PARAM_SERVER = "SERVER";
		/// <summary>
		/// コマンド引数 インスタンス名
		/// </summary>
		public static string PARAM_INSTANCE = "INSTANCE";
		/// <summary>
		/// コマンド引数 ユーザー名
		/// </summary>
		public static string PARAM_USER = "USER";
		/// <summary>
		/// コマンド引数 パスワード
		/// </summary>
		public static string PARAM_PASSWORD = "PASSWORD";
		/// <summary>
		/// コマンド引数 Window認証
		/// </summary>
		public static string PARAM_TRUST = "TRUST";

        /// <summary>
        /// コマンド引数
        /// </summary>
        private Hashtable commnadArgHt = null;
        private Button btnSelectServer;

        private bool IsActivateWithArgs = false;


		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="initialOption">記憶された設定情報</param>
		public LogOnDialog(ConditionRecorder initialOption)
		{
			// この呼び出しは Windows フォーム デザイナで必要です。
			InitializeComponent();

			this.initOpt = initialOption;

		}

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="initialOption">記憶された設定情報</param>
        /// <param name="argHt">コマンド引数を格納したパラメータ</param>
        public LogOnDialog(ConditionRecorder initialOption, 
            Hashtable argHt
            )
        {
            // この呼び出しは Windows フォーム デザイナで必要です。
            InitializeComponent();

            this.initOpt = initialOption;

            if (argHt != null)
            {
                this.commnadArgHt = (Hashtable)argHt.Clone();
                argHt.Clear();
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

		#region デザイナで生成されたコード
		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogOnDialog));
            this.chkTrust = new System.Windows.Forms.CheckBox();
            this.labelSchema = new System.Windows.Forms.Label();
            this.chkSaveInfo = new System.Windows.Forms.CheckBox();
            this.btnServerHistory = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPassword = new quickDBExplorer.quickDBExplorerTextBox();
            this.txtUser = new quickDBExplorer.quickDBExplorerTextBox();
            this.txtInstance = new quickDBExplorer.quickDBExplorerTextBox();
            this.txtServerName = new quickDBExplorer.quickDBExplorerTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSelectServer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // MsgArea
            // 
            this.MsgArea.Location = new System.Drawing.Point(152, 240);
            this.MsgArea.Size = new System.Drawing.Size(339, 25);
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
            // labelSchema
            // 
            this.labelSchema.Font = new System.Drawing.Font("MS UI Gothic", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelSchema.Location = new System.Drawing.Point(8, 249);
            this.labelSchema.Name = "labelSchema";
            this.labelSchema.Size = new System.Drawing.Size(48, 16);
            this.labelSchema.TabIndex = 20;
            this.labelSchema.Text = "C Info;";
            // 
            // chkSaveInfo
            // 
            this.chkSaveInfo.Location = new System.Drawing.Point(364, 88);
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
            this.txtPassword.CanCtrlDelete = true;
            this.txtPassword.IsDigitOnly = false;
            this.txtPassword.IsShowZoom = false;
            this.txtPassword.Location = new System.Drawing.Point(144, 201);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.PdHistory = null;
            this.txtPassword.Size = new System.Drawing.Size(208, 19);
            this.txtPassword.TabIndex = 23;
            // 
            // txtUser
            // 
            this.txtUser.CanCtrlDelete = true;
            this.txtUser.IsDigitOnly = false;
            this.txtUser.IsShowZoom = false;
            this.txtUser.Location = new System.Drawing.Point(144, 161);
            this.txtUser.Name = "txtUser";
            this.txtUser.PdHistory = null;
            this.txtUser.Size = new System.Drawing.Size(208, 19);
            this.txtUser.TabIndex = 21;
            this.txtUser.Text = "sa";
            // 
            // txtInstance
            // 
            this.txtInstance.CanCtrlDelete = true;
            this.txtInstance.IsDigitOnly = false;
            this.txtInstance.IsShowZoom = false;
            this.txtInstance.Location = new System.Drawing.Point(144, 89);
            this.txtInstance.Name = "txtInstance";
            this.txtInstance.PdHistory = null;
            this.txtInstance.Size = new System.Drawing.Size(208, 19);
            this.txtInstance.TabIndex = 17;
            // 
            // txtServerName
            // 
            this.txtServerName.CanCtrlDelete = true;
            this.txtServerName.IsDigitOnly = false;
            this.txtServerName.IsShowZoom = false;
            this.txtServerName.Location = new System.Drawing.Point(144, 49);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.PdHistory = null;
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
            this.label2.TabIndex = 25;
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
            // btnSelectServer
            // 
            this.btnSelectServer.Location = new System.Drawing.Point(364, 47);
            this.btnSelectServer.Name = "btnSelectServer";
            this.btnSelectServer.Size = new System.Drawing.Size(101, 23);
            this.btnSelectServer.TabIndex = 16;
            this.btnSelectServer.Text = "接続先(&L)";
            this.btnSelectServer.UseVisualStyleBackColor = true;
            this.btnSelectServer.Click += new System.EventHandler(this.btnSelectServer_Click);
            // 
            // LogOnDialog
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
            this.ClientSize = new System.Drawing.Size(520, 266);
            this.Controls.Add(this.btnSelectServer);
            this.Controls.Add(this.chkTrust);
            this.Controls.Add(this.labelSchema);
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
            this.Name = "LogOnDialog";
            this.Text = "ログイン";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.LogOnDialog_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.LogOnDialog_Closing);
            this.Controls.SetChildIndex(this.MsgArea, 0);
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
            this.Controls.SetChildIndex(this.labelSchema, 0);
            this.Controls.SetChildIndex(this.chkTrust, 0);
            this.Controls.SetChildIndex(this.btnSelectServer, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        /// <summary>
        /// 初めて表示された場合には、コマンド引数を処理する
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            SetCommandParmeter();
        }

        private bool SetCommandParmeter()
        {
            if (this.commnadArgHt != null && this.commnadArgHt.Count > 0)
            {
                // コマンド引数が指定されている
                if (this.commnadArgHt.Count != 0)
                {
                    this.chkSaveInfo.Checked = true;
                }
                this.txtServerName.Text = (string)this.commnadArgHt[PARAM_SERVER];
                    this.txtInstance.Text = (string)this.commnadArgHt[PARAM_INSTANCE];
                if (this.commnadArgHt.Contains(PARAM_TRUST))
                {
                    if ((bool)this.commnadArgHt[PARAM_TRUST] == true)
                    {
                        this.chkTrust.Checked = true;
                    }
                    else
                    {
                        this.chkTrust.Checked = false;
                    }
                }
                this.txtUser.Text = (string)this.commnadArgHt[PARAM_USER];
                this.txtPassword.Text = (string)this.commnadArgHt[PARAM_PASSWORD];

                if (this.txtServerName.Text.Length > 0 &&
                    (this.chkTrust.Checked ||
                     (this.txtUser.Text.Length > 0 &&
                     this.txtPassword.Text.Length > 0)
                    )
                    )
                {
                    // 必要最低の情報は指定されているので、ログイン処理を実施する
                    this.IsActivateWithArgs = true;
                    DoLogin();
                    this.Close();
                    return true;
                }
            }
            return false;

        }

		/// <summary>
		/// 画面初期表示時処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LogOnDialog_Load(object sender, System.EventArgs e)
		{

			this.chkSaveInfo.Checked = true;
			// 最後に表示したサーバーの情報があれば、それを表示する
			if( initOpt.LastServerKey.Length != 0 )
			{
				// 記憶されたサーバー別の記憶情報
				ServerData sv = (ServerData)initOpt.PerServerData[initOpt.LastServerKey];
                SetServer(sv.Servername, sv.InstanceName);
				// パスワードは記憶していないので戻す必要なし
			}
			if( this.initOpt.PerServerData.Count > 0 )
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
            DoLogin();
		}

        private void DoLogin()
        {
            String myConnString;
            if (this.chkTrust.Checked == false)
            {
                // ユーザー名での接続
                if (this.txtInstance.Text.Length != 0)
                {
                    // インスタンス名あり
                    myConnString = "Server=" + this.txtServerName.Text + @"\" + this.txtInstance.Text + ";"
                        + "Database=master;User ID=" + this.txtUser.Text
                        + ";Password=" + this.txtPassword.Text;
                }
                else
                {
                    // インスタンス名なし
                    myConnString = "Server=" + this.txtServerName.Text + ";"
                        + "Database=master;User ID=" + this.txtUser.Text
                        + ";Password=" + this.txtPassword.Text;
                }
            }
            else
            {
                // 信頼関係接続
                if (this.txtInstance.Text.Length != 0)
                {
                    // インスタンス名あり
                    myConnString = "Server=" + this.txtServerName.Text + @"\" + this.txtInstance.Text + ";"
                        + "Database=master;Integrated Security=SSPI;";
                }
                else
                {
                    // インスタンス名なし
                    myConnString = "Server=" + this.txtServerName.Text + ";"
                        + "Database=master;Integrated Security=SSPI;";
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
                if (initOpt.PerServerData[sv.KeyName] == null)
                {
                    initOpt.PerServerData.Add(sv.KeyName, sv);
                }
                else
                {
                    sv = (ServerData)initOpt.PerServerData[sv.KeyName];
                }

                if (this.chkSaveInfo.Checked == false)
                {
                    sv.IsSaveKey = false;
                }
                else
                {
                    sv.IsSaveKey = true;
                }
                sv.IsUseTrust = this.chkTrust.Checked;
                sv.LogOnUser = this.txtUser.Text;
                // 最後に接続したサーバーを更新
                initOpt.LastServerKey = sv.KeyName;
                // SQL 処理クラスの初期化
                SqlVersion sqlVer = new SqlVersion(con.ServerVersion);

                // メインダイアログを表示
                MainForm mainForm = new MainForm(sv, sqlVer);
                mainForm.MdiParent = this.MdiParent;
                mainForm.ServerName = this.txtServerName.Text;
                mainForm.ServerRealName = this.txtServerName.Text;
                mainForm.InstanceName = this.txtInstance.Text;
                mainForm.LogOnUid = this.txtUser.Text;
                mainForm.LogOnPassword = this.txtPassword.Text;
                mainForm.IsUseTruse = this.chkTrust.Checked;
                if (this.txtInstance.Text.Length != 0)
                {
                    mainForm.ServerName = this.txtServerName.Text + "@" + this.txtInstance.Text;
                }
                else
                {
                    mainForm.ServerName = this.txtServerName.Text;
                }

                // SQL SERVERのバージョンに応じたDLLを読み込む
                dllName = string.Format(System.Globalization.CultureInfo.CurrentCulture, Application.StartupPath + "\\SqlServer{0}Driver.dll", sqlVer.AdapterNameString);
                className = string.Format(System.Globalization.CultureInfo.CurrentCulture, "quickDBExplorer.SqlServerDriver{0}", sqlVer.AdapterNameString);
                asm = Assembly.LoadFrom(dllName);
                mainForm.SqlDriver = (ISqlInterface)asm.CreateInstance(className, true);

                mainForm.SqlDriver.SetConnection(con, mainForm.SqlTimeout);
                mainForm.InitPopupMenu();

                // MDI なので、モードレスでダイアログを表示する
                mainForm.Show();
                // メインダイアログを表示すれば、このダイアログは不要
                if (this.IsActivateWithArgs == false)
                {
                    this.Close();
                }
            }
            catch (System.Data.SqlClient.SqlException se)
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
			if( this.initOpt.PerServerData.Count > 0 )
			{
				ServerSelectDialog dlg = new ServerSelectDialog(this.initOpt);
			
				if( dlg.ShowDialog() == DialogResult.OK	)
				{
                    SetServer(dlg.SelectedServer,dlg.SelectedInstance);
                    this.txtPassword.Focus();
                }
			}
		}

        private void SetServer(string serverName, string instanceName)
        {
            this.txtServerName.Text = serverName;
            this.txtInstance.Text = instanceName;

            ServerData sv = new ServerData();
            sv.Servername = this.txtServerName.Text;
            sv.InstanceName = this.txtInstance.Text;

            ServerData selectSv = (ServerData)this.initOpt.PerServerData[sv.KeyName];
            if (selectSv != null && selectSv.IsUseTrust == true)
            {
                this.chkTrust.Checked = true;
            }
            else
            {
                this.chkTrust.Checked = false;
                this.txtPassword.Text = "";
            }
        }

		private void LogOnDialog_Closing(object sender, System.ComponentModel.CancelEventArgs e)
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

        private void btnSelectServer_Click(object sender, EventArgs e)
        {
            SqlDataSourceEnumerator list = System.Data.Sql.SqlDataSourceEnumerator.Instance;
            this.Cursor = Cursors.WaitCursor;
            DataTable serverList = list.GetDataSources();
            this.Cursor = Cursors.Default;
            if (serverList.Rows.Count == 0)
            {
                MessageBox.Show("選択可能なサーバーはありません");
                return;
            }
            SqlServerSelector dlg = new SqlServerSelector(serverList);
            if (dlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            SetServer(dlg.SelectedServerName,dlg.SelectedInstanceName);
        }
	}
}

