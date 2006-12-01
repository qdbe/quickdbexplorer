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
		SerialFileFactory		sfile = new SerialFileFactory();
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
				this.sfile.SerialArrayCnt = cnt;
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
				this.sfile.SerialYosoLen = len;
			}
		}

		public string SerialFileName
		{
			get { return this.serialFileName; }
			set { this.serialFileName = value; }
		}

		/// <summary>
		/// �V���A���L�[�����݂��邩�ǂ������`�F�b�N���A���݂��Ȃ��ꍇ�x�����b�Z�[�W&�L�[�̓o�^��ʂ�\������
		/// </summary>
		/// <param name="filename"></param>
		/// <returns></returns>
		public bool LoadAndCheckSerial()
		{
			bool ret = sfile.ReadSerialFile(this.serialFileName);
			if( ret == true )
			{
				// �V���A���L�[�͐���ɓo�^����Ă���
				return true;
			}
			this.errMsg = sfile.errMsg;
			// ��������̓V���A���L�[���o�^����Ă��Ȃ��ꍇ
			SetupSerialDialog dlg = new SetupSerialDialog();
			dlg.smanager = this;
			if( dlg.ShowDialog() == DialogResult.OK )
			{
				return true;
			}
			return false;
		}

		public bool	SetupSerial(string serial, string user)
		{
			bool ret = sfile.SetupSerial(this.serialFileName,serial,user);
			if( ret == false )
			{
				this.errMsg = sfile.errMsg;
				return false;
			}
			return true;
		}
	
	}
}
