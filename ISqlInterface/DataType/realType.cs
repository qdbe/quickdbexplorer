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

        public override bool TryParse(string data, DBFieldInfo fieldInfo, EmptyNullBehavior isEmptyAsNull, ref object result, ref string errmsg)
        {
            return TryParse(data, typeof(Single), "Single値を指定してください。", isEmptyAsNull, ref result, ref errmsg);
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

        public override string GetFieldExcelOutString(string typename, int length, int prec, int xscale, bool isComma)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(typename);
            sb.Append(this.GetSeparator(isComma));
            sb.Append(this.GetSeparator(isComma));
            return sb.ToString();

        }

        public override Type Type
        {
            get { return typeof(Single); }
        }
    }
}
