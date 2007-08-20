using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace quickDBExplorer
{
	/// <summary>
	/// エラー情報表示エリアを持つ、base となるダイアログクラス。
	/// 継承して利用することを想定
	/// </summary>
	public class quickDBExplorerBaseForm : System.Windows.Forms.Form
	{
		protected System.Windows.Forms.Label msgArea;
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.ErrorProvider errorProvider1;
		/// <summary>
		/// 表示するエラーメッセージ
		/// </summary>
		protected string  errMessage = "";

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public quickDBExplorerBaseForm()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(quickDBExplorerBaseForm));
			this.msgArea = new System.Windows.Forms.Label();
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider();
			this.SuspendLayout();
			// 
			// msgArea
			// 
			this.msgArea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.msgArea.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(128)));
			this.msgArea.ForeColor = System.Drawing.Color.Red;
			this.msgArea.Location = new System.Drawing.Point(48, 224);
			this.msgArea.Name = "msgArea";
			this.msgArea.Size = new System.Drawing.Size(232, 24);
			this.msgArea.TabIndex = 10;
			this.msgArea.DoubleClick += new System.EventHandler(this.msgArea_DoubleClick);
			// 
			// errorProvider1
			// 
			this.errorProvider1.ContainerControl = this;
			// 
			// quickDBExplorerBaseForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(288, 266);
			this.Controls.Add(this.msgArea);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "quickDBExplorerBaseForm";
			this.Text = "quickDBExplorerBaseForm";
			this.Load += new System.EventHandler(this.quickDBExplorerBaseForm_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void quickDBExplorerBaseForm_Load(object sender, System.EventArgs e)
		{
			this.msgArea.Text = "";
		}

		/// <summary>
		/// エラーメッセージを初期化し、クリアする
		/// </summary>
		protected void InitErrMessage()
		{
			this.msgArea.Text = "";
			this.errMessage = "";
			this.errorProvider1.SetIconAlignment(this.msgArea,ErrorIconAlignment.MiddleLeft);
			this.errorProvider1.SetError(this.msgArea,"");
		}

		/// <summary>
		/// Exception の内容からエラーメッセージを表示する
		/// </summary>
		/// <param name="ex">発生したException
		/// このException の Message がエラーメッセージ領域に表示され、ToString()した結果が ダブルクリック時のクリップボード貼り付け対象となる</param>
		protected void SetErrorMessage(Exception ex)
		{
			this.msgArea.Text = ex.Message;
			this.errMessage = ex.ToString();
			this.errorProvider1.SetError(this.msgArea,this.msgArea.Text);
		}

		/// <summary>
		/// エラーメッセージの領域に表示する内容をセットする
		/// </summary>
		/// <param name="dspdata"></param>
		protected void SetMessageArea(object dspdata)
		{
			this.msgArea.Text = dspdata.ToString();
			Application.DoEvents();
		}

		/// <summary>
		/// エラーメッセージ領域をダブルクリックした場合、エラーメッセージの詳細な内容をクリップボードにコピーする
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void msgArea_DoubleClick(object sender, System.EventArgs e)
		{
			if( this.errMessage != "" )
			{
				Clipboard.SetDataObject(this.errMessage,true );
			}
		}
	}
}
