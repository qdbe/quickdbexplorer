using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace serialFactory
{
	/// <summary>
	/// RegisterInfoDialog の概要の説明です。
	/// </summary>
	public class RegisterInfoDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtUserName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtLimitDate;
		private System.Windows.Forms.Button btnClose;
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SerialManager	smanager;


		public RegisterInfoDialog()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(RegisterInfoDialog));
			this.label1 = new System.Windows.Forms.Label();
			this.txtUserName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtLimitDate = new System.Windows.Forms.TextBox();
			this.btnClose = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(88, 18);
			this.label1.TabIndex = 0;
			this.label1.Text = "登録ユーザー名";
			// 
			// txtUserName
			// 
			this.txtUserName.Location = new System.Drawing.Point(116, 20);
			this.txtUserName.Name = "txtUserName";
			this.txtUserName.ReadOnly = true;
			this.txtUserName.Size = new System.Drawing.Size(396, 19);
			this.txtUserName.TabIndex = 1;
			this.txtUserName.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 52);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(88, 18);
			this.label2.TabIndex = 0;
			this.label2.Text = "利用有効期限";
			// 
			// txtLimitDate
			// 
			this.txtLimitDate.Location = new System.Drawing.Point(116, 52);
			this.txtLimitDate.Name = "txtLimitDate";
			this.txtLimitDate.ReadOnly = true;
			this.txtLimitDate.Size = new System.Drawing.Size(396, 19);
			this.txtLimitDate.TabIndex = 2;
			this.txtLimitDate.Text = "";
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(442, 84);
			this.btnClose.Name = "btnClose";
			this.btnClose.TabIndex = 3;
			this.btnClose.Text = "閉じる(&X)";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// RegisterInfoDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(536, 116);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.txtLimitDate);
			this.Controls.Add(this.txtUserName);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "RegisterInfoDialog";
			this.Text = "登録情報";
			this.Load += new System.EventHandler(this.RegisterInfoDialog_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void RegisterInfoDialog_Load(object sender, System.EventArgs e)
		{
			this.txtUserName.Text = smanager.UserName;
			this.txtLimitDate.Text = smanager.LimitDate;
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
