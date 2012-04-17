using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace quickDBExplorer.DataType
{
    internal class uniqueidentifierType : baseType
    {
        #region IDataType メンバ

        public override string Convert(IDataReader dr, int col, string addstr, string unichar, bool outNull, DBFieldInfo fieldInfo)
        {
            return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{1}{0}{1}", dr.GetValue(col).ToString(), addstr);
        }

        public override bool TryParse(string data, DBFieldInfo fieldInfo, ref object result, ref string errmsg)
        {
            try
            {
                if (string.IsNullOrEmpty(data.Trim()))
                {
                    result = DBNull.Value;
                    return true;
                }
                result = data;
                return true;
            }
            catch
            {
                errmsg = "Guidを表す文字列を指定してください。";
                return false;
            }
        }

        public override bool CanLoadData()
        {
            return true;
        }

        #endregion

    }
}
