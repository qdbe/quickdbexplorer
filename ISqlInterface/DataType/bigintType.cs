using System;
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

        public override bool TryParse(string data, DBFieldInfo fieldInfo, EmptyNullBehavior isEmptyAsNull, ref object result, ref string errmsg)
        {
            return this.TryParse(data, typeof(System.Int64), "Int64の整数を指定してください。", isEmptyAsNull, ref result, ref errmsg);
        }

        public override bool CanLoadData()
        {
            return true;
        }
        
        public override string GetFieldExcelOutString(string typename, int length, int prec, int xscale, bool isComma)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(typename);
            sb.Append(this.GetSeparator(isComma));
            sb.Append(this.GetSeparator(isComma));

            return sb.ToString();
        }


        public override Type Type
        {
            get { return typeof(Int64); }
        }
    }
}
