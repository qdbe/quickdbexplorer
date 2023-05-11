using System;
using System.Collections.Generic;
using System.Text;

namespace quickDBExplorer.DataType
{
    internal class datetimeBaseType : baseType
    {
        #region IDataType メンバ

        public override string Convert(System.Data.IDataReader dr, int col, string addstr, string unichar, bool outNull, DBFieldInfo fieldInfo)
        {
            return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{1}{0}{1}", dr.GetDateTime(col).ToString(System.Globalization.CultureInfo.CurrentCulture), addstr);
        }

        public override bool TryParse(string data, DBFieldInfo fieldInfo, EmptyNullBehavior isEmptyAsNull, ref object result, ref string errmsg)
        {
            throw new NotImplementedException(); // Baseなので実装不要
        }

        public override bool CanLoadData()
        {
            return true;
        }

        public datetimeBaseType()
        {
            this.DefalutParseString = "2016/2/1";
        }

        #endregion

        public override string GetCSharpTypeString()
        {
            return "DateTime";
        }

        public override Type Type
        {
            get { return typeof(DateTime); }
        }
    }
}
