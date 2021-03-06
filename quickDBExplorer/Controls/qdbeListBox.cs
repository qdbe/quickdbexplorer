using System;
using System.Collections;
using System.Windows.Forms;

namespace quickDBExplorer
{
	/// <summary>
	/// Ctrl+C 押下時の デリゲート
	/// </summary>
	public delegate void CopyDataEventHandler(
	object sender,
	System.EventArgs e
	);

	/// <summary>
	/// Ctrl + F 押下時のデリゲート
	/// </summary>
	public delegate void ExtendedCopyDataEventHandler(
	object sender,
	System.EventArgs e
	);

	/// <summary>
	/// リストボックスの拡張機能版
	/// キー押下時の特殊処理(delegateでのイベントハンドラ呼び出し処理)が組み込んである
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public class qdbeListBox : System.Windows.Forms.ListBox
	{


		/// <summary>
		/// Ctrl+Cが押された場合のイベント
		/// </summary>
		public event CopyDataEventHandler CopyData = null;


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
		/// コンストラクタ
		/// </summary>
		public qdbeListBox() : base()
		{
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
				if( this.SelectionMode != SelectionMode.MultiExtended &&
					this.SelectionMode != SelectionMode.MultiSimple )
				{
					return true;
				}
				this.isAllSelecting = true;
				this.BeginUpdate();
				for( int i = 0; i <this.Items.Count; i++ )
				{
					this.SetSelected(i, true);
				}
				this.EndUpdate();
				this.isAllSelecting = false;

				this.OnSelectedIndexChanged (new System.EventArgs());
				return true;
			}
			return base.ProcessCmdKey (ref msg, keyData);
		}

        /// <summary>
        /// 選択されたアイテムの一覧を返す
        /// </summary>
        /// <returns></returns>
        public string[] GetSelectedItemAsStringArray()
        {
            ArrayList ar = new ArrayList();
            foreach(object each in this.SelectedItems)
            {
                ar.Add(each.ToString());
            }
            return (string[])ar.ToArray(typeof(string));
        }

        /// <summary>
        /// 指定したアイテムを選択状態にする
        /// </summary>
        /// <param name="itemlist"></param>
        public void SetSelectedItems(string[] itemlist)
        {
            this.ClearSelected();
            for (int i = 0; i < itemlist.Length; i++)
            {
                int idx = this.FindString(itemlist[i]);
                if (idx >= 0)
                {
                    this.SetSelected(idx, true);
                }
            }
        }
	}
}
