namespace quickDBExplorer.Forms.Dialog
{
    partial class OuterToolEditForm
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.EditPanelContainer = new System.Windows.Forms.SplitContainer();
            this.ListPanel = new System.Windows.Forms.Panel();
            this.toolList = new quickDBExplorer.qdbeListView();
            this.NameColumn = new System.Windows.Forms.ColumnHeader();
            this.commandColumn = new System.Windows.Forms.ColumnHeader();
            this.argColumn = new System.Windows.Forms.ColumnHeader();
            this.label4 = new System.Windows.Forms.Label();
            this.btnInsertArgMacro = new System.Windows.Forms.Button();
            this.btnInsertCommandMacro = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnCommandRef = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.macroList = new quickDBExplorer.qdbeListView();
            this.macroName = new System.Windows.Forms.ColumnHeader();
            this.macroValue = new System.Windows.Forms.ColumnHeader();
            this.txtName = new quickDBExplorer.quickDBExplorerTextBox();
            this.txtArgs = new quickDBExplorer.quickDBExplorerTextBox();
            this.txtCommand = new quickDBExplorer.quickDBExplorerTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.EditPanelContainer.Panel1.SuspendLayout();
            this.EditPanelContainer.Panel2.SuspendLayout();
            this.EditPanelContainer.SuspendLayout();
            this.ListPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MsgArea
            // 
            this.MsgArea.Location = new System.Drawing.Point(163, 478);
            this.MsgArea.Size = new System.Drawing.Size(567, 20);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(764, 478);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "閉じる(&X)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(12, 477);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(72, 24);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "決定(&O)";
            this.btnOK.Visible = false;
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
            this.EditPanelContainer.Panel2.Controls.Add(this.label4);
            this.EditPanelContainer.Panel2.Controls.Add(this.btnInsertArgMacro);
            this.EditPanelContainer.Panel2.Controls.Add(this.btnInsertCommandMacro);
            this.EditPanelContainer.Panel2.Controls.Add(this.btnNew);
            this.EditPanelContainer.Panel2.Controls.Add(this.btnCommandRef);
            this.EditPanelContainer.Panel2.Controls.Add(this.btnUpdate);
            this.EditPanelContainer.Panel2.Controls.Add(this.macroList);
            this.EditPanelContainer.Panel2.Controls.Add(this.txtName);
            this.EditPanelContainer.Panel2.Controls.Add(this.txtArgs);
            this.EditPanelContainer.Panel2.Controls.Add(this.txtCommand);
            this.EditPanelContainer.Panel2.Controls.Add(this.label3);
            this.EditPanelContainer.Panel2.Controls.Add(this.label1);
            this.EditPanelContainer.Panel2.Controls.Add(this.label2);
            this.EditPanelContainer.Size = new System.Drawing.Size(827, 459);
            this.EditPanelContainer.SplitterDistance = 359;
            this.EditPanelContainer.TabIndex = 0;
            // 
            // ListPanel
            // 
            this.ListPanel.Controls.Add(this.toolList);
            this.ListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListPanel.Location = new System.Drawing.Point(0, 0);
            this.ListPanel.Name = "ListPanel";
            this.ListPanel.Size = new System.Drawing.Size(355, 455);
            this.ListPanel.TabIndex = 0;
            // 
            // toolList
            // 
            this.toolList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NameColumn,
            this.commandColumn,
            this.argColumn});
            this.toolList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolList.FullRowSelect = true;
            this.toolList.IsAutoColumSort = true;
            this.toolList.Location = new System.Drawing.Point(0, 0);
            this.toolList.Name = "toolList";
            this.toolList.Size = new System.Drawing.Size(355, 455);
            this.toolList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.toolList.TabIndex = 0;
            this.toolList.UseCompatibleStateImageBehavior = false;
            this.toolList.View = System.Windows.Forms.View.Details;
            this.toolList.SelectedIndexChanged += new System.EventHandler(this.toolList_SelectedIndexChanged);
            // 
            // NameColumn
            // 
            this.NameColumn.Text = "名称";
            this.NameColumn.Width = 135;
            // 
            // commandColumn
            // 
            this.commandColumn.Text = "コマンド";
            this.commandColumn.Width = 100;
            // 
            // argColumn
            // 
            this.argColumn.Text = "引数";
            this.argColumn.Width = 115;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 17;
            this.label4.Text = "引数";
            // 
            // btnInsertArgMacro
            // 
            this.btnInsertArgMacro.Location = new System.Drawing.Point(172, 190);
            this.btnInsertArgMacro.Name = "btnInsertArgMacro";
            this.btnInsertArgMacro.Size = new System.Drawing.Size(92, 23);
            this.btnInsertArgMacro.TabIndex = 12;
            this.btnInsertArgMacro.Text = "引数に挿入(&K)";
            this.btnInsertArgMacro.UseVisualStyleBackColor = true;
            this.btnInsertArgMacro.Click += new System.EventHandler(this.btnInsertArgMacro_Click);
            // 
            // btnInsertCommandMacro
            // 
            this.btnInsertCommandMacro.Location = new System.Drawing.Point(62, 190);
            this.btnInsertCommandMacro.Name = "btnInsertCommandMacro";
            this.btnInsertCommandMacro.Size = new System.Drawing.Size(104, 23);
            this.btnInsertCommandMacro.TabIndex = 12;
            this.btnInsertCommandMacro.Text = "コマンドに挿入(&I)";
            this.btnInsertCommandMacro.UseVisualStyleBackColor = true;
            this.btnInsertCommandMacro.Click += new System.EventHandler(this.btnInsertMacro_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(62, 11);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(60, 23);
            this.btnNew.TabIndex = 16;
            this.btnNew.Text = "新規(&N)";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnCommandRef
            // 
            this.btnCommandRef.Location = new System.Drawing.Point(383, 63);
            this.btnCommandRef.Name = "btnCommandRef";
            this.btnCommandRef.Size = new System.Drawing.Size(60, 23);
            this.btnCommandRef.TabIndex = 15;
            this.btnCommandRef.Text = "参照(&R)";
            this.btnCommandRef.UseVisualStyleBackColor = true;
            this.btnCommandRef.Click += new System.EventHandler(this.btnCommandRef_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(139, 11);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(60, 23);
            this.btnUpdate.TabIndex = 15;
            this.btnUpdate.Text = "登録(&W)";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // macroList
            // 
            this.macroList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.macroList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.macroName,
            this.macroValue});
            this.macroList.IsAutoColumSort = true;
            this.macroList.Location = new System.Drawing.Point(62, 219);
            this.macroList.Name = "macroList";
            this.macroList.Size = new System.Drawing.Size(382, 215);
            this.macroList.TabIndex = 14;
            this.macroList.UseCompatibleStateImageBehavior = false;
            this.macroList.View = System.Windows.Forms.View.Details;
            this.macroList.DoubleClick += new System.EventHandler(this.macroList_DoubleClick);
            // 
            // macroName
            // 
            this.macroName.Text = "マクロ名";
            this.macroName.Width = 99;
            // 
            // macroValue
            // 
            this.macroValue.Text = "値（サンプル）";
            this.macroValue.Width = 166;
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.CanCtrlDelete = true;
            this.txtName.IsDigitOnly = false;
            this.txtName.IsShowZoom = false;
            this.txtName.Location = new System.Drawing.Point(62, 40);
            this.txtName.Name = "txtName";
            this.txtName.PdHistory = null;
            this.txtName.Size = new System.Drawing.Size(382, 19);
            this.txtName.TabIndex = 9;
            // 
            // txtArgs
            // 
            this.txtArgs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtArgs.CanCtrlDelete = true;
            this.txtArgs.IsDigitOnly = false;
            this.txtArgs.IsShowZoom = false;
            this.txtArgs.Location = new System.Drawing.Point(62, 93);
            this.txtArgs.Multiline = true;
            this.txtArgs.Name = "txtArgs";
            this.txtArgs.PdHistory = null;
            this.txtArgs.Size = new System.Drawing.Size(381, 91);
            this.txtArgs.TabIndex = 11;
            // 
            // txtCommand
            // 
            this.txtCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCommand.CanCtrlDelete = true;
            this.txtCommand.IsDigitOnly = false;
            this.txtCommand.IsShowZoom = false;
            this.txtCommand.Location = new System.Drawing.Point(62, 65);
            this.txtCommand.Name = "txtCommand";
            this.txtCommand.PdHistory = null;
            this.txtCommand.Size = new System.Drawing.Size(315, 19);
            this.txtCommand.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "名称";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 219);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "マクロ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "コマンド";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(90, 478);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(60, 23);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "削除(&D)";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // OuterToolEditForm
            // 
            this.ClientSize = new System.Drawing.Size(851, 507);
            this.Controls.Add(this.EditPanelContainer);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnCancel);
            this.Name = "OuterToolEditForm";
            this.Load += new System.EventHandler(this.OuterToolEditForm_Load);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnDelete, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.EditPanelContainer, 0);
            this.Controls.SetChildIndex(this.MsgArea, 0);
            this.EditPanelContainer.Panel1.ResumeLayout(false);
            this.EditPanelContainer.Panel2.ResumeLayout(false);
            this.EditPanelContainer.Panel2.PerformLayout();
            this.EditPanelContainer.ResumeLayout(false);
            this.ListPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.SplitContainer EditPanelContainer;
        private System.Windows.Forms.Panel ListPanel;
        private qdbeListView toolList;
        private System.Windows.Forms.ColumnHeader NameColumn;
        private System.Windows.Forms.ColumnHeader commandColumn;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnInsertCommandMacro;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnUpdate;
        private qdbeListView macroList;
        private System.Windows.Forms.ColumnHeader macroName;
        private System.Windows.Forms.ColumnHeader macroValue;
        private quickDBExplorerTextBox txtName;
        private quickDBExplorerTextBox txtCommand;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCommandRef;
        private quickDBExplorerTextBox txtArgs;
        private System.Windows.Forms.ColumnHeader argColumn;
        private System.Windows.Forms.Button btnInsertArgMacro;
    }
}
