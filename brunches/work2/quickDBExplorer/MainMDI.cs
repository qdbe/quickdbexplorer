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
using serialFactory;
using System.Reflection;

namespace quickDBExplorer
{
	/// <summary>
	/// Form1 の概要の説明です。
	/// </summary>
	public class MainMDI : System.Windows.Forms.Form
	{
		private System.Windows.Forms.StatusBar statusBar1;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.ErrorProvider errorProvider1;

		protected saveClass	initopt;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		protected string  errMessage = "";
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem menuItem9;

		private SerialManager smanager = new SerialManager();



		public MainMDI()
		{
			//
			// Windows フォーム デザイナ サポートに必要です。
			//
			InitializeComponent();

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainMDI));
			this.statusBar1 = new System.Windows.Forms.StatusBar();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.SuspendLayout();
			// 
			// statusBar1
			// 
			this.statusBar1.Location = new System.Drawing.Point(0, 619);
			this.statusBar1.Name = "statusBar1";
			this.statusBar1.Size = new System.Drawing.Size(960, 22);
			this.statusBar1.TabIndex = 1;
			this.statusBar1.DoubleClick += new System.EventHandler(this.statusBar1_DoubleClick);
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1,
																					  this.menuItem2,
																					  this.menuItem4});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem3,
																					  this.menuItem5});
			this.menuItem1.Text = "接続(&C)";
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 0;
			this.menuItem3.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
			this.menuItem3.Text = "新規接続(&N)";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 1;
			this.menuItem5.Text = "終了(&Q)";
			this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.MdiList = true;
			this.menuItem2.Text = "ウィンドウ(&W)";
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 2;
			this.menuItem4.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem6,
																					  this.menuItem7,
																					  this.menuItem8,
																					  this.menuItem9});
			this.menuItem4.Text = "HELP(&H)";
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 0;
			this.menuItem6.Text = "ヘルプ参照";
			this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 1;
			this.menuItem7.Text = "ライセンス登録状況";
			this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 2;
			this.menuItem8.Text = "ライセンス更新登録";
			this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
			// 
			// errorProvider1
			// 
			this.errorProvider1.ContainerControl = this;
			// 
			// menuItem9
			// 
			this.menuItem9.Index = 3;
			this.menuItem9.Text = "About";
			this.menuItem9.Click += new System.EventHandler(this.menuItem9_Click);
			// 
			// MainMDI
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(960, 641);
			this.Controls.Add(this.statusBar1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.IsMdiContainer = true;
			this.Menu = this.mainMenu1;
			this.Name = "MainMDI";
			this.Text = "quickDBExplorer";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.MainMDI_Closing);
			this.Load += new System.EventHandler(this.MainMDI_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainMDI());
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			LoginDialog logindlg = new LoginDialog(this.initopt);
			logindlg.MdiParent = this;
			logindlg.Show();
			logindlg.Focus();
		}

		protected void InitErrMessage()
		{
			this.statusBar1.Text = "";
			this.errMessage = "";
			this.errorProvider1.SetIconAlignment(this.statusBar1,ErrorIconAlignment.MiddleLeft);
			this.errorProvider1.SetError(this.statusBar1,"");
		}

		protected void SetErrorMessage(Exception ex)
		{
			this.statusBar1.Text = ex.Message;
			this.errMessage = ex.ToString();
			this.errorProvider1.SetError(this.statusBar1,this.statusBar1.Text);
		}

		private void statusBar1_DoubleClick(object sender, System.EventArgs e)
		{
			if( this.errMessage != "" )
			{
				Clipboard.SetDataObject(this.errMessage,true );
			}
		
		}

		private void MainMDI_Load(object sender, System.EventArgs e)
		{
			smanager.SerialFileName = Application.ExecutablePath + ".auth";
			if( smanager.LoadAndCheckSerial() == false )
			{
				this.Close();
			}


			FileStream fs = null;

			this.InitErrMessage();

			initopt = new saveClass();
			try
			{
				string path = Application.StartupPath;
				fs = new FileStream(path + "\\quickDBExplorer.xml", FileMode.Open);
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
			catch( Exception exp )
			{
				this.SetErrorMessage(exp);
			}
			finally
			{
				if( fs != null )
				{
					fs.Close();
				}
			}

			// 最初は強制的にログインを表示する
			LoginDialog logindlg = new LoginDialog(this.initopt);
			logindlg.MdiParent = this;
			logindlg.Show();
			logindlg.Focus();
		}

		private void MainMDI_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			FileStream fs = null;

			try
			{
				string path = Application.StartupPath;
				fs = new FileStream(path + "\\quickDBExplorer.xml", FileMode.OpenOrCreate );
				fs.Close();
				fs = null;
				fs = new FileStream(path + "\\quickDBExplorer.xml", FileMode.Truncate, FileAccess.Write);
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
						sv.Servername = "(local)";
						sv.InstanceName = "";
						sv.IsUseTrust = false;
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

		private void menuItem5_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void menuItem6_Click(object sender, System.EventArgs e)
		{
			string helpname = AppDomain.CurrentDomain.FriendlyName;
			// D:\godz\local\quickDBExplorer\trunc\quickDBExplorer\quickDBExplorerHelp.htm
			helpname = helpname.Replace(".exe","Help.htm");
			if( File.Exists(helpname) == true )
			{
				System.Diagnostics.Process.Start( helpname );
			}
		}

		private void menuItem7_Click(object sender, System.EventArgs e)
		{
			this.smanager.ShowRegisterInfo();
		}

		private void menuItem8_Click(object sender, System.EventArgs e)
		{
			this.smanager.UpdateSerial();
		}

		private void menuItem9_Click(object sender, System.EventArgs e)
		{
			object []obj = System.Reflection.Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute),false);
			string aboutstr = Application.ProductName + " Version " + Application.ProductVersion;
			if( obj != null && obj.Length != 0)
			{
				aboutstr += Environment.NewLine + Environment.NewLine + ((AssemblyCopyrightAttribute)obj[0]).Copyright;
			}
			MessageBox.Show(
				aboutstr
				);
		}

	}
}
