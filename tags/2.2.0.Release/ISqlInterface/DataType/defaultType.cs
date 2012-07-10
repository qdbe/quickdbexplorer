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

        public override bool TryParse(string data, DBFieldInfo fieldInfo, ref object result, ref string errmsg)
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
    }
}
