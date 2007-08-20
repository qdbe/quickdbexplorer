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
using System.Net;

namespace quickDBExplorer
{
	/// <summary>
	/// Form1 の概要の説明です。
	/// </summary>
	public class MainMDI : System.Windows.Forms.Form
	{
		private System.Windows.Forms.StatusBar statusBar1;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuConnect;
		private System.Windows.Forms.MenuItem menuWindow;
		private System.Windows.Forms.MenuItem menuNewConnect;
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.ErrorProvider errorProvider1;
		private System.Windows.Forms.MenuItem menuHelpMain;
		private System.Windows.Forms.MenuItem menuQuit;
		private System.Windows.Forms.MenuItem menuViewHelp;
		private System.Windows.Forms.MenuItem menuVersion;
		private System.Windows.Forms.MenuItem menuAbout;

		/// <summary>
		/// 前回操作時の各種記憶情報
		/// </summary>
		protected saveClass	initopt;
		/// <summary>
		/// 表示エラーメッセージ
		/// </summary>
		protected string  errMessage = "";



		/// <summary>
		/// コンストラクタ
		/// </summary>
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
			this.menuConnect = new System.Windows.Forms.MenuItem();
			this.menuNewConnect = new System.Windows.Forms.MenuItem();
			this.menuQuit = new System.Windows.Forms.MenuItem();
			this.menuWindow = new System.Windows.Forms.MenuItem();
			this.menuHelpMain = new System.Windows.Forms.MenuItem();
			this.menuViewHelp = new System.Windows.Forms.MenuItem();
			this.menuAbout = new System.Windows.Forms.MenuItem();
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider();
			this.menuVersion = new System.Windows.Forms.MenuItem();
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
																					  this.menuConnect,
																					  this.menuWindow,
																					  this.menuHelpMain});
			// 
			// menuConnect
			// 
			this.menuConnect.Index = 0;
			this.menuConnect.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuNewConnect,
																					  this.menuQuit});
			this.menuConnect.Text = "接続(&C)";
			// 
			// menuNewConnect
			// 
			this.menuNewConnect.Index = 0;
			this.menuNewConnect.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
			this.menuNewConnect.Text = "新規接続(&N)";
			this.menuNewConnect.Click += new System.EventHandler(this.menuNewConnect_Click);
			// 
			// menuQuit
			// 
			this.menuQuit.Index = 1;
			this.menuQuit.Text = "終了(&Q)";
			this.menuQuit.Click += new System.EventHandler(this.menuQuit_Click);
			// 
			// menuWindow
			// 
			this.menuWindow.Index = 1;
			this.menuWindow.MdiList = true;
			this.menuWindow.Text = "ウィンドウ(&W)";
			// 
			// menuHelpMain
			// 
			this.menuHelpMain.Index = 2;
			this.menuHelpMain.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuViewHelp,
																					  this.menuAbout,
																					  this.menuVersion});
			this.menuHelpMain.Text = "HELP(&H)";
			// 
			// menuViewHelp
			// 
			this.menuViewHelp.Index = 0;
			this.menuViewHelp.Text = "ヘルプ参照";
			this.menuViewHelp.Click += new System.EventHandler(this.menuViewHelp_Click);
			// 
			// menuAbout
			// 
			this.menuAbout.Index = 1;
			this.menuAbout.Text = "About";
			this.menuAbout.Click += new System.EventHandler(this.menuAbout_Click);
			// 
			// errorProvider1
			// 
			this.errorProvider1.ContainerControl = this;
			// 
			// menuVersion
			// 
			this.menuVersion.Index = 2;
			this.menuVersion.Text = "最新バージョンのチェック";
			this.menuVersion.Click += new System.EventHandler(this.menuVersion_Click);
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

		/// <summary>
		/// 接続メニュークリック時イベントハンドら
		/// </summary>
		/// <param name="sender">--</param>
		/// <param name="e">--</param>
		private void menuNewConnect_Click(object sender, System.EventArgs e)
		{
			LoginDialog logindlg = new LoginDialog(this.initopt);
			logindlg.MdiParent = this;
			logindlg.Show();
			logindlg.Focus();
		}

		/// <summary>
		/// エラーメッセージの初期化を行う
		/// </summary>
		protected void InitErrMessage()
		{
			this.statusBar1.Text = "";
			this.errMessage = "";
			this.errorProvider1.SetIconAlignment(this.statusBar1,ErrorIconAlignment.MiddleLeft);
			this.errorProvider1.SetError(this.statusBar1,"");
		}

		/// <summary>
		/// エラーメッセージを表示する
		/// </summary>
		/// <param name="ex">エラーが発生したException</param>
		protected void SetErrorMessage(Exception ex)
		{
			this.statusBar1.Text = ex.Message;
			this.errMessage = ex.ToString();
			this.errorProvider1.SetError(this.statusBar1,this.statusBar1.Text);
		}

		/// <summary>
		/// ステータスバーをダブルクリックすることで、エラーメッセージをクリップボードにコピーする
		/// </summary>
		/// <param name="sender">--</param>
		/// <param name="e">--</param>
		private void statusBar1_DoubleClick(object sender, System.EventArgs e)
		{
			if( this.errMessage != "" )
			{
				Clipboard.SetDataObject(this.errMessage,true );
			}
		
		}

		/// <summary>
		/// 画面Load時の処理
		/// </summary>
		/// <param name="sender">--</param>
		/// <param name="e">--</param>
		private void MainMDI_Load(object sender, System.EventArgs e)
		{

			// 設定ファイルの読み込みストリーム
			FileStream fs = null;

			// エラーメッセージを初期化
			this.InitErrMessage();

			initopt = new saveClass();
			try
			{
				// 設定ファイルを読み込み
				string path = Application.StartupPath;
				fs = new FileStream(path + "\\quickDBExplorer.xml", FileMode.Open);
				// Soap Serialize しているので、それをDeSerialize
				SoapFormatter sf = new SoapFormatter();
				if( fs != null && fs.CanRead )
				{
					initopt = (saveClass)sf.Deserialize(fs);
				}
			}
			catch(	System.IO.FileNotFoundException )
			{
				// 初回インストール時はファイルがない可能性がある
				;
			}
			catch( Exception exp )
			{
				this.SetErrorMessage(exp);
				initopt = new saveClass();
			}
			finally
			{
				if( fs != null )
				{
					fs.Close();
				}
			}
			// 最新バージョンの存在チェックを実施
			CheckNewVersion(false);

			// 最初は強制的にログインを表示する
			LoginDialog logindlg = new LoginDialog(this.initopt);
			logindlg.MdiParent = this;
			logindlg.Show();
			logindlg.Focus();
		}

		/// <summary>
		/// ダイアログのクローズ処理
		/// </summary>
		/// <param name="sender">--</param>
		/// <param name="e">--</param>
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

		private void menuQuit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void menuViewHelp_Click(object sender, System.EventArgs e)
		{
			string helpname = AppDomain.CurrentDomain.FriendlyName;
			// D:\godz\local\quickDBExplorer\trunc\quickDBExplorer\quickDBExplorerHelp.htm
			helpname = helpname.Replace(".exe","Help.htm");
			if( File.Exists(helpname) == true )
			{
				System.Diagnostics.Process.Start( helpname );
			}
		}

		private void menuAbout_Click(object sender, System.EventArgs e)
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

		private void menuVersion_Click(object sender, System.EventArgs e)
		{
			CheckNewVersion(true);
		}

		/// <summary>
		/// 最新バージョンが存在するかどうかをチェックする
		/// </summary>
		/// <param name="catchError"></param>
		protected void CheckNewVersion(bool catchError)
		{
			StreamReader sr = null;
			try
			{
				WebClient cl = new WebClient();
				sr = new StreamReader(cl.OpenRead("http://qdbe.rgr.jp/latestVersion.txt"));
				string lastversion = sr.ReadLine();
				if( lastversion.CompareTo(Application.ProductVersion) > 0 )
				{
					MessageBox.Show("最新バージョンが公開されています\r\nhttp://qdbe.rgr.jp/ をチェックしてください");
				}
			}
			catch(Exception ex)
			{
				if( catchError == true )
				{
					MessageBox.Show( ex.ToString() );
				}
			}
			finally
			{
				if( sr != null )
				{
					sr.Close();
				}
			}
		}
	}
}
