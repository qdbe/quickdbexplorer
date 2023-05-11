using System;
using System.Collections.Generic;
using System.Text;

namespace quickDBExplorer.DataType
{
    internal class charBaseType : baseType
    {
        #region IDataType メンバ

        public override string Convert(System.Data.IDataReader dr, int col, string addstr, string unichar, bool outNull, DBFieldInfo fieldInfo)
        {
            if (dr.GetString(col).Equals("") || dr.GetString(col).Equals("\0"))
            {
                return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{0}{0}", addstr);
            }
            else
            {
                if (dr.GetString(col).IndexOf("'") >= 0)
                {
                    // ' が文字列に入っている場合は '' に強制的に変換する
                    return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{1}{0}{1}", dr.GetString(col).Replace("'", "''").Replace("\0", ""), addstr);
                }
                else
                {
                    return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{1}{0}{1}", dr.GetString(col).Replace("\0", ""), addstr);
                }
            }

        }

        public override bool TryParse(string data, DBFieldInfo fieldInfo, EmptyNullBehavior isEmptyAsNull, ref object result, ref string errmsg)
        {
            errmsg = null;
            if (IsSingleByte == true)
            {
                if (fieldInfo.Length >= 0 && fieldInfo.Length < Encoding.GetEncoding("Shift_JIS").GetByteCount(data))
                {
                    errmsg = fieldInfo.Length.ToString() + "桁以上の値は指定できません。";
                    return false;
                }
            }
            else
            {
                if (fieldInfo.Length >= 0 && fieldInfo.Length < data.Length)
                {
                    errmsg = fieldInfo.Length.ToString() + "桁以上の値は指定できません。";
                    return false;
                }
            }
            if (string.IsNullOrEmpty(data))
            {
                if (isEmptyAsNull == EmptyNullBehavior.NoConv)
                {
                    result = data;
                }
                else if (isEmptyAsNull == EmptyNullBehavior.AsNULL)
                {
                    result = null;
                }
                else if (isEmptyAsNull == EmptyNullBehavior.AsEmpty)
                {
                    result = string.Empty;
                }
            }
            return true;
        }

        public override string GetFieldTypeString(string typename, int length, int prec, int xscale)
        {
            if (this.TypeHasSize == false) return base.GetFieldTypeString(typename, length, prec, xscale);


            if (length == -1)
            {
                return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{0}(max)",
                    typename);
            }
            else
            {
                return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{0}({1})",
                    typename,
                    length);
            }
        }

        public override bool CanLoadData()
        {
            return true;
        }

        #endregion

        protected bool IsSingleByte {get;set;}

        public charBaseType()
        {
            IsSingleByte = true;
        }

        public override string GetCSharpTypeString()
        {
            return "string";
        }

        public override Type Type
        {
            get { return typeof(string); }
        }
    }
}
