using System;
using System.Windows.Forms;

namespace serialFactory
{
	/// <summary>
	/// アプリケーション向けのシリアルキー処理のインターフェース
	/// </summary>
	public class SerialManager
	{
		public SerialManager()
		{
			// 
			// TODO: コンストラクタ ロジックをここに追加してください。
			//
		}

		private int				serialArrayCnt;		// シリアルキーの項目数
		private int				serialYosoLen;		// シリアルキーの各要素の文字数
		private string			serialFileName = "";	// シリアルキーを登録するファイル名
		SerialFileFactory		sfile = new SerialFileFactory();
		public string			errMsg;

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
				this.sfile.SerialArrayCnt = cnt;
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
				this.sfile.SerialYosoLen = len;
			}
		}

		public string SerialFileName
		{
			get { return this.serialFileName; }
			set { this.serialFileName = value; }
		}

		/// <summary>
		/// シリアルキーが存在するかどうかをチェックし、存在しない場合警告メッセージ&キーの登録画面を表示する
		/// </summary>
		/// <param name="filename"></param>
		/// <returns></returns>
		public bool LoadAndCheckSerial()
		{
			bool ret = sfile.ReadSerialFile(this.serialFileName);
			if( ret == true )
			{
				// シリアルキーは正常に登録されている
				return true;
			}
			this.errMsg = sfile.errMsg;
			// ここからはシリアルキーが登録されていない場合
			SetupSerialDialog dlg = new SetupSerialDialog();
			dlg.smanager = this;
			if( dlg.ShowDialog() == DialogResult.OK )
			{
				return true;
			}
			return false;
		}

		public bool	SetupSerial(string serial, string user)
		{
			bool ret = sfile.SetupSerial(this.serialFileName,serial,user);
			if( ret == false )
			{
				this.errMsg = sfile.errMsg;
				return false;
			}
			return true;
		}
	
	}
}
