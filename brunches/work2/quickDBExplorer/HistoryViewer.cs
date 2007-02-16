using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace quickDBExplorer
{
	public class HistoryViewer : quickDBExplorer.quickDBExplorerBaseForm
	{
		private System.Windows.Forms.ListBox historyList;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnClear;
		private System.ComponentModel.IContainer components = null;
		ArrayList	histAr = new ArrayList();

		public HistoryViewer(ArrayList histdata)
		{
			// ���̌Ăяo���� Windows �t�H�[�� �f�U�C�i�ŕK�v�ł��B
			InitializeComponent();
			this.histAr = histdata;

			// TODO: InitializeComponent �Ăяo���̌�ɏ�����������ǉ����܂��B
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
			this.historyList = new System.Windows.Forms.ListBox();
			this.button1 = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnClear = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// msgArea
			// 
			this.msgArea.Location = new System.Drawing.Point(198, 270);
			this.msgArea.Name = "msgArea";
			// 
			// historyList
			// 
			this.historyList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.historyList.ItemHeight = 12;
			this.historyList.Location = new System.Drawing.Point(16, 12);
			this.historyList.Name = "historyList";
			this.historyList.Size = new System.Drawing.Size(514, 244);
			this.historyList.TabIndex = 11;
			// 
			// button1
			// 
			this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button1.Location = new System.Drawing.Point(462, 270);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(72, 24);
			this.button1.TabIndex = 12;
			this.button1.Text = "�߂�(&X)";
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(18, 272);
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 13;
			this.btnOK.Text = "����(&O)";
			// 
			// btnClear
			// 
			this.btnClear.Location = new System.Drawing.Point(104, 272);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(88, 23);
			this.btnClear.TabIndex = 14;
			this.btnClear.Text = "��������(&L)";
			// 
			// HistoryViewer
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(552, 304);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.historyList);
			this.Name = "HistoryViewer";
			this.Load += new System.EventHandler(this.HistoryViewer_Load);
			this.Controls.SetChildIndex(this.historyList, 0);
			this.Controls.SetChildIndex(this.msgArea, 0);
			this.Controls.SetChildIndex(this.button1, 0);
			this.Controls.SetChildIndex(this.btnOK, 0);
			this.Controls.SetChildIndex(this.btnClear, 0);
			this.ResumeLayout(false);

		}
		#endregion

		private void HistoryViewer_Load(object sender, System.EventArgs e)
		{
			// ��U�����͑S�ăN���A
			this.historyList.Items.Clear();

			if( this.histAr != null && this.histAr.Count != 0 )
			{
				this.historyList.BeginUpdate();
				this.historyList.DisplayMember
				textHistory hi;
				for( int i = this.histAr.Count; i > 0; i-- )
				{
					hi = (textHistory)this.histAr[i-1];
					this.historyList.Items.Add(ListBox.
				}
				this.historyList.EndUpdate();
			}
		}
	}
}

