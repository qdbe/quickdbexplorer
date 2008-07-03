using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using quickDBExplorer;

namespace quickDBExplorer
{
	/// <summary>
	/// �e����͗����̑I���_�C�A���O
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public class HistoryViewer : quickDBExplorer.quickDBExplorerBaseForm
	{
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnClear;
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.ListView historyList;
		/// <summary>
		/// ���͗������
		/// </summary>
		private TextHistoryDataSet	TextHistoryDataSetDS = new TextHistoryDataSet();
		/// <summary>
		/// �ΏۃI�u�W�F�N�g����
		/// </summary>
		private string targetTable = "";
		/// <summary>
		/// �����g��\���{�^��
		/// </summary>
		private System.Windows.Forms.Button btnDispAll;

		/// <summary>
		/// �I�����ꂽ���͗������
		/// </summary>
		private string pRetString = "";
		/// <summary>
		/// �I�����ꂽ���͗������
		/// </summary>
		public string RetString 
		{
			get { return this.pRetString; }
			set { this.pRetString = value; }
		}

		/// <summary>
		/// �I�u�W�F�N�g���̂�\�����邩�ۂ��̎w��
		/// </summary>
		private bool pIsShowTable = true;

		/// <summary>
		/// �I�u�W�F�N�g���̂�\�����邩�ۂ��̎w��
		/// </summary>
		public bool IsShowTable
		{
			get { return this.pIsShowTable; }
			set { this.pIsShowTable = value; }
		}

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		/// <param name="hdata">�������</param>
		/// <param name="curTable">���ݑI������Ă���I�u�W�F�N�g����</param>
		public HistoryViewer(TextHistoryDataSet hdata, string curTable)
		{
			// ���̌Ăяo���� Windows �t�H�[�� �f�U�C�i�ŕK�v�ł��B
			InitializeComponent();
			this.TextHistoryDataSetDS = hdata;
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
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnClear = new System.Windows.Forms.Button();
			this.historyList = new System.Windows.Forms.ListView();
			this.btnDispAll = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// MsgArea
			// 
			this.MsgArea.Location = new System.Drawing.Point(332, 270);
			this.MsgArea.Name = "MsgArea";
			this.MsgArea.Size = new System.Drawing.Size(124, 24);
			this.MsgArea.TabIndex = 3;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(462, 270);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(72, 24);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "�߂�(&X)";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnOK.Location = new System.Drawing.Point(18, 272);
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "����(&O)";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnClear
			// 
			this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnClear.Location = new System.Drawing.Point(104, 272);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(88, 23);
			this.btnClear.TabIndex = 2;
			this.btnClear.Text = "��������(&L)";
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// historyList
			// 
			this.historyList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.historyList.FullRowSelect = true;
			this.historyList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.historyList.Location = new System.Drawing.Point(16, 8);
			this.historyList.MultiSelect = false;
			this.historyList.Name = "historyList";
			this.historyList.Size = new System.Drawing.Size(520, 254);
			this.historyList.TabIndex = 0;
			this.historyList.View = System.Windows.Forms.View.Details;
			this.historyList.DoubleClick += new System.EventHandler(this.historyList_DoubleClick);
			// 
			// btnDispAll
			// 
			this.btnDispAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnDispAll.Location = new System.Drawing.Point(210, 272);
			this.btnDispAll.Name = "btnDispAll";
			this.btnDispAll.Size = new System.Drawing.Size(112, 23);
			this.btnDispAll.TabIndex = 3;
			this.btnDispAll.Text = "�����g��\��(&Z)";
			this.btnDispAll.Click += new System.EventHandler(this.btnDispAll_Click);
			// 
			// HistoryViewer
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(552, 304);
			this.Controls.Add(this.historyList);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnDispAll);
			this.Name = "HistoryViewer";
			this.ShowInTaskbar = false;
			this.Text = "�ߋ����͗���I��";
			this.Load += new System.EventHandler(this.HistoryViewer_Load);
			this.Controls.SetChildIndex(this.btnDispAll, 0);
			this.Controls.SetChildIndex(this.MsgArea, 0);
			this.Controls.SetChildIndex(this.btnCancel, 0);
			this.Controls.SetChildIndex(this.btnOK, 0);
			this.Controls.SetChildIndex(this.btnClear, 0);
			this.Controls.SetChildIndex(this.historyList, 0);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// ��ʏ����\��������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HistoryViewer_Load(object sender, System.EventArgs e)
		{
			// ��U�����ꗗ�͑S�ăN���A
			this.historyList.Clear();
			// �\������J�����̃Z�b�g
			if( pIsShowTable == true )
			{
				this.historyList.Columns.Add("�I�u�W�F�N�g",120,HorizontalAlignment.Left);
			}
			this.historyList.Columns.Add("����",this.historyList.Width - 120 - 4,HorizontalAlignment.Left);

			// �ꗗ�̓��e���Z�b�g���Ă���
			if( this.TextHistoryDataSetDS != null && this.TextHistoryDataSetDS.TextHistoryDataSets.Rows.Count != 0 )
			{
				this.historyList.BeginUpdate();

				// �܂��́A�����I�u�W�F�N�g���̂��̂�D�悵�ĕ\������
				DataRow []drl = this.TextHistoryDataSetDS.TextHistoryDataSets.Select(string.Format(System.Globalization.CultureInfo.CurrentCulture,"KeyValue = '{0}'",this.targetTable),
					"KeyNo desc");
				ListViewItem item ;
				for( int i = 0 ; i < drl.Length; i++ )
				{
					if( pIsShowTable == true )
					{
						item = new ListViewItem(
							new string[] {
											 (string)drl[i]["KeyValue"],
											 (string)drl[i]["DataValue"]
										 }
							);
					}
					else
					{
						item = new ListViewItem(
							new string[] {
											 (string)drl[i]["DataValue"]
										 }
							);
					}
					this.historyList.Items.Add(item);
				}

				// ���ɈႤ�I�u�W�F�N�g�̂��̂�\��
				drl = this.TextHistoryDataSetDS.TextHistoryDataSets.Select(string.Format(System.Globalization.CultureInfo.CurrentCulture,"KeyValue <> '{0}'",this.targetTable),
					"KeyNo desc");
				for( int i = 0 ; i < drl.Length; i++ )
				{
					if( pIsShowTable == true )
					{
						item = new ListViewItem(
							new string[] {
											 (string)drl[i]["KeyValue"],
											 (string)drl[i]["DataValue"]
										 }
							);
					}
					else
					{
						item = new ListViewItem(
							new string[] {
											 (string)drl[i]["DataValue"]
										 }
							);
					}
					this.historyList.Items.Add(item);
				}

				this.historyList.EndUpdate();

				this.historyList.Items[0].Selected = true;
			}
		}

		/// <summary>
		/// ���������{�^������������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnClear_Click(object sender, System.EventArgs e)
		{
			this.TextHistoryDataSetDS.TextHistoryDataSets.Rows.Clear();
			this.historyList.Clear();
		}


		/// <summary>
		/// �߂�{�^������������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		/// <summary>
		/// OK�{�^������������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOK_Click(object sender, System.EventArgs e)
		{
			if( this.historyList.SelectedItems.Count > 0 )
			{
				// �I�����ڂ�����΁A�����߂�
				if( pIsShowTable == true )
				{
					this.pRetString = this.historyList.SelectedItems[0].SubItems[1].Text;
				}
				else
				{
					this.pRetString = this.historyList.SelectedItems[0].SubItems[0].Text;
				}
			}
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		/// <summary>
		/// �_�u���N���b�N�͑I���Ɠ��ꎋ����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void historyList_DoubleClick(object sender, System.EventArgs e)
		{
			this.btnOK_Click(sender, e);
		}

		/// <summary>
		/// �g��\���{�^������������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDispAll_Click(object sender, System.EventArgs e)
		{
			if( this.historyList.SelectedItems.Count == 0 )
			{
				return;
			}
			// �I�����ڂ̓��e���g��\���_�C�A���O�𗘗p���ĕ\��������B
			// �������A�ǂݎ���p
			ZoomDialog dlg = new ZoomDialog();
			dlg.IsDispOnly = true;
			if( pIsShowTable == true )
			{
				dlg.EditText = this.historyList.SelectedItems[0].SubItems[1].Text;
			}
			else
			{
				dlg.EditText = this.historyList.SelectedItems[0].SubItems[0].Text;
			}
			dlg.LableName = "�����g��\��";
			dlg.ShowDialog();
		}
	}
}

