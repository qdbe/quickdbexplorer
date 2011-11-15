using System;
using System.Collections.Generic;
using System.Text;

namespace quickDBExplorer.DataType
{
    internal class moneyBaseType : baseType
    {
        public override string Convert(System.Data.IDataReader dr, int col, string addstr, string unichar, bool outNull, DBFieldInfo fieldInfo)
        {
            return dr.GetValue(col).ToString();
        }

        public override string TryParse(string data, DBFieldInfo fieldInfo, ref object result)
        {
            throw new NotImplementedException();
        }
    }
}
