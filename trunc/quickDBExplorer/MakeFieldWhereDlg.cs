using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Text;

namespace quickDBExplorer
{
	/// <summary>
	/// テーブルのフィールドを利用した条件句の生成指定を行う
	/// </summary>
	public class MakeFieldWhereDlg : quickDBExplorer.quickDBExplorerBaseForm
	{
		private System.Windows.Forms.TextBox txtAliasT1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cmbANDOR;
		private System.Windows.Forms.TextBox txtWhereResult;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox cmbCondition;
		private System.Windows.Forms.RadioButton rdoTable;
		private System.Windows.Forms.RadioButton rdoEtc;
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.TextBox txtAliasT2;

		/// <summary>
		/// 処理対象にするフィールドのリスト
		/// </summary>
		private  StringCollection pFieldList = null;

		/// <summary>
		/// 処理対象にするフィールドのリスト
		/// </summary>
		public StringCollection FieldList
		{
			get { return this.pFieldList; }
			set { this.pFieldList = value; }
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public MakeFieldWhereDlg()
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
			this.txtAliasT1 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtAliasT2 = new System.Windows.Forms.TextBox();
			this.cmbANDOR = new System.Windows.Forms.ComboBox();
			this.txtWhereResult = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.rdoTable = new System.Windows.Forms.RadioButton();
			this.label4 = new System.Windows.Forms.Label();
			this.rdoEtc = new System.Windows.Forms.RadioButton();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.cmbCondition = new System.Windows.Forms.ComboBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// MsgArea
			// 
			this.MsgArea.Location = new System.Drawing.Point(148, 320);
			this.MsgArea.Name = "MsgArea";
			this.MsgArea.Size = new System.Drawing.Size(496, 16);
			// 
			// txtAliasT1
			// 
			this.txtAliasT1.Location = new System.Drawing.Point(164, 60);
			this.txtAliasT1.Name = "txtAliasT1";
			this.txtAliasT1.TabIndex = 1;
			this.txtAliasT1.Text = "t1";
			this.txtAliasT1.TextChanged += new System.EventHandler(this.txtAliasT1_TextChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(164, 36);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 20);
			this.label1.TabIndex = 2;
			this.label1.Text = "テーブル名(1)";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(288, 36);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 20);
			this.label2.TabIndex = 2;
			this.label2.Text = "テーブル名(2)";
			// 
			// txtAliasT2
			// 
			this.txtAliasT2.Location = new System.Drawing.Point(284, 60);
			this.txtAliasT2.Name = "txtAliasT2";
			this.txtAliasT2.TabIndex = 1;
			this.txtAliasT2.Text = "t2";
			this.txtAliasT2.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// cmbANDOR
			// 
			this.cmbANDOR.Items.AddRange(new object[] {
														  "AND",
														  "OR"});
			this.cmbANDOR.Location = new System.Drawing.Point(160, 132);
			this.cmbANDOR.Name = "cmbANDOR";
			this.cmbANDOR.Size = new System.Drawing.Size(184, 20);
			this.cmbANDOR.TabIndex = 3;
			this.cmbANDOR.SelectedIndexChanged += new System.EventHandler(this.cmbANDOR_SelectedIndexChanged);
			// 
			// txtWhereResult
			// 
			this.txtWhereResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtWhereResult.Location = new System.Drawing.Point(160, 160);
			this.txtWhereResult.Multiline = true;
			this.txtWhereResult.Name = "txtWhereResult";
			this.txtWhereResult.ReadOnly = true;
			this.txtWhereResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtWhereResult.Size = new System.Drawing.Size(552, 144);
			this.txtWhereResult.TabIndex = 4;
			this.txtWhereResult.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(32, 164);
			this.label3.Name = "label3";
			this.label3.TabIndex = 5;
			this.label3.Text = "生成結果(&R)";
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnOK.Location = new System.Drawing.Point(8, 316);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(136, 23);
			this.btnOK.TabIndex = 6;
			this.btnOK.Text = "クリップボードにコピー(&O)";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(656, 316);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 7;
			this.btnCancel.Text = "戻る(&X)";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// rdoTable
			// 
			this.rdoTable.Checked = true;
			this.rdoTable.Location = new System.Drawing.Point(40, 56);
			this.rdoTable.Name = "rdoTable";
			this.rdoTable.Size = new System.Drawing.Size(108, 28);
			this.rdoTable.TabIndex = 8;
			this.rdoTable.TabStop = true;
			this.rdoTable.Text = "テーブル結合(&T)";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(32, 132);
			this.label4.Name = "label4";
			this.label4.TabIndex = 2;
			this.label4.Text = "結合条件(&J)";
			// 
			// rdoEtc
			// 
			this.rdoEtc.Location = new System.Drawing.Point(40, 88);
			this.rdoEtc.Name = "rdoEtc";
			this.rdoEtc.Size = new System.Drawing.Size(108, 28);
			this.rdoEtc.TabIndex = 8;
			this.rdoEtc.Text = "その他条件(&E)";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.cmbCondition);
			this.groupBox1.Location = new System.Drawing.Point(24, 20);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(424, 104);
			this.groupBox1.TabIndex = 9;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "条件";
			// 
			// cmbCondition
			// 
			this.cmbCondition.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
			this.cmbCondition.Items.AddRange(new object[] {
															  "is null",
															  "is null or != \'\'",
															  "is not null"});
			this.cmbCondition.Location = new System.Drawing.Point(140, 68);
			this.cmbCondition.Name = "cmbCondition";
			this.cmbCondition.Size = new System.Drawing.Size(121, 20);
			this.cmbCondition.TabIndex = 0;
			// 
			// MakeFieldWhereDlg
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(744, 345);
			this.Controls.Add(this.rdoTable);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtWhereResult);
			this.Controls.Add(this.cmbANDOR);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtAliasT1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtAliasT2);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.rdoEtc);
			this.Controls.Add(this.groupBox1);
			this.Name = "MakeFieldWhereDlg";
			this.Text = "where 条件生成";
			this.Controls.SetChildIndex(this.groupBox1, 0);
			this.Controls.SetChildIndex(this.rdoEtc, 0);
			this.Controls.SetChildIndex(this.label4, 0);
			this.Controls.SetChildIndex(this.txtAliasT2, 0);
			this.Controls.SetChildIndex(this.label2, 0);
			this.Controls.SetChildIndex(this.txtAliasT1, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.cmbANDOR, 0);
			this.Controls.SetChildIndex(this.txtWhereResult, 0);
			this.Controls.SetChildIndex(this.label3, 0);
			this.Controls.SetChildIndex(this.btnOK, 0);
			this.Controls.SetChildIndex(this.btnCancel, 0);
			this.Controls.SetChildIndex(this.rdoTable, 0);
			this.Controls.SetChildIndex(this.MsgArea, 0);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// 設定結果を反映させる
		/// </summary>
		private void reCreateSql()
		{
			StringBuilder sb = new StringBuilder();

			if( this.rdoTable.Checked == true )
			{
				string a1 = this.txtAliasT1.Text;
				string a2 = this.txtAliasT2.Text;
				if( a1.Length != 0 && !a1.EndsWith("."))
				{
					a1 += ".";
				}
				if( a2.Length != 0 && !a2.EndsWith("."))
				{
					a2 += ".";
				}
				// テーブル結合条件を生成する
				for(int i = 0; i < this.pFieldList.Count; i++ )
				{
					// 最初に左側
					string fname = this.pFieldList[i];
					sb.Append(a1 + fname);
				}

			}
			else
			{
			}
			this.txtWhereResult.Text = "";
		}

		private void txtAliasT1_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void textBox1_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void cmbANDOR_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
		
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
		
		}
	}
}

