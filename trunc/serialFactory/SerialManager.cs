using System;
using System.Windows.Forms;

namespace serialFactory
{
	/// <summary>
	/// �A�v���P�[�V���������̃V���A���L�[�����̃C���^�[�t�F�[�X
	/// </summary>
	public class SerialManager
	{
		public SerialManager()
		{
			// 
			// TODO: �R���X�g���N�^ ���W�b�N�������ɒǉ����Ă��������B
			//
		}

		private int				serialArrayCnt;		// �V���A���L�[�̍��ڐ�
		private int				serialYosoLen;		// �V���A���L�[�̊e�v�f�̕�����
		private string			serialFileName = "";	// �V���A���L�[��o�^����t�@�C����
		SerialFileFactory		serialFilef = new SerialFileFactory();
		public string			errMsg;

		public int SerialArrayCnt
		{
			get { return this.serialArrayCnt; }
			set 
			{
				int cnt = value;
				if( cnt % 2 != 0 || cnt < 2 )
				{
					throw new Exception("SerialArrayCnt ��2�ȏ�̋����łȂ���΂����܂���");
				}
				this.serialArrayCnt = cnt;
				this.serialFilef.SerialArrayCnt = cnt;
			}
		}

		public int SerialYosoLen
		{
			get { return this.SerialYosoLen; }
			set 
			{
				int len = value;
				if( len < 3 )
				{
					throw new Exception( "SerialYosoLen ��3�ȏ�̐��l�łȂ���΂����܂���");
				}
				this.serialYosoLen = len;
				this.serialFilef.SerialYosoLen = len;
			}
		}

		public string SerialFileName
		{
			get { return this.serialFileName; }
			set { this.serialFileName = value; }
		}

		public string UserName
		{
			get { return this.serialFilef.SerialData.UserName; }
		}

		public string LimitDate
		{
			get { return this.serialFilef.SerialData.LimitDate.ToString("yyyy/M/d"); }
		}

		/// <summary>
		/// �V���A���L�[�����݂��邩�ǂ������`�F�b�N���A���݂��Ȃ��ꍇ�x�����b�Z�[�W&�L�[�̓o�^��ʂ�\������
		/// </summary>
		/// <param name="filename"></param>
		/// <returns></returns>
		public bool LoadAndCheckSerial()
		{
			bool ret = serialFilef.ReadSerialFile(this.serialFileName);
			if( ret == true )
			{
				if( this.serialFilef.IsTestingPeriod == false )
				{
					TimeSpan ts = this.serialFilef.SerialData.LimitDate - DateTime.Now;
					if( ts.Days < 7 )
					{
						MessageBox.Show("���C�Z���X�̗L�������� " + this.serialFilef.SerialData.LimitDate.ToString("yyyy/M/d") + "�ł��B�����i�K�ŐV�������C�Z���X����肷�邱�Ƃ������߂��܂��B" );
					}
				}
				// �V���A���L�[�͐���ɓo�^����Ă���
				return true;
			}
			this.errMsg = serialFilef.errMsg;
			// ��������̓V���A���L�[���o�^����Ă��Ȃ��ꍇ
			SetupSerialDialog dlg = new SetupSerialDialog();
			dlg.smanager = this;
			if( dlg.ShowDialog() == DialogResult.OK )
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// �V���A���L�[��o�^����
		/// </summary>
		/// <param name="serial"></param>
		/// <param name="user"></param>
		/// <returns></returns>
		public bool	SetupSerial(string serial, string user)
		{
			bool ret = serialFilef.SetupSerial(this.serialFileName,serial,user);
			if( ret == false )
			{
				this.errMsg = serialFilef.errMsg;
				return false;
			}
			return true;
		}

		public void	ShowRegisterInfo()
		{
			if( this.serialFilef.IsTestingPeriod == true )
			{
				MessageBox.Show("���ݎ��p���Ԓ��ł�");
			}
			else
			{
				RegisterInfoDialog dlg = new RegisterInfoDialog();
				dlg.smanager = this;
				dlg.ShowDialog();
			}
		}
	
	}
}
