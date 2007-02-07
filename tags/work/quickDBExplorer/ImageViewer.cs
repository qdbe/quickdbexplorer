using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace quickDBExplorer
{
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

		protected Image viewImage;

		public Image ViewImage
		{
			get { return this.viewImage; }
			set { this.viewImage = value; }
		}


		public ImageViewer()
		{
			// この呼び出しは Windows フォーム デザイナで必要です。
			InitializeComponent();

			// TODO: InitializeComponent 呼び出しの後に初期化処理を追加します。
		}

		/// <summary>
		/// 使用されているリソースに後処理を実行します。
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

		#region デザイナで生成されたコード
		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
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
			this.msgArea.Location = new System.Drawing.Point(166, 88);
			this.msgArea.Name = "msgArea";
			this.msgArea.Visible = false;
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
			this.label1.Text = "横";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.Location = new System.Drawing.Point(188, 22);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(30, 20);
			this.label2.TabIndex = 12;
			this.label2.Text = "縦";
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
			this.btnClose.Text = "閉じる(&X)";
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
			this.Text = "画像表示";
			this.Load += new System.EventHandler(this.ImageViewer_Load);
			this.Controls.SetChildIndex(this.label4, 0);
			this.Controls.SetChildIndex(this.PixcelWidth, 0);
			this.Controls.SetChildIndex(this.label2, 0);
			this.Controls.SetChildIndex(this.pictureBox1, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.PixcelHeight, 0);
			this.Controls.SetChildIndex(this.label3, 0);
			this.Controls.SetChildIndex(this.msgArea, 0);
			this.Controls.SetChildIndex(this.btnClose, 0);
			this.ResumeLayout(false);

		}
		#endregion

		private void ImageViewer_Load(object sender, System.EventArgs e)
		{
			int basewidth = this.pictureBox1.Width;
			int baseheight = this.pictureBox1.Height;
			this.pictureBox1.Image = this.viewImage;
			int diffwidth = this.viewImage.Width - basewidth;
			int diffheight = this.viewImage.Height - baseheight;
			this.Width += diffwidth;
			this.Height += diffheight;
			this.PixcelHeight.Text = this.viewImage.Height.ToString();
			this.PixcelWidth.Text = this.viewImage.Width.ToString();
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}

