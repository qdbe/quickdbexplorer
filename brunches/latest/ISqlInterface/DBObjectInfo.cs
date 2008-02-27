using	System;
using	System.Data;
using	System.Collections;

namespace quickDBExplorer
{
	/// <summary>
	/// フィールド情報取得時のデータ再取得用イベントハンドラ
	/// </summary>
	public delegate void DataGetEventHandler(object sender, System.EventArgs e);

	/// <summary>
	/// リストに表示する DB オブジェクトの情報を管理する
	/// </summary>
	public class	DBObjectInfo
	{

		/// <summary>
		/// フィールド情報取得時に情報がまだ未取得時に発生するイベント
		/// このイベントハンドラにより、データをセットさせる
		/// </summary>
		public event DataGetEventHandler DataGet = null;

		private		string objType;
		/// <summary>
		/// オブジェクトの種類
		/// 空白: Table
		/// V:		View
		/// S:		Synonym
		/// </summary>
		public		string	ObjType
		{
			get { return this.objType; }
			set { this.objType = value; }
		}


		/// <summary>
		/// オブジェクトの種類(表示用)
		/// 空白: Table
		/// V:		View
		/// S:		Synonym
		/// </summary>
		public		string DisplayObjType
		{
			get 
			{
				switch(this.objType)
				{
					case	"U":
						return " ";
					case	"SN":
						return "S";
					default:
						return this.objType;
				}
			}
		}

		private		string	owner;
		/// <summary>
		/// オブジェクトの所有者名
		/// </summary>
		public	string	Owner
		{
			get { return this.owner; }
			set { this.owner = value; }
		}

		private		string	objname;
		/// <summary>
		/// オブジェクトの名称
		/// </summary>
		public	string ObjName
		{
			get { return this.objname; }
			set { this.objname = value; }
		}

		private string createTime;
		/// <summary>
		/// オブジェクトが生成された日時
		/// </summary>
		public string CreateTime
		{
			get { return this.createTime; }
			set { this.createTime = value; }
		}

		private string synonymBase;
		/// <summary>
		/// オブジェクトがシノニムの場合、その参照先
		/// </summary>
		public string SynonymBase
		{
			get { return this.synonymBase; }
			set { this.synonymBase = value; }
		}
		private string synonymBaseType;

		/// <summary>
		/// オブジェクトがシノニムの場合、その参照先のオブジェクトの種類
		/// </summary>
		public string SynonymBaseType
		{
			get { return this.synonymBaseType; }
			set { this.synonymBaseType = value; }
		}

		/// <summary>
		/// [] でくくったオブジェクトの正式名称を取得する
		/// </summary>
		public string	FormalName
		{
			get 
			{
				return "[" + this.owner + "].[" + this.objname + "]";
			}
		}

		/// <summary>
		/// select 可能か否かを取得する
		/// </summary>
		public bool		CanSelect
		{
			get 
			{
				switch( this.objType )
				{
					case	"U":
					case	"V":
						return true;
					case	"SN":	
						// Synonymの場合、ベースオブジェクトの型を見る必要がある
						if( this.synonymBaseType == "U" ||
							this.synonymBaseType == "V" )
						{
							return true;
						}
						break;
					default:
						break;
				}
				return false;
			}
		}

		/// <summary>
		/// シノニムかどうかを取得する
		/// </summary>
		public bool	IsSynonym
		{
			get 
			{
				if( this.objType == "SN" )
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		/// <summary>
		/// 統計情報の更新が可能などうかを取得する
		/// </summary>
		public bool	CanStatistics
		{
			get 
			{
				if( this.objType == "U" )
				{
					return true;
				}
				if( this.objType == "SN" &&
					this.synonymBaseType == "U" )
				{
					return true;
				}
				return false;
			}
		}

		/// <summary>
		/// 実際のオブジェクト名を取得する
		/// シノニムの場合はその参照先のオブジェクト名を返す
		/// </summary>
		public string	RealObjName
		{
			get 
			{
				if( this.objType == "SN" )
				{
					return this.synonymBase;
				}
				return this.FormalName;
			}
		}

		/// <summary>
		/// 実際のオブジェクト名を取得する
		/// 名称には[]はつかない
		/// シノニムの場合はその参照先のオブジェクト名を返す
		/// </summary>
		public string	RealObjNameNoPare
		{
			get
			{
				if( this.objType == "SN" )
				{
					return this.synonymBase;
				}
				return string.Format(System.Globalization.CultureInfo.CurrentCulture,"{0}.{1}", this.owner, this.objname );
			}
		}

		/// <summary>
		/// 実際のオブジェクトの型を取得する
		/// シノニムの場合はその参照先のオブジェクトの型を返す
		/// </summary>
		public string	RealObjType
		{
			get 
			{
				if( this.objType == "SN" )
				{
					return this.synonymBaseType;
				}
				else
				{
					return this.objType;
				}
			}
		}

		private	ArrayList	fieldInfo = null;
		/// <summary>
		/// テーブルのフィールド情報をキャッシュして保持する
		/// フィールド情報のコレクションを管理する
		/// </summary>
		public	ArrayList	FieldInfo
		{
			get 
			{
				// まだ読み込んでいない場合は、読み込みを自動的に実施する
				if( this.fieldInfo == null )
				{
					this.DataGet(this, new EventArgs());
				}
				return this.fieldInfo; 
			}
			set 
			{
				this.fieldInfo = value;
			}
		}

		private DataTable	pSchemaBaseInfo;
		/// <summary>
		/// スキーマ情報を保持している DataTable
		/// 行は保持していない
		/// </summary>
		public	DataTable	SchemaBaseInfo
		{
			get { 
				// まだ読み込んでいない場合は、読み込みを自動的に実施する
				if( this.fieldInfo == null )
				{
					this.DataGet(this, new EventArgs());
				}
				return this.pSchemaBaseInfo; 
			}
			set { this.pSchemaBaseInfo = value; }
		}

		/// <summary>
			/// コンストラクタ
			/// </summary>
			/// <param name="objectType">オブジェクトの型</param>
			/// <param name="owner">オブジェクトの所有者名</param>
			/// <param name="name">オブジェクトの名称</param>
			/// <param name="createdTime">オブジェクトの作成日時</param>
			/// <param name="synonymBase">シノニムの場合、その参照先のオブジェクト名</param>
			/// <param name="synonymBaseType">シノニムの場合、その参照先のオブジェクトの型</param>
		public DBObjectInfo( string objectType, string owner, string name, string createdTime, string synonymBase, string synonymBaseType )
		{
			if( objectType == null )
			{
				throw new ArgumentNullException("objectType");
			}
			if( synonymBaseType == null )
			{
				throw new ArgumentNullException("synonymBaseType");
			}

			this.objType = objectType.TrimEnd(null);
			this.owner = owner;
			this.objname = name;
			this.createTime = createdTime;
			this.synonymBase = synonymBase;
			this.synonymBaseType = synonymBaseType.TrimEnd(null);
		}

		/// <summary>
		/// 文字列化する。
		/// 所有者名+"."+オブジェクト名
		/// を返す
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format(System.Globalization.CultureInfo.CurrentCulture,"{0}.{1}", this.owner, this.objname );
		}

		/// <summary>
		/// Alias が指定されていた場合に、Alias 付きのオブジェクト正式名称を返す
		/// </summary>
		/// <param name="alias">テーブル修飾子</param>
		/// <returns>解析後のテーブル名([owner].[tabblname] alias 形式)</returns>
		public string GetAliasName(string alias)
		{
			string retstr;
			retstr = this.FormalName;
			if( alias != null && alias.Length != 0 )
			{
				retstr += " " + alias;
			}
			return retstr;
		}

		/// <summary>
		/// 正式名称に指定れた文字列を付加したものにして返す
		/// </summary>
		/// <param name="suffix">付加する文字列</param>
		/// <returns></returns>
		public string	GetNameAdd(string suffix)
		{
			return string.Format(System.Globalization.CultureInfo.CurrentCulture,"[{0}].[{1}_{2}]",this.owner, this.objname, suffix);
		}

		/// <summary>
		/// オブジェクトの情報を取得しなおす
		/// </summary>
		public	void	ReloadInfo()
		{
			this.DataGet(this, new EventArgs());
		}
	}
}
