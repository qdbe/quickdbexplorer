using System;
using System.Collections.Generic;
using System.Text;
using System.Data;


namespace quickDBExplorer.DataType
{
    internal class smallintType : baseType
    {
        #region IDataType メンバ

        public override string Convert(IDataReader dr, int col, string addstr, string unichar, bool outNull, DBFieldInfo fieldInfo)
        {
            return dr.GetInt16(col).ToString(System.Globalization.CultureInfo.CurrentCulture);
        }

        public override string TryParse(string data, DBFieldInfo fieldInfo, ref object result)
        {
            string errmsg = null;
            try
            {
                if (data == "")
                {
                    result = DBNull.Value;
                }
                else
                {
                    Int16 localresult;
                    if (Int16.TryParse(data, out localresult) == true)
                    {
                        result = localresult;
                    }
                    else
                    {
                        errmsg = "Int16 の整数を指定してください。";
                    }
                }
            }
            catch
            {
                errmsg = "Int16 の整数を指定してください。";
            }
            return errmsg;
        }

        #endregion
    }
}
