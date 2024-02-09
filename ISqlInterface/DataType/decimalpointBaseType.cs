using System;
using System.Collections.Generic;
using System.Text;

namespace quickDBExplorer.DataType
{
    internal class decimalpointBaseType : baseType
    {
        #region IDataType メンバ

        public override string Convert(System.Data.IDataReader dr, int col, string addstr, string unichar, bool outNull, DBFieldInfo fieldInfo)
        {
            return dr.GetDouble(col).ToString(System.Globalization.CultureInfo.CurrentCulture);
        }

        public override bool TryParse(string data, DBFieldInfo fieldInfo, EmptyNullBehavior isEmptyAsNull, ref object result, ref string errmsg)
        {
            return TryParse(data, typeof(double), "Double値を指定してください。", isEmptyAsNull, ref result, ref errmsg);
        }

        public override bool CanLoadData()
        {
            return true;
        }

        #endregion

        public override string GetCSharpTypeString()
        {
            return "decimal";
        }

        public override string GetFieldExcelOutString(string typename, int length, int prec, int xscale, bool isComma)
        {
            StringBuilder sb = new StringBuilder();
            if (!this.TypeHasSize)
            {
                sb.Append(typename);
                sb.Append(this.GetSeparator(isComma));
                sb.Append(this.GetSeparator(isComma));
            }
            else
            {
                sb.Append(typename);
                sb.Append(this.GetSeparator(isComma));
                if (prec > 0)
                {
                    sb.Append(prec);
                    sb.Append(this.GetSeparator(isComma));
                    sb.Append(xscale);
                }
                else
                {
                    if (length < 0)
                    {
                        sb.Append("max");
                    }
                    else
                    {
                        sb.Append(length);
                    }
                }
                sb.Append(this.GetSeparator(isComma));
            }
            return sb.ToString();

        }

        public override Type Type
        {
            get { return typeof(decimal); }
        }
    }
}
