using System;
using System.Collections.Generic;
using System.Text;

namespace quickDBExplorer.DataType
{
    internal class timeType : datetimeBaseType
    {
        public override bool TryParse(string data, DBFieldInfo fieldInfo, ref object result, ref string errmsg)
        {
            return TryParse(data, typeof(System.TimeSpan), "TimeSpanを表す値を指定してください。", ref result, ref errmsg);
        }
    }
}
