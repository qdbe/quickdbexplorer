using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace quickDBExplorer
{
	/// <summary>
	/// クエリ等を入力する為のダイアログ
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public class QueryDialog : System.Windows.Forms.Form
	{

		/// <summary>
		/// 入力された文字列
		/// </summary>
		private string	pSelectSql = "";
		/// <summary>
		/// 入力された文字列
		/// </summary>
		public string	SelectSql
		{
			get { return this.pSelectSql; }
			set { this.pSelectSql = value ; }
		}
		/// <summary>
		/// 結果に戻り値があるかどうか
		/// </summary>
		private bool		pHasReturn = false;
		/// <summary>
		/// 結果に戻り値があるかどうか
		/// </summary>
		public bool		HasReturn
		{
			get { return this.pHasReturn; }
			set { this.pHasReturn = value; }
		}
		
		/// <summary>
		/// テキスト入力エリア
		/// </summary>
		protected quickDBExplorerTextBox txtInput;
		/// <summary>
		/// OKボタン
		/// </summary>
		protected System.Windows.Forms.Button btnGo;
		/// <summary>
		/// キャンセル（閉じる）ボタン
		/// </summary>
		protected System.Windows.Forms.Button btnCancel;
		/// <summary>
		/// 戻り値あり チェックボックス
		/// </summary>
		protected System.Windows.Forms.CheckBox chkReturn;

		/// <summary>
		/// 履歴表示ボタン
		/// </summary>
		protected System.Windows.Forms.Button btnHistory;

		/// <summary>
		/// 入力履歴辞書データ
		/// </summary>
        public Dictionary<string, TextHistoryDataSet> Histories { get; set; }

        private string pHistoryKey;
        /// <summary>
        /// 履歴に利用するキー値
        /// </summary>
        public string HistoryKey
        {
            get { return pHistoryKey; }
            set
            {
                pHistoryKey = value;
                this.txtInput.Histories = this.Histories;
                this.txtInput.HistoryKey = value;
            }
        }

        /// <summary>
        /// 入力履歴
        /// </summary>
        public TextHistoryDataSet History
        {
            get { return this.Histories[this.HistoryKey]; }
        }

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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QueryDialog));
			this.btnGo = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.chkReturn = new System.Windows.Forms.CheckBox();
			this.btnHistory = new System.Windows.Forms.Button();
			this.txtInput = new quickDBExplorer.quickDBExplorerTextBox();
			this.SuspendLayout();
			// 
			// btnGo
			// 
			this.btnGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnGo.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnGo.Location = new System.Drawing.Point(16, 233);
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
			this.btnCancel.Location = new System.Drawing.Point(348, 233);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(88, 24);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "キャンセル(&X)";
			// 
			// chkReturn
			// 
			this.chkReturn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.chkReturn.Location = new System.Drawing.Point(40, 209);
			this.chkReturn.Name = "chkReturn";
			this.chkReturn.Size = new System.Drawing.Size(208, 16);
			this.chkReturn.TabIndex = 3;
			this.chkReturn.Text = "戻り値あり(&R)";
			// 
			// btnHistory
			// 
			this.btnHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnHistory.Location = new System.Drawing.Point(144, 233);
			this.btnHistory.Name = "btnHistory";
			this.btnHistory.Size = new System.Drawing.Size(88, 24);
			this.btnHistory.TabIndex = 2;
			this.btnHistory.Text = "履歴引用(&L)";
			this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);
			// 
			// txtInput
			// 
			this.txtInput.AcceptsReturn = true;
			this.txtInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtInput.ImeMode = System.Windows.Forms.ImeMode.Off;
			this.txtInput.CanCtrlDelete = true;
			this.txtInput.IsDigitOnly = false;
			this.txtInput.Location = new System.Drawing.Point(16, 12);
			this.txtInput.Multiline = true;
			this.txtInput.Name = "txtInput";
			this.txtInput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtInput.Size = new System.Drawing.Size(444, 191);
			this.txtInput.TabIndex = 0;
			this.txtInput.WordWrap = false;
			this.txtInput.ShowHistory += new quickDBExplorer.ShowHistoryEventHandler(this.txtInput_ShowHistory);
			// 
			// QueryDialog
			// 
			this.AcceptButton = this.btnGo;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(480, 262);
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
			this.VisibleChanged += new System.EventHandler(this.QueryDialog_VisibleChanged);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion


		/// <summary>
		/// OKボタン押下時イベントハンドラ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		internal virtual void btnGo_Click(object sender, System.EventArgs e)
		{
			SelectSql = this.txtInput.Text;
			if( this.chkReturn.Checked == true )
			{
				this.pHasReturn = true;
			}
			else
			{
				this.pHasReturn = false;
			}
			//this.DialogResult = DialogResult.OK;
            this.txtInput.SaveHistory("");
		}

		private void QueryDialog_Load(object sender, System.EventArgs e)
		{
			this.txtInput.Text = SelectSql;
			this.txtInput.Focus();
		}

		/// <summary>
		/// 履歴引用ボタンの押下
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnHistory_Click(object sender, System.EventArgs e)
		{
			// 入力履歴の選択ダイアログを表示する
			this.txtInput.DoShowHistory("");
		}

		private void QueryDialog_VisibleChanged(object sender, EventArgs e)
		{
			this.txtInput.Focus();
		}

		private void txtInput_ShowHistory(object sender, EventArgs e)
		{
			// 入力履歴の選択ダイアログを表示する
			this.txtInput.DoShowHistory("");
		}

	}
}
