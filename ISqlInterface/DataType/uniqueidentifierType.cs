using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace quickDBExplorer.DataType
{
    internal class uniqueidentifierType : IDataType
    {
        #region IDataType メンバ

        public string Convert(IDataReader dr, int col, string addstr, string unichar, bool outNull, DBFieldInfo fieldInfo)
        {
            return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{1}{0}{1}", dr.GetValue(col).ToString(), addstr);
        }

        public string CheckForInput(string data, DBFieldInfo fieldInfo)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
