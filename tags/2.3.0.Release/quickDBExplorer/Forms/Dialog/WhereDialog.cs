using System;
using System.Collections;
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
		private const string FIELDNAME = "Fields";
		private const string CONDCMB = "cond";
		private const string VALUES = "datavalues";
		private const string FIELDTYPE = "fieldtypes";

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
		private DataGridViewTextBoxColumn Fields;
		private DataGridViewTextBoxColumn fieldtypes;
		private DataGridViewComboBoxColumn cond;
		private DataGridViewTextBoxColumn datavalues;
		
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
			this.fieldCondition = new System.Windows.Forms.DataGridView();
			this.Fields = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.fieldtypes = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cond = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.datavalues = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.btnClear = new System.Windows.Forms.Button();
			this.cmbCondition = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this.fieldCondition)).BeginInit();
			this.SuspendLayout();
			// 
			// chkStayOnTop
			// 
			this.chkStayOnTop.TabIndex = 8;
			// 
			// btnApply
			// 
			this.btnApply.Location = new System.Drawing.Point(462, 362);
			this.btnApply.TabIndex = 6;
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
			this.btnOk.TabIndex = 5;
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(558, 362);
			this.btnClose.TabIndex = 7;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// MsgArea
			// 
			this.MsgArea.Location = new System.Drawing.Point(106, 362);
			this.MsgArea.Size = new System.Drawing.Size(350, 26);
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
            this.Fields,
            this.fieldtypes,
            this.cond,
            this.datavalues});
			this.fieldCondition.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.fieldCondition.Location = new System.Drawing.Point(14, 169);
			this.fieldCondition.Name = "fieldCondition";
			this.fieldCondition.RowHeadersVisible = false;
			this.fieldCondition.RowTemplate.Height = 21;
			this.fieldCondition.Size = new System.Drawing.Size(622, 160);
			this.fieldCondition.TabIndex = 3;
			this.fieldCondition.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.fieldCondition_CellLeave);
			this.fieldCondition.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.fieldCondition_CellEndEdit);
			this.fieldCondition.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.fieldCondition_CellEnter);
			// 
			// Fields
			// 
			this.Fields.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.Fields.DataPropertyName = "Fields";
			this.Fields.HeaderText = "フィールド名";
			this.Fields.Name = "Fields";
			this.Fields.ReadOnly = true;
			this.Fields.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.Fields.Width = 42;
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
            "like",
            "not like",
            "is null",
            "is null and = \'\'",
            "is not null",
            "is not null and != \'\'",
            "<",
            ">",
            ">=",
            "<=",
			});
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
			// btnClear
			// 
			this.btnClear.Location = new System.Drawing.Point(14, 335);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(181, 23);
			this.btnClear.TabIndex = 4;
			this.btnClear.Text = "フィールド条件クリア";
			this.btnClear.UseVisualStyleBackColor = true;
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// cmbCondition
			// 
			this.cmbCondition.FormattingEnabled = true;
			this.cmbCondition.Items.AddRange(new object[] {
            "AND",
            "OR"});
			this.cmbCondition.Location = new System.Drawing.Point(14, 144);
			this.cmbCondition.Name = "cmbCondition";
			this.cmbCondition.Size = new System.Drawing.Size(154, 20);
			this.cmbCondition.TabIndex = 2;
			this.cmbCondition.SelectedIndexChanged += new System.EventHandler(this.cmbCondition_SelectedIndexChanged);
			this.cmbCondition.TextChanged += new System.EventHandler(this.cmbCondition_TextChanged);
			// 
			// WhereDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(648, 394);
			this.Controls.Add(this.fieldCondition);
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
			this.Controls.SetChildIndex(this.MsgArea, 0);
			this.Controls.SetChildIndex(this.fieldCondition, 0);
			this.Controls.SetChildIndex(this.txtZoom, 0);
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
			fieldDs.Tables[BASETABLE].Columns.Add(FIELDNAME);
			fieldDs.Tables[BASETABLE].Columns.Add(FIELDTYPE);
			fieldDs.Tables[BASETABLE].Columns.Add(CONDCMB);
			fieldDs.Tables[BASETABLE].Columns.Add(VALUES);
			// フィールド情報のセット
            foreach(DBFieldInfo each in this.pTargetObject.FieldInfo){
				DataRow dr = fieldDs.Tables[BASETABLE].NewRow();
                dr[FIELDNAME] = ((DBFieldInfo)each).Name;
                dr[FIELDTYPE] = ((DBFieldInfo)each).RealTypeName;
				dr[CONDCMB] = "";
				dr[VALUES] = "";
				this.fieldDs.Tables[BASETABLE].Rows.Add(dr);
			}

			// DataGrid の 表示を整える

			this.fieldCondition.DataSource = this.fieldDs;
			this.fieldCondition.DataMember = BASETABLE;
		}

		private void fieldCondition_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == 3 &&
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
						sb.Append(@"N'").Append(values).Append(@"'");
					}
					else if (valtype == "varchar" ||
						valtype == "char" ||
						valtype == "text")
					{
						sb.Append(@"'").Append(values).Append(@"'");
					}
					else if (valtype.Equals("datetime") ||
						valtype.Equals("smalldatetime") ||
						valtype.Equals("time") ||
						valtype.Equals("date") ||
						valtype.Equals("datetime2") ||
						valtype.Equals("datetimeoffset"))
					{
						sb.Append(@"N'").Append(values).Append(@"'");
					}
					else
					{
						sb.Append(values);
					}
				}

				sb.Append(" )\r\n");
			}
			this.txtZoom.Text = sb.ToString();
		}

		/// <summary>
		/// 条件をクリアする
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
	}
}

