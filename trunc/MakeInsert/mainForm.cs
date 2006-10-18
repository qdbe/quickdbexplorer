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

namespace MakeInsert
{
	/// <summary>
	/// メインとなる画面
	/// DBの選択、オーナーの選択、テーブルの選択、処理の選択などのメインとなる処理を全て実装している
	/// </summary>
	public class MainForm : sqaoBaseForm
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
		private System.Windows.Forms.ListBox dbList;
		private System.Windows.Forms.ListBox fieldListbox;
		private System.Windows.Forms.ListBox ownerListbox;
		private System.Windows.Forms.ListBox tableList;
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
		private System.Windows.Forms.TextBox txtDspCount;
		private System.Windows.Forms.TextBox txtOutput;
		private System.Windows.Forms.TextBox txtSort;
		private System.Windows.Forms.TextBox txtWhere;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.ToolTip toolTip2;
		private System.Windows.Forms.ToolTip toolTip3;

		private Form5 Sqldlg2 = new Form5();
		private Form4 Sqldlg = new Form4();
		private DataSet dspdt = new DataSet();
		private ServerData svdata;
		private	Font	gfont;
		private	Color	gcolor;
		private Color	btnBackColor;
		private Color	btnForeColor;
		private	Form7 indexdlg = null;
		private	string	NumFormat;
		private	string	FloatFormat;
		private	string	DateFormat;

		/// <summary>
		///  接続先のサーバー名。表示用にのみ利用
		/// </summary>
		public string servername = "";

		/// <summary>
		/// 接続先サーバーの本当の名前。インスタンス名を含まない
		/// </summary>
		public string serverRealName = "";

		/// <summary>
		/// 接続先サーバーのインスタンス名
		/// </summary>
		public string instanceName = "";

		/// <summary>
		/// ログインID
		/// </summary>
		public string loginUid = "";

		/// <summary>
		/// ログイン用パスワード
		/// </summary>
		public string loginPasswd = "";

		/// <summary>
		/// 信頼関係接続を利用するか否か
		/// </summary>
		public bool IsUseTruse = false;

		/// <summary>
		/// DB接続情報
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
		
		/// <summary>
		/// メニュー情報
		/// </summary>
		public System.Windows.Forms.ContextMenu mainContextMenu;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="sv">前回最終処理の記憶値</param>
		public MainForm(ServerData sv)
		{
			//
			// Windows フォーム デザイナ サポートに必要です。
			//
			InitializeComponent();

			//
			// TODO: InitializeComponent 呼び出しの後に、コンストラクタ コードを追加してください。
			//
			svdata = sv;
			Sqldlg.SelectSql = "";
			Sqldlg2.SelectSql = "";
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
				if( this.sqlConnection1 != null )
				{
					this.sqlConnection1.Close();
					this.sqlConnection1.Dispose();
					this.sqlConnection1 = null;
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
			this.dbList = new System.Windows.Forms.ListBox();
			this.tableList = new System.Windows.Forms.ListBox();
			this.mainContextMenu = new System.Windows.Forms.ContextMenu();
			this.menuTableCopy = new System.Windows.Forms.MenuItem();
			this.menuTableCopyCsv = new System.Windows.Forms.MenuItem();
			this.menuInsert = new System.Windows.Forms.MenuItem();
			this.menuInsertDelete = new System.Windows.Forms.MenuItem();
			this.menuInsertNoFld = new System.Windows.Forms.MenuItem();
			this.menuInsertNoFldDelete = new System.Windows.Forms.MenuItem();
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
			this.txtWhere = new System.Windows.Forms.TextBox();
			this.txtSort = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.btnSelect = new System.Windows.Forms.Button();
			this.ownerListbox = new System.Windows.Forms.ListBox();
			this.btnDDL = new System.Windows.Forms.Button();
			this.dbGrid = new System.Windows.Forms.DataGrid();
			this.chkDspData = new System.Windows.Forms.CheckBox();
			this.grpDataDspMode = new System.Windows.Forms.GroupBox();
			this.txtDspCount = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.grpSysUserMode = new System.Windows.Forms.GroupBox();
			this.rdoNotDspSysUser = new System.Windows.Forms.RadioButton();
			this.rdoDspSysUser = new System.Windows.Forms.RadioButton();
			this.grpOutputMode = new System.Windows.Forms.GroupBox();
			this.btnReference = new System.Windows.Forms.Button();
			this.txtOutput = new System.Windows.Forms.TextBox();
			this.rdoOutFile = new System.Windows.Forms.RadioButton();
			this.rdoClipboard = new System.Windows.Forms.RadioButton();
			this.rdoOutFolder = new System.Windows.Forms.RadioButton();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.fieldListbox = new System.Windows.Forms.ListBox();
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
			this.menuMakeTab = new System.Windows.Forms.MenuItem();
			this.menuMakeTabDQ = new System.Windows.Forms.MenuItem();
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
			this.msgArea.Location = new System.Drawing.Point(176, 580);
			this.msgArea.Name = "msgArea";
			this.msgArea.Size = new System.Drawing.Size(652, 16);
			// 
			// dbList
			// 
			this.dbList.ItemHeight = 12;
			this.dbList.Location = new System.Drawing.Point(60, 16);
			this.dbList.Name = "dbList";
			this.dbList.Size = new System.Drawing.Size(160, 52);
			this.dbList.TabIndex = 0;
			this.dbList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dbList_KeyDown);
			this.dbList.SelectedIndexChanged += new System.EventHandler(this.dbList_SelectedIndexChanged);
			// 
			// tableList
			// 
			this.tableList.ContextMenu = this.mainContextMenu;
			this.tableList.HorizontalScrollbar = true;
			this.tableList.ItemHeight = 12;
			this.tableList.Location = new System.Drawing.Point(240, 16);
			this.tableList.Name = "tableList";
			this.tableList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.tableList.Size = new System.Drawing.Size(256, 304);
			this.tableList.TabIndex = 10;
			this.tableList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tableList_KeyDown);
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
			this.menuTableCopy.Text = "テーブル名コピー";
			this.menuTableCopy.Click += new System.EventHandler(this.menuTableCopy_Click);
			// 
			// menuTableCopyCsv
			// 
			this.menuTableCopyCsv.Index = 1;
			this.menuTableCopyCsv.Text = "テーブル名コピー カンマ付き";
			this.menuTableCopyCsv.Click += new System.EventHandler(this.menuTableCopyCsv_Click);
			// 
			// menuInsert
			// 
			this.menuInsert.Index = 2;
			this.menuInsert.Text = "INSERT文作成";
			this.menuInsert.Click += new System.EventHandler(this.insertmake);
			// 
			// menuInsertDelete
			// 
			this.menuInsertDelete.Index = 3;
			this.menuInsertDelete.Text = "INSERT文作成(DELETE文付き)";
			this.menuInsertDelete.Click += new System.EventHandler(this.insertmakeDelete);
			// 
			// menuInsertNoFld
			// 
			this.menuInsertNoFld.Index = 4;
			this.menuInsertNoFld.Text = "INSERT文作成(フィールドリストなし)";
			this.menuInsertNoFld.Click += new System.EventHandler(this.insertmakeNoField);
			// 
			// menuInsertNoFldDelete
			// 
			this.menuInsertNoFldDelete.Index = 5;
			this.menuInsertNoFldDelete.Text = "INSERT文作成(フィールドリストなし　DELETE文付き)";
			this.menuInsertNoFldDelete.Click += new System.EventHandler(this.insertmakeNoFieldDelete);
			// 
			// menuSeparater1
			// 
			this.menuSeparater1.Index = 6;
			this.menuSeparater1.Text = "-";
			// 
			// menuMakeFld
			// 
			this.menuMakeFld.Index = 7;
			this.menuMakeFld.Text = "フィールドリスト作成";
			this.menuMakeFld.Click += new System.EventHandler(this.makefldlist);
			// 
			// menuMakeFldCRLF
			// 
			this.menuMakeFldCRLF.Index = 8;
			this.menuMakeFldCRLF.Text = "フィールドリスト改行作成";
			this.menuMakeFldCRLF.Click += new System.EventHandler(this.makefldListLF);
			// 
			// menuMakeFldNoComma
			// 
			this.menuMakeFldNoComma.Index = 9;
			this.menuMakeFldNoComma.Text = "フィールドリストカンマなし作成";
			this.menuMakeFldNoComma.Click += new System.EventHandler(this.makefldListNoComma);
			// 
			// menuSeparater2
			// 
			this.menuSeparater2.Index = 10;
			this.menuSeparater2.Text = "-";
			// 
			// menuDDL
			// 
			this.menuDDL.Index = 11;
			this.menuDDL.Text = "簡易定義文生成";
			this.menuDDL.Click += new System.EventHandler(this.makeDDL);
			// 
			// menuDDLDrop
			// 
			this.menuDDLDrop.Index = 12;
			this.menuDDLDrop.Text = "簡易定義文生成 DROP文付き";
			this.menuDDLDrop.Click += new System.EventHandler(this.makeDDLDrop);
			// 
			// menuDDLPare
			// 
			this.menuDDLPare.Index = 13;
			this.menuDDLPare.Text = "簡易定義文生成([]付き)";
			this.menuDDLPare.Click += new System.EventHandler(this.makeDDLPare);
			// 
			// menuDDLDropPare
			// 
			this.menuDDLDropPare.Index = 14;
			this.menuDDLDropPare.Text = "簡易定義文生成( DROP []付き)";
			this.menuDDLDropPare.Click += new System.EventHandler(this.makeDDLDropPare);
			// 
			// menuSeparater3
			// 
			this.menuSeparater3.Index = 15;
			this.menuSeparater3.Text = "-";
			// 
			// menuSelect
			// 
			this.menuSelect.Index = 16;
			this.menuSelect.Text = "Select文生成";
			this.menuSelect.Click += new System.EventHandler(this.btnSelect_Click);
			// 
			// menuMakeCSV
			// 
			this.menuMakeCSV.Index = 17;
			this.menuMakeCSV.Text = "CSV作成";
			this.menuMakeCSV.Click += new System.EventHandler(this.makeCSV);
			// 
			// menuMakeCSVDQ
			// 
			this.menuMakeCSVDQ.Index = 18;
			this.menuMakeCSVDQ.Text = "CSV作成(”付き)";
			this.menuMakeCSVDQ.Click += new System.EventHandler(this.makeCSVQuote);
			// 
			// menuSeparater4
			// 
			this.menuSeparater4.Index = 21;
			this.menuSeparater4.Text = "-";
			// 
			// menuDepend
			// 
			this.menuDepend.Index = 22;
			this.menuDepend.Text = "依存関係出力";
			this.menuDepend.Click += new System.EventHandler(this.DependOutPut);
			// 
			// menuISQLW
			// 
			this.menuISQLW.Index = -1;
			this.menuISQLW.Text = "クエリアナライザ起動";
			this.menuISQLW.Click += new System.EventHandler(this.CallISQLW);
			// 
			// btnInsert
			// 
			this.btnInsert.Location = new System.Drawing.Point(508, 16);
			this.btnInsert.Name = "btnInsert";
			this.btnInsert.Size = new System.Drawing.Size(132, 24);
			this.btnInsert.TabIndex = 11;
			this.btnInsert.Text = "INSERT文作成";
			this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
			// 
			// btnFieldList
			// 
			this.btnFieldList.Location = new System.Drawing.Point(508, 44);
			this.btnFieldList.Name = "btnFieldList";
			this.btnFieldList.Size = new System.Drawing.Size(132, 24);
			this.btnFieldList.TabIndex = 12;
			this.btnFieldList.Text = "フィールドリスト作成";
			this.btnFieldList.Click += new System.EventHandler(this.btnFieldList_Click);
			// 
			// btnCSV
			// 
			this.btnCSV.Location = new System.Drawing.Point(508, 128);
			this.btnCSV.Name = "btnCSV";
			this.btnCSV.Size = new System.Drawing.Size(132, 24);
			this.btnCSV.TabIndex = 15;
			this.btnCSV.Text = "CSV作成";
			this.btnCSV.Click += new System.EventHandler(this.btnCSV_Click);
			// 
			// rdoDspView
			// 
			this.rdoDspView.Location = new System.Drawing.Point(8, 16);
			this.rdoDspView.Name = "rdoDspView";
			this.rdoDspView.Size = new System.Drawing.Size(88, 16);
			this.rdoDspView.TabIndex = 0;
			this.rdoDspView.Text = "表示する";
			this.rdoDspView.CheckedChanged += new System.EventHandler(this.rdoDspView_CheckedChanged);
			// 
			// grpViewMode
			// 
			this.grpViewMode.Controls.Add(this.rdoNotDspView);
			this.grpViewMode.Controls.Add(this.rdoDspView);
			this.grpViewMode.Location = new System.Drawing.Point(8, 220);
			this.grpViewMode.Name = "grpViewMode";
			this.grpViewMode.Size = new System.Drawing.Size(216, 40);
			this.grpViewMode.TabIndex = 3;
			this.grpViewMode.TabStop = false;
			this.grpViewMode.Text = "VIEWを一覧に";
			// 
			// rdoNotDspView
			// 
			this.rdoNotDspView.Location = new System.Drawing.Point(112, 16);
			this.rdoNotDspView.Name = "rdoNotDspView";
			this.rdoNotDspView.Size = new System.Drawing.Size(88, 16);
			this.rdoNotDspView.TabIndex = 1;
			this.rdoNotDspView.Text = "表示しない";
			// 
			// grpSortMode
			// 
			this.grpSortMode.Controls.Add(this.rdoSortOwnerTable);
			this.grpSortMode.Controls.Add(this.rdoSortTable);
			this.grpSortMode.Location = new System.Drawing.Point(8, 264);
			this.grpSortMode.Name = "grpSortMode";
			this.grpSortMode.Size = new System.Drawing.Size(216, 52);
			this.grpSortMode.TabIndex = 4;
			this.grpSortMode.TabStop = false;
			this.grpSortMode.Text = "ソート順";
			// 
			// rdoSortOwnerTable
			// 
			this.rdoSortOwnerTable.Location = new System.Drawing.Point(112, 16);
			this.rdoSortOwnerTable.Name = "rdoSortOwnerTable";
			this.rdoSortOwnerTable.Size = new System.Drawing.Size(88, 32);
			this.rdoSortOwnerTable.TabIndex = 1;
			this.rdoSortOwnerTable.Text = "オーナー名・テーブル名";
			// 
			// rdoSortTable
			// 
			this.rdoSortTable.Location = new System.Drawing.Point(8, 16);
			this.rdoSortTable.Name = "rdoSortTable";
			this.rdoSortTable.Size = new System.Drawing.Size(96, 32);
			this.rdoSortTable.TabIndex = 0;
			this.rdoSortTable.Text = "テーブル名のみ";
			this.rdoSortTable.CheckedChanged += new System.EventHandler(this.rdoSortTable_CheckedChanged);
			// 
			// txtWhere
			// 
			this.txtWhere.Location = new System.Drawing.Point(72, 480);
			this.txtWhere.Name = "txtWhere";
			this.txtWhere.Size = new System.Drawing.Size(144, 19);
			this.txtWhere.TabIndex = 7;
			this.txtWhere.Text = "";
			this.txtWhere.Leave += new System.EventHandler(this.txtWhere_Leave);
			// 
			// txtSort
			// 
			this.txtSort.Location = new System.Drawing.Point(72, 508);
			this.txtSort.Name = "txtSort";
			this.txtSort.Size = new System.Drawing.Size(144, 19);
			this.txtSort.TabIndex = 8;
			this.txtSort.Text = "";
			this.txtSort.Leave += new System.EventHandler(this.txtSort_Leave);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 480);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 16);
			this.label1.TabIndex = 13;
			this.label1.Text = "where";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 508);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 16);
			this.label2.TabIndex = 14;
			this.label2.Text = "order by";
			// 
			// btnSelect
			// 
			this.btnSelect.Location = new System.Drawing.Point(508, 72);
			this.btnSelect.Name = "btnSelect";
			this.btnSelect.Size = new System.Drawing.Size(132, 24);
			this.btnSelect.TabIndex = 13;
			this.btnSelect.Text = "Select 文生成";
			this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
			// 
			// ownerListbox
			// 
			this.ownerListbox.ItemHeight = 12;
			this.ownerListbox.Location = new System.Drawing.Point(60, 80);
			this.ownerListbox.Name = "ownerListbox";
			this.ownerListbox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.ownerListbox.Size = new System.Drawing.Size(160, 88);
			this.ownerListbox.TabIndex = 1;
			this.ownerListbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ownerListbox_KeyDown);
			this.ownerListbox.SelectedIndexChanged += new System.EventHandler(this.ownerListbox_SelectedIndexChanged);
			// 
			// btnDDL
			// 
			this.btnDDL.Location = new System.Drawing.Point(508, 100);
			this.btnDDL.Name = "btnDDL";
			this.btnDDL.Size = new System.Drawing.Size(132, 24);
			this.btnDDL.TabIndex = 14;
			this.btnDDL.Text = "簡易定義文生成";
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
			this.dbGrid.DataMember = "";
			this.dbGrid.Font = new System.Drawing.Font("ＭＳ ゴシック", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(128)));
			this.dbGrid.ForeColor = System.Drawing.Color.Black;
			this.dbGrid.GridLineColor = System.Drawing.Color.Silver;
			this.dbGrid.HeaderBackColor = System.Drawing.Color.Silver;
			this.dbGrid.HeaderFont = new System.Drawing.Font("ＭＳ ゴシック", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.dbGrid.HeaderForeColor = System.Drawing.Color.Black;
			this.dbGrid.LinkColor = System.Drawing.Color.Maroon;
			this.dbGrid.Location = new System.Drawing.Point(240, 364);
			this.dbGrid.Name = "dbGrid";
			this.dbGrid.ParentRowsBackColor = System.Drawing.Color.Silver;
			this.dbGrid.ParentRowsForeColor = System.Drawing.Color.Black;
			this.dbGrid.SelectionBackColor = System.Drawing.Color.Maroon;
			this.dbGrid.SelectionForeColor = System.Drawing.Color.White;
			this.dbGrid.Size = new System.Drawing.Size(672, 212);
			this.dbGrid.TabIndex = 24;
			this.dbGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.dbGrid_Paint);
			// 
			// chkDspData
			// 
			this.chkDspData.Location = new System.Drawing.Point(24, 548);
			this.chkDspData.Name = "chkDspData";
			this.chkDspData.Size = new System.Drawing.Size(52, 24);
			this.chkDspData.TabIndex = 9;
			this.chkDspData.Text = "表示";
			this.chkDspData.CheckedChanged += new System.EventHandler(this.chkDspData_CheckedChanged);
			// 
			// grpDataDspMode
			// 
			this.grpDataDspMode.Controls.Add(this.txtDspCount);
			this.grpDataDspMode.Controls.Add(this.label3);
			this.grpDataDspMode.Location = new System.Drawing.Point(8, 532);
			this.grpDataDspMode.Name = "grpDataDspMode";
			this.grpDataDspMode.Size = new System.Drawing.Size(216, 44);
			this.grpDataDspMode.TabIndex = 9;
			this.grpDataDspMode.TabStop = false;
			this.grpDataDspMode.Text = "データグリッド";
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
			this.txtDspCount.TabIndex = 0;
			this.txtDspCount.Text = "1000";
			this.txtDspCount.TextChanged += new System.EventHandler(this.txtDspCount_TextChanged);
			this.txtDspCount.Leave += new System.EventHandler(this.txtDspCount_Leave);
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label3.Location = new System.Drawing.Point(72, 20);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 16);
			this.label3.TabIndex = 0;
			this.label3.Text = "表示件数";
			// 
			// grpSysUserMode
			// 
			this.grpSysUserMode.Controls.Add(this.rdoNotDspSysUser);
			this.grpSysUserMode.Controls.Add(this.rdoDspSysUser);
			this.grpSysUserMode.Location = new System.Drawing.Point(8, 176);
			this.grpSysUserMode.Name = "grpSysUserMode";
			this.grpSysUserMode.Size = new System.Drawing.Size(216, 40);
			this.grpSysUserMode.TabIndex = 2;
			this.grpSysUserMode.TabStop = false;
			this.grpSysUserMode.Text = "システムユーザーを";
			// 
			// rdoNotDspSysUser
			// 
			this.rdoNotDspSysUser.Checked = true;
			this.rdoNotDspSysUser.Location = new System.Drawing.Point(112, 16);
			this.rdoNotDspSysUser.Name = "rdoNotDspSysUser";
			this.rdoNotDspSysUser.Size = new System.Drawing.Size(88, 16);
			this.rdoNotDspSysUser.TabIndex = 1;
			this.rdoNotDspSysUser.TabStop = true;
			this.rdoNotDspSysUser.Text = "表示しない";
			this.rdoNotDspSysUser.CheckedChanged += new System.EventHandler(this.rdoNotDspSysUser_CheckedChanged);
			// 
			// rdoDspSysUser
			// 
			this.rdoDspSysUser.Location = new System.Drawing.Point(8, 16);
			this.rdoDspSysUser.Name = "rdoDspSysUser";
			this.rdoDspSysUser.Size = new System.Drawing.Size(88, 16);
			this.rdoDspSysUser.TabIndex = 0;
			this.rdoDspSysUser.Text = "表示する";
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
			this.grpOutputMode.TabIndex = 5;
			this.grpOutputMode.TabStop = false;
			this.grpOutputMode.Text = "出力先";
			// 
			// btnReference
			// 
			this.btnReference.Location = new System.Drawing.Point(168, 52);
			this.btnReference.Name = "btnReference";
			this.btnReference.Size = new System.Drawing.Size(40, 20);
			this.btnReference.TabIndex = 4;
			this.btnReference.Text = "参照";
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
			this.rdoOutFile.Text = "単独ファイル";
			this.rdoOutFile.CheckedChanged += new System.EventHandler(this.rdoOutFile_CheckedChanged);
			// 
			// rdoClipboard
			// 
			this.rdoClipboard.Location = new System.Drawing.Point(8, 12);
			this.rdoClipboard.Name = "rdoClipboard";
			this.rdoClipboard.Size = new System.Drawing.Size(88, 16);
			this.rdoClipboard.TabIndex = 0;
			this.rdoClipboard.Text = "クリップボード";
			this.rdoClipboard.CheckedChanged += new System.EventHandler(this.rdoClipboard_CheckedChanged);
			// 
			// rdoOutFolder
			// 
			this.rdoOutFolder.Location = new System.Drawing.Point(104, 32);
			this.rdoOutFolder.Name = "rdoOutFolder";
			this.rdoOutFolder.Size = new System.Drawing.Size(88, 16);
			this.rdoOutFolder.TabIndex = 2;
			this.rdoOutFolder.Text = "複数ファイル";
			this.rdoOutFolder.CheckedChanged += new System.EventHandler(this.rdoOutFolder_CheckedChanged);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 28);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(48, 32);
			this.label4.TabIndex = 25;
			this.label4.Text = "DB";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(4, 100);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(48, 32);
			this.label5.TabIndex = 25;
			this.label5.Text = "owner/Role";
			// 
			// fieldListbox
			// 
			this.fieldListbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.fieldListbox.ContextMenu = this.fldContextMenu;
			this.fieldListbox.HorizontalScrollbar = true;
			this.fieldListbox.ItemHeight = 12;
			this.fieldListbox.Location = new System.Drawing.Point(656, 40);
			this.fieldListbox.Name = "fieldListbox";
			this.fieldListbox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.fieldListbox.Size = new System.Drawing.Size(240, 268);
			this.fieldListbox.TabIndex = 23;
			this.fieldListbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fieldListbox_KeyDown);
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
			this.fldmenuCopy.Text = "コピー";
			this.fldmenuCopy.Click += new System.EventHandler(this.fldmenuCopy_Click);
			// 
			// fldmenuCopyNoCRLF
			// 
			this.fldmenuCopyNoCRLF.Index = 1;
			this.fldmenuCopyNoCRLF.Text = "改行なしコピー";
			this.fldmenuCopyNoCRLF.Click += new System.EventHandler(this.fldmenuCopyNoCRLF_Click);
			// 
			// fldmenuCopyNoComma
			// 
			this.fldmenuCopyNoComma.Index = 2;
			this.fldmenuCopyNoComma.Text = "コピーカンマなし";
			this.fldmenuCopyNoComma.Click += new System.EventHandler(this.fldmenuCopyNoComma_Click);
			// 
			// fldmenuCopyNoCRLFNoComma
			// 
			this.fldmenuCopyNoCRLFNoComma.Index = 3;
			this.fldmenuCopyNoCRLFNoComma.Text = "コピー改行・カンマなし";
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
			this.grpCharaSet.TabIndex = 6;
			this.grpCharaSet.TabStop = false;
			this.grpCharaSet.Text = "出力文字コード";
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
			this.chkDspFieldAttr.Location = new System.Drawing.Point(652, 12);
			this.chkDspFieldAttr.Name = "chkDspFieldAttr";
			this.chkDspFieldAttr.Size = new System.Drawing.Size(244, 20);
			this.chkDspFieldAttr.TabIndex = 22;
			this.chkDspFieldAttr.Text = "フィールド属性を表示";
			this.chkDspFieldAttr.CheckedChanged += new System.EventHandler(this.chkDspFieldAttr_CheckedChanged);
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label6.Font = new System.Drawing.Font("MS UI Gothic", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(128)));
			this.label6.Location = new System.Drawing.Point(4, 588);
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
			this.btnQuerySelect.Size = new System.Drawing.Size(132, 24);
			this.btnQuerySelect.TabIndex = 18;
			this.btnQuerySelect.Text = "クエリ指定結果表示";
			this.btnQuerySelect.Click += new System.EventHandler(this.btnQuerySelect_Click);
			// 
			// btnDataUpdate
			// 
			this.btnDataUpdate.Location = new System.Drawing.Point(508, 296);
			this.btnDataUpdate.Name = "btnDataUpdate";
			this.btnDataUpdate.Size = new System.Drawing.Size(132, 24);
			this.btnDataUpdate.TabIndex = 21;
			this.btnDataUpdate.Text = "データ更新";
			this.btnDataUpdate.Click += new System.EventHandler(this.btnDataUpdate_Click);
			// 
			// btnDataEdit
			// 
			this.btnDataEdit.Location = new System.Drawing.Point(508, 268);
			this.btnDataEdit.Name = "btnDataEdit";
			this.btnDataEdit.Size = new System.Drawing.Size(132, 24);
			this.btnDataEdit.TabIndex = 20;
			this.btnDataEdit.Text = "データ編集";
			this.btnDataEdit.Click += new System.EventHandler(this.btnDataEdit_Click);
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(244, 328);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(372, 16);
			this.label7.TabIndex = 31;
			this.label7.Text = "見出しに★がある列はNULL可です。NULLのセルは水色に着色されます。";
			// 
			// btnGridFormat
			// 
			this.btnGridFormat.Location = new System.Drawing.Point(652, 320);
			this.btnGridFormat.Name = "btnGridFormat";
			this.btnGridFormat.Size = new System.Drawing.Size(132, 20);
			this.btnGridFormat.TabIndex = 25;
			this.btnGridFormat.Text = "書式グリッド表示指定";
			this.btnGridFormat.Click += new System.EventHandler(this.btnGridFormat_Click);
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(244, 348);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(392, 16);
			this.label8.TabIndex = 32;
			this.label8.Text = "NULLを入力するにはCtrl+1 を、空文字列を入力するにはCtrl+2を押下します。";
			// 
			// btnIndex
			// 
			this.btnIndex.Location = new System.Drawing.Point(508, 156);
			this.btnIndex.Name = "btnIndex";
			this.btnIndex.Size = new System.Drawing.Size(132, 23);
			this.btnIndex.TabIndex = 16;
			this.btnIndex.Text = "INDEX情報表示";
			this.btnIndex.Click += new System.EventHandler(this.btnIndex_Click);
			// 
			// btnRedisp
			// 
			this.btnRedisp.Location = new System.Drawing.Point(652, 344);
			this.btnRedisp.Name = "btnRedisp";
			this.btnRedisp.Size = new System.Drawing.Size(96, 20);
			this.btnRedisp.TabIndex = 26;
			this.btnRedisp.Text = "グリッド再描画";
			this.btnRedisp.Click += new System.EventHandler(this.Redisp_Click);
			// 
			// btnTmpAllDsp
			// 
			this.btnTmpAllDsp.Location = new System.Drawing.Point(752, 344);
			this.btnTmpAllDsp.Name = "btnTmpAllDsp";
			this.btnTmpAllDsp.Size = new System.Drawing.Size(148, 20);
			this.btnTmpAllDsp.TabIndex = 27;
			this.btnTmpAllDsp.Text = "一時的に全データを表示";
			this.btnTmpAllDsp.Click += new System.EventHandler(this.btnTmpAllDsp_Click);
			// 
			// btnEtc
			// 
			this.btnEtc.Location = new System.Drawing.Point(508, 184);
			this.btnEtc.Name = "btnEtc";
			this.btnEtc.Size = new System.Drawing.Size(132, 23);
			this.btnEtc.TabIndex = 17;
			this.btnEtc.Text = "その他";
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
																						   this.menuRecordCount});
			// 
			// menuQuery
			// 
			this.menuQuery.Index = 0;
			this.menuQuery.Text = "簡易クエリ実行（Select以外）";
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
			this.menuISQL.Text = "クエリアナライザ起動";
			this.menuISQL.Click += new System.EventHandler(this.CallISQLW);
			// 
			// menuProfile
			// 
			this.menuProfile.Index = 3;
			this.menuProfile.Text = "プロファイラ起動";
			this.menuProfile.Click += new System.EventHandler(this.CallProfile);
			// 
			// menuEPM
			// 
			this.menuEPM.Index = 4;
			this.menuEPM.Text = "エンタープライズマネージャー起動";
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
			this.menuDependBtn.Text = "依存関係出力";
			this.menuDependBtn.Click += new System.EventHandler(this.DependOutPut);
			// 
			// menuRecordCount
			// 
			this.menuRecordCount.Index = 7;
			this.menuRecordCount.Text = "データ件数出力";
			this.menuRecordCount.Click += new System.EventHandler(this.RecordCountOutPut);
			// 
			// menuMakeTab
			// 
			this.menuMakeTab.Index = 19;
			this.menuMakeTab.Text = "Tab区切出力";
			this.menuMakeTab.Click += new System.EventHandler(this.menuMakeTab_Click);
			// 
			// menuMakeTabDQ
			// 
			this.menuMakeTabDQ.Index = 20;
			this.menuMakeTabDQ.Text = "Tab区切出力(\"付き)";
			this.menuMakeTabDQ.Click += new System.EventHandler(this.menuMakeTabDQ_Click);
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(928, 597);
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
			this.Name = "MainForm";
			this.ShowInTaskbar = false;
			this.Text = "DataBase選択";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
			this.Load += new System.EventHandler(this.MainForm_Load);
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
			this.Controls.SetChildIndex(this.msgArea, 0);
			this.Controls.SetChildIndex(this.btnEtc, 0);
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

			// 前回の値を元にDB先を変更する
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

			// ボタンの表示色を記憶しておく
			this.btnBackColor = this.btnDataEdit.BackColor;
			this.btnForeColor = this.btnDataEdit.ForeColor;
		}

		private void dbList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if( this.dbList.SelectedItems.Count != 0 )
			{
				svdata.lastdb = (string)this.dbList.SelectedItem;
			}
			dsplist2();
			displistowner();
			if( svdata.dbopt[svdata.lastdb] != null )
			{
				// 該当DBの最後に選択したユーザーを選択する
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
			if( svdata.outdest[svdata.lastdb] != null )
			{
				// 該当DBの最後の出力先をセットする
				switch( (int)svdata.outdest[svdata.lastdb] )
				{
					case	0:
						//クリップボード
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
				//標準はクリップボード
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
			
		}

		// INSERT文生成
		private void btnInsert_Click(object sender, System.EventArgs e)
		{
			MenuItem[] list = new MenuItem[] {
												 this.menuInsert,
												 this.menuInsertDelete,
												 this.menuInsertNoFld,
												 this.menuInsertNoFldDelete
											 };
			foreach( MenuItem m in this.mainContextMenu.MenuItems )
			{
				bool isFound = false;
				foreach( MenuItem mm in list )
				{
					if( m == mm )
					{
						isFound = true;
					}
				}
				if( isFound == false )
				{
					m.Visible = false;
				}
			}

			this.mainContextMenu.Show(this.btnInsert,new Point(0,0));
			foreach( MenuItem m in this.mainContextMenu.MenuItems )
			{
				m.Visible = true;
			}
		}
		
		// フィールドリスト生成
		private void btnFieldList_Click(object sender, System.EventArgs e)
		{
			MenuItem[] list = new MenuItem[] {
												 this.menuMakeFld,
												 this.menuMakeFldCRLF,
												 this.menuMakeFldNoComma
											 };
			foreach( MenuItem m in this.mainContextMenu.MenuItems )
			{
				bool isFound = false;
				foreach( MenuItem mm in list )
				{
					if( m == mm )
					{
						isFound = true;
					}
				}
				if( isFound == false )
				{
					m.Visible = false;
				}
			}
			this.mainContextMenu.Show(this.btnFieldList,new Point(0,0));
			foreach( MenuItem m in this.mainContextMenu.MenuItems )
			{
				m.Visible = true;
			}
		}

		// CSV生成
		private void btnCSV_Click(object sender, System.EventArgs e)
		{
			MenuItem[] list = new MenuItem[] {
												 this.menuMakeCSV,
												 this.menuMakeCSVDQ,
												 this.menuMakeTab,
												 this.menuMakeTabDQ
											 };
			foreach( MenuItem m in this.mainContextMenu.MenuItems )
			{
				bool isFound = false;
				foreach( MenuItem mm in list )
				{
					if( m == mm )
					{
						isFound = true;
					}
				}
				if( isFound == false )
				{
					m.Visible = false;
				}
			}
			this.mainContextMenu.Show(this.btnCSV,new Point(0,0));
			foreach( MenuItem m in this.mainContextMenu.MenuItems )
			{
				m.Visible = true;
			}
		}

		// 定義文生成
		private void btnDDL_Click(object sender, System.EventArgs e)
		{
			MenuItem[] list = new MenuItem[] {
												 this.menuDDL,
												 this.menuDDLDrop
											 };
			foreach( MenuItem m in this.mainContextMenu.MenuItems )
			{
				bool isFound = false;
				foreach( MenuItem mm in list )
				{
					if( m == mm )
					{
						isFound = true;
					}
				}
				if( isFound == false )
				{
					m.Visible = false;
				}
			}
			this.mainContextMenu.Show(this.btnDDL,new Point(0,0));
			foreach( MenuItem m in this.mainContextMenu.MenuItems )
			{
				m.Visible = true;
			}
		}

		private void insertmake(object sender, System.EventArgs e)
		{
			this.CreInsert(true,false);
		}

		private bool CheckFileSpec()
		{
			if( this.rdoOutFile.Checked == true ) 
			{
				if( this.txtOutput.Text == "" )
				{
					this.saveFileDialog1.CreatePrompt = true;
					this.saveFileDialog1.Filter = "SQL|*.sql|CSV|*.csv|TXT|*.txt|全て|*.*";
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
						MessageBox.Show("指定されたファイル名はフォルダを指しています。ファイル名を指定してください。処理を中断します");
						return false;
					}
				}
				if( this.txtOutput.Text == "" )
				{
					MessageBox.Show("ファイル名が指定されていないので、処理を中断します");
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
						MessageBox.Show("フォルダ名は指定されていますが、フォルダではありません。処理を中断します");
						return false;
					}
					else if( !d.Exists )
					{
						Directory.CreateDirectory(this.txtOutput.Text);
						DirectoryInfo ff = new DirectoryInfo(this.txtOutput.Text);
						if( !ff.Exists )
						{
							MessageBox.Show("フォルダ名は指定されていますが、作成できませんでした。処理を中断します");
							return false;
						}
					}
				}
				else
				{
					MessageBox.Show("フォルダ名が指定されませんでした。処理を中断します");
					return false;
				}
				
			}
			return true;
		}

		private void CreInsert(bool fieldlst, bool deletefrom)
		{
			try
			{
				this.InitErrMessage();
				// insert 文の作成
				if( this.tableList.SelectedItems.Count == 0 )
				{
					return;
				}
				if( this.tableList.SelectedItems.Count > 1 &&
					this.txtWhere.Text != null &&
					this.txtWhere.Text != "" )
				{
					if( MessageBox.Show("複数テーブルに同一の where 句を適用しますか？","確認",System.Windows.Forms.MessageBoxButtons.YesNo) 
						== System.Windows.Forms.DialogResult.No )
					{
						return;
					}
				}
				if( this.tableList.SelectedItems.Count > 1 &&
					this.txtSort.Text != null &&
					this.txtSort.Text != "" )
				{
					if( MessageBox.Show("複数テーブルに同一の order by 句を適用しますか？","確認",System.Windows.Forms.MessageBoxButtons.YesNo) 
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
					if( this.txtWhere.Text != "" )
					{
						sqlstr += " where " + this.txtWhere.Text;
					}
					if( this.txtSort.Text != "" )
					{
						sqlstr += " order by " + this.txtSort.Text;
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

					if( deletefrom == true && dr.HasRows == true)
					{
						wr.Write("delete from  ");
						string delimStr = ".";
						string []tbstr = tbname.Split(delimStr.ToCharArray(), 2);
						wr.Write(string.Format("[{0}].[{1}]",tbstr[0],tbstr[1]));
						if( this.txtWhere.Text != "" )
						{
							wr.Write( " where {0}", this.txtWhere.Text );

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
							else if( fldtypename == "nvarchar" ||
								fldtypename == "varchar" ||
								fldtypename == "char" ||
								fldtypename == "nchar" ||
								fldtypename == "ntext")
							{
								// 文字列
								if( dr.GetString(i).Equals("") )
								{
									wr.Write( "''" );
								}
								else
								{
									wr.Write( "'{0}'", dr.GetString(i) );
								}
							}
							else
							{
								wr.Write( dr.GetValue(i).ToString() );
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
							// ファイルを作成したが、空になったので削除する	
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
					MessageBox.Show("対象データがありませんでした");
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
					MessageBox.Show("処理を完了しました");
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

				// insert 文の作成
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

				MessageBox.Show( "処理を終了しました" );
			}
			catch( Exception exp )
			{
				this.SetErrorMessage(exp);
			}
		}

		// フィールドのリストを表示する
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
				// 1件のみ選択されている場合、データ表示部に、該当テーブルのデータを表示する
				DspData(this.tableList.SelectedItem.ToString());
			}
			else
			{
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

			if( this.tableList.SelectedItems.Count == 0 )
			{
				return;
			}
			if( this.tableList.SelectedItems.Count > 1 &&
				this.txtWhere.Text != null &&
				this.txtWhere.Text != "" )
			{
				if( MessageBox.Show("複数テーブルに同一の where 句を適用しますか？","確認",System.Windows.Forms.MessageBoxButtons.YesNo) 
					== System.Windows.Forms.DialogResult.No )
				{
					return;
				}
			}
			if( this.tableList.SelectedItems.Count > 1 &&
				this.txtSort.Text != null &&
				this.txtSort.Text != "" )
			{
				if( MessageBox.Show("複数テーブルに同一の order by 句を適用しますか？","確認",System.Windows.Forms.MessageBoxButtons.YesNo) 
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
					if( this.txtWhere.Text != "" )
					{
						sqlstr += " where " + this.txtWhere.Text;
					}
					if( this.txtSort.Text != "" )
					{
						sqlstr += " order by " + this.txtSort.Text;
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

					// 先頭行は列見出し
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

					// データの書き出し
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
								fldtypename == "varchar" ||
								fldtypename == "char" ||
								fldtypename == "nchar" ||
								fldtypename == "ntext")
							{
								// 文字列
								if( !dr.GetString(i).Equals("") )
								{
									if( isdquote )
									{
										wr.Write( "\"{0}\"", dr.GetString(i) );
									}
									else
									{
										wr.Write( dr.GetString(i) );
									}
								}
							}
							else
							{
								wr.Write( dr.GetValue(i).ToString() );
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
							// ファイルを作成したが、空になったので削除する	
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
					MessageBox.Show("対象データがありませんでした");
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
					MessageBox.Show("処理を完了しました");
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
			dsplist2();
		}

		private void rdoSortTable_CheckedChanged(object sender, System.EventArgs e)
		{
			dsplist2();
		}

		private void insertmakeDelete(object sender, System.EventArgs e)
		{
			this.CreInsert( true, true );
		}

		private void insertmakeNoField(object sender, System.EventArgs e)
		{
			this.CreInsert(false,false );
		}

		private void insertmakeNoFieldDelete(object sender, System.EventArgs e)
		{
			this.CreInsert(false,true);
		}

		private void btnSelect_Click(object sender, System.EventArgs e)
		{
			// select 文の作成

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
					if( this.txtWhere.Text != "" )
					{
						wr.Write(" where {0}{1}", this.txtWhere.Text,wr.NewLine);
					}
					if( this.txtSort.Text != "" )
					{
						wr.Write(" order by {0}{1}", this.txtSort.Text,wr.NewLine);
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

				MessageBox.Show( "処理を終了しました" );
			}
			catch( Exception exp )
			{
				this.SetErrorMessage(exp);
			}
		}

		private void ownerListbox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if( this.ownerListbox.SelectedItem != null )
			{
				// 選択したDBの最終オーナーを記録する
				svdata.dbopt[svdata.lastdb] = (string)this.ownerListbox.SelectedItem;
			}
			dsplist2();
		}

		private void dsplist2()
		{
			SqlDataReader dr = null;
			SqlCommand	cm = new SqlCommand();

			this.InitErrMessage();

			try 
			{
				if( this.dbList.SelectedItem == null )
				{
					return ;
				}
				this.sqlConnection1.ChangeDatabase((String)this.dbList.SelectedItem);
				
				// listbox2 にテーブル一覧を表示

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
					// 選択があれば、そのOWNERのみのテーブルを表示する
					string ownerlist = "";
					foreach( String owname in this.ownerListbox.SelectedItems )
					{
						if( owname == "全て" )
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
						//フィールド名
						if( usekakko )
						{
							wr.Write("\t[{0}]", ds.Tables[tbname].Rows[i][0]);
						}
						else
						{
							wr.Write("\t{0}", ds.Tables[tbname].Rows[i][0]);
						}
						wr.Write("\t");
						// 型
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
				MessageBox.Show("処理を完了しました");
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
				// 1件のみ選択されている場合、データ表示部に、該当テーブルのデータを表示する
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

				int	maxlines;
				if( this.txtDspCount.Text != "" )
				{
					maxlines = int.Parse(this.txtDspCount.Text);
				}
				else
				{
					maxlines = 0;
				}
				if( isAllDsp == true )
				{
					maxlines = 0;
				}

				// データの内容を取得し、表示する
				string sqlstr;
				sqlstr = "select ";

				if( maxlines != 0 )
				{
					sqlstr += " TOP " + this.txtDspCount.Text;
				}

				sqlstr += string.Format(" * from {0}",gettbname(tbname));
				if( this.txtWhere.Text != "" )
				{
					sqlstr += " where " + this.txtWhere.Text;
				}
				if( this.txtSort.Text != "" )
				{
					sqlstr += " order by " + this.txtSort.Text;
				}
				SqlDataAdapter da = new SqlDataAdapter(sqlstr, this.sqlConnection1);
				dspdt = new DataSet();
				dspdt.CaseSensitive = true;
				da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
				da.Fill(dspdt, "aaaa");

				//新しいDataGridTableStyleの作成
				DataGridTableStyle ts = new DataGridTableStyle();
				//マップ名を指定する
				ts.MappingName = "aaaa";

				MyDataGridTextBoxColumn cs;
				foreach( DataColumn col in dspdt.Tables[0].Columns )
				{
					//列スタイルにMyDataGridTextBoxColumnを使う
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
					//マップ名を指定する
					cs.MappingName = col.ColumnName;
					if( col.AllowDBNull == true )
					{
						cs.HeaderText = "★"+col.ColumnName;
					}
					else
					{
						cs.HeaderText = col.ColumnName;
					}
					
					//DataGridTableStyleに追加する
					ts.GridColumnStyles.Add(cs);
				}

				//テーブルスタイルをDataGridに追加する
				this.dbGrid.TableStyles.Clear();
				this.dbGrid.TableStyles.Add(ts);



				this.dbGrid.AllowSorting = true;
				this.toolTip3.SetToolTip(this.dbGrid,sqlstr);
				this.dbGrid.SetDataBinding(dspdt, "aaaa");
				this.dbGrid.Show();
				this.btnDataEdit.Text = "データ編集";
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
				// 1件のみ選択されている場合、データ表示部に、該当テーブルのデータを表示する
				DspData(this.tableList.SelectedItem.ToString());
			}
			else
			{
				DspData("");
			}
		}

		private void txtWhere_Leave(object sender, System.EventArgs e)
		{
			if( this.chkDspData.CheckState == CheckState.Checked &&
				this.tableList.SelectedItems.Count == 1 )
			{
				// 1件のみ選択されている場合、データ表示部に、該当テーブルのデータを表示する
				DspData(this.tableList.SelectedItem.ToString());
			}
			else
			{
				DspData("");
			}
		}

		private void txtSort_Leave(object sender, System.EventArgs e)
		{
			if( this.chkDspData.CheckState == CheckState.Checked &&
				this.tableList.SelectedItems.Count == 1 )
			{
				// 1件のみ選択されている場合、データ表示部に、該当テーブルのデータを表示する
				DspData(this.tableList.SelectedItem.ToString());
			}
			else
			{
				DspData("");
			}
		}

		private void rdoDspSysUser_CheckedChanged(object sender, System.EventArgs e)
		{
			dsplist2();
			displistowner();
		}

		private void displistowner()
		{
			SqlDataReader dr = null;
			SqlCommand	cm = new SqlCommand();
			
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
				this.ownerListbox.Items.Add("全て");
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
			if( this.chkDspData.CheckState == CheckState.Checked ){
				svdata.showgrid[svdata.lastdb] = 1;
			}
			else{
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
				// 単独ファイルの参照指定
				
				this.saveFileDialog1.CreatePrompt = true;
				this.saveFileDialog1.Filter = "SQL|*.sql|CSV|*.csv|TXT|*.txt|全て|*.*";
				DialogResult ret = this.saveFileDialog1.ShowDialog();
				if( ret == DialogResult.OK )
				{
					this.txtOutput.Text = this.saveFileDialog1.FileName;
				}
			}
			else
			{
				// 複数ファイルのディレクトリ参照指定
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

		private void fieldListbox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if( e.KeyCode == Keys.C &&
				e.Control == true &&
				e.Alt != true &&
				e.Shift != true )
			{
				copyfldlist(true,true);
			}
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

		private void tableList_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if( e.KeyCode == Keys.C &&
				e.Control == true &&
				e.Alt != true &&
				e.Shift != true )
			{
				copytablename(false);
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

					//新しいDataGridTableStyleの作成
					DataGridTableStyle ts = new DataGridTableStyle();
					//マップ名を指定する
					ts.MappingName = "aaaa";

					MyDataGridTextBoxColumn cs;
					foreach( DataColumn col in dspdt.Tables[0].Columns )
					{
						//列スタイルにMyDataGridTextBoxColumnを使う
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
						//マップ名を指定する
						cs.MappingName = col.ColumnName;
						if( col.AllowDBNull == true )
						{
							cs.HeaderText = "★"+col.ColumnName;
						}
						else
						{
							cs.HeaderText = col.ColumnName;
						}
					
						//DataGridTableStyleに追加する
						ts.GridColumnStyles.Add(cs);
					}

					//テーブルスタイルをDataGridに追加する
					this.dbGrid.TableStyles.Clear();
					this.dbGrid.TableStyles.Add(ts);

					this.dbGrid.ReadOnly = true;
					this.btnDataEdit.BackColor = this.btnBackColor;
					this.btnDataEdit.ForeColor = this.btnForeColor;
					this.btnTmpAllDsp.BackColor = this.btnBackColor;
					this.btnTmpAllDsp.ForeColor = this.btnForeColor;
					this.btnDataEdit.Text = "データ編集";
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
					MessageBox.Show("本当に更新してよろしいですか","",MessageBoxButtons.YesNo) == DialogResult.Yes
					)
				{
					// 1件のみ選択されている場合、データ表示部に、該当テーブルのデータを表示する
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
					if( this.txtWhere.Text != "" )
					{
						sqlstr += " where " + this.txtWhere.Text;
					}
					if( this.txtSort.Text != "" )
					{
						sqlstr += " order by " + this.txtSort.Text;
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
					// 編集可にする
					this.dbGrid.ReadOnly = false;
					this.btnDataEdit.Text = "データ編集終了";
					this.btnDataEdit.ForeColor = Color.WhiteSmoke;
					this.btnDataEdit.BackColor = Color.Navy;
				}
				else
				{
					// 編集不可にする
					if( this.dspdt.Tables["aaaa"].GetChanges() == null ||
						this.dspdt.Tables["aaaa"].GetChanges().Rows.Count == 0 )
					{
						this.dbGrid.ReadOnly = true;
						this.btnDataEdit.Text = "データ編集";
						this.btnDataEdit.BackColor = this.btnBackColor;
						this.btnDataEdit.ForeColor = this.btnForeColor;
					}
					else
					{
						// 変更があった
						if( MessageBox.Show("変更を破棄してもよろしいですか?","",MessageBoxButtons.YesNo) == DialogResult.Yes )
						{
							this.dspdt.Tables["aaaa"].RejectChanges();
							this.dbGrid.SetDataBinding(dspdt, "aaaa");
							this.dbGrid.Show();
							this.dbGrid.ReadOnly = true;
							this.btnDataEdit.Text = "データ編集";
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
			Form6 dlg = new Form6();
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

		private void dbList_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			// DB名のコピー
			if( e.KeyCode == Keys.C &&
				e.Control == true &&
				e.Alt != true &&
				e.Shift != true )
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
		}

		private void ownerListbox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			// DB名のコピー
			if( e.KeyCode == Keys.C &&
				e.Control == true &&
				e.Alt != true &&
				e.Shift != true )
			{
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
					string msg = "";
					if( Sqldlg2.hasReturn == true )
					{
						object ret = cm.ExecuteScalar();
						tran.Commit();
						msg = string.Format("処理が終了しました。\r\nリターン値は [{0}] です", ret.ToString() );
					}
					else
					{
						int cnt = cm.ExecuteNonQuery();
						tran.Commit();
						msg = string.Format("処理が終了しました。\r\n影響した件数は {0} 件です", cnt );
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

		private void Redisp_Click(object sender, System.EventArgs e)
		{
			//再描画ボタン押下
			if( this.chkDspData.CheckState == CheckState.Checked &&
				this.tableList.SelectedItems.Count == 1 )
			{
				// 1件のみ選択されている場合、データ表示部に、該当テーブルのデータを表示する
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
				indexdlg = new Form7();
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
			//再描画ボタン押下
			if( this.chkDspData.CheckState == CheckState.Checked &&
				this.tableList.SelectedItems.Count == 1 )
			{
				// 1件のみ選択されている場合、データ表示部に、該当テーブルのデータを表示する
				DspData(this.tableList.SelectedItem.ToString(),true);
				this.btnTmpAllDsp.ForeColor = Color.WhiteSmoke;
				this.btnTmpAllDsp.BackColor = Color.Navy;
			}
			else
			{
				DspData("");
			}
		}

		/// <summary>
		/// 選択されたテーブルに関する依存関係を出力する
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DependOutPut(object sender, System.EventArgs e)
		{
			SqlCommand	cm = new SqlCommand();

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
					wr.Write("テーブル名");
					wr.Write(",依存関係先名称");
					wr.Write(",種類");
					wr.Write(",更新あり");
					wr.Write(",selectでの利用");
					wr.Write(",従属性が存在する列またはパラメータ");
					wr.Write(wr.NewLine);
				}
				else if( this.rdoOutFile.Checked == true ) 
				{
					StreamWriter sw = new StreamWriter(this.txtOutput.Text,false, GetEncode());
					sw.AutoFlush = false;
					wr = sw;
					fname.Append(this.txtOutput.Text);
					wr.Write("テーブル名");
					wr.Write(",依存関係先名称");
					wr.Write(",種類");
					wr.Write(",更新あり");
					wr.Write(",selectでの利用");
					wr.Write(",従属性が存在する列またはパラメータ");
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
						wr.Write("テーブル名");
						wr.Write(",依存関係先名称");
						wr.Write(",種類");
						wr.Write(",更新あり");
						wr.Write(",selectでの利用");
						wr.Write(",従属性が存在する列またはパラメータ");
						wr.Write(wr.NewLine);
					}

					// 依存関係の情報を取得し、

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
							// テーブル名
							wr.Write(tbname);
							foreach( DataColumn col in ds.Tables[tbname].Columns)
							{
								wr.Write(",");
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
				MessageBox.Show("処理を完了しました");
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
		/// 選択されたテーブルに関するデータ件数を出力する
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RecordCountOutPut(object sender, System.EventArgs e)
		{
			SqlDataReader dr = null;
			SqlCommand	cm = new SqlCommand();
			cm.CommandTimeout = 180;

			if( this.tableList.SelectedItems.Count == 0 )
			{
				return;
			}
			if( this.tableList.SelectedItems.Count > 1 &&
				this.txtWhere.Text != null &&
				this.txtWhere.Text != "" )
			{
				if( MessageBox.Show("複数テーブルに同一の where 句を適用しますか？","確認",System.Windows.Forms.MessageBoxButtons.YesNo) 
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
					wr.WriteLine("テーブル名,データ件数");
				}
				else if( this.rdoOutFile.Checked == true ) 
				{
					StreamWriter sw = new StreamWriter(this.txtOutput.Text,false, GetEncode());
					sw.AutoFlush = false;
					wr = sw;
					fname.Append(this.txtOutput.Text);
					wr.WriteLine("テーブル名,データ件数");
				}

				foreach( String tbname in this.tableList.SelectedItems )
				{

					if( this.rdoOutFolder.Checked == true ) 
					{
						StreamWriter sw = new StreamWriter(this.txtOutput.Text + "\\" + tbname + ".csv.tmp",false, GetEncode());
						sw.AutoFlush = false;
						wr = sw;
						wr.WriteLine("テーブル名,データ件数");
					}
					trow = 0;
					string sqlstr;
					sqlstr = string.Format("select  count(1) from {0} ",gettbname(tbname));
					if( this.txtWhere.Text != "" )
					{
						sqlstr += " where " + this.txtWhere.Text;
					}

					cm.CommandText = sqlstr;
					cm.Connection = this.sqlConnection1;

					dr = cm.ExecuteReader();

					ArrayList fldname = new ArrayList();
					ArrayList strint = new ArrayList();
	
					fldname.Clear();
					strint.Clear();


					// データの書き出し
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
							// ファイルを作成したが、空になったので削除する	
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
					MessageBox.Show("対象データがありませんでした");
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
					MessageBox.Show("処理を完了しました");
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
		/// 現在接続先のDBを初期値として、クエリアナライザを起動する
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
		/// 現在接続先のDBを初期値として、クエリアナライザを起動する
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
		/// エンタープライズマネージャーを起動する
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
	}
	public class MyDataGridTextBoxColumn : DataGridTextBoxColumn
	{
		private CurrencyManager _sorce;
		private int				editrow;
		private bool	canSetEmptyString;

		public MyDataGridTextBoxColumn(bool canset)
		{
			this.NullText = "";
			this.TextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.setstringempty);
			this.canSetEmptyString = canset;
		}

		private void setstringempty(object sender, System.Windows.Forms.KeyEventArgs e)
		{
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

		//Paintメソッドをオーバーライドする
		protected override void Paint(Graphics g,
			Rectangle bounds,
			CurrencyManager source,
			int rowNum, 
			Brush backBrush,
			Brush foreBrush,
			bool alignToRight)
		{
			//セルの値を取得する
			object cellValue =
				this.GetColumnValueAtRow(source, rowNum);
			if (cellValue == DBNull.Value)
			{
				backBrush = new SolidBrush(Color.FromArgb(0xbf,0xef,0xff));
			}
			//基本クラスのPaintメソッドを呼び出す
			base.Paint(g, bounds, source, rowNum,
				backBrush, foreBrush, alignToRight);
		}
	}
}
