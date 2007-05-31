using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace quickDBExplorer
{
	/// <summary>
	/// ZoomDialog �̊T�v�̐����ł��B
	/// where, order ���̃e�L�X�g�{�b�N�X�̒l���A�S�̂�������悤�ɂ���_�C�A���O
	/// �����Œl�̕ҏW���\
	/// </summary>
	public class ZoomDialog : quickDBExplorerBaseForm
	{
		protected System.Windows.Forms.TextBox txtZoom;
		protected System.Windows.Forms.Button btnOk;
		protected System.Windows.Forms.Button btnClose;
		/// <summary>
		/// �K�v�ȃf�U�C�i�ϐ��ł��B
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// �\������e�L�X�g�̕ҏW�l
		/// </summary>
		protected string   editText = "";

		/// <summary>
		/// �^�C�g������
		/// </summary>
		protected string	labelName = "";

		/// <summary>
		/// �\���݂̂��ۂ�
		/// true �F �\���̂�
		/// false : �ҏW�\
		/// </summary>
		protected bool		isDispOnly = false;

		/// <summary>
		/// �\������e�L�X�g�̕ҏW�l
		/// </summary>
		public virtual string EditText
		{
			get { return this.editText; }
			set 
			{
				this.editText = value; 
				if( this.txtZoom != null &&
					this.txtZoom.Visible == true &&
					this.txtZoom.IsDisposed != true )
				{
					this.txtZoom.Text = value;
				}
			}
		}

		/// <summary>
		/// �^�C�g������
		/// </summary>
		public virtual string LableName
		{
			get { return this.labelName; }
			set 
			{ 
				this.labelName = value; 
				this.Text = value;
			}
		}

		/// <summary>
		/// �\���݂̂��ۂ�
		/// true �F �\���̂�
		/// false : �ҏW�\
		/// </summary>
		public virtual bool IsDispOnly
		{
			get { return this.isDispOnly; }
			set { this.isDispOnly = value; }
		}


		public ZoomDialog()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ZoomDialog));
			this.txtZoom = new System.Windows.Forms.TextBox();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// msgArea
			// 
			this.msgArea.Location = new System.Drawing.Point(110, 230);
			this.msgArea.Name = "msgArea";
			// 
			// txtZoom
			// 
			this.txtZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtZoom.Location = new System.Drawing.Point(14, 12);
			this.txtZoom.Multiline = true;
			this.txtZoom.Name = "txtZoom";
			this.txtZoom.Size = new System.Drawing.Size(520, 210);
			this.txtZoom.TabIndex = 0;
			this.txtZoom.Text = "";
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnOk.Location = new System.Drawing.Point(10, 232);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(70, 26);
			this.btnOk.TabIndex = 1;
			this.btnOk.Text = "����(&O)";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(462, 236);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(70, 24);
			this.btnClose.TabIndex = 2;
			this.btnClose.Text = "����(&X)";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// ZoomDialog
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(544, 266);
			this.Controls.Add(this.txtZoom);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnOk);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ZoomDialog";
			this.Text = "ZoomDialog";
			this.Load += new System.EventHandler(this.ZoomDialog_Load);
			this.Controls.SetChildIndex(this.btnOk, 0);
			this.Controls.SetChildIndex(this.btnClose, 0);
			this.Controls.SetChildIndex(this.txtZoom, 0);
			this.Controls.SetChildIndex(this.msgArea, 0);
			this.ResumeLayout(false);

		}
		#endregion

		protected virtual void ZoomDialog_Load(object sender, System.EventArgs e)
		{
			this.txtZoom.Text = this.editText;
			if( this.isDispOnly == true)
			{
				this.btnOk.Enabled = false;
			}
		}

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			this.editText = this.txtZoom.Text;
			this.DialogResult = DialogResult.OK;
			this.OnEnter(new EventArgs());
			this.Close();
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}
