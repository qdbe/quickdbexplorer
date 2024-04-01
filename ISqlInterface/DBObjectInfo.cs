using	System;
using	System.Data;
using	System.Collections;
using   System.Collections.Generic;
using System.Web;

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
        public string ObjType{ get; private set; }


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
        public string Owner { get; private set; }

		/// <summary>
		/// オブジェクトの名称
		/// </summary>
        public string ObjName { get; private set; }


        /// <summary>
        /// オブジェクトのID
        /// </summary>
        public int ObjId { get; private set; }

        /// <summary>
		/// オブジェクトが生成された日時
		/// </summary>
        public string CreateTime { get; private set; }

		/// <summary>
		/// オブジェクトが更新された日時
		/// </summary>
		public string ModifyTime { get; private set; }

		/// <summary>
		/// オブジェクトがシノニムの場合、その参照先
		/// </summary>
		public string SynonymBase { get; private set; }

		/// <summary>
		/// オブジェクトがシノニムの場合、その参照先のオブジェクトの種類
		/// </summary>
        public string SynonymBaseType { get; private set; }

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
        private Dictionary<string, DBFieldInfo> fieldDictionary = new Dictionary<string, DBFieldInfo>();
        private Dictionary<string, DBFieldInfo> FieldDictionary
        {
            get
            {
                SetFieldInfo();
                return this.fieldDictionary;
            }
        }

        private List<DBFieldInfo> FieldInfoList
        {
            get
            {
                // まだ読み込んでいない場合は、読み込みを自動的に実施する
                SetFieldInfo(); 
                return this.pFieldInfo;
            }
        }

		/// <summary>
		/// オブジェクトのフィールド情報をキャッシュして保持する
		/// フィールド情報のコレクションを管理する
		/// </summary>
        public IEnumerable<DBFieldInfo> FieldInfo
		{
			get 
			{
                SetFieldInfo();
                return this.FieldInfoList;
			}
		}

        /// <summary>
        /// フィールドの項目数を取得する
        /// </summary>
        public int FieldCount
        {
            get
            {
                SetFieldInfo();
                return this.FieldInfoList.Count;
            }
        }

        /// <summary>
        /// フィールドの情報を返す
        /// </summary>
        /// <param name="filedName">フィールド名</param>
        /// <returns></returns>
        public DBFieldInfo this[string filedName]{
            get {
                return this.FieldDictionary[filedName]; 
            }
        }

        /// <summary>
        /// フィールドの情報を返す
        /// </summary>
        /// <param name="fieldorder">フィールドの順番(0オリジン)</param>
        /// <returns></returns>
        public DBFieldInfo this[int fieldorder]
        {
            get {
                SetFieldInfo();
                return this.FieldInfoList[fieldorder]; 
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
                SetFieldInfo();
				return this.pSchemaBaseInfo; 
			}
            private set { this.pSchemaBaseInfo = value; }
		}

        private void SetFieldInfo()
        {
            if (this.pFieldInfo == null)
            {
                this.DataGet(this, new EventArgs());
                SetFieldDictionary();
            }
        }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="objectid">オブジェクトID</param>
		/// <param name="objectType">オブジェクトの型</param>
		/// <param name="owner">オブジェクトの所有者名</param>
		/// <param name="name">オブジェクトの名称</param>
		/// <param name="createdTime">オブジェクトの作成日時</param>
		/// <param name="modifyTime">オブジェクトの更新日時</param>
		/// <param name="synonymBase">シノニムの場合、その参照先のオブジェクト名</param>
		/// <param name="synonymBaseType">シノニムの場合、その参照先のオブジェクトの型</param>
		public DBObjectInfo( int objectid, string objectType, string owner, string name, string createdTime, string modifyTime, string synonymBase, string synonymBaseType )
		{
			if( objectType == null )
			{
				throw new ArgumentNullException("objectType");
			}
			if( synonymBaseType == null )
			{
				throw new ArgumentNullException("synonymBaseType");
			}

            this.ObjId = objectid;
            this.ObjType = objectType.TrimEnd(null);
			this.Owner = owner;
			this.ObjName = name;
			this.CreateTime = createdTime;
			this.ModifyTime = modifyTime;
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
        /// フィールド情報をクリアする
        /// </summary>
        public void ClearField()
        {
            this.pFieldInfo = new List<DBFieldInfo>();
        }

        /// <summary>
        /// フィールド情報を追加する
        /// </summary>
        /// <param name="add"></param>
        public void AddField(DBFieldInfo add)
        {
            SetFieldInfo();
            this.pFieldInfo.Add(add);
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

        /// <summary>
        /// Viewか否か
        /// </summary>
        public bool IsView
        {
            get
            {

                if (this.ObjType == "V")
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// スキーマ情報のテーブルをセットする
        /// </summary>
        /// <param name="dt"></param>
        public void SetSchemaInfo(DataTable dt)
        {
            this.pSchemaBaseInfo = dt;
        }


        private void SetFieldDictionary()
        {
            this.fieldDictionary.Clear();
            foreach (DBFieldInfo each in this.FieldInfoList)
            {
                this.fieldDictionary[each.Name] = each;
            }
        }


		/// <summary>
		/// 指定された名前に一致するかどうか
		/// </summary>
		/// <param name="schema"></param>
		/// <param name="objName"></param>
		/// <param name="isCheckSchema"></param>
		/// <param name="isCaseSensitive"></param>
		/// <param name="selectType"></param>
		/// <returns></returns>
        public bool IsMatch(string schema, string objName, bool isCheckSchema, bool isCaseSensitive, SearchType selectType)
		{
			if (isCheckSchema)
			{
				if(Owner == schema)
				{
					return CheckObjName(objName, isCaseSensitive,selectType);
				}
			}
            else
            {
                return CheckObjName(objName, isCaseSensitive,selectType);
            }
			return false;
        }

        private bool CheckObjName(string objName, bool isCaseSensitive, SearchType selectType)
        {
			string objn = this.ObjName;
			string searchName = objName;
			string[] searchNames = searchName.Split(".".ToCharArray());
			if (searchNames.Length == 2 &&
				!string.IsNullOrEmpty(searchNames[0])
				)
			{
				// スキーマ付き
				if (isCaseSensitive)
				{
					if (this.Owner != searchNames[0])
					{
						return false;
					}
				}
				else
				{
					if (this.Owner != searchNames[0])
					{
						return false;
					}
				}
                searchName = searchNames[1];
            }

            if (!isCaseSensitive)
            {
				objn = objn.ToLower();
				searchName = searchName.ToLower();
            }
            switch (selectType)
            {
                case SearchType.SearchExact:
                    if (objn == searchName)
                    {
						return true;
                    }
                    break;
                case SearchType.SearchContain:
                    if (objn.Contains(searchName))
                    {
                        return true;
                    }
                    break;
                case SearchType.SearchStartWith:
                    if (objn.StartsWith(searchName))
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
}
