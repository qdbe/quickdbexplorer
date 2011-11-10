using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using quickDBExplorer;

namespace quickDBExplorer.DataType
{
    abstract class stringBaseType : baseType
    {
        private stringBaseType()
        {
            this.TypeName = "stringBase";
        }

        public override static IDataType Create()
        {
            throw new NotImplementedException();
        }

        public virtual string Convert(string data, string addstr, string unichar, bool outNull, DBFieldInfo fieldInfo)
        {
            if (data.Equals("") || data.Equals("\0"))
            {
                return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{1}{0}{0}", addstr, unichar);
            }
            else
            {
                if (data.IndexOf("'") >= 0)
                {
                    // ' が文字列に入っている場合は '' に強制的に変換する
                    return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{2}{1}{0}{1}", data.Replace("'", "''").Replace("\0", ""), addstr, unichar);
                }
                else
                {
                    return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{2}{1}{0}{1}", data.Replace("\0", ""), addstr, unichar);
                }
            }
        }

        public virtual string CheckForInput(string data, DBFieldInfo fieldInfo)
        {
            if (fieldInfo.Length < data.Length)
            {
                return "項目 " + fieldInfo.Name + "には " + fieldInfo.Length + "桁以上の値は指定できません。";
            }
            return null;
        }
    }
}
