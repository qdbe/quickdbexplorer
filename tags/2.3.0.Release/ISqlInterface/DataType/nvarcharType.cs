using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using quickDBExplorer;

namespace quickDBExplorer.DataType
{
    internal class nvarcharType : ncharBaseType
    {
        public nvarcharType()
        {
            this.TypeHasSize = true;
        }
    }
}
