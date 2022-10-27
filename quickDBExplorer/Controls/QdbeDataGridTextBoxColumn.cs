using System;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using System.IO;
using System.Collections;
using System.Collections.Generic;


namespace quickDBExplorer
{
	/// <summary>
	/// データグリッドの表示形式を管理する
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public class QdbeDataGridTextBoxColumn : DataGridViewTextBoxColumn
	{
		//private CurrencyManager _source;
		//private int				editrow;
		private bool canSetEmptyString;
		private bool isThisImage;
		private DataGridView parentdg = new DataGridView();

		/// <summary>
		/// フィールド情報
		/// </summary>
		public DBFieldInfo FiledInfo { get; private set; }

		/// <summary>
		/// 既定値の幅
		/// </summary>
		public int DefalutWidth { get; private set; }

		private int maxlength = 0;
		/// <summary>
		/// 入力可能な最大文字数
		/// </summary>
		public int MaxLength
		{
			get { return maxlength; }
			private set { this.maxlength = value; }
		}

		/// <summary>
		/// 空文字列を設定できるか否か
		/// </summary>
		public bool CanSetEmptyString
		{
			get { return this.canSetEmptyString; }
			private set { this.canSetEmptyString = value; }
		}

		/// <summary>
		/// NULLを設定できるか否か
		/// </summary>
		public bool CanSetNull { get; protected set; }

		/// <summary>
		/// 管理するデータがイメージか否かを取得・設定する
		/// </summary>
		public bool IsThisImage
		{
			get { return this.isThisImage; }
			set 
			{ 
				this.isThisImage  = value; 
				if(this.DataGridView != null && this.isThisImage == true )
				{
                    this.ReadOnly = true;
				}
			}
		}

		/// <summary>
		/// 既定のヘッダの背景色
		/// </summary>
		protected Color DefaultHeaderBackColor;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="pa">親となるグリッド</param>
		/// <param name="canset">空白文字列の設定が可能か否か</param>
		public QdbeDataGridTextBoxColumn(DataGridView pa, bool canset) : this(pa,canset,false)
		{
            this.maxlength = 0;
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
		public QdbeDataGridTextBoxColumn(DataGridView pa, DataColumn col) : this(pa,false,false)
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
			this.DataPropertyName = col.ColumnName;
			if( col.AllowDBNull == true )
			{
				this.HeaderText = "★"+col.ColumnName;
				this.CanSetNull = true;
			}
			else
			{
				bool isPKey = false;
				if (col.Table != null &&
					col.Table.PrimaryKey != null && 
					col.Table.PrimaryKey.Length != 0)
				{
					for (int j = 0; j < col.Table.PrimaryKey.Length; j++)
					{
						if (col.Table.PrimaryKey[j].ColumnName == col.ColumnName)
						{
							isPKey = true;
							break;
						}
					}

				}
				if (isPKey == true)
				{
					this.HeaderText = "※"+col.ColumnName;
				}
				else
				{
					this.HeaderText = col.ColumnName;
				}
				this.CanSetNull = false;
			}
			this.HeaderCell.Style.WrapMode = DataGridViewTriState.False;
		}

		/// <summary>
		/// コンストラクタ
		/// データカラムに応じ、自動的に、
		/// 空白文字列の設定が可能か否か
		/// 管理するデータがイメージか否か
		/// を設定する
		/// また、データカラムに応じ、自動的に書式を設定する
		/// </summary>
		/// <param name="pa">親となるグリッド</param>
		/// <param name="col">結合させるデータカラム</param>
		/// <param name="setting">グリッド表示書式</param>
        /// <param name="finfo">フィールド情報</param>
		public QdbeDataGridTextBoxColumn(
			DataGridView pa, 
			DataColumn col,
			GridFormatSetting setting,
            DBFieldInfo finfo
			) : this(pa,col)
		{

			if ( col.DataType.FullName == "System.Int32" ||
				col.DataType.FullName == "System.Int16" ||
				col.DataType.FullName == "System.Int64" ||
				col.DataType.FullName == "System.UInt32" ||
				col.DataType.FullName == "System.UInt16" ||
				col.DataType.FullName == "System.UInt64"  )
			{
				this.DefaultCellStyle.Format = setting.GridNumberFormatPlain;
			}
			if( col.DataType.FullName == "System.Double" ||
				col.DataType.FullName == "System.Single" || 
				col.DataType.FullName == "System.Decimal")
			{
				this.DefaultCellStyle.Format = setting.GridFloatFormatPlain;
			}
			if( col.DataType.FullName == "System.DateTime" )
			{
				this.DefaultCellStyle.Format = setting.GridDateFormatPlain;
			}
            if (finfo != null)
            {
                this.maxlength = finfo.Length;
                if (this.maxlength < 0)
                {
                    this.maxlength = 0;
                }
            }
            FiledInfo = finfo;
			this.Tag = finfo;
		}


		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="pa">親となるグリッド</param>
		/// <param name="canset">空白文字列の設定が可能か否か</param>
		/// <param name="isImage">管理するデータがイメージか否か</param>
		public QdbeDataGridTextBoxColumn(DataGridView pa, bool canset, bool isImage)
		{
			// DataGridView.DefaultCellStyle.NullValue で設定すべき
			this.DefaultCellStyle.NullValue = string.Empty;
			// Mainform でEditingControlShowingにて対応
			this.canSetEmptyString = canset;
			this.IsThisImage = isImage;
			this.parentdg = pa;
			this.SortMode = DataGridViewColumnSortMode.Automatic;
			this.Width = 75;
			this.DefalutWidth = this.Width;
			this.DefaultHeaderBackColor = this.HeaderCell.Style.BackColor;
		}


		/// <summary>
		/// 幅を既定値に戻す
		/// </summary>
		public void ResetWidth2Defalt()
		{
			this.Width = this.DefalutWidth;
		}

		/// <summary>
		/// 固定列の色に変更する
		/// </summary>
		public void SetFreezeColor()
		{
			// # c1e4e9 = 白藍しらあい
			this.HeaderCell.Style.BackColor = Color.FromArgb(0xc1, 0xe4, 0xe9);
		}

		/// <summary>
		/// 通常列の色に変更する
		/// </summary>
		public void UnsetFreezeColor()
		{
			this.HeaderCell.Style.BackColor = this.DefaultHeaderBackColor;
		}

		/// <summary>
		/// 値をチェックする
		/// </summary>
		/// <param name="val"></param>
		/// <param name="errmsg"></param>
		/// <returns></returns>
		public bool CheckValue(object val, out string errmsg)
		{
			errmsg = string.Empty;
			if (!this.CanSetNull)
			{
				if (val == null || val == DBNull.Value)
				{
					return false;
				}
			}
			if (this.FiledInfo.CheckValue(val, out errmsg))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
