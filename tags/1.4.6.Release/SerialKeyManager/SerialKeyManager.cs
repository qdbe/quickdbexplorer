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
	/// SerialKeyManager �̊T�v�̐����ł��B
	/// </summary>
	public class SerialKeyManager : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnMakeSerial;
		private System.Windows.Forms.Button btnCheckSerial;
		private System.Windows.Forms.Button btnRireki;
		/// <summary>
		/// �K�v�ȃf�U�C�i�ϐ��ł��B
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SerialKeyManager()
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
			this.btnMakeSerial = new System.Windows.Forms.Button();
			this.btnCheckSerial = new System.Windows.Forms.Button();
			this.btnRireki = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnMakeSerial
			// 
			this.btnMakeSerial.Location = new System.Drawing.Point(24, 32);
			this.btnMakeSerial.Name = "btnMakeSerial";
			this.btnMakeSerial.Size = new System.Drawing.Size(216, 48);
			this.btnMakeSerial.TabIndex = 0;
			this.btnMakeSerial.Text = "�V���A���L�[�쐬";
			this.btnMakeSerial.Click += new System.EventHandler(this.btnMakeSerial_Click);
			// 
			// btnCheckSerial
			// 
			this.btnCheckSerial.Location = new System.Drawing.Point(24, 96);
			this.btnCheckSerial.Name = "btnCheckSerial";
			this.btnCheckSerial.Size = new System.Drawing.Size(216, 48);
			this.btnCheckSerial.TabIndex = 1;
			this.btnCheckSerial.Text = "�V���A���L�[�Ó����`�F�b�N";
			this.btnCheckSerial.Click += new System.EventHandler(this.btnCheckSerial_Click);
			// 
			// btnRireki
			// 
			this.btnRireki.Location = new System.Drawing.Point(24, 160);
			this.btnRireki.Name = "btnRireki";
			this.btnRireki.Size = new System.Drawing.Size(216, 48);
			this.btnRireki.TabIndex = 2;
			this.btnRireki.Text = "���s�����Q��";
			this.btnRireki.Click += new System.EventHandler(this.btnRireki_Click);
			// 
			// SerialKeyManager
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(272, 237);
			this.Controls.Add(this.btnRireki);
			this.Controls.Add(this.btnCheckSerial);
			this.Controls.Add(this.btnMakeSerial);
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

		private void btnMakeSerial_Click(object sender, System.EventArgs e)
		{
			MakeSerialKey dlg = new MakeSerialKey();
			dlg.ShowDialog();
		}

		private void btnCheckSerial_Click(object sender, System.EventArgs e)
		{
			SerialCheckDlg dlg = new SerialCheckDlg();
			dlg.ShowDialog();
		}

		private void btnRireki_Click(object sender, System.EventArgs e)
		{
			string logFileName = Application.ExecutablePath + ".log";

			System.Diagnostics.Process.Start( logFileName );
		}
	}
}
