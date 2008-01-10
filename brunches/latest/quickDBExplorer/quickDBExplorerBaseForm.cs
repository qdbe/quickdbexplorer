using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace quickDBExplorer
{
	/// <summary>
	/// �G���[���\���G���A�����Abase �ƂȂ�_�C�A���O�N���X�B
	/// �p�����ė��p���邱�Ƃ�z��
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public class quickDBExplorerBaseForm : System.Windows.Forms.Form
	{
		/// <summary>
		/// ���b�Z�[�W�\���G���A
		/// </summary>
		private System.Windows.Forms.Label pMsgArea;

		/// <summary>
		/// ���b�Z�[�W�\���G���A
		/// </summary>
		protected System.Windows.Forms.Label MsgArea
		{
			get { return this.pMsgArea; }
		}
		/// <summary>
		/// �K�v�ȃf�U�C�i�ϐ��ł��B
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.ErrorProvider errorProvider1;

		/// <summary>
		/// �\������G���[���b�Z�[�W
		/// </summary>
		private string  pErrMessage = "";

		/// <summary>
		/// �\������G���[���b�Z�[�W
		/// </summary>
		protected string  ErrMessage
		{
			get { return this.pErrMessage; }
			set { this.pErrMessage = value; }

		}

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public quickDBExplorerBaseForm()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(quickDBExplorerBaseForm));
			this.pMsgArea = new System.Windows.Forms.Label();
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider();
			this.SuspendLayout();
			// 
			// msgArea
			// 
			this.pMsgArea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.pMsgArea.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(128)));
			this.pMsgArea.ForeColor = System.Drawing.Color.Red;
			this.pMsgArea.Location = new System.Drawing.Point(48, 224);
			this.pMsgArea.Name = "msgArea";
			this.pMsgArea.Size = new System.Drawing.Size(232, 24);
			this.pMsgArea.TabIndex = 10;
			this.pMsgArea.DoubleClick += new System.EventHandler(this.msgArea_DoubleClick);
			// 
			// errorProvider1
			// 
			this.errorProvider1.ContainerControl = this;
			// 
			// quickDBExplorerBaseForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(288, 266);
			this.Controls.Add(this.pMsgArea);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "quickDBExplorerBaseForm";
			this.Text = "quickDBExplorerBaseForm";
			this.Load += new System.EventHandler(this.quickDBExplorerBaseForm_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void quickDBExplorerBaseForm_Load(object sender, System.EventArgs e)
		{
			this.pMsgArea.Text = "";
		}

		/// <summary>
		/// �G���[���b�Z�[�W�����������A�N���A����
		/// </summary>
		protected void InitErrMessage()
		{
			this.pMsgArea.Text = "";
			this.ErrMessage = "";
			this.errorProvider1.SetIconAlignment(this.pMsgArea,ErrorIconAlignment.MiddleLeft);
			this.errorProvider1.SetError(this.pMsgArea,"");
		}

		/// <summary>
		/// Exception �̓��e����G���[���b�Z�[�W��\������
		/// </summary>
		/// <param name="ex">��������Exception
		/// ����Exception �� Message ���G���[���b�Z�[�W�̈�ɕ\������AToString()�������ʂ� �_�u���N���b�N���̃N���b�v�{�[�h�\��t���ΏۂƂȂ�</param>
		protected void SetErrorMessage(Exception ex)
		{
			this.pMsgArea.Text = ex.Message;
			this.ErrMessage = ex.ToString();
			this.errorProvider1.SetError(this.pMsgArea,this.pMsgArea.Text);
		}

		/// <summary>
		/// �G���[���b�Z�[�W�̗̈�ɕ\��������e���Z�b�g����
		/// </summary>
		/// <param name="dspdata"></param>
		protected void SetMessageArea(object dspdata)
		{
			this.pMsgArea.Text = dspdata.ToString();
			Application.DoEvents();
		}

		/// <summary>
		/// �G���[���b�Z�[�W�̈���_�u���N���b�N�����ꍇ�A�G���[���b�Z�[�W�̏ڍׂȓ��e���N���b�v�{�[�h�ɃR�s�[����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void msgArea_DoubleClick(object sender, System.EventArgs e)
		{
			if( this.ErrMessage != "" )
			{
				Clipboard.SetDataObject(this.ErrMessage,true );
			}
		}
	}
}
