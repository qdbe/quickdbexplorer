﻿using System;
using System.Collections.Generic;
using System.Text;

namespace quickDBExplorer.DataType
{
    internal class sql_variantType : baseType
    {
        #region IDataType メンバ

        public override string Convert(System.Data.IDataReader dr, int col, string addstr, string unichar, bool outNull, DBFieldInfo fieldInfo)
        {
            return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{1}{0}{1}", dr.GetValue(col).ToString(), addstr);
        }

        public override bool TryParse(string data, DBFieldInfo fieldInfo, ref object result, ref string errmsg)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}