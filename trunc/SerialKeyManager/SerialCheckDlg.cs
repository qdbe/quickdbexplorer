using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using serialFactory;


namespace SerialKeyManager
{
	/// <summary>
	/// SerialCheckDlg の概要の説明です。
	/// </summary>
	public class SerialCheckDlg : System.Windows.Forms.Form
	{
		SerialFactory sfact = new SerialFactory();

		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.TextBox txtMakeKeyDate;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnCheckSerialKey;
		private System.Windows.Forms.Button btnCheckSetup;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtSerialKey;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtSerialArray;
		private System.Windows.Forms.TextBox txtSetupDate;
		private System.Windows.Forms.TextBox txtLimitDate;
		private System.Windows.Forms.TextBox txtLimitDays;
		private System.Windows.Forms.TextBox txtMsg;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox txtUser;
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SerialCheckDlg()
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
			this.btnClose = new System.Windows.Forms.Button();
			this.txtMakeKeyDate = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtSerialArray = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.btnCheckSerialKey = new System.Windows.Forms.Button();
			this.btnCheckSetup = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.txtSerialKey = new System.Windows.Forms.TextBox();
			this.txtSetupDate = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtLimitDate = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.txtLimitDays = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.txtMsg = new System.Windows.Forms.TextBox();
			this.txtUser = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(484, 552);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(80, 40);
			this.btnClose.TabIndex = 4;
			this.btnClose.Text = "閉じる(&X)";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// txtMakeKeyDate
			// 
			this.txtMakeKeyDate.Location = new System.Drawing.Point(112, 340);
			this.txtMakeKeyDate.Name = "txtMakeKeyDate";
			this.txtMakeKeyDate.ReadOnly = true;
			this.txtMakeKeyDate.Size = new System.Drawing.Size(332, 19);
			this.txtMakeKeyDate.TabIndex = 21;
			this.txtMakeKeyDate.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(20, 340);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(76, 16);
			this.label3.TabIndex = 18;
			this.label3.Text = "キー作成日";
			// 
			// txtSerialArray
			// 
			this.txtSerialArray.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(128)));
			this.txtSerialArray.Location = new System.Drawing.Point(112, 224);
			this.txtSerialArray.Multiline = true;
			this.txtSerialArray.Name = "txtSerialArray";
			this.txtSerialArray.ReadOnly = true;
			this.txtSerialArray.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtSerialArray.Size = new System.Drawing.Size(332, 100);
			this.txtSerialArray.TabIndex = 14;
			this.txtSerialArray.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 224);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(80, 24);
			this.label2.TabIndex = 13;
			this.label2.Text = "シリアルキー";
			// 
			// btnCheckSerialKey
			// 
			this.btnCheckSerialKey.Location = new System.Drawing.Point(296, 152);
			this.btnCheckSerialKey.Name = "btnCheckSerialKey";
			this.btnCheckSerialKey.Size = new System.Drawing.Size(232, 24);
			this.btnCheckSerialKey.TabIndex = 3;
			this.btnCheckSerialKey.Text = "シリアルキーとして検証";
			this.btnCheckSerialKey.Click += new System.EventHandler(this.btnCheckSerialKey_Click);
			// 
			// btnCheckSetup
			// 
			this.btnCheckSetup.Location = new System.Drawing.Point(44, 152);
			this.btnCheckSetup.Name = "btnCheckSetup";
			this.btnCheckSetup.Size = new System.Drawing.Size(200, 24);
			this.btnCheckSetup.TabIndex = 2;
			this.btnCheckSetup.Text = "セットアップキーとして検証";
			this.btnCheckSetup.Click += new System.EventHandler(this.btnCheckSetup_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = "シリアルキーを入力";
			// 
			// txtSerialKey
			// 
			this.txtSerialKey.Location = new System.Drawing.Point(16, 48);
			this.txtSerialKey.Multiline = true;
			this.txtSerialKey.Name = "txtSerialKey";
			this.txtSerialKey.Size = new System.Drawing.Size(544, 80);
			this.txtSerialKey.TabIndex = 1;
			this.txtSerialKey.Text = "";
			// 
			// txtSetupDate
			// 
			this.txtSetupDate.Location = new System.Drawing.Point(112, 376);
			this.txtSetupDate.Name = "txtSetupDate";
			this.txtSetupDate.ReadOnly = true;
			this.txtSetupDate.Size = new System.Drawing.Size(332, 19);
			this.txtSetupDate.TabIndex = 22;
			this.txtSetupDate.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(20, 376);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(76, 16);
			this.label4.TabIndex = 16;
			this.label4.Text = "セットアップ日";
			// 
			// txtLimitDate
			// 
			this.txtLimitDate.Location = new System.Drawing.Point(112, 408);
			this.txtLimitDate.Name = "txtLimitDate";
			this.txtLimitDate.ReadOnly = true;
			this.txtLimitDate.Size = new System.Drawing.Size(332, 19);
			this.txtLimitDate.TabIndex = 20;
			this.txtLimitDate.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(20, 408);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(76, 16);
			this.label5.TabIndex = 15;
			this.label5.Text = "有効期限日";
			// 
			// txtLimitDays
			// 
			this.txtLimitDays.Location = new System.Drawing.Point(112, 440);
			this.txtLimitDays.Name = "txtLimitDays";
			this.txtLimitDays.ReadOnly = true;
			this.txtLimitDays.Size = new System.Drawing.Size(332, 19);
			this.txtLimitDays.TabIndex = 19;
			this.txtLimitDays.Text = "";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(20, 444);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(84, 16);
			this.label6.TabIndex = 17;
			this.label6.Text = "有効期限日数";
			// 
			// txtMsg
			// 
			this.txtMsg.Location = new System.Drawing.Point(108, 572);
			this.txtMsg.Name = "txtMsg";
			this.txtMsg.ReadOnly = true;
			this.txtMsg.Size = new System.Drawing.Size(336, 19);
			this.txtMsg.TabIndex = 23;
			this.txtMsg.Text = "";
			// 
			// txtUser
			// 
			this.txtUser.Location = new System.Drawing.Point(112, 472);
			this.txtUser.Multiline = true;
			this.txtUser.Name = "txtUser";
			this.txtUser.ReadOnly = true;
			this.txtUser.Size = new System.Drawing.Size(332, 92);
			this.txtUser.TabIndex = 24;
			this.txtUser.Text = "textBox1";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(24, 476);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(76, 16);
			this.label7.TabIndex = 25;
			this.label7.Text = "ユーザー名";
			// 
			// SerialCheckDlg
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(580, 601);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.txtUser);
			this.Controls.Add(this.txtMsg);
			this.Controls.Add(this.txtMakeKeyDate);
			this.Controls.Add(this.txtSerialArray);
			this.Controls.Add(this.txtSerialKey);
			this.Controls.Add(this.txtSetupDate);
			this.Controls.Add(this.txtLimitDate);
			this.Controls.Add(this.txtLimitDays);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnCheckSerialKey);
			this.Controls.Add(this.btnCheckSetup);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label6);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "SerialCheckDlg";
			this.Text = "SerialCheckDlg";
			this.ResumeLayout(false);

		}
		#endregion

		private void btnCheckSetup_Click(object sender, System.EventArgs e)
		{
			if( this.txtSerialKey.Text == "" )
			{
				MessageBox.Show("シリアルキーを入力してください");
				return;
			}
			try
			{
				this.sfact.LoadSetupData(this.txtSerialKey.Text.Replace("\r\n","").Replace("\n","").Replace("\r",""),
					false);
			}
			catch{}
			// シリアルキー配列
			string output = "";
			ArrayList copyar = (ArrayList)this.sfact.SerialArray.Clone();
			SerialKeyFactory.EncodeArray(copyar,this.sfact.SerialYosoLen);
			for( int i = 0; i < this.sfact.SerialArrayCnt && i < this.sfact.SerialArray.Count; i++ )
			{
				output += (string)this.sfact.SerialArray[i] + ":";
				if( (string)this.sfact.SerialArray[i] == (string)copyar[i] )
				{
					output += "OK";
				}
				else
				{
					output += "NG";
				}
				output += "\r\n";
			}

			this.txtSerialArray.Text = output;

			// キー作成日時
			this.txtMakeKeyDate.Text = this.sfact.KeyMakeDate.ToString();

			// 次に、シリアルキーの利用期限日時
			this.txtLimitDays.Text = this.sfact.LimitLength.ToString();

			this.txtLimitDate.Text = "";

			this.txtSetupDate.Text = "";

			this.txtUser.Text = "";

			this.txtMsg.Text = this.sfact.errMsg;
		}

		private void btnCheckSerialKey_Click(object sender, System.EventArgs e)
		{
			if( this.txtSerialKey.Text == "" )
			{
				MessageBox.Show("シリアルキーを入力してください");
				return;
			}
			try
			{
				this.sfact.LoadData(this.txtSerialKey.Text.Replace("\r\n","").Replace("\n","").Replace("\r",""),
					false);
			}
			catch{}
			// シリアルキー配列
			string output = "";
			ArrayList copyar = (ArrayList)this.sfact.SerialArray.Clone();
			SerialKeyFactory.EncodeArray(copyar,this.sfact.SerialYosoLen);
			for( int i = 0; i < this.sfact.SerialArrayCnt && i < this.sfact.SerialArray.Count; i++ )
			{
				output += (string)this.sfact.SerialArray[i] + ":";
				if( (string)this.sfact.SerialArray[i] == (string)copyar[i] )
				{
					output += "OK";
				}
				else
				{
					output += "NG";
				}
				output += "\r\n";
			}

			this.txtSerialArray.Text = output;

			// キー作成日時
			this.txtMakeKeyDate.Text = this.sfact.KeyMakeDate.ToString();

			// 次に、シリアルキーの利用期限日時
			this.txtLimitDays.Text = this.sfact.LimitLength.ToString();

			// 有効期限日付
			this.txtLimitDate.Text = this.sfact.LimitDate.ToString();

			// シリアルキーセットアップ日付
			this.txtSetupDate.Text = this.sfact.SerialSetupDate.ToString();

			// ユーザー名
			this.txtUser.Text = this.sfact.UserName;

			this.txtMsg.Text = this.sfact.errMsg;
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

	}
}
