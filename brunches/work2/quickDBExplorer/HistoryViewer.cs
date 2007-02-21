using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using quickDBExplorer;

namespace quickDBExplorer
{
	public class HistoryViewer : quickDBExplorer.quickDBExplorerBaseForm
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnClear;
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.ListView historyList;
		textHistory	textHistoryDS = new textHistory();
		string targetTable = "";
		private System.Windows.Forms.ColumnHeader KeyValue;
		private System.Windows.Forms.ColumnHeader DataValue;

		public string retString = "";

		public HistoryViewer(textHistory hdata, string curTable)
		{
			// ���̌Ăяo���� Windows �t�H�[�� �f�U�C�i�ŕK�v�ł��B
			InitializeComponent();
			this.textHistoryDS = hdata;
			this.targetTable = curTable;
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
			this.button1 = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnClear = new System.Windows.Forms.Button();
			this.historyList = new System.Windows.Forms.ListView();
			this.KeyValue = new System.Windows.Forms.ColumnHeader();
			this.DataValue = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// msgArea
			// 
			this.msgArea.Location = new System.Drawing.Point(198, 270);
			this.msgArea.Name = "msgArea";
			this.msgArea.TabIndex = 3;
			// 
			// button1
			// 
			this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button1.Location = new System.Drawing.Point(462, 270);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(72, 24);
			this.button1.TabIndex = 4;
			this.button1.Text = "�߂�(&X)";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(18, 272);
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "����(&O)";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnClear
			// 
			this.btnClear.Location = new System.Drawing.Point(104, 272);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(88, 23);
			this.btnClear.TabIndex = 2;
			this.btnClear.Text = "��������(&L)";
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// historyList
			// 
			this.historyList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						  this.KeyValue,
																						  this.DataValue});
			this.historyList.Location = new System.Drawing.Point(16, 8);
			this.historyList.Name = "historyList";
			this.historyList.Size = new System.Drawing.Size(520, 254);
			this.historyList.TabIndex = 0;
			// 
			// KeyValue
			// 
			this.KeyValue.Text = "�e�[�u����";
			// 
			// DataValue
			// 
			this.DataValue.Text = "�����ڍ�";
			// 
			// HistoryViewer
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(552, 304);
			this.Controls.Add(this.historyList);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.button1);
			this.Name = "HistoryViewer";
			this.Load += new System.EventHandler(this.HistoryViewer_Load);
			this.Controls.SetChildIndex(this.msgArea, 0);
			this.Controls.SetChildIndex(this.button1, 0);
			this.Controls.SetChildIndex(this.btnOK, 0);
			this.Controls.SetChildIndex(this.btnClear, 0);
			this.Controls.SetChildIndex(this.historyList, 0);
			this.ResumeLayout(false);

		}
		#endregion

		private void HistoryViewer_Load(object sender, System.EventArgs e)
		{
			// ��U�����͑S�ăN���A
			this.historyList.Clear();
			if( this.textHistoryDS != null && this.textHistoryDS.textHistoryData.Rows.Count != 0 )
			{
				// �܂��́A�����e�[�u�����̂��̂�D��
				this.textHistoryDS.textHistoryData.DefaultView.RowFilter = string.Format("KeyValue = '{0}'",this.targetTable);
				this.textHistoryDS.textHistoryData.DefaultView.Sort = "KeyNo desc";
				this.historyList.BeginUpdate();

				foreach( DataRow dr in this.textHistoryDS.textHistoryData.Rows )
				{
					ListViewItem item = new ListViewItem();
					item.SubItems.Add((string)dr["KeyValue"]);
					item.SubItems.Add((string)dr["DataValue"]);
					this.historyList.Items.Add(item);
				}

				// ���ɈႤ�e�[�u���̂��̂�\��
				this.textHistoryDS.textHistoryData.DefaultView.RowFilter = string.Format("KeyValue <> '{0}'",this.targetTable);
				this.textHistoryDS.textHistoryData.DefaultView.Sort = "KeyValue, KeyNo desc";
				foreach( DataRow dr in this.textHistoryDS.textHistoryData.Rows )
				{
					ListViewItem item = new ListViewItem();
					item.SubItems.Add((string)dr["KeyValue"]);
					item.SubItems.Add((string)dr["DataValue"]);
					this.historyList.Items.Add(item);
				}

				this.historyList.EndUpdate();

				this.historyList.Items[0].Selected = true;
			}
		}

		private void btnClear_Click(object sender, System.EventArgs e)
		{
			this.textHistoryDS.textHistoryData.Rows.Clear();
			this.historyList.Clear();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			if( this.historyList.SelectedItems.Count > 0 )
			{
				this.retString = this.historyList.SelectedItems[0].SubItems[1].Text;
			}
		}
	}
}

