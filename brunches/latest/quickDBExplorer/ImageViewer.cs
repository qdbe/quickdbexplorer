using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace quickDBExplorer
{
	/// <summary>
	/// �C���[�W�̕\���_�C�A���O
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public class ImageViewer : quickDBExplorer.quickDBExplorerBaseForm
	{
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox PixcelHeight;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox PixcelWidth;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnClose;

		/// <summary>
		/// �\������C���[�W���
		/// </summary>
		private Image pViewImage;

		/// <summary>
		/// �\������C���[�W���̎擾�E�ݒ�
		/// </summary>
		public Image ViewImage
		{
			get { return this.pViewImage; }
			set { this.pViewImage = value; }
		}


		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public ImageViewer()
		{
			// ���̌Ăяo���� Windows �t�H�[�� �f�U�C�i�ŕK�v�ł��B
			InitializeComponent();

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
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.PixcelHeight = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.PixcelWidth = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.btnClose = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// msgArea
			// 
			this.MsgArea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.MsgArea.Location = new System.Drawing.Point(188, 84);
			this.MsgArea.Name = "msgArea";
			this.MsgArea.Size = new System.Drawing.Size(216, 80);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBox1.Location = new System.Drawing.Point(14, 16);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(166, 212);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 11;
			this.pictureBox1.TabStop = false;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Location = new System.Drawing.Point(188, 56);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(30, 20);
			this.label1.TabIndex = 12;
			this.label1.Text = "��";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.Location = new System.Drawing.Point(188, 22);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(30, 20);
			this.label2.TabIndex = 12;
			this.label2.Text = "�c";
			// 
			// PixcelHeight
			// 
			this.PixcelHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.PixcelHeight.Location = new System.Drawing.Point(224, 20);
			this.PixcelHeight.Name = "PixcelHeight";
			this.PixcelHeight.ReadOnly = true;
			this.PixcelHeight.TabIndex = 13;
			this.PixcelHeight.Text = "";
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.Location = new System.Drawing.Point(330, 22);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(76, 18);
			this.label3.TabIndex = 14;
			this.label3.Text = "Pixel";
			// 
			// PixcelWidth
			// 
			this.PixcelWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.PixcelWidth.Location = new System.Drawing.Point(224, 54);
			this.PixcelWidth.Name = "PixcelWidth";
			this.PixcelWidth.ReadOnly = true;
			this.PixcelWidth.TabIndex = 13;
			this.PixcelWidth.Text = "";
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label4.Location = new System.Drawing.Point(330, 56);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(76, 18);
			this.label4.TabIndex = 14;
			this.label4.Text = "Pixel";
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(308, 204);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(88, 24);
			this.btnClose.TabIndex = 16;
			this.btnClose.Text = "����(&X)";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// ImageViewer
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.AutoScroll = true;
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(408, 240);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.PixcelHeight);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.PixcelWidth);
			this.Controls.Add(this.label4);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "ImageViewer";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "�摜�\��";
			this.Load += new System.EventHandler(this.ImageViewer_Load);
			this.Controls.SetChildIndex(this.label4, 0);
			this.Controls.SetChildIndex(this.PixcelWidth, 0);
			this.Controls.SetChildIndex(this.label2, 0);
			this.Controls.SetChildIndex(this.pictureBox1, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.PixcelHeight, 0);
			this.Controls.SetChildIndex(this.label3, 0);
			this.Controls.SetChildIndex(this.MsgArea, 0);
			this.Controls.SetChildIndex(this.btnClose, 0);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// ��ʏ����\��������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ImageViewer_Load(object sender, System.EventArgs e)
		{
			// ���Ƃ��Ƃ̕��A��������U�L������
			int basewidth = this.pictureBox1.Width;
			int baseheight = this.pictureBox1.Height;
			// �\���C���[�W�̐ݒ�
			this.pictureBox1.Image = this.pViewImage;
			// ���Ƃ��Ƃ̃C���[�W�\���G���A�̃T�C�Y�Ƃ̍������擾����
			int diffwidth = this.pViewImage.Width - basewidth;
			int diffheight = this.pViewImage.Height - baseheight;
			// ���̃_�C�A���O���̂̃T�C�Y�𓮓I�ɕύX����
			this.Width += diffwidth;
			this.Height += diffheight;
			// �C���[�W�̕��A�������̕\��
			this.PixcelHeight.Text = this.pViewImage.Height.ToString(System.Globalization.CultureInfo.CurrentCulture);
			this.PixcelWidth.Text = this.pViewImage.Width.ToString(System.Globalization.CultureInfo.CurrentCulture);
		}

		/// <summary>
		/// ����{�^������������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}

