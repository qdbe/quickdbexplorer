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

namespace quickDBExplorer
{
    
	/// <summary>
	/// �S�Ă̐e��ʂƂȂ�MDI�t�H�[��
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
        /// �O�񑀍쎞�̊e��L�����
        /// </summary>
        private ConditionRecorder initopt;
        /// <summary>
        /// �\���G���[���b�Z�[�W
        /// </summary>
        private string errMessage = "";

        /// <summary>
        /// �R�}���h�����̓��e
        /// </summary>
        private Hashtable argHt = new Hashtable();
        private MenuItem menuBookamrk;
        private MenuItem menuAddBookMark;
        private MenuItem menuEditBookmark;

        /// <summary>
        /// �T�[�o�[�ʃu�b�N�}�[�N
        /// </summary>
        private Dictionary<string, List<Forms.BookmarkInfo>> bookMarks;
        private MenuItem menuBookMarkSeparator;
        private MenuItem menuTools;
        private MenuItem menuToolEdit;
        private MenuItem menuToolSeparator;

        private BookmarkManager bmManager;

        private List<ToolInfo> outerTools;
        private ToolManager outerToolManager;
        private ToolMacroManager toolMacroManager;


        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public MainMdi(string[] args)
        {
            //
            // Windows �t�H�[�� �f�U�C�i �T�|�[�g�ɕK�v�ł��B
            //
            InitializeComponent();
            this.ParseArg(args);
        }

        /// <summary>
        /// �g�p����Ă��郊�\�[�X�Ɍ㏈�������s���܂��B
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

        #region Windows �t�H�[�� �f�U�C�i�Ő������ꂽ�R�[�h
        /// <summary>
        /// �f�U�C�i �T�|�[�g�ɕK�v�ȃ��\�b�h�ł��B���̃��\�b�h�̓��e��
        /// �R�[�h �G�f�B�^�ŕύX���Ȃ��ł��������B
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
            this.menuBookamrk = new System.Windows.Forms.MenuItem();
            this.menuAddBookMark = new System.Windows.Forms.MenuItem();
            this.menuEditBookmark = new System.Windows.Forms.MenuItem();
            this.menuBookMarkSeparator = new System.Windows.Forms.MenuItem();
            this.menuHelpMain = new System.Windows.Forms.MenuItem();
            this.menuViewHelp = new System.Windows.Forms.MenuItem();
            this.menuAbout = new System.Windows.Forms.MenuItem();
            this.menuVersion = new System.Windows.Forms.MenuItem();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.menuTools = new System.Windows.Forms.MenuItem();
            this.menuToolEdit = new System.Windows.Forms.MenuItem();
            this.menuToolSeparator = new System.Windows.Forms.MenuItem();
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
            this.menuHelpMain});
            // 
            // menuConnect
            // 
            this.menuConnect.Index = 0;
            this.menuConnect.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuNewConnect,
            this.menuQuit});
            this.menuConnect.Text = "�ڑ�(&C)";
            // 
            // menuNewConnect
            // 
            this.menuNewConnect.Index = 0;
            this.menuNewConnect.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
            this.menuNewConnect.Text = "�V�K�ڑ�(&N)";
            this.menuNewConnect.Click += new System.EventHandler(this.menuNewConnect_Click);
            // 
            // menuQuit
            // 
            this.menuQuit.Index = 1;
            this.menuQuit.Text = "�I��(&Q)";
            this.menuQuit.Click += new System.EventHandler(this.menuQuit_Click);
            // 
            // menuWindow
            // 
            this.menuWindow.Index = 1;
            this.menuWindow.MdiList = true;
            this.menuWindow.Text = "�E�B���h�E(&W)";
            // 
            // menuBookamrk
            // 
            this.menuBookamrk.Index = 2;
            this.menuBookamrk.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuAddBookMark,
            this.menuEditBookmark,
            this.menuBookMarkSeparator});
            this.menuBookamrk.Text = "Bookmark(&M)";
            this.menuBookamrk.Popup += new System.EventHandler(this.menuBookamrk_Popup);
            // 
            // menuAddBookMark
            // 
            this.menuAddBookMark.Index = 0;
            this.menuAddBookMark.Shortcut = System.Windows.Forms.Shortcut.CtrlM;
            this.menuAddBookMark.Text = "�ǉ�(&M)";
            this.menuAddBookMark.Click += new System.EventHandler(this.menuAddBookMark_Click);
            // 
            // menuEditBookmark
            // 
            this.menuEditBookmark.Index = 1;
            this.menuEditBookmark.Shortcut = System.Windows.Forms.Shortcut.CtrlB;
            this.menuEditBookmark.Text = "�Ǘ�(&B)";
            this.menuEditBookmark.Click += new System.EventHandler(this.menuEditBookmark_Click);
            // 
            // menuBookMarkSeparator
            // 
            this.menuBookMarkSeparator.Index = 2;
            this.menuBookMarkSeparator.Text = "-";
            // 
            // menuHelpMain
            // 
            this.menuHelpMain.Index = 4;
            this.menuHelpMain.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuViewHelp,
            this.menuAbout,
            this.menuVersion});
            this.menuHelpMain.Text = "HELP(&H)";
            // 
            // menuViewHelp
            // 
            this.menuViewHelp.Index = 0;
            this.menuViewHelp.Text = "�w���v�Q��";
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
            this.menuVersion.Text = "�ŐV�o�[�W�����̃`�F�b�N";
            this.menuVersion.Click += new System.EventHandler(this.menuVersion_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // menuTools
            // 
            this.menuTools.Index = 3;
            this.menuTools.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuToolEdit,
            this.menuToolSeparator});
            this.menuTools.Text = "�O���c�[��(&N)";
            this.menuTools.Popup += new System.EventHandler(this.menuTools_Popup);
            // 
            // menuToolEdit
            // 
            this.menuToolEdit.Index = 0;
            this.menuToolEdit.Text = "�Ǘ�";
            this.menuToolEdit.Click += new System.EventHandler(this.menuToolEdit_Click);
            // 
            // menuToolSeparator
            // 
            this.menuToolSeparator.Index = 1;
            this.menuToolSeparator.Text = "-";
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
        /// �ڑ����j���[�N���b�N���C�x���g�n���h��
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
                logindlg = new LogOnDialog(this.initopt);
            }
            else
            {
                logindlg = new LogOnDialog(this.initopt, argHt);
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
        }

        /// <summary>
        /// �G���[���b�Z�[�W�̏��������s��
        /// </summary>
        protected void InitErrMessage()
        {
            this.statusBar1.Text = "";
            this.errMessage = "";
            this.errorProvider1.SetIconAlignment(this.statusBar1, ErrorIconAlignment.MiddleLeft);
            this.errorProvider1.SetError(this.statusBar1, "");
        }

        /// <summary>
        /// �G���[���b�Z�[�W��\������
        /// </summary>
        /// <param name="ex">�G���[����������Exception</param>
        protected void SetErrorMessage(Exception ex)
        {
            this.statusBar1.Text = ex.Message;
            this.errMessage = ex.ToString();
            this.errorProvider1.SetError(this.statusBar1, this.statusBar1.Text);
        }

        /// <summary>
        /// �X�e�[�^�X�o�[���_�u���N���b�N���邱�ƂŁA�G���[���b�Z�[�W���N���b�v�{�[�h�ɃR�s�[����
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
        /// ���Load���̏���
        /// </summary>
        /// <param name="sender">--</param>
        /// <param name="e">--</param>
        private void MainMdi_Load(object sender, System.EventArgs e)
        {
            //������̃`�F�b�N
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
                MessageBox.Show("��������s�K�؂ł��B.NET Framework 2.0 SP1 �ȏ�̊����K�v�ł��B");
                this.Close();
            }

            // �ݒ�t�@�C���̓ǂݍ��݃X�g���[��
            FileStream fs = null;

            // �G���[���b�Z�[�W��������
            this.InitErrMessage();

            initopt = new ConditionRecorder();
            try
            {
                // �ݒ�t�@�C����ǂݍ���
                string path = Application.StartupPath;
                fs = new FileStream(path + "\\quickDBExplorer.xml", FileMode.Open);
                // Soap Serialize ���Ă���̂ŁA�����DeSerialize
                SoapFormatter sf = new SoapFormatter();
                if (fs != null && fs.CanRead)
                {
                    initopt = (ConditionRecorder)sf.Deserialize(fs);
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                // ����C���X�g�[�����̓t�@�C�����Ȃ��\��������
                ;
            }
            catch (Exception exp)
            {
                this.SetErrorMessage(exp);
                initopt = new ConditionRecorder();
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
            // �ŐV�o�[�W�����̑��݃`�F�b�N�����{
            // �����I�ɂ���Ă��܂��Ɩ��Ȃ̂ŁA�����ł͂��Ȃ�
            //CheckNewVersion(false);

            // Bookmark����
            InitBookMark();

            InitTools();

            // �ŏ��͋����I�Ƀ��O�C����\������
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
        /// �_�C�A���O�̃N���[�Y����
        /// </summary>
        /// <param name="sender">--</param>
        /// <param name="e">--</param>
        private void MainMdi_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FileStream fs = null;

            try
            {
                string path = Application.StartupPath;
                fs = new FileStream(path + "\\quickDBExplorer.xml", FileMode.OpenOrCreate);
                fs.Close();
                fs = null;
                fs = new FileStream(path + "\\quickDBExplorer.xml", FileMode.Truncate, FileAccess.Write);
                SoapFormatter sf = new SoapFormatter();
                if (fs != null && fs.CanWrite)
                {
                    ArrayList ar = new ArrayList();
                    foreach (object keys in initopt.PerServerData.Keys)
                    {
                        if (((ServerData)(initopt.PerServerData[keys])).IsSaveKey == false)
                        {
                            ar.Add((string)keys);
                        }
                    }
                    foreach (string kk in ar)
                    {
                        initopt.PerServerData.Remove(kk);
                    }
                    if (initopt.PerServerData.Count == 0)
                    {
                        ServerData sv = new ServerData();
                        sv.Servername = "(local)";
                        sv.InstanceName = "";
                        sv.IsUseTrust = false;
                        initopt.PerServerData.Add(sv.KeyName, sv);
                    }
                    sf.Serialize(fs, (object)initopt);
                }

                if (this.bmManager != null)
                {
                    this.bmManager.Save(this.bookMarks);
                }
                if (this.outerToolManager != null)
                {
                    this.outerToolManager.Save(this.outerTools);
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                ;
            }
            catch (Exception ex)
            {
                this.SetErrorMessage(ex);
            }
            finally
            {
                if (fs != null)
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
            helpname = helpname.Replace(".exe", "Help.htm");
            if (File.Exists(helpname) == true)
            {
                System.Diagnostics.Process.Start(helpname);
            }
        }

        private void menuAbout_Click(object sender, System.EventArgs e)
        {
#if true
            // ��������������
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
        /// �ŐV�o�[�W���������݂��邩�ǂ������`�F�b�N����
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
                    MessageBox.Show("�ŐV�o�[�W���������J����Ă��܂�\r\nhttp://code.google.com/p/quickdbexplorer/ ���`�F�b�N���Ă�������");
                }
                else
                {
                    MessageBox.Show("���ɍŐV�o�[�W�����ł��B�X�V�̕K�v�͂���܂���B");
                }
            }
#if DEBUG
            catch (Exception ex)
            {
                MessageBox.Show("�ŐV���ǂ����`�F�b�N�ł��܂���ł����B�l�b�g���[�N���𒲍����ĉ������B\r\nhttp://qdbe.rgr.jp/�ւ̃A�N�Z�X���\�ł���K�v�ł�");
                if (catchError == true)
                {
                    MessageBox.Show(ex.ToString());

                }
            }
#else
            catch
            {
                MessageBox.Show("�ŐV���ǂ����`�F�b�N�ł��܂���ł����B�l�b�g���[�N���𒲍����ĉ������B\r\nhttp://qdbe.rgr.jp/�ւ̃A�N�Z�X���\�ł���K�v�ł�");

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
        /// �R�}���h�������p�[�X����
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
                        // �T�[�o�[���w��
                        if (preParam != string.Empty)
                        {
                            return false;
                        }
                        preParam = LogOnDialog.PARAM_SERVER;
                        break;
                    case "-I":
                    case "-i":
                        // �C���X�^���X���w��
                        if (preParam != string.Empty)
                        {
                            return false;
                        }
                        preParam = LogOnDialog.PARAM_INSTANCE;
                        break;
                    case "-U":
                    case "-u":
                        // ���[�U�[���w��
                        if (preParam != string.Empty)
                        {
                            return false;
                        }
                        preParam = LogOnDialog.PARAM_USER;
                        break;
                    case "-P":
                    case "-p":
                        // �p�X���[�h�w��
                        if (preParam != string.Empty)
                        {
                            return false;
                        }
                        preParam = LogOnDialog.PARAM_PASSWORD;
                        break;
                    case "-T":
                    case "-t":
                        // Windows �F�؎w��
                        this.argHt[LogOnDialog.PARAM_TRUST] = true;
                        preParam = string.Empty;
                        break;
                    default:
                        //�����ȊO�̏ꍇ�͒l����
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
                MessageBox.Show("�������s���ł��B�w���v���Q�Ƃ��ĉ�����");
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
                    MessageBox.Show("���ɓ������O�̃u�b�N�}�[�N���o�^����Ă��܂�");
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
                // ���O�C����
                return;
            }
            MainForm form = ((MainForm)this.ActiveMdiChild);
            if (this.bookMarks.ContainsKey(form.ServerName) == false)
            {
                MessageBox.Show("�ΏۃT�[�o�[�̃u�b�N�}�[�N�͂���܂���");
                return;
            }
            List<BookmarkInfo> bookmarklist = this.bookMarks[form.ServerName];
            if (bookmarklist.Count == 0)
            {
                MessageBox.Show("�ΏۃT�[�o�[�̃u�b�N�}�[�N�͂���܂���");
                return;
            }
            Forms.Dialog.BookMarkEditForm bookmarkEditor = new quickDBExplorer.Forms.Dialog.BookMarkEditForm(bookmarklist);
            bookmarkEditor.ShowDialog(this);
        }

        private void menuToolEdit_Click(object sender, EventArgs e)
        {
            Forms.Dialog.OuterToolEditForm dlg = new quickDBExplorer.Forms.Dialog.OuterToolEditForm(outerTools, toolMacroManager);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                this.outerTools = dlg.ResultToolList;
            }
        }

        private void menuTools_Popup(object sender, EventArgs e)
        {
            this.menuTools.MenuItems.Clear();
            this.menuTools.MenuItems.Add(menuToolEdit);
            SetToolPopupList();
        }

        private void SetToolPopupList()
        {
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
            outerToolManager.DoAction(((MenuItem)sender).Tag);
        }
    }
    
}
