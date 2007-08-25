using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace quickDBExplorer
{
	/// <summary>
	/// ServerSelectDialog の概要の説明です。
	/// </summary>
	public class ServerSelectDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Button btnOk;
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button btnCancel;
		private	saveClass	ServerList;

		/// <summary>
		/// 選択されたサーバー名
		/// </summary>
		protected	string selectedServer;
		/// <summary>
		/// 選択されたサーバー名
		/// </summary>
		public	string SelectedServer
		{
			get { return this.selectedServer; }
			set { this.selectedServer = value; }
		}
		/// <summary>
		/// 選択されたインスタンス名
		/// </summary>
		protected  string selectedInstance;
		/// <summary>
		/// 選択されたインスタンス名
		/// </summary>
		public  string SelectedInstance
		{
			get { return this.selectedInstance; }
			set { this.selectedInstance = value; }
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="cl">過去の接続サーバー履歴情報</param>
		public ServerSelectDialog(saveClass cl)
		{
			//
			// Windows フォーム デザイナ サポートに必要です。
			//
			InitializeComponent();

			ServerList = cl;
			selectedServer = "";
			selectedInstance = "";
		}

		/// <summary>
		/// 使用されているリソースに後処理を実行します。
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

		#region Windows フォーム デザイナで生成されたコード 
		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
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
			this.btnOk.Text = "サーバー選択(&O)";
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
			this.btnCancel.Text = "キャンセル(&X)";
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
			this.Text = "サーバー選択";
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
