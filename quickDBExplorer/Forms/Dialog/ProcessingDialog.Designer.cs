using quickDBExplorer.Controls;

namespace quickDBExplorer.Forms
{
    partial class ProcessingDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.progressBar1 = new quickDBExplorer.Controls.qdbeProgressBar();
            this.txtCurVal = new System.Windows.Forms.TextBox();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblCurTarget = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // MsgArea
            // 
            this.MsgArea.Location = new System.Drawing.Point(160, 213);
            this.MsgArea.Size = new System.Drawing.Size(226, 16);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "処理済み";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(226, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "/  TOTAL";
            // 
            // progressBar1
            // 
            this.progressBar1.BarColor = System.Drawing.Color.LawnGreen;
            this.progressBar1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.progressBar1.Location = new System.Drawing.Point(33, 70);
            this.progressBar1.Maximum = 0;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(381, 43);
            this.progressBar1.TabIndex = 2;
            this.progressBar1.Value = 1;
            // 
            // txtCurVal
            // 
            this.txtCurVal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCurVal.Location = new System.Drawing.Point(126, 45);
            this.txtCurVal.Name = "txtCurVal";
            this.txtCurVal.ReadOnly = true;
            this.txtCurVal.Size = new System.Drawing.Size(94, 12);
            this.txtCurVal.TabIndex = 3;
            this.txtCurVal.TabStop = false;
            // 
            // txtTotal
            // 
            this.txtTotal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTotal.Location = new System.Drawing.Point(287, 45);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(94, 12);
            this.txtTotal.TabIndex = 3;
            this.txtTotal.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(137, 139);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(159, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "中断";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblCurTarget
            // 
            this.lblCurTarget.Location = new System.Drawing.Point(31, 9);
            this.lblCurTarget.Name = "lblCurTarget";
            this.lblCurTarget.Size = new System.Drawing.Size(383, 18);
            this.lblCurTarget.TabIndex = 5;
            // 
            // ProcessingDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 191);
            this.Controls.Add(this.lblCurTarget);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.txtCurVal);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProcessingDialog";
            this.Text = "処理中..";
            this.Controls.SetChildIndex(this.MsgArea, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.progressBar1, 0);
            this.Controls.SetChildIndex(this.txtCurVal, 0);
            this.Controls.SetChildIndex(this.txtTotal, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.lblCurTarget, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private qdbeProgressBar progressBar1;
        private System.Windows.Forms.TextBox txtCurVal;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblCurTarget;
    }
}