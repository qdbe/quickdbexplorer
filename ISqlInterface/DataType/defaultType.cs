using System;
using System.Collections.Generic;
using System.Text;

namespace quickDBExplorer.DataType
{
    internal class defaultType : baseType
    {
        public override string Convert(System.Data.IDataReader dr, int col, string addstr, string unichar, bool outNull, DBFieldInfo fieldInfo)
        {
            throw new NotImplementedException();
        }

        public override bool TryParse(string data, DBFieldInfo fieldInfo, EmptyNullBehavior isEmptyAsNull, ref object result, ref string errmsg)
        {
            if (fieldInfo.IsAssembly)
            {
                // 何もしない
            }
            else
            {
                // 想定外の型の場合、文字列扱いにする
                // 桁数の制限もなにもわからないので、チェックなし
                result = data;
            }
            return true;
        }

        public override bool CanLoadData()
        {
            return false;
        }

        public override string GetCSharpTypeString()
        {
            return "object";
        }

        public override string GetFieldExcelOutString(string typename, int length, int prec, int xscale, bool isComma)
        {
            StringBuilder sb = new StringBuilder();
            if (!this.TypeHasSize)
            {
                sb.Append(typename);
                sb.Append(this.GetSeparator(isComma));
                sb.Append(this.GetSeparator(isComma));
            }
            else
            {
                sb.Append(typename);
                sb.Append(this.GetSeparator(isComma));
                if (prec > 0)
                {
                    sb.Append(prec + "," + xscale);
                }
                else
                {
                    if (length < 0)
                    {
                        sb.Append("max");
                    }
                    else
                    {
                        sb.Append(length);
                    }
                }
                sb.Append(this.GetSeparator(isComma));
            }
            return sb.ToString();

        }

        public override Type Type
        {
            get { return typeof(object); }
        }
    }
}
