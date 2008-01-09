using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Text;

namespace quickDBExplorer
{
	/// <summary>
	/// DataTable ��\�����邽�߂̃_�C�A���O
	/// �e������ꗗ�\������ꍇ�ɁA���̃_�C�A���O���x�[�X�ɔh������B
	/// �������́A���ڗ��p����
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public class DataGridViewBase : quickDBExplorer.quickDBExplorerBaseForm
	{
		/// <summary>
		///  �N���b�v�{�[�h�R�s�[�{�^��
		/// </summary>
		private System.Windows.Forms.Button btnClipboard;
		/// <summary>
		/// �f�[�^�\���G���A
		/// </summary>
		private System.Windows.Forms.DataGrid dataViewGrid;
		private System.ComponentModel.IContainer components = null;
		/// <summary>
		/// ����{�^��
		/// </summary>
		private System.Windows.Forms.Button btnClose;
		/// <summary>
		/// �\������f�[�^
		/// </summary>
		//protected DataTable viewData = new DataTable("viewData");
		private DataTable viewData = null;
		/// <summary>
		/// �^�C�g���\��
		/// </summary>
		//private string  titleName	= "";

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		/// <param name="dt">�\������f�[�^</param>
		/// <param name="tableName">�\���^�C�g��</param>
		public DataGridViewBase(DataTable dt, string tableName)
		{
			// ���̌Ăяo���� Windows �t�H�[�� �f�U�C�i�ŕK�v�ł��B
			InitializeComponent();

			this.viewData = dt;
			//this.titleName = tableName;
			this.Text = tableName;
		}

		/// <summary>
		/// �g�p����Ă��郊�\�[�X�Ɍ㏈�������s���܂��B
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

		#region �f�U�C�i�Ő������ꂽ�R�[�h
		/// <summary>
		/// �f�U�C�i �T�|�[�g�ɕK�v�ȃ��\�b�h�ł��B���̃��\�b�h�̓��e��
		/// �R�[�h �G�f�B�^�ŕύX���Ȃ��ł��������B
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(DataGridViewBase));
			this.dataViewGrid = new System.Windows.Forms.DataGrid();
			this.btnClipboard = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataViewGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// msgArea
			// 
			this.msgArea.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.msgArea.Location = new System.Drawing.Point(152, 220);
			this.msgArea.Name = "msgArea";
			this.msgArea.Size = new System.Drawing.Size(254, 24);
			// 
			// dataViewGrid
			// 
			this.dataViewGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.dataViewGrid.DataMember = "";
			this.dataViewGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataViewGrid.Location = new System.Drawing.Point(12, 14);
			this.dataViewGrid.Name = "dataViewGrid";
			this.dataViewGrid.ReadOnly = true;
			this.dataViewGrid.Size = new System.Drawing.Size(490, 194);
			this.dataViewGrid.TabIndex = 0;
			// 
			// btnClipboard
			// 
			this.btnClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnClipboard.Location = new System.Drawing.Point(12, 218);
			this.btnClipboard.Name = "btnClipboard";
			this.btnClipboard.Size = new System.Drawing.Size(136, 26);
			this.btnClipboard.TabIndex = 1;
			this.btnClipboard.Text = "�N���b�v�{�[�h�ɃR�s�[(&B)";
			this.btnClipboard.Click += new System.EventHandler(this.btnClipboard_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.Location = new System.Drawing.Point(420, 220);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(76, 26);
			this.btnClose.TabIndex = 11;
			this.btnClose.Text = "����(&X)";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// DataGridViewBase
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(516, 252);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnClipboard);
			this.Controls.Add(this.dataViewGrid);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "DataGridViewBase";
			this.ShowInTaskbar = false;
			this.Load += new System.EventHandler(this.DataGridViewBase_Load);
			this.Controls.SetChildIndex(this.dataViewGrid, 0);
			this.Controls.SetChildIndex(this.btnClipboard, 0);
			this.Controls.SetChildIndex(this.btnClose, 0);
			this.Controls.SetChildIndex(this.msgArea, 0);
			((System.ComponentModel.ISupportInitialize)(this.dataViewGrid)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// �f�[�^�̓��e��TAB��؂ŃN���b�v�{�[�h�ɃR�s�[����
		/// </summary>
		/// <param name="sender">--</param>
		/// <param name="e">--</param>
		private void btnClipboard_Click(object sender, System.EventArgs e)
		{
			if( this.viewData == null )
			{
				return;
			}
			if( this.viewData.Rows.Count == 0 )
			{
				MessageBox.Show(this,"�\������Ă���f�[�^�͂���܂���");
				return;
			}

			StringBuilder stb = new StringBuilder();
			// �J���������܂��͏o��
			for( int j = 0; j < this.viewData.Columns.Count; j++ )
			{
				if( j != 0 )
				{
					stb.Append("\t");
				}
				stb.Append(this.viewData.Columns[j].ColumnName);
			}
			stb.Append(Environment.NewLine);

			// �f�[�^�̓��e���o��
			foreach( DataRow dr in this.viewData.Rows )
			{
				for( int i = 0; i < this.viewData.Columns.Count; i++ )
				{
					if( i != 0 )
					{
						stb.Append("\t");
					}
					if( dr[i] != DBNull.Value )
					{
						stb.Append(dr[i].ToString());
					}
					else
					{
						stb.Append("");
					}
				}
				stb.Append(Environment.NewLine);
			}
			Clipboard.SetDataObject(stb.ToString(),true );
		}

		/// <summary>
		/// ��ʏ����\��������
		/// </summary>
		/// <param name="sender">--</param>
		/// <param name="e">--</param>
		private void DataGridViewBase_Load(object sender, System.EventArgs e)
		{
			if( this.viewData == null )
			{
				this.Close();
			}
			this.dataViewGrid.AllowSorting = true;
			this.dataViewGrid.ColumnHeadersVisible = true;
			this.dataViewGrid.DataSource = this.viewData;
		}

		/// <summary>
		/// ����{�^������������
		/// </summary>
		/// <param name="sender">--</param>
		/// <param name="e">--</param>
		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();		
		}
	}
}

