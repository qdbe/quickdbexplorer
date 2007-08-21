using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace quickDBExplorer
{
	/// <summary>
	/// テーブル指定ダイアログ
	/// </summary>
	public class TableSelectDialog : quickDBExplorer.quickDBExplorerBaseForm
	{
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox txtTableSelect;
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 当ダイアログでの入力結果
		/// </summary>
		public string ResultStr = "";

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public TableSelectDialog()
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
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.txtTableSelect = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// msgArea
			// 
			this.msgArea.Location = new System.Drawing.Point(136, 236);
			this.msgArea.Name = "msgArea";
			this.msgArea.Size = new System.Drawing.Size(232, 16);
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.Location = new System.Drawing.Point(412, 228);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(88, 24);
			this.button2.TabIndex = 12;
			this.button2.Text = "キャンセル(&X)";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.button1.Location = new System.Drawing.Point(12, 228);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(96, 24);
			this.button1.TabIndex = 11;
			this.button1.Text = "決定(&O)";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// txtTableSelect
			// 
			this.txtTableSelect.Location = new System.Drawing.Point(20, 12);
			this.txtTableSelect.Multiline = true;
			this.txtTableSelect.Name = "txtTableSelect";
			this.txtTableSelect.Size = new System.Drawing.Size(480, 204);
			this.txtTableSelect.TabIndex = 13;
			this.txtTableSelect.Text = "";
			// 
			// TableSelectDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(520, 257);
			this.Controls.Add(this.txtTableSelect);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Name = "TableSelectDialog";
			this.Load += new System.EventHandler(this.TableSelectDialog_Load);
			this.Controls.SetChildIndex(this.msgArea, 0);
			this.Controls.SetChildIndex(this.button1, 0);
			this.Controls.SetChildIndex(this.button2, 0);
			this.Controls.SetChildIndex(this.txtTableSelect, 0);
			this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			if( this.txtTableSelect.Text != "" )
			{
				this.ResultStr = this.txtTableSelect.Text;
			}
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void TableSelectDialog_Load(object sender, System.EventArgs e)
		{
			this.ResultStr = "";
			this.txtTableSelect.Text = "";
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}

