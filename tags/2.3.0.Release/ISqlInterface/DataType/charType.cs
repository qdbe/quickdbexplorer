using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace quickDBExplorer.DataType
{
    internal class charType : charBaseType
    {
        public charType()
        {
            this.TypeHasSize = true;
            this.IsSingleByte = true;
        }
    }
}
