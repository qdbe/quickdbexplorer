using System;
using System.Collections.Generic;
using System.Text;

namespace quickDBExplorer.DataType
{
    internal class ntextType : ncharBaseType
    {
        public override bool TryParse(string data, DBFieldInfo fieldInfo, EmptyNullBehavior isEmptyAsNull, ref object result, ref string errmsg)
        {
            errmsg = null;
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
            else
            {
                result = data;
            }
            return true;
        }

    }
}
