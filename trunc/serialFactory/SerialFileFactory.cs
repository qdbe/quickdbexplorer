using System;
using System.IO;
using System.Text;
using System.Collections;

namespace serialFactory
{
	/// <summary>
	/// �A�v���P�[�V�������� �V���A���L�[�ƃt�@�C���Ɋւ��鑀���API 
	/// �V���A���L�[���Ȃ��ꍇ�̐V�K�쐬������ SerialFactory �𒼐ڑ��삷�邱��
	/// </summary>
	public class SerialFileFactory
	{

		SerialFactory	serialData = new SerialFactory();
		private int				serialArrayCnt;		// �V���A���L�[�̍��ڐ�
		private int				serialYosoLen;		// �V���A���L�[�̊e�v�f�̕�����
		public string			errMsg = "";

		public SerialFileFactory()
		{
			// 
			// TODO: �R���X�g���N�^ ���W�b�N�������ɒǉ����Ă��������B
			//
		}

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
				this.SerialData.SerialArrayCnt = cnt;
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
				this.SerialData.SerialYosoLen = len;
			}
		}

		public SerialFactory SerialData
		{
			get { return this.serialData; }
		}

		// �V���A���L�[�t�@�C���̑��݊m�F

		public bool	IsExistsSerialFile( string filename )
		{
			if( !File.Exists( filename ) )
			{
				return false;
			}
			return true;
		}

		public bool IsTestingPeriod
		{
			get 
			{
				return this.SerialData.IsTestingPeriod;
			}
		}

		/// <summary>
		/// �V���A���L�[�t�@�C���̓ǂݍ��ݏ���
		/// </summary>
		/// <param name="filename"></param>
		/// <returns></returns>
		public bool ReadSerialFile( string filename )
		{
			try
			{
				this.errMsg = null;

				if( !File.Exists( filename ) )
				{
					return false;
				}

				string serialfile;

				StreamReader sr = new StreamReader(filename);
				serialfile = sr.ReadToEnd();
				sr.Close();

				bool ret = this.SerialData.LoadData(serialfile);
				this.errMsg = StrEncoder.Encode(this.SerialData.errMsg);

				return ret;
			}
			catch( Exception ex)
			{
				this.errMsg = StrEncoder.Encode(ex.ToString());
				return false;
			}
		}

		/// <summary>
		/// �Z�b�g�A�b�v�L�[�̓o�^����
		/// </summary>
		/// <param name="serial"></param>
		/// <returns></returns>
		public bool SetupSerial(string filename, string serial, string username)
		{
			try
			{
				if( this.serialData.LoadSetupData(serial) == false )
				{
					this.errMsg = StrEncoder.Encode(this.SerialData.errMsg);
					return false;
				}
				// ���[�U�[���̐ݒ�̏ꏊ�͂����łȂ��Ƃ����Ȃ�
				this.SerialData.UserName = username;

				StreamWriter sr = new StreamWriter(filename);
				sr.Write(this.SerialData.SerializeData());
				sr.Close();

				return true;
			}
			catch( Exception ex)
			{
				this.errMsg = StrEncoder.Encode(ex.ToString());
				return false;
			}
		}
	}

}

