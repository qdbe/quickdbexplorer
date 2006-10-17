using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace MakeInsert
{
	/// <summary>
	/// Form3 の概要の説明です。
	/// </summary>
	public class Form3 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Button button1;
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button button2;
		protected	saveClass	ServerList;
		public	string selectedServer;
		public  string selectedInstance;

		public Form3(saveClass cl)
		{
			//
			// Windows フォーム デザイナ サポートに必要です。
			//
			InitializeComponent();

			//
			// TODO: InitializeComponent 呼び出しの後に、コンストラクタ コードを追加してください。
			//
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
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
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
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.button1.Location = new System.Drawing.Point(8, 264);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(120, 24);
			this.button1.TabIndex = 1;
			this.button1.Text = "サーバー選択";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.Location = new System.Drawing.Point(168, 264);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(120, 24);
			this.button2.TabIndex = 2;
			this.button2.Text = "キャンセル";
			// 
			// Form3
			// 
			this.AcceptButton = this.button1;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.CancelButton = this.button2;
			this.ClientSize = new System.Drawing.Size(292, 293);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.listBox1);
			this.Name = "Form3";
			this.ShowInTaskbar = false;
			this.Text = "Form3";
			this.Load += new System.EventHandler(this.Form3_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
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

		private void Form3_Load(object sender, System.EventArgs e)
		{
			foreach( object sd in ServerList.ht.Values )
			{
				this.listBox1.Items.Add(((ServerData)sd).Servername + ":" + ((ServerData)sd).InstanceName );
			}
			this.listBox1.Refresh();
		}

		private void listBox1_DoubleClick(object sender, System.EventArgs e)
		{
			this.button1.PerformClick();
		}
	}
}
