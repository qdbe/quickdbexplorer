using System;
using System.Collections.Generic;
using System.Text;

namespace quickDBExplorer.Forms
{
    public class BookmarkInfo
    {
        public string Name { get; set; }
        public string DBName { get; set; }
        public string []Schema { get; set; }
        public List<DBObjectInfo> Objects { get; set; }

        public BookmarkInfo(string dbname, string[] schema, List<DBObjectInfo> objects)
        {
            this.DBName = dbname;
            this.Schema = schema;
            this.Objects = objects;

            this.Name  = CreateDefaultName();
        }

        public BookmarkInfo(string name, string dbname, string[] schema, List<DBObjectInfo> objects)
        {
            this.Name = name;
            this.DBName = dbname;
            this.Schema = schema;
            this.Objects = objects;
        }

        private string CreateDefaultName()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.DBName);
            foreach (string each in Schema)
            {
                sb.Append(":").Append(each);
            }

            foreach (DBObjectInfo eachobj in Objects)
            {
                sb.Append(":").Append(eachobj.ObjName);
            }
            return sb.ToString();
        }
    }
}
