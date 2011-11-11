using System;
using System.Collections.Generic;
using System.Text;
using System.Data;


namespace quickDBExplorer.DataType
{
    internal class smallintType : IDataType
    {
        #region IDataType メンバ

        public string Convert(IDataReader dr, int col, string addstr, string unichar, bool outNull, DBFieldInfo fieldInfo)
        {
            return dr.GetInt16(col).ToString(System.Globalization.CultureInfo.CurrentCulture);
        }

        public string CheckForInput(string data, DBFieldInfo fieldInfo)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
