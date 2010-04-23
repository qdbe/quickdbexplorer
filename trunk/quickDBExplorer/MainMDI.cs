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
	/// 全ての親画面となるMDIフォーム
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public class MainMdi : System.Windows.Forms.Form
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
		private ConditionRecorder	initopt;
		/// <summary>
		/// 表示エラーメッセージ
		/// </summary>
		private string  errMessage = "";

		/// <summary>
		/// コマンド引数の内容
		/// </summary>
		private Hashtable argHt = new Hashtable();

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			Application.Run(new MainMdi(args));
		}


		/// <summary>
		/// コンストラクタ
		/// </summary>
		public MainMdi(string[]args)
		{
			//
			// Windows フォーム デザイナ サポートに必要です。
			//
			InitializeComponent();
			this.ParseArg(args);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMdi));
			this.statusBar1 = new System.Windows.Forms.StatusBar();
			this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
			this.menuConnect = new System.Windows.Forms.MenuItem();
			this.menuNewConnect = new System.Windows.Forms.MenuItem();
			this.menuQuit = new System.Windows.Forms.MenuItem();
			this.menuWindow = new System.Windows.Forms.MenuItem();
			this.menuHelpMain = new System.Windows.Forms.MenuItem();
			this.menuViewHelp = new System.Windows.Forms.MenuItem();
			this.menuAbout = new System.Windows.Forms.MenuItem();
			this.menuVersion = new System.Windows.Forms.MenuItem();
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
			((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
			this.SuspendLayout();
			// 
			// statusBar1
			// 
			this.statusBar1.Location = new System.Drawing.Point(0, 635);
			this.statusBar1.Name = "statusBar1";
			this.statusBar1.Size = new System.Drawing.Size(1033, 22);
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
			// menuVersion
			// 
			this.menuVersion.Index = 2;
			this.menuVersion.Text = "最新バージョンのチェック";
			this.menuVersion.Click += new System.EventHandler(this.menuVersion_Click);
			// 
			// errorProvider1
			// 
			this.errorProvider1.ContainerControl = this;
			// 
			// MainMdi
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(1033, 657);
			this.Controls.Add(this.statusBar1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.IsMdiContainer = true;
			this.Menu = this.mainMenu1;
			this.Name = "MainMdi";
			this.Text = "quickDBExplorer";
			this.Load += new System.EventHandler(this.MainMdi_Load);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.MainMdi_Closing);
			((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// 接続メニュークリック時イベントハンドら
		/// </summary>
		/// <param name="sender">--</param>
		/// <param name="e">--</param>
		private void menuNewConnect_Click(object sender, System.EventArgs e)
		{
			LogOnDialog logindlg = new LogOnDialog(this.initopt);
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
		private void MainMdi_Load(object sender, System.EventArgs e)
		{
			//動作環境のチェック
			System.Version clrVer = System.Environment.Version;
			if (clrVer.Major < 2 ||
				( clrVer.Major == 2 &&
				  clrVer.Minor == 0 &&
				  clrVer.Build == 50727 &&
				  clrVer.MinorRevision < 1433 ) ||
				(clrVer.Major == 3 &&
				  clrVer.Minor == 0 &&
				  clrVer.Build == 4506 &&
				  clrVer.MinorRevision < 648)
				)
			{
				MessageBox.Show("動作環境が不適切です。.NET Framework 2.0 SP1 以上の環境が必要です。");
				this.Close();
			}

			// 設定ファイルの読み込みストリーム
			FileStream fs = null;

			// エラーメッセージを初期化
			this.InitErrMessage();

			initopt = new ConditionRecorder();
			try
			{
				// 設定ファイルを読み込み
				string path = Application.StartupPath;
				fs = new FileStream(path + "\\quickDBExplorer.xml", FileMode.Open);
				// Soap Serialize しているので、それをDeSerialize
				SoapFormatter sf = new SoapFormatter();
				if( fs != null && fs.CanRead )
				{
					initopt = (ConditionRecorder)sf.Deserialize(fs);
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
				initopt = new ConditionRecorder();
			}
			finally
			{
				if( fs != null )
				{
					fs.Close();
				}
			}
			// 最新バージョンの存在チェックを実施
			// 自動的にやってしまうと問題なので、ここではやらない
			//CheckNewVersion(false);

			// 最初は強制的にログインを表示する
			LogOnDialog logindlg ;
            logindlg = new LogOnDialog(this.initopt, argHt);
			logindlg.MdiParent = this;
			logindlg.Show();
			logindlg.Focus();
		}

		/// <summary>
		/// ダイアログのクローズ処理
		/// </summary>
		/// <param name="sender">--</param>
		/// <param name="e">--</param>
		private void MainMdi_Closing(object sender, System.ComponentModel.CancelEventArgs e)
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
					foreach( object keys in initopt.PerServerData.Keys )
					{
						if( ((ServerData)(initopt.PerServerData[keys])).IsSaveKey == false )
						{
							ar.Add((string)keys);
						}
					}
					foreach( string kk in ar )
					{
						initopt.PerServerData.Remove(kk);
					}
					if( initopt.PerServerData.Count == 0 )
					{
						ServerData sv = new ServerData();
						sv.Servername = "(local)";
						sv.InstanceName = "";
						sv.IsUseTrust = false;
						initopt.PerServerData.Add(sv.KeyName,sv);
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
#if true
            // こっちが正しい
			qdbeAboutBox dlg = new qdbeAboutBox();
			dlg.Show();
#else
            //object []obj = System.Reflection.Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute),false);
            //string aboutstr = Application.ProductName + " Version " + Application.ProductVersion;
            //if( obj != null && obj.Length != 0)
            //{
            //    aboutstr += Environment.NewLine + Environment.NewLine + ((AssemblyCopyrightAttribute)obj[0]).Copyright;
            //}
            //MessageBox.Show(
            //    aboutstr
            //    );
#endif
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
                if (lastversion.CompareTo(Application.ProductVersion) > 0)
                {
                    MessageBox.Show("最新バージョンが公開されています\r\nhttp://code.google.com/p/quickdbexplorer/ をチェックしてください");
                }
                else
                {
                    MessageBox.Show("既に最新バージョンです。更新の必要はありません。");
                }
			}
#if DEBUG
			catch(Exception ex)
			{
                MessageBox.Show("最新かどうかチェックできませんでした。ネットワーク環境を調査して下さい。\r\nhttp://qdbe.rgr.jp/へのアクセスが可能である必要です");
                if (catchError == true)
                {
                    MessageBox.Show(ex.ToString());

                }
			}
#else
            catch
            {
                MessageBox.Show("最新かどうかチェックできませんでした。ネットワーク環境を調査して下さい。\r\nhttp://qdbe.rgr.jp/へのアクセスが可能である必要です");

			}
#endif
			finally
			{
				if( sr != null )
				{
					sr.Close();
				}
			}
		}

        /// <summary>
        /// コマンド引数をパースする
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
		private bool ParseArg(string[] args)
		{
			string preParam = string.Empty;

			foreach (string arg in args)
			{
				switch (arg)
				{
					case "-S":
					case "-s":
						// サーバー名指定
						if (preParam != string.Empty)
						{
							return false;
						}
						preParam = LogOnDialog.PARAM_SERVER;
						break;
					case "-I":
					case "-i":
						// インスタンス名指定
						if (preParam != string.Empty)
						{
							return false;
						}
						preParam = LogOnDialog.PARAM_INSTANCE;
						break;
					case "-U":
					case "-u":
						// ユーザー名指定
						if (preParam != string.Empty)
						{
							return false;
						}
						preParam = LogOnDialog.PARAM_USER;
						break;
					case "-P":
					case "-p":
						// パスワード指定
						if (preParam != string.Empty)
						{
							return false;
						}
						preParam = LogOnDialog.PARAM_PASSWORD;
						break;
					case "-T":
					case "-t":
						// Windows 認証指定
						this.argHt[LogOnDialog.PARAM_TRUST] = true;
						preParam = string.Empty;
						break;
					default:
						//引数以外の場合は値扱い
						if (preParam == string.Empty)
						{
							return false;
						}
						this.argHt[preParam] = arg;
						preParam = string.Empty;
						break;
				}
			}
            if( this.argHt.Count > 0 &&
                ( !this.argHt.Contains(LogOnDialog.PARAM_SERVER) ||
                  !( this.argHt.Contains(LogOnDialog.PARAM_TRUST) ||
                    ( this.argHt.Contains(LogOnDialog.PARAM_USER) &&
                this.argHt.Contains(LogOnDialog.PARAM_PASSWORD) ))
                ) )
            {
                MessageBox.Show("引数が不正です。ヘルプを参照して下さい");
                this.Close();
            }
			return true;
		}
	}
}
