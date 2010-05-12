using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using quickDBExplorer.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace quickDBExplorer.manager
{
    class BookmarkManager
    {
        public BookmarkManager()
        {
        }

        public void Save(Dictionary<string,List<BookmarkInfo>> bookmark)
        {
            DataSet ds = this.Convert2DataSet(bookmark);
            ds.WriteXml("Bookmark.xml");
        }

        public Dictionary<string, List<BookmarkInfo>> Load()
        {

            DataSet ds = new DataSet();
            ds.ReadXml("Bookmark.xml");

            Dictionary<string, List<BookmarkInfo>> bookmark = new Dictionary<string, List<BookmarkInfo>>();

            return bookmark;
        }

        private DataSet GetSchemaDataSet()
        {
            DataSet ds = new DataSet();
            DataTable baseTable = new DataTable("BASE");
            baseTable.Columns.Add("SERVERNAME");
            baseTable.Columns.Add("NAME");
            baseTable.Columns.Add("DBNAME");
            DataTable schemaTable = new DataTable("SCHEMA");
            schemaTable.Columns.Add("SERVERNAME");
            schemaTable.Columns.Add("NAME");
            schemaTable.Columns.Add("SCHEMANAME");

            DataTable objectTable = new DataTable("OBJECTS");
            objectTable.Columns.Add("SERVERNAME");
            objectTable.Columns.Add("NAME");
            objectTable.Columns.Add("ObjType");
            objectTable.Columns.Add("Owner");
            objectTable.Columns.Add("ObjectName");
            objectTable.Columns.Add("CreateTime");
            objectTable.Columns.Add("SynonymBase");
            objectTable.Columns.Add("SynonymBaseType");

            ds.Tables.Add(baseTable);
            ds.Tables.Add(schemaTable);
            ds.Tables.Add(objectTable);

            return ds;
        }

        /// <summary>
        /// ブックマークの情報をDataSetに変換する
        /// </summary>
        /// <param name="bookmark"></param>
        /// <returns></returns>
        private DataSet Convert2DataSet(Dictionary<string,List<BookmarkInfo>> bookmark)
        {
            DataSet result = this.GetSchemaDataSet();
            foreach (string key in bookmark.Keys)
            {
                List<BookmarkInfo> eachList = bookmark[key];
                foreach (BookmarkInfo each in eachList)
                {
                    Convert2DataSetEach(key, each, result);
                }
            }

            return result;
        }

        private void Convert2DataSetEach(string key, BookmarkInfo bookmarkInfo, DataSet result)
        {
            DataTable baseTable = result.Tables["BASE"];
            DataTable schemaTable = result.Tables["SCHEMA"];
            DataTable objectTable = result.Tables["OBJECTS"];

            baseTable.Rows.Add(new object[]{
                key,
                bookmarkInfo.Name,
                bookmarkInfo.DBName
            });

            foreach (string eachSchema in bookmarkInfo.Schema)
            {
                schemaTable.Rows.Add(new object[] {
                    key,
                    bookmarkInfo.Name,
                    eachSchema }
                    );
            }

            foreach (DBObjectInfo eachObj in bookmarkInfo.Objects)
            {
                objectTable.Rows.Add(new object[] {
                    key,
                    bookmarkInfo.Name,
                    eachObj.ObjType,
                    eachObj.Owner,
                    eachObj.ObjName,
                    eachObj.CreateTime,
                    eachObj.SynonymBase,
                    eachObj.SynonymBaseType }
                    );
            }
        }

    }
}
