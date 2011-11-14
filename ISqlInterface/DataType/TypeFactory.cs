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

        static private Dictionary<string, baseType> typeDic = new Dictionary<string, baseType>();

        /// <summary>
        /// SQLデータタイプを生成する
        /// </summary>
        /// <param name="typeString">SQLデータタイプの型名(例：nvarchar, int)</param>
        /// <returns></returns>
        public static baseType Create(string typeString)
        {
            string lowerTypeName = typeString.ToLower();
            if (typeDic.ContainsKey(lowerTypeName))
            {
                return typeDic[lowerTypeName];
            }

            Type ctype = Type.GetType("quickDBExplorer.DataType." + typeString.ToLower() + "Type");
            if (ctype == null)
            {
                ctype = Type.GetType("quickDBExplorer.DataType.defaultType");
            }
            baseType result = (baseType)Activator.CreateInstance(ctype);
            typeDic[lowerTypeName] = result;
            return result;
        }
    }
}
