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
        /// 接続先データベース名
        /// </summary>
        public string DatabaseName { get; private set; }
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
        public ServerJsonData ServerDataInfo {get;private set; }
        /// <summary>
        /// DBサーバーバージョン情報
        /// </summary>
        public SqlVersion SqlVersionInfo { get; private set; }

        /// <summary>
        /// 暗号化通信を使用するかどうか
        /// </summary>
        public bool IsUseTrusted { get; set; }

        /// <summary>
        /// SSL証明書エラーを無視するかどうか
        /// </summary>
        public bool IgnoreCertificateError { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="serverName"></param>
        /// <param name="serverRealName"></param>
        /// <param name="instanceName"></param>
        /// <param name="databaseName"></param>
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
            string databaseName,
            string logonUid,
            string logonPassword,
            bool isUseTruse,
            ISqlInterface sqlDriver,
            SqlVersion sqlVersion,
            ServerJsonData serverdata,
            bool isUseTrusted,
            bool ignoreCertificateError
            )
        {
            this.ServerName = serverName;
            this.ServerRealName = serverRealName;
            this.InstanceName = instanceName;
            this.DatabaseName = databaseName;
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
                "",
                "sa",
                "saPassword",
                false,
                new EmptySqlInterface(),
                SqlVersion.SQLSERVER2000(),
                new ServerJsonData(),
                false,
                false
                );
        }
    }
}
