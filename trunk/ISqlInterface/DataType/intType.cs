using System;
using System.Collections.Generic;
using System.Text;

namespace quickDBExplorer.DataType
{
    internal class intType : baseType
    {
        #region IDataType メンバ

        public override string Convert(System.Data.IDataReader dr, int col, string addstr, string unichar, bool outNull, DBFieldInfo fieldInfo)
        {
            return dr.GetInt32(col).ToString(System.Globalization.CultureInfo.CurrentCulture);
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
                    Int32 localresult;
                    if (Int32.TryParse(data, out localresult) == true)
                    {
                        result = localresult;
                    }
                    else
                    {
                        errmsg = "Int32 の整数を指定してください。";
                    }
                }
            }
            catch
            {
                errmsg = "Int32 の整数を指定してください。";
            }
            return errmsg;

        }

        #endregion
    }
}
