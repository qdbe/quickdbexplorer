using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace quickDBExplorer
{
    /// <summary>
    /// SQL Server の一覧を選択するダイアログ
    /// </summary>
    public partial class SqlServerSelector : quickDBExplorer.quickDBExplorerBaseForm
    {
        /// <summary>
        /// 選択されたサーバー名
        /// </summary>
        public string SelectedServerName { get; private set; }
        /// <summary>
        /// 選択されたインスタンス名
        /// </summary>
        public string SelectedInstanceName { get; private set; }
        /// <summary>
        /// 選択されたサーバーのクラスタ状態
        /// </summary>
        public string SelectedIsClustered { get; private set; }
        /// <summary>
        /// 選択されたサーバーのバージョン
        /// </summary>
        public string SelectedVersion { get; private set; }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="serverList">SqlDataSourceEnumerator.GetDataSourcesの結果を渡す</param>
        public SqlServerSelector(DataTable serverList)
        {
            InitializeComponent();

            SetServerList(serverList);
        }

        private void SetServerList(DataTable　serverList)
        {
            this.sqlServerList.SuspendLayout();
            foreach (DataRow dr in serverList.Rows)
            {
                ListViewItem item = new ListViewItem(
                    new string[] {
                    dr["ServerName"].ToString(),
                    dr["InstanceName"].ToString(),
                    dr["IsClustered"].ToString(),
                    dr["Version"].ToString()
                    }
                    );
                this.sqlServerList.Items.Add(item);
            }
            this.sqlServerList.ResumeLayout();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DecideServer();
        }

        private void DecideServer()
        {
            if (this.sqlServerList.SelectedItems.Count == 0)
            {
                MessageBox.Show("サーバーを選択して下さい");
                return;
            }
            this.SelectedServerName = this.sqlServerList.SelectedItems[0].SubItems[0].Text;
            this.SelectedInstanceName = this.sqlServerList.SelectedItems[0].SubItems[1].Text;
            this.SelectedIsClustered = this.sqlServerList.SelectedItems[0].SubItems[2].Text;
            this.SelectedVersion = this.sqlServerList.SelectedItems[0].SubItems[3].Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void sqlServerList_DoubleClick(object sender, EventArgs e)
        {
            DecideServer();
        }
    }
}
