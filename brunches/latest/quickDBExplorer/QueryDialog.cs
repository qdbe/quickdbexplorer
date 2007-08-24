using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace quickDBExplorer
{
	/// <summary>
	/// クエリ等を入力する為のダイアログ
	/// </summary>
	public class QueryDialog : System.Windows.Forms.Form
	{

		/// <summary>
		/// 入力された文字列
		/// </summary>
		protected string	selectSql = "";
		/// <summary>
		/// 入力された文字列
		/// </summary>
		public string	SelectSql
		{
			get { return this.selectSql; }
			set { this.selectSql = value ; }
		}
		/// <summary>
		/// 結果に戻り値があるかどうか
		/// </summary>
		protected bool		hasReturn = false;
		/// <summary>
		/// 結果に戻り値があるかどうか
		/// </summary>
		public bool		HasReturn
		{
			get { return this.hasReturn; }
			set { this.hasReturn = value; }
		}
		
		/// <summary>
		/// テキスト入力エリア
		/// </summary>
		protected System.Windows.Forms.TextBox txtInput;
		/// <summary>
		/// OKボタン
		/// </summary>
		protected System.Windows.Forms.Button btnGo;
		/// <summary>
		/// 戻るボタン
		/// </summary>
		private System.Windows.Forms.Button btnCancel;
		/// <summary>
		/// テキスト入力エリアでの右クリックメニュー
		/// </summary>
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuCopy;
		private System.Windows.Forms.MenuItem menuCut;
		private System.Windows.Forms.MenuItem menuPaste;
		private System.Windows.Forms.MenuItem menuAllSelect;
		/// <summary>
		/// 戻り値あり チェックボックス
		/// </summary>
		protected System.Windows.Forms.CheckBox chkReturn;
		private System.Windows.Forms.Button btnHistory;

		/// <summary>
		/// 入力履歴データ
		/// </summary>
		protected textHistory  dHistory = new textHistory();

		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public QueryDialog()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(QueryDialog));
			this.txtInput = new System.Windows.Forms.TextBox();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuCopy = new System.Windows.Forms.MenuItem();
			this.menuCut = new System.Windows.Forms.MenuItem();
			this.menuPaste = new System.Windows.Forms.MenuItem();
			this.menuAllSelect = new System.Windows.Forms.MenuItem();
			this.btnGo = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.chkReturn = new System.Windows.Forms.CheckBox();
			this.btnHistory = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtInput
			// 
			this.txtInput.AcceptsReturn = true;
			this.txtInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtInput.ContextMenu = this.contextMenu1;
			this.txtInput.ImeMode = System.Windows.Forms.ImeMode.Off;
			this.txtInput.Location = new System.Drawing.Point(16, 24);
			this.txtInput.Multiline = true;
			this.txtInput.Name = "txtInput";
			this.txtInput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtInput.Size = new System.Drawing.Size(444, 232);
			this.txtInput.TabIndex = 0;
			this.txtInput.Text = "";
			this.txtInput.WordWrap = false;
			this.txtInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInput_KeyDown);
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuCopy,
																						 this.menuCut,
																						 this.menuPaste,
																						 this.menuAllSelect});
			// 
			// menuCopy
			// 
			this.menuCopy.Index = 0;
			this.menuCopy.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
			this.menuCopy.Text = "コピー";
			this.menuCopy.Click += new System.EventHandler(this.menuCopy_Click);
			// 
			// menuCut
			// 
			this.menuCut.Index = 1;
			this.menuCut.Shortcut = System.Windows.Forms.Shortcut.CtrlX;
			this.menuCut.Text = "切り取り";
			this.menuCut.Click += new System.EventHandler(this.menuCut_Click);
			// 
			// menuPaste
			// 
			this.menuPaste.Index = 2;
			this.menuPaste.Shortcut = System.Windows.Forms.Shortcut.CtrlV;
			this.menuPaste.Text = "貼り付け";
			this.menuPaste.Click += new System.EventHandler(this.menuPaste_Click);
			// 
			// menuAllSelect
			// 
			this.menuAllSelect.Index = 3;
			this.menuAllSelect.Shortcut = System.Windows.Forms.Shortcut.CtrlA;
			this.menuAllSelect.Text = "全て選択";
			this.menuAllSelect.Click += new System.EventHandler(this.menuAllSelect_Click);
			// 
			// btnGo
			// 
			this.btnGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnGo.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnGo.Location = new System.Drawing.Point(16, 296);
			this.btnGo.Name = "btnGo";
			this.btnGo.Size = new System.Drawing.Size(96, 24);
			this.btnGo.TabIndex = 1;
			this.btnGo.Text = "SQL実行(&O)";
			this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(348, 292);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(88, 24);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "キャンセル(&X)";
			// 
			// chkReturn
			// 
			this.chkReturn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.chkReturn.Location = new System.Drawing.Point(40, 272);
			this.chkReturn.Name = "chkReturn";
			this.chkReturn.Size = new System.Drawing.Size(208, 16);
			this.chkReturn.TabIndex = 3;
			this.chkReturn.Text = "戻り値あり(&R)";
			// 
			// btnHistory
			// 
			this.btnHistory.Location = new System.Drawing.Point(144, 296);
			this.btnHistory.Name = "btnHistory";
			this.btnHistory.Size = new System.Drawing.Size(88, 23);
			this.btnHistory.TabIndex = 2;
			this.btnHistory.Text = "履歴引用(&L)";
			this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);
			// 
			// QueryDialog
			// 
			this.AcceptButton = this.btnGo;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(480, 325);
			this.Controls.Add(this.btnHistory);
			this.Controls.Add(this.chkReturn);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnGo);
			this.Controls.Add(this.txtInput);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "QueryDialog";
			this.ShowInTaskbar = false;
			this.Text = "SQL文指定";
			this.Load += new System.EventHandler(this.QueryDialog_Load);
			this.ResumeLayout(false);

		}
		#endregion


		/// <summary>
		/// OKボタン押下時イベントハンドラ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void btnGo_Click(object sender, System.EventArgs e)
		{
			SelectSql = this.txtInput.Text;
			if( this.chkReturn.Checked == true )
			{
				this.hasReturn = true;
			}
			else
			{
				this.hasReturn = false;
			}
			//this.DialogResult = DialogResult.OK;
			qdbeUtil.SetNewHistory("",this.txtInput.Text,ref this.dHistory);
		}

		private void QueryDialog_Load(object sender, System.EventArgs e)
		{
			this.txtInput.Text = SelectSql;
			this.txtInput.Focus();
		}

		private void menuCopy_Click(object sender, System.EventArgs e)
		{
			this.txtInput.Copy();
		}

		private void menuCut_Click(object sender, System.EventArgs e)
		{
			this.txtInput.Cut();
		}

		private void menuPaste_Click(object sender, System.EventArgs e)
		{
			this.txtInput.Paste();
		}

		private void menuAllSelect_Click(object sender, System.EventArgs e)
		{
			this.txtInput.SelectAll();
		}

		/// <summary>
		/// 履歴引用ボタンの押下
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnHistory_Click(object sender, System.EventArgs e)
		{
			// 入力履歴の選択ダイアログを表示する
			HistoryViewer hv = new HistoryViewer(this.dHistory, "");
			hv.IsShowTable = false;
			if( DialogResult.OK == hv.ShowDialog() && this.txtInput.Text != hv.retString)
			{
				//違う情報であれば、それを表示し、履歴として追加する
				this.txtInput.Text = hv.retString;
				qdbeUtil.SetNewHistory("",hv.retString,ref this.dHistory);
			}
		}

		/// <summary>
		/// キーダウンイベントハンドラ
		/// </summary>
		/// <param name="sender">--</param>
		/// <param name="e">--</param>
		private void txtInput_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			// Ctrl+Dで全入力文字削除
			if( e.Alt == false &&
				e.Control == true &&
				e.KeyCode == Keys.D )
			{
				// 全削除を行う
				((TextBox)sender).Text = "";
			}

			// Ctrl＋Sで過去入力履歴ダイアログを表示する
			if( e.Alt == false &&
				e.Control == true &&
				e.KeyCode == Keys.S )
			{
				HistoryViewer hv = new HistoryViewer(this.dHistory, "");
				hv.IsShowTable = false;
				if( DialogResult.OK == hv.ShowDialog() && this.txtInput.Text != hv.retString)
				{
					this.txtInput.Text = hv.retString;
					qdbeUtil.SetNewHistory("",hv.retString,ref this.dHistory);
				}
			}
		
		}

	}
}
