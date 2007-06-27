using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace quickDBExplorer
{
	/// <summary>
	/// IndexViewDialog の概要の説明です。
	/// </summary>
	public class IndexViewDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.DataGrid dataGrid1;
		private System.Windows.Forms.Button button1;
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public string dsptbname;
		public System.Data.SqlClient.SqlConnection sqlConnection;

		public IndexViewDialog()
		{
			//
			// Windows フォーム デザイナ サポートに必要です。
			//
			InitializeComponent();

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(IndexViewDialog));
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.dataGrid1 = new System.Windows.Forms.DataGrid();
			this.button1 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.SuspendLayout();
			// 
			// checkBox1
			// 
			this.checkBox1.Location = new System.Drawing.Point(16, 8);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(144, 16);
			this.checkBox1.TabIndex = 0;
			this.checkBox1.Text = "常にTOPに表示(&T)";
			this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// dataGrid1
			// 
			this.dataGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.dataGrid1.DataMember = "";
			this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid1.Location = new System.Drawing.Point(8, 32);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.ReadOnly = true;
			this.dataGrid1.Size = new System.Drawing.Size(528, 196);
			this.dataGrid1.TabIndex = 1;
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button1.Location = new System.Drawing.Point(448, 240);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(80, 24);
			this.button1.TabIndex = 2;
			this.button1.Text = "閉じる(&X)";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// IndexViewDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.CancelButton = this.button1;
			this.ClientSize = new System.Drawing.Size(544, 269);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.dataGrid1);
			this.Controls.Add(this.checkBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimizeBox = false;
			this.Name = "IndexViewDialog";
			this.ShowInTaskbar = false;
			this.Text = "INDEX情報";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.IndexViewDialog_Closing);
			this.Load += new System.EventHandler(this.IndexViewDialog_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		public void settabledsp(string tbname)
		{
			if( tbname == null || tbname == "" )
			{
				this.dataGrid1.Hide();
				return;
			}
			// 現在の幅の情報を記録しておく

			Hashtable colwidth = new Hashtable();
			colwidth.Clear();
			if( this.dataGrid1.TableStyles.Count > 0 &&
				this.dataGrid1.TableStyles[0].GridColumnStyles.Count > 0 )
			{
				foreach(DataGridTextBoxColumn ccs in this.dataGrid1.TableStyles[0].GridColumnStyles )
				{
					colwidth[ccs.MappingName] = ccs.Width;
				}
			}

			this.dsptbname = tbname;

			// split owner.table -> owner, table
			string delimStr = ".";
			string []str = tbname.Split(delimStr.ToCharArray(), 2);
			string sqlstr;

			sqlstr = string.Format( @"select base_object_name from sys.synonyms 
	inner join sys.schemas on sys.synonyms.schema_id= sys.schemas.schema_id 
	where
	sys.schemas.name = '{0}' and 
	sys.synonyms.name = '{1}' ",
				str[0],
				str[1]
				);
			SqlDataAdapter dasyn = new SqlDataAdapter(sqlstr, this.sqlConnection);
			DataSet dssyn = new DataSet();
			dssyn.CaseSensitive = true;
			dasyn.Fill(dssyn,tbname);
			if( dssyn.Tables[tbname].Rows.Count > 0 )
			{
				// Synonym 
				sqlstr = string.Format(@"sp_helpindex '{0}'", dssyn.Tables[tbname].Rows[0]["base_object_name"] );
			}
			else
			{
				sqlstr = string.Format(@"sp_helpindex '{0}'", tbname );
			}

			SqlDataAdapter daa = new SqlDataAdapter(sqlstr, this.sqlConnection);
			DataSet baseidx = new DataSet();
			daa.Fill(baseidx,"basedata");



			// 取得した結果の index_keys フィールドを分割し、複数行にする
			// 名称, 属性、順序、フィールドに分割

			DataSet idx = new DataSet();
			idx.Tables.Add("abc");
			idx.Tables["abc"].Columns.Add("名称");
			idx.Tables["abc"].Columns.Add("属性");
			idx.Tables["abc"].Columns.Add("順序",typeof(int));
			idx.Tables["abc"].Columns.Add("フィールド");

			if( baseidx.Tables["basedata"] != null &&
				baseidx.Tables["basedata"].Rows.Count != 0 )
			{

				string index_keys;
				foreach( DataRow dr in baseidx.Tables["basedata"].Rows )
				{
					index_keys = (string)dr["index_keys"];
					string [] fname = index_keys.Split(',');
					for(int i = 0; i < fname.Length; i++ )
					{
						DataRow newdr = idx.Tables["abc"].NewRow();
						newdr["名称"] = (string)dr["index_name"];
						newdr["属性"] = (string)dr["index_description"];
						newdr["順序"] = i + 1;
						newdr["フィールド"] = fname[i].Trim();
						idx.Tables["abc"].Rows.Add(newdr);
					}
				}
			}




			DataGridTextBoxColumn  cs;
			//新しいDataGridTableStyleの作成
			DataGridTableStyle ts = new DataGridTableStyle();
			//マップ名を指定する
			ts.MappingName = "abc";

			foreach( DataColumn col in idx.Tables[0].Columns )
			{
				cs = new DataGridTextBoxColumn();
				if( colwidth[col.ColumnName] != null )
				{
					cs.Width = (int)colwidth[col.ColumnName];
				}
				else
				{
					cs.Width = 150;
				}

				//マップ名を指定する
				cs.MappingName = col.ColumnName;
				cs.HeaderText = col.ColumnName;
					
				//DataGridTableStyleに追加する
				ts.GridColumnStyles.Add(cs);
			}

			//テーブルスタイルをDataGridに追加する
			this.dataGrid1.TableStyles.Clear();
			this.dataGrid1.TableStyles.Add(ts);

			this.dataGrid1.SetDataBinding(idx,"abc");
			this.dataGrid1.Show();
		}

		private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
		{
			if( this.checkBox1.Checked == true )
			{
				this.TopMost = true;
			}
			else
			{
				this.TopMost = false;
			}
		}

		private void IndexViewDialog_Load(object sender, System.EventArgs e)
		{
			if(dsptbname != "" )
			{
				settabledsp(dsptbname);
			}
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			this.Visible = false;
		}

		private void IndexViewDialog_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			this.Visible = false;
			e.Cancel = true;
		}
	}
}
