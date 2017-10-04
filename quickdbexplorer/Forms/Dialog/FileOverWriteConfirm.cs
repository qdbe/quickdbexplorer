using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace quickDBExplorer.Forms
{
    /// <summary>
    /// ファイル上書き確認
    /// </summary>
    public partial class FileOverWriteConfirm : quickDBExplorer.quickDBExplorerBaseForm
    {
        /// <summary>
        /// 上書きするファイル名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 表示以外のファイルを同様に上書きするか
        /// </summary>
        public bool IsOverWriteEtc { get; private set; }

        /// <summary>
        /// 対象ファイル数
        /// </summary>
        public int  TargetFileCount { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FileOverWriteConfirm()
        {
            InitializeComponent();
            TargetFileCount = 0;
            FileName = string.Empty;
            IsOverWriteEtc = false;
        }

        private void FileOverWriteConfirm_Load(object sender, EventArgs e)
        {
            this.txtFileName.Text = this.FileName;
            if (TargetFileCount > 1)
            {
                this.chkOverwriteEtc.Enabled = true;
            }
            else
            {
                this.chkOverwriteEtc.Enabled = false;
            }
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.IsOverWriteEtc = this.chkOverwriteEtc.Checked;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
