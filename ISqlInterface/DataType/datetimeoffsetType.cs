using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace quickDBExplorer.DataType
{
    internal class datetimeoffsetType : baseType
    {
        #region IDataType メンバ

        public override string Convert(System.Data.IDataReader dr, int col, string addstr, string unichar, bool outNull, DBFieldInfo fieldInfo)
        {
            return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{1}{0}{1}",
                ((SqlDataReader)dr).GetDateTimeOffset(col).ToString(System.Globalization.CultureInfo.CurrentCulture), addstr);
        }

        public override bool TryParse(string data, DBFieldInfo fieldInfo, EmptyNullBehavior isEmptyAsNull, ref object result, ref string errmsg)
        {
            return TryParse(data, typeof(DateTimeOffset), "DateTimeOffset を表す値を指定してください。", isEmptyAsNull, ref result, ref errmsg);
        }

        public override bool CanLoadData()
        {
            return true;
        }


        public datetimeoffsetType()
        {
            this.DefalutParseString = "2016/01/21 +09:00";
            this.TypeHasSize = true;
        }
        #endregion

        public override string GetCSharpTypeString()
        {
            return "DateTimeOffset";
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



        public override Type Type
        {
            get { return typeof(DateTimeOffset); }
        }
    }
}
