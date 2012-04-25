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

        public override bool TryParse(string data, DBFieldInfo fieldInfo, ref object result, ref string errmsg)
        {
            return TryParse(data, typeof(DateTimeOffset), "DateTimeOffset を表す値を指定してください。", ref result, ref errmsg);
        }

        public override bool CanLoadData()
        {
            return true;
        }


        public datetimeoffsetType()
        {
            this.DefalutParseString = "2012/01/21 +09:00";
        }
        #endregion
    }
}
