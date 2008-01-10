using System;
using System.Windows.Forms;
using System.Windows;

namespace quickDBExplorer
{
	/// <summary>
	/// CTRL+Aでの全選択機能を追加したテキストボックス
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public class quickDBExplorerTextBox : TextBox
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public quickDBExplorerTextBox() : base()
		{
			this.KeyDown += new KeyEventHandler(quickDBExplorerTextBox_KeyDown);
		}

		/// <summary>
		/// キーダウンイベントハンドラ
		/// CTRL+Aでの全選択処理を追加する
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="ev"></param>
		internal virtual void quickDBExplorerTextBox_KeyDown(object sender, KeyEventArgs ev)
		{
			if( ((TextBox)sender).Multiline != true &&
				ev.Alt != true &&
				ev.Control == true &&
				ev.Shift != true &&
				ev.KeyCode == Keys.A )
			{
				this.SelectAll();
				ev.Handled = true;
			}
		}
	}
}
