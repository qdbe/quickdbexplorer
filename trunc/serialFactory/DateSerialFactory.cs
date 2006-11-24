using System;
using System.Collections;

namespace serialFactory
{
	/// <summary>
	/// DateSerialFactory の概要の説明です。
	/// </summary>
	public class DateSerialFactory
	{
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

		public static string Encode(DateTime tm)
		{
			// 日付を変換する
			string hexstr = tm.Ticks.ToString();
			ArrayList ar = new ArrayList();
			ar.Add(hexstr.Substring(0,6));
			ar.Add(hexstr.Substring(6,4));
			ar.Add(hexstr.Substring(10,4));
			ar.Add(hexstr.Substring(14,4));

			string prestr = (string)ar[2] + (string)ar[1] + (string)ar[3] + (string)ar[0];
			int checksum = 0;
			for( int j = 0; j < prestr.Length; j++ )
			{
				checksum += int.Parse(prestr[j].ToString());
			}
			checksum = checksum%16;
			prestr += checksum.ToString("X2").ToUpper();
			return prestr;
		}

		public static DateTime Decode(string keystr)
		{
			string basestr = keystr;
			basestr = basestr.Substring(0,basestr.Length-2);
			ArrayList dear = new ArrayList();
			dear.Add(basestr.Substring(0,4));
			dear.Add(basestr.Substring(4,4));
			dear.Add(basestr.Substring(8,4));
			dear.Add(basestr.Substring(12,6));
			string longstr = (string)dear[3]+(string)dear[1]+(string)dear[0]+(string)dear[2];
			long timelong = long.Parse(longstr);
			return new DateTime(timelong);
		}
	}
}
