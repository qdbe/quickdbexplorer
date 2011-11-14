﻿using System;
using System.Collections.Generic;
using System.Text;

namespace quickDBExplorer.DataType
{
    internal class numBaseType : baseType
    {
        #region IDataType メンバ

        public override string Convert(System.Data.IDataReader dr, int col, string addstr, string unichar, bool outNull, DBFieldInfo fieldInfo)
        {
            return dr.GetDecimal(col).ToString(System.Globalization.CultureInfo.CurrentCulture);
        }

        public override string CheckForInput(string data, DBFieldInfo fieldInfo)
        {
            throw new NotImplementedException();
        }

        public override string GetFieldTypeString(string typename, int length, int prec, int xscale)
        {
            return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{0}({1},{2})",
                typename,
                prec,
                xscale);
        }

        #endregion
    }
}
