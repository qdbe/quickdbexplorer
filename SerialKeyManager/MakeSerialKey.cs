using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Text;
using serialFactory;

namespace SerialKeyManager
{
	/// <summary>
	/// MakeSerialKey の概要の説明です。
	/// </summary>
	public class MakeSerialKey : System.Windows.Forms.Form
	{
		SerialManager smanager = new SerialManager();
		SerialFactory  sfact = new SerialFactory();
		SerialFactory tmpsf = new SerialFactory();
		string		  logFileName  = "";
							
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnMake;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.TextBox txtUser;
		private System.Windows.Forms.TextBox txtDay;
		private System.Windows.Forms.TextBox txtKey;
		private System.Windows.Forms.Button btnClipBoard;
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MakeSerialKey()
		{
			//
			// Windows フォーム デザイナ サポートに必要です。
			//
			InitializeComponent();

			//
			// TODO: InitializeComponent 呼び出しの後に、コンストラクタ コードを追加してください。
			//
			this.logFileName = Application.ExecutablePath + ".log";
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
			this.txtUser = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtDay = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.txtKey = new System.Windows.Forms.TextBox();
			this.btnMake = new System.Windows.Forms.Button();
			this.btnClipBoard = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtUser
			// 
			this.txtUser.Location = new System.Drawing.Point(168, 24);
			this.txtUser.Name = "txtUser";
			this.txtUser.Size = new System.Drawing.Size(384, 19);
			this.txtUser.TabIndex = 1;
			this.txtUser.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(24, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(136, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "対象ユーザー名(任意)(&U)";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(32, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(88, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "有効日数(&D)";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtDay
			// 
			this.txtDay.Location = new System.Drawing.Point(176, 48);
			this.txtDay.Name = "txtDay";
			this.txtDay.Size = new System.Drawing.Size(56, 19);
			this.txtDay.TabIndex = 3;
			this.txtDay.Text = "30";
			// 
			// label3
			// 
			this.label3.ForeColor = System.Drawing.Color.Red;
			this.label3.Location = new System.Drawing.Point(144, 48);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(24, 16);
			this.label3.TabIndex = 3;
			this.label3.Text = "＊";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(288, 48);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(56, 24);
			this.label4.TabIndex = 4;
			this.label4.Text = "日";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtKey
			// 
			this.txtKey.Location = new System.Drawing.Point(24, 128);
			this.txtKey.Name = "txtKey";
			this.txtKey.Size = new System.Drawing.Size(560, 19);
			this.txtKey.TabIndex = 5;
			this.txtKey.Text = "";
			// 
			// btnMake
			// 
			this.btnMake.Location = new System.Drawing.Point(24, 80);
			this.btnMake.Name = "btnMake";
			this.btnMake.Size = new System.Drawing.Size(136, 40);
			this.btnMake.TabIndex = 4;
			this.btnMake.Text = "シリアルキーの作成(&S)";
			this.btnMake.Click += new System.EventHandler(this.btnMake_Click);
			// 
			// btnClipBoard
			// 
			this.btnClipBoard.Location = new System.Drawing.Point(24, 160);
			this.btnClipBoard.Name = "btnClipBoard";
			this.btnClipBoard.Size = new System.Drawing.Size(136, 48);
			this.btnClipBoard.TabIndex = 6;
			this.btnClipBoard.Text = "クリップボードにコピー(&C)";
			this.btnClipBoard.Click += new System.EventHandler(this.btnClipBoard_Click);
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(472, 176);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(112, 32);
			this.btnClose.TabIndex = 7;
			this.btnClose.Text = "閉じる(&X)";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// MakeSerialKey
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(624, 213);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnClipBoard);
			this.Controls.Add(this.btnMake);
			this.Controls.Add(this.txtKey);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtDay);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtUser);
			this.Controls.Add(this.label2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "MakeSerialKey";
			this.Text = "MakeSerialKey";
			this.ResumeLayout(false);

		}
		#endregion

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btnMake_Click(object sender, System.EventArgs e)
		{
			if( this.txtDay.Text == "" )
			{
				this.txtDay.Focus();
				MessageBox.Show("有効日付を入力してください");
				return ;
			}
			int days = 0;
			try
			{
				days = int.Parse(this.txtDay.Text);
			}
			catch
			{
				this.txtDay.Focus();
				MessageBox.Show("正しい数値を入力してください");
				return;
			}
			try
			{
				string skey = this.sfact.MakeSetupKey(days);
				this.txtKey.Text = skey;
				if( this.tmpsf.LoadSetupData(skey) == false )
				{
					MessageBox.Show("異常が発生しました。このシリアルキーを技術担当者に通知してください");
					return;
				}

				StreamWriter wr = new StreamWriter(this.logFileName,true);
				// 書出し日時
				wr.Write(DateTime.Now.ToString());
				wr.Write("\t");
				// 有効日付
				wr.Write(this.txtDay.Text);
				wr.Write("\t");
				// 対象ユーザー
				wr.Write(this.txtUser.Text);
				wr.Write("\t");
				// シリアルキー
				wr.WriteLine(skey);
				wr.Close();

				this.btnClipBoard.Focus();

			}
			catch(Exception exp)
			{
				MessageBox.Show("エラーが発生しました\r\n" + exp.ToString() );
			}
		}

		private void btnClipBoard_Click(object sender, System.EventArgs e)
		{
			if( this.txtKey.Text != "" )
			{
				Clipboard.SetDataObject(this.txtKey.Text,true );
			}
			this.btnMake.Focus();
		}
	}
}
