using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace quickDBExplorer
{
	/// <summary>
	/// イメージの表示ダイアログ
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public class ImageViewer : quickDBExplorer.quickDBExplorerBaseForm
	{
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private quickDBExplorerTextBox PixcelHeight;
		private System.Windows.Forms.Label label3;
		private quickDBExplorerTextBox PixcelWidth;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnClose;
        private Button btnLoadFile;
        private Button btnLoadClipboard;
        private GroupBox grpImport;
        private Button btnDefaultImage;

		/// <summary>
		/// 表示するイメージ情報
		/// </summary>
		private Image pOriginalImage;
        private GroupBox grpExport;
        private Button btnSaveClipboard;
        private Button btnSaveFile;
        private Button btnOK;

        /// <summary>
        /// 表示するイメージ情報
        /// </summary>
        private Image currentImage;


		/// <summary>
		/// 表示するイメージ情報の取得・設定
		/// </summary>
		public Image ViewImage
		{
            get { return this.currentImage; }
			set { 
                this.pOriginalImage = value;
                this.currentImage = pOriginalImage;
            }
		}

        /// <summary>
        /// 参照モードか否か
        /// </summary>
        public bool IsReadOnly { get; private set; }


		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ImageViewer(bool isReadOnly)
		{
			// この呼び出しは Windows フォーム デザイナで必要です。
			InitializeComponent();
            this.IsReadOnly = isReadOnly;
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
            this.PixcelHeight = new quickDBExplorer.quickDBExplorerTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.PixcelWidth = new quickDBExplorer.quickDBExplorerTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnLoadFile = new System.Windows.Forms.Button();
            this.btnLoadClipboard = new System.Windows.Forms.Button();
            this.grpImport = new System.Windows.Forms.GroupBox();
            this.btnDefaultImage = new System.Windows.Forms.Button();
            this.grpExport = new System.Windows.Forms.GroupBox();
            this.btnSaveClipboard = new System.Windows.Forms.Button();
            this.btnSaveFile = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.grpImport.SuspendLayout();
            this.grpExport.SuspendLayout();
            this.SuspendLayout();
            // 
            // MsgArea
            // 
            this.MsgArea.Location = new System.Drawing.Point(12, 292);
            this.MsgArea.Size = new System.Drawing.Size(290, 16);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(14, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(166, 273);
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
            this.PixcelHeight.CanCtrlDelete = true;
            this.PixcelHeight.IsDigitOnly = false;
            this.PixcelHeight.IsShowZoom = false;
            this.PixcelHeight.Location = new System.Drawing.Point(224, 20);
            this.PixcelHeight.Name = "PixcelHeight";
            this.PixcelHeight.PdHistory = null;
            this.PixcelHeight.ReadOnly = true;
            this.PixcelHeight.Size = new System.Drawing.Size(100, 19);
            this.PixcelHeight.TabIndex = 13;
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
            this.PixcelWidth.CanCtrlDelete = true;
            this.PixcelWidth.IsDigitOnly = false;
            this.PixcelWidth.IsShowZoom = false;
            this.PixcelWidth.Location = new System.Drawing.Point(224, 54);
            this.PixcelWidth.Name = "PixcelWidth";
            this.PixcelWidth.PdHistory = null;
            this.PixcelWidth.ReadOnly = true;
            this.PixcelWidth.Size = new System.Drawing.Size(100, 19);
            this.PixcelWidth.TabIndex = 13;
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
            this.btnClose.Location = new System.Drawing.Point(308, 284);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(88, 24);
            this.btnClose.TabIndex = 16;
            this.btnClose.Text = "閉じる(&X)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnLoadFile
            // 
            this.btnLoadFile.Location = new System.Drawing.Point(6, 18);
            this.btnLoadFile.Name = "btnLoadFile";
            this.btnLoadFile.Size = new System.Drawing.Size(174, 23);
            this.btnLoadFile.TabIndex = 17;
            this.btnLoadFile.Text = "ファイルから";
            this.btnLoadFile.UseVisualStyleBackColor = true;
            this.btnLoadFile.Click += new System.EventHandler(this.btnLoadFile_Click);
            // 
            // btnLoadClipboard
            // 
            this.btnLoadClipboard.Location = new System.Drawing.Point(6, 47);
            this.btnLoadClipboard.Name = "btnLoadClipboard";
            this.btnLoadClipboard.Size = new System.Drawing.Size(174, 23);
            this.btnLoadClipboard.TabIndex = 18;
            this.btnLoadClipboard.Text = "クリップボードから";
            this.btnLoadClipboard.UseVisualStyleBackColor = true;
            this.btnLoadClipboard.Click += new System.EventHandler(this.btnLoadClipboard_Click);
            // 
            // grpImport
            // 
            this.grpImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.grpImport.Controls.Add(this.btnDefaultImage);
            this.grpImport.Controls.Add(this.btnLoadFile);
            this.grpImport.Controls.Add(this.btnLoadClipboard);
            this.grpImport.Location = new System.Drawing.Point(190, 79);
            this.grpImport.Name = "grpImport";
            this.grpImport.Size = new System.Drawing.Size(200, 107);
            this.grpImport.TabIndex = 15;
            this.grpImport.TabStop = false;
            this.grpImport.Text = "イメージのインポート";
            // 
            // btnDefaultImage
            // 
            this.btnDefaultImage.Location = new System.Drawing.Point(6, 78);
            this.btnDefaultImage.Name = "btnDefaultImage";
            this.btnDefaultImage.Size = new System.Drawing.Size(174, 23);
            this.btnDefaultImage.TabIndex = 19;
            this.btnDefaultImage.Text = "元に戻す";
            this.btnDefaultImage.UseVisualStyleBackColor = true;
            this.btnDefaultImage.Click += new System.EventHandler(this.btnDefaultImage_Click);
            // 
            // grpExport
            // 
            this.grpExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.grpExport.Controls.Add(this.btnSaveClipboard);
            this.grpExport.Controls.Add(this.btnSaveFile);
            this.grpExport.Location = new System.Drawing.Point(190, 193);
            this.grpExport.Name = "grpExport";
            this.grpExport.Size = new System.Drawing.Size(200, 83);
            this.grpExport.TabIndex = 17;
            this.grpExport.TabStop = false;
            this.grpExport.Text = "イメージのエクスポート";
            // 
            // btnSaveClipboard
            // 
            this.btnSaveClipboard.Location = new System.Drawing.Point(6, 49);
            this.btnSaveClipboard.Name = "btnSaveClipboard";
            this.btnSaveClipboard.Size = new System.Drawing.Size(174, 23);
            this.btnSaveClipboard.TabIndex = 21;
            this.btnSaveClipboard.Text = "クリップボードへ";
            this.btnSaveClipboard.UseVisualStyleBackColor = true;
            this.btnSaveClipboard.Click += new System.EventHandler(this.btnSaveClipboard_Click);
            // 
            // btnSaveFile
            // 
            this.btnSaveFile.Location = new System.Drawing.Point(6, 18);
            this.btnSaveFile.Name = "btnSaveFile";
            this.btnSaveFile.Size = new System.Drawing.Size(174, 23);
            this.btnSaveFile.TabIndex = 20;
            this.btnSaveFile.Text = "ファイルへ";
            this.btnSaveFile.UseVisualStyleBackColor = true;
            this.btnSaveFile.Click += new System.EventHandler(this.btnSaveFile_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(214, 284);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(88, 24);
            this.btnOK.TabIndex = 18;
            this.btnOK.Text = "決定(&O)";
            // 
            // ImageViewer
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
            this.AutoScroll = true;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(408, 314);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.grpExport);
            this.Controls.Add(this.grpImport);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.PixcelHeight);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PixcelWidth);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimumSize = new System.Drawing.Size(414, 339);
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
            this.Controls.SetChildIndex(this.MsgArea, 0);
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this.grpImport, 0);
            this.Controls.SetChildIndex(this.grpExport, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.grpImport.ResumeLayout(false);
            this.grpExport.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// 画面初期表示時処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ImageViewer_Load(object sender, System.EventArgs e)
		{
            ResetImageSize();
            if (this.IsReadOnly == true)
            {
                this.grpImport.Enabled = false;
            }
		}

        /// <summary>
        /// イメージからバイト配列を取得する
        /// </summary>
        /// <returns></returns>
        public byte[] GetBytes()
        {
            System.Drawing.ImageConverter conv = new ImageConverter();
            return (byte[])conv.ConvertTo(this.currentImage,typeof(byte[]));
            //MemoryStream ms = new MemoryStream();
            //this.currentImage.Save(ms,System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders(this.currentImage.en  this.currentImage.RawFormat);
            //return ms.GetBuffer();
        }

        private void ResetImageSize()
        {
            // もともとの幅、高さを一旦記憶する
            int basewidth = this.pictureBox1.Width;
            int baseheight = this.pictureBox1.Height;
            // 表示イメージの設定
            this.pictureBox1.Image = this.currentImage;
            // もともとのイメージ表示エリアのサイズとの差分を取得して
            int diffwidth = this.currentImage.Width - basewidth;
            int diffheight = this.currentImage.Height - baseheight;
            // このダイアログ自体のサイズを動的に変更する
            this.Width += diffwidth;
            this.Height += diffheight;
            // イメージの幅、高さ情報の表示
            this.PixcelHeight.Text = this.currentImage.Height.ToString(System.Globalization.CultureInfo.CurrentCulture);
            this.PixcelWidth.Text = this.currentImage.Width.ToString(System.Globalization.CultureInfo.CurrentCulture);
        }

		/// <summary>
		/// 閉じるボタン押下時処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

        private void btnDefaultImage_Click(object sender, EventArgs e)
        {
            this.currentImage = this.pOriginalImage;
            ResetImageSize();
        }

        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.OverwritePrompt = true;
            dlg.Filter = "Bmp|*.bmp|Gif|*.gif|Icon|*.ico|jpeg|*.jpeg|Png|*.png|全て|*.*";

            if (dlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            this.currentImage.Save(dlg.FileName, GetFileFormatFromFileName(dlg.FileName));
        }

        private System.Drawing.Imaging.ImageFormat GetFileFormatFromFileName(string fname)
        {
            System.Drawing.Imaging.ImageFormat format = System.Drawing.Imaging.ImageFormat.Png;
            switch (Path.GetExtension(fname).ToLower())
            {
                case ".bmp":
                    format = System.Drawing.Imaging.ImageFormat.Bmp;
                    break;
                case ".gif":
                    format = System.Drawing.Imaging.ImageFormat.Gif;
                    break;
                case ".ico":
                    format = System.Drawing.Imaging.ImageFormat.Icon;
                    break;
                case ".jpg":
                case ".jpeg":
                    format = System.Drawing.Imaging.ImageFormat.Jpeg;
                    break;
                case ".png":
                    format = System.Drawing.Imaging.ImageFormat.Png;
                    break;
                case ".tiff":
                    format = System.Drawing.Imaging.ImageFormat.Tiff;
                    break;
                case ".wmf":
                    format = System.Drawing.Imaging.ImageFormat.Wmf;
                    break;
                case ".emf":
                    format = System.Drawing.Imaging.ImageFormat.Emf;
                    break;
                default:
                    break;
            }
            return format;
        }

        private void btnSaveClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(this.currentImage, true);
        }

        private void btnLoadClipboard_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsImage())
            {
                Image tmp = Clipboard.GetImage();
                if (tmp == null) return;
                this.currentImage = tmp;
                ResetImageSize();
            }
        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Bmp|*.bmp|Gif|*.gif|Icon|*.ico|jpeg|*.jpeg|Png|*.png|全て|*.*";
            if (dlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            Image tmp = null;
            try
            {
                tmp = Image.FromFile(dlg.FileName);
            }
            catch
            {
                MessageBox.Show("正しいイメージ形式ではありません");
                return;
            }
            this.currentImage = tmp;
            ResetImageSize();
        }
	}
}

