using System;
using System.Collections.Generic;
using System.Text;

namespace quickDBExplorer.DataType
{
    internal class datetime2Type : datetimeBaseType
    {
        public datetime2Type()
        {
            this.TypeHasSize = true;
        }

        public override bool TryParse(string data, DBFieldInfo fieldInfo, EmptyNullBehavior isEmptyAsNull, ref object result, ref string errmsg)
        {
            return TryParse(data, typeof(DateTime), "DateTime2を表す値を指定してください。", isEmptyAsNull, ref result, ref errmsg);
        }

        public override string GetFieldTypeString(string typename, int length, int prec, int xscale)
        {
            return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{0}({1})",
                typename,
                xscale);
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

    }
}
