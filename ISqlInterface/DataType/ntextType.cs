using System;
using System.Collections.Generic;
using System.Text;

namespace quickDBExplorer.DataType
{
    internal class ntextType : ncharBaseType
    {
        public override bool TryParse(string data, DBFieldInfo fieldInfo, ref object result, ref string errmsg)
        {
            errmsg = null;
            result = data;
            return true;
        }

    }
}
