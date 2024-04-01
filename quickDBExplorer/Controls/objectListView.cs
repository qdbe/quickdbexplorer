using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using quickDBExplorer.Forms.Dialog;
using System.Linq;


namespace quickDBExplorer
{
    /// <summary>
    /// オブジェクト一覧の絞り込み条件変更イベント引数
    /// </summary>
    public class ObjectFilterChangedEventArgs : EventArgs
    { 
        /// <summary>
        /// 絞り込みされているか否か
        /// </summary>
        public bool IsFilterd { get; set; }
        /// <summary>
        /// 対象コントロール
        /// </summary>
        public ObjectListView Sender { get; set; }
    }

    /// <summary>
    /// オブジェクト一覧の絞り込み条件変更イベントDelegate
    /// </summary>
    public delegate void ObjectFilterChangedEventHandler(object sender, ObjectFilterChangedEventArgs e);

    /// <summary>
    /// Table等DBオブジェクトの一覧表示専用クラス
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
	public class ObjectListView : qdbeListView
	{
        private List<ListViewItem> currentItems;


        /// <summary>
        /// オブジェクト一覧の絞り込み条件変更イベントハンドラー
        /// </summary>
        public ObjectFilterChangedEventHandler ObjectFilterChanged = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ObjectListView()
		{
            IsAutoColumSort = true;
            this.currentItems = new List<ListViewItem>();
		}

		/// <summary>
		/// 一覧に追加するアイテムを作成作成する
		/// </summary>
		/// <param name="dr">データを参照するDataReader</param>
		/// <param name="dataSetter">オブジェクトの詳細を設定するメソッド</param>
		/// <returns></returns>
		private ListViewItem	CreateItem(IDataRecord dr, DataGetEventHandler dataSetter)
		{
			DBObjectInfo dboInfo = new DBObjectInfo(
                (int)dr["objectid"],
				(string)dr["tvs"],
				(string)dr["uname"],
				(string)dr["tbname"],
				(string)((DateTime)dr["cretime"]).ToString("yyyy/MM/dd HH:mm:ss"),
                (string)((DateTime)dr["modtime"]).ToString("yyyy/MM/dd HH:mm:ss"),
                (string)dr["synbase"],
				(string)dr["synType"]
				);
			dboInfo.DataGet	+= dataSetter;

			ListViewItem it = new ListViewItem( new string[]{ 
																dboInfo.DisplayObjType,
																dboInfo.Owner,
																dboInfo.ObjName,
																dboInfo.CreateTime,
                                                                dboInfo.ModifyTime
                                                            }
				);
			it.Tag = dboInfo;
			return it;
		}

        /// <summary>
        /// DataReader から オブジェクトリストの各項目を作成し設定する
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="sqlInterface"></param>
        public void SetObjectListItem(IDataReader dr, ISqlInterface sqlInterface)
        {
            this.BeginUpdate();
            this.Items.Clear();
            this.currentItems.Clear();
            DataGetEventHandler datahandler = sqlInterface.ObjectDetailSet();

            while (dr.Read())
            {
                currentItems.Add(this.CreateItem(dr, datahandler));
            }
            this.Items.AddRange(currentItems.ToArray());
            this.EndUpdate();
        }

        /// <summary>
        /// リストの内容をフィルタリングして表示する
        /// </summary>
        /// <param name="filterText"></param>
        public void FilterObjectList(string filterText)
        {
            FilterObjectList(filterText, true);
        }
        /// <summary>
        /// リストの内容をフィルタリングして表示する
        /// </summary>
        /// <param name="filterText"></param>
        /// <param name="isCaseSensitive">フィルター適用時に大文字小文字を区別するか否か</param>
        public void FilterObjectList(string filterText, bool isCaseSensitive)
        {
            this.BeginUpdate();
            this.Items.Clear();
            filterText = filterText.Trim();
            if (filterText == string.Empty)
            {
                this.Items.AddRange(currentItems.ToArray());
                this.EndUpdate();
                if (ObjectFilterChanged != null)
                {
                    ObjectFilterChanged(this, new ObjectFilterChangedEventArgs { IsFilterd = false, Sender = this });
                }
                return;
            }
            if (!isCaseSensitive)
            {
                filterText = filterText.ToLower();
            }
            List<ListViewItem> newlist = new List<ListViewItem>();
            foreach (ListViewItem itm in currentItems)
            {
                DBObjectInfo objInfo = (DBObjectInfo)itm.Tag;
                if (objInfo.IsMatch("", filterText, false, isCaseSensitive,SearchType.SearchContain))
                {
                    newlist.Add(itm);
                }
            }

            this.Items.AddRange(newlist.ToArray());
            this.EndUpdate();
            if (ObjectFilterChanged != null)
            {
                ObjectFilterChanged(this, new ObjectFilterChangedEventArgs { IsFilterd = true, Sender = this });
            }
        }

        /// <summary>
        /// 複数のフィルタを設定する
        /// </summary>
        /// <param name="filterTexts"></param>
        /// <param name="isCaseSensitive"></param>
        /// <param name="selectType"></param>
        public void FilterObjectList(string[] filterTexts, bool isCaseSensitive, SearchType selectType)
        {
            this.BeginUpdate();
            this.Items.Clear();

            if (filterTexts.Length == 0)
            {
                this.Items.AddRange(currentItems.ToArray());
                this.EndUpdate();
                if (ObjectFilterChanged != null)
                {
                    ObjectFilterChanged(this, new ObjectFilterChangedEventArgs { IsFilterd = false, Sender = this });
                }
                return;
            }

            List<ListViewItem> newlist = new List<ListViewItem>();
            foreach (ListViewItem itm in currentItems)
            {
                foreach (string filterText in filterTexts)
                {
                    string each = filterText.Trim();
                    if (each == string.Empty)
                    {
                        continue;
                    }
                    DBObjectInfo obj = (DBObjectInfo)itm.Tag;
                    if (obj.IsMatch(null, each, false, isCaseSensitive, selectType))
                    {
                        newlist.Add(itm);
                        break;
                    }
                }
            }

            if (newlist.Count == 0)
            {
                this.Items.AddRange(currentItems.ToArray());
                this.EndUpdate();
                if (ObjectFilterChanged != null)
                {
                    ObjectFilterChanged(this, new ObjectFilterChangedEventArgs { IsFilterd = false, Sender = this });
                }
                return;
            }

            this.Items.AddRange(newlist.ToArray());
            this.EndUpdate();
            if (ObjectFilterChanged != null)
            {
                ObjectFilterChanged(this, new ObjectFilterChangedEventArgs { IsFilterd = true, Sender = this });
            }
        }

        /// <summary>
        /// 複数のフィルタを設定する
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="isCaseSensitive"></param>
        /// <param name="selectType"></param>
        public void FilterObjectListDs(DataTable dt, bool isCaseSensitive, SearchType selectType)
        {
            this.BeginUpdate();
            this.Items.Clear();

            if (dt.Rows.Count == 0)
            {
                this.Items.AddRange(currentItems.ToArray());
                this.EndUpdate();
                if (ObjectFilterChanged != null)
                {
                    ObjectFilterChanged(this, new ObjectFilterChangedEventArgs { IsFilterd = false, Sender = this });
                }
                return;
            }

            List<ListViewItem> newlist = new List<ListViewItem>();
            foreach (ListViewItem itm in currentItems)
            {
                DBObjectInfo obj = (DBObjectInfo)itm.Tag;
                foreach (DataRow dr in dt.Rows)
                {
                    if (obj.IsMatch((string)dr[0], (string)dr[1], true, isCaseSensitive, selectType))
                    {
                        //if (!string.IsNullOrEmpty(excludeField))
                        //{
                        //    if( obj.FieldInfo != null )
                        //    {
                        //        if (obj.FieldInfo.Any(x => x.Name != null && x.Name.Contains(excludeField)))
                        //        {
                        //            continue;
                        //        }
                        //    }
                        //}
                        //if(!string.IsNullOrEmpty(excludeObjName))
                        //{
                        //    if (obj.ObjName.Contains(excludeObjName))
                        //    {
                        //        continue;
                        //    }
                        //}
                        newlist.Add(itm);
                        break;
                    }
                }
            }

            if (newlist.Count == 0)
            {
                this.Items.AddRange(currentItems.ToArray());
                this.EndUpdate();
                if (ObjectFilterChanged != null)
                {
                    ObjectFilterChanged(this, new ObjectFilterChangedEventArgs { IsFilterd = false, Sender = this });
                }
                return;
            }

            this.Items.AddRange(newlist.ToArray());
            this.EndUpdate();
            if (ObjectFilterChanged != null)
            {
                ObjectFilterChanged(this, new ObjectFilterChangedEventArgs { IsFilterd = true, Sender = this });
            }
        }


        /// <summary>
        /// 単一選択されているオブジェクト名を取得する([]なし)
        /// </summary>
        /// <returns></returns>
        public string GetSelectOneObjectName()
		{
			if( this.SelectedItems.Count == 1 )
			{
				return this.SelectedItems[0].Tag.ToString();
			}
			return string.Empty;
		}

		/// <summary>
		/// 単一選択されているオブジェクト名を取得する([]あり)
		/// </summary>
		/// <returns></returns>
		public string GetSelectOneObjectFormalName()
		{
			if( this.SelectedItems.Count == 1 )
			{
				return ((DBObjectInfo)this.SelectedItems[0].Tag).FormalName;
			}
			return string.Empty;
		}

		/// <summary>
		/// 選択された項目のうち、指定されたオブジェクト名を取得する
		/// </summary>
		/// <param name="i"></param>
		/// <returns></returns>
		public string GetSelectObjectName(int i)
		{
			if( this.SelectedItems.Count == 0 )
			{
				return "";
			}
			return this.SelectedItems[i].Tag.ToString();
		}

		/// <summary>
		/// 選択された項目のうち、指定されたオブジェクト名の情報を取得する
		/// </summary>
		/// <param name="i"></param>
		/// <returns></returns>
		public DBObjectInfo	GetSelectObject(int i)
		{
			if( this.SelectedItems.Count == 0 )
			{
				return null;
			}
			return (DBObjectInfo)this.SelectedItems[i].Tag;
		}
        
        /// <summary>
        /// 選択された項目のうち、選択されたオブジェクトの情報を取得する
        /// </summary>
        /// <returns></returns>
        public List<DBObjectInfo> GetSelectObjects()
        {
            if (this.SelectedItems.Count == 0)
            {
                return new List<DBObjectInfo>();
            }
            List<DBObjectInfo> selectList = new List<DBObjectInfo>();
            foreach(ListViewItem each in this.SelectedItems)
            {
                selectList.Add((DBObjectInfo)each.Tag);
            }
            return selectList;
        }

        /// <summary>
        /// 指定されたオブジェクトを選択状態にする
        /// </summary>
        /// <param name="list"></param>
        public void SetSelectedObjects(List<DBObjectInfo> list)
        {
            if( list == null || list.Count == 0 || this.Items.Count == 0 )
            {
                return;
            }
            this.BeginUpdate();
            this.ClearSelected();
            int idx = 0;
            foreach (DBObjectInfo each in list)
            {
                idx = this.FindStringExact(each.FormalName);
                if (idx >= 0)
                {
                    this.Items[idx].Selected = true;
                }
            }

            if (idx >= 0)
            {
                this.EnsureVisible(idx);
            }
            
            this.EndUpdate();
        }

		/// <summary>
		/// 一覧で選択されているオブジェクトの情報を再取得しなおす
		/// </summary>
		public void	ReloadSelectObjectInfo()
		{
			if( this.SelectedItems.Count == 0 )
			{
				return;
			}
			for( int i = 0; i < this.SelectedItems.Count; i++ )
			{
				((DBObjectInfo)this.SelectedItems[i].Tag).ReloadInfo();
			}
		}

		/// <summary>
		/// 指定した値と全く同じものを持つキーを検索する
		/// .NET 標準と違い、大文字・小文字を区別する
		/// </summary>
		/// <param name="itemkey"></param>
		/// <returns></returns>
		public override int FindStringExact(string itemkey)
		{
			for( int i = 0; i < this.Items.Count; i++ )
			{
				if( (string)this.Items[i].Tag.ToString() == itemkey ||
					((DBObjectInfo)this.Items[i].Tag).FormalName == itemkey
					)
				{
					return i;
				}
			}
			return -1;
		}

        /// <summary>
        /// オブジェクトのみのソート方法にセットする
        /// </summary>
        public void SetSortSchemaObject()
        {
            this.SortOrder = new ColumnSortOrder[] {
																		  new ColumnSortOrder(1,true),
																		  new ColumnSortOrder(2,true)
																	  };
        }

        /// <summary>
        /// スキーマとオブジェクトのソート方法にセットする
        /// </summary>
        public void SetSortObjectOnly()
        {
            this.SortOrder = new ColumnSortOrder[] {
																		  new ColumnSortOrder(2,true)
																	  };
        }
	}
}
