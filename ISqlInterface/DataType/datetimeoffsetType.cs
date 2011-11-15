using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace quickDBExplorer.DataType
{
    internal class datetimeoffsetType : baseType
    {
        #region IDataType メンバ

        public override string Convert(System.Data.IDataReader dr, int col, string addstr, string unichar, bool outNull, DBFieldInfo fieldInfo)
        {
            return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{1}{0}{1}",
                ((SqlDataReader)dr).GetDateTimeOffset(col).ToString(System.Globalization.CultureInfo.CurrentCulture), addstr);
        }

        public override string TryParse(string data, DBFieldInfo fieldInfo, ref object result)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
