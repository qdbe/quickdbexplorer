using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Security.Cryptography;


namespace serialFactory
{
	/// <summary>
	/// SerialFactory の概要の説明です。
	/// </summary>
	public class SerialFactory
	{

		private ArrayList serialArray;		// シリアルキー
		private int				serialArrayCnt;		// シリアルキーの項目数
		private int				serialYosoLen;		// シリアルキーの各要素の文字数
		private DateTime		keyMakeDate;		// シリアルキー作成日時
		private DateTime		serialSetupDate;	// シリアルキー セットアップ日時
		private DateTime		limitDate;			// 利用期限日時
		private DateTime		limitLength;		// セットアップ後の有効期限日数(2002/05/21 13:48:00 からの差分)
		private DateTime		baseDate = new DateTime(2001,5,21,13,48,00);
		string	userName;			// ユーザー名
		public  string errMsg = "";

		private int				setUpLimitDay;

		public SerialFactory()
		{
			initDatas();
			this.serialArrayCnt = 8;
			this.serialYosoLen = 10;
			this.SetUpLimitDay = 60;
		}

		/// <summary>
		/// 各種データの初期化を行う
		/// </summary>
		protected void initDatas()
		{
			this.userName = "";
			this.serialArray = new ArrayList();
			this.serialSetupDate = DateTime.Parse("1900/1/1");
			this.keyMakeDate = DateTime.Parse("1900/1/1");
			this.limitDate = DateTime.Parse("1900/1/1");
			this.LimitLength = this.baseDate;
		}



		public string UserName
		{
			get { return this.userName; }
			set { this.userName = value; }
		}

		public ArrayList SerialArray
		{
			get { return this.serialArray; }
			set { this.serialArray = value; }
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
			}
		}

		public int SerialYosoLen
		{
			get { return this.serialYosoLen; }
			set 
			{
				int len = value;
				if( len < 3 )
				{
					throw new Exception( "SerialYosoLen は3以上の数値でなければいけません");
				}
				this.serialYosoLen = len;
			}
		}


		public DateTime SerialSetupDate
		{
			get { return this.serialSetupDate; }
			set { this.serialSetupDate = value; }
		}

		public DateTime KeyMakeDate
		{
			get { return this.keyMakeDate; }
			set { this.keyMakeDate = value; }
		}

		public DateTime LimitDate
		{
			get 
			{ 
				return this.limitDate; 
			}
			set { this.limitDate = value; }
		}

		public DateTime LimitLength
		{
			get { return this.limitLength; }
			set { this.limitLength = value; }
		}

		public int	LimitLengthInt
		{
			get 
			{
				TimeSpan ts = this.LimitLength - this.baseDate;
				return ts.Days;
			}
			set
			{
				this.LimitLength = this.baseDate.AddDays(value);
			}
		}

		public bool IsTestingPeriod
		{
			get 
			{
				if( this.serialArray.Count == 0 )
				{
					// 試用期間中である
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		public int SetUpLimitDay
		{
			get { return this.setUpLimitDay; }
			set { this.setUpLimitDay = value; }
		}

		/// <summary>
		/// 完全なシリアルキーデータが入っている文字列から、情報を読み取る
		/// </summary>
		/// <param name="serialstr">シリアルキーデータを持つ文字列</param>
		/// <returns></returns>
		public bool	LoadData(string serialstr)
		{
			return LoadData(serialstr,true);
		}

		public bool	LoadData(string serialstr, bool doCheck)
		{
			this.errMsg = "";
			try
			{
				this.initDatas();

				if( doCheck == true && this.IsValidSerialStr(serialstr) == false )
				{
					this.initDatas();
					return false;
				}

				// 文字列の分解を行う

				// チェックサムを取り除き
				string needstr = CheckSum.MinusCheckSum(serialstr);

				// 最初はシリアルキーのArrayList
				string kaiseki = needstr.Substring(0,this.SerialArrayCnt*this.SerialYosoLen);
				this.SerialArray = new ArrayList();
				for( int i = 0; i < this.SerialArrayCnt; i ++ )
				{
					this.SerialArray.Add(kaiseki.Substring(this.SerialYosoLen*i,this.SerialYosoLen));
				}

				// 次に、キーの作成日時
				needstr = needstr.Substring(kaiseki.Length);
				kaiseki = needstr.Substring(0,DateSerialFactory.SerializedLen);
				object ret = DateSerialFactory.Decode(kaiseki);
				if( doCheck == true && ret == null )
				{
					this.initDatas();
					return false;
				}
				if( ret != null )
				{
					this.KeyMakeDate = (DateTime)ret;
				}

				// 次に、シリアルキーのセットアップ日時
				needstr = needstr.Substring(kaiseki.Length);
				kaiseki = needstr.Substring(0,DateSerialFactory.SerializedLen);
				ret = DateSerialFactory.Decode(kaiseki);
				if( doCheck == true && ret == null )
				{
					this.initDatas();
					return false;
				}
				if( ret != null )
				{
					this.SerialSetupDate = (DateTime)ret;
				}


				// 次に、シリアルキーの利用期限日時
				needstr = needstr.Substring(kaiseki.Length);
				kaiseki = needstr.Substring(0,DateSerialFactory.SerializedLen);
				ret = DateSerialFactory.Decode(kaiseki);
				if( doCheck == true && ret == null )
				{
					this.initDatas();
					return false;
				}
				if( ret != null )
				{
					this.LimitDate = (DateTime)ret;
				}

				// 次に、シリアルキーの利用期限日数
				needstr = needstr.Substring(kaiseki.Length);
				kaiseki = needstr.Substring(0,DateSerialFactory.SerializedLen);
				ret = DateSerialFactory.Decode(kaiseki);
				if( doCheck == true && ret == null )
				{
					this.initDatas();
					return false;
				}
				if( ret != null )
				{
					this.LimitLength = (DateTime)ret;
				}


				// 次に、シリアルキーの利用期限日数
				needstr = needstr.Substring(kaiseki.Length);
				// 残りは全てユーザー名のはず
				string ustr = UserNameFactory.Decode(needstr);
				if( doCheck == true && ustr == null )
				{
					this.initDatas();
					return false;
				}
				this.UserName = ustr;

				if( doCheck == true )
				{
					return IsValidSerial();
				}
				return true;
			}
			catch( Exception ex)
			{
				this.errMsg = ex.ToString();
				if( doCheck == true )
				{
					this.initDatas();
				}
				return false;
			}
		}

		/// <summary>
		/// 現在の各種値が正しいものかどうかをチェックする
		/// </summary>
		/// <returns></returns>
		public bool IsValidSerial()
		{
			// シリアルキーの配列
			if( SerialKeyFactory.CheckArray(this.SerialArray,this.SerialYosoLen) == false )
			{
				this.initDatas();
				return false;
			}

			TimeSpan ts = this.SerialSetupDate - this.KeyMakeDate;
			//　キーの作成日より後にセットアップされているはず
			if( ts.Days < 0 )
			{
				this.initDatas();
				return false;
			}

			ts = this.LimitDate  - this.SerialSetupDate;
			//　有効期限 >= キーセットアップ日 であるはず
			if( ts.Days < 0 )
			{
				this.initDatas();
				return false;
			}

			// 現在の日付と有効期限を比較し、すでに有効期限が切れている
			ts = this.limitDate - DateTime.Now;
			if( ts.Days < 0 )
			{
				this.initDatas();
				return false;
			}

			// 有効日数がマイナスはありえない
			if( this.LimitLengthInt < 0 )
			{
				this.initDatas();
				return false;
			}

			return true;
		}


		/// <summary>
		/// 登録用シリアルキーデータが入っている文字列から、情報を読み取る
		/// シリアルキーのせっっとアップ時に利用
		/// </summary>
		/// <param name="serialstr">シリアルキーデータを持つ文字列</param>
		/// <returns></returns>
		public bool	LoadSetupData(string serialstr)
		{
			return LoadSetupData(serialstr,true);
		}

		public bool	LoadSetupData(string serialstr, bool doCheck)
		{
			try
			{
				this.initDatas();

				if( doCheck == true && this.IsValidSetupSerialStr(serialstr) == false )
				{
					this.initDatas();
					return false;
				}

				// 文字列の分解を行う

				// チェックサムを取り除き
				string needstr = CheckSum.MinusCheckSum(serialstr);

				// 最初はシリアルキーのArrayList
				string kaiseki = needstr.Substring(0,this.SerialArrayCnt*this.SerialYosoLen);
				this.SerialArray = new ArrayList();
				for( int i = 0; i < this.SerialArrayCnt; i ++ )
				{
					this.SerialArray.Add(kaiseki.Substring(this.SerialYosoLen*i,this.SerialYosoLen));
				}

				// 次に、キーの作成日時
				needstr = needstr.Substring(kaiseki.Length);
				kaiseki = needstr.Substring(0,DateSerialFactory.SerializedLen);
				object ret = DateSerialFactory.Decode(kaiseki);
				if( doCheck == true && ret == null )
				{
					this.initDatas();
					return false;
				}
				if( ret != null )
				{
					this.KeyMakeDate = (DateTime)ret;
				}

				// キー作成後、一定日数以内にセットアップが必要
				TimeSpan ts = DateTime.Now - this.keyMakeDate;
				if( doCheck == true && ts.Days > this.SetUpLimitDay )
				{
					this.initDatas();
					return false;
				}

				// 次に、シリアルキーの利用期限日時
				needstr = needstr.Substring(kaiseki.Length);
				kaiseki = needstr.Substring(0,DateSerialFactory.SerializedLen);
				ret = DateSerialFactory.Decode(kaiseki);
				if( doCheck == true && ret == null )
				{
					this.initDatas();
					return false;
				}
				if( ret != null )
				{
					this.LimitLength = (DateTime)ret;
				}

				// シリアルキーのセットアップ日時は現在になる
				this.SerialSetupDate = DateTime.Now;

				// 有効期限の設定
				this.LimitDate = this.SerialSetupDate.AddDays(this.LimitLengthInt);

				if( doCheck == true )
				{
					return IsValidSerial();
				}
				return true;
			}
			catch( Exception ex)
			{
				this.errMsg = ex.ToString();
				if( doCheck == true )
				{
					this.initDatas();
				}
				return false;
			}
		}

		/// <summary>
		/// 登録用のシリアルキーの構成をチェックする
		/// </summary>
		/// <param name="serialstr"></param>
		/// <returns></returns>
		private bool IsValidSerialStr(string serialstr)
		{
			try
			{
				if( serialstr == null ||
					serialstr == "" ||
					CheckSum.DoCheckSum(serialstr) != true)
				{
					return false;
				}
				// 文字列の長さのチェック
				if( serialstr.Length < 
					( ( this.serialArrayCnt * this.serialYosoLen ) + // シリアルキーの文字列長
					DateSerialFactory.SerializedLen * 4 +			// 日付*3 ( キー作成日, キー登録日、有効期限、有効日数 )
					2												// チェックサム 
					) )
				{
					return false;
				}
				return true;
			}
			catch( Exception ex)
			{
				this.errMsg = ex.ToString();
				// 何かしらのエラーになった
				return false;
			}
		}

		/// <summary>
		/// セットアップ時シリアルキーの構成をチェックする
		/// </summary>
		/// <param name="serialstr"></param>
		/// <returns></returns>
		private bool IsValidSetupSerialStr(string serialstr)
		{
			try
			{
				if( serialstr == null ||
					serialstr == "" ||
					CheckSum.DoCheckSum(serialstr) != true)
				{
					return false;
				}
				// 文字列の長さのチェック
				if( serialstr.Length !=
					( ( this.serialArrayCnt * this.serialYosoLen ) + // シリアルキーの文字列長
					DateSerialFactory.SerializedLen * 2 +			// 日付*3 ( キー作成日, 有効日数)
					2												// チェックサム 
					) )
				{
					return false;
				}
				return true;
			}
			catch( Exception ex)
			{
				this.errMsg = ex.ToString();
				// 何かしらのエラーになった
				return false;
			}
		}

		/// <summary>
		/// セットされている値から、ファイルに記録すべき認証情報を文字列として作成する
		/// </summary>
		/// <returns>変換されたシリアルキー情報の文字列</returns>
		public string SerializeData()
		{
			try
			{
				string serialstr = "";

				// シリアルキー
				for( int i = 0; i < this.SerialArrayCnt; i++ )
				{
					serialstr += (string)this.SerialArray[i];
				}
				serialstr += DateSerialFactory.Encode(this.KeyMakeDate);
				serialstr += DateSerialFactory.Encode(this.SerialSetupDate);
				serialstr += DateSerialFactory.Encode(this.LimitDate);
				serialstr += DateSerialFactory.Encode(this.LimitLength);
				serialstr += UserNameFactory.Encode(this.UserName);
				serialstr += CheckSum.MakeCheckSum(serialstr);


				if( this.IsValidSerialStr(serialstr) == false )
				{
					return null;
				}

				return serialstr;
			}
			catch( Exception ex)
			{
				this.errMsg = ex.ToString();
				return null;
			}
		}
		
		/// <summary>
		/// セットアップ用シリアルキーデータの新規作成
		/// </summary>
		/// <param name="limitDays">有効期限日数</param>
		/// <returns>作成されたシリアルキー  エラーになった場合はnull</returns>
		public string	MakeSetupKey(int limitDays)
		{
			try
			{
				this.KeyMakeDate = DateTime.Now;
				this.SerialArray = SerialKeyFactory.CreateKeys(this.SerialYosoLen,this.SerialArrayCnt);
				this.LimitLengthInt = limitDays;
				string serialstr = "";
				// シリアルキー
				for( int i = 0; i < this.SerialArrayCnt; i++ )
				{
					serialstr += (string)this.SerialArray[i];
				}
				// キー作成日時
				serialstr += DateSerialFactory.Encode(this.KeyMakeDate);
				// 
				serialstr += DateSerialFactory.Encode(this.LimitLength);
				serialstr += CheckSum.MakeCheckSum(serialstr);
				if( this.IsValidSetupSerialStr(serialstr) == false )
				{
					return null;
				}
				return serialstr;
			}
			catch( Exception ex)
			{
				this.errMsg = ex.ToString();
				return null;
			}

		}
	}
}