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
	/// IndexViewDialog �̊T�v�̐����ł��B
	/// </summary>
	public class IndexViewDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.DataGrid dataGrid1;
		private System.Windows.Forms.Button button1;
		/// <summary>
		/// �K�v�ȃf�U�C�i�ϐ��ł��B
		/// </summary>
		private System.ComponentModel.Container components = null;

		public string dsptbname;
		public System.Data.SqlClient.SqlConnection sqlConnection;

		public IndexViewDialog()
		{
			//
			// Windows �t�H�[�� �f�U�C�i �T�|�[�g�ɕK�v�ł��B
			//
			InitializeComponent();

			//
			// TODO: InitializeComponent �Ăяo���̌�ɁA�R���X�g���N�^ �R�[�h��ǉ����Ă��������B
			//
		}

		/// <summary>
		/// �g�p����Ă��郊�\�[�X�Ɍ㏈�������s���܂��B
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

		#region Windows �t�H�[�� �f�U�C�i�Ő������ꂽ�R�[�h 
		/// <summary>
		/// �f�U�C�i �T�|�[�g�ɕK�v�ȃ��\�b�h�ł��B���̃��\�b�h�̓��e��
		/// �R�[�h �G�f�B�^�ŕύX���Ȃ��ł��������B
		/// </summary>
		private void InitializeComponent()
		{
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
			this.checkBox1.Text = "���TOP�ɕ\��(&T)";
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
			this.button1.Text = "����(&X)";
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
			this.MinimizeBox = false;
			this.Name = "IndexViewDialog";
			this.ShowInTaskbar = false;
			this.Text = "INDEX���";
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
			// ���݂̕��̏����L�^���Ă���

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

			sqlstr = string.Format(@"select 
				sysobjects.id  
				from 
					sysobjects, sysusers 
				where 
					sysobjects.uid= sysusers.uid and 
					sysusers.name = '{0}' and sysobjects.name = '{1}' ",str[0],str[1]);
			SqlDataAdapter da = new SqlDataAdapter(sqlstr, this.sqlConnection);
			DataSet ds = new DataSet();
			da.Fill(ds,tbname);

			sqlstr = string.Format(@"select t1.name as ����,
			convert(varchar(210), 
				case when (t1.status & 16)<>0 then 'clustered' else 'nonclustered' end
				+ case when (t1.status & 1)<>0 then ', '+(select name from master.dbo.spt_values where type = 'I' and number = 1)  else '' end
				+ case when (t1.status & 2)<>0 then ', '+(select name from master.dbo.spt_values where type = 'I' and number = 2) else '' end
				+ case when (t1.status & 4)<>0 then ', '+(select name from master.dbo.spt_values where type = 'I' and number = 4) else '' end
				+ case when (t1.status & 64)<>0 then ', '+(select name from master.dbo.spt_values where type = 'I' and number = 64) else case when (t1.status & 32)<>0 then ', '+(select  name from master.dbo.spt_values where type = 'I' and number = 32) else '' end end
				+ case when (t1.status & 2048)<>0 then ', '+(select name from master.dbo.spt_values where type = 'I' and number = 2048) else '' end
				+ case when (t1.status & 4096)<>0 then ', '+(select name from master.dbo.spt_values where type = 'I' and number = 4096) else '' end
				+ case when (t1.status & 8388608)<>0 then ', '+(select name from master.dbo.spt_values where type = 'I' and number = 8388608) else '' end
				+ case when (t1.status & 16777216)<>0 then ', '+(select name from master.dbo.spt_values where type = 'I' and number = 16777216)  else '' end ) as ����,
	t2.keyno ����,
	t3.name as �t�B�[���h
 from 
	sysindexes t1, 
	sysindexkeys t2,
	syscolumns t3
where 
	t1.indid > 0 and t1.indid < 255  and 
	(t1.status & 64)=0 and
	t1.id={0}  and 
	t1.id = t2.id and
	t1.indid = t2.indid and
	t1.id = t3.id and
	t2.colid = t3.colid
order by t2.indid, t2.keyno",
				(int)ds.Tables[tbname].Rows[0]["id"] );
			SqlDataAdapter daa = new SqlDataAdapter(sqlstr, this.sqlConnection);
			DataSet idx = new DataSet();
			daa.Fill(idx,"abc");


			DataGridTextBoxColumn  cs;
			//�V����DataGridTableStyle�̍쐬
			DataGridTableStyle ts = new DataGridTableStyle();
			//�}�b�v�����w�肷��
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

				//�}�b�v�����w�肷��
				cs.MappingName = col.ColumnName;
				cs.HeaderText = col.ColumnName;
					
				//DataGridTableStyle�ɒǉ�����
				ts.GridColumnStyles.Add(cs);
			}

			//�e�[�u���X�^�C����DataGrid�ɒǉ�����
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
