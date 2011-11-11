using System;
using System.Collections.Generic;
using System.Text;

namespace quickDBExplorer.DataType
{
    internal class datetimeBaseType : IDataType
    {
        #region IDataType メンバ

        public virtual string Convert(System.Data.IDataReader dr, int col, string addstr, string unichar, bool outNull, DBFieldInfo fieldInfo)
        {
            return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{1}{0}{1}", dr.GetDateTime(col).ToString(System.Globalization.CultureInfo.CurrentCulture), addstr);
        }

        public virtual string CheckForInput(string data, DBFieldInfo fieldInfo)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
