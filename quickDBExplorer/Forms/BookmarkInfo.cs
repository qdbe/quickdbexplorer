using System;
using System.Collections.Generic;
using System.Text;

namespace quickDBExplorer.Forms
{
    class BookmarkInfo
    {
        public string Name { get; private set; }
        public string DBName { get; private set; }
        public string []Schema { get; private set; }
        public List<DBObjectInfo> Objects { get; private set; }

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
