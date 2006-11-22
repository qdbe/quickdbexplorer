using System;
using System.Text;
using System.Random;
using System.Collections;
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

		public string Encode(string keystr, int startpos, int len)
		{
			MD5 md5 = new MD5CryptoServiceProvider();
			Encoding encoder = Encoding.GetEncoding(54936);

			byte[] result = md5.ComputeHash(encoder.GetBytes(keystr));
			string output = "";
			for( int i = 0; i < result.Length; i++ )
			{
				output += result[i].ToString("x");
			}
			return output.ToUpper().Substring(startpos,len);
		}

		public ArrayList EncodeArray(ArrayList strar,int len)
		{
			string srcstr = "";
			int		prenum = 0;
			int		startpos = 0;
			int		yoso = 0;

			for( int i = 0; i < strar.Count; i += 2 )
			{
				srcstr = strar[i];
				prenum = int.Parse(srcstr.Substring(0,2));
				startpos = prenum % 9;
				yoso = ( prenum % ( strar.Count / 2 ) ) * 2;
				strar[yoso] = this.Encode(srcstr,startpos,len);
			}

			return strar;
		}

		public bool CheckArray(ArrayList strar, int len)
		{
			ArrayList copyar = strar.Clone();
			this.EncodeArray(copyar,len);

			for( int i = 0; i < copyar.Count; i++ )
			{
				if( copyar[i] != strar[i] )
				{
					return false;
				}
			}

			return true;
		}

		public ArrayList( int len, int arCount )
		{
			// �V���A���L�[�̏����w�肳�ꂽ�����쐬����
			
		}
	}
}
