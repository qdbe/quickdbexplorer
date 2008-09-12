using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using quickDBExplorer;

namespace quickDBExplorer
{
	/// <summary>
	/// 各種入力履歴の選択ダイアログ
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public class HistoryViewer : quickDBExplorer.quickDBExplorerBaseForm
	{
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnClear;
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.ListView historyList;
		/// <summary>
		/// 入力履歴情報
		/// </summary>
		private TextHistoryDataSet	TextHistoryDataSetDS = new TextHistoryDataSet();
		/// <summary>
		/// 対象オブジェクト名称
		/// </summary>
		private string targetTable = "";
		/// <summary>
		/// 履歴拡大表示ボタン
		/// </summary>
		private System.Windows.Forms.Button btnDispAll;

		/// <summary>
		/// 選択された入力履歴情報
		/// </summary>
		private string pRetString = "";
		/// <summary>
		/// 選択された入力履歴情報
		/// </summary>
		public string RetString 
		{
			get { return this.pRetString; }
			set { this.pRetString = value; }
		}

		/// <summary>
		/// オブジェクト名称を表示するか否かの指定
		/// </summary>
		private bool pIsShowTable = true;

		/// <summary>
		/// オブジェクト名称を表示するか否かの指定
		/// </summary>
		public bool IsShowTable
		{
			get { return this.pIsShowTable; }
			set { this.pIsShowTable = value; }
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="hdata">履歴情報</param>
		/// <param name="curTable">現在選択されているオブジェクト名称</param>
		public HistoryViewer(TextHistoryDataSet hdata, string curTable)
		{
			// この呼び出しは Windows フォーム デザイナで必要です。
			InitializeComponent();
			this.TextHistoryDataSetDS = hdata;
			this.targetTable = curTable;
		}


		/// <summary>
		/// 使用されているリソースに後処理を実行します。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region デザイナで生成されたコード
		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnClear = new System.Windows.Forms.Button();
			this.historyList = new System.Windows.Forms.ListView();
			this.btnDispAll = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// MsgArea
			// 
			this.MsgArea.Location = new System.Drawing.Point(332, 270);
			this.MsgArea.Name = "MsgArea";
			this.MsgArea.Size = new System.Drawing.Size(124, 24);
			this.MsgArea.TabIndex = 3;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(462, 270);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(72, 24);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "戻る(&X)";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnOK.Location = new System.Drawing.Point(18, 272);
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "決定(&O)";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnClear
			// 
			this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnClear.Location = new System.Drawing.Point(104, 272);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(88, 23);
			this.btnClear.TabIndex = 2;
			this.btnClear.Text = "履歴消去(&L)";
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// historyList
			// 
			this.historyList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.historyList.FullRowSelect = true;
			this.historyList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.historyList.Location = new System.Drawing.Point(16, 8);
			this.historyList.MultiSelect = false;
			this.historyList.Name = "historyList";
			this.historyList.Size = new System.Drawing.Size(520, 254);
			this.historyList.TabIndex = 0;
			this.historyList.View = System.Windows.Forms.View.Details;
			this.historyList.DoubleClick += new System.EventHandler(this.historyList_DoubleClick);
			// 
			// btnDispAll
			// 
			this.btnDispAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnDispAll.Location = new System.Drawing.Point(210, 272);
			this.btnDispAll.Name = "btnDispAll";
			this.btnDispAll.Size = new System.Drawing.Size(112, 23);
			this.btnDispAll.TabIndex = 3;
			this.btnDispAll.Text = "履歴拡大表示(&Z)";
			this.btnDispAll.Click += new System.EventHandler(this.btnDispAll_Click);
			// 
			// HistoryViewer
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(552, 304);
			this.Controls.Add(this.historyList);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnDispAll);
			this.Name = "HistoryViewer";
			this.ShowInTaskbar = false;
			this.Text = "過去入力履歴選択";
			this.Load += new System.EventHandler(this.HistoryViewer_Load);
			this.Controls.SetChildIndex(this.btnDispAll, 0);
			this.Controls.SetChildIndex(this.MsgArea, 0);
			this.Controls.SetChildIndex(this.btnCancel, 0);
			this.Controls.SetChildIndex(this.btnOK, 0);
			this.Controls.SetChildIndex(this.btnClear, 0);
			this.Controls.SetChildIndex(this.historyList, 0);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// 画面初期表示時処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HistoryViewer_Load(object sender, System.EventArgs e)
		{
			// 一旦履歴一覧は全てクリア
			this.historyList.Clear();
			// 表示するカラムのセット
			if( pIsShowTable == true )
			{
				this.historyList.Columns.Add("オブジェクト",120,HorizontalAlignment.Left);
			}
			this.historyList.Columns.Add("履歴",this.historyList.Width - 120 - 4,HorizontalAlignment.Left);

			// 一覧の内容をセットしていく
			if( this.TextHistoryDataSetDS != null && this.TextHistoryDataSetDS.TextHistoryDataSets.Rows.Count != 0 )
			{
				this.historyList.BeginUpdate();

				// まずは、同じオブジェクト名のものを優先して表示する
				DataRow []drl = this.TextHistoryDataSetDS.TextHistoryDataSets.Select(string.Format(System.Globalization.CultureInfo.CurrentCulture,"KeyValue = '{0}'",this.targetTable),
					"KeyNo desc");
				ListViewItem item ;
				for( int i = 0 ; i < drl.Length; i++ )
				{
					if( pIsShowTable == true )
					{
						item = new ListViewItem(
							new string[] {
											 (string)drl[i]["KeyValue"],
											 (string)drl[i]["DataValue"]
										 }
							);
					}
					else
					{
						item = new ListViewItem(
							new string[] {
											 (string)drl[i]["DataValue"]
										 }
							);
					}
					this.historyList.Items.Add(item);
				}

				// 次に違うオブジェクトのものを表示
				drl = this.TextHistoryDataSetDS.TextHistoryDataSets.Select(string.Format(System.Globalization.CultureInfo.CurrentCulture,"KeyValue <> '{0}'",this.targetTable),
					"KeyNo desc");
				for( int i = 0 ; i < drl.Length; i++ )
				{
					if( pIsShowTable == true )
					{
						item = new ListViewItem(
							new string[] {
											 (string)drl[i]["KeyValue"],
											 (string)drl[i]["DataValue"]
										 }
							);
					}
					else
					{
						item = new ListViewItem(
							new string[] {
											 (string)drl[i]["DataValue"]
										 }
							);
					}
					this.historyList.Items.Add(item);
				}

				this.historyList.EndUpdate();

				this.historyList.Items[0].Selected = true;
			}
		}

		/// <summary>
		/// 履歴消去ボタン押下時処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnClear_Click(object sender, System.EventArgs e)
		{
			this.TextHistoryDataSetDS.TextHistoryDataSets.Rows.Clear();
			this.historyList.Clear();
		}


		/// <summary>
		/// 戻るボタン押下時処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		/// <summary>
		/// OKボタン押下時処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOK_Click(object sender, System.EventArgs e)
		{
			if( this.historyList.SelectedItems.Count > 0 )
			{
				// 選択項目があれば、それを戻す
				if( pIsShowTable == true )
				{
					this.pRetString = this.historyList.SelectedItems[0].SubItems[1].Text;
				}
				else
				{
					this.pRetString = this.historyList.SelectedItems[0].SubItems[0].Text;
				}
			}
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		/// <summary>
		/// ダブルクリックは選択と同一視する
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void historyList_DoubleClick(object sender, System.EventArgs e)
		{
			this.btnOK_Click(sender, e);
		}

		/// <summary>
		/// 拡大表示ボタン押下時処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDispAll_Click(object sender, System.EventArgs e)
		{
			if( this.historyList.SelectedItems.Count == 0 )
			{
				return;
			}
			// 選択項目の内容を拡大表示ダイアログを利用して表示させる。
			// ただし、読み取り専用
			ZoomDialog dlg = new ZoomDialog();
			dlg.IsDispOnly = true;
			if( pIsShowTable == true )
			{
				dlg.EditText = this.historyList.SelectedItems[0].SubItems[1].Text;
			}
			else
			{
				dlg.EditText = this.historyList.SelectedItems[0].SubItems[0].Text;
			}
			dlg.LableName = "履歴拡大表示";
			dlg.ShowDialog();
		}
	}
}

