using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace quickDBExplorer
{
	/// <summary>
	/// 指定されたテーブルのINDEX情報の表示ダイアログ
	/// </summary>
	public class IndexViewDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.CheckBox chkTopStay;
		private System.Windows.Forms.DataGrid dataGrid1;
		private System.Windows.Forms.Button btnCancel;
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// 表示するテーブル名称
		/// </summary>
		protected DBObjectInfo dspObj;
		/// <summary>
		/// 表示するテーブル名称
		/// </summary>
		public DBObjectInfo DspObj
		{
			get { return this.dspObj; }
			set { this.dspObj = value; }
		}

		/// <summary>
		/// SQLドライバ
		/// </summary>
		protected ISqlInterface sqlDriver = null;

		/// <summary>
		/// SQL文を処理するクラス
		/// </summary>
		public ISqlInterface SqlDriver
		{
			get { return this.sqlDriver; }
			set { this.sqlDriver = value; }
		}


		/// <summary>
		/// コンストラクタ
		/// </summary>
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
			this.chkTopStay = new System.Windows.Forms.CheckBox();
			this.dataGrid1 = new System.Windows.Forms.DataGrid();
			this.btnCancel = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.SuspendLayout();
			// 
			// chkTopStay
			// 
			this.chkTopStay.Location = new System.Drawing.Point(16, 8);
			this.chkTopStay.Name = "chkTopStay";
			this.chkTopStay.Size = new System.Drawing.Size(144, 16);
			this.chkTopStay.TabIndex = 0;
			this.chkTopStay.Text = "常に前面に表示(&T)";
			this.chkTopStay.CheckedChanged += new System.EventHandler(this.chkTopStay_CheckedChanged);
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
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(448, 240);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(80, 24);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "閉じる(&X)";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// IndexViewDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(544, 269);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.dataGrid1);
			this.Controls.Add(this.chkTopStay);
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

		/// <summary>
		/// INDEX情報を表示するテーブルを切り替える
		/// </summary>
		/// <param name="dboInfo">新規に表示するオブジェクト情報</param>
		public void settabledsp(DBObjectInfo dboInfo)
		{
			if( dboInfo == null || 
				( dboInfo.RealObjType != "U" &&
				dboInfo.RealObjType != "V" ) )
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

			this.dspObj = dboInfo;

			string sqlstr;

			sqlstr = string.Format(@"sp_helpindex '{0}'", dboInfo.RealObjName );

			DbDataAdapter da = this.sqlDriver.NewDataAdapter();
			IDbCommand cmd = this.sqlDriver.NewSqlCommand(sqlstr);
			this.sqlDriver.SetSelectCmd(da,cmd);

			DataSet baseidx = new DataSet();
			da.Fill(baseidx,"basedata");



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
			//マップ名を指定するこれは適当
			ts.MappingName = "abc";

			foreach( DataColumn col in idx.Tables[0].Columns )
			{
				cs = new DataGridTextBoxColumn();
				// 以前の表示幅を記憶している場合、その同じ幅で表示する
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

		/// <summary>
		/// 常にTOPに表示 チェックボックス変更時ハンドラ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void chkTopStay_CheckedChanged(object sender, System.EventArgs e)
		{
			if( this.chkTopStay.Checked == true )
			{
				this.TopMost = true;
			}
			else
			{
				this.TopMost = false;
			}
		}

		/// <summary>
		/// 画面初期表示時処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void IndexViewDialog_Load(object sender, System.EventArgs e)
		{
			if( this.dspObj != null )
			{
				settabledsp(dspObj);
			}
		}

		/// <summary>
		/// 戻るボタン押下時処理
		/// ここでは、ダイアログを実際に閉じるのではなく
		/// 非表示にするのみとする
		/// これは、以前の表示幅を記憶して、その幅で表示するようにする為
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Visible = false;
		}

		/// <summary>
		/// 「X」ボタンや、ALT+F4 等によるダイアログの閉じる処理への対応
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void IndexViewDialog_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			// ここでも非表示にするのみとする
			this.Visible = false;
			e.Cancel = true;
		}
	}
}
