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
	public class quickDBExplorerBaseForm : System.Windows.Forms.Form
	{
		/// <summary>
		/// ���b�Z�[�W�\���G���A
		/// </summary>
		protected System.Windows.Forms.Label msgArea;
		/// <summary>
		/// �K�v�ȃf�U�C�i�ϐ��ł��B
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.ErrorProvider errorProvider1;
		/// <summary>
		/// �\������G���[���b�Z�[�W
		/// </summary>
		protected string  errMessage = "";

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
			this.msgArea = new System.Windows.Forms.Label();
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider();
			this.SuspendLayout();
			// 
			// msgArea
			// 
			this.msgArea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.msgArea.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(128)));
			this.msgArea.ForeColor = System.Drawing.Color.Red;
			this.msgArea.Location = new System.Drawing.Point(48, 224);
			this.msgArea.Name = "msgArea";
			this.msgArea.Size = new System.Drawing.Size(232, 24);
			this.msgArea.TabIndex = 10;
			this.msgArea.DoubleClick += new System.EventHandler(this.msgArea_DoubleClick);
			// 
			// errorProvider1
			// 
			this.errorProvider1.ContainerControl = this;
			// 
			// quickDBExplorerBaseForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(288, 266);
			this.Controls.Add(this.msgArea);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "quickDBExplorerBaseForm";
			this.Text = "quickDBExplorerBaseForm";
			this.Load += new System.EventHandler(this.quickDBExplorerBaseForm_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void quickDBExplorerBaseForm_Load(object sender, System.EventArgs e)
		{
			this.msgArea.Text = "";
		}

		/// <summary>
		/// �G���[���b�Z�[�W�����������A�N���A����
		/// </summary>
		protected void InitErrMessage()
		{
			this.msgArea.Text = "";
			this.errMessage = "";
			this.errorProvider1.SetIconAlignment(this.msgArea,ErrorIconAlignment.MiddleLeft);
			this.errorProvider1.SetError(this.msgArea,"");
		}

		/// <summary>
		/// Exception �̓��e����G���[���b�Z�[�W��\������
		/// </summary>
		/// <param name="ex">��������Exception
		/// ����Exception �� Message ���G���[���b�Z�[�W�̈�ɕ\������AToString()�������ʂ� �_�u���N���b�N���̃N���b�v�{�[�h�\��t���ΏۂƂȂ�</param>
		protected void SetErrorMessage(Exception ex)
		{
			this.msgArea.Text = ex.Message;
			this.errMessage = ex.ToString();
			this.errorProvider1.SetError(this.msgArea,this.msgArea.Text);
		}

		/// <summary>
		/// �G���[���b�Z�[�W�̗̈�ɕ\��������e���Z�b�g����
		/// </summary>
		/// <param name="dspdata"></param>
		protected void SetMessageArea(object dspdata)
		{
			this.msgArea.Text = dspdata.ToString();
			Application.DoEvents();
		}

		/// <summary>
		/// �G���[���b�Z�[�W�̈���_�u���N���b�N�����ꍇ�A�G���[���b�Z�[�W�̏ڍׂȓ��e���N���b�v�{�[�h�ɃR�s�[����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void msgArea_DoubleClick(object sender, System.EventArgs e)
		{
			if( this.errMessage != "" )
			{
				Clipboard.SetDataObject(this.errMessage,true );
			}
		}
	}
}
