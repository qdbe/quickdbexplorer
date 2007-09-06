using System;
using System.Windows.Forms;


namespace quickDBExplorer
{
	/// <summary>
	/// qdbeListView の概要の説明です。
	/// </summary>
	public class qdbeListView : ListView
	{

		/// <summary>
		/// Ctrl+C 押下時の デリゲート
		/// </summary>
		public delegate void CopyDataHandler(
			object sender
			);

		/// <summary>
		/// Ctrl+Cが押された場合のイベント
		/// </summary>
		public event CopyDataHandler CopyData = null;

		/// <summary>
		/// Ctrl + F 押下時のデリゲート
		/// </summary>
		public delegate void ExTendedCopyDataHandler(
			object sender
			);

		/// <summary>
		/// Ctrl+F 押下時のイベント
		/// </summary>
		public event ExTendedCopyDataHandler ExtendedCopyData = null;


		private bool isAllSelecting = false;

		/// <summary>
		/// 全ての項目が選択されているか否か
		/// </summary>
		public bool IsAllSelecting 
		{
			get { return this.isAllSelecting; }
		}

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
					this.CopyData(this);
				}
				return true;
			}
			// CTRL+Fで拡張コピー処理
			if( (int)keyData == ( (int)Keys.Control + (int)Keys.F ) )
			{
				if( this.ExtendedCopyData != null )
				{
					this.ExtendedCopyData(this);
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

		public void ClearSelected()
		{
			this.BeginUpdate();
			for( int i = 0; i < this.Items.Count; i++ )
			{
				this.Items[i].Selected = false;
			}
			this.EndUpdate();
		}

		public int FindStringExact(string itemkey)
		{
			for( int i = 0; i < this.Items.Count; i++ )
			{
				if( (string)this.Items[i].Tag == itemkey )
				{
					return i;
				}
			}
			return -1;
		}
	}
}
