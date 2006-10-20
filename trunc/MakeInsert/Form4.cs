using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace MakeInsert
{
	/// <summary>
	/// Form4 �̊T�v�̐����ł��B
	/// </summary>
	public class Form4 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;

		public string	SelectSql = "";
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		/// <summary>
		/// �K�v�ȃf�U�C�i�ϐ��ł��B
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form4()
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
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.AcceptsReturn = true;
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.ContextMenu = this.contextMenu1;
			this.textBox1.ImeMode = System.Windows.Forms.ImeMode.Off;
			this.textBox1.Location = new System.Drawing.Point(16, 24);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBox1.Size = new System.Drawing.Size(444, 208);
			this.textBox1.TabIndex = 0;
			this.textBox1.Text = "";
			this.textBox1.WordWrap = false;
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem1,
																						 this.menuItem2,
																						 this.menuItem3,
																						 this.menuItem4});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
			this.menuItem1.Text = "�R�s�[";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.Shortcut = System.Windows.Forms.Shortcut.CtrlX;
			this.menuItem2.Text = "�؂���";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 2;
			this.menuItem3.Shortcut = System.Windows.Forms.Shortcut.CtrlV;
			this.menuItem3.Text = "�\��t��";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 3;
			this.menuItem4.Shortcut = System.Windows.Forms.Shortcut.CtrlA;
			this.menuItem4.Text = "�S�đI��";
			this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.button1.Location = new System.Drawing.Point(16, 240);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(96, 24);
			this.button1.TabIndex = 1;
			this.button1.Text = "SQL���s(&W)";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.Location = new System.Drawing.Point(348, 240);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(88, 24);
			this.button2.TabIndex = 2;
			this.button2.Text = "�L�����Z��(&X)";
			// 
			// Form4
			// 
			this.AcceptButton = this.button1;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.CancelButton = this.button2;
			this.ClientSize = new System.Drawing.Size(480, 273);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.textBox1);
			this.Name = "Form4";
			this.ShowInTaskbar = false;
			this.Text = "SQL���w��";
			this.Load += new System.EventHandler(this.Form4_Load);
			this.ResumeLayout(false);

		}
		#endregion


		private void button1_Click(object sender, System.EventArgs e)
		{
			SelectSql = this.textBox1.Text;
			//this.DialogResult = DialogResult.OK;
		}

		private void Form4_Load(object sender, System.EventArgs e)
		{
			this.textBox1.Text = SelectSql;
			this.textBox1.Focus();
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			this.textBox1.Copy();
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			this.textBox1.Cut();
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			this.textBox1.Paste();
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			this.textBox1.SelectAll();
		}

	}
}
