using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using quickDBExplorer;
using System.Data;


namespace quickDBExplorer.DataType
{
    internal abstract class ncharBaseType : baseType
    {
        internal ncharBaseType()
        {
        }

        public override string Convert(IDataReader dr, int col, string addstr, string unichar, bool outNull, DBFieldInfo fieldInfo)
        {
            string targetData = (string)dr.GetString(col);
            if (targetData.Equals("") || targetData.Equals("\0"))
            {
                return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{1}{0}{0}", addstr, unichar);
            }
            else
            {
                if (targetData.IndexOf("'") >= 0)
                {
                    // ' が文字列に入っている場合は '' に強制的に変換する
                    return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{2}{1}{0}{1}", targetData.Replace("'", "''").Replace("\0", ""), addstr, unichar);
                }
                else
                {
                    return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{2}{1}{0}{1}", targetData.Replace("\0", ""), addstr, unichar);
                }
            }
        }

        public override bool TryParse(string data, DBFieldInfo fieldInfo, ref object result, ref string errmsg)
        {
            errmsg = null;
            if (fieldInfo.Length < data.Length)
            {
                errmsg = fieldInfo.Length.ToString() + "桁以上の値は指定できません。";
                return false;
            }
            result = data;
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

    }
}
