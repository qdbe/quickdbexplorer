using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace quickDBExplorer
{
	/// <summary>
	/// select 文を入力する為のダイアログ
	/// </summary>
	public class QueryDialogSelect : quickDBExplorer.QueryDialog
	{
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public QueryDialogSelect()
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
			((System.ComponentModel.ISupportInitialize)(this.dHistory)).BeginInit();
			this.SuspendLayout();
			// 
			// txtInput
			// 
			this.txtInput.Name = "txtInput";
			this.txtInput.Size = new System.Drawing.Size(444, 256);
			// 
			// btnGo
			// 
			this.btnGo.Name = "btnGo";
			// 
			// chkReturn
			// 
			this.chkReturn.Enabled = false;
			this.chkReturn.Location = new System.Drawing.Point(240, 296);
			this.chkReturn.Name = "chkReturn";
			this.chkReturn.Size = new System.Drawing.Size(96, 16);
			this.chkReturn.Visible = false;
			// 
			// QueryDialogSelect
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(480, 325);
			this.Name = "QueryDialogSelect";
			((System.ComponentModel.ISupportInitialize)(this.dHistory)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
	}
}

