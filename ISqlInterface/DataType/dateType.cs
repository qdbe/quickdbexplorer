﻿using System;
using System.Collections.Generic;
using System.Text;

namespace quickDBExplorer.DataType
{
    internal class dateType : datetimeBaseType
    {
        public override bool TryParse(string data, DBFieldInfo fieldInfo, EmptyNullBehavior isEmptyAsNull, ref object result, ref string errmsg)
        {
            return TryParse(data, typeof(DateTime), "DateTimeを表す値を指定してください。", isEmptyAsNull, ref result, ref errmsg);
        }
    }
}
