using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace quickDBExplorer.DataType
{
    internal class xmlType : ncharBaseType
    {
        public xmlType()
        {
            TypeHasSize = false;
            this.DefalutParseString = "<a/>";
        }

        public override bool TryParse(string data, DBFieldInfo fieldInfo, ref object result, ref string errmsg)
        {
            try
            {
                if (string.IsNullOrEmpty(data.Trim()))
                {
                    result = DBNull.Value;
                    return true;
                }
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(data);
                result = doc;
                return true;
            }
            catch(Exception e)
            {
                errmsg = e.Message;
                return false;
            }
        }

        public override string GetCSharpTypeString()
        {
            return "XmlDocument";
        }

        public override Type Type
        {
            get
            {
                return typeof(XmlDocument);
            }
        }
    }
}
