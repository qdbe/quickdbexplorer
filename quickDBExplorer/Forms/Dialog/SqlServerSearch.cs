using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace quickDBExplorer
{
    /// <summary>
    /// SQL Server の一覧を選択するダイアログ
    /// </summary>
    public partial class SqlServerSearch : quickDBExplorer.quickDBExplorerBaseForm
    {

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; private set; }

        /// <summary>
        /// キャンセルしたか否か
        /// </summary>
        protected bool IsCancel { get; set; }

        /// <summary>
        /// 完了したか否か
        /// </summary>
        protected bool IsComplete { get; set; }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SqlServerSearch()
        {
            InitializeComponent();
        }

        private void SqlServerSearch_Load(object sender, EventArgs e)
        {
            this.timer1.Interval = 500;
            this.timer1.Start();
        }

        private void SearchProcess(object sender, EventArgs e)
        {
            this.IsCancel = false;
            this.timer1.Stop();

            SqlDataSourceEnumerator list = System.Data.Sql.SqlDataSourceEnumerator.Instance;
            DataTable serverList = new DataTable();
            this.Cursor = Cursors.WaitCursor;
            this.IsComplete = false;
            this.IsCancel = false;
            System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                serverList = list.GetDataSources();
                this.IsComplete = true;
            }));
            th.Start();

            try
            {
                while (true)
                {
                    bool result = th.Join(100);
                    if (this.IsCancel == true || result == true)
                    {
                        if (this.IsCancel == true)
                        {
                            //th.Abort();
                        }
                        break;
                    }
                    if (this.progressBar1.Value == this.progressBar1.Maximum)
                    {
                        this.progressBar1.Value = this.progressBar1.Minimum;
                    }
                    this.progressBar1.Increment(this.progressBar1.Step);
                    Application.DoEvents();

                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
                if (this.IsComplete)
                {
                    this.SearchResult = serverList;
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    this.DialogResult = DialogResult.Cancel;
                }
                this.Close();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.IsCancel = true;
        }
    }
}
