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
    /// オブジェクト一覧の絞り込み複数指定ダイアログ
    /// </summary>
    public partial class MultiTableFilter : quickDBExplorer.QueryDialog
    {

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MultiTableFilter()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 検索方法
        /// </summary>
        public SearchType SelecteType { get; private set; }

        /// <summary>
        /// 大文字小文字を区別するか否か
        /// </summary>
        public bool IsCaseSensitive { get; private set; }

        /// <summary>
        /// 実行ボタン押下時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal override void btnGo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtInput.Text))
            {
                MessageBox.Show("検索対象を指定してください。");

                return;
            }
            this.InputedText = this.txtInput.Text; 
            if(this.chkCaseSensitive.Checked)
            {
                this.IsCaseSensitive = true;
            }
            else
            {
                this.IsCaseSensitive= false;
            }

            if( this.rdoContain.Checked ) {
                this.SelecteType = SearchType.SearchContain;
            }
            else if( this.rdoStartWith.Checked )
            {
                this.SelecteType = SearchType.SearchStartWith;
            }
            else if (this.rdoExact.Checked)
            {
                this.SelecteType = SearchType.SearchExact;
            }
            this.txtInput.SaveHistory("");

            this.DialogResult = DialogResult.OK;
        }
    }
}
