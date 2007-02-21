using System;
using System.Text;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace quickDBExplorer
{
	/// <summary>
	/// ���C���ƂȂ���
	/// DB�̑I���A�I�[�i�[�̑I���A�e�[�u���̑I���A�����̑I���Ȃǂ̃��C���ƂȂ鏈����S�Ď������Ă���
	/// </summary>
	public class MainForm : quickDBExplorerBaseForm
	{
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Button btnCSV;
		private System.Windows.Forms.Button btnDDL;
		private System.Windows.Forms.Button btnDataEdit;
		private System.Windows.Forms.Button btnDataUpdate;
		private System.Windows.Forms.Button btnFieldList;
		private System.Windows.Forms.Button btnGridFormat;
		private System.Windows.Forms.Button btnIndex;
		private System.Windows.Forms.Button btnInsert;
		private System.Windows.Forms.Button btnQuerySelect;
		private System.Windows.Forms.Button btnRedisp;
		private System.Windows.Forms.Button btnReference;
		private System.Windows.Forms.Button btnSelect;
		private System.Windows.Forms.Button btnTmpAllDsp;
		private System.Windows.Forms.CheckBox chkDspData;
		private System.Windows.Forms.CheckBox chkDspFieldAttr;
		private System.Windows.Forms.ContextMenu fldContextMenu;
		private System.Windows.Forms.DataGrid dbGrid;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.GroupBox grpCharaSet;
		private System.Windows.Forms.GroupBox grpDataDspMode;
		private System.Windows.Forms.GroupBox grpOutputMode;
		private System.Windows.Forms.GroupBox grpSortMode;
		private System.Windows.Forms.GroupBox grpSysUserMode;
		private System.Windows.Forms.GroupBox grpViewMode;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private quickDBExplorer.qdbeListBox dbList;
		private quickDBExplorer.qdbeListBox fieldListbox;
		private quickDBExplorer.qdbeListBox ownerListbox;
		private quickDBExplorer.qdbeListBox tableList;
		private System.Windows.Forms.MenuItem fldmenuCopy;
		private System.Windows.Forms.MenuItem fldmenuCopyNoCRLF;
		private System.Windows.Forms.MenuItem fldmenuCopyNoCRLFNoComma;
		private System.Windows.Forms.MenuItem fldmenuCopyNoComma;
		private System.Windows.Forms.MenuItem menuDDL;
		private System.Windows.Forms.MenuItem menuDDLDrop;
		private System.Windows.Forms.MenuItem menuDDLDropPare;
		private System.Windows.Forms.MenuItem menuDDLPare;
		private System.Windows.Forms.MenuItem menuInsert;
		private System.Windows.Forms.MenuItem menuInsertDelete;
		private System.Windows.Forms.MenuItem menuInsertNoFld;
		private System.Windows.Forms.MenuItem menuInsertNoFldDelete;
		private System.Windows.Forms.MenuItem menuMakeCSV;
		private System.Windows.Forms.MenuItem menuMakeCSVDQ;
		private System.Windows.Forms.MenuItem menuMakeFld;
		private System.Windows.Forms.MenuItem menuMakeFldCRLF;
		private System.Windows.Forms.MenuItem menuMakeFldNoComma;
		private System.Windows.Forms.MenuItem menuSelect;
		private System.Windows.Forms.MenuItem menuSeparater1;
		private System.Windows.Forms.MenuItem menuSeparater2;
		private System.Windows.Forms.MenuItem menuSeparater3;
		private System.Windows.Forms.MenuItem menuTableCopy;
		private System.Windows.Forms.MenuItem menuTableCopyCsv;
		private System.Windows.Forms.RadioButton rdoDspSysUser;
		private System.Windows.Forms.RadioButton rdoNotDspSysUser;
		private System.Windows.Forms.RadioButton rdoDspView;
		private System.Windows.Forms.RadioButton rdoNotDspView;
		private System.Windows.Forms.RadioButton rdoClipboard;
		private System.Windows.Forms.RadioButton rdoOutFile;
		private System.Windows.Forms.RadioButton rdoOutFolder;
		private System.Windows.Forms.RadioButton rdoSortOwnerTable;
		private System.Windows.Forms.RadioButton rdoSortTable;
		private System.Windows.Forms.RadioButton rdoSjis;
		private System.Windows.Forms.RadioButton rdoUnicode;
		private System.Windows.Forms.RadioButton rdoUtf8;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private quickDBExplorerTextBox txtDspCount;
		private quickDBExplorerTextBox txtOutput;
		private quickDBExplorerTextBox txtSort;
		private quickDBExplorerTextBox txtWhere;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.ToolTip toolTip2;
		private System.Windows.Forms.ToolTip toolTip3;

		private QueryDialog Sqldlg2 = new QueryDialog();
		private QuerySelectDialog Sqldlg = new QuerySelectDialog();
		private DataSet dspdt = new DataSet();
		private ServerData svdata;
		private	Font	gfont;
		private	Color	gcolor;
		private Color	btnBackColor;
		private Color	btnForeColor;
		private	IndexViewDialog indexdlg = null;
		private	string	NumFormat;
		private	string	FloatFormat;
		private	string	DateFormat;

		/// <summary>
		///  �ڑ���̃T�[�o�[���B�\���p�ɂ̂ݗ��p
		/// </summary>
		public string servername = "";

		/// <summary>
		/// �ڑ���T�[�o�[�̖{���̖��O�B�C���X�^���X�����܂܂Ȃ�
		/// </summary>
		public string serverRealName = "";

		/// <summary>
		/// �ڑ���T�[�o�[�̃C���X�^���X��
		/// </summary>
		public string instanceName = "";

		/// <summary>
		/// ���O�C��ID
		/// </summary>
		public string loginUid = "";

		/// <summary>
		/// ���O�C���p�p�X���[�h
		/// </summary>
		public string loginPasswd = "";

		/// <summary>
		/// �M���֌W�ڑ��𗘗p���邩�ۂ�
		/// </summary>
		public bool IsUseTruse = false;

		/// <summary>
		/// �X���b�h�̉ғ���Ԃ�\��
		/// ������=1 ���f���ꂽ�A�܂��͖����� = 0
		/// </summary>
		protected int IsThreadAlive = 0;

		/// <summary>
		/// Table/View ���X�g�̑I�𗚗�
		/// Max10����z��
		/// </summary>
		protected ArrayList selectedTables = new ArrayList();


		/// <summary>
		/// Table/View ���X�g�̑I�𗚗� MAX����
		/// </summary>
		protected const int MaxTableHistory = 10;

		/// <summary>
		///  �R���{�{�b�N�X�̃C�x���g���������ۂ�
		/// </summary>
		protected bool isInCmbEvent = false;

		private int SqlTimeOut = 300;

		private textHistory  whereHistory = new textHistory();
		private textHistory  sortHistory = new textHistory();

		/// <summary>
		/// DB�ڑ����
		/// </summary>
		public System.Data.SqlClient.SqlConnection sqlConnection1;
		private System.Windows.Forms.MenuItem menuSeparater4;
		private System.Windows.Forms.MenuItem menuDepend;
		private System.Windows.Forms.Button btnEtc;
		private System.Windows.Forms.ContextMenu etcContextMenu;
		private System.Windows.Forms.MenuItem menuQuery;
		private System.Windows.Forms.MenuItem menuProfile;
		private System.Windows.Forms.MenuItem menuISQL;
		private System.Windows.Forms.MenuItem menuISQLW;
		private System.Windows.Forms.MenuItem menuDependBtn;
		private System.Windows.Forms.MenuItem menuEPM;
		private System.Windows.Forms.MenuItem menuRecordCount;
		private System.Windows.Forms.MenuItem menuSeparater5;
		private System.Windows.Forms.MenuItem menuSeparater6;
		private System.Windows.Forms.MenuItem menuMakeTab;
		private System.Windows.Forms.MenuItem menuMakeTabDQ;
		private System.Windows.Forms.Button btnWhereZoom;
		private System.Windows.Forms.Button btnOrderZoom;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.CheckBox useCheckBox;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.MenuItem menuInsertDeleteTaihi;
		private System.Windows.Forms.MenuItem menuInsertNoFldDeleteTaihi;
		private System.Windows.Forms.MenuItem menuRecordCountDsp;
		private System.Windows.Forms.ContextMenu dbGridMenu;
		private System.Windows.Forms.MenuItem copyDbGridMenu;
		private System.Windows.Forms.ContextMenu contextMenu1;
		protected System.Windows.Forms.ComboBox cmbHistory;
		
		/// <summary>
		/// ���j���[���
		/// </summary>
		public System.Windows.Forms.ContextMenu mainContextMenu;

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		/// <param name="sv">�O��ŏI�����̋L���l</param>
		public MainForm(ServerData sv)
		{
			//
			// Windows �t�H�[�� �f�U�C�i �T�|�[�g�ɕK�v�ł��B
			//
			InitializeComponent();

			svdata = sv;
			Sqldlg.SelectSql = "";
			Sqldlg2.SelectSql = "";
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
				if( this.sqlConnection1 != null )
				{
					this.sqlConnection1.Close();
					this.sqlConnection1.Dispose();
					this.sqlConnection1 = null;
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
			this.dbList = new quickDBExplorer.qdbeListBox();
			this.tableList = new quickDBExplorer.qdbeListBox();
			this.mainContextMenu = new System.Windows.Forms.ContextMenu();
			this.menuTableCopy = new System.Windows.Forms.MenuItem();
			this.menuTableCopyCsv = new System.Windows.Forms.MenuItem();
			this.menuInsert = new System.Windows.Forms.MenuItem();
			this.menuInsertDelete = new System.Windows.Forms.MenuItem();
			this.menuInsertNoFld = new System.Windows.Forms.MenuItem();
			this.menuInsertNoFldDelete = new System.Windows.Forms.MenuItem();
			this.menuInsertDeleteTaihi = new System.Windows.Forms.MenuItem();
			this.menuInsertNoFldDeleteTaihi = new System.Windows.Forms.MenuItem();
			this.menuSeparater1 = new System.Windows.Forms.MenuItem();
			this.menuMakeFld = new System.Windows.Forms.MenuItem();
			this.menuMakeFldCRLF = new System.Windows.Forms.MenuItem();
			this.menuMakeFldNoComma = new System.Windows.Forms.MenuItem();
			this.menuSeparater2 = new System.Windows.Forms.MenuItem();
			this.menuDDL = new System.Windows.Forms.MenuItem();
			this.menuDDLDrop = new System.Windows.Forms.MenuItem();
			this.menuDDLPare = new System.Windows.Forms.MenuItem();
			this.menuDDLDropPare = new System.Windows.Forms.MenuItem();
			this.menuSeparater3 = new System.Windows.Forms.MenuItem();
			this.menuSelect = new System.Windows.Forms.MenuItem();
			this.menuMakeCSV = new System.Windows.Forms.MenuItem();
			this.menuMakeCSVDQ = new System.Windows.Forms.MenuItem();
			this.menuMakeTab = new System.Windows.Forms.MenuItem();
			this.menuMakeTabDQ = new System.Windows.Forms.MenuItem();
			this.menuSeparater4 = new System.Windows.Forms.MenuItem();
			this.menuDepend = new System.Windows.Forms.MenuItem();
			this.menuISQLW = new System.Windows.Forms.MenuItem();
			this.btnInsert = new System.Windows.Forms.Button();
			this.btnFieldList = new System.Windows.Forms.Button();
			this.btnCSV = new System.Windows.Forms.Button();
			this.rdoDspView = new System.Windows.Forms.RadioButton();
			this.grpViewMode = new System.Windows.Forms.GroupBox();
			this.rdoNotDspView = new System.Windows.Forms.RadioButton();
			this.grpSortMode = new System.Windows.Forms.GroupBox();
			this.rdoSortOwnerTable = new System.Windows.Forms.RadioButton();
			this.rdoSortTable = new System.Windows.Forms.RadioButton();
			this.txtWhere = new quickDBExplorer.quickDBExplorerTextBox();
			this.txtSort = new quickDBExplorer.quickDBExplorerTextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.btnSelect = new System.Windows.Forms.Button();
			this.ownerListbox = new quickDBExplorer.qdbeListBox();
			this.btnDDL = new System.Windows.Forms.Button();
			this.dbGrid = new System.Windows.Forms.DataGrid();
			this.dbGridMenu = new System.Windows.Forms.ContextMenu();
			this.copyDbGridMenu = new System.Windows.Forms.MenuItem();
			this.chkDspData = new System.Windows.Forms.CheckBox();
			this.grpDataDspMode = new System.Windows.Forms.GroupBox();
			this.txtDspCount = new quickDBExplorer.quickDBExplorerTextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.grpSysUserMode = new System.Windows.Forms.GroupBox();
			this.rdoNotDspSysUser = new System.Windows.Forms.RadioButton();
			this.rdoDspSysUser = new System.Windows.Forms.RadioButton();
			this.grpOutputMode = new System.Windows.Forms.GroupBox();
			this.btnReference = new System.Windows.Forms.Button();
			this.txtOutput = new quickDBExplorer.quickDBExplorerTextBox();
			this.rdoOutFile = new System.Windows.Forms.RadioButton();
			this.rdoClipboard = new System.Windows.Forms.RadioButton();
			this.rdoOutFolder = new System.Windows.Forms.RadioButton();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.fieldListbox = new quickDBExplorer.qdbeListBox();
			this.fldContextMenu = new System.Windows.Forms.ContextMenu();
			this.fldmenuCopy = new System.Windows.Forms.MenuItem();
			this.fldmenuCopyNoCRLF = new System.Windows.Forms.MenuItem();
			this.fldmenuCopyNoComma = new System.Windows.Forms.MenuItem();
			this.fldmenuCopyNoCRLFNoComma = new System.Windows.Forms.MenuItem();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.grpCharaSet = new System.Windows.Forms.GroupBox();
			this.rdoUtf8 = new System.Windows.Forms.RadioButton();
			this.rdoSjis = new System.Windows.Forms.RadioButton();
			this.rdoUnicode = new System.Windows.Forms.RadioButton();
			this.chkDspFieldAttr = new System.Windows.Forms.CheckBox();
			this.label6 = new System.Windows.Forms.Label();
			this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
			this.btnQuerySelect = new System.Windows.Forms.Button();
			this.toolTip3 = new System.Windows.Forms.ToolTip(this.components);
			this.btnDataUpdate = new System.Windows.Forms.Button();
			this.btnDataEdit = new System.Windows.Forms.Button();
			this.label7 = new System.Windows.Forms.Label();
			this.btnGridFormat = new System.Windows.Forms.Button();
			this.label8 = new System.Windows.Forms.Label();
			this.btnIndex = new System.Windows.Forms.Button();
			this.btnRedisp = new System.Windows.Forms.Button();
			this.btnTmpAllDsp = new System.Windows.Forms.Button();
			this.btnEtc = new System.Windows.Forms.Button();
			this.etcContextMenu = new System.Windows.Forms.ContextMenu();
			this.menuQuery = new System.Windows.Forms.MenuItem();
			this.menuSeparater5 = new System.Windows.Forms.MenuItem();
			this.menuISQL = new System.Windows.Forms.MenuItem();
			this.menuProfile = new System.Windows.Forms.MenuItem();
			this.menuEPM = new System.Windows.Forms.MenuItem();
			this.menuSeparater6 = new System.Windows.Forms.MenuItem();
			this.menuDependBtn = new System.Windows.Forms.MenuItem();
			this.menuRecordCount = new System.Windows.Forms.MenuItem();
			this.menuRecordCountDsp = new System.Windows.Forms.MenuItem();
			this.btnWhereZoom = new System.Windows.Forms.Button();
			this.btnOrderZoom = new System.Windows.Forms.Button();
			this.label9 = new System.Windows.Forms.Label();
			this.useCheckBox = new System.Windows.Forms.CheckBox();
			this.label10 = new System.Windows.Forms.Label();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.cmbHistory = new System.Windows.Forms.ComboBox();
			this.grpViewMode.SuspendLayout();
			this.grpSortMode.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dbGrid)).BeginInit();
			this.grpDataDspMode.SuspendLayout();
			this.grpSysUserMode.SuspendLayout();
			this.grpOutputMode.SuspendLayout();
			this.grpCharaSet.SuspendLayout();
			this.SuspendLayout();
			// 
			// msgArea
			// 
			this.msgArea.Location = new System.Drawing.Point(176, 624);
			this.msgArea.Name = "msgArea";
			this.msgArea.Size = new System.Drawing.Size(652, 16);
			this.msgArea.TabIndex = 40;
			// 
			// dbList
			// 
			this.dbList.Font = new System.Drawing.Font("�l�r �S�V�b�N", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(128)));
			this.dbList.ItemHeight = 12;
			this.dbList.Location = new System.Drawing.Point(60, 16);
			this.dbList.Name = "dbList";
			this.dbList.Size = new System.Drawing.Size(160, 52);
			this.dbList.TabIndex = 1;
			this.dbList.CopyData += new quickDBExplorer.qdbeListBox.CopyDataHandler(this.dbList_CopyData);
			this.dbList.SelectedIndexChanged += new System.EventHandler(this.dbList_SelectedIndexChanged);
			// 
			// tableList
			// 
			this.tableList.ContextMenu = this.mainContextMenu;
			this.tableList.Font = new System.Drawing.Font("�l�r �S�V�b�N", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(128)));
			this.tableList.HorizontalScrollbar = true;
			this.tableList.ItemHeight = 12;
			this.tableList.Location = new System.Drawing.Point(240, 24);
			this.tableList.Name = "tableList";
			this.tableList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.tableList.Size = new System.Drawing.Size(256, 292);
			this.tableList.TabIndex = 20;
			this.tableList.CopyData += new quickDBExplorer.qdbeListBox.CopyDataHandler(this.tableList_CopyData);
			this.tableList.DoubleClick += new System.EventHandler(this.insertmake);
			this.tableList.SelectedIndexChanged += new System.EventHandler(this.tableList_SelectedIndexChanged);
			// 
			// mainContextMenu
			// 
			this.mainContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																							this.menuTableCopy,
																							this.menuTableCopyCsv,
																							this.menuInsert,
																							this.menuInsertDelete,
																							this.menuInsertNoFld,
																							this.menuInsertNoFldDelete,
																							this.menuInsertDeleteTaihi,
																							this.menuInsertNoFldDeleteTaihi,
																							this.menuSeparater1,
																							this.menuMakeFld,
																							this.menuMakeFldCRLF,
																							this.menuMakeFldNoComma,
																							this.menuSeparater2,
																							this.menuDDL,
																							this.menuDDLDrop,
																							this.menuDDLPare,
																							this.menuDDLDropPare,
																							this.menuSeparater3,
																							this.menuSelect,
																							this.menuMakeCSV,
																							this.menuMakeCSVDQ,
																							this.menuMakeTab,
																							this.menuMakeTabDQ,
																							this.menuSeparater4,
																							this.menuDepend});
			this.mainContextMenu.Popup += new System.EventHandler(this.mainContextMenu_Popup);
			// 
			// menuTableCopy
			// 
			this.menuTableCopy.Index = 0;
			this.menuTableCopy.Text = "�e�[�u�����R�s�[";
			this.menuTableCopy.Click += new System.EventHandler(this.menuTableCopy_Click);
			// 
			// menuTableCopyCsv
			// 
			this.menuTableCopyCsv.Index = 1;
			this.menuTableCopyCsv.Text = "�e�[�u�����R�s�[ �J���}�t��";
			this.menuTableCopyCsv.Click += new System.EventHandler(this.menuTableCopyCsv_Click);
			// 
			// menuInsert
			// 
			this.menuInsert.Index = 2;
			this.menuInsert.Text = "INSERT���쐬";
			this.menuInsert.Click += new System.EventHandler(this.insertmake);
			// 
			// menuInsertDelete
			// 
			this.menuInsertDelete.Index = 3;
			this.menuInsertDelete.Text = "INSERT���쐬(DELETE���t��)";
			this.menuInsertDelete.Click += new System.EventHandler(this.insertmakeDelete);
			// 
			// menuInsertNoFld
			// 
			this.menuInsertNoFld.Index = 4;
			this.menuInsertNoFld.Text = "INSERT���쐬(�t�B�[���h���X�g�Ȃ�)";
			this.menuInsertNoFld.Click += new System.EventHandler(this.insertmakeNoField);
			// 
			// menuInsertNoFldDelete
			// 
			this.menuInsertNoFldDelete.Index = 5;
			this.menuInsertNoFldDelete.Text = "INSERT���쐬(�t�B�[���h���X�g�Ȃ��@DELETE���t��)";
			this.menuInsertNoFldDelete.Click += new System.EventHandler(this.insertmakeNoFieldDelete);
			// 
			// menuInsertDeleteTaihi
			// 
			this.menuInsertDeleteTaihi.Index = 6;
			this.menuInsertDeleteTaihi.Text = "INSERT���쐬(DELETE���t���A�ޔ�t��)";
			this.menuInsertDeleteTaihi.Click += new System.EventHandler(this.menuInsertDeleteTaihi_Click);
			// 
			// menuInsertNoFldDeleteTaihi
			// 
			this.menuInsertNoFldDeleteTaihi.Index = 7;
			this.menuInsertNoFldDeleteTaihi.Text = "INSERT���쐬(�t�B�[���h�Ȃ� DELETE���t�� �ޔ�t��)";
			this.menuInsertNoFldDeleteTaihi.Click += new System.EventHandler(this.menuInsertNoFldDeleteTaihi_Click);
			// 
			// menuSeparater1
			// 
			this.menuSeparater1.Index = 8;
			this.menuSeparater1.Text = "-";
			// 
			// menuMakeFld
			// 
			this.menuMakeFld.Index = 9;
			this.menuMakeFld.Text = "�t�B�[���h���X�g�쐬";
			this.menuMakeFld.Click += new System.EventHandler(this.makefldlist);
			// 
			// menuMakeFldCRLF
			// 
			this.menuMakeFldCRLF.Index = 10;
			this.menuMakeFldCRLF.Text = "�t�B�[���h���X�g���s�쐬";
			this.menuMakeFldCRLF.Click += new System.EventHandler(this.makefldListLF);
			// 
			// menuMakeFldNoComma
			// 
			this.menuMakeFldNoComma.Index = 11;
			this.menuMakeFldNoComma.Text = "�t�B�[���h���X�g�J���}�Ȃ��쐬";
			this.menuMakeFldNoComma.Click += new System.EventHandler(this.makefldListNoComma);
			// 
			// menuSeparater2
			// 
			this.menuSeparater2.Index = 12;
			this.menuSeparater2.Text = "-";
			// 
			// menuDDL
			// 
			this.menuDDL.Index = 13;
			this.menuDDL.Text = "�ȈՒ�`������";
			this.menuDDL.Click += new System.EventHandler(this.makeDDL);
			// 
			// menuDDLDrop
			// 
			this.menuDDLDrop.Index = 14;
			this.menuDDLDrop.Text = "�ȈՒ�`������ DROP���t��";
			this.menuDDLDrop.Click += new System.EventHandler(this.makeDDLDrop);
			// 
			// menuDDLPare
			// 
			this.menuDDLPare.Index = 15;
			this.menuDDLPare.Text = "�ȈՒ�`������([]�t��)";
			this.menuDDLPare.Click += new System.EventHandler(this.makeDDLPare);
			// 
			// menuDDLDropPare
			// 
			this.menuDDLDropPare.Index = 16;
			this.menuDDLDropPare.Text = "�ȈՒ�`������( DROP []�t��)";
			this.menuDDLDropPare.Click += new System.EventHandler(this.makeDDLDropPare);
			// 
			// menuSeparater3
			// 
			this.menuSeparater3.Index = 17;
			this.menuSeparater3.Text = "-";
			// 
			// menuSelect
			// 
			this.menuSelect.Index = 18;
			this.menuSelect.Text = "Select������";
			this.menuSelect.Click += new System.EventHandler(this.btnSelect_Click);
			// 
			// menuMakeCSV
			// 
			this.menuMakeCSV.Index = 19;
			this.menuMakeCSV.Text = "CSV�쐬";
			this.menuMakeCSV.Click += new System.EventHandler(this.makeCSV);
			// 
			// menuMakeCSVDQ
			// 
			this.menuMakeCSVDQ.Index = 20;
			this.menuMakeCSVDQ.Text = "CSV�쐬(�h�t��)";
			this.menuMakeCSVDQ.Click += new System.EventHandler(this.makeCSVQuote);
			// 
			// menuMakeTab
			// 
			this.menuMakeTab.Index = 21;
			this.menuMakeTab.Text = "Tab��؏o��";
			this.menuMakeTab.Click += new System.EventHandler(this.menuMakeTab_Click);
			// 
			// menuMakeTabDQ
			// 
			this.menuMakeTabDQ.Index = 22;
			this.menuMakeTabDQ.Text = "Tab��؏o��(\"�t��)";
			this.menuMakeTabDQ.Click += new System.EventHandler(this.menuMakeTabDQ_Click);
			// 
			// menuSeparater4
			// 
			this.menuSeparater4.Index = 23;
			this.menuSeparater4.Text = "-";
			// 
			// menuDepend
			// 
			this.menuDepend.Index = 24;
			this.menuDepend.Text = "�ˑ��֌W�o��";
			this.menuDepend.Click += new System.EventHandler(this.DependOutPut);
			// 
			// menuISQLW
			// 
			this.menuISQLW.Index = -1;
			this.menuISQLW.Text = "�N�G���A�i���C�U�N��";
			this.menuISQLW.Click += new System.EventHandler(this.CallISQLW);
			// 
			// btnInsert
			// 
			this.btnInsert.Location = new System.Drawing.Point(508, 16);
			this.btnInsert.Name = "btnInsert";
			this.btnInsert.Size = new System.Drawing.Size(136, 24);
			this.btnInsert.TabIndex = 21;
			this.btnInsert.Text = "INSERT���쐬(&I)";
			this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
			// 
			// btnFieldList
			// 
			this.btnFieldList.Location = new System.Drawing.Point(508, 44);
			this.btnFieldList.Name = "btnFieldList";
			this.btnFieldList.Size = new System.Drawing.Size(136, 24);
			this.btnFieldList.TabIndex = 22;
			this.btnFieldList.Text = "�t�B�[���h���X�g�쐬(&F)";
			this.btnFieldList.Click += new System.EventHandler(this.btnFieldList_Click);
			// 
			// btnCSV
			// 
			this.btnCSV.Location = new System.Drawing.Point(508, 128);
			this.btnCSV.Name = "btnCSV";
			this.btnCSV.Size = new System.Drawing.Size(136, 24);
			this.btnCSV.TabIndex = 25;
			this.btnCSV.Text = "CSV�쐬(&K)";
			this.btnCSV.Click += new System.EventHandler(this.btnCSV_Click);
			// 
			// rdoDspView
			// 
			this.rdoDspView.Location = new System.Drawing.Point(8, 16);
			this.rdoDspView.Name = "rdoDspView";
			this.rdoDspView.Size = new System.Drawing.Size(88, 16);
			this.rdoDspView.TabIndex = 0;
			this.rdoDspView.Text = "�\������";
			this.rdoDspView.CheckedChanged += new System.EventHandler(this.rdoDspView_CheckedChanged);
			// 
			// grpViewMode
			// 
			this.grpViewMode.Controls.Add(this.rdoNotDspView);
			this.grpViewMode.Controls.Add(this.rdoDspView);
			this.grpViewMode.Location = new System.Drawing.Point(8, 220);
			this.grpViewMode.Name = "grpViewMode";
			this.grpViewMode.Size = new System.Drawing.Size(216, 40);
			this.grpViewMode.TabIndex = 6;
			this.grpViewMode.TabStop = false;
			this.grpViewMode.Text = "VIEW���ꗗ��";
			// 
			// rdoNotDspView
			// 
			this.rdoNotDspView.Location = new System.Drawing.Point(112, 16);
			this.rdoNotDspView.Name = "rdoNotDspView";
			this.rdoNotDspView.Size = new System.Drawing.Size(88, 16);
			this.rdoNotDspView.TabIndex = 1;
			this.rdoNotDspView.Text = "�\�����Ȃ�";
			// 
			// grpSortMode
			// 
			this.grpSortMode.Controls.Add(this.rdoSortOwnerTable);
			this.grpSortMode.Controls.Add(this.rdoSortTable);
			this.grpSortMode.Location = new System.Drawing.Point(8, 264);
			this.grpSortMode.Name = "grpSortMode";
			this.grpSortMode.Size = new System.Drawing.Size(216, 52);
			this.grpSortMode.TabIndex = 7;
			this.grpSortMode.TabStop = false;
			this.grpSortMode.Text = "�\�[�g��";
			// 
			// rdoSortOwnerTable
			// 
			this.rdoSortOwnerTable.Location = new System.Drawing.Point(112, 16);
			this.rdoSortOwnerTable.Name = "rdoSortOwnerTable";
			this.rdoSortOwnerTable.Size = new System.Drawing.Size(88, 32);
			this.rdoSortOwnerTable.TabIndex = 1;
			this.rdoSortOwnerTable.Text = "�I�[�i�[���E�e�[�u����";
			// 
			// rdoSortTable
			// 
			this.rdoSortTable.Location = new System.Drawing.Point(8, 16);
			this.rdoSortTable.Name = "rdoSortTable";
			this.rdoSortTable.Size = new System.Drawing.Size(96, 32);
			this.rdoSortTable.TabIndex = 0;
			this.rdoSortTable.Text = "�e�[�u�����̂�";
			this.rdoSortTable.CheckedChanged += new System.EventHandler(this.rdoSortTable_CheckedChanged);
			// 
			// txtWhere
			// 
			this.txtWhere.Location = new System.Drawing.Point(72, 480);
			this.txtWhere.Name = "txtWhere";
			this.txtWhere.Size = new System.Drawing.Size(144, 19);
			this.txtWhere.TabIndex = 11;
			this.txtWhere.Text = "";
			this.txtWhere.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtWhere_KeyDown);
			this.txtWhere.Leave += new System.EventHandler(this.txtWhere_Leave);
			// 
			// txtSort
			// 
			this.txtSort.Location = new System.Drawing.Point(72, 508);
			this.txtSort.Name = "txtSort";
			this.txtSort.Size = new System.Drawing.Size(144, 19);
			this.txtSort.TabIndex = 14;
			this.txtSort.Text = "";
			this.txtSort.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSort_KeyDown);
			this.txtSort.Leave += new System.EventHandler(this.txtSort_Leave);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 480);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(52, 16);
			this.label1.TabIndex = 10;
			this.label1.Text = "where(&P)";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 508);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 16);
			this.label2.TabIndex = 13;
			this.label2.Text = "order by(&S)";
			// 
			// btnSelect
			// 
			this.btnSelect.Location = new System.Drawing.Point(508, 72);
			this.btnSelect.Name = "btnSelect";
			this.btnSelect.Size = new System.Drawing.Size(136, 24);
			this.btnSelect.TabIndex = 23;
			this.btnSelect.Text = "Select ������(&X)";
			this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
			// 
			// ownerListbox
			// 
			this.ownerListbox.Font = new System.Drawing.Font("�l�r �S�V�b�N", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(128)));
			this.ownerListbox.ItemHeight = 12;
			this.ownerListbox.Location = new System.Drawing.Point(60, 80);
			this.ownerListbox.Name = "ownerListbox";
			this.ownerListbox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.ownerListbox.Size = new System.Drawing.Size(160, 88);
			this.ownerListbox.TabIndex = 4;
			this.ownerListbox.CopyData += new quickDBExplorer.qdbeListBox.CopyDataHandler(this.ownerListbox_CopyData);
			this.ownerListbox.SelectedIndexChanged += new System.EventHandler(this.ownerListbox_SelectedIndexChanged);
			// 
			// btnDDL
			// 
			this.btnDDL.Location = new System.Drawing.Point(508, 100);
			this.btnDDL.Name = "btnDDL";
			this.btnDDL.Size = new System.Drawing.Size(136, 24);
			this.btnDDL.TabIndex = 24;
			this.btnDDL.Text = "�ȈՒ�`������(&D)";
			this.btnDDL.Click += new System.EventHandler(this.btnDDL_Click);
			// 
			// dbGrid
			// 
			this.dbGrid.AlternatingBackColor = System.Drawing.Color.Silver;
			this.dbGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.dbGrid.BackColor = System.Drawing.Color.White;
			this.dbGrid.CaptionBackColor = System.Drawing.Color.Gainsboro;
			this.dbGrid.CaptionFont = new System.Drawing.Font("Tahoma", 8F);
			this.dbGrid.CaptionForeColor = System.Drawing.Color.White;
			this.dbGrid.CaptionVisible = false;
			this.dbGrid.ContextMenu = this.dbGridMenu;
			this.dbGrid.DataMember = "";
			this.dbGrid.Font = new System.Drawing.Font("�l�r �S�V�b�N", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(128)));
			this.dbGrid.ForeColor = System.Drawing.Color.Black;
			this.dbGrid.GridLineColor = System.Drawing.Color.Silver;
			this.dbGrid.HeaderBackColor = System.Drawing.Color.Silver;
			this.dbGrid.HeaderFont = new System.Drawing.Font("�l�r �S�V�b�N", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.dbGrid.HeaderForeColor = System.Drawing.Color.Black;
			this.dbGrid.LinkColor = System.Drawing.Color.Maroon;
			this.dbGrid.Location = new System.Drawing.Point(240, 384);
			this.dbGrid.Name = "dbGrid";
			this.dbGrid.ParentRowsBackColor = System.Drawing.Color.Silver;
			this.dbGrid.ParentRowsForeColor = System.Drawing.Color.Black;
			this.dbGrid.SelectionBackColor = System.Drawing.Color.Maroon;
			this.dbGrid.SelectionForeColor = System.Drawing.Color.White;
			this.dbGrid.Size = new System.Drawing.Size(672, 236);
			this.dbGrid.TabIndex = 39;
			this.dbGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.dbGrid_Paint);
			// 
			// dbGridMenu
			// 
			this.dbGridMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.copyDbGridMenu});
			// 
			// copyDbGridMenu
			// 
			this.copyDbGridMenu.Index = 0;
			this.copyDbGridMenu.Text = "�N���b�v�{�[�h�ɃR�s�[";
			this.copyDbGridMenu.Click += new System.EventHandler(this.copyDbGridMenu_Click);
			// 
			// chkDspData
			// 
			this.chkDspData.Location = new System.Drawing.Point(24, 548);
			this.chkDspData.Name = "chkDspData";
			this.chkDspData.Size = new System.Drawing.Size(52, 24);
			this.chkDspData.TabIndex = 17;
			this.chkDspData.Text = "�\��";
			this.chkDspData.CheckedChanged += new System.EventHandler(this.chkDspData_CheckedChanged);
			// 
			// grpDataDspMode
			// 
			this.grpDataDspMode.Controls.Add(this.txtDspCount);
			this.grpDataDspMode.Controls.Add(this.label3);
			this.grpDataDspMode.Location = new System.Drawing.Point(8, 532);
			this.grpDataDspMode.Name = "grpDataDspMode";
			this.grpDataDspMode.Size = new System.Drawing.Size(216, 44);
			this.grpDataDspMode.TabIndex = 16;
			this.grpDataDspMode.TabStop = false;
			this.grpDataDspMode.Text = "�f�[�^�O���b�h";
			// 
			// txtDspCount
			// 
			this.txtDspCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.txtDspCount.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
			this.txtDspCount.ImeMode = System.Windows.Forms.ImeMode.Disable;
			this.txtDspCount.Location = new System.Drawing.Point(132, 16);
			this.txtDspCount.MaxLength = 300;
			this.txtDspCount.Name = "txtDspCount";
			this.txtDspCount.Size = new System.Drawing.Size(72, 19);
			this.txtDspCount.TabIndex = 1;
			this.txtDspCount.Text = "1000";
			this.txtDspCount.Leave += new System.EventHandler(this.txtDspCount_Leave);
			this.txtDspCount.TextChanged += new System.EventHandler(this.txtDspCount_TextChanged);
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label3.Location = new System.Drawing.Point(72, 20);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 16);
			this.label3.TabIndex = 0;
			this.label3.Text = "�\������";
			// 
			// grpSysUserMode
			// 
			this.grpSysUserMode.Controls.Add(this.rdoNotDspSysUser);
			this.grpSysUserMode.Controls.Add(this.rdoDspSysUser);
			this.grpSysUserMode.Location = new System.Drawing.Point(8, 176);
			this.grpSysUserMode.Name = "grpSysUserMode";
			this.grpSysUserMode.Size = new System.Drawing.Size(216, 40);
			this.grpSysUserMode.TabIndex = 5;
			this.grpSysUserMode.TabStop = false;
			this.grpSysUserMode.Text = "�V�X�e�����[�U�[��";
			// 
			// rdoNotDspSysUser
			// 
			this.rdoNotDspSysUser.Checked = true;
			this.rdoNotDspSysUser.Location = new System.Drawing.Point(112, 16);
			this.rdoNotDspSysUser.Name = "rdoNotDspSysUser";
			this.rdoNotDspSysUser.Size = new System.Drawing.Size(88, 16);
			this.rdoNotDspSysUser.TabIndex = 1;
			this.rdoNotDspSysUser.TabStop = true;
			this.rdoNotDspSysUser.Text = "�\�����Ȃ�";
			this.rdoNotDspSysUser.CheckedChanged += new System.EventHandler(this.rdoNotDspSysUser_CheckedChanged);
			// 
			// rdoDspSysUser
			// 
			this.rdoDspSysUser.Location = new System.Drawing.Point(8, 16);
			this.rdoDspSysUser.Name = "rdoDspSysUser";
			this.rdoDspSysUser.Size = new System.Drawing.Size(88, 16);
			this.rdoDspSysUser.TabIndex = 0;
			this.rdoDspSysUser.Text = "�\������";
			this.rdoDspSysUser.CheckedChanged += new System.EventHandler(this.rdoDspSysUser_CheckedChanged);
			// 
			// grpOutputMode
			// 
			this.grpOutputMode.Controls.Add(this.btnReference);
			this.grpOutputMode.Controls.Add(this.txtOutput);
			this.grpOutputMode.Controls.Add(this.rdoOutFile);
			this.grpOutputMode.Controls.Add(this.rdoClipboard);
			this.grpOutputMode.Controls.Add(this.rdoOutFolder);
			this.grpOutputMode.Location = new System.Drawing.Point(8, 324);
			this.grpOutputMode.Name = "grpOutputMode";
			this.grpOutputMode.Size = new System.Drawing.Size(216, 84);
			this.grpOutputMode.TabIndex = 8;
			this.grpOutputMode.TabStop = false;
			this.grpOutputMode.Text = "�o�͐�";
			// 
			// btnReference
			// 
			this.btnReference.Location = new System.Drawing.Point(168, 52);
			this.btnReference.Name = "btnReference";
			this.btnReference.Size = new System.Drawing.Size(40, 20);
			this.btnReference.TabIndex = 4;
			this.btnReference.Text = "�Q��(&R)";
			this.btnReference.Click += new System.EventHandler(this.btnReference_Click);
			// 
			// txtOutput
			// 
			this.txtOutput.Location = new System.Drawing.Point(8, 52);
			this.txtOutput.Name = "txtOutput";
			this.txtOutput.Size = new System.Drawing.Size(160, 19);
			this.txtOutput.TabIndex = 3;
			this.txtOutput.Text = "";
			this.txtOutput.TextChanged += new System.EventHandler(this.txtOutput_TextChanged);
			// 
			// rdoOutFile
			// 
			this.rdoOutFile.Location = new System.Drawing.Point(8, 32);
			this.rdoOutFile.Name = "rdoOutFile";
			this.rdoOutFile.Size = new System.Drawing.Size(88, 16);
			this.rdoOutFile.TabIndex = 1;
			this.rdoOutFile.Text = "�P�ƃt�@�C��";
			this.rdoOutFile.CheckedChanged += new System.EventHandler(this.rdoOutFile_CheckedChanged);
			// 
			// rdoClipboard
			// 
			this.rdoClipboard.Location = new System.Drawing.Point(8, 12);
			this.rdoClipboard.Name = "rdoClipboard";
			this.rdoClipboard.Size = new System.Drawing.Size(88, 16);
			this.rdoClipboard.TabIndex = 0;
			this.rdoClipboard.Text = "�N���b�v�{�[�h";
			this.rdoClipboard.CheckedChanged += new System.EventHandler(this.rdoClipboard_CheckedChanged);
			// 
			// rdoOutFolder
			// 
			this.rdoOutFolder.Location = new System.Drawing.Point(104, 32);
			this.rdoOutFolder.Name = "rdoOutFolder";
			this.rdoOutFolder.Size = new System.Drawing.Size(88, 16);
			this.rdoOutFolder.TabIndex = 2;
			this.rdoOutFolder.Text = "�����t�@�C��";
			this.rdoOutFolder.CheckedChanged += new System.EventHandler(this.rdoOutFolder_CheckedChanged);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 28);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(48, 32);
			this.label4.TabIndex = 0;
			this.label4.Text = "DB(&B)";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(4, 80);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(48, 32);
			this.label5.TabIndex = 3;
			this.label5.Text = "owner/Role(&O)";
			// 
			// fieldListbox
			// 
			this.fieldListbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.fieldListbox.ContextMenu = this.fldContextMenu;
			this.fieldListbox.Font = new System.Drawing.Font("�l�r �S�V�b�N", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(128)));
			this.fieldListbox.HorizontalScrollbar = true;
			this.fieldListbox.ItemHeight = 12;
			this.fieldListbox.Location = new System.Drawing.Point(656, 40);
			this.fieldListbox.Name = "fieldListbox";
			this.fieldListbox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.fieldListbox.Size = new System.Drawing.Size(240, 268);
			this.fieldListbox.TabIndex = 32;
			this.fieldListbox.CopyData += new quickDBExplorer.qdbeListBox.CopyDataHandler(this.fieldListbox_CopyData);
			// 
			// fldContextMenu
			// 
			this.fldContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						   this.fldmenuCopy,
																						   this.fldmenuCopyNoCRLF,
																						   this.fldmenuCopyNoComma,
																						   this.fldmenuCopyNoCRLFNoComma});
			// 
			// fldmenuCopy
			// 
			this.fldmenuCopy.Index = 0;
			this.fldmenuCopy.Text = "�R�s�[";
			this.fldmenuCopy.Click += new System.EventHandler(this.fldmenuCopy_Click);
			// 
			// fldmenuCopyNoCRLF
			// 
			this.fldmenuCopyNoCRLF.Index = 1;
			this.fldmenuCopyNoCRLF.Text = "���s�Ȃ��R�s�[";
			this.fldmenuCopyNoCRLF.Click += new System.EventHandler(this.fldmenuCopyNoCRLF_Click);
			// 
			// fldmenuCopyNoComma
			// 
			this.fldmenuCopyNoComma.Index = 2;
			this.fldmenuCopyNoComma.Text = "�R�s�[�J���}�Ȃ�";
			this.fldmenuCopyNoComma.Click += new System.EventHandler(this.fldmenuCopyNoComma_Click);
			// 
			// fldmenuCopyNoCRLFNoComma
			// 
			this.fldmenuCopyNoCRLFNoComma.Index = 3;
			this.fldmenuCopyNoCRLFNoComma.Text = "�R�s�[���s�E�J���}�Ȃ�";
			this.fldmenuCopyNoCRLFNoComma.Click += new System.EventHandler(this.fldmenuCopyNoCRLFNoComma_Click);
			// 
			// grpCharaSet
			// 
			this.grpCharaSet.Controls.Add(this.rdoUtf8);
			this.grpCharaSet.Controls.Add(this.rdoSjis);
			this.grpCharaSet.Controls.Add(this.rdoUnicode);
			this.grpCharaSet.Location = new System.Drawing.Point(8, 412);
			this.grpCharaSet.Name = "grpCharaSet";
			this.grpCharaSet.Size = new System.Drawing.Size(216, 60);
			this.grpCharaSet.TabIndex = 9;
			this.grpCharaSet.TabStop = false;
			this.grpCharaSet.Text = "�o�͕����R�[�h";
			// 
			// rdoUtf8
			// 
			this.rdoUtf8.Location = new System.Drawing.Point(8, 36);
			this.rdoUtf8.Name = "rdoUtf8";
			this.rdoUtf8.Size = new System.Drawing.Size(80, 16);
			this.rdoUtf8.TabIndex = 2;
			this.rdoUtf8.Text = "UTF-8";
			this.rdoUtf8.CheckedChanged += new System.EventHandler(this.rdoUtf8_CheckedChanged);
			// 
			// rdoSjis
			// 
			this.rdoSjis.Location = new System.Drawing.Point(104, 16);
			this.rdoSjis.Name = "rdoSjis";
			this.rdoSjis.Size = new System.Drawing.Size(80, 16);
			this.rdoSjis.TabIndex = 1;
			this.rdoSjis.Text = "ShiftJIS";
			this.rdoSjis.CheckedChanged += new System.EventHandler(this.rdoSjis_CheckedChanged);
			// 
			// rdoUnicode
			// 
			this.rdoUnicode.Location = new System.Drawing.Point(8, 16);
			this.rdoUnicode.Name = "rdoUnicode";
			this.rdoUnicode.Size = new System.Drawing.Size(92, 16);
			this.rdoUnicode.TabIndex = 0;
			this.rdoUnicode.Text = "UNICODE";
			this.rdoUnicode.CheckedChanged += new System.EventHandler(this.rdoUnicode_CheckedChanged);
			// 
			// chkDspFieldAttr
			// 
			this.chkDspFieldAttr.Location = new System.Drawing.Point(656, 16);
			this.chkDspFieldAttr.Name = "chkDspFieldAttr";
			this.chkDspFieldAttr.Size = new System.Drawing.Size(244, 20);
			this.chkDspFieldAttr.TabIndex = 31;
			this.chkDspFieldAttr.Text = "�t�B�[���h������\��(&Z)";
			this.chkDspFieldAttr.CheckedChanged += new System.EventHandler(this.chkDspFieldAttr_CheckedChanged);
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label6.Font = new System.Drawing.Font("MS UI Gothic", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(128)));
			this.label6.Location = new System.Drawing.Point(4, 657);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(124, 12);
			this.label6.TabIndex = 27;
			this.label6.Text = "C info;";
			this.toolTip2.SetToolTip(this.label6, "Copyright; Y.N(godz)  2004-2006");
			// 
			// btnQuerySelect
			// 
			this.btnQuerySelect.Location = new System.Drawing.Point(508, 212);
			this.btnQuerySelect.Name = "btnQuerySelect";
			this.btnQuerySelect.Size = new System.Drawing.Size(136, 24);
			this.btnQuerySelect.TabIndex = 28;
			this.btnQuerySelect.Text = "�N�G���w�茋�ʕ\��(&J)";
			this.btnQuerySelect.Click += new System.EventHandler(this.btnQuerySelect_Click);
			// 
			// btnDataUpdate
			// 
			this.btnDataUpdate.Location = new System.Drawing.Point(508, 296);
			this.btnDataUpdate.Name = "btnDataUpdate";
			this.btnDataUpdate.Size = new System.Drawing.Size(132, 24);
			this.btnDataUpdate.TabIndex = 30;
			this.btnDataUpdate.Text = "�f�[�^�X�V(&U)";
			this.btnDataUpdate.Click += new System.EventHandler(this.btnDataUpdate_Click);
			// 
			// btnDataEdit
			// 
			this.btnDataEdit.Location = new System.Drawing.Point(508, 268);
			this.btnDataEdit.Name = "btnDataEdit";
			this.btnDataEdit.Size = new System.Drawing.Size(132, 24);
			this.btnDataEdit.TabIndex = 29;
			this.btnDataEdit.Text = "�f�[�^�ҏW(&T)";
			this.btnDataEdit.Click += new System.EventHandler(this.btnDataEdit_Click);
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(244, 328);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(368, 16);
			this.label7.TabIndex = 33;
			this.label7.Text = "���o���Ɂ���������NULL�ł��BNULL�̃Z���͐��F�ɒ��F����܂��B";
			// 
			// btnGridFormat
			// 
			this.btnGridFormat.Location = new System.Drawing.Point(752, 336);
			this.btnGridFormat.Name = "btnGridFormat";
			this.btnGridFormat.Size = new System.Drawing.Size(156, 20);
			this.btnGridFormat.TabIndex = 37;
			this.btnGridFormat.Text = "�O���b�h�\�������w��(&G)";
			this.btnGridFormat.Click += new System.EventHandler(this.btnGridFormat_Click);
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(244, 344);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(500, 16);
			this.label8.TabIndex = 34;
			this.label8.Text = "NULL����͂���ɂ�Ctrl+1 ���A�󕶎������͂���ɂ�Ctrl+2���������܂��BCtrl+3�Œl�g��\���B";
			// 
			// btnIndex
			// 
			this.btnIndex.Location = new System.Drawing.Point(508, 156);
			this.btnIndex.Name = "btnIndex";
			this.btnIndex.Size = new System.Drawing.Size(136, 23);
			this.btnIndex.TabIndex = 26;
			this.btnIndex.Text = "INDEX���\��(&N)";
			this.btnIndex.Click += new System.EventHandler(this.btnIndex_Click);
			// 
			// btnRedisp
			// 
			this.btnRedisp.Location = new System.Drawing.Point(640, 360);
			this.btnRedisp.Name = "btnRedisp";
			this.btnRedisp.Size = new System.Drawing.Size(108, 20);
			this.btnRedisp.TabIndex = 36;
			this.btnRedisp.Text = "�O���b�h�ĕ`��(&L)";
			this.btnRedisp.Click += new System.EventHandler(this.Redisp_Click);
			// 
			// btnTmpAllDsp
			// 
			this.btnTmpAllDsp.Location = new System.Drawing.Point(752, 360);
			this.btnTmpAllDsp.Name = "btnTmpAllDsp";
			this.btnTmpAllDsp.Size = new System.Drawing.Size(156, 20);
			this.btnTmpAllDsp.TabIndex = 38;
			this.btnTmpAllDsp.Text = "�ꎞ�I�ɑS�f�[�^��\��(&A)";
			this.btnTmpAllDsp.Click += new System.EventHandler(this.btnTmpAllDsp_Click);
			// 
			// btnEtc
			// 
			this.btnEtc.Location = new System.Drawing.Point(508, 184);
			this.btnEtc.Name = "btnEtc";
			this.btnEtc.Size = new System.Drawing.Size(136, 23);
			this.btnEtc.TabIndex = 27;
			this.btnEtc.Text = "���̑�(&E)";
			this.btnEtc.Click += new System.EventHandler(this.btnEtc_Click);
			// 
			// etcContextMenu
			// 
			this.etcContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						   this.menuQuery,
																						   this.menuSeparater5,
																						   this.menuISQL,
																						   this.menuProfile,
																						   this.menuEPM,
																						   this.menuSeparater6,
																						   this.menuDependBtn,
																						   this.menuRecordCount,
																						   this.menuRecordCountDsp});
			// 
			// menuQuery
			// 
			this.menuQuery.Index = 0;
			this.menuQuery.Text = "(&1) �ȈՃN�G�����s�iSelect�ȊO�j";
			this.menuQuery.Click += new System.EventHandler(this.btnQueryNonSelect_Click);
			// 
			// menuSeparater5
			// 
			this.menuSeparater5.Index = 1;
			this.menuSeparater5.Text = "-";
			// 
			// menuISQL
			// 
			this.menuISQL.Index = 2;
			this.menuISQL.Text = "(&2) �N�G���A�i���C�U�N��";
			this.menuISQL.Click += new System.EventHandler(this.CallISQLW);
			// 
			// menuProfile
			// 
			this.menuProfile.Index = 3;
			this.menuProfile.Text = "(&3) �v���t�@�C���N��";
			this.menuProfile.Click += new System.EventHandler(this.CallProfile);
			// 
			// menuEPM
			// 
			this.menuEPM.Index = 4;
			this.menuEPM.Text = "(&4) �G���^�[�v���C�Y�}�l�[�W���[�N��";
			this.menuEPM.Click += new System.EventHandler(this.CallEPM);
			// 
			// menuSeparater6
			// 
			this.menuSeparater6.Index = 5;
			this.menuSeparater6.Text = "-";
			// 
			// menuDependBtn
			// 
			this.menuDependBtn.Index = 6;
			this.menuDependBtn.Text = "(&5) �ˑ��֌W�o��";
			this.menuDependBtn.Click += new System.EventHandler(this.DependOutPut);
			// 
			// menuRecordCount
			// 
			this.menuRecordCount.Index = 7;
			this.menuRecordCount.Text = "(&6) �f�[�^�����o��";
			this.menuRecordCount.Click += new System.EventHandler(this.RecordCountOutPut);
			// 
			// menuRecordCountDsp
			// 
			this.menuRecordCountDsp.Index = 8;
			this.menuRecordCountDsp.Text = "(&7) �f�[�^�����\��";
			this.menuRecordCountDsp.Click += new System.EventHandler(this.menuRecordCountDsp_Click);
			// 
			// btnWhereZoom
			// 
			this.btnWhereZoom.Location = new System.Drawing.Point(220, 484);
			this.btnWhereZoom.Name = "btnWhereZoom";
			this.btnWhereZoom.Size = new System.Drawing.Size(16, 20);
			this.btnWhereZoom.TabIndex = 12;
			this.btnWhereZoom.Click += new System.EventHandler(this.btnWhereZoom_Click);
			// 
			// btnOrderZoom
			// 
			this.btnOrderZoom.Location = new System.Drawing.Point(220, 508);
			this.btnOrderZoom.Name = "btnOrderZoom";
			this.btnOrderZoom.Size = new System.Drawing.Size(16, 20);
			this.btnOrderZoom.TabIndex = 15;
			this.btnOrderZoom.Click += new System.EventHandler(this.btnOrderZoom_Click);
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(244, 364);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(280, 16);
			this.label9.TabIndex = 35;
			this.label9.Text = "�����s�ɂ킽�镶����͂Ƃ��F(�s���N)�ɒ��F����܂��B";
			// 
			// useCheckBox
			// 
			this.useCheckBox.Location = new System.Drawing.Point(0, 124);
			this.useCheckBox.Name = "useCheckBox";
			this.useCheckBox.Size = new System.Drawing.Size(56, 36);
			this.useCheckBox.TabIndex = 2;
			this.useCheckBox.Text = "CheckList";
			this.useCheckBox.Visible = false;
			this.useCheckBox.CheckedChanged += new System.EventHandler(this.useCheckBox_CheckedChanged);
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(240, 4);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(152, 16);
			this.label10.TabIndex = 18;
			this.label10.Text = "Table/View(&V)";
			// 
			// cmbHistory
			// 
			this.cmbHistory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbHistory.DropDownWidth = 300;
			this.cmbHistory.Location = new System.Drawing.Point(320, 0);
			this.cmbHistory.MaxDropDownItems = 11;
			this.cmbHistory.Name = "cmbHistory";
			this.cmbHistory.Size = new System.Drawing.Size(176, 20);
			this.cmbHistory.TabIndex = 19;
			this.cmbHistory.SelectionChangeCommitted += new System.EventHandler(this.cmbHistory_SelectionChangeCommitted);
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(928, 637);
			this.Controls.Add(this.cmbHistory);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.useCheckBox);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.btnWhereZoom);
			this.Controls.Add(this.btnEtc);
			this.Controls.Add(this.btnTmpAllDsp);
			this.Controls.Add(this.btnRedisp);
			this.Controls.Add(this.btnIndex);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.btnGridFormat);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.btnDataEdit);
			this.Controls.Add(this.btnDataUpdate);
			this.Controls.Add(this.btnQuerySelect);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.chkDspFieldAttr);
			this.Controls.Add(this.grpCharaSet);
			this.Controls.Add(this.fieldListbox);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.grpOutputMode);
			this.Controls.Add(this.chkDspData);
			this.Controls.Add(this.dbGrid);
			this.Controls.Add(this.btnDDL);
			this.Controls.Add(this.ownerListbox);
			this.Controls.Add(this.btnSelect);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtSort);
			this.Controls.Add(this.txtWhere);
			this.Controls.Add(this.grpSortMode);
			this.Controls.Add(this.grpViewMode);
			this.Controls.Add(this.btnCSV);
			this.Controls.Add(this.btnFieldList);
			this.Controls.Add(this.btnInsert);
			this.Controls.Add(this.tableList);
			this.Controls.Add(this.dbList);
			this.Controls.Add(this.grpDataDspMode);
			this.Controls.Add(this.grpSysUserMode);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.btnOrderZoom);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.ShowInTaskbar = false;
			this.Text = "DataBase�I��";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.Controls.SetChildIndex(this.btnOrderZoom, 0);
			this.Controls.SetChildIndex(this.label5, 0);
			this.Controls.SetChildIndex(this.grpSysUserMode, 0);
			this.Controls.SetChildIndex(this.grpDataDspMode, 0);
			this.Controls.SetChildIndex(this.dbList, 0);
			this.Controls.SetChildIndex(this.tableList, 0);
			this.Controls.SetChildIndex(this.btnInsert, 0);
			this.Controls.SetChildIndex(this.btnFieldList, 0);
			this.Controls.SetChildIndex(this.btnCSV, 0);
			this.Controls.SetChildIndex(this.grpViewMode, 0);
			this.Controls.SetChildIndex(this.grpSortMode, 0);
			this.Controls.SetChildIndex(this.txtWhere, 0);
			this.Controls.SetChildIndex(this.txtSort, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.label2, 0);
			this.Controls.SetChildIndex(this.btnSelect, 0);
			this.Controls.SetChildIndex(this.ownerListbox, 0);
			this.Controls.SetChildIndex(this.btnDDL, 0);
			this.Controls.SetChildIndex(this.dbGrid, 0);
			this.Controls.SetChildIndex(this.chkDspData, 0);
			this.Controls.SetChildIndex(this.grpOutputMode, 0);
			this.Controls.SetChildIndex(this.label4, 0);
			this.Controls.SetChildIndex(this.fieldListbox, 0);
			this.Controls.SetChildIndex(this.grpCharaSet, 0);
			this.Controls.SetChildIndex(this.chkDspFieldAttr, 0);
			this.Controls.SetChildIndex(this.label6, 0);
			this.Controls.SetChildIndex(this.btnQuerySelect, 0);
			this.Controls.SetChildIndex(this.btnDataUpdate, 0);
			this.Controls.SetChildIndex(this.btnDataEdit, 0);
			this.Controls.SetChildIndex(this.label7, 0);
			this.Controls.SetChildIndex(this.btnGridFormat, 0);
			this.Controls.SetChildIndex(this.label8, 0);
			this.Controls.SetChildIndex(this.btnIndex, 0);
			this.Controls.SetChildIndex(this.btnRedisp, 0);
			this.Controls.SetChildIndex(this.btnTmpAllDsp, 0);
			this.Controls.SetChildIndex(this.btnEtc, 0);
			this.Controls.SetChildIndex(this.btnWhereZoom, 0);
			this.Controls.SetChildIndex(this.label9, 0);
			this.Controls.SetChildIndex(this.useCheckBox, 0);
			this.Controls.SetChildIndex(this.label10, 0);
			this.Controls.SetChildIndex(this.cmbHistory, 0);
			this.Controls.SetChildIndex(this.msgArea, 0);
			this.grpViewMode.ResumeLayout(false);
			this.grpSortMode.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dbGrid)).EndInit();
			this.grpDataDspMode.ResumeLayout(false);
			this.grpSysUserMode.ResumeLayout(false);
			this.grpOutputMode.ResumeLayout(false);
			this.grpCharaSet.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// �����\������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Load(object sender, System.EventArgs e)
		{

			this.InitErrMessage();

			try
			{
				this.Text = servername;
				SqlDataAdapter da = new SqlDataAdapter("SELECT name FROM sysdatabases order by name", this.sqlConnection1);
				DataSet ds = new DataSet();
				ds.CaseSensitive = true;
				da.Fill(ds,"sysdatabases");

				foreach (DataRow row in ds.Tables["sysdatabases"].Rows )
				{
					this.dbList.Items.Add(row["name"]);
				}
			}
			catch ( System.Data.SqlClient.SqlException se )
			{
				this.SetErrorMessage(se);
			}
			if( svdata.isShowsysuser == 0 )
			{
				this.rdoNotDspSysUser.Checked = true;
				this.rdoDspSysUser.Checked = false;
			}
			else
			{
				this.rdoNotDspSysUser.Checked = false;
				this.rdoDspSysUser.Checked = true;
			}
			if( svdata.sortKey == 0 )
			{
				this.rdoSortTable.Checked = false;
				this.rdoSortOwnerTable.Checked = true;
			}
			else
			{
				this.rdoSortTable.Checked = true;
				this.rdoSortOwnerTable.Checked = false;
			}
			if( svdata.showView == 0 )
			{
				this.rdoDspView.Checked = false;
				this.rdoNotDspView.Checked = true;
			}
			else
			{
				this.rdoDspView.Checked = true;
				this.rdoNotDspView.Checked = false;
			}

			// �O��̒l������DB���ύX����
			if(  svdata.lastdb != null && svdata.lastdb != "" )
			{
				for( int i = 0; i < this.dbList.Items.Count; i++  )
				{
					if( (string)this.dbList.Items[i] == svdata.lastdb )
					{
						this.dbList.SetSelected(i,true);
						this.dbList.Focus();
						break;
					}
				}
			}
			else
			{
				this.dbList.SelectedIndex = 0;
			}
			gfont = this.dbGrid.Font;
			gcolor = this.dbGrid.ForeColor;

			// �{�^���̕\���F���L�����Ă���
			this.btnBackColor = this.btnDataEdit.BackColor;
			this.btnForeColor = this.btnDataEdit.ForeColor;
		}

		private void dbList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if( this.dbList.SelectedItems.Count != 0 )
			{
				svdata.lastdb = (string)this.dbList.SelectedItem;
			}
			// �e�[�u���̑I�𗚗����N���A
			this.selectedTables.Clear();
			this.cmbHistory.DataSource = null;
			this.cmbHistory.DataSource = this.selectedTables;
			this.cmbHistory.Refresh();
			

			dsplist2();
			displistowner();
			if( svdata.dbopt[svdata.lastdb] != null )
			{
				// �Y��DB�̍Ō�ɑI���������[�U�[��I������
				for( int i = 0; i < this.ownerListbox.Items.Count; i++ )
				{
					if( (string)this.ownerListbox.Items[i] == (string)svdata.dbopt[svdata.lastdb] )
					{
						this.ownerListbox.SetSelected(i, true);
						this.ownerListbox.Focus();
						break;
					}
				}
			}

			if( this.ownerListbox.SelectedItems.Count == 0 )
			{
				// �I�����Ȃ��ꍇ�A��ԍŏ����f�B�t�H���g�őI������
				this.ownerListbox.SetSelected(0,true);
			}
			if( svdata.outdest[svdata.lastdb] != null )
			{
				// �Y��DB�̍Ō�̏o�͐���Z�b�g����
				switch( (int)svdata.outdest[svdata.lastdb] )
				{
					case	0:
						//�N���b�v�{�[�h
						this.rdoClipboard.Checked = true;
						this.rdoOutFile.Checked = false;
						this.rdoOutFolder.Checked = false;
						break;
					case	1:
						this.rdoClipboard.Checked = false;
						this.rdoOutFile.Checked = true;
						this.rdoOutFolder.Checked = false;
						break;
					case	2:
						this.rdoClipboard.Checked = false;
						this.rdoOutFile.Checked = false;
						this.rdoOutFolder.Checked = true;
						break;
				}
			}
			else
			{
				//�W���̓N���b�v�{�[�h
				this.rdoClipboard.Checked = true;
				this.rdoOutFile.Checked = false;
				this.rdoOutFolder.Checked = false;
			}

			if( svdata.outfile[svdata.lastdb] != null )
			{
				this.txtOutput.Text = (string)svdata.outfile[svdata.lastdb];
			}
			else
			{
				this.txtOutput.Text = "";
			}
			

			if( svdata.showgrid[svdata.lastdb] != null )
			{
				if( (int)svdata.showgrid[svdata.lastdb] == 0 )
				{
					this.chkDspData.CheckState = CheckState.Unchecked;
				}
				else
				{
					this.chkDspData.CheckState = CheckState.Checked;
				}	
			}
			else
			{
				this.chkDspData.CheckState = CheckState.Checked;
			}

			if( svdata.griddspcnt[svdata.lastdb] != null )
			{
				if( (string)svdata.griddspcnt[svdata.lastdb] != "" )
				{
					this.txtDspCount.Text = (string)svdata.griddspcnt[svdata.lastdb];
				}
				else
				{
					this.txtDspCount.Text = "";
				}
			}
			else
			{
				this.txtDspCount.Text = "1000";
			}

			if( svdata.txtencode[svdata.lastdb] != null )
			{
				if( (int)svdata.txtencode[svdata.lastdb] == 0 )
				{
					this.rdoUnicode.Checked = true;
				}
				else if( (int)svdata.txtencode[svdata.lastdb] == 1 )
				{
					this.rdoSjis.Checked = true;
				}
				else
				{
					this.rdoUtf8.Checked = true;
				}
			}
			else
			{
				this.rdoUnicode.Checked = true;
			}
			this.Text = servername + "@" + (string)this.dbList.SelectedItem;
			this.dbList.Focus();	//�t�H�[�J�X�����ɖ߂�
		}

		// INSERT������
		private void btnInsert_Click(object sender, System.EventArgs e)
		{
			MenuItem[] list = new MenuItem[] {
												 this.menuInsert,
												 this.menuInsertDelete,
												 this.menuInsertNoFld,
												 this.menuInsertNoFldDelete,
												 this.menuInsertDeleteTaihi,
												 this.menuInsertNoFldDeleteTaihi
											 };

			ContextMenu tmpmenu = new System.Windows.Forms.ContextMenu();
			MenuItem[] cplist = new MenuItem[list.Length];
			for( int i = 0; i<list.Length; i++ )
			{
				cplist[i] = list[i].CloneMenu();
				cplist[i].Text = string.Format("(&{0}) {1}",i+1,cplist[i].Text );
			}
			tmpmenu.MenuItems.AddRange(cplist);

			tmpmenu.Show(this.btnInsert,new Point(0,0));
		}
		
		// �t�B�[���h���X�g����
		private void btnFieldList_Click(object sender, System.EventArgs e)
		{
			MenuItem[] list = new MenuItem[] {
												 this.menuMakeFld,
												 this.menuMakeFldCRLF,
												 this.menuMakeFldNoComma
											 };
			ContextMenu tmpmenu = new System.Windows.Forms.ContextMenu();
			MenuItem[] cplist = new MenuItem[list.Length];
			for( int i = 0; i<list.Length; i++ )
			{
				cplist[i] = list[i].CloneMenu();
				cplist[i].Text = string.Format("(&{0}) {1}",i+1,cplist[i].Text );
			}
			tmpmenu.MenuItems.AddRange(cplist);

			tmpmenu.Show(this.btnFieldList,new Point(0,0));
		}

		// CSV����
		private void btnCSV_Click(object sender, System.EventArgs e)
		{
			MenuItem[] list = new MenuItem[] {
												 this.menuMakeCSV,
												 this.menuMakeCSVDQ,
												 this.menuMakeTab,
												 this.menuMakeTabDQ
											 };
			ContextMenu tmpmenu = new System.Windows.Forms.ContextMenu();
			MenuItem[] cplist = new MenuItem[list.Length];
			for( int i = 0; i<list.Length; i++ )
			{
				cplist[i] = list[i].CloneMenu();
				cplist[i].Text = string.Format("(&{0}) {1}",i+1,cplist[i].Text );
			}
			tmpmenu.MenuItems.AddRange(cplist);

			tmpmenu.Show(this.btnCSV,new Point(0,0));
		}

		// ��`������
		private void btnDDL_Click(object sender, System.EventArgs e)
		{
			MenuItem[] list = new MenuItem[] {
												 this.menuDDL,
												 this.menuDDLDrop
											 };
			ContextMenu tmpmenu = new System.Windows.Forms.ContextMenu();
			MenuItem[] cplist = new MenuItem[list.Length];
			for( int i = 0; i<list.Length; i++ )
			{
				cplist[i] = list[i].CloneMenu();
				cplist[i].Text = string.Format("(&{0}) {1}",i+1,cplist[i].Text );
			}
			tmpmenu.MenuItems.AddRange(cplist);

			tmpmenu.Show(this.btnDDL,new Point(0,0));
		}

		private void insertmake(object sender, System.EventArgs e)
		{
			this.CreInsert(true,false,false);
		}

		private bool CheckFileSpec()
		{
			if( this.rdoOutFile.Checked == true ) 
			{
				if( this.txtOutput.Text == "" )
				{
					this.saveFileDialog1.CreatePrompt = true;
					this.saveFileDialog1.Filter = "SQL|*.sql|CSV|*.csv|TXT|*.txt|�S��|*.*";
					DialogResult ret = this.saveFileDialog1.ShowDialog();
					if( ret == DialogResult.OK )
					{
						this.txtOutput.Text = this.saveFileDialog1.FileName;
					}
				}
				else
				{
					DirectoryInfo d = new DirectoryInfo(this.txtOutput.Text);
					if( d.Exists )
					{
						MessageBox.Show("�w�肳�ꂽ�t�@�C�����̓t�H���_���w���Ă��܂��B�t�@�C�������w�肵�Ă��������B�����𒆒f���܂�");
						return false;
					}
				}
				if( this.txtOutput.Text == "" )
				{
					MessageBox.Show("�t�@�C�������w�肳��Ă��Ȃ��̂ŁA�����𒆒f���܂�");
					return false;
				}
			}
			if( this.rdoOutFolder.Checked == true )
			{

				if( this.txtOutput.Text == "" )
				{
					this.folderBrowserDialog1.SelectedPath = "";
					this.folderBrowserDialog1.ShowNewFolderButton = true;
					DialogResult ret = this.folderBrowserDialog1.ShowDialog();
					if( ret == DialogResult.OK )
					{
						this.txtOutput.Text = this.folderBrowserDialog1.SelectedPath;
					}
				}

				if( this.txtOutput.Text != "" )
				{
					DirectoryInfo d = new DirectoryInfo(this.txtOutput.Text);
					FileInfo	f = new FileInfo(this.txtOutput.Text);
					if( f.Exists )
					{
						MessageBox.Show("�t�H���_���͎w�肳��Ă��܂����A�t�H���_�ł͂���܂���B�����𒆒f���܂�");
						return false;
					}
					else if( !d.Exists )
					{
						Directory.CreateDirectory(this.txtOutput.Text);
						DirectoryInfo ff = new DirectoryInfo(this.txtOutput.Text);
						if( !ff.Exists )
						{
							MessageBox.Show("�t�H���_���͎w�肳��Ă��܂����A�쐬�ł��܂���ł����B�����𒆒f���܂�");
							return false;
						}
					}
				}
				else
				{
					MessageBox.Show("�t�H���_�����w�肳��܂���ł����B�����𒆒f���܂�");
					return false;
				}
				
			}
			return true;
		}

		private void CreInsert(bool fieldlst, bool deletefrom, bool isTaihi)
		{
			try
			{
				this.InitErrMessage();
				// insert ���̍쐬
				if( this.tableList.SelectedItems.Count == 0 )
				{
					return;
				}
				if( this.tableList.SelectedItems.Count > 1 &&
					this.txtWhere.Text != null &&
					this.txtWhere.Text.Trim() != "" )
				{
					if( MessageBox.Show("�����e�[�u���ɓ���� where ���K�p���܂����H","�m�F",System.Windows.Forms.MessageBoxButtons.YesNo) 
						== System.Windows.Forms.DialogResult.No )
					{
						return;
					}
				}
				if( this.tableList.SelectedItems.Count > 1 &&
					this.txtSort.Text != null &&
					this.txtSort.Text.Trim() != "" )
				{
					if( MessageBox.Show("�����e�[�u���ɓ���� order by ���K�p���܂����H","�m�F",System.Windows.Forms.MessageBoxButtons.YesNo) 
						== System.Windows.Forms.DialogResult.No )
					{
						return;
					}
				}
			
				if( CheckFileSpec() == false )
				{
					return;
				}
			
				int			rowcount = 0;
				int			trow	= 0;
				StringBuilder strline =  new StringBuilder();
				TextWriter	wr = new StringWriter(strline);
				StringBuilder fname = new StringBuilder();

				if( this.rdoClipboard.Checked == true) 
				{
					wr = new StringWriter(strline);
				}
				else if( this.rdoOutFile.Checked == true ) 
				{
					StreamWriter sw = new StreamWriter(this.txtOutput.Text,false,GetEncode());
					sw.AutoFlush = false;
					wr = sw;
					fname.Append(this.txtOutput.Text);
				}
				if( this.rdoOutFolder.Checked == false ) 
				{
					wr.Write("SET NOCOUNT ON{0}GO{0}{0}",wr.NewLine);
				}

				SqlDataReader dr = null;
				SqlCommand	cm = new SqlCommand();
				cm.CommandTimeout = this.SqlTimeOut;
			

				foreach( String tbname in this.tableList.SelectedItems )
				{
					if( this.rdoOutFolder.Checked == true ) 
					{
						StreamWriter sw = new StreamWriter(this.txtOutput.Text + "\\" + tbname + ".sql.tmp",false, GetEncode());
						sw.AutoFlush = false;
						wr = sw;
						wr.Write("SET NOCOUNT ON{0}GO{0}{0}",wr.NewLine);
					}

					// get id 
					string sqlstr;
					sqlstr = string.Format("select  * from {0} ",gettbname(tbname));
					//sqlstr = "select * from [" + tbname + "]";
					if( this.txtWhere.Text.Trim() != "" )
					{
						sqlstr += " where " + this.txtWhere.Text.Trim();
					}
					if( this.txtSort.Text.Trim() != "" )
					{
						sqlstr += " order by " + this.txtSort.Text.Trim();
					}
					cm.CommandText = sqlstr;
					cm.Connection = this.sqlConnection1;

					dr = cm.ExecuteReader();

					ArrayList fldname = new ArrayList();
					ArrayList strint = new ArrayList();
					int			maxcol;
	
					fldname.Clear();
					strint.Clear();

					maxcol = dr.FieldCount;
					for( int j=0 ; j < maxcol; j++ )
					{
						fldname.Add( dr.GetName(j) );
						strint.Add( dr.GetFieldType(j) );
					}
					//ds.Tables[tbname].Columns.Count;

					if( isTaihi == true )
					{
						string taihistr = 
							String.Format("select * into {1} from {0} ",
							gettbname(tbname),
							gettbnameAdd(tbname,DateTime.Now.ToString("yyyyMMdd")) 
							);
						wr.Write(taihistr);
						wr.Write("{0}GO{0}",wr.NewLine );

					}

					if( deletefrom == true && dr.HasRows == true)
					{
						wr.Write("delete from  ");
						string delimStr = ".";
						string []tbstr = tbname.Split(delimStr.ToCharArray(), 2);
						wr.Write(string.Format("[{0}].[{1}]",tbstr[0],tbstr[1]));
						if( this.txtWhere.Text.Trim() != "" )
						{
							wr.Write( " where {0}", this.txtWhere.Text.Trim() );

						}
						wr.Write("{0}GO{0}",wr.NewLine );
					}


					trow	= 0;
					while(dr.Read())
					{
						if( trow != 0 && ( trow % 1000 == 0 ) )
						{
							wr.Write("GO{0}",wr.NewLine);
						}
						trow++;
						rowcount ++;
						if( fieldlst == true )
						{
							wr.Write("insert into {0} ( ", gettbname(tbname) );
							for( int i = 0 ; i < maxcol; i++ )
							{
								if( i != 0 )
								{
									wr.Write(",");
								}
								wr.Write( fldname[i] );
							}
							wr.Write(" ) values ( " );
						}
						else
						{
							wr.Write("insert into {0} values ( ", gettbname(tbname) );
							//wr.Write("insert into [{0}] values ( ", tbname );
						}

						string fldtypename;

						for( int i = 0 ; i < maxcol; i++ )
						{
							if( i != 0 )
							{
								wr.Write( ", " );
							}
							fldtypename = dr.GetDataTypeName(i);
							if( dr.IsDBNull(i) )
							{
								wr.Write( "null" );
							}
							else if( fldtypename.Equals("int") )
							{
								wr.Write( dr.GetInt32(i).ToString() );
							}
							else if( fldtypename.Equals("bigint") )
							{
								wr.Write( dr.GetInt64(i).ToString() );
							}
							else if( fldtypename.Equals("image") ||
								fldtypename.Equals("varbinary") ||
								fldtypename.Equals("binary"))
							{
								wr.Write("'{0}'", dr.GetSqlBinary(i).ToString() );
							}
							else if( fldtypename.Equals("decimal") 
								|| fldtypename.Equals("numeric"))
							{
								wr.Write( dr.GetDecimal(i).ToString() );
							}
							else if( fldtypename.Equals("float")||
								fldtypename.Equals("double") )
							{
								wr.Write( dr.GetDouble(i).ToString() );
							}
							else if( fldtypename.Equals("real"))
							{
								wr.Write( dr.GetValue(i).ToString() );
							}
							else if( fldtypename.Equals("datetime") ||
								fldtypename.Equals("smalldatetime"))
							{
								wr.Write( dr.GetDateTime(i).ToString() );
							}
								//							else if( fldtypename.Equals("money") )
								//							{
								//								wr.Write( dr.GetDouble(i).ToString() );
								//							}
							else if( fldtypename == "varchar" ||
								fldtypename == "char" )
							{
								// ������
								if( dr.GetString(i).Equals("") || dr.GetString(i).Equals("\0"))
								{
									wr.Write( "''" );
								}
								else
								{
									if( dr.GetString(i).IndexOf("'") >= 0 )
									{
										// ' ��������ɓ����Ă���ꍇ�� '' �ɋ����I�ɕϊ�����
										wr.Write( "'{0}'", dr.GetString(i).Replace("'","''").Replace("\0",""));
									}
									else
									{
										wr.Write( "'{0}'", dr.GetString(i).Replace("\0","") );
									}
								}
							}
							else if( fldtypename == "nvarchar" ||
								fldtypename == "nchar" ||
								fldtypename == "ntext")
							{
								// ������
								if( dr.GetString(i).Equals("") || dr.GetString(i).Equals("\0"))
								{
									wr.Write( "N''" );
								}
								else
								{
									if( dr.GetString(i).IndexOf("'") >= 0 )
									{
										// ' ��������ɓ����Ă���ꍇ�� '' �ɋ����I�ɕϊ�����
										wr.Write( "N'{0}'", dr.GetString(i).Replace("'","''").Replace("\0",""));
									}
									else
									{
										wr.Write( "N'{0}'", dr.GetString(i).Replace("\0","") );
									}
								}
							}
							else
							{
								wr.Write( dr.GetValue(i).ToString().Replace("\0","") );
							}
						}
						wr.Write( " ) {0}",wr.NewLine );
					}
					if( trow > 0 )
					{
						wr.Write("GO{0}{0}",wr.NewLine );
					}
					if( this.rdoOutFolder.Checked == true ) 
					{
						wr.Close();
						if( trow > 0 )
						{
							fname.Append(this.txtOutput.Text + "\\" + tbname + ".sql\r\n");
							// �t�@�C�����쐬�������A��ɂȂ����̂ō폜����	
							File.Copy(this.txtOutput.Text + "\\" + tbname + ".sql.tmp", 
								this.txtOutput.Text + "\\" + tbname + ".sql", 
								true );
						}
						File.Delete(this.txtOutput.Text + "\\" + tbname + ".sql.tmp");
					}
					if( dr != null && dr.IsClosed == false )
					{
						dr.Close();
					}
				}
				if( dr != null && dr.IsClosed == false )
				{
					dr.Close();
				}


				// set datas to clipboard
				if( rowcount == 0 )
				{
					MessageBox.Show("�Ώۃf�[�^������܂���ł���");
				}
				else
				{
					if( this.rdoOutFolder.Checked == false ) 
					{
						wr.Close();
					}
					if( this.rdoClipboard.Checked == true ) 
					{
						Clipboard.SetDataObject(strline.ToString(),true );
					}
					else
					{
						Clipboard.SetDataObject(fname.ToString(),true );
					}
					MessageBox.Show("�������������܂���");
				}
			}
			catch( Exception exp )
			{
				this.SetErrorMessage(exp);
			}
		}

		private void crefldlst(bool isLF, bool iscomma)
		{
			try
			{
				this.InitErrMessage();

				// insert ���̍쐬
				if( this.tableList.SelectedItems.Count == 0 )
				{
					return;
				}

				if( CheckFileSpec() == false )
				{
					return;
				}

				StringBuilder strline =  new StringBuilder();
				TextWriter	wr = new StringWriter(strline);
				StringBuilder fname = new StringBuilder();

				if( this.rdoClipboard.Checked == true) 
				{
					wr = new StringWriter(strline);
				}
				else if( this.rdoOutFile.Checked == true ) 
				{
					StreamWriter sw = new StreamWriter(this.txtOutput.Text,false, GetEncode());
					sw.AutoFlush = false;
					wr = sw;
					fname.Append(this.txtOutput.Text);
				}

				foreach( String tbname in this.tableList.SelectedItems )
				{
					if( this.rdoOutFolder.Checked == true ) 
					{
						StreamWriter sw = new StreamWriter(this.txtOutput.Text + "\\" + tbname + ".sql",false, GetEncode());
						sw.AutoFlush = false;
						wr = sw;
						fname.Append(this.txtOutput.Text + "\\" + tbname + ".sql\r\n");
					}

					// get id 
					SqlDataAdapter da = new SqlDataAdapter(string.Format("select  * from {0} where 0=1",gettbname(tbname)), this.sqlConnection1);

					DataSet ds = new DataSet();
					ds.CaseSensitive = true;
					da.Fill(ds,tbname);
	
					wr.Write(tbname);
					wr.Write(":");
					int		maxcol = ds.Tables[tbname].Columns.Count;
					for( int i = 0; i < maxcol ; i++ )
					{
						if( i != 0 && iscomma )
						{
							wr.Write(",");
						}
						if( isLF )
						{
							wr.Write("{0}\t",wr.NewLine);
						}
						wr.Write(ds.Tables[tbname].Columns[i].ColumnName);
					}
					wr.Write(wr.NewLine);

					if( this.rdoOutFolder.Checked == true ) 
					{
						wr.Close();
					}
				}

				if( this.rdoOutFolder.Checked == false ) 
				{
					wr.Close();
				}
				if( this.rdoClipboard.Checked == true ) 
				{
					Clipboard.SetDataObject(strline.ToString(),true );
				}
				else
				{
					Clipboard.SetDataObject(fname.ToString(),true );
				}

				MessageBox.Show( "�������I�����܂���" );
			}
			catch( Exception exp )
			{
				this.SetErrorMessage(exp);
			}
		}

		// �t�B�[���h�̃��X�g��\������
		private void dspfldlist(string tbname)
		{
			try
			{
				this.fieldListbox.Items.Clear();
				if( tbname == "" )
				{
					return;
				}

				bool	dodsp;
				if( this.chkDspFieldAttr.Checked == true )
				{
					dodsp = true;
				}
				else
				{
					dodsp = false;
				}

				string delimStr = ".";
				string []str = tbname.Split(delimStr.ToCharArray(), 2);
				string sqlstr;
				// split owner.table -> owner, table

				sqlstr = "select syscolumns.name colname, systypes.name valtype, syscolumns.length, syscolumns.prec, syscolumns.xscale, syscolumns.colid, syscolumns.colorder, syscolumns.isnullable, syscolumns.collation, sysobjects.id  from sysobjects, syscolumns, sysusers, systypes where sysobjects.id = syscolumns.id and sysobjects.uid= sysusers.uid and syscolumns.xusertype=systypes.xusertype and sysusers.name = '" + str[0] +"' and sysobjects.name = '" + str[1] + "' order by syscolumns.colorder";
				SqlDataAdapter da = new SqlDataAdapter(sqlstr, this.sqlConnection1);
				DataSet ds = new DataSet();
				ds.CaseSensitive = true;
				da.Fill(ds,tbname);

				sqlstr = string.Format("select * from sysindexes where id={0} and indid > 0 and indid < 255 and (status & 2048)=2048",
					(int)ds.Tables[tbname].Rows[0]["id"] );
				SqlDataAdapter daa = new SqlDataAdapter(sqlstr, this.sqlConnection1);
				DataSet idx = new DataSet();
				idx.CaseSensitive = true;
				daa.Fill(idx,tbname);

				int indid = -1;
				DataSet idkey = new DataSet();
				idkey.CaseSensitive = true;
				if( dodsp == true && idx.Tables[0].Rows.Count != 0 )
				{
					indid = (short)idx.Tables[0].Rows[0]["indid"];
					sqlstr = string.Format("select * from sysindexkeys where id={0} and indid={1}",
						(int)ds.Tables[tbname].Rows[0]["id"],
						(short)indid );
					SqlDataAdapter dai = new SqlDataAdapter(sqlstr, this.sqlConnection1);
					dai.Fill(idkey,tbname);
				}

				int		maxRow = ds.Tables[tbname].Rows.Count;

				string	valtype;
				string	istr = "";
				for( int i = 0; i < maxRow ; i++ )
				{
					if( dodsp == false )
					{
						istr = (string)ds.Tables[tbname].Rows[i][0] + " ";
					}
					else
					{
						valtype = (string)ds.Tables[tbname].Rows[i][1];
						if( valtype == "varchar" ||
							valtype == "varbinary" ||
							valtype == "nvarchar" ||
							valtype == "char" ||
							valtype == "nchar" ||
							valtype == "binary" )
						{
							istr = string.Format("{0}  {1}({2}) ",
								ds.Tables[tbname].Rows[i][0],
								ds.Tables[tbname].Rows[i][1],
								ds.Tables[tbname].Rows[i][3]);
										 
						}
						else if( valtype == "numeric" ||
							valtype == "decimal" )
						{
							istr = string.Format("{0}  {1}({2},{3}) ",
								ds.Tables[tbname].Rows[i][0],
								ds.Tables[tbname].Rows[i][1],
								ds.Tables[tbname].Rows[i][3],
								ds.Tables[tbname].Rows[i][4]);

						}
						else
						{
							istr = string.Format("{0}  {1} ",
								ds.Tables[tbname].Rows[i][0],
								ds.Tables[tbname].Rows[i][1]);
						}
						if( (int)ds.Tables[tbname].Rows[i]["isnullable"] == 0 )
						{
							istr +=" NOT NULL";
						}
						else
						{
							istr +=" NULL";
						}
						if( idkey.Tables.Count != 0 && idkey.Tables[0].Rows.Count != 0 )
						{
							foreach(DataRow dr in idkey.Tables[0].Rows )
							{
								if( (short)dr["colid"] == (short)ds.Tables[tbname].Rows[i]["colid"] )
								{
									istr +=" PRIMARY KEY";
								}
							}
						}
					}
					this.fieldListbox.Items.Add(istr);
				}
			}
			catch( Exception exp )
			{
				this.SetErrorMessage(exp);
			}
		}


		private void makefldlist(object sender, System.EventArgs e)
		{
			crefldlst(false,true);
		}

		private void makefldListLF(object sender, System.EventArgs e)
		{
			crefldlst(true,true);
		}

		private void makefldListNoComma(object sender, System.EventArgs e)
		{
			crefldlst(true,false);
		}

		private void tableList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if( this.chkDspData.CheckState == CheckState.Checked &&
				this.tableList.SelectedItems.Count == 1 )
			{
				// 1���̂ݑI������Ă���ꍇ

				if( isInCmbEvent == false )
				{
					// �I�����ꂽTable/View ���L������
					if( this.selectedTables.Contains(this.tableList.SelectedItem.ToString()) == false )
					{
						if( this.selectedTables.Count > MaxTableHistory )
						{
							this.selectedTables.RemoveAt(0);
						}
					}
					else
					{
						this.selectedTables.Remove(this.tableList.SelectedItem.ToString());

					}
					this.selectedTables.Add(this.tableList.SelectedItem.ToString());
					this.cmbHistory.DataSource = null;
					this.cmbHistory.DataSource = this.selectedTables;
					int i = this.cmbHistory.FindStringExact(this.tableList.SelectedItem.ToString());
					this.cmbHistory.SelectedIndex = i;
					this.cmbHistory.Refresh();
				}


				this.SetNewHistory(this.tableList.SelectedItem.ToString(),this.txtWhere.Text,ref this.whereHistory);
				this.SetNewHistory(this.tableList.SelectedItem.ToString(),this.txtSort.Text,ref this.sortHistory);
				// �f�[�^�\�����ɁA�Y���e�[�u���̃f�[�^��\������
				DspData(this.tableList.SelectedItem.ToString());
			}
			else
			{
				this.SetNewHistory("",this.txtWhere.Text,ref this.whereHistory);
				this.SetNewHistory("",this.txtSort.Text,ref this.sortHistory);
				DspData("");
			}
			if( this.tableList.SelectedItems.Count == 1 )
			{
				dspfldlist(this.tableList.SelectedItem.ToString());
			}
			else
			{
				dspfldlist("");
			}
			if( indexdlg != null && indexdlg.Visible == true )
			{
				if( this.tableList.SelectedItems.Count == 1 )
				{
					indexdlg.settabledsp(this.tableList.SelectedItem.ToString());
				}
				else
				{
					indexdlg.settabledsp("");
				}
				indexdlg.Show();
			}
		}


		private void makeCSV(object sender, System.EventArgs e)
		{
			crecsv(false, ",");
		}

		private void crecsv(bool isdquote, string separater)
		{
			SqlDataReader dr = null;
			SqlCommand	cm = new SqlCommand();
			cm.CommandTimeout = this.SqlTimeOut;

			if( this.tableList.SelectedItems.Count == 0 )
			{
				return;
			}
			if( this.tableList.SelectedItems.Count > 1 &&
				this.txtWhere.Text != null &&
				this.txtWhere.Text.Trim() != "" )
			{
				if( MessageBox.Show("�����e�[�u���ɓ���� where ���K�p���܂����H","�m�F",System.Windows.Forms.MessageBoxButtons.YesNo) 
					== System.Windows.Forms.DialogResult.No )
				{
					return;
				}
			}
			if( this.tableList.SelectedItems.Count > 1 &&
				this.txtSort.Text != null &&
				this.txtSort.Text.Trim() != "" )
			{
				if( MessageBox.Show("�����e�[�u���ɓ���� order by ���K�p���܂����H","�m�F",System.Windows.Forms.MessageBoxButtons.YesNo) 
					== System.Windows.Forms.DialogResult.No )
				{
					return;
				}
			}

			if( CheckFileSpec() == false )
			{
				return;
			}

			this.InitErrMessage();

			int			rowcount = 0;
			int			trow = 0;
			try
			{

				StringBuilder strline =  new StringBuilder();
				TextWriter	wr = new StringWriter(strline);
				StringBuilder fname = new StringBuilder();

				if( this.rdoClipboard.Checked == true) 
				{
					wr = new StringWriter(strline);
				}
				else if( this.rdoOutFile.Checked == true ) 
				{
					StreamWriter sw = new StreamWriter(this.txtOutput.Text,false, GetEncode());
					sw.AutoFlush = false;
					wr = sw;
					fname.Append(this.txtOutput.Text);
				}

				foreach( String tbname in this.tableList.SelectedItems )
				{

					if( this.rdoOutFolder.Checked == true ) 
					{
						StreamWriter sw = new StreamWriter(this.txtOutput.Text + "\\" + tbname + ".csv.tmp",false, GetEncode());
						sw.AutoFlush = false;
						wr = sw;
					}
					trow = 0;
					// get id 
					string sqlstr;
					sqlstr = string.Format("select  * from {0} ",gettbname(tbname));
					//sqlstr = "select * from [" + tbname + "]";
					if( this.txtWhere.Text.Trim() != "" )
					{
						sqlstr += " where " + this.txtWhere.Text.Trim();
					}
					if( this.txtSort.Text.Trim() != "" )
					{
						sqlstr += " order by " + this.txtSort.Text.Trim();
					}
					cm.CommandText = sqlstr;
					cm.Connection = this.sqlConnection1;

					dr = cm.ExecuteReader();

					ArrayList fldname = new ArrayList();
					ArrayList strint = new ArrayList();
					int			maxcol;
	
					fldname.Clear();
					strint.Clear();

					maxcol = dr.FieldCount;
					for( int j=0 ; j < maxcol; j++ )
					{
						fldname.Add( dr.GetName(j) );
						strint.Add( dr.GetFieldType(j) );
					}

					// �擪�s�͗񌩏o��
					for( int i = 0 ; i < maxcol; i++ )
					{
						if( i != 0 )
						{
							wr.Write(separater);
						}
						wr.Write( fldname[i] );
					}
					wr.Write( wr.NewLine );
					string fldtypename;

					// �f�[�^�̏����o��
					while (dr.Read())
					{
						rowcount++;
						trow++;
						for( int i = 0 ; i < maxcol; i++ )
						{
							if( i != 0 )
							{
								wr.Write( separater );
							}
							fldtypename = dr.GetDataTypeName(i);
							if( dr.IsDBNull(i) )
							{
								wr.Write( "" );
							}
							else if( fldtypename.Equals("int") )
							{
								wr.Write( dr.GetInt32(i).ToString() );
							}
							else if( fldtypename.Equals("bigint") )
							{
								wr.Write( dr.GetInt64(i).ToString() );
							}
							else if( fldtypename.Equals("image") ||
								fldtypename.Equals("varbinary") ||
								fldtypename.Equals("binary"))
							{
								wr.Write("\"{0}\"", dr.GetSqlBinary(i).ToString() );
							}
							else if( fldtypename.Equals("decimal") 
								|| fldtypename.Equals("numeric"))
							{
								wr.Write( dr.GetDecimal(i).ToString() );
							}
							else if( fldtypename.Equals("float")||
								fldtypename.Equals("double") )
							{
								wr.Write( dr.GetDouble(i).ToString() );
							}
							else if( fldtypename.Equals("real"))
							{
								wr.Write( dr.GetValue(i).ToString() );
							}
							else if( fldtypename.Equals("datetime") ||
								fldtypename.Equals("smalldatetime"))
							{
								wr.Write( dr.GetDateTime(i).ToString() );
							}
								//							else if( fldtypename.Equals("money") )
								//							{
								//								wr.Write( dr.GetDouble(i).ToString() );
								//							}
							else if( fldtypename == "nvarchar" ||
								fldtypename == "nchar" ||
								fldtypename == "ntext" ||
								fldtypename == "varchar" ||
								fldtypename == "char" )
							{
								// ������
								if( dr.GetString(i).Equals("\0") )
								{
									if( isdquote )
									{
										wr.Write( "\"\"" );
									}
									else
									{
										wr.Write( "" );
									}
								}
								else if( !dr.GetString(i).Equals("") )
								{
									if( isdquote )
									{
										wr.Write( "\"{0}\"", dr.GetString(i).Replace("\0","") );
									}
									else
									{
										wr.Write( dr.GetString(i).Replace("\0","") );
									}
								}
							}
							else
							{
								wr.Write( dr.GetValue(i).ToString().Replace("\0","") );
							}
						}
						wr.Write( wr.NewLine );
					}
					if( dr != null && dr.IsClosed == false )
					{
						dr.Close();
					}
					if( this.rdoOutFolder.Checked == true ) 
					{
						wr.Close();
						if( trow > 0 )
						{
							fname.Append(this.txtOutput.Text + "\\" + tbname + ".csv\r\n");
							// �t�@�C�����쐬�������A��ɂȂ����̂ō폜����	
							File.Copy(this.txtOutput.Text + "\\" + tbname + ".csv.tmp", 
								this.txtOutput.Text + "\\" + tbname + ".csv", 
								true );
						}
						File.Delete(this.txtOutput.Text + "\\" + tbname + ".csv.tmp");
					}
				}
				if( this.rdoOutFolder.Checked == false ) 
				{
					wr.Close();
				}
				if( rowcount == 0 )
				{
					MessageBox.Show("�Ώۃf�[�^������܂���ł���");
				}
				else
				{
					if( this.rdoClipboard.Checked == true ) 
					{
						Clipboard.SetDataObject(strline.ToString(),true );
					}
					else
					{
						Clipboard.SetDataObject(fname.ToString(),true );
					}
					MessageBox.Show("�������������܂���");
				}
			}
			catch ( System.Data.SqlClient.SqlException se )
			{
				if( dr != null && dr.IsClosed == false )
				{
					dr.Close();
				}
				this.SetErrorMessage(se);
				return;
			}
			catch( Exception se )
			{
				if( dr != null && dr.IsClosed == false )
				{
					dr.Close();
				}
				this.SetErrorMessage(se);
			}
			finally 
			{
				if( cm != null )
				{
					cm.Dispose();
				}
			}

			// set datas to clipboard
		}

		private void makeCSVQuote(object sender, System.EventArgs e)
		{
			crecsv(true,",");
		}

		private void rdoDspView_CheckedChanged(object sender, System.EventArgs e)
		{
			// �e�[�u���̑I�𗚗����N���A
			this.selectedTables.Clear();
			this.cmbHistory.DataSource = null;
			this.cmbHistory.DataSource = this.selectedTables;
			this.cmbHistory.Refresh();
			

			dsplist2();
		}

		private void rdoSortTable_CheckedChanged(object sender, System.EventArgs e)
		{
			dsplist2();
		}

		private void insertmakeDelete(object sender, System.EventArgs e)
		{
			this.CreInsert( true, true,false );
		}

		private void insertmakeNoField(object sender, System.EventArgs e)
		{
			this.CreInsert(false,false,false );
		}

		private void insertmakeNoFieldDelete(object sender, System.EventArgs e)
		{
			this.CreInsert(false,true,false);
		}

		private void btnSelect_Click(object sender, System.EventArgs e)
		{
			// select ���̍쐬

			this.InitErrMessage();

			try
			{
				if( this.tableList.SelectedItems.Count == 0 )
				{
					return;
				}
				if( CheckFileSpec() == false )
				{
					return;
				}

				StringBuilder strline =  new StringBuilder();
				TextWriter	wr = new StringWriter(strline);
				StringBuilder fname = new StringBuilder();

				if( this.rdoClipboard.Checked == true) 
				{
					wr = new StringWriter(strline);
				}
				else if( this.rdoOutFile.Checked == true ) 
				{
					StreamWriter sw = new StreamWriter(this.txtOutput.Text,false, GetEncode());
					sw.AutoFlush = false;
					wr = sw;
					fname.Append(this.txtOutput.Text);
				}

				foreach( String tbname in this.tableList.SelectedItems )
				{

					if( this.rdoOutFolder.Checked == true ) 
					{
						StreamWriter sw = new StreamWriter(this.txtOutput.Text + "\\" + tbname + ".sql",false, GetEncode());
						sw.AutoFlush = false;
						wr = sw;
						fname.Append(this.txtOutput.Text + "\\" + tbname + ".sql\r\n");
					}
					// get id 
					SqlDataAdapter da = new SqlDataAdapter(string.Format("select  * from {0} where 0=1",gettbname(tbname)), this.sqlConnection1);

					DataSet ds = new DataSet();
					ds.CaseSensitive = true;
					da.Fill(ds,tbname);
	
					wr.Write("select {0}",wr.NewLine);
					int		maxcol = ds.Tables[tbname].Columns.Count;
					for( int i = 0; i < maxcol ; i++ )
					{
						if( i != 0 )
						{
							wr.Write(",{0}", wr.NewLine);
						}
						wr.Write("\t{0}", ds.Tables[tbname].Columns[i].ColumnName);
					
					}
					wr.Write(wr.NewLine);
					wr.Write(" from {0}{1}", gettbname(tbname),wr.NewLine);
					if( this.txtWhere.Text.Trim() != "" )
					{
						wr.Write(" where {0}{1}", this.txtWhere.Text.Trim(),wr.NewLine);
					}
					if( this.txtSort.Text.Trim() != "" )
					{
						wr.Write(" order by {0}{1}", this.txtSort.Text.Trim(),wr.NewLine);
					}
					if( this.rdoOutFolder.Checked == true ) 
					{
						wr.Close();
					}
				}
				if( this.rdoOutFolder.Checked == false ) 
				{
					wr.Close();
				}

				if( this.rdoClipboard.Checked == true ) 
				{
					Clipboard.SetDataObject(strline.ToString(),true );
				}
				else
				{
					Clipboard.SetDataObject(fname.ToString(),true );
				}

				MessageBox.Show( "�������I�����܂���" );
			}
			catch( Exception exp )
			{
				this.SetErrorMessage(exp);
			}
		}

		private void ownerListbox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if( this.ownerListbox.IsAllSelecting == true )
			{
				return;
			}
			if( this.ownerListbox.SelectedItem != null )
			{
				// �I������DB�̍ŏI�I�[�i�[���L�^����
				svdata.dbopt[svdata.lastdb] = (string)this.ownerListbox.SelectedItem;
			}
			// �e�[�u���̑I�𗚗����N���A
			this.selectedTables.Clear();
			this.cmbHistory.DataSource = null;
			this.cmbHistory.DataSource = this.selectedTables;
			this.cmbHistory.Refresh();
			

			dsplist2();
		}

		private void dsplist2()
		{
			SqlDataReader dr = null;
			SqlCommand	cm = new SqlCommand();
			cm.CommandTimeout = this.SqlTimeOut;

			this.InitErrMessage();

			try 
			{
				if( this.dbList.SelectedItem == null )
				{
					return ;
				}
				this.sqlConnection1.ChangeDatabase((String)this.dbList.SelectedItem);
				
				// listbox2 �Ƀe�[�u���ꗗ��\��

				string sortkey;
				if( this.rdoSortTable.Checked == true )
				{
					sortkey = " order by 1 ";
				}
				else
				{
					sortkey = " order by 2,1 ";
				}

				if( this.rdoDspView.Checked == true )
				{
					cm.CommandText = "select sysobjects.name as tbname, sysusers.name as uname from sysobjects, sysusers where ( xtype='U' or xtype='V' ) and sysobjects.uid = sysusers.uid ";
				}
				else
				{
					cm.CommandText = "select sysobjects.name as tbname, sysusers.name as uname from sysobjects, sysusers where xtype='U' and sysobjects.uid = sysusers.uid ";
				}

				if( this.ownerListbox.SelectedItem != null )
				{
					bool	allsele = false;
					// �I��������΁A����OWNER�݂̂̃e�[�u����\������
					string ownerlist = "";
					foreach( String owname in this.ownerListbox.SelectedItems )
					{
						if( owname == "�S��" )
						{
							allsele = true;
							break;
						}
						if( ownerlist != "" )
						{
							ownerlist += ",";
						}
						ownerlist += "'" + owname + "'";
					}
					if( allsele == false )
					{
						cm.CommandText += " and sysusers.name in ( " + ownerlist + " ) ";
					}
				}
				cm.CommandText += sortkey;
				cm.Connection = this.sqlConnection1;

				dr = cm.ExecuteReader();

				this.tableList.Items.Clear();
				while ( dr.Read())
				{
					this.tableList.Items.Add(dr["uname"] + "." + dr["tbname"]);
				}
				dr.Close();
			}
			catch ( System.Data.SqlClient.SqlException se )
			{
				if( dr != null )
				{
					dr.Close();
				}
				this.SetErrorMessage(se);
			}
			catch ( Exception se )
			{
				if( dr != null )
				{
					dr.Close();
				}
				this.SetErrorMessage(se);
			}
			finally 
			{
				cm.Dispose();
			}
		
		}

		private void makeDDL(object sender, System.EventArgs e)
		{
			this.CreDDL(false, false);
		}

		private void makeDDLDrop(object sender, System.EventArgs e)
		{
			this.CreDDL(true, false);
		}
		
		private void makeDDLPare(object sender, System.EventArgs e)
		{
			this.CreDDL(false, true);
		
		}

		private void makeDDLDropPare(object sender, System.EventArgs e)
		{
			this.CreDDL(true, true);
		}

		private void	CreDDL(bool bDrop, bool usekakko)
		{	
			SqlDataReader dr = null;
			SqlCommand	cm = new SqlCommand();
			cm.CommandTimeout = this.SqlTimeOut;

			if( this.tableList.SelectedItems.Count == 0 )
			{
				return;
			}
			if( CheckFileSpec() == false )
			{
				return;
			}
			
			this.InitErrMessage();

			try
			{
				StringBuilder strline =  new StringBuilder();
				TextWriter	wr = new StringWriter(strline);
				StringBuilder fname = new StringBuilder();

				if( this.rdoClipboard.Checked == true) 
				{
					wr = new StringWriter(strline);
				}
				else if( this.rdoOutFile.Checked == true ) 
				{
					StreamWriter sw = new StreamWriter(this.txtOutput.Text,false, GetEncode());
					sw.AutoFlush = false;
					wr = sw;
					fname.Append(this.txtOutput.Text);
				}

				foreach( String tbname in this.tableList.SelectedItems )
				{
					if( this.rdoOutFolder.Checked == true ) 
					{
						StreamWriter sw = new StreamWriter(this.txtOutput.Text + "\\" + tbname + ".sql",false, GetEncode());
						sw.AutoFlush = false;
						wr = sw;
						fname.Append(this.txtOutput.Text + "\\" + tbname + ".sql\r\n");
					}

					if( bDrop )
					{
						wr.Write( "DROP TABLE " );
						wr.Write("{0}{1}", gettbname(tbname),wr.NewLine);
						wr.Write( "GO{0}",wr.NewLine);
					}
					// get id 
					string sqlstr;
					// split owner.table -> owner, table

					string delimStr = ".";
					string []str = tbname.Split(delimStr.ToCharArray(), 2);
					sqlstr = "select syscolumns.name colname, systypes.name valtype, syscolumns.length, syscolumns.prec, syscolumns.xscale, syscolumns.colid, syscolumns.colorder, syscolumns.isnullable, syscolumns.collation  from sysobjects, syscolumns, sysusers, systypes where sysobjects.id = syscolumns.id and sysobjects.uid= sysusers.uid and syscolumns.xusertype=systypes.xusertype and sysusers.name = '" + str[0] +"' and sysobjects.name = '" + str[1] + "' order by syscolumns.colorder";
					SqlDataAdapter da = new SqlDataAdapter(sqlstr, this.sqlConnection1);
					DataSet ds = new DataSet();
					ds.CaseSensitive = true;
					da.Fill(ds,tbname);

					int		maxRow = ds.Tables[tbname].Rows.Count;
					if( usekakko )
					{
						wr.Write("Create table [{0}] ", str[0]);
						wr.Write(".[{0}]",str[1]);
					}
					else
					{
						wr.Write("Create table {0} ", tbname);
					}
					wr.Write(" ( {0}",wr.NewLine);
					string	valtype;
					for( int i = 0; i < maxRow ; i++ )
					{
						if( i != 0 )
						{
							wr.Write(",{0}",wr.NewLine);
						}
						//�t�B�[���h��
						if( usekakko )
						{
							wr.Write("\t[{0}]", ds.Tables[tbname].Rows[i][0]);
						}
						else
						{
							wr.Write("\t{0}", ds.Tables[tbname].Rows[i][0]);
						}
						wr.Write("\t");
						// �^
						valtype = (string)ds.Tables[tbname].Rows[i][1];

						wr.Write("\t");

						if( usekakko )
						{
							wr.Write("[{0}]",valtype);
						}
						else
						{
							wr.Write(valtype);
						}
						if( valtype == "varchar" ||
							valtype == "varbinary" ||
							valtype == "nvarchar" ||
							valtype == "char" ||
							valtype == "nchar" ||
							valtype == "binary" )
						{
							wr.Write(" ({0})", ds.Tables[tbname].Rows[i][3]);
						}
						else if( valtype == "numeric" ||
							valtype == "decimal" )
						{
							wr.Write(" ({0},", ds.Tables[tbname].Rows[i][3]);
							wr.Write("{0})", ds.Tables[tbname].Rows[i][4]);
						}
						wr.Write("\t");
						
						if( !ds.Tables[tbname].Rows[i].IsNull("collation"))
						{
							wr.Write("COLLATE {0}",ds.Tables[tbname].Rows[i]["collation"]);
							wr.Write("\t");
						}
						
						if( (int)ds.Tables[tbname].Rows[i]["isnullable"] == 0 )
						{
							wr.Write("\tNOT NULL");
						}
						else
						{
							wr.Write("\tNULL");
						}
					}
					wr.Write("{0}){0}Go{0}",wr.NewLine);
					if( this.rdoOutFolder.Checked == true ) 
					{
						wr.Close();
					}
				}
				if( this.rdoOutFolder.Checked == false ) 
				{
					wr.Close();
				}
				if( this.rdoClipboard.Checked == true ) 
				{
					Clipboard.SetDataObject(strline.ToString(),true );
				}
				else
				{
					Clipboard.SetDataObject(fname.ToString(),true );
				}
				MessageBox.Show("�������������܂���");
			}
			catch ( System.Data.SqlClient.SqlException se )
			{
				if( dr != null && dr.IsClosed == false )
				{
					dr.Close();
				}
				this.SetErrorMessage(se);
			}
			catch ( Exception se )
			{
				if( dr != null && dr.IsClosed == false )
				{
					dr.Close();
				}
				this.SetErrorMessage(se);
			}
			finally 
			{
				if( cm != null )
				{
					cm.Dispose();
				}
			}
		}

		private void chkDspData_CheckedChanged(object sender, System.EventArgs e)
		{
			if( this.chkDspData.CheckState == CheckState.Checked &&
				this.tableList.SelectedItems.Count == 1 )
			{
				// 1���̂ݑI������Ă���ꍇ�A�f�[�^�\�����ɁA�Y���e�[�u���̃f�[�^��\������
				DspData(this.tableList.SelectedItem.ToString());
			}
			else
			{
				DspData("");
			}
			if( this.chkDspData.CheckState == CheckState.Checked )
			{
				svdata.showgrid[svdata.lastdb] = 1;
			}
			else
			{
				svdata.showgrid[svdata.lastdb] = 0;
			}
		}

		protected void DspData(string tbname)
		{
			DspData(tbname,false);
		}
		
		protected void DspData(string tbname, bool isAllDsp)
		{
			try
			{
				this.InitErrMessage();

				if( tbname == "" )
				{
					this.dbGrid.Hide();
					this.btnDataUpdate.Enabled = false;
					this.btnDataEdit.Enabled = false;
					this.btnGridFormat.Enabled = false;
					return;
				}

				ProcCondition procCond = GetProcCondition(tbname);
				procCond.isAllDisp = isAllDsp;

				int	maxlines;
				int	maxGetLines;
				if( procCond.MaxStr != "" )
				{
					maxlines = int.Parse(procCond.MaxStr);
				}
				else
				{
					maxlines = 0;
				}
				if( procCond.isAllDisp == true )
				{
					maxlines = 0;
				}

				// �f�[�^�̓��e���擾���A�\������
				string sqlstr;
				string sqlstrDisp;
				sqlstr = "select ";
				sqlstrDisp = "select ";

				maxGetLines = 0;
				if( maxlines != 0 )
				{
					maxGetLines = maxlines + 1;
					sqlstr += " TOP " + maxGetLines.ToString();
					sqlstrDisp += " TOP " + maxlines.ToString();
				}

				sqlstr += string.Format(" * from {0}",gettbname(tbname));
				sqlstrDisp += string.Format(" * from {0}",gettbname(tbname));
				if( procCond.WhereStr.Trim() != "" )
				{
					sqlstr += " where " + procCond.WhereStr.Trim();
					sqlstrDisp += " where " + procCond.WhereStr.Trim();
				}
				if( procCond.OrderStr.Trim() != "" )
				{
					sqlstr += " order by " + procCond.OrderStr.Trim();
					sqlstrDisp += " order by " + procCond.OrderStr.Trim();
				}
				SqlDataAdapter da = new SqlDataAdapter(sqlstr, this.sqlConnection1);
				dspdt = new DataSet();
				dspdt.CaseSensitive = true;
				da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
				da.Fill(dspdt, "aaaa");

				if( maxlines != 0 )
				{
					if( dspdt.Tables["aaaa"].Rows.Count == maxGetLines )
					{
						this.btnTmpAllDsp.Enabled = true;
						dspdt.Tables["aaaa"].Rows[maxGetLines-1].Delete();
						dspdt.Tables["aaaa"].AcceptChanges();
					}
					else
					{
						// �擾�����f�[�^���\�������ɖ����Ȃ��ׁA�ꎞ�I�ɑS�f�[�^��\��(&A) �{�^���͖����ł悢
						this.btnTmpAllDsp.Enabled = false;
					}
				}
				else
				{
					// �S���\������ׁA�ꎞ�I�ɑS�f�[�^��\��(&A) �{�^���͖����ł悢
					this.btnTmpAllDsp.Enabled = false;
				}

				//�V����DataGridTableStyle�̍쐬
				DataGridTableStyle ts = new DataGridTableStyle();
				//�}�b�v�����w�肷��
				ts.MappingName = "aaaa";

				MyDataGridTextBoxColumn cs;
				foreach( DataColumn col in dspdt.Tables[0].Columns )
				{
					//��X�^�C����MyDataGridTextBoxColumn���g��
					if( col.DataType.FullName == "System.String" )
					{
						cs = new MyDataGridTextBoxColumn(true);
					}
					else
					{
						cs = new MyDataGridTextBoxColumn(false);
						if( col.DataType.FullName == "System.Int32" ||
							col.DataType.FullName == "System.Int16" ||
							col.DataType.FullName == "System.Int64" ||
							col.DataType.FullName == "System.UInt32" ||
							col.DataType.FullName == "System.UInt16" ||
							col.DataType.FullName == "System.UInt64" ||
							col.DataType.FullName == "System.Decimal" )
						{
							cs.Format = getFormat(this.NumFormat);
						}
						if( col.DataType.FullName == "System.Double" ||
							col.DataType.FullName == "System.Single" )
						{
							cs.Format = getFormat(this.FloatFormat);
						}
						if( col.DataType.FullName == "System.DateTime" )
						{
							cs.Format = getFormat(this.DateFormat);
						}
					}
					//�}�b�v�����w�肷��
					cs.MappingName = col.ColumnName;
					if( col.AllowDBNull == true )
					{
						cs.HeaderText = "��"+col.ColumnName;
					}
					else
					{
						cs.HeaderText = col.ColumnName;
					}
					
					//DataGridTableStyle�ɒǉ�����
					ts.GridColumnStyles.Add(cs);
				}

				//�e�[�u���X�^�C����DataGrid�ɒǉ�����
				this.dbGrid.TableStyles.Clear();
				this.dbGrid.TableStyles.Add(ts);



				this.dbGrid.AllowSorting = true;
				this.toolTip3.SetToolTip(this.dbGrid,sqlstrDisp);
				this.dbGrid.SetDataBinding(dspdt, "aaaa");
				this.dbGrid.Show();
				this.btnDataEdit.Text = "�f�[�^�ҏW(&T)";
				this.dbGrid.ReadOnly = true;
				this.btnDataEdit.BackColor = this.btnBackColor;
				this.btnDataEdit.ForeColor = this.btnForeColor;
				this.btnTmpAllDsp.BackColor = this.btnBackColor;
				this.btnTmpAllDsp.ForeColor = this.btnForeColor;
				this.btnDataUpdate.Enabled = true;
				this.btnDataEdit.Enabled = true;
				this.btnGridFormat.Enabled = true;
			}
			catch( Exception exp)
			{
				this.SetErrorMessage(exp);
			}
		}

		private void txtDspCount_Leave(object sender, System.EventArgs e)
		{
			if( this.chkDspData.CheckState == CheckState.Checked &&
				this.tableList.SelectedItems.Count == 1 )
			{
				// 1���̂ݑI������Ă���ꍇ�A�f�[�^�\�����ɁA�Y���e�[�u���̃f�[�^��\������
				DspData(this.tableList.SelectedItem.ToString());
			}
			else
			{
				DspData("");
			}
		}

		private void txtWhere_Leave(object sender, System.EventArgs e)
		{
			string tbname = "";
			if( this.chkDspData.CheckState == CheckState.Checked &&
				this.tableList.SelectedItems.Count == 1 )
			{
				// 1���̂ݑI������Ă���ꍇ�A�f�[�^�\�����ɁA�Y���e�[�u���̃f�[�^��\������
				tbname = this.tableList.SelectedItem.ToString();
			}
			else
			{
				tbname = "";
			}
			DspData(tbname);

			// �����Ɍ��݂̒l���L�^ TODO
			SetNewHistory(tbname,this.txtWhere.Text,ref this.whereHistory);

		}

		private void txtSort_Leave(object sender, System.EventArgs e)
		{
			string tbname = "";
			if( this.chkDspData.CheckState == CheckState.Checked &&
				this.tableList.SelectedItems.Count == 1 )
			{
				// 1���̂ݑI������Ă���ꍇ�A�f�[�^�\�����ɁA�Y���e�[�u���̃f�[�^��\������
				tbname = this.tableList.SelectedItem.ToString();
			}
			else
			{
				tbname = "";
			}
			DspData(tbname);

			// �����Ɍ��݂̒l���L�^ TODO
			SetNewHistory(tbname,this.txtSort.Text,ref this.sortHistory);
		}

		private void rdoDspSysUser_CheckedChanged(object sender, System.EventArgs e)
		{
			// �e�[�u���̑I�𗚗����N���A
			this.selectedTables.Clear();
			this.cmbHistory.DataSource = null;
			this.cmbHistory.DataSource = this.selectedTables;
			this.cmbHistory.Refresh();
			

			dsplist2();
			displistowner();
		}

		private void displistowner()
		{
			SqlDataReader dr = null;
			SqlCommand	cm = new SqlCommand();
			cm.CommandTimeout = this.SqlTimeOut;

			this.InitErrMessage();

			try 
			{
				if( rdoDspSysUser.Checked )
				{
					cm.CommandText = "select * from sysusers order by name";
				}
				else
				{
					cm.CommandText = "select * from sysusers where name not like 'db_%' order by name";
				}
				cm.Connection = this.sqlConnection1;

				dr = cm.ExecuteReader();

				this.ownerListbox.Items.Clear();
				this.ownerListbox.Items.Add("�S��");
				while ( dr.Read())
				{
					this.ownerListbox.Items.Add(dr["name"]);
				}
				dr.Close();
			
			}
			catch ( Exception se )
			{
				if( dr != null )
				{
					dr.Close();
				}
				this.SetErrorMessage(se);
			}
			finally 
			{
				cm.Dispose();
			}
		}

		private void rdoNotDspSysUser_CheckedChanged(object sender, System.EventArgs e)
		{
			// �e�[�u���̑I�𗚗����N���A
			this.selectedTables.Clear();
			this.cmbHistory.DataSource = null;
			this.cmbHistory.DataSource = this.selectedTables;
			this.cmbHistory.Refresh();
			

			dsplist2();
			displistowner();
		}

		private void menuTableCopy_Click(object sender, System.EventArgs e)
		{
			copytablename(false);
		}

		private void menuTableCopyCsv_Click(object sender, System.EventArgs e)
		{
			copytablename(true);
		}
		private void copytablename(bool addcomma)
		{
			if( this.tableList.SelectedItems.Count > 0 )
			{
				StringBuilder strline =  new StringBuilder();
				foreach( string name in tableList.SelectedItems )
				{
					if( strline.Length != 0 )
					{
						if( addcomma )
						{
							strline.Append(",");
						}
						strline.Append("\r\n");
					}
					strline.Append(name);
				}
				Clipboard.SetDataObject(strline.ToString(),true );
			}
		}

		private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if( this.rdoNotDspSysUser.Checked == true )
			{
				svdata.isShowsysuser = 0;
			}
			else
			{
				svdata.isShowsysuser = 1;
			}

			if( this.rdoSortOwnerTable.Checked == true )
			{
				svdata.sortKey = 0;
			}
			else
			{
				svdata.sortKey = 1;
			}
			if( this.rdoDspView.Checked == false) 
			{
				svdata.showView = 0;
			}
			else
			{
				svdata.showView = 1;
			}

			if( this.rdoClipboard.Checked == true) 
			{
				svdata.outdest[svdata.lastdb] = 0;
			}
			if( this.rdoOutFile.Checked == true) 
			{
				svdata.outdest[svdata.lastdb] = 1;
			}
			if( this.rdoOutFolder.Checked == true) 
			{
				svdata.outdest[svdata.lastdb] = 2;
			}
			svdata.outfile[svdata.lastdb] = this.txtOutput.Text;
			if( this.chkDspData.CheckState == CheckState.Checked )
			{
				svdata.showgrid[svdata.lastdb] = 1;
			}
			else
			{
				svdata.showgrid[svdata.lastdb] = 0;
			}
			if( this.rdoUnicode.Checked == true )
			{
				svdata.txtencode[svdata.lastdb] = 0;
			}
			if( this.rdoSjis.Checked == true )
			{
				svdata.txtencode[svdata.lastdb] = 1;
			}
			if( this.rdoUtf8.Checked == true )
			{
				svdata.txtencode[svdata.lastdb] = 2;
			}
			svdata.griddspcnt[svdata.lastdb] = this.txtDspCount.Text;

			if( this.sqlConnection1 != null )
			{
				this.sqlConnection1.Close();
				this.sqlConnection1.Dispose();
				this.sqlConnection1 = null;
			}
		}



		protected void copyfldlist(bool lf, bool docomma)
		{
			StringBuilder str = new StringBuilder();
			for( int i=0; i < this.fieldListbox.SelectedItems.Count; i++ )
			{
				if( i != 0 )
				{
					if( lf == true )
					{
						if( docomma ) 
						{
							str.Append(",\r\n");
						}
						else
						{
							str.Append("\r\n");
						}
					}
					else
					{
						if( docomma )
						{
							str.Append(",");
						}
						else
						{
							str.Append("\t");
						}
					}
				}
				str.Append((string)this.fieldListbox.SelectedItems[i]);
			}
			if( str.Length != 0 )
			{
				Clipboard.SetDataObject(str.ToString(),true );
			}
		}

		private void fldmenuCopy_Click(object sender, System.EventArgs e)
		{
			copyfldlist(true,true);
		}

		private void fldmenuCopyNoCRLF_Click(object sender, System.EventArgs e)
		{
			copyfldlist(false,true);
		}

		private void rdoClipboard_CheckedChanged(object sender, System.EventArgs e)
		{
			if( rdoClipboard.Checked == true )
			{
				this.txtOutput.Enabled = false;
				this.btnReference.Enabled = false;
				svdata.outdest[svdata.lastdb] = 0;
				this.rdoUnicode.Enabled = false;
				this.rdoSjis.Enabled = false;
				this.rdoUtf8.Enabled = false;
			}
		}

		private void rdoOutFolder_CheckedChanged(object sender, System.EventArgs e)
		{
			if( this.rdoOutFolder.Checked == true )
			{
				this.txtOutput.Enabled = true;
				this.btnReference.Enabled = true;
				svdata.outdest[svdata.lastdb] = 2;
				this.rdoUnicode.Enabled = true;
				this.rdoSjis.Enabled = true;
				this.rdoUtf8.Enabled = true;
			}
		}

		private void rdoOutFile_CheckedChanged(object sender, System.EventArgs e)
		{
			if( this.rdoOutFile.Checked == true )
			{
				this.txtOutput.Enabled = true;
				this.btnReference.Enabled = true;
				svdata.outdest[svdata.lastdb] = 1;
				this.rdoUnicode.Enabled = true;
				this.rdoSjis.Enabled = true;
				this.rdoUtf8.Enabled = true;
			}
		}

		private void btnReference_Click(object sender, System.EventArgs e)
		{
			if( this.rdoOutFile.Checked == true )
			{
				if( this.txtOutput.Text != "" )
				{
					DirectoryInfo d = new DirectoryInfo(this.txtOutput.Text);
					if( d.Exists )
					{
						this.saveFileDialog1.InitialDirectory = this.txtOutput.Text;
						this.saveFileDialog1.FileName = "";
					}
					else
					{
						this.saveFileDialog1.FileName = this.txtOutput.Text;
					}
				}
				// �P�ƃt�@�C���̎Q�Ǝw��
				
				this.saveFileDialog1.CreatePrompt = true;
				this.saveFileDialog1.Filter = "SQL|*.sql|CSV|*.csv|TXT|*.txt|�S��|*.*";
				DialogResult ret = this.saveFileDialog1.ShowDialog();
				if( ret == DialogResult.OK )
				{
					this.txtOutput.Text = this.saveFileDialog1.FileName;
				}
			}
			else
			{
				// �����t�@�C���̃f�B���N�g���Q�Ǝw��
				if( this.txtOutput.Text != "" )
				{
					FileInfo f = new FileInfo(this.txtOutput.Text);
					if( f.Exists &&
						( f.Attributes & FileAttributes.Directory ) == FileAttributes.Directory)
					{
						this.folderBrowserDialog1.SelectedPath = this.txtOutput.Text;
					}
					else if( f.Exists && (f.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
					{
						this.folderBrowserDialog1.SelectedPath = f.Directory.FullName;
					}
					else if( !f.Exists )
					{
						this.folderBrowserDialog1.SelectedPath = this.txtOutput.Text;
					}
					else
					{
						this.folderBrowserDialog1.SelectedPath = "";
					}
				}
				else
				{
					this.folderBrowserDialog1.SelectedPath = "";
				}
				
				this.folderBrowserDialog1.ShowNewFolderButton = true;
				DialogResult ret = this.folderBrowserDialog1.ShowDialog();
				if( ret == DialogResult.OK )
				{
					this.txtOutput.Text = this.folderBrowserDialog1.SelectedPath;
				}
			}
		}

		private void txtOutput_TextChanged(object sender, System.EventArgs e)
		{
			this.toolTip1.SetToolTip(this.txtOutput,this.txtOutput.Text);
			svdata.outfile[svdata.lastdb] = this.txtOutput.Text;
		}


		private void txtDspCount_TextChanged(object sender, System.EventArgs e)
		{
			svdata.griddspcnt[svdata.lastdb] = this.txtDspCount.Text;
		}

		private void fldmenuCopyNoComma_Click(object sender, System.EventArgs e)
		{
			copyfldlist(true,false);
		}

		private void fldmenuCopyNoCRLFNoComma_Click(object sender, System.EventArgs e)
		{
			copyfldlist(false,false);
		}

		private void rdoUnicode_CheckedChanged(object sender, System.EventArgs e)
		{
			if( this.rdoUnicode.Checked == true )
			{
				svdata.txtencode[svdata.lastdb] = 0;
			}
		}

		private void rdoSjis_CheckedChanged(object sender, System.EventArgs e)
		{
			if( this.rdoSjis.Checked == true )
			{
				svdata.txtencode[svdata.lastdb] = 1;
			}
		}

		private void rdoUtf8_CheckedChanged(object sender, System.EventArgs e)
		{
			if( this.rdoUtf8.Checked == true )
			{
				svdata.txtencode[svdata.lastdb] = 2;
			}
		}
		private System.Text.Encoding GetEncode()
		{
			if( this.rdoUnicode.Checked == true )
			{
				// UNICODE
				return new System.Text.UnicodeEncoding();
			}
			else if( this.rdoSjis.Checked == true )
			{
				// (MS932)ShiftJIS
				return Encoding.GetEncoding("shift-jis");
			}
			else
			{
				// UTF-8
				return new System.Text.UTF8Encoding();
			}
		}

		private void chkDspFieldAttr_CheckedChanged(object sender, System.EventArgs e)
		{
			if( this.tableList.SelectedItems.Count == 1 )
			{
				dspfldlist(this.tableList.SelectedItem.ToString());
			}
			else
			{
				dspfldlist("");
			}
		}

		private void btnQuerySelect_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.InitErrMessage();

				if( Sqldlg.ShowDialog() == DialogResult.OK )
				{
					SqlDataAdapter da = new SqlDataAdapter(Sqldlg.SelectSql, this.sqlConnection1);
					da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
					dspdt = new DataSet();
					dspdt.CaseSensitive = true;

					da.Fill(dspdt,"aaaa");

					//�V����DataGridTableStyle�̍쐬
					DataGridTableStyle ts = new DataGridTableStyle();
					//�}�b�v�����w�肷��
					ts.MappingName = "aaaa";

					MyDataGridTextBoxColumn cs;
					foreach( DataColumn col in dspdt.Tables[0].Columns )
					{
						//��X�^�C����MyDataGridTextBoxColumn���g��
						if( col.DataType.FullName == "System.String" )
						{
							cs = new MyDataGridTextBoxColumn(true);
						}
						else
						{
							cs = new MyDataGridTextBoxColumn(false);
							if( col.DataType.FullName == "System.Int32" ||
								col.DataType.FullName == "System.Int16" ||
								col.DataType.FullName == "System.Int64" ||
								col.DataType.FullName == "System.UInt32" ||
								col.DataType.FullName == "System.UInt16" ||
								col.DataType.FullName == "System.UInt64" ||
								col.DataType.FullName == "System.Decimal" )
							{
								cs.Format = getFormat(this.NumFormat);
							}
							if( col.DataType.FullName == "System.Double" ||
								col.DataType.FullName == "System.Single" )
							{
								cs.Format = getFormat(this.FloatFormat);
							}
							if( col.DataType.FullName == "System.DateTime" )
							{
								cs.Format = getFormat(this.DateFormat);
							}
						}
						//�}�b�v�����w�肷��
						cs.MappingName = col.ColumnName;
						if( col.AllowDBNull == true )
						{
							cs.HeaderText = "��"+col.ColumnName;
						}
						else
						{
							cs.HeaderText = col.ColumnName;
						}
					
						//DataGridTableStyle�ɒǉ�����
						ts.GridColumnStyles.Add(cs);
					}

					//�e�[�u���X�^�C����DataGrid�ɒǉ�����
					this.dbGrid.TableStyles.Clear();
					this.dbGrid.TableStyles.Add(ts);

					this.dbGrid.ReadOnly = true;
					this.btnDataEdit.BackColor = this.btnBackColor;
					this.btnDataEdit.ForeColor = this.btnForeColor;
					this.btnTmpAllDsp.BackColor = this.btnBackColor;
					this.btnTmpAllDsp.ForeColor = this.btnForeColor;
					this.btnDataEdit.Text = "�f�[�^�ҏW(&T)";
					this.btnDataUpdate.Enabled = true;
					this.btnDataEdit.Enabled = true;
					this.btnGridFormat.Enabled = true;
					this.chkDspData.Checked = true;
					this.dbGrid.AllowSorting = true;
					this.toolTip3.SetToolTip(this.dbGrid,Sqldlg.SelectSql.Replace("\r\n"," ").Replace("\t"," "));
					this.dbGrid.SetDataBinding(dspdt,"aaaa");
					this.dbGrid.Show();
				}
			}
			catch( Exception exp)
			{
				this.SetErrorMessage(exp);
			}
		}

		private void btnDataUpdate_Click(object sender, System.EventArgs e)
		{
			SqlTransaction tran	= null;
			try
			{
				this.InitErrMessage();

				if( this.chkDspData.CheckState == CheckState.Checked &&
					this.tableList.SelectedItems.Count == 1 &&
					this.dspdt.GetChanges() != null &&
					this.dspdt.GetChanges().Tables[0].Rows.Count > 0 &&
					MessageBox.Show("�{���ɍX�V���Ă�낵���ł���","",MessageBoxButtons.YesNo) == DialogResult.Yes
					)
				{
					// 1���̂ݑI������Ă���ꍇ�A�f�[�^�\�����ɁA�Y���e�[�u���̃f�[�^��\������
					string tbname = this.tableList.SelectedItem.ToString();
					string sqlstr;
					sqlstr = "select ";
					int	maxlines;
					if( this.txtDspCount.Text != "" )
					{
						maxlines = int.Parse(this.txtDspCount.Text);
					}
					else
					{
						maxlines = 0;
					}
					if( maxlines != 0 )
					{
						sqlstr += " TOP " + this.txtDspCount.Text;
					}

					sqlstr += string.Format(" * from {0}",gettbname(tbname));
					//sqlstr += " * from [" + tbname + "]";
					if( this.txtWhere.Text.Trim() != "" )
					{
						sqlstr += " where " + this.txtWhere.Text.Trim();
					}
					if( this.txtSort.Text.Trim() != "" )
					{
						sqlstr += " order by " + this.txtSort.Text.Trim();
					}
					SqlDataAdapter da = new SqlDataAdapter(sqlstr, this.sqlConnection1);
										
					tran = this.sqlConnection1.BeginTransaction();
					da.SelectCommand.Transaction = tran;
					SqlCommandBuilder  cb = new SqlCommandBuilder(da);
					da.Update(dspdt, "aaaa");
					tran.Commit();

					this.dbGrid.SetDataBinding(dspdt, "aaaa");
				}
			}
			catch( Exception exp )
			{
				this.SetErrorMessage(exp);
				tran.Rollback();
			}

		}

		private void btnDataEdit_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.InitErrMessage();

				if( this.dbGrid.ReadOnly == true )
				{
					// �ҏW�ɂ���
					this.dbGrid.ReadOnly = false;
					this.btnDataEdit.Text = "�f�[�^�ҏW�I��(&T)";
					this.btnDataEdit.ForeColor = Color.WhiteSmoke;
					this.btnDataEdit.BackColor = Color.Navy;
				}
				else
				{
					// �ҏW�s�ɂ���
					if( this.dspdt.Tables["aaaa"].GetChanges() == null ||
						this.dspdt.Tables["aaaa"].GetChanges().Rows.Count == 0 )
					{
						this.dbGrid.ReadOnly = true;
						this.btnDataEdit.Text = "�f�[�^�ҏW(&T)";
						this.btnDataEdit.BackColor = this.btnBackColor;
						this.btnDataEdit.ForeColor = this.btnForeColor;
					}
					else
					{
						// �ύX��������
						if( MessageBox.Show("�ύX��j�����Ă���낵���ł���?","",MessageBoxButtons.YesNo) == DialogResult.Yes )
						{
							this.dspdt.Tables["aaaa"].RejectChanges();
							this.dbGrid.SetDataBinding(dspdt, "aaaa");
							this.dbGrid.Show();
							this.dbGrid.ReadOnly = true;
							this.btnDataEdit.Text = "�f�[�^�ҏW(&T)";
							this.btnDataEdit.BackColor = this.btnBackColor;
							this.btnDataEdit.ForeColor = this.btnForeColor;
						}
					}
					
				}
			}
			catch( Exception exp )
			{
				this.SetErrorMessage(exp);
			}
		}
		private void dbGrid_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if( this.dspdt == null ||
				this.dspdt.Tables.Count == 0 ||
				this.dspdt.Tables["aaaa"].Rows.Count == 0 )
			{
				return;
			}

			int row = 0;
			int yDelta = dbGrid.GetCellBounds(row, 0).Height + 1;
			int y = dbGrid.GetCellBounds(row, 0).Top + 2;
     
			CurrencyManager cm = (CurrencyManager) this.BindingContext[dbGrid.DataSource, dbGrid.DataMember];
			while(y < dbGrid.Height - yDelta && row < cm.Count)
			{
				//get & draw the header text...
				string text = string.Format("{0}", row+1);
				e.Graphics.DrawString(text, dbGrid.Font, new SolidBrush(Color.Black), 5, y);
				y += yDelta;
				row++;
			}
		}

		private void btnGridFormat_Click(object sender, System.EventArgs e)
		{
			GridFormatDialog dlg = new GridFormatDialog();
			dlg.gfont = gfont;
			dlg.gcolor = gcolor;
			dlg.NumFormat = this.NumFormat;
			dlg.FloatFormat = this.FloatFormat;
			dlg.DateFormat = this.DateFormat;
			
			if( dlg.ShowDialog() == DialogResult.OK )
			{
				this.gfont = dlg.gfont;
				this.gcolor = dlg.gcolor;
				this.dbGrid.Font = this.gfont;
				this.dbGrid.ForeColor = this.gcolor;
				this.NumFormat = dlg.NumFormat;
				this.FloatFormat = dlg.FloatFormat;
				this.DateFormat = dlg.DateFormat;
			}
		}


		private void btnQueryNonSelect_Click(object sender, System.EventArgs e)
		{
			SqlTransaction tran	= null;
			try
			{
				this.InitErrMessage();

				if( Sqldlg2.ShowDialog() == DialogResult.OK )
				{
					tran = this.sqlConnection1.BeginTransaction();

					SqlCommand cm = new SqlCommand(Sqldlg2.SelectSql,this.sqlConnection1,tran);
					cm.CommandTimeout = this.SqlTimeOut;

					string msg = "";
					if( Sqldlg2.hasReturn == true )
					{
						object ret = cm.ExecuteScalar();
						tran.Commit();
						msg = string.Format("�������I�����܂����B\r\n���^�[���l�� [{0}] �ł�", ret.ToString() );
					}
					else
					{
						int cnt = cm.ExecuteNonQuery();
						tran.Commit();
						msg = string.Format("�������I�����܂����B\r\n�e������������ {0} ���ł�", cnt );
					}
					MessageBox.Show(msg);
				}
			}
			catch( Exception exp)
			{
				if( tran != null )
				{
					tran.Rollback();
				}
				this.SetErrorMessage(exp);
			}
		}

		protected string gettbname(string tbname)
		{
			string delimStr = ".";
			string []str = tbname.Split(delimStr.ToCharArray(), 2);
			return string.Format("[{0}].[{1}]",str[0],str[1]);
		}

		protected string gettbnameAdd(string tbname,string addstr)
		{
			string delimStr = ".";
			string []str = tbname.Split(delimStr.ToCharArray(), 2);
			return string.Format("[{0}].[{1}_{2}]",str[0], str[1], addstr);
		}

		private void Redisp_Click(object sender, System.EventArgs e)
		{
			//�ĕ`��{�^������
			if( this.chkDspData.CheckState == CheckState.Checked &&
				this.tableList.SelectedItems.Count == 1 )
			{
				// 1���̂ݑI������Ă���ꍇ�A�f�[�^�\�����ɁA�Y���e�[�u���̃f�[�^��\������
				DspData(this.tableList.SelectedItem.ToString());
			}
			else
			{
				DspData("");
			}
		}

		protected string getFormat(string fstr)
		{
			if(fstr == null)
			{
				return "";
			}
			int termp = fstr.IndexOf("	");
			if( termp == -1 )
			{
				return fstr;
			}
			return fstr.Substring(0,termp);
		}

		private void btnIndex_Click(object sender, System.EventArgs e)
		{
			if( indexdlg == null )
			{
				indexdlg = new IndexViewDialog();
				indexdlg.sqlConnection = this.sqlConnection1;
				if( this.tableList.SelectedItems.Count == 1 )
				{
					indexdlg.dsptbname = this.tableList.SelectedItem.ToString();
				}
				else
				{
					indexdlg.dsptbname = "";
				}
				indexdlg.Show();
			}
			else
			{
				indexdlg.Show();
				indexdlg.BringToFront();
			}
		}

		private void btnTmpAllDsp_Click(object sender, System.EventArgs e)
		{
			//�ĕ`��{�^������
			if( this.chkDspData.CheckState == CheckState.Checked &&
				this.tableList.SelectedItems.Count == 1 )
			{
				// 1���̂ݑI������Ă���ꍇ�A�f�[�^�\�����ɁA�Y���e�[�u���̃f�[�^��\������
				DspData(this.tableList.SelectedItem.ToString(),true);
				this.btnTmpAllDsp.ForeColor = Color.WhiteSmoke;
				this.btnTmpAllDsp.BackColor = Color.Navy;
				this.btnTmpAllDsp.Enabled = true;
			}
			else
			{
				DspData("");
			}
		}

		/// <summary>
		/// �I�����ꂽ�e�[�u���Ɋւ���ˑ��֌W���o�͂���
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DependOutPut(object sender, System.EventArgs e)
		{
			SqlCommand	cm = new SqlCommand();
			cm.CommandTimeout = this.SqlTimeOut;

			if( this.tableList.SelectedItems.Count == 0 )
			{
				return;
			}
			if( CheckFileSpec() == false )
			{
				return;
			}
			
			this.InitErrMessage();

			try
			{
				StringBuilder strline =  new StringBuilder();
				TextWriter	wr = new StringWriter(strline);
				StringBuilder fname = new StringBuilder();

				if( this.rdoClipboard.Checked == true) 
				{
					wr = new StringWriter(strline);
					wr.Write("�e�[�u����");
					wr.Write("\t�ˑ��֌W�於��");
					wr.Write("\t���");
					wr.Write("\t�X�V����");
					wr.Write("\tselect�ł̗��p");
					wr.Write("\t�]���������݂����܂��̓p�����[�^");
					wr.Write(wr.NewLine);
				}
				else if( this.rdoOutFile.Checked == true ) 
				{
					StreamWriter sw = new StreamWriter(this.txtOutput.Text,false, GetEncode());
					sw.AutoFlush = false;
					wr = sw;
					fname.Append(this.txtOutput.Text);
					wr.Write("�e�[�u����");
					wr.Write("\t�ˑ��֌W�於��");
					wr.Write("\t���");
					wr.Write("\t�X�V����");
					wr.Write("\tselect�ł̗��p");
					wr.Write("\t�]���������݂����܂��̓p�����[�^");
					wr.Write(wr.NewLine);
				}

				foreach( String tbname in this.tableList.SelectedItems )
				{
					if( this.rdoOutFolder.Checked == true ) 
					{
						StreamWriter sw = new StreamWriter(this.txtOutput.Text + "\\" + tbname + ".csv",false, GetEncode());
						sw.AutoFlush = false;
						wr = sw;
						fname.Append(this.txtOutput.Text + "\\" + tbname + ".sql\r\n");
						wr.Write("�e�[�u����");
						wr.Write("\t�ˑ��֌W�於��");
						wr.Write("\t���");
						wr.Write("\t�X�V����");
						wr.Write("\tselect�ł̗��p");
						wr.Write("\t�]���������݂����܂��̓p�����[�^");
						wr.Write(wr.NewLine);
					}

					// �ˑ��֌W�̏����擾���A

					// get id 
					string sqlstr;
					// split owner.table -> owner, table

					string delimStr = ".";
					string []str = tbname.Split(delimStr.ToCharArray(), 2);
					sqlstr = "sp_depends N'[" + str[0] +"].[" + str[1] + "]'";
					SqlDataAdapter da = new SqlDataAdapter(sqlstr, this.sqlConnection1);
					DataSet ds = new DataSet();
					ds.CaseSensitive = true;
					da.Fill(ds,tbname);

					if(	ds.Tables.Count != 0 &&
						ds.Tables[tbname].Rows != null &&
						ds.Tables[tbname].Rows.Count != 0)
					{
						foreach(DataRow dr in ds.Tables[tbname].Rows)
						{
							// �e�[�u����
							wr.Write(tbname);
							foreach( DataColumn col in ds.Tables[tbname].Columns)
							{
								wr.Write("\t");
								wr.Write(dr[col.ColumnName].ToString());
							}
							wr.Write(wr.NewLine);
						}
					}

					if( this.rdoOutFolder.Checked == true ) 
					{
						wr.Close();
					}
				}
				if( this.rdoOutFolder.Checked == false ) 
				{
					wr.Close();
				}
				if( this.rdoClipboard.Checked == true ) 
				{
					Clipboard.SetDataObject(strline.ToString(),true );
				}
				else
				{
					Clipboard.SetDataObject(fname.ToString(),true );
				}
				MessageBox.Show("�������������܂���");
			}
			catch ( Exception se )
			{
				this.SetErrorMessage(se);
			}
			finally 
			{
				if( cm != null )
				{
					cm.Dispose();
				}
			}

		
		}

		/// <summary>
		/// �I�����ꂽ�e�[�u���Ɋւ���f�[�^�������o�͂���
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RecordCountOutPut(object sender, System.EventArgs e)
		{
			SqlDataReader dr = null;
			SqlCommand	cm = new SqlCommand();
			cm.CommandTimeout = this.SqlTimeOut;

			if( this.tableList.SelectedItems.Count == 0 )
			{
				return;
			}
			if( this.tableList.SelectedItems.Count > 1 &&
				this.txtWhere.Text != null &&
				this.txtWhere.Text.Trim() != "" )
			{
				if( MessageBox.Show("�����e�[�u���ɓ���� where ���K�p���܂����H","�m�F",System.Windows.Forms.MessageBoxButtons.YesNo) 
					== System.Windows.Forms.DialogResult.No )
				{
					return;
				}
			}

			if( CheckFileSpec() == false )
			{
				return;
			}

			this.InitErrMessage();

			int			rowcount = 0;
			int			trow = 0;
			try
			{

				StringBuilder strline =  new StringBuilder();
				TextWriter	wr = new StringWriter(strline);
				StringBuilder fname = new StringBuilder();

				if( this.rdoClipboard.Checked == true) 
				{
					wr = new StringWriter(strline);
					wr.WriteLine("�e�[�u����,�f�[�^����");
				}
				else if( this.rdoOutFile.Checked == true ) 
				{
					StreamWriter sw = new StreamWriter(this.txtOutput.Text,false, GetEncode());
					sw.AutoFlush = false;
					wr = sw;
					fname.Append(this.txtOutput.Text);
					wr.WriteLine("�e�[�u����,�f�[�^����");
				}

				foreach( String tbname in this.tableList.SelectedItems )
				{

					if( this.rdoOutFolder.Checked == true ) 
					{
						StreamWriter sw = new StreamWriter(this.txtOutput.Text + "\\" + tbname + ".csv.tmp",false, GetEncode());
						sw.AutoFlush = false;
						wr = sw;
						wr.WriteLine("�e�[�u����,�f�[�^����");
					}
					trow = 0;
					string sqlstr;
					sqlstr = string.Format("select  count(1) from {0} ",gettbname(tbname));
					if( this.txtWhere.Text.Trim() != "" )
					{
						sqlstr += " where " + this.txtWhere.Text.Trim();
					}

					cm.CommandText = sqlstr;
					cm.Connection = this.sqlConnection1;

					dr = cm.ExecuteReader();

					ArrayList fldname = new ArrayList();
					ArrayList strint = new ArrayList();
	
					fldname.Clear();
					strint.Clear();


					// �f�[�^�̏����o��
					while (dr.Read())
					{
						rowcount++;
						trow++;
						wr.Write(gettbname(tbname));
						wr.Write(",");
						if( dr.IsDBNull(0) )
						{
							wr.WriteLine( "0" );
						}
						else
						{
							wr.WriteLine( dr.GetValue(0).ToString() );
						}
					}
					if( dr != null && dr.IsClosed == false )
					{
						dr.Close();
					}
					if( this.rdoOutFolder.Checked == true ) 
					{
						wr.Close();
						if( trow > 0 )
						{
							fname.Append(this.txtOutput.Text + "\\" + tbname + ".csv\r\n");
							// �t�@�C�����쐬�������A��ɂȂ����̂ō폜����	
							File.Copy(this.txtOutput.Text + "\\" + tbname + ".csv.tmp", 
								this.txtOutput.Text + "\\" + tbname + ".csv", 
								true );
						}
						File.Delete(this.txtOutput.Text + "\\" + tbname + ".csv.tmp");
					}
				}
				if( this.rdoOutFolder.Checked == false ) 
				{
					wr.Close();
				}
				if( rowcount == 0 )
				{
					MessageBox.Show("�Ώۃf�[�^������܂���ł���");
				}
				else
				{
					if( this.rdoClipboard.Checked == true ) 
					{
						Clipboard.SetDataObject(strline.ToString(),true );
					}
					else
					{
						Clipboard.SetDataObject(fname.ToString(),true );
					}
					MessageBox.Show("�������������܂���");
				}
			}
			catch ( System.Data.SqlClient.SqlException se )
			{
				if( dr != null && dr.IsClosed == false )
				{
					dr.Close();
				}
				this.SetErrorMessage(se);
				return;
			}
			catch( Exception se )
			{
				if( dr != null && dr.IsClosed == false )
				{
					dr.Close();
				}
				this.SetErrorMessage(se);
			}
			finally 
			{
				if( cm != null )
				{
					cm.Dispose();
				}
			}

			// set datas to clipboard
		}

		private void btnEtc_Click(object sender, System.EventArgs e)
		{
			//this.etcContextMenu.MenuItems.Clear();
			//this.etcContextMenu.MenuItems.Add(this.menuDepend);
			//this.etcContextMenu.MenuItems.Add(this.menuQuery);

			this.etcContextMenu.Show(this.btnEtc,new Point(0,0));
		}

		/// <summary>
		/// ���ݐڑ����DB�������l�Ƃ��āA�N�G���A�i���C�U���N������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CallISQLW(object sender, System.EventArgs e)
		{
			Process isqlProcess = new Process();
			isqlProcess.StartInfo.FileName = "isqlw";
			isqlProcess.StartInfo.ErrorDialog = true;
			string serverstr = "";
			if( this.instanceName != "" )
			{
				serverstr = this.serverRealName + "\\" + this.instanceName;
			}
			else
			{
				serverstr = this.serverRealName;
			}
			if( this.IsUseTruse == true )
			{
				if( this.dbList.SelectedItems.Count != 0 )
				{
					isqlProcess.StartInfo.Arguments = string.Format(" -S {0} -d {1} -E ",
						serverstr,
						(string)this.dbList.SelectedItem
						);
				}
				else
				{
					isqlProcess.StartInfo.Arguments = string.Format(" -S {0} -E ",
						serverstr
						);
				}
			}
			else
			{
				if( this.dbList.SelectedItems.Count != 0 )
				{
					isqlProcess.StartInfo.Arguments = string.Format(" -S {0} -d {1} -U {2} -P {3} ",
						serverstr,
						(string)this.dbList.SelectedItem,
						this.loginUid,
						this.loginPasswd );
				}
				else
				{
					isqlProcess.StartInfo.Arguments = string.Format(" -S {0} -U {2} -P {3} ",
						serverstr,
						this.loginUid,
						this.loginPasswd );
				}
			}
			isqlProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
			isqlProcess.Start();
		}

		/// <summary>
		/// ���ݐڑ����DB�������l�Ƃ��āA�N�G���A�i���C�U���N������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CallProfile(object sender, System.EventArgs e)
		{
			Process isqlProcess = new Process();
			isqlProcess.StartInfo.FileName = "profiler.exe";
			isqlProcess.StartInfo.ErrorDialog = true;
			string serverstr = "";
			if( this.instanceName != "" )
			{
				serverstr = this.serverRealName + "\\" + this.instanceName;
			}
			else
			{
				serverstr = this.serverRealName;
			}
			if( this.IsUseTruse == true )
			{
				if( this.dbList.SelectedItems.Count != 0 )
				{
					isqlProcess.StartInfo.Arguments = string.Format("/S{0} /D{1} /E ",
						serverstr,
						(string)this.dbList.SelectedItem
						);
				}
				else
				{
					isqlProcess.StartInfo.Arguments = string.Format("/S{0} /E ",
						serverstr
						);
				}
			}
			else
			{
				if( this.dbList.SelectedItems.Count != 0 )
				{
					isqlProcess.StartInfo.Arguments = string.Format(" /S{0} D{1} /U{2} /P{3} ",
						serverstr,
						(string)this.dbList.SelectedItem,
						this.loginUid,
						this.loginPasswd );
				}
				else
				{
					isqlProcess.StartInfo.Arguments = string.Format(" /S{0} /U{2} /P{3} ",
						serverstr,
						this.loginUid,
						this.loginPasswd );
				}
			}
			isqlProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
			isqlProcess.Start();
		}

		/// <summary>
		/// �G���^�[�v���C�Y�}�l�[�W���[���N������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CallEPM(object sender, System.EventArgs e)
		{
			Process isqlProcess = new Process();
			isqlProcess.StartInfo.FileName = "SQL Server Enterprise Manager.MSC";
			isqlProcess.StartInfo.ErrorDialog = true;

			isqlProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
			isqlProcess.Start();
		}

		private void mainContextMenu_Popup(object sender, System.EventArgs e)
		{
		
		}

		private void menuMakeTab_Click(object sender, System.EventArgs e)
		{
			crecsv(false,"	");
		}

		private void menuMakeTabDQ_Click(object sender, System.EventArgs e)
		{
			crecsv(true,"	");
		}

		private void btnWhereZoom_Click(object sender, System.EventArgs e)
		{
			ZoomFloatingDialog dlg = new ZoomFloatingDialog();
			dlg.EditText = this.txtWhere.Text;
			dlg.LableName = "where �w��";
			dlg.Enter += new System.EventHandler(this.dlgWhereZoom_Click);
			dlg.Show();
			dlg.BringToFront();
			dlg.Focus();
		}

		private void dlgWhereZoom_Click(object sender, System.EventArgs e)
		{
			this.txtWhere.Text = ((ZoomDialog)sender).EditText;

			string targetTable = "";
			if( this.tableList.SelectedItems.Count == 1 )
			{
				targetTable = this.tableList.SelectedItem.ToString();
			}
			DspData(targetTable);
		}

		private void btnOrderZoom_Click(object sender, System.EventArgs e)
		{
			ZoomFloatingDialog dlg = new ZoomFloatingDialog();
			dlg.EditText = this.txtSort.Text;
			dlg.LableName = "order by �w��";
			dlg.Enter += new System.EventHandler(this.dlgSortZoom_Click);
			dlg.Show();
			dlg.BringToFront();
			dlg.Focus();
		}
		private void dlgSortZoom_Click(object sender, System.EventArgs e)
		{
			this.txtSort.Text = ((ZoomDialog)sender).EditText;
			string targetTable = "";
			if( this.tableList.SelectedItems.Count == 1 )
			{
				targetTable = this.tableList.SelectedItem.ToString();
			}
			DspData(targetTable);
		}

		private void useCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
		
		}

		private void menuInsertDeleteTaihi_Click(object sender, System.EventArgs e)
		{
			this.CreInsert(true,true,true);
		}

		private void menuInsertNoFldDeleteTaihi_Click(object sender, System.EventArgs e)
		{
			this.CreInsert(false,true,true);
		}

		private void menuRecordCountDsp_Click(object sender, System.EventArgs e)
		{
			SqlDataReader dr = null;
			SqlCommand	cm = new SqlCommand();
			cm.CommandTimeout = this.SqlTimeOut;

			DataTable	rcdt = new DataTable("RecordCount");
			rcdt.CaseSensitive = true;

			if( this.tableList.SelectedItems.Count == 0 )
			{
				return;
			}
			if( this.tableList.SelectedItems.Count > 1 &&
				this.txtWhere.Text != null &&
				this.txtWhere.Text.Trim() != "" )
			{
				if( MessageBox.Show("�����e�[�u���ɓ���� where ���K�p���܂����H","�m�F",System.Windows.Forms.MessageBoxButtons.YesNo) 
					== System.Windows.Forms.DialogResult.No )
				{
					return;
				}
			}

			this.InitErrMessage();

			int			rowcount = 0;
			int			trow = 0;
			rcdt.Columns.Add("�e�[�u����");
			rcdt.Columns.Add("�f�[�^����",typeof(int));

			try
			{

				foreach( String tbname in this.tableList.SelectedItems )
				{
					trow = 0;
					string sqlstr;
					sqlstr = string.Format("select  count(1) from {0} ",gettbname(tbname));
					if( this.txtWhere.Text.Trim() != "" )
					{
						sqlstr += " where " + this.txtWhere.Text.Trim();
					}

					cm.CommandText = sqlstr;
					cm.Connection = this.sqlConnection1;

					dr = cm.ExecuteReader();

					ArrayList fldname = new ArrayList();
					ArrayList strint = new ArrayList();
	
					fldname.Clear();
					strint.Clear();


					// �f�[�^�̏����o��
					while (dr.Read())
					{
						rowcount++;
						trow++;
						DataRow addrow = rcdt.NewRow();
						addrow[0] = gettbname(tbname);
						if( dr.IsDBNull(0) )
						{
							addrow[1] = 0;
						}
						else
						{
							addrow[1] = dr.GetValue(0);
						}
						rcdt.Rows.Add(addrow);
					}
					if( dr != null && dr.IsClosed == false )
					{
						dr.Close();
					}
				}
				if( rowcount == 0 )
				{
					MessageBox.Show("�Ώۃf�[�^������܂���ł���");
				}
				else
				{
					DataGridViewBase dlg = new DataGridViewBase(rcdt,"�f�[�^����");
					dlg.ShowDialog();
				}
			}
			catch ( System.Data.SqlClient.SqlException se )
			{
				if( dr != null && dr.IsClosed == false )
				{
					dr.Close();
				}
				this.SetErrorMessage(se);
				return;
			}
			catch( Exception se )
			{
				if( dr != null && dr.IsClosed == false )
				{
					dr.Close();
				}
				this.SetErrorMessage(se);
			}
			finally 
			{
				if( cm != null )
				{
					cm.Dispose();
				}
			}

			// set datas to clipboard
		
		}

		private void copyDbGridMenu_Click(object sender, System.EventArgs e)
		{
			if( this.dbGrid.Visible == false )
			{
				return;
			}
			if( this.dbGrid.DataSource == null )
			{
				return;
			}
			DataTable dt = null;
			if( this.dbGrid.DataSource is DataSet )
			{
				dt = ((DataSet)this.dbGrid.DataSource).Tables[0];
			}
			else if( this.dbGrid.DataSource is DataTable )
			{
				dt = (DataTable)this.dbGrid.DataSource;
			}
			if( dt == null ||
				dt.Rows.Count == 0 )
			{
				return;
			}
			StringBuilder strline = new StringBuilder();
			StringWriter wr = new StringWriter(strline);
			// header 
			int cnt = 0;
			foreach( DataColumn col in dt.Columns )
			{
				if( cnt != 0 )
				{
					wr.Write("\t");
				}
				wr.Write(col.ColumnName);
				cnt++;
			}
			wr.Write(wr.NewLine);

			foreach( DataRow dr in dt.Rows )
			{
				for( int i = 0; i < dt.Columns.Count; i++ )
				{
					if( i != 0 )
					{
						wr.Write("\t");
					}
					if( dr[i] != DBNull.Value )
					{
						wr.Write(dr[i].ToString());
					}
				}
				wr.Write(wr.NewLine);
			}
			Clipboard.SetDataObject(strline.ToString(),true );
			MessageBox.Show("�������������܂���");
		}

		protected bool	BeginNewThread(WaitCallback callb, object tags)
		{
			if( this.IsThreadAlive > 0 )
			{
				return false;
			}
			try
			{
				Interlocked.Increment(ref this.IsThreadAlive);
				ThreadPool.QueueUserWorkItem(callb,tags);
				return true;
			}
			catch(Exception e)
			{
				this.SetErrorMessage(e);
				if( this.IsThreadAlive != 0 )
				{
					Interlocked.Decrement(ref this.IsThreadAlive);
				}
				return false;
			}
		}

		protected void	ThreadEndIfAlive()
		{
			if( this.IsThreadAlive == 0 )
			{
				return;
			}
			try
			{
				Interlocked.Decrement(ref this.IsThreadAlive);
			}
			catch
			{
				;
			}
		}

		protected void MainForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if( e.Control == true && e.Shift == false && e.Alt == false && e.KeyCode == Keys.G )
			{
				ThreadEndIfAlive();
			}
		}

		protected bool IsProcCancel()
		{
			if( this.IsThreadAlive == 0 )
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		
		protected ProcCondition GetProcCondition(string tbname)
		{
			ProcCondition procCond = new ProcCondition();
			if( tbname != null )
			{
				procCond.Tbname.Add(tbname);
			}
			else
			{
				foreach( object obj in this.tableList.SelectedItems)
				{
					procCond.Tbname.Add((string)obj);
				}
			}
			procCond.WhereStr = this.txtWhere.Text;
			procCond.OrderStr = this.txtSort.Text;
			procCond.MaxStr = this.txtDspCount.Text;
			return procCond;
		}

		private void fieldListbox_CopyData(object sender)
		{
			copyfldlist(true,true);
		}

		private void dbList_CopyData(object sender)
		{
			if( this.dbList.SelectedItems.Count > 0 )
			{
				StringBuilder strline =  new StringBuilder();
				foreach( string name in dbList.SelectedItems )
				{
					if( strline.Length != 0 )
					{
						strline.Append(",");
						strline.Append("\r\n");
					}
					strline.Append(name);
				}
				Clipboard.SetDataObject(strline.ToString(),true );
			}
		}

		private void ownerListbox_CopyData(object sender)
		{
			// DB���̃R�s�[
			if( this.ownerListbox.SelectedItems.Count > 0 )
			{
				StringBuilder strline =  new StringBuilder();
				foreach( string name in ownerListbox.SelectedItems )
				{
					if( strline.Length != 0 )
					{
						strline.Append(",");
						strline.Append("\r\n");
					}
					strline.Append(name);
				}
				Clipboard.SetDataObject(strline.ToString(),true );
			}
		}

		private void tableList_CopyData(object sender)
		{
			copytablename(false);
		}

		private void cmbHistory_SelectionChangeCommitted(object sender, System.EventArgs e)
		{
			if( this.cmbHistory.SelectedIndex < 0 )
			{
				return;
			}
			string tablename = (string)this.cmbHistory.SelectedItem;

			isInCmbEvent = true;
			int setidx = this.tableList.FindStringExact(tablename);
			this.tableList.ClearSelected();
			this.tableList.SelectedIndex = setidx;
			isInCmbEvent = false;
			this.tableList.Focus();
		}

		private void SetNewHistory(string key, string hvalue, ref textHistory tdata)
		{
			if( hvalue == null || hvalue == "" )
			{
				return;
			}

			DataRow []drl = tdata.textHistoryData.Select( string.Format("KeyValue = '{0}'", key ) );

			for( int i = 0; i < drl.Length; i++ ){
				if( (string)drl[i]["DataValue"] == hvalue )
				{
					// �����e�[�u���ɑ΂��A���ɓ����������o�^����Ă��邽�߁A�������Ȃ�
					return;
				}
			}
			
			textHistory.textHistoryDataRow ndr = tdata.textHistoryData.NewtextHistoryDataRow();
			ndr.KeyValue = key;
			ndr.DataValue = hvalue;
			tdata.textHistoryData.Rows.Add(ndr);
		}

		private void txtWhere_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if( e.Alt == false &&
				e.Control == true &&
				e.KeyCode == Keys.D )
			{
				// �S�폜���s��
				((TextBox)sender).Text = "";
			}
			if( e.Alt == false &&
				e.Control == true &&
				e.KeyCode == Keys.S )
			{
				string targetTable = "";
				if( this.tableList.SelectedItems.Count == 1 )
				{
					targetTable = this.tableList.SelectedItem.ToString();
				}
				HistoryViewer hv = new HistoryViewer(this.whereHistory, targetTable);
				if( DialogResult.OK == hv.ShowDialog() && this.txtWhere.Text != hv.retString)
				{
					this.txtWhere.Text = hv.retString;
					SetNewHistory(targetTable,hv.retString,ref this.whereHistory);

					DspData(targetTable);
				}
			}
			if( e.KeyCode == Keys.Return ||
				e.KeyCode == Keys.Enter )
			{
				string targetTable = "";
				if( this.tableList.SelectedItems.Count == 1 )
				{
					targetTable = this.tableList.SelectedItem.ToString();
				}
				SetNewHistory(targetTable,this.txtWhere.Text,ref this.whereHistory);
				DspData(targetTable);
			}
		
		}

		private void txtSort_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if( e.Alt == false &&
				e.Control == true &&
				e.KeyCode == Keys.D )
			{
				// �S�폜���s��
				((TextBox)sender).Text = "";
			}
			if( e.Alt == false &&
				e.Control == true &&
				e.KeyCode == Keys.S )
			{
				string targetTable = "";
				if( this.tableList.SelectedItems.Count == 1 )
				{
					targetTable = this.tableList.SelectedItem.ToString();
				}
				HistoryViewer hv = new HistoryViewer(this.sortHistory, targetTable);
				if( DialogResult.OK == hv.ShowDialog() && this.txtSort.Text != hv.retString)
				{
					this.txtSort.Text = hv.retString;
					SetNewHistory(targetTable,hv.retString,ref this.sortHistory);

					DspData(targetTable);
				}
			}
			if( e.KeyCode == Keys.Return ||
				e.KeyCode == Keys.Enter )
			{
				string targetTable = "";
				if( this.tableList.SelectedItems.Count == 1 )
				{
					targetTable = this.tableList.SelectedItem.ToString();
				}
				SetNewHistory(targetTable,this.txtSort.Text,ref this.sortHistory);
				DspData(targetTable);
			}
		
		}
	}

	public class ProcCondition
	{
		public ArrayList	Tbname;
		public string		WhereStr;
		public string		OrderStr;
		public string		MaxStr;
		public bool		isAllDisp;

		public ProcCondition()
		{
			Tbname = new ArrayList();
			this.WhereStr = "";
			this.OrderStr = "";
			this.MaxStr = "";
			this.isAllDisp = false;
		}
	}

	public class MyDataGridTextBoxColumn : DataGridTextBoxColumn
	{
		private CurrencyManager _sorce;
		private int				editrow;
		private bool	canSetEmptyString;

		public MyDataGridTextBoxColumn(bool canset)
		{
			this.NullText = "";
			this.TextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GridKeyDownControler);
			this.canSetEmptyString = canset;
		}

		private void GridKeyDownControler(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			// Ctrl+3 �Œl�̕ҏW�_�C�A���O��\��
			if(	e.KeyCode == Keys.D3 &&
				e.Control == true &&
				e.Alt != true &&
				e.Shift != true )
			{
				// �o�C�i���̏ꍇ�A�C���[�W�\�����s���Ă݂�
				Object obj = GetColumnValueAtRow(this._sorce, this.editrow );
				if( obj is byte[] )
				{
					MemoryStream ms = new MemoryStream((byte[])obj);
					Image gazo = Image.FromStream(ms);
					if( gazo != null )
					{
						ImageViewer viewdlg = new ImageViewer();
						viewdlg.ViewImage = gazo;
						viewdlg.ShowDialog();
						return;
					}
				}


				ZoomDialog dlg  = new ZoomDialog();
				dlg.EditText = this.TextBox.Text;
				if( this.TextBox.ReadOnly == true )
				{
					dlg.IsDispOnly = true;
					dlg.LableName = "�l�Q��";
				}
				else
				{
					dlg.LableName = "�l�ҏW";
				}
				if( dlg.ShowDialog() == DialogResult.OK &&
					dlg.EditText != "" )
				{
					this.TextBox.Text = dlg.EditText;
					SetColumnValueAtRow(this._sorce, this.editrow, dlg.EditText);
				}
			}
			if( this.TextBox.ReadOnly == true )
			{
				return;
			}
			if( e.KeyCode == Keys.D1 &&
				e.Control == true &&
				e.Alt != true &&
				e.Shift != true )
			{
				EnterNullValue();
			}
			if( canSetEmptyString == true&&
				e.KeyCode == Keys.D2 &&
				e.Control == true &&
				e.Alt != true &&
				e.Shift != true )
			{
				this.TextBox.Text = this.NullText;
				SetColumnValueAtRow(this._sorce, this.editrow, "");
			}
		}
		protected override void Edit(CurrencyManager source,
			int rowNum, Rectangle bounds, bool readOnly,
			string instantText, bool cellIsVisible)
		{
			this._sorce = source;
			this.editrow = rowNum;
			base.Edit(source, rowNum, bounds, readOnly, instantText, cellIsVisible);
		}

		protected override void EnterNullValue()
		{
			this.TextBox.Text = this.NullText;
			SetColumnValueAtRow(this._sorce, this.editrow, DBNull.Value);
		}

		//Paint���\�b�h���I�[�o�[���C�h����
		protected override void Paint(Graphics g,
			Rectangle bounds,
			CurrencyManager source,
			int rowNum, 
			Brush backBrush,
			Brush foreBrush,
			bool alignToRight)
		{
			//�Z���̒l���擾����
			object cellValue =
				this.GetColumnValueAtRow(source, rowNum);
			if (cellValue == DBNull.Value)
			{
				backBrush = new SolidBrush(Color.FromArgb(0xbf,0xef,0xff));
			}
			else if( cellValue is string && 
				( 
				((string)cellValue).IndexOf("\r\n") >= 0 ||
				((string)cellValue).IndexOf("\n") >= 0 ) )
			{
				backBrush = new SolidBrush(Color.FromArgb(0xf4,0xb3,0xc2));
			}

			//��{�N���X��Paint���\�b�h���Ăяo��
			base.Paint(g, bounds, source, rowNum,
				backBrush, foreBrush, alignToRight);
		}
	}
}
