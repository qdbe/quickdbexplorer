using System;
using System.Text;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace MakeInsert
{
	/// <summary>
	/// Form2 の概要の説明です。
	/// </summary>
	public class Form2 : System.Windows.Forms.Form
	{
		public System.Data.SqlClient.SqlConnection sqlConnection1;
		private System.Windows.Forms.Button btnInsert;
		private System.Windows.Forms.Button btnFieldList;
		public System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.Button btnCSV;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.RadioButton rdoDspView;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton rdoNotDspView;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton rdoSortTable;
		private System.Windows.Forms.RadioButton rdoSortOwnerTable;
		private System.Windows.Forms.TextBox txtWhere;
		private System.Windows.Forms.TextBox txtSort;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem menuItem10;
		private System.Windows.Forms.MenuItem menuItem12;
		private System.Windows.Forms.Button btnSelect;
		private System.Windows.Forms.MenuItem menuItem11;
		private System.Windows.Forms.Button btnDDL;
		private System.Windows.Forms.MenuItem menuItem13;
		private System.Windows.Forms.MenuItem menuItem14;
		private System.Windows.Forms.MenuItem menuItem15;
		private System.Windows.Forms.CheckBox chkDspData;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtDspCount;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.MenuItem menuItem16;
		private System.Windows.Forms.MenuItem menuItem17;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.RadioButton rdoClipboard;
		private System.Windows.Forms.RadioButton rdoOutFile;
		private System.Windows.Forms.RadioButton rdoOutFolder;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ContextMenu contextMenu2;
		private System.Windows.Forms.MenuItem menuItem18;
		private System.Windows.Forms.MenuItem menuItem19;
		private System.Windows.Forms.MenuItem menuItem20;
		private System.Windows.Forms.MenuItem menuItem21;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.MenuItem menuItem22;
		private System.Windows.Forms.MenuItem menuItem23;
		private System.Windows.Forms.GroupBox groupBox6;
		private System.Windows.Forms.RadioButton rdoUnicode;
		private System.Windows.Forms.RadioButton rdoSjis;
		private System.Windows.Forms.RadioButton rdoUtf8;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ToolTip toolTip2;
		private System.Windows.Forms.Button btnQuerySelect;
		private ServerData svdata;
		private System.Windows.Forms.ToolTip toolTip3;
		private System.Windows.Forms.Button btnDataUpdate;

		private DataSet dspdt = new DataSet();
		private System.Windows.Forms.Button btnDataEdit;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button btnGridFormat;
		private	Font	gfont;
		private	Color	gcolor;
		private	string	NumFormat;
		private	string	FloatFormat;
		private	string	DateFormat;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Button btnQueryNonSelect;

		public string servername = "";
		Form4 Sqldlg = new Form4();
		private System.Windows.Forms.Button btnIndex;
		private System.Windows.Forms.Button btnRedisp;
		Form5 Sqldlg2 = new Form5();
		private System.Windows.Forms.Button btnTmpAllDsp;
		private System.Windows.Forms.ListBox dbList;
		private System.Windows.Forms.ListBox ownerListbox;
		private System.Windows.Forms.ListBox tableList;
		private System.Windows.Forms.ListBox fieldListbox;
		private System.Windows.Forms.DataGrid dbGrid;
		private System.Windows.Forms.RadioButton rdoDspSysUser;
		private System.Windows.Forms.RadioButton rdoNotDspSysUser;

		Form7 indexdlg = null;

		public Form2(ServerData sv)
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
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem16 = new System.Windows.Forms.MenuItem();
			this.menuItem17 = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.menuItem10 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem12 = new System.Windows.Forms.MenuItem();
			this.menuItem13 = new System.Windows.Forms.MenuItem();
			this.menuItem14 = new System.Windows.Forms.MenuItem();
			this.menuItem20 = new System.Windows.Forms.MenuItem();
			this.menuItem21 = new System.Windows.Forms.MenuItem();
			this.menuItem15 = new System.Windows.Forms.MenuItem();
			this.menuItem11 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.btnInsert = new System.Windows.Forms.Button();
			this.btnFieldList = new System.Windows.Forms.Button();
			this.btnCSV = new System.Windows.Forms.Button();
			this.rdoDspView = new System.Windows.Forms.RadioButton();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.rdoNotDspView = new System.Windows.Forms.RadioButton();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
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
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.txtDspCount = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.rdoNotDspSysUser = new System.Windows.Forms.RadioButton();
			this.rdoDspSysUser = new System.Windows.Forms.RadioButton();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.button4 = new System.Windows.Forms.Button();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.rdoOutFile = new System.Windows.Forms.RadioButton();
			this.rdoClipboard = new System.Windows.Forms.RadioButton();
			this.rdoOutFolder = new System.Windows.Forms.RadioButton();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.fieldListbox = new System.Windows.Forms.ListBox();
			this.contextMenu2 = new System.Windows.Forms.ContextMenu();
			this.menuItem18 = new System.Windows.Forms.MenuItem();
			this.menuItem19 = new System.Windows.Forms.MenuItem();
			this.menuItem22 = new System.Windows.Forms.MenuItem();
			this.menuItem23 = new System.Windows.Forms.MenuItem();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.rdoUtf8 = new System.Windows.Forms.RadioButton();
			this.rdoSjis = new System.Windows.Forms.RadioButton();
			this.rdoUnicode = new System.Windows.Forms.RadioButton();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.label6 = new System.Windows.Forms.Label();
			this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
			this.btnQuerySelect = new System.Windows.Forms.Button();
			this.toolTip3 = new System.Windows.Forms.ToolTip(this.components);
			this.btnDataUpdate = new System.Windows.Forms.Button();
			this.btnDataEdit = new System.Windows.Forms.Button();
			this.label7 = new System.Windows.Forms.Label();
			this.btnGridFormat = new System.Windows.Forms.Button();
			this.label8 = new System.Windows.Forms.Label();
			this.btnQueryNonSelect = new System.Windows.Forms.Button();
			this.btnIndex = new System.Windows.Forms.Button();
			this.btnRedisp = new System.Windows.Forms.Button();
			this.btnTmpAllDsp = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dbGrid)).BeginInit();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.SuspendLayout();
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
			this.tableList.ContextMenu = this.contextMenu1;
			this.tableList.HorizontalScrollbar = true;
			this.tableList.ItemHeight = 12;
			this.tableList.Location = new System.Drawing.Point(240, 16);
			this.tableList.Name = "tableList";
			this.tableList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.tableList.Size = new System.Drawing.Size(256, 304);
			this.tableList.TabIndex = 11;
			this.tableList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tableList_KeyDown);
			this.tableList.DoubleClick += new System.EventHandler(this.insertmake);
			this.tableList.SelectedIndexChanged += new System.EventHandler(this.tableList_SelectedIndexChanged);
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem16,
																						 this.menuItem17,
																						 this.menuItem1,
																						 this.menuItem7,
																						 this.menuItem8,
																						 this.menuItem9,
																						 this.menuItem10,
																						 this.menuItem2,
																						 this.menuItem3,
																						 this.menuItem4,
																						 this.menuItem12,
																						 this.menuItem13,
																						 this.menuItem14,
																						 this.menuItem20,
																						 this.menuItem21,
																						 this.menuItem15,
																						 this.menuItem11,
																						 this.menuItem5,
																						 this.menuItem6});
			this.contextMenu1.Popup += new System.EventHandler(this.contextMenu1_Popup);
			// 
			// menuItem16
			// 
			this.menuItem16.Index = 0;
			this.menuItem16.Text = "テーブル名コピー";
			this.menuItem16.Click += new System.EventHandler(this.menuItem16_Click);
			// 
			// menuItem17
			// 
			this.menuItem17.Index = 1;
			this.menuItem17.Text = "テーブル名コピー カンマ付き";
			this.menuItem17.Click += new System.EventHandler(this.menuItem17_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 2;
			this.menuItem1.Text = "ININSERT文作成";
			this.menuItem1.Click += new System.EventHandler(this.insertmake);
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 3;
			this.menuItem7.Text = "INSERT文作成(DELETE文付き)";
			this.menuItem7.Click += new System.EventHandler(this.insertmakeDelete);
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 4;
			this.menuItem8.Text = "INSERT文作成(フィールドリストなし)";
			this.menuItem8.Click += new System.EventHandler(this.insertmakeNoField);
			// 
			// menuItem9
			// 
			this.menuItem9.Index = 5;
			this.menuItem9.Text = "INSERT文作成(フィールドリストなし　DELETE文付き)";
			this.menuItem9.Click += new System.EventHandler(this.insertmakeNoFieldDelete);
			// 
			// menuItem10
			// 
			this.menuItem10.Index = 6;
			this.menuItem10.Text = "-";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 7;
			this.menuItem2.Text = "フィールドリスト作成";
			this.menuItem2.Click += new System.EventHandler(this.makefldlist);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 8;
			this.menuItem3.Text = "フィールドリスト改行作成";
			this.menuItem3.Click += new System.EventHandler(this.makefldListLF);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 9;
			this.menuItem4.Text = "フィールドリストカンマなし作成";
			this.menuItem4.Click += new System.EventHandler(this.makefldListNoComma);
			// 
			// menuItem12
			// 
			this.menuItem12.Index = 10;
			this.menuItem12.Text = "-";
			// 
			// menuItem13
			// 
			this.menuItem13.Index = 11;
			this.menuItem13.Text = "定義文生成";
			this.menuItem13.Click += new System.EventHandler(this.makeDDL);
			// 
			// menuItem14
			// 
			this.menuItem14.Index = 12;
			this.menuItem14.Text = "定義文生成 DROP文付き";
			this.menuItem14.Click += new System.EventHandler(this.makeDDLDrop);
			// 
			// menuItem20
			// 
			this.menuItem20.Index = 13;
			this.menuItem20.Text = "定義文生成([]付き)";
			this.menuItem20.Click += new System.EventHandler(this.makeDDLPare);
			// 
			// menuItem21
			// 
			this.menuItem21.Index = 14;
			this.menuItem21.Text = "定義文生成( DROP []付き)";
			this.menuItem21.Click += new System.EventHandler(this.makeDDLDropPare);
			// 
			// menuItem15
			// 
			this.menuItem15.Index = 15;
			this.menuItem15.Text = "-";
			// 
			// menuItem11
			// 
			this.menuItem11.Index = 16;
			this.menuItem11.Text = "Select文生成";
			this.menuItem11.Click += new System.EventHandler(this.btnSelect_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 17;
			this.menuItem5.Text = "CSV作成";
			this.menuItem5.Click += new System.EventHandler(this.makeCSV);
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 18;
			this.menuItem6.Text = "CSV作成(”付き)";
			this.menuItem6.Click += new System.EventHandler(this.makeCSVQuote);
			// 
			// btnInsert
			// 
			this.btnInsert.Location = new System.Drawing.Point(508, 16);
			this.btnInsert.Name = "btnInsert";
			this.btnInsert.Size = new System.Drawing.Size(132, 24);
			this.btnInsert.TabIndex = 12;
			this.btnInsert.Text = "INSERT文作成";
			this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
			// 
			// btnFieldList
			// 
			this.btnFieldList.Location = new System.Drawing.Point(508, 44);
			this.btnFieldList.Name = "btnFieldList";
			this.btnFieldList.Size = new System.Drawing.Size(132, 24);
			this.btnFieldList.TabIndex = 13;
			this.btnFieldList.Text = "フィールドリスト作成";
			this.btnFieldList.Click += new System.EventHandler(this.btnFieldList_Click);
			// 
			// btnCSV
			// 
			this.btnCSV.Location = new System.Drawing.Point(508, 128);
			this.btnCSV.Name = "btnCSV";
			this.btnCSV.Size = new System.Drawing.Size(132, 24);
			this.btnCSV.TabIndex = 16;
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
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.rdoNotDspView);
			this.groupBox1.Controls.Add(this.rdoDspView);
			this.groupBox1.Location = new System.Drawing.Point(8, 220);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(216, 40);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "VIEWを一覧に";
			this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
			// 
			// rdoNotDspView
			// 
			this.rdoNotDspView.Location = new System.Drawing.Point(112, 16);
			this.rdoNotDspView.Name = "rdoNotDspView";
			this.rdoNotDspView.Size = new System.Drawing.Size(88, 16);
			this.rdoNotDspView.TabIndex = 1;
			this.rdoNotDspView.Text = "表示しない";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.rdoSortOwnerTable);
			this.groupBox2.Controls.Add(this.rdoSortTable);
			this.groupBox2.Location = new System.Drawing.Point(8, 264);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(216, 52);
			this.groupBox2.TabIndex = 4;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "ソート順";
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
			this.btnSelect.TabIndex = 14;
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
			this.btnDDL.TabIndex = 15;
			this.btnDDL.Text = "定義文生成";
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
			this.chkDspData.TabIndex = 10;
			this.chkDspData.Text = "表示";
			this.chkDspData.CheckedChanged += new System.EventHandler(this.chkDspData_CheckedChanged);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.txtDspCount);
			this.groupBox3.Controls.Add(this.label3);
			this.groupBox3.Location = new System.Drawing.Point(8, 532);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(216, 44);
			this.groupBox3.TabIndex = 9;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "データグリッド";
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
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.rdoNotDspSysUser);
			this.groupBox4.Controls.Add(this.rdoDspSysUser);
			this.groupBox4.Location = new System.Drawing.Point(8, 176);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(216, 40);
			this.groupBox4.TabIndex = 2;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "システムユーザーを";
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
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.button4);
			this.groupBox5.Controls.Add(this.textBox4);
			this.groupBox5.Controls.Add(this.rdoOutFile);
			this.groupBox5.Controls.Add(this.rdoClipboard);
			this.groupBox5.Controls.Add(this.rdoOutFolder);
			this.groupBox5.Location = new System.Drawing.Point(8, 324);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(216, 84);
			this.groupBox5.TabIndex = 5;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "出力先";
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(168, 52);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(40, 20);
			this.button4.TabIndex = 4;
			this.button4.Text = "参照";
			this.button4.Click += new System.EventHandler(this.button4_Click_1);
			// 
			// textBox4
			// 
			this.textBox4.Location = new System.Drawing.Point(8, 52);
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(160, 19);
			this.textBox4.TabIndex = 3;
			this.textBox4.Text = "";
			this.textBox4.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
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
			this.fieldListbox.ContextMenu = this.contextMenu2;
			this.fieldListbox.HorizontalScrollbar = true;
			this.fieldListbox.ItemHeight = 12;
			this.fieldListbox.Location = new System.Drawing.Point(656, 40);
			this.fieldListbox.Name = "fieldListbox";
			this.fieldListbox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.fieldListbox.Size = new System.Drawing.Size(240, 280);
			this.fieldListbox.TabIndex = 23;
			this.fieldListbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fieldListbox_KeyDown);
			// 
			// contextMenu2
			// 
			this.contextMenu2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem18,
																						 this.menuItem19,
																						 this.menuItem22,
																						 this.menuItem23});
			// 
			// menuItem18
			// 
			this.menuItem18.Index = 0;
			this.menuItem18.Text = "コピー";
			this.menuItem18.Click += new System.EventHandler(this.menuItem18_Click);
			// 
			// menuItem19
			// 
			this.menuItem19.Index = 1;
			this.menuItem19.Text = "改行なしコピー";
			this.menuItem19.Click += new System.EventHandler(this.menuItem19_Click);
			// 
			// menuItem22
			// 
			this.menuItem22.Index = 2;
			this.menuItem22.Text = "コピーカンマなし";
			this.menuItem22.Click += new System.EventHandler(this.menuItem22_Click);
			// 
			// menuItem23
			// 
			this.menuItem23.Index = 3;
			this.menuItem23.Text = "コピー改行・カンマなし";
			this.menuItem23.Click += new System.EventHandler(this.menuItem23_Click);
			// 
			// groupBox6
			// 
			this.groupBox6.Controls.Add(this.rdoUtf8);
			this.groupBox6.Controls.Add(this.rdoSjis);
			this.groupBox6.Controls.Add(this.rdoUnicode);
			this.groupBox6.Location = new System.Drawing.Point(8, 412);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(216, 60);
			this.groupBox6.TabIndex = 6;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "出力文字コード";
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
			// checkBox2
			// 
			this.checkBox2.Location = new System.Drawing.Point(652, 12);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(244, 20);
			this.checkBox2.TabIndex = 22;
			this.checkBox2.Text = "フィールド属性を表示";
			this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
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
			this.btnQuerySelect.Location = new System.Drawing.Point(508, 156);
			this.btnQuerySelect.Name = "btnQuerySelect";
			this.btnQuerySelect.Size = new System.Drawing.Size(132, 24);
			this.btnQuerySelect.TabIndex = 17;
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
			this.btnGridFormat.Location = new System.Drawing.Point(508, 240);
			this.btnGridFormat.Name = "btnGridFormat";
			this.btnGridFormat.Size = new System.Drawing.Size(132, 24);
			this.btnGridFormat.TabIndex = 19;
			this.btnGridFormat.Text = "書式グリッド表示指定";
			this.btnGridFormat.Click += new System.EventHandler(this.btnGridFormat_Click);
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(244, 348);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(392, 16);
			this.label8.TabIndex = 32;
			this.label8.Text = "NULLを登録するにはCtrl+1 を、空文字列を登録するにはCtrl+2を押下します。";
			// 
			// btnQueryNonSelect
			// 
			this.btnQueryNonSelect.Location = new System.Drawing.Point(508, 184);
			this.btnQueryNonSelect.Name = "btnQueryNonSelect";
			this.btnQueryNonSelect.Size = new System.Drawing.Size(132, 24);
			this.btnQueryNonSelect.TabIndex = 18;
			this.btnQueryNonSelect.Text = "クエリ実効(Select以外)";
			this.btnQueryNonSelect.Click += new System.EventHandler(this.btnQueryNonSelect_Click);
			// 
			// btnIndex
			// 
			this.btnIndex.Location = new System.Drawing.Point(508, 212);
			this.btnIndex.Name = "btnIndex";
			this.btnIndex.Size = new System.Drawing.Size(132, 23);
			this.btnIndex.TabIndex = 35;
			this.btnIndex.Text = "INDEX情報表示";
			this.btnIndex.Click += new System.EventHandler(this.btnIndex_Click);
			// 
			// btnRedisp
			// 
			this.btnRedisp.Location = new System.Drawing.Point(652, 336);
			this.btnRedisp.Name = "btnRedisp";
			this.btnRedisp.Size = new System.Drawing.Size(96, 20);
			this.btnRedisp.TabIndex = 36;
			this.btnRedisp.Text = "グリッド再描画";
			this.btnRedisp.Click += new System.EventHandler(this.Redisp_Click);
			// 
			// btnTmpAllDsp
			// 
			this.btnTmpAllDsp.Location = new System.Drawing.Point(752, 336);
			this.btnTmpAllDsp.Name = "btnTmpAllDsp";
			this.btnTmpAllDsp.Size = new System.Drawing.Size(148, 20);
			this.btnTmpAllDsp.TabIndex = 37;
			this.btnTmpAllDsp.Text = "一時的に全データを表示";
			this.btnTmpAllDsp.Click += new System.EventHandler(this.btnTmpAllDsp_Click);
			// 
			// Form2
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(928, 597);
			this.Controls.Add(this.btnTmpAllDsp);
			this.Controls.Add(this.btnRedisp);
			this.Controls.Add(this.btnIndex);
			this.Controls.Add(this.btnQueryNonSelect);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.btnGridFormat);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.btnDataEdit);
			this.Controls.Add(this.btnDataUpdate);
			this.Controls.Add(this.btnQuerySelect);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.checkBox2);
			this.Controls.Add(this.groupBox6);
			this.Controls.Add(this.fieldListbox);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.groupBox5);
			this.Controls.Add(this.chkDspData);
			this.Controls.Add(this.dbGrid);
			this.Controls.Add(this.btnDDL);
			this.Controls.Add(this.ownerListbox);
			this.Controls.Add(this.btnSelect);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtSort);
			this.Controls.Add(this.txtWhere);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnCSV);
			this.Controls.Add(this.btnFieldList);
			this.Controls.Add(this.btnInsert);
			this.Controls.Add(this.tableList);
			this.Controls.Add(this.dbList);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.label5);
			this.Name = "Form2";
			this.ShowInTaskbar = false;
			this.Text = "DataBase選択";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.Form2_Closing);
			this.Load += new System.EventHandler(this.Form2_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dbGrid)).EndInit();
			this.groupBox3.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this.groupBox6.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void Form2_Load(object sender, System.EventArgs e)
		{

			try
			{
				this.Text = servername;
				SqlDataAdapter da = new SqlDataAdapter("SELECT name FROM sysdatabases order by name", this.sqlConnection1);
				DataSet ds = new DataSet();
				da.Fill(ds,"sysdatabases");

				foreach (DataRow row in ds.Tables["sysdatabases"].Rows )
				{
					this.dbList.Items.Add(row["name"]);
				}
			}
			catch ( System.Data.SqlClient.SqlException se )
			{
				MessageBox.Show(se.Message+":"+se.StackTrace+":\n"+se.ToString());
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
				this.textBox4.Text = (string)svdata.outfile[svdata.lastdb];
			}
			else
			{
				this.textBox4.Text = "";
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
												 this.menuItem16,
												 this.menuItem17,
												 this.menuItem10,
												 this.menuItem2,
												 this.menuItem3,
												 this.menuItem4,
												 this.menuItem12,
												 this.menuItem13,
												 this.menuItem14,
												 this.menuItem15,
												 this.menuItem11,
												 this.menuItem5,
												 this.menuItem6,
												 this.menuItem20,
												 this.menuItem21};
			foreach( MenuItem m in list )
			{
				m.Visible = false;
			}
			this.contextMenu1.Show(this.btnInsert,new Point(0,0));
			foreach( MenuItem m in list )
			{
				m.Visible = true;
			}
		}
		
		// フィールドリスト生成
		private void btnFieldList_Click(object sender, System.EventArgs e)
		{
			MenuItem[] list = new MenuItem[] {
												 this.menuItem16,
												 this.menuItem17,
												 this.menuItem1,
												 this.menuItem7,
												 this.menuItem8,
												 this.menuItem9,
												 this.menuItem10,
												 this.menuItem12,
												 this.menuItem13,
												 this.menuItem14,
												 this.menuItem15,
												 this.menuItem11,
												 this.menuItem5,
												 this.menuItem6,
												 this.menuItem20,
												 this.menuItem21};
			foreach( MenuItem m in list )
			{
				m.Visible = false;
			}
			this.contextMenu1.Show(this.btnFieldList,new Point(0,0));
			foreach( MenuItem m in list )
			{
				m.Visible = true;
			}
		}

		// CSV生成
		private void btnCSV_Click(object sender, System.EventArgs e)
		{
			MenuItem[] list = new MenuItem[] {
												 this.menuItem16,
												 this.menuItem17,
												 this.menuItem1,
												 this.menuItem7,
												 this.menuItem8,
												 this.menuItem9,
												 this.menuItem10,
												 this.menuItem2,
												 this.menuItem3,
												 this.menuItem4,
												 this.menuItem12,
												 this.menuItem13,
												 this.menuItem14,
												 this.menuItem15,
												 this.menuItem11,
												 this.menuItem20,
												 this.menuItem21};
			foreach( MenuItem m in list )
			{
				m.Visible = false;
			}
			this.contextMenu1.Show(this.btnCSV,new Point(0,0));
			foreach( MenuItem m in list )
			{
				m.Visible = true;
			}
		}

		// 定義文生成
		private void btnDDL_Click(object sender, System.EventArgs e)
		{
			MenuItem[] list = new MenuItem[] {
												 this.menuItem16,
												 this.menuItem17,
												 this.menuItem1,
												 this.menuItem7,
												 this.menuItem8,
												 this.menuItem9,
												 this.menuItem10,
												 this.menuItem2,
												 this.menuItem3,
												 this.menuItem4,
												 this.menuItem12,
												 this.menuItem15,
												 this.menuItem11,
												 this.menuItem5,
												 this.menuItem6};
			foreach( MenuItem m in list )
			{
				m.Visible = false;
			}
			this.contextMenu1.Show(this.btnDDL,new Point(0,0));
			foreach( MenuItem m in list )
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
				if( this.textBox4.Text == "" )
				{
					this.saveFileDialog1.CreatePrompt = true;
					this.saveFileDialog1.Filter = "SQL|*.sql|CSV|*.csv|TXT|*.txt|全て|*.*";
					DialogResult ret = this.saveFileDialog1.ShowDialog();
					if( ret == DialogResult.OK )
					{
						this.textBox4.Text = this.saveFileDialog1.FileName;
					}
				}
				else
				{
					DirectoryInfo d = new DirectoryInfo(this.textBox4.Text);
					if( d.Exists )
					{
						MessageBox.Show("指定されたファイル名はフォルダを指しています。ファイル名を指定してください。処理を中断します");
						return false;
					}
				}
				if( this.textBox4.Text == "" )
				{
					MessageBox.Show("ファイル名が指定されていないので、処理を中断します");
					return false;
				}
			}
			if( this.rdoOutFolder.Checked == true )
			{

				if( this.textBox4.Text == "" )
				{
					this.folderBrowserDialog1.SelectedPath = "";
					this.folderBrowserDialog1.ShowNewFolderButton = true;
					DialogResult ret = this.folderBrowserDialog1.ShowDialog();
					if( ret == DialogResult.OK )
					{
						this.textBox4.Text = this.folderBrowserDialog1.SelectedPath;
					}
				}

				if( this.textBox4.Text != "" )
				{
					DirectoryInfo d = new DirectoryInfo(this.textBox4.Text);
					FileInfo	f = new FileInfo(this.textBox4.Text);
					if( f.Exists )
					{
						MessageBox.Show("フォルダ名は指定されていますが、フォルダではありません。処理を中断します");
						return false;
					}
					else if( !d.Exists )
					{
						Directory.CreateDirectory(this.textBox4.Text);
						DirectoryInfo ff = new DirectoryInfo(this.textBox4.Text);
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
					StreamWriter sw = new StreamWriter(this.textBox4.Text,false,GetEncode());
					sw.AutoFlush = false;
					wr = sw;
					fname.Append(this.textBox4.Text);
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
						StreamWriter sw = new StreamWriter(this.textBox4.Text + "\\" + tbname + ".sql.tmp",false, GetEncode());
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
							fname.Append(this.textBox4.Text + "\\" + tbname + ".sql\r\n");
							// ファイルを作成したが、空になったので削除する	
							File.Copy(this.textBox4.Text + "\\" + tbname + ".sql.tmp", 
								this.textBox4.Text + "\\" + tbname + ".sql", 
								true );
						}
						File.Delete(this.textBox4.Text + "\\" + tbname + ".sql.tmp");
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
				MessageBox.Show(exp.Message+exp.StackTrace);
			}
		}

		private void crefldlst(bool isLF, bool iscomma)
		{
			try
			{
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
					StreamWriter sw = new StreamWriter(this.textBox4.Text,false, GetEncode());
					sw.AutoFlush = false;
					wr = sw;
					fname.Append(this.textBox4.Text);
				}

				foreach( String tbname in this.tableList.SelectedItems )
				{
					if( this.rdoOutFolder.Checked == true ) 
					{
						StreamWriter sw = new StreamWriter(this.textBox4.Text + "\\" + tbname + ".sql",false, GetEncode());
						sw.AutoFlush = false;
						wr = sw;
						fname.Append(this.textBox4.Text + "\\" + tbname + ".sql\r\n");
					}

					// get id 
					SqlDataAdapter da = new SqlDataAdapter(string.Format("select  * from {0} where 0=1",gettbname(tbname)), this.sqlConnection1);

					DataSet ds = new DataSet();
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
				MessageBox.Show(exp.Message+exp.StackTrace);
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
				if( this.checkBox2.Checked == true )
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
				da.Fill(ds,tbname);

				sqlstr = string.Format("select * from sysindexes where id={0} and indid > 0 and indid < 255 and (status & 2048)=2048",
					(int)ds.Tables[tbname].Rows[0]["id"] );
				SqlDataAdapter daa = new SqlDataAdapter(sqlstr, this.sqlConnection1);
				DataSet idx = new DataSet();
				daa.Fill(idx,tbname);

				int indid = -1;
				DataSet idkey = new DataSet();
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
				MessageBox.Show(exp.Message+exp.StackTrace);
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
			crecsv(false);
		}

		private void crecsv(bool isdquote)
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
					StreamWriter sw = new StreamWriter(this.textBox4.Text,false, GetEncode());
					sw.AutoFlush = false;
					wr = sw;
					fname.Append(this.textBox4.Text);
				}

				foreach( String tbname in this.tableList.SelectedItems )
				{

					if( this.rdoOutFolder.Checked == true ) 
					{
						StreamWriter sw = new StreamWriter(this.textBox4.Text + "\\" + tbname + ".csv.tmp",false, GetEncode());
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
							wr.Write(",");
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
								wr.Write( "," );
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
							fname.Append(this.textBox4.Text + "\\" + tbname + ".csv\r\n");
							// ファイルを作成したが、空になったので削除する	
							File.Copy(this.textBox4.Text + "\\" + tbname + ".csv.tmp", 
								this.textBox4.Text + "\\" + tbname + ".csv", 
								true );
						}
						File.Delete(this.textBox4.Text + "\\" + tbname + ".csv.tmp");
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
				MessageBox.Show(se.Message+":"+se.StackTrace+":\n"+se.ToString());
				return;
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
			crecsv(true);
		}

		private void groupBox1_Enter(object sender, System.EventArgs e)
		{
		
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
					StreamWriter sw = new StreamWriter(this.textBox4.Text,false, GetEncode());
					sw.AutoFlush = false;
					wr = sw;
					fname.Append(this.textBox4.Text);
				}

				foreach( String tbname in this.tableList.SelectedItems )
				{

					if( this.rdoOutFolder.Checked == true ) 
					{
						StreamWriter sw = new StreamWriter(this.textBox4.Text + "\\" + tbname + ".sql",false, GetEncode());
						sw.AutoFlush = false;
						wr = sw;
						fname.Append(this.textBox4.Text + "\\" + tbname + ".sql\r\n");
					}
					// get id 
					SqlDataAdapter da = new SqlDataAdapter(string.Format("select  * from {0} where 0=1",gettbname(tbname)), this.sqlConnection1);

					DataSet ds = new DataSet();
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
				MessageBox.Show(exp.Message+exp.StackTrace);
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
				MessageBox.Show(se.Message+":"+se.StackTrace+":\n"+se.ToString());
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
					StreamWriter sw = new StreamWriter(this.textBox4.Text,false, GetEncode());
					sw.AutoFlush = false;
					wr = sw;
					fname.Append(this.textBox4.Text);
				}

				foreach( String tbname in this.tableList.SelectedItems )
				{
					if( this.rdoOutFolder.Checked == true ) 
					{
						StreamWriter sw = new StreamWriter(this.textBox4.Text + "\\" + tbname + ".sql",false, GetEncode());
						sw.AutoFlush = false;
						wr = sw;
						fname.Append(this.textBox4.Text + "\\" + tbname + ".sql\r\n");
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
				MessageBox.Show(se.Message+":"+se.StackTrace+":\n"+se.ToString());
				return;
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
				this.btnDataUpdate.Enabled = true;
				this.btnDataEdit.Enabled = true;
				this.btnGridFormat.Enabled = true;
			}
			catch( Exception exp)
			{
				MessageBox.Show(exp.Message + "\r\n" + exp.StackTrace);
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
			catch ( System.Data.SqlClient.SqlException se )
			{
				if( dr != null )
				{
					dr.Close();
				}
				MessageBox.Show(se.Message+":"+se.StackTrace+":\n"+se.ToString());
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

		private void menuItem16_Click(object sender, System.EventArgs e)
		{
			copytablename(false);
		}

		private void menuItem17_Click(object sender, System.EventArgs e)
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

		private void Form2_Closing(object sender, System.ComponentModel.CancelEventArgs e)
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
			svdata.outfile[svdata.lastdb] = this.textBox4.Text;
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

		private void menuItem18_Click(object sender, System.EventArgs e)
		{
			copyfldlist(true,true);
		}

		private void menuItem19_Click(object sender, System.EventArgs e)
		{
			copyfldlist(false,true);
		}

		private void rdoClipboard_CheckedChanged(object sender, System.EventArgs e)
		{
			if( rdoClipboard.Checked == true )
			{
				this.textBox4.Enabled = false;
				this.button4.Enabled = false;
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
				this.textBox4.Enabled = true;
				this.button4.Enabled = true;
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
				this.textBox4.Enabled = true;
				this.button4.Enabled = true;
				svdata.outdest[svdata.lastdb] = 1;
				this.rdoUnicode.Enabled = true;
				this.rdoSjis.Enabled = true;
				this.rdoUtf8.Enabled = true;
			}
		}

		private void button4_Click_1(object sender, System.EventArgs e)
		{
			if( this.rdoOutFile.Checked == true )
			{
				if( this.textBox4.Text != "" )
				{
					DirectoryInfo d = new DirectoryInfo(this.textBox4.Text);
					if( d.Exists )
					{
						this.saveFileDialog1.InitialDirectory = this.textBox4.Text;
						this.saveFileDialog1.FileName = "";
					}
					else
					{
						this.saveFileDialog1.FileName = this.textBox4.Text;
					}
				}
				// 単独ファイルの参照指定
				
				this.saveFileDialog1.CreatePrompt = true;
				this.saveFileDialog1.Filter = "SQL|*.sql|CSV|*.csv|TXT|*.txt|全て|*.*";
				DialogResult ret = this.saveFileDialog1.ShowDialog();
				if( ret == DialogResult.OK )
				{
					this.textBox4.Text = this.saveFileDialog1.FileName;
				}
			}
			else
			{
				// 複数ファイルのディレクトリ参照指定
				if( this.textBox4.Text != "" )
				{
					FileInfo f = new FileInfo(this.textBox4.Text);
					if( f.Exists &&
						( f.Attributes & FileAttributes.Directory ) == FileAttributes.Directory)
					{
						this.folderBrowserDialog1.SelectedPath = this.textBox4.Text;
					}
					else if( f.Exists && (f.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
					{
						this.folderBrowserDialog1.SelectedPath = f.Directory.FullName;
					}
					else if( !f.Exists )
					{
						this.folderBrowserDialog1.SelectedPath = this.textBox4.Text;
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
					this.textBox4.Text = this.folderBrowserDialog1.SelectedPath;
				}
			}
		}

		private void textBox4_TextChanged(object sender, System.EventArgs e)
		{
			this.toolTip1.SetToolTip(this.textBox4,this.textBox4.Text);
			svdata.outfile[svdata.lastdb] = this.textBox4.Text;
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

		private void menuItem22_Click(object sender, System.EventArgs e)
		{
			copyfldlist(true,false);
		}

		private void menuItem23_Click(object sender, System.EventArgs e)
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

		private void checkBox2_CheckedChanged(object sender, System.EventArgs e)
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
				if( Sqldlg.ShowDialog() == DialogResult.OK )
				{
					SqlDataAdapter da = new SqlDataAdapter(Sqldlg.SelectSql, this.sqlConnection1);
					da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
					dspdt = new DataSet();

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
				MessageBox.Show(exp.Message + "\r\n" + exp.StackTrace);
			}
		}

		private void btnDataUpdate_Click(object sender, System.EventArgs e)
		{
			SqlTransaction tran	= null;
			try
			{
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
				MessageBox.Show(exp.Message+":"+exp.StackTrace+":\n"+exp.ToString());
				tran.Rollback();
			}

		}

		private void btnDataEdit_Click(object sender, System.EventArgs e)
		{
			try
			{
				if( this.dbGrid.ReadOnly == true )
				{
					// 編集可にする
					this.dbGrid.ReadOnly = false;
					this.btnDataEdit.Text = "データ編集終了";
				}
				else
				{
					// 編集不可にする
					if( this.dspdt.Tables["aaaa"].GetChanges() == null ||
						this.dspdt.Tables["aaaa"].GetChanges().Rows.Count == 0 )
					{
						this.dbGrid.ReadOnly = true;
						this.btnDataEdit.Text = "データ編集";
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
						}
					}
					
				}
			}
			catch( Exception exp )
			{
				MessageBox.Show(exp.Message+":"+exp.StackTrace+":\n"+exp.ToString());
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
				e.Graphics.DrawString(text, dbGrid.Font, new SolidBrush(Color.Black), 12, y);
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

		private void contextMenu1_Popup(object sender, System.EventArgs e)
		{
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
				MessageBox.Show(exp.Message + "\r\n" + exp.StackTrace);

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
			}
			else
			{
				DspData("");
			}
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
