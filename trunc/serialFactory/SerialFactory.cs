using System;
using System.IO;
using System.Text;
using System.Collections;

namespace serialFactory
{
	/// <summary>
	/// SerialFactory の概要の説明です。
	/// </summary>
	public class SerialFactory
	{

		
		public SerialFactory()
		{
			// 
			// TODO: コンストラクタ ロジックをここに追加してください。
			//
		}

		// シリアルキーファイルの存在確認

		public bool	IsExistsSerialFile( string filename )
		{
			if( !File.Exists( filename ) )
			{
				return false;
			}
			return true;
		}

		// シリアルキーファイルの読み込み処理
		public string ReadSerialFile( string filename )
		{
			if( !File.Exists( filename ) )
			{
				return null;
			}

			string serialfile;

			StreamReader sr = new StreamReader(filename);
			serialfile = sr.ReadLine();
			sr.Close();

			return serialfile;
		}

		// シリアルキーがない場合の新規作成処理

		// シリアルキーファイルのチェック処理
	}

	internal class SerialData
	{
		string			UserName;			// ユーザー名
		private ArrayList serialArray;		// シリアルキー
		DateTime		SerialSetupDate;	// シリアルキー セットアップ日時
		DateTime		KeyMakeDate;		// シリアルキー作成日時
		DateTime		LimitDate;			// 利用期限日時
		DateTime		LimitLength;		// セットアップ後しかない

		// 文字列からのデータ取込
		public bool	loaddata(string serialstr)
		{
			return false;

		}
	}
}
