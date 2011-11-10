using System;
using System.Collections.Generic;
using System.Text;

namespace quickDBExplorer.DataType
{
    public interface IDataType
    {
        IDataType Create();
        string TypeName { get; }
        string Convert(string data, string addstr, string unichar, bool outNull, DBFieldInfo fieldInfo);
        string CheckForInput(string data, DBFieldInfo fieldInfo);
    }
}
