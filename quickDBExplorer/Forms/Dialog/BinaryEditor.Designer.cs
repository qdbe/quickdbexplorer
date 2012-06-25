namespace quickDBExplorer.Forms.Dialog
{
    partial class BinaryEditor
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.txtData = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.grpImport = new System.Windows.Forms.GroupBox();
            this.btnDefaultImage = new System.Windows.Forms.Button();
            this.btnLoadFile = new System.Windows.Forms.Button();
            this.btnLoadClipboard = new System.Windows.Forms.Button();
            this.grpExport = new System.Windows.Forms.GroupBox();
            this.btnSaveClipboard = new System.Windows.Forms.Button();
            this.btnSaveFile = new System.Windows.Forms.Button();
            this.grpImport.SuspendLayout();
            this.grpExport.SuspendLayout();
            this.SuspendLayout();
            // 
            // MsgArea
            // 
            this.MsgArea.Location = new System.Drawing.Point(12, 436);
            this.MsgArea.Size = new System.Drawing.Size(698, 23);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(644, 460);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 24);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "キャンセル(&X)";
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOk.Location = new System.Drawing.Point(12, 461);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "決定(&O)";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // txtData
            // 
            this.txtData.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtData.Location = new System.Drawing.Point(29, 26);
            this.txtData.Multiline = true;
            this.txtData.Name = "txtData";
            this.txtData.ReadOnly = true;
            this.txtData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtData.Size = new System.Drawing.Size(452, 407);
            this.txtData.TabIndex = 6;
            this.txtData.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(128, 461);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // grpImport
            // 
            this.grpImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.grpImport.Controls.Add(this.btnDefaultImage);
            this.grpImport.Controls.Add(this.btnLoadFile);
            this.grpImport.Controls.Add(this.btnLoadClipboard);
            this.grpImport.Location = new System.Drawing.Point(532, 26);
            this.grpImport.Name = "grpImport";
            this.grpImport.Size = new System.Drawing.Size(200, 107);
            this.grpImport.TabIndex = 19;
            this.grpImport.TabStop = false;
            this.grpImport.Text = "データのインポート";
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
            // btnLoadFile
            // 
            this.btnLoadFile.Location = new System.Drawing.Point(6, 18);
            this.btnLoadFile.Name = "btnLoadFile";
            this.btnLoadFile.Size = new System.Drawing.Size(174, 23);
            this.btnLoadFile.TabIndex = 17;
            this.btnLoadFile.Text = "ファイルから";
            this.btnLoadFile.UseVisualStyleBackColor = true;
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
            // grpExport
            // 
            this.grpExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.grpExport.Controls.Add(this.btnSaveClipboard);
            this.grpExport.Controls.Add(this.btnSaveFile);
            this.grpExport.Location = new System.Drawing.Point(532, 149);
            this.grpExport.Name = "grpExport";
            this.grpExport.Size = new System.Drawing.Size(200, 83);
            this.grpExport.TabIndex = 20;
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
            // 
            // btnSaveFile
            // 
            this.btnSaveFile.Location = new System.Drawing.Point(6, 18);
            this.btnSaveFile.Name = "btnSaveFile";
            this.btnSaveFile.Size = new System.Drawing.Size(174, 23);
            this.btnSaveFile.TabIndex = 20;
            this.btnSaveFile.Text = "ファイルへ";
            this.btnSaveFile.UseVisualStyleBackColor = true;
            // 
            // BinaryEditor
            // 
            this.ClientSize = new System.Drawing.Size(744, 493);
            this.Controls.Add(this.grpExport);
            this.Controls.Add(this.grpImport);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtData);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Name = "BinaryEditor";
            this.Text = "バイナリデータ";
            this.Load += new System.EventHandler(this.BinaryEditor_Load);
            this.Controls.SetChildIndex(this.MsgArea, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.txtData, 0);
            this.Controls.SetChildIndex(this.button1, 0);
            this.Controls.SetChildIndex(this.grpImport, 0);
            this.Controls.SetChildIndex(this.grpExport, 0);
            this.grpImport.ResumeLayout(false);
            this.grpExport.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox txtData;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox grpImport;
        private System.Windows.Forms.Button btnDefaultImage;
        private System.Windows.Forms.Button btnLoadFile;
        private System.Windows.Forms.GroupBox grpExport;
        private System.Windows.Forms.Button btnSaveClipboard;
        private System.Windows.Forms.Button btnSaveFile;
        private System.Windows.Forms.Button btnLoadClipboard;
    }
}
