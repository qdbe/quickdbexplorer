using	System;
using	System.Data;
using	System.Collections;
using   System.Collections.Generic;

namespace quickDBExplorer
{
	/// <summary>
	/// フィールド情報取得時のデータ再取得用イベントハンドラ
	/// </summary>
    public delegate void DataGetEventHandler(DBObjectInfo sender, System.EventArgs e);

	/// <summary>
	/// リストに表示する DB オブジェクトの情報を管理する
	/// </summary>
    [Serializable()]
	public class	DBObjectInfo
	{

		/// <summary>
		/// フィールド情報取得時に情報がまだ未取得時に発生するイベント
		/// このイベントハンドラにより、データをセットさせる
		/// </summary>
		public event DataGetEventHandler DataGet = null;

		/// <summary>
		/// オブジェクトの種類
		/// 空白: Table
		/// V:		View
		/// S:		Synonym
		/// </summary>
        public string ObjType{ get; set; }


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
				switch(this.ObjType)
				{
					case	"U":
						return " ";
					case	"SN":
						return "S";
					default:
                        return this.ObjType;
				}
			}
		}

		/// <summary>
		/// オブジェクトの所有者名
		/// </summary>
        public string Owner{ get; set; }

		/// <summary>
		/// オブジェクトの名称
		/// </summary>
        public string ObjName { get; set; }

		/// <summary>
		/// オブジェクトが生成された日時
		/// </summary>
		public string CreateTime{ get; set; }

		/// <summary>
		/// オブジェクトがシノニムの場合、その参照先
		/// </summary>
		public string SynonymBase{ get; set; }

		/// <summary>
		/// オブジェクトがシノニムの場合、その参照先のオブジェクトの種類
		/// </summary>
		public string SynonymBaseType{ get; set; }

		/// <summary>
		/// [] でくくったオブジェクトの正式名称を取得する
		/// </summary>
		public string	FormalName
		{
			get 
			{
				return "[" + this.Owner + "].[" + this.ObjName + "]";
			}
		}

		/// <summary>
		/// select 可能か否かを取得する
		/// </summary>
		public bool		CanSelect
		{
			get 
			{
				switch( this.ObjType )
				{
					case	"U":
					case	"V":
						return true;
					case	"SN":	
						// Synonymの場合、ベースオブジェクトの型を見る必要がある
						if( this.SynonymBaseType == "U" ||
                            this.SynonymBaseType == "V")
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
				if( this.ObjType == "SN" )
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
				if( this.ObjType == "U" )
				{
					return true;
				}
				if( this.ObjType == "SN" &&
					this.SynonymBaseType == "U" )
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
				if( this.ObjType == "SN" )
				{
					return this.SynonymBase;
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
				if( this.ObjType == "SN" )
				{
					return this.SynonymBase;
				}
				return string.Format(System.Globalization.CultureInfo.CurrentCulture,"{0}.{1}", this.Owner, this.ObjName );
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
				if( this.ObjType == "SN" )
				{
					return this.SynonymBaseType;
				}
				else
				{
                    return this.ObjType;
				}
			}
		}

		private	List<DBFieldInfo>	pFieldInfo = null;

		/// <summary>
		/// オブジェクトのフィールド情報をキャッシュして保持する
		/// フィールド情報のコレクションを管理する
		/// </summary>
		public List<DBFieldInfo> FieldInfo
		{
			get 
			{
				// まだ読み込んでいない場合は、読み込みを自動的に実施する
				if( this.pFieldInfo == null )
				{
					this.DataGet(this, new EventArgs());
				}
				return this.pFieldInfo; 
			}
			set 
			{
				this.pFieldInfo = value;
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
				if( this.pFieldInfo == null )
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

            this.ObjType = objectType.TrimEnd(null);
			this.Owner = owner;
			this.ObjName = name;
			this.CreateTime = createdTime;
			this.SynonymBase = synonymBase;
			this.SynonymBaseType = synonymBaseType.TrimEnd(null);
			this.pSchemaBaseInfo = null;
		}

		/// <summary>
		/// 文字列化する。
		/// 所有者名+"."+オブジェクト名
		/// を返す
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format(System.Globalization.CultureInfo.CurrentCulture,"{0}.{1}", this.Owner, this.ObjName );
		}

		/// <summary>
		/// Alias が指定されていた場合に、Alias 付きのオブジェクト正式名称を返す
		/// </summary>
		/// <param name="alias">オブジェクト修飾子</param>
		/// <returns>解析後のオブジェクト名([owner].[tabblname] alias 形式)</returns>
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
			return string.Format(System.Globalization.CultureInfo.CurrentCulture,"[{0}].[{1}_{2}]",this.Owner, this.ObjName, suffix);
		}

		/// <summary>
		/// オブジェクトの情報を取得しなおす
		/// </summary>
		public	void	ReloadInfo()
		{
			this.DataGet(this, new EventArgs());
		}

		/// <summary>
		/// フィールドにアセンブリを利用した型があるか否か
		/// </summary>
		public bool  IsUseAssemblyType
		{
			get 
			{
				foreach(DBFieldInfo fi in this.FieldInfo)
				{
					if( fi.IsAssembly == true )
					{
						return true;
					}
				}
				return false;
			}
		}

		/// <summary>
		/// フィールドに Identityを利用したものを持つか否か
		/// </summary>
		public bool IsUseIdentity
		{
			get 
			{
				foreach(DBFieldInfo fi in this.FieldInfo)
				{
					if( fi.IsIdentity == true )
					{
						return true;
					}
				}
				return false;
			}
		}

	}
}
