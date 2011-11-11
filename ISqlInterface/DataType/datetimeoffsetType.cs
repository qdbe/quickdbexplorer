using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace quickDBExplorer.DataType
{
    internal class datetimeoffsetType : IDataType
    {
        #region IDataType メンバ

        public string Convert(System.Data.IDataReader dr, int col, string addstr, string unichar, bool outNull, DBFieldInfo fieldInfo)
        {
            return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{1}{0}{1}",
                ((SqlDataReader)dr).GetDateTimeOffset(col).ToString(System.Globalization.CultureInfo.CurrentCulture), addstr);
        }

        public string CheckForInput(string data, DBFieldInfo fieldInfo)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
