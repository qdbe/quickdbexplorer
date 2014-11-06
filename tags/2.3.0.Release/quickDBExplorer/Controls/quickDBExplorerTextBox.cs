using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows;
using System.Text.RegularExpressions;

namespace quickDBExplorer
{
	/// <summary>
	/// テキストボックスで Ctrl+Sが押下された場合のイベントハンドラ
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	public delegate void ShowHistoryEventHandler(
		object sender,
		System.EventArgs e
	);

	/// <summary>
	/// テキストボックスで Ctrl+Wが押下された場合のイベントハンドラ
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	public delegate void ShowZoomEventHandler(
		object sender,
		System.EventArgs e
	);

	/// <summary>
	/// CTRL+Aでの全選択機能、CTRL+Dでの文字削除を追加したテキストボックス
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public class quickDBExplorerTextBox : TextBox
	{
		/// <summary>
		/// テキスト入力エリアでの右クリックメニュー
		/// </summary>
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuCopy;
		private System.Windows.Forms.MenuItem menuCut;
		private System.Windows.Forms.MenuItem menuPaste;
		private System.Windows.Forms.MenuItem menuAllSelect;
		private System.Windows.Forms.MenuItem menuAllDelete;
		private System.Windows.Forms.MenuItem menuShowHistory;
		private System.Windows.Forms.MenuItem menuShowZoom;

		/// <summary>
		/// Ctrl+Sが押された場合のイベント
		/// </summary>
		public event ShowHistoryEventHandler ShowHistory = null;

		/// <summary>
		/// Ctrl+Sが押された場合のイベント
		/// </summary>
		public event ShowZoomEventHandler ShowZoom = null;


        private Dictionary<string, TextHistoryDataSet> pHistories;
        /// <summary>
        /// 入力履歴辞書
        /// </summary>
        public Dictionary<string, TextHistoryDataSet> Histories {
            get { return this.pHistories; }
            set
            {
                this.pHistories = value;
                ResetHistoryMemuItem();
            }
        }

        /// <summary>
        /// 履歴キー
        /// </summary>
        private string pHistoryKey { get; set; }

        /// <summary>
        /// 履歴キー
        /// </summary>
        public string HistoryKey
        {
            get {
                if (pHistoryKey == null)
                {
                    pHistoryKey = this.Name;
                }
                return pHistoryKey; 
            }
            set
            {
                pHistoryKey = value;
                ResetHistoryMemuItem();
            }
        }

        private void ResetHistoryMemuItem()
        {
            if (Histories == null)
            {
                if (this.contextMenu1.MenuItems.Contains(this.menuShowHistory))
                {
                    this.contextMenu1.MenuItems.Remove(this.menuShowHistory);
                }
            }
            else
            {
                if (!this.contextMenu1.MenuItems.Contains(this.menuShowHistory))
                {
                    this.menuShowHistory.Index = this.contextMenu1.MenuItems.Count + 1;
                    this.contextMenu1.MenuItems.Add(this.menuShowHistory);
                }
            }
        }

        /// <summary>
        /// 履歴情報
        /// </summary>
		public TextHistoryDataSet History
		{
			get {
                if (this.Histories == null)
                {
                    return null;
                }
                if (!Histories.ContainsKey(HistoryKey))
                {
                    Histories.Add(pHistoryKey, new TextHistoryDataSet());
                }
                return Histories[HistoryKey];
            }
		}

		/// <summary>
		/// 値の拡大表示を可能とするか否か
		/// </summary>
		private bool pShowZoom = false;

		/// <summary>
		/// 値の拡大表示を可能とするか否か
		/// </summary>
		public bool IsShowZoom
		{
			get { return pShowZoom; }
			set { 
				pShowZoom = value;
				if (this.pShowZoom == true)
				{
					if (!this.contextMenu1.MenuItems.Contains(this.menuShowZoom))
					{
						this.menuShowHistory.Index = this.contextMenu1.MenuItems.Count + 1;
						this.contextMenu1.MenuItems.Add(this.menuShowZoom);
					}
				}
				else
				{
					if (this.contextMenu1.MenuItems.Contains(this.menuShowZoom))
					{
						this.contextMenu1.MenuItems.Remove(this.menuShowZoom);
					}
				}
			}
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public quickDBExplorerTextBox() : base()
		{
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuCopy = new System.Windows.Forms.MenuItem();
			this.menuCut = new System.Windows.Forms.MenuItem();
			this.menuPaste = new System.Windows.Forms.MenuItem();
			this.menuAllSelect = new System.Windows.Forms.MenuItem();
			this.menuAllDelete = new System.Windows.Forms.MenuItem();
			this.menuShowHistory = new System.Windows.Forms.MenuItem();
			this.menuShowZoom = new System.Windows.Forms.MenuItem();

			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuCopy,
            this.menuCut,
            this.menuPaste,
            this.menuAllSelect,
            this.menuAllDelete
			});
			// 
			// menuCopy
			// 
			this.menuCopy.Index = 0;
			this.menuCopy.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
			this.menuCopy.Text = "コピー";
			this.menuCopy.Click += new System.EventHandler(this.menuCopy_Click);
			// 
			// menuCut
			// 
			this.menuCut.Index = 1;
			this.menuCut.Shortcut = System.Windows.Forms.Shortcut.CtrlX;
			this.menuCut.Text = "切り取り";
			this.menuCut.Click += new System.EventHandler(this.menuCut_Click);
			// 
			// menuPaste
			// 
			this.menuPaste.Index = 2;
			this.menuPaste.Shortcut = System.Windows.Forms.Shortcut.CtrlV;
			this.menuPaste.Text = "貼り付け";
			this.menuPaste.Click += new System.EventHandler(this.menuPaste_Click);
			// 
			// menuAllSelect
			// 
			this.menuAllSelect.Index = 3;
			this.menuAllSelect.Shortcut = System.Windows.Forms.Shortcut.CtrlA;
			this.menuAllSelect.Text = "全て選択";
			this.menuAllSelect.Click += new System.EventHandler(this.menuAllSelect_Click);
			// 
			// menuAllSelect
			// 
			this.menuAllDelete.Index = 4;
			this.menuAllDelete.Shortcut = System.Windows.Forms.Shortcut.CtrlD;
			this.menuAllDelete.Text = "全て削除";
			this.menuAllDelete.Click += new EventHandler(menuAllDelete_Click);

			// 
			// menuShowHistory
			// 
			this.menuShowHistory.Index = 5;
			this.menuShowHistory.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
			this.menuShowHistory.Text = "履歴表示";
			this.menuShowHistory.Click += new EventHandler(menuShowHistory_Click);

			// 
			// menuShowHistory
			// 
			this.menuShowZoom.Index = 6;
			this.menuShowZoom.Shortcut = System.Windows.Forms.Shortcut.CtrlW;
			this.menuShowZoom.Text = "値拡大表示";
			this.menuShowZoom.Click += new EventHandler(menuShowZoom_Click);

			this.ContextMenu = this.contextMenu1;

			this.KeyDown += new KeyEventHandler(quickDBExplorerTextBox_KeyDown);
			this.Enter +=new EventHandler(quickDBExplorerTextBox_Enter);
			this.TextChanged +=new EventHandler(quickDBExplorerTextBox_TextChanged);
		}

		private void menuAllDelete_Click(object sender, EventArgs e)
		{
			this.Text = string.Empty;
		}

		/// <summary>
		/// Ctrl+S を押下された場合に値の履歴表示イベントを呼び出し
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuShowHistory_Click(object sender, EventArgs e)
		{
			// 履歴が登録されていれば履歴選択画面を表示する
			if (this.History != null &&
				this.ShowHistory != null)
			{
				this.ShowHistory(this, new EventArgs());
			}
		}

		/// <summary>
		/// Ctrl+W を押下された場合に値の拡大表示イベントを呼び出し
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuShowZoom_Click(object sender, EventArgs e)
		{
			// 履歴が登録されていれば履歴選択画面を表示する
			if (this.IsShowZoom == true &&
				this.ShowZoom != null)
			{
				this.ShowZoom(this, new EventArgs());
			}
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

		///// <summary>
		///// MultiLine に限らず Ctrl + A で全選択させるか否か
		///// </summary>
		//private bool forceSelectAll = false;

		///// <summary>
		///// MultiLine に限らず Ctrl + A で全選択させるか否か
		///// </summary>
		//public bool ForceSelectAll
		//{
		//    get { return forceSelectAll; }
		//    set { forceSelectAll = value; }
		//}


		private bool pCanCtrlDelete = true;
		
		/// <summary>
		/// CTRL+Dで文字を削除可能にするか否か
		/// false: 削除できない
		/// true: 削除できる(既定値)
		/// </summary>
		public bool CanCtrlDelete
		{
			get { return this.pCanCtrlDelete; }
			set { this.pCanCtrlDelete = value; }
		}

		/// <summary>
		/// 変更前のテキスト
		/// </summary>
		private string orgString;

		/// <summary>
		/// Enter時のテキスト
		/// </summary>
		private string enterString = string.Empty;

		/// <summary>
		/// Enter 後、値に変更があったか否かを返す
		/// Focus がない場合、常にfalseを返す
		/// </summary>
		public bool IsTextChanged
		{
			get
			{
				if (this.Focused == true)
				{
					if (this.enterString == this.Text)
					{
						return false;
					}
					else
					{
						return true;
					}
				}
				else
				{
					return false;
				}
			}
		}

		private void menuCopy_Click(object sender, System.EventArgs e)
		{
			this.Copy();
		}

		private void menuCut_Click(object sender, System.EventArgs e)
		{
			this.Cut();
		}

		private void menuPaste_Click(object sender, System.EventArgs e)
		{
			this.Paste();
		}

		private void menuAllSelect_Click(object sender, System.EventArgs e)
		{
			this.SelectAll();
		}

		/// <summary>
		/// キーダウンイベントハンドラ
		/// CTRL+Aでの全選択処理を追加する
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="ev"></param>
		internal virtual void quickDBExplorerTextBox_KeyDown(object sender, KeyEventArgs ev)
		{
//			(((TextBox)sender).Multiline != true ||
//				forceSelectAll == true ) &&
			if( ev.Alt != true &&
				ev.Control == true &&
				ev.Shift != true &&
				ev.KeyCode == Keys.A )
			{
				this.SelectAll();
				ev.Handled = true;
			}

			// Ctrl＋Sで過去入力履歴ダイアログを表示する
			if (ev.Alt == false &&
				ev.Control == true &&
				ev.KeyCode == Keys.S)
			{
				if (this.History != null &&
					this.ShowHistory != null)
				{
					this.ShowHistory(this,new EventArgs());
				}
			}

			// Ctrl＋Wで過去入力履歴ダイアログを表示する
			if (ev.Alt == false &&
				ev.Control == true &&
				ev.KeyCode == Keys.W)
			{
				// 履歴が登録されていれば履歴選択画面を表示する
				if (this.IsShowZoom == true &&
					this.ShowZoom != null)
				{
					this.ShowZoom(this, new EventArgs());
				}
			}


			if( this.pCanCtrlDelete == true &&
				ev.Alt != true &&
				ev.Control == true &&
				ev.Shift != true &&
				ev.KeyCode == Keys.D )
			{
				this.Text = string.Empty;
			}
		}

		/// <summary>
		/// 値拡大ダイアログを表示する
		/// </summary>
		/// <param name="labelName">Windowタイトル</param>
		/// <param name="dlgEnter">ダイアログでOKボタンが押下された時のイベントハンドラ</param>
		public void DoShowZoom(string labelName, System.EventHandler dlgEnter)
		{
			ZoomFloatingDialog dlg = new ZoomFloatingDialog();
			dlg.EditText = this.Text;
			dlg.LableName = labelName;
			dlg.Enter += dlgEnter;
			dlg.Show();
			dlg.BringToFront();
			dlg.Focus();
		}

		/// <summary>
		/// 履歴選択ダイアログを表示する
		/// </summary>
		/// <param name="key">履歴を選択する際のキー情報</param>
		public bool DoShowHistory(string key)
		{
			if (this.History == null)
			{
				return false;
			}
			// 入力履歴の選択ダイアログを表示する
			HistoryViewer hv = new HistoryViewer(this.History, key);
			if (key != null)
			{
				hv.IsShowTable = true;
			}
			else
			{
				hv.IsShowTable = false;
			}
			if (DialogResult.OK == hv.ShowDialog())
			{
				if (this.Text != hv.RetString)
				{
					//違う情報であれば、それを表示し、履歴として追加する
					this.Text = hv.RetString;
					qdbeUtil.SetNewHistory(key, hv.RetString, this.History);
				}
				return true;
			}
			else
			{
				return false;
			}
		}

		private void quickDBExplorerTextBox_Enter(object sender, EventArgs e)
		{
			// 編集開始時点の文字列を記憶
			this.orgString = this.Text;
			this.enterString = this.Text;
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

        /// <summary>
        /// 履歴を保存する
        /// </summary>
        /// <param name="key"></param>
        public void SaveHistory(string key)
        {
            qdbeUtil.SetNewHistory(key,this.Text,this.History);
        }
	}
}
