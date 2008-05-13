using System;
using System.Windows.Forms;
using System.Windows;
using System.Text.RegularExpressions;

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
			this.Enter +=new EventHandler(quickDBExplorerTextBox_Enter);
			this.TextChanged +=new EventHandler(quickDBExplorerTextBox_TextChanged);
		}

		/// <summary>
		/// 数値のみ入力可能にするか否か
		/// </summary>
		private bool pIsDigitOnly = false;

		/// <summary>
		/// 数値のみ入力可能にするか否か
		/// false: 数値以外も入力可能
		/// true: 数値のみ可能
		/// </summary>
		public bool IsDigitOnly
		{
			get { return this.pIsDigitOnly; }
			set { this.pIsDigitOnly = value; }
		}

		private string orgString;

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

		private void quickDBExplorerTextBox_Enter(object sender, EventArgs e)
		{
			// 編集開始時点の文字列を記憶
			this.orgString = this.Text;
		}

		private void quickDBExplorerTextBox_TextChanged(object sender, EventArgs e)
		{
			string nowText = this.Text;
			if( this.pIsDigitOnly == true && nowText != string.Empty)
			{
				// 数値のみの場合、文字変更の結果 数字のみ残っているかどうかをチェックする
				if( Regex.IsMatch(nowText,"^[0-9]+$") == false )
				{
					// 一致しない場合は元に戻してやる
					this.Text = this.orgString;
				}
				else
				{
					// 現在の値を最新の文字列として記録しておき、比較材料とスル
					this.orgString = this.Text;
				}
			}
			else
			{
				this.orgString = this.Text;
			}
		}
	}
}
