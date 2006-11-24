using System;
using System.IO;
using System.Text;
using System.Globalization;

namespace serialFactory
{
	/// <summary>
	/// UserNameFactoru の概要の説明です。
	/// </summary>
	public class UserNameFactoru
	{
		public UserNameFactoru()
		{
			// 
			// TODO: コンストラクタ ロジックをここに追加してください。
			//
		}
		public static string Encode(string srcstr)
		{
			Encoding encoder = Encoding.GetEncoding(54936);

			byte[] result = encoder.GetBytes(srcstr);
			string output = "";
			for( int i = 0; i < result.Length; i++ )
			{
				output += result[i].ToString("X2");
			}
			return output;
		}

		public static string Decode(string srcstr)
		{
			Encoding encoder = Encoding.GetEncoding(54936);

			byte[] ret = new byte[srcstr.Length/2];
			for( int j = 0; j < ret.Length; j++ )
			{
				ret[j] = byte.Parse(srcstr.Substring(j*2,2),NumberStyles.HexNumber);
			}

			string retstr = new string(encoder.GetChars(ret));
			return retstr;
		}

	}
}
