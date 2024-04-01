using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace quickDBExplorer
{
	/// <summary>
	/// 検索条件を入力するダイアログクラス
	/// </summary>
	public class SearchConditionDlg : quickDBExplorer.QueryDialog
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox chkField;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton rdoStartWith;
		private System.Windows.Forms.RadioButton rdoContain;
		private System.Windows.Forms.RadioButton rdoExact;
		private System.Windows.Forms.CheckBox chkTable;
		private System.Windows.Forms.CheckBox chkView;
		private System.Windows.Forms.CheckBox chkSynonym;
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.CheckBox chkCaseSensitive;
		private System.Windows.Forms.CheckBox chkSchema;
        private GroupBox groupBox3;
        private CheckBox chkTargetSynonym;
        private CheckBox chkTargetView;
        private CheckBox chkTargetTable;
        private GroupBox groupBox4;
        private RadioButton rdoObjectFilter;
        private RadioButton rdoShowObjectSelector;
        private Label label2;
        private GroupBox groupBox5;
        private quickDBExplorerTextBox txtExcludeField;
        private quickDBExplorerTextBox txtExcludeObjName;
        private Label label3;
        private quickDBExplorerTextBox txtExcludeFieldName;
        private Label label4;
        private SqlVersion pSqlVersion = null;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public SearchConditionDlg(SqlVersion sqlVer)
		{
			// この呼び出しは Windows フォーム デザイナで必要です。
			InitializeComponent();

			this.pSqlVersion = sqlVer;
            this.IsViewShow = true;

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



        /// <summary>
        /// 画面で設定された検索条件
        /// </summary>
        public ObjectSearchCondition Condition
        {
            get
            {
                ObjectSearchCondition result = new ObjectSearchCondition();
                result.IsSearchField = this.chkField.Checked;
                result.IsSearchTable = this.chkTable.Checked;
                result.IsSearchView = this.chkView.Checked;
                result.IsSearchSynonym = this.chkSynonym.Checked;
                result.IsSearchProcedure = false;
                result.IsSearchFunction = false;
                result.IsFieldTable = this.chkTargetTable.Checked;
                result.IsFieldView = this.chkTargetView.Checked;
                result.IsFieldSynonym = this.chkTargetSynonym.Checked;
                result.IsShowTableSelect = this.rdoShowObjectSelector.Checked;
                result.IsTableFilter = this.rdoObjectFilter.Checked;
                result.IsCaseSensitive = this.chkCaseSensitive.Checked;
                result.IsSchemaOnly = this.chkSchema.Checked;
                result.ExcludeField = this.txtExcludeField.Text;
                result.ExcludeFieldName = this.txtExcludeFieldName.Text;
                result.ExcludeObjName = this.txtExcludeObjName.Text;
                return result;
            }
        }

        /// <summary>
        /// オブジェクト検索履歴（設定含む）
        /// DBName,検索文字,ObjectSearchCondition
        /// </summary>
        public Dictionary<string, Dictionary<string, ObjectSearchCondition>> ObjectSearchHistory { get; set; }

        /// <summary>
        /// DB名
        /// </summary>
        public string DbName { get; set; }

        /// <summary>
        /// Viewを表示対象に含めるか否か
        /// </summary>
        public bool IsViewShow { get; set; }

        /// <summary>
        /// 検索方法を取得する
        /// </summary>
        public quickDBExplorer.SearchType SearchType
		{
			get 
			{
				if( this.rdoContain.Checked == true )
				{
					return quickDBExplorer.SearchType.SearchContain;
				}
				else if( this.rdoStartWith.Checked == true )
				{
					return quickDBExplorer.SearchType.SearchStartWith;
				}
				else 
				{
					return quickDBExplorer.SearchType.SearchExact;
				}
			}
		}

		#region デザイナで生成されたコード
		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
            this.label1 = new System.Windows.Forms.Label();
            this.chkField = new System.Windows.Forms.CheckBox();
            this.chkTable = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkSynonym = new System.Windows.Forms.CheckBox();
            this.chkView = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkCaseSensitive = new System.Windows.Forms.CheckBox();
            this.rdoStartWith = new System.Windows.Forms.RadioButton();
            this.rdoContain = new System.Windows.Forms.RadioButton();
            this.rdoExact = new System.Windows.Forms.RadioButton();
            this.chkSchema = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkTargetSynonym = new System.Windows.Forms.CheckBox();
            this.chkTargetView = new System.Windows.Forms.CheckBox();
            this.chkTargetTable = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rdoObjectFilter = new System.Windows.Forms.RadioButton();
            this.rdoShowObjectSelector = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtExcludeFieldName = new quickDBExplorer.quickDBExplorerTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtExcludeObjName = new quickDBExplorer.quickDBExplorerTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtExcludeField = new quickDBExplorer.quickDBExplorerTextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(16, 38);
            this.txtInput.Multiline = false;
            this.txtInput.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtInput.Size = new System.Drawing.Size(442, 19);
            this.txtInput.TabIndex = 1;
            this.txtInput.qdbeTextChanged += new quickDBExplorer.qdbeTextChangedEventHandler(this.txtInput_qdbeTextChanged);
            // 
            // btnGo
            // 
            this.btnGo.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnGo.Location = new System.Drawing.Point(16, 619);
            this.btnGo.TabIndex = 7;
            this.btnGo.Text = "決定(&O)";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(346, 619);
            this.btnCancel.TabIndex = 9;
            // 
            // chkReturn
            // 
            this.chkReturn.Location = new System.Drawing.Point(258, 461);
            this.chkReturn.Size = new System.Drawing.Size(208, 18);
            this.chkReturn.Visible = false;
            // 
            // btnHistory
            // 
            this.btnHistory.Location = new System.Drawing.Point(144, 619);
            this.btnHistory.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(18, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(430, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "検索する名前を指定してください。検索結果はクリップボードに転送されます。";
            // 
            // chkField
            // 
            this.chkField.Checked = true;
            this.chkField.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkField.Location = new System.Drawing.Point(14, 18);
            this.chkField.Name = "chkField";
            this.chkField.Size = new System.Drawing.Size(104, 20);
            this.chkField.TabIndex = 0;
            this.chkField.Text = "フィールド名";
            this.chkField.CheckedChanged += new System.EventHandler(this.chkField_CheckedChanged);
            // 
            // chkTable
            // 
            this.chkTable.Checked = true;
            this.chkTable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTable.Location = new System.Drawing.Point(14, 40);
            this.chkTable.Name = "chkTable";
            this.chkTable.Size = new System.Drawing.Size(180, 20);
            this.chkTable.TabIndex = 1;
            this.chkTable.Text = "テーブル名";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkSynonym);
            this.groupBox1.Controls.Add(this.chkField);
            this.groupBox1.Controls.Add(this.chkTable);
            this.groupBox1.Controls.Add(this.chkView);
            this.groupBox1.Location = new System.Drawing.Point(16, 100);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 112);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "検索対象";
            // 
            // chkSynonym
            // 
            this.chkSynonym.Checked = true;
            this.chkSynonym.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSynonym.Location = new System.Drawing.Point(14, 84);
            this.chkSynonym.Name = "chkSynonym";
            this.chkSynonym.Size = new System.Drawing.Size(180, 20);
            this.chkSynonym.TabIndex = 2;
            this.chkSynonym.Text = "Synonym 名";
            // 
            // chkView
            // 
            this.chkView.Checked = true;
            this.chkView.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkView.Location = new System.Drawing.Point(14, 62);
            this.chkView.Name = "chkView";
            this.chkView.Size = new System.Drawing.Size(180, 20);
            this.chkView.TabIndex = 1;
            this.chkView.Text = "View名";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkCaseSensitive);
            this.groupBox2.Controls.Add(this.rdoStartWith);
            this.groupBox2.Controls.Add(this.rdoContain);
            this.groupBox2.Controls.Add(this.rdoExact);
            this.groupBox2.Location = new System.Drawing.Point(246, 98);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(204, 146);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "検索方法";
            // 
            // chkCaseSensitive
            // 
            this.chkCaseSensitive.Checked = true;
            this.chkCaseSensitive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCaseSensitive.Location = new System.Drawing.Point(14, 110);
            this.chkCaseSensitive.Name = "chkCaseSensitive";
            this.chkCaseSensitive.Size = new System.Drawing.Size(152, 24);
            this.chkCaseSensitive.TabIndex = 3;
            this.chkCaseSensitive.Text = "大文字小文字を区別する";
            // 
            // rdoStartWith
            // 
            this.rdoStartWith.Location = new System.Drawing.Point(14, 50);
            this.rdoStartWith.Name = "rdoStartWith";
            this.rdoStartWith.Size = new System.Drawing.Size(104, 24);
            this.rdoStartWith.TabIndex = 1;
            this.rdoStartWith.Text = "前方一致";
            // 
            // rdoContain
            // 
            this.rdoContain.Checked = true;
            this.rdoContain.Location = new System.Drawing.Point(14, 20);
            this.rdoContain.Name = "rdoContain";
            this.rdoContain.Size = new System.Drawing.Size(104, 24);
            this.rdoContain.TabIndex = 0;
            this.rdoContain.TabStop = true;
            this.rdoContain.Text = "曖昧検索";
            // 
            // rdoExact
            // 
            this.rdoExact.Location = new System.Drawing.Point(14, 80);
            this.rdoExact.Name = "rdoExact";
            this.rdoExact.Size = new System.Drawing.Size(104, 24);
            this.rdoExact.TabIndex = 2;
            this.rdoExact.Text = "完全一致";
            // 
            // chkSchema
            // 
            this.chkSchema.Checked = true;
            this.chkSchema.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSchema.Location = new System.Drawing.Point(20, 66);
            this.chkSchema.Name = "chkSchema";
            this.chkSchema.Size = new System.Drawing.Size(178, 24);
            this.chkSchema.TabIndex = 2;
            this.chkSchema.Text = "表示中のスキーマのみ対象";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkTargetSynonym);
            this.groupBox3.Controls.Add(this.chkTargetView);
            this.groupBox3.Controls.Add(this.chkTargetTable);
            this.groupBox3.Location = new System.Drawing.Point(16, 219);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 91);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "フィールド対象";
            // 
            // chkTargetSynonym
            // 
            this.chkTargetSynonym.AutoSize = true;
            this.chkTargetSynonym.Checked = true;
            this.chkTargetSynonym.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTargetSynonym.Location = new System.Drawing.Point(14, 63);
            this.chkTargetSynonym.Name = "chkTargetSynonym";
            this.chkTargetSynonym.Size = new System.Drawing.Size(70, 16);
            this.chkTargetSynonym.TabIndex = 2;
            this.chkTargetSynonym.Text = "Synonym";
            this.chkTargetSynonym.UseVisualStyleBackColor = true;
            // 
            // chkTargetView
            // 
            this.chkTargetView.AutoSize = true;
            this.chkTargetView.Checked = true;
            this.chkTargetView.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTargetView.Location = new System.Drawing.Point(14, 41);
            this.chkTargetView.Name = "chkTargetView";
            this.chkTargetView.Size = new System.Drawing.Size(51, 16);
            this.chkTargetView.TabIndex = 1;
            this.chkTargetView.Text = "VIEW";
            this.chkTargetView.UseVisualStyleBackColor = true;
            // 
            // chkTargetTable
            // 
            this.chkTargetTable.AutoSize = true;
            this.chkTargetTable.Checked = true;
            this.chkTargetTable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTargetTable.Location = new System.Drawing.Point(14, 19);
            this.chkTargetTable.Name = "chkTargetTable";
            this.chkTargetTable.Size = new System.Drawing.Size(62, 16);
            this.chkTargetTable.TabIndex = 0;
            this.chkTargetTable.Text = "テーブル";
            this.chkTargetTable.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rdoObjectFilter);
            this.groupBox4.Controls.Add(this.rdoShowObjectSelector);
            this.groupBox4.Location = new System.Drawing.Point(16, 316);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(434, 88);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "処理";
            // 
            // rdoObjectFilter
            // 
            this.rdoObjectFilter.AutoSize = true;
            this.rdoObjectFilter.Location = new System.Drawing.Point(14, 52);
            this.rdoObjectFilter.Name = "rdoObjectFilter";
            this.rdoObjectFilter.Size = new System.Drawing.Size(203, 16);
            this.rdoObjectFilter.TabIndex = 1;
            this.rdoObjectFilter.Text = "検索結果を元にオブジェクトを絞り込む";
            this.rdoObjectFilter.UseVisualStyleBackColor = true;
            // 
            // rdoShowObjectSelector
            // 
            this.rdoShowObjectSelector.AutoSize = true;
            this.rdoShowObjectSelector.Checked = true;
            this.rdoShowObjectSelector.Location = new System.Drawing.Point(14, 18);
            this.rdoShowObjectSelector.Name = "rdoShowObjectSelector";
            this.rdoShowObjectSelector.Size = new System.Drawing.Size(204, 16);
            this.rdoShowObjectSelector.TabIndex = 0;
            this.rdoShowObjectSelector.TabStop = true;
            this.rdoShowObjectSelector.Text = "検索結果を元にオブジェクトを選択する";
            this.rdoShowObjectSelector.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(285, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "以下のフィールドを含まない(テーブル、VIEW検索時も有効）";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtExcludeFieldName);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.txtExcludeObjName);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.txtExcludeField);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Location = new System.Drawing.Point(16, 410);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(432, 194);
            this.groupBox5.TabIndex = 12;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "除外設定";
            // 
            // txtExcludeFieldName
            // 
            this.txtExcludeFieldName.CanCtrlDelete = true;
            this.txtExcludeFieldName.Histories = null;
            this.txtExcludeFieldName.HistoryKey = "quickDBExplorerTextBox1";
            this.txtExcludeFieldName.IsDigitOnly = false;
            this.txtExcludeFieldName.IsShowZoom = false;
            this.txtExcludeFieldName.Location = new System.Drawing.Point(16, 97);
            this.txtExcludeFieldName.Name = "txtExcludeFieldName";
            this.txtExcludeFieldName.PlaceholderColor = System.Drawing.Color.Empty;
            this.txtExcludeFieldName.PlaceholderText = "";
            this.txtExcludeFieldName.Size = new System.Drawing.Size(328, 19);
            this.txtExcludeFieldName.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(297, 12);
            this.label4.TabIndex = 15;
            this.label4.Text = "以下をフィールド名検索から除外（フィールド検索時のみ有効）";
            // 
            // txtExcludeObjName
            // 
            this.txtExcludeObjName.CanCtrlDelete = true;
            this.txtExcludeObjName.Histories = null;
            this.txtExcludeObjName.HistoryKey = "quickDBExplorerTextBox1";
            this.txtExcludeObjName.IsDigitOnly = false;
            this.txtExcludeObjName.IsShowZoom = false;
            this.txtExcludeObjName.Location = new System.Drawing.Point(16, 154);
            this.txtExcludeObjName.Name = "txtExcludeObjName";
            this.txtExcludeObjName.PlaceholderColor = System.Drawing.Color.Empty;
            this.txtExcludeObjName.PlaceholderText = "";
            this.txtExcludeObjName.Size = new System.Drawing.Size(328, 19);
            this.txtExcludeObjName.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(277, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "以下のオブジェクト名を含まない（フィールド検索時も有効）";
            // 
            // txtExcludeField
            // 
            this.txtExcludeField.CanCtrlDelete = true;
            this.txtExcludeField.Histories = null;
            this.txtExcludeField.HistoryKey = "quickDBExplorerTextBox1";
            this.txtExcludeField.IsDigitOnly = false;
            this.txtExcludeField.IsShowZoom = false;
            this.txtExcludeField.Location = new System.Drawing.Point(14, 45);
            this.txtExcludeField.Name = "txtExcludeField";
            this.txtExcludeField.PlaceholderColor = System.Drawing.Color.Empty;
            this.txtExcludeField.PlaceholderText = "";
            this.txtExcludeField.Size = new System.Drawing.Size(328, 19);
            this.txtExcludeField.TabIndex = 12;
            // 
            // SearchConditionDlg
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
            this.ClientSize = new System.Drawing.Size(478, 649);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.chkSchema);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "SearchConditionDlg";
            this.Text = "フィールド検索";
            this.Load += new System.EventHandler(this.SearchCondition_Load);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.chkSchema, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.groupBox4, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.chkReturn, 0);
            this.Controls.SetChildIndex(this.txtInput, 0);
            this.Controls.SetChildIndex(this.btnGo, 0);
            this.Controls.SetChildIndex(this.btnHistory, 0);
            this.Controls.SetChildIndex(this.groupBox5, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		internal override void btnGo_Click(object sender, System.EventArgs e)
		{
			InputedText = this.txtInput.Text;
			if( InputedText == string.Empty )
			{
				MessageBox.Show("何か条件を指定してください");
				return;
			}
			if( this.chkField.Checked == false &&
				this.chkTable.Checked == false &&
				this.chkView.Checked == false &&
				this.chkSynonym.Checked == false )
			{
				MessageBox.Show("検索対象は必ず一つ以上指定して下さい");
				return;
			}
            if (this.chkField.Checked == true &&
                this.chkTargetTable.Checked == false &&
                this.chkTargetView.Checked == false &&
                this.chkTargetSynonym.Checked == false)
            {
                MessageBox.Show("フィールド対象は必ず一つ以上指定して下さい");
                return;
            }
            this.HasReturn = false;
			this.DialogResult = DialogResult.OK;
            this.txtInput.SaveHistory("");
            if (!this.ObjectSearchHistory.ContainsKey(this.DbName))
            {
                this.ObjectSearchHistory[this.DbName] = new Dictionary<string, ObjectSearchCondition>();
            }
            this.ObjectSearchHistory[this.DbName][this.txtInput.Text] = this.Condition;
		}

		/// <summary>
		/// 画面の初期設定
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SearchCondition_Load(object sender, System.EventArgs e)
		{
			if( this.pSqlVersion.CanUseSynonym == false)
			{
				this.chkSynonym.Visible = false;
				this.chkSynonym.Enabled = false;
				this.chkSynonym.Checked = false;
                this.chkTargetSynonym.Visible = false;
                this.chkTargetSynonym.Enabled = false;
                this.chkTargetSynonym.Checked = false;
			}
			else
			{
				this.chkSynonym.Visible = true;
				this.chkSynonym.Enabled = true;
				this.chkSynonym.Checked = true;
                this.chkTargetSynonym.Visible = true;
                this.chkTargetSynonym.Enabled = true;
                this.chkTargetSynonym.Checked = true;
            }

            SetFieldCondition();

            if (this.IsViewShow == false)
            {
                this.chkTargetView.Enabled = false;
                this.chkTargetView.Checked = false;
                this.chkView.Enabled = false;
                this.chkView.Checked = false;
            }
        }

        private void chkField_CheckedChanged(object sender, EventArgs e)
        {
            SetFieldCondition();
        }

        /// <summary>
        /// フィールド条件の設定
        /// </summary>
        private void SetFieldCondition()
        {
            if (this.chkField.Checked)
            {
                this.groupBox3.Enabled = true;
                this.chkTargetTable.Enabled = true;
                this.chkTargetView.Enabled = true;
                this.chkTargetSynonym.Enabled = true;
            }
            else
            {
                this.groupBox3.Enabled = false;
                this.chkTargetTable.Enabled = false;
                this.chkTargetView.Enabled = false;
                this.chkTargetSynonym.Enabled = false;
                this.chkTargetTable.Checked = false;
                this.chkTargetView.Checked = false;
                this.chkTargetSynonym.Checked = false;
            }
        }

        private void txtInput_qdbeTextChanged(object sender, qdbeTextChangedEventArgs e)
        {
            if (!this.ObjectSearchHistory.ContainsKey(this.DbName))
            {
                this.ObjectSearchHistory[this.DbName] = new Dictionary<string, ObjectSearchCondition>();
            }
            ObjectSearchCondition opt = null;
            if (this.ObjectSearchHistory[this.DbName].ContainsKey(e.NewText))
            {

                opt = this.ObjectSearchHistory[this.DbName][e.NewText];
            }
            else
            {
                opt = new ObjectSearchCondition();
            }
            SetOption(opt);
        }

        private void SetOption(ObjectSearchCondition opt)
        {
            this.chkField.Checked = opt.IsSearchField;
            this.chkTable.Checked = opt.IsSearchTable;
            this.chkView.Checked = opt.IsSearchView;
            this.chkSynonym.Checked = opt.IsSearchSynonym;
            //opt.IsSearchProcedure = false;
            //opt.IsSearchFunction = false;
            this.chkTargetTable.Checked = opt.IsFieldTable;
            this.chkTargetView.Checked = opt.IsFieldView;
            this.chkTargetSynonym.Checked = opt.IsFieldSynonym;
            this.rdoShowObjectSelector.Checked = opt.IsShowTableSelect;
            this.rdoObjectFilter.Checked = opt.IsTableFilter;
            this.chkCaseSensitive.Checked = opt.IsCaseSensitive;
            this.chkSchema.Checked = opt.IsSchemaOnly;
            this.txtExcludeField.Text = opt.ExcludeField;
            this.txtExcludeFieldName.Text = opt.ExcludeFieldName;
            this.txtExcludeObjName.Text = opt.ExcludeObjName;
        }
    }
}

