using System;

namespace quickDBExplorer.DataType
{
    interface IDataType
    {
        string CheckForInput(string data, quickDBExplorer.DBFieldInfo fieldInfo);
        string Convert(System.Data.IDataReader dr, int col, string addstr, string unichar, bool outNull, quickDBExplorer.DBFieldInfo fieldInfo);
        string GetFieldTypeString(string fieldname, int length, int prec, int xscale);
    }
}
