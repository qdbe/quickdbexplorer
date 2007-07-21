using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace quickDBExplorer
{
	/// <summary>
	/// QuerySelectDialog の概要の説明です。
	/// </summary>
	public class QuerySelectDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;

		public string	SelectSql = "";
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.Button btnHistory;

		protected textHistory  dHistory = new textHistory();

		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public QuerySelectDialog()
		{
			//
			// Windows フォーム デザイナ サポートに必要です。
			//
			InitializeComponent();

		}

		/// <summary>
		/// 入力履歴情報
		/// </summary>
		public textHistory DHistory
		{
			get { return this.dHistory; }
			set { this.dHistory = value; }
		}

		/// <summary>
		/// 使用されているリソースに後処理を実行します。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows フォーム デザイナで生成されたコード 
		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(QuerySelectDialog));
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.btnHistory = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.AcceptsReturn = true;
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.ContextMenu = this.contextMenu1;
			this.textBox1.ImeMode = System.Windows.Forms.ImeMode.Off;
			this.textBox1.Location = new System.Drawing.Point(16, 24);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBox1.Size = new System.Drawing.Size(444, 208);
			this.textBox1.TabIndex = 0;
			this.textBox1.Text = "";
			this.textBox1.WordWrap = false;
			this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem1,
																						 this.menuItem2,
																						 this.menuItem3,
																						 this.menuItem4});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
			this.menuItem1.Text = "コピー";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.Shortcut = System.Windows.Forms.Shortcut.CtrlX;
			this.menuItem2.Text = "切り取り";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 2;
			this.menuItem3.Shortcut = System.Windows.Forms.Shortcut.CtrlV;
			this.menuItem3.Text = "貼り付け";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 3;
			this.menuItem4.Shortcut = System.Windows.Forms.Shortcut.CtrlA;
			this.menuItem4.Text = "全て選択";
			this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.button1.Location = new System.Drawing.Point(16, 240);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(96, 24);
			this.button1.TabIndex = 1;
			this.button1.Text = "SQL実行(&O)";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.Location = new System.Drawing.Point(348, 240);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(88, 24);
			this.button2.TabIndex = 2;
			this.button2.Text = "キャンセル(&X)";
			// 
			// btnHistory
			// 
			this.btnHistory.Location = new System.Drawing.Point(128, 240);
			this.btnHistory.Name = "btnHistory";
			this.btnHistory.Size = new System.Drawing.Size(80, 23);
			this.btnHistory.TabIndex = 3;
			this.btnHistory.Text = "履歴引用(&L)";
			this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);
			// 
			// QuerySelectDialog
			// 
			this.AcceptButton = this.button1;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.CancelButton = this.button2;
			this.ClientSize = new System.Drawing.Size(480, 273);
			this.Controls.Add(this.btnHistory);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.textBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "QuerySelectDialog";
			this.ShowInTaskbar = false;
			this.Text = "SQL文指定";
			this.Load += new System.EventHandler(this.QuerySelectDialog_Load);
			this.ResumeLayout(false);

		}
		#endregion


		private void button1_Click(object sender, System.EventArgs e)
		{
			SelectSql = this.textBox1.Text;
			//this.DialogResult = DialogResult.OK;
			MainForm.SetNewHistory("",this.textBox1.Text,ref this.dHistory);
		}

		private void QuerySelectDialog_Load(object sender, System.EventArgs e)
		{
			this.textBox1.Text = SelectSql;
			this.textBox1.Focus();
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			this.textBox1.Copy();
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			this.textBox1.Cut();
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			this.textBox1.Paste();
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			this.textBox1.SelectAll();
		}

		private void btnHistory_Click(object sender, System.EventArgs e)
		{
			HistoryViewer hv = new HistoryViewer(this.dHistory, "");
			hv.IsShowTable = false;

			if( DialogResult.OK == hv.ShowDialog() && this.textBox1.Text != hv.retString)
			{
				this.textBox1.Text = hv.retString;
				MainForm.SetNewHistory("",hv.retString,ref this.dHistory);
			}
		}

		private void textBox1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if( e.Alt == false &&
				e.Control == true &&
				e.KeyCode == Keys.D )
			{
				// 全削除を行う
				((TextBox)sender).Text = "";
			}
			if( e.Alt == false &&
				e.Control == true &&
				e.KeyCode == Keys.S )
			{
				HistoryViewer hv = new HistoryViewer(this.dHistory, "");
				hv.IsShowTable = false;
				if( DialogResult.OK == hv.ShowDialog() && this.textBox1.Text != hv.retString)
				{
					this.textBox1.Text = hv.retString;
					MainForm.SetNewHistory("",hv.retString,ref this.dHistory);
				}
			}
		}

	}
}
