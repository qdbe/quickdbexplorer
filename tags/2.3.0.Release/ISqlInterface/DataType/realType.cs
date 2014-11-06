using System;
using System.Collections.Generic;
using System.Text;
using System.Data;


namespace quickDBExplorer.DataType
{
    internal class realType : baseType
    {
        #region IDataType メンバ

        public override string Convert(IDataReader dr, int col, string addstr, string unichar, bool outNull, DBFieldInfo fieldInfo)
        {
            return dr.GetValue(col).ToString();
        }

        public override bool TryParse(string data, DBFieldInfo fieldInfo, ref object result, ref string errmsg)
        {
            return TryParse(data, typeof(Single), "Single値を指定してください。", ref result, ref errmsg);
        }

        public override bool CanLoadData()
        {
            return true;
        }


        #endregion

        public override string GetCSharpTypeString()
        {
            return "Single";
        }

        public override Type Type
        {
            get { return typeof(Single); }
        }
    }
}
