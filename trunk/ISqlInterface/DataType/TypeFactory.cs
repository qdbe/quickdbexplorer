using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace quickDBExplorer.DataType
{
    public class TypeFactory
    {
        public static IDataType Create(string typeString)
        {
            Type ctype = Type.GetType("quickDBExplorer.DataType." + typeString.ToLower() + "Type");
            MethodInfo mi = ctype.GetMethod("Create");
            return (IDataType)mi.Invoke(null, null);
        }
    }
}
