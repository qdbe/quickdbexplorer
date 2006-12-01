using System;
using System.Text;
using System.Globalization;
using System.Security.Cryptography;

namespace serialFactory
{
	/// <summary>
	/// CheckSum の概要の説明です。
	/// </summary>
	public class CheckSum
	{


		public const int CheckSumLen = 2;
		public CheckSum()
		{
			// 
			// TODO: コンストラクタ ロジックをここに追加してください。
			//
		}
		public static string MakeCheckSum(string userSerialStr)
		{
			MD5 md5 = new MD5CryptoServiceProvider();
			Encoding encoder = Encoding.GetEncoding(20127);

			byte[] result = md5.ComputeHash(encoder.GetBytes(userSerialStr));
			int		csum = 0;
			for( int i = 0; i < result.Length; i++ )
			{
				csum += (int)result[i];
			}
			csum = csum % 100;
			return csum.ToString("X2");
		}

		public static bool	DoCheckSum(string SerialStr)
		{
			string csum = GetCheckSumStr(SerialStr);
			string csumstr = MakeCheckSum( MinusCheckSum(SerialStr) );
			if( csum != csumstr)
			{
				return false;
			}
			return true;
		}

		public static string MinusCheckSum(string SerialStr)
		{
			return SerialStr.Substring(0,SerialStr.Length-2);
		}

		public static string GetCheckSumStr(string SerialStr)
		{
			return SerialStr.Substring(SerialStr.Length-2,2);
		}
	}
}
