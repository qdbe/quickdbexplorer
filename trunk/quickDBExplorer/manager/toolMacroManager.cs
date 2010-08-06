using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace quickDBExplorer.manager
{
    /// <summary>
    /// マクロへの引数情報
    /// </summary>
    public class MacroArgInfo
    {
        /// <summary>
        /// DBコネクション情報
        /// </summary>
        public ConnectionInfo Connection { get; private set; }
        /// <summary>
        /// DB名
        /// </summary>
        public string DbName { get; private set; }
        /// <summary>
        /// スキーマ名
        /// </summary>
        public string SchemaName { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="dbname"></param>
        /// <param name="schema"></param>
        public MacroArgInfo(ConnectionInfo conn, string dbname, string schema)
        {
            this.Connection = conn;
            this.DbName = dbname;
            this.SchemaName = schema;
        }
    }

    /// <summary>
    /// コンバート関数
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public delegate string StrConverter(MacroArgInfo args);

    /// <summary>
    /// マクロ定義項目情報
    /// </summary>
    public class MacroInfo
    {
        /// <summary>
        /// マクロ名
        /// </summary>
        public string MacroName { get; private set; }
        /// <summary>
        /// コメント
        /// </summary>
        public string Comment { get; private set; }
        /// <summary>
        /// サンプル文字列
        /// </summary>
        public string SampleStr { get; private set; }

        /// <summary>
        /// 文字変換関数
        /// </summary>
        public StrConverter Converter { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name"></param>
        /// <param name="comment"></param>
        /// <param name="sample"></param>
        /// <param name="func"></param>
        public MacroInfo(string name, string comment, string sample, StrConverter func)
        {
            this.MacroName = name;
            this.Comment = comment;
            this.SampleStr = sample;
            this.Converter = func;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name"></param>
        /// <param name="comment"></param>
        /// <param name="func"></param>
        public MacroInfo(string name, string comment, StrConverter func)
        {
            this.MacroName = name;
            this.Comment = comment;
            this.SampleStr = string.Empty;
            this.Converter = func;
        }

        /// <summary>
        /// マクロのパラメータ文字列を返す
        /// このパラメータ文字列をコマンド文字列に埋め込むことで、置換が行われる。
        /// </summary>
        /// <returns></returns>
        public string GetMacroParam()
        {
            return "{$" + this.MacroName + "}";
        }

        /// <summary>
        /// パラメータを置換する
        /// </summary>
        /// <param name="commandString"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public string ReplaceParam(string commandString, MacroArgInfo arg)
        {
            string param = GetMacroParam();
            if (!commandString.Contains(param))
            {
                return commandString;
            }
            return commandString.Replace(param, this.Converter(arg));
        }

        public string BuildSample(ConnectionInfo conn, MainForm forms)
        {
            string result = string.Empty;
            MacroArgInfo arg;
            if (forms != null)
            {
                arg = forms.CreateMacroArg();
            }
            else
            {
                arg = new MacroArgInfo(conn, string.Empty, string.Empty);
            }
            return this.ReplaceParam(this.GetMacroParam(), arg);
        }
    
    }


    /// <summary>
    /// ツール情報管理クラス
    /// </summary>
    public class ToolMacroManager
    {
        private List<MacroInfo> macroList;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ToolMacroManager()
        {
            InitializeMacro();
        }

        private void InitializeMacro()
        {
            macroList = new List<MacroInfo>();
            macroList.Add(new MacroInfo("SERVERNAME", "サーバー名", x => x.Connection.ServerName ));
            macroList.Add(new MacroInfo("SERVERREALNAME", "実サーバー名", x => x.Connection.ServerRealName ));
            macroList.Add(new MacroInfo("INSTANCE", "インスタンス名", x => x.Connection.InstanceName ));
            macroList.Add(new MacroInfo("UID", "ログインID", x => x.Connection.LogOnUid ));
            macroList.Add(new MacroInfo("PASS", "ログインパスワード", x => x.Connection.LogOnPassword));
            macroList.Add(new MacroInfo("TRUSTPARAM", "Windows認証(-t パラメータ)", x => x.Connection.IsUseTruse ? "-t" : string.Empty));
            macroList.Add(new MacroInfo("TRUSTSTRING", "Windows認証(Bool)", x => x.Connection.IsUseTruse.ToString() ));
            macroList.Add(new MacroInfo("SQLFULLVERSION", "SQLSERVERバージョン文字列", x => x.Connection.SqlVersionInfo.FullVersionString));
            macroList.Add(new MacroInfo("SQLPUBLICVERSION", "SQLSERVERバージョン名", x => x.Connection.SqlVersionInfo.PublicVersionNo ));
            macroList.Add(new MacroInfo("DATABASENAME", "データベース名", x => x.DbName ));
            macroList.Add(new MacroInfo("SCHEMANAME", "スキーマ名", x => x.SchemaName ));
            macroList.Add(new MacroInfo("APPPATH", "アプリケーション起動パス", x => Application.ExecutablePath ));
        }

        public IEnumerator<MacroInfo> GetEnumrator()
        {
            return macroList.GetEnumerator();
        }

        /// <summary>
        /// コマンドを実際の文字に変換する
        /// </summary>
        /// <param name="commandString">コマンド文字列</param>
        /// <param name="conn">接続情報</param>
        /// <param name="forms">カレントダイアログ</param>
        /// <returns></returns>
        internal string BuildCommand(string commandString, ConnectionInfo conn, MainForm forms)
        {
            MacroArgInfo arg;
            if( forms != null )
            {
                arg = forms.CreateMacroArg();
            }
            else{
                arg = new MacroArgInfo(conn, string.Empty, string.Empty);
            }

            string result = commandString;
            foreach (MacroInfo each in macroList)
            {
                result = each.ReplaceParam(result, arg);
            }
            return result;
        }

    }
}
