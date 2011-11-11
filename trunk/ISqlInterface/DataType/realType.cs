using System;
using System.Collections.Generic;
using System.Text;
using System.Data;


namespace quickDBExplorer.DataType
{
    internal class realType : IDataType
    {
        #region IDataType メンバ

        public string Convert(IDataReader dr, int col, string addstr, string unichar, bool outNull, DBFieldInfo fieldInfo)
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
