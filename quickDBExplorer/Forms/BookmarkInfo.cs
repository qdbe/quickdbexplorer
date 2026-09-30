using System;
using System.Collections.Generic;
using System.Text;

namespace quickDBExplorer.Forms
{
    /// <summary>
    /// 
    /// </summary>
    public class BookmarkInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DBName { get; set; }
        /// <summary>
        ///     
        /// </summary>
        public string []Schema { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<DBObjectInfo> Objects { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbname"></param>
        /// <param name="schema"></param>
        /// <param name="objects"></param>
        public BookmarkInfo(string dbname, string[] schema, List<DBObjectInfo> objects)
        {
            this.DBName = dbname;
            this.Schema = schema;
            this.Objects = objects;

            this.Name  = CreateDefaultName();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dbname"></param>
        /// <param name="schema"></param>
        /// <param name="objects"></param>
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
