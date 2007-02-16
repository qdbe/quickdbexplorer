using System;
using System.Text;
using System.Globalization;
using System.Security.Cryptography;

namespace serialFactory
{
	/// <summary>
	/// 文字列を暗号化/複合化する
	/// </summary>
	public class StrEncoder
	{
		public StrEncoder()
		{
			// 
			// TODO: コンストラクタ ロジックをここに追加してください。
			//
		}

		/// <summary>
		/// 文字列を暗号化する
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static string Encode(string str)
		{
			if( str == null ||
				str.Length < 1 )
			{
				return null;
			}

			Encoding encoder = Encoding.GetEncoding(65001);

			byte[] result = encoder.GetBytes(str);
			string output = "";
			for( int i = 0; i < result.Length; i++ )
			{
				output += result[i].ToString("X2");
			}
			return output.ToUpper() + CheckSum.MakeCheckSum(output.ToUpper());
		}

		/// <summary>
		/// 文字列を複合化する
		/// </summary>
		/// <param name="encodedstr"></param>
		/// <returns></returns>
		public static string Decode(string encodedstr)
		{
			if( encodedstr == null ||
				encodedstr.Length < ( CheckSum.CheckSumLen + 1 ) ||
				CheckSum.DoCheckSum(encodedstr) == false
				)
			{
				return null;
			}

			byte[] codebyte = new byte[encodedstr.Length];

			string nochecksum = CheckSum.MinusCheckSum(encodedstr);

			for( int i = 0; i < nochecksum.Length; i += 2 )
			{
				codebyte[i/2] = byte.Parse(nochecksum.Substring(i,2),NumberStyles.HexNumber );
			}
			Encoding encoder = Encoding.GetEncoding(65001);

			return encoder.GetString(codebyte);
		}
	}
}
