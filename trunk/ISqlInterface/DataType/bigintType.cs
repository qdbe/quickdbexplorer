﻿using System;
using System.Collections.Generic;
using System.Text;

namespace quickDBExplorer.DataType
{
    internal class bigintType : baseType
    {
        public override string Convert(System.Data.IDataReader dr, int col, string addstr, string unichar, bool outNull, DBFieldInfo fieldInfo)
        {
            return dr.GetInt64(col).ToString(System.Globalization.CultureInfo.CurrentCulture);
        }

        public override string TryParse(string data, DBFieldInfo fieldInfo, ref object result)
        {
            throw new NotImplementedException();
        }
    }
}
