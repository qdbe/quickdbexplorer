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
	/// �w�肳�ꂽ�e�[�u����INDEX���̕\���_�C�A���O
	/// </summary>
	public class IndexViewDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.CheckBox chkTopStay;
		private System.Windows.Forms.DataGrid dataGrid1;
		private System.Windows.Forms.Button btnCancel;
		/// <summary>
		/// �K�v�ȃf�U�C�i�ϐ��ł��B
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// �\������e�[�u������
		/// </summary>
		protected string dsptbname;
		/// <summary>
		/// �\������e�[�u������
		/// </summary>
		public string DspTbname
		{
			get { return this.dsptbname; }
			set { this.dsptbname = value; }
		}

		/// <summary>
		/// SQL Server �ւ̃R�l�N�V�������
		/// </summary>
		protected System.Data.SqlClient.SqlConnection sqlConnection;
		/// <summary>
		/// SQL Server �ւ̃R�l�N�V�������
		/// </summary>
		public System.Data.SqlClient.SqlConnection SqlConnection
		{
			get { return this.sqlConnection; }
			set { this.sqlConnection = value; }
		}

		/// <summary>
		/// SQL Server �̃o�[�W����
		/// </summary>
		protected int		sqlVersion = 2000;
		/// <summary>
		/// SQL Server �̃o�[�W����
		/// </summary>
		public int		SqlVersion
		{
			get	{ return this.sqlVersion; }
			set { this.sqlVersion = value; }
		}

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public IndexViewDialog()
		{
			//
			// Windows �t�H�[�� �f�U�C�i �T�|�[�g�ɕK�v�ł��B
			//
			InitializeComponent();

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
			this.chkTopStay.Text = "��ɑO�ʂɕ\��(&T)";
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
			this.btnCancel.Text = "����(&X)";
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
			this.Text = "INDEX���";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.IndexViewDialog_Closing);
			this.Load += new System.EventHandler(this.IndexViewDialog_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// INDEX����\������e�[�u����؂�ւ���
		/// </summary>
		/// <param name="tbname">�V�K�ɕ\������e�[�u������</param>
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

			if( this.sqlVersion != 2000 )
			{
				// SQL Server 2005 �ȍ~ �ł́Asynonym ���w�肳���\��������ׁA���̃`�F�b�N���s��
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
					// Synonym �̏ꍇ�A���̐e�ƂȂ�I�u�W�F�N�g�ɑ΂��� ���{����K�v����
					sqlstr = string.Format(@"sp_helpindex '{0}'", dssyn.Tables[tbname].Rows[0]["base_object_name"] );
				}
				else
				{
					sqlstr = string.Format(@"sp_helpindex '{0}'", tbname );
				}
			}
			else
			{
				// SQL Server 2000 �ł́Asynonym �̍l���͕K�v�Ȃ�
				sqlstr = string.Format(@"sp_helpindex '{0}'", tbname );
			}

			SqlDataAdapter daa = new SqlDataAdapter(sqlstr, this.sqlConnection);
			DataSet baseidx = new DataSet();
			daa.Fill(baseidx,"basedata");



			// �擾�������ʂ� index_keys �t�B�[���h�𕪊����A�����s�ɂ���
			// ����, �����A�����A�t�B�[���h�ɕ���

			DataSet idx = new DataSet();
			idx.Tables.Add("abc");
			idx.Tables["abc"].Columns.Add("����");
			idx.Tables["abc"].Columns.Add("����");
			idx.Tables["abc"].Columns.Add("����",typeof(int));
			idx.Tables["abc"].Columns.Add("�t�B�[���h");

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
						newdr["����"] = (string)dr["index_name"];
						newdr["����"] = (string)dr["index_description"];
						newdr["����"] = i + 1;
						newdr["�t�B�[���h"] = fname[i].Trim();
						idx.Tables["abc"].Rows.Add(newdr);
					}
				}
			}




			DataGridTextBoxColumn  cs;
			//�V����DataGridTableStyle�̍쐬
			DataGridTableStyle ts = new DataGridTableStyle();
			//�}�b�v�����w�肷�邱��͓K��
			ts.MappingName = "abc";

			foreach( DataColumn col in idx.Tables[0].Columns )
			{
				cs = new DataGridTextBoxColumn();
				// �ȑO�̕\�������L�����Ă���ꍇ�A���̓������ŕ\������
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

		/// <summary>
		/// ���TOP�ɕ\�� �`�F�b�N�{�b�N�X�ύX���n���h��
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
		/// ��ʏ����\��������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void IndexViewDialog_Load(object sender, System.EventArgs e)
		{
			if(dsptbname != "" )
			{
				settabledsp(dsptbname);
			}
		}

		/// <summary>
		/// �߂�{�^������������
		/// �����ł́A�_�C�A���O�����ۂɕ���̂ł͂Ȃ�
		/// ��\���ɂ���݂̂Ƃ���
		/// ����́A�ȑO�̕\�������L�����āA���̕��ŕ\������悤�ɂ����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Visible = false;
		}

		/// <summary>
		/// �uX�v�{�^����AALT+F4 ���ɂ��_�C�A���O�̕��鏈���ւ̑Ή�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void IndexViewDialog_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			// �����ł���\���ɂ���݂̂Ƃ���
			this.Visible = false;
			e.Cancel = true;
		}
	}
}
