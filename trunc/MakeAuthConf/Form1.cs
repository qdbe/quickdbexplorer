using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using serialFactory;
using System.Security.Cryptography;


namespace MakeAuthConf
{
	/// <summary>
	/// Form1 �̊T�v�̐����ł��B
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button button4;
		/// <summary>
		/// �K�v�ȃf�U�C�i�ϐ��ł��B
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Windows �t�H�[�� �f�U�C�i �T�|�[�g�ɕK�v�ł��B
			//
			InitializeComponent();

			//
			// TODO: InitializeComponent �Ăяo���̌�ɁA�R���X�g���N�^ �R�[�h��ǉ����Ă��������B
			//
		}

		/// <summary>
		/// �g�p����Ă��郊�\�[�X�Ɍ㏈�������s���܂��B
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

		#region Windows �t�H�[�� �f�U�C�i�Ő������ꂽ�R�[�h 
		/// <summary>
		/// �f�U�C�i �T�|�[�g�ɕK�v�ȃ��\�b�h�ł��B���̃��\�b�h�̓��e��
		/// �R�[�h �G�f�B�^�ŕύX���Ȃ��ł��������B
		/// </summary>
		private void InitializeComponent()
		{
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.button4 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(48, 24);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(184, 24);
			this.button1.TabIndex = 0;
			this.button1.Text = "�F�ؕs�v";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(48, 64);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(184, 24);
			this.button2.TabIndex = 1;
			this.button2.Text = "�F�ؕK�v";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(48, 104);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(184, 24);
			this.button3.TabIndex = 2;
			this.button3.Text = "�L������";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(48, 152);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(184, 19);
			this.textBox1.TabIndex = 3;
			this.textBox1.Text = "";
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(176, 200);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(64, 32);
			this.button4.TabIndex = 4;
			this.button4.Text = "�N���b�v�{�[�h";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// �A�v���P�[�V�����̃��C�� �G���g�� �|�C���g�ł��B
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void button4_Click(object sender, System.EventArgs e)
		{
			Clipboard.SetDataObject(this.textBox1.Text,true );
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			// �F�ؕK�v
			// ������̓����_���l�ɂ��Ă���
			// 
			Random rdm = new Random(unchecked((int)DateTime.Now.Ticks));
			
			this.textBox1.Text = StrEncoder.Encode("Need"+rdm.ToString()+"Licence");
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			// �F�ؕs�v
			this.textBox1.Text = StrEncoder.Encode("NoLicense");
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			// �L������
			this.textBox1.Text = StrEncoder.Encode("LimitDate");
		}
	}
}
