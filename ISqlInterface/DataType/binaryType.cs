using System;
using System.Collections.Generic;
using System.Text;

namespace quickDBExplorer.DataType
{
    internal class binaryType : binaryBaseType
    {
        public binaryType()
        {
            this.TypeHasSize = true;
        }

        public override string GetFieldExcelOutString(string typename, int length, int prec, int xscale, bool isComma)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(typename);
            sb.Append(this.GetSeparator(isComma));
            sb.Append(length);
            sb.Append(this.GetSeparator(isComma));
            return sb.ToString();

        }
    }
}
