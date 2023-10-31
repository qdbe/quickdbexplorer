using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
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
using quickDBExplorer.Forms;
using quickDBExplorer.Forms.Events;
using quickDBExplorer.manager;
using System.Diagnostics;

namespace quickDBExplorer.Forms
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
        /// 各種設定値（履歴含む）
        /// </summary>
        protected ConditionRecorderJson _initOpt = null;
        /// <summary>
        /// 前回操作時の各種記憶情報
        /// </summary>
        public ConditionRecorderJson InitOpt {
            get {
                return this._initOpt; 
            }
            set { this._initOpt = value; }
        }
    
        /// <summary>
        /// 表示エラーメッセージ
        /// </summary>
        private string errMessage = "";

        /// <summary>
        /// コマンド引数の内容
        /// </summary>
        private Hashtable argHt = new Hashtable();
        private MenuItem menuBookamrk;
        private MenuItem menuAddBookMark;
        private MenuItem menuEditBookmark;

        /// <summary>
        /// サーバー別ブックマーク
        /// </summary>
        private Dictionary<string, List<Forms.BookmarkInfo>> bookMarks;
        private MenuItem menuBookMarkSeparator;
        private MenuItem menuTools;
        private MenuItem menuToolEdit;
        private MenuItem menuToolSeparator;

        private BookmarkManager bmManager;

        private List<ToolInfo> outerTools;
        private ToolManager outerToolManager;
        private MenuItem menuReConnect;
        private MenuItem menuItem1;
        private MenuItem menuResetLayout;
        private MenuItem menuItem2;
        private MenuItem menuOptNullOrEmpty;
        private MenuItem menuOptNull;
        private MenuItem menuOptEmpty;
        private MenuItem menuOptWidth;
        private MenuItem menuOptWidthDefalut;
        private MenuItem menuOptWidthFull;
        private MenuItem menuItem3;
        private MenuItem menuOptFilterNoCS;
        private MenuItem menuOptFilterCS;
        private ToolMacroManager toolMacroManager;


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainMdi(string[] args)
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
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
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
            this.menuReConnect = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuQuit = new System.Windows.Forms.MenuItem();
            this.menuWindow = new System.Windows.Forms.MenuItem();
            this.menuResetLayout = new System.Windows.Forms.MenuItem();
            this.menuBookamrk = new System.Windows.Forms.MenuItem();
            this.menuAddBookMark = new System.Windows.Forms.MenuItem();
            this.menuEditBookmark = new System.Windows.Forms.MenuItem();
            this.menuBookMarkSeparator = new System.Windows.Forms.MenuItem();
            this.menuTools = new System.Windows.Forms.MenuItem();
            this.menuToolEdit = new System.Windows.Forms.MenuItem();
            this.menuToolSeparator = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuOptNullOrEmpty = new System.Windows.Forms.MenuItem();
            this.menuOptNull = new System.Windows.Forms.MenuItem();
            this.menuOptEmpty = new System.Windows.Forms.MenuItem();
            this.menuHelpMain = new System.Windows.Forms.MenuItem();
            this.menuViewHelp = new System.Windows.Forms.MenuItem();
            this.menuAbout = new System.Windows.Forms.MenuItem();
            this.menuVersion = new System.Windows.Forms.MenuItem();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.menuOptWidth = new System.Windows.Forms.MenuItem();
            this.menuOptWidthDefalut = new System.Windows.Forms.MenuItem();
            this.menuOptWidthFull = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuOptFilterNoCS = new System.Windows.Forms.MenuItem();
            this.menuOptFilterCS = new System.Windows.Forms.MenuItem();
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
            this.menuBookamrk,
            this.menuTools,
            this.menuItem2,
            this.menuHelpMain});
            // 
            // menuConnect
            // 
            this.menuConnect.Index = 0;
            this.menuConnect.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuNewConnect,
            this.menuReConnect,
            this.menuItem1,
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
            // menuReConnect
            // 
            this.menuReConnect.Index = 1;
            this.menuReConnect.Text = "再接続";
            this.menuReConnect.Click += new System.EventHandler(this.menuReConnect_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 2;
            this.menuItem1.Text = "設定を保存(&S)";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuQuit
            // 
            this.menuQuit.Index = 3;
            this.menuQuit.Text = "終了(&Q)";
            this.menuQuit.Click += new System.EventHandler(this.menuQuit_Click);
            // 
            // menuWindow
            // 
            this.menuWindow.Index = 1;
            this.menuWindow.MdiList = true;
            this.menuWindow.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuResetLayout});
            this.menuWindow.Text = "ウィンドウ(&W)";
            // 
            // menuResetLayout
            // 
            this.menuResetLayout.Index = 0;
            this.menuResetLayout.Text = "ウィンドウレイアウト初期化";
            this.menuResetLayout.Click += new System.EventHandler(this.menuResetLayout_Click);
            // 
            // menuBookamrk
            // 
            this.menuBookamrk.Index = 2;
            this.menuBookamrk.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuAddBookMark,
            this.menuEditBookmark,
            this.menuBookMarkSeparator});
            this.menuBookamrk.Text = "Bookmark(&Q)";
            this.menuBookamrk.Popup += new System.EventHandler(this.menuBookamrk_Popup);
            // 
            // menuAddBookMark
            // 
            this.menuAddBookMark.Index = 0;
            this.menuAddBookMark.Shortcut = System.Windows.Forms.Shortcut.CtrlM;
            this.menuAddBookMark.Text = "追加(&M)";
            this.menuAddBookMark.Click += new System.EventHandler(this.menuAddBookMark_Click);
            // 
            // menuEditBookmark
            // 
            this.menuEditBookmark.Index = 1;
            this.menuEditBookmark.Shortcut = System.Windows.Forms.Shortcut.CtrlB;
            this.menuEditBookmark.Text = "管理(&B)";
            this.menuEditBookmark.Click += new System.EventHandler(this.menuEditBookmark_Click);
            // 
            // menuBookMarkSeparator
            // 
            this.menuBookMarkSeparator.Index = 2;
            this.menuBookMarkSeparator.Text = "-";
            // 
            // menuTools
            // 
            this.menuTools.Index = 3;
            this.menuTools.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuToolEdit,
            this.menuToolSeparator});
            this.menuTools.Text = "外部ツール(&R)";
            this.menuTools.Popup += new System.EventHandler(this.menuTools_Popup);
            // 
            // menuToolEdit
            // 
            this.menuToolEdit.Index = 0;
            this.menuToolEdit.Text = "管理";
            this.menuToolEdit.Click += new System.EventHandler(this.menuToolEdit_Click);
            // 
            // menuToolSeparator
            // 
            this.menuToolSeparator.Index = 1;
            this.menuToolSeparator.Text = "-";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 4;
            this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuOptNullOrEmpty,
            this.menuOptWidth,
            this.menuItem3});
            this.menuItem2.Text = "オプション(&T)";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // menuOptNullOrEmpty
            // 
            this.menuOptNullOrEmpty.Index = 0;
            this.menuOptNullOrEmpty.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuOptNull,
            this.menuOptEmpty});
            this.menuOptNullOrEmpty.Text = "読込時の空白の扱い";
            // 
            // menuOptNull
            // 
            this.menuOptNull.Checked = true;
            this.menuOptNull.Index = 0;
            this.menuOptNull.Text = "NULL";
            this.menuOptNull.Click += new System.EventHandler(this.menuOptNullEmpty_Click);
            // 
            // menuOptEmpty
            // 
            this.menuOptEmpty.Index = 1;
            this.menuOptEmpty.Text = "空文字";
            this.menuOptEmpty.Click += new System.EventHandler(this.menuOptNullEmpty_Click);
            // 
            // menuHelpMain
            // 
            this.menuHelpMain.Index = 5;
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
            // menuOptWidth
            // 
            this.menuOptWidth.Index = 1;
            this.menuOptWidth.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuOptWidthDefalut,
            this.menuOptWidthFull});
            this.menuOptWidth.Text = "グリッド幅";
            // 
            // menuOptWidthDefalut
            // 
            this.menuOptWidthDefalut.Index = 0;
            this.menuOptWidthDefalut.Text = "既定値";
            this.menuOptWidthDefalut.Click += new System.EventHandler(this.menuOptWidth_Click);
            // 
            // menuOptWidthFull
            // 
            this.menuOptWidthFull.Index = 1;
            this.menuOptWidthFull.Text = "全表示";
            this.menuOptWidthFull.Click += new System.EventHandler(this.menuOptWidth_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 2;
            this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuOptFilterNoCS,
            this.menuOptFilterCS});
            this.menuItem3.Text = "オブジェクトフィルタ";
            // 
            // menuOptFilterNoCS
            // 
            this.menuOptFilterNoCS.Index = 0;
            this.menuOptFilterNoCS.Text = "大文字小文字を区別しない";
            this.menuOptFilterNoCS.Click += new System.EventHandler(this.menuOptFilter_Click);
            // 
            // menuOptFilterCS
            // 
            this.menuOptFilterCS.Index = 1;
            this.menuOptFilterCS.Text = "大文字小文字を区別する";
            this.menuOptFilterCS.Click += new System.EventHandler(this.menuOptFilter_Click);
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
            this.Activated += new System.EventHandler(this.MainMdi_Activated);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainMdi_Closing);
            this.Deactivate += new System.EventHandler(this.MainMdi_Deactivate);
            this.Load += new System.EventHandler(this.MainMdi_Load);
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
            OpenLogOnDlg(null);
        }

        private void OpenLogOnDlg(Hashtable argHt)
        {

            LogOnDialog logindlg;
            if (argHt == null)
            {
                logindlg = new LogOnDialog(this.InitOpt);
            }
            else
            {
                logindlg = new LogOnDialog(this.InitOpt, argHt);
            }
            logindlg.MdiParent = this;
            logindlg.LoginConnected += new LoginConnectedHandler(logindlg_LoginConnected);
            logindlg.Show();
            logindlg.Focus();
        }

        void logindlg_LoginConnected(ConnectionInfo conInfo)
        {
            MainForm mainform = new MainForm(this, conInfo);
            mainform.Enter += new EventHandler(mainform_Enter);
            mainform.Show();
        }

        void mainform_Enter(object sender, EventArgs e)
        {
            SetBookmarkPopupList((MainForm)sender);
            SetOption((MainForm)sender);
        }

        private void SetOption(MainForm sender)
        {
            if (sender != null)
            {
                SetOptNullEmpty(sender);
                SetGridDefaultWidth(sender);
                SetOptFilter(sender);
            }
        }

        private void SetOptNullEmpty(MainForm sender)
        {
            if (sender.ReadEmptyAsNull)
            {
                this.menuOptNull.Checked = true;
                this.menuOptEmpty.Checked = false;
            }
            else
            {
                this.menuOptNull.Checked = false;
                this.menuOptEmpty.Checked = true;
            }
        }

        private void SetOptFilter(MainForm sender)
        {
            if (sender.IsFilterCaseSensitive)
            {
                this.menuOptFilterNoCS.Checked = false;
                this.menuOptFilterCS.Checked = true;
            }
            else
            {
                this.menuOptFilterNoCS.Checked = true;
                this.menuOptFilterCS.Checked = false;
            }

            sender.SetFilterCS();
        }


        /// <summary>
        /// グリッドの幅の初期値を設定する
        /// </summary>
        /// <param name="sender"></param>
        private void SetGridDefaultWidth(MainForm sender)
        {
            if (sender.GridDefaltWidth)
            {
                this.menuOptWidthDefalut.Checked = true;
                this.menuOptWidthFull.Checked = false;
            }
            else
            {
                this.menuOptWidthDefalut.Checked = false;
                this.menuOptWidthFull.Checked = true;
            }
        }

        /// <summary>
        /// エラーメッセージの初期化を行う
        /// </summary>
        protected void InitErrMessage()
        {
            this.statusBar1.Text = "";
            this.errMessage = "";
            this.errorProvider1.SetIconAlignment(this.statusBar1, ErrorIconAlignment.MiddleLeft);
            this.errorProvider1.SetError(this.statusBar1, "");
        }

        /// <summary>
        /// エラーメッセージを表示する
        /// </summary>
        /// <param name="ex">エラーが発生したException</param>
        protected void SetErrorMessage(Exception ex)
        {
            this.statusBar1.Text = ex.Message;
            this.errMessage = ex.ToString();
            this.errorProvider1.SetError(this.statusBar1, this.statusBar1.Text);
        }

        /// <summary>
        /// ステータスバーをダブルクリックすることで、エラーメッセージをクリップボードにコピーする
        /// </summary>
        /// <param name="sender">--</param>
        /// <param name="e">--</param>
        private void statusBar1_DoubleClick(object sender, System.EventArgs e)
        {
            if (this.errMessage != "")
            {
                Clipboard.SetDataObject(this.errMessage, true);
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
                (clrVer.Major == 2 &&
                  clrVer.Minor == 0 &&
                  clrVer.Build == 50727 &&
                  clrVer.MinorRevision < 1433) ||
                (clrVer.Major == 3 &&
                  clrVer.Minor == 0 &&
                  clrVer.Build == 4506 &&
                  clrVer.MinorRevision < 648)
                )
            {
                MessageBox.Show("動作環境が不適切です。.NET Framework 2.0 SP1 以上の環境が必要です。");
                this.Close();
            }


            // エラーメッセージを初期化
            this.InitErrMessage();

            try
            {
                this.InitOpt = ConditionRecorderJson.Create();
            }
            catch (Exception exp)
            {
                this.SetErrorMessage(exp);
                this.InitOpt = new ConditionRecorderJson();
            }

            // 最新バージョンの存在チェックを実施
            // 自動的にやってしまうと問題なので、ここではやらない
            //CheckNewVersion(false);

            // Bookmark処理
            InitBookMark();

            InitTools();

            // 最初は強制的にログインを表示する
            OpenLogOnDlg(argHt);
        }


        private void InitTools()
        {
            this.outerToolManager = new ToolManager();
            this.toolMacroManager = new ToolMacroManager();
            this.outerTools = this.outerToolManager.Load();
        }

        private void InitBookMark()
        {
            bmManager = new BookmarkManager();
            this.bookMarks = bmManager.Load();
        }

        /// <summary>
        /// ダイアログのクローズ処理
        /// </summary>
        /// <param name="sender">--</param>
        /// <param name="e">--</param>
        private void MainMdi_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveSettings();
        }

        private void SaveSettings()
        {
            try
            {
                this.InitOpt.Save();

                if (this.bmManager != null)
                {
                    this.bmManager.Save(this.bookMarks);
                }
                if (this.outerToolManager != null)
                {
                    this.outerToolManager.Save(this.outerTools);
                }
            }
            catch (Exception ex)
            {
                this.SetErrorMessage(ex);
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
            helpname = helpname.Replace(".exe", "Help.htm");
            if (File.Exists(helpname) == true)
            {
                System.Diagnostics.Process.Start(helpname);
            }
        }

        private void menuAbout_Click(object sender, System.EventArgs e)
        {
#if true
            // こっちが正しい
            qdbeAboutBox dlg = new qdbeAboutBox();
            dlg.Show(this);
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
            catch (Exception ex)
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
                if (sr != null)
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
            if (this.argHt.Count > 0 &&
                (!this.argHt.Contains(LogOnDialog.PARAM_SERVER) ||
                  !(this.argHt.Contains(LogOnDialog.PARAM_TRUST) ||
                    (this.argHt.Contains(LogOnDialog.PARAM_USER) &&
                this.argHt.Contains(LogOnDialog.PARAM_PASSWORD)))
                ))
            {
                MessageBox.Show("引数が不正です。ヘルプを参照して下さい");
                this.Close();
            }
            return true;
        }

        private void menuBookamrk_Popup(object sender, EventArgs e)
        {
            this.menuBookamrk.MenuItems.Clear();
            if (this.ActiveMdiChild != null &&
                this.ActiveMdiChild is MainForm )
            {
                this.menuBookamrk.MenuItems.Add(menuAddBookMark);
            }
            this.menuBookamrk.MenuItems.Add(menuEditBookmark);
            if (this.ActiveMdiChild != null &&
                this.ActiveMdiChild is MainForm)
            {
                this.menuBookamrk.MenuItems.Add(menuBookMarkSeparator);
                SetBookmarkPopupList((MainForm)this.ActiveMdiChild);
            }
        }

        private void SetBookmarkPopupList(MainForm mainForm)
        {
            if (!bookMarks.ContainsKey(mainForm.ServerName))
            {
                return;
            }
            foreach (BookmarkInfo each in bookMarks[mainForm.ServerName])
            {
                MenuItem itm = new MenuItem(each.Name);
                itm.Click += new EventHandler(BookmarkItemClick);
                itm.Tag = each;
                this.menuBookamrk.MenuItems.Add(itm);
            }
        }

        private void BookmarkItemClick(object sender, EventArgs e)
        {
            MenuItem itm = (MenuItem)sender;
            if (itm.Tag == null || !(itm.Tag is BookmarkInfo))
            {
                return;
            }

            MainForm form = (MainForm)this.ActiveMdiChild;
            form.LoadBookmark((BookmarkInfo)itm.Tag);
        }

        private void menuAddBookMark_Click(object sender, EventArgs e)
        {
            MainForm form = ((MainForm)this.ActiveMdiChild);
            BookmarkInfo addinfo = form.GetCurrentBookmarkInfo();
            List<BookmarkInfo> list;
            if (this.bookMarks.ContainsKey(form.ServerName) == false)
            {
                list = new List<BookmarkInfo>();
                this.bookMarks.Add(form.ServerName, list);
            }
            else
            {
                list = this.bookMarks[form.ServerName];
            }
            // check Name Duplicate
            foreach (BookmarkInfo each in list)
            {
                if (each.Name == addinfo.Name)
                {
                    MessageBox.Show("既に同じ名前のブックマークが登録されています");
                    return;
                }
            }
            list.Add(addinfo);
        }

        private void menuEditBookmark_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild == null ||
                !(this.ActiveMdiChild is MainForm))
            {
                // ログイン後
                return;
            }
            MainForm form = ((MainForm)this.ActiveMdiChild);
            if (this.bookMarks.ContainsKey(form.ServerName) == false)
            {
                MessageBox.Show("対象サーバーのブックマークはありません");
                return;
            }
            List<BookmarkInfo> bookmarklist = this.bookMarks[form.ServerName];
            if (bookmarklist.Count == 0)
            {
                MessageBox.Show("対象サーバーのブックマークはありません");
                return;
            }
            Forms.Dialog.BookMarkEditForm bookmarkEditor = new quickDBExplorer.Forms.Dialog.BookMarkEditForm(bookmarklist);
            bookmarkEditor.ShowDialog(this);
        }

        private void menuToolEdit_Click(object sender, EventArgs e)
        {
            MacroArgInfo arg = GetMacroArg();
            toolMacroManager.SetMacroArg(arg);

            Forms.Dialog.OuterToolEditForm dlg = new quickDBExplorer.Forms.Dialog.OuterToolEditForm(outerTools, toolMacroManager);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                this.outerTools = dlg.ResultToolList;
            }
        }

        private MacroArgInfo GetMacroArg()
        {
            MacroArgInfo arg;
            if (this.ActiveMdiChild == null ||
                !(this.ActiveMdiChild is MainForm))
            {
                ConnectionInfo dummySetting = ConnectionInfo.CreateDummyConnection();
                arg = new MacroArgInfo(dummySetting, string.Empty, string.Empty);
            }
            else
            {
                arg = ((MainForm)this.ActiveMdiChild).CreateMacroArg();
            }
            return arg;
        }

        private void menuTools_Popup(object sender, EventArgs e)
        {
            SetToolPopupList();
        }

        private void SetToolPopupList()
        {
            this.menuTools.MenuItems.Clear();
            this.menuTools.MenuItems.Add(menuToolEdit);
            if (outerTools.Count > 0)
            {
                this.menuTools.MenuItems.Add(new MenuItem("-"));
            }
            foreach (ToolInfo each in outerTools)
            {
                MenuItem itm = new MenuItem(each.Name);
                itm.Click += new EventHandler(toolItemClick);
                itm.Tag = each;
                this.menuTools.MenuItems.Add(itm);
            }
        }

        void toolItemClick(object sender, EventArgs e)
        {
            ToolInfo info = (ToolInfo)((MenuItem)sender).Tag;
            MacroArgInfo arg = GetMacroArg();
            string commandstr = this.toolMacroManager.BuildCommand(info.Command, arg);
            string argstr = this.toolMacroManager.BuildCommand(info.Args, arg);

            StartProcess(commandstr,argstr);
        }


        private void StartProcess(string commandstr,string arg)
        {
            try
            {
                ProcessStartInfo startinfo = new ProcessStartInfo();
                startinfo.ErrorDialog = true;
                startinfo.ErrorDialogParentHandle = this.Handle;
                startinfo.FileName = commandstr;
                startinfo.Arguments = arg;
                startinfo.UseShellExecute = true;
                Process.Start(startinfo);
            }
            catch 
            {
            }
        }

        private void menuReConnect_Click(object sender, EventArgs e)
        {
            MainForm main = (MainForm)this.ActiveMdiChild;
            main.ReConnect();
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            this.SaveSettings();
        }

        private void menuResetLayout_Click(object sender, EventArgs e)
        {
            MainForm main = (MainForm)this.ActiveMdiChild;
            main.ResetSplitLayout();
        }

        private void MainMdi_Activated(object sender, EventArgs e)
        {
            MainForm main = this.ActiveMdiChild as MainForm;
            if (main != null)
            {
                main.MainForm_Activated(sender, e);
            }
        }

        private void MainMdi_Deactivate(object sender, EventArgs e)
        {
            MainForm main = this.ActiveMdiChild as MainForm;
            if (main != null)
            {
                main.MainForm_Deactivate(sender, e);
            }
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {

        }

        private void menuOptNullEmpty_Click(object sender, EventArgs e)
        {
            MainForm main = this.ActiveMdiChild as MainForm;
            if (main != null)
            {
                if (sender == this.menuOptEmpty)
                {
                    main.ReadEmptyAsNull = false;
                }
                else
                {
                    main.ReadEmptyAsNull = true;
                }
                SetOptNullEmpty(main);
            }
        }

        private void menuOptWidth_Click(object sender, EventArgs e)
        {
            MainForm main = this.ActiveMdiChild as MainForm;
            if (main != null)
            {
                if (sender == this.menuOptWidthDefalut)
                {
                    main.GridDefaltWidth = true;
                }
                else
                {
                    main.GridDefaltWidth = false;
                }
                SetGridDefaultWidth(main);
            }
        }

        private void menuOptFilter_Click(object sender, EventArgs e)
        {
            MainForm main = this.ActiveMdiChild as MainForm;
            if (main != null)
            {
                if (sender == this.menuOptFilterNoCS)
                {
                    main.IsFilterCaseSensitive = false;
                }
                else
                {
                    main.IsFilterCaseSensitive = true;
                }
                SetOptFilter(main);
            }
        }
    }
}
