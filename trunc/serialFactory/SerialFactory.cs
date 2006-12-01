using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Security.Cryptography;


namespace serialFactory
{
	/// <summary>
	/// SerialFactory �̊T�v�̐����ł��B
	/// </summary>
	public class SerialFactory
	{

		private ArrayList serialArray;		// �V���A���L�[
		private int				serialArrayCnt;		// �V���A���L�[�̍��ڐ�
		private int				serialYosoLen;		// �V���A���L�[�̊e�v�f�̕�����
		private DateTime		keyMakeDate;		// �V���A���L�[�쐬����
		private DateTime		serialSetupDate;	// �V���A���L�[ �Z�b�g�A�b�v����
		private DateTime		limitDate;			// ���p��������
		private DateTime		limitLength;		// �Z�b�g�A�b�v��̗L����������(2002/05/21 13:48:00 ����̍���)
		private DateTime		baseDate = new DateTime(2006,5,21,13,48,00);
		string	userName;			// ���[�U�[��
		public  string errMsg = "";

		
		public SerialFactory()
		{
			initDatas();
			this.serialArrayCnt = 6;
			this.serialYosoLen = 5;
		}

		/// <summary>
		/// �e��f�[�^�̏��������s��
		/// </summary>
		protected void initDatas()
		{
			this.userName = "";
			this.serialArray = new ArrayList();
			this.serialSetupDate = DateTime.Now;
			this.keyMakeDate = DateTime.Now;
			this.limitDate = DateTime.Now;
			this.LimitLength = new DateTime(2006,5,21,13,48,00).AddDays(30);
		}



		public string UserName
		{
			get { return this.userName; }
			set { this.userName = value; }
		}

		public ArrayList SerialArray
		{
			get { return this.serialArray; }
			set { this.serialArray = value; }
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
			}
		}

		public int SerialYosoLen
		{
			get { return this.serialYosoLen; }
			set 
			{
				int len = value;
				if( len < 3 )
				{
					throw new Exception( "SerialYosoLen ��3�ȏ�̐��l�łȂ���΂����܂���");
				}
				this.serialYosoLen = len;
			}
		}


		public DateTime SerialSetupDate
		{
			get { return this.serialSetupDate; }
			set { this.serialSetupDate = value; }
		}

		public DateTime KeyMakeDate
		{
			get { return this.keyMakeDate; }
			set { this.keyMakeDate = value; }
		}

		public DateTime LimitDate
		{
			get 
			{ 
				return this.limitDate; 
			}
			set { this.limitDate = value; }
		}

		public DateTime LimitLength
		{
			get { return this.limitLength; }
			set { this.limitLength = value; }
		}

		public int	LimitLengthInt
		{
			get 
			{
				TimeSpan ts = this.LimitLength - this.baseDate;
				return ts.Days;
			}
			set
			{
				this.LimitLength = this.baseDate.AddDays(value);
			}
		}

		/// <summary>
		/// ���S�ȃV���A���L�[�f�[�^�������Ă��镶���񂩂�A����ǂݎ��
		/// </summary>
		/// <param name="serialstr">�V���A���L�[�f�[�^����������</param>
		/// <returns></returns>
		public bool	LoadData(string serialstr)
		{
			this.errMsg = "";
			try
			{
				if( this.IsValidSerialStr(serialstr) == false )
				{
					this.initDatas();
					return false;
				}

				// ������̕������s��

				// �`�F�b�N�T������菜��
				string needstr = CheckSum.MinusCheckSum(serialstr);

				// �ŏ��̓V���A���L�[��ArrayList
				string kaiseki = needstr.Substring(0,this.SerialArrayCnt*this.SerialYosoLen);
				this.SerialArray = new ArrayList();
				for( int i = 0; i < this.SerialArrayCnt; i ++ )
				{
					this.SerialArray.Add(kaiseki.Substring(this.SerialYosoLen*i,this.SerialYosoLen));
				}

				// ���ɁA�L�[�̍쐬����
				needstr = needstr.Substring(kaiseki.Length);
				kaiseki = needstr.Substring(0,DateSerialFactory.SerializedLen);
				object ret = DateSerialFactory.Decode(kaiseki);
				if( ret == null )
				{
					this.initDatas();
					return false;
				}
				this.KeyMakeDate = (DateTime)ret;

				// ���ɁA�V���A���L�[�̃Z�b�g�A�b�v����
				needstr = needstr.Substring(kaiseki.Length);
				kaiseki = needstr.Substring(0,DateSerialFactory.SerializedLen);
				ret = DateSerialFactory.Decode(kaiseki);
				if( ret == null )
				{
					this.initDatas();
					return false;
				}
				this.SerialSetupDate = (DateTime)ret;


				// ���ɁA�V���A���L�[�̗��p��������
				needstr = needstr.Substring(kaiseki.Length);
				kaiseki = needstr.Substring(0,DateSerialFactory.SerializedLen);
				ret = DateSerialFactory.Decode(kaiseki);
				if( ret == null )
				{
					this.initDatas();
					return false;
				}
				this.LimitDate = (DateTime)ret;

				// ���ɁA�V���A���L�[�̗��p��������
				needstr = needstr.Substring(kaiseki.Length);
				kaiseki = needstr.Substring(0,DateSerialFactory.SerializedLen);
				ret = DateSerialFactory.Decode(kaiseki);
				if( ret == null )
				{
					this.initDatas();
					return false;
				}
				this.LimitLength = (DateTime)ret;


				// ���ɁA�V���A���L�[�̗��p��������
				needstr = needstr.Substring(kaiseki.Length);
				// �c��͑S�ă��[�U�[���̂͂�
				string ustr = UserNameFactory.Decode(needstr);
				if( ustr == null )
				{
					this.initDatas();
					return false;
				}
				this.UserName = ustr;

				return IsValidSerial();
			}
			catch( Exception ex)
			{
				this.errMsg = ex.ToString();
				this.initDatas();
				return false;
			}
		}

		/// <summary>
		/// ���݂̊e��l�����������̂��ǂ������`�F�b�N����
		/// </summary>
		/// <returns></returns>
		private bool IsValidSerial()
		{
			if( SerialKeyFactory.CheckArray(this.SerialArray,this.SerialYosoLen) == false )
			{
				this.initDatas();
				return false;
			}

			TimeSpan ts = this.SerialSetupDate - this.KeyMakeDate;
			//�@�L�[�̍쐬������ɃZ�b�g�A�b�v����Ă���͂�
			if( ts.Days < 0 )
			{
				this.initDatas();
				return false;
			}

			ts = this.LimitDate  - this.SerialSetupDate;
			//�@�L������ > �L�[�Z�b�g�A�b�v��
			if( ts.Days < 0 )
			{
				this.initDatas();
				return false;
			}
			// �L���������}�C�i�X�͂��肦�Ȃ�
			if( this.LimitLengthInt < 0 )
			{
				this.initDatas();
				return false;
			}

			return true;
		}


		/// <summary>
		/// �o�^�p�V���A���L�[�f�[�^�������Ă��镶���񂩂�A����ǂݎ��
		/// �V���A���L�[�̂������ƃA�b�v���ɗ��p
		/// </summary>
		/// <param name="serialstr">�V���A���L�[�f�[�^����������</param>
		/// <returns></returns>
		public bool	LoadSetupData(string serialstr)
		{
			try
			{
				if( this.IsValidSetupSerialStr(serialstr) == false )
				{
					this.initDatas();
					return false;
				}

				// ������̕������s��

				// �`�F�b�N�T������菜��
				string needstr = CheckSum.MinusCheckSum(serialstr);

				// �ŏ��̓V���A���L�[��ArrayList
				string kaiseki = needstr.Substring(0,this.SerialArrayCnt*this.SerialYosoLen);
				this.SerialArray = new ArrayList();
				for( int i = 0; i < this.SerialArrayCnt; i ++ )
				{
					this.SerialArray.Add(kaiseki.Substring(this.SerialYosoLen*i,this.SerialYosoLen));
				}

				// ���ɁA�L�[�̍쐬����
				needstr = needstr.Substring(kaiseki.Length);
				kaiseki = needstr.Substring(0,DateSerialFactory.SerializedLen);
				object ret = DateSerialFactory.Decode(kaiseki);
				if( ret == null )
				{
					this.initDatas();
					return false;
				}
				this.KeyMakeDate = (DateTime)ret;


				// ���ɁA�V���A���L�[�̗��p��������
				needstr = needstr.Substring(kaiseki.Length);
				kaiseki = needstr.Substring(0,DateSerialFactory.SerializedLen);
				ret = DateSerialFactory.Decode(kaiseki);
				if( ret == null )
				{
					this.initDatas();
					return false;
				}
				this.LimitLength = (DateTime)ret;

				// �V���A���L�[�̃Z�b�g�A�b�v�����͌��݂ɂȂ�
				this.SerialSetupDate = DateTime.Now;

				// �L�������̐ݒ�
				this.LimitDate = this.SerialSetupDate.AddDays(this.LimitLengthInt);

				return IsValidSerial();
			}
			catch( Exception ex)
			{
				this.errMsg = ex.ToString();
				this.initDatas();
				return false;
			}
		}

		/// <summary>
		/// �o�^�p�̃V���A���L�[�̍\�����`�F�b�N����
		/// </summary>
		/// <param name="serialstr"></param>
		/// <returns></returns>
		private bool IsValidSerialStr(string serialstr)
		{
			try
			{
				if( serialstr == null ||
					serialstr == "" ||
					CheckSum.DoCheckSum(serialstr) != true)
				{
					return false;
				}
				// ������̒����̃`�F�b�N
				if( serialstr.Length < 
					( ( this.serialArrayCnt * this.serialYosoLen ) + // �V���A���L�[�̕�����
					DateSerialFactory.SerializedLen * 4 +			// ���t*3 ( �L�[�쐬��, �L�[�o�^���A�L�������A�L������ )
					2												// �`�F�b�N�T�� 
					) )
				{
					return false;
				}
				return true;
			}
			catch( Exception ex)
			{
				this.errMsg = ex.ToString();
				// ��������̃G���[�ɂȂ���
				return false;
			}
		}

		/// <summary>
		/// �Z�b�g�A�b�v���V���A���L�[�̍\�����`�F�b�N����
		/// </summary>
		/// <param name="serialstr"></param>
		/// <returns></returns>
		private bool IsValidSetupSerialStr(string serialstr)
		{
			try
			{
				if( serialstr == null ||
					serialstr == "" ||
					CheckSum.DoCheckSum(serialstr) != true)
				{
					return false;
				}
				// ������̒����̃`�F�b�N
				if( serialstr.Length !=
					( ( this.serialArrayCnt * this.serialYosoLen ) + // �V���A���L�[�̕�����
					DateSerialFactory.SerializedLen * 2 +			// ���t*3 ( �L�[�쐬��, �L������)
					2												// �`�F�b�N�T�� 
					) )
				{
					return false;
				}
				return true;
			}
			catch( Exception ex)
			{
				this.errMsg = ex.ToString();
				// ��������̃G���[�ɂȂ���
				return false;
			}
		}

		/// <summary>
		/// �Z�b�g����Ă���l����A�t�@�C���ɋL�^���ׂ��F�؏��𕶎���Ƃ��č쐬����
		/// </summary>
		/// <returns>�ϊ����ꂽ�V���A���L�[���̕�����</returns>
		public string SerializeData()
		{
			try
			{
				string serialstr = "";

				// �V���A���L�[
				for( int i = 0; i < this.SerialArrayCnt; i++ )
				{
					serialstr += (string)this.SerialArray[i];
				}
				serialstr += DateSerialFactory.Encode(this.KeyMakeDate);
				serialstr += DateSerialFactory.Encode(this.SerialSetupDate);
				serialstr += DateSerialFactory.Encode(this.LimitDate);
				serialstr += DateSerialFactory.Encode(this.LimitLength);
				serialstr += UserNameFactory.Encode(this.UserName);
				serialstr += CheckSum.MakeCheckSum(serialstr);


				if( this.IsValidSerialStr(serialstr) == false )
				{
					return null;
				}

				return serialstr;
			}
			catch( Exception ex)
			{
				this.errMsg = ex.ToString();
				return null;
			}
		}
		
		/// <summary>
		/// �Z�b�g�A�b�v�p�V���A���L�[�f�[�^�̐V�K�쐬
		/// </summary>
		/// <param name="limitDays">�L����������</param>
		/// <returns>�쐬���ꂽ�V���A���L�[  �G���[�ɂȂ����ꍇ��null</returns>
		public string	MakeSetupKey(int limitDays)
		{
			try
			{
				this.KeyMakeDate = DateTime.Now;
				this.SerialArray = SerialKeyFactory.CreateKeys(this.SerialYosoLen,this.SerialArrayCnt);
				this.LimitLengthInt = limitDays;
				string serialstr = "";
				// �V���A���L�[
				for( int i = 0; i < this.SerialArrayCnt; i++ )
				{
					serialstr += (string)this.SerialArray[i];
				}
				// �L�[�쐬����
				serialstr += DateSerialFactory.Encode(this.KeyMakeDate);
				// 
				serialstr += DateSerialFactory.Encode(this.LimitLength);
				serialstr += CheckSum.MakeCheckSum(serialstr);
				if( this.IsValidSetupSerialStr(serialstr) == false )
				{
					return null;
				}
				return serialstr;
			}
			catch( Exception ex)
			{
				this.errMsg = ex.ToString();
				return null;
			}

		}
	}
}