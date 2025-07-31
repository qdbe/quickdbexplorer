using System;
using System.Collections.Generic;
using System.Text;
using quickDBExplorer.Forms;
using System.Data;

namespace quickDBExplorer.manager
{
    class ToolManager
    {
        private const string SettingPath = "outerTools.";
        private const string TOOLTABLENAME = "TOOLS";
        /// <summary>
        /// ツール情報を保存する
        /// </summary>
        public void Save(List<ToolInfo> saveList)
        {
            DataSet ds = this.Convert2DataSet(saveList);
            ds.WriteXml(SettingPath + System.Environment.MachineName + ".xml");
        }

        private DataSet Convert2DataSet(List<ToolInfo> saveList)
        {
            DataSet ds = this.GetSchemaDataSet();

            foreach (ToolInfo each in saveList)
            {
                ds.Tables[TOOLTABLENAME].Rows.Add(new object[] { each.Name, each.Command, each.Args });
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
                if (System.IO.File.Exists(SettingPath + System.Environment.MachineName + ".xml"))
                {
                    ds.ReadXml(SettingPath + System.Environment.MachineName + ".xml");
                }
                else
                {
                    if (System.IO.File.Exists(SettingPath + ".xml"))
                    {
                        ds.ReadXml(SettingPath + ".xml");
                    }
                }
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
                result.Add(new ToolInfo((string)dr["NAME"], (string)dr["COMMAND"], (string)dr["ARGS"]));
            }
            return result;
        }

        private DataSet GetSchemaDataSet()
        {
            DataSet ds = new DataSet();
            DataTable keyTable = new DataTable(TOOLTABLENAME);
            keyTable.Columns.Add("NAME");
            keyTable.Columns.Add("COMMAND");
            keyTable.Columns.Add("ARGS");
            ds.Tables.Add(keyTable);

            return ds;
        }
    }
}
