using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace quickDBExplorer
{
	public class CmdInputDialog : quickDBExplorer.QueryDialog
	{
		private System.Windows.Forms.Label label1;
		private System.ComponentModel.IContainer components = null;

		public CmdInputDialog()
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
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dHistory)).BeginInit();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(16, 38);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(444, 218);
			// 
			// button1
			// 
			this.button1.Name = "button1";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(14, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(434, 24);
			this.label1.TabIndex = 4;
			this.label1.Text = "{0}をクエリ内に記載することで、選択しているテーブル名に実行時に変換されます";
			// 
			// CmdInputDialog
			// 
			this.AcceptButton = null;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(480, 325);
			this.Controls.Add(this.label1);
			this.Name = "CmdInputDialog";
			this.Text = "各種クエリ実行(テーブル引数)";
			this.Controls.SetChildIndex(this.button1, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.textBox1, 0);
			((System.ComponentModel.ISupportInitialize)(this.dHistory)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		protected override void button1_Click(object sender, EventArgs e)
		{
			if( this.textBox1.Text != "" &&
				this.textBox1.Text.IndexOf("{0}") < 0 )
			{
				return;
			}
			base.button1_Click (sender, e);
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

	}
}

