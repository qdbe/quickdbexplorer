using System;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using System.IO;


namespace quickDBExplorer
{
	/// <summary>
	/// データグリッドの表示形式を管理する
	/// </summary>
	public class QdbeDataGridTextBoxColumn : DataGridTextBoxColumn
	{
		private CurrencyManager _sorce;
		private int				editrow;
		private bool	canSetEmptyString;
		private bool	isThisImage;
		private DataGrid parentdg = new DataGrid();

		/// <summary>
		/// 編集処理を中断する
		/// </summary>
		public void	CancelEdit()
		{
			this.HideEditBox();
		}

		/// <summary>
		/// 管理するデータがイメージか否かを取得・設定する
		/// </summary>
		public bool IsThisImage
		{
			get { return this.isThisImage; }
			set 
			{ 
				this.isThisImage  = value; 
				if( this.isThisImage == true )
				{
					this.ReadOnly = true;
					this.TextBox.Text = "";
					this.TextBox.ReadOnly = true;
				}
			}
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="pa">親となるグリッド</param>
		/// <param name="canset">空白文字列の設定が可能か否か</param>
		public QdbeDataGridTextBoxColumn(DataGrid pa, bool canset) : this(pa,canset,false)
		{
		}

		/// <summary>
		/// コンストラクタ
		/// データカラムに応じ、自動的に、
		/// 空白文字列の設定が可能か否か
		/// 管理するデータがイメージか否か
		/// を設定する
		/// </summary>
		/// <param name="pa">親となるグリッド</param>
		/// <param name="col">結合させるデータカラム</param>
		public QdbeDataGridTextBoxColumn(DataGrid pa, DataColumn col) : this(pa,false,false)
		{
			if( col.DataType.FullName == "System.String" )
			{
				this.canSetEmptyString = true;
				this.IsThisImage = false;
			}
			else if( col.DataType.FullName == "System.Byte[]" )
			{
				this.canSetEmptyString = false;
				this.IsThisImage = true;
			}
			else
			{
				this.canSetEmptyString = false;
				this.IsThisImage = false;
			}
			//マップ名を指定する
			this.MappingName = col.ColumnName;
			if( col.AllowDBNull == true )
			{
				this.HeaderText = "★"+col.ColumnName;
			}
			else
			{
				this.HeaderText = col.ColumnName;
			}
		}


		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="pa">親となるグリッド</param>
		/// <param name="canset">空白文字列の設定が可能か否か</param>
		/// <param name="isImage">管理するデータがイメージか否か</param>
		public QdbeDataGridTextBoxColumn(DataGrid pa, bool canset, bool isImage)
		{
			this.NullText = "";
			this.TextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GridKeyDownControler);
			this.canSetEmptyString = canset;
			this.IsThisImage = isImage;
			this.TextBox.Enter += new EventHandler(this.GridTextControler);
			this.TextBox.GotFocus += new EventHandler(this.GridTextControler);
			this.TextBox.TextChanged += new EventHandler(this.GridTextControler);
			this.parentdg = pa;
		}

		/// <summary>
		/// TextBoxへのイベントハンドラ
		/// </summary>
		/// <param name="sender">--</param>
		/// <param name="e">--</param>
		private void GridTextControler(object sender, EventArgs e)
		{
			if( isThisImage == true )
			{
				((TextBox)sender).Text = "";
			}
		}


		/// <summary>
		/// グリッド上でのキーダウンイベント処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GridKeyDownControler(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			// Ctrl+3 で値の編集ダイアログを表示
			if(	e.KeyCode == Keys.D3 &&
				e.Control == true &&
				e.Alt != true &&
				e.Shift != true )
			{
				// バイナリの場合、イメージ表示を行ってみる
				Object obj = GetColumnValueAtRow(this._sorce, this.editrow );
				if( obj is byte[] )
				{
					MemoryStream ms = new MemoryStream((byte[])obj);
					try
					{
						Image gazo = Image.FromStream(ms);
						if( gazo != null )
						{
							ImageViewer viewdlg = new ImageViewer();
							viewdlg.ViewImage = gazo;
							viewdlg.ShowDialog();
							return;
						}
					}
					catch
					{
						return;
					}
				}


				// 画像以外の場合、拡大表示ダイアログで値を表示させる
				ZoomDialog dlg  = new ZoomDialog();
				dlg.EditText = this.TextBox.Text;
				if( this.TextBox.ReadOnly == true )
				{
					dlg.IsDispOnly = true;
					dlg.LableName = "値参照";
					dlg.ShowDialog();
				}
				else
				{
					dlg.LableName = "値編集";
					if( dlg.ShowDialog() == DialogResult.OK &&
						dlg.EditText != "" )
					{
						this.TextBox.Text = dlg.EditText;
						SetColumnValueAtRow(this._sorce, this.editrow, dlg.EditText);
					}
				}
			}

			// 参照のみの場合、これ以降の処理は行う必要なし。
			if( this.parentdg.ReadOnly == true )
			{
				e.Handled = true;
				return;
			}

			// CTRL+1でNULL値の入力
			if( e.KeyCode == Keys.D1 &&
				e.Control == true &&
				e.Alt != true &&
				e.Shift != true )
			{
				EnterNullValue();
			}
			if( this.TextBox.ReadOnly == true )
			{
				e.Handled = true;
				return;
			}
			// CTRL+2 で空白文字列の入力
			if( canSetEmptyString == true&&
				e.KeyCode == Keys.D2 &&
				e.Control == true &&
				e.Alt != true &&
				e.Shift != true )
			{
				this.TextBox.Text = this.NullText;
				SetColumnValueAtRow(this._sorce, this.editrow, "");
			}
		}

		/// <summary>
		/// 編集開始時処理のオーバーライド
		/// </summary>
		/// <param name="source"></param>
		/// <param name="rowNum"></param>
		/// <param name="bounds"></param>
		/// <param name="readOnly"></param>
		/// <param name="instantText"></param>
		/// <param name="cellIsVisible"></param>
		protected override void Edit(CurrencyManager source,
			int rowNum, Rectangle bounds, bool readOnly,
			string instantText, bool cellIsVisible)
		{
			this._sorce = source;
			this.editrow = rowNum;
			if( this.isThisImage == true )
			{
				//this.TextBox.Text = "";
				base.Edit(source, rowNum, bounds, readOnly, instantText, cellIsVisible);
			}
			else
			{
				base.Edit(source, rowNum, bounds, readOnly, instantText, cellIsVisible);
			}
		}

		/// <summary>
		/// NULL値入力処理のオーバーライド
		/// </summary>
		protected override void EnterNullValue()
		{
			this.TextBox.Text = this.NullText;
			SetColumnValueAtRow(this._sorce, this.editrow, DBNull.Value);
		}

		/// <summary>
		/// Paintメソッドをオーバーライドする
		/// </summary>
		/// <param name="g"></param>
		/// <param name="bounds"></param>
		/// <param name="source"></param>
		/// <param name="rowNum"></param>
		/// <param name="backBrush"></param>
		/// <param name="foreBrush"></param>
		/// <param name="alignToRight"></param>
		protected override void Paint(Graphics g,
			Rectangle bounds,
			CurrencyManager source,
			int rowNum, 
			Brush backBrush,
			Brush foreBrush,
			bool alignToRight)
		{
			//セルの値を取得する
			object cellValue =
				this.GetColumnValueAtRow(source, rowNum);
			if (cellValue == DBNull.Value)
			{
				// NULLの場合は水色に着色
				backBrush = new SolidBrush(Color.FromArgb(0xbf,0xef,0xff));
			}
			if (cellValue is byte[])
			{
				// バイナリデータの場合、コバルトグリーンに着色
				backBrush = new SolidBrush(Color.FromArgb(0x10,0xC9,0x8D));
				g.FillRectangle(backBrush,bounds);
				return;
			}
			else if( cellValue is string && 
				( 
				((string)cellValue).IndexOf("\r\n") >= 0 ||
				((string)cellValue).IndexOf("\n") >= 0 ) )
			{
				// 文字列で複数行にわたる場合、とき色に着色
				backBrush = new SolidBrush(Color.FromArgb(0xf4,0xb3,0xc2));
			}

			//基本クラスのPaintメソッドを呼び出す
			base.Paint(g, bounds, source, rowNum,
				backBrush, foreBrush, alignToRight);
		}
	}
}
