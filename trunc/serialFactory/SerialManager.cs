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
		SerialFileFactory		serialFilef = new SerialFileFactory();
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
				this.serialFilef.SerialArrayCnt = cnt;
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
				this.serialFilef.SerialYosoLen = len;
			}
		}

		public string SerialFileName
		{
			get { return this.serialFileName; }
			set { this.serialFileName = value; }
		}

		public string UserName
		{
			get { return this.serialFilef.SerialData.UserName; }
		}

		public string LimitDate
		{
			get { return this.serialFilef.SerialData.LimitDate.ToString("yyyy/M/d"); }
		}

		/// <summary>
		/// シリアルキーが存在するかどうかをチェックし、存在しない場合警告メッセージ&キーの登録画面を表示する
		/// </summary>
		/// <param name="filename"></param>
		/// <returns></returns>
		public bool LoadAndCheckSerial()
		{
			bool ret = serialFilef.ReadSerialFile(this.serialFileName);
			if( ret == true )
			{
				if( this.serialFilef.IsTestingPeriod == false )
				{
					TimeSpan ts = this.serialFilef.SerialData.LimitDate - DateTime.Now;
					if( ts.Days < 7 )
					{
						MessageBox.Show("ライセンスの有効期限は " + this.serialFilef.SerialData.LimitDate.ToString("yyyy/M/d") + "です。早い段階で新しいライセンスを入手することをお勧めします。" );
					}
				}
				// シリアルキーは正常に登録されている
				return true;
			}
			this.errMsg = serialFilef.errMsg;
			// ここからはシリアルキーが登録されていない場合
			SetupSerialDialog dlg = new SetupSerialDialog();
			dlg.smanager = this;
			if( dlg.ShowDialog() == DialogResult.OK )
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// シリアルキーを登録する
		/// </summary>
		/// <param name="serial"></param>
		/// <param name="user"></param>
		/// <returns></returns>
		public bool	SetupSerial(string serial, string user)
		{
			bool ret = serialFilef.SetupSerial(this.serialFileName,serial,user);
			if( ret == false )
			{
				this.errMsg = serialFilef.errMsg;
				return false;
			}
			return true;
		}

		public void	ShowRegisterInfo()
		{
			if( this.serialFilef.IsTestingPeriod == true )
			{
				MessageBox.Show("現在試用期間中です");
			}
			else
			{
				RegisterInfoDialog dlg = new RegisterInfoDialog();
				dlg.smanager = this;
				dlg.ShowDialog();
			}
		}
	
	}
}
