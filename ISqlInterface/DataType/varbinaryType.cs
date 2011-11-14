using System;
using System.Collections.Generic;
using System.Text;
using System.Data;


namespace quickDBExplorer.DataType
{
    internal class varbinaryType : binaryBaseType
    {
        public varbinaryType()
        {
            this.TypeHasSize = true;
        }
    }
}
