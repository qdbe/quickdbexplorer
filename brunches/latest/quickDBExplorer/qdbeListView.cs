using System;
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
		/// コンストラクタ
		/// </summary>
		public qdbeListView()
		{
			// 
			// TODO: コンストラクタ ロジックをここに追加してください。
			//
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
	}
}
