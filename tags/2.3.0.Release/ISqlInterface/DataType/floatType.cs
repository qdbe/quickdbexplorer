using System;
using System.Collections.Generic;
using System.Text;

namespace quickDBExplorer.DataType
{
    internal class floatType : decimalpointBaseType
    {
        public override string GetCSharpTypeString()
        {
            return "float";
        }

        public override Type Type
        {
            get
            {
                return typeof(float);
            }
        }
    }
}
