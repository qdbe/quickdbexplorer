﻿namespace quickDBExplorer.Forms.Dialog
{
    /// <summary>
    /// INSERT文設定ダイアログ
    /// </summary>
    partial class InsertOptionDialog
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoMultiValues = new System.Windows.Forms.RadioButton();
            this.rdoAllInsert = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtGoInsertLine = new System.Windows.Forms.TextBox();
            this.txtValueLine = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblSample = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // MsgArea
            // 
            this.MsgArea.Location = new System.Drawing.Point(160, 447);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(640, 432);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 24);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "キャンセル(&X)";
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOk.Location = new System.Drawing.Point(12, 432);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(96, 24);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "決定(&O)";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 12);
            this.label1.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoMultiValues);
            this.groupBox1.Controls.Add(this.rdoAllInsert);
            this.groupBox1.Location = new System.Drawing.Point(42, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(628, 123);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "INSERT文の形式";
            // 
            // rdoMultiValues
            // 
            this.rdoMultiValues.AutoSize = true;
            this.rdoMultiValues.Location = new System.Drawing.Point(42, 73);
            this.rdoMultiValues.Name = "rdoMultiValues";
            this.rdoMultiValues.Size = new System.Drawing.Size(174, 16);
            this.rdoMultiValues.TabIndex = 1;
            this.rdoMultiValues.Text = "1つのVALUES に複数行を記載";
            this.rdoMultiValues.UseVisualStyleBackColor = true;
            this.rdoMultiValues.Click += new System.EventHandler(this.rdoInsert_CheckedChanged);
            // 
            // rdoAllInsert
            // 
            this.rdoAllInsert.AutoSize = true;
            this.rdoAllInsert.Checked = true;
            this.rdoAllInsert.Location = new System.Drawing.Point(42, 34);
            this.rdoAllInsert.Name = "rdoAllInsert";
            this.rdoAllInsert.Size = new System.Drawing.Size(248, 16);
            this.rdoAllInsert.TabIndex = 0;
            this.rdoAllInsert.TabStop = true;
            this.rdoAllInsert.Text = "INSERT INTO (...) VALUES を全ての行に記載";
            this.rdoAllInsert.UseVisualStyleBackColor = true;
            this.rdoAllInsert.Click += new System.EventHandler(this.rdoInsert_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 359);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "GO を出力する行数";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 398);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "1つのVALUESに出力する行数";
            // 
            // txtGoInsertLine
            // 
            this.txtGoInsertLine.Location = new System.Drawing.Point(253, 356);
            this.txtGoInsertLine.Name = "txtGoInsertLine";
            this.txtGoInsertLine.Size = new System.Drawing.Size(209, 19);
            this.txtGoInsertLine.TabIndex = 3;
            this.txtGoInsertLine.Text = "1000";
            // 
            // txtValueLine
            // 
            this.txtValueLine.Location = new System.Drawing.Point(253, 395);
            this.txtValueLine.Name = "txtValueLine";
            this.txtValueLine.Size = new System.Drawing.Size(209, 19);
            this.txtValueLine.TabIndex = 4;
            this.txtValueLine.Text = "10";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblSample);
            this.groupBox2.Location = new System.Drawing.Point(42, 165);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(628, 168);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "出力サンプル";
            // 
            // lblSample
            // 
            this.lblSample.AutoSize = true;
            this.lblSample.Location = new System.Drawing.Point(17, 18);
            this.lblSample.Name = "lblSample";
            this.lblSample.Size = new System.Drawing.Size(43, 12);
            this.lblSample.TabIndex = 0;
            this.lblSample.Text = "サンプル";
            // 
            // InsertOptionDialog
            // 
            this.AcceptButton = this.btnOk;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(744, 468);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.txtValueLine);
            this.Controls.Add(this.txtGoInsertLine);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Name = "InsertOptionDialog";
            this.Text = "INSERT文の出力設定";
            this.Controls.SetChildIndex(this.MsgArea, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.txtGoInsertLine, 0);
            this.Controls.SetChildIndex(this.txtValueLine, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdoMultiValues;
        private System.Windows.Forms.RadioButton rdoAllInsert;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtGoInsertLine;
        private System.Windows.Forms.TextBox txtValueLine;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblSample;
    }
}
