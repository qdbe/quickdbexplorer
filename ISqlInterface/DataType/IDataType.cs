using System;

namespace quickDBExplorer.DataType
{
    interface IDataType
    {
        bool TryParse(string data, quickDBExplorer.DBFieldInfo fieldInfo, EmptyNullBehavior isEmptyAsNull, ref object result, ref string errmsg);

        bool TryCheckObject(object data, quickDBExplorer.DBFieldInfo fieldInfo, EmptyNullBehavior isEmptyAsNull, ref string errmsg);
        string Convert(System.Data.IDataReader dr, int col, string addstr, string unichar, bool outNull, quickDBExplorer.DBFieldInfo fieldInfo);
        string GetFieldTypeString(string fieldname, int length, int prec, int xscale);
        bool CanLoadData();
        bool IsBinary { get; }
        string GetCSharpTypeString();

        bool CheckValue(object val, quickDBExplorer.DBFieldInfo fieldInfo, EmptyNullBehavior isEmptyAsNull, out string errmsg);
    }
}
