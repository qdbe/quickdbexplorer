using System;
using System.IO;
using System.Text;
using System.Collections;

namespace serialFactory
{
	/// <summary>
	/// アプリケーション向け シリアルキーとファイルに関する操作のAPI 
	/// シリアルキーがない場合の新規作成処理は SerialFactory を直接操作すること
	/// </summary>
	public class SerialFileFactory
	{

		SerialFactory	serialData = new SerialFactory();
		private int				serialArrayCnt;		// シリアルキーの項目数
		private int				serialYosoLen;		// シリアルキーの各要素の文字数
		public string			errMsg = "";

		public SerialFileFactory()
		{
			// 
			// TODO: コンストラクタ ロジックをここに追加してください。
			//
		}

		public int SerialArrayCnt
		{
			get { return this.serialArrayCnt; }
			set 
			{
				int cnt = value;
				if( cnt % 2 != 0 || cnt < 2 )
				{
					throw new Exception("SerialArrayCnt は2以上の偶数でなければいけません");
				}
				this.serialArrayCnt = cnt;
				this.SerialData.SerialArrayCnt = cnt;
			}
		}

		public int SerialYosoLen
		{
			get { return this.SerialYosoLen; }
			set 
			{
				int len = value;
				if( len < 3 )
				{
					throw new Exception( "SerialYosoLen は3以上の数値でなければいけません");
				}
				this.serialYosoLen = len;
				this.SerialData.SerialYosoLen = len;
			}
		}

		public SerialFactory SerialData
		{
			get { return this.serialData; }
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

		public bool IsTestingPeriod
		{
			get 
			{
				return this.SerialData.IsTestingPeriod;
			}
		}

		/// <summary>
		/// シリアルキーファイルの読み込み処理
		/// </summary>
		/// <param name="filename"></param>
		/// <returns></returns>
		public bool ReadSerialFile( string filename )
		{
			try
			{
				this.errMsg = null;

				if( !File.Exists( filename ) )
				{
					return false;
				}

				string serialfile;

				StreamReader sr = new StreamReader(filename);
				serialfile = sr.ReadToEnd();
				sr.Close();

				bool ret = this.SerialData.LoadData(serialfile);
				this.errMsg = StrEncoder.Encode(this.SerialData.errMsg);

				return ret;
			}
			catch( Exception ex)
			{
				this.errMsg = StrEncoder.Encode(ex.ToString());
				return false;
			}
		}

		/// <summary>
		/// セットアップキーの登録処理
		/// </summary>
		/// <param name="serial"></param>
		/// <returns></returns>
		public bool SetupSerial(string filename, string serial, string username)
		{
			try
			{
				if( this.serialData.LoadSetupData(serial) == false )
				{
					this.errMsg = StrEncoder.Encode(this.SerialData.errMsg);
					return false;
				}
				// ユーザー名の設定の場所はここでないといけない
				this.SerialData.UserName = username;

				StreamWriter sr = new StreamWriter(filename);
				sr.Write(this.SerialData.SerializeData());
				sr.Close();

				return true;
			}
			catch( Exception ex)
			{
				this.errMsg = StrEncoder.Encode(ex.ToString());
				return false;
			}
		}
	}

}

