﻿using System;
using System.Collections.Generic;
using System.Text;

namespace quickDBExplorer.DataType
{
    internal class timeType : datetimeBaseType
    {
        public override string Convert(System.Data.IDataReader dr, int col, string addstr, string unichar, bool outNull, DBFieldInfo fieldInfo)
        {
            return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{1}{0}{1}", dr.GetValue(col).ToString(), addstr);
        }

        public override bool TryParse(string data, DBFieldInfo fieldInfo, EmptyNullBehavior isEmptyAsNull, ref object result, ref string errmsg)
        {
            return TryParse(data, typeof(System.TimeSpan), "TimeSpanを表す値を指定してください。", isEmptyAsNull, ref result, ref errmsg);
        }

        public timeType()
        {
            this.DefalutParseString = "12:00";
            this.TypeHasSize = true;
        }

        public override string GetCSharpTypeString()
        {
            return "TimeSpan";
        }

        public override string GetFieldExcelOutString(string typename, int length, int prec, int xscale, bool isComma)
        {
            if (this.TypeHasSize == false) return typename;

            StringBuilder sb = new StringBuilder();
            sb.Append(typename);
            sb.Append(this.GetSeparator(isComma));
            sb.Append(xscale);
            sb.Append(this.GetSeparator(isComma));

            return sb.ToString();
        }


        public override Type Type
        {
            get
            {
                return typeof(TimeSpan);
            }
        }
    }
}
