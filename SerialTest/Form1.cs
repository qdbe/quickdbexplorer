using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using serialFactory;
using System.IO;
using System.Security.Cryptography;


namespace SerialTest
{
	/// <summary>
	/// Form1 の概要の説明です。
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		SerialManager smanager = new SerialManager();

		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.TextBox textBox5;
		private System.Windows.Forms.TextBox textBox6;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.TextBox textBox7;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.TextBox textBox8;
		private System.Windows.Forms.TextBox textBox9;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button button7;
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Windows フォーム デザイナ サポートに必要です。
			//
			InitializeComponent();

			//
			// TODO: InitializeComponent 呼び出しの後に、コンストラクタ コードを追加してください。
			//
			this.smanager.SerialFileName = Application.ExecutablePath + ".auth";
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

		#region Windows フォーム デザイナで生成されたコード 
		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.button2 = new System.Windows.Forms.Button();
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.textBox6 = new System.Windows.Forms.TextBox();
			this.button3 = new System.Windows.Forms.Button();
			this.textBox7 = new System.Windows.Forms.TextBox();
			this.button4 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.textBox8 = new System.Windows.Forms.TextBox();
			this.textBox9 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.button6 = new System.Windows.Forms.Button();
			this.button7 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(30, 24);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(150, 19);
			this.textBox1.TabIndex = 0;
			this.textBox1.Text = "20061208";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(36, 62);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(144, 20);
			this.button1.TabIndex = 1;
			this.button1.Text = "日付のSerialチェック";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(32, 102);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(150, 19);
			this.textBox2.TabIndex = 2;
			this.textBox2.Text = "textBox2";
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(28, 142);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(154, 19);
			this.textBox3.TabIndex = 3;
			this.textBox3.Text = "textBox3";
			// 
			// textBox4
			// 
			this.textBox4.Location = new System.Drawing.Point(214, 28);
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(132, 19);
			this.textBox4.TabIndex = 4;
			this.textBox4.Text = "textBox4";
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(224, 64);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(122, 18);
			this.button2.TabIndex = 5;
			this.button2.Text = "ユーザー名のチェック";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// textBox5
			// 
			this.textBox5.Location = new System.Drawing.Point(218, 104);
			this.textBox5.Name = "textBox5";
			this.textBox5.Size = new System.Drawing.Size(130, 19);
			this.textBox5.TabIndex = 6;
			this.textBox5.Text = "textBox5";
			// 
			// textBox6
			// 
			this.textBox6.Location = new System.Drawing.Point(214, 142);
			this.textBox6.Name = "textBox6";
			this.textBox6.Size = new System.Drawing.Size(134, 19);
			this.textBox6.TabIndex = 7;
			this.textBox6.Text = "textBox6";
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(368, 64);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(82, 20);
			this.button3.TabIndex = 8;
			this.button3.Text = "button3";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// textBox7
			// 
			this.textBox7.Location = new System.Drawing.Point(362, 108);
			this.textBox7.Multiline = true;
			this.textBox7.Name = "textBox7";
			this.textBox7.Size = new System.Drawing.Size(118, 146);
			this.textBox7.TabIndex = 9;
			this.textBox7.Text = "textBox7";
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(524, 40);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(106, 30);
			this.button4.TabIndex = 10;
			this.button4.Text = "シリアルキー確認";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// button5
			// 
			this.button5.Location = new System.Drawing.Point(524, 92);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(122, 32);
			this.button5.TabIndex = 11;
			this.button5.Text = "セットアップキー作成";
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// textBox8
			// 
			this.textBox8.Location = new System.Drawing.Point(518, 146);
			this.textBox8.Name = "textBox8";
			this.textBox8.Size = new System.Drawing.Size(76, 19);
			this.textBox8.TabIndex = 12;
			this.textBox8.Text = "30";
			// 
			// textBox9
			// 
			this.textBox9.Location = new System.Drawing.Point(522, 178);
			this.textBox9.Name = "textBox9";
			this.textBox9.Size = new System.Drawing.Size(158, 19);
			this.textBox9.TabIndex = 13;
			this.textBox9.Text = "textBox9";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(600, 148);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(60, 20);
			this.label1.TabIndex = 14;
			this.label1.Text = "日";
			// 
			// button6
			// 
			this.button6.Location = new System.Drawing.Point(520, 216);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(180, 30);
			this.button6.TabIndex = 15;
			this.button6.Text = "ファイル削除";
			this.button6.Click += new System.EventHandler(this.button6_Click);
			// 
			// button7
			// 
			this.button7.Location = new System.Drawing.Point(722, 88);
			this.button7.Name = "button7";
			this.button7.Size = new System.Drawing.Size(128, 23);
			this.button7.TabIndex = 16;
			this.button7.Text = "認証情報表示";
			this.button7.Click += new System.EventHandler(this.button7_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(908, 350);
			this.Controls.Add(this.button7);
			this.Controls.Add(this.button6);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBox9);
			this.Controls.Add(this.textBox8);
			this.Controls.Add(this.textBox7);
			this.Controls.Add(this.textBox6);
			this.Controls.Add(this.textBox5);
			this.Controls.Add(this.textBox4);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			this.textBox2.Text = DateSerialFactory.Encode(this.textBox1.Text);
			this.textBox3.Text = DateSerialFactory.Decode(this.textBox2.Text).ToString();
		
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			this.textBox5.Text = UserNameFactory.Encode(this.textBox4.Text);
			this.textBox6.Text = UserNameFactory.Decode(this.textBox5.Text);
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			Random rdm = new Random(unchecked((int)DateTime.Now.Ticks));
 
			ArrayList retar = new ArrayList();
			int		randint;
			
			for( int	finishCnt = 0; finishCnt < 64; )
			{
				randint = rdm.Next(64);
				randint = randint % 64;
				bool	isFound = false;
				foreach( int seed in retar )
				{
					if( seed == randint )
					{
						isFound = true;
						break;
					}
				}
				if( isFound == true )
				{
					continue;
				}
				retar.Add(randint);
				finishCnt++;
			}
			string outstr = "";
			foreach( int nseed in retar )
			{
				outstr += nseed.ToString();
				outstr += "\r\n";
			}
			this.textBox7.Text = outstr;
		
		}

		private void button4_Click(object sender, System.EventArgs e)
		{
			smanager.LoadAndCheckSerial();
		}

		private void button5_Click(object sender, System.EventArgs e)
		{
			SerialFactory	sf = new SerialFactory();
			this.textBox9.Text = sf.MakeSetupKey(int.Parse(this.textBox8.Text));
			if( sf.LoadSetupData(this.textBox9.Text) != true)
			{
				MessageBox.Show("Error!");
			}
		}

		private void button6_Click(object sender, System.EventArgs e)
		{
			File.Delete(Application.ExecutablePath + ".auth");
		}

		private void button7_Click(object sender, System.EventArgs e)
		{
			smanager.ShowRegisterInfo();
		}
	}
}
