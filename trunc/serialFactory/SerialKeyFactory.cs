using System;
using System.Text;
using System.Collections;
using System.Globalization;
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
		/// シリアルキーを
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
		/// 新規にシリアルキーを作成する
		/// </summary>
		/// <param name="len">一つあたりのキーの長さ</param>
		/// <param name="arCount">全体の配列数</param>
		/// <returns>作成されたシリアルキー</returns>
		public static ArrayList CreateKeys( int len, int arCount )
		{
			// シリアルキーの情報を指定された数分作成する

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
				// 先頭2桁をまずは決定する
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

				// キー部分を決定する
				string keystr = randint.ToString("X2");
				// キーの残りの部分をセットする
				for( int j = 0; j < (len - 2); j++ )
				{
					randint = rdm.Next(1,256);
					keystr += randint.ToString("X2").Substring(1,1);
				}
				retar[finishCnt] = keystr;
				retar[yoso] = keystr;	// ここではダミーでいれておく
				finishCnt += 2;
			}

			// 対応するチェックサム部分をセットする

			EncodeArray(retar,len);

			return retar;
		}
	}
}
