using System;
using System.Text;
using System.Security.Cryptography;


namespace serialFactory
{
	/// <summary>
	/// SerialKeyFactory の概要の説明です。
	/// </summary>
	public class SerialKeyFactory
	{
		public SerialKeyFactory()
		{
			// 
			// TODO: コンストラクタ ロジックをここに追加してください。
			//
		}

		public string Encode(string keystr)
		{
			MD5 md5 = new MD5CryptoServiceProvider();
			Encoding encoder = Encoding.GetEncoding(54936);

			byte[] result = md5.ComputeHash(encoder.GetBytes(keystr));
			string output = "";
			for( int i = 0; i < result.Length; i++ )
			{
				output += result[i].ToString("x");
			}
			return output.ToUpper();
		}
	}
}
