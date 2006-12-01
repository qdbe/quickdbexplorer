using System;
using System.Collections;
using System.Globalization;
using System.Security.Cryptography;

namespace serialFactory
{
	/// <summary>
	/// DateSerialFactory �̊T�v�̐����ł��B
	/// </summary>
	public class DateSerialFactory
	{
		// �V���A���C�Y�L�[�̍Œᒷ
		public const int SerializedLen = 68;	// �����񒷂ƃ`�F�b�N�T���܂�
		static int []bunkai = new int[] {
											38,
											17,
											33,
											23,
											22,
											11,
											41,
											57,
											54,
											45,
											36,
											32,
											34,
											43,
											61,
											60,
											51,
											5,
											14,
											42,
											12,
											62,
											19,
											15,
											50,
											3,
											39,
											29,
											55,
											9,
											58,
											0,
											8,
											7,
											52,
											28,
											24,
											26,
											21,
											59,
											56,
											30,
											4,
											2,
											47,
											37,
											44,
											48,
											31,
											20,
											10,
											27,
											16,
											25,
											35,
											53,
											1,
											63,
											40,
											18,
											13,
											49,
											6,
											46
										};

		public DateSerialFactory()
		{
			// 
			// TODO: �R���X�g���N�^ ���W�b�N�������ɒǉ����Ă��������B
			//
		}


		public static string Encode(string datestr)
		{
			// ���t��ϊ�����
			int yy = 0;
			int mm = 0;
			int dd = 0;

			yy = int.Parse(datestr.Substring(0,4));
			mm = int.Parse(datestr.Substring(4,2));
			dd = int.Parse(datestr.Substring(6,2));
			DateTime tm = new DateTime(yy,mm,dd,yy%24,mm,dd);

			return Encode(tm);
		}

		public static string Encode(DateTime inittm)
		{
			// ���A���A�b�͋K��l�ɂ��킹��

			Random rdm = new Random(unchecked((int)DateTime.Now.Ticks));

			// ���t��ϊ�����
			DateTime tm = new DateTime(inittm.Year,inittm.Month,inittm.Day,inittm.Year%24,inittm.Month,inittm.Day);
			string hexstr = tm.Ticks.ToString();	// ��������
			char [] ar = new char[64];
			for(int i = 0; i < ar.Length; i++ )
			{
				if( bunkai[i] < hexstr.Length )
				{
					// ���̏ꍇ���̕����񂩂�̒l��o�^����
					ar[i] = hexstr[bunkai[i]];
				}
				else
				{
					// ���̕�����𒴂��Ă���̂ŁA�����_���Ȓl������
					int randint = rdm.Next(16);
					ar[i] = randint.ToString()[0];
				}
			}
			string prestr = hexstr.Length.ToString("X2") + new string(ar);
			return prestr + CheckSum.MakeCheckSum(prestr);
		}

		public static object Decode(string keystr)
		{
			if( keystr == null ||
				keystr.Length < SerializedLen ||
				CheckSum.DoCheckSum(keystr) == false )
			{
				return null;
			}

			string basestr = CheckSum.MinusCheckSum(keystr);
			int		needlen = int.Parse(basestr.Substring(0,2),NumberStyles.HexNumber );
			basestr = basestr.Substring(2);

			char [] ar = new char[needlen];

			for( int i = 0; i < 64; i++ )
			{
				if( bunkai[i] < needlen )
				{
					ar[bunkai[i]] = basestr[i];
				}
			}

			string longstr = new string(ar);
			long timelong = long.Parse(longstr);
			return new DateTime(timelong);// ��������
		}
	}
}
