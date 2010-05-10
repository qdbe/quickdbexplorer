using System;
using System.Collections.Generic;
using System.Text;

namespace quickDBExplorer
{
    internal class ConnectionInfo
    {
        public string ServerName { get; private set; }
        public string ServerRealName { get; private set; }
        public string InstanceName { get; private set; }
        public string LogOnUid { get; private set; }
        public string LogOnPassword { get; private set; }
        public bool IsUseTruse { get; private set; }
        public ISqlInterface SqlDriver { get; private set; }
        public ServerData ServerDataInfo {get;private set; }
        public SqlVersion SqlVersionInfo { get; private set; }

        public ConnectionInfo(
            string serverName,
            string serverRealName,
            string instanceName,
            string logonUid,
            string logonPassword,
            bool isUseTruse,
            ISqlInterface sqlDriver,
            SqlVersion sqlVersion,
            ServerData serverdata
            )
        {
            this.ServerName = serverName;
            this.ServerRealName = serverRealName;
            this.InstanceName = instanceName;
            this.LogOnUid = logonUid;
            this.LogOnPassword = logonPassword;
            this.IsUseTruse = isUseTruse;
            this.SqlDriver = sqlDriver;
            this.SqlVersionInfo = sqlVersion;
            this.ServerDataInfo = serverdata;
        }
    }
}
