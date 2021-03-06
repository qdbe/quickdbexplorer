using System;
using System.Collections;
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
		private System.Windows.Forms.CheckBox chkShowObjectSelector;
		private System.Windows.Forms.CheckBox chkCaseSensitive;
		private System.Windows.Forms.CheckBox chkSchema;

		private SqlVersion pSqlVersion = null;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public SearchConditionDlg(SqlVersion sqlVer)
		{
			// この呼び出しは Windows フォーム デザイナで必要です。
			InitializeComponent();

			this.pSqlVersion = sqlVer;
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
		/// フィールド名を検索するか否か
		/// </summary>
		public bool IsSearchField
		{
			get { return this.chkField.Checked; }
		}

		/// <summary>
		/// テーブル名を検索するか否か
		/// </summary>
		public bool IsSearchTable
		{
			get { return this.chkTable.Checked; }
		}

		/// <summary>
		/// View名を検索するか否か
		/// </summary>
		public bool IsSearchView
		{
			get { return this.chkView.Checked; }
		}

		/// <summary>
		/// Synonym名を検索するか否か
		/// </summary>
		public bool IsSearchSynonym
		{
			get { return this.chkSynonym.Checked; }
		}

		/// <summary>
		/// ストアドプロシージャーを検索するか否か
		/// </summary>
		public bool IsSearchProcedure
		{
			get { return false; }
		}

		/// <summary>
		/// 関数を検索するか否か
		/// </summary>
		public bool IsSearchFunction
		{
			get { return false; }
		}

		/// <summary>
		/// 検索結果を元にオブジェクトを選択するか否か
		/// </summary>
		public bool IsShowTableSelect
		{
			get { return this.chkShowObjectSelector.Checked; }
		}

		/// <summary>
		/// 大文字・小文字を区別するか否か
		/// </summary>
		public bool IsCaseSensitive
		{
			get { return this.chkCaseSensitive.Checked; }
		}

		/// <summary>
		/// 現在表示中のスキーマのみ対象とするか否か
		/// </summary>
		public bool IsSchemaOnly
		{
			get { return this.chkSchema.Checked; }
		}

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
			this.chkShowObjectSelector = new System.Windows.Forms.CheckBox();
			this.chkSchema = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtInput
			// 
			this.txtInput.CanCtrlDelete = true;
			this.txtInput.Location = new System.Drawing.Point(16, 38);
			this.txtInput.Multiline = false;
			this.txtInput.Name = "txtInput";
			this.txtInput.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.txtInput.Size = new System.Drawing.Size(444, 19);
			this.txtInput.TabIndex = 1;
			// 
			// btnGo
			// 
			this.btnGo.DialogResult = System.Windows.Forms.DialogResult.None;
			this.btnGo.Location = new System.Drawing.Point(16, 251);
			this.btnGo.Name = "btnGo";
			this.btnGo.TabIndex = 6;
			this.btnGo.Text = "決定(&O)";
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(348, 251);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 8;
			// 
			// chkReturn
			// 
			this.chkReturn.Location = new System.Drawing.Point(252, 14);
			this.chkReturn.Name = "chkReturn";
			this.chkReturn.Size = new System.Drawing.Size(208, 18);
			this.chkReturn.Visible = false;
			// 
			// btnHistory
			// 
			this.btnHistory.Location = new System.Drawing.Point(144, 251);
			this.btnHistory.Name = "btnHistory";
			this.btnHistory.TabIndex = 7;
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
			this.groupBox2.TabIndex = 4;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "検索方法";
			// 
			// chkCaseSensitive
			// 
			this.chkCaseSensitive.Checked = true;
			this.chkCaseSensitive.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkCaseSensitive.Location = new System.Drawing.Point(16, 116);
			this.chkCaseSensitive.Name = "chkCaseSensitive";
			this.chkCaseSensitive.Size = new System.Drawing.Size(152, 24);
			this.chkCaseSensitive.TabIndex = 3;
			this.chkCaseSensitive.Text = "大文字小文字を区別する";
			// 
			// rdoStartWith
			// 
			this.rdoStartWith.Location = new System.Drawing.Point(14, 50);
			this.rdoStartWith.Name = "rdoStartWith";
			this.rdoStartWith.TabIndex = 1;
			this.rdoStartWith.Text = "前方一致";
			// 
			// rdoContain
			// 
			this.rdoContain.Checked = true;
			this.rdoContain.Location = new System.Drawing.Point(14, 20);
			this.rdoContain.Name = "rdoContain";
			this.rdoContain.TabIndex = 0;
			this.rdoContain.TabStop = true;
			this.rdoContain.Text = "曖昧検索";
			// 
			// rdoExact
			// 
			this.rdoExact.Location = new System.Drawing.Point(14, 80);
			this.rdoExact.Name = "rdoExact";
			this.rdoExact.TabIndex = 2;
			this.rdoExact.Text = "完全一致";
			// 
			// chkShowObjectSelector
			// 
			this.chkShowObjectSelector.Checked = true;
			this.chkShowObjectSelector.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkShowObjectSelector.Location = new System.Drawing.Point(18, 218);
			this.chkShowObjectSelector.Name = "chkShowObjectSelector";
			this.chkShowObjectSelector.Size = new System.Drawing.Size(222, 24);
			this.chkShowObjectSelector.TabIndex = 5;
			this.chkShowObjectSelector.Text = "検索結果を元にオブジェクトを選択する";
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
			// SearchConditionDlg
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(480, 281);
			this.Controls.Add(this.chkSchema);
			this.Controls.Add(this.chkShowObjectSelector);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label1);
			this.MaximizeBox = false;
			this.Name = "SearchConditionDlg";
			this.Text = "フィールド検索";
			this.Load += new System.EventHandler(this.SearchCondition_Load);
			this.Controls.SetChildIndex(this.btnCancel, 0);
			this.Controls.SetChildIndex(this.chkReturn, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.groupBox1, 0);
			this.Controls.SetChildIndex(this.groupBox2, 0);
			this.Controls.SetChildIndex(this.chkShowObjectSelector, 0);
			this.Controls.SetChildIndex(this.chkSchema, 0);
			this.Controls.SetChildIndex(this.txtInput, 0);
			this.Controls.SetChildIndex(this.btnGo, 0);
			this.Controls.SetChildIndex(this.btnHistory, 0);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		internal override void btnGo_Click(object sender, System.EventArgs e)
		{
			SelectSql = this.txtInput.Text;
			if( SelectSql == string.Empty )
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
			this.HasReturn = false;
			this.DialogResult = DialogResult.OK;
            this.txtInput.SaveHistory("");
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
			}
			else
			{
				this.chkSynonym.Visible = true;
				this.chkSynonym.Enabled = true;
				this.chkSynonym.Checked = true;
			}
		}
	}
}

