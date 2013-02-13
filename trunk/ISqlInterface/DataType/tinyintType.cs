using System;
using System.Collections.Generic;
using System.Text;

namespace quickDBExplorer.DataType
{
    internal class tinyintType : baseType
    {
        #region IDataType メンバ

        public override string Convert(System.Data.IDataReader dr, int col, string addstr, string unichar, bool outNull, DBFieldInfo fieldInfo)
        {
            return dr.GetValue(col).ToString();
        }

        public override bool TryParse(string data, DBFieldInfo fieldInfo, ref object result, ref string errmsg)
        {
            return TryParse(data, typeof(Byte), "Byte値を指定してください。", ref result, ref errmsg);
        }

        public override bool CanLoadData()
        {
            return true;
        }
        #endregion

        public override string GetCSharpTypeString()
        {
            return "byte";
        }

        public override Type Type
        {
            get { return typeof(byte); }
        }
    }
}
