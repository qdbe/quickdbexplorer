using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Text;

namespace quickDBExplorer
{
	/// <summary>
	/// where 句を指定するためのダイアログ
	/// </summary>
	public class WhereDialog : quickDBExplorer.ZoomFloatingDialog
	{
		private System.ComponentModel.IContainer components = null;

		private const string BASETABLE = "Base";

        private const string CLEARBUTTON = "ClearButton";
        private const string FIELDNAME = "Fields";
        private const string CONDCMB = "cond";
		private const string VALUES = "datavalues";
		private const string FIELDTYPE = "fieldtypes";
        private const string COLLATETYPE = "collatetype";
        private const string CANCOLLATE = "cancollate";

        private const int COL_CLEAR_BUTTON = 0;
        private const int COL_LABEL = 1;
        private const int COL_HIDDEN_FIELDTYPE = 2;
        private const int COL_COND = 3;
        private const int COL_VAL = 4;
        private const int COL_COLLATE = 5;
        private const int COL_CANCOLLATE = 6;

        private DataSet fieldDs = null;
		private DataGridView fieldCondition;
		private Button btnClear;
		private ComboBox cmbCondition;
		private int forceCellColumn = -1;
		private int forceCellRow = -1;

		/// <summary>
		/// where 句指定の対象オブジェクト
		/// </summary>
		private DBObjectInfo pTargetObject;
		private DBObjectInfo preTartgetObject = null;
		
		/// <summary>
		/// where 句指定の対象オブジェクト
		/// </summary>
		public DBObjectInfo TargetObject
		{
			set {
				this.pTargetObject = value;
				string preobjname = string.Empty;
				string newobjname = string.Empty;

				if ( this.pTargetObject != null)
				{
					newobjname = pTargetObject.FormalName;
				}
				if (this.preTartgetObject != null)
				{
					preobjname = this.preTartgetObject.FormalName;
				}
				if (preobjname != newobjname)
				{
					this.ResetTarget();
				}
				this.preTartgetObject = this.pTargetObject;
			}
		}

		/// <summary>
		/// オブジェクトの alias
		/// </summary>
		private string preAliasName = "";
        private DataGridViewButtonColumn clearLine;
        private DataGridViewTextBoxColumn Fields;
        private DataGridViewTextBoxColumn fieldtypes;
        private DataGridViewComboBoxColumn cond;
        private DataGridViewTextBoxColumn datavalues;
        private DataGridViewComboBoxColumn collatetype;
        private DataGridViewTextBoxColumn cancollate;
        private ComboBox cmbCollate;
        private Button btnCollateAllSet;
        private Label label1;

        /// <summary>
        /// Whereダイアログ テーブル毎履歴
        /// database, object wherestring 
        /// </summary>
        public Dictionary<string, Dictionary<string, Dictionary<string, DataSet>>> WhereHistory { get; set; }

        /// <summary>
        /// オブジェクトの alias
        /// </summary>
        private string pAliasName = "";

		/// <summary>
		/// オブジェクトの alias
		/// </summary>
		public string AliasName
		{
			get { return pAliasName; }
			set { 
				pAliasName = value;
				if (pAliasName != this.preAliasName)
				{
					this.SetFieldCondResult();
				}
				this.preAliasName = pAliasName; 
			}
		}

        /// <summary>
        /// 接続先データベース名
        /// </summary>
        public string DbName { get; set; }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public WhereDialog()
		{
			// この呼び出しは Windows フォーム デザイナで必要です。
			InitializeComponent();

			// TODO: InitializeComponent 呼び出しの後に初期化処理を追加します。
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

		#region デザイナで生成されたコード
		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.fieldCondition = new System.Windows.Forms.DataGridView();
            this.clearLine = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Fields = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fieldtypes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cond = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.datavalues = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.collatetype = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.cancollate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnClear = new System.Windows.Forms.Button();
            this.cmbCondition = new System.Windows.Forms.ComboBox();
            this.cmbCollate = new System.Windows.Forms.ComboBox();
            this.btnCollateAllSet = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.fieldCondition)).BeginInit();
            this.SuspendLayout();
            // 
            // chkStayOnTop
            // 
            this.chkStayOnTop.TabIndex = 9;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(462, 362);
            this.btnApply.TabIndex = 7;
            // 
            // txtZoom
            // 
            this.txtZoom.AcceptsReturn = true;
            this.txtZoom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtZoom.Size = new System.Drawing.Size(624, 114);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(14, 362);
            this.btnOk.TabIndex = 6;
            this.btnOk.Enter += new System.EventHandler(this.OkOrApply);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(558, 362);
            this.btnClose.TabIndex = 8;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // MsgArea
            // 
            this.MsgArea.Location = new System.Drawing.Point(106, 362);
            this.MsgArea.Size = new System.Drawing.Size(350, 26);
            this.MsgArea.TabIndex = 11;
            // 
            // fieldCondition
            // 
            this.fieldCondition.AllowUserToAddRows = false;
            this.fieldCondition.AllowUserToDeleteRows = false;
            this.fieldCondition.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fieldCondition.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.fieldCondition.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.fieldCondition.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.fieldCondition.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clearLine,
            this.Fields,
            this.fieldtypes,
            this.cond,
            this.datavalues,
            this.collatetype,
            this.cancollate});
            this.fieldCondition.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.fieldCondition.Location = new System.Drawing.Point(14, 169);
            this.fieldCondition.Name = "fieldCondition";
            this.fieldCondition.RowHeadersVisible = false;
            this.fieldCondition.RowTemplate.Height = 21;
            this.fieldCondition.Size = new System.Drawing.Size(622, 160);
            this.fieldCondition.TabIndex = 4;
            this.fieldCondition.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.FieldCondition_CellContentClick);
            this.fieldCondition.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.fieldCondition_CellEndEdit);
            this.fieldCondition.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.fieldCondition_CellEnter);
            this.fieldCondition.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.fieldCondition_CellLeave);
            // 
            // clearLine
            // 
            this.clearLine.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.clearLine.DataPropertyName = "ClearButton";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.clearLine.DefaultCellStyle = dataGridViewCellStyle1;
            this.clearLine.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.clearLine.HeaderText = "クリア";
            this.clearLine.Name = "clearLine";
            this.clearLine.Text = "クリア";
            this.clearLine.ToolTipText = "選択行をクリア";
            this.clearLine.UseColumnTextForButtonValue = true;
            this.clearLine.Width = 45;
            // 
            // Fields
            // 
            this.Fields.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Fields.DataPropertyName = "Fields";
            this.Fields.HeaderText = "フィールド名";
            this.Fields.Name = "Fields";
            this.Fields.ReadOnly = true;
            this.Fields.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Fields.Width = 50;
            // 
            // fieldtypes
            // 
            this.fieldtypes.DataPropertyName = "fieldtypes";
            this.fieldtypes.HeaderText = "";
            this.fieldtypes.Name = "fieldtypes";
            this.fieldtypes.ReadOnly = true;
            this.fieldtypes.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.fieldtypes.Visible = false;
            this.fieldtypes.Width = 5;
            // 
            // cond
            // 
            this.cond.DataPropertyName = "cond";
            this.cond.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.cond.HeaderText = "結合条件指定";
            this.cond.Items.AddRange(new object[] {
            "",
            "=",
            "!=",
            "contain",
            "not contain",
            "like",
            "not like",
            "is null",
            "is null and = \'\'",
            "is not null",
            "is not null and != \'\'",
            "<",
            ">",
            ">=",
            "<="});
            this.cond.MinimumWidth = 50;
            this.cond.Name = "cond";
            this.cond.Width = 53;
            // 
            // datavalues
            // 
            this.datavalues.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.datavalues.DataPropertyName = "datavalues";
            this.datavalues.HeaderText = "値";
            this.datavalues.MinimumWidth = 100;
            this.datavalues.Name = "datavalues";
            this.datavalues.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // collatetype
            // 
            this.collatetype.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.collatetype.DataPropertyName = "collatetype";
            this.collatetype.HeaderText = "照合順序";
            this.collatetype.Items.AddRange(new object[] {
            "",
            "Japanese_BIN",
            "Japanese_CI_AI",
            "Japanese_CI_AS",
            "Japanese_CS_AI",
            "Japanese_CS_AS",
            "Japanese_Unicode_CI_AI",
            "Japanese_Unicode_CI_AS",
            "Japanese_Unicode_CS_AI",
            "Japanese_Unicode_CS_AS",
            "Japanese_XJIS_100_CI_AI",
            "Japanese_XJIS_100_CI_AS",
            "Japanese_XJIS_100_CS_AI",
            "Japanese_XJIS_100_CS_AS",
            "Japanese_XJIS_140_BIN",
            "Japanese_XJIS_140_CI_AI_VSS",
            "Japanese_XJIS_140_CI_AS_VSS",
            "Japanese_XJIS_140_CS_AI_VSS",
            "Japanese_XJIS_140_CS_AS_VSS"});
            this.collatetype.MinimumWidth = 50;
            this.collatetype.Name = "collatetype";
            this.collatetype.Width = 50;
            // 
            // cancollate
            // 
            this.cancollate.DataPropertyName = "cancollate";
            this.cancollate.HeaderText = "";
            this.cancollate.Name = "cancollate";
            this.cancollate.ReadOnly = true;
            this.cancollate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.cancollate.Visible = false;
            this.cancollate.Width = 5;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClear.Location = new System.Drawing.Point(14, 335);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(181, 23);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "フィールド条件を全てクリア";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // cmbCondition
            // 
            this.cmbCondition.FormattingEnabled = true;
            this.cmbCondition.Items.AddRange(new object[] {
            "AND",
            "OR",
            "AND",
            "OR"});
            this.cmbCondition.Location = new System.Drawing.Point(14, 144);
            this.cmbCondition.Name = "cmbCondition";
            this.cmbCondition.Size = new System.Drawing.Size(154, 20);
            this.cmbCondition.TabIndex = 1;
            this.cmbCondition.SelectedIndexChanged += new System.EventHandler(this.cmbCondition_SelectedIndexChanged);
            this.cmbCondition.TextChanged += new System.EventHandler(this.cmbCondition_TextChanged);
            // 
            // cmbCollate
            // 
            this.cmbCollate.FormattingEnabled = true;
            this.cmbCollate.Items.AddRange(new object[] {
            "",
            "Japanese_BIN",
            "Japanese_CI_AI",
            "Japanese_CI_AS",
            "Japanese_CS_AI",
            "Japanese_CS_AS",
            "Japanese_Unicode_CI_AI",
            "Japanese_Unicode_CI_AS",
            "Japanese_Unicode_CS_AI",
            "Japanese_Unicode_CS_AS",
            "Japanese_XJIS_100_CI_AI",
            "Japanese_XJIS_100_CI_AS",
            "Japanese_XJIS_100_CS_AI",
            "Japanese_XJIS_100_CS_AS",
            "Japanese_XJIS_140_BIN",
            "Japanese_XJIS_140_CI_AI_VSS",
            "Japanese_XJIS_140_CI_AS_VSS",
            "Japanese_XJIS_140_CS_AI_VSS",
            "Japanese_XJIS_140_CS_AS_VSS"});
            this.cmbCollate.Location = new System.Drawing.Point(314, 144);
            this.cmbCollate.Name = "cmbCollate";
            this.cmbCollate.Size = new System.Drawing.Size(154, 20);
            this.cmbCollate.TabIndex = 2;
            this.cmbCollate.SelectedIndexChanged += new System.EventHandler(this.cmbCondition_SelectedIndexChanged);
            this.cmbCollate.TextChanged += new System.EventHandler(this.cmbCondition_TextChanged);
            // 
            // btnCollateAllSet
            // 
            this.btnCollateAllSet.Location = new System.Drawing.Point(474, 142);
            this.btnCollateAllSet.Name = "btnCollateAllSet";
            this.btnCollateAllSet.Size = new System.Drawing.Size(75, 23);
            this.btnCollateAllSet.TabIndex = 3;
            this.btnCollateAllSet.Text = "設定";
            this.btnCollateAllSet.UseVisualStyleBackColor = true;
            this.btnCollateAllSet.Click += new System.EventHandler(this.btnCollateAllSet_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(207, 147);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "照合順序一括指定";
            // 
            // WhereDialog
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
            this.ClientSize = new System.Drawing.Size(648, 394);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCollateAllSet);
            this.Controls.Add(this.fieldCondition);
            this.Controls.Add(this.cmbCollate);
            this.Controls.Add(this.cmbCondition);
            this.Controls.Add(this.btnClear);
            this.Name = "WhereDialog";
            this.Text = "Where 句指定";
            this.Load += new System.EventHandler(this.WhereDialog_Load);
            this.VisibleChanged += new System.EventHandler(this.WhereDialog_VisibleChanged);
            this.Controls.SetChildIndex(this.chkStayOnTop, 0);
            this.Controls.SetChildIndex(this.btnClear, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this.btnApply, 0);
            this.Controls.SetChildIndex(this.cmbCondition, 0);
            this.Controls.SetChildIndex(this.cmbCollate, 0);
            this.Controls.SetChildIndex(this.MsgArea, 0);
            this.Controls.SetChildIndex(this.fieldCondition, 0);
            this.Controls.SetChildIndex(this.txtZoom, 0);
            this.Controls.SetChildIndex(this.btnCollateAllSet, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.fieldCondition)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// 画面起動時処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void WhereDialog_Load(object sender, System.EventArgs e)
		{
			this.cmbCondition.SelectedIndex = 0;
			DispFieldCondition();
		}

		/// <summary>
		/// フィールド一覧の設定を行う
		/// </summary>
		private void DispFieldCondition()
		{
			if( this.pTargetObject == null )
			{
				return;
			}
			if( this.pTargetObject.CanSelect == false )
			{
				return;
			}
			// 表示するデータを生成する
			this.fieldDs = new DataSet();
			fieldDs.Tables.Add(BASETABLE);
            fieldDs.Tables[BASETABLE].Columns.Add(CLEARBUTTON);
            fieldDs.Tables[BASETABLE].Columns.Add(FIELDNAME);
			fieldDs.Tables[BASETABLE].Columns.Add(FIELDTYPE);
			fieldDs.Tables[BASETABLE].Columns.Add(CONDCMB);
			fieldDs.Tables[BASETABLE].Columns.Add(VALUES);
            fieldDs.Tables[BASETABLE].Columns.Add(COLLATETYPE);
            fieldDs.Tables[BASETABLE].Columns.Add(CANCOLLATE);
            // フィールド情報のセット
            foreach (DBFieldInfo each in this.pTargetObject.FieldInfo){
				DataRow dr = fieldDs.Tables[BASETABLE].NewRow();
                dr[FIELDNAME] = ((DBFieldInfo)each).Name;
                string valtype = ((DBFieldInfo)each).RealTypeName;
                dr[FIELDTYPE] = valtype;
				dr[CONDCMB] = "";
				dr[VALUES] = "";
                dr[COLLATETYPE] = "";
                if (valtype == "nvarchar" ||
                    valtype == "nchar" ||
                    valtype == "xml" ||
                    valtype == "sql_variant" ||
                    valtype == "ntext" ||
                    valtype == "varchar" ||
                    valtype == "char" ||
                    valtype == "text")
                {
                    dr[CANCOLLATE] = "1";
                }
                else
                {
                    dr[CANCOLLATE] = "0";
                }
                this.fieldDs.Tables[BASETABLE].Rows.Add(dr);
			}

			// DataGrid の 表示を整える

			this.fieldCondition.DataSource = this.fieldDs;
			this.fieldCondition.DataMember = BASETABLE;

            for (int rLine = 0; rLine < fieldDs.Tables[BASETABLE].Rows.Count; rLine++)
            {
                DataRow each = fieldDs.Tables[BASETABLE].Rows[rLine];
                if ((string)each[CANCOLLATE] == "1")
                {
                    fieldCondition[COLLATETYPE, rLine].ReadOnly = false;
                    fieldCondition[COLLATETYPE, rLine].Style.BackColor = Color.White;
                }
                else
                {
                    fieldCondition[COLLATETYPE, rLine].Value = "";
                    fieldCondition[COLLATETYPE, rLine].ReadOnly = true;
                    fieldCondition[COLLATETYPE, rLine].Style.BackColor = Color.LightGray;
                }

            }
            SetFieldCondResult();

        }

		private void fieldCondition_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == COL_VAL &&
				this.fieldCondition[e.ColumnIndex, e.RowIndex].Value != null &&
				!this.fieldCondition[e.ColumnIndex, e.RowIndex].Value.Equals(string.Empty) &&
				this.fieldCondition[e.ColumnIndex, e.RowIndex].Value != DBNull.Value)
			{
				if( this.fieldCondition[e.ColumnIndex -1,e.RowIndex].Value == null ||
					this.fieldCondition[e.ColumnIndex -1,e.RowIndex].Value.Equals(string.Empty) ||
					this.fieldCondition[e.ColumnIndex -1,e.RowIndex].Value == DBNull.Value )
				{
					string inputVal = this.fieldCondition[e.ColumnIndex, e.RowIndex].Value.ToString();
					string valtype = (string)this.fieldCondition[e.ColumnIndex - 2, e.RowIndex].Value;
					// 値
					if ((valtype == "nvarchar" ||
						valtype == "nchar" ||
						valtype == "xml" ||
						valtype == "sql_variant" ||
						valtype == "ntext"||
						valtype == "varchar" ||
						valtype == "char" ||
						valtype == "text") &&
						(inputVal.Contains("%") ||
						inputVal.Contains("_")))
					{
						this.fieldCondition[e.ColumnIndex - 1, e.RowIndex].Value = "like";
					}
					else
					{
						this.fieldCondition[e.ColumnIndex - 1, e.RowIndex].Value = "=";
					}
				}
			}
			SetFieldCondResult();
		}

		private void cmbCondition_SelectedIndexChanged(object sender, EventArgs e)
		{
			SetFieldCondResult();
		}

		/// <summary>
		/// 画面条件からwhere句を生成する
		/// </summary>
		public void SetFieldCondResult()
		{
			if (fieldDs == null)
			{
				return;
			}
			this.txtZoom.Text = string.Empty;

			StringBuilder sb = new StringBuilder();
			string alias = this.pAliasName;
			if (alias.Length != 0 && !alias.EndsWith("."))
			{
				alias += ".";
			}
			int rowCount = 0;
			string condstr = string.Empty;
			string fieldname = string.Empty;
			string values = string.Empty;
			for (int i = 0; i < this.fieldDs.Tables[BASETABLE].Rows.Count; i++)
			{
				DataRow dr = this.fieldDs.Tables[BASETABLE].Rows[i];
				fieldname = alias + (string)dr[FIELDNAME];
				if (dr[CONDCMB] == DBNull.Value)
				{
					continue;
				}
				condstr = (string)dr[CONDCMB];
				if (condstr == null || condstr.Length == 0)
				{
					continue;
				}
				if (dr[VALUES] == DBNull.Value)
				{
					values = string.Empty;
				}
				else
				{
					values = (string)dr[VALUES];
				}

				// 条件句の指定
				if (rowCount != 0)
				{
					sb.Append(" ").Append(this.cmbCondition.Text).Append(" ( ");
				}
				else
				{
					sb.Append(" ( ");
				}
				rowCount++;

				if (condstr == "=")
				{
					sb.Append(fieldname).Append(" = ");
				}
				else if (condstr == "!=")
				{
					sb.Append(fieldname).Append(" != ");
				}
				else if (condstr == "<")
				{
					sb.Append(fieldname).Append(" < ");
				}
				else if (condstr == ">")
				{
					sb.Append(fieldname).Append(" > ");
				}
				else if (condstr == "<=")
				{
					sb.Append(fieldname).Append(" <= ");
				}
				else if (condstr == ">")
				{
					sb.Append(fieldname).Append(" >= ");
				}
				else if (condstr == "like")
				{
					sb.Append(fieldname).Append(" like ");
				}
				else if (condstr == "not like")
				{
					sb.Append(fieldname).Append(" not like ");
				}
                else if (condstr == "contain")
                {
                    sb.Append(fieldname).Append(" like ");
                }
                else if (condstr == "not contain")
                {
                    sb.Append(fieldname).Append(" not like ");
                }
                else if (condstr == "is null")
				{
					sb.Append(fieldname).Append(" is null ");
				}
				else if (condstr == "is null and = \'\'")
				{
					sb.Append(fieldname).AppendFormat(" is null and {0} = \'\' ", fieldname);
				}
				else if (condstr == "is not null")
				{
					sb.Append(fieldname).Append(" is not null ");
				}
				else if (condstr == "is not null and != \'\'")
				{
					sb.Append(fieldname).AppendFormat(" is not null and {0} != \'\' ", fieldname);
				}
				else
				{
					sb.Append(fieldname).Append(" ").AppendFormat(condstr, fieldname).Append(" ");
				}

				if (!condstr.StartsWith("is"))
				{
					string valtype = (string)dr[FIELDTYPE];
					// 値
					if (valtype == "nvarchar" ||
						valtype == "nchar" ||
						valtype == "xml" ||
						valtype == "sql_variant" ||
						valtype == "ntext")
					{
                        if (condstr == "contain" || condstr == "not contain")
                        {
                            sb.Append(@"N'%").Append(values).Append(@"%'");
                        }
                        else
                        {
                            sb.Append(@"N'").Append(values).Append(@"'");
                        }
                    }
					else if (valtype == "varchar" ||
						valtype == "char" ||
						valtype == "text")
					{
                        if (condstr == "contain" || condstr == "not contain")
                        {
                            sb.Append(@"'%").Append(values).Append(@"%'");
                        }
                        else
                        {
                            sb.Append(@"'").Append(values).Append(@"'");
                        }
                    }
					else if (valtype.Equals("datetime") ||
						valtype.Equals("smalldatetime") ||
						valtype.Equals("time") ||
						valtype.Equals("date") ||
						valtype.Equals("datetime2") ||
						valtype.Equals("datetimeoffset"))
					{
                        if (condstr == "contain" || condstr == "not contain")
                        {
                            sb.Append(@"N'%").Append(values).Append(@"%'");
                        }
                        else
                        {
                            sb.Append(@"N'").Append(values).Append(@"'");
                        }
                    }
					else
					{
						sb.Append(values);
					}
				}

                string collatestr = (string)dr[COLLATETYPE];
                if (string.IsNullOrEmpty(collatestr) )
                {
                    ;
                }
                else
                {
                    sb.AppendFormat(" collate {0} ", collatestr);
                }

				sb.Append(" )\r\n");
			}
			this.txtZoom.Text = sb.ToString();
		}

		/// <summary>
		/// 全ての条件をクリアする
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnClear_Click(object sender, EventArgs e)
		{
			DispFieldCondition();
		}

		/// <summary>
		/// フィールド等の表示内容を設定しなおす
		/// </summary>
		public void ResetTarget()
		{
			DispFieldCondition();
		}

		/// <summary>
		/// AND/OR の条件変更があった
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cmbCondition_TextChanged(object sender, EventArgs e)
		{
			SetFieldCondResult();
		}

		/// <summary>
		/// 閉じる処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnClose_Click(object sender, EventArgs e)
		{
			// 画面を閉じる場合でも、非表示にし、実体を close しない
			this.Visible = false;
		}

		/// <summary>
		/// 画面を閉じる場合でも、非表示にし、実体を Dispose しない
		/// </summary>
		/// <param name="e"></param>
		protected override void OnClosing(CancelEventArgs e)
		{
			this.Visible = false;
			e.Cancel = true;
		}

		private void WhereDialog_VisibleChanged(object sender, EventArgs e)
		{
			this.txtZoom.Focus();
		}

		private void fieldCondition_CellLeave(object sender, DataGridViewCellEventArgs e)
		{
		}

		private void fieldCondition_CellEnter(object sender, DataGridViewCellEventArgs e)
		{
		}

		private void fieldCondition_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
		{
			if (this.forceCellRow != -1 ||
				this.forceCellColumn != -1)
			{
				if (e.RowIndex != this.forceCellRow ||
					e.ColumnIndex != this.forceCellColumn)
				{
					this.fieldCondition.CurrentCell = this.fieldCondition[this.forceCellColumn, this.forceCellRow];
					this.forceCellColumn = -1;
					this.forceCellRow = -1;
					this.fieldCondition.BeginEdit(true);
				}
				else
				{
					this.forceCellColumn = -1;
					this.forceCellRow = -1;
				}
			}
		}

        private void FieldCondition_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == COL_CLEAR_BUTTON)
            {
                this.fieldCondition[COL_COND, e.RowIndex].Value = "";
                this.fieldCondition[COL_VAL, e.RowIndex].Value = "";
                this.fieldCondition[COL_COLLATE, e.RowIndex].Value = "";
            }
            SetFieldCondResult();

        }

        private void btnCollateAllSet_Click(object sender, EventArgs e)
        {
            string collateStr = string.Empty;
            if (this.cmbCollate.SelectedIndex >= 0)
            {
                collateStr = this.cmbCollate.Items[this.cmbCollate.SelectedIndex].ToString();
            }
            for (int rLine = 0; rLine < fieldDs.Tables[BASETABLE].Rows.Count; rLine++)
            {
                DataRow each = fieldDs.Tables[BASETABLE].Rows[rLine];
                
                if ((string)each[CANCOLLATE] == "1")
                {
                    fieldCondition[COLLATETYPE, rLine].Value = collateStr;
                    fieldCondition[COLLATETYPE, rLine].ReadOnly = false;
                    fieldCondition[COLLATETYPE, rLine].Style.BackColor = Color.White;
                }
                else
                {
                    fieldCondition[COLLATETYPE, rLine].Value = "";
                    fieldCondition[COLLATETYPE, rLine].ReadOnly = true;
                    fieldCondition[COLLATETYPE, rLine].Style.BackColor = Color.LightGray;
                }

            }
        }

        private void OkOrApply(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtZoom.Text)||
                this.fieldDs == null
                )
            {
                return;
            }
            if (!this.WhereHistory.ContainsKey(this.DbName))
            {
                this.WhereHistory[DbName] = new Dictionary<string, Dictionary<string, DataSet>>();
            }
            if (!this.WhereHistory[DbName].ContainsKey(this.pTargetObject.FormalName))
            {
                this.WhereHistory[DbName][this.pTargetObject.FormalName] = new Dictionary<string, DataSet>();
            }
            this.WhereHistory[DbName][this.pTargetObject.FormalName][this.txtZoom.Text.Replace("\r\n","")] = this.fieldDs.Copy();
        }

        /// <summary>
        /// テキストの値が外部から変更された場合の処理
        /// </summary>
        protected override void TextValuesChanged()
        {
            Console.WriteLine("TextValuesChanged");
            string textkey = this.txtZoom.Text.Replace("\r\n", "");
            if (string.IsNullOrEmpty(textkey)) 
            {
                return;
            }
            if (!this.WhereHistory.ContainsKey(this.DbName)) 
            {
                return;
            }
            if (!this.WhereHistory[DbName].ContainsKey(this.pTargetObject.FormalName))
            {
                return;
            }
            if (!this.WhereHistory[DbName][this.pTargetObject.FormalName].ContainsKey(textkey))
            {
                return;
            }
            DataSet ds = this.WhereHistory[DbName][this.pTargetObject.FormalName][textkey];
            if (!ds.Tables.Contains(BASETABLE))
            {
                return;
            }
            if (ds.Tables[BASETABLE].Rows.Count == 0)
            {
                return;
            }
        //private const string CLEARBUTTON = "ClearButton";
        //private const string FIELDNAME = "Fields";
        //private const string CONDCMB = "cond";
        //private const string VALUES = "datavalues";
        //private const string FIELDTYPE = "fieldtypes";
        //private const string COLLATETYPE = "collatetype";
        //private const string CANCOLLATE = "cancollate";
            foreach (DataRow dr in ds.Tables[BASETABLE].Rows)
            {
                foreach (DataRow fdr in this.fieldDs.Tables[BASETABLE].Rows)
                {
                    if ((string)dr[FIELDNAME] == (string)fdr[FIELDNAME])
                    {
                        fdr[CONDCMB] = (string)dr[CONDCMB];
                        fdr[VALUES] = (string)dr[VALUES];
                        fdr[COLLATETYPE] = (string)dr[COLLATETYPE];
                        break;
                    }
                }
            }
        }
    }
}

