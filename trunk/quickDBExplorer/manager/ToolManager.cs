using System;
using System.Collections.Generic;
using System.Text;
using quickDBExplorer.Forms;
using System.Data;

namespace quickDBExplorer.manager
{
    class ToolManager
    {
        private const string SettingPath = "outerTools.xml";
        private const string TOOLTABLENAME = "TOOLS";
        /// <summary>
        /// ツール情報を保存する
        /// </summary>
        public void Save(List<ToolInfo> saveList)
        {
            DataSet ds = this.Convert2DataSet(saveList);
            ds.WriteXml(SettingPath);
        }

        private DataSet Convert2DataSet(List<ToolInfo> saveList)
        {
            DataSet ds = this.GetSchemaDataSet();

            foreach (ToolInfo each in saveList)
            {
                ds.Tables[TOOLTABLENAME].Rows.Add(new object[] { each.Name, each.Command });
            }
            return ds;
        }

        /// <summary>
        /// ツール情報をロードする
        /// </summary>
        /// <returns></returns>
        public List<ToolInfo> Load()
        {
            DataSet ds = new DataSet();
            try
            {
                ds.ReadXml(SettingPath);
                return Convert2Tools(ds);
            }
            catch (Exception exp)
            {
                exp.ToString();
                return new List<ToolInfo>();
            }
        }

        private List<ToolInfo> Convert2Tools(DataSet ds)
        {
            if (!ds.Tables.Contains(TOOLTABLENAME))
            {
                return new List<ToolInfo>();
            }
            DataTable dt = ds.Tables[TOOLTABLENAME];
            List<ToolInfo> result = new List<ToolInfo>();
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new ToolInfo((string)dr["NAME"], (string)dr["COMMAND"]));
            }
            return result;
        }

        private DataSet GetSchemaDataSet()
        {
            DataSet ds = new DataSet();
            DataTable keyTable = new DataTable(TOOLTABLENAME);
            keyTable.Columns.Add("NAME");
            keyTable.Columns.Add("COMMAND");
            ds.Tables.Add(keyTable);

            return ds;
        }


        internal void DoAction(object p)
        {
            if (!(p is ToolInfo) )
            {
                return;
            }
            ToolInfo info = (ToolInfo)p;

            
        }
    }
}
