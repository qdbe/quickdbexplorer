using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace quickDBExplorer
{
	/// <summary>
	/// 値の拡大表示用ダイアログ
	/// where, order 等のテキストボックスの値を、全体が見えるようにするダイアログ
	/// ここで値の編集が可能
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public class ZoomDialog : quickDBExplorerBaseForm
	{
		/// <summary>
		/// 表示テキストエリア
		/// </summary>
		protected quickDBExplorerTextBox txtZoom;
		/// <summary>
		/// OKボタン
		/// </summary>
		protected System.Windows.Forms.Button btnOk;
		/// <summary>
		/// 閉じるボタン
		/// </summary>
		protected System.Windows.Forms.Button btnClose;
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// 表示するテキストの編集値
		/// </summary>
		private string   pEditText = "";

		/// <summary>
		/// タイトル名称
		/// </summary>
		private string	pLabelName = "";

		/// <summary>
		/// 表示のみか否か
		/// true ： 表示のみ
		/// false : 編集可能
		/// </summary>
		private bool		pIsDisplayOnly = false;

		/// <summary>
		/// 表示するテキストの編集値
		/// </summary>
		public virtual string EditText
		{
			get { return this.pEditText; }
			set 
			{
				this.pEditText = value; 
				if( this.txtZoom != null &&
					this.txtZoom.Visible == true &&
					this.txtZoom.IsDisposed != true )
				{
					this.txtZoom.Text = value;
                    TextValuesChanged();
				}
			}
		}

        /// <summary>
        /// タイトル名称
        /// </summary>
        public virtual string LableName
		{
			get { return this.pLabelName; }
			set 
			{ 
				this.pLabelName = value; 
				this.Text = value;
			}
		}

		/// <summary>
		/// 表示のみか否か
		/// true ： 表示のみ
		/// false : 編集可能
		/// </summary>
		public virtual bool IsDispOnly
		{
			get { return this.pIsDisplayOnly; }
			set { this.pIsDisplayOnly = value; }
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ZoomDialog()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ZoomDialog));
			this.txtZoom = new quickDBExplorer.quickDBExplorerTextBox();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// MsgArea
			// 
			this.MsgArea.Location = new System.Drawing.Point(88, 240);
			this.MsgArea.Size = new System.Drawing.Size(372, 16);
			// 
			// txtZoom
			// 
			this.txtZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtZoom.CanCtrlDelete = true;
			this.txtZoom.IsDigitOnly = false;
			this.txtZoom.Location = new System.Drawing.Point(14, 12);
			this.txtZoom.Multiline = true;
			this.txtZoom.Name = "txtZoom";
			this.txtZoom.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtZoom.Size = new System.Drawing.Size(526, 216);
			this.txtZoom.TabIndex = 0;
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnOk.Location = new System.Drawing.Point(10, 238);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(70, 26);
			this.btnOk.TabIndex = 1;
			this.btnOk.Text = "決定(&O)";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(468, 242);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(70, 24);
			this.btnClose.TabIndex = 2;
			this.btnClose.Text = "閉じる(&X)";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// ZoomDialog
			// 
			//this.AcceptButton = this.btnOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(550, 272);
			this.Controls.Add(this.txtZoom);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnOk);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ZoomDialog";
			this.Text = "ZoomDialog";
			this.Load += new System.EventHandler(this.ZoomDialog_Load);
			this.Controls.SetChildIndex(this.btnOk, 0);
			this.Controls.SetChildIndex(this.btnClose, 0);
			this.Controls.SetChildIndex(this.txtZoom, 0);
			this.Controls.SetChildIndex(this.MsgArea, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// 画面初期表示時処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		internal virtual void ZoomDialog_Load(object sender, System.EventArgs e)
		{
			this.txtZoom.Text = this.pEditText;
			if( this.pIsDisplayOnly == true)
			{
				this.btnOk.Enabled = false;
			}
		}

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			this.pEditText = this.txtZoom.Text;
			this.DialogResult = DialogResult.OK;
			this.OnEnter(new EventArgs());
			this.Close();
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
        
        /// <summary>
        /// 何か値が変わった時に呼び出される
        /// 派生先での利用を想定
        /// </summary>
        protected virtual void TextValuesChanged()
        {
            
        }

    }
}
