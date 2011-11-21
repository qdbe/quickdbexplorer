﻿using System;
using System.Collections.Generic;
using System.Text;

namespace quickDBExplorer.DataType
{
    internal class datetime2Type : datetimeBaseType
    {
        public datetime2Type()
        {
            this.TypeHasSize = false;
        }

        public override bool TryParse(string data, DBFieldInfo fieldInfo, ref object result, ref string errmsg)
        {
            return TryParse(data, typeof(DateTime), "DateTime2を表す値を指定してください。", ref result, ref errmsg);
        }
    }
}
