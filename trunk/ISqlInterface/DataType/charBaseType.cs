using System;
using System.Collections.Generic;
using System.Text;

namespace quickDBExplorer.DataType
{
    internal class charBaseType : IDataType
    {
        #region IDataType メンバ

        public string Convert(System.Data.IDataReader dr, int col, string addstr, string unichar, bool outNull, DBFieldInfo fieldInfo)
        {
            if (dr.GetString(col).Equals("") || dr.GetString(col).Equals("\0"))
            {
                return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{0}{0}", addstr);
            }
            else
            {
                if (dr.GetString(col).IndexOf("'") >= 0)
                {
                    // ' が文字列に入っている場合は '' に強制的に変換する
                    return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{1}{0}{1}", dr.GetString(col).Replace("'", "''").Replace("\0", ""), addstr);
                }
                else
                {
                    return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{1}{0}{1}", dr.GetString(col).Replace("\0", ""), addstr);
                }
            }

        }

        public string CheckForInput(string data, DBFieldInfo fieldInfo)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
