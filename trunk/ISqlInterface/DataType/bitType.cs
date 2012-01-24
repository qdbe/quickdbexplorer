using System;
using System.Collections.Generic;
using System.Text;

namespace quickDBExplorer.DataType
{
    internal class bitType : baseType
    {
        #region IDataType メンバ

        public override string Convert(System.Data.IDataReader dr, int col, string addstr, string unichar, bool outNull, DBFieldInfo fieldInfo)
        {
            return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{1}{0}{1}", dr.GetBoolean(col).ToString(), addstr);
        }

        public override bool TryParse(string data, DBFieldInfo fieldInfo, ref object result, ref string errmsg)
        {
            errmsg = null;
            bool localresult;
            if (bool.TryParse(data, out localresult))
            {
                result = localresult;
                return true;
            }
            else if( data == "0" )
            {
                // 0/1 も可能にする
                result = false;
                return true;
            }
            else if (data == "1")
            {
                // 0/1 も可能にする
                result = true;
                return true;
            }
            else
            {
                errmsg = "Boolean値を指定してください。";
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
