using System;
using System.Collections;
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

        /// <summary>
        /// ブックマークを保存する
        /// </summary>
        /// <param name="bookmark"></param>
        public void Save(Dictionary<string,List<BookmarkInfo>> bookmark)
        {
            DataSet ds = this.Convert2DataSet(bookmark);
            ds.WriteXml("Bookmark."+System.Environment.MachineName+".xml");
        }

        /// <summary>
        /// ブックマークをディスクから読み込む
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, List<BookmarkInfo>> Load()
        {
            DataSet ds = new DataSet();
            Dictionary<string, List<BookmarkInfo>> bookmark;
            try
            {
                if (System.IO.File.Exists("Bookmark."+System.Environment.MachineName+".xml"))
                {
                    ds.ReadXml("Bookmark." + System.Environment.MachineName +".xml");
                }
                else 
                {
                    if (System.IO.File.Exists("Bookmark.xml"))
                    {
                        ds.ReadXml("Bookmark.xml");
                    }
                }
                bookmark = Convert2Bookmark(ds);
            }
            catch (Exception exp)
            {
                exp.ToString();
                bookmark = new Dictionary<string, List<BookmarkInfo>>();
            }


            return bookmark;
        }

        private DataSet GetSchemaDataSet()
        {
            DataSet ds = new DataSet();
            DataTable keyTable = new DataTable("KEY");
            keyTable.Columns.Add("SERVERNAME");
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

            ds.Tables.Add(keyTable);
            ds.Tables.Add(baseTable);
            ds.Tables.Add(schemaTable);
            ds.Tables.Add(objectTable);

            return ds;
        }

        private Dictionary<string, List<BookmarkInfo>> Convert2Bookmark(DataSet ds)
        {
            Dictionary<string, List<BookmarkInfo>> result = new Dictionary<string, List<BookmarkInfo>>();

            if( ds.Tables.Count != 4 )  return result;

            DataTable keyTable = ds.Tables["KEY"];
            if (keyTable == null || keyTable.Rows.Count == 0) return result;

            DataTable baseTable = ds.Tables["BASE"];
            if (baseTable == null) return result;
            baseTable.DefaultView.Sort = "SERVERNAME";
            
            DataTable schemaTable = ds.Tables["SCHEMA"];
            if (schemaTable == null) return result;
            schemaTable.DefaultView.Sort = "SERVERNAME,NAME";

            DataTable objectTable = ds.Tables["OBJECTS"];
            if (objectTable == null) return result;
            objectTable.DefaultView.Sort = "SERVERNAME,NAME";


            string preKey = string.Empty;

            foreach (DataRow keyDr in keyTable.Rows)
            {
                string key = (string)keyDr["SERVERNAME"];
                List<BookmarkInfo> addInfo = new List<BookmarkInfo>();

                foreach (DataRowView baseDr in baseTable.DefaultView.FindRows(key))
                {
                    string name = (string)baseDr["NAME"];
                    string dbname = (string)baseDr["DBNAME"];

                    ArrayList schemaAr = new ArrayList();
                    foreach (DataRowView schemaDr in schemaTable.DefaultView.FindRows(new object[] { key, name }))
                    {
                        schemaAr.Add((string)schemaDr["SCHEMANAME"]);
                    }
                    
                    List<DBObjectInfo> objs = new List<DBObjectInfo>();
                    foreach (DataRowView objDr in objectTable.DefaultView.FindRows(new object[] { key, name }))
                    {
                        DBObjectInfo newInfo = new DBObjectInfo(
                            (int)objDr["objectid"],
                            (string)objDr["ObjType"],
                            (string)objDr["Owner"],
                            (string)objDr["ObjectName"],
                            (string)objDr["CreateTime"],
                            (string)objDr["ModifyTime"],
                            (string)objDr["SynonymBase"],
                            (string)objDr["SynonymBaseType"]
                            );
                        objs.Add(newInfo);
                    }
                    BookmarkInfo bookmark = new BookmarkInfo(dbname, (string[])schemaAr.ToArray(typeof(string)), objs);

                    addInfo.Add(bookmark);
                }
                result.Add(key, addInfo);
            }

            return result;
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
                DataTable keyTable = result.Tables["KEY"];
                keyTable.Rows.Add(new object[] { key });
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
