using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Globalization;

namespace SerialKeyManager
{
	/// <summary>
	/// SerialKeyManager の概要の説明です。
	/// </summary>
	public class SerialKeyManager : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.TextBox textBox4;
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SerialKeyManager()
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
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(88, 40);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(128, 19);
			this.textBox1.TabIndex = 0;
			this.textBox1.Text = "textBox1";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(176, 88);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(64, 32);
			this.button1.TabIndex = 1;
			this.button1.Text = "button1";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(40, 152);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(176, 19);
			this.textBox2.TabIndex = 2;
			this.textBox2.Text = "textBox2";
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(24, 192);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(192, 19);
			this.textBox3.TabIndex = 3;
			this.textBox3.Text = "textBox3";
			// 
			// textBox4
			// 
			this.textBox4.Location = new System.Drawing.Point(64, 88);
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(96, 19);
			this.textBox4.TabIndex = 4;
			this.textBox4.Text = "54936";
			// 
			// SerialKeyManager
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.textBox4);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.button1);
			this.Name = "SerialKeyManager";
			this.Text = "SerialKeyManager";
			this.ResumeLayout(false);

		}
		#endregion

		[STAThread]
		static void Main() 
		{
			Application.Run(new SerialKeyManager());
		}

		private void button1_Click(object sender, System.EventArgs e)
		{

			Encoding encoder = Encoding.GetEncoding(int.Parse(this.textBox4.Text));

			byte[] result = encoder.GetBytes(this.textBox1.Text);
			string output = "";
			for( int i = 0; i < result.Length; i++ )
			{
				output += result[i].ToString("X2");
			}
			this.textBox2.Text = output;

			byte[] ret = new byte[output.Length/2];
			for( int j = 0; j < ret.Length; j++ )
			{
				ret[j] = byte.Parse(output.Substring(j*2,2),NumberStyles.HexNumber);
			}

			string retstr = new string(encoder.GetChars(ret));
			this.textBox3.Text = retstr;
		
		}
	}
}
