using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace quickDBExplorer
{
	/// <summary>
	/// ServerSelectDialog �̊T�v�̐����ł��B
	/// </summary>
	public class ServerSelectDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Button btnOk;
		/// <summary>
		/// �K�v�ȃf�U�C�i�ϐ��ł��B
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button btnCancel;
		private	saveClass	ServerList;

		/// <summary>
		/// �I�����ꂽ�T�[�o�[��
		/// </summary>
		protected	string selectedServer;
		/// <summary>
		/// �I�����ꂽ�T�[�o�[��
		/// </summary>
		public	string SelectedServer
		{
			get { return this.selectedServer; }
			set { this.selectedServer = value; }
		}
		/// <summary>
		/// �I�����ꂽ�C���X�^���X��
		/// </summary>
		protected  string selectedInstance;
		/// <summary>
		/// �I�����ꂽ�C���X�^���X��
		/// </summary>
		public  string SelectedInstance
		{
			get { return this.selectedInstance; }
			set { this.selectedInstance = value; }
		}

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		/// <param name="cl">�ߋ��̐ڑ��T�[�o�[�������</param>
		public ServerSelectDialog(saveClass cl)
		{
			//
			// Windows �t�H�[�� �f�U�C�i �T�|�[�g�ɕK�v�ł��B
			//
			InitializeComponent();

			ServerList = cl;
			selectedServer = "";
			selectedInstance = "";
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ServerSelectDialog));
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listBox1
			// 
			this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.listBox1.ItemHeight = 12;
			this.listBox1.Location = new System.Drawing.Point(8, 8);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(280, 244);
			this.listBox1.TabIndex = 0;
			this.listBox1.DoubleClick += new System.EventHandler(this.listBox1_DoubleClick);
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(8, 264);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(120, 24);
			this.btnOk.TabIndex = 1;
			this.btnOk.Text = "�T�[�o�[�I��(&O)";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(168, 264);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(120, 24);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "�L�����Z��(&X)";
			// 
			// ServerSelectDialog
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(292, 293);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.listBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ServerSelectDialog";
			this.ShowInTaskbar = false;
			this.Text = "�T�[�o�[�I��";
			this.Load += new System.EventHandler(this.ServerSelectDialog_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			if( this.listBox1.SelectedItem != null )
			{
				string delimStr = ":";
				string []str = this.listBox1.SelectedItem.ToString().Split(delimStr.ToCharArray(), 2);
				this.selectedServer = str[0];
				this.selectedInstance = str[1];
			}
			else
			{
				this.selectedServer = "";
				this.selectedInstance = "";
			}
		}

		private void ServerSelectDialog_Load(object sender, System.EventArgs e)
		{
			foreach( object sd in ServerList.PerServerData.Values )
			{
				this.listBox1.Items.Add(((ServerData)sd).Servername + ":" + ((ServerData)sd).InstanceName );
			}
			this.listBox1.Refresh();
		}

		private void listBox1_DoubleClick(object sender, System.EventArgs e)
		{
			this.btnOk.PerformClick();
		}
	}
}
