using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace quickDBExplorer
{
	/// <summary>
	/// オブジェクトに対する各種コマンド実行 のコマンド入力用ダイアログ
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public class CmdInputDialog : quickDBExplorer.QueryDialog
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CmdInputDialog()
		{
			// この呼び出しは Windows フォーム デザイナで必要です。
			InitializeComponent();
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(22, 61);
            this.txtInput.Size = new System.Drawing.Size(430, 177);
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(22, 280);
            this.btnGo.Size = new System.Drawing.Size(135, 30);
            this.btnGo.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(312, 280);
            this.btnCancel.Size = new System.Drawing.Size(123, 30);
            this.btnCancel.TabIndex = 5;
            // 
            // chkReturn
            // 
            this.chkReturn.Location = new System.Drawing.Point(-170, 245);
            this.chkReturn.Size = new System.Drawing.Size(292, 20);
            this.chkReturn.TabIndex = 2;
            // 
            // btnHistory
            // 
            this.btnHistory.Location = new System.Drawing.Point(202, 280);
            this.btnHistory.Size = new System.Drawing.Size(123, 30);
            this.btnHistory.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(20, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(607, 22);
            this.label1.TabIndex = 4;
            this.label1.Text = "{0}をクエリ内に記載することで、選択しているオブジェクト名に実行時に変換されます";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(20, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(607, 23);
            this.label2.TabIndex = 4;
            this.label2.Text = "例： truncate table {0}";
            // 
            // CmdInputDialog
            // 
            this.AcceptButton = null;
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 15);
            this.ClientSize = new System.Drawing.Size(480, 325);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Name = "CmdInputDialog";
            this.Text = "各種クエリ実行(オブジェクト引数)";
            this.VisibleChanged += new System.EventHandler(this.CmdInputDialog_VisibleChanged);
            this.Controls.SetChildIndex(this.txtInput, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.chkReturn, 0);
            this.Controls.SetChildIndex(this.btnHistory, 0);
            this.Controls.SetChildIndex(this.btnGo, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// OKボタン押下時処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		internal override void btnGo_Click(object sender, EventArgs e)
		{
			if( this.txtInput.Text != "" &&
				this.txtInput.Text.IndexOf("{0}") < 0 )
			{
				return;
			}
			base.btnGo_Click (sender, e);
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void CmdInputDialog_VisibleChanged(object sender, EventArgs e)
		{
			this.txtInput.Focus();
		}

	}
}

