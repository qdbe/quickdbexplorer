using System;
using System.Collections.Generic;
using System.Text;

namespace quickDBExplorer.DataType
{
    internal abstract class baseType : IDataType
    {
        public string TypeName { get; private set; }

        private baseType()
        {
            this.TypeName = "baseType";
        }

        public abstract static IDataType Create();

        public abstract string Convert(string data, string addstr, string unichar, bool outNull, DBFieldInfo fieldInfo);

        public abstract string CheckForInput(string data, DBFieldInfo fieldInfo);

    }
}
