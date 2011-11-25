using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace quickDBExplorer.DataType
{
    internal class timestampType : baseType
    {
        #region IDataType メンバ

        public override string Convert(IDataReader dr, int col, string addstr, string unichar, bool outNull, DBFieldInfo fieldInfo)
        {
            // timestamp は自動更新されるのでnullでよい
            if (outNull)
            {
                return string.Format(System.Globalization.CultureInfo.CurrentCulture, "null");
            }
            else
            {
                // バイナリはヘキサ文字列で出しておく
                byte[] odata = ((SqlDataReader)dr).GetSqlBinary(col).Value;
                string sodata = "0x";
                for (int k = 0; k < odata.Length; k++)
                {
                    sodata += odata[k].ToString("X2", System.Globalization.CultureInfo.CurrentCulture);
                }
                return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{1}{0}{1}", sodata, addstr);
            }
            
        }

        public override bool TryParse(string data, DBFieldInfo fieldInfo, ref object result, ref string errmsg)
        {
            return true;
        }

        public override bool CanLoadData()
        {
            return true;
        }

        #endregion
    }
}
