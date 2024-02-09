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

        public override string GetFieldExcelOutString(string typename, int length, int prec, int xscale, bool isComma)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(typename);
            sb.Append(this.GetSeparator(isComma));
            if (length < 0)
            {
                sb.Append("max");
            }
            else
            {
                sb.Append(length);
            }
            sb.Append(this.GetSeparator(isComma));
            return sb.ToString();

        }
    }
}
