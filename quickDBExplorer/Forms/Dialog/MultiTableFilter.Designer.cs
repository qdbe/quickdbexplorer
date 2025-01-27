namespace quickDBExplorer.Forms.Dialog
{
    partial class MultiTableFilter
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkCaseSensitive = new System.Windows.Forms.CheckBox();
            this.rdoStartWith = new System.Windows.Forms.RadioButton();
            this.rdoContain = new System.Windows.Forms.RadioButton();
            this.rdoExact = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(16, 161);
            this.txtInput.Size = new System.Drawing.Size(678, 281);
            this.txtInput.TabIndex = 1;
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(16, 472);
            this.btnGo.Size = new System.Drawing.Size(107, 24);
            this.btnGo.TabIndex = 2;
            this.btnGo.Text = "絞り込み実施(&O)";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(582, 472);
            this.btnCancel.TabIndex = 4;
            // 
            // chkReturn
            // 
            this.chkReturn.Location = new System.Drawing.Point(274, 448);
            this.chkReturn.Visible = false;
            // 
            // btnHistory
            // 
            this.btnHistory.Location = new System.Drawing.Point(144, 472);
            this.btnHistory.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkCaseSensitive);
            this.groupBox2.Controls.Add(this.rdoStartWith);
            this.groupBox2.Controls.Add(this.rdoContain);
            this.groupBox2.Controls.Add(this.rdoExact);
            this.groupBox2.Location = new System.Drawing.Point(16, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(518, 106);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "検索方法";
            // 
            // chkCaseSensitive
            // 
            this.chkCaseSensitive.Checked = true;
            this.chkCaseSensitive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCaseSensitive.Location = new System.Drawing.Point(14, 61);
            this.chkCaseSensitive.Name = "chkCaseSensitive";
            this.chkCaseSensitive.Size = new System.Drawing.Size(152, 24);
            this.chkCaseSensitive.TabIndex = 3;
            this.chkCaseSensitive.Text = "大文字小文字を区別する";
            // 
            // rdoStartWith
            // 
            this.rdoStartWith.Location = new System.Drawing.Point(137, 20);
            this.rdoStartWith.Name = "rdoStartWith";
            this.rdoStartWith.Size = new System.Drawing.Size(104, 24);
            this.rdoStartWith.TabIndex = 1;
            this.rdoStartWith.Text = "前方一致";
            // 
            // rdoContain
            // 
            this.rdoContain.Checked = true;
            this.rdoContain.Location = new System.Drawing.Point(14, 20);
            this.rdoContain.Name = "rdoContain";
            this.rdoContain.Size = new System.Drawing.Size(104, 24);
            this.rdoContain.TabIndex = 0;
            this.rdoContain.TabStop = true;
            this.rdoContain.Text = "曖昧検索";
            // 
            // rdoExact
            // 
            this.rdoExact.Location = new System.Drawing.Point(247, 20);
            this.rdoExact.Name = "rdoExact";
            this.rdoExact.Size = new System.Drawing.Size(104, 24);
            this.rdoExact.TabIndex = 2;
            this.rdoExact.Text = "完全一致";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 132);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "検索文字";
            // 
            // MultiTableFilter
            // 
            this.ClientSize = new System.Drawing.Size(714, 501);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Name = "MultiTableFilter";
            this.Text = "複数オブジェクトフィルター指定";
            this.Controls.SetChildIndex(this.txtInput, 0);
            this.Controls.SetChildIndex(this.btnGo, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.chkReturn, 0);
            this.Controls.SetChildIndex(this.btnHistory, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkCaseSensitive;
        private System.Windows.Forms.RadioButton rdoStartWith;
        private System.Windows.Forms.RadioButton rdoContain;
        private System.Windows.Forms.RadioButton rdoExact;
        private System.Windows.Forms.Label label1;
    }
}
