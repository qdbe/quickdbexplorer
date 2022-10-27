using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Timers;

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

        private DataTable displayList;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SqlServerSelector()
        {
            InitializeComponent();


        }

        private void SetServerList(DataTable serverList, string filterString)
        {
            displayList = serverList;
            this.sqlServerList.BeginUpdate();
            this.sqlServerList.SuspendLayout();
            this.sqlServerList.Items.Clear();
            string cmpstr = filterString.ToLower();
            foreach (DataRow dr in serverList.Rows)
            {
                if (filterString != string.Empty)
                {
                    if (!dr["ServerName"].ToString().ToLower().Contains(cmpstr) &&
                        !dr["InstanceName"].ToString().ToLower().Contains(cmpstr))
                    {
                        continue;
                    }
                }
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
            this.sqlServerList.EndUpdate();
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

        private void btnReload_Click(object sender, EventArgs e)
        {
            StartTimer();
        }

        private void StartTimer()
        {
            this.timer1.Interval = 500;
            this.timer1.Start();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string filterText = this.txtFilter.Text;
            SetServerList(displayList, filterText);
        }

        private void SqlServerSelector_Load(object sender, EventArgs e)
        {
            StartTimer();
        }

        private void StartSearch(object sender, EventArgs e)
        {
            this.timer1.Stop();
            SqlServerSearch searchdlg = new SqlServerSearch();
            searchdlg.Owner = this;
            searchdlg.ShowDialog(this);
            if (searchdlg.DialogResult == DialogResult.OK)
            {
                DataTable serverList = searchdlg.SearchResult;
                if (serverList.Rows.Count == 0)
                {
                    MessageBox.Show("選択可能なサーバーはありません");
                    return;
                }
                this.txtFilter.Text = string.Empty;
                SetServerList(serverList, string.Empty);
            }
        }
    }
}
