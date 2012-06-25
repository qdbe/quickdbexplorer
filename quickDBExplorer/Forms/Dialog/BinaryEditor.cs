using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace quickDBExplorer.Forms.Dialog
{
    public partial class BinaryEditor : quickDBExplorer.quickDBExplorerBaseForm
    {
        private byte[] originalData;
        public byte[] CurrentData { get; private set; }
        public bool IsReadOnly { get; private set; }
        public int MaxLength { get; private set; }

        public BinaryEditor(byte[] data, int maxlen, bool isReadOnly)
        {
            InitializeComponent();
            this.originalData = data;
            this.CurrentData = data;
            this.MaxLength = maxlen;
            this.IsReadOnly = IsReadOnly;
        }

        private void button1_Click(object sender, EventArgs e)
        {

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
    }
}
