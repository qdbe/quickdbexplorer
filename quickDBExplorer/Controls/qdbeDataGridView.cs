using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace quickDBExplorer.Controls
{
    /// <summary>
    /// 独自DataGridView
    /// </summary>
    public class qdbeDataGridView : System.Windows.Forms.DataGridView
    {
        /// <summary>
        /// サーバー毎設定
        /// </summary>
        public ServerJsonData ServerData { get; set; }

        /// <summary>
        /// 表示対象にしていてるテーブル名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 既定の行の高さ
        /// </summary>
        public int DefaultRowHeight { get; protected set; }

        /// <summary>
        /// 既定の行ヘッダの幅
        /// </summary>
        public int DefaultRowHeaderWidth { get; protected set; }

        /// <summary>
        /// 既定の見出しの高さ
        /// </summary>
        public int DefaultColumnHeaderHeight { get; protected set; }

        /// <summary>
        /// データロード時に発生するイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void LoadDataEventHandler(
            object sender,
            qdbeLoadDataEventArgs e
        );

        /// <summary>
        /// メニューでファイルからのデータ読み込み時に発生するイベント
        /// </summary>
        public event LoadDataEventHandler LoadData = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public qdbeDataGridView() : base()
        {
            this.BackColor = System.Drawing.Color.White;
            this.AllowUserToResizeRows = true;
            this.AllowUserToOrderColumns = true;
            this.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.None;
            this.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            //this.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystrokeOrF2;
            this.AutoGenerateColumns = false;
            this.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            this.DefaultCellStyle.NullValue = string.Empty;
            this.DefaultCellStyle.DataSourceNullValue = DBNull.Value;
            this.RowHeadersWidth = 40;
            this.DefaultRowHeaderWidth = this.RowHeadersWidth;
            this.RowTemplate.Height = 15;
            this.DefaultRowHeight = this.RowTemplate.Height;
            this.ColumnHeadersHeight = 17;
            this.DefaultColumnHeaderHeight = this.ColumnHeadersHeight;
            this.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            this.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
            this.AllowUserToDeleteRows = true;
            this.ShowCellToolTips = false;

            this.EditingControlShowing += DbGrid_EditingControlShowing;
            this.CellFormatting += DbGrid_CellFormatting;
            this.ColumnDividerDoubleClick += DbGrid_ColumnDividerDoubleClick; ;
            this.ColumnHeaderMouseDoubleClick += DbGrid_ColumnHeaderMouseDoubleClick; ;
            this.ColumnHeaderMouseClick += DbGrid_ColumnHeaderMouseClick; ;
            this.ColumnWidthChanged += DbGrid_ColumnWidthChanged;
            this.Leave += new System.EventHandler(this.dbGrid_Leave);
            this.KeyDown += QdbeDataGridView_KeyDown;
            //this.DataError += QdbeDataGridView_DataError;
            this.RowPostPaint += QdbeDataGridView_RowPostPaint;
            this.CellClick += QdbeDataGridView_CellClick;
            //this.CellValidating += QdbeDataGridView_CellValidating;
            this.RowValidating += QdbeDataGridView_RowValidating;

            this.DoubleBuffered = true;

            //this.CurrentCellChanged += QdbeDataGridView_CurrentCellChanged;

            InitializeConponents();

        }

        private void QdbeDataGridView_CurrentCellChanged(object sender, EventArgs e)
        {
            
        }

        private void QdbeDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (this.DataSource == null)
            {
                return;
            }

            //新しい行でなく、行の内容が変更されている時だけ検証する
            if (e.RowIndex == this.NewRowIndex || !this.IsCurrentRowDirty)
            {
                return;
            }

            QdbeDataGridTextBoxColumn gcol = this.GetColumn(e.ColumnIndex);
            int row = e.RowIndex;
            DataGridViewTextBoxCell cell = (DataGridViewTextBoxCell)this[e.ColumnIndex, row];
            object val = cell.Value;
            string errmsg = "";
            if (!gcol.CheckValue(val, out errmsg))
            {
                e.Cancel = true;
                MessageBox.Show(string.Format("{0}行目, {1} の値が不正です({2})", row + 1, gcol.FiledInfo.Name, errmsg));
            }
        }

        private void QdbeDataGridView_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (this.DataSource == null)
            {
                return;
            }

            //新しい行でなく、行の内容が変更されている時だけ検証する
            if (e.RowIndex == this.NewRowIndex || !this.IsCurrentRowDirty)
            {
                return;
            }

            StringBuilder sb = new StringBuilder();
            for (int col = 0; col < this.ColumnCount; col++)
            {
                QdbeDataGridTextBoxColumn gcol = this.GetColumn(col);
                int row = e.RowIndex;
                DataGridViewTextBoxCell cell = (DataGridViewTextBoxCell)this[col, row];
                object val = cell.Value;
                string errmsg = "";
                if (!gcol.CheckValue(val, out errmsg))
                {
                    e.Cancel = true;
                    sb.AppendFormat("{0}行目, {1} の値が不正です({2})", row + 1, gcol.FiledInfo.Name, errmsg);
                    sb.AppendLine("");
                }
            }
            if (e.Cancel == true)
            {
                MessageBox.Show(sb.ToString());
            }
        }

        /// <summary>
        /// 表示フォントを設定する
        /// </summary>
        /// <param name="setting"></param>
        public void SetDisplayFont(GridFormatSetting setting)
        {
            this.Font = setting.GridFont;
            this.ForeColor = setting.GridForeColor;

            this.AutoResizeColumnHeadersHeight();
        }

        /// <summary>
        /// 行ヘッダクリック時でもセルが編集状態のままであることへの対応
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QdbeDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0)
            {
                // 行ヘッダクリック時には EditingMode を変更して行選択のみ可能にする
                // RowHeader
                this.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
                this.EndEdit();
            }
            else
            {
                // それ以外はセルクリックでエンター
                this.EditMode = DataGridViewEditMode.EditOnEnter;
                this.BeginEdit(false);
            }
        }


        private void QdbeDataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var rowIdx = (e.RowIndex + 1).ToString();

            var centerFormat = new StringFormat()
            {
                // right alignment might actually make more sense for numbers
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, this.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }

        /// <summary>
        /// メニューデータ
        /// </summary>
        private class gridMenuData
        {
            public string Text { get; set; }
            public EventHandler click { get; set; }

            public string Name { get; set; }
        }

        private void InitializeConponents()
        {
            List<gridMenuData> menuList = new List<gridMenuData>();
            menuList.Add(new gridMenuData { Text = "全ての行をクリップボードにコピー", click = copyDbGridMenu_Click, Name = "copyDbGridMenu" }); 
            menuList.Add(new gridMenuData { Text = "選択行のみクリップボードにコピー", click = copySelectedDbGridMenu_Click, Name = "copySelectedDbGridMenu" });
            menuList.Add(new gridMenuData { Text = "全行選択", click = allSelectDbGridMenu_Click, Name = "allSelectDbGridMenu" });
            menuList.Add(new gridMenuData { Text = "全行選択解除", click = allUnSelectDbGridMenu_Click, Name = "allUnSelectDbGridMenu" });
            menuList.Add(new gridMenuData { Text = "データ取込(CSV)", click = readCsvDbGridMenu_Click, Name = "readCsvDbGridMenu" });
            menuList.Add(new gridMenuData { Text = "データ取込(CSV)(\"付き)", click = readCsvDQDbGridMenu_Click, Name = "readCsvDQDbGridMenu" });
            menuList.Add(new gridMenuData { Text = "データ取込(TAB)", click = readTsvDbGridMenu_Click, Name = "readTsvDbGridMenu" });
            menuList.Add(new gridMenuData { Text = "データ取込(TAB)(\"付き)", click = readTsvDQDbGridMenu_Click, Name = "readTsvDQDbGridMenu" });
            menuList.Add(new gridMenuData { Text = "このカラムまでを固定表示にする", click = SetFreezeMenu_Click, Name = "setFreezeMenu" });   // 列見出しのみ
            menuList.Add(new gridMenuData { Text = "カラムの固定表示を解除", click = UnsetFreezeMenu_Click, Name = "unsetFreezeMenu" }); // 列見出しのみ
            menuList.Add(new gridMenuData { Text = "選択行を削除", click = DeleteGridRow_Click, Name = "deleteSelectedRow" });　// 行見出しのみ
            menuList.Add(new gridMenuData { Text = "列幅を初期化", click = SetColumn2DefaultWidth, Name = "SetColumn2DefaultWidth" }); // 列見出しのみ
            menuList.Add(new gridMenuData { Text = "列を全表示", click = SetColumn2FullWidth, Name = "SetColumn2FullWidth" }); // 列見出しのみ

            ContextMenuStrip dbGridContextMenuStrip = new ContextMenuStrip();

            foreach (gridMenuData each in menuList)
            {
                ToolStripMenuItem add = new ToolStripMenuItem();
                add.Text = each.Text;
                add.Click += each.click;
                add.Name = each.Name;

                dbGridContextMenuStrip.Items.Add(add);
            }

            this.CellContextMenuStripNeeded += QdbeDataGridView_CellContextMenuStripNeeded;

            this.ContextMenuStrip = dbGridContextMenuStrip;

        }

        /// <summary>
        /// データエラー時にメッセージを表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QdbeDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
            QdbeDataGridTextBoxColumn col = this.GetColumn(e.ColumnIndex);

            MessageBox.Show(string.Format("{0}行目, {1} の値が不正です", e.RowIndex + 1, col.FiledInfo.Name));
        }


        /// <summary>
        /// カラムを取得する
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        protected QdbeDataGridTextBoxColumn GetColumn(int col)
        {
            return (QdbeDataGridTextBoxColumn)this.Columns[col];
        }



        /// <summary>
        /// 読み取り専用か否かを設定する
        /// </summary>
        /// <param name="isReadOnly"></param>
        public void SetReadOnly(bool isReadOnly)
        {
            this.ReadOnly = isReadOnly;
            this.AllowUserToAddRows = !isReadOnly;
        }

        /// <summary>
        /// 全ての行を選択状態にする
        /// </summary>
        /// <param name="isSet"></param>
        public void SetAllSelect(bool isSet)
        {
            if (this.Visible == false)
            {
                return;
            }
            if (this.DataSource == null)
            {
                return;
            }
            int maxCnt = this.Rows.Count;
            this.SuspendLayout();
            for (int i = 0; i < maxCnt; i++)
            {
                this.Rows[i].Selected = isSet;
            }
            this.ResumeLayout();
        }

        /// <summary>
        /// カラムを設定する
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dt"></param>
        /// <param name="svdata"></param>
        /// <param name="dboInfo"></param>
        public void SetColumns(string tableName, DataTable dt, ServerJsonData svdata, DBObjectInfo dboInfo)
        {
            QdbeDataGridTextBoxColumn cs;
            this.TableName = tableName;
            this.ServerData = svdata;
            foreach (DataColumn col in dt.Columns)
            {
                //列スタイルにQdbeDataGridTextBoxColumnを使う
                cs = new QdbeDataGridTextBoxColumn(this, col,
                    svdata.GridSetting,
                    dboInfo == null ? null : dboInfo[col.ColumnName]
                    );

                if (svdata.PerTableColumnWidth.ContainsKey(tableName))
                {
                    if (!string.IsNullOrEmpty(cs.DataPropertyName))
                    {
                        if (svdata.PerTableColumnWidth[tableName].ContainsKey(cs.DataPropertyName))
                        {
                            cs.Width = svdata.PerTableColumnWidth[tableName][cs.DataPropertyName];
                        }
                    }
                }
                this.Columns.Add(cs);
            }

            if (svdata.PerTableHeaderSize.ContainsKey(tableName))
            {
                //列見出しの高さ
                if (svdata.PerTableHeaderSize[tableName].ContainsKey("ColumnHeadersHeight"))
                {
                    this.ColumnHeadersHeight = svdata.PerTableHeaderSize[tableName]["ColumnHeadersHeight"];
                }
                // 行見出しの幅
                if (svdata.PerTableHeaderSize[tableName].ContainsKey("RowHeadersWidth"))
                {
                    this.RowHeadersWidth = svdata.PerTableHeaderSize[tableName]["RowHeadersWidth"];
                }
            }
        }
        /// <summary>
        /// フォーマット文字列を取得する
        /// </summary>
        /// <param name="fstr">基の表示書式</param>
        /// <returns>表示書式文字列</returns>
        protected string GetFormat(string fstr)
        {
            if (fstr == null)
            {
                return "";
            }
            int termp = fstr.IndexOf("	");
            if (termp == -1)
            {
                return fstr;
            }
            return fstr.Substring(0, termp);
        }

        private void DbGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.DataSource == null)
            {
                return;
            }
            SetCellColor(e.ColumnIndex, e.RowIndex, e);
        }


        /// <summary>
        /// セルの背景色を変更する
        /// </summary>
        /// <param name="colIdx"></param>
        /// <param name="rowIdx"></param>
        /// <param name="e">イベント</param>
        protected void SetCellColor(int colIdx, int rowIdx, DataGridViewCellFormattingEventArgs e)
        {
            //セルの値を取得する
            try
            {
                DataGridViewTextBoxCell cell = (DataGridViewTextBoxCell)this[colIdx, rowIdx];

                object cellValue = cell.Value;

                if (cellValue == DBNull.Value)
                {
                    // NULLの場合は水色に着色
                    cell.Style.BackColor = Color.FromArgb(0xbf, 0xef, 0xff);
                    //isBacknew = true;
                }
                else if (cellValue is byte[])
                {
                    // バイナリデータの場合、コバルトグリーンに着色
                    cell.Style.BackColor = Color.FromArgb(0x10, 0xC9, 0x8D);
                    //isBacknew = true;
                    e.Value = "";
                    e.FormattingApplied = true;
                    return;
                }
                else if (cellValue is string &&
                    (
                    (cellValue as string).IndexOf("\r\n", StringComparison.CurrentCulture) >= 0 ||
                    (cellValue as string).IndexOf("\n", StringComparison.CurrentCulture) >= 0))
                {
                    // 文字列で複数行にわたる場合、とき色に着色
                    cell.Style.BackColor = Color.FromArgb(0xf4, 0xb3, 0xc2);
                    //isBacknew = true;
                }
                else
                {
                    if (cell.Style.BackColor != Color.White)
                    {
                        cell.Style.BackColor = Color.White;
                    }
                }
            }
            finally
            {
            }
        }
        private void DbGrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridViewTextBoxEditingControl editingControl = (DataGridViewTextBoxEditingControl)e.Control;
            if (editingControl != null)
            {
                editingControl.KeyDown -= GridKeyDownControler;
                editingControl.PreviewKeyDown -= EditingControl_PreviewKeyDown;
            }
            if (e.CellStyle != null && e.CellStyle.Tag != null && (int)e.CellStyle.Tag == 1)
            {
                return;
            }
            if (e.CellStyle == null)
            {
                e.CellStyle = new DataGridViewCellStyle();
            }
            e.CellStyle.Tag = 1;



            editingControl.KeyDown += GridKeyDownControler;
            editingControl.PreviewKeyDown += EditingControl_PreviewKeyDown;
        }

        private void EditingControl_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.C))
            {
                e.IsInputKey = true;
                //System.Diagnostics.Debug.WriteLine("e.IsInputKey = true;");
            }
        }

        private void QdbeDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (!this.Focused)
            {
                return;
            }
            if (this.IsCurrentCellInEditMode && !this.ReadOnly)
            {
                // 編集中は
                //Console.WriteLine("IsCurrentCellInEditMode=true");
                return;
            }
            //Console.WriteLine("IsCurrentCellInEditMode=false");
            GridKeyDownControler(sender, e);
        }

        /// <summary>
        /// グリッド上でのキーダウンイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridKeyDownControler(object sender, System.Windows.Forms.KeyEventArgs e)
        {

            // 特殊キー押下のみであれば対処の必要なし
            if (e.KeyData == Keys.None ||
                e.KeyData == Keys.ControlKey ||
                e.KeyData == Keys.ShiftKey ||
                e.KeyData == Keys.Alt
                )
            {
                return;
            }

            DataGridViewCell curcell = this.CurrentCell;
            //if (curcell == null) return;
            Object obj = null;
            QdbeDataGridTextBoxColumn col = null;
            if (curcell != null)
            {
                col = (QdbeDataGridTextBoxColumn)this.Columns[curcell.ColumnIndex];
                obj = curcell.Value;
            }
            if (e.KeyData == (Keys.Control | Keys.C))
            {
                //System.Diagnostics.Debug.WriteLine("Ctrl+C");

                copyDbGrid(selectionMode.SELECTED_CELL,false);
                e.SuppressKeyPress = true;
                return;
            }
            //System.Diagnostics.Debug.WriteLine(e.ToString() + ":" + e.KeyCode.ToString() + ":" + e.Alt + ":" + e.Control + ":" + e.Shift);

            // Ctrl+3 で値の編集ダイアログを表示
            if ((e.KeyCode == Keys.D3 ||
                e.KeyCode == Keys.W) &&
                e.Control == true &&
                e.Alt != true &&
                e.Shift != true &&
                obj != null && 
                col != null )
            {
                // バイナリの場合、イメージ表示を行ってみる

                if ( obj is byte[] || (col.FiledInfo != null && col.FiledInfo.IsBinary))
                {
                    try
                    {
                        //Notice a subtlety in this code that is particular of the Northwind 
                        //database but has no relevance in general. 
                        //The original Access database was converted into SQL Server's Northwind database, 
                        //so the image field called Photo doesn't contain a true GIF file; 
                        //instead it contains the OLE object that Access builds to wrap any image. 
                        //As a result, the stream of bytes you read from the field is prefixed with a header 
                        //you must strip off to get the bits of the image. 
                        //Such a header is variable-length and also depends on the length of the originally imported file's name. 
                        //For Northwind, the length of this offset is 78 bytes.
                        //
                        int[] offsets = new int[] { 0, 78 };
                        int maxLen = (obj as byte[]).Length;
                        for (int i = 0; i < offsets.Length; i++)
                        {
                            if (maxLen < offsets[i])
                            {
                                continue;
                            }

                            MemoryStream ms = new MemoryStream(obj as byte[], offsets[i], maxLen - offsets[i]);
                            try
                            {
                                Image gazo = Image.FromStream(ms);
                                if (gazo != null)
                                {
                                    ImageViewer viewdlg = new ImageViewer(this.ReadOnly);
                                    viewdlg.ViewImage = gazo;
                                    if (viewdlg.ShowDialog(this) == DialogResult.OK)
                                    {
                                        curcell.Value = viewdlg.GetBytes();
                                        //this.SetColumnValueAtRow(this._source, this.editrow, viewdlg.GetBytes());
                                    }
                                    return;
                                }
                            }
                            catch
                            {
                                ;
                            }
                        }
                    }
                    catch
                    {
                        ;
                    }

                    // 画像以外
                    // バイナリ表示ダイアログを出す
                    // 表示は...

                    quickDBExplorer.Forms.Dialog.BinaryEditor bdlg = new quickDBExplorer.Forms.Dialog.BinaryEditor(obj as byte[], col.MaxLength, this.ReadOnly);
                    if (bdlg.ShowDialog(this) == DialogResult.OK)
                    {
                        curcell.Value = bdlg.CurrentData;
                        //this.SetColumnValueAtRow(this._source, this.editrow, bdlg.CurrentData);
                    }
                    return;
                }


                // 画像・バイナリ以外の場合、拡大表示ダイアログで値を表示させる
                ZoomDialog dlg = new ZoomDialog();
                dlg.EditText = (string)curcell.Value.ToString(); //this.TextBox.Text;
                if (curcell.ReadOnly == true)
                {
                    dlg.IsDispOnly = true;
                    dlg.LableName = "値参照";
                    dlg.ShowDialog(this);
                }
                else
                {
                    dlg.LableName = "値編集";
                    if (dlg.ShowDialog(this) == DialogResult.OK &&
                        dlg.EditText.Length != 0)
                    {
                        curcell.Value = dlg.EditText;
                    }
                }
            }

            // 参照のみの場合、これ以降の処理は行う必要なし。
            if (this.ReadOnly == true)
            {
                e.Handled = false;
                return;
            }



            // CTRL+1でNULL値の入力
            if (e.KeyCode == Keys.D1 &&
                e.Control == true &&
                e.Alt != true &&
                e.Shift != true &&
                col.CanSetNull == true &&
                curcell != null
                )
            {
                curcell.Value = null;
                this.InvalidateCell(curcell);
            }
            // CTRL+2 で空白文字列の入力
            if (col.CanSetEmptyString == true &&
                e.KeyCode == Keys.D2 &&
                e.Control == true &&
                e.Alt != true &&
                e.Shift != true &&
                curcell != null)
            {
                curcell.Value = string.Empty;
                this.InvalidateCell(curcell);
            }

            if (curcell != null &&
                curcell.IsInEditMode &&
                e.KeyCode == Keys.F2
                )
            {
                // F2 で選択状態を解除する
                TextBox tbox = this.EditingControl as TextBox;
                if (tbox != null)
                {
                    tbox.Select(0, 0);
                }
            }

            //if (e.KeyCode == Keys.Delete &&
            //    e.Control == false &&
            //    e.Alt == false &&
            //    e.Shift == false)
            //{
            //    if (this.SelectedRows.Count != 0)
            //    {
            //        this.CancelEdit();
            //    }
            //    foreach (DataGridViewRow r in this.SelectedRows)
            //    {
            //        if (!r.IsNewRow)
            //        {
            //            this.Rows.Remove(r);
            //        }
            //    }
            //}
        }


        private void DbGrid_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void DbGrid_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {

        }

        private void DbGrid_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void DbGrid_ColumnDividerDoubleClick(object sender, DataGridViewColumnDividerDoubleClickEventArgs e)
        {
            if (e.ColumnIndex < 0)
            {
                // 行ヘッダは自前で描画しているのでサイズ0になってしまう為無効にする
                e.Handled = true;
            }
        }
        private void dbGrid_Leave(object sender, EventArgs e)
        {
            if (this.DataSource != null)
            {
                foreach (QdbeDataGridTextBoxColumn col in this.Columns)
                {
                    if (!string.IsNullOrEmpty(col.DataPropertyName))
                    {
                        if (col.Width == col.DefalutWidth)
                        {
                            // 既定値と同じであればjsonファイルの容量圧縮の為、設定削除
                            if (this.ServerData.PerTableColumnWidth.ContainsKey(this.TableName))
                            {
                                if (this.ServerData.PerTableColumnWidth[this.TableName].ContainsKey(col.DataPropertyName))
                                {
                                    this.ServerData.PerTableColumnWidth[this.TableName].Remove(col.DataPropertyName);
                                }
                            }
                        }
                        else
                        {
                            if (!this.ServerData.PerTableColumnWidth.ContainsKey(this.TableName))
                            {
                                this.ServerData.PerTableColumnWidth[this.TableName] = new Dictionary<string, int>();
                            }
                            this.ServerData.PerTableColumnWidth[this.TableName][col.DataPropertyName] = col.Width;
                        }
                    }
                }
                if (!this.ServerData.PerTableHeaderSize.ContainsKey(this.TableName))
                {
                    this.ServerData.PerTableHeaderSize[this.TableName] = new Dictionary<string, int>();
                }
                this.ServerData.PerTableHeaderSize[this.TableName]["ColumnHeadersHeight"] = this.ColumnHeadersHeight;
                this.ServerData.PerTableHeaderSize[this.TableName]["RowHeadersWidth"] = this.RowHeadersWidth;
            }
        }

        /// <summary>
        /// すべての行をクリップボードにコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void copyDbGridMenu_Click(object sender, System.EventArgs e)
        {
            copyDbGrid(selectionMode.ALL);
        }

        /// <summary>
        /// 選択行のみクリップボードにコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void copySelectedDbGridMenu_Click(object sender, System.EventArgs e)
        {
            copyDbGrid(selectionMode.SELECTED_LINE);
        }

        /// <summary>
        /// 選択モード種別
        /// </summary>
        protected enum selectionMode { 
            /// <summary>
            /// 全選択
            /// </summary>
            ALL,
            /// <summary>
            /// 行選択
            /// </summary>
            SELECTED_LINE,
            /// <summary>
            /// セル選択
            /// </summary>
            SELECTED_CELL
        }

        /// <summary>
        /// 選択行のみクリップボードにコピーする
        /// </summary>
        /// <param name="copyHeader"></param>
        /// <param name="selMode"></param>
        private void copyDbGrid(selectionMode selMode, bool copyHeader = true)
        {
            if (this.Visible == false)
            {
                return;
            }
            if (this.DataSource == null)
            {
                return;
            }
            DataTable dt = null;
            if (this.DataSource is DataSet)
            {
                dt = ((DataSet)this.DataSource).Tables[0];
            }
            else if (this.DataSource is DataTable)
            {
                dt = (DataTable)this.DataSource;
            }
            if (dt == null ||
                dt.Rows.Count == 0)
            {
                return;
            }
            StringBuilder strline = new StringBuilder();
            StringBuilder strHeader = new StringBuilder();
            StringWriter wr = new StringWriter(strline, System.Globalization.CultureInfo.CurrentCulture);

            bool hasSetHeader = false;

            bool isMulitiSelect = this.SelectedCells.Count == 1 ? false : true;


            for (int y = 0; y < this.Rows.Count; y++)
            {
                if (selMode == selectionMode.SELECTED_LINE &&
                    !this.Rows[y].Selected)
                {
                    continue;
                }

                bool hasData = false;
                int colCnt = 0;
                for (int x = 0; x < dt.Columns.Count; x++)
                {
                    if (selMode == selectionMode.SELECTED_CELL &&
                        this[x, y].Selected == false)
                    {
                        continue;
                    }
                    // header 
                    if (hasSetHeader == false && copyHeader)
                    {
                        if (strHeader.Length != 0)
                        {
                            strHeader.Append("\t");
                        }
                        strHeader.Append(dt.Columns[x].ColumnName);
                    }
                    if (colCnt != 0)
                    {
                        wr.Write("\t");
                    }
                    if (this[x, y].Value != DBNull.Value &&
                        this[x, y].Value != null)
                    {
                        wr.Write(this[x, y].Value.ToString());
                    }
                    colCnt++;
                    hasData = true;
                }
                if (hasData)
                {
                    hasSetHeader = true;
                    if (isMulitiSelect|| copyHeader)
                    {
                        wr.Write(wr.NewLine);
                    }
                }
            }
            if (strHeader.Length > 0)
            {
                strHeader.AppendLine("");
            }
            Clipboard.SetDataObject(strHeader.ToString() + strline.ToString(), true);
        }

        private void allSelectDbGridMenu_Click(object sender, System.EventArgs e)
        {
            this.SetAllSelect(true);
        }

        private void allUnSelectDbGridMenu_Click(object sender, System.EventArgs e)
        {
            this.SetAllSelect(false);
        }


        /// <summary>
        /// データグリッドへの CSVの取込
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void readCsvDbGridMenu_Click(object sender, System.EventArgs e)
        {
            this.LoadFile2Grid(true, false);
        }

        private void readTsvDbGridMenu_Click(object sender, System.EventArgs e)
        {
            this.LoadFile2Grid(false, false);
        }

        private void readCsvDQDbGridMenu_Click(object sender, System.EventArgs e)
        {
            this.LoadFile2Grid(true, true);
        }

        private void readTsvDQDbGridMenu_Click(object sender, System.EventArgs e)
        {
            this.LoadFile2Grid(false, true);
        }

        /// <summary>
        /// ファイルからのデータ読み込み
        /// </summary>
        /// <param name="isCsv"></param>
        /// <param name="isUseDQ"></param>
        protected void LoadFile2Grid(bool isCsv, bool isUseDQ)
        {
            if (this.Visible != true)
            {
                return;
            }
            if (this.ReadOnly == true)
            {
                return;
            }
            if (this.DataSource == null)
            {
                return;
            }
            if (this.LoadData != null)
            {
                this.LoadData(this, new qdbeLoadDataEventArgs(isCsv, isUseDQ));
            }
        }

        /// <summary>
        /// カラム固定をなくす
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UnsetFreezeMenu_Click(object sender, EventArgs e)
        {
            for (int i= 0; i < this.Columns.Count; i++ ){
                this.Columns[i].Frozen = false;
                ((QdbeDataGridTextBoxColumn)this.Columns[i]).UnsetFreezeColor();
            }
        }

        /// <summary>
        /// カラム固定を行う
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetFreezeMenu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menu = (ToolStripMenuItem)sender;
            int maxcol = (int)menu.Tag;
            if (maxcol < 0) return;
            for (int i = 0; i < this.Columns.Count; i++)
            {
                this.Columns[i].Frozen = false;
                if (i <= maxcol)
                {
                    ((QdbeDataGridTextBoxColumn)this.Columns[i]).SetFreezeColor();
                }
                else
                {
                    ((QdbeDataGridTextBoxColumn)this.Columns[i]).UnsetFreezeColor();
                }
            }
            this.Columns[maxcol].Frozen = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DeleteGridRow_Click(object sender, EventArgs e)
        {
            if (this.SelectedRows.Count != 0)
            {
                this.CancelEdit();
            }
            foreach (DataGridViewRow r in this.SelectedRows)
            {
                if (!r.IsNewRow)
                {
                    this.Rows.Remove(r);
                }
            }
        }

        /// <summary>
        /// 特定のカラムの幅を初期化する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SetColumn2DefaultWidth(object sender, EventArgs e)
        {
            ToolStripMenuItem menu = (ToolStripMenuItem)sender;
            int colidx = (int)menu.Tag;
            if (colidx < 0) return;
            QdbeDataGridTextBoxColumn col = (QdbeDataGridTextBoxColumn)this.Columns[colidx];
            col.ResetWidth2Defalt();
        }

        /// <summary>
        /// 特定のカラムの幅を全表示にする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SetColumn2FullWidth(object sender, EventArgs e)
        {
            ToolStripMenuItem menu = (ToolStripMenuItem)sender;
            int colidx = (int)menu.Tag;
            if (colidx < 0) return;
            QdbeDataGridTextBoxColumn col = (QdbeDataGridTextBoxColumn)this.Columns[colidx];

            int width = col.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);

            if (width > 65536)
            {
                return;
            }
            col.Width = width;

            
        }


        /// <summary>
        /// メニュー表示時の要否を判断する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QdbeDataGridView_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            List<string> controlTarget = new List<string>();
            controlTarget.Add("readCsvDbGridMenu");
            controlTarget.Add("readCsvDQDbGridMenu");
            controlTarget.Add("readTsvDbGridMenu");
            controlTarget.Add("readTsvDQDbGridMenu");


            if (this.Visible == true &&
                this.ReadOnly == false &&
                this.DataSource != null)
            {
                // 編集可能
                foreach (ToolStripMenuItem each in this.ContextMenuStrip.Items)
                {
                    if (controlTarget.Contains(each.Name))
                    {
                        each.Visible = true;
                    }
                }
            }
            else
            {
                // 編集不可
                foreach (ToolStripMenuItem each in this.ContextMenuStrip.Items)
                {
                    if (controlTarget.Contains(each.Name))
                    {
                        each.Visible = false;
                    }
                }
            }

            List<string> colMenu = new List<string>();
            colMenu.Add("setFreezeMenu");
            colMenu.Add("unsetFreezeMenu");
            colMenu.Add("SetColumn2DefaultWidth");
            colMenu.Add("SetColumn2FullWidth");
            if (e.RowIndex < 0 && e.ColumnIndex >= 0)
            {
                foreach (ToolStripMenuItem each in this.ContextMenuStrip.Items)
                {
                    if (colMenu.Contains(each.Name))
                    {
                        each.Visible = true;
                        each.Tag = e.ColumnIndex;
                        QdbeDataGridTextBoxColumn col = (QdbeDataGridTextBoxColumn)this.Columns[e.ColumnIndex];
                        if (each.Name == "unsetFreezeMenu")
                        {
                            // どの列もFreezeになっていない
                            if (this.Columns[0].Frozen == false)
                            {
                                each.Visible = false;
                            }
                        }
                        else if (each.Name == "SetColumn2DefaultWidth")
                        {
                            if (col.Width == col.DefalutWidth)
                            {
                                each.Visible = false;
                            }
                        }
                        else if (each.Name == "SetColumn2FullWidth")
                        {
                            int width = col.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
                            if( col.Width == width)
                            {
                                each.Visible = false;
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (ToolStripMenuItem each in this.ContextMenuStrip.Items)
                {
                    if (colMenu.Contains(each.Name))
                    {
                        each.Visible = false;
                    }
                }
            }


            List<string> rowMenu = new List<string>();
            rowMenu.Add("deleteSelectedRow");

            // 選択行
            if (this.Visible == true &&
                this.ReadOnly == false &&
                this.DataSource != null && 
                e.ColumnIndex < 0 &&
                this.SelectedRows != null && 
                this.SelectedRows.Count != 0 
                )
            {
                foreach (ToolStripMenuItem each in this.ContextMenuStrip.Items)
                {
                    if (rowMenu.Contains(each.Name))
                    {
                        each.Visible = true;
                    }
                }
            }
            else
            {
                foreach (ToolStripMenuItem each in this.ContextMenuStrip.Items)
                {
                    if (rowMenu.Contains(each.Name))
                    {
                        each.Visible = false;
                    }
                }
            }
        }

        /// <summary>
        /// 列幅を初期化する
        /// </summary>
        public void ResetWidth2Default()
        {
            this.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            foreach (QdbeDataGridTextBoxColumn each in this.Columns)
            {
                each.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                each.ResetWidth2Defalt();
            }
            this.RowHeadersWidth = this.DefaultRowHeaderWidth;
        }

        /// <summary>
        /// 列を全セルが表示される幅に設定する
        /// </summary>
        public void SetWidth2Full()
        {
            //this.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            
            //this.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells, true);
            foreach (QdbeDataGridTextBoxColumn each in this.Columns)
            {
                each.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                //int width =  each.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
                //if (width > 65536)
                //{
                //    continue;
                //}
                //each.Width = width;
            }
        }

        /// <summary>
        /// 列を全セルが表示される幅に設定する
        /// </summary>
        public void SetHeight2Full()
        {
            this.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells,true);
            //foreach (DataGridViewRow each in this.Rows)
            //{
            //    int height = each.GetPreferredHeight(each.Index, DataGridViewAutoSizeRowMode.AllCellsExceptHeader, false);
            //    if (height > 65536)
            //    {
            //        continue;
            //    }
            //    each.Height = height;
            //}
        }

        /// <summary>
        /// 高さを既定値に戻す
        /// </summary>
        public void ResetHight2Default()
        {
            foreach (DataGridViewRow each in this.Rows)
            {
                each.Height = this.DefaultRowHeight;
            }

            this.ColumnHeadersHeight = this.DefaultColumnHeaderHeight;
        }

        /// <summary>
        /// 列幅、行高さを既定値に戻す
        /// </summary>
        public void ResetWidthHeight2Defalt()
        {
            this.ResetWidth2Default();
            this.ResetHight2Default();
        }

        /// <summary>
        /// クリップボードにコピーする内容を取得する
        /// </summary>
        /// <returns></returns>
        public override DataObject GetClipboardContent()
        {
            //System.Diagnostics.Debug.WriteLine("GetClipboardContent");
            //元のDataObjectを取得する
            DataObject oldData = base.GetClipboardContent();
            if (oldData == null)
            {
                return new DataObject();
            }

            //新しいDataObjectを作成する
            DataObject newData = new DataObject();

            //テキスト形式のデータをセットする（UnicodeTextもセットされる）
            newData.SetData(DataFormats.Text, oldData.GetData(DataFormats.Text));

            //HTML形式のデータを取得する
            object htmlObj = oldData.GetData(DataFormats.Html);
            MemoryStream htmlStrm = null;
            if (htmlObj is string)
            {
                //String型の時は、MemoryStreamに変換する
                htmlStrm = new MemoryStream(
                    Encoding.UTF8.GetBytes((string)htmlObj));
            }
            else if (htmlObj is MemoryStream)
            {
                //.NET Framework 4.0以降では、MemoryStreamとなる
                htmlStrm = (MemoryStream)htmlObj;
            }
            if (htmlStrm != null)
            {
                //HTML形式のデータをセットする
                newData.SetData(DataFormats.Html, htmlStrm);
            }

            //CSV形式のデータを取得する
            object csvObj = oldData.GetData(DataFormats.CommaSeparatedValue);
            MemoryStream csvStrm = null;
            if (csvObj is string)
            {
                //MemoryStreamに変換する
                csvStrm = new MemoryStream(
                    Encoding.Default.GetBytes((string)csvObj));
            }
            else if (csvObj is MemoryStream)
            {
                //今のところこうなることはないが、将来を見据えて...
                csvStrm = (MemoryStream)csvObj;
            }
            if (csvStrm != null)
            {
                //CSV形式のデータをセットする
                newData.SetData(DataFormats.CommaSeparatedValue, csvStrm);
            }

            return newData;
        }
    }

    /// <summary>
    /// ファイルからのデータ読み込み時イベント引数
    /// </summary>
    public class qdbeLoadDataEventArgs
    {
        /// <summary>
        /// CSV形式か否か
        /// </summary>
        public bool IsCSV { get; protected set; }

        /// <summary>
        /// ダブルクォーテーションがあるか否か
        /// </summary>
        public bool IsUseDQ { get; protected set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="csv"></param>
        /// <param name="dq"></param>
        public qdbeLoadDataEventArgs(bool csv, bool dq)
        {
            this.IsCSV = csv;
            this.IsUseDQ = dq;
        }
    }
}
