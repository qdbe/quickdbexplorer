using System;
using System.Collections;
using System.Data;
using System.Data.Sql;
using System.Reflection;
using System.Windows.Forms;

namespace quickDBExplorer
{
	/// <summary>
	/// SQL Server�ւ̃��O�C���w��_�C�A���O
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public class LogOnDialog : quickDBExplorer.quickDBExplorerBaseForm
	{
		/// <summary>
		/// �ȑO�܂ł̐ڑ����L�^���
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
		/// �R�}���h���� �T�[�o�[�w��
		/// </summary>
		public static string PARAM_SERVER = "SERVER";
		/// <summary>
		/// �R�}���h���� �C���X�^���X��
		/// </summary>
		public static string PARAM_INSTANCE = "INSTANCE";
		/// <summary>
		/// �R�}���h���� ���[�U�[��
		/// </summary>
		public static string PARAM_USER = "USER";
		/// <summary>
		/// �R�}���h���� �p�X���[�h
		/// </summary>
		public static string PARAM_PASSWORD = "PASSWORD";
		/// <summary>
		/// �R�}���h���� Window�F��
		/// </summary>
		public static string PARAM_TRUST = "TRUST";

        /// <summary>
        /// �R�}���h����
        /// </summary>
        private Hashtable commnadArgHt = null;
        private Button btnSelectServer;

        private bool IsActivateWithArgs = false;


		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		/// <param name="initialOption">�L�����ꂽ�ݒ���</param>
		public LogOnDialog(ConditionRecorder initialOption)
		{
			// ���̌Ăяo���� Windows �t�H�[�� �f�U�C�i�ŕK�v�ł��B
			InitializeComponent();

			this.initOpt = initialOption;

		}

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="initialOption">�L�����ꂽ�ݒ���</param>
        /// <param name="argHt">�R�}���h�������i�[�����p�����[�^</param>
        public LogOnDialog(ConditionRecorder initialOption, 
            Hashtable argHt
            )
        {
            // ���̌Ăяo���� Windows �t�H�[�� �f�U�C�i�ŕK�v�ł��B
            InitializeComponent();

            this.initOpt = initialOption;

            if (argHt != null)
            {
                this.commnadArgHt = (Hashtable)argHt.Clone();
                argHt.Clear();
            }

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
            this.chkTrust.Text = "Windows�F�؂𗘗p(&T)";
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
            this.chkSaveInfo.Text = "�ڑ����ۑ�����(&R)";
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
            this.label3.Text = "���[�U�[ID(&U)";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(32, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 23);
            this.label2.TabIndex = 25;
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
            // btnSelectServer
            // 
            this.btnSelectServer.Location = new System.Drawing.Point(364, 47);
            this.btnSelectServer.Name = "btnSelectServer";
            this.btnSelectServer.Size = new System.Drawing.Size(101, 23);
            this.btnSelectServer.TabIndex = 16;
            this.btnSelectServer.Text = "�ڑ���(&L)";
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
            this.Text = "���O�C��";
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
        /// ���߂ĕ\�����ꂽ�ꍇ�ɂ́A�R�}���h��������������
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
                // �R�}���h�������w�肳��Ă���
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
                    // �K�v�Œ�̏��͎w�肳��Ă���̂ŁA���O�C�����������{����
                    this.IsActivateWithArgs = true;
                    DoLogin();
                    this.Close();
                    return true;
                }
            }
            return false;

        }

		/// <summary>
		/// ��ʏ����\��������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LogOnDialog_Load(object sender, System.EventArgs e)
		{

			this.chkSaveInfo.Checked = true;
			// �Ō�ɕ\�������T�[�o�[�̏�񂪂���΁A�����\������
			if( initOpt.LastServerKey.Length != 0 )
			{
				// �L�����ꂽ�T�[�o�[�ʂ̋L�����
				ServerData sv = (ServerData)initOpt.PerServerData[initOpt.LastServerKey];
                SetServer(sv.Servername, sv.InstanceName);
				// �p�X���[�h�͋L�����Ă��Ȃ��̂Ŗ߂��K�v�Ȃ�
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
		/// ���O�C���{�^������������
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
                // ���[�U�[���ł̐ڑ�
                if (this.txtInstance.Text.Length != 0)
                {
                    // �C���X�^���X������
                    myConnString = "Server=" + this.txtServerName.Text + @"\" + this.txtInstance.Text + ";"
                        + "Database=master;User ID=" + this.txtUser.Text
                        + ";Password=" + this.txtPassword.Text;
                }
                else
                {
                    // �C���X�^���X���Ȃ�
                    myConnString = "Server=" + this.txtServerName.Text + ";"
                        + "Database=master;User ID=" + this.txtUser.Text
                        + ";Password=" + this.txtPassword.Text;
                }
            }
            else
            {
                // �M���֌W�ڑ�
                if (this.txtInstance.Text.Length != 0)
                {
                    // �C���X�^���X������
                    myConnString = "Server=" + this.txtServerName.Text + @"\" + this.txtInstance.Text + ";"
                        + "Database=master;Integrated Security=SSPI;";
                }
                else
                {
                    // �C���X�^���X���Ȃ�
                    myConnString = "Server=" + this.txtServerName.Text + ";"
                        + "Database=master;Integrated Security=SSPI;";
                }
            }

            // �G���[���b�Z�[�W�N���A
            this.InitErrMessage();

            try
            {
                Assembly asm = null;
                string dllName = "";
                string className = "";

                // SQL Server �Ƃ̃R�l�N�V�������m������
                System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection();
                con.ConnectionString = myConnString;
                con.Open();

                // �ȑO�̃T�[�o�[�ʏ�񂪂���΁A������č쐬����
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
                // �Ō�ɐڑ������T�[�o�[���X�V
                initOpt.LastServerKey = sv.KeyName;
                // SQL �����N���X�̏�����
                SqlVersion sqlVer = new SqlVersion(con.ServerVersion);

                // ���C���_�C�A���O��\��
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

                // SQL SERVER�̃o�[�W�����ɉ�����DLL��ǂݍ���
                dllName = string.Format(System.Globalization.CultureInfo.CurrentCulture, Application.StartupPath + "\\SqlServer{0}Driver.dll", sqlVer.AdapterNameString);
                className = string.Format(System.Globalization.CultureInfo.CurrentCulture, "quickDBExplorer.SqlServerDriver{0}", sqlVer.AdapterNameString);
                asm = Assembly.LoadFrom(dllName);
                mainForm.SqlDriver = (ISqlInterface)asm.CreateInstance(className, true);

                mainForm.SqlDriver.SetConnection(con, mainForm.SqlTimeout);
                mainForm.InitPopupMenu();

                // MDI �Ȃ̂ŁA���[�h���X�Ń_�C�A���O��\������
                mainForm.Show();
                // ���C���_�C�A���O��\������΁A���̃_�C�A���O�͕s�v
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
		/// �ߋ��̃T�[�o�[�ڑ���������A�I������
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
                MessageBox.Show("�I���\�ȃT�[�o�[�͂���܂���");
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

