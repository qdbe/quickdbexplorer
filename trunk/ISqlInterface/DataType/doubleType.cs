using System;
using System.Collections.Generic;
using System.Text;

namespace quickDBExplorer.DataType
{
    internal class doubleType : decimalpointBaseType
    {
        public override string GetCSharpTypeString()
        {
            return "double";
        }

        public override Type Type
        {
            get
            {
                return typeof(double);
            }
        }
    }
}
