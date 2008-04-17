using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace quickDBExplorer
{
	/// <summary>
	/// �e�[�u���w��_�C�A���O
	/// �I�u�W�F�N�g�ꗗ�������̃I�u�W�F�N�g��I������ ���͗p�_�C�A���O
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public class TableSelectDialog : quickDBExplorer.quickDBExplorerBaseForm
	{
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.TextBox txtTableSelect;
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// ���_�C�A���O�ł̓��͌���
		/// </summary>
		private string pResultStr = "";
		/// <summary>
		/// ���_�C�A���O�ł̓��͌���
		/// </summary>
		public string ResultStr
		{
			get { return this.pResultStr; }
			set { this.pResultStr = value; }
		}

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public TableSelectDialog()
		{
			// ���̌Ăяo���� Windows �t�H�[�� �f�U�C�i�ŕK�v�ł��B
			InitializeComponent();

			pResultStr = string.Empty;

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
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.txtTableSelect = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// MsgArea
			// 
			this.MsgArea.Location = new System.Drawing.Point(112, 232);
			this.MsgArea.Name = "MsgArea";
			this.MsgArea.Size = new System.Drawing.Size(284, 16);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(412, 228);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(88, 24);
			this.btnCancel.TabIndex = 12;
			this.btnCancel.Text = "�L�����Z��(&X)";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(12, 228);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(96, 24);
			this.btnOk.TabIndex = 11;
			this.btnOk.Text = "����(&O)";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// txtTableSelect
			// 
			this.txtTableSelect.Location = new System.Drawing.Point(20, 12);
			this.txtTableSelect.Multiline = true;
			this.txtTableSelect.Name = "txtTableSelect";
			this.txtTableSelect.Size = new System.Drawing.Size(480, 204);
			this.txtTableSelect.TabIndex = 13;
			this.txtTableSelect.Text = "";
			// 
			// TableSelectDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(520, 257);
			this.Controls.Add(this.txtTableSelect);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Name = "TableSelectDialog";
			this.Load += new System.EventHandler(this.TableSelectDialog_Load);
			this.Controls.SetChildIndex(this.MsgArea, 0);
			this.Controls.SetChildIndex(this.btnOk, 0);
			this.Controls.SetChildIndex(this.btnCancel, 0);
			this.Controls.SetChildIndex(this.txtTableSelect, 0);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			if( this.txtTableSelect.Text != "" )
			{
				this.ResultStr = this.txtTableSelect.Text;
			}
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void TableSelectDialog_Load(object sender, System.EventArgs e)
		{
			this.txtTableSelect.Text = this.ResultStr;
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}

