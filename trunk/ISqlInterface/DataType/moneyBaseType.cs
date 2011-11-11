using System;
using System.Collections.Generic;
using System.Text;

namespace quickDBExplorer.DataType
{
    internal class moneyBaseType : IDataType
    {
        #region IDataType メンバ

        public string Convert(System.Data.IDataReader dr, int col, string addstr, string unichar, bool outNull, DBFieldInfo fieldInfo)
        {
            return dr.GetValue(col).ToString();
        }

        public string CheckForInput(string data, DBFieldInfo fieldInfo)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
