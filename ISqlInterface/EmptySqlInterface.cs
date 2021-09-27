using System;
using System.Collections.Generic;
using System.Text;

namespace quickDBExplorer
{
    /// <summary>
    /// 空の ISqlInterface を返す
    /// </summary>
    public class EmptySqlInterface : ISqlInterface
    {
        #region ISqlInterface メンバ

        /// <summary>
        /// 空処理
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="timeout"></param>
        public void SetConnection(System.Data.IDbConnection sqlConnection, int timeout)
        {
            
        }

        /// <summary>
        /// 空処理
        /// </summary>
        public void CloseConnection()
        {
            
        }

        /// <summary>
        /// 空処理
        /// </summary>
        /// <param name="timeout"></param>
        public void SetTimeout(int timeout)
        {
            
        }

        /// <summary>
        /// 空処理
        /// </summary>
        /// <returns></returns>
        public System.Data.Common.DbDataAdapter NewDataAdapter()
        {
            return null;
        }

        /// <summary>
        /// 空処理
        /// </summary>
        /// <returns></returns>
        public System.Data.IDbCommand NewSqlCommand()
        {
            return null;
        }

        /// <summary>
        /// 空処理
        /// </summary>
        /// <param name="stSql"></param>
        /// <returns></returns>
        public System.Data.IDbCommand NewSqlCommand(string stSql)
        {
            return null;
        }

        /// <summary>
        /// 空処理
        /// </summary>
        /// <param name="da"></param>
        /// <param name="cmd"></param>
        public void SetSelectCmd(System.Data.Common.DbDataAdapter da, System.Data.IDbCommand cmd)
        {
            
        }

        /// <summary>
        /// 空処理
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public System.Data.IDbTransaction SetTransaction(System.Data.IDbCommand cmd)
        {
            return null;
        }

        /// <summary>
        /// 空処理
        /// </summary>
        /// <param name="da"></param>
        public void SetCommandBuilder(System.Data.Common.DbDataAdapter da)
        {
            
        }

        /// <summary>
        /// 空処理
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public byte[] GetDataReaderBytes(System.Data.IDataReader dr, int col)
        {
            return null;
        }

        /// <summary>
        /// 空処理
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public DateTimeOffset GetDataReaderDateTimeOffSet(System.Data.IDataReader dr, int col)
        {
            return DateTimeOffset.Now;
        }

        /// <summary>
        /// 空処理
        /// </summary>
        /// <returns></returns>
        public string GetDBSelect()
        {
            return string.Empty;
        }

        /// <summary>
        /// 空処理
        /// </summary>
        /// <param name="dbName"></param>
        public void SetDatabase(string dbName)
        {
            
        }

        /// <summary>
        /// 空処理
        /// </summary>
        /// <returns></returns>
        public string GetTableListColumnName()
        {
            return string.Empty;
        }

        /// <summary>
        /// 空処理
        /// </summary>
        /// <returns></returns>
        public string GetOwnerLabel1()
        {
            return string.Empty;
        }

        /// <summary>
        /// 空処理
        /// </summary>
        /// <returns></returns>
        public string GetOwnerLabel2()
        {
            return string.Empty;
        }

        /// <summary>
        /// 空処理
        /// </summary>
        /// <param name="isDisplayTable"></param>
        /// <param name="isDisplayView"></param>
        /// <param name="isSynonym"></param>
        /// <param name="isDisplayFunction"></param>
        /// <param name="isDisplaySP"></param>
        /// <param name="ownerList"></param>
        /// <returns></returns>
        public string GetDisplayObjList(bool isDisplayTable, bool isDisplayView, bool isSynonym, bool isDisplayFunction, bool isDisplaySP, string ownerList)
        {
            return string.Empty;
        }

        /// <summary>
        /// 空処理
        /// </summary>
        /// <param name="isDisplaySysUser"></param>
        /// <returns></returns>
        public string GetOwnerList(bool isDisplaySysUser)
        {
            return string.Empty;
        }

        /// <summary>
        /// 空処理
        /// </summary>
        /// <param name="serverRealName"></param>
        /// <param name="instanceName"></param>
        /// <param name="isUseTrust"></param>
        /// <param name="dbName"></param>
        /// <param name="logOnUserId"></param>
        /// <param name="logOnPassword"></param>
        public void CallISQL(string serverRealName, string instanceName, bool isUseTrust, string dbName, string logOnUserId, string logOnPassword)
        {
            
        }

        /// <summary>
        /// 空処理
        /// </summary>
        /// <param name="serverRealName"></param>
        /// <param name="instanceName"></param>
        /// <param name="isUseTrust"></param>
        /// <param name="dbName"></param>
        /// <param name="logOnUserId"></param>
        /// <param name="logOnPassword"></param>
        public void CallEPM(string serverRealName, string instanceName, bool isUseTrust, string dbName, string logOnUserId, string logOnPassword)
        {
            
        }

        /// <summary>
        /// 空処理
        /// </summary>
        /// <param name="serverRealName"></param>
        /// <param name="instanceName"></param>
        /// <param name="isUseTrust"></param>
        /// <param name="dbName"></param>
        /// <param name="logOnUserId"></param>
        /// <param name="logOnPassword"></param>
        public void CallProfile(string serverRealName, string instanceName, bool isUseTrust, string dbName, string logOnUserId, string logOnPassword)
        {
            
        }

        /// <summary>
        /// 空処理
        /// </summary>
        /// <param name="databaseObjectInfo"></param>
        /// <param name="useParentheses"></param>
        /// <returns></returns>
        public string GetDdlCreateString(DBObjectInfo databaseObjectInfo, bool useParentheses)
        {
            return string.Empty;
        }

        /// <summary>
        /// 空処理
        /// </summary>
        /// <param name="databaseObjectInfo"></param>
        /// <returns></returns>
        public string GetDDLDropStr(DBObjectInfo databaseObjectInfo)
        {
            return string.Empty;
        }

        /// <summary>
        /// 空処理
        /// </summary>
        /// <param name="objTable"></param>
        public void InitObjTable(System.Data.DataTable objTable)
        {
            
        }

        /// <summary>
        /// 空処理
        /// </summary>
        /// <param name="databaseObjectInfo"></param>
        /// <param name="dt"></param>
        public void AddObjectInfo(DBObjectInfo databaseObjectInfo, System.Data.DataTable dt)
        {
            
        }

        /// <summary>
        /// 空処理
        /// </summary>
        /// <returns></returns>
        public DataGetEventHandler ObjectDetailSet()
        {
            return null;
        }

        /// <summary>
        /// 空処理
        /// </summary>
        /// <param name="searchCondition"></param>
        /// <param name="searchType"></param>
        /// <param name="limitSchema"></param>
        /// <param name="condition">検索条件</param>
        /// <returns></returns>
        public string GetSearchFieldSql(string searchCondition, SearchType searchType, List<string> limitSchema, ObjectSearchCondition condition)
        {
            return string.Empty;
        }

        /// <summary>
        /// 空処理
        /// </summary>
        /// <param name="searchCondition"></param>
        /// <param name="searchType"></param>
        /// <param name="limitSchema"></param>
        /// <param name="condition">検索条件</param>
        /// <returns></returns>
        public string GetSearchObjectSql(string searchCondition, SearchType searchType, List<string> limitSchema, ObjectSearchCondition condition)
        {
            return string.Empty;
        }

        /// <summary>
        /// 再接続する
        /// </summary>
        public void ReConnect()
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// SQL Version情報
        /// </summary>
        protected SqlVersion sqlVersion { get; set; }

        /// <summary>
        /// SQL Version情報をセットする
        /// </summary>
        /// <param name="version"></param>
        public void SetupVersion(SqlVersion version)
        {
            this.sqlVersion = version;
        }

    }
}
