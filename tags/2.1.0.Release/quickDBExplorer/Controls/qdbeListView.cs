using System;
using System.Collections;
using System.Windows.Forms;


namespace quickDBExplorer
{
	/// <summary>
	/// qdbeListView の概要の説明です。
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public class qdbeListView : ListView
	{

		/// <summary>
		/// Ctrl+C 押下時の デリゲート
		/// </summary>
		public delegate void CopyDataEventHandler(
			object sender,
			System.EventArgs e
			);

		/// <summary>
		/// Ctrl+Cが押された場合のイベント
		/// </summary>
		public event CopyDataEventHandler CopyData = null;

		/// <summary>
		/// Ctrl + F 押下時のデリゲート
		/// </summary>
		public delegate void ExtendedCopyDataEventHandler(
			object sender,
			System.EventArgs e
			);

		/// <summary>
		/// Ctrl+F 押下時のイベント
		/// </summary>
		public event ExtendedCopyDataEventHandler ExtendedCopyData = null;


		private bool isAllSelecting = false;

		/// <summary>
		/// 全ての項目が選択されているか否か
		/// </summary>
		public bool IsAllSelecting 
		{
			get { return this.isAllSelecting; }
		}

        /// <summary>
        /// 一覧のソート順を指定する
        /// </summary>
        public ColumnSortOrder[] SortOrder
        {
            get { return this.sorter.SortOrder; }
            set { this.sorter.SortOrder = value; }
        }

        private ListSorter sorter;

        /// <summary>
        /// カラムを利用したソートを可能とするか
        /// </summary>
        public bool IsAutoColumSort { get; set; }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public qdbeListView()
		{
            sorter = new ListSorter();
            IsAutoColumSort = false;
            this.ListViewItemSorter = this.sorter;
		}

		/// <summary>
		/// 特殊キー押下時イベントハンドラ
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="keyData"></param>
		/// <returns></returns>
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			// CTRL+Cでコピー処理
			if( (int)keyData == ( (int)Keys.Control + (int)Keys.C ) )
			{
				if( this.CopyData != null )
				{
					this.CopyData(this, new System.EventArgs());
				}
				return true;
			}
			// CTRL+Fで拡張コピー処理
			if( (int)keyData == ( (int)Keys.Control + (int)Keys.F ) )
			{
				if( this.ExtendedCopyData != null )
				{
					this.ExtendedCopyData(this, new System.EventArgs());
				}
				return true;
			}
			// CTRL+Aで全選択
			if( (int)keyData == ( (int)Keys.Control + (int)Keys.A ) )
			{
				if( this.MultiSelect != true  )
				{
					return true;
				}
				this.isAllSelecting = true;
				this.BeginUpdate();
				for( int i = 0; i <this.Items.Count; i++ )
				{
					this.Items[i].Selected = true;
				}
				this.EndUpdate();
				this.isAllSelecting = false;

				this.OnSelectedIndexChanged (new System.EventArgs());
				return true;
			}
			return base.ProcessCmdKey (ref msg, keyData);
		}

		/// <summary>
		/// 全ての選択状態をクリアする
		/// </summary>
		public void ClearSelected()
		{
			this.BeginUpdate();
			for( int i = 0; i < this.Items.Count; i++ )
			{
				this.Items[i].Selected = false;
				this.Items[i].Focused = false;
			}
			this.EndUpdate();
		}

		/// <summary>
		/// 指定した値と全く同じものを持つキーを検索する
		/// .NET 標準と違い、大文字・小文字を区別する
		/// </summary>
		/// <param name="itemkey"></param>
		/// <returns></returns>
		public virtual int FindStringExact(string itemkey)
		{
			for( int i = 0; i < this.Items.Count; i++ )
			{
				if( (string)this.Items[i].Tag.ToString() == itemkey )
				{
					return i;
				}
			}
			return -1;
		}

        /// <summary>
        /// カラムヘッダがクリックされたことによるソート順を書き換える
        /// </summary>
        /// <param name="col"></param>
        public void SetColumClick(int col)
        {
            ColumnSortOrder[] so = this.sorter.SortOrder;
            if (so.Length != 1)
            {
                this.sorter.SortOrder = new ColumnSortOrder[] {
																	   new ColumnSortOrder(col,true)
																   };

            }
            else
            {
                if (this.sorter.SortOrder[0].Column == col)
                {
                    // 同じカラムが指定された場合は、ソート順を逆にする
                    this.sorter.SortOrder[0].SortAsc = !this.sorter.SortOrder[0].SortAsc;
                }
                else
                {
                    // 別のカラムが指定された場合は、そのカラムをソートキーにして再設定
                    this.sorter.SortOrder = new ColumnSortOrder[] {
																		   new ColumnSortOrder(col,true)
																	   };
                }
            }
            this.Sort();
        }
        /// <summary>
        /// カラムヘッダがクリックされた
        /// </summary>
        /// <param name="e"></param>
        protected override void OnColumnClick(ColumnClickEventArgs e)
        {
            if (!this.IsAutoColumSort) return;

            SetColumClick(e.Column);
        }

	}

    /// <summary>
    /// オブジェクトのソート順を管理する為のクラス
    /// </summary>
    public class ColumnSortOrder
    {
        private int column = 0;
        private bool sortAsc = true;

        /// <summary>
        /// ソートするカラムの位置
        /// </summary>
        public int Column
        {
            get { return this.column; }
            set { this.column = value; }
        }

        /// <summary>
        /// ソートを昇順で行うか、降順で行うかの指定
        /// </summary>
        public bool SortAsc
        {
            get { return this.sortAsc; }
            set { this.sortAsc = value; }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="col"></param>
        /// <param name="asc"></param>
        public ColumnSortOrder(int col, bool asc)
        {
            this.column = col;
            this.sortAsc = asc;
        }
    }

    /// <summary>
    /// qdbeListView の内部で利用する
    /// sort() メソッド実施時のソート順を制御する為の比較用クラス
    /// </summary>
    class ListSorter : IComparer
    {
        private ColumnSortOrder[] sortOrder;

        /// <summary>
        /// ソート順を取得・設定する
        /// </summary>
        public ColumnSortOrder[] SortOrder
        {
            get { return this.sortOrder; }
            set { this.sortOrder = value; }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ListSorter()
        {
            // 規定のソート順の指定
            this.sortOrder = new ColumnSortOrder[] { 
                new ColumnSortOrder(0,true)
            };
        }

        /// <summary>
        /// 実際の比較メソッド
        /// </summary>
        /// <param name="x">比較元</param>
        /// <param name="y">比較元</param>
        /// <returns>1,0,-1のいづれか</returns>
        public int Compare(object x, object y)
        {
            ListViewItem xx;
            ListViewItem yy;
            xx = (ListViewItem)x;
            yy = (ListViewItem)y;

            int ret = 0;

            // 比較対象として設定されているカラム数分、違う結果になるまで比較を行う
            for (int i = 0; i < this.sortOrder.Length && ret == 0; i++)
            {
                if (this.sortOrder[i].SortAsc == true)
                {
                    ret = xx.SubItems[this.sortOrder[i].Column].Text.CompareTo(yy.SubItems[this.sortOrder[i].Column].Text);
                }
                else
                {
                    ret = yy.SubItems[this.sortOrder[i].Column].Text.CompareTo(xx.SubItems[this.sortOrder[i].Column].Text);
                }
            }
            return ret;
        }
    }
}
