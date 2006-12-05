using System;
using System.Text;
using System.Collections;
using System.Globalization;
using System.Security.Cryptography;


namespace serialFactory
{
	/// <summary>
	/// SerialKeyFactory �̊T�v�̐����ł��B
	/// </summary>
	public class SerialKeyFactory
	{
		public SerialKeyFactory()
		{
			// 
			// TODO: �R���X�g���N�^ ���W�b�N�������ɒǉ����Ă��������B
			//
		}

		public static string Encode(string keystr, int len)
		{
			int prenum = int.Parse(keystr.Substring(0,2),NumberStyles.HexNumber);
			int startpos = prenum % 9;

			return Encode(keystr, startpos, len);
		}

		public static string Encode(string keystr, int startpos, int len)
		{
			MD5 md5 = new MD5CryptoServiceProvider();
			Encoding encoder = Encoding.GetEncoding(54936);

			byte[] result = md5.ComputeHash(encoder.GetBytes(keystr));
			string output = "";
			for( int i = 0; i < result.Length; i++ )
			{
				output += result[i].ToString("X");
			}
			return output.ToUpper().Substring(startpos,len);
		}

		/// <summary>
		/// �V���A���L�[��
		/// </summary>
		/// <param name="strar"></param>
		/// <param name="len"></param>
		/// <returns></returns>
		public static ArrayList EncodeArray(ArrayList strar,int len)
		{
			string srcstr = "";
			int		prenum = 0;
			int		startpos = 0;
			int		yoso = 0;

			for( int i = 0; i < strar.Count; i += 2 )
			{
				srcstr = (string)strar[i];
				prenum = int.Parse(srcstr.Substring(0,2),NumberStyles.HexNumber);
				startpos = prenum % 9;
				yoso = prenum % strar.Count;
				strar[yoso] = SerialKeyFactory.Encode(srcstr,startpos,len);
			}

			return strar;
		}

		public static bool CheckArray(ArrayList strar, int len)
		{
			ArrayList copyar = (ArrayList)strar.Clone();
			SerialKeyFactory.EncodeArray(copyar,len);

			for( int i = 0; i < copyar.Count; i++ )
			{
				if( (string)copyar[i] != (string)strar[i] )
				{
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// �V�K�ɃV���A���L�[���쐬����
		/// </summary>
		/// <param name="len">�������̃L�[�̒���</param>
		/// <param name="arCount">�S�̂̔z��</param>
		/// <returns>�쐬���ꂽ�V���A���L�[</returns>
		public static ArrayList CreateKeys( int len, int arCount )
		{
			// �V���A���L�[�̏����w�肳�ꂽ�����쐬����

			if( arCount % 2 == 1 ||
				arCount < 2 )
			{
				return null;
			}
			if( len < 3 )
			{
				return null;
			}

			Random rdm = new Random(unchecked((int)DateTime.Now.Ticks));
 
			ArrayList retar = new ArrayList(arCount);
			for(int k = 0; k < arCount; k ++ )
			{
				retar.Add(null);
			}
			int		randint;
			
			for( int	finishCnt = 0; finishCnt < arCount; )
			{
				// �擪2�����܂��͌��肷��
				randint = rdm.Next(1,100);
				int yoso = randint % arCount;
				if( yoso % 2 == 0 )
				{
					continue;
				}
				if( retar.Count != 0 && retar[yoso] != null )
				{
					continue;
				}

				// �L�[���������肷��
				string keystr = randint.ToString("X2");
				// �L�[�̎c��̕������Z�b�g����
				for( int j = 0; j < (len - 2); j++ )
				{
					randint = rdm.Next(1,256);
					keystr += randint.ToString("X2").Substring(1,1);
				}
				retar[finishCnt] = keystr;
				retar[yoso] = keystr;	// �����ł̓_�~�[�ł���Ă���
				finishCnt += 2;
			}

			// �Ή�����`�F�b�N�T���������Z�b�g����

			EncodeArray(retar,len);

			return retar;
		}
	}
}
