using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace quickDBExplorer.Forms.Dialog
{
    /// <summary>
    /// ブックマークの管理を行うフォーム    
    /// </summary>
    internal partial class BookMarkEditForm : quickDBExplorer.quickDBExplorerBaseForm
    {
        //
        internal List<BookmarkInfo> ResultBookmark { get; private set; }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        internal BookMarkEditForm(List<BookmarkInfo> bookmark)
        {
            //this.ResultBookmark = new List<BookmarkInfo>();
            //this.ResultBookmark.AddRange(bookmark);
            this.ResultBookmark = bookmark;
            InitializeComponent();
        }

        private void BookMarkEditForm_Load(object sender, EventArgs e)
        {
            // 一覧にブックマークをロードする
            LoadBookMark2List();
        }

        private void LoadBookMark2List()
        {
            this.bookmarkList.Items.Clear();
            foreach (BookmarkInfo each in this.ResultBookmark)
            {
                ListViewItem addItem = new ListViewItem(
                    new string[]{ 
                        each.Name,
                        each.DBName,
                        Array2String(each.Schema,":"),
                        ObjectList2String(each.Objects,":")});
                addItem.Tag = each;
                this.bookmarkList.Items.Add(addItem);
            }
        }

        private string Array2String(string[] baseArray, string separater)
        {
            StringBuilder schemaStr = new StringBuilder();
            foreach (string eachStr in baseArray)
            {
                if (schemaStr.Length != 0)
                {
                    schemaStr.Append(separater);
                }
                schemaStr.Append(eachStr);
            }
            return schemaStr.ToString();
        }
        private string ObjectList2String(List<DBObjectInfo> baseList, string separater)
        {
            StringBuilder schemaStr = new StringBuilder();
            foreach (DBObjectInfo each in baseList)
            {
                if (schemaStr.Length != 0)
                {
                    schemaStr.Append(separater);
                }
                schemaStr.Append(each.FormalName);
            }
            return schemaStr.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void bookmarkList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearDetail();
            if (this.bookmarkList.SelectedItems.Count == 0)
            {
                return;
            }
            Load2Detail((BookmarkInfo)this.bookmarkList.SelectedItems[0].Tag);
        }

        private void Load2Detail(BookmarkInfo bookmarkInfo)
        {
            this.txtDBname.Text = bookmarkInfo.DBName;
            this.txtBookmarkName.Text = bookmarkInfo.Name;
            foreach (string eachSchema in bookmarkInfo.Schema)
            {
                this.SchemaList.Items.Add(eachSchema);
            }
            foreach (DBObjectInfo eachObj in bookmarkInfo.Objects)
            {
                ListViewItem addItem = new ListViewItem(eachObj.FormalName);
                addItem.Tag = eachObj;
                this.ListObject.Items.Add(addItem);
            }
        }

        private void ClearDetail()
        {
            this.txtBookmarkName.Text = string.Empty;
            this.txtDBname.Text = string.Empty;
            this.SchemaList.Items.Clear();
            this.ListObject.Items.Clear();
        }

        private void btnAddSchema_Click(object sender, EventArgs e)
        {
            // TODO 
        }

        private void btnDelSchema_Click(object sender, EventArgs e)
        {
            DeleteSelectedSchema();
        }

        private void DeleteSelectedSchema()
        {
            if (this.SchemaList.SelectedItems.Count == 0)
            {
                return;
            }

            this.SuspendLayout();
            List<string> delSchema = new List<string>();
            foreach (string eachSchema in this.SchemaList.SelectedItems)
            {
                DeleteSchemaObjects(eachSchema);
                delSchema.Add(eachSchema);

            }
            foreach (string delEach in delSchema)
            {
                this.SchemaList.Items.Remove(delEach);
            }
            this.ResumeLayout();
        }

        private void DeleteSchemaObjects(string eachSchema)
        {
            List<ListViewItem> delList = new List<ListViewItem>();
            foreach (ListViewItem each in this.ListObject.Items)
            {
                DBObjectInfo eachValue = (DBObjectInfo)each.Tag;
                if (eachValue.Owner == eachSchema)
                {
                    delList.Add(each);
                }
            }
            foreach (ListViewItem delEach in delList)
            {
                this.ListObject.Items.Remove(delEach);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteBookmark();
        }

        private void DeleteBookmark()
        {
            if (this.bookmarkList.SelectedItems.Count == 0)
            {
                return;
            }
            if (MessageBox.Show("指定されたブックマークを削除してもよろしいですか？", "確認", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            this.SuspendLayout();
            ListViewItem[] delList = new ListViewItem[this.bookmarkList.SelectedItems.Count];
            this.bookmarkList.SelectedItems.CopyTo(delList,0);
            foreach (ListViewItem each in delList)
            {
                this.bookmarkList.Items.Remove(each);
                this.ResultBookmark.Remove((BookmarkInfo)each.Tag);
            }
            this.ResumeLayout();
        }

        private void txtBookmarkName_TextChanged(object sender, EventArgs e)
        {
            if (this.bookmarkList.SelectedItems.Count == 0)
            {
                return;
            }
            this.bookmarkList.SelectedItems[0].Text = this.txtBookmarkName.Text;
            ((BookmarkInfo)this.bookmarkList.SelectedItems[0].Tag).Name = this.txtBookmarkName.Text;
        }
    }
}
