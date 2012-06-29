using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace quickDBExplorer.Forms.Dialog
{
    /// <summary>
    /// バイナリデータ参照・編集ダイアログ
    /// </summary>
    public partial class BinaryEditor : quickDBExplorer.quickDBExplorerBaseForm
    {
        private byte[] originalData;
        /// <summary>
        /// 現在の値
        /// </summary>
        public byte[] CurrentData { get; private set; }
        /// <summary>
        /// 参照のみか否か
        /// </summary>
        public bool IsReadOnly { get; private set; }

        /// <summary>
        /// 最大バイト長
        /// </summary>
        public int MaxLength { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="data"></param>
        /// <param name="maxlen"></param>
        /// <param name="isReadOnly"></param>
        public BinaryEditor(byte[] data, int maxlen, bool isReadOnly)
        {
            InitializeComponent();
            this.originalData = data;
            this.CurrentData = data;
            this.MaxLength = maxlen;
            this.IsReadOnly = isReadOnly;
        }

        private void btnLoadClipboard_Click(object sender, EventArgs e)
        {
        }

        private void btnDefaultImage_Click(object sender, EventArgs e)
        {
            this.CurrentData = originalData;
        }

        private void BinaryEditor_Load(object sender, EventArgs e)
        {
            ResetBinaryData();
            if (this.IsReadOnly)
            {
                this.grpImport.Enabled = false;
                this.btnOk.Enabled = false;
            }

        }

        private void ResetBinaryData()
        {
            if (CurrentData == null)
            {
                this.txtData.Text = string.Empty;
            }
            else
            {
                string sodata = GetHexString();
                // 行で割る
                this.txtData.MaxLength = sodata.Length;
                string outtext = SplitHexString(sodata);
                this.txtData.Text = outtext;
            }
        }

        private static readonly int HEXMAX = 60;
        private string SplitHexString(string sodata)
        {
            int max = sodata.Length;
            StringBuilder sb = new StringBuilder();
            int restlen = max;
            for (int i = 0; restlen > 0; i += HEXMAX)
            {
                int len = HEXMAX;
                if( len > restlen )
                {
                    len = restlen;
                }
                sb.Append(sodata.Substring(i, len));
                restlen -= len;
                sb.Append("\r\n");
            }
            return sb.ToString();
        }

        private string GetHexString()
        {
            StringBuilder sb = new StringBuilder();
            for (int k = 0; k < CurrentData.Length; k++)
            {
                sb.Append(CurrentData[k].ToString("X2", System.Globalization.CultureInfo.InvariantCulture));
            }
            return sb.ToString();
        }

        public bool TryParse(string data, ref object result)
        {
            if (string.IsNullOrEmpty(data.Trim()))
            {
                result = null;
                return true;
            }

            // ヘキサ文字列を読み込む
            if (!data.StartsWith("0x"))
            {
                MessageBox.Show("バイナリデータにはヘキサ文字列のみが指定できます。");
                return false;
            }


            string hexstr = data.Substring(2);
            if (hexstr.Length % 2 == 1)
            {
                MessageBox.Show("ヘキサ文字列の形式が不正です");
                return false;
            }
            if (this.MaxLength != 0)
            {
                int len = hexstr.Length / 2;
                if (this.MaxLength < len)
                {
                    MessageBox.Show("データサイズを超えています");
                    return false;
                }
            }

            byte each;
            List<byte> ret = new List<byte>();

            for (int i = 0; i < hexstr.Length; i += 2)
            {
                if (byte.TryParse(hexstr.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out each) == false)
                {
                    MessageBox.Show("ヘキサ文字列の形式が不正です");
                    return false;
                }
                ret.Add(each);
            }
            result = ret.ToArray();
            return true;
        }


        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            exportfile();
        }

        private void exportfile()
        {
            try
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.OverwritePrompt = true;
                dlg.Filter = "全て|*.*";

                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                BinaryWriter bw = new BinaryWriter(File.Open(dlg.FileName, FileMode.OpenOrCreate));
                try
                {
                    bw.Write(this.CurrentData);
                }
                finally
                {
                    bw.Close();
                }
            }
            catch (Exception exp)
            {
                this.MsgArea.Text = exp.ToString();
            }
        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "全て|*.*";
            if (dlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            try
            {
                BinaryReader br = new BinaryReader(File.OpenRead(dlg.FileName));
                try
                {
                    List<byte> result = new List<byte>();
                    for (; ; )
                    {
                        byte[] loadData = br.ReadBytes(512);
                        if (loadData == null || loadData.Length == 0)
                        {
                            break;
                        }
                        result.AddRange(loadData);
                    }
                    if (this.MaxLength != 0 &&
                        result.Count > this.MaxLength)
                    {
                        MessageBox.Show("制限されたデータ長を超えていますので読み込みできません");
                        return;
                    }
                    this.CurrentData = result.ToArray();
                    ResetBinaryData();
                }
                finally
                {
                    br.Close();
                }
            }
            catch(Exception exp)
            {
                this.MsgArea.Text = exp.ToString();
                return;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (this.IsReadOnly == false)
            {
                if (this.originalData != this.CurrentData)
                {
                    if (MessageBox.Show("値が変更されていますが、キャンセルしますか？", "", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    {
                        return;
                    }
                }
            }
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
