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
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button button1;
		private System.Data.SqlClient.SqlConnection sqlConnection1;
		private System.Windows.Forms.Button button2;
		protected saveClass	initopt;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ToolTip toolTip1;
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
				MessageBox.Show(e.ToString()+ e.StackTrace);
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
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.sqlConnection1 = new System.Data.SqlClient.SqlConnection();
			this.button2 = new System.Windows.Forms.Button();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.label5 = new System.Windows.Forms.Label();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(144, 56);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(208, 19);
			this.textBox1.TabIndex = 1;
			this.textBox1.Text = "(local)";
			this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
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
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(144, 96);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(208, 19);
			this.textBox2.TabIndex = 3;
			this.textBox2.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(40, 136);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(88, 23);
			this.label3.TabIndex = 4;
			this.label3.Text = "ユーザーID";
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(144, 136);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(208, 19);
			this.textBox3.TabIndex = 4;
			this.textBox3.Text = "sa";
			// 
			// textBox4
			// 
			this.textBox4.Location = new System.Drawing.Point(144, 176);
			this.textBox4.Name = "textBox4";
			this.textBox4.PasswordChar = '*';
			this.textBox4.Size = new System.Drawing.Size(208, 19);
			this.textBox4.TabIndex = 5;
			this.textBox4.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(40, 176);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(88, 23);
			this.label4.TabIndex = 7;
			this.label4.Text = "パスワード";
			// 
			// button1
			// 
			this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.button1.Location = new System.Drawing.Point(40, 216);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(88, 24);
			this.button1.TabIndex = 6;
			this.button1.Text = "接続";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(40, 8);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(304, 24);
			this.button2.TabIndex = 0;
			this.button2.Text = "過去に接続したサーバーから選択";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// checkBox1
			// 
			this.checkBox1.Location = new System.Drawing.Point(368, 56);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(128, 16);
			this.checkBox1.TabIndex = 2;
			this.checkBox1.Text = "接続先を保存する";
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("MS UI Gothic", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(128)));
			this.label5.Location = new System.Drawing.Point(400, 224);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(48, 16);
			this.label5.TabIndex = 8;
			this.label5.Text = "C Info;";
			this.toolTip1.SetToolTip(this.label5, "Copyright; Y.N(godz)  2004-2006");
			// 
			// Form1
			// 
			this.AcceptButton = this.button1;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(528, 245);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textBox4);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "Form1";
			this.Text = "SQL Add on Tool";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
			this.Load += new System.EventHandler(this.Form1_Load);
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

		private void textBox1_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			// ローカルのファイルから　オプションを読み込む

			this.checkBox1.Checked = true;
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			String myConnString;
			if( this.textBox2.Text != "" )
			{
				myConnString = "Server=" + this.textBox1.Text + @"\" + this.textBox2.Text + ";"
					+"Database=master;User ID="+this.textBox3.Text
					+";Password="+this.textBox4.Text;			}
			else
			{
				myConnString = "Server=" + this.textBox1.Text + ";"
					+"Database=master;User ID="+this.textBox3.Text
					+";Password="+this.textBox4.Text;
			}
			

			try 
			{
				System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection();
				con.ConnectionString = myConnString;
				con.Open();

				ServerData sv = new ServerData();
				sv.Servername = this.textBox1.Text;
				sv.InstanceName = this.textBox2.Text;
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

				Form2 fm2 = new Form2(sv);
				fm2.servername = this.textBox1.Text;
				if( this.textBox2.Text != "" )
				{
					fm2.servername = this.textBox1.Text + "@" + this.textBox2.Text;
				}
				else
				{
					fm2.servername = this.textBox1.Text;
				}
				fm2.sqlConnection1 = con;
				fm2.Show();
				//fm2.ShowDialog();
			}
			catch ( System.Data.SqlClient.SqlException se)
			{
				MessageBox.Show(se.Message+":"+se.StackTrace+":\n"+se.ToString());
			}
			finally {
				this.sqlConnection1.Close();
			}
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			if( this.initopt.ht.Count > 0 )
			{
				Form3 dlg = new Form3(this.initopt);
			
				if( dlg.ShowDialog() == DialogResult.OK	)
				{
					this.textBox1.Text = dlg.selectedServer;
					this.textBox2.Text = dlg.selectedInstance;
					this.textBox4.Text = "";
					this.textBox4.Focus();
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
						sv.Servername = this.textBox1.Text;
						sv.InstanceName = this.textBox2.Text;
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
				MessageBox.Show(ex.ToString()+ ex.StackTrace);
			}
			finally
			{
				if( fs != null )
				{
					fs.Close();
				}
			}
		}
	}
}
