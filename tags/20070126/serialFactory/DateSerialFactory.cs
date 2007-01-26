using System;
using System.Collections;
using System.Globalization;
using System.Security.Cryptography;

namespace serialFactory
{
	/// <summary>
	/// DateSerialFactory の概要の説明です。
	/// </summary>
	public class DateSerialFactory
	{
		// シリアライズキーの最低長
		public const int SerializedLen = 68;	// 文字列長とチェックサム含む
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
			// TODO: コンストラクタ ロジックをここに追加してください。
			//
		}


		public static string Encode(string datestr)
		{
			// 日付を変換する
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
			// 時、分、秒は規定値にあわせる

			Random rdm = new Random(unchecked((int)DateTime.Now.Ticks));

			// 日付を変換する
			DateTime tm = new DateTime(inittm.Year,inittm.Month,inittm.Day,inittm.Year%24,inittm.Month,inittm.Day);
			string hexstr = tm.Ticks.ToString();	// ここが肝
			char [] ar = new char[64];
			for(int i = 0; i < ar.Length; i++ )
			{
				if( bunkai[i] < hexstr.Length )
				{
					// この場合元の文字列からの値を登録する
					ar[i] = hexstr[bunkai[i]];
				}
				else
				{
					// 元の文字列を超えているので、ランダムな値を入れる
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
			return new DateTime(timelong);// ここが肝
		}
	}
}
