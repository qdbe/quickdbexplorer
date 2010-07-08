namespace quickDBExplorer
{
    partial class SqlServerSelector
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
            this.sqlServerList = new quickDBExplorer.qdbeListView();
            this.ServerNameCol = new System.Windows.Forms.ColumnHeader();
            this.InstanceNameCol = new System.Windows.Forms.ColumnHeader();
            this.IsClusteredCol = new System.Windows.Forms.ColumnHeader();
            this.VersionCol = new System.Windows.Forms.ColumnHeader();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpServerList = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.btnReload = new System.Windows.Forms.Button();
            this.grpServerList.SuspendLayout();
            this.SuspendLayout();
            // 
            // MsgArea
            // 
            this.MsgArea.Location = new System.Drawing.Point(94, 452);
            this.MsgArea.Size = new System.Drawing.Size(369, 32);
            this.MsgArea.TabIndex = 2;
            // 
            // sqlServerList
            // 
            this.sqlServerList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.sqlServerList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ServerNameCol,
            this.InstanceNameCol,
            this.IsClusteredCol,
            this.VersionCol});
            this.sqlServerList.FullRowSelect = true;
            this.sqlServerList.IsAutoColumSort = true;
            this.sqlServerList.Location = new System.Drawing.Point(15, 44);
            this.sqlServerList.MultiSelect = false;
            this.sqlServerList.Name = "sqlServerList";
            this.sqlServerList.Size = new System.Drawing.Size(511, 381);
            this.sqlServerList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.sqlServerList.TabIndex = 2;
            this.sqlServerList.UseCompatibleStateImageBehavior = false;
            this.sqlServerList.View = System.Windows.Forms.View.Details;
            this.sqlServerList.DoubleClick += new System.EventHandler(this.sqlServerList_DoubleClick);
            // 
            // ServerNameCol
            // 
            this.ServerNameCol.Text = "ホスト名";
            this.ServerNameCol.Width = 144;
            // 
            // InstanceNameCol
            // 
            this.InstanceNameCol.Text = "インスタンス名";
            this.InstanceNameCol.Width = 148;
            // 
            // IsClusteredCol
            // 
            this.IsClusteredCol.Text = "クラスター";
            this.IsClusteredCol.Width = 59;
            // 
            // VersionCol
            // 
            this.VersionCol.Text = "バージョン";
            this.VersionCol.Width = 148;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOk.Location = new System.Drawing.Point(13, 461);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "決定(&O)";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(469, 460);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 24);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "キャンセル(&X)";
            // 
            // grpServerList
            // 
            this.grpServerList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpServerList.Controls.Add(this.label1);
            this.grpServerList.Controls.Add(this.txtFilter);
            this.grpServerList.Controls.Add(this.btnReload);
            this.grpServerList.Controls.Add(this.sqlServerList);
            this.grpServerList.Location = new System.Drawing.Point(13, 8);
            this.grpServerList.Name = "grpServerList";
            this.grpServerList.Size = new System.Drawing.Size(544, 436);
            this.grpServerList.TabIndex = 0;
            this.grpServerList.TabStop = false;
            this.grpServerList.Text = "SQL SERVER 一覧";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(116, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "フィルタ(&F)";
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(185, 18);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(191, 19);
            this.txtFilter.TabIndex = 1;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // btnReload
            // 
            this.btnReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReload.Location = new System.Drawing.Point(452, 16);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(75, 23);
            this.btnReload.TabIndex = 3;
            this.btnReload.Text = "再検索(&R)";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // SqlServerSelector
            // 
            this.AcceptButton = this.btnOk;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(569, 487);
            this.Controls.Add(this.grpServerList);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Name = "SqlServerSelector";
            this.Text = "サーバー選択";
            this.Controls.SetChildIndex(this.MsgArea, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.grpServerList, 0);
            this.grpServerList.ResumeLayout(false);
            this.grpServerList.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private qdbeListView sqlServerList;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox grpServerList;
        private System.Windows.Forms.ColumnHeader ServerNameCol;
        private System.Windows.Forms.ColumnHeader InstanceNameCol;
        private System.Windows.Forms.ColumnHeader IsClusteredCol;
        private System.Windows.Forms.ColumnHeader VersionCol;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFilter;
    }
}
