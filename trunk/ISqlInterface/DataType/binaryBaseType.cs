using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;



namespace quickDBExplorer.DataType
{
    internal abstract class binaryBaseType : IDataType
    {
        #region IDataType メンバ

        public virtual string Convert(IDataReader dr, int col, string addstr, string unichar, bool outNull, DBFieldInfo fieldInfo)
        {
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
                    sodata += odata[k].ToString("X2", System.Globalization.CultureInfo.InvariantCulture);
                }
                return string.Format(System.Globalization.CultureInfo.InvariantCulture, "{1}{0}{1}", sodata, addstr);
            }
        }

        public virtual string CheckForInput(string data, DBFieldInfo fieldInfo)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
