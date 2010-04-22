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
            this.grpServerList.SuspendLayout();
            this.SuspendLayout();
            // 
            // MsgArea
            // 
            this.MsgArea.Location = new System.Drawing.Point(94, 367);
            this.MsgArea.Size = new System.Drawing.Size(369, 32);
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
            this.sqlServerList.Location = new System.Drawing.Point(15, 18);
            this.sqlServerList.MultiSelect = false;
            this.sqlServerList.Name = "sqlServerList";
            this.sqlServerList.Size = new System.Drawing.Size(511, 318);
            this.sqlServerList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.sqlServerList.TabIndex = 1;
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
            this.btnOk.Location = new System.Drawing.Point(13, 376);
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
            this.btnCancel.Location = new System.Drawing.Point(469, 375);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 24);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "キャンセル(&X)";
            // 
            // grpServerList
            // 
            this.grpServerList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpServerList.Controls.Add(this.sqlServerList);
            this.grpServerList.Location = new System.Drawing.Point(13, 12);
            this.grpServerList.Name = "grpServerList";
            this.grpServerList.Size = new System.Drawing.Size(544, 347);
            this.grpServerList.TabIndex = 0;
            this.grpServerList.TabStop = false;
            this.grpServerList.Text = "SQL SERVER 一覧";
            // 
            // SqlServerSelector
            // 
            this.AcceptButton = this.btnOk;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(569, 402);
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
    }
}
