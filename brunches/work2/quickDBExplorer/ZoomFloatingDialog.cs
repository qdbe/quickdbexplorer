using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace quickDBExplorer
{
	public class ZoomFloatingDialog : quickDBExplorer.ZoomDialog
	{
		private System.Windows.Forms.CheckBox chkStayOnTop;
		private System.Windows.Forms.Button btnApply;
		private System.ComponentModel.IContainer components = null;

		public ZoomFloatingDialog()
		{
			// ���̌Ăяo���� Windows �t�H�[�� �f�U�C�i�ŕK�v�ł��B
			InitializeComponent();

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ZoomFloatingDialog));
			this.chkStayOnTop = new System.Windows.Forms.CheckBox();
			this.btnApply = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtZoom
			// 
			this.txtZoom.Location = new System.Drawing.Point(14, 24);
			this.txtZoom.Name = "txtZoom";
			this.txtZoom.Size = new System.Drawing.Size(520, 198);
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(10, 234);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(80, 26);
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(462, 234);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(80, 26);
			// 
			// msgArea
			// 
			this.msgArea.Location = new System.Drawing.Point(220, 234);
			this.msgArea.Name = "msgArea";
			this.msgArea.Size = new System.Drawing.Size(80, 26);
			// 
			// chkStayOnTop
			// 
			this.chkStayOnTop.Location = new System.Drawing.Point(16, 4);
			this.chkStayOnTop.Name = "chkStayOnTop";
			this.chkStayOnTop.Size = new System.Drawing.Size(152, 18);
			this.chkStayOnTop.TabIndex = 11;
			this.chkStayOnTop.Text = "���TOP�ɕ\��(&T)";
			this.chkStayOnTop.CheckedChanged += new System.EventHandler(this.chkStayOnTop_CheckedChanged);
			// 
			// btnApply
			// 
			this.btnApply.Location = new System.Drawing.Point(364, 234);
			this.btnApply.Name = "btnApply";
			this.btnApply.Size = new System.Drawing.Size(80, 26);
			this.btnApply.TabIndex = 12;
			this.btnApply.Text = "�K�p(&A)";
			this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
			// 
			// ZoomFloatingDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(544, 266);
			this.Controls.Add(this.btnApply);
			this.Controls.Add(this.chkStayOnTop);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ZoomFloatingDialog";
			this.Controls.SetChildIndex(this.chkStayOnTop, 0);
			this.Controls.SetChildIndex(this.btnApply, 0);
			this.Controls.SetChildIndex(this.btnOk, 0);
			this.Controls.SetChildIndex(this.btnClose, 0);
			this.Controls.SetChildIndex(this.txtZoom, 0);
			this.Controls.SetChildIndex(this.msgArea, 0);
			this.ResumeLayout(false);

		}
		#endregion

		private void chkStayOnTop_CheckedChanged(object sender, System.EventArgs e)
		{
			if( this.chkStayOnTop.Checked == true )
			{
				this.TopMost = true;
			}
			else
			{
				this.TopMost = false;
			}
		}

		private void btnApply_Click(object sender, System.EventArgs e)
		{
			this.editText = this.txtZoom.Text;
			this.OnEnter(new EventArgs());
		}
	}
}

