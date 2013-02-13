using System;

namespace quickDBExplorer.DataType
{
    interface IDataType
    {
        bool TryParse(string data, quickDBExplorer.DBFieldInfo fieldInfo, ref object result, ref string errmsg);
        string Convert(System.Data.IDataReader dr, int col, string addstr, string unichar, bool outNull, quickDBExplorer.DBFieldInfo fieldInfo);
        string GetFieldTypeString(string fieldname, int length, int prec, int xscale);
        bool CanLoadData();
        bool IsBinary { get; }
        string GetCSharpTypeString();
    }
}
