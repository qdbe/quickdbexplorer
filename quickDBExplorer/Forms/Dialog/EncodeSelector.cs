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
    /// 文字コード選択ダイアログ
    /// </summary>
    public partial class EncodeSelector : quickDBExplorer.quickDBExplorerBaseForm
    {
        /// <summary>
        /// 選択された文字コード
        /// </summary>
        public Encoding SelectedEncoding
        {
            get;
            private set;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public EncodeSelector()
        {
            InitializeComponent();
            this.SelectedEncoding = Encoding.GetEncoding("Shift_JIS");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.SelectedEncoding = ((EncodingSet)this.cmbEncoding.SelectedItem).Encode;
            this.Close();
        }

        private void cmbEncoding_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cmbEncoding.Items.Clear();
            this.cmbEncoding.Items.Add(new EncodingSet("Shift-JIS", Encoding.GetEncoding("Shift_JIS")));
            this.cmbEncoding.Items.Add(new EncodingSet("UTF-8", Encoding.UTF8));
            this.cmbEncoding.Items.Add(new EncodingSet("UTF-16", Encoding.Unicode));
        }

        class EncodingSet
        {
            public string Name { get; set; }
            public Encoding Encode { get; set; }

            public EncodingSet(string encodeName, Encoding enc)
            {
                this.Name = encodeName;
                this.Encode = enc;
            }

            public override string ToString()
            {
                return this.Name;
            }
        }
    }
}
