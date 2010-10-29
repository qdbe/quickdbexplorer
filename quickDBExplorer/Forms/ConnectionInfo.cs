using System;
using System.Collections.Generic;
using System.Text;

namespace quickDBExplorer
{
    /// <summary>
    /// DB接続情報
    /// </summary>
    public class ConnectionInfo
    {
        /// <summary>
        /// サーバー名
        /// </summary>
        public string ServerName { get; private set; }
        /// <summary>
        /// 実サーバー名
        /// </summary>
        public string ServerRealName { get; private set; }
        /// <summary>
        /// インスタンス名
        /// </summary>
        public string InstanceName { get; private set; }
        /// <summary>
        /// ログインID
        /// </summary>
        public string LogOnUid { get; private set; }
        /// <summary>
        /// ログインパスワード
        /// </summary>
        public string LogOnPassword { get; private set; }
        /// <summary>
        /// 信頼関係接続
        /// </summary>
        public bool IsUseTruse { get; private set; }
        /// <summary>
        /// SQLドライバー情報
        /// </summary>
        public ISqlInterface SqlDriver { get; private set; }
        /// <summary>
        /// 前回終了値
        /// </summary>
        public ServerData ServerDataInfo {get;private set; }
        /// <summary>
        /// DBサーバーバージョン情報
        /// </summary>
        public SqlVersion SqlVersionInfo { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="serverName"></param>
        /// <param name="serverRealName"></param>
        /// <param name="instanceName"></param>
        /// <param name="logonUid"></param>
        /// <param name="logonPassword"></param>
        /// <param name="isUseTruse"></param>
        /// <param name="sqlDriver"></param>
        /// <param name="sqlVersion"></param>
        /// <param name="serverdata"></param>
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

        /// <summary>
        /// ダミーのコネクション情報を生成する
        /// </summary>
        /// <returns></returns>
        public static ConnectionInfo CreateDummyConnection()
        {
            return new ConnectionInfo(
                "DummyServerName",
                "DummyServerName",
                "SQLSERVER",
                "sa",
                "saPassword",
                false,
                new EmptySqlInterface(),
                SqlVersion.SQLSERVER2000(),
                new ServerData()
                );
        }
    }
}
