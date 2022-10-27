using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;



namespace quickDBExplorer.DataType
{
    internal abstract class binaryBaseType : baseType
    {
        #region IDataType メンバ

        public override string Convert(IDataReader dr, int col, string addstr, string unichar, bool outNull, DBFieldInfo fieldInfo)
        {
            // バイナリはヘキサ文字列で出しておく
            byte[] odata = ((SqlDataReader)dr).GetSqlBinary(col).Value;
            string sodata = "0x";
            for (int k = 0; k < odata.Length; k++)
            {
                sodata += odata[k].ToString("X2", System.Globalization.CultureInfo.InvariantCulture);
            }
            return sodata;
        }

        public override bool TryParse(string data, DBFieldInfo fieldInfo, ref object result, ref string errmsg)
        {
            if (string.IsNullOrEmpty(data.Trim()))
            {
                result = DBNull.Value;
                return true;
            }
            // ヘキサ文字列を読み込む
            if (!data.StartsWith("0x"))
            {
                errmsg = "バイナリデータにはヘキサ文字列のみが指定できます。";
                return false;
            }

            List<byte> ret = new List<byte>();

            string hexstr = data.Substring(2);
            if( hexstr.Length % 2 == 1 )
            {
                errmsg = "ヘキサ文字列の形式が不正です";
                return false;
            }
            int maxlen = hexstr.Length / 2;
            if (this.TypeHasSize == true)
            {
                if (fieldInfo.Length >= 0 && fieldInfo.Length < maxlen)
                {
                    errmsg = "データサイズを超えています";
                    return false;
                }
            }

            byte each;
            for (int i = 0; i < maxlen; i++)
            {
                if (byte.TryParse(hexstr.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out each) == false)
                {
                    errmsg = "ヘキサ文字列の形式が不正です";
                    return false;
                }
                ret.Add(each);
            }
            result = ret.ToArray();
            return true;
        }

        public override string GetFieldTypeString(string typename, int length, int prec, int xscale)
        {
            if (this.TypeHasSize == false) return base.GetFieldTypeString(typename, length, prec, xscale);

            if (length == -1)
            {
                return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{0}(max)",
                    typename);
            }
            else
            {
                return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{0}({1})",
                    typename,
                    length);
            }
        }

        public override bool CanLoadData()
        {
            return true;
        }

        public override bool IsBinary
        {
            get
            {
                return true;
            }
        }


        #endregion

        public override Type Type
        {
            get { return typeof(byte[]); }
        }

        public override string GetCSharpTypeString()
        {
            return "byte[]";
        }

        public override bool TryCheckObject(object data, DBFieldInfo fieldInfo, ref string errmsg)
        {
            if (data == DBNull.Value)
            {
                return true;
            }

            byte[] datas = (byte[])data;
            int maxlen = datas.Length;
            if (this.TypeHasSize == true)
            {
                if (fieldInfo.Length >= 0 && fieldInfo.Length < maxlen)
                {
                    errmsg = "データサイズを超えています";
                    return false;
                }
            }
            return true;
        }
    }
}
