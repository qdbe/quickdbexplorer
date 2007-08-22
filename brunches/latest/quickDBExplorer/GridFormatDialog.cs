using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Globalization;


namespace quickDBExplorer
{
	/// <summary>
	/// GridFormatDialog �̊T�v�̐����ł��B
	/// </summary>
	public class GridFormatDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button button3;
		/// <summary>
		/// �K�v�ȃf�U�C�i�ϐ��ł��B
		/// </summary>
		private System.ComponentModel.Container components = null;
		/// <summary>
		/// �\���t�H���g�̎w��
		/// </summary>
		protected	Font	gfont;
		/// <summary>
		/// �\���t�H���g�̎w��
		/// </summary>
		public	Font	Gfont
		{
			get { return this.gfont; }
			set { this.gfont = value; }
		}
		/// <summary>
		/// �t�H���g�\���F�̎w��
		/// </summary>
		protected	Color	gcolor;
		/// <summary>
		/// �t�H���g�\���F�̎w��
		/// </summary>
		public	Color	Gcolor
		{
			get { return this.gcolor; }
			set { this.gcolor = value; }
		}
		/// <summary>
		/// ���l�ϊ������̎w��
		/// </summary>
		protected	string	numFormat;
		/// <summary>
		/// ���l�ϊ������̎w��
		/// </summary>
		public	string	NumFormat
		{
			get { return this.numFormat; }
			set { this.numFormat = value; }

		}
		/// <summary>
		/// �����_�����̎w��
		/// </summary>
		protected	string	floatFormat;
		/// <summary>
		/// �����_�����̎w��
		/// </summary>
		public	string	FloatFormat
		{
			get { return this.floatFormat; }
			set { this.floatFormat = value; }
		}
		/// <summary>
		/// ���t�ϊ������̎w��
		/// </summary>
		protected	string	dateFormat;
		/// <summary>
		/// ���t�ϊ������̎w��
		/// </summary>
		public	string	DateFormat
		{
			get { return this.dateFormat; }
			set { this.dateFormat = value; }
		}
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.ComboBox comboBox2;
		private System.Windows.Forms.ComboBox comboBox3;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.FontDialog fontDialog1;

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public GridFormatDialog()
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
				if(components != null)
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(GridFormatDialog));
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.button3 = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.comboBox2 = new System.Windows.Forms.ComboBox();
			this.comboBox3 = new System.Windows.Forms.ComboBox();
			this.fontDialog1 = new System.Windows.Forms.FontDialog();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button1.Location = new System.Drawing.Point(560, 168);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(72, 24);
			this.button1.TabIndex = 10;
			this.button1.Text = "�߂�(&X)";
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(16, 168);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(72, 24);
			this.button2.TabIndex = 9;
			this.button2.Text = "����(&O)";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(24, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(128, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "���������_ ����(&B)";
			this.label2.Click += new System.EventHandler(this.label2_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(24, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(104, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "��������(&S)";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(24, 80);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(88, 16);
			this.label3.TabIndex = 4;
			this.label3.Text = "���t����(&D)";
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(360, 112);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(104, 24);
			this.button3.TabIndex = 8;
			this.button3.Text = "�t�H���g�w��(&F)";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(24, 112);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(144, 16);
			this.label4.TabIndex = 6;
			this.label4.Text = "�O���b�h�\���t�H���g�w��(&G)";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(176, 16);
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(168, 19);
			this.textBox1.TabIndex = 5;
			this.textBox1.Text = "";
			// 
			// comboBox1
			// 
			this.comboBox1.Items.AddRange(new object[] {
														   "D\t\t�J���}�Ȃ�",
														   "###,###,###,###\t�J���}����"});
			this.comboBox1.Location = new System.Drawing.Point(360, 16);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(272, 20);
			this.comboBox1.TabIndex = 1;
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(176, 48);
			this.textBox2.Name = "textBox2";
			this.textBox2.ReadOnly = true;
			this.textBox2.Size = new System.Drawing.Size(168, 19);
			this.textBox2.TabIndex = 7;
			this.textBox2.Text = "";
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(176, 80);
			this.textBox3.Name = "textBox3";
			this.textBox3.ReadOnly = true;
			this.textBox3.Size = new System.Drawing.Size(168, 19);
			this.textBox3.TabIndex = 7;
			this.textBox3.Text = "";
			// 
			// comboBox2
			// 
			this.comboBox2.Items.AddRange(new object[] {
														   "g\t�W��",
														   "n\t�J���}�t��",
														   "F\t�����_",
														   "e\t�w��"});
			this.comboBox2.Location = new System.Drawing.Point(360, 48);
			this.comboBox2.Name = "comboBox2";
			this.comboBox2.Size = new System.Drawing.Size(272, 20);
			this.comboBox2.TabIndex = 3;
			this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
			// 
			// comboBox3
			// 
			this.comboBox3.Items.AddRange(new object[] {
														   "yyyy/MM/dd\t\t2006/01/01",
														   "yyyy/M/d\t\t\t2006/1/1",
														   "yyyy�NMM��dd��\t\t2006�N01��01��",
														   "yyyy�NM��d��\t\t2006�N1��1��",
														   "yyyy/MM/dd HH:mm:ss\t2006/01/01 01:23:05",
														   "yyyy/M/d HH:mm:ss\t\t2006/1/1 01:23:05",
														   "yyyy�NMM��dd�� HH:mm:ss\t2006�N01��01�� 01:23:05",
														   "yyyy�NM��d�� HH:mm:ss\t2006�N1��1�� 01:23:05"});
			this.comboBox3.Location = new System.Drawing.Point(360, 80);
			this.comboBox3.Name = "comboBox3";
			this.comboBox3.Size = new System.Drawing.Size(272, 20);
			this.comboBox3.TabIndex = 5;
			this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
			// 
			// richTextBox1
			// 
			this.richTextBox1.Location = new System.Drawing.Point(176, 112);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.ReadOnly = true;
			this.richTextBox1.Size = new System.Drawing.Size(168, 40);
			this.richTextBox1.TabIndex = 7;
			this.richTextBox1.Text = "����ABC";
			// 
			// GridFormatDialog
			// 
			this.AcceptButton = this.button2;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.CancelButton = this.button1;
			this.ClientSize = new System.Drawing.Size(648, 205);
			this.Controls.Add(this.richTextBox1);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.comboBox2);
			this.Controls.Add(this.comboBox3);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "GridFormatDialog";
			this.ShowInTaskbar = false;
			this.Text = "�f�[�^�\�������w��";
			this.Load += new System.EventHandler(this.GridFormatDialog_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void label2_Click(object sender, System.EventArgs e)
		{
		
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			this.fontDialog1.Font = gfont;
			this.fontDialog1.Color = gcolor;
			this.fontDialog1.ShowColor = false;
			
			if( this.fontDialog1.ShowDialog() == DialogResult.OK )
			{
				this.gfont = this.fontDialog1.Font;
				this.gcolor = Color.FromArgb(this.fontDialog1.Color.ToArgb());
				this.richTextBox1.Text = this.gfont.Name;
				this.richTextBox1.ForeColor = this.gcolor;
				this.richTextBox1.Font = this.gfont;
			}
		}

		private void GridFormatDialog_Load(object sender, System.EventArgs e)
		{
			this.richTextBox1.Text = this.gfont.Name;
			this.richTextBox1.ForeColor = this.gcolor;
			this.richTextBox1.Font = this.gfont;
			if( this.NumFormat != null )
			{
				this.comboBox1.SelectedItem = this.NumFormat;
			}
			else
			{
				this.comboBox1.SelectedIndex = 0;
			}
			if( this.FloatFormat != null )
			{
				this.comboBox2.SelectedItem = this.FloatFormat;
			}
			else
			{
				this.comboBox2.SelectedIndex = 0;
			}
			if( this.DateFormat != null )
			{
				this.comboBox3.SelectedItem = this.DateFormat;
			}
			else
			{
				this.comboBox3.SelectedIndex = 0;
			}
		}

		private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			int i = 1234;
			this.textBox1.Text = i.ToString(getFormat(this.comboBox1.SelectedItem.ToString()));
		}

		private void comboBox2_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			double dd = 1234.23;
			this.textBox2.Text = dd.ToString(getFormat(this.comboBox2.SelectedItem.ToString()));
		}

		private void comboBox3_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.textBox3.Text = DateTime.Now.ToString(getFormat(this.comboBox3.SelectedItem.ToString()));
		}

		/// <summary>
		/// �t�H�[�}�b�g��������擾����
		/// </summary>
		/// <param name="fstr">��̕\������</param>
		/// <returns>�\������������</returns>
		protected string getFormat(string fstr)
		{
			// \t\t ������΁A�v���O�����̏����I���}����̑I��
			int termp = fstr.IndexOf("	");
			if( termp == -1 )
			{
				// \t\t ���Ȃ��ꍇ�A���[�U�[�����͂���������
				return fstr;
			}
			return fstr.Substring(0,termp);
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			this.NumFormat = (string)this.comboBox1.SelectedItem;
			this.FloatFormat = (string)this.comboBox2.SelectedItem;
			this.DateFormat = (string)this.comboBox3.SelectedItem;
			this.DialogResult = DialogResult.OK;
		}

	}
}
