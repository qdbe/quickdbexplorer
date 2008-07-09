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
		private quickDBExplorerTextBox txtAliasT1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cmbANDOR;
		private quickDBExplorerTextBox txtWhereResult;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox cmbCondition;
		private System.Windows.Forms.RadioButton rdoTable;
		private System.Windows.Forms.RadioButton rdoEtc;
		private System.ComponentModel.IContainer components = null;
		private quickDBExplorerTextBox txtAliasT2;
		private System.Windows.Forms.Button btnRef1;
		private System.Windows.Forms.Button btnRef2;

		/// <summary>
		/// 処理対象にするフィールドのリスト
		/// </summary>
		private  StringCollection pFieldList = null;
		private quickDBExplorerTextBox txtAlias;
		private System.Windows.Forms.Button btnRef3;
		private System.Windows.Forms.CheckBox chkPlace;
		private ComboBox cmbJoinCond;

		/// <summary>
		/// 処理対象にするフィールドのリスト
		/// </summary>
		public StringCollection FieldList
		{
			get { return this.pFieldList; }
			set { this.pFieldList = value; }
		}

		/// <summary>
		/// 処理対象オブジェクト
		/// </summary>
		private DBObjectInfo pObjectInfo = null;
		/// <summary>
		/// 処理対象オブジェクト
		/// </summary>
		public DBObjectInfo ObjectInfo
		{
			get { return this.pObjectInfo; }
			set { this.pObjectInfo = value; }
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
			this.txtAliasT1 = new quickDBExplorer.quickDBExplorerTextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtAliasT2 = new quickDBExplorer.quickDBExplorerTextBox();
			this.cmbANDOR = new System.Windows.Forms.ComboBox();
			this.txtWhereResult = new quickDBExplorer.quickDBExplorerTextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.rdoTable = new System.Windows.Forms.RadioButton();
			this.label4 = new System.Windows.Forms.Label();
			this.rdoEtc = new System.Windows.Forms.RadioButton();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnRef1 = new System.Windows.Forms.Button();
			this.cmbJoinCond = new System.Windows.Forms.ComboBox();
			this.cmbCondition = new System.Windows.Forms.ComboBox();
			this.btnRef2 = new System.Windows.Forms.Button();
			this.txtAlias = new quickDBExplorer.quickDBExplorerTextBox();
			this.btnRef3 = new System.Windows.Forms.Button();
			this.chkPlace = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// MsgArea
			// 
			this.MsgArea.Location = new System.Drawing.Point(148, 393);
			this.MsgArea.Size = new System.Drawing.Size(537, 16);
			// 
			// txtAliasT1
			// 
			this.txtAliasT1.IsCTRLDelete = true;
			this.txtAliasT1.IsDigitOnly = false;
			this.txtAliasT1.Location = new System.Drawing.Point(130, 40);
			this.txtAliasT1.Name = "txtAliasT1";
			this.txtAliasT1.Size = new System.Drawing.Size(172, 19);
			this.txtAliasT1.TabIndex = 2;
			this.txtAliasT1.Text = "t1";
			this.txtAliasT1.TextChanged += new System.EventHandler(this.txtAliasT1_TextChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(134, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(168, 20);
			this.label1.TabIndex = 2;
			this.label1.Text = "テーブル名(1)";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(315, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(172, 20);
			this.label2.TabIndex = 2;
			this.label2.Text = "テーブル名(2)";
			// 
			// txtAliasT2
			// 
			this.txtAliasT2.IsCTRLDelete = true;
			this.txtAliasT2.IsDigitOnly = false;
			this.txtAliasT2.Location = new System.Drawing.Point(315, 40);
			this.txtAliasT2.Name = "txtAliasT2";
			this.txtAliasT2.Size = new System.Drawing.Size(172, 19);
			this.txtAliasT2.TabIndex = 4;
			this.txtAliasT2.Text = "t2";
			this.txtAliasT2.TextChanged += new System.EventHandler(this.txtAliasT2_TextChanged);
			// 
			// cmbANDOR
			// 
			this.cmbANDOR.Items.AddRange(new object[] {
            "AND",
            "OR"});
			this.cmbANDOR.Location = new System.Drawing.Point(160, 180);
			this.cmbANDOR.Name = "cmbANDOR";
			this.cmbANDOR.Size = new System.Drawing.Size(184, 20);
			this.cmbANDOR.TabIndex = 2;
			this.cmbANDOR.SelectedIndexChanged += new System.EventHandler(this.cmbANDOR_SelectedIndexChanged);
			this.cmbANDOR.TextChanged += new System.EventHandler(this.cmbANDOR_TextChanged);
			// 
			// txtWhereResult
			// 
			this.txtWhereResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtWhereResult.IsCTRLDelete = false;
			this.txtWhereResult.IsDigitOnly = false;
			this.txtWhereResult.Location = new System.Drawing.Point(160, 208);
			this.txtWhereResult.Multiline = true;
			this.txtWhereResult.Name = "txtWhereResult";
			this.txtWhereResult.ReadOnly = true;
			this.txtWhereResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtWhereResult.Size = new System.Drawing.Size(593, 168);
			this.txtWhereResult.TabIndex = 5;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(32, 212);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 23);
			this.label3.TabIndex = 4;
			this.label3.Text = "生成結果(&R)";
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnOK.Location = new System.Drawing.Point(8, 389);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(136, 23);
			this.btnOK.TabIndex = 6;
			this.btnOK.Text = "クリップボードにコピー(&O)";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(697, 389);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 7;
			this.btnCancel.Text = "戻る(&X)";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// rdoTable
			// 
			this.rdoTable.Checked = true;
			this.rdoTable.Location = new System.Drawing.Point(10, 36);
			this.rdoTable.Name = "rdoTable";
			this.rdoTable.Size = new System.Drawing.Size(108, 28);
			this.rdoTable.TabIndex = 0;
			this.rdoTable.TabStop = true;
			this.rdoTable.Text = "テーブル結合(&T)";
			this.rdoTable.CheckedChanged += new System.EventHandler(this.rdoTable_CheckedChanged);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(32, 180);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100, 23);
			this.label4.TabIndex = 1;
			this.label4.Text = "結合条件(&J)";
			// 
			// rdoEtc
			// 
			this.rdoEtc.Location = new System.Drawing.Point(10, 92);
			this.rdoEtc.Name = "rdoEtc";
			this.rdoEtc.Size = new System.Drawing.Size(108, 28);
			this.rdoEtc.TabIndex = 1;
			this.rdoEtc.Text = "その他条件(&E)";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnRef1);
			this.groupBox1.Controls.Add(this.cmbJoinCond);
			this.groupBox1.Controls.Add(this.cmbCondition);
			this.groupBox1.Controls.Add(this.rdoEtc);
			this.groupBox1.Controls.Add(this.btnRef2);
			this.groupBox1.Controls.Add(this.txtAliasT2);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.txtAliasT1);
			this.groupBox1.Controls.Add(this.txtAlias);
			this.groupBox1.Controls.Add(this.btnRef3);
			this.groupBox1.Controls.Add(this.rdoTable);
			this.groupBox1.Location = new System.Drawing.Point(24, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(729, 166);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "条件";
			// 
			// btnRef1
			// 
			this.btnRef1.Location = new System.Drawing.Point(130, 64);
			this.btnRef1.Name = "btnRef1";
			this.btnRef1.Size = new System.Drawing.Size(172, 20);
			this.btnRef1.TabIndex = 3;
			this.btnRef1.Text = "選択テーブルを反映(&1)";
			this.btnRef1.Click += new System.EventHandler(this.btnRef1_Click);
			// 
			// cmbJoinCond
			// 
			this.cmbJoinCond.Items.AddRange(new object[] {
            "= のみ",
            "= & null チェック",
            "!= のみ",
            "!= & null チェック"});
			this.cmbJoinCond.Location = new System.Drawing.Point(493, 39);
			this.cmbJoinCond.Name = "cmbJoinCond";
			this.cmbJoinCond.Size = new System.Drawing.Size(230, 20);
			this.cmbJoinCond.TabIndex = 6;
			this.cmbJoinCond.SelectedIndexChanged += new System.EventHandler(this.cmbJoinCond_SelectedIndexChanged);
			this.cmbJoinCond.TextChanged += new System.EventHandler(this.cmbCondition_TextChanged);
			// 
			// cmbCondition
			// 
			this.cmbCondition.Items.AddRange(new object[] {
            "is null",
            "is null or != \'\'",
            "is not null"});
			this.cmbCondition.Location = new System.Drawing.Point(315, 96);
			this.cmbCondition.Name = "cmbCondition";
			this.cmbCondition.Size = new System.Drawing.Size(224, 20);
			this.cmbCondition.TabIndex = 9;
			this.cmbCondition.SelectedIndexChanged += new System.EventHandler(this.cmbCondition_SelectedIndexChanged);
			this.cmbCondition.TextChanged += new System.EventHandler(this.cmbCondition_TextChanged);
			// 
			// btnRef2
			// 
			this.btnRef2.Location = new System.Drawing.Point(315, 64);
			this.btnRef2.Name = "btnRef2";
			this.btnRef2.Size = new System.Drawing.Size(172, 20);
			this.btnRef2.TabIndex = 5;
			this.btnRef2.Text = "選択テーブルを反映(&2)";
			this.btnRef2.Click += new System.EventHandler(this.btnRef2_Click);
			// 
			// txtAlias
			// 
			this.txtAlias.IsCTRLDelete = true;
			this.txtAlias.IsDigitOnly = false;
			this.txtAlias.Location = new System.Drawing.Point(130, 96);
			this.txtAlias.Name = "txtAlias";
			this.txtAlias.Size = new System.Drawing.Size(172, 19);
			this.txtAlias.TabIndex = 7;
			this.txtAlias.TextChanged += new System.EventHandler(this.txtAlias_TextChanged);
			// 
			// btnRef3
			// 
			this.btnRef3.Location = new System.Drawing.Point(130, 120);
			this.btnRef3.Name = "btnRef3";
			this.btnRef3.Size = new System.Drawing.Size(172, 20);
			this.btnRef3.TabIndex = 8;
			this.btnRef3.Text = "選択テーブルを反映(&3)";
			this.btnRef3.Click += new System.EventHandler(this.btnRef3_Click);
			// 
			// chkPlace
			// 
			this.chkPlace.Checked = true;
			this.chkPlace.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkPlace.Location = new System.Drawing.Point(360, 180);
			this.chkPlace.Name = "chkPlace";
			this.chkPlace.Size = new System.Drawing.Size(104, 24);
			this.chkPlace.TabIndex = 3;
			this.chkPlace.Text = "前につける(&P)";
			this.chkPlace.CheckedChanged += new System.EventHandler(this.chkPlace_CheckedChanged);
			// 
			// MakeFieldWhereDlg
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(785, 418);
			this.Controls.Add(this.chkPlace);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtWhereResult);
			this.Controls.Add(this.cmbANDOR);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.groupBox1);
			this.Name = "MakeFieldWhereDlg";
			this.Text = "where 条件生成";
			this.Load += new System.EventHandler(this.MakeFieldWhereDlg_Load);
			this.Controls.SetChildIndex(this.groupBox1, 0);
			this.Controls.SetChildIndex(this.label4, 0);
			this.Controls.SetChildIndex(this.cmbANDOR, 0);
			this.Controls.SetChildIndex(this.txtWhereResult, 0);
			this.Controls.SetChildIndex(this.label3, 0);
			this.Controls.SetChildIndex(this.btnOK, 0);
			this.Controls.SetChildIndex(this.btnCancel, 0);
			this.Controls.SetChildIndex(this.chkPlace, 0);
			this.Controls.SetChildIndex(this.MsgArea, 0);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

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
					if( this.chkPlace.Checked == true )
					{
						// AND, OR は前につける
						if( i != 0 )
						{
							sb.Append(this.cmbANDOR.Text).Append(" ");
						}
						// 最初は何もしない
					}
					// 最初に左側
					string fname = this.pFieldList[i];

					string joinStr = "";
					if (this.cmbJoinCond.Text == "= のみ")
					{
						joinStr = "( {0} = {1} )";
					}
					else if (this.cmbJoinCond.Text == "= & null チェック")
					{
						joinStr = @"( ( {0} = {1} ) or ( {0} is null and {1} is null ))";
					}
					else if (this.cmbJoinCond.Text == "!= のみ")
					{
						joinStr = "( {0} != {1} )";
					}
					else if (this.cmbJoinCond.Text == "!= & null チェック")
					{
						joinStr = "( ( {0} != {1} ) or ( {0} is null and {1} is not null ) or ( {0} is not null and {1} is null ))";
					}
					else
					{
						joinStr = "( " + this.cmbJoinCond.Text + " )";
					}
					try
					{
						sb.AppendFormat(joinStr, a1 + fname, a2 + fname);
					}
					catch
					{
						return;
					}

					if( this.chkPlace.Checked == true )
					{
						// 前につける場合改行のみでよい
						sb.Append("\r\n");
					}
					else
					{
						// 後につけるパターンは最後の行はいらない
						if( (i+1) != this.pFieldList.Count )
						{
							sb.Append(" ").Append(this.cmbANDOR.Text).Append("\r\n");
						}
						else
						{
							sb.Append("\r\n");
						}
					}
				}
			}
			else
			{
				string  strAlias = this.txtAlias.Text;
				if( strAlias.Length != 0 && !strAlias.EndsWith(".") )
				{
					strAlias += ".";
				}
				// 全フィールドに同一句を設定する
				for(int i = 0; i < this.pFieldList.Count; i++ )
				{
					string fname = this.pFieldList[i];
					if( this.chkPlace.Checked == true )
					{
						// AND, OR は前につける
						if( i != 0 )
						{
							sb.Append(this.cmbANDOR.Text).Append(" ");
						}
						// 最初は何もしない
					}

					if( this.cmbCondition.Text == "is null")
					{
						sb.Append("(").Append(strAlias).Append(fname).Append(" is null ").Append(")");
					}
					else if ( this.cmbCondition.Text == @"is null or != ''" )
					{
						sb.Append("(").Append(strAlias).Append(fname).Append(" is null or ").Append(strAlias).Append(fname).Append(" != '' ").Append(")");
					}
					else if( this.cmbCondition.Text == @"is not null" )
					{
						sb.Append("(").Append(strAlias).Append(fname).Append(" is not null ").Append(")");
					}
					else
					{
						try
						{
							sb.Append("( ").AppendFormat(this.cmbCondition.Text,strAlias+fname).Append(" )");
						}
						catch
						{
							// 書式不明の為、何もしない
							return;
						}
					}
					if( this.chkPlace.Checked == true )
					{
						// 前につける場合改行のみでよい
						sb.Append("\r\n");
					}
					else
					{
						// 後につけるパターンは最後の行はいらない
						if( (i+1) != this.pFieldList.Count )
						{
							sb.Append(" ").Append(this.cmbANDOR.Text).Append("\r\n");
						}
						else
						{
							sb.Append("\r\n");
						}
					}
				}
			}
			this.txtWhereResult.Text = sb.ToString();
		}

		private void SetRdoCondition()
		{
			ArrayList tar = new ArrayList();
			ArrayList ear = new ArrayList();
			tar.Add(this.label1);
			tar.Add(this.label2);
			tar.Add(this.txtAliasT1);
			tar.Add(this.txtAliasT2);
			tar.Add(this.btnRef1);
			tar.Add(this.btnRef2);
			tar.Add(this.cmbJoinCond);

			ear.Add(this.txtAlias);
			ear.Add(this.btnRef3);
			ear.Add(this.cmbCondition);

			bool cond = false;
			
			if( this.rdoTable.Checked == true )
			{
				cond = true;
			}
			else
			{
				cond = false;
			}
			for(int i = 0; i < tar.Count; i++ )
			{
				((Control)tar[i]).Enabled = cond;
			}
			for(int i = 0; i < ear.Count; i++ )
			{
				((Control)ear[i]).Enabled = !cond;
			}
		}

		private void txtAliasT1_TextChanged(object sender, System.EventArgs e)
		{
			reCreateSql();
		}

		private void txtAliasT2_TextChanged(object sender, System.EventArgs e)
		{
			reCreateSql();
		}

		private void cmbANDOR_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			reCreateSql();
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			Clipboard.SetDataObject(this.txtWhereResult.Text,true );
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btnRef1_Click(object sender, System.EventArgs e)
		{
			this.txtAliasT1.Text = this.pObjectInfo.FormalName;
		}

		private void btnRef2_Click(object sender, System.EventArgs e)
		{
			this.txtAliasT2.Text = this.pObjectInfo.FormalName;
		}

		private void btnRef3_Click(object sender, System.EventArgs e)
		{
			this.txtAlias.Text = this.pObjectInfo.FormalName;
		}


		private void rdoTable_CheckedChanged(object sender, System.EventArgs e)
		{
			SetRdoCondition();
			reCreateSql();
		}

		private void MakeFieldWhereDlg_Load(object sender, System.EventArgs e)
		{
			this.cmbCondition.SelectedIndex = 0;
			this.cmbANDOR.SelectedIndex = 0;
			this.cmbJoinCond.SelectedIndex = 0;
			SetRdoCondition();
			reCreateSql();
		}

		private void txtAlias_TextChanged(object sender, System.EventArgs e)
		{
			reCreateSql();
		}

		private void cmbCondition_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			reCreateSql();
		}

		private void cmbCondition_TextChanged(object sender, System.EventArgs e)
		{
			reCreateSql();
		}

		private void cmbANDOR_TextChanged(object sender, System.EventArgs e)
		{
			reCreateSql();
		}

		private void chkPlace_CheckedChanged(object sender, System.EventArgs e)
		{
			reCreateSql();
		}

		private void cmbJoinCond_SelectedIndexChanged(object sender, EventArgs e)
		{
			reCreateSql();
		}
	}
}

