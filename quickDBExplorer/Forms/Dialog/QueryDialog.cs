using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace quickDBExplorer
{
	/// <summary>
	/// �N�G��������͂���ׂ̃_�C�A���O
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public class QueryDialog : System.Windows.Forms.Form
	{

		/// <summary>
		/// ���͂��ꂽ������
		/// </summary>
		private string	pSelectSql = "";
		/// <summary>
		/// ���͂��ꂽ������
		/// </summary>
		public string	SelectSql
		{
			get { return this.pSelectSql; }
			set { this.pSelectSql = value ; }
		}
		/// <summary>
		/// ���ʂɖ߂�l�����邩�ǂ���
		/// </summary>
		private bool		pHasReturn = false;
		/// <summary>
		/// ���ʂɖ߂�l�����邩�ǂ���
		/// </summary>
		public bool		HasReturn
		{
			get { return this.pHasReturn; }
			set { this.pHasReturn = value; }
		}
		
		/// <summary>
		/// �e�L�X�g���̓G���A
		/// </summary>
		protected quickDBExplorerTextBox txtInput;
		/// <summary>
		/// OK�{�^��
		/// </summary>
		protected System.Windows.Forms.Button btnGo;
		/// <summary>
		/// �L�����Z���i����j�{�^��
		/// </summary>
		protected System.Windows.Forms.Button btnCancel;
		/// <summary>
		/// �߂�l���� �`�F�b�N�{�b�N�X
		/// </summary>
		protected System.Windows.Forms.CheckBox chkReturn;

		/// <summary>
		/// ����\���{�^��
		/// </summary>
		protected System.Windows.Forms.Button btnHistory;

		/// <summary>
		/// ���͗��������f�[�^
		/// </summary>
        public Dictionary<string, TextHistoryDataSet> Histories { get; set; }

        private string pHistoryKey;
        /// <summary>
        /// �����ɗ��p����L�[�l
        /// </summary>
        public string HistoryKey
        {
            get { return pHistoryKey; }
            set
            {
                pHistoryKey = value;
                this.txtInput.Histories = this.Histories;
                this.txtInput.HistoryKey = value;
            }
        }

        /// <summary>
        /// ���͗���
        /// </summary>
        public TextHistoryDataSet History
        {
            get { return this.Histories[this.HistoryKey]; }
        }

		/// <summary>
		/// �K�v�ȃf�U�C�i�ϐ��ł��B
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public QueryDialog()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QueryDialog));
			this.btnGo = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.chkReturn = new System.Windows.Forms.CheckBox();
			this.btnHistory = new System.Windows.Forms.Button();
			this.txtInput = new quickDBExplorer.quickDBExplorerTextBox();
			this.SuspendLayout();
			// 
			// btnGo
			// 
			this.btnGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnGo.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnGo.Location = new System.Drawing.Point(16, 233);
			this.btnGo.Name = "btnGo";
			this.btnGo.Size = new System.Drawing.Size(96, 24);
			this.btnGo.TabIndex = 1;
			this.btnGo.Text = "SQL���s(&O)";
			this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(348, 233);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(88, 24);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "�L�����Z��(&X)";
			// 
			// chkReturn
			// 
			this.chkReturn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.chkReturn.Location = new System.Drawing.Point(40, 209);
			this.chkReturn.Name = "chkReturn";
			this.chkReturn.Size = new System.Drawing.Size(208, 16);
			this.chkReturn.TabIndex = 3;
			this.chkReturn.Text = "�߂�l����(&R)";
			// 
			// btnHistory
			// 
			this.btnHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnHistory.Location = new System.Drawing.Point(144, 233);
			this.btnHistory.Name = "btnHistory";
			this.btnHistory.Size = new System.Drawing.Size(88, 24);
			this.btnHistory.TabIndex = 2;
			this.btnHistory.Text = "�������p(&L)";
			this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);
			// 
			// txtInput
			// 
			this.txtInput.AcceptsReturn = true;
			this.txtInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtInput.ImeMode = System.Windows.Forms.ImeMode.Off;
			this.txtInput.CanCtrlDelete = true;
			this.txtInput.IsDigitOnly = false;
			this.txtInput.Location = new System.Drawing.Point(16, 12);
			this.txtInput.Multiline = true;
			this.txtInput.Name = "txtInput";
			this.txtInput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtInput.Size = new System.Drawing.Size(444, 191);
			this.txtInput.TabIndex = 0;
			this.txtInput.WordWrap = false;
			this.txtInput.ShowHistory += new quickDBExplorer.ShowHistoryEventHandler(this.txtInput_ShowHistory);
			// 
			// QueryDialog
			// 
			this.AcceptButton = this.btnGo;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(480, 262);
			this.Controls.Add(this.btnHistory);
			this.Controls.Add(this.chkReturn);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnGo);
			this.Controls.Add(this.txtInput);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "QueryDialog";
			this.ShowInTaskbar = false;
			this.Text = "SQL���w��";
			this.Load += new System.EventHandler(this.QueryDialog_Load);
			this.VisibleChanged += new System.EventHandler(this.QueryDialog_VisibleChanged);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion


		/// <summary>
		/// OK�{�^���������C�x���g�n���h��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		internal virtual void btnGo_Click(object sender, System.EventArgs e)
		{
			SelectSql = this.txtInput.Text;
			if( this.chkReturn.Checked == true )
			{
				this.pHasReturn = true;
			}
			else
			{
				this.pHasReturn = false;
			}
			//this.DialogResult = DialogResult.OK;
            this.txtInput.SaveHistory("");
		}

		private void QueryDialog_Load(object sender, System.EventArgs e)
		{
			this.txtInput.Text = SelectSql;
			this.txtInput.Focus();
		}

		/// <summary>
		/// �������p�{�^���̉���
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnHistory_Click(object sender, System.EventArgs e)
		{
			// ���͗����̑I���_�C�A���O��\������
			this.txtInput.DoShowHistory("");
		}

		private void QueryDialog_VisibleChanged(object sender, EventArgs e)
		{
			this.txtInput.Focus();
		}

		private void txtInput_ShowHistory(object sender, EventArgs e)
		{
			// ���͗����̑I���_�C�A���O��\������
			this.txtInput.DoShowHistory("");
		}

	}
}
