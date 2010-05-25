namespace quickDBExplorer.Forms.Dialog
{
    partial class BookMarkEditForm
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.EditPanelContainer = new System.Windows.Forms.SplitContainer();
            this.ListPanel = new System.Windows.Forms.Panel();
            this.bookmarkList = new quickDBExplorer.qdbeListView();
            this.NameCol = new System.Windows.Forms.ColumnHeader();
            this.DBName = new System.Windows.Forms.ColumnHeader();
            this.Schema = new System.Windows.Forms.ColumnHeader();
            this.ObjectList = new System.Windows.Forms.ColumnHeader();
            this.infoPanel = new System.Windows.Forms.Panel();
            this.ListObject = new quickDBExplorer.qdbeListBox();
            this.SchemaList = new quickDBExplorer.qdbeListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBookmarkName = new System.Windows.Forms.TextBox();
            this.txtDBname = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.EditPanelContainer.Panel1.SuspendLayout();
            this.EditPanelContainer.Panel2.SuspendLayout();
            this.EditPanelContainer.SuspendLayout();
            this.ListPanel.SuspendLayout();
            this.infoPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MsgArea
            // 
            this.MsgArea.Location = new System.Drawing.Point(264, 479);
            this.MsgArea.Size = new System.Drawing.Size(392, 16);
            // 
            // EditPanelContainer
            // 
            this.EditPanelContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.EditPanelContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.EditPanelContainer.Location = new System.Drawing.Point(12, 12);
            this.EditPanelContainer.Name = "EditPanelContainer";
            // 
            // EditPanelContainer.Panel1
            // 
            this.EditPanelContainer.Panel1.Controls.Add(this.ListPanel);
            // 
            // EditPanelContainer.Panel2
            // 
            this.EditPanelContainer.Panel2.Controls.Add(this.infoPanel);
            this.EditPanelContainer.Size = new System.Drawing.Size(759, 451);
            this.EditPanelContainer.SplitterDistance = 353;
            this.EditPanelContainer.TabIndex = 0;
            // 
            // ListPanel
            // 
            this.ListPanel.Controls.Add(this.bookmarkList);
            this.ListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListPanel.Location = new System.Drawing.Point(0, 0);
            this.ListPanel.Name = "ListPanel";
            this.ListPanel.Size = new System.Drawing.Size(349, 447);
            this.ListPanel.TabIndex = 0;
            // 
            // bookmarkList
            // 
            this.bookmarkList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NameCol,
            this.DBName,
            this.Schema,
            this.ObjectList});
            this.bookmarkList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bookmarkList.IsAutoColumSort = false;
            this.bookmarkList.Location = new System.Drawing.Point(0, 0);
            this.bookmarkList.Name = "bookmarkList";
            this.bookmarkList.Size = new System.Drawing.Size(349, 447);
            this.bookmarkList.TabIndex = 0;
            this.bookmarkList.UseCompatibleStateImageBehavior = false;
            this.bookmarkList.View = System.Windows.Forms.View.Details;
            // 
            // NameCol
            // 
            this.NameCol.Text = "名称";
            // 
            // DBName
            // 
            this.DBName.Text = "データベース名";
            this.DBName.Width = 94;
            // 
            // Schema
            // 
            this.Schema.Text = "スキーマ";
            this.Schema.Width = 87;
            // 
            // ObjectList
            // 
            this.ObjectList.Text = "オブジェクト";
            this.ObjectList.Width = 108;
            // 
            // infoPanel
            // 
            this.infoPanel.Controls.Add(this.ListObject);
            this.infoPanel.Controls.Add(this.SchemaList);
            this.infoPanel.Controls.Add(this.label4);
            this.infoPanel.Controls.Add(this.label3);
            this.infoPanel.Controls.Add(this.label1);
            this.infoPanel.Controls.Add(this.label2);
            this.infoPanel.Controls.Add(this.txtBookmarkName);
            this.infoPanel.Controls.Add(this.txtDBname);
            this.infoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoPanel.Location = new System.Drawing.Point(0, 0);
            this.infoPanel.Name = "infoPanel";
            this.infoPanel.Size = new System.Drawing.Size(398, 447);
            this.infoPanel.TabIndex = 1;
            // 
            // ListObject
            // 
            this.ListObject.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ListObject.FormattingEnabled = true;
            this.ListObject.ItemHeight = 12;
            this.ListObject.Location = new System.Drawing.Point(102, 187);
            this.ListObject.Name = "ListObject";
            this.ListObject.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.ListObject.Size = new System.Drawing.Size(280, 244);
            this.ListObject.TabIndex = 3;
            // 
            // SchemaList
            // 
            this.SchemaList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SchemaList.FormattingEnabled = true;
            this.SchemaList.ItemHeight = 12;
            this.SchemaList.Location = new System.Drawing.Point(102, 75);
            this.SchemaList.Name = "SchemaList";
            this.SchemaList.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.SchemaList.Size = new System.Drawing.Size(280, 100);
            this.SchemaList.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "データベース名";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "ブックマーク名称";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 187);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "オブジェクト";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "スキーマ";
            // 
            // txtBookmarkName
            // 
            this.txtBookmarkName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBookmarkName.Location = new System.Drawing.Point(102, 13);
            this.txtBookmarkName.Name = "txtBookmarkName";
            this.txtBookmarkName.Size = new System.Drawing.Size(280, 19);
            this.txtBookmarkName.TabIndex = 0;
            // 
            // txtDBname
            // 
            this.txtDBname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDBname.Location = new System.Drawing.Point(102, 44);
            this.txtDBname.Name = "txtDBname";
            this.txtDBname.ReadOnly = true;
            this.txtDBname.Size = new System.Drawing.Size(280, 19);
            this.txtDBname.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(696, 474);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "閉じる(&X)";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(12, 474);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "決定(&O)";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Visible = false;
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Location = new System.Drawing.Point(183, 474);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "削除(&D)";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // BookMarkEditForm
            // 
            this.ClientSize = new System.Drawing.Size(783, 504);
            this.Controls.Add(this.EditPanelContainer);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Name = "BookMarkEditForm";
            this.Text = "ブックマーク管理";
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnDelete, 0);
            this.Controls.SetChildIndex(this.MsgArea, 0);
            this.Controls.SetChildIndex(this.EditPanelContainer, 0);
            this.EditPanelContainer.Panel1.ResumeLayout(false);
            this.EditPanelContainer.Panel2.ResumeLayout(false);
            this.EditPanelContainer.ResumeLayout(false);
            this.ListPanel.ResumeLayout(false);
            this.infoPanel.ResumeLayout(false);
            this.infoPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer EditPanelContainer;
        private System.Windows.Forms.Panel ListPanel;
        private qdbeListView bookmarkList;
        private System.Windows.Forms.ColumnHeader NameCol;
        private System.Windows.Forms.ColumnHeader DBName;
        private System.Windows.Forms.ColumnHeader Schema;
        private System.Windows.Forms.ColumnHeader ObjectList;
        private System.Windows.Forms.Panel infoPanel;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtDBname;
        private System.Windows.Forms.Button btnOK;
        private qdbeListBox SchemaList;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBookmarkName;
        private qdbeListBox ListObject;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDelete;
    }
}
