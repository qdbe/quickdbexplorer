using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace quickDBExplorer.DataType
{
    /// <summary>
    /// SQLデータタイプのファクトリ
    /// </summary>
    public class TypeFactory
    {

        static private Dictionary<string, IDataType> typeDic = new Dictionary<string, IDataType>();

        /// <summary>
        /// SQLデータタイプを生成する
        /// </summary>
        /// <param name="typeString">SQLデータタイプの型名(例：nvarchar, int)</param>
        /// <returns></returns>
        public static IDataType Create(string typeString)
        {
            string lowerTypeName = typeString.ToLower();
            if (typeDic.ContainsKey(lowerTypeName))
            {
                return typeDic[lowerTypeName];
            }

            Type ctype = Type.GetType("quickDBExplorer.DataType." + typeString.ToLower() + "Type");
            IDataType result = (IDataType)Activator.CreateInstance(ctype);
            typeDic[lowerTypeName] = result;
            return result;
        }
    }
}
