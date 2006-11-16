using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace quickDBExplorer
{
	/// <summary>
	/// quickDBExplorerBaseForm の概要の説明です。
	/// </summary>
	public class quickDBExplorerBaseForm : System.Windows.Forms.Form
	{
		protected System.Windows.Forms.Label msgArea;
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.ErrorProvider errorProvider1;
		protected string  errMessage = "";

		public quickDBExplorerBaseForm()
		{
			//
			// Windows フォーム デザイナ サポートに必要です。
			//
			InitializeComponent();

			//
			// TODO: InitializeComponent 呼び出しの後に、コンストラクタ コードを追加してください。
			//
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

		protected void InitErrMessage()
		{
			this.msgArea.Text = "";
			this.errMessage = "";
			this.errorProvider1.SetIconAlignment(this.msgArea,ErrorIconAlignment.MiddleLeft);
			this.errorProvider1.SetError(this.msgArea,"");
		}

		protected void SetErrorMessage(Exception ex)
		{
			this.msgArea.Text = ex.Message;
			this.errMessage = ex.ToString();
			this.errorProvider1.SetError(this.msgArea,this.msgArea.Text);
		}

		private void msgArea_DoubleClick(object sender, System.EventArgs e)
		{
			if( this.errMessage != "" )
			{
				Clipboard.SetDataObject(this.errMessage,true );
			}
		}
	}
}
